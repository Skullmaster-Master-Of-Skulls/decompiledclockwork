using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B7 RID: 183
	public class InventoryLocationDAO : IInventoryLocationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0002EC34 File Offset: 0x0002CE34
		public InventoryLocationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0002EC46 File Offset: 0x0002CE46
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0002EC4E File Offset: 0x0002CE4E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000507 RID: 1287 RVA: 0x0002EC58 File Offset: 0x0002CE58
		public int CreateLocation(InventoryLocation location)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@locationid", DbType.Int32, 0),
				databaseLayer.GetParameter("@campus", DbType.String, string.IsNullOrEmpty(location.Campus) ? string.Empty : location.Campus),
				databaseLayer.GetParameter("@building", DbType.String, string.IsNullOrEmpty(location.Building) ? string.Empty : location.Building),
				databaseLayer.GetParameter("@roomnumber", DbType.String, string.IsNullOrEmpty(location.RoomNumber) ? string.Empty : location.RoomNumber),
				databaseLayer.GetParameter("@seat", DbType.String, string.IsNullOrEmpty(location.Seat) ? string.Empty : location.Seat),
				databaseLayer.GetParameter("@locationnotes", DbType.String, string.IsNullOrEmpty(location.Notes) ? string.Empty : location.Notes)
			};
			databaseLayer.ExecuteNonQuery("insert into InventoryV2_Location (Campus, Building, RoomNumber, Seat, LocationNotes)\r\nvalues (@campus, @building, @roomnumber, @seat, @locationnotes)\r\nset @locationid=SCOPE_IDENTITY()", array);
			bool flag = !(array[0].Value is DBNull);
			if (flag)
			{
				location.Id = (int)array[0].Value;
			}
			return location.Id;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0002EDAC File Offset: 0x0002CFAC
		public InventoryLocation GetLocationById(int locationId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_Location where LocationID=@locationid", new DbParameter[]
			{
				databaseLayer.GetParameter("@locationid", DbType.Int32, locationId)
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return InventoryLocationDAO.GetLocationFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0002EE38 File Offset: 0x0002D038
		public IList<InventoryLocation> GetAllLocations()
		{
			List<InventoryLocation> list = new List<InventoryLocation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from InventoryV2_Location"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryLocation locationFromReader = InventoryLocationDAO.GetLocationFromReader(dataReader);
						bool flag2 = locationFromReader != null;
						if (flag2)
						{
							list.Add(locationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002EECC File Offset: 0x0002D0CC
		public IList<InventoryLocation> GetLocations(string includingText)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@searchingtext", DbType.String, includingText);
			List<InventoryLocation> list = new List<InventoryLocation>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT * FROM InventoryV2_Location\r\n                where Campus LIKE '%'+@searchingtext+'%' OR\r\n\t                  Building LIKE '%'+@searchingtext+'%' OR\r\n\t                  RoomNumber LIKE '%'+@searchingtext+'%' OR\r\n\t                  LocationNotes LIKE '%'+@searchingtext+'%'", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryLocation locationFromReader = InventoryLocationDAO.GetLocationFromReader(dataReader);
						bool flag2 = locationFromReader != null;
						if (flag2)
						{
							list.Add(locationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002EF7C File Offset: 0x0002D17C
		public bool LocationInUse(int locationId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@locationid", DbType.Int32, locationId)
			};
			object obj = databaseLayer.ExecuteScalar("select 1 from InventoryV2_Product where LocationID = @locationid AND IsActive = 1\r\nUNION\r\nselect 1 from InventoryV2_LoanGroup where LocationID = @locationid", parameters);
			return obj != null && !Convert.IsDBNull(obj) && (int)obj > 0;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002EFE8 File Offset: 0x0002D1E8
		public bool DeleteLocation(int locationId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@locationid", DbType.Int32, locationId)
			};
			return databaseLayer.ExecuteNonQuery("if not exists (select 1 from InventoryV2_Product where LocationID = @locationid AND IsActive = 1\r\n\t\t\tUNION\r\n\t\t\tselect 1 from InventoryV2_LoanGroup where LocationID = @locationid\r\n\t\t\t)\r\n\tdelete from InventoryV2_Location where LocationID=@locationid", parameters) > 0;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002F040 File Offset: 0x0002D240
		public void UpdateLocation(InventoryLocation location)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@locationid", DbType.Int32, location.LocationId),
				databaseLayer.GetParameter("@campus", DbType.String, string.IsNullOrEmpty(location.Campus) ? string.Empty : location.Campus),
				databaseLayer.GetParameter("@building", DbType.String, string.IsNullOrEmpty(location.Building) ? string.Empty : location.Building),
				databaseLayer.GetParameter("@roomnumber", DbType.String, string.IsNullOrEmpty(location.RoomNumber) ? string.Empty : location.RoomNumber),
				databaseLayer.GetParameter("@seat", DbType.String, string.IsNullOrEmpty(location.Seat) ? string.Empty : location.Seat),
				databaseLayer.GetParameter("@locationnotes", DbType.String, string.IsNullOrEmpty(location.Notes) ? string.Empty : location.Notes)
			};
			databaseLayer.ExecuteNonQuery("update InventoryV2_Location\r\nset  Campus = @campus\r\n\t,Building = @building\r\n\t,RoomNumber = @roomnumber\r\n\t,Seat = @seat\r\n\t,LocationNotes = @locationnotes\r\nwhere LocationID = @locationid", parameters);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002F168 File Offset: 0x0002D368
		internal static InventoryLocation GetLocationFromReader(IDataReader record)
		{
			bool flag = !record.ContainsColumn("LocationID");
			InventoryLocation result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["LocationID"] is DBNull) ? 0 : ((int)record["LocationID"]);
				InventoryLocation inventoryLocation;
				if (num <= 0)
				{
					inventoryLocation = null;
				}
				else
				{
					InventoryLocation inventoryLocation2 = new InventoryLocation();
					inventoryLocation2.Id = num;
					inventoryLocation2.Campus = (string)record["Campus"];
					inventoryLocation2.Building = (string)record["Building"];
					inventoryLocation2.RoomNumber = (string)record["RoomNumber"];
					inventoryLocation2.Seat = (string)record["Seat"];
					inventoryLocation = inventoryLocation2;
					inventoryLocation2.Notes = (string)record["LocationNotes"];
				}
				result = inventoryLocation;
			}
			return result;
		}
	}
}
