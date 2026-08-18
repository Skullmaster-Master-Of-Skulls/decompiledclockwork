using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.BinXml;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.I18N;
using OracleInternal.Network;
using OracleInternal.SelfTuning;
using OracleInternal.TTC;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020000D4 RID: 212
	internal class OracleConnectionImpl : PoolResource<OraclePoolManager, OraclePool, OracleConnectionImpl>
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000827 RID: 2087 RVA: 0x00056334 File Offset: 0x00054534
		// (remove) Token: 0x06000828 RID: 2088 RVA: 0x0005636C File Offset: 0x0005456C
		internal event OracleConnectionImpl.OracleConnectionCloseEventHandler ConnectionCloseEvent;

		// Token: 0x06000829 RID: 2089 RVA: 0x000563A4 File Offset: 0x000545A4
		internal void RegisterForConnectionClose(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				lock (this.lockOnWeakReferenceObjList)
				{
					this.listofWeakReferenceObj.Add(new WeakReference(obj));
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

		// Token: 0x0600082A RID: 2090 RVA: 0x0005644C File Offset: 0x0005464C
		internal void DeregisterForConnectionClose(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				lock (this.lockOnWeakReferenceObjList)
				{
					int i = 0;
					while (i < this.listofWeakReferenceObj.Count)
					{
						if (this.listofWeakReferenceObj[i].Target == null)
						{
							this.listofWeakReferenceObj.Remove(this.listofWeakReferenceObj[i]);
						}
						else
						{
							if (this.listofWeakReferenceObj[i].Target == obj)
							{
								this.listofWeakReferenceObj.Remove(this.listofWeakReferenceObj[i]);
								break;
							}
							i++;
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

		// Token: 0x0600082B RID: 2091 RVA: 0x00056554 File Offset: 0x00054754
		internal void FireConnectionCloseEvent()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			List<WeakReference> list = new List<WeakReference>();
			try
			{
				lock (this.lockOnWeakReferenceObjList)
				{
					for (int i = 0; i < this.listofWeakReferenceObj.Count; i++)
					{
						if (this.listofWeakReferenceObj[i].IsAlive)
						{
							list.Add(this.listofWeakReferenceObj[i]);
						}
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].Target is OracleBFile)
					{
						OracleBFile oracleBFile = (OracleBFile)list[j].Target;
						if (oracleBFile != null)
						{
							oracleBFile.Close();
						}
					}
					else if (list[j].Target is OracleBlob)
					{
						OracleBlob oracleBlob = (OracleBlob)list[j].Target;
						if (oracleBlob != null)
						{
							oracleBlob.Close();
						}
					}
					else if (list[j].Target is OracleClob)
					{
						OracleClob oracleClob = (OracleClob)list[j].Target;
						if (oracleClob != null)
						{
							oracleClob.Close();
						}
					}
					else if (list[j].Target is OracleRefCursor)
					{
						OracleRefCursor oracleRefCursor = (OracleRefCursor)list[j].Target;
						if (oracleRefCursor != null)
						{
							oracleRefCursor.Close();
						}
					}
					else if (list[j].Target is OracleXmlStream)
					{
						OracleXmlStream oracleXmlStream = (OracleXmlStream)list[j].Target;
						if (oracleXmlStream != null)
						{
							oracleXmlStream.Close();
						}
					}
					else if (list[j].Target is OracleXmlType)
					{
						OracleXmlType oracleXmlType = (OracleXmlType)list[j].Target;
						if (oracleXmlType != null)
						{
							oracleXmlType.Close();
						}
					}
					else if (list[j].Target is OracleDataReaderImpl)
					{
						OracleDataReaderImpl oracleDataReaderImpl = (OracleDataReaderImpl)list[j].Target;
						if (oracleDataReaderImpl != null)
						{
							oracleDataReaderImpl.Close();
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

		// Token: 0x0600082C RID: 2092 RVA: 0x000567E8 File Offset: 0x000549E8
		internal ObxmlDecodeStream GetDecodeStream(OracleConnection conn, OracleBlob csxBlob)
		{
			return this.m_obxmlProcessor.GetDecodeStream(conn, csxBlob);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000567F8 File Offset: 0x000549F8
		internal void CloseDecodeStream(ObxmlDecodeContext decodeContext)
		{
			this.m_obxmlProcessor.CloseDecodeStream(decodeContext);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00056808 File Offset: 0x00054A08
		internal OracleDataReaderImpl GetInitializedDataReaderImpl(Accessor[] defineAccessors, SQLMetaData sqlMetaData, int cursorId, int noOfRowsFetched, CachedStatement cachedStmt, OracleIntervalDS sessionTimeZone, long initialLongFS, long clientInitialLOBFS, long internalInitialLOBFS, long[] snapshotSCN, bool metadataHasImplicitROWIDcolumn = false, bool bInitialLongFetchSizeModified = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleDataReaderImpl result;
			try
			{
				OracleDataReaderImpl oracleDataReaderImpl = null;
				if (this.m_preferredReaderImplTaken)
				{
					oracleDataReaderImpl = new OracleDataReaderImpl(this)
					{
						m_bPooled = false
					};
				}
				else
				{
					lock (this.m_readerImplLock)
					{
						if (this.m_preferredReaderImplTaken)
						{
							oracleDataReaderImpl = new OracleDataReaderImpl(this)
							{
								m_bPooled = false
							};
						}
						else
						{
							this.m_preferredReaderImplTaken = true;
							if (this.m_preferredReaderImpl == null)
							{
								this.m_preferredReaderImpl = new OracleDataReaderImpl(this)
								{
									m_bPooled = true
								};
							}
							oracleDataReaderImpl = this.m_preferredReaderImpl;
						}
					}
				}
				oracleDataReaderImpl.Init(defineAccessors, sqlMetaData, cursorId, noOfRowsFetched, cachedStmt, sessionTimeZone, initialLongFS, clientInitialLOBFS, internalInitialLOBFS, snapshotSCN, metadataHasImplicitROWIDcolumn, bInitialLongFetchSizeModified);
				result = oracleDataReaderImpl;
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

		// Token: 0x0600082F RID: 2095 RVA: 0x00056928 File Offset: 0x00054B28
		internal OracleDataReaderImpl GetInitializedDataReaderImpl(List<OracleRefCursor> refCursors, long longFetchSize, long[] snapshotSCN)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleDataReaderImpl result;
			try
			{
				OracleDataReaderImpl oracleDataReaderImpl = null;
				if (this.m_preferredReaderImplTaken)
				{
					oracleDataReaderImpl = new OracleDataReaderImpl(this)
					{
						m_bPooled = false
					};
				}
				else
				{
					lock (this.m_readerImplLock)
					{
						if (this.m_preferredReaderImplTaken)
						{
							oracleDataReaderImpl = new OracleDataReaderImpl(this)
							{
								m_bPooled = false
							};
						}
						else
						{
							this.m_preferredReaderImplTaken = true;
							if (this.m_preferredReaderImpl == null)
							{
								this.m_preferredReaderImpl = new OracleDataReaderImpl(this)
								{
									m_bPooled = true
								};
							}
							oracleDataReaderImpl = this.m_preferredReaderImpl;
						}
					}
				}
				oracleDataReaderImpl.Init(refCursors, longFetchSize, snapshotSCN);
				result = oracleDataReaderImpl;
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

		// Token: 0x06000830 RID: 2096 RVA: 0x00056A34 File Offset: 0x00054C34
		internal OracleCommandImpl getCommandImpl()
		{
			OracleCommandImpl oracleCommandImpl = null;
			if (this.m_preferredCommandImplTaken)
			{
				oracleCommandImpl = new OracleCommandImpl
				{
					m_bPooled = false
				};
			}
			else
			{
				lock (this.m_commandImplLock)
				{
					if (this.m_preferredCommandImplTaken)
					{
						oracleCommandImpl = new OracleCommandImpl
						{
							m_bPooled = false
						};
					}
					else
					{
						this.m_preferredCommandImplTaken = true;
						oracleCommandImpl = this.m_preferredCommandImpl;
					}
				}
			}
			oracleCommandImpl.Init();
			return oracleCommandImpl;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00056ABC File Offset: 0x00054CBC
		static OracleConnectionImpl()
		{
			TimeStamp.InitializelatestTZversion();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00056AF8 File Offset: 0x00054CF8
		public OracleConnectionImpl()
		{
			this.m_endToEndMetrics[2] = null;
			this.m_endToEndMetrics[0] = null;
			this.m_endToEndMetrics[1] = null;
			this.m_endToEndMetrics[3] = null;
			this.m_endToEndMetricsModified[2] = false;
			this.m_endToEndMetricsModified[0] = false;
			this.m_endToEndMetricsModified[1] = false;
			this.m_endToEndMetricsModified[3] = false;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00056C60 File Offset: 0x00054E60
		internal bool CurrentlyInTransaction
		{
			get
			{
				return (this.m_marshallingEngine.m_endOfCallStatus & 2L) != 0L;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00056C78 File Offset: 0x00054E78
		internal TTCAuthenticate AuthenticateObject
		{
			get
			{
				if (this.m_ttcAuth == null)
				{
					this.m_ttcAuth = new TTCAuthenticate(this.m_marshallingEngine, this.m_pm.m_appThreadLCID);
				}
				return this.m_ttcAuth;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00056CA4 File Offset: 0x00054EA4
		internal TTCExecuteSql ExecuteSqlObject
		{
			get
			{
				if (this.m_executeSql == null)
				{
					this.m_executeSql = new TTCExecuteSql(this.m_marshallingEngine);
				}
				return this.m_executeSql;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00056CC8 File Offset: 0x00054EC8
		internal TTCSimpleOperations SimpleOperationsObject
		{
			get
			{
				if (this.m_ttcSimplOp == null)
				{
					this.m_ttcSimplOp = new TTCSimpleOperations(this.m_marshallingEngine);
				}
				return this.m_ttcSimplOp;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00056CEC File Offset: 0x00054EEC
		internal TTCClose TTCCloseObject
		{
			get
			{
				if (this.m_ttcClose == null)
				{
					this.m_ttcClose = new TTCClose(this.m_marshallingEngine);
				}
				return this.m_ttcClose;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00056D10 File Offset: 0x00054F10
		internal TTCCancel TTCCancelObject
		{
			get
			{
				if (this.m_ttcCancel == null)
				{
					this.m_ttcCancel = new TTCCancel(this.m_marshallingEngine);
				}
				return this.m_ttcCancel;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00056D34 File Offset: 0x00054F34
		internal TTCNotification TTCNotificationObject
		{
			get
			{
				if (this.m_ttcNotification == null)
				{
					this.m_ttcNotification = new TTCNotification(this.m_marshallingEngine);
				}
				return this.m_ttcNotification;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00056D58 File Offset: 0x00054F58
		internal TTCSwitchSession TTCSwitchSessionObject
		{
			get
			{
				if (this.m_ttcSwitchSession == null)
				{
					this.m_ttcSwitchSession = new TTCSwitchSession(this.m_marshallingEngine);
				}
				return this.m_ttcSwitchSession;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00056D7C File Offset: 0x00054F7C
		internal TTCSessionGet TTCSessionGetObject
		{
			get
			{
				if (this.m_ttcSessionGet == null)
				{
					this.m_ttcSessionGet = new TTCSessionGet(this.m_marshallingEngine);
				}
				return this.m_ttcSessionGet;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00056DA0 File Offset: 0x00054FA0
		internal TTCSessionRelease TTCSessionReleaseObject
		{
			get
			{
				if (this.m_ttcSessionRelease == null)
				{
					this.m_ttcSessionRelease = new TTCSessionRelease(this.m_marshallingEngine);
				}
				return this.m_ttcSessionRelease;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00056DC4 File Offset: 0x00054FC4
		internal TTCOPing TTCOPingObject
		{
			get
			{
				if (this.m_ttcOPing == null)
				{
					this.m_ttcOPing = new TTCOPing(this.m_marshallingEngine);
				}
				return this.m_ttcOPing;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00056DE8 File Offset: 0x00054FE8
		internal TTCEndToEndMetrics TTCEndToEndMetricsObject
		{
			get
			{
				if (this.m_ttcEndToEndMetrics == null)
				{
					this.m_ttcEndToEndMetrics = new TTCEndToEndMetrics(this.m_marshallingEngine);
				}
				return this.m_ttcEndToEndMetrics;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00056E0C File Offset: 0x0005500C
		internal TTCFetch TTCFetchObject
		{
			get
			{
				if (this.m_ttcFetch == null)
				{
					this.m_ttcFetch = new TTCFetch(this.m_marshallingEngine);
				}
				return this.m_ttcFetch;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00056E30 File Offset: 0x00055030
		internal TTCTransactionSE TTCTransactionSE
		{
			get
			{
				if (this.m_ttcTransactionSE == null)
				{
					this.m_ttcTransactionSE = new TTCTransactionSE(this.m_marshallingEngine);
				}
				return this.m_ttcTransactionSE;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00056E54 File Offset: 0x00055054
		internal TTCTransactionEN TTCTransactionEN
		{
			get
			{
				if (this.m_ttcTransactionEN == null)
				{
					this.m_ttcTransactionEN = new TTCTransactionEN(this.m_marshallingEngine);
				}
				return this.m_ttcTransactionEN;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00056E78 File Offset: 0x00055078
		internal bool IsServerUsingBigSCN
		{
			get
			{
				return this.m_marshallingEngine.m_bServerUsingBigSCN;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00056E88 File Offset: 0x00055088
		internal bool IsSupportPromotableTransaction
		{
			get
			{
				return this.m_dbMajorVersion > 11 || (this.m_dbMajorVersion == 11 && this.m_dbMajorVersion > 1) || (this.m_dbMajorVersion == 11 && this.m_dbMinorVersion == 1 && this.m_dbPatchsetVersion >= 7);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00056EC8 File Offset: 0x000550C8
		internal int DatabaseMajorVersion
		{
			get
			{
				return this.m_dbMajorVersion;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00056ED0 File Offset: 0x000550D0
		internal int DatabaseMinorVersion
		{
			get
			{
				return this.m_dbMinorVersion;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00056ED8 File Offset: 0x000550D8
		internal int DatabasePatchsetVersion
		{
			get
			{
				return this.m_dbPatchsetVersion;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00056EE0 File Offset: 0x000550E0
		internal bool IsTZDataSentAsLocalTime
		{
			get
			{
				return this.m_dtyNeg.m_sendTZDataAsLocalTime;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00056EF0 File Offset: 0x000550F0
		internal byte[] GetLogicalTransactionId
		{
			get
			{
				return this.m_marshallingEngine.m_ltxId;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00056F00 File Offset: 0x00055100
		internal void SwitchIsolationLevel(System.Data.IsolationLevel isolationLevel)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				string sqlText;
				if (System.Data.IsolationLevel.ReadCommitted == isolationLevel)
				{
					sqlText = "ALTER SESSION SET ISOLATION_LEVEL=READ COMMITTED";
				}
				else
				{
					sqlText = "ALTER SESSION SET ISOLATION_LEVEL=SERIALIZABLE";
				}
				this.ExecuteBasicSQL(sqlText);
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6144, new string[]
					{
						string.Concat(new object[]
						{
							"OracleConnectionImpl.SwitchIsolationLevel(Session ID = ) ",
							this.SessionId,
							" Requested IsolationLevel ",
							isolationLevel,
							" Current IsolationLevel ",
							this.m_currentIsolationLvl
						})
					});
				}
				this.m_currentIsolationLvl = isolationLevel;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00056FFC File Offset: 0x000551FC
		internal int WaitForConnectionForExecution(AutoResetEvent cmdCancelEvent = null)
		{
			int num;
			if (cmdCancelEvent == null)
			{
				this.m_connectionFreeToUseEvent.WaitOne();
				num = 1;
			}
			else
			{
				if (this.m_waitHandlesToStartExecution == null || this.m_waitHandlesToStartExecution.Length < 2)
				{
					this.m_waitHandlesToStartExecution = new WaitHandle[2];
				}
				this.m_waitHandlesToStartExecution[0] = cmdCancelEvent;
				this.m_waitHandlesToStartExecution[1] = this.m_connectionFreeToUseEvent;
				num = WaitHandle.WaitAny(this.m_waitHandlesToStartExecution);
				if (num == 0)
				{
					throw new OracleException(1013, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1013, new string[0]));
				}
			}
			return num;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0005708C File Offset: 0x0005528C
		internal override bool PingServer()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				bool flag = true;
				TTCOPing ttcopingObject = this.TTCOPingObject;
				try
				{
					this.m_connectionFreeToUseEvent.WaitOne();
					this.AddAllPiggyBackRequests();
					ttcopingObject.Write();
					ttcopingObject.m_marshallingEngine.m_oraBufWriter.FlushData();
					ttcopingObject.ReadResponse();
					if (this.m_marshallingEngine.TTCErrorObject.ErrorCode != 0)
					{
						flag = false;
					}
				}
				finally
				{
					this.m_connectionFreeToUseEvent.Set();
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

		// Token: 0x0600084C RID: 2124 RVA: 0x00057164 File Offset: 0x00055364
		internal void ExecuteBasicSQL(string sqlText)
		{
			int num = -1;
			int num2 = 0;
			try
			{
				bool? flag = new bool?(false);
				SqlStatementType sqlStatementType = OracleCommandImpl.GetSqlStatementType(sqlText, ref flag);
				num = this.WaitForConnectionForExecution(null);
				this.AddAllPiggyBackRequests();
				TTCExecuteSql executeSqlObject = this.ExecuteSqlObject;
				TTCExecuteSql.MarshalBindParameterValueHelper @null = TTCExecuteSql.MarshalBindParameterValueHelper.Null;
				byte[] sqlStmtByteStream = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(sqlText, 0, sqlText.Length, true);
				executeSqlObject.SendExecuteRequest(this, sqlStmtByteStream, false, 0, 0L, null, 0L, true, true, false, false, this.m_autoCommit, false, sqlStatementType, 0, 0, null, ref @null, 0);
				Accessor[] array = null;
				SQLMetaData sqlmetaData = null;
				int num3 = 0;
				long num4 = 0L;
				DataUnmarshaller dataUnmarshaller = null;
				long[] array2 = null;
				bool flag2 = false;
				List<TTCResultSet> list = null;
				OracleException ex = null;
				bool bThrowArrayBindRelatedErrors = true;
				bool flag3 = false;
				executeSqlObject.ReceiveExecuteResponse(ref array, null, false, ref sqlmetaData, SqlStatementType.OTHERS, -1L, 0, out num3, ref num4, 0, 0L, null, false, 0, ref dataUnmarshaller, ref @null, out array2, false, ref flag2, ref list, false);
				this.VerifyExecution(out num2, bThrowArrayBindRelatedErrors, sqlStatementType, 0, ref ex, out flag3, false);
				if (executeSqlObject.m_bSessionTimeZoneUpdated)
				{
					this.m_sessionTimeZone = new OracleIntervalDS(executeSqlObject.m_sessionTimeZone);
					executeSqlObject.m_bSessionTimeZoneUpdated = false;
				}
				this.m_sessionTimeZone = this.m_sessionTimeZone;
			}
			finally
			{
				if (num > 0)
				{
					this.m_connectionFreeToUseEvent.Set();
				}
				if (num2 > 0)
				{
					this.AddCursorIdToBeClosed((long)num2);
				}
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000572A0 File Offset: 0x000554A0
		internal int VerifyExecution(out int cursorId, bool bThrowArrayBindRelatedErrors, SqlStatementType sqlStatementType, int arrayBindCount, ref OracleException exceptionForArrayBindDML, out bool hasMoreRowsInDB, bool bFirstIterationDone = false)
		{
			int result = 0;
			hasMoreRowsInDB = true;
			TTCError ttcerrorObject = this.m_marshallingEngine.TTCErrorObject;
			cursorId = ttcerrorObject.CursorId;
			if (ttcerrorObject.ErrorCode != 0)
			{
				if (ttcerrorObject.ErrorCode == 1403 && sqlStatementType == SqlStatementType.SELECT)
				{
					result = (int)ttcerrorObject.m_curRowNumber;
					ttcerrorObject.Initialize();
					hasMoreRowsInDB = false;
				}
				else
				{
					char[] array = ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Dequeue();
					OracleException ex;
					if (ttcerrorObject.ErrorCode == 24381 && exceptionForArrayBindDML != null)
					{
						ex = exceptionForArrayBindDML;
					}
					else
					{
						ex = new OracleException(ttcerrorObject.ErrorCode, string.Empty, string.Empty, ttcerrorObject.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(ttcerrorObject.ErrorMessage, 0, ttcerrorObject.ErrorMessage.Length, array, true));
						if (ttcerrorObject.ErrorCode != 24381)
						{
							bThrowArrayBindRelatedErrors = true;
						}
					}
					if (arrayBindCount > 1 && ttcerrorObject.m_bindErrors != null)
					{
						foreach (TTCArrayBindError ttcarrayBindError in ttcerrorObject.m_bindErrors)
						{
							ex.AddBindErrorToCollection(ttcarrayBindError.m_errorCode, string.Empty, string.Empty, ttcerrorObject.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(ttcarrayBindError.m_errorMsg, 0, ttcarrayBindError.m_errorMsg.Length, array, true), bFirstIterationDone ? (ttcarrayBindError.m_rowOffset + 1) : ttcarrayBindError.m_rowOffset);
						}
					}
					if (array != null)
					{
						ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Enqueue(ref array);
					}
					if (ex.Message.Contains("ORA-01000"))
					{
						this.OnOra01000Exception();
					}
					if (arrayBindCount == 0 || bThrowArrayBindRelatedErrors)
					{
						if (cursorId > 0)
						{
							this.AddCursorIdToBeClosed((long)cursorId);
						}
						if (ttcerrorObject.m_bindErrors == null || ttcerrorObject.m_bindErrors.Length == arrayBindCount)
						{
							throw ex;
						}
					}
					exceptionForArrayBindDML = ex;
				}
			}
			else
			{
				result = (int)ttcerrorObject.m_curRowNumber;
			}
			return result;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00057460 File Offset: 0x00055660
		internal void OnOra01000Exception()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_pm != null && this.m_pm.m_bSelfTuning && !ProviderConfig.MaxStatementCacheSize.IsUserDefined)
				{
					int maxCacheSize = this.m_statementCache.m_maxCacheSize;
					int num = 10;
					int num2 = maxCacheSize - num;
					if (num2 > 0)
					{
						this.m_pm.MaxAllowedValue = num2;
						if (this.m_pm.m_recommendedSCS > num2)
						{
							this.m_pm.OnUpdateRecommendations(RecommendationType.SCS, num2);
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

		// Token: 0x0600084F RID: 2127 RVA: 0x0005752C File Offset: 0x0005572C
		public override void Connect(ConnectionString cs, bool bOpenEndUserSession, CriteriaCtx criteriaCtx, string instanceName = null)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					Trace.GetCPInfo(this, null, instanceName, "open", false, false)
				});
			}
			string userId = null;
			string proxyUserId = null;
			string password = null;
			string proxyPassword = null;
			bool flag = false;
			bool flag2 = false;
			this.m_logonMode = 0L;
			try
			{
				if (criteriaCtx != null && !string.IsNullOrEmpty(criteriaCtx.m_edition) && string.IsNullOrEmpty(criteriaCtx.m_pdbName))
				{
					this.m_editionName = criteriaCtx.m_edition;
				}
				if (this.m_bConnected)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_ALREADY_OPEN, new string[0]));
				}
				this.m_cs = cs;
				if (this.m_cs.m_userId != "/")
				{
					userId = this.m_cs.m_userId;
				}
				if (this.m_cs.m_proxyUserId != "/")
				{
					proxyUserId = this.m_cs.m_proxyUserId;
				}
				if (this.m_pm.m_cs.m_pooling)
				{
					flag = this.m_pm.m_bDoNAHandShake;
					flag2 = this.m_pm.m_bDoExternalAuth;
					this.m_logonMode = this.m_pm.m_logonMode;
					if (this.m_pm.m_bUsingSEPSCredentials)
					{
						if ("/" == this.m_cs.m_userId)
						{
							userId = this.m_pm.m_cs.m_sepsUserId;
							password = this.m_pm.m_cs.SEPSPassword;
						}
						else if ("/" == this.m_cs.m_proxyUserId)
						{
							proxyUserId = this.m_pm.m_cs.m_sepsProxyUserId;
							proxyPassword = this.m_pm.m_cs.SEPSProxyPassword;
						}
					}
					else
					{
						password = cs.Password;
						proxyPassword = cs.ProxyPassword;
					}
				}
				else
				{
					if (this.m_cs.m_dbaPrivilege == DBAPrivilege.SYSDBA)
					{
						this.m_logonMode |= 32L;
						flag = (flag2 = true);
					}
					else if (this.m_cs.m_dbaPrivilege == DBAPrivilege.SYSOPER)
					{
						this.m_logonMode |= 64L;
						flag = (flag2 = true);
					}
					if (!flag2 && ("/" == this.m_cs.m_userId || "/" == this.m_cs.m_proxyUserId))
					{
						if (!SqlNetOraConfig.WalletOverride)
						{
							flag = (flag2 = true);
							password = cs.Password;
							proxyPassword = cs.ProxyPassword;
						}
						else
						{
							flag2 = false;
							string text = null;
							string text2 = null;
							string text3 = null;
							string text4 = null;
							OraclePoolManager.FetchSEPSCredentails(this.m_cs.m_dataSource, out text, out text2, out text3, out text4);
							if ("/" == this.m_cs.m_userId)
							{
								userId = text;
								password = text2;
							}
							else if ("/" == this.m_cs.m_proxyUserId)
							{
								proxyUserId = text;
								proxyPassword = text2;
							}
						}
					}
					else
					{
						password = cs.Password;
						proxyPassword = cs.ProxyPassword;
					}
				}
				this.m_oracleCommunication = new OracleCommunication(new ConOraBufPool(this.m_pm.m_oraBufPool));
				if (!flag)
				{
					flag = (this.m_pm.m_bNAEInUse == null || this.m_pm.m_bNAEInUse.Value);
				}
				if (this.m_pm.m_fullDescriptor != null && this.m_pm.m_fullDescriptor != string.Empty)
				{
					this.m_oracleCommunication.Connect(this.m_pm.m_fullDescriptor, flag, instanceName);
				}
				else
				{
					this.m_oracleCommunication.Connect(cs.m_dataSource, flag, instanceName);
				}
				if (this.m_pm.m_bNAEInUse == null)
				{
					this.m_pm.m_bNAEInUse = new bool?(this.m_oracleCommunication.IsNAEInUse());
				}
				if (flag2 || !SqlNetOraConfig.WalletOverride)
				{
					if ((!cs.m_bUserIdSet && !cs.m_bProxyUserIdSet) || (cs.m_bProxyPasswordSet && !cs.m_bProxyUserIdSet) || (cs.m_bPasswordSet && !cs.m_bUserIdSet && cs.m_bProxyUserIdSet && cs.m_bProxyPasswordSet) || (cs.m_proxyUserId == "/" && !cs.m_bUserIdSet && cs.m_bPasswordSet))
					{
						throw new OracleException(1017, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1017, new string[0]));
					}
					if ((cs.m_userId != "/" || cs.m_bProxyUserIdSet || cs.m_bProxyPasswordSet) && ((!cs.m_bProxyUserIdSet && !cs.m_bPasswordSet) || (cs.m_bProxyUserIdSet && !cs.m_bProxyPasswordSet)) && cs.m_proxyUserId != "/")
					{
						throw new OracleException(1005, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1005, new string[0]));
					}
				}
				else if ("/" == this.m_cs.m_userId)
				{
					if (cs.m_bProxyPasswordSet && cs.m_bProxyUserIdSet)
					{
						throw new OracleException(28179, string.Empty, string.Empty, "ORA-28179: client user name not provided by proxy");
					}
					if (cs.m_bProxyUserIdSet)
					{
						throw new OracleException(1005, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1005, new string[0]));
					}
					if (cs.m_bProxyPasswordSet)
					{
						throw new OracleException(1017, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1017, new string[0]));
					}
				}
				this.m_oracleCommunication.OraBufPool.Init(this.m_oracleCommunication);
				if (this.m_oracleCommunication.m_sessionCtx != null && this.m_oracleCommunication.m_sessionCtx.m_sessionDataUnit != this.m_pm.m_oraBufPool.m_smallBufSize)
				{
					if (this.m_pm.m_oraBufPool.m_smallBufSize != 0)
					{
						this.m_pm.ClearAllPools(this, false);
					}
					this.m_pm.m_oraBufPool.UpdateBufSizes(this.m_oracleCommunication);
				}
				this.m_marshallingEngine = new MarshallingEngine(this.m_oracleCommunication, this);
				this.m_ttcSessionGet = new TTCSessionGet(this.m_marshallingEngine);
				if (string.Compare(this.m_oracleCommunication.Server, "POOLED", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					this.m_pm.m_cs.m_drcpEnabled = DrcpType.True;
					this.m_marshallingEngine.m_bDRCPConnection = true;
					if (criteriaCtx != null)
					{
						this.m_drcpConnectionClass = criteriaCtx.m_connectionClass;
						if (criteriaCtx.m_bDrcpPurityNew == 1)
						{
							this.m_drcpSessionPurity = "1";
						}
					}
				}
				else
				{
					this.m_pm.m_cs.m_drcpEnabled = DrcpType.False;
					if (criteriaCtx != null && !string.IsNullOrEmpty(criteriaCtx.m_connectionClass))
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7601, new string[]
						{
							"DRCPConnectionClass",
							"DRCP"
						}));
					}
				}
				this.DoProtocolNegotiation();
				this.DoDataTypeNegotiation();
				if (this.m_ttcAuth != null)
				{
					this.m_ttcAuth.ReInit(this.m_marshallingEngine, this.m_pm.m_appThreadLCID);
				}
				TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
				double totalMilliseconds = currentTimeZone.GetUtcOffset(DateTime.Now).TotalMilliseconds;
				int hours = (int)(totalMilliseconds / 3600000.0);
				int minutes = (int)(totalMilliseconds / 60000.0 % 60.0);
				this.m_sessionTimeZone = new OracleIntervalDS(0, hours, minutes, 0, 0);
				bool flag3 = this.DoAuthentication(userId, password, proxyUserId, proxyPassword, cs.m_newPassword, flag2, bOpenEndUserSession);
				if (this.m_oracleCommunication.IsNAEInUse() && !flag3)
				{
					this.m_oracleCommunication.SetFoldInKey(this.m_ttcAuth.m_xoredKaAndKb);
				}
				this.m_bConnected = true;
				this.SendVersionMessageAndProcessResponse();
				this.m_creationTime = DateTime.Now;
				this.m_marshallingEngine.DBVersion = this.m_ttcVersion.GetVersionNumber();
				this.SetAutoCommit(true);
				if (this.m_sessionProperties != null)
				{
					this.m_endUserSessionId = this.SessionId;
					this.m_endUserSerialNum = this.SerialNumber;
				}
				else
				{
					this.m_pxyUserSessionId = this.ProxySessionId;
					this.m_pxyUserSerialNum = this.ProxySerialNumber;
				}
				if (this.m_marshallingEngine.m_bDRCPConnection)
				{
					this.m_marshallingEngine.m_bDRCPSessionAttached = true;
					this.bDRCPServerProcessAttached = true;
				}
				if (ProviderConfig.m_bTraceLevelPrivate && this.m_pxyUserSessionId == -1)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"(CON) (ENDSID=",
							this.m_endUserSessionId,
							":",
							this.m_endUserSerialNum,
							")(TYPE=",
							this.m_sessionType.ToString(),
							")(CHRSET=",
							this.m_serverCharacterSet,
							":",
							this.m_serverNCharSet,
							")"
						})
					});
				}
				this.EvaluateDbMajorMinorPatchsetVersion();
				if (string.IsNullOrEmpty(this.m_pm.m_databaseDomainName))
				{
					this.m_pm.m_databaseDomainName = this.DatabaseDomainName;
					this.m_pm.SetDomainForDS(this.m_cs.ServerID, this.m_pm.m_databaseDomainName);
				}
				if (string.IsNullOrEmpty(this.m_pm.m_conStrServiceName))
				{
					string serviceName = this.ServiceName;
					if (!string.IsNullOrEmpty(serviceName) && serviceName != "null")
					{
						this.m_pm.m_conStrServiceName = serviceName;
						this.m_pm.SetServiceForDS(cs.ServerID, serviceName);
					}
				}
				this.UpdateAttributes();
				if (!this.m_pm.m_maxOpenCursorsFetched)
				{
					int num = this.MaxOpenCursors;
					int num2 = 10;
					if (num > 0)
					{
						num -= num2;
						if (num > 0)
						{
							this.m_pm.MaxAllowedValue = num;
							if (this.m_pm.m_recommendedSCS > this.m_pm.MaxAllowedValue)
							{
								this.m_pm.m_recommendedSCS = this.m_pm.MaxAllowedValue;
							}
						}
					}
					this.m_pm.m_maxOpenCursorsFetched = true;
				}
				int recommendedSCS = this.m_pm.m_recommendedSCS;
				if (recommendedSCS > 0 && this.m_statementCache == null)
				{
					this.m_statementCache = new StatementCache(recommendedSCS);
				}
				if (OraclePool.m_bPerfHardConnectsPerSecond)
				{
					OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.HardConnectsPerSecond, this, this.m_cp);
				}
				if (this.m_executeSql != null)
				{
					this.m_executeSql.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcSimplOp != null)
				{
					this.m_ttcSimplOp.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcOPing != null)
				{
					this.m_ttcOPing.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcClose != null)
				{
					this.m_ttcClose.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcCancel != null)
				{
					this.m_ttcCancel.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcSwitchSession != null)
				{
					this.m_ttcSwitchSession.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcEndToEndMetrics != null)
				{
					this.m_ttcEndToEndMetrics.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcNotification != null)
				{
					this.m_ttcNotification.ReInit(this.m_marshallingEngine);
				}
				if (this.m_ttcFetch != null)
				{
					this.m_ttcFetch.ReInit(this.m_marshallingEngine);
				}
				bool haEvents = this.m_cs.m_haEvents;
				if (!this.m_cs.m_haEventsPresentInConnString)
				{
					haEvents = ConfigBaseClass.m_haEvents;
				}
				bool flag4 = false;
				if (haEvents && !OracleConnectionImpl.dbsRegisteredForHA.Contains(this.m_databaseName))
				{
					lock (OracleConnectionImpl.m_lockForHARLBRegistration)
					{
						if (!OracleConnectionImpl.dbsRegisteredForHA.Contains(this.m_databaseName))
						{
							flag4 = true;
							OracleConnectionImpl.dbsRegisteredForHA.Add(this.m_databaseName);
						}
					}
				}
				bool loadBalancing = this.m_cs.m_loadBalancing;
				if (!this.m_cs.m_loadBalancingPresentInConnString)
				{
					loadBalancing = ConfigBaseClass.m_loadBalancing;
				}
				bool flag6 = false;
				if (loadBalancing && !OracleConnectionImpl.servicesRegisteredForRLB.Contains(this.ServiceName))
				{
					lock (OracleConnectionImpl.m_lockForHARLBRegistration)
					{
						if (!OracleConnectionImpl.servicesRegisteredForRLB.Contains(this.ServiceName))
						{
							flag6 = true;
							OracleConnectionImpl.servicesRegisteredForRLB.Add(this.ServiceName);
						}
					}
				}
				if (flag4 || flag6)
				{
					OracleConnectionImpl.HARLBCallbackRegisterData harlbcallbackRegisterData = new OracleConnectionImpl.HARLBCallbackRegisterData();
					harlbcallbackRegisterData.m_serviceName = this.ServiceName;
					harlbcallbackRegisterData.m_databaseName = this.m_databaseName;
					harlbcallbackRegisterData.m_conTimeout = this.m_conTimeout;
					harlbcallbackRegisterData.m_onsHASubscrPatter = this.m_onsHASubscrPatter;
					harlbcallbackRegisterData.m_onsRLBSubscrPatter = this.m_onsRLBSubscPatter;
					harlbcallbackRegisterData.m_onsConfigFromDb = this.m_onsConfigFromDb;
					harlbcallbackRegisterData.m_bRegisterForHA = flag4;
					harlbcallbackRegisterData.m_bRegisterForRLB = flag6;
					ThreadPool.QueueUserWorkItem(new WaitCallback(OracleConnectionImpl.Register_HA_RLB_Callbacks), harlbcallbackRegisterData);
				}
				if (criteriaCtx != null)
				{
					criteriaCtx.m_bNewConCreated = true;
					this.m_connectionClass = criteriaCtx.m_connectionClass;
					this.m_pm.m_criteriaMapper.GetId(this);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						Trace.GetCPInfo(this, null, instanceName, "open", false, false)
					});
				}
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00058268 File Offset: 0x00056468
		internal override void UpdateAttributes()
		{
			this.m_instanceName = this.InstanceName;
			this.m_hostName = this.HostName;
			this.m_databaseDomainName = this.DatabaseDomainName;
			this.m_databaseName = this.DatabaseUniqueName;
			this.m_maxIdentifierLength = this.MaxIdentifierLength;
			this.m_onsConfigFromDb = this.OnsConfigFromDB;
			this.m_onsHASubscrPatter = this.OnsHASubscriberPattern;
			this.m_onsRLBSubscPatter = this.OnsRLBSubscriberPattern;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000582D8 File Offset: 0x000564D8
		internal override bool AlterSession(bool[] alterConnectionTuple, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				bool flag = false;
				string text = "ALTER SESSION SET";
				if (alterConnectionTuple[0])
				{
					string defaultEditionName = this.GetDefaultEditionName();
					if (!string.IsNullOrEmpty(defaultEditionName))
					{
						text = text + " EDITION=\"" + defaultEditionName.Trim() + "\"";
					}
					if (!string.IsNullOrEmpty(criteriaCtx.m_pdbName))
					{
						text = text + " CONTAINER=\"" + criteriaCtx.m_pdbName.Trim().ToLowerInvariant() + "\"";
					}
					if (!string.IsNullOrEmpty(criteriaCtx.m_serviceName))
					{
						text = text + " SERVICE=\"" + criteriaCtx.m_serviceName.Trim().ToLowerInvariant() + "\"";
					}
					if (!string.IsNullOrEmpty(criteriaCtx.m_edition))
					{
						text = text + " EDITION=" + criteriaCtx.m_edition.Trim();
					}
				}
				else if (alterConnectionTuple[1])
				{
					if (!string.IsNullOrEmpty(criteriaCtx.m_edition))
					{
						text = text + " EDITION=" + criteriaCtx.m_edition.Trim();
					}
					else
					{
						string defaultEditionName2 = this.GetDefaultEditionName();
						if (!string.IsNullOrEmpty(defaultEditionName2))
						{
							text = text + " EDITION=\"" + defaultEditionName2.Trim() + "\"";
						}
					}
				}
				try
				{
					if (this.m_pm.m_cs.m_drcpEnabled == DrcpType.True)
					{
						this.PingServer();
					}
					this.PurgeStatementCache(0);
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[]
						{
							string.Format(" (ENTRY)  (MULTITENANT) ExecuteBasicSql({0}) using Conn ID = {1} , DBInst = {2}", text, this.m_endUserSessionId, this.m_instanceName)
						});
					}
					this.ExecuteBasicSQL(text);
					flag = true;
					this.UpdateAttributes();
					if (alterConnectionTuple[0])
					{
						this.bSessionSwitched = true;
						bool loadBalancing = this.m_cs.m_loadBalancing;
						if (!this.m_cs.m_loadBalancingPresentInConnString)
						{
							loadBalancing = ConfigBaseClass.m_loadBalancing;
						}
						string serviceName = this.ServiceName;
						bool flag2 = false;
						bool bRegisterForHA = false;
						if (loadBalancing && !OracleConnectionImpl.servicesRegisteredForRLB.Contains(serviceName))
						{
							lock (OracleConnectionImpl.m_lockForHARLBRegistration)
							{
								if (!OracleConnectionImpl.servicesRegisteredForRLB.Contains(serviceName))
								{
									flag2 = true;
									OracleConnectionImpl.servicesRegisteredForRLB.Add(serviceName);
								}
							}
						}
						if (flag2)
						{
							OracleConnectionImpl.HARLBCallbackRegisterData harlbcallbackRegisterData = new OracleConnectionImpl.HARLBCallbackRegisterData();
							harlbcallbackRegisterData.m_serviceName = this.ServiceName;
							harlbcallbackRegisterData.m_databaseName = this.m_databaseName;
							harlbcallbackRegisterData.m_conTimeout = this.m_conTimeout;
							harlbcallbackRegisterData.m_onsRLBSubscrPatter = this.m_onsRLBSubscPatter;
							harlbcallbackRegisterData.m_onsConfigFromDb = this.m_onsConfigFromDb;
							harlbcallbackRegisterData.m_bRegisterForHA = bRegisterForHA;
							harlbcallbackRegisterData.m_bRegisterForRLB = flag2;
							ThreadPool.QueueUserWorkItem(new WaitCallback(OracleConnectionImpl.Register_HA_RLB_Callbacks), harlbcallbackRegisterData);
						}
					}
					else
					{
						this.bSessionSwitched = false;
					}
					if (alterConnectionTuple[1])
					{
						if (!string.IsNullOrEmpty(criteriaCtx.m_edition))
						{
							this.m_editionName = criteriaCtx.m_edition.Trim();
						}
						else
						{
							this.m_editionName = this.m_pm.m_defaultEditionDict[this.ServiceName];
						}
					}
				}
				catch (Exception innerException)
				{
					OracleException ex = new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]), innerException);
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
					throw ex;
				}
				result = flag;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00058668 File Offset: 0x00056868
		internal static void Register_HA_RLB_Callbacks(object state)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				OracleConnectionImpl.HARLBCallbackRegisterData harlbcallbackRegisterData = (OracleConnectionImpl.HARLBCallbackRegisterData)state;
				if (harlbcallbackRegisterData != null)
				{
					if (harlbcallbackRegisterData.m_bRegisterForHA)
					{
						try
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)1310720, new string[]
								{
									"Registring for HA notifications for database: " + harlbcallbackRegisterData.m_databaseName
								});
							}
							OracleONSNotificationManager notificationManager = OracleONSNotificationManager.GetNotificationManager(NotificationType.HA);
							notificationManager.RegisterForNotification(harlbcallbackRegisterData.m_serviceName, harlbcallbackRegisterData.m_databaseName, harlbcallbackRegisterData.m_conTimeout, harlbcallbackRegisterData.m_onsHASubscrPatter, harlbcallbackRegisterData.m_onsConfigFromDb);
						}
						catch (Exception arg)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)1310720, new string[]
								{
									"Failed to Register for HA Events: " + arg
								});
							}
						}
					}
					if (harlbcallbackRegisterData.m_bRegisterForRLB)
					{
						try
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)786432, new string[]
								{
									"Registring for RLB notifications for service: " + harlbcallbackRegisterData.m_serviceName
								});
							}
							OracleONSNotificationManager notificationManager2 = OracleONSNotificationManager.GetNotificationManager(NotificationType.RLB);
							notificationManager2.RegisterForNotification(harlbcallbackRegisterData.m_serviceName, harlbcallbackRegisterData.m_databaseName, harlbcallbackRegisterData.m_conTimeout, harlbcallbackRegisterData.m_onsRLBSubscrPatter, harlbcallbackRegisterData.m_onsConfigFromDb);
						}
						catch (Exception arg2)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)786432, new string[]
								{
									"Failed to Register for RLB Events: " + arg2
								});
							}
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00058830 File Offset: 0x00056A30
		internal void Logoff()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				try
				{
					this.m_connectionFreeToUseEvent.WaitOne();
					this.AddAllPiggyBackRequests();
					TTCSimpleOperations simpleOperationsObject = this.SimpleOperationsObject;
					simpleOperationsObject.SetFunctionCode(9);
					simpleOperationsObject.WriteMessage();
					simpleOperationsObject.m_marshallingEngine.m_oraBufWriter.FlushData();
					simpleOperationsObject.ReadResponse();
				}
				finally
				{
					this.m_connectionFreeToUseEvent.Set();
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

		// Token: 0x06000854 RID: 2132 RVA: 0x000588F8 File Offset: 0x00056AF8
		internal void AlterSessionOnConnect(OracleConnection con)
		{
			OracleGlobalizationImpl oracleGlobalizationImpl = null;
			if (this.m_cs.m_drcpEnabled != DrcpType.True && this.m_oracleGlobalizationImpl == null)
			{
				if (this.m_pm.m_dictSvcCtx[this.ServiceName].m_orclGlobImpl == null)
				{
					lock (this.m_pm.m_orclGlobLock)
					{
						if (this.m_pm.m_dictSvcCtx[this.ServiceName].m_orclGlobImpl == null)
						{
							oracleGlobalizationImpl = new OracleGlobalizationImpl(this.m_pm.m_appThreadLCID);
							this.m_pm.m_dictSvcCtx[this.ServiceName].m_orclGlobImpl = oracleGlobalizationImpl;
							oracleGlobalizationImpl.AlterSession(oracleGlobalizationImpl, con);
						}
						else
						{
							oracleGlobalizationImpl = this.m_pm.m_dictSvcCtx[this.ServiceName].m_orclGlobImpl;
						}
						goto IL_EC;
					}
				}
				oracleGlobalizationImpl = this.m_pm.m_dictSvcCtx[this.ServiceName].m_orclGlobImpl;
			}
			IL_EC:
			if (oracleGlobalizationImpl != null)
			{
				this.m_oracleGlobalizationImpl = (OracleGlobalizationImpl)oracleGlobalizationImpl.Clone();
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00058A18 File Offset: 0x00056C18
		public override bool Dump()
		{
			return this.m_oracleCommunication != null;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00058A28 File Offset: 0x00056C28
		internal bool DRCPConnection
		{
			get
			{
				return this.m_marshallingEngine.m_bDRCPConnection;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00058A44 File Offset: 0x00056C44
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00058A38 File Offset: 0x00056C38
		internal string DRCPConnectionClass
		{
			get
			{
				return this.m_drcpConnectionClass;
			}
			set
			{
				this.m_drcpConnectionClass = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00058A58 File Offset: 0x00056C58
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00058A4C File Offset: 0x00056C4C
		internal string DRCPtagName
		{
			get
			{
				return this.m_drcpTagName;
			}
			set
			{
				this.m_drcpTagName = value;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00058A60 File Offset: 0x00056C60
		public override void DetachServerProcess(string drcpTagName, bool bUseDRCPMultiTag)
		{
			this.TTCSessionReleaseObject.ReleaseSession(drcpTagName, false);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00058A70 File Offset: 0x00056C70
		public override void AttachServerProcess(long sessionFlags, bool bUseDRCPMultiTag, ref long s2cSessionFlags)
		{
			TTCSessionGet ttcsessionGetObject = this.TTCSessionGetObject;
			ttcsessionGetObject.GetSession(0L, false);
			s2cSessionFlags = ttcsessionGetObject.m_s2cSessionGetflags;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00058A98 File Offset: 0x00056C98
		internal void NewDRCPSessionAttached()
		{
			if (this.m_statementCache != null)
			{
				this.m_statementCache.Purge(0);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00058AB0 File Offset: 0x00056CB0
		public override void DisConnect(CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[]
				{
					Trace.GetCPInfo(this, null, null, "kill", false, false)
				});
				if (this.m_pxyUserSessionId == -1)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"(DISCON) (ENDSID=",
							this.m_endUserSessionId,
							":",
							this.m_endUserSerialNum,
							")"
						})
					});
				}
				else
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"(DISCON) (ENDSID=",
							this.m_endUserSessionId,
							":",
							this.m_endUserSerialNum,
							")(PXYSID=",
							this.m_pxyUserSessionId,
							":",
							this.m_pxyUserSerialNum,
							")"
						})
					});
				}
			}
			try
			{
				if (this.m_deletionRequestor != DeletionRequestor.HA)
				{
					try
					{
						if (this.m_pm != null && this.m_pm.m_cs != null && this.m_pm.m_cs.m_drcpEnabled == DrcpType.True && this.bDRCPServerProcessAttached)
						{
							this.DetachServerProcess(null, false);
							this.bDRCPServerProcessAttached = false;
							this.bGotMatchingServerProcess = false;
						}
						this.Logoff();
					}
					catch
					{
					}
				}
				try
				{
					this.m_oracleCommunication.Disconnect();
				}
				catch
				{
				}
				if (OraclePool.m_bPerfHardDisconnectsPerSecond)
				{
					OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.HardDisconnectsPerSecond, this, this.m_cp);
				}
				this.m_oracleCommunication.OraBufPool.ReturnAll();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				this.ClearState(this.m_deletionRequestor == DeletionRequestor.HA);
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[]
					{
						Trace.GetCPInfo(this, null, null, "kill", false, false)
					});
				}
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00058CF8 File Offset: 0x00056EF8
		internal void ClearState(bool isRequestFofHA)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_bConnected = false;
				if (!isRequestFofHA)
				{
					this.m_oracleCommunication = null;
					this.m_marshallingEngine = null;
					this.m_statementCache = null;
					this.m_serverCompiletimeCapabilities = null;
					this.m_serverRuntimeCapabilities = null;
					this.m_cursorsToBeClosed.Clear();
					if (this.m_sessionProperties != null)
					{
						this.m_sessionProperties.Clear();
					}
					if (this.m_proxySessionProperties != null)
					{
						this.m_proxySessionProperties.Clear();
					}
					this.m_cursorsToBeCancelled.Clear();
					this.m_autoCommit = true;
					this.m_sessionType = SessionType.Non_Proxy;
					this.m_endOfCallStatus = 0L;
					this.m_serverCharacterSet = 0;
					this.m_serverNCharSet = 0;
					this.m_pxyUserSessionId = -1;
					this.m_pxyUserSerialNum = -1;
					this.m_endUserSessionId = -1;
					this.m_endUserSerialNum = -1;
					this.m_oracleGlobalizationImpl = null;
					this.m_temporaryLobReferences.Clear();
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

		// Token: 0x06000860 RID: 2144 RVA: 0x00058E24 File Offset: 0x00057024
		internal void DoProtocolNegotiation()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_protoNeg == null)
				{
					this.m_protoNeg = new TTCProtocolNegotiation(this.m_marshallingEngine);
				}
				else
				{
					this.m_protoNeg.ReInit(this.m_marshallingEngine);
				}
				this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1] = 2;
				this.m_protoNeg.WriteMessage();
				this.m_protoNeg.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.m_protoNeg.ReadResponse();
				this.m_serverCharacterSet = this.m_protoNeg.ServerCharacterSet;
				this.m_serverNCharSet = this.m_protoNeg.ServerNCharacterSet;
				this.m_marshallingEngine.m_typeRepresentation.m_serverConvFlags = this.m_protoNeg.ServerFlags;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SvcObj, new string[]
					{
						string.Concat(new object[]
						{
							"DoProtocolNegotiation : ServerCharSet = ",
							this.m_serverCharacterSet,
							", ServerNCharSet = ",
							this.m_serverNCharSet,
							", ServerFlags: ",
							this.m_protoNeg.ServerFlags
						})
					});
				}
				this.m_serverCompiletimeCapabilities = this.m_protoNeg.ServerCompileTimeCapabilities;
				this.m_serverRuntimeCapabilities = this.m_protoNeg.ServerRunTimeCapabilities;
				this.m_marshallingEngine.m_dbCharSetConv = Conv.GetInstance((int)this.m_serverCharacterSet);
				this.m_marshallingEngine.m_nCharSetConv = Conv.GetInstance((int)this.m_serverNCharSet);
				short serverCharacterSet = this.m_serverCharacterSet;
				if (Conv.GetMaxBytesPerChar((int)this.m_serverCharacterSet) > 1)
				{
					this.m_marshallingEngine.m_bSvrCSMultibyte = true;
				}
				TTCTypeRepresentation typeRepresentation = this.m_marshallingEngine.m_typeRepresentation;
				typeRepresentation.m_serverConvFlags |= 2;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SvcObj, new string[]
					{
						string.Concat(new object[]
						{
							"After Charset Negotiation : ServerCharSet = ",
							this.m_serverCharacterSet,
							", ServerNCharSet = ",
							this.m_serverNCharSet,
							", NetworkCharSet = ",
							serverCharacterSet,
							", ServerConvFlags: ",
							this.m_marshallingEngine.m_typeRepresentation.m_serverConvFlags,
							", Network CHARSET  MaxBytesPerChar = ",
							Conv.GetMaxBytesPerChar((int)serverCharacterSet)
						}),
						", Server  CHARSET  MaxBytesPerChar = " + Conv.GetMaxBytesPerChar((int)this.m_serverCharacterSet),
						", Server  NCHARSET MaxBytesPerChar = " + Conv.GetMaxBytesPerChar((int)this.m_serverNCharSet)
					});
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

		// Token: 0x06000861 RID: 2145 RVA: 0x00059138 File Offset: 0x00057338
		internal void DoDataTypeNegotiation()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_dtyNeg == null)
				{
					this.m_dtyNeg = new TTCDataTypeNegotiation(this.m_marshallingEngine);
				}
				else
				{
					this.m_dtyNeg.ReInit(this.m_marshallingEngine);
				}
				this.m_dtyNeg.WriteMessage(this.m_serverCompiletimeCapabilities, this.m_serverRuntimeCapabilities, this.m_serverCharacterSet, this.m_serverNCharSet, this.m_marshallingEngine.m_typeRepresentation.m_serverConvFlags);
				this.m_dtyNeg.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.m_dtyNeg.ReadResponse();
				if (this.m_dtyNeg.m_CompileTimeCapabilities[7] < this.m_serverCompiletimeCapabilities[7])
				{
					this.m_marshallingEngine.NegotiatedTTCVersion = this.m_dtyNeg.m_CompileTimeCapabilities[7];
				}
				else
				{
					this.m_marshallingEngine.NegotiatedTTCVersion = this.m_serverCompiletimeCapabilities[7];
				}
				this.m_b32kTypeSupported = this.m_dtyNeg.m_b32kTypeSupported;
				if ((this.m_serverCompiletimeCapabilities[15] & 1) != 0)
				{
					this.m_marshallingEngine.HasEOCSCapability = true;
				}
				if ((this.m_serverCompiletimeCapabilities[16] & 16) != 0)
				{
					this.m_marshallingEngine.HasFSAPCapability = true;
				}
				if (this.m_serverCompiletimeCapabilities.Length > 37 && (this.m_serverCompiletimeCapabilities[37] & 32) != 0)
				{
					this.m_marshallingEngine.m_bUseBigCLRChunks = true;
					this.m_marshallingEngine.m_effectiveTTCC_MXIN = 32767;
				}
				this.m_marshallingEngine.m_bServerUsingBigSCN = (this.m_serverCompiletimeCapabilities[7] >= 8);
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

		// Token: 0x06000862 RID: 2146 RVA: 0x00059314 File Offset: 0x00057514
		internal void SendVersionMessageAndProcessResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_ttcVersion == null)
				{
					this.m_ttcVersion = new TTCVersion(this.m_marshallingEngine);
				}
				else
				{
					this.m_ttcVersion.ReInit(this.m_marshallingEngine);
				}
				this.m_ttcVersion.WriteMessage();
				this.m_ttcVersion.m_marshallingEngine.m_oraBufWriter.FlushData();
				this.m_ttcVersion.ReadResponse();
				OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
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

		// Token: 0x06000863 RID: 2147 RVA: 0x000593EC File Offset: 0x000575EC
		internal bool DoAuthentication(string userId, string password, string proxyUserId, string proxyPassword, string newPassword, bool bDoExternalAuth, bool bOpenEndUserSession = true)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				if (!string.IsNullOrEmpty(this.m_editionName) && (this.m_serverCompiletimeCapabilities == null || this.m_serverCompiletimeCapabilities.Length < 31 || (this.m_serverCompiletimeCapabilities[31] & 8) != 8))
				{
					OracleException innerException = new OracleException(-7500, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7500, new string[]
					{
						"11g",
						"Edition Based Redefinition"
					}));
					OracleException ex = new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]), innerException);
					throw ex;
				}
				bool flag = true;
				TTCAuthenticate authenticateObject = this.AuthenticateObject;
				bool flag2;
				if (this.m_cs.m_bProxyUserIdSet || this.m_cs.m_bProxyPasswordSet)
				{
					this.m_sessionType = SessionType.Two_Session_Proxy;
					if (!this.m_cs.m_bPasswordSet && !string.IsNullOrEmpty(userId))
					{
						this.m_sessionType = SessionType.Single_Session_Proxy;
					}
					flag2 = this.HandleProxyConection(userId, password, proxyUserId, proxyPassword, bDoExternalAuth, bOpenEndUserSession);
				}
				else
				{
					if (!string.IsNullOrEmpty(userId) || !bDoExternalAuth)
					{
						authenticateObject.WriteOSessKeyMessage(userId, this.m_logonMode);
						authenticateObject.m_marshallingEngine.m_oraBufWriter.FlushData();
						authenticateObject.ReadOSessKeyResponse();
						if ((32L & this.m_logonMode) != 0L || (64L & this.m_logonMode) != 0L)
						{
							flag = false;
						}
						else
						{
							OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
						}
					}
					else
					{
						flag = false;
					}
					if (string.IsNullOrEmpty(newPassword))
					{
						this.m_logonMode |= 1L;
					}
					else
					{
						this.m_logonMode |= 2L;
					}
					authenticateObject.WriteOAuthMessage(userId, password, string.Empty, false, -1, -1, this.m_logonMode, newPassword, this.m_serverCompiletimeCapabilities[4], !flag, true, this.m_protoNeg.ServerCharacterSet.ToString());
					authenticateObject.m_marshallingEngine.m_oraBufWriter.FlushData();
					flag2 = authenticateObject.ReceiveOAuthResponse();
					if (flag && flag2)
					{
						throw new OracleException(1017, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1017, new string[0]));
					}
					OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
					this.m_sessionProperties = authenticateObject.m_sessionProperties;
				}
				result = flag2;
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex2, null);
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

		// Token: 0x06000864 RID: 2148 RVA: 0x00059694 File Offset: 0x00057894
		internal void SetAutoCommit(bool bAutoCommitOn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_autoCommit = bAutoCommitOn;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000596D0 File Offset: 0x000578D0
		internal void AddTempLOBsToBeFreed(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			lock (this.m_lockForLists)
			{
				this.m_tempLOBsToBeFreed.Add(lobLocator);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00059748 File Offset: 0x00057948
		internal void AddCursorIdToBeClosed(long cursorId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			lock (this.m_lockForLists)
			{
				this.m_cursorsToBeClosed.Add(cursorId);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000597C4 File Offset: 0x000579C4
		internal void AddCursorIdsToBeClosed(List<long> cursorIds)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			lock (this.m_lockForLists)
			{
				this.m_cursorsToBeClosed.AddRange(cursorIds);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0005983C File Offset: 0x00057A3C
		internal void AddCursorIdToBeCancelled(long cursorId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			lock (this.m_lockForLists)
			{
				this.m_cursorsToBeCancelled.Add(cursorId);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000598B8 File Offset: 0x00057AB8
		internal void FlushPendingPiggybackMessages()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				bool flag = false;
				lock (this.m_lockForLists)
				{
					if (this.m_tempLOBsToBeFreed.Count > 0 || this.m_cursorsToBeClosed.Count > 0 || this.m_cursorsToBeCancelled.Count > 0 || this.m_endToEndMetricsModified[2] || this.m_endToEndMetricsModified[0] || this.m_endToEndMetricsModified[1] || this.m_endToEndMetricsModified[3])
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.PingServer();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000599B0 File Offset: 0x00057BB0
		internal void AddAllPiggyBackRequests()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				lock (this.m_lockForLists)
				{
					if (this.m_tempLOBsToBeFreed.Count > 0)
					{
						TTCLob.FreeTempLobsPiggyBack(this.m_marshallingEngine, this.m_tempLOBsToBeFreed);
						this.m_tempLOBsToBeFreed.Clear();
					}
					if (this.m_cursorsToBeClosed.Count > 0)
					{
						this.TTCCloseObject.Write(this.m_cursorsToBeClosed);
						this.m_cursorsToBeClosed.Clear();
					}
					if (this.m_cursorsToBeCancelled.Count > 0)
					{
						this.TTCCancelObject.Write(this.m_cursorsToBeCancelled);
						this.m_cursorsToBeCancelled.Clear();
					}
					if (this.m_endToEndMetricsModified[2] || this.m_endToEndMetricsModified[0] || this.m_endToEndMetricsModified[1] || this.m_endToEndMetricsModified[3])
					{
						this.TTCEndToEndMetricsObject.Write(this.m_endToEndMetrics, this.m_endToEndMetricsModified);
						this.m_endToEndMetricsModified[2] = false;
						this.m_endToEndMetricsModified[0] = false;
						this.m_endToEndMetricsModified[1] = false;
						this.m_endToEndMetricsModified[3] = false;
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

		// Token: 0x0600086B RID: 2155 RVA: 0x00059B4C File Offset: 0x00057D4C
		internal void ResetEndToEndMetrics()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_endToEndMetrics[2] = null;
				this.m_endToEndMetrics[0] = null;
				this.m_endToEndMetrics[3] = null;
				this.m_endToEndMetricsModified[2] = true;
				this.m_endToEndMetricsModified[0] = true;
				this.m_endToEndMetricsModified[3] = true;
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

		// Token: 0x0600086C RID: 2156 RVA: 0x00059BF0 File Offset: 0x00057DF0
		internal void ResetMTSTxnCtx()
		{
			if (this.m_mtsTxnCtx != null)
			{
				this.m_mtsTxnCtx.Reset(this);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00059C08 File Offset: 0x00057E08
		internal string GetServerVersion()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string serverVersion;
			try
			{
				serverVersion = this.m_pm.m_serverVersion;
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
			return serverVersion;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00059C84 File Offset: 0x00057E84
		internal void EvaluateDbMajorMinorPatchsetVersion()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				string text = null;
				if (this.m_sessionProperties != null)
				{
					text = (string)this.m_sessionProperties["AUTH_VERSION_NO"];
				}
				else if (this.m_proxySessionProperties != null)
				{
					text = (string)this.m_proxySessionProperties["AUTH_VERSION_NO"];
				}
				if (!string.IsNullOrEmpty(text))
				{
					int versionInt = int.Parse(text);
					this.m_pm.m_serverVersion = TTCAuthenticate.ConvertVersionIntToString(versionInt, ref this.m_dbMajorVersion, ref this.m_dbMinorVersion, ref this.m_dbPatchsetVersion);
					if (this.m_dbMajorVersion > 10 || (this.m_dbMajorVersion == 10 && this.m_dbMinorVersion >= 2))
					{
						this.m_isDb10gR2OrHigher = true;
					}
					if (this.m_dbMajorVersion > 11 || (this.m_dbMajorVersion == 11 && this.m_dbMinorVersion >= 1))
					{
						this.m_isDb11gR1OrHigher = true;
					}
					if (this.m_dbMajorVersion > 12 || (this.m_dbMajorVersion == 12 && this.m_dbMinorVersion >= 1))
					{
						this.m_isDb12cR1OrHigher = true;
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

		// Token: 0x0600086F RID: 2159 RVA: 0x00059DD4 File Offset: 0x00057FD4
		internal static void CheckForAnyErrorFromDB(TTCError ttcOER)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (ttcOER.ErrorCode != 0)
			{
				byte[] errorMessage = ttcOER.ErrorMessage;
				char[] chars = ttcOER.m_marshallingEngine.m_charArrayPooler.Dequeue();
				string errMsg = ttcOER.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(errorMessage, 0, errorMessage.Length, chars, true);
				ttcOER.m_marshallingEngine.m_charArrayPooler.Enqueue(ref chars);
				throw new OracleException(ttcOER.ErrorCode, string.Empty, string.Empty, errMsg);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00059E74 File Offset: 0x00058074
		internal OracleTimeZoneInfo? GetDBTimeZoneBytes()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleTimeZoneInfo? result;
			try
			{
				if (this.m_dtyNeg != null)
				{
					if (this.m_dbTimeZoneInfo == null)
					{
						this.m_dbTimeZoneInfo = new OracleTimeZoneInfo?(TimeStamp.GetTimeZoneInfo(this.m_dtyNeg.m_dbTimeZoneBytes));
					}
					result = this.m_dbTimeZoneInfo;
				}
				else
				{
					result = null;
				}
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00059F2C File Offset: 0x0005812C
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00059F08 File Offset: 0x00058108
		internal int SessionId
		{
			get
			{
				int result;
				try
				{
					int num = -1;
					if (this.m_sessionProperties != null)
					{
						string s = (string)this.m_sessionProperties["AUTH_SESSION_ID"];
						int.TryParse(s, out num);
					}
					result = num;
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
					throw;
				}
				return result;
			}
			set
			{
				if (this.m_sessionProperties != null)
				{
					this.m_sessionProperties["AUTH_SESSION_ID"] = value.ToString();
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00059FAC File Offset: 0x000581AC
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00059F88 File Offset: 0x00058188
		internal int SerialNumber
		{
			get
			{
				int result;
				try
				{
					int num = -1;
					if (this.m_sessionProperties != null)
					{
						string s = (string)this.m_sessionProperties["AUTH_SERIAL_NUM"];
						int.TryParse(s, out num);
					}
					result = num;
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
					throw;
				}
				return result;
			}
			set
			{
				if (this.m_sessionProperties != null)
				{
					this.m_sessionProperties["AUTH_SERIAL_NUM"] = value.ToString();
				}
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0005A008 File Offset: 0x00058208
		internal int ProxySessionId
		{
			get
			{
				int result;
				try
				{
					int num = -1;
					if (this.m_proxySessionProperties != null)
					{
						string s = (string)this.m_proxySessionProperties["AUTH_SESSION_ID"];
						int.TryParse(s, out num);
					}
					result = num;
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0005A064 File Offset: 0x00058264
		internal int ProxySerialNumber
		{
			get
			{
				int result;
				try
				{
					int num = -1;
					if (this.m_proxySessionProperties != null)
					{
						string s = (string)this.m_proxySessionProperties["AUTH_SERIAL_NUM"];
						int.TryParse(s, out num);
					}
					result = num;
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
					throw;
				}
				return result;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0005A0C0 File Offset: 0x000582C0
		private bool HandleProxyConection(string userId, string password, string proxyUserId, string proxyPassword, bool bDoExternalAuth, bool bOpenEndUserSession)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				bool flag = this.OpenProxySession(userId, proxyUserId, proxyPassword, bDoExternalAuth, !string.IsNullOrEmpty(proxyUserId));
				if (bOpenEndUserSession && SessionType.Two_Session_Proxy == this.m_sessionType)
				{
					this.OpenEndUserSession(userId, password, null);
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

		// Token: 0x06000878 RID: 2168 RVA: 0x0005A15C File Offset: 0x0005835C
		private bool OpenProxySession(string userId, string proxyUserId, string proxyPassword, bool bDoExternalAuth, bool bVerifyResponseFromServer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				TTCAuthenticate authenticateObject = this.AuthenticateObject;
				if (!string.IsNullOrEmpty(proxyUserId) || !bDoExternalAuth)
				{
					authenticateObject.WriteOSessKeyMessage(proxyUserId, this.m_logonMode);
					authenticateObject.m_marshallingEngine.m_oraBufWriter.FlushData();
					authenticateObject.ReadOSessKeyResponse();
					OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
				}
				this.m_logonMode |= 1L;
				string proxyClientName = string.Empty;
				if (SessionType.Single_Session_Proxy == this.m_sessionType)
				{
					proxyClientName = userId;
				}
				authenticateObject.WriteOAuthMessage(proxyUserId, proxyPassword, proxyClientName, false, -1, -1, this.m_logonMode, null, this.m_serverCompiletimeCapabilities[4], !bVerifyResponseFromServer, SessionType.Single_Session_Proxy == this.m_sessionType, this.m_protoNeg.ServerCharacterSet.ToString());
				authenticateObject.m_marshallingEngine.m_oraBufWriter.FlushData();
				bool flag = authenticateObject.ReceiveOAuthResponse();
				if (bVerifyResponseFromServer && flag)
				{
					throw new OracleException(1017, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesgWithErrCode(1017, new string[0]));
				}
				OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
				this.m_proxySessionProperties = authenticateObject.m_sessionProperties;
				this.m_pxyUserSessionId = this.ProxySessionId;
				this.m_pxyUserSerialNum = this.ProxySerialNumber;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"(CON) (ENDSID=",
							this.m_endUserSessionId,
							":",
							this.m_endUserSerialNum,
							")(PXYSID=",
							this.m_pxyUserSessionId,
							":",
							this.m_pxyUserSerialNum,
							")(TYPE=",
							this.m_sessionType.ToString(),
							")(CHRSET=",
							this.m_serverCharacterSet,
							":",
							this.m_serverNCharSet,
							")"
						})
					});
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

		// Token: 0x06000879 RID: 2169 RVA: 0x0005A3F4 File Offset: 0x000585F4
		internal void OpenEndUserSession(string userId, string password, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool flag = false;
			try
			{
				if (criteriaCtx != null && !string.IsNullOrEmpty(criteriaCtx.m_edition))
				{
					this.m_editionName = criteriaCtx.m_edition;
				}
				TTCAuthenticate authenticateObject = this.AuthenticateObject;
				authenticateObject.ReInit(authenticateObject.m_marshallingEngine);
				if (!string.IsNullOrEmpty(userId))
				{
					authenticateObject.WriteOSessKeyMessage(userId, this.m_logonMode);
					authenticateObject.m_marshallingEngine.m_oraBufWriter.FlushData();
					authenticateObject.ReadOSessKeyResponse();
					OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
					flag = true;
				}
				authenticateObject.WriteOAuthMessage(userId, password, string.Empty, true, this.m_pxyUserSessionId, this.m_pxyUserSerialNum, this.m_logonMode, null, this.m_serverCompiletimeCapabilities[4], !flag, true, this.m_protoNeg.ServerCharacterSet.ToString());
				authenticateObject.m_marshallingEngine.m_oraBufWriter.FlushData();
				authenticateObject.ReceiveOAuthResponse();
				OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
				this.m_sessionProperties = authenticateObject.m_sessionProperties;
				this.m_endUserSessionId = this.SessionId;
				this.m_endUserSerialNum = this.SerialNumber;
				this.TTCSwitchSessionObject.Write(this.m_endUserSessionId, this.m_endUserSerialNum, TTCSwitchSession.OSESSWS);
				this.m_sessionType = SessionType.Two_Session_Proxy;
				this.m_bEndUserSessionEstablished = true;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"(CON) (ENDSID=",
							this.m_endUserSessionId,
							":",
							this.m_endUserSerialNum,
							")(PXYSID=",
							this.m_pxyUserSessionId,
							":",
							this.m_pxyUserSerialNum,
							")(TYPE=",
							this.m_sessionType.ToString(),
							")(CHRSET=",
							this.m_serverCharacterSet,
							":",
							this.m_serverNCharSet,
							")"
						})
					});
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

		// Token: 0x0600087A RID: 2170 RVA: 0x0005A690 File Offset: 0x00058890
		internal void CloseEndUserSession()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
				{
					string.Concat(new object[]
					{
						"(DISCON) (ENDSID=",
						this.m_endUserSessionId,
						":",
						this.m_endUserSerialNum,
						")"
					})
				});
			}
			this.m_bEndUserSessionEstablished = false;
			try
			{
				this.Logoff();
				this.TTCSwitchSessionObject.Write(this.m_pxyUserSessionId, this.m_pxyUserSerialNum, TTCSwitchSession.OSESSWS);
				this.m_sessionProperties = this.m_proxySessionProperties;
				this.m_sessionType = SessionType.Non_Proxy;
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

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0005A794 File Offset: 0x00058994
		internal SessionType SessionType
		{
			get
			{
				return this.m_sessionType;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0005A79C File Offset: 0x0005899C
		internal static string GetCorrespondingAuthAttrName(int keyword)
		{
			switch (keyword)
			{
			case 0:
				return "AUTH_NLS_LXCCURRENCY";
			case 1:
				return "AUTH_NLS_LXCISOCURR";
			case 2:
				return "AUTH_NLS_LXCNUMERICS";
			case 3:
			case 4:
			case 5:
			case 6:
			case 13:
			case 14:
			case 15:
				break;
			case 7:
				return "AUTH_NLS_LXCDATEFM";
			case 8:
				return "AUTH_NLS_LXCDATELANG";
			case 9:
				return "AUTH_NLS_LXCTERRITORY";
			case 10:
				return "SESSION_NLS_LXCCHARSET";
			case 11:
				return "AUTH_NLS_LXCSORT";
			case 12:
				return "AUTH_NLS_LXCCALENDAR";
			case 16:
				return "AUTH_NLS_LXLAN";
			default:
				switch (keyword)
				{
				case 50:
					return "AUTH_NLS_LXCSORT";
				case 52:
					return "AUTH_NLS_LXCUNIONCUR";
				case 57:
					return "AUTH_NLS_LXCTIMEFM";
				case 58:
					return "AUTH_NLS_LXCSTMPFM";
				case 59:
					return "AUTH_NLS_LXCTTZNFM";
				case 60:
					return "AUTH_NLS_LXCSTZNFM";
				case 61:
					return "SESSION_NLS_LXCNLSLENSEM";
				case 62:
					return "SESSION_NLS_LXCNCHAREXCP";
				case 63:
					return "SESSION_NLS_LXCNCHARIMP";
				}
				break;
			}
			return string.Empty;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0005A8B0 File Offset: 0x00058AB0
		internal void UpdateSessionAttributes(TTCKeywordValuePair[] al8KeyVals)
		{
			if (al8KeyVals == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < al8KeyVals.Length; i++)
			{
				try
				{
					num = al8KeyVals[i].m_keyword;
					byte[] binaryValue = al8KeyVals[i].m_binaryValue;
					int num2 = num;
					switch (num2)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 14:
					case 15:
					case 16:
						break;
					default:
						switch (num2)
						{
						case 50:
						case 51:
						case 52:
						case 53:
						case 54:
						case 55:
						case 56:
						case 57:
						case 58:
						case 59:
						case 60:
						case 61:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 165:
							{
								string value = string.Empty;
								if (binaryValue != null)
								{
									value = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(binaryValue, 0, binaryValue.Length, null, true);
									this.m_sessionProperties["AUTH_SESSION_ID"] = value;
								}
								break;
							}
							case 166:
							{
								string value2 = string.Empty;
								if (binaryValue != null)
								{
									value2 = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(binaryValue, 0, binaryValue.Length, null, true);
									this.m_sessionProperties["AUTH_SERIAL_NUM"] = value2;
								}
								break;
							}
							case 172:
								if (al8KeyVals[i].m_textValueInString != null)
								{
									this.m_sessionProperties["AUTH_ORA_EDITION"] = al8KeyVals[i].m_textValueInString;
									this.m_editionName = al8KeyVals[i].m_textValueInString;
								}
								break;
							case 175:
								if (binaryValue != null && binaryValue.Length >= 4)
								{
									int num3 = (int)binaryValue[3];
									num3 |= (int)binaryValue[2] << 8;
									num3 |= (int)binaryValue[1] << 16;
									num3 |= (int)binaryValue[0] << 24;
									this.m_sessionProperties["AUTH_MAX_OPEN_CURSORS"] = num3.ToString();
								}
								break;
							case 176:
								if (binaryValue != null && binaryValue.Length >= 4)
								{
									int num4 = (int)binaryValue[3];
									num4 |= (int)binaryValue[2] << 8;
									num4 |= (int)binaryValue[1] << 16;
									this.PDBUniqueId = (num4 | (int)binaryValue[0] << 24).ToString();
								}
								break;
							case 177:
							{
								long num5 = 0L;
								if (binaryValue != null)
								{
									for (int j = 3; j >= 0; j--)
									{
										num5 |= (long)((long)((ulong)binaryValue[3 - j] & 255UL) << 8 * j);
									}
									this.DatabaseId = num5.ToString();
								}
								break;
							}
							case 182:
								if (binaryValue != null && binaryValue.Length >= 4)
								{
									int num6 = (int)binaryValue[3];
									num6 |= (int)binaryValue[2] << 8;
									num6 |= (int)binaryValue[1] << 16;
									num6 |= (int)binaryValue[0] << 24;
									this.m_sessionProperties["AUTH_MAX_IDEN_LENGTH"] = num6.ToString();
									this.m_maxIdentifierLength = num6;
								}
								break;
							case 183:
								if (al8KeyVals[i].m_textValueInString != null)
								{
									this.m_sessionProperties["AUTH_SC_SERVICE_NAME"] = al8KeyVals[i].m_textValueInString;
								}
								break;
							case 197:
								if (al8KeyVals[i].m_textValueInString != null)
								{
									this.m_sessionProperties["AL8KW_CONTAINER_NAME"] = al8KeyVals[i].m_textValueInString;
								}
								break;
							}
							break;
						}
						break;
					}
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SvcObj, new string[]
						{
							"Failed to Parse Keyword: " + num
						});
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SvcObj, new string[]
						{
							ex.ToString()
						});
					}
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0005ACBC File Offset: 0x00058EBC
		internal override string EditionName
		{
			get
			{
				string text = null;
				if (this.m_isDb11gR1OrHigher)
				{
					if (this.m_sessionProperties != null)
					{
						if (this.m_sessionProperties.ContainsKey("AUTH_ORA_EDITION"))
						{
							text = (string)this.m_sessionProperties["AUTH_ORA_EDITION"];
						}
					}
					else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AUTH_ORA_EDITION"))
					{
						text = (string)this.m_proxySessionProperties["AUTH_ORA_EDITION"];
					}
					if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.m_editionName))
					{
						if (this.m_editionName[0] == '"')
						{
							int num = this.m_editionName.IndexOf('"', 1);
							text = this.m_editionName.Substring(1, num - 1);
						}
						else
						{
							text = this.m_editionName.ToUpper();
						}
					}
				}
				return text;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0005AD8C File Offset: 0x00058F8C
		internal override string PdbName
		{
			get
			{
				string result = null;
				if (this.m_isDb12cR1OrHigher)
				{
					if (this.m_sessionProperties != null && this.m_sessionProperties.ContainsKey("AL8KW_CONTAINER_NAME"))
					{
						result = ((string)this.m_sessionProperties["AL8KW_CONTAINER_NAME"]).ToLowerInvariant();
					}
					else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AL8KW_CONTAINER_NAME"))
					{
						result = ((string)this.m_proxySessionProperties["AL8KW_CONTAINER_NAME"]).ToLowerInvariant();
					}
					else if (this.m_pm != null)
					{
						if (string.IsNullOrEmpty(this.m_pm.m_conStrPdbName))
						{
							this.GetConStrDefaults();
						}
						result = this.m_pm.m_conStrPdbName;
					}
				}
				return result;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0005AE44 File Offset: 0x00059044
		internal override void GetConStrDefaults()
		{
			if (!this.m_pm.m_bDefaultsFetched)
			{
				lock (this.m_pm.m_conStrDefaultsLocker)
				{
					if (!this.m_pm.m_bDefaultsFetched)
					{
						if (this.m_isDb12cR1OrHigher)
						{
							OracleCommandImpl commandImpl = this.getCommandImpl();
							commandImpl.m_addRowid = false;
							commandImpl.m_addToStatementCache = false;
							commandImpl.m_arrayBindCount = 0;
							commandImpl.m_bBindByName = false;
							commandImpl.m_fetchSize = (long)ConfigBaseClass.m_FetchSize;
							OracleParameterCollection oracleParameterCollection = new OracleParameterCollection();
							OracleParameter oracleParameter = null;
							string cmdText = "DECLARE\n\n                          BEGIN\n                             select sys_context('USERENV','CON_NAME') into :PDBNAME from dual;                             \n                          END;\n                          ";
							oracleParameter = new OracleParameter("PDBNAME", OracleDbType.Varchar2, 256, "", ParameterDirection.Output);
							oracleParameterCollection.Add(oracleParameter);
							TTCError ttcerrorObject = this.m_marshallingEngine.TTCErrorObject;
							try
							{
								this.m_marshallingEngine.TTCErrorObject = new TTCError(this.m_marshallingEngine);
								this.ExecuteNonQueryWithBind(commandImpl, cmdText, oracleParameterCollection, CommandType.Text);
							}
							finally
							{
								this.m_marshallingEngine.TTCErrorObject = ttcerrorObject;
							}
							if (this.m_isDb12cR1OrHigher && oracleParameter != null && oracleParameter.Value != null && string.IsNullOrEmpty(this.m_pm.m_conStrPdbName))
							{
								this.m_pm.m_conStrPdbName = oracleParameter.Value.ToString().ToLowerInvariant();
							}
						}
						this.m_pm.m_bDefaultsFetched = true;
					}
				}
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0005AFC4 File Offset: 0x000591C4
		internal override string GetDefaultEditionName()
		{
			string result = null;
			if (this.m_isDb11gR1OrHigher)
			{
				lock (this.m_pm.m_defaultEditionLocker)
				{
					if (!this.m_pm.m_defaultEditionDict.ContainsKey(this.ServiceName))
					{
						OracleCommandImpl commandImpl = this.getCommandImpl();
						commandImpl.m_addRowid = false;
						commandImpl.m_addToStatementCache = false;
						commandImpl.m_arrayBindCount = 0;
						commandImpl.m_bBindByName = false;
						commandImpl.m_fetchSize = (long)ConfigBaseClass.m_FetchSize;
						OracleParameterCollection oracleParameterCollection = new OracleParameterCollection();
						OracleParameter oracleParameter = null;
						string cmdText = "DECLARE\n\n                          BEGIN \n                             SELECT PROPERTY_VALUE into :EDITIONNAME FROM DATABASE_PROPERTIES WHERE PROPERTY_NAME = 'DEFAULT_EDITION';                        \n                          END;\n                          ";
						oracleParameter = new OracleParameter("EDITIONNAME", OracleDbType.Varchar2, 256, "", ParameterDirection.Output);
						oracleParameterCollection.Add(oracleParameter);
						TTCError ttcerrorObject = this.m_marshallingEngine.TTCErrorObject;
						try
						{
							this.m_marshallingEngine.TTCErrorObject = new TTCError(this.m_marshallingEngine);
							this.ExecuteNonQueryWithBind(commandImpl, cmdText, oracleParameterCollection, CommandType.Text);
						}
						finally
						{
							this.m_marshallingEngine.TTCErrorObject = ttcerrorObject;
						}
						if (oracleParameter != null && oracleParameter.Value != null)
						{
							this.m_pm.m_defaultEditionDict[this.ServiceName] = oracleParameter.Value.ToString();
						}
					}
					result = this.m_pm.m_defaultEditionDict[this.ServiceName];
				}
			}
			return result;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0005B13C File Offset: 0x0005933C
		private int ExecuteNonQueryWithBind(OracleCommandImpl cmdImpl, string cmdText, OracleParameterCollection m_parameters, CommandType m_commandType)
		{
			int result = 0;
			OracleException ex = null;
			OracleLogicalTransaction oracleLogicalTransaction = null;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				cmdImpl.m_cancelExecutionEvent.Reset();
				cmdImpl.m_continueCancel.Reset();
				cmdImpl.m_bServerExecutionComplete = false;
				OracleParameterCollection oracleParameterCollection = null;
				bool flag = true;
				Timer timer = null;
				long[] scnFromExecution;
				try
				{
					OracleDependencyImpl orclDependencyImpl = null;
					result = cmdImpl.ExecuteNonQuery(cmdText, m_parameters, m_commandType, this, 0, 0L, orclDependencyImpl, out scnFromExecution, out oracleParameterCollection, ref flag, out ex, null, ref oracleLogicalTransaction, false);
				}
				finally
				{
					if (timer != null)
					{
						timer.Change(-1L, -1L);
						timer.Dispose();
					}
				}
				if (flag && m_parameters != null && m_parameters.Count > 0)
				{
					this.ExtractAccessorValuesIntoParam(cmdImpl, oracleParameterCollection, this, oracleParameterCollection.Count, cmdText, 0L, 0L, 0L, scnFromExecution, false);
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex2, oracleLogicalTransaction);
				if (!(ex2 is OracleException))
				{
					throw;
				}
				if (((OracleException)ex2).OracleLogicalTransaction == null || !(((OracleException)ex2).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex2).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
			}
			finally
			{
				if (cmdImpl != null)
				{
					cmdImpl.m_bindAccessors = null;
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0005B2E4 File Offset: 0x000594E4
		internal void ExtractAccessorValuesIntoParam(OracleCommandImpl cmdImpl, OracleParameterCollection paramColl, OracleConnectionImpl connImpl, int paramCount, string commandText, long longFetchSize, long clientInitialLOBFS, long internalInitialLOBFS, long[] scnFromExecution, bool bCallFromExecuteReader)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int num = 0;
			char[] array = null;
			BindDirection[] bindDirectionsFromServer = cmdImpl.m_bindDirectionsFromServer;
			Accessor[] bindAccessors = cmdImpl.m_bindAccessors;
			try
			{
				for (int i = 0; i < paramCount; i++)
				{
					OracleParameter oracleParameter = paramColl[i];
					if (!oracleParameter.m_bDuplicateBind)
					{
						if (bindDirectionsFromServer[num] != BindDirection.Input)
						{
							Accessor accessor = bindAccessors[num];
							OracleDbType oracleDbType = oracleParameter.OracleDbType;
							if (oracleDbType <= OracleDbType.Long)
							{
								if (oracleDbType != OracleDbType.Char && oracleDbType != OracleDbType.Long)
								{
									goto IL_AD;
								}
								goto IL_8D;
							}
							else
							{
								switch (oracleDbType)
								{
								case OracleDbType.NChar:
								case OracleDbType.NVarchar2:
									goto IL_8D;
								case (OracleDbType)118:
									goto IL_AD;
								default:
									if (oracleDbType == OracleDbType.Varchar2)
									{
										goto IL_8D;
									}
									goto IL_AD;
								}
							}
							IL_BA:
							accessor.Initialize();
							goto IL_C0;
							IL_8D:
							if (array == null)
							{
								array = this.m_marshallingEngine.m_charArrayPooler.Dequeue();
							}
							oracleParameter.PostBind_Char(this, accessor, array);
							goto IL_BA;
							IL_AD:
							oracleParameter.Value = accessor.GetValue();
							goto IL_BA;
						}
						IL_C0:
						num++;
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
				if (array != null)
				{
					this.m_marshallingEngine.m_charArrayPooler.Enqueue(array);
				}
				if (this.m_marshallingEngine.m_oraBufRdr != null)
				{
					this.m_marshallingEngine.m_oraBufRdr.FreeTempOBList();
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0005B440 File Offset: 0x00059640
		internal int MaxIdentifierLength
		{
			get
			{
				string s = null;
				if (this.m_sessionProperties != null && this.m_sessionProperties.ContainsKey("AL8KW_MAX_IDEN_LENGTH"))
				{
					s = (string)this.m_sessionProperties["AL8KW_MAX_IDEN_LENGTH"];
				}
				int result = 0;
				int.TryParse(s, out result);
				return result;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0005B48C File Offset: 0x0005968C
		internal int MaxOpenCursors
		{
			get
			{
				string s = null;
				if (this.m_sessionProperties != null)
				{
					if (this.m_sessionProperties.ContainsKey("AUTH_MAX_OPEN_CURSORS"))
					{
						s = (string)this.m_sessionProperties["AUTH_MAX_OPEN_CURSORS"];
					}
				}
				else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AUTH_MAX_OPEN_CURSORS"))
				{
					s = (string)this.m_proxySessionProperties["AUTH_MAX_OPEN_CURSORS"];
				}
				int result = -1;
				int.TryParse(s, out result);
				return result;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0005B50C File Offset: 0x0005970C
		internal string OnsConfigFromDB
		{
			get
			{
				string result = null;
				if (this.m_sessionProperties != null)
				{
					if (this.m_sessionProperties.ContainsKey("AUTH_ONS_CONFIG"))
					{
						result = (string)this.m_sessionProperties["AUTH_ONS_CONFIG"];
					}
				}
				else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AUTH_ONS_CONFIG"))
				{
					result = (string)this.m_proxySessionProperties["AUTH_ONS_CONFIG"];
				}
				return result;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0005B580 File Offset: 0x00059780
		internal string OnsHASubscriberPattern
		{
			get
			{
				string result = null;
				if (this.m_sessionProperties != null)
				{
					if (this.m_sessionProperties.ContainsKey("AUTH_ONS_HA_SUBSCR_PATTERN"))
					{
						result = (string)this.m_sessionProperties["AUTH_ONS_HA_SUBSCR_PATTERN"];
					}
				}
				else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AUTH_ONS_HA_SUBSCR_PATTERN"))
				{
					result = (string)this.m_proxySessionProperties["AUTH_ONS_HA_SUBSCR_PATTERN"];
				}
				return result;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0005B5F4 File Offset: 0x000597F4
		internal string OnsRLBSubscriberPattern
		{
			get
			{
				string result = null;
				if (this.m_sessionProperties != null)
				{
					if (this.m_sessionProperties.ContainsKey("AUTH_ONS_RLB_SUBSCR_PATTERN"))
					{
						result = (string)this.m_sessionProperties["AUTH_ONS_RLB_SUBSCR_PATTERN"];
					}
				}
				else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AUTH_ONS_RLB_SUBSCR_PATTERN"))
				{
					result = (string)this.m_proxySessionProperties["AUTH_ONS_RLB_SUBSCR_PATTERN"];
				}
				return result;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0005B668 File Offset: 0x00059868
		internal string InstanceName
		{
			get
			{
				string text = null;
				if (this.m_sessionProperties != null && this.m_sessionProperties.ContainsKey("AUTH_SC_INSTANCE_NAME"))
				{
					text = (string)this.m_sessionProperties["AUTH_SC_INSTANCE_NAME"];
				}
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0005B6B4 File Offset: 0x000598B4
		internal override string ServiceName
		{
			get
			{
				string result = null;
				if (this.m_sessionProperties != null)
				{
					if (this.m_sessionProperties.ContainsKey("AUTH_SC_SERVICE_NAME"))
					{
						result = ((string)this.m_sessionProperties["AUTH_SC_SERVICE_NAME"]).ToLowerInvariant();
					}
				}
				else if (this.m_proxySessionProperties != null && this.m_proxySessionProperties.ContainsKey("AUTH_SC_SERVICE_NAME"))
				{
					result = ((string)this.m_proxySessionProperties["AUTH_SC_SERVICE_NAME"]).ToLowerInvariant();
				}
				return result;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0005B730 File Offset: 0x00059930
		internal string DatabaseDomainName
		{
			get
			{
				string text = string.Empty;
				if (this.m_sessionProperties != null && this.m_sessionProperties.ContainsKey("AUTH_SC_DB_DOMAIN"))
				{
					text = (string)this.m_sessionProperties["AUTH_SC_DB_DOMAIN"];
				}
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0005B780 File Offset: 0x00059980
		internal string DatabaseUniqueName
		{
			get
			{
				string text = string.Empty;
				if (this.m_sessionProperties != null && this.m_sessionProperties.ContainsKey("AUTH_SC_DBUNIQUE_NAME"))
				{
					text = (string)this.m_sessionProperties["AUTH_SC_DBUNIQUE_NAME"];
				}
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0005B7D0 File Offset: 0x000599D0
		internal string HostName
		{
			get
			{
				string text = string.Empty;
				if (this.m_sessionProperties != null && this.m_sessionProperties.ContainsKey("AUTH_SC_SERVER_HOST"))
				{
					text = (string)this.m_sessionProperties["AUTH_SC_SERVER_HOST"];
				}
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0005B820 File Offset: 0x00059A20
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0005B884 File Offset: 0x00059A84
		internal string DatabaseId
		{
			get
			{
				string result = string.Empty;
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_ID\0"))
				{
					result = this.m_sessionProperties["AUTH_DB_ID\0"].ToString();
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_ID"))
				{
					result = this.m_sessionProperties["AUTH_DB_ID"].ToString();
				}
				return result;
			}
			set
			{
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_ID\0"))
				{
					this.m_sessionProperties["AUTH_DB_ID\0"] = value;
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_ID"))
				{
					this.m_sessionProperties["AUTH_DB_ID"] = value;
				}
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0005B8D8 File Offset: 0x00059AD8
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0005B93C File Offset: 0x00059B3C
		internal string DatabaseMountId
		{
			get
			{
				string result = string.Empty;
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_MOUNT_ID\0"))
				{
					result = this.m_sessionProperties["AUTH_DB_MOUNT_ID\0"].ToString();
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_MOUNT_ID"))
				{
					result = this.m_sessionProperties["AUTH_DB_MOUNT_ID"].ToString();
				}
				return result;
			}
			set
			{
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_MOUNT_ID\0"))
				{
					this.m_sessionProperties["AUTH_DB_MOUNT_ID\0"] = value;
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_DB_MOUNT_ID"))
				{
					this.m_sessionProperties["AUTH_DB_MOUNT_ID"] = value;
				}
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0005B990 File Offset: 0x00059B90
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0005B9F4 File Offset: 0x00059BF4
		internal string GloballyUniqueDatabaseId
		{
			get
			{
				string result = string.Empty;
				if (this.m_sessionProperties.ContainsKey("AUTH_GLOBALLY_UNIQUE_DBID\0"))
				{
					result = this.m_sessionProperties["AUTH_GLOBALLY_UNIQUE_DBID\0"].ToString();
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_GLOBALLY_UNIQUE_DBID"))
				{
					result = this.m_sessionProperties["AUTH_GLOBALLY_UNIQUE_DBID"].ToString();
				}
				return result;
			}
			set
			{
				if (this.m_sessionProperties.ContainsKey("AUTH_GLOBALLY_UNIQUE_DBID\0"))
				{
					this.m_sessionProperties["AUTH_GLOBALLY_UNIQUE_DBID\0"] = value;
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_GLOBALLY_UNIQUE_DBID"))
				{
					this.m_sessionProperties["AUTH_GLOBALLY_UNIQUE_DBID"] = value;
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0005BA48 File Offset: 0x00059C48
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0005BAAC File Offset: 0x00059CAC
		internal string PDBUniqueId
		{
			get
			{
				string result = string.Empty;
				if (this.m_sessionProperties.ContainsKey("AUTH_PDB_UID\0"))
				{
					result = this.m_sessionProperties["AUTH_PDB_UID\0"].ToString();
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_PDB_UID"))
				{
					result = this.m_sessionProperties["AUTH_PDB_UID"].ToString();
				}
				return result;
			}
			set
			{
				if (this.m_sessionProperties.ContainsKey("AUTH_PDB_UID\0"))
				{
					this.m_sessionProperties["AUTH_PDB_UID\0"] = value;
				}
				if (this.m_sessionProperties.ContainsKey("AUTH_PDB_UID"))
				{
					this.m_sessionProperties["AUTH_PDB_UID"] = value;
				}
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0005BB00 File Offset: 0x00059D00
		internal void PurgeStatementCache(int targetSize = 0)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (this.m_statementCache != null && this.m_statementCache.Count > targetSize)
				{
					List<long> list = this.m_statementCache.Purge(targetSize);
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						string text = string.Format("Purging Stmt Cache to <{0}> size, Closing <{1}> cursors.", targetSize, list.Count);
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SvcObj, new string[]
						{
							text
						});
					}
					if (list.Count > 0)
					{
						this.AddCursorIdsToBeClosed(list);
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

		// Token: 0x06000897 RID: 2199 RVA: 0x0005BBDC File Offset: 0x00059DDC
		internal void AcceptStatementData(string stmtText)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			try
			{
				if (this.m_pm.m_bSelfTuning && !string.IsNullOrWhiteSpace(stmtText))
				{
					Dictionary<string, int> dictionary = null;
					lock (this.m_tuningLock)
					{
						if (!this.m_samples.ContainsKey(stmtText))
						{
							this.m_samples.Add(stmtText, 1);
						}
						else
						{
							Dictionary<string, int> samples;
							(samples = this.m_samples)[stmtText] = samples[stmtText] + 1;
						}
						this.m_samplesCount++;
						if (this.m_samplesCount >= 1000)
						{
							dictionary = this.m_samples;
							this.m_samplesCount = 0;
							this.m_samples = new Dictionary<string, int>();
						}
					}
					if (dictionary != null)
					{
						OracleTuner.Instance.SubmitData(this.m_pm, this.m_pm.m_recommendedSCS, (this.m_pm.m_pmListPR != null) ? this.m_pm.m_pmListPR.Count : 0, dictionary);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268500992, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
				}
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0005BD40 File Offset: 0x00059F40
		internal bool GetLastWarning(out string warningMsg, out int errorCode)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool result;
			try
			{
				warningMsg = null;
				errorCode = -1;
				if (this.m_marshallingEngine == null)
				{
					result = false;
				}
				else
				{
					errorCode = this.m_marshallingEngine.TTCErrorObject.ErrorCode;
					int ttiwrnflag = this.m_marshallingEngine.TTCErrorObject.m_TTIWRNFlag;
					if (ttiwrnflag != 0 && this.m_marshallingEngine.TTCErrorObject.ErrorMessage != null)
					{
						byte[] errorMessage = this.m_marshallingEngine.TTCErrorObject.ErrorMessage;
						char[] chars = this.m_marshallingEngine.m_charArrayPooler.Dequeue();
						string text = this.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(errorMessage, 0, errorMessage.Length, chars, true);
						this.m_marshallingEngine.m_charArrayPooler.Enqueue(ref chars);
						warningMsg = text;
					}
					short warningFlag = this.m_marshallingEngine.TTCErrorObject.m_warningFlag;
					if (warningFlag != 0)
					{
						if ((warningFlag & 32) == 32)
						{
							warningMsg = OracleStringResourceManager.GetErrorMesgWithErrCode(24344, new string[0]);
						}
						else if ((warningFlag & 16) == 16)
						{
							warningMsg = OracleStringResourceManager.GetErrorMesgWithErrCode(24348, new string[0]);
						}
						else if ((warningFlag & 4) == 4)
						{
							warningMsg = OracleStringResourceManager.GetErrorMesgWithErrCode(24347, new string[0]);
						}
					}
					if ((this.m_marshallingEngine.TTCErrorObject.m_flags & 4) == 4)
					{
						warningMsg = OracleStringResourceManager.GetErrorMesgWithErrCode(24439, new string[0]);
					}
					result = (warningMsg != null);
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

		// Token: 0x06000899 RID: 2201 RVA: 0x0005BF00 File Offset: 0x0005A100
		public bool IsConnectionAlive()
		{
			bool flag = true;
			try
			{
				if (this.m_marshallingEngine != null && this.m_marshallingEngine.TTCErrorObject != null)
				{
					int errorCode = this.m_marshallingEngine.TTCErrorObject.ErrorCode;
					if (errorCode == 22 || errorCode == 28 || errorCode == 45 || errorCode == 378 || errorCode == 602 || errorCode == 603 || errorCode == 1012 || errorCode == 1033 || errorCode == 1034 || errorCode == 1041 || errorCode == 1043 || errorCode == 1089 || errorCode == 1090 || errorCode == 1092 || errorCode == 3105 || errorCode == 3106 || errorCode == 3116 || errorCode == 3118 || errorCode == 3119 || errorCode == 3122 || errorCode == 3133 || errorCode == 3137 || errorCode == 3146 || errorCode == 21500 || errorCode == 27146 || errorCode == 28511 || errorCode == 3107 || errorCode == 3109 || errorCode == 3111 || errorCode == 3113 || errorCode == 3114 || errorCode == 3123 || errorCode == 3124 || errorCode == 3125 || errorCode == 3135 || errorCode == 3136 || errorCode == 3140 || errorCode == 3141 || errorCode == 12608 || errorCode == 12609 || (errorCode >= 6400 && errorCode <= 6420) || (errorCode >= 12150 && errorCode <= 12170) || errorCode == 12237 || errorCode == 12537 || errorCode == 12571 || errorCode == 12614 || errorCode == 12547 || errorCode == 13583 || errorCode == 12153 || errorCode == 12514)
					{
						flag = false;
					}
					else if (this.m_marshallingEngine.TTCErrorObject.m_bindErrors != null)
					{
						foreach (TTCArrayBindError ttcarrayBindError in this.m_marshallingEngine.TTCErrorObject.m_bindErrors)
						{
							errorCode = ttcarrayBindError.m_errorCode;
							if (errorCode == 22 || errorCode == 28 || errorCode == 45 || errorCode == 378 || errorCode == 602 || errorCode == 603 || errorCode == 1012 || errorCode == 1033 || errorCode == 1034 || errorCode == 1041 || errorCode == 1043 || errorCode == 1089 || errorCode == 1090 || errorCode == 1092 || errorCode == 3105 || errorCode == 3106 || errorCode == 3116 || errorCode == 3118 || errorCode == 3119 || errorCode == 3122 || errorCode == 3133 || errorCode == 3137 || errorCode == 3146 || errorCode == 21500 || errorCode == 27146 || errorCode == 28511 || errorCode == 3107 || errorCode == 3109 || errorCode == 3111 || errorCode == 3113 || errorCode == 3114 || errorCode == 3123 || errorCode == 3124 || errorCode == 3125 || errorCode == 3135 || errorCode == 3136 || errorCode == 3140 || errorCode == 3141 || errorCode == 12608 || errorCode == 12609 || (errorCode >= 6400 && errorCode <= 6420) || (errorCode >= 12150 && errorCode <= 12170) || errorCode == 12237 || errorCode == 12537 || errorCode == 12571 || errorCode == 12614 || errorCode == 12547 || errorCode == 13583 || errorCode == 12153 || errorCode == 12514)
							{
								flag = false;
								break;
							}
						}
					}
				}
				if (flag)
				{
					flag = this.IsConnectionAlive(this.m_lastErrorNum);
				}
			}
			catch
			{
			}
			return flag;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0005C3D4 File Offset: 0x0005A5D4
		private bool IsConnectionAlive(int errNum)
		{
			bool result = true;
			try
			{
				if (errNum == 22 || errNum == 28 || errNum == 45 || errNum == 378 || errNum == 602 || errNum == 603 || errNum == 1012 || errNum == 1033 || errNum == 1034 || errNum == 1041 || errNum == 1043 || errNum == 1089 || errNum == 1090 || errNum == 1092 || errNum == 3105 || errNum == 3106 || errNum == 3116 || errNum == 3118 || errNum == 3119 || errNum == 3122 || errNum == 3133 || errNum == 3137 || errNum == 3146 || errNum == 21500 || errNum == 27146 || errNum == 28511 || errNum == 3107 || errNum == 3109 || errNum == 3111 || errNum == 3113 || errNum == 3114 || errNum == 3123 || errNum == 3124 || errNum == 3125 || errNum == 3135 || errNum == 3136 || errNum == 3140 || errNum == 3141 || errNum == 12608 || errNum == 12609 || (errNum >= 6400 && errNum <= 6420) || (errNum >= 12150 && errNum <= 12170) || errNum == 12237 || errNum == 12537 || errNum == 12571 || errNum == 12614 || errNum == 12547 || errNum == 13583 || errNum == 12153 || errNum == 12514)
				{
					result = false;
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0005C60C File Offset: 0x0005A80C
		internal object TemporaryLobReferenceGet(string lobId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			object obj = null;
			object result;
			try
			{
				lock (this.m_lockForLists)
				{
					obj = this.m_temporaryLobReferences[lobId];
				}
				result = obj;
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

		// Token: 0x0600089C RID: 2204 RVA: 0x0005C698 File Offset: 0x0005A898
		internal void TemporaryLobReferenceAdd(string lobId, object lobImpl, bool bumpRefCount = true)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				lock (this.m_lockForLists)
				{
					this.m_temporaryLobReferences.Add(lobId, lobImpl);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0005C720 File Offset: 0x0005A920
		internal void TemporaryLobReferenceRemove(string lobId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				lock (this.m_lockForLists)
				{
					this.m_temporaryLobReferences.Remove(lobId);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x04000B29 RID: 2857
		private const long SYSDBA = 8L;

		// Token: 0x04000B2A RID: 2858
		private const long SYSOPER = 16L;

		// Token: 0x04000B2B RID: 2859
		internal const int Default_DotNetCharSet = 1000;

		// Token: 0x04000B2C RID: 2860
		internal const int Default_DotNetNCharSet = 1000;

		// Token: 0x04000B2D RID: 2861
		internal const string SERVER_POOLED = "POOLED";

		// Token: 0x04000B2E RID: 2862
		internal const long TTIEOCFRO = 1L;

		// Token: 0x04000B2F RID: 2863
		internal const long TTIEOCCUR = 2L;

		// Token: 0x04000B30 RID: 2864
		internal const long TTIEOCDON = 4L;

		// Token: 0x04000B31 RID: 2865
		internal const long TTIEOCECT = 8L;

		// Token: 0x04000B32 RID: 2866
		internal const long TTIEOCFSE = 16L;

		// Token: 0x04000B33 RID: 2867
		internal const long TTIEOCFPR = 32L;

		// Token: 0x04000B34 RID: 2868
		internal const long TTIEOCFSW = 64L;

		// Token: 0x04000B35 RID: 2869
		internal const long TTIEOCFMF = 128L;

		// Token: 0x04000B36 RID: 2870
		internal const long TTIEOCETS = 256L;

		// Token: 0x04000B37 RID: 2871
		internal const long TTIEOCFCP = 512L;

		// Token: 0x04000B38 RID: 2872
		internal const long TTIEOCFIV = 2147483648L;

		// Token: 0x04000B39 RID: 2873
		internal const byte END_TO_END_CID_INDEX = 0;

		// Token: 0x04000B3A RID: 2874
		internal const byte END_TO_END_MODULE_INDEX = 1;

		// Token: 0x04000B3B RID: 2875
		internal const byte END_TO_END_ACTION_INDEX = 2;

		// Token: 0x04000B3C RID: 2876
		internal const byte END_TO_END_CLINFO_INDEX = 3;

		// Token: 0x04000B3D RID: 2877
		internal const byte END_TO_END_STATE_INDEX_MAX = 4;

		// Token: 0x04000B3F RID: 2879
		private object lockOnWeakReferenceObjList = new object();

		// Token: 0x04000B40 RID: 2880
		private List<WeakReference> listofWeakReferenceObj = new List<WeakReference>();

		// Token: 0x04000B41 RID: 2881
		private ObxmlProcessor m_obxmlProcessor = new ObxmlProcessor();

		// Token: 0x04000B42 RID: 2882
		private object m_readerImplLock = new object();

		// Token: 0x04000B43 RID: 2883
		internal bool m_preferredReaderImplTaken;

		// Token: 0x04000B44 RID: 2884
		private OracleDataReaderImpl m_preferredReaderImpl;

		// Token: 0x04000B45 RID: 2885
		private object m_commandImplLock = new object();

		// Token: 0x04000B46 RID: 2886
		internal bool m_preferredCommandImplTaken;

		// Token: 0x04000B47 RID: 2887
		private OracleCommandImpl m_preferredCommandImpl = new OracleCommandImpl
		{
			m_bPooled = true
		};

		// Token: 0x04000B48 RID: 2888
		internal static char[] delim = new char[]
		{
			' ',
			'\t',
			'"'
		};

		// Token: 0x04000B49 RID: 2889
		internal static HashSet<string> dbsRegisteredForHA = new HashSet<string>();

		// Token: 0x04000B4A RID: 2890
		internal static HashSet<string> servicesRegisteredForRLB = new HashSet<string>();

		// Token: 0x04000B4B RID: 2891
		internal bool m_bConnected;

		// Token: 0x04000B4C RID: 2892
		internal OracleCommunication m_oracleCommunication;

		// Token: 0x04000B4D RID: 2893
		internal MarshallingEngine m_marshallingEngine;

		// Token: 0x04000B4E RID: 2894
		protected Hashtable m_proxySessionProperties;

		// Token: 0x04000B4F RID: 2895
		protected Hashtable m_sessionProperties;

		// Token: 0x04000B50 RID: 2896
		internal AutoResetEvent m_connectionFreeToUseEvent = new AutoResetEvent(true);

		// Token: 0x04000B51 RID: 2897
		internal WaitHandle[] m_waitHandlesToStartExecution;

		// Token: 0x04000B52 RID: 2898
		internal object m_tuningLock = new object();

		// Token: 0x04000B53 RID: 2899
		private Dictionary<string, int> m_samples = new Dictionary<string, int>();

		// Token: 0x04000B54 RID: 2900
		private int m_samplesCount;

		// Token: 0x04000B55 RID: 2901
		internal static object m_lockForHARLBRegistration = new object();

		// Token: 0x04000B56 RID: 2902
		internal bool m_autoCommit;

		// Token: 0x04000B57 RID: 2903
		internal System.Data.IsolationLevel m_currentIsolationLvl = System.Data.IsolationLevel.ReadCommitted;

		// Token: 0x04000B58 RID: 2904
		private TTCProtocolNegotiation m_protoNeg;

		// Token: 0x04000B59 RID: 2905
		private TTCDataTypeNegotiation m_dtyNeg;

		// Token: 0x04000B5A RID: 2906
		private TTCVersion m_ttcVersion;

		// Token: 0x04000B5B RID: 2907
		private TTCFetch m_ttcFetch;

		// Token: 0x04000B5C RID: 2908
		private TTCAuthenticate m_ttcAuth;

		// Token: 0x04000B5D RID: 2909
		private TTCExecuteSql m_executeSql;

		// Token: 0x04000B5E RID: 2910
		private TTCSimpleOperations m_ttcSimplOp;

		// Token: 0x04000B5F RID: 2911
		private TTCOPing m_ttcOPing;

		// Token: 0x04000B60 RID: 2912
		private TTCNotification m_ttcNotification;

		// Token: 0x04000B61 RID: 2913
		private TTCClose m_ttcClose;

		// Token: 0x04000B62 RID: 2914
		private TTCCancel m_ttcCancel;

		// Token: 0x04000B63 RID: 2915
		private TTCSwitchSession m_ttcSwitchSession;

		// Token: 0x04000B64 RID: 2916
		private TTCEndToEndMetrics m_ttcEndToEndMetrics;

		// Token: 0x04000B65 RID: 2917
		private TTCTransactionSE m_ttcTransactionSE;

		// Token: 0x04000B66 RID: 2918
		private TTCTransactionEN m_ttcTransactionEN;

		// Token: 0x04000B67 RID: 2919
		private TTCSessionGet m_ttcSessionGet;

		// Token: 0x04000B68 RID: 2920
		private TTCSessionRelease m_ttcSessionRelease;

		// Token: 0x04000B69 RID: 2921
		internal long m_logonMode;

		// Token: 0x04000B6A RID: 2922
		internal short m_serverCharacterSet;

		// Token: 0x04000B6B RID: 2923
		internal short m_serverNCharSet;

		// Token: 0x04000B6C RID: 2924
		internal byte m_serverFlags;

		// Token: 0x04000B6D RID: 2925
		internal byte[] m_serverCompiletimeCapabilities;

		// Token: 0x04000B6E RID: 2926
		internal byte[] m_serverRuntimeCapabilities;

		// Token: 0x04000B6F RID: 2927
		internal bool m_b32kTypeSupported;

		// Token: 0x04000B70 RID: 2928
		internal long m_endOfCallStatus;

		// Token: 0x04000B71 RID: 2929
		internal string[] m_endToEndMetrics = new string[4];

		// Token: 0x04000B72 RID: 2930
		internal bool[] m_endToEndMetricsModified = new bool[4];

		// Token: 0x04000B73 RID: 2931
		internal object m_lockForLists = new object();

		// Token: 0x04000B74 RID: 2932
		private ArrayList m_tempLOBsToBeFreed = new ArrayList(10);

		// Token: 0x04000B75 RID: 2933
		private ArrayList m_cursorsToBeClosed = new ArrayList(10);

		// Token: 0x04000B76 RID: 2934
		private ArrayList m_cursorsToBeCancelled = new ArrayList(10);

		// Token: 0x04000B77 RID: 2935
		internal StatementCache m_statementCache;

		// Token: 0x04000B78 RID: 2936
		internal OracleGlobalizationImpl m_oracleGlobalizationImpl;

		// Token: 0x04000B79 RID: 2937
		internal Transaction m_lastEnlistedTransaction;

		// Token: 0x04000B7A RID: 2938
		internal string m_onsConfigFromDb = string.Empty;

		// Token: 0x04000B7B RID: 2939
		internal string m_onsHASubscrPatter = string.Empty;

		// Token: 0x04000B7C RID: 2940
		internal string m_onsRLBSubscPatter = string.Empty;

		// Token: 0x04000B7D RID: 2941
		internal int m_maxIdentifierLength = 30;

		// Token: 0x04000B7E RID: 2942
		internal string m_editionName;

		// Token: 0x04000B7F RID: 2943
		internal string m_drcpConnectionClass;

		// Token: 0x04000B80 RID: 2944
		internal string m_drcpTagName;

		// Token: 0x04000B81 RID: 2945
		internal bool m_bDRCPUseMultitag;

		// Token: 0x04000B82 RID: 2946
		internal string m_drcpSessionPurity = "2";

		// Token: 0x04000B83 RID: 2947
		internal string m_drcpPLSQLCallback;

		// Token: 0x04000B84 RID: 2948
		internal Hashtable m_temporaryLobReferences = new Hashtable();

		// Token: 0x04000B85 RID: 2949
		internal OracleTimeZoneInfo? m_dbTimeZoneInfo = null;

		// Token: 0x04000B86 RID: 2950
		internal int m_lastErrorNum;

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x0600089F RID: 2207
		internal delegate void OracleConnectionCloseEventHandler();

		// Token: 0x020000D6 RID: 214
		internal class HARLBCallbackRegisterData
		{
			// Token: 0x04000B87 RID: 2951
			internal string m_serviceName;

			// Token: 0x04000B88 RID: 2952
			internal string m_databaseName;

			// Token: 0x04000B89 RID: 2953
			internal int m_conTimeout;

			// Token: 0x04000B8A RID: 2954
			internal string m_onsHASubscrPatter;

			// Token: 0x04000B8B RID: 2955
			internal string m_onsRLBSubscrPatter;

			// Token: 0x04000B8C RID: 2956
			internal string m_onsConfigFromDb;

			// Token: 0x04000B8D RID: 2957
			internal bool m_bRegisterForHA;

			// Token: 0x04000B8E RID: 2958
			internal bool m_bRegisterForRLB;
		}
	}
}
