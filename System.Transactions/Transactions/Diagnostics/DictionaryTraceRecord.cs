using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;

namespace System.Transactions.Diagnostics
{
	// Token: 0x020000C2 RID: 194
	internal class DictionaryTraceRecord : TraceRecord
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x000421E4 File Offset: 0x000415E4
		internal DictionaryTraceRecord(IDictionary dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00042204 File Offset: 0x00041604
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2004/03/Transactions/DictionaryTraceRecord";
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00042224 File Offset: 0x00041624
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.dictionary != null)
			{
				foreach (object obj in this.dictionary.Keys)
				{
					xml.WriteElementString(obj.ToString(), this.dictionary[obj].ToString());
				}
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000422B4 File Offset: 0x000416B4
		public override string ToString()
		{
			string result = null;
			if (this.dictionary != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.dictionary.Keys)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
					{
						obj,
						this.dictionary[obj].ToString()
					}));
				}
			}
			return result;
		}

		// Token: 0x040002E7 RID: 743
		private IDictionary dictionary;
	}
}
