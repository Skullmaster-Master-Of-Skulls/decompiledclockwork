using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram.DataBinding
{
	// Token: 0x0200022B RID: 555
	[ParseChildren(ChildrenAsProperties = true)]
	public class ShapeSettings : BaseBindingSettings
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00046A7C File Offset: 0x00044C7C
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x00046A9C File Offset: 0x00044C9C
		public string DataXField
		{
			get
			{
				return (string)(base.ViewState["DataXField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataXField"] = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00046AAF File Offset: 0x00044CAF
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x00046ACF File Offset: 0x00044CCF
		public string DataYField
		{
			get
			{
				return (string)(base.ViewState["DataYField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataYField"] = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00046AE2 File Offset: 0x00044CE2
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x00046B02 File Offset: 0x00044D02
		public string DataWidthField
		{
			get
			{
				return (string)(base.ViewState["DataWidthField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataWidthField"] = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00046B15 File Offset: 0x00044D15
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00046B35 File Offset: 0x00044D35
		public string DataHeightField
		{
			get
			{
				return (string)(base.ViewState["DataHeightField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataHeightField"] = value;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00046B48 File Offset: 0x00044D48
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x00046B68 File Offset: 0x00044D68
		public string DataMinWidthField
		{
			get
			{
				return (string)(base.ViewState["DataMinWidthField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMinWidthField"] = value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00046B7B File Offset: 0x00044D7B
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00046B9B File Offset: 0x00044D9B
		public string DataMinHeightField
		{
			get
			{
				return (string)(base.ViewState["DataMinHeightField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMinHeightField"] = value;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00046BAE File Offset: 0x00044DAE
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x00046BCE File Offset: 0x00044DCE
		public string DataFillColorField
		{
			get
			{
				return (string)(base.ViewState["DataFillColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFillColorField"] = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00046BE1 File Offset: 0x00044DE1
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x00046C01 File Offset: 0x00044E01
		public string DataContentTextField
		{
			get
			{
				return (string)(base.ViewState["DataContentTextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataContentTextField"] = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00046C14 File Offset: 0x00044E14
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00046C34 File Offset: 0x00044E34
		public string DataContentAlignField
		{
			get
			{
				return (string)(base.ViewState["DataContentAlignField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataContentAlignField"] = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00046C47 File Offset: 0x00044E47
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00046C67 File Offset: 0x00044E67
		public string DataIdField
		{
			get
			{
				return (string)(base.ViewState["DataIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataIdField"] = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00046C7A File Offset: 0x00044E7A
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00046C9A File Offset: 0x00044E9A
		public string DataPathField
		{
			get
			{
				return (string)(base.ViewState["DataPathField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataPathField"] = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00046CAD File Offset: 0x00044EAD
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00046CCD File Offset: 0x00044ECD
		public string DataTypeField
		{
			get
			{
				return (string)(base.ViewState["DataTypeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTypeField"] = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00046CE0 File Offset: 0x00044EE0
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00046D00 File Offset: 0x00044F00
		public string DataHoverFillColorField
		{
			get
			{
				return (string)(base.ViewState["DataHoverFillColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataHoverFillColorField"] = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00046D13 File Offset: 0x00044F13
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00046D33 File Offset: 0x00044F33
		public string DataRotationAngleField
		{
			get
			{
				return (string)(base.ViewState["DataRotationAngleField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataRotationAngleField"] = value;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00046D46 File Offset: 0x00044F46
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00046D66 File Offset: 0x00044F66
		public string DataStrokeColorField
		{
			get
			{
				return (string)(base.ViewState["DataStrokeColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataStrokeColorField"] = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00046D79 File Offset: 0x00044F79
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00046D99 File Offset: 0x00044F99
		public string DataStrokeDashTypeField
		{
			get
			{
				return (string)(base.ViewState["DataStrokeDashTypeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataStrokeDashTypeField"] = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00046DAC File Offset: 0x00044FAC
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00046DCC File Offset: 0x00044FCC
		public string DataStrokeWidthField
		{
			get
			{
				return (string)(base.ViewState["DataStrokeWidthField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataStrokeWidthField"] = value;
			}
		}
	}
}
