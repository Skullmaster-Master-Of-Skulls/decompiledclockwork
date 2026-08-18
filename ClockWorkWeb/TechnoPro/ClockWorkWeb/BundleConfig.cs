using System;
using System.Web.Optimization;

namespace TechnoPro.ClockWorkWeb
{
	// Token: 0x02000014 RID: 20
	public class BundleConfig
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003C24 File Offset: 0x00001E24
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", Array.Empty<IItemTransform>()));
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*", Array.Empty<IItemTransform>()));
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Content/bootstrap.js", Array.Empty<IItemTransform>()));
			bundles.Add(new StyleBundle("~/Content/css").Include(new string[]
			{
				"~/Content/bootstrap.css",
				"~/Content/site.css"
			}));
		}
	}
}
