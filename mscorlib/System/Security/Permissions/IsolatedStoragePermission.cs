using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000637 RID: 1591
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06003955 RID: 14677 RVA: 0x000C188C File Offset: 0x000C088C
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_userQuota = long.MaxValue;
				this.m_machineQuota = long.MaxValue;
				this.m_expirationDays = long.MaxValue;
				this.m_permanentData = true;
				this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_userQuota = 0L;
				this.m_machineQuota = 0L;
				this.m_expirationDays = 0L;
				this.m_permanentData = false;
				this.m_allowed = IsolatedStorageContainment.None;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000C191C File Offset: 0x000C091C
		internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
		{
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = ExpirationDays;
			this.m_permanentData = PermanentData;
			this.m_allowed = UsageAllowed;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000C1949 File Offset: 0x000C0949
		internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData, long UserQuota)
		{
			this.m_machineQuota = 0L;
			this.m_userQuota = UserQuota;
			this.m_expirationDays = ExpirationDays;
			this.m_permanentData = PermanentData;
			this.m_allowed = UsageAllowed;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x000C197F File Offset: 0x000C097F
		// (set) Token: 0x06003958 RID: 14680 RVA: 0x000C1976 File Offset: 0x000C0976
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000C1990 File Offset: 0x000C0990
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x000C1987 File Offset: 0x000C0987
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				this.m_allowed = value;
			}
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000C1998 File Offset: 0x000C0998
		public bool IsUnrestricted()
		{
			return this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000C19A7 File Offset: 0x000C09A7
		internal static long min(long x, long y)
		{
			if (x <= y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000C19B0 File Offset: 0x000C09B0
		internal static long max(long x, long y)
		{
			if (x >= y)
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000C19B9 File Offset: 0x000C09B9
		public override SecurityElement ToXml()
		{
			return this.ToXml(base.GetType().FullName);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000C19CC File Offset: 0x000C09CC
		internal SecurityElement ToXml(string permName)
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, permName);
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Allowed", Enum.GetName(typeof(IsolatedStorageContainment), this.m_allowed));
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_machineQuota > 0L)
				{
					securityElement.AddAttribute("MachineQuota", this.m_machineQuota.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_expirationDays > 0L)
				{
					securityElement.AddAttribute("Expiry", this.m_expirationDays.ToString(CultureInfo.InvariantCulture));
				}
				if (this.m_permanentData)
				{
					securityElement.AddAttribute("Permanent", this.m_permanentData.ToString());
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000C1AB4 File Offset: 0x000C0AB4
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			this.m_allowed = IsolatedStorageContainment.None;
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
			else
			{
				string text = esd.Attribute("Allowed");
				if (text != null)
				{
					this.m_allowed = (IsolatedStorageContainment)Enum.Parse(typeof(IsolatedStorageContainment), text);
				}
			}
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				this.m_userQuota = long.MaxValue;
				this.m_machineQuota = long.MaxValue;
				this.m_expirationDays = long.MaxValue;
				this.m_permanentData = true;
				return;
			}
			string text2 = esd.Attribute("UserQuota");
			this.m_userQuota = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("MachineQuota");
			this.m_machineQuota = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("Expiry");
			this.m_expirationDays = ((text2 != null) ? long.Parse(text2, CultureInfo.InvariantCulture) : 0L);
			text2 = esd.Attribute("Permanent");
			this.m_permanentData = (text2 != null && bool.Parse(text2));
		}

		// Token: 0x04001DE3 RID: 7651
		private const string _strUserQuota = "UserQuota";

		// Token: 0x04001DE4 RID: 7652
		private const string _strMachineQuota = "MachineQuota";

		// Token: 0x04001DE5 RID: 7653
		private const string _strExpiry = "Expiry";

		// Token: 0x04001DE6 RID: 7654
		private const string _strPermDat = "Permanent";

		// Token: 0x04001DE7 RID: 7655
		internal long m_userQuota;

		// Token: 0x04001DE8 RID: 7656
		internal long m_machineQuota;

		// Token: 0x04001DE9 RID: 7657
		internal long m_expirationDays;

		// Token: 0x04001DEA RID: 7658
		internal bool m_permanentData;

		// Token: 0x04001DEB RID: 7659
		internal IsolatedStorageContainment m_allowed;
	}
}
