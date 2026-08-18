using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004AF RID: 1199
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeValueAccommodationDataDTO : MailMergeValueBaseDTO
	{
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0000BCCD File Offset: 0x00009ECD
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x0000BCD5 File Offset: 0x00009ED5
		[DataMember]
		public AccommodationDataDTO Value { get; set; }

		// Token: 0x06001994 RID: 6548 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public override object GetValueObject()
		{
			return this.Value;
		}
	}
}
