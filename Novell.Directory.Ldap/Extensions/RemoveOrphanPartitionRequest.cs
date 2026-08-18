using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AF RID: 175
	public class RemoveOrphanPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x000170CC File Offset: 0x000160CC
		public RemoveOrphanPartitionRequest(string serverDN, string contextName) : base("2.16.840.1.113719.1.27.100.41", null)
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
