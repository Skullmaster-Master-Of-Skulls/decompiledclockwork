using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Compilation;
using System.Web.Http.Dispatcher;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000018 RID: 24
	internal sealed class WebHostHttpControllerTypeResolver : DefaultHttpControllerTypeResolver
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000402C File Offset: 0x0000222C
		public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
		{
			HttpControllerTypeCacheSerializer serializer = new HttpControllerTypeCacheSerializer();
			List<Type> list = WebHostHttpControllerTypeResolver.ReadTypesFromCache("MS-ApiControllerTypeCache.xml", this.IsControllerTypePredicate, serializer);
			if (list != null)
			{
				return list;
			}
			list = base.GetControllerTypes(assembliesResolver).ToList<Type>();
			WebHostHttpControllerTypeResolver.SaveTypesToCache("MS-ApiControllerTypeCache.xml", list, serializer);
			return list;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004088 File Offset: 0x00002288
		private static List<Type> ReadTypesFromCache(string cacheName, Predicate<Type> predicate, HttpControllerTypeCacheSerializer serializer)
		{
			try
			{
				Stream stream = BuildManager.ReadCachedFile(cacheName);
				if (stream != null)
				{
					using (StreamReader streamReader = new StreamReader(stream))
					{
						ICollection<Type> collection = serializer.DeserializeTypes(streamReader);
						if (collection != null)
						{
							if (collection.All((Type type) => predicate(type)))
							{
								return collection.ToList<Type>();
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

		// Token: 0x0600009A RID: 154 RVA: 0x00004118 File Offset: 0x00002318
		private static void SaveTypesToCache(string cacheName, IEnumerable<Type> matchingTypes, HttpControllerTypeCacheSerializer serializer)
		{
			try
			{
				Stream stream = BuildManager.CreateCachedFile(cacheName);
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

		// Token: 0x04000026 RID: 38
		private const string TypeCacheName = "MS-ApiControllerTypeCache.xml";
	}
}
