using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C3 RID: 707
	internal sealed class DbConnectionPoolAuthenticationContextKey
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0011A1E4 File Offset: 0x001195E4
		internal string StsAuthority
		{
			get
			{
				return this._stsAuthority;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x0011A1F8 File Offset: 0x001195F8
		internal string ServicePrincipalName
		{
			get
			{
				return this._servicePrincipalName;
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0011A20C File Offset: 0x0011960C
		internal DbConnectionPoolAuthenticationContextKey(string stsAuthority, string servicePrincipalName)
		{
			this._stsAuthority = stsAuthority;
			this._servicePrincipalName = servicePrincipalName;
			this._hashCode = this.ComputeHashCode();
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0011A23C File Offset: 0x0011963C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DbConnectionPoolAuthenticationContextKey dbConnectionPoolAuthenticationContextKey = obj as DbConnectionPoolAuthenticationContextKey;
			return dbConnectionPoolAuthenticationContextKey != null && string.Equals(this.StsAuthority, dbConnectionPoolAuthenticationContextKey.StsAuthority, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.ServicePrincipalName, dbConnectionPoolAuthenticationContextKey.ServicePrincipalName, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0011A284 File Offset: 0x00119684
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0011A298 File Offset: 0x00119698
		private int ComputeHashCode()
		{
			int num = 33;
			num = num * 17 + this.StsAuthority.GetHashCode();
			return num * 17 + this.ServicePrincipalName.GetHashCode();
		}

		// Token: 0x04001B64 RID: 7012
		private readonly string _stsAuthority;

		// Token: 0x04001B65 RID: 7013
		private readonly string _servicePrincipalName;

		// Token: 0x04001B66 RID: 7014
		private readonly int _hashCode;
	}
}
