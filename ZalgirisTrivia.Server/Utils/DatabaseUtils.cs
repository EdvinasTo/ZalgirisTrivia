using Microsoft.EntityFrameworkCore;
using Trivia.Data;
using Trivia.Models;
using ZalgirisTrivia.Server.Models;

namespace ZalgirisTrivia.Server.Utils
{
    /// <summary>
    /// class designed to send data or retrieve it from database
    /// </summary>
    public class DatabaseUtils
    {
        private readonly TriviaDbContext _context;  // instance to the database

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"> instance to the database </param>
        public DatabaseUtils(TriviaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// getter method
        /// </summary>
        /// <returns> a path to questions JSON folder </returns>
        public static string GetPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "data", "questions.json");
        }

        /// <summary>
        /// getter method
        /// </summary>
        /// <returns> a list of question objects </returns>
        /// <exception cref="FileNotFoundException"> exception if file is not found </exception>
        private List<Question> GetQuestionsFromFile()
        {
            string filePath = GetPath();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Questions file not found.");
            }

            return InOut.GetQuestions(filePath);
        }

        /// <summary>
        /// adds questions to the database
        /// </summary>
        /// <returns> saves questions in the database </returns>
        public async Task AddQuestionsToDb()
        {
            var questionsFromFile = GetQuestionsFromFile();

            var existingQuestions = await _context.Questions.ToListAsync();
            _context.Questions.RemoveRange(existingQuestions);

            await _context.Questions.AddRangeAsync(questionsFromFile);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// adds submission object in database
        /// </summary>
        /// <param name="submission"> user's submission </param>
        /// <returns> saves submission in the database </returns>
        public async Task AddSubmissionsToDb(Submission submission)
        {
            if (submission.Answers != null && submission.Answers.Count > 0)
            {
                foreach (var answer in submission.Answers)
                {
                    answer.Id = 0; 
                }
            }

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// adds user in the database
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <param name="score"> user's score </param>
        /// <returns> saves data about user in database </returns>
        public async Task AddsUsersToDb(string email, int score)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                if (user.Score < score)
                {
                    user.Score = score;
                    user.Date = DateTime.Now;
                }
            }
            else {
                _context.Users.Add(new User {
                    Id = _context.Users.Count() + 1,
                    Email = email,
                    Score = score,
                    Date = DateTime.Now });
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// getter method
        /// </summary>
        /// <returns> a list of question objects </returns>
        public async Task<List<Question>> GetQuestions() 
        {
            return await _context.Questions.ToListAsync();
        }

        /// <summary>
        /// getter method, used in forming the leaderboard
        /// </summary>
        /// <returns> a sorted list of 10 heighest ranking users </returns>
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users
                .OrderByDescending(s => s.Score)
                .ThenBy(d => d.Date)
                .Take(10)
                .ToListAsync();
        }
    }
}