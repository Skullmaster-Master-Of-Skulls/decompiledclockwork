using System;
using TechnoPro.Common.Public.Entities.Adapters;

namespace ClockWorkAPI.Exams
{
	// Token: 0x02000035 RID: 53
	public class ClassTestDefinition
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00010340 File Offset: 0x0000F340
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00010357 File Offset: 0x0000F357
		public virtual int ExamId { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00010360 File Offset: 0x0000F360
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00010377 File Offset: 0x0000F377
		public virtual DateTime DateOfTest { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010380 File Offset: 0x0000F380
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00010397 File Offset: 0x0000F397
		public virtual int DurationMinutes { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000103A0 File Offset: 0x0000F3A0
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000103B7 File Offset: 0x0000F3B7
		public virtual string Description { get; set; }

		// Token: 0x060002AA RID: 682 RVA: 0x000103CC File Offset: 0x0000F3CC
		public override string ToString()
		{
			return string.Format("{0} {1} ({2})", this.DateOfTest.ToString("yyyy-MM-dd"), this.DateOfTest.ToString("h:mm tt"), this.DurationMinutes.GetDurationDescription());
		}
	}
}
