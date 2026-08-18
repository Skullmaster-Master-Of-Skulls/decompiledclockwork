using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000029 RID: 41
	public class CmsCompressedDataStreamGenerator
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000087C4 File Offset: 0x000077C4
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000087CD File Offset: 0x000077CD
		public Stream Open(Stream outStream, string compressionOID)
		{
			return this.Open(outStream, CmsObjectIdentifiers.Data.Id, compressionOID);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000087E4 File Offset: 0x000077E4
		public Stream Open(Stream outStream, string contentOID, string compressionOID)
		{
			BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
			berSequenceGenerator.AddObject(CmsObjectIdentifiers.CompressedData);
			BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
			berSequenceGenerator2.AddObject(new DerInteger(0));
			berSequenceGenerator2.AddObject(new AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.3.8")));
			BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(berSequenceGenerator2.GetRawOutputStream());
			berSequenceGenerator3.AddObject(new DerObjectIdentifier(contentOID));
			Stream outp = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, true, this._bufferSize);
			return new CmsCompressedDataStreamGenerator.CmsCompressedOutputStream(new ZDeflaterOutputStream(outp), berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
		}

		// Token: 0x04000098 RID: 152
		public const string ZLib = "1.2.840.113549.1.9.16.3.8";

		// Token: 0x04000099 RID: 153
		private int _bufferSize;

		// Token: 0x0200002B RID: 43
		private class CmsCompressedOutputStream : BaseOutputStream
		{
			// Token: 0x06000138 RID: 312 RVA: 0x000088EF File Offset: 0x000078EF
			internal CmsCompressedOutputStream(ZDeflaterOutputStream outStream, BerSequenceGenerator sGen, BerSequenceGenerator cGen, BerSequenceGenerator eiGen)
			{
				this._out = outStream;
				this._sGen = sGen;
				this._cGen = cGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06000139 RID: 313 RVA: 0x00008914 File Offset: 0x00007914
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x0600013A RID: 314 RVA: 0x00008922 File Offset: 0x00007922
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x0600013B RID: 315 RVA: 0x00008932 File Offset: 0x00007932
			public override void Close()
			{
				this._out.Close();
				this._eiGen.Close();
				this._cGen.Close();
				this._sGen.Close();
				base.Close();
			}

			// Token: 0x0400009B RID: 155
			private ZDeflaterOutputStream _out;

			// Token: 0x0400009C RID: 156
			private BerSequenceGenerator _sGen;

			// Token: 0x0400009D RID: 157
			private BerSequenceGenerator _cGen;

			// Token: 0x0400009E RID: 158
			private BerSequenceGenerator _eiGen;
		}
	}
}
