using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000090 RID: 144
	internal sealed class OletxResourceManager
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00039974 File Offset: 0x00038D74
		internal OletxResourceManager(OletxTransactionManager transactionManager, Guid resourceManagerIdentifier)
		{
			this.resourceManagerShim = null;
			this.oletxTransactionManager = transactionManager;
			this.resourceManagerIdentifier = resourceManagerIdentifier;
			this.enlistmentHashtable = new Hashtable();
			this.reenlistList = new ArrayList();
			this.reenlistPendingList = new ArrayList();
			this.reenlistThreadTimer = null;
			this.reenlistThread = null;
			this.recoveryCompleteCalledByApplication = false;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000399D4 File Offset: 0x00038DD4
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00039BA4 File Offset: 0x00038FA4
		internal IResourceManagerShim ResourceManagerShim
		{
			get
			{
				IResourceManagerShim resourceManagerShim = null;
				if (this.resourceManagerShim == null)
				{
					lock (this)
					{
						if (this.resourceManagerShim == null)
						{
							this.oletxTransactionManager.dtcTransactionManagerLock.AcquireReaderLock(-1);
							try
							{
								Guid guid = this.resourceManagerIdentifier;
								IntPtr intPtr = IntPtr.Zero;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									intPtr = HandleTable.AllocHandle(this);
									this.oletxTransactionManager.DtcTransactionManager.ProxyShimFactory.CreateResourceManager(guid, intPtr, out resourceManagerShim);
								}
								finally
								{
									if (resourceManagerShim == null && intPtr != IntPtr.Zero)
									{
										HandleTable.FreeHandle(intPtr);
									}
								}
							}
							catch (COMException ex)
							{
								if (NativeMethods.XACT_E_CONNECTION_DOWN != ex.ErrorCode && NativeMethods.XACT_E_TMNOTAVAILABLE != ex.ErrorCode)
								{
									throw;
								}
								resourceManagerShim = null;
								if (DiagnosticTrace.Verbose)
								{
									ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex);
								}
							}
							catch (TransactionException ex2)
							{
								COMException ex3 = ex2.InnerException as COMException;
								if (ex3 == null)
								{
									throw;
								}
								if (NativeMethods.XACT_E_CONNECTION_DOWN != ex3.ErrorCode && NativeMethods.XACT_E_TMNOTAVAILABLE != ex3.ErrorCode)
								{
									throw;
								}
								resourceManagerShim = null;
								if (DiagnosticTrace.Verbose)
								{
									ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex2);
								}
							}
							finally
							{
								this.oletxTransactionManager.dtcTransactionManagerLock.ReleaseReaderLock();
							}
							Thread.MemoryBarrier();
							this.resourceManagerShim = resourceManagerShim;
						}
					}
				}
				return this.resourceManagerShim;
			}
			set
			{
				this.resourceManagerShim = value;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00039BC4 File Offset: 0x00038FC4
		internal bool CallProxyReenlistComplete()
		{
			bool result = false;
			if (this.RecoveryCompleteCalledByApplication)
			{
				try
				{
					try
					{
						IResourceManagerShim resourceManagerShim = this.ResourceManagerShim;
						if (resourceManagerShim != null)
						{
							resourceManagerShim.ReenlistComplete();
							result = true;
						}
					}
					catch (COMException ex)
					{
						if (NativeMethods.XACT_E_CONNECTION_DOWN == ex.ErrorCode || NativeMethods.XACT_E_TMNOTAVAILABLE == ex.ErrorCode)
						{
							result = false;
							if (DiagnosticTrace.Verbose)
							{
								ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex);
							}
						}
						else
						{
							if (NativeMethods.XACT_E_RECOVERYALREADYDONE != ex.ErrorCode)
							{
								OletxTransactionManager.ProxyException(ex);
								throw;
							}
							result = true;
						}
					}
					return result;
				}
				finally
				{
				}
			}
			result = true;
			return result;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00039C84 File Offset: 0x00039084
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00039CA4 File Offset: 0x000390A4
		internal bool RecoveryCompleteCalledByApplication
		{
			get
			{
				return this.recoveryCompleteCalledByApplication;
			}
			set
			{
				this.recoveryCompleteCalledByApplication = value;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00039CC4 File Offset: 0x000390C4
		internal void TMDownFromInternalRM(OletxTransactionManager oletxTM)
		{
			Hashtable hashtable = null;
			this.ResourceManagerShim = null;
			lock (this.enlistmentHashtable.SyncRoot)
			{
				hashtable = (Hashtable)this.enlistmentHashtable.Clone();
			}
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				OletxEnlistment oletxEnlistment = enumerator.Value as OletxEnlistment;
				if (oletxEnlistment != null)
				{
					oletxEnlistment.TMDownFromInternalRM(oletxTM);
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00039D54 File Offset: 0x00039154
		public void TMDown()
		{
			this.StartReenlistThread();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00039D74 File Offset: 0x00039174
		internal OletxEnlistment EnlistDurable(OletxTransaction oletxTransaction, bool canDoSinglePhase, IEnlistmentNotificationInternal enlistmentNotification, EnlistmentOptions enlistmentOptions)
		{
			IResourceManagerShim resourceManagerShim = null;
			IEnlistmentShim enlistmentShim = null;
			IPhase0EnlistmentShim phase0EnlistmentShim = null;
			Guid empty = Guid.Empty;
			IntPtr intPtr = IntPtr.Zero;
			bool flag = false;
			bool flag2 = false;
			OletxEnlistment oletxEnlistment = new OletxEnlistment(canDoSinglePhase, enlistmentNotification, oletxTransaction.RealTransaction.TxGuid, enlistmentOptions, this, oletxTransaction);
			bool flag3 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if ((enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						oletxTransaction.RealTransaction.IncrementUndecidedEnlistments();
						flag2 = true;
					}
				}
				lock (oletxEnlistment)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						resourceManagerShim = this.ResourceManagerShim;
						if (resourceManagerShim == null)
						{
							throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), null);
						}
						if ((enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None)
						{
							intPtr = HandleTable.AllocHandle(oletxEnlistment);
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
							}
							finally
							{
								oletxTransaction.RealTransaction.TransactionShim.Phase0Enlist(intPtr, out phase0EnlistmentShim);
								flag = true;
							}
							oletxEnlistment.Phase0EnlistmentShim = phase0EnlistmentShim;
						}
						oletxEnlistment.phase1Handle = HandleTable.AllocHandle(oletxEnlistment);
						resourceManagerShim.Enlist(oletxTransaction.RealTransaction.TransactionShim, oletxEnlistment.phase1Handle, out enlistmentShim);
						oletxEnlistment.EnlistmentShim = enlistmentShim;
					}
					catch (COMException ex)
					{
						if (NativeMethods.XACT_E_TOOMANY_ENLISTMENTS == ex.ErrorCode)
						{
							throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("OletxTooManyEnlistments"), ex);
						}
						OletxTransactionManager.ProxyException(ex);
						throw;
					}
					finally
					{
						if (oletxEnlistment.EnlistmentShim == null)
						{
							if (intPtr != IntPtr.Zero && !flag)
							{
								HandleTable.FreeHandle(intPtr);
							}
							if (oletxEnlistment.phase1Handle != IntPtr.Zero)
							{
								HandleTable.FreeHandle(oletxEnlistment.phase1Handle);
							}
						}
					}
				}
				flag3 = true;
			}
			finally
			{
				if (!flag3 && (enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None && flag2)
				{
					oletxTransaction.RealTransaction.DecrementUndecidedEnlistments();
				}
			}
			return oletxEnlistment;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00039F94 File Offset: 0x00039394
		internal OletxEnlistment Reenlist(int prepareInfoLength, byte[] prepareInfo, IEnlistmentNotificationInternal enlistmentNotification)
		{
			OletxTransactionOutcome oletxTransactionOutcome = OletxTransactionOutcome.NotKnownYet;
			OletxTransactionStatus xactStatus = OletxTransactionStatus.OLETX_TRANSACTION_STATUS_NONE;
			MemoryStream serializationStream = new MemoryStream(prepareInfo);
			IFormatter formatter = new BinaryFormatter();
			OletxRecoveryInformation oletxRecoveryInformation;
			try
			{
				oletxRecoveryInformation = (formatter.Deserialize(serializationStream) as OletxRecoveryInformation);
			}
			catch (SerializationException innerException)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "prepareInfo", innerException);
			}
			if (oletxRecoveryInformation == null)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "prepareInfo");
			}
			byte[] array = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				array[i] = oletxRecoveryInformation.proxyRecoveryInformation[i + 16];
			}
			Guid a = new Guid(array);
			if (a != this.resourceManagerIdentifier)
			{
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("ResourceManagerIdDoesNotMatchRecoveryInformation"), null);
			}
			try
			{
				IResourceManagerShim resourceManagerShim = this.ResourceManagerShim;
				if (resourceManagerShim == null)
				{
					throw new COMException(SR.GetString("DtcTransactionManagerUnavailable"), NativeMethods.XACT_E_CONNECTION_DOWN);
				}
				resourceManagerShim.Reenlist(Convert.ToUInt32(oletxRecoveryInformation.proxyRecoveryInformation.Length, CultureInfo.InvariantCulture), oletxRecoveryInformation.proxyRecoveryInformation, out oletxTransactionOutcome);
				if (OletxTransactionOutcome.Committed == oletxTransactionOutcome)
				{
					xactStatus = OletxTransactionStatus.OLETX_TRANSACTION_STATUS_COMMITTED;
				}
				else if (OletxTransactionOutcome.Aborted == oletxTransactionOutcome)
				{
					xactStatus = OletxTransactionStatus.OLETX_TRANSACTION_STATUS_ABORTED;
				}
				else
				{
					xactStatus = OletxTransactionStatus.OLETX_TRANSACTION_STATUS_PREPARED;
					this.StartReenlistThread();
				}
			}
			catch (COMException ex)
			{
				if (NativeMethods.XACT_E_CONNECTION_DOWN != ex.ErrorCode)
				{
					throw;
				}
				xactStatus = OletxTransactionStatus.OLETX_TRANSACTION_STATUS_PREPARED;
				this.ResourceManagerShim = null;
				this.StartReenlistThread();
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex);
				}
			}
			finally
			{
			}
			return new OletxEnlistment(enlistmentNotification, xactStatus, oletxRecoveryInformation.proxyRecoveryInformation, this);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0003A164 File Offset: 0x00039564
		internal void RecoveryComplete()
		{
			Timer timer = null;
			this.RecoveryCompleteCalledByApplication = true;
			try
			{
				lock (this.reenlistList)
				{
					lock (this)
					{
						if (this.reenlistList.Count == 0 && this.reenlistPendingList.Count == 0)
						{
							if (this.reenlistThreadTimer != null)
							{
								timer = this.reenlistThreadTimer;
								this.reenlistThreadTimer = null;
							}
							if (!this.CallProxyReenlistComplete())
							{
								this.StartReenlistThread();
							}
						}
						else
						{
							this.StartReenlistThread();
						}
					}
				}
			}
			finally
			{
				if (timer != null)
				{
					timer.Dispose();
				}
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0003A244 File Offset: 0x00039644
		internal void StartReenlistThread()
		{
			lock (this)
			{
				if (this.reenlistThreadTimer == null && this.reenlistThread == null)
				{
					this.reenlistThreadTimer = new Timer(new TimerCallback(this.ReenlistThread), this, 10, -1);
				}
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0003A2B4 File Offset: 0x000396B4
		internal void RemoveFromReenlistPending(OletxEnlistment enlistment)
		{
			lock (this.reenlistList)
			{
				this.reenlistPendingList.Remove(enlistment);
				lock (this)
				{
					if (this.reenlistThreadTimer != null && this.reenlistList.Count == 0 && this.reenlistPendingList.Count == 0 && !this.reenlistThreadTimer.Change(0, -1))
					{
						throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("UnexpectedTimerFailure"), null);
					}
				}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0003A374 File Offset: 0x00039774
		internal void ReenlistThread(object state)
		{
			int num = 0;
			bool flag = false;
			OletxEnlistment oletxEnlistment = null;
			IResourceManagerShim resourceManagerShim = null;
			Timer timer = null;
			bool flag2 = false;
			OletxResourceManager oletxResourceManager = (OletxResourceManager)state;
			try
			{
				if (DiagnosticTrace.Information)
				{
					MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxResourceManager.ReenlistThread");
				}
				lock (oletxResourceManager)
				{
					resourceManagerShim = oletxResourceManager.ResourceManagerShim;
					timer = oletxResourceManager.reenlistThreadTimer;
					oletxResourceManager.reenlistThreadTimer = null;
					oletxResourceManager.reenlistThread = Thread.CurrentThread;
				}
				if (resourceManagerShim != null)
				{
					lock (oletxResourceManager.reenlistList)
					{
						num = oletxResourceManager.reenlistList.Count;
					}
					flag = false;
					while (!flag && num > 0 && resourceManagerShim != null)
					{
						lock (oletxResourceManager.reenlistList)
						{
							oletxEnlistment = null;
							num--;
							if (oletxResourceManager.reenlistList.Count == 0)
							{
								flag = true;
							}
							else
							{
								oletxEnlistment = (oletxResourceManager.reenlistList[0] as OletxEnlistment);
								if (oletxEnlistment == null)
								{
									if (DiagnosticTrace.Critical)
									{
										InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
									}
									throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("InternalError"), null);
								}
								oletxResourceManager.reenlistList.RemoveAt(0);
								object obj4 = oletxEnlistment;
								lock (obj4)
								{
									if (OletxEnlistment.OletxEnlistmentState.Done == oletxEnlistment.State)
									{
										oletxEnlistment = null;
									}
									else if (OletxEnlistment.OletxEnlistmentState.Prepared != oletxEnlistment.State)
									{
										oletxResourceManager.reenlistList.Add(oletxEnlistment);
										oletxEnlistment = null;
									}
								}
							}
						}
						if (oletxEnlistment != null)
						{
							OletxTransactionOutcome oletxTransactionOutcome = OletxTransactionOutcome.NotKnownYet;
							try
							{
								if (oletxEnlistment.ProxyPrepareInfoByteArray == null)
								{
									if (DiagnosticTrace.Critical)
									{
										InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
									}
									throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("InternalError"), null);
								}
								resourceManagerShim.Reenlist((uint)oletxEnlistment.ProxyPrepareInfoByteArray.Length, oletxEnlistment.ProxyPrepareInfoByteArray, out oletxTransactionOutcome);
								if (oletxTransactionOutcome == OletxTransactionOutcome.NotKnownYet)
								{
									object obj6 = oletxEnlistment;
									lock (obj6)
									{
										if (OletxEnlistment.OletxEnlistmentState.Done == oletxEnlistment.State)
										{
											oletxEnlistment = null;
										}
										else
										{
											lock (oletxResourceManager.reenlistList)
											{
												oletxResourceManager.reenlistList.Add(oletxEnlistment);
												oletxEnlistment = null;
											}
										}
									}
								}
							}
							catch (COMException ex)
							{
								if (NativeMethods.XACT_E_CONNECTION_DOWN != ex.ErrorCode)
								{
									throw;
								}
								if (DiagnosticTrace.Verbose)
								{
									ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), ex);
								}
								if (NativeMethods.XACT_E_CONNECTION_DOWN == ex.ErrorCode)
								{
									oletxResourceManager.ResourceManagerShim = null;
									resourceManagerShim = oletxResourceManager.ResourceManagerShim;
								}
							}
							if (oletxEnlistment != null)
							{
								object obj9 = oletxEnlistment;
								lock (obj9)
								{
									if (OletxEnlistment.OletxEnlistmentState.Done == oletxEnlistment.State)
									{
										oletxEnlistment = null;
									}
									else
									{
										lock (oletxResourceManager.reenlistList)
										{
											oletxResourceManager.reenlistPendingList.Add(oletxEnlistment);
										}
										if (OletxTransactionOutcome.Committed == oletxTransactionOutcome)
										{
											oletxEnlistment.State = OletxEnlistment.OletxEnlistmentState.Committing;
											if (DiagnosticTrace.Verbose)
											{
												EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceOletx"), oletxEnlistment.EnlistmentTraceId, NotificationCall.Commit);
											}
											oletxEnlistment.EnlistmentNotification.Commit(oletxEnlistment);
										}
										else
										{
											if (OletxTransactionOutcome.Aborted != oletxTransactionOutcome)
											{
												if (DiagnosticTrace.Critical)
												{
													InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
												}
												throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("InternalError"), null);
											}
											oletxEnlistment.State = OletxEnlistment.OletxEnlistmentState.Aborting;
											if (DiagnosticTrace.Verbose)
											{
												EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceOletx"), oletxEnlistment.EnlistmentTraceId, NotificationCall.Rollback);
											}
											oletxEnlistment.EnlistmentNotification.Rollback(oletxEnlistment);
										}
									}
								}
							}
						}
					}
				}
				resourceManagerShim = null;
				lock (oletxResourceManager.reenlistList)
				{
					lock (oletxResourceManager)
					{
						num = oletxResourceManager.reenlistList.Count;
						if (0 >= num && 0 >= oletxResourceManager.reenlistPendingList.Count)
						{
							bool flag3 = oletxResourceManager.CallProxyReenlistComplete();
							if (flag3)
							{
								flag2 = true;
							}
							else
							{
								oletxResourceManager.reenlistThreadTimer = timer;
								if (!timer.Change(10000, -1))
								{
									throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("UnexpectedTimerFailure"), null);
								}
							}
						}
						else
						{
							oletxResourceManager.reenlistThreadTimer = timer;
							if (!timer.Change(10000, -1))
							{
								throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceLtm"), SR.GetString("UnexpectedTimerFailure"), null);
							}
						}
						oletxResourceManager.reenlistThread = null;
					}
					if (DiagnosticTrace.Information)
					{
						MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxResourceManager.ReenlistThread");
					}
				}
			}
			finally
			{
				resourceManagerShim = null;
				if (flag2 && timer != null)
				{
					timer.Dispose();
				}
			}
		}

		// Token: 0x040001F9 RID: 505
		internal Guid resourceManagerIdentifier;

		// Token: 0x040001FA RID: 506
		internal IResourceManagerShim resourceManagerShim;

		// Token: 0x040001FB RID: 507
		internal Hashtable enlistmentHashtable;

		// Token: 0x040001FC RID: 508
		internal static Hashtable volatileEnlistmentHashtable = new Hashtable();

		// Token: 0x040001FD RID: 509
		internal OletxTransactionManager oletxTransactionManager;

		// Token: 0x040001FE RID: 510
		internal ArrayList reenlistList;

		// Token: 0x040001FF RID: 511
		internal ArrayList reenlistPendingList;

		// Token: 0x04000200 RID: 512
		internal Timer reenlistThreadTimer;

		// Token: 0x04000201 RID: 513
		internal Thread reenlistThread;

		// Token: 0x04000202 RID: 514
		private bool recoveryCompleteCalledByApplication;
	}
}
