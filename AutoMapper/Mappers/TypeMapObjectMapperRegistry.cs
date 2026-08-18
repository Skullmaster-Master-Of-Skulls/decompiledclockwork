using System;
using System.Collections.Generic;

namespace AutoMapper.Mappers
{
	// Token: 0x02000093 RID: 147
	public static class TypeMapObjectMapperRegistry
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00011FC4 File Offset: 0x000101C4
		public static IList<ITypeMapObjectMapper> Mappers { get; } = new List<ITypeMapObjectMapper>
		{
			new TypeMapObjectMapperRegistry.SubstutitionMapperStrategy(),
			new TypeMapObjectMapperRegistry.CustomMapperStrategy(),
			new TypeMapObjectMapperRegistry.NullMappingStrategy(),
			new TypeMapObjectMapperRegistry.CacheMappingStrategy(),
			new TypeMapObjectMapperRegistry.NewObjectPropertyMapMappingStrategy(),
			new TypeMapObjectMapperRegistry.ExistingObjectMappingStrategy()
		};

		// Token: 0x02000145 RID: 325
		private class CustomMapperStrategy : ITypeMapObjectMapper
		{
			// Token: 0x0600093A RID: 2362 RVA: 0x00018E24 File Offset: 0x00017024
			public object Map(ResolutionContext context)
			{
				return context.TypeMap.CustomMapper(context);
			}

			// Token: 0x0600093B RID: 2363 RVA: 0x00018E37 File Offset: 0x00017037
			public bool IsMatch(ResolutionContext context)
			{
				return context.TypeMap.CustomMapper != null;
			}
		}

		// Token: 0x02000146 RID: 326
		private class SubstutitionMapperStrategy : ITypeMapObjectMapper
		{
			// Token: 0x0600093D RID: 2365 RVA: 0x00018E48 File Offset: 0x00017048
			public object Map(ResolutionContext context)
			{
				object obj = context.TypeMap.Substitution(context.SourceValue);
				TypeMap memberTypeMap = context.ConfigurationProvider.ResolveTypeMap(obj.GetType(), context.DestinationType);
				ResolutionContext context2 = context.CreateTypeContext(memberTypeMap, obj, context.DestinationValue, obj.GetType(), context.DestinationType);
				return context.Engine.Map(context2);
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x00018EAB File Offset: 0x000170AB
			public bool IsMatch(ResolutionContext context)
			{
				return context.TypeMap.Substitution != null;
			}
		}

		// Token: 0x02000147 RID: 327
		private class NullMappingStrategy : ITypeMapObjectMapper
		{
			// Token: 0x06000940 RID: 2368 RVA: 0x00018EBB File Offset: 0x000170BB
			public object Map(ResolutionContext context)
			{
				return null;
			}

			// Token: 0x06000941 RID: 2369 RVA: 0x00018EC0 File Offset: 0x000170C0
			public bool IsMatch(ResolutionContext context)
			{
				IProfileConfiguration profileConfiguration = context.ConfigurationProvider.GetProfileConfiguration(context.TypeMap.Profile);
				return context.SourceValue == null && profileConfiguration.AllowNullDestinationValues;
			}
		}

		// Token: 0x02000148 RID: 328
		private class CacheMappingStrategy : ITypeMapObjectMapper
		{
			// Token: 0x06000943 RID: 2371 RVA: 0x00018EF4 File Offset: 0x000170F4
			public object Map(ResolutionContext context)
			{
				return context.InstanceCache[context];
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x00018F02 File Offset: 0x00017102
			public bool IsMatch(ResolutionContext context)
			{
				return !context.Options.DisableCache && context.DestinationValue == null && context.InstanceCache.ContainsKey(context);
			}
		}

		// Token: 0x02000149 RID: 329
		private abstract class PropertyMapMappingStrategy : ITypeMapObjectMapper
		{
			// Token: 0x06000946 RID: 2374 RVA: 0x00018F28 File Offset: 0x00017128
			public object Map(ResolutionContext context)
			{
				object obj = this.GetMappedObject(context);
				if (context.SourceValue != null && !context.Options.DisableCache)
				{
					context.InstanceCache[context] = obj;
				}
				context.TypeMap.BeforeMap(context.SourceValue, obj);
				context.BeforeMap(obj);
				foreach (PropertyMap propertyMap in context.TypeMap.GetPropertyMaps())
				{
					this.MapPropertyValue(context.CreatePropertyMapContext(propertyMap), obj, propertyMap);
				}
				obj = this.ReassignValue(context, obj);
				context.AfterMap(obj);
				context.TypeMap.AfterMap(context.SourceValue, obj);
				return obj;
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x00018DA0 File Offset: 0x00016FA0
			protected virtual object ReassignValue(ResolutionContext context, object o)
			{
				return o;
			}

			// Token: 0x06000948 RID: 2376
			public abstract bool IsMatch(ResolutionContext context);

			// Token: 0x06000949 RID: 2377
			protected abstract object GetMappedObject(ResolutionContext context);

			// Token: 0x0600094A RID: 2378 RVA: 0x00018FF4 File Offset: 0x000171F4
			private void MapPropertyValue(ResolutionContext context, object mappedObject, PropertyMap propertyMap)
			{
				if (!propertyMap.CanResolveValue() || !propertyMap.ShouldAssignValuePreResolving(context))
				{
					return;
				}
				Exception ex = null;
				ResolutionResult resolutionResult;
				try
				{
					resolutionResult = propertyMap.ResolveValue(context);
				}
				catch (AutoMapperMappingException)
				{
					throw;
				}
				catch (Exception inner)
				{
					ex = new AutoMapperMappingException(this.CreateErrorContext(context, propertyMap, null), inner);
					resolutionResult = new ResolutionResult(context);
				}
				if (resolutionResult.ShouldIgnore)
				{
					return;
				}
				object destinationValue = propertyMap.GetDestinationValue(mappedObject);
				Type type = resolutionResult.Type;
				Type memberType = propertyMap.DestinationProperty.MemberType;
				TypeMap typeMap = context.ConfigurationProvider.ResolveTypeMap(resolutionResult, memberType);
				Type sourceMemberType = (typeMap != null) ? typeMap.SourceType : type;
				ResolutionContext context2 = context.CreateMemberContext(typeMap, resolutionResult.Value, destinationValue, sourceMemberType, propertyMap);
				if (!propertyMap.ShouldAssignValue(context2))
				{
					return;
				}
				if (ex != null)
				{
					throw ex;
				}
				try
				{
					object propertyValueToAssign = context.Engine.Map(context2);
					this.AssignValue(propertyMap, mappedObject, propertyValueToAssign);
				}
				catch (AutoMapperMappingException)
				{
					throw;
				}
				catch (Exception inner2)
				{
					throw new AutoMapperMappingException(context2, inner2);
				}
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x00019108 File Offset: 0x00017308
			protected virtual void AssignValue(PropertyMap propertyMap, object mappedObject, object propertyValueToAssign)
			{
				if (propertyMap.CanBeSet)
				{
					propertyMap.DestinationProperty.SetValue(mappedObject, propertyValueToAssign);
				}
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0001911F File Offset: 0x0001731F
			private ResolutionContext CreateErrorContext(ResolutionContext context, PropertyMap propertyMap, object destinationValue)
			{
				TypeMap memberTypeMap = null;
				object sourceValue = context.SourceValue;
				object sourceValue2 = context.SourceValue;
				return context.CreateMemberContext(memberTypeMap, sourceValue, destinationValue, ((sourceValue2 != null) ? sourceValue2.GetType() : null) ?? typeof(object), propertyMap);
			}
		}

		// Token: 0x0200014A RID: 330
		private class NewObjectPropertyMapMappingStrategy : TypeMapObjectMapperRegistry.PropertyMapMappingStrategy
		{
			// Token: 0x0600094E RID: 2382 RVA: 0x00019150 File Offset: 0x00017350
			public override bool IsMatch(ResolutionContext context)
			{
				return context.DestinationValue == null;
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0001915B File Offset: 0x0001735B
			protected override object GetMappedObject(ResolutionContext context)
			{
				object obj = context.Engine.CreateObject(context);
				if (obj == null)
				{
					throw new InvalidOperationException("Cannot create destination object. " + context);
				}
				return obj;
			}
		}

		// Token: 0x0200014B RID: 331
		private class ExistingObjectMappingStrategy : TypeMapObjectMapperRegistry.PropertyMapMappingStrategy
		{
			// Token: 0x06000951 RID: 2385 RVA: 0x00006339 File Offset: 0x00004539
			public override bool IsMatch(ResolutionContext context)
			{
				return true;
			}

			// Token: 0x06000952 RID: 2386 RVA: 0x00019185 File Offset: 0x00017385
			protected override object GetMappedObject(ResolutionContext context)
			{
				return context.DestinationValue;
			}
		}
	}
}
