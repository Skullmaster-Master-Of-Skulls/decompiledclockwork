using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000035 RID: 53
	public class LdapIntermediateResponse : LdapResponse
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000B948 File Offset: 0x0000A948
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapIntermediateResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B964 File Offset: 0x0000A964
		public static RespExtensionSet getRegisteredResponses()
		{
			return LdapIntermediateResponse.registeredResponses;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B97C File Offset: 0x0000A97C
		public LdapIntermediateResponse(RfcLdapMessage message) : base(message)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B990 File Offset: 0x0000A990
		public string getID()
		{
			RfcLdapOID responseName = ((RfcIntermediateResponse)this.message.Response).getResponseName();
			string result;
			if (responseName == null)
			{
				result = null;
			}
			else
			{
				result = responseName.stringValue();
			}
			return result;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B9C4 File Offset: 0x0000A9C4
		[CLSCompliant(false)]
		public sbyte[] getValue()
		{
			Asn1OctetString response = ((RfcIntermediateResponse)this.message.Response).getResponse();
			sbyte[] result;
			if (response == null)
			{
				result = null;
			}
			else
			{
				result = response.byteValue();
			}
			return result;
		}

		// Token: 0x0400010E RID: 270
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
