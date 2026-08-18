using System;

namespace System.Web.Mvc
{
	// Token: 0x020001F6 RID: 502
	public class WebFormViewEngine : BuildManagerViewEngine
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x00028147 File Offset: 0x00026347
		public WebFormViewEngine() : this(null)
		{
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00028150 File Offset: 0x00026350
		public WebFormViewEngine(IViewPageActivator viewPageActivator) : base(viewPageActivator)
		{
			base.MasterLocationFormats = new string[]
			{
				"~/Views/{1}/{0}.master",
				"~/Views/Shared/{0}.master"
			};
			base.AreaMasterLocationFormats = new string[]
			{
				"~/Areas/{2}/Views/{1}/{0}.master",
				"~/Areas/{2}/Views/Shared/{0}.master"
			};
			base.ViewLocationFormats = new string[]
			{
				"~/Views/{1}/{0}.aspx",
				"~/Views/{1}/{0}.ascx",
				"~/Views/Shared/{0}.aspx",
				"~/Views/Shared/{0}.ascx"
			};
			base.AreaViewLocationFormats = new string[]
			{
				"~/Areas/{2}/Views/{1}/{0}.aspx",
				"~/Areas/{2}/Views/{1}/{0}.ascx",
				"~/Areas/{2}/Views/Shared/{0}.aspx",
				"~/Areas/{2}/Views/Shared/{0}.ascx"
			};
			base.PartialViewLocationFormats = base.ViewLocationFormats;
			base.AreaPartialViewLocationFormats = base.AreaViewLocationFormats;
			base.FileExtensions = new string[]
			{
				"aspx",
				"ascx",
				"master"
			};
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002823F File Offset: 0x0002643F
		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			return new WebFormView(controllerContext, partialPath, null, base.ViewPageActivator);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002824F File Offset: 0x0002644F
		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			return new WebFormView(controllerContext, viewPath, masterPath, base.ViewPageActivator);
		}
	}
}
