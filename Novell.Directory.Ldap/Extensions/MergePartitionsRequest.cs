using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A8 RID: 168
	public class MergePartitionsRequest : LdapExtendedOperation
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00016D5C File Offset: 0x00015D5C
		public MergePartitionsRequest(string dn, int flags) : base("2.16.840.1.113719.1.27.100.5", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1Integer asn1Integer = new Asn1Integer(flags);
				Asn1OctetString asn1OctetString = new Asn1OctetString(dn);
				asn1Integer.encode(enc, memoryStream);
				asn1OctetString.encode(enc, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException ex)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
