using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000BA RID: 186
	public class InventoryReservationDAO : IInventoryReservationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0003231C File Offset: 0x0003051C
		public InventoryReservationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0003232E File Offset: 0x0003052E
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00032336 File Offset: 0x00030536
		public OperationContext OpContext { get; set; }

		// Token: 0x0600053E RID: 1342 RVA: 0x00032340 File Offset: 0x00030540
		public InventoryReservation GetReservationById(int reservationId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@reservationid", DbType.Int32, reservationId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.ReservationID=@reservationid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetReservationFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000323D4 File Offset: 0x000305D4
		public IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueID);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.IsCompleted=0 and r.ProductUniqueId=@productuniqueid\r\n                ORDER BY rg.ReservationStartDate", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0003248C File Offset: 0x0003068C
		public IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueID),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.IsCompleted=0 and r.ProductUniqueId=@productuniqueid AND rg.ReservationStartDate < @enddate AND rg.ReservationEndDate > @startdate\r\n                ORDER BY rg.ReservationStartDate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0003256C File Offset: 0x0003076C
		public IList<InventoryReservation> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productid", DbType.Int32, productId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                INNER JOIN InventoryV2_Product p ON p.ProductUniqueId=r.ProductUniqueId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.IsCompleted=0 and p.ProductDynamicDataID=@productid AND rg.ReservationStartDate < @enddate AND rg.ReservationEndDate > @startdate\r\n                ORDER BY rg.ReservationStartDate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0003264C File Offset: 0x0003084C
		public IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@whomadereservationid", DbType.Int32, personId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.IsCompleted=0 and rg.WhoMadeReservationId=@whomadereservationid\r\n                ORDER BY rg.ReservationStartDate", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00032704 File Offset: 0x00030904
		public IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@whomadereservationid", DbType.Int32, personId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.IsCompleted=0 and rg.WhoMadeReservationId=@whomadereservationid AND rg.ReservationStartDate < @enddate AND rg.ReservationEndDate > @startdate\r\n                ORDER BY rg.ReservationStartDate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000327E4 File Offset: 0x000309E4
		public IList<InventoryReservation> GetReservations(DateTime startDate, DateTime endDate)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t    rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t    rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t    rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\nFROM InventoryV2_Reservation r\r\nINNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\nLEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\nLEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\nLEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\nLEFT JOIN InventoryV2_Product p ON r.ProductUniqueID=p.ProductUniqueID\r\nLEFT JOIN InventoryV2_Category cg ON cg.CategoryName=p.CategoryName\r\nLEFT JOIN InventoryV2_Catalog ct ON ct.CatalogID=cg.CatalogId\r\nWHERE ct.IsActive = 1 AND r.IsCompleted=0 AND rg.ReservationStartDate < @enddate AND rg.ReservationEndDate > @startdate\r\nORDER BY rg.ReservationStartDate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000328B0 File Offset: 0x00030AB0
		public InventoryReservation GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueID),
				databaseLayer.GetParameter("@date", DbType.DateTime, date)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT TOP(1) \r\n\t\t               r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n            WHERE r.IsCompleted=0 and r.ProductUniqueId=@productuniqueid AND rg.ReservationStartDate >= @date\r\n            ORDER BY rg.ReservationStartDate", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetReservationFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00032958 File Offset: 0x00030B58
		public int MakeReservation(InventoryReservationGroup reservationGroup, params Guid[] reservedProductUniqueIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[8];
			array[0] = databaseLayer.GetOutputParameter("@reservationgroupid", DbType.Int32, 0);
			array[1] = databaseLayer.GetParameter("@reservationstartdate", DbType.DateTime, reservationGroup.StartDate);
			array[2] = databaseLayer.GetParameter("@reservationenddate", DbType.DateTime, reservationGroup.EndDate);
			int num = 3;
			DatabaseLayer databaseLayer2 = databaseLayer;
			string pName = "@whomadereservationid";
			DbType pType = DbType.Int32;
			PersonBase whoMadeReservation = reservationGroup.WhoMadeReservation;
			array[num] = databaseLayer2.GetParameter(pName, pType, (whoMadeReservation != null) ? whoMadeReservation.Id : 0);
			int num2 = 4;
			DatabaseLayer databaseLayer3 = databaseLayer;
			string pName2 = "@whoreservedstaffpersonid";
			DbType pType2 = DbType.Int32;
			PersonBase whoReservedStaffPerson = reservationGroup.WhoReservedStaffPerson;
			array[num2] = databaseLayer3.GetParameter(pName2, pType2, (whoReservedStaffPerson != null) ? whoReservedStaffPerson.Id : 0);
			array[5] = databaseLayer.GetParameter("@notificationemails", DbType.String, (reservationGroup.NotificationEmails != null) ? string.Join("|", reservationGroup.NotificationEmails.ToArray<string>()) : string.Empty);
			array[6] = databaseLayer.GetParameter("@benotified", DbType.Boolean, reservationGroup.BeNotified);
			array[7] = databaseLayer.GetParameter("@reservationnotes", DbType.String, reservationGroup.ReservationNotes ?? string.Empty);
			DbParameter[] array2 = array;
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			databaseLayer.ExecuteNonQueryTransaction("INSERT INTO [InventoryV2_ReservationGroup]\r\n                       ([ReservationStartDate]\r\n                       ,[ReservationEndDate]\r\n                       ,[WhoMadeReservationId]\r\n                       ,[WhoReservedStaffPersonId]\r\n                       ,[NotificationEmails]\r\n                       ,[BeNotified]\r\n                       ,[ReservationNotes])\r\n                 VALUES\r\n                       (@reservationstartdate\r\n                       ,@reservationenddate\r\n                       ,@whomadereservationid\r\n                       ,@whoreservedstaffpersonid\r\n                       ,@notificationemails\r\n                       ,@benotified\r\n                       ,@reservationnotes)\r\n\r\n\t            SET @reservationgroupid = scope_identity()", transaction, array2);
			int num3 = Convert.ToInt32(array2[0].Value);
			bool flag = num3 > 0;
			if (flag)
			{
				foreach (Guid guid in reservedProductUniqueIds)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetOutputParameter("@reservationid", DbType.Int32, 0),
						databaseLayer.GetParameter("@productuniqueid", DbType.Guid, guid),
						databaseLayer.GetParameter("@reservationgroupid", DbType.Int32, num3)
					};
					databaseLayer.ExecuteNonQueryTransaction("INSERT INTO [InventoryV2_Reservation]\r\n                       ([ProductUniqueId]\r\n                       ,[ReservationGroupId])\r\n                 VALUES\r\n                       (@productuniqueid\r\n                       ,@reservationgroupid)\r\n\r\n\t            SET @reservationid = scope_identity()", transaction, parameters);
				}
				databaseLayer.CommitDbTransaction(transaction);
			}
			else
			{
				databaseLayer.RollbackDbTransaction(transaction);
			}
			return num3;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00032B4C File Offset: 0x00030D4C
		public void MarkReservationAsCompleted(int reservationId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@reservationid", DbType.Int32, reservationId);
			databaseLayer.ExecuteNonQuery("update InventoryV2_Reservation set IsCompleted=1 where ReservationID=@reservationid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00032BA0 File Offset: 0x00030DA0
		public void CancelReservation(int reservationId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@reservationid", DbType.Int32, reservationId);
			databaseLayer.ExecuteNonQuery("DELETE FROM InventoryV2_Reservation where ReservationID=@reservationid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00032BF4 File Offset: 0x00030DF4
		public void CancelReservationGroup(int reservationGroupId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@reservationgroupid", DbType.Int32, reservationGroupId);
			databaseLayer.ExecuteNonQuery("DELETE FROM InventoryV2_ReservationGroup where ReservationGroupId=@reservationgroupid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00032C48 File Offset: 0x00030E48
		public int UpdateReservation(InventoryReservation reservation)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@rgroupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@reservationid", DbType.Int32, reservation.ReservationId),
				databaseLayer.GetParameter("@reservationgroupid", DbType.Int32, reservation.Group.ReservationGroupId),
				databaseLayer.GetParameter("@reservationstartdate", DbType.DateTime, reservation.Group.StartDate),
				databaseLayer.GetParameter("@reservationenddate", DbType.DateTime, reservation.Group.EndDate),
				databaseLayer.GetParameter("@reservationnotes", DbType.String, reservation.Group.ReservationNotes ?? string.Empty),
				databaseLayer.GetParameter("@notificationemails", DbType.String, (reservation.Group.NotificationEmails != null) ? string.Join("|", reservation.Group.NotificationEmails.ToArray<string>()) : string.Empty),
				databaseLayer.GetParameter("@benotified", DbType.Boolean, reservation.Group.BeNotified),
				databaseLayer.GetParameter("@whomadereservationid", DbType.Int32, (reservation.Group.WhoMadeReservation != null) ? reservation.Group.WhoMadeReservation.Id : 0),
				databaseLayer.GetParameter("@whoreservedstaffpersonid", DbType.Int32, (reservation.Group.WhoReservedStaffPerson != null) ? reservation.Group.WhoReservedStaffPerson.Id : 0)
			};
			databaseLayer.ExecuteNonQuery("if ((select count(*) from InventoryV2_Reservation where ReservationGroupId=@reservationgroupid) > 1)\r\n\t            begin\r\n\t\t            BEGIN TRANSACTION\r\n\t\t            INSERT INTO [InventoryV2_ReservationGroup]\r\n                                    ([ReservationStartDate]\r\n                                    ,[ReservationEndDate]\r\n                                    ,[WhoMadeReservationId]\r\n                                    ,[WhoReservedStaffPersonId]\r\n                                    ,[NotificationEmails]\r\n                                    ,[BeNotified]\r\n                                    ,[ReservationNotes])\r\n                                VALUES\r\n                                    (@reservationstartdate\r\n                                    ,@reservationenddate\r\n                                    ,@whomadereservationid\r\n                                    ,@whoreservedstaffpersonid\r\n                                    ,@notificationemails\r\n                                    ,@benotified\r\n                                    ,@reservationnotes)\r\n\t                SET @rgroupid = scope_identity()\r\n\t                UPDATE [InventoryV2_Reservation]\r\n\t\t\t\t            SET\tReservationGroupId = @rgroupid\r\n\t\t\t\t            where ReservationID = @reservationid\r\n\t\t            COMMIT TRANSACTION\r\n\t            end\r\n            else\r\n\t            begin\r\n\t\t            UPDATE InventoryV2_ReservationGroup\r\n                            SET ReservationStartDate = @reservationstartdate,\r\n\t                            ReservationEndDate = @reservationenddate,\r\n\t                            NotificationEmails = @notificationemails,\r\n\t                            BeNotified = @benotified,\r\n\t                            ReservationNotes = @reservationnotes\r\n                            where ReservationGroupId = @reservationgroupid\r\n\t\t            set @rgroupid = @reservationgroupid\r\n\t            end", array);
			return reservation.Group.ReservationGroupId = ((array[0].Value is DBNull) ? 0 : Convert.ToInt32(array[0].Value));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00032E2C File Offset: 0x0003102C
		public void UpdateReservationGroup(InventoryReservationGroup reservationGroup)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@reservationgroupid", DbType.Int32, reservationGroup.ReservationGroupId),
				databaseLayer.GetParameter("@reservationstartdate", DbType.DateTime, reservationGroup.StartDate),
				databaseLayer.GetParameter("@reservationenddate", DbType.DateTime, reservationGroup.EndDate),
				databaseLayer.GetParameter("@notificationemails", DbType.String, (reservationGroup.NotificationEmails != null) ? string.Join("|", reservationGroup.NotificationEmails.ToArray<string>()) : string.Empty),
				databaseLayer.GetParameter("@benotified", DbType.Boolean, reservationGroup.BeNotified),
				databaseLayer.GetParameter("@reservationnotes", DbType.String, reservationGroup.ReservationNotes ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("UPDATE InventoryV2_ReservationGroup\r\n                SET ReservationStartDate = @reservationstartdate,\r\n\t                ReservationEndDate = @reservationenddate,\r\n\t                NotificationEmails = @notificationemails,\r\n\t                BeNotified = @benotified,\r\n\t                ReservationNotes = @reservationnotes\r\n                where ReservationGroupId = @reservationgroupid", parameters);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00032F24 File Offset: 0x00031124
		public IList<InventoryReservation> GetReservationsByReservationGroupId(int reservationGroupId)
		{
			List<InventoryReservation> list = new List<InventoryReservation>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@reservationgroupid", DbType.Int32, reservationGroupId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT r.ReservationID, r.ProductUniqueId,\r\n\t                   rg.ReservationGroupId, rg.ReservationStartDate, rg.ReservationEndDate, rg.CreationDatetime,\r\n\t                   rg.WhoMadeReservationId as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       rg.WhoReservedStaffPersonId as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n\t                   rg.NotificationEmails, rg.BeNotified, rg.ReservationNotes\r\n                FROM InventoryV2_Reservation r\r\n                INNER JOIN InventoryV2_ReservationGroup rg ON rg.ReservationGroupId=r.ReservationGroupId\r\n                LEFT JOIN people pto ON pto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=rg.WhoMadeReservationId\r\n                LEFT JOIN people pfrom ON pfrom.personid=rg.WhoReservedStaffPersonId\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=rg.WhoReservedStaffPersonId\r\n                WHERE r.IsCompleted=0 and r.ReservationGroupId=@reservationgroupid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						InventoryReservation reservationFromReader = this.GetReservationFromReader(dataReader, null);
						bool flag2 = reservationFromReader != null;
						if (flag2)
						{
							list.Add(reservationFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00032FDC File Offset: 0x000311DC
		private InventoryReservationGroup GetReservationGroupFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new InventoryReservationGroup
			{
				ReservationGroupId = Convert.ToInt32(record["ReservationGroupId"]),
				StartDate = (DateTime)record["ReservationStartDate"],
				EndDate = (DateTime)record["ReservationEndDate"],
				CreationDate = (DateTime)record["CreationDatetime"],
				WhoMadeReservation = PeopleDAO.GetPersonFromReader("to", record, this.OpContext, decryptor),
				WhoReservedStaffPerson = PeopleDAO.GetPersonFromReader("from", record, this.OpContext, decryptor),
				NotificationEmails = Convert.ToString(record["NotificationEmails"]).Split(new char[]
				{
					'|'
				}, StringSplitOptions.RemoveEmptyEntries),
				BeNotified = (bool)record["BeNotified"],
				ReservationNotes = Convert.ToString(record["ReservationNotes"])
			};
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000330DC File Offset: 0x000312DC
		private InventoryReservation GetReservationFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			IInventoryProductDAO inventoryProductDAO = new InventoryProductDAO(this.OpContext);
			InventoryProduct productById = inventoryProductDAO.GetProductById((Guid)record["ProductUniqueID"]);
			return new InventoryReservation
			{
				ReservationId = Convert.ToInt32(record["ReservationID"]),
				ReservedProduct = productById,
				Group = this.GetReservationGroupFromReader(record, decryptor)
			};
		}
	}
}
