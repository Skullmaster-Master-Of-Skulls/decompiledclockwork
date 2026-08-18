using System;
using System.Configuration;
using System.Xml;
using NLog.Common;
using NLog.Internal.Fakeables;

namespace NLog.Config
{
	// Token: 0x02000042 RID: 66
	public sealed class ConfigSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00004C10 File Offset: 0x00002E10
		private object Create(XmlNode section, IAppDomain appDomain)
		{
			object result;
			try
			{
				string configurationFile = appDomain.ConfigurationFile;
				result = new XmlLoggingConfiguration((XmlElement)section, configurationFile);
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "ConfigSectionHandler error.");
				throw;
			}
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004C54 File Offset: 0x00002E54
		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
		{
			return this.Create(section, AppDomainWrapper.CurrentDomain);
		}
	}
}
