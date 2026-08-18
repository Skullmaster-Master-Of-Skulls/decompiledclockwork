using System;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x020009A8 RID: 2472
	internal static class SerializationService
	{
		// Token: 0x06005EDD RID: 24285 RVA: 0x00121A3E File Offset: 0x0011FC3E
		internal static string Serialize(object obj, int maxJsonLength = 4194304)
		{
			return SerializationService.GetSerializer(maxJsonLength).Serialize(obj);
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x00121A4C File Offset: 0x0011FC4C
		internal static string Serialize(object obj, bool encrypted, int maxJsonLength = 4194304)
		{
			if (encrypted)
			{
				return CryptoService.GetService("").Encrypt(SerializationService.Serialize(obj, maxJsonLength));
			}
			return SerializationService.Serialize(obj, 4194304);
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x00121A74 File Offset: 0x0011FC74
		internal static object Deserialize(string obj, Type type)
		{
			JavaScriptSerializer serializer = SerializationService.GetSerializer(obj.Length);
			SerializationService.ApplyConverters(type, serializer);
			MethodInfo methodInfo = typeof(JavaScriptSerializer).GetMethod("Deserialize", new Type[]
			{
				typeof(string)
			}, null).MakeGenericMethod(new Type[]
			{
				type
			});
			return methodInfo.Invoke(serializer, new object[]
			{
				obj
			});
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x00121AEC File Offset: 0x0011FCEC
		private static void ApplyConverters(Type type, JavaScriptSerializer serializer)
		{
			if (type.GetInterface(typeof(IAsyncUploadConfiguration).FullName) != null)
			{
				serializer.RegisterConverters(new AsyncUploadConfigurationConverter[]
				{
					new AsyncUploadConfigurationConverter()
				});
			}
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x00121B2C File Offset: 0x0011FD2C
		internal static object Deserialize(string obj, Type type, bool decrypt)
		{
			if (decrypt)
			{
				obj = CryptoService.GetService("").Decrypt(obj);
			}
			return SerializationService.Deserialize(obj, type);
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x00121B4C File Offset: 0x0011FD4C
		internal static JavaScriptSerializer GetSerializer(int maxJsonLength)
		{
			return new JavaScriptSerializer
			{
				MaxJsonLength = maxJsonLength
			};
		}
	}
}
