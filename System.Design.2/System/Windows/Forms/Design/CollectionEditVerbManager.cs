using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A1 RID: 673
	internal class CollectionEditVerbManager : IWindowsFormsEditorService, ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x00094430 File Offset: 0x00092630
		internal CollectionEditVerbManager(string text, ComponentDesigner designer, PropertyDescriptor prop, bool addToDesignerVerbs)
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
			if (text == null)
			{
				text = SR.GetString("ToolStripItemCollectionEditorVerb");
			}
			this._editItemsVerb = new DesignerVerb(text, new EventHandler(this.OnEditItems));
			if (addToDesignerVerbs)
			{
				this._designer.Verbs.Add(this._editItemsVerb);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x000944C4 File Offset: 0x000926C4
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

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x000944EF File Offset: 0x000926EF
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

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x0009451A File Offset: 0x0009271A
		public DesignerVerb EditItemsVerb
		{
			get
			{
				return this._editItemsVerb;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00094522 File Offset: 0x00092722
		void ITypeDescriptorContext.OnComponentChanged()
		{
			this.ChangeService.OnComponentChanged(this._designer.Component, this._targetProperty, null, null);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00094544 File Offset: 0x00092744
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

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00094594 File Offset: 0x00092794
		object ITypeDescriptorContext.Instance
		{
			get
			{
				return this._designer.Component;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x000945A1 File Offset: 0x000927A1
		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				return this._targetProperty;
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000945AC File Offset: 0x000927AC
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(ITypeDescriptorContext) || serviceType == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			if (this._designer.Component.Site != null)
			{
				return this._designer.Component.Site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.CloseDropDown()
		{
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.DropDownControl(Control control)
		{
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0009460C File Offset: 0x0009280C
		DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
		{
			IUIService iuiservice = (IUIService)((IServiceProvider)this).GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				return iuiservice.ShowDialog(dialog);
			}
			return dialog.ShowDialog(this._designer.Component as IWin32Window);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00094650 File Offset: 0x00092850
		private void OnEditItems(object sender, EventArgs e)
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)((IServiceProvider)this).GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null)
			{
				designerActionUIService.HideUI(this._designer.Component);
			}
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

		// Token: 0x040015C5 RID: 5573
		private ComponentDesigner _designer;

		// Token: 0x040015C6 RID: 5574
		private IComponentChangeService _componentChangeSvc;

		// Token: 0x040015C7 RID: 5575
		private PropertyDescriptor _targetProperty;

		// Token: 0x040015C8 RID: 5576
		private DesignerVerb _editItemsVerb;
	}
}
