using System;
using System.Configuration;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005D8 RID: 1496
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ContextBindingElementExtensionElement : BindingElementExtensionElement
	{
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000DFEFE File Offset: 0x000DE0FE
		public override Type BindingElementType
		{
			get
			{
				return typeof(ContextBindingElement);
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000DFF0A File Offset: 0x000DE10A
		// (set) Token: 0x06003A0D RID: 14861 RVA: 0x000DFF1C File Offset: 0x000DE11C
		[ConfigurationProperty("clientCallbackAddress", DefaultValue = null)]
		public Uri ClientCallbackAddress
		{
			get
			{
				return (Uri)base["clientCallbackAddress"];
			}
			set
			{
				base["clientCallbackAddress"] = value;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000DFF2A File Offset: 0x000DE12A
		// (set) Token: 0x06003A0F RID: 14863 RVA: 0x000DFF3C File Offset: 0x000DE13C
		[ConfigurationProperty("contextExchangeMechanism", DefaultValue = ContextExchangeMechanism.ContextSoapHeader)]
		[ServiceModelEnumValidator(typeof(ContextExchangeMechanismHelper))]
		public ContextExchangeMechanism ContextExchangeMechanism
		{
			get
			{
				return (ContextExchangeMechanism)base["contextExchangeMechanism"];
			}
			set
			{
				base["contextExchangeMechanism"] = value;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x000DFF4F File Offset: 0x000DE14F
		// (set) Token: 0x06003A11 RID: 14865 RVA: 0x000DFF61 File Offset: 0x000DE161
		[ConfigurationProperty("protectionLevel", DefaultValue = ProtectionLevel.Sign)]
		[ServiceModelEnumValidator(typeof(ProtectionLevelHelper))]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return (ProtectionLevel)base["protectionLevel"];
			}
			set
			{
				base["protectionLevel"] = value;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000DFF74 File Offset: 0x000DE174
		// (set) Token: 0x06003A13 RID: 14867 RVA: 0x000DFF86 File Offset: 0x000DE186
		[ConfigurationProperty("contextManagementEnabled", DefaultValue = true)]
		public bool ContextManagementEnabled
		{
			get
			{
				return (bool)base["contextManagementEnabled"];
			}
			set
			{
				base["contextManagementEnabled"] = value;
			}
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000DFF99 File Offset: 0x000DE199
		protected internal override BindingElement CreateBindingElement()
		{
			return new ContextBindingElement(this.ProtectionLevel, this.ContextExchangeMechanism, this.ClientCallbackAddress, this.ContextManagementEnabled);
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000DFFB8 File Offset: 0x000DE1B8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCallbackAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("contextExchangeMechanism", typeof(ContextExchangeMechanism), ContextExchangeMechanism.ContextSoapHeader, null, new ServiceModelEnumValidator(typeof(ContextExchangeMechanismHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("protectionLevel", typeof(ProtectionLevel), ProtectionLevel.Sign, null, new ServiceModelEnumValidator(typeof(ProtectionLevelHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("contextManagementEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A47 RID: 10823
		internal const string ContextExchangeMechanismName = "contextExchangeMechanism";

		// Token: 0x04002A48 RID: 10824
		internal const string ContextManagementEnabledName = "contextManagementEnabled";

		// Token: 0x04002A49 RID: 10825
		private const string ProtectionLevelName = "protectionLevel";

		// Token: 0x04002A4A RID: 10826
		private ConfigurationPropertyCollection properties;
	}
}
