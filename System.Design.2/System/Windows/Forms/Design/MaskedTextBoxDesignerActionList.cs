using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000314 RID: 788
	internal class MaskedTextBoxDesignerActionList : DesignerActionList
	{
		// Token: 0x06001F2A RID: 7978 RVA: 0x000BB488 File Offset: 0x000B9688
		public MaskedTextBoxDesignerActionList(MaskedTextBoxDesigner designer) : base(designer.Component)
		{
			this.maskedTextBox = (MaskedTextBox)designer.Component;
			this.discoverySvc = (base.GetService(typeof(ITypeDiscoveryService)) as ITypeDiscoveryService);
			this.uiSvc = (base.GetService(typeof(IUIService)) as IUIService);
			this.helpService = (base.GetService(typeof(IHelpService)) as IHelpService);
			if (this.discoverySvc != null)
			{
				IUIService iuiservice = this.uiSvc;
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000BB514 File Offset: 0x000B9714
		public void SetMask()
		{
			string text = MaskPropertyEditor.EditMask(this.discoverySvc, this.uiSvc, this.maskedTextBox, this.helpService);
			if (text != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.maskedTextBox)["Mask"];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(this.maskedTextBox, text);
				}
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000BB568 File Offset: 0x000B9768
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "SetMask", SR.GetString("MaskedTextBoxDesignerVerbsSetMaskDesc"))
			};
		}

		// Token: 0x04001801 RID: 6145
		private MaskedTextBox maskedTextBox;

		// Token: 0x04001802 RID: 6146
		private ITypeDiscoveryService discoverySvc;

		// Token: 0x04001803 RID: 6147
		private IUIService uiSvc;

		// Token: 0x04001804 RID: 6148
		private IHelpService helpService;
	}
}
