using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using Microsoft.Win32;

namespace System.Configuration.Internal
{
	// Token: 0x020000C0 RID: 192
	internal class WriteFileContext
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x000203A8 File Offset: 0x0001E5A8
		internal WriteFileContext(string filename, string templateFilename)
		{
			string directoryOrRootName = UrlPath.GetDirectoryOrRootName(filename);
			this._templateFilename = templateFilename;
			this._tempFiles = new TempFileCollection(directoryOrRootName);
			try
			{
				this._tempNewFilename = this._tempFiles.AddExtension("newcfg");
			}
			catch
			{
				((IDisposable)this._tempFiles).Dispose();
				this._tempFiles = null;
				throw;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002041E File Offset: 0x0001E61E
		internal string TempNewFilename
		{
			get
			{
				return this._tempNewFilename;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020428 File Offset: 0x0001E628
		internal void Complete(string filename, bool success)
		{
			try
			{
				if (success)
				{
					if (File.Exists(filename))
					{
						this.ValidateWriteAccess(filename);
						this.DuplicateFileAttributes(filename, this._tempNewFilename);
					}
					else if (this._templateFilename != null)
					{
						this.DuplicateTemplateAttributes(this._templateFilename, this._tempNewFilename);
					}
					this.ReplaceFile(this._tempNewFilename, filename);
					this._tempFiles.KeepFiles = true;
				}
			}
			finally
			{
				((IDisposable)this._tempFiles).Dispose();
				this._tempFiles = null;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000204B0 File Offset: 0x0001E6B0
		private void DuplicateFileAttributes(string source, string destination)
		{
			FileAttributes attributes = File.GetAttributes(source);
			File.SetAttributes(destination, attributes);
			DateTime creationTimeUtc = File.GetCreationTimeUtc(source);
			File.SetCreationTimeUtc(destination, creationTimeUtc);
			this.DuplicateTemplateAttributes(source, destination);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000204E4 File Offset: 0x0001E6E4
		private void DuplicateTemplateAttributes(string source, string destination)
		{
			if (this.IsWinNT)
			{
				FileSecurity accessControl = File.GetAccessControl(source, AccessControlSections.Access);
				accessControl.SetAccessRuleProtection(accessControl.AreAccessRulesProtected, true);
				File.SetAccessControl(destination, accessControl);
				return;
			}
			FileAttributes attributes = File.GetAttributes(source);
			File.SetAttributes(destination, attributes);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00020524 File Offset: 0x0001E724
		private void ValidateWriteAccess(string filename)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
			}
			catch (UnauthorizedAccessException)
			{
				throw;
			}
			catch (IOException)
			{
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00020588 File Offset: 0x0001E788
		private void ReplaceFile(string Source, string Target)
		{
			int num = 0;
			bool flag = this.AttemptMove(Source, Target);
			while (!flag && num < 10000 && File.Exists(Target) && !this.FileIsWriteLocked(Target))
			{
				Thread.Sleep(100);
				num += 100;
				flag = this.AttemptMove(Source, Target);
			}
			if (!flag)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_write_failed", new object[]
				{
					Target
				}));
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000205F4 File Offset: 0x0001E7F4
		private bool AttemptMove(string Source, string Target)
		{
			bool result = false;
			if (this.IsWinNT)
			{
				result = UnsafeNativeMethods.MoveFileEx(Source, Target, 1);
			}
			else
			{
				try
				{
					File.Copy(Source, Target, true);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002063C File Offset: 0x0001E83C
		private bool FileIsWriteLocked(string FileName)
		{
			Stream stream = null;
			bool result = true;
			if (!FileUtil.FileExists(FileName, true))
			{
				return false;
			}
			try
			{
				FileShare fileShare = FileShare.Read;
				if (this.IsWinNT)
				{
					fileShare |= FileShare.Delete;
				}
				stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, fileShare);
				result = false;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
					stream = null;
				}
			}
			return result;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00020694 File Offset: 0x0001E894
		private bool IsWinNT
		{
			get
			{
				if (!WriteFileContext._osPlatformDetermined)
				{
					WriteFileContext._osPlatform = Environment.OSVersion.Platform;
					WriteFileContext._osPlatformDetermined = true;
				}
				return WriteFileContext._osPlatform == PlatformID.Win32NT;
			}
		}

		// Token: 0x0400045F RID: 1119
		private const int SAVING_TIMEOUT = 10000;

		// Token: 0x04000460 RID: 1120
		private const int SAVING_RETRY_INTERVAL = 100;

		// Token: 0x04000461 RID: 1121
		private static volatile bool _osPlatformDetermined = false;

		// Token: 0x04000462 RID: 1122
		private static volatile PlatformID _osPlatform;

		// Token: 0x04000463 RID: 1123
		private TempFileCollection _tempFiles;

		// Token: 0x04000464 RID: 1124
		private string _tempNewFilename;

		// Token: 0x04000465 RID: 1125
		private string _templateFilename;
	}
}
