using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Runtime.CompilerServices;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x02000175 RID: 373
	public class DefaultBodyModelValidator : IBodyModelValidator
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (metadataProvider == null)
			{
				throw Error.ArgumentNull("metadataProvider");
			}
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (model != null && !this.ShouldValidateType(model.GetType()))
			{
				return true;
			}
			ModelValidatorProvider[] array = actionContext.GetValidatorProviders().ToArray<ModelValidatorProvider>();
			if (array == null || array.Length == 0)
			{
				return true;
			}
			ModelMetadata metadataForType = metadataProvider.GetMetadataForType(() => model, type);
			DefaultBodyModelValidator.ValidationContext validationContext = new DefaultBodyModelValidator.ValidationContext
			{
				MetadataProvider = metadataProvider,
				ActionContext = actionContext,
				ValidatorCache = actionContext.GetValidatorCache(),
				ModelState = actionContext.ModelState,
				Visited = new HashSet<object>(ReferenceEqualityComparer.Instance),
				KeyBuilders = new Stack<DefaultBodyModelValidator.IKeyBuilder>(),
				RootPrefix = keyPrefix
			};
			return this.ValidateNodeAndChildren(metadataForType, validationContext, null, null);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001FDAA File Offset: 0x0001DFAA
		public virtual bool ShouldValidateType(Type type)
		{
			return !MediaTypeFormatterCollection.IsTypeExcludedFromValidation(type);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001FDB8 File Offset: 0x0001DFB8
		private bool ValidateNodeAndChildren(ModelMetadata metadata, DefaultBodyModelValidator.ValidationContext validationContext, object container, IEnumerable<ModelValidator> validators)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			object obj = null;
			try
			{
				obj = metadata.Model;
			}
			catch
			{
				return true;
			}
			if (validators == null)
			{
				validators = validationContext.ActionContext.GetValidators(metadata, validationContext.ValidatorCache);
			}
			if (obj == null)
			{
				return DefaultBodyModelValidator.ShallowValidate(metadata, validationContext, container, validators);
			}
			Type type = obj.GetType();
			if (TypeHelper.IsSimpleType(type) || !this.ShouldValidateType(type))
			{
				return DefaultBodyModelValidator.ShallowValidate(metadata, validationContext, container, validators);
			}
			if (validationContext.Visited.Contains(obj))
			{
				return true;
			}
			validationContext.Visited.Add(obj);
			IEnumerable enumerable = obj as IEnumerable;
			bool flag;
			if (enumerable == null)
			{
				flag = this.ValidateProperties(metadata, validationContext);
			}
			else
			{
				flag = this.ValidateElements(enumerable, validationContext);
			}
			if (flag)
			{
				flag = DefaultBodyModelValidator.ShallowValidate(metadata, validationContext, container, validators);
			}
			validationContext.Visited.Remove(obj);
			return flag;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001FE94 File Offset: 0x0001E094
		private bool ValidateProperties(ModelMetadata metadata, DefaultBodyModelValidator.ValidationContext validationContext)
		{
			bool result = true;
			DefaultBodyModelValidator.PropertyScope propertyScope = new DefaultBodyModelValidator.PropertyScope();
			validationContext.KeyBuilders.Push(propertyScope);
			foreach (ModelMetadata modelMetadata in validationContext.MetadataProvider.GetMetadataForProperties(metadata.Model, metadata.RealModelType))
			{
				propertyScope.PropertyName = modelMetadata.PropertyName;
				if (!this.ValidateNodeAndChildren(modelMetadata, validationContext, metadata.Model, null))
				{
					result = false;
				}
			}
			validationContext.KeyBuilders.Pop();
			return result;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001FF2C File Offset: 0x0001E12C
		private bool ValidateElements(IEnumerable model, DefaultBodyModelValidator.ValidationContext validationContext)
		{
			bool result = true;
			Type elementType = DefaultBodyModelValidator.GetElementType(model.GetType());
			ModelMetadata metadataForType = validationContext.MetadataProvider.GetMetadataForType(null, elementType);
			DefaultBodyModelValidator.ElementScope elementScope = new DefaultBodyModelValidator.ElementScope
			{
				Index = 0
			};
			validationContext.KeyBuilders.Push(elementScope);
			IEnumerable<ModelValidator> validators = validationContext.ActionContext.GetValidators(metadataForType, validationContext.ValidatorCache);
			bool flag = validators.Any<ModelValidator>();
			foreach (object obj in model)
			{
				if (obj != null || flag)
				{
					metadataForType.Model = obj;
					if (!this.ValidateNodeAndChildren(metadataForType, validationContext, model, validators))
					{
						result = false;
					}
				}
				elementScope.Index++;
			}
			validationContext.KeyBuilders.Pop();
			return result;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00020010 File Offset: 0x0001E210
		private static bool ShallowValidate(ModelMetadata metadata, DefaultBodyModelValidator.ValidationContext validationContext, object container, IEnumerable<ModelValidator> validators)
		{
			bool result = true;
			string text = null;
			ICollection collection = validators as ICollection;
			if (collection != null && collection.Count == 0)
			{
				return result;
			}
			foreach (ModelValidator modelValidator in validators)
			{
				foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(metadata, container))
				{
					if (text == null)
					{
						text = validationContext.RootPrefix;
						foreach (DefaultBodyModelValidator.IKeyBuilder keyBuilder in validationContext.KeyBuilders.Reverse<DefaultBodyModelValidator.IKeyBuilder>())
						{
							text = keyBuilder.AppendTo(text);
						}
					}
					string key = ModelBindingHelper.CreatePropertyModelName(text, modelValidationResult.MemberName);
					validationContext.ModelState.AddModelError(key, modelValidationResult.Message);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002012C File Offset: 0x0001E32C
		private static Type GetElementType(Type type)
		{
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type2.GetGenericArguments()[0];
				}
			}
			return typeof(object);
		}

		// Token: 0x02000176 RID: 374
		private interface IKeyBuilder
		{
			// Token: 0x060009B0 RID: 2480
			string AppendTo(string prefix);
		}

		// Token: 0x02000177 RID: 375
		private class PropertyScope : DefaultBodyModelValidator.IKeyBuilder
		{
			// Token: 0x170002CE RID: 718
			// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002019F File Offset: 0x0001E39F
			// (set) Token: 0x060009B2 RID: 2482 RVA: 0x000201A7 File Offset: 0x0001E3A7
			public string PropertyName { get; set; }

			// Token: 0x060009B3 RID: 2483 RVA: 0x000201B0 File Offset: 0x0001E3B0
			public string AppendTo(string prefix)
			{
				return ModelBindingHelper.CreatePropertyModelName(prefix, this.PropertyName);
			}
		}

		// Token: 0x02000178 RID: 376
		private class ElementScope : DefaultBodyModelValidator.IKeyBuilder
		{
			// Token: 0x170002CF RID: 719
			// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000201C6 File Offset: 0x0001E3C6
			// (set) Token: 0x060009B6 RID: 2486 RVA: 0x000201CE File Offset: 0x0001E3CE
			public int Index { get; set; }

			// Token: 0x060009B7 RID: 2487 RVA: 0x000201D7 File Offset: 0x0001E3D7
			public string AppendTo(string prefix)
			{
				return ModelBindingHelper.CreateIndexModelName(prefix, this.Index);
			}
		}

		// Token: 0x02000179 RID: 377
		private class ValidationContext
		{
			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000201ED File Offset: 0x0001E3ED
			// (set) Token: 0x060009BA RID: 2490 RVA: 0x000201F5 File Offset: 0x0001E3F5
			public ModelMetadataProvider MetadataProvider { get; set; }

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x060009BB RID: 2491 RVA: 0x000201FE File Offset: 0x0001E3FE
			// (set) Token: 0x060009BC RID: 2492 RVA: 0x00020206 File Offset: 0x0001E406
			public HttpActionContext ActionContext { get; set; }

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002020F File Offset: 0x0001E40F
			// (set) Token: 0x060009BE RID: 2494 RVA: 0x00020217 File Offset: 0x0001E417
			public IModelValidatorCache ValidatorCache { get; set; }

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x060009BF RID: 2495 RVA: 0x00020220 File Offset: 0x0001E420
			// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00020228 File Offset: 0x0001E428
			public ModelStateDictionary ModelState { get; set; }

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00020231 File Offset: 0x0001E431
			// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00020239 File Offset: 0x0001E439
			public HashSet<object> Visited { get; set; }

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00020242 File Offset: 0x0001E442
			// (set) Token: 0x060009C4 RID: 2500 RVA: 0x0002024A File Offset: 0x0001E44A
			public Stack<DefaultBodyModelValidator.IKeyBuilder> KeyBuilders { get; set; }

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00020253 File Offset: 0x0001E453
			// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0002025B File Offset: 0x0001E45B
			public string RootPrefix { get; set; }
		}
	}
}
