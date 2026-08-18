using System;
using Google.Apis.Util;
using Newtonsoft.Json;

namespace Google.Apis.Json
{
	// Token: 0x02000021 RID: 33
	public class RFC3339DateTimeConverter : JsonConverter
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000036DB File Offset: 0x000018DB
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because CanRead is false.");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000036E7 File Offset: 0x000018E7
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003710 File Offset: 0x00001910
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value != null)
			{
				DateTime date = (DateTime)value;
				serializer.Serialize(writer, Utilities.ConvertToRFC3339(date));
			}
		}
	}
}
