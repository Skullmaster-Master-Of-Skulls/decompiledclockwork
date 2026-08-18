using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Google.Apis.Json
{
	// Token: 0x02000022 RID: 34
	public class ExplicitNullConverter : JsonConverter
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000036C8 File Offset: 0x000018C8
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000373C File Offset: 0x0000193C
		public override bool CanConvert(Type objectType)
		{
			return objectType.GetTypeInfo().GetCustomAttributes(typeof(JsonExplicitNullAttribute), false).Any<object>();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036DB File Offset: 0x000018DB
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("Unnecessary because CanRead is false.");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003759 File Offset: 0x00001959
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteNull();
		}
	}
}
