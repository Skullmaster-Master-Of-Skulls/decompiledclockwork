using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000679 RID: 1657
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UnitConverter : TypeConverter
	{
		// Token: 0x060051B9 RID: 20921 RVA: 0x0014A71E File Offset: 0x0014971E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0014A737 File Offset: 0x00149737
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0014A760 File Offset: 0x00149760
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

		// Token: 0x060051BC RID: 20924 RVA: 0x0014A7C0 File Offset: 0x001497C0
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
				if (destinationType != typeof(InstanceDescriptor) || value == null)
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
