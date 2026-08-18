using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200084D RID: 2125
	internal sealed class TypeBuilderInstantiation : Type
	{
		// Token: 0x06004D32 RID: 19762 RVA: 0x0010EA89 File Offset: 0x0010DA89
		internal TypeBuilderInstantiation(Type type, Type[] inst)
		{
			this.m_type = type;
			this.m_inst = inst;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0010EA9F File Offset: 0x0010DA9F
		public override string ToString()
		{
			return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.ToString);
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004D34 RID: 19764 RVA: 0x0010EAA8 File Offset: 0x0010DAA8
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004D35 RID: 19765 RVA: 0x0010EAB5 File Offset: 0x0010DAB5
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004D36 RID: 19766 RVA: 0x0010EAC2 File Offset: 0x0010DAC2
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004D37 RID: 19767 RVA: 0x0010EACF File Offset: 0x0010DACF
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06004D38 RID: 19768 RVA: 0x0010EADC File Offset: 0x0010DADC
		internal override int MetadataTokenInternal
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0010EAE3 File Offset: 0x0010DAE3
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0010EAF6 File Offset: 0x0010DAF6
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0010EB09 File Offset: 0x0010DB09
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0010EB1C File Offset: 0x0010DB1C
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			for (int i = 1; i < rank; i++)
			{
				text += ",";
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				text
			});
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004D3D RID: 19773 RVA: 0x0010EB7A File Offset: 0x0010DB7A
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0010EB81 File Offset: 0x0010DB81
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x0010EB88 File Offset: 0x0010DB88
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x0010EB95 File Offset: 0x0010DB95
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x0010EB9C File Offset: 0x0010DB9C
		public override string FullName
		{
			get
			{
				if (this.m_strFullQualName == null)
				{
					this.m_strFullQualName = TypeNameBuilder.ToString(this, TypeNameBuilder.Format.FullName);
				}
				return this.m_strFullQualName;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x0010EBB9 File Offset: 0x0010DBB9
		public override string Namespace
		{
			get
			{
				return this.m_type.Namespace;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x0010EBC6 File Offset: 0x0010DBC6
		public override string AssemblyQualifiedName
		{
			get
			{
				return TypeNameBuilder.ToString(this, TypeNameBuilder.Format.AssemblyQualifiedName);
			}
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0010EBD0 File Offset: 0x0010DBD0
		internal Type Substitute(Type[] substitutes)
		{
			Type[] genericArguments = this.GetGenericArguments();
			Type[] array = new Type[genericArguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Type type = genericArguments[i];
				if (type is TypeBuilderInstantiation)
				{
					array[i] = (type as TypeBuilderInstantiation).Substitute(substitutes);
				}
				else if (type is GenericTypeParameterBuilder)
				{
					array[i] = substitutes[type.GenericParameterPosition];
				}
				else
				{
					array[i] = type;
				}
			}
			return this.GetGenericTypeDefinition().MakeGenericType(array);
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x0010EC40 File Offset: 0x0010DC40
		public override Type BaseType
		{
			get
			{
				Type baseType = this.m_type.BaseType;
				if (baseType == null)
				{
					return null;
				}
				TypeBuilderInstantiation typeBuilderInstantiation = baseType as TypeBuilderInstantiation;
				if (typeBuilderInstantiation == null)
				{
					return baseType;
				}
				return typeBuilderInstantiation.Substitute(this.GetGenericArguments());
			}
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0010EC76 File Offset: 0x0010DC76
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0010EC7D File Offset: 0x0010DC7D
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0010EC84 File Offset: 0x0010DC84
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0010EC8B File Offset: 0x0010DC8B
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x0010EC92 File Offset: 0x0010DC92
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0010EC99 File Offset: 0x0010DC99
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0010ECA0 File Offset: 0x0010DCA0
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0010ECA7 File Offset: 0x0010DCA7
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0010ECAE File Offset: 0x0010DCAE
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0010ECB5 File Offset: 0x0010DCB5
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0010ECBC File Offset: 0x0010DCBC
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0010ECC3 File Offset: 0x0010DCC3
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0010ECCA File Offset: 0x0010DCCA
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0010ECD1 File Offset: 0x0010DCD1
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0010ECD8 File Offset: 0x0010DCD8
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0010ECDF File Offset: 0x0010DCDF
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0010ECE6 File Offset: 0x0010DCE6
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0010ECED File Offset: 0x0010DCED
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0010ECF4 File Offset: 0x0010DCF4
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_type.Attributes;
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0010ED01 File Offset: 0x0010DD01
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0010ED04 File Offset: 0x0010DD04
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0010ED07 File Offset: 0x0010DD07
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0010ED0A File Offset: 0x0010DD0A
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0010ED0D File Offset: 0x0010DD0D
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0010ED10 File Offset: 0x0010DD10
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0010ED17 File Offset: 0x0010DD17
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x0010ED1A File Offset: 0x0010DD1A
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x0010ED1D File Offset: 0x0010DD1D
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x0010ED25 File Offset: 0x0010DD25
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x0010ED28 File Offset: 0x0010DD28
		public override bool IsGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x0010ED2B File Offset: 0x0010DD2B
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x0010ED2E File Offset: 0x0010DD2E
		public override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x0010ED35 File Offset: 0x0010DD35
		protected override bool IsValueTypeImpl()
		{
			return this.m_type.IsValueType;
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06004D67 RID: 19815 RVA: 0x0010ED44 File Offset: 0x0010DD44
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x0010ED76 File Offset: 0x0010DD76
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x0010ED79 File Offset: 0x0010DD79
		public override Type GetGenericTypeDefinition()
		{
			return this.m_type;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x0010ED81 File Offset: 0x0010DD81
		public override Type MakeGenericType(params Type[] inst)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x0010ED92 File Offset: 0x0010DD92
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x0010ED99 File Offset: 0x0010DD99
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x0010EDA0 File Offset: 0x0010DDA0
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0010EDA7 File Offset: 0x0010DDA7
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0010EDAE File Offset: 0x0010DDAE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002847 RID: 10311
		private Type m_type;

		// Token: 0x04002848 RID: 10312
		private Type[] m_inst;

		// Token: 0x04002849 RID: 10313
		private string m_strFullQualName;
	}
}
