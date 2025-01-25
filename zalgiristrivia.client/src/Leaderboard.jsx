import { useEffect, useState } from 'react';
import './App.css';

function LeaderboardPage() {

    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetchQuestions();
    }, []);

    // method that fetches data about top ranking users from the backend 
    async function fetchQuestions()
    {
        try {
            const response = await fetch('http://localhost:5244/api/quiz/users');
            if (response.ok) {
                const data = await response.json();
                setUsers(data.result);
            }
            else {
                console.error('Failed to fetch users');
            }
        }
        catch (error) {
            console.error('error fetching users:', error);
        }
    }

    // method that formats the date given by the database
    // returns formatted date
    const formatDate = (date) => {
        const d = new Date(date);

        const year = d.getFullYear();
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const day = String(d.getDate()).padStart(2, '0');
        const hour = String(d.getHours()).padStart(2, '0');
        const minute = String(d.getMinutes()).padStart(2, '0');
        const second = String(d.getSeconds()).padStart(2, '0');

        return `${year}-${month}-${day}  ${hour}:${minute}:${second}`;
    };

    // method that picks the correct color a table row should be colored in
    // returns a background color
    const rowColor = (index) => {
        switch (index) {
            case 1:
                return { backgroundColor: 'gold' }
            case 2:
                return { backgroundColor: 'silver' }
            case 3:
                return { backgroundColor: '#cd7f32' }
            default:
                return { backgroundColor: '#eee'}
        }
    }

    // generates table rows of top ranking users
    // returns table rows
    const generation = () => {
        const leaderboardData = [];
            for (let i = 1; i <= users.length; i++) {
                leaderboardData.push(
                    <tr key={i}
                        style={rowColor(i)}
                    >
                        <td>{i}</td>
                        <td>{users[i - 1].email}</td>
                        <td>{users[i - 1].score}</td>
                        <td>{formatDate(users[i - 1].date)}</td>
                    </tr>
                );
            }

        return leaderboardData;
    };

    // if there are registered users it generates table
    // else it generates a paragraph saying that there are no registered users
    const generateTable = () => {
        if (users.length !== 0) {
            return (
                <table>
                    <thead>
                        <tr>
                            <th>Rank</th>
                            <th>Email</th>
                            <th>Score</th>
                            <th>Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {generation()}
                    </tbody>
                </table>);
        }
        else
            return <p> There are no registered users </p>
    }

    // returns the structure of the leaderboard page
    return  <div>
                <h1>Leaderboard</h1>
                <p>Welcome to the top ranking users leaderboard page.</p>
                { generateTable() }
            </div>
}

export default LeaderboardPage;