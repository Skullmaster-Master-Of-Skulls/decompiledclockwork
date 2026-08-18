using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200002A RID: 42
	public class RoleManager<TRole, TKey> : IDisposable where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003C2B File Offset: 0x00001E2B
		public RoleManager(IRoleStore<TRole, TKey> store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			this.Store = store;
			this.RoleValidator = new RoleValidator<TRole, TKey>(this);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003C54 File Offset: 0x00001E54
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003C5C File Offset: 0x00001E5C
		private protected IRoleStore<TRole, TKey> Store { protected get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003C65 File Offset: 0x00001E65
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003C6D File Offset: 0x00001E6D
		public IIdentityValidator<TRole> RoleValidator
		{
			get
			{
				return this._roleValidator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._roleValidator = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003C84 File Offset: 0x00001E84
		public virtual IQueryable<TRole> Roles
		{
			get
			{
				IQueryableRoleStore<TRole, TKey> queryableRoleStore = this.Store as IQueryableRoleStore<TRole, TKey>;
				if (queryableRoleStore == null)
				{
					throw new NotSupportedException(Resources.StoreNotIQueryableRoleStore);
				}
				return queryableRoleStore.Roles;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003CB1 File Offset: 0x00001EB1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003E88 File Offset: 0x00002088
		public virtual async Task<IdentityResult> CreateAsync(TRole role)
		{
			this.ThrowIfDisposed();
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			IdentityResult result = await this.RoleValidator.ValidateAsync(role).WithCurrentCulture<IdentityResult>();
			IdentityResult result2;
			if (!result.Succeeded)
			{
				result2 = result;
			}
			else
			{
				await this.Store.CreateAsync(role).WithCurrentCulture();
				result2 = IdentityResult.Success;
			}
			return result2;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000040A0 File Offset: 0x000022A0
		public virtual async Task<IdentityResult> UpdateAsync(TRole role)
		{
			this.ThrowIfDisposed();
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			IdentityResult result = await this.RoleValidator.ValidateAsync(role).WithCurrentCulture<IdentityResult>();
			IdentityResult result2;
			if (!result.Succeeded)
			{
				result2 = result;
			}
			else
			{
				await this.Store.UpdateAsync(role).WithCurrentCulture();
				result2 = IdentityResult.Success;
			}
			return result2;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004200 File Offset: 0x00002400
		public virtual async Task<IdentityResult> DeleteAsync(TRole role)
		{
			this.ThrowIfDisposed();
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			await this.Store.DeleteAsync(role).WithCurrentCulture();
			return IdentityResult.Success;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000435C File Offset: 0x0000255C
		public virtual async Task<bool> RoleExistsAsync(string roleName)
		{
			this.ThrowIfDisposed();
			if (roleName == null)
			{
				throw new ArgumentNullException("roleName");
			}
			return await this.FindByNameAsync(roleName).WithCurrentCulture<TRole>() != null;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000044A0 File Offset: 0x000026A0
		public virtual async Task<TRole> FindByIdAsync(TKey roleId)
		{
			this.ThrowIfDisposed();
			return await this.Store.FindByIdAsync(roleId).WithCurrentCulture<TRole>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000045F4 File Offset: 0x000027F4
		public virtual async Task<TRole> FindByNameAsync(string roleName)
		{
			this.ThrowIfDisposed();
			if (roleName == null)
			{
				throw new ArgumentNullException("roleName");
			}
			return await this.Store.FindByNameAsync(roleName).WithCurrentCulture<TRole>();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004642 File Offset: 0x00002842
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000465D File Offset: 0x0000285D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				this.Store.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x04000018 RID: 24
		private bool _disposed;

		// Token: 0x04000019 RID: 25
		private IIdentityValidator<TRole> _roleValidator;
	}
}
