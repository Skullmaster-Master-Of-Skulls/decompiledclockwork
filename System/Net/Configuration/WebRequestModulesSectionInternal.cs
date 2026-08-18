using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000670 RID: 1648
	internal sealed class WebRequestModulesSectionInternal
	{
		// Token: 0x060032F3 RID: 13043 RVA: 0x000D7AD4 File Offset: 0x000D6AD4
		internal WebRequestModulesSectionInternal(WebRequestModulesSection section)
		{
			if (section.WebRequestModules.Count > 0)
			{
				this.webRequestModules = new ArrayList(section.WebRequestModules.Count);
				foreach (object obj in section.WebRequestModules)
				{
					WebRequestModuleElement webRequestModuleElement = (WebRequestModuleElement)obj;
					try
					{
						this.webRequestModules.Add(new WebRequestPrefixElement(webRequestModuleElement.Prefix, webRequestModuleElement.Type));
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						throw new ConfigurationErrorsException(SR.GetString("net_config_webrequestmodules"), ex);
					}
					catch
					{
						throw new ConfigurationErrorsException(ConfigurationStrings.WebRequestModulesSectionPath, new Exception(SR.GetString("net_nonClsCompliantException")));
					}
				}
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000D7BC4 File Offset: 0x000D6BC4
		internal static object ClassSyncObject
		{
			get
			{
				if (WebRequestModulesSectionInternal.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref WebRequestModulesSectionInternal.classSyncObject, value, null);
				}
				return WebRequestModulesSectionInternal.classSyncObject;
			}
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000D7BF0 File Offset: 0x000D6BF0
		internal static WebRequestModulesSectionInternal GetSection()
		{
			WebRequestModulesSectionInternal result;
			lock (WebRequestModulesSectionInternal.ClassSyncObject)
			{
				WebRequestModulesSection webRequestModulesSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.WebRequestModulesSectionPath) as WebRequestModulesSection;
				if (webRequestModulesSection == null)
				{
					result = null;
				}
				else
				{
					result = new WebRequestModulesSectionInternal(webRequestModulesSection);
				}
			}
			return result;
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000D7C44 File Offset: 0x000D6C44
		internal ArrayList WebRequestModules
		{
			get
			{
				ArrayList arrayList = this.webRequestModules;
				if (arrayList == null)
				{
					arrayList = new ArrayList(0);
				}
				return arrayList;
			}
		}

		// Token: 0x04002F7E RID: 12158
		private static object classSyncObject;

		// Token: 0x04002F7F RID: 12159
		private ArrayList webRequestModules;
	}
}
