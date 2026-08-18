using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x02000627 RID: 1575
	public sealed class MaxLengthAttributeAdapter : DataAnnotationsModelValidator<MaxLengthAttribute>
	{
		// Token: 0x06004ED1 RID: 20177 RVA: 0x0011244E File Offset: 0x0011064E
		public MaxLengthAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, MaxLengthAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x00112459 File Offset: 0x00110659
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
