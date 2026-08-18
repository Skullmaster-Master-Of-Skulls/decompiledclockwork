using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ClockWorkAPI
{
	// Token: 0x02000083 RID: 131
	public class CompressionTP
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x00024870 File Offset: 0x00023870
		public static string Compress(string text)
		{
			Exception ex;
			return CompressionTP.Compress(text, true, out ex);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002488C File Offset: 0x0002388C
		public static string Compress(string text, bool throwError)
		{
			Exception ex;
			return CompressionTP.Compress(text, throwError, out ex);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000248A8 File Offset: 0x000238A8
		public static string Decompress(string compressedText)
		{
			Exception ex;
			return CompressionTP.Decompress(compressedText, true, out ex);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000248C4 File Offset: 0x000238C4
		public static string Decompress(string compressedText, bool throwError)
		{
			Exception ex;
			return CompressionTP.Decompress(compressedText, throwError, out ex);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000248E0 File Offset: 0x000238E0
		public static string Compress(string text, bool throwError, out Exception ex)
		{
			try
			{
				if (text.Length < 1)
				{
					ex = null;
					return "";
				}
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				MemoryStream memoryStream = new MemoryStream();
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}
				memoryStream.Position = 0L;
				MemoryStream memoryStream2 = new MemoryStream();
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, array.Length);
				byte[] array2 = new byte[array.Length + 4];
				Buffer.BlockCopy(array, 0, array2, 4, array.Length);
				Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, array2, 0, 4);
				string result = Convert.ToBase64String(array2);
				ex = null;
				return result;
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null && throwError)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
			return "";
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00024A14 File Offset: 0x00023A14
		public static string Decompress(string compressedText, bool throwError, out Exception ex)
		{
			try
			{
				if (compressedText.Length < 1)
				{
					ex = null;
					return "";
				}
				byte[] array = Convert.FromBase64String(compressedText);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					int num = BitConverter.ToInt32(array, 0);
					memoryStream.Write(array, 4, array.Length - 4);
					byte[] array2 = new byte[num];
					memoryStream.Position = 0L;
					using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
					{
						gzipStream.Read(array2, 0, array2.Length);
					}
					string @string = Encoding.UTF8.GetString(array2);
					ex = null;
					return @string;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null && throwError)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
			return "";
		}
	}
}
