using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000076 RID: 118
	public class DictionaryMapper : IObjectMapper
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x000103E2 File Offset: 0x0000E5E2
		public bool IsMatch(TypePair context)
		{
			return context.SourceType.IsDictionaryType() && context.DestinationType.IsDictionaryType();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010400 File Offset: 0x0000E600
		public object Map(ResolutionContext context)
		{
			if (context.IsSourceValueNull && context.Engine.ShouldMapSourceCollectionAsNull(context))
			{
				return null;
			}
			Type dictionaryType = context.SourceType.GetDictionaryType();
			Type type = dictionaryType.GetTypeInfo().GenericTypeArguments[0];
			Type type2 = dictionaryType.GetTypeInfo().GenericTypeArguments[1];
			Type type3 = DictionaryMapper.KvpType.MakeGenericType(new Type[]
			{
				type,
				type2
			});
			Type dictionaryType2 = context.DestinationType.GetDictionaryType();
			Type type4 = dictionaryType2.GetTypeInfo().GenericTypeArguments[0];
			Type type5 = dictionaryType2.GetTypeInfo().GenericTypeArguments[1];
			IEnumerator keyValuePairEnumerator = DictionaryMapper.GetKeyValuePairEnumerator(context, type3);
			object obj = ObjectCreator.CreateDictionary(context.DestinationType, type4, type5);
			int num = 0;
			while (keyValuePairEnumerator.MoveNext())
			{
				object obj2 = keyValuePairEnumerator.Current;
				object value = type3.GetProperty("Key").GetValue(obj2, new object[0]);
				object value2 = type3.GetProperty("Value").GetValue(obj2, new object[0]);
				TypeMap elementTypeMap = context.ConfigurationProvider.ResolveTypeMap(value, null, type, type4);
				TypeMap elementTypeMap2 = context.ConfigurationProvider.ResolveTypeMap(value2, null, type2, type5);
				ResolutionContext context2 = context.CreateElementContext(elementTypeMap, value, type, type4, num);
				ResolutionContext context3 = context.CreateElementContext(elementTypeMap2, value2, type2, type5, num);
				object obj3 = context.Engine.Map(context2);
				object obj4 = context.Engine.Map(context3);
				dictionaryType2.GetMethod("Add").Invoke(obj, new object[]
				{
					obj3,
					obj4
				});
				num++;
			}
			return obj;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001058C File Offset: 0x0000E78C
		private static IEnumerator GetKeyValuePairEnumerator(ResolutionContext context, Type sourceKvpType)
		{
			if (context.SourceValue == null)
			{
				return Enumerable.Empty<object>().GetEnumerator();
			}
			IEnumerable enumerable = (IEnumerable)context.SourceValue;
			IEnumerable<object> enumerable2 = from e in enumerable.Cast<object>().OfType<DictionaryEntry>()
			select Activator.CreateInstance(sourceKvpType, new object[]
			{
				e.Key,
				e.Value
			});
			if (enumerable2.Any<object>())
			{
				return enumerable2.GetEnumerator();
			}
			Type type = typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				sourceKvpType
			});
			if (type.IsAssignableFrom(enumerable.GetType()))
			{
				return (IEnumerator)type.GetMethod("GetEnumerator").Invoke(enumerable, null);
			}
			throw new AutoMapperMappingException(context, "Cannot map dictionary type " + context.SourceType);
		}

		// Token: 0x040000C8 RID: 200
		private static readonly Type KvpType = typeof(KeyValuePair<, >);
	}
}
