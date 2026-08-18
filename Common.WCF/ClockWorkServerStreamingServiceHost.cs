using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace TechnoPro.Common.WCF
{
	// Token: 0x0200000D RID: 13
	public class ClockWorkServerStreamingServiceHost : ClockWorkServerBaseServiceHost
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000317D File Offset: 0x0000137D
		public ClockWorkServerStreamingServiceHost()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003187 File Offset: 0x00001387
		public ClockWorkServerStreamingServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
		{
			base.ServiceName = serviceType.Name;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031A0 File Offset: 0x000013A0
		public ClockWorkServerStreamingServiceHost(string serviceName, params Uri[] baseAddresses) : base(ClockWorkServerBaseServiceHost.CreateServiceType(serviceName), baseAddresses)
		{
			base.ServiceName = serviceName;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000031BC File Offset: 0x000013BC
		protected override void AddHttpEndpoint()
		{
			string contractName = base.ContractName;
			Type contractType = base.ContractType;
			bool flag = contractType == null;
			if (!flag)
			{
				bool flag2 = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("BasicHttpBinding", StringComparison.OrdinalIgnoreCase));
				if (flag2)
				{
					base.AddServiceEndpoint(contractType, contractType.GetHttpBinding(), "basicHttp").Behaviors.Add(new DispatcherSynchronizationBehavior
					{
						AsynchronousSendEnabled = true
					});
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000323C File Offset: 0x0000143C
		protected override void AddNetTcpEndpoint()
		{
			string contractName = base.ContractName;
			Type contractType = base.ContractType;
			bool flag = contractType == null;
			if (!flag)
			{
				bool flag2 = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != contractName || !e.Binding.Name.Equals("NetTcpBinding", StringComparison.OrdinalIgnoreCase));
				if (flag2)
				{
					base.AddServiceEndpoint(contractType, contractType.GetNetTcpBinding(SecurityMode.Message), "netTcp").Behaviors.Add(new DispatcherSynchronizationBehavior
					{
						AsynchronousSendEnabled = true
					});
				}
			}
		}
	}
}
