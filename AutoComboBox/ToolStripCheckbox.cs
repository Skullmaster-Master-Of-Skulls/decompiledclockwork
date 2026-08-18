using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AutoComboBox
{
	// Token: 0x0200003E RID: 62
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripCheckbox : ToolStripControlHost
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00012811 File Offset: 0x00011811
		public ToolStripCheckbox() : base(new CheckBox())
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00012824 File Offset: 0x00011824
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CheckBox MyCheckBox
		{
			get
			{
				return (CheckBox)base.Control;
			}
		}
	}
}
