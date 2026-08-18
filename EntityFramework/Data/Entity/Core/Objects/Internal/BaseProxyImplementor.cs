using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000575 RID: 1397
	internal class BaseProxyImplementor
	{
		// Token: 0x06003697 RID: 13975 RVA: 0x0010333B File Offset: 0x0010153B
		public BaseProxyImplementor()
		{
			this._baseGetters = new List<PropertyInfo>();
			this._baseSetters = new List<PropertyInfo>();
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x00103359 File Offset: 0x00101559
		public List<PropertyInfo> BaseGetters
		{
			get
			{
				return this._baseGetters;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x00103361 File Offset: 0x00101561
		public List<PropertyInfo> BaseSetters
		{
			get
			{
				return this._baseSetters;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x00103369 File Offset: 0x00101569
		public void AddBasePropertyGetter(PropertyInfo baseProperty)
		{
			this._baseGetters.Add(baseProperty);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x00103377 File Offset: 0x00101577
		public void AddBasePropertySetter(PropertyInfo baseProperty)
		{
			this._baseSetters.Add(baseProperty);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x00103385 File Offset: 0x00101585
		public void Implement(TypeBuilder typeBuilder)
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

		// Token: 0x0600369D RID: 13981 RVA: 0x001033B4 File Offset: 0x001015B4
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
				ilgenerator.Emit(OpCodes.Call, BaseProxyImplementor.StringEquals);
				ilgenerator.Emit(OpCodes.Brfalse_S, array[i]);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, this._baseGetters[i].Getter());
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(array[i]);
			}
			ilgenerator.Emit(OpCodes.Newobj, BaseProxyImplementor._invalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x001034E8 File Offset: 0x001016E8
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
				ilgenerator.Emit(OpCodes.Call, BaseProxyImplementor.StringEquals);
				ilgenerator.Emit(OpCodes.Brfalse_S, array[i]);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Castclass, this._baseSetters[i].PropertyType);
				ilgenerator.Emit(OpCodes.Call, this._baseSetters[i].Setter());
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(array[i]);
			}
			ilgenerator.Emit(OpCodes.Newobj, BaseProxyImplementor._invalidOperationConstructor);
			ilgenerator.Emit(OpCodes.Throw);
		}

		// Token: 0x040014D9 RID: 5337
		private readonly List<PropertyInfo> _baseGetters;

		// Token: 0x040014DA RID: 5338
		private readonly List<PropertyInfo> _baseSetters;

		// Token: 0x040014DB RID: 5339
		internal static readonly MethodInfo StringEquals = typeof(string).GetDeclaredMethod("op_Equality", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040014DC RID: 5340
		private static readonly ConstructorInfo _invalidOperationConstructor = typeof(InvalidOperationException).GetDeclaredConstructor(new Type[0]);
	}
}
