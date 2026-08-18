using System;
using System.IO;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000822 RID: 2082
	internal abstract class DiskBuildResultCache : BuildResultCache
	{
		// Token: 0x0600638C RID: 25484 RVA: 0x0015CCD8 File Offset: 0x0015AED8
		internal DiskBuildResultCache(string cacheDir)
		{
			this._cacheDir = cacheDir;
			if (DiskBuildResultCache.s_maxRecompilations < 0)
			{
				DiskBuildResultCache.s_maxRecompilations = CompilationUtil.GetRecompilationsBeforeAppRestarts();
			}
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x0015CCFC File Offset: 0x0015AEFC
		protected void EnsureDiskCacheDirectoryCreated()
		{
			if (!FileUtil.DirectoryExists(this._cacheDir))
			{
				try
				{
					Directory.CreateDirectory(this._cacheDir);
				}
				catch (IOException innerException)
				{
					throw new HttpException(SR.GetString("Failed_to_create_temp_dir", new object[]
					{
						HttpRuntime.GetSafePath(this._cacheDir)
					}), innerException);
				}
			}
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x0015CD5C File Offset: 0x0015AF5C
		internal override BuildResult GetBuildResult(string cacheKey, VirtualPath virtualPath, long hashCode, bool ensureIsUpToDate)
		{
			string preservedDataFileName = this.GetPreservedDataFileName(cacheKey);
			PreservationFileReader preservationFileReader = new PreservationFileReader(this, this.PrecompilationMode);
			return preservationFileReader.ReadBuildResultFromFile(virtualPath, preservedDataFileName, hashCode, ensureIsUpToDate);
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x0015CD90 File Offset: 0x0015AF90
		internal override void CacheBuildResult(string cacheKey, BuildResult result, long hashCode, DateTime utcStart)
		{
			if (!result.CacheToDisk)
			{
				return;
			}
			if (HostingEnvironment.ShutdownInitiated)
			{
				BuildResultCompiledAssemblyBase buildResultCompiledAssemblyBase = result as BuildResultCompiledAssemblyBase;
				if (buildResultCompiledAssemblyBase != null && buildResultCompiledAssemblyBase.ResultAssembly != null && !buildResultCompiledAssemblyBase.UsesExistingAssembly)
				{
					this.MarkAssemblyAndRelatedFilesForDeletion(buildResultCompiledAssemblyBase.ResultAssembly.GetName().Name);
				}
				return;
			}
			string preservedDataFileName = this.GetPreservedDataFileName(cacheKey);
			PreservationFileWriter preservationFileWriter = new PreservationFileWriter(this.PrecompilationMode);
			preservationFileWriter.SaveBuildResultToFile(preservedDataFileName, result, hashCode);
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x0015CE04 File Offset: 0x0015B004
		private void MarkAssemblyAndRelatedFilesForDeletion(string assemblyName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this._cacheDir);
			string str = assemblyName.Substring("App_Web_".Length);
			FileInfo[] files = directoryInfo.GetFiles("*" + str + ".*");
			foreach (FileInfo f in files)
			{
				DiskBuildResultCache.CreateDotDeleteFile(f);
			}
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x0015CE66 File Offset: 0x0015B066
		private string GetPreservedDataFileName(string cacheKey)
		{
			cacheKey = Util.MakeValidFileName(cacheKey);
			cacheKey = Path.Combine(this._cacheDir, cacheKey);
			cacheKey = FileUtil.TruncatePathIfNeeded(cacheKey, 9);
			return cacheKey + ".compiled";
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool PrecompilationMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x06006393 RID: 25491 RVA: 0x0015CE93 File Offset: 0x0015B093
		internal static bool InUseAssemblyWasDeleted
		{
			get
			{
				return DiskBuildResultCache.s_inUseAssemblyWasDeleted;
			}
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x0015CE9A File Offset: 0x0015B09A
		internal static void ResetAssemblyDeleted()
		{
			DiskBuildResultCache.s_inUseAssemblyWasDeleted = false;
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x0015CEA4 File Offset: 0x0015B0A4
		internal virtual void RemoveAssemblyAndRelatedFiles(string assemblyName)
		{
			if (!assemblyName.StartsWith("App_Web_", StringComparison.Ordinal))
			{
				return;
			}
			string str = assemblyName.Substring("App_Web_".Length);
			bool flag = false;
			try
			{
				CompilationLock.GetLock(ref flag);
				DirectoryInfo directoryInfo = new DirectoryInfo(this._cacheDir);
				FileInfo[] files = directoryInfo.GetFiles("*" + str + ".*");
				foreach (FileInfo fileInfo in files)
				{
					if (fileInfo.Extension == ".dll")
					{
						string assemblyCacheKey = BuildResultCache.GetAssemblyCacheKey(fileInfo.FullName);
						HttpRuntime.Cache.InternalCache.Remove(assemblyCacheKey);
						DiskBuildResultCache.RemoveAssembly(fileInfo);
						StandardDiskBuildResultCache.RemoveSatelliteAssemblies(assemblyName);
					}
					else if (fileInfo.Extension == ".delete")
					{
						DiskBuildResultCache.CheckAndRemoveDotDeleteFile(fileInfo);
					}
					else
					{
						DiskBuildResultCache.TryDeleteFile(fileInfo);
					}
				}
			}
			finally
			{
				if (flag)
				{
					CompilationLock.ReleaseLock();
				}
				DiskBuildResultCache.ShutDownAppDomainIfRequired();
			}
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x0015CFA4 File Offset: 0x0015B1A4
		internal static void RemoveAssembly(FileInfo f)
		{
			if (HostingEnvironment.ShutdownInitiated)
			{
				DiskBuildResultCache.CreateDotDeleteFile(f);
				return;
			}
			if (DiskBuildResultCache.HasDotDeleteFile(f.FullName))
			{
				return;
			}
			if (DiskBuildResultCache.TryDeleteFile(f))
			{
				return;
			}
			if (++DiskBuildResultCache.s_recompilations == DiskBuildResultCache.s_maxRecompilations)
			{
				DiskBuildResultCache.s_shutdownStatus = 1;
			}
			DiskBuildResultCache.s_inUseAssemblyWasDeleted = true;
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0015CFF6 File Offset: 0x0015B1F6
		internal static void ShutDownAppDomainIfRequired()
		{
			if (DiskBuildResultCache.s_shutdownStatus == 1 && Interlocked.Exchange(ref DiskBuildResultCache.s_shutdownStatus, 2) == 1)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(DiskBuildResultCache.ShutdownCallBack));
			}
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x0015D020 File Offset: 0x0015B220
		private static void ShutdownCallBack(object state)
		{
			HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.MaxRecompilationsReached, "Recompilation limit of " + DiskBuildResultCache.s_maxRecompilations.ToString() + " reached");
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x0015D043 File Offset: 0x0015B243
		internal static bool TryDeleteFile(string s)
		{
			return DiskBuildResultCache.TryDeleteFile(new FileInfo(s));
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x0015D050 File Offset: 0x0015B250
		internal static bool TryDeleteFile(FileInfo f)
		{
			if (f.Extension == ".delete")
			{
				return DiskBuildResultCache.CheckAndRemoveDotDeleteFile(f);
			}
			try
			{
				f.Delete();
				return true;
			}
			catch
			{
			}
			DiskBuildResultCache.CreateDotDeleteFile(f);
			return false;
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x0015D0A0 File Offset: 0x0015B2A0
		internal static bool CheckAndRemoveDotDeleteFile(FileInfo f)
		{
			if (f.Extension != ".delete")
			{
				return false;
			}
			string text = Path.GetDirectoryName(f.FullName) + Path.DirectorySeparatorChar.ToString() + Path.GetFileNameWithoutExtension(f.FullName);
			if (FileUtil.FileExists(text))
			{
				try
				{
					File.Delete(text);
				}
				catch
				{
					return false;
				}
			}
			try
			{
				f.Delete();
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x0600639C RID: 25500 RVA: 0x0015D12C File Offset: 0x0015B32C
		internal static bool HasDotDeleteFile(string s)
		{
			return File.Exists(s + ".delete");
		}

		// Token: 0x0600639D RID: 25501 RVA: 0x0015D140 File Offset: 0x0015B340
		private static void CreateDotDeleteFile(FileInfo f)
		{
			if (f.Extension == ".delete")
			{
				return;
			}
			string path = f.FullName + ".delete";
			if (!File.Exists(path))
			{
				try
				{
					new StreamWriter(path).Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0400338D RID: 13197
		protected const string preservationFileExtension = ".compiled";

		// Token: 0x0400338E RID: 13198
		protected string _cacheDir;

		// Token: 0x0400338F RID: 13199
		private static int s_recompilations;

		// Token: 0x04003390 RID: 13200
		private static int s_maxRecompilations = -1;

		// Token: 0x04003391 RID: 13201
		private static bool s_inUseAssemblyWasDeleted;

		// Token: 0x04003392 RID: 13202
		protected const string dotDelete = ".delete";

		// Token: 0x04003393 RID: 13203
		private static int s_shutdownStatus;

		// Token: 0x04003394 RID: 13204
		private const int SHUTDOWN_NEEDED = 1;

		// Token: 0x04003395 RID: 13205
		private const int SHUTDOWN_STARTED = 2;
	}
}
