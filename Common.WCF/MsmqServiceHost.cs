using System;
using System.Linq;
using System.ServiceModel.Description;
using TechnoPro.Common.WCF.Adapters;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000010 RID: 16
	public class MsmqServiceHost : ClockWorkServerBaseServiceHost
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003AD9 File Offset: 0x00001CD9
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003AE1 File Offset: 0x00001CE1
		public string VirtualDirectory { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x0000317D File Offset: 0x0000137D
		public MsmqServiceHost()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003AEA File Offset: 0x00001CEA
		public MsmqServiceHost(Type t, params Uri[] baseAddresses) : base(t, baseAddresses)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003AF8 File Offset: 0x00001CF8
		protected override void OnOpening()
		{
			foreach (ServiceEndpoint endpoint in base.Description.Endpoints)
			{
				endpoint.VerifyQueue(false);
			}
			base.OnOpening();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003B58 File Offset: 0x00001D58
		protected override void AddEndpoints()
		{
			string address = string.Format("net.msmq://localhost/private/{0}/{1}.svc", this.VirtualDirectory, base.ContractName.Substring(1));
			bool flag = base.Description.Endpoints.All((ServiceEndpoint e) => e.Contract.Name != base.ContractName || !e.Binding.Name.Equals("NetMsmqBinding", StringComparison.OrdinalIgnoreCase));
			if (flag)
			{
				base.AddServiceEndpoint(base.ContractType, base.ContractType.GetNetMsmqBinding(), address);
			}
		}
	}
}
