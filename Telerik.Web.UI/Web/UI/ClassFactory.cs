using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Telerik.Web.UI
{
	// Token: 0x02000375 RID: 885
	internal class ClassFactory
	{
		// Token: 0x06001E46 RID: 7750 RVA: 0x0005E5A0 File Offset: 0x0005C7A0
		private ClassFactory()
		{
			AssemblyName name = new AssemblyName("DynamicClasses");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
			this.module = assemblyBuilder.DefineDynamicModule("Module");
			this.classes = new Dictionary<Signature, Type>();
			this.rwLock = new ReaderWriterLock();
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0005E5F4 File Offset: 0x0005C7F4
		public Type GetDynamicClass(IEnumerable<DynamicProperty> properties)
		{
			this.rwLock.AcquireReaderLock(-1);
			Type result;
			try
			{
				Signature signature = new Signature(properties);
				Type type;
				if (!this.classes.TryGetValue(signature, out type))
				{
					type = this.CreateDynamicClass(signature.properties);
					this.classes.Add(signature, type);
				}
				result = type;
			}
			finally
			{
				this.rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0005E660 File Offset: 0x0005C860
		private Type CreateDynamicClass(DynamicProperty[] properties)
		{
			LockCookie lockCookie = this.rwLock.UpgradeToWriterLock(-1);
			Type result;
			try
			{
				string name = "DynamicClass" + (this.classCount + 1);
				TypeBuilder typeBuilder = this.module.DefineType(name, TypeAttributes.Public, typeof(DynamicClass));
				FieldInfo[] fields = this.GenerateProperties(typeBuilder, properties);
				this.GenerateEquals(typeBuilder, fields);
				this.GenerateGetHashCode(typeBuilder, fields);
				Type type = typeBuilder.CreateType();
				this.classCount++;
				result = type;
			}
			finally
			{
				this.rwLock.DowngradeFromWriterLock(ref lockCookie);
			}
			return result;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0005E700 File Offset: 0x0005C900
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
		{
			FieldInfo[] array = new FieldBuilder[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
				DynamicProperty dynamicProperty = properties[i];
				FieldBuilder fieldBuilder = tb.DefineField("_" + dynamicProperty.Name, dynamicProperty.Type, FieldAttributes.Private);
				PropertyBuilder propertyBuilder = tb.DefineProperty(dynamicProperty.Name, PropertyAttributes.HasDefault, dynamicProperty.Type, null);
				MethodBuilder methodBuilder = tb.DefineMethod("get_" + dynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, dynamicProperty.Type, Type.EmptyTypes);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
				ilgenerator.Emit(OpCodes.Ret);
				MethodBuilder methodBuilder2 = tb.DefineMethod("set_" + dynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null, new Type[]
				{
					dynamicProperty.Type
				});
				ILGenerator ilgenerator2 = methodBuilder2.GetILGenerator();
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				ilgenerator2.Emit(OpCodes.Ldarg_1);
				ilgenerator2.Emit(OpCodes.Stfld, fieldBuilder);
				ilgenerator2.Emit(OpCodes.Ret);
				propertyBuilder.SetGetMethod(methodBuilder);
				propertyBuilder.SetSetMethod(methodBuilder2);
				array[i] = fieldBuilder;
			}
			return array;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0005E840 File Offset: 0x0005CA40
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
		{
			MethodBuilder methodBuilder = tb.DefineMethod("Equals", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new Type[]
			{
				typeof(object)
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			LocalBuilder local = ilgenerator.DeclareLocal(tb);
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Isinst, tb);
			ilgenerator.Emit(OpCodes.Stloc, local);
			ilgenerator.Emit(OpCodes.Ldloc, local);
			ilgenerator.Emit(OpCodes.Brtrue_S, label);
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Ret);
			ilgenerator.MarkLabel(label);
			foreach (FieldInfo fieldInfo in fields)
			{
				Type fieldType = fieldInfo.FieldType;
				Type type = typeof(EqualityComparer<>).MakeGenericType(new Type[]
				{
					fieldType
				});
				label = ilgenerator.DefineLabel();
				ilgenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.Emit(OpCodes.Ldloc, local);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("Equals", new Type[]
				{
					fieldType,
					fieldType
				}), null);
				ilgenerator.Emit(OpCodes.Brtrue_S, label);
				ilgenerator.Emit(OpCodes.Ldc_I4_0);
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(label);
			}
			ilgenerator.Emit(OpCodes.Ldc_I4_1);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0005E9F8 File Offset: 0x0005CBF8
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
		{
			MethodBuilder methodBuilder = tb.DefineMethod("GetHashCode", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int), Type.EmptyTypes);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			foreach (FieldInfo fieldInfo in fields)
			{
				Type fieldType = fieldInfo.FieldType;
				Type type = typeof(EqualityComparer<>).MakeGenericType(new Type[]
				{
					fieldType
				});
				ilgenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("GetHashCode", new Type[]
				{
					fieldType
				}), null);
				ilgenerator.Emit(OpCodes.Xor);
			}
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x04000781 RID: 1921
		public static readonly ClassFactory Instance = new ClassFactory();

		// Token: 0x04000782 RID: 1922
		private ModuleBuilder module;

		// Token: 0x04000783 RID: 1923
		private Dictionary<Signature, Type> classes;

		// Token: 0x04000784 RID: 1924
		private int classCount;

		// Token: 0x04000785 RID: 1925
		private ReaderWriterLock rwLock;
	}
}
