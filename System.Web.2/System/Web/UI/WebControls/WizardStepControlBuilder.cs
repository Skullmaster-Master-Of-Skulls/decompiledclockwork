using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200051D RID: 1309
	public sealed class WizardStepControlBuilder : ControlBuilder
	{
		// Token: 0x06004245 RID: 16965 RVA: 0x000D86AC File Offset: 0x000D68AC
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
