using System;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI
{
	// Token: 0x020012B7 RID: 4791
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.LiteralMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadLiteralMaskPart.js")]
	public class LiteralMaskPart : MaskPart
	{
		// Token: 0x170040C9 RID: 16585
		// (get) Token: 0x0600C88B RID: 51339 RVA: 0x002CBEA9 File Offset: 0x002CA0A9
		// (set) Token: 0x0600C88C RID: 51340 RVA: 0x002CBEB0 File Offset: 0x002CA0B0
		public override string Value
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x170040CA RID: 16586
		// (get) Token: 0x0600C88D RID: 51341 RVA: 0x002CBEB2 File Offset: 0x002CA0B2
		// (set) Token: 0x0600C88E RID: 51342 RVA: 0x002CBEBA File Offset: 0x002CA0BA
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x0600C88F RID: 51343 RVA: 0x002CBEC3 File Offset: 0x002CA0C3
		public override string ToString()
		{
			return "Literal Part";
		}

		// Token: 0x0600C890 RID: 51344 RVA: 0x002CBECA File Offset: 0x002CA0CA
		internal void Append(char c)
		{
			this._text += c;
		}

		// Token: 0x0600C891 RID: 51345 RVA: 0x002CBEE3 File Offset: 0x002CA0E3
		internal void Append(string c)
		{
			this._text += c;
		}

		// Token: 0x170040CB RID: 16587
		// (get) Token: 0x0600C892 RID: 51346 RVA: 0x002CBEF7 File Offset: 0x002CA0F7
		internal override int PromptLength
		{
			get
			{
				return this._text.Length;
			}
		}

		// Token: 0x170040CC RID: 16588
		// (get) Token: 0x0600C893 RID: 51347 RVA: 0x002CBF04 File Offset: 0x002CA104
		internal override int ValueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170040CD RID: 16589
		// (get) Token: 0x0600C894 RID: 51348 RVA: 0x002CBF07 File Offset: 0x002CA107
		internal override string Part
		{
			get
			{
				return LiteralMaskPart.Escape(this._text);
			}
		}

		// Token: 0x170040CE RID: 16590
		// (get) Token: 0x0600C895 RID: 51349 RVA: 0x002CBF14 File Offset: 0x002CA114
		internal override string Prompt
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x170040CF RID: 16591
		// (get) Token: 0x0600C896 RID: 51350 RVA: 0x002CBF1C File Offset: 0x002CA11C
		internal override string InitScript
		{
			get
			{
				return string.Format("new Telerik.Web.UI.RadLiteralMaskPart('{0}')", this.EscapedPropmt);
			}
		}

		// Token: 0x170040D0 RID: 16592
		// (get) Token: 0x0600C897 RID: 51351 RVA: 0x002CBF2E File Offset: 0x002CA12E
		private string EscapedPropmt
		{
			get
			{
				return this.Prompt.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'");
			}
		}

		// Token: 0x0600C898 RID: 51352 RVA: 0x002CBF63 File Offset: 0x002CA163
		internal override int SetValue(string value)
		{
			if (value.ToLower() == this._text.ToLower())
			{
				return value.Length;
			}
			return 0;
		}

		// Token: 0x0600C899 RID: 51353 RVA: 0x002CBF85 File Offset: 0x002CA185
		internal static string Escape(string source)
		{
			return Regex.Replace(source, "(\\\\|\\*|\\||#|L|l|a|<|>)", "\\$1");
		}

		// Token: 0x040034CC RID: 13516
		private const string MaskPattern = "\\\\|\\*|\\||#|L|l|a|<|>";

		// Token: 0x040034CD RID: 13517
		private string _text = string.Empty;
	}
}
