using System;
using System.Collections.Generic;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.I18N;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x02000228 RID: 552
	internal class TTCExecuteSql : TTCFunction
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x000D9998 File Offset: 0x000D7B98
		internal TTCExecuteSql(MarshallingEngine mEngine) : base(mEngine, 94, 0)
		{
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000D99B4 File Offset: 0x000D7BB4
		internal override void ReInit(MarshallingEngine marshallingEngine)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.ReInit(marshallingEngine);
				if (this.m_reExecuteSql != null)
				{
					this.m_reExecuteSql.ReInit(marshallingEngine);
				}
				this.m_al8i4 = new long[13];
				this.m_sessionTimeZone = null;
				this.m_bSessionTimeZoneUpdated = false;
				if (this.m_rowHeader != null)
				{
					this.m_rowHeader.ReInit(this.m_marshallingEngine);
				}
				if (this.m_rowData != null)
				{
					this.m_rowData.ReInit(this.m_marshallingEngine);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x000D9A8C File Offset: 0x000D7C8C
		internal TTCReExecuteSql ReExecuteSqlObject
		{
			get
			{
				if (this.m_reExecuteSql == null)
				{
					this.m_reExecuteSql = new TTCReExecuteSql(this.m_marshallingEngine);
				}
				return this.m_reExecuteSql;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000D9AB0 File Offset: 0x000D7CB0
		internal void SendReExecuteRequest(OracleConnectionImpl commImpl, int cursorId, long noOfRowsToFetch, bool bAutoCommit, bool bDisableCompressedFetch, SqlStatementType stmtType, int arrayBindCount, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalArrayBindValuesHelper)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			short ttcCallCode = 4;
			int num = 0;
			int num2 = 0;
			long numIterations = 1L;
			bool bArrayBinding = false;
			try
			{
				if (stmtType == SqlStatementType.SELECT && noOfRowsToFetch > 0L)
				{
					ttcCallCode = 78;
					num |= 32;
					if (bDisableCompressedFetch)
					{
						num |= 262144;
					}
					numIterations = noOfRowsToFetch;
				}
				else
				{
					if (arrayBindCount > 0)
					{
						bArrayBinding = true;
						numIterations = (long)arrayBindCount;
					}
					if (bAutoCommit)
					{
						num2 |= 1;
					}
				}
				TTCExecuteSql.ValidateTransactionContext(commImpl);
				this.ReExecuteSqlObject.WriteMessage(ttcCallCode, cursorId, num, num2, numIterations, bArrayBinding, ref marshalArrayBindValuesHelper);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000D9B7C File Offset: 0x000D7D7C
		internal void SendExecuteRequest(OracleConnectionImpl commImpl, byte[] sqlStmtByteStream, bool bHasReturningClause, int cursorId, long dbChangeRegistrationId, ColumnDescribeInfo[] columnDefines, long noOfRowsToFetch, bool parse, bool execute, bool fetch, bool define, bool bAutoCommit, bool bDisableCompressedFetch, SqlStatementType stmtType, int longFetchSize, int arrayBindCount, long[] scnForSnapshot, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindParamsHelper, int startIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (dbChangeRegistrationId > 0L && stmtType != SqlStatementType.SELECT && SqlStatementType.PLSQL != stmtType)
				{
					throw new OracleException(29973, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(29973, new string[0]));
				}
				int noOfParameters = (marshalBindParamsHelper.m_paramCollInfoArray == null) ? 0 : marshalBindParamsHelper.m_paramCollInfoArray.Length;
				for (int i = 0; i < this.m_al8i4.Length; i++)
				{
					this.m_al8i4[i] = 0L;
				}
				long executeOptions = this.GetExecuteOptions(parse, execute, fetch, define, bAutoCommit, bDisableCompressedFetch, bHasReturningClause, noOfParameters, arrayBindCount, stmtType);
				if ((executeOptions & 1L) > 0L)
				{
					this.m_al8i4[0] = 1L;
				}
				else
				{
					this.m_al8i4[0] = 0L;
				}
				if (stmtType == SqlStatementType.OTHERS)
				{
					this.m_al8i4[1] = 1L;
				}
				else if (stmtType == SqlStatementType.DML || stmtType == SqlStatementType.PLSQL)
				{
					if (arrayBindCount > 0)
					{
						this.m_al8i4[1] = (long)arrayBindCount;
						if (stmtType == SqlStatementType.DML)
						{
							this.m_al8i4[9] = 16384L;
						}
					}
					else
					{
						this.m_al8i4[1] = 1L;
					}
				}
				else if (fetch)
				{
					this.m_al8i4[1] = noOfRowsToFetch;
				}
				else
				{
					this.m_al8i4[1] = 0L;
				}
				if (scnForSnapshot != null)
				{
					this.m_al8i4[5] = scnForSnapshot[0];
					this.m_al8i4[6] = scnForSnapshot[1];
				}
				else
				{
					this.m_al8i4[5] = (this.m_al8i4[6] = 0L);
				}
				if (stmtType == SqlStatementType.SELECT)
				{
					this.m_al8i4[7] = 1L;
				}
				else
				{
					this.m_al8i4[7] = 0L;
				}
				if ((executeOptions & 32L) != 0L)
				{
					this.m_al8i4[9] |= 32768L;
				}
				else
				{
					this.m_al8i4[9] &= -32769L;
				}
				int numDefineCols = 0;
				if (define && columnDefines != null)
				{
					numDefineCols = columnDefines.Length;
				}
				TTCExecuteSql.ValidateTransactionContext(commImpl);
				this.WriteOall8Message(sqlStmtByteStream, cursorId, dbChangeRegistrationId, executeOptions, columnDefines, longFetchSize, arrayBindCount, numDefineCols, noOfRowsToFetch, stmtType, ref marshalBindParamsHelper, startIndex);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000D9DD4 File Offset: 0x000D7FD4
		private static void ValidateTransactionContext(OracleConnectionImpl commImpl)
		{
			Transaction transaction = null;
			if (commImpl != null && null != commImpl.m_lastEnlistedTransaction)
			{
				try
				{
					transaction = Transaction.Current;
					if (null == transaction)
					{
						try
						{
							TransactionStatus status = commImpl.m_lastEnlistedTransaction.TransactionInformation.Status;
							transaction = commImpl.m_lastEnlistedTransaction;
						}
						catch
						{
							commImpl.m_lastEnlistedTransaction = null;
							return;
						}
					}
				}
				catch
				{
					commImpl.m_lastEnlistedTransaction = null;
					return;
				}
			}
			if (null != transaction && transaction == commImpl.m_lastEnlistedTransaction && transaction.TransactionInformation.Status != TransactionStatus.Active)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_TXN_NOT_DISPOSED, new string[0]));
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000D9E8C File Offset: 0x000D808C
		private void WriteOall8Message(byte[] sqlStmtByteStream, int cursorId, long dbChangeRegistrationId, long executeOptions, ColumnDescribeInfo[] colDefinesInfoArray, int longFetchSize, int arrayBindCount, int numDefineCols, long noOfRowsToFetch, SqlStatementType stmtType, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindParamsHelper, int startIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				int paramLength = 0;
				if (marshalBindParamsHelper.m_paramCollInfoArray != null)
				{
					paramLength = marshalBindParamsHelper.m_paramCollInfoArray.Length;
				}
				this.WritePisdef(executeOptions, cursorId, dbChangeRegistrationId, sqlStmtByteStream, paramLength, longFetchSize, numDefineCols, noOfRowsToFetch, arrayBindCount, stmtType);
				this.WritePisdefData(executeOptions, sqlStmtByteStream, colDefinesInfoArray, arrayBindCount, ref marshalBindParamsHelper, startIndex);
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000D9F4C File Offset: 0x000D814C
		private void WritePisdef(long executeOptions, int cursorId, long dbChangeRegistrationId, byte[] sqlStmtByteStream, int paramLength, int longFetchSize, int numDefineCols, long noOfRowsToFetch, int arrayBindCount, SqlStatementType stmtType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_marshallingEngine.MarshalUB4(executeOptions);
				this.m_marshallingEngine.MarshalSWORD(cursorId);
				int value = 0;
				if (cursorId > 0)
				{
					this.m_marshallingEngine.MarshalNullPointer();
				}
				else
				{
					this.m_marshallingEngine.MarshalPointer();
					if (sqlStmtByteStream != null)
					{
						value = sqlStmtByteStream.Length;
					}
				}
				this.m_marshallingEngine.MarshalSWORD(value);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalSWORD(this.m_al8i4.Length);
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				if ((executeOptions & 64L) == 0L && (executeOptions & 32L) != 0L && (executeOptions & 1L) != 0L && stmtType == SqlStatementType.SELECT)
				{
					this.m_marshallingEngine.MarshalUB4(0L);
					this.m_marshallingEngine.MarshalUB4(noOfRowsToFetch);
				}
				else
				{
					this.m_marshallingEngine.MarshalUB4(0L);
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				if (longFetchSize == -1)
				{
					this.m_marshallingEngine.MarshalUB4(2147483647L);
				}
				else if (longFetchSize == 0)
				{
					this.m_marshallingEngine.MarshalUB4(1L);
				}
				else
				{
					this.m_marshallingEngine.MarshalUB4((long)longFetchSize);
				}
				if ((executeOptions & 8L) != 0L && paramLength > 0)
				{
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalSWORD(paramLength);
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalSWORD(0);
				}
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalNullPointer();
				if ((executeOptions & 16L) != 0L && numDefineCols > 0)
				{
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalSWORD(numDefineCols);
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalSWORD(0);
				}
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 4)
				{
					int num = (int)(dbChangeRegistrationId & (long)((ulong)-1));
					int num2 = (int)((ulong)(dbChangeRegistrationId & -4294967296L) >> 32);
					this.m_marshallingEngine.MarshalUB4((long)num);
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalPointer();
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 5)
					{
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalUB4(0L);
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalUB4(0L);
						this.m_marshallingEngine.MarshalUB4((long)num2);
						if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7)
						{
							if (SqlStatementType.DML == stmtType && arrayBindCount > 0)
							{
								this.m_marshallingEngine.MarshalPointer();
								this.m_marshallingEngine.MarshalUB4((long)arrayBindCount);
								this.m_marshallingEngine.MarshalPointer();
							}
							else
							{
								this.m_marshallingEngine.MarshalNullPointer();
								this.m_marshallingEngine.MarshalUB4(0L);
								this.m_marshallingEngine.MarshalNullPointer();
							}
							if (this.m_marshallingEngine.NegotiatedTTCVersion >= 8)
							{
								this.m_marshallingEngine.MarshalNullPointer();
								this.m_marshallingEngine.MarshalUB4(0L);
								this.m_marshallingEngine.MarshalNullPointer();
								this.m_marshallingEngine.MarshalUB4(0L);
								this.m_marshallingEngine.MarshalNullPointer();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000DA2DC File Offset: 0x000D84DC
		internal void WritePisdefData(long executeOptions, byte[] sqlStmtByteStream, ColumnDescribeInfo[] colDefinesInfoArray, int arrayBindCount, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindParamsHelper, int startIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (sqlStmtByteStream != null)
				{
					this.m_marshallingEngine.MarshalCHR(sqlStmtByteStream);
				}
				this.m_marshallingEngine.MarshalUB4Array(this.m_al8i4);
				ColumnDescribeInfo[] paramCollInfoArray = marshalBindParamsHelper.m_paramCollInfoArray;
				if (paramCollInfoArray != null)
				{
					int num = paramCollInfoArray.Length;
					if ((executeOptions & 8L) != 0L && num > 0)
					{
						TTCExecuteSql.MarshalBindMetaData(this.m_marshallingEngine, paramCollInfoArray);
					}
					if ((executeOptions & 16L) != 0L)
					{
						TTCExecuteSql.MarshalDefines(this.m_marshallingEngine, colDefinesInfoArray);
					}
					if ((executeOptions & 32L) != 0L && num > 0)
					{
						if (arrayBindCount > 0)
						{
							TTCExecuteSql.MarshalValuesForArrayBind(this.m_marshallingEngine, arrayBindCount, startIndex, ref marshalBindParamsHelper);
						}
						else
						{
							TTCExecuteSql.MarshalBindValues(this.m_marshallingEngine, ref marshalBindParamsHelper);
						}
					}
				}
				else if ((executeOptions & 16L) != 0L)
				{
					TTCExecuteSql.MarshalDefines(this.m_marshallingEngine, colDefinesInfoArray);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000DA3F0 File Offset: 0x000D85F0
		internal static void MarshalValuesForArrayBind(MarshallingEngine mEngine, int arrayBindCount, int startIndex, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalArrayBindValuesHelper)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			marshalArrayBindValuesHelper.m_IsInArrayBindingMode = true;
			try
			{
				if (marshalArrayBindValuesHelper.m_sqlStmtType != SqlStatementType.PLSQL || !marshalArrayBindValuesHelper.m_bAllOutBinds)
				{
					marshalArrayBindValuesHelper.MarshalArrayBindValues(mEngine, startIndex, arrayBindCount);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000DA484 File Offset: 0x000D8684
		private static void MarshalDefines(MarshallingEngine mEngine, ColumnDescribeInfo[] definesArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				TTCExecuteSql.MarshalBindMetaData(mEngine, definesArray);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000DA4FC File Offset: 0x000D86FC
		private static void MarshalBindMetaData(MarshallingEngine mEngine, ColumnDescribeInfo[] paramCollInfoArray)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				int num = paramCollInfoArray.Length;
				for (int i = 0; i < num; i++)
				{
					TTCColumnMetaData.WriteMessage(mEngine, paramCollInfoArray[i]);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000DA584 File Offset: 0x000D8784
		internal static void MarshalBindValues(MarshallingEngine mEngine, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindValuesHelper)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			marshalBindValuesHelper.m_IsInArrayBindingMode = false;
			try
			{
				if (!marshalBindValuesHelper.m_bAllOutBinds)
				{
					marshalBindValuesHelper.MarshalBindValues(mEngine);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000DA60C File Offset: 0x000D880C
		internal static void MarshalParameterValue(MarshallingEngine mEngine, object paramValue, OraType dataType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (paramValue != null)
				{
					byte[] array = (byte[])paramValue;
					if (dataType <= OraType.ORA_LONGRAW)
					{
						switch (dataType)
						{
						case OraType.ORA_CHARN:
							goto IL_10A;
						case OraType.ORA_NUMBER:
							break;
						default:
							switch (dataType)
							{
							case OraType.ORA_VARNUM:
								break;
							case (OraType)7:
								goto IL_14F;
							case OraType.ORA_LONG:
								goto IL_10A;
							default:
								switch (dataType)
								{
								case OraType.ORA_RAW:
								case OraType.ORA_LONGRAW:
									goto IL_10A;
								default:
									goto IL_14F;
								}
								break;
							}
							break;
						}
						if (array.Length >= 22)
						{
							mEngine.MarshalCLR(array, 1, (int)array[0]);
							goto IL_15A;
						}
						mEngine.MarshalCLR(array, 0, array.Length);
						goto IL_15A;
					}
					else if (dataType != OraType.ORA_CHAR)
					{
						if (dataType != OraType.ORA_REFCURSOR)
						{
							switch (dataType)
							{
							case OraType.ORA_XMLTYPE:
								mEngine.MarshalDALC(null);
								mEngine.MarshalDALC(null);
								mEngine.MarshalDALC(null);
								mEngine.MarshalUB2(0);
								mEngine.MarshalUB4((long)array.Length);
								mEngine.MarshalUB2(1);
								mEngine.MarshalCLR(array, array.Length);
								goto IL_15A;
							case OraType.ORA_OCIRef:
							case (OraType)111:
							case (OraType)115:
								goto IL_14F;
							case OraType.ORA_OCICLobLocator:
							case OraType.ORA_OCIBLobLocator:
							case OraType.ORA_OCIBFileLocator:
							{
								int num = array.Length;
								mEngine.MarshalUB4((long)num);
								mEngine.MarshalCLR(array, 0, num);
								goto IL_15A;
							}
							case OraType.ORA_RESULTSET:
								break;
							default:
								goto IL_14F;
							}
						}
						int value = BitConverter.ToInt32(array, 0);
						int noOfBytesToBeWritten = (int)mEngine.GetNoOfBytesToBeWritten(value, 2);
						mEngine.MarshalUB4((long)noOfBytesToBeWritten);
						mEngine.MarshalCLR(array, noOfBytesToBeWritten);
						goto IL_15A;
					}
					IL_10A:
					mEngine.MarshalCLR(array, array.Length);
					goto IL_15A;
					IL_14F:
					mEngine.MarshalCLR(array, 0, array.Length);
					IL_15A:;
				}
				else if (dataType == OraType.ORA_OCICLobLocator || dataType == OraType.ORA_OCIBLobLocator || dataType == OraType.ORA_OCIBFileLocator)
				{
					mEngine.MarshalUB4(0L);
				}
				else if (dataType == OraType.ORA_REFCURSOR)
				{
					mEngine.MarshalUB1(1);
					mEngine.MarshalUB1(0);
				}
				else if (dataType == OraType.ORA_XMLTYPE)
				{
					mEngine.MarshalDALC(null);
					mEngine.MarshalDALC(null);
					mEngine.MarshalDALC(null);
					mEngine.MarshalUB2(0);
					mEngine.MarshalUB4(0L);
					mEngine.MarshalUB2(1);
				}
				else if (dataType == OraType.ORA_BOOLEAN)
				{
					mEngine.MarshalUB1(253);
					mEngine.MarshalUB1(1);
				}
				else if (dataType != OraType.ORA_LONG && dataType != OraType.ORA_LONGRAW)
				{
					mEngine.MarshalUB1(0);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x000DA86C File Offset: 0x000D8A6C
		internal static void MarshalAssociativeArrayParameterValue(MarshallingEngine mEngine, object paramValue, OraType dataType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				if (paramValue != null)
				{
					int length = (paramValue as Array).Length;
					mEngine.MarshalUB4((long)length);
					for (int i = 0; i < length; i++)
					{
						byte[] array = ((byte[][])paramValue)[i];
						if (array == null)
						{
							mEngine.MarshalUB4(0L);
						}
						else if (dataType == OraType.ORA_NUMBER || dataType == OraType.ORA_VARNUM)
						{
							if (array.Length >= 22)
							{
								mEngine.MarshalCLR(array, 1, (int)array[0]);
							}
							else
							{
								mEngine.MarshalCLR(array, 0, array.Length);
							}
						}
						else
						{
							mEngine.MarshalCLR(array, array.Length);
						}
					}
				}
				else
				{
					mEngine.MarshalUB4(0L);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000DA958 File Offset: 0x000D8B58
		internal List<TTCResultSet> ProcessImplicitResultSet(ref List<TTCResultSet> implicitRSList)
		{
			int num = (int)this.m_marshallingEngine.UnmarshalUB4(false);
			TTCRefCursorAccessor ttcrefCursorAccessor = new TTCRefCursorAccessor(null, this.m_marshallingEngine);
			for (int i = 0; i < num; i++)
			{
				ttcrefCursorAccessor.UnmarshalOneRow();
			}
			if (implicitRSList != null)
			{
				implicitRSList.AddRange(ttcrefCursorAccessor.m_TTCResultSetList);
			}
			else
			{
				implicitRSList = ttcrefCursorAccessor.m_TTCResultSetList;
			}
			return implicitRSList;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000DA9B0 File Offset: 0x000D8BB0
		internal void ReceiveExecuteResponse(ref Accessor[] defineAccessors, Accessor[] bindAccessors, bool bHasReturningParams, ref SQLMetaData sqlMetaData, SqlStatementType statementType, long noOfRowsFetchedLastTime, int noOfRowsToFetch, out int noOfRowsFetched, ref long queryId, int longFetchSize, long initialLOBFetchSize, long[] scnFromExecution, bool bAllInputBinds, int arrayBindCount, ref DataUnmarshaller dataUnmarshaller, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindParamsHelper, out long[] rowsAffectedByArrayBind, bool bDefineDone, ref bool bMoreThanOneRowAffectedByDmlWithRetClause, ref List<TTCResultSet> implicitRSList, bool bLOBArrayFetchRequired = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			noOfRowsFetched = 0;
			rowsAffectedByArrayBind = null;
			bool flag = false;
			Exception ex = null;
			try
			{
				int num = 0;
				bool bIgnoreMetadata = sqlMetaData != null && sqlMetaData.bGotDescribeInfoFromDB;
				if (this.m_rowData != null)
				{
					this.m_rowData.ReInitialize();
				}
				if (this.m_rowHeader != null)
				{
					this.m_rowHeader.ReInitialize();
				}
				bool flag2 = false;
				this.m_marshallingEngine.TTCErrorObject.Initialize();
				while (!flag)
				{
					try
					{
						byte b = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
						byte b2 = b;
						switch (b2)
						{
						case 4:
							this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
							flag = true;
							continue;
						case 5:
						case 9:
						case 10:
						case 13:
						case 14:
						case 15:
							break;
						case 6:
							if (this.m_rowHeader == null)
							{
								this.m_rowHeader = new TTCRowHeader(this.m_marshallingEngine);
							}
							if (this.m_rowData == null)
							{
								this.m_rowData = new TTCRowData(this.m_marshallingEngine);
							}
							if (sqlMetaData != null)
							{
								this.m_rowData.SetNumberOfColumns((int)sqlMetaData.m_noOfColumns);
							}
							this.m_rowHeader.ReadMessage(this.m_rowData);
							if (flag2)
							{
								continue;
							}
							if (statementType == SqlStatementType.SELECT)
							{
								if (noOfRowsFetched == 0 && dataUnmarshaller != null)
								{
									if (this.m_rowData.m_bitVectorFound)
									{
										dataUnmarshaller.SaveAllDuplicateColumnsFromLastRow(this.m_rowData, defineAccessors, (int)noOfRowsFetchedLastTime);
									}
									else if (dataUnmarshaller.m_duplicateDataStore != null)
									{
										if (dataUnmarshaller.m_duplicateDataStore.Length != (int)sqlMetaData.m_noOfColumns)
										{
											dataUnmarshaller.m_duplicateDataStore = null;
										}
										else
										{
											for (int i = 0; i < dataUnmarshaller.m_duplicateDataStore.Length; i++)
											{
												dataUnmarshaller.m_duplicateDataStore[i] = null;
											}
										}
									}
								}
								int num2 = (int)sqlMetaData.m_noOfColumns * noOfRowsToFetch;
								if (dataUnmarshaller == null)
								{
									dataUnmarshaller = new DataUnmarshaller(this.m_marshallingEngine);
									this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset = new int[num2];
									this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray = new int[num2];
									this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo = new OraArraySegment[noOfRowsToFetch];
								}
								else
								{
									DataUnmarshaller.ReleaseAllOBs(dataUnmarshaller.m_oraArrSegWithColRowInfo, dataUnmarshaller.m_oraArrSegCount, this.m_marshallingEngine.m_oracleCommunication);
									if (dataUnmarshaller.m_colDataStartOffset == null || dataUnmarshaller.m_colDataStartOffset.Length < num2)
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset = new int[num2];
										this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray = new int[num2];
									}
									else
									{
										this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset = dataUnmarshaller.m_colDataStartOffset;
										this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray = dataUnmarshaller.m_indexOfOASArray;
									}
									if (dataUnmarshaller.m_oraArrSegWithColRowInfo == null || dataUnmarshaller.m_oraArrSegWithColRowInfo.Length < noOfRowsToFetch)
									{
										this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo = new OraArraySegment[noOfRowsToFetch];
									}
									else
									{
										this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo = dataUnmarshaller.m_oraArrSegWithColRowInfo;
									}
								}
								this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfoIndex = 0;
								if (defineAccessors == null)
								{
									defineAccessors = this.CreateDefineAccessors(sqlMetaData, longFetchSize, initialLOBFetchSize, bDefineDone, bLOBArrayFetchRequired, noOfRowsToFetch);
									continue;
								}
								foreach (Accessor accessor in defineAccessors)
								{
									if (accessor != null)
									{
										accessor.m_lastRowProcessed = 0;
										if (accessor is TTCLongAccessor && accessor.m_totalLengthOfData != null)
										{
											accessor.m_totalLengthOfData.Clear();
										}
										else if (accessor is TTCLobAccessor)
										{
											((TTCLobAccessor)accessor).ReInit(bLOBArrayFetchRequired, initialLOBFetchSize, noOfRowsToFetch);
											((TTCLobAccessor)accessor).m_isDefineDone = bDefineDone;
										}
									}
								}
								continue;
							}
							else
							{
								if (defineAccessors == null)
								{
									defineAccessors = new Accessor[(int)sqlMetaData.m_noOfColumns];
									continue;
								}
								continue;
							}
							break;
						case 7:
							flag2 = true;
							if (bHasReturningParams && bindAccessors != null)
							{
								int num3 = bindAccessors.Length;
								this.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = true;
								for (int k = 0; k < num3; k++)
								{
									if (bindAccessors[k] != null)
									{
										int num4 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
										if (num4 > 1)
										{
											bMoreThanOneRowAffectedByDmlWithRetClause = true;
										}
										if (num4 == 0)
										{
											bindAccessors[k].AddNullForData();
										}
										else
										{
											for (int l = 0; l < num4; l++)
											{
												bindAccessors[k].m_bReceivedOutValueFromServer = true;
												bindAccessors[k].UnmarshalOneRow();
											}
										}
									}
								}
								if (this.m_marshallingEngine.m_oraBufRdr.m_currentOB != null)
								{
									this.m_marshallingEngine.m_oraBufRdr.m_tempOBList.Add(this.m_marshallingEngine.m_oraBufRdr.m_currentOB);
									this.m_marshallingEngine.m_oraBufRdr.m_currentOB = null;
								}
								this.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = false;
								noOfRowsFetched++;
								continue;
							}
							if (this.m_rowData == null)
							{
								this.m_rowData = new TTCRowData(this.m_marshallingEngine);
							}
							if (bindAccessors != null && !bAllInputBinds)
							{
								if (noOfRowsFetched == 0)
								{
									this.m_marshallingEngine.m_oraBufRdr.StartAccumulatingRowData();
								}
								this.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = true;
								this.m_rowData.ReadRow(bindAccessors, bindAccessors.Length);
								if (this.m_marshallingEngine.m_oraBufRdr.m_currentOB != null)
								{
									this.m_marshallingEngine.m_oraBufRdr.m_tempOBList.Add(this.m_marshallingEngine.m_oraBufRdr.m_currentOB);
									this.m_marshallingEngine.m_oraBufRdr.m_currentOB = null;
								}
								this.m_marshallingEngine.m_oraBufRdr.m_bHoldOBTemporarily = false;
								for (int m = 0; m < num; m++)
								{
									TTCRefCursorAccessor ttcrefCursorAccessor = bindAccessors[m] as TTCRefCursorAccessor;
									if (ttcrefCursorAccessor != null)
									{
										Accessor[] defineAccessorForCurrentRow = this.CreateDefineAccessors(ttcrefCursorAccessor.SqlMetaDataForCurrentRow, longFetchSize, initialLOBFetchSize, bDefineDone, false, 0);
										ttcrefCursorAccessor.DefineAccessorForCurrentRow = defineAccessorForCurrentRow;
									}
								}
							}
							else
							{
								this.m_rowData.ReadRowNew(dataUnmarshaller, defineAccessors, noOfRowsFetched);
								this.m_marshallingEngine.m_oraBufRdr.UpdateOASMaxRow(noOfRowsFetched);
							}
							noOfRowsFetched++;
							continue;
						case 8:
							this.Process_RPA_Message(ref queryId, scnFromExecution, statementType, arrayBindCount, out rowsAffectedByArrayBind);
							continue;
						case 11:
							if (this.m_rowHeader == null)
							{
								this.m_rowHeader = new TTCRowHeader(this.m_marshallingEngine);
							}
							if (this.m_rowData == null)
							{
								this.m_rowData = new TTCRowData(this.m_marshallingEngine);
							}
							this.m_rowHeader.ReadMessage(this.m_rowData);
							num = this.m_rowHeader.m_noOfRequests;
							if (num > 0)
							{
								for (int n = 0; n < num; n++)
								{
									byte b3;
									if ((b3 = (byte)this.m_marshallingEngine.UnmarshalUB1(false)) != 0)
									{
										if (32 == b3)
										{
											bindAccessors[n] = null;
										}
										if (marshalBindParamsHelper.m_bindDirections == null)
										{
											marshalBindParamsHelper.m_bindDirections = new BindDirection[num];
										}
										marshalBindParamsHelper.m_bindDirections[n] = (BindDirection)b3;
									}
								}
								marshalBindParamsHelper.EvaluateBindDirections();
								bAllInputBinds = marshalBindParamsHelper.m_bAllInBinds;
								continue;
							}
							continue;
						case 12:
							if (this.m_rowData == null)
							{
								this.m_rowData = new TTCRowData(this.m_marshallingEngine);
							}
							else
							{
								this.m_rowData.ReInitialize();
							}
							if (this.m_rowHeader == null)
							{
								this.m_rowHeader = new TTCRowHeader(this.m_marshallingEngine);
							}
							else
							{
								this.m_rowHeader.ReInitialize();
							}
							this.m_rowHeader.ReadMessage(this.m_rowData);
							if (!marshalBindParamsHelper.Equals(TTCExecuteSql.MarshalBindParameterValueHelper.Null))
							{
								marshalBindParamsHelper.m_InTTISLGMode = true;
								marshalBindParamsHelper.MarshalBindParameters(this.m_marshallingEngine, this.m_rowHeader.m_iterationNumber, this.m_rowHeader.m_noOfIterations);
								marshalBindParamsHelper.m_InTTISLGMode = false;
								continue;
							}
							continue;
						case 16:
							if (sqlMetaData == null)
							{
								sqlMetaData = new SQLMetaData();
							}
							TTCDescribeInfo.ReadMessage(false, false, this.m_marshallingEngine, sqlMetaData, bIgnoreMetadata);
							if (this.m_rowData == null)
							{
								this.m_rowData = new TTCRowData(this.m_marshallingEngine);
								continue;
							}
							continue;
						default:
							switch (b2)
							{
							case 19:
								this.m_marshallingEngine.MarshalUB1(19);
								this.m_marshallingEngine.m_oraBufWriter.FlushData();
								continue;
							case 20:
							case 22:
								break;
							case 21:
							{
								int nbOfColumnSent = this.m_marshallingEngine.UnmarshalUB2(false);
								if (this.m_rowData == null)
								{
									this.m_rowData = new TTCRowData(this.m_marshallingEngine);
								}
								this.m_rowData.ReadBVC(nbOfColumnSent);
								if (noOfRowsFetched != 0 || dataUnmarshaller == null)
								{
									continue;
								}
								if (this.m_rowData.m_bitVectorFound)
								{
									dataUnmarshaller.SaveAllDuplicateColumnsFromLastRow(this.m_rowData, defineAccessors, (int)noOfRowsFetchedLastTime);
									continue;
								}
								if (dataUnmarshaller.m_duplicateDataStore != null)
								{
									for (int num5 = 0; num5 < dataUnmarshaller.m_duplicateDataStore.Length; num5++)
									{
										dataUnmarshaller.m_duplicateDataStore[num5] = null;
									}
									continue;
								}
								continue;
							}
							case 23:
								base.ProcessServerSidePiggybackFunction();
								continue;
							default:
								switch (b2)
								{
								case 27:
									this.ProcessImplicitResultSet(ref implicitRSList);
									continue;
								case 28:
									this.m_marshallingEngine.m_connImplReference.DoProtocolNegotiation();
									this.m_marshallingEngine.m_connImplReference.DoDataTypeNegotiation();
									continue;
								}
								break;
							}
							break;
						}
						throw new Exception("TTCExecuteSql:ReceiveExecuteResponse - Unexpected Packet received.");
					}
					catch (NetworkException ex2)
					{
						if (ex2.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.m_oracleCommunication.Reset();
					}
					catch (Exception ex3)
					{
						ex = ex3;
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.m_oracleCommunication.Reset();
					}
				}
				if (ex != null)
				{
					throw ex;
				}
				if (statementType == SqlStatementType.SELECT && dataUnmarshaller != null)
				{
					dataUnmarshaller.m_columnCount = (int)sqlMetaData.m_noOfColumns;
					dataUnmarshaller.m_colDataStartOffset = this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset;
					dataUnmarshaller.m_indexOfOASArray = this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray;
					dataUnmarshaller.m_oraArrSegWithColRowInfo = this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo;
					dataUnmarshaller.m_oraArrSegCount = this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfoIndex;
					dataUnmarshaller.m_bFirstNonNullOraArrSegWithColInfoEntry = 0;
					dataUnmarshaller.m_indexOfLastOraArrSegUsed = 0;
					this.m_marshallingEngine.m_oraBufRdr.m_colDataStartOffset = null;
					this.m_marshallingEngine.m_oraBufRdr.m_indexOfOASArray = null;
					this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfo = null;
					this.m_marshallingEngine.m_oraBufRdr.m_oraArrSegWithColRowInfoIndex = 0;
				}
			}
			catch (Exception ex4)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex4, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000DB46C File Offset: 0x000D966C
		private void Process_RPA_Message(ref long queryId, long[] scnFromExecution, SqlStatementType statementType, int arrayBindCount, out long[] rowsAffectedByArrayBind)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			rowsAffectedByArrayBind = null;
			try
			{
				int num = this.m_marshallingEngine.UnmarshalUB2(false);
				int num2 = 0;
				if (scnFromExecution != null)
				{
					int num3 = 32768;
					scnFromExecution[0] = (long)((int)this.m_marshallingEngine.UnmarshalUB4(false));
					scnFromExecution[1] = (long)((int)this.m_marshallingEngine.UnmarshalUB4(false) & ~num3);
					num2 += 2;
				}
				for (int i = num2; i < num; i++)
				{
					this.m_marshallingEngine.UnmarshalUB4(true);
				}
				int num4 = this.m_marshallingEngine.UnmarshalUB2(false);
				if (num4 > 0)
				{
					num4 = this.m_marshallingEngine.UnmarshalNBytes_ScanOnly(num4);
				}
				int num5 = this.m_marshallingEngine.UnmarshalUB2(false);
				TTCKeywordValuePair[] array = new TTCKeywordValuePair[num5];
				for (int j = 0; j < num5; j++)
				{
					array[j] = TTCKeywordValuePair.Unmarshal(this.m_marshallingEngine);
					if (163 == array[j].m_keyword)
					{
						this.m_sessionTimeZone = array[j].m_binaryValue;
						this.m_marshallingEngine.m_connImplReference.m_sessionTimeZone.initialZoneId = 0;
						this.m_bSessionTimeZoneUpdated = true;
					}
				}
				this.m_marshallingEngine.m_connImplReference.UpdateSessionAttributes(array);
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 4)
				{
					int num6 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
					if (num6 > 0)
					{
						byte[] array2 = this.m_marshallingEngine.UnmarshalNBytes(num6);
						bool flag = true;
						int byteCount = num6;
						if (flag)
						{
							byteCount = num6 - 8;
						}
						char[] chars = this.m_marshallingEngine.m_charArrayPooler.Dequeue();
						string text = Conv.GetInstance(871).ConvertBytesToString(array2, 0, byteCount, chars, true);
						this.m_marshallingEngine.m_charArrayPooler.Enqueue(ref chars);
						char[] array3 = new char[1];
						char[] separator = array3;
						text.Split(separator);
						if (flag)
						{
							int num7 = (int)array2[num6 - 1] | (int)array2[num6 - 2] << 8 | (int)array2[num6 - 3] << 16 | (int)array2[num6 - 4] << 24;
							int num8 = (int)array2[num6 - 5] | (int)array2[num6 - 6] << 8 | (int)array2[num6 - 7] << 16 | (int)array2[num6 - 8] << 24;
							queryId = (((long)num8 & (long)((ulong)-1)) | (long)num7 << 32);
						}
					}
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7 && SqlStatementType.DML == statementType && arrayBindCount > 0)
					{
						int num9 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
						if (num9 > 0)
						{
							rowsAffectedByArrayBind = new long[num9];
							for (int k = 0; k < num9; k++)
							{
								rowsAffectedByArrayBind[k] = this.m_marshallingEngine.UnmarshalSB8();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000DB754 File Offset: 0x000D9954
		private long GetExecuteOptions(bool parse, bool execute, bool fetch, bool bDoDefines, bool bAutoCommit, bool bDisableCompressedFetch, bool bHasReturningClause, int noOfParameters, int arrayBindCount, SqlStatementType stmtType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			long num = 0L;
			if (bDisableCompressedFetch)
			{
				num = 262144L;
			}
			long result;
			try
			{
				if (stmtType != SqlStatementType.SELECT && bAutoCommit)
				{
					num |= 256L;
				}
				if (arrayBindCount > 1 && SqlStatementType.DML == stmtType)
				{
					num |= 524288L;
				}
				if (parse && !execute && !fetch)
				{
					num |= 163841L;
				}
				else if (parse && execute && !fetch)
				{
					num |= 32801L;
				}
				else if (execute && fetch)
				{
					if (parse)
					{
						num |= 1L;
					}
					switch (stmtType)
					{
					case SqlStatementType.SELECT:
						num |= 32864L;
						break;
					case SqlStatementType.DML:
					case SqlStatementType.OTHERS:
						if (bHasReturningClause)
						{
							if (parse)
							{
								num |= 1056L;
							}
							else
							{
								num |= 32L;
							}
						}
						else
						{
							num |= 32800L;
						}
						break;
					case SqlStatementType.PLSQL:
						if (noOfParameters > 0)
						{
							if (parse)
							{
								num |= 1056L;
							}
							else
							{
								num |= 32L;
							}
						}
						else
						{
							num |= 32L;
						}
						break;
					}
				}
				else if (!parse && !execute && fetch)
				{
					num |= 32832L;
				}
				else
				{
					if (parse || !execute || fetch)
					{
						throw new Exception("Invalid Execution Options");
					}
					num |= 32800L;
				}
				if (noOfParameters > 0)
				{
					num |= 8L;
				}
				if (bDoDefines)
				{
					num |= 16L;
				}
				result = (num & (long)((ulong)-1));
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000DB908 File Offset: 0x000D9B08
		private Accessor[] CreateDefineAccessors(SQLMetaData sqlMetaData, int initialLongFetchSize, long initialLOBFetchSize, bool bDefineDone, bool bLOBArrayFetchRequired = false, int noOfRowsToFetch = 0)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			Accessor[] result;
			try
			{
				Accessor[] array = new Accessor[(int)sqlMetaData.m_noOfColumns];
				for (int i = 0; i < array.Length; i++)
				{
					if (sqlMetaData != null && array[i] == null)
					{
						ColumnDescribeInfo colMetaData = sqlMetaData.m_columnDescribeInfo[i];
						array[i] = Accessor.CreateAccessorForDefine(this.m_marshallingEngine, colMetaData, initialLongFetchSize, initialLOBFetchSize, bDefineDone, bLOBArrayFetchRequired, noOfRowsToFetch);
					}
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x000DB9BC File Offset: 0x000D9BBC
		internal static ColumnDescribeInfo[] InitDefines(ColumnDescribeInfo[] columnMetadataArray, long initialLOBFetchSize)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			ColumnDescribeInfo[] result;
			try
			{
				if (columnMetadataArray == null)
				{
					result = null;
				}
				else
				{
					int maxLength = int.MaxValue;
					ColumnDescribeInfo[] array = new ColumnDescribeInfo[columnMetadataArray.Length];
					for (int i = 0; i < columnMetadataArray.Length; i++)
					{
						array[i] = new ColumnDescribeInfo();
						array[i].m_dataType = columnMetadataArray[i].m_dataType;
						if (columnMetadataArray[i].m_dataType == 113 || columnMetadataArray[i].m_dataType == 112)
						{
							maxLength = 0;
							array[i].m_contFlag |= 33554432;
							if (initialLOBFetchSize == -1L)
							{
								maxLength = int.MaxValue;
								array[i].m_maxLengthOfChars = 0;
								if (columnMetadataArray[i].m_dataType == 112)
								{
									array[i].m_dataType = 1;
								}
								else
								{
									array[i].m_dataType = 23;
								}
							}
							else
							{
								array[i].m_maxLengthOfChars = (int)initialLOBFetchSize;
							}
						}
						array[i].m_flag = 3;
						array[i].m_characterSetForm = columnMetadataArray[i].m_characterSetForm;
						array[i].m_characterSetId = columnMetadataArray[i].m_characterSetId;
						array[i].m_maxLength = maxLength;
					}
					result = array;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000DBB14 File Offset: 0x000D9D14
		internal static long CalculateInternalILFS(long lobPrefetchSize, bool bForFill, bool bReturnPSTypes)
		{
			int num = 32768;
			long result = lobPrefetchSize;
			if (ConfigBaseClass.m_bLegacyNegativeOneILFSBehavior)
			{
				if (-1L == lobPrefetchSize)
				{
					result = (long)num;
				}
				else if (0L == lobPrefetchSize)
				{
					if (bForFill && !bReturnPSTypes)
					{
						result = (long)num;
					}
				}
				else if (!bReturnPSTypes)
				{
					result = ((lobPrefetchSize > (long)num) ? lobPrefetchSize : ((long)num));
				}
			}
			else if (bForFill)
			{
				if (bReturnPSTypes)
				{
					result = 0L;
				}
				else if (lobPrefetchSize <= 0L)
				{
					result = (long)num;
				}
				else
				{
					result = ((lobPrefetchSize > (long)num) ? lobPrefetchSize : ((long)num));
				}
			}
			else if (lobPrefetchSize < 0L)
			{
				result = (long)num;
			}
			return result;
		}

		// Token: 0x04001850 RID: 6224
		private const int UOPF_PRS = 1;

		// Token: 0x04001851 RID: 6225
		private const int UOPF_BND = 8;

		// Token: 0x04001852 RID: 6226
		private const int UOPF_EXE = 32;

		// Token: 0x04001853 RID: 6227
		private const int UOPF_FEX = 512;

		// Token: 0x04001854 RID: 6228
		private const int UOPF_FCH = 64;

		// Token: 0x04001855 RID: 6229
		private const int UOPF_CAN = 128;

		// Token: 0x04001856 RID: 6230
		private const int UOPF_COM = 256;

		// Token: 0x04001857 RID: 6231
		private const int UOPF_DSY = 8192;

		// Token: 0x04001858 RID: 6232
		private const int UOPF_SIO = 1024;

		// Token: 0x04001859 RID: 6233
		private const int UOPF_NPL = 32768;

		// Token: 0x0400185A RID: 6234
		private const int UOPF_DFN = 16;

		// Token: 0x0400185B RID: 6235
		private const int UOPF_NCF = 262144;

		// Token: 0x0400185C RID: 6236
		private const int UOPF_BER = 524288;

		// Token: 0x0400185D RID: 6237
		private const int UOPF_SCN = 2097152;

		// Token: 0x0400185E RID: 6238
		private const int KPUCXDSY = 131072;

		// Token: 0x0400185F RID: 6239
		internal const int AL8KW_CURRENCY = 0;

		// Token: 0x04001860 RID: 6240
		internal const int AL8KW_ISOCURR = 1;

		// Token: 0x04001861 RID: 6241
		internal const int AL8KW_NUMERICS = 2;

		// Token: 0x04001862 RID: 6242
		internal const int AL8KW_DATEFM = 7;

		// Token: 0x04001863 RID: 6243
		internal const int AL8KW_DATELANG = 8;

		// Token: 0x04001864 RID: 6244
		internal const int AL8KW_TERRITORY = 9;

		// Token: 0x04001865 RID: 6245
		internal const int AL8KW_CHARSET = 10;

		// Token: 0x04001866 RID: 6246
		internal const int AL8KW_SORT = 11;

		// Token: 0x04001867 RID: 6247
		internal const int AL8KW_CALENDAR = 12;

		// Token: 0x04001868 RID: 6248
		internal const int AL8KW_LANGUAGE = 16;

		// Token: 0x04001869 RID: 6249
		internal const int AL8KW_NLSCOMP = 50;

		// Token: 0x0400186A RID: 6250
		internal const int AL8KW_UNIONCUR = 52;

		// Token: 0x0400186B RID: 6251
		internal const int AL8KW_TIMEFM = 57;

		// Token: 0x0400186C RID: 6252
		internal const int AL8KW_STMPFM = 58;

		// Token: 0x0400186D RID: 6253
		internal const int AL8KW_TTZNFM = 59;

		// Token: 0x0400186E RID: 6254
		internal const int AL8KW_STZNFM = 60;

		// Token: 0x0400186F RID: 6255
		internal const int AL8KW_NLSLENSEM = 61;

		// Token: 0x04001870 RID: 6256
		internal const int AL8KW_NCHAREXCP = 62;

		// Token: 0x04001871 RID: 6257
		internal const int AL8KW_NCHARIMP = 63;

		// Token: 0x04001872 RID: 6258
		internal const int AL8KW_MAXLANG = 63;

		// Token: 0x04001873 RID: 6259
		internal const int AL8KW_TIMEZONE = 163;

		// Token: 0x04001874 RID: 6260
		internal const int AL8KW_ERR_OVLAP = 164;

		// Token: 0x04001875 RID: 6261
		internal const int AL8KW_SESSION_ID = 165;

		// Token: 0x04001876 RID: 6262
		internal const int AL8KW_SERIAL_NUM = 166;

		// Token: 0x04001877 RID: 6263
		internal const int AL8KW_TAG_FOUND = 167;

		// Token: 0x04001878 RID: 6264
		internal const int AL8KW_SCHEMA_NAME = 168;

		// Token: 0x04001879 RID: 6265
		internal const int AL8KW_SCHEMA_ID = 169;

		// Token: 0x0400187A RID: 6266
		internal const int AL8KW_ENABLED_ROLES = 170;

		// Token: 0x0400187B RID: 6267
		internal const int AL8KW_AUX_SESSSTATE = 171;

		// Token: 0x0400187C RID: 6268
		internal const int AL8KW_EDITION = 172;

		// Token: 0x0400187D RID: 6269
		internal const int AL8KW_SQL_TXLP = 173;

		// Token: 0x0400187E RID: 6270
		internal const int AL8KW_FSQL_SNTX = 174;

		// Token: 0x0400187F RID: 6271
		internal const int AL8KW_OPENCURSORS = 175;

		// Token: 0x04001880 RID: 6272
		internal const int AL8KW_PDBUID = 176;

		// Token: 0x04001881 RID: 6273
		internal const int AL8KW_DBID = 177;

		// Token: 0x04001882 RID: 6274
		internal const int AL8KW_GUDBID = 178;

		// Token: 0x04001883 RID: 6275
		internal const int AL8KW_DBNAME = 179;

		// Token: 0x04001884 RID: 6276
		internal const int AL8KW_PDB_SDATE = 180;

		// Token: 0x04001885 RID: 6277
		internal const int AL8KW_PDB_STIME = 181;

		// Token: 0x04001886 RID: 6278
		internal const int AL8KW_MAX_IDEN_LENGTH = 182;

		// Token: 0x04001887 RID: 6279
		internal const int AL8KW_SERVICE_NAME = 183;

		// Token: 0x04001888 RID: 6280
		internal const int AL8KW_MODULE = 184;

		// Token: 0x04001889 RID: 6281
		internal const int AL8KW_ACTION = 185;

		// Token: 0x0400188A RID: 6282
		internal const int AL8KW_CLIENT_INFO = 186;

		// Token: 0x0400188B RID: 6283
		internal const int AL8KW_ROW_ARCHIVAL = 187;

		// Token: 0x0400188C RID: 6284
		internal const int AL8KW_FAILOVER_TYPE = 188;

		// Token: 0x0400188D RID: 6285
		internal const int AL8KW_FAILOVER_DELAY = 189;

		// Token: 0x0400188E RID: 6286
		internal const int AL8KW_FAILOVER_RETRIES = 190;

		// Token: 0x0400188F RID: 6287
		internal const int AL8KW_FAILOVER_METHOD = 191;

		// Token: 0x04001890 RID: 6288
		internal const int AL8KW_COMMIT_OUTCOME = 192;

		// Token: 0x04001891 RID: 6289
		internal const int AL8KW_SERVICE_FLAGS = 193;

		// Token: 0x04001892 RID: 6290
		internal const int AL8KW_SESSSTATE_CONS = 194;

		// Token: 0x04001893 RID: 6291
		internal const int AL8KW_REPLAY_TIMEOUT = 195;

		// Token: 0x04001894 RID: 6292
		internal const int AL8KW_FAILOVER_RESTORE = 196;

		// Token: 0x04001895 RID: 6293
		internal const int AL8KW_CONTAINER_NAME = 197;

		// Token: 0x04001896 RID: 6294
		internal const int AL8KW_CLIENT_ID = 198;

		// Token: 0x04001897 RID: 6295
		internal const int LONGLIMITFOR12G = 32767;

		// Token: 0x04001898 RID: 6296
		internal const int LONGLIMITFORDBLESSTHAN12G = 4000;

		// Token: 0x04001899 RID: 6297
		private const long AL8EX_GET_PIDMLRC = 16384L;

		// Token: 0x0400189A RID: 6298
		private const long AL8EX_IMPL_RESULTS_CLIENT = 32768L;

		// Token: 0x0400189B RID: 6299
		private long[] m_al8i4 = new long[13];

		// Token: 0x0400189C RID: 6300
		private TTCRowHeader m_rowHeader;

		// Token: 0x0400189D RID: 6301
		private TTCRowData m_rowData;

		// Token: 0x0400189E RID: 6302
		private TTCReExecuteSql m_reExecuteSql;

		// Token: 0x0400189F RID: 6303
		internal byte[] m_sessionTimeZone;

		// Token: 0x040018A0 RID: 6304
		internal bool m_bSessionTimeZoneUpdated;

		// Token: 0x02000229 RID: 553
		internal struct MarshalBindParameterValueHelper
		{
			// Token: 0x06001471 RID: 5233 RVA: 0x000DBB88 File Offset: 0x000D9D88
			internal void EvaluateBindDirections()
			{
				bool flag = false;
				bool flag2 = false;
				if (this.m_bindDirections != null)
				{
					for (int i = 0; i < this.m_bindDirections.Length; i++)
					{
						if (this.m_bindDirections[i] == BindDirection.InputOutput)
						{
							flag = true;
							flag2 = true;
						}
						else if (this.m_bindDirections[i] == BindDirection.Input)
						{
							flag = true;
						}
						else if (this.m_bindDirections[i] == BindDirection.Output)
						{
							flag2 = true;
						}
						if (flag && flag2)
						{
							this.m_bAllInBinds = false;
							this.m_bAllOutBinds = false;
							return;
						}
					}
					if (flag && !flag2)
					{
						this.m_bAllInBinds = true;
						this.m_bAllOutBinds = false;
						return;
					}
					if (flag2 && !flag)
					{
						this.m_bAllOutBinds = true;
						this.m_bAllInBinds = false;
					}
				}
			}

			// Token: 0x06001472 RID: 5234 RVA: 0x000DBC28 File Offset: 0x000D9E28
			internal bool Equals(TTCExecuteSql.MarshalBindParameterValueHelper obj)
			{
				return this.m_paramValueArray == obj.m_paramValueArray || this.m_paramCollInfoArray == obj.m_paramCollInfoArray || this.m_bindDirections == obj.m_bindDirections;
			}

			// Token: 0x06001473 RID: 5235 RVA: 0x000DBC5C File Offset: 0x000D9E5C
			internal void MarshalBindParameters(MarshallingEngine mEngine, int rowIndexToSendFrom, int numberOfSubsequentRowsToSend)
			{
				if (this.m_IsInArrayBindingMode)
				{
					this.MarshalArrayBindValues(mEngine, rowIndexToSendFrom, numberOfSubsequentRowsToSend);
					return;
				}
				this.MarshalBindValues(mEngine);
			}

			// Token: 0x06001474 RID: 5236 RVA: 0x000DBC78 File Offset: 0x000D9E78
			internal void ResetBindDirections(int origParamCount)
			{
				if (this.m_bindDirections == null)
				{
					this.m_bindDirections = new BindDirection[origParamCount];
					for (int i = 0; i < this.m_bindDirections.Length; i++)
					{
						this.m_bindDirections[i] = BindDirection.Input;
					}
				}
			}

			// Token: 0x06001475 RID: 5237 RVA: 0x000DBCB8 File Offset: 0x000D9EB8
			internal void MarshalBindValues(MarshallingEngine mEngine)
			{
				if (this.m_InTTISLGMode)
				{
					mEngine.MarshalUB1(7);
					if (this.m_indexOfLongParamsWithLargeData != null && this.m_bindIndexOfLongParamsWithLargeData != null)
					{
						int num = 0;
						for (int i = 0; i < this.m_indexOfLongParamsWithLargeData.Count; i++)
						{
							if ((this.m_paramCollInfoArray[this.m_indexOfLongParamsWithLargeData[i]].m_flag | 128) != 128)
							{
								if (this.m_bindDirections == null || this.m_bindDirections[this.m_bindIndexOfLongParamsWithLargeData[num]] != BindDirection.Output)
								{
									TTCExecuteSql.MarshalParameterValue(mEngine, this.m_paramValueArray[this.m_indexOfLongParamsWithLargeData[i]], (OraType)this.m_paramCollInfoArray[this.m_indexOfLongParamsWithLargeData[i]].m_dataType);
								}
								num++;
							}
						}
						return;
					}
				}
				else
				{
					this.MarshalSingleRow(mEngine);
				}
			}

			// Token: 0x06001476 RID: 5238 RVA: 0x000DBD8C File Offset: 0x000D9F8C
			private void MarshalSingleRow(MarshallingEngine mEngine)
			{
				mEngine.MarshalUB1(7);
				if (this.m_bindIndexOfLongParamsWithLargeData != null)
				{
					this.m_bindIndexOfLongParamsWithLargeData.Clear();
				}
				else
				{
					this.m_bindIndexOfLongParamsWithLargeData = new List<int>();
				}
				int num = 0;
				for (int i = 0; i < this.m_paramValueArray.Length; i++)
				{
					if ((this.m_paramCollInfoArray[i].m_flag | 128) != 128)
					{
						if (this.m_bindDirections == null || this.m_bindDirections[num] != BindDirection.Output)
						{
							int maxNoOfArrayElements = this.m_paramCollInfoArray[i].m_maxNoOfArrayElements;
							if (maxNoOfArrayElements > 0)
							{
								TTCExecuteSql.MarshalAssociativeArrayParameterValue(mEngine, this.m_paramValueArray[i], (OraType)this.m_paramCollInfoArray[i].m_dataType);
							}
							else if (!this.m_indexOfLongParamsWithLargeData.Contains(i))
							{
								TTCExecuteSql.MarshalParameterValue(mEngine, this.m_paramValueArray[i], (OraType)this.m_paramCollInfoArray[i].m_dataType);
							}
							else
							{
								this.m_bindIndexOfLongParamsWithLargeData.Add(num);
							}
						}
						num++;
					}
				}
				if (this.m_indexOfLongParamsWithLargeData != null && this.m_bindIndexOfLongParamsWithLargeData != null)
				{
					num = 0;
					for (int j = 0; j < this.m_indexOfLongParamsWithLargeData.Count; j++)
					{
						if ((this.m_paramCollInfoArray[this.m_indexOfLongParamsWithLargeData[j]].m_flag | 128) != 128)
						{
							if (this.m_bindDirections == null || this.m_bindDirections[this.m_bindIndexOfLongParamsWithLargeData[num]] != BindDirection.Output)
							{
								TTCExecuteSql.MarshalParameterValue(mEngine, this.m_paramValueArray[this.m_indexOfLongParamsWithLargeData[j]], (OraType)this.m_paramCollInfoArray[this.m_indexOfLongParamsWithLargeData[j]].m_dataType);
							}
							num++;
						}
					}
				}
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x000DBF20 File Offset: 0x000DA120
			internal void MarshalArrayBindValues(MarshallingEngine mEngine, int rowIndexToSendFrom, int numberOfSubsequentRowsToSend)
			{
				if (this.m_offsetRowIndicesForwardByOne)
				{
					rowIndexToSendFrom++;
				}
				int num = numberOfSubsequentRowsToSend + rowIndexToSendFrom - 1;
				if (rowIndexToSendFrom > num)
				{
					return;
				}
				if (this.m_InTTISLGMode)
				{
					mEngine.MarshalUB1(7);
					if (this.m_indexOfLongParamsWithLargeData != null && this.m_bindIndexOfLongParamsWithLargeData != null)
					{
						int num2 = 0;
						for (int i = 0; i < this.m_indexOfLongParamsWithLargeData.Count; i++)
						{
							if ((this.m_paramCollInfoArray[this.m_indexOfLongParamsWithLargeData[i]].m_flag | 128) != 128)
							{
								if (this.m_bindDirections == null || this.m_bindDirections[this.m_bindIndexOfLongParamsWithLargeData[num2]] != BindDirection.Output)
								{
									this.MarshalParam(mEngine, rowIndexToSendFrom, this.m_indexOfLongParamsWithLargeData[i], this.m_bindIndexOfLongParamsWithLargeData[num2], true);
								}
								num2++;
							}
						}
					}
					rowIndexToSendFrom++;
				}
				if (rowIndexToSendFrom <= num)
				{
					this.MarshalRows(mEngine, rowIndexToSendFrom, num);
				}
			}

			// Token: 0x06001478 RID: 5240 RVA: 0x000DC004 File Offset: 0x000DA204
			private void MarshalRows(MarshallingEngine mEngine, int rowIndexToSendFrom, int rowIndexToSendUntil)
			{
				for (int i = rowIndexToSendFrom; i <= rowIndexToSendUntil; i++)
				{
					mEngine.MarshalUB1(7);
					if (this.m_bindIndexOfLongParamsWithLargeData != null)
					{
						this.m_bindIndexOfLongParamsWithLargeData.Clear();
					}
					else
					{
						this.m_bindIndexOfLongParamsWithLargeData = new List<int>();
					}
					int num = 0;
					for (int j = 0; j < this.m_paramValueArray.Length; j++)
					{
						if ((this.m_paramCollInfoArray[j].m_flag | 128) != 128)
						{
							if (this.m_bindDirections == null || this.m_bindDirections[num] != BindDirection.Output)
							{
								if (!this.m_indexOfLongParamsWithLargeData.Contains(j))
								{
									this.MarshalParam(mEngine, i, j, num, rowIndexToSendFrom == i);
								}
								else
								{
									this.m_bindIndexOfLongParamsWithLargeData.Add(num);
								}
							}
							num++;
						}
					}
					if (this.m_indexOfLongParamsWithLargeData != null && this.m_bindIndexOfLongParamsWithLargeData != null)
					{
						num = 0;
						for (int k = 0; k < this.m_indexOfLongParamsWithLargeData.Count; k++)
						{
							if ((this.m_paramCollInfoArray[this.m_indexOfLongParamsWithLargeData[k]].m_flag | 128) != 128)
							{
								if (this.m_bindDirections == null || this.m_bindDirections[this.m_bindIndexOfLongParamsWithLargeData[num]] != BindDirection.Output)
								{
									this.MarshalParam(mEngine, i, this.m_indexOfLongParamsWithLargeData[k], this.m_bindIndexOfLongParamsWithLargeData[num], rowIndexToSendFrom == i);
								}
								num++;
							}
						}
					}
				}
			}

			// Token: 0x06001479 RID: 5241 RVA: 0x000DC158 File Offset: 0x000DA358
			private void MarshalParam(MarshallingEngine mEngine, int rowIndex, int paramIndex, int bindIndex, bool isFirstRow)
			{
				OraType dataType = (OraType)this.m_paramCollInfoArray[paramIndex].m_dataType;
				if (this.m_paramValueArray[paramIndex] != null && (this.m_bindDirections == null || this.m_bindDirections[bindIndex] != BindDirection.Output))
				{
					TTCExecuteSql.MarshalParameterValue(mEngine, ((byte[][])this.m_paramValueArray[paramIndex])[rowIndex], dataType);
					return;
				}
				if (SqlStatementType.PLSQL != this.m_sqlStmtType || isFirstRow || this.m_bindDirections[bindIndex] != BindDirection.Output)
				{
					TTCExecuteSql.MarshalParameterValue(mEngine, null, dataType);
				}
			}

			// Token: 0x040018A1 RID: 6305
			internal object[] m_paramValueArray;

			// Token: 0x040018A2 RID: 6306
			internal ColumnDescribeInfo[] m_paramCollInfoArray;

			// Token: 0x040018A3 RID: 6307
			internal BindDirection[] m_bindDirections;

			// Token: 0x040018A4 RID: 6308
			internal bool m_bAllInBinds;

			// Token: 0x040018A5 RID: 6309
			internal bool m_bAllOutBinds;

			// Token: 0x040018A6 RID: 6310
			internal bool m_IsInArrayBindingMode;

			// Token: 0x040018A7 RID: 6311
			internal bool m_InTTISLGMode;

			// Token: 0x040018A8 RID: 6312
			internal List<int> m_indexOfLongParamsWithLargeData;

			// Token: 0x040018A9 RID: 6313
			internal List<int> m_bindIndexOfLongParamsWithLargeData;

			// Token: 0x040018AA RID: 6314
			internal SqlStatementType m_sqlStmtType;

			// Token: 0x040018AB RID: 6315
			internal static readonly TTCExecuteSql.MarshalBindParameterValueHelper Null = default(TTCExecuteSql.MarshalBindParameterValueHelper);

			// Token: 0x040018AC RID: 6316
			internal bool m_offsetRowIndicesForwardByOne;
		}
	}
}
