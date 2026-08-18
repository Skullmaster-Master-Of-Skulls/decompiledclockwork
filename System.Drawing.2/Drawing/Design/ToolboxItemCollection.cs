using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x02000080 RID: 128
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ToolboxItemCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00020D06 File Offset: 0x0001EF06
		public ToolboxItemCollection(ToolboxItemCollection value)
		{
			base.InnerList.AddRange(value);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00020D06 File Offset: 0x0001EF06
		public ToolboxItemCollection(ToolboxItem[] value)
		{
			base.InnerList.AddRange(value);
		}

		// Token: 0x17000334 RID: 820
		public ToolboxItem this[int index]
		{
			get
			{
				return (ToolboxItem)base.InnerList[index];
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00020D2D File Offset: 0x0001EF2D
		public bool Contains(ToolboxItem value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00020D3B File Offset: 0x0001EF3B
		public void CopyTo(ToolboxItem[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00020D4A File Offset: 0x0001EF4A
		public int IndexOf(ToolboxItem value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
