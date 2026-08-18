using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200009D RID: 157
	internal class BaseDataListActionList : DataBoundControlActionList
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00015EFD File Offset: 0x000140FD
		public BaseDataListActionList(ControlDesigner controlDesigner, IDataSourceDesigner dataSourceDesigner) : base(controlDesigner, dataSourceDesigner)
		{
			this._controlDesigner = controlDesigner;
			this._dataSourceDesigner = dataSourceDesigner;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00015F15 File Offset: 0x00014115
		public void InvokePropertyBuilder()
		{
			((BaseDataListDesigner)this._controlDesigner).InvokePropertyBuilder(0);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00015F28 File Offset: 0x00014128
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = base.GetSortedActionItems();
			if (designerActionItemCollection == null)
			{
				designerActionItemCollection = new DesignerActionItemCollection();
			}
			designerActionItemCollection.Add(new DesignerActionMethodItem(this, "InvokePropertyBuilder", SR.GetString("BDL_PropertyBuilderVerb"), SR.GetString("BDL_BehaviorGroup"), SR.GetString("BDL_PropertyBuilderDesc")));
			return designerActionItemCollection;
		}

		// Token: 0x04000213 RID: 531
		private IDataSourceDesigner _dataSourceDesigner;

		// Token: 0x04000214 RID: 532
		private ControlDesigner _controlDesigner;
	}
}
