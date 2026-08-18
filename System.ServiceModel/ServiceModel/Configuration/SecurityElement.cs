using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000682 RID: 1666
	public sealed class SecurityElement : SecurityElementBase
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x000F3F00 File Offset: 0x000F2100
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
							configurationPropertyCollection.Add(new ConfigurationProperty("secureConversationBootstrap", typeof(SecurityElementBase), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x000F3F7C File Offset: 0x000F217C
		public SecurityElement()
		{
			this.SecureConversationBootstrap.IsSecurityElementBootstrap = true;
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x000F3F90 File Offset: 0x000F2190
		[ConfigurationProperty("secureConversationBootstrap")]
		public SecurityElementBase SecureConversationBootstrap
		{
			get
			{
				return (SecurityElementBase)base["secureConversationBootstrap"];
			}
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x000F3FA4 File Offset: 0x000F21A4
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			SecurityElement securityElement = (SecurityElement)from;
			if (securityElement.ElementInformation.Properties["secureConversationBootstrap"].ValueOrigin != PropertyValueOrigin.Default)
			{
				this.SecureConversationBootstrap.CopyFrom(securityElement.SecureConversationBootstrap);
			}
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000F3FEC File Offset: 0x000F21EC
		protected internal override BindingElement CreateBindingElement(bool createTemplateOnly)
		{
			SecurityBindingElement securityBindingElement;
			if (base.AuthenticationMode == AuthenticationMode.SecureConversation)
			{
				if (this.SecureConversationBootstrap == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationNeedsBootstrapSecurity")));
				}
				if (this.SecureConversationBootstrap.AuthenticationMode == AuthenticationMode.SecureConversation)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationBootstrapCannotUseSecureConversation")));
				}
				SecurityBindingElement bootstrapSecurity = (SecurityBindingElement)this.SecureConversationBootstrap.CreateBindingElement(createTemplateOnly);
				securityBindingElement = SecurityBindingElement.CreateSecureConversationBindingElement(bootstrapSecurity, base.RequireSecurityContextCancellation);
			}
			else
			{
				securityBindingElement = (SecurityBindingElement)base.CreateBindingElement(createTemplateOnly);
			}
			this.ApplyConfiguration(securityBindingElement);
			return securityBindingElement;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000F4084 File Offset: 0x000F2284
		protected override void AddBindingTemplates(Dictionary<AuthenticationMode, SecurityBindingElement> bindingTemplates)
		{
			base.AddBindingTemplates(bindingTemplates);
			base.AddBindingTemplate(bindingTemplates, AuthenticationMode.SecureConversation);
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000F4096 File Offset: 0x000F2296
		private void InitializeSecureConversationParameters(SecureConversationSecurityTokenParameters sc, bool initializeNestedBindings)
		{
			base.SetPropertyValueIfNotDefaultValue<bool>("requireSecurityContextCancellation", sc.RequireCancellation);
			base.CanRenewSecurityContextToken = sc.CanRenewSession;
			if (sc.BootstrapSecurityBindingElement != null)
			{
				this.SecureConversationBootstrap.InitializeFrom(sc.BootstrapSecurityBindingElement, initializeNestedBindings);
			}
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000F40CF File Offset: 0x000F22CF
		protected override void InitializeNestedTokenParameterSettings(SecurityTokenParameters sp, bool initializeNestedBindings)
		{
			if (sp is SecureConversationSecurityTokenParameters)
			{
				this.InitializeSecureConversationParameters((SecureConversationSecurityTokenParameters)sp, initializeNestedBindings);
				return;
			}
			base.InitializeNestedTokenParameterSettings(sp, initializeNestedBindings);
		}

		// Token: 0x04002CCB RID: 11467
		private ConfigurationPropertyCollection properties;
	}
}
