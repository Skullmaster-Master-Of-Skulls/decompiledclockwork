using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B1 RID: 433
	internal class OracleDataReaderImpl
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x000B0878 File Offset: 0x000AEA78
		internal void Close()
		{
			if (this.m_closed)
			{
				return;
			}
			lock (this.m_lock)
			{
				if (!this.m_closed)
				{
					this.m_closed = true;
					try
					{
						string commandText = null;
						if (this.m_sqlMetaData != null)
						{
							commandText = this.m_sqlMetaData.pCommandText;
						}
						this.ReleaseCursor(commandText);
						if (this.OnClose != null)
						{
							this.OnClose();
							this.OnClose = null;
						}
						if (this.m_refCursors != null)
						{
							for (int i = this.m_currentRefCursorIndex; i < this.m_refCursors.Count; i++)
							{
								OracleRefCursor oracleRefCursor = this.m_refCursors[i];
								if (oracleRefCursor != null)
								{
									oracleRefCursor.Dispose();
								}
							}
						}
					}
					finally
					{
						if (this.m_connectionImpl != null)
						{
							if (this.m_bPooled)
							{
								this.m_connectionImpl.m_preferredReaderImplTaken = false;
							}
							else
							{
								this.m_connectionImpl.DeregisterForConnectionClose(this);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000B097C File Offset: 0x000AEB7C
		internal OracleDataReaderImpl(OracleConnectionImpl connectionImpl)
		{
			this.m_connectionImpl = connectionImpl;
			if (this.m_connectionImpl != null)
			{
				this.m_connectionImpl.RegisterForConnectionClose(this);
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000B09B8 File Offset: 0x000AEBB8
		internal void Init(List<OracleRefCursor> refCursors, long longFetchSize, long[] snapshotSCN)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_closed = false;
				this.OnClose = null;
				this.m_oraBufReleaseInfoList = null;
				this.m_accessors = null;
				this.m_cursorId = 0;
				this.m_sqlMetaData = null;
				this.m_cachedStmt = null;
				this.m_sessionTimeZone = OracleIntervalDS.Zero;
				this.m_clientInitialLOBFS = 0L;
				this.m_internalInitialLOBFS = 0L;
				this.m_bInitialLongFetchSizeModified = false;
				this.m_numberOfHiddenColumns = 0;
				this.m_refCursors = refCursors;
				this.m_bForRefCursor = true;
				this.m_snapshotSCN = snapshotSCN;
				this.m_initialLongFS = longFetchSize;
				this.m_currentRefCursorIndex = 0;
				this.m_bFetchForRefCursorFirstTime = true;
				this.m_rowsFetchedLastTime = -1;
				this.m_rowsFetched = 0;
				OracleRefCursor oracleRefCursor = this.m_refCursors[this.m_currentRefCursorIndex];
				if (oracleRefCursor != null && !oracleRefCursor.IsNull)
				{
					this.m_bHasMoreRowsInDB = true;
					this.m_cursorId = oracleRefCursor.m_refCursorImpl.m_cursorId;
					this.m_sqlMetaData = oracleRefCursor.m_refCursorImpl.m_sqlMetaData;
					this.m_accessors = oracleRefCursor.m_refCursorImpl.m_accessors;
					this.m_sessionTimeZone = oracleRefCursor.m_sessionTimeZone;
				}
				else
				{
					this.m_bHasMoreRowsInDB = false;
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

		// Token: 0x06001065 RID: 4197 RVA: 0x000B0B3C File Offset: 0x000AED3C
		internal void Init(Accessor[] defineAccessors, SQLMetaData sqlMetaData, int cursorId, int noOfRowsFetched, CachedStatement cachedStmt, OracleIntervalDS sessionTimeZone, long initialLongFS, long clientInitialLOBFS, long internalInitialLOBFS, long[] snapshotSCN, bool metadataHasImplicitROWIDcolumn = false, bool bInitialLongFetchSizeModified = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_closed = false;
				this.OnClose = null;
				this.m_oraBufReleaseInfoList = null;
				this.m_bHasMoreRowsInDB = false;
				this.m_refCursors = null;
				this.m_bForRefCursor = false;
				this.m_currentRefCursorIndex = 0;
				this.m_bFetchForRefCursorFirstTime = true;
				this.m_accessors = defineAccessors;
				this.m_cursorId = cursorId;
				this.m_sqlMetaData = sqlMetaData;
				this.m_cachedStmt = cachedStmt;
				this.m_sessionTimeZone = sessionTimeZone;
				this.m_initialLongFS = initialLongFS;
				this.m_bInitialLongFetchSizeModified = bInitialLongFetchSizeModified;
				this.m_clientInitialLOBFS = clientInitialLOBFS;
				this.m_internalInitialLOBFS = internalInitialLOBFS;
				this.m_snapshotSCN = snapshotSCN;
				this.m_rowsFetched = noOfRowsFetched;
				this.m_rowsFetchedLastTime = noOfRowsFetched;
				this.m_numberOfHiddenColumns = OracleDataReaderImpl.CountHiddenColumns(metadataHasImplicitROWIDcolumn);
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

		// Token: 0x06001066 RID: 4198 RVA: 0x000B0C50 File Offset: 0x000AEE50
		internal static int CountHiddenColumns(bool metadataHasImplicitROWIDcolumn)
		{
			if (!metadataHasImplicitROWIDcolumn)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000B0C58 File Offset: 0x000AEE58
		internal void ReleaseCursor(string commandText)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_cursorId != 0)
				{
					if (this.m_connectionImpl.m_statementCache != null && this.m_cachedStmt != null)
					{
						if (this.m_bHasMoreRowsInDB)
						{
							this.m_connectionImpl.AddCursorIdToBeCancelled((long)this.m_cachedStmt.m_cursorId);
						}
						if (this.m_dataUnmarshaller != null)
						{
							DataUnmarshaller.ReleaseAllOBs(this.m_dataUnmarshaller.m_oraArrSegWithColRowInfo, this.m_dataUnmarshaller.m_oraArrSegCount, this.m_connectionImpl.m_oracleCommunication);
							if (this.m_dataUnmarshaller.m_charArrayForConversion != null)
							{
								this.m_connectionImpl.m_marshallingEngine.m_charArrayPooler.Enqueue(this.m_dataUnmarshaller.m_charArrayForConversion);
								this.m_dataUnmarshaller.m_charArrayForConversion = null;
							}
							this.m_dataUnmarshaller.m_charArrayForBigDataConversion = null;
							this.m_cachedStmt.m_dataUnmarshaller = this.m_dataUnmarshaller;
							this.m_dataUnmarshaller = null;
						}
						if (!string.IsNullOrWhiteSpace(commandText))
						{
							CachedStatement cachedStatement = this.m_connectionImpl.m_statementCache.Put(commandText, this.m_cachedStmt);
							if (cachedStatement != null)
							{
								this.m_connectionImpl.AddCursorIdToBeClosed((long)cachedStatement.m_cursorId);
							}
						}
					}
					else
					{
						this.m_connectionImpl.AddCursorIdToBeClosed((long)this.m_cursorId);
					}
					this.m_cursorId = 0;
					if (this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr != null)
					{
						this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.FreeTempOBList();
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

		// Token: 0x06001068 RID: 4200 RVA: 0x000B0E28 File Offset: 0x000AF028
		internal int FetchMoreRows(int noOfRowsToFetch, bool fillReader, bool returnPSTypes)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool flag = false;
			bool flag2 = false;
			bool bLOBArrayFetchRequired = false;
			ColumnDescribeInfo[] columnDefines = null;
			int rowsFetched;
			try
			{
				bool flag3 = false;
				if (this.m_sqlMetaData != null && this.m_sqlMetaData.HasLOBOrLongColumn)
				{
					if (this.m_connectionImpl.m_marshallingEngine.DBVersion >= 11100 && this.m_sqlMetaData.m_numOfLOBColumns > 0)
					{
						this.m_internalInitialLOBFS = TTCExecuteSql.CalculateInternalILFS(this.m_clientInitialLOBFS, fillReader, returnPSTypes);
						if ((this.m_sqlMetaData.HasLOBColumns && !fillReader && -1L == this.m_clientInitialLOBFS) || (fillReader && !returnPSTypes))
						{
							bLOBArrayFetchRequired = true;
						}
						if (this.m_cachedStmt == null || !this.m_cachedStmt.m_bDefinesDone || this.m_cachedStmt.m_internalInitialLOBFS != this.m_internalInitialLOBFS)
						{
							flag2 = true;
							columnDefines = TTCExecuteSql.InitDefines(this.m_sqlMetaData.m_columnDescribeInfo, this.m_internalInitialLOBFS);
							if (this.m_cachedStmt != null)
							{
								this.m_cachedStmt.m_internalInitialLOBFS = this.m_internalInitialLOBFS;
								this.m_cachedStmt.m_longFetchSize = (int)this.m_initialLongFS;
								this.m_cachedStmt.m_bDefinesDone = true;
							}
						}
					}
					else if (this.m_bInitialLongFetchSizeModified)
					{
						flag3 = true;
						this.m_bInitialLongFetchSizeModified = false;
						if (this.m_cachedStmt != null)
						{
							this.m_cachedStmt.m_longFetchSize = (int)this.m_initialLongFS;
						}
					}
				}
				long num = 0L;
				bool bDisableCompressedFetch = false;
				if (this.m_cachedStmt != null)
				{
					bDisableCompressedFetch = this.m_cachedStmt.m_bDisableCompressedFetch;
				}
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					this.m_connectionImpl.AddAllPiggyBackRequests();
					TTCExecuteSql executeSqlObject = this.m_connectionImpl.ExecuteSqlObject;
					TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
					if ((this.m_bForRefCursor && this.m_bFetchForRefCursorFirstTime) || flag2 || flag3)
					{
						executeSqlObject.SendExecuteRequest(null, null, false, this.m_cursorId, 0L, columnDefines, (long)noOfRowsToFetch, false, false, true, flag2, false, bDisableCompressedFetch, SqlStatementType.SELECT, (int)this.m_initialLongFS, 0, this.m_snapshotSCN, ref @null, 0);
						if (this.m_bForRefCursor && this.m_bFetchForRefCursorFirstTime)
						{
							this.m_bFetchForRefCursorFirstTime = false;
						}
					}
					else
					{
						TTCFetch ttcfetchObject = this.m_connectionImpl.TTCFetchObject;
						ttcfetchObject.WriteMessage(this.m_cursorId, noOfRowsToFetch);
					}
					long[] scnFromExecution = null;
					long[] array = null;
					List<TTCResultSet> list = null;
					bool bDefineDone = (this.m_cachedStmt != null && this.m_cachedStmt.m_bDefinesDone) || flag2;
					executeSqlObject.ReceiveExecuteResponse(ref this.m_accessors, null, false, ref this.m_sqlMetaData, SqlStatementType.SELECT, (long)this.m_rowsFetchedLastTime, noOfRowsToFetch, out this.m_rowsFetched, ref num, (int)this.m_initialLongFS, this.m_internalInitialLOBFS, scnFromExecution, @null.m_bAllInBinds, 0, ref this.m_dataUnmarshaller, ref @null, out array, bDefineDone, ref flag, ref list, bLOBArrayFetchRequired);
					this.m_rowsFetchedLastTime = this.m_rowsFetched;
					if (this.m_cachedStmt != null)
					{
						this.m_cachedStmt.m_numRowsFetchArrayCanAccomodate = (long)noOfRowsToFetch;
					}
					TTCError ttcerrorObject = this.m_connectionImpl.m_marshallingEngine.TTCErrorObject;
					this.m_bHasMoreRowsInDB = true;
					if (ttcerrorObject.ErrorCode != 0)
					{
						if (ttcerrorObject.ErrorCode != 1403)
						{
							char[] chars = ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Dequeue();
							string errMsg = ttcerrorObject.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(ttcerrorObject.ErrorMessage, 0, ttcerrorObject.ErrorMessage.Length, chars, true);
							ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Enqueue(ref chars);
							throw new OracleException(ttcerrorObject.ErrorCode, string.Empty, string.Empty, errMsg);
						}
						this.m_bHasMoreRowsInDB = false;
						ttcerrorObject.Initialize();
					}
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				if (this.m_dataUnmarshaller != null && this.m_dataUnmarshaller.m_charArrayForConversion == null)
				{
					this.m_dataUnmarshaller.m_charArrayForConversion = this.m_connectionImpl.m_marshallingEngine.m_charArrayPooler.Dequeue();
				}
				rowsFetched = this.m_rowsFetched;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				if (ex is OracleException)
				{
					this.m_connectionImpl.m_lastErrorNum = ((OracleException)ex).Number;
				}
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return rowsFetched;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000B126C File Offset: 0x000AF46C
		internal void DoLobArrayRead()
		{
			TTCLobAccessor[] array = null;
			TTCLobAccessor[] array2 = null;
			int num = 0;
			int num2 = 0;
			if (this.m_sqlMetaData.m_numOfCLOBCols > 0)
			{
				array = new TTCLobAccessor[this.m_sqlMetaData.m_numOfCLOBCols];
			}
			if (this.m_sqlMetaData.m_numOfBLOBCols > 0)
			{
				array2 = new TTCLobAccessor[this.m_sqlMetaData.m_numOfBLOBCols];
			}
			for (int i = 0; i < (int)this.m_sqlMetaData.m_noOfColumns; i++)
			{
				if (this.m_accessors[i].m_colMetaData.m_dataType == 112)
				{
					array[num++] = (TTCLobAccessor)this.m_accessors[i];
				}
				else if (this.m_accessors[i].m_colMetaData.m_dataType == 113)
				{
					array2[num2++] = (TTCLobAccessor)this.m_accessors[i];
				}
			}
			if (num + num2 != this.m_sqlMetaData.m_numOfLOBColumns)
			{
				throw new Exception("LOB Column Count inconsistent.");
			}
			if (num > 0)
			{
				this.LobArrayFetch(array, num);
				if (this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList.Count > 0)
				{
					this.m_tempOBList = this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList;
					this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList = new List<OraBuf>();
				}
			}
			if (num2 > 0)
			{
				this.LobArrayFetch(array2, num2);
				if (this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList.Count > 0)
				{
					if (this.m_tempOBList == null)
					{
						this.m_tempOBList = this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList;
						this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList = new List<OraBuf>();
						return;
					}
					this.m_tempOBList.AddRange(this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList);
					this.m_connectionImpl.m_marshallingEngine.m_oraBufRdr.m_tempOBList.Clear();
				}
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000B1450 File Offset: 0x000AF650
		internal void LobArrayFetch(TTCLobAccessor[] lobAccessors, int lobColCount)
		{
			long num = 0L;
			long num2 = 0L;
			byte[] array = null;
			bool flag = false;
			int num3 = this.m_rowsFetched * lobColCount;
			int num4 = 0;
			int num5 = 0;
			byte[][] array2 = new byte[num3][];
			long[] array3 = new long[num3];
			long[] array4 = new long[num3];
			for (int i = 0; i < this.m_rowsFetched; i++)
			{
				for (int j = 0; j < lobColCount; j++)
				{
					lobAccessors[j].GetLOBInfoForArrayRead(i, out num, out array, out num2);
					if (array != null && this.m_internalInitialLOBFS < num2)
					{
						flag = true;
						num5++;
						array2[num4] = array;
						array3[num4] = num2 - this.m_internalInitialLOBFS;
						array4[num4] = this.m_internalInitialLOBFS + 1L;
					}
					else
					{
						array2[num4] = null;
						array3[num4] = -1L;
						array4[num4] = -1L;
					}
					num4++;
				}
			}
			if (flag)
			{
				TTCClob ttcclob = new TTCClob(this.m_connectionImpl.m_marshallingEngine);
				try
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					ttcclob.LobArrayRead(lobAccessors, array2, array3, array4, num5, lobColCount);
				}
				finally
				{
					this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				}
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000B1578 File Offset: 0x000AF778
		internal byte[] GetByteRepresentation(int currentRow, int columnIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			byte[] array = null;
			byte[] result;
			try
			{
				if (this.m_accessors[columnIndex] != null)
				{
					array = this.m_accessors[columnIndex].GetByteRepresentation(this.m_dataUnmarshaller, currentRow, columnIndex);
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

		// Token: 0x0600106C RID: 4204 RVA: 0x000B160C File Offset: 0x000AF80C
		internal byte[] GetLobLocator(int currentRow, int columnIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			byte[] result;
			try
			{
				switch (this.m_accessors[columnIndex].m_colMetaData.m_dataType)
				{
				case 112:
				case 113:
				case 114:
				{
					byte[] lobLocator = ((TTCLobAccessor)this.m_accessors[columnIndex]).GetLobLocator(currentRow);
					result = lobLocator;
					break;
				}
				default:
					throw new Exception("Internal Error");
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

		// Token: 0x0600106D RID: 4205 RVA: 0x000B16C8 File Offset: 0x000AF8C8
		internal double GetDouble(int currentRow, int columnIndex, out byte[] byteRep)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			double @double;
			try
			{
				@double = this.m_accessors[columnIndex].GetDouble(this.m_dataUnmarshaller, currentRow, columnIndex, out byteRep);
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
			return @double;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000B174C File Offset: 0x000AF94C
		internal float GetFloat(int currentRow, int columnIndex, out byte[] byteRep)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			float @float;
			try
			{
				@float = this.m_accessors[columnIndex].GetFloat(this.m_dataUnmarshaller, currentRow, columnIndex, out byteRep);
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
			return @float;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x000B17D0 File Offset: 0x000AF9D0
		internal bool IsCompleteDataForLongAvailable(int currentRow, int columnIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				result = (this.m_accessors[columnIndex] as TTCLongAccessor).IsCompleteDataAvailable(currentRow);
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

		// Token: 0x06001070 RID: 4208 RVA: 0x000B1854 File Offset: 0x000AFA54
		internal int GetColumnOrdinal(string colName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				bool flag = false;
				if (colName.StartsWith("\"") && colName.EndsWith("\""))
				{
					flag = true;
					colName = colName.Trim(new char[]
					{
						'"'
					});
				}
				int num = this.m_sqlMetaData.m_columnDescribeInfo.Length;
				for (int i = 0; i < num; i++)
				{
					if (colName.Equals(this.m_sqlMetaData.m_columnDescribeInfo[i].pColAlias))
					{
						return i;
					}
				}
				if (!flag)
				{
					for (int i = 0; i < num; i++)
					{
						if (colName.Equals(this.m_sqlMetaData.m_columnDescribeInfo[i].pColAlias, StringComparison.InvariantCultureIgnoreCase))
						{
							return i;
						}
					}
				}
				throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_NAME, new string[0]));
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
			int result;
			return result;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000B1978 File Offset: 0x000AFB78
		internal long GetBytes(int currentRow, int columnIndex, long fieldOffset, byte[] buffer, int bufferOffset, int length, byte[] lobLocator, bool bLOBArrayReadDone, ref OracleBlobImpl tempBlobImpl)
		{
			long result;
			if (buffer == null)
			{
				if (this.m_connectionImpl.m_marshallingEngine.DBVersion >= 11100)
				{
					result = ((TTCLobAccessor)this.m_accessors[columnIndex]).GetTotalLobLengthInDB(this.m_dataUnmarshaller, currentRow, columnIndex, bLOBArrayReadDone);
				}
				else
				{
					if (lobLocator == null)
					{
						lobLocator = ((TTCLobAccessor)this.m_accessors[columnIndex]).GetLobLocator(currentRow);
					}
					if (tempBlobImpl == null)
					{
						tempBlobImpl = new OracleBlobImpl(this.m_connectionImpl, lobLocator);
					}
					else
					{
						tempBlobImpl.m_lobLocator = lobLocator;
					}
					result = tempBlobImpl.GetLength();
				}
			}
			else
			{
				fieldOffset += 1L;
				if (bLOBArrayReadDone)
				{
					result = OracleBlobImpl.CopyBlobDataInBytes(currentRow, this.m_connectionImpl, (TTCLobAccessor)this.m_accessors[columnIndex], fieldOffset, buffer, bufferOffset, length);
				}
				else
				{
					if (lobLocator == null)
					{
						lobLocator = ((TTCLobAccessor)this.m_accessors[columnIndex]).GetLobLocator(currentRow);
					}
					if (tempBlobImpl == null)
					{
						tempBlobImpl = new OracleBlobImpl(this.m_connectionImpl, lobLocator);
					}
					else
					{
						tempBlobImpl.m_lobLocator = lobLocator;
					}
					result = tempBlobImpl.Read(fieldOffset, (long)length, (long)bufferOffset, ref buffer);
				}
			}
			return result;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x000B1A84 File Offset: 0x000AFC84
		internal long GetBytes(OracleConnection connection, int currentRow, int columnIndex, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			long result;
			try
			{
				long num = 0L;
				OraType internalType = this.m_accessors[columnIndex].m_internalType;
				if (OraType.ORA_RAW == internalType)
				{
					if (buffer == null)
					{
						num = (this.m_accessors[columnIndex] as TTCRawAccessor).GetDataLen(this.m_dataUnmarshaller, currentRow, columnIndex);
					}
					else
					{
						num = (this.m_accessors[columnIndex] as TTCRawAccessor).GetDataInBuffer(this.m_dataUnmarshaller, currentRow, columnIndex, fieldOffset, buffer, bufferOffset, length);
					}
				}
				else if (OraType.ORA_OCIBFileLocator == internalType)
				{
					byte[] lobLocator = (this.m_accessors[columnIndex] as TTCLobAccessor).GetLobLocator(currentRow);
					OracleBFileImpl oracleBFileImpl = new OracleBFileImpl(this.m_connectionImpl, lobLocator);
					oracleBFileImpl.OpenFile();
					fieldOffset += 1L;
					try
					{
						if (buffer != null)
						{
							num = oracleBFileImpl.Read(fieldOffset, (long)length, (long)bufferOffset, ref buffer);
						}
						else
						{
							num = oracleBFileImpl.GetLength();
						}
					}
					finally
					{
						oracleBFileImpl.CloseFile();
						oracleBFileImpl = null;
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

		// Token: 0x06001073 RID: 4211 RVA: 0x000B1BC0 File Offset: 0x000AFDC0
		internal long GetChars(int currentRow, int columnIndex, long fieldOffset, char[] buffer, int bufferOffset, int length, byte[] lobLocator, bool bLOBArrayReadDone, ref OracleClobImpl tempClobImpl)
		{
			long result;
			if (buffer == null)
			{
				if (this.m_connectionImpl.m_marshallingEngine.DBVersion >= 11100)
				{
					result = ((TTCLobAccessor)this.m_accessors[columnIndex]).GetTotalLobLengthInDB(this.m_dataUnmarshaller, currentRow, columnIndex, bLOBArrayReadDone);
				}
				else
				{
					TTCLobAccessor ttclobAccessor = (TTCLobAccessor)this.m_accessors[columnIndex];
					if (lobLocator == null)
					{
						lobLocator = ttclobAccessor.GetLobLocator(currentRow);
					}
					if (tempClobImpl == null)
					{
						tempClobImpl = new OracleClobImpl(this.m_connectionImpl, lobLocator, ttclobAccessor.m_colMetaData.m_characterSetForm == 2);
					}
					else
					{
						tempClobImpl.m_lobLocator = lobLocator;
						tempClobImpl.m_clobFormOfUse = (byte)ttclobAccessor.m_colMetaData.m_characterSetForm;
						tempClobImpl.m_isNClob = (ttclobAccessor.m_colMetaData.m_characterSetForm == 2);
					}
					result = tempClobImpl.GetLength();
				}
			}
			else
			{
				fieldOffset += 1L;
				if (bLOBArrayReadDone)
				{
					result = OracleClobImpl.GetClobDataInChars(currentRow, this.m_connectionImpl, lobLocator, (TTCLobAccessor)this.m_accessors[columnIndex], fieldOffset, buffer, bufferOffset, length);
				}
				else
				{
					TTCLobAccessor ttclobAccessor2 = (TTCLobAccessor)this.m_accessors[columnIndex];
					if (lobLocator == null)
					{
						lobLocator = ttclobAccessor2.GetLobLocator(currentRow);
					}
					if (tempClobImpl == null)
					{
						tempClobImpl = new OracleClobImpl(this.m_connectionImpl, lobLocator, ttclobAccessor2.m_colMetaData.m_characterSetForm == 2);
					}
					else
					{
						tempClobImpl.m_lobLocator = lobLocator;
						tempClobImpl.m_clobFormOfUse = (byte)ttclobAccessor2.m_colMetaData.m_characterSetForm;
						tempClobImpl.m_isNClob = (ttclobAccessor2.m_colMetaData.m_characterSetForm == 2);
					}
					result = tempClobImpl.Read(fieldOffset, (long)length, (long)bufferOffset, ref buffer);
				}
			}
			return result;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000B1D4C File Offset: 0x000AFF4C
		internal long GetChars(OracleConnection connection, int currentRow, int columnIndex, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			long result;
			try
			{
				long num = 0L;
				OraType internalType = this.m_accessors[columnIndex].m_internalType;
				if (OraType.ORA_CHAR == internalType || OraType.ORA_CHARN == internalType)
				{
					if (buffer == null)
					{
						num = (long)(this.m_accessors[columnIndex] as TTCVarcharAccessor).GetCharLengthFromBuffer(this.m_dataUnmarshaller, currentRow, columnIndex, fieldOffset, (byte)this.m_accessors[columnIndex].m_colMetaData.m_characterSetForm);
					}
					else
					{
						num = (long)(this.m_accessors[columnIndex] as TTCVarcharAccessor).GetCharsFromBuffer(this.m_dataUnmarshaller, currentRow, columnIndex, fieldOffset, buffer, bufferOffset, length, (byte)this.m_accessors[columnIndex].m_colMetaData.m_characterSetForm);
					}
				}
				else if (OraType.ORA_ROWID == internalType || OraType.ORA_UROWID == internalType)
				{
					TTCRowIdAccessor ttcrowIdAccessor = this.m_accessors[columnIndex] as TTCRowIdAccessor;
					if (ttcrowIdAccessor != null)
					{
						num = ttcrowIdAccessor.GetChars(this.m_dataUnmarshaller, currentRow, columnIndex, fieldOffset, buffer, bufferOffset, length);
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

		// Token: 0x06001075 RID: 4213 RVA: 0x000B1E7C File Offset: 0x000B007C
		internal bool ConfigureNextResult()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				if (this.m_refCursors != null && this.m_currentRefCursorIndex < this.m_refCursors.Count)
				{
					OracleRefCursor oracleRefCursor = this.m_refCursors[this.m_currentRefCursorIndex];
					if (oracleRefCursor != null)
					{
						oracleRefCursor.Dispose();
					}
				}
				this.m_currentRefCursorIndex++;
				if (this.m_refCursors == null || this.m_currentRefCursorIndex >= this.m_refCursors.Count)
				{
					result = false;
				}
				else
				{
					if (this.m_cursorId > 0)
					{
						this.m_connectionImpl.AddCursorIdToBeClosed((long)this.m_cursorId);
						this.m_cursorId = 0;
					}
					this.m_bFetchForRefCursorFirstTime = true;
					this.m_rowsFetchedLastTime = -1;
					this.m_rowsFetched = 0;
					OracleRefCursor oracleRefCursor2 = this.m_refCursors[this.m_currentRefCursorIndex];
					if (oracleRefCursor2 != null && !oracleRefCursor2.IsNull)
					{
						this.m_bHasMoreRowsInDB = true;
						this.m_cursorId = oracleRefCursor2.m_refCursorImpl.m_cursorId;
						this.m_sqlMetaData = oracleRefCursor2.m_refCursorImpl.m_sqlMetaData;
						this.m_accessors = oracleRefCursor2.m_refCursorImpl.m_accessors;
						this.m_sessionTimeZone = oracleRefCursor2.m_sessionTimeZone;
					}
					else
					{
						this.m_bHasMoreRowsInDB = false;
						this.m_cursorId = 0;
						this.m_sqlMetaData = null;
						this.m_accessors = null;
						this.m_sessionTimeZone = OracleIntervalDS.Null;
					}
					result = true;
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

		// Token: 0x06001076 RID: 4214 RVA: 0x000B202C File Offset: 0x000B022C
		internal void CollectTempLOBsToBeFreed(int rowNumber)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (this.m_accessors != null)
			{
				for (int i = 0; i < this.m_accessors.Length; i++)
				{
					if (this.m_accessors[i] is TTCLobAccessor)
					{
						TTCLobAccessor ttclobAccessor = this.m_accessors[i] as TTCLobAccessor;
						if (ttclobAccessor.AbstractOrTempLOB(rowNumber))
						{
							byte[] lobLocator = ttclobAccessor.GetLobLocator(rowNumber);
							if (lobLocator != null)
							{
								if (this.m_connectionImpl.TemporaryLobReferenceGet(TTCLob.GetLobIdString(lobLocator)) == null)
								{
									this.m_connectionImpl.AddTempLOBsToBeFreed(lobLocator);
								}
								ttclobAccessor.m_lobLocators[rowNumber].Clear();
							}
						}
						else if (ttclobAccessor.m_lobLocators[rowNumber] != null && ttclobAccessor.m_lobLocators[rowNumber].Count > 0)
						{
							ttclobAccessor.m_lobLocators[rowNumber].Clear();
						}
					}
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x04001313 RID: 4883
		internal Accessor[] m_accessors;

		// Token: 0x04001314 RID: 4884
		private OracleConnectionImpl m_connectionImpl;

		// Token: 0x04001315 RID: 4885
		private int m_cursorId;

		// Token: 0x04001316 RID: 4886
		private int m_rowsFetchedLastTime = -1;

		// Token: 0x04001317 RID: 4887
		internal List<object> m_oraBufReleaseInfoList;

		// Token: 0x04001318 RID: 4888
		internal List<OraBuf> m_tempOBList;

		// Token: 0x04001319 RID: 4889
		internal SQLMetaData m_sqlMetaData;

		// Token: 0x0400131A RID: 4890
		internal bool m_bForRefCursor;

		// Token: 0x0400131B RID: 4891
		private bool m_bFetchForRefCursorFirstTime = true;

		// Token: 0x0400131C RID: 4892
		internal long[] m_snapshotSCN;

		// Token: 0x0400131D RID: 4893
		private bool m_bInitialLongFetchSizeModified;

		// Token: 0x0400131E RID: 4894
		internal long m_initialLongFS;

		// Token: 0x0400131F RID: 4895
		internal long m_clientInitialLOBFS;

		// Token: 0x04001320 RID: 4896
		internal long m_internalInitialLOBFS;

		// Token: 0x04001321 RID: 4897
		internal int m_rowsFetched;

		// Token: 0x04001322 RID: 4898
		internal bool m_bHasMoreRowsInDB;

		// Token: 0x04001323 RID: 4899
		private List<OracleRefCursor> m_refCursors;

		// Token: 0x04001324 RID: 4900
		internal int m_currentRefCursorIndex;

		// Token: 0x04001325 RID: 4901
		private CachedStatement m_cachedStmt;

		// Token: 0x04001326 RID: 4902
		internal OracleIntervalDS m_sessionTimeZone;

		// Token: 0x04001327 RID: 4903
		internal int m_numberOfHiddenColumns;

		// Token: 0x04001328 RID: 4904
		internal DataUnmarshaller m_dataUnmarshaller;

		// Token: 0x04001329 RID: 4905
		internal bool m_bPooled;

		// Token: 0x0400132A RID: 4906
		private object m_lock = new object();

		// Token: 0x0400132B RID: 4907
		private bool m_closed;

		// Token: 0x0400132C RID: 4908
		internal Action OnClose;
	}
}
