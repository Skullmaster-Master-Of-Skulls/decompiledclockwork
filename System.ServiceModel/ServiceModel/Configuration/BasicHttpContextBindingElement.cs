using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005DE RID: 1502
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class BasicHttpContextBindingElement : BasicHttpBindingElement
	{
		// Token: 0x06003A34 RID: 14900 RVA: 0x000E04B1 File Offset: 0x000DE6B1
		public BasicHttpContextBindingElement()
		{
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000E04B9 File Offset: 0x000DE6B9
		public BasicHttpContextBindingElement(string name) : base(name)
		{
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000E04C2 File Offset: 0x000DE6C2
		protected override Type BindingElementType
		{
			get
			{
				return typeof(BasicHttpContextBinding);
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000E04CE File Offset: 0x000DE6CE
		// (set) Token: 0x06003A38 RID: 14904 RVA: 0x000E04E0 File Offset: 0x000DE6E0
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

		// Token: 0x06003A39 RID: 14905 RVA: 0x000E04F4 File Offset: 0x000DE6F4
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			BasicHttpContextBinding basicHttpContextBinding = (BasicHttpContextBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<bool>("contextManagementEnabled", basicHttpContextBinding.ContextManagementEnabled);
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000E0520 File Offset: 0x000DE720
		internal override void InitializeAllowCookies(HttpBindingBase binding)
		{
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000E0524 File Offset: 0x000DE724
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			if (base.ElementInformation.Properties["allowCookies"].ValueOrigin == PropertyValueOrigin.Default)
			{
				((BasicHttpBinding)binding).AllowCookies = true;
			}
			else if (!base.AllowCookies)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("BasicHttpContextBindingRequiresAllowCookie", new object[]
				{
					base.Name,
					""
				}));
			}
			((BasicHttpContextBinding)binding).ContextManagementEnabled = this.ContextManagementEnabled;
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x000E05A8 File Offset: 0x000DE7A8
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
							configurationPropertyCollection.Add(new ConfigurationProperty("contextManagementEnabled", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002A54 RID: 10836
		private const string ContextManagementEnabledName = "contextManagementEnabled";

		// Token: 0x04002A55 RID: 10837
		private ConfigurationPropertyCollection properties;
	}
}
