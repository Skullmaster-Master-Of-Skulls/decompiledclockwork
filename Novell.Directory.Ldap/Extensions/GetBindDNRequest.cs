using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009B RID: 155
	public class GetBindDNRequest : LdapExtendedOperation
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00015C20 File Offset: 0x00014C20
		static GetBindDNRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.32", Type.GetType("Novell.Directory.Ldap.Extensions.GetBindDNResponse"));
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015C74 File Offset: 0x00014C74
		public GetBindDNRequest() : base("2.16.840.1.113719.1.27.100.31", null)
		{
		}
	}
}
