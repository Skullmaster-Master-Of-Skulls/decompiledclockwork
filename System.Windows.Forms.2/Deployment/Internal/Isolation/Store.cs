using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005F RID: 95
	internal class Store
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007842 File Offset: 0x00005A42
		public IStore InternalStore
		{
			get
			{
				return this._pStore;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000784A File Offset: 0x00005A4A
		public Store(IStore pStore)
		{
			if (pStore == null)
			{
				throw new ArgumentNullException("pStore");
			}
			this._pStore = pStore;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007868 File Offset: 0x00005A68
		[SecuritySafeCritical]
		public uint[] Transact(StoreTransactionOperation[] operations)
		{
			if (operations == null || operations.Length == 0)
			{
				throw new ArgumentException("operations");
			}
			uint[] array = new uint[operations.Length];
			int[] rgResults = new int[operations.Length];
			this._pStore.Transact(new IntPtr(operations.Length), operations, array, rgResults);
			return array;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000078B0 File Offset: 0x00005AB0
		public void Transact(StoreTransactionOperation[] operations, uint[] rgDispositions, int[] rgResults)
		{
			if (operations == null || operations.Length == 0)
			{
				throw new ArgumentException("operations");
			}
			this._pStore.Transact(new IntPtr(operations.Length), operations, rgDispositions, rgResults);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000078DC File Offset: 0x00005ADC
		[SecuritySafeCritical]
		public IDefinitionIdentity BindReferenceToAssemblyIdentity(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)obj;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007908 File Offset: 0x00005B08
		[SecuritySafeCritical]
		public void CalculateDelimiterOfDeploymentsBasedOnQuota(uint dwFlags, uint cDeployments, IDefinitionAppId[] rgpIDefinitionAppId_Deployments, ref StoreApplicationReference InstallerReference, ulong ulonglongQuota, ref uint Delimiter, ref ulong SizeSharedWithExternalDeployment, ref ulong SizeConsumedByInputDeploymentArray)
		{
			IntPtr zero = IntPtr.Zero;
			this._pStore.CalculateDelimiterOfDeploymentsBasedOnQuota(dwFlags, new IntPtr((long)((ulong)cDeployments)), rgpIDefinitionAppId_Deployments, ref InstallerReference, ulonglongQuota, ref zero, ref SizeSharedWithExternalDeployment, ref SizeConsumedByInputDeploymentArray);
			Delimiter = (uint)zero.ToInt64();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007944 File Offset: 0x00005B44
		[SecuritySafeCritical]
		public ICMS BindReferenceToAssemblyManifest(uint Flags, IReferenceIdentity ReferenceIdentity, uint cDeploymentsToIgnore, IDefinitionIdentity[] DefinitionIdentity_DeploymentsToIgnore)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object obj = this._pStore.BindReferenceToAssembly(Flags, ReferenceIdentity, cDeploymentsToIgnore, DefinitionIdentity_DeploymentsToIgnore, ref iid_ICMS);
			return (ICMS)obj;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007970 File Offset: 0x00005B70
		[SecuritySafeCritical]
		public ICMS GetAssemblyManifest(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_ICMS = IsolationInterop.IID_ICMS;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_ICMS);
			return (ICMS)assemblyInformation;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000799C File Offset: 0x00005B9C
		[SecuritySafeCritical]
		public IDefinitionIdentity GetAssemblyIdentity(uint Flags, IDefinitionIdentity DefinitionIdentity)
		{
			Guid iid_IDefinitionIdentity = IsolationInterop.IID_IDefinitionIdentity;
			object assemblyInformation = this._pStore.GetAssemblyInformation(Flags, DefinitionIdentity, ref iid_IDefinitionIdentity);
			return (IDefinitionIdentity)assemblyInformation;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000079C5 File Offset: 0x00005BC5
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags)
		{
			return this.EnumAssemblies(Flags, null);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000079D0 File Offset: 0x00005BD0
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration EnumAssemblies(Store.EnumAssembliesFlags Flags, IReferenceIdentity refToMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));
			object obj = this._pStore.EnumAssemblies((uint)Flags, refToMatch, ref guidOfType);
			return new StoreAssemblyEnumeration((IEnumSTORE_ASSEMBLY)obj);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007A08 File Offset: 0x00005C08
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumFiles(Store.EnumAssemblyFilesFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumFiles((uint)Flags, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007A40 File Offset: 0x00005C40
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration EnumPrivateFiles(Store.EnumApplicationPrivateFiles Flags, IDefinitionAppId Application, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));
			object obj = this._pStore.EnumPrivateFiles((uint)Flags, Application, Assembly, ref guidOfType);
			return new StoreAssemblyFileEnumeration((IEnumSTORE_ASSEMBLY_FILE)obj);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007A7C File Offset: 0x00005C7C
		[SecuritySafeCritical]
		public IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE EnumInstallationReferences(Store.EnumAssemblyInstallReferenceFlags Flags, IDefinitionIdentity Assembly)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE));
			object obj = this._pStore.EnumInstallationReferences((uint)Flags, Assembly, ref guidOfType);
			return (IEnumSTORE_ASSEMBLY_INSTALLATION_REFERENCE)obj;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007AB0 File Offset: 0x00005CB0
		[SecuritySafeCritical]
		public Store.IPathLock LockAssemblyPath(IDefinitionIdentity asm)
		{
			IntPtr c;
			string path = this._pStore.LockAssemblyPath(0U, asm, out c);
			return new Store.AssemblyPathLock(this._pStore, c, path);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007ADC File Offset: 0x00005CDC
		[SecuritySafeCritical]
		public Store.IPathLock LockApplicationPath(IDefinitionAppId app)
		{
			IntPtr c;
			string path = this._pStore.LockApplicationPath(0U, app, out c);
			return new Store.ApplicationPathLock(this._pStore, c, path);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007B08 File Offset: 0x00005D08
		[SecuritySafeCritical]
		public ulong QueryChangeID(IDefinitionIdentity asm)
		{
			return this._pStore.QueryChangeID(asm);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007B24 File Offset: 0x00005D24
		[SecuritySafeCritical]
		public StoreCategoryEnumeration EnumCategories(Store.EnumCategoriesFlags Flags, IReferenceIdentity CategoryMatch)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));
			object obj = this._pStore.EnumCategories((uint)Flags, CategoryMatch, ref guidOfType);
			return new StoreCategoryEnumeration((IEnumSTORE_CATEGORY)obj);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007B5C File Offset: 0x00005D5C
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity CategoryMatch)
		{
			return this.EnumSubcategories(Flags, CategoryMatch, null);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007B68 File Offset: 0x00005D68
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration EnumSubcategories(Store.EnumSubcategoriesFlags Flags, IDefinitionIdentity Category, string SearchPattern)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_SUBCATEGORY));
			object obj = this._pStore.EnumSubcategories((uint)Flags, Category, SearchPattern, ref guidOfType);
			return new StoreSubcategoryEnumeration((IEnumSTORE_CATEGORY_SUBCATEGORY)obj);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007BA4 File Offset: 0x00005DA4
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration EnumCategoryInstances(Store.EnumCategoryInstancesFlags Flags, IDefinitionIdentity Category, string SubCat)
		{
			Guid guidOfType = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));
			object obj = this._pStore.EnumCategoryInstances((uint)Flags, Category, SubCat, ref guidOfType);
			return new StoreCategoryInstanceEnumeration((IEnumSTORE_CATEGORY_INSTANCE)obj);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007BE0 File Offset: 0x00005DE0
		[SecurityCritical]
		public byte[] GetDeploymentProperty(Store.GetPackagePropertyFlags Flags, IDefinitionAppId Deployment, StoreApplicationReference Reference, Guid PropertySet, string PropertyName)
		{
			BLOB blob = default(BLOB);
			byte[] array = null;
			try
			{
				this._pStore.GetDeploymentProperty((uint)Flags, Deployment, ref Reference, ref PropertySet, PropertyName, out blob);
				array = new byte[blob.Size];
				Marshal.Copy(blob.BlobData, array, 0, (int)blob.Size);
			}
			finally
			{
				blob.Dispose();
			}
			return array;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007C48 File Offset: 0x00005E48
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration EnumInstallerDeployments(Guid InstallerId, string InstallerName, string InstallerMetadata, IReferenceAppId DeploymentFilter)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadata(0U, ref storeApplicationReference, DeploymentFilter, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA);
			return new StoreDeploymentMetadataEnumeration((IEnumSTORE_DEPLOYMENT_METADATA)obj);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007C84 File Offset: 0x00005E84
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration EnumInstallerDeploymentProperties(Guid InstallerId, string InstallerName, string InstallerMetadata, IDefinitionAppId Deployment)
		{
			StoreApplicationReference storeApplicationReference = new StoreApplicationReference(InstallerId, InstallerName, InstallerMetadata);
			object obj = this._pStore.EnumInstallerDeploymentMetadataProperties(0U, ref storeApplicationReference, Deployment, ref IsolationInterop.IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY);
			return new StoreDeploymentMetadataPropertyEnumeration((IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY)obj);
		}

		// Token: 0x0400019D RID: 413
		private IStore _pStore;

		// Token: 0x02000532 RID: 1330
		[Flags]
		public enum EnumAssembliesFlags
		{
			// Token: 0x040037D5 RID: 14293
			Nothing = 0,
			// Token: 0x040037D6 RID: 14294
			VisibleOnly = 1,
			// Token: 0x040037D7 RID: 14295
			MatchServicing = 2,
			// Token: 0x040037D8 RID: 14296
			ForceLibrarySemantics = 4
		}

		// Token: 0x02000533 RID: 1331
		[Flags]
		public enum EnumAssemblyFilesFlags
		{
			// Token: 0x040037DA RID: 14298
			Nothing = 0,
			// Token: 0x040037DB RID: 14299
			IncludeInstalled = 1,
			// Token: 0x040037DC RID: 14300
			IncludeMissing = 2
		}

		// Token: 0x02000534 RID: 1332
		[Flags]
		public enum EnumApplicationPrivateFiles
		{
			// Token: 0x040037DE RID: 14302
			Nothing = 0,
			// Token: 0x040037DF RID: 14303
			IncludeInstalled = 1,
			// Token: 0x040037E0 RID: 14304
			IncludeMissing = 2
		}

		// Token: 0x02000535 RID: 1333
		[Flags]
		public enum EnumAssemblyInstallReferenceFlags
		{
			// Token: 0x040037E2 RID: 14306
			Nothing = 0
		}

		// Token: 0x02000536 RID: 1334
		public interface IPathLock : IDisposable
		{
			// Token: 0x17001483 RID: 5251
			// (get) Token: 0x0600554F RID: 21839
			string Path { get; }
		}

		// Token: 0x02000537 RID: 1335
		private class AssemblyPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06005550 RID: 21840 RVA: 0x00165EEC File Offset: 0x001640EC
			public AssemblyPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06005551 RID: 21841 RVA: 0x00165F14 File Offset: 0x00164114
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseAssemblyPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06005552 RID: 21842 RVA: 0x00165F50 File Offset: 0x00164150
			~AssemblyPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06005553 RID: 21843 RVA: 0x00165F80 File Offset: 0x00164180
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001484 RID: 5252
			// (get) Token: 0x06005554 RID: 21844 RVA: 0x00165F89 File Offset: 0x00164189
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037E3 RID: 14307
			private IStore _pSourceStore;

			// Token: 0x040037E4 RID: 14308
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037E5 RID: 14309
			private string _path;
		}

		// Token: 0x02000538 RID: 1336
		private class ApplicationPathLock : Store.IPathLock, IDisposable
		{
			// Token: 0x06005555 RID: 21845 RVA: 0x00165F91 File Offset: 0x00164191
			public ApplicationPathLock(IStore s, IntPtr c, string path)
			{
				this._pSourceStore = s;
				this._pLockCookie = c;
				this._path = path;
			}

			// Token: 0x06005556 RID: 21846 RVA: 0x00165FB9 File Offset: 0x001641B9
			[SecuritySafeCritical]
			private void Dispose(bool fDisposing)
			{
				if (fDisposing)
				{
					GC.SuppressFinalize(this);
				}
				if (this._pLockCookie != IntPtr.Zero)
				{
					this._pSourceStore.ReleaseApplicationPath(this._pLockCookie);
					this._pLockCookie = IntPtr.Zero;
				}
			}

			// Token: 0x06005557 RID: 21847 RVA: 0x00165FF4 File Offset: 0x001641F4
			~ApplicationPathLock()
			{
				this.Dispose(false);
			}

			// Token: 0x06005558 RID: 21848 RVA: 0x00166024 File Offset: 0x00164224
			void IDisposable.Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x17001485 RID: 5253
			// (get) Token: 0x06005559 RID: 21849 RVA: 0x0016602D File Offset: 0x0016422D
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x040037E6 RID: 14310
			private IStore _pSourceStore;

			// Token: 0x040037E7 RID: 14311
			private IntPtr _pLockCookie = IntPtr.Zero;

			// Token: 0x040037E8 RID: 14312
			private string _path;
		}

		// Token: 0x02000539 RID: 1337
		[Flags]
		public enum EnumCategoriesFlags
		{
			// Token: 0x040037EA RID: 14314
			Nothing = 0
		}

		// Token: 0x0200053A RID: 1338
		[Flags]
		public enum EnumSubcategoriesFlags
		{
			// Token: 0x040037EC RID: 14316
			Nothing = 0
		}

		// Token: 0x0200053B RID: 1339
		[Flags]
		public enum EnumCategoryInstancesFlags
		{
			// Token: 0x040037EE RID: 14318
			Nothing = 0
		}

		// Token: 0x0200053C RID: 1340
		[Flags]
		public enum GetPackagePropertyFlags
		{
			// Token: 0x040037F0 RID: 14320
			Nothing = 0
		}
	}
}
