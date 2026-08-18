using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200065B RID: 1627
	public class ToolBar : StateManager, IDefaultCheck
	{
		// Token: 0x06003BAF RID: 15279 RVA: 0x000C24B8 File Offset: 0x000C06B8
		public ToolBar()
		{
			this.Items = this.defaultItems;
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000C252E File Offset: 0x000C072E
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000C254F File Offset: 0x000C074F
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Items
		{
			get
			{
				return (string[])(base.ViewState["Items"] ?? this.defaultItems);
			}
			set
			{
				base.ViewState["Items"] = value;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000C2562 File Offset: 0x000C0762
		public bool IsDefault
		{
			get
			{
				return this.Items.SequenceEqual(this.defaultItems);
			}
		}

		// Token: 0x04001026 RID: 4134
		internal readonly string[] defaultItems = new string[]
		{
			"pager",
			"spacer",
			"zoom",
			"toggleSelection",
			"spacer",
			"search",
			"open",
			"download",
			"print"
		};
	}
}
