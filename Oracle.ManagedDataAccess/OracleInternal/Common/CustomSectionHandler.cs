using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace OracleInternal.Common
{
	// Token: 0x0200003D RID: 61
	internal class CustomSectionHandler : ConfigurationSection
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00012F80 File Offset: 0x00011180
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.m_bSectionExists = true;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			CustomConfigFileReader customConfigFileReader = null;
			if (xmlDocument.DocumentElement.Name == "oracle.manageddataaccess.client")
			{
				customConfigFileReader = (ConfigBaseClass.GetInstance(true) as CustomConfigFileReader);
			}
			else if (xmlDocument.DocumentElement.Name == "oracle.unmanageddataaccess.client")
			{
				customConfigFileReader = (ConfigBaseClass.GetInstance(false) as CustomConfigFileReader);
			}
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

		// Token: 0x0400042F RID: 1071
		internal bool m_bSectionExists;
	}
}
