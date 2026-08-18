using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000093 RID: 147
	internal class OletxTransactionManager
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0003B8C4 File Offset: 0x0003ACC4
		internal static EventWaitHandle ShimWaitHandle
		{
			get
			{
				if (OletxTransactionManager.shimWaitHandle == null)
				{
					lock (OletxTransactionManager.ClassSyncObject)
					{
						if (OletxTransactionManager.shimWaitHandle == null)
						{
							OletxTransactionManager.shimWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
						}
					}
				}
				return OletxTransactionManager.shimWaitHandle;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0003B924 File Offset: 0x0003AD24
		internal static void ShimNotificationCallback(object state, bool timeout)
		{
			IntPtr zero = IntPtr.Zero;
			ShimNotificationType shimNotificationType = ShimNotificationType.None;
			bool singlePhase = false;
			bool flag = false;
			uint num = 0U;
			CoTaskMemHandle coTaskMemHandle = null;
			bool flag2 = false;
			bool flag3 = false;
			IDtcProxyShimFactory dtcProxyShimFactory = null;
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransactionManager.ShimNotificationCallback");
			}
			Thread.BeginCriticalRegion();
			try
			{
				do
				{
					dtcProxyShimFactory = OletxTransactionManager.proxyShimFactory;
					try
					{
						Thread.BeginThreadAffinity();
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
							dtcProxyShimFactory.GetNotification(out zero, out shimNotificationType, out singlePhase, out flag, out flag2, out num, out coTaskMemHandle);
						}
						finally
						{
							if (flag2)
							{
								if (HandleTable.FindHandle(zero) is OletxInternalResourceManager)
								{
									OletxTransactionManager.processingTmDown = true;
									Monitor.Enter(OletxTransactionManager.proxyShimFactory);
								}
								else
								{
									flag2 = false;
								}
								dtcProxyShimFactory.ReleaseNotificationLock();
							}
							Thread.EndThreadAffinity();
						}
						if (OletxTransactionManager.processingTmDown)
						{
							lock (OletxTransactionManager.proxyShimFactory)
							{
							}
						}
						if (shimNotificationType != ShimNotificationType.None)
						{
							object obj2 = HandleTable.FindHandle(zero);
							switch (shimNotificationType)
							{
							case ShimNotificationType.Phase0RequestNotify:
								try
								{
									OletxPhase0VolatileEnlistmentContainer oletxPhase0VolatileEnlistmentContainer = obj2 as OletxPhase0VolatileEnlistmentContainer;
									if (oletxPhase0VolatileEnlistmentContainer != null)
									{
										DiagnosticTrace.SetActivityId(oletxPhase0VolatileEnlistmentContainer.TransactionIdentifier);
										oletxPhase0VolatileEnlistmentContainer.Phase0Request(flag);
									}
									else
									{
										OletxEnlistment oletxEnlistment = obj2 as OletxEnlistment;
										if (oletxEnlistment != null)
										{
											DiagnosticTrace.SetActivityId(oletxEnlistment.TransactionIdentifier);
											oletxEnlistment.Phase0Request(flag);
										}
										else
										{
											Environment.FailFast(SR.GetString("InternalError"));
										}
									}
									goto IL_416;
								}
								finally
								{
									HandleTable.FreeHandle(zero);
								}
								break;
							case ShimNotificationType.VoteRequestNotify:
								break;
							case ShimNotificationType.PrepareRequestNotify:
								goto IL_2A7;
							case ShimNotificationType.CommitRequestNotify:
								goto IL_311;
							case ShimNotificationType.AbortRequestNotify:
								goto IL_34F;
							case ShimNotificationType.CommittedNotify:
								try
								{
									OutcomeEnlistment outcomeEnlistment = obj2 as OutcomeEnlistment;
									if (outcomeEnlistment != null)
									{
										DiagnosticTrace.SetActivityId(outcomeEnlistment.TransactionIdentifier);
										outcomeEnlistment.Committed();
									}
									else
									{
										OletxPhase1VolatileEnlistmentContainer oletxPhase1VolatileEnlistmentContainer = obj2 as OletxPhase1VolatileEnlistmentContainer;
										if (oletxPhase1VolatileEnlistmentContainer != null)
										{
											DiagnosticTrace.SetActivityId(oletxPhase1VolatileEnlistmentContainer.TransactionIdentifier);
											oletxPhase1VolatileEnlistmentContainer.Committed();
										}
										else
										{
											Environment.FailFast(SR.GetString("InternalError"));
										}
									}
									goto IL_416;
								}
								finally
								{
									HandleTable.FreeHandle(zero);
								}
								goto IL_1FA;
							case ShimNotificationType.AbortedNotify:
								goto IL_1FA;
							case ShimNotificationType.InDoubtNotify:
								goto IL_248;
							case ShimNotificationType.EnlistmentTmDownNotify:
								goto IL_38D;
							case ShimNotificationType.ResourceManagerTmDownNotify:
								goto IL_3C4;
							default:
								goto IL_407;
							}
							OletxPhase1VolatileEnlistmentContainer oletxPhase1VolatileEnlistmentContainer2 = obj2 as OletxPhase1VolatileEnlistmentContainer;
							if (oletxPhase1VolatileEnlistmentContainer2 != null)
							{
								DiagnosticTrace.SetActivityId(oletxPhase1VolatileEnlistmentContainer2.TransactionIdentifier);
								oletxPhase1VolatileEnlistmentContainer2.VoteRequest();
								goto IL_416;
							}
							Environment.FailFast(SR.GetString("InternalError"));
							goto IL_416;
							IL_1FA:
							try
							{
								OutcomeEnlistment outcomeEnlistment2 = obj2 as OutcomeEnlistment;
								if (outcomeEnlistment2 != null)
								{
									DiagnosticTrace.SetActivityId(outcomeEnlistment2.TransactionIdentifier);
									outcomeEnlistment2.Aborted();
								}
								else
								{
									OletxPhase1VolatileEnlistmentContainer oletxPhase1VolatileEnlistmentContainer3 = obj2 as OletxPhase1VolatileEnlistmentContainer;
									if (oletxPhase1VolatileEnlistmentContainer3 != null)
									{
										DiagnosticTrace.SetActivityId(oletxPhase1VolatileEnlistmentContainer3.TransactionIdentifier);
										oletxPhase1VolatileEnlistmentContainer3.Aborted();
									}
								}
								goto IL_416;
							}
							finally
							{
								HandleTable.FreeHandle(zero);
							}
							IL_248:
							try
							{
								OutcomeEnlistment outcomeEnlistment3 = obj2 as OutcomeEnlistment;
								if (outcomeEnlistment3 != null)
								{
									DiagnosticTrace.SetActivityId(outcomeEnlistment3.TransactionIdentifier);
									outcomeEnlistment3.InDoubt();
								}
								else
								{
									OletxPhase1VolatileEnlistmentContainer oletxPhase1VolatileEnlistmentContainer4 = obj2 as OletxPhase1VolatileEnlistmentContainer;
									if (oletxPhase1VolatileEnlistmentContainer4 != null)
									{
										DiagnosticTrace.SetActivityId(oletxPhase1VolatileEnlistmentContainer4.TransactionIdentifier);
										oletxPhase1VolatileEnlistmentContainer4.InDoubt();
									}
									else
									{
										Environment.FailFast(SR.GetString("InternalError"));
									}
								}
								goto IL_416;
							}
							finally
							{
								HandleTable.FreeHandle(zero);
							}
							IL_2A7:
							byte[] array = new byte[num];
							Marshal.Copy(coTaskMemHandle.DangerousGetHandle(), array, 0, Convert.ToInt32(num));
							bool flag4 = true;
							try
							{
								OletxEnlistment oletxEnlistment2 = obj2 as OletxEnlistment;
								if (oletxEnlistment2 != null)
								{
									DiagnosticTrace.SetActivityId(oletxEnlistment2.TransactionIdentifier);
									flag4 = oletxEnlistment2.PrepareRequest(singlePhase, array);
								}
								else
								{
									Environment.FailFast(SR.GetString("InternalError"));
								}
								goto IL_416;
							}
							finally
							{
								if (flag4)
								{
									HandleTable.FreeHandle(zero);
								}
							}
							IL_311:
							try
							{
								OletxEnlistment oletxEnlistment3 = obj2 as OletxEnlistment;
								if (oletxEnlistment3 != null)
								{
									DiagnosticTrace.SetActivityId(oletxEnlistment3.TransactionIdentifier);
									oletxEnlistment3.CommitRequest();
								}
								else
								{
									Environment.FailFast(SR.GetString("InternalError"));
								}
								goto IL_416;
							}
							finally
							{
								HandleTable.FreeHandle(zero);
							}
							IL_34F:
							try
							{
								OletxEnlistment oletxEnlistment4 = obj2 as OletxEnlistment;
								if (oletxEnlistment4 != null)
								{
									DiagnosticTrace.SetActivityId(oletxEnlistment4.TransactionIdentifier);
									oletxEnlistment4.AbortRequest();
								}
								else
								{
									Environment.FailFast(SR.GetString("InternalError"));
								}
								goto IL_416;
							}
							finally
							{
								HandleTable.FreeHandle(zero);
							}
							IL_38D:
							try
							{
								OletxEnlistment oletxEnlistment5 = obj2 as OletxEnlistment;
								if (oletxEnlistment5 != null)
								{
									DiagnosticTrace.SetActivityId(oletxEnlistment5.TransactionIdentifier);
									oletxEnlistment5.TMDown();
								}
								else
								{
									Environment.FailFast(SR.GetString("InternalError"));
								}
								goto IL_416;
							}
							finally
							{
								HandleTable.FreeHandle(zero);
							}
							IL_3C4:
							OletxResourceManager oletxResourceManager = obj2 as OletxResourceManager;
							try
							{
								if (oletxResourceManager != null)
								{
									oletxResourceManager.TMDown();
								}
								else
								{
									OletxInternalResourceManager oletxInternalResourceManager = obj2 as OletxInternalResourceManager;
									if (oletxInternalResourceManager != null)
									{
										oletxInternalResourceManager.TMDown();
									}
									else
									{
										Environment.FailFast(SR.GetString("InternalError"));
									}
								}
								goto IL_416;
							}
							finally
							{
								HandleTable.FreeHandle(zero);
							}
							IL_407:
							Environment.FailFast(SR.GetString("InternalError"));
						}
						IL_416:;
					}
					finally
					{
						if (coTaskMemHandle != null)
						{
							coTaskMemHandle.Close();
						}
						if (flag2)
						{
							flag2 = false;
							OletxTransactionManager.processingTmDown = false;
							Monitor.Exit(OletxTransactionManager.proxyShimFactory);
						}
					}
				}
				while (shimNotificationType != ShimNotificationType.None);
				flag3 = true;
			}
			finally
			{
				if (flag2)
				{
					flag2 = false;
					OletxTransactionManager.processingTmDown = false;
					Monitor.Exit(OletxTransactionManager.proxyShimFactory);
				}
				if (!flag3 && zero != IntPtr.Zero)
				{
					HandleTable.FreeHandle(zero);
				}
				Thread.EndCriticalRegion();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxTransactionManager.ShimNotificationCallback");
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0003BF04 File Offset: 0x0003B304
		internal OletxTransactionManager(string nodeName)
		{
			lock (OletxTransactionManager.ClassSyncObject)
			{
				if (OletxTransactionManager.proxyShimFactory == null)
				{
					int notificationFactory = NativeMethods.GetNotificationFactory(OletxTransactionManager.ShimWaitHandle.SafeWaitHandle, out OletxTransactionManager.proxyShimFactory);
					if (notificationFactory != 0)
					{
						throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("UnableToGetNotificationShimFactory"), null);
					}
					ThreadPool.UnsafeRegisterWaitForSingleObject(OletxTransactionManager.ShimWaitHandle, new WaitOrTimerCallback(OletxTransactionManager.ShimNotificationCallback), null, -1, false);
				}
			}
			this.dtcTransactionManagerLock = new ReaderWriterLock();
			this.nodeNameField = nodeName;
			if (this.nodeNameField != null && this.nodeNameField.Length == 0)
			{
				this.nodeNameField = null;
			}
			if (DiagnosticTrace.Verbose)
			{
				DistributedTransactionManagerCreatedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.GetType(), this.nodeNameField);
			}
			this.configuredTransactionOptions.IsolationLevel = (this.isolationLevelProperty = TransactionManager.DefaultIsolationLevel);
			this.configuredTransactionOptions.Timeout = (this.timeoutProperty = TransactionManager.DefaultTimeout);
			this.internalResourceManager = new OletxInternalResourceManager(this);
			this.dtcTransactionManagerLock.AcquireWriterLock(-1);
			try
			{
				this.dtcTransactionManager = new DtcTransactionManager(this.nodeNameField, this);
			}
			finally
			{
				this.dtcTransactionManagerLock.ReleaseWriterLock();
			}
			if (OletxTransactionManager.resourceManagerHashTable == null)
			{
				OletxTransactionManager.resourceManagerHashTable = new Hashtable(2);
				OletxTransactionManager.resourceManagerHashTableLock = new ReaderWriterLock();
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0003C0A4 File Offset: 0x0003B4A4
		internal OletxCommittableTransaction CreateTransaction(TransactionOptions properties)
		{
			OletxCommittableTransaction oletxCommittableTransaction = null;
			ITransactionShim transactionShim = null;
			Guid empty = Guid.Empty;
			OutcomeEnlistment outcomeEnlistment = null;
			DistributedTransactionPermission distributedTransactionPermission = new DistributedTransactionPermission(PermissionState.Unrestricted);
			distributedTransactionPermission.Demand();
			TransactionManager.ValidateIsolationLevel(properties.IsolationLevel);
			if (IsolationLevel.Unspecified == properties.IsolationLevel)
			{
				properties.IsolationLevel = this.configuredTransactionOptions.IsolationLevel;
			}
			properties.Timeout = TransactionManager.ValidateTimeout(properties.Timeout);
			this.dtcTransactionManagerLock.AcquireReaderLock(-1);
			try
			{
				OletxTransactionIsolationLevel oletxTransactionIsolationLevel = OletxTransactionManager.ConvertIsolationLevel(properties.IsolationLevel);
				uint timeout = DtcTransactionManager.AdjustTimeout(properties.Timeout);
				outcomeEnlistment = new OutcomeEnlistment();
				IntPtr intPtr = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					intPtr = HandleTable.AllocHandle(outcomeEnlistment);
					this.dtcTransactionManager.ProxyShimFactory.BeginTransaction(timeout, oletxTransactionIsolationLevel, intPtr, out empty, out transactionShim);
				}
				catch (COMException comException)
				{
					OletxTransactionManager.ProxyException(comException);
					throw;
				}
				finally
				{
					if (transactionShim == null && intPtr != IntPtr.Zero)
					{
						HandleTable.FreeHandle(intPtr);
					}
				}
				RealOletxTransaction realOletxTransaction = new RealOletxTransaction(this, transactionShim, outcomeEnlistment, empty, oletxTransactionIsolationLevel, true);
				oletxCommittableTransaction = new OletxCommittableTransaction(realOletxTransaction);
				if (DiagnosticTrace.Information)
				{
					TransactionCreatedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), oletxCommittableTransaction.TransactionTraceId);
				}
			}
			finally
			{
				this.dtcTransactionManagerLock.ReleaseReaderLock();
			}
			return oletxCommittableTransaction;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0003C224 File Offset: 0x0003B624
		internal OletxEnlistment ReenlistTransaction(Guid resourceManagerIdentifier, byte[] recoveryInformation, IEnlistmentNotificationInternal enlistmentNotification)
		{
			if (recoveryInformation == null)
			{
				throw new ArgumentNullException("recoveryInformation");
			}
			if (enlistmentNotification == null)
			{
				throw new ArgumentNullException("enlistmentNotification");
			}
			OletxResourceManager oletxResourceManager = this.RegisterResourceManager(resourceManagerIdentifier);
			if (oletxResourceManager == null)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "resourceManagerIdentifier");
			}
			if (oletxResourceManager.RecoveryCompleteCalledByApplication)
			{
				throw new InvalidOperationException(SR.GetString("ReenlistAfterRecoveryComplete"));
			}
			return oletxResourceManager.Reenlist(recoveryInformation.Length, recoveryInformation, enlistmentNotification);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0003C294 File Offset: 0x0003B694
		internal void ResourceManagerRecoveryComplete(Guid resourceManagerIdentifier)
		{
			OletxResourceManager oletxResourceManager = this.RegisterResourceManager(resourceManagerIdentifier);
			if (oletxResourceManager.RecoveryCompleteCalledByApplication)
			{
				throw new InvalidOperationException(SR.GetString("DuplicateRecoveryComplete"));
			}
			oletxResourceManager.RecoveryComplete();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0003C2D4 File Offset: 0x0003B6D4
		internal OletxResourceManager RegisterResourceManager(Guid resourceManagerIdentifier)
		{
			OletxResourceManager oletxResourceManager = null;
			OletxTransactionManager.resourceManagerHashTableLock.AcquireWriterLock(-1);
			try
			{
				oletxResourceManager = (OletxTransactionManager.resourceManagerHashTable[resourceManagerIdentifier] as OletxResourceManager);
				if (oletxResourceManager != null)
				{
					return oletxResourceManager;
				}
				oletxResourceManager = new OletxResourceManager(this, resourceManagerIdentifier);
				OletxTransactionManager.resourceManagerHashTable.Add(resourceManagerIdentifier, oletxResourceManager);
			}
			finally
			{
				OletxTransactionManager.resourceManagerHashTableLock.ReleaseWriterLock();
			}
			return oletxResourceManager;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0003C354 File Offset: 0x0003B754
		internal string CreationNodeName
		{
			get
			{
				return this.nodeNameField;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0003C374 File Offset: 0x0003B774
		internal OletxResourceManager FindOrRegisterResourceManager(Guid resourceManagerIdentifier)
		{
			if (resourceManagerIdentifier == Guid.Empty)
			{
				throw new ArgumentException(SR.GetString("BadResourceManagerId"), "resourceManagerIdentifier");
			}
			OletxResourceManager oletxResourceManager = null;
			OletxTransactionManager.resourceManagerHashTableLock.AcquireReaderLock(-1);
			try
			{
				oletxResourceManager = (OletxTransactionManager.resourceManagerHashTable[resourceManagerIdentifier] as OletxResourceManager);
			}
			finally
			{
				OletxTransactionManager.resourceManagerHashTableLock.ReleaseReaderLock();
			}
			if (oletxResourceManager == null)
			{
				return this.RegisterResourceManager(resourceManagerIdentifier);
			}
			return oletxResourceManager;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0003C404 File Offset: 0x0003B804
		internal DtcTransactionManager DtcTransactionManager
		{
			get
			{
				if (!this.dtcTransactionManagerLock.IsReaderLockHeld && !this.dtcTransactionManagerLock.IsWriterLockHeld)
				{
					throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("InternalError"), null);
				}
				if (this.dtcTransactionManager == null)
				{
					throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("DtcTransactionManagerUnavailable"), null);
				}
				return this.dtcTransactionManager;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0003C474 File Offset: 0x0003B874
		internal string NodeName
		{
			get
			{
				return this.nodeNameField;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0003C494 File Offset: 0x0003B894
		internal static void ProxyException(COMException comException)
		{
			if (NativeMethods.XACT_E_CONNECTION_DOWN == comException.ErrorCode || NativeMethods.XACT_E_TMNOTAVAILABLE == comException.ErrorCode)
			{
				throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TransactionManagerCommunicationException"), comException);
			}
			if (NativeMethods.XACT_E_NETWORK_TX_DISABLED == comException.ErrorCode)
			{
				throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("NetworkTransactionsDisabled"), comException);
			}
			if (NativeMethods.XACT_E_FIRST > comException.ErrorCode || NativeMethods.XACT_E_LAST < comException.ErrorCode)
			{
				return;
			}
			if (NativeMethods.XACT_E_NOTRANSACTION == comException.ErrorCode)
			{
				throw TransactionException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TransactionAlreadyOver"), comException);
			}
			throw TransactionException.Create(SR.GetString("TraceSourceOletx"), comException.Message, comException);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0003C564 File Offset: 0x0003B964
		internal void ReinitializeProxy()
		{
			this.dtcTransactionManagerLock.AcquireWriterLock(-1);
			try
			{
				if (this.dtcTransactionManager != null)
				{
					this.dtcTransactionManager.ReleaseProxy();
				}
			}
			finally
			{
				this.dtcTransactionManagerLock.ReleaseWriterLock();
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0003C5C4 File Offset: 0x0003B9C4
		internal static OletxTransactionIsolationLevel ConvertIsolationLevel(IsolationLevel isolationLevel)
		{
			switch (isolationLevel)
			{
			case IsolationLevel.Serializable:
				return OletxTransactionIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE;
			case IsolationLevel.RepeatableRead:
				return OletxTransactionIsolationLevel.ISOLATIONLEVEL_REPEATABLEREAD;
			case IsolationLevel.ReadCommitted:
				return OletxTransactionIsolationLevel.ISOLATIONLEVEL_CURSORSTABILITY;
			case IsolationLevel.ReadUncommitted:
				return OletxTransactionIsolationLevel.ISOLATIONLEVEL_READUNCOMMITTED;
			case IsolationLevel.Chaos:
				return OletxTransactionIsolationLevel.ISOLATIONLEVEL_CHAOS;
			case IsolationLevel.Unspecified:
				return OletxTransactionIsolationLevel.ISOLATIONLEVEL_UNSPECIFIED;
			}
			return OletxTransactionIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0003C634 File Offset: 0x0003BA34
		internal static IsolationLevel ConvertIsolationLevelFromProxyValue(OletxTransactionIsolationLevel proxyIsolationLevel)
		{
			if (proxyIsolationLevel <= OletxTransactionIsolationLevel.ISOLATIONLEVEL_READUNCOMMITTED)
			{
				if (proxyIsolationLevel == OletxTransactionIsolationLevel.ISOLATIONLEVEL_UNSPECIFIED)
				{
					return IsolationLevel.Unspecified;
				}
				if (proxyIsolationLevel == OletxTransactionIsolationLevel.ISOLATIONLEVEL_CHAOS)
				{
					return IsolationLevel.Chaos;
				}
				if (proxyIsolationLevel == OletxTransactionIsolationLevel.ISOLATIONLEVEL_READUNCOMMITTED)
				{
					return IsolationLevel.ReadUncommitted;
				}
			}
			else
			{
				if (proxyIsolationLevel == OletxTransactionIsolationLevel.ISOLATIONLEVEL_CURSORSTABILITY)
				{
					return IsolationLevel.ReadCommitted;
				}
				if (proxyIsolationLevel == OletxTransactionIsolationLevel.ISOLATIONLEVEL_REPEATABLEREAD)
				{
					return IsolationLevel.RepeatableRead;
				}
				if (proxyIsolationLevel == OletxTransactionIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE)
				{
					return IsolationLevel.Serializable;
				}
			}
			return IsolationLevel.Serializable;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0003C694 File Offset: 0x0003BA94
		internal static object ClassSyncObject
		{
			get
			{
				if (OletxTransactionManager.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref OletxTransactionManager.classSyncObject, value, null);
				}
				return OletxTransactionManager.classSyncObject;
			}
		}

		// Token: 0x0400021B RID: 539
		private IsolationLevel isolationLevelProperty;

		// Token: 0x0400021C RID: 540
		private TimeSpan timeoutProperty;

		// Token: 0x0400021D RID: 541
		private TransactionOptions configuredTransactionOptions = default(TransactionOptions);

		// Token: 0x0400021E RID: 542
		private static object classSyncObject;

		// Token: 0x0400021F RID: 543
		internal static Hashtable resourceManagerHashTable;

		// Token: 0x04000220 RID: 544
		internal static ReaderWriterLock resourceManagerHashTableLock;

		// Token: 0x04000221 RID: 545
		internal static volatile bool processingTmDown;

		// Token: 0x04000222 RID: 546
		internal ReaderWriterLock dtcTransactionManagerLock;

		// Token: 0x04000223 RID: 547
		private DtcTransactionManager dtcTransactionManager;

		// Token: 0x04000224 RID: 548
		internal OletxInternalResourceManager internalResourceManager;

		// Token: 0x04000225 RID: 549
		internal static IDtcProxyShimFactory proxyShimFactory;

		// Token: 0x04000226 RID: 550
		internal static EventWaitHandle shimWaitHandle;

		// Token: 0x04000227 RID: 551
		private string nodeNameField;
	}
}
