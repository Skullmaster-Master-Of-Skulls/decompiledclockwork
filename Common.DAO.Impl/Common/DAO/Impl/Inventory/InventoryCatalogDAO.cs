using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B2 RID: 178
	public class InventoryCatalogDAO : IInventoryCatalogDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x0002C132 File Offset: 0x0002A332
		public InventoryCatalogDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0002C144 File Offset: 0x0002A344
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0002C14C File Offset: 0x0002A34C
		public OperationContext OpContext { get; set; }

		// Token: 0x060004C7 RID: 1223 RVA: 0x0002C158 File Offset: 0x0002A358
		public InventoryCatalog GetCatalogById(int catalogId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@catalogid", DbType.Int32, catalogId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n                    cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.CatalogID=@catalogid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetCatalog(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		public InventoryCatalog GetCatalogByName(IList<int> allowedCatalogIds, string name)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@catalogname", DbType.String, name);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n                    cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.CatalogName=@catalogname", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					InventoryCatalog catalog = this.GetCatalog(dataReader, null);
					bool flag2 = !allowedCatalogIds.Contains(catalog.InventoryCatalogId);
					if (flag2)
					{
						throw new PermissionDeniedException(string.Format("User Id '{0}' does not have permission to read Catalog Id '{1}'", this.OpContext.WhoAmI, catalog.InventoryCatalogId));
					}
					return catalog;
				}
			}
			return null;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		public int CreateCatalog(InventoryCatalog catalog)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@catalogid", DbType.Int32, 0),
				databaseLayer.GetParameter("@catalogname", DbType.String, catalog.Name),
				databaseLayer.GetParameter("@whocreated", DbType.Int32, (catalog.WhoCreated != null) ? catalog.WhoCreated.PersonId : this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@catalogdescription", DbType.String, string.IsNullOrEmpty(catalog.Description) ? string.Empty : catalog.Description)
			};
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			databaseLayer.ExecuteNonQueryTransaction("if not exists (select 1 from InventoryV2_Catalog where CatalogName=@catalogname)\r\n\t            begin\r\n\t\t            insert into InventoryV2_Catalog (CatalogName, CatalogDescription, WhoCreatedPersonId)\r\n\t\t            values (@catalogname, @catalogdescription, @whocreated)\r\n\t\t            set @catalogid=SCOPE_IDENTITY()\r\n\t            end", transaction, array);
			bool flag = !(array[0].Value is DBNull);
			int result;
			if (flag)
			{
				catalog.Id = (int)array[0].Value;
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@categoryname", DbType.String, catalog.Name),
					databaseLayer.GetParameter("@catalogid", DbType.Int32, catalog.InventoryCatalogId)
				};
				databaseLayer.ExecuteNonQueryTransaction("if not exists (select 1 from InventoryV2_Category where CategoryName=@categoryname)\r\n                begin\r\n\t\t\t\t\tinsert into InventoryV2_Category (CategoryName, DynamicFormID, CatalogId)\r\n                    values (@categoryname, NULL, @catalogid)\r\n                end", transaction, parameters);
				databaseLayer.CommitDbTransaction(transaction);
				result = catalog.Id;
			}
			else
			{
				databaseLayer.RollbackDbTransaction(transaction);
				result = 0;
			}
			return result;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0002C424 File Offset: 0x0002A624
		public void UpdateCatalog(InventoryCatalog catalogDAO)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@catalogid", DbType.Int32, catalogDAO.Id),
				databaseLayer.GetParameter("@catalogdescription", DbType.String, string.IsNullOrEmpty(catalogDAO.Description) ? string.Empty : catalogDAO.Description)
			};
			databaseLayer.ExecuteNonQuery("update InventoryV2_Catalog\r\n            set CatalogDescription=@catalogdescription\r\n            where CatalogID=@catalogid", parameters);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
		public IList<InventoryCatalog> GetCatalogs(IList<int> allowedCatalogIds)
		{
			List<InventoryCatalog> list = new List<InventoryCatalog>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			bool flag = allowedCatalogIds == null;
			if (flag)
			{
				using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n                    cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.IsActive=1"))
				{
					bool flag2 = dataReader != null;
					if (flag2)
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						while (dataReader.Read())
						{
							InventoryCatalog catalogWithoutCategories = this.GetCatalogWithoutCategories(dataReader, batchDecryptor);
							bool flag3 = catalogWithoutCategories != null;
							if (flag3)
							{
								list.Add(catalogWithoutCategories);
							}
						}
					}
				}
			}
			else
			{
				DbParameter parameter = databaseLayer.GetParameter("@allowedcatalogids", DbType.String, allowedCatalogIds.CommaSeparatedValues<int>());
				using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("select OrderID as CatalogID into #temp from SplitOrderIDs(@allowedcatalogids, ',')\r\n\r\n            select cat.CatalogID, cat.CatalogName, cat.CatalogDescription, cat.DateCreated,\r\n            cat.WhoCreatedPersonId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n            from InventoryV2_Catalog cat \r\n            left join people p on p.PersonID=cat.WhoCreatedPersonId\r\n            left join peoplemingroup pg on pg.PersonID=cat.WhoCreatedPersonId\r\n            where cat.IsActive=1 and cat.CatalogID in (select CatalogID from #temp);\r\n\r\n            drop table #temp", new DbParameter[]
				{
					parameter
				}))
				{
					bool flag4 = dataReader2 == null;
					if (flag4)
					{
						return list;
					}
					IBatchDecryptor batchDecryptor2 = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader2.Read())
					{
						InventoryCatalog catalogWithoutCategories2 = this.GetCatalogWithoutCategories(dataReader2, batchDecryptor2);
						bool flag5 = catalogWithoutCategories2 != null;
						if (flag5)
						{
							list.Add(catalogWithoutCategories2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0002C5F4 File Offset: 0x0002A7F4
		public bool DeleteEmptyCatalog(int catalogId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			return databaseLayer.ExecuteNonQuery("delete from InventoryV2_Catalog \r\n                where CatalogID=@catalogid and not exists(select 1 from InventoryV2_Category where CatalogID=@catalogid)", new DbParameter[]
			{
				databaseLayer.GetParameter("@catalogId", DbType.Int32, catalogId)
			}) > 0;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0002C64C File Offset: 0x0002A84C
		private InventoryCatalog GetCatalog(IDataReader record, IBatchDecryptor decryptor = null)
		{
			InventoryCatalog catalogWithoutCategories = this.GetCatalogWithoutCategories(record, decryptor);
			IInventoryCategoryDAO inventoryCategoryDAO = new InventoryCategoryDAO(this.OpContext);
			IList<InventoryCategory> categoriesByCatalog = inventoryCategoryDAO.GetCategoriesByCatalog(catalogWithoutCategories.InventoryCatalogId);
			catalogWithoutCategories.Categories = categoriesByCatalog;
			return catalogWithoutCategories;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002C68C File Offset: 0x0002A88C
		private InventoryCatalog GetCatalogWithoutCategories(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new InventoryCatalog
			{
				Id = (int)record["CatalogID"],
				Name = (string)record["CatalogName"],
				Description = (string)record["CatalogDescription"],
				WhoCreated = PeopleDAO.GetPersonFromReader("", record, this.OpContext, decryptor),
				CreationDate = (DateTime)record["DateCreated"]
			};
		}
	}
}
