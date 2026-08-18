using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Internal;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.EntityClient
{
	// Token: 0x0200011C RID: 284
	public sealed class EntityParameter : DbParameter, IDbDataParameter, IDataParameter
	{
		// Token: 0x06000EAB RID: 3755 RVA: 0x0003E8D8 File Offset: 0x0003CAD8
		private EntityParameter(EntityParameter source) : this()
		{
			EntityUtil.CheckArgumentNull<EntityParameter>(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003E919 File Offset: 0x0003CB19
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0003E921 File Offset: 0x0003CB21
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003E92C File Offset: 0x0003CB2C
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0003E946 File Offset: 0x0003CB46
		[RefreshProperties(RefreshProperties.All)]
		[EntityResCategory("DataCategory_Data")]
		[EntityResDescription("DbParameter_Direction")]
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw EntityUtil.InvalidParameterDirection(value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003E970 File Offset: 0x0003CB70
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0003E99A File Offset: 0x0003CB9A
		public override bool IsNullable
		{
			get
			{
				return this._isNullable == null || this._isNullable.Value;
			}
			set
			{
				this._isNullable = new bool?(value);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000173E2 File Offset: 0x000155E2
		internal int Offset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		[EntityResCategory("DataCategory_Data")]
		[EntityResDescription("DbParameter_Size")]
		public override int Size
		{
			get
			{
				int num = (this._size != null) ? this._size.Value : 0;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size == null || this._size.Value != value)
				{
					if (value < -1)
					{
						throw EntityUtil.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					if (value == 0)
					{
						this._size = null;
						return;
					}
					this._size = new int?(value);
				}
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003EA39 File Offset: 0x0003CC39
		private void ResetSize()
		{
			if (this._size != null)
			{
				this.PropertyChanging();
				this._size = null;
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003EA5A File Offset: 0x0003CC5A
		private bool ShouldSerializeSize()
		{
			return this._size != null && this._size.Value != 0;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0003EA7C File Offset: 0x0003CC7C
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0003EA9A File Offset: 0x0003CC9A
		[EntityResCategory("DataCategory_Update")]
		[EntityResDescription("DbParameter_SourceColumn")]
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return string.Empty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0003EAA3 File Offset: 0x0003CCA3
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x0003EAAB File Offset: 0x0003CCAB
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0003EAB4 File Offset: 0x0003CCB4
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0003EAD2 File Offset: 0x0003CCD2
		[EntityResCategory("DataCategory_Update")]
		[EntityResDescription("DbParameter_SourceVersion")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_32;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_32;
				}
				this._sourceVersion = value;
				return;
				IL_32:
				throw EntityUtil.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003EB0C File Offset: 0x0003CD0C
		private void CloneHelperCore(EntityParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003EB6D File Offset: 0x0003CD6D
		internal void CopyTo(DbParameter destination)
		{
			EntityUtil.CheckArgumentNull<DbParameter>(destination, "destination");
			this.CloneHelper((EntityParameter)destination);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003EB88 File Offset: 0x0003CD88
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003EBA8 File Offset: 0x0003CDA8
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003EBB1 File Offset: 0x0003CDB1
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003EBBC File Offset: 0x0003CDBC
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003EBE6 File Offset: 0x0003CDE6
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003EC0C File Offset: 0x0003CE0C
		private int ValueSizeCore(object value)
		{
			if (!EntityUtil.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003EC61 File Offset: 0x0003CE61
		public EntityParameter()
		{
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003EC69 File Offset: 0x0003CE69
		public EntityParameter(string parameterName, DbType dbType)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003EC84 File Offset: 0x0003CE84
		public EntityParameter(string parameterName, DbType dbType, int size)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003ECA6 File Offset: 0x0003CEA6
		public EntityParameter(string parameterName, DbType dbType, int size, string sourceColumn)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003ECD0 File Offset: 0x0003CED0
		public EntityParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.Value = value;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003ED35 File Offset: 0x0003CF35
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x0003ED46 File Offset: 0x0003CF46
		public override string ParameterName
		{
			get
			{
				return this._parameterName ?? "";
			}
			set
			{
				this.SetParameterNameWithValidation(value, "value");
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003ED54 File Offset: 0x0003CF54
		private void SetParameterNameWithValidation(string parameterName, string argumentName)
		{
			if (!string.IsNullOrEmpty(parameterName) && !DbCommandTree.IsValidParameterName(parameterName))
			{
				throw EntityUtil.Argument(Strings.EntityClient_InvalidParameterName(parameterName), argumentName);
			}
			this.PropertyChanging();
			this._parameterName = parameterName;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003ED80 File Offset: 0x0003CF80
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0003EDF8 File Offset: 0x0003CFF8
		public override DbType DbType
		{
			get
			{
				if (this._dbType == null)
				{
					if (this._edmType != null)
					{
						return EntityParameter.GetDbTypeFromEdm(this._edmType);
					}
					if (this._value == null)
					{
						return DbType.String;
					}
					try
					{
						return TypeHelpers.ConvertClrTypeToDbType(this._value.GetType());
					}
					catch (ArgumentException inner)
					{
						throw EntityUtil.InvalidOperation(Strings.EntityClient_CannotDeduceDbType, inner);
					}
				}
				return this._dbType.Value;
			}
			set
			{
				this.PropertyChanging();
				this._dbType = new DbType?(value);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003EE0C File Offset: 0x0003D00C
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x0003EE14 File Offset: 0x0003D014
		public EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
			set
			{
				if (value != null && !Helper.IsScalarType(value))
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_EntityParameterEdmTypeNotScalar(value.FullName));
				}
				this.PropertyChanging();
				this._edmType = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0003EE40 File Offset: 0x0003D040
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x0003EE6A File Offset: 0x0003D06A
		public new byte Precision
		{
			get
			{
				return (this._precision != null) ? this._precision.Value : 0;
			}
			set
			{
				this.PropertyChanging();
				this._precision = new byte?(value);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003EE80 File Offset: 0x0003D080
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0003EEAA File Offset: 0x0003D0AA
		public new byte Scale
		{
			get
			{
				return (this._scale != null) ? this._scale.Value : 0;
			}
			set
			{
				this.PropertyChanging();
				this._scale = new byte?(value);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003EEBE File Offset: 0x0003D0BE
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0003EEC8 File Offset: 0x0003D0C8
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._dbType == null && this._edmType == null)
				{
					DbType dbType = DbType.String;
					if (this._value != null)
					{
						dbType = TypeHelpers.ConvertClrTypeToDbType(this._value.GetType());
					}
					DbType dbType2 = DbType.String;
					if (value != null)
					{
						dbType2 = TypeHelpers.ConvertClrTypeToDbType(value.GetType());
					}
					if (dbType != dbType2)
					{
						this.PropertyChanging();
					}
				}
				this._value = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003EF29 File Offset: 0x0003D129
		internal bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0003EF31 File Offset: 0x0003D131
		internal bool IsDbTypeSpecified
		{
			get
			{
				return this._dbType != null;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003EF3E File Offset: 0x0003D13E
		internal bool IsDirectionSpecified
		{
			get
			{
				return this._direction > (ParameterDirection)0;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003EF49 File Offset: 0x0003D149
		internal bool IsIsNullableSpecified
		{
			get
			{
				return this._isNullable != null;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003EF56 File Offset: 0x0003D156
		internal bool IsPrecisionSpecified
		{
			get
			{
				return this._precision != null;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0003EF63 File Offset: 0x0003D163
		internal bool IsScaleSpecified
		{
			get
			{
				return this._scale != null;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003EF70 File Offset: 0x0003D170
		internal bool IsSizeSpecified
		{
			get
			{
				return this._size != null;
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003EF7D File Offset: 0x0003D17D
		public override void ResetDbType()
		{
			if (this._dbType != null || this._edmType != null)
			{
				this.PropertyChanging();
			}
			this._edmType = null;
			this._dbType = null;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003EFAD File Offset: 0x0003D1AD
		internal EntityParameter Clone()
		{
			return new EntityParameter(this);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003EFB8 File Offset: 0x0003D1B8
		private void CloneHelper(EntityParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._parameterName = this._parameterName;
			destination._dbType = this._dbType;
			destination._edmType = this._edmType;
			destination._precision = this._precision;
			destination._scale = this._scale;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003F008 File Offset: 0x0003D208
		private void PropertyChanging()
		{
			this._isDirty = true;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003F011 File Offset: 0x0003D211
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003F01C File Offset: 0x0003D21C
		internal TypeUsage GetTypeUsage()
		{
			if (!this.IsTypeConsistent)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_EntityParameterInconsistentEdmType(this._edmType.FullName, this._parameterName));
			}
			TypeUsage result;
			if (this._edmType != null)
			{
				result = TypeUsage.Create(this._edmType);
			}
			else if (!DbTypeMap.TryGetModelTypeUsage(this.DbType, out result))
			{
				PrimitiveType primitiveType;
				if (this.DbType != DbType.Object || this.Value == null || !ClrProviderManifest.Instance.TryGetPrimitiveType(this.Value.GetType(), out primitiveType) || !Helper.IsSpatialType(primitiveType))
				{
					throw EntityUtil.InvalidOperation(Strings.EntityClient_UnsupportedDbType(this.DbType.ToString(), this.ParameterName));
				}
				result = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return result;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003F0DE File Offset: 0x0003D2DE
		internal void ResetIsDirty()
		{
			this._isDirty = false;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0003F0E8 File Offset: 0x0003D2E8
		private bool IsTypeConsistent
		{
			get
			{
				if (this._edmType == null || this._dbType == null)
				{
					return true;
				}
				DbType dbTypeFromEdm = EntityParameter.GetDbTypeFromEdm(this._edmType);
				DbType? dbType;
				DbType dbType2;
				if (dbTypeFromEdm == DbType.String)
				{
					dbType = this._dbType;
					dbType2 = DbType.String;
					if (!(dbType.GetValueOrDefault() == dbType2 & dbType != null))
					{
						dbType = this._dbType;
						dbType2 = DbType.AnsiString;
						if (!(dbType.GetValueOrDefault() == dbType2 & dbType != null) && dbTypeFromEdm != DbType.AnsiStringFixedLength)
						{
							return dbTypeFromEdm == DbType.StringFixedLength;
						}
					}
					return true;
				}
				dbType = this._dbType;
				dbType2 = dbTypeFromEdm;
				return dbType.GetValueOrDefault() == dbType2 & dbType != null;
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003F184 File Offset: 0x0003D384
		private static DbType GetDbTypeFromEdm(EdmType edmType)
		{
			PrimitiveType type = Helper.AsPrimitive(edmType);
			if (Helper.IsSpatialType(type))
			{
				return DbType.Object;
			}
			DbType result;
			if (DbCommandDefinition.TryGetDbTypeFromPrimitiveType(type, out result))
			{
				return result;
			}
			return DbType.AnsiString;
		}

		// Token: 0x040009E5 RID: 2533
		private object _value;

		// Token: 0x040009E6 RID: 2534
		private object _parent;

		// Token: 0x040009E7 RID: 2535
		private ParameterDirection _direction;

		// Token: 0x040009E8 RID: 2536
		private int? _size;

		// Token: 0x040009E9 RID: 2537
		private string _sourceColumn;

		// Token: 0x040009EA RID: 2538
		private DataRowVersion _sourceVersion;

		// Token: 0x040009EB RID: 2539
		private bool _sourceColumnNullMapping;

		// Token: 0x040009EC RID: 2540
		private bool? _isNullable;

		// Token: 0x040009ED RID: 2541
		private object _coercedValue;

		// Token: 0x040009EE RID: 2542
		private string _parameterName;

		// Token: 0x040009EF RID: 2543
		private DbType? _dbType;

		// Token: 0x040009F0 RID: 2544
		private EdmType _edmType;

		// Token: 0x040009F1 RID: 2545
		private byte? _precision;

		// Token: 0x040009F2 RID: 2546
		private byte? _scale;

		// Token: 0x040009F3 RID: 2547
		private bool _isDirty;
	}
}
