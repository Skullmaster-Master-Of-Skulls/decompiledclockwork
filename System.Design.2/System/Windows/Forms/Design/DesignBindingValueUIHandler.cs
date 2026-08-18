using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D0 RID: 720
	internal class DesignBindingValueUIHandler : IDisposable
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x000AC36C File Offset: 0x000AA56C
		internal Bitmap DataBitmap
		{
			get
			{
				if (this.dataBitmap == null)
				{
					this.dataBitmap = new Bitmap(BitmapSelector.GetResourceStream(typeof(DesignBindingValueUIHandler), "BoundProperty.bmp"));
					this.dataBitmap.MakeTransparent();
				}
				return this.dataBitmap;
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000AC3A8 File Offset: 0x000AA5A8
		internal void OnGetUIValueItem(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUIItemList)
		{
			if (context.Instance is Control)
			{
				Control control = (Control)context.Instance;
				foreach (object obj in control.DataBindings)
				{
					Binding binding = (Binding)obj;
					if ((binding.DataSource is IListSource || binding.DataSource is IList || binding.DataSource is Array) && binding.PropertyName.Equals(propDesc.Name))
					{
						valueUIItemList.Add(new DesignBindingValueUIHandler.LocalUIItem(this, binding));
					}
				}
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000AC460 File Offset: 0x000AA660
		private void OnPropertyValueUIItemInvoke(ITypeDescriptorContext context, PropertyDescriptor descriptor, PropertyValueUIItem invokedItem)
		{
			DesignBindingValueUIHandler.LocalUIItem localUIItem = (DesignBindingValueUIHandler.LocalUIItem)invokedItem;
			IServiceProvider serviceProvider = null;
			Control control = localUIItem.Binding.Control;
			if (control.Site != null)
			{
				serviceProvider = (IServiceProvider)control.Site.GetService(typeof(IServiceProvider));
			}
			if (serviceProvider != null)
			{
				AdvancedBindingPropertyDescriptor.advancedBindingEditor.EditValue(context, serviceProvider, control.DataBindings);
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000AC4BB File Offset: 0x000AA6BB
		public void Dispose()
		{
			if (this.dataBitmap != null)
			{
				this.dataBitmap.Dispose();
			}
		}

		// Token: 0x040016F4 RID: 5876
		private Bitmap dataBitmap;

		// Token: 0x02000567 RID: 1383
		private class LocalUIItem : PropertyValueUIItem
		{
			// Token: 0x060031AF RID: 12719 RVA: 0x0010DFF9 File Offset: 0x0010C1F9
			internal LocalUIItem(DesignBindingValueUIHandler handler, Binding binding) : base(handler.DataBitmap, new PropertyValueUIItemInvokeHandler(handler.OnPropertyValueUIItemInvoke), DesignBindingValueUIHandler.LocalUIItem.GetToolTip(binding))
			{
				this.binding = binding;
			}

			// Token: 0x170009A8 RID: 2472
			// (get) Token: 0x060031B0 RID: 12720 RVA: 0x0010E020 File Offset: 0x0010C220
			internal Binding Binding
			{
				get
				{
					return this.binding;
				}
			}

			// Token: 0x060031B1 RID: 12721 RVA: 0x0010E028 File Offset: 0x0010C228
			private static string GetToolTip(Binding binding)
			{
				string text = "";
				if (binding.DataSource is IComponent)
				{
					IComponent component = (IComponent)binding.DataSource;
					if (component.Site != null)
					{
						text = component.Site.Name;
					}
				}
				if (text.Length == 0)
				{
					text = "(List)";
				}
				return text + " - " + binding.BindingMemberInfo.BindingMember;
			}

			// Token: 0x04002138 RID: 8504
			private Binding binding;
		}
	}
}
