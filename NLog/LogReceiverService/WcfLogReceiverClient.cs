using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace NLog.LogReceiverService
{
	// Token: 0x02000138 RID: 312
	public sealed class WcfLogReceiverClient : IWcfLogReceiverClient, ICommunicationObject
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x000195A8 File Offset: 0x000177A8
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x000195B0 File Offset: 0x000177B0
		public IWcfLogReceiverClient ProxiedClient { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x000195B9 File Offset: 0x000177B9
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x000195C1 File Offset: 0x000177C1
		public bool UseOneWay { get; private set; }

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000195CC File Offset: 0x000177CC
		public WcfLogReceiverClient(bool useOneWay)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient proxiedClient;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient();
				proxiedClient = wcfLogReceiverClient;
			}
			else
			{
				proxiedClient = new WcfLogReceiverOneWayClient();
			}
			this.ProxiedClient = proxiedClient;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00019600 File Offset: 0x00017800
		public WcfLogReceiverClient(bool useOneWay, string endpointConfigurationName)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient proxiedClient;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(endpointConfigurationName);
				proxiedClient = wcfLogReceiverClient;
			}
			else
			{
				proxiedClient = new WcfLogReceiverOneWayClient(endpointConfigurationName);
			}
			this.ProxiedClient = proxiedClient;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00019634 File Offset: 0x00017834
		public WcfLogReceiverClient(bool useOneWay, string endpointConfigurationName, string remoteAddress)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient proxiedClient;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(endpointConfigurationName, remoteAddress);
				proxiedClient = wcfLogReceiverClient;
			}
			else
			{
				proxiedClient = new WcfLogReceiverOneWayClient(endpointConfigurationName, remoteAddress);
			}
			this.ProxiedClient = proxiedClient;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001966C File Offset: 0x0001786C
		public WcfLogReceiverClient(bool useOneWay, string endpointConfigurationName, EndpointAddress remoteAddress)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient proxiedClient;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(endpointConfigurationName, remoteAddress);
				proxiedClient = wcfLogReceiverClient;
			}
			else
			{
				proxiedClient = new WcfLogReceiverOneWayClient(endpointConfigurationName, remoteAddress);
			}
			this.ProxiedClient = proxiedClient;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000196A4 File Offset: 0x000178A4
		public WcfLogReceiverClient(bool useOneWay, Binding binding, EndpointAddress remoteAddress)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient proxiedClient;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(binding, remoteAddress);
				proxiedClient = wcfLogReceiverClient;
			}
			else
			{
				proxiedClient = new WcfLogReceiverOneWayClient(binding, remoteAddress);
			}
			this.ProxiedClient = proxiedClient;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000196D9 File Offset: 0x000178D9
		public void Abort()
		{
			this.ProxiedClient.Abort();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000196E6 File Offset: 0x000178E6
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginClose(callback, state);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000196F5 File Offset: 0x000178F5
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginClose(timeout, callback, state);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00019705 File Offset: 0x00017905
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginOpen(callback, state);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00019714 File Offset: 0x00017914
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00019724 File Offset: 0x00017924
		public IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return this.ProxiedClient.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00019734 File Offset: 0x00017934
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.ProxiedClient.ClientCredentials;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00019741 File Offset: 0x00017941
		public void Close(TimeSpan timeout)
		{
			this.ProxiedClient.Close(timeout);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001974F File Offset: 0x0001794F
		public void Close()
		{
			this.ProxiedClient.Close();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001975C File Offset: 0x0001795C
		public void CloseAsync(object userState)
		{
			this.ProxiedClient.CloseAsync(userState);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001976A File Offset: 0x0001796A
		public void CloseAsync()
		{
			this.ProxiedClient.CloseAsync();
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000AE9 RID: 2793 RVA: 0x00019777 File Offset: 0x00017977
		// (remove) Token: 0x06000AEA RID: 2794 RVA: 0x00019785 File Offset: 0x00017985
		public event EventHandler<AsyncCompletedEventArgs> CloseCompleted
		{
			add
			{
				this.ProxiedClient.CloseCompleted += value;
			}
			remove
			{
				this.ProxiedClient.CloseCompleted -= value;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000AEB RID: 2795 RVA: 0x00019793 File Offset: 0x00017993
		// (remove) Token: 0x06000AEC RID: 2796 RVA: 0x000197A1 File Offset: 0x000179A1
		public event EventHandler Closed
		{
			add
			{
				this.ProxiedClient.Closed += value;
			}
			remove
			{
				this.ProxiedClient.Closed -= value;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000AED RID: 2797 RVA: 0x000197AF File Offset: 0x000179AF
		// (remove) Token: 0x06000AEE RID: 2798 RVA: 0x000197BD File Offset: 0x000179BD
		public event EventHandler Closing
		{
			add
			{
				this.ProxiedClient.Closing += value;
			}
			remove
			{
				this.ProxiedClient.Closing -= value;
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000197CB File Offset: 0x000179CB
		public void DisplayInitializationUI()
		{
			this.ProxiedClient.DisplayInitializationUI();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000197D8 File Offset: 0x000179D8
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x000197E5 File Offset: 0x000179E5
		public CookieContainer CookieContainer
		{
			get
			{
				return this.ProxiedClient.CookieContainer;
			}
			set
			{
				this.ProxiedClient.CookieContainer = value;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000197F3 File Offset: 0x000179F3
		public void EndClose(IAsyncResult result)
		{
			this.ProxiedClient.EndClose(result);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00019801 File Offset: 0x00017A01
		public void EndOpen(IAsyncResult result)
		{
			this.ProxiedClient.EndOpen(result);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001980F File Offset: 0x00017A0F
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.ProxiedClient.Endpoint;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001981C File Offset: 0x00017A1C
		public void EndProcessLogMessages(IAsyncResult result)
		{
			this.ProxiedClient.EndProcessLogMessages(result);
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000AF6 RID: 2806 RVA: 0x0001982A File Offset: 0x00017A2A
		// (remove) Token: 0x06000AF7 RID: 2807 RVA: 0x00019838 File Offset: 0x00017A38
		public event EventHandler Faulted
		{
			add
			{
				this.ProxiedClient.Faulted += value;
			}
			remove
			{
				this.ProxiedClient.Faulted -= value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00019846 File Offset: 0x00017A46
		public IClientChannel InnerChannel
		{
			get
			{
				return this.ProxiedClient.InnerChannel;
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00019853 File Offset: 0x00017A53
		public void Open()
		{
			this.ProxiedClient.Open();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00019860 File Offset: 0x00017A60
		public void Open(TimeSpan timeout)
		{
			this.ProxiedClient.Open(timeout);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001986E File Offset: 0x00017A6E
		public void OpenAsync()
		{
			this.ProxiedClient.OpenAsync();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001987B File Offset: 0x00017A7B
		public void OpenAsync(object userState)
		{
			this.ProxiedClient.OpenAsync(userState);
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000AFD RID: 2813 RVA: 0x00019889 File Offset: 0x00017A89
		// (remove) Token: 0x06000AFE RID: 2814 RVA: 0x00019897 File Offset: 0x00017A97
		public event EventHandler<AsyncCompletedEventArgs> OpenCompleted
		{
			add
			{
				this.ProxiedClient.OpenCompleted += value;
			}
			remove
			{
				this.ProxiedClient.OpenCompleted -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000AFF RID: 2815 RVA: 0x000198A5 File Offset: 0x00017AA5
		// (remove) Token: 0x06000B00 RID: 2816 RVA: 0x000198B3 File Offset: 0x00017AB3
		public event EventHandler Opened
		{
			add
			{
				this.ProxiedClient.Opened += value;
			}
			remove
			{
				this.ProxiedClient.Opened -= value;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000B01 RID: 2817 RVA: 0x000198C1 File Offset: 0x00017AC1
		// (remove) Token: 0x06000B02 RID: 2818 RVA: 0x000198CF File Offset: 0x00017ACF
		public event EventHandler Opening
		{
			add
			{
				this.ProxiedClient.Opening += value;
			}
			remove
			{
				this.ProxiedClient.Opening -= value;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000198DD File Offset: 0x00017ADD
		public void ProcessLogMessagesAsync(NLogEvents events)
		{
			this.ProxiedClient.ProcessLogMessagesAsync(events);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000198EB File Offset: 0x00017AEB
		public void ProcessLogMessagesAsync(NLogEvents events, object userState)
		{
			this.ProxiedClient.ProcessLogMessagesAsync(events, userState);
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000B05 RID: 2821 RVA: 0x000198FA File Offset: 0x00017AFA
		// (remove) Token: 0x06000B06 RID: 2822 RVA: 0x00019908 File Offset: 0x00017B08
		public event EventHandler<AsyncCompletedEventArgs> ProcessLogMessagesCompleted
		{
			add
			{
				this.ProxiedClient.ProcessLogMessagesCompleted += value;
			}
			remove
			{
				this.ProxiedClient.ProcessLogMessagesCompleted -= value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00019916 File Offset: 0x00017B16
		public CommunicationState State
		{
			get
			{
				return this.ProxiedClient.State;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00019923 File Offset: 0x00017B23
		public void CloseCommunicationObject()
		{
			this.ProxiedClient.Close();
		}
	}
}
