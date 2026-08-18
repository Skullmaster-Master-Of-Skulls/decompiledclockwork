using System;

namespace System.Data.Objects
{
	// Token: 0x02000143 RID: 323
	public sealed class ObjectContextOptions
	{
		// Token: 0x06001713 RID: 5907 RVA: 0x0004CA50 File Offset: 0x0004AC50
		internal ObjectContextOptions()
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x0004CA5F File Offset: 0x0004AC5F
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x0004CA67 File Offset: 0x0004AC67
		public bool LazyLoadingEnabled
		{
			get
			{
				return this._lazyLoadingEnabled;
			}
			set
			{
				this._lazyLoadingEnabled = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0004CA70 File Offset: 0x0004AC70
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x0004CA78 File Offset: 0x0004AC78
		public bool ProxyCreationEnabled
		{
			get
			{
				return this._proxyCreationEnabled;
			}
			set
			{
				this._proxyCreationEnabled = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x0004CA81 File Offset: 0x0004AC81
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x0004CA89 File Offset: 0x0004AC89
		public bool UseLegacyPreserveChangesBehavior
		{
			get
			{
				return this._useLegacyPreserveChangesBehavior;
			}
			set
			{
				this._useLegacyPreserveChangesBehavior = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0004CA92 File Offset: 0x0004AC92
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x0004CA9A File Offset: 0x0004AC9A
		public bool UseConsistentNullReferenceBehavior
		{
			get
			{
				return this._useConsistentNullReferenceBehavior;
			}
			set
			{
				this._useConsistentNullReferenceBehavior = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0004CAA3 File Offset: 0x0004ACA3
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x0004CAAB File Offset: 0x0004ACAB
		public bool UseCSharpNullComparisonBehavior
		{
			get
			{
				return this._useCSharpNullComparisonBehavior;
			}
			set
			{
				this._useCSharpNullComparisonBehavior = value;
			}
		}

		// Token: 0x04000A77 RID: 2679
		private bool _lazyLoadingEnabled;

		// Token: 0x04000A78 RID: 2680
		private bool _proxyCreationEnabled = true;

		// Token: 0x04000A79 RID: 2681
		private bool _useLegacyPreserveChangesBehavior;

		// Token: 0x04000A7A RID: 2682
		private bool _useConsistentNullReferenceBehavior;

		// Token: 0x04000A7B RID: 2683
		private bool _useCSharpNullComparisonBehavior;
	}
}
