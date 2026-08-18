using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009F RID: 159
	public class GetReplicaInfoRequest : LdapExtendedOperation
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00015EBC File Offset: 0x00014EBC
		static GetReplicaInfoRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.18", Type.GetType("Novell.Directory.Ldap.Extensions.GetReplicaInfoResponse"));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015F10 File Offset: 0x00014F10
		public GetReplicaInfoRequest(string serverDN, string partitionDN) : base("2.16.840.1.113719.1.27.100.17", null)
		{
			try
			{
				if (serverDN == null || partitionDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder enc = new LBEREncoder();
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(partitionDN);
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
