using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Spatial;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Builders
{
	// Token: 0x020006CA RID: 1738
	public class ColumnBuilder
	{
		// Token: 0x060044F4 RID: 17652 RVA: 0x001452E8 File Offset: 0x001434E8
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel Binary(bool? nullable = null, int? maxLength = null, bool? fixedLength = null, byte[] defaultValue = null, string defaultValueSql = null, bool timestamp = false, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Binary, nullable, defaultValue, defaultValueSql, maxLength, null, null, null, fixedLength, false, timestamp, name, storeType, annotations);
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x00145328 File Offset: 0x00143528
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Boolean(bool? nullable = null, bool? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Boolean, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x0014537C File Offset: 0x0014357C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Byte(bool? nullable = null, bool identity = false, byte? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Byte, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x001453D0 File Offset: 0x001435D0
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel DateTime(bool? nullable = null, byte? precision = null, DateTime? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.DateTime, nullable, defaultValue, defaultValueSql, null, precision, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x0014541C File Offset: 0x0014361C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Decimal(bool? nullable = null, byte? precision = null, byte? scale = null, decimal? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool identity = false, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Decimal, nullable, defaultValue, defaultValueSql, null, precision, scale, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x00145464 File Offset: 0x00143664
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Double(bool? nullable = null, double? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Double, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x001454B8 File Offset: 0x001436B8
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Guid(bool? nullable = null, bool identity = false, Guid? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Guid, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x0014550C File Offset: 0x0014370C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Single(bool? nullable = null, float? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Single, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00145560 File Offset: 0x00143760
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel Short(bool? nullable = null, bool identity = false, short? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Int16, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x001455B4 File Offset: 0x001437B4
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Int(bool? nullable = null, bool identity = false, int? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Int32, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x00145608 File Offset: 0x00143808
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel Long(bool? nullable = null, bool identity = false, long? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Int64, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x0014565C File Offset: 0x0014385C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel String(bool? nullable = null, int? maxLength = null, bool? fixedLength = null, bool? unicode = null, string defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.String, nullable, defaultValue, defaultValueSql, maxLength, null, null, unicode, fixedLength, false, false, name, storeType, annotations);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x00145698 File Offset: 0x00143898
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Time(bool? nullable = null, byte? precision = null, TimeSpan? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Time, nullable, defaultValue, defaultValueSql, null, precision, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x001456E4 File Offset: 0x001438E4
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel DateTimeOffset(bool? nullable = null, byte? precision = null, DateTimeOffset? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.DateTimeOffset, nullable, defaultValue, defaultValueSql, null, precision, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x00145730 File Offset: 0x00143930
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public ColumnModel Geography(bool? nullable = null, DbGeography defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Geography, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x00145780 File Offset: 0x00143980
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public ColumnModel Geometry(bool? nullable = null, DbGeometry defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Geometry, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x001457D0 File Offset: 0x001439D0
		private static ColumnModel BuildColumn(PrimitiveTypeKind primitiveTypeKind, bool? nullable, object defaultValue, string defaultValueSql = null, int? maxLength = null, byte? precision = null, byte? scale = null, bool? unicode = null, bool? fixedLength = null, bool identity = false, bool timestamp = false, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return new ColumnModel(primitiveTypeKind)
			{
				IsNullable = nullable,
				MaxLength = maxLength,
				Precision = precision,
				Scale = scale,
				IsUnicode = unicode,
				IsFixedLength = fixedLength,
				IsIdentity = identity,
				DefaultValue = defaultValue,
				DefaultValueSql = defaultValueSql,
				IsTimestamp = timestamp,
				Name = name,
				StoreType = storeType,
				Annotations = annotations
			};
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x0014584C File Offset: 0x00143A4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00145854 File Offset: 0x00143A54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x0014585D File Offset: 0x00143A5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00145865 File Offset: 0x00143A65
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x0014586D File Offset: 0x00143A6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}
	}
}
