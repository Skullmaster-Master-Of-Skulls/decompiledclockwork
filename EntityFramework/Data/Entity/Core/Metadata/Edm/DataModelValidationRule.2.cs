using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000017 RID: 23
	internal abstract class DataModelValidationRule<TItem> : DataModelValidationRule where TItem : class
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004E28 File Offset: 0x00003028
		internal DataModelValidationRule(Action<EdmModelValidationContext, TItem> validate)
		{
			this._validate = validate;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004E37 File Offset: 0x00003037
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004E43 File Offset: 0x00003043
		internal override void Evaluate(EdmModelValidationContext context, MetadataItem item)
		{
			this._validate(context, item as TItem);
		}

		// Token: 0x04000026 RID: 38
		protected Action<EdmModelValidationContext, TItem> _validate;
	}
}
