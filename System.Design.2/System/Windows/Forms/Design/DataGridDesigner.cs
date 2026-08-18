using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B6 RID: 694
	internal class DataGridDesigner : ControlDesigner
	{
		// Token: 0x06001B79 RID: 7033 RVA: 0x000A33D4 File Offset: 0x000A15D4
		private DataGridDesigner()
		{
			this.designerVerbs = new DesignerVerbCollection();
			this.designerVerbs.Add(new DesignerVerb(SR.GetString("DataGridAutoFormatString"), new EventHandler(this.OnAutoFormat)));
			base.AutoResizeHandles = true;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000A3420 File Offset: 0x000A1620
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				this.changeNotificationService = (IComponentChangeService)designerHost.GetService(typeof(IComponentChangeService));
				if (this.changeNotificationService != null)
				{
					this.changeNotificationService.ComponentRemoved += this.DataSource_ComponentRemoved;
				}
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000A3488 File Offset: 0x000A1688
		private void DataSource_ComponentRemoved(object sender, ComponentEventArgs e)
		{
			DataGrid dataGrid = (DataGrid)base.Component;
			if (e.Component == dataGrid.DataSource)
			{
				dataGrid.DataSource = null;
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000A34B6 File Offset: 0x000A16B6
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.changeNotificationService != null)
			{
				this.changeNotificationService.ComponentRemoved -= this.DataSource_ComponentRemoved;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000A34E1 File Offset: 0x000A16E1
		public override DesignerVerbCollection Verbs
		{
			get
			{
				return this.designerVerbs;
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000A34EC File Offset: 0x000A16EC
		private void OnAutoFormat(object sender, EventArgs e)
		{
			object component = base.Component;
			DataGrid dgrid = component as DataGrid;
			DataGridAutoFormatDialog dataGridAutoFormatDialog = DpiHelper.CreateInstanceInSystemAwareContext<DataGridAutoFormatDialog>(() => new DataGridAutoFormatDialog(dgrid));
			if (dataGridAutoFormatDialog.ShowDialog() == DialogResult.OK)
			{
				DataRow selectedData = dataGridAutoFormatDialog.SelectedData;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("DataGridAutoFormatUndoTitle", new object[]
				{
					base.Component.Site.Name
				}));
				try
				{
					if (selectedData != null)
					{
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(DataGrid));
						foreach (object obj in selectedData.Table.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj;
							object obj2 = selectedData[dataColumn];
							PropertyDescriptor propertyDescriptor = properties[dataColumn.ColumnName];
							if (propertyDescriptor != null)
							{
								if (Convert.IsDBNull(obj2) || obj2.ToString().Length == 0)
								{
									propertyDescriptor.ResetValue(dgrid);
								}
								else
								{
									try
									{
										TypeConverter converter = propertyDescriptor.Converter;
										object value = converter.ConvertFromString(obj2.ToString());
										propertyDescriptor.SetValue(dgrid, value);
									}
									catch
									{
									}
								}
							}
						}
					}
				}
				finally
				{
					designerTransaction.Commit();
				}
				dgrid.Invalidate();
			}
		}

		// Token: 0x04001650 RID: 5712
		protected DesignerVerbCollection designerVerbs;

		// Token: 0x04001651 RID: 5713
		private IComponentChangeService changeNotificationService;
	}
}
