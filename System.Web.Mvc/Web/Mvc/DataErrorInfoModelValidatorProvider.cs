using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000138 RID: 312
	public class DataErrorInfoModelValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00016280 File Offset: 0x00014480
		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return DataErrorInfoModelValidatorProvider.GetValidatorsImpl(metadata, context);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000163D8 File Offset: 0x000145D8
		private static IEnumerable<ModelValidator> GetValidatorsImpl(ModelMetadata metadata, ControllerContext context)
		{
			if (DataErrorInfoModelValidatorProvider.TypeImplementsIDataErrorInfo(metadata.ModelType))
			{
				yield return new DataErrorInfoModelValidatorProvider.DataErrorInfoClassModelValidator(metadata, context);
			}
			if (DataErrorInfoModelValidatorProvider.TypeImplementsIDataErrorInfo(metadata.ContainerType))
			{
				yield return new DataErrorInfoModelValidatorProvider.DataErrorInfoPropertyModelValidator(metadata, context);
			}
			yield break;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000163FC File Offset: 0x000145FC
		private static bool TypeImplementsIDataErrorInfo(Type type)
		{
			return typeof(IDataErrorInfo).IsAssignableFrom(type);
		}

		// Token: 0x02000139 RID: 313
		internal sealed class DataErrorInfoClassModelValidator : ModelValidator
		{
			// Token: 0x0600081F RID: 2079 RVA: 0x00016416 File Offset: 0x00014616
			public DataErrorInfoClassModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext)
			{
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x00016420 File Offset: 0x00014620
			public override IEnumerable<ModelValidationResult> Validate(object container)
			{
				IDataErrorInfo dataErrorInfo = base.Metadata.Model as IDataErrorInfo;
				if (dataErrorInfo != null)
				{
					string error = dataErrorInfo.Error;
					if (!string.IsNullOrEmpty(error))
					{
						return new ModelValidationResult[]
						{
							new ModelValidationResult
							{
								Message = error
							}
						};
					}
				}
				return Enumerable.Empty<ModelValidationResult>();
			}
		}

		// Token: 0x0200013A RID: 314
		internal sealed class DataErrorInfoPropertyModelValidator : ModelValidator
		{
			// Token: 0x06000821 RID: 2081 RVA: 0x0001646F File Offset: 0x0001466F
			public DataErrorInfoPropertyModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext)
			{
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x0001647C File Offset: 0x0001467C
			public override IEnumerable<ModelValidationResult> Validate(object container)
			{
				IDataErrorInfo dataErrorInfo = container as IDataErrorInfo;
				if (dataErrorInfo != null && !string.Equals(base.Metadata.PropertyName, "error", StringComparison.OrdinalIgnoreCase))
				{
					string text = dataErrorInfo[base.Metadata.PropertyName];
					if (!string.IsNullOrEmpty(text))
					{
						return new ModelValidationResult[]
						{
							new ModelValidationResult
							{
								Message = text
							}
						};
					}
				}
				return Enumerable.Empty<ModelValidationResult>();
			}
		}
	}
}
