using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200008D RID: 141
	public class ReadOnlyCollectionMapper : IObjectMapper
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public object Map(ResolutionContext context)
		{
			Type typeFromHandle = typeof(ReadOnlyCollectionMapper.EnumerableMapper<>);
			Type elementType = TypeHelper.GetElementType(context.DestinationType);
			IObjectMapper objectMapper = (IObjectMapper)Activator.CreateInstance(typeFromHandle.MakeGenericType(new Type[]
			{
				elementType
			}));
			ResolutionContext context2 = (context.PropertyMap != null) ? context.CreateMemberContext(context.TypeMap, context.SourceValue, null, context.SourceType, context.PropertyMap) : context;
			return objectMapper.Map(context2);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00011A51 File Offset: 0x0000FC51
		public bool IsMatch(TypePair context)
		{
			return context.SourceType.IsEnumerableType() && context.DestinationType.IsGenericType() && context.DestinationType.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>);
		}

		// Token: 0x0200013F RID: 319
		private class EnumerableMapper<TElement> : EnumerableMapperBase<IList<TElement>>
		{
			// Token: 0x0600091F RID: 2335 RVA: 0x00008F3F File Offset: 0x0000713F
			public override bool IsMatch(TypePair context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x00018D3F File Offset: 0x00016F3F
			protected override void SetElementValue(IList<TElement> elements, object mappedValue, int index)
			{
				this.inner.Add((TElement)((object)mappedValue));
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x00018D52 File Offset: 0x00016F52
			protected override IList<TElement> GetEnumerableFor(object destination)
			{
				return this.inner;
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x00008F3F File Offset: 0x0000713F
			protected override IList<TElement> CreateDestinationObjectBase(Type destElementType, int sourceLength)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x00018D5A File Offset: 0x00016F5A
			protected override object CreateDestinationObject(ResolutionContext context, Type destinationElementType, int count)
			{
				return new ReadOnlyCollection<TElement>(this.inner);
			}

			// Token: 0x040003FD RID: 1021
			private readonly IList<TElement> inner = new List<TElement>();
		}
	}
}
