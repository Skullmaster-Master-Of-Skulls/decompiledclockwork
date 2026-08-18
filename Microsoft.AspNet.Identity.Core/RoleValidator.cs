using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000027 RID: 39
	public class RoleValidator<TRole, TKey> : IIdentityValidator<TRole> where TRole : class, IRole<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003852 File Offset: 0x00001A52
		public RoleValidator(RoleManager<TRole, TKey> manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			this.Manager = manager;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000386F File Offset: 0x00001A6F
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003877 File Offset: 0x00001A77
		private RoleManager<TRole, TKey> Manager { get; set; }

		// Token: 0x0600007F RID: 127 RVA: 0x000039B4 File Offset: 0x00001BB4
		public virtual async Task<IdentityResult> ValidateAsync(TRole item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			List<string> errors = new List<string>();
			await this.ValidateRoleName(item, errors).WithCurrentCulture();
			IdentityResult result;
			if (errors.Count > 0)
			{
				result = IdentityResult.Failed(errors.ToArray());
			}
			else
			{
				result = IdentityResult.Success;
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003BCC File Offset: 0x00001DCC
		private async Task ValidateRoleName(TRole role, List<string> errors)
		{
			if (string.IsNullOrWhiteSpace(role.Name))
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, new object[]
				{
					"Name"
				}));
			}
			else
			{
				TRole owner = await this.Manager.FindByNameAsync(role.Name).WithCurrentCulture<TRole>();
				if (owner != null && !EqualityComparer<TKey>.Default.Equals(owner.Id, role.Id))
				{
					errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, new object[]
					{
						role.Name
					}));
				}
			}
		}
	}
}
