using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace TechnoPro.Common.ClientManager.Notifications.Entities
{
	// Token: 0x0200001A RID: 26
	public static class MessagingSerializers
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000032E8 File Offset: 0x000014E8
		public static string GetXmlFromMessageAppointmentsParameters(this List<MessageAppointmentsParameter> items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
			stringBuilder.Append("<appointments>\n");
			foreach (MessageAppointmentsParameter messageAppointmentsParameter in items)
			{
				stringBuilder.Append("  <appointment>\n");
				stringBuilder.AppendFormat("    <id>{0}</id>\n", messageAppointmentsParameter.AppointmentId.ToString());
				stringBuilder.AppendFormat("    <startdate>{0}</startdate>\n", messageAppointmentsParameter.StartDate.ToString("yyyy-MM-dd H:mm"));
				stringBuilder.AppendFormat("    <pids>{0}</pids>\n", string.Join(",", messageAppointmentsParameter.PersonIds.ConvertAll<string>((int f) => f.ToString()).ToArray()));
				stringBuilder.Append("  </appointment>");
			}
			stringBuilder.Append("</appointments>");
			return stringBuilder.ToString();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000033FC File Offset: 0x000015FC
		public static List<MessageAppointmentsParameter> GetMessageAppointmentsParametersFromXml(this string xml)
		{
			List<MessageAppointmentsParameter> list = new List<MessageAppointmentsParameter>();
			if (string.IsNullOrEmpty(xml))
			{
				return list;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.CreateNavigator().Select("appointments/appointment"))
			{
				XPathNavigator xpathNavigator = (XPathNavigator)obj;
				xpathNavigator.MoveToFirstChild();
				MessageAppointmentsParameter messageAppointmentsParameter = new MessageAppointmentsParameter
				{
					PersonIds = new List<int>()
				};
				do
				{
					string name = xpathNavigator.Name;
					string value = xpathNavigator.Value;
					if (name.Equals("id"))
					{
						int appointmentId;
						if (int.TryParse(value, out appointmentId))
						{
							messageAppointmentsParameter.AppointmentId = appointmentId;
						}
					}
					else if (name.Equals("startdate"))
					{
						DateTime startDate;
						if (DateTime.TryParse(value, out startDate))
						{
							messageAppointmentsParameter.StartDate = startDate;
						}
					}
					else if (name.Equals("pids"))
					{
						string[] array = value.Split(new char[]
						{
							','
						});
						for (int i = 0; i < array.Length; i++)
						{
							int item;
							if (int.TryParse(array[i], out item))
							{
								messageAppointmentsParameter.PersonIds.Add(item);
							}
						}
					}
				}
				while (xpathNavigator.MoveToNext());
				list.Add(messageAppointmentsParameter);
			}
			return list;
		}
	}
}
