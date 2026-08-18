using System;
using System.Collections;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A74 RID: 2676
	internal class CollectionTraceRecord : TraceRecord
	{
		// Token: 0x0600696D RID: 26989 RVA: 0x001896A4 File Offset: 0x001878A4
		public CollectionTraceRecord(string collectionName, string elementName, IEnumerable entries)
		{
			this.collectionName = (string.IsNullOrEmpty(collectionName) ? "Elements" : collectionName);
			this.elementName = (string.IsNullOrEmpty(elementName) ? "Element" : elementName);
			this.entries = entries;
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x0600696E RID: 26990 RVA: 0x001896DF File Offset: 0x001878DF
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("Collection");
			}
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x001896EC File Offset: 0x001878EC
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.entries != null)
			{
				xml.WriteStartElement(this.collectionName);
				foreach (object obj in this.entries)
				{
					xml.WriteElementString(this.elementName, (obj == null) ? "null" : obj.ToString());
				}
				xml.WriteEndElement();
			}
		}

		// Token: 0x04003C4B RID: 15435
		private IEnumerable entries;

		// Token: 0x04003C4C RID: 15436
		private string collectionName;

		// Token: 0x04003C4D RID: 15437
		private string elementName;
	}
}
