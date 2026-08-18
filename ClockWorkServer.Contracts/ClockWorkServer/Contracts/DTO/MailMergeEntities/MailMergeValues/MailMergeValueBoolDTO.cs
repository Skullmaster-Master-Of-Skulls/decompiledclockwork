using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B1 RID: 1201
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueBoolDTO : MailMergeValueBaseDTO
	{
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0000BD17 File Offset: 0x00009F17
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x0000BD1F File Offset: 0x00009F1F
		[DataMember]
		public bool Value { get; set; }

		// Token: 0x0600199A RID: 6554 RVA: 0x0000BD28 File Offset: 0x00009F28
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
