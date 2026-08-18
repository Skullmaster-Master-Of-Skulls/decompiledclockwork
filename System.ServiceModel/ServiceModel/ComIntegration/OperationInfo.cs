using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200025D RID: 605
	internal class OperationInfo
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x00040220 File Offset: 0x0003E420
		public OperationInfo(ComCatalogObject methodObject, ComCatalogObject application)
		{
			this.name = (string)methodObject.GetValue("Name");
			ComCatalogCollection collection = methodObject.GetCollection("RolesForMethod");
			this.methodRoleMembers = CatalogUtil.GetRoleMembers(application, collection);
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00040262 File Offset: 0x0003E462
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0004026A File Offset: 0x0003E46A
		public string[] MethodRoleMembers
		{
			get
			{
				return this.methodRoleMembers;
			}
		}

		// Token: 0x0400198A RID: 6538
		private string name;

		// Token: 0x0400198B RID: 6539
		private string[] methodRoleMembers;
	}
}
