using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x02000188 RID: 392
	public abstract class ModelValidator
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x00021878 File Offset: 0x0001FA78
		protected ModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			this.ValidatorProviders = validatorProviders;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00021895 File Offset: 0x0001FA95
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0002189D File Offset: 0x0001FA9D
		protected internal IEnumerable<ModelValidatorProvider> ValidatorProviders { get; private set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000218A6 File Offset: 0x0001FAA6
		public virtual bool IsRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000218A9 File Offset: 0x0001FAA9
		public static ModelValidator GetModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			return new ModelValidator.CompositeModelValidator(validatorProviders);
		}

		// Token: 0x06000A27 RID: 2599
		public abstract IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container);

		// Token: 0x02000189 RID: 393
		private class CompositeModelValidator : ModelValidator
		{
			// Token: 0x06000A28 RID: 2600 RVA: 0x000218B1 File Offset: 0x0001FAB1
			public CompositeModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders) : base(validatorProviders)
			{
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x00021D74 File Offset: 0x0001FF74
			public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
			{
				bool propertiesValid = true;
				foreach (ModelMetadata propertyMetadata in metadata.Properties)
				{
					foreach (ModelValidator propertyValidator in propertyMetadata.GetValidators(base.ValidatorProviders))
					{
						foreach (ModelValidationResult propertyResult in propertyValidator.Validate(metadata, container))
						{
							propertiesValid = false;
							yield return new ModelValidationResult
							{
								MemberName = ModelBindingHelper.CreatePropertyModelName(propertyMetadata.PropertyName, propertyResult.MemberName),
								Message = propertyResult.Message
							};
						}
					}
				}
				if (propertiesValid)
				{
					foreach (ModelValidator typeValidator in metadata.GetValidators(base.ValidatorProviders))
					{
						foreach (ModelValidationResult typeResult in typeValidator.Validate(metadata, container))
						{
							yield return typeResult;
						}
					}
				}
				yield break;
			}
		}
	}
}
