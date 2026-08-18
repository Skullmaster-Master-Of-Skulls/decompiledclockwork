using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AA RID: 170
	public class PartitionEntryCountRequest : LdapExtendedOperation
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00016E00 File Offset: 0x00015E00
		static PartitionEntryCountRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.14", Type.GetType("Novell.Directory.Ldap.Extensions.PartitionEntryCountResponse"));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00016E54 File Offset: 0x00015E54
		public PartitionEntryCountRequest(string dn) : base("2.16.840.1.113719.1.27.100.13", null)
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
