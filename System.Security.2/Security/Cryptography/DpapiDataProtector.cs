using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200001E RID: 30
	public sealed class DpapiDataProtector : DataProtector
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000518A File Offset: 0x0000338A
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005192 File Offset: 0x00003392
		public DataProtectionScope Scope { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004984 File Offset: 0x00002B84
		protected override bool PrependHashedPurposeToPlaintext
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000519B File Offset: 0x0000339B
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, ProtectData = true)]
		protected override byte[] ProviderProtect(byte[] userData)
		{
			return ProtectedData.Protect(userData, this.GetHashedPurpose(), this.Scope);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000051AF File Offset: 0x000033AF
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, UnprotectData = true)]
		protected override byte[] ProviderUnprotect(byte[] encryptedData)
		{
			return ProtectedData.Unprotect(encryptedData, this.GetHashedPurpose(), this.Scope);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004F9A File Offset: 0x0000319A
		public override bool IsReprotectRequired(byte[] encryptedData)
		{
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000051C3 File Offset: 0x000033C3
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Demand, Unrestricted = true)]
		public DpapiDataProtector(string appName, string primaryPurpose, params string[] specificPurpose) : base(appName, primaryPurpose, specificPurpose)
		{
		}
	}
}
