using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x0200046F RID: 1135
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueFormatDTO
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0000B519 File Offset: 0x00009719
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x0000B521 File Offset: 0x00009721
		[DataMember]
		public string CustomFormat { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0000B52A File Offset: 0x0000972A
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0000B532 File Offset: 0x00009732
		[DataMember]
		public eValueFormatTypeDTO ValueFormatType { get; set; }
	}
}
