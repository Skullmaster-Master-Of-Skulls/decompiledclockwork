using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D7C RID: 3452
	internal class XmlaMdSchemaLevelsReader
	{
		// Token: 0x060080CA RID: 32970 RVA: 0x001D7366 File Offset: 0x001D5566
		public XmlaMdSchemaLevelsReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028DC RID: 10460
		// (get) Token: 0x060080CB RID: 32971 RVA: 0x001D737C File Offset: 0x001D557C
		public IEnumerable<LevelSchemaElement> Levels
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaLevelsReader.GetXName("row")).Select(new Func<XElement, LevelSchemaElement>(this.CreateLevelInfoFromXml)).ToList<LevelSchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x001D73C8 File Offset: 0x001D55C8
		private LevelSchemaElement CreateLevelInfoFromXml(XElement e)
		{
			return new LevelSchemaElement
			{
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				DimensionUniqueName = XmlaXmlHelper.TryGetElementValue(e, "DIMENSION_UNIQUE_NAME"),
				HierarchyUniqueName = XmlaXmlHelper.TryGetElementValue(e, "HIERARCHY_UNIQUE_NAME"),
				Caption = XmlaXmlHelper.TryGetElementValue(e, "LEVEL_CAPTION"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "LEVEL_NAME"),
				UniqueName = XmlaXmlHelper.TryGetElementValue(e, "LEVEL_UNIQUE_NAME")
			};
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x001D7455 File Offset: 0x001D5655
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x04002370 RID: 9072
		private XElement rootElement;

		// Token: 0x04002371 RID: 9073
		private IList<LevelSchemaElement> infos;
	}
}
