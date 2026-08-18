using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x020000FE RID: 254
	public class CompositeModelBinder : IModelBinder
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x00014640 File Offset: 0x00012840
		public CompositeModelBinder(IEnumerable<IModelBinder> binders) : this(binders.ToArray<IModelBinder>())
		{
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001464E File Offset: 0x0001284E
		public CompositeModelBinder(params IModelBinder[] binders)
		{
			this.Binders = binders;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001465D File Offset: 0x0001285D
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00014665 File Offset: 0x00012865
		private IModelBinder[] Binders { get; set; }

		// Token: 0x0600062B RID: 1579 RVA: 0x00014670 File Offset: 0x00012870
		public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingContext modelBindingContext = CompositeModelBinder.CreateNewBindingContext(bindingContext, bindingContext.ModelName);
			bool flag = this.TryBind(actionContext, modelBindingContext);
			if (!flag && !string.IsNullOrEmpty(bindingContext.ModelName) && bindingContext.FallbackToEmptyPrefix)
			{
				modelBindingContext = CompositeModelBinder.CreateNewBindingContext(bindingContext, string.Empty);
				flag = this.TryBind(actionContext, modelBindingContext);
			}
			if (!flag)
			{
				return false;
			}
			if (!modelBindingContext.ModelMetadata.IsComplexType && string.IsNullOrEmpty(modelBindingContext.ModelName))
			{
				modelBindingContext.ValidationNode = new ModelValidationNode(modelBindingContext.ModelMetadata, bindingContext.ModelName);
			}
			modelBindingContext.ValidationNode.Validate(actionContext, null);
			bindingContext.Model = modelBindingContext.Model;
			return true;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00014710 File Offset: 0x00012910
		private bool TryBind(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			return actionContext.Bind(bindingContext, this.Binders);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00014720 File Offset: 0x00012920
		private static ModelBindingContext CreateNewBindingContext(ModelBindingContext oldBindingContext, string modelName)
		{
			ModelBindingContext modelBindingContext = new ModelBindingContext
			{
				ModelMetadata = oldBindingContext.ModelMetadata,
				ModelName = modelName,
				ModelState = oldBindingContext.ModelState,
				ValueProvider = oldBindingContext.ValueProvider
			};
			if (object.ReferenceEquals(modelName, oldBindingContext.ModelName))
			{
				modelBindingContext.ValidationNode = oldBindingContext.ValidationNode;
			}
			return modelBindingContext;
		}
	}
}
