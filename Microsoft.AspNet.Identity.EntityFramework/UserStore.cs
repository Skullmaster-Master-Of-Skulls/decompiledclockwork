using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000004 RID: 4
	public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, IQueryableUserStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, IUserPhoneNumberStore<TUser, TKey>, IUserTwoFactorStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, IUserStore<TUser, TKey>, IDisposable where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim> where TRole : IdentityRole<TKey, TUserRole> where TKey : IEquatable<TKey> where TUserLogin : IdentityUserLogin<TKey>, new() where TUserRole : IdentityUserRole<TKey>, new() where TUserClaim : IdentityUserClaim<TKey>, new()
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000026DC File Offset: 0x000008DC
		public UserStore(DbContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.Context = context;
			this.AutoSaveChanges = true;
			this._userStore = new EntityStore<TUser>(context);
			this._roleStore = new EntityStore<TRole>(context);
			this._logins = this.Context.Set<TUserLogin>();
			this._userClaims = this.Context.Set<TUserClaim>();
			this._userRoles = this.Context.Set<TUserRole>();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000275E File Offset: 0x0000095E
		public DbContext Context { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000276F File Offset: 0x0000096F
		public bool DisposeContext { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002778 File Offset: 0x00000978
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002780 File Offset: 0x00000980
		public bool AutoSaveChanges { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002789 File Offset: 0x00000989
		public IQueryable<TUser> Users
		{
			get
			{
				return this._userStore.EntitySet;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028F8 File Offset: 0x00000AF8
		public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			await this.EnsureClaimsLoaded(user).WithCurrentCulture();
			return (from c in user.Claims
			select new Claim(c.ClaimType, c.ClaimValue)).ToList<Claim>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002948 File Offset: 0x00000B48
		public virtual Task AddClaimAsync(TUser user, Claim claim)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			IDbSet<TUserClaim> userClaims = this._userClaims;
			TUserClaim entity = Activator.CreateInstance<TUserClaim>();
			entity.UserId = user.Id;
			entity.ClaimType = claim.Type;
			entity.ClaimValue = claim.Value;
			userClaims.Add(entity);
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			string claimValue = claim.Value;
			string claimType = claim.Type;
			IEnumerable<TUserClaim> claims;
			if (this.AreClaimsLoaded(user))
			{
				claims = (from uc in user.Claims
				where uc.ClaimValue == claimValue && uc.ClaimType == claimType
				select uc).ToList<TUserClaim>();
			}
			else
			{
				TKey userId = user.Id;
				claims = await(from uc in this._userClaims
				where uc.ClaimValue == claimValue && uc.ClaimType == claimType && uc.UserId.Equals(userId)
				select uc).ToListAsync<TUserClaim>().WithCurrentCulture<List<TUserClaim>>();
			}
			foreach (TUserClaim entity in claims)
			{
				this._userClaims.Remove(entity);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E0A File Offset: 0x0000100A
		public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<bool>(user.EmailConfirmed);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002E37 File Offset: 0x00001037
		public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.EmailConfirmed = confirmed;
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002E66 File Offset: 0x00001066
		public virtual Task SetEmailAsync(TUser user, string email)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.Email = email;
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002E95 File Offset: 0x00001095
		public virtual Task<string> GetEmailAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<string>(user.Email);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002ECC File Offset: 0x000010CC
		public virtual Task<TUser> FindByEmailAsync(string email)
		{
			this.ThrowIfDisposed();
			return this.GetUserAggregateAsync((TUser u) => u.Email.ToUpper() == email.ToUpper());
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002F98 File Offset: 0x00001198
		public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<DateTimeOffset>((user.LockoutEndDateUtc != null) ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : default(DateTimeOffset));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003008 File Offset: 0x00001208
		public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.LockoutEndDateUtc = ((lockoutEnd == DateTimeOffset.MinValue) ? null : new DateTime?(lockoutEnd.UtcDateTime));
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003068 File Offset: 0x00001268
		public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.AccessFailedCount++;
			return Task.FromResult<int>(user.AccessFailedCount);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000030BB File Offset: 0x000012BB
		public virtual Task ResetAccessFailedCountAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.AccessFailedCount = 0;
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000030EA File Offset: 0x000012EA
		public virtual Task<int> GetAccessFailedCountAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<int>(user.AccessFailedCount);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003117 File Offset: 0x00001317
		public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<bool>(user.LockoutEnabled);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003144 File Offset: 0x00001344
		public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.LockoutEnabled = enabled;
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000317C File Offset: 0x0000137C
		public virtual Task<TUser> FindByIdAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			return this.GetUserAggregateAsync((TUser u) => u.Id.Equals(userId));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003228 File Offset: 0x00001428
		public virtual Task<TUser> FindByNameAsync(string userName)
		{
			this.ThrowIfDisposed();
			return this.GetUserAggregateAsync((TUser u) => u.UserName.ToUpper() == userName.ToUpper());
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003408 File Offset: 0x00001608
		public virtual async Task CreateAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			this._userStore.Create(user);
			await this.SaveChanges().WithCurrentCulture();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000356C File Offset: 0x0000176C
		public virtual async Task DeleteAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			this._userStore.Delete(user);
			await this.SaveChanges().WithCurrentCulture();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000036D0 File Offset: 0x000018D0
		public virtual async Task UpdateAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			this._userStore.Update(user);
			await this.SaveChanges().WithCurrentCulture();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000371E File Offset: 0x0000191E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003ABC File Offset: 0x00001CBC
		public virtual async Task<TUser> FindAsync(UserLoginInfo login)
		{
			UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass1f CS$<>8__locals1 = new UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass1f();
			this.ThrowIfDisposed();
			if (login == null)
			{
				throw new ArgumentNullException("login");
			}
			CS$<>8__locals1.provider = login.LoginProvider;
			CS$<>8__locals1.key = login.ProviderKey;
			TUserLogin userLogin = await this._logins.FirstOrDefaultAsync((TUserLogin l) => l.LoginProvider == CS$<>8__locals1.provider && l.ProviderKey == CS$<>8__locals1.key).WithCurrentCulture<TUserLogin>();
			TUser result;
			if (userLogin != null)
			{
				UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass21 CS$<>8__locals2 = new UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass21();
				CS$<>8__locals2.CS$<>8__locals20 = CS$<>8__locals1;
				CS$<>8__locals2.userId = userLogin.UserId;
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TUser), "u");
				result = await this.GetUserAggregateAsync(Expression.Lambda<Func<TUser, bool>>(Expression.Call(Expression.Property(parameterExpression2, methodof(IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>.get_Id())), methodof(IEquatable<TKey>.Equals(T)), new Expression[]
				{
					Expression.Field(Expression.Constant(CS$<>8__locals2), fieldof(UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass21.userId))
				}), new ParameterExpression[]
				{
					parameterExpression2
				})).WithCurrentCulture<TUser>();
			}
			else
			{
				result = default(TUser);
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003B0C File Offset: 0x00001D0C
		public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (login == null)
			{
				throw new ArgumentNullException("login");
			}
			IDbSet<TUserLogin> logins = this._logins;
			TUserLogin entity = Activator.CreateInstance<TUserLogin>();
			entity.UserId = user.Id;
			entity.ProviderKey = login.ProviderKey;
			entity.LoginProvider = login.LoginProvider;
			logins.Add(entity);
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003F34 File Offset: 0x00002134
		public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (login == null)
			{
				throw new ArgumentNullException("login");
			}
			string provider = login.LoginProvider;
			string key = login.ProviderKey;
			TUserLogin entry;
			if (this.AreLoginsLoaded(user))
			{
				entry = user.Logins.SingleOrDefault((TUserLogin ul) => ul.LoginProvider == provider && ul.ProviderKey == key);
			}
			else
			{
				TKey userId = user.Id;
				entry = await this._logins.SingleOrDefaultAsync((TUserLogin ul) => ul.LoginProvider == provider && ul.ProviderKey == key && ul.UserId.Equals(userId)).WithCurrentCulture<TUserLogin>();
			}
			if (entry != null)
			{
				this._logins.Remove(entry);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000040EC File Offset: 0x000022EC
		public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			await this.EnsureLoginsLoaded(user).WithCurrentCulture();
			return (from l in user.Logins
			select new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList<UserLoginInfo>();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000413A File Offset: 0x0000233A
		public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.PasswordHash = passwordHash;
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004169 File Offset: 0x00002369
		public virtual Task<string> GetPasswordHashAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<string>(user.PasswordHash);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004196 File Offset: 0x00002396
		public virtual Task<bool> HasPasswordAsync(TUser user)
		{
			return Task.FromResult<bool>(user.PasswordHash != null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000041B0 File Offset: 0x000023B0
		public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.PhoneNumber = phoneNumber;
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000041DF File Offset: 0x000023DF
		public virtual Task<string> GetPhoneNumberAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<string>(user.PhoneNumber);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000420C File Offset: 0x0000240C
		public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<bool>(user.PhoneNumberConfirmed);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004239 File Offset: 0x00002439
		public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.PhoneNumberConfirmed = confirmed;
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004528 File Offset: 0x00002728
		public virtual async Task AddToRoleAsync(TUser user, string roleName)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (string.IsNullOrWhiteSpace(roleName))
			{
				throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
			}
			TRole roleEntity = await this._roleStore.DbEntitySet.SingleOrDefaultAsync((TRole r) => r.Name.ToUpper() == roleName.ToUpper()).WithCurrentCulture<TRole>();
			if (roleEntity == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, new object[]
				{
					roleName
				}));
			}
			TUserRole tuserRole = Activator.CreateInstance<TUserRole>();
			tuserRole.UserId = user.Id;
			tuserRole.RoleId = roleEntity.Id;
			TUserRole ur = tuserRole;
			this._userRoles.Add(ur);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000049A4 File Offset: 0x00002BA4
		public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
		{
			UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass40 CS$<>8__locals1 = new UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass40();
			CS$<>8__locals1.roleName = roleName;
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (string.IsNullOrWhiteSpace(CS$<>8__locals1.roleName))
			{
				throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
			}
			TRole roleEntity = await this._roleStore.DbEntitySet.SingleOrDefaultAsync((TRole r) => r.Name.ToUpper() == CS$<>8__locals1.roleName.ToUpper()).WithCurrentCulture<TRole>();
			if (roleEntity != null)
			{
				UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass42 CS$<>8__locals2 = new UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass42();
				CS$<>8__locals2.CS$<>8__locals41 = CS$<>8__locals1;
				CS$<>8__locals2.roleId = roleEntity.Id;
				CS$<>8__locals2.userId = user.Id;
				IQueryable<TUserRole> userRoles = this._userRoles;
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TUserRole), "r");
				TUserRole userRole = await userRoles.FirstOrDefaultAsync(Expression.Lambda<Func<TUserRole, bool>>(Expression.AndAlso(Expression.Call(Expression.Field(Expression.Constant(CS$<>8__locals2), fieldof(UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass42.roleId)), methodof(IEquatable<TKey>.Equals(T)), new Expression[]
				{
					Expression.Property(parameterExpression2, methodof(IdentityUserRole<TKey>.get_RoleId()))
				}), Expression.Call(Expression.Property(parameterExpression2, methodof(IdentityUserRole<TKey>.get_UserId())), methodof(IEquatable<TKey>.Equals(T)), new Expression[]
				{
					Expression.Field(Expression.Constant(CS$<>8__locals2), fieldof(UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass42.userId))
				})), new ParameterExpression[]
				{
					parameterExpression2
				})).WithCurrentCulture<TUserRole>();
				if (userRole != null)
				{
					this._userRoles.Remove(userRole);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public virtual async Task<IList<string>> GetRolesAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			TKey userId = user.Id;
			IQueryable<string> query = from userRole in this._userRoles
			where userRole.UserId.Equals(userId)
			join role in this._roleStore.DbEntitySet on userRole.RoleId equals role.Id
			select role.Name;
			return await query.ToListAsync<string>().WithCurrentCulture<List<string>>();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000512C File Offset: 0x0000332C
		public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
		{
			UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass51 CS$<>8__locals1 = new UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass51();
			CS$<>8__locals1.roleName = roleName;
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (string.IsNullOrWhiteSpace(CS$<>8__locals1.roleName))
			{
				throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
			}
			TRole role = await this._roleStore.DbEntitySet.SingleOrDefaultAsync((TRole r) => r.Name.ToUpper() == CS$<>8__locals1.roleName.ToUpper()).WithCurrentCulture<TRole>();
			bool result;
			if (role != null)
			{
				UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass53 CS$<>8__locals2 = new UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass53();
				CS$<>8__locals2.CS$<>8__locals52 = CS$<>8__locals1;
				CS$<>8__locals2.userId = user.Id;
				CS$<>8__locals2.roleId = role.Id;
				IQueryable<TUserRole> userRoles = this._userRoles;
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TUserRole), "ur");
				result = await userRoles.AnyAsync(Expression.Lambda<Func<TUserRole, bool>>(Expression.AndAlso(Expression.Call(Expression.Property(parameterExpression2, methodof(IdentityUserRole<TKey>.get_RoleId())), methodof(IEquatable<TKey>.Equals(T)), new Expression[]
				{
					Expression.Field(Expression.Constant(CS$<>8__locals2), fieldof(UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass53.roleId))
				}), Expression.Call(Expression.Property(parameterExpression2, methodof(IdentityUserRole<TKey>.get_UserId())), methodof(IEquatable<TKey>.Equals(T)), new Expression[]
				{
					Expression.Field(Expression.Constant(CS$<>8__locals2), fieldof(UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.<>c__DisplayClass53.userId))
				})), new ParameterExpression[]
				{
					parameterExpression2
				})).WithCurrentCulture<bool>();
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00005182 File Offset: 0x00003382
		public virtual Task SetSecurityStampAsync(TUser user, string stamp)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.SecurityStamp = stamp;
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000051B1 File Offset: 0x000033B1
		public virtual Task<string> GetSecurityStampAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<string>(user.SecurityStamp);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000051DE File Offset: 0x000033DE
		public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.TwoFactorEnabled = enabled;
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000520D File Offset: 0x0000340D
		public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return Task.FromResult<bool>(user.TwoFactorEnabled);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005328 File Offset: 0x00003528
		private async Task SaveChanges()
		{
			if (this.AutoSaveChanges)
			{
				await this.Context.SaveChangesAsync().WithCurrentCulture<int>();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005370 File Offset: 0x00003570
		private bool AreClaimsLoaded(TUser user)
		{
			return this.Context.Entry<TUser>(user).Collection<TUserClaim>((TUser u) => u.Claims).IsLoaded;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005600 File Offset: 0x00003800
		private async Task EnsureClaimsLoaded(TUser user)
		{
			if (!this.AreClaimsLoaded(user))
			{
				TKey userId = user.Id;
				await(from uc in this._userClaims
				where uc.UserId.Equals(userId)
				select uc).LoadAsync().WithCurrentCulture();
				DbEntityEntry<TUser> dbEntityEntry = this.Context.Entry<TUser>(user);
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TUser), "u");
				dbEntityEntry.Collection<TUserClaim>(Expression.Lambda<Func<TUser, ICollection<TUserClaim>>>(Expression.Property(parameterExpression2, methodof(IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>.get_Claims())), new ParameterExpression[]
				{
					parameterExpression2
				})).IsLoaded = true;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000058D0 File Offset: 0x00003AD0
		private async Task EnsureRolesLoaded(TUser user)
		{
			if (!this.Context.Entry<TUser>(user).Collection<TUserRole>((TUser u) => u.Roles).IsLoaded)
			{
				TKey userId = user.Id;
				await(from uc in this._userRoles
				where uc.UserId.Equals(userId)
				select uc).LoadAsync().WithCurrentCulture();
				DbEntityEntry<TUser> dbEntityEntry = this.Context.Entry<TUser>(user);
				ParameterExpression parameterExpression3 = Expression.Parameter(typeof(TUser), "u");
				dbEntityEntry.Collection<TUserRole>(Expression.Lambda<Func<TUser, ICollection<TUserRole>>>(Expression.Property(parameterExpression3, methodof(IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>.get_Roles())), new ParameterExpression[]
				{
					parameterExpression3
				})).IsLoaded = true;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005920 File Offset: 0x00003B20
		private bool AreLoginsLoaded(TUser user)
		{
			return this.Context.Entry<TUser>(user).Collection<TUserLogin>((TUser u) => u.Logins).IsLoaded;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private async Task EnsureLoginsLoaded(TUser user)
		{
			if (!this.AreLoginsLoaded(user))
			{
				TKey userId = user.Id;
				await(from uc in this._logins
				where uc.UserId.Equals(userId)
				select uc).LoadAsync().WithCurrentCulture();
				DbEntityEntry<TUser> dbEntityEntry = this.Context.Entry<TUser>(user);
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(TUser), "u");
				dbEntityEntry.Collection<TUserLogin>(Expression.Lambda<Func<TUser, ICollection<TUserLogin>>>(Expression.Property(parameterExpression2, methodof(IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>.get_Logins())), new ParameterExpression[]
				{
					parameterExpression2
				})).IsLoaded = true;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005F48 File Offset: 0x00004148
		protected virtual async Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
		{
			TKey id;
			TUser user;
			if (UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.FindByIdFilterParser.TryMatchAndGetId(filter, out id))
			{
				user = await this._userStore.GetByIdAsync(id).WithCurrentCulture<TUser>();
			}
			else
			{
				user = await this.Users.FirstOrDefaultAsync(filter).WithCurrentCulture<TUser>();
			}
			if (user != null)
			{
				await this.EnsureClaimsLoaded(user).WithCurrentCulture();
				await this.EnsureLoginsLoaded(user).WithCurrentCulture();
				await this.EnsureRolesLoaded(user).WithCurrentCulture();
			}
			return user;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005F96 File Offset: 0x00004196
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005FB1 File Offset: 0x000041B1
		protected virtual void Dispose(bool disposing)
		{
			if (this.DisposeContext && disposing && this.Context != null)
			{
				this.Context.Dispose();
			}
			this._disposed = true;
			this.Context = null;
			this._userStore = null;
		}

		// Token: 0x04000005 RID: 5
		private readonly IDbSet<TUserLogin> _logins;

		// Token: 0x04000006 RID: 6
		private readonly EntityStore<TRole> _roleStore;

		// Token: 0x04000007 RID: 7
		private readonly IDbSet<TUserClaim> _userClaims;

		// Token: 0x04000008 RID: 8
		private readonly IDbSet<TUserRole> _userRoles;

		// Token: 0x04000009 RID: 9
		private bool _disposed;

		// Token: 0x0400000A RID: 10
		private EntityStore<TUser> _userStore;

		// Token: 0x02000005 RID: 5
		private static class FindByIdFilterParser
		{
			// Token: 0x0600004C RID: 76 RVA: 0x00005FE8 File Offset: 0x000041E8
			internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey id)
			{
				id = default(TKey);
				if (filter.Body.NodeType != ExpressionType.Call)
				{
					return false;
				}
				MethodCallExpression methodCallExpression = (MethodCallExpression)filter.Body;
				if (methodCallExpression.Method != UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.FindByIdFilterParser.EqualsMethodInfo)
				{
					return false;
				}
				if (methodCallExpression.Object == null || methodCallExpression.Object.NodeType != ExpressionType.MemberAccess || ((MemberExpression)methodCallExpression.Object).Member != UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.FindByIdFilterParser.UserIdMemberInfo)
				{
					return false;
				}
				if (methodCallExpression.Arguments.Count != 1)
				{
					return false;
				}
				MemberExpression memberExpression;
				if (methodCallExpression.Arguments[0].NodeType == ExpressionType.Convert)
				{
					UnaryExpression unaryExpression = (UnaryExpression)methodCallExpression.Arguments[0];
					if (unaryExpression.Operand.NodeType != ExpressionType.MemberAccess)
					{
						return false;
					}
					memberExpression = (MemberExpression)unaryExpression.Operand;
				}
				else
				{
					if (methodCallExpression.Arguments[0].NodeType != ExpressionType.MemberAccess)
					{
						return false;
					}
					memberExpression = (MemberExpression)methodCallExpression.Arguments[0];
				}
				if (memberExpression.Member.MemberType != MemberTypes.Field || memberExpression.Expression.NodeType != ExpressionType.Constant)
				{
					return false;
				}
				FieldInfo fieldInfo = (FieldInfo)memberExpression.Member;
				object value = ((ConstantExpression)memberExpression.Expression).Value;
				id = (TKey)((object)fieldInfo.GetValue(value));
				return true;
			}

			// Token: 0x04000010 RID: 16
			private static readonly Expression<Func<TUser, bool>> Predicate = (TUser u) => u.Id.Equals(default(TKey));

			// Token: 0x04000011 RID: 17
			private static readonly MethodInfo EqualsMethodInfo = ((MethodCallExpression)UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.FindByIdFilterParser.Predicate.Body).Method;

			// Token: 0x04000012 RID: 18
			private static readonly MemberInfo UserIdMemberInfo = ((MemberExpression)((MethodCallExpression)UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>.FindByIdFilterParser.Predicate.Body).Object).Member;
		}
	}
}
