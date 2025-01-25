using System.ComponentModel.DataAnnotations;

namespace Trivia.Models
{
    public class Question
    {
        /// <summary>
        /// properties of Question object
        /// </summary>
        [Key]
        public int Id { get; set; }                     // question id
        public string Text { get; set; }                // question's text
        public QuestionType Type { get; set; }          // the type of question
        public string[] Options { get; set; }           // all options from which user can pick an answer
        public string[] CorrectAnswer { get; set; }     // correct answer or answers to the question
    }
}