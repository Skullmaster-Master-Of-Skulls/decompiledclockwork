using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F6 RID: 2038
	public class DecimalPropertyConvention : IConceptualModelConvention<EdmProperty>, IConvention
	{
		// Token: 0x06005C4C RID: 23628 RVA: 0x0018E1E6 File Offset: 0x0018C3E6
		public DecimalPropertyConvention() : this(18, 2)
		{
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x0018E1F1 File Offset: 0x0018C3F1
		public DecimalPropertyConvention(byte precision, byte scale)
		{
			this._precision = precision;
			this._scale = scale;
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x0018E208 File Offset: 0x0018C408
		public virtual void Apply(EdmProperty item, DbModel model)
		{
			Check.NotNull<EdmProperty>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Decimal))
			{
				byte? precision = item.Precision;
				int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
				if (num == null)
				{
					item.Precision = new byte?(this._precision);
				}
				byte? scale = item.Scale;
				int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
				if (num2 == null)
				{
					item.Scale = new byte?(this._scale);
				}
			}
		}

		// Token: 0x0400249F RID: 9375
		private readonly byte _precision;

		// Token: 0x040024A0 RID: 9376
		private readonly byte _scale;
	}
}
