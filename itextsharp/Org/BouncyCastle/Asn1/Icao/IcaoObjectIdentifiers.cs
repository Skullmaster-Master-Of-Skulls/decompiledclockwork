using System;

namespace Org.BouncyCastle.Asn1.Icao
{
	// Token: 0x0200051C RID: 1308
	public abstract class IcaoObjectIdentifiers
	{
		// Token: 0x04001EAF RID: 7855
		public const string IdIcao = "2.23.136";

		// Token: 0x04001EB0 RID: 7856
		public static readonly DerObjectIdentifier IdIcaoMrtd = new DerObjectIdentifier("2.23.136.1");

		// Token: 0x04001EB1 RID: 7857
		public static readonly DerObjectIdentifier IdIcaoMrtdSecurity = new DerObjectIdentifier(IcaoObjectIdentifiers.IdIcaoMrtd + ".1");

		// Token: 0x04001EB2 RID: 7858
		public static readonly DerObjectIdentifier IdIcaoLdsSecurityObject = new DerObjectIdentifier(IcaoObjectIdentifiers.IdIcaoMrtdSecurity + ".1");
	}
}
