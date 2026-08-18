using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001774 RID: 6004
	internal class ElementShapeConverter : TypeConverter
	{
		// Token: 0x0600EA3A RID: 59962 RVA: 0x003559EB File Offset: 0x00353BEB
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600EA3B RID: 59963 RVA: 0x00355A09 File Offset: 0x00353C09
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600EA3C RID: 59964 RVA: 0x00355A28 File Offset: 0x00353C28
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (context != null && context.Container != null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = value as string;
			if (text != null)
			{
				if ("(none)".Equals(value))
				{
					return null;
				}
				try
				{
					string[] array = text.Split(new char[]
					{
						'|'
					});
					if (array.Length == 0 || string.IsNullOrEmpty(array[0]))
					{
						return null;
					}
					if (ElementShapeConverter.shownError)
					{
						return null;
					}
					ElementShape elementShape = null;
					Type type = null;
					if (type == null)
					{
						return null;
					}
					if (!typeof(ElementShape).IsAssignableFrom(type))
					{
						ElementShapeConverter.shownError = true;
					}
					else
					{
						elementShape = (ElementShape)Activator.CreateInstance(type);
						if (array.Length > 1)
						{
							elementShape.DeserializeProperties(array[1]);
						}
					}
					return elementShape;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error deserializing custom shape: " + ex.ToString());
					return null;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600EA3D RID: 59965 RVA: 0x00355B28 File Offset: 0x00353D28
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null && destinationType == typeof(string))
			{
				return "(none)";
			}
			if (value == null)
			{
				return null;
			}
			if (destinationType == typeof(string))
			{
				if (context != null && context.Container != null)
				{
					Component component = value as Component;
					if (component != null && component.Site != null)
					{
						return component.Site.Name;
					}
					return value.GetType().FullName;
				}
				else if (typeof(ElementShape).IsAssignableFrom(value.GetType()))
				{
					string text = ((ElementShape)value).SerializeProperties();
					return value.GetType().FullName + (string.IsNullOrEmpty(text) ? "" : ("|" + text));
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x04004381 RID: 17281
		private const string NONE_STRING = "(none)";

		// Token: 0x04004382 RID: 17282
		private static bool shownError;
	}
}
