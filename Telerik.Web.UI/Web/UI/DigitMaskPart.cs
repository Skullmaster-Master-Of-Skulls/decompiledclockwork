using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI
{
	// Token: 0x020012B4 RID: 4788
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.DigitMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadDigitMaskPart.js")]
	public class DigitMaskPart : MaskPart
	{
		// Token: 0x170040BB RID: 16571
		// (get) Token: 0x0600C872 RID: 51314 RVA: 0x002CBA9B File Offset: 0x002C9C9B
		// (set) Token: 0x0600C873 RID: 51315 RVA: 0x002CBAB6 File Offset: 0x002C9CB6
		public override string Value
		{
			get
			{
				if (!this.isEmpty)
				{
					return this._intValue.ToString();
				}
				return "";
			}
			set
			{
				if (string.IsNullOrEmpty(value) || value == base.PromptChar)
				{
					this.isEmpty = true;
					return;
				}
				this.isEmpty = false;
				this._intValue = int.Parse(value);
			}
		}

		// Token: 0x170040BC RID: 16572
		// (get) Token: 0x0600C874 RID: 51316 RVA: 0x002CBAE9 File Offset: 0x002C9CE9
		internal override string Part
		{
			get
			{
				return "#";
			}
		}

		// Token: 0x0600C875 RID: 51317 RVA: 0x002CBAF0 File Offset: 0x002C9CF0
		public override string ToString()
		{
			return "Digit Part";
		}

		// Token: 0x170040BD RID: 16573
		// (get) Token: 0x0600C876 RID: 51318 RVA: 0x002CBAF7 File Offset: 0x002C9CF7
		internal override string Prompt
		{
			get
			{
				if (!this.isEmpty)
				{
					return this.Value;
				}
				return base.PromptChar;
			}
		}

		// Token: 0x170040BE RID: 16574
		// (get) Token: 0x0600C877 RID: 51319 RVA: 0x002CBB0E File Offset: 0x002C9D0E
		internal override string InitScript
		{
			get
			{
				return "new Telerik.Web.UI.RadDigitMaskPart()";
			}
		}

		// Token: 0x0600C878 RID: 51320 RVA: 0x002CBB15 File Offset: 0x002C9D15
		internal override int SetValue(string value)
		{
			this.Value = string.Empty;
			if (!Regex.IsMatch(value, "\\d{1}"))
			{
				return 0;
			}
			this.Value = value;
			return 1;
		}

		// Token: 0x040034C8 RID: 13512
		private int _intValue;

		// Token: 0x040034C9 RID: 13513
		private bool isEmpty = true;
	}
}
