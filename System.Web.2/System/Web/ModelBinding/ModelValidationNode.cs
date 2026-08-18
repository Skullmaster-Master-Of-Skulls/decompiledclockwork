using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200067F RID: 1663
	public sealed class ModelValidationNode
	{
		// Token: 0x060050BB RID: 20667 RVA: 0x00116570 File Offset: 0x00114770
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey) : this(modelMetadata, modelStateKey, null)
		{
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0011657C File Offset: 0x0011477C
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey, IEnumerable<ModelValidationNode> childNodes)
		{
			if (modelMetadata == null)
			{
				throw new ArgumentNullException("modelMetadata");
			}
			if (modelStateKey == null)
			{
				throw new ArgumentNullException("modelStateKey");
			}
			this.ModelMetadata = modelMetadata;
			this.ModelStateKey = modelStateKey;
			this.ChildNodes = ((childNodes != null) ? childNodes.ToList<ModelValidationNode>() : new List<ModelValidationNode>());
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x001165CF File Offset: 0x001147CF
		// (set) Token: 0x060050BE RID: 20670 RVA: 0x001165D7 File Offset: 0x001147D7
		public ICollection<ModelValidationNode> ChildNodes { get; private set; }

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x001165E0 File Offset: 0x001147E0
		// (set) Token: 0x060050C0 RID: 20672 RVA: 0x001165E8 File Offset: 0x001147E8
		public ModelMetadata ModelMetadata { get; private set; }

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x001165F1 File Offset: 0x001147F1
		// (set) Token: 0x060050C2 RID: 20674 RVA: 0x001165F9 File Offset: 0x001147F9
		public string ModelStateKey { get; private set; }

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x060050C3 RID: 20675 RVA: 0x00116602 File Offset: 0x00114802
		// (set) Token: 0x060050C4 RID: 20676 RVA: 0x0011660A File Offset: 0x0011480A
		public bool ValidateAllProperties { get; set; }

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x00116613 File Offset: 0x00114813
		// (set) Token: 0x060050C6 RID: 20678 RVA: 0x0011661B File Offset: 0x0011481B
		public bool SuppressValidation { get; set; }

		// Token: 0x1400012D RID: 301
		// (add) Token: 0x060050C7 RID: 20679 RVA: 0x00116624 File Offset: 0x00114824
		// (remove) Token: 0x060050C8 RID: 20680 RVA: 0x0011665C File Offset: 0x0011485C
		public event EventHandler<ModelValidatedEventArgs> Validated;

		// Token: 0x1400012E RID: 302
		// (add) Token: 0x060050C9 RID: 20681 RVA: 0x00116694 File Offset: 0x00114894
		// (remove) Token: 0x060050CA RID: 20682 RVA: 0x001166CC File Offset: 0x001148CC
		public event EventHandler<ModelValidatingEventArgs> Validating;

		// Token: 0x060050CB RID: 20683 RVA: 0x00116704 File Offset: 0x00114904
		public void CombineWith(ModelValidationNode otherNode)
		{
			if (otherNode != null && !otherNode.SuppressValidation)
			{
				this.Validated += otherNode.Validated;
				this.Validating += otherNode.Validating;
				foreach (ModelValidationNode item in otherNode.ChildNodes)
				{
					this.ChildNodes.Add(item);
				}
			}
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0011677C File Offset: 0x0011497C
		private void OnValidated(ModelValidatedEventArgs e)
		{
			EventHandler<ModelValidatedEventArgs> validated = this.Validated;
			if (validated != null)
			{
				validated(this, e);
			}
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0011679C File Offset: 0x0011499C
		private void OnValidating(ModelValidatingEventArgs e)
		{
			EventHandler<ModelValidatingEventArgs> validating = this.Validating;
			if (validating != null)
			{
				validating(this, e);
			}
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x001167BC File Offset: 0x001149BC
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

		// Token: 0x060050CF RID: 20687 RVA: 0x001167FC File Offset: 0x001149FC
		public void Validate(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			this.Validate(modelBindingExecutionContext, null);
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x00116808 File Offset: 0x00114A08
		public void Validate(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			if (this.SuppressValidation)
			{
				return;
			}
			ModelValidatingEventArgs modelValidatingEventArgs = new ModelValidatingEventArgs(modelBindingExecutionContext, parentNode);
			this.OnValidating(modelValidatingEventArgs);
			if (modelValidatingEventArgs.Cancel)
			{
				return;
			}
			this.ValidateChildren(modelBindingExecutionContext);
			this.ValidateThis(modelBindingExecutionContext, parentNode);
			ModelValidatedEventArgs e = new ModelValidatedEventArgs(modelBindingExecutionContext, parentNode);
			this.OnValidated(e);
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00116864 File Offset: 0x00114A64
		private void ValidateChildren(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			foreach (ModelValidationNode modelValidationNode in this.ChildNodes)
			{
				modelValidationNode.Validate(modelBindingExecutionContext, this);
			}
			if (this.ValidateAllProperties)
			{
				this.ValidateProperties(modelBindingExecutionContext);
			}
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x001168C4 File Offset: 0x00114AC4
		private void ValidateProperties(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			ModelStateDictionary modelState = modelBindingExecutionContext.ModelState;
			object model = this.ModelMetadata.Model;
			ModelMetadata metadataForType = ModelMetadataProviders.Current.GetMetadataForType(() => model, this.ModelMetadata.ModelType);
			foreach (ModelMetadata modelMetadata in metadataForType.Properties)
			{
				string text = ModelBinderUtil.CreatePropertyModelName(this.ModelStateKey, modelMetadata.PropertyName);
				if (modelState.IsValidField(text))
				{
					foreach (ModelValidator modelValidator in modelMetadata.GetValidators(modelBindingExecutionContext))
					{
						foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(model))
						{
							string key = ModelBinderUtil.CreatePropertyModelName(text, modelValidationResult.MemberName);
							modelState.AddModelError(key, modelValidationResult.Message);
						}
					}
				}
			}
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x00116A10 File Offset: 0x00114C10
		private void ValidateThis(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			ModelStateDictionary modelState = modelBindingExecutionContext.ModelState;
			if (!modelState.IsValidField(this.ModelStateKey))
			{
				return;
			}
			object container = this.TryConvertContainerToMetadataType(parentNode);
			foreach (ModelValidator modelValidator in this.ModelMetadata.GetValidators(modelBindingExecutionContext))
			{
				foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(container))
				{
					string key = ModelBinderUtil.CreatePropertyModelName(this.ModelStateKey, modelValidationResult.MemberName);
					modelState.AddModelError(key, modelValidationResult.Message);
				}
			}
		}
	}
}
