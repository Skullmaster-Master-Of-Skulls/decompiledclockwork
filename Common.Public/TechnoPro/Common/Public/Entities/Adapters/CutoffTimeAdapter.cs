using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005BC RID: 1468
	public static class CutoffTimeAdapter
	{
		// Token: 0x06002F5E RID: 12126 RVA: 0x00034F14 File Offset: 0x00033114
		public static CutoffTime CutoffTimeFromXElement(this XElement xe)
		{
			bool flag = xe == null;
			CutoffTime result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XAttribute xattribute = xe.Attribute("enabled");
				XAttribute xattribute2 = xe.Attribute("amount");
				XAttribute xattribute3 = xe.Attribute("interval");
				int num = (xattribute3 == null) ? 0 : (xattribute3.Value ?? "").GetIntFromString(0);
				eTimeInterval interval = (eTimeInterval)(Enum.IsDefined(typeof(eTimeInterval), num) ? num : 1);
				result = new CutoffTime
				{
					Enabled = (xattribute != null && "1yestrue".IndexOf(xattribute.Value ?? "", StringComparison.Ordinal) >= 0),
					Amount = ((xattribute2 == null) ? 0 : (xattribute2.Value ?? "").GetIntFromString(0)),
					Interval = interval
				};
			}
			return result;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x00035004 File Offset: 0x00033204
		public static CutoffTime CutoffTimeFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			CutoffTime result;
			if (flag)
			{
				result = new CutoffTime
				{
					Enabled = false
				};
			}
			else
			{
				int amount;
				bool flag2 = !xml.Contains("<cutofftimes>") && !string.IsNullOrEmpty(xml) && int.TryParse(xml, out amount);
				if (flag2)
				{
					result = new CutoffTime
					{
						Enabled = true,
						Amount = amount,
						Interval = eTimeInterval.Days
					};
				}
				else
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xml);
					XPathNavigator xpathNavigator = xmlDocument.CreateNavigator();
					xpathNavigator.MoveToRoot();
					CutoffTime cutoffTime = new CutoffTime();
					bool flag3 = xpathNavigator.HasChildren && xpathNavigator.MoveToFirstChild() && xpathNavigator.HasChildren && xpathNavigator.MoveToFirstChild() && xpathNavigator.HasAttributes && xpathNavigator.MoveToFirstAttribute();
					if (flag3)
					{
						do
						{
							string text = xpathNavigator.Name.ToLower();
							string value = xpathNavigator.Value;
							string text2 = text;
							string a = text2;
							if (!(a == "enabled"))
							{
								if (!(a == "amount"))
								{
									if (a == "interval")
									{
										int num;
										bool flag4 = int.TryParse(value, out num) && Enum.IsDefined(typeof(eTimeInterval), num);
										if (flag4)
										{
											cutoffTime.Interval = (eTimeInterval)num;
										}
									}
								}
								else
								{
									int num;
									cutoffTime.Amount = ((!int.TryParse(value, out num)) ? 0 : num);
								}
							}
							else
							{
								cutoffTime.Enabled = ("1yestrue".IndexOf(value, StringComparison.Ordinal) >= 0);
							}
						}
						while (xpathNavigator.MoveToNextAttribute());
					}
					result = cutoffTime;
				}
			}
			return result;
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000351A8 File Offset: 0x000333A8
		public static XElement CutoffTimeToXmlElement(this CutoffTime cutoffTime)
		{
			XElement result = new XElement("cutofftime");
			cutoffTime.CutoffTimeToXElement(ref result);
			return result;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000351D4 File Offset: 0x000333D4
		public static void CutoffTimeToXElement(this CutoffTime cutoffTime, ref XElement cutofftimeElement)
		{
			cutofftimeElement.Add(new object[]
			{
				new XAttribute("enabled", (cutoffTime == null) ? "0" : (cutoffTime.Enabled ? "1" : "0")),
				new XAttribute("amount", ((cutoffTime != null) ? cutoffTime.Amount.ToString() : null) ?? ""),
				new XAttribute("interval", (cutoffTime == null) ? "" : ((int)cutoffTime.Interval).ToString())
			});
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0003527C File Offset: 0x0003347C
		public static string CutoffTimeToXml(this CutoffTime cutoffTime)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml("<cutofftimes></cutofftimes>");
			XmlElement xmlElement = xmlDocument.CreateElement("cutofftime");
			XmlNode newChild = xmlElement;
			xmlDocument.DocumentElement.AppendChild(newChild);
			CutoffTimeAdapter.AppendXmlAttribute(xmlDocument, ref newChild, "enabled", cutoffTime.Enabled ? "1" : "0");
			CutoffTimeAdapter.AppendXmlAttribute(xmlDocument, ref newChild, "amount", cutoffTime.Amount.ToString());
			CutoffTimeAdapter.AppendXmlAttribute(xmlDocument, ref newChild, "interval", ((int)cutoffTime.Interval).ToString());
			StringBuilder stringBuilder = new StringBuilder();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new StringWriter(stringBuilder));
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlDocument.WriteTo(xmlTextWriter);
			xmlTextWriter.Flush();
			return stringBuilder.ToString();
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0003534C File Offset: 0x0003354C
		private static void AppendXmlAttribute(XmlDocument doc, ref XmlNode node, string attributeTitle, string attributeValue)
		{
			XmlAttribute xmlAttribute = doc.CreateAttribute(attributeTitle);
			xmlAttribute.Value = attributeValue;
			XmlAttributeCollection attributes = node.Attributes;
			if (attributes != null)
			{
				attributes.Append(xmlAttribute);
			}
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x00035380 File Offset: 0x00033580
		public static bool? IsRightNowBeforeCutoffTime(this CutoffTime cutoffTime, DateTime contextDateTime)
		{
			DateTime? minimumDateForBeforeTypeCutoff = cutoffTime.GetMinimumDateForBeforeTypeCutoff();
			bool flag = minimumDateForBeforeTypeCutoff == null;
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new bool?(contextDateTime >= minimumDateForBeforeTypeCutoff.Value);
			}
			return result;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000353C8 File Offset: 0x000335C8
		public static DateTime? GetMinimumDateForBeforeTypeCutoff(this CutoffTime cutoffTime)
		{
			DateTime? result;
			if (!cutoffTime.Enabled)
			{
				DateTime? dateTime = null;
				result = dateTime;
			}
			else
			{
				DateTime? dateTime = CutoffTimeAdapter.AddOrSubtractDate(cutoffTime.Interval, cutoffTime.Amount, -1);
				result = ((dateTime != null) ? new DateTime?(dateTime.GetValueOrDefault().AddSeconds(1.0)) : null);
			}
			return result;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x00035434 File Offset: 0x00033634
		public static DateTime? GetMaximumDateForAfterTypeCutoff(this CutoffTime cutoffTime)
		{
			return cutoffTime.Enabled ? CutoffTimeAdapter.AddOrSubtractDate(cutoffTime.Interval, cutoffTime.Amount, 1) : null;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x0003546C File Offset: 0x0003366C
		public static string GetCutoffTimeDescription(this CutoffTime cutoffTime)
		{
			bool flag = cutoffTime == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = string.Format("{0} {1}(s)", cutoffTime.Amount, cutoffTime.Interval);
			}
			return result;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000354B0 File Offset: 0x000336B0
		private static DateTime? ProcessDateResult(DateTime dt, int direction, bool ignoreTimeComponent)
		{
			bool flag = !ignoreTimeComponent;
			DateTime? result;
			if (flag)
			{
				result = new DateTime?(dt);
			}
			else
			{
				dt = ((direction < 1) ? dt.AddSeconds(-1.0) : dt.AddDays(1.0).AddSeconds(-1.0));
				result = new DateTime?(dt);
			}
			return result;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x00035514 File Offset: 0x00033714
		private static DateTime? AddOrSubtractDate(eTimeInterval theInterval, int amount, int direction)
		{
			bool flag = theInterval == eTimeInterval.Hours;
			if (flag)
			{
				theInterval = eTimeInterval.Minutes;
				amount *= 60;
			}
			TimeIntervalAttribute attribute = theInterval.GetAttribute<TimeIntervalAttribute>();
			bool flag2 = attribute != null && attribute.IgnoreTimeComponent;
			DateTime date = flag2 ? DateTime.Now.Date : DateTime.Now;
			DateTime? result;
			switch (theInterval)
			{
			case eTimeInterval.Days:
				result = CutoffTimeAdapter.ProcessDateResult(date.AddDays((double)(amount * -(double)direction)), direction, flag2);
				break;
			case eTimeInterval.WeekDays:
				result = CutoffTimeAdapter.ProcessDateResult(CutoffTimeAdapter.AddWeekdays(date, amount * -direction), direction, flag2);
				break;
			case eTimeInterval.Months:
				result = CutoffTimeAdapter.ProcessDateResult(date.AddMonths(amount * -direction), direction, flag2);
				break;
			case eTimeInterval.Years:
				result = CutoffTimeAdapter.ProcessDateResult(date.AddYears(amount * -direction), direction, flag2);
				break;
			case eTimeInterval.Minutes:
				result = CutoffTimeAdapter.ProcessDateResult(date.AddMinutes((double)(amount * -(double)direction)), direction, flag2);
				break;
			default:
				throw new ArgumentOutOfRangeException("theInterval", theInterval, null);
			}
			return result;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00035610 File Offset: 0x00033810
		private static DateTime AddWeekdays(DateTime date, int numWeekdays)
		{
			DateTime result = date.Date;
			int num = (numWeekdays < 0) ? -1 : 1;
			int i = Math.Abs(numWeekdays);
			while (i > 0)
			{
				result = result.AddDays((double)num);
				DayOfWeek dayOfWeek = result.DayOfWeek;
				bool flag = dayOfWeek != DayOfWeek.Saturday && dayOfWeek > DayOfWeek.Sunday;
				if (flag)
				{
					i--;
				}
			}
			return result;
		}
	}
}
