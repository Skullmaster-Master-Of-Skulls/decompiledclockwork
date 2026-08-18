using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x02000420 RID: 1056
	public sealed class ServiceMetadataContractBehavior : IContractBehavior
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x00097D75 File Offset: 0x00095F75
		public ServiceMetadataContractBehavior()
		{
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00097D7D File Offset: 0x00095F7D
		public ServiceMetadataContractBehavior(bool metadataGenerationDisabled) : this()
		{
			this.metadataGenerationDisabled = metadataGenerationDisabled;
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x00097D8C File Offset: 0x00095F8C
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x00097D94 File Offset: 0x00095F94
		public bool MetadataGenerationDisabled
		{
			get
			{
				return this.metadataGenerationDisabled;
			}
			set
			{
				this.metadataGenerationDisabled = value;
			}
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x00097D9D File Offset: 0x00095F9D
		void IContractBehavior.Validate(ContractDescription description, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x00097D9F File Offset: 0x00095F9F
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatch)
		{
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00097DA1 File Offset: 0x00095FA1
		void IContractBehavior.AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x00097DA3 File Offset: 0x00095FA3
		void IContractBehavior.ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime proxy)
		{
		}

		// Token: 0x04002246 RID: 8774
		private bool metadataGenerationDisabled;
	}
}
