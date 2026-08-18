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
	// Token: 0x020000B9 RID: 185
	public class InventoryProductStatusDAO : IInventoryProductStatusDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x00032038 File Offset: 0x00030238
		public InventoryProductStatusDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0003204A File Offset: 0x0003024A
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00032052 File Offset: 0x00030252
		public OperationContext OpContext { get; set; }

		// Token: 0x06000536 RID: 1334 RVA: 0x0003205C File Offset: 0x0003025C
		public int CreateProductStatus(InventoryProductStatus productStatus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@productstatusid", DbType.Int32, 0),
				databaseLayer.GetParameter("@productstatusname", DbType.String, productStatus.Name),
				databaseLayer.GetParameter("@productstatusdescription", DbType.String, productStatus.Description ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO [InventoryV2_ProductStatus]\r\n                       ([ProductStatusName]\r\n                       ,[ProductStatusDescription])\r\n              VALUES\r\n                       (@productstatusname\r\n                       ,@productstatusdescription)\r\n\r\n            SET @productstatusid = scope_identity()", array);
			return productStatus.ProductStatusId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0003210C File Offset: 0x0003030C
		public void UpdateProductStatus(InventoryProductStatus productStatus)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productstatusid", DbType.Int32, productStatus.ProductStatusId),
				databaseLayer.GetParameter("@productstatusname", DbType.String, productStatus.Name),
				databaseLayer.GetParameter("@productstatusdescription", DbType.String, productStatus.Description ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [InventoryV2_ProductStatus]\r\n                SET [ProductStatusName] = @productstatusname\r\n                    ,[ProductStatusDescription] = @productstatusdescription\r\n                WHERE ProductStatusID=@productstatusid", parameters);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00032198 File Offset: 0x00030398
		public InventoryProductStatus GetProductStatusById(int pStatusId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productstatusid", DbType.Int32, pStatusId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from InventoryV2_ProductStatus where ProductStatusID=@productstatusid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryProductStatusDAO.GetProductStatusFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0003222C File Offset: 0x0003042C
		public IList<InventoryProductStatus> GetProductStatusList()
		{
			List<InventoryProductStatus> list = new List<InventoryProductStatus>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from InventoryV2_ProductStatus"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryProductStatus productStatusFromReader = InventoryProductStatusDAO.GetProductStatusFromReader(dataReader);
						bool flag2 = productStatusFromReader != null;
						if (flag2)
						{
							list.Add(productStatusFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000322C0 File Offset: 0x000304C0
		internal static InventoryProductStatus GetProductStatusFromReader(IDataRecord record)
		{
			return new InventoryProductStatus
			{
				ProductStatusId = Convert.ToInt32(record["ProductStatusID"]),
				Name = Convert.ToString(record["ProductStatusName"]),
				Description = Convert.ToString(record["ProductStatusDescription"])
			};
		}
	}
}
