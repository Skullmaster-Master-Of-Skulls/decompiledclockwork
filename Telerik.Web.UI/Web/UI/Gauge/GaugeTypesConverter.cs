using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B5F RID: 2911
	internal class GaugeTypesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06006DDC RID: 28124 RVA: 0x00197B38 File Offset: 0x00195D38
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Appearance appearance = obj as Appearance;
			if (appearance != null)
			{
				return this.SerializeAppearance(appearance);
			}
			RadialScale radialScale = obj as RadialScale;
			if (radialScale != null)
			{
				return this.SerializeRadialScale(radialScale);
			}
			RadialPointer radialPointer = obj as RadialPointer;
			if (radialPointer != null)
			{
				return this.SerializeRadialPointer(radialPointer);
			}
			RadialPointersCollection radialPointersCollection = obj as RadialPointersCollection;
			if (radialPointersCollection != null)
			{
				return this.SerializeRadialPointersCollection(radialPointersCollection);
			}
			LinearScale linearScale = obj as LinearScale;
			if (linearScale != null)
			{
				return this.SerializeLinearScale(linearScale);
			}
			LinearPointer linearPointer = obj as LinearPointer;
			if (linearPointer != null)
			{
				return this.SerializeLinearPointer(linearPointer);
			}
			LinearPointersCollection linearPointersCollection = obj as LinearPointersCollection;
			if (linearPointersCollection != null)
			{
				return this.SerializeLinearPointersCollection(linearPointersCollection);
			}
			throw new InvalidOperationException("Can serialize only RadialScale, RadialPointer, LinearScale and LinearPointer objects.");
		}

		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x06006DDD RID: 28125 RVA: 0x00197BD8 File Offset: 0x00195DD8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadialScale),
					typeof(RadialPointer),
					typeof(LinearScale),
					typeof(LinearPointer),
					typeof(Appearance),
					typeof(RadialPointersCollection),
					typeof(LinearPointersCollection)
				};
			}
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x00197C48 File Offset: 0x00195E48
		private static string LowercaseFirst(string s)
		{
			return char.ToLowerInvariant(s[0]) + s.Substring(1);
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x00197CC4 File Offset: 0x00195EC4
		private IDictionary<string, object> SerializeRadialScale(RadialScale radialScale)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int startAngle = radialScale.StartAngle;
			if (startAngle != -30)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "startAngle", startAngle, -30);
			}
			int endAngle = radialScale.EndAngle;
			if (endAngle != 210)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "endAngle", endAngle, 210);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "min", radialScale.Min, 0);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "max", radialScale.Max, 100);
			decimal? minorUnit = radialScale.MinorUnit;
			if (minorUnit != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "minorUnit", minorUnit, null);
			}
			decimal? majorUnit = radialScale.MajorUnit;
			if (majorUnit != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "majorUnit", majorUnit, null);
			}
			bool reverse = radialScale.Reverse;
			if (reverse)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "reverse", reverse, false);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "minorTicks", this.SerializeTicks(radialScale.MinorTicks), new
			{

			});
			ExplicitJavaScriptConverter.AddProperty(dictionary, "majorTicks", this.SerializeTicks(radialScale.MajorTicks), new
			{

			});
			ExplicitJavaScriptConverter.AddProperty(dictionary, "labels", this.SerializeLabels(radialScale.Labels, false), new
			{

			});
			ExplicitJavaScriptConverter.AddProperty(dictionary, "ranges", this.SerializeRanges(radialScale.Ranges), new object[0]);
			return dictionary;
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x00197E3C File Offset: 0x0019603C
		private IDictionary<string, object> SerializeLinearScale(LinearScale linearScale)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			bool vertical = linearScale.Vertical;
			if (!vertical)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "vertical", vertical, true);
			}
			bool mirror = linearScale.Mirror;
			if (mirror)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "mirror", mirror, false);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "min", linearScale.Min, 0);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "max", linearScale.Max, 100);
			decimal? minorUnit = linearScale.MinorUnit;
			if (minorUnit != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "minorUnit", minorUnit, null);
			}
			decimal? majorUnit = linearScale.MajorUnit;
			if (majorUnit != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "majorUnit", majorUnit, null);
			}
			bool reverse = linearScale.Reverse;
			if (reverse)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "reverse", reverse, false);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "minorTicks", this.SerializeTicks(linearScale.MinorTicks), new
			{

			});
			ExplicitJavaScriptConverter.AddProperty(dictionary, "majorTicks", this.SerializeTicks(linearScale.MajorTicks), new
			{

			});
			ExplicitJavaScriptConverter.AddProperty(dictionary, "labels", this.SerializeLabels(linearScale.Labels, true), new
			{

			});
			ExplicitJavaScriptConverter.AddProperty(dictionary, "ranges", this.SerializeRanges(linearScale.Ranges), new object[0]);
			return dictionary;
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x00197FA8 File Offset: 0x001961A8
		private IDictionary<string, object> SerializeTicks(Ticks ticks)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color color = ticks.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			float? size = ticks.Size;
			if (size != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "size", size, null);
			}
			double width = ticks.Width;
			if (width != 0.5)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "size", width, null);
			}
			bool visible = ticks.Visible;
			if (!visible)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "visible", visible, true);
			}
			return dictionary;
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x00198054 File Offset: 0x00196254
		private IDictionary<string, object> SerializeLabels(ScaleLabels labels, bool isLinearScale)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color backgroundColor = labels.BackgroundColor;
			if (backgroundColor != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "background", ColorTranslator.ToHtml(backgroundColor), "");
			}
			Color color = labels.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			string font = labels.Font;
			if (!string.IsNullOrEmpty(font))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "font", font, "");
			}
			string format = labels.Format;
			if (!string.IsNullOrEmpty(format))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "format", format, "");
			}
			string template = labels.Template;
			if (!string.IsNullOrEmpty(template))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "template", template, "");
			}
			bool visible = labels.Visible;
			if (!visible)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "visible", visible, true);
			}
			if (isLinearScale)
			{
				return dictionary;
			}
			ScaleLabelsPosition position = labels.Position;
			if (position != ScaleLabelsPosition.Inside)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "position", GaugeTypesConverter.LowercaseFirst(position.ToString()), "inside");
			}
			return dictionary;
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x00198178 File Offset: 0x00196378
		private object[] SerializeRanges(GaugeRangeCollection ranges)
		{
			int count = ranges.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.SerializeGaugeRange(ranges[i]);
			}
			return array;
		}

		// Token: 0x06006DE4 RID: 28132 RVA: 0x001981B0 File Offset: 0x001963B0
		private IDictionary<string, object> SerializeGaugeRange(GaugeRange gaugeRange)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color color = gaugeRange.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "from", gaugeRange.From, null);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "to", gaugeRange.To, null);
			return dictionary;
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x0019821C File Offset: 0x0019641C
		private IDictionary<string, object> SerializeRadialPointer(RadialPointer radialPointer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color color = radialPointer.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			decimal? value = radialPointer.Value;
			if (value != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "value", value, null);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "cap", this.SerializeCap(radialPointer.Cap), new
			{

			});
			return dictionary;
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x00198298 File Offset: 0x00196498
		private IDictionary<string, object> SerializeRadialPointersCollection(RadialPointersCollection radialPointersCollection)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			object[] array = new object[radialPointersCollection.Count];
			for (int i = 0; i < radialPointersCollection.Count; i++)
			{
				array[i] = this.SerializeRadialPointer(radialPointersCollection[i]);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "pointers", array, new object[0]);
			return dictionary;
		}

		// Token: 0x06006DE7 RID: 28135 RVA: 0x001982EC File Offset: 0x001964EC
		private IDictionary<string, object> SerializeLinearPointer(LinearPointer linearPointer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color color = linearPointer.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			decimal? value = linearPointer.Value;
			if (value != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "value", value, null);
			}
			float opacity = linearPointer.Opacity;
			if (opacity != 1f)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "opacity", opacity, 1);
			}
			float? size = linearPointer.Size;
			if (size != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "size", size, null);
			}
			PointerShape shape = linearPointer.Shape;
			if (shape == PointerShape.BarIndicator)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "track", this.SerializeTrack(linearPointer.Track), new
				{

				});
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "shape", GaugeTypesConverter.LowercaseFirst(shape.ToString()), "barIndicator");
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "margin", linearPointer.Margin, null);
			return dictionary;
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x001983F8 File Offset: 0x001965F8
		private IDictionary<string, object> SerializeLinearPointersCollection(LinearPointersCollection linearPointersCollection)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			object[] array = new object[linearPointersCollection.Count];
			for (int i = 0; i < linearPointersCollection.Count; i++)
			{
				array[i] = this.SerializeLinearPointer(linearPointersCollection[i]);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "pointers", array, new object[0]);
			return dictionary;
		}

		// Token: 0x06006DE9 RID: 28137 RVA: 0x0019844C File Offset: 0x0019664C
		private IDictionary<string, object> SerializeCap(Cap cap)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color color = cap.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			float? size = cap.Size;
			if (size != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "size", size, null);
			}
			return dictionary;
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x001984AC File Offset: 0x001966AC
		private IDictionary<string, object> SerializeTrack(Track track)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color color = track.Color;
			if (color != Color.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "color", ColorTranslator.ToHtml(color), "");
			}
			float? size = track.Size;
			if (size != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "size", size, null);
			}
			float opacity = track.Opacity;
			if (opacity != 1f)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "opacity", opacity, 1);
			}
			bool visible = track.Visible;
			if (!visible)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "visible", visible, true);
			}
			return dictionary;
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x00198558 File Offset: 0x00196758
		private IDictionary<string, object> SerializeAppearance(Appearance appearance)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color backgroundColor = appearance.BackgroundColor;
			if (backgroundColor != Color.White)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "background", ColorTranslator.ToHtml(backgroundColor), "White");
			}
			return dictionary;
		}
	}
}
