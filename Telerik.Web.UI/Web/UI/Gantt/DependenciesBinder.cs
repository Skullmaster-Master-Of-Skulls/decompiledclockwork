using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200049B RID: 1179
	[ToolboxItem(false)]
	public class DependenciesBinder : DataBoundControl
	{
		// Token: 0x060029E6 RID: 10726 RVA: 0x00086EF4 File Offset: 0x000850F4
		public new DataSourceView GetData()
		{
			return base.GetData();
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00086EFC File Offset: 0x000850FC
		public static IDependency BindDependency(object dataItem, IDependenciesDataBinding bindings)
		{
			return new Dependency
			{
				ID = (DataBinder.Eval(dataItem, bindings.IdField) ?? string.Empty),
				PredecessorID = (DataBinder.Eval(dataItem, bindings.PredecessorIdField) ?? string.Empty),
				SuccessorID = (DataBinder.Eval(dataItem, bindings.SuccessorIdField) ?? string.Empty),
				Type = (DependencyType)(DataBinder.Eval(dataItem, bindings.TypeField) ?? DependencyType.StartStart)
			};
		}
	}
}
