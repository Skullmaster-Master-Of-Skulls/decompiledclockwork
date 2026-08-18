using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012B8 RID: 4792
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.LowerMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadLowerMaskPart.js")]
	public class LowerMaskPart : MaskPart
	{
		// Token: 0x170040D1 RID: 16593
		// (get) Token: 0x0600C89B RID: 51355 RVA: 0x002CBFAA File Offset: 0x002CA1AA
		// (set) Token: 0x0600C89C RID: 51356 RVA: 0x002CBFB2 File Offset: 0x002CA1B2
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value.ToLower();
			}
		}

		// Token: 0x0600C89D RID: 51357 RVA: 0x002CBFC0 File Offset: 0x002CA1C0
		public override string ToString()
		{
			return "Lower Part";
		}

		// Token: 0x170040D2 RID: 16594
		// (get) Token: 0x0600C89E RID: 51358 RVA: 0x002CBFC7 File Offset: 0x002CA1C7
		internal override string Part
		{
			get
			{
				return "l";
			}
		}

		// Token: 0x170040D3 RID: 16595
		// (get) Token: 0x0600C89F RID: 51359 RVA: 0x002CBFCE File Offset: 0x002CA1CE
		internal override string Prompt
		{
			get
			{
				if (string.IsNullOrEmpty(this.Value))
				{
					return base.PromptChar;
				}
				return this.Value;
			}
		}

		// Token: 0x170040D4 RID: 16596
		// (get) Token: 0x0600C8A0 RID: 51360 RVA: 0x002CBFEA File Offset: 0x002CA1EA
		internal override string InitScript
		{
			get
			{
				return "new Telerik.Web.UI.RadLowerMaskPart()";
			}
		}
	}
}
