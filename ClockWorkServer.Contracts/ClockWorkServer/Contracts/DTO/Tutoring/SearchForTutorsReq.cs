using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000196 RID: 406
	[DataContract(Namespace = "http://tpro.ca")]
	public class SearchForTutorsReq : BaseMessageReq
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00004493 File Offset: 0x00002693
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x0000449B File Offset: 0x0000269B
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x000044A4 File Offset: 0x000026A4
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x000044AC File Offset: 0x000026AC
		[DataMember]
		public string SearchString { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x000044B5 File Offset: 0x000026B5
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x000044BD File Offset: 0x000026BD
		[DataMember]
		public int MaxReturnResults { get; set; }
	}
}
