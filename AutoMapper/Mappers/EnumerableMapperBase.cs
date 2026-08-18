using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200007B RID: 123
	public abstract class EnumerableMapperBase<TEnumerable> : IObjectMapper where TEnumerable : IEnumerable
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00010898 File Offset: 0x0000EA98
		public object Map(ResolutionContext context)
		{
			if (context.IsSourceValueNull && context.Engine.ShouldMapSourceCollectionAsNull(context))
			{
				return null;
			}
			ICollection<object> collection = (((IEnumerable)context.SourceValue) ?? new object[0]).Cast<object>().ToList<object>();
			Type elementType = TypeHelper.GetElementType(context.SourceType, collection);
			Type elementType2 = TypeHelper.GetElementType(context.DestinationType);
			if (this.ShouldAssignEnumerable(context) && context.ConfigurationProvider.ResolveTypeMap(elementType, elementType2) == null)
			{
				return context.SourceValue;
			}
			int count = collection.Count;
			object orCreateDestinationObject = this.GetOrCreateDestinationObject(context, elementType2, count);
			TEnumerable enumerableFor = this.GetEnumerableFor(orCreateDestinationObject);
			this.ClearEnumerable(enumerableFor);
			int num = 0;
			foreach (object item in collection)
			{
				ResolutionContext context2 = context.CreateElementContext(null, item, elementType, elementType2, num);
				ResolutionResult resolutionResult = new ResolutionResult(context2);
				TypeMap typeMap = context.ConfigurationProvider.ResolveTypeMap(resolutionResult, elementType2);
				Type sourceElementType = (typeMap != null) ? typeMap.SourceType : elementType;
				Type destinationElementType = (typeMap != null) ? typeMap.DestinationType : elementType2;
				context2 = context.CreateElementContext(typeMap, item, sourceElementType, destinationElementType, num);
				object mappedValue = context.Engine.Map(context2);
				this.SetElementValue(enumerableFor, mappedValue, num);
				num++;
			}
			return orCreateDestinationObject;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000109FC File Offset: 0x0000EBFC
		protected virtual bool ShouldAssignEnumerable(ResolutionContext context)
		{
			return false;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010A00 File Offset: 0x0000EC00
		protected virtual object GetOrCreateDestinationObject(ResolutionContext context, Type destElementType, int sourceLength)
		{
			if (context.DestinationValue != null)
			{
				if (!(context.DestinationValue is Array))
				{
					return context.DestinationValue;
				}
				if (((Array)context.DestinationValue).Length >= sourceLength)
				{
					return context.DestinationValue;
				}
			}
			return this.CreateDestinationObject(context, destElementType, sourceLength);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010A4C File Offset: 0x0000EC4C
		protected virtual TEnumerable GetEnumerableFor(object destination)
		{
			return (TEnumerable)((object)destination);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000098B0 File Offset: 0x00007AB0
		protected virtual void ClearEnumerable(TEnumerable enumerable)
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010A54 File Offset: 0x0000EC54
		protected virtual object CreateDestinationObject(ResolutionContext context, Type destinationElementType, int count)
		{
			Type destinationType = context.DestinationType;
			if (!destinationType.IsInterface() && !destinationType.IsArray)
			{
				return context.Engine.CreateObject(context);
			}
			return this.CreateDestinationObjectBase(destinationElementType, count);
		}

		// Token: 0x06000404 RID: 1028
		public abstract bool IsMatch(TypePair context);

		// Token: 0x06000405 RID: 1029
		protected abstract void SetElementValue(TEnumerable destination, object mappedValue, int index);

		// Token: 0x06000406 RID: 1030
		protected abstract TEnumerable CreateDestinationObjectBase(Type destElementType, int sourceLength);
	}
}
