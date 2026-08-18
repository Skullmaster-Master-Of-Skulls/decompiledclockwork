using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000836 RID: 2102
	internal sealed class SymbolMethod : MethodInfo
	{
		// Token: 0x06004B37 RID: 19255 RVA: 0x00104B74 File Offset: 0x00103B74
		internal SymbolMethod(ModuleBuilder mod, MethodToken token, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.m_mdMethod = token;
			this.m_tkMethod = token.Token;
			this.m_returnType = returnType;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = new Type[0];
			}
			this.m_module = mod;
			this.m_containingType = arrayClass;
			this.m_name = methodName;
			this.m_callingConvention = callingConvention;
			this.m_signature = SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x00104C09 File Offset: 0x00103C09
		internal override Type[] GetParameterTypes()
		{
			return this.m_parameterTypes;
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x00104C11 File Offset: 0x00103C11
		internal MethodToken GetToken(ModuleBuilder mod)
		{
			return mod.GetArrayMethodToken(this.m_containingType, this.m_name, this.m_callingConvention, this.m_returnType, this.m_parameterTypes);
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x00104C37 File Offset: 0x00103C37
		public override Module Module
		{
			get
			{
				return this.m_module;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x00104C3F File Offset: 0x00103C3F
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_tkMethod;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x00104C47 File Offset: 0x00103C47
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004B3D RID: 19261 RVA: 0x00104C4F File Offset: 0x00103C4F
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004B3E RID: 19262 RVA: 0x00104C57 File Offset: 0x00103C57
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00104C5F File Offset: 0x00103C5F
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00104C70 File Offset: 0x00103C70
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004B41 RID: 19265 RVA: 0x00104C81 File Offset: 0x00103C81
		public override MethodAttributes Attributes
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004B42 RID: 19266 RVA: 0x00104C92 File Offset: 0x00103C92
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x00104C9A File Offset: 0x00103C9A
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00104CAB File Offset: 0x00103CAB
		internal override Type GetReturnType()
		{
			return this.m_returnType;
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x00104CB3 File Offset: 0x00103CB3
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x00104CB6 File Offset: 0x00103CB6
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00104CC7 File Offset: 0x00103CC7
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x00104CCA File Offset: 0x00103CCA
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x00104CDB File Offset: 0x00103CDB
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x00104CEC File Offset: 0x00103CEC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x00104CFD File Offset: 0x00103CFD
		public Module GetModule()
		{
			return this.m_module;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00104D05 File Offset: 0x00103D05
		public MethodToken GetToken()
		{
			return this.m_mdMethod;
		}

		// Token: 0x04002664 RID: 9828
		private ModuleBuilder m_module;

		// Token: 0x04002665 RID: 9829
		private Type m_containingType;

		// Token: 0x04002666 RID: 9830
		private string m_name;

		// Token: 0x04002667 RID: 9831
		private CallingConventions m_callingConvention;

		// Token: 0x04002668 RID: 9832
		private Type m_returnType;

		// Token: 0x04002669 RID: 9833
		private MethodToken m_mdMethod;

		// Token: 0x0400266A RID: 9834
		private int m_tkMethod;

		// Token: 0x0400266B RID: 9835
		private Type[] m_parameterTypes;

		// Token: 0x0400266C RID: 9836
		private SignatureHelper m_signature;
	}
}
