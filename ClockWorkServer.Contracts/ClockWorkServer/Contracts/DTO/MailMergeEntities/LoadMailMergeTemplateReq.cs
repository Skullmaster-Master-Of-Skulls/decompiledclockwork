using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x020004A6 RID: 1190
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMailMergeTemplateReq : BaseReportMessageReq
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0000BBCE File Offset: 0x00009DCE
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0000BBD6 File Offset: 0x00009DD6
		[DataMember]
		public int TemplateId { get; set; }
	}
}
