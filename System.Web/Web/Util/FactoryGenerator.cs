using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x02000762 RID: 1890
	internal class FactoryGenerator
	{
		// Token: 0x06005BE7 RID: 23527 RVA: 0x001709AC File Offset: 0x0016F9AC
		internal FactoryGenerator() : this(typeof(object), typeof(IWebObjectFactory))
		{
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x001709C8 File Offset: 0x0016F9C8
		private FactoryGenerator(Type returnedType, Type factoryInterface)
		{
			this._returnedType = returnedType;
			this._factoryInterface = factoryInterface;
			this._methodToOverride = factoryInterface.GetMethod("CreateInstance", new Type[0]);
			if (this._methodToOverride.ReturnType != this._returnedType)
			{
				throw new ArgumentException(SR.GetString("FactoryInterface"));
			}
			this._interfacesToImplement = new Type[1];
			this._interfacesToImplement[0] = factoryInterface;
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x00170A44 File Offset: 0x0016FA44
		internal static void CheckPublicParameterlessConstructor(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPublic && !type.IsNestedPublic)
			{
				throw new InvalidOperationException(SR.GetString("FactoryGenerator_TypeNotPublic", new object[]
				{
					type.Name
				}));
			}
			if (type.GetConstructor(Type.EmptyTypes) == null)
			{
				throw new InvalidOperationException(SR.GetString("FactoryGenerator_TypeHasNoParameterlessConstructor", new object[]
				{
					type.Name
				}));
			}
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x00170AC0 File Offset: 0x0016FAC0
		private static string GetUniqueCompilationName()
		{
			return Guid.NewGuid().ToString().Replace('-', '_');
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x00170AEC File Offset: 0x0016FAEC
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.ReflectionEmit)]
		private Type GetFactoryTypeWithAssert(Type type)
		{
			FactoryGenerator.CheckPublicParameterlessConstructor(type);
			if (this._dynamicModule == null)
			{
				lock (this)
				{
					if (this._dynamicModule == null)
					{
						string uniqueCompilationName = FactoryGenerator.GetUniqueCompilationName();
						AssemblyName assemblyName = new AssemblyName();
						assemblyName.Name = "A_" + uniqueCompilationName;
						AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, null, null, null, null, null, true);
						this._dynamicModule = assemblyBuilder.DefineDynamicModule("M_" + uniqueCompilationName);
					}
				}
			}
			string uniqueCompilationName2 = FactoryGenerator.GetUniqueCompilationName();
			TypeBuilder typeBuilder = this._dynamicModule.DefineType("T_" + uniqueCompilationName2, TypeAttributes.Public, typeof(object), this._interfacesToImplement);
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("CreateInstance", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, this._returnedType, null);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			typeBuilder.DefineMethodOverride(methodBuilder, this._methodToOverride);
			return typeBuilder.CreateType();
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x00170C0C File Offset: 0x0016FC0C
		internal IWebObjectFactory CreateFactory(Type type)
		{
			Type factoryTypeWithAssert = this.GetFactoryTypeWithAssert(type);
			return (IWebObjectFactory)Activator.CreateInstance(factoryTypeWithAssert);
		}

		// Token: 0x04003127 RID: 12583
		private Type _factoryInterface;

		// Token: 0x04003128 RID: 12584
		private Type _returnedType;

		// Token: 0x04003129 RID: 12585
		private MethodInfo _methodToOverride;

		// Token: 0x0400312A RID: 12586
		private ModuleBuilder _dynamicModule;

		// Token: 0x0400312B RID: 12587
		private Type[] _emptyParameterList = new Type[0];

		// Token: 0x0400312C RID: 12588
		private Type[] _interfacesToImplement;
	}
}
