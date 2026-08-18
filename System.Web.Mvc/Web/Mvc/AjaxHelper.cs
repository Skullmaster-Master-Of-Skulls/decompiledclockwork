using System;
using System.Runtime.CompilerServices;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x0200017D RID: 381
	public class AjaxHelper
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001C0C9 File Offset: 0x0001A2C9
		public AjaxHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : this(viewContext, viewDataContainer, RouteTable.Routes)
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		public AjaxHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
		{
			if (viewContext == null)
			{
				throw new ArgumentNullException("viewContext");
			}
			if (viewDataContainer == null)
			{
				throw new ArgumentNullException("viewDataContainer");
			}
			if (routeCollection == null)
			{
				throw new ArgumentNullException("routeCollection");
			}
			this.ViewContext = viewContext;
			this.ViewDataContainer = viewDataContainer;
			this.RouteCollection = routeCollection;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0001C12A File Offset: 0x0001A32A
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0001C147 File Offset: 0x0001A347
		public static string GlobalizationScriptPath
		{
			get
			{
				if (string.IsNullOrEmpty(AjaxHelper._globalizationScriptPath))
				{
					AjaxHelper._globalizationScriptPath = "~/Scripts/Globalization";
				}
				return AjaxHelper._globalizationScriptPath;
			}
			set
			{
				AjaxHelper._globalizationScriptPath = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001C14F File Offset: 0x0001A34F
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0001C157 File Offset: 0x0001A357
		public RouteCollection RouteCollection { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001C168 File Offset: 0x0001A368
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewDataDictionary == null)
				{
					this._dynamicViewDataDictionary = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewDataDictionary;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001C1A1 File Offset: 0x0001A3A1
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0001C1A9 File Offset: 0x0001A3A9
		public ViewContext ViewContext { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0001C1B2 File Offset: 0x0001A3B2
		public ViewDataDictionary ViewData
		{
			get
			{
				return this.ViewDataContainer.ViewData;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001C1BF File Offset: 0x0001A3BF
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0001C1C7 File Offset: 0x0001A3C7
		public IViewDataContainer ViewDataContainer { get; internal set; }

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		public string JavaScriptStringEncode(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return message;
			}
			return HttpUtility.JavaScriptStringEncode(message);
		}

		// Token: 0x040002BD RID: 701
		private static string _globalizationScriptPath;

		// Token: 0x040002BE RID: 702
		private DynamicViewDataDictionary _dynamicViewDataDictionary;
	}
}
