using System;
using System.Collections.ObjectModel;
using System.Security.Principal;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000189 RID: 393
	public class WindowsSecurityToken : SecurityToken, IDisposable
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x0003BCDA File Offset: 0x00039EDA
		public WindowsSecurityToken(WindowsIdentity windowsIdentity) : this(windowsIdentity, SecurityUniqueId.Create().Value)
		{
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0003BCED File Offset: 0x00039EED
		public WindowsSecurityToken(WindowsIdentity windowsIdentity, string id) : this(windowsIdentity, id, null)
		{
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003BCF8 File Offset: 0x00039EF8
		public WindowsSecurityToken(WindowsIdentity windowsIdentity, string id, string authenticationType)
		{
			DateTime utcNow = DateTime.UtcNow;
			this.Initialize(id, authenticationType, utcNow, DateTime.UtcNow.AddHours(10.0), windowsIdentity, true);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000304B0 File Offset: 0x0002E6B0
		protected WindowsSecurityToken()
		{
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003BD32 File Offset: 0x00039F32
		protected void Initialize(string id, DateTime effectiveTime, DateTime expirationTime, WindowsIdentity windowsIdentity, bool clone)
		{
			this.Initialize(id, null, effectiveTime, expirationTime, windowsIdentity, clone);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003BD44 File Offset: 0x00039F44
		protected void Initialize(string id, string authenticationType, DateTime effectiveTime, DateTime expirationTime, WindowsIdentity windowsIdentity, bool clone)
		{
			if (windowsIdentity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windowsIdentity");
			}
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			this.id = id;
			this.authenticationType = authenticationType;
			this.effectiveTime = effectiveTime;
			this.expirationTime = expirationTime;
			this.windowsIdentity = (clone ? SecurityUtils.CloneWindowsIdentityIfNecessary(windowsIdentity, authenticationType) : windowsIdentity);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0003BDAB File Offset: 0x00039FAB
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0003BDB3 File Offset: 0x00039FB3
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0003BDBB File Offset: 0x00039FBB
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003BDC3 File Offset: 0x00039FC3
		public override DateTime ValidTo
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0003BDCB File Offset: 0x00039FCB
		public virtual WindowsIdentity WindowsIdentity
		{
			get
			{
				this.ThrowIfDisposed();
				return this.windowsIdentity;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0003B988 File Offset: 0x00039B88
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return EmptyReadOnlyCollection<SecurityKey>.Instance;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003BDD9 File Offset: 0x00039FD9
		public virtual void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.windowsIdentity != null)
				{
					this.windowsIdentity.Dispose();
					this.windowsIdentity = null;
				}
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003BE04 File Offset: 0x0003A004
		protected void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04000C9D RID: 3229
		private string authenticationType;

		// Token: 0x04000C9E RID: 3230
		private string id;

		// Token: 0x04000C9F RID: 3231
		private DateTime effectiveTime;

		// Token: 0x04000CA0 RID: 3232
		private DateTime expirationTime;

		// Token: 0x04000CA1 RID: 3233
		private WindowsIdentity windowsIdentity;

		// Token: 0x04000CA2 RID: 3234
		private bool disposed;
	}
}
