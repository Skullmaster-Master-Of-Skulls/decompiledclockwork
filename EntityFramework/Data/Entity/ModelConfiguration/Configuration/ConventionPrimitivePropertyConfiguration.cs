using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002C2 RID: 706
	public class ConventionPrimitivePropertyConfiguration
	{
		// Token: 0x06001902 RID: 6402 RVA: 0x0007BD84 File Offset: 0x00079F84
		internal ConventionPrimitivePropertyConfiguration(PropertyInfo propertyInfo, Func<PrimitivePropertyConfiguration> configuration)
		{
			this._propertyInfo = propertyInfo;
			this._configuration = configuration;
			this._binaryConfiguration = new Lazy<BinaryPropertyConfiguration>(() => this._configuration() as BinaryPropertyConfiguration);
			this._dateTimeConfiguration = new Lazy<DateTimePropertyConfiguration>(() => this._configuration() as DateTimePropertyConfiguration);
			this._decimalConfiguration = new Lazy<DecimalPropertyConfiguration>(() => this._configuration() as DecimalPropertyConfiguration);
			this._lengthConfiguration = new Lazy<LengthPropertyConfiguration>(() => this._configuration() as LengthPropertyConfiguration);
			this._stringConfiguration = new Lazy<StringPropertyConfiguration>(() => this._configuration() as StringPropertyConfiguration);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0007BE3F File Offset: 0x0007A03F
		public virtual PropertyInfo ClrPropertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0007BE47 File Offset: 0x0007A047
		internal Func<PrimitivePropertyConfiguration> Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0007BE4F File Offset: 0x0007A04F
		public virtual ConventionPrimitivePropertyConfiguration HasColumnName(string columnName)
		{
			Check.NotEmpty(columnName, "columnName");
			if (this._configuration() != null && this._configuration().ColumnName == null)
			{
				this._configuration().ColumnName = columnName;
			}
			return this;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0007BE90 File Offset: 0x0007A090
		public virtual ConventionPrimitivePropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			if (this._configuration() != null && !this._configuration().Annotations.ContainsKey(name))
			{
				this._configuration().SetAnnotation(name, value);
			}
			return this;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0007BEE1 File Offset: 0x0007A0E1
		public virtual ConventionPrimitivePropertyConfiguration HasParameterName(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (this._configuration() != null && this._configuration().ParameterName == null)
			{
				this._configuration().ParameterName = parameterName;
			}
			return this;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0007BF20 File Offset: 0x0007A120
		public virtual ConventionPrimitivePropertyConfiguration HasColumnOrder(int columnOrder)
		{
			if (columnOrder < 0)
			{
				throw new ArgumentOutOfRangeException("columnOrder");
			}
			if (this._configuration() != null && this._configuration().ColumnOrder == null)
			{
				this._configuration().ColumnOrder = new int?(columnOrder);
			}
			return this;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0007BF7A File Offset: 0x0007A17A
		public virtual ConventionPrimitivePropertyConfiguration HasColumnType(string columnType)
		{
			Check.NotEmpty(columnType, "columnType");
			if (this._configuration() != null && this._configuration().ColumnType == null)
			{
				this._configuration().ColumnType = columnType;
			}
			return this;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0007BFB9 File Offset: 0x0007A1B9
		public virtual ConventionPrimitivePropertyConfiguration IsConcurrencyToken()
		{
			return this.IsConcurrencyToken(true);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0007BFC4 File Offset: 0x0007A1C4
		public virtual ConventionPrimitivePropertyConfiguration IsConcurrencyToken(bool concurrencyToken)
		{
			if (this._configuration() != null && this._configuration().ConcurrencyMode == null)
			{
				this._configuration().ConcurrencyMode = new ConcurrencyMode?(concurrencyToken ? ConcurrencyMode.Fixed : ConcurrencyMode.None);
			}
			return this;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0007C018 File Offset: 0x0007A218
		public virtual ConventionPrimitivePropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption databaseGeneratedOption)
		{
			if (!Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
			{
				throw new ArgumentOutOfRangeException("databaseGeneratedOption");
			}
			if (this._configuration() != null && this._configuration().DatabaseGeneratedOption == null)
			{
				this._configuration().DatabaseGeneratedOption = new DatabaseGeneratedOption?(databaseGeneratedOption);
			}
			return this;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0007C088 File Offset: 0x0007A288
		public virtual ConventionPrimitivePropertyConfiguration IsOptional()
		{
			if (this._configuration() != null && this._configuration().IsNullable == null)
			{
				if (!this._propertyInfo.PropertyType.IsNullable())
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonNullableProperty(this._propertyInfo.DeclaringType + "." + this._propertyInfo.Name, this._propertyInfo.PropertyType.Name));
				}
				this._configuration().IsNullable = new bool?(true);
			}
			return this;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0007C120 File Offset: 0x0007A320
		public virtual ConventionPrimitivePropertyConfiguration IsRequired()
		{
			if (this._configuration() != null && this._configuration().IsNullable == null)
			{
				this._configuration().IsNullable = new bool?(false);
			}
			return this;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0007C16B File Offset: 0x0007A36B
		public virtual ConventionPrimitivePropertyConfiguration IsUnicode()
		{
			return this.IsUnicode(true);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0007C174 File Offset: 0x0007A374
		public virtual ConventionPrimitivePropertyConfiguration IsUnicode(bool unicode)
		{
			if (this._configuration() != null)
			{
				if (this._stringConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_IsUnicodeNonString(this._propertyInfo.Name));
				}
				if (this._stringConfiguration.Value.IsUnicode == null)
				{
					this._stringConfiguration.Value.IsUnicode = new bool?(unicode);
				}
			}
			return this;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0007C1E4 File Offset: 0x0007A3E4
		public virtual ConventionPrimitivePropertyConfiguration IsFixedLength()
		{
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.IsFixedLength == null)
				{
					this._lengthConfiguration.Value.IsFixedLength = new bool?(true);
				}
			}
			return this;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0007C254 File Offset: 0x0007A454
		public virtual ConventionPrimitivePropertyConfiguration IsVariableLength()
		{
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.IsFixedLength == null)
				{
					this._lengthConfiguration.Value.IsFixedLength = new bool?(false);
				}
			}
			return this;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0007C2C4 File Offset: 0x0007A4C4
		public virtual ConventionPrimitivePropertyConfiguration HasMaxLength(int maxLength)
		{
			if (maxLength < 1)
			{
				throw new ArgumentOutOfRangeException("maxLength");
			}
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.MaxLength == null && this._lengthConfiguration.Value.IsMaxLength == null)
				{
					this._lengthConfiguration.Value.MaxLength = new int?(maxLength);
					if (this._lengthConfiguration.Value.IsFixedLength == null)
					{
						this._lengthConfiguration.Value.IsFixedLength = new bool?(false);
					}
				}
			}
			return this;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0007C390 File Offset: 0x0007A590
		public virtual ConventionPrimitivePropertyConfiguration IsMaxLength()
		{
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.IsMaxLength == null && this._lengthConfiguration.Value.MaxLength == null)
				{
					this._lengthConfiguration.Value.IsMaxLength = new bool?(true);
				}
			}
			return this;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0007C418 File Offset: 0x0007A618
		public virtual ConventionPrimitivePropertyConfiguration HasPrecision(byte value)
		{
			if (this._configuration() != null)
			{
				if (this._dateTimeConfiguration.Value == null)
				{
					if (this._decimalConfiguration.Value != null)
					{
						throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_DecimalNoScale(this._propertyInfo.Name));
					}
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime(this._propertyInfo.Name));
				}
				else
				{
					byte? precision = this._dateTimeConfiguration.Value.Precision;
					int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
					if (num == null)
					{
						this._dateTimeConfiguration.Value.Precision = new byte?(value);
					}
				}
			}
			return this;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0007C4D0 File Offset: 0x0007A6D0
		public virtual ConventionPrimitivePropertyConfiguration HasPrecision(byte precision, byte scale)
		{
			if (this._configuration() != null)
			{
				if (this._decimalConfiguration.Value == null)
				{
					if (this._dateTimeConfiguration.Value != null)
					{
						throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_DateTimeScale(this._propertyInfo.Name));
					}
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal(this._propertyInfo.Name));
				}
				else
				{
					byte? precision2 = this._decimalConfiguration.Value.Precision;
					int? num = (precision2 != null) ? new int?((int)precision2.GetValueOrDefault()) : null;
					if (num == null)
					{
						byte? scale2 = this._decimalConfiguration.Value.Scale;
						int? num2 = (scale2 != null) ? new int?((int)scale2.GetValueOrDefault()) : null;
						if (num2 == null)
						{
							this._decimalConfiguration.Value.Precision = new byte?(precision);
							this._decimalConfiguration.Value.Scale = new byte?(scale);
						}
					}
				}
			}
			return this;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0007C5D8 File Offset: 0x0007A7D8
		public virtual ConventionPrimitivePropertyConfiguration IsRowVersion()
		{
			if (this._configuration() != null)
			{
				if (this._binaryConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary(this._propertyInfo.Name));
				}
				if (this._binaryConfiguration.Value.IsRowVersion == null)
				{
					this._binaryConfiguration.Value.IsRowVersion = new bool?(true);
				}
			}
			return this;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0007C648 File Offset: 0x0007A848
		public virtual ConventionPrimitivePropertyConfiguration IsKey()
		{
			if (this._configuration() != null)
			{
				EntityTypeConfiguration entityTypeConfiguration = this._configuration().TypeConfiguration as EntityTypeConfiguration;
				if (entityTypeConfiguration != null && !entityTypeConfiguration.IsKeyConfigured)
				{
					entityTypeConfiguration.Key(this.ClrPropertyInfo);
				}
			}
			return this;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0007C690 File Offset: 0x0007A890
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0007C698 File Offset: 0x0007A898
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0007C6A1 File Offset: 0x0007A8A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0007C6A9 File Offset: 0x0007A8A9
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000895 RID: 2197
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x04000896 RID: 2198
		private readonly Func<PrimitivePropertyConfiguration> _configuration;

		// Token: 0x04000897 RID: 2199
		private readonly Lazy<BinaryPropertyConfiguration> _binaryConfiguration;

		// Token: 0x04000898 RID: 2200
		private readonly Lazy<DateTimePropertyConfiguration> _dateTimeConfiguration;

		// Token: 0x04000899 RID: 2201
		private readonly Lazy<DecimalPropertyConfiguration> _decimalConfiguration;

		// Token: 0x0400089A RID: 2202
		private readonly Lazy<LengthPropertyConfiguration> _lengthConfiguration;

		// Token: 0x0400089B RID: 2203
		private readonly Lazy<StringPropertyConfiguration> _stringConfiguration;
	}
}
