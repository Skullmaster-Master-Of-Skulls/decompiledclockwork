using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x02000072 RID: 114
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class CategoryNameCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x00020D06 File Offset: 0x0001EF06
		public CategoryNameCollection(CategoryNameCollection value)
		{
			base.InnerList.AddRange(value);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00020D06 File Offset: 0x0001EF06
		public CategoryNameCollection(string[] value)
		{
			base.InnerList.AddRange(value);
		}

		// Token: 0x17000319 RID: 793
		public string this[int index]
		{
			get
			{
				return (string)base.InnerList[index];
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00020D2D File Offset: 0x0001EF2D
		public bool Contains(string value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00020D3B File Offset: 0x0001EF3B
		public void CopyTo(string[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00020D4A File Offset: 0x0001EF4A
		public int IndexOf(string value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
