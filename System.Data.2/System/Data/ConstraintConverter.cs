using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x0200009F RID: 159
	internal sealed class ConstraintConverter : ExpandableObjectConverter
	{
		// Token: 0x06000824 RID: 2084 RVA: 0x00057C1C File Offset: 0x0005701C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00057C48 File Offset: 0x00057048
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Constraint)
			{
				if (value is UniqueConstraint)
				{
					UniqueConstraint uniqueConstraint = (UniqueConstraint)value;
					ConstructorInfo constructor = typeof(UniqueConstraint).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string[]),
						typeof(bool)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							uniqueConstraint.ConstraintName,
							uniqueConstraint.ColumnNames,
							uniqueConstraint.IsPrimaryKey
						});
					}
				}
				else
				{
					ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)value;
					ConstructorInfo constructor2 = typeof(ForeignKeyConstraint).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(AcceptRejectRule),
						typeof(Rule),
						typeof(Rule)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[]
						{
							foreignKeyConstraint.ConstraintName,
							foreignKeyConstraint.ParentKey.Table.TableName,
							foreignKeyConstraint.ParentColumnNames,
							foreignKeyConstraint.ChildColumnNames,
							foreignKeyConstraint.AcceptRejectRule,
							foreignKeyConstraint.DeleteRule,
							foreignKeyConstraint.UpdateRule
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
