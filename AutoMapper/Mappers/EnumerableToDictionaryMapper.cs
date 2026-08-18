using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200007C RID: 124
	public class EnumerableToDictionaryMapper : IObjectMapper
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x00010A92 File Offset: 0x0000EC92
		public bool IsMatch(TypePair context)
		{
			return context.DestinationType.IsDictionaryType() && context.SourceType.IsEnumerableType() && !context.SourceType.IsDictionaryType();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		public object Map(ResolutionContext context)
		{
			IEnumerable enumerable = ((IEnumerable)context.SourceValue) ?? new object[0];
			IEnumerable<object> enumerable2 = enumerable.Cast<object>();
			Type elementType = TypeHelper.GetElementType(context.SourceType, enumerable);
			Type dictionaryType = context.DestinationType.GetDictionaryType();
			Type type = dictionaryType.GetTypeInfo().GenericTypeArguments[0];
			Type type2 = dictionaryType.GetTypeInfo().GenericTypeArguments[1];
			Type type3 = EnumerableToDictionaryMapper.KvpType.MakeGenericType(new Type[]
			{
				type,
				type2
			});
			object obj = ObjectCreator.CreateDictionary(context.DestinationType, type, type2);
			int num = 0;
			foreach (object obj2 in enumerable2)
			{
				TypeMap typeMap = context.ConfigurationProvider.ResolveTypeMap(obj2, null, elementType, type3);
				Type sourceElementType = (typeMap != null) ? typeMap.SourceType : elementType;
				Type destinationElementType = (typeMap != null) ? typeMap.DestinationType : type3;
				ResolutionContext context2 = context.CreateElementContext(typeMap, obj2, sourceElementType, destinationElementType, num);
				object obj3 = context.Engine.Map(context2);
				object value = obj3.GetType().GetProperty("Key").GetValue(obj3, null);
				object value2 = obj3.GetType().GetProperty("Value").GetValue(obj3, null);
				dictionaryType.GetMethod("Add").Invoke(obj, new object[]
				{
					value,
					value2
				});
				num++;
			}
			return obj;
		}

		// Token: 0x040000C9 RID: 201
		private static readonly Type KvpType = typeof(KeyValuePair<, >);
	}
}
