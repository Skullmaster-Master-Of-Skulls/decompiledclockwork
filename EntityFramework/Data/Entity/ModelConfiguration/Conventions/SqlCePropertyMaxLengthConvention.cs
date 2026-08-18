using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000803 RID: 2051
	public class SqlCePropertyMaxLengthConvention : IConceptualModelConvention<EntityType>, IConceptualModelConvention<ComplexType>, IConvention
	{
		// Token: 0x06005C7D RID: 23677 RVA: 0x0018F5F9 File Offset: 0x0018D7F9
		public SqlCePropertyMaxLengthConvention() : this(4000)
		{
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x0018F606 File Offset: 0x0018D806
		public SqlCePropertyMaxLengthConvention(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", Strings.InvalidMaxLengthSize);
			}
			this._length = length;
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x0018F62C File Offset: 0x0018D82C
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			DbProviderInfo providerInfo = model.ProviderInfo;
			if (providerInfo != null && providerInfo.IsSqlCe())
			{
				this.SetLength(item.DeclaredProperties);
			}
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x0018F670 File Offset: 0x0018D870
		public virtual void Apply(ComplexType item, DbModel model)
		{
			Check.NotNull<ComplexType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			DbProviderInfo providerInfo = model.ProviderInfo;
			if (providerInfo != null && providerInfo.IsSqlCe())
			{
				this.SetLength(item.Properties);
			}
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x0018F6B4 File Offset: 0x0018D8B4
		private void SetLength(IEnumerable<EdmProperty> properties)
		{
			foreach (EdmProperty edmProperty in properties)
			{
				if (edmProperty.IsPrimitiveType && (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String) || edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Binary)))
				{
					this.SetDefaults(edmProperty);
				}
			}
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x0018F724 File Offset: 0x0018D924
		private void SetDefaults(EdmProperty property)
		{
			if (property.MaxLength == null && !property.IsMaxLength)
			{
				property.MaxLength = new int?(this._length);
			}
		}

		// Token: 0x040024AF RID: 9391
		private const int DefaultLength = 4000;

		// Token: 0x040024B0 RID: 9392
		private readonly int _length;
	}
}
