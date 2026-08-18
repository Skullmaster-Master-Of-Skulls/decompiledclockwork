using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x0200132D RID: 4909
	public abstract class FileBrowserContentProvider
	{
		// Token: 0x0600CCFF RID: 52479 RVA: 0x002DAD73 File Offset: 0x002D8F73
		protected FileBrowserContentProvider(HttpContext context, string[] searchPatterns, string[] viewPaths, string[] uploadPaths, string[] deletePaths, string selectedUrl, string selectedItemTag) : this()
		{
			this._context = context;
			this._searchPatterns = searchPatterns;
			this._viewPaths = viewPaths;
			this._uploadPaths = uploadPaths;
			this._deletePaths = deletePaths;
			this._selectedUrl = selectedUrl;
			this._selectedItemTag = selectedItemTag;
		}

		// Token: 0x0600CD00 RID: 52480 RVA: 0x002DADB0 File Offset: 0x002D8FB0
		public string NormalizeRelativePath(string path)
		{
			FilePath filePath = new FilePath(this.PathSeparator);
			return filePath.NormalizeRelativePath(path);
		}

		// Token: 0x0600CD01 RID: 52481 RVA: 0x002DADD0 File Offset: 0x002D8FD0
		[Obsolete("This method is no longer used. Only Tree display mode is supported.")]
		public virtual DirectoryItem[] ResolveRootDirectoryAsList(string path)
		{
			return null;
		}

		// Token: 0x0600CD02 RID: 52482
		public abstract DirectoryItem ResolveRootDirectoryAsTree(string path);

		// Token: 0x0600CD03 RID: 52483
		public abstract DirectoryItem ResolveDirectory(string path);

		// Token: 0x0600CD04 RID: 52484
		public abstract string GetFileName(string url);

		// Token: 0x0600CD05 RID: 52485
		public abstract string GetPath(string url);

		// Token: 0x0600CD06 RID: 52486
		public abstract Stream GetFile(string url);

		// Token: 0x0600CD07 RID: 52487
		public abstract string StoreBitmap(Bitmap bitmap, string url, ImageFormat format);

		// Token: 0x0600CD08 RID: 52488 RVA: 0x002DADD3 File Offset: 0x002D8FD3
		[Obsolete("Please use the other overload of StoreFile()")]
		public virtual string StoreFile(HttpPostedFile file, string path, string name, params string[] arguments)
		{
			return string.Empty;
		}

		// Token: 0x0600CD09 RID: 52489
		public abstract string StoreFile(UploadedFile file, string path, string name, params string[] arguments);

		// Token: 0x0600CD0A RID: 52490
		public abstract string DeleteFile(string path);

		// Token: 0x0600CD0B RID: 52491
		public abstract string DeleteDirectory(string path);

		// Token: 0x0600CD0C RID: 52492
		public abstract string CreateDirectory(string path, string name);

		// Token: 0x0600CD0D RID: 52493 RVA: 0x002DADDA File Offset: 0x002D8FDA
		public virtual string MoveFile(string path, string newPath)
		{
			return string.Empty;
		}

		// Token: 0x0600CD0E RID: 52494 RVA: 0x002DADE1 File Offset: 0x002D8FE1
		public virtual string MoveDirectory(string path, string newPath)
		{
			return string.Empty;
		}

		// Token: 0x0600CD0F RID: 52495 RVA: 0x002DADE8 File Offset: 0x002D8FE8
		public virtual string CopyFile(string path, string newPath)
		{
			return string.Empty;
		}

		// Token: 0x0600CD10 RID: 52496 RVA: 0x002DADEF File Offset: 0x002D8FEF
		public virtual string CopyDirectory(string path, string newPath)
		{
			return string.Empty;
		}

		// Token: 0x0600CD11 RID: 52497 RVA: 0x002DADF8 File Offset: 0x002D8FF8
		public virtual FileItem GetFileItem(string path)
		{
			string path2 = this.GetPath(path);
			DirectoryItem directoryItem = this.ResolveDirectory(path2);
			if (directoryItem != null)
			{
				foreach (FileItem fileItem in directoryItem.Files)
				{
					if (path.EndsWith(fileItem.Name))
					{
						return fileItem;
					}
				}
			}
			return null;
		}

		// Token: 0x0600CD12 RID: 52498 RVA: 0x002DAE50 File Offset: 0x002D9050
		public FileBrowserRoot ResolveViewPaths()
		{
			ArrayList arrayList = new ArrayList();
			foreach (string path in this.ViewPaths)
			{
				DirectoryItem directoryItem = this.ResolveRootDirectoryAsTree(path);
				if (directoryItem != null)
				{
					arrayList.Add(directoryItem);
				}
			}
			return new FileBrowserRoot((DirectoryItem[])arrayList.ToArray(typeof(DirectoryItem)));
		}

		// Token: 0x170041EE RID: 16878
		// (get) Token: 0x0600CD13 RID: 52499 RVA: 0x002DAEAE File Offset: 0x002D90AE
		public virtual bool CanCreateDirectory
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170041EF RID: 16879
		// (get) Token: 0x0600CD14 RID: 52500 RVA: 0x002DAEB1 File Offset: 0x002D90B1
		protected HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x170041F0 RID: 16880
		// (get) Token: 0x0600CD15 RID: 52501 RVA: 0x002DAEB9 File Offset: 0x002D90B9
		// (set) Token: 0x0600CD16 RID: 52502 RVA: 0x002DAEC1 File Offset: 0x002D90C1
		protected string SelectedUrl
		{
			get
			{
				return this._selectedUrl;
			}
			set
			{
				this._selectedUrl = value;
			}
		}

		// Token: 0x170041F1 RID: 16881
		// (get) Token: 0x0600CD17 RID: 52503 RVA: 0x002DAECA File Offset: 0x002D90CA
		// (set) Token: 0x0600CD18 RID: 52504 RVA: 0x002DAED2 File Offset: 0x002D90D2
		[Obsolete("This property is no longer used")]
		protected string SelectedItemTag
		{
			get
			{
				return this._selectedItemTag;
			}
			set
			{
				this._selectedItemTag = value;
			}
		}

		// Token: 0x170041F2 RID: 16882
		// (get) Token: 0x0600CD19 RID: 52505 RVA: 0x002DAEDB File Offset: 0x002D90DB
		protected string[] SearchPatterns
		{
			get
			{
				return this._searchPatterns;
			}
		}

		// Token: 0x170041F3 RID: 16883
		// (get) Token: 0x0600CD1A RID: 52506 RVA: 0x002DAEE3 File Offset: 0x002D90E3
		protected string[] ViewPaths
		{
			get
			{
				return this._viewPaths;
			}
		}

		// Token: 0x170041F4 RID: 16884
		// (get) Token: 0x0600CD1B RID: 52507 RVA: 0x002DAEEB File Offset: 0x002D90EB
		protected string[] UploadPaths
		{
			get
			{
				return this._uploadPaths;
			}
		}

		// Token: 0x170041F5 RID: 16885
		// (get) Token: 0x0600CD1C RID: 52508 RVA: 0x002DAEF3 File Offset: 0x002D90F3
		protected string[] DeletePaths
		{
			get
			{
				return this._deletePaths;
			}
		}

		// Token: 0x0600CD1D RID: 52509 RVA: 0x002DAEFC File Offset: 0x002D90FC
		public static string RemoveProtocolNameAndServerName(string url)
		{
			int num = url.IndexOf("//", StringComparison.OrdinalIgnoreCase);
			if (num >= 0)
			{
				num = url.IndexOf("/", num + 2, StringComparison.OrdinalIgnoreCase);
				if (num >= 0)
				{
					return url.Substring(num);
				}
			}
			return url;
		}

		// Token: 0x0600CD1E RID: 52510 RVA: 0x002DAF38 File Offset: 0x002D9138
		private bool CheckPermissionsInternal(string folderPath, PathPermissions permToCheck)
		{
			folderPath = folderPath.TrimEnd(new char[]
			{
				this.PathSeparator
			}) + this.PathSeparator;
			folderPath = this.NormalizeRelativePath(folderPath);
			string[] array;
			if ((permToCheck & PathPermissions.Upload) != (PathPermissions)0)
			{
				array = this.UploadPaths;
			}
			else if ((permToCheck & PathPermissions.Delete) != (PathPermissions)0)
			{
				array = this.DeletePaths;
			}
			else
			{
				array = this.ViewPaths;
			}
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text) && folderPath.StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					string text2 = text.TrimEnd(new char[]
					{
						this.PathSeparator
					});
					bool result;
					if (text2.Length == 0)
					{
						result = true;
					}
					else
					{
						if (folderPath.Length <= text2.Length || folderPath[text2.Length] != this.PathSeparator)
						{
							goto IL_C5;
						}
						result = true;
					}
					return result;
				}
				IL_C5:;
			}
			return false;
		}

		// Token: 0x0600CD1F RID: 52511 RVA: 0x002DB01B File Offset: 0x002D921B
		public virtual bool CheckReadPermissions(string folderPath)
		{
			return this.CheckPermissionsInternal(folderPath, PathPermissions.Read);
		}

		// Token: 0x0600CD20 RID: 52512 RVA: 0x002DB025 File Offset: 0x002D9225
		public virtual bool CheckDeletePermissions(string folderPath)
		{
			return this.CheckPermissionsInternal(folderPath, PathPermissions.Delete);
		}

		// Token: 0x0600CD21 RID: 52513 RVA: 0x002DB02F File Offset: 0x002D922F
		public virtual bool CheckWritePermissions(string folderPath)
		{
			return this.CheckPermissionsInternal(folderPath, PathPermissions.Upload);
		}

		// Token: 0x170041F6 RID: 16886
		// (get) Token: 0x0600CD22 RID: 52514 RVA: 0x002DB039 File Offset: 0x002D9239
		public virtual char PathSeparator
		{
			get
			{
				return '/';
			}
		}

		// Token: 0x0600CD23 RID: 52515 RVA: 0x002DB03D File Offset: 0x002D923D
		protected FileBrowserContentProvider()
		{
		}

		// Token: 0x040036A7 RID: 13991
		private readonly HttpContext _context;

		// Token: 0x040036A8 RID: 13992
		private readonly string[] _searchPatterns;

		// Token: 0x040036A9 RID: 13993
		private readonly string[] _viewPaths;

		// Token: 0x040036AA RID: 13994
		private readonly string[] _uploadPaths;

		// Token: 0x040036AB RID: 13995
		private readonly string[] _deletePaths;

		// Token: 0x040036AC RID: 13996
		private string _selectedUrl = string.Empty;

		// Token: 0x040036AD RID: 13997
		private string _selectedItemTag = string.Empty;
	}
}
