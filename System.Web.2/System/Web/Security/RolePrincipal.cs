using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Hosting;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005F4 RID: 1524
	[Serializable]
	public class RolePrincipal : ClaimsPrincipal, ISerializable
	{
		// Token: 0x06004CCD RID: 19661 RVA: 0x0010696C File Offset: 0x00104B6C
		public RolePrincipal(IIdentity identity, string encryptedTicket)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (encryptedTicket == null)
			{
				throw new ArgumentNullException("encryptedTicket");
			}
			this._Identity = identity;
			this._ProviderName = Roles.Provider.Name;
			if (identity.IsAuthenticated)
			{
				this.InitFromEncryptedTicket(encryptedTicket);
				return;
			}
			this.Init();
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x001069C8 File Offset: 0x00104BC8
		public RolePrincipal(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this._Identity = identity;
			this.Init();
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x001069EC File Offset: 0x00104BEC
		public RolePrincipal(string providerName, IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (providerName == null)
			{
				throw new ArgumentException(SR.GetString("Role_provider_name_invalid"), "providerName");
			}
			this._ProviderName = providerName;
			if (Roles.Providers[providerName] == null)
			{
				throw new ArgumentException(SR.GetString("Role_provider_name_invalid"), "providerName");
			}
			this._Identity = identity;
			this.Init();
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x00106A5C File Offset: 0x00104C5C
		public RolePrincipal(string providerName, IIdentity identity, string encryptedTicket)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (encryptedTicket == null)
			{
				throw new ArgumentNullException("encryptedTicket");
			}
			if (providerName == null)
			{
				throw new ArgumentException(SR.GetString("Role_provider_name_invalid"), "providerName");
			}
			this._ProviderName = providerName;
			if (Roles.Providers[this._ProviderName] == null)
			{
				throw new ArgumentException(SR.GetString("Role_provider_name_invalid"), "providerName");
			}
			this._Identity = identity;
			if (identity.IsAuthenticated)
			{
				this.InitFromEncryptedTicket(encryptedTicket);
				return;
			}
			this.Init();
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00106AF0 File Offset: 0x00104CF0
		private void InitFromEncryptedTicket(string encryptedTicket)
		{
			if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_BEGIN, HttpContext.Current.WorkerRequest);
			}
			if (!string.IsNullOrEmpty(encryptedTicket))
			{
				byte[] array = CookieProtectionHelper.Decode(Roles.CookieProtectionValue, encryptedTicket, Purpose.RolePrincipal_Ticket);
				if (array != null)
				{
					RolePrincipal rolePrincipal = null;
					MemoryStream memoryStream = null;
					try
					{
						memoryStream = new MemoryStream(array);
						rolePrincipal = (new BinaryFormatter().Deserialize(memoryStream) as RolePrincipal);
					}
					catch
					{
					}
					finally
					{
						memoryStream.Close();
					}
					if (rolePrincipal != null && StringUtil.EqualsIgnoreCase(rolePrincipal._Username, this._Identity.Name) && StringUtil.EqualsIgnoreCase(rolePrincipal._ProviderName, this._ProviderName) && !(DateTime.UtcNow > rolePrincipal._ExpireDate))
					{
						this._Version = rolePrincipal._Version;
						this._ExpireDate = rolePrincipal._ExpireDate;
						this._IssueDate = rolePrincipal._IssueDate;
						this._IsRoleListCached = rolePrincipal._IsRoleListCached;
						this._CachedListChanged = false;
						this._Username = rolePrincipal._Username;
						this._Roles = rolePrincipal._Roles;
						this.RenewIfOld();
						if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_END, HttpContext.Current.WorkerRequest, "RolePrincipal", this._Identity.Name);
						}
						return;
					}
				}
			}
			this.Init();
			this._CachedListChanged = true;
			if (HostingEnvironment.IsHosted && EtwTrace.IsTraceEnabled(4, 8) && HttpContext.Current != null)
			{
				EtwTrace.Trace(EtwTraceType.ETW_TYPE_ROLE_END, HttpContext.Current.WorkerRequest, "RolePrincipal", this._Identity.Name);
			}
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x00106CAC File Offset: 0x00104EAC
		private void Init()
		{
			this._Version = 1;
			this._IssueDate = DateTime.UtcNow;
			this._ExpireDate = DateTime.UtcNow.AddMinutes((double)Roles.CookieTimeout);
			this._IsRoleListCached = false;
			this._CachedListChanged = false;
			if (this._ProviderName == null)
			{
				this._ProviderName = Roles.Provider.Name;
			}
			if (this._Roles == null)
			{
				this._Roles = new HybridDictionary(true);
			}
			if (this._Identity != null)
			{
				this._Username = this._Identity.Name;
			}
			this.AddIdentityAttachingRoles(this._Identity);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x00106D44 File Offset: 0x00104F44
		[SecuritySafeCritical]
		private void AddIdentityAttachingRoles(IIdentity identity)
		{
			ClaimsIdentity claimsIdentity;
			if (identity is ClaimsIdentity)
			{
				claimsIdentity = (identity as ClaimsIdentity).Clone();
			}
			else
			{
				claimsIdentity = new ClaimsIdentity(identity);
			}
			this.AttachRoleClaims(claimsIdentity);
			base.AddIdentity(claimsIdentity);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x00106D80 File Offset: 0x00104F80
		[SecuritySafeCritical]
		private void AttachRoleClaims(ClaimsIdentity claimsIdentity)
		{
			RoleClaimProvider roleClaimProvider = new RoleClaimProvider(this, claimsIdentity);
			if (RolePrincipal.s_type == null)
			{
				RolePrincipal.s_type = typeof(DynamicRoleClaimProvider);
			}
			RolePrincipal.s_type.InvokeMember("AddDynamicRoleClaims", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				claimsIdentity,
				roleClaimProvider.Claims
			}, CultureInfo.InvariantCulture);
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x00106DE0 File Offset: 0x00104FE0
		public int Version
		{
			get
			{
				return this._Version;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x00106DE8 File Offset: 0x00104FE8
		public DateTime ExpireDate
		{
			get
			{
				return this._ExpireDate.ToLocalTime();
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x00106DF5 File Offset: 0x00104FF5
		public DateTime IssueDate
		{
			get
			{
				return this._IssueDate.ToLocalTime();
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x00106E02 File Offset: 0x00105002
		public bool Expired
		{
			get
			{
				return this._ExpireDate < DateTime.UtcNow;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x00106E14 File Offset: 0x00105014
		public string CookiePath
		{
			get
			{
				return Roles.CookiePath;
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x00106E1B File Offset: 0x0010501B
		public override IIdentity Identity
		{
			get
			{
				return this._Identity;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x00106E23 File Offset: 0x00105023
		public bool IsRoleListCached
		{
			get
			{
				return this._IsRoleListCached;
			}
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x00106E2B File Offset: 0x0010502B
		public bool CachedListChanged
		{
			get
			{
				return this._CachedListChanged;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x00106E33 File Offset: 0x00105033
		public string ProviderName
		{
			get
			{
				return this._ProviderName;
			}
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x00106E3C File Offset: 0x0010503C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public string ToEncryptedTicket()
		{
			if (!Roles.Enabled)
			{
				return null;
			}
			if (this._Identity != null && !this._Identity.IsAuthenticated)
			{
				return null;
			}
			if (this._Identity == null && string.IsNullOrEmpty(this._Username))
			{
				return null;
			}
			if (this._Roles.Count > Roles.MaxCachedResults)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] buf = null;
			IIdentity identity = this._Identity;
			try
			{
				this._Identity = null;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				bool serializingForCookie = RolePrincipal._serializingForCookie;
				try
				{
					RolePrincipal._serializingForCookie = true;
					binaryFormatter.Serialize(memoryStream, this);
				}
				finally
				{
					RolePrincipal._serializingForCookie = serializingForCookie;
				}
				buf = memoryStream.ToArray();
			}
			finally
			{
				memoryStream.Close();
				this._Identity = identity;
			}
			return CookieProtectionHelper.Encode(Roles.CookieProtectionValue, buf, Purpose.RolePrincipal_Ticket);
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x00106F14 File Offset: 0x00105114
		private void RenewIfOld()
		{
			if (!Roles.CookieSlidingExpiration)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan t = utcNow - this._IssueDate;
			TimeSpan t2 = this._ExpireDate - utcNow;
			if (t2 > t)
			{
				return;
			}
			this._ExpireDate = utcNow + (this._ExpireDate - this._IssueDate);
			this._IssueDate = utcNow;
			this._CachedListChanged = true;
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x00106F80 File Offset: 0x00105180
		public string[] GetRoles()
		{
			if (this._Identity == null)
			{
				throw new ProviderException(SR.GetString("Role_Principal_not_fully_constructed"));
			}
			if (!this._Identity.IsAuthenticated)
			{
				return new string[0];
			}
			string[] array;
			if (!this._IsRoleListCached || !this._GetRolesCalled)
			{
				this._Roles.Clear();
				array = Roles.Providers[this._ProviderName].GetRolesForUser(this.Identity.Name);
				foreach (string key in array)
				{
					if (this._Roles[key] == null)
					{
						this._Roles.Add(key, string.Empty);
					}
				}
				this._IsRoleListCached = true;
				this._CachedListChanged = true;
				this._GetRolesCalled = true;
				return array;
			}
			array = new string[this._Roles.Count];
			int num = 0;
			foreach (object obj in this._Roles.Keys)
			{
				string text = (string)obj;
				array[num++] = text;
			}
			return array;
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x001070B4 File Offset: 0x001052B4
		public override bool IsInRole(string role)
		{
			if (this._Identity == null)
			{
				throw new ProviderException(SR.GetString("Role_Principal_not_fully_constructed"));
			}
			if (!this._Identity.IsAuthenticated || role == null)
			{
				return false;
			}
			role = role.Trim();
			if (!this.IsRoleListCached)
			{
				this._Roles.Clear();
				string[] rolesForUser = Roles.Providers[this._ProviderName].GetRolesForUser(this.Identity.Name);
				foreach (string key in rolesForUser)
				{
					if (this._Roles[key] == null)
					{
						this._Roles.Add(key, string.Empty);
					}
				}
				this._IsRoleListCached = true;
				this._CachedListChanged = true;
			}
			return this._Roles[role] != null || base.IsInRole(role);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0010717F File Offset: 0x0010537F
		public void SetDirty()
		{
			this._IsRoleListCached = false;
			this._CachedListChanged = true;
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x00107190 File Offset: 0x00105390
		protected RolePrincipal(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._Version = info.GetInt32("_Version");
			this._ExpireDate = info.GetDateTime("_ExpireDate");
			this._IssueDate = info.GetDateTime("_IssueDate");
			try
			{
				this._Identity = (info.GetValue("_Identity", typeof(IIdentity)) as IIdentity);
			}
			catch
			{
			}
			this._ProviderName = info.GetString("_ProviderName");
			this._Username = info.GetString("_Username");
			this._IsRoleListCached = info.GetBoolean("_IsRoleListCached");
			this._Roles = new HybridDictionary(true);
			string @string = info.GetString("_AllRoles");
			if (@string != null)
			{
				foreach (string key in @string.Split(new char[]
				{
					','
				}))
				{
					if (this._Roles[key] == null)
					{
						this._Roles.Add(key, string.Empty);
					}
				}
			}
			bool flag = false;
			foreach (ClaimsIdentity claimsIdentity in base.Identities)
			{
				if (claimsIdentity != null)
				{
					this.AttachRoleClaims(claimsIdentity);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.AddIdentityAttachingRoles(new ClaimsIdentity(this._Identity));
			}
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x00107304 File Offset: 0x00105504
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00107310 File Offset: 0x00105510
		protected override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (!RolePrincipal._serializingForCookie)
			{
				base.GetObjectData(info, context);
			}
			info.AddValue("_Version", this._Version);
			info.AddValue("_ExpireDate", this._ExpireDate);
			info.AddValue("_IssueDate", this._IssueDate);
			try
			{
				info.AddValue("_Identity", this._Identity);
			}
			catch
			{
			}
			info.AddValue("_ProviderName", this._ProviderName);
			info.AddValue("_Username", (this._Identity == null) ? this._Username : this._Identity.Name);
			info.AddValue("_IsRoleListCached", this._IsRoleListCached);
			if (this._Roles.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(this._Roles.Count * 10);
				foreach (object obj in this._Roles.Keys)
				{
					stringBuilder.Append((string)obj + ",");
				}
				string text = stringBuilder.ToString();
				info.AddValue("_AllRoles", text.Substring(0, text.Length - 1));
				return;
			}
			info.AddValue("_AllRoles", string.Empty);
		}

		// Token: 0x04002918 RID: 10520
		[NonSerialized]
		private static Type s_type;

		// Token: 0x04002919 RID: 10521
		private int _Version;

		// Token: 0x0400291A RID: 10522
		private DateTime _ExpireDate;

		// Token: 0x0400291B RID: 10523
		private DateTime _IssueDate;

		// Token: 0x0400291C RID: 10524
		private IIdentity _Identity;

		// Token: 0x0400291D RID: 10525
		private string _ProviderName;

		// Token: 0x0400291E RID: 10526
		private string _Username;

		// Token: 0x0400291F RID: 10527
		private bool _IsRoleListCached;

		// Token: 0x04002920 RID: 10528
		private bool _CachedListChanged;

		// Token: 0x04002921 RID: 10529
		[ThreadStatic]
		private static bool _serializingForCookie;

		// Token: 0x04002922 RID: 10530
		[NonSerialized]
		private HybridDictionary _Roles;

		// Token: 0x04002923 RID: 10531
		[NonSerialized]
		private bool _GetRolesCalled;
	}
}
