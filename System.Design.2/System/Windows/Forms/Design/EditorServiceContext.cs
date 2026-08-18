using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002DE RID: 734
	internal class EditorServiceContext : IWindowsFormsEditorService, ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x06001D66 RID: 7526 RVA: 0x000B1BE6 File Offset: 0x000AFDE6
		internal EditorServiceContext(ComponentDesigner designer)
		{
			this._designer = designer;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000B1BF8 File Offset: 0x000AFDF8
		internal EditorServiceContext(ComponentDesigner designer, PropertyDescriptor prop)
		{
			this._designer = designer;
			this._targetProperty = prop;
			if (prop == null)
			{
				prop = TypeDescriptor.GetDefaultProperty(designer.Component);
				if (prop != null && typeof(ICollection).IsAssignableFrom(prop.PropertyType))
				{
					this._targetProperty = prop;
				}
			}
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000B1C4A File Offset: 0x000AFE4A
		internal EditorServiceContext(ComponentDesigner designer, PropertyDescriptor prop, string newVerbText) : this(designer, prop)
		{
			this._designer.Verbs.Add(new DesignerVerb(newVerbText, new EventHandler(this.OnEditItems)));
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000B1C78 File Offset: 0x000AFE78
		public static object EditValue(ComponentDesigner designer, object objectToChange, string propName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(objectToChange)[propName];
			EditorServiceContext editorServiceContext = new EditorServiceContext(designer, propertyDescriptor);
			UITypeEditor uitypeEditor = propertyDescriptor.GetEditor(typeof(UITypeEditor)) as UITypeEditor;
			object value = propertyDescriptor.GetValue(objectToChange);
			object obj = uitypeEditor.EditValue(editorServiceContext, editorServiceContext, value);
			if (obj != value)
			{
				try
				{
					propertyDescriptor.SetValue(objectToChange, obj);
				}
				catch (CheckoutException)
				{
				}
			}
			return obj;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x000B1CE8 File Offset: 0x000AFEE8
		private IComponentChangeService ChangeService
		{
			get
			{
				if (this._componentChangeSvc == null)
				{
					this._componentChangeSvc = (IComponentChangeService)((IServiceProvider)this).GetService(typeof(IComponentChangeService));
				}
				return this._componentChangeSvc;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x000B1D13 File Offset: 0x000AFF13
		IContainer ITypeDescriptorContext.Container
		{
			get
			{
				if (this._designer.Component.Site != null)
				{
					return this._designer.Component.Site.Container;
				}
				return null;
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000B1D3E File Offset: 0x000AFF3E
		void ITypeDescriptorContext.OnComponentChanged()
		{
			this.ChangeService.OnComponentChanged(this._designer.Component, this._targetProperty, null, null);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000B1D60 File Offset: 0x000AFF60
		bool ITypeDescriptorContext.OnComponentChanging()
		{
			try
			{
				this.ChangeService.OnComponentChanging(this._designer.Component, this._targetProperty);
			}
			catch (CheckoutException ex)
			{
				if (ex == CheckoutException.Canceled)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000B1DB0 File Offset: 0x000AFFB0
		object ITypeDescriptorContext.Instance
		{
			get
			{
				return this._designer.Component;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x000B1DBD File Offset: 0x000AFFBD
		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				return this._targetProperty;
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000B1DC8 File Offset: 0x000AFFC8
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(ITypeDescriptorContext) || serviceType == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			if (this._designer.Component != null && this._designer.Component.Site != null)
			{
				return this._designer.Component.Site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.CloseDropDown()
		{
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.DropDownControl(Control control)
		{
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000B1E34 File Offset: 0x000B0034
		DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
		{
			IUIService iuiservice = (IUIService)((IServiceProvider)this).GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				return iuiservice.ShowDialog(dialog);
			}
			return dialog.ShowDialog(this._designer.Component as IWin32Window);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000B1E78 File Offset: 0x000B0078
		private void OnEditItems(object sender, EventArgs e)
		{
			object value = this._targetProperty.GetValue(this._designer.Component);
			if (value == null)
			{
				return;
			}
			CollectionEditor collectionEditor = TypeDescriptor.GetEditor(value, typeof(UITypeEditor)) as CollectionEditor;
			if (collectionEditor != null)
			{
				collectionEditor.EditValue(this, this, value);
			}
		}

		// Token: 0x0400176E RID: 5998
		private ComponentDesigner _designer;

		// Token: 0x0400176F RID: 5999
		private IComponentChangeService _componentChangeSvc;

		// Token: 0x04001770 RID: 6000
		private PropertyDescriptor _targetProperty;
	}
}
