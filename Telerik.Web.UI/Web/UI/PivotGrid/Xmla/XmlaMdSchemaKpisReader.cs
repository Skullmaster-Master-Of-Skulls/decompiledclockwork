using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D7B RID: 3451
	internal class XmlaMdSchemaKpisReader
	{
		// Token: 0x060080C6 RID: 32966 RVA: 0x001D7228 File Offset: 0x001D5428
		public XmlaMdSchemaKpisReader(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028DB RID: 10459
		// (get) Token: 0x060080C7 RID: 32967 RVA: 0x001D723C File Offset: 0x001D543C
		public IEnumerable<KpiSchemaElement> Kpis
		{
			get
			{
				if (this.infos == null)
				{
					this.infos = this.rootElement.Descendants(XmlaMdSchemaKpisReader.GetXName("row")).Select(new Func<XElement, KpiSchemaElement>(this.CreateKpiInfoFromXml)).ToList<KpiSchemaElement>();
				}
				return this.infos;
			}
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x001D7288 File Offset: 0x001D5488
		private KpiSchemaElement CreateKpiInfoFromXml(XElement e)
		{
			return new KpiSchemaElement
			{
				CatalogName = XmlaXmlHelper.TryGetElementValue(e, "CATALOG_NAME"),
				CubeName = XmlaXmlHelper.TryGetElementValue(e, "CUBE_NAME"),
				Caption = XmlaXmlHelper.TryGetElementValue(e, "KPI_CAPTION"),
				DisplayFolder = XmlaXmlHelper.TryGetElementValue(e, "KPI_DISPLAY_FOLDER"),
				Name = XmlaXmlHelper.TryGetElementValue(e, "KPI_NAME"),
				StatusGraphic = XmlaXmlHelper.TryGetElementValue(e, "KPI_STATUS_GRAPHIC"),
				TrendGraphic = XmlaXmlHelper.TryGetElementValue(e, "KPI_TREND_GRAPHIC"),
				ValueMemberUniqueName = XmlaXmlHelper.TryGetElementValue(e, "KPI_VALUE"),
				GoalMemberUniqueName = XmlaXmlHelper.TryGetElementValue(e, "KPI_GOAL"),
				TrendMemberUniqueName = XmlaXmlHelper.TryGetElementValue(e, "KPI_TREND"),
				StatusMemberUniqueName = XmlaXmlHelper.TryGetElementValue(e, "KPI_STATUS")
			};
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x001D7359 File Offset: 0x001D5559
		private static XName GetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x0400236E RID: 9070
		private XElement rootElement;

		// Token: 0x0400236F RID: 9071
		private IList<KpiSchemaElement> infos;
	}
}
