using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003AD RID: 941
	public class MsmqIntegrationBinding : MsmqBindingBase
	{
		// Token: 0x06002338 RID: 9016 RVA: 0x00080F77 File Offset: 0x0007F177
		public MsmqIntegrationBinding()
		{
			this.Initialize();
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00080F90 File Offset: 0x0007F190
		public MsmqIntegrationBinding(string configurationName)
		{
			this.Initialize();
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00080FB0 File Offset: 0x0007F1B0
		public MsmqIntegrationBinding(MsmqIntegrationSecurityMode securityMode)
		{
			if (!MsmqIntegrationSecurityModeHelper.IsDefined(securityMode))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("securityMode", (int)securityMode, typeof(MsmqIntegrationSecurityMode)));
			}
			this.Initialize();
			this.security.Mode = securityMode;
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00081008 File Offset: 0x0007F208
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x00081010 File Offset: 0x0007F210
		public MsmqIntegrationSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				this.security = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x00081019 File Offset: 0x0007F219
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x0008102B File Offset: 0x0007F22B
		internal Type[] TargetSerializationTypes
		{
			get
			{
				return (this.transport as MsmqIntegrationBindingElement).TargetSerializationTypes;
			}
			set
			{
				(this.transport as MsmqIntegrationBindingElement).TargetSerializationTypes = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x0008103E File Offset: 0x0007F23E
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x00081050 File Offset: 0x0007F250
		[DefaultValue(MsmqMessageSerializationFormat.Xml)]
		public MsmqMessageSerializationFormat SerializationFormat
		{
			get
			{
				return (this.transport as MsmqIntegrationBindingElement).SerializationFormat;
			}
			set
			{
				(this.transport as MsmqIntegrationBindingElement).SerializationFormat = value;
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x00081064 File Offset: 0x0007F264
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.security.Mode != MsmqIntegrationSecurityMode.Transport || (this.security.Transport.MsmqAuthenticationMode != MsmqAuthenticationMode.WindowsDomain || this.security.Transport.MsmqEncryptionAlgorithm != MsmqEncryptionAlgorithm.RC4Stream || this.security.Transport.MsmqSecureHashAlgorithm != MsmqDefaults.MsmqSecureHashAlgorithm || this.security.Transport.MsmqProtectionLevel != ProtectionLevel.Sign);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000810D3 File Offset: 0x0007F2D3
		private void Initialize()
		{
			this.transport = new MsmqIntegrationBindingElement();
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000810E0 File Offset: 0x0007F2E0
		private void ApplyConfiguration(string configurationName)
		{
			MsmqIntegrationBindingCollectionElement bindingCollectionElement = MsmqIntegrationBindingCollectionElement.GetBindingCollectionElement();
			MsmqIntegrationBindingElement msmqIntegrationBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (msmqIntegrationBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"msmqIntegrationBinding"
				})));
			}
			msmqIntegrationBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00081138 File Offset: 0x0007F338
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			this.security.ConfigureTransportSecurity(this.transport);
			bindingElementCollection.Add(this.transport);
			return bindingElementCollection.Clone();
		}

		// Token: 0x04001FE1 RID: 8161
		private MsmqIntegrationSecurity security = new MsmqIntegrationSecurity();
	}
}
