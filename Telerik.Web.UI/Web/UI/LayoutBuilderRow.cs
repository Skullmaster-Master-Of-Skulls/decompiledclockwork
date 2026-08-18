using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001841 RID: 6209
	public class LayoutBuilderRow : StateManager
	{
		// Token: 0x170048E4 RID: 18660
		// (get) Token: 0x0600F143 RID: 61763 RVA: 0x0036D72F File Offset: 0x0036B92F
		// (set) Token: 0x0600F144 RID: 61764 RVA: 0x0036D74A File Offset: 0x0036B94A
		public LayoutBuilderCellCollection LayoutBuilderCells
		{
			get
			{
				if (this._layoutBuilderCells == null)
				{
					this._layoutBuilderCells = new LayoutBuilderCellCollection();
				}
				return this._layoutBuilderCells;
			}
			set
			{
				this._layoutBuilderCells = value;
			}
		}

		// Token: 0x04004568 RID: 17768
		private LayoutBuilderCellCollection _layoutBuilderCells;
	}
}
