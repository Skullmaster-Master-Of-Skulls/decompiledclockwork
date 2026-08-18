using System;
using System.IO;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003DC RID: 988
	public class CmsTypedStream
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x000D6048 File Offset: 0x000D5048
		public CmsTypedStream(Stream inStream) : this(PkcsObjectIdentifiers.Data.Id, inStream, 32768)
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000D6060 File Offset: 0x000D5060
		public CmsTypedStream(string oid, Stream inStream) : this(oid, inStream, 32768)
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000D606F File Offset: 0x000D506F
		public CmsTypedStream(string oid, Stream inStream, int bufSize)
		{
			this._oid = oid;
			this._in = new CmsTypedStream.FullReaderStream(inStream, bufSize);
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000D608B File Offset: 0x000D508B
		public string ContentType
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000D6093 File Offset: 0x000D5093
		public Stream ContentStream
		{
			get
			{
				return this._in;
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000D609B File Offset: 0x000D509B
		public void Drain()
		{
			Streams.Drain(this._in);
			this._in.Close();
		}

		// Token: 0x040017A2 RID: 6050
		private const int BufferSize = 32768;

		// Token: 0x040017A3 RID: 6051
		private readonly string _oid;

		// Token: 0x040017A4 RID: 6052
		private readonly Stream _in;

		// Token: 0x020003DD RID: 989
		private class FullReaderStream : BaseInputStream
		{
			// Token: 0x06002273 RID: 8819 RVA: 0x000D60B3 File Offset: 0x000D50B3
			internal FullReaderStream(Stream inStream, int bufSize)
			{
				this._stream = inStream;
			}

			// Token: 0x06002274 RID: 8820 RVA: 0x000D60C2 File Offset: 0x000D50C2
			public override int ReadByte()
			{
				return this._stream.ReadByte();
			}

			// Token: 0x06002275 RID: 8821 RVA: 0x000D60CF File Offset: 0x000D50CF
			public override int Read(byte[] buf, int off, int len)
			{
				return Streams.ReadFully(this._stream, buf, off, len);
			}

			// Token: 0x06002276 RID: 8822 RVA: 0x000D60DF File Offset: 0x000D50DF
			public override void Close()
			{
				this._stream.Close();
				base.Close();
			}

			// Token: 0x040017A5 RID: 6053
			internal Stream _stream;
		}
	}
}
