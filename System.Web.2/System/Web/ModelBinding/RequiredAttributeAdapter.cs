using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x02000669 RID: 1641
	public sealed class RequiredAttributeAdapter : DataAnnotationsModelValidator<RequiredAttribute>
	{
		// Token: 0x0600504E RID: 20558 RVA: 0x0011556C File Offset: 0x0011376C
		public RequiredAttributeAdapter(ModelMetadata metadata, ModelBindingExecutionContext context, RequiredAttribute attribute) : base(metadata, context, attribute)
		{
		}
	}
}
