using Microsoft.AspNetCore.Mvc;
using ZalgirisTrivia.Server.Models;
using ZalgirisTrivia.Server.Utils;

[ApiController]
[Route("api/quiz")]
public class QuestionsController : ControllerBase
{
    private readonly DatabaseUtils utils;   // instance to the database utils

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="utils"> instance of database utils </param>
    public QuestionsController(DatabaseUtils utils)
    {
        this.utils = utils;
    }

    /// <summary>
    /// adds questions to the database and sends them to the frontend
    /// </summary>
    /// <returns> questions and a response </returns>
    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions()
    {
        await utils.AddQuestionsToDb();
        var questions = utils.GetQuestions();
        return Ok(questions);
    }

    /// <summary>
    /// gets the submission's data from the frontend
    /// calculates the score
    /// adds submission's and user's data to the database
    /// </summary>
    /// <param name="submission"> user's submission </param>
    /// <returns> score and a response </returns>
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitQuiz([FromBody] Submission submission)
    {
        if (submission == null)
        {
            return BadRequest("Invalid answers submitted.");
        }

        int score = Calculations.GetHighScore(submission.Answers);

        await utils.AddSubmissionsToDb(submission);
        await utils.AddsUsersToDb(submission.Email, score);

        return Ok(score);
    }

    /// <summary>
    /// sends the data about top ranking users to the frontend
    /// </summary>
    /// <returns> top ranking users and a response </returns>
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = utils.GetUsers();
        return Ok(users);
    }
}