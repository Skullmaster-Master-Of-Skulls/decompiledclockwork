using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x0200045C RID: 1116
	[Serializable]
	public sealed class ClockWorkServerJobWeeklySchedule : ClockWorkServerJobSchedule
	{
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x00025E51 File Offset: 0x00024051
		// (set) Token: 0x060021F3 RID: 8691 RVA: 0x00025E59 File Offset: 0x00024059
		public bool AvoidWeekends { get; set; }

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00025E62 File Offset: 0x00024062
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x00025E6A File Offset: 0x0002406A
		public IList<DayOfWeek> DaysOfWeek { get; set; }

		// Token: 0x060021F6 RID: 8694 RVA: 0x00025E74 File Offset: 0x00024074
		public override bool IsValidRunningDate(DateTime datetime)
		{
			DayOfWeek dayOfWeek = datetime.DayOfWeek;
			return this.DaysOfWeek != null && this.DaysOfWeek.Contains(dayOfWeek) && (!this.AvoidWeekends || (dayOfWeek != DayOfWeek.Saturday && dayOfWeek > DayOfWeek.Sunday));
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00025EC0 File Offset: 0x000240C0
		public override string SaveToXml()
		{
			XName name = "ClockWorkServerJobWeeklySchedule";
			object[] array = new object[2];
			int num = 0;
			XName name2 = "DaysOfWeek";
			object content;
			if (this.DaysOfWeek == null || this.DaysOfWeek.Count <= 0)
			{
				content = null;
			}
			else
			{
				content = this.DaysOfWeek.Select(delegate(DayOfWeek d)
				{
					XName name3 = "Day";
					DayOfWeek dayOfWeek = d;
					return new XElement(name3, dayOfWeek.ToString());
				});
			}
			array[num] = new XElement(name2, content);
			array[1] = new XElement("AvoidWeekends", this.AvoidWeekends);
			return new XElement(name, array).ToString();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00025F60 File Offset: 0x00024160
		public override string ToCron(TimeSpan startTime)
		{
			IEnumerable<DayOfWeek> enumerable;
			if (!this.AvoidWeekends || this.DaysOfWeek == null)
			{
				IEnumerable<DayOfWeek> daysOfWeek = this.DaysOfWeek;
				enumerable = daysOfWeek;
			}
			else
			{
				enumerable = from w in this.DaysOfWeek
				where w != DayOfWeek.Sunday && w != DayOfWeek.Saturday
				select w;
			}
			IEnumerable<DayOfWeek> enumerable2 = enumerable;
			string text;
			if (enumerable2 == null)
			{
				text = "*";
			}
			else
			{
				text = (from w in enumerable2
				select (int)w).ToList<int>().CommaSeparatedValuesWithoutSpace<int>();
			}
			string arg = text;
			return string.Format("{0} {1} * * {2}", startTime.Minutes, startTime.Hours, arg);
		}
	}
}
