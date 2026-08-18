using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001D4 RID: 468
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal static class ReflectionUtil
	{
		// Token: 0x06001782 RID: 6018 RVA: 0x00049A68 File Offset: 0x00047C68
		public static void Reset<T>(T obj) where T : class
		{
			ReflectionUtil.ResetUtil<T>.ResetFn(obj);
		}

		// Token: 0x02000938 RID: 2360
		private static class ResetUtil<T> where T : class
		{
			// Token: 0x06006962 RID: 26978 RVA: 0x00177334 File Offset: 0x00175534
			private static Action<T> CreateResetFn()
			{
				Type typeFromHandle = typeof(T);
				DynamicMethod dynamicMethod = ReflectionUtil.ResetUtil<T>.CreateDynamicMethodWithAssert();
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					if (!fieldInfo.IsInitOnly && !fieldInfo.IsDefined(typeof(DoNotResetAttribute)))
					{
						ilgenerator.Emit(OpCodes.Ldarg_0);
						ilgenerator.Emit(OpCodes.Ldflda, fieldInfo);
						ilgenerator.Emit(OpCodes.Initobj, fieldInfo.FieldType);
					}
				}
				ilgenerator.Emit(OpCodes.Ret);
				return (Action<T>)dynamicMethod.CreateDelegate(typeof(Action<T>));
			}

			// Token: 0x06006963 RID: 26979 RVA: 0x001773E8 File Offset: 0x001755E8
			[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
			private static DynamicMethod CreateDynamicMethodWithAssert()
			{
				Type typeFromHandle = typeof(T);
				return new DynamicMethod("Reset-" + typeFromHandle.Name, typeof(void), new Type[]
				{
					typeFromHandle
				}, typeFromHandle, true);
			}

			// Token: 0x0400379A RID: 14234
			internal static readonly Action<T> ResetFn = ReflectionUtil.ResetUtil<T>.CreateResetFn();
		}
	}
}
