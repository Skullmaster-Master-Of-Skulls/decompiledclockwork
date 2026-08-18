using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F1 RID: 241
	public class ExtResponseFactory
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0001CA28 File Offset: 0x0001BA28
		public static LdapExtendedResponse convertToExtendedResponse(RfcLdapMessage inResponse)
		{
			LdapExtendedResponse ldapExtendedResponse = new LdapExtendedResponse(inResponse);
			string id = ldapExtendedResponse.ID;
			RespExtensionSet registeredResponses = LdapExtendedResponse.RegisteredResponses;
			try
			{
				Type type = registeredResponses.findResponseExtension(id);
				if (type == null)
				{
					return ldapExtendedResponse;
				}
				Type[] types = new Type[]
				{
					typeof(RfcLdapMessage)
				};
				object[] parameters = new object[]
				{
					inResponse
				};
				try
				{
					ConstructorInfo constructor = type.GetConstructor(types);
					try
					{
						object obj = constructor.Invoke(parameters);
						return (LdapExtendedResponse)obj;
					}
					catch (UnauthorizedAccessException ex)
					{
					}
					catch (TargetInvocationException ex2)
					{
					}
					catch (Exception ex3)
					{
					}
				}
				catch (MethodAccessException ex4)
				{
				}
			}
			catch (FieldAccessException ex5)
			{
			}
			return ldapExtendedResponse;
		}
	}
}
