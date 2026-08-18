using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Legacy
{
	// Token: 0x020000A4 RID: 164
	public class LegacyAccommodationDAO : ILegacyAccommodationDAO
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00028E32 File Offset: 0x00027032
		public LegacyAccommodationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00028E44 File Offset: 0x00027044
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00028E4C File Offset: 0x0002704C
		public OperationContext OpContext { get; set; }

		// Token: 0x0600047C RID: 1148 RVA: 0x00028E58 File Offset: 0x00027058
		public void AddAccommodationLoaIssuedRow(int pid, int lucid, string loaString)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucid),
				databaseLayer.GetParameter("@whoissued", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@issuedmethod", DbType.Int32, 1),
				databaseLayer.GetParameter("@loa", DbType.Binary, databaseLayer.Encryption.Encrypt(loaString ?? ""))
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO accommodationloaissued (personid,lucourseid,whoissued,issuedmethod,loa)\r\nVALUES (@pid,@lucid,@whoissued,@issuedmethod,@loa)", parameters);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00028F1C File Offset: 0x0002711C
		public void CreateOrAddAccommodationApprovalNote(int pid, string note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@bb", DbType.Binary, databaseLayer.Encryption.Encrypt(note ?? "")),
				databaseLayer.GetParameter("@whoentered", DbType.Int32, this.OpContext.WhoAmI)
			};
			databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT personid FROM AccommodationsApprovalNotes WHERE personid=@pid)\r\n    UPDATE AccommodationsApprovalNotes SET controlvalue=@bb WHERE personid=@pid\r\nELSE\r\n    INSERT INTO AccommodationsApprovalNotes (whoentered,personid,controlvalue) VALUES (@whoentered,@pid,@bb)", parameters);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00028FB4 File Offset: 0x000271B4
		public string GetAccommodationsApprovalSummary(int pid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT controlvalue FROM AccommodationsApprovalNotes WHERE personid=@pid", parameters);
			bool flag = obj == null || obj is DBNull;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				byte[] encryptedText = (byte[])obj;
				result = databaseLayer.Encryption.Decrypt(encryptedText);
			}
			return result;
		}
	}
}
