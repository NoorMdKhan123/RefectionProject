using System;
using System.Reflection;
using System.Text.RegularExpressions;



namespace ReflectionMood
{
  public class MoodAnalyserFactory
    {
        //UC4 CreateMA Method to create object of MA class
        public static object CreateMoodAnalyse
            (string className, string constructorName)
        {
            string pattern = @"." + constructorName + "$";
            Match result = Regex.Match(className, pattern);

            if (result.Success)
            {
                try
                {
                    Assembly executing = Assembly.GetExecutingAssembly();
                    Type moodAnalyseType = executing.GetType(className);
                    return Activator.CreateInstance(moodAnalyseType);

                }
                catch (ArgumentNullException)
                {
                    throw new MoodAnalyserCustomException(MoodAnalyserCustomException.
                        ExceptionType.NO_SUCH_CLASS, "Class not found");
                }
            }
            else
            {
                throw new MoodAnalyserCustomException(MoodAnalyserCustomException.
                    ExceptionType.NO_SUCH_METHOD, "Cosntructor is not found");
            }
        }
    }
}
