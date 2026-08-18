using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002C4 RID: 708
	public class Asn1OutputStream : DerOutputStream
	{
		// Token: 0x06001A88 RID: 6792 RVA: 0x0009C544 File Offset: 0x0009B544
		public Asn1OutputStream(Stream os) : base(os)
		{
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0009C550 File Offset: 0x0009B550
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
			throw new IOException("object not Asn1Encodable");
		}
	}
}
