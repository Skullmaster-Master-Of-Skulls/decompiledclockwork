using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000740 RID: 1856
	internal class XmlaMdSchemaMeasureGroupsReader
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x000CEECE File Offset: 0x000CD0CE
		public XmlaMdSchemaMeasureGroupsReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x000CEEE4 File Offset: 0x000CD0E4
		public IEnumerable<MeasureGroupSchemaElement> MeasureGroups
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaMeasureGroupsReader.GetXName("row")).Select(new Func<XElement, MeasureGroupSchemaElement>(this.CreateMeasureGroupFromXml)).ToList<MeasureGroupSchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000CEF30 File Offset: 0x000CD130
		private MeasureGroupSchemaElement CreateMeasureGroupFromXml(XElement e)
		{
			return new MeasureGroupSchemaElement
			{
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				Caption = XmlaXmlHelper.TryGetElementValue(e, "MEASUREGROUP_CAPTION"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "MEASUREGROUP_NAME")
			};
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000CEF8A File Offset: 0x000CD18A
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x0400116B RID: 4459
		private XElement rootElement;

		// Token: 0x0400116C RID: 4460
		private IList<MeasureGroupSchemaElement> infos;
	}
}
