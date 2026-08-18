using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D77 RID: 3447
	internal class XmlaTupleInfo : IOlapTuple
	{
		// Token: 0x060080B7 RID: 32951 RVA: 0x001D6ED7 File Offset: 0x001D50D7
		public XmlaTupleInfo()
		{
			this.members = new List<XmlaMemberInfo>();
		}

		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x060080B8 RID: 32952 RVA: 0x001D6EEA File Offset: 0x001D50EA
		public IEnumerable<XmlaMemberInfo> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170028D7 RID: 10455
		// (get) Token: 0x060080B9 RID: 32953 RVA: 0x001D6EF2 File Offset: 0x001D50F2
		IEnumerable IOlapTuple.Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x001D6EFC File Offset: 0x001D50FC
		public static XmlaTupleInfo FromXElement(XElement tupleElement)
		{
			XmlaTupleInfo xmlaTupleInfo = new XmlaTupleInfo();
			List<XElement> list = tupleElement.Descendants(XName.Get("Member", "urn:schemas-microsoft-com:xml-analysis:mddataset")).ToList<XElement>();
			foreach (XElement memberElement in list)
			{
				XmlaMemberInfo item = XmlaMemberInfo.FromXElement(memberElement);
				xmlaTupleInfo.members.Add(item);
			}
			return xmlaTupleInfo;
		}

		// Token: 0x04002367 RID: 9063
		private IList<XmlaMemberInfo> members;
	}
}
