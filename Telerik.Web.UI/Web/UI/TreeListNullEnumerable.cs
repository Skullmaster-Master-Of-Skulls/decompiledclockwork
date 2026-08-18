using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200128A RID: 4746
	public class TreeListNullEnumerable : TreeListEnumerableBase
	{
		// Token: 0x0600C609 RID: 50697 RVA: 0x002C37A6 File Offset: 0x002C19A6
		public TreeListNullEnumerable() : base(null, null)
		{
		}

		// Token: 0x0600C60A RID: 50698 RVA: 0x002C37B0 File Offset: 0x002C19B0
		protected override void TransformEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C60B RID: 50699 RVA: 0x002C37BC File Offset: 0x002C19BC
		public override IEnumerable<TreeListSourceItem> RawEnumerable()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x17003FF3 RID: 16371
		// (get) Token: 0x0600C60C RID: 50700 RVA: 0x002C37C8 File Offset: 0x002C19C8
		public override int DataSourceCount
		{
			get
			{
				throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
			}
		}

		// Token: 0x0600C60D RID: 50701 RVA: 0x002C37D4 File Offset: 0x002C19D4
		public override List<TreeListHierarchyIndex> GetExpandedItems()
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C60E RID: 50702 RVA: 0x002C37E0 File Offset: 0x002C19E0
		internal override IEnumerable<TreeListSourceItem> GetItemsToDelete(TreeListDeleteContext context)
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C60F RID: 50703 RVA: 0x002C37EC File Offset: 0x002C19EC
		internal override IEnumerable<TreeListSourceItem> GetItemsToReorder(TreeListReorderContext context)
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}

		// Token: 0x0600C610 RID: 50704 RVA: 0x002C37F8 File Offset: 0x002C19F8
		internal override void AdjustReorderedIndexes(TreeListReorderContext context)
		{
			throw new InvalidOperationException("Cannot perform this operation when DataSource is not assigned");
		}
	}
}
