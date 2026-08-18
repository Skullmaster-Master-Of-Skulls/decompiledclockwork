using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Query.Dynamic
{
	// Token: 0x0200003D RID: 61
	internal class ClassFactory
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private ClassFactory()
		{
			List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
			ConstructorInfo constructor = typeof(SecurityRulesAttribute).GetConstructor(new Type[]
			{
				typeof(SecurityRuleSet)
			});
			CustomAttributeBuilder item = new CustomAttributeBuilder(constructor, new object[]
			{
				SecurityRuleSet.Level1
			});
			list.Add(item);
			AssemblyName name = new AssemblyName("DynamicClasses");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run, list);
			this.module = assemblyBuilder.DefineDynamicModule("Module");
			this.classes = new Dictionary<Signature, Type>();
			this.rwLock = new ReaderWriterLock();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
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
					LockCookie lockCookie = this.rwLock.UpgradeToWriterLock(-1);
					try
					{
						if (this.classes.TryGetValue(signature, out type))
						{
							return type;
						}
						Thread.MemoryBarrier();
						type = this.CreateDynamicClass(signature.properties);
						this.classes.Add(signature, type);
					}
					finally
					{
						this.rwLock.DowngradeFromWriterLock(ref lockCookie);
					}
				}
				result = type;
			}
			finally
			{
				this.rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DD68 File Offset: 0x0000BF68
		private Type CreateDynamicClass(DynamicProperty[] properties)
		{
			string name = "DynamicClass" + (this.classCount + 1).ToString();
			TypeBuilder typeBuilder = this.module.DefineType(name, TypeAttributes.Public, typeof(DynamicClass));
			FieldInfo[] fields = this.GenerateProperties(typeBuilder, properties);
			this.GenerateEquals(typeBuilder, fields);
			this.GenerateGetHashCode(typeBuilder, fields);
			Type result = typeBuilder.CreateType();
			this.classCount++;
			return result;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
		{
			FieldInfo[] array = new FieldBuilder[properties.Length];
			FieldInfo[] array2 = array;
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
				array2[i] = fieldBuilder;
			}
			return array2;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000DF1C File Offset: 0x0000C11C
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

		// Token: 0x06000241 RID: 577 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
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

		// Token: 0x040000E1 RID: 225
		public static readonly ClassFactory Instance = new ClassFactory();

		// Token: 0x040000E2 RID: 226
		private ModuleBuilder module;

		// Token: 0x040000E3 RID: 227
		private Dictionary<Signature, Type> classes;

		// Token: 0x040000E4 RID: 228
		private int classCount;

		// Token: 0x040000E5 RID: 229
		private ReaderWriterLock rwLock;
	}
}
