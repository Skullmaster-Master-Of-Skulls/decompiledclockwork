using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000851 RID: 2129
	internal sealed class MethodOnTypeBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004DF8 RID: 19960 RVA: 0x0010F568 File Offset: 0x0010E568
		internal static MethodInfo GetMethod(MethodInfo method, TypeBuilderInstantiation type)
		{
			return new MethodOnTypeBuilderInstantiation(method, type);
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x0010F571 File Offset: 0x0010E571
		internal MethodOnTypeBuilderInstantiation(MethodInfo method, TypeBuilderInstantiation type)
		{
			this.m_method = method;
			this.m_type = type;
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x0010F587 File Offset: 0x0010E587
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06004DFB RID: 19963 RVA: 0x0010F594 File Offset: 0x0010E594
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x0010F5A1 File Offset: 0x0010E5A1
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x0010F5AE File Offset: 0x0010E5AE
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x0010F5B6 File Offset: 0x0010E5B6
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x0010F5BE File Offset: 0x0010E5BE
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x0010F5CC File Offset: 0x0010E5CC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x0010F5DB File Offset: 0x0010E5DB
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06004E02 RID: 19970 RVA: 0x0010F5EA File Offset: 0x0010E5EA
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_method.MetadataTokenInternal;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x0010F5F7 File Offset: 0x0010E5F7
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x0010F604 File Offset: 0x0010E604
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x0010F60C File Offset: 0x0010E60C
		public override ParameterInfo[] GetParameters()
		{
			return this.m_method.GetParameters();
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x0010F619 File Offset: 0x0010E619
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x0010F626 File Offset: 0x0010E626
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_method.MethodHandle;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x0010F633 File Offset: 0x0010E633
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x0010F640 File Offset: 0x0010E640
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x0010F647 File Offset: 0x0010E647
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x0010F654 File Offset: 0x0010E654
		public override Type[] GetGenericArguments()
		{
			return this.m_method.GetGenericArguments();
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0010F661 File Offset: 0x0010E661
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x0010F669 File Offset: 0x0010E669
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004E0E RID: 19982 RVA: 0x0010F676 File Offset: 0x0010E676
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_method.ContainsGenericParameters;
			}
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0010F683 File Offset: 0x0010E683
		public override MethodInfo MakeGenericMethod(params Type[] typeArgs)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
			}
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArgs);
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x0010F6A4 File Offset: 0x0010E6A4
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_method.IsGenericMethod;
			}
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0010F6B1 File Offset: 0x0010E6B1
		internal override Type GetReturnType()
		{
			return this.m_method.GetReturnType();
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x0010F6BE File Offset: 0x0010E6BE
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x0010F6C5 File Offset: 0x0010E6C5
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x0010F6CC File Offset: 0x0010E6CC
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002851 RID: 10321
		internal MethodInfo m_method;

		// Token: 0x04002852 RID: 10322
		private TypeBuilderInstantiation m_type;
	}
}
