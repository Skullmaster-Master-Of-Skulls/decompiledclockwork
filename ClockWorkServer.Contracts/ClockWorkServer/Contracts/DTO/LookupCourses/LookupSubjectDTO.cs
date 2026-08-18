using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007FE RID: 2046
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupSubjectDTO : ICloneable<LookupSubjectDTO>, ICloneable
	{
		// Token: 0x060029B9 RID: 10681 RVA: 0x000036BD File Offset: 0x000018BD
		public LookupSubjectDTO()
		{
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00013CA8 File Offset: 0x00011EA8
		public LookupSubjectDTO(LookupSubjectDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.SubjectId = item.SubjectId;
				this.SubjectCode = item.SubjectCode;
				this.SubjectDescription = item.SubjectDescription;
				this.SubjectEmail = item.SubjectEmail;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x00013CFB File Offset: 0x00011EFB
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x00013D03 File Offset: 0x00011F03
		[DataMember]
		public int SubjectId { get; set; }

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x00013D0C File Offset: 0x00011F0C
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x00013D14 File Offset: 0x00011F14
		[DataMember]
		public string SubjectCode { get; set; }

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x00013D1D File Offset: 0x00011F1D
		// (set) Token: 0x060029C0 RID: 10688 RVA: 0x00013D25 File Offset: 0x00011F25
		[DataMember]
		public string SubjectDescription { get; set; }

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x00013D2E File Offset: 0x00011F2E
		// (set) Token: 0x060029C2 RID: 10690 RVA: 0x00013D36 File Offset: 0x00011F36
		[DataMember]
		public string SubjectEmail { get; set; }

		// Token: 0x060029C3 RID: 10691 RVA: 0x00013D40 File Offset: 0x00011F40
		public LookupSubjectDTO Clone()
		{
			return new LookupSubjectDTO(this);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00013D58 File Offset: 0x00011F58
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
