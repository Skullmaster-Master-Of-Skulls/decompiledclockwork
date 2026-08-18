using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000499 RID: 1177
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMailMergeCodeDefinitionsForDisplayResp
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0000BA9C File Offset: 0x00009C9C
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		[DataMember]
		public string DisplayString { get; set; }
	}
}
