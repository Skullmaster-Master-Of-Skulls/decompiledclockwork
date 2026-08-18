using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x02000002 RID: 2
	public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>, IRoleStore<TRole, TKey>, IDisposable where TRole : IdentityRole<TKey, TUserRole>, new() where TUserRole : IdentityUserRole<TKey>, new()
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public RoleStore(DbContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.Context = context;
			this._roleStore = new EntityStore<TRole>(context);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002101 File Offset: 0x00000301
		public DbContext Context { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002112 File Offset: 0x00000312
		public bool DisposeContext { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		public Task<TRole> FindByIdAsync(TKey roleId)
		{
			this.ThrowIfDisposed();
			return this._roleStore.GetByIdAsync(roleId);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000213C File Offset: 0x0000033C
		public Task<TRole> FindByNameAsync(string roleName)
		{
			this.ThrowIfDisposed();
			return this._roleStore.EntitySet.FirstOrDefaultAsync((TRole u) => u.Name.ToUpper() == roleName.ToUpper());
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000232C File Offset: 0x0000052C
		public virtual async Task CreateAsync(TRole role)
		{
			this.ThrowIfDisposed();
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			this._roleStore.Create(role);
			await this.Context.SaveChangesAsync().WithCurrentCulture<int>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002498 File Offset: 0x00000698
		public virtual async Task DeleteAsync(TRole role)
		{
			this.ThrowIfDisposed();
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			this._roleStore.Delete(role);
			await this.Context.SaveChangesAsync().WithCurrentCulture<int>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002604 File Offset: 0x00000804
		public virtual async Task UpdateAsync(TRole role)
		{
			this.ThrowIfDisposed();
			if (role == null)
			{
				throw new ArgumentNullException("role");
			}
			this._roleStore.Update(role);
			await this.Context.SaveChangesAsync().WithCurrentCulture<int>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002652 File Offset: 0x00000852
		public IQueryable<TRole> Roles
		{
			get
			{
				return this._roleStore.EntitySet;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000265F File Offset: 0x0000085F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000266E File Offset: 0x0000086E
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002689 File Offset: 0x00000889
		protected virtual void Dispose(bool disposing)
		{
			if (this.DisposeContext && disposing && this.Context != null)
			{
				this.Context.Dispose();
			}
			this._disposed = true;
			this.Context = null;
			this._roleStore = null;
		}

		// Token: 0x04000001 RID: 1
		private bool _disposed;

		// Token: 0x04000002 RID: 2
		private EntityStore<TRole> _roleStore;
	}
}
