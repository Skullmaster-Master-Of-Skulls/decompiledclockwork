using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x0200005B RID: 91
	[CLSCompliant(true)]
	public class Asn1Null : Asn1Object
	{
		// Token: 0x0600035B RID: 859 RVA: 0x00010F18 File Offset: 0x0000FF18
		public Asn1Null() : base(Asn1Null.ID)
		{
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010F34 File Offset: 0x0000FF34
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00010F4C File Offset: 0x0000FF4C
		public override string ToString()
		{
			return base.ToString() + "NULL: \"\"";
		}

		// Token: 0x04000193 RID: 403
		public const int TAG = 5;

		// Token: 0x04000194 RID: 404
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 5);
	}
}
