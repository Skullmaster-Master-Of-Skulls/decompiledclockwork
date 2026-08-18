using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x02001331 RID: 4913
	public class FileSystemContentProvider : FileBrowserContentProvider
	{
		// Token: 0x0600CD31 RID: 52529 RVA: 0x002DB1D8 File Offset: 0x002D93D8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FileSystemContentProvider(HttpContext context, string[] searchPatterns, string[] viewPaths, string[] uploadPaths, string[] deletePaths, string selectedUrl, string selectedItemTag) : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
		{
			this.ProcessPaths(base.ViewPaths);
			this.ProcessPaths(base.UploadPaths);
			this.ProcessPaths(base.DeletePaths);
			base.SelectedUrl = FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(base.SelectedUrl));
		}

		// Token: 0x0600CD32 RID: 52530 RVA: 0x002DB231 File Offset: 0x002D9431
		protected virtual bool IsValid(FileInfo file)
		{
			return true;
		}

		// Token: 0x0600CD33 RID: 52531 RVA: 0x002DB234 File Offset: 0x002D9434
		protected virtual bool IsValid(DirectoryInfo directory)
		{
			return true;
		}

		// Token: 0x0600CD34 RID: 52532 RVA: 0x002DB238 File Offset: 0x002D9438
		public override DirectoryItem ResolveRootDirectoryAsTree(string path)
		{
			string path2 = this.MapPath(path);
			string virtualName = (path == "/") ? string.Empty : VirtualPathUtility.GetFileName(path);
			string location = (path == "/") ? "/" : VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.GetDirectory(path));
			DirectoryInfo directoryInfo = new DirectoryInfo(path2);
			if (!directoryInfo.Exists)
			{
				return null;
			}
			FileSystemContentProvider.DirectoryLister directoryLister = new FileSystemContentProvider.DirectoryLister(this, false, true);
			return directoryLister.GetDirectory(directoryInfo, virtualName, location, path, string.Empty);
		}

		// Token: 0x0600CD35 RID: 52533 RVA: 0x002DB2B4 File Offset: 0x002D94B4
		public override DirectoryItem ResolveDirectory(string path)
		{
			string path2 = this.MapPath(path);
			DirectoryInfo directoryInfo = new DirectoryInfo(path2);
			if (!directoryInfo.Exists)
			{
				return null;
			}
			FileSystemContentProvider.DirectoryLister directoryLister = new FileSystemContentProvider.DirectoryLister(this, true, false);
			return directoryLister.GetDirectory(directoryInfo, path);
		}

		// Token: 0x170041FC RID: 16892
		// (get) Token: 0x0600CD36 RID: 52534 RVA: 0x002DB2EB File Offset: 0x002D94EB
		public override bool CanCreateDirectory
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600CD37 RID: 52535 RVA: 0x002DB2EE File Offset: 0x002D94EE
		public override string GetFileName(string url)
		{
			return Path.GetFileName(FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(url)));
		}

		// Token: 0x0600CD38 RID: 52536 RVA: 0x002DB304 File Offset: 0x002D9504
		public override string GetPath(string url)
		{
			string virtualPath = FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(url));
			string result;
			try
			{
				result = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.GetDirectory(virtualPath).Replace("\\", "/")));
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600CD39 RID: 52537 RVA: 0x002DB35C File Offset: 0x002D955C
		public override Stream GetFile(string url)
		{
			string path = this.MapPath(FileBrowserContentProvider.RemoveProtocolNameAndServerName(this.GetAbsolutePath(url)));
			if (!File.Exists(path))
			{
				return null;
			}
			return File.OpenRead(path);
		}

		// Token: 0x0600CD3A RID: 52538 RVA: 0x002DB38C File Offset: 0x002D958C
		public override string StoreBitmap(Bitmap bitmap, string url, ImageFormat format)
		{
			bitmap.Save(this.MapPath(FileBrowserContentProvider.RemoveProtocolNameAndServerName(url)), format);
			return url;
		}

		// Token: 0x0600CD3B RID: 52539 RVA: 0x002DB3A2 File Offset: 0x002D95A2
		[Obsolete("Please use the other overload of StoreFile()")]
		public override string StoreFile(HttpPostedFile file, string path, string name, params string[] arguments)
		{
			return this.StoreFile(new PostedFile(string.Empty, file), path, name, arguments);
		}

		// Token: 0x0600CD3C RID: 52540 RVA: 0x002DB3BC File Offset: 0x002D95BC
		public override string StoreFile(UploadedFile file, string path, string name, params string[] arguments)
		{
			string text = Path.Combine(path, name);
			text = text.Replace('\\', '/');
			string fileName = this.MapPath(text);
			file.SaveAs(fileName);
			return text;
		}

		// Token: 0x0600CD3D RID: 52541 RVA: 0x002DB3EC File Offset: 0x002D95EC
		public override string DeleteFile(string path)
		{
			string path2 = this.MapPath(FileBrowserContentProvider.RemoveProtocolNameAndServerName(path));
			try
			{
				if (File.Exists(path2))
				{
					if ((File.GetAttributes(path2) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						return "FileReadOnly";
					}
					File.Delete(path2);
				}
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToDeleteFile";
			}
			return string.Empty;
		}

		// Token: 0x0600CD3E RID: 52542 RVA: 0x002DB44C File Offset: 0x002D964C
		public override string DeleteDirectory(string path)
		{
			string path2 = this.MapPath(path);
			try
			{
				if (Directory.Exists(path2))
				{
					Directory.Delete(path2, true);
				}
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToDeleteFolder";
			}
			return string.Empty;
		}

		// Token: 0x0600CD3F RID: 52543 RVA: 0x002DB494 File Offset: 0x002D9694
		public override string CreateDirectory(string path, string name)
		{
			if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				return "InvalidCharactersInPath";
			}
			string path2 = this.MapPath(path + name);
			if (Directory.Exists(path2))
			{
				return "NameExists";
			}
			try
			{
				Directory.CreateDirectory(path2);
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToCreateFolder";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}

		// Token: 0x0600CD40 RID: 52544 RVA: 0x002DB510 File Offset: 0x002D9710
		public override string MoveDirectory(string path, string newPath)
		{
			try
			{
				string sourceDirName = this.MapPath(path);
				string text = this.MapPath(VirtualPathUtility.AppendTrailingSlash(newPath));
				if (Directory.Exists(text))
				{
					return "NameExists";
				}
				Directory.Move(sourceDirName, text);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}

		// Token: 0x0600CD41 RID: 52545 RVA: 0x002DB56C File Offset: 0x002D976C
		public override string MoveFile(string path, string newPath)
		{
			try
			{
				string text = this.MapPath(path);
				string text2 = this.MapPath(newPath);
				if (!File.Exists(text))
				{
					return "FileNotFound";
				}
				if (File.Exists(text2))
				{
					return "FileExists";
				}
				File.Move(text, text2);
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToMoveFile";
			}
			catch (IOException)
			{
				return "NameExists";
			}
			return string.Empty;
		}

		// Token: 0x0600CD42 RID: 52546 RVA: 0x002DB5E8 File Offset: 0x002D97E8
		public override string CopyDirectory(string path, string newPath)
		{
			if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(newPath))
			{
				return "MessageCannotWriteToFolder";
			}
			string text = this.MapPath(VirtualPathUtility.AppendTrailingSlash(path.Replace('\\', '/')));
			string text2 = this.MapPath(VirtualPathUtility.AppendTrailingSlash(newPath.Replace('\\', '/')));
			if (Directory.Exists(text2))
			{
				return "NameExists";
			}
			try
			{
				Directory.CreateDirectory(text2);
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToCreateFolder";
			}
			foreach (string text3 in Directory.GetDirectories(text, "*.*", SearchOption.AllDirectories))
			{
				string path2 = text3.Remove(0, text.Length).Insert(0, text2);
				if (!Directory.Exists(path2))
				{
					try
					{
						Directory.CreateDirectory(path2);
					}
					catch (UnauthorizedAccessException)
					{
						return "NoPermissionsToCreateFolder";
					}
				}
			}
			foreach (string text4 in Directory.GetFiles(text, "*.*", SearchOption.AllDirectories))
			{
				string destFileName = text4.Remove(0, text.Length).Insert(0, text2);
				try
				{
					File.Copy(text4, destFileName, true);
				}
				catch (UnauthorizedAccessException)
				{
					return "NoPermissionsToMoveFile";
				}
			}
			return string.Empty;
		}

		// Token: 0x0600CD43 RID: 52547 RVA: 0x002DB738 File Offset: 0x002D9938
		public override string CopyFile(string path, string newPath)
		{
			string text = this.MapPath(path);
			string text2 = this.MapPath(newPath);
			if (!File.Exists(text))
			{
				return "FileNotFound";
			}
			if (File.Exists(text2))
			{
				return "FileExists";
			}
			try
			{
				File.Copy(text, text2);
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToMoveFile";
			}
			return string.Empty;
		}

		// Token: 0x0600CD44 RID: 52548 RVA: 0x002DB79C File Offset: 0x002D999C
		public void ProcessPaths(string[] paths)
		{
			for (int i = 0; i < paths.Length; i++)
			{
				paths[i] = this.GetAbsolutePath(paths[i]);
			}
		}

		// Token: 0x0600CD45 RID: 52549 RVA: 0x002DB7C3 File Offset: 0x002D99C3
		protected virtual string GetAbsolutePath(string path)
		{
			path = path.Replace("~/", VirtualPathUtility.AppendTrailingSlash(base.Context.Request.ApplicationPath));
			if (!string.IsNullOrEmpty(path))
			{
				path = VirtualPathUtility.RemoveTrailingSlash(path);
			}
			return path;
		}

		// Token: 0x0600CD46 RID: 52550 RVA: 0x002DB7F8 File Offset: 0x002D99F8
		protected static bool IsParentOf(string virtualParent, string virtualChild)
		{
			if (virtualChild.StartsWith(virtualParent, StringComparison.OrdinalIgnoreCase))
			{
				if (virtualParent.Length == virtualChild.Length)
				{
					return true;
				}
				int length = virtualParent.Length;
				if (virtualChild[length] == '/' || virtualChild[length] == '\\' || virtualChild[length - 1] == '/' || virtualChild[length - 1] == '\\')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CD47 RID: 52551 RVA: 0x002DB85C File Offset: 0x002D9A5C
		protected PathPermissions GetPermissions(string path)
		{
			PathPermissions pathPermissions = PathPermissions.Read;
			if (this.CanUpload(path))
			{
				pathPermissions |= PathPermissions.Upload;
			}
			if (this.CanDelete(path))
			{
				pathPermissions |= PathPermissions.Delete;
			}
			return pathPermissions;
		}

		// Token: 0x0600CD48 RID: 52552 RVA: 0x002DB888 File Offset: 0x002D9A88
		protected bool CanUpload(string path)
		{
			foreach (string virtualParent in base.UploadPaths)
			{
				if (FileSystemContentProvider.IsParentOf(virtualParent, path))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CD49 RID: 52553 RVA: 0x002DB8C0 File Offset: 0x002D9AC0
		protected bool CanDelete(string path)
		{
			foreach (string virtualParent in base.DeletePaths)
			{
				if (FileSystemContentProvider.IsParentOf(virtualParent, path))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CD4A RID: 52554 RVA: 0x002DB8F8 File Offset: 0x002D9AF8
		protected bool IsInDeletePaths(string folderPath)
		{
			folderPath = folderPath.TrimEnd(new char[]
			{
				this.PathSeparator
			});
			foreach (string text in base.DeletePaths)
			{
				if (!string.IsNullOrEmpty(text) && folderPath.StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					string text2 = text.TrimEnd(new char[]
					{
						this.PathSeparator
					});
					if (folderPath.Length == text2.Length)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600CD4B RID: 52555 RVA: 0x002DB981 File Offset: 0x002D9B81
		protected virtual string MapPath(string path)
		{
			return base.Context.Server.MapPath(path);
		}

		// Token: 0x02001332 RID: 4914
		private class DirectoryLister
		{
			// Token: 0x0600CD4C RID: 52556 RVA: 0x002DB994 File Offset: 0x002D9B94
			public DirectoryLister(FileSystemContentProvider contentProvider, bool includeFiles, bool includeDirectories)
			{
				this._contentProvider = contentProvider;
				this._includeFiles = includeFiles;
				this._includeDirectories = includeDirectories;
			}

			// Token: 0x0600CD4D RID: 52557 RVA: 0x002DB9B4 File Offset: 0x002D9BB4
			protected FileItem[] GetFiles(DirectoryInfo directory, PathPermissions permissions, string location)
			{
				ArrayList arrayList = new ArrayList();
				Hashtable hashtable = new Hashtable();
				foreach (string searchPattern in this._contentProvider.SearchPatterns)
				{
					foreach (FileInfo fileInfo in directory.GetFiles(searchPattern))
					{
						if (!hashtable.ContainsKey(fileInfo.FullName) && this._contentProvider.IsValid(fileInfo))
						{
							hashtable.Add(fileInfo.FullName, string.Empty);
							string location2 = location + fileInfo.Name;
							arrayList.Add(new FileItem(fileInfo.Name, fileInfo.Extension, fileInfo.Length, location2, string.Empty, string.Empty, permissions));
						}
					}
				}
				return (FileItem[])arrayList.ToArray(typeof(FileItem));
			}

			// Token: 0x0600CD4E RID: 52558 RVA: 0x002DBA9C File Offset: 0x002D9C9C
			protected DirectoryItem[] GetDirectories(DirectoryInfo directory, string parentPath)
			{
				DirectoryInfo[] directories = directory.GetDirectories();
				ArrayList arrayList = new ArrayList();
				foreach (DirectoryInfo directoryInfo in directories)
				{
					if (this._contentProvider.IsValid(directoryInfo))
					{
						string text = VirtualPathUtility.AppendTrailingSlash(parentPath) + directoryInfo.Name;
						PathPermissions pathPermissions = this._contentProvider.GetPermissions(text);
						if (this._contentProvider.IsInDeletePaths(text))
						{
							pathPermissions ^= PathPermissions.Delete;
						}
						arrayList.Add(new DirectoryItem(directoryInfo.Name, string.Empty, text, string.Empty, pathPermissions, new FileItem[0], new DirectoryItem[0]));
					}
				}
				return (DirectoryItem[])arrayList.ToArray(typeof(DirectoryItem));
			}

			// Token: 0x0600CD4F RID: 52559 RVA: 0x002DBB50 File Offset: 0x002D9D50
			public DirectoryItem GetDirectory(DirectoryInfo dir, string virtualName, string location, string fullPath, string tag)
			{
				PathPermissions pathPermissions = this._contentProvider.GetPermissions(fullPath);
				DirectoryItem[] directories = this.IncludeDirectories ? this.GetDirectories(dir, fullPath) : new DirectoryItem[0];
				FileItem[] files = this.IncludeFiles ? this.GetFiles(dir, pathPermissions, fullPath.TrimEnd(new char[]
				{
					'/'
				}) + "/") : new FileItem[0];
				if (this._contentProvider.IsInDeletePaths(fullPath))
				{
					pathPermissions ^= PathPermissions.Delete;
				}
				return new DirectoryItem(virtualName, location, fullPath, tag, pathPermissions, files, directories);
			}

			// Token: 0x0600CD50 RID: 52560 RVA: 0x002DBBDD File Offset: 0x002D9DDD
			public DirectoryItem GetDirectory(DirectoryInfo dir, string fullPath)
			{
				return this.GetDirectory(dir, dir.Name, string.Empty, fullPath, string.Empty);
			}

			// Token: 0x170041FD RID: 16893
			// (get) Token: 0x0600CD51 RID: 52561 RVA: 0x002DBBF7 File Offset: 0x002D9DF7
			protected bool IncludeFiles
			{
				get
				{
					return this._includeFiles;
				}
			}

			// Token: 0x170041FE RID: 16894
			// (get) Token: 0x0600CD52 RID: 52562 RVA: 0x002DBBFF File Offset: 0x002D9DFF
			protected bool IncludeDirectories
			{
				get
				{
					return this._includeDirectories;
				}
			}

			// Token: 0x040036B5 RID: 14005
			private FileSystemContentProvider _contentProvider;

			// Token: 0x040036B6 RID: 14006
			private bool _includeFiles;

			// Token: 0x040036B7 RID: 14007
			private bool _includeDirectories;
		}
	}
}
