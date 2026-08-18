using System;

namespace TechnoPro.Common.DAO.Impl.FileStorages
{
	// Token: 0x020000CD RID: 205
	public static class QueryStorageFileStorage
	{
		// Token: 0x040002DD RID: 733
		internal const string SQ_GET_FILE_TYPE_BY_TITLE = "select * from Common_FileType where title=@filetype or secondarytitle=@filetype";

		// Token: 0x040002DE RID: 734
		internal const string SQ_GET_BASE_FILE_INFO = "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_Files]";

		// Token: 0x040002DF RID: 735
		internal const string SQ_GET_BASE_TEMP_FILE_INFO = "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_TempFiles]";

		// Token: 0x040002E0 RID: 736
		internal const string SQ_GET_FILE_INFO_BY_UNIQUEID = "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_Files] where FileID=@fileid";

		// Token: 0x040002E1 RID: 737
		internal const string SQ_GET_TEMP_FILE_INFO_BY_UNIQUEID = "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_TempFiles] where FileID=@fileid";

		// Token: 0x040002E2 RID: 738
		internal const string SQ_GET_FILE_BY_LEGACY_ID = "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_Files] where LegacyID=@legacyid AND [Source]=@source";

		// Token: 0x040002E3 RID: 739
		internal const string SQ_GET_TEMP_FILE_BY_LEGACY_ID = "select FileID, LegacyID, [Source], Filename, FileLength, FileUri, WhoUploaded, DateCreated from [FileStorage_TempFiles] where LegacyID=@legacyid AND [Source]=@source";

		// Token: 0x040002E4 RID: 740
		internal const string SQ_GET_FILE_BY_ID = "select FileData from FileStorage_FilesData where FileID=@fileid";

		// Token: 0x040002E5 RID: 741
		internal const string SQ_GET_TEMP_FILE_BY_ID = "select FileData from FileStorage_TempFilesData where FileID=@fileid";

		// Token: 0x040002E6 RID: 742
		internal const string IQ_INSERT_FILE_INFO = "insert into FileStorage_Files (FileID, Filename, FileLength, WhoUploaded, LegacyID, [Source], FileUri) values ( @fileid, @filename, @filelen, @whouploaded, @legacyid, @source, @fileuri)";

		// Token: 0x040002E7 RID: 743
		internal const string IQ_INSERT_TEMP_FILE_INFO = "insert into FileStorage_TempFiles (FileID, Filename, FileLength, WhoUploaded, LegacyID, [Source], FileUri) values ( @fileid, @filename, @filelen, @whouploaded, @legacyid, @source, @fileuri)";

		// Token: 0x040002E8 RID: 744
		internal const string IQ_UPLOAD_FILE = "if not exists (select 1 from FileStorage_FilesData where FileID=@fileid)\r\n\tbegin\r\n\t\tinsert into FileStorage_FilesData (FileID, FileData, LegacyID, Source) values ( @fileid, @filedata, @filelegacyid, @filesource)\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tupdate FileStorage_FilesData set FileData=@filedata, LegacyID=@filelegacyid, Source=@filesource where FileID=@fileid\r\n\tend";

		// Token: 0x040002E9 RID: 745
		internal const string IQ_UPLOAD_TEMP_FILE = "insert into FileStorage_TempFilesData (FileID, FileData) values ( @fileid, @filedata)";

		// Token: 0x040002EA RID: 746
		internal const string DQ_DELETE_FILE_DATA = "delete from FileStorage_FilesData where FileID=@fileid";

		// Token: 0x040002EB RID: 747
		internal const string DQ_DELETE_TEMP_FILE_DATA = "delete from FileStorage_TempFilesData where FileID=@fileid";

		// Token: 0x040002EC RID: 748
		internal const string DQ_DELETE_FILE_INFO = "delete from FileStorage_Files where FileID = @fileid";

		// Token: 0x040002ED RID: 749
		internal const string DQ_DELETE_TEMP_FILE_INFO = "delete from FileStorage_TempFiles where FileID = @fileid";
	}
}
