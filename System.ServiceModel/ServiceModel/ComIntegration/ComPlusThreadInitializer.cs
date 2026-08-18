using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Transactions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000214 RID: 532
	internal class ComPlusThreadInitializer : ICallContextInitializer
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x0003A268 File Offset: 0x00038468
		public ComPlusThreadInitializer(ContractDescription contract, DispatchOperation operation, ServiceInfo info)
		{
			this.info = info;
			this.iid = contract.ContractType.GUID;
			if (info.CheckRoles)
			{
				string[] serviceRoleMembers = null;
				string[] contractRoleMembers = null;
				string[] array = null;
				serviceRoleMembers = info.ComponentRoleMembers;
				foreach (ContractInfo contractInfo in this.info.Contracts)
				{
					if (contractInfo.IID == this.iid)
					{
						contractRoleMembers = contractInfo.InterfaceRoleMembers;
						foreach (OperationInfo operationInfo in contractInfo.Operations)
						{
							if (operationInfo.Name == operation.Name)
							{
								array = operationInfo.MethodRoleMembers;
								break;
							}
						}
						if (array == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ComOperationNotFound", new object[]
							{
								contract.Name,
								operation.Name
							})));
						}
						break;
					}
				}
				this.comAuth = new ComPlusAuthorization(serviceRoleMembers, contractRoleMembers, array);
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
		{
			ComPlusServerSecurity comPlusServerSecurity = null;
			WindowsImpersonationContext windowsImpersonationContext = null;
			bool flag = false;
			WindowsIdentity windowsIdentity = null;
			Uri uri = null;
			int instanceID = 0;
			string text = null;
			TransactionProxy transactionProxy = null;
			Guid guidIncomingTransactionID = Guid.Empty;
			object result;
			try
			{
				try
				{
					windowsIdentity = MessageUtil.GetMessageIdentity(message);
					if (message.Headers.From != null)
					{
						uri = message.Headers.From.Uri;
					}
					object serviceInstance = instanceContext.GetServiceInstance(message);
					instanceID = serviceInstance.GetHashCode();
					text = message.Headers.Action;
					ComPlusMethodCallTrace.Trace(TraceEventType.Verbose, 327704, "TraceCodeComIntegrationInvokingMethod", this.info, uri, text, windowsIdentity.Name, this.iid, instanceID, false);
					if (this.info.CheckRoles && !this.comAuth.IsAuthorizedForOperation(windowsIdentity))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.CallAccessDenied());
					}
					if (this.info.HostingMode != HostingMode.WebHostOutOfProcess)
					{
						comPlusServerSecurity = new ComPlusServerSecurity(windowsIdentity, this.info.CheckRoles);
					}
					transactionProxy = instanceContext.Extensions.Find<TransactionProxy>();
					if (transactionProxy != null)
					{
						Transaction messageTransaction = MessageUtil.GetMessageTransaction(message);
						if (messageTransaction != null)
						{
							guidIncomingTransactionID = messageTransaction.TransactionInformation.DistributedIdentifier;
						}
						try
						{
							if (messageTransaction != null)
							{
								transactionProxy.SetTransaction(messageTransaction);
								ComPlusMethodCallTrace.Trace(TraceEventType.Verbose, 327706, "TraceCodeComIntegrationInvokingMethodNewTransaction", this.info, uri, text, windowsIdentity.Name, this.iid, instanceID, guidIncomingTransactionID);
								goto IL_2CD;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.TransactionMismatch());
						}
						catch (FaultException ex)
						{
							Transaction currentTransaction = transactionProxy.CurrentTransaction;
							Guid guid = Guid.Empty;
							if (currentTransaction != null)
							{
								guid = currentTransaction.TransactionInformation.DistributedIdentifier;
							}
							string text2 = string.Empty;
							if (windowsIdentity != null)
							{
								text2 = windowsIdentity.Name;
							}
							DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356571U, new string[]
							{
								guidIncomingTransactionID.ToString("B").ToUpperInvariant(),
								guid.ToString("B").ToUpperInvariant(),
								uri.ToString(),
								this.info.AppID.ToString("B").ToUpperInvariant(),
								this.info.Clsid.ToString("B").ToUpperInvariant(),
								this.iid.ToString(),
								text,
								instanceID.ToString(CultureInfo.InvariantCulture),
								Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture),
								SafeNativeMethods.GetCurrentThreadId().ToString(CultureInfo.InvariantCulture),
								text2,
								ex.ToString()
							});
							flag = true;
							throw;
						}
					}
					ComPlusMethodCallTrace.Trace(TraceEventType.Verbose, 327707, "TraceCodeComIntegrationInvokingMethodContextTransaction", this.info, uri, text, windowsIdentity.Name, this.iid, instanceID, true);
					IL_2CD:
					if (this.info.HostingMode == HostingMode.WebHostOutOfProcess)
					{
						windowsImpersonationContext = windowsIdentity.Impersonate();
					}
					ComPlusThreadInitializer.CorrelationState correlationState = new ComPlusThreadInitializer.CorrelationState(windowsImpersonationContext, comPlusServerSecurity, uri, text, windowsIdentity.Name, instanceID);
					windowsImpersonationContext = null;
					comPlusServerSecurity = null;
					result = correlationState;
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
					if (comPlusServerSecurity != null)
					{
						((IDisposable)comPlusServerSecurity).Dispose();
					}
				}
			}
			catch (Exception ex2)
			{
				if (!flag && DiagnosticUtility.ShouldTraceError)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356569U, new string[]
					{
						(uri == null) ? string.Empty : uri.ToString(),
						this.info.AppID.ToString("B").ToUpperInvariant(),
						this.info.Clsid.ToString("B").ToUpperInvariant(),
						this.iid.ToString("B").ToUpperInvariant(),
						text,
						instanceID.ToString(CultureInfo.InvariantCulture),
						Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture),
						SafeNativeMethods.GetCurrentThreadId().ToString(CultureInfo.InvariantCulture),
						windowsIdentity.Name,
						ex2.ToString()
					});
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0003A824 File Offset: 0x00038A24
		public void AfterInvoke(object correlationState)
		{
			ComPlusThreadInitializer.CorrelationState correlationState2 = (ComPlusThreadInitializer.CorrelationState)correlationState;
			if (correlationState2 != null)
			{
				ComPlusMethodCallTrace.Trace(TraceEventType.Verbose, 327705, "TraceCodeComIntegrationInvokedMethod", this.info, correlationState2.From, correlationState2.Action, correlationState2.CallerIdentity, this.iid, correlationState2.InstanceID, false);
				correlationState2.Cleanup();
			}
		}

		// Token: 0x04001864 RID: 6244
		private ServiceInfo info;

		// Token: 0x04001865 RID: 6245
		private ComPlusAuthorization comAuth;

		// Token: 0x04001866 RID: 6246
		private Guid iid;

		// Token: 0x02000B0E RID: 2830
		private class CorrelationState
		{
			// Token: 0x06006F6D RID: 28525 RVA: 0x0019DC64 File Offset: 0x0019BE64
			public CorrelationState(WindowsImpersonationContext context, ComPlusServerSecurity serverSecurity, Uri from, string action, string callerIdentity, int instanceID)
			{
				this.impersonationContext = context;
				this.serverSecurity = serverSecurity;
				this.from = from;
				this.action = action;
				this.callerIdentity = callerIdentity;
				this.instanceID = instanceID;
			}

			// Token: 0x170019FB RID: 6651
			// (get) Token: 0x06006F6E RID: 28526 RVA: 0x0019DC99 File Offset: 0x0019BE99
			public Uri From
			{
				get
				{
					return this.from;
				}
			}

			// Token: 0x170019FC RID: 6652
			// (get) Token: 0x06006F6F RID: 28527 RVA: 0x0019DCA1 File Offset: 0x0019BEA1
			public string Action
			{
				get
				{
					return this.action;
				}
			}

			// Token: 0x170019FD RID: 6653
			// (get) Token: 0x06006F70 RID: 28528 RVA: 0x0019DCA9 File Offset: 0x0019BEA9
			public string CallerIdentity
			{
				get
				{
					return this.callerIdentity;
				}
			}

			// Token: 0x170019FE RID: 6654
			// (get) Token: 0x06006F71 RID: 28529 RVA: 0x0019DCB1 File Offset: 0x0019BEB1
			public int InstanceID
			{
				get
				{
					return this.instanceID;
				}
			}

			// Token: 0x06006F72 RID: 28530 RVA: 0x0019DCB9 File Offset: 0x0019BEB9
			public void Cleanup()
			{
				if (this.impersonationContext != null)
				{
					this.impersonationContext.Undo();
				}
				if (this.serverSecurity != null)
				{
					((IDisposable)this.serverSecurity).Dispose();
				}
			}

			// Token: 0x04003F9C RID: 16284
			private WindowsImpersonationContext impersonationContext;

			// Token: 0x04003F9D RID: 16285
			private ComPlusServerSecurity serverSecurity;

			// Token: 0x04003F9E RID: 16286
			private Uri from;

			// Token: 0x04003F9F RID: 16287
			private string action;

			// Token: 0x04003FA0 RID: 16288
			private string callerIdentity;

			// Token: 0x04003FA1 RID: 16289
			private int instanceID;
		}
	}
}
