using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000010 RID: 16
	internal class RecordStream
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000054B0 File Offset: 0x000044B0
		internal RecordStream(TlsProtocolHandler handler, Stream inStr, Stream outStr)
		{
			this.handler = handler;
			this.inStr = inStr;
			this.outStr = outStr;
			this.hash1 = new CombinedHash();
			this.hash2 = new CombinedHash();
			this.hash3 = new CombinedHash();
			this.readSuite = new TlsNullCipherSuite();
			this.writeSuite = this.readSuite;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005510 File Offset: 0x00004510
		public void ReadData()
		{
			short num = TlsUtilities.ReadUint8(this.inStr);
			TlsUtilities.CheckVersion(this.inStr, this.handler);
			int len = TlsUtilities.ReadUint16(this.inStr);
			byte[] array = this.DecodeAndVerify(num, this.inStr, len);
			this.handler.ProcessData(num, array, 0, array.Length);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005568 File Offset: 0x00004568
		internal byte[] DecodeAndVerify(short type, Stream inStr, int len)
		{
			byte[] array = new byte[len];
			TlsUtilities.ReadFully(array, inStr);
			return this.readSuite.DecodeCiphertext(type, array, 0, array.Length);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005594 File Offset: 0x00004594
		internal void WriteMessage(short type, byte[] message, int offset, int len)
		{
			if (type == 22)
			{
				this.UpdateHandshakeData(message, offset, len);
			}
			byte[] array = this.writeSuite.EncodePlaintext(type, message, offset, len);
			byte[] array2 = new byte[array.Length + 5];
			TlsUtilities.WriteUint8(type, array2, 0);
			TlsUtilities.WriteUint8(3, array2, 1);
			TlsUtilities.WriteUint8(1, array2, 2);
			TlsUtilities.WriteUint16(array.Length, array2, 3);
			Array.Copy(array, 0, array2, 5, array.Length);
			this.outStr.Write(array2, 0, array2.Length);
			this.outStr.Flush();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005615 File Offset: 0x00004615
		internal void UpdateHandshakeData(byte[] message, int offset, int len)
		{
			this.hash1.BlockUpdate(message, offset, len);
			this.hash2.BlockUpdate(message, offset, len);
			this.hash3.BlockUpdate(message, offset, len);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005644 File Offset: 0x00004644
		internal void Close()
		{
			IOException ex = null;
			try
			{
				this.inStr.Close();
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			try
			{
				this.outStr.Close();
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005698 File Offset: 0x00004698
		internal void Flush()
		{
			this.outStr.Flush();
		}

		// Token: 0x04000038 RID: 56
		private TlsProtocolHandler handler;

		// Token: 0x04000039 RID: 57
		private Stream inStr;

		// Token: 0x0400003A RID: 58
		private Stream outStr;

		// Token: 0x0400003B RID: 59
		internal CombinedHash hash1;

		// Token: 0x0400003C RID: 60
		internal CombinedHash hash2;

		// Token: 0x0400003D RID: 61
		internal CombinedHash hash3;

		// Token: 0x0400003E RID: 62
		internal TlsCipherSuite readSuite;

		// Token: 0x0400003F RID: 63
		internal TlsCipherSuite writeSuite;
	}
}
