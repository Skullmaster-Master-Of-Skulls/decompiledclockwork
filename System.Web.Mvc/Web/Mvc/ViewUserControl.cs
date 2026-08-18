using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Properties;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000188 RID: 392
	[FileLevelControlBuilder(typeof(ViewUserControlControlBuilder))]
	public class ViewUserControl : UserControl, IViewDataContainer
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001DA2D File Offset: 0x0001BC2D
		public AjaxHelper<object> Ajax
		{
			get
			{
				if (this._ajaxHelper == null)
				{
					this._ajaxHelper = new AjaxHelper<object>(this.ViewContext, this);
				}
				return this._ajaxHelper;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001DA4F File Offset: 0x0001BC4F
		public HtmlHelper<object> Html
		{
			get
			{
				if (this._htmlHelper == null)
				{
					this._htmlHelper = new HtmlHelper<object>(this.ViewContext, this);
				}
				return this._htmlHelper;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001DA71 File Offset: 0x0001BC71
		public object Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0001DA7E File Offset: 0x0001BC7E
		public TempDataDictionary TempData
		{
			get
			{
				return this.ViewPage.TempData;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001DA8B File Offset: 0x0001BC8B
		public UrlHelper Url
		{
			get
			{
				return this.ViewPage.Url;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewData == null)
				{
					this._dynamicViewData = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewData;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001DAD9 File Offset: 0x0001BCD9
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ViewContext ViewContext
		{
			get
			{
				return this._viewContext ?? this.ViewPage.ViewContext;
			}
			set
			{
				this._viewContext = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0001DAF9 File Offset: 0x0001BCF9
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x0001DB07 File Offset: 0x0001BD07
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ViewDataDictionary ViewData
		{
			get
			{
				this.EnsureViewData();
				return this._viewData;
			}
			set
			{
				this.SetViewData(value);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0001DB10 File Offset: 0x0001BD10
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x0001DB21 File Offset: 0x0001BD21
		[DefaultValue("")]
		public string ViewDataKey
		{
			get
			{
				return this._viewDataKey ?? string.Empty;
			}
			set
			{
				this._viewDataKey = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		internal ViewPage ViewPage
		{
			get
			{
				ViewPage viewPage = this.Page as ViewPage;
				if (viewPage == null)
				{
					throw new InvalidOperationException(MvcResources.ViewUserControl_RequiresViewPage);
				}
				return viewPage;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001DB54 File Offset: 0x0001BD54
		public HtmlTextWriter Writer
		{
			get
			{
				return this.ViewPage.Writer;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001DB61 File Offset: 0x0001BD61
		protected virtual void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = viewData;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		protected void EnsureViewData()
		{
			if (this._viewData != null)
			{
				return;
			}
			IViewDataContainer viewDataContainer = ViewUserControl.GetViewDataContainer(this);
			if (viewDataContainer == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ViewUserControl_RequiresViewDataProvider, new object[]
				{
					base.AppRelativeVirtualPath
				}));
			}
			ViewDataDictionary viewDataDictionary = viewDataContainer.ViewData;
			if (!string.IsNullOrEmpty(this.ViewDataKey))
			{
				object obj = viewDataDictionary.Eval(this.ViewDataKey);
				ViewDataDictionary viewDataDictionary2;
				if ((viewDataDictionary2 = (obj as ViewDataDictionary)) == null)
				{
					viewDataDictionary2 = new ViewDataDictionary(viewDataDictionary)
					{
						Model = obj
					};
				}
				viewDataDictionary = viewDataDictionary2;
			}
			this.SetViewData(viewDataDictionary);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		private static IViewDataContainer GetViewDataContainer(Control control)
		{
			while (control != null)
			{
				control = control.Parent;
				IViewDataContainer viewDataContainer = control as IViewDataContainer;
				if (viewDataContainer != null)
				{
					return viewDataContainer;
				}
			}
			return null;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public virtual void RenderView(ViewContext viewContext)
		{
			using (ViewUserControl.ViewUserControlContainerPage viewUserControlContainerPage = new ViewUserControl.ViewUserControlContainerPage(this))
			{
				ViewUserControl.RenderViewAndRestoreContentType(viewUserControlContainerPage, viewContext);
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001DC58 File Offset: 0x0001BE58
		internal static void RenderViewAndRestoreContentType(ViewPage containerPage, ViewContext viewContext)
		{
			string contentType = viewContext.HttpContext.Response.ContentType;
			containerPage.RenderView(viewContext);
			viewContext.HttpContext.Response.ContentType = contentType;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001DC8E File Offset: 0x0001BE8E
		[Obsolete("The TextWriter is now provided by the ViewContext object passed to the RenderView method.", true)]
		public void SetTextWriter(TextWriter textWriter)
		{
		}

		// Token: 0x040002F1 RID: 753
		private AjaxHelper<object> _ajaxHelper;

		// Token: 0x040002F2 RID: 754
		private DynamicViewDataDictionary _dynamicViewData;

		// Token: 0x040002F3 RID: 755
		private HtmlHelper<object> _htmlHelper;

		// Token: 0x040002F4 RID: 756
		private ViewContext _viewContext;

		// Token: 0x040002F5 RID: 757
		private ViewDataDictionary _viewData;

		// Token: 0x040002F6 RID: 758
		private string _viewDataKey;

		// Token: 0x0200018C RID: 396
		private sealed class ViewUserControlContainerPage : ViewPage
		{
			// Token: 0x06000B58 RID: 2904 RVA: 0x0001E20E File Offset: 0x0001C40E
			public ViewUserControlContainerPage(ViewUserControl userControl)
			{
				this._userControl = userControl;
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x0001E21D File Offset: 0x0001C41D
			public override void ProcessRequest(HttpContext context)
			{
				this._userControl.ID = ViewPage.NextId();
				this.Controls.Add(this._userControl);
				base.ProcessRequest(context);
			}

			// Token: 0x04000303 RID: 771
			private readonly ViewUserControl _userControl;
		}
	}
}
