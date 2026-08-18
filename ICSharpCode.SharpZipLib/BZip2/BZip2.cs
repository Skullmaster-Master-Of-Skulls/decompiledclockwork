using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x02000039 RID: 57
	public static class BZip2
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000DDFC File Offset: 0x0000CDFC
		public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
		{
			if (inStream == null || outStream == null)
			{
				throw new Exception("Null Stream");
			}
			try
			{
				using (BZip2InputStream bzip2InputStream = new BZip2InputStream(inStream))
				{
					bzip2InputStream.IsStreamOwner = isStreamOwner;
					StreamUtils.Copy(bzip2InputStream, outStream, new byte[4096]);
				}
			}
			finally
			{
				if (isStreamOwner)
				{
					outStream.Close();
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000DE70 File Offset: 0x0000CE70
		public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
		{
			if (inStream == null || outStream == null)
			{
				throw new Exception("Null Stream");
			}
			try
			{
				using (BZip2OutputStream bzip2OutputStream = new BZip2OutputStream(outStream, level))
				{
					bzip2OutputStream.IsStreamOwner = isStreamOwner;
					StreamUtils.Copy(inStream, bzip2OutputStream, new byte[4096]);
				}
			}
			finally
			{
				if (isStreamOwner)
				{
					inStream.Close();
				}
			}
		}
	}
}
