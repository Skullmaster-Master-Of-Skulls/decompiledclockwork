using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012BC RID: 4796
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.UpperMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadUpperMaskPart.js")]
	public class UpperMaskPart : MaskPart
	{
		// Token: 0x170040E2 RID: 16610
		// (get) Token: 0x0600C8C2 RID: 51394 RVA: 0x002CC54F File Offset: 0x002CA74F
		// (set) Token: 0x0600C8C3 RID: 51395 RVA: 0x002CC557 File Offset: 0x002CA757
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value.ToUpper();
			}
		}

		// Token: 0x0600C8C4 RID: 51396 RVA: 0x002CC565 File Offset: 0x002CA765
		public override string ToString()
		{
			return "Upper Part";
		}

		// Token: 0x170040E3 RID: 16611
		// (get) Token: 0x0600C8C5 RID: 51397 RVA: 0x002CC56C File Offset: 0x002CA76C
		internal override string Part
		{
			get
			{
				return "L";
			}
		}

		// Token: 0x170040E4 RID: 16612
		// (get) Token: 0x0600C8C6 RID: 51398 RVA: 0x002CC573 File Offset: 0x002CA773
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

		// Token: 0x170040E5 RID: 16613
		// (get) Token: 0x0600C8C7 RID: 51399 RVA: 0x002CC58F File Offset: 0x002CA78F
		internal override string InitScript
		{
			get
			{
				return "new Telerik.Web.UI.RadUpperMaskPart()";
			}
		}
	}
}
