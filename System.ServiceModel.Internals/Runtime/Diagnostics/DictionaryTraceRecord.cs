using System;
using System.Collections;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000042 RID: 66
	internal class DictionaryTraceRecord : TraceRecord
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000AE75 File Offset: 0x00009075
		internal DictionaryTraceRecord(IDictionary dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AE84 File Offset: 0x00009084
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/DictionaryTraceRecord";
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000AE8C File Offset: 0x0000908C
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.dictionary != null)
			{
				foreach (object obj in this.dictionary.Keys)
				{
					object obj2 = this.dictionary[obj];
					xml.WriteElementString(obj.ToString(), (obj2 == null) ? string.Empty : obj2.ToString());
				}
			}
		}

		// Token: 0x04000118 RID: 280
		private IDictionary dictionary;
	}
}
