using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x020005B6 RID: 1462
	[ComVisible(true)]
	[Serializable]
	public sealed class FileInfo : FileSystemInfo
	{
		// Token: 0x060035EB RID: 13803 RVA: 0x000B3EF0 File Offset: 0x000B2EF0
		public FileInfo(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.OriginalPath = fileName;
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			new FileIOPermission(FileIOPermissionAccess.Read, new string[]
			{
				fullPathInternal
			}, false, false).Demand();
			this._name = Path.GetFileName(fileName);
			this.FullPath = fullPathInternal;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000B3F4C File Offset: 0x000B2F4C
		private FileInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			new FileIOPermission(FileIOPermissionAccess.Read, new string[]
			{
				this.FullPath
			}, false, false).Demand();
			this._name = Path.GetFileName(this.OriginalPath);
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000B3F90 File Offset: 0x000B2F90
		internal FileInfo(string fullPath, bool ignoreThis)
		{
			this._name = Path.GetFileName(fullPath);
			this.OriginalPath = this._name;
			this.FullPath = fullPath;
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060035EE RID: 13806 RVA: 0x000B3FB7 File Offset: 0x000B2FB7
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000B3FC0 File Offset: 0x000B2FC0
		public long Length
		{
			get
			{
				if (this._dataInitialised == -1)
				{
					base.Refresh();
				}
				if (this._dataInitialised != 0)
				{
					__Error.WinIOError(this._dataInitialised, this.OriginalPath);
				}
				if ((this._data.fileAttributes & 16) != 0)
				{
					__Error.WinIOError(2, this.OriginalPath);
				}
				return (long)this._data.fileSizeHigh << 32 | ((long)this._data.fileSizeLow & (long)((ulong)-1));
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000B4030 File Offset: 0x000B3030
		public string DirectoryName
		{
			get
			{
				string directoryName = Path.GetDirectoryName(this.FullPath);
				if (directoryName != null)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[]
					{
						directoryName
					}, false, false).Demand();
				}
				return directoryName;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000B4068 File Offset: 0x000B3068
		public DirectoryInfo Directory
		{
			get
			{
				string directoryName = this.DirectoryName;
				if (directoryName == null)
				{
					return null;
				}
				return new DirectoryInfo(directoryName);
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000B4087 File Offset: 0x000B3087
		// (set) Token: 0x060035F3 RID: 13811 RVA: 0x000B4097 File Offset: 0x000B3097
		public bool IsReadOnly
		{
			get
			{
				return (base.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0;
			}
			set
			{
				if (value)
				{
					base.Attributes |= FileAttributes.ReadOnly;
					return;
				}
				base.Attributes &= ~FileAttributes.ReadOnly;
			}
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000B40BA File Offset: 0x000B30BA
		public FileSecurity GetAccessControl()
		{
			return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000B40C9 File Offset: 0x000B30C9
		public FileSecurity GetAccessControl(AccessControlSections includeSections)
		{
			return File.GetAccessControl(this.FullPath, includeSections);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000B40D7 File Offset: 0x000B30D7
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			File.SetAccessControl(this.FullPath, fileSecurity);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000B40E5 File Offset: 0x000B30E5
		public StreamReader OpenText()
		{
			return new StreamReader(this.FullPath, Encoding.UTF8, true, 1024);
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000B40FD File Offset: 0x000B30FD
		public StreamWriter CreateText()
		{
			return new StreamWriter(this.FullPath, false);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000B410B File Offset: 0x000B310B
		public StreamWriter AppendText()
		{
			return new StreamWriter(this.FullPath, true);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000B4119 File Offset: 0x000B3119
		public FileInfo CopyTo(string destFileName)
		{
			return this.CopyTo(destFileName, false);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000B4123 File Offset: 0x000B3123
		public FileInfo CopyTo(string destFileName, bool overwrite)
		{
			destFileName = File.InternalCopy(this.FullPath, destFileName, overwrite);
			return new FileInfo(destFileName, false);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000B413B File Offset: 0x000B313B
		public FileStream Create()
		{
			return File.Create(this.FullPath);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000B4148 File Offset: 0x000B3148
		public override void Delete()
		{
			new FileIOPermission(FileIOPermissionAccess.Write, new string[]
			{
				this.FullPath
			}, false, false).Demand();
			if (Environment.IsWin9X() && System.IO.Directory.InternalExists(this.FullPath))
			{
				throw new UnauthorizedAccessException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("UnauthorizedAccess_IODenied_Path"), new object[]
				{
					this.OriginalPath
				}));
			}
			if (!Win32Native.DeleteFile(this.FullPath))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					return;
				}
				__Error.WinIOError(lastWin32Error, this.OriginalPath);
			}
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000B41D8 File Offset: 0x000B31D8
		[ComVisible(false)]
		public void Decrypt()
		{
			File.Decrypt(this.FullPath);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000B41E5 File Offset: 0x000B31E5
		[ComVisible(false)]
		public void Encrypt()
		{
			File.Encrypt(this.FullPath);
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000B41F4 File Offset: 0x000B31F4
		public override bool Exists
		{
			get
			{
				bool result;
				try
				{
					if (this._dataInitialised == -1)
					{
						base.Refresh();
					}
					if (this._dataInitialised != 0)
					{
						result = false;
					}
					else
					{
						result = ((this._data.fileAttributes & 16) == 0);
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000B4248 File Offset: 0x000B3248
		public FileStream Open(FileMode mode)
		{
			return this.Open(mode, FileAccess.ReadWrite, FileShare.None);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000B4253 File Offset: 0x000B3253
		public FileStream Open(FileMode mode, FileAccess access)
		{
			return this.Open(mode, access, FileShare.None);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000B425E File Offset: 0x000B325E
		public FileStream Open(FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(this.FullPath, mode, access, share);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000B426E File Offset: 0x000B326E
		public FileStream OpenRead()
		{
			return new FileStream(this.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000B427E File Offset: 0x000B327E
		public FileStream OpenWrite()
		{
			return new FileStream(this.FullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000B4290 File Offset: 0x000B3290
		public void MoveTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[]
			{
				this.FullPath
			}, false, false).Demand();
			string fullPathInternal = Path.GetFullPathInternal(destFileName);
			new FileIOPermission(FileIOPermissionAccess.Write, new string[]
			{
				fullPathInternal
			}, false, false).Demand();
			if (!Win32Native.MoveFile(this.FullPath, fullPathInternal))
			{
				__Error.WinIOError();
			}
			this.FullPath = fullPathInternal;
			this.OriginalPath = destFileName;
			this._name = Path.GetFileName(fullPathInternal);
			this._dataInitialised = -1;
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000B433A File Offset: 0x000B333A
		[ComVisible(false)]
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
		{
			return this.Replace(destinationFileName, destinationBackupFileName, false);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000B4345 File Offset: 0x000B3345
		[ComVisible(false)]
		public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			File.Replace(this.FullPath, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
			return new FileInfo(destinationFileName);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000B435B File Offset: 0x000B335B
		public override string ToString()
		{
			return this.OriginalPath;
		}

		// Token: 0x04001C36 RID: 7222
		private string _name;
	}
}
