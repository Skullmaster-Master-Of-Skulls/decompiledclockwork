using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000074 RID: 116
	internal sealed class DirectoryMonitor : IDisposable
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000AD89 File Offset: 0x00008F89
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0000AD91 File Offset: 0x00008F91
		internal int FcnMode { get; set; }

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000AD9A File Offset: 0x00008F9A
		internal DirectoryMonitor(string appPathInternal, int fcnMode) : this(appPathInternal, true, 347U, fcnMode)
		{
			this._isDirMonAppPathInternal = true;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000ADB1 File Offset: 0x00008FB1
		internal DirectoryMonitor(string dir, bool watchSubtree, uint notifyFilter, int fcnMode) : this(dir, watchSubtree, notifyFilter, false, fcnMode)
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000ADBF File Offset: 0x00008FBF
		internal DirectoryMonitor(string dir, bool watchSubtree, uint notifyFilter, bool ignoreSubdirChange, int fcnMode)
		{
			this.Directory = dir;
			this._fileMons = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this._watchSubtree = watchSubtree;
			this._notifyFilter = notifyFilter;
			this._ignoreSubdirChange = ignoreSubdirChange;
			this.FcnMode = fcnMode;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000ADFC File Offset: 0x00008FFC
		void IDisposable.Dispose()
		{
			if (this._dirMonCompletion != null)
			{
				((IDisposable)this._dirMonCompletion).Dispose();
				this._dirMonCompletion = null;
			}
			if (this._anyFileMon != null)
			{
				HttpRuntime.FileChangesMonitor.RemoveAliases(this._anyFileMon);
				this._anyFileMon = null;
			}
			foreach (object obj in this._fileMons)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string b = (string)dictionaryEntry.Key;
				FileMonitor fileMonitor = (FileMonitor)dictionaryEntry.Value;
				if (fileMonitor.FileNameLong == b)
				{
					HttpRuntime.FileChangesMonitor.RemoveAliases(fileMonitor);
				}
			}
			this._fileMons.Clear();
			this._cShortNames = 0;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000AED0 File Offset: 0x000090D0
		internal bool IsMonitoring()
		{
			return this.GetFileMonitorsCount() > 0;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000AEDB File Offset: 0x000090DB
		private void StartMonitoring()
		{
			if (this._dirMonCompletion == null)
			{
				this._dirMonCompletion = new DirMonCompletion(this, this.Directory, this._watchSubtree, this._notifyFilter);
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000AF04 File Offset: 0x00009104
		internal void StopMonitoring()
		{
			lock (this)
			{
				((IDisposable)this).Dispose();
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000AF40 File Offset: 0x00009140
		private FileMonitor FindFileMonitor(string file)
		{
			FileMonitor result;
			if (file == null)
			{
				result = this._anyFileMon;
			}
			else
			{
				result = (FileMonitor)this._fileMons[file];
			}
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000AF6C File Offset: 0x0000916C
		private FileMonitor AddFileMonitor(string file)
		{
			FindFileData findFileData = null;
			FileMonitor fileMonitor;
			if (string.IsNullOrEmpty(file))
			{
				fileMonitor = new FileMonitor(this, null, null, true, null, null);
				this._anyFileMon = fileMonitor;
			}
			else
			{
				string text = Path.Combine(this.Directory, file);
				int num;
				if (this._isDirMonAppPathInternal)
				{
					num = FindFileData.FindFile(text, this.Directory, out findFileData);
				}
				else
				{
					num = FindFileData.FindFile(text, out findFileData);
				}
				if (num == 0)
				{
					if (!this._isDirMonAppPathInternal && (findFileData.FileAttributesData.FileAttributes & FileAttributes.Directory) != (FileAttributes)0)
					{
						throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, text);
					}
					byte[] dacl = FileSecurity.GetDacl(text);
					fileMonitor = new FileMonitor(this, findFileData.FileNameLong, findFileData.FileNameShort, true, findFileData.FileAttributesData, dacl);
					this._fileMons.Add(findFileData.FileNameLong, fileMonitor);
					this.UpdateFileNameShort(fileMonitor, null, findFileData.FileNameShort);
				}
				else
				{
					if (num != -2147024893 && num != -2147024894)
					{
						throw FileChangesMonitor.CreateFileMonitoringException(num, text);
					}
					if (file.IndexOf('~') != -1)
					{
						throw FileChangesMonitor.CreateFileMonitoringException(-2147024809, text);
					}
					fileMonitor = new FileMonitor(this, file, null, false, null, null);
					this._fileMons.Add(file, fileMonitor);
				}
			}
			return fileMonitor;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000B084 File Offset: 0x00009284
		private void UpdateFileNameShort(FileMonitor fileMon, string oldFileNameShort, string newFileNameShort)
		{
			if (oldFileNameShort != null)
			{
				FileMonitor fileMonitor = (FileMonitor)this._fileMons[oldFileNameShort];
				if (fileMonitor != null)
				{
					if (fileMonitor != fileMon)
					{
						fileMonitor.RemoveFileNameShort();
					}
					this._fileMons.Remove(oldFileNameShort);
					this._cShortNames--;
				}
			}
			if (newFileNameShort != null)
			{
				this._fileMons.Add(newFileNameShort, fileMon);
				this._cShortNames++;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000B0EC File Offset: 0x000092EC
		private void RemoveFileMonitor(FileMonitor fileMon)
		{
			if (fileMon == this._anyFileMon)
			{
				this._anyFileMon = null;
			}
			else
			{
				this._fileMons.Remove(fileMon.FileNameLong);
				if (fileMon.FileNameShort != null)
				{
					this._fileMons.Remove(fileMon.FileNameShort);
					this._cShortNames--;
				}
			}
			HttpRuntime.FileChangesMonitor.RemoveAliases(fileMon);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000B150 File Offset: 0x00009350
		private int GetFileMonitorsCount()
		{
			int num = this._fileMons.Count - this._cShortNames;
			if (this._anyFileMon != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000B180 File Offset: 0x00009380
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal FileMonitor StartMonitoringFileWithAssert(string file, FileChangeEventHandler callback, string alias)
		{
			FileMonitor fileMonitor = null;
			bool flag = false;
			lock (this)
			{
				fileMonitor = this.FindFileMonitor(file);
				if (fileMonitor == null)
				{
					fileMonitor = this.AddFileMonitor(file);
					if (this.GetFileMonitorsCount() == 1)
					{
						flag = true;
					}
				}
				fileMonitor.AddTarget(callback, alias, true);
				if (flag)
				{
					this.StartMonitoring();
				}
			}
			return fileMonitor;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000B1EC File Offset: 0x000093EC
		internal void StopMonitoringFile(string file, object target)
		{
			lock (this)
			{
				FileMonitor fileMonitor = this.FindFileMonitor(file);
				if (fileMonitor != null && fileMonitor.RemoveTarget(target) == 0)
				{
					this.RemoveFileMonitor(fileMonitor);
					if (this.GetFileMonitorsCount() == 0)
					{
						((IDisposable)this).Dispose();
					}
				}
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000B24C File Offset: 0x0000944C
		internal bool GetFileAttributes(string file, out FileAttributesData fad)
		{
			fad = null;
			lock (this)
			{
				FileMonitor fileMonitor = this.FindFileMonitor(file);
				if (fileMonitor != null)
				{
					fad = fileMonitor.Attributes;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000B2A0 File Offset: 0x000094A0
		private bool IsChangeAfterStartMonitoring(FileAttributesData fad, FileMonitorTarget target, DateTime utcCompletion)
		{
			return fad.UtcLastAccessTime.AddSeconds(60.0) < target.UtcStartMonitoring || utcCompletion > target.UtcStartMonitoring || fad.UtcLastAccessTime < fad.UtcLastWriteTime || fad.UtcLastAccessTime.TimeOfDay == TimeSpan.Zero || fad.UtcLastAccessTime >= target.UtcStartMonitoring;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000B32C File Offset: 0x0000952C
		private bool GetFileMonitorForSpecialDirectory(string fileName, ref FileMonitor fileMon)
		{
			for (int i = 0; i < FileChangesMonitor.s_dirsToMonitor.Length; i++)
			{
				if (StringUtil.StringStartsWithIgnoreCase(fileName, FileChangesMonitor.s_dirsToMonitor[i]))
				{
					fileMon = (FileMonitor)this._fileMons[FileChangesMonitor.s_dirsToMonitor[i]];
					return fileMon != null;
				}
			}
			int num = fileName.IndexOf("App_LocalResources", StringComparison.OrdinalIgnoreCase);
			if (num > -1)
			{
				int num2 = num + "App_LocalResources".Length;
				if (fileName.Length == num2 || fileName[num2] == Path.DirectorySeparatorChar)
				{
					string key = fileName.Substring(0, num2);
					fileMon = (FileMonitor)this._fileMons[key];
					return fileMon != null;
				}
			}
			return false;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000B3D4 File Offset: 0x000095D4
		internal void OnFileChange(FileAction action, string fileName, DateTime utcCompletion)
		{
			try
			{
				FileMonitor fileMonitor = null;
				ArrayList arrayList = null;
				FileAttributesData fileAttributesData = null;
				FileAttributesData fileAttributesData2 = null;
				byte[] array = null;
				byte[] array2 = null;
				FileAction fileAction = FileAction.Error;
				DateTime dateTime = DateTime.MinValue;
				bool flag = false;
				if (this._dirMonCompletion != null)
				{
					lock (this)
					{
						if (this._fileMons.Count > 0)
						{
							if (action == FileAction.Error || action == FileAction.Overwhelming)
							{
								if (action == FileAction.Overwhelming && Interlocked.Increment(ref DirectoryMonitor.s_notificationBufferSizeIncreased) == 1)
								{
									UnsafeNativeMethods.GrowFileNotificationBuffer(HttpRuntime.AppDomainAppId, this._watchSubtree);
								}
								arrayList = new ArrayList();
								foreach (object obj in this._fileMons)
								{
									DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
									string b = (string)dictionaryEntry.Key;
									fileMonitor = (FileMonitor)dictionaryEntry.Value;
									if (fileMonitor.FileNameLong == b)
									{
										fileMonitor.ResetCachedAttributes();
										fileMonitor.LastAction = action;
										fileMonitor.UtcLastCompletion = utcCompletion;
										ICollection targets = fileMonitor.Targets;
										arrayList.AddRange(targets);
									}
								}
								fileMonitor = null;
							}
							else
							{
								fileMonitor = (FileMonitor)this._fileMons[fileName];
								if (this._isDirMonAppPathInternal && fileMonitor == null)
								{
									flag = this.GetFileMonitorForSpecialDirectory(fileName, ref fileMonitor);
								}
								if (fileMonitor != null)
								{
									ICollection targets = fileMonitor.Targets;
									arrayList = new ArrayList(targets);
									fileAttributesData = fileMonitor.Attributes;
									array = fileMonitor.Dacl;
									fileAction = fileMonitor.LastAction;
									dateTime = fileMonitor.UtcLastCompletion;
									fileMonitor.LastAction = action;
									fileMonitor.UtcLastCompletion = utcCompletion;
									if (action == FileAction.Removed || action == FileAction.RenamedOldName)
									{
										fileMonitor.MakeExtinct();
									}
									else if (fileMonitor.Exists)
									{
										if (dateTime != utcCompletion)
										{
											fileMonitor.UpdateCachedAttributes();
										}
									}
									else
									{
										FindFileData findFileData = null;
										string text = Path.Combine(this.Directory, fileMonitor.FileNameLong);
										int num;
										if (this._isDirMonAppPathInternal)
										{
											num = FindFileData.FindFile(text, this.Directory, out findFileData);
										}
										else
										{
											num = FindFileData.FindFile(text, out findFileData);
										}
										if (num == 0)
										{
											string fileNameShort = fileMonitor.FileNameShort;
											byte[] dacl = FileSecurity.GetDacl(text);
											fileMonitor.MakeExist(findFileData, dacl);
											this.UpdateFileNameShort(fileMonitor, fileNameShort, findFileData.FileNameShort);
										}
									}
									fileAttributesData2 = fileMonitor.Attributes;
									array2 = fileMonitor.Dacl;
								}
							}
						}
						if (this._anyFileMon != null)
						{
							ICollection targets = this._anyFileMon.Targets;
							if (arrayList != null)
							{
								arrayList.AddRange(targets);
							}
							else
							{
								arrayList = new ArrayList(targets);
							}
						}
						if (action == FileAction.Error)
						{
							((IDisposable)this).Dispose();
						}
					}
					bool flag3 = false;
					if (!flag && fileName != null && action == FileAction.Modified)
					{
						FileAttributesData fileAttributesData3 = fileAttributesData2;
						if (fileAttributesData3 == null)
						{
							string path = Path.Combine(this.Directory, fileName);
							FileAttributesData.GetFileAttributes(path, out fileAttributesData3);
						}
						if (fileAttributesData3 != null && (fileAttributesData3.FileAttributes & FileAttributes.Directory) != (FileAttributes)0)
						{
							flag3 = true;
						}
					}
					if (this._ignoreSubdirChange && (action == FileAction.Removed || action == FileAction.RenamedOldName) && fileName != null)
					{
						string fullPath = Path.Combine(this.Directory, fileName);
						if (!HttpRuntime.FileChangesMonitor.IsDirNameMonitored(fullPath, fileName))
						{
							flag3 = true;
						}
					}
					if (arrayList != null && !flag3)
					{
						object syncRoot = DirectoryMonitor.s_notificationQueue.SyncRoot;
						lock (syncRoot)
						{
							int i = 0;
							int count = arrayList.Count;
							while (i < count)
							{
								FileMonitorTarget fileMonitorTarget = (FileMonitorTarget)arrayList[i];
								bool flag5;
								if ((action != FileAction.Added && action != FileAction.Modified) || fileAttributesData2 == null)
								{
									flag5 = true;
								}
								else if (action == FileAction.Added)
								{
									flag5 = this.IsChangeAfterStartMonitoring(fileAttributesData2, fileMonitorTarget, utcCompletion);
								}
								else if (utcCompletion == dateTime)
								{
									flag5 = (fileAction != FileAction.Modified);
								}
								else
								{
									flag5 = (fileAttributesData == null || (array == null || array != array2) || this.IsChangeAfterStartMonitoring(fileAttributesData2, fileMonitorTarget, utcCompletion));
								}
								if (flag5)
								{
									DirectoryMonitor.s_notificationQueue.Enqueue(new NotificationQueueItem(fileMonitorTarget.Callback, action, fileMonitorTarget.Alias));
								}
								i++;
							}
						}
						if (DirectoryMonitor.s_notificationQueue.Count > 0 && DirectoryMonitor.s_inNotificationThread == 0 && Interlocked.Exchange(ref DirectoryMonitor.s_inNotificationThread, 1) == 0)
						{
							WorkItem.PostInternal(DirectoryMonitor.s_notificationCallback);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000B828 File Offset: 0x00009A28
		private static void FireNotifications()
		{
			try
			{
				do
				{
					NotificationQueueItem notificationQueueItem = null;
					object syncRoot = DirectoryMonitor.s_notificationQueue.SyncRoot;
					lock (syncRoot)
					{
						if (DirectoryMonitor.s_notificationQueue.Count > 0)
						{
							notificationQueueItem = (NotificationQueueItem)DirectoryMonitor.s_notificationQueue.Dequeue();
						}
					}
					if (notificationQueueItem != null)
					{
						try
						{
							notificationQueueItem.Callback(null, new FileChangeEvent(notificationQueueItem.Action, notificationQueueItem.Filename));
							continue;
						}
						catch (Exception ex)
						{
							continue;
						}
					}
					Interlocked.Exchange(ref DirectoryMonitor.s_inNotificationThread, 0);
				}
				while (DirectoryMonitor.s_notificationQueue.Count != 0 && Interlocked.Exchange(ref DirectoryMonitor.s_inNotificationThread, 1) == 0);
			}
			catch
			{
				Interlocked.Exchange(ref DirectoryMonitor.s_inNotificationThread, 0);
			}
		}

		// Token: 0x04000216 RID: 534
		private static Queue s_notificationQueue = new Queue();

		// Token: 0x04000217 RID: 535
		private static WorkItemCallback s_notificationCallback = new WorkItemCallback(DirectoryMonitor.FireNotifications);

		// Token: 0x04000218 RID: 536
		private static int s_inNotificationThread;

		// Token: 0x04000219 RID: 537
		private static int s_notificationBufferSizeIncreased = 0;

		// Token: 0x0400021A RID: 538
		internal readonly string Directory;

		// Token: 0x0400021B RID: 539
		private Hashtable _fileMons;

		// Token: 0x0400021C RID: 540
		private int _cShortNames;

		// Token: 0x0400021D RID: 541
		private FileMonitor _anyFileMon;

		// Token: 0x0400021E RID: 542
		private bool _watchSubtree;

		// Token: 0x0400021F RID: 543
		private uint _notifyFilter;

		// Token: 0x04000220 RID: 544
		private bool _ignoreSubdirChange;

		// Token: 0x04000221 RID: 545
		private DirMonCompletion _dirMonCompletion;

		// Token: 0x04000222 RID: 546
		private bool _isDirMonAppPathInternal;
	}
}
