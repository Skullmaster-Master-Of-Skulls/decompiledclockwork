using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x0200045A RID: 1114
	[Serializable]
	public abstract class ClockWorkServerJobSchedule : IClockWorkServerJobSchedule
	{
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x00025A76 File Offset: 0x00023C76
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x00025A7E File Offset: 0x00023C7E
		public TimeSpan StartTime { get; set; }

		// Token: 0x060021E5 RID: 8677
		public abstract bool IsValidRunningDate(DateTime datetime);

		// Token: 0x060021E6 RID: 8678
		public abstract string SaveToXml();

		// Token: 0x060021E7 RID: 8679
		public abstract string ToCron(TimeSpan startTime);

		// Token: 0x060021E8 RID: 8680 RVA: 0x00025A88 File Offset: 0x00023C88
		public static ClockWorkServerJobSchedule FromXml(string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			ClockWorkServerJobSchedule result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TextReader textReader = new StringReader(xml);
				XDocument xdocument = XDocument.Load(textReader);
				XElement root = xdocument.Root;
				bool flag2 = root == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Type type = Type.GetType("TechnoPro.Common.Public.Entities.ClockWorkServerJob." + root.Name.LocalName);
					bool flag3 = type == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						ClockWorkServerJobSchedule clockWorkServerJobSchedule = (ClockWorkServerJobSchedule)Activator.CreateInstance(type);
						bool flag4 = clockWorkServerJobSchedule is ClockWorkServerJobDailySchedule;
						if (flag4)
						{
							ClockWorkServerJobDailySchedule clockWorkServerJobDailySchedule = (ClockWorkServerJobDailySchedule)clockWorkServerJobSchedule;
							XElement xelement = root.Element("AvoidWeekends");
							bool flag5;
							clockWorkServerJobDailySchedule.AvoidWeekends = (xelement != null && bool.TryParse(xelement.Value, out flag5) && flag5);
							result = clockWorkServerJobDailySchedule;
						}
						else
						{
							bool flag6 = clockWorkServerJobSchedule is ClockWorkServerJobWeeklySchedule;
							if (flag6)
							{
								ClockWorkServerJobWeeklySchedule clockWorkServerJobWeeklySchedule = (ClockWorkServerJobWeeklySchedule)clockWorkServerJobSchedule;
								XElement xelement2 = root.Element("AvoidWeekends");
								bool flag7;
								clockWorkServerJobWeeklySchedule.AvoidWeekends = (xelement2 != null && bool.TryParse(xelement2.Value, out flag7) && flag7);
								XElement xelement3 = root.Element("DaysOfWeek");
								bool flag8 = xelement3 != null;
								if (flag8)
								{
									IEnumerable<XElement> source = xelement3.Elements("Day");
									clockWorkServerJobWeeklySchedule.DaysOfWeek = (from xDay in source
									where Enum.IsDefined(typeof(DayOfWeek), xDay.Value)
									select (DayOfWeek)Enum.Parse(typeof(DayOfWeek), xDay.Value)).ToList<DayOfWeek>();
								}
								result = clockWorkServerJobWeeklySchedule;
							}
							else
							{
								bool flag9 = clockWorkServerJobSchedule is ClockWorkServerJobMonthlySchedule;
								if (flag9)
								{
									ClockWorkServerJobMonthlySchedule clockWorkServerJobMonthlySchedule = (ClockWorkServerJobMonthlySchedule)clockWorkServerJobSchedule;
									XElement xelement4 = root.Element("DaysOfMonth");
									clockWorkServerJobMonthlySchedule.DaysOfMonth = ((xelement4 != null && !string.IsNullOrEmpty(xelement4.Value)) ? xelement4.Value.SplitIntValues() : null);
									XElement xelement5 = root.Element("MonthsOfYear");
									clockWorkServerJobMonthlySchedule.MonthsOfYear = ((xelement5 != null && !string.IsNullOrEmpty(xelement5.Value)) ? xelement5.Value.SplitIntValues() : null);
									result = clockWorkServerJobMonthlySchedule;
								}
								else
								{
									result = null;
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}
