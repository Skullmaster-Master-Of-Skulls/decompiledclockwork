using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001917 RID: 6423
	public class InputSettingCustomValidator : CustomValidator
	{
		// Token: 0x0600F93E RID: 63806 RVA: 0x003846E0 File Offset: 0x003828E0
		public InputSettingCustomValidator(RadInputManager manager, InputSetting setting)
		{
			this._setting = setting;
			this._manager = manager;
			this.ForeColor = Color.Empty;
			setting.Validation.AssignedValidator = this;
		}

		// Token: 0x0600F93F RID: 63807 RVA: 0x00384710 File Offset: 0x00382910
		protected override bool EvaluateIsValid()
		{
			bool flag = base.EvaluateIsValid();
			List<string> list = new List<string>();
			InputManagerValidatingEventArgs inputManagerValidatingEventArgs = new InputManagerValidatingEventArgs(this._setting, flag);
			this._manager.OnValidating(inputManagerValidatingEventArgs);
			if (!inputManagerValidatingEventArgs.Canceled)
			{
				foreach (object obj in this._setting.TargetControls)
				{
					TargetInput targetInput = (TargetInput)obj;
					Control control = ChildControlHelper.FindControlRecursive(this, targetInput.ControlID, null);
					if (control != null)
					{
						TextBox textBox = control as TextBox;
						if (textBox != null && textBox.Enabled && control.Visible)
						{
							this._setting.Validate(textBox, inputManagerValidatingEventArgs.Context);
							if (!this._setting.IsValid)
							{
								list.Add(control.ID);
							}
						}
					}
				}
				if (list.Count > 0)
				{
					flag = false;
				}
				InputManagerValidatedEventArgs args = new InputManagerValidatedEventArgs(this._setting);
				this._manager.OnValidated(args);
			}
			else
			{
				flag = inputManagerValidatingEventArgs.IsValid;
				if (!flag)
				{
					if (this._setting.invalidIds == null)
					{
						this._setting.invalidIds = new List<string>();
					}
					foreach (object obj2 in this._setting.TargetControls)
					{
						TargetInput targetInput2 = (TargetInput)obj2;
						Control control2 = ChildControlHelper.FindControlRecursive(this, targetInput2.ControlID, null);
						if (control2 != null && control2 is TextBox && control2.Visible && !this._setting.invalidIds.Contains(control2.ID))
						{
							this._setting.invalidIds.Add(control2.ID);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x040046E2 RID: 18146
		private InputSetting _setting;

		// Token: 0x040046E3 RID: 18147
		private RadInputManager _manager;
	}
}
