using Trivia.Models;

namespace ZalgirisTrivia.Server.Utils
{
    /// <summary>
    /// class designed to calculate user's score
    /// </summary>
    public class Calculations
    {
        private static List<Question> questions = InOut.GetQuestions(DatabaseUtils.GetPath());
        private static int score = 0; 

        /// <summary>
        /// the main method which calculates the score of the user
        /// </summary>
        /// <param name="answers"> user's answers </param>
        private static void CalculateScore(List<Answer> answers) 
        {
            score = 0;
            for (int i = 0; i < questions.Count; i++) 
            {
                switch ((int)questions[i].Type)
                {
                    case 1:
                        CalculateRadioPoints(questions[i], answers[i]);
                        break;
                    case 2:
                        CalculateCheckBoxPoints(questions[i], answers[i]);
                        break;
                    case 3:
                        CalculateTextBoxPoints(questions[i], answers[i]);
                        break;
                    default:
                        return;
                }
            }
        }

        /// <summary>
        /// helper method that specifically calculates score from radioButton questions
        /// </summary>
        /// <param name="question"> current question </param>
        /// <param name="answer"> current user's answer </param>
        private static void CalculateRadioPoints(Question question, Answer answer) 
        {
            if (answer.UserAnswer.Length == 0)
                return;
            if (question.CorrectAnswer[0] == answer.UserAnswer[0])
            {
                score += 100;
            }
        }

        /// <summary>
        /// helper method that specifically calculates score from checkBox questions
        /// </summary>
        /// <param name="question"> current question </param>
        /// <param name="answer"> current user's answer </param>
        private static void CalculateCheckBoxPoints(Question question, Answer answer)
        {
            // It doesn't subtract points from the score if the wrong answer is checked
            // For now I have left it as per requirements 
            if (answer.UserAnswer.Length == 0)
                return;
            int correctlyAnswered = 0;
            for (int i = 0; i < answer.UserAnswer.Count(); i++) 
            {
                if (question.CorrectAnswer.Contains(answer.UserAnswer[i])) 
                {
                    correctlyAnswered ++;
                }
            }
            score += (int)Math.Round((decimal)100/question.CorrectAnswer.Count()*correctlyAnswered);
        }

        /// <summary>
        /// helper method that specifically calculates score from textBox questions
        /// </summary>
        /// <param name="question"> current question </param>
        /// <param name="answer"> current user's answer </param>
        private static void CalculateTextBoxPoints(Question question, Answer answer)
        {
            if (answer.UserAnswer.Length == 0)
                return;
            if (question.CorrectAnswer[0].ToLower() == answer.UserAnswer[0].ToLower())
                score += 100;
        }

        /// <summary>
        /// getter method
        /// </summary>
        /// <param name="answers"> user's answers </param>
        /// <returns> calculated user's score </returns>
        public static int GetHighScore(List<Answer> answers) 
        {
            CalculateScore(answers);
            return score;
        }
    }
}