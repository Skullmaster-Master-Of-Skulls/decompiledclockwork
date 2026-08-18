using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D7D RID: 3453
	internal class XmlaMdSchemaMeasuresReader
	{
		// Token: 0x060080CE RID: 32974 RVA: 0x001D7462 File Offset: 0x001D5662
		public XmlaMdSchemaMeasuresReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028DD RID: 10461
		// (get) Token: 0x060080CF RID: 32975 RVA: 0x001D7478 File Offset: 0x001D5678
		public IEnumerable<MeasureSchemaElement> Measures
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaMeasuresReader.GetXName("row")).Select(new Func<XElement, MeasureSchemaElement>(this.CreateMeasureFromXml)).ToList<MeasureSchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060080D0 RID: 32976 RVA: 0x001D74C4 File Offset: 0x001D56C4
		private MeasureSchemaElement CreateMeasureFromXml(XElement e)
		{
			return new MeasureSchemaElement
			{
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				Caption = XmlaXmlHelper.TryGetElementValue(e, "MEASURE_CAPTION"),
				DisplayFolder = XmlaXmlHelper.TryGetElementValue(e, "MEASURE_DISPLAY_FOLDER"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "MEASURE_NAME"),
				GroupName = XmlaXmlHelper.TryGetElementValue(e, "MEASUREGROUP_NAME"),
				UniqueName = XmlaXmlHelper.TryGetElementValue(e, "MEASURE_UNIQUE_NAME"),
				DataTypeNumber = XmlaXmlHelper.TryGetElementValueAsInt(e, "DATA_TYPE")
			};
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x001D7562 File Offset: 0x001D5762
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x04002372 RID: 9074
		private XElement rootElement;

		// Token: 0x04002373 RID: 9075
		private IList<MeasureSchemaElement> infos;
	}
}
