using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003BC RID: 956
	internal class PanConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002332 RID: 9010 RVA: 0x00075E38 File Offset: 0x00074038
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Pan pan = obj as Pan;
			if (pan != null && pan.Enabled)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "lock", pan.Lock.ToString().ToLowerInvariant(), AxisLock.None);
				ExplicitJavaScriptConverter.AddProperty(state, "key", pan.ModifierKey.ToString().ToLowerInvariant(), ModifierKey.None);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00075EA4 File Offset: 0x000740A4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Pan)
				};
			}
		}
	}
}
