using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B59 RID: 2905
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableMediaContentFileByStudentIdReq : BaseMessageReq
	{
		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x0001E4CC File Offset: 0x0001C6CC
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x0001E4DD File Offset: 0x0001C6DD
		// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x0001E4E5 File Offset: 0x0001C6E5
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x0001E4EE File Offset: 0x0001C6EE
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x0001E4F6 File Offset: 0x0001C6F6
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
