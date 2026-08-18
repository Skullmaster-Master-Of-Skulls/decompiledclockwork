using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D8 RID: 2520
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestBaseDTO : ICloneable<ClassTestBaseDTO>, ICloneable
	{
		// Token: 0x06003463 RID: 13411 RVA: 0x000036BD File Offset: 0x000018BD
		public ClassTestBaseDTO()
		{
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x00019760 File Offset: 0x00017960
		public ClassTestBaseDTO(ClassTestBaseDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ExamId = item.ExamId;
				this.StartDateTime = item.StartDateTime;
				this.EndDateTime = item.EndDateTime;
				LookupCourseBaseDTO course = item.Course;
				this.Course = ((course != null) ? course.Clone() : null);
				this.Location = item.Location;
				this.ExternalExamId = item.ExternalExamId;
				this.ExamType = item.ExamType;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000197E6 File Offset: 0x000179E6
		// (set) Token: 0x06003466 RID: 13414 RVA: 0x000197EE File Offset: 0x000179EE
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000197F7 File Offset: 0x000179F7
		// (set) Token: 0x06003468 RID: 13416 RVA: 0x000197FF File Offset: 0x000179FF
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x00019808 File Offset: 0x00017A08
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x00019810 File Offset: 0x00017A10
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x00019819 File Offset: 0x00017A19
		// (set) Token: 0x0600346C RID: 13420 RVA: 0x00019821 File Offset: 0x00017A21
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x0001982A File Offset: 0x00017A2A
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x00019832 File Offset: 0x00017A32
		[DataMember]
		public string Location { get; set; }

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x0001983B File Offset: 0x00017A3B
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x00019843 File Offset: 0x00017A43
		[DataMember]
		public string ExternalExamId { get; set; }

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x0001984C File Offset: 0x00017A4C
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x00019854 File Offset: 0x00017A54
		[DataMember]
		public eClassTestType ExamType { get; set; }

		// Token: 0x06003473 RID: 13427 RVA: 0x00019860 File Offset: 0x00017A60
		public ClassTestBaseDTO Clone()
		{
			return new ClassTestBaseDTO(this);
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00019878 File Offset: 0x00017A78
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
