using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000081 RID: 129
	public class HashSetMapper : IObjectMapper
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public object Map(ResolutionContext context)
		{
			Type typeFromHandle = typeof(HashSetMapper.EnumerableMapper<, >);
			Type destinationType = context.DestinationType;
			Type elementType = TypeHelper.GetElementType(context.DestinationType);
			return ((IObjectMapper)Activator.CreateInstance(typeFromHandle.MakeGenericType(new Type[]
			{
				destinationType,
				elementType
			}))).Map(context);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001130D File Offset: 0x0000F50D
		public bool IsMatch(TypePair context)
		{
			return context.SourceType.IsEnumerableType() && HashSetMapper.IsSetType(context.DestinationType);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001132C File Offset: 0x0000F52C
		private static bool IsSetType(Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(ISet<>))
			{
				return true;
			}
			return (from t in type.GetTypeInfo().ImplementedInterfaces
			where t.IsGenericType()
			select t.GetGenericTypeDefinition()).Any((Type t) => t == typeof(ISet<>));
		}

		// Token: 0x0200013B RID: 315
		private class EnumerableMapper<TCollection, TElement> : EnumerableMapperBase<TCollection> where TCollection : ISet<TElement>
		{
			// Token: 0x06000911 RID: 2321 RVA: 0x00008F3F File Offset: 0x0000713F
			public override bool IsMatch(TypePair context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000912 RID: 2322 RVA: 0x00018C69 File Offset: 0x00016E69
			protected override void SetElementValue(TCollection destination, object mappedValue, int index)
			{
				destination.Add((TElement)((object)mappedValue));
			}

			// Token: 0x06000913 RID: 2323 RVA: 0x00018C7F File Offset: 0x00016E7F
			protected override void ClearEnumerable(TCollection enumerable)
			{
				enumerable.Clear();
			}

			// Token: 0x06000914 RID: 2324 RVA: 0x00018C90 File Offset: 0x00016E90
			protected override TCollection CreateDestinationObjectBase(Type destElementType, int sourceLength)
			{
				object obj;
				if (typeof(TCollection).IsInterface())
				{
					obj = new HashSet<TElement>();
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
