using System;

namespace Telerik.Web.UI
{
	// Token: 0x020009C0 RID: 2496
	public class AutoCompleteTextEventArgs : EventArgs
	{
		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06005F5B RID: 24411 RVA: 0x00122783 File Offset: 0x00120983
		// (set) Token: 0x06005F5C RID: 24412 RVA: 0x0012278B File Offset: 0x0012098B
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

		// Token: 0x06005F5D RID: 24413 RVA: 0x00122794 File Offset: 0x00120994
		public AutoCompleteTextEventArgs(string text)
		{
			this._text = text;
		}

		// Token: 0x040016F9 RID: 5881
		private string _text;
	}
}
