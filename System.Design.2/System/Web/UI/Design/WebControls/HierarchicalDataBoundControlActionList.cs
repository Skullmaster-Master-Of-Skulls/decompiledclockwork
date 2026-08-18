using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000CF RID: 207
	internal class HierarchicalDataBoundControlActionList : DesignerActionList
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x00027461 File Offset: 0x00025661
		public HierarchicalDataBoundControlActionList(HierarchicalDataBoundControlDesigner controlDesigner, IHierarchicalDataSourceDesigner dataSourceDesigner) : base(controlDesigner.Component)
		{
			this._controlDesigner = controlDesigner;
			this._dataSourceDesigner = dataSourceDesigner;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00003937 File Offset: 0x00001B37
		public override bool AutoShow
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00027480 File Offset: 0x00025680
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x000274AD File Offset: 0x000256AD
		[TypeConverter(typeof(HierarchicalDataSourceIDConverter))]
		public string DataSourceID
		{
			get
			{
				string dataSourceID = this._controlDesigner.DataSourceID;
				if (string.IsNullOrEmpty(dataSourceID))
				{
					return SR.GetString("DataSourceIDChromeConverter_NoDataSource");
				}
				return dataSourceID;
			}
			set
			{
				this._controlDesigner.DataSourceID = value;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000274BC File Offset: 0x000256BC
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._controlDesigner.Component)["DataSourceID"];
			if (propertyDescriptor != null && propertyDescriptor.IsBrowsable)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("DataSourceID", SR.GetString("BaseDataBoundControl_ConfigureDataVerb"), SR.GetString("BaseDataBoundControl_DataActionGroup"), SR.GetString("BaseDataBoundControl_ConfigureDataVerbDesc")));
			}
			ControlDesigner controlDesigner = this._dataSourceDesigner as ControlDesigner;
			if (controlDesigner != null)
			{
				((DesignerActionPropertyItem)designerActionItemCollection[0]).RelatedComponent = controlDesigner.Component;
			}
			return designerActionItemCollection;
		}

		// Token: 0x04000457 RID: 1111
		private IHierarchicalDataSourceDesigner _dataSourceDesigner;

		// Token: 0x04000458 RID: 1112
		private HierarchicalDataBoundControlDesigner _controlDesigner;
	}
}
