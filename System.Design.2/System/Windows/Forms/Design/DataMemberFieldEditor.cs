using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C6 RID: 710
	internal class DataMemberFieldEditor : UITypeEditor
	{
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000AA0B0 File Offset: 0x000A82B0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context != null && context.Instance != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(context.Instance)["DataSource"];
				if (propertyDescriptor != null)
				{
					object value2 = propertyDescriptor.GetValue(context.Instance);
					if (this.designBindingPicker == null)
					{
						this.designBindingPicker = new DesignBindingPicker();
					}
					DesignBinding initialSelectedItem = new DesignBinding(value2, (string)value);
					DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, false, true, false, value2, string.Empty, initialSelectedItem);
					if (value2 != null && designBinding != null)
					{
						value = designBinding.DataMember;
					}
				}
			}
			return value;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040016CC RID: 5836
		private DesignBindingPicker designBindingPicker;
	}
}
