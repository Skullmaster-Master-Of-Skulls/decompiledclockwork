using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x0200027C RID: 636
	internal static class DelegateHelpers
	{
		// Token: 0x060016A2 RID: 5794 RVA: 0x0004B0B0 File Offset: 0x000492B0
		private static Type MakeNewCustomDelegate(Type[] types)
		{
			Type returnType = types[types.Length - 1];
			Type[] parameterTypes = types.RemoveLast<Type>();
			TypeBuilder typeBuilder = AssemblyGen.DefineDelegateType("Delegate" + types.Length.ToString());
			typeBuilder.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName, CallingConventions.Standard, DelegateHelpers._DelegateCtorSignature).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			typeBuilder.DefineMethod("Invoke", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, returnType, parameterTypes).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			return typeBuilder.CreateType();
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0004B11C File Offset: 0x0004931C
		internal static Type MakeDelegateType(Type[] types)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			Type delegateType;
			lock (delegateCache)
			{
				DelegateHelpers.TypeInfo typeInfo = DelegateHelpers._DelegateCache;
				for (int i = 0; i < types.Length; i++)
				{
					typeInfo = DelegateHelpers.NextTypeInfo(types[i], typeInfo);
				}
				if (typeInfo.DelegateType == null)
				{
					typeInfo.DelegateType = DelegateHelpers.MakeNewDelegate((Type[])types.Clone());
				}
				delegateType = typeInfo.DelegateType;
			}
			return delegateType;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0004B1A4 File Offset: 0x000493A4
		internal static Type MakeCallSiteDelegate(ReadOnlyCollection<Expression> types, Type returnType)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			Type delegateType;
			lock (delegateCache)
			{
				DelegateHelpers.TypeInfo typeInfo = DelegateHelpers._DelegateCache;
				typeInfo = DelegateHelpers.NextTypeInfo(typeof(CallSite), typeInfo);
				for (int i = 0; i < types.Count; i++)
				{
					typeInfo = DelegateHelpers.NextTypeInfo(types[i].Type, typeInfo);
				}
				typeInfo = DelegateHelpers.NextTypeInfo(returnType, typeInfo);
				if (typeInfo.DelegateType == null)
				{
					typeInfo.MakeDelegateType(returnType, types);
				}
				delegateType = typeInfo.DelegateType;
			}
			return delegateType;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0004B244 File Offset: 0x00049444
		internal static Type MakeDeferredSiteDelegate(DynamicMetaObject[] args, Type returnType)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			Type delegateType;
			lock (delegateCache)
			{
				DelegateHelpers.TypeInfo typeInfo = DelegateHelpers._DelegateCache;
				typeInfo = DelegateHelpers.NextTypeInfo(typeof(CallSite), typeInfo);
				foreach (DynamicMetaObject dynamicMetaObject in args)
				{
					Type type = dynamicMetaObject.Expression.Type;
					if (DelegateHelpers.IsByRef(dynamicMetaObject))
					{
						type = type.MakeByRefType();
					}
					typeInfo = DelegateHelpers.NextTypeInfo(type, typeInfo);
				}
				typeInfo = DelegateHelpers.NextTypeInfo(returnType, typeInfo);
				if (typeInfo.DelegateType == null)
				{
					Type[] array = new Type[args.Length + 2];
					array[0] = typeof(CallSite);
					array[array.Length - 1] = returnType;
					for (int j = 0; j < args.Length; j++)
					{
						DynamicMetaObject dynamicMetaObject2 = args[j];
						Type type2 = dynamicMetaObject2.Expression.Type;
						if (DelegateHelpers.IsByRef(dynamicMetaObject2))
						{
							type2 = type2.MakeByRefType();
						}
						array[j + 1] = type2;
					}
					typeInfo.DelegateType = DelegateHelpers.MakeNewDelegate(array);
				}
				delegateType = typeInfo.DelegateType;
			}
			return delegateType;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0004B364 File Offset: 0x00049564
		private static bool IsByRef(DynamicMetaObject mo)
		{
			ParameterExpression parameterExpression = mo.Expression as ParameterExpression;
			return parameterExpression != null && parameterExpression.IsByRef;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0004B388 File Offset: 0x00049588
		internal static DelegateHelpers.TypeInfo NextTypeInfo(Type initialArg)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			DelegateHelpers.TypeInfo result;
			lock (delegateCache)
			{
				result = DelegateHelpers.NextTypeInfo(initialArg, DelegateHelpers._DelegateCache);
			}
			return result;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0004B3D0 File Offset: 0x000495D0
		internal static DelegateHelpers.TypeInfo GetNextTypeInfo(Type initialArg, DelegateHelpers.TypeInfo curTypeInfo)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			DelegateHelpers.TypeInfo result;
			lock (delegateCache)
			{
				result = DelegateHelpers.NextTypeInfo(initialArg, curTypeInfo);
			}
			return result;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0004B414 File Offset: 0x00049614
		private static DelegateHelpers.TypeInfo NextTypeInfo(Type initialArg, DelegateHelpers.TypeInfo curTypeInfo)
		{
			if (curTypeInfo.TypeChain == null)
			{
				curTypeInfo.TypeChain = new Dictionary<Type, DelegateHelpers.TypeInfo>();
			}
			DelegateHelpers.TypeInfo typeInfo;
			if (!curTypeInfo.TypeChain.TryGetValue(initialArg, out typeInfo))
			{
				typeInfo = new DelegateHelpers.TypeInfo();
				if (initialArg.CanCache())
				{
					curTypeInfo.TypeChain[initialArg] = typeInfo;
				}
			}
			return typeInfo;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0004B464 File Offset: 0x00049664
		private static Type MakeNewDelegate(Type[] types)
		{
			if (types.Length <= 17)
			{
				if (!types.Any((Type t) => t.IsByRef))
				{
					Type result;
					if (types[types.Length - 1] == typeof(void))
					{
						result = DelegateHelpers.GetActionType(types.RemoveLast<Type>());
					}
					else
					{
						result = DelegateHelpers.GetFuncType(types);
					}
					return result;
				}
			}
			return DelegateHelpers.MakeNewCustomDelegate(types);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0004B4D4 File Offset: 0x000496D4
		internal static Type GetFuncType(Type[] types)
		{
			switch (types.Length)
			{
			case 1:
				return typeof(Func<>).MakeGenericType(types);
			case 2:
				return typeof(Func<, >).MakeGenericType(types);
			case 3:
				return typeof(Func<, , >).MakeGenericType(types);
			case 4:
				return typeof(Func<, , , >).MakeGenericType(types);
			case 5:
				return typeof(Func<, , , , >).MakeGenericType(types);
			case 6:
				return typeof(Func<, , , , , >).MakeGenericType(types);
			case 7:
				return typeof(Func<, , , , , , >).MakeGenericType(types);
			case 8:
				return typeof(Func<, , , , , , , >).MakeGenericType(types);
			case 9:
				return typeof(Func<, , , , , , , , >).MakeGenericType(types);
			case 10:
				return typeof(Func<, , , , , , , , , >).MakeGenericType(types);
			case 11:
				return typeof(Func<, , , , , , , , , , >).MakeGenericType(types);
			case 12:
				return typeof(Func<, , , , , , , , , , , >).MakeGenericType(types);
			case 13:
				return typeof(Func<, , , , , , , , , , , , >).MakeGenericType(types);
			case 14:
				return typeof(Func<, , , , , , , , , , , , , >).MakeGenericType(types);
			case 15:
				return typeof(Func<, , , , , , , , , , , , , , >).MakeGenericType(types);
			case 16:
				return typeof(Func<, , , , , , , , , , , , , , , >).MakeGenericType(types);
			case 17:
				return typeof(Func<, , , , , , , , , , , , , , , , >).MakeGenericType(types);
			default:
				return null;
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0004B658 File Offset: 0x00049858
		internal static Type GetActionType(Type[] types)
		{
			switch (types.Length)
			{
			case 0:
				return typeof(Action);
			case 1:
				return typeof(Action<>).MakeGenericType(types);
			case 2:
				return typeof(Action<, >).MakeGenericType(types);
			case 3:
				return typeof(Action<, , >).MakeGenericType(types);
			case 4:
				return typeof(Action<, , , >).MakeGenericType(types);
			case 5:
				return typeof(Action<, , , , >).MakeGenericType(types);
			case 6:
				return typeof(Action<, , , , , >).MakeGenericType(types);
			case 7:
				return typeof(Action<, , , , , , >).MakeGenericType(types);
			case 8:
				return typeof(Action<, , , , , , , >).MakeGenericType(types);
			case 9:
				return typeof(Action<, , , , , , , , >).MakeGenericType(types);
			case 10:
				return typeof(Action<, , , , , , , , , >).MakeGenericType(types);
			case 11:
				return typeof(Action<, , , , , , , , , , >).MakeGenericType(types);
			case 12:
				return typeof(Action<, , , , , , , , , , , >).MakeGenericType(types);
			case 13:
				return typeof(Action<, , , , , , , , , , , , >).MakeGenericType(types);
			case 14:
				return typeof(Action<, , , , , , , , , , , , , >).MakeGenericType(types);
			case 15:
				return typeof(Action<, , , , , , , , , , , , , , >).MakeGenericType(types);
			case 16:
				return typeof(Action<, , , , , , , , , , , , , , , >).MakeGenericType(types);
			default:
				return null;
			}
		}

		// Token: 0x04000B41 RID: 2881
		private const MethodAttributes CtorAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName;

		// Token: 0x04000B42 RID: 2882
		private const MethodImplAttributes ImplAttributes = MethodImplAttributes.CodeTypeMask;

		// Token: 0x04000B43 RID: 2883
		private const MethodAttributes InvokeAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask;

		// Token: 0x04000B44 RID: 2884
		private static readonly Type[] _DelegateCtorSignature = new Type[]
		{
			typeof(object),
			typeof(IntPtr)
		};

		// Token: 0x04000B45 RID: 2885
		private static DelegateHelpers.TypeInfo _DelegateCache = new DelegateHelpers.TypeInfo();

		// Token: 0x04000B46 RID: 2886
		private const int MaximumArity = 17;

		// Token: 0x0200044F RID: 1103
		internal class TypeInfo
		{
			// Token: 0x06001FC7 RID: 8135 RVA: 0x0006F2F5 File Offset: 0x0006D4F5
			public Type MakeDelegateType(Type retType, params Expression[] args)
			{
				return this.MakeDelegateType(retType, args);
			}

			// Token: 0x06001FC8 RID: 8136 RVA: 0x0006F300 File Offset: 0x0006D500
			public Type MakeDelegateType(Type retType, IList<Expression> args)
			{
				Type[] array = new Type[args.Count + 2];
				array[0] = typeof(CallSite);
				array[array.Length - 1] = retType;
				for (int i = 0; i < args.Count; i++)
				{
					array[i + 1] = args[i].Type;
				}
				return this.DelegateType = DelegateHelpers.MakeNewDelegate(array);
			}

			// Token: 0x040012D5 RID: 4821
			public Type DelegateType;

			// Token: 0x040012D6 RID: 4822
			public Dictionary<Type, DelegateHelpers.TypeInfo> TypeChain;
		}
	}
}
