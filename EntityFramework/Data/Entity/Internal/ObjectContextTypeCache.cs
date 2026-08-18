using System;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000780 RID: 1920
	internal static class ObjectContextTypeCache
	{
		// Token: 0x060056FA RID: 22266 RVA: 0x00177FA1 File Offset: 0x001761A1
		public static Type GetObjectType(Type type)
		{
			return ObjectContextTypeCache._typeCache.GetOrAdd(type, new Func<Type, Type>(ObjectContext.GetObjectType));
		}

		// Token: 0x0400231D RID: 8989
		private static readonly ConcurrentDictionary<Type, Type> _typeCache = new ConcurrentDictionary<Type, Type>();
	}
}
