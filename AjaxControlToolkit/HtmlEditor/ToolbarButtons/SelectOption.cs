using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000124 RID: 292
	public class SelectOption
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00013FBA File Offset: 0x000121BA
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00013FC2 File Offset: 0x000121C2
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00013FCB File Offset: 0x000121CB
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00013FD3 File Offset: 0x000121D3
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

		// Token: 0x0400030E RID: 782
		private string _value = string.Empty;

		// Token: 0x0400030F RID: 783
		private string _text = string.Empty;
	}
}
