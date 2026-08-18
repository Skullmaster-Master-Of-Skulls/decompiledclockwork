using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D79 RID: 3449
	internal class XmlaMdSchemaDimensionsReader
	{
		// Token: 0x060080BE RID: 32958 RVA: 0x001D6FDA File Offset: 0x001D51DA
		public XmlaMdSchemaDimensionsReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028D9 RID: 10457
		// (get) Token: 0x060080BF RID: 32959 RVA: 0x001D6FF0 File Offset: 0x001D51F0
		public IEnumerable<DimensionSchemaElement> Dimensions
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaDimensionsReader.GetXName("row")).Select(new Func<XElement, DimensionSchemaElement>(this.CreateDimensionFromXml)).ToList<DimensionSchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x001D703C File Offset: 0x001D523C
		private DimensionSchemaElement CreateDimensionFromXml(XElement e)
		{
			return new DimensionSchemaElement
			{
				Caption = XmlaXmlHelper.TryGetElementValue(e, "DIMENSION_CAPTION"),
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "DIMENSION_NAME"),
				UniqueName = XmlaXmlHelper.TryGetElementValue(e, "DIMENSION_UNIQUE_NAME")
			};
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x001D70A7 File Offset: 0x001D52A7
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x0400236A RID: 9066
		private XElement rootElement;

		// Token: 0x0400236B RID: 9067
		private IList<DimensionSchemaElement> infos;
	}
}
