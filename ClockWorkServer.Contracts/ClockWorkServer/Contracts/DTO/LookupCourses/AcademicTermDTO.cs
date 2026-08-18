using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200078B RID: 1931
	[DataContract(Namespace = "http://tpro.ca")]
	public class AcademicTermDTO : ICloneable<AcademicTermDTO>, ICloneable
	{
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00012AE0 File Offset: 0x00010CE0
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x00012AE8 File Offset: 0x00010CE8
		[DataMember]
		public DateTime StartMonthDay { get; set; }

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x00012AF1 File Offset: 0x00010CF1
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x00012AF9 File Offset: 0x00010CF9
		[DataMember]
		public DateTime EndMonthDay { get; set; }

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00012B02 File Offset: 0x00010D02
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x00012B0A File Offset: 0x00010D0A
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x00012B13 File Offset: 0x00010D13
		// (set) Token: 0x060027BC RID: 10172 RVA: 0x00012B1B File Offset: 0x00010D1B
		[DataMember]
		public int TermId { get; set; }

		// Token: 0x060027BD RID: 10173 RVA: 0x000036BD File Offset: 0x000018BD
		public AcademicTermDTO()
		{
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00012B24 File Offset: 0x00010D24
		public AcademicTermDTO(AcademicTermDTO term)
		{
			bool flag = term == null;
			if (!flag)
			{
				this.StartMonthDay = term.StartMonthDay;
				this.EndMonthDay = term.EndMonthDay;
				this.Title = term.Title;
				this.TermId = term.TermId;
			}
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00012B78 File Offset: 0x00010D78
		public AcademicTermDTO Clone()
		{
			return new AcademicTermDTO(this);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00012B90 File Offset: 0x00010D90
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
