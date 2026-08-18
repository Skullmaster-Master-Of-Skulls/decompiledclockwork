using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000685 RID: 1669
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class TempFileCollection : ICollection, IEnumerable, IDisposable
	{
		// Token: 0x06003D7C RID: 15740 RVA: 0x000FC5A1 File Offset: 0x000FA7A1
		public TempFileCollection() : this(null, false)
		{
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000FC5AB File Offset: 0x000FA7AB
		public TempFileCollection(string tempDir) : this(tempDir, false)
		{
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000FC5B8 File Offset: 0x000FA7B8
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		public TempFileCollection(string tempDir, bool keepFiles)
		{
			this.keepFiles = keepFiles;
			this.tempDir = tempDir;
			this.files = new Hashtable(StringComparer.OrdinalIgnoreCase);
			WindowsImpersonationContext impersonation = Executor.RevertImpersonation();
			try
			{
				this.currentIdentity = WindowsIdentity.GetCurrent();
			}
			finally
			{
				Executor.ReImpersonate(impersonation);
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000FC614 File Offset: 0x000FA814
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000FC623 File Offset: 0x000FA823
		protected virtual void Dispose(bool disposing)
		{
			this.Delete();
			this.DeleteHighIntegrityDirectory();
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x000FC634 File Offset: 0x000FA834
		~TempFileCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000FC664 File Offset: 0x000FA864
		public string AddExtension(string fileExtension)
		{
			return this.AddExtension(fileExtension, this.keepFiles);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000FC674 File Offset: 0x000FA874
		public string AddExtension(string fileExtension, bool keepFile)
		{
			if (fileExtension == null || fileExtension.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[]
				{
					"fileExtension"
				}), "fileExtension");
			}
			string text = this.BasePath + "." + fileExtension;
			this.AddFile(text, keepFile);
			return text;
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x000FC6CC File Offset: 0x000FA8CC
		public void AddFile(string fileName, bool keepFile)
		{
			if (fileName == null || fileName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[]
				{
					"fileName"
				}), "fileName");
			}
			if (this.files[fileName] != null)
			{
				throw new ArgumentException(SR.GetString("DuplicateFileName", new object[]
				{
					fileName
				}), "fileName");
			}
			this.files.Add(fileName, keepFile);
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x000FC746 File Offset: 0x000FA946
		public IEnumerator GetEnumerator()
		{
			return this.files.Keys.GetEnumerator();
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000FC758 File Offset: 0x000FA958
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.files.Keys.GetEnumerator();
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000FC76A File Offset: 0x000FA96A
		void ICollection.CopyTo(Array array, int start)
		{
			this.files.Keys.CopyTo(array, start);
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000FC77E File Offset: 0x000FA97E
		public void CopyTo(string[] fileNames, int start)
		{
			this.files.Keys.CopyTo(fileNames, start);
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x000FC792 File Offset: 0x000FA992
		public int Count
		{
			get
			{
				return this.files.Count;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000FC79F File Offset: 0x000FA99F
		int ICollection.Count
		{
			get
			{
				return this.files.Count;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x000FC7AC File Offset: 0x000FA9AC
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000FC7AF File Offset: 0x000FA9AF
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000FC7B2 File Offset: 0x000FA9B2
		public string TempDir
		{
			get
			{
				if (this.tempDir != null)
				{
					return this.tempDir;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000FC7C8 File Offset: 0x000FA9C8
		public string BasePath
		{
			get
			{
				this.EnsureTempNameCreated();
				return this.basePath;
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000FC7D8 File Offset: 0x000FA9D8
		private void EnsureTempNameCreated()
		{
			if (this.basePath == null)
			{
				string text = null;
				bool flag = false;
				int num = 5000;
				do
				{
					try
					{
						this.basePath = this.GetTempFileName(this.TempDir);
						string fullPath = Path.GetFullPath(this.basePath);
						new FileIOPermission(FileIOPermissionAccess.AllAccess, fullPath).Demand();
						text = this.basePath + ".tmp";
						using (new FileStream(text, FileMode.CreateNew, FileAccess.Write))
						{
						}
						flag = true;
					}
					catch (IOException e)
					{
						num--;
						uint num2 = 2147942480U;
						if (num == 0 || (long)Marshal.GetHRForException(e) != (long)((ulong)num2))
						{
							throw;
						}
						flag = false;
					}
				}
				while (!flag);
				this.files.Add(text, this.keepFiles);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000FC8B4 File Offset: 0x000FAAB4
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x000FC8BC File Offset: 0x000FAABC
		public bool KeepFiles
		{
			get
			{
				return this.keepFiles;
			}
			set
			{
				this.keepFiles = value;
			}
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000FC8C8 File Offset: 0x000FAAC8
		private bool KeepFile(string fileName)
		{
			object obj = this.files[fileName];
			return obj != null && (bool)obj;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000FC8F0 File Offset: 0x000FAAF0
		public void Delete()
		{
			if (this.files != null && this.files.Count > 0)
			{
				string[] array = new string[this.files.Count];
				this.files.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (!this.KeepFile(text))
					{
						this.Delete(text);
						this.files.Remove(text);
					}
				}
			}
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000FC968 File Offset: 0x000FAB68
		private void DeleteHighIntegrityDirectory()
		{
			try
			{
				if (this.currentIdentity != null && Directory.Exists(this.highIntegrityDirectory))
				{
					TempFileCollection.RemoveAceOnTempDirectory(this.highIntegrityDirectory, this.currentIdentity.User.ToString());
					if (Directory.GetFiles(this.highIntegrityDirectory).Length == 0)
					{
						Directory.Delete(this.highIntegrityDirectory, true);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000FC9D4 File Offset: 0x000FABD4
		internal void SafeDelete()
		{
			WindowsImpersonationContext impersonation = Executor.RevertImpersonation();
			try
			{
				this.Delete();
			}
			finally
			{
				Executor.ReImpersonate(impersonation);
			}
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000FCA08 File Offset: 0x000FAC08
		private void Delete(string fileName)
		{
			try
			{
				File.Delete(fileName);
			}
			catch
			{
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000FCA30 File Offset: 0x000FAC30
		private string GetTempFileName(string tempDir)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
			if (string.IsNullOrEmpty(tempDir))
			{
				tempDir = Path.GetTempPath();
				if (!LocalAppContextSwitches.DisableTempFileCollectionDirectoryFeature && this.currentIdentity != null && new WindowsPrincipal(this.currentIdentity).IsInRole(WindowsBuiltInRole.Administrator))
				{
					tempDir = Path.Combine(tempDir, fileNameWithoutExtension);
					TempFileCollection.CreateTempDirectoryWithAce(tempDir, this.currentIdentity.User.ToString());
					this.highIntegrityDirectory = tempDir;
				}
			}
			string result;
			if (tempDir.EndsWith("\\", StringComparison.Ordinal))
			{
				result = tempDir + fileNameWithoutExtension;
			}
			else
			{
				result = tempDir + "\\" + fileNameWithoutExtension;
			}
			return result;
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000FCACC File Offset: 0x000FACCC
		private static void CreateTempDirectoryWithAce(string directory, string identity)
		{
			string stringSecurityDescriptor = "D:(D;OI;SD;;;" + identity + ")(A;OICI;FA;;;BA)S:(ML;OI;NW;;;HI)";
			SafeLocalMemHandle acl = null;
			SafeLocalMemHandle.ConvertStringSecurityDescriptorToSecurityDescriptor(stringSecurityDescriptor, 1, out acl, IntPtr.Zero);
			NativeMethods.CreateDirectory(directory, acl);
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000FCB04 File Offset: 0x000FAD04
		private static void RemoveAceOnTempDirectory(string directory, string identity)
		{
			string stringSecurityDescriptor = "D:(A;OICI;FA;;;" + identity + ")(A;OICI;FA;;;BA)";
			SafeLocalMemHandle pDacl = null;
			SafeLocalMemHandle.ConvertStringSecurityDescriptorToSecurityDescriptor(stringSecurityDescriptor, 1, out pDacl, IntPtr.Zero);
			NativeMethods.SetNamedSecurityInfo(directory, pDacl);
		}

		// Token: 0x04002CD0 RID: 11472
		private string basePath;

		// Token: 0x04002CD1 RID: 11473
		private string tempDir;

		// Token: 0x04002CD2 RID: 11474
		private bool keepFiles;

		// Token: 0x04002CD3 RID: 11475
		private Hashtable files;

		// Token: 0x04002CD4 RID: 11476
		[NonSerialized]
		private WindowsIdentity currentIdentity;

		// Token: 0x04002CD5 RID: 11477
		[NonSerialized]
		private string highIntegrityDirectory;
	}
}
