using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003BE RID: 958
	internal class MouseWheelZoomConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002338 RID: 9016 RVA: 0x00075F68 File Offset: 0x00074168
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			MouseWheelZoom mouseWheelZoom = obj as MouseWheelZoom;
			if (mouseWheelZoom != null && mouseWheelZoom.Enabled)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "lock", mouseWheelZoom.Lock.ToString().ToLowerInvariant(), AxisLock.None);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x00075FB0 File Offset: 0x000741B0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MouseWheelZoom)
				};
			}
		}
	}
}
