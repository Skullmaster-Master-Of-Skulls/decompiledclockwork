using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000155 RID: 341
	[ToolboxBitmap(typeof(Accessor), "PagingBulletedList.bmp")]
	[TargetControlType(typeof(BulletedList))]
	[ClientScriptResource("Sys.Extended.UI.PagingBulletedListBehavior", "PagingBulletedList")]
	[Designer(typeof(PagingBulletedListExtenderDesigner))]
	public class PagingBulletedListExtender : ExtenderControlBase
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x00017A93 File Offset: 0x00015C93
		public PagingBulletedListExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00017AA2 File Offset: 0x00015CA2
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00017AB0 File Offset: 0x00015CB0
		[DefaultValue(1)]
		[ClientPropertyName("indexSize")]
		[ExtenderControlProperty]
		public int IndexSize
		{
			get
			{
				return base.GetPropertyValue<int>("IndexSize", 1);
			}
			set
			{
				base.SetPropertyValue<int>("IndexSize", value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x00017AC0 File Offset: 0x00015CC0
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x00017AE1 File Offset: 0x00015CE1
		[ClientPropertyName("height")]
		[ExtenderControlProperty]
		public int? Height
		{
			get
			{
				return base.GetPropertyValue<int?>("Height", null);
			}
			set
			{
				base.SetPropertyValue<int?>("Height", value);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00017AEF File Offset: 0x00015CEF
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x00017B01 File Offset: 0x00015D01
		[ClientPropertyName("separator")]
		[ExtenderControlProperty]
		[DefaultValue(" - ")]
		public string Separator
		{
			get
			{
				return base.GetPropertyValue<string>("Separator", " - ");
			}
			set
			{
				base.SetPropertyValue<string>("Separator", value);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00017B10 File Offset: 0x00015D10
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x00017B31 File Offset: 0x00015D31
		[ClientPropertyName("maxItemPerPage")]
		[ExtenderControlProperty]
		public int? MaxItemPerPage
		{
			get
			{
				return base.GetPropertyValue<int?>("MaxItemPerPage", null);
			}
			set
			{
				base.SetPropertyValue<int?>("MaxItemPerPage", value);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00017B3F File Offset: 0x00015D3F
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x00017B4D File Offset: 0x00015D4D
		[ExtenderControlProperty]
		[ClientPropertyName("clientSort")]
		[DefaultValue(false)]
		public bool ClientSort
		{
			get
			{
				return base.GetPropertyValue<bool>("ClientSort", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ClientSort", value);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00017B5B File Offset: 0x00015D5B
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x00017B6D File Offset: 0x00015D6D
		[ExtenderControlProperty]
		[ClientPropertyName("selectIndexCssClass")]
		public string SelectIndexCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("SelectIndexCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("SelectIndexCssClass", value);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00017B7B File Offset: 0x00015D7B
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00017B8D File Offset: 0x00015D8D
		[ExtenderControlProperty]
		[ClientPropertyName("unselectIndexCssClass")]
		public string UnselectIndexCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("UnselectIndexCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("UnselectIndexCssClass", value);
			}
		}
	}
}
