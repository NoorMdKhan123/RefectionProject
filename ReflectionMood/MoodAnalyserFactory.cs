using System;
using System.Reflection;
using System.Text.RegularExpressions;
namespace ReflectionMood
{
  public class MoodAnalyserFactory
    {
        //UC5 for parameterized constructor by passing message paramter to class method
        public static object CreateMoodAnalyseUsingPrametrizedConstructor(string className, string constructorName, string message)

        {
            Type type = typeof(MoodAnalyser);
            if (type.Name.Equals(className) || type.FullName.Equals(className))
            {
                if (type.Name.Equals(constructorName))
                {
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string)});
                    object instance = ctor.Invoke(new object[] { "HAPPY"});
                    return instance;
                }
                else
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
