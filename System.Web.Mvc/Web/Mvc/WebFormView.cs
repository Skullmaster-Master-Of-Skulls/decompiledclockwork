using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001F5 RID: 501
	public class WebFormView : BuildManagerCompiledView
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x0002804C File Offset: 0x0002624C
		public WebFormView(ControllerContext controllerContext, string viewPath) : this(controllerContext, viewPath, null, null)
		{
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00028058 File Offset: 0x00026258
		public WebFormView(ControllerContext controllerContext, string viewPath, string masterPath) : this(controllerContext, viewPath, masterPath, null)
		{
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00028064 File Offset: 0x00026264
		public WebFormView(ControllerContext controllerContext, string viewPath, string masterPath, IViewPageActivator viewPageActivator) : base(controllerContext, viewPath, viewPageActivator)
		{
			this.MasterPath = (masterPath ?? string.Empty);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00028080 File Offset: 0x00026280
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00028088 File Offset: 0x00026288
		public string MasterPath { get; private set; }

		// Token: 0x06000F47 RID: 3911 RVA: 0x00028094 File Offset: 0x00026294
		protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
		{
			ViewPage viewPage = instance as ViewPage;
			if (viewPage != null)
			{
				this.RenderViewPage(viewContext, viewPage);
				return;
			}
			ViewUserControl viewUserControl = instance as ViewUserControl;
			if (viewUserControl != null)
			{
				this.RenderViewUserControl(viewContext, viewUserControl);
				return;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.WebFormViewEngine_WrongViewBase, new object[]
			{
				base.ViewPath
			}));
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000280EC File Offset: 0x000262EC
		private void RenderViewPage(ViewContext context, ViewPage page)
		{
			if (!string.IsNullOrEmpty(this.MasterPath))
			{
				page.MasterLocation = this.MasterPath;
			}
			page.ViewData = context.ViewData;
			page.RenderView(context);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002811A File Offset: 0x0002631A
		private void RenderViewUserControl(ViewContext context, ViewUserControl control)
		{
			if (!string.IsNullOrEmpty(this.MasterPath))
			{
				throw new InvalidOperationException(MvcResources.WebFormViewEngine_UserControlCannotHaveMaster);
			}
			control.ViewData = context.ViewData;
			control.RenderView(context);
		}
	}
}
