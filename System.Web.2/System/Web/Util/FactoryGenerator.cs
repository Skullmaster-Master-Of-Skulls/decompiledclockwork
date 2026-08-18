using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x020001FA RID: 506
	internal class FactoryGenerator
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0004CED5 File Offset: 0x0004B0D5
		internal FactoryGenerator() : this(typeof(object), typeof(IWebObjectFactory))
		{
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0004CEF4 File Offset: 0x0004B0F4
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

		// Token: 0x060018FD RID: 6397 RVA: 0x0004CF78 File Offset: 0x0004B178
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
			ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new InvalidOperationException(SR.GetString("FactoryGenerator_TypeHasNoParameterlessConstructor", new object[]
				{
					type.Name
				}));
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0004CFFC File Offset: 0x0004B1FC
		private static string GetUniqueCompilationName()
		{
			return Guid.NewGuid().ToString().Replace('-', '_');
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0004D028 File Offset: 0x0004B228
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
						AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, null, true, null);
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

		// Token: 0x06001900 RID: 6400 RVA: 0x0004D158 File Offset: 0x0004B358
		internal IWebObjectFactory CreateFactory(Type type)
		{
			Type factoryTypeWithAssert = this.GetFactoryTypeWithAssert(type);
			return (IWebObjectFactory)Activator.CreateInstance(factoryTypeWithAssert);
		}

		// Token: 0x04001799 RID: 6041
		private Type _factoryInterface;

		// Token: 0x0400179A RID: 6042
		private Type _returnedType;

		// Token: 0x0400179B RID: 6043
		private MethodInfo _methodToOverride;

		// Token: 0x0400179C RID: 6044
		private ModuleBuilder _dynamicModule;

		// Token: 0x0400179D RID: 6045
		private Type[] _emptyParameterList = new Type[0];

		// Token: 0x0400179E RID: 6046
		private Type[] _interfacesToImplement;
	}
}
