using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200009C RID: 156
	public abstract class BaseDataBoundControlDesigner : ControlDesigner
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000158F4 File Offset: 0x00013AF4
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00015924 File Offset: 0x00013B24
		public string DataSource
		{
			get
			{
				DataBinding dataBinding = base.DataBindings["DataSource"];
				if (dataBinding != null)
				{
					return dataBinding.Expression;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DataBindings.Remove("DataSource");
				}
				else
				{
					DataBinding dataBinding = base.DataBindings["DataSource"];
					if (dataBinding == null)
					{
						dataBinding = new DataBinding("DataSource", typeof(IEnumerable), value);
					}
					else
					{
						dataBinding.Expression = value;
					}
					base.DataBindings.Add(dataBinding);
				}
				this.OnDataSourceChanged(true);
				base.OnBindingsCollectionChangedInternal("DataSource");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001599F File Offset: 0x00013B9F
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x000159B4 File Offset: 0x00013BB4
		public string DataSourceID
		{
			get
			{
				return ((BaseDataBoundControl)base.Component).DataSourceID;
			}
			set
			{
				if (value == this.DataSourceID)
				{
					return;
				}
				if (value == SR.GetString("DataSourceIDChromeConverter_NewDataSource"))
				{
					this.CreateDataSource();
					TypeDescriptor.Refresh(base.Component);
					return;
				}
				if (value == SR.GetString("DataSourceIDChromeConverter_NoDataSource"))
				{
					value = string.Empty;
				}
				TypeDescriptor.Refresh(base.Component);
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof(BaseDataBoundControl))["DataSourceID"];
				propertyDescriptor.SetValue(base.Component, value);
				this.OnDataSourceChanged(false);
				this.OnSchemaRefreshed();
			}
		}

		// Token: 0x060004A9 RID: 1193
		protected abstract bool ConnectToDataSource();

		// Token: 0x060004AA RID: 1194
		protected abstract void CreateDataSource();

		// Token: 0x060004AB RID: 1195
		protected abstract void DataBind(BaseDataBoundControl dataBoundControl);

		// Token: 0x060004AC RID: 1196
		protected abstract void DisconnectFromDataSource();

		// Token: 0x060004AD RID: 1197 RVA: 0x00015A4C File Offset: 0x00013C4C
		protected override void Dispose(bool disposing)
		{
			if (disposing && base.Component != null && base.Component.Site != null)
			{
				this.DisconnectFromDataSource();
				if (base.RootDesigner != null)
				{
					base.RootDesigner.LoadComplete -= this.OnDesignerLoadComplete;
				}
				IComponentChangeService componentChangeService = (IComponentChangeService)base.Component.Site.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded -= this.OnComponentAdded;
					componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
					componentChangeService.ComponentChanged -= this.OnAnyComponentChanged;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015B14 File Offset: 0x00013D14
		public override string GetDesignTimeHtml()
		{
			string result = string.Empty;
			try
			{
				this.DataBind((BaseDataBoundControl)base.ViewControl);
				result = base.GetDesignTimeHtml();
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00004782 File Offset: 0x00002982
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(null);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015B60 File Offset: 0x00013D60
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRenderingShort") + "<br/>" + e.Message);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00015B84 File Offset: 0x00013D84
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(BaseDataBoundControl));
			base.Initialize(component);
			base.SetViewFlags(ViewFlags.DesignTimeHtmlRequiresLoadComplete, true);
			if (base.RootDesigner != null)
			{
				if (base.RootDesigner.IsLoading)
				{
					base.RootDesigner.LoadComplete += this.OnDesignerLoadComplete;
				}
				else
				{
					this.OnDesignerLoadComplete(null, EventArgs.Empty);
				}
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)component.Site.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoving += this.OnComponentRemoving;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				componentChangeService.ComponentChanged += this.OnAnyComponentChanged;
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015C50 File Offset: 0x00013E50
		private void OnAnyComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			Control control = ce.Component as Control;
			if (control != null && ce.Member != null && ce.Member.Name == "ID" && base.Component != null && ((string)ce.OldValue == this.DataSourceID || (string)ce.NewValue == this.DataSourceID))
			{
				this.OnDataSourceChanged(false);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015CCC File Offset: 0x00013ECC
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			Control control = e.Component as Control;
			if (control != null && control.ID == this.DataSourceID)
			{
				this.OnDataSourceChanged(false);
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00015D04 File Offset: 0x00013F04
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			Control control = e.Component as Control;
			if (control != null && base.Component != null && control.ID == this.DataSourceID)
			{
				this.DisconnectFromDataSource();
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00015D44 File Offset: 0x00013F44
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			Control control = e.Component as Control;
			if (control != null && base.Component != null && control.ID == this.DataSourceID)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && !designerHost.Loading)
				{
					this.OnDataSourceChanged(false);
				}
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00015DA4 File Offset: 0x00013FA4
		protected virtual void OnDataSourceChanged(bool forceUpdateView)
		{
			bool flag = this.ConnectToDataSource();
			if (flag || forceUpdateView)
			{
				this.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00015DC3 File Offset: 0x00013FC3
		private void OnDesignerLoadComplete(object sender, EventArgs e)
		{
			this.OnDataSourceChanged(false);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnSchemaRefreshed()
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00015DCC File Offset: 0x00013FCC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DataSource"];
			AttributeCollection attributes = propertyDescriptor.Attributes;
			int num = -1;
			int count = attributes.Count;
			string dataSource = this.DataSource;
			bool flag = dataSource != null && dataSource.Length > 0;
			if (flag)
			{
				this._keepDataSourceBrowsable = true;
			}
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is BrowsableAttribute)
				{
					num = i;
					break;
				}
			}
			int num2;
			if (num == -1 && !flag && !this._keepDataSourceBrowsable)
			{
				num2 = count + 1;
			}
			else
			{
				num2 = count;
			}
			Attribute[] array = new Attribute[num2];
			attributes.CopyTo(array, 0);
			if (!flag && !this._keepDataSourceBrowsable)
			{
				if (num == -1)
				{
					array[count] = BrowsableAttribute.No;
				}
				else
				{
					array[num] = BrowsableAttribute.No;
				}
			}
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), "DataSource", typeof(string), array);
			properties["DataSource"] = propertyDescriptor;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00015ECC File Offset: 0x000140CC
		public static DialogResult ShowCreateDataSourceDialog(ControlDesigner controlDesigner, Type dataSourceType, bool configure, out string dataSourceID)
		{
			CreateDataSourceDialog createDataSourceDialog = new CreateDataSourceDialog(controlDesigner, dataSourceType, configure);
			DialogResult result = UIServiceHelper.ShowDialog(controlDesigner.Component.Site, createDataSourceDialog);
			dataSourceID = createDataSourceDialog.DataSourceID;
			return result;
		}

		// Token: 0x04000212 RID: 530
		private bool _keepDataSourceBrowsable;
	}
}
