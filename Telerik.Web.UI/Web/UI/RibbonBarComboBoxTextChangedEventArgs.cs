using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E2E RID: 3630
	public class RibbonBarComboBoxTextChangedEventArgs : EventArgs
	{
		// Token: 0x17002B7F RID: 11135
		// (get) Token: 0x06008976 RID: 35190 RVA: 0x001F5C85 File Offset: 0x001F3E85
		public RibbonBarComboBox ComboBox
		{
			get
			{
				return this._comboBox;
			}
		}

		// Token: 0x17002B80 RID: 11136
		// (get) Token: 0x06008977 RID: 35191 RVA: 0x001F5C8D File Offset: 0x001F3E8D
		public RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17002B81 RID: 11137
		// (get) Token: 0x06008978 RID: 35192 RVA: 0x001F5C95 File Offset: 0x001F3E95
		public string Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x001F5C9D File Offset: 0x001F3E9D
		public RibbonBarComboBoxTextChangedEventArgs(string text, RibbonBarComboBox comboBox, RibbonBarGroup group)
		{
			this._group = group;
			this._comboBox = comboBox;
			this._text = text;
		}

		// Token: 0x04002673 RID: 9843
		private RibbonBarGroup _group;

		// Token: 0x04002674 RID: 9844
		private RibbonBarComboBox _comboBox;

		// Token: 0x04002675 RID: 9845
		private string _text;
	}
}
