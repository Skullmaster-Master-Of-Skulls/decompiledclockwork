using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Google.Apis.Logging;
using Google.Apis.Util;

namespace Google.Apis.Requests.Parameters
{
	// Token: 0x02000016 RID: 22
	public static class ParameterUtils
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003140 File Offset: 0x00001340
		public static FormUrlEncodedContent CreateFormUrlEncodedContent(object request)
		{
			IList<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			ParameterUtils.IterateParameters(request, delegate(RequestParameterType type, string name, object value)
			{
				list.Add(new KeyValuePair<string, string>(name, value.ToString()));
			});
			return new FormUrlEncodedContent(list);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000317C File Offset: 0x0000137C
		public static IDictionary<string, object> CreateParameterDictionary(object request)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			ParameterUtils.IterateParameters(request, delegate(RequestParameterType type, string name, object value)
			{
				dict.Add(name, value);
			});
			return dict;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031B4 File Offset: 0x000013B4
		public static void InitParameters(RequestBuilder builder, object request)
		{
			ParameterUtils.IterateParameters(request, delegate(RequestParameterType type, string name, object value)
			{
				builder.AddParameter(type, name, value.ToString());
			});
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031E0 File Offset: 0x000013E0
		private static void IterateParameters(object request, Action<RequestParameterType, string, object> action)
		{
			foreach (PropertyInfo propertyInfo in request.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				RequestParameterAttribute requestParameterAttribute = propertyInfo.GetCustomAttributes(typeof(RequestParameterAttribute), false).FirstOrDefault<object>() as RequestParameterAttribute;
				if (requestParameterAttribute != null)
				{
					string arg = requestParameterAttribute.Name ?? propertyInfo.Name.ToLower();
					Type propertyType = propertyInfo.PropertyType;
					object value = propertyInfo.GetValue(request, null);
					if (propertyType.GetTypeInfo().IsValueType || value != null)
					{
						if (requestParameterAttribute.Type == RequestParameterType.UserDefinedQueries)
						{
							if (typeof(IEnumerable<KeyValuePair<string, string>>).IsAssignableFrom(value.GetType()))
							{
								using (IEnumerator<KeyValuePair<string, string>> enumerator = ((IEnumerable<KeyValuePair<string, string>>)value).GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										KeyValuePair<string, string> keyValuePair = enumerator.Current;
										action(RequestParameterType.Query, keyValuePair.Key, keyValuePair.Value);
									}
									goto IL_106;
								}
							}
							ParameterUtils.Logger.Warning("Parameter marked with RequestParameterType.UserDefinedQueries attribute was not of type IEnumerable<KeyValuePair<string, string>> and will be skipped.", new object[0]);
						}
						else
						{
							action(requestParameterAttribute.Type, arg, value);
						}
					}
				}
				IL_106:;
			}
		}

		// Token: 0x04000025 RID: 37
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType(typeof(ParameterUtils));
	}
}
