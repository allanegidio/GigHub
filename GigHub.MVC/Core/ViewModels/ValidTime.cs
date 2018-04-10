using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.MVC.Core.ViewModels
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object time)
        {
            var isValid = DateTime.TryParseExact(Convert.ToString(time),
                                               "HH:mm",
                                               CultureInfo.CurrentCulture,
                                               DateTimeStyles.None,
                                               out var dateTime);

            return (isValid);
        }
    }
}