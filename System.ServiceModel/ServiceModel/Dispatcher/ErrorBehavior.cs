using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000544 RID: 1348
	internal class ErrorBehavior
	{
		// Token: 0x06003343 RID: 13123 RVA: 0x000C5EDC File Offset: 0x000C40DC
		internal ErrorBehavior(ChannelDispatcher channelDispatcher)
		{
			this.handlers = EmptyArray<IErrorHandler>.ToArray(channelDispatcher.ErrorHandlers);
			this.debug = channelDispatcher.IncludeExceptionDetailInFaults;
			this.isOnServer = channelDispatcher.IsOnServer;
			this.messageVersion = channelDispatcher.MessageVersion;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000C5F1C File Offset: 0x000C411C
		private void InitializeFault(ref MessageRpc rpc)
		{
			Exception error = rpc.Error;
			FaultException ex = error as FaultException;
			if (ex != null)
			{
				string defaultFaultAction;
				MessageFault messageFault = rpc.Operation.FaultFormatter.Serialize(ex, out defaultFaultAction);
				if (defaultFaultAction == null)
				{
					defaultFaultAction = rpc.RequestVersion.Addressing.DefaultFaultAction;
				}
				if (messageFault != null)
				{
					rpc.FaultInfo.Fault = Message.CreateMessage(rpc.RequestVersion, messageFault, defaultFaultAction);
				}
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000C5F7D File Offset: 0x000C417D
		internal IErrorHandler[] Handlers
		{
			get
			{
				return this.handlers;
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000C5F85 File Offset: 0x000C4185
		internal void ProvideMessageFault(ref MessageRpc rpc)
		{
			if (rpc.Error != null)
			{
				this.ProvideMessageFaultCore(ref rpc);
			}
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000C5F96 File Offset: 0x000C4196
		private void ProvideMessageFaultCore(ref MessageRpc rpc)
		{
			MessageVersion messageVersion = this.messageVersion;
			MessageVersion requestVersion = rpc.RequestVersion;
			this.InitializeFault(ref rpc);
			this.ProvideFault(rpc.Error, rpc.Channel.GetProperty<FaultConverter>(), ref rpc.FaultInfo);
			this.ProvideMessageFaultCoreCoda(ref rpc);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000C5FD4 File Offset: 0x000C41D4
		private void ProvideFaultOfLastResort(Exception error, ref ErrorHandlerFaultInfo faultInfo)
		{
			if (faultInfo.Fault == null)
			{
				FaultCode faultCode = new FaultCode("InternalServiceFault", "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher");
				faultCode = FaultCode.CreateReceiverFaultCode(faultCode);
				string text = "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault";
				MessageFault fault;
				if (this.debug)
				{
					faultInfo.DefaultFaultAction = text;
					fault = MessageFault.CreateFault(faultCode, new FaultReason(error.Message), new ExceptionDetail(error));
				}
				else
				{
					string text2 = this.isOnServer ? SR.GetString("SFxInternalServerError") : SR.GetString("SFxInternalCallbackError");
					fault = MessageFault.CreateFault(faultCode, new FaultReason(text2));
				}
				faultInfo.IsConsideredUnhandled = true;
				faultInfo.Fault = Message.CreateMessage(this.messageVersion, fault, text);
				return;
			}
			if (error != null)
			{
				FaultException ex = error as FaultException;
				if (ex != null && ex.Fault != null && ex.Fault.Code != null && ex.Fault.Code.SubCode != null && string.Compare(ex.Fault.Code.SubCode.Namespace, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher", StringComparison.Ordinal) == 0 && string.Compare(ex.Fault.Code.SubCode.Name, "InternalServiceFault", StringComparison.Ordinal) == 0)
				{
					faultInfo.IsConsideredUnhandled = true;
				}
			}
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000C6104 File Offset: 0x000C4304
		private void ProvideMessageFaultCoreCoda(ref MessageRpc rpc)
		{
			if (rpc.FaultInfo.Fault.Headers.Action == null)
			{
				rpc.FaultInfo.Fault.Headers.Action = rpc.RequestVersion.Addressing.DefaultFaultAction;
			}
			rpc.Reply = rpc.FaultInfo.Fault;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000C615E File Offset: 0x000C435E
		internal void ProvideOnlyFaultOfLastResort(ref MessageRpc rpc)
		{
			this.ProvideFaultOfLastResort(rpc.Error, ref rpc.FaultInfo);
			this.ProvideMessageFaultCoreCoda(ref rpc);
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000C617C File Offset: 0x000C437C
		internal void ProvideFault(Exception e, FaultConverter faultConverter, ref ErrorHandlerFaultInfo faultInfo)
		{
			this.ProvideWellKnownFault(e, faultConverter, ref faultInfo);
			for (int i = 0; i < this.handlers.Length; i++)
			{
				Message fault = faultInfo.Fault;
				this.handlers[i].ProvideFault(e, this.messageVersion, ref fault);
				faultInfo.Fault = fault;
				if (TD.FaultProviderInvokedIsEnabled())
				{
					TD.FaultProviderInvoked(this.handlers[i].GetType().FullName, e.Message);
				}
			}
			this.ProvideFaultOfLastResort(e, ref faultInfo);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000C61F8 File Offset: 0x000C43F8
		private void ProvideWellKnownFault(Exception e, FaultConverter faultConverter, ref ErrorHandlerFaultInfo faultInfo)
		{
			Message fault;
			if (faultConverter != null && faultConverter.TryCreateFaultMessage(e, out fault))
			{
				faultInfo.Fault = fault;
				return;
			}
			if (e is NetDispatcherFaultException)
			{
				NetDispatcherFaultException ex = e as NetDispatcherFaultException;
				if (this.debug)
				{
					ExceptionDetail detail = new ExceptionDetail(ex);
					faultInfo.Fault = Message.CreateMessage(this.messageVersion, MessageFault.CreateFault(ex.Code, ex.Reason, detail), ex.Action);
					return;
				}
				faultInfo.Fault = Message.CreateMessage(this.messageVersion, ex.CreateMessageFault(), ex.Action);
			}
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000C6280 File Offset: 0x000C4480
		internal void HandleError(ref MessageRpc rpc)
		{
			if (rpc.Error != null)
			{
				this.HandleErrorCore(ref rpc);
			}
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000C6294 File Offset: 0x000C4494
		private void HandleErrorCore(ref MessageRpc rpc)
		{
			bool flag = this.HandleErrorCommon(rpc.Error, ref rpc.FaultInfo);
			if (flag)
			{
				rpc.Error = null;
			}
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000C62C0 File Offset: 0x000C44C0
		private bool HandleErrorCommon(Exception error, ref ErrorHandlerFaultInfo faultInfo)
		{
			bool flag = faultInfo.Fault != null && !faultInfo.IsConsideredUnhandled;
			try
			{
				if (TD.ServiceExceptionIsEnabled())
				{
					TD.ServiceException(null, error.ToString(), error.GetType().FullName);
				}
				for (int i = 0; i < this.handlers.Length; i++)
				{
					bool flag2 = this.handlers[i].HandleError(error);
					flag = (flag2 || flag);
					if (TD.ErrorHandlerInvokedIsEnabled())
					{
						TD.ErrorHandlerInvoked(this.handlers[i].GetType().FullName, flag2, error.GetType().FullName);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
			return flag;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000C637C File Offset: 0x000C457C
		internal bool HandleError(Exception error)
		{
			ErrorHandlerFaultInfo errorHandlerFaultInfo = new ErrorHandlerFaultInfo(this.messageVersion.Addressing.DefaultFaultAction);
			return this.HandleError(error, ref errorHandlerFaultInfo);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000C63A9 File Offset: 0x000C45A9
		internal bool HandleError(Exception error, ref ErrorHandlerFaultInfo faultInfo)
		{
			return this.HandleErrorCommon(error, ref faultInfo);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C63B3 File Offset: 0x000C45B3
		internal static bool ShouldRethrowExceptionAsIs(Exception e)
		{
			return true;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C63B6 File Offset: 0x000C45B6
		internal static bool ShouldRethrowClientSideExceptionAsIs(Exception e)
		{
			return true;
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000C63BC File Offset: 0x000C45BC
		internal static void ThrowAndCatch(Exception e, Message message)
		{
			try
			{
				if (Debugger.IsAttached)
				{
					if (message == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(e);
					}
					throw TraceUtility.ThrowHelperError(e, message);
				}
				else
				{
					if (message == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(e);
					}
					TraceUtility.ThrowHelperError(e, message);
				}
			}
			catch (Exception ex)
			{
				if (e != ex)
				{
					throw;
				}
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000C641C File Offset: 0x000C461C
		internal static void ThrowAndCatch(Exception e)
		{
			ErrorBehavior.ThrowAndCatch(e, null);
		}

		// Token: 0x04002778 RID: 10104
		private IErrorHandler[] handlers;

		// Token: 0x04002779 RID: 10105
		private bool debug;

		// Token: 0x0400277A RID: 10106
		private bool isOnServer;

		// Token: 0x0400277B RID: 10107
		private MessageVersion messageVersion;
	}
}
