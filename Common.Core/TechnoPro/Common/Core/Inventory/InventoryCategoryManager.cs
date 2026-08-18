using System;
using System.Collections.Generic;
using System.Text;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E3 RID: 227
	public class InventoryCategoryManager : IInventoryCategoryManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00039C02 File Offset: 0x00037E02
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00039C0A File Offset: 0x00037E0A
		public IInventoryCategoryDAO InventoryCategoryDAO { get; set; }

		// Token: 0x060008AC RID: 2220 RVA: 0x00039C13 File Offset: 0x00037E13
		public InventoryCategoryManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.InventoryCategoryDAO = new InventoryCategoryDAO(opContext);
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00039C32 File Offset: 0x00037E32
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00039C3A File Offset: 0x00037E3A
		public OperationContext OpContext { get; set; }

		// Token: 0x060008AF RID: 2223 RVA: 0x00039C44 File Offset: 0x00037E44
		public bool CreateCategory(InventoryCategory category)
		{
			string[] array = category.CategoryName.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder stringBuilder = new StringBuilder(array[0]);
			List<string> list = new List<string>
			{
				array[0]
			};
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.AppendFormat(".{0}", array[i]);
				list.Add(stringBuilder.ToString());
			}
			return this.InventoryCategoryDAO.CreateCategory(category.CatalogId, category.DynamicFormId, list.ToArray());
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00039CD8 File Offset: 0x00037ED8
		public void AssignCategoryDynamicForm(string categoryName, int dynamicFormId)
		{
			this.InventoryCategoryDAO.AssignCategoryDynamicForm(categoryName, dynamicFormId);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00039CEC File Offset: 0x00037EEC
		public bool DeleteEmptyCategory(int catalogId, string categoryName)
		{
			bool flag = this.InventoryCategoryDAO.DeleteEmptyCategory(catalogId, categoryName);
			bool flag2 = flag;
			if (flag2)
			{
				string key = string.Format("Catalogs[{0}].Categories", catalogId);
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				cacheStorageManager.Remove(key);
			}
			return flag;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00039D38 File Offset: 0x00037F38
		public InventoryCategory GetCategoryByName(string categoryName)
		{
			return this.InventoryCategoryDAO.GetCategoryByName(categoryName);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00039D58 File Offset: 0x00037F58
		public IList<InventoryCategory> GetCategoriesByCatalog(int catalogId)
		{
			string key = string.Format("Catalogs[{0}].Categories", catalogId);
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[key];
			bool flag = obj is IList<int>;
			IList<InventoryCategory> result;
			if (flag)
			{
				result = (IList<InventoryCategory>)obj;
			}
			else
			{
				IList<InventoryCategory> categoriesByCatalog = this.InventoryCategoryDAO.GetCategoriesByCatalog(catalogId);
				cacheStorageManager.Insert(key, categoriesByCatalog, TimeSpan.FromHours(1.0));
				result = categoriesByCatalog;
			}
			return result;
		}
	}
}
