using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000627 RID: 1575
	internal sealed class ReadOnlyHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x06004E14 RID: 19988 RVA: 0x0013C75C File Offset: 0x0013B75C
		public ReadOnlyHierarchicalDataSourceView(IHierarchicalEnumerable dataSource)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0013C76B File Offset: 0x0013B76B
		public override IHierarchicalEnumerable Select()
		{
			return this._dataSource;
		}

		// Token: 0x04002C7B RID: 11387
		private IHierarchicalEnumerable _dataSource;
	}
}
