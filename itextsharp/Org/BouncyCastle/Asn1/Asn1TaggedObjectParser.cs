using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000151 RID: 337
	public interface Asn1TaggedObjectParser : IAsn1Convertible
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C17 RID: 3095
		int TagNo { get; }

		// Token: 0x06000C18 RID: 3096
		IAsn1Convertible GetObjectParser(int tag, bool isExplicit);
	}
}
