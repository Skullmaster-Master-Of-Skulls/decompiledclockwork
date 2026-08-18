using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A2 RID: 1186
	public sealed class PolygonHotSpot : HotSpot
	{
		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000C13D4 File Offset: 0x000BF5D4
		// (set) Token: 0x06003B7C RID: 15228 RVA: 0x000C1401 File Offset: 0x000BF601
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[WebSysDescription("PolygonHotSpot_Coordinates")]
		public string Coordinates
		{
			get
			{
				string text = base.ViewState["Coordinates"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.ViewState["Coordinates"] = value;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000C1414 File Offset: 0x000BF614
		protected internal override string MarkupName
		{
			get
			{
				return "poly";
			}
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000C141B File Offset: 0x000BF61B
		public override string GetCoordinates()
		{
			return this.Coordinates;
		}
	}
}
