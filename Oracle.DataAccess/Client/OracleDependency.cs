using System;
using System.Collections;
using System.Data;
using System.Net;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000093 RID: 147
	public class OracleDependency
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x000474F4 File Offset: 0x000464F4
		static OracleDependency()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			if (OracleDependency.s_Listener.port != -1)
			{
				OracleDependency.Port = OracleDependency.s_Listener.port;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00047558 File Offset: 0x00046558
		public OracleDependency()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDependency()\n"
				});
			}
			this.Initialize(OracleNotificationRequest.s_bDefIsNotifiedOnce, (long)OracleNotificationRequest.s_DefRegTimeout, OracleNotificationRequest.s_bDefIsPersistent);
			OracleDependency.s_depTable.Add(this.m_guid, this);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDependency()\n"
				});
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000475CC File Offset: 0x000465CC
		public OracleDependency(OracleCommand cmd) : this(cmd, OracleNotificationRequest.s_bDefIsNotifiedOnce, (long)OracleNotificationRequest.s_DefRegTimeout, OracleNotificationRequest.s_bDefIsPersistent)
		{
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000475E8 File Offset: 0x000465E8
		public OracleDependency(OracleCommand cmd, bool isNotifiedOnce, long timeout, bool isPersistent)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDependency()\n"
				});
			}
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (timeout < 0L || timeout > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			if (cmd.Notification != null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.NTFN_CMD_ALREADY_EXIST, new string[0]));
			}
			this.Initialize(isNotifiedOnce, timeout, isPersistent);
			OracleNotificationRequest oracleNotificationRequest = new OracleNotificationRequest(OracleNotificationRequest.s_ChangedNotificationName, this.m_invalidationStr, isNotifiedOnce, timeout, isPersistent, this.m_opoSubscrCtx);
			cmd.m_NTFNReq = oracleNotificationRequest;
			OracleDependency.s_depTable.Add(this.m_guid, this);
			if (OracleNotificationRequest.s_idTable[oracleNotificationRequest.Id] == null)
			{
				OracleNotificationRequest.s_idTable.Add(oracleNotificationRequest.Id, this.m_guid);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDependency()\n"
				});
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x000476DB File Offset: 0x000466DB
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x000476E8 File Offset: 0x000466E8
		public static int Port
		{
			get
			{
				return OracleDependency.s_Listener.port;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("Port");
				}
				lock (OracleDependency.s_Listener)
				{
					if (!OracleDependency.s_Listener.bListenerStart)
					{
						try
						{
							OpsSubscr.SetPort(OracleDependency.s_opsEnvCtx, OracleDependency.s_opsErrCtx, (uint)value);
							OracleDependency.s_Listener.port = value;
							goto IL_6F;
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						goto IL_59;
						IL_6F:
						return;
					}
					IL_59:
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.NTFN_LISTENER_ALREADY_STARTED, new string[0]));
				}
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0004778C File Offset: 0x0004678C
		public static OracleDependency GetOracleDependency(string guid)
		{
			if (guid == null)
			{
				throw new ArgumentNullException("guid");
			}
			return (OracleDependency)OracleDependency.s_depTable[guid];
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x000477B9 File Offset: 0x000467B9
		public string DataSource
		{
			get
			{
				return this.m_dataSource;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x000477C4 File Offset: 0x000467C4
		public bool HasChanges
		{
			get
			{
				bool bHasChanges = this.m_bHasChanges;
				this.m_bHasChanges = false;
				return bHasChanges;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x000477E0 File Offset: 0x000467E0
		public string Id
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x000477E8 File Offset: 0x000467E8
		private string InvalidationString
		{
			get
			{
				return this.m_invalidationStr;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x000477F0 File Offset: 0x000467F0
		public bool IsEnabled
		{
			get
			{
				return this.m_bIsEnabled;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x000477F8 File Offset: 0x000467F8
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00047800 File Offset: 0x00046800
		public bool QueryBasedNotification
		{
			get
			{
				return this.m_bQueryBasedNTFN;
			}
			set
			{
				if (this.m_bQueryBasedNTFN != value)
				{
					this.m_bQueryBasedNTFN = value;
				}
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00047812 File Offset: 0x00046812
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0004781A File Offset: 0x0004681A
		public OracleRowidInfo RowidInfo
		{
			get
			{
				return this.m_OracleRowidInfo;
			}
			set
			{
				this.m_OracleRowidInfo = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00047823 File Offset: 0x00046823
		public ArrayList RegisteredResources
		{
			get
			{
				return (ArrayList)this.m_regList.Clone();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00047835 File Offset: 0x00046835
		public ArrayList RegisteredQueryIDs
		{
			get
			{
				return (ArrayList)this.m_queryIDList.Clone();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00047847 File Offset: 0x00046847
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000755 RID: 1877 RVA: 0x00047850 File Offset: 0x00046850
		// (remove) Token: 0x06000756 RID: 1878 RVA: 0x00047888 File Offset: 0x00046888
		public event OnChangeEventHandler OnChange;

		// Token: 0x06000757 RID: 1879 RVA: 0x000478C0 File Offset: 0x000468C0
		public void AddCommandDependency(OracleCommand cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (cmd.Notification != null && OracleNotificationRequest.s_idTable[cmd.Notification.Id] != null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.NTFN_CMD_ALREADY_EXIST, new string[0]));
			}
			cmd.m_NTFNReq = new OracleNotificationRequest(OracleNotificationRequest.s_ChangedNotificationName, this.m_invalidationStr, this.m_bIsNotifiedOnce, this.m_timeout, this.m_bIsPersistent, this.m_opoSubscrCtx);
			if (OracleDependency.s_depTable[this.m_guid] == null)
			{
				OracleDependency.s_depTable.Add(this.m_guid, this);
			}
			if (OracleNotificationRequest.s_idTable[cmd.m_NTFNReq.Id] == null)
			{
				OracleNotificationRequest.s_idTable.Add(cmd.m_NTFNReq.Id, this.m_guid);
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00047994 File Offset: 0x00046994
		public void RemoveRegistration(OracleConnection conn)
		{
			if (!this.m_bIsRegistered)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.NTFN_REG_NOTVALID, new string[0]));
			}
			if (conn.State != ConnectionState.Open)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			int num = 0;
			try
			{
				num = OpsSubscr.UnRegister(conn.m_opoConCtx.opsConCtx, conn.m_opoConCtx.opsErrCtx, this.m_opoSubscrCtx.opsSubscrCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
				}
				this.m_bIsRegistered = false;
				this.m_bIsEnabled = false;
			}
			try
			{
				if (OracleDependency.s_depTable[this.m_guid] != null)
				{
					OracleDependency.s_depTable.Remove(this.m_guid);
				}
			}
			catch
			{
			}
			try
			{
				if (OracleNotificationRequest.s_idTable[this.m_invalidationStr] != null)
				{
					OracleNotificationRequest.s_idTable.Remove(this.m_invalidationStr);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00047AC4 File Offset: 0x00046AC4
		internal static IntPtr CreateDependencyEnv()
		{
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			try
			{
				OpsSubscr.AllocGlobalCtx(out zero, out zero2);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			OracleDependency.s_onChangeOpsCallback = new OnChangeCallback(OracleDependency.OnChangeOpsCallback_fn);
			try
			{
				num = OpsChgNTFN.RegisterNotificationCallback(OracleDependency.s_onChangeOpsCallback);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					new OracleException(num, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
				}
			}
			OracleDependency.s_opsErrCtx = zero2;
			return zero;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00047B80 File Offset: 0x00046B80
		internal static string GetMachineAddress()
		{
			IPHostEntry iphostEntry = Dns.Resolve(Dns.GetHostName());
			IPAddress ipaddress = iphostEntry.AddressList[0];
			return ipaddress.ToString();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00047BA8 File Offset: 0x00046BA8
		internal static OracleDependency GetOracleDependencyFromNTFNId(string id)
		{
			string text = (string)OracleNotificationRequest.s_idTable[id];
			if (text != null)
			{
				return (OracleDependency)OracleDependency.s_depTable[text];
			}
			return null;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00047BE0 File Offset: 0x00046BE0
		internal static void OnChangeOpsCallback_fn(string id, IntPtr opsErrCtx, IntPtr opsChgNTFNDesc, NotiVal notiVal)
		{
			int num = 0;
			OracleDependency oracleDependencyFromNTFNId = OracleDependency.GetOracleDependencyFromNTFNId(id);
			if (oracleDependencyFromNTFNId == null)
			{
				return;
			}
			if (oracleDependencyFromNTFNId.m_bIsNotifiedOnce)
			{
				oracleDependencyFromNTFNId.m_bIsEnabled = false;
				oracleDependencyFromNTFNId.m_bIsRegistered = false;
				try
				{
					OracleDependency.s_depTable.Remove(oracleDependencyFromNTFNId.m_guid);
				}
				catch
				{
				}
				try
				{
					OracleNotificationRequest.s_idTable.Remove(id);
				}
				catch
				{
				}
			}
			if (oracleDependencyFromNTFNId.OnChange == null)
			{
				if (notiVal.source == OracleNotificationSource.Object || notiVal.source == OracleNotificationSource.Data)
				{
					oracleDependencyFromNTFNId.m_bHasChanges = true;
				}
				return;
			}
			OracleNotificationEventArgs oracleNotificationEventArgs = new OracleNotificationEventArgs();
			oracleNotificationEventArgs.m_type = notiVal.type;
			oracleNotificationEventArgs.m_source = notiVal.source;
			if (notiVal.source == OracleNotificationSource.Database || notiVal.source == OracleNotificationSource.Subscription)
			{
				if (notiVal.source == OracleNotificationSource.Subscription && notiVal.info == OracleNotificationInfo.End)
				{
					oracleDependencyFromNTFNId.m_bIsEnabled = false;
					oracleDependencyFromNTFNId.m_bIsRegistered = false;
					try
					{
						OracleDependency.s_depTable.Remove(oracleDependencyFromNTFNId.m_guid);
					}
					catch
					{
					}
					try
					{
						OracleNotificationRequest.s_idTable.Remove(id);
					}
					catch
					{
					}
				}
				oracleNotificationEventArgs.m_info = notiVal.info;
				oracleDependencyFromNTFNId.FiredEvent(oracleNotificationEventArgs);
				return;
			}
			oracleDependencyFromNTFNId.m_bHasChanges = true;
			if (notiVal.type == OracleNotificationType.Query)
			{
				ArrayList arrayList = new ArrayList();
				int num2 = 0;
				for (int i = 0; i < notiVal.numQueries; i++)
				{
					long queryid = 0L;
					IntPtr zero = IntPtr.Zero;
					try
					{
						num = OpsChgNTFN.GetQueryIds(OracleDependency.s_opsEnvCtx, opsErrCtx, opsChgNTFNDesc, i, ref zero, ref queryid, ref notiVal.numTables);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							throw new OracleException(num, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
						}
					}
					NotiTblVal[] array = new NotiTblVal[notiVal.numTables];
					GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					IntPtr notiTblDescs = gchandle.AddrOfPinnedObject();
					IntPtr zero2 = IntPtr.Zero;
					try
					{
						num = OpsChgNTFN.GetTableInfos(OracleDependency.s_opsEnvCtx, opsErrCtx, notiVal.numTables, notiVal.type, zero, notiTblDescs, out zero2);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					finally
					{
						if (gchandle.IsAllocated)
						{
							gchandle.Free();
						}
						if (num != 0)
						{
							throw new OracleException(num, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
						}
					}
					IntPtr intPtr = zero2;
					NotiTblRef notiTblRef = new NotiTblRef();
					for (int j = 0; j < notiVal.numTables; j++)
					{
						Marshal.PtrToStructure(intPtr, notiTblRef);
						arrayList.Add(notiTblRef.tableName);
						intPtr = (IntPtr)((int)intPtr + Marshal.SizeOf(notiTblRef));
					}
					try
					{
						OpsChgNTFN.FreeNotiTblRefs(ref zero2, notiVal.numTables);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					for (int k = 0; k < notiVal.numTables; k++)
					{
						if (array[k].numRows == 0)
						{
							if ((array[k].info & OracleNotificationInfo.Drop) == OracleNotificationInfo.Drop)
							{
								oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Drop, null, queryid);
							}
							if ((array[k].info & OracleNotificationInfo.Alter) == OracleNotificationInfo.Alter)
							{
								oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Alter, null, queryid);
							}
							if ((array[k].info & OracleNotificationInfo.Update) == OracleNotificationInfo.Update)
							{
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Update, null, queryid);
							}
							if ((array[k].info & OracleNotificationInfo.Insert) == OracleNotificationInfo.Insert)
							{
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Insert, null, queryid);
							}
							if ((array[k].info & OracleNotificationInfo.Delete) == OracleNotificationInfo.Delete)
							{
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Delete, null, queryid);
							}
						}
						else
						{
							if ((array[k].info & OracleNotificationInfo.Drop) == OracleNotificationInfo.Drop)
							{
								oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Drop, null, queryid);
							}
							if ((array[k].info & OracleNotificationInfo.Alter) == OracleNotificationInfo.Alter)
							{
								oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), OracleNotificationInfo.Alter, null, queryid);
							}
							NotiRowVal[] array2 = new NotiRowVal[array[k].numRows];
							GCHandle gchandle2 = GCHandle.Alloc(array2, GCHandleType.Pinned);
							IntPtr notiRowDescs = gchandle2.AddrOfPinnedObject();
							IntPtr zero3 = IntPtr.Zero;
							try
							{
								num = OpsChgNTFN.GetRowInfos(OracleDependency.s_opsEnvCtx, opsErrCtx, array[k].numRows, array[k].pOpsTableChangeDesc, notiRowDescs, out zero3);
							}
							catch (Exception ex4)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex4);
								}
								throw;
							}
							finally
							{
								if (gchandle2.IsAllocated)
								{
									gchandle2.Free();
								}
								if (num != 0)
								{
									throw new OracleException(num, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
								}
							}
							NotiRowRef notiRowRef = new NotiRowRef();
							IntPtr intPtr2 = zero3;
							for (int l = 0; l < array[k].numRows; l++)
							{
								Marshal.PtrToStructure(intPtr2, notiRowRef);
								oracleNotificationEventArgs.AddRowDetail(arrayList[num2].ToString(), array2[l].info, notiRowRef.rowid, queryid);
								intPtr2 = (IntPtr)((int)intPtr2 + Marshal.SizeOf(notiRowRef));
							}
							try
							{
								OpsChgNTFN.FreeNotiRowRefs(ref zero3, array[k].numRows);
							}
							catch (Exception ex5)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex5);
								}
							}
						}
						num2++;
					}
				}
				oracleNotificationEventArgs.m_resources = (string[])arrayList.ToArray(typeof(string));
				arrayList.Clear();
				arrayList = null;
			}
			else
			{
				NotiTblVal[] array3 = new NotiTblVal[notiVal.numTables];
				GCHandle gchandle3 = GCHandle.Alloc(array3, GCHandleType.Pinned);
				IntPtr notiTblDescs2 = gchandle3.AddrOfPinnedObject();
				IntPtr zero4 = IntPtr.Zero;
				try
				{
					num = OpsChgNTFN.GetTableInfos(OracleDependency.s_opsEnvCtx, opsErrCtx, notiVal.numTables, notiVal.type, opsChgNTFNDesc, notiTblDescs2, out zero4);
				}
				catch (Exception ex6)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex6);
					}
					throw;
				}
				finally
				{
					if (gchandle3.IsAllocated)
					{
						gchandle3.Free();
					}
					if (num != 0)
					{
						throw new OracleException(num, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
					}
				}
				oracleNotificationEventArgs.m_resources = new string[notiVal.numTables];
				IntPtr intPtr3 = zero4;
				NotiTblRef notiTblRef2 = new NotiTblRef();
				for (int m = 0; m < notiVal.numTables; m++)
				{
					Marshal.PtrToStructure(intPtr3, notiTblRef2);
					oracleNotificationEventArgs.m_resources[m] = notiTblRef2.tableName;
					intPtr3 = (IntPtr)((int)intPtr3 + Marshal.SizeOf(notiTblRef2));
				}
				try
				{
					OpsChgNTFN.FreeNotiTblRefs(ref zero4, notiVal.numTables);
				}
				catch (Exception ex7)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex7);
					}
				}
				for (int n = 0; n < notiVal.numTables; n++)
				{
					if (array3[n].numRows == 0)
					{
						if ((array3[n].info & OracleNotificationInfo.Drop) == OracleNotificationInfo.Drop)
						{
							oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Drop, null, 0L);
						}
						if ((array3[n].info & OracleNotificationInfo.Alter) == OracleNotificationInfo.Alter)
						{
							oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Alter, null, 0L);
						}
						if ((array3[n].info & OracleNotificationInfo.Update) == OracleNotificationInfo.Update)
						{
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Update, null, 0L);
						}
						if ((array3[n].info & OracleNotificationInfo.Insert) == OracleNotificationInfo.Insert)
						{
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Insert, null, 0L);
						}
						if ((array3[n].info & OracleNotificationInfo.Delete) == OracleNotificationInfo.Delete)
						{
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Delete, null, 0L);
						}
					}
					else
					{
						if ((array3[n].info & OracleNotificationInfo.Drop) == OracleNotificationInfo.Drop)
						{
							oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Drop, null, 0L);
						}
						if ((array3[n].info & OracleNotificationInfo.Alter) == OracleNotificationInfo.Alter)
						{
							oracleNotificationEventArgs.m_source = OracleNotificationSource.Object;
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], OracleNotificationInfo.Alter, null, 0L);
						}
						NotiRowVal[] array4 = new NotiRowVal[array3[n].numRows];
						GCHandle gchandle4 = GCHandle.Alloc(array4, GCHandleType.Pinned);
						IntPtr notiRowDescs2 = gchandle4.AddrOfPinnedObject();
						IntPtr zero5 = IntPtr.Zero;
						try
						{
							num = OpsChgNTFN.GetRowInfos(OracleDependency.s_opsEnvCtx, opsErrCtx, array3[n].numRows, array3[n].pOpsTableChangeDesc, notiRowDescs2, out zero5);
						}
						catch (Exception ex8)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex8);
							}
							throw;
						}
						finally
						{
							if (gchandle4.IsAllocated)
							{
								gchandle4.Free();
							}
							if (num != 0)
							{
								throw new OracleException(num, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(num, new string[0]));
							}
						}
						NotiRowRef notiRowRef2 = new NotiRowRef();
						IntPtr intPtr4 = zero5;
						for (int num3 = 0; num3 < array3[n].numRows; num3++)
						{
							Marshal.PtrToStructure(intPtr4, notiRowRef2);
							oracleNotificationEventArgs.AddRowDetail(oracleNotificationEventArgs.m_resources[n], array4[num3].info, notiRowRef2.rowid, 0L);
							intPtr4 = (IntPtr)((int)intPtr4 + Marshal.SizeOf(notiRowRef2));
						}
						try
						{
							OpsChgNTFN.FreeNotiRowRefs(ref zero5, array3[n].numRows);
						}
						catch (Exception ex9)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex9);
							}
						}
					}
				}
			}
			if (oracleNotificationEventArgs.m_details.Rows.Count > 0)
			{
				oracleNotificationEventArgs.m_info = (OracleNotificationInfo)oracleNotificationEventArgs.m_details.Rows[0][1];
			}
			else
			{
				oracleNotificationEventArgs.m_info = OracleNotificationInfo.Error;
			}
			oracleDependencyFromNTFNId.FiredEvent(oracleNotificationEventArgs);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000486B0 File Offset: 0x000476B0
		internal void Initialize(bool isNotifiedOnce, long timeout, bool isPersistent)
		{
			this.m_guid = Guid.NewGuid().ToString();
			this.m_bIsRegistered = false;
			this.m_invalidationStr = string.Concat(new string[]
			{
				"<MachineAddress>tcp://",
				OracleDependency.s_machineAddress,
				"</MachineAddress><key>",
				this.m_guid,
				"</key>"
			});
			this.m_dataSource = "";
			this.m_userName = "";
			this.m_bHasChanges = false;
			this.m_bIsEnabled = false;
			this.m_bQueryBasedNTFN = true;
			this.m_OracleRowidInfo = OracleRowidInfo.Default;
			this.m_queryIDList = new ArrayList();
			this.m_regList = new ArrayList();
			this.m_opoSubscrCtx = new OpoSubscrCtx();
			try
			{
				OpsSubscr.AllocCtx(OracleDependency.s_opsEnvCtx, out this.m_opoSubscrCtx.opsErrCtx, out this.m_opoSubscrCtx.opsSubscrCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			this.m_bIsNotifiedOnce = isNotifiedOnce;
			this.m_bIsPersistent = isPersistent;
			this.m_timeout = timeout;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000487C4 File Offset: 0x000477C4
		internal int GetPort()
		{
			uint result = 0U;
			try
			{
				OpsSubscr.GetPort(OracleDependency.s_opsEnvCtx, OracleDependency.s_opsErrCtx, out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			return (int)result;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00048808 File Offset: 0x00047808
		internal void SetRegisterInfo(string username, string dataSource, bool IsNotifiedOnce, bool IsPersistent, long timeout)
		{
			lock (OracleDependency.s_Listener)
			{
				if (!OracleDependency.s_Listener.bListenerStart)
				{
					OracleDependency.s_Listener.port = this.GetPort();
					OracleDependency.s_Listener.bListenerStart = true;
				}
			}
			if (!this.m_bIsRegistered)
			{
				this.m_userName = username;
				this.m_dataSource = dataSource;
				this.m_bIsRegistered = true;
				this.m_bIsNotifiedOnce = IsNotifiedOnce;
				this.m_bIsPersistent = IsPersistent;
				this.m_timeout = timeout;
				this.m_regList.Clear();
				this.m_queryIDList.Clear();
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000488B4 File Offset: 0x000478B4
		public void FiredEvent(OracleNotificationEventArgs e)
		{
			if (this.OnChange != null)
			{
				try
				{
					this.OnChange(this, e);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400041D RID: 1053
		internal static IntPtr s_opsErrCtx;

		// Token: 0x0400041E RID: 1054
		internal static IntPtr s_opsEnvCtx = OracleDependency.CreateDependencyEnv();

		// Token: 0x0400041F RID: 1055
		internal static string s_machineAddress = OracleDependency.GetMachineAddress();

		// Token: 0x04000420 RID: 1056
		internal static Hashtable s_depTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000421 RID: 1057
		internal static SubscrListenerInfo s_Listener = new SubscrListenerInfo();

		// Token: 0x04000422 RID: 1058
		internal static OnChangeCallback s_onChangeOpsCallback;

		// Token: 0x04000423 RID: 1059
		internal OpoSubscrCtx m_opoSubscrCtx;

		// Token: 0x04000424 RID: 1060
		internal bool m_bIsRegistered;

		// Token: 0x04000425 RID: 1061
		internal string m_guid;

		// Token: 0x04000426 RID: 1062
		internal string m_invalidationStr;

		// Token: 0x04000427 RID: 1063
		internal string m_dataSource;

		// Token: 0x04000428 RID: 1064
		internal string m_userName;

		// Token: 0x04000429 RID: 1065
		internal bool m_bHasChanges;

		// Token: 0x0400042A RID: 1066
		internal bool m_bIsEnabled;

		// Token: 0x0400042B RID: 1067
		internal bool m_bQueryBasedNTFN;

		// Token: 0x0400042C RID: 1068
		internal ArrayList m_queryIDList;

		// Token: 0x0400042D RID: 1069
		internal ArrayList m_regList;

		// Token: 0x0400042E RID: 1070
		internal bool m_bIsNotifiedOnce;

		// Token: 0x0400042F RID: 1071
		internal bool m_bIsPersistent;

		// Token: 0x04000430 RID: 1072
		internal long m_timeout;

		// Token: 0x04000431 RID: 1073
		internal OracleRowidInfo m_OracleRowidInfo;
	}
}
