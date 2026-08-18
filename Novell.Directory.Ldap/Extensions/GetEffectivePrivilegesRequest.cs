using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009D RID: 157
	public class GetEffectivePrivilegesRequest : LdapExtendedOperation
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x00015D34 File Offset: 0x00014D34
		static GetEffectivePrivilegesRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.34", Type.GetType("Novell.Directory.Ldap.Extensions.GetEffectivePrivilegesResponse"));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015D88 File Offset: 0x00014D88
		public GetEffectivePrivilegesRequest(string dn, string trusteeDN, string attrName) : base("2.16.840.1.113719.1.27.100.33", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(dn);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(trusteeDN);
				Asn1OctetString asn1OctetString3 = new Asn1OctetString(attrName);
				asn1OctetString.encode(enc, memoryStream);
				asn1OctetString2.encode(enc, memoryStream);
				asn1OctetString3.encode(enc, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException ex)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
