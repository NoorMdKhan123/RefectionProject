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
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string) });
                    object instance = ctor.Invoke(new object[] { "HAPPY" });
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
        public static string InvokeAnalyseMood(string message, string methodName)
        {
            try
            {
                Type type = Type.GetType("ReflectionMood.MoodAnalyser");
                object moodAnalyseObject = MoodAnalyserFactory.CreateMoodAnalyseUsingPrametrizedConstructor("ReflectionMood.MoodAnalyser",
                    "MoodAnalyser", message);
                MethodInfo analyseMethodInfo = type.GetMethod(methodName);
                object mood = analyseMethodInfo.Invoke(moodAnalyseObject, null);
                return mood.ToString();
            }
            catch
            {
                throw new MoodAnalyserCustomException(MoodAnalyserCustomException.ExceptionType.NO_SUCH_METHOD, "Method is not found");
            }
        }
        // setting Mood Dynamically with a function
        public static string SetField(string message, string fieldName)
        {
            try
            {
                MoodAnalyser moodAnalyse = new MoodAnalyser();
                Type type = typeof(MoodAnalyser);
                FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Public |
                   BindingFlags.Instance);
                if (message == null)
                {
                    throw new MoodAnalyserCustomException(MoodAnalyserCustomException.ExceptionType
                        .NO_SUCH_FIELD, "message should not be null");

                }
                fieldInfo.SetValue(moodAnalyse, message);
                return moodAnalyse.AnalyseMood();
            }
            catch (NullReferenceException)
            {
                throw new MoodAnalyserCustomException(MoodAnalyserCustomException.ExceptionType.NO_SUCH_FIELD,
                    "Field is not found");
            }

        }
    }
}
