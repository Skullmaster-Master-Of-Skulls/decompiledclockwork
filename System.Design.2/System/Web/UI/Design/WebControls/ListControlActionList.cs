using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D6 RID: 214
	internal class ListControlActionList : DesignerActionList
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x00027AE3 File Offset: 0x00025CE3
		public ListControlActionList(ListControlDesigner listControlDesigner, IDataSourceDesigner dataSourceDesigner) : base(listControlDesigner.Component)
		{
			this._listControlDesigner = listControlDesigner;
			this._dataSourceDesigner = dataSourceDesigner;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00027AFF File Offset: 0x00025CFF
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00027B18 File Offset: 0x00025D18
		public bool AutoPostBack
		{
			get
			{
				return ((ListControl)this._listControlDesigner.Component).AutoPostBack;
			}
			set
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._listControlDesigner.Component)["AutoPostBack"];
				propertyDescriptor.SetValue(this._listControlDesigner.Component, value);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00003937 File Offset: 0x00001B37
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

		// Token: 0x0600073D RID: 1853 RVA: 0x00027B57 File Offset: 0x00025D57
		public void EditItems()
		{
			this._listControlDesigner.EditItems();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00027B64 File Offset: 0x00025D64
		public void ConnectToDataSource()
		{
			this._listControlDesigner.ConnectToDataSourceAction();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00027B74 File Offset: 0x00025D74
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._listControlDesigner.Component);
			PropertyDescriptor propertyDescriptor = properties["DataSourceID"];
			if (propertyDescriptor != null && propertyDescriptor.IsBrowsable)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "ConnectToDataSource", SR.GetString("ListControl_ConfigureDataVerb"), SR.GetString("BaseDataBoundControl_DataActionGroup"), SR.GetString("BaseDataBoundControl_ConfigureDataVerbDesc")));
			}
			ControlDesigner controlDesigner = this._dataSourceDesigner as ControlDesigner;
			if (controlDesigner != null)
			{
				((DesignerActionMethodItem)designerActionItemCollection[0]).RelatedComponent = controlDesigner.Component;
			}
			propertyDescriptor = properties["Items"];
			if (propertyDescriptor != null && propertyDescriptor.IsBrowsable)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditItems", SR.GetString("ListControl_EditItems"), "Actions", SR.GetString("ListControl_EditItemsDesc")));
			}
			propertyDescriptor = properties["AutoPostBack"];
			if (propertyDescriptor != null && propertyDescriptor.IsBrowsable)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("AutoPostBack", SR.GetString("ListControl_EnableAutoPostBack"), "Behavior", SR.GetString("ListControl_EnableAutoPostBackDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x0400045A RID: 1114
		private IDataSourceDesigner _dataSourceDesigner;

		// Token: 0x0400045B RID: 1115
		private ListControlDesigner _listControlDesigner;
	}
}
