using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000171 RID: 369
	internal class LazyLoadImplementor
	{
		// Token: 0x06001B24 RID: 6948 RVA: 0x0005CD5B File Offset: 0x0005AF5B
		public LazyLoadImplementor(EntityType ospaceEntityType)
		{
			this.CheckType(ospaceEntityType);
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0005CD6A File Offset: 0x0005AF6A
		public IEnumerable<EdmMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0005CD74 File Offset: 0x0005AF74
		private void CheckType(EntityType ospaceEntityType)
		{
			this._members = new HashSet<EdmMember>();
			foreach (EdmMember edmMember in ospaceEntityType.Members)
			{
				PropertyInfo topProperty = EntityUtil.GetTopProperty(ospaceEntityType.ClrType, edmMember.Name);
				if (topProperty != null && EntityProxyFactory.CanProxyGetter(topProperty) && LazyLoadBehavior.IsLazyLoadCandidate(ospaceEntityType, edmMember))
				{
					this._members.Add(edmMember);
				}
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0005CE04 File Offset: 0x0005B004
		public bool CanProxyMember(EdmMember member)
		{
			return this._members.Contains(member);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0005CE14 File Offset: 0x0005B014
		public void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			FieldBuilder arg = typeBuilder.DefineField("_entityWrapper", typeof(object), FieldAttributes.Public);
			registerField(arg, false);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0005CE40 File Offset: 0x0005B040
		public bool EmitMember(TypeBuilder typeBuilder, EdmMember member, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, BaseProxyImplementor baseImplementor)
		{
			if (this._members.Contains(member))
			{
				MethodInfo getMethod = baseProperty.GetGetMethod(true);
				MethodAttributes methodAttributes = getMethod.Attributes & MethodAttributes.MemberAccessMask;
				Type type = typeof(Func<, , >).MakeGenericType(new Type[]
				{
					typeBuilder,
					baseProperty.PropertyType,
					typeof(bool)
				});
				MethodInfo method = TypeBuilder.GetMethod(type, typeof(Func<, , >).GetMethod("Invoke"));
				FieldBuilder field = typeBuilder.DefineField(LazyLoadImplementor.GetInterceptorFieldName(baseProperty.Name), type, FieldAttributes.Private | FieldAttributes.Static);
				MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), baseProperty.PropertyType, Type.EmptyTypes);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				Label label = ilgenerator.DefineLabel();
				ilgenerator.DeclareLocal(baseProperty.PropertyType);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, getMethod);
				ilgenerator.Emit(OpCodes.Stloc_0);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, field);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Callvirt, method);
				ilgenerator.Emit(OpCodes.Brtrue_S, label);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, getMethod);
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(label);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Ret);
				propertyBuilder.SetGetMethod(methodBuilder);
				baseImplementor.AddBasePropertyGetter(baseProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0005CFEC File Offset: 0x0005B1EC
		internal static string GetInterceptorFieldName(string memberName)
		{
			return "ef_proxy_interceptorFor" + memberName;
		}

		// Token: 0x04000B41 RID: 2881
		private HashSet<EdmMember> _members;
	}
}
