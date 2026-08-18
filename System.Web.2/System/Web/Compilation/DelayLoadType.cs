using System;
using System.Globalization;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x02000838 RID: 2104
	internal class DelayLoadType : Type
	{
		// Token: 0x06006454 RID: 25684 RVA: 0x001603E5 File Offset: 0x0015E5E5
		public DelayLoadType(string assemblyName, string typeName)
		{
			this._assemblyName = assemblyName;
			this._typeName = typeName;
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x001443BC File Offset: 0x001425BC
		internal static bool Enabled
		{
			get
			{
				return BuildManagerHost.InClientBuildManager;
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x06006456 RID: 25686 RVA: 0x001603FC File Offset: 0x0015E5FC
		public Type Type
		{
			get
			{
				if (this._type == null)
				{
					Assembly assembly = Assembly.Load(this._assemblyName);
					this._type = assembly.GetType(this._typeName);
				}
				return this._type;
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x06006457 RID: 25687 RVA: 0x0016043B File Offset: 0x0015E63B
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x00160443 File Offset: 0x0015E643
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x0016044B File Offset: 0x0015E64B
		public override Assembly Assembly
		{
			get
			{
				return this.Type.Assembly;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x0600645A RID: 25690 RVA: 0x00160458 File Offset: 0x0015E658
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.Type.AssemblyQualifiedName;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x00160465 File Offset: 0x0015E665
		public override Type BaseType
		{
			get
			{
				return this.Type.BaseType;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x0600645C RID: 25692 RVA: 0x00160472 File Offset: 0x0015E672
		public override string FullName
		{
			get
			{
				return this.Type.FullName;
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x0016047F File Offset: 0x0015E67F
		public override Guid GUID
		{
			get
			{
				return this.Type.GUID;
			}
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x0016048C File Offset: 0x0015E68C
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.Type.Attributes;
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x00160499 File Offset: 0x0015E699
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.Type.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x001604AD File Offset: 0x0015E6AD
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.Type.GetConstructors(bindingAttr);
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x001604BB File Offset: 0x0015E6BB
		public override Type GetElementType()
		{
			return this.Type.GetElementType();
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x001604C8 File Offset: 0x0015E6C8
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.Type.GetEvent(name, bindingAttr);
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x001604D7 File Offset: 0x0015E6D7
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.Type.GetEvents(bindingAttr);
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x001604E5 File Offset: 0x0015E6E5
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.Type.GetField(name, bindingAttr);
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x001604F4 File Offset: 0x0015E6F4
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.Type.GetFields(bindingAttr);
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x00160502 File Offset: 0x0015E702
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.Type.GetInterface(name, ignoreCase);
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x00160511 File Offset: 0x0015E711
		public override Type[] GetInterfaces()
		{
			return this.Type.GetInterfaces();
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x0016051E File Offset: 0x0015E71E
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.Type.GetMembers(bindingAttr);
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x0016052C File Offset: 0x0015E72C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.Type.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x00160542 File Offset: 0x0015E742
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.Type.GetMethods(bindingAttr);
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00160550 File Offset: 0x0015E750
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.Type.GetNestedType(name, bindingAttr);
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x0016055F File Offset: 0x0015E75F
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.Type.GetNestedTypes(bindingAttr);
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x0016056D File Offset: 0x0015E76D
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.Type.GetProperties(bindingAttr);
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x0016057B File Offset: 0x0015E77B
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.Type.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x00160591 File Offset: 0x0015E791
		protected override bool HasElementTypeImpl()
		{
			return this.Type.HasElementType;
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x001605A0 File Offset: 0x0015E7A0
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.Type.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x001605C5 File Offset: 0x0015E7C5
		protected override bool IsArrayImpl()
		{
			return this.Type.IsArray;
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x001605D2 File Offset: 0x0015E7D2
		protected override bool IsByRefImpl()
		{
			return this.Type.IsByRef;
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x001605DF File Offset: 0x0015E7DF
		protected override bool IsCOMObjectImpl()
		{
			return this.Type.IsCOMObject;
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x001605EC File Offset: 0x0015E7EC
		protected override bool IsPointerImpl()
		{
			return this.Type.IsPointer;
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x001605F9 File Offset: 0x0015E7F9
		protected override bool IsPrimitiveImpl()
		{
			return this.Type.IsPrimitive;
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06006476 RID: 25718 RVA: 0x00160606 File Offset: 0x0015E806
		public override Module Module
		{
			get
			{
				return this.Type.Module;
			}
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x00160613 File Offset: 0x0015E813
		public override string Namespace
		{
			get
			{
				return this.Type.Namespace;
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x00160620 File Offset: 0x0015E820
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.Type.UnderlyingSystemType;
			}
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x0016062D File Offset: 0x0015E82D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.Type.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x0016063C File Offset: 0x0015E83C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.Type.GetCustomAttributes(inherit);
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x0016064A File Offset: 0x0015E84A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.Type.IsDefined(attributeType, inherit);
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x00160659 File Offset: 0x0015E859
		public override string Name
		{
			get
			{
				return this.Type.Name;
			}
		}

		// Token: 0x040033DF RID: 13279
		private Type _type;

		// Token: 0x040033E0 RID: 13280
		private string _assemblyName;

		// Token: 0x040033E1 RID: 13281
		private string _typeName;
	}
}
