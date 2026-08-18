using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x020012B5 RID: 4789
	[ClientScriptResource("Telerik.Web.UI.Input.MaskedTextBox.MaskParts.EnumerationMaskPart", "Telerik.Web.UI.Input.MaskedTextBox.MaskParts.RadEnumerationMaskPart.js")]
	public class EnumerationMaskPart : MaskPart
	{
		// Token: 0x170040BF RID: 16575
		// (get) Token: 0x0600C87A RID: 51322 RVA: 0x002CBB48 File Offset: 0x002C9D48
		[Editor("Telerik.Web.Design.EnumerationMaskPartCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public StringCollection Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x170040C0 RID: 16576
		// (get) Token: 0x0600C87B RID: 51323 RVA: 0x002CBB50 File Offset: 0x002C9D50
		// (set) Token: 0x0600C87C RID: 51324 RVA: 0x002CBB88 File Offset: 0x002C9D88
		public override string Value
		{
			get
			{
				if (this.SelectedIndex >= 0)
				{
					return this._items[this.SelectedIndex];
				}
				if (base.AllowEmptyEnumerations)
				{
					return "";
				}
				return this._items[0];
			}
			set
			{
				char[] trimChars = new char[]
				{
					base.PromptChar[0]
				};
				this.SelectedIndex = this._items.IndexOf(value.Trim(trimChars));
			}
		}

		// Token: 0x170040C1 RID: 16577
		// (get) Token: 0x0600C87D RID: 51325 RVA: 0x002CBBC5 File Offset: 0x002C9DC5
		// (set) Token: 0x0600C87E RID: 51326 RVA: 0x002CBBCD File Offset: 0x002C9DCD
		internal int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				this._selectedIndex = value;
			}
		}

		// Token: 0x170040C2 RID: 16578
		// (get) Token: 0x0600C87F RID: 51327 RVA: 0x002CBBD8 File Offset: 0x002C9DD8
		internal override int PromptLength
		{
			get
			{
				int num = 0;
				foreach (string text in this._items)
				{
					num = Math.Max(num, text.Length);
				}
				return num;
			}
		}

		// Token: 0x170040C3 RID: 16579
		// (get) Token: 0x0600C880 RID: 51328 RVA: 0x002CBC38 File Offset: 0x002C9E38
		internal override string Part
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this._items)
				{
					stringBuilder.Append(text.Replace("\\", "\\\\").Replace("|", "\\|"));
					stringBuilder.Append("|");
				}
				if (this._items.Count == 0)
				{
					return "<>";
				}
				return "<" + stringBuilder.ToString(0, stringBuilder.Length - 1) + ">";
			}
		}

		// Token: 0x170040C4 RID: 16580
		// (get) Token: 0x0600C881 RID: 51329 RVA: 0x002CBCF0 File Offset: 0x002C9EF0
		internal override string Prompt
		{
			get
			{
				if (this.SelectedIndex == -1)
				{
					return string.Empty.PadRight(this.PromptLength, base.PromptChar[0]);
				}
				return this._items[this.SelectedIndex].PadRight(this.PromptLength, base.PromptChar[0]);
			}
		}

		// Token: 0x170040C5 RID: 16581
		// (get) Token: 0x0600C882 RID: 51330 RVA: 0x002CBD4C File Offset: 0x002C9F4C
		internal override string InitScript
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this._items)
				{
					stringBuilder.Append('\'');
					stringBuilder.Append(text.Replace("\\", "\\\\").Replace("'", "\\'"));
					stringBuilder.Append('\'');
					stringBuilder.Append(",");
				}
				return string.Format("new Telerik.Web.UI.RadEnumerationMaskPart([{0}])", stringBuilder.ToString(0, stringBuilder.Length - 1));
			}
		}

		// Token: 0x0600C883 RID: 51331 RVA: 0x002CBDFC File Offset: 0x002C9FFC
		internal override int SetValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.Value = value;
				return 0;
			}
			while (this.Items.IndexOf(value) == -1 && value.Length != 0)
			{
				value = value.Substring(0, value.Length - 1);
			}
			this.Value = value;
			return value.Length;
		}

		// Token: 0x0600C884 RID: 51332 RVA: 0x002CBE4F File Offset: 0x002CA04F
		public override string ToString()
		{
			return "Enumeration Part";
		}

		// Token: 0x040034CA RID: 13514
		private StringCollection _items = new StringCollection();

		// Token: 0x040034CB RID: 13515
		private int _selectedIndex = -1;
	}
}
