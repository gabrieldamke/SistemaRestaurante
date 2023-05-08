using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Helpers;

public static class EnumHelper
{
    public static IEnumerable<SelectListItem> GetSelectList(Type enumType)
    {
        var values = Enum.GetValues(enumType)
            .Cast<Enum>()
            .Select(
                e =>
                    new SelectListItem
                    {
                        Text = e.GetDisplayName(),
                        Value = Convert.ToInt32(e).ToString()
                    }
            )
            .ToList();

        return values;
    }
    
    private static string GetDisplayName(this Enum enumValue)
    {
        var displayName = enumValue.GetType().GetMember(enumValue.ToString())
            .FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>(false)?.Value;

        return displayName ?? enumValue.ToString();
    }
}