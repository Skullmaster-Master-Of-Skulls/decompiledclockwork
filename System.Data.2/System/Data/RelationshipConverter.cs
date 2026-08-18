using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x02000121 RID: 289
	internal sealed class RelationshipConverter : ExpandableObjectConverter
	{
		// Token: 0x06001163 RID: 4451 RVA: 0x00085BA8 File Offset: 0x00084FA8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00085BD4 File Offset: 0x00084FD4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is DataRelation)
			{
				DataRelation dataRelation = (DataRelation)value;
				DataTable table = dataRelation.ParentKey.Table;
				DataTable table2 = dataRelation.ChildKey.Table;
				ConstructorInfo constructor;
				object[] arguments;
				if (ADP.IsEmpty(table.Namespace) && ADP.IsEmpty(table2.Namespace))
				{
					constructor = typeof(DataRelation).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(bool)
					});
					arguments = new object[]
					{
						dataRelation.RelationName,
						dataRelation.ParentKey.Table.TableName,
						dataRelation.ChildKey.Table.TableName,
						dataRelation.ParentColumnNames,
						dataRelation.ChildColumnNames,
						dataRelation.Nested
					};
				}
				else
				{
					constructor = typeof(DataRelation).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(bool)
					});
					arguments = new object[]
					{
						dataRelation.RelationName,
						dataRelation.ParentKey.Table.TableName,
						dataRelation.ParentKey.Table.Namespace,
						dataRelation.ChildKey.Table.TableName,
						dataRelation.ChildKey.Table.Namespace,
						dataRelation.ParentColumnNames,
						dataRelation.ChildColumnNames,
						dataRelation.Nested
					};
				}
				return new InstanceDescriptor(constructor, arguments);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
