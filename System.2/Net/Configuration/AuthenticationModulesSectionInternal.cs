using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000327 RID: 807
	internal sealed class AuthenticationModulesSectionInternal
	{
		// Token: 0x06001CFA RID: 7418 RVA: 0x0008AAA8 File Offset: 0x00088CA8
		internal AuthenticationModulesSectionInternal(AuthenticationModulesSection section)
		{
			if (section.AuthenticationModules.Count > 0)
			{
				this.authenticationModules = new List<Type>(section.AuthenticationModules.Count);
				foreach (object obj in section.AuthenticationModules)
				{
					AuthenticationModuleElement authenticationModuleElement = (AuthenticationModuleElement)obj;
					Type type = null;
					try
					{
						type = Type.GetType(authenticationModuleElement.Type, true, true);
						if (!typeof(IAuthenticationModule).IsAssignableFrom(type))
						{
							throw new InvalidCastException(SR.GetString("net_invalid_cast", new object[]
							{
								type.FullName,
								"IAuthenticationModule"
							}));
						}
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_authenticationmodules"), ex);
					}
					this.authenticationModules.Add(type);
				}
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0008ABAC File Offset: 0x00088DAC
		internal List<Type> AuthenticationModules
		{
			get
			{
				List<Type> list = this.authenticationModules;
				if (list == null)
				{
					list = new List<Type>(0);
				}
				return list;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0008ABCC File Offset: 0x00088DCC
		internal static object ClassSyncObject
		{
			get
			{
				if (AuthenticationModulesSectionInternal.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref AuthenticationModulesSectionInternal.classSyncObject, value, null);
				}
				return AuthenticationModulesSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0008ABF8 File Offset: 0x00088DF8
		internal static AuthenticationModulesSectionInternal GetSection()
		{
			object obj = AuthenticationModulesSectionInternal.ClassSyncObject;
			AuthenticationModulesSectionInternal result;
			lock (obj)
			{
				AuthenticationModulesSection authenticationModulesSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.AuthenticationModulesSectionPath) as AuthenticationModulesSection;
				if (authenticationModulesSection == null)
				{
					result = null;
				}
				else
				{
					result = new AuthenticationModulesSectionInternal(authenticationModulesSection);
				}
			}
			return result;
		}

		// Token: 0x04001BCA RID: 7114
		private List<Type> authenticationModules;

		// Token: 0x04001BCB RID: 7115
		private static object classSyncObject;
	}
}
