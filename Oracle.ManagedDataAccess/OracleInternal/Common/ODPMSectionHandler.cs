using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace OracleInternal.Common
{
	// Token: 0x0200009C RID: 156
	internal class ODPMSectionHandler : ConfigurationSection
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0003E360 File Offset: 0x0003C560
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			if (customConfigFileReader != null)
			{
				customConfigFileReader.ValidateBaseDocument(xmlDocument);
				if (ConfigBaseClass.m_ParseMode == ParseMode.ReParseTnsNames)
				{
					customConfigFileReader.ParseClientXmlNode(xmlDocument.DocumentElement, ref customConfigFileReader.s_storedProcInformation, ref ConfigBaseClass.m_versionSpecificNodesList, new ArrayList
					{
						"dataSources"
					});
					return;
				}
				if (ConfigBaseClass.m_ParseMode == ParseMode.FirstParse)
				{
					customConfigFileReader.ParseClientXmlNode(xmlDocument.DocumentElement, ref customConfigFileReader.s_storedProcInformation, ref ConfigBaseClass.m_versionSpecificNodesList, null);
				}
			}
		}
	}
}
