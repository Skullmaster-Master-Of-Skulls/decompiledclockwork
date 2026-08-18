using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000131 RID: 305
	public abstract class AssociatedMetadataProvider<TModelMetadata> : ModelMetadataProvider where TModelMetadata : ModelMetadata
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x0001929A File Offset: 0x0001749A
		public sealed override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			if (containerType == null)
			{
				throw Error.ArgumentNull("containerType");
			}
			return this.GetMetadataForPropertiesImpl(container, containerType);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001953C File Offset: 0x0001773C
		private IEnumerable<ModelMetadata> GetMetadataForPropertiesImpl(object container, Type containerType)
		{
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInfo = this.GetTypeInformation(containerType);
			foreach (KeyValuePair<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> kvp in typeInfo.Properties)
			{
				KeyValuePair<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> keyValuePair = kvp;
				AssociatedMetadataProvider<TModelMetadata>.PropertyInformation propertyInfo = keyValuePair.Value;
				Func<object> modelAccessor = null;
				if (container != null)
				{
					Func<object, object> propertyGetter = propertyInfo.ValueAccessor;
					modelAccessor = (() => propertyGetter(container));
				}
				yield return this.CreateMetadataFromPrototype(propertyInfo.Prototype, modelAccessor);
			}
			yield break;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00019568 File Offset: 0x00017768
		public sealed override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			if (containerType == null)
			{
				throw Error.ArgumentNull("containerType");
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				throw Error.ArgumentNullOrEmpty("propertyName");
			}
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInformation = this.GetTypeInformation(containerType);
			AssociatedMetadataProvider<TModelMetadata>.PropertyInformation propertyInformation;
			if (!typeInformation.Properties.TryGetValue(propertyName, out propertyInformation))
			{
				throw Error.Argument("propertyName", SRResources.Common_PropertyNotFound, new object[]
				{
					containerType,
					propertyName
				});
			}
			return this.CreateMetadataFromPrototype(propertyInformation.Prototype, modelAccessor);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000195E8 File Offset: 0x000177E8
		public sealed override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			TModelMetadata prototype = this.GetTypeInformation(modelType).Prototype;
			return this.CreateMetadataFromPrototype(prototype, modelAccessor);
		}

		// Token: 0x06000784 RID: 1924
		protected abstract TModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName);

		// Token: 0x06000785 RID: 1925
		protected abstract TModelMetadata CreateMetadataFromPrototype(TModelMetadata prototype, Func<object> modelAccessor);

		// Token: 0x06000786 RID: 1926 RVA: 0x00019624 File Offset: 0x00017824
		private AssociatedMetadataProvider<TModelMetadata>.TypeInformation GetTypeInformation(Type type)
		{
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInformation;
			if (!this._typeInfoCache.TryGetValue(type, out typeInformation))
			{
				typeInformation = this.CreateTypeInformation(type);
				this._typeInfoCache.TryAdd(type, typeInformation);
			}
			return typeInformation;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00019658 File Offset: 0x00017858
		private AssociatedMetadataProvider<TModelMetadata>.TypeInformation CreateTypeInformation(Type type)
		{
			AssociatedMetadataProvider<TModelMetadata>.TypeInformation typeInformation = new AssociatedMetadataProvider<TModelMetadata>.TypeInformation();
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptorHelper.Get(type);
			typeInformation.TypeDescriptor = customTypeDescriptor;
			typeInformation.Prototype = this.CreateMetadataPrototype(AssociatedMetadataProvider<TModelMetadata>.AsAttributes(customTypeDescriptor.GetAttributes()), null, type, null);
			Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> dictionary = new Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation>();
			foreach (object obj in customTypeDescriptor.GetProperties())
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!dictionary.ContainsKey(propertyDescriptor.Name))
				{
					dictionary.Add(propertyDescriptor.Name, this.CreatePropertyInformation(type, propertyDescriptor));
				}
			}
			typeInformation.Properties = dictionary;
			return typeInformation;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00019710 File Offset: 0x00017910
		private AssociatedMetadataProvider<TModelMetadata>.PropertyInformation CreatePropertyInformation(Type containerType, PropertyDescriptor property)
		{
			return new AssociatedMetadataProvider<TModelMetadata>.PropertyInformation
			{
				ValueAccessor = AssociatedMetadataProvider<TModelMetadata>.CreatePropertyValueAccessor(property),
				Prototype = this.CreateMetadataPrototype(AssociatedMetadataProvider<TModelMetadata>.AsAttributes(property.Attributes), containerType, property.PropertyType, property.Name)
			};
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000198EC File Offset: 0x00017AEC
		private static IEnumerable<Attribute> AsAttributes(IEnumerable attributes)
		{
			foreach (object attribute in attributes)
			{
				yield return attribute as Attribute;
			}
			yield break;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00019920 File Offset: 0x00017B20
		private static Func<object, object> CreatePropertyValueAccessor(PropertyDescriptor property)
		{
			Type componentType = property.ComponentType;
			if (componentType.IsVisible)
			{
				string name = property.Name;
				PropertyInfo property2 = componentType.GetProperty(name, property.PropertyType);
				if (property2 != null && property2.CanRead)
				{
					MethodInfo getMethod = property2.GetGetMethod();
					if (getMethod != null)
					{
						return AssociatedMetadataProvider<TModelMetadata>.CreateDynamicValueAccessor(getMethod, componentType, name);
					}
				}
			}
			return (object container) => property.GetValue(container);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000199AC File Offset: 0x00017BAC
		private static Func<object, object> CreateDynamicValueAccessor(MethodInfo getMethodInfo, Type declaringType, string propertyName)
		{
			Type returnType = getMethodInfo.ReturnType;
			DynamicMethod dynamicMethod = new DynamicMethod("Get" + propertyName + "From" + declaringType.Name, typeof(object), new Type[]
			{
				typeof(object)
			});
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			if (declaringType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox, declaringType);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Castclass, declaringType);
			}
			if (declaringType.IsValueType || !getMethodInfo.IsVirtual || getMethodInfo.IsFinal)
			{
				ilgenerator.Emit(OpCodes.Call, getMethodInfo);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Callvirt, getMethodInfo);
			}
			if (returnType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Box, returnType);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (Func<object, object>)dynamicMethod.CreateDelegate(typeof(Func<object, object>));
		}

		// Token: 0x04000227 RID: 551
		private ConcurrentDictionary<Type, AssociatedMetadataProvider<TModelMetadata>.TypeInformation> _typeInfoCache = new ConcurrentDictionary<Type, AssociatedMetadataProvider<TModelMetadata>.TypeInformation>();

		// Token: 0x02000132 RID: 306
		private class TypeInformation
		{
			// Token: 0x1700023F RID: 575
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x00019AA8 File Offset: 0x00017CA8
			// (set) Token: 0x0600078E RID: 1934 RVA: 0x00019AB0 File Offset: 0x00017CB0
			public ICustomTypeDescriptor TypeDescriptor { get; set; }

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x0600078F RID: 1935 RVA: 0x00019AB9 File Offset: 0x00017CB9
			// (set) Token: 0x06000790 RID: 1936 RVA: 0x00019AC1 File Offset: 0x00017CC1
			public TModelMetadata Prototype { get; set; }

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x00019ACA File Offset: 0x00017CCA
			// (set) Token: 0x06000792 RID: 1938 RVA: 0x00019AD2 File Offset: 0x00017CD2
			public Dictionary<string, AssociatedMetadataProvider<TModelMetadata>.PropertyInformation> Properties { get; set; }
		}

		// Token: 0x02000133 RID: 307
		private class PropertyInformation
		{
			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000794 RID: 1940 RVA: 0x00019AE3 File Offset: 0x00017CE3
			// (set) Token: 0x06000795 RID: 1941 RVA: 0x00019AEB File Offset: 0x00017CEB
			public Func<object, object> ValueAccessor { get; set; }

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000796 RID: 1942 RVA: 0x00019AF4 File Offset: 0x00017CF4
			// (set) Token: 0x06000797 RID: 1943 RVA: 0x00019AFC File Offset: 0x00017CFC
			public TModelMetadata Prototype { get; set; }
		}
	}
}
