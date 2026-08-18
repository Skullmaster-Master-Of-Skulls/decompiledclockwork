using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000689 RID: 1673
	[ToolboxItem(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	[Bindable(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WizardStep : WizardStepBase
	{
	}
}
