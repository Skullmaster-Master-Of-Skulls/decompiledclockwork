using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200034D RID: 845
	internal sealed class WebRequestModulesSectionInternal
	{
		// Token: 0x06001E55 RID: 7765 RVA: 0x0008DF7C File Offset: 0x0008C17C
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
				}
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0008E044 File Offset: 0x0008C244
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

		// Token: 0x06001E57 RID: 7767 RVA: 0x0008E070 File Offset: 0x0008C270
		internal static WebRequestModulesSectionInternal GetSection()
		{
			object obj = WebRequestModulesSectionInternal.ClassSyncObject;
			WebRequestModulesSectionInternal result;
			lock (obj)
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0008E0C8 File Offset: 0x0008C2C8
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

		// Token: 0x04001CC2 RID: 7362
		private static object classSyncObject;

		// Token: 0x04001CC3 RID: 7363
		private ArrayList webRequestModules;
	}
}
