using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Telerik.Web.UI
{
	// Token: 0x02001165 RID: 4453
	public class DynamicMethodDelegateFactory
	{
		// Token: 0x0600B58C RID: 46476 RVA: 0x0027FE50 File Offset: 0x0027E050
		public static DynamicMethodDelegate Create(MethodInfo method)
		{
			Type[] parameterTypes = new Type[]
			{
				typeof(object),
				typeof(object[])
			};
			DynamicMethod dynamicMethod = new DynamicMethod("", typeof(object), parameterTypes, typeof(DynamicMethodDelegateFactory));
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			if (!method.IsStatic)
			{
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Castclass, method.DeclaringType);
			}
			if (method.IsFinal)
			{
				ilgenerator.Emit(OpCodes.Call, method);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Callvirt, method);
			}
			if (method.ReturnType != typeof(void))
			{
				if (method.ReturnType.IsValueType)
				{
					ilgenerator.Emit(OpCodes.Box, method.ReturnType);
				}
			}
			else
			{
				ilgenerator.Emit(OpCodes.Ldnull);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return (DynamicMethodDelegate)dynamicMethod.CreateDelegate(typeof(DynamicMethodDelegate));
		}
	}
}
