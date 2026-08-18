using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Inventory.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.Inventory
{
	// Token: 0x020000B5 RID: 181
	public class InventoryLoanDAO : IInventoryLoanDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x0002CF5C File Offset: 0x0002B15C
		public InventoryLoanDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0002CF6E File Offset: 0x0002B16E
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0002CF76 File Offset: 0x0002B176
		public OperationContext OpContext { get; set; }

		// Token: 0x060004E5 RID: 1253 RVA: 0x0002CF80 File Offset: 0x0002B180
		public IList<InventoryLoan> GetActiveLoans()
		{
			List<InventoryLoan> list = new List<InventoryLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select   al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n        lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n        lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n        al.ProductUniqueID, al.LoanGroupId\r\nfrom InventoryV2_ActiveLoan al \r\nINNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\nLEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\nLEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\nLEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\nLEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\nLEFT JOIN InventoryV2_Product p ON al.ProductUniqueID=p.ProductUniqueID\r\nLEFT JOIN InventoryV2_Category cg ON cg.CategoryName=p.CategoryName\r\nLEFT JOIN InventoryV2_Catalog ct ON ct.CatalogID=cg.CatalogId\r\nWHERE ct.IsActive = 1"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryLoan activeLoanFromReader = this.GetActiveLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = activeLoanFromReader != null;
						if (flag2)
						{
							list.Add(activeLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002D028 File Offset: 0x0002B228
		public InventoryLoan GetActiveLoanById(int loanID)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@loanid", DbType.Int32, loanID);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select   al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where al.LoanID = @loanid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetActiveLoanFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		public InventoryLoan GetActiveLoanByProduct(Guid productUniqueID)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueID);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where al.ProductUniqueID = @productuniqueid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetActiveLoanFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002D150 File Offset: 0x0002B350
		public InventoryLoan GetActiveLoanByProduct(int productId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productid", DbType.Int32, productId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId,\r\n                       p.ProductDynamicDataID\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                INNER JOIN InventoryV2_Product p ON p.ProductUniqueID=al.ProductUniqueID\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where p.ProductDynamicDataID = @productid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetActiveLoanFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		public IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId)
		{
			List<InventoryLoan> list = new List<InventoryLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@loanedtoid", DbType.Int32, personId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes,\r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where lg.LoanedToID = @loanedtoid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryLoan activeLoanFromReader = this.GetActiveLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = activeLoanFromReader != null;
						if (flag2)
						{
							list.Add(activeLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		public IList<InventoryLoan> GetActiveLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			List<InventoryLoan> list = new List<InventoryLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loanedtoid", DbType.Int32, personId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes,\r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where lg.LoanedToID = @loanedtoid AND lg.LoanedDate < @enddate AND lg.DueDate > @startdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryLoan activeLoanFromReader = this.GetActiveLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = activeLoanFromReader != null;
						if (flag2)
						{
							list.Add(activeLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002D39C File Offset: 0x0002B59C
		public IList<InventoryLoan> GetActiveLoansByDueDateInLessThan(TimeSpan dueDateIn)
		{
			List<InventoryLoan> list = new List<InventoryLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@enddate", DbType.DateTime, DateTime.Now.Date.Add(dueDateIn))
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes,\r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where lg.DueDate BETWEEN getdate() AND @enddate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryLoan activeLoanFromReader = this.GetActiveLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = activeLoanFromReader != null;
						if (flag2)
						{
							list.Add(activeLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0002D47C File Offset: 0x0002B67C
		public IList<InventoryLoan> GetOverDueDateActiveLoans()
		{
			List<InventoryLoan> list = new List<InventoryLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes,\r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where lg.DueDate < getdate()"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryLoan activeLoanFromReader = this.GetActiveLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = activeLoanFromReader != null;
						if (flag2)
						{
							list.Add(activeLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0002D524 File Offset: 0x0002B724
		public int MakeLoan(InventoryLoanGroup loanGroup, params Guid[] loanedProductUniqueIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@loangroupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@duedate", DbType.DateTime, loanGroup.DueDate),
				databaseLayer.GetParameter("@loannotes", DbType.String, loanGroup.LoanNotes ?? string.Empty),
				databaseLayer.GetParameter("@loanedtoid", DbType.Int32, loanGroup.LoanedTo.Id),
				databaseLayer.GetParameter("@locationid", DbType.Int32, (loanGroup.Location == null) ? DBNull.Value : loanGroup.Location.LocationId),
				databaseLayer.GetParameter("@wholoanedid", DbType.Int32, this.OpContext.WhoAmI)
			};
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			databaseLayer.ExecuteNonQueryTransaction("INSERT INTO [InventoryV2_LoanGroup]\r\n                       ([DueDate]\r\n                       ,[LoanNotes]\r\n                       ,[LoanedToID]\r\n                       ,[LocationID]\r\n                       ,[WhoLoanedID]\r\n                       )\r\n                 VALUES\r\n                       (@duedate\r\n                       ,@loannotes\r\n                       ,@loanedtoid\r\n                       ,@locationid\r\n                       ,@wholoanedid)\r\n\r\n            SET @loangroupid=SCOPE_IDENTITY()", transaction, array);
			int num = Convert.ToInt32(array[0].Value);
			int[] array2 = new int[loanedProductUniqueIds.Length];
			bool flag = num > 0;
			int result;
			if (flag)
			{
				for (int i = 0; i < loanedProductUniqueIds.Length; i++)
				{
					Guid guid = loanedProductUniqueIds[i];
					DbParameter[] array3 = new DbParameter[]
					{
						databaseLayer.GetOutputParameter("@loanid", DbType.Int32, 0),
						databaseLayer.GetParameter("@productuniqueid", DbType.Guid, guid),
						databaseLayer.GetParameter("@loangroupid", DbType.Int32, num)
					};
					databaseLayer.ExecuteNonQueryTransaction("INSERT INTO [InventoryV2_ActiveLoan]\r\n                       ([ProductUniqueID]\r\n                       ,[LoanGroupId])\r\n                 VALUES\r\n                       (@productuniqueid\r\n                       ,@loangroupid)\r\n\r\n            SET @loanid = SCOPE_IDENTITY()", transaction, array3);
					int num2 = (array3[0].Value is DBNull) ? 0 : Convert.ToInt32(array3[0].Value);
					bool flag2 = num2 <= 0;
					if (flag2)
					{
						databaseLayer.RollbackDbTransaction(transaction);
						return 0;
					}
					array2[i] = num2;
				}
				databaseLayer.CommitDbTransaction(transaction);
				IInventoryProductDAO inventoryProductDAO = new InventoryProductDAO(this.OpContext);
				for (int j = 0; j < loanedProductUniqueIds.Length; j++)
				{
					Guid guid2 = loanedProductUniqueIds[j];
					int returnLoanId = array2[j];
					InventoryProduct productById = inventoryProductDAO.GetProductById(guid2);
					bool flag3 = productById != null;
					if (flag3)
					{
						InventoryProductSnapshot inventoryProductSnapshot = new InventoryProductSnapshot
						{
							ProductUniqueId = guid2,
							ProductDynamicDataId = productById.ProductDynamicDataId,
							ProductName = (productById.Name ?? string.Empty),
							BarCode = (productById.BarCode ?? string.Empty),
							SerialNumber = (productById.SerialNumber ?? string.Empty),
							CategoryName = (productById.CategoryName ?? string.Empty),
							Location = ((productById.Location != null) ? productById.Location.ToString() : string.Empty),
							LocationDate = productById.LocationDatetime,
							InChargePerson = productById.InChargePerson,
							GroupName = ((productById.Group != null) ? (productById.Group.Name ?? string.Empty) : string.Empty),
							ProductStatus = ((productById.Status != null) ? (productById.Status.Name ?? string.Empty) : string.Empty),
							ReturnLoanId = returnLoanId,
							LoanGroupId = num,
							LoanedDate = new DateTime?(loanGroup.LoanedDate),
							DueDate = new DateTime?(loanGroup.DueDate),
							ReturnedDate = null,
							LoanedTo = loanGroup.LoanedTo,
							LoanLocation = ((loanGroup.Location != null) ? loanGroup.Location.ToString() : string.Empty),
							WhoLoaned = loanGroup.WhoLoaned,
							WhoReturned = null,
							LoanNotes = (loanGroup.LoanNotes ?? string.Empty),
							ReturnedStatus = string.Empty,
							ReturnedNotes = string.Empty,
							WhoModified = new PersonBase
							{
								PersonId = this.OpContext.WhoAmI
							},
							ModifiedDate = DateTime.Now,
							Reason = eInventoryProductSnapshotReason.Product_Loaned
						};
						inventoryProductSnapshot.ProductSnapshotId = inventoryProductDAO.CreateProductSnapshot(inventoryProductSnapshot);
					}
				}
				result = num;
			}
			else
			{
				databaseLayer.RollbackDbTransaction(transaction);
				result = 0;
			}
			return result;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0002D998 File Offset: 0x0002BB98
		public int UpdateLoan(InventoryLoan loan)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@rgroupid", DbType.Int32, 0),
				databaseLayer.GetParameter("@loanid", DbType.Int32, loan.LoanId),
				databaseLayer.GetParameter("@loangroupid", DbType.Int32, loan.Group.LoanGroupId),
				databaseLayer.GetParameter("@loaneddate", DbType.DateTime, loan.Group.LoanedDate),
				databaseLayer.GetParameter("@duedate", DbType.DateTime, loan.Group.DueDate),
				databaseLayer.GetParameter("@loannotes", DbType.String, loan.Group.LoanNotes ?? string.Empty),
				databaseLayer.GetParameter("@locationid", DbType.Int32, (loan.Group.Location == null) ? DBNull.Value : loan.Group.Location.LocationId),
				databaseLayer.GetParameter("@loanedtoid", DbType.Int32, loan.Group.LoanedTo.Id),
				databaseLayer.GetParameter("@wholoanedid", DbType.Int32, this.OpContext.WhoAmI)
			};
			databaseLayer.ExecuteNonQuery("if ((select count(*) from InventoryV2_ActiveLoan where LoanGroupId=@loangroupid) > 1)\r\n\t            begin\r\n\t\t            BEGIN TRANSACTION\r\n\t\t            INSERT INTO [InventoryV2_LoanGroup]\r\n                                    ([LoanedDate]\r\n                                    ,[DueDate]\r\n                                    ,[LoanNotes]\r\n                                    ,[LoanedToID]\r\n                                    ,[LocationID]\r\n                                    ,[WhoLoanedID]\r\n                                    )\r\n                                VALUES\r\n                                    (@loaneddate\r\n                                    ,@duedate\r\n                                    ,@loannotes\r\n                                    ,@loanedtoid\r\n                                    ,@locationid\r\n                                    ,@wholoanedid)\r\n\t\t            set @rgroupid = SCOPE_IDENTITY()\r\n\t\t            UPDATE [InventoryV2_ActiveLoan]\r\n\t\t\t\t            SET\tLoanGroupId = @rgroupid\r\n\t\t\t\t            where LoanID = @loanid\r\n\t\t            COMMIT TRANSACTION\r\n\t            end\r\n            else\r\n\t            begin\r\n\t\t            UPDATE [InventoryV2_LoanGroup]\r\n                                SET [DueDate] = @duedate\r\n                                    ,[LoanNotes] = @loannotes\r\n                                    ,[LocationID] = @locationid\r\n                                WHERE LoanGroupId = @loangroupid\r\n\t\t            set @rgroupid = @loangroupid\r\n\t            end", array);
			return loan.Group.LoanGroupId = ((array[0].Value is DBNull) ? 0 : Convert.ToInt32(array[0].Value));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0002DB34 File Offset: 0x0002BD34
		public void UpdateLoanGroup(InventoryLoanGroup loanGroup)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loangroupid", DbType.Int32, loanGroup.LoanGroupId),
				databaseLayer.GetParameter("@duedate", DbType.DateTime, loanGroup.DueDate),
				databaseLayer.GetParameter("@loannotes", DbType.String, loanGroup.LoanNotes ?? string.Empty),
				databaseLayer.GetParameter("@locationid", DbType.Int32, (loanGroup.Location == null) ? DBNull.Value : loanGroup.Location.LocationId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [InventoryV2_LoanGroup]\r\n                   SET [DueDate] = @duedate\r\n                      ,[LoanNotes] = @loannotes\r\n                      ,[LocationID] = @locationid\r\n                 WHERE LoanGroupId = @loangroupid", parameters);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0002DBF4 File Offset: 0x0002BDF4
		public IList<InventoryArchivedLoan> GetReturnedLoans()
		{
			List<InventoryArchivedLoan> list = new List<InventoryArchivedLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryArchivedLoan archivedLoanFromReader = this.GetArchivedLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = archivedLoanFromReader != null;
						if (flag2)
						{
							list.Add(archivedLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0002DC9C File Offset: 0x0002BE9C
		public void ReturnLoan(InventoryReturnedLoan returnedLoan)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, returnedLoan.LoanedProduct.UniqueId),
				databaseLayer.GetParameter("@productdynamicdataid", DbType.Int32, returnedLoan.LoanedProduct.ProductDynamicDataId),
				databaseLayer.GetParameter("@productname", DbType.String, returnedLoan.LoanedProduct.Name ?? string.Empty),
				databaseLayer.GetParameter("@barcode", DbType.String, returnedLoan.LoanedProduct.BarCode ?? string.Empty),
				databaseLayer.GetParameter("@serialnumber", DbType.String, returnedLoan.LoanedProduct.SerialNumber ?? string.Empty),
				databaseLayer.GetParameter("@categoryname", DbType.String, returnedLoan.LoanedProduct.CategoryName ?? string.Empty),
				databaseLayer.GetParameter("@productstatusid", DbType.Int32, (returnedLoan.LoanedProduct.Status != null) ? returnedLoan.LoanedProduct.Status.ProductStatusId : 0),
				databaseLayer.GetParameter("@productstatus", DbType.String, (returnedLoan.LoanedProduct.Status != null) ? (returnedLoan.LoanedProduct.Status.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@productlocationid", DbType.Int32, (returnedLoan.LoanedProduct.Location == null) ? DBNull.Value : returnedLoan.LoanedProduct.Location.LocationId),
				databaseLayer.GetParameter("@productlocation", DbType.String, (returnedLoan.LoanedProduct.Location != null) ? returnedLoan.LoanedProduct.Location.ToString() : string.Empty),
				databaseLayer.GetParameter("@locationdate", DbType.DateTime, (returnedLoan.LoanedProduct.LocationDatetime != null) ? returnedLoan.LoanedProduct.LocationDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@inchargepersonid", DbType.Int32, (returnedLoan.LoanedProduct.InChargePerson != null) ? returnedLoan.LoanedProduct.InChargePerson.Id : 0),
				databaseLayer.GetParameter("@groupid", DbType.Int32, (returnedLoan.LoanedProduct.Group != null) ? returnedLoan.LoanedProduct.Group.ProductGroupId : 0),
				databaseLayer.GetParameter("@groupname", DbType.String, (returnedLoan.LoanedProduct.Group != null) ? (returnedLoan.LoanedProduct.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@loanid", DbType.Int32, returnedLoan.LoanId),
				databaseLayer.GetParameter("@loangroupid", DbType.Int32, returnedLoan.Group.LoanGroupId),
				databaseLayer.GetParameter("@loaneddate", DbType.DateTime, returnedLoan.Group.LoanedDate),
				databaseLayer.GetParameter("@duedate", DbType.DateTime, returnedLoan.Group.DueDate),
				databaseLayer.GetParameter("@loanedtopersonid", DbType.Int32, (returnedLoan.Group.LoanedTo != null) ? returnedLoan.Group.LoanedTo.PersonId : 0),
				databaseLayer.GetParameter("@loanlocation", DbType.String, (returnedLoan.Group.Location != null) ? returnedLoan.Group.Location.ToString() : string.Empty),
				databaseLayer.GetParameter("@wholoanedpersonid", DbType.Int32, (returnedLoan.Group.WhoLoaned != null) ? returnedLoan.Group.WhoLoaned.PersonId : 0),
				databaseLayer.GetParameter("@whoreturnedid", DbType.Int32, (returnedLoan.WhoReturned != null) ? returnedLoan.WhoReturned.PersonId : 0),
				databaseLayer.GetParameter("@loannotes", DbType.String, returnedLoan.ReturnedNotes ?? string.Empty),
				databaseLayer.GetParameter("@returnedstatusid", DbType.Int32, (returnedLoan.ReturnedStatus != null) ? returnedLoan.ReturnedStatus.LoanStatusId : 0),
				databaseLayer.GetParameter("@returnedstatus", DbType.String, (returnedLoan.ReturnedStatus != null) ? (returnedLoan.ReturnedStatus.Name ?? string.Empty) : string.Empty),
				databaseLayer.GetParameter("@returnednotes", DbType.String, returnedLoan.ReturnedNotes ?? string.Empty),
				databaseLayer.GetParameter("@whomodifiedpersonid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@productaccessories", DbType.Xml, (returnedLoan.LoanedProduct.Accessories != null) ? returnedLoan.LoanedProduct.Accessories.ToXml() : DBNull.Value)
			};
			databaseLayer.ExecuteStoredProcedure("sp_Inventory_ReturnLoan", parameters);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0002E1B4 File Offset: 0x0002C3B4
		public InventoryArchivedLoan GetReturnedLoanById(int loanID)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@loanid", DbType.Int32, loanID);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId \r\n            where rl.LoanID = @loanid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetArchivedLoanFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0002E248 File Offset: 0x0002C448
		public IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID)
		{
			List<InventoryArchivedLoan> list = new List<InventoryArchivedLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueID);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId\r\n            where rl.ProductUniqueID = @productuniqueid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryArchivedLoan archivedLoanFromReader = this.GetArchivedLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = archivedLoanFromReader != null;
						if (flag2)
						{
							list.Add(archivedLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0002E310 File Offset: 0x0002C510
		public IList<InventoryArchivedLoan> GetReturnedLoansByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			List<InventoryArchivedLoan> list = new List<InventoryArchivedLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productuniqueid", DbType.Guid, productUniqueID),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId\r\n            where rl.ProductUniqueID = @productuniqueid AND lg.LoanedDate < @enddate AND rl.ReturnedDate > @startdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryArchivedLoan archivedLoanFromReader = this.GetArchivedLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = archivedLoanFromReader != null;
						if (flag2)
						{
							list.Add(archivedLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0002E400 File Offset: 0x0002C600
		public IList<InventoryArchivedLoan> GetReturnedLoansByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			List<InventoryArchivedLoan> list = new List<InventoryArchivedLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@productid", DbType.Int32, productId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription,\r\n                   p.ProductDynamicDataID\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            INNER JOIN InventoryV2_Product p ON p.ProductUniqueID=rl.ProductUniqueID\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId\r\n            where p.ProductDynamicDataID = @productid AND lg.LoanedDate < @enddate AND rl.ReturnedDate > @startdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryArchivedLoan archivedLoanFromReader = this.GetArchivedLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = archivedLoanFromReader != null;
						if (flag2)
						{
							list.Add(archivedLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0002E4F0 File Offset: 0x0002C6F0
		public IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId)
		{
			List<InventoryArchivedLoan> list = new List<InventoryArchivedLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@loanedtoid", DbType.Int32, personId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId\r\n            where lg.LoanedToId = @loanedtoid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryArchivedLoan archivedLoanFromReader = this.GetArchivedLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = archivedLoanFromReader != null;
						if (flag2)
						{
							list.Add(archivedLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
		public IList<InventoryArchivedLoan> GetReturnedLoansByPersonLoanedTo(int personId, DateTime startDate, DateTime endDate)
		{
			List<InventoryArchivedLoan> list = new List<InventoryArchivedLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loanedtoid", DbType.Int32, personId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select rl.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes, \r\n                   lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid, \r\n                   lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                   lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                   rl.ProductUniqueID, rl.LoanGroupId,\r\n                   rl.WhoReturnedId as retpersonid, pret.firstname as retfirstname,pret.lastname as retlastname,pret.middlename as retmiddlename,pret.student_no as retstudent_no,pgret.mingroupid AS retgroupid,\r\n                   rl.ReturnedNotes, rl.ReturnedDate, \r\n                   ls.LoanStatusID, ls.LoanStatusName, ls.LoanStatusDescription\r\n            from InventoryV2_ReturnedLoan rl \r\n            INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=rl.LoanGroupId\r\n            LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n            LEFT JOIN InventoryV2_LoanStatus ls on ls.LoanStatusID=rl.ReturnedStatusID\r\n            LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n            LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n            LEFT JOIN people pret ON pret.personid=rl.WhoReturnedId\r\n            LEFT JOIN peoplemingroup pgret ON pgret.personid=rl.WhoReturnedId\r\n            where lg.LoanedToID = @loanedtoid AND lg.LoanedDate < @enddate AND rl.ReturnedDate > @startdate", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryArchivedLoan archivedLoanFromReader = this.GetArchivedLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = archivedLoanFromReader != null;
						if (flag2)
						{
							list.Add(archivedLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
		public IList<InventoryLoan> GetLoansByLoanGroupId(int loanGroupId)
		{
			List<InventoryLoan> list = new List<InventoryLoan>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@loangroupid", DbType.Int32, loanGroupId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select al.LoanID, lg.LoanedDate, lg.DueDate, lg.LoanNotes,\r\n                       lg.LoanedToID as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                       lg.LocationID, l.Campus, l.Building, l.RoomNumber, l.Seat, l.LocationNotes,\r\n                       lg.WhoLoanedID as frompersonid, pfrom.firstname as fromfirstname, pfrom.lastname as fromlastname, pfrom.middlename as frommiddlename, pfrom.student_no as fromstudent_no, pgfrom.mingroupid AS fromgroupid,\r\n                       al.ProductUniqueID, al.LoanGroupId\r\n                from InventoryV2_ActiveLoan al \r\n                INNER JOIN InventoryV2_LoanGroup lg ON lg.LoanGroupId=al.LoanGroupId\r\n                LEFT JOIN InventoryV2_Location l ON l.LocationID=lg.LocationID\r\n                LEFT JOIN people pto ON pto.personid=lg.LoanedToID\r\n                LEFT JOIN peoplemingroup pgto ON pgto.personid=lg.LoanedToID\r\n                LEFT JOIN people pfrom ON pfrom.personid=lg.WhoLoanedID\r\n                LEFT JOIN peoplemingroup pgfrom ON pgfrom.personid=lg.WhoLoanedID\r\n                where al.LoanGroupId = @loangroupid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						InventoryLoan activeLoanFromReader = this.GetActiveLoanFromReader(dataReader, batchDecryptor);
						bool flag2 = activeLoanFromReader != null;
						if (flag2)
						{
							list.Add(activeLoanFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002E770 File Offset: 0x0002C970
		private InventoryLoan GetActiveLoanFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			IInventoryProductDAO inventoryProductDAO = new InventoryProductDAO(this.OpContext);
			InventoryProduct productById = inventoryProductDAO.GetProductById((Guid)record["ProductUniqueID"]);
			return new InventoryLoan
			{
				LoanId = Convert.ToInt32(record["LoanID"]),
				LoanedProduct = productById,
				Group = this.GetLoanGroupFromReader(record, decryptor)
			};
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		private InventoryLoanGroup GetLoanGroupFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new InventoryLoanGroup
			{
				LoanGroupId = Convert.ToInt32(record["LoanGroupId"]),
				LoanedDate = (DateTime)record["LoanedDate"],
				DueDate = (DateTime)record["DueDate"],
				LoanNotes = Convert.ToString(record["LoanNotes"]),
				LoanedTo = PeopleDAO.GetPersonFromReader("to", record, this.OpContext, decryptor),
				Location = InventoryLocationDAO.GetLocationFromReader(record),
				WhoLoaned = PeopleDAO.GetPersonFromReader("from", record, this.OpContext, decryptor)
			};
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002E88C File Offset: 0x0002CA8C
		private InventoryArchivedLoan GetArchivedLoanFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			int loanId = Convert.ToInt32(record["LoanID"]);
			Guid productUniqueId = (Guid)record["ProductUniqueID"];
			IInventoryProductDAO inventoryProductDAO = new InventoryProductDAO(this.OpContext);
			InventoryProductSnapshot productSnapshot = inventoryProductDAO.GetProductSnapshot(productUniqueId, loanId);
			return new InventoryArchivedLoan
			{
				LoanId = loanId,
				LoanedProduct = productSnapshot,
				Group = this.GetLoanGroupFromReader(record, decryptor),
				WhoReturned = PeopleDAO.GetPersonFromReader("ret", record, this.OpContext, decryptor),
				ReturnedNotes = Convert.ToString(record["ReturnedNotes"]),
				ReturnedDate = (DateTime)record["ReturnedDate"],
				ReturnedStatus = InventoryLoanStatusDAO.GetLoanStatusFromReader(record)
			};
		}
	}
}
