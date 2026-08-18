using System;
using System.Collections.Generic;
using System.Web.Http.Internal;

namespace System.Web.Http.Routing
{
	// Token: 0x02000109 RID: 265
	public class HttpRouteValueDictionary : Dictionary<string, object>
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x00015B8F File Offset: 0x00013D8F
		public HttpRouteValueDictionary() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00015B9C File Offset: 0x00013D9C
		public HttpRouteValueDictionary(IDictionary<string, object> dictionary) : base(StringComparer.OrdinalIgnoreCase)
		{
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					base.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00015C00 File Offset: 0x00013E00
		public HttpRouteValueDictionary(object values) : base(StringComparer.OrdinalIgnoreCase)
		{
			IDictionary<string, object> dictionary = values as IDictionary<string, object>;
			if (dictionary != null)
			{
				using (IEnumerator<KeyValuePair<string, object>> enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						base.Add(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			if (values != null)
			{
				foreach (PropertyHelper propertyHelper in PropertyHelper.GetProperties(values))
				{
					base.Add(propertyHelper.Name, propertyHelper.GetValue(values));
				}
			}
		}
	}
}
