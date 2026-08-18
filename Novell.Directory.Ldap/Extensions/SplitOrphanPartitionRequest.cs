using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B5 RID: 181
	public class SplitOrphanPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x00017468 File Offset: 0x00016468
		public SplitOrphanPartitionRequest(string serverDN, string contextName) : base("2.16.840.1.113719.1.27.100.39", null)
		{
			try
			{
				if (serverDN == null || contextName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(contextName);
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
