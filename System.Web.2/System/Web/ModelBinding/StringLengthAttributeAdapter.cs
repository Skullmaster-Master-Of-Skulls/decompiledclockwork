using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x0200066A RID: 1642
	public sealed class StringLengthAttributeAdapter : DataAnnotationsModelValidator<StringLengthAttribute>
	{
		// Token: 0x0600504F RID: 20559 RVA: 0x00115577 File Offset: 0x00113777
		public StringLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, StringLengthAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00115584 File Offset: 0x00113784
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			return base.GetLocalizedString(errorMessage, new object[]
			{
				base.Metadata.GetDisplayName(),
				base.Attribute.MinimumLength,
				base.Attribute.MaximumLength
			});
		}
	}
}
