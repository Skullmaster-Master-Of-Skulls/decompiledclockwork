using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using ClockWorkLogger;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000006 RID: 6
	public static class FileSystem
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000028D0 File Offset: 0x00000AD0
		public static string ByteSize(int size)
		{
			return FileSystem.ByteSize((long)size);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028DC File Offset: 0x00000ADC
		public static string ByteSize(long size)
		{
			if (size == 0L)
			{
				return string.Format("{0}{1:0.#} {2}", null, 0, FileSystem.SizeSuffixes[0]);
			}
			double num = Math.Abs((double)size);
			int num2 = (int)Math.Log(num, 1000.0);
			int num3 = (num2 >= FileSystem.SizeSuffixes.Length) ? (FileSystem.SizeSuffixes.Length - 1) : num2;
			double num4 = num / Math.Pow(1000.0, (double)num3);
			return string.Format("{0}{1:0.#} {2}", (size < 0L) ? "-" : null, num4, FileSystem.SizeSuffixes[num3]);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000296C File Offset: 0x00000B6C
		public static void ManagePermissions(string directory, WellKnownSidType sidType = WellKnownSidType.WorldSid)
		{
			try
			{
				if (Directory.Exists(directory))
				{
					DirectorySecurity accessControl = Directory.GetAccessControl(directory);
					SecurityIdentifier identity = new SecurityIdentifier(sidType, null);
					accessControl.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ExecuteFile | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.Delete | FileSystemRights.ReadPermissions | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
					Directory.SetAccessControl(directory, accessControl);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static void ManagePermissions(string directory, string account, FileSystemRights rights, AccessControlType controlType, bool addAccess = true)
		{
			try
			{
				if (Directory.Exists(directory))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(directory);
					DirectorySecurity accessControl = directoryInfo.GetAccessControl();
					if (addAccess)
					{
						accessControl.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));
					}
					else
					{
						accessControl.RemoveAccessRule(new FileSystemAccessRule(account, rights, controlType));
					}
					directoryInfo.SetAccessControl(accessControl);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A28 File Offset: 0x00000C28
		public static void OpenContainerFolder(string directory)
		{
			try
			{
				if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
				{
					Process.Start(new ProcessStartInfo("explorer.exe", directory));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static void DeleteDirectory(string sourcePath, bool includeSubdirectories = true)
		{
			if (!Directory.Exists(sourcePath))
			{
				return;
			}
			foreach (string filename in Directory.GetFiles(sourcePath))
			{
				try
				{
					FileSystem.ForceDeleteFile(filename);
				}
				catch (Exception)
				{
				}
			}
			if (includeSubdirectories)
			{
				foreach (string sourcePath2 in Directory.GetDirectories(sourcePath))
				{
					try
					{
						FileSystem.DeleteDirectory(sourcePath2, true);
					}
					catch (Exception)
					{
					}
				}
				if (Directory.GetFiles(sourcePath).Length == 0)
				{
					try
					{
						Directory.Delete(sourcePath);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B0C File Offset: 0x00000D0C
		public static void DeleteInsideDirectory(string sourcePath, bool includeSubdirectories = true)
		{
			if (!Directory.Exists(sourcePath))
			{
				return;
			}
			foreach (string filename in Directory.GetFiles(sourcePath))
			{
				try
				{
					FileSystem.ForceDeleteFile(filename);
				}
				catch (Exception)
				{
				}
			}
			if (includeSubdirectories)
			{
				foreach (string sourcePath2 in Directory.GetDirectories(sourcePath))
				{
					try
					{
						FileSystem.DeleteDirectory(sourcePath2, true);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B8C File Offset: 0x00000D8C
		public static void DeleteFiles(string sourcePath, string extension)
		{
			foreach (string filename in Directory.GetFiles(sourcePath, extension.StartsWith(".") ? string.Format("*{0}", extension) : string.Format("*.{0}", extension), SearchOption.TopDirectoryOnly))
			{
				try
				{
					FileSystem.ForceDeleteFile(filename);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("FileSystem::DeleteFiles:: {0}", ex.ToString()), ex);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002C10 File Offset: 0x00000E10
		public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
		{
			bool result;
			try
			{
				SourcePath = (SourcePath.EndsWith("\\") ? SourcePath : (SourcePath + "\\"));
				DestinationPath = (DestinationPath.EndsWith("\\") ? DestinationPath : (DestinationPath + "\\"));
				if (Directory.Exists(SourcePath))
				{
					if (!Directory.Exists(DestinationPath))
					{
						Directory.CreateDirectory(DestinationPath);
					}
					string[] array = Directory.GetFiles(SourcePath);
					for (int i = 0; i < array.Length; i++)
					{
						FileInfo fileInfo = new FileInfo(array[i]);
						fileInfo.CopyTo(DestinationPath + fileInfo.Name, overwriteexisting);
					}
					foreach (string text in Directory.GetDirectories(SourcePath))
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(text);
						if (!FileSystem.CopyDirectory(text, DestinationPath + directoryInfo.Name, overwriteexisting))
						{
							return false;
						}
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSystem::CopyDirectory:: {0}", ex.ToString()), ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D14 File Offset: 0x00000F14
		public static bool CopyDirectoryAndContinueIfFailing(string SourcePath, string DestinationPath, bool overwriteexisting)
		{
			bool result;
			try
			{
				SourcePath = (SourcePath.EndsWith("\\") ? SourcePath : (SourcePath + "\\"));
				DestinationPath = (DestinationPath.EndsWith("\\") ? DestinationPath : (DestinationPath + "\\"));
				if (Directory.Exists(SourcePath))
				{
					if (!Directory.Exists(DestinationPath))
					{
						Directory.CreateDirectory(DestinationPath);
					}
					foreach (string text in Directory.GetFiles(SourcePath))
					{
						try
						{
							FileInfo fileInfo = new FileInfo(text);
							CWLogger.Logger.Trace("FileSystem::CopyDirectory:: trying to copy file='" + text + "'");
							fileInfo.CopyTo(DestinationPath + fileInfo.Name, overwriteexisting);
							CWLogger.Logger.Trace("FileSystem::CopyDirectory:: success copying file='" + text + "'");
						}
						catch (Exception ex)
						{
							CWLogger.Logger.ErrorException(string.Format("FileSystem::CopyDirectory:: file='{0}' {1}", text ?? "NULL", ex), ex);
						}
					}
					foreach (string text2 in Directory.GetDirectories(SourcePath))
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(text2);
						if (!FileSystem.CopyDirectoryAndContinueIfFailing(text2, DestinationPath + directoryInfo.Name, overwriteexisting))
						{
							return false;
						}
					}
				}
				result = true;
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSystem::CopyDirectory:: {0}", ex2), ex2);
				result = false;
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E9C File Offset: 0x0000109C
		public static bool CopyDirectoryAndContinueIfFailing(string SourcePath, string DestinationPath, bool overwriteexisting, params string[] exceptFolders)
		{
			bool result;
			try
			{
				SourcePath = (SourcePath.EndsWith("\\") ? SourcePath : (SourcePath + "\\"));
				DestinationPath = (DestinationPath.EndsWith("\\") ? DestinationPath : (DestinationPath + "\\"));
				if (Directory.Exists(SourcePath))
				{
					if (!Directory.Exists(DestinationPath))
					{
						Directory.CreateDirectory(DestinationPath);
					}
					foreach (string text in Directory.GetFiles(SourcePath))
					{
						try
						{
							FileInfo fileInfo = new FileInfo(text);
							fileInfo.CopyTo(DestinationPath + fileInfo.Name, overwriteexisting);
						}
						catch (Exception ex)
						{
							CWLogger.Logger.ErrorException(string.Format("FileSystem::CopyDirectory:: file='{0}' {1}", text ?? "NULL", ex), ex);
						}
					}
					IEnumerable<string> enumerable;
					if (exceptFolders == null)
					{
						IEnumerable<string> directories = Directory.GetDirectories(SourcePath);
						enumerable = directories;
					}
					else
					{
						enumerable = Directory.GetDirectories(SourcePath).Except(exceptFolders);
					}
					foreach (string text2 in enumerable)
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(text2);
						if (!FileSystem.CopyDirectoryAndContinueIfFailing(text2, DestinationPath + directoryInfo.Name, overwriteexisting, exceptFolders))
						{
							return false;
						}
					}
				}
				result = true;
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSystem::CopyDirectory:: {0}", ex2), ex2);
				result = false;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003030 File Offset: 0x00001230
		public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting, bool recursive, string extension = "*")
		{
			bool result;
			try
			{
				SourcePath = (SourcePath.EndsWith("\\") ? SourcePath : (SourcePath + "\\"));
				DestinationPath = (DestinationPath.EndsWith("\\") ? DestinationPath : (DestinationPath + "\\"));
				if (Directory.Exists(SourcePath))
				{
					if (!Directory.Exists(DestinationPath))
					{
						Directory.CreateDirectory(DestinationPath);
					}
					string[] array = Directory.GetFiles(SourcePath, extension.StartsWith(".") ? string.Format("*{0}", extension) : string.Format("*.{0}", extension), SearchOption.TopDirectoryOnly);
					for (int i = 0; i < array.Length; i++)
					{
						FileInfo fileInfo = new FileInfo(array[i]);
						fileInfo.CopyTo(DestinationPath + fileInfo.Name, overwriteexisting);
					}
					if (recursive)
					{
						foreach (string text in Directory.GetDirectories(SourcePath))
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(text);
							if (!FileSystem.CopyDirectory(text, DestinationPath + directoryInfo.Name, overwriteexisting))
							{
								return false;
							}
						}
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("FileSystem::CopyDirectory:: {0}", ex.ToString()), ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003164 File Offset: 0x00001364
		public static string GetTemporalFolder()
		{
			return FileSystem.GetTemporalFolderInTechnoPro();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000316B File Offset: 0x0000136B
		public static void CleanTechnoProTempFolder()
		{
			FileSystem.DeleteInsideDirectory(FileSystem.GetTechnoProTempFolderPath(), true);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003178 File Offset: 0x00001378
		public static string GetTechnoProTempFolderPath()
		{
			return Path.Combine(Path.GetTempPath(), "TechnoPro");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000318C File Offset: 0x0000138C
		public static string GetTemporalFolderInTechnoPro()
		{
			string path = Guid.NewGuid().ToString();
			string technoProTempFolderPath = FileSystem.GetTechnoProTempFolderPath();
			string result;
			try
			{
				if (!Directory.Exists(technoProTempFolderPath))
				{
					Directory.CreateDirectory(technoProTempFolderPath);
				}
				string text = Path.Combine(technoProTempFolderPath, path);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				result = text;
			}
			catch (Exception)
			{
				result = (Directory.Exists(technoProTempFolderPath) ? technoProTempFolderPath : null);
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003204 File Offset: 0x00001404
		public static string GetTempFileName(string extension)
		{
			string path = string.Format("{0}_{1}{2}", Guid.NewGuid().ToString(), DateTime.Now.Millisecond.ToString(), extension);
			string text = Path.GetTempPath();
			text = Path.Combine(text, "TechnoPro");
			text = Path.Combine(text, "ClockWork");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return Path.Combine(text, path);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000327A File Offset: 0x0000147A
		public static bool MoveFile(string existingFilename, string newFilename, MoveFileFlags moveFlag)
		{
			return FileSystem.MoveFileEx(existingFilename, newFilename, moveFlag);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003284 File Offset: 0x00001484
		public static void ForceDeleteFile(string filename)
		{
			try
			{
				File.Delete(filename);
			}
			catch
			{
				try
				{
					FileSystem.MoveFileEx(filename, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("ClockWork:Common:Win32:FileSystem:ForceDeleteFile:{0}", ex.ToString());
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000032DC File Offset: 0x000014DC
		public static string GetUserApplicationDataFolder(string appName = null)
		{
			string text = string.IsNullOrEmpty(appName) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TechnoPro") : Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TechnoPro"), appName);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

		// Token: 0x0400000A RID: 10
		private static readonly string[] SizeSuffixes = new string[]
		{
			"B",
			"KB",
			"MB",
			"GB",
			"TB",
			"PB",
			"EB",
			"ZB",
			"YB"
		};
	}
}
