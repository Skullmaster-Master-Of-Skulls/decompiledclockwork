using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B7 RID: 1207
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueIntDTO : MailMergeValueBaseDTO
	{
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0000BE2C File Offset: 0x0000A02C
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x0000BE34 File Offset: 0x0000A034
		[DataMember]
		public int Value { get; set; }

		// Token: 0x060019B2 RID: 6578 RVA: 0x0000BE40 File Offset: 0x0000A040
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
