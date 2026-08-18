using System;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Builders
{
	// Token: 0x020001A3 RID: 419
	public class ParameterBuilder
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Binary(int? maxLength = null, bool? fixedLength = null, byte[] defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Binary, defaultValue, defaultValueSql, maxLength, null, null, null, fixedLength, name, storeType, outParameter);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003E934 File Offset: 0x0003CB34
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Boolean(bool? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Boolean, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003E984 File Offset: 0x0003CB84
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel Byte(byte? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Byte, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003E9D4 File Offset: 0x0003CBD4
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel DateTime(byte? precision = null, DateTime? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.DateTime, defaultValue, defaultValueSql, null, precision, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003EA1C File Offset: 0x0003CC1C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel Decimal(byte? precision = null, byte? scale = null, decimal? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Decimal, defaultValue, defaultValueSql, null, precision, scale, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003EA5C File Offset: 0x0003CC5C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel Double(double? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Double, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003EAAC File Offset: 0x0003CCAC
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel Guid(Guid? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Guid, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003EAFC File Offset: 0x0003CCFC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Single(float? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Single, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003EB4C File Offset: 0x0003CD4C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Short(short? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Int16, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel Int(int? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Int32, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003EBEC File Offset: 0x0003CDEC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Long(long? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Int64, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel String(int? maxLength = null, bool? fixedLength = null, bool? unicode = null, string defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.String, defaultValue, defaultValueSql, maxLength, null, null, unicode, fixedLength, name, storeType, outParameter);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003EC74 File Offset: 0x0003CE74
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Time(byte? precision = null, TimeSpan? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Time, defaultValue, defaultValueSql, null, precision, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003ECBC File Offset: 0x0003CEBC
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel DateTimeOffset(byte? precision = null, DateTimeOffset? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.DateTimeOffset, defaultValue, defaultValueSql, null, precision, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003ED04 File Offset: 0x0003CF04
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ParameterModel Geography(DbGeography defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Geography, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003ED50 File Offset: 0x0003CF50
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ParameterModel Geometry(DbGeometry defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Geometry, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003ED9C File Offset: 0x0003CF9C
		private static ParameterModel BuildParameter(PrimitiveTypeKind primitiveTypeKind, object defaultValue, string defaultValueSql = null, int? maxLength = null, byte? precision = null, byte? scale = null, bool? unicode = null, bool? fixedLength = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return new ParameterModel(primitiveTypeKind)
			{
				MaxLength = maxLength,
				Precision = precision,
				Scale = scale,
				IsUnicode = unicode,
				IsFixedLength = fixedLength,
				DefaultValue = defaultValue,
				DefaultValueSql = defaultValueSql,
				Name = name,
				StoreType = storeType,
				IsOutParameter = outParameter
			};
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003EE00 File Offset: 0x0003D000
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003EE08 File Offset: 0x0003D008
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003EE11 File Offset: 0x0003D011
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003EE19 File Offset: 0x0003D019
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003EE21 File Offset: 0x0003D021
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}
	}
}
