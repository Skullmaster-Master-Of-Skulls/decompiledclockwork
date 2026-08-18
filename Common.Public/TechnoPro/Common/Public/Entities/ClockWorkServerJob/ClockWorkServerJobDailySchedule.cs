using System;
using System.Xml.Linq;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x0200045D RID: 1117
	[Serializable]
	public sealed class ClockWorkServerJobDailySchedule : ClockWorkServerJobSchedule
	{
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00026014 File Offset: 0x00024214
		// (set) Token: 0x060021FB RID: 8699 RVA: 0x0002601C File Offset: 0x0002421C
		public bool AvoidWeekends { get; set; }

		// Token: 0x060021FC RID: 8700 RVA: 0x00026028 File Offset: 0x00024228
		public override bool IsValidRunningDate(DateTime datetime)
		{
			DayOfWeek dayOfWeek = datetime.DayOfWeek;
			return !this.AvoidWeekends || (dayOfWeek != DayOfWeek.Saturday && dayOfWeek > DayOfWeek.Sunday);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00026058 File Offset: 0x00024258
		public override string SaveToXml()
		{
			return new XElement("ClockWorkServerJobDailySchedule", new XElement("AvoidWeekends", this.AvoidWeekends)).ToString();
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00026098 File Offset: 0x00024298
		public override string ToCron(TimeSpan startTime)
		{
			string arg = this.AvoidWeekends ? "1-5" : "*";
			return string.Format("{0} {1} * * {2}", startTime.Minutes, startTime.Hours, arg);
		}
	}
}
