using System;
using System.Collections.Generic;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000871 RID: 2161
	internal abstract class HttpTransportManager : TransportManager, ITransportManagerRegistration
	{
		// Token: 0x060051C7 RID: 20935 RVA: 0x0012CFC9 File Offset: 0x0012B1C9
		internal HttpTransportManager()
		{
			this.addressTables = new Dictionary<string, UriPrefixTable<HttpChannelListener>>();
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0012CFDE File Offset: 0x0012B1DE
		internal HttpTransportManager(Uri listenUri, HostNameComparisonMode hostNameComparisonMode) : this()
		{
			this.hostNameComparisonMode = hostNameComparisonMode;
			this.listenUri = listenUri;
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0012CFF4 File Offset: 0x0012B1F4
		internal HttpTransportManager(Uri listenUri, HostNameComparisonMode hostNameComparisonMode, string realm) : this(listenUri, hostNameComparisonMode)
		{
			this.realm = realm;
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x060051CA RID: 20938 RVA: 0x0012D005 File Offset: 0x0012B205
		internal string Realm
		{
			get
			{
				return this.realm;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x0012D00D File Offset: 0x0012B20D
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x0012D015 File Offset: 0x0012B215
		// (set) Token: 0x060051CD RID: 20941 RVA: 0x0012D01D File Offset: 0x0012B21D
		internal bool IsHosted { get; set; }

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x060051CE RID: 20942 RVA: 0x0012D026 File Offset: 0x0012B226
		internal override string Scheme
		{
			get
			{
				return Uri.UriSchemeHttp;
			}
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x0012D02D File Offset: 0x0012B22D
		internal virtual UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return HttpChannelListener.StaticTransportManagerTable;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x060051D0 RID: 20944 RVA: 0x0012D034 File Offset: 0x0012B234
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0012D03C File Offset: 0x0012B23C
		protected void Fault(Exception exception)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				foreach (KeyValuePair<string, UriPrefixTable<HttpChannelListener>> keyValuePair in this.addressTables)
				{
					base.Fault<HttpChannelListener>(keyValuePair.Value, exception);
				}
			}
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0012D0C0 File Offset: 0x0012B2C0
		internal virtual bool IsCompatible(HttpChannelListener listener)
		{
			return this.hostNameComparisonMode == listener.HostNameComparisonMode && this.realm == listener.Realm;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0012D0E3 File Offset: 0x0012B2E3
		internal override void OnClose(TimeSpan timeout)
		{
			this.Cleanup();
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0012D0EB File Offset: 0x0012B2EB
		internal override void OnAbort()
		{
			this.Cleanup();
			base.OnAbort();
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0012D0F9 File Offset: 0x0012B2F9
		private void Cleanup()
		{
			this.TransportManagerTable.UnregisterUri(this.ListenUri, this.HostNameComparisonMode);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x0012D112 File Offset: 0x0012B312
		protected void StartReceiveBytesActivity(ServiceModelActivity activity, Uri requestUri)
		{
			ServiceModelActivity.Start(activity, SR.GetString("ActivityReceiveBytes", new object[]
			{
				requestUri.ToString()
			}), ActivityType.ReceiveBytes);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x0012D135 File Offset: 0x0012B335
		protected void TraceMessageReceived(EventTraceActivity eventTraceActivity, Uri listenUri)
		{
			if (TD.HttpMessageReceiveStartIsEnabled())
			{
				TD.HttpMessageReceiveStart(eventTraceActivity);
			}
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0012D144 File Offset: 0x0012B344
		protected bool TryLookupUri(Uri requestUri, string requestMethod, HostNameComparisonMode hostNameComparisonMode, bool isWebSocketRequest, out HttpChannelListener listener)
		{
			listener = null;
			if (isWebSocketRequest)
			{
				requestMethod = "WEBSOCKET";
			}
			if (requestMethod == null)
			{
				requestMethod = string.Empty;
			}
			Dictionary<string, UriPrefixTable<HttpChannelListener>> dictionary = this.addressTables;
			HttpChannelListener httpChannelListener = null;
			UriPrefixTable<HttpChannelListener> uriPrefixTable;
			if (requestMethod.Length > 0 && dictionary.TryGetValue(requestMethod, out uriPrefixTable) && uriPrefixTable.TryLookupUri(requestUri, hostNameComparisonMode, out httpChannelListener) && string.Compare(requestUri.AbsolutePath, httpChannelListener.Uri.AbsolutePath, StringComparison.OrdinalIgnoreCase) != 0)
			{
				httpChannelListener = null;
			}
			if (dictionary.TryGetValue(string.Empty, out uriPrefixTable) && uriPrefixTable.TryLookupUri(requestUri, hostNameComparisonMode, out listener))
			{
				if (httpChannelListener != null && httpChannelListener.Uri.AbsoluteUri.Length >= listener.Uri.AbsoluteUri.Length)
				{
					listener = httpChannelListener;
				}
			}
			else
			{
				listener = httpChannelListener;
			}
			return listener != null;
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x0012D204 File Offset: 0x0012B404
		internal override void Register(TransportChannelListener channelListener)
		{
			string method = ((HttpChannelListener)channelListener).Method;
			UriPrefixTable<HttpChannelListener> uriPrefixTable;
			if (!this.addressTables.TryGetValue(method, out uriPrefixTable))
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (!this.addressTables.TryGetValue(method, out uriPrefixTable))
					{
						Dictionary<string, UriPrefixTable<HttpChannelListener>> dictionary = new Dictionary<string, UriPrefixTable<HttpChannelListener>>(this.addressTables);
						uriPrefixTable = new UriPrefixTable<HttpChannelListener>();
						dictionary[method] = uriPrefixTable;
						this.addressTables = dictionary;
					}
				}
			}
			uriPrefixTable.RegisterUri(channelListener.Uri, channelListener.InheritBaseAddressSettings ? this.hostNameComparisonMode : channelListener.HostNameComparisonModeInternal, (HttpChannelListener)channelListener);
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x0012D2C0 File Offset: 0x0012B4C0
		IList<TransportManager> ITransportManagerRegistration.Select(TransportChannelListener channelListener)
		{
			IList<TransportManager> list = null;
			if (this.IsCompatible((HttpChannelListener)channelListener))
			{
				list = new List<TransportManager>();
				list.Add(this);
			}
			return list;
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x0012D2EC File Offset: 0x0012B4EC
		internal override void Unregister(TransportChannelListener channelListener)
		{
			UriPrefixTable<HttpChannelListener> uriPrefixTable;
			if (!this.addressTables.TryGetValue(((HttpChannelListener)channelListener).Method, out uriPrefixTable))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ListenerFactoryNotRegistered", new object[]
				{
					channelListener.Uri
				})));
			}
			HostNameComparisonMode registeredComparisonMode = channelListener.InheritBaseAddressSettings ? this.hostNameComparisonMode : channelListener.HostNameComparisonModeInternal;
			TransportManager.EnsureRegistered<HttpChannelListener>(uriPrefixTable, (HttpChannelListener)channelListener, registeredComparisonMode);
			uriPrefixTable.UnregisterUri(channelListener.Uri, registeredComparisonMode);
		}

		// Token: 0x04003226 RID: 12838
		private volatile Dictionary<string, UriPrefixTable<HttpChannelListener>> addressTables;

		// Token: 0x04003227 RID: 12839
		private readonly HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x04003228 RID: 12840
		private readonly Uri listenUri;

		// Token: 0x04003229 RID: 12841
		private readonly string realm;

		// Token: 0x02000D56 RID: 3414
		protected class ActivityHolder : IDisposable
		{
			// Token: 0x06007D26 RID: 32038 RVA: 0x001D3F3C File Offset: 0x001D213C
			public ActivityHolder(ServiceModelActivity activity, HttpRequestContext requestContext)
			{
				this.activity = activity;
				this.context = requestContext;
			}

			// Token: 0x06007D27 RID: 32039 RVA: 0x001D3F52 File Offset: 0x001D2152
			public void Dispose()
			{
				if (this.activity != null)
				{
					this.activity.Dispose();
				}
			}

			// Token: 0x040047DC RID: 18396
			internal HttpRequestContext context;

			// Token: 0x040047DD RID: 18397
			internal ServiceModelActivity activity;
		}
	}
}
