using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000098 RID: 152
	public class AddReplicaRequest : LdapExtendedOperation
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00015A98 File Offset: 0x00014A98
		public AddReplicaRequest(string dn, string serverDN, int replicaType, int flags) : base("2.16.840.1.113719.1.27.100.7", null)
		{
			try
			{
				if (dn == null || serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1Integer asn1Integer = new Asn1Integer(flags);
				Asn1Integer asn1Integer2 = new Asn1Integer(replicaType);
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(dn);
				asn1Integer.encode(enc, memoryStream);
				asn1Integer2.encode(enc, memoryStream);
				asn1OctetString.encode(enc, memoryStream);
				asn1OctetString2.encode(enc, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException ex)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
