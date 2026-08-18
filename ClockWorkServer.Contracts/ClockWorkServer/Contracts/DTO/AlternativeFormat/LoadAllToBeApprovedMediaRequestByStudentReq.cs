using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C3C RID: 3132
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllToBeApprovedMediaRequestByStudentReq : BaseReportMessageReq
	{
		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x000200B4 File Offset: 0x0001E2B4
		// (set) Token: 0x06004183 RID: 16771 RVA: 0x000200BC File Offset: 0x0001E2BC
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x000200C5 File Offset: 0x0001E2C5
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x000200CD File Offset: 0x0001E2CD
		[DataMember]
		public int CampusId { get; set; }
	}
}
