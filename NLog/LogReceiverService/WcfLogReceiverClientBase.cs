using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;

namespace NLog.LogReceiverService
{
	// Token: 0x02000136 RID: 310
	public abstract class WcfLogReceiverClientBase<TService> : ClientBase<TService>, IWcfLogReceiverClient, ICommunicationObject where TService : class
	{
		// Token: 0x06000AAC RID: 2732 RVA: 0x00019199 File Offset: 0x00017399
		internal WcfLogReceiverClientBase()
		{
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000191A1 File Offset: 0x000173A1
		internal WcfLogReceiverClientBase(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000191AA File Offset: 0x000173AA
		internal WcfLogReceiverClientBase(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000191B4 File Offset: 0x000173B4
		internal WcfLogReceiverClientBase(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000191BE File Offset: 0x000173BE
		internal WcfLogReceiverClientBase(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x000191C8 File Offset: 0x000173C8
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x00019200 File Offset: 0x00017400
		public event EventHandler<AsyncCompletedEventArgs> ProcessLogMessagesCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000AB3 RID: 2739 RVA: 0x00019238 File Offset: 0x00017438
		// (remove) Token: 0x06000AB4 RID: 2740 RVA: 0x00019270 File Offset: 0x00017470
		public event EventHandler<AsyncCompletedEventArgs> OpenCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000AB5 RID: 2741 RVA: 0x000192A8 File Offset: 0x000174A8
		// (remove) Token: 0x06000AB6 RID: 2742 RVA: 0x000192E0 File Offset: 0x000174E0
		public event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00019318 File Offset: 0x00017518
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0001933C File Offset: 0x0001753C
		public CookieContainer CookieContainer
		{
			get
			{
				IHttpCookieContainerManager property = base.InnerChannel.GetProperty<IHttpCookieContainerManager>();
				if (property != null)
				{
					return property.CookieContainer;
				}
				return null;
			}
			set
			{
				IHttpCookieContainerManager property = base.InnerChannel.GetProperty<IHttpCookieContainerManager>();
				if (property != null)
				{
					property.CookieContainer = value;
					return;
				}
				throw new InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpCookieContainerBindingElement.");
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001936A File Offset: 0x0001756A
		public void OpenAsync()
		{
			this.OpenAsync(null);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00019373 File Offset: 0x00017573
		public void OpenAsync(object userState)
		{
			base.InvokeAsync(new ClientBase<TService>.BeginOperationDelegate(this.OnBeginOpen), null, new ClientBase<TService>.EndOperationDelegate(this.OnEndOpen), new SendOrPostCallback(this.OnOpenCompleted), userState);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000193A1 File Offset: 0x000175A1
		public void CloseAsync()
		{
			this.CloseAsync(null);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000193AA File Offset: 0x000175AA
		public void CloseAsync(object userState)
		{
			base.InvokeAsync(new ClientBase<TService>.BeginOperationDelegate(this.OnBeginClose), null, new ClientBase<TService>.EndOperationDelegate(this.OnEndClose), new SendOrPostCallback(this.OnCloseCompleted), userState);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000193D8 File Offset: 0x000175D8
		public void ProcessLogMessagesAsync(NLogEvents events)
		{
			this.ProcessLogMessagesAsync(events, null);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000193E4 File Offset: 0x000175E4
		public void ProcessLogMessagesAsync(NLogEvents events, object userState)
		{
			base.InvokeAsync(new ClientBase<TService>.BeginOperationDelegate(this.OnBeginProcessLogMessages), new object[]
			{
				events
			}, new ClientBase<TService>.EndOperationDelegate(this.OnEndProcessLogMessages), new SendOrPostCallback(this.OnProcessLogMessagesCompleted), userState);
		}

		// Token: 0x06000ABF RID: 2751
		public abstract IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x06000AC0 RID: 2752
		public abstract void EndProcessLogMessages(IAsyncResult result);

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00019428 File Offset: 0x00017628
		private IAsyncResult OnBeginProcessLogMessages(object[] inValues, AsyncCallback callback, object asyncState)
		{
			NLogEvents events = (NLogEvents)inValues[0];
			return this.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00019447 File Offset: 0x00017647
		private object[] OnEndProcessLogMessages(IAsyncResult result)
		{
			this.EndProcessLogMessages(result);
			return null;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00019454 File Offset: 0x00017654
		private void OnProcessLogMessagesCompleted(object state)
		{
			if (this.ProcessLogMessagesCompleted != null)
			{
				ClientBase<TService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<TService>.InvokeAsyncCompletedEventArgs)state;
				this.ProcessLogMessagesCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00019493 File Offset: 0x00017693
		private IAsyncResult OnBeginOpen(object[] inValues, AsyncCallback callback, object asyncState)
		{
			return ((ICommunicationObject)this).BeginOpen(callback, asyncState);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001949D File Offset: 0x0001769D
		private object[] OnEndOpen(IAsyncResult result)
		{
			((ICommunicationObject)this).EndOpen(result);
			return null;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000194A8 File Offset: 0x000176A8
		private void OnOpenCompleted(object state)
		{
			if (this.OpenCompleted != null)
			{
				ClientBase<TService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<TService>.InvokeAsyncCompletedEventArgs)state;
				this.OpenCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000194E7 File Offset: 0x000176E7
		private IAsyncResult OnBeginClose(object[] inValues, AsyncCallback callback, object asyncState)
		{
			return ((ICommunicationObject)this).BeginClose(callback, asyncState);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000194F1 File Offset: 0x000176F1
		private object[] OnEndClose(IAsyncResult result)
		{
			((ICommunicationObject)this).EndClose(result);
			return null;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000194FC File Offset: 0x000176FC
		private void OnCloseCompleted(object state)
		{
			if (this.CloseCompleted != null)
			{
				ClientBase<TService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<TService>.InvokeAsyncCompletedEventArgs)state;
				this.CloseCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001953B File Offset: 0x0001773B
		ClientCredentials IWcfLogReceiverClient.get_ClientCredentials()
		{
			return base.ClientCredentials;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00019543 File Offset: 0x00017743
		IClientChannel IWcfLogReceiverClient.get_InnerChannel()
		{
			return base.InnerChannel;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001954B File Offset: 0x0001774B
		ServiceEndpoint IWcfLogReceiverClient.get_Endpoint()
		{
			return base.Endpoint;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00019553 File Offset: 0x00017753
		void IWcfLogReceiverClient.DisplayInitializationUI()
		{
			base.DisplayInitializationUI();
		}
	}
}
