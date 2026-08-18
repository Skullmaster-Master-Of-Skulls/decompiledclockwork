using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200084E RID: 2126
	[ComVisible(true)]
	public sealed class GenericTypeParameterBuilder : Type
	{
		// Token: 0x06004D70 RID: 19824 RVA: 0x0010EDB5 File Offset: 0x0010DDB5
		internal GenericTypeParameterBuilder(TypeBuilder type)
		{
			this.m_type = type;
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0010EDC4 File Offset: 0x0010DDC4
		public override string ToString()
		{
			return this.m_type.Name;
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0010EDD4 File Offset: 0x0010DDD4
		public override bool Equals(object o)
		{
			GenericTypeParameterBuilder genericTypeParameterBuilder = o as GenericTypeParameterBuilder;
			return genericTypeParameterBuilder != null && genericTypeParameterBuilder.m_type == this.m_type;
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0010EDFB File Offset: 0x0010DDFB
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x0010EE08 File Offset: 0x0010DE08
		public override Type DeclaringType
		{
			get
			{
				return this.m_type.DeclaringType;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06004D75 RID: 19829 RVA: 0x0010EE15 File Offset: 0x0010DE15
		public override Type ReflectedType
		{
			get
			{
				return this.m_type.ReflectedType;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x0010EE22 File Offset: 0x0010DE22
		public override string Name
		{
			get
			{
				return this.m_type.Name;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x0010EE2F File Offset: 0x0010DE2F
		public override Module Module
		{
			get
			{
				return this.m_type.Module;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x0010EE3C File Offset: 0x0010DE3C
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_type.MetadataTokenInternal;
			}
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0010EE49 File Offset: 0x0010DE49
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0010EE5C File Offset: 0x0010DE5C
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x0010EE6F File Offset: 0x0010DE6F
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0010EE84 File Offset: 0x0010DE84
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				text
			});
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0) as SymbolType;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06004D7D RID: 19837 RVA: 0x0010EEF8 File Offset: 0x0010DEF8
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x0010EEFF File Offset: 0x0010DEFF
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004D7F RID: 19839 RVA: 0x0010EF06 File Offset: 0x0010DF06
		public override Assembly Assembly
		{
			get
			{
				return this.m_type.Assembly;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x0010EF13 File Offset: 0x0010DF13
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x0010EF1A File Offset: 0x0010DF1A
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x0010EF1D File Offset: 0x0010DF1D
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06004D83 RID: 19843 RVA: 0x0010EF20 File Offset: 0x0010DF20
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x0010EF23 File Offset: 0x0010DF23
		public override Type BaseType
		{
			get
			{
				return this.m_type.BaseType;
			}
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x0010EF30 File Offset: 0x0010DF30
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x0010EF37 File Offset: 0x0010DF37
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x0010EF3E File Offset: 0x0010DF3E
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x0010EF45 File Offset: 0x0010DF45
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x0010EF4C File Offset: 0x0010DF4C
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x0010EF53 File Offset: 0x0010DF53
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x0010EF5A File Offset: 0x0010DF5A
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x0010EF61 File Offset: 0x0010DF61
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x0010EF68 File Offset: 0x0010DF68
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x0010EF6F File Offset: 0x0010DF6F
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0010EF76 File Offset: 0x0010DF76
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0010EF7D File Offset: 0x0010DF7D
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0010EF84 File Offset: 0x0010DF84
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0010EF8B File Offset: 0x0010DF8B
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0010EF92 File Offset: 0x0010DF92
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x0010EF99 File Offset: 0x0010DF99
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0010EFA0 File Offset: 0x0010DFA0
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x0010EFA7 File Offset: 0x0010DFA7
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x0010EFAE File Offset: 0x0010DFAE
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x0010EFB5 File Offset: 0x0010DFB5
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0010EFB8 File Offset: 0x0010DFB8
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x0010EFBB File Offset: 0x0010DFBB
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0010EFBE File Offset: 0x0010DFBE
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x0010EFC1 File Offset: 0x0010DFC1
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x0010EFC4 File Offset: 0x0010DFC4
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x0010EFCB File Offset: 0x0010DFCB
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06004D9F RID: 19871 RVA: 0x0010EFCE File Offset: 0x0010DFCE
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x0010EFD1 File Offset: 0x0010DFD1
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x0010EFD8 File Offset: 0x0010DFD8
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004DA2 RID: 19874 RVA: 0x0010EFDB File Offset: 0x0010DFDB
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x0010EFDE File Offset: 0x0010DFDE
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004DA4 RID: 19876 RVA: 0x0010EFE1 File Offset: 0x0010DFE1
		public override int GenericParameterPosition
		{
			get
			{
				return this.m_type.GenericParameterPosition;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x0010EFEE File Offset: 0x0010DFEE
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_type.ContainsGenericParameters;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x0010EFFB File Offset: 0x0010DFFB
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.m_type.DeclaringMethod;
			}
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x0010F008 File Offset: 0x0010E008
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0010F00F File Offset: 0x0010E00F
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x0010F020 File Offset: 0x0010E020
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x0010F023 File Offset: 0x0010E023
		public override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x0010F02A File Offset: 0x0010E02A
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0010F031 File Offset: 0x0010E031
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x0010F038 File Offset: 0x0010E038
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x0010F03F File Offset: 0x0010E03F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x0010F046 File Offset: 0x0010E046
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (this.m_type.m_ca == null)
			{
				this.m_type.m_ca = new ArrayList();
			}
			this.m_type.m_ca.Add(new TypeBuilder.CustAttr(con, binaryAttribute));
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0010F07D File Offset: 0x0010E07D
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (this.m_type.m_ca == null)
			{
				this.m_type.m_ca = new ArrayList();
			}
			this.m_type.m_ca.Add(new TypeBuilder.CustAttr(customBuilder));
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0010F0B4 File Offset: 0x0010E0B4
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.m_type.CheckContext(new Type[]
			{
				baseTypeConstraint
			});
			this.m_type.SetParent(baseTypeConstraint);
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0010F0E4 File Offset: 0x0010E0E4
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.m_type.CheckContext(interfaceConstraints);
			this.m_type.SetInterfaces(interfaceConstraints);
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x0010F0FE File Offset: 0x0010E0FE
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.m_type.m_genParamAttributes = genericParameterAttributes;
		}

		// Token: 0x0400284A RID: 10314
		internal TypeBuilder m_type;
	}
}
