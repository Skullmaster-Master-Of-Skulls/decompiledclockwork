using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x020007D8 RID: 2008
	internal abstract class LengthPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005B5B RID: 23387 RVA: 0x00188F1C File Offset: 0x0018711C
		// (set) Token: 0x06005B5C RID: 23388 RVA: 0x00188F24 File Offset: 0x00187124
		public bool? IsFixedLength { get; set; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005B5D RID: 23389 RVA: 0x00188F2D File Offset: 0x0018712D
		// (set) Token: 0x06005B5E RID: 23390 RVA: 0x00188F35 File Offset: 0x00187135
		public int? MaxLength { get; set; }

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005B5F RID: 23391 RVA: 0x00188F3E File Offset: 0x0018713E
		// (set) Token: 0x06005B60 RID: 23392 RVA: 0x00188F46 File Offset: 0x00187146
		public bool? IsMaxLength { get; set; }

		// Token: 0x06005B61 RID: 23393 RVA: 0x00188F4F File Offset: 0x0018714F
		protected LengthPropertyConfiguration()
		{
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00188F57 File Offset: 0x00187157
		protected LengthPropertyConfiguration(LengthPropertyConfiguration source) : base(source)
		{
			Check.NotNull<LengthPropertyConfiguration>(source, "source");
			this.IsFixedLength = source.IsFixedLength;
			this.MaxLength = source.MaxLength;
			this.IsMaxLength = source.IsMaxLength;
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x00188F90 File Offset: 0x00187190
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			if (this.IsFixedLength != null)
			{
				property.IsFixedLength = this.IsFixedLength;
			}
			if (this.MaxLength != null)
			{
				property.MaxLength = this.MaxLength;
			}
			if (this.IsMaxLength != null)
			{
				property.IsMaxLength = this.IsMaxLength.Value;
			}
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x00189000 File Offset: 0x00187200
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName;
			if ((facetName = facetDescription.FacetName) != null)
			{
				if (facetName == "FixedLength")
				{
					bool? isFixedLength2;
					if (!facetDescription.IsConstant)
					{
						bool? isFixedLength = this.IsFixedLength;
						isFixedLength2 = ((isFixedLength != null) ? new bool?(isFixedLength.GetValueOrDefault()) : column.IsFixedLength);
					}
					else
					{
						isFixedLength2 = null;
					}
					column.IsFixedLength = isFixedLength2;
					return;
				}
				if (!(facetName == "MaxLength"))
				{
					return;
				}
				int? maxLength2;
				if (!facetDescription.IsConstant)
				{
					int? maxLength = this.MaxLength;
					maxLength2 = ((maxLength != null) ? new int?(maxLength.GetValueOrDefault()) : column.MaxLength);
				}
				else
				{
					maxLength2 = null;
				}
				column.MaxLength = maxLength2;
				column.IsMaxLength = (!facetDescription.IsConstant && (this.IsMaxLength ?? column.IsMaxLength));
			}
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x001890EC File Offset: 0x001872EC
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration != null)
			{
				this.IsFixedLength = lengthPropertyConfiguration.IsFixedLength;
				this.MaxLength = lengthPropertyConfiguration.MaxLength;
				this.IsMaxLength = lengthPropertyConfiguration.IsMaxLength;
			}
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x00189130 File Offset: 0x00187330
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration != null)
			{
				if (this.IsFixedLength == null)
				{
					this.IsFixedLength = lengthPropertyConfiguration.IsFixedLength;
				}
				if (this.MaxLength == null)
				{
					this.MaxLength = lengthPropertyConfiguration.MaxLength;
				}
				if (this.IsMaxLength == null)
				{
					this.IsMaxLength = lengthPropertyConfiguration.IsMaxLength;
				}
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x001891A4 File Offset: 0x001873A4
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration == null)
			{
				return;
			}
			if (lengthPropertyConfiguration.IsFixedLength != null)
			{
				this.IsFixedLength = null;
			}
			if (lengthPropertyConfiguration.MaxLength != null)
			{
				this.MaxLength = null;
			}
			if (lengthPropertyConfiguration.IsMaxLength != null)
			{
				this.IsMaxLength = null;
			}
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x00189224 File Offset: 0x00187424
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = lengthPropertyConfiguration == null || base.IsCompatible<bool, LengthPropertyConfiguration>((LengthPropertyConfiguration c) => c.IsFixedLength, lengthPropertyConfiguration, ref errorMessage);
			bool flag3 = lengthPropertyConfiguration == null || base.IsCompatible<bool, LengthPropertyConfiguration>((LengthPropertyConfiguration c) => c.IsMaxLength, lengthPropertyConfiguration, ref errorMessage);
			bool flag4 = lengthPropertyConfiguration == null || base.IsCompatible<int, LengthPropertyConfiguration>((LengthPropertyConfiguration c) => c.MaxLength, lengthPropertyConfiguration, ref errorMessage);
			return flag && flag2 && flag3 && flag4;
		}
	}
}
