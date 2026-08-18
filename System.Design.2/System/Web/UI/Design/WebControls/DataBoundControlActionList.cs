using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B2 RID: 178
	internal class DataBoundControlActionList : DesignerActionList
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		public DataBoundControlActionList(ControlDesigner controlDesigner, IDataSourceDesigner dataSourceDesigner) : base(controlDesigner.Component)
		{
			this._controlDesigner = controlDesigner;
			this._dataSourceDesigner = dataSourceDesigner;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00003937 File Offset: 0x00001B37
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

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0001C22D File Offset: 0x0001A42D
		[TypeConverter(typeof(DataSourceIDConverter))]
		public string DataSourceID
		{
			get
			{
				string text = null;
				DataBoundControlDesigner dataBoundControlDesigner = this._controlDesigner as DataBoundControlDesigner;
				if (dataBoundControlDesigner != null)
				{
					text = dataBoundControlDesigner.DataSourceID;
				}
				else
				{
					BaseDataListDesigner baseDataListDesigner = this._controlDesigner as BaseDataListDesigner;
					if (baseDataListDesigner != null)
					{
						text = baseDataListDesigner.DataSourceID;
					}
					else
					{
						RepeaterDesigner repeaterDesigner = this._controlDesigner as RepeaterDesigner;
						if (repeaterDesigner != null)
						{
							text = repeaterDesigner.DataSourceID;
						}
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					return SR.GetString("DataSourceIDChromeConverter_NoDataSource");
				}
				return text;
			}
			set
			{
				ControlDesigner.InvokeTransactedChange(this._controlDesigner.Component, new TransactedChangeCallback(this.SetDataSourceIDCallback), value, SR.GetString("DataBoundControlActionList_SetDataSourceIDTransaction"));
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001C258 File Offset: 0x0001A458
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

		// Token: 0x06000584 RID: 1412 RVA: 0x0001C2E8 File Offset: 0x0001A4E8
		private bool SetDataSourceIDCallback(object context)
		{
			string value = (string)context;
			DataBoundControlDesigner dataBoundControlDesigner = this._controlDesigner as DataBoundControlDesigner;
			if (dataBoundControlDesigner != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataBoundControlDesigner.Component)["DataSourceID"];
				propertyDescriptor.SetValue(dataBoundControlDesigner.Component, value);
			}
			else
			{
				BaseDataListDesigner baseDataListDesigner = this._controlDesigner as BaseDataListDesigner;
				if (baseDataListDesigner != null)
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(baseDataListDesigner.Component)["DataSourceID"];
					propertyDescriptor2.SetValue(baseDataListDesigner.Component, value);
				}
				else
				{
					RepeaterDesigner repeaterDesigner = this._controlDesigner as RepeaterDesigner;
					if (repeaterDesigner != null)
					{
						PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(repeaterDesigner.Component)["DataSourceID"];
						propertyDescriptor3.SetValue(repeaterDesigner.Component, value);
					}
				}
			}
			return true;
		}

		// Token: 0x040002EE RID: 750
		private IDataSourceDesigner _dataSourceDesigner;

		// Token: 0x040002EF RID: 751
		private ControlDesigner _controlDesigner;
	}
}
