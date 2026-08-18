using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000314 RID: 788
	public class DerSetGenerator : DerGenerator
	{
		// Token: 0x06001CB8 RID: 7352 RVA: 0x000AB944 File Offset: 0x000AA944
		public DerSetGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000AB958 File Offset: 0x000AA958
		public DerSetGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000AB96E File Offset: 0x000AA96E
		public override void AddObject(Asn1Encodable obj)
		{
			new DerOutputStream(this._bOut).WriteObject(obj);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000AB981 File Offset: 0x000AA981
		public override Stream GetRawOutputStream()
		{
			return this._bOut;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000AB989 File Offset: 0x000AA989
		public override void Close()
		{
			base.WriteDerEncoded(49, this._bOut.ToArray());
		}

		// Token: 0x040013D0 RID: 5072
		private readonly MemoryStream _bOut = new MemoryStream();
	}
}
