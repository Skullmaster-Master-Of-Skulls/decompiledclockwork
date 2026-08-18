using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B2 RID: 1202
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueByteArrayDTO : MailMergeValueBaseDTO
	{
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0000BD45 File Offset: 0x00009F45
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x0000BD4D File Offset: 0x00009F4D
		[DataMember]
		public byte[] Value { get; set; }

		// Token: 0x0600199E RID: 6558 RVA: 0x0000BD58 File Offset: 0x00009F58
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
