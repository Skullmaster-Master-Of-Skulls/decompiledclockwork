using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B3 RID: 179
	public class SendAllUpdatesRequest : LdapExtendedOperation
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x000172A8 File Offset: 0x000162A8
		public SendAllUpdatesRequest(string partitionRoot, string origServerDN) : base("2.16.840.1.113719.1.27.100.23", null)
		{
			try
			{
				if (partitionRoot == null || origServerDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(partitionRoot);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(origServerDN);
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
