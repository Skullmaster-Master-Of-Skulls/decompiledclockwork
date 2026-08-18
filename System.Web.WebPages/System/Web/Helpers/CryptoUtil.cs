using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace System.Web.Helpers
{
	// Token: 0x0200002F RID: 47
	internal static class CryptoUtil
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public static bool AreByteArraysEqual(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			bool flag = true;
			for (int i = 0; i < a.Length; i++)
			{
				flag &= (a[i] == b[i]);
			}
			return flag;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004E24 File Offset: 0x00003024
		public static byte[] ComputeSHA256(IList<string> parameters)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					foreach (string value in parameters)
					{
						binaryWriter.Write(value);
					}
					binaryWriter.Flush();
					using (SHA256Cng sha256Cng = new SHA256Cng())
					{
						byte[] array = sha256Cng.ComputeHash(memoryStream.GetBuffer(), 0, checked((int)memoryStream.Length));
						result = array;
					}
				}
			}
			return result;
		}
	}
}
