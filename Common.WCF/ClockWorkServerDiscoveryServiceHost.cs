using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;

namespace TechnoPro.Common.WCF
{
	// Token: 0x0200000E RID: 14
	public class ClockWorkServerDiscoveryServiceHost : ClockWorkServerBaseServiceHost
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000317D File Offset: 0x0000137D
		public ClockWorkServerDiscoveryServiceHost()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003187 File Offset: 0x00001387
		public ClockWorkServerDiscoveryServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
		{
			base.ServiceName = serviceType.Name;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000031A0 File Offset: 0x000013A0
		public ClockWorkServerDiscoveryServiceHost(string serviceName, params Uri[] baseAddresses) : base(ClockWorkServerBaseServiceHost.CreateServiceType(serviceName), baseAddresses)
		{
			base.ServiceName = serviceName;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000032BC File Offset: 0x000014BC
		protected override void AddEndpoints()
		{
			string contractName = base.ContractName;
			Type contractType = base.ContractType;
			this.AddServiceEndpoint(new UdpDiscoveryEndpoint());
			bool flag = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("BasicHttpBinding", StringComparison.OrdinalIgnoreCase));
			if (flag)
			{
				EndpointDiscoveryBehavior endpointDiscoveryBehavior = new EndpointDiscoveryBehavior();
				endpointDiscoveryBehavior.Scopes.Add(new Uri("http://clockworks.ca"));
				base.AddServiceEndpoint(contractType, contractType.GetHttpBinding(), "basicHttp").Behaviors.Add(endpointDiscoveryBehavior);
			}
			bool flag2 = contractType != null && base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("NetTcpBinding", StringComparison.OrdinalIgnoreCase));
			if (flag2)
			{
				EndpointDiscoveryBehavior endpointDiscoveryBehavior2 = new EndpointDiscoveryBehavior();
				endpointDiscoveryBehavior2.Scopes.Add(new Uri("net.tcp://clockworks.ca"));
				base.AddServiceEndpoint(contractType, contractType.GetNetTcpBinding(SecurityMode.Message), "netTcp").Behaviors.Add(endpointDiscoveryBehavior2);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033B9 File Offset: 0x000015B9
		protected override void AddBehaviors()
		{
			base.AddBehaviors();
			base.Description.Behaviors.RemoveAll<ServiceDiscoveryBehavior>();
			base.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
		}
	}
}
