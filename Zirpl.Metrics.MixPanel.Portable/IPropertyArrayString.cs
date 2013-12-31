using System;
using Newtonsoft.Json;

namespace Zirpl.Metrics.MixPanel
{
    public interface IPropertyArrayString
    {
        String ToPropertyArrayJson();
        String ToPropertyArrayJson(Formatting formatting);
    }
}
