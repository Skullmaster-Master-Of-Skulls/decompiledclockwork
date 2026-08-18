using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004BD RID: 1213
	[Editor("System.Web.UI.Design.WebControls.RoleGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class RoleGroupCollection : CollectionBase
	{
		// Token: 0x170011B2 RID: 4530
		public RoleGroup this[int index]
		{
			get
			{
				return (RoleGroup)base.List[index];
			}
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x000C4466 File Offset: 0x000C2666
		public void Add(RoleGroup group)
		{
			base.List.Add(group);
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(RoleGroup[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(RoleGroup group)
		{
			return base.List.Contains(group);
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000C4478 File Offset: 0x000C2678
		public RoleGroup GetMatchingRoleGroup(IPrincipal user)
		{
			int matchingRoleGroupInternal = this.GetMatchingRoleGroupInternal(user);
			if (matchingRoleGroupInternal != -1)
			{
				return this[matchingRoleGroupInternal];
			}
			return null;
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x000C449C File Offset: 0x000C269C
		internal int GetMatchingRoleGroupInternal(IPrincipal user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			int num = 0;
			foreach (object obj in this)
			{
				RoleGroup roleGroup = (RoleGroup)obj;
				if (roleGroup.ContainsUser(user))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(RoleGroup group)
		{
			return base.List.IndexOf(group);
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, RoleGroup group)
		{
			base.List.Insert(index, group);
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x000C4510 File Offset: 0x000C2710
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (!(value is RoleGroup))
			{
				throw new ArgumentException(SR.GetString("RoleGroupCollection_InvalidType"), "value");
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x000C4538 File Offset: 0x000C2738
		public void Remove(RoleGroup group)
		{
			int num = this.IndexOf(group);
			if (num >= 0)
			{
				base.List.RemoveAt(num);
			}
		}
	}
}
