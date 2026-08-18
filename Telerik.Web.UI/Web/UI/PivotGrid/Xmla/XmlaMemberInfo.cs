using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D76 RID: 3446
	internal class XmlaMemberInfo : IOlapMember, IOlapElement
	{
		// Token: 0x170028D0 RID: 10448
		// (get) Token: 0x060080A8 RID: 32936 RVA: 0x001D6CE7 File Offset: 0x001D4EE7
		// (set) Token: 0x060080A9 RID: 32937 RVA: 0x001D6CEF File Offset: 0x001D4EEF
		public string HierarchyName { get; private set; }

		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x060080AA RID: 32938 RVA: 0x001D6CF8 File Offset: 0x001D4EF8
		// (set) Token: 0x060080AB RID: 32939 RVA: 0x001D6D00 File Offset: 0x001D4F00
		public string UniqueName { get; private set; }

		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x060080AC RID: 32940 RVA: 0x001D6D09 File Offset: 0x001D4F09
		// (set) Token: 0x060080AD RID: 32941 RVA: 0x001D6D11 File Offset: 0x001D4F11
		public string Caption { get; private set; }

		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x060080AE RID: 32942 RVA: 0x001D6D1A File Offset: 0x001D4F1A
		// (set) Token: 0x060080AF RID: 32943 RVA: 0x001D6D22 File Offset: 0x001D4F22
		public int LevelNumber { get; private set; }

		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x060080B0 RID: 32944 RVA: 0x001D6D2B File Offset: 0x001D4F2B
		// (set) Token: 0x060080B1 RID: 32945 RVA: 0x001D6D33 File Offset: 0x001D4F33
		public string LevelName { get; private set; }

		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x060080B2 RID: 32946 RVA: 0x001D6D3C File Offset: 0x001D4F3C
		// (set) Token: 0x060080B3 RID: 32947 RVA: 0x001D6D44 File Offset: 0x001D4F44
		public IList<string> SortKeys { get; private set; }

		// Token: 0x060080B4 RID: 32948 RVA: 0x001D6D50 File Offset: 0x001D4F50
		public static XmlaMemberInfo FromXElement(XElement memberElement)
		{
			XmlaMemberInfo xmlaMemberInfo = new XmlaMemberInfo();
			xmlaMemberInfo.HierarchyName = memberElement.Attribute(XName.Get("Hierarchy")).Value;
			xmlaMemberInfo.Caption = memberElement.Element(XName.Get("Caption", "urn:schemas-microsoft-com:xml-analysis:mddataset")).Value;
			xmlaMemberInfo.UniqueName = memberElement.Element(XName.Get("UName", "urn:schemas-microsoft-com:xml-analysis:mddataset")).Value;
			xmlaMemberInfo.LevelName = memberElement.Element(XName.Get("LName", "urn:schemas-microsoft-com:xml-analysis:mddataset")).Value;
			xmlaMemberInfo.LevelNumber = int.Parse(memberElement.Element(XName.Get("LNum", "urn:schemas-microsoft-com:xml-analysis:mddataset")).Value, CultureInfo.InvariantCulture);
			XName xname = XName.Get("Key", "urn:schemas-microsoft-com:xml-analysis:mddataset");
			if (xname != null && memberElement.Element(xname) != null)
			{
				xmlaMemberInfo.SortKeys = new List<string>();
				List<XElement> list = memberElement.Elements(xname).ToList<XElement>();
				foreach (XElement xelement in list)
				{
					xmlaMemberInfo.SortKeys.Add(xelement.Value);
				}
			}
			return xmlaMemberInfo;
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x001D6E90 File Offset: 0x001D5090
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} | {1} | {2}", new object[]
			{
				this.Caption,
				this.HierarchyName,
				this.UniqueName
			});
		}
	}
}
