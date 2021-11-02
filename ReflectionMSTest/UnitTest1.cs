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

        //UC 5.1 Given Mood Analyser class name should return mood analyser object
        public void MoodAnalyserClassNameReturn_MoodAnalyserObj_WithParametrizedConstructor()
        {
            object expected = new MoodAnalyser("HAPPY");
            object obj =
               MoodAnalyserFactory.CreateMoodAnalyseUsingPrametrizedConstructor("ReflectionMood.MoodAnalyser",
                "MoodAnalyser", "HAPPY");
            expected.Equals(obj);
        }
        //UC 5.2
        [TestMethod]
        public void PassingWrongClassName_ToPassTheTestCase()
        {
            string message = null;
            object expected = new MoodAnalyser(message);
            object obj = MoodAnalyserFactory.CreateMoodAnalyse
                ("ReflectionMood.Mood", "MoodAnalyser");
            expected.Equals(obj);
        }

        //5.3
        public void PassingIncorretcParamterToPassThisTestCase()
        {
            object expected = new MoodAnalyser("HAPPY");
            object obj =
               MoodAnalyserFactory.CreateMoodAnalyseUsingPrametrizedConstructor("ReflectionMood.MoodAnalyser",
                "MoodAnalyser", "SAD");
            expected.Equals(obj);
        }
    }
}
