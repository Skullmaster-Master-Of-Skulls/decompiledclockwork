using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E0 RID: 224
	public class RfcReferral : Asn1SequenceOf
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x0001A914 File Offset: 0x00019914
		[CLSCompliant(false)]
		public RfcReferral(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}
	}
}
