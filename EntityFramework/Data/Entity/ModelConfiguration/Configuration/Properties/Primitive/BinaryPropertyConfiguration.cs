using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x020007D9 RID: 2009
	internal class BinaryPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06005B69 RID: 23401 RVA: 0x00189340 File Offset: 0x00187540
		// (set) Token: 0x06005B6A RID: 23402 RVA: 0x00189348 File Offset: 0x00187548
		public bool? IsRowVersion { get; set; }

		// Token: 0x06005B6B RID: 23403 RVA: 0x00189351 File Offset: 0x00187551
		public BinaryPropertyConfiguration()
		{
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x00189359 File Offset: 0x00187559
		private BinaryPropertyConfiguration(BinaryPropertyConfiguration source) : base(source)
		{
			this.IsRowVersion = source.IsRowVersion;
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x0018936E File Offset: 0x0018756E
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new BinaryPropertyConfiguration(this);
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x00189378 File Offset: 0x00187578
		protected override void ConfigureProperty(EdmProperty property)
		{
			if (this.IsRowVersion != null && this.IsRowVersion.Value)
			{
				base.ConcurrencyMode = new ConcurrencyMode?(base.ConcurrencyMode ?? System.Data.Entity.Core.Metadata.Edm.ConcurrencyMode.Fixed);
				base.DatabaseGeneratedOption = new DatabaseGeneratedOption?(base.DatabaseGeneratedOption ?? System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
				base.IsNullable = new bool?(base.IsNullable ?? false);
				base.MaxLength = new int?(base.MaxLength ?? 8);
			}
			base.ConfigureProperty(property);
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x00189448 File Offset: 0x00187648
		protected override void ConfigureColumn(EdmProperty column, EntityType table, DbProviderManifest providerManifest)
		{
			if (this.IsRowVersion != null && this.IsRowVersion.Value)
			{
				base.ColumnType = (base.ColumnType ?? "rowversion");
			}
			base.ConfigureColumn(column, table, providerManifest);
			if (this.IsRowVersion != null && this.IsRowVersion.Value)
			{
				column.MaxLength = null;
			}
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x001894C4 File Offset: 0x001876C4
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			if (binaryPropertyConfiguration != null)
			{
				this.IsRowVersion = binaryPropertyConfiguration.IsRowVersion;
			}
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x001894F0 File Offset: 0x001876F0
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			if (binaryPropertyConfiguration != null && this.IsRowVersion == null)
			{
				this.IsRowVersion = binaryPropertyConfiguration.IsRowVersion;
			}
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x0018952C File Offset: 0x0018772C
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			if (binaryPropertyConfiguration == null)
			{
				return;
			}
			if (binaryPropertyConfiguration.IsRowVersion != null)
			{
				this.IsRowVersion = null;
			}
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x0018956C File Offset: 0x0018776C
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			BinaryPropertyConfiguration binaryPropertyConfiguration = other as BinaryPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = binaryPropertyConfiguration == null || base.IsCompatible<bool, BinaryPropertyConfiguration>((BinaryPropertyConfiguration c) => c.IsRowVersion, binaryPropertyConfiguration, ref errorMessage);
			return flag && flag2;
		}
	}
}
