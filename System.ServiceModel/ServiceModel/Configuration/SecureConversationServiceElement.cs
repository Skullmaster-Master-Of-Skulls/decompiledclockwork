using System;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000680 RID: 1664
	public sealed class SecureConversationServiceElement : ConfigurationElement
	{
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x000F2398 File Offset: 0x000F0598
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("securityStateEncoderType", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x000F23F5 File Offset: 0x000F05F5
		// (set) Token: 0x06003FF6 RID: 16374 RVA: 0x000F2407 File Offset: 0x000F0607
		[ConfigurationProperty("securityStateEncoderType", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string SecurityStateEncoderType
		{
			get
			{
				return (string)base["securityStateEncoderType"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["securityStateEncoderType"] = value;
			}
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000F2424 File Offset: 0x000F0624
		public void Copy(SecureConversationServiceElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.SecurityStateEncoderType = from.SecurityStateEncoderType;
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000F2474 File Offset: 0x000F0674
		internal void ApplyConfiguration(SecureConversationServiceCredential secureConversation)
		{
			if (secureConversation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("secureConversation");
			}
			if (!string.IsNullOrEmpty(this.SecurityStateEncoderType))
			{
				Type type = Type.GetType(this.SecurityStateEncoderType, true);
				if (!typeof(SecurityStateEncoder).IsAssignableFrom(type))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidSecurityStateEncoderType", new object[]
					{
						this.SecurityStateEncoderType,
						typeof(SecurityStateEncoder).ToString()
					})));
				}
				secureConversation.SecurityStateEncoder = (SecurityStateEncoder)Activator.CreateInstance(type);
			}
		}

		// Token: 0x04002CC4 RID: 11460
		private ConfigurationPropertyCollection properties;
	}
}
