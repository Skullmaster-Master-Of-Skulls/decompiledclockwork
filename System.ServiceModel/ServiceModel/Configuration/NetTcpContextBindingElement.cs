using System;
using System.Configuration;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005DA RID: 1498
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class NetTcpContextBindingElement : NetTcpBindingElement
	{
		// Token: 0x06003A18 RID: 14872 RVA: 0x000E009F File Offset: 0x000DE29F
		public NetTcpContextBindingElement()
		{
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000E00A7 File Offset: 0x000DE2A7
		public NetTcpContextBindingElement(string name) : base(name)
		{
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000E00B0 File Offset: 0x000DE2B0
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000E00C2 File Offset: 0x000DE2C2
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

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000E00D0 File Offset: 0x000DE2D0
		// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000E00E2 File Offset: 0x000DE2E2
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

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000E00F5 File Offset: 0x000DE2F5
		// (set) Token: 0x06003A1F RID: 14879 RVA: 0x000E0107 File Offset: 0x000DE307
		[ConfigurationProperty("contextProtectionLevel", DefaultValue = ProtectionLevel.Sign)]
		[ServiceModelEnumValidator(typeof(ProtectionLevelHelper))]
		public ProtectionLevel ContextProtectionLevel
		{
			get
			{
				return (ProtectionLevel)base["contextProtectionLevel"];
			}
			set
			{
				base["contextProtectionLevel"] = value;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000E011A File Offset: 0x000DE31A
		protected override Type BindingElementType
		{
			get
			{
				return typeof(NetTcpContextBinding);
			}
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000E0128 File Offset: 0x000DE328
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			NetTcpContextBinding netTcpContextBinding = (NetTcpContextBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<Uri>("clientCallbackAddress", netTcpContextBinding.ClientCallbackAddress);
			base.SetPropertyValueIfNotDefaultValue<bool>("contextManagementEnabled", netTcpContextBinding.ContextManagementEnabled);
			base.SetPropertyValueIfNotDefaultValue<ProtectionLevel>("contextProtectionLevel", netTcpContextBinding.ContextProtectionLevel);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000E0178 File Offset: 0x000DE378
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			NetTcpContextBinding netTcpContextBinding = (NetTcpContextBinding)binding;
			netTcpContextBinding.ClientCallbackAddress = this.ClientCallbackAddress;
			netTcpContextBinding.ContextManagementEnabled = this.ContextManagementEnabled;
			netTcpContextBinding.ContextProtectionLevel = this.ContextProtectionLevel;
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000E01B8 File Offset: 0x000DE3B8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("clientCallbackAddress", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("contextManagementEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("contextProtectionLevel", typeof(ProtectionLevel), ProtectionLevel.Sign, null, new ServiceModelEnumValidator(typeof(ProtectionLevelHelper)), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002A4C RID: 10828
		private const string ContextManagementEnabledName = "contextManagementEnabled";

		// Token: 0x04002A4D RID: 10829
		private const string ContextProtectionLevelName = "contextProtectionLevel";

		// Token: 0x04002A4E RID: 10830
		private ConfigurationPropertyCollection properties;
	}
}
