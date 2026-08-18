using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000452 RID: 1106
	public class ShapeRotationConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060027E5 RID: 10213 RVA: 0x000818F4 File Offset: 0x0007FAF4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeRotation shapeRotation = obj as ShapeRotation;
			ExplicitJavaScriptConverter.AddProperty(state, "angle", shapeRotation.Angle, 0.0);
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x0008192C File Offset: 0x0007FB2C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeRotation)
				};
			}
		}
	}
}
