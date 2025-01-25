import { useEffect, useState } from 'react';
import validator from 'validator';
import './App.css';
import { Step, Stepper, StepLabel } from '@mui/material';

function App() {
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState([]);
    const [currentIndex, setCurrentIndex] = useState(0);
    const [email, setEmail] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchQuestions();
    }, []);

    // fetches trivia's questions from the backend
    // initialises answer id's and user answer array
    async function fetchQuestions() {
        try {
            const response = await fetch('http://localhost:5244/api/quiz/questions');
            if (response.ok) {
                const data = await response.json();
                setQuestions(data.result);

                const initialAnswers = data.result.map((question) => ({
                    Id: question.id,
                    UserAnswer: [],
                }));
                setAnswers(initialAnswers);
            } else {
                console.error('Failed to fetch questions');
            }
        } catch (error) {
            console.error('Error fetching questions:', error);
        } finally {
            setLoading(false);
        }
    }

    // handles button click event when button 'Next' is pressed
    // increments the current index
    const handleNext = () => {
            setCurrentIndex(currentIndex + 1);
    };

    // handles button click event when button 'Previous' is pressed
    // decrements the current index
    const handlePrevious = () => {
        if (currentIndex > 0) {
            setCurrentIndex(currentIndex - 1);
        }
    };

    // validates user's email
    // returns false if invalid
    // returns true if valid
    const validateEmail = () => {
        if (!email) {
            alert('Please enter your email before submitting');
            return false;
        }
        if (!validator.isEmail(email)) {
            alert('Please enter a valid email')
            return false;
        }
        return true;
    }

    // handles button click event when button 'Submit' is pressed
    // sends user's email and answers to the backend for calculation
    // fetches user's score
    const handleSubmit = async () => {
        try {
            if (!validateEmail()) return;

            const payload = { email, answers };

            const response = await fetch('http://localhost:5244/api/quiz/submit', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                const result = await response.json();
                alert(`Quiz finished! Your score is: ${result}`);
            } else {
                console.error('Failed to submit answers');
            }
        } catch (error) {
            console.error('Error submitting quiz:', error);
        }
    };

    // updates the marked answer option
    const handleInputChange = (index, value) => {
        const updatedAnswers = [...answers];
        const userAnswer = Array.isArray(value) ? value : [value];
        updatedAnswers[index] = { ...updatedAnswers[index], UserAnswer: userAnswer };
        setAnswers(updatedAnswers);
    };

    // renders the structure of trivia page
    function quizContent() {
        if (questions.length === 0) {
            return <p>No questions found. Please refresh the page.</p>;
        }
        
        if (currentIndex < questions.length) {
            return (
                addStepper() &&
                <div>
                    <div style={{ marginBottom: '20px' }}>
                        <h3>{questions[currentIndex].text}</h3>
                        {renderQuestionInput(questions[currentIndex])}
                    </div>

                    <button
                        onClick={handlePrevious}
                        disabled={currentIndex === 0}
                        style={{ marginRight: '10px' }}
                    >
                        Previous
                    </button>

                    <button
                        onClick={handleNext}
                        style={{ marginRight: '10px' }}
                    >
                        Next
                    </button>
                </div>
            )
        }

        return emailInput();
    }

    // renders the email input structure upon completion of the quiz
    function emailInput() {
        return (
            <div>
                <div style={{ marginBottom: '20px' }}>
                    <h3>Enter your email here before submitting</h3>
                    <input
                        type="email"
                        placeholder="Enter your email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                        style={{ marginRight: '10px' }}
                    />
                </div>

                <button
                    onClick={handlePrevious}
                    disabled={currentIndex === 0}
                    style={{ marginRight: '10px' }}
                >
                    Previous
                </button>

                <button onClick={handleSubmit}>Submit</button>
            </div>
        );
    }

    // renders the structure of question and it's options
    // calls handleInputChange to update checked options
    function renderQuestionInput(question) {
        const currentAnswer = answers.find((answer) => answer.Id === question.id)?.UserAnswer || [];

        switch (question.type) {
            case 1:
                return radioButtonQuestion(question, currentAnswer)
            case 2:
                return checkBoxQuestion(question, currentAnswer)
            case 3:
                return textBoxQuestion(question, currentAnswer)
            default:
                return null;
        }
    }

    // structures the page for radioButton type question
    function radioButtonQuestion(question, currentAnswer) {
        return question.options.map((option, index) => (
            <div key={index}>
                <label>
                    <input
                        type="radio"
                        name={`question-${question.id}`}
                        value={option}
                        checked={currentAnswer[0] === option}
                        onChange={(e) =>
                            handleInputChange(question.id - 1, e.target.value)
                        }
                    />
                    {option}
                </label>
            </div>
        ));
    }

    // structures the page for checkBox type question
    function checkBoxQuestion(question, currentAnswer) {
        const currentAnswers = currentAnswer ? currentAnswer : [];
        return question.options.map((option, index) => (
            <div key={index}>
                <label>
                    <input
                        type="checkbox"
                        value={option}
                        checked={currentAnswers.includes(option)}
                        onChange={(e) => {
                            const updatedAnswers = e.target.checked
                                ? [...currentAnswers, option]
                                : currentAnswers.filter((ans) => ans !== option);
                            handleInputChange(question.id - 1, updatedAnswers);
                        }}
                    />
                    {option}
                </label>
            </div>
        )); 
    }

    // structures the page for textBox type question
    function textBoxQuestion(question, currentAnswer) {
        return (
            <div>
                <input
                    type="text"
                    placeholder="Your answer"
                    value={currentAnswer[0] || ''}
                    onChange={(e) =>
                        handleInputChange(question.id - 1, e.target.value)
                    }
                />
            </div>
        );
    }

    // adds react stepper 
    function addStepper() {
        return (
            <div>
                <Stepper activeStep={currentIndex}
                    style={{ maxWidth: '40%' }}>
                    {createSteps()}
                </Stepper>
            </div>
        )
    }

    // creates all the steps
    function createSteps() {
        return questions.map((index) => (
            <Step key={index}>
                <StepLabel></StepLabel>
            </Step>
        ));
    }

    // returns trivia's page structure
    return <div style={{ placeItems: 'center' }}>
        <div>
        <h1>Quiz</h1>
        {loading ? (
            <p><em>Loading questions...</em></p>
        ) : (
            quizContent()
        )}
    </div>
    <br></br>
        { addStepper() }
    </div>
}

export default App;