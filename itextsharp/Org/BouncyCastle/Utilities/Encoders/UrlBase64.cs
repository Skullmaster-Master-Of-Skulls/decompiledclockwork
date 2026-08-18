using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000296 RID: 662
	public class UrlBase64
	{
		// Token: 0x060018F6 RID: 6390 RVA: 0x00092DBC File Offset: 0x00091DBC
		public static byte[] Encode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.Encode(data, 0, data.Length, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception encoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00092E10 File Offset: 0x00091E10
		public static int Encode(byte[] data, Stream outStr)
		{
			return UrlBase64.encoder.Encode(data, 0, data.Length, outStr);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00092E24 File Offset: 0x00091E24
		public static byte[] Decode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.Decode(data, 0, data.Length, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception decoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00092E78 File Offset: 0x00091E78
		public static int Decode(byte[] data, Stream outStr)
		{
			return UrlBase64.encoder.Decode(data, 0, data.Length, outStr);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00092E8C File Offset: 0x00091E8C
		public static byte[] Decode(string data)
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				UrlBase64.encoder.DecodeString(data, memoryStream);
			}
			catch (IOException ex)
			{
				throw new Exception("exception decoding URL safe base64 string: " + ex.Message, ex);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00092EDC File Offset: 0x00091EDC
		public static int Decode(string data, Stream outStr)
		{
			return UrlBase64.encoder.DecodeString(data, outStr);
		}

		// Token: 0x040010E4 RID: 4324
		private static readonly IEncoder encoder = new UrlBase64Encoder();
	}
}
