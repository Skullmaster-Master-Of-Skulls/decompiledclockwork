using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004AF RID: 1199
	public sealed class RectangleHotSpot : HotSpot
	{
		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000C28F0 File Offset: 0x000C0AF0
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000C2919 File Offset: 0x000C0B19
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Bottom")]
		public int Bottom
		{
			get
			{
				object obj = base.ViewState["Bottom"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Bottom"] = value;
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000C2934 File Offset: 0x000C0B34
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x000C295D File Offset: 0x000C0B5D
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Left")]
		public int Left
		{
			get
			{
				object obj = base.ViewState["Left"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000C2978 File Offset: 0x000C0B78
		// (set) Token: 0x06003BEF RID: 15343 RVA: 0x000C29A1 File Offset: 0x000C0BA1
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Right")]
		public int Right
		{
			get
			{
				object obj = base.ViewState["Right"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Right"] = value;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000C29BC File Offset: 0x000C0BBC
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000C29E5 File Offset: 0x000C0BE5
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("RectangleHotSpot_Top")]
		public int Top
		{
			get
			{
				object obj = base.ViewState["Top"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000C29FD File Offset: 0x000C0BFD
		protected internal override string MarkupName
		{
			get
			{
				return "rect";
			}
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000C2A04 File Offset: 0x000C0C04
		public override string GetCoordinates()
		{
			return string.Concat(new string[]
			{
				this.Left.ToString(),
				",",
				this.Top.ToString(),
				",",
				this.Right.ToString(),
				",",
				this.Bottom.ToString()
			});
		}
	}
}
