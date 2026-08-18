using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000811 RID: 2065
	internal abstract class BuildResult
	{
		// Token: 0x060062F0 RID: 25328 RVA: 0x0015B5CC File Offset: 0x001597CC
		internal static BuildResult CreateBuildResultFromCode(BuildResultTypeCode code, VirtualPath virtualPath)
		{
			BuildResult buildResult;
			switch (code)
			{
			case BuildResultTypeCode.BuildResultCompiledAssembly:
				buildResult = new BuildResultCompiledAssembly();
				goto IL_72;
			case BuildResultTypeCode.BuildResultCompiledType:
				buildResult = new BuildResultCompiledType();
				goto IL_72;
			case BuildResultTypeCode.BuildResultCompiledTemplateType:
				buildResult = new BuildResultCompiledTemplateType();
				goto IL_72;
			case BuildResultTypeCode.BuildResultCustomString:
				buildResult = new BuildResultCustomString();
				goto IL_72;
			case BuildResultTypeCode.BuildResultMainCodeAssembly:
				buildResult = new BuildResultMainCodeAssembly();
				goto IL_72;
			case BuildResultTypeCode.BuildResultCodeCompileUnit:
				buildResult = new BuildResultCodeCompileUnit();
				goto IL_72;
			case BuildResultTypeCode.BuildResultCompiledGlobalAsaxType:
				buildResult = new BuildResultCompiledGlobalAsaxType();
				goto IL_72;
			case BuildResultTypeCode.BuildResultResourceAssembly:
				buildResult = new BuildResultResourceAssembly();
				goto IL_72;
			}
			return null;
			IL_72:
			buildResult.VirtualPath = virtualPath;
			buildResult._nextUpToDateCheck = DateTime.MinValue;
			return buildResult;
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x0015B65E File Offset: 0x0015985E
		internal virtual BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.Invalid;
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x060062F2 RID: 25330 RVA: 0x0015B661 File Offset: 0x00159861
		// (set) Token: 0x060062F3 RID: 25331 RVA: 0x0015B66E File Offset: 0x0015986E
		internal int Flags
		{
			get
			{
				return this._flags.IntegerValue;
			}
			set
			{
				this._flags.IntegerValue = value;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x060062F4 RID: 25332 RVA: 0x0015B67C File Offset: 0x0015987C
		// (set) Token: 0x060062F5 RID: 25333 RVA: 0x0015B684 File Offset: 0x00159884
		internal VirtualPath VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x0015B68D File Offset: 0x0015988D
		// (set) Token: 0x060062F7 RID: 25335 RVA: 0x0015B69F File Offset: 0x0015989F
		internal bool UsesCacheDependency
		{
			get
			{
				return this._flags[65536];
			}
			set
			{
				this._flags[65536] = value;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x0015B6B2 File Offset: 0x001598B2
		internal bool ShutdownAppDomainOnChange
		{
			get
			{
				return this._flags[1];
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x0015B6C0 File Offset: 0x001598C0
		internal ICollection VirtualPathDependencies
		{
			get
			{
				return this._virtualPathDependencies;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x060062FA RID: 25338 RVA: 0x0015B6C8 File Offset: 0x001598C8
		// (set) Token: 0x060062FB RID: 25339 RVA: 0x0015B6D6 File Offset: 0x001598D6
		internal string VirtualPathDependenciesHash
		{
			get
			{
				this.EnsureVirtualPathDependenciesHashComputed();
				return this._virtualPathDependenciesHash;
			}
			set
			{
				this._virtualPathDependenciesHash = value;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x060062FC RID: 25340 RVA: 0x0015B6DF File Offset: 0x001598DF
		internal bool DependenciesHashComputed
		{
			get
			{
				return this._flags[1048576];
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x0015B6F1 File Offset: 0x001598F1
		internal void EnsureVirtualPathDependenciesHashComputed()
		{
			if (!this.DependenciesHashComputed)
			{
				if (this._virtualPathDependencies != null)
				{
					this._virtualPathDependencies.Sort(InvariantComparer.Default);
				}
				this._virtualPathDependenciesHash = this.ComputeSourceDependenciesHashCode(null);
				this._flags[1048576] = true;
			}
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x0015B731 File Offset: 0x00159931
		internal void SetVirtualPathDependencies(ArrayList sourceDependencies)
		{
			this._virtualPathDependencies = sourceDependencies;
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x0015B73A File Offset: 0x0015993A
		internal void AddVirtualPathDependencies(ICollection sourceDependencies)
		{
			if (sourceDependencies == null)
			{
				return;
			}
			if (this._virtualPathDependencies == null)
			{
				this._virtualPathDependencies = new ArrayList(sourceDependencies);
				return;
			}
			this._virtualPathDependencies.AddRange(sourceDependencies);
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06006300 RID: 25344 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool IsUnloadable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06006301 RID: 25345 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool CacheToDisk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x06006302 RID: 25346 RVA: 0x0015B761 File Offset: 0x00159961
		// (set) Token: 0x06006303 RID: 25347 RVA: 0x0015B776 File Offset: 0x00159976
		internal bool CacheToMemory
		{
			get
			{
				return !this._flags[262144];
			}
			set
			{
				this._flags[262144] = !value;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x06006304 RID: 25348 RVA: 0x0015B78C File Offset: 0x0015998C
		internal virtual DateTime MemoryCacheExpiration
		{
			get
			{
				return Cache.NoAbsoluteExpiration;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x0015B793 File Offset: 0x00159993
		internal virtual TimeSpan MemoryCacheSlidingExpiration
		{
			get
			{
				return Cache.NoSlidingExpiration;
			}
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x0015B79C File Offset: 0x0015999C
		protected void ReadPreservedFlags(PreservationFileReader pfr)
		{
			string attribute = pfr.GetAttribute("flags");
			if (attribute != null && attribute.Length != 0)
			{
				this.Flags = int.Parse(attribute, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x0015B7D6 File Offset: 0x001599D6
		internal virtual void GetPreservedAttributes(PreservationFileReader pfr)
		{
			this.ReadPreservedFlags(pfr);
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x0015B7E0 File Offset: 0x001599E0
		internal virtual void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			if (this.Flags != 0)
			{
				pfw.SetAttribute("flags", this.Flags.ToString("x", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void RemoveOutOfDateResources(PreservationFileReader pfw)
		{
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x0015B818 File Offset: 0x00159A18
		internal long ComputeHashCode(long hashCode)
		{
			return this.ComputeHashCode(hashCode, 0L);
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x0015B824 File Offset: 0x00159A24
		internal long ComputeHashCode(long hashCode1, long hashCode2)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			if (hashCode1 != 0L)
			{
				hashCodeCombiner.AddObject(hashCode1);
			}
			if (hashCode2 != 0L)
			{
				hashCodeCombiner.AddObject(hashCode2);
			}
			this.ComputeHashCode(hashCodeCombiner);
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void ComputeHashCode(HashCodeCombiner hashCodeCombiner)
		{
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x0015B858 File Offset: 0x00159A58
		internal virtual string ComputeSourceDependenciesHashCode(VirtualPath virtualPath)
		{
			if (this.VirtualPathDependencies == null)
			{
				return string.Empty;
			}
			if (virtualPath == null)
			{
				virtualPath = this.VirtualPath;
			}
			return virtualPath.GetFileHash(this.VirtualPathDependencies);
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x0015B888 File Offset: 0x00159A88
		internal bool IsUpToDate(VirtualPath virtualPath, bool ensureIsUpToDate)
		{
			if (!ensureIsUpToDate)
			{
				return true;
			}
			if (this._lock < 0)
			{
				return false;
			}
			DateTime now = DateTime.Now;
			if (now < this._nextUpToDateCheck && !BuildManagerHost.InClientBuildManager)
			{
				return true;
			}
			if (Interlocked.CompareExchange(ref this._lock, 1, 0) != 0)
			{
				return true;
			}
			string text;
			try
			{
				text = this.ComputeSourceDependenciesHashCode(virtualPath);
			}
			catch
			{
				Interlocked.Exchange(ref this._lock, 0);
				throw;
			}
			if (text == null || text != this._virtualPathDependenciesHash)
			{
				this._lock = -1;
				return false;
			}
			this._nextUpToDateCheck = now.AddSeconds(2.0);
			Interlocked.Exchange(ref this._lock, 0);
			return true;
		}

		// Token: 0x04003364 RID: 13156
		protected const int usesCacheDependency = 65536;

		// Token: 0x04003365 RID: 13157
		protected const int usesExistingAssembly = 131072;

		// Token: 0x04003366 RID: 13158
		private const int noMemoryCache = 262144;

		// Token: 0x04003367 RID: 13159
		protected const int hasAppOrSessionObjects = 524288;

		// Token: 0x04003368 RID: 13160
		protected const int dependenciesHashComputed = 1048576;

		// Token: 0x04003369 RID: 13161
		protected SimpleBitVector32 _flags;

		// Token: 0x0400336A RID: 13162
		private VirtualPath _virtualPath;

		// Token: 0x0400336B RID: 13163
		private ArrayList _virtualPathDependencies;

		// Token: 0x0400336C RID: 13164
		private string _virtualPathDependenciesHash;

		// Token: 0x0400336D RID: 13165
		private DateTime _nextUpToDateCheck = DateTime.Now.AddSeconds(2.0);

		// Token: 0x0400336E RID: 13166
		private int _lock;

		// Token: 0x0400336F RID: 13167
		private const int UpdateInterval = 2;
	}
}
