using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000172 RID: 370
	internal class BaseProxyImplementor
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x0005CFF9 File Offset: 0x0005B1F9
		public BaseProxyImplementor()
		{
			this._baseGetters = new List<PropertyInfo>();
			this._baseSetters = new List<PropertyInfo>();
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0005D017 File Offset: 0x0005B217
		public List<PropertyInfo> BaseGetters
		{
			get
			{
				return this._baseGetters;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0005D01F File Offset: 0x0005B21F
		public List<PropertyInfo> BaseSetters
		{
			get
			{
				return this._baseSetters;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0005D027 File Offset: 0x0005B227
		public void AddBasePropertyGetter(PropertyInfo baseProperty)
		{
			this._baseGetters.Add(baseProperty);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0005D035 File Offset: 0x0005B235
		public void AddBasePropertySetter(PropertyInfo baseProperty)
		{
			this._baseSetters.Add(baseProperty);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0005D043 File Offset: 0x0005B243
		public void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			if (this._baseGetters.Count > 0)
			{
				this.ImplementBaseGetter(typeBuilder);
			}
			if (this._baseSetters.Count > 0)
			{
				this.ImplementBaseSetter(typeBuilder);
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0005D070 File Offset: 0x0005B270
		private void ImplementBaseGetter(TypeBuilder typeBuilder)
		{
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("GetBasePropertyValue", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig, typeof(object), new Type[]
			{
				typeof(string)
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label[] array = new Label[this._baseGetters.Count];
			for (int i = 0; i < this._baseGetters.Count; i++)
			{
				array[i] = ilgenerator.DefineLabel();
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldstr, this._baseGetters[i].Name);
				ilgenerator.Emit(OpCodes.Call, BaseProxyImplementor.s_StringEquals);
				ilgenerator.Emit(OpCodes.Brfalse_S, array[i]);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, this._baseGetters[i].GetGetMethod(true));
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(array[i]);
			}
			ilgenerator.Emit(OpCodes.Newobj, BaseProxyImplementor.s_InvalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0005D190 File Offset: 0x0005B390
		private void ImplementBaseSetter(TypeBuilder typeBuilder)
		{
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("SetBasePropertyValue", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig, typeof(void), new Type[]
			{
				typeof(string),
				typeof(object)
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label[] array = new Label[this._baseSetters.Count];
			for (int i = 0; i < this._baseSetters.Count; i++)
			{
				array[i] = ilgenerator.DefineLabel();
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldstr, this._baseSetters[i].Name);
				ilgenerator.Emit(OpCodes.Call, BaseProxyImplementor.s_StringEquals);
				ilgenerator.Emit(OpCodes.Brfalse_S, array[i]);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Castclass, this._baseSetters[i].PropertyType);
				ilgenerator.Emit(OpCodes.Call, this._baseSetters[i].GetSetMethod(true));
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(array[i]);
			}
			ilgenerator.Emit(OpCodes.Newobj, BaseProxyImplementor.s_InvalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
		}

		// Token: 0x04000B42 RID: 2882
		private readonly List<PropertyInfo> _baseGetters;

		// Token: 0x04000B43 RID: 2883
		private readonly List<PropertyInfo> _baseSetters;

		// Token: 0x04000B44 RID: 2884
		private static readonly MethodInfo s_StringEquals = typeof(string).GetMethod("op_Equality", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04000B45 RID: 2885
		private static readonly ConstructorInfo s_InvalidOperationConstructor = typeof(InvalidOperationException).GetConstructor(Type.EmptyTypes);
	}
}
