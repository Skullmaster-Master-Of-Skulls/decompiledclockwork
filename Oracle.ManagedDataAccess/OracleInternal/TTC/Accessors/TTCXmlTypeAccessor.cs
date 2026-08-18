using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000213 RID: 531
	internal class TTCXmlTypeAccessor : TTCNamedTypeAccessor
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x000CF9CC File Offset: 0x000CDBCC
		internal TTCXmlTypeAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind, "SYS.XMLTYPE")
		{
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000CF9DC File Offset: 0x000CDBDC
		internal void UnpickleXmlType(OracleConnectionImpl connImpl, DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, OraXmlTypeHeader xmlTypeHeader, out OraXmlTypeData xmlTypeData)
		{
			int maxSize = this.m_totalLengthOfData[currentRow];
			xmlTypeData = null;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(maxSize, ref maxSize);
				this.UnpickleXmlType(connImpl, this.m_colDataSegments, xmlTypeHeader, out xmlTypeData);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000CFA60 File Offset: 0x000CDC60
		internal void UnpickleXmlType(OracleConnectionImpl connImpl, int currentRow, OraXmlTypeHeader xmlTypeHeader, out OraXmlTypeData xmlTypeData)
		{
			List<ArraySegment<byte>> dataSegments = this.m_RowDataSegments[currentRow];
			this.UnpickleXmlType(connImpl, dataSegments, xmlTypeHeader, out xmlTypeData);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000CFA88 File Offset: 0x000CDC88
		internal void UnpickleXmlType(OracleConnectionImpl connImpl, List<ArraySegment<byte>> dataSegments, OraXmlTypeHeader xmlTypeHeader, out OraXmlTypeData xmlTypeData)
		{
			xmlTypeData = null;
			try
			{
				TTCXmlTypePickler.ReadXmlHeader(connImpl, dataSegments, xmlTypeHeader);
				if (xmlTypeHeader.m_dataLength > 0L)
				{
					xmlTypeData = new OraXmlTypeData();
					this.UnpickleXmlData(connImpl, dataSegments, xmlTypeHeader, xmlTypeData);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000CFAD8 File Offset: 0x000CDCD8
		internal void UnpickleXmlData(OracleConnectionImpl connImpl, List<ArraySegment<byte>> dataSegments, OraXmlTypeHeader xmlTypeHeader, OraXmlTypeData xmlTypeData)
		{
			if (TypeOfXmlType.Clob == xmlTypeHeader.m_typeOfXmlType)
			{
				byte[] array = new byte[xmlTypeHeader.m_dataLength];
				Accessor.CopyDataToUserBuffer(dataSegments, xmlTypeHeader.m_headerLength, array, 0, (int)xmlTypeHeader.m_dataLength);
				xmlTypeData.m_typeOfXmlData = TypeOfXmlData.Clob;
				xmlTypeData.m_xmlLobLocator = array;
			}
			else if ((TypeOfXmlType)2147483649U == xmlTypeHeader.m_typeOfXmlType)
			{
				int num = (int)xmlTypeHeader.m_dataLength - 2;
				byte[] array2 = new byte[2];
				Accessor.CopyDataToUserBuffer(dataSegments, xmlTypeHeader.m_headerLength + num, array2, 0, 2);
				xmlTypeData.m_csid = TTCXmlTypePickler.ReadShortInNativeFormat(array2);
				byte[] array3 = new byte[num];
				Accessor.CopyDataToUserBuffer(dataSegments, xmlTypeHeader.m_headerLength, array3, 0, num);
				xmlTypeData.m_typeOfXmlData = TypeOfXmlData.BlobWithText;
				xmlTypeData.m_xmlLobLocator = array3;
			}
			else if (TypeOfXmlType.BlobCSX == xmlTypeHeader.m_typeOfXmlType)
			{
				byte[] array4 = new byte[xmlTypeHeader.m_dataLength];
				Accessor.CopyDataToUserBuffer(dataSegments, xmlTypeHeader.m_headerLength, array4, 0, (int)xmlTypeHeader.m_dataLength);
				xmlTypeData.m_xmlLobLocator = array4;
				xmlTypeData.m_typeOfXmlData = TypeOfXmlData.BlobCSX;
			}
			else if (TypeOfXmlType.String == xmlTypeHeader.m_typeOfXmlType)
			{
				int num2 = (int)xmlTypeHeader.m_dataLength;
				char[] array5 = new char[num2];
				this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(dataSegments, xmlTypeHeader.m_headerLength, (int)xmlTypeHeader.m_dataLength, array5, 0, ref num2, true);
				xmlTypeData.m_typeOfXmlData = TypeOfXmlData.Chars;
				Array.Resize<char>(ref array5, num2);
				xmlTypeData.m_xmlChars = array5;
			}
			else
			{
				if (TypeOfXmlType.Object == xmlTypeHeader.m_typeOfXmlType)
				{
					throw new OracleException(ResourceStringConstants.XML_NOT_SUPPORTED_IMAGE_TYPE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.XML_NOT_SUPPORTED_IMAGE_TYPE, new string[]
					{
						"Object-relational",
						connImpl.m_pm.m_serverVersion
					}));
				}
				throw new OracleException(ResourceStringConstants.XML_NOT_SUPPORTED_IMAGE_TYPE, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.XML_NOT_SUPPORTED_IMAGE_TYPE, new string[]
				{
					"Unknonwn",
					connImpl.m_pm.m_serverVersion
				}));
			}
		}
	}
}
