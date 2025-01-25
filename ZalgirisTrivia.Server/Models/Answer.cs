namespace Trivia.Models
{
    public class Answer
    {
        /// <summary>
        /// properties of Answer object
        /// </summary>
        public int Id { get; set; }                 // answer id
        public string[] UserAnswer { get; set; }    // user answers
    }
}
