using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000486 RID: 1158
	internal class KeyValueJavaScriptConverter<k, v> : JavaScriptConverter
	{
		// Token: 0x0600294D RID: 10573 RVA: 0x00085674 File Offset: 0x00083874
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			return new KeyValuePair<k, v>((k)((object)dictionary["Key"]), (v)((object)dictionary["Value"]));
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000856A0 File Offset: 0x000838A0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			KeyValuePair<k, v> keyValuePair = (KeyValuePair<k, v>)obj;
			if (!object.Equals(keyValuePair, null))
			{
				dictionary.Add("Key", keyValuePair.Key);
				dictionary.Add("Value", keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000856F8 File Offset: 0x000838F8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(KeyValuePair<k, v>)
				};
			}
		}
	}
}
