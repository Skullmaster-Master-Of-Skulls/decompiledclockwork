using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;

namespace System.Web.Http.Validation
{
	// Token: 0x0200017D RID: 381
	public sealed class ModelValidationNode
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x000203A3 File Offset: 0x0001E5A3
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey) : this(modelMetadata, modelStateKey, null)
		{
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000203B0 File Offset: 0x0001E5B0
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey, IEnumerable<ModelValidationNode> childNodes)
		{
			if (modelMetadata == null)
			{
				throw Error.ArgumentNull("modelMetadata");
			}
			if (modelStateKey == null)
			{
				throw Error.ArgumentNull("modelStateKey");
			}
			this.ModelMetadata = modelMetadata;
			this.ModelStateKey = modelStateKey;
			this._childNodes = ((childNodes != null) ? childNodes.ToList<ModelValidationNode>() : new List<ModelValidationNode>());
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060009D7 RID: 2519 RVA: 0x00020404 File Offset: 0x0001E604
		// (remove) Token: 0x060009D8 RID: 2520 RVA: 0x0002043C File Offset: 0x0001E63C
		public event EventHandler<ModelValidatedEventArgs> Validated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060009D9 RID: 2521 RVA: 0x00020474 File Offset: 0x0001E674
		// (remove) Token: 0x060009DA RID: 2522 RVA: 0x000204AC File Offset: 0x0001E6AC
		public event EventHandler<ModelValidatingEventArgs> Validating;

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x000204E1 File Offset: 0x0001E6E1
		public ICollection<ModelValidationNode> ChildNodes
		{
			get
			{
				return this._childNodes;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000204E9 File Offset: 0x0001E6E9
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x000204F1 File Offset: 0x0001E6F1
		public ModelMetadata ModelMetadata { get; private set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000204FA File Offset: 0x0001E6FA
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00020502 File Offset: 0x0001E702
		public string ModelStateKey { get; private set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002050B File Offset: 0x0001E70B
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00020513 File Offset: 0x0001E713
		public bool ValidateAllProperties { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002051C File Offset: 0x0001E71C
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00020524 File Offset: 0x0001E724
		public bool SuppressValidation { get; set; }

		// Token: 0x060009E4 RID: 2532 RVA: 0x00020530 File Offset: 0x0001E730
		public void CombineWith(ModelValidationNode otherNode)
		{
			if (otherNode != null && !otherNode.SuppressValidation)
			{
				this.Validated += otherNode.Validated;
				this.Validating += otherNode.Validating;
				List<ModelValidationNode> childNodes = otherNode._childNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					ModelValidationNode item = childNodes[i];
					this._childNodes.Add(item);
				}
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002058C File Offset: 0x0001E78C
		private void OnValidated(ModelValidatedEventArgs e)
		{
			EventHandler<ModelValidatedEventArgs> validated = this.Validated;
			if (validated != null)
			{
				validated(this, e);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000205AC File Offset: 0x0001E7AC
		private void OnValidating(ModelValidatingEventArgs e)
		{
			EventHandler<ModelValidatingEventArgs> validating = this.Validating;
			if (validating != null)
			{
				validating(this, e);
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000205CC File Offset: 0x0001E7CC
		private object TryConvertContainerToMetadataType(ModelValidationNode parentNode)
		{
			if (parentNode != null)
			{
				object model = parentNode.ModelMetadata.Model;
				if (model != null)
				{
					Type containerType = this.ModelMetadata.ContainerType;
					if (containerType != null && containerType.IsInstanceOfType(model))
					{
						return model;
					}
				}
			}
			return null;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002060C File Offset: 0x0001E80C
		public void Validate(HttpActionContext actionContext)
		{
			this.Validate(actionContext, null);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00020618 File Offset: 0x0001E818
		public void Validate(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (this.SuppressValidation)
			{
				return;
			}
			ModelValidatingEventArgs modelValidatingEventArgs = new ModelValidatingEventArgs(actionContext, parentNode);
			this.OnValidating(modelValidatingEventArgs);
			if (modelValidatingEventArgs.Cancel)
			{
				return;
			}
			this.ValidateChildren(actionContext);
			this.ValidateThis(actionContext, parentNode);
			ModelValidatedEventArgs e = new ModelValidatedEventArgs(actionContext, parentNode);
			this.OnValidated(e);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00020674 File Offset: 0x0001E874
		private void ValidateChildren(HttpActionContext actionContext)
		{
			for (int i = 0; i < this._childNodes.Count; i++)
			{
				ModelValidationNode modelValidationNode = this._childNodes[i];
				modelValidationNode.Validate(actionContext, this);
			}
			if (this.ValidateAllProperties)
			{
				this.ValidateProperties(actionContext);
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000206CC File Offset: 0x0001E8CC
		private void ValidateProperties(HttpActionContext actionContext)
		{
			ModelStateDictionary modelState = actionContext.ModelState;
			object model = this.ModelMetadata.Model;
			ModelMetadata metadataForType = actionContext.GetMetadataProvider().GetMetadataForType(() => model, this.ModelMetadata.ModelType);
			foreach (ModelMetadata modelMetadata in metadataForType.Properties)
			{
				string text = ModelBindingHelper.CreatePropertyModelName(this.ModelStateKey, modelMetadata.PropertyName);
				if (modelState.IsValidField(text))
				{
					foreach (ModelValidator modelValidator in actionContext.GetValidators(modelMetadata))
					{
						foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(modelMetadata, model))
						{
							string key = ModelBindingHelper.CreatePropertyModelName(text, modelValidationResult.MemberName);
							modelState.AddModelError(key, modelValidationResult.Message);
						}
					}
				}
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00020820 File Offset: 0x0001EA20
		private void ValidateThis(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			ModelStateDictionary modelState = actionContext.ModelState;
			if (!modelState.IsValidField(this.ModelStateKey))
			{
				return;
			}
			if (parentNode == null && this.ModelMetadata.Model == null)
			{
				string key = ModelBindingHelper.CreatePropertyModelName(this.ModelStateKey, this.ModelMetadata.GetDisplayName());
				modelState.AddModelError(key, SRResources.Validation_ValueNotFound);
				return;
			}
			this._validators = actionContext.GetValidators(this.ModelMetadata);
			object container = this.TryConvertContainerToMetadataType(parentNode);
			foreach (ModelValidator modelValidator in this._validators.AsArray<ModelValidator>())
			{
				foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(this.ModelMetadata, container))
				{
					string key2 = ModelBindingHelper.CreatePropertyModelName(this.ModelStateKey, modelValidationResult.MemberName);
					modelState.AddModelError(key2, modelValidationResult.Message);
				}
			}
		}

		// Token: 0x040002EC RID: 748
		private IEnumerable<ModelValidator> _validators;

		// Token: 0x040002ED RID: 749
		private readonly List<ModelValidationNode> _childNodes;
	}
}
