using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Script.Serialization;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x0200000D RID: 13
	internal class DynamicJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000039E8 File Offset: 0x00001BE8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(IDynamicMetaObjectProvider);
				yield return typeof(DynamicObject);
				yield break;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003A05 File Offset: 0x00001C05
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003A0C File Offset: 0x00001C0C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			IEnumerable<string> memberNames = DynamicHelper.GetMemberNames(obj);
			foreach (string text in memberNames)
			{
				object obj2 = DynamicHelper.GetMemberValue(obj, text);
				DynamicJsonArray dynamicJsonArray = obj2 as DynamicJsonArray;
				if (dynamicJsonArray != null)
				{
					obj2 = dynamicJsonArray;
				}
				dictionary[text] = obj2;
			}
			return dictionary;
		}
	}
}
