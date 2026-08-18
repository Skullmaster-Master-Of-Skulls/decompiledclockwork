using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceModel.Diagnostics;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200012F RID: 303
	public class RsaSecurityToken : SecurityToken
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00023A68 File Offset: 0x00021C68
		public RsaSecurityToken(RSA rsa) : this(rsa, SecurityUniqueId.Create().Value)
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00023A7C File Offset: 0x00021C7C
		public RsaSecurityToken(RSA rsa, string id)
		{
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rsa");
			}
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			this.rsa = rsa;
			this.id = id;
			this.effectiveTime = DateTime.UtcNow;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00023AD4 File Offset: 0x00021CD4
		private RsaSecurityToken(RSACryptoServiceProvider rsa, bool ownsRsa)
		{
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rsa");
			}
			this.rsa = rsa;
			this.id = SecurityUniqueId.Create().Value;
			this.effectiveTime = DateTime.UtcNow;
			if (ownsRsa)
			{
				this.keyContainerInfo = rsa.CspKeyContainerInfo;
				rsa.PersistKeyInCsp = true;
				this.rsaHandle = GCHandle.Alloc(rsa);
				return;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00023B48 File Offset: 0x00021D48
		~RsaSecurityToken()
		{
			this.Dispose(false);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00023B78 File Offset: 0x00021D78
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00023B88 File Offset: 0x00021D88
		private void Dispose(bool disposing)
		{
			if (this.rsaHandle.IsAllocated)
			{
				try
				{
					string keyContainerName = this.keyContainerInfo.KeyContainerName;
					string providerName = this.keyContainerInfo.ProviderName;
					uint providerType = (uint)this.keyContainerInfo.ProviderType;
					((IDisposable)this.rsa).Dispose();
					SafeProvHandle handle;
					if (!NativeMethods.CryptAcquireContextW(out handle, keyContainerName, providerName, providerType, 16U))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						try
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FailedToDeleteKeyContainerFile"), new Win32Exception(lastWin32Error)));
						}
						catch (InvalidOperationException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
						}
					}
					Utility.CloseInvalidOutSafeHandle(handle);
				}
				finally
				{
					this.rsaHandle.Free();
				}
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00023C48 File Offset: 0x00021E48
		internal static RsaSecurityToken CreateSafeRsaSecurityToken(int keySize)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			RsaSecurityToken result;
			try
			{
				try
				{
				}
				finally
				{
					rsacryptoServiceProvider = new RSACryptoServiceProvider(keySize);
				}
				result = new RsaSecurityToken(rsacryptoServiceProvider, true);
				rsacryptoServiceProvider = null;
			}
			finally
			{
				if (rsacryptoServiceProvider != null)
				{
					((IDisposable)rsacryptoServiceProvider).Dispose();
				}
			}
			return result;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00023C9C File Offset: 0x00021E9C
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00023CA4 File Offset: 0x00021EA4
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00023CAC File Offset: 0x00021EAC
		public override DateTime ValidTo
		{
			get
			{
				return SecurityUtils.MaxUtcDateTime;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00023CB4 File Offset: 0x00021EB4
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this.rsaKey == null)
				{
					this.rsaKey = new List<SecurityKey>(1)
					{
						new RsaSecurityKey(this.rsa)
					}.AsReadOnly();
				}
				return this.rsaKey;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00023CF3 File Offset: 0x00021EF3
		public RSA Rsa
		{
			get
			{
				return this.rsa;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00023CFB File Offset: 0x00021EFB
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(RsaKeyIdentifierClause);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00023D18 File Offset: 0x00021F18
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(RsaKeyIdentifierClause))
			{
				return (T)((object)new RsaKeyIdentifierClause(this.rsa));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TokenDoesNotSupportKeyIdentifierClauseCreation", new object[]
			{
				base.GetType().Name,
				typeof(T).Name
			})));
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00023D90 File Offset: 0x00021F90
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			RsaKeyIdentifierClause rsaKeyIdentifierClause = keyIdentifierClause as RsaKeyIdentifierClause;
			return rsaKeyIdentifierClause != null && rsaKeyIdentifierClause.Matches(this.rsa);
		}

		// Token: 0x04000B1E RID: 2846
		private string id;

		// Token: 0x04000B1F RID: 2847
		private DateTime effectiveTime;

		// Token: 0x04000B20 RID: 2848
		private ReadOnlyCollection<SecurityKey> rsaKey;

		// Token: 0x04000B21 RID: 2849
		private RSA rsa;

		// Token: 0x04000B22 RID: 2850
		private CspKeyContainerInfo keyContainerInfo;

		// Token: 0x04000B23 RID: 2851
		private GCHandle rsaHandle;
	}
}
