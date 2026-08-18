using System;
using System.Configuration;
using System.Web;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001342 RID: 4930
	internal class Configuration
	{
		// Token: 0x17004207 RID: 16903
		// (get) Token: 0x0600CD81 RID: 52609 RVA: 0x002DBF08 File Offset: 0x002DA108
		public static int ChunkSize
		{
			get
			{
				if (Configuration._chunkSize < 0)
				{
					string text = ConfigurationManager.AppSettings["Telerik.RadUpload.TelerikInternalChunkSize"];
					Configuration._chunkSize = ((text != null && Utility.IsInteger(text)) ? int.Parse(text) : Configuration.DEFAULT_CHUNK_SIZE);
					Configuration._chunkSize = Math.Min(Configuration.MAX_CHUNK_SIZE, Configuration._chunkSize);
				}
				return Configuration._chunkSize;
			}
		}

		// Token: 0x17004208 RID: 16904
		// (get) Token: 0x0600CD82 RID: 52610 RVA: 0x002DBF63 File Offset: 0x002DA163
		public static bool IsDefaultChunkSize
		{
			get
			{
				return Configuration.ChunkSize == Configuration.DEFAULT_CHUNK_SIZE;
			}
		}

		// Token: 0x17004209 RID: 16905
		// (get) Token: 0x0600CD83 RID: 52611 RVA: 0x002DBF74 File Offset: 0x002DA174
		public static string TempFolder
		{
			get
			{
				if (Configuration._tempFolder == null)
				{
					string text = ConfigurationManager.AppSettings["Telerik.RadUpload.TempFolder"];
					Configuration._tempFolder = ((text != null) ? text : Configuration.DEFAULT_TEMP_FOLDER);
					if (Configuration._tempFolder.StartsWith("~/"))
					{
						Configuration._tempFolder = HttpContext.Current.Server.MapPath(Configuration._tempFolder);
					}
				}
				return Configuration._tempFolder;
			}
		}

		// Token: 0x1700420A RID: 16906
		// (get) Token: 0x0600CD84 RID: 52612 RVA: 0x002DBFD7 File Offset: 0x002DA1D7
		public static bool IsDefaultTempFolder
		{
			get
			{
				return Configuration.TempFolder == Configuration.DEFAULT_TEMP_FOLDER;
			}
		}

		// Token: 0x040036E6 RID: 14054
		private static readonly int DEFAULT_CHUNK_SIZE = 32768;

		// Token: 0x040036E7 RID: 14055
		private static readonly string DEFAULT_TEMP_FOLDER = string.Empty;

		// Token: 0x040036E8 RID: 14056
		public static readonly int MAX_CHUNK_SIZE = 1000000;

		// Token: 0x040036E9 RID: 14057
		private static int _chunkSize = -1;

		// Token: 0x040036EA RID: 14058
		private static string _tempFolder = null;
	}
}
