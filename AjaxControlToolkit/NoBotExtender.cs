using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x0200014E RID: 334
	[ClientScriptResource("Sys.Extended.UI.NoBotBehavior", "NoBot")]
	[TargetControlType(typeof(Label))]
	[ToolboxItem(false)]
	public class NoBotExtender : ExtenderControlBase
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x000178A4 File Offset: 0x00015AA4
		public NoBotExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000178B3 File Offset: 0x00015AB3
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x000178C5 File Offset: 0x00015AC5
		[ClientPropertyName("challengeScript")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public string ChallengeScript
		{
			get
			{
				return base.GetPropertyValue<string>("ChallengeScript", "");
			}
			set
			{
				base.SetPropertyValue<string>("ChallengeScript", value);
			}
		}
	}
}
