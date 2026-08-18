using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;
using OracleInternal.SelfTuning;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001AF RID: 431
	internal class OracleCommandImpl
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x000A96B4 File Offset: 0x000A78B4
		internal OracleCommandImpl()
		{
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000A96F0 File Offset: 0x000A78F0
		internal void Init()
		{
			this.m_sqlStatementType = SqlStatementType.SELECT;
			this.m_commandTextByteStream = null;
			this.m_sqlMetaData = null;
			this.m_rowsToFetch = 25;
			this.m_bindAccessors = null;
			this.m_bindDirectionsFromServer = null;
			this.m_bHasReturningClause = false;
			this.m_bServerExecutionComplete = false;
			this.m_numReturningParams = 0;
			this.m_fetchSize = 0L;
			this.m_arrayBindCount = 0;
			this.m_bBindByName = ConfigBaseClass.m_BindByName;
			OracleCommandImpl.m_clientRegistrationId = 0;
			this.m_sessionTimeZone = OracleIntervalDS.Null;
			this.m_addToStatementCache = true;
			this.m_addRowid = false;
			this.m_addRowidDoneImplicitly = false;
			this.m_foundExplicitRowidInSql = false;
			this.m_executionId = 0L;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000A978C File Offset: 0x000A798C
		internal void Copy(OracleCommandImpl orclCmdImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_fetchSize = orclCmdImpl.m_fetchSize;
				this.m_arrayBindCount = orclCmdImpl.m_arrayBindCount;
				this.m_bBindByName = orclCmdImpl.m_bBindByName;
				this.m_addToStatementCache = orclCmdImpl.m_addToStatementCache;
				this.m_addRowid = orclCmdImpl.m_addRowid;
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

		// Token: 0x0600103F RID: 4159 RVA: 0x000A9838 File Offset: 0x000A7A38
		private bool CanUseOptimizeExecute(SqlStatementType stmtType, long numRowsToFetch, long numIterations, bool bsnapshot, bool bChangeNtfReq)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool flag = false;
			bool result;
			try
			{
				if (stmtType == SqlStatementType.SELECT)
				{
					numIterations = numRowsToFetch;
				}
				else if (stmtType == SqlStatementType.DML)
				{
					numIterations = (long)this.m_arrayBindCount;
				}
				if (((stmtType == SqlStatementType.SELECT && numIterations < 32768L) || (stmtType == SqlStatementType.DML && numIterations < 32768L) || (stmtType == SqlStatementType.PLSQL && numIterations == 1L)) && !bChangeNtfReq && !bsnapshot)
				{
					flag = true;
				}
				result = flag;
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

		// Token: 0x06001040 RID: 4160 RVA: 0x000A98EC File Offset: 0x000A7AEC
		internal void Cancel(OracleConnectionImpl connImpl, long cancelExecutionId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_cancelExecutionEvent.Set();
			try
			{
				if (!this.m_bServerExecutionComplete)
				{
					lock (this.m_lockCancel)
					{
						if (!this.m_bServerExecutionComplete && cancelExecutionId == this.m_executionId && this.m_continueCancel.WaitOne(10000))
						{
							connImpl.m_marshallingEngine.m_oracleCommunication.Break();
						}
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

		// Token: 0x06001041 RID: 4161 RVA: 0x000A99CC File Offset: 0x000A7BCC
		internal static void ValidateStatementCacheSize(OracleConnectionImpl connectionImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (connectionImpl.m_pm != null && connectionImpl.m_pm.MaxAllowedValue != 2147483647 && connectionImpl.m_statementCache.m_maxCacheSize > connectionImpl.m_pm.MaxAllowedValue)
				{
					int num = (connectionImpl.m_pm.m_recommendedSCS <= connectionImpl.m_pm.MaxAllowedValue) ? connectionImpl.m_pm.m_recommendedSCS : connectionImpl.m_pm.MaxAllowedValue;
					if (connectionImpl.m_statementCache.Count > num)
					{
						connectionImpl.PurgeStatementCache(num);
					}
					connectionImpl.m_statementCache.m_maxCacheSize = num;
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

		// Token: 0x06001042 RID: 4162 RVA: 0x000A9AC0 File Offset: 0x000A7CC0
		internal int VerifyExecution(OracleConnectionImpl connectionImpl, out int cursorId, bool bThrowArrayBindRelatedErrors, ref OracleException exceptionForArrayBindDML, out bool hasMoreRowsInDB, bool bFirstIterationDone = false)
		{
			return connectionImpl.VerifyExecution(out cursorId, bThrowArrayBindRelatedErrors, this.m_sqlStatementType, this.m_arrayBindCount, ref exceptionForArrayBindDML, out hasMoreRowsInDB, bFirstIterationDone);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000A9ADC File Offset: 0x000A7CDC
		internal static void TrimCommentsFromSQL(ref string cmdText)
		{
			if (string.IsNullOrWhiteSpace(cmdText))
			{
				return;
			}
			string text = cmdText.TrimStart(new char[0]);
			string text2 = string.Empty;
			char[] trimChars = new char[]
			{
				' ',
				'(',
				'\r',
				'\t',
				'\n'
			};
			try
			{
				OracleCommandImpl.TrimStringsFromSQL(ref text);
				int num = text.IndexOf("--");
				int num2 = text.IndexOf("/*");
				if (num == -1 && num2 == -1)
				{
					text2 = text;
				}
				else
				{
					while (num != -1 || num2 != -1)
					{
						if (num > -1 && (num2 <= -1 || num2 > num))
						{
							int num3 = text.IndexOf('\n', num + 2);
							if (num3 == -1)
							{
								num3 = text.Length - 1;
							}
							text2 += text.Substring(0, num);
							text = text.Substring(num3 + 1);
						}
						else if (num2 > -1)
						{
							int num4 = text.IndexOf("*/", 2);
							if (num4 == -1)
							{
								throw new FormatException();
							}
							text2 += text.Substring(0, num2);
							text = text.Substring(num4 + 2);
						}
						if (text != null)
						{
							text = text.TrimStart(trimChars);
						}
						num = text.IndexOf("--");
						num2 = text.IndexOf("/*");
					}
					if (text.Length > 0)
					{
						text2 += text;
					}
				}
			}
			catch
			{
				text2 = cmdText;
			}
			text2 = text2.TrimStart(trimChars);
			cmdText = text2;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000A9C48 File Offset: 0x000A7E48
		internal static void TrimStringsFromSQL(ref string cmdText)
		{
			if (cmdText.IndexOf('\'') == -1)
			{
				return;
			}
			int length = cmdText.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			bool flag = false;
			bool flag2 = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int i = 0;
				while (i < length)
				{
					char c = cmdText[i];
					if (c == '\'')
					{
						if (flag2)
						{
							goto IL_9A;
						}
						if (!flag)
						{
							flag = true;
							stringBuilder.Append('\'');
						}
						else if (i + 1 != length && cmdText[i + 1] == '\'')
						{
							i++;
						}
						else
						{
							flag = false;
							stringBuilder.Append('\'');
						}
					}
					else
					{
						if (c == '"' && !flag)
						{
							flag2 = !flag2;
							goto IL_9A;
						}
						goto IL_9A;
					}
					IL_A6:
					i++;
					continue;
					IL_9A:
					if (!flag)
					{
						stringBuilder.Append(c);
						goto IL_A6;
					}
					goto IL_A6;
				}
				if (!flag && !flag2)
				{
					cmdText = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000A9D64 File Offset: 0x000A7F64
		internal int ExecuteNonQuery(string commandText, OracleParameterCollection paramColl, CommandType commandType, OracleConnectionImpl connectionImpl, int longFetchSize, long clientInitialLOBFS, OracleDependencyImpl orclDependencyImpl, out long[] scnFromExecution, out OracleParameterCollection bindByPositionParamColl, ref bool bBindParamPresent, out OracleException exceptionForArrayBindDML, OracleConnection connection, ref OracleLogicalTransaction oracleLogicalTransaction, bool isFromEF = false)
		{
			bool flag = false;
			bool flag2 = false;
			Accessor[] array = null;
			ColumnDescribeInfo[] cachedParamMetadata = null;
			CachedStatement cachedStatement = null;
			SQLMetaData sqlmetaData = null;
			SQLInfo sqlinfo = null;
			int num = 0;
			ArrayList arrayList = null;
			bindByPositionParamColl = null;
			bool flag3 = false;
			bool? flag4 = new bool?(false);
			string text = commandText;
			this.m_sqlMetaData = null;
			scnFromExecution = null;
			IEnumerable<OracleLpStatement> enumerable = null;
			bool flag5 = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (this.m_implicitRSList != null && this.m_implicitRSList.Count > 0)
			{
				this.CloseImplicitRefCursors(connectionImpl);
				this.m_implicitRSList.Clear();
			}
			int result;
			try
			{
				BindDirection[] bindDirections = null;
				if (connectionImpl.m_statementCache != null && (!connectionImpl.m_marshallingEngine.m_bDRCPConnection || connectionImpl.m_marshallingEngine.m_bDRCPSessionAttached))
				{
					OracleCommandImpl.ValidateStatementCacheSize(connectionImpl);
					connectionImpl.m_statementCache.Get(commandText, out cachedStatement, out sqlmetaData, out sqlinfo);
					if (cachedStatement != null)
					{
						num = cachedStatement.m_cursorId;
						this.m_bindAccessors = cachedStatement.m_bindAccessors;
						cachedParamMetadata = cachedStatement.m_bindParamMetadata;
						scnFromExecution = cachedStatement.m_scnFromExecution;
						arrayList = cachedStatement.m_placeHolderCollection;
						bBindParamPresent = cachedStatement.m_bBindParamPresent;
						bindDirections = cachedStatement.m_bindDirections;
						this.m_bindDirectionsFromServer = cachedStatement.m_bindDirections;
						enumerable = ((cachedStatement.statementdata != null) ? cachedStatement.statementdata.parsedStmt : null);
						flag = true;
					}
				}
				if (sqlinfo != null)
				{
					this.m_commandTextByteStream = sqlinfo.m_SQLcommandTextByteStream;
					this.m_sqlStatementType = sqlinfo.m_SQLStatementType;
				}
				else
				{
					if (commandType == CommandType.StoredProcedure)
					{
						this.m_sqlStatementType = SqlStatementType.PLSQL;
					}
					else if (commandType == CommandType.TableDirect)
					{
						this.m_sqlStatementType = SqlStatementType.SELECT;
					}
					else
					{
						OracleCommandImpl.TrimCommentsFromSQL(ref text);
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementTypeAdrianParser(connection, text, ref enumerable, ref flag4, ref flag5);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementType(text, ref flag4);
						}
					}
					string text2 = commandText;
					if (!connectionImpl.m_isDb11gR1OrHigher && (this.m_sqlStatementType == SqlStatementType.PLSQL || text.StartsWith("create", StringComparison.InvariantCultureIgnoreCase)))
					{
						text2 = commandText.Replace("\r\n", "\n");
					}
					this.m_commandTextByteStream = connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text2, 0, text2.Length, true);
				}
				flag2 = ((this.m_sqlMetaData = sqlmetaData) != null);
				if (!flag || (bBindParamPresent && this.m_bBindByName && arrayList == null))
				{
					if (this.m_bBindByName)
					{
						arrayList = new ArrayList();
					}
					if (commandType == CommandType.Text)
					{
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
						{
							this.GetBindInfoUsingAdrianParser(connection, commandText, ref bBindParamPresent, ref arrayList, ref enumerable, ref flag5);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
						{
							this.ParseCommandText(text, ref bBindParamPresent, ref arrayList);
						}
					}
					else if (commandType == CommandType.TableDirect)
					{
						bBindParamPresent = false;
					}
				}
				if (this.m_bBindByName && paramColl != null && paramColl.Count > 0)
				{
					if (commandType == CommandType.StoredProcedure)
					{
						bindByPositionParamColl = this.ReorderBindByNameBasedParameterCollectionForStoredProcedure(paramColl);
					}
					else
					{
						bindByPositionParamColl = this.GetBindByPositionBasedParameterCollection(paramColl, arrayList, false);
					}
				}
				else
				{
					bindByPositionParamColl = paramColl;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
					{
						commandText
					});
				}
				bool flag6 = false;
				bool flag7 = false;
				TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
				if (cachedStatement != null)
				{
					@null.m_bAllInBinds = cachedStatement.m_bAllInBinds;
					@null.m_bAllOutBinds = cachedStatement.m_bAllOutBinds;
				}
				if (bBindParamPresent && bindByPositionParamColl != null)
				{
					@null.m_bindDirections = bindDirections;
					@null.m_sqlStmtType = this.m_sqlStatementType;
					if (this.m_bindAccessors == null || this.m_bindAccessors.Length != paramColl.Count)
					{
						this.m_bindAccessors = new Accessor[paramColl.Count];
					}
					this.ProcessParameters(bindByPositionParamColl, connectionImpl, cachedParamMetadata, ref flag7, isFromEF && this.m_sqlStatementType == SqlStatementType.SELECT, ref @null);
				}
				else
				{
					this.m_bindAccessors = null;
					this.m_bindDirectionsFromServer = null;
					this.m_arrayBindCount = 0;
				}
				if (flag && !flag7 && this.CanUseOptimizeExecute(this.m_sqlStatementType, (long)this.m_rowsToFetch, (long)((this.m_arrayBindCount == 0) ? 1 : this.m_arrayBindCount), false, orclDependencyImpl != null))
				{
					flag6 = true;
				}
				int num2 = 0;
				int num3 = 0;
				long num4 = 0L;
				DataUnmarshaller dataUnmarshaller = null;
				if (scnFromExecution != null)
				{
					scnFromExecution[0] = 0L;
					scnFromExecution[1] = 0L;
				}
				else
				{
					scnFromExecution = new long[2];
				}
				if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
				{
					this.CheckForReturningClauseAdrianParser(connection, this.m_sqlStatementType, sqlinfo, commandType, ref enumerable, commandText, ref flag5);
				}
				if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
				{
					this.CheckForReturningClause(text, this.m_sqlStatementType, sqlinfo, commandType, bindByPositionParamColl, arrayList, bBindParamPresent, commandText);
				}
				bool bDisableCompressedFetch = this.m_bHasReturningClause || SqlStatementType.PLSQL == this.m_sqlStatementType || (cachedStatement != null && cachedStatement.m_bDisableCompressedFetch);
				int num5 = -1;
				try
				{
					num5 = connectionImpl.WaitForConnectionForExecution(this.m_cancelExecutionEvent);
					this.m_continueCancel.Set();
					connectionImpl.AddAllPiggyBackRequests();
					TTCExecuteSql executeSqlObject = connectionImpl.ExecuteSqlObject;
					if (flag6)
					{
						if (connection != null)
						{
							oracleLogicalTransaction = connection.OracleLogicalTransaction;
						}
						executeSqlObject.SendReExecuteRequest(connectionImpl, num, 0L, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, this.m_arrayBindCount, ref @null);
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num2, ref num4, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
						exceptionForArrayBindDML = null;
						bool flag8 = false;
						num3 = this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag8, false);
					}
					else
					{
						bool parse = !flag;
						bool fetch = flag || this.m_bHasReturningClause || this.m_sqlStatementType == SqlStatementType.PLSQL;
						bool flag9 = this.m_arrayBindCount > 1 && SqlStatementType.PLSQL == this.m_sqlStatementType;
						bool flag10 = false;
						bool bThrowArrayBindRelatedErrors = true;
						if (flag9 && (!flag || (flag && !cachedStatement.m_bAllInBinds && !cachedStatement.m_bAllOutBinds)))
						{
							flag10 = true;
						}
						if (connection != null)
						{
							oracleLogicalTransaction = connection.OracleLogicalTransaction;
						}
						executeSqlObject.SendExecuteRequest(connectionImpl, flag ? null : this.m_commandTextByteStream, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), null, 0L, parse, true, fetch, false, !flag10 && connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, flag10 ? 1 : this.m_arrayBindCount, null, ref @null, 0);
						if (@null.m_bindDirections == null && @null.m_paramValueArray != null)
						{
							@null.ResetBindDirections(paramColl.Count);
							@null.m_bAllInBinds = true;
						}
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num2, ref num4, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
						if (flag10)
						{
							bThrowArrayBindRelatedErrors = false;
						}
						exceptionForArrayBindDML = null;
						bool flag11 = false;
						num3 = this.VerifyExecution(connectionImpl, out num, bThrowArrayBindRelatedErrors, ref exceptionForArrayBindDML, out flag11, false);
						if (flag10)
						{
							if (@null.m_bAllInBinds || @null.m_bAllOutBinds)
							{
								if (connection != null)
								{
									oracleLogicalTransaction = connection.OracleLogicalTransaction;
								}
								executeSqlObject.SendExecuteRequest(connectionImpl, null, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), null, 0L, false, true, true, false, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, this.m_arrayBindCount - 1, null, ref @null, 1);
								executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num2, ref num4, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
								num3 += this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag11, true);
							}
							else
							{
								bool bAutoCommit = false;
								for (int i = 1; i < this.m_arrayBindCount; i++)
								{
									if (i == this.m_arrayBindCount - 1)
									{
										bAutoCommit = connectionImpl.m_autoCommit;
									}
									if (connection != null)
									{
										oracleLogicalTransaction = connection.OracleLogicalTransaction;
									}
									executeSqlObject.SendExecuteRequest(connectionImpl, null, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), null, 0L, false, true, true, false, bAutoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, 1, null, ref @null, i);
									executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num2, ref num4, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
									num3 += this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag11, true);
								}
							}
						}
					}
					lock (this.m_lockCancel)
					{
						this.m_bServerExecutionComplete = true;
						if (this.m_executionId >= 9223372036854775807L)
						{
							this.m_executionId = 0L;
						}
						else
						{
							this.m_executionId += 1L;
						}
					}
					if (connectionImpl.m_oracleCommunication.InBreakResetMode())
					{
						connectionImpl.m_marshallingEngine.ProcessReset();
					}
					if (executeSqlObject.m_bSessionTimeZoneUpdated)
					{
						connectionImpl.m_sessionTimeZone = new OracleIntervalDS(executeSqlObject.m_sessionTimeZone);
						executeSqlObject.m_bSessionTimeZoneUpdated = false;
					}
					this.m_sessionTimeZone = connectionImpl.m_sessionTimeZone;
					if (this.m_sqlMetaData != null)
					{
						this.m_sqlMetaData.pCommandText = commandText;
						if (!flag2)
						{
							sqlmetaData = this.m_sqlMetaData;
						}
					}
					this.m_bindDirectionsFromServer = @null.m_bindDirections;
					if (orclDependencyImpl != null)
					{
						orclDependencyImpl.m_bIsEnabled = orclDependencyImpl.m_bIsRegistered;
						if (orclDependencyImpl.m_bQueryBasedNTFN && !orclDependencyImpl.m_queryIDList.Contains(num4))
						{
							lock (orclDependencyImpl.m_syncList)
							{
								if (!orclDependencyImpl.m_queryIDList.Contains(num4))
								{
									orclDependencyImpl.m_queryIDList.Add(num4);
								}
							}
						}
					}
					if (exceptionForArrayBindDML != null)
					{
						return num3;
					}
					if ((flag4 == null || flag4 == false) && (this.m_sqlStatementType == SqlStatementType.DML || this.m_sqlStatementType == SqlStatementType.SELECT || this.m_sqlStatementType == SqlStatementType.PLSQL) && !flag && connectionImpl.m_statementCache != null && this.m_addToStatementCache)
					{
						cachedStatement = new CachedStatement();
						cachedStatement.sqlInfo = new SQLInfo
						{
							m_SQLcommandTextByteStream = this.m_commandTextByteStream,
							m_SQLhasReturningClause = this.m_bHasReturningClause,
							m_SQLStatementType = this.m_sqlStatementType
						};
						cachedStatement.statementdata = sqlmetaData;
						cachedStatement.m_cursorId = num;
						cachedStatement.m_bindAccessors = this.m_bindAccessors;
						cachedStatement.m_bindParamMetadata = @null.m_paramCollInfoArray;
						cachedStatement.m_scnFromExecution = scnFromExecution;
						cachedStatement.m_placeHolderCollection = arrayList;
						cachedStatement.m_bBindParamPresent = bBindParamPresent;
						cachedStatement.m_bindDirections = @null.m_bindDirections;
						cachedStatement.m_bAllInBinds = @null.m_bAllInBinds;
						cachedStatement.m_bAllOutBinds = @null.m_bAllOutBinds;
						if (cachedStatement.statementdata != null && enumerable != null)
						{
							foreach (OracleLpStatement oracleLpStatement in enumerable)
							{
								oracleLpStatement.m_vODPContext = null;
							}
							cachedStatement.statementdata.parsedStmt = enumerable;
						}
					}
					if (cachedStatement != null && connectionImpl.m_statementCache != null)
					{
						if (this.m_addToStatementCache && connectionImpl.m_pm != null && connectionImpl.m_pm.m_bSelfTuning && !OracleTuner.Instance.HighMemoryUsageAlert)
						{
							connectionImpl.AcceptStatementData(commandText);
						}
						CachedStatement cachedStatement2 = connectionImpl.m_statementCache.Put(commandText, cachedStatement);
						if (cachedStatement2 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement2.m_cursorId);
						}
					}
					else
					{
						connectionImpl.AddCursorIdToBeClosed((long)num);
					}
				}
				finally
				{
					if (num5 > 0)
					{
						connectionImpl.m_connectionFreeToUseEvent.Set();
					}
					else if (flag)
					{
						CachedStatement cachedStatement3 = connectionImpl.m_statementCache.Put(commandText, cachedStatement);
						if (cachedStatement3 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement3.m_cursorId);
						}
					}
				}
				if (this.m_bHasReturningClause && flag3)
				{
					throw new OracleException(24369, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(24369, new string[0]));
				}
				if (SqlStatementType.DML != this.m_sqlStatementType)
				{
					num3 = -1;
				}
				result = num3;
			}
			catch (Exception ex)
			{
				if (bindByPositionParamColl != null)
				{
					foreach (object obj in bindByPositionParamColl)
					{
						OracleParameter oracleParameter = (OracleParameter)obj;
						oracleParameter.PreBindFree();
					}
				}
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				if (ex is OracleException)
				{
					connectionImpl.m_lastErrorNum = ((OracleException)ex).Number;
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
			return result;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x000AAAD8 File Offset: 0x000A8CD8
		internal int ExecuteXmlQuery(string commandText, OracleParameterCollection paramColl, CommandType commandType, OracleXmlCommandType xmlCommandType, OracleConnectionImpl connectionImpl, int longFetchSize, long clientInitialLOBFS, OracleDependencyImpl orclDependencyImpl, OracleConnection connection, out long[] scnFromExecution, out OracleParameterCollection bindByPositionParamColl, ref bool bBindParamPresent, ref OracleXmlQueryProperties xmlQueryProperties, out OracleException exceptionForArrayBindDML, out bool transform, out int numberOfUserParameters, ref OracleLogicalTransaction oracleLogicalTransaction, bool isFromEF = false, bool isOracle8i = false, bool wantResult = false)
		{
			bool flag = false;
			bool flag2 = false;
			Accessor[] array = null;
			ColumnDescribeInfo[] cachedParamMetadata = null;
			CachedStatement cachedStatement = null;
			SQLMetaData sqlmetaData = null;
			SQLInfo sqlinfo = null;
			int num = 0;
			ArrayList arrayList = null;
			bindByPositionParamColl = null;
			bool flag3 = false;
			bool? flag4 = null;
			this.m_sqlMetaData = null;
			scnFromExecution = null;
			numberOfUserParameters = 0;
			transform = false;
			IEnumerable<OracleLpStatement> enumerable = null;
			IEnumerable<OracleLpStatement> enumerable2 = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				if (this.m_implicitRSList != null && this.m_implicitRSList.Count > 0)
				{
					this.CloseImplicitRefCursors(connectionImpl);
					this.m_implicitRSList.Clear();
				}
				if (xmlQueryProperties == null)
				{
					xmlQueryProperties = new OracleXmlQueryProperties();
				}
				if (xmlQueryProperties.Xslt != null && xmlQueryProperties.Xslt.Length != 0)
				{
					transform = true;
				}
				string text = ":OracleResult$";
				string parameterName = ":OracleXslDoc$";
				string parameterName2 = ":OracleSqlQuery$";
				BindDirection[] bindDirections = null;
				flag2 = ((this.m_sqlMetaData = sqlmetaData) != null);
				bool flag5 = false;
				bool flag6 = false;
				if (!flag || (bBindParamPresent && this.m_bBindByName && arrayList == null))
				{
					if (this.m_bBindByName)
					{
						arrayList = new ArrayList();
					}
					if (commandType == CommandType.Text)
					{
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
						{
							this.GetBindInfoUsingAdrianParser(connection, commandText, ref bBindParamPresent, ref arrayList, ref enumerable, ref flag5);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
						{
							this.ParseCommandText(commandText, ref bBindParamPresent, ref arrayList);
						}
					}
					else if (commandType == CommandType.TableDirect)
					{
						bBindParamPresent = false;
					}
				}
				if (this.m_bBindByName && paramColl != null && paramColl.Count > 0)
				{
					if (commandType == CommandType.StoredProcedure)
					{
						bindByPositionParamColl = this.ReorderBindByNameBasedParameterCollectionForStoredProcedure(paramColl);
					}
					else
					{
						bindByPositionParamColl = this.GetBindByPositionBasedParameterCollection(paramColl, arrayList, true);
					}
				}
				else
				{
					bindByPositionParamColl = paramColl;
				}
				if (bindByPositionParamColl != null && bindByPositionParamColl.Count > 0)
				{
					numberOfUserParameters = bindByPositionParamColl.Count;
					if (!this.m_bBindByName)
					{
						throw new InvalidOperationException();
					}
				}
				int num2 = numberOfUserParameters;
				if (wantResult)
				{
					num2++;
				}
				if (transform)
				{
					num2++;
				}
				if (isOracle8i)
				{
					num2++;
				}
				this.BuildXmlQueryCommandText(wantResult, text, isOracle8i, commandText, bindByPositionParamColl, xmlCommandType, xmlQueryProperties);
				this.m_sqlStatementType = SqlStatementType.PLSQL;
				if (connectionImpl.m_statementCache != null && (!connectionImpl.m_marshallingEngine.m_bDRCPConnection || connectionImpl.m_marshallingEngine.m_bDRCPSessionAttached))
				{
					OracleCommandImpl.ValidateStatementCacheSize(connectionImpl);
					connectionImpl.m_statementCache.Get(this.m_pooledCmdText, out cachedStatement, out sqlmetaData, out sqlinfo);
					if (cachedStatement != null)
					{
						num = cachedStatement.m_cursorId;
						this.m_bindAccessors = cachedStatement.m_bindAccessors;
						cachedParamMetadata = cachedStatement.m_bindParamMetadata;
						scnFromExecution = cachedStatement.m_scnFromExecution;
						arrayList = cachedStatement.m_placeHolderCollection;
						bBindParamPresent = cachedStatement.m_bBindParamPresent;
						bindDirections = cachedStatement.m_bindDirections;
						this.m_bindDirectionsFromServer = cachedStatement.m_bindDirections;
						enumerable2 = ((cachedStatement.statementdata != null) ? cachedStatement.statementdata.parsedStmt : null);
						flag = true;
					}
				}
				if (sqlinfo != null)
				{
					this.m_commandTextByteStream = sqlinfo.m_SQLcommandTextByteStream;
					this.m_sqlStatementType = sqlinfo.m_SQLStatementType;
				}
				else
				{
					if (commandType == CommandType.StoredProcedure)
					{
						this.m_sqlStatementType = SqlStatementType.PLSQL;
					}
					else if (commandType == CommandType.TableDirect)
					{
						this.m_sqlStatementType = SqlStatementType.SELECT;
					}
					else
					{
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag6)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementTypeAdrianParser(connection, this.m_pooledCmdText, ref enumerable2, ref flag4, ref flag6);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag6)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementType(this.m_pooledCmdText, ref flag4);
						}
					}
					this.m_commandTextByteStream = connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(this.m_pooledCmdText, 0, this.m_pooledCmdText.Length, true);
				}
				if (num2 > 0 && bindByPositionParamColl == null)
				{
					bindByPositionParamColl = new OracleParameterCollection();
				}
				if (isOracle8i)
				{
					if (commandText.Length > 32512)
					{
						OracleParameter oracleParameter = new OracleParameter(parameterName2, OracleDbType.Clob);
						oracleParameter.Direction = ParameterDirection.Input;
						OracleClob oracleClob = new OracleClob(connection);
						oracleClob.Append(commandText.ToCharArray(), 0, commandText.Length);
						oracleParameter.Value = oracleClob;
						bindByPositionParamColl.Add(oracleParameter);
					}
					else
					{
						OracleParameter oracleParameter = new OracleParameter(parameterName2, OracleDbType.Varchar2);
						oracleParameter.Direction = ParameterDirection.Input;
						oracleParameter.Value = commandText;
						bindByPositionParamColl.Add(oracleParameter);
					}
					bBindParamPresent = true;
				}
				if (transform)
				{
					if (xmlQueryProperties.Xslt.Length > 32512 || isOracle8i)
					{
						OracleParameter oracleParameter2 = new OracleParameter(parameterName, OracleDbType.Clob);
						oracleParameter2.Direction = ParameterDirection.Input;
						OracleClob oracleClob2 = new OracleClob(connection);
						oracleClob2.Append(xmlQueryProperties.Xslt.ToCharArray(), 0, xmlQueryProperties.Xslt.Length);
						oracleParameter2.Value = oracleClob2;
						bindByPositionParamColl.Add(oracleParameter2);
					}
					else
					{
						OracleParameter oracleParameter2 = new OracleParameter(parameterName, OracleDbType.Varchar2);
						oracleParameter2.Direction = ParameterDirection.Input;
						oracleParameter2.Value = xmlQueryProperties.Xslt;
						bindByPositionParamColl.Add(oracleParameter2);
					}
					bBindParamPresent = true;
				}
				if (wantResult)
				{
					OracleParameter oracleParameter3 = new OracleParameter(text, OracleDbType.Clob);
					oracleParameter3.Direction = ParameterDirection.Output;
					bindByPositionParamColl.Add(oracleParameter3);
					bBindParamPresent = true;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
					{
						this.m_pooledCmdText
					});
				}
				bool flag7 = false;
				bool flag8 = false;
				TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
				if (cachedStatement != null)
				{
					@null.m_bAllInBinds = cachedStatement.m_bAllInBinds;
					@null.m_bAllOutBinds = cachedStatement.m_bAllOutBinds;
				}
				if (bBindParamPresent && bindByPositionParamColl != null)
				{
					@null.m_bindDirections = bindDirections;
					@null.m_sqlStmtType = this.m_sqlStatementType;
					if (this.m_bindAccessors == null || this.m_bindAccessors.Length != bindByPositionParamColl.Count)
					{
						this.m_bindAccessors = new Accessor[num2];
					}
					this.ProcessParameters(bindByPositionParamColl, connectionImpl, cachedParamMetadata, ref flag8, isFromEF && this.m_sqlStatementType == SqlStatementType.SELECT, ref @null);
				}
				else
				{
					this.m_bindAccessors = null;
					this.m_bindDirectionsFromServer = null;
					this.m_arrayBindCount = 0;
				}
				if (flag && !flag8 && this.CanUseOptimizeExecute(this.m_sqlStatementType, (long)this.m_rowsToFetch, 1L, false, orclDependencyImpl != null))
				{
					flag7 = true;
				}
				int num3 = 0;
				int num4 = 0;
				long num5 = 0L;
				DataUnmarshaller dataUnmarshaller = null;
				if (scnFromExecution != null)
				{
					scnFromExecution[0] = 0L;
					scnFromExecution[1] = 0L;
				}
				else
				{
					scnFromExecution = new long[2];
				}
				if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
				{
					this.CheckForReturningClauseAdrianParser(connection, this.m_sqlStatementType, sqlinfo, commandType, ref enumerable, commandText, ref flag5);
				}
				if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
				{
					this.CheckForReturningClause(this.m_pooledCmdText, this.m_sqlStatementType, sqlinfo, commandType, bindByPositionParamColl, arrayList, bBindParamPresent, commandText);
				}
				bool bDisableCompressedFetch = this.m_bHasReturningClause || SqlStatementType.PLSQL == this.m_sqlStatementType || (cachedStatement != null && cachedStatement.m_bDisableCompressedFetch);
				int num6 = -1;
				try
				{
					num6 = connectionImpl.WaitForConnectionForExecution(this.m_cancelExecutionEvent);
					this.m_continueCancel.Set();
					connectionImpl.AddAllPiggyBackRequests();
					TTCExecuteSql executeSqlObject = connectionImpl.ExecuteSqlObject;
					if (flag7)
					{
						oracleLogicalTransaction = connection.OracleLogicalTransaction;
						executeSqlObject.SendReExecuteRequest(connectionImpl, num, 0L, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, 0, ref @null);
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num3, ref num5, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, 0, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
						exceptionForArrayBindDML = null;
						bool flag9 = false;
						num4 = this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag9, false);
					}
					else
					{
						bool parse = !flag;
						bool fetch = flag || this.m_bHasReturningClause || this.m_sqlStatementType == SqlStatementType.PLSQL;
						oracleLogicalTransaction = connection.OracleLogicalTransaction;
						executeSqlObject.SendExecuteRequest(connectionImpl, flag ? null : this.m_commandTextByteStream, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), null, 0L, parse, true, fetch, false, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, 0, null, ref @null, 0);
						if (@null.m_bindDirections == null && @null.m_paramValueArray != null)
						{
							@null.ResetBindDirections(bindByPositionParamColl.Count);
							@null.m_bAllInBinds = true;
						}
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num3, ref num5, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, 0, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
						exceptionForArrayBindDML = null;
						bool flag10 = false;
						num4 = this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag10, false);
					}
					lock (this.m_lockCancel)
					{
						this.m_bServerExecutionComplete = true;
						if (this.m_executionId >= 9223372036854775807L)
						{
							this.m_executionId = 0L;
						}
						else
						{
							this.m_executionId += 1L;
						}
					}
					if (connectionImpl.m_oracleCommunication.InBreakResetMode())
					{
						connectionImpl.m_marshallingEngine.ProcessReset();
					}
					if (executeSqlObject.m_bSessionTimeZoneUpdated)
					{
						connectionImpl.m_sessionTimeZone = new OracleIntervalDS(executeSqlObject.m_sessionTimeZone);
						executeSqlObject.m_bSessionTimeZoneUpdated = false;
					}
					this.m_sessionTimeZone = connectionImpl.m_sessionTimeZone;
					if (this.m_sqlMetaData != null)
					{
						this.m_sqlMetaData.pCommandText = this.m_pooledCmdText;
						if (!flag2)
						{
							sqlmetaData = this.m_sqlMetaData;
						}
					}
					this.m_bindDirectionsFromServer = @null.m_bindDirections;
					if (orclDependencyImpl != null)
					{
						orclDependencyImpl.m_bIsEnabled = orclDependencyImpl.m_bIsRegistered;
						if (orclDependencyImpl.m_bQueryBasedNTFN && !orclDependencyImpl.m_queryIDList.Contains(num5))
						{
							lock (orclDependencyImpl.m_syncList)
							{
								if (!orclDependencyImpl.m_queryIDList.Contains(num5))
								{
									orclDependencyImpl.m_queryIDList.Add(num5);
								}
							}
						}
					}
					if ((flag4 == null || flag4 == false) && (this.m_sqlStatementType == SqlStatementType.DML || this.m_sqlStatementType == SqlStatementType.SELECT || this.m_sqlStatementType == SqlStatementType.PLSQL) && !flag && connectionImpl.m_statementCache != null && this.m_addToStatementCache)
					{
						cachedStatement = new CachedStatement();
						cachedStatement.sqlInfo = new SQLInfo
						{
							m_SQLcommandTextByteStream = this.m_commandTextByteStream,
							m_SQLhasReturningClause = this.m_bHasReturningClause,
							m_SQLStatementType = this.m_sqlStatementType
						};
						cachedStatement.statementdata = sqlmetaData;
						cachedStatement.m_cursorId = num;
						cachedStatement.m_bindAccessors = this.m_bindAccessors;
						cachedStatement.m_bindParamMetadata = @null.m_paramCollInfoArray;
						cachedStatement.m_scnFromExecution = scnFromExecution;
						cachedStatement.m_placeHolderCollection = arrayList;
						cachedStatement.m_bBindParamPresent = bBindParamPresent;
						cachedStatement.m_bindDirections = @null.m_bindDirections;
						cachedStatement.m_bAllInBinds = @null.m_bAllInBinds;
						cachedStatement.m_bAllOutBinds = @null.m_bAllOutBinds;
						if (cachedStatement.statementdata != null && enumerable2 != null)
						{
							foreach (OracleLpStatement oracleLpStatement in enumerable2)
							{
								oracleLpStatement.m_vODPContext = null;
							}
							cachedStatement.statementdata.parsedStmt = enumerable2;
						}
					}
					if (cachedStatement != null && connectionImpl.m_statementCache != null)
					{
						if (this.m_addToStatementCache && connectionImpl.m_pm != null && connectionImpl.m_pm.m_bSelfTuning && !OracleTuner.Instance.HighMemoryUsageAlert)
						{
							connectionImpl.AcceptStatementData(this.m_pooledCmdText);
						}
						CachedStatement cachedStatement2 = connectionImpl.m_statementCache.Put(this.m_pooledCmdText, cachedStatement);
						if (cachedStatement2 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement2.m_cursorId);
						}
					}
					else
					{
						connectionImpl.AddCursorIdToBeClosed((long)num);
					}
				}
				finally
				{
					if (num6 > 0)
					{
						connectionImpl.m_connectionFreeToUseEvent.Set();
					}
					else if (flag)
					{
						CachedStatement cachedStatement3 = connectionImpl.m_statementCache.Put(commandText, cachedStatement);
						if (cachedStatement3 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement3.m_cursorId);
						}
					}
				}
				if (this.m_bHasReturningClause && flag3)
				{
					throw new OracleException(24369, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(24369, new string[0]));
				}
				if (SqlStatementType.DML != this.m_sqlStatementType)
				{
					num4 = -1;
				}
				result = num4;
			}
			catch (Exception ex)
			{
				if (bindByPositionParamColl != null)
				{
					foreach (object obj in bindByPositionParamColl)
					{
						OracleParameter oracleParameter4 = (OracleParameter)obj;
						oracleParameter4.PreBindFree();
					}
				}
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				if (ex is OracleException)
				{
					connectionImpl.m_lastErrorNum = ((OracleException)ex).Number;
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
			return result;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000AB7F8 File Offset: 0x000A99F8
		internal int ExecuteXmlSave(string commandText, OracleParameterCollection paramColl, CommandType commandType, OracleXmlCommandType xmlCommandType, OracleConnectionImpl connectionImpl, int longFetchSize, long clientInitialLOBFS, OracleDependencyImpl orclDependencyImpl, OracleConnection connection, out long[] scnFromExecution, out OracleParameterCollection bindByPositionParamColl, ref bool bBindParamPresent, ref OracleXmlSaveProperties xmlSaveProperties, out OracleException exceptionForArrayBindDML, out bool transform, ref OracleLogicalTransaction oracleLogicalTransaction, bool isFromEF = false)
		{
			bool flag = false;
			bool flag2 = false;
			Accessor[] array = null;
			ColumnDescribeInfo[] cachedParamMetadata = null;
			CachedStatement cachedStatement = null;
			SQLMetaData sqlmetaData = null;
			SQLInfo sqlinfo = null;
			int num = 0;
			ArrayList arrayList = null;
			bindByPositionParamColl = null;
			bool flag3 = false;
			bool? flag4 = null;
			this.m_sqlMetaData = null;
			scnFromExecution = null;
			IEnumerable<OracleLpStatement> enumerable = null;
			bool flag5 = false;
			transform = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (xmlSaveProperties == null)
			{
				xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if (xmlSaveProperties.Xslt != null && xmlSaveProperties.Xslt.Length != 0)
			{
				transform = true;
			}
			int result;
			try
			{
				if (this.m_implicitRSList != null && this.m_implicitRSList.Count > 0)
				{
					this.CloseImplicitRefCursors(connectionImpl);
					this.m_implicitRSList.Clear();
				}
				BindDirection[] bindDirections = null;
				flag2 = ((this.m_sqlMetaData = sqlmetaData) != null);
				string parameterName = ":OracleXmlDoc$";
				string parameterName2 = ":OracleResult$";
				string parameterName3 = ":OracleTableName$";
				string parameterName4 = ":OracleXslDoc$";
				int num2 = 3;
				if (transform)
				{
					num2++;
				}
				this.BuildXmlSaveCommandText(connection, commandText, xmlCommandType, xmlSaveProperties);
				if (connectionImpl.m_statementCache != null && (!connectionImpl.m_marshallingEngine.m_bDRCPConnection || connectionImpl.m_marshallingEngine.m_bDRCPSessionAttached))
				{
					OracleCommandImpl.ValidateStatementCacheSize(connectionImpl);
					connectionImpl.m_statementCache.Get(this.m_pooledCmdText, out cachedStatement, out sqlmetaData, out sqlinfo);
					if (cachedStatement != null)
					{
						num = cachedStatement.m_cursorId;
						this.m_bindAccessors = cachedStatement.m_bindAccessors;
						cachedParamMetadata = cachedStatement.m_bindParamMetadata;
						scnFromExecution = cachedStatement.m_scnFromExecution;
						arrayList = cachedStatement.m_placeHolderCollection;
						bBindParamPresent = cachedStatement.m_bBindParamPresent;
						bindDirections = cachedStatement.m_bindDirections;
						this.m_bindDirectionsFromServer = cachedStatement.m_bindDirections;
						enumerable = ((cachedStatement.statementdata != null) ? cachedStatement.statementdata.parsedStmt : null);
						flag = true;
					}
				}
				if (sqlinfo != null)
				{
					this.m_commandTextByteStream = sqlinfo.m_SQLcommandTextByteStream;
					this.m_sqlStatementType = sqlinfo.m_SQLStatementType;
				}
				else
				{
					if (commandType == CommandType.StoredProcedure)
					{
						this.m_sqlStatementType = SqlStatementType.PLSQL;
					}
					else if (commandType == CommandType.TableDirect)
					{
						this.m_sqlStatementType = SqlStatementType.SELECT;
					}
					else
					{
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementTypeAdrianParser(connection, this.m_pooledCmdText, ref enumerable, ref flag4, ref flag5);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementType(this.m_pooledCmdText, ref flag4);
						}
					}
					this.m_commandTextByteStream = connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(this.m_pooledCmdText, 0, this.m_pooledCmdText.Length, true);
				}
				bindByPositionParamColl = new OracleParameterCollection();
				OracleParameter oracleParameter = new OracleParameter(parameterName3, OracleDbType.Varchar2);
				oracleParameter.Direction = ParameterDirection.Input;
				if (xmlSaveProperties.Table == null)
				{
					oracleParameter.Value = string.Empty;
				}
				else
				{
					oracleParameter.Value = xmlSaveProperties.Table;
				}
				bindByPositionParamColl.Add(oracleParameter);
				if (transform)
				{
					if (connection.m_majorVersion == 8 && connection.m_minorVersion == 1 && xmlSaveProperties.Xslt.Length <= 32512)
					{
						OracleParameter oracleParameter2 = new OracleParameter(parameterName4, OracleDbType.Varchar2);
						oracleParameter2.Direction = ParameterDirection.Input;
						oracleParameter2.Value = xmlSaveProperties.Xslt;
						bindByPositionParamColl.Add(oracleParameter2);
					}
					else
					{
						OracleParameter oracleParameter2 = new OracleParameter(parameterName4, OracleDbType.Clob);
						oracleParameter2.Direction = ParameterDirection.Input;
						OracleClob oracleClob = new OracleClob(connection);
						oracleClob.Append(xmlSaveProperties.Xslt.ToCharArray(), 0, xmlSaveProperties.Xslt.Length);
						oracleParameter2.Value = oracleClob;
						bindByPositionParamColl.Add(oracleParameter2);
					}
				}
				OracleParameter oracleParameter3 = new OracleParameter();
				oracleParameter3.ParameterName = parameterName2;
				oracleParameter3.DbType = DbType.Int32;
				oracleParameter3.Direction = ParameterDirection.Output;
				bindByPositionParamColl.Add(oracleParameter3);
				if (commandText.Length > 32512)
				{
					OracleParameter oracleParameter4 = new OracleParameter(parameterName, OracleDbType.Clob);
					oracleParameter4.Direction = ParameterDirection.Input;
					OracleClob oracleClob2 = new OracleClob(connection);
					oracleClob2.Append(commandText.ToCharArray(), 0, commandText.Length);
					oracleParameter4.Value = oracleClob2;
					bindByPositionParamColl.Add(oracleParameter4);
				}
				else
				{
					OracleParameter oracleParameter4 = new OracleParameter(parameterName, OracleDbType.Varchar2);
					oracleParameter4.Direction = ParameterDirection.Input;
					oracleParameter4.Value = commandText;
					bindByPositionParamColl.Add(oracleParameter4);
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
					{
						this.m_pooledCmdText
					});
				}
				bool flag6 = false;
				bool flag7 = false;
				TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
				if (cachedStatement != null)
				{
					@null.m_bAllInBinds = cachedStatement.m_bAllInBinds;
					@null.m_bAllOutBinds = cachedStatement.m_bAllOutBinds;
				}
				if (bBindParamPresent && bindByPositionParamColl != null)
				{
					@null.m_bindDirections = bindDirections;
					@null.m_sqlStmtType = this.m_sqlStatementType;
					if (this.m_bindAccessors == null || this.m_bindAccessors.Length != bindByPositionParamColl.Count)
					{
						this.m_bindAccessors = new Accessor[bindByPositionParamColl.Count];
					}
					this.ProcessParameters(bindByPositionParamColl, connectionImpl, cachedParamMetadata, ref flag7, isFromEF && this.m_sqlStatementType == SqlStatementType.SELECT, ref @null);
				}
				else
				{
					this.m_bindAccessors = null;
					this.m_bindDirectionsFromServer = null;
					this.m_arrayBindCount = 0;
				}
				if (flag && !flag7 && this.CanUseOptimizeExecute(this.m_sqlStatementType, (long)this.m_rowsToFetch, 1L, false, orclDependencyImpl != null))
				{
					flag6 = true;
				}
				int num3 = 0;
				int num4 = 0;
				long num5 = 0L;
				DataUnmarshaller dataUnmarshaller = null;
				if (scnFromExecution != null)
				{
					scnFromExecution[0] = 0L;
					scnFromExecution[1] = 0L;
				}
				else
				{
					scnFromExecution = new long[2];
				}
				if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
				{
					this.CheckForReturningClauseAdrianParser(connection, this.m_sqlStatementType, sqlinfo, commandType, ref enumerable, commandText, ref flag5);
				}
				if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
				{
					this.CheckForReturningClause(this.m_pooledCmdText, this.m_sqlStatementType, sqlinfo, commandType, bindByPositionParamColl, arrayList, bBindParamPresent, commandText);
				}
				bool bDisableCompressedFetch = this.m_bHasReturningClause || SqlStatementType.PLSQL == this.m_sqlStatementType || (cachedStatement != null && cachedStatement.m_bDisableCompressedFetch);
				int num6 = -1;
				try
				{
					num6 = connectionImpl.WaitForConnectionForExecution(this.m_cancelExecutionEvent);
					this.m_continueCancel.Set();
					connectionImpl.AddAllPiggyBackRequests();
					TTCExecuteSql executeSqlObject = connectionImpl.ExecuteSqlObject;
					if (flag6)
					{
						oracleLogicalTransaction = connection.OracleLogicalTransaction;
						executeSqlObject.SendReExecuteRequest(connectionImpl, num, 0L, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, 0, ref @null);
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num3, ref num5, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, 0, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
						exceptionForArrayBindDML = null;
						bool flag8 = false;
						num4 = this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag8, false);
					}
					else
					{
						bool parse = !flag;
						bool fetch = flag || this.m_bHasReturningClause || this.m_sqlStatementType == SqlStatementType.PLSQL;
						oracleLogicalTransaction = connection.OracleLogicalTransaction;
						executeSqlObject.SendExecuteRequest(connectionImpl, flag ? null : this.m_commandTextByteStream, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), null, 0L, parse, true, fetch, false, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, 0, null, ref @null, 0);
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, -1L, 0, out num3, ref num5, longFetchSize, 0L, scnFromExecution, @null.m_bAllInBinds, 0, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, false);
						exceptionForArrayBindDML = null;
						bool flag9 = false;
						num4 = this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out flag9, false);
					}
					lock (this.m_lockCancel)
					{
						this.m_bServerExecutionComplete = true;
						if (this.m_executionId >= 9223372036854775807L)
						{
							this.m_executionId = 0L;
						}
						else
						{
							this.m_executionId += 1L;
						}
					}
					if (connectionImpl.m_oracleCommunication.InBreakResetMode())
					{
						connectionImpl.m_marshallingEngine.ProcessReset();
					}
					if (executeSqlObject.m_bSessionTimeZoneUpdated)
					{
						connectionImpl.m_sessionTimeZone = new OracleIntervalDS(executeSqlObject.m_sessionTimeZone);
						executeSqlObject.m_bSessionTimeZoneUpdated = false;
					}
					this.m_sessionTimeZone = connectionImpl.m_sessionTimeZone;
					if (this.m_sqlMetaData != null)
					{
						this.m_sqlMetaData.pCommandText = this.m_pooledCmdText;
						if (!flag2)
						{
							sqlmetaData = this.m_sqlMetaData;
						}
					}
					this.m_bindDirectionsFromServer = @null.m_bindDirections;
					if (orclDependencyImpl != null)
					{
						orclDependencyImpl.m_bIsEnabled = orclDependencyImpl.m_bIsRegistered;
						if (orclDependencyImpl.m_bQueryBasedNTFN && !orclDependencyImpl.m_queryIDList.Contains(num5))
						{
							lock (orclDependencyImpl.m_syncList)
							{
								if (!orclDependencyImpl.m_queryIDList.Contains(num5))
								{
									orclDependencyImpl.m_queryIDList.Add(num5);
								}
							}
						}
					}
					if ((flag4 == null || flag4 == false) && (this.m_sqlStatementType == SqlStatementType.DML || this.m_sqlStatementType == SqlStatementType.SELECT || this.m_sqlStatementType == SqlStatementType.PLSQL) && !flag && connectionImpl.m_statementCache != null && this.m_addToStatementCache)
					{
						cachedStatement = new CachedStatement();
						cachedStatement.sqlInfo = new SQLInfo
						{
							m_SQLcommandTextByteStream = this.m_commandTextByteStream,
							m_SQLhasReturningClause = this.m_bHasReturningClause,
							m_SQLStatementType = this.m_sqlStatementType
						};
						cachedStatement.statementdata = sqlmetaData;
						cachedStatement.m_cursorId = num;
						cachedStatement.m_bindAccessors = this.m_bindAccessors;
						cachedStatement.m_bindParamMetadata = @null.m_paramCollInfoArray;
						cachedStatement.m_scnFromExecution = scnFromExecution;
						cachedStatement.m_placeHolderCollection = arrayList;
						cachedStatement.m_bBindParamPresent = bBindParamPresent;
						cachedStatement.m_bindDirections = @null.m_bindDirections;
						cachedStatement.m_bAllInBinds = @null.m_bAllInBinds;
						cachedStatement.m_bAllOutBinds = @null.m_bAllOutBinds;
						if (cachedStatement.statementdata != null && enumerable != null)
						{
							foreach (OracleLpStatement oracleLpStatement in enumerable)
							{
								oracleLpStatement.m_vODPContext = null;
							}
							cachedStatement.statementdata.parsedStmt = enumerable;
						}
					}
					if (cachedStatement != null && connectionImpl.m_statementCache != null)
					{
						if (this.m_addToStatementCache && connectionImpl.m_pm != null && connectionImpl.m_pm.m_bSelfTuning && !OracleTuner.Instance.HighMemoryUsageAlert)
						{
							connectionImpl.AcceptStatementData(this.m_pooledCmdText);
						}
						CachedStatement cachedStatement2 = connectionImpl.m_statementCache.Put(this.m_pooledCmdText, cachedStatement);
						if (cachedStatement2 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement2.m_cursorId);
						}
					}
					else
					{
						connectionImpl.AddCursorIdToBeClosed((long)num);
					}
				}
				finally
				{
					if (num6 > 0)
					{
						connectionImpl.m_connectionFreeToUseEvent.Set();
					}
					else if (flag)
					{
						CachedStatement cachedStatement3 = connectionImpl.m_statementCache.Put(commandText, cachedStatement);
						if (cachedStatement3 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement3.m_cursorId);
						}
					}
				}
				if (this.m_bHasReturningClause && flag3)
				{
					throw new OracleException(24369, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(24369, new string[0]));
				}
				if (SqlStatementType.DML != this.m_sqlStatementType)
				{
					num4 = -1;
				}
				result = num4;
			}
			catch (Exception ex)
			{
				if (bindByPositionParamColl != null)
				{
					foreach (object obj in bindByPositionParamColl)
					{
						OracleParameter oracleParameter5 = (OracleParameter)obj;
						oracleParameter5.PreBindFree();
					}
				}
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				if (ex is OracleException)
				{
					connectionImpl.m_lastErrorNum = ((OracleException)ex).Number;
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
			return result;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000AC450 File Offset: 0x000AA650
		internal int ExecuteReader(string commandText, OracleParameterCollection paramColl, CommandType commandType, OracleConnectionImpl connectionImpl, ref OracleDataReaderImpl rdrImpl, int longFetchSize, long clientInitialLOBFS, OracleDependencyImpl orclDependencyImpl, long[] scnForExecution, out long[] scnFromExecution, out OracleParameterCollection bindByPositionParamColl, ref bool bBindParamPresent, ref long internalInitialLOBFS, out OracleException exceptionForArrayBindDML, OracleConnection connection, ref OracleLogicalTransaction oracleLogicalTransaction, IEnumerable<OracleLpStatement> adrianParsedStmt, bool isDescribeOnly = false, bool isFromEF = false)
		{
			bool flag = false;
			bool flag2 = false;
			Accessor[] array = null;
			ColumnDescribeInfo[] cachedParamMetadata = null;
			List<object> oraBufReleaseInfoList = null;
			CachedStatement cachedStatement = null;
			SQLMetaData sqlmetaData = null;
			SQLInfo sqlinfo = null;
			int num = 0;
			bool bDisableCompressedFetch = false;
			ArrayList arrayList = null;
			DataUnmarshaller dataUnmarshaller = null;
			bool flag3 = false;
			bool? flag4 = new bool?(false);
			string text = commandText;
			bool flag5 = false;
			this.m_sqlMetaData = null;
			scnFromExecution = null;
			bindByPositionParamColl = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				if (this.m_implicitRSList != null && this.m_implicitRSList.Count > 0)
				{
					this.CloseImplicitRefCursors(connectionImpl);
					this.m_implicitRSList.Clear();
				}
				BindDirection[] bindDirections = null;
				if (connectionImpl.m_statementCache != null && (!connectionImpl.m_marshallingEngine.m_bDRCPConnection || connectionImpl.m_marshallingEngine.m_bDRCPSessionAttached))
				{
					OracleCommandImpl.ValidateStatementCacheSize(connectionImpl);
					connectionImpl.m_statementCache.Get(commandText, out cachedStatement, out sqlmetaData, out sqlinfo);
					if (cachedStatement != null)
					{
						num = cachedStatement.m_cursorId;
						array = cachedStatement.m_accessors;
						this.m_bindAccessors = cachedStatement.m_bindAccessors;
						cachedParamMetadata = cachedStatement.m_bindParamMetadata;
						scnFromExecution = cachedStatement.m_scnFromExecution;
						arrayList = cachedStatement.m_placeHolderCollection;
						bBindParamPresent = cachedStatement.m_bBindParamPresent;
						bindDirections = cachedStatement.m_bindDirections;
						dataUnmarshaller = cachedStatement.m_dataUnmarshaller;
						this.m_bindDirectionsFromServer = cachedStatement.m_bindDirections;
						if (adrianParsedStmt == null)
						{
							adrianParsedStmt = ((cachedStatement.statementdata != null) ? cachedStatement.statementdata.parsedStmt : null);
						}
						flag = true;
					}
				}
				if (sqlinfo != null)
				{
					this.m_commandTextByteStream = sqlinfo.m_SQLcommandTextByteStream;
					this.m_sqlStatementType = sqlinfo.m_SQLStatementType;
				}
				else
				{
					if (commandType == CommandType.StoredProcedure)
					{
						this.m_sqlStatementType = SqlStatementType.PLSQL;
					}
					else if (commandType == CommandType.TableDirect)
					{
						this.m_sqlStatementType = SqlStatementType.SELECT;
					}
					else
					{
						OracleCommandImpl.TrimCommentsFromSQL(ref text);
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementTypeAdrianParser(connection, text, ref adrianParsedStmt, ref flag4, ref flag5);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementType(text, ref flag4);
						}
					}
					string text2 = commandText;
					if (!connectionImpl.m_isDb11gR1OrHigher && (this.m_sqlStatementType == SqlStatementType.PLSQL || text.StartsWith("create", StringComparison.InvariantCultureIgnoreCase)))
					{
						text2 = commandText.Replace("\r\n", "\n");
					}
					this.m_commandTextByteStream = connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(text2, 0, text2.Length, true);
				}
				flag2 = ((this.m_sqlMetaData = sqlmetaData) != null);
				if (flag2)
				{
					if (this.m_sqlStatementType == SqlStatementType.SELECT && (this.m_sqlMetaData.m_maxRowSize > 0 || this.m_sqlMetaData.m_bHasLOBOrLongColumn || this.m_sqlMetaData.m_bHasBFILEColumn))
					{
						int num2 = this.m_sqlMetaData.m_maxRowSize + this.m_sqlMetaData.m_numOfLOBColumns * Math.Max(86, 86 + (int)clientInitialLOBFS) + this.m_sqlMetaData.m_numOfLONGColumns * Math.Max(2, longFetchSize) + this.m_sqlMetaData.m_numOfBFileColumns * 86;
						this.m_rowsToFetch = (int)this.m_fetchSize / num2;
						this.m_rowsToFetch++;
						if (this.m_rowsToFetch > 65535)
						{
							this.m_rowsToFetch = 65535;
						}
					}
					else
					{
						this.m_rowsToFetch = 25;
					}
					if (longFetchSize > 0 && this.m_sqlMetaData.IsInitialLongFetchSizeInChars)
					{
						long num3 = (long)(longFetchSize * connectionImpl.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar);
						if (num3 < 2147483647L)
						{
							longFetchSize = (int)num3;
						}
						else
						{
							longFetchSize = int.MaxValue;
						}
					}
				}
				else
				{
					this.m_rowsToFetch = 25;
				}
				if (isDescribeOnly)
				{
					this.m_rowsToFetch = 0;
				}
				if (!flag || (bBindParamPresent && this.m_bBindByName && arrayList == null))
				{
					if (this.m_bBindByName)
					{
						arrayList = new ArrayList();
					}
					if (commandType == CommandType.Text)
					{
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
						{
							this.GetBindInfoUsingAdrianParser(connection, commandText, ref bBindParamPresent, ref arrayList, ref adrianParsedStmt, ref flag5);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
						{
							this.ParseCommandText(text, ref bBindParamPresent, ref arrayList);
						}
					}
					else if (commandType == CommandType.TableDirect)
					{
						bBindParamPresent = false;
					}
				}
				if (this.m_bBindByName && paramColl != null && paramColl.Count > 0)
				{
					if (commandType == CommandType.StoredProcedure)
					{
						bindByPositionParamColl = this.ReorderBindByNameBasedParameterCollectionForStoredProcedure(paramColl);
					}
					else
					{
						bindByPositionParamColl = this.GetBindByPositionBasedParameterCollection(paramColl, arrayList, false);
					}
				}
				else
				{
					bindByPositionParamColl = paramColl;
				}
				bool bLOBArrayFetchRequired = false;
				bool flag6 = false;
				bool flag7 = false;
				TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
				if (cachedStatement != null)
				{
					@null.m_bAllInBinds = cachedStatement.m_bAllInBinds;
					@null.m_bAllOutBinds = cachedStatement.m_bAllOutBinds;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
					{
						commandText
					});
				}
				if (bBindParamPresent && bindByPositionParamColl != null)
				{
					@null.m_bindDirections = bindDirections;
					@null.m_sqlStmtType = this.m_sqlStatementType;
					if (this.m_bindAccessors == null || this.m_bindAccessors.Length != paramColl.Count)
					{
						this.m_bindAccessors = new Accessor[paramColl.Count];
					}
					this.ProcessParameters(bindByPositionParamColl, connectionImpl, cachedParamMetadata, ref flag7, isFromEF && this.m_sqlStatementType == SqlStatementType.SELECT, ref @null);
				}
				else
				{
					this.m_bindAccessors = null;
					this.m_bindDirectionsFromServer = null;
					this.m_arrayBindCount = 0;
				}
				if (this.m_sqlMetaData != null && this.m_sqlMetaData.HasLOBOrLongColumn && connectionImpl.m_marshallingEngine.DBVersion >= 11100)
				{
					internalInitialLOBFS = TTCExecuteSql.CalculateInternalILFS(clientInitialLOBFS, this.m_bExecutingForFill, this.m_bReturnPSTypes);
					if ((!this.m_bExecutingForFill && -1L == clientInitialLOBFS) || (this.m_bExecutingForFill && !this.m_bReturnPSTypes))
					{
						bLOBArrayFetchRequired = true;
					}
				}
				if (flag && this.m_sqlMetaData != null && this.m_sqlMetaData.bGotDescribeInfoFromDB && (!this.m_sqlMetaData.HasLOBOrLongColumn || ((cachedStatement.m_bDefinesDone || connectionImpl.m_marshallingEngine.DBVersion < 11100) && cachedStatement.m_internalInitialLOBFS == internalInitialLOBFS && cachedStatement.m_longFetchSize == longFetchSize)) && !flag7 && this.CanUseOptimizeExecute(this.m_sqlStatementType, (long)this.m_rowsToFetch, (long)((this.m_arrayBindCount == 0) ? 1 : this.m_arrayBindCount), false, orclDependencyImpl != null))
				{
					flag6 = true;
				}
				int noOfRowsFetched = 0;
				bool bHasMoreRowsInDB = true;
				int num4 = 0;
				long num5 = 0L;
				if (scnFromExecution != null)
				{
					scnFromExecution[0] = 0L;
					scnFromExecution[1] = 0L;
				}
				else
				{
					scnFromExecution = new long[2];
				}
				if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag5)
				{
					this.CheckForReturningClauseAdrianParser(connection, this.m_sqlStatementType, sqlinfo, commandType, ref adrianParsedStmt, commandText, ref flag5);
				}
				if (ConfigBaseClass.m_bUseLegacyLocalParser || flag5)
				{
					this.CheckForReturningClause(text, this.m_sqlStatementType, sqlinfo, commandType, bindByPositionParamColl, arrayList, bBindParamPresent, commandText);
				}
				bDisableCompressedFetch = (this.m_bHasReturningClause || SqlStatementType.PLSQL == this.m_sqlStatementType || (cachedStatement != null && cachedStatement.m_bDisableCompressedFetch));
				int num6 = -1;
				try
				{
					num6 = connectionImpl.WaitForConnectionForExecution(this.m_cancelExecutionEvent);
					this.m_continueCancel.Set();
					connectionImpl.AddAllPiggyBackRequests();
					TTCExecuteSql executeSqlObject = connectionImpl.ExecuteSqlObject;
					if (flag6)
					{
						oracleLogicalTransaction = connection.OracleLogicalTransaction;
						executeSqlObject.SendReExecuteRequest(connectionImpl, num, (long)this.m_rowsToFetch, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, this.m_arrayBindCount, ref @null);
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, (cachedStatement != null) ? cachedStatement.m_numRowsFetchArrayCanAccomodate : -1L, (this.m_sqlStatementType == SqlStatementType.SELECT) ? this.m_rowsToFetch : 0, out noOfRowsFetched, ref num5, longFetchSize, internalInitialLOBFS, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, bLOBArrayFetchRequired);
						exceptionForArrayBindDML = null;
						num4 = this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out bHasMoreRowsInDB, false);
					}
					else
					{
						bool bThrowArrayBindRelatedErrors = true;
						bool parse = !flag;
						bool flag8 = false;
						ColumnDescribeInfo[] columnDefines = null;
						if (cachedStatement != null)
						{
							if ((!cachedStatement.m_bDefinesDone || cachedStatement.m_internalInitialLOBFS != internalInitialLOBFS) && this.m_sqlMetaData != null && this.m_sqlMetaData.HasLOBOrLongColumn && connectionImpl.m_marshallingEngine.DBVersion >= 11100)
							{
								flag8 = true;
								columnDefines = TTCExecuteSql.InitDefines(this.m_sqlMetaData.m_columnDescribeInfo, internalInitialLOBFS);
								cachedStatement.m_internalInitialLOBFS = internalInitialLOBFS;
								cachedStatement.m_bDefinesDone = true;
							}
							cachedStatement.m_longFetchSize = longFetchSize;
						}
						bool fetch = (flag && !flag8) || this.m_bHasReturningClause || this.m_sqlStatementType == SqlStatementType.PLSQL;
						bool flag9 = this.m_arrayBindCount > 1 && SqlStatementType.PLSQL == this.m_sqlStatementType;
						bool flag10 = false;
						if (flag9 && (!flag || (flag && !cachedStatement.m_bAllInBinds && !cachedStatement.m_bAllOutBinds)))
						{
							flag10 = true;
						}
						oracleLogicalTransaction = connection.OracleLogicalTransaction;
						executeSqlObject.SendExecuteRequest(connectionImpl, flag ? null : this.m_commandTextByteStream, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), columnDefines, (long)this.m_rowsToFetch, parse, true, fetch, flag8, !flag10 && connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, flag10 ? 1 : this.m_arrayBindCount, scnForExecution, ref @null, 0);
						if (@null.m_bindDirections == null && @null.m_paramValueArray != null)
						{
							@null.ResetBindDirections(paramColl.Count);
							@null.m_bAllInBinds = true;
						}
						executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, (cachedStatement != null) ? cachedStatement.m_numRowsFetchArrayCanAccomodate : -1L, (this.m_sqlStatementType == SqlStatementType.SELECT) ? this.m_rowsToFetch : 0, out noOfRowsFetched, ref num5, longFetchSize, internalInitialLOBFS, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, bLOBArrayFetchRequired);
						if (flag10)
						{
							bThrowArrayBindRelatedErrors = false;
						}
						exceptionForArrayBindDML = null;
						num4 = this.VerifyExecution(connectionImpl, out num, bThrowArrayBindRelatedErrors, ref exceptionForArrayBindDML, out bHasMoreRowsInDB, false);
						if (flag10)
						{
							if (@null.m_bAllInBinds || @null.m_bAllOutBinds)
							{
								oracleLogicalTransaction = connection.OracleLogicalTransaction;
								executeSqlObject.SendExecuteRequest(connectionImpl, null, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), columnDefines, (long)this.m_rowsToFetch, false, true, fetch, false, connectionImpl.m_autoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, this.m_arrayBindCount - 1, scnForExecution, ref @null, 1);
								executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, (cachedStatement != null) ? cachedStatement.m_numRowsFetchArrayCanAccomodate : -1L, (this.m_sqlStatementType == SqlStatementType.SELECT) ? this.m_rowsToFetch : 0, out noOfRowsFetched, ref num5, longFetchSize, internalInitialLOBFS, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount - 1, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, bLOBArrayFetchRequired);
								num4 += this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out bHasMoreRowsInDB, true);
							}
							else
							{
								bool bAutoCommit = false;
								for (int i = 1; i < this.m_arrayBindCount; i++)
								{
									if (i == this.m_arrayBindCount - 1)
									{
										bAutoCommit = connectionImpl.m_autoCommit;
									}
									oracleLogicalTransaction = connection.OracleLogicalTransaction;
									executeSqlObject.SendExecuteRequest(connectionImpl, null, this.m_bHasReturningClause, num, (long)((orclDependencyImpl != null) ? orclDependencyImpl.m_RegIdFromServer : 0), columnDefines, (long)this.m_rowsToFetch, false, true, fetch, false, bAutoCommit, bDisableCompressedFetch, this.m_sqlStatementType, longFetchSize, 1, scnForExecution, ref @null, i);
									executeSqlObject.ReceiveExecuteResponse(ref array, this.m_bindAccessors, this.m_bHasReturningClause, ref this.m_sqlMetaData, this.m_sqlStatementType, (cachedStatement != null) ? cachedStatement.m_numRowsFetchArrayCanAccomodate : -1L, (this.m_sqlStatementType == SqlStatementType.SELECT) ? this.m_rowsToFetch : 0, out noOfRowsFetched, ref num5, longFetchSize, internalInitialLOBFS, scnFromExecution, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, cachedStatement != null && cachedStatement.m_bDefinesDone, ref flag3, ref this.m_implicitRSList, bLOBArrayFetchRequired);
									num4 += this.VerifyExecution(connectionImpl, out num, true, ref exceptionForArrayBindDML, out bHasMoreRowsInDB, true);
								}
							}
						}
					}
					lock (this.m_lockCancel)
					{
						this.m_bServerExecutionComplete = true;
						if (this.m_executionId >= 9223372036854775807L)
						{
							this.m_executionId = 0L;
						}
						else
						{
							this.m_executionId += 1L;
						}
					}
					if (connectionImpl.m_oracleCommunication.InBreakResetMode())
					{
						connectionImpl.m_marshallingEngine.ProcessReset();
					}
					bDisableCompressedFetch = ((connectionImpl.m_marshallingEngine.TTCErrorObject.Flags & 32) != 0);
					if (cachedStatement != null)
					{
						cachedStatement.m_numRowsFetchArrayCanAccomodate = (long)this.m_rowsToFetch;
						cachedStatement.m_bDisableCompressedFetch = bDisableCompressedFetch;
					}
					if (this.m_sqlMetaData != null && this.m_sqlMetaData.m_maxRowSize == 0)
					{
						this.m_sqlMetaData.CalculateRowSize();
					}
					if (orclDependencyImpl != null)
					{
						orclDependencyImpl.m_bIsEnabled = orclDependencyImpl.m_bIsRegistered;
						if (orclDependencyImpl.m_bQueryBasedNTFN && !orclDependencyImpl.m_queryIDList.Contains(num5))
						{
							lock (orclDependencyImpl.m_syncList)
							{
								if (!orclDependencyImpl.m_queryIDList.Contains(num5))
								{
									orclDependencyImpl.m_queryIDList.Add(num5);
								}
							}
						}
					}
					if (executeSqlObject.m_bSessionTimeZoneUpdated)
					{
						connectionImpl.m_sessionTimeZone = new OracleIntervalDS(executeSqlObject.m_sessionTimeZone);
						executeSqlObject.m_bSessionTimeZoneUpdated = false;
					}
					this.m_sessionTimeZone = connectionImpl.m_sessionTimeZone;
					if (this.m_sqlMetaData != null)
					{
						this.m_sqlMetaData.pCommandText = commandText;
						if (!flag2)
						{
							if (this.m_sqlStatementType == SqlStatementType.SELECT && (this.m_sqlMetaData.m_maxRowSize > 0 || this.m_sqlMetaData.m_bHasLOBOrLongColumn || this.m_sqlMetaData.m_bHasBFILEColumn))
							{
								int num7 = this.m_sqlMetaData.m_maxRowSize + this.m_sqlMetaData.m_numOfLOBColumns * Math.Max(86, 86 + (int)clientInitialLOBFS) + this.m_sqlMetaData.m_numOfLONGColumns * Math.Max(2, longFetchSize) + this.m_sqlMetaData.m_numOfBFileColumns * 86;
								this.m_rowsToFetch = (int)this.m_fetchSize / num7;
								this.m_rowsToFetch++;
								if (this.m_rowsToFetch > 65535)
								{
									this.m_rowsToFetch = 65535;
								}
							}
							sqlmetaData = this.m_sqlMetaData;
						}
					}
					this.m_bindDirectionsFromServer = @null.m_bindDirections;
					if (exceptionForArrayBindDML != null)
					{
						return num4;
					}
					if ((flag4 == null || flag4 == false) && (this.m_sqlStatementType == SqlStatementType.DML || this.m_sqlStatementType == SqlStatementType.SELECT || this.m_sqlStatementType == SqlStatementType.PLSQL) && !flag && connectionImpl.m_statementCache != null && this.m_addToStatementCache)
					{
						cachedStatement = new CachedStatement();
						cachedStatement.sqlInfo = new SQLInfo
						{
							m_SQLcommandTextByteStream = this.m_commandTextByteStream,
							m_SQLhasReturningClause = this.m_bHasReturningClause,
							m_SQLStatementType = this.m_sqlStatementType
						};
						cachedStatement.statementdata = sqlmetaData;
						cachedStatement.m_cursorId = num;
						cachedStatement.m_bDisableCompressedFetch = bDisableCompressedFetch;
						cachedStatement.m_accessors = array;
						cachedStatement.m_numRowsFetchArrayCanAccomodate = (long)this.m_rowsToFetch;
						cachedStatement.m_bindAccessors = this.m_bindAccessors;
						cachedStatement.m_bindParamMetadata = @null.m_paramCollInfoArray;
						cachedStatement.m_scnFromExecution = scnFromExecution;
						cachedStatement.m_placeHolderCollection = arrayList;
						cachedStatement.m_bBindParamPresent = bBindParamPresent;
						cachedStatement.m_dataUnmarshaller = dataUnmarshaller;
						cachedStatement.m_bindDirections = @null.m_bindDirections;
						cachedStatement.m_bAllInBinds = @null.m_bAllInBinds;
						cachedStatement.m_bAllOutBinds = @null.m_bAllOutBinds;
						if (cachedStatement.statementdata != null && adrianParsedStmt != null)
						{
							foreach (OracleLpStatement oracleLpStatement in adrianParsedStmt)
							{
								oracleLpStatement.m_vODPContext = null;
							}
							cachedStatement.statementdata.parsedStmt = adrianParsedStmt;
						}
					}
				}
				finally
				{
					if (num6 > 0)
					{
						connectionImpl.m_connectionFreeToUseEvent.Set();
					}
					else if (flag)
					{
						CachedStatement cachedStatement2 = connectionImpl.m_statementCache.Put(commandText, cachedStatement);
						if (cachedStatement2 != null)
						{
							connectionImpl.AddCursorIdToBeClosed((long)cachedStatement2.m_cursorId);
						}
					}
				}
				if (this.m_bHasReturningClause && flag3)
				{
					throw new OracleException(24369, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(24369, new string[0]));
				}
				if (cachedStatement != null && connectionImpl.m_statementCache != null && this.m_addToStatementCache && connectionImpl.m_pm != null && connectionImpl.m_pm.m_bSelfTuning && !OracleTuner.Instance.HighMemoryUsageAlert)
				{
					connectionImpl.AcceptStatementData(commandText);
				}
				if (this.m_sqlStatementType == SqlStatementType.SELECT)
				{
					bool bInitialLongFetchSizeModified = false;
					if (!flag)
					{
						if (this.m_sqlMetaData != null && longFetchSize > 0 && this.m_sqlMetaData.IsInitialLongFetchSizeInChars)
						{
							long num8 = (long)(longFetchSize * connectionImpl.m_marshallingEngine.m_dbCharSetConv.MaxBytesPerChar);
							if (num8 < 2147483647L)
							{
								longFetchSize = (int)num8;
							}
							else
							{
								longFetchSize = int.MaxValue;
							}
							bInitialLongFetchSizeModified = true;
						}
						if (cachedStatement != null)
						{
							cachedStatement.m_longFetchSize = longFetchSize;
						}
					}
					rdrImpl = connectionImpl.GetInitializedDataReaderImpl(array, this.m_sqlMetaData, num, noOfRowsFetched, cachedStatement, this.m_sessionTimeZone, (long)longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, this.m_addRowidDoneImplicitly, bInitialLongFetchSizeModified);
					if (dataUnmarshaller != null)
					{
						rdrImpl.m_dataUnmarshaller = dataUnmarshaller;
						if (rdrImpl.m_dataUnmarshaller.m_charArrayForConversion == null)
						{
							rdrImpl.m_dataUnmarshaller.m_charArrayForConversion = connectionImpl.m_marshallingEngine.m_charArrayPooler.Dequeue();
						}
					}
					rdrImpl.m_bHasMoreRowsInDB = bHasMoreRowsInDB;
					rdrImpl.m_oraBufReleaseInfoList = oraBufReleaseInfoList;
				}
				else if (cachedStatement != null && connectionImpl.m_statementCache != null)
				{
					CachedStatement cachedStatement3 = connectionImpl.m_statementCache.Put(commandText, cachedStatement);
					if (cachedStatement3 != null)
					{
						connectionImpl.AddCursorIdToBeClosed((long)cachedStatement3.m_cursorId);
					}
				}
				else
				{
					connectionImpl.AddCursorIdToBeClosed((long)num);
				}
				result = num4;
			}
			catch (Exception ex)
			{
				if (bindByPositionParamColl != null)
				{
					foreach (object obj in bindByPositionParamColl)
					{
						OracleParameter oracleParameter = (OracleParameter)obj;
						oracleParameter.PreBindFree();
					}
				}
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				if (ex is OracleException)
				{
					connectionImpl.m_lastErrorNum = ((OracleException)ex).Number;
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
			return result;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000AD6E4 File Offset: 0x000AB8E4
		internal OracleDataReaderImpl GetReaderImplWithSchemaOnly(OracleConnectionImpl connectionImpl, CommandType commandType, string commandText, bool? gotSqlMetadata, SQLMetaData sqlMetaData)
		{
			SQLInfo sqlinfo = null;
			int cursorId = 0;
			this.m_sqlMetaData = sqlMetaData;
			if (gotSqlMetadata == null)
			{
				if (connectionImpl.m_statementCache != null && connectionImpl.m_statementCache.PeekForSQLMetaInfo(commandText, out sqlinfo, out this.m_sqlMetaData) && this.m_sqlMetaData.bGotDescribeInfoFromDB)
				{
					gotSqlMetadata = new bool?(true);
				}
				else
				{
					gotSqlMetadata = new bool?(false);
				}
			}
			if (gotSqlMetadata == false)
			{
				this.m_commandTextByteStream = connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(commandText, 0, commandText.Length, true);
				this.m_sqlMetaData = new SQLMetaData();
				try
				{
					connectionImpl.WaitForConnectionForExecution(null);
					TTCDescribe ttcdescribe = new TTCDescribe(connectionImpl.m_marshallingEngine);
					ttcdescribe.WriteMessage(this.m_commandTextByteStream);
					ttcdescribe.ReadMessage(this.m_sqlMetaData);
					bool flag = false;
					OracleException ex = null;
					this.VerifyExecution(connectionImpl, out cursorId, true, ref ex, out flag, true);
					this.m_sessionTimeZone = connectionImpl.m_sessionTimeZone;
					if (this.m_sqlMetaData != null)
					{
						this.m_sqlMetaData.pCommandText = commandText;
					}
				}
				finally
				{
					connectionImpl.m_connectionFreeToUseEvent.Set();
				}
			}
			return connectionImpl.GetInitializedDataReaderImpl(null, this.m_sqlMetaData, cursorId, 0, null, this.m_sessionTimeZone, 0L, 0L, 0L, null, this.m_addRowidDoneImplicitly, false);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000AD834 File Offset: 0x000ABA34
		private void ProcessParameters(OracleParameterCollection paramColl, OracleConnectionImpl connectionImpl, ColumnDescribeInfo[] cachedParamMetadata, ref bool bBindMetadataModified, bool isEFSelectStatement, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindValuesHelper)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.InitializeParamInfo(paramColl, connectionImpl, cachedParamMetadata, ref bBindMetadataModified, isEFSelectStatement, ref marshalBindValuesHelper);
				BindDirection[] bindDirections = marshalBindValuesHelper.m_bindDirections;
				ColumnDescribeInfo[] paramCollInfoArray = marshalBindValuesHelper.m_paramCollInfoArray;
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Prm, new string[]
					{
						"Parameters Count = " + paramColl.Count
					});
				}
				int num = 0;
				int num2 = 0;
				foreach (object obj in paramColl)
				{
					OracleParameter oracleParameter = (OracleParameter)obj;
					if (oracleParameter.m_bDuplicateBind)
					{
						num++;
					}
					else if (bindDirections != null && bindDirections[num2] == BindDirection.Input)
					{
						num++;
						num2++;
					}
					else
					{
						if (oracleParameter.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
						{
							if (this.m_bindAccessors[num2] == null || bBindMetadataModified)
							{
								this.m_bindAccessors[num2] = new TTCPLSQLAssociativeArrayAccessor(paramCollInfoArray[num], connectionImpl.m_marshallingEngine);
							}
							else
							{
								this.m_bindAccessors[num2].Initialize(paramCollInfoArray[num], connectionImpl.m_marshallingEngine, true);
							}
						}
						else
						{
							if (this.m_bindAccessors[num2] == null || bBindMetadataModified)
							{
								this.m_bindAccessors[num2] = Accessor.CreateAccessorForBind(connectionImpl.m_marshallingEngine, paramCollInfoArray[num], this.m_sqlStatementType, 0);
							}
							else
							{
								this.m_bindAccessors[num2].Initialize(paramCollInfoArray[num], connectionImpl.m_marshallingEngine, true);
								this.m_bindAccessors[num2].m_statementType = this.m_sqlStatementType;
							}
							if (this.m_bHasReturningClause)
							{
								this.m_bindAccessors[num2].m_bForReturningParameter = true;
							}
						}
						if (ProviderConfig.m_bTraceLevelPublic)
						{
							string text = oracleParameter.m_paramName;
							if (string.IsNullOrEmpty(text))
							{
								text = (num + 1).ToString();
							}
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.Append("(Name/Position = " + text + ") ");
							stringBuilder.Append("(OracleDbType = " + oracleParameter.OracleDbType.ToString() + ") ");
							stringBuilder.Append("(Direction = " + oracleParameter.Direction.ToString() + ") ");
							stringBuilder.Append("(Size (In Bytes) = " + oracleParameter.m_maxBytesToBeWrittenOrRead + ") ");
							stringBuilder.Append("(Array Bind Count = " + oracleParameter.m_bindElemCnt + ") ");
							if (oracleParameter.m_oraDbType == OracleDbType.Decimal)
							{
								stringBuilder.Append("(Precision = " + oracleParameter.Precision + ") ");
								stringBuilder.Append("(Scale = " + oracleParameter.Scale + ") ");
							}
							Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Prm, new string[]
							{
								stringBuilder.ToString()
							});
						}
						num++;
						num2++;
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

		// Token: 0x0600104B RID: 4171 RVA: 0x000ADBA8 File Offset: 0x000ABDA8
		private ICollection InitializeParamInfo(ICollection paramColl, OracleConnectionImpl connectionImpl, ColumnDescribeInfo[] cachedParamMetadata, ref bool bMetadataModified, bool isEFSelectStatement, ref TTCExecuteSql.MarshalBindParameterValueHelper marshalBindValuesHelper)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			ColumnDescribeInfo[] array = null;
			object[] array2 = null;
			int num = 0;
			if (marshalBindValuesHelper.m_indexOfLongParamsWithLargeData != null)
			{
				marshalBindValuesHelper.m_indexOfLongParamsWithLargeData.Clear();
			}
			else
			{
				marshalBindValuesHelper.m_indexOfLongParamsWithLargeData = new List<int>();
			}
			try
			{
				int count;
				if (paramColl != null && (count = paramColl.Count) > 0)
				{
					bool flag = true;
					if (cachedParamMetadata != null)
					{
						if (cachedParamMetadata.Length == count)
						{
							array = cachedParamMetadata;
							flag = false;
						}
						else
						{
							bMetadataModified = true;
						}
					}
					if (flag)
					{
						array = new ColumnDescribeInfo[count];
					}
					array2 = new object[count];
					using (IEnumerator enumerator = paramColl.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							OracleParameter oracleParameter = (OracleParameter)obj;
							ColumnDescribeInfo columnDescribeInfo;
							object obj2;
							oracleParameter.PreBind(connectionImpl, (cachedParamMetadata != null && cachedParamMetadata.Length > num) ? cachedParamMetadata[num] : null, ref bMetadataModified, this.m_arrayBindCount, out columnDescribeInfo, out obj2, isEFSelectStatement, this.m_sqlStatementType);
							array[num] = columnDescribeInfo;
							if (oracleParameter.Direction == ParameterDirection.Input || oracleParameter.Direction == ParameterDirection.InputOutput)
							{
								array2[num] = obj2;
								if (this.m_sqlStatementType != SqlStatementType.PLSQL)
								{
									long num2 = connectionImpl.m_b32kTypeSupported ? 32767L : 4000L;
									OraType dataType = (OraType)columnDescribeInfo.m_dataType;
									bool flag2 = dataType == OraType.ORA_LONG || dataType == OraType.ORA_LONGRAW || dataType == OraType.ORA_CHAR || dataType == OraType.ORA_CHARN || dataType == OraType.ORA_RAW;
									if (flag2 && (long)columnDescribeInfo.m_maxLength > num2)
									{
										marshalBindValuesHelper.m_indexOfLongParamsWithLargeData.Add(num);
									}
								}
							}
							num++;
						}
						goto IL_17F;
					}
				}
				bMetadataModified = false;
				IL_17F:
				marshalBindValuesHelper.m_paramValueArray = array2;
				marshalBindValuesHelper.m_paramCollInfoArray = array;
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
			return paramColl;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000ADDC4 File Offset: 0x000ABFC4
		internal static SqlStatementType GetSqlStatementTypeAdrianParser(OracleConnection conn, string cmdText, ref IEnumerable<OracleLpStatement> parsedStmt, ref bool? bIsDefineInSelect, ref bool exceptionWhileUsingAdrianParsing)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					" (SQL) : " + cmdText
				});
			}
			SqlStatementType result = SqlStatementType.OTHERS;
			try
			{
				string text = cmdText;
				if (cmdText != null)
				{
					string text2 = cmdText.TrimEnd(new char[0]);
					if (!text2.EndsWith(";"))
					{
						text = cmdText + ";";
					}
				}
				if (parsedStmt == null)
				{
					try
					{
						parsedStmt = OracleConnection.OracleLpParser.ParseStatements(conn, text);
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPublic)
						{
							string text3 = cmdText.Replace(OracleCommandImpl.s_replaceString, string.Empty);
							string text4 = ex.ToString().Replace(OracleCommandImpl.s_replaceString, string.Empty);
							Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
							{
								string.Concat(new string[]
								{
									"(LOCALPARSER) (ERROR:",
									text4,
									") \n(SQL:",
									text3,
									")"
								})
							});
						}
					}
				}
				if (parsedStmt == null)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
						{
							"OracleLpParser.ParseStatements() returned null for " + text
						});
					}
					throw new NotSupportedException("OracleLpParser.ParseStatements() returned null for " + text);
				}
				using (IEnumerator<OracleLpStatement> enumerator = parsedStmt.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						OracleLpStatement oracleLpStatement = enumerator.Current;
						if (oracleLpStatement.StatementType == OracleLpStatementType.Select)
						{
							result = SqlStatementType.SELECT;
							if (bIsDefineInSelect == null || !oracleLpStatement.HasBindParameters)
							{
								goto IL_22F;
							}
							using (List<OracleLpBindParameter>.Enumerator enumerator2 = oracleLpStatement.BindParameters.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									OracleLpBindParameter oracleLpBindParameter = enumerator2.Current;
									if (oracleLpBindParameter.ParentClause == OracleLpStatementClauseType.SelectList)
									{
										bIsDefineInSelect = new bool?(true);
										break;
									}
								}
								goto IL_22F;
							}
						}
						if (oracleLpStatement.StatementType == OracleLpStatementType.Update || oracleLpStatement.StatementType == OracleLpStatementType.Delete || oracleLpStatement.StatementType == OracleLpStatementType.Merge || oracleLpStatement.StatementType == OracleLpStatementType.Insert)
						{
							result = SqlStatementType.DML;
						}
						else if (oracleLpStatement.StatementType == OracleLpStatementType.BlockStatement || oracleLpStatement.StatementType == OracleLpStatementType.Call || oracleLpStatement.StatementType == OracleLpStatementType.Execute)
						{
							result = SqlStatementType.PLSQL;
						}
						else
						{
							result = SqlStatementType.OTHERS;
						}
					}
					IL_22F:;
				}
			}
			catch (Exception ex2)
			{
				exceptionWhileUsingAdrianParsing = true;
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex2, null);
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

		// Token: 0x0600104D RID: 4173 RVA: 0x000AE0C0 File Offset: 0x000AC2C0
		internal static SqlStatementType GetSqlStatementType(string cmdText, ref bool? bIsDefineInSelect)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			SqlStatementType result;
			try
			{
				SqlStatementType sqlStatementType;
				if (cmdText.StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("WITH", StringComparison.InvariantCultureIgnoreCase))
				{
					sqlStatementType = SqlStatementType.SELECT;
					if (bIsDefineInSelect != null)
					{
						int num = cmdText.IndexOf("select", StringComparison.OrdinalIgnoreCase);
						int num2 = cmdText.IndexOf("from", StringComparison.OrdinalIgnoreCase);
						while (num != -1)
						{
							if (num2 == -1)
							{
								break;
							}
							int num3 = cmdText.IndexOf(':');
							if (num3 > num && num3 < num2)
							{
								bIsDefineInSelect = new bool?(true);
								break;
							}
							num = cmdText.IndexOf("select", num2 + 3, StringComparison.OrdinalIgnoreCase);
							if (num != -1)
							{
								num2 = cmdText.IndexOf("from", num + 5, StringComparison.OrdinalIgnoreCase);
							}
						}
					}
				}
				else if (cmdText.StartsWith("INSERT", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("DELETE", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("UPDATE", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("MERGE", StringComparison.InvariantCultureIgnoreCase))
				{
					sqlStatementType = SqlStatementType.DML;
				}
				else if (cmdText.StartsWith("begin", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("BEGIN", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("declare", StringComparison.InvariantCultureIgnoreCase) || cmdText.StartsWith("DECLARE", StringComparison.InvariantCultureIgnoreCase))
				{
					sqlStatementType = SqlStatementType.PLSQL;
				}
				else
				{
					sqlStatementType = SqlStatementType.OTHERS;
				}
				result = sqlStatementType;
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

		// Token: 0x0600104E RID: 4174 RVA: 0x000AE268 File Offset: 0x000AC468
		private void CheckForReturningClauseAdrianParser(OracleConnection conn, SqlStatementType statementType, SQLInfo sqlInfo, CommandType commandType, ref IEnumerable<OracleLpStatement> parsedStmt, string originalCmdText, ref bool exceptionWhileUsingAdrianParsing)
		{
			try
			{
				this.m_bHasReturningClause = false;
				if (sqlInfo != null)
				{
					this.m_bHasReturningClause = sqlInfo.m_SQLhasReturningClause;
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
						{
							"Cmd Text (From Cache):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
						});
					}
				}
				else if (commandType == CommandType.StoredProcedure)
				{
					this.m_bHasReturningClause = false;
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
						{
							"Cmd Text (StoredProcedure):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
						});
					}
				}
				else if (commandType == CommandType.TableDirect)
				{
					this.m_bHasReturningClause = false;
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
						{
							"Cmd Text (TableDirect):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
						});
					}
				}
				else
				{
					if (SqlStatementType.PLSQL == statementType || string.IsNullOrEmpty(originalCmdText))
					{
						this.m_bHasReturningClause = false;
					}
					else
					{
						if (parsedStmt == null)
						{
							string text = originalCmdText;
							text = text.TrimEnd(new char[0]);
							if (!text.EndsWith(";"))
							{
								text += ";";
							}
							try
							{
								parsedStmt = OracleConnection.OracleLpParser.ParseStatements(conn, text);
							}
							catch (Exception ex)
							{
								if (ProviderConfig.m_bTraceLevelPublic)
								{
									string text2 = text.Replace(OracleCommandImpl.s_replaceString, string.Empty);
									string text3 = ex.ToString().Replace(OracleCommandImpl.s_replaceString, string.Empty);
									Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
									{
										string.Concat(new string[]
										{
											"(LOCALPARSER) (ERROR:",
											text3,
											") (SQL:",
											text2,
											")"
										})
									});
								}
							}
						}
						if (parsedStmt == null)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									"OracleLpParser.ParseStatements() returned null for " + originalCmdText
								});
							}
							throw new NotSupportedException("OracleLpParser.ParseStatements() returned null for " + originalCmdText);
						}
						foreach (OracleLpStatement oracleLpStatement in parsedStmt)
						{
							if (oracleLpStatement.HasReturningClause)
							{
								this.m_bHasReturningClause = true;
								break;
							}
						}
					}
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
						{
							"Cmd Text (SQL):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
						});
					}
				}
			}
			catch (Exception ex2)
			{
				exceptionWhileUsingAdrianParsing = true;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
					{
						"Error while parsing using Adrian Parser: " + ex2.ToString()
					});
				}
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000AE590 File Offset: 0x000AC790
		private void CheckForReturningClause(string cmdTextWithoutComments, SqlStatementType statementType, SQLInfo sqlInfo, CommandType commandType, OracleParameterCollection paramCollection, ArrayList placeholderCollection, bool bBindParamPresent, string originalCmdText)
		{
			if (sqlInfo != null)
			{
				this.m_bHasReturningClause = sqlInfo.m_SQLhasReturningClause;
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
					{
						"Cmd Text (From Cache):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
					});
					return;
				}
			}
			else if (commandType == CommandType.StoredProcedure)
			{
				this.m_bHasReturningClause = false;
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
					{
						"Cmd Text (StoredProcedure):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
					});
					return;
				}
			}
			else if (commandType == CommandType.TableDirect)
			{
				this.m_bHasReturningClause = false;
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
					{
						"Cmd Text (TableDirect):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
					});
					return;
				}
			}
			else
			{
				this.m_bHasReturningClause = OracleCommandImpl.HasReturningClause(cmdTextWithoutComments, statementType, paramCollection, placeholderCollection, bBindParamPresent);
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
					{
						"Cmd Text (SQL):  HasReturnClauseSearchKey : " + originalCmdText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
					});
				}
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000AE6C8 File Offset: 0x000AC8C8
		private static bool HasReturningClause(string cmdText, SqlStatementType stmtType, OracleParameterCollection paramCollection, ArrayList placeholderCollection, bool bBindParamPresent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				bool flag = false;
				if (SqlStatementType.PLSQL == stmtType || cmdText == null)
				{
					flag = false;
				}
				else
				{
					try
					{
						if ((paramCollection != null && paramCollection.Count != 0) || (placeholderCollection != null && placeholderCollection.Count != 0) || bBindParamPresent)
						{
							string pattern = "(?i)\\s+(RETURN|RETURNING)\\s+.+\\s+INTO\\s+:+";
							Regex regex = new Regex(pattern);
							MatchCollection matchCollection = regex.Matches(cmdText);
							if (matchCollection.Count != 0)
							{
								Match match = matchCollection[matchCollection.Count - 1];
								int num = cmdText.IndexOf('\'', match.Index + match.Length);
								if (num == -1)
								{
									flag = true;
								}
							}
						}
					}
					catch
					{
						if (ProviderConfig.m_bTraceLevelPublic)
						{
							Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
							{
								"Error while trying to search for returning clause. Trying another search pattern"
							});
						}
						flag = Regex.IsMatch(cmdText, "\\bRETURNING\\b | \\bRETURN\\b ", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
				}
				result = flag;
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

		// Token: 0x06001051 RID: 4177 RVA: 0x000AE7FC File Offset: 0x000AC9FC
		internal void ExtractAccessorValuesIntoParam(OracleParameterCollection paramColl, OracleConnection connection, int paramCount, string commandText, long longFetchSize, long clientInitialLOBFS, long internalInitialLOBFS, long[] scnFromExecution, bool bCallFromExecuteReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int num = 0;
			char[] array = null;
			try
			{
				for (int i = 0; i < paramCount; i++)
				{
					OracleParameter oracleParameter = paramColl[i];
					if (oracleParameter.OracleDbType == OracleDbType.RefCursor)
					{
						if (this.m_bBindByName)
						{
							oracleParameter.m_paramPosOrName = oracleParameter.ParameterName;
						}
						else
						{
							oracleParameter.m_paramPosOrName = i.ToString();
						}
					}
					if (!oracleParameter.m_bDuplicateBind)
					{
						if (this.m_bindDirectionsFromServer[num] != BindDirection.Input)
						{
							Accessor accessor = this.m_bindAccessors[num];
							if (this.m_bHasReturningClause && ParameterDirection.InputOutput == oracleParameter.m_direction && !accessor.m_bReceivedOutValueFromServer)
							{
								num++;
								goto IL_28F;
							}
							switch (oracleParameter.OracleDbType)
							{
							case OracleDbType.BFile:
							case OracleDbType.Blob:
							case OracleDbType.Clob:
							case OracleDbType.NClob:
								oracleParameter.PostBind_Lob(connection, accessor);
								break;
							case OracleDbType.Byte:
							case OracleDbType.Int16:
							case OracleDbType.Int32:
								oracleParameter.PostBind_Int32(accessor);
								break;
							case OracleDbType.Char:
							case OracleDbType.Long:
							case OracleDbType.NChar:
							case OracleDbType.NVarchar2:
							case OracleDbType.Varchar2:
								if (array == null)
								{
									array = connection.m_oracleConnectionImpl.m_marshallingEngine.m_charArrayPooler.Dequeue();
								}
								oracleParameter.PostBind_Char(connection.m_oracleConnectionImpl, accessor, array);
								break;
							case OracleDbType.Date:
								oracleParameter.PostBind_Date(accessor);
								break;
							case OracleDbType.Decimal:
								oracleParameter.PostBind_Decimal(accessor);
								break;
							case OracleDbType.Double:
								oracleParameter.PostBind_Double(accessor);
								break;
							case OracleDbType.LongRaw:
							case OracleDbType.Raw:
								oracleParameter.PostBind_Raw(accessor);
								break;
							case OracleDbType.Int64:
								oracleParameter.PostBind_Int64(accessor);
								break;
							case OracleDbType.IntervalDS:
								oracleParameter.PostBind_IntervalDS(this.m_bindAccessors[num]);
								break;
							case OracleDbType.IntervalYM:
								oracleParameter.PostBind_IntervalYM(accessor);
								break;
							case (OracleDbType)118:
							case (OracleDbType)128:
							case (OracleDbType)129:
							case (OracleDbType)130:
							case (OracleDbType)131:
								goto IL_257;
							case OracleDbType.RefCursor:
								oracleParameter.PostBind_RefCursor(connection, accessor, this.m_fetchSize, this.m_sessionTimeZone, commandText, oracleParameter.m_paramPosOrName, longFetchSize, clientInitialLOBFS, internalInitialLOBFS, scnFromExecution, bCallFromExecuteReader);
								break;
							case OracleDbType.Single:
								oracleParameter.PostBind_Single(accessor);
								break;
							case OracleDbType.TimeStamp:
								oracleParameter.PostBind_TimeStamp(accessor);
								break;
							case OracleDbType.TimeStampLTZ:
								oracleParameter.PostBind_TimeStampLTZ(connection.m_oracleConnectionImpl, accessor);
								break;
							case OracleDbType.TimeStampTZ:
								oracleParameter.PostBind_TimeStampTZ(connection.m_oracleConnectionImpl, accessor);
								break;
							case OracleDbType.XmlType:
								oracleParameter.PostBind_XmlType(connection, accessor);
								break;
							case OracleDbType.BinaryDouble:
								oracleParameter.PostBind_BinaryDouble(accessor);
								break;
							case OracleDbType.BinaryFloat:
								oracleParameter.PostBind_BinaryFloat(accessor);
								break;
							case OracleDbType.Boolean:
								oracleParameter.PostBind_Boolean(accessor);
								break;
							default:
								goto IL_257;
							}
							IL_263:
							accessor.Initialize();
							goto IL_28B;
							IL_257:
							oracleParameter.Value = accessor.GetValue();
							goto IL_263;
						}
						else
						{
							OracleDbType oracleDbType = oracleParameter.OracleDbType;
							if (oracleDbType == OracleDbType.Blob || oracleDbType == OracleDbType.Clob || oracleDbType == OracleDbType.NClob)
							{
								oracleParameter.PreBindFree();
							}
						}
						IL_28B:
						num++;
					}
					IL_28F:;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (array != null)
				{
					connection.m_oracleConnectionImpl.m_marshallingEngine.m_charArrayPooler.Enqueue(array);
				}
				if (connection.m_oracleConnectionImpl.m_marshallingEngine.m_oraBufRdr != null)
				{
					connection.m_oracleConnectionImpl.m_marshallingEngine.m_oraBufRdr.FreeTempOBList();
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000AEB4C File Offset: 0x000ACD4C
		internal OracleClob ExtractXMLValuesIntoParam(ref OracleParameterCollection paramColl, OracleConnection connection, int paramIndex, bool wantResult, bool transform, bool isOracle8i, OracleXmlQueryProperties xmlQueryProperties)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			char[] array = null;
			OracleParameter oracleParameter = null;
			OracleParameter oracleParameter2 = null;
			OracleParameter oracleParameter3 = null;
			OracleClob result;
			try
			{
				if (transform)
				{
					oracleParameter2 = paramColl[paramIndex];
					paramIndex++;
				}
				if (isOracle8i)
				{
					oracleParameter3 = paramColl[paramIndex];
					paramIndex++;
				}
				if (wantResult)
				{
					oracleParameter = paramColl[paramIndex];
					Accessor bindAccessor = this.m_bindAccessors[paramIndex];
					try
					{
						oracleParameter.PostBind_Lob(connection, bindAccessor);
					}
					catch (Exception)
					{
						oracleParameter.PreBindFree();
						if (transform)
						{
							oracleParameter2 = paramColl[paramIndex];
							paramIndex++;
						}
						if (isOracle8i)
						{
							oracleParameter3 = paramColl[paramIndex];
							paramIndex++;
						}
						throw;
					}
					paramIndex++;
				}
				if (wantResult)
				{
					string fullName = oracleParameter.Value.GetType().FullName;
					if (fullName.Equals("Oracle.ManagedDataAccess.Types.OracleClob") && !((OracleClob)oracleParameter.Value).IsNull)
					{
						OracleClob oracleClob = (OracleClob)oracleParameter.Value;
						result = oracleClob;
					}
					else
					{
						string text;
						if (isOracle8i)
						{
							text = "<?xml version = '1.0'?>\n";
						}
						else
						{
							text = "<?xml version = \"1.0\"?>\n";
						}
						if (xmlQueryProperties.RootTag != null && xmlQueryProperties.RootTag.Length != 0)
						{
							text = text + "<" + xmlQueryProperties.RootTag + "/>\n";
						}
						OracleClob oracleClob2 = new OracleClob(connection);
						oracleClob2.Append(text.ToCharArray(), 0, text.Length);
						result = oracleClob2;
					}
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (oracleParameter != null)
				{
					paramColl.Remove(oracleParameter);
				}
				if (oracleParameter2 != null)
				{
					paramColl.Remove(oracleParameter2);
				}
				if (oracleParameter3 != null)
				{
					paramColl.Remove(oracleParameter3);
				}
				if (array != null)
				{
					connection.m_oracleConnectionImpl.m_marshallingEngine.m_charArrayPooler.Enqueue(array);
				}
				if (connection.m_oracleConnectionImpl.m_marshallingEngine.m_oraBufRdr != null)
				{
					connection.m_oracleConnectionImpl.m_marshallingEngine.m_oraBufRdr.FreeTempOBList();
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000AED98 File Offset: 0x000ACF98
		internal int ExtractXMLSaveValuesIntoParam(ref OracleParameterCollection paramColl, bool transform)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int num = 0;
			OracleParameter oracleParameter = null;
			OracleParameter oracleParameter2 = null;
			OracleParameter oracleParameter3 = null;
			OracleParameter oracleParameter4 = null;
			int result;
			try
			{
				oracleParameter = paramColl[num];
				num++;
				if (transform)
				{
					oracleParameter4 = paramColl[num];
					num++;
				}
				oracleParameter2 = paramColl[num];
				Accessor bindAccessor = this.m_bindAccessors[num];
				try
				{
					oracleParameter2.PostBind_Int32(bindAccessor);
				}
				catch (Exception)
				{
					oracleParameter2.PreBindFree();
					num++;
					throw;
				}
				num++;
				oracleParameter3 = paramColl[num];
				result = (int)oracleParameter2.Value;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (oracleParameter != null)
				{
					paramColl.Remove(oracleParameter);
				}
				if (oracleParameter2 != null)
				{
					paramColl.Remove(oracleParameter2);
				}
				if (oracleParameter3 != null)
				{
					paramColl.Remove(oracleParameter3);
				}
				if (oracleParameter4 != null)
				{
					paramColl.Remove(oracleParameter4);
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000AEEB4 File Offset: 0x000AD0B4
		internal void RetrieveMetadata(string commandText, CommandType commandType, OracleParameterCollection paramColl, OracleConnectionImpl connectionImpl, OracleConnection con, out SQLMetaData sqlMetadata, out int hiddenColumnCount)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			sqlMetadata = null;
			hiddenColumnCount = 0;
			if (commandType != CommandType.Text && commandType != CommandType.TableDirect)
			{
				return;
			}
			this.m_sqlMetaData = null;
			bool flag = false;
			bool? flag2 = new bool?(false);
			bool flag3 = false;
			string text = commandText;
			IEnumerable<OracleLpStatement> enumerable = null;
			bool flag4 = false;
			try
			{
				SQLInfo sqlinfo = null;
				if (connectionImpl.m_statementCache != null && connectionImpl.m_statementCache.PeekForSQLMetaInfo(commandText, out sqlinfo, out sqlMetadata) && sqlMetadata.bGotDescribeInfoFromDB)
				{
					flag = true;
				}
				if (!flag)
				{
					if (commandType == CommandType.TableDirect)
					{
						this.m_sqlStatementType = SqlStatementType.SELECT;
					}
					else
					{
						OracleCommandImpl.TrimCommentsFromSQL(ref text);
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag4)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementTypeAdrianParser(con, text, ref enumerable, ref flag2, ref flag4);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag4)
						{
							this.m_sqlStatementType = OracleCommandImpl.GetSqlStatementType(text, ref flag2);
						}
						if (this.m_sqlStatementType != SqlStatementType.SELECT)
						{
							return;
						}
					}
					if (connectionImpl.m_statementCache != null)
					{
						OracleCommandImpl.ValidateStatementCacheSize(connectionImpl);
					}
					byte[] sqlStmtByteStream = connectionImpl.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(commandText, 0, commandText.Length, true);
					ArrayList arrayList = null;
					if (this.m_bBindByName)
					{
						arrayList = new ArrayList();
					}
					bool flag5 = true;
					if (commandType == CommandType.Text)
					{
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag4)
						{
							this.GetBindInfoUsingAdrianParser(con, text, ref flag5, ref arrayList, ref enumerable, ref flag4);
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag4)
						{
							this.ParseCommandText(text, ref flag5, ref arrayList);
						}
					}
					OracleParameterCollection oracleParameterCollection;
					if (this.m_bBindByName && paramColl != null && paramColl.Count > 0)
					{
						if (commandType == CommandType.StoredProcedure)
						{
							oracleParameterCollection = this.ReorderBindByNameBasedParameterCollectionForStoredProcedure(paramColl);
						}
						else
						{
							oracleParameterCollection = this.GetBindByPositionBasedParameterCollection(paramColl, arrayList, false);
						}
					}
					else
					{
						oracleParameterCollection = paramColl;
					}
					bool flag6 = false;
					TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
					if (flag5 && oracleParameterCollection != null)
					{
						@null.m_sqlStmtType = this.m_sqlStatementType;
						if (this.m_bindAccessors == null || this.m_bindAccessors.Length != paramColl.Count)
						{
							this.m_bindAccessors = new Accessor[paramColl.Count];
						}
						this.ProcessParameters(oracleParameterCollection, connectionImpl, null, ref flag6, false, ref @null);
					}
					else
					{
						this.m_bindAccessors = null;
						this.m_bindDirectionsFromServer = null;
						this.m_arrayBindCount = 0;
					}
					if (ProviderConfig.m_bTraceLevelPublic)
					{
						Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
						{
							commandText
						});
					}
					try
					{
						connectionImpl.WaitForConnectionForExecution(null);
						connectionImpl.AddAllPiggyBackRequests();
						TTCExecuteSql executeSqlObject = connectionImpl.ExecuteSqlObject;
						long[] array = new long[2];
						List<TTCResultSet> list = null;
						if (commandType == CommandType.TableDirect)
						{
							this.m_bHasReturningClause = false;
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
								{
									"Cmd Text (TableDirect): " + commandText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
								});
							}
						}
						else
						{
							this.m_bHasReturningClause = OracleCommandImpl.HasReturningClause(text, this.m_sqlStatementType, paramColl, arrayList, flag5);
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[]
								{
									"Cmd Text (SQL): " + commandText + "\nHas Returning Clause: " + this.m_bHasReturningClause.ToString()
								});
							}
						}
						executeSqlObject.SendExecuteRequest(connectionImpl, sqlStmtByteStream, this.m_bHasReturningClause, 0, 0L, null, 0L, true, false, false, false, false, false, this.m_sqlStatementType, 0, this.m_arrayBindCount, array, ref @null, 0);
						int num = 0;
						long num2 = 0L;
						Accessor[] array2 = null;
						DataUnmarshaller dataUnmarshaller = null;
						if (@null.m_bindDirections == null && @null.m_paramValueArray != null)
						{
							@null.ResetBindDirections(paramColl.Count);
							@null.m_bAllInBinds = true;
						}
						executeSqlObject.ReceiveExecuteResponse(ref array2, this.m_bindAccessors, this.m_bHasReturningClause, ref sqlMetadata, this.m_sqlStatementType, -1L, 0, out num, ref num2, 0, 0L, array, @null.m_bAllInBinds, this.m_arrayBindCount, ref dataUnmarshaller, ref @null, out this.m_rowsAffectedPerBind, false, ref flag3, ref list, false);
						int num3 = 0;
						bool flag7 = false;
						OracleException ex = null;
						this.VerifyExecution(connectionImpl, out num3, true, ref ex, out flag7, true);
						this.m_sessionTimeZone = connectionImpl.m_sessionTimeZone;
						if (sqlMetadata != null)
						{
							sqlMetadata.pCommandText = commandText;
						}
						if (num3 != 0)
						{
							connectionImpl.AddCursorIdToBeClosed((long)num3);
						}
					}
					finally
					{
						connectionImpl.m_connectionFreeToUseEvent.Set();
					}
				}
				this.m_sqlMetaData = sqlMetadata;
				hiddenColumnCount = OracleDataReaderImpl.CountHiddenColumns(this.m_addRowidDoneImplicitly);
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex2, null);
				if (ex2 is OracleException)
				{
					connectionImpl.m_lastErrorNum = ((OracleException)ex2).Number;
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
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000AF34C File Offset: 0x000AD54C
		internal void GetBindInfoUsingAdrianParser(OracleConnection conn, string commandText, ref bool bBindParamPresent, ref ArrayList placeHolderCollection, ref IEnumerable<OracleLpStatement> parsedStmt, ref bool exceptionWhileUsingAdrianParsing)
		{
			try
			{
				bBindParamPresent = false;
				if (parsedStmt == null)
				{
					if (string.IsNullOrEmpty(commandText))
					{
						return;
					}
					commandText = commandText.TrimEnd(new char[0]);
					if (!commandText.EndsWith(";"))
					{
						commandText += ";";
					}
					try
					{
						parsedStmt = OracleConnection.OracleLpParser.ParseStatements(conn, commandText);
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPublic)
						{
							string text = commandText.Replace(OracleCommandImpl.s_replaceString, string.Empty);
							string text2 = ex.ToString().Replace(OracleCommandImpl.s_replaceString, string.Empty);
							Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
							{
								string.Concat(new string[]
								{
									"(LOCALPARSER) (ERROR:",
									text2,
									") \n(SQL:",
									text,
									")"
								})
							});
						}
					}
				}
				if (parsedStmt == null)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
						{
							"OracleLpParser.ParseStatements() returned null for " + commandText
						});
					}
					bBindParamPresent = false;
					throw new NotSupportedException("OracleLpParser.ParseStatements() returned null for " + commandText);
				}
				foreach (OracleLpStatement oracleLpStatement in parsedStmt)
				{
					if (oracleLpStatement.HasBindParameters)
					{
						bBindParamPresent = true;
						if (placeHolderCollection == null)
						{
							break;
						}
						foreach (OracleLpBindParameter oracleLpBindParameter in oracleLpStatement.BindParameters)
						{
							placeHolderCollection.Add(oracleLpBindParameter.Name.ToString());
						}
					}
				}
			}
			catch (Exception ex2)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
					{
						"Error while parsing using Adrian Parser: " + ex2.ToString()
					});
				}
				exceptionWhileUsingAdrianParsing = true;
				if (placeHolderCollection != null)
				{
					placeHolderCollection.Clear();
				}
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000AF59C File Offset: 0x000AD79C
		internal void ParseCommandText(string commandText, ref bool bBindParamPresent, ref ArrayList placeHolderCollection)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int i = 0;
			bBindParamPresent = false;
			try
			{
				int length = commandText.Length;
				while (i < length)
				{
					char c = commandText[i];
					if (c == '\'')
					{
						i++;
						while (i < length && commandText[i] != '\'')
						{
							i++;
						}
						if (i >= length)
						{
							break;
						}
						c = commandText[i];
					}
					if (c == '"')
					{
						i++;
						while (i < length && commandText[i] != '"')
						{
							i++;
						}
						if (i >= length)
						{
							break;
						}
						c = commandText[i];
					}
					int num = length - 1;
					if (c == '/' && i < num && commandText[i + 1] == '*')
					{
						for (i += 2; i < length; i++)
						{
							if (i >= num || commandText[i] == '*' || commandText[i + 1] == '/')
							{
								i += 2;
								break;
							}
						}
						if (i >= length)
						{
							break;
						}
						c = commandText[i];
					}
					if (c == '-' && i < num && commandText[i + 1] == '-')
					{
						i += 2;
						while (i < length && commandText[i++] != '\n')
						{
						}
						if (i >= length)
						{
							break;
						}
						c = commandText[i];
					}
					if (c == ':')
					{
						i++;
						while (i < length && commandText[i] == ' ')
						{
							i++;
						}
						if (i >= length)
						{
							break;
						}
						c = commandText[i];
						if (i + 3 < length && commandText[i + 3] == '.' && (((c == 'N' || c == 'n') && (c == 'E' || c == 'e') && (c == 'W' || c == 'w')) || ((c == 'O' || c == 'o') && (c == 'L' || c == 'l') && (c == 'D' || c == 'd'))))
						{
							continue;
						}
						if (c != '=')
						{
							bBindParamPresent = true;
							if (placeHolderCollection == null)
							{
								break;
							}
							placeHolderCollection.Add(this.GetPlaceHolderName(commandText, i, length));
						}
					}
					i++;
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

		// Token: 0x06001057 RID: 4183 RVA: 0x000AF7D4 File Offset: 0x000AD9D4
		private string GetPlaceHolderName(string commandText, int index, int strLength)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = index;
				char c = commandText[num];
				while (char.IsLetterOrDigit(c) || c == '_' || c == '#' || c == '$')
				{
					stringBuilder.Append(c);
					if (num + 1 >= strLength)
					{
						break;
					}
					c = commandText[++num];
				}
				result = stringBuilder.ToString();
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

		// Token: 0x06001058 RID: 4184 RVA: 0x000AF890 File Offset: 0x000ADA90
		private OracleParameterCollection ReorderBindByNameBasedParameterCollectionForStoredProcedure(OracleParameterCollection orclParamColl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleParameterCollection result;
			try
			{
				OracleParameterCollection oracleParameterCollection = null;
				if (orclParamColl != null)
				{
					ArrayList arrayList = new ArrayList();
					OracleParameter oracleParameter = null;
					foreach (object obj in orclParamColl)
					{
						OracleParameter oracleParameter2 = (OracleParameter)obj;
						if (oracleParameter2.Direction == ParameterDirection.ReturnValue)
						{
							oracleParameter = oracleParameter2;
						}
						else
						{
							arrayList.Add(oracleParameter2);
						}
					}
					oracleParameterCollection = new OracleParameterCollection(arrayList);
					if (oracleParameter != null)
					{
						oracleParameter.m_collRef = null;
						oracleParameterCollection.Insert(0, oracleParameter);
					}
				}
				result = oracleParameterCollection;
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

		// Token: 0x06001059 RID: 4185 RVA: 0x000AF984 File Offset: 0x000ADB84
		internal OracleParameterCollection GetBindByPositionBasedParameterCollection(OracleParameterCollection orclParamColl, ArrayList placeHolderCollection, bool bXmlQuerySave = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			ArrayList arrayList = null;
			OracleParameterCollection result;
			try
			{
				if (placeHolderCollection != null && placeHolderCollection.Count > 0)
				{
					arrayList = new ArrayList();
					OracleParameter oracleParameter = null;
					foreach (object obj in placeHolderCollection)
					{
						string text = (string)obj;
						if (bXmlQuerySave)
						{
							int index;
							if ((index = orclParamColl.FindLastParamByName(text)) != -1)
							{
								oracleParameter = orclParamColl[index];
							}
						}
						else
						{
							oracleParameter = orclParamColl[text];
						}
						if (oracleParameter == null)
						{
							string name = ":" + text;
							if (bXmlQuerySave)
							{
								int index;
								if ((index = orclParamColl.FindLastParamByName(name)) != -1)
								{
									oracleParameter = orclParamColl[index];
								}
							}
							else
							{
								oracleParameter = orclParamColl[name];
							}
						}
						if (oracleParameter == null)
						{
							throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.PRM_INVALID_BIND, new string[0]), text);
						}
						if (!arrayList.Contains(oracleParameter))
						{
							arrayList.Add(oracleParameter);
						}
						else if (this.m_sqlStatementType != SqlStatementType.PLSQL)
						{
							OracleParameter oracleParameter2 = (OracleParameter)oracleParameter.Clone();
							oracleParameter2.DuplicateBind = true;
							arrayList.Add(oracleParameter2);
						}
					}
					result = new OracleParameterCollection(arrayList);
				}
				else
				{
					result = null;
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

		// Token: 0x0600105A RID: 4186 RVA: 0x000AFB38 File Offset: 0x000ADD38
		internal void CloseImplicitRefCursors(OracleConnectionImpl connectionImpl)
		{
			for (int i = 0; i < this.m_implicitRSList.Count; i++)
			{
				connectionImpl.AddCursorIdToBeClosed((long)this.m_implicitRSList[i].CursorId);
			}
			this.m_implicitRSList = null;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000AFB7C File Offset: 0x000ADD7C
		private void BuildXmlQueryCommandText(bool wantResult, string resultParamName, bool isOracle8i, string commandText, OracleParameterCollection parameters, OracleXmlCommandType xmlCommandType, OracleXmlQueryProperties xmlQueryProperties)
		{
			int num = 0;
			bool flag = false;
			string text = string.Empty;
			string text2 = string.Empty;
			this.m_pooledCmdText = commandText;
			StringBuilder stringBuilder = new StringBuilder(4096);
			if (xmlQueryProperties == null)
			{
				xmlQueryProperties = new OracleXmlQueryProperties();
			}
			if (xmlQueryProperties.Xslt != null && xmlQueryProperties.Xslt.Length != 0)
			{
				flag = true;
			}
			if (xmlQueryProperties.RootTag != null && xmlQueryProperties.RootTag.Length != 0)
			{
				text = xmlQueryProperties.RootTag;
			}
			if (xmlQueryProperties.RowTag != null && xmlQueryProperties.RowTag.Length != 0)
			{
				text2 = xmlQueryProperties.RowTag;
			}
			if (isOracle8i)
			{
				stringBuilder.Append("declare ");
				stringBuilder.Append("ctx DBMS_XMLQUERY.ctxType; ");
				if (!wantResult)
				{
					stringBuilder.Append("OracleResult CLOB; ");
				}
				stringBuilder.Append("begin ");
				stringBuilder.Append("ctx := DBMS_XMLQUERY.newContext(:OracleSqlQuery$); ");
				stringBuilder.Append("DBMS_XMLQUERY.setRaiseException(ctx, true); ");
				stringBuilder.Append("DBMS_XMLQUERY.setRowIdAttrName(ctx, ''); ");
				stringBuilder.Append("DBMS_XMLQUERY.setDateFormat(ctx, 'yyyy-MM-dd''T''HH:mm:ss.SSS'); ");
				stringBuilder.Append("DBMS_XMLQUERY.useTypeForCollElemTag(ctx); ");
				if (!text.Equals("ROWSET"))
				{
					stringBuilder.Append("DBMS_XMLQUERY.setRowsetTag(ctx, '");
					stringBuilder.Append(text);
					stringBuilder.Append("'); ");
				}
				if (!text2.Equals("ROW"))
				{
					stringBuilder.Append("DBMS_XMLQUERY.setRowTag(ctx, '");
					stringBuilder.Append(text2);
					stringBuilder.Append("'); ");
				}
				if (xmlQueryProperties.MaxRows > -1)
				{
					stringBuilder.Append("DBMS_XMLQUERY.setMaxRows(ctx, '");
					stringBuilder.Append(xmlQueryProperties.MaxRows.ToString());
					stringBuilder.Append("'); ");
				}
				if (parameters != null)
				{
					num = parameters.Count;
				}
				for (int i = 0; i < num; i++)
				{
					string text3 = parameters[i].ParameterName.Trim();
					stringBuilder.Append("DBMS_XMLQUERY.setBindValue(ctx, '");
					stringBuilder.Append(text3.Substring(1));
					stringBuilder.Append("', ");
					stringBuilder.Append(text3);
					stringBuilder.Append("); ");
				}
				if (flag)
				{
					stringBuilder.Append("DBMS_XMLQUERY.setXSLT(ctx, :OracleXslDoc$, ''); ");
				}
				if (wantResult)
				{
					stringBuilder.Append(resultParamName);
				}
				else
				{
					stringBuilder.Append("OracleResult");
				}
				stringBuilder.Append(" := DBMS_XMLQUERY.getXML(ctx); ");
				stringBuilder.Append("DBMS_XMLQUERY.closeContext(ctx); ");
				stringBuilder.Append("end;");
			}
			else
			{
				stringBuilder.Append("declare ");
				stringBuilder.Append("ctx DBMS_XMLGEN.ctxHandle; ");
				stringBuilder.Append("refcur SYS_REFCURSOR; ");
				if (!wantResult)
				{
					stringBuilder.Append("OracleResult CLOB; ");
				}
				if (flag)
				{
					stringBuilder.Append("xmlClob CLOB; ");
					stringBuilder.Append("tmpClob CLOB; ");
					stringBuilder.Append("p DBMS_XMLPARSER.Parser; ");
					stringBuilder.Append("xmldoc DBMS_XMLDOM.DOMDocument; ");
					stringBuilder.Append("xsldoc DBMS_XMLDOM.DOMDocument; ");
					stringBuilder.Append("ss DBMS_XSLPROCESSOR.Stylesheet; ");
					stringBuilder.Append("proc DBMS_XSLPROCESSOR.Processor; ");
				}
				stringBuilder.Append("begin ");
				this.m_pooledCmdText = this.m_pooledCmdText.Trim();
				stringBuilder.Append("OPEN refcur FOR ");
				stringBuilder.Append(this.m_pooledCmdText);
				if (this.m_pooledCmdText.EndsWith(";"))
				{
					stringBuilder.Append(" ");
				}
				else
				{
					stringBuilder.Append("; ");
				}
				stringBuilder.Append("ctx := DBMS_XMLGEN.newContext(refcur); ");
				if (!text.Equals("ROWSET"))
				{
					stringBuilder.Append("DBMS_XMLGEN.setRowSetTag(ctx, '");
					stringBuilder.Append(text);
					stringBuilder.Append("'); ");
				}
				if (!text2.Equals("ROW"))
				{
					stringBuilder.Append("DBMS_XMLGEN.setRowTag(ctx, '");
					stringBuilder.Append(text2);
					stringBuilder.Append("'); ");
				}
				if (xmlQueryProperties.MaxRows > -1)
				{
					stringBuilder.Append("DBMS_XMLGEN.setMaxRows(ctx, '");
					stringBuilder.Append(xmlQueryProperties.MaxRows.ToString());
					stringBuilder.Append("'); ");
				}
				if (flag)
				{
					stringBuilder.Append("xmlClob");
				}
				else if (wantResult)
				{
					stringBuilder.Append(resultParamName);
				}
				else
				{
					stringBuilder.Append("OracleResult");
				}
				stringBuilder.Append(" := DBMS_XMLGEN.getXML(ctx); ");
				stringBuilder.Append("DBMS_XMLGEN.closeContext(ctx); ");
				stringBuilder.Append("CLOSE refcur; ");
				if (flag)
				{
					this.Build9iXslCommandTextForXmlGen(stringBuilder, wantResult, xmlQueryProperties.XsltParams, commandText, xmlCommandType, xmlQueryProperties, null);
				}
				stringBuilder.Append("end;");
			}
			this.m_pooledCmdText = stringBuilder.ToString();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000B0028 File Offset: 0x000AE228
		private void BuildXmlSaveCommandText(OracleConnection connection, string commandText, OracleXmlCommandType xmlCommandType, OracleXmlSaveProperties xmlSaveProperties)
		{
			bool flag = false;
			bool flag2 = false;
			string[] array = null;
			string[] array2 = null;
			string text = string.Empty;
			int majorVersion = connection.m_majorVersion;
			int minorVersion = connection.m_minorVersion;
			if ((majorVersion == 8 && minorVersion == 1) || (majorVersion == 9 && minorVersion == 0))
			{
				flag = true;
			}
			StringBuilder stringBuilder = new StringBuilder(4096);
			if (xmlSaveProperties == null)
			{
				xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if (xmlSaveProperties.Xslt != null && xmlSaveProperties.Xslt.Length != 0)
			{
				flag2 = true;
			}
			if (xmlSaveProperties.RowTag != null && xmlSaveProperties.RowTag.Length != 0)
			{
				text = xmlSaveProperties.RowTag;
			}
			stringBuilder.Append("declare ");
			stringBuilder.Append("ctx DBMS_XMLSAVE.ctxType; ");
			if (flag && flag2)
			{
				stringBuilder.Append("xmlClob CLOB; ");
				stringBuilder.Append("tmpClob CLOB; ");
				stringBuilder.Append("p XMLPARSER.Parser; ");
				stringBuilder.Append("xmldoc XMLDOM.DOMDocument; ");
				stringBuilder.Append("xsldoc XMLDOM.DOMDocument; ");
				stringBuilder.Append("ss XSLPROCESSOR.Stylesheet; ");
				stringBuilder.Append("proc XSLPROCESSOR.Processor; ");
			}
			stringBuilder.Append("begin ");
			if (flag && flag2)
			{
				this.Build8iXslCommandTextForXmlSave(stringBuilder, xmlSaveProperties.XsltParams, commandText, xmlSaveProperties);
			}
			stringBuilder.Append("ctx := DBMS_XMLSAVE.newContext(:OracleTableName$); ");
			if (!text.Equals("ROW"))
			{
				stringBuilder.Append("DBMS_XMLSAVE.setRowTag(ctx, '");
				stringBuilder.Append(text);
				stringBuilder.Append("'); ");
			}
			stringBuilder.Append("DBMS_XMLSAVE.setIgnoreCase(ctx, DBMS_XMLSAVE.MATCH_CASE); ");
			if (!flag)
			{
				stringBuilder.Append("DBMS_XMLSAVE.setSQLToXMLNameEscaping(ctx, true); ");
			}
			stringBuilder.Append("DBMS_XMLSAVE.setDateFormat(ctx, 'yyyy-MM-dd''T''HH:mm:ss.SSS'); ");
			if (xmlSaveProperties.KeyColumnsList != null)
			{
				int i = 0;
				while (i < xmlSaveProperties.KeyColumnsList.Length && xmlSaveProperties.KeyColumnsList[i] != null)
				{
					stringBuilder.Append("DBMS_XMLSAVE.setKeyColumn(ctx, '");
					stringBuilder.Append(xmlSaveProperties.KeyColumnsList[i]);
					stringBuilder.Append("'); ");
					i++;
				}
			}
			if (xmlSaveProperties.UpdateColumnsList != null)
			{
				int i = 0;
				while (i < xmlSaveProperties.UpdateColumnsList.Length && xmlSaveProperties.UpdateColumnsList[i] != null)
				{
					stringBuilder.Append("DBMS_XMLSAVE.setUpdateColumn(ctx, '");
					stringBuilder.Append(xmlSaveProperties.UpdateColumnsList[i]);
					stringBuilder.Append("'); ");
					i++;
				}
			}
			if (!flag && flag2)
			{
				stringBuilder.Append("DBMS_XMLSAVE.setXSLT(ctx, :OracleXslDoc$, ''); ");
				int num = this.ParseXsltParams(xmlSaveProperties.XsltParams, out array, out array2);
				for (int i = 0; i < num; i++)
				{
					stringBuilder.Append("DBMS_XMLSAVE.setXSLTParam(ctx, '");
					stringBuilder.Append(array[i]);
					stringBuilder.Append("', '");
					stringBuilder.Append(array2[i]);
					stringBuilder.Append("'); ");
				}
			}
			stringBuilder.Append(":OracleResult$");
			if (flag && flag2)
			{
				if (OracleXmlCommandType.Insert == xmlCommandType)
				{
					stringBuilder.Append(" := DBMS_XMLSAVE.insertXML(ctx, xmlClob); ");
				}
				else if (OracleXmlCommandType.Update == xmlCommandType)
				{
					stringBuilder.Append(" := DBMS_XMLSAVE.updateXML(ctx, xmlClob); ");
				}
				else if (OracleXmlCommandType.Delete == xmlCommandType)
				{
					stringBuilder.Append(" := DBMS_XMLSAVE.deleteXML(ctx, xmlClob); ");
				}
			}
			else if (OracleXmlCommandType.Insert == xmlCommandType)
			{
				stringBuilder.Append(" := DBMS_XMLSAVE.insertXML(ctx, :OracleXmlDoc$); ");
			}
			else if (OracleXmlCommandType.Update == xmlCommandType)
			{
				stringBuilder.Append(" := DBMS_XMLSAVE.updateXML(ctx, :OracleXmlDoc$); ");
			}
			else if (OracleXmlCommandType.Delete == xmlCommandType)
			{
				stringBuilder.Append(" := DBMS_XMLSAVE.deleteXML(ctx, :OracleXmlDoc$); ");
			}
			stringBuilder.Append("DBMS_XMLSAVE.closeContext(ctx); ");
			if (flag && flag2)
			{
				stringBuilder.Append("dbms_lob.freetemporary(xmlClob); ");
			}
			stringBuilder.Append("end;");
			this.m_pooledCmdText = stringBuilder.ToString();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000B039C File Offset: 0x000AE59C
		private void Build8iXslCommandTextForXmlSave(StringBuilder strBldr, string xsltParams, string commandText, OracleXmlSaveProperties xmlSaveProperties)
		{
			string[] array = null;
			string[] array2 = null;
			string value = ":OracleXmlDoc$";
			string value2 = "xmlClob";
			strBldr.Append("dbms_lob.createtemporary(tmpClob, TRUE); ");
			strBldr.Append("p := XMLPARSER.newParser; ");
			strBldr.Append("XMLPARSER.setValidationMode(p, FALSE); ");
			strBldr.Append("XMLPARSER.setPreserveWhiteSpace(p, TRUE); ");
			if (commandText.Length > 32512)
			{
				strBldr.Append("XMLPARSER.parseClob(p, ");
			}
			else
			{
				strBldr.Append("XMLPARSER.parseBuffer(p, ");
			}
			strBldr.Append(value);
			strBldr.Append("); ");
			strBldr.Append("xmldoc := XMLPARSER.getDocument(p); ");
			if (xmlSaveProperties == null)
			{
				xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if (xmlSaveProperties.Xslt.Length > 32512)
			{
				strBldr.Append("XMLPARSER.parseClob(p, :OracleXslDoc$); ");
			}
			else
			{
				strBldr.Append("XMLPARSER.parseBuffer(p, :OracleXslDoc$); ");
			}
			strBldr.Append("xsldoc := XMLPARSER.getDocument(p); ");
			strBldr.Append("ss := XSLPROCESSOR.newStylesheet(xsldoc, ''); ");
			int num = this.ParseXsltParams(xsltParams, out array, out array2);
			for (int i = 0; i < num; i++)
			{
				strBldr.Append("XSLPROCESSOR.setParam(ss, '");
				strBldr.Append(array[i]);
				strBldr.Append("', '");
				strBldr.Append(array2[i]);
				strBldr.Append("'); ");
			}
			strBldr.Append("proc := XSLPROCESSOR.newProcessor; ");
			strBldr.Append("XSLPROCESSOR.processXSL(proc, ss, xmldoc, tmpClob); ");
			strBldr.Append(value2);
			strBldr.Append(" := tmpClob; ");
			strBldr.Append("XMLDOM.freeDocument(xmldoc); ");
			strBldr.Append("XMLDOM.freeDocument(xsldoc); ");
			strBldr.Append("XSLPROCESSOR.freeProcessor(proc); ");
			strBldr.Append("XSLPROCESSOR.freeStylesheet(ss); ");
			strBldr.Append("XMLPARSER.freeParser(p); ");
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000B0540 File Offset: 0x000AE740
		private void Build9iXslCommandTextForXmlGen(StringBuilder strBldr, bool wantResult, string xsltParams, string commandText, OracleXmlCommandType xmlCommandType, OracleXmlQueryProperties xmlQueryProperties, OracleXmlSaveProperties xmlSaveProperties)
		{
			string[] array = null;
			string[] array2 = null;
			string value;
			string value2;
			if (OracleXmlCommandType.Query == xmlCommandType)
			{
				value = "xmlClob";
				if (wantResult)
				{
					value2 = ":OracleResult$";
				}
				else
				{
					value2 = "OracleResult";
				}
			}
			else
			{
				value = ":OracleXmlDoc$";
				value2 = "xmlClob";
			}
			strBldr.Append("dbms_lob.createtemporary(tmpClob, TRUE); ");
			strBldr.Append("p := DBMS_XMLPARSER.newParser; ");
			strBldr.Append("DBMS_XMLPARSER.setValidationMode(p, FALSE); ");
			strBldr.Append("DBMS_XMLPARSER.setPreserveWhiteSpace(p, TRUE); ");
			if (OracleXmlCommandType.Query == xmlCommandType || commandText.Length > 32512)
			{
				strBldr.Append("DBMS_XMLPARSER.parseClob(p, ");
			}
			else
			{
				strBldr.Append("DBMS_XMLPARSER.parseBuffer(p, ");
			}
			strBldr.Append(value);
			strBldr.Append("); ");
			strBldr.Append("xmldoc := DBMS_XMLPARSER.getDocument(p); ");
			if (xmlQueryProperties == null)
			{
				xmlQueryProperties = new OracleXmlQueryProperties();
			}
			if (xmlSaveProperties == null)
			{
				xmlSaveProperties = new OracleXmlSaveProperties();
			}
			if ((OracleXmlCommandType.Query == xmlCommandType && xmlQueryProperties.Xslt.Length > 32512) || (OracleXmlCommandType.Query != xmlCommandType && xmlSaveProperties.Xslt.Length > 32512))
			{
				strBldr.Append("DBMS_XMLPARSER.parseClob(p, :OracleXslDoc$); ");
			}
			else
			{
				strBldr.Append("DBMS_XMLPARSER.parseBuffer(p, :OracleXslDoc$); ");
			}
			strBldr.Append("xsldoc := DBMS_XMLPARSER.getDocument(p); ");
			strBldr.Append("ss := DBMS_XSLPROCESSOR.newStylesheet(xsldoc, ''); ");
			int num = this.ParseXsltParams(xsltParams, out array, out array2);
			for (int i = 0; i < num; i++)
			{
				strBldr.Append("DBMS_XSLPROCESSOR.setParam(ss, '");
				strBldr.Append(array[i]);
				strBldr.Append("', '");
				strBldr.Append(array2[i]);
				strBldr.Append("'); ");
			}
			strBldr.Append("proc := DBMS_XSLPROCESSOR.newProcessor; ");
			strBldr.Append("DBMS_XSLPROCESSOR.processXSL(proc, ss, xmldoc, tmpClob); ");
			strBldr.Append(value2);
			strBldr.Append(" := tmpClob; ");
			strBldr.Append("DBMS_XMLDOM.freeDocument(xmldoc); ");
			strBldr.Append("DBMS_XMLDOM.freeDocument(xsldoc); ");
			strBldr.Append("DBMS_XSLPROCESSOR.freeProcessor(proc); ");
			strBldr.Append("DBMS_XSLPROCESSOR.freeStylesheet(ss); ");
			strBldr.Append("DBMS_XMLPARSER.freeParser(p); ");
			strBldr.Append("dbms_lob.freetemporary(tmpClob); ");
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000B0748 File Offset: 0x000AE948
		private int ParseXsltParams(string xsltParams, out string[] xsltParamNames, out string[] xsltParamValues)
		{
			int num = 1;
			int num2 = 0;
			xsltParamNames = null;
			xsltParamValues = null;
			if (xsltParams == null || xsltParams.Length == 0)
			{
				return num2;
			}
			int num3 = 0;
			int num4;
			while (-1 != (num4 = xsltParams.IndexOf(";", num3)))
			{
				num++;
				num3 = num4 + 1;
			}
			xsltParamNames = new string[num];
			xsltParamValues = new string[num];
			num3 = 0;
			for (int i = 0; i < num; i++)
			{
				num4 = xsltParams.IndexOf(";", num3);
				int num5;
				if (-1 == num4)
				{
					num5 = xsltParams.Length;
				}
				else
				{
					num5 = num4;
				}
				string text = xsltParams.Substring(num3, num5 - num3);
				int num6;
				if (text != null && text.Length != 0 && -1 != (num6 = text.IndexOf("=")))
				{
					string text2 = text.Substring(0, num6).Trim();
					if (text2 != null && text2.Length != 0)
					{
						string text3 = text.Substring(num6 + 1).Trim();
						xsltParamNames[num2] = text2;
						xsltParamValues[num2] = text3;
						num2++;
					}
				}
				num3 = num5 + 1;
			}
			return num2;
		}

		// Token: 0x040012E0 RID: 4832
		private const int ROWS_TO_FETCH = 25;

		// Token: 0x040012E1 RID: 4833
		private static string s_replaceString = "\r\n";

		// Token: 0x040012E2 RID: 4834
		internal SqlStatementType m_sqlStatementType;

		// Token: 0x040012E3 RID: 4835
		private byte[] m_commandTextByteStream;

		// Token: 0x040012E4 RID: 4836
		internal SQLMetaData m_sqlMetaData;

		// Token: 0x040012E5 RID: 4837
		private int m_rowsToFetch = 25;

		// Token: 0x040012E6 RID: 4838
		internal Accessor[] m_bindAccessors;

		// Token: 0x040012E7 RID: 4839
		internal BindDirection[] m_bindDirectionsFromServer;

		// Token: 0x040012E8 RID: 4840
		internal bool m_bHasReturningClause;

		// Token: 0x040012E9 RID: 4841
		internal long m_executionId;

		// Token: 0x040012EA RID: 4842
		internal bool m_bServerExecutionComplete;

		// Token: 0x040012EB RID: 4843
		internal object m_lockCancel = new object();

		// Token: 0x040012EC RID: 4844
		internal AutoResetEvent m_continueCancel = new AutoResetEvent(false);

		// Token: 0x040012ED RID: 4845
		internal AutoResetEvent m_cancelExecutionEvent = new AutoResetEvent(false);

		// Token: 0x040012EE RID: 4846
		internal List<TTCResultSet> m_implicitRSList;

		// Token: 0x040012EF RID: 4847
		internal int m_numReturningParams;

		// Token: 0x040012F0 RID: 4848
		internal long m_fetchSize;

		// Token: 0x040012F1 RID: 4849
		internal int m_arrayBindCount;

		// Token: 0x040012F2 RID: 4850
		internal bool m_bBindByName;

		// Token: 0x040012F3 RID: 4851
		internal long[] m_rowsAffectedPerBind;

		// Token: 0x040012F4 RID: 4852
		internal static int m_clientRegistrationId = 0;

		// Token: 0x040012F5 RID: 4853
		internal OracleIntervalDS m_sessionTimeZone;

		// Token: 0x040012F6 RID: 4854
		internal bool m_addToStatementCache = true;

		// Token: 0x040012F7 RID: 4855
		internal bool m_addRowid;

		// Token: 0x040012F8 RID: 4856
		internal bool m_addRowidDoneImplicitly;

		// Token: 0x040012F9 RID: 4857
		internal bool m_foundExplicitRowidInSql;

		// Token: 0x040012FA RID: 4858
		internal bool m_bPooled;

		// Token: 0x040012FB RID: 4859
		internal bool m_bExecutingForFill;

		// Token: 0x040012FC RID: 4860
		internal bool m_bReturnPSTypes;

		// Token: 0x040012FD RID: 4861
		private string m_pooledCmdText;
	}
}
