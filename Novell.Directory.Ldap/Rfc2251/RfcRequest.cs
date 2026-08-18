using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000B9 RID: 185
	public interface RfcRequest
	{
		// Token: 0x060004D7 RID: 1239
		RfcRequest dupRequest(string base_Renamed, string filter, bool reference);

		// Token: 0x060004D8 RID: 1240
		string getRequestDN();
	}
}
