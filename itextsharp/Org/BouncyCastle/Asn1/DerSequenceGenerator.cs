using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020004D7 RID: 1239
	public class DerSequenceGenerator : DerGenerator
	{
		// Token: 0x06002A31 RID: 10801 RVA: 0x001006B3 File Offset: 0x000FF6B3
		public DerSequenceGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x001006C7 File Offset: 0x000FF6C7
		public DerSequenceGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x001006DD File Offset: 0x000FF6DD
		public override void AddObject(Asn1Encodable obj)
		{
			new DerOutputStream(this._bOut).WriteObject(obj);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x001006F0 File Offset: 0x000FF6F0
		public override Stream GetRawOutputStream()
		{
			return this._bOut;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x001006F8 File Offset: 0x000FF6F8
		public override void Close()
		{
			base.WriteDerEncoded(48, this._bOut.ToArray());
		}

		// Token: 0x04001D71 RID: 7537
		private readonly MemoryStream _bOut = new MemoryStream();
	}
}
