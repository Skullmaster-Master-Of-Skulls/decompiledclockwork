using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200025E RID: 606
	public class Margin : StateManager, IDefaultCheck
	{
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0004AAFA File Offset: 0x00048CFA
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x0004AB23 File Offset: 0x00048D23
		[DefaultValue(0.0)]
		public double Bottom
		{
			get
			{
				return (double)(base.ViewState["Bottom"] ?? 0.0);
			}
			set
			{
				base.ViewState["Bottom"] = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0004AB3B File Offset: 0x00048D3B
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x0004AB64 File Offset: 0x00048D64
		[DefaultValue(0.0)]
		public double Left
		{
			get
			{
				return (double)(base.ViewState["Left"] ?? 0.0);
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0004AB7C File Offset: 0x00048D7C
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x0004ABA5 File Offset: 0x00048DA5
		[DefaultValue(0.0)]
		public double Right
		{
			get
			{
				return (double)(base.ViewState["Right"] ?? 0.0);
			}
			set
			{
				base.ViewState["Right"] = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0004ABBD File Offset: 0x00048DBD
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x0004ABE6 File Offset: 0x00048DE6
		[DefaultValue(0.0)]
		public double Top
		{
			get
			{
				return (double)(base.ViewState["Top"] ?? 0.0);
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0004AC00 File Offset: 0x00048E00
		public bool IsDefault
		{
			get
			{
				return this.Bottom == 0.0 && this.Left == 0.0 && this.Right == 0.0 && this.Top == 0.0;
			}
		}
	}
}
