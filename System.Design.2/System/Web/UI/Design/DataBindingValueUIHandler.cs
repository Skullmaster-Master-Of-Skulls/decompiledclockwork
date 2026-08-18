using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000023 RID: 35
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataBindingValueUIHandler
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000BBFD File Offset: 0x00009DFD
		private Bitmap DataBindingBitmap
		{
			get
			{
				if (this.dataBindingBitmap == null)
				{
					this.dataBindingBitmap = BitmapSelector.CreateBitmap(typeof(DataBindingValueUIHandler), "DataBindingGlyph.bmp");
					this.dataBindingBitmap.MakeTransparent();
				}
				return this.dataBindingBitmap;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000BC32 File Offset: 0x00009E32
		private string DataBindingToolTip
		{
			get
			{
				if (this.dataBindingToolTip == null)
				{
					this.dataBindingToolTip = SR.GetString("DataBindingGlyph_ToolTip");
				}
				return this.dataBindingToolTip;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000BC54 File Offset: 0x00009E54
		public void OnGetUIValueItem(ITypeDescriptorContext context, PropertyDescriptor propDesc, ArrayList valueUIItemList)
		{
			Control control = context.Instance as Control;
			if (control != null)
			{
				IDataBindingsAccessor dataBindingsAccessor = control;
				if (dataBindingsAccessor.HasDataBindings)
				{
					DataBinding dataBinding = dataBindingsAccessor.DataBindings[propDesc.Name];
					if (dataBinding != null)
					{
						valueUIItemList.Add(new DataBindingValueUIHandler.DataBindingUIItem(this));
					}
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003937 File Offset: 0x00001B37
		private void OnValueUIItemInvoke(ITypeDescriptorContext context, PropertyDescriptor propDesc, PropertyValueUIItem invokedItem)
		{
		}

		// Token: 0x0400010E RID: 270
		private Bitmap dataBindingBitmap;

		// Token: 0x0400010F RID: 271
		private string dataBindingToolTip;

		// Token: 0x020003AD RID: 941
		private class DataBindingUIItem : PropertyValueUIItem
		{
			// Token: 0x060025F3 RID: 9715 RVA: 0x000EC3DB File Offset: 0x000EA5DB
			public DataBindingUIItem(DataBindingValueUIHandler handler) : base(handler.DataBindingBitmap, new PropertyValueUIItemInvokeHandler(handler.OnValueUIItemInvoke), handler.DataBindingToolTip)
			{
			}
		}
	}
}
