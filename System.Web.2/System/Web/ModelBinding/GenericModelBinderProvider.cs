using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000671 RID: 1649
	public sealed class GenericModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06005071 RID: 20593 RVA: 0x00115A54 File Offset: 0x00113C54
		public GenericModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (modelBinder == null)
			{
				throw new ArgumentNullException("modelBinder");
			}
			GenericModelBinderProvider.ValidateParameters(modelType, null);
			this._modelType = modelType;
			this._modelBinderFactory = ((Type[] _) => modelBinder);
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x00115ABC File Offset: 0x00113CBC
		public GenericModelBinderProvider(Type modelType, Type modelBinderType)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (modelBinderType == null)
			{
				throw new ArgumentNullException("modelBinderType");
			}
			GenericModelBinderProvider.ValidateParameters(modelType, modelBinderType);
			bool modelBinderTypeIsOpenGeneric = modelBinderType.IsGenericTypeDefinition;
			this._modelType = modelType;
			this._modelBinderFactory = delegate(Type[] typeArguments)
			{
				Type type = modelBinderTypeIsOpenGeneric ? modelBinderType.MakeGenericType(typeArguments) : modelBinderType;
				return (IModelBinder)Activator.CreateInstance(type);
			};
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00115B3F File Offset: 0x00113D3F
		public GenericModelBinderProvider(Type modelType, Func<Type[], IModelBinder> modelBinderFactory)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (modelBinderFactory == null)
			{
				throw new ArgumentNullException("modelBinderFactory");
			}
			GenericModelBinderProvider.ValidateParameters(modelType, null);
			this._modelType = modelType;
			this._modelBinderFactory = modelBinderFactory;
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x00115B7E File Offset: 0x00113D7E
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06005075 RID: 20597 RVA: 0x00115B86 File Offset: 0x00113D86
		// (set) Token: 0x06005076 RID: 20598 RVA: 0x00115B8E File Offset: 0x00113D8E
		public bool SuppressPrefixCheck { get; set; }

		// Token: 0x06005077 RID: 20599 RVA: 0x00115B98 File Offset: 0x00113D98
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			Type[] array = null;
			if (this.ModelType.IsInterface)
			{
				Type type = TypeHelpers.ExtractGenericInterface(bindingContext.ModelType, this.ModelType);
				if (type != null)
				{
					array = type.GetGenericArguments();
				}
			}
			else
			{
				array = TypeHelpers.GetTypeArgumentsIfMatch(bindingContext.ModelType, this.ModelType);
			}
			if (array != null && (this.SuppressPrefixCheck || bindingContext.UnvalidatedValueProvider.ContainsPrefix(bindingContext.ModelName)))
			{
				return this._modelBinderFactory(array);
			}
			return null;
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x00115C1C File Offset: 0x00113E1C
		private static void ValidateParameters(Type modelType, Type modelBinderType)
		{
			if (!modelType.IsGenericTypeDefinition)
			{
				throw Error.GenericModelBinderProvider_ParameterMustSpecifyOpenGenericType(modelType, "modelType");
			}
			if (modelBinderType != null)
			{
				if (!typeof(IModelBinder).IsAssignableFrom(modelBinderType))
				{
					throw Error.Common_TypeMustImplementInterface(modelBinderType, typeof(IModelBinder), "modelBinderType");
				}
				if (modelBinderType.IsGenericTypeDefinition && modelType.GetGenericArguments().Length != modelBinderType.GetGenericArguments().Length)
				{
					throw Error.GenericModelBinderProvider_TypeArgumentCountMismatch(modelType, modelBinderType);
				}
			}
		}

		// Token: 0x04002AC4 RID: 10948
		private readonly Func<Type[], IModelBinder> _modelBinderFactory;

		// Token: 0x04002AC5 RID: 10949
		private readonly Type _modelType;
	}
}
