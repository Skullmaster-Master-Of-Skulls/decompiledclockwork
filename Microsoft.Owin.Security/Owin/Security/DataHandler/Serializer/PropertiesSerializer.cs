using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x02000012 RID: 18
	public class PropertiesSerializer : IDataSerializer<AuthenticationProperties>
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000250C File Offset: 0x0000070C
		public byte[] Serialize(AuthenticationProperties model)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					PropertiesSerializer.Write(binaryWriter, model);
					binaryWriter.Flush();
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002570 File Offset: 0x00000770
		public AuthenticationProperties Deserialize(byte[] data)
		{
			AuthenticationProperties result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					result = PropertiesSerializer.Read(binaryReader);
				}
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025C8 File Offset: 0x000007C8
		public static void Write(BinaryWriter writer, AuthenticationProperties properties)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			writer.Write(1);
			writer.Write(properties.Dictionary.Count);
			foreach (KeyValuePair<string, string> keyValuePair in properties.Dictionary)
			{
				writer.Write(keyValuePair.Key);
				writer.Write(keyValuePair.Value);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000265C File Offset: 0x0000085C
		public static AuthenticationProperties Read(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadInt32() != 1)
			{
				return null;
			}
			int num = reader.ReadInt32();
			Dictionary<string, string> dictionary = new Dictionary<string, string>(num);
			for (int num2 = 0; num2 != num; num2++)
			{
				string key = reader.ReadString();
				string value = reader.ReadString();
				dictionary.Add(key, value);
			}
			return new AuthenticationProperties(dictionary);
		}

		// Token: 0x04000011 RID: 17
		private const int FormatVersion = 1;
	}
}
