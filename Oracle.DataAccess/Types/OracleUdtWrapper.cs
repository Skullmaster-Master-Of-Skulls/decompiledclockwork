using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000028 RID: 40
	internal class OracleUdtWrapper
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0001B978 File Offset: 0x0001A978
		public override string ToString()
		{
			string result = null;
			XmlTextWriter xmlTextWriter = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY)  OracleUdtWrapper::ToString()\n"
				});
			}
			try
			{
				StringWriter stringWriter = new StringWriter();
				xmlTextWriter = new XmlTextWriter(stringWriter);
				if (this.m_udtDescriptor.OracleDbType == OracleDbType.Object)
				{
					OracleUdtWrapper.GenerateXmlForObjectType(this, xmlTextWriter);
				}
				else
				{
					OracleUdtWrapper.GenerateXmlForCollectionType(this, xmlTextWriter);
				}
				result = stringWriter.ToString();
			}
			finally
			{
				xmlTextWriter.Close();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdtWrapper::ToString()\n"
				});
			}
			return result;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001BA18 File Offset: 0x0001AA18
		internal static void GenerateXmlForObjectType(OracleUdtWrapper udtWrapper, XmlTextWriter xmlWriter)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY)  OracleUdtWrapper::GenerateXmlForObjectType()\n"
				});
			}
			DataTable metaDataTable = udtWrapper.m_udtDescriptor.GetMetaDataTable();
			string[] array = new string[metaDataTable.Rows.Count];
			OracleDbType[] array2 = new OracleDbType[metaDataTable.Rows.Count];
			xmlWriter.WriteStartElement("Data");
			for (int i = 0; i < metaDataTable.Rows.Count; i++)
			{
				array[i] = metaDataTable.Rows[i]["Name"].ToString();
				array2[i] = (OracleDbType)metaDataTable.Rows[i]["ProviderType"];
				if (udtWrapper.m_udtStatusArray[i] == OracleUdtStatus.NotNull)
				{
					if (array2[i] == OracleDbType.Object || array2[i] == OracleDbType.Array)
					{
						xmlWriter.WriteElementString(array[i], ((OracleUdtWrapper)((object[])udtWrapper.m_udtData)[i]).m_udtDescriptor.UdtTypeName);
					}
					else
					{
						string text = OracleUdtWrapper.GetStringFromObject(((object[])udtWrapper.m_udtData)[i]);
						if (text.Length > 10 && OracleUdtWrapper.IsLargeDataType(array2[i]))
						{
							text = text.Substring(0, 10);
						}
						xmlWriter.WriteElementString(array[i], text);
					}
				}
				else
				{
					xmlWriter.WriteElementString(array[i], "NULL");
				}
			}
			xmlWriter.WriteEndElement();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdtWrapper::GenerateXmlForObjectType()\n"
				});
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001BBA4 File Offset: 0x0001ABA4
		internal static void GenerateXmlForCollectionType(OracleUdtWrapper udtWrapper, XmlTextWriter xmlWriter)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY)  OracleUdtWrapper::GenerateXmlForCollectionType()\n"
				});
			}
			DataTable metaDataTable = udtWrapper.m_udtDescriptor.GetMetaDataTable();
			OracleDbType dbType = (OracleDbType)metaDataTable.Rows[0]["ProviderType"];
			xmlWriter.WriteStartElement("Elements");
			if ((OracleDbType)metaDataTable.Rows[0]["ProviderType"] == OracleDbType.Object || (OracleDbType)metaDataTable.Rows[0]["ProviderType"] == OracleDbType.Array)
			{
				for (int i = 0; i < ((object[])udtWrapper.m_udtData).Length; i++)
				{
					xmlWriter.WriteStartElement("Data" + (i + 1));
					if (udtWrapper.m_udtStatusArray[i] == OracleUdtStatus.NotNull)
					{
						xmlWriter.WriteElementString("Value", ((OracleUdtWrapper)((object[])udtWrapper.m_udtData)[i]).m_udtDescriptor.UdtTypeName);
					}
					else
					{
						xmlWriter.WriteElementString("Value", "NULL");
					}
					xmlWriter.WriteEndElement();
				}
			}
			else
			{
				bool flag = OracleUdtWrapper.IsLargeDataType(dbType);
				for (int j = 0; j < ((object[])udtWrapper.m_udtData).Length; j++)
				{
					xmlWriter.WriteStartElement("Data" + (j + 1));
					if (udtWrapper.m_udtStatusArray[j] == OracleUdtStatus.NotNull)
					{
						string text = OracleUdtWrapper.GetStringFromObject(((object[])udtWrapper.m_udtData)[j]);
						if (text.Length > 10 && flag)
						{
							text = text.Substring(0, 10);
						}
						xmlWriter.WriteElementString("Value", text);
					}
					else
					{
						xmlWriter.WriteElementString("Value", "NULL");
					}
					xmlWriter.WriteEndElement();
				}
			}
			xmlWriter.WriteEndElement();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdtWrapper::GenerateXmlForCollectionType()\n"
				});
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0001BD90 File Offset: 0x0001AD90
		internal static string GetStringFromObject(object obj)
		{
			string result = string.Empty;
			if (obj is OracleBFile)
			{
				OracleBFile oracleBFile = obj as OracleBFile;
				byte[] array = new byte[10];
				if (!oracleBFile.IsNull)
				{
					int num = oracleBFile.Read(array, 0, 10);
					if (num > 0)
					{
						result = BitConverter.ToString(array, 0, num);
					}
				}
			}
			else if (obj is OracleBlob)
			{
				OracleBlob oracleBlob = obj as OracleBlob;
				byte[] array2 = new byte[10];
				if (!oracleBlob.IsNull)
				{
					int num2 = oracleBlob.Read(array2, 0, 10);
					if (num2 > 0)
					{
						result = BitConverter.ToString(array2, 0, num2);
					}
				}
			}
			else if (obj is OracleClob)
			{
				OracleClob oracleClob = obj as OracleClob;
				char[] array3 = new char[10];
				if (!oracleClob.IsNull)
				{
					int num3 = oracleClob.Read(array3, 0, 10);
					if (num3 > 0)
					{
						result = new string(array3, 0, num3);
					}
				}
			}
			else if (obj is OracleXmlType)
			{
				OracleXmlType oracleXmlType = obj as OracleXmlType;
				byte[] array4 = new byte[20];
				if (!oracleXmlType.IsNull)
				{
					int num4 = oracleXmlType.GetStream().Read(array4, 0, 20);
					if (num4 > 0)
					{
						result = new string(Encoding.Unicode.GetChars(array4), 0, num4 / 2);
					}
				}
			}
			else
			{
				result = obj.ToString();
			}
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0001BECD File Offset: 0x0001AECD
		internal static bool IsLargeDataType(OracleDbType dbType)
		{
			return dbType == OracleDbType.BFile || dbType == OracleDbType.Blob || dbType == OracleDbType.Clob || dbType == OracleDbType.NClob || dbType == OracleDbType.Long || dbType == OracleDbType.LongRaw || dbType == OracleDbType.XmlType;
		}

		// Token: 0x04000114 RID: 276
		private const string UDTElements = "Elements";

		// Token: 0x04000115 RID: 277
		private const string UDTData = "Data";

		// Token: 0x04000116 RID: 278
		private const string UDTDataValue = "Value";

		// Token: 0x04000117 RID: 279
		private const string UDTName = "Name";

		// Token: 0x04000118 RID: 280
		private const string UDTProviderType = "ProviderType";

		// Token: 0x04000119 RID: 281
		private const string UDTNullValue = "NULL";

		// Token: 0x0400011A RID: 282
		private const int MAX_LENGTH_FOR_LARGE_TYPES = 10;

		// Token: 0x0400011B RID: 283
		public object m_udtData;

		// Token: 0x0400011C RID: 284
		public OracleUdtStatus[] m_udtStatusArray;

		// Token: 0x0400011D RID: 285
		public OracleUdtDescriptor m_udtDescriptor;
	}
}
