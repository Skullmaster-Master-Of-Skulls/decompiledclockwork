using System;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200001A RID: 26
	public static class RoleManagerExtensions
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002520 File Offset: 0x00000720
		public static TRole FindById<TRole, TKey>(this RoleManager<TRole, TKey> manager, TKey roleId) where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TRole>(() => manager.FindByIdAsync(roleId));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002580 File Offset: 0x00000780
		public static TRole FindByName<TRole, TKey>(this RoleManager<TRole, TKey> manager, string roleName) where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TRole>(() => manager.FindByNameAsync(roleName));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000025E0 File Offset: 0x000007E0
		public static IdentityResult Create<TRole, TKey>(this RoleManager<TRole, TKey> manager, TRole role) where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.CreateAsync(role));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002640 File Offset: 0x00000840
		public static IdentityResult Update<TRole, TKey>(this RoleManager<TRole, TKey> manager, TRole role) where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.UpdateAsync(role));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026A0 File Offset: 0x000008A0
		public static IdentityResult Delete<TRole, TKey>(this RoleManager<TRole, TKey> manager, TRole role) where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.DeleteAsync(role));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002700 File Offset: 0x00000900
		public static bool RoleExists<TRole, TKey>(this RoleManager<TRole, TKey> manager, string roleName) where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.RoleExistsAsync(roleName));
		}
	}
}
