using System.ComponentModel.DataAnnotations;

namespace Trivia.Models
{
    public class User
    {
        /// <summary>
        /// properties of User object
        /// </summary>
        [Key]
        public int Id { get; set; }         // user's id
        public string Email { get; set; }   // user's email
        public int Score { get; set; }      // user's score
        public DateTime Date { get; set; }  // date of finished quiz

        /// <summary>
        /// parameterless constructor
        /// </summary>
        public User() {   }
    }
}
