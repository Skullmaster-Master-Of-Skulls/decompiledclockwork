using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x0200025C RID: 604
	[DataContract(Namespace = "http://tpro.ca")]
	public class SelfRegCheckedAccommodationDTO
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00006820 File Offset: 0x00004A20
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x00006828 File Offset: 0x00004A28
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00006831 File Offset: 0x00004A31
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x00006839 File Offset: 0x00004A39
		[DataMember]
		public bool IsChecked { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00006842 File Offset: 0x00004A42
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0000684A File Offset: 0x00004A4A
		[DataMember]
		public string Text { get; set; }
	}
}
