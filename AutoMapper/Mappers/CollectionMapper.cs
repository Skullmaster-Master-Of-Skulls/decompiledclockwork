using System;
using System.Collections.Generic;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000071 RID: 113
	public class CollectionMapper : IObjectMapper
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00009928 File Offset: 0x00007B28
		public object Map(ResolutionContext context)
		{
			Type typeFromHandle = typeof(CollectionMapper.EnumerableMapper<, >);
			Type destinationType = context.DestinationType;
			Type elementType = TypeHelper.GetElementType(context.DestinationType);
			return ((IObjectMapper)Activator.CreateInstance(typeFromHandle.MakeGenericType(new Type[]
			{
				destinationType,
				elementType
			}))).Map(context);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00009975 File Offset: 0x00007B75
		public bool IsMatch(TypePair context)
		{
			return context.SourceType.IsEnumerableType() && context.DestinationType.IsCollectionType();
		}

		// Token: 0x02000133 RID: 307
		private class EnumerableMapper<TCollection, TElement> : EnumerableMapperBase<TCollection> where TCollection : ICollection<TElement>
		{
			// Token: 0x06000731 RID: 1841 RVA: 0x00008F3F File Offset: 0x0000713F
			public override bool IsMatch(TypePair context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x000173A4 File Offset: 0x000155A4
			protected override void SetElementValue(TCollection destination, object mappedValue, int index)
			{
				destination.Add((TElement)((object)mappedValue));
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x000173B9 File Offset: 0x000155B9
			protected override void ClearEnumerable(TCollection enumerable)
			{
				enumerable.Clear();
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x000173C8 File Offset: 0x000155C8
			protected override TCollection CreateDestinationObjectBase(Type destElementType, int sourceLength)
			{
				object obj;
				if (typeof(TCollection).IsInterface())
				{
					obj = new List<TElement>();
				}
				else
				{
					obj = ObjectCreator.CreateDefaultValue(typeof(TCollection));
				}
				return (TCollection)((object)obj);
			}
		}
	}
}
