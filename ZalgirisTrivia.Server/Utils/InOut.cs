using Newtonsoft.Json;
using Trivia.Models;

namespace ZalgirisTrivia.Server.Utils
{
    public class InOut
    {
        private static List<Question> questions;    // trivia's questions

        /// <summary>
        /// reads the questions from JSON file
        /// </summary>
        /// <param name="filePath"> the path to JSON file </param>
        private static void ReadQuestions(string filePath) 
        {
            string jsonContent = System.IO.File.ReadAllText(filePath);

            questions = JsonConvert.DeserializeObject<List<Question>>(jsonContent);
        }

        /// <summary>
        /// getter method
        /// </summary>
        /// <param name="filePath"> the path to JSON file </param>
        /// <returns> a list of Question objects </returns>
        public static List<Question> GetQuestions(string filePath)
        {
            ReadQuestions(filePath);
            return questions;
        }
    }
}
