using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x02000433 RID: 1075
	public abstract class ServiceHealthBehaviorBase : IServiceBehavior
	{
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000A17DF File Offset: 0x0009F9DF
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x000A17E7 File Offset: 0x0009F9E7
		[DefaultValue(true)]
		public bool HealthDetailsEnabled { get; set; } = true;

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000A17F0 File Offset: 0x0009F9F0
		// (set) Token: 0x060029E0 RID: 10720 RVA: 0x000A17F8 File Offset: 0x0009F9F8
		[DefaultValue(true)]
		public bool HttpGetEnabled { get; set; } = true;

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000A1801 File Offset: 0x0009FA01
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x000A180C File Offset: 0x0009FA0C
		[DefaultValue(null)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri HttpGetUrl
		{
			get
			{
				return this.httpGetUrl;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && value.Scheme != Uri.UriSchemeHttp)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxServiceMetadataBehaviorUrlMustBeHttpOrRelative", new object[]
					{
						"HttpGetUrl",
						Uri.UriSchemeHttp,
						value.ToString(),
						value.Scheme
					}));
				}
				this.httpGetUrl = value;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x000A1880 File Offset: 0x0009FA80
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x000A1888 File Offset: 0x0009FA88
		[DefaultValue(true)]
		public bool HttpsGetEnabled { get; set; } = true;

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x000A1891 File Offset: 0x0009FA91
		// (set) Token: 0x060029E6 RID: 10726 RVA: 0x000A189C File Offset: 0x0009FA9C
		[DefaultValue(null)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri HttpsGetUrl
		{
			get
			{
				return this.httpsGetUrl;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && value.Scheme != Uri.UriSchemeHttps)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxServiceMetadataBehaviorUrlMustBeHttpOrRelative", new object[]
					{
						"HttpsGetUrl",
						Uri.UriSchemeHttps,
						value.ToString(),
						value.Scheme
					}));
				}
				this.httpsGetUrl = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000A1910 File Offset: 0x0009FB10
		// (set) Token: 0x060029E8 RID: 10728 RVA: 0x000A1918 File Offset: 0x0009FB18
		public Binding HttpGetBinding
		{
			get
			{
				return this.httpGetBinding;
			}
			set
			{
				if (value != null)
				{
					if (!value.Scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxBindingSchemeDoesNotMatch", new object[]
						{
							value.Scheme,
							value.GetType().ToString(),
							Uri.UriSchemeHttp
						}));
					}
					CustomBinding customBinding = new CustomBinding(value);
					TextMessageEncodingBindingElement textMessageEncodingBindingElement = customBinding.Elements.Find<TextMessageEncodingBindingElement>();
					if (textMessageEncodingBindingElement != null && !textMessageEncodingBindingElement.MessageVersion.IsMatch(MessageVersion.None))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxIncorrectMessageVersion", new object[]
						{
							textMessageEncodingBindingElement.MessageVersion.ToString(),
							MessageVersion.None.ToString()
						}));
					}
					HttpTransportBindingElement httpTransportBindingElement = customBinding.Elements.Find<HttpTransportBindingElement>();
					if (httpTransportBindingElement != null)
					{
						httpTransportBindingElement.Method = "GET";
					}
					this.httpGetBinding = customBinding;
				}
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x000A19F7 File Offset: 0x0009FBF7
		// (set) Token: 0x060029EA RID: 10730 RVA: 0x000A1A00 File Offset: 0x0009FC00
		public Binding HttpsGetBinding
		{
			get
			{
				return this.httpsGetBinding;
			}
			set
			{
				if (value != null)
				{
					if (!value.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxBindingSchemeDoesNotMatch", new object[]
						{
							value.Scheme,
							value.GetType().ToString(),
							Uri.UriSchemeHttps
						}));
					}
					CustomBinding customBinding = new CustomBinding(value);
					TextMessageEncodingBindingElement textMessageEncodingBindingElement = customBinding.Elements.Find<TextMessageEncodingBindingElement>();
					if (textMessageEncodingBindingElement != null && !textMessageEncodingBindingElement.MessageVersion.IsMatch(MessageVersion.None))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxIncorrectMessageVersion", new object[]
						{
							textMessageEncodingBindingElement.MessageVersion.ToString(),
							MessageVersion.None.ToString()
						}));
					}
					HttpsTransportBindingElement httpsTransportBindingElement = customBinding.Elements.Find<HttpsTransportBindingElement>();
					if (httpsTransportBindingElement != null)
					{
						httpsTransportBindingElement.Method = "GET";
					}
					this.httpsGetBinding = customBinding;
				}
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x000A1ADF File Offset: 0x0009FCDF
		// (set) Token: 0x060029EC RID: 10732 RVA: 0x000A1AE7 File Offset: 0x0009FCE7
		private protected DateTimeOffset ServiceStartTime { protected get; private set; }

		// Token: 0x060029ED RID: 10733 RVA: 0x000A1AF0 File Offset: 0x0009FCF0
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			KeyedByTypeCollection<IServiceBehavior> behaviors = description.Behaviors;
			foreach (IServiceBehavior serviceBehavior in behaviors)
			{
				if (serviceBehavior != this && serviceBehavior is ServiceHealthBehaviorBase)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ServiceHealthBehaviorBase", SR.GetString("DuplicateBehavior1", new object[]
					{
						base.GetType().FullName
					}));
				}
			}
			this.ServiceStartTime = DateTimeOffset.Now;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000A1B80 File Offset: 0x0009FD80
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			bool flag = true;
			foreach (object obj in parameters)
			{
				if (obj is ServiceHealthBehaviorBase)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				parameters.Add(this);
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000A1BF0 File Offset: 0x0009FDF0
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (!this.HttpGetEnabled && !this.HttpsGetEnabled)
			{
				return;
			}
			ServiceMetadataExtension mex = ServiceMetadataExtension.EnsureServiceMetadataExtension(description, serviceHostBase);
			this.CreateHealthEndpoints(description, serviceHostBase, mex);
		}

		// Token: 0x060029F0 RID: 10736
		public abstract void HandleHealthRequest(ServiceHostBase serviceHost, Message httpGetRequest, string[] queries, out Message replyMessage);

		// Token: 0x060029F1 RID: 10737 RVA: 0x000A1C20 File Offset: 0x0009FE20
		private bool EnsureHealthDispatcher(ServiceHostBase host, ServiceMetadataExtension mex, Uri url, string scheme)
		{
			Uri via = host.GetVia(scheme, (url == null) ? new Uri(string.Empty, UriKind.Relative) : url);
			if (via == null)
			{
				return false;
			}
			ChannelDispatcher channelDispatcher = this.EnsureGetDispatcher(host, mex, via);
			((ServiceMetadataExtension.HttpGetImpl)channelDispatcher.Endpoints[0].DispatchRuntime.SingletonInstanceContext.UserObject).HealthBehavior = this;
			return true;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000A1C8C File Offset: 0x0009FE8C
		private ChannelDispatcher EnsureGetDispatcher(ServiceHostBase host, ServiceMetadataExtension mex, Uri listenUri)
		{
			ChannelDispatcher channelDispatcher = mex.FindGetDispatcher(listenUri);
			if (channelDispatcher == null)
			{
				Binding binding;
				if (listenUri.Scheme == Uri.UriSchemeHttp)
				{
					binding = (this.HttpGetBinding ?? MetadataExchangeBindings.HttpGet);
				}
				else
				{
					if (!(listenUri.Scheme == Uri.UriSchemeHttps))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxGetChannelDispatcherDoesNotSupportScheme", new object[]
						{
							"ChannelDispatcher",
							Uri.UriSchemeHttp,
							Uri.UriSchemeHttps
						})));
					}
					binding = (this.HttpsGetBinding ?? MetadataExchangeBindings.HttpsGet);
				}
				channelDispatcher = mex.CreateGetDispatcher(listenUri, binding, "ServiceHealthBehaviorHttpGetBinding");
				host.ChannelDispatchers.Add(channelDispatcher);
			}
			if (host.ServiceThrottle != null)
			{
				channelDispatcher.ServiceThrottle = new ServiceThrottle(host)
				{
					MaxConcurrentCalls = host.ServiceThrottle.Calls.Capacity,
					MaxConcurrentSessions = host.ServiceThrottle.Sessions.Capacity,
					MaxConcurrentInstances = host.ServiceThrottle.InstanceContexts.Capacity
				};
			}
			channelDispatcher.IsServiceThrottleReplaced = true;
			return channelDispatcher;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000A1DA4 File Offset: 0x0009FFA4
		private void CreateHealthEndpoints(ServiceDescription description, ServiceHostBase host, ServiceMetadataExtension mex)
		{
			if (this.HttpGetEnabled && !this.EnsureHealthDispatcher(host, mex, this.httpGetUrl, Uri.UriSchemeHttp))
			{
				ServiceHealthBehaviorBase.TraceWarning(this.httpGetUrl, "ServiceHeathBehaviorHttpHealthUrl", "ServiceHeathBehaviorHttpHealthEnabled");
			}
			if (this.HttpsGetEnabled && !this.EnsureHealthDispatcher(host, mex, this.httpsGetUrl, Uri.UriSchemeHttps))
			{
				ServiceHealthBehaviorBase.TraceWarning(this.httpsGetUrl, "ServiceHeathBehaviorHttpsHealthUrl", "ServiceHeathBehaviorHttpsHealthEnabled");
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000A1E18 File Offset: 0x000A0018
		private static void TraceWarning(Uri address, string urlProperty, string enabledProperty)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Hashtable dictionary = new Hashtable(2)
				{
					{
						enabledProperty,
						"true"
					},
					{
						urlProperty,
						(address == null) ? string.Empty : address.ToString()
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Information, 524382, SR.GetString("TraceCodeWarnServiceHealthPageEnabledNoBaseAddress"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x040022A5 RID: 8869
		private Uri httpGetUrl;

		// Token: 0x040022A6 RID: 8870
		private Uri httpsGetUrl;

		// Token: 0x040022A7 RID: 8871
		private Binding httpGetBinding;

		// Token: 0x040022A8 RID: 8872
		private Binding httpsGetBinding;
	}
}
