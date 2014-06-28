using System;
using Newtonsoft.Json;

namespace Zirpl.Mixpanel.HttpApi
{
    public interface IPropertyArrayString
    {
        String ToPropertyArrayJson();
        String ToPropertyArrayJson(Formatting formatting);
    }
}
