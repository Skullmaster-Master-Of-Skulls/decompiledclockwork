using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003BD RID: 957
	internal class SelectionZoomConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002335 RID: 9013 RVA: 0x00075ED0 File Offset: 0x000740D0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			SelectionZoom selectionZoom = obj as SelectionZoom;
			if (selectionZoom != null && selectionZoom.Enabled)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "lock", selectionZoom.Lock.ToString().ToLowerInvariant(), AxisLock.None);
				ExplicitJavaScriptConverter.AddProperty(state, "key", selectionZoom.ModifierKey.ToString().ToLowerInvariant(), ModifierKey.Shift);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x00075F3C File Offset: 0x0007413C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SelectionZoom)
				};
			}
		}
	}
}
