using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebGrease.Extensions
{
	// Token: 0x020000FD RID: 253
	internal static class JsonExtensions
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x000498F4 File Offset: 0x00047AF4
		internal static T FromJson<T>(this string json, bool nonPublic = false)
		{
			return JsonConvert.DeserializeObject<T>(json, JsonExtensions.GetJsonSerializationSettings(nonPublic));
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00049902 File Offset: 0x00047B02
		internal static string ToJson(this object value, bool nonPublic = false)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, JsonExtensions.GetJsonSerializationSettings(nonPublic));
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00049911 File Offset: 0x00047B11
		private static JsonSerializerSettings GetJsonSerializationSettings(bool nonPublic)
		{
			if (!nonPublic)
			{
				return JsonExtensions.DefaultJsonSerializerSettings;
			}
			return JsonExtensions.JsonSerializerSettings.Value;
		}

		// Token: 0x0400065F RID: 1631
		private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings();

		// Token: 0x04000660 RID: 1632
		private static readonly Lazy<JsonSerializerSettings> JsonSerializerSettings = new Lazy<JsonSerializerSettings>(delegate()
		{
			DefaultContractResolver defaultContractResolver = new DefaultContractResolver();
			defaultContractResolver.DefaultMembersSearchFlags |= BindingFlags.NonPublic;
			return new JsonSerializerSettings
			{
				ContractResolver = defaultContractResolver
			};
		});
	}
}
