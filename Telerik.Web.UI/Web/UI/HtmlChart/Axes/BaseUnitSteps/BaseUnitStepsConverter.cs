using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart.Axes.BaseUnitSteps
{
	// Token: 0x020003AC RID: 940
	public class BaseUnitStepsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002320 RID: 8992 RVA: 0x00075904 File Offset: 0x00073B04
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			BaseUnitSteps baseUnitSteps = obj as BaseUnitSteps;
			if (baseUnitSteps.Seconds.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "seconds", baseUnitSteps.Seconds.ToIntList(), null);
			}
			if (baseUnitSteps.Minutes.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "minutes", baseUnitSteps.Minutes.ToIntList(), null);
			}
			if (baseUnitSteps.Hours.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "hours", baseUnitSteps.Hours.ToIntList(), null);
			}
			if (baseUnitSteps.Days.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "days", baseUnitSteps.Days.ToIntList(), null);
			}
			if (baseUnitSteps.Weeks.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "weeks", baseUnitSteps.Weeks.ToIntList(), null);
			}
			if (baseUnitSteps.Months.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "months", baseUnitSteps.Months.ToIntList(), null);
			}
			if (baseUnitSteps.Years.Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "years", baseUnitSteps.Years.ToIntList(), null);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x00075A1C File Offset: 0x00073C1C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BaseUnitSteps)
				};
			}
		}
	}
}
