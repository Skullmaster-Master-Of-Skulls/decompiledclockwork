using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000833 RID: 2099
	internal sealed class MethodBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004AE4 RID: 19172 RVA: 0x0010429F File Offset: 0x0010329F
		internal static MethodInfo MakeGenericMethod(MethodInfo method, Type[] inst)
		{
			if (!method.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return new MethodBuilderInstantiation(method, inst);
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x001042B6 File Offset: 0x001032B6
		internal MethodBuilderInstantiation(MethodInfo method, Type[] inst)
		{
			this.m_method = method;
			this.m_inst = inst;
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x001042CC File Offset: 0x001032CC
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x001042D9 File Offset: 0x001032D9
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x001042E6 File Offset: 0x001032E6
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004AE9 RID: 19177 RVA: 0x001042F3 File Offset: 0x001032F3
		public override Type DeclaringType
		{
			get
			{
				return this.m_method.DeclaringType;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x00104300 File Offset: 0x00103300
		public override Type ReflectedType
		{
			get
			{
				return this.m_method.ReflectedType;
			}
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x0010430D File Offset: 0x0010330D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x0010431B File Offset: 0x0010331B
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x0010432A File Offset: 0x0010332A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x00104339 File Offset: 0x00103339
		internal override int MetadataTokenInternal
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004AEF RID: 19183 RVA: 0x00104340 File Offset: 0x00103340
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x0010434D File Offset: 0x0010334D
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00104355 File Offset: 0x00103355
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x0010435C File Offset: 0x0010335C
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x00104369 File Offset: 0x00103369
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x0010437A File Offset: 0x0010337A
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00104387 File Offset: 0x00103387
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x0010438E File Offset: 0x0010338E
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x0010439B File Offset: 0x0010339B
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x001043A3 File Offset: 0x001033A3
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x001043AB File Offset: 0x001033AB
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x001043B0 File Offset: 0x001033B0
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
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x001043F9 File Offset: 0x001033F9
		public override MethodInfo MakeGenericMethod(params Type[] arguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x0010440A File Offset: 0x0010340A
		public override bool IsGenericMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0010440D File Offset: 0x0010340D
		internal override Type GetReturnType()
		{
			return this.m_method.GetReturnType();
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x0010441A File Offset: 0x0010341A
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004AFF RID: 19199 RVA: 0x00104421 File Offset: 0x00103421
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00104428 File Offset: 0x00103428
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002657 RID: 9815
		internal MethodInfo m_method;

		// Token: 0x04002658 RID: 9816
		private Type[] m_inst;
	}
}
