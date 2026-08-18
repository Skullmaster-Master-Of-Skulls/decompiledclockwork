using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001B0 RID: 432
	public abstract class PropertyModel
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x0003F2AE File Offset: 0x0003D4AE
		protected PropertyModel(PrimitiveTypeKind type, TypeUsage typeUsage)
		{
			this._type = type;
			this._typeUsage = typeUsage;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0003F2C4 File Offset: 0x0003D4C4
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public virtual PrimitiveTypeKind Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003F2CC File Offset: 0x0003D4CC
		public TypeUsage TypeUsage
		{
			get
			{
				TypeUsage result;
				if ((result = this._typeUsage) == null)
				{
					result = (this._typeUsage = this.BuildTypeUsage());
				}
				return result;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0003F2F2 File Offset: 0x0003D4F2
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0003F2FA File Offset: 0x0003D4FA
		public virtual string Name { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003F303 File Offset: 0x0003D503
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0003F30B File Offset: 0x0003D50B
		public virtual string StoreType { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0003F314 File Offset: 0x0003D514
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0003F31C File Offset: 0x0003D51C
		public virtual int? MaxLength { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0003F325 File Offset: 0x0003D525
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0003F32D File Offset: 0x0003D52D
		public virtual byte? Precision { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0003F336 File Offset: 0x0003D536
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0003F33E File Offset: 0x0003D53E
		public virtual byte? Scale { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003F347 File Offset: 0x0003D547
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0003F34F File Offset: 0x0003D54F
		public virtual object DefaultValue { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003F358 File Offset: 0x0003D558
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0003F360 File Offset: 0x0003D560
		public virtual string DefaultValueSql { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0003F369 File Offset: 0x0003D569
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0003F371 File Offset: 0x0003D571
		public virtual bool? IsFixedLength { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003F37A File Offset: 0x0003D57A
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0003F382 File Offset: 0x0003D582
		public virtual bool? IsUnicode { get; set; }

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003F38C File Offset: 0x0003D58C
		private TypeUsage BuildTypeUsage()
		{
			PrimitiveType edmPrimitiveType = PrimitiveType.GetEdmPrimitiveType(this.Type);
			if (this.Type == PrimitiveTypeKind.Binary)
			{
				if (this.MaxLength != null)
				{
					return TypeUsage.CreateBinaryTypeUsage(edmPrimitiveType, this.IsFixedLength ?? false, this.MaxLength.Value);
				}
				return TypeUsage.CreateBinaryTypeUsage(edmPrimitiveType, this.IsFixedLength ?? false);
			}
			else if (this.Type == PrimitiveTypeKind.String)
			{
				if (this.MaxLength != null)
				{
					return TypeUsage.CreateStringTypeUsage(edmPrimitiveType, this.IsUnicode ?? true, this.IsFixedLength ?? false, this.MaxLength.Value);
				}
				return TypeUsage.CreateStringTypeUsage(edmPrimitiveType, this.IsUnicode ?? true, this.IsFixedLength ?? false);
			}
			else
			{
				if (this.Type == PrimitiveTypeKind.DateTime)
				{
					return TypeUsage.CreateDateTimeTypeUsage(edmPrimitiveType, this.Precision);
				}
				if (this.Type == PrimitiveTypeKind.DateTimeOffset)
				{
					return TypeUsage.CreateDateTimeOffsetTypeUsage(edmPrimitiveType, this.Precision);
				}
				if (this.Type == PrimitiveTypeKind.Decimal)
				{
					byte? precision = this.Precision;
					int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
					if (num == null)
					{
						byte? scale = this.Scale;
						int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
						if (num2 == null)
						{
							return TypeUsage.CreateDecimalTypeUsage(edmPrimitiveType);
						}
					}
					return TypeUsage.CreateDecimalTypeUsage(edmPrimitiveType, this.Precision ?? 18, this.Scale ?? 0);
				}
				if (this.Type != PrimitiveTypeKind.Time)
				{
					return TypeUsage.CreateDefaultTypeUsage(edmPrimitiveType);
				}
				return TypeUsage.CreateTimeTypeUsage(edmPrimitiveType, this.Precision);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003F5B8 File Offset: 0x0003D7B8
		internal virtual FacetValues ToFacetValues()
		{
			FacetValues facetValues = new FacetValues();
			if (this.DefaultValue != null)
			{
				facetValues.DefaultValue = this.DefaultValue;
			}
			if (this.IsFixedLength != null)
			{
				facetValues.FixedLength = new bool?(this.IsFixedLength.Value);
			}
			if (this.IsUnicode != null)
			{
				facetValues.Unicode = new bool?(this.IsUnicode.Value);
			}
			if (this.MaxLength != null)
			{
				facetValues.MaxLength = new int?(this.MaxLength.Value);
			}
			byte? precision = this.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				facetValues.Precision = new byte?(this.Precision.Value);
			}
			byte? scale = this.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				facetValues.Scale = new byte?(this.Scale.Value);
			}
			return facetValues;
		}

		// Token: 0x040003E9 RID: 1001
		private readonly PrimitiveTypeKind _type;

		// Token: 0x040003EA RID: 1002
		private TypeUsage _typeUsage;
	}
}
