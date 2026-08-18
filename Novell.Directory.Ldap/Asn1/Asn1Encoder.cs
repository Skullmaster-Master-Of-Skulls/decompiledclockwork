using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000055 RID: 85
	[CLSCompliant(true)]
	public interface Asn1Encoder : ISerializable
	{
		// Token: 0x06000330 RID: 816
		void encode(Asn1Boolean b, Stream out_Renamed);

		// Token: 0x06000331 RID: 817
		void encode(Asn1Numeric n, Stream out_Renamed);

		// Token: 0x06000332 RID: 818
		void encode(Asn1Null n, Stream out_Renamed);

		// Token: 0x06000333 RID: 819
		void encode(Asn1OctetString os, Stream out_Renamed);

		// Token: 0x06000334 RID: 820
		void encode(Asn1Structured c, Stream out_Renamed);

		// Token: 0x06000335 RID: 821
		void encode(Asn1Tagged t, Stream out_Renamed);

		// Token: 0x06000336 RID: 822
		void encode(Asn1Identifier id, Stream out_Renamed);
	}
}
