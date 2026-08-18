using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001065 RID: 4197
	[Serializable]
	public class FileManagerDialogParameters : DialogParameters
	{
		// Token: 0x0600A952 RID: 43346 RVA: 0x0024C680 File Offset: 0x0024A880
		public FileManagerDialogParameters()
		{
		}

		// Token: 0x0600A953 RID: 43347 RVA: 0x0024C688 File Offset: 0x0024A888
		protected FileManagerDialogParameters(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A954 RID: 43348 RVA: 0x0024C694 File Offset: 0x0024A894
		public static FileManagerDialogParameters Convert(DialogParameters dialogParameters)
		{
			FileManagerDialogParameters fileManagerDialogParameters = new FileManagerDialogParameters();
			foreach (object key in dialogParameters.Keys)
			{
				fileManagerDialogParameters[key] = dialogParameters[key];
			}
			return fileManagerDialogParameters;
		}

		// Token: 0x1700365C RID: 13916
		// (get) Token: 0x0600A955 RID: 43349 RVA: 0x0024C6F8 File Offset: 0x0024A8F8
		// (set) Token: 0x0600A956 RID: 43350 RVA: 0x0024C705 File Offset: 0x0024A905
		public string[] ViewPaths
		{
			get
			{
				return this.GetDefaultingStringArray("ViewPaths");
			}
			set
			{
				this.SetDefaultingStringArray("ViewPaths", value);
			}
		}

		// Token: 0x1700365D RID: 13917
		// (get) Token: 0x0600A957 RID: 43351 RVA: 0x0024C713 File Offset: 0x0024A913
		// (set) Token: 0x0600A958 RID: 43352 RVA: 0x0024C720 File Offset: 0x0024A920
		public string[] UploadPaths
		{
			get
			{
				return this.GetDefaultingStringArray("UploadPaths");
			}
			set
			{
				this.SetDefaultingStringArray("UploadPaths", value);
			}
		}

		// Token: 0x1700365E RID: 13918
		// (get) Token: 0x0600A959 RID: 43353 RVA: 0x0024C72E File Offset: 0x0024A92E
		// (set) Token: 0x0600A95A RID: 43354 RVA: 0x0024C73B File Offset: 0x0024A93B
		public string[] DeletePaths
		{
			get
			{
				return this.GetDefaultingStringArray("DeletePaths");
			}
			set
			{
				this.SetDefaultingStringArray("DeletePaths", value);
			}
		}

		// Token: 0x1700365F RID: 13919
		// (get) Token: 0x0600A95B RID: 43355 RVA: 0x0024C749 File Offset: 0x0024A949
		// (set) Token: 0x0600A95C RID: 43356 RVA: 0x0024C756 File Offset: 0x0024A956
		public string[] SearchPatterns
		{
			get
			{
				return this.GetDefaultingStringArray("SearchPatterns");
			}
			set
			{
				this.SetDefaultingStringArray("SearchPatterns", value);
			}
		}

		// Token: 0x0600A95D RID: 43357 RVA: 0x0024C764 File Offset: 0x0024A964
		private string[] GetDefaultingStringArray(string key)
		{
			if (this[key] == null)
			{
				return new string[0];
			}
			return (string[])this[key];
		}

		// Token: 0x0600A95E RID: 43358 RVA: 0x0024C782 File Offset: 0x0024A982
		private void SetDefaultingStringArray(string key, string[] value)
		{
			if (value == null && this.ContainsKey(key))
			{
				this.Remove(key);
				return;
			}
			this[key] = value;
		}

		// Token: 0x17003660 RID: 13920
		// (get) Token: 0x0600A95F RID: 43359 RVA: 0x0024C7A0 File Offset: 0x0024A9A0
		// (set) Token: 0x0600A960 RID: 43360 RVA: 0x0024C7C1 File Offset: 0x0024A9C1
		public int MaxUploadFileSize
		{
			get
			{
				if (this["MaxUploadFileSize"] != null)
				{
					return (int)this["MaxUploadFileSize"];
				}
				return 0;
			}
			set
			{
				this["MaxUploadFileSize"] = value;
			}
		}

		// Token: 0x17003661 RID: 13921
		// (get) Token: 0x0600A961 RID: 43361 RVA: 0x0024C7D4 File Offset: 0x0024A9D4
		// (set) Token: 0x0600A962 RID: 43362 RVA: 0x0024C7E6 File Offset: 0x0024A9E6
		public string FileBrowserContentProviderTypeName
		{
			get
			{
				return (string)this["FileBrowserContentProviderTypeName"];
			}
			set
			{
				this["FileBrowserContentProviderTypeName"] = value;
			}
		}

		// Token: 0x17003662 RID: 13922
		// (get) Token: 0x0600A963 RID: 43363 RVA: 0x0024C7F4 File Offset: 0x0024A9F4
		// (set) Token: 0x0600A964 RID: 43364 RVA: 0x0024C810 File Offset: 0x0024AA10
		public bool EnableAsyncUpload
		{
			get
			{
				return (bool)(this["EnableAsyncUpload"] ?? false);
			}
			set
			{
				this["EnableAsyncUpload"] = value;
			}
		}

		// Token: 0x17003663 RID: 13923
		// (get) Token: 0x0600A965 RID: 43365 RVA: 0x0024C823 File Offset: 0x0024AA23
		// (set) Token: 0x0600A966 RID: 43366 RVA: 0x0024C83F File Offset: 0x0024AA3F
		public bool AllowMultipleSelection
		{
			get
			{
				return (bool)(this["AllowMultipleSelection"] ?? false);
			}
			set
			{
				this["AllowMultipleSelection"] = value;
			}
		}

		// Token: 0x17003664 RID: 13924
		// (get) Token: 0x0600A967 RID: 43367 RVA: 0x0024C852 File Offset: 0x0024AA52
		// (set) Token: 0x0600A968 RID: 43368 RVA: 0x0024C86E File Offset: 0x0024AA6E
		public RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(this["RenderMode"] ?? RenderMode.Classic);
			}
			set
			{
				this["RenderMode"] = value;
			}
		}
	}
}
