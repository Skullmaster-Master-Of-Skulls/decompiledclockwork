using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x02000668 RID: 1640
	public sealed class RegularExpressionAttributeAdapter : DataAnnotationsModelValidator<RegularExpressionAttribute>
	{
		// Token: 0x0600504C RID: 20556 RVA: 0x00115536 File Offset: 0x00113736
		public RegularExpressionAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RegularExpressionAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x00115541 File Offset: 0x00113741
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			return base.GetLocalizedString(errorMessage, new object[]
			{
				base.Metadata.GetDisplayName(),
				base.Attribute.Pattern
			});
		}
	}
}
