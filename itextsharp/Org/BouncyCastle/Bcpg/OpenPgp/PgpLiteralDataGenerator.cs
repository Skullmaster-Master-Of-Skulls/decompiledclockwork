using System;
using System.IO;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000608 RID: 1544
	public class PgpLiteralDataGenerator : IStreamGenerator
	{
		// Token: 0x0600349C RID: 13468 RVA: 0x00147950 File Offset: 0x00146950
		public PgpLiteralDataGenerator()
		{
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x00147958 File Offset: 0x00146958
		public PgpLiteralDataGenerator(bool oldFormat)
		{
			this.oldFormat = oldFormat;
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x00147968 File Offset: 0x00146968
		private void WriteHeader(BcpgOutputStream outStr, char format, string name, long modificationTime)
		{
			byte[] array = Strings.ToUtf8ByteArray(name);
			outStr.Write(new byte[]
			{
				(byte)format,
				(byte)array.Length
			});
			outStr.Write(array);
			long num = modificationTime / 1000L;
			outStr.Write(new byte[]
			{
				(byte)(num >> 24),
				(byte)(num >> 16),
				(byte)(num >> 8),
				(byte)num
			});
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x001479D4 File Offset: 0x001469D4
		public Stream Open(Stream outStr, char format, string name, long length, DateTime modificationTime)
		{
			if (this.pkOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			long modificationTime2 = DateTimeUtilities.DateTimeToUnixMs(modificationTime);
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.LiteralData, length + 2L + (long)name.Length + 4L, this.oldFormat);
			this.WriteHeader(this.pkOut, format, name, modificationTime2);
			return new WrappedGeneratorStream(this, this.pkOut);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x00147A4C File Offset: 0x00146A4C
		public Stream Open(Stream outStr, char format, string name, DateTime modificationTime, byte[] buffer)
		{
			if (this.pkOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			long modificationTime2 = DateTimeUtilities.DateTimeToUnixMs(modificationTime);
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.LiteralData, buffer);
			this.WriteHeader(this.pkOut, format, name, modificationTime2);
			return new WrappedGeneratorStream(this, this.pkOut);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x00147AAD File Offset: 0x00146AAD
		public Stream Open(Stream outStr, char format, FileInfo file)
		{
			return this.Open(outStr, format, file.Name, file.Length, file.LastWriteTime);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x00147AC9 File Offset: 0x00146AC9
		public void Close()
		{
			if (this.pkOut != null)
			{
				this.pkOut.Finish();
				this.pkOut.Flush();
				this.pkOut = null;
			}
		}

		// Token: 0x04002359 RID: 9049
		public const char Binary = 'b';

		// Token: 0x0400235A RID: 9050
		public const char Text = 't';

		// Token: 0x0400235B RID: 9051
		public const string Console = "_CONSOLE";

		// Token: 0x0400235C RID: 9052
		private BcpgOutputStream pkOut;

		// Token: 0x0400235D RID: 9053
		private bool oldFormat;
	}
}
