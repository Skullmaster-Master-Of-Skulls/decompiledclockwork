using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002CD RID: 717
	internal class DesignBindingEditor : UITypeEditor
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000AA6C8 File Offset: 0x000A88C8
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				if (this.designBindingPicker == null)
				{
					this.designBindingPicker = new DesignBindingPicker();
				}
				value = this.designBindingPicker.Pick(context, provider, true, true, false, null, string.Empty, (DesignBinding)value);
			}
			return value;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040016D3 RID: 5843
		private DesignBindingPicker designBindingPicker;
	}
}
