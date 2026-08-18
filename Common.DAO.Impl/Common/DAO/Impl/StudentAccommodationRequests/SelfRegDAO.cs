using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.Impl.StudentAccommodationRequests
{
	// Token: 0x02000043 RID: 67
	public class SelfRegDAO : ISelfRegDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0001008A File Offset: 0x0000E28A
		public SelfRegDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0001009C File Offset: 0x0000E29C
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public OperationContext OpContext { get; set; }

		// Token: 0x060001C4 RID: 452 RVA: 0x000100B0 File Offset: 0x0000E2B0
		public void CopyAccommodationsToCourse(int pid, int lucid, List<StudentCourseAccommodationModificationRequestItem> accommodationModificationRequests, IList<int> cidsToSkip)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucid)
			};
			string text = "DELETE FROM maininfoaccommodationps WHERE personid=@pid AND courseid=@lucid;\r\nDELETE FROM otherinfoaccommodationps WHERE personid=@pid AND courseid=@lucid;\r\nDELETE FROM datetimeinfoaccommodationps WHERE personid=@pid AND courseid=@lucid;\r\nDELETE FROM imageinfoaccommodationps WHERE personid=@pid AND courseid=@lucid;";
			databaseLayer.ExecuteNonQuery(text, parameters);
			List<int> cidsDeclined = (from f in accommodationModificationRequests
			select f.RequestedAccommodationData.Field.ControlId).ToList<int>();
			text = "SELECT orderid AS controlid INTO #tcids FROM splitorderids(@cids,',') \r\nSELECT orderid AS controlid INTO #tcids2 FROM splitorderids(@cidsToSkip,',')\r\n\r\nINSERT INTO {0} (personid,controlid,controlvalue,courseid,whomodified,showonletter) \r\n    SELECT  @pid,controlid,controlvalue,@lucid,@pid,1 FROM {0} \r\n    WHERE   personid=@pid AND courseid=0 \r\n            AND NOT controlid IN (SELECT controlid FROM #tcids)\r\n            AND NOT controlid IN (SELECT controlid FROM #tcids2)\r\n\r\nDROP TABLE #tcids\r\nDROP TABLE #tcids2";
			SelfRegDAO.CopyAccommodationsToCourse(databaseLayer, text, "maininfoaccommodationps", pid, lucid, cidsDeclined, cidsToSkip);
			SelfRegDAO.CopyAccommodationsToCourse(databaseLayer, text, "otherinfoaccommodationps", pid, lucid, cidsDeclined, cidsToSkip);
			SelfRegDAO.CopyAccommodationsToCourse(databaseLayer, text, "datetimeinfoaccommodationps", pid, lucid, cidsDeclined, cidsToSkip);
			SelfRegDAO.CopyAccommodationsToCourse(databaseLayer, text, "imageinfoaccommodationps", pid, lucid, cidsDeclined, cidsToSkip);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001018C File Offset: 0x0000E38C
		private static void CopyAccommodationsToCourse(DatabaseLayer databaseManager, string sql, string tableName, int pid, int lucid, IList<int> cidsDeclined, IList<int> cidsToSkip)
		{
			DbParameter[] array = new DbParameter[4];
			array[0] = databaseManager.GetParameter("@pid", DbType.Int32, pid);
			array[1] = databaseManager.GetParameter("@lucid", DbType.Int32, lucid);
			int num = 2;
			string pName = "@cids";
			DbType pType = DbType.String;
			object value;
			if (cidsDeclined != null)
			{
				value = string.Join(",", (from g in cidsDeclined
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			int num2 = 3;
			string pName2 = "@cidsToSkip";
			DbType pType2 = DbType.String;
			object value2;
			if (cidsToSkip != null)
			{
				value2 = string.Join(",", (from g in cidsToSkip
				select g.ToString()).ToArray<string>());
			}
			else
			{
				value2 = "";
			}
			array[num2] = databaseManager.GetParameter(pName2, pType2, value2);
			DbParameter[] parameters = array;
			databaseManager.ExecuteNonQuery(string.Format(sql, tableName), parameters);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00010280 File Offset: 0x0000E480
		public Pair<string, string> GetPersonIdAndLuCourseIdAsLongtermUrlStrings(int pid, int lucid)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			IEncryption encryption = databaseLayer.Encryption;
			return new Pair<string, string>(this.ConvertIntParameterToLongtermUrlString(encryption, pid), this.ConvertIntParameterToLongtermUrlString(encryption, pid));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000102C0 File Offset: 0x0000E4C0
		private string ConvertIntParameterToLongtermUrlString(IEncryption encryption, int parameter)
		{
			string arg = DateTime.Now.ToString("yyyy-MM-dd H:mm");
			string plainText = string.Format("{0}`{1}`{2}", parameter.ToString(), arg, "LNG");
			byte[] inArray = encryption.Encrypt(plainText);
			string stringToEscape = Convert.ToBase64String(inArray);
			return Uri.EscapeDataString(stringToEscape);
		}
	}
}
