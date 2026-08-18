using System;

namespace System.Web.Mvc
{
	// Token: 0x020000C0 RID: 192
	public class RazorViewEngine : BuildManagerViewEngine
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0000E1B7 File Offset: 0x0000C3B7
		public RazorViewEngine() : this(null)
		{
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public RazorViewEngine(IViewPageActivator viewPageActivator) : base(viewPageActivator)
		{
			base.AreaViewLocationFormats = new string[]
			{
				"~/Areas/{2}/Views/{1}/{0}.cshtml",
				"~/Areas/{2}/Views/{1}/{0}.vbhtml",
				"~/Areas/{2}/Views/Shared/{0}.cshtml",
				"~/Areas/{2}/Views/Shared/{0}.vbhtml"
			};
			base.AreaMasterLocationFormats = new string[]
			{
				"~/Areas/{2}/Views/{1}/{0}.cshtml",
				"~/Areas/{2}/Views/{1}/{0}.vbhtml",
				"~/Areas/{2}/Views/Shared/{0}.cshtml",
				"~/Areas/{2}/Views/Shared/{0}.vbhtml"
			};
			base.AreaPartialViewLocationFormats = new string[]
			{
				"~/Areas/{2}/Views/{1}/{0}.cshtml",
				"~/Areas/{2}/Views/{1}/{0}.vbhtml",
				"~/Areas/{2}/Views/Shared/{0}.cshtml",
				"~/Areas/{2}/Views/Shared/{0}.vbhtml"
			};
			base.ViewLocationFormats = new string[]
			{
				"~/Views/{1}/{0}.cshtml",
				"~/Views/{1}/{0}.vbhtml",
				"~/Views/Shared/{0}.cshtml",
				"~/Views/Shared/{0}.vbhtml"
			};
			base.MasterLocationFormats = new string[]
			{
				"~/Views/{1}/{0}.cshtml",
				"~/Views/{1}/{0}.vbhtml",
				"~/Views/Shared/{0}.cshtml",
				"~/Views/Shared/{0}.vbhtml"
			};
			base.PartialViewLocationFormats = new string[]
			{
				"~/Views/{1}/{0}.cshtml",
				"~/Views/{1}/{0}.vbhtml",
				"~/Views/Shared/{0}.cshtml",
				"~/Views/Shared/{0}.vbhtml"
			};
			base.FileExtensions = new string[]
			{
				"cshtml",
				"vbhtml"
			};
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000E318 File Offset: 0x0000C518
		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			return new RazorView(controllerContext, partialPath, null, false, base.FileExtensions, base.ViewPageActivator)
			{
				DisplayModeProvider = base.DisplayModeProvider
			};
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000E348 File Offset: 0x0000C548
		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			return new RazorView(controllerContext, viewPath, masterPath, true, base.FileExtensions, base.ViewPageActivator)
			{
				DisplayModeProvider = base.DisplayModeProvider
			};
		}

		// Token: 0x0400015E RID: 350
		internal static readonly string ViewStartFileName = "_ViewStart";
	}
}
