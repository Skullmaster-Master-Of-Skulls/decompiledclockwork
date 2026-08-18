using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.DAO.Impl
{
	// Token: 0x02000017 RID: 23
	public class FileStorageDAO : IFileStorageDAO
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004738 File Offset: 0x00002938
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004740 File Offset: 0x00002940
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x00004749 File Offset: 0x00002949
		public FileStorageDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000477A File Offset: 0x0000297A
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004782 File Offset: 0x00002982
		public OperationContext OpContext { get; set; }

		// Token: 0x0600008C RID: 140 RVA: 0x0000478C File Offset: 0x0000298C
		public FileStructure LoadFile(FileType fileType, int addrSize, string clientVersion)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@title", DbType.String, fileType.Title),
				this.DatabaseManager.GetParameter("@addrsize", DbType.Int32, addrSize)
			};
			FileStructure result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT TOP 1 cf.BinaryData, cf.Version, cf.UploadDateTime, cf.WhoUploaded, cf.IsActive, cf.FileTypeId, cf.Notes, ct.title, cf.AddrSize\r\nFROM Common_FileStore cf left join Common_FileType ct ON cf.FileTypeId = ct.FileTypeId\r\nWHERE   ct.title=@title AND cf.isactive=1 AND cf.AddrSize=@addrsize\r\nORDER BY cf.Version DESC, cf.UploadDateTime DESC", parameters))
			{
				result = ((dataReader != null && dataReader.Read()) ? FileStorageDAO.GetFileStructure(dataReader, fileType) : null);
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004820 File Offset: 0x00002A20
		public void SaveFile(FileStructure fs)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@filetype", DbType.String, fs.FileType.Title),
				this.DatabaseManager.GetParameter("@binarydata", DbType.Binary, fs.BinaryData),
				this.DatabaseManager.GetParameter("@version", DbType.String, string.IsNullOrEmpty(fs.Version) ? string.Empty : fs.Version),
				this.DatabaseManager.GetParameter("@whouploaded", DbType.Int32, fs.WhoUploaded),
				this.DatabaseManager.GetParameter("@uploaddatetime", DbType.DateTime, DateTime.Now),
				this.DatabaseManager.GetParameter("@addrsize", DbType.Int32, fs.AddrSize)
			};
			this.DatabaseManager.ExecuteNonQuery("IF EXISTS(SELECT filestoreid FROM Common_FileStore WHERE FileTypeId IN (SELECT FileTypeId FROM Common_FileType WHERE title=@filetype) AND Version=@version AND AddrSize=@addrsize)\r\n                    UPDATE Common_FileStore SET binarydata=@binarydata, UploadDatetime=@uploaddatetime WHERE FileTypeId IN (SELECT FileTypeId FROM Common_FileType WHERE title=@filetype) AND Version=@version AND AddrSize=@addrsize\r\n                ELSE\r\n                    INSERT INTO Common_FileStore (BinaryData,Version,WhoUploaded,FileTypeId, AddrSize) \r\n                    SELECT TOP 1 @binarydata,@version,@whouploaded,FileTypeId, @addrsize FROM Common_FileType WHERE title=@filetype", parameters);
			DbParameter[] parameters2 = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@filetype", DbType.String, fs.FileType.Title),
				this.DatabaseManager.GetParameter("@version", DbType.String, string.IsNullOrEmpty(fs.Version) ? string.Empty : fs.Version),
				this.DatabaseManager.GetParameter("@addrsize", DbType.Int32, fs.AddrSize)
			};
			this.DatabaseManager.ExecuteNonQuery("delete from Common_FileStore \r\nwhere FileTypeId IN (SELECT FileTypeId FROM Common_FileType WHERE title=@filetype) AND [Version] > @version AND (AddrSize=@addrsize or AddrSize=0 or AddrSize is NULL)", parameters2);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004998 File Offset: 0x00002B98
		public FileVersionResp GetFileVersion(FileType fileType, int addrSize)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@title", DbType.String, fileType.Title),
				this.DatabaseManager.GetParameter("@addrsize", DbType.Int32, addrSize)
			};
			object obj = this.DatabaseManager.ExecuteScalar("SELECT TOP 1 cf.Version\r\n            FROM Common_FileStore cf inner join Common_FileType ct ON cf.FileTypeId = ct.FileTypeId\r\n            WHERE ct.title=@title AND cf.isactive=1 AND AddrSize=@addrsize\r\n            ORDER BY cf.Version DESC, cf.UploadDateTime DESC", parameters);
			object obj2 = null;
			bool flag = !string.IsNullOrEmpty(fileType.SecondaryTitle);
			if (flag)
			{
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@title", DbType.String, fileType.SecondaryTitle),
					this.DatabaseManager.GetParameter("@addrsize", DbType.Int32, addrSize)
				};
				obj2 = this.DatabaseManager.ExecuteScalar("SELECT TOP 1 cf.Version\r\n            FROM Common_FileStore cf inner join Common_FileType ct ON cf.FileTypeId = ct.FileTypeId\r\n            WHERE ct.title=@title AND cf.isactive=1 AND AddrSize=@addrsize\r\n            ORDER BY cf.Version DESC, cf.UploadDateTime DESC", parameters);
			}
			return new FileVersionResp
			{
				FileVersion = ((obj != null) ? ((string)obj) : string.Empty),
				SecondaryFileVersion = ((obj2 != null) ? ((string)obj2) : string.Empty)
			};
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004A94 File Offset: 0x00002C94
		private static FileStructure GetFileStructure(IDataRecord record, FileType fileType)
		{
			return new FileStructure
			{
				BinaryData = ((record["BinaryData"] == DBNull.Value) ? new byte[0] : ((byte[])record["BinaryData"])),
				Version = (string)record["Version"],
				UploadDateTime = (DateTime)record["UploadDateTime"],
				WhoUploaded = ((record["WhoUploaded"] == DBNull.Value) ? 0 : ((int)record["WhoUploaded"])),
				IsActive = (record["IsActive"] != DBNull.Value && Convert.ToBoolean(record["IsActive"])),
				AddrSize = ((record["AddrSize"] is DBNull) ? 0 : ((int)record["AddrSize"])),
				FileType = fileType
			};
		}
	}
}
