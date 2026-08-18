using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000075 RID: 117
	internal sealed class FileChangesMonitor
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0000B920 File Offset: 0x00009B20
		internal static string GenerateErrorMessage(FileAction action, string fileName = null)
		{
			string text;
			if (action == FileAction.Overwhelming)
			{
				text = "Overwhelming Change Notification in ";
			}
			else
			{
				if (action != FileAction.Error)
				{
					return null;
				}
				text = "File Change Notification Error in ";
			}
			if (fileName == null)
			{
				return text;
			}
			return text + Path.GetDirectoryName(fileName);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000B95C File Offset: 0x00009B5C
		internal static HttpException CreateFileMonitoringException(int hr, string path)
		{
			bool flag = false;
			string name;
			if (hr <= -2147024891)
			{
				if (hr - -2147024894 <= 1)
				{
					name = "Directory_does_not_exist_for_monitoring";
					goto IL_5A;
				}
				if (hr == -2147024891)
				{
					name = "Access_denied_for_monitoring";
					flag = true;
					goto IL_5A;
				}
			}
			else
			{
				if (hr == -2147024840)
				{
					name = "NetBios_command_limit_reached";
					flag = true;
					goto IL_5A;
				}
				if (hr == -2147024809)
				{
					name = "Invalid_file_name_for_monitoring";
					goto IL_5A;
				}
			}
			name = "Failed_to_start_monitoring";
			IL_5A:
			if (flag)
			{
				UnsafeNativeMethods.RaiseFileMonitoringEventlogEvent(SR.GetString(name, new object[]
				{
					HttpRuntime.GetSafePath(path)
				}) + "\n\r" + SR.GetString("App_Virtual_Path", new object[]
				{
					HttpRuntime.AppDomainAppVirtualPath
				}), path, HttpRuntime.AppDomainAppVirtualPath, hr);
			}
			return new HttpException(SR.GetString(name, new object[]
			{
				HttpRuntime.GetSafePath(path)
			}), hr);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000BA24 File Offset: 0x00009C24
		internal static string GetFullPath(string alias)
		{
			try
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, alias).Assert();
			}
			catch
			{
				throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
			}
			string fullPath = Path.GetFullPath(alias);
			return FileUtil.RemoveTrailingDirectoryBackSlash(fullPath);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000BA6C File Offset: 0x00009C6C
		private bool IsBeneathAppPathInternal(string fullPathName)
		{
			return this._appPathInternal != null && fullPathName.Length > this._appPathInternal.Length + 1 && fullPathName.IndexOf(this._appPathInternal, StringComparison.OrdinalIgnoreCase) > -1 && fullPathName[this._appPathInternal.Length] == Path.DirectorySeparatorChar;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		private bool IsFCNDisabled
		{
			get
			{
				return this._FCNMode == 1;
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000BACC File Offset: 0x00009CCC
		internal FileChangesMonitor(FcnMode mode)
		{
			switch (mode)
			{
			case FcnMode.NotSet:
				UnsafeNativeMethods.GetDirMonConfiguration(out this._FCNMode);
				goto IL_44;
			case FcnMode.Disabled:
				this._FCNMode = 1;
				goto IL_44;
			case FcnMode.Single:
				this._FCNMode = 2;
				goto IL_44;
			}
			this._FCNMode = 0;
			IL_44:
			if (this.IsFCNDisabled)
			{
				return;
			}
			this._aliases = Hashtable.Synchronized(new Hashtable(StringComparer.OrdinalIgnoreCase));
			this._dirs = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this._subDirDirMons = new Hashtable(StringComparer.OrdinalIgnoreCase);
			if (this._FCNMode == 2 && HttpRuntime.AppDomainAppPathInternal != null)
			{
				this._appPathInternal = FileChangesMonitor.GetFullPath(HttpRuntime.AppDomainAppPathInternal);
				this._dirMonAppPathInternal = new DirectoryMonitor(this._appPathInternal, this._FCNMode);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000BB94 File Offset: 0x00009D94
		internal bool IsDirNameMonitored(string fullPath, string dirName)
		{
			if (this._dirs.ContainsKey(fullPath))
			{
				return true;
			}
			foreach (string text in FileChangesMonitor.s_dirsToMonitor)
			{
				if (StringUtil.StringStartsWithIgnoreCase(dirName, text))
				{
					if (dirName.Length == text.Length)
					{
						return true;
					}
					if (dirName.Length > text.Length && dirName[text.Length] == Path.DirectorySeparatorChar)
					{
						return true;
					}
				}
			}
			return dirName.IndexOf("App_LocalResources", StringComparison.OrdinalIgnoreCase) > -1;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000BC18 File Offset: 0x00009E18
		private DirectoryMonitor FindDirectoryMonitor(string dir, bool addIfNotFound, bool throwOnError)
		{
			FileAttributesData fileAttributesData = null;
			DirectoryMonitor directoryMonitor = (DirectoryMonitor)this._dirs[dir];
			if (directoryMonitor != null && !directoryMonitor.IsMonitoring() && (FileAttributesData.GetFileAttributes(dir, out fileAttributesData) != 0 || (fileAttributesData.FileAttributes & FileAttributes.Directory) == (FileAttributes)0))
			{
				directoryMonitor = null;
			}
			if (directoryMonitor != null || !addIfNotFound)
			{
				return directoryMonitor;
			}
			object syncRoot = this._dirs.SyncRoot;
			lock (syncRoot)
			{
				directoryMonitor = (DirectoryMonitor)this._dirs[dir];
				if (directoryMonitor != null)
				{
					if (!directoryMonitor.IsMonitoring())
					{
						int num = FileAttributesData.GetFileAttributes(dir, out fileAttributesData);
						if (num == 0 && (fileAttributesData.FileAttributes & FileAttributes.Directory) == (FileAttributes)0)
						{
							num = -2147024809;
						}
						if (num != 0)
						{
							this._dirs.Remove(dir);
							directoryMonitor.StopMonitoring();
							if (addIfNotFound && throwOnError)
							{
								throw FileChangesMonitor.CreateFileMonitoringException(num, dir);
							}
							return null;
						}
					}
				}
				else if (addIfNotFound)
				{
					int num = FileAttributesData.GetFileAttributes(dir, out fileAttributesData);
					if (num == 0 && (fileAttributesData.FileAttributes & FileAttributes.Directory) == (FileAttributes)0)
					{
						num = -2147024809;
					}
					if (num == 0)
					{
						directoryMonitor = new DirectoryMonitor(dir, false, 347U, this._FCNMode);
						this._dirs.Add(dir, directoryMonitor);
					}
					else if (throwOnError)
					{
						throw FileChangesMonitor.CreateFileMonitoringException(num, dir);
					}
				}
			}
			return directoryMonitor;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000BD54 File Offset: 0x00009F54
		internal void RemoveAliases(FileMonitor fileMon)
		{
			if (this.IsFCNDisabled)
			{
				return;
			}
			foreach (object obj in fileMon.Aliases)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (this._aliases[dictionaryEntry.Key] == fileMon)
				{
					this._aliases.Remove(dictionaryEntry.Key);
				}
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		internal DateTime StartMonitoringFile(string alias, FileChangeEventHandler callback)
		{
			bool flag = false;
			if (alias == null)
			{
				throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
			}
			string fullPath;
			if (!this.IsFCNDisabled)
			{
				DateTime result;
				using (new ApplicationImpersonationContext())
				{
					this._lockDispose.AcquireReaderLock();
					FileMonitor fileMonitor;
					string text;
					try
					{
						if (this._disposed)
						{
							return DateTime.MinValue;
						}
						fileMonitor = (FileMonitor)this._aliases[alias];
						DirectoryMonitor directoryMonitor;
						if (fileMonitor != null)
						{
							directoryMonitor = fileMonitor.DirectoryMonitor;
							text = fileMonitor.FileNameLong;
						}
						else
						{
							flag = true;
							if (alias.Length == 0 || !UrlPath.IsAbsolutePhysicalPath(alias))
							{
								throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
							}
							fullPath = FileChangesMonitor.GetFullPath(alias);
							if (this.IsBeneathAppPathInternal(fullPath))
							{
								directoryMonitor = this._dirMonAppPathInternal;
								text = fullPath.Substring(this._appPathInternal.Length + 1);
							}
							else
							{
								string directoryOrRootName = UrlPath.GetDirectoryOrRootName(fullPath);
								text = Path.GetFileName(fullPath);
								if (string.IsNullOrEmpty(text))
								{
									throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
								}
								directoryMonitor = this.FindDirectoryMonitor(directoryOrRootName, true, true);
							}
						}
						fileMonitor = directoryMonitor.StartMonitoringFileWithAssert(text, callback, alias);
						if (flag)
						{
							this._aliases[alias] = fileMonitor;
						}
					}
					finally
					{
						this._lockDispose.ReleaseReaderLock();
					}
					FileAttributesData fileAttributesData;
					fileMonitor.DirectoryMonitor.GetFileAttributes(text, out fileAttributesData);
					if (fileAttributesData != null)
					{
						result = fileAttributesData.UtcLastWriteTime;
					}
					else
					{
						result = DateTime.MinValue;
					}
				}
				return result;
			}
			fullPath = FileChangesMonitor.GetFullPath(alias);
			FindFileData findFileData = null;
			if (FindFileData.FindFile(fullPath, out findFileData) == 0)
			{
				return findFileData.FileAttributesData.UtcLastWriteTime;
			}
			return DateTime.MinValue;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000BF88 File Offset: 0x0000A188
		internal DateTime StartMonitoringPath(string alias, FileChangeEventHandler callback, out FileAttributesData fad)
		{
			FileMonitor fileMonitor = null;
			string text = null;
			bool flag = false;
			fad = null;
			if (alias == null)
			{
				throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
				{
					string.Empty
				}));
			}
			string fullPath;
			if (!this.IsFCNDisabled)
			{
				DateTime result;
				using (new ApplicationImpersonationContext())
				{
					this._lockDispose.AcquireReaderLock();
					try
					{
						if (this._disposed)
						{
							return DateTime.MinValue;
						}
						fileMonitor = (FileMonitor)this._aliases[alias];
						if (fileMonitor != null)
						{
							text = fileMonitor.FileNameLong;
							fileMonitor = fileMonitor.DirectoryMonitor.StartMonitoringFileWithAssert(text, callback, alias);
						}
						else
						{
							flag = true;
							if (alias.Length == 0 || !UrlPath.IsAbsolutePhysicalPath(alias))
							{
								throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
								{
									HttpRuntime.GetSafePath(alias)
								}));
							}
							fullPath = FileChangesMonitor.GetFullPath(alias);
							if (this.IsBeneathAppPathInternal(fullPath))
							{
								DirectoryMonitor directoryMonitor = this._dirMonAppPathInternal;
								text = fullPath.Substring(this._appPathInternal.Length + 1);
								fileMonitor = directoryMonitor.StartMonitoringFileWithAssert(text, callback, alias);
							}
							else
							{
								DirectoryMonitor directoryMonitor = this.FindDirectoryMonitor(fullPath, false, false);
								if (directoryMonitor != null)
								{
									fileMonitor = directoryMonitor.StartMonitoringFileWithAssert(null, callback, alias);
								}
								else
								{
									string directoryOrRootName = UrlPath.GetDirectoryOrRootName(fullPath);
									text = Path.GetFileName(fullPath);
									if (!string.IsNullOrEmpty(text))
									{
										directoryMonitor = this.FindDirectoryMonitor(directoryOrRootName, false, false);
										if (directoryMonitor != null)
										{
											try
											{
												fileMonitor = directoryMonitor.StartMonitoringFileWithAssert(text, callback, alias);
											}
											catch
											{
											}
											if (fileMonitor != null)
											{
												goto IL_1BD;
											}
										}
									}
									directoryMonitor = this.FindDirectoryMonitor(fullPath, true, false);
									if (directoryMonitor != null)
									{
										text = null;
									}
									else
									{
										if (string.IsNullOrEmpty(text))
										{
											throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
										}
										directoryMonitor = this.FindDirectoryMonitor(directoryOrRootName, true, true);
									}
									fileMonitor = directoryMonitor.StartMonitoringFileWithAssert(text, callback, alias);
								}
							}
						}
						IL_1BD:
						if (!fileMonitor.IsDirectory)
						{
							fileMonitor.DirectoryMonitor.GetFileAttributes(text, out fad);
						}
						if (flag)
						{
							this._aliases[alias] = fileMonitor;
						}
					}
					finally
					{
						this._lockDispose.ReleaseReaderLock();
					}
					if (fad != null)
					{
						result = fad.UtcLastWriteTime;
					}
					else
					{
						result = DateTime.MinValue;
					}
				}
				return result;
			}
			fullPath = FileChangesMonitor.GetFullPath(alias);
			FindFileData findFileData = null;
			if (FindFileData.FindFile(fullPath, out findFileData) == 0)
			{
				fad = findFileData.FileAttributesData;
				return findFileData.FileAttributesData.UtcLastWriteTime;
			}
			return DateTime.MinValue;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		internal void StartMonitoringDirectoryRenamesAndBinDirectory(string dir, FileChangeEventHandler callback)
		{
			if (string.IsNullOrEmpty(dir))
			{
				throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
				{
					string.Empty
				}));
			}
			if (this.IsFCNDisabled)
			{
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				this._lockDispose.AcquireReaderLock();
				try
				{
					if (!this._disposed)
					{
						this._callbackRenameOrCriticaldirChange = callback;
						string fullPath = FileChangesMonitor.GetFullPath(dir);
						this._dirMonSubdirs = new DirectoryMonitor(fullPath, true, 2U, true, this._FCNMode);
						try
						{
							this._dirMonSubdirs.StartMonitoringFileWithAssert(null, new FileChangeEventHandler(this.OnSubdirChange), fullPath);
						}
						catch
						{
							((IDisposable)this._dirMonSubdirs).Dispose();
							this._dirMonSubdirs = null;
							throw;
						}
						this._dirMonSpecialDirs = new ArrayList();
						for (int i = 0; i < FileChangesMonitor.s_dirsToMonitor.Length; i++)
						{
							this._dirMonSpecialDirs.Add(this.ListenToSubdirectoryChanges(fullPath, FileChangesMonitor.s_dirsToMonitor[i]));
						}
					}
				}
				finally
				{
					this._lockDispose.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000C324 File Offset: 0x0000A524
		internal void StartListeningToLocalResourcesDirectory(VirtualPath virtualDir)
		{
			if (this.IsFCNDisabled)
			{
				return;
			}
			if (this._callbackRenameOrCriticaldirChange == null || this._dirMonSpecialDirs == null)
			{
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				this._lockDispose.AcquireReaderLock();
				try
				{
					if (!this._disposed)
					{
						string text = virtualDir.MapPath();
						text = FileUtil.RemoveTrailingDirectoryBackSlash(text);
						string fileName = Path.GetFileName(text);
						text = Path.GetDirectoryName(text);
						if (Directory.Exists(text))
						{
							this._dirMonSpecialDirs.Add(this.ListenToSubdirectoryChanges(text, fileName));
						}
					}
				}
				finally
				{
					this._lockDispose.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		private DirectoryMonitor ListenToSubdirectoryChanges(string dirRoot, string dirToListenTo)
		{
			string text;
			if (StringUtil.StringEndsWith(dirRoot, '\\'))
			{
				text = dirRoot + dirToListenTo;
			}
			else
			{
				text = dirRoot + "\\" + dirToListenTo;
			}
			DirectoryMonitor directoryMonitor;
			if (this.IsBeneathAppPathInternal(text))
			{
				directoryMonitor = this._dirMonAppPathInternal;
				dirToListenTo = text.Substring(this._appPathInternal.Length + 1);
				directoryMonitor.StartMonitoringFileWithAssert(dirToListenTo, new FileChangeEventHandler(this.OnCriticaldirChange), text);
			}
			else
			{
				if (Directory.Exists(text))
				{
					directoryMonitor = new DirectoryMonitor(text, true, 345U, this._FCNMode);
					try
					{
						directoryMonitor.StartMonitoringFileWithAssert(null, new FileChangeEventHandler(this.OnCriticaldirChange), text);
						return directoryMonitor;
					}
					catch
					{
						((IDisposable)directoryMonitor).Dispose();
						directoryMonitor = null;
						throw;
					}
				}
				directoryMonitor = (DirectoryMonitor)this._subDirDirMons[dirRoot];
				if (directoryMonitor == null)
				{
					directoryMonitor = new DirectoryMonitor(dirRoot, false, 347U, this._FCNMode);
					this._subDirDirMons[dirRoot] = directoryMonitor;
				}
				try
				{
					directoryMonitor.StartMonitoringFileWithAssert(dirToListenTo, new FileChangeEventHandler(this.OnCriticaldirChange), text);
				}
				catch
				{
					((IDisposable)directoryMonitor).Dispose();
					directoryMonitor = null;
					throw;
				}
			}
			return directoryMonitor;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		private void OnSubdirChange(object sender, FileChangeEvent e)
		{
			try
			{
				Interlocked.Increment(ref this._activeCallbackCount);
				if (!this._disposed)
				{
					FileChangeEventHandler callbackRenameOrCriticaldirChange = this._callbackRenameOrCriticaldirChange;
					if (callbackRenameOrCriticaldirChange != null && (e.Action == FileAction.Error || e.Action == FileAction.Overwhelming || e.Action == FileAction.RenamedOldName || e.Action == FileAction.Removed))
					{
						HttpRuntime.SetShutdownMessage(SR.GetString("Directory_rename_notification", new object[]
						{
							e.FileName
						}));
						callbackRenameOrCriticaldirChange(this, e);
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this._activeCallbackCount);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000C58C File Offset: 0x0000A78C
		private void OnCriticaldirChange(object sender, FileChangeEvent e)
		{
			try
			{
				Interlocked.Increment(ref this._activeCallbackCount);
				if (!this._disposed)
				{
					HttpRuntime.SetShutdownMessage(SR.GetString("Change_notification_critical_dir"));
					FileChangeEventHandler callbackRenameOrCriticaldirChange = this._callbackRenameOrCriticaldirChange;
					if (callbackRenameOrCriticaldirChange != null)
					{
						callbackRenameOrCriticaldirChange(this, e);
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this._activeCallbackCount);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		internal void StopMonitoringFile(string alias, object target)
		{
			if (this.IsFCNDisabled)
			{
				return;
			}
			if (alias == null)
			{
				throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
				{
					string.Empty
				}));
			}
			using (new ApplicationImpersonationContext())
			{
				this._lockDispose.AcquireReaderLock();
				try
				{
					if (!this._disposed)
					{
						FileMonitor fileMonitor = (FileMonitor)this._aliases[alias];
						DirectoryMonitor directoryMonitor;
						string text;
						if (fileMonitor != null && !fileMonitor.IsDirectory)
						{
							directoryMonitor = fileMonitor.DirectoryMonitor;
							text = fileMonitor.FileNameLong;
						}
						else
						{
							if (alias.Length == 0 || !UrlPath.IsAbsolutePhysicalPath(alias))
							{
								throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
								{
									HttpRuntime.GetSafePath(alias)
								}));
							}
							string fullPath = FileChangesMonitor.GetFullPath(alias);
							string directoryOrRootName = UrlPath.GetDirectoryOrRootName(fullPath);
							text = Path.GetFileName(fullPath);
							if (string.IsNullOrEmpty(text))
							{
								throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
								{
									HttpRuntime.GetSafePath(alias)
								}));
							}
							directoryMonitor = this.FindDirectoryMonitor(directoryOrRootName, false, false);
						}
						if (directoryMonitor != null)
						{
							directoryMonitor.StopMonitoringFile(text, target);
						}
					}
				}
				finally
				{
					this._lockDispose.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000C730 File Offset: 0x0000A930
		internal void StopMonitoringPath(string alias, object target)
		{
			if (this.IsFCNDisabled)
			{
				return;
			}
			string text = null;
			if (alias == null)
			{
				throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
				{
					string.Empty
				}));
			}
			using (new ApplicationImpersonationContext())
			{
				this._lockDispose.AcquireReaderLock();
				try
				{
					if (!this._disposed)
					{
						FileMonitor fileMonitor = (FileMonitor)this._aliases[alias];
						DirectoryMonitor directoryMonitor;
						if (fileMonitor != null)
						{
							directoryMonitor = fileMonitor.DirectoryMonitor;
							text = fileMonitor.FileNameLong;
						}
						else
						{
							if (alias.Length == 0 || !UrlPath.IsAbsolutePhysicalPath(alias))
							{
								throw new HttpException(SR.GetString("Invalid_file_name_for_monitoring", new object[]
								{
									HttpRuntime.GetSafePath(alias)
								}));
							}
							string fullPath = FileChangesMonitor.GetFullPath(alias);
							directoryMonitor = this.FindDirectoryMonitor(fullPath, false, false);
							if (directoryMonitor == null)
							{
								string directoryOrRootName = UrlPath.GetDirectoryOrRootName(fullPath);
								text = Path.GetFileName(fullPath);
								if (!string.IsNullOrEmpty(text))
								{
									directoryMonitor = this.FindDirectoryMonitor(directoryOrRootName, false, false);
								}
							}
						}
						if (directoryMonitor != null)
						{
							directoryMonitor.StopMonitoringFile(text, target);
						}
					}
				}
				finally
				{
					this._lockDispose.ReleaseReaderLock();
				}
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000C858 File Offset: 0x0000AA58
		internal FileAttributesData GetFileAttributes(string alias)
		{
			DirectoryMonitor directoryMonitor = null;
			string text = null;
			FileAttributesData fileAttributesData = null;
			if (alias == null)
			{
				throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
			}
			string fullPath;
			if (!this.IsFCNDisabled)
			{
				FileAttributesData result;
				using (new ApplicationImpersonationContext())
				{
					this._lockDispose.AcquireReaderLock();
					try
					{
						if (!this._disposed)
						{
							FileMonitor fileMonitor = (FileMonitor)this._aliases[alias];
							if (fileMonitor != null && !fileMonitor.IsDirectory)
							{
								directoryMonitor = fileMonitor.DirectoryMonitor;
								text = fileMonitor.FileNameLong;
							}
							else
							{
								if (alias.Length == 0 || !UrlPath.IsAbsolutePhysicalPath(alias))
								{
									throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
								}
								fullPath = FileChangesMonitor.GetFullPath(alias);
								string directoryOrRootName = UrlPath.GetDirectoryOrRootName(fullPath);
								text = Path.GetFileName(fullPath);
								if (!string.IsNullOrEmpty(text))
								{
									directoryMonitor = this.FindDirectoryMonitor(directoryOrRootName, false, false);
								}
							}
						}
					}
					finally
					{
						this._lockDispose.ReleaseReaderLock();
					}
					if (directoryMonitor == null || !directoryMonitor.GetFileAttributes(text, out fileAttributesData))
					{
						FileAttributesData.GetFileAttributes(alias, out fileAttributesData);
					}
					result = fileAttributesData;
				}
				return result;
			}
			if (alias.Length == 0 || !UrlPath.IsAbsolutePhysicalPath(alias))
			{
				throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, alias);
			}
			fullPath = FileChangesMonitor.GetFullPath(alias);
			FindFileData findFileData = null;
			if (FindFileData.FindFile(fullPath, out findFileData) == 0)
			{
				return findFileData.FileAttributesData;
			}
			return null;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		internal void Stop()
		{
			if (this.IsFCNDisabled)
			{
				return;
			}
			using (new ApplicationImpersonationContext())
			{
				this._lockDispose.AcquireWriterLock();
				try
				{
					this._disposed = true;
					goto IL_39;
				}
				finally
				{
					this._lockDispose.ReleaseWriterLock();
				}
				IL_2F:
				Thread.Sleep(250);
				IL_39:
				if (this._activeCallbackCount != 0)
				{
					goto IL_2F;
				}
				if (this._dirMonSubdirs != null)
				{
					this._dirMonSubdirs.StopMonitoring();
					this._dirMonSubdirs = null;
				}
				if (this._dirMonSpecialDirs != null)
				{
					foreach (object obj in this._dirMonSpecialDirs)
					{
						DirectoryMonitor directoryMonitor = (DirectoryMonitor)obj;
						if (directoryMonitor != null)
						{
							directoryMonitor.StopMonitoring();
						}
					}
					this._dirMonSpecialDirs = null;
				}
				this._callbackRenameOrCriticaldirChange = null;
				if (this._dirs != null)
				{
					IDictionaryEnumerator enumerator2 = this._dirs.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						DirectoryMonitor directoryMonitor2 = (DirectoryMonitor)enumerator2.Value;
						directoryMonitor2.StopMonitoring();
					}
				}
				this._dirs.Clear();
				this._aliases.Clear();
				while (DirMonCompletion.ActiveDirMonCompletions != 0)
				{
					Thread.Sleep(10);
				}
			}
		}

		// Token: 0x04000224 RID: 548
		internal static string[] s_dirsToMonitor = new string[]
		{
			"bin",
			"App_GlobalResources",
			"App_Code",
			"App_WebReferences",
			"App_Browsers"
		};

		// Token: 0x04000225 RID: 549
		internal const int MAX_PATH = 260;

		// Token: 0x04000226 RID: 550
		private ReadWriteSpinLock _lockDispose;

		// Token: 0x04000227 RID: 551
		private bool _disposed;

		// Token: 0x04000228 RID: 552
		private Hashtable _aliases;

		// Token: 0x04000229 RID: 553
		private Hashtable _dirs;

		// Token: 0x0400022A RID: 554
		private DirectoryMonitor _dirMonSubdirs;

		// Token: 0x0400022B RID: 555
		private Hashtable _subDirDirMons;

		// Token: 0x0400022C RID: 556
		private ArrayList _dirMonSpecialDirs;

		// Token: 0x0400022D RID: 557
		private FileChangeEventHandler _callbackRenameOrCriticaldirChange;

		// Token: 0x0400022E RID: 558
		private int _activeCallbackCount;

		// Token: 0x0400022F RID: 559
		private DirectoryMonitor _dirMonAppPathInternal;

		// Token: 0x04000230 RID: 560
		private string _appPathInternal;

		// Token: 0x04000231 RID: 561
		private int _FCNMode;
	}
}
