using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x02000136 RID: 310
	internal sealed class FieldDescriptor : PropertyDescriptor
	{
		// Token: 0x0600169D RID: 5789 RVA: 0x0004C0C4 File Offset: 0x0004A2C4
		internal FieldDescriptor(Type itemType, bool isReadOnly, EdmProperty property) : base(property.Name, null)
		{
			this._itemType = itemType;
			this._property = property;
			this._isReadOnly = isReadOnly;
			this._fieldType = this.DetermineClrType(this._property.TypeUsage);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004C100 File Offset: 0x0004A300
		private Type DetermineClrType(TypeUsage typeUsage)
		{
			Type type = null;
			EdmType edmType = typeUsage.EdmType;
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind == BuiltInTypeKind.ComplexType || builtInTypeKind == BuiltInTypeKind.EntityType)
					{
						type = edmType.ClrType;
					}
				}
				else
				{
					TypeUsage typeUsage2 = ((CollectionType)edmType).TypeUsage;
					type = this.DetermineClrType(typeUsage2);
					type = typeof(IEnumerable<>).MakeGenericType(new Type[]
					{
						type
					});
				}
			}
			else if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
			{
				if (builtInTypeKind == BuiltInTypeKind.EnumType || builtInTypeKind == BuiltInTypeKind.PrimitiveType)
				{
					type = edmType.ClrType;
					Facet facet;
					if (type.IsValueType && typeUsage.Facets.TryGetValue("Nullable", false, out facet) && (bool)facet.Value)
					{
						type = typeof(Nullable<>).MakeGenericType(new Type[]
						{
							type
						});
					}
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
			{
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					type = typeof(IDataRecord);
				}
			}
			else
			{
				type = typeof(EntityKey);
			}
			return type;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0004C209 File Offset: 0x0004A409
		internal EdmProperty EdmProperty
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0004C211 File Offset: 0x0004A411
		public override Type ComponentType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0004C219 File Offset: 0x0004A419
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0004C221 File Offset: 0x0004A421
		public override Type PropertyType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool CanResetValue(object item)
		{
			return false;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0004C22C File Offset: 0x0004A42C
		public override object GetValue(object item)
		{
			EntityUtil.CheckArgumentNull<object>(item, "item");
			if (!this._itemType.IsAssignableFrom(item.GetType()))
			{
				throw EntityUtil.IncompatibleArgument();
			}
			DbDataRecord dbDataRecord = item as DbDataRecord;
			object value;
			if (dbDataRecord != null)
			{
				value = dbDataRecord.GetValue(dbDataRecord.GetOrdinal(this._property.Name));
			}
			else
			{
				value = LightweightCodeGenerator.GetValue(this._property, item);
			}
			return value;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00013A81 File Offset: 0x00011C81
		public override void ResetValue(object item)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0004C290 File Offset: 0x0004A490
		public override void SetValue(object item, object value)
		{
			EntityUtil.CheckArgumentNull<object>(item, "item");
			if (!this._itemType.IsAssignableFrom(item.GetType()))
			{
				throw EntityUtil.IncompatibleArgument();
			}
			if (!this._isReadOnly)
			{
				LightweightCodeGenerator.SetValue(this._property, item, value);
				return;
			}
			throw EntityUtil.WriteOperationNotAllowedOnReadOnlyBindingList();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool ShouldSerializeValue(object item)
		{
			return false;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00017938 File Offset: 0x00015B38
		public override bool IsBrowsable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000A5C RID: 2652
		private readonly EdmProperty _property;

		// Token: 0x04000A5D RID: 2653
		private readonly Type _fieldType;

		// Token: 0x04000A5E RID: 2654
		private readonly Type _itemType;

		// Token: 0x04000A5F RID: 2655
		private readonly bool _isReadOnly;
	}
}
