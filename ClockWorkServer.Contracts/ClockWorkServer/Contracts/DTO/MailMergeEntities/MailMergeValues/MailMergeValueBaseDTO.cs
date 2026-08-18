using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues
{
	// Token: 0x020004B0 RID: 1200
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(MailMergeValueAccommodationDataDTO))]
	[KnownType(typeof(MailMergeValueDynamicDataDTO))]
	[KnownType(typeof(MailMergeValueStringDTO))]
	[KnownType(typeof(MailMergeValueBoolDTO))]
	[KnownType(typeof(MailMergeValueByteArrayDTO))]
	[KnownType(typeof(MailMergeValueDoubleDTO))]
	[KnownType(typeof(MailMergeValueIntDTO))]
	[KnownType(typeof(MailMergeValueDateTimeDTO))]
	[KnownType(typeof(MailMergeValueDateTimeNullableDTO))]
	public class MailMergeValueBaseDTO
	{
		// Token: 0x06001997 RID: 6551 RVA: 0x0000BD04 File Offset: 0x00009F04
		public virtual object GetValueObject()
		{
			return null;
		}
	}
}
