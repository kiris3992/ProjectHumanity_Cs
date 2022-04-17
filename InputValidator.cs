using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHumanity
{
    class InputValidator
    {
        public List<Validator> Items;

        public InputValidator()
        {
            Items = new List<Validator>();
        }
        public void Add(Validator item)
        {
            Items.Add(item);
        }
        public bool IsValid()
        {
            for(int i = 0; i < Items.Count; i++)
            {
                if (!Items[i].isValid) { return false; }
            }
            return true;
        }
        public object[] GetParamValuesToArray() {
            Object[] ar = new object[Items.Count];
            for(int i = 0; i < Items.Count; i++)
            {
                ar[i] = Items[i].Out;
            }
            return ar;
        }

        internal class Validator
        {
            internal bool isValid = false;
            public string Query { get; set; }
            internal string TagName { get; set; }
            internal string Message { get; set; }
            public object Out;

            public Validator(string tagName, string query)
            {
                TagName = tagName;
                Query = query;
            }
            public virtual bool Validate(string userInput)
            {
                userInput = userInput.Trim();
                if (userInput == string.Empty)
                {
                    Message = $"{TagName} must be not empty.";
                    return false;
                }
                return true;
            }
        }


        internal class CapString : Validator
        {
            int MinLength { get; set; }
            int MaxLength { get; set; }
            /// <summary>
            /// Capitalize and Validates the first letter of a given String.
            /// </summary>
            /// <param name="minLength">The Minimum inclusive length of the given String.</param>
            /// <param name="maxLength">The Maximum inclusive lenth of the given String.</param>
            public CapString(string tagName, string query, int minLength, int maxLength) : base(tagName, query)
            {
                MinLength = minLength;
                MaxLength = maxLength;
            }
            public override bool Validate(string userInput)
            {
                if (!base.Validate(userInput)) { return false; }
                else
                {
                    if (userInput.Length < MinLength || userInput.Length > MaxLength)
                    {
                        Message = TagName + $" Must be from {MinLength} to {MaxLength} characters."; return false;
                    }
                    userInput = char.ToUpper(userInput[0]) + userInput.Substring(1);
                    Out = userInput;
                    isValid = true;
                    return true;
                }
            }
        }


        internal class FloatNumber : Validator
        {
            float RangeMin { get; set; }
            float RangeMax { get; set; }
            /// <summary>
            /// Validates if a given String is a Float number, Optional with Range criteria.
            /// </summary>
            /// <param name="rangeMin">(Optinal) The minimum inclusive Range, if not used then no Min Range applied.</param>
            /// <param name="rangeMax">(Optinal) The maximum inclusive Range, if not used then no Max Range applied.</param>
            public FloatNumber(string tagName, string query, float rangeMin = float.MinValue, float rangeMax = float.MinValue) : base(tagName, query)
            {
                RangeMin = rangeMin;
                RangeMax = rangeMax;
            }
            public override bool Validate(string userInput)
            {
                if (!base.Validate(userInput)) { return false; }
                else
                {
                    float floatValue;
                    if (!float.TryParse(userInput, out floatValue))
                    {
                        Message = TagName + " must be a Real number."; return false;
                    }
                    if (RangeMin != float.MinValue && floatValue < RangeMin)
                    {
                        Message = TagName + $" must not be smaller than {RangeMin}."; return false;
                    }
                    if (RangeMax != float.MinValue && floatValue > RangeMax)
                    {
                        Message = TagName + $" must not be larger than {RangeMax}."; return false;
                    }
                    Out = floatValue;
                    isValid = true;
                    return true;
                }
            }
        }

        internal class IntNumber : Validator
        {
            int RangeMin { get; set; }
            int RangeMax { get; set; }
            /// <summary>
            /// Validates if a given String is an Integer number, Optional with Range criteria.
            /// </summary>
            /// <param name="rangeMin">(Optinal) The minimum inclusive Range, if not used then no Min Range applied.</param>
            /// <param name="rangeMax">(Optinal) The maximum inclusive Range, if not used then no Max Range applied.</param>
            public IntNumber(string tagName, string query, int rangeMin = int.MinValue, int rangeMax = int.MinValue) : base(tagName, query)
            {
                RangeMin = rangeMin;
                RangeMax = rangeMax;
            }
            public override bool Validate(string userInput)
            {
                if (!base.Validate(userInput)) { return false; }
                else
                {
                    int intValue;
                    if (!int.TryParse(userInput, out intValue))
                    {
                        Message = TagName + " must be an Integer."; return false;
                    }
                    if (RangeMin != int.MinValue && intValue < RangeMin)
                    {
                        Message = TagName + $" must not be smaller than {RangeMin}."; return false;
                    }
                    if (RangeMax != int.MinValue && intValue > RangeMax)
                    {
                        Message = TagName + $" must not be larger than {RangeMax}."; return false;
                    }
                    Out = intValue;
                    isValid = true;
                    return true;
                }
            }
        }

        internal class FucNumber : Validator
        {
            int MinLength { get; set; }
            int MaxLength { get; set; }
            /// <summary>
            /// Validates if a given String is a Faculty Number.
            /// </summary>
            /// <param name="minLength">The Minimum inclusive length of the Faculty Number</param>
            /// <param name="maxLength">The Maximum inclusive length of the Faculty Number</param>
            public FucNumber(string tagName, string query, int minLength, int maxLength) : base(tagName, query)
            {
                MinLength = minLength;
                MaxLength = maxLength;
            }
            public override bool Validate(string userInput)
            {
                if (!base.Validate(userInput)) { return false; }
                else
                {
                    if (userInput.Length < MinLength || userInput.Length > MaxLength)
                    {
                        Message = TagName + $" must be from {MinLength} to {MaxLength} characters."; return false;
                    }
                    if (!(userInput.All(char.IsLetterOrDigit) && userInput.Any(char.IsDigit) && userInput.Any(char.IsLetter) && !userInput.Any(char.IsWhiteSpace)))
                    {
                        Message = TagName + " must be an alphanumeric string."; return false;
                    }
                    Out = userInput;
                    isValid = true;
                    return true;
                }
            }
        }
    }
}
