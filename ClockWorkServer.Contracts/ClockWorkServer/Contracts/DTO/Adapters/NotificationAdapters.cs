using System;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notifications.MultiUserSimulatenousAccess;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C89 RID: 3209
	public static class NotificationAdapters
	{
		// Token: 0x060042DB RID: 17115 RVA: 0x000226E0 File Offset: 0x000208E0
		public static MultiAccessInfo GetMultiAccessInfoFromString(this string s)
		{
			MultiAccessInfo result;
			try
			{
				XDocument xdocument = XDocument.Parse(s);
				XElement xelement = xdocument.Element("multiaccessinfo");
				XElement xelement2 = xelement.Element("context");
				XAttribute xattribute = xelement.Attribute("accesstype");
				XAttribute attr = xelement.Attribute("who");
				XAttribute xattribute2 = xelement.Attribute("whoname");
				XAttribute attr2 = xelement2.Attribute("pid");
				XAttribute attr3 = xelement2.Attribute("appid");
				XAttribute attr4 = xelement2.Attribute("screennum");
				string value = (xattribute == null) ? "" : (xattribute.Value ?? "");
				eMultiAccessType accessType = (string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(eMultiAccessType), value)) ? eMultiAccessType.Unknown : ((eMultiAccessType)Enum.Parse(typeof(eMultiAccessType), value));
				result = new MultiAccessInfo
				{
					AccessType = accessType,
					WhoIsAccessingDisplayName = ((xattribute2 == null) ? "" : (xattribute2.Value ?? "")),
					WhoIsAccessingPersonId = NotificationAdapters.ParseIntFromAttribute(attr, 0),
					Context = new MultiAccessContext
					{
						StudentPersonId = NotificationAdapters.ParseIntFromAttribute(attr2, 0),
						ScreenNum = NotificationAdapters.ParseIntFromAttribute(attr4, 0),
						AppointmentId = NotificationAdapters.ParseIntFromAttribute(attr3, 0)
					}
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x00022878 File Offset: 0x00020A78
		private static int ParseIntFromAttribute(XAttribute attr, int defaultValue = 0)
		{
			int result;
			bool flag = attr == null || string.IsNullOrEmpty(attr.Value) || !int.TryParse(attr.Value, out result);
			if (flag)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x000228B4 File Offset: 0x00020AB4
		public static string GetStringFromMultiAccessInfo(this MultiAccessInfo multiAccessInfo)
		{
			bool flag = multiAccessInfo == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				XElement xelement = new XElement("multiaccessinfo", new object[]
				{
					new XAttribute("accesstype", multiAccessInfo.AccessType.ToString()),
					new XAttribute("who", multiAccessInfo.WhoIsAccessingPersonId.ToString()),
					new XAttribute("whoname", multiAccessInfo.WhoIsAccessingDisplayName ?? ""),
					new XElement("context", new object[]
					{
						new XAttribute("pid", (multiAccessInfo.Context == null) ? "" : multiAccessInfo.Context.StudentPersonId.ToString()),
						new XAttribute("appid", (multiAccessInfo.Context == null) ? "" : multiAccessInfo.Context.AppointmentId.ToString()),
						new XAttribute("screennum", (multiAccessInfo.Context == null) ? "" : multiAccessInfo.Context.ScreenNum.ToString())
					})
				});
				result = xelement.ToString();
			}
			return result;
		}
	}
}
