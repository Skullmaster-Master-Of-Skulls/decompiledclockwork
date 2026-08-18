using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x020001FF RID: 511
	internal abstract class Accessor
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x000CB178 File Offset: 0x000C9378
		internal Accessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind)
		{
			this.m_marshallingEngine = marshallingEngine;
			this.m_bForBind = bForBind;
			this.m_colMetaData = colMetaData;
			this.Initialize();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000CB19C File Offset: 0x000C939C
		internal static Accessor CreateAccessorForDefine(MarshallingEngine mEngine, ColumnDescribeInfo colMetaData, int initialLongFetchSize, long initialLOBFetchSize, bool bDefineDone, bool bLOBArrayFetchRequired, int numRowsRequested)
		{
			return Accessor.CreateAccessor(mEngine, colMetaData, false, initialLongFetchSize, initialLOBFetchSize, bDefineDone, bLOBArrayFetchRequired, numRowsRequested);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000CB1B0 File Offset: 0x000C93B0
		internal static Accessor CreateAccessorForBind(MarshallingEngine mEngine, ColumnDescribeInfo colMetaData, SqlStatementType stmtType, int initialLongFetchSize)
		{
			Accessor accessor = Accessor.CreateAccessor(mEngine, colMetaData, true, initialLongFetchSize, 0L, false, false, 0);
			accessor.m_statementType = stmtType;
			return accessor;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000CB1D4 File Offset: 0x000C93D4
		private static Accessor CreateAccessor(MarshallingEngine mEngine, ColumnDescribeInfo colMetaData, bool bForBind, int initialLongFetchSize, long initialLOBFetchSize, bool bDefineDone, bool bLOBArrayFetchRequired = false, int numRowsRequested = 0)
		{
			OraType dataType = (OraType)colMetaData.m_dataType;
			if (dataType <= OraType.ORA_RESULTSET)
			{
				if (dataType <= OraType.ORA_LONGRAW)
				{
					switch (dataType)
					{
					case OraType.ORA_CHARN:
					case OraType.ORA_VARCHAR:
						goto IL_126;
					case OraType.ORA_NUMBER:
					case OraType.ORA_FLOAT:
					case OraType.ORA_VARNUM:
						return new TTCNumberAccessor(colMetaData, mEngine, bForBind);
					case OraType.ORA_SB1:
					case OraType.ORA_NULLSTR:
					case (OraType)7:
					case (OraType)10:
						goto IL_1D0;
					case OraType.ORA_LONG:
						break;
					case OraType.ORA_ROWID:
						goto IL_188;
					case OraType.ORA_DATE:
						goto IL_138;
					default:
						switch (dataType)
						{
						case OraType.ORA_RAW:
							return new TTCRawAccessor(colMetaData, mEngine, bForBind);
						case OraType.ORA_LONGRAW:
							break;
						default:
							goto IL_1D0;
						}
						break;
					}
					return new TTCLongAccessor(colMetaData, mEngine, bForBind, initialLongFetchSize);
				}
				switch (dataType)
				{
				case OraType.ORA_CHAR:
					goto IL_126;
				case OraType.ORA_CHARZ:
				case (OraType)98:
				case (OraType)99:
					goto IL_1D0;
				case OraType.ORA_IBFLOAT:
					return new TTCBinaryFloatAccessor(colMetaData, mEngine, bForBind);
				case OraType.ORA_IBDOUBLE:
					return new TTCBinaryDoubleAccessor(colMetaData, mEngine, bForBind);
				case OraType.ORA_REFCURSOR:
					break;
				default:
					switch (dataType)
					{
					case OraType.ORA_XMLTYPE:
						if (colMetaData.bIsXmlType)
						{
							return new TTCXmlTypeAccessor(colMetaData, mEngine, bForBind);
						}
						throw new OracleException(ResourceStringConstants.CMD_TYPE_NOT_SUPPORTED, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CMD_TYPE_NOT_SUPPORTED, new string[0]));
					case OraType.ORA_OCIRef:
					case (OraType)111:
					case (OraType)115:
						goto IL_1D0;
					case OraType.ORA_OCICLobLocator:
					case OraType.ORA_OCIBLobLocator:
					case OraType.ORA_OCIBFileLocator:
						return new TTCLobAccessor(colMetaData, mEngine, bForBind, initialLOBFetchSize, bDefineDone, bLOBArrayFetchRequired, numRowsRequested);
					case OraType.ORA_RESULTSET:
						break;
					default:
						goto IL_1D0;
					}
					break;
				}
				return new TTCRefCursorAccessor(colMetaData, mEngine);
				IL_126:
				return new TTCVarcharAccessor(colMetaData, mEngine, bForBind);
			}
			if (dataType <= OraType.ORA_UROWID)
			{
				switch (dataType)
				{
				case OraType.ORA_TIMESTAMP_DTY:
				case OraType.ORA_TIMESTAMP_TZ_DTY:
				case OraType.ORA_TIMESTAMP:
				case OraType.ORA_TIMESTAMP_TZ:
					break;
				case OraType.ORA_INTERVAL_YM_DTY:
				case OraType.ORA_INTERVAL_DS_DTY:
				case OraType.ORA_INTERVAL_YM:
				case OraType.ORA_INTERVAL_DS:
					return new TTCIntervalTypeAccessor(colMetaData, mEngine, bForBind);
				case (OraType)184:
				case (OraType)185:
				case OraType.ORA_TIME_TZ:
					goto IL_1D0;
				default:
					if (dataType != OraType.ORA_UROWID)
					{
						goto IL_1D0;
					}
					goto IL_188;
				}
			}
			else
			{
				switch (dataType)
				{
				case OraType.ORA_TIMESTAMP_LTZ_DTY:
				case OraType.ORA_TIMESTAMP_LTZ:
					break;
				default:
					if (dataType != OraType.ORA_BOOLEAN)
					{
						goto IL_1D0;
					}
					return new TTCPLSQLBooleanAccessor(colMetaData, mEngine, bForBind);
				}
			}
			IL_138:
			return new TTCDateTimeAccessor(colMetaData, mEngine, bForBind);
			IL_188:
			return new TTCRowIdAccessor(colMetaData, mEngine, bForBind);
			IL_1D0:
			throw new OracleException(ResourceStringConstants.CMD_TYPE_NOT_SUPPORTED, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CMD_TYPE_NOT_SUPPORTED, new string[0]));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000CB3D8 File Offset: 0x000C95D8
		internal virtual void InitForDataAccess(int max_len)
		{
			if (max_len > 0 && max_len < this.m_internalTypeMaxLength)
			{
				this.m_internalTypeMaxLength = max_len;
			}
			this.m_byteLength = this.m_internalTypeMaxLength;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000CB3FC File Offset: 0x000C95FC
		internal virtual void Unimplemented(string methodName)
		{
			throw new Exception(methodName + " not implemented for " + base.GetType());
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000CB414 File Offset: 0x000C9614
		internal virtual string GetString(int currentRow, char[] charArrayFromPooler)
		{
			this.Unimplemented("GetString");
			return null;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000CB424 File Offset: 0x000C9624
		internal int GetCharsFromBuffer(byte[] rawData, int rawDataLen, long fieldOffset, char[] buffer, int bufferOffset, int noOfCharsReqd, byte charSetForm)
		{
			int result = 0;
			if (rawData.Length > 0)
			{
				int byteOffset = (int)fieldOffset;
				result = noOfCharsReqd;
				if (charSetForm != 2)
				{
					if (this.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
					{
						byteOffset = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(rawData, (int)fieldOffset, rawDataLen, noOfCharsReqd);
					}
					this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToChars(rawData, byteOffset, rawDataLen, buffer, bufferOffset, ref result, true);
				}
				else
				{
					if (this.m_marshallingEngine.m_nCharSetConv.MaxBytesPerChar > 1 && fieldOffset > 0L)
					{
						byteOffset = this.m_marshallingEngine.m_dbCharSetConv.GetBytesOffset(rawData, (int)fieldOffset, rawDataLen, noOfCharsReqd);
					}
					this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToChars(rawData, byteOffset, rawDataLen, buffer, bufferOffset, ref result, true);
				}
			}
			return result;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000CB4E0 File Offset: 0x000C96E0
		internal string GetString(byte[] rawBytesToConvert, int byteOffset, int length, byte charSetForm, char[] charArrayForConversion)
		{
			string result;
			if (charSetForm != 2)
			{
				result = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(rawBytesToConvert, byteOffset, length, charArrayForConversion, true);
			}
			else
			{
				result = this.m_marshallingEngine.m_nCharSetConv.ConvertBytesToString(rawBytesToConvert, byteOffset, length, charArrayForConversion, true);
			}
			return result;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000CB524 File Offset: 0x000C9724
		internal virtual string GetString(int currentRow, byte charSetForm, char[] charArrayFromPooler)
		{
			this.Unimplemented("GetString");
			return null;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000CB534 File Offset: 0x000C9734
		internal virtual string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			this.Unimplemented("GetString");
			return null;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000CB544 File Offset: 0x000C9744
		internal virtual string GetString(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, byte charSetForm)
		{
			this.Unimplemented("GetString");
			return null;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000CB554 File Offset: 0x000C9754
		internal virtual int GetInt(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			this.Unimplemented("GetInt");
			return -1;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000CB564 File Offset: 0x000C9764
		internal virtual int GetInt(int currentRow)
		{
			this.Unimplemented("GetInt");
			return -1;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000CB574 File Offset: 0x000C9774
		internal virtual double GetDouble(int currentRow)
		{
			this.Unimplemented("GetDouble");
			return -1.0;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000CB58C File Offset: 0x000C978C
		internal virtual double GetDouble(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			byteRep = null;
			this.Unimplemented("GetDouble");
			return -1.0;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000CB5A8 File Offset: 0x000C97A8
		internal virtual double GetDouble(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			this.Unimplemented("GetDouble");
			return -1.0;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000CB5C0 File Offset: 0x000C97C0
		internal virtual float GetFloat(int currentRow)
		{
			this.Unimplemented("GetFloat");
			return -1f;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000CB5D4 File Offset: 0x000C97D4
		internal virtual float GetFloat(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			this.Unimplemented("GetDouble");
			return -1f;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x000CB5E8 File Offset: 0x000C97E8
		internal virtual float GetFloat(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			byteRep = null;
			this.Unimplemented("GetDouble");
			return -1f;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000CB600 File Offset: 0x000C9800
		internal virtual decimal GetDecimal(int currentRow)
		{
			this.Unimplemented("GetDecimal");
			return -1m;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000CB614 File Offset: 0x000C9814
		internal virtual DateTime GetDateTime(int currentRow)
		{
			this.Unimplemented("GetDate");
			return default(DateTime);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000CB638 File Offset: 0x000C9838
		internal virtual DateTime GetDateTime(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			this.Unimplemented("GetDate");
			return default(DateTime);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000CB65C File Offset: 0x000C985C
		internal virtual DateTime GetDateTime(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, out byte[] byteRep)
		{
			byteRep = null;
			this.Unimplemented("GetDate");
			return default(DateTime);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000CB684 File Offset: 0x000C9884
		internal virtual void GetInternalDataRef(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex, ref byte[] dataByteRep, ref int dataOffset, ref int dataLength)
		{
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref dataLength);
				if (dataLength > 0)
				{
					if (this.m_colDataSegments.Count > 1)
					{
						dataByteRep = new byte[dataLength];
						Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, dataByteRep, 0, dataLength);
					}
					else
					{
						dataByteRep = this.m_colDataSegments[0].Array;
						dataOffset = this.m_colDataSegments[0].Offset;
					}
				}
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000CB744 File Offset: 0x000C9944
		internal virtual byte[] GetByteRepresentation(DataUnmarshaller dataUnmarshaller, int currentRow, int columnIndex)
		{
			byte[] array = null;
			int num = 0;
			try
			{
				dataUnmarshaller.StartAccumulatingColumnData(currentRow, columnIndex, this.m_colDataSegments);
				dataUnmarshaller.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, ref num);
				if (num > 0)
				{
					array = new byte[num];
					Accessor.CopyDataToUserBuffer(this.m_colDataSegments, 0, array, 0, num);
				}
			}
			finally
			{
				this.m_colDataSegments.Clear();
				dataUnmarshaller.m_bAccumulateByteSegments = false;
				dataUnmarshaller.m_dataSegments = null;
			}
			return array;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000CB7C0 File Offset: 0x000C99C0
		internal virtual byte[] GetByteRepresentation(int currentRow)
		{
			byte[] array = null;
			int num = this.m_totalLengthOfData[currentRow];
			if (num > 0)
			{
				List<ArraySegment<byte>> list = this.m_RowDataSegments[currentRow];
				if (list != null)
				{
					array = new byte[num];
					Accessor.CopyDataToUserBuffer(list, 0, array, 0, num);
				}
			}
			return array;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000CB804 File Offset: 0x000C9A04
		internal virtual bool IsNullIndicatorSet(int currentRow)
		{
			bool result = this.m_bNullByDescribe;
			if (this.m_totalLengthOfData.Count > currentRow)
			{
				result = (this.m_totalLengthOfData[currentRow] == 0);
			}
			return result;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000CB838 File Offset: 0x000C9A38
		internal virtual bool IsNullIndicatorSet(DataUnmarshaller dataUnmarshaller, int columnCount, int currentRow, int columnIndex)
		{
			bool result = this.m_bNullByDescribe;
			if (!this.m_bNullByDescribe)
			{
				result = (-1 == dataUnmarshaller.m_colDataStartOffset[currentRow * columnCount + columnIndex]);
			}
			return result;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000CB868 File Offset: 0x000C9A68
		internal virtual void UnmarshalColumnData()
		{
			if (!this.m_bNullByDescribe)
			{
				try
				{
					this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = true;
					this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = true;
					OraType definedColumnType = this.m_definedColumnType;
					if (definedColumnType <= OraType.ORA_CHAR)
					{
						if (definedColumnType <= OraType.ORA_DATE)
						{
							switch (definedColumnType)
							{
							case OraType.ORA_CHARN:
							case OraType.ORA_VARCHAR:
								break;
							case OraType.ORA_NUMBER:
							case OraType.ORA_FLOAT:
							case OraType.ORA_VARNUM:
								this.m_marshallingEngine.UnmarshalCLR_ColData(21);
								goto IL_15B;
							case OraType.ORA_SB1:
							case OraType.ORA_NULLSTR:
							case (OraType)7:
							case OraType.ORA_LONG:
								goto IL_150;
							default:
								if (definedColumnType != OraType.ORA_DATE)
								{
									goto IL_150;
								}
								this.m_marshallingEngine.UnmarshalCLR_ColData(7);
								goto IL_15B;
							}
						}
						else if (definedColumnType != OraType.ORA_RAW && definedColumnType != OraType.ORA_CHAR)
						{
							goto IL_150;
						}
					}
					else
					{
						if (definedColumnType > OraType.ORA_RESULTSET)
						{
							switch (definedColumnType)
							{
							case OraType.ORA_TIMESTAMP_DTY:
							case OraType.ORA_TIMESTAMP:
								break;
							case OraType.ORA_TIMESTAMP_TZ_DTY:
							case OraType.ORA_TIMESTAMP_TZ:
								this.m_marshallingEngine.UnmarshalCLR_ColData(13);
								goto IL_15B;
							case OraType.ORA_INTERVAL_YM_DTY:
							case OraType.ORA_INTERVAL_DS_DTY:
							case OraType.ORA_INTERVAL_YM:
							case OraType.ORA_INTERVAL_DS:
								goto IL_F7;
							case (OraType)184:
							case (OraType)185:
							case OraType.ORA_TIME_TZ:
								goto IL_150;
							default:
								switch (definedColumnType)
								{
								case OraType.ORA_TIMESTAMP_LTZ_DTY:
								case OraType.ORA_TIMESTAMP_LTZ:
									break;
								default:
									goto IL_150;
								}
								break;
							}
							this.m_marshallingEngine.UnmarshalCLR_ColData(11);
							goto IL_15B;
						}
						switch (definedColumnType)
						{
						case OraType.ORA_IBFLOAT:
						case OraType.ORA_IBDOUBLE:
							break;
						default:
							if (definedColumnType != OraType.ORA_RESULTSET)
							{
								goto IL_150;
							}
							throw new InvalidOperationException();
						}
					}
					IL_F7:
					this.m_marshallingEngine.UnmarshalCLR_ColData(this.m_colMetaData.m_maxLength);
					goto IL_15B;
					IL_150:
					throw new Exception("UnmarshalColumnData: Unimplemented type");
					IL_15B:
					goto IL_1A2;
				}
				finally
				{
					this.m_marshallingEngine.m_oraBufRdr.m_bMarkStartOffsetForColData = false;
					this.m_marshallingEngine.m_oraBufRdr.m_bParsingColumnData = false;
				}
			}
			this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset[this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffsetIndexToUpdate] = -1;
			IL_1A2:
			this.m_lastRowProcessed++;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000CBA44 File Offset: 0x000C9C44
		internal bool NoRowsAffected
		{
			get
			{
				return this.m_lastRowProcessed <= 0;
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000CBA54 File Offset: 0x000C9C54
		internal void AddNullForData()
		{
			if (this.m_RowDataSegments != null && this.m_RowDataSegments.Count > this.m_lastRowProcessed)
			{
				this.m_RowDataSegments[this.m_lastRowProcessed] = null;
				this.m_totalLengthOfData[this.m_lastRowProcessed] = 0;
			}
			else
			{
				this.m_RowDataSegments.Add(null);
				this.m_totalLengthOfData.Add(0);
			}
			this.m_lastRowProcessed++;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000CBAC8 File Offset: 0x000C9CC8
		internal virtual bool UnmarshalOneRow()
		{
			List<ArraySegment<byte>> list = null;
			int num = 0;
			bool flag = true;
			if (!this.m_bNullByDescribe)
			{
				try
				{
					flag = this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingColumnData(this.m_RowDataSegments, this.m_lastRowProcessed);
					OraType definedColumnType = this.m_definedColumnType;
					if (definedColumnType <= OraType.ORA_CHAR)
					{
						if (definedColumnType <= OraType.ORA_DATE)
						{
							switch (definedColumnType)
							{
							case OraType.ORA_CHARN:
							case OraType.ORA_VARCHAR:
								break;
							case OraType.ORA_NUMBER:
							case OraType.ORA_FLOAT:
							case OraType.ORA_VARNUM:
								this.m_marshallingEngine.UnmarshalCLR_ScanOnly(21, out list, ref num);
								goto IL_1C8;
							case OraType.ORA_SB1:
							case OraType.ORA_NULLSTR:
							case (OraType)7:
							case OraType.ORA_LONG:
								goto IL_1BD;
							default:
								if (definedColumnType != OraType.ORA_DATE)
								{
									goto IL_1BD;
								}
								this.m_marshallingEngine.UnmarshalCLR_ScanOnly(7, out list, ref num);
								goto IL_1C8;
							}
						}
						else if (definedColumnType != OraType.ORA_RAW && definedColumnType != OraType.ORA_CHAR)
						{
							goto IL_1BD;
						}
					}
					else
					{
						if (definedColumnType > OraType.ORA_RESULTSET)
						{
							switch (definedColumnType)
							{
							case OraType.ORA_TIMESTAMP_DTY:
							case OraType.ORA_TIMESTAMP:
								break;
							case OraType.ORA_TIMESTAMP_TZ_DTY:
							case OraType.ORA_TIMESTAMP_TZ:
								this.m_marshallingEngine.UnmarshalCLR_ScanOnly(13, out list, ref num);
								goto IL_1C8;
							case OraType.ORA_INTERVAL_YM_DTY:
							case OraType.ORA_INTERVAL_DS_DTY:
							case OraType.ORA_INTERVAL_YM:
							case OraType.ORA_INTERVAL_DS:
								goto IL_129;
							case (OraType)184:
							case (OraType)185:
							case OraType.ORA_TIME_TZ:
								goto IL_1BD;
							default:
								switch (definedColumnType)
								{
								case OraType.ORA_TIMESTAMP_LTZ_DTY:
								case OraType.ORA_TIMESTAMP_LTZ:
									break;
								default:
									if (definedColumnType == OraType.ORA_BOOLEAN)
									{
										goto IL_129;
									}
									goto IL_1BD;
								}
								break;
							}
							this.m_marshallingEngine.UnmarshalCLR_ScanOnly(11, out list, ref num);
							goto IL_1C8;
						}
						switch (definedColumnType)
						{
						case OraType.ORA_IBFLOAT:
						case OraType.ORA_IBDOUBLE:
							break;
						default:
							switch (definedColumnType)
							{
							case OraType.ORA_OCICLobLocator:
							case OraType.ORA_OCIBLobLocator:
							case OraType.ORA_OCIBFileLocator:
							{
								int num2 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
								if (num2 > 0)
								{
									this.m_marshallingEngine.UnmarshalCLR_ScanOnly(num2, out list, ref num);
									goto IL_1C8;
								}
								goto IL_1C8;
							}
							case (OraType)115:
								goto IL_1BD;
							case OraType.ORA_RESULTSET:
								throw new InvalidOperationException();
							default:
								goto IL_1BD;
							}
							break;
						}
					}
					IL_129:
					this.m_marshallingEngine.UnmarshalCLR_ScanOnly(this.m_colMetaData.m_maxLength, out list, ref num);
					goto IL_1C8;
					IL_1BD:
					throw new Exception("UnmarshalOneRow: Unimplemented type");
					IL_1C8:
					goto IL_257;
				}
				finally
				{
					this.m_marshallingEngine.m_oraBufRdr.StopAccumulatingColumnData();
					if (this.m_bForBind && -1 == this.m_marshallingEngine.ProcessIndicator(num <= 0, num))
					{
						num = 0;
					}
					if (flag)
					{
						this.m_RowDataSegments.Add(list);
						this.m_totalLengthOfData.Add(num);
					}
					else
					{
						this.m_RowDataSegments[this.m_lastRowProcessed] = list;
						this.m_totalLengthOfData[this.m_lastRowProcessed] = num;
					}
				}
			}
			this.m_RowDataSegments.Add(list);
			this.m_totalLengthOfData.Add(num);
			IL_257:
			this.m_lastRowProcessed++;
			return false;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000CBD58 File Offset: 0x000C9F58
		internal void CopyRow_ScanOnly()
		{
			if (this.m_bNullByDescribe)
			{
				return;
			}
			List<ArraySegment<byte>> list;
			int num;
			if (this.m_lastRowProcessed == 0)
			{
				list = this.m_LastRowDataSegments;
				num = this.m_LastRowtotalLengthOfData;
			}
			else
			{
				list = this.m_RowDataSegments[this.m_lastRowProcessed - 1];
				num = this.m_totalLengthOfData[this.m_lastRowProcessed - 1];
			}
			List<ArraySegment<byte>> list2 = new List<ArraySegment<byte>>();
			if (list != null && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					list2.Add(list[i]);
				}
			}
			if (this.m_RowDataSegments.Count > this.m_lastRowProcessed)
			{
				this.m_RowDataSegments[this.m_lastRowProcessed] = list2;
				this.m_totalLengthOfData[this.m_lastRowProcessed] = num;
			}
			else
			{
				this.m_RowDataSegments.Add(list2);
				this.m_totalLengthOfData.Add(num);
			}
			this.m_lastRowProcessed++;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000CBE40 File Offset: 0x000CA040
		internal virtual object GetValue()
		{
			return null;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000CBE44 File Offset: 0x000CA044
		internal static int CopyDataToUserBuffer(List<ArraySegment<byte>> dataSegments, int dataOffset, byte[] userBuffer, int userBuffOffset, int dataLength)
		{
			int num = 0;
			int num2 = 0;
			bool flag = dataOffset > 0;
			int num3 = (dataLength < userBuffer.Length - userBuffOffset) ? dataLength : (userBuffer.Length - userBuffOffset);
			int num4 = userBuffOffset;
			int i = 0;
			while (i < dataSegments.Count)
			{
				ArraySegment<byte> arraySegment = dataSegments[i];
				int count = arraySegment.Count;
				if (!flag)
				{
					goto IL_59;
				}
				if (count > dataOffset)
				{
					num2 = dataOffset;
					dataOffset = 0;
					flag = false;
					goto IL_59;
				}
				dataOffset -= count;
				IL_B2:
				i++;
				continue;
				IL_59:
				int num5 = count - num2;
				if (num5 >= num3)
				{
					Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset + num2, userBuffer, num4, num3);
					num += num3;
					break;
				}
				Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset + num2, userBuffer, num4, num5);
				num4 += num5;
				num3 -= num5;
				num += num5;
				num2 = 0;
				goto IL_B2;
			}
			return num;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000CBF18 File Offset: 0x000CA118
		internal static byte GetValueAt(List<ArraySegment<byte>> dataSegments, int dataOffset)
		{
			byte result = 0;
			int num = 0;
			bool flag = dataOffset > 0;
			int i = 0;
			while (i < dataSegments.Count)
			{
				ArraySegment<byte> arraySegment = dataSegments[i];
				int count = arraySegment.Count;
				if (flag)
				{
					if (count <= dataOffset)
					{
						dataOffset -= count;
						i++;
						continue;
					}
					num = dataOffset;
					dataOffset = 0;
				}
				result = arraySegment.Array[arraySegment.Offset + num];
				break;
			}
			return result;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000CBF88 File Offset: 0x000CA188
		internal virtual void Initialize(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind)
		{
			this.m_marshallingEngine = marshallingEngine;
			this.m_bForBind = bForBind;
			this.m_colMetaData = colMetaData;
			this.Initialize();
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000CBFA8 File Offset: 0x000CA1A8
		internal void Initialize()
		{
			if (this.m_bForBind)
			{
				if (this.m_totalLengthOfData != null)
				{
					this.m_totalLengthOfData.Clear();
				}
				else
				{
					this.m_totalLengthOfData = new List<int>();
				}
				if (this.m_RowDataSegments != null)
				{
					this.m_RowDataSegments.Clear();
				}
				else
				{
					this.m_RowDataSegments = new List<List<ArraySegment<byte>>>();
				}
			}
			else if (this.m_colDataSegments != null)
			{
				this.m_colDataSegments.Clear();
			}
			else
			{
				this.m_colDataSegments = new List<ArraySegment<byte>>();
			}
			this.m_lastRowProcessed = 0;
			this.m_LastRowDataSegments = null;
			this.m_LastRowtotalLengthOfData = 0;
			this.m_bReceivedOutValueFromServer = false;
			if (this.m_colMetaData != null)
			{
				this.m_internalType = (this.m_definedColumnType = (OraType)this.m_colMetaData.m_dataType);
				this.m_internalTypeMaxLength = this.m_colMetaData.m_maxLength;
				this.m_bNullByDescribe = (this.m_colMetaData.m_maxLength <= 0);
			}
		}

		// Token: 0x0400144A RID: 5194
		protected bool m_bForBind;

		// Token: 0x0400144B RID: 5195
		internal bool m_bForReturningParameter;

		// Token: 0x0400144C RID: 5196
		internal SqlStatementType m_statementType;

		// Token: 0x0400144D RID: 5197
		internal OraType m_internalType;

		// Token: 0x0400144E RID: 5198
		protected int m_internalTypeMaxLength;

		// Token: 0x0400144F RID: 5199
		internal OraType m_definedColumnType;

		// Token: 0x04001450 RID: 5200
		internal ColumnDescribeInfo m_colMetaData;

		// Token: 0x04001451 RID: 5201
		internal MarshallingEngine m_marshallingEngine;

		// Token: 0x04001452 RID: 5202
		protected int m_byteLength;

		// Token: 0x04001453 RID: 5203
		internal int m_lastRowProcessed;

		// Token: 0x04001454 RID: 5204
		internal List<ArraySegment<byte>> m_colDataSegments;

		// Token: 0x04001455 RID: 5205
		internal List<List<ArraySegment<byte>>> m_RowDataSegments;

		// Token: 0x04001456 RID: 5206
		internal List<int> m_totalLengthOfData;

		// Token: 0x04001457 RID: 5207
		internal List<ArraySegment<byte>> m_LastRowDataSegments;

		// Token: 0x04001458 RID: 5208
		internal int m_LastRowtotalLengthOfData;

		// Token: 0x04001459 RID: 5209
		internal bool m_bNullByDescribe;

		// Token: 0x0400145A RID: 5210
		internal bool m_bReceivedOutValueFromServer;
	}
}
