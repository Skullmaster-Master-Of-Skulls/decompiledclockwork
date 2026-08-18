using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000115 RID: 277
	internal abstract class DTCPSPEManager : PSPEManager
	{
		// Token: 0x06000C03 RID: 3075 RVA: 0x00086670 File Offset: 0x00084870
		static DTCPSPEManager()
		{
			try
			{
				string assemblyString = string.Format("Oracle.ManagedDataAccessDTC, Version={0}, Culture=neutral, PublicKeyToken=89b483f429c47342", ConfigBaseClass.m_assemblyVersion);
				Assembly assembly = Assembly.Load(assemblyString);
				Type type = assembly.GetType("OracleInternal.MTS.CPP.MDtcTxCreator");
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				DTCPSPEManager.s_dtcTxCreator = constructor.Invoke(null);
				DTCPSPEManager.CreateDTCTxWrapper = type.GetMethod("CreateDTCTxWrapper");
				Type type2 = assembly.GetType("OracleInternal.MTS.CPP.MDtcTxWrapper");
				DTCPSPEManager.Commit = type2.GetMethod("Commit");
				DTCPSPEManager.Abort = type2.GetMethod("Abort");
				DTCPSPEManager.Dispose = type2.GetMethod("Dispose");
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
				throw;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00086734 File Offset: 0x00084934
		internal DTCPSPEManager(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch) : base(connImpl, txn, txnRM, txnBranch)
		{
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00086744 File Offset: 0x00084944
		internal override byte[] InternalPromote(out Guid txnGuid)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = 0;
			txnGuid = Guid.Empty;
			byte[] result;
			try
			{
				object[] array = new object[]
				{
					num,
					intPtr,
					intPtr2
				};
				this.m_dtcTxWrapper = DTCPSPEManager.CreateDTCTxWrapper.Invoke(DTCPSPEManager.s_dtcTxCreator, array);
				num = (int)array[0];
				intPtr = (IntPtr)array[1];
				intPtr2 = (IntPtr)array[2];
				byte[] array2 = new byte[num];
				byte[] array3 = new byte[16];
				Marshal.Copy(intPtr, array2, 0, num);
				Marshal.Copy(intPtr2, array3, 0, 16);
				txnGuid = new Guid(array3);
				result = array2;
			}
			finally
			{
				try
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
				catch
				{
				}
				try
				{
					Marshal.FreeCoTaskMem(intPtr2);
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00086848 File Offset: 0x00084A48
		internal override bool InternalCommit()
		{
			bool result;
			try
			{
				if (this.m_mtsTxnRM.m_enlistedState == EnlistedState.Local)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"Invoking MTSTxnRM.EnlistToSysTransaction() MTSTxnRM : ",
								this.m_mtsTxnRM.m_RMGuid,
								" RM EnlistedState = ",
								this.m_mtsTxnRM.m_enlistedState
							})
						});
					}
					this.m_mtsTxnRM.EnlistToSysTransaction();
				}
				int num = (int)DTCPSPEManager.Commit.Invoke(this.m_dtcTxWrapper, null);
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Concat(new object[]
						{
							"PSPEManager.SinglePhaseCommit(): DTC Transaction committed HRESULT = ",
							num,
							" Local TxnID = ",
							this.m_sysTxn.TransactionInformation.LocalIdentifier,
							"\tTxnID = ",
							this.m_sysTxn.TransactionInformation.DistributedIdentifier
						})
					});
				}
				result = (num == 0);
			}
			finally
			{
				try
				{
					DTCPSPEManager.Dispose.Invoke(this.m_dtcTxWrapper, null);
				}
				catch
				{
				}
				this.m_dtcTxWrapper = null;
			}
			return result;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000869C4 File Offset: 0x00084BC4
		internal override bool InternalRollback()
		{
			bool result;
			try
			{
				if (this.m_mtsTxnRM.m_enlistedState == EnlistedState.Local)
				{
					try
					{
						this.m_mtsTxnRM.doAbort();
					}
					catch (Exception ex)
					{
						try
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
						}
						catch
						{
						}
						try
						{
							DTCPSPEManager.Abort.Invoke(this.m_dtcTxWrapper, null);
						}
						catch
						{
						}
						throw;
					}
					finally
					{
						try
						{
							base.ReleaseRM(this.m_connStr);
						}
						catch
						{
						}
					}
				}
				int num = (int)DTCPSPEManager.Abort.Invoke(this.m_dtcTxWrapper, null);
				result = (num == 0);
			}
			finally
			{
				try
				{
					DTCPSPEManager.Dispose.Invoke(this.m_dtcTxWrapper, null);
				}
				catch
				{
				}
				this.m_dtcTxWrapper = null;
			}
			return result;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00086AC8 File Offset: 0x00084CC8
		internal override void InternalHandlePromoteError()
		{
		}

		// Token: 0x04000D2C RID: 3372
		internal const int s_txnGuidSize = 16;

		// Token: 0x04000D2D RID: 3373
		internal static object s_dtcTxCreator = null;

		// Token: 0x04000D2E RID: 3374
		private object m_dtcTxWrapper;

		// Token: 0x04000D2F RID: 3375
		private static MethodInfo CreateDTCTxWrapper;

		// Token: 0x04000D30 RID: 3376
		private static MethodInfo Commit;

		// Token: 0x04000D31 RID: 3377
		private static MethodInfo Abort;

		// Token: 0x04000D32 RID: 3378
		private static MethodInfo Dispose;
	}
}
