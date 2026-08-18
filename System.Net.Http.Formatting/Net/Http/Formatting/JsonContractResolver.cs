using System;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003D RID: 61
	public class JsonContractResolver : DefaultContractResolver
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000885D File Offset: 0x00006A5D
		public JsonContractResolver(MediaTypeFormatter formatter)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this._formatter = formatter;
			base.IgnoreSerializableAttribute = false;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008884 File Offset: 0x00006A84
		private void ConfigureProperty(MemberInfo member, JsonProperty property)
		{
			if (this._formatter.RequiredMemberSelector != null && this._formatter.RequiredMemberSelector.IsRequiredMember(member))
			{
				property.Required = Required.AllowNull;
				property.DefaultValueHandling = new DefaultValueHandling?(DefaultValueHandling.Include);
				property.NullValueHandling = new NullValueHandling?(NullValueHandling.Include);
				return;
			}
			property.Required = Required.Default;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000088D8 File Offset: 0x00006AD8
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
			this.ConfigureProperty(member, jsonProperty);
			return jsonProperty;
		}

		// Token: 0x04000097 RID: 151
		private readonly MediaTypeFormatter _formatter;
	}
}
