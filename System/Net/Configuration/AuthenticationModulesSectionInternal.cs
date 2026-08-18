using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000644 RID: 1604
	internal sealed class AuthenticationModulesSectionInternal
	{
		// Token: 0x060031B1 RID: 12721 RVA: 0x000D498C File Offset: 0x000D398C
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
					catch
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_authenticationmodules"), new Exception(SR.GetString("net_nonClsCompliantException")));
					}
					this.authenticationModules.Add(type);
				}
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x000D4AC8 File Offset: 0x000D3AC8
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

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x000D4AE8 File Offset: 0x000D3AE8
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

		// Token: 0x060031B4 RID: 12724 RVA: 0x000D4B14 File Offset: 0x000D3B14
		internal static AuthenticationModulesSectionInternal GetSection()
		{
			AuthenticationModulesSectionInternal result;
			lock (AuthenticationModulesSectionInternal.ClassSyncObject)
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

		// Token: 0x04002EA3 RID: 11939
		private List<Type> authenticationModules;

		// Token: 0x04002EA4 RID: 11940
		private static object classSyncObject;
	}
}
