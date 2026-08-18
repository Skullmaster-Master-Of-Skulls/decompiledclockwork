using System;
using System.IdentityModel.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Security
{
	// Token: 0x02000379 RID: 889
	public class WSTrustServiceHost : ServiceHost
	{
		// Token: 0x0600210D RID: 8461 RVA: 0x0007AB6C File Offset: 0x00078D6C
		public WSTrustServiceHost(SecurityTokenServiceConfiguration securityTokenServiceConfiguration, params Uri[] baseAddresses) : this(new WSTrustServiceContract(securityTokenServiceConfiguration), baseAddresses)
		{
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0007AB7B File Offset: 0x00078D7B
		public WSTrustServiceHost(WSTrustServiceContract serviceContract, params Uri[] baseAddresses) : base(serviceContract, baseAddresses)
		{
			if (serviceContract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceContract");
			}
			if (serviceContract.SecurityTokenServiceConfiguration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceContract.SecurityTokenServiceConfiguration");
			}
			this._serviceContract = serviceContract;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0007ABB7 File Offset: 0x00078DB7
		public WSTrustServiceContract ServiceContract
		{
			get
			{
				return this._serviceContract;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x0007ABBF File Offset: 0x00078DBF
		public SecurityTokenServiceConfiguration SecurityTokenServiceConfiguration
		{
			get
			{
				return this._serviceContract.SecurityTokenServiceConfiguration;
			}
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0007ABCC File Offset: 0x00078DCC
		protected virtual void ConfigureMetadata()
		{
			if (base.BaseAddresses == null || base.BaseAddresses.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID3140"));
			}
			ServiceMetadataBehavior serviceMetadataBehavior = base.Description.Behaviors.Find<ServiceMetadataBehavior>();
			if (serviceMetadataBehavior == null)
			{
				serviceMetadataBehavior = new ServiceMetadataBehavior();
				base.Description.Behaviors.Add(serviceMetadataBehavior);
			}
			bool flag = base.Description.Endpoints.Find(typeof(IMetadataExchange)) != null;
			Binding binding = null;
			foreach (Uri uri in base.BaseAddresses)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(uri.Scheme, Uri.UriSchemeHttp))
				{
					serviceMetadataBehavior.HttpGetEnabled = true;
					binding = MetadataExchangeBindings.CreateMexHttpBinding();
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(uri.Scheme, Uri.UriSchemeHttps))
				{
					serviceMetadataBehavior.HttpsGetEnabled = true;
					binding = MetadataExchangeBindings.CreateMexHttpsBinding();
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(uri.Scheme, Uri.UriSchemeNetTcp))
				{
					binding = MetadataExchangeBindings.CreateMexTcpBinding();
				}
				else if (StringComparer.OrdinalIgnoreCase.Equals(uri.Scheme, Uri.UriSchemeNetPipe))
				{
					binding = MetadataExchangeBindings.CreateMexNamedPipeBinding();
				}
				if (!flag && binding != null)
				{
					base.AddServiceEndpoint("IMetadataExchange", binding, "mex");
				}
				binding = null;
			}
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0007AD34 File Offset: 0x00078F34
		protected override void ApplyConfiguration()
		{
			base.ApplyConfiguration();
			WSTrustServiceContract wstrustServiceContract = (WSTrustServiceContract)base.SingletonInstance;
			if (!wstrustServiceContract.SecurityTokenServiceConfiguration.DisableWsdl)
			{
				this.ConfigureMetadata();
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0007AD66 File Offset: 0x00078F66
		protected override void InitializeRuntime()
		{
			if (base.Description.Endpoints.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3097")));
			}
			this.UpdateServiceConfiguration();
			base.InitializeRuntime();
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0007ADA0 File Offset: 0x00078FA0
		protected virtual void UpdateServiceConfiguration()
		{
			base.Credentials.IdentityConfiguration = this._serviceContract.SecurityTokenServiceConfiguration;
			base.Credentials.UseIdentityConfiguration = true;
		}

		// Token: 0x04001F2F RID: 7983
		private WSTrustServiceContract _serviceContract;
	}
}
