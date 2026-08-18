using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000582 RID: 1410
	public class BerOutputStream : DerOutputStream
	{
		// Token: 0x06002FFD RID: 12285 RVA: 0x00127F4D File Offset: 0x00126F4D
		public BerOutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00127F58 File Offset: 0x00126F58
		[Obsolete("Use version taking an Asn1Encodable arg instead")]
		public override void WriteObject(object obj)
		{
			if (obj == null)
			{
				base.WriteNull();
				return;
			}
			if (obj is Asn1Object)
			{
				((Asn1Object)obj).Encode(this);
				return;
			}
			if (obj is Asn1Encodable)
			{
				((Asn1Encodable)obj).ToAsn1Object().Encode(this);
				return;
			}
			throw new IOException("object not BerEncodable");
		}
	}
}
