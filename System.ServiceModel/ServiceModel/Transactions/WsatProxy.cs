using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Text;
using System.Threading;
using System.Transactions;
using Microsoft.Transactions.Wsat.Messaging;
using Microsoft.Transactions.Wsat.Protocol;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001BA RID: 442
	internal class WsatProxy
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x00033E2F File Offset: 0x0003202F
		public WsatProxy(WsatConfiguration wsatConfig, ProtocolVersion protocolVersion)
		{
			this.wsatConfig = wsatConfig;
			this.protocolVersion = protocolVersion;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00033E50 File Offset: 0x00032050
		public Transaction UnmarshalTransaction(WsatTransactionInfo info)
		{
			if (info.Context.ProtocolVersion != this.protocolVersion)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidWsatProtocolVersion")));
			}
			if (this.wsatConfig.OleTxUpgradeEnabled)
			{
				byte[] propagationToken = info.Context.PropagationToken;
				if (propagationToken != null)
				{
					try
					{
						return OleTxTransactionInfo.UnmarshalPropagationToken(propagationToken);
					}
					catch (TransactionException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 917518, SR.GetString("TraceCodeTxFailedToNegotiateOleTx", new object[]
						{
							info.Context.Identifier
						}));
					}
				}
			}
			CoordinationContext coordinationContext = info.Context;
			if (!this.wsatConfig.IsLocalRegistrationService(coordinationContext.RegistrationService, this.protocolVersion))
			{
				if (!this.wsatConfig.IsProtocolServiceEnabled(this.protocolVersion))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("WsatProtocolServiceDisabled", new object[]
					{
						this.protocolVersion
					})));
				}
				if (!this.wsatConfig.InboundEnabled)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("InboundTransactionsDisabled")));
				}
				if (this.wsatConfig.IsDisabledRegistrationService(coordinationContext.RegistrationService))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("SourceTransactionsDisabled")));
				}
				coordinationContext = this.CreateCoordinationContext(info);
			}
			Guid localTransactionId = coordinationContext.LocalTransactionId;
			if (localTransactionId == Guid.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("InvalidCoordinationContextTransactionId")));
			}
			byte[] propToken = WsatProxy.MarshalPropagationToken(ref localTransactionId, coordinationContext.IsolationLevel, coordinationContext.IsolationFlags, coordinationContext.Description);
			return OleTxTransactionInfo.UnmarshalPropagationToken(propToken);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00034010 File Offset: 0x00032210
		private CoordinationContext CreateCoordinationContext(WsatTransactionInfo info)
		{
			CreateCoordinationContext createCoordinationContext = new CreateCoordinationContext(this.protocolVersion);
			createCoordinationContext.CurrentContext = info.Context;
			createCoordinationContext.IssuedToken = info.IssuedToken;
			CoordinationContext coordinationContext;
			try
			{
				using (new OperationContextScope(null))
				{
					coordinationContext = this.Enlist(ref createCoordinationContext).CoordinationContext;
				}
			}
			catch (WsatFaultException ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("UnmarshalTransactionFaulted", new object[]
				{
					ex.Message
				}), ex));
			}
			catch (WsatSendFailureException ex2)
			{
				DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionManagerCommunicationException(SR.GetString("TMCommunicationError"), ex2));
			}
			return coordinationContext;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000340E8 File Offset: 0x000322E8
		private CreateCoordinationContextResponse Enlist(ref CreateCoordinationContext cccMessage)
		{
			int num = 0;
			CreateCoordinationContextResponse result;
			for (;;)
			{
				ActivationProxy activationProxy = this.GetActivationProxy();
				EndpointAddress endpointAddress = activationProxy.To;
				EndpointAddress endpointAddress2 = this.wsatConfig.LocalActivationService(this.protocolVersion);
				EndpointAddress endpointAddress3 = this.wsatConfig.RemoteActivationService(this.protocolVersion);
				try
				{
					result = activationProxy.SendCreateCoordinationContext(ref cccMessage);
					break;
				}
				catch (WsatSendFailureException ex)
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
					Exception innerException = ex.InnerException;
					if (innerException is TimeoutException || innerException is QuotaExceededException || innerException is FaultException)
					{
						throw;
					}
					if (num > 10)
					{
						throw;
					}
					if (num > 5 && endpointAddress3 != null && endpointAddress == endpointAddress2)
					{
						endpointAddress = endpointAddress3;
					}
				}
				finally
				{
					activationProxy.Release();
				}
				this.TryStartMsdtcService();
				this.RefreshActivationProxy(endpointAddress);
				Thread.Sleep(0);
				num++;
			}
			return result;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000341C4 File Offset: 0x000323C4
		private void TryStartMsdtcService()
		{
			try
			{
				TransactionInterop.GetWhereabouts();
			}
			catch (TransactionException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000341F4 File Offset: 0x000323F4
		private ActivationProxy GetActivationProxy()
		{
			if (this.activationProxy == null)
			{
				this.RefreshActivationProxy(null);
			}
			object obj = this.proxyLock;
			ActivationProxy result;
			lock (obj)
			{
				ActivationProxy activationProxy = this.activationProxy;
				activationProxy.AddRef();
				result = activationProxy;
			}
			return result;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00034250 File Offset: 0x00032450
		private void RefreshActivationProxy(EndpointAddress suggestedAddress)
		{
			EndpointAddress endpointAddress = suggestedAddress;
			if (endpointAddress == null)
			{
				endpointAddress = this.wsatConfig.LocalActivationService(this.protocolVersion);
				if (endpointAddress == null)
				{
					endpointAddress = this.wsatConfig.RemoteActivationService(this.protocolVersion);
				}
			}
			if (!(endpointAddress != null))
			{
				DiagnosticUtility.FailFast("Must have valid activation service address");
			}
			object obj = this.proxyLock;
			lock (obj)
			{
				ActivationProxy activationProxy = this.CreateActivationProxy(endpointAddress);
				if (this.activationProxy != null)
				{
					this.activationProxy.Release();
				}
				this.activationProxy = activationProxy;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000342F8 File Offset: 0x000324F8
		private ActivationProxy CreateActivationProxy(EndpointAddress address)
		{
			CoordinationService coordinationService = this.GetCoordinationService();
			ActivationProxy result;
			try
			{
				result = coordinationService.CreateActivationProxy(address, false);
			}
			catch (CreateChannelFailureException ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("WsatProxyCreationFailed"), ex));
			}
			return result;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003434C File Offset: 0x0003254C
		private CoordinationService GetCoordinationService()
		{
			if (this.coordinationService == null)
			{
				object obj = this.proxyLock;
				lock (obj)
				{
					if (this.coordinationService == null)
					{
						try
						{
							this.coordinationService = new CoordinationService(new CoordinationServiceConfiguration
							{
								Mode = CoordinationServiceMode.Formatter,
								RemoteClientsEnabled = (this.wsatConfig.RemoteActivationService(this.protocolVersion) != null)
							}, this.protocolVersion);
						}
						catch (MessagingInitializationException ex)
						{
							DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException(SR.GetString("WsatMessagingInitializationFailed"), ex));
						}
					}
				}
			}
			return this.coordinationService;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00034410 File Offset: 0x00032610
		private static byte[] CreateFixedPropagationToken()
		{
			if (WsatProxy.fixedPropagationToken == null)
			{
				CommittableTransaction committableTransaction = new CommittableTransaction();
				byte[] transmitterPropagationToken = TransactionInterop.GetTransmitterPropagationToken(committableTransaction);
				try
				{
					committableTransaction.Commit();
				}
				catch (TransactionException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				Interlocked.CompareExchange<byte[]>(ref WsatProxy.fixedPropagationToken, transmitterPropagationToken, null);
			}
			byte[] array = new byte[WsatProxy.fixedPropagationToken.Length];
			Array.Copy(WsatProxy.fixedPropagationToken, array, WsatProxy.fixedPropagationToken.Length);
			return array;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00034480 File Offset: 0x00032680
		private static byte[] MarshalPropagationToken(ref Guid transactionId, IsolationLevel isoLevel, IsolationFlags isoFlags, string description)
		{
			byte[] array = WsatProxy.CreateFixedPropagationToken();
			byte[] array2 = transactionId.ToByteArray();
			Array.Copy(array2, 0, array, 8, array2.Length);
			byte[] bytes = BitConverter.GetBytes((int)WsatProxy.ConvertIsolationLevel(isoLevel));
			Array.Copy(bytes, 0, array, 24, bytes.Length);
			byte[] bytes2 = BitConverter.GetBytes((int)isoFlags);
			Array.Copy(bytes2, 0, array, 28, bytes2.Length);
			if (!string.IsNullOrEmpty(description))
			{
				byte[] bytes3 = Encoding.UTF8.GetBytes(description);
				int num = Math.Min(bytes3.Length, 39);
				Array.Copy(bytes3, 0, array, 36, num);
				array[36 + num] = 0;
			}
			return array;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003450C File Offset: 0x0003270C
		private static WsatProxy.ProxyIsolationLevel ConvertIsolationLevel(IsolationLevel IsolationLevel)
		{
			switch (IsolationLevel)
			{
			case IsolationLevel.Serializable:
				return WsatProxy.ProxyIsolationLevel.Serializable;
			case IsolationLevel.RepeatableRead:
				return WsatProxy.ProxyIsolationLevel.RepeatableRead;
			case IsolationLevel.ReadCommitted:
				return WsatProxy.ProxyIsolationLevel.CursorStability;
			case IsolationLevel.ReadUncommitted:
				return WsatProxy.ProxyIsolationLevel.ReadUncommitted;
			case IsolationLevel.Unspecified:
				return WsatProxy.ProxyIsolationLevel.Unspecified;
			}
			return WsatProxy.ProxyIsolationLevel.Serializable;
		}

		// Token: 0x0400175F RID: 5983
		private WsatConfiguration wsatConfig;

		// Token: 0x04001760 RID: 5984
		private ProtocolVersion protocolVersion;

		// Token: 0x04001761 RID: 5985
		private CoordinationService coordinationService;

		// Token: 0x04001762 RID: 5986
		private ActivationProxy activationProxy;

		// Token: 0x04001763 RID: 5987
		private object proxyLock = new object();

		// Token: 0x04001764 RID: 5988
		private static byte[] fixedPropagationToken;

		// Token: 0x02000AFD RID: 2813
		private enum ProxyIsolationLevel
		{
			// Token: 0x04003F67 RID: 16231
			Unspecified = -1,
			// Token: 0x04003F68 RID: 16232
			Chaos = 16,
			// Token: 0x04003F69 RID: 16233
			ReadUncommitted = 256,
			// Token: 0x04003F6A RID: 16234
			Browse = 256,
			// Token: 0x04003F6B RID: 16235
			CursorStability = 4096,
			// Token: 0x04003F6C RID: 16236
			ReadCommitted = 4096,
			// Token: 0x04003F6D RID: 16237
			RepeatableRead = 65536,
			// Token: 0x04003F6E RID: 16238
			Serializable = 1048576,
			// Token: 0x04003F6F RID: 16239
			Isolated = 1048576
		}
	}
}
