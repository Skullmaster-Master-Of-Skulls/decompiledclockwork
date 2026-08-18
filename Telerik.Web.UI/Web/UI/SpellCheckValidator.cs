using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02001B51 RID: 6993
	[ToolboxBitmap(typeof(SpellCheckValidator), "Telerik.Web.UI.Spell.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Description("RadSpell spellcheck validator")]
	[TelerikToolboxCategory("Miscellaneous")]
	public class SpellCheckValidator : CustomValidator
	{
		// Token: 0x06010F19 RID: 69401 RVA: 0x003C0546 File Offset: 0x003BE746
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}

		// Token: 0x06010F1A RID: 69402 RVA: 0x003C0558 File Offset: 0x003BE758
		private void SpellCheckValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			RadSpell radSpell = this.NamingContainer.FindControl(base.ControlToValidate) as RadSpell;
			if (radSpell != null)
			{
				args.IsValid = radSpell.SpellChecked;
			}
		}

		// Token: 0x06010F1B RID: 69403 RVA: 0x003C058B File Offset: 0x003BE78B
		private void InitializeComponent()
		{
			base.ServerValidate += this.SpellCheckValidator_ServerValidate;
			base.PreRender += this.SpellCheckValidator_PreRender;
		}

		// Token: 0x06010F1C RID: 69404 RVA: 0x003C05B4 File Offset: 0x003BE7B4
		private void SpellCheckValidator_PreRender(object sender, EventArgs e)
		{
			RadSpell radSpell = this.NamingContainer.FindControl(base.ControlToValidate) as RadSpell;
			if (radSpell == null)
			{
				throw new ArgumentException("Could not find a target RadSpell object.  Please set ControlToValidate to point to a RadSpell control.");
			}
			string text = "<script type='text/javascript'>\r\n/*<![CDATA[*/\r\nfunction {0}spellValidate(variable, args)\r\n{{\r\n\tvar spell = $find('{0}');\r\n\tif (spell == null)\r\n\t{{\r\n\t\talert('Could not find target RadSpell object');\r\n\t\targs.IsValid = false;\r\n\t}}\r\n\telse\r\n\t{{\r\n\t\targs.IsValid = spell.get_spellChecked();\r\n\t}}\r\n}}\r\n/*]]>*/\r\n</script>";
			text = text.Replace("\n", "").Replace("\r", "").Replace("\t", "");
			this.Controls.Add(new LiteralControl(string.Format(text, radSpell.ClientID)));
			base.ClientValidationFunction = string.Format("{0}spellValidate", radSpell.ClientID);
		}
	}
}
