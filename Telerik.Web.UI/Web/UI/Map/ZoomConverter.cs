using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005BB RID: 1467
	public class ZoomConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003452 RID: 13394 RVA: 0x000AD828 File Offset: 0x000ABA28
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Zoom zoom = obj as Zoom;
			ExplicitJavaScriptConverter.AddProperty(state, "position", StringHelpers.ToCamelCase(zoom.Position.ToString()), StringHelpers.ToCamelCase(ZoomPosition.TopLeft.ToString()));
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06003453 RID: 13395 RVA: 0x000AD86C File Offset: 0x000ABA6C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Zoom)
				};
			}
		}
	}
}
