using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000041 RID: 65
	public abstract class ModelValidator
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00005F5F File Offset: 0x0000415F
		protected ModelValidator(ModelMetadata metadata, ControllerContext controllerContext)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			this.Metadata = metadata;
			this.ControllerContext = controllerContext;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005F91 File Offset: 0x00004191
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005F99 File Offset: 0x00004199
		protected internal ControllerContext ControllerContext { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005FA2 File Offset: 0x000041A2
		public virtual bool IsRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005FA5 File Offset: 0x000041A5
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005FAD File Offset: 0x000041AD
		protected internal ModelMetadata Metadata { get; private set; }

		// Token: 0x06000147 RID: 327 RVA: 0x00005FB6 File Offset: 0x000041B6
		public virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return Enumerable.Empty<ModelClientValidationRule>();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005FBD File Offset: 0x000041BD
		public static ModelValidator GetModelValidator(ModelMetadata metadata, ControllerContext context)
		{
			return new ModelValidator.CompositeModelValidator(metadata, context);
		}

		// Token: 0x06000149 RID: 329
		public abstract IEnumerable<ModelValidationResult> Validate(object container);

		// Token: 0x02000042 RID: 66
		private class CompositeModelValidator : ModelValidator
		{
			// Token: 0x0600014A RID: 330 RVA: 0x00005FC6 File Offset: 0x000041C6
			public CompositeModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext)
			{
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00005FD0 File Offset: 0x000041D0
			private static ModelValidationResult CreateSubPropertyResult(ModelMetadata propertyMetadata, ModelValidationResult propertyResult)
			{
				return new ModelValidationResult
				{
					MemberName = DefaultModelBinder.CreateSubPropertyName(propertyMetadata.PropertyName, propertyResult.MemberName),
					Message = propertyResult.Message
				};
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00006430 File Offset: 0x00004630
			public override IEnumerable<ModelValidationResult> Validate(object container)
			{
				bool propertiesValid = true;
				foreach (ModelMetadata propertyMetadata in base.Metadata.PropertiesAsArray)
				{
					foreach (ModelValidator propertyValidator in propertyMetadata.GetValidators(base.ControllerContext))
					{
						foreach (ModelValidationResult propertyResult in propertyValidator.Validate(base.Metadata.Model))
						{
							propertiesValid = false;
							yield return ModelValidator.CompositeModelValidator.CreateSubPropertyResult(propertyMetadata, propertyResult);
						}
					}
				}
				if (propertiesValid)
				{
					foreach (ModelValidator typeValidator in base.Metadata.GetValidators(base.ControllerContext))
					{
						foreach (ModelValidationResult typeResult in typeValidator.Validate(container))
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
