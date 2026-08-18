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
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x0200043B RID: 1083
	public class ServiceMetadataBehavior : IServiceBehavior
	{
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000A32CB File Offset: 0x000A14CB
		// (set) Token: 0x06002A38 RID: 10808 RVA: 0x000A32D3 File Offset: 0x000A14D3
		public bool HttpGetEnabled
		{
			get
			{
				return this.httpGetEnabled;
			}
			set
			{
				this.httpGetEnabled = value;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x000A32DC File Offset: 0x000A14DC
		// (set) Token: 0x06002A3A RID: 10810 RVA: 0x000A32E4 File Offset: 0x000A14E4
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

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x000A3358 File Offset: 0x000A1558
		// (set) Token: 0x06002A3C RID: 10812 RVA: 0x000A3360 File Offset: 0x000A1560
		public bool HttpsGetEnabled
		{
			get
			{
				return this.httpsGetEnabled;
			}
			set
			{
				this.httpsGetEnabled = value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x000A3369 File Offset: 0x000A1569
		// (set) Token: 0x06002A3E RID: 10814 RVA: 0x000A3374 File Offset: 0x000A1574
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

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000A33E8 File Offset: 0x000A15E8
		// (set) Token: 0x06002A40 RID: 10816 RVA: 0x000A33F0 File Offset: 0x000A15F0
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000A34CF File Offset: 0x000A16CF
		// (set) Token: 0x06002A42 RID: 10818 RVA: 0x000A34D8 File Offset: 0x000A16D8
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

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000A35B7 File Offset: 0x000A17B7
		// (set) Token: 0x06002A44 RID: 10820 RVA: 0x000A35C0 File Offset: 0x000A17C0
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri ExternalMetadataLocation
		{
			get
			{
				return this.externalMetadataLocation;
			}
			set
			{
				if (value != null && value.IsAbsoluteUri && !(value.Scheme == Uri.UriSchemeHttp) && !(value.Scheme == Uri.UriSchemeHttps))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ExternalMetadataLocation", SR.GetString("SFxBadMetadataLocationUri", new object[]
					{
						value.OriginalString,
						value.Scheme
					}));
				}
				this.externalMetadataLocation = value;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000A363B File Offset: 0x000A183B
		// (set) Token: 0x06002A46 RID: 10822 RVA: 0x000A3656 File Offset: 0x000A1856
		public MetadataExporter MetadataExporter
		{
			get
			{
				if (this.metadataExporter == null)
				{
					this.metadataExporter = new WsdlExporter();
				}
				return this.metadataExporter;
			}
			set
			{
				this.metadataExporter = value;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000A365F File Offset: 0x000A185F
		internal static ContractDescription MexContract
		{
			get
			{
				ServiceMetadataBehavior.EnsureMexContractDescription();
				return ServiceMetadataBehavior.mexContract;
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000A366B File Offset: 0x000A186B
		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000A366D File Offset: 0x000A186D
		void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000A366F File Offset: 0x000A186F
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (serviceHostBase == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceHostBase");
			}
			this.ApplyBehavior(description, serviceHostBase);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000A36A0 File Offset: 0x000A18A0
		private void ApplyBehavior(ServiceDescription description, ServiceHostBase host)
		{
			ServiceMetadataExtension mex = ServiceMetadataExtension.EnsureServiceMetadataExtension(description, host);
			this.SetExtensionProperties(description, host, mex);
			ServiceMetadataBehavior.CustomizeMetadataEndpoints(description, host, mex);
			this.CreateHttpGetEndpoints(description, host, mex);
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000A36D0 File Offset: 0x000A18D0
		private void CreateHttpGetEndpoints(ServiceDescription description, ServiceHostBase host, ServiceMetadataExtension mex)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.httpGetEnabled)
			{
				flag = ServiceMetadataBehavior.EnsureGetDispatcher(host, mex, this.httpGetUrl, Uri.UriSchemeHttp);
			}
			if (this.httpsGetEnabled)
			{
				flag2 = ServiceMetadataBehavior.EnsureGetDispatcher(host, mex, this.httpsGetUrl, Uri.UriSchemeHttps);
			}
			if (!flag && !flag2)
			{
				if (this.httpGetEnabled)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceMetadataBehaviorNoHttpBaseAddress")));
				}
				if (this.httpsGetEnabled)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceMetadataBehaviorNoHttpsBaseAddress")));
				}
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000A3764 File Offset: 0x000A1964
		private static bool EnsureGetDispatcher(ServiceHostBase host, ServiceMetadataExtension mex, Uri url, string scheme)
		{
			Uri via = host.GetVia(scheme, (url == null) ? new Uri(string.Empty, UriKind.Relative) : url);
			if (via != null)
			{
				ChannelDispatcher channelDispatcher = mex.EnsureGetDispatcher(via, false);
				((ServiceMetadataExtension.HttpGetImpl)channelDispatcher.Endpoints[0].DispatchRuntime.SingletonInstanceContext.UserObject).GetWsdlEnabled = true;
				return true;
			}
			return false;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000A37CC File Offset: 0x000A19CC
		private void SetExtensionProperties(ServiceDescription description, ServiceHostBase host, ServiceMetadataExtension mex)
		{
			mex.ExternalMetadataLocation = this.ExternalMetadataLocation;
			mex.Initializer = new ServiceMetadataBehavior.MetadataExtensionInitializer(this, description, host);
			mex.HttpGetEnabled = this.httpGetEnabled;
			mex.HttpsGetEnabled = this.httpsGetEnabled;
			mex.HttpGetUrl = host.GetVia(Uri.UriSchemeHttp, (this.httpGetUrl == null) ? new Uri(string.Empty, UriKind.Relative) : this.httpGetUrl);
			mex.HttpsGetUrl = host.GetVia(Uri.UriSchemeHttps, (this.httpsGetUrl == null) ? new Uri(string.Empty, UriKind.Relative) : this.httpsGetUrl);
			mex.HttpGetBinding = this.httpGetBinding;
			mex.HttpsGetBinding = this.httpsGetBinding;
			UseRequestHeadersForMetadataAddressBehavior useRequestHeadersForMetadataAddressBehavior = description.Behaviors.Find<UseRequestHeadersForMetadataAddressBehavior>();
			if (useRequestHeadersForMetadataAddressBehavior != null)
			{
				mex.UpdateAddressDynamically = true;
				mex.UpdatePortsByScheme = new Dictionary<string, int>(useRequestHeadersForMetadataAddressBehavior.DefaultPortsByScheme);
			}
			foreach (ChannelDispatcherBase channelDispatcherBase in host.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
				if (channelDispatcher != null && ServiceMetadataBehavior.IsMetadataTransferDispatcher(description, channelDispatcher))
				{
					mex.MexEnabled = true;
					mex.MexUrl = channelDispatcher.Listener.Uri;
					if (useRequestHeadersForMetadataAddressBehavior == null)
					{
						break;
					}
					using (IEnumerator<EndpointDispatcher> enumerator2 = channelDispatcher.Endpoints.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EndpointDispatcher endpointDispatcher = enumerator2.Current;
							if (!endpointDispatcher.AddressFilterSetExplicit)
							{
								endpointDispatcher.AddressFilter = new MatchAllMessageFilter();
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000A3968 File Offset: 0x000A1B68
		private static void CustomizeMetadataEndpoints(ServiceDescription description, ServiceHostBase host, ServiceMetadataExtension mex)
		{
			for (int i = 0; i < host.ChannelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = host.ChannelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null && ServiceMetadataBehavior.IsMetadataTransferDispatcher(description, channelDispatcher))
				{
					if (channelDispatcher.Endpoints.Count != 1)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceMetadataBehaviorInstancingError", new object[]
						{
							channelDispatcher.Listener.Uri,
							channelDispatcher.CreateContractListString()
						})));
					}
					DispatchRuntime dispatchRuntime = channelDispatcher.Endpoints[0].DispatchRuntime;
					dispatchRuntime.InstanceContextProvider = InstanceContextProviderBase.GetProviderForMode(InstanceContextMode.Single, dispatchRuntime);
					bool isListeningOnHttps = channelDispatcher.Listener.Uri.Scheme == Uri.UriSchemeHttps;
					Uri uri = channelDispatcher.Listener.Uri;
					ServiceMetadataExtension.WSMexImpl implementation = new ServiceMetadataExtension.WSMexImpl(mex, isListeningOnHttps, uri);
					dispatchRuntime.SingletonInstanceContext = new InstanceContext(host, implementation, false);
				}
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000A3A5C File Offset: 0x000A1C5C
		private static EndpointDispatcher GetListenerByID(SynchronizedCollection<ChannelDispatcherBase> channelDispatchers, string id)
		{
			for (int i = 0; i < channelDispatchers.Count; i++)
			{
				ChannelDispatcher channelDispatcher = channelDispatchers[i] as ChannelDispatcher;
				if (channelDispatcher != null)
				{
					for (int j = 0; j < channelDispatcher.Endpoints.Count; j++)
					{
						EndpointDispatcher endpointDispatcher = channelDispatcher.Endpoints[j];
						if (endpointDispatcher.Id == id)
						{
							return endpointDispatcher;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
		internal static bool IsMetadataDispatcher(ServiceDescription description, ChannelDispatcher channelDispatcher)
		{
			foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
			{
				if (ServiceMetadataBehavior.IsMetadataTransferDispatcher(description, channelDispatcher) || ServiceMetadataBehavior.IsHttpGetMetadataDispatcher(description, channelDispatcher))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000A3B20 File Offset: 0x000A1D20
		private static bool IsMetadataTransferDispatcher(ServiceDescription description, ChannelDispatcher channelDispatcher)
		{
			if (ServiceMetadataBehavior.BehaviorMissingObjectNullOrServiceImplements(description, channelDispatcher))
			{
				return false;
			}
			foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
			{
				if (endpointDispatcher.ContractName == "IMetadataExchange" && endpointDispatcher.ContractNamespace == "http://schemas.microsoft.com/2006/04/mex")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000A3B9C File Offset: 0x000A1D9C
		private static bool BehaviorMissingObjectNullOrServiceImplements(ServiceDescription description, object obj)
		{
			return obj == null || (description.Behaviors != null && description.Behaviors.Find<ServiceMetadataBehavior>() == null) || (description.ServiceType != null && description.ServiceType.GetInterface(typeof(IMetadataExchange).Name) != null);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000A3BF8 File Offset: 0x000A1DF8
		internal static bool IsHttpGetMetadataDispatcher(ServiceDescription description, ChannelDispatcher channelDispatcher)
		{
			if (description.Behaviors.Find<ServiceMetadataBehavior>() == null)
			{
				return false;
			}
			foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
			{
				if (endpointDispatcher.ContractName == "IHttpGetHelpPageAndMetadataContract" && endpointDispatcher.ContractNamespace == "http://schemas.microsoft.com/2006/04/http/metadata")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000A3C78 File Offset: 0x000A1E78
		internal static bool IsMetadataEndpoint(ServiceDescription description, ServiceEndpoint endpoint)
		{
			return !ServiceMetadataBehavior.BehaviorMissingObjectNullOrServiceImplements(description, endpoint) && ServiceMetadataBehavior.IsMetadataEndpoint(endpoint);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000A3C8B File Offset: 0x000A1E8B
		private static bool IsMetadataEndpoint(ServiceEndpoint endpoint)
		{
			return endpoint.Contract.Name == "IMetadataExchange" && endpoint.Contract.Namespace == "http://schemas.microsoft.com/2006/04/mex";
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000A3CBB File Offset: 0x000A1EBB
		internal static bool IsMetadataImplementedType(ServiceDescription description, Type type)
		{
			return !ServiceMetadataBehavior.BehaviorMissingObjectNullOrServiceImplements(description, type) && type == typeof(IMetadataExchange);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000A3CD8 File Offset: 0x000A1ED8
		internal static bool IsMetadataImplementedType(Type type)
		{
			return type == typeof(IMetadataExchange);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000A3CEA File Offset: 0x000A1EEA
		internal void AddImplementedContracts(ServiceHostBase.ServiceAndBehaviorsContractResolver resolver)
		{
			if (!resolver.BehaviorContracts.ContainsKey("IMetadataExchange"))
			{
				resolver.BehaviorContracts.Add("IMetadataExchange", ServiceMetadataBehavior.MexContract);
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000A3D14 File Offset: 0x000A1F14
		private static void EnsureMexContractDescription()
		{
			if (ServiceMetadataBehavior.mexContract == null)
			{
				object obj = ServiceMetadataBehavior.thisLock;
				lock (obj)
				{
					if (ServiceMetadataBehavior.mexContract == null)
					{
						ServiceMetadataBehavior.mexContract = ServiceMetadataBehavior.CreateMexContract();
					}
				}
			}
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000A3D68 File Offset: 0x000A1F68
		private static ContractDescription CreateMexContract()
		{
			ContractDescription contract = ContractDescription.GetContract(typeof(IMetadataExchange));
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				operationDescription.Behaviors.Find<OperationBehaviorAttribute>().Impersonation = ImpersonationOption.Allowed;
			}
			contract.Behaviors.Add(new ServiceMetadataContractBehavior(true));
			return contract;
		}

		// Token: 0x040022BF RID: 8895
		public const string MexContractName = "IMetadataExchange";

		// Token: 0x040022C0 RID: 8896
		internal const string MexContractNamespace = "http://schemas.microsoft.com/2006/04/mex";

		// Token: 0x040022C1 RID: 8897
		private static readonly Uri emptyUri = new Uri(string.Empty, UriKind.Relative);

		// Token: 0x040022C2 RID: 8898
		private bool httpGetEnabled;

		// Token: 0x040022C3 RID: 8899
		private bool httpsGetEnabled;

		// Token: 0x040022C4 RID: 8900
		private Uri httpGetUrl;

		// Token: 0x040022C5 RID: 8901
		private Uri httpsGetUrl;

		// Token: 0x040022C6 RID: 8902
		private Binding httpGetBinding;

		// Token: 0x040022C7 RID: 8903
		private Binding httpsGetBinding;

		// Token: 0x040022C8 RID: 8904
		private Uri externalMetadataLocation;

		// Token: 0x040022C9 RID: 8905
		private MetadataExporter metadataExporter;

		// Token: 0x040022CA RID: 8906
		private static ContractDescription mexContract = null;

		// Token: 0x040022CB RID: 8907
		private static object thisLock = new object();

		// Token: 0x02000C1A RID: 3098
		internal class MetadataExtensionInitializer
		{
			// Token: 0x060076BF RID: 30399 RVA: 0x001BCF9E File Offset: 0x001BB19E
			internal MetadataExtensionInitializer(ServiceMetadataBehavior behavior, ServiceDescription description, ServiceHostBase host)
			{
				this.behavior = behavior;
				this.description = description;
				this.host = host;
			}

			// Token: 0x060076C0 RID: 30400 RVA: 0x001BCFBC File Offset: 0x001BB1BC
			internal MetadataSet GenerateMetadata()
			{
				if (this.behavior.ExternalMetadataLocation == null || this.behavior.ExternalMetadataLocation.ToString() == string.Empty)
				{
					if (this.metadataGenerationException != null)
					{
						throw this.metadataGenerationException;
					}
					try
					{
						MetadataExporter metadataExporter = this.behavior.MetadataExporter;
						XmlQualifiedName wsdlServiceQName = new XmlQualifiedName(this.description.Name, this.description.Namespace);
						Collection<ServiceEndpoint> collection = new Collection<ServiceEndpoint>();
						foreach (ServiceEndpoint serviceEndpoint in this.description.Endpoints)
						{
							ServiceMetadataContractBehavior serviceMetadataContractBehavior = serviceEndpoint.Contract.Behaviors.Find<ServiceMetadataContractBehavior>();
							if ((serviceMetadataContractBehavior != null && !serviceMetadataContractBehavior.MetadataGenerationDisabled) || (serviceMetadataContractBehavior == null && !serviceEndpoint.IsSystemEndpoint))
							{
								EndpointAddress address = null;
								EndpointDispatcher listenerByID = ServiceMetadataBehavior.GetListenerByID(this.host.ChannelDispatchers, serviceEndpoint.Id);
								if (listenerByID != null)
								{
									address = listenerByID.EndpointAddress;
								}
								ServiceEndpoint serviceEndpoint2 = new ServiceEndpoint(serviceEndpoint.Contract);
								serviceEndpoint2.Binding = serviceEndpoint.Binding;
								serviceEndpoint2.Name = serviceEndpoint.Name;
								serviceEndpoint2.Address = address;
								foreach (IEndpointBehavior item in serviceEndpoint.Behaviors)
								{
									serviceEndpoint2.Behaviors.Add(item);
								}
								collection.Add(serviceEndpoint2);
							}
						}
						WsdlExporter wsdlExporter = metadataExporter as WsdlExporter;
						if (wsdlExporter != null)
						{
							wsdlExporter.ExportEndpoints(collection, wsdlServiceQName, this.host.GetBindingParameters(collection));
						}
						else
						{
							foreach (ServiceEndpoint endpoint in collection)
							{
								metadataExporter.ExportEndpoint(endpoint);
							}
						}
						if (metadataExporter.Errors.Count > 0 && DiagnosticUtility.ShouldTraceWarning)
						{
							ServiceMetadataBehavior.MetadataExtensionInitializer.TraceWsdlExportErrors(metadataExporter);
						}
						return metadataExporter.GetGeneratedMetadata();
					}
					catch (Exception ex)
					{
						this.metadataGenerationException = ex;
						throw;
					}
				}
				return null;
			}

			// Token: 0x060076C1 RID: 30401 RVA: 0x001BD234 File Offset: 0x001BB434
			private static void TraceWsdlExportErrors(MetadataExporter exporter)
			{
				foreach (MetadataConversionError metadataConversionError in exporter.Errors)
				{
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						Hashtable dictionary = new Hashtable(2)
						{
							{
								"IsWarning",
								metadataConversionError.IsWarning
							},
							{
								"Message",
								metadataConversionError.Message
							}
						};
						TraceUtility.TraceEvent(TraceEventType.Warning, 524349, SR.GetString("TraceCodeWsmexNonCriticalWsdlExportError"), new DictionaryTraceRecord(dictionary), null, null);
					}
				}
			}

			// Token: 0x0400432F RID: 17199
			private ServiceMetadataBehavior behavior;

			// Token: 0x04004330 RID: 17200
			private ServiceDescription description;

			// Token: 0x04004331 RID: 17201
			private ServiceHostBase host;

			// Token: 0x04004332 RID: 17202
			private Exception metadataGenerationException;
		}
	}
}
