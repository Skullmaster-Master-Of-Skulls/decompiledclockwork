using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net.Cache;
using System.Net.Configuration;
using System.Net.Security;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200018A RID: 394
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class WebRequest : MarshalByRefObject, ISerializable
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0004DD7F File Offset: 0x0004BF7F
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IWebRequestCreate CreatorInstance
		{
			get
			{
				return WebRequest.webRequestCreate;
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004DD86 File Offset: 0x0004BF86
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPortableWebRequestCreator(IWebRequestCreate creator)
		{
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0004DD88 File Offset: 0x0004BF88
		private static object InternalSyncObject
		{
			get
			{
				if (WebRequest.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref WebRequest.s_InternalSyncObject, value, null);
				}
				return WebRequest.s_InternalSyncObject;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0004DDB4 File Offset: 0x0004BFB4
		internal static TimerThread.Queue DefaultTimerQueue
		{
			get
			{
				return WebRequest.s_DefaultTimerQueue;
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0004DDBC File Offset: 0x0004BFBC
		private static WebRequest Create(Uri requestUri, bool useUriBase)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "WebRequest", "Create", requestUri.ToString());
			}
			WebRequestPrefixElement webRequestPrefixElement = null;
			bool flag = false;
			string text;
			if (!useUriBase)
			{
				text = requestUri.AbsoluteUri;
			}
			else
			{
				text = requestUri.Scheme + ":";
			}
			int length = text.Length;
			ArrayList prefixList = WebRequest.PrefixList;
			for (int i = 0; i < prefixList.Count; i++)
			{
				webRequestPrefixElement = (WebRequestPrefixElement)prefixList[i];
				if (length >= webRequestPrefixElement.Prefix.Length && string.Compare(webRequestPrefixElement.Prefix, 0, text, 0, webRequestPrefixElement.Prefix.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				WebRequest webRequest = webRequestPrefixElement.Creator.Create(requestUri);
				if (Logging.On)
				{
					Logging.Exit(Logging.Web, "WebRequest", "Create", webRequest);
				}
				return webRequest;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, "WebRequest", "Create", null);
			}
			throw new NotSupportedException(SR.GetString("net_unknown_prefix"));
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0004DECB File Offset: 0x0004C0CB
		[__DynamicallyInvokable]
		public static WebRequest Create(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.Create(new Uri(requestUriString), false);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0004DEE7 File Offset: 0x0004C0E7
		[__DynamicallyInvokable]
		public static WebRequest Create(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.Create(requestUri, false);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0004DF04 File Offset: 0x0004C104
		public static WebRequest CreateDefault(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.Create(requestUri, true);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0004DF21 File Offset: 0x0004C121
		[__DynamicallyInvokable]
		public static HttpWebRequest CreateHttp(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.CreateHttp(new Uri(requestUriString));
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0004DF3C File Offset: 0x0004C13C
		[__DynamicallyInvokable]
		public static HttpWebRequest CreateHttp(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			if (requestUri.Scheme != Uri.UriSchemeHttp && requestUri.Scheme != Uri.UriSchemeHttps)
			{
				throw new NotSupportedException(SR.GetString("net_unknown_prefix"));
			}
			return (HttpWebRequest)WebRequest.CreateDefault(requestUri);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0004DF9C File Offset: 0x0004C19C
		[__DynamicallyInvokable]
		public static bool RegisterPrefix(string prefix, IWebRequestCreate creator)
		{
			bool flag = false;
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			object internalSyncObject = WebRequest.InternalSyncObject;
			lock (internalSyncObject)
			{
				ArrayList arrayList = (ArrayList)WebRequest.PrefixList.Clone();
				Uri uri;
				if (Uri.TryCreate(prefix, UriKind.Absolute, out uri))
				{
					string text = uri.AbsoluteUri;
					if (!prefix.EndsWith("/", StringComparison.Ordinal) && uri.GetComponents(UriComponents.Path | UriComponents.Query | UriComponents.Fragment, UriFormat.UriEscaped).Equals("/"))
					{
						text = text.Substring(0, text.Length - 1);
					}
					prefix = text;
				}
				int i;
				for (i = 0; i < arrayList.Count; i++)
				{
					WebRequestPrefixElement webRequestPrefixElement = (WebRequestPrefixElement)arrayList[i];
					if (prefix.Length > webRequestPrefixElement.Prefix.Length)
					{
						break;
					}
					if (prefix.Length == webRequestPrefixElement.Prefix.Length && string.Compare(webRequestPrefixElement.Prefix, prefix, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList.Insert(i, new WebRequestPrefixElement(prefix, creator));
					WebRequest.PrefixList = arrayList;
				}
			}
			return !flag;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0004E0D8 File Offset: 0x0004C2D8
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0004E13C File Offset: 0x0004C33C
		internal static ArrayList PrefixList
		{
			get
			{
				if (WebRequest.s_PrefixList == null)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (WebRequest.s_PrefixList == null)
						{
							WebRequest.s_PrefixList = WebRequestModulesSectionInternal.GetSection().WebRequestModules;
						}
					}
				}
				return WebRequest.s_PrefixList;
			}
			set
			{
				WebRequest.s_PrefixList = value;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0004E146 File Offset: 0x0004C346
		[__DynamicallyInvokable]
		protected WebRequest()
		{
			this.m_ImpersonationLevel = TokenImpersonationLevel.Delegation;
			this.m_AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0004E15C File Offset: 0x0004C35C
		protected WebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0004E164 File Offset: 0x0004C364
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0004E16E File Offset: 0x0004C36E
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0004E170 File Offset: 0x0004C370
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0004E184 File Offset: 0x0004C384
		public static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return RequestCacheManager.GetBinding(string.Empty).Policy;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				RequestCacheBinding binding = RequestCacheManager.GetBinding(string.Empty);
				RequestCacheManager.SetBinding(string.Empty, new RequestCacheBinding(binding.Cache, binding.Validator, value));
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0004E1C2 File Offset: 0x0004C3C2
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0004E1CA File Offset: 0x0004C3CA
		public virtual RequestCachePolicy CachePolicy
		{
			get
			{
				return this.m_CachePolicy;
			}
			set
			{
				this.InternalSetCachePolicy(value);
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0004E1D4 File Offset: 0x0004C3D4
		private void InternalSetCachePolicy(RequestCachePolicy policy)
		{
			if (this.m_CacheBinding != null && this.m_CacheBinding.Cache != null && this.m_CacheBinding.Validator != null && this.CacheProtocol == null && policy != null && policy.Level != RequestCacheLevel.BypassCache)
			{
				this.CacheProtocol = new RequestCacheProtocol(this.m_CacheBinding.Cache, this.m_CacheBinding.Validator.CreateValidator());
			}
			this.m_CachePolicy = policy;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0004E244 File Offset: 0x0004C444
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x0004E24B File Offset: 0x0004C44B
		[__DynamicallyInvokable]
		public virtual string Method
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0004E252 File Offset: 0x0004C452
		[__DynamicallyInvokable]
		public virtual Uri RequestUri
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0004E259 File Offset: 0x0004C459
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x0004E260 File Offset: 0x0004C460
		public virtual string ConnectionGroupName
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0004E267 File Offset: 0x0004C467
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0004E26E File Offset: 0x0004C46E
		[__DynamicallyInvokable]
		public virtual WebHeaderCollection Headers
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0004E275 File Offset: 0x0004C475
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0004E27C File Offset: 0x0004C47C
		public virtual long ContentLength
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0004E283 File Offset: 0x0004C483
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0004E28A File Offset: 0x0004C48A
		[__DynamicallyInvokable]
		public virtual string ContentType
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0004E291 File Offset: 0x0004C491
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x0004E298 File Offset: 0x0004C498
		[__DynamicallyInvokable]
		public virtual ICredentials Credentials
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0004E29F File Offset: 0x0004C49F
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x0004E2A6 File Offset: 0x0004C4A6
		[__DynamicallyInvokable]
		public virtual bool UseDefaultCredentials
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0004E2AD File Offset: 0x0004C4AD
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
		[__DynamicallyInvokable]
		public virtual IWebProxy Proxy
		{
			[__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			[__DynamicallyInvokable]
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0004E2BB File Offset: 0x0004C4BB
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0004E2C2 File Offset: 0x0004C4C2
		public virtual bool PreAuthenticate
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0004E2C9 File Offset: 0x0004C4C9
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0004E2D0 File Offset: 0x0004C4D0
		public virtual int Timeout
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0004E2D7 File Offset: 0x0004C4D7
		public virtual Stream GetRequestStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0004E2DE File Offset: 0x0004C4DE
		public virtual WebResponse GetResponse()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0004E2E5 File Offset: 0x0004C4E5
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0004E2EC File Offset: 0x0004C4EC
		[__DynamicallyInvokable]
		public virtual WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0004E2FA File Offset: 0x0004C4FA
		[__DynamicallyInvokable]
		public virtual Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0004E304 File Offset: 0x0004C504
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<Stream> GetRequestStreamAsync()
		{
			IWebProxy webProxy = null;
			try
			{
				webProxy = this.Proxy;
			}
			catch (NotImplementedException)
			{
			}
			if (ExecutionContext.IsFlowSuppressed() && (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null)))
			{
				WindowsIdentity currentUser = this.SafeCaptureIdenity();
				return Task.Run<Stream>(delegate()
				{
					Task<Stream> result;
					using (WindowsIdentity currentUser = currentUser)
					{
						using (currentUser.Impersonate())
						{
							result = Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null);
						}
					}
					return result;
				});
			}
			return Task.Run<Stream>(() => Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null));
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0004E390 File Offset: 0x0004C590
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<WebResponse> GetResponseAsync()
		{
			IWebProxy webProxy = null;
			try
			{
				webProxy = this.Proxy;
			}
			catch (NotImplementedException)
			{
			}
			if (ExecutionContext.IsFlowSuppressed() && (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null)))
			{
				WindowsIdentity currentUser = this.SafeCaptureIdenity();
				return Task.Run<WebResponse>(delegate()
				{
					Task<WebResponse> result;
					using (WindowsIdentity currentUser = currentUser)
					{
						using (currentUser.Impersonate())
						{
							result = Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null);
						}
					}
					return result;
				});
			}
			return Task.Run<WebResponse>(() => Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null));
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0004E41C File Offset: 0x0004C61C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private WindowsIdentity SafeCaptureIdenity()
		{
			return WindowsIdentity.GetCurrent();
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004E423 File Offset: 0x0004C623
		[__DynamicallyInvokable]
		public virtual void Abort()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0004E42A File Offset: 0x0004C62A
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0004E432 File Offset: 0x0004C632
		internal RequestCacheProtocol CacheProtocol
		{
			get
			{
				return this.m_CacheProtocol;
			}
			set
			{
				this.m_CacheProtocol = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0004E43B File Offset: 0x0004C63B
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0004E443 File Offset: 0x0004C643
		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.m_AuthenticationLevel;
			}
			set
			{
				this.m_AuthenticationLevel = value;
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0004E44C File Offset: 0x0004C64C
		internal virtual ContextAwareResult GetConnectingContext()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0004E453 File Offset: 0x0004C653
		internal virtual ContextAwareResult GetWritingContext()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0004E45A File Offset: 0x0004C65A
		internal virtual ContextAwareResult GetReadingContext()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0004E461 File Offset: 0x0004C661
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x0004E469 File Offset: 0x0004C669
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.m_ImpersonationLevel;
			}
			set
			{
				this.m_ImpersonationLevel = value;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0004E472 File Offset: 0x0004C672
		internal virtual void RequestCallback(object obj)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0004E47C File Offset: 0x0004C67C
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0004E4EC File Offset: 0x0004C6EC
		internal static IWebProxy InternalDefaultWebProxy
		{
			get
			{
				if (!WebRequest.s_DefaultWebProxyInitialized)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (!WebRequest.s_DefaultWebProxyInitialized)
						{
							DefaultProxySectionInternal section = DefaultProxySectionInternal.GetSection();
							if (section != null)
							{
								WebRequest.s_DefaultWebProxy = section.WebProxy;
							}
							WebRequest.s_DefaultWebProxyInitialized = true;
						}
					}
				}
				return WebRequest.s_DefaultWebProxy;
			}
			set
			{
				if (!WebRequest.s_DefaultWebProxyInitialized)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						WebRequest.s_DefaultWebProxy = value;
						WebRequest.s_DefaultWebProxyInitialized = true;
						return;
					}
				}
				WebRequest.s_DefaultWebProxy = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0004E548 File Offset: 0x0004C748
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x0004E559 File Offset: 0x0004C759
		[__DynamicallyInvokable]
		public static IWebProxy DefaultWebProxy
		{
			[__DynamicallyInvokable]
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return WebRequest.InternalDefaultWebProxy;
			}
			[__DynamicallyInvokable]
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				WebRequest.InternalDefaultWebProxy = value;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0004E56B File Offset: 0x0004C76B
		public static IWebProxy GetSystemWebProxy()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			return WebRequest.InternalGetSystemWebProxy();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0004E57C File Offset: 0x0004C77C
		internal static IWebProxy InternalGetSystemWebProxy()
		{
			return new WebRequest.WebProxyWrapperOpaque(new WebProxy(true));
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0004E589 File Offset: 0x0004C789
		internal void SetupCacheProtocol(Uri uri)
		{
			this.m_CacheBinding = RequestCacheManager.GetBinding(uri.Scheme);
			this.InternalSetCachePolicy(this.m_CacheBinding.Policy);
			if (this.m_CachePolicy == null)
			{
				this.InternalSetCachePolicy(WebRequest.DefaultCachePolicy);
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0004E5C0 File Offset: 0x0004C7C0
		private static void InitEtwMethods()
		{
			Type typeFromHandle = typeof(FrameworkEventSource);
			Type[] types = new Type[]
			{
				typeof(object),
				typeof(string),
				typeof(bool),
				typeof(bool)
			};
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo method = typeFromHandle.GetMethod("BeginGetResponse", bindingAttr, null, types, null);
			MethodInfo method2 = typeFromHandle.GetMethod("EndGetResponse", bindingAttr, null, new Type[]
			{
				typeof(object),
				typeof(bool),
				typeof(bool),
				typeof(int)
			}, null);
			MethodInfo method3 = typeFromHandle.GetMethod("BeginGetRequestStream", bindingAttr, null, types, null);
			MethodInfo method4 = typeFromHandle.GetMethod("EndGetRequestStream", bindingAttr, null, new Type[]
			{
				typeof(object),
				typeof(bool),
				typeof(bool)
			}, null);
			if (method != null && method2 != null && method3 != null && method4 != null)
			{
				WebRequest.s_EtwFireBeginGetResponse = (WebRequest.DelEtwFireBeginWRGet)method.CreateDelegate(typeof(WebRequest.DelEtwFireBeginWRGet), FrameworkEventSource.Log);
				WebRequest.s_EtwFireEndGetResponse = (WebRequest.DelEtwFireEndWRespGet)method2.CreateDelegate(typeof(WebRequest.DelEtwFireEndWRespGet), FrameworkEventSource.Log);
				WebRequest.s_EtwFireBeginGetRequestStream = (WebRequest.DelEtwFireBeginWRGet)method3.CreateDelegate(typeof(WebRequest.DelEtwFireBeginWRGet), FrameworkEventSource.Log);
				WebRequest.s_EtwFireEndGetRequestStream = (WebRequest.DelEtwFireEndWRGet)method4.CreateDelegate(typeof(WebRequest.DelEtwFireEndWRGet), FrameworkEventSource.Log);
			}
			WebRequest.s_TriedGetEtwDelegates = true;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0004E778 File Offset: 0x0004C978
		internal void LogBeginGetResponse(bool success, bool synchronous)
		{
			string originalString = this.RequestUri.OriginalString;
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireBeginGetResponse != null)
			{
				WebRequest.s_EtwFireBeginGetResponse(this, originalString, success, synchronous);
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0004E7B4 File Offset: 0x0004C9B4
		internal void LogEndGetResponse(bool success, bool synchronous, int statusCode)
		{
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireEndGetResponse != null)
			{
				WebRequest.s_EtwFireEndGetResponse(this, success, synchronous, statusCode);
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0004E7DC File Offset: 0x0004C9DC
		internal void LogBeginGetRequestStream(bool success, bool synchronous)
		{
			string originalString = this.RequestUri.OriginalString;
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireBeginGetRequestStream != null)
			{
				WebRequest.s_EtwFireBeginGetRequestStream(this, originalString, success, synchronous);
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0004E818 File Offset: 0x0004CA18
		internal void LogEndGetRequestStream(bool success, bool synchronous)
		{
			if (!WebRequest.s_TriedGetEtwDelegates)
			{
				WebRequest.InitEtwMethods();
			}
			if (WebRequest.s_EtwFireEndGetRequestStream != null)
			{
				WebRequest.s_EtwFireEndGetRequestStream(this, success, synchronous);
			}
		}

		// Token: 0x04001293 RID: 4755
		internal const int DefaultTimeout = 100000;

		// Token: 0x04001294 RID: 4756
		private static volatile ArrayList s_PrefixList;

		// Token: 0x04001295 RID: 4757
		private static object s_InternalSyncObject;

		// Token: 0x04001296 RID: 4758
		private static TimerThread.Queue s_DefaultTimerQueue = TimerThread.CreateQueue(100000);

		// Token: 0x04001297 RID: 4759
		private AuthenticationLevel m_AuthenticationLevel;

		// Token: 0x04001298 RID: 4760
		private TokenImpersonationLevel m_ImpersonationLevel;

		// Token: 0x04001299 RID: 4761
		private RequestCachePolicy m_CachePolicy;

		// Token: 0x0400129A RID: 4762
		private RequestCacheProtocol m_CacheProtocol;

		// Token: 0x0400129B RID: 4763
		private RequestCacheBinding m_CacheBinding;

		// Token: 0x0400129C RID: 4764
		private static WebRequest.DesignerWebRequestCreate webRequestCreate = new WebRequest.DesignerWebRequestCreate();

		// Token: 0x0400129D RID: 4765
		private static volatile IWebProxy s_DefaultWebProxy;

		// Token: 0x0400129E RID: 4766
		private static volatile bool s_DefaultWebProxyInitialized;

		// Token: 0x0400129F RID: 4767
		private static WebRequest.DelEtwFireBeginWRGet s_EtwFireBeginGetResponse;

		// Token: 0x040012A0 RID: 4768
		private static WebRequest.DelEtwFireEndWRespGet s_EtwFireEndGetResponse;

		// Token: 0x040012A1 RID: 4769
		private static WebRequest.DelEtwFireBeginWRGet s_EtwFireBeginGetRequestStream;

		// Token: 0x040012A2 RID: 4770
		private static WebRequest.DelEtwFireEndWRGet s_EtwFireEndGetRequestStream;

		// Token: 0x040012A3 RID: 4771
		private static volatile bool s_TriedGetEtwDelegates;

		// Token: 0x02000735 RID: 1845
		internal class DesignerWebRequestCreate : IWebRequestCreate
		{
			// Token: 0x060041B0 RID: 16816 RVA: 0x00111110 File Offset: 0x0010F310
			public WebRequest Create(Uri uri)
			{
				return WebRequest.Create(uri);
			}
		}

		// Token: 0x02000736 RID: 1846
		internal class WebProxyWrapperOpaque : IAutoWebProxy, IWebProxy
		{
			// Token: 0x060041B2 RID: 16818 RVA: 0x00111120 File Offset: 0x0010F320
			internal WebProxyWrapperOpaque(WebProxy webProxy)
			{
				this.webProxy = webProxy;
			}

			// Token: 0x060041B3 RID: 16819 RVA: 0x0011112F File Offset: 0x0010F32F
			public Uri GetProxy(Uri destination)
			{
				return this.webProxy.GetProxy(destination);
			}

			// Token: 0x060041B4 RID: 16820 RVA: 0x0011113D File Offset: 0x0010F33D
			public bool IsBypassed(Uri host)
			{
				return this.webProxy.IsBypassed(host);
			}

			// Token: 0x17000F07 RID: 3847
			// (get) Token: 0x060041B5 RID: 16821 RVA: 0x0011114B File Offset: 0x0010F34B
			// (set) Token: 0x060041B6 RID: 16822 RVA: 0x00111158 File Offset: 0x0010F358
			public ICredentials Credentials
			{
				get
				{
					return this.webProxy.Credentials;
				}
				set
				{
					this.webProxy.Credentials = value;
				}
			}

			// Token: 0x060041B7 RID: 16823 RVA: 0x00111166 File Offset: 0x0010F366
			public ProxyChain GetProxies(Uri destination)
			{
				return ((IAutoWebProxy)this.webProxy).GetProxies(destination);
			}

			// Token: 0x040031B8 RID: 12728
			protected readonly WebProxy webProxy;
		}

		// Token: 0x02000737 RID: 1847
		internal class WebProxyWrapper : WebRequest.WebProxyWrapperOpaque
		{
			// Token: 0x060041B8 RID: 16824 RVA: 0x00111174 File Offset: 0x0010F374
			internal WebProxyWrapper(WebProxy webProxy) : base(webProxy)
			{
			}

			// Token: 0x17000F08 RID: 3848
			// (get) Token: 0x060041B9 RID: 16825 RVA: 0x0011117D File Offset: 0x0010F37D
			internal WebProxy WebProxy
			{
				get
				{
					return this.webProxy;
				}
			}
		}

		// Token: 0x02000738 RID: 1848
		// (Invoke) Token: 0x060041BB RID: 16827
		private delegate void DelEtwFireBeginWRGet(object id, string uri, bool success, bool synchronous);

		// Token: 0x02000739 RID: 1849
		// (Invoke) Token: 0x060041BF RID: 16831
		private delegate void DelEtwFireEndWRGet(object id, bool success, bool synchronous);

		// Token: 0x0200073A RID: 1850
		// (Invoke) Token: 0x060041C3 RID: 16835
		private delegate void DelEtwFireEndWRespGet(object id, bool success, bool synchronous, int statusCode);
	}
}
