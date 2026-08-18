using System;
using System.Configuration;
using System.Net;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E3 RID: 1507
	public sealed class ServiceAuthenticationElement : BehaviorExtensionElement
	{
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000E09CA File Offset: 0x000DEBCA
		// (set) Token: 0x06003A5A RID: 14938 RVA: 0x000E09DC File Offset: 0x000DEBDC
		[ConfigurationProperty("serviceAuthenticationManagerType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string ServiceAuthenticationManagerType
		{
			get
			{
				return (string)base["serviceAuthenticationManagerType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["serviceAuthenticationManagerType"] = value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x000E09F9 File Offset: 0x000DEBF9
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x000E0A0B File Offset: 0x000DEC0B
		[ConfigurationProperty("authenticationSchemes", DefaultValue = AuthenticationSchemes.None)]
		[StandardRuntimeFlagEnumValidator(typeof(AuthenticationSchemes))]
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return (AuthenticationSchemes)base["authenticationSchemes"];
			}
			set
			{
				base["authenticationSchemes"] = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000E0A1E File Offset: 0x000DEC1E
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceAuthenticationBehavior);
			}
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000E0A2C File Offset: 0x000DEC2C
		protected internal override object CreateBehavior()
		{
			ServiceAuthenticationBehavior serviceAuthenticationBehavior = new ServiceAuthenticationBehavior();
			string serviceAuthenticationManagerType = this.ServiceAuthenticationManagerType;
			if (!string.IsNullOrEmpty(serviceAuthenticationManagerType))
			{
				Type type = Type.GetType(serviceAuthenticationManagerType, true);
				if (!typeof(ServiceAuthenticationManager).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidServiceAuthenticationManagerType", new object[]
					{
						serviceAuthenticationManagerType,
						typeof(ServiceAuthenticationManager)
					})));
				}
				serviceAuthenticationBehavior.ServiceAuthenticationManager = (ServiceAuthenticationManager)Activator.CreateInstance(type);
			}
			if (this.AuthenticationSchemes != AuthenticationSchemes.None)
			{
				serviceAuthenticationBehavior.AuthenticationSchemes = this.AuthenticationSchemes;
			}
			return serviceAuthenticationBehavior;
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000E0AC0 File Offset: 0x000DECC0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("serviceAuthenticationManagerType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("authenticationSchemes", typeof(AuthenticationSchemes), AuthenticationSchemes.None, null, new StandardRuntimeFlagEnumValidator<AuthenticationSchemes>(), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A59 RID: 10841
		private ConfigurationPropertyCollection properties;
	}
}
