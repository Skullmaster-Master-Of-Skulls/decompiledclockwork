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
	// Token: 0x020000B4 RID: 180
	public class InventoryGroupDAO : IInventoryGroupDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0002CC2F File Offset: 0x0002AE2F
		public InventoryGroupDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0002CC41 File Offset: 0x0002AE41
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0002CC49 File Offset: 0x0002AE49
		public OperationContext OpContext { get; set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x0002CC54 File Offset: 0x0002AE54
		public int CreateProductGroup(InventoryGroup pGroup)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@productgroupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@groupname", DbType.String, pGroup.Name),
				databaseLayer.GetParameter("@groupnotes", DbType.String, pGroup.Notes ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO [InventoryV2_ProductGroup]\r\n                       ([GroupName]\r\n                       ,[GroupNotes])\r\n            VALUES\r\n                       (@groupname\r\n                       ,@groupnotes)\r\n\r\n            SET @productgroupid=scope_identity()", array);
			return pGroup.ProductGroupId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0002CD04 File Offset: 0x0002AF04
		public void UpdateProductGroup(InventoryGroup pGroup)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, pGroup.ProductGroupId),
				databaseLayer.GetParameter("@groupname", DbType.String, pGroup.Name),
				databaseLayer.GetParameter("@groupnotes", DbType.String, pGroup.Notes)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [InventoryV2_ProductGroup]\r\n                SET GroupName=@groupname,\r\n\t                GroupNotes=@groupnotes\r\n                WHERE ProductGroupID=@groupid", parameters);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0002CD88 File Offset: 0x0002AF88
		public bool DeleteEmptyProductGroup(int pGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productgroupid", DbType.Int32, pGroupId);
			return databaseLayer.ExecuteNonQuery("delete from InventoryV2_ProductGroup \r\n                where ProductGroupID=@productgroupid\r\n                AND NOT EXISTS (SELECT 1 from InventoryV2_Product p WHERE p.IsActive=1 AND p.GroupID=@productgroupid)", new DbParameter[]
			{
				parameter
			}) > 0;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		public InventoryGroup GetGroupById(int id)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_ProductGroup where ProductGroupID=@groupid", new DbParameter[]
			{
				databaseLayer.GetParameter("@groupid", DbType.Int32, id)
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryGroupDAO.GetGroupFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002CE6C File Offset: 0x0002B06C
		public IList<InventoryGroup> GetGroups()
		{
			List<InventoryGroup> list = new List<InventoryGroup>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_ProductGroup"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryGroup groupFromReader = InventoryGroupDAO.GetGroupFromReader(dataReader);
						bool flag2 = groupFromReader != null;
						if (flag2)
						{
							list.Add(groupFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0002CF00 File Offset: 0x0002B100
		internal static InventoryGroup GetGroupFromReader(IDataRecord record)
		{
			return new InventoryGroup
			{
				Id = (int)record["ProductGroupID"],
				Name = (string)record["GroupName"],
				Notes = (string)record["GroupNotes"]
			};
		}
	}
}
