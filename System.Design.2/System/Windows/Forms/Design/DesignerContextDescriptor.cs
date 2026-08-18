using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D4 RID: 724
	internal class DesignerContextDescriptor : IWindowsFormsEditorService, ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x06001CBE RID: 7358 RVA: 0x000AD920 File Offset: 0x000ABB20
		public DesignerContextDescriptor(Component component, PropertyDescriptor imageProperty, IDesignerHost host)
		{
			this._component = component;
			this._propertyDescriptor = imageProperty;
			this._host = host;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000AD940 File Offset: 0x000ABB40
		public Image OpenImageCollection()
		{
			object value = this._propertyDescriptor.GetValue(this._component);
			if (this._propertyDescriptor != null)
			{
				Image image = null;
				UITypeEditor uitypeEditor = this._propertyDescriptor.GetEditor(typeof(UITypeEditor)) as UITypeEditor;
				if (uitypeEditor != null)
				{
					image = (Image)uitypeEditor.EditValue(this, this, value);
				}
				if (image != null)
				{
					return image;
				}
			}
			return (Image)value;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00003598 File Offset: 0x00001798
		IContainer ITypeDescriptorContext.Container
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x000AD9A1 File Offset: 0x000ABBA1
		object ITypeDescriptorContext.Instance
		{
			get
			{
				return this._component;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x000AD9A9 File Offset: 0x000ABBA9
		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get
			{
				return this._propertyDescriptor;
			}
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00003937 File Offset: 0x00001B37
		void ITypeDescriptorContext.OnComponentChanged()
		{
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0000445B File Offset: 0x0000265B
		bool ITypeDescriptorContext.OnComponentChanging()
		{
			return false;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000AD9B1 File Offset: 0x000ABBB1
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			return this._host.GetService(serviceType);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.CloseDropDown()
		{
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.DropDownControl(Control control)
		{
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000AD9D4 File Offset: 0x000ABBD4
		DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			IUIService iuiservice = (IUIService)((IServiceProvider)this).GetService(typeof(IUIService));
			DialogResult result;
			if (iuiservice != null)
			{
				result = iuiservice.ShowDialog(dialog);
			}
			else
			{
				result = dialog.ShowDialog(this._component as IWin32Window);
			}
			if (focus != IntPtr.Zero)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(null, focus));
			}
			return result;
		}

		// Token: 0x04001711 RID: 5905
		private Component _component;

		// Token: 0x04001712 RID: 5906
		private PropertyDescriptor _propertyDescriptor;

		// Token: 0x04001713 RID: 5907
		private IDesignerHost _host;
	}
}
