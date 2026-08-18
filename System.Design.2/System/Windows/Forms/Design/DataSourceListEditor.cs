using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C9 RID: 713
	internal class DataSourceListEditor : UITypeEditor
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000AA37C File Offset: 0x000A857C
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null && context.Instance != null)
			{
				if (this.designBindingPicker == null)
				{
					this.designBindingPicker = new DesignBindingPicker();
				}
				DesignBinding initialSelectedItem = new DesignBinding(value, "");
				DesignBinding designBinding = this.designBindingPicker.Pick(context, provider, true, false, false, null, string.Empty, initialSelectedItem);
				if (designBinding != null)
				{
					value = designBinding.DataSource;
				}
			}
			return value;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040016CF RID: 5839
		private DesignBindingPicker designBindingPicker;
	}
}
