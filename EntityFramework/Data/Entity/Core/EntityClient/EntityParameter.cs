using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x0200033D RID: 829
	public class EntityParameter : DbParameter, IDbDataParameter, IDataParameter
	{
		// Token: 0x06001D67 RID: 7527 RVA: 0x0008E667 File Offset: 0x0008C867
		public EntityParameter()
		{
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0008E66F File Offset: 0x0008C86F
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EntityParameter(string parameterName, DbType dbType)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0008E68A File Offset: 0x0008C88A
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EntityParameter(string parameterName, DbType dbType, int size)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0008E6AC File Offset: 0x0008C8AC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EntityParameter(string parameterName, DbType dbType, int size, string sourceColumn)
		{
			this.SetParameterNameWithValidation(parameterName, "parameterName");
			this.DbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0008E6D8 File Offset: 0x0008C8D8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

		// Token: 0x06001D6C RID: 7532 RVA: 0x0008E740 File Offset: 0x0008C940
		private EntityParameter(EntityParameter source) : this()
		{
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0008E775 File Offset: 0x0008C975
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0008E786 File Offset: 0x0008C986
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

		// Token: 0x06001D6F RID: 7535 RVA: 0x0008E794 File Offset: 0x0008C994
		private void SetParameterNameWithValidation(string parameterName, string argumentName)
		{
			if (!string.IsNullOrEmpty(parameterName) && !DbCommandTree.IsValidParameterName(parameterName))
			{
				throw new ArgumentException(Strings.EntityClient_InvalidParameterName(parameterName), argumentName);
			}
			this.PropertyChanging();
			this._parameterName = parameterName;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0008E7C0 File Offset: 0x0008C9C0
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0008E838 File Offset: 0x0008CA38
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
					catch (ArgumentException innerException)
					{
						throw new InvalidOperationException(Strings.EntityClient_CannotDeduceDbType, innerException);
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0008E84C File Offset: 0x0008CA4C
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x0008E854 File Offset: 0x0008CA54
		public virtual EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
			set
			{
				if (value != null && !Helper.IsScalarType(value))
				{
					throw new InvalidOperationException(Strings.EntityClient_EntityParameterEdmTypeNotScalar(value.FullName));
				}
				this.PropertyChanging();
				this._edmType = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0008E880 File Offset: 0x0008CA80
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x0008E8AA File Offset: 0x0008CAAA
		public new virtual byte Precision
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

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0008E8C0 File Offset: 0x0008CAC0
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0008E8EA File Offset: 0x0008CAEA
		public new virtual byte Scale
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0008E8FE File Offset: 0x0008CAFE
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0008E908 File Offset: 0x0008CB08
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

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0008E969 File Offset: 0x0008CB69
		internal virtual bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0008E971 File Offset: 0x0008CB71
		internal virtual bool IsDbTypeSpecified
		{
			get
			{
				return this._dbType != null;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0008E97E File Offset: 0x0008CB7E
		internal virtual bool IsDirectionSpecified
		{
			get
			{
				return this._direction != (ParameterDirection)0;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0008E98C File Offset: 0x0008CB8C
		internal virtual bool IsIsNullableSpecified
		{
			get
			{
				return this._isNullable != null;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0008E999 File Offset: 0x0008CB99
		internal virtual bool IsPrecisionSpecified
		{
			get
			{
				return this._precision != null;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0008E9A6 File Offset: 0x0008CBA6
		internal virtual bool IsScaleSpecified
		{
			get
			{
				return this._scale != null;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0008E9B3 File Offset: 0x0008CBB3
		internal virtual bool IsSizeSpecified
		{
			get
			{
				return this._size != null;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0008E9C0 File Offset: 0x0008CBC0
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0008E9DC File Offset: 0x0008CBDC
		[EntityResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
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
				if (this._direction != value)
				{
					switch (value)
					{
					case ParameterDirection.Input:
					case ParameterDirection.Output:
					case ParameterDirection.InputOutput:
					case ParameterDirection.ReturnValue:
						this.PropertyChanging();
						this._direction = value;
						return;
					}
					string name = typeof(ParameterDirection).Name;
					object name2 = typeof(ParameterDirection).Name;
					int num = (int)value;
					throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0008EA5C File Offset: 0x0008CC5C
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0008EA86 File Offset: 0x0008CC86
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0008EA94 File Offset: 0x0008CC94
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0008EAD0 File Offset: 0x0008CCD0
		[EntityResDescription("DbParameter_Size")]
		[EntityResCategory("DataCategory_Data")]
		public override int Size
		{
			get
			{
				int num = (this._size != null) ? this._size.Value : 0;
				if (num == 0)
				{
					num = EntityParameter.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size == null || this._size.Value != value)
				{
					if (value < -1)
					{
						throw new ArgumentException(Strings.ADP_InvalidSizeValue(value.ToString(CultureInfo.InvariantCulture)));
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0008EB38 File Offset: 0x0008CD38
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0008EB56 File Offset: 0x0008CD56
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0008EB5F File Offset: 0x0008CD5F
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0008EB67 File Offset: 0x0008CD67
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0008EB70 File Offset: 0x0008CD70
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0008EB90 File Offset: 0x0008CD90
		[EntityResDescription("DbParameter_SourceVersion")]
		[EntityResCategory("DataCategory_Update")]
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
						goto IL_34;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_34;
				}
				this._sourceVersion = value;
				return;
				IL_34:
				string name = typeof(DataRowVersion).Name;
				object name2 = typeof(DataRowVersion).Name;
				int num = (int)value;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0008EC07 File Offset: 0x0008CE07
		public override void ResetDbType()
		{
			if (this._dbType != null || this._edmType != null)
			{
				this.PropertyChanging();
			}
			this._edmType = null;
			this._dbType = null;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0008EC37 File Offset: 0x0008CE37
		private void PropertyChanging()
		{
			this._isDirty = true;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0008EC40 File Offset: 0x0008CE40
		private static int ValueSize(object value)
		{
			return EntityParameter.ValueSizeCore(value);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0008EC48 File Offset: 0x0008CE48
		internal virtual EntityParameter Clone()
		{
			return new EntityParameter(this);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0008EC50 File Offset: 0x0008CE50
		private void CloneHelper(EntityParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
			destination._parameterName = this._parameterName;
			destination._dbType = this._dbType;
			destination._edmType = this._edmType;
			destination._precision = this._precision;
			destination._scale = this._scale;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0008ECF0 File Offset: 0x0008CEF0
		internal virtual TypeUsage GetTypeUsage()
		{
			if (!this.IsTypeConsistent)
			{
				throw new InvalidOperationException(Strings.EntityClient_EntityParameterInconsistentEdmType(this._edmType.FullName, this._parameterName));
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
					throw new InvalidOperationException(Strings.EntityClient_UnsupportedDbType(this.DbType.ToString(), this.ParameterName));
				}
				result = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return result;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0008EDAE File Offset: 0x0008CFAE
		internal virtual void ResetIsDirty()
		{
			this._isDirty = false;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0008EDB8 File Offset: 0x0008CFB8
		private bool IsTypeConsistent
		{
			get
			{
				if (this._edmType == null || this._dbType == null)
				{
					return true;
				}
				DbType dbTypeFromEdm = EntityParameter.GetDbTypeFromEdm(this._edmType);
				if (dbTypeFromEdm == DbType.String)
				{
					return this._dbType == DbType.String || this._dbType == DbType.AnsiString || dbTypeFromEdm == DbType.AnsiStringFixedLength || dbTypeFromEdm == DbType.StringFixedLength;
				}
				return this._dbType == dbTypeFromEdm;
			}
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0008EE58 File Offset: 0x0008D058
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

		// Token: 0x06001D96 RID: 7574 RVA: 0x0008EE84 File Offset: 0x0008D084
		private void ResetSize()
		{
			if (this._size != null)
			{
				this.PropertyChanging();
				this._size = null;
			}
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0008EEA5 File Offset: 0x0008D0A5
		private bool ShouldSerializeSize()
		{
			return this._size != null && this._size.Value != 0;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0008EEC7 File Offset: 0x0008D0C7
		internal virtual void CopyTo(DbParameter destination)
		{
			this.CloneHelper((EntityParameter)destination);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0008EED8 File Offset: 0x0008D0D8
		internal virtual object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0008EEF8 File Offset: 0x0008D0F8
		internal virtual void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0008EF01 File Offset: 0x0008D101
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0008EF0C File Offset: 0x0008D10C
		private static int ValueSizeCore(object value)
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

		// Token: 0x04000A0B RID: 2571
		private string _parameterName;

		// Token: 0x04000A0C RID: 2572
		private DbType? _dbType;

		// Token: 0x04000A0D RID: 2573
		private EdmType _edmType;

		// Token: 0x04000A0E RID: 2574
		private byte? _precision;

		// Token: 0x04000A0F RID: 2575
		private byte? _scale;

		// Token: 0x04000A10 RID: 2576
		private bool _isDirty;

		// Token: 0x04000A11 RID: 2577
		private object _value;

		// Token: 0x04000A12 RID: 2578
		private object _parent;

		// Token: 0x04000A13 RID: 2579
		private ParameterDirection _direction;

		// Token: 0x04000A14 RID: 2580
		private int? _size;

		// Token: 0x04000A15 RID: 2581
		private string _sourceColumn;

		// Token: 0x04000A16 RID: 2582
		private DataRowVersion _sourceVersion;

		// Token: 0x04000A17 RID: 2583
		private bool _sourceColumnNullMapping;

		// Token: 0x04000A18 RID: 2584
		private bool? _isNullable;
	}
}
