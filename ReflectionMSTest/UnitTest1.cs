using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReflectionMood;

namespace ReflectionMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
       //UC 4.1
        public void MoodAnalyserProblem_ReturnMoodAnalyseObject()
        {
            string message = null;
            object expected = new MoodAnalyser(message);
            object obj = MoodAnalyserFactory.CreateMoodAnalyse
                ("ReflectionMood.MoodAnalyser", "MoodAnalyser");
            expected.Equals(obj);
        }
    }
}
