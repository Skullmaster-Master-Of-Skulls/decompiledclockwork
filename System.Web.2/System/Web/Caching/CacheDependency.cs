using System;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web.Caching
{
	// Token: 0x0200087D RID: 2173
	public class CacheDependency : IDisposable
	{
		// Token: 0x06006630 RID: 26160 RVA: 0x00167D31 File Offset: 0x00165F31
		static CacheDependency()
		{
			CacheDependency.s_stringsEmpty = new string[0];
			CacheDependency.s_entriesEmpty = new CacheDependency.DepCacheInfo[0];
			CacheDependency.s_dependencyEmpty = new CacheDependency(0);
			CacheDependency.s_depFileInfosEmpty = new CacheDependency.DepFileInfo[0];
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x000030B5 File Offset: 0x000012B5
		private CacheDependency(int bogus)
		{
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x00167D6C File Offset: 0x00165F6C
		protected CacheDependency()
		{
			this.Init(true, null, null, null, DateTime.MaxValue);
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x00167D83 File Offset: 0x00165F83
		public CacheDependency(string filename) : this(filename, DateTime.MaxValue)
		{
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x00167D94 File Offset: 0x00165F94
		public CacheDependency(string filename, DateTime start)
		{
			if (filename == null)
			{
				return;
			}
			DateTime utcStart = DateTimeUtil.ConvertToUniversalTime(start);
			string[] filenamesArg = new string[]
			{
				filename
			};
			this.Init(true, filenamesArg, null, null, utcStart);
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x00167DC8 File Offset: 0x00165FC8
		public CacheDependency(string[] filenames)
		{
			this.Init(true, filenames, null, null, DateTime.MaxValue);
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x00167DE0 File Offset: 0x00165FE0
		public CacheDependency(string[] filenames, DateTime start)
		{
			DateTime utcStart = DateTimeUtil.ConvertToUniversalTime(start);
			this.Init(true, filenames, null, null, utcStart);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x00167E05 File Offset: 0x00166005
		public CacheDependency(string[] filenames, string[] cachekeys)
		{
			this.Init(true, filenames, cachekeys, null, DateTime.MaxValue);
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x00167E1C File Offset: 0x0016601C
		public CacheDependency(string[] filenames, string[] cachekeys, DateTime start)
		{
			DateTime utcStart = DateTimeUtil.ConvertToUniversalTime(start);
			this.Init(true, filenames, cachekeys, null, utcStart);
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x00167E41 File Offset: 0x00166041
		public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency)
		{
			this.Init(true, filenames, cachekeys, dependency, DateTime.MaxValue);
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x00167E58 File Offset: 0x00166058
		public CacheDependency(string[] filenames, string[] cachekeys, CacheDependency dependency, DateTime start)
		{
			DateTime utcStart = DateTimeUtil.ConvertToUniversalTime(start);
			this.Init(true, filenames, cachekeys, dependency, utcStart);
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x00167E7E File Offset: 0x0016607E
		internal CacheDependency(int dummy, string filename) : this(dummy, filename, DateTime.MaxValue)
		{
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x00167E90 File Offset: 0x00166090
		internal CacheDependency(int dummy, string filename, DateTime utcStart)
		{
			if (filename == null)
			{
				return;
			}
			string[] filenamesArg = new string[]
			{
				filename
			};
			this.Init(false, filenamesArg, null, null, utcStart);
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x00167EBD File Offset: 0x001660BD
		internal CacheDependency(int dummy, string[] filenames)
		{
			this.Init(false, filenames, null, null, DateTime.MaxValue);
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x00167ED4 File Offset: 0x001660D4
		internal CacheDependency(int dummy, string[] filenames, DateTime utcStart)
		{
			this.Init(false, filenames, null, null, utcStart);
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x00167EE7 File Offset: 0x001660E7
		internal CacheDependency(int dummy, string[] filenames, string[] cachekeys)
		{
			this.Init(false, filenames, cachekeys, null, DateTime.MaxValue);
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x00167EFE File Offset: 0x001660FE
		internal CacheDependency(int dummy, string[] filenames, string[] cachekeys, DateTime utcStart)
		{
			this.Init(false, filenames, cachekeys, null, utcStart);
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x00167F12 File Offset: 0x00166112
		internal CacheDependency(int dummy, string[] filenames, string[] cachekeys, CacheDependency dependency)
		{
			this.Init(false, filenames, cachekeys, dependency, DateTime.MaxValue);
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x00167F2A File Offset: 0x0016612A
		internal CacheDependency(int dummy, string[] filenames, string[] cachekeys, CacheDependency dependency, DateTime utcStart)
		{
			this.Init(false, filenames, cachekeys, dependency, utcStart);
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x00167F40 File Offset: 0x00166140
		private void Init(bool isPublic, string[] filenamesArg, string[] cachekeysArg, CacheDependency dependency, DateTime utcStart)
		{
			CacheDependency.DepFileInfo[] array = CacheDependency.s_depFileInfosEmpty;
			CacheDependency.DepCacheInfo[] array2 = CacheDependency.s_entriesEmpty;
			this._bits = new SafeBitVector32(0);
			string[] array3;
			if (filenamesArg != null)
			{
				array3 = (string[])filenamesArg.Clone();
			}
			else
			{
				array3 = null;
			}
			string[] array4;
			if (cachekeysArg != null)
			{
				array4 = (string[])cachekeysArg.Clone();
			}
			else
			{
				array4 = null;
			}
			this._utcLastModified = DateTime.MinValue;
			try
			{
				if (array3 == null)
				{
					array3 = CacheDependency.s_stringsEmpty;
				}
				else
				{
					foreach (string text in array3)
					{
						if (text == null)
						{
							throw new ArgumentNullException("filenamesArg");
						}
						if (isPublic)
						{
							InternalSecurityPermissions.PathDiscovery(text).Demand();
						}
					}
				}
				if (array4 == null)
				{
					array4 = CacheDependency.s_stringsEmpty;
				}
				else
				{
					string[] array6 = array4;
					for (int j = 0; j < array6.Length; j++)
					{
						if (array6[j] == null)
						{
							throw new ArgumentNullException("cachekeysArg");
						}
					}
				}
				if (dependency == null)
				{
					dependency = CacheDependency.s_dependencyEmpty;
				}
				else
				{
					if (dependency.GetType() != CacheDependency.s_dependencyEmpty.GetType())
					{
						throw new ArgumentException(SR.GetString("Invalid_Dependency_Type"));
					}
					object depFileInfos = dependency._depFileInfos;
					object entries = dependency._entries;
					DateTime utcLastModified = dependency._utcLastModified;
					if (dependency._bits[4])
					{
						this._bits[4] = true;
						this.DisposeInternal();
						return;
					}
					if (depFileInfos != null)
					{
						if (depFileInfos is CacheDependency.DepFileInfo)
						{
							array = new CacheDependency.DepFileInfo[]
							{
								(CacheDependency.DepFileInfo)depFileInfos
							};
						}
						else
						{
							array = (CacheDependency.DepFileInfo[])depFileInfos;
						}
						foreach (CacheDependency.DepFileInfo depFileInfo in array)
						{
							string filename = depFileInfo._filename;
							if (filename == null)
							{
								this._bits[4] = true;
								this.DisposeInternal();
								return;
							}
							if (isPublic)
							{
								InternalSecurityPermissions.PathDiscovery(filename).Demand();
							}
						}
					}
					if (entries != null)
					{
						if (entries is CacheDependency.DepCacheInfo)
						{
							array2 = new CacheDependency.DepCacheInfo[]
							{
								(CacheDependency.DepCacheInfo)entries
							};
						}
						else
						{
							array2 = (CacheDependency.DepCacheInfo[])entries;
							CacheDependency.DepCacheInfo[] array8 = array2;
							for (int l = 0; l < array8.Length; l++)
							{
								if (array8[l] == null)
								{
									this._bits[4] = true;
									this.DisposeInternal();
									return;
								}
							}
						}
					}
					this._utcLastModified = utcLastModified;
				}
				int num = array.Length + array3.Length;
				if (num > 0)
				{
					CacheDependency.DepFileInfo[] array9 = new CacheDependency.DepFileInfo[num];
					FileChangeEventHandler callback = new FileChangeEventHandler(this.FileChange);
					FileChangesMonitor fileChangesMonitor = HttpRuntime.FileChangesMonitor;
					int m;
					for (m = 0; m < num; m++)
					{
						array9[m] = new CacheDependency.DepFileInfo();
					}
					m = 0;
					foreach (CacheDependency.DepFileInfo depFileInfo2 in array)
					{
						string filename2 = depFileInfo2._filename;
						fileChangesMonitor.StartMonitoringPath(filename2, callback, out array9[m]._fad);
						array9[m]._filename = filename2;
						m++;
					}
					DateTime dateTime = DateTime.MinValue;
					foreach (string text2 in array3)
					{
						DateTime dateTime2 = fileChangesMonitor.StartMonitoringPath(text2, callback, out array9[m]._fad);
						array9[m]._filename = text2;
						m++;
						if (dateTime2 > this._utcLastModified)
						{
							this._utcLastModified = dateTime2;
						}
						if (utcStart < DateTime.MaxValue)
						{
							if (dateTime == DateTime.MinValue)
							{
								dateTime = DateTime.UtcNow;
							}
							if (dateTime2 >= utcStart && !(dateTime2 - dateTime > CacheDependency.FUTURE_FILETIME_BUFFER))
							{
								this._bits[4] = true;
								break;
							}
						}
					}
					if (array9.Length == 1)
					{
						this._depFileInfos = array9[0];
					}
					else
					{
						this._depFileInfos = array9;
					}
				}
				int num3 = array2.Length + array4.Length;
				if (num3 > 0 && !this._bits[4])
				{
					CacheDependency.DepCacheInfo[] array12 = new CacheDependency.DepCacheInfo[num3];
					int num4 = 0;
					foreach (CacheDependency.DepCacheInfo depCacheInfo in array2)
					{
						DateTime dateTime3;
						depCacheInfo._cacheStore.AddDependent(depCacheInfo._key, this, out dateTime3);
						array12[num4++] = depCacheInfo;
					}
					CacheStoreProvider cacheStoreProvider = isPublic ? HttpRuntime.Cache.ObjectCache : HttpRuntime.Cache.InternalCache;
					foreach (string key in array4)
					{
						DateTime dateTime3;
						if (!cacheStoreProvider.AddDependent(key, this, out dateTime3))
						{
							this._bits[4] = true;
							break;
						}
						array12[num4++] = new CacheDependency.DepCacheInfo
						{
							_cacheStore = cacheStoreProvider,
							_key = key
						};
						if (dateTime3 > this._utcLastModified)
						{
							this._utcLastModified = dateTime3;
						}
						if (dateTime3 > utcStart)
						{
							this._bits[4] = true;
							break;
						}
					}
					if (array12.Length == 1)
					{
						this._entries = array12[0];
					}
					else
					{
						this._entries = array12;
					}
				}
				this._bits[1] = true;
				if (dependency._bits[4])
				{
					this._bits[4] = true;
				}
				if (this._bits[16] || this._bits[4])
				{
					this.DisposeInternal();
				}
			}
			catch
			{
				this._bits[1] = true;
				this.DisposeInternal();
				throw;
			}
			finally
			{
				this.InitUniqueID();
			}
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x001684B4 File Offset: 0x001666B4
		public void Dispose()
		{
			this._bits[32] = true;
			if (this.TakeOwnership())
			{
				this.DisposeInternal();
			}
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x001684D2 File Offset: 0x001666D2
		protected internal void FinishInit()
		{
			this._bits[32] = true;
			if (this._bits[16])
			{
				this.DisposeInternal();
			}
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x001684F8 File Offset: 0x001666F8
		internal void DisposeInternal()
		{
			this._bits[16] = true;
			if (this._bits[32] && this._bits.ChangeValue(64, true))
			{
				this.DependencyDispose();
			}
			if (this._bits[1] && this._bits.ChangeValue(8, true))
			{
				this.DisposeOurself();
			}
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void DependencyDispose()
		{
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x0016855C File Offset: 0x0016675C
		private void DisposeOurself()
		{
			object depFileInfos = this._depFileInfos;
			object entries = this._entries;
			this._objNotify = null;
			this._depFileInfos = null;
			this._entries = null;
			if (depFileInfos != null)
			{
				FileChangesMonitor fileChangesMonitor = HttpRuntime.FileChangesMonitor;
				CacheDependency.DepFileInfo depFileInfo = depFileInfos as CacheDependency.DepFileInfo;
				if (depFileInfo != null)
				{
					fileChangesMonitor.StopMonitoringPath(depFileInfo._filename, this);
				}
				else
				{
					CacheDependency.DepFileInfo[] array = (CacheDependency.DepFileInfo[])depFileInfos;
					foreach (CacheDependency.DepFileInfo depFileInfo2 in array)
					{
						string filename = depFileInfo2._filename;
						if (filename != null)
						{
							fileChangesMonitor.StopMonitoringPath(filename, this);
						}
					}
				}
			}
			if (entries != null)
			{
				CacheDependency.DepCacheInfo depCacheInfo = entries as CacheDependency.DepCacheInfo;
				if (depCacheInfo != null)
				{
					depCacheInfo._cacheStore.RemoveDependent(depCacheInfo._key, this);
					return;
				}
				CacheDependency.DepCacheInfo[] array3 = (CacheDependency.DepCacheInfo[])entries;
				foreach (CacheDependency.DepCacheInfo depCacheInfo2 in array3)
				{
					if (depCacheInfo2 != null)
					{
						depCacheInfo2._cacheStore.RemoveDependent(depCacheInfo2._key, this);
					}
				}
			}
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x0016864C File Offset: 0x0016684C
		public bool TakeOwnership()
		{
			return this._bits.ChangeValue(2, true);
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x0016865B File Offset: 0x0016685B
		public bool HasChanged
		{
			get
			{
				return this._bits[4];
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x0600664B RID: 26187 RVA: 0x00168669 File Offset: 0x00166869
		public DateTime UtcLastModified
		{
			get
			{
				return this._utcLastModified;
			}
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x00168671 File Offset: 0x00166871
		protected void SetUtcLastModified(DateTime utcLastModified)
		{
			this._utcLastModified = utcLastModified;
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x0016867C File Offset: 0x0016687C
		public void KeepDependenciesAlive()
		{
			object entries = this._entries;
			if (entries != null)
			{
				CacheDependency.DepCacheInfo depCacheInfo = entries as CacheDependency.DepCacheInfo;
				if (depCacheInfo != null)
				{
					depCacheInfo._cacheStore.Get(depCacheInfo._key);
					return;
				}
				foreach (CacheDependency.DepCacheInfo depCacheInfo2 in (CacheDependency.DepCacheInfo[])entries)
				{
					if (depCacheInfo2 != null)
					{
						object obj = depCacheInfo2._cacheStore.Get(depCacheInfo2._key);
					}
				}
			}
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x001686E3 File Offset: 0x001668E3
		public void SetCacheDependencyChanged(Action<object, EventArgs> dependencyChangedAction)
		{
			this._bits[32] = true;
			if (!this._bits[8])
			{
				this._objNotify = dependencyChangedAction;
			}
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x00168708 File Offset: 0x00166908
		internal void AppendFileUniqueId(CacheDependency.DepFileInfo depFileInfo, StringBuilder sb)
		{
			FileAttributesData fileAttributesData = depFileInfo._fad;
			if (fileAttributesData == null)
			{
				fileAttributesData = FileAttributesData.NonExistantAttributesData;
			}
			sb.Append(depFileInfo._filename);
			sb.Append(fileAttributesData.UtcLastWriteTime.Ticks.ToString("d", NumberFormatInfo.InvariantInfo));
			sb.Append(fileAttributesData.FileSize.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x00168774 File Offset: 0x00166974
		private void InitUniqueID()
		{
			StringBuilder stringBuilder = null;
			object depFileInfos = this._depFileInfos;
			if (depFileInfos != null)
			{
				CacheDependency.DepFileInfo depFileInfo = depFileInfos as CacheDependency.DepFileInfo;
				if (depFileInfo != null)
				{
					stringBuilder = new StringBuilder();
					this.AppendFileUniqueId(depFileInfo, stringBuilder);
				}
				else
				{
					CacheDependency.DepFileInfo[] array = (CacheDependency.DepFileInfo[])depFileInfos;
					foreach (CacheDependency.DepFileInfo depFileInfo2 in array)
					{
						if (depFileInfo2._filename != null)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							this.AppendFileUniqueId(depFileInfo2, stringBuilder);
						}
					}
				}
			}
			object entries = this._entries;
			if (entries != null)
			{
				CacheDependency.DepCacheInfo depCacheInfo = entries as CacheDependency.DepCacheInfo;
				if (depCacheInfo != null)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(depCacheInfo._key);
					stringBuilder.Append(depCacheInfo.GetHashCode().ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					CacheDependency.DepCacheInfo[] array3 = (CacheDependency.DepCacheInfo[])entries;
					foreach (CacheDependency.DepCacheInfo depCacheInfo2 in array3)
					{
						if (depCacheInfo2 != null)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							stringBuilder.Append(depCacheInfo2._key);
							stringBuilder.Append(depCacheInfo2.GetHashCode().ToString(CultureInfo.InvariantCulture));
						}
					}
				}
			}
			if (stringBuilder != null)
			{
				this._uniqueID = stringBuilder.ToString();
			}
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x001688A2 File Offset: 0x00166AA2
		public virtual string GetUniqueID()
		{
			return this._uniqueID;
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x001688AC File Offset: 0x00166AAC
		protected void NotifyDependencyChanged(object sender, EventArgs e)
		{
			if (this._bits.ChangeValue(4, true))
			{
				this._utcLastModified = DateTime.UtcNow;
				Action<object, EventArgs> objNotify = this._objNotify;
				if (objNotify != null && !this._bits[8])
				{
					objNotify(sender, e);
				}
				this.DisposeInternal();
			}
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x001688F9 File Offset: 0x00166AF9
		public void ItemRemoved()
		{
			this.NotifyDependencyChanged(this, EventArgs.Empty);
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x00168907 File Offset: 0x00166B07
		private void FileChange(object sender, FileChangeEvent e)
		{
			this.NotifyDependencyChanged(sender, e);
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x00168914 File Offset: 0x00166B14
		internal virtual bool IsFileDependency()
		{
			object entries = this._entries;
			if (entries != null)
			{
				CacheDependency.DepCacheInfo depCacheInfo = entries as CacheDependency.DepCacheInfo;
				if (depCacheInfo != null)
				{
					return false;
				}
				CacheDependency.DepCacheInfo[] array = (CacheDependency.DepCacheInfo[])entries;
				if (array != null && array.Length != 0)
				{
					return false;
				}
			}
			object depFileInfos = this._depFileInfos;
			if (depFileInfos != null)
			{
				CacheDependency.DepFileInfo depFileInfo = depFileInfos as CacheDependency.DepFileInfo;
				if (depFileInfo != null)
				{
					return true;
				}
				CacheDependency.DepFileInfo[] array2 = (CacheDependency.DepFileInfo[])depFileInfos;
				if (array2 != null && array2.Length != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x00168974 File Offset: 0x00166B74
		public virtual string[] GetFileDependencies()
		{
			object depFileInfos = this._depFileInfos;
			if (depFileInfos == null)
			{
				return null;
			}
			CacheDependency.DepFileInfo depFileInfo = depFileInfos as CacheDependency.DepFileInfo;
			if (depFileInfo != null)
			{
				return new string[]
				{
					depFileInfo._filename
				};
			}
			CacheDependency.DepFileInfo[] array = (CacheDependency.DepFileInfo[])depFileInfos;
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i]._filename;
			}
			return array2;
		}

		// Token: 0x040034A6 RID: 13478
		private string _uniqueID;

		// Token: 0x040034A7 RID: 13479
		private object _depFileInfos;

		// Token: 0x040034A8 RID: 13480
		private object _entries;

		// Token: 0x040034A9 RID: 13481
		private Action<object, EventArgs> _objNotify;

		// Token: 0x040034AA RID: 13482
		private SafeBitVector32 _bits;

		// Token: 0x040034AB RID: 13483
		private DateTime _utcLastModified;

		// Token: 0x040034AC RID: 13484
		private static readonly string[] s_stringsEmpty;

		// Token: 0x040034AD RID: 13485
		private static readonly CacheDependency.DepCacheInfo[] s_entriesEmpty;

		// Token: 0x040034AE RID: 13486
		private static readonly CacheDependency s_dependencyEmpty;

		// Token: 0x040034AF RID: 13487
		private static readonly CacheDependency.DepFileInfo[] s_depFileInfosEmpty;

		// Token: 0x040034B0 RID: 13488
		private static readonly TimeSpan FUTURE_FILETIME_BUFFER = new TimeSpan(0, 1, 0);

		// Token: 0x040034B1 RID: 13489
		private const int BASE_INIT = 1;

		// Token: 0x040034B2 RID: 13490
		private const int USED = 2;

		// Token: 0x040034B3 RID: 13491
		private const int CHANGED = 4;

		// Token: 0x040034B4 RID: 13492
		private const int BASE_DISPOSED = 8;

		// Token: 0x040034B5 RID: 13493
		private const int WANTS_DISPOSE = 16;

		// Token: 0x040034B6 RID: 13494
		private const int DERIVED_INIT = 32;

		// Token: 0x040034B7 RID: 13495
		private const int DERIVED_DISPOSED = 64;

		// Token: 0x02000A74 RID: 2676
		internal class DepFileInfo
		{
			// Token: 0x04003BB1 RID: 15281
			internal string _filename;

			// Token: 0x04003BB2 RID: 15282
			internal FileAttributesData _fad;
		}

		// Token: 0x02000A75 RID: 2677
		internal class DepCacheInfo
		{
			// Token: 0x04003BB3 RID: 15283
			internal CacheStoreProvider _cacheStore;

			// Token: 0x04003BB4 RID: 15284
			internal string _key;
		}
	}
}
