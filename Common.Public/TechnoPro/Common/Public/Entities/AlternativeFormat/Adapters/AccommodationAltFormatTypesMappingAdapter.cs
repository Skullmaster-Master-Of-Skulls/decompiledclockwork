using System;
using Newtonsoft.Json;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters
{
	// Token: 0x0200059A RID: 1434
	public static class AccommodationAltFormatTypesMappingAdapter
	{
		// Token: 0x06002EAF RID: 11951 RVA: 0x00033338 File Offset: 0x00031538
		public static string SerializeAccommodationAltFormatTypesMappings(this AccommodationAltFormatTypesMapping[] mappings)
		{
			bool flag = mappings == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = JsonConvert.SerializeObject(mappings);
			}
			return result;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x00033360 File Offset: 0x00031560
		public static AccommodationAltFormatTypesMapping[] DeSerializeAccommodationALtFormatTypesMappings(this string serializedMappings)
		{
			bool flag = string.IsNullOrWhiteSpace(serializedMappings);
			AccommodationAltFormatTypesMapping[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = JsonConvert.DeserializeObject<AccommodationAltFormatTypesMapping[]>(serializedMappings);
			}
			return result;
		}
	}
}
