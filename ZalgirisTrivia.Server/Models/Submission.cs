using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trivia.Models;

namespace ZalgirisTrivia.Server.Models
{
    public class Submission
    {
        /// <summary>
        /// properties of Submission object
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }                                         // submission id
        public string Email { get; set; }                                   // user's email
        public List<Answer> Answers { get; set; } = new List<Answer>();     // user's answers
    }
}
