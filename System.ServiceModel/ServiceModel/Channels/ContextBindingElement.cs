using System;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Administration;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A6 RID: 1958
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ContextBindingElement : BindingElement, IPolicyExportExtension, IContextSessionProvider, IWmiInstanceProvider, IContextBindingElement
	{
		// Token: 0x06004A13 RID: 18963 RVA: 0x00110268 File Offset: 0x0010E468
		public ContextBindingElement() : this(ProtectionLevel.Sign, ContextExchangeMechanism.ContextSoapHeader, null, true)
		{
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x00110274 File Offset: 0x0010E474
		public ContextBindingElement(ProtectionLevel protectionLevel) : this(protectionLevel, ContextExchangeMechanism.ContextSoapHeader, null, true)
		{
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x00110280 File Offset: 0x0010E480
		public ContextBindingElement(ProtectionLevel protectionLevel, ContextExchangeMechanism contextExchangeMechanism) : this(protectionLevel, contextExchangeMechanism, null, true)
		{
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x0011028C File Offset: 0x0010E48C
		public ContextBindingElement(ProtectionLevel protectionLevel, ContextExchangeMechanism contextExchangeMechanism, Uri clientCallbackAddress) : this(protectionLevel, contextExchangeMechanism, clientCallbackAddress, true)
		{
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00110298 File Offset: 0x0010E498
		public ContextBindingElement(ProtectionLevel protectionLevel, ContextExchangeMechanism contextExchangeMechanism, Uri clientCallbackAddress, bool contextManagementEnabled)
		{
			this.ProtectionLevel = protectionLevel;
			this.ContextExchangeMechanism = contextExchangeMechanism;
			this.ClientCallbackAddress = clientCallbackAddress;
			this.ContextManagementEnabled = contextManagementEnabled;
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x001102BD File Offset: 0x0010E4BD
		private ContextBindingElement(ContextBindingElement other) : base(other)
		{
			this.ProtectionLevel = other.ProtectionLevel;
			this.ContextExchangeMechanism = other.ContextExchangeMechanism;
			this.ClientCallbackAddress = other.ClientCallbackAddress;
			this.ContextManagementEnabled = other.ContextManagementEnabled;
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004A19 RID: 18969 RVA: 0x001102F6 File Offset: 0x0010E4F6
		// (set) Token: 0x06004A1A RID: 18970 RVA: 0x001102FE File Offset: 0x0010E4FE
		[DefaultValue(null)]
		public Uri ClientCallbackAddress { get; set; }

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06004A1B RID: 18971 RVA: 0x00110307 File Offset: 0x0010E507
		// (set) Token: 0x06004A1C RID: 18972 RVA: 0x0011030F File Offset: 0x0010E50F
		[DefaultValue(ContextExchangeMechanism.ContextSoapHeader)]
		public ContextExchangeMechanism ContextExchangeMechanism
		{
			get
			{
				return this.contextExchangeMechanism;
			}
			set
			{
				if (!ContextExchangeMechanismHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.contextExchangeMechanism = value;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x00110335 File Offset: 0x0010E535
		// (set) Token: 0x06004A1E RID: 18974 RVA: 0x0011033D File Offset: 0x0010E53D
		[DefaultValue(true)]
		public bool ContextManagementEnabled
		{
			get
			{
				return this.contextManagementEnabled;
			}
			set
			{
				this.contextManagementEnabled = value;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x00110346 File Offset: 0x0010E546
		// (set) Token: 0x06004A20 RID: 18976 RVA: 0x0011034E File Offset: 0x0010E54E
		[DefaultValue(ProtectionLevel.Sign)]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00110374 File Offset: 0x0010E574
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextBindingElementCannotProvideChannelFactory", new object[]
				{
					typeof(TChannel).ToString()
				})));
			}
			this.EnsureContextExchangeMechanismCompatibleWithScheme(context);
			this.EnsureContextExchangeMechanismCompatibleWithTransportCookieSetting(context);
			return new ContextChannelFactory<TChannel>(context, this.ContextExchangeMechanism, this.ClientCallbackAddress, this.ContextManagementEnabled);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x001103F8 File Offset: 0x0010E5F8
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ContextBindingElementCannotProvideChannelListener", new object[]
				{
					typeof(TChannel).ToString()
				})));
			}
			this.EnsureContextExchangeMechanismCompatibleWithScheme(context);
			return new ContextChannelListener<TChannel>(context, this.ContextExchangeMechanism);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00110468 File Offset: 0x0010E668
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return (typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IRequestSessionChannel) || (typeof(TChannel) == typeof(IDuplexSessionChannel) && this.ContextExchangeMechanism != ContextExchangeMechanism.HttpCookie)) && context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00110520 File Offset: 0x0010E720
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return (typeof(TChannel) == typeof(IInputChannel) || typeof(TChannel) == typeof(IInputSessionChannel) || typeof(TChannel) == typeof(IReplyChannel) || typeof(TChannel) == typeof(IReplySessionChannel) || (typeof(TChannel) == typeof(IDuplexSessionChannel) && this.ContextExchangeMechanism != ContextExchangeMechanism.HttpCookie)) && context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x001105D8 File Offset: 0x0010E7D8
		public override BindingElement Clone()
		{
			return new ContextBindingElement(this);
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x001105E0 File Offset: 0x0010E7E0
		public virtual void ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			ContextBindingElementPolicy.ExportRequireContextAssertion(this, context.GetBindingAssertions());
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00110604 File Offset: 0x0010E804
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ChannelProtectionRequirements) && this.ProtectionLevel != ProtectionLevel.None)
			{
				ChannelProtectionRequirements innerProperty = context.GetInnerProperty<ChannelProtectionRequirements>();
				if (innerProperty == null)
				{
					return (T)((object)ContextMessageHeader.GetChannelProtectionRequirements(this.ProtectionLevel));
				}
				ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements(innerProperty);
				channelProtectionRequirements.Add(ContextMessageHeader.GetChannelProtectionRequirements(this.ProtectionLevel));
				return (T)((object)channelProtectionRequirements);
			}
			else
			{
				if (typeof(T) == typeof(IContextSessionProvider))
				{
					return (T)((object)this);
				}
				if (typeof(T) == typeof(IContextBindingElement))
				{
					return (T)((object)this);
				}
				if (typeof(T) == typeof(ICorrelationDataSource))
				{
					ICorrelationDataSource correlationDataSource = this.instanceCorrelationData;
					if (correlationDataSource == null)
					{
						ICorrelationDataSource innerProperty2 = context.GetInnerProperty<ICorrelationDataSource>();
						correlationDataSource = CorrelationDataSourceHelper.Combine(innerProperty2, ContextBindingElement.ContextExchangeCorrelationDataDescription.DataSource);
						this.instanceCorrelationData = correlationDataSource;
					}
					return (T)((object)correlationDataSource);
				}
				return context.GetInnerProperty<T>();
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00110714 File Offset: 0x0010E914
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			ContextBindingElement contextBindingElement = b as ContextBindingElement;
			return contextBindingElement != null && !(this.ClientCallbackAddress != contextBindingElement.ClientCallbackAddress) && this.ContextExchangeMechanism == contextBindingElement.ContextExchangeMechanism && this.ContextManagementEnabled == contextBindingElement.ContextManagementEnabled && this.ProtectionLevel == contextBindingElement.protectionLevel;
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00110778 File Offset: 0x0010E978
		void IWmiInstanceProvider.FillInstance(IWmiInstance wmiInstance)
		{
			wmiInstance.SetProperty("ProtectionLevel", this.protectionLevel.ToString());
			wmiInstance.SetProperty("ContextExchangeMechanism", this.contextExchangeMechanism.ToString());
			wmiInstance.SetProperty("ContextManagementEnabled", this.contextManagementEnabled);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x001107D3 File Offset: 0x0010E9D3
		string IWmiInstanceProvider.GetInstanceType()
		{
			return "ContextBindingElement";
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x001107DC File Offset: 0x0010E9DC
		internal static void ValidateContextBindingElementOnAllEndpointsWithSessionfulContract(ServiceDescription description, IServiceBehavior callingBehavior)
		{
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (callingBehavior == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callingBehavior");
			}
			BindingParameterCollection parameters = new BindingParameterCollection();
			foreach (ServiceEndpoint serviceEndpoint in description.Endpoints)
			{
				if (serviceEndpoint.Binding != null && serviceEndpoint.Contract != null && !serviceEndpoint.InternalIsSystemEndpoint(description) && serviceEndpoint.Contract.SessionMode != SessionMode.NotAllowed && serviceEndpoint.Binding.GetProperty<IContextBindingElement>(parameters) == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BehaviorRequiresContextProtocolSupportInBinding", new object[]
					{
						callingBehavior.GetType().Name,
						serviceEndpoint.Name,
						serviceEndpoint.ListenUri.ToString()
					})));
				}
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x001108D0 File Offset: 0x0010EAD0
		private void EnsureContextExchangeMechanismCompatibleWithScheme(BindingContext context)
		{
			if (context.Binding != null && this.contextExchangeMechanism == ContextExchangeMechanism.HttpCookie && !"http".Equals(context.Binding.Scheme, StringComparison.OrdinalIgnoreCase) && !"https".Equals(context.Binding.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpCookieContextExchangeMechanismNotCompatibleWithTransportType", new object[]
				{
					context.Binding.Scheme,
					context.Binding.Namespace,
					context.Binding.Name
				})));
			}
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0011096C File Offset: 0x0010EB6C
		private void EnsureContextExchangeMechanismCompatibleWithTransportCookieSetting(BindingContext context)
		{
			if (context.Binding != null && this.contextExchangeMechanism == ContextExchangeMechanism.HttpCookie)
			{
				foreach (BindingElement bindingElement in context.Binding.Elements)
				{
					HttpTransportBindingElement httpTransportBindingElement = bindingElement as HttpTransportBindingElement;
					if (httpTransportBindingElement != null && httpTransportBindingElement.AllowCookies)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpCookieContextExchangeMechanismNotCompatibleWithTransportCookieSetting", new object[]
						{
							context.Binding.Namespace,
							context.Binding.Name
						})));
					}
				}
			}
		}

		// Token: 0x04002EF4 RID: 12020
		internal const ContextExchangeMechanism DefaultContextExchangeMechanism = ContextExchangeMechanism.ContextSoapHeader;

		// Token: 0x04002EF5 RID: 12021
		internal const bool DefaultContextManagementEnabled = true;

		// Token: 0x04002EF6 RID: 12022
		internal const ProtectionLevel DefaultProtectionLevel = ProtectionLevel.Sign;

		// Token: 0x04002EF7 RID: 12023
		private ContextExchangeMechanism contextExchangeMechanism;

		// Token: 0x04002EF8 RID: 12024
		private ICorrelationDataSource instanceCorrelationData;

		// Token: 0x04002EF9 RID: 12025
		private bool contextManagementEnabled;

		// Token: 0x04002EFA RID: 12026
		private ProtectionLevel protectionLevel;

		// Token: 0x02000CF4 RID: 3316
		private class ContextExchangeCorrelationDataDescription : CorrelationDataDescription
		{
			// Token: 0x06007A85 RID: 31365 RVA: 0x001C8461 File Offset: 0x001C6661
			private ContextExchangeCorrelationDataDescription()
			{
			}

			// Token: 0x17001BB8 RID: 7096
			// (get) Token: 0x06007A86 RID: 31366 RVA: 0x001C8469 File Offset: 0x001C6669
			public static ICorrelationDataSource DataSource
			{
				get
				{
					if (ContextBindingElement.ContextExchangeCorrelationDataDescription.cachedCorrelationDataSource == null)
					{
						ContextBindingElement.ContextExchangeCorrelationDataDescription.cachedCorrelationDataSource = new CorrelationDataSourceHelper(new CorrelationDataDescription[]
						{
							new ContextBindingElement.ContextExchangeCorrelationDataDescription()
						});
					}
					return ContextBindingElement.ContextExchangeCorrelationDataDescription.cachedCorrelationDataSource;
				}
			}

			// Token: 0x17001BB9 RID: 7097
			// (get) Token: 0x06007A87 RID: 31367 RVA: 0x001C848F File Offset: 0x001C668F
			public override bool IsOptional
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001BBA RID: 7098
			// (get) Token: 0x06007A88 RID: 31368 RVA: 0x001C8492 File Offset: 0x001C6692
			public override bool IsDefault
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001BBB RID: 7099
			// (get) Token: 0x06007A89 RID: 31369 RVA: 0x001C8495 File Offset: 0x001C6695
			public override bool KnownBeforeSend
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001BBC RID: 7100
			// (get) Token: 0x06007A8A RID: 31370 RVA: 0x001C8498 File Offset: 0x001C6698
			public override string Name
			{
				get
				{
					return ContextExchangeCorrelationHelper.CorrelationName;
				}
			}

			// Token: 0x17001BBD RID: 7101
			// (get) Token: 0x06007A8B RID: 31371 RVA: 0x001C849F File Offset: 0x001C669F
			public override bool ReceiveValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001BBE RID: 7102
			// (get) Token: 0x06007A8C RID: 31372 RVA: 0x001C84A2 File Offset: 0x001C66A2
			public override bool SendValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0400460B RID: 17931
			private static CorrelationDataSourceHelper cachedCorrelationDataSource;
		}
	}
}
