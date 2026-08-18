using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.FileStorage;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.FileStorages
{
	// Token: 0x020000CC RID: 204
	public class FileTypeDAO : IFileTypeDAO
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00034E03 File Offset: 0x00033003
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00034E0B File Offset: 0x0003300B
		internal DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x06000581 RID: 1409 RVA: 0x00034E14 File Offset: 0x00033014
		public FileTypeDAO(OperationContext opContext)
		{
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00034E38 File Offset: 0x00033038
		public FileType GetFileType(string fileType)
		{
			DbParameter parameter = this.DatabaseManager.GetParameter("@filetype", DbType.String, fileType);
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("select * from Common_FileType where title=@filetype or secondarytitle=@filetype", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetFileType(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00034EB4 File Offset: 0x000330B4
		private FileType GetFileType(IDataRecord record)
		{
			return new FileType
			{
				Title = (string)record["title"],
				Description = (string)record["description"],
				Extension = ((record["Extension"] is DBNull) ? string.Empty : ((string)record["Extension"])),
				AddrSizeVersion = (bool)record["AddrSizeVersion"],
				SecondaryTitle = ((record["secondarytitle"] is DBNull) ? string.Empty : ((string)record["secondarytitle"]))
			};
		}
	}
}
