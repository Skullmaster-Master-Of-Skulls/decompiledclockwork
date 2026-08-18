using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;

namespace System.Web.Helpers
{
	// Token: 0x02000014 RID: 20
	public static class Json
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000052C8 File Offset: 0x000034C8
		public static string Encode(object value)
		{
			DynamicJsonArray dynamicJsonArray = value as DynamicJsonArray;
			if (dynamicJsonArray != null)
			{
				return Json._serializer.Serialize(dynamicJsonArray);
			}
			return Json._serializer.Serialize(value);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000052FB File Offset: 0x000034FB
		public static void Write(object value, TextWriter writer)
		{
			writer.Write(Json._serializer.Serialize(value));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000530E File Offset: 0x0000350E
		[return: Dynamic]
		public static dynamic Decode(string value)
		{
			return Json.WrapObject(Json._serializer.DeserializeObject(value));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005320 File Offset: 0x00003520
		[return: Dynamic]
		public static dynamic Decode(string value, Type targetType)
		{
			return Json.WrapObject(Json._serializer.Deserialize(value, targetType));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005333 File Offset: 0x00003533
		public static T Decode<T>(string value)
		{
			return Json._serializer.Deserialize<T>(value);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005340 File Offset: 0x00003540
		private static JavaScriptSerializer CreateSerializer()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new DynamicJavaScriptConverter[]
			{
				new DynamicJavaScriptConverter()
			});
			return javaScriptSerializer;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000536C File Offset: 0x0000356C
		[return: Dynamic]
		internal static dynamic WrapObject(object value)
		{
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			if (dictionary != null)
			{
				return new DynamicJsonObject(dictionary);
			}
			object[] array = value as object[];
			if (array != null)
			{
				return new DynamicJsonArray(array);
			}
			return value;
		}

		// Token: 0x04000044 RID: 68
		private static readonly JavaScriptSerializer _serializer = Json.CreateSerializer();
	}
}
