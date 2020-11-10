using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayTech.Shared.Utils
{
    public class Check
    {
        public static T ArgumentNotNull<T>(T checkValue, string propertyName, string location)
        {
            if (checkValue == null)
            {
                throw new ArgumentNullException($"{location} {propertyName} argument is null");
            }

            return checkValue;
        }

        public static string ArgumentNotNullAndNotEmpty(string checkValue, string propertyName, string location)
        {
            if (checkValue == null)
            {
                throw new ArgumentNullException($"{location} {propertyName} argument is null");
            }
            else if (checkValue.Trim().Length == 0)
            {
                throw new ArgumentException($"{location} {propertyName} argument is empty");
            }

            return checkValue;
        }

        public static IEnumerable<T> ArgumentNotNullAndNotEmpty<T>(IEnumerable<T> checkValue, string propertyName, string location)
        {
            if (checkValue == null)
            {
                throw new ArgumentNullException($"{location} {propertyName} argument is null");
            }
            else if (!checkValue.Any())
            {
                throw new ArgumentException($"{location} {propertyName} argument is empty");
            }

            return checkValue;
        }

        public static T NotNull<T>(T checkValue, string propertyName, string location)
        {
            if (checkValue == null)
            {
                throw new NullReferenceException($"{location} {propertyName}");
            }

            return checkValue;
        }

        public static T NotNull<T>(T checkValue, string propertyName, string location, string message)
        {
            if (checkValue == null)
            {
                throw new NullReferenceException($"{location}{propertyName}. Message: {message}");
            }

            return checkValue;
        }
    }
}
