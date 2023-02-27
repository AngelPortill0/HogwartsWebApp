using System.Text.RegularExpressions;

using HogwartsWebApp.DataAccess;

namespace HogwartsWebApp.BusinessLogic
{
    public class Validators
    {
        static public bool isRetrievedData(Student data)
        {
            bool retrieved = false;

            if (data != null)
            {
                retrieved = true;
            }

            return retrieved;
        }

        static public bool isRetrievedData(StudentHistory data)
        {
            bool retrieved = false;

            if (data != null)
            {
                retrieved = true;
            }

            return retrieved;
        }

        static public bool isRetrievedData(Status data)
        {
            bool retrieved = false;

            if (data != null)
            {
                retrieved = true;
            }

            return retrieved;
        }

        static public bool isChangeableName(string name)
        {
            bool ischangeable = false;
            Regex regex = new Regex(@"^[a-zA-Z]{3,20}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (name is not null && regex.Match(name).Success)
            {
                ischangeable = true;
            }

            return ischangeable;
        }

        static public bool isValidAge(int age)
        {
            bool isValid = false;
            Regex regex = new Regex(@"^[0-9]{1,2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (age > 0 && regex.Match(age.ToString()).Success)
            {
                isValid = true;
            }

            return isValid;
        }

        static public bool isValidIdentityNumber(int numberIdentity)
        {
            bool isValid = false;
            Regex regex = new Regex(@"^[0-9]{1,10}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (numberIdentity > 0 && regex.Match(numberIdentity.ToString()).Success)
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
