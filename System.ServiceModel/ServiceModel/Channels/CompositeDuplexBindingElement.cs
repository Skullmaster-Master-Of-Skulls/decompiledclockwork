using System;
using System.ComponentModel;
using System.Net.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200089B RID: 2203
	public sealed class CompositeDuplexBindingElement : BindingElement, IPolicyExportExtension
	{
		// Token: 0x060053C5 RID: 21445 RVA: 0x00134932 File Offset: 0x00132B32
		public CompositeDuplexBindingElement()
		{
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0013493A File Offset: 0x00132B3A
		private CompositeDuplexBindingElement(CompositeDuplexBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.clientBaseAddress = elementToBeCloned.ClientBaseAddress;
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x0013494F File Offset: 0x00132B4F
		// (set) Token: 0x060053C8 RID: 21448 RVA: 0x00134957 File Offset: 0x00132B57
		[DefaultValue(null)]
		public Uri ClientBaseAddress
		{
			get
			{
				return this.clientBaseAddress;
			}
			set
			{
				this.clientBaseAddress = value;
			}
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00134960 File Offset: 0x00132B60
		public override BindingElement Clone()
		{
			return new CompositeDuplexBindingElement(this);
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00134968 File Offset: 0x00132B68
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(TChannel) != typeof(IOutputChannel))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x001349D8 File Offset: 0x00132BD8
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(TChannel) != typeof(IInputChannel))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}));
			}
			if (context.ListenUriBaseAddress == null)
			{
				if (this.clientBaseAddress != null)
				{
					context.ListenUriBaseAddress = this.clientBaseAddress;
					context.ListenUriRelativeAddress = Guid.NewGuid().ToString();
					context.ListenUriMode = ListenUriMode.Explicit;
				}
				else
				{
					context.ListenUriRelativeAddress = string.Empty;
					context.ListenUriMode = ListenUriMode.Unique;
				}
			}
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00134AA2 File Offset: 0x00132CA2
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return typeof(TChannel) == typeof(IOutputChannel) && context.CanBuildInnerChannelFactory<IOutputChannel>();
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x00134ADA File Offset: 0x00132CDA
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return typeof(TChannel) == typeof(IInputChannel) && context.CanBuildInnerChannelListener<IInputChannel>();
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00134B14 File Offset: 0x00132D14
		private ChannelProtectionRequirements GetProtectionRequirements()
		{
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(XD.UtilityDictionary.UniqueEndpointHeaderName.Value, XD.UtilityDictionary.UniqueEndpointHeaderNamespace.Value);
			MessagePartSpecification messagePartSpecification = new MessagePartSpecification(new XmlQualifiedName[]
			{
				xmlQualifiedName
			});
			messagePartSpecification.MakeReadOnly();
			channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification);
			channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification);
			return channelProtectionRequirements;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00134B7C File Offset: 0x00132D7C
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				ISecurityCapabilities innerProperty = context.GetInnerProperty<ISecurityCapabilities>();
				if (innerProperty != null)
				{
					return (T)((object)new SecurityCapabilities(innerProperty.SupportsClientAuthentication, false, innerProperty.SupportsClientWindowsIdentity, innerProperty.SupportedRequestProtectionLevel, ProtectionLevel.None));
				}
				return default(T);
			}
			else
			{
				if (typeof(T) == typeof(ChannelProtectionRequirements))
				{
					ChannelProtectionRequirements protectionRequirements = this.GetProtectionRequirements();
					protectionRequirements.Add(context.GetInnerProperty<ChannelProtectionRequirements>() ?? new ChannelProtectionRequirements());
					return (T)((object)protectionRequirements);
				}
				return context.GetInnerProperty<T>();
			}
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x00134C30 File Offset: 0x00132E30
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			CompositeDuplexBindingElement compositeDuplexBindingElement = b as CompositeDuplexBindingElement;
			return compositeDuplexBindingElement != null && this.clientBaseAddress == compositeDuplexBindingElement.clientBaseAddress;
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00134C60 File Offset: 0x00132E60
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			exporter.State[typeof(SupportedAddressingMode).Name] = SupportedAddressingMode.NonAnonymous;
			context.GetBindingAssertions().Add(CompositeDuplexBindingElement.CreateCompositeDuplexAssertion());
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00134CB0 File Offset: 0x00132EB0
		private static XmlElement CreateCompositeDuplexAssertion()
		{
			XmlDocument xmlDocument = new XmlDocument();
			return xmlDocument.CreateElement("cdp", "CompositeDuplex", "http://schemas.microsoft.com/net/2006/06/duplex");
		}

		// Token: 0x040032E8 RID: 13032
		private Uri clientBaseAddress;
	}
}
