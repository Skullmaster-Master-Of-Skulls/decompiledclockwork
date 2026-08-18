using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x0200011E RID: 286
	public class WindowsSecurityContext : log4net.Core.SecurityContext, IOptionHandler
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00019DDF File Offset: 0x00017FDF
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00019DE7 File Offset: 0x00017FE7
		public WindowsSecurityContext.ImpersonationMode Credentials
		{
			get
			{
				return this.m_impersonationMode;
			}
			set
			{
				this.m_impersonationMode = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00019DF0 File Offset: 0x00017FF0
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
			set
			{
				this.m_userName = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00019E01 File Offset: 0x00018001
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00019E09 File Offset: 0x00018009
		public string DomainName
		{
			get
			{
				return this.m_domainName;
			}
			set
			{
				this.m_domainName = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00019E12 File Offset: 0x00018012
		public string Password
		{
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00019E1C File Offset: 0x0001801C
		public void ActivateOptions()
		{
			if (this.m_impersonationMode == WindowsSecurityContext.ImpersonationMode.User)
			{
				if (this.m_userName == null)
				{
					throw new ArgumentNullException("m_userName");
				}
				if (this.m_domainName == null)
				{
					throw new ArgumentNullException("m_domainName");
				}
				if (this.m_password == null)
				{
					throw new ArgumentNullException("m_password");
				}
				this.m_identity = WindowsSecurityContext.LogonUser(this.m_userName, this.m_domainName, this.m_password);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00019E87 File Offset: 0x00018087
		public override IDisposable Impersonate(object state)
		{
			if (this.m_impersonationMode == WindowsSecurityContext.ImpersonationMode.User)
			{
				if (this.m_identity != null)
				{
					return new WindowsSecurityContext.DisposableImpersonationContext(this.m_identity.Impersonate());
				}
			}
			else if (this.m_impersonationMode == WindowsSecurityContext.ImpersonationMode.Process)
			{
				return new WindowsSecurityContext.DisposableImpersonationContext(WindowsIdentity.Impersonate(IntPtr.Zero));
			}
			return null;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00019EC4 File Offset: 0x000180C4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		private static WindowsIdentity LogonUser(string userName, string domainName, string password)
		{
			IntPtr zero = IntPtr.Zero;
			if (!WindowsSecurityContext.LogonUser(userName, domainName, password, 2, 0, ref zero))
			{
				NativeError lastError = NativeError.GetLastError();
				throw new Exception(string.Concat(new string[]
				{
					"Failed to LogonUser [",
					userName,
					"] in Domain [",
					domainName,
					"]. Error: ",
					lastError.ToString()
				}));
			}
			IntPtr zero2 = IntPtr.Zero;
			if (!WindowsSecurityContext.DuplicateToken(zero, 2, ref zero2))
			{
				NativeError lastError2 = NativeError.GetLastError();
				if (zero != IntPtr.Zero)
				{
					WindowsSecurityContext.CloseHandle(zero);
				}
				throw new Exception("Failed to DuplicateToken after LogonUser. Error: " + lastError2.ToString());
			}
			WindowsIdentity result = new WindowsIdentity(zero2);
			if (zero2 != IntPtr.Zero)
			{
				WindowsSecurityContext.CloseHandle(zero2);
			}
			if (zero != IntPtr.Zero)
			{
				WindowsSecurityContext.CloseHandle(zero);
			}
			return result;
		}

		// Token: 0x06000868 RID: 2152
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		// Token: 0x06000869 RID: 2153
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600086A RID: 2154
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool DuplicateToken(IntPtr ExistingTokenHandle, int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

		// Token: 0x0400030A RID: 778
		private WindowsSecurityContext.ImpersonationMode m_impersonationMode;

		// Token: 0x0400030B RID: 779
		private string m_userName;

		// Token: 0x0400030C RID: 780
		private string m_domainName = Environment.MachineName;

		// Token: 0x0400030D RID: 781
		private string m_password;

		// Token: 0x0400030E RID: 782
		private WindowsIdentity m_identity;

		// Token: 0x0200011F RID: 287
		public enum ImpersonationMode
		{
			// Token: 0x04000310 RID: 784
			User,
			// Token: 0x04000311 RID: 785
			Process
		}

		// Token: 0x02000120 RID: 288
		private sealed class DisposableImpersonationContext : IDisposable
		{
			// Token: 0x0600086B RID: 2155 RVA: 0x00019FA2 File Offset: 0x000181A2
			public DisposableImpersonationContext(WindowsImpersonationContext impersonationContext)
			{
				this.m_impersonationContext = impersonationContext;
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x00019FB1 File Offset: 0x000181B1
			public void Dispose()
			{
				this.m_impersonationContext.Undo();
			}

			// Token: 0x04000312 RID: 786
			private readonly WindowsImpersonationContext m_impersonationContext;
		}
	}
}
