using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000751 RID: 1873
	public sealed class DbProviderInfo
	{
		// Token: 0x060054F2 RID: 21746 RVA: 0x00172556 File Offset: 0x00170756
		public DbProviderInfo(string providerInvariantName, string providerManifestToken)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			this._providerInvariantName = providerInvariantName;
			this._providerManifestToken = providerManifestToken;
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x00172584 File Offset: 0x00170784
		public string ProviderInvariantName
		{
			get
			{
				return this._providerInvariantName;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060054F4 RID: 21748 RVA: 0x0017258C File Offset: 0x0017078C
		public string ProviderManifestToken
		{
			get
			{
				return this._providerManifestToken;
			}
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x00172594 File Offset: 0x00170794
		private bool Equals(DbProviderInfo other)
		{
			return string.Equals(this._providerInvariantName, other._providerInvariantName) && string.Equals(this._providerManifestToken, other._providerManifestToken);
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x001725BC File Offset: 0x001707BC
		public override bool Equals(object obj)
		{
			DbProviderInfo dbProviderInfo = obj as DbProviderInfo;
			return dbProviderInfo != null && this.Equals(dbProviderInfo);
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x001725DC File Offset: 0x001707DC
		public override int GetHashCode()
		{
			return this._providerInvariantName.GetHashCode() * 397 ^ this._providerManifestToken.GetHashCode();
		}

		// Token: 0x0400229A RID: 8858
		private readonly string _providerInvariantName;

		// Token: 0x0400229B RID: 8859
		private readonly string _providerManifestToken;
	}
}
