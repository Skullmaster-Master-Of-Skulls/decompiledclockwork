using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x020000B2 RID: 178
	internal class RegAndConfigRdr : ConfigBaseClass
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x000421C8 File Offset: 0x000403C8
		public static DataTable GetRefCursorInfoForSP(string configFileAlongWithFullPath, string schemaName, string storedProcName)
		{
			CustomConfigFileReader customConfigFileReader = ConfigBaseClass.GetInstance(true) as CustomConfigFileReader;
			if (storedProcName == null || configFileAlongWithFullPath == null)
			{
				customConfigFileReader.InitEdmMapping();
				return null;
			}
			string text = schemaName + "." + storedProcName;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[]
				{
					" (REFCURSOR) GetRefCursorInfoForSP(" + text + ")\n"
				});
			}
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(configFileAlongWithFullPath);
			RegAndConfigRdr.ODTConfigFileInfoForRefCursors odtconfigFileInfoForRefCursors = null;
			string key = configFileAlongWithFullPath.Trim();
			bool flag;
			if (flag = ConfigBaseClass.s_odtConfigNamesToRefCursorInfo.Contains(key))
			{
				odtconfigFileInfoForRefCursors = (RegAndConfigRdr.ODTConfigFileInfoForRefCursors)ConfigBaseClass.s_odtConfigNamesToRefCursorInfo[key];
				if (odtconfigFileInfoForRefCursors.lastModifiedTime != lastWriteTimeUtc)
				{
					odtconfigFileInfoForRefCursors = null;
					flag = false;
				}
			}
			if (!flag)
			{
				customConfigFileReader.InitEdmMapping();
				XmlDocument xmlDocument = new XmlDocument();
				XmlTextReader xmlTextReader = new XmlTextReader(configFileAlongWithFullPath);
				xmlDocument.Load(xmlTextReader);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("oracle.manageddataaccess.client");
				if (elementsByTagName.Count > 0)
				{
					odtconfigFileInfoForRefCursors = new RegAndConfigRdr.ODTConfigFileInfoForRefCursors
					{
						lastModifiedTime = lastWriteTimeUtc
					};
					ProviderConfig providerConfig = new ProviderConfig();
					providerConfig.ParseConfigParamsForODT(elementsByTagName[0], ref odtconfigFileInfoForRefCursors.storedProcList);
					ConfigBaseClass.s_odtConfigNamesToRefCursorInfo[key] = odtconfigFileInfoForRefCursors;
				}
				xmlTextReader.Close();
			}
			if (odtconfigFileInfoForRefCursors != null && odtconfigFileInfoForRefCursors.storedProcList.ContainsKey(text))
			{
				ConfigBaseClass.StoredProcedureInfo storedProcedureInfo = (ConfigBaseClass.StoredProcedureInfo)odtconfigFileInfoForRefCursors.storedProcList[text];
				if (storedProcedureInfo != null && (storedProcedureInfo.m_refCursors.Count > 0 || storedProcedureInfo.m_implicitlyRetRefCursors.Count > 0))
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[]
						{
							" (REFCURSOR) GetRefCursorInfoForSP(" + text + ") match found!\n"
						});
					}
					if (storedProcedureInfo.m_refCursors.Count > 0)
					{
						return storedProcedureInfo.m_refCursors[0].columnInfo.Copy();
					}
					if (storedProcedureInfo.m_implicitlyRetRefCursors.Count > 0)
					{
						return storedProcedureInfo.m_implicitlyRetRefCursors[0].columnInfo.Copy();
					}
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[]
				{
					" (REFCURSOR) GetRefCursorInfoForSP(" + text + ") no match.\n"
				});
			}
			return null;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000423F0 File Offset: 0x000405F0
		internal override void setudtmapping(out Hashtable s_mapUdtNameToMappingObj)
		{
			s_mapUdtNameToMappingObj = null;
		}

		// Token: 0x020000B3 RID: 179
		private class ODTConfigFileInfoForRefCursors
		{
			// Token: 0x04000966 RID: 2406
			internal DateTime lastModifiedTime;

			// Token: 0x04000967 RID: 2407
			internal Hashtable storedProcList = new Hashtable();
		}
	}
}
