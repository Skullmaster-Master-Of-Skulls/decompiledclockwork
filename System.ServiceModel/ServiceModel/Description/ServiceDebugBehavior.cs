using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x02000432 RID: 1074
	public class ServiceDebugBehavior : IServiceBehavior
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000A123F File Offset: 0x0009F43F
		// (set) Token: 0x060029C8 RID: 10696 RVA: 0x000A1247 File Offset: 0x0009F447
		[DefaultValue(true)]
		public bool HttpHelpPageEnabled
		{
			get
			{
				return this.httpHelpPageEnabled;
			}
			set
			{
				this.httpHelpPageEnabled = value;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x000A1250 File Offset: 0x0009F450
		// (set) Token: 0x060029CA RID: 10698 RVA: 0x000A1258 File Offset: 0x0009F458
		[DefaultValue(null)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri HttpHelpPageUrl
		{
			get
			{
				return this.httpHelpPageUrl;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && value.Scheme != Uri.UriSchemeHttp)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxServiceMetadataBehaviorUrlMustBeHttpOrRelative", new object[]
					{
						"HttpHelpPageUrl",
						Uri.UriSchemeHttp,
						value.ToString(),
						value.Scheme
					}));
				}
				this.httpHelpPageUrl = value;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x000A12CC File Offset: 0x0009F4CC
		// (set) Token: 0x060029CC RID: 10700 RVA: 0x000A12D4 File Offset: 0x0009F4D4
		[DefaultValue(true)]
		public bool HttpsHelpPageEnabled
		{
			get
			{
				return this.httpsHelpPageEnabled;
			}
			set
			{
				this.httpsHelpPageEnabled = value;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000A12DD File Offset: 0x0009F4DD
		// (set) Token: 0x060029CE RID: 10702 RVA: 0x000A12E8 File Offset: 0x0009F4E8
		[DefaultValue(null)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri HttpsHelpPageUrl
		{
			get
			{
				return this.httpsHelpPageUrl;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && value.Scheme != Uri.UriSchemeHttps)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxServiceMetadataBehaviorUrlMustBeHttpOrRelative", new object[]
					{
						"HttpsHelpPageUrl",
						Uri.UriSchemeHttps,
						value.ToString(),
						value.Scheme
					}));
				}
				this.httpsHelpPageUrl = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000A135C File Offset: 0x0009F55C
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000A1364 File Offset: 0x0009F564
		public Binding HttpHelpPageBinding
		{
			get
			{
				return this.httpHelpPageBinding;
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
					this.httpHelpPageBinding = customBinding;
				}
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x000A1443 File Offset: 0x0009F643
		// (set) Token: 0x060029D2 RID: 10706 RVA: 0x000A144C File Offset: 0x0009F64C
		public Binding HttpsHelpPageBinding
		{
			get
			{
				return this.httpsHelpPageBinding;
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
					this.httpsHelpPageBinding = customBinding;
				}
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000A152B File Offset: 0x0009F72B
		// (set) Token: 0x060029D4 RID: 10708 RVA: 0x000A1533 File Offset: 0x0009F733
		[DefaultValue(false)]
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this.includeExceptionDetailInFaults;
			}
			set
			{
				this.includeExceptionDetailInFaults = value;
			}
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000A153C File Offset: 0x0009F73C
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000A1540 File Offset: 0x0009F740
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			if (parameters.Find<ServiceDebugBehavior>() == null)
			{
				parameters.Add(this);
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000A1574 File Offset: 0x0009F774
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (this.includeExceptionDetailInFaults)
			{
				for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
				{
					ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
					if (channelDispatcher != null)
					{
						channelDispatcher.IncludeExceptionDetailInFaults = true;
					}
				}
			}
			if (!this.httpHelpPageEnabled && !this.httpsHelpPageEnabled)
			{
				return;
			}
			ServiceMetadataExtension mex = ServiceMetadataExtension.EnsureServiceMetadataExtension(description, serviceHostBase);
			this.SetExtensionProperties(mex, serviceHostBase);
			this.CreateHelpPageEndpoints(description, serviceHostBase, mex);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000A15E8 File Offset: 0x0009F7E8
		private void SetExtensionProperties(ServiceMetadataExtension mex, ServiceHostBase host)
		{
			mex.HttpHelpPageEnabled = this.httpHelpPageEnabled;
			mex.HttpHelpPageUrl = host.GetVia(Uri.UriSchemeHttp, (this.httpHelpPageUrl == null) ? new Uri(string.Empty, UriKind.Relative) : this.httpHelpPageUrl);
			mex.HttpHelpPageBinding = this.HttpHelpPageBinding;
			mex.HttpsHelpPageEnabled = this.httpsHelpPageEnabled;
			mex.HttpsHelpPageUrl = host.GetVia(Uri.UriSchemeHttps, (this.httpsHelpPageUrl == null) ? new Uri(string.Empty, UriKind.Relative) : this.httpsHelpPageUrl);
			mex.HttpsHelpPageBinding = this.HttpsHelpPageBinding;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000A168C File Offset: 0x0009F88C
		private bool EnsureHelpPageDispatcher(ServiceHostBase host, ServiceMetadataExtension mex, Uri url, string scheme)
		{
			Uri via = host.GetVia(scheme, (url == null) ? new Uri(string.Empty, UriKind.Relative) : url);
			if (via == null)
			{
				return false;
			}
			ChannelDispatcher channelDispatcher = mex.EnsureGetDispatcher(via, true);
			((ServiceMetadataExtension.HttpGetImpl)channelDispatcher.Endpoints[0].DispatchRuntime.SingletonInstanceContext.UserObject).HelpPageEnabled = true;
			return true;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000A16F4 File Offset: 0x0009F8F4
		private void CreateHelpPageEndpoints(ServiceDescription description, ServiceHostBase host, ServiceMetadataExtension mex)
		{
			if (this.httpHelpPageEnabled && !this.EnsureHelpPageDispatcher(host, mex, this.httpHelpPageUrl, Uri.UriSchemeHttp))
			{
				ServiceDebugBehavior.TraceWarning(this.httpHelpPageUrl, "ServiceDebugBehaviorHttpHelpPageUrl", "ServiceDebugBehaviorHttpHelpPageEnabled");
			}
			if (this.httpsHelpPageEnabled && !this.EnsureHelpPageDispatcher(host, mex, this.httpsHelpPageUrl, Uri.UriSchemeHttps))
			{
				ServiceDebugBehavior.TraceWarning(this.httpHelpPageUrl, "ServiceDebugBehaviorHttpsHelpPageUrl", "ServiceDebugBehaviorHttpsHelpPageEnabled");
			}
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000A1768 File Offset: 0x0009F968
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
				TraceUtility.TraceEvent(TraceEventType.Information, 524381, SR.GetString("TraceCodeWarnHelpPageEnabledNoBaseAddress"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x0400229E RID: 8862
		private bool includeExceptionDetailInFaults;

		// Token: 0x0400229F RID: 8863
		private bool httpHelpPageEnabled = true;

		// Token: 0x040022A0 RID: 8864
		private Uri httpHelpPageUrl;

		// Token: 0x040022A1 RID: 8865
		private bool httpsHelpPageEnabled = true;

		// Token: 0x040022A2 RID: 8866
		private Uri httpsHelpPageUrl;

		// Token: 0x040022A3 RID: 8867
		private Binding httpHelpPageBinding;

		// Token: 0x040022A4 RID: 8868
		private Binding httpsHelpPageBinding;
	}
}
