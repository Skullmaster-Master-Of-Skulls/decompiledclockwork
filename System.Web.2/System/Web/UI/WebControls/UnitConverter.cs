using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000507 RID: 1287
	public class UnitConverter : TypeConverter
	{
		// Token: 0x060040F7 RID: 16631 RVA: 0x000A017D File Offset: 0x0009E37D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000A01E8 File Offset: 0x0009E3E8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x000D478C File Offset: 0x000D298C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text2 = text.Trim();
			if (text2.Length == 0)
			{
				return Unit.Empty;
			}
			if (culture != null)
			{
				return Unit.Parse(text2, culture);
			}
			return Unit.Parse(text2, CultureInfo.CurrentCulture);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000D47EC File Offset: 0x000D29EC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				if (value == null || ((Unit)value).IsEmpty)
				{
					return string.Empty;
				}
				return ((Unit)value).ToString(culture);
			}
			else
			{
				if (!(destinationType == typeof(InstanceDescriptor)) || value == null)
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				Unit unit = (Unit)value;
				object[] arguments = null;
				MemberInfo memberInfo;
				if (unit.IsEmpty)
				{
					memberInfo = typeof(Unit).GetField("Empty");
				}
				else
				{
					memberInfo = typeof(Unit).GetConstructor(new Type[]
					{
						typeof(double),
						typeof(UnitType)
					});
					arguments = new object[]
					{
						unit.Value,
						unit.Type
					};
				}
				if (memberInfo != null)
				{
					return new InstanceDescriptor(memberInfo, arguments);
				}
				return null;
			}
		}
	}
}
