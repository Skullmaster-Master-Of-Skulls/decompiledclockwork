using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.MonitoringLists;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005DC RID: 1500
	public static class MonitoringListAdapter
	{
		// Token: 0x06003045 RID: 12357 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		public static List<MonitorList> GetMonitorListsFromXml(this string Xml)
		{
			bool flag = string.IsNullOrEmpty(Xml);
			List<MonitorList> result;
			if (flag)
			{
				result = new List<MonitorList>();
			}
			else
			{
				XDocument xdocument = XDocument.Parse(Xml);
				int num;
				List<MonitorList> list = (from lbl in xdocument.Descendants("monitorlist")
				select new MonitorList
				{
					UniqueName = lbl.Element("name").Value,
					ReportId = (int.TryParse(lbl.Element("reportid").Value, out num) ? num : 0),
					SubReportId = (int.TryParse(lbl.Element("subreportid").Value, out num) ? num : 0),
					IsVisible = lbl.Element("isvisible").Value.Equals("1"),
					IsActive = lbl.Element("isactive").Value.Equals("1"),
					Title = lbl.Element("title").Value
				}).ToList<MonitorList>();
				result = list;
			}
			return result;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0003E43C File Offset: 0x0003C63C
		public static string GetXmlFromMonitorLists(this List<MonitorList> monitorLists)
		{
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			array[0] = new XElement("monitorlists", from ml in monitorLists
			select new XElement("monitorlist", new object[]
			{
				new XElement("name", ml.UniqueName),
				new XElement("reportid", ml.ReportId.ToString()),
				new XElement("subreportid", ml.SubReportId.ToString()),
				new XElement("isvisible", ml.IsVisible ? "1" : "0"),
				new XElement("isactive", ml.IsActive ? "1" : "0"),
				new XElement("title", ml.Title)
			}));
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.ToString();
		}
	}
}
