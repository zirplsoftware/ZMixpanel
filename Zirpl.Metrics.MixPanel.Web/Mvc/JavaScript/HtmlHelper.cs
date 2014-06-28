namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public static class HtmlHelper
    {
        public static InstanceHelper MixPanel(this System.Web.Mvc.HtmlHelper helper)
        {
            return new InstanceHelper();
        }
    }
}
