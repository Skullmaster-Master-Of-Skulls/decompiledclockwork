using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x0200064A RID: 1610
	public class DataAnnotationsModelValidator<TAttribute> : DataAnnotationsModelValidator where TAttribute : ValidationAttribute
	{
		// Token: 0x06004F8D RID: 20365 RVA: 0x0011449D File Offset: 0x0011269D
		public DataAnnotationsModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context, TAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x001144AD File Offset: 0x001126AD
		protected new TAttribute Attribute
		{
			get
			{
				return (TAttribute)((object)base.Attribute);
			}
		}
	}
}
