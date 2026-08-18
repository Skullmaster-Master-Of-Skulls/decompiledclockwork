using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000044 RID: 68
	public class UserManager<TUser, TKey> : IDisposable where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006B9C File Offset: 0x00004D9C
		public UserManager(IUserStore<TUser, TKey> store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			this.Store = store;
			this.UserValidator = new UserValidator<TUser, TKey>(this);
			this.PasswordValidator = new MinimumLengthValidator(6);
			this.PasswordHasher = new PasswordHasher();
			this.ClaimsIdentityFactory = new ClaimsIdentityFactory<TUser, TKey>();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006C08 File Offset: 0x00004E08
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006C10 File Offset: 0x00004E10
		protected internal IUserStore<TUser, TKey> Store { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006C19 File Offset: 0x00004E19
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00006C27 File Offset: 0x00004E27
		public IPasswordHasher PasswordHasher
		{
			get
			{
				this.ThrowIfDisposed();
				return this._passwordHasher;
			}
			set
			{
				this.ThrowIfDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._passwordHasher = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006C44 File Offset: 0x00004E44
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006C52 File Offset: 0x00004E52
		public IIdentityValidator<TUser> UserValidator
		{
			get
			{
				this.ThrowIfDisposed();
				return this._userValidator;
			}
			set
			{
				this.ThrowIfDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._userValidator = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006C6F File Offset: 0x00004E6F
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00006C7D File Offset: 0x00004E7D
		public IIdentityValidator<string> PasswordValidator
		{
			get
			{
				this.ThrowIfDisposed();
				return this._passwordValidator;
			}
			set
			{
				this.ThrowIfDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._passwordValidator = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006C9A File Offset: 0x00004E9A
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory
		{
			get
			{
				this.ThrowIfDisposed();
				return this._claimsFactory;
			}
			set
			{
				this.ThrowIfDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._claimsFactory = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006CC5 File Offset: 0x00004EC5
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00006CCD File Offset: 0x00004ECD
		public IIdentityMessageService EmailService { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006CD6 File Offset: 0x00004ED6
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006CDE File Offset: 0x00004EDE
		public IIdentityMessageService SmsService { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006CE7 File Offset: 0x00004EE7
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006CEF File Offset: 0x00004EEF
		public IUserTokenProvider<TUser, TKey> UserTokenProvider { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006CF8 File Offset: 0x00004EF8
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006D00 File Offset: 0x00004F00
		public bool UserLockoutEnabledByDefault { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006D09 File Offset: 0x00004F09
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00006D11 File Offset: 0x00004F11
		public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006D1A File Offset: 0x00004F1A
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00006D22 File Offset: 0x00004F22
		public TimeSpan DefaultAccountLockoutTimeSpan
		{
			get
			{
				return this._defaultLockout;
			}
			set
			{
				this._defaultLockout = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006D2B File Offset: 0x00004F2B
		public virtual bool SupportsUserTwoFactor
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserTwoFactorStore<TUser, TKey>;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006D41 File Offset: 0x00004F41
		public virtual bool SupportsUserPassword
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserPasswordStore<TUser, TKey>;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006D57 File Offset: 0x00004F57
		public virtual bool SupportsUserSecurityStamp
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserSecurityStampStore<TUser, TKey>;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006D6D File Offset: 0x00004F6D
		public virtual bool SupportsUserRole
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserRoleStore<TUser, TKey>;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006D83 File Offset: 0x00004F83
		public virtual bool SupportsUserLogin
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserLoginStore<TUser, TKey>;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006D99 File Offset: 0x00004F99
		public virtual bool SupportsUserEmail
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserEmailStore<TUser, TKey>;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006DAF File Offset: 0x00004FAF
		public virtual bool SupportsUserPhoneNumber
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserPhoneNumberStore<TUser, TKey>;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006DC5 File Offset: 0x00004FC5
		public virtual bool SupportsUserClaim
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserClaimStore<TUser, TKey>;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006DDB File Offset: 0x00004FDB
		public virtual bool SupportsUserLockout
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IUserLockoutStore<TUser, TKey>;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public virtual bool SupportsQueryableUsers
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IQueryableUserStore<TUser, TKey>;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006E08 File Offset: 0x00005008
		public virtual IQueryable<TUser> Users
		{
			get
			{
				IQueryableUserStore<TUser, TKey> queryableUserStore = this.Store as IQueryableUserStore<TUser, TKey>;
				if (queryableUserStore == null)
				{
					throw new NotSupportedException(Resources.StoreNotIQueryableUserStore);
				}
				return queryableUserStore.Users;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006E35 File Offset: 0x00005035
		public IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders
		{
			get
			{
				return this._factors;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006E3D File Offset: 0x0000503D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006E4C File Offset: 0x0000504C
		public virtual Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			return this.ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007150 File Offset: 0x00005350
		public virtual async Task<IdentityResult> CreateAsync(TUser user)
		{
			this.ThrowIfDisposed();
			await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
			IdentityResult result = await this.UserValidator.ValidateAsync(user).WithCurrentCulture<IdentityResult>();
			IdentityResult result2;
			if (!result.Succeeded)
			{
				result2 = result;
			}
			else
			{
				if (this.UserLockoutEnabledByDefault && this.SupportsUserLockout)
				{
					await this.GetUserLockoutStore().SetLockoutEnabledAsync(user, true).WithCurrentCulture();
				}
				await this.Store.CreateAsync(user).WithCurrentCulture();
				result2 = IdentityResult.Success;
			}
			return result2;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007368 File Offset: 0x00005568
		public virtual async Task<IdentityResult> UpdateAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			IdentityResult result = await this.UserValidator.ValidateAsync(user).WithCurrentCulture<IdentityResult>();
			IdentityResult result2;
			if (!result.Succeeded)
			{
				result2 = result;
			}
			else
			{
				await this.Store.UpdateAsync(user).WithCurrentCulture();
				result2 = IdentityResult.Success;
			}
			return result2;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000074B0 File Offset: 0x000056B0
		public virtual async Task<IdentityResult> DeleteAsync(TUser user)
		{
			this.ThrowIfDisposed();
			await this.Store.DeleteAsync(user).WithCurrentCulture();
			return IdentityResult.Success;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000074FE File Offset: 0x000056FE
		public virtual Task<TUser> FindByIdAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			return this.Store.FindByIdAsync(userId);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007512 File Offset: 0x00005712
		public virtual Task<TUser> FindByNameAsync(string userName)
		{
			this.ThrowIfDisposed();
			if (userName == null)
			{
				throw new ArgumentNullException("userName");
			}
			return this.Store.FindByNameAsync(userName);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007534 File Offset: 0x00005734
		private IUserPasswordStore<TUser, TKey> GetPasswordStore()
		{
			IUserPasswordStore<TUser, TKey> userPasswordStore = this.Store as IUserPasswordStore<TUser, TKey>;
			if (userPasswordStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
			}
			return userPasswordStore;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007748 File Offset: 0x00005948
		public virtual async Task<IdentityResult> CreateAsync(TUser user, string password)
		{
			this.ThrowIfDisposed();
			IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			IdentityResult result = await this.UpdatePassword(passwordStore, user, password).WithCurrentCulture<IdentityResult>();
			IdentityResult result2;
			if (!result.Succeeded)
			{
				result2 = result;
			}
			else
			{
				result2 = await this.CreateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result2;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007960 File Offset: 0x00005B60
		public virtual async Task<TUser> FindAsync(string userName, string password)
		{
			this.ThrowIfDisposed();
			TUser user = await this.FindByNameAsync(userName).WithCurrentCulture<TUser>();
			TUser result;
			if (user == null)
			{
				result = default(TUser);
			}
			else
			{
				result = ((await this.CheckPasswordAsync(user, password).WithCurrentCulture<bool>()) ? user : default(TUser));
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public virtual async Task<bool> CheckPasswordAsync(TUser user, string password)
		{
			this.ThrowIfDisposed();
			IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
			bool result;
			if (user == null)
			{
				result = false;
			}
			else
			{
				result = await this.VerifyPasswordAsync(passwordStore, user, password).WithCurrentCulture<bool>();
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007D04 File Offset: 0x00005F04
		public virtual async Task<bool> HasPasswordAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await passwordStore.HasPasswordAsync(user).WithCurrentCulture<bool>();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008088 File Offset: 0x00006288
		public virtual async Task<IdentityResult> AddPasswordAsync(TKey userId, string password)
		{
			this.ThrowIfDisposed();
			IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			string hash = await passwordStore.GetPasswordHashAsync(user).WithCurrentCulture<string>();
			IdentityResult result2;
			if (hash != null)
			{
				result2 = new IdentityResult(new string[]
				{
					Resources.UserAlreadyHasPassword
				});
			}
			else
			{
				IdentityResult result = await this.UpdatePassword(passwordStore, user, password).WithCurrentCulture<IdentityResult>();
				if (!result.Succeeded)
				{
					result2 = result;
				}
				else
				{
					result2 = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
				}
			}
			return result2;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008410 File Offset: 0x00006610
		public virtual async Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword)
		{
			this.ThrowIfDisposed();
			IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result2;
			if (await this.VerifyPasswordAsync(passwordStore, user, currentPassword).WithCurrentCulture<bool>())
			{
				IdentityResult result = await this.UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture<IdentityResult>();
				if (!result.Succeeded)
				{
					result2 = result;
				}
				else
				{
					result2 = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
				}
			}
			else
			{
				result2 = IdentityResult.Failed(new string[]
				{
					Resources.PasswordMismatch
				});
			}
			return result2;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008748 File Offset: 0x00006948
		public virtual async Task<IdentityResult> RemovePasswordAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await passwordStore.SetPasswordHashAsync(user, null).WithCurrentCulture();
			await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual async Task<IdentityResult> UpdatePassword(IUserPasswordStore<TUser, TKey> passwordStore, TUser user, string newPassword)
		{
			IdentityResult result = await this.PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture<IdentityResult>();
			IdentityResult result2;
			if (!result.Succeeded)
			{
				result2 = result;
			}
			else
			{
				await passwordStore.SetPasswordHashAsync(user, this.PasswordHasher.HashPassword(newPassword)).WithCurrentCulture();
				await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
				result2 = IdentityResult.Success;
			}
			return result2;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008B40 File Offset: 0x00006D40
		protected virtual async Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, TKey> store, TUser user, string password)
		{
			string hash = await store.GetPasswordHashAsync(user).WithCurrentCulture<string>();
			return this.PasswordHasher.VerifyHashedPassword(hash, password) != PasswordVerificationResult.Failed;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008BA0 File Offset: 0x00006DA0
		private IUserSecurityStampStore<TUser, TKey> GetSecurityStore()
		{
			IUserSecurityStampStore<TUser, TKey> userSecurityStampStore = this.Store as IUserSecurityStampStore<TUser, TKey>;
			if (userSecurityStampStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
			}
			return userSecurityStampStore;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008D9C File Offset: 0x00006F9C
		public virtual async Task<string> GetSecurityStampAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserSecurityStampStore<TUser, TKey> securityStore = this.GetSecurityStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await securityStore.GetSecurityStampAsync(user).WithCurrentCulture<string>();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009048 File Offset: 0x00007248
		public virtual async Task<IdentityResult> UpdateSecurityStampAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserSecurityStampStore<TUser, TKey> securityStore = this.GetSecurityStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await securityStore.SetSecurityStampAsync(user, UserManager<TUser, TKey>.NewSecurityStamp()).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00009096 File Offset: 0x00007296
		public virtual Task<string> GeneratePasswordResetTokenAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			return this.GenerateUserTokenAsync("ResetPassword", userId);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000093DC File Offset: 0x000075DC
		public virtual async Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
		{
			this.ThrowIfDisposed();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result2;
			if (!(await this.VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture<bool>()))
			{
				result2 = IdentityResult.Failed(new string[]
				{
					Resources.InvalidToken
				});
			}
			else
			{
				IUserPasswordStore<TUser, TKey> passwordStore = this.GetPasswordStore();
				IdentityResult result = await this.UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture<IdentityResult>();
				if (!result.Succeeded)
				{
					result2 = result;
				}
				else
				{
					result2 = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
				}
			}
			return result2;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009534 File Offset: 0x00007734
		internal async Task UpdateSecurityStampInternal(TUser user)
		{
			if (this.SupportsUserSecurityStamp)
			{
				await this.GetSecurityStore().SetSecurityStampAsync(user, UserManager<TUser, TKey>.NewSecurityStamp()).WithCurrentCulture();
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009584 File Offset: 0x00007784
		private static string NewSecurityStamp()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000095A4 File Offset: 0x000077A4
		private IUserLoginStore<TUser, TKey> GetLoginStore()
		{
			IUserLoginStore<TUser, TKey> userLoginStore = this.Store as IUserLoginStore<TUser, TKey>;
			if (userLoginStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserLoginStore);
			}
			return userLoginStore;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000095CC File Offset: 0x000077CC
		public virtual Task<TUser> FindAsync(UserLoginInfo login)
		{
			this.ThrowIfDisposed();
			return this.GetLoginStore().FindAsync(login);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000098D0 File Offset: 0x00007AD0
		public virtual async Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login)
		{
			this.ThrowIfDisposed();
			IUserLoginStore<TUser, TKey> loginStore = this.GetLoginStore();
			if (login == null)
			{
				throw new ArgumentNullException("login");
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await loginStore.RemoveLoginAsync(user, login).WithCurrentCulture();
			await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009C4C File Offset: 0x00007E4C
		public virtual async Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login)
		{
			this.ThrowIfDisposed();
			IUserLoginStore<TUser, TKey> loginStore = this.GetLoginStore();
			if (login == null)
			{
				throw new ArgumentNullException("login");
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			TUser existingUser = await this.FindAsync(login).WithCurrentCulture<TUser>();
			IdentityResult result;
			if (existingUser != null)
			{
				result = IdentityResult.Failed(new string[]
				{
					Resources.ExternalLoginExists
				});
			}
			else
			{
				await loginStore.AddLoginAsync(user, login).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009E78 File Offset: 0x00008078
		public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLoginStore<TUser, TKey> loginStore = this.GetLoginStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await loginStore.GetLoginsAsync(user).WithCurrentCulture<IList<UserLoginInfo>>();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009EC8 File Offset: 0x000080C8
		private IUserClaimStore<TUser, TKey> GetClaimStore()
		{
			IUserClaimStore<TUser, TKey> userClaimStore = this.Store as IUserClaimStore<TUser, TKey>;
			if (userClaimStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserClaimStore);
			}
			return userClaimStore;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A160 File Offset: 0x00008360
		public virtual async Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim)
		{
			this.ThrowIfDisposed();
			IUserClaimStore<TUser, TKey> claimStore = this.GetClaimStore();
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await claimStore.AddClaimAsync(user, claim).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A414 File Offset: 0x00008614
		public virtual async Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim)
		{
			this.ThrowIfDisposed();
			IUserClaimStore<TUser, TKey> claimStore = this.GetClaimStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await claimStore.RemoveClaimAsync(user, claim).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A640 File Offset: 0x00008840
		public virtual async Task<IList<Claim>> GetClaimsAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserClaimStore<TUser, TKey> claimStore = this.GetClaimStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await claimStore.GetClaimsAsync(user).WithCurrentCulture<IList<Claim>>();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000A690 File Offset: 0x00008890
		private IUserRoleStore<TUser, TKey> GetUserRoleStore()
		{
			IUserRoleStore<TUser, TKey> userRoleStore = this.Store as IUserRoleStore<TUser, TKey>;
			if (userRoleStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserRoleStore);
			}
			return userRoleStore;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public virtual async Task<IdentityResult> AddToRoleAsync(TKey userId, string role)
		{
			this.ThrowIfDisposed();
			IUserRoleStore<TUser, TKey> userRoleStore = this.GetUserRoleStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IList<string> userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture<IList<string>>();
			IdentityResult result;
			if (userRoles.Contains(role))
			{
				result = new IdentityResult(new string[]
				{
					Resources.UserAlreadyInRole
				});
			}
			else
			{
				await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public virtual async Task<IdentityResult> AddToRolesAsync(TKey userId, params string[] roles)
		{
			this.ThrowIfDisposed();
			IUserRoleStore<TUser, TKey> userRoleStore = this.GetUserRoleStore();
			if (roles == null)
			{
				throw new ArgumentNullException("roles");
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IList<string> userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture<IList<string>>();
			foreach (string r in roles)
			{
				if (userRoles.Contains(r))
				{
					return new IdentityResult(new string[]
					{
						Resources.UserAlreadyInRole
					});
				}
				await userRoleStore.AddToRoleAsync(user, r).WithCurrentCulture();
			}
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B190 File Offset: 0x00009390
		public virtual async Task<IdentityResult> RemoveFromRolesAsync(TKey userId, params string[] roles)
		{
			this.ThrowIfDisposed();
			IUserRoleStore<TUser, TKey> userRoleStore = this.GetUserRoleStore();
			if (roles == null)
			{
				throw new ArgumentNullException("roles");
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IList<string> userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture<IList<string>>();
			foreach (string role in roles)
			{
				if (!userRoles.Contains(role))
				{
					return new IdentityResult(new string[]
					{
						Resources.UserNotInRole
					});
				}
				await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
			}
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public virtual async Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role)
		{
			this.ThrowIfDisposed();
			IUserRoleStore<TUser, TKey> userRoleStore = this.GetUserRoleStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result;
			if (!(await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture<bool>()))
			{
				result = new IdentityResult(new string[]
				{
					Resources.UserNotInRole
				});
			}
			else
			{
				await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B714 File Offset: 0x00009914
		public virtual async Task<IList<string>> GetRolesAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserRoleStore<TUser, TKey> userRoleStore = this.GetUserRoleStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await userRoleStore.GetRolesAsync(user).WithCurrentCulture<IList<string>>();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B940 File Offset: 0x00009B40
		public virtual async Task<bool> IsInRoleAsync(TKey userId, string role)
		{
			this.ThrowIfDisposed();
			IUserRoleStore<TUser, TKey> userRoleStore = this.GetUserRoleStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture<bool>();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B998 File Offset: 0x00009B98
		internal IUserEmailStore<TUser, TKey> GetEmailStore()
		{
			IUserEmailStore<TUser, TKey> userEmailStore = this.Store as IUserEmailStore<TUser, TKey>;
			if (userEmailStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
			}
			return userEmailStore;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000BB94 File Offset: 0x00009D94
		public virtual async Task<string> GetEmailAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserEmailStore<TUser, TKey> store = this.GetEmailStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetEmailAsync(user).WithCurrentCulture<string>();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000BF40 File Offset: 0x0000A140
		public virtual async Task<IdentityResult> SetEmailAsync(TKey userId, string email)
		{
			this.ThrowIfDisposed();
			IUserEmailStore<TUser, TKey> store = this.GetEmailStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await store.SetEmailAsync(user, email).WithCurrentCulture();
			await store.SetEmailConfirmedAsync(user, false).WithCurrentCulture();
			await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000BF98 File Offset: 0x0000A198
		public virtual Task<TUser> FindByEmailAsync(string email)
		{
			this.ThrowIfDisposed();
			IUserEmailStore<TUser, TKey> emailStore = this.GetEmailStore();
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			return emailStore.FindByEmailAsync(email);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000BFC7 File Offset: 0x0000A1C7
		public virtual Task<string> GenerateEmailConfirmationTokenAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			return this.GenerateUserTokenAsync("Confirmation", userId);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		public virtual async Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token)
		{
			this.ThrowIfDisposed();
			IUserEmailStore<TUser, TKey> store = this.GetEmailStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result;
			if (!(await this.VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture<bool>()))
			{
				result = IdentityResult.Failed(new string[]
				{
					Resources.InvalidToken
				});
			}
			else
			{
				await store.SetEmailConfirmedAsync(user, true).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000C508 File Offset: 0x0000A708
		public virtual async Task<bool> IsEmailConfirmedAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserEmailStore<TUser, TKey> store = this.GetEmailStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetEmailConfirmedAsync(user).WithCurrentCulture<bool>();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000C558 File Offset: 0x0000A758
		internal IUserPhoneNumberStore<TUser, TKey> GetPhoneNumberStore()
		{
			IUserPhoneNumberStore<TUser, TKey> userPhoneNumberStore = this.Store as IUserPhoneNumberStore<TUser, TKey>;
			if (userPhoneNumberStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserPhoneNumberStore);
			}
			return userPhoneNumberStore;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000C754 File Offset: 0x0000A954
		public virtual async Task<string> GetPhoneNumberAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserPhoneNumberStore<TUser, TKey> store = this.GetPhoneNumberStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetPhoneNumberAsync(user).WithCurrentCulture<string>();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public virtual async Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber)
		{
			this.ThrowIfDisposed();
			IUserPhoneNumberStore<TUser, TKey> store = this.GetPhoneNumberStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
			await store.SetPhoneNumberConfirmedAsync(user, false).WithCurrentCulture();
			await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CF60 File Offset: 0x0000B160
		public virtual async Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token)
		{
			this.ThrowIfDisposed();
			IUserPhoneNumberStore<TUser, TKey> store = this.GetPhoneNumberStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result;
			if (await this.VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).WithCurrentCulture<bool>())
			{
				await store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
				await store.SetPhoneNumberConfirmedAsync(user, true).WithCurrentCulture();
				await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			else
			{
				result = IdentityResult.Failed(new string[]
				{
					Resources.InvalidToken
				});
			}
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000D194 File Offset: 0x0000B394
		public virtual async Task<bool> IsPhoneNumberConfirmedAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserPhoneNumberStore<TUser, TKey> store = this.GetPhoneNumberStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetPhoneNumberConfirmedAsync(user).WithCurrentCulture<bool>();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		internal async Task<SecurityToken> CreateSecurityTokenAsync(TKey userId)
		{
			return new SecurityToken(Encoding.Unicode.GetBytes(await this.GetSecurityStampAsync(userId).WithCurrentCulture<string>()));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000D45C File Offset: 0x0000B65C
		public virtual async Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber)
		{
			this.ThrowIfDisposed();
			return Rfc6238AuthenticationService.GenerateCode(await this.CreateSecurityTokenAsync(userId).WithCurrentCulture<SecurityToken>(), phoneNumber).ToString("D6", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public virtual async Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber)
		{
			this.ThrowIfDisposed();
			SecurityToken securityToken = await this.CreateSecurityTokenAsync(userId).WithCurrentCulture<SecurityToken>();
			int code;
			bool result;
			if (securityToken != null && int.TryParse(token, out code))
			{
				result = Rfc6238AuthenticationService.ValidateCode(securityToken, code, phoneNumber);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000D838 File Offset: 0x0000BA38
		public virtual async Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token)
		{
			this.ThrowIfDisposed();
			if (this.UserTokenProvider == null)
			{
				throw new NotSupportedException(Resources.NoTokenProvider);
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await this.UserTokenProvider.ValidateAsync(purpose, token, this, user).WithCurrentCulture<bool>();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000DA84 File Offset: 0x0000BC84
		public virtual async Task<string> GenerateUserTokenAsync(string purpose, TKey userId)
		{
			this.ThrowIfDisposed();
			if (this.UserTokenProvider == null)
			{
				throw new NotSupportedException(Resources.NoTokenProvider);
			}
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await this.UserTokenProvider.GenerateAsync(purpose, this, user).WithCurrentCulture<string>();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000DADA File Offset: 0x0000BCDA
		public virtual void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TKey> provider)
		{
			this.ThrowIfDisposed();
			if (twoFactorProvider == null)
			{
				throw new ArgumentNullException("twoFactorProvider");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this.TwoFactorProviders[twoFactorProvider] = provider;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public virtual async Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			List<string> results = new List<string>();
			foreach (KeyValuePair<string, IUserTokenProvider<TUser, TKey>> f in this.TwoFactorProviders)
			{
				KeyValuePair<string, IUserTokenProvider<TUser, TKey>> keyValuePair = f;
				if (await keyValuePair.Value.IsValidProviderForUserAsync(this, user).WithCurrentCulture<bool>())
				{
					List<string> list = results;
					KeyValuePair<string, IUserTokenProvider<TUser, TKey>> keyValuePair2 = f;
					list.Add(keyValuePair2.Key);
				}
			}
			return results;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000E00C File Offset: 0x0000C20C
		public virtual async Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
		{
			this.ThrowIfDisposed();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			if (!this._factors.ContainsKey(twoFactorProvider))
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, new object[]
				{
					twoFactorProvider
				}));
			}
			IUserTokenProvider<TUser, TKey> provider = this._factors[twoFactorProvider];
			return await provider.ValidateAsync(twoFactorProvider, token, this, user).WithCurrentCulture<bool>();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000E28C File Offset: 0x0000C48C
		public virtual async Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider)
		{
			this.ThrowIfDisposed();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			if (!this._factors.ContainsKey(twoFactorProvider))
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, new object[]
				{
					twoFactorProvider
				}));
			}
			return await this._factors[twoFactorProvider].GenerateAsync(twoFactorProvider, this, user).WithCurrentCulture<string>();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000E50C File Offset: 0x0000C70C
		public virtual async Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
		{
			this.ThrowIfDisposed();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			if (!this._factors.ContainsKey(twoFactorProvider))
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, new object[]
				{
					twoFactorProvider
				}));
			}
			await this._factors[twoFactorProvider].NotifyAsync(token, this, user).WithCurrentCulture();
			return IdentityResult.Success;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000E56C File Offset: 0x0000C76C
		internal IUserTwoFactorStore<TUser, TKey> GetUserTwoFactorStore()
		{
			IUserTwoFactorStore<TUser, TKey> userTwoFactorStore = this.Store as IUserTwoFactorStore<TUser, TKey>;
			if (userTwoFactorStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserTwoFactorStore);
			}
			return userTwoFactorStore;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000E768 File Offset: 0x0000C968
		public virtual async Task<bool> GetTwoFactorEnabledAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserTwoFactorStore<TUser, TKey> store = this.GetUserTwoFactorStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetTwoFactorEnabledAsync(user).WithCurrentCulture<bool>();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000EA94 File Offset: 0x0000CC94
		public virtual async Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled)
		{
			this.ThrowIfDisposed();
			IUserTwoFactorStore<TUser, TKey> store = this.GetUserTwoFactorStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await store.SetTwoFactorEnabledAsync(user, enabled).WithCurrentCulture();
			await this.UpdateSecurityStampInternal(user).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		public virtual async Task SendEmailAsync(TKey userId, string subject, string body)
		{
			this.ThrowIfDisposed();
			if (this.EmailService != null)
			{
				IdentityMessage msg = new IdentityMessage
				{
					Destination = await this.GetEmailAsync(userId).WithCurrentCulture<string>(),
					Subject = subject,
					Body = body
				};
				await this.EmailService.SendAsync(msg).WithCurrentCulture();
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public virtual async Task SendSmsAsync(TKey userId, string message)
		{
			this.ThrowIfDisposed();
			if (this.SmsService != null)
			{
				IdentityMessage msg = new IdentityMessage
				{
					Destination = await this.GetPhoneNumberAsync(userId).WithCurrentCulture<string>(),
					Body = message
				};
				await this.SmsService.SendAsync(msg).WithCurrentCulture();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000EF84 File Offset: 0x0000D184
		internal IUserLockoutStore<TUser, TKey> GetUserLockoutStore()
		{
			IUserLockoutStore<TUser, TKey> userLockoutStore = this.Store as IUserLockoutStore<TUser, TKey>;
			if (userLockoutStore == null)
			{
				throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
			}
			return userLockoutStore;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F224 File Offset: 0x0000D424
		public virtual async Task<bool> IsLockedOutAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			bool result;
			if (!(await store.GetLockoutEnabledAsync(user).WithCurrentCulture<bool>()))
			{
				result = false;
			}
			else
			{
				DateTimeOffset lockoutTime = await store.GetLockoutEndDateAsync(user).WithCurrentCulture<DateTimeOffset>();
				result = (lockoutTime >= DateTimeOffset.UtcNow);
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public virtual async Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			await store.SetLockoutEnabledAsync(user, enabled).WithCurrentCulture();
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public virtual async Task<bool> GetLockoutEnabledAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetLockoutEnabledAsync(user).WithCurrentCulture<bool>();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F920 File Offset: 0x0000DB20
		public virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetLockoutEndDateAsync(user).WithCurrentCulture<DateTimeOffset>();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		public virtual async Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result;
			if (!(await store.GetLockoutEnabledAsync(user).WithCurrentCulture<bool>()))
			{
				result = IdentityResult.Failed(new string[]
				{
					Resources.LockoutNotEnabled
				});
			}
			else
			{
				await store.SetLockoutEndDateAsync(user, lockoutEnd).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00010054 File Offset: 0x0000E254
		public virtual async Task<IdentityResult> AccessFailedAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			int count = await store.IncrementAccessFailedCountAsync(user).WithCurrentCulture<int>();
			if (count >= this.MaxFailedAccessAttemptsBeforeLockout)
			{
				await store.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(this.DefaultAccountLockoutTimeSpan)).WithCurrentCulture();
				await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
			}
			return await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00010390 File Offset: 0x0000E590
		public virtual async Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			IdentityResult result;
			if (!(await this.GetAccessFailedCountAsync(user.Id).WithCurrentCulture<int>()))
			{
				result = IdentityResult.Success;
			}
			else
			{
				await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
				result = await this.UpdateAsync(user).WithCurrentCulture<IdentityResult>();
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000105B4 File Offset: 0x0000E7B4
		public virtual async Task<int> GetAccessFailedCountAsync(TKey userId)
		{
			this.ThrowIfDisposed();
			IUserLockoutStore<TUser, TKey> store = this.GetUserLockoutStore();
			TUser user = await this.FindByIdAsync(userId).WithCurrentCulture<TUser>();
			if (user == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[]
				{
					userId
				}));
			}
			return await store.GetAccessFailedCountAsync(user).WithCurrentCulture<int>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00010602 File Offset: 0x0000E802
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0001061D File Offset: 0x0000E81D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				this.Store.Dispose();
				this._disposed = true;
			}
		}

		// Token: 0x04000032 RID: 50
		private readonly Dictionary<string, IUserTokenProvider<TUser, TKey>> _factors = new Dictionary<string, IUserTokenProvider<TUser, TKey>>();

		// Token: 0x04000033 RID: 51
		private IClaimsIdentityFactory<TUser, TKey> _claimsFactory;

		// Token: 0x04000034 RID: 52
		private TimeSpan _defaultLockout = TimeSpan.Zero;

		// Token: 0x04000035 RID: 53
		private bool _disposed;

		// Token: 0x04000036 RID: 54
		private IPasswordHasher _passwordHasher;

		// Token: 0x04000037 RID: 55
		private IIdentityValidator<string> _passwordValidator;

		// Token: 0x04000038 RID: 56
		private IIdentityValidator<TUser> _userValidator;
	}
}
