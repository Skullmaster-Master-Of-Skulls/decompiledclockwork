using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x02000628 RID: 1576
	public sealed class MinLengthAttributeAdapter : DataAnnotationsModelValidator<MinLengthAttribute>
	{
		// Token: 0x06004ED3 RID: 20179 RVA: 0x00112489 File Offset: 0x00110689
		public MinLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, MinLengthAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x00112494 File Offset: 0x00110694
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			return base.GetLocalizedString(errorMessage, new object[]
			{
				base.Metadata.GetDisplayName(),
				base.Attribute.Length
			});
		}
	}
}
