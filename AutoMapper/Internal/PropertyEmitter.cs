using System;
using System.Reflection;
using System.Reflection.Emit;

namespace AutoMapper.Internal
{
	// Token: 0x020000B4 RID: 180
	public class PropertyEmitter
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00014048 File Offset: 0x00012248
		public PropertyEmitter(TypeBuilder owner, string name, Type propertyType, FieldBuilder propertyChangedField)
		{
			this.fieldBuilder = owner.DefineField(string.Format("<{0}>", name), propertyType, FieldAttributes.Private);
			this.getterBuilder = owner.DefineMethod(string.Format("get_{0}", name), MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, propertyType, Type.EmptyTypes);
			ILGenerator ilgenerator = this.getterBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, this.fieldBuilder);
			ilgenerator.Emit(OpCodes.Ret);
			this.setterBuilder = owner.DefineMethod(string.Format("set_{0}", name), MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, typeof(void), new Type[]
			{
				propertyType
			});
			ILGenerator ilgenerator2 = this.setterBuilder.GetILGenerator();
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldarg_1);
			ilgenerator2.Emit(OpCodes.Stfld, this.fieldBuilder);
			if (propertyChangedField != null)
			{
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				ilgenerator2.Emit(OpCodes.Dup);
				ilgenerator2.Emit(OpCodes.Ldfld, propertyChangedField);
				ilgenerator2.Emit(OpCodes.Ldstr, name);
				ilgenerator2.Emit(OpCodes.Call, PropertyEmitter.proxyBase_NotifyPropertyChanged);
			}
			ilgenerator2.Emit(OpCodes.Ret);
			this.propertyBuilder = owner.DefineProperty(name, PropertyAttributes.None, propertyType, null);
			this.propertyBuilder.SetGetMethod(this.getterBuilder);
			this.propertyBuilder.SetSetMethod(this.setterBuilder);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000141B0 File Offset: 0x000123B0
		public Type PropertyType
		{
			get
			{
				return this.propertyBuilder.PropertyType;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000141BD File Offset: 0x000123BD
		public MethodBuilder GetGetter(Type requiredType)
		{
			if (!requiredType.IsAssignableFrom(this.PropertyType))
			{
				throw new InvalidOperationException("Types are not compatible");
			}
			return this.getterBuilder;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000141DE File Offset: 0x000123DE
		public MethodBuilder GetSetter(Type requiredType)
		{
			if (!this.PropertyType.IsAssignableFrom(requiredType))
			{
				throw new InvalidOperationException("Types are not compatible");
			}
			return this.setterBuilder;
		}

		// Token: 0x040000F0 RID: 240
		private static readonly MethodInfo proxyBase_NotifyPropertyChanged = typeof(ProxyBase).GetMethod("NotifyPropertyChanged", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		// Token: 0x040000F1 RID: 241
		private readonly FieldBuilder fieldBuilder;

		// Token: 0x040000F2 RID: 242
		private readonly MethodBuilder getterBuilder;

		// Token: 0x040000F3 RID: 243
		private readonly PropertyBuilder propertyBuilder;

		// Token: 0x040000F4 RID: 244
		private readonly MethodBuilder setterBuilder;
	}
}
