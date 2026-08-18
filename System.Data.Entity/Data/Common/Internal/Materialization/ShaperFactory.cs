using System;
using System.Data.Common.QueryCache;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.Internal;
using System.Data.Query.InternalTrees;
using System.Runtime.CompilerServices;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D2 RID: 978
	internal abstract class ShaperFactory
	{
		// Token: 0x060034CF RID: 13519 RVA: 0x000CBEE8 File Offset: 0x000CA0E8
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static ShaperFactory Create(Type elementType, QueryCacheManager cacheManager, ColumnMap columnMap, MetadataWorkspace metadata, SpanIndex spanInfo, MergeOption mergeOption, bool valueLayer)
		{
			ShaperFactory.ShaperFactoryCreator shaperFactoryCreator = (ShaperFactory.ShaperFactoryCreator)Activator.CreateInstance(typeof(ShaperFactory.TypedShaperFactoryCreator<>).MakeGenericType(new Type[]
			{
				elementType
			}));
			return shaperFactoryCreator.TypedCreate(cacheManager, columnMap, metadata, spanInfo, mergeOption, valueLayer);
		}

		// Token: 0x0200069E RID: 1694
		private abstract class ShaperFactoryCreator
		{
			// Token: 0x06004574 RID: 17780
			internal abstract ShaperFactory TypedCreate(QueryCacheManager cacheManager, ColumnMap columnMap, MetadataWorkspace metadata, SpanIndex spanInfo, MergeOption mergeOption, bool valueLayer);
		}

		// Token: 0x0200069F RID: 1695
		private sealed class TypedShaperFactoryCreator<T> : ShaperFactory.ShaperFactoryCreator
		{
			// Token: 0x06004577 RID: 17783 RVA: 0x000F9F32 File Offset: 0x000F8132
			internal override ShaperFactory TypedCreate(QueryCacheManager cacheManager, ColumnMap columnMap, MetadataWorkspace metadata, SpanIndex spanInfo, MergeOption mergeOption, bool valueLayer)
			{
				return Translator.TranslateColumnMap<T>(cacheManager, columnMap, metadata, spanInfo, mergeOption, valueLayer);
			}
		}
	}
}
