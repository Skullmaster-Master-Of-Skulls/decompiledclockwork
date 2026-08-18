using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200021E RID: 542
	internal class EmitterCache
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0003CD5C File Offset: 0x0003AF5C
		internal static EmitterCache TypeEmitter
		{
			get
			{
				object obj = EmitterCache.initLock;
				lock (obj)
				{
					if (EmitterCache.Provider == null)
					{
						EmitterCache provider = new EmitterCache();
						Thread.MemoryBarrier();
						EmitterCache.Provider = provider;
					}
				}
				if (EmitterCache.Provider == null)
				{
					throw Fx.AssertAndThrowFatal("Provider should not be null");
				}
				return EmitterCache.Provider;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003CDC4 File Offset: 0x0003AFC4
		private EmitterCache()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = Guid.NewGuid().ToString();
			this.assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			this.DynamicModule = this.assemblyBuilder.DefineDynamicModule(Guid.NewGuid().ToString());
			this.interfaceToClassMap = new Dictionary<Type, Type>();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003CE38 File Offset: 0x0003B038
		private Type[] GetParameterTypes(MethodInfo mInfo)
		{
			ParameterInfo[] parameters = mInfo.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003CE70 File Offset: 0x0003B070
		internal Type FindOrCreateType(Type interfaceType)
		{
			if (!interfaceType.IsInterface)
			{
				throw Fx.AssertAndThrow("Passed in type should be an Interface");
			}
			Type type = null;
			lock (this)
			{
				this.interfaceToClassMap.TryGetValue(interfaceType, out type);
				if (type == null)
				{
					TypeBuilder typeBuilder = this.DynamicModule.DefineType(interfaceType.Name + "MarshalByRefObject", TypeAttributes.Public | TypeAttributes.Abstract, typeof(MarshalByRefObject), new Type[]
					{
						interfaceType
					});
					Type[] types = new Type[]
					{
						typeof(ClassInterfaceType)
					};
					ConstructorInfo constructor = typeof(ClassInterfaceAttribute).GetConstructor(types);
					CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, new object[]
					{
						ClassInterfaceType.None
					});
					typeBuilder.SetCustomAttribute(customAttribute);
					typeBuilder.AddInterfaceImplementation(interfaceType);
					foreach (MethodInfo methodInfo in interfaceType.GetMethods())
					{
						MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.Abstract, methodInfo.ReturnType, this.GetParameterTypes(methodInfo));
					}
					type = typeBuilder.CreateType();
					this.interfaceToClassMap[interfaceType] = type;
				}
			}
			if (type == null)
			{
				throw Fx.AssertAndThrow("Class Type should not be null at this point");
			}
			return type;
		}

		// Token: 0x0400187B RID: 6267
		private static EmitterCache Provider = null;

		// Token: 0x0400187C RID: 6268
		private static object initLock = new object();

		// Token: 0x0400187D RID: 6269
		private ModuleBuilder DynamicModule;

		// Token: 0x0400187E RID: 6270
		private AssemblyBuilder assemblyBuilder;

		// Token: 0x0400187F RID: 6271
		private Dictionary<Type, Type> interfaceToClassMap;
	}
}
