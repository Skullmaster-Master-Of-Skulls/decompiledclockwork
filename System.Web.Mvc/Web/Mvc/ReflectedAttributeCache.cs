using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000097 RID: 151
	internal static class ReflectedAttributeCache
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000C407 File Offset: 0x0000A607
		public static ReadOnlyCollection<FilterAttribute> GetTypeFilterAttributes(Type type)
		{
			return ReflectedAttributeCache.GetAttributes<Type, FilterAttribute>(ReflectedAttributeCache._typeFilterAttributeCache, type);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C414 File Offset: 0x0000A614
		public static ReadOnlyCollection<FilterAttribute> GetMethodFilterAttributes(MethodInfo methodInfo)
		{
			return ReflectedAttributeCache.GetAttributes<MethodInfo, FilterAttribute>(ReflectedAttributeCache._methodFilterAttributeCache, methodInfo);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000C421 File Offset: 0x0000A621
		public static ReadOnlyCollection<ActionMethodSelectorAttribute> GetActionMethodSelectorAttributesCollection(MethodInfo methodInfo)
		{
			return ReflectedAttributeCache.GetAttributes<MethodInfo, ActionMethodSelectorAttribute>(ReflectedAttributeCache._actionMethodSelectorAttributeCache, methodInfo);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C42E File Offset: 0x0000A62E
		public static ReadOnlyCollection<ActionNameSelectorAttribute> GetActionNameSelectorAttributes(MethodInfo methodInfo)
		{
			return ReflectedAttributeCache.GetAttributes<MethodInfo, ActionNameSelectorAttribute>(ReflectedAttributeCache._actionNameSelectorAttributeCache, methodInfo);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C43B File Offset: 0x0000A63B
		private static ReadOnlyCollection<TAttribute> GetAttributes<TMemberInfo, TAttribute>(ConcurrentDictionary<TMemberInfo, ReadOnlyCollection<TAttribute>> lookup, TMemberInfo memberInfo) where TMemberInfo : MemberInfo where TAttribute : Attribute
		{
			return lookup.GetOrAdd(memberInfo, ReflectedAttributeCache.CachedDelegates<TMemberInfo, TAttribute>.GetCustomAttributes);
		}

		// Token: 0x0400012B RID: 299
		private static readonly ConcurrentDictionary<MethodInfo, ReadOnlyCollection<ActionMethodSelectorAttribute>> _actionMethodSelectorAttributeCache = new ConcurrentDictionary<MethodInfo, ReadOnlyCollection<ActionMethodSelectorAttribute>>();

		// Token: 0x0400012C RID: 300
		private static readonly ConcurrentDictionary<MethodInfo, ReadOnlyCollection<ActionNameSelectorAttribute>> _actionNameSelectorAttributeCache = new ConcurrentDictionary<MethodInfo, ReadOnlyCollection<ActionNameSelectorAttribute>>();

		// Token: 0x0400012D RID: 301
		private static readonly ConcurrentDictionary<MethodInfo, ReadOnlyCollection<FilterAttribute>> _methodFilterAttributeCache = new ConcurrentDictionary<MethodInfo, ReadOnlyCollection<FilterAttribute>>();

		// Token: 0x0400012E RID: 302
		private static readonly ConcurrentDictionary<Type, ReadOnlyCollection<FilterAttribute>> _typeFilterAttributeCache = new ConcurrentDictionary<Type, ReadOnlyCollection<FilterAttribute>>();

		// Token: 0x02000098 RID: 152
		private static class CachedDelegates<TMemberInfo, TAttribute> where TMemberInfo : MemberInfo where TAttribute : Attribute
		{
			// Token: 0x0400012F RID: 303
			internal static Func<TMemberInfo, ReadOnlyCollection<TAttribute>> GetCustomAttributes = (TMemberInfo memberInfo) => new ReadOnlyCollection<TAttribute>((TAttribute[])memberInfo.GetCustomAttributes(typeof(TAttribute), true));
		}
	}
}
