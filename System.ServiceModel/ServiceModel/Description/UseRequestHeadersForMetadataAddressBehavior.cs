using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Description
{
	// Token: 0x02000441 RID: 1089
	public class UseRequestHeadersForMetadataAddressBehavior : IServiceBehavior
	{
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000A423F File Offset: 0x000A243F
		public IDictionary<string, int> DefaultPortsByScheme
		{
			get
			{
				if (this.defaultPortsByScheme == null)
				{
					this.defaultPortsByScheme = new Dictionary<string, int>();
				}
				return this.defaultPortsByScheme;
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000A425A File Offset: 0x000A245A
		void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000A425C File Offset: 0x000A245C
		void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000A425E File Offset: 0x000A245E
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x040022D5 RID: 8917
		private Dictionary<string, int> defaultPortsByScheme;
	}
}
