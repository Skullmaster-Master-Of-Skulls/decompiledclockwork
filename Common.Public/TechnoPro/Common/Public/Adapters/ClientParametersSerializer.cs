using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005EA RID: 1514
	public static class ClientParametersSerializer
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x000426E8 File Offset: 0x000408E8
		public static byte[] Serialize(this ClientParameters clientParameters)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, clientParameters);
			return memoryStream.ToArray();
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x00042718 File Offset: 0x00040918
		public static ClientParameters Deserialize(byte[] binaryData)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream serializationStream = new MemoryStream(binaryData);
			return (ClientParameters)binaryFormatter.Deserialize(serializationStream);
		}
	}
}
