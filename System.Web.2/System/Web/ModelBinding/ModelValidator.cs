using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000660 RID: 1632
	public abstract class ModelValidator
	{
		// Token: 0x0600502A RID: 20522 RVA: 0x0011523B File Offset: 0x0011343B
		protected ModelValidator(ModelMetadata metadata, ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			this.Metadata = metadata;
			this.ModelBindingExecutionContext = modelBindingExecutionContext;
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x0011526D File Offset: 0x0011346D
		// (set) Token: 0x0600502C RID: 20524 RVA: 0x00115275 File Offset: 0x00113475
		protected internal ModelBindingExecutionContext ModelBindingExecutionContext { get; private set; }

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x0011527E File Offset: 0x0011347E
		// (set) Token: 0x0600502F RID: 20527 RVA: 0x00115286 File Offset: 0x00113486
		protected internal ModelMetadata Metadata { get; private set; }

		// Token: 0x06005030 RID: 20528 RVA: 0x0011528F File Offset: 0x0011348F
		public static ModelValidator GetModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			return new ModelValidator.CompositeModelValidator(metadata, context);
		}

		// Token: 0x06005031 RID: 20529
		public abstract IEnumerable<ModelValidationResult> Validate(object container);

		// Token: 0x02000A2C RID: 2604
		private class CompositeModelValidator : ModelValidator
		{
			// Token: 0x06006E4B RID: 28235 RVA: 0x001155E0 File Offset: 0x001137E0
			public CompositeModelValidator(ModelMetadata metadata, ModelBindingExecutionContext modelBindingExecutionContext) : base(metadata, modelBindingExecutionContext)
			{
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x00189849 File Offset: 0x00187A49
			public override IEnumerable<ModelValidationResult> Validate(object container)
			{
				bool propertiesValid = true;
				foreach (ModelMetadata propertyMetadata in base.Metadata.Properties)
				{
					foreach (ModelValidator modelValidator in propertyMetadata.GetValidators(base.ModelBindingExecutionContext))
					{
						foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(base.Metadata.Model))
						{
							propertiesValid = false;
							yield return new ModelValidationResult
							{
								MemberName = ValueProviderUtil.CreateSubPropertyName(propertyMetadata.PropertyName, modelValidationResult.MemberName),
								Message = modelValidationResult.Message
							};
						}
						IEnumerator<ModelValidationResult> enumerator3 = null;
					}
					IEnumerator<ModelValidator> enumerator2 = null;
					propertyMetadata = null;
				}
				IEnumerator<ModelMetadata> enumerator = null;
				if (propertiesValid)
				{
					foreach (ModelValidator modelValidator2 in base.Metadata.GetValidators(base.ModelBindingExecutionContext))
					{
						foreach (ModelValidationResult modelValidationResult2 in modelValidator2.Validate(container))
						{
							yield return modelValidationResult2;
						}
						IEnumerator<ModelValidationResult> enumerator3 = null;
					}
					IEnumerator<ModelValidator> enumerator2 = null;
				}
				yield break;
				yield break;
			}
		}
	}
}
