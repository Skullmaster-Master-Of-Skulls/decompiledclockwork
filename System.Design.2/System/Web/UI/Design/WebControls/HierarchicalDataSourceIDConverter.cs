using System;
using System.ComponentModel;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D1 RID: 209
	public class HierarchicalDataSourceIDConverter : DataSourceIDConverter
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x00027958 File Offset: 0x00025B58
		protected override bool IsValidDataSource(IComponent component)
		{
			return component is IHierarchicalDataSource;
		}
	}
}
