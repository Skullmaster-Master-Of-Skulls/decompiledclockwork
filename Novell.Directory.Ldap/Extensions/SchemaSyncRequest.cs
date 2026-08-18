using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B2 RID: 178
	public class SchemaSyncRequest : LdapExtendedOperation
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00017218 File Offset: 0x00016218
		public SchemaSyncRequest(string serverName, int delay) : base("2.16.840.1.113719.1.27.100.27", null)
		{
			try
			{
				if (serverName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverName);
				Asn1Integer asn1Integer = new Asn1Integer(delay);
				asn1OctetString.encode(enc, memoryStream);
				asn1Integer.encode(enc, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException ex)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
