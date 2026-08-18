using System;
using System.Collections.Generic;
using System.Data;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Room;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.DAO.Impl.Room
{
	// Token: 0x0200006A RID: 106
	public class RoomDAO : IRoomDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00015EEC File Offset: 0x000140EC
		public RoomDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00015EFE File Offset: 0x000140FE
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00015F06 File Offset: 0x00014106
		public OperationContext OpContext { get; set; }

		// Token: 0x0600028C RID: 652 RVA: 0x00015F10 File Offset: 0x00014110
		public Seat GetSeatFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			return AppointmentRoomDAO.GetAppointmentRoomBaseFromRecord<Seat>(record, this.OpContext, null);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00015F30 File Offset: 0x00014130
		public IList<Seat> LoadAllSeats()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IList<Seat> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tDISTINCT p.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\ts.ParentSeatGroupId,s.Campus,s.OrderNum\r\nFROM\tpeoplegroups pg LEFT JOIN people p ON p.PersonID=pg.PersonID\r\n\t\tLEFT JOIN Seat s ON s.PersonId=pg.PersonID\r\nWHERE\tpg.GroupID=3\r\nORDER BY s.Campus,s.ParentSeatGroupId,s.OrderNum"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Seat> list = new List<Seat>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						Seat seatFromRecord = this.GetSeatFromRecord(dataReader, batchDecryptor);
						bool flag2 = seatFromRecord != null;
						if (flag2)
						{
							list.Add(seatFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
