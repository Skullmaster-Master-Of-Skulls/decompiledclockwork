using System;
using System.Collections.Generic;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200025C RID: 604
	internal class ContractInfo
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x0004017C File Offset: 0x0003E37C
		public ContractInfo(Guid iid, ServiceEndpointElement endpoint, ComCatalogObject interfaceObject, ComCatalogObject application)
		{
			this.name = endpoint.Contract;
			this.iid = iid;
			ComCatalogCollection collection = interfaceObject.GetCollection("RolesForInterface");
			this.interfaceRoleMembers = CatalogUtil.GetRoleMembers(application, collection);
			this.operations = new List<OperationInfo>();
			ComCatalogCollection collection2 = interfaceObject.GetCollection("MethodsForInterface");
			foreach (ComCatalogObject methodObject in collection2)
			{
				this.operations.Add(new OperationInfo(methodObject, application));
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00040200 File Offset: 0x0003E400
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00040208 File Offset: 0x0003E408
		public Guid IID
		{
			get
			{
				return this.iid;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00040210 File Offset: 0x0003E410
		public string[] InterfaceRoleMembers
		{
			get
			{
				return this.interfaceRoleMembers;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00040218 File Offset: 0x0003E418
		public List<OperationInfo> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x04001986 RID: 6534
		private string name;

		// Token: 0x04001987 RID: 6535
		private Guid iid;

		// Token: 0x04001988 RID: 6536
		private string[] interfaceRoleMembers;

		// Token: 0x04001989 RID: 6537
		private List<OperationInfo> operations;
	}
}
