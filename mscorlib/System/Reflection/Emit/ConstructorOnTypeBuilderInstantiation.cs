using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000852 RID: 2130
	internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
	{
		// Token: 0x06004E15 RID: 19989 RVA: 0x0010F6D3 File Offset: 0x0010E6D3
		internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
		{
			return new ConstructorOnTypeBuilderInstantiation(Constructor, type);
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x0010F6DC File Offset: 0x0010E6DC
		internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
		{
			this.m_ctor = constructor;
			this.m_type = type;
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0010F6F2 File Offset: 0x0010E6F2
		internal override Type[] GetParameterTypes()
		{
			return this.m_ctor.GetParameterTypes();
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0010F6FF File Offset: 0x0010E6FF
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_ctor.MemberType;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x0010F70C File Offset: 0x0010E70C
		public override string Name
		{
			get
			{
				return this.m_ctor.Name;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x0010F719 File Offset: 0x0010E719
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x0010F721 File Offset: 0x0010E721
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0010F729 File Offset: 0x0010E729
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(inherit);
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0010F737 File Offset: 0x0010E737
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x0010F746 File Offset: 0x0010E746
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_ctor.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x0010F755 File Offset: 0x0010E755
		internal override int MetadataTokenInternal
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06004E20 RID: 20000 RVA: 0x0010F75C File Offset: 0x0010E75C
		public override Module Module
		{
			get
			{
				return this.m_ctor.Module;
			}
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x0010F769 File Offset: 0x0010E769
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0010F771 File Offset: 0x0010E771
		public override ParameterInfo[] GetParameters()
		{
			return this.m_ctor.GetParameters();
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0010F77E File Offset: 0x0010E77E
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_ctor.GetMethodImplementationFlags();
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x0010F78B File Offset: 0x0010E78B
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_ctor.MethodHandle;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06004E25 RID: 20005 RVA: 0x0010F798 File Offset: 0x0010E798
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_ctor.Attributes;
			}
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x0010F7A5 File Offset: 0x0010E7A5
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x0010F7AC File Offset: 0x0010E7AC
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_ctor.CallingConvention;
			}
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x0010F7B9 File Offset: 0x0010E7B9
		public override Type[] GetGenericArguments()
		{
			return this.m_ctor.GetGenericArguments();
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0010F7C6 File Offset: 0x0010E7C6
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06004E2A RID: 20010 RVA: 0x0010F7C9 File Offset: 0x0010E7C9
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06004E2B RID: 20011 RVA: 0x0010F7CC File Offset: 0x0010E7CC
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x0010F7CF File Offset: 0x0010E7CF
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04002853 RID: 10323
		internal ConstructorInfo m_ctor;

		// Token: 0x04002854 RID: 10324
		private TypeBuilderInstantiation m_type;
	}
}
