using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000143 RID: 323
	public class CssStyleWriter
	{
		// Token: 0x06000CEB RID: 3307 RVA: 0x0002DF89 File Offset: 0x0002C189
		public CssStyleWriter(HtmlTextWriter writer)
		{
			this.writer = writer;
			this.styles = new Dictionary<string, string>();
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002DFA3 File Offset: 0x0002C1A3
		public void AddStyle(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				return;
			}
			this.styles[name] = value;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002DFBB File Offset: 0x0002C1BB
		public void WriteAttribute()
		{
			if (this.styles.Count == 0)
			{
				return;
			}
			this.writer.Write(" style=\"");
			this.WriteStyles();
			this.writer.Write("\"");
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
		private void WriteStyles()
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.styles)
			{
				this.WriteStyle(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002E054 File Offset: 0x0002C254
		private void WriteStyle(string name, string value)
		{
			this.writer.WriteStyleAttribute(name, value);
		}

		// Token: 0x0400032A RID: 810
		private Dictionary<string, string> styles;

		// Token: 0x0400032B RID: 811
		private readonly HtmlTextWriter writer;
	}
}
