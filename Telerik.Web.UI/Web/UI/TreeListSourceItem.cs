using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200128B RID: 4747
	public class TreeListSourceItem
	{
		// Token: 0x17003FF4 RID: 16372
		// (get) Token: 0x0600C611 RID: 50705 RVA: 0x002C3804 File Offset: 0x002C1A04
		// (set) Token: 0x0600C612 RID: 50706 RVA: 0x002C380C File Offset: 0x002C1A0C
		public int ItemIndex { get; set; }

		// Token: 0x17003FF5 RID: 16373
		// (get) Token: 0x0600C613 RID: 50707 RVA: 0x002C3815 File Offset: 0x002C1A15
		// (set) Token: 0x0600C614 RID: 50708 RVA: 0x002C381D File Offset: 0x002C1A1D
		public TreeListHierarchyIndex HierarchyIndex { get; set; }

		// Token: 0x17003FF6 RID: 16374
		// (get) Token: 0x0600C615 RID: 50709 RVA: 0x002C3826 File Offset: 0x002C1A26
		// (set) Token: 0x0600C616 RID: 50710 RVA: 0x002C382E File Offset: 0x002C1A2E
		public object OriginalDataItem { get; set; }

		// Token: 0x17003FF7 RID: 16375
		// (get) Token: 0x0600C617 RID: 50711 RVA: 0x002C3837 File Offset: 0x002C1A37
		// (set) Token: 0x0600C618 RID: 50712 RVA: 0x002C383F File Offset: 0x002C1A3F
		public int SiblingsCount { get; set; }

		// Token: 0x17003FF8 RID: 16376
		// (get) Token: 0x0600C619 RID: 50713 RVA: 0x002C3848 File Offset: 0x002C1A48
		// (set) Token: 0x0600C61A RID: 50714 RVA: 0x002C3850 File Offset: 0x002C1A50
		public TreeListSourceItem ParentItem { get; set; }

		// Token: 0x17003FF9 RID: 16377
		// (get) Token: 0x0600C61B RID: 50715 RVA: 0x002C3859 File Offset: 0x002C1A59
		// (set) Token: 0x0600C61C RID: 50716 RVA: 0x002C3874 File Offset: 0x002C1A74
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public IList<TreeListSourceItem> ChildItems
		{
			get
			{
				if (this._childItems == null)
				{
					this._childItems = new List<TreeListSourceItem>();
				}
				return this._childItems;
			}
			set
			{
				this._childItems = value;
			}
		}

		// Token: 0x17003FFA RID: 16378
		// (get) Token: 0x0600C61D RID: 50717 RVA: 0x002C387D File Offset: 0x002C1A7D
		public int ChildItemsCount
		{
			get
			{
				return this.ChildItems.Count;
			}
		}

		// Token: 0x17003FFB RID: 16379
		// (get) Token: 0x0600C61E RID: 50718 RVA: 0x002C388A File Offset: 0x002C1A8A
		// (set) Token: 0x0600C61F RID: 50719 RVA: 0x002C38A5 File Offset: 0x002C1AA5
		public TreeListItemState ItemState
		{
			get
			{
				if (this._itemState == null)
				{
					this._itemState = new TreeListItemState();
				}
				return this._itemState;
			}
			internal set
			{
				this._itemState = value;
			}
		}

		// Token: 0x0600C620 RID: 50720 RVA: 0x002C38AE File Offset: 0x002C1AAE
		public bool IsCalculatedColumn(string propertyName)
		{
			return this.CalculatedColumns.ContainsKey(propertyName);
		}

		// Token: 0x17003FFC RID: 16380
		// (get) Token: 0x0600C621 RID: 50721 RVA: 0x002C38BC File Offset: 0x002C1ABC
		// (set) Token: 0x0600C622 RID: 50722 RVA: 0x002C38D7 File Offset: 0x002C1AD7
		public Dictionary<string, object> CalculatedColumns
		{
			get
			{
				if (this._calculatedColumns == null)
				{
					this._calculatedColumns = new Dictionary<string, object>();
				}
				return this._calculatedColumns;
			}
			set
			{
				this._calculatedColumns = value;
			}
		}

		// Token: 0x0400345B RID: 13403
		private IList<TreeListSourceItem> _childItems;

		// Token: 0x0400345C RID: 13404
		private TreeListItemState _itemState;

		// Token: 0x0400345D RID: 13405
		private Dictionary<string, object> _calculatedColumns;
	}
}
