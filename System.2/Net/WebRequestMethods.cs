using System;

namespace System.Net
{
	// Token: 0x020000EB RID: 235
	public static class WebRequestMethods
	{
		// Token: 0x020006F8 RID: 1784
		public static class Ftp
		{
			// Token: 0x040030AC RID: 12460
			public const string DownloadFile = "RETR";

			// Token: 0x040030AD RID: 12461
			public const string ListDirectory = "NLST";

			// Token: 0x040030AE RID: 12462
			public const string UploadFile = "STOR";

			// Token: 0x040030AF RID: 12463
			public const string DeleteFile = "DELE";

			// Token: 0x040030B0 RID: 12464
			public const string AppendFile = "APPE";

			// Token: 0x040030B1 RID: 12465
			public const string GetFileSize = "SIZE";

			// Token: 0x040030B2 RID: 12466
			public const string UploadFileWithUniqueName = "STOU";

			// Token: 0x040030B3 RID: 12467
			public const string MakeDirectory = "MKD";

			// Token: 0x040030B4 RID: 12468
			public const string RemoveDirectory = "RMD";

			// Token: 0x040030B5 RID: 12469
			public const string ListDirectoryDetails = "LIST";

			// Token: 0x040030B6 RID: 12470
			public const string GetDateTimestamp = "MDTM";

			// Token: 0x040030B7 RID: 12471
			public const string PrintWorkingDirectory = "PWD";

			// Token: 0x040030B8 RID: 12472
			public const string Rename = "RENAME";
		}

		// Token: 0x020006F9 RID: 1785
		public static class Http
		{
			// Token: 0x040030B9 RID: 12473
			public const string Get = "GET";

			// Token: 0x040030BA RID: 12474
			public const string Connect = "CONNECT";

			// Token: 0x040030BB RID: 12475
			public const string Head = "HEAD";

			// Token: 0x040030BC RID: 12476
			public const string Put = "PUT";

			// Token: 0x040030BD RID: 12477
			public const string Post = "POST";

			// Token: 0x040030BE RID: 12478
			public const string MkCol = "MKCOL";
		}

		// Token: 0x020006FA RID: 1786
		public static class File
		{
			// Token: 0x040030BF RID: 12479
			public const string DownloadFile = "GET";

			// Token: 0x040030C0 RID: 12480
			public const string UploadFile = "PUT";
		}
	}
}
