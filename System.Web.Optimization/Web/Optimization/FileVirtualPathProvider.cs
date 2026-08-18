using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x02000018 RID: 24
	internal class FileVirtualPathProvider : VirtualPathProvider
	{
		// Token: 0x060000DC RID: 220 RVA: 0x0000429A File Offset: 0x0000249A
		public FileVirtualPathProvider(string applicationPath)
		{
			if (string.IsNullOrEmpty(applicationPath))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("applicationPath");
			}
			this.ApplicationPath = applicationPath;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000042C3 File Offset: 0x000024C3
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000042CB File Offset: 0x000024CB
		public string ApplicationPath { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000042D4 File Offset: 0x000024D4
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000042DC File Offset: 0x000024DC
		internal bool EnsureExists
		{
			get
			{
				return this._ensureExists;
			}
			set
			{
				this._ensureExists = value;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000042E8 File Offset: 0x000024E8
		public string MapPath(string virtualPath)
		{
			string oldValue = this.ApplicationPath.EndsWith("/", StringComparison.OrdinalIgnoreCase) ? "~/" : "~";
			return virtualPath.Replace(oldValue, this.ApplicationPath);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004322 File Offset: 0x00002522
		public override bool FileExists(string virtualPath)
		{
			return !this.EnsureExists || File.Exists(this.MapPath(virtualPath));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000433A File Offset: 0x0000253A
		public override bool DirectoryExists(string virtualDir)
		{
			return !this.EnsureExists || Directory.Exists(this.MapPath(virtualDir));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004354 File Offset: 0x00002554
		public override VirtualFile GetFile(string virtualPath)
		{
			string text = this.MapPath(virtualPath);
			return new FileVirtualPathProvider.FileInfoVirtualFile(text, new FileInfo(text));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004378 File Offset: 0x00002578
		public override VirtualDirectory GetDirectory(string virtualDir)
		{
			string text = this.MapPath(virtualDir);
			return new FileVirtualPathProvider.DirectoryInfoVirtualDirectory(text, new DirectoryInfo(text));
		}

		// Token: 0x04000047 RID: 71
		private bool _ensureExists = true;

		// Token: 0x02000019 RID: 25
		internal class FileInfoVirtualFile : VirtualFile
		{
			// Token: 0x060000E6 RID: 230 RVA: 0x00004399 File Offset: 0x00002599
			public FileInfoVirtualFile(string virtualPath, FileInfo file) : base(virtualPath)
			{
				this.File = file;
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x000043A9 File Offset: 0x000025A9
			// (set) Token: 0x060000E8 RID: 232 RVA: 0x000043B1 File Offset: 0x000025B1
			public FileInfo File { get; set; }

			// Token: 0x060000E9 RID: 233 RVA: 0x000043BA File Offset: 0x000025BA
			public override Stream Open()
			{
				return this.File.OpenRead();
			}
		}

		// Token: 0x0200001A RID: 26
		internal class DirectoryInfoVirtualDirectory : VirtualDirectory
		{
			// Token: 0x060000EA RID: 234 RVA: 0x000043C7 File Offset: 0x000025C7
			public DirectoryInfoVirtualDirectory(string virtualPath, DirectoryInfo directory) : base(virtualPath)
			{
				this.Directory = directory;
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000EB RID: 235 RVA: 0x000043D7 File Offset: 0x000025D7
			// (set) Token: 0x060000EC RID: 236 RVA: 0x000043DF File Offset: 0x000025DF
			public DirectoryInfo Directory { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000ED RID: 237 RVA: 0x000043E8 File Offset: 0x000025E8
			public override IEnumerable Files
			{
				get
				{
					List<VirtualFile> list = new List<VirtualFile>();
					foreach (FileInfo fileInfo in this.Directory.GetFiles())
					{
						list.Add(new FileVirtualPathProvider.FileInfoVirtualFile(fileInfo.FullName, fileInfo));
					}
					return list;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000EE RID: 238 RVA: 0x0000442C File Offset: 0x0000262C
			public override IEnumerable Children
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000EF RID: 239 RVA: 0x00004433 File Offset: 0x00002633
			public override IEnumerable Directories
			{
				get
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}
