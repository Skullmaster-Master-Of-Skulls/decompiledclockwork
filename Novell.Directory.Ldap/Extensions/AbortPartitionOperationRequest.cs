using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000097 RID: 151
	public class AbortPartitionOperationRequest : LdapExtendedOperation
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00015A08 File Offset: 0x00014A08
		public AbortPartitionOperationRequest(string partitionDN, int flags) : base("2.16.840.1.113719.1.27.100.29", null)
		{
			try
			{
				if (partitionDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1Integer asn1Integer = new Asn1Integer(flags);
				Asn1OctetString asn1OctetString = new Asn1OctetString(partitionDN);
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
