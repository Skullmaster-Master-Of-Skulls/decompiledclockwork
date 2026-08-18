using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x0200011A RID: 282
	internal static class TypeCacheUtil
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00013DBC File Offset: 0x00011FBC
		private static IEnumerable<Type> FilterTypesInAssemblies(IBuildManager buildManager, Predicate<Type> predicate)
		{
			IEnumerable<Type> enumerable = Type.EmptyTypes;
			ICollection referencedAssemblies = buildManager.GetReferencedAssemblies();
			foreach (object obj in referencedAssemblies)
			{
				Assembly assembly = (Assembly)obj;
				Type[] types;
				try
				{
					types = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					types = ex.Types;
				}
				enumerable = enumerable.Concat(types);
			}
			return from type in enumerable
			where TypeCacheUtil.TypeIsPublicClass(type) && predicate(type)
			select type;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00013E68 File Offset: 0x00012068
		public static List<Type> GetFilteredTypesFromAssemblies(string cacheName, Predicate<Type> predicate, IBuildManager buildManager)
		{
			TypeCacheSerializer serializer = new TypeCacheSerializer();
			List<Type> list = TypeCacheUtil.ReadTypesFromCache(cacheName, predicate, buildManager, serializer);
			if (list != null)
			{
				return list;
			}
			list = TypeCacheUtil.FilterTypesInAssemblies(buildManager, predicate).ToList<Type>();
			TypeCacheUtil.SaveTypesToCache(cacheName, list, buildManager, serializer);
			return list;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00013EC4 File Offset: 0x000120C4
		internal static List<Type> ReadTypesFromCache(string cacheName, Predicate<Type> predicate, IBuildManager buildManager, TypeCacheSerializer serializer)
		{
			try
			{
				Stream stream = buildManager.ReadCachedFile(cacheName);
				if (stream != null)
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						List<Type> list = serializer.DeserializeTypes(streamReader);
						if (list != null)
						{
							if (list.All((Type type) => TypeCacheUtil.TypeIsPublicClass(type) && predicate(type)))
							{
								return list;
							}
						}
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00013F50 File Offset: 0x00012150
		internal static void SaveTypesToCache(string cacheName, IList<Type> matchingTypes, IBuildManager buildManager, TypeCacheSerializer serializer)
		{
			try
			{
				Stream stream = buildManager.CreateCachedFile(cacheName);
				if (stream != null)
				{
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						serializer.SerializeTypes(matchingTypes, streamWriter);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00013FA4 File Offset: 0x000121A4
		private static bool TypeIsPublicClass(Type type)
		{
			return type != null && type.IsPublic && type.IsClass && !type.IsAbstract;
		}
	}
}
