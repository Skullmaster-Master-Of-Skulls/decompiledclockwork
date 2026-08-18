using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000636 RID: 1590
	[Editor("System.Web.UI.Design.WebControls.RoleGroupCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleGroupCollection : CollectionBase
	{
		// Token: 0x170013E2 RID: 5090
		public RoleGroup this[int index]
		{
			get
			{
				return (RoleGroup)base.List[index];
			}
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0013DEDE File Offset: 0x0013CEDE
		public void Add(RoleGroup group)
		{
			base.List.Add(group);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0013DEED File Offset: 0x0013CEED
		public void CopyTo(RoleGroup[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0013DEFC File Offset: 0x0013CEFC
		public bool Contains(RoleGroup group)
		{
			return base.List.Contains(group);
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0013DF0C File Offset: 0x0013CF0C
		public RoleGroup GetMatchingRoleGroup(IPrincipal user)
		{
			int matchingRoleGroupInternal = this.GetMatchingRoleGroupInternal(user);
			if (matchingRoleGroupInternal != -1)
			{
				return this[matchingRoleGroupInternal];
			}
			return null;
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0013DF30 File Offset: 0x0013CF30
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

		// Token: 0x06004E9F RID: 20127 RVA: 0x0013DFA4 File Offset: 0x0013CFA4
		public int IndexOf(RoleGroup group)
		{
			return base.List.IndexOf(group);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0013DFB2 File Offset: 0x0013CFB2
		public void Insert(int index, RoleGroup group)
		{
			base.List.Insert(index, group);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0013DFC1 File Offset: 0x0013CFC1
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (!(value is RoleGroup))
			{
				throw new ArgumentException(SR.GetString("RoleGroupCollection_InvalidType"), "value");
			}
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0013DFE8 File Offset: 0x0013CFE8
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
