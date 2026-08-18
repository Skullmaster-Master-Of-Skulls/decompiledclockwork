using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000570 RID: 1392
	internal sealed class FieldDescriptor : PropertyDescriptor
	{
		// Token: 0x06003647 RID: 13895 RVA: 0x00102EA8 File Offset: 0x001010A8
		internal FieldDescriptor(string propertyName) : base(propertyName, null)
		{
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x00102EB2 File Offset: 0x001010B2
		internal FieldDescriptor(Type itemType, bool isReadOnly, EdmProperty property) : base(property.Name, null)
		{
			this._itemType = itemType;
			this._property = property;
			this._isReadOnly = isReadOnly;
			this._fieldType = this.DetermineClrType(this._property.TypeUsage);
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x00102EF0 File Offset: 0x001010F0
		private Type DetermineClrType(TypeUsage typeUsage)
		{
			Type type = null;
			EdmType edmType = typeUsage.EdmType;
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EnumType)
			{
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.CollectionType:
				{
					TypeUsage typeUsage2 = ((CollectionType)edmType).TypeUsage;
					type = this.DetermineClrType(typeUsage2);
					return typeof(IEnumerable<>).MakeGenericType(new Type[]
					{
						type
					});
				}
				case BuiltInTypeKind.CollectionKind:
					return type;
				case BuiltInTypeKind.ComplexType:
					break;
				default:
					switch (builtInTypeKind)
					{
					case BuiltInTypeKind.EntityType:
						break;
					case BuiltInTypeKind.EnumType:
						goto IL_AE;
					default:
						return type;
					}
					break;
				}
				return edmType.ClrType;
			}
			if (builtInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				if (builtInTypeKind == BuiltInTypeKind.RefType)
				{
					return typeof(EntityKey);
				}
				if (builtInTypeKind != BuiltInTypeKind.RowType)
				{
					return type;
				}
				return typeof(IDataRecord);
			}
			IL_AE:
			type = edmType.ClrType;
			Facet facet;
			if (type.IsValueType() && typeUsage.Facets.TryGetValue("Nullable", false, out facet) && (bool)facet.Value)
			{
				type = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
			}
			return type;
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x00103009 File Offset: 0x00101209
		internal EdmProperty EdmProperty
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x00103011 File Offset: 0x00101211
		public override Type ComponentType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x00103019 File Offset: 0x00101219
		public override bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600364D RID: 13901 RVA: 0x00103021 File Offset: 0x00101221
		public override Type PropertyType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x00103029 File Offset: 0x00101229
		public override bool CanResetValue(object item)
		{
			return false;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x0010302C File Offset: 0x0010122C
		public override object GetValue(object item)
		{
			Check.NotNull<object>(item, "item");
			if (!this._itemType.IsAssignableFrom(item.GetType()))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
			}
			DbDataRecord dbDataRecord = item as DbDataRecord;
			object value;
			if (dbDataRecord != null)
			{
				value = dbDataRecord.GetValue(dbDataRecord.GetOrdinal(this._property.Name));
			}
			else
			{
				value = DelegateFactory.GetValue(this._property, item);
			}
			return value;
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x00103095 File Offset: 0x00101295
		public override void ResetValue(object item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x0010309C File Offset: 0x0010129C
		public override void SetValue(object item, object value)
		{
			Check.NotNull<object>(item, "item");
			if (!this._itemType.IsAssignableFrom(item.GetType()))
			{
				throw new ArgumentException(Strings.ObjectView_IncompatibleArgument);
			}
			if (!this._isReadOnly)
			{
				DelegateFactory.SetValue(this._property, item, value);
				return;
			}
			throw new InvalidOperationException(Strings.ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x001030F3 File Offset: 0x001012F3
		public override bool ShouldSerializeValue(object item)
		{
			return false;
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x001030F6 File Offset: 0x001012F6
		public override bool IsBrowsable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040014CA RID: 5322
		private readonly EdmProperty _property;

		// Token: 0x040014CB RID: 5323
		private readonly Type _fieldType;

		// Token: 0x040014CC RID: 5324
		private readonly Type _itemType;

		// Token: 0x040014CD RID: 5325
		private readonly bool _isReadOnly;
	}
}
