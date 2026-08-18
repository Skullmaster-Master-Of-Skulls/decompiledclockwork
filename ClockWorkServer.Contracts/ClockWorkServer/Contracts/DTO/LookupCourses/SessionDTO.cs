using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000811 RID: 2065
	[DataContract(Namespace = "http://tpro.ca")]
	public class SessionDTO : ICloneable<SessionDTO>, ICloneable
	{
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x00013F08 File Offset: 0x00012108
		// (set) Token: 0x06002A08 RID: 10760 RVA: 0x00013F10 File Offset: 0x00012110
		[DataMember]
		public AcademicTermDTO AcademicTerm { get; set; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x00013F19 File Offset: 0x00012119
		// (set) Token: 0x06002A0A RID: 10762 RVA: 0x00013F21 File Offset: 0x00012121
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x00013F2A File Offset: 0x0001212A
		// (set) Token: 0x06002A0C RID: 10764 RVA: 0x00013F32 File Offset: 0x00012132
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x06002A0D RID: 10765 RVA: 0x000036BD File Offset: 0x000018BD
		public SessionDTO()
		{
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00013F3C File Offset: 0x0001213C
		public SessionDTO(SessionDTO session)
		{
			bool flag = session == null;
			if (!flag)
			{
				this.AcademicTerm = session.AcademicTerm.Clone();
				this.StartDate = session.StartDate;
				this.EndDate = session.EndDate;
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00013F88 File Offset: 0x00012188
		public SessionDTO Clone()
		{
			return new SessionDTO(this);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00013FA0 File Offset: 0x000121A0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
