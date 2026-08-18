using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x0200045B RID: 1115
	[Serializable]
	public sealed class ClockWorkServerJobMonthlySchedule : ClockWorkServerJobSchedule
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x00025CE4 File Offset: 0x00023EE4
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x00025CEC File Offset: 0x00023EEC
		public IList<int> DaysOfMonth { get; set; }

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x00025CF5 File Offset: 0x00023EF5
		// (set) Token: 0x060021ED RID: 8685 RVA: 0x00025CFD File Offset: 0x00023EFD
		public IList<int> MonthsOfYear { get; set; }

		// Token: 0x060021EE RID: 8686 RVA: 0x00025D08 File Offset: 0x00023F08
		public override bool IsValidRunningDate(DateTime datetime)
		{
			return this.DaysOfMonth != null && this.DaysOfMonth.Contains(datetime.Day) && this.MonthsOfYear != null && this.MonthsOfYear.Contains(datetime.Month);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00025D54 File Offset: 0x00023F54
		public override string SaveToXml()
		{
			return new XElement("ClockWorkServerJobMonthlySchedule", new object[]
			{
				new XElement("MonthsOfYear", (this.MonthsOfYear != null && this.MonthsOfYear.Count > 0) ? this.MonthsOfYear.CommaSeparatedValuesWithoutSpace<int>() : null),
				new XElement("DaysOfMonth", (this.DaysOfMonth != null && this.DaysOfMonth.Count > 0) ? this.DaysOfMonth.CommaSeparatedValuesWithoutSpace<int>() : null)
			}).ToString();
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00025DEC File Offset: 0x00023FEC
		public override string ToCron(TimeSpan startTime)
		{
			return string.Format("{0} {1} {2} {3} *", new object[]
			{
				startTime.Minutes,
				startTime.Hours,
				this.DaysOfMonth.CommaSeparatedValuesWithoutSpace<int>(),
				this.MonthsOfYear.CommaSeparatedValuesWithoutSpace<int>()
			});
		}
	}
}
