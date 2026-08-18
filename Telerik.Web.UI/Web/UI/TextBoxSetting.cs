using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200191A RID: 6426
	public class TextBoxSetting : InputSetting
	{
		// Token: 0x0600F96D RID: 63853 RVA: 0x00384F74 File Offset: 0x00383174
		internal override void Describe(IScriptDescriptor descriptor)
		{
			base.Describe(descriptor);
			if (this.PasswordStrengthSettings.ShowIndicator)
			{
				descriptor.AddScriptProperty("passwordSettings", InputUtil.PasswordStrengthSettingsToClient(this.PasswordStrengthSettings));
				if (this.PasswordStrengthSettings.OnClientPasswordStrengthCalculating != "")
				{
					descriptor.AddEvent("passwordStrengthCalculating", this.PasswordStrengthSettings.OnClientPasswordStrengthCalculating);
				}
			}
		}

		// Token: 0x17004B58 RID: 19288
		// (get) Token: 0x0600F96E RID: 63854 RVA: 0x00384FD8 File Offset: 0x003831D8
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public InputPasswordStrengthSettings PasswordStrengthSettings
		{
			get
			{
				if (this.passwordStrengthSettings == null)
				{
					this.passwordStrengthSettings = new InputPasswordStrengthSettings(base.ViewState);
				}
				return this.passwordStrengthSettings;
			}
		}

		// Token: 0x040046E5 RID: 18149
		private InputPasswordStrengthSettings passwordStrengthSettings;
	}
}
