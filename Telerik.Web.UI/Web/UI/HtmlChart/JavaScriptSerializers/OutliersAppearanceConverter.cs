using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;
using Telerik.Web.UI.HtmlChart.Appearance;

namespace Telerik.Web.UI.HtmlChart.JavaScriptSerializers
{
	// Token: 0x020004EA RID: 1258
	internal class OutliersAppearanceConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002CF6 RID: 11510 RVA: 0x00093B7C File Offset: 0x00091D7C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			OutliersAppearance outliersAppearance = obj as OutliersAppearance;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (outliersAppearance != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "background", HtmlChartHelper.ColorToHex(outliersAppearance.BackgroundColor), HtmlChartHelper.ColorToHex(Color.Empty));
				ExplicitJavaScriptConverter.AddProperty(dictionary, "rotation", outliersAppearance.RotationAngle, 0);
				ExplicitJavaScriptConverter.AddProperty(dictionary, "rotation", outliersAppearance.RotationAngle, 0);
				this.SerializeMarkersType(outliersAppearance, dictionary);
				ExplicitJavaScriptConverter.AddProperty(dictionary, "size", outliersAppearance.Size, null);
				ExplicitJavaScriptConverter.AddProperty(dictionary, "visible", outliersAppearance.Visible, null);
				ExplicitJavaScriptConverter.AddProperty(dictionary, "border", outliersAppearance.BorderAppearance, null);
			}
			return dictionary;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x00093C3D File Offset: 0x00091E3D
		protected virtual void SerializeMarkersType(OutliersAppearance outliers, IDictionary<string, object> state)
		{
			ExplicitJavaScriptConverter.AddProperty(state, "type", outliers.MarkersType.ToString().ToLower(), OutliersMarkersType.Cross.ToString().ToLower());
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x00093C70 File Offset: 0x00091E70
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(OutliersAppearance),
					typeof(BorderAppearance)
				};
			}
		}
	}
}
