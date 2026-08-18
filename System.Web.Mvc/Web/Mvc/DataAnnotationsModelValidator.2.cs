using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000044 RID: 68
	public class DataAnnotationsModelValidator<TAttribute> : DataAnnotationsModelValidator where TAttribute : ValidationAttribute
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000065C7 File Offset: 0x000047C7
		public DataAnnotationsModelValidator(ModelMetadata metadata, ControllerContext context, TAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000065D7 File Offset: 0x000047D7
		protected new TAttribute Attribute
		{
			get
			{
				return (TAttribute)((object)base.Attribute);
			}
		}
	}
}
