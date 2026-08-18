using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.Core;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B7 RID: 439
	internal class OracleParameterImpl
	{
		// Token: 0x0600109B RID: 4251 RVA: 0x000B5694 File Offset: 0x000B3894
		internal OracleParameterImpl()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000B56EC File Offset: 0x000B38EC
		private object ExtractDecimalFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				byte[] byteRepresentation = ttcnumberAccessor.GetByteRepresentation(currentRow);
				object obj;
				if (this.m_precision != 100 || this.m_scale != 129)
				{
					OracleDecimal value = new OracleDecimal(byteRepresentation, false);
					OracleDecimal oracleDecimal = OracleDecimal.Null;
					if (this.m_precision != 100 && this.m_scale != 129)
					{
						oracleDecimal = OracleDecimal.ConvertToPrecScale(value, (int)this.m_precision, (int)this.m_scale);
					}
					else if (this.m_precision != 100)
					{
						oracleDecimal = OracleDecimal.SetPrecision(value, (int)this.m_precision);
					}
					else if (this.m_scale != 129)
					{
						oracleDecimal = OracleDecimal.AdjustScale(value, (int)this.m_scale, true);
					}
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = oracleDecimal;
					}
					else
					{
						obj = oracleDecimal.Value;
					}
				}
				else if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleDecimal(byteRepresentation, false);
				}
				else
				{
					obj = DecimalConv.GetDecimal(byteRepresentation, 0, byteRepresentation.Length);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000B5840 File Offset: 0x000B3A40
		private object ExtractDoubleFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj = ttcnumberAccessor.GetDouble(currentRow);
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleDecimal((double)obj);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000B58DC File Offset: 0x000B3ADC
		private object ExtractSingleFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					byte[] numBytes = OracleNumberCore.lnxfpr(ttcnumberAccessor.GetByteRepresentation(currentRow), 7);
					obj = new OracleDecimal(numBytes, false);
				}
				else
				{
					obj = ttcnumberAccessor.GetFloat(currentRow);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000B5984 File Offset: 0x000B3B84
		private object ExtractIntFromAccessor(Accessor accessor, PrmEnumType enumType, OracleDbType oraDbType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					byte[] byteRepresentation = ttcnumberAccessor.GetByteRepresentation(currentRow);
					obj = new OracleDecimal(byteRepresentation, false);
				}
				else
				{
					obj = ttcnumberAccessor.GetInt(currentRow);
					if (oraDbType == OracleDbType.Int16)
					{
						obj = Convert.ToInt16(obj);
					}
					else if (oraDbType == OracleDbType.Byte)
					{
						obj = Convert.ToByte(obj);
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000B5A4C File Offset: 0x000B3C4C
		private object ExtractRefCursorFromAccessor(OracleConnection conn, Accessor accessor, long fetchSize, PrmEnumType enumType, OracleIntervalDS sessionTimeZone, string commandText, string paramPosOrName, long longFetchSize, long clientInitialLOBFS, long internalInitialLOBFS, long[] scnFromExecution, int currentRow, bool bCallFromExecuteReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCRefCursorAccessor ttcrefCursorAccessor = accessor as TTCRefCursorAccessor;
				OracleRefCursorImpl oracleRefCursorImpl = new OracleRefCursorImpl(ttcrefCursorAccessor.GetResultSet(currentRow));
				OracleRefCursor oracleRefCursor = new OracleRefCursor(conn, oracleRefCursorImpl, sessionTimeZone, commandText, paramPosOrName, longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, false);
				if (fetchSize > 0L)
				{
					oracleRefCursor.FetchSize = fetchSize;
				}
				if (oracleRefCursorImpl.m_sqlMetaData != null && oracleRefCursorImpl.m_sqlMetaData.m_maxRowSize == 0)
				{
					oracleRefCursorImpl.m_sqlMetaData.CalculateRowSize();
				}
				object obj;
				if (bCallFromExecuteReader || PrmEnumType.ORADBTYPE == enumType)
				{
					obj = oracleRefCursor;
				}
				else
				{
					obj = oracleRefCursor.GetDataReader(false);
					oracleRefCursor.Dispose();
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000B5B34 File Offset: 0x000B3D34
		private object ExtractLongFromAccessor(object accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				byte[] byteRepresentation = ttcnumberAccessor.GetByteRepresentation(currentRow);
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleDecimal(byteRepresentation, false);
				}
				else
				{
					obj = OracleNumberCore.lnxsni(byteRepresentation);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000B5BD8 File Offset: 0x000B3DD8
		private object ExtractBDoubleFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCBinaryDoubleAccessor ttcbinaryDoubleAccessor = accessor as TTCBinaryDoubleAccessor;
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleDecimal(ttcbinaryDoubleAccessor.GetValue(currentRow));
				}
				else
				{
					obj = ttcbinaryDoubleAccessor.GetValue(currentRow);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000B5C78 File Offset: 0x000B3E78
		private object ExtractBFloatFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCBinaryFloatAccessor ttcbinaryFloatAccessor = accessor as TTCBinaryFloatAccessor;
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleDecimal(ttcbinaryFloatAccessor.GetValue(currentRow));
				}
				else
				{
					obj = ttcbinaryFloatAccessor.GetValue(currentRow);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000B5D18 File Offset: 0x000B3F18
		private object ExtractCharFromAccessor(Accessor accessor, PrmEnumType enumType, byte charSetForm, int maxSizeRequested, int currentRow, char[] charArrayFromPooler, out int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCVarcharAccessor ttcvarcharAccessor = accessor as TTCVarcharAccessor;
				string text = string.Empty;
				if (maxSizeRequested > 0)
				{
					text = ttcvarcharAccessor.GetString(currentRow, charSetForm, charArrayFromPooler);
					if (maxSizeRequested < text.Length)
					{
						text = text.Substring(0, maxSizeRequested);
					}
				}
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleString(text);
				}
				else
				{
					obj = text;
				}
				length = text.Length;
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000B5DDC File Offset: 0x000B3FDC
		private object ExtractRawFromAccessor(Accessor accessor, PrmEnumType enumType, int maxLength, int currentRow, out int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCRawAccessor ttcrawAccessor = accessor as TTCRawAccessor;
				byte[] array = new byte[0];
				if (maxLength > 0)
				{
					array = ttcrawAccessor.GetByteRepresentation(currentRow);
					if (maxLength < array.Length)
					{
						Array.Resize<byte>(ref array, maxLength);
					}
				}
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleBinary(array, false);
				}
				else
				{
					obj = array;
				}
				length = array.Length;
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x000B5E94 File Offset: 0x000B4094
		private object ExtractDateFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleDate(ttcdateTimeAccessor.GetByteRepresentation(currentRow));
				}
				else
				{
					obj = ttcdateTimeAccessor.GetDateTime(currentRow);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000B5F34 File Offset: 0x000B4134
		private object ExtractTimeStampFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleTimeStamp(ttcdateTimeAccessor.GetByteRepresentation(currentRow));
				}
				else
				{
					obj = ttcdateTimeAccessor.GetDateTime(currentRow);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000B5FD4 File Offset: 0x000B41D4
		private object ExtractTimeStampTZFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow, OracleConnectionImpl connImpl, bool asDateTimeOffset = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				byte[] byteRepresentation = ttcdateTimeAccessor.GetByteRepresentation(currentRow);
				object obj;
				if (connImpl.IsTZDataSentAsLocalTime)
				{
					byte[] binData = null;
					DateTime? dateTime = null;
					TimeStamp.GetUTCByteRepFromLocalArray(byteRepresentation, out binData, out dateTime, false);
					obj = new OracleTimeStampTZ(binData);
				}
				else
				{
					obj = new OracleTimeStampTZ(byteRepresentation);
				}
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					result = obj;
				}
				else
				{
					obj = ((OracleTimeStampTZ)obj).Value;
					if (asDateTimeOffset && obj != null)
					{
						obj = new DateTimeOffset((DateTime)obj);
					}
					result = obj;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000B60C8 File Offset: 0x000B42C8
		private object ExtractTimeStampLTZFromAccessor(Accessor accessor, PrmEnumType enumType, OracleConnectionImpl connImpl, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj = null;
				DateTime? dateTime = null;
				byte[] byteRepresentation = ttcdateTimeAccessor.GetByteRepresentation(currentRow);
				if (byteRepresentation != null)
				{
					byte[] binData = null;
					TimeStamp.ConvertDBTimeToLTZData(byteRepresentation, connImpl.GetDBTimeZoneBytes(), connImpl.m_sessionTimeZone, out binData, out dateTime, false);
					obj = new OracleTimeStampLTZ(binData, false);
				}
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					result = obj;
				}
				else
				{
					result = ((OracleTimeStampLTZ)obj).Value;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000B619C File Offset: 0x000B439C
		private object ExtractIntervalYMFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCIntervalTypeAccessor ttcintervalTypeAccessor = accessor as TTCIntervalTypeAccessor;
				byte[] byteRepresentation = ttcintervalTypeAccessor.GetByteRepresentation(currentRow);
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleIntervalYM(byteRepresentation);
				}
				else
				{
					obj = OracleIntervalYM.GetLong(byteRepresentation, OracleDbType.IntervalYM, 0, -1);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000B6240 File Offset: 0x000B4440
		private object ExtractIntervalDSFromAccessor(Accessor accessor, PrmEnumType enumType, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCIntervalTypeAccessor ttcintervalTypeAccessor = accessor as TTCIntervalTypeAccessor;
				byte[] byteRepresentation = ttcintervalTypeAccessor.GetByteRepresentation(currentRow);
				object obj;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					obj = new OracleIntervalDS(byteRepresentation);
				}
				else
				{
					obj = OracleIntervalDS.GetTimeSpan(byteRepresentation, OracleDbType.IntervalDS, 0, -1);
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000B62E4 File Offset: 0x000B44E4
		internal ColumnDescribeInfo GetParameterMetaData(OracleParameter parameter, ColumnDescribeInfo cachedParamMetadata, ref bool bMetadataModified)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			ColumnDescribeInfo result;
			try
			{
				ColumnDescribeInfo columnDescribeInfo;
				if (!bMetadataModified && cachedParamMetadata == null)
				{
					if (cachedParamMetadata == null)
					{
						columnDescribeInfo = new ColumnDescribeInfo();
					}
					else
					{
						columnDescribeInfo = cachedParamMetadata;
					}
					if (!parameter.m_bDuplicateBind)
					{
						columnDescribeInfo.m_flag = 3;
					}
					else
					{
						columnDescribeInfo.m_flag = 128;
					}
					if (parameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
					{
						ColumnDescribeInfo columnDescribeInfo2 = columnDescribeInfo;
						columnDescribeInfo2.m_flag |= 64;
					}
					if (parameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
					{
						columnDescribeInfo.m_maxNoOfArrayElements = parameter.m_maxNoOfArrayElements;
					}
					else
					{
						columnDescribeInfo.m_maxNoOfArrayElements = 0;
					}
					columnDescribeInfo.m_dataType = (short)parameter.m_oraType;
					columnDescribeInfo.m_precision = 0;
					columnDescribeInfo.m_scale = 0;
					if (parameter.m_oraType == OraType.ORA_CHAR || parameter.m_oraType == OraType.ORA_CHARN || parameter.m_oraType == OraType.ORA_VARCHAR)
					{
						columnDescribeInfo.m_contFlag = 16;
					}
					else
					{
						columnDescribeInfo.m_contFlag = 0;
					}
					columnDescribeInfo.m_maxLength = parameter.m_maxBytesToBeWrittenOrRead;
					columnDescribeInfo.m_maxLengthOfChars = parameter.m_maxCharsToBeWrittenOrRead;
					if (parameter.m_oraType == OraType.ORA_XMLTYPE)
					{
						columnDescribeInfo.bIsXmlType = true;
						columnDescribeInfo.m_toid = TTCXmlTypePickler.TOID;
						columnDescribeInfo.m_version = 1;
						columnDescribeInfo.m_characterSetId = 2;
					}
					else
					{
						columnDescribeInfo.m_toid = null;
						columnDescribeInfo.m_version = 0;
						columnDescribeInfo.m_characterSetId = parameter.m_characterSetId;
					}
					columnDescribeInfo.m_characterSetForm = (short)parameter.m_charSetForm;
				}
				else
				{
					short num = 3;
					if (parameter.m_bDuplicateBind)
					{
						num = 128;
					}
					if (parameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
					{
						num |= 64;
					}
					if (num != cachedParamMetadata.m_flag)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_flag = num;
					}
					int num2 = 0;
					if (parameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
					{
						num2 = parameter.m_maxNoOfArrayElements;
					}
					if (num2 != cachedParamMetadata.m_maxNoOfArrayElements)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_maxNoOfArrayElements = num2;
					}
					if ((short)parameter.m_oraType != cachedParamMetadata.m_dataType)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_dataType = (short)parameter.m_oraType;
					}
					if ((short)parameter.Precision != cachedParamMetadata.m_precision)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_precision = 0;
					}
					if ((short)parameter.Scale != cachedParamMetadata.m_scale)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_scale = 0;
					}
					int num3 = 0;
					if (parameter.m_oraType == OraType.ORA_CHAR || parameter.m_oraType == OraType.ORA_CHARN || parameter.m_oraType == OraType.ORA_VARCHAR)
					{
						num3 = 16;
					}
					if (cachedParamMetadata.m_contFlag != num3)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_contFlag = num3;
					}
					if (cachedParamMetadata.m_maxLength != parameter.m_maxBytesToBeWrittenOrRead)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_maxLength = parameter.m_maxBytesToBeWrittenOrRead;
					}
					if (cachedParamMetadata.m_maxLengthOfChars != parameter.m_maxCharsToBeWrittenOrRead)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_maxLengthOfChars = parameter.m_maxCharsToBeWrittenOrRead;
					}
					if (parameter.m_oraType != OraType.ORA_XMLTYPE)
					{
						cachedParamMetadata.m_toid = null;
						cachedParamMetadata.m_version = 0;
					}
					else
					{
						cachedParamMetadata.m_toid = TTCXmlTypePickler.TOID;
						cachedParamMetadata.m_version = 1;
						cachedParamMetadata.m_characterSetId = 2;
					}
					if (cachedParamMetadata.m_characterSetForm != (short)parameter.m_charSetForm)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_characterSetForm = (short)parameter.m_charSetForm;
					}
					if (cachedParamMetadata.m_characterSetId != parameter.m_characterSetId)
					{
						bMetadataModified = true;
						cachedParamMetadata.m_characterSetId = parameter.m_characterSetId;
					}
					columnDescribeInfo = cachedParamMetadata;
				}
				result = columnDescribeInfo;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000B661C File Offset: 0x000B481C
		internal int SetCharDataArrayInBytes(OracleConnectionImpl connImpl, object paramValue, int[] bindSize, int offset, int noOfElems, bool[] nullIndicatorsForArrayBind, byte charSetForm)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						int num2 = this.SetCharDataInBytes(connImpl, value, bindSize[i], offset, out this.m_paramValForArrayBindInBytes[i], charSetForm);
						if (num < num2)
						{
							num = num2;
						}
					}
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000B66D8 File Offset: 0x000B48D8
		internal void SetRawDataArrayInBytes(object paramValue, int[] bindSize, int offset, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetRawDataInBytes(value, bindSize[i], offset, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000B6780 File Offset: 0x000B4980
		internal int SetCharDataInBytes(OracleConnectionImpl connImpl, object paramValue, int size, int offset, byte charSetForm)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				int num = this.SetCharDataInBytes(connImpl, paramValue, size, offset, out this.m_paramValInBytes, charSetForm);
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000B6804 File Offset: 0x000B4A04
		internal int SetCharDataInBytes(OracleConnectionImpl connImpl, object paramValue, int size, int offset, out byte[] charByteArray, byte charSetForm)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				string str;
				char[] chars;
				if ((str = (paramValue as string)) != null)
				{
					if (charSetForm != 2)
					{
						charByteArray = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(str, offset, size, true);
					}
					else
					{
						charByteArray = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertStringToBytes(str, offset, size, true);
					}
				}
				else if ((chars = (paramValue as char[])) != null)
				{
					if (charSetForm != 2)
					{
						charByteArray = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertCharsToBytes(chars, offset, size, true);
					}
					else
					{
						charByteArray = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertCharsToBytes(chars, offset, size, true);
					}
				}
				else if (paramValue is OracleString)
				{
					str = ((OracleString)paramValue).Value;
					if (charSetForm != 2)
					{
						charByteArray = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(str, offset, size, true);
					}
					else
					{
						charByteArray = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertStringToBytes(str, offset, size, true);
					}
				}
				else if (paramValue is char)
				{
					char[] array = new char[]
					{
						(char)paramValue
					};
					if (charSetForm != 2)
					{
						charByteArray = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertCharsToBytes(array, 0, array.Length, true);
					}
					else
					{
						charByteArray = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertCharsToBytes(array, 0, array.Length, true);
					}
				}
				else
				{
					str = Convert.ToString(paramValue);
					if (charSetForm != 2)
					{
						charByteArray = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(str, 0, size, true);
					}
					else
					{
						charByteArray = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertStringToBytes(str, 0, size, true);
					}
				}
				result = charByteArray.Length;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000B6A10 File Offset: 0x000B4C10
		internal void SetInt32DataInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetInt32DataInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000B6A8C File Offset: 0x000B4C8C
		internal void SetInt32DataInBytes(object paramValue, out byte[] int32ByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if (paramValue is int)
				{
					int32ByteArray = OracleNumberCore.lnxmin(Convert.ToInt64((int)paramValue));
				}
				else if (paramValue is OracleDecimal)
				{
					int32ByteArray = ((OracleDecimal)paramValue).InternalByteRepresentation;
				}
				else if (paramValue is OracleString)
				{
					int32ByteArray = OracleNumberCore.lnxmin(Convert.ToInt64(((OracleString)paramValue).Value));
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					int32ByteArray = array;
				}
				else
				{
					long longNum = Convert.ToInt64(paramValue);
					int32ByteArray = OracleNumberCore.lnxmin(longNum);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000B6B78 File Offset: 0x000B4D78
		internal void SetPlsqlBooleanDataInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetPlsqlBooleanDataInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000B6BF4 File Offset: 0x000B4DF4
		internal void SetPlsqlBooleanDataInBytes(object paramValue, out byte[] byteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				bool flag;
				if (paramValue is bool)
				{
					flag = (bool)paramValue;
				}
				else if (paramValue is OracleBoolean)
				{
					flag = ((OracleBoolean)paramValue).Value;
				}
				else
				{
					flag = Convert.ToBoolean(paramValue);
				}
				if (flag)
				{
					byteArray = TTCPLSQLBooleanAccessor.TRUE_VAL_BYTES;
				}
				else
				{
					byteArray = TTCPLSQLBooleanAccessor.FALSE_VAL_BYTES;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000B6CA8 File Offset: 0x000B4EA8
		internal void SetInt64DataInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetInt64DataInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000B6D24 File Offset: 0x000B4F24
		internal void SetInt64DataInBytes(object paramValue, out byte[] int64ByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if (paramValue is long)
				{
					int64ByteArray = OracleNumberCore.lnxmin((long)paramValue);
				}
				else if (paramValue is OracleDecimal)
				{
					int64ByteArray = ((OracleDecimal)paramValue).InternalByteRepresentation;
				}
				else if (paramValue is OracleString)
				{
					int64ByteArray = OracleNumberCore.lnxmin(Convert.ToInt64(((OracleString)paramValue).Value));
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					int64ByteArray = array;
				}
				else
				{
					long longNum = Convert.ToInt64(paramValue);
					int64ByteArray = OracleNumberCore.lnxmin(longNum);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000B6E08 File Offset: 0x000B5008
		internal void SetInt32ArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetInt32DataInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000B6EAC File Offset: 0x000B50AC
		internal void SetInt64ArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetInt64DataInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000B6F50 File Offset: 0x000B5150
		internal void SetBinaryDoubleArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetBinaryDoubleInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000B6FF4 File Offset: 0x000B51F4
		internal void SetClobArrayDataInBytes(OracleConnectionImpl connImpl, bool bIsNClob, object paramValue, int offset, int maxSize, int[] maxArrayBindSize, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_saveValue = new object[noOfElems];
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetClobDataInBytes(connImpl, bIsNClob, value, offset, maxSize, maxArrayBindSize, out this.m_paramValForArrayBindInBytes[i], out this.m_saveValue[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000B70B8 File Offset: 0x000B52B8
		internal void SetBlobArrayDataInBytes(OracleConnectionImpl connImpl, object paramValue, int offSet, int maxSize, int[] maxArrayBindSize, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_saveValue = new object[noOfElems];
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetBlobDataInBytes(connImpl, value, offSet, maxSize, maxArrayBindSize, out this.m_paramValForArrayBindInBytes[i], out this.m_saveValue[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000B717C File Offset: 0x000B537C
		internal void SetBFileArrayInBytes(OracleConnectionImpl connImpl, object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetBFileDataInBytes(connImpl, value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000B7220 File Offset: 0x000B5420
		internal void SetTimeStampArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetTimeStampInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000B72C4 File Offset: 0x000B54C4
		internal void SetTimeStampLTZArrayInBytes(OracleConnectionImpl connImpl, object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetTimeStampLTZInBytes(connImpl, value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000B7368 File Offset: 0x000B5568
		internal void SetTimeStampTZArrayInBytes(OracleConnectionImpl connImpl, object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetTimeStampTZInBytes(connImpl, value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000B740C File Offset: 0x000B560C
		internal void SetIntervalYMArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetIntervalYMInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000B74B0 File Offset: 0x000B56B0
		internal void SetIntervalDSArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetIntervalDSInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000B7554 File Offset: 0x000B5754
		internal void SetBinaryFloatArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetBinaryFloatInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000B75F8 File Offset: 0x000B57F8
		internal void SetDecimalArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetDecimalDataInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000B769C File Offset: 0x000B589C
		internal void SetDateArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetDateInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000B7740 File Offset: 0x000B5940
		internal void SetPlsqlBooleanArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetPlsqlBooleanDataInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000B77E4 File Offset: 0x000B59E4
		internal void SetRefCursorArrayInBytes(OracleConnectionImpl connImpl, object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetRefCursorDataInBytes(connImpl, value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000B7888 File Offset: 0x000B5A88
		internal void SetDecimalDataInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetDecimalDataInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000B7904 File Offset: 0x000B5B04
		internal void SetDecimalDataInBytes(object paramValue, out byte[] decimalByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleDecimal value;
				byte[] array;
				if (paramValue is decimal)
				{
					value = new OracleDecimal((decimal)paramValue);
				}
				else if (paramValue is byte)
				{
					value = new OracleDecimal((int)((byte)paramValue));
				}
				else if (paramValue is short)
				{
					value = new OracleDecimal((int)((short)paramValue));
				}
				else if (paramValue is int)
				{
					value = new OracleDecimal((int)paramValue);
				}
				else if (paramValue is long)
				{
					value = new OracleDecimal((long)paramValue);
				}
				else if (paramValue is float)
				{
					value = new OracleDecimal((float)paramValue);
					value = OracleDecimal.SetPrecisionNoRound(value, 7);
				}
				else if (paramValue is double)
				{
					value = new OracleDecimal((double)paramValue);
				}
				else if (paramValue is OracleDecimal)
				{
					value = (OracleDecimal)paramValue;
				}
				else if (paramValue is OracleString)
				{
					string value2 = ((OracleString)paramValue).Value;
					value = new OracleDecimal(value2);
				}
				else if (paramValue is string || paramValue is char || paramValue is char[])
				{
					string preBindBuffer_Str = this.GetPreBindBuffer_Str(paramValue);
					value = new OracleDecimal(preBindBuffer_Str);
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					if (array.Length != 22)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					value = new OracleDecimal(array);
				}
				else if (paramValue is bool)
				{
					value = new OracleDecimal((int)(((bool)paramValue) ? 1 : 0));
				}
				else
				{
					value = new OracleDecimal(Convert.ToDecimal(paramValue));
				}
				if (this.m_precision != 100 || this.m_scale != 129)
				{
					OracleDecimal oracleDecimal = OracleDecimal.Null;
					if (this.m_precision != 100 && this.m_scale != 129)
					{
						oracleDecimal = OracleDecimal.ConvertToPrecScale(value, (int)this.m_precision, (int)this.m_scale);
					}
					else if (this.m_precision != 100)
					{
						oracleDecimal = OracleDecimal.SetPrecision(value, (int)this.m_precision);
					}
					else if (this.m_scale != 129)
					{
						oracleDecimal = OracleDecimal.AdjustScale(value, (int)this.m_scale, true);
					}
					decimalByteArray = oracleDecimal.InternalByteRepresentation;
				}
				else
				{
					decimalByteArray = value.InternalByteRepresentation;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000B7B98 File Offset: 0x000B5D98
		internal void SetBinaryDoubleInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetBinaryDoubleInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000B7C14 File Offset: 0x000B5E14
		internal void SetBinaryDoubleInBytes(object paramValue, out byte[] bdoubleByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if (paramValue is double)
				{
					bdoubleByteArray = TTCBinaryDoubleAccessor.DoubleToCanonicalFormatBytes((double)paramValue);
				}
				else if (paramValue is OracleDecimal)
				{
					bdoubleByteArray = TTCBinaryDoubleAccessor.DoubleToCanonicalFormatBytes(((OracleDecimal)paramValue).ToDouble());
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					if (array.Length != TTCBinaryDoubleAccessor.BINARY_DOUBLE_MAX_LENGTH)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					bdoubleByteArray = array;
				}
				else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
				{
					string preBindBuffer_Str = this.GetPreBindBuffer_Str(paramValue);
					OracleDecimal oracleDecimal = new OracleDecimal(preBindBuffer_Str);
					bdoubleByteArray = TTCBinaryDoubleAccessor.DoubleToCanonicalFormatBytes(oracleDecimal.ToDouble());
				}
				else
				{
					double d = Convert.ToDouble(paramValue);
					bdoubleByteArray = TTCBinaryDoubleAccessor.DoubleToCanonicalFormatBytes(d);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000B7D48 File Offset: 0x000B5F48
		internal void SetDoubleInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetDoubleInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000B7DC4 File Offset: 0x000B5FC4
		internal void SetDoubleInBytes(object paramValue, out byte[] doubleByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				double doubleNum;
				byte[] array;
				if (paramValue is double)
				{
					doubleNum = (double)paramValue;
				}
				else if (paramValue is OracleDecimal)
				{
					doubleNum = ((OracleDecimal)paramValue).ToDouble();
				}
				else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
				{
					string preBindBuffer_Str = this.GetPreBindBuffer_Str(paramValue);
					doubleNum = new OracleDecimal(preBindBuffer_Str).ToDouble();
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					if (array.Length != 22)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]));
					}
					doubleNum = new OracleDecimal(array).ToDouble();
				}
				else
				{
					doubleNum = Convert.ToDouble(paramValue);
				}
				doubleByteArray = OracleNumberCore.GetByteRep(doubleNum);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000B7EE4 File Offset: 0x000B60E4
		internal void SetDoubleArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetDoubleInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000B7F88 File Offset: 0x000B6188
		internal void SetSingleInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetSingleInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000B8004 File Offset: 0x000B6204
		internal void SetSingleInBytes(object paramValue, out byte[] singeByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleDecimal value;
				byte[] numBytes;
				if (paramValue is float)
				{
					value = new OracleDecimal((double)((float)paramValue));
				}
				else if (paramValue is OracleDecimal)
				{
					value = (OracleDecimal)paramValue;
				}
				else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
				{
					string preBindBuffer_Str = this.GetPreBindBuffer_Str(paramValue);
					value = new OracleDecimal(preBindBuffer_Str);
				}
				else if ((numBytes = (paramValue as byte[])) != null)
				{
					value = new OracleDecimal(numBytes);
				}
				else
				{
					value = new OracleDecimal((double)Convert.ToSingle(paramValue));
				}
				singeByteArray = OracleDecimal.SetPrecisionNoRound(value, 7).InternalByteRepresentation;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000B8100 File Offset: 0x000B6300
		internal void SetSingleArrayInBytes(object paramValue, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetSingleInBytes(value, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000B81A4 File Offset: 0x000B63A4
		internal void SetBinaryFloatInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetBinaryFloatInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000B8220 File Offset: 0x000B6420
		internal void SetBinaryFloatInBytes(object paramValue, out byte[] bfloatByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if (paramValue is float)
				{
					bfloatByteArray = TTCBinaryFloatAccessor.FloatToCanonicalFormatBytes((float)paramValue);
				}
				else if (paramValue is OracleDecimal)
				{
					bfloatByteArray = TTCBinaryFloatAccessor.FloatToCanonicalFormatBytes(((OracleDecimal)paramValue).ToSingle());
				}
				else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
				{
					string preBindBuffer_Str = this.GetPreBindBuffer_Str(paramValue);
					OracleDecimal oracleDecimal = new OracleDecimal(preBindBuffer_Str);
					bfloatByteArray = TTCBinaryFloatAccessor.FloatToCanonicalFormatBytes(oracleDecimal.ToSingle());
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					if (array.Length != TTCBinaryFloatAccessor.BINARY_FLOAT_MAX_LENGTH)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					bfloatByteArray = array;
				}
				else
				{
					float f = Convert.ToSingle(paramValue);
					bfloatByteArray = TTCBinaryFloatAccessor.FloatToCanonicalFormatBytes(f);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000B8354 File Offset: 0x000B6554
		internal void SetTimeStampInBytes(object bindValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetTimeStampInBytes(bindValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000B83D0 File Offset: 0x000B65D0
		internal void SetTimeStampInBytes(object bindValue, out byte[] timeStampByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if ((array = (bindValue as byte[])) != null)
				{
					if (array.Length != 11)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
				}
				else
				{
					OracleTimeStamp oracleTimeStamp;
					if (bindValue is DateTime)
					{
						oracleTimeStamp = new OracleTimeStamp((DateTime)bindValue);
					}
					else if (bindValue is OracleTimeStamp)
					{
						oracleTimeStamp = (OracleTimeStamp)bindValue;
					}
					else if (bindValue is OracleTimeStampLTZ)
					{
						oracleTimeStamp = ((OracleTimeStampLTZ)bindValue).ToOracleTimeStamp();
					}
					else if (bindValue is OracleTimeStampTZ)
					{
						oracleTimeStamp = ((OracleTimeStampTZ)bindValue).ToOracleTimeStamp();
					}
					else if (bindValue is OracleDate)
					{
						oracleTimeStamp = (OracleDate)bindValue;
					}
					else
					{
						if (!(bindValue is string) && !(bindValue is char[]) && !(bindValue is OracleString) && !(bindValue is char))
						{
							throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
						}
						string preBindBuffer_Str = this.GetPreBindBuffer_Str(bindValue);
						oracleTimeStamp = new OracleTimeStamp(preBindBuffer_Str);
					}
					array = oracleTimeStamp.InternalByteRepresentation;
				}
				bool flag = false;
				for (int i = 7; i < array.Length; i++)
				{
					if (array[i] != 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					timeStampByteArray = new byte[7];
					Array.Copy(array, timeStampByteArray, 7);
				}
				else
				{
					timeStampByteArray = array;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000B858C File Offset: 0x000B678C
		internal void SetTimeStampLTZInBytes(OracleConnectionImpl connImpl, object bindValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetTimeStampLTZInBytes(connImpl, bindValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000B8608 File Offset: 0x000B6808
		internal void SetTimeStampLTZInBytes(OracleConnectionImpl connImpl, object bindValue, out byte[] timeStampTZByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if ((array = (bindValue as byte[])) != null)
				{
					if (array.Length != 11)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
				}
				else
				{
					OracleTimeStampLTZ oracleTimeStampLTZ;
					if (bindValue is DateTime)
					{
						oracleTimeStampLTZ = new OracleTimeStampLTZ((DateTime)bindValue);
					}
					else if (bindValue is OracleTimeStamp)
					{
						oracleTimeStampLTZ = ((OracleTimeStamp)bindValue).ToOracleTimeStampLTZ();
					}
					else if (bindValue is OracleTimeStampLTZ)
					{
						oracleTimeStampLTZ = (OracleTimeStampLTZ)bindValue;
					}
					else if (bindValue is OracleTimeStampTZ)
					{
						oracleTimeStampLTZ = ((OracleTimeStampTZ)bindValue).ToOracleTimeStampLTZ();
					}
					else if (bindValue is OracleDate)
					{
						oracleTimeStampLTZ = (OracleDate)bindValue;
					}
					else
					{
						if (!(bindValue is string) && !(bindValue is char[]) && !(bindValue is OracleString) && !(bindValue is char))
						{
							throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
						}
						string preBindBuffer_Str = this.GetPreBindBuffer_Str(bindValue);
						oracleTimeStampLTZ = new OracleTimeStampLTZ(preBindBuffer_Str);
					}
					array = TimeStamp.ConvertLTZDataToDBTime(oracleTimeStampLTZ.InternalByteRepresentation, connImpl.GetDBTimeZoneBytes(), connImpl.m_sessionTimeZone);
				}
				bool flag = false;
				for (int i = 7; i < array.Length; i++)
				{
					if (array[i] != 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					timeStampTZByteArray = new byte[7];
					Array.Copy(array, timeStampTZByteArray, 7);
				}
				else
				{
					timeStampTZByteArray = array;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x000B87D4 File Offset: 0x000B69D4
		internal void SetTimeStampTZInBytes(OracleConnectionImpl connImpl, object bindValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetTimeStampTZInBytes(connImpl, bindValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000B8850 File Offset: 0x000B6A50
		internal void SetTimeStampTZInBytes(OracleConnectionImpl connImpl, object bindValue, out byte[] timeStampLTZByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if ((array = (bindValue as byte[])) != null)
				{
					if (array.Length != 13)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
				}
				else
				{
					OracleTimeStampTZ oracleTimeStampTZ;
					if (bindValue is DateTime)
					{
						oracleTimeStampTZ = new OracleTimeStampTZ((DateTime)bindValue);
					}
					else if (bindValue is OracleTimeStamp)
					{
						oracleTimeStampTZ = ((OracleTimeStamp)bindValue).ToOracleTimeStampTZ();
					}
					else if (bindValue is OracleTimeStampLTZ)
					{
						oracleTimeStampTZ = ((OracleTimeStampLTZ)bindValue).ToOracleTimeStampTZ();
					}
					else if (bindValue is OracleTimeStampTZ)
					{
						oracleTimeStampTZ = (OracleTimeStampTZ)bindValue;
					}
					else if (bindValue is OracleDate)
					{
						oracleTimeStampTZ = (OracleDate)bindValue;
					}
					else if (bindValue is string || bindValue is char[] || bindValue is OracleString || bindValue is char)
					{
						string preBindBuffer_Str = this.GetPreBindBuffer_Str(bindValue);
						oracleTimeStampTZ = new OracleTimeStampTZ(preBindBuffer_Str);
					}
					else
					{
						if (!(bindValue is DateTimeOffset))
						{
							throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
						}
						oracleTimeStampTZ = new OracleTimeStampTZ(((DateTimeOffset)bindValue).DateTime, ((DateTimeOffset)bindValue).Offset.ToString());
					}
					array = oracleTimeStampTZ.InternalByteRepresentation;
					if (connImpl.IsTZDataSentAsLocalTime)
					{
						array = TimeStamp.GetLocalTimeFromUTCByteRep(array);
					}
				}
				bool flag = false;
				for (int i = 7; i < array.Length; i++)
				{
					if (array[i] != 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					timeStampLTZByteArray = new byte[7];
					Array.Copy(array, timeStampLTZByteArray, 7);
				}
				else
				{
					timeStampLTZByteArray = array;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000B8A60 File Offset: 0x000B6C60
		internal void SetIntervalDSInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetIntervalDSInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000B8ADC File Offset: 0x000B6CDC
		internal void SetIntervalDSInBytes(object paramValue, out byte[] intervalDSByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if ((array = (paramValue as byte[])) != null)
				{
					intervalDSByteArray = array;
				}
				else
				{
					OracleIntervalDS oracleIntervalDS;
					if (paramValue is OracleIntervalDS)
					{
						oracleIntervalDS = (OracleIntervalDS)paramValue;
					}
					else if (paramValue is TimeSpan)
					{
						oracleIntervalDS = new OracleIntervalDS((TimeSpan)paramValue);
					}
					else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
					{
						oracleIntervalDS = new OracleIntervalDS(this.GetPreBindBuffer_Str(paramValue));
					}
					else
					{
						if (!(paramValue is decimal))
						{
							throw new ArgumentException();
						}
						oracleIntervalDS = new OracleIntervalDS(TimeSpan.FromSeconds((double)((decimal)paramValue)));
					}
					intervalDSByteArray = oracleIntervalDS.InternalByteRepresentation;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000B8BE8 File Offset: 0x000B6DE8
		internal void SetIntervalYMInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetIntervalYMInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000B8C64 File Offset: 0x000B6E64
		internal void SetIntervalYMInBytes(object paramValue, out byte[] intervalYMByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				byte[] array;
				if ((array = (paramValue as byte[])) != null)
				{
					intervalYMByteArray = array;
				}
				else
				{
					OracleIntervalYM oracleIntervalYM;
					if (paramValue is OracleIntervalYM)
					{
						oracleIntervalYM = (OracleIntervalYM)paramValue;
					}
					else if (paramValue is byte || paramValue is short || paramValue is int || paramValue is long)
					{
						oracleIntervalYM = new OracleIntervalYM((long)paramValue);
					}
					else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
					{
						oracleIntervalYM = new OracleIntervalYM(this.GetPreBindBuffer_Str(paramValue));
					}
					else
					{
						if (!(paramValue is decimal))
						{
							throw new ArgumentException();
						}
						oracleIntervalYM = new OracleIntervalYM((long)((decimal)paramValue));
					}
					intervalYMByteArray = oracleIntervalYM.InternalByteRepresentation;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000B8D84 File Offset: 0x000B6F84
		internal void SetRawDataInBytes(object paramValue, int size, int offSet)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetRawDataInBytes(paramValue, size, offSet, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000B8E04 File Offset: 0x000B7004
		internal void SetRawDataInBytes(object paramValue, int size, int offSet, out byte[] rawByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			rawByteArray = null;
			try
			{
				byte[] array;
				if ((array = (paramValue as byte[])) != null)
				{
					if (offSet > 0 || size != array.Length)
					{
						rawByteArray = new byte[size];
						Array.Copy(array, offSet, rawByteArray, 0, size);
					}
					else
					{
						rawByteArray = array;
					}
				}
				else if (paramValue is OracleBinary)
				{
					array = ((OracleBinary)paramValue).m_value;
					if (offSet > 0 || size != array.Length)
					{
						rawByteArray = new byte[size];
						Array.Copy(array, offSet, rawByteArray, 0, size);
					}
					else
					{
						rawByteArray = array;
					}
				}
				else if (paramValue is Guid)
				{
					array = ((Guid)paramValue).ToByteArray();
					if (offSet > 0 || size != array.Length)
					{
						rawByteArray = new byte[size];
						Array.Copy(array, offSet, rawByteArray, 0, size);
					}
					else
					{
						rawByteArray = array;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000B8F20 File Offset: 0x000B7120
		internal void SetDateInBytes(object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetDateInBytes(paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000B8F9C File Offset: 0x000B719C
		internal void SetDateInBytes(object paramValue, out byte[] dateByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			dateByteArray = null;
			try
			{
				byte[] array;
				if (paramValue is DateTime)
				{
					dateByteArray = new byte[7];
					TTCDateTimeAccessor.GetOracleDate(dateByteArray, (DateTime)paramValue);
				}
				else if (paramValue is OracleDate)
				{
					dateByteArray = ((OracleDate)paramValue).InternalByteRepresentation;
				}
				else if (paramValue is OracleTimeStamp)
				{
					dateByteArray = new byte[7];
					Array.Copy(((OracleTimeStamp)paramValue).InternalByteRepresentation, dateByteArray, 7);
				}
				else if (paramValue is OracleTimeStampLTZ)
				{
					dateByteArray = new byte[7];
					Array.Copy(((OracleTimeStampLTZ)paramValue).InternalByteRepresentation, dateByteArray, 7);
				}
				else if (paramValue is OracleTimeStampTZ)
				{
					dateByteArray = ((OracleTimeStampTZ)paramValue).ToOracleDate().InternalByteRepresentation;
				}
				else if (paramValue is string || paramValue is char[] || paramValue is OracleString || paramValue is char)
				{
					string preBindBuffer_Str = this.GetPreBindBuffer_Str(paramValue);
					dateByteArray = new OracleDate(preBindBuffer_Str).InternalByteRepresentation;
				}
				else if ((array = (paramValue as byte[])) != null)
				{
					int year = (int)((array[0] - 100) * 100 + array[1] - 100);
					int month = (int)array[2];
					int day = (int)array[3];
					int hour = (int)(array[4] - 1);
					int minute = (int)(array[5] - 1);
					int second = (int)(array[6] - 1);
					if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, 0))
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					dateByteArray = array;
				}
				else
				{
					DateTime data = Convert.ToDateTime(paramValue);
					dateByteArray = new OracleDate(data).InternalByteRepresentation;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000B91AC File Offset: 0x000B73AC
		internal void SetBlobDataInBytes(OracleConnectionImpl connImpl, object paramValue, int offSet, int maxSize, int[] maxArrayBindSize)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_saveValue = new object[1];
				this.SetBlobDataInBytes(connImpl, paramValue, offSet, maxSize, maxArrayBindSize, out this.m_paramValInBytes, out this.m_saveValue[0]);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000B9248 File Offset: 0x000B7448
		internal void SetBlobDataInBytes(OracleConnectionImpl connImpl, object paramValue, int offSet, int maxSize, int[] maxArrayBindSize, out byte[] blobDataInBytes, out object saveValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				saveValue = null;
				OracleBlob oracleBlob = paramValue as OracleBlob;
				if (oracleBlob != null)
				{
					if (oracleBlob.m_connection.m_oracleConnectionImpl != connImpl)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					if (oracleBlob.m_blobImpl.m_isTemporaryLob)
					{
						oracleBlob.CreateTempLob();
					}
					blobDataInBytes = oracleBlob.GetLobLocator();
				}
				else
				{
					byte[] preBindBuffer_Raw = this.GetPreBindBuffer_Raw(paramValue, false, 0);
					int bindingSize = this.GetBindingSize(preBindBuffer_Raw, false, 0, offSet, maxSize, maxArrayBindSize);
					OracleBlobImpl oracleBlobImpl = new OracleBlobImpl(connImpl, null, false);
					oracleBlobImpl.CreateTemporaryLob();
					saveValue = oracleBlobImpl;
					if (bindingSize > 0)
					{
						oracleBlobImpl.Write(1L, preBindBuffer_Raw, (long)offSet, (long)bindingSize);
					}
					blobDataInBytes = oracleBlobImpl.m_lobLocator;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000B9350 File Offset: 0x000B7550
		internal void SetBFileDataInBytes(OracleConnectionImpl connectionImpl, object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetBFileDataInBytes(connectionImpl, paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000B93CC File Offset: 0x000B75CC
		internal void SetBFileDataInBytes(OracleConnectionImpl connectionImpl, object paramValue, out byte[] bfileByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleBFile oracleBFile = paramValue as OracleBFile;
				if (oracleBFile == null)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
				}
				if (oracleBFile.m_connection.m_oracleConnectionImpl != connectionImpl)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				bfileByteArray = oracleBFile.GetLobLocator();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000B9490 File Offset: 0x000B7690
		internal void SetRefCursorDataInBytes(OracleConnectionImpl connImpl, object paramValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetRefCursorDataInBytes(connImpl, paramValue, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000B950C File Offset: 0x000B770C
		internal void SetRefCursorDataInBytes(OracleConnectionImpl connImpl, object paramValue, out byte[] refCursorByteArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleRefCursor oracleRefCursor = paramValue as OracleRefCursor;
				if (oracleRefCursor == null || oracleRefCursor.Connection == null)
				{
					throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
				}
				if (oracleRefCursor.Connection.m_oracleConnectionImpl != connImpl)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
				}
				refCursorByteArray = BitConverter.GetBytes(oracleRefCursor.m_refCursorImpl.m_cursorId);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000B95E0 File Offset: 0x000B77E0
		internal void SetClobDataInBytes(OracleConnectionImpl connImpl, bool bIsNClob, object paramValue, int offset, int maxSize, int[] maxArrayBindSize)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_saveValue = new object[1];
				this.SetClobDataInBytes(connImpl, bIsNClob, paramValue, offset, maxSize, maxArrayBindSize, out this.m_paramValInBytes, out this.m_saveValue[0]);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000B967C File Offset: 0x000B787C
		internal void SetClobDataInBytes(OracleConnectionImpl connImpl, bool bIsNClob, object paramValue, int offset, int maxSize, int[] maxArrayBindSize, out byte[] clobByteArray, out object saveValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				saveValue = null;
				OracleClob oracleClob = paramValue as OracleClob;
				if (oracleClob != null)
				{
					if (oracleClob.m_connection.m_oracleConnectionImpl != connImpl)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					if (oracleClob.m_clobImpl.m_isTemporaryLob)
					{
						oracleClob.CreateTempLob();
					}
					clobByteArray = oracleClob.GetLobLocator();
				}
				else
				{
					char[] preBindBuffer_Char = this.GetPreBindBuffer_Char(paramValue, false, 0);
					int bindingSize = this.GetBindingSize(preBindBuffer_Char, false, 0, offset, maxSize, maxArrayBindSize);
					OracleClobImpl oracleClobImpl = new OracleClobImpl(connImpl, null, bIsNClob, false);
					oracleClobImpl.CreateTemporaryLob();
					saveValue = oracleClobImpl;
					if (bindingSize > 0)
					{
						oracleClobImpl.Write(1L, bIsNClob, preBindBuffer_Char, (long)offset, (long)bindingSize);
					}
					clobByteArray = oracleClobImpl.m_lobLocator;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000B9788 File Offset: 0x000B7988
		internal void SetXmlTypeInBytes(OracleConnectionImpl connImpl, object paramValue, int offset, int maxSize, int[] maxArrayBindSize)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.SetXmlTypeInBytes(connImpl, paramValue, offset, maxSize, maxArrayBindSize, out this.m_paramValInBytes);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000B980C File Offset: 0x000B7A0C
		internal void SetXmlTypeInBytes(OracleConnectionImpl connImpl, object paramValue, int offset, int maxSize, int[] maxArrayBindSize, out byte[] bytes)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bytes = null;
			try
			{
				OracleXmlType oracleXmlType;
				OracleClob oracleClob;
				if ((oracleXmlType = (paramValue as OracleXmlType)) != null)
				{
					if (oracleXmlType.m_connection.m_oracleConnectionImpl != connImpl)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					bytes = TTCXmlTypePickler.Pickle(connImpl.m_marshallingEngine.m_dbCharSetConv, oracleXmlType.m_xmlTypeImpl);
				}
				else if ((oracleClob = (paramValue as OracleClob)) != null)
				{
					if (oracleClob.m_connection.m_oracleConnectionImpl != connImpl)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_DIFFERENT_CONNECTIONS, new string[0]));
					}
					bytes = TTCXmlTypePickler.Pickle(connImpl.m_marshallingEngine.m_dbCharSetConv, TypeOfXmlType.Clob, TypeOfXmlData.Clob, offset, maxSize, oracleClob);
				}
				else
				{
					char[] preBindBuffer_Char = this.GetPreBindBuffer_Char(paramValue, false, 0);
					int bindingSize = this.GetBindingSize(preBindBuffer_Char, false, 0, offset, maxSize, maxArrayBindSize);
					bytes = TTCXmlTypePickler.Pickle(connImpl.m_marshallingEngine.m_dbCharSetConv, TypeOfXmlType.String, TypeOfXmlData.Chars, offset, bindingSize, preBindBuffer_Char);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000B9948 File Offset: 0x000B7B48
		internal void SetXmlTypeArrayInBytes(OracleConnectionImpl connImpl, object paramValue, int offset, int maxSize, int[] maxArrayBindSize, int noOfElems, bool[] nullIndicatorsForArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				for (int i = 0; i < noOfElems; i++)
				{
					if (!nullIndicatorsForArrayBind[i])
					{
						object value = ((Array)paramValue).GetValue(i);
						this.SetXmlTypeInBytes(connImpl, value, offset, maxSize, maxArrayBindSize, out this.m_paramValForArrayBindInBytes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000B99F4 File Offset: 0x000B7BF4
		private char[] GetPreBindBuffer_Char(object parameterValue, bool bArraybind, int index)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			char[] result;
			try
			{
				object obj;
				if (!bArraybind)
				{
					obj = parameterValue;
				}
				else
				{
					Array array;
					if ((array = (parameterValue as Array)) == null)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					obj = array.GetValue(index);
				}
				string text;
				char[] array2;
				char[] array3;
				if ((text = (obj as string)) != null)
				{
					array2 = text.ToCharArray();
				}
				else if ((array3 = (obj as char[])) != null)
				{
					array2 = array3;
				}
				else if (obj is char)
				{
					array2 = ((char)obj).ToString().ToCharArray();
				}
				else if (obj is OracleString)
				{
					array2 = ((OracleString)obj).Value.ToCharArray();
				}
				else
				{
					array2 = Convert.ToString(obj).ToCharArray();
				}
				result = array2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000B9B10 File Offset: 0x000B7D10
		private string GetPreBindBuffer_Str(object bindValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string result;
			try
			{
				string text;
				string text2;
				char[] value;
				if ((text = (bindValue as string)) != null)
				{
					text2 = text;
				}
				else if ((value = (bindValue as char[])) != null)
				{
					text2 = new string(value);
				}
				else if (bindValue is char)
				{
					text2 = new string((char)bindValue, 1);
				}
				else if (bindValue is OracleString)
				{
					text2 = ((OracleString)bindValue).Value;
				}
				else
				{
					text2 = Convert.ToString(bindValue);
				}
				result = text2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000B9BDC File Offset: 0x000B7DDC
		private int GetBindingSize(Array buffer, bool bArrayBind, int idx, int offset, int maxSize, int[] maxArrayBindSize)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				int num;
				if (!bArrayBind)
				{
					if (maxSize == -1)
					{
						num = buffer.Length;
					}
					else
					{
						num = maxSize;
					}
				}
				else if (maxArrayBindSize[idx] == -1)
				{
					num = buffer.Length;
				}
				else
				{
					num = maxArrayBindSize[idx];
				}
				if (offset > buffer.Length)
				{
					throw new ArgumentException("Invalid offset", "Parameter Name");
				}
				if (offset + num > buffer.Length)
				{
					num = buffer.Length - offset;
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000B9CA8 File Offset: 0x000B7EA8
		private byte[] GetPreBindBuffer_Raw(object parameterValue, bool bArrayBind, int index)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			byte[] result;
			try
			{
				object obj;
				if (!bArrayBind)
				{
					obj = parameterValue;
				}
				else
				{
					Array array;
					if ((array = (parameterValue as Array)) == null)
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					obj = array.GetValue(index);
				}
				byte[] array2;
				byte[] array3;
				if ((array2 = (obj as byte[])) != null)
				{
					array3 = array2;
				}
				else
				{
					if (!(obj is OracleBinary))
					{
						throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), "ParameterName");
					}
					array3 = ((OracleBinary)obj).Value;
				}
				result = array3;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000B9D98 File Offset: 0x000B7F98
		internal object GetCharDataFromBytes(Accessor accessor, PrmEnumType enumType, byte charSetForm, int maxSize, char[] charArrayFromPooler)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCVarcharAccessor ttcvarcharAccessor = accessor as TTCVarcharAccessor;
				object obj;
				if (ttcvarcharAccessor != null && !ttcvarcharAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractCharFromAccessor(accessor, enumType, charSetForm, maxSize, 0, charArrayFromPooler, out this.m_curSize);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleString.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000B9E60 File Offset: 0x000B8060
		internal object GetRefCursorFromBytes(OracleConnection conn, Accessor accessor, long fetchSize, PrmEnumType enumType, OracleIntervalDS sessionTimeZone, string commandText, string paramPosOrName, long longFetchSize, long clientInitialLOBFS, long internalInitialLOBFS, long[] scnFromExecution, bool bCallFromExecuteReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCRefCursorAccessor ttcrefCursorAccessor = accessor as TTCRefCursorAccessor;
				object obj;
				if (ttcrefCursorAccessor != null && !ttcrefCursorAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractRefCursorFromAccessor(conn, ttcrefCursorAccessor, fetchSize, enumType, sessionTimeZone, commandText, paramPosOrName, longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, 0, bCallFromExecuteReader);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleRefCursor.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000B9F2C File Offset: 0x000B812C
		internal void GetLobDataFromBytes(OracleConnection conn, Accessor accessor, PrmEnumType enumType, OraType oraType, ref object value, bool isInputOutput, byte charSetForm)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.ExtractLobDataFromAccessor(conn, accessor, enumType, oraType, ref value, isInputOutput, charSetForm, ref this.m_curSize, ref this.m_status, 0);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000B9FB8 File Offset: 0x000B81B8
		private void ExtractLobDataFromAccessor(OracleConnection conn, Accessor accessor, PrmEnumType enumType, OraType oraType, ref object value, bool isInputOutput, byte charSetForm, ref int curSize, ref OracleParameterStatus status, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				TTCLobAccessor ttclobAccessor = accessor as TTCLobAccessor;
				byte[] array = null;
				if (ttclobAccessor != null && !ttclobAccessor.IsNullIndicatorSet(currentRow))
				{
					array = ttclobAccessor.GetLobLocator(currentRow);
				}
				if (array == null)
				{
					if (isInputOutput && value != null)
					{
						if (value is OracleBlob)
						{
							((OracleBlob)value).Dispose();
						}
						else if (value is OracleClob)
						{
							((OracleClob)value).Dispose();
						}
						else if (value is OracleBFile)
						{
							((OracleBFile)value).Dispose();
						}
						else if (this.m_saveValue != null)
						{
							if (this.m_saveValue[currentRow] is OracleBlobImpl)
							{
								((OracleBlobImpl)this.m_saveValue[currentRow]).RelRef();
							}
							else if (this.m_saveValue[currentRow] is OracleClobImpl)
							{
								((OracleClobImpl)this.m_saveValue[currentRow]).RelRef();
							}
							this.m_saveValue[currentRow] = null;
						}
					}
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						if (oraType == OraType.ORA_OCIBLobLocator)
						{
							value = OracleBlob.Null;
						}
						else if (oraType == OraType.ORA_OCICLobLocator)
						{
							value = OracleClob.Null;
						}
						else
						{
							value = OracleBFile.Null;
						}
					}
					else
					{
						value = DBNull.Value;
					}
					curSize = 0;
					status = OracleParameterStatus.NullFetched;
				}
				else
				{
					if (isInputOutput && value != null && this.m_saveValue != null)
					{
						this.m_saveValue[currentRow] = 0;
					}
					switch (oraType)
					{
					case OraType.ORA_OCICLobLocator:
					{
						OracleClob oracleClob = value as OracleClob;
						if (isInputOutput && oracleClob != null && !oracleClob.IsNull)
						{
							bool bTempLob = OracleClobImpl.IsTemporaryLob(array);
							oracleClob.SetLobLocator(array, bTempLob);
						}
						else
						{
							value = new OracleClob(conn, array, charSetForm == 2, false);
						}
						if (value is OracleClob)
						{
							((OracleClob)value).Position = 0L;
						}
						if (PrmEnumType.ORADBTYPE != enumType)
						{
							value = (value as OracleClob).Value;
						}
						break;
					}
					case OraType.ORA_OCIBLobLocator:
					{
						OracleBlob oracleBlob = value as OracleBlob;
						if (isInputOutput && oracleBlob != null && !oracleBlob.IsNull)
						{
							bool bTempLob2 = OracleBlobImpl.IsTemporaryLob(array);
							oracleBlob.SetLobLocator(array, bTempLob2);
						}
						else
						{
							value = new OracleBlob(conn, array);
						}
						if (value is OracleBlob)
						{
							((OracleBlob)value).Position = 0L;
						}
						if (PrmEnumType.ORADBTYPE != enumType)
						{
							value = (value as OracleBlob).Value;
						}
						break;
					}
					case OraType.ORA_OCIBFileLocator:
					{
						OracleBFile oracleBFile = value as OracleBFile;
						if (isInputOutput && oracleBFile != null && !oracleBFile.IsNull)
						{
							oracleBFile.SetLobLocator(array, false);
						}
						else
						{
							value = new OracleBFile(conn, array);
						}
						if (PrmEnumType.ORADBTYPE != enumType)
						{
							OracleBFile oracleBFile2 = (OracleBFile)value;
							value = oracleBFile2.GetValue();
							oracleBFile2.Dispose();
						}
						break;
					}
					}
					status = OracleParameterStatus.Success;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000BA2CC File Offset: 0x000B84CC
		internal void GetXmlTypeArrayFromBytes(OracleConnection conn, Accessor accessor, PrmEnumType enumType, OraType oraType, ref object value, bool isInputOutput, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			Array array = null;
			try
			{
				if (isInputOutput && value != null)
				{
					array = (value as Array);
				}
				Array array2 = new OracleXmlType[bindElemCnt];
				for (int i = 0; i < bindElemCnt; i++)
				{
					object obj = null;
					if (array != null && i < array.Length)
					{
						obj = array.GetValue(i);
					}
					this.ExtractXmlTypeDataFromAccessor(conn, accessor, enumType, oraType, ref obj, isInputOutput, ref this.m_curArrayBindSize[i], ref this.m_arrayBindStatus[i], i);
					if (DBNull.Value == obj && enumType != PrmEnumType.ORADBTYPE)
					{
						array2.SetValue(null, i);
					}
					else
					{
						array2.SetValue(obj, i);
					}
				}
				value = array2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000BA3C8 File Offset: 0x000B85C8
		private void ExtractXmlTypeDataFromAccessor(OracleConnection conn, Accessor accessor, PrmEnumType enumType, OraType oraType, ref object value, bool isInputOutput, ref int curSize, ref OracleParameterStatus status, int currentRow)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				TTCXmlTypeAccessor ttcxmlTypeAccessor = accessor as TTCXmlTypeAccessor;
				OraXmlTypeHeader xmlTypeHeader = new OraXmlTypeHeader();
				OraXmlTypeData oraXmlTypeData = null;
				if (ttcxmlTypeAccessor != null && !ttcxmlTypeAccessor.IsNullIndicatorSet(currentRow))
				{
					ttcxmlTypeAccessor.UnpickleXmlType(conn.m_oracleConnectionImpl, currentRow, xmlTypeHeader, out oraXmlTypeData);
				}
				if (oraXmlTypeData == null)
				{
					if (isInputOutput && value != null)
					{
						if (value is OracleXmlType)
						{
							((OracleXmlType)value).Dispose();
						}
						else if (value is OracleClob)
						{
							((OracleClob)value).Dispose();
						}
					}
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						value = OracleXmlType.Null;
					}
					else
					{
						value = DBNull.Value;
					}
					curSize = 0;
					status = OracleParameterStatus.NullFetched;
				}
				else
				{
					OracleXmlType oracleXmlType = value as OracleXmlType;
					if (isInputOutput && oracleXmlType != null && !oracleXmlType.IsNull)
					{
						oracleXmlType.Set(conn, xmlTypeHeader, oraXmlTypeData);
					}
					else
					{
						OracleXmlTypeImpl xmlTypeImpl = new OracleXmlTypeImpl(conn.m_oracleConnectionImpl, xmlTypeHeader, oraXmlTypeData);
						value = new OracleXmlType(conn, xmlTypeImpl);
					}
					if (PrmEnumType.DBTYPE == enumType)
					{
						value = ((OracleXmlType)value).Value;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000BA514 File Offset: 0x000B8714
		internal object GetTimeStampFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj;
				if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractTimeStampFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleTimeStamp.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000BA5D0 File Offset: 0x000B87D0
		internal object GetTimeStampLTZFromBytes(OracleConnectionImpl connImpl, Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj;
				if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractTimeStampLTZFromAccessor(ttcdateTimeAccessor, enumType, connImpl, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleTimeStampLTZ.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000BA690 File Offset: 0x000B8890
		internal object GetTimeStampTZFromBytes(OracleConnectionImpl connImpl, Accessor accessor, PrmEnumType enumType, bool asDateTimeOffset = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj;
				if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractTimeStampTZFromAccessor(ttcdateTimeAccessor, enumType, 0, connImpl, asDateTimeOffset);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleTimeStampTZ.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000BA750 File Offset: 0x000B8950
		internal object GetRawDataFromBytes(Accessor accessor, PrmEnumType enumType, int maxSize)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCRawAccessor ttcrawAccessor = accessor as TTCRawAccessor;
				object obj;
				if (ttcrawAccessor != null && !ttcrawAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractRawFromAccessor(ttcrawAccessor, enumType, maxSize, 0, out this.m_curSize);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleBinary.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000BA814 File Offset: 0x000B8A14
		internal object GetRawDataFromBytesInPlSqlArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				byte[][] array = null;
				OracleBinary[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array2 = new OracleBinary[num];
						obj = array2;
					}
					else
					{
						array = new byte[num][];
						obj = array;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[elementSizes[i]];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, array3.Length);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array2[i] = new OracleBinary(array3, false);
							}
							else
							{
								array[i] = array3;
							}
							this.m_curArrayBindSize[i] = array3.Length;
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array2[i] = OracleBinary.Null;
							}
							this.m_curArrayBindSize[i] = 0;
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000BA994 File Offset: 0x000B8B94
		internal object GetDecimalArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else
				{
					array = new decimal[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractDecimalFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000BAA8C File Offset: 0x000B8C8C
		internal object GetBinaryDoubleArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCBinaryDoubleAccessor ttcbinaryDoubleAccessor = accessor as TTCBinaryDoubleAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else
				{
					array = new double[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcbinaryDoubleAccessor != null && !ttcbinaryDoubleAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractBDoubleFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000BAB84 File Offset: 0x000B8D84
		internal object GetBinaryFloatArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCBinaryFloatAccessor ttcbinaryFloatAccessor = accessor as TTCBinaryFloatAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else
				{
					array = new float[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcbinaryFloatAccessor != null && !ttcbinaryFloatAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractBFloatFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000BAC7C File Offset: 0x000B8E7C
		internal object GetIntArrayFromBytes(Accessor accessor, PrmEnumType enumType, OracleDbType oraDbType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				Array array = null;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else if (oraDbType != OracleDbType.Byte)
				{
					switch (oraDbType)
					{
					case OracleDbType.Int16:
						array = new short[bindElemCnt];
						break;
					case OracleDbType.Int32:
						array = new int[bindElemCnt];
						break;
					}
				}
				else
				{
					array = new byte[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractIntFromAccessor(accessor, enumType, oraDbType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000BADA8 File Offset: 0x000B8FA8
		internal object GetPlsqlBooleanArrayFromBytes(Accessor accessor, PrmEnumType enumType, OracleDbType oraDbType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLBooleanAccessor ttcplsqlbooleanAccessor = accessor as TTCPLSQLBooleanAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleBoolean[bindElemCnt];
				}
				else
				{
					array = new bool[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcplsqlbooleanAccessor != null && !ttcplsqlbooleanAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.GetPlsqlBooleanFromBytes(accessor, enumType, oraDbType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleBoolean.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000BAEA4 File Offset: 0x000B90A4
		internal object GetCharArrayFromBytes(Accessor accessor, PrmEnumType enumType, byte charSetForm, int[] maxArrayBindSize, int bindElemCnt, char[] charArrayFromPooler)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCVarcharAccessor ttcvarcharAccessor = accessor as TTCVarcharAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleString[bindElemCnt];
				}
				else
				{
					array = new string[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcvarcharAccessor != null && !ttcvarcharAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractCharFromAccessor(accessor, enumType, charSetForm, maxArrayBindSize[i], i, charArrayFromPooler, out this.m_curArrayBindSize[i]), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleString.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000BAFB0 File Offset: 0x000B91B0
		internal object GetDateArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDate[bindElemCnt];
				}
				else
				{
					array = new DateTime[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractDateFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDate.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000BB0A4 File Offset: 0x000B92A4
		internal object GetDoubleArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else
				{
					array = new double[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractDoubleFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000BB19C File Offset: 0x000B939C
		internal object GetIntervalDSArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCIntervalTypeAccessor ttcintervalTypeAccessor = accessor as TTCIntervalTypeAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleIntervalDS[bindElemCnt];
				}
				else
				{
					array = new TimeSpan[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcintervalTypeAccessor != null && !ttcintervalTypeAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractIntervalDSFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleIntervalDS.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000BB290 File Offset: 0x000B9490
		internal object GetIntervalYMArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCIntervalTypeAccessor ttcintervalTypeAccessor = accessor as TTCIntervalTypeAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleIntervalYM[bindElemCnt];
				}
				else
				{
					array = new long[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcintervalTypeAccessor != null && !ttcintervalTypeAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractIntervalYMFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleIntervalYM.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000BB384 File Offset: 0x000B9584
		internal void GetLobArrayFromBytes(OracleConnection conn, Accessor accessor, PrmEnumType enumType, OraType oraType, ref object value, bool isInputOutput, byte charSetForm, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			Array array = null;
			try
			{
				if (isInputOutput && value != null)
				{
					array = (value as Array);
				}
				Array array2;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					if (oraType == OraType.ORA_OCIBLobLocator)
					{
						array2 = new OracleBlob[bindElemCnt];
					}
					else if (oraType == OraType.ORA_OCICLobLocator)
					{
						array2 = new OracleClob[bindElemCnt];
					}
					else
					{
						array2 = new OracleBFile[bindElemCnt];
					}
				}
				else if (oraType == OraType.ORA_OCICLobLocator)
				{
					array2 = new string[bindElemCnt];
				}
				else
				{
					array2 = new byte[bindElemCnt][];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					object obj = null;
					if (array != null && i < array.Length)
					{
						obj = array.GetValue(i);
					}
					this.ExtractLobDataFromAccessor(conn, accessor, enumType, oraType, ref obj, isInputOutput, charSetForm, ref this.m_curArrayBindSize[i], ref this.m_arrayBindStatus[i], i);
					if (DBNull.Value == obj && enumType != PrmEnumType.ORADBTYPE)
					{
						array2.SetValue(null, i);
					}
					else
					{
						array2.SetValue(obj, i);
					}
				}
				value = array2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000BB4C0 File Offset: 0x000B96C0
		internal object GetLongArrayFromBytes(object accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else
				{
					array = new long[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractLongFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000BB5B8 File Offset: 0x000B97B8
		internal object GetRawArrayFromBytes(Accessor accessor, PrmEnumType enumType, int[] maxArrayBindSize, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCRawAccessor ttcrawAccessor = accessor as TTCRawAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleBinary[bindElemCnt];
				}
				else
				{
					array = new byte[bindElemCnt][];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcrawAccessor != null && !ttcrawAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractRawFromAccessor(accessor, enumType, maxArrayBindSize[i], i, out this.m_curArrayBindSize[i]), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleBinary.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000BB6C0 File Offset: 0x000B98C0
		internal object GetRefCursorArrayFromBytes(OracleConnection conn, Accessor accessor, long fetchSize, PrmEnumType enumType, OracleIntervalDS sessionTimeZone, string commandText, string paramPosOrName, long longFetchSize, long clientInitialLOBFS, long internalInitialLOBFS, long[] scnFromExecution, int bindElemCnt, bool bCallFromExecuteReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCRefCursorAccessor ttcrefCursorAccessor = accessor as TTCRefCursorAccessor;
				Array array;
				if (bCallFromExecuteReader || PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleRefCursor[bindElemCnt];
				}
				else
				{
					array = new OracleDataReader[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcrefCursorAccessor != null && !ttcrefCursorAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractRefCursorFromAccessor(conn, accessor, fetchSize, enumType, sessionTimeZone, commandText, paramPosOrName, longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, i, bCallFromExecuteReader), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleRefCursor.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000BB7CC File Offset: 0x000B99CC
		internal object GetSingleArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleDecimal[bindElemCnt];
				}
				else
				{
					array = new float[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractSingleFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleDecimal.Null, i);
						}
						else
						{
							array.SetValue(0, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000BB8C4 File Offset: 0x000B9AC4
		internal object GetTimeStampArrayFromBytes(Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleTimeStamp[bindElemCnt];
				}
				else
				{
					array = new DateTime[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractTimeStampFromAccessor(accessor, enumType, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleTimeStamp.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000BB9B8 File Offset: 0x000B9BB8
		internal object GetTimeStampLTZArrayFromBytes(OracleConnectionImpl connImpl, Accessor accessor, PrmEnumType enumType, int bindElemCnt)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleTimeStampLTZ[bindElemCnt];
				}
				else
				{
					array = new DateTime[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractTimeStampLTZFromAccessor(accessor, enumType, connImpl, i), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleTimeStampLTZ.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000BBAB0 File Offset: 0x000B9CB0
		internal object GetTimeStampTZArrayFromBytes(OracleConnectionImpl connImpl, Accessor accessor, PrmEnumType enumType, int bindElemCnt, bool asDateTimeOffset = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				Array array;
				if (PrmEnumType.ORADBTYPE == enumType)
				{
					array = new OracleTimeStampTZ[bindElemCnt];
				}
				else if (!asDateTimeOffset)
				{
					array = new DateTime[bindElemCnt];
				}
				else
				{
					array = new DateTimeOffset[bindElemCnt];
				}
				for (int i = 0; i < bindElemCnt; i++)
				{
					if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(i))
					{
						array.SetValue(this.ExtractTimeStampTZFromAccessor(accessor, enumType, i, connImpl, asDateTimeOffset), i);
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							array.SetValue(OracleTimeStampTZ.Null, i);
						}
						else
						{
							array.SetValue(null, i);
						}
						this.m_curArrayBindSize[i] = 0;
						this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000BBBB8 File Offset: 0x000B9DB8
		internal object GetDecimalFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractDecimalFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000BBC74 File Offset: 0x000B9E74
		internal object GetIntFromBytes(Accessor accessor, PrmEnumType enumType, OracleDbType oraDbType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractIntFromAccessor(accessor, enumType, oraDbType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000BBD34 File Offset: 0x000B9F34
		internal object GetPlsqlBooleanFromBytes(Accessor accessor, PrmEnumType enumType, OracleDbType oraDbType, int currentRow = 0)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLBooleanAccessor ttcplsqlbooleanAccessor = accessor as TTCPLSQLBooleanAccessor;
				object obj = null;
				if (ttcplsqlbooleanAccessor != null)
				{
					bool? flag = (bool?)ttcplsqlbooleanAccessor.GetBooleanValue(currentRow);
					if (flag != null)
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							obj = new OracleBoolean(flag.Value);
						}
						else
						{
							obj = flag.Value;
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						if (PrmEnumType.ORADBTYPE == enumType)
						{
							obj = OracleBoolean.Null;
						}
						else
						{
							obj = DBNull.Value;
						}
						this.m_curSize = 0;
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000BBE1C File Offset: 0x000BA01C
		internal object GetLongFromBytes(object accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractLongFromAccessor(ttcnumberAccessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000BBED8 File Offset: 0x000BA0D8
		internal object GetSingleFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractSingleFromAccessor(ttcnumberAccessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000BBF94 File Offset: 0x000BA194
		internal object GetBinaryFloatFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCBinaryFloatAccessor ttcbinaryFloatAccessor = accessor as TTCBinaryFloatAccessor;
				object obj;
				if (ttcbinaryFloatAccessor != null && !ttcbinaryFloatAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractBFloatFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000BC050 File Offset: 0x000BA250
		internal object GetDoubleFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCNumberAccessor ttcnumberAccessor = accessor as TTCNumberAccessor;
				object obj;
				if (ttcnumberAccessor != null && !ttcnumberAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractDoubleFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000BC10C File Offset: 0x000BA30C
		internal object GetBinaryDoubleFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCBinaryDoubleAccessor ttcbinaryDoubleAccessor = accessor as TTCBinaryDoubleAccessor;
				object obj;
				if (ttcbinaryDoubleAccessor != null && !ttcbinaryDoubleAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractBDoubleFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDecimal.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000BC1C8 File Offset: 0x000BA3C8
		internal object GetDateFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCDateTimeAccessor ttcdateTimeAccessor = accessor as TTCDateTimeAccessor;
				object obj;
				if (ttcdateTimeAccessor != null && !ttcdateTimeAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractDateFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleDate.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000BC284 File Offset: 0x000BA484
		internal object GetIntervalDSFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCIntervalTypeAccessor ttcintervalTypeAccessor = accessor as TTCIntervalTypeAccessor;
				object obj;
				if (ttcintervalTypeAccessor != null && !ttcintervalTypeAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractIntervalDSFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleIntervalDS.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000BC340 File Offset: 0x000BA540
		internal object GetIntervalYMFromBytes(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCIntervalTypeAccessor ttcintervalTypeAccessor = accessor as TTCIntervalTypeAccessor;
				object obj;
				if (ttcintervalTypeAccessor != null && !ttcintervalTypeAccessor.IsNullIndicatorSet(0))
				{
					obj = this.ExtractIntervalYMFromAccessor(accessor, enumType, 0);
					this.m_status = OracleParameterStatus.Success;
				}
				else
				{
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						obj = OracleIntervalYM.Null;
					}
					else
					{
						obj = DBNull.Value;
					}
					this.m_curSize = 0;
					this.m_status = OracleParameterStatus.NullFetched;
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000BC3FC File Offset: 0x000BA5FC
		internal void GetXmlTypeDataFromBytes(OracleConnection conn, Accessor accessor, PrmEnumType enumType, OraType oraType, ref object value, bool isInputOutput)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.ExtractXmlTypeDataFromAccessor(conn, accessor, enumType, oraType, ref value, isInputOutput, ref this.m_curSize, ref this.m_status, 0);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000BC488 File Offset: 0x000BA688
		internal object GetCharDataFromBytesInPLSQLArray(OracleConnectionImpl connImpl, Accessor accessor, PrmEnumType enumType, int[] elemSizesToBeReturned, byte charSetForm, char[] charArrayFromPooler)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				string[] array = null;
				OracleString[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array2 = new OracleString[num];
						obj = array2;
					}
					else
					{
						array = new string[num];
						obj = array;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							if (num2 > elemSizesToBeReturned[i])
							{
								num2 = elemSizesToBeReturned[i];
							}
							string text;
							if (charSetForm != 2)
							{
								text = connImpl.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(plSqlAssociativeArray[i], 0, num2, charArrayFromPooler, true);
							}
							else
							{
								text = connImpl.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(plSqlAssociativeArray[i], 0, num2, charArrayFromPooler, true);
							}
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array2[i] = new OracleString(text);
							}
							else
							{
								array[i] = text;
							}
							this.m_curArrayBindSize[i] = num2;
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array2[i] = OracleString.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_curArrayBindSize[i] = 0;
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000BC648 File Offset: 0x000BA848
		internal object GetDoubleFromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				double[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new double[num];
						obj = array2;
					}
					byte[] array3 = null;
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							if (array3 == null || array3.Length != num2)
							{
								array3 = new byte[num2];
							}
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, num2);
							double num3 = OracleNumberCore.lnxnur(array3);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDecimal(num3);
							}
							else
							{
								array2[i] = num3;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0.0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000BC7D4 File Offset: 0x000BA9D4
		internal object GetSingleFromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				float[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new float[num];
						obj = array2;
					}
					byte[] array3 = null;
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								if (array3 == null || array3.Length != num2)
								{
									array3 = new byte[num2];
								}
								Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, num2);
								byte[] numBytes = OracleNumberCore.lnxfpr(array3, 7);
								array[i] = new OracleDecimal(numBytes, false);
							}
							else if (plSqlAssociativeArray[i].Count == 1)
							{
								ArraySegment<byte> arraySegment = plSqlAssociativeArray[i][0];
								array2[i] = HelperClass.GetFloat(arraySegment.Array, arraySegment.Offset, num2);
							}
							else
							{
								if (array3 == null || array3.Length != num2)
								{
									array3 = new byte[num2];
								}
								Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, num2);
								array2[i] = HelperClass.GetFloat(array3, 0, num2);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0f;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000BC9C4 File Offset: 0x000BABC4
		internal object GetBinaryDoubleFromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				double[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new double[num];
						obj = array2;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[8];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, elementSizes[i]);
							double doubleFromByteArray = TTCBinaryDoubleAccessor.GetDoubleFromByteArray(array3, 0);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDecimal(doubleFromByteArray);
							}
							else
							{
								array2[i] = doubleFromByteArray;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0.0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000BCB40 File Offset: 0x000BAD40
		internal object GetBinaryFloatFromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				float[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new float[num];
						obj = array2;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[4];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, elementSizes[i]);
							float floatFromByteArray = TTCBinaryFloatAccessor.GetFloatFromByteArray(array3, 0);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDecimal(floatFromByteArray);
							}
							else
							{
								array2[i] = floatFromByteArray;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0f;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000BCCB8 File Offset: 0x000BAEB8
		internal object GetInt32FromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				int[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new int[num];
						obj = array2;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[elementSizes[i]];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, array3.Length);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDecimal(array3, false);
							}
							else
							{
								array2[i] = HelperClass.GetInt(array3, 0, array3.Length);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000BCE30 File Offset: 0x000BB030
		internal object GetDecimalFromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				decimal[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new decimal[num];
						obj = array2;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[elementSizes[i]];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, array3.Length);
							if (this.m_precision != 100 || this.m_scale != 129)
							{
								OracleDecimal value = new OracleDecimal(array3, false);
								OracleDecimal @null = OracleDecimal.Null;
								if (this.m_precision != 100 && this.m_scale != 129)
								{
									OracleDecimal.ConvertToPrecScale(value, (int)this.m_precision, (int)this.m_scale);
								}
								else if (this.m_precision != 100)
								{
									OracleDecimal.SetPrecision(value, (int)this.m_precision);
								}
								else if (this.m_scale != 129)
								{
									OracleDecimal.AdjustScale(value, (int)this.m_scale, true);
								}
							}
							else if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDecimal(array3, false);
							}
							else
							{
								array2[i] = DecimalConv.GetDecimal(array3, 0, array3.Length);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0m;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000BD054 File Offset: 0x000BB254
		internal object GetLongFromBytesInPLSQLArray(object accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDecimal[] array = null;
				long[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDecimal[num];
						obj = array;
					}
					else
					{
						array2 = new long[num];
						obj = array2;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[num2];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, array3.Length);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDecimal(array3, false);
							}
							else
							{
								array2[i] = OracleNumberCore.lnxsni(array3);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0L;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000BD1CC File Offset: 0x000BB3CC
		internal object GetDateFromBytesInPLSQLArray(Accessor accessor, PrmEnumType enumType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object result;
			try
			{
				TTCPLSQLAssociativeArrayAccessor ttcplsqlassociativeArrayAccessor = accessor as TTCPLSQLAssociativeArrayAccessor;
				List<ArraySegment<byte>>[] plSqlAssociativeArray = ttcplsqlassociativeArrayAccessor.GetPlSqlAssociativeArray();
				int[] elementSizes = ttcplsqlassociativeArrayAccessor.GetElementSizes();
				object obj = null;
				OracleDate[] array = null;
				DateTime[] array2 = null;
				if (plSqlAssociativeArray != null)
				{
					int num = plSqlAssociativeArray.Length;
					if (PrmEnumType.ORADBTYPE == enumType)
					{
						array = new OracleDate[num];
						obj = array;
					}
					else
					{
						array2 = new DateTime[num];
						obj = array2;
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = elementSizes[i];
						if (plSqlAssociativeArray[i] != null && num2 > 0)
						{
							byte[] array3 = new byte[elementSizes[i]];
							Accessor.CopyDataToUserBuffer(plSqlAssociativeArray[i], 0, array3, 0, array3.Length);
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = new OracleDate(array3);
							}
							else
							{
								array2[i] = DateTimeConv.GetDateTime(array3, OracleDbType.Date, 0, 7);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (PrmEnumType.ORADBTYPE == enumType)
							{
								array[i] = OracleDate.Null;
							}
							else
							{
								array2[i] = DateTime.MinValue;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
				}
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x04001351 RID: 4945
		internal const byte InvalidPrecision = 100;

		// Token: 0x04001352 RID: 4946
		internal const byte InvalidScale = 129;

		// Token: 0x04001353 RID: 4947
		internal byte[] m_paramValInBytes;

		// Token: 0x04001354 RID: 4948
		internal byte[][] m_paramValForArrayBindInBytes;

		// Token: 0x04001355 RID: 4949
		internal byte m_precision = 100;

		// Token: 0x04001356 RID: 4950
		internal byte m_scale = 129;

		// Token: 0x04001357 RID: 4951
		internal int m_curSize;

		// Token: 0x04001358 RID: 4952
		internal int[] m_curArrayBindSize;

		// Token: 0x04001359 RID: 4953
		internal OracleParameterStatus[] m_arrayBindStatus;

		// Token: 0x0400135A RID: 4954
		internal OracleParameterStatus m_status;

		// Token: 0x0400135B RID: 4955
		internal object[] m_saveValue;
	}
}
