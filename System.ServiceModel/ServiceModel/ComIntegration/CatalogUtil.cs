using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001DE RID: 478
	internal static class CatalogUtil
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x00036918 File Offset: 0x00034B18
		internal static string[] GetRoleMembers(ComCatalogObject application, ComCatalogCollection rolesCollection)
		{
			ComCatalogCollection collection = application.GetCollection("Roles");
			List<string> list = new List<string>();
			foreach (ComCatalogObject comCatalogObject in rolesCollection)
			{
				string a = (string)comCatalogObject.GetValue("Name");
				foreach (ComCatalogObject comCatalogObject2 in collection)
				{
					string b = (string)comCatalogObject2.GetValue("Name");
					if (a == b)
					{
						ComCatalogCollection collection2 = comCatalogObject2.GetCollection("UsersInRole");
						foreach (ComCatalogObject comCatalogObject3 in collection2)
						{
							string item = (string)comCatalogObject3.GetValue("User");
							list.Add(item);
						}
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000369F4 File Offset: 0x00034BF4
		internal static ComCatalogObject FindApplication(Guid applicationId)
		{
			ICatalog2 catalog = (ICatalog2)new xCatalog();
			ICatalogCollection catalogCollection = null;
			try
			{
				catalogCollection = (ICatalogCollection)catalog.GetCollection("Partitions");
				catalogCollection.Populate();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != HR.COMADMIN_E_PARTITIONS_DISABLED)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
			}
			if (catalogCollection != null)
			{
				for (int i = 0; i < catalogCollection.Count(); i++)
				{
					ICatalogObject catalogObject = (ICatalogObject)catalogCollection.Item(i);
					ICatalogCollection catalogCollection2 = (ICatalogCollection)catalogCollection.GetCollection("Applications", catalogObject.Key());
					catalogCollection2.Populate();
					ICatalogObject catalogObject2 = CatalogUtil.FindApplication(catalogCollection2, applicationId);
					if (catalogObject2 != null)
					{
						return new ComCatalogObject(catalogObject2, catalogCollection2);
					}
				}
			}
			else
			{
				ICatalogCollection catalogCollection3 = (ICatalogCollection)catalog.GetCollection("Applications");
				catalogCollection3.Populate();
				ICatalogObject catalogObject2 = CatalogUtil.FindApplication(catalogCollection3, applicationId);
				if (catalogObject2 != null)
				{
					return new ComCatalogObject(catalogObject2, catalogCollection3);
				}
			}
			return null;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00036AE4 File Offset: 0x00034CE4
		private static ICatalogObject FindApplication(ICatalogCollection appCollection, Guid applicationId)
		{
			for (int i = 0; i < appCollection.Count(); i++)
			{
				ICatalogObject catalogObject = (ICatalogObject)appCollection.Item(i);
				Guid a = Fx.CreateGuid((string)catalogObject.GetValue("ID"));
				if (a == applicationId)
				{
					return catalogObject;
				}
			}
			return null;
		}
	}
}
