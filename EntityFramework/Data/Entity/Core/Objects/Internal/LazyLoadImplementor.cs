using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200058C RID: 1420
	internal class LazyLoadImplementor
	{
		// Token: 0x0600376F RID: 14191 RVA: 0x001077CC File Offset: 0x001059CC
		public LazyLoadImplementor(EntityType ospaceEntityType)
		{
			this.CheckType(ospaceEntityType);
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x001077DB File Offset: 0x001059DB
		public IEnumerable<EdmMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x001077E4 File Offset: 0x001059E4
		private void CheckType(EntityType ospaceEntityType)
		{
			this._members = new HashSet<EdmMember>();
			foreach (EdmMember edmMember in ospaceEntityType.Members)
			{
				PropertyInfo topProperty = ospaceEntityType.ClrType.GetTopProperty(edmMember.Name);
				if (topProperty != null && EntityProxyFactory.CanProxyGetter(topProperty) && LazyLoadBehavior.IsLazyLoadCandidate(ospaceEntityType, edmMember))
				{
					this._members.Add(edmMember);
				}
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x00107874 File Offset: 0x00105A74
		public bool CanProxyMember(EdmMember member)
		{
			return this._members.Contains(member);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x00107884 File Offset: 0x00105A84
		public virtual void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			FieldBuilder arg = typeBuilder.DefineField("_entityWrapper", typeof(object), FieldAttributes.Public);
			registerField(arg, false);
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x001078B0 File Offset: 0x00105AB0
		public bool EmitMember(TypeBuilder typeBuilder, EdmMember member, PropertyBuilder propertyBuilder, PropertyInfo baseProperty, BaseProxyImplementor baseImplementor)
		{
			if (this._members.Contains(member))
			{
				MethodInfo methodInfo = baseProperty.Getter();
				MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
				Type type = typeof(Func<, , >).MakeGenericType(new Type[]
				{
					typeBuilder,
					baseProperty.PropertyType,
					typeof(bool)
				});
				MethodInfo method = TypeBuilder.GetMethod(type, typeof(Func<, , >).GetOnlyDeclaredMethod("Invoke"));
				FieldBuilder field = typeBuilder.DefineField(LazyLoadImplementor.GetInterceptorFieldName(baseProperty.Name), type, FieldAttributes.Private | FieldAttributes.Static);
				MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), baseProperty.PropertyType, Type.EmptyTypes);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				Label label = ilgenerator.DefineLabel();
				ilgenerator.DeclareLocal(baseProperty.PropertyType);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, methodInfo);
				ilgenerator.Emit(OpCodes.Stloc_0);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, field);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Callvirt, method);
				ilgenerator.Emit(OpCodes.Brtrue_S, label);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Call, methodInfo);
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

		// Token: 0x06003775 RID: 14197 RVA: 0x00107A62 File Offset: 0x00105C62
		internal static string GetInterceptorFieldName(string memberName)
		{
			return "ef_proxy_interceptorFor" + memberName;
		}

		// Token: 0x0400155C RID: 5468
		private HashSet<EdmMember> _members;
	}
}
