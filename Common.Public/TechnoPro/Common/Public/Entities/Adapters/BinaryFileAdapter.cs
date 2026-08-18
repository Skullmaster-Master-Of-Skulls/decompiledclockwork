using System;
using System.IO;
using System.Security.Cryptography;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005B8 RID: 1464
	public static class BinaryFileAdapter
	{
		// Token: 0x06002F57 RID: 12119 RVA: 0x00034B0C File Offset: 0x00032D0C
		public static string ComputeFileHash(this byte[] FileBytes)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (Stream stream = new MemoryStream(FileBytes))
				{
					result = BitConverter.ToString(md.ComputeHash(stream)).Replace("-", "").ToLower();
				}
			}
			return result;
		}
	}
}
