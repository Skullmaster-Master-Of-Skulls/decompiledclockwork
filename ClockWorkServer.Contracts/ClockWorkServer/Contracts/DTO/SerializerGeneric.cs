using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F8 RID: 248
	public static class SerializerGeneric
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x000029E4 File Offset: 0x00000BE4
		public static byte[] Serialize<T>(T item)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractSerializer.WriteObject(memoryStream, item);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00002A3C File Offset: 0x00000C3C
		public static T Deserialize<T>(byte[] bytes)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
			T result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = (T)((object)dataContractSerializer.ReadObject(memoryStream));
			}
			return result;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00002A90 File Offset: 0x00000C90
		public static IList<byte[]> Serialize<T>(IList<T> items)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
			List<byte[]> list = new List<byte[]>();
			foreach (T t in items)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					dataContractSerializer.WriteObject(memoryStream, t);
					list.Add(memoryStream.ToArray());
				}
			}
			return list;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00002B34 File Offset: 0x00000D34
		public static IList<T> Deserialize<T>(IList<byte[]> bytes)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
			List<T> list = new List<T>();
			foreach (byte[] buffer in bytes)
			{
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					list.Add((T)((object)dataContractSerializer.ReadObject(memoryStream)));
				}
			}
			return list;
		}
	}
}
