using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001DB RID: 475
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	internal static class FastDelegateCreator<TDelegate> where TDelegate : class
	{
		// Token: 0x0600179F RID: 6047 RVA: 0x0004A14C File Offset: 0x0004834C
		internal static TDelegate BindTo(object obj, IntPtr method)
		{
			return FastDelegateCreator<TDelegate>._factory(obj, method);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0004A15C File Offset: 0x0004835C
		internal static TDelegate BindTo(object obj, MethodInfo method)
		{
			return FastDelegateCreator<TDelegate>.BindTo(obj, method.MethodHandle.GetFunctionPointer());
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004A180 File Offset: 0x00048380
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static Func<object, IntPtr, TDelegate> GetFactory()
		{
			ConstructorInfo constructor = typeof(TDelegate).GetConstructor(new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			});
			DynamicMethod dynamicMethod = new DynamicMethod("FastCreateDelegate_" + typeof(TDelegate).Name, typeof(TDelegate), new Type[]
			{
				typeof(object),
				typeof(IntPtr)
			}, typeof(FastDelegateCreator<TDelegate>), true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return (Func<object, IntPtr, TDelegate>)dynamicMethod.CreateDelegate(typeof(Func<object, IntPtr, TDelegate>));
		}

		// Token: 0x0400171F RID: 5919
		private static readonly Func<object, IntPtr, TDelegate> _factory = FastDelegateCreator<TDelegate>.GetFactory();
	}
}
