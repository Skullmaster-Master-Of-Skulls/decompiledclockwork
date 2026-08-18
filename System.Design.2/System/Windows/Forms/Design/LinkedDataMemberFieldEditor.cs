using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000305 RID: 773
	internal class LinkedDataMemberFieldEditor : UITypeEditor
	{
		// Token: 0x06001EA0 RID: 7840 RVA: 0x000B772C File Offset: 0x000B592C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context != null && context.Instance != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(context.Instance)["LinkedDataSource"];
				if (propertyDescriptor != null)
				{
					object value2 = propertyDescriptor.GetValue(context.Instance);
					if (value2 != null)
					{
						if (this.designBindingPicker == null)
						{
							this.designBindingPicker = new DesignBindingPicker();
						}
						DesignBinding initialSelectedItem = new DesignBinding(null, (string)value);
						DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, false, true, false, value2, string.Empty, initialSelectedItem);
						if (designBinding != null)
						{
							value = designBinding.DataMember;
						}
					}
				}
			}
			return value;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040017D4 RID: 6100
		private DesignBindingPicker designBindingPicker;
	}
}
