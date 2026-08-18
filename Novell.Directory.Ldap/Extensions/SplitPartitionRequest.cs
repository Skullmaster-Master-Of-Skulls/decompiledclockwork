using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B6 RID: 182
	public class SplitPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x000174FC File Offset: 0x000164FC
		public SplitPartitionRequest(string dn, int flags) : base("2.16.840.1.113719.1.27.100.3", null)
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
