using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200023D RID: 573
	public class DiagramGrid : StateManager, IDefaultCheck
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00047FF6 File Offset: 0x000461F6
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x0004801F File Offset: 0x0004621F
		[DefaultValue(50.0)]
		public double ComponentSpacingX
		{
			get
			{
				return (double)(base.ViewState["ComponentSpacingX"] ?? 50.0);
			}
			set
			{
				base.ViewState["ComponentSpacingX"] = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00048037 File Offset: 0x00046237
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x00048060 File Offset: 0x00046260
		[DefaultValue(50.0)]
		public double ComponentSpacingY
		{
			get
			{
				return (double)(base.ViewState["ComponentSpacingY"] ?? 50.0);
			}
			set
			{
				base.ViewState["ComponentSpacingY"] = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00048078 File Offset: 0x00046278
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x000480A1 File Offset: 0x000462A1
		[DefaultValue(50.0)]
		public double OffsetX
		{
			get
			{
				return (double)(base.ViewState["OffsetX"] ?? 50.0);
			}
			set
			{
				base.ViewState["OffsetX"] = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x000480B9 File Offset: 0x000462B9
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x000480E2 File Offset: 0x000462E2
		[DefaultValue(50.0)]
		public double OffsetY
		{
			get
			{
				return (double)(base.ViewState["OffsetY"] ?? 50.0);
			}
			set
			{
				base.ViewState["OffsetY"] = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x000480FA File Offset: 0x000462FA
		// (set) Token: 0x060014EA RID: 5354 RVA: 0x00048123 File Offset: 0x00046323
		[DefaultValue(1500.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 1500.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0004813C File Offset: 0x0004633C
		public bool IsDefault
		{
			get
			{
				return this.ComponentSpacingX == 50.0 && this.ComponentSpacingY == 50.0 && this.OffsetX == 50.0 && this.OffsetY == 50.0 && this.Width == 1500.0;
			}
		}
	}
}
