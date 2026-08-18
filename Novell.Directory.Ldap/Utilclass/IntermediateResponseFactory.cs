using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F2 RID: 242
	public class IntermediateResponseFactory
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0001CB68 File Offset: 0x0001BB68
		public static LdapIntermediateResponse convertToIntermediateResponse(RfcLdapMessage inResponse)
		{
			LdapIntermediateResponse ldapIntermediateResponse = new LdapIntermediateResponse(inResponse);
			string id = ldapIntermediateResponse.getID();
			RespExtensionSet registeredResponses = LdapIntermediateResponse.getRegisteredResponses();
			try
			{
				Type type = registeredResponses.findResponseExtension(id);
				if (type == null)
				{
					return ldapIntermediateResponse;
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
						return (LdapIntermediateResponse)obj;
					}
					catch (UnauthorizedAccessException ex)
					{
					}
					catch (TargetInvocationException ex2)
					{
					}
				}
				catch (MissingMethodException ex3)
				{
				}
			}
			catch (MissingFieldException ex4)
			{
			}
			return ldapIntermediateResponse;
		}
	}
}
