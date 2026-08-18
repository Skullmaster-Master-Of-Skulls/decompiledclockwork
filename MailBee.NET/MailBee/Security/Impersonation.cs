using System;
using System.Runtime.InteropServices;
using System.Security;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000107 RID: 263
	public class Impersonation : IDisposable
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x00029BB2 File Offset: 0x00028BB2
		public Impersonation() : this(null)
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00029BBC File Offset: 0x00028BBC
		public Impersonation(string licenseKey)
		{
			if (Powerup.License == null)
			{
				Powerup.a(licenseKey);
			}
			if (!Powerup.License.d())
			{
				throw new MailBeeLicenseException(Powerup.License, typeof(Powerup));
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00029C0F File Offset: 0x00028C0F
		public int LastResult
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00029C17 File Offset: 0x00028C17
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x00029C1F File Offset: 0x00028C1F
		public bool ThrowExceptions
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00029C28 File Offset: 0x00028C28
		public bool IsImpersonated
		{
			get
			{
				return this.d != IntPtr.Zero;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00029C3C File Offset: 0x00028C3C
		private void a(bool A_0)
		{
			if (!this.c)
			{
				try
				{
					this.Logoff();
				}
				catch (MailBeeImpersonationException)
				{
				}
			}
			this.c = true;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00029C78 File Offset: 0x00028C78
		public void Dispose()
		{
			this.a(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00029C88 File Offset: 0x00028C88
		[SecuritySafeCritical]
		public bool LogonAs(string accountName, string domainName, string password)
		{
			this.a = 0;
			if (accountName != null && accountName.Length != 0)
			{
				if (this.IsImpersonated)
				{
					this.Logoff();
				}
				try
				{
					if (l.LogonUser(accountName, domainName, password, 2, 0, ref this.d) == 0)
					{
						this.a = 1121;
						throw new MailBeeImpersonationWin32Exception(Marshal.GetLastWin32Error());
					}
					if (l.ImpersonateLoggedOnUser(this.d) == 0)
					{
						this.a = 1121;
						throw new MailBeeImpersonationWin32Exception(Marshal.GetLastWin32Error());
					}
				}
				catch (MailBeeImpersonationWin32Exception)
				{
					if (this.d != IntPtr.Zero)
					{
						l.CloseHandle(this.d);
					}
					this.d = IntPtr.Zero;
					if (this.b)
					{
						throw;
					}
					return false;
				}
				return true;
			}
			this.a = 22;
			if (this.b)
			{
				throw new MailBeeInvalidArgumentException(this.a);
			}
			return false;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00029D70 File Offset: 0x00028D70
		[SecuritySafeCritical]
		public bool Logoff()
		{
			if (!(this.d != IntPtr.Zero))
			{
				this.a = 1120;
				throw new MailBeeInvalidStateException(this.a);
			}
			bool flag = l.RevertToSelf() != 0;
			l.CloseHandle(this.d);
			this.d = IntPtr.Zero;
			if (flag)
			{
				return true;
			}
			this.a = 1121;
			if (this.b)
			{
				throw new MailBeeImpersonationWin32Exception(Marshal.GetLastWin32Error());
			}
			return false;
		}

		// Token: 0x04000704 RID: 1796
		private int a;

		// Token: 0x04000705 RID: 1797
		private bool b = true;

		// Token: 0x04000706 RID: 1798
		private bool c;

		// Token: 0x04000707 RID: 1799
		private IntPtr d = IntPtr.Zero;
	}
}
