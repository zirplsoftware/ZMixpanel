namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public static class HtmlHelper
    {
        public static InstanceHelper MixPanel(this System.Web.Mvc.HtmlHelper helper)
        {
            return new InstanceHelper();
        }
    }
}
