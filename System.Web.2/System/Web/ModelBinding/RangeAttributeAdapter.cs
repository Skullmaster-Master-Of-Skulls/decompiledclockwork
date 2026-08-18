using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x02000667 RID: 1639
	public sealed class RangeAttributeAdapter : DataAnnotationsModelValidator<RangeAttribute>
	{
		// Token: 0x0600504A RID: 20554 RVA: 0x001154F2 File Offset: 0x001136F2
		public RangeAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RangeAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x001154FD File Offset: 0x001136FD
		protected override string GetLocalizedErrorMessage(string errorMessage)
		{
			return base.GetLocalizedString(errorMessage, new object[]
			{
				base.Metadata.GetDisplayName(),
				base.Attribute.Minimum,
				base.Attribute.Maximum
			});
		}
	}
}
