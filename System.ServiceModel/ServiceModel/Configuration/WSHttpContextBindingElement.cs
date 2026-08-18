using System;
using System.Configuration;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005DC RID: 1500
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class WSHttpContextBindingElement : WSHttpBindingElement
	{
		// Token: 0x06003A26 RID: 14886 RVA: 0x000E02A9 File Offset: 0x000DE4A9
		public WSHttpContextBindingElement()
		{
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000E02B1 File Offset: 0x000DE4B1
		public WSHttpContextBindingElement(string name) : base(name)
		{
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000E02BA File Offset: 0x000DE4BA
		// (set) Token: 0x06003A29 RID: 14889 RVA: 0x000E02CC File Offset: 0x000DE4CC
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

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000E02DA File Offset: 0x000DE4DA
		// (set) Token: 0x06003A2B RID: 14891 RVA: 0x000E02EC File Offset: 0x000DE4EC
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

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000E02FF File Offset: 0x000DE4FF
		// (set) Token: 0x06003A2D RID: 14893 RVA: 0x000E0311 File Offset: 0x000DE511
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

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x000E0324 File Offset: 0x000DE524
		protected override Type BindingElementType
		{
			get
			{
				return typeof(WSHttpContextBinding);
			}
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000E0330 File Offset: 0x000DE530
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			WSHttpContextBinding wshttpContextBinding = (WSHttpContextBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<Uri>("clientCallbackAddress", wshttpContextBinding.ClientCallbackAddress);
			base.SetPropertyValueIfNotDefaultValue<bool>("contextManagementEnabled", wshttpContextBinding.ContextManagementEnabled);
			base.SetPropertyValueIfNotDefaultValue<ProtectionLevel>("contextProtectionLevel", wshttpContextBinding.ContextProtectionLevel);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000E0380 File Offset: 0x000DE580
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			WSHttpContextBinding wshttpContextBinding = (WSHttpContextBinding)binding;
			wshttpContextBinding.ClientCallbackAddress = this.ClientCallbackAddress;
			wshttpContextBinding.ContextProtectionLevel = this.ContextProtectionLevel;
			wshttpContextBinding.ContextManagementEnabled = this.ContextManagementEnabled;
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06003A31 RID: 14897 RVA: 0x000E03C0 File Offset: 0x000DE5C0
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

		// Token: 0x04002A50 RID: 10832
		private const string ContextManagementEnabledName = "contextManagementEnabled";

		// Token: 0x04002A51 RID: 10833
		private const string ContextProtectionLevelName = "contextProtectionLevel";

		// Token: 0x04002A52 RID: 10834
		private ConfigurationPropertyCollection properties;
	}
}
