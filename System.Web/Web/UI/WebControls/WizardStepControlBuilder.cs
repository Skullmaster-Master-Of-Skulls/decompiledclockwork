using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200068A RID: 1674
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WizardStepControlBuilder : ControlBuilder
	{
		// Token: 0x0600520C RID: 21004 RVA: 0x0014B9D4 File Offset: 0x0014A9D4
		internal override void SetParentBuilder(ControlBuilder parentBuilder)
		{
			if (base.Parser.FInDesigner || base.Parser is PageThemeParser)
			{
				return;
			}
			if (parentBuilder.ControlType == null || !typeof(WizardStepCollection).IsAssignableFrom(parentBuilder.ControlType))
			{
				throw new HttpException(SR.GetString("WizardStep_WrongContainment"));
			}
			base.SetParentBuilder(parentBuilder);
		}
	}
}
