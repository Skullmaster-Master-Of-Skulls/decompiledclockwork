using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.Net.Http
{
	// Token: 0x02000073 RID: 115
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class UriExtensions
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000FB11 File Offset: 0x0000DD11
		public static NameValueCollection ParseQueryString(this Uri address)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			return new FormDataCollection(address).ReadAsNameValueCollection();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public static bool TryReadQueryAsJson(this Uri address, out JObject value)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			IEnumerable<KeyValuePair<string, string>> nameValuePairs = new FormDataCollection(address);
			return FormUrlEncodedJson.TryParse(nameValuePairs, out value);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000FB64 File Offset: 0x0000DD64
		public static bool TryReadQueryAs(this Uri address, Type type, out object value)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			IEnumerable<KeyValuePair<string, string>> nameValuePairs = new FormDataCollection(address);
			JObject token;
			if (FormUrlEncodedJson.TryParse(nameValuePairs, out token))
			{
				using (JTokenReader jtokenReader = new JTokenReader(token))
				{
					value = new JsonSerializer().Deserialize(jtokenReader, type);
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		public static bool TryReadQueryAs<T>(this Uri address, out T value)
		{
			if (address == null)
			{
				throw Error.ArgumentNull("address");
			}
			IEnumerable<KeyValuePair<string, string>> nameValuePairs = new FormDataCollection(address);
			JObject jobject;
			if (FormUrlEncodedJson.TryParse(nameValuePairs, out jobject))
			{
				value = jobject.ToObject<T>();
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
