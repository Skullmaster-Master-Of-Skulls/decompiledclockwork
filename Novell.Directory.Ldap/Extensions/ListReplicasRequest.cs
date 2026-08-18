using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A6 RID: 166
	public class ListReplicasRequest : LdapExtendedOperation
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x00016B98 File Offset: 0x00015B98
		static ListReplicasRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.20", Type.GetType("Novell.Directory.Ldap.Extensions.ListReplicasResponse"));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016BEC File Offset: 0x00015BEC
		public ListReplicasRequest(string serverName) : base("2.16.840.1.113719.1.27.100.19", null)
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
