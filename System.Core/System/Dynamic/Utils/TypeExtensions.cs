using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D9 RID: 217
	internal static class TypeExtensions
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x00015BB4 File Offset: 0x00013DB4
		internal static Delegate CreateDelegate(this MethodInfo methodInfo, Type delegateType, object target)
		{
			DynamicMethod dynamicMethod = methodInfo as DynamicMethod;
			if (dynamicMethod != null)
			{
				return dynamicMethod.CreateDelegate(delegateType, target);
			}
			return Delegate.CreateDelegate(delegateType, target, methodInfo);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00015BE2 File Offset: 0x00013DE2
		internal static Type GetReturnType(this MethodBase mi)
		{
			if (!mi.IsConstructor)
			{
				return ((MethodInfo)mi).ReturnType;
			}
			return mi.DeclaringType;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00015C00 File Offset: 0x00013E00
		internal static ParameterInfo[] GetParametersCached(this MethodBase method)
		{
			CacheDict<MethodBase, ParameterInfo[]> paramInfoCache = TypeExtensions._ParamInfoCache;
			ParameterInfo[] parameters;
			if (!paramInfoCache.TryGetValue(method, out parameters))
			{
				parameters = method.GetParameters();
				Type declaringType = method.DeclaringType;
				if (declaringType != null && declaringType.CanCache())
				{
					paramInfoCache[method] = parameters;
				}
			}
			return parameters;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00015C46 File Offset: 0x00013E46
		internal static bool IsByRefParameter(this ParameterInfo pi)
		{
			return pi.ParameterType.IsByRef || (pi.Attributes & ParameterAttributes.Out) == ParameterAttributes.Out;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00015C64 File Offset: 0x00013E64
		internal static MethodInfo GetMethodValidated(this Type type, string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			MethodInfo method = type.GetMethod(name, bindingAttr, binder, types, modifiers);
			if (!method.MatchesArgumentTypes(types))
			{
				return null;
			}
			return method;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00015C8C File Offset: 0x00013E8C
		private static bool MatchesArgumentTypes(this MethodInfo mi, Type[] argTypes)
		{
			if (mi == null || argTypes == null)
			{
				return false;
			}
			ParameterInfo[] parameters = mi.GetParameters();
			if (parameters.Length != argTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (!TypeUtils.AreReferenceAssignable(parameters[i].ParameterType, argTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040005C7 RID: 1479
		private static readonly CacheDict<MethodBase, ParameterInfo[]> _ParamInfoCache = new CacheDict<MethodBase, ParameterInfo[]>(75);
	}
}
