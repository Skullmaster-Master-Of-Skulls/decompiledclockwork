using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200013A RID: 314
	[__DynamicallyInvokable]
	public class CallSite
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x000249BB File Offset: 0x00022BBB
		internal CallSite(CallSiteBinder binder)
		{
			this._binder = binder;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000249CA File Offset: 0x00022BCA
		[__DynamicallyInvokable]
		public CallSiteBinder Binder
		{
			[__DynamicallyInvokable]
			get
			{
				return this._binder;
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000249D4 File Offset: 0x00022BD4
		[__DynamicallyInvokable]
		public static CallSite Create(Type delegateType, CallSiteBinder binder)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			CacheDict<Type, Func<CallSiteBinder, CallSite>> cacheDict = CallSite._SiteCtors;
			if (cacheDict == null)
			{
				cacheDict = (CallSite._SiteCtors = new CacheDict<Type, Func<CallSiteBinder, CallSite>>(100));
			}
			MethodInfo methodInfo = null;
			Func<CallSiteBinder, CallSite> func;
			if (!cacheDict.TryGetValue(delegateType, out func))
			{
				methodInfo = typeof(CallSite<>).MakeGenericType(new Type[]
				{
					delegateType
				}).GetMethod("Create");
				if (delegateType.CanCache())
				{
					func = (Func<CallSiteBinder, CallSite>)Delegate.CreateDelegate(typeof(Func<CallSiteBinder, CallSite>), methodInfo);
					cacheDict.Add(delegateType, func);
				}
			}
			if (func != null)
			{
				return func(binder);
			}
			return (CallSite)methodInfo.Invoke(null, new object[]
			{
				binder
			});
		}

		// Token: 0x04000762 RID: 1890
		private static volatile CacheDict<Type, Func<CallSiteBinder, CallSite>> _SiteCtors;

		// Token: 0x04000763 RID: 1891
		internal readonly CallSiteBinder _binder;

		// Token: 0x04000764 RID: 1892
		internal bool _match;
	}
}
