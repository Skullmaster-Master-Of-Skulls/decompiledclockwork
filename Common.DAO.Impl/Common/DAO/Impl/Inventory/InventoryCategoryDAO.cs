using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B3 RID: 179
	public class InventoryCategoryDAO : IInventoryCategoryDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0002C718 File Offset: 0x0002A918
		public InventoryCategoryDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0002C72A File Offset: 0x0002A92A
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0002C732 File Offset: 0x0002A932
		public OperationContext OpContext { get; set; }

		// Token: 0x060004D2 RID: 1234 RVA: 0x0002C73C File Offset: 0x0002A93C
		public IList<InventoryCategory> GetCategoriesByCatalog(int catalogId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<InventoryCategory> list = new List<InventoryCategory>();
			DbParameter parameter = databaseLayer.GetParameter("@catalogid", DbType.Int32, catalogId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_category where CatalogID=@catalogid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryCategory categoryFromReader = InventoryCategoryDAO.GetCategoryFromReader(dataReader);
						bool flag2 = categoryFromReader != null;
						if (flag2)
						{
							list.Add(categoryFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		public void AssignCategoryDynamicForm(string categoryName, int dynamicFormId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@categoryname", DbType.String, categoryName),
				databaseLayer.GetParameter("@dynamicformid", DbType.Int32, dynamicFormId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE InventoryV2_Category\r\n                SET DynamicFormID=@dynamicformid\r\n                WHERE CategoryName=@categoryname OR (CategoryName like @categoryname+ '.%' AND (DynamicFormID is NULL OR DynamicFormID=0))", parameters);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0002C854 File Offset: 0x0002AA54
		public bool DeleteEmptyCategory(int catalogId, string categoryName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@categoryname", DbType.String, categoryName)
			};
			List<Guid> list = new List<Guid>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("EXECUTE sp_Inventory_Delete_Category @categoryname", parameters))
			{
				bool flag = dataReader == null || dataReader.FieldCount == 0;
				if (flag)
				{
					return false;
				}
				while (dataReader.Read())
				{
					Guid item = (Guid)dataReader["ProductUniqueID"];
					list.Add(item);
				}
			}
			bool flag2 = list.Count > 0;
			if (flag2)
			{
				IInventoryAttachmentDAO inventoryAttachmentDAO = new InventoryAttachmentDAO(this.OpContext);
				foreach (Guid guid in list)
				{
					inventoryAttachmentDAO.SetProductPicture(guid, null);
					inventoryAttachmentDAO.RemoveAllAttachmentsFromProduct(guid);
				}
			}
			return true;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0002C984 File Offset: 0x0002AB84
		public void DeleteRootCategory(int catalogId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@catalogid", DbType.Int32, catalogId);
			databaseLayer.ExecuteNonQuery("declare @categoryname as varchar(250)\r\n                set @categoryname=(Select top(1) CatalogName as CategoryName from InventoryV2_Catalog where CatalogID=@catalogid)\r\n\r\n                if not(@categoryname is NULL)\r\n\t                begin\r\n\t\t                delete InventoryV2_Category \r\n\t\t                where CatalogId=@catalogid \r\n\t\t                AND CategoryName = @categoryname \r\n\t\t                AND NOT EXISTS (SELECT 1 from InventoryV2_Category where CategoryName LIKE @categoryname + '.%')\r\n\t\t                AND NOT EXISTS (SELECT 1 from InventoryV2_Product p where p.CategoryName=@categoryname)\r\n\t                end", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0002C9D8 File Offset: 0x0002ABD8
		public InventoryCategory GetCategoryByName(string categoryName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@categoryname", DbType.String, categoryName);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_Category where CategoryName=@categoryname", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryCategoryDAO.GetCategoryFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0002CA64 File Offset: 0x0002AC64
		public bool CreateCategory(int catalogId, int dynamicFormId, params string[] categories)
		{
			bool flag = categories == null || categories.Length == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				string text = null;
				DbTransaction transaction = databaseLayer.BeginDbTransaction();
				for (int i = 0; i < categories.Length - 1; i++)
				{
					string text2 = categories[i];
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@categoryname", DbType.String, text2),
						databaseLayer.GetParameter("@catalogid", DbType.Int32, catalogId),
						databaseLayer.GetParameter("@parentcategoryname", DbType.String, string.IsNullOrEmpty(text) ? DBNull.Value : text)
					};
					databaseLayer.ExecuteNonQuery("if not exists (select 1 from InventoryV2_Category where CategoryName=@categoryname)\r\n                begin\r\n\t\t\t\t\tdeclare @dynamicformid as int\r\n\t\t\t\t\tset @dynamicformid = (select top(1) DynamicFormID from InventoryV2_Category where CategoryName=@parentcategoryname)\r\n                    \r\n\t\t\t\t\tinsert into InventoryV2_Category (CategoryName, DynamicFormID, CatalogId)\r\n                    values (@categoryname, @dynamicformid, @catalogid)\r\n                end", parameters);
					text = text2;
				}
				string value = categories[categories.Length - 1];
				DbParameter[] parameters2 = new DbParameter[]
				{
					databaseLayer.GetParameter("@categoryname", DbType.String, value),
					databaseLayer.GetParameter("@dynamicformid", DbType.Int32, (dynamicFormId == 0) ? DBNull.Value : dynamicFormId),
					databaseLayer.GetParameter("@catalogid", DbType.Int32, catalogId),
					databaseLayer.GetParameter("@parentcategoryname", DbType.String, text)
				};
				bool flag2 = databaseLayer.ExecuteNonQuery("if not exists (select 1 from InventoryV2_Category where CategoryName=@categoryname)\r\n                begin\r\n\t\t\t\t\tdeclare @dynamicformid2 as int\r\n\t\t\t\t\tif(@dynamicformid is null or @dynamicformid=0)\r\n\t\t\t\t\t\tbegin\r\n\t\t\t\t\t\t\tset @dynamicformid2 = (select top(1) DynamicFormID from InventoryV2_Category where CategoryName=@parentcategoryname)\r\n\t\t\t\t\t\tend\r\n\t\t\t\t\telse\r\n\t\t\t\t\t\tbegin\r\n\t\t\t\t\t\t\tset @dynamicformid2 = @dynamicformid\r\n\t\t\t\t\t\tend\r\n\t\t\t\t\t\r\n                    insert into InventoryV2_Category (CategoryName, DynamicFormID, CatalogId)\r\n                    values (@categoryname, @dynamicformid2, @catalogid)\r\n                end", parameters2) > 0;
				databaseLayer.CommitDbTransaction(transaction);
				result = flag2;
			}
			return result;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0002CBBC File Offset: 0x0002ADBC
		internal static InventoryCategory GetCategoryFromReader(IDataRecord record)
		{
			return new InventoryCategory
			{
				CategoryName = Convert.ToString(record["CategoryName"]),
				DynamicFormId = ((record["DynamicFormID"] is DBNull) ? 0 : Convert.ToInt32(record["DynamicFormID"])),
				CatalogId = Convert.ToInt32(record["CatalogId"])
			};
		}
	}
}
