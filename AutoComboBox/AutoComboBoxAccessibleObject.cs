using System;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000070 RID: 112
	internal class AutoComboBoxAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x000235D6 File Offset: 0x000225D6
		public AutoComboBoxAccessibleObject(AutoComboBox cmb) : base(cmb)
		{
			this.ctrl = cmb;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000235EC File Offset: 0x000225EC
		public override string Description
		{
			get
			{
				return this.ctrl.AccessibleDescription;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0002360C File Offset: 0x0002260C
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00023642 File Offset: 0x00022642
		public override string Name
		{
			get
			{
				string accessibleName = this.ctrl.AccessibleName;
				string result;
				if (accessibleName != null)
				{
					result = accessibleName;
				}
				else
				{
					result = this.ctrl.Text;
				}
				return result;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00023650 File Offset: 0x00022650
		public override AccessibleRole Role
		{
			get
			{
				return AccessibleRole.ComboBox;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00023664 File Offset: 0x00022664
		public override AccessibleStates State
		{
			get
			{
				return AccessibleStates.ReadOnly;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00023678 File Offset: 0x00022678
		public override int GetChildCount()
		{
			return 0;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0002368C File Offset: 0x0002268C
		public override AccessibleObject GetChild(int index)
		{
			return null;
		}

		// Token: 0x040003D6 RID: 982
		private AutoComboBox ctrl;
	}
}
