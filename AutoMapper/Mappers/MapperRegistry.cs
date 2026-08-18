using System;
using System.Collections.Generic;

namespace AutoMapper.Mappers
{
	// Token: 0x02000084 RID: 132
	public static class MapperRegistry
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00011480 File Offset: 0x0000F680
		public static IList<IObjectMapper> Mappers
		{
			get
			{
				return MapperRegistry._mappers;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00011487 File Offset: 0x0000F687
		public static void Reset()
		{
			MapperRegistry._mappers.Clear();
			MapperRegistry._mappers.AddRange(MapperRegistry._initialMappers);
		}

		// Token: 0x040000CA RID: 202
		private static readonly IObjectMapper[] _initialMappers = new IObjectMapper[]
		{
			new ExpressionMapper(),
			new FlagsEnumMapper(),
			new EnumMapper(),
			new MultidimensionalArrayMapper(),
			new PrimitiveArrayMapper(),
			new ArrayMapper(),
			new EnumerableToDictionaryMapper(),
			new NameValueCollectionMapper(),
			new DictionaryMapper(),
			new ReadOnlyCollectionMapper(),
			new HashSetMapper(),
			new CollectionMapper(),
			new EnumerableMapper(),
			new StringMapper(),
			new AssignableMapper(),
			new TypeConverterMapper(),
			new NullableSourceMapper(),
			new ImplicitConversionOperatorMapper(),
			new ExplicitConversionOperatorMapper(),
			new ConvertMapper(),
			new FromStringDictionaryMapper(),
			new ToStringDictionaryMapper(),
			new FromDynamicMapper(),
			new ToDynamicMapper()
		};

		// Token: 0x040000CB RID: 203
		private static readonly List<IObjectMapper> _mappers = new List<IObjectMapper>(MapperRegistry._initialMappers);
	}
}
