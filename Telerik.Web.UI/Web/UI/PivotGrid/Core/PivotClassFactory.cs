using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE2 RID: 3298
	[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Design choice.")]
	internal class PivotClassFactory
	{
		// Token: 0x06007B37 RID: 31543 RVA: 0x001C480C File Offset: 0x001C2A0C
		private PivotClassFactory()
		{
			AssemblyName name = new AssemblyName("DynamicClasses");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
			this.module = assemblyBuilder.DefineDynamicModule("Module");
			this.classes = new Dictionary<PivotSignature, Type>();
		}

		// Token: 0x1700276C RID: 10092
		// (get) Token: 0x06007B38 RID: 31544 RVA: 0x001C485E File Offset: 0x001C2A5E
		public static PivotClassFactory Instance
		{
			get
			{
				if (PivotClassFactory.instance.classCount > PivotClassFactory.instance.maxCachedClassesCount)
				{
					PivotClassFactory.instance = new PivotClassFactory();
				}
				return PivotClassFactory.instance;
			}
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x001C4888 File Offset: 0x001C2A88
		public Type GetDynamicClass(IEnumerable<PivotDynamicProperty> properties)
		{
			return this.GetOrCreateDynamicClass(properties);
		}

		// Token: 0x06007B3A RID: 31546 RVA: 0x001C48A0 File Offset: 0x001C2AA0
		private Type GetOrCreateDynamicClass(IEnumerable<PivotDynamicProperty> properties)
		{
			PivotSignature pivotSignature = new PivotSignature(properties);
			Type type;
			if (!this.classes.TryGetValue(pivotSignature, out type))
			{
				type = this.CreateDynamicClass(pivotSignature.Properties);
				this.classes.Add(pivotSignature, type);
			}
			return type;
		}

		// Token: 0x06007B3B RID: 31547 RVA: 0x001C48E0 File Offset: 0x001C2AE0
		private Type CreateDynamicClass(PivotDynamicProperty[] properties)
		{
			string name = "DynamicClass" + (this.classCount + 1);
			TypeBuilder typeBuilder = this.module.DefineType(name, TypeAttributes.Public, typeof(PivotDynamicClass));
			FieldInfo[] fields = this.GenerateProperties(typeBuilder, properties);
			this.GenerateEquals(typeBuilder, fields);
			this.GenerateGetHashCode(typeBuilder, fields);
			Type result = typeBuilder.CreateType();
			this.classCount++;
			return result;
		}

		// Token: 0x06007B3C RID: 31548 RVA: 0x001C4954 File Offset: 0x001C2B54
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Design choice.")]
		private FieldInfo[] GenerateProperties(TypeBuilder tb, PivotDynamicProperty[] properties)
		{
			FieldInfo[] array = new FieldBuilder[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
				PivotDynamicProperty pivotDynamicProperty = properties[i];
				FieldBuilder fieldBuilder = tb.DefineField("_" + pivotDynamicProperty.Name, pivotDynamicProperty.Type, FieldAttributes.Private);
				PropertyBuilder propertyBuilder = tb.DefineProperty(pivotDynamicProperty.Name, PropertyAttributes.HasDefault, pivotDynamicProperty.Type, null);
				MethodBuilder methodBuilder = tb.DefineMethod("get_" + pivotDynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, pivotDynamicProperty.Type, Type.EmptyTypes);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
				ilgenerator.Emit(OpCodes.Ret);
				MethodBuilder methodBuilder2 = tb.DefineMethod("set_" + pivotDynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null, new Type[]
				{
					pivotDynamicProperty.Type
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

		// Token: 0x06007B3D RID: 31549 RVA: 0x001C4A94 File Offset: 0x001C2C94
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Design choice.")]
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

		// Token: 0x06007B3E RID: 31550 RVA: 0x001C4C4C File Offset: 0x001C2E4C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Design choice.")]
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

		// Token: 0x040021B9 RID: 8633
		private static PivotClassFactory instance = new PivotClassFactory();

		// Token: 0x040021BA RID: 8634
		private ModuleBuilder module;

		// Token: 0x040021BB RID: 8635
		private Dictionary<PivotSignature, Type> classes;

		// Token: 0x040021BC RID: 8636
		private int classCount;

		// Token: 0x040021BD RID: 8637
		private int maxCachedClassesCount = 128;
	}
}
