using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C7 RID: 711
	internal class DataMemberListEditor : UITypeEditor
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000AA138 File Offset: 0x000A8338
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
					DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, false, true, true, value2, string.Empty, initialSelectedItem);
					if (value2 != null && designBinding != null)
					{
						value = designBinding.DataMember;
					}
				}
			}
			return value;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040016CD RID: 5837
		private DesignBindingPicker designBindingPicker;
	}
}
