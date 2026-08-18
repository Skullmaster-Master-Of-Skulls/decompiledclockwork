using System;
using System.Configuration;
using System.Runtime.Remoting;
using System.Xml;

namespace TechnoPro.Common.Configuration
{
	// Token: 0x02000003 RID: 3
	public static class ConfigurationSectionExtensions
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000025B0 File Offset: 0x000007B0
		public static T GetAs<T>(this ConfigurationSection section) where T : class
		{
			SectionInformation sectionInformation = section.SectionInformation;
			string type = section.SectionInformation.Type;
			string fullName = typeof(IConfigurationSectionHandler).Assembly.GetName().FullName;
			ObjectHandle objectHandle = Activator.CreateInstance(fullName, type);
			bool flag = objectHandle == null;
			if (flag)
			{
				throw new InvalidOperationException("Unable to find section handler type '" + type + "'.");
			}
			string rawXml = sectionInformation.GetRawXml();
			bool flag2 = rawXml == null;
			T result;
			if (flag2)
			{
				result = default(T);
			}
			else
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(rawXml);
				IConfigurationSectionHandler configurationSectionHandler = objectHandle.Unwrap() as IConfigurationSectionHandler;
				result = (T)((object)((configurationSectionHandler != null) ? configurationSectionHandler.Create(null, null, xmlDocument.DocumentElement) : null));
			}
			return result;
		}
	}
}
