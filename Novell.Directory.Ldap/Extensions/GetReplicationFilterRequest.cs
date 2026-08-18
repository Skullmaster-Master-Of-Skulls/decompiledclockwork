using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A1 RID: 161
	public class GetReplicationFilterRequest : LdapExtendedOperation
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x00016258 File Offset: 0x00015258
		static GetReplicationFilterRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.38", Type.GetType("Novell.Directory.Ldap.Extensions.GetReplicationFilterResponse"));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000162AC File Offset: 0x000152AC
		public GetReplicationFilterRequest(string serverDN) : base("2.16.840.1.113719.1.27.100.37", null)
		{
			try
			{
				if (serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
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
