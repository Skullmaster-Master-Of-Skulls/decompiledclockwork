using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012B6 RID: 4790
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.FreeMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadFreeMaskPart.js")]
	public class FreeMaskPart : MaskPart
	{
		// Token: 0x170040C6 RID: 16582
		// (get) Token: 0x0600C886 RID: 51334 RVA: 0x002CBE70 File Offset: 0x002CA070
		internal override string Part
		{
			get
			{
				return "a";
			}
		}

		// Token: 0x170040C7 RID: 16583
		// (get) Token: 0x0600C887 RID: 51335 RVA: 0x002CBE77 File Offset: 0x002CA077
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

		// Token: 0x170040C8 RID: 16584
		// (get) Token: 0x0600C888 RID: 51336 RVA: 0x002CBE93 File Offset: 0x002CA093
		internal override string InitScript
		{
			get
			{
				return "new Telerik.Web.UI.RadFreeMaskPart()";
			}
		}

		// Token: 0x0600C889 RID: 51337 RVA: 0x002CBE9A File Offset: 0x002CA09A
		public override string ToString()
		{
			return "Free Part";
		}
	}
}
