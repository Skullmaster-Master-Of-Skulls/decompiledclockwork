using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017FE RID: 6142
	internal class UnitConverter : TypeConverter
	{
		// Token: 0x0600EEC9 RID: 61129 RVA: 0x00365D44 File Offset: 0x00363F44
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(double) || sourceType == typeof(int) || sourceType == typeof(Unit) || sourceType == typeof(Unit) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600EECA RID: 61130 RVA: 0x00365DB8 File Offset: 0x00363FB8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(double) || destinationType == typeof(int) || destinationType == typeof(Unit) || destinationType == typeof(Unit) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600EECB RID: 61131 RVA: 0x00365E2C File Offset: 0x0036402C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null)
			{
				Unit unit = value as Unit;
				if (unit != null)
				{
					return unit;
				}
				string text = value as string;
				if (text != null)
				{
					return new Unit(text);
				}
				if (value is Unit)
				{
					Unit unit2 = (Unit)value;
					UnitType type = unit2.Type;
					if (type == UnitType.Pixel || type != UnitType.Percentage)
					{
						return Unit.Pixel((float)((int)unit2.Value));
					}
					return Unit.Percentage((double)((int)unit2.Value));
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600EECC RID: 61132 RVA: 0x00365EA8 File Offset: 0x003640A8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Unit unit = value as Unit;
			if (unit != null)
			{
				if (destinationType == typeof(string))
				{
					return value.ToString();
				}
				if (destinationType == typeof(float))
				{
					return unit.Value;
				}
				if (destinationType == typeof(int))
				{
					return (int)unit.Value;
				}
				if (destinationType == typeof(Unit))
				{
					switch (unit.Type)
					{
					default:
						return Unit.Pixel((int)unit.Value);
					case UnitType.Percentage:
						return Unit.Percentage((double)((int)unit.Value));
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
