using System.Web;
using System.Web.Optimization;

namespace WebsiteTraSua
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/frameworks/tailwindcss").Include(
                        "~/Content/Frameworks/TailwindCss/tailwindcss-v3.4.14.js"));
        }
    }
}
