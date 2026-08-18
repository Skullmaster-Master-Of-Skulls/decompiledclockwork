using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Policy;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001DE RID: 478
	public class WindowsClaimSet : ClaimSet, IIdentityInfo, IDisposable
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x00044C60 File Offset: 0x00042E60
		public WindowsClaimSet(WindowsIdentity windowsIdentity) : this(windowsIdentity, true)
		{
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00044C6C File Offset: 0x00042E6C
		public WindowsClaimSet(WindowsIdentity windowsIdentity, bool includeWindowsGroups) : this(windowsIdentity, includeWindowsGroups, DateTime.UtcNow.AddHours(10.0))
		{
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00044C97 File Offset: 0x00042E97
		public WindowsClaimSet(WindowsIdentity windowsIdentity, DateTime expirationTime) : this(windowsIdentity, true, expirationTime)
		{
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00044CA2 File Offset: 0x00042EA2
		public WindowsClaimSet(WindowsIdentity windowsIdentity, bool includeWindowsGroups, DateTime expirationTime) : this(windowsIdentity, null, includeWindowsGroups, expirationTime, true)
		{
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00044CAF File Offset: 0x00042EAF
		public WindowsClaimSet(WindowsIdentity windowsIdentity, string authenticationType, bool includeWindowsGroups, DateTime expirationTime) : this(windowsIdentity, authenticationType, includeWindowsGroups, expirationTime, true)
		{
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00044CC0 File Offset: 0x00042EC0
		internal WindowsClaimSet(WindowsIdentity windowsIdentity, string authenticationType, bool includeWindowsGroups, bool clone) : this(windowsIdentity, authenticationType, includeWindowsGroups, DateTime.UtcNow.AddHours(10.0), clone)
		{
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00044CF0 File Offset: 0x00042EF0
		internal WindowsClaimSet(WindowsIdentity windowsIdentity, string authenticationType, bool includeWindowsGroups, DateTime expirationTime, bool clone)
		{
			if (windowsIdentity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windowsIdentity");
			}
			this.windowsIdentity = (clone ? SecurityUtils.CloneWindowsIdentityIfNecessary(windowsIdentity, authenticationType) : windowsIdentity);
			this.includeWindowsGroups = includeWindowsGroups;
			this.expirationTime = expirationTime;
			this.authenticationType = authenticationType;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00044D40 File Offset: 0x00042F40
		private WindowsClaimSet(WindowsClaimSet from) : this(from.WindowsIdentity, from.authenticationType, from.includeWindowsGroups, from.expirationTime, true)
		{
		}

		// Token: 0x17000432 RID: 1074
		public override Claim this[int index]
		{
			get
			{
				this.ThrowIfDisposed();
				this.EnsureClaims();
				return this.claims[index];
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00044D7B File Offset: 0x00042F7B
		public override int Count
		{
			get
			{
				this.ThrowIfDisposed();
				this.EnsureClaims();
				return this.claims.Count;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00044D94 File Offset: 0x00042F94
		IIdentity IIdentityInfo.Identity
		{
			get
			{
				this.ThrowIfDisposed();
				return this.windowsIdentity;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00044D94 File Offset: 0x00042F94
		public WindowsIdentity WindowsIdentity
		{
			get
			{
				this.ThrowIfDisposed();
				return this.windowsIdentity;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00044DA2 File Offset: 0x00042FA2
		public override ClaimSet Issuer
		{
			get
			{
				return ClaimSet.Windows;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00044DA9 File Offset: 0x00042FA9
		public DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00044DB1 File Offset: 0x00042FB1
		private WindowsClaimSet.GroupSidClaimCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new WindowsClaimSet.GroupSidClaimCollection(this.windowsIdentity);
				}
				return this.groups;
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00044DD2 File Offset: 0x00042FD2
		internal WindowsClaimSet Clone()
		{
			this.ThrowIfDisposed();
			return new WindowsClaimSet(this);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00044DE0 File Offset: 0x00042FE0
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.windowsIdentity.Dispose();
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00044DFC File Offset: 0x00042FFC
		private IList<Claim> InitializeClaimsCore()
		{
			if (this.windowsIdentity.Token == IntPtr.Zero)
			{
				return new List<Claim>();
			}
			List<Claim> list = new List<Claim>(3);
			list.Add(new Claim(ClaimTypes.Sid, this.windowsIdentity.User, Rights.Identity));
			Claim item;
			if (WindowsClaimSet.TryCreateWindowsSidClaim(this.windowsIdentity, out item))
			{
				list.Add(item);
			}
			list.Add(Claim.CreateNameClaim(this.windowsIdentity.Name));
			if (this.includeWindowsGroups)
			{
				list.AddRange(this.Groups);
			}
			return list;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00044E8E File Offset: 0x0004308E
		private void EnsureClaims()
		{
			if (this.claims != null)
			{
				return;
			}
			this.claims = this.InitializeClaimsCore();
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00044EA5 File Offset: 0x000430A5
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00044ECA File Offset: 0x000430CA
		private static bool SupportedClaimType(string claimType)
		{
			return claimType == null || ClaimTypes.Sid == claimType || ClaimTypes.DenyOnlySid == claimType || ClaimTypes.Name == claimType;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00044EF6 File Offset: 0x000430F6
		public override IEnumerable<Claim> FindClaims(string claimType, string right)
		{
			this.ThrowIfDisposed();
			if (!WindowsClaimSet.SupportedClaimType(claimType) || !ClaimSet.SupportedRight(right))
			{
				yield break;
			}
			if (this.claims == null && (ClaimTypes.Sid == claimType || ClaimTypes.DenyOnlySid == claimType))
			{
				if (ClaimTypes.Sid == claimType && (right == null || Rights.Identity == right))
				{
					yield return new Claim(ClaimTypes.Sid, this.windowsIdentity.User, Rights.Identity);
				}
				Claim claim;
				if ((right == null || Rights.PossessProperty == right) && WindowsClaimSet.TryCreateWindowsSidClaim(this.windowsIdentity, out claim) && claimType == claim.ClaimType)
				{
					yield return claim;
				}
				if (this.includeWindowsGroups && (right == null || Rights.PossessProperty == right))
				{
					int num;
					for (int i = 0; i < this.Groups.Count; i = num)
					{
						Claim claim2 = this.Groups[i];
						if (claimType == claim2.ClaimType)
						{
							yield return claim2;
						}
						num = i + 1;
					}
				}
			}
			else
			{
				this.EnsureClaims();
				bool anyClaimType = claimType == null;
				bool anyRight = right == null;
				int num;
				for (int i = 0; i < this.claims.Count; i = num)
				{
					Claim claim3 = this.claims[i];
					if (claim3 != null && (anyClaimType || claimType == claim3.ClaimType) && (anyRight || right == claim3.Right))
					{
						yield return claim3;
					}
					num = i + 1;
				}
			}
			yield break;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00044F14 File Offset: 0x00043114
		public override IEnumerator<Claim> GetEnumerator()
		{
			this.ThrowIfDisposed();
			this.EnsureClaims();
			return this.claims.GetEnumerator();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00044F2D File Offset: 0x0004312D
		public override string ToString()
		{
			if (!this.disposed)
			{
				return SecurityUtils.ClaimSetToString(this);
			}
			return base.ToString();
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00044F44 File Offset: 0x00043144
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		private static SafeHGlobalHandle GetTokenInformation(IntPtr tokenHandle, TokenInformationClass tokenInformationClass, out uint dwLength)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			dwLength = (uint)Marshal.SizeOf(typeof(uint));
			bool tokenInformation = NativeMethods.GetTokenInformation(tokenHandle, (uint)tokenInformationClass, safeHGlobalHandle, 0U, out dwLength);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 24 && lastWin32Error != 122)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
			safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(dwLength);
			tokenInformation = NativeMethods.GetTokenInformation(tokenHandle, (uint)tokenInformationClass, safeHGlobalHandle, dwLength, out dwLength);
			lastWin32Error = Marshal.GetLastWin32Error();
			if (!tokenInformation)
			{
				safeHGlobalHandle.Close();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
			return safeHGlobalHandle;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00044FCC File Offset: 0x000431CC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		private static bool TryCreateWindowsSidClaim(WindowsIdentity windowsIdentity, out Claim claim)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			try
			{
				uint num;
				safeHGlobalHandle = WindowsClaimSet.GetTokenInformation(windowsIdentity.Token, TokenInformationClass.TokenUser, out num);
				SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (SID_AND_ATTRIBUTES)Marshal.PtrToStructure(safeHGlobalHandle.DangerousGetHandle(), typeof(SID_AND_ATTRIBUTES));
				uint num2 = 16U;
				if (sid_AND_ATTRIBUTES.Attributes == 0U)
				{
					claim = Claim.CreateWindowsSidClaim(new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid));
					return true;
				}
				if ((sid_AND_ATTRIBUTES.Attributes & num2) == 16U)
				{
					claim = Claim.CreateDenyOnlyWindowsSidClaim(new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid));
					return true;
				}
			}
			finally
			{
				safeHGlobalHandle.Close();
			}
			claim = null;
			return false;
		}

		// Token: 0x04000DCA RID: 3530
		internal const bool DefaultIncludeWindowsGroups = true;

		// Token: 0x04000DCB RID: 3531
		private WindowsIdentity windowsIdentity;

		// Token: 0x04000DCC RID: 3532
		private DateTime expirationTime;

		// Token: 0x04000DCD RID: 3533
		private bool includeWindowsGroups;

		// Token: 0x04000DCE RID: 3534
		private IList<Claim> claims;

		// Token: 0x04000DCF RID: 3535
		private WindowsClaimSet.GroupSidClaimCollection groups;

		// Token: 0x04000DD0 RID: 3536
		private bool disposed;

		// Token: 0x04000DD1 RID: 3537
		private string authenticationType;

		// Token: 0x020002A3 RID: 675
		private class GroupSidClaimCollection : Collection<Claim>
		{
			// Token: 0x060013A0 RID: 5024 RVA: 0x000532D8 File Offset: 0x000514D8
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			public GroupSidClaimCollection(WindowsIdentity windowsIdentity)
			{
				if (windowsIdentity.Token != IntPtr.Zero)
				{
					SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
					try
					{
						uint num;
						safeHGlobalHandle = WindowsClaimSet.GetTokenInformation(windowsIdentity.Token, TokenInformationClass.TokenGroups, out num);
						int num2 = Marshal.ReadInt32(safeHGlobalHandle.DangerousGetHandle());
						IntPtr intPtr = new IntPtr((long)safeHGlobalHandle.DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(TOKEN_GROUPS), "Groups"));
						for (int i = 0; i < num2; i++)
						{
							SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (SID_AND_ATTRIBUTES)Marshal.PtrToStructure(intPtr, typeof(SID_AND_ATTRIBUTES));
							uint num3 = 3221225492U;
							if ((sid_AND_ATTRIBUTES.Attributes & num3) == 4U)
							{
								base.Add(Claim.CreateWindowsSidClaim(new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid)));
							}
							else if ((sid_AND_ATTRIBUTES.Attributes & num3) == 16U)
							{
								base.Add(Claim.CreateDenyOnlyWindowsSidClaim(new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid)));
							}
							intPtr = new IntPtr((long)intPtr + SID_AND_ATTRIBUTES.SizeOf);
						}
					}
					finally
					{
						safeHGlobalHandle.Close();
					}
				}
			}
		}
	}
}
