using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D7E RID: 3454
	internal class XmlaMdSchemaSetsReader : XmlaMdSchemaReader
	{
		// Token: 0x060080D2 RID: 32978 RVA: 0x001D756F File Offset: 0x001D576F
		public XmlaMdSchemaSetsReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028DE RID: 10462
		// (get) Token: 0x060080D3 RID: 32979 RVA: 0x001D7583 File Offset: 0x001D5783
		protected override string SchemaName
		{
			get
			{
				return "MDSCHEMA_KPIS";
			}
		}

		// Token: 0x170028DF RID: 10463
		// (get) Token: 0x060080D4 RID: 32980 RVA: 0x001D758C File Offset: 0x001D578C
		public IEnumerable<NamedSetSchemaElement> Sets
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaSetsReader.GetXName("row")).Select(new Func<XElement, NamedSetSchemaElement>(this.CreateSetInfoFromXml)).ToList<NamedSetSchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060080D5 RID: 32981 RVA: 0x001D75D8 File Offset: 0x001D57D8
		private NamedSetSchemaElement CreateSetInfoFromXml(XElement e)
		{
			return new NamedSetSchemaElement
			{
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				Caption = XmlaXmlHelper.TryGetElementValue(e, "SET_CAPTION"),
				Dimensions = XmlaXmlHelper.TryGetElementValue(e, "DIMENSIONS"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "SET_NAME")
			};
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x001D7643 File Offset: 0x001D5843
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x04002374 RID: 9076
		private const string ElementNameCatalogName = "CATALOG_NAME";

		// Token: 0x04002375 RID: 9077
		private const string ElementNameCubeName = "CUBE_NAME";

		// Token: 0x04002376 RID: 9078
		private const string ElementNameSetCaption = "SET_CAPTION";

		// Token: 0x04002377 RID: 9079
		private const string ElementNameDimensions = "DIMENSIONS";

		// Token: 0x04002378 RID: 9080
		private const string ElementNameSetName = "SET_NAME";

		// Token: 0x04002379 RID: 9081
		private XElement rootElement;

		// Token: 0x0400237A RID: 9082
		private IList<NamedSetSchemaElement> infos;
	}
}
