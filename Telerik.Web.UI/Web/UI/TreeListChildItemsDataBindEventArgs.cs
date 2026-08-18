using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F1A RID: 3866
	public class TreeListChildItemsDataBindEventArgs : EventArgs
	{
		// Token: 0x060093AA RID: 37802 RVA: 0x00212914 File Offset: 0x00210B14
		public TreeListChildItemsDataBindEventArgs(TreeListHierarchyIndex parentHierarchyIndex, Hashtable dataKeyValues)
		{
			this.dataKeyValues = dataKeyValues;
			this.parentHierarchyIndex = parentHierarchyIndex;
		}

		// Token: 0x17002EB2 RID: 11954
		// (get) Token: 0x060093AB RID: 37803 RVA: 0x0021292A File Offset: 0x00210B2A
		public TreeListHierarchyIndex ParentHierarchyIndex
		{
			get
			{
				return this.parentHierarchyIndex;
			}
		}

		// Token: 0x17002EB3 RID: 11955
		// (get) Token: 0x060093AC RID: 37804 RVA: 0x00212932 File Offset: 0x00210B32
		public Hashtable ParentDataKeyValues
		{
			get
			{
				return this.dataKeyValues;
			}
		}

		// Token: 0x17002EB4 RID: 11956
		// (get) Token: 0x060093AD RID: 37805 RVA: 0x0021293A File Offset: 0x00210B3A
		// (set) Token: 0x060093AE RID: 37806 RVA: 0x00212942 File Offset: 0x00210B42
		public object ChildItemsDataSource
		{
			get
			{
				return this.childItemsDataSource;
			}
			set
			{
				this.childItemsDataSource = value;
			}
		}

		// Token: 0x04002A57 RID: 10839
		private TreeListHierarchyIndex parentHierarchyIndex;

		// Token: 0x04002A58 RID: 10840
		private Hashtable dataKeyValues;

		// Token: 0x04002A59 RID: 10841
		private object childItemsDataSource;
	}
}
