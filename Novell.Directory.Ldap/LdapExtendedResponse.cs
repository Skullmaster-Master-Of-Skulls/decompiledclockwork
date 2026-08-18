using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000034 RID: 52
	public class LdapExtendedResponse : LdapResponse
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000B880 File Offset: 0x0000A880
		public virtual string ID
		{
			get
			{
				RfcLdapOID responseName = ((RfcExtendedResponse)this.message.Response).ResponseName;
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
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B8CC File Offset: 0x0000A8CC
		public static RespExtensionSet RegisteredResponses
		{
			get
			{
				return LdapExtendedResponse.registeredResponses;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B8E4 File Offset: 0x0000A8E4
		[CLSCompliant(false)]
		public virtual sbyte[] Value
		{
			get
			{
				Asn1OctetString response = ((RfcExtendedResponse)this.message.Response).Response;
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
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B918 File Offset: 0x0000A918
		public LdapExtendedResponse(RfcLdapMessage message) : base(message)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B92C File Offset: 0x0000A92C
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapExtendedResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x0400010D RID: 269
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
