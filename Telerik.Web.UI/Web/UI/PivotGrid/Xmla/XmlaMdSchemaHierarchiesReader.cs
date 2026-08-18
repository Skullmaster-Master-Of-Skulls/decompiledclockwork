using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D7A RID: 3450
	internal class XmlaMdSchemaHierarchiesReader
	{
		// Token: 0x060080C2 RID: 32962 RVA: 0x001D70B4 File Offset: 0x001D52B4
		public XmlaMdSchemaHierarchiesReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028DA RID: 10458
		// (get) Token: 0x060080C3 RID: 32963 RVA: 0x001D70C8 File Offset: 0x001D52C8
		public IEnumerable<HierarchySchemaElement> Hierarchies
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaHierarchiesReader.GetXName("row")).Select(new Func<XElement, HierarchySchemaElement>(this.CreateHierarchyInfoFromXml)).ToList<HierarchySchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060080C4 RID: 32964 RVA: 0x001D7114 File Offset: 0x001D5314
		private HierarchySchemaElement CreateHierarchyInfoFromXml(XElement e)
		{
			HierarchySchemaElement hierarchySchemaElement = new HierarchySchemaElement
			{
				AllMemberName = XmlaXmlHelper.TryGetElementValue(e, "ALL_MEMBER"),
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				DefaultMember = XmlaXmlHelper.TryGetElementValue(e, "DEFAULT_MEMBER"),
				DimensionUniqueName = XmlaXmlHelper.TryGetElementValue(e, "DIMENSION_UNIQUE_NAME"),
				DisplayFolder = XmlaXmlHelper.TryGetElementValue(e, "HIERARCHY_DISPLAY_FOLDER"),
				Caption = XmlaXmlHelper.TryGetElementValue(e, "HIERARCHY_CAPTION"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "HIERARCHY_NAME"),
				UniqueName = XmlaXmlHelper.TryGetElementValue(e, "HIERARCHY_UNIQUE_NAME")
			};
			XElement xelement = e.Element(XmlaMdSchemaHierarchiesReader.GetXName("GROUPING_BEHAVIOR"));
			XElement xelement2 = e.Element(XmlaMdSchemaHierarchiesReader.GetXName("INSTANCE_SELECTION"));
			if (xelement != null)
			{
				hierarchySchemaElement.Grouping = (DimensionHierarchyGroupingBehavior)(int.Parse(xelement.Value, CultureInfo.InvariantCulture) - 1);
			}
			if (xelement2 != null)
			{
				hierarchySchemaElement.ViewType = (DimensionHierarchyInstanceSelection)(int.Parse(xelement2.Value, CultureInfo.InvariantCulture) - 1);
			}
			return hierarchySchemaElement;
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x001D721B File Offset: 0x001D541B
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x0400236C RID: 9068
		private XElement rootElement;

		// Token: 0x0400236D RID: 9069
		private IList<HierarchySchemaElement> infos;
	}
}
