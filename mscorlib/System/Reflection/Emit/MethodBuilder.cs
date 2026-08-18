using System;
using System.Collections;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x0200082F RID: 2095
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x06004A91 RID: 19089 RVA: 0x00102E7C File Offset: 0x00101E7C
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			this.Init(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, mod, type, bIsGlobalMethod);
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00102EA8 File Offset: 0x00101EA8
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, Module mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			this.Init(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, mod, type, bIsGlobalMethod);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x00102ED8 File Offset: 0x00101ED8
		private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, Module mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.m_link = type.m_currentMethod;
			type.m_currentMethod = this;
			this.m_strName = name;
			this.m_module = mod;
			this.m_containingType = type;
			this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(mod);
			this.m_returnType = returnType;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				callingConvention |= CallingConventions.HasThis;
			}
			else if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NoStaticVirtual"));
			}
			if ((attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName && (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != (MethodAttributes.Virtual | MethodAttributes.Abstract) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
			}
			this.m_callingConvention = callingConvention;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = null;
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
			this.m_iAttributes = attributes;
			this.m_bIsGlobalMethod = bIsGlobalMethod;
			this.m_bIsBaked = false;
			this.m_fInitLocals = true;
			this.m_localSymInfo = new LocalSymInfo();
			this.m_ubBody = null;
			this.m_ilGenerator = null;
			this.m_dwMethodImplFlags = MethodImplAttributes.IL;
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x0010309A File Offset: 0x0010209A
		internal void CheckContext(params Type[][] typess)
		{
			((AssemblyBuilder)this.Module.Assembly).CheckContext(typess);
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x001030B2 File Offset: 0x001020B2
		internal void CheckContext(params Type[] types)
		{
			((AssemblyBuilder)this.Module.Assembly).CheckContext(types);
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x001030CC File Offset: 0x001020CC
		internal void CreateMethodBodyHelper(ILGenerator il)
		{
			int num = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_module;
			this.m_containingType.ThrowIfCreated();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodHasBody"));
			}
			if (il == null)
			{
				throw new ArgumentNullException("il");
			}
			if (il.m_methodBuilder != this && il.m_methodBuilder != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
			}
			this.ThrowIfShouldNotHaveBody();
			if (il.m_ScopeTree.m_iOpenScopeCount != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OpenLocalVariableScope"));
			}
			this.m_ubBody = il.BakeByteArray();
			this.m_RVAFixups = il.GetRVAFixups();
			this.m_mdMethodFixups = il.GetTokenFixups();
			__ExceptionInfo[] exceptions = il.GetExceptions();
			this.m_numExceptions = this.CalculateNumberOfExceptions(exceptions);
			if (this.m_numExceptions > 0)
			{
				this.m_exceptions = new __ExceptionInstance[this.m_numExceptions];
				for (int i = 0; i < exceptions.Length; i++)
				{
					int[] filterAddresses = exceptions[i].GetFilterAddresses();
					int[] catchAddresses = exceptions[i].GetCatchAddresses();
					int[] catchEndAddresses = exceptions[i].GetCatchEndAddresses();
					Type[] catchClass = exceptions[i].GetCatchClass();
					for (int j = 0; j < catchClass.Length; j++)
					{
						if (catchClass[j] != null)
						{
							moduleBuilder.GetTypeTokenInternal(catchClass[j]);
						}
					}
					int numberOfCatches = exceptions[i].GetNumberOfCatches();
					int startAddress = exceptions[i].GetStartAddress();
					int endAddress = exceptions[i].GetEndAddress();
					int[] exceptionTypes = exceptions[i].GetExceptionTypes();
					for (int k = 0; k < numberOfCatches; k++)
					{
						int exceptionClass = 0;
						if (catchClass[k] != null)
						{
							exceptionClass = moduleBuilder.GetTypeTokenInternal(catchClass[k]).Token;
						}
						switch (exceptionTypes[k])
						{
						case 0:
						case 1:
						case 4:
							this.m_exceptions[num++] = new __ExceptionInstance(startAddress, endAddress, filterAddresses[k], catchAddresses[k], catchEndAddresses[k], exceptionTypes[k], exceptionClass);
							break;
						case 2:
							this.m_exceptions[num++] = new __ExceptionInstance(startAddress, exceptions[i].GetFinallyEndAddress(), filterAddresses[k], catchAddresses[k], catchEndAddresses[k], exceptionTypes[k], exceptionClass);
							break;
						}
					}
				}
			}
			this.m_bIsBaked = true;
			if (moduleBuilder.GetSymWriter() != null)
			{
				SymbolToken method = new SymbolToken(this.MetadataTokenInternal);
				ISymbolWriter symWriter = moduleBuilder.GetSymWriter();
				symWriter.OpenMethod(method);
				symWriter.OpenScope(0);
				if (this.m_symCustomAttrs != null)
				{
					foreach (object obj in this.m_symCustomAttrs)
					{
						MethodBuilder.SymCustomAttr symCustomAttr = (MethodBuilder.SymCustomAttr)obj;
						moduleBuilder.GetSymWriter().SetSymAttribute(new SymbolToken(this.MetadataTokenInternal), symCustomAttr.m_name, symCustomAttr.m_data);
					}
				}
				if (this.m_localSymInfo != null)
				{
					this.m_localSymInfo.EmitLocalSymInfo(symWriter);
				}
				il.m_ScopeTree.EmitScopeTree(symWriter);
				il.m_LineNumberInfo.EmitLineNumberInfo(symWriter);
				symWriter.CloseScope(il.m_length);
				symWriter.CloseMethod();
			}
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00103408 File Offset: 0x00102408
		internal void ReleaseBakedStructures()
		{
			if (!this.m_bIsBaked)
			{
				return;
			}
			this.m_ubBody = null;
			this.m_localSymInfo = null;
			this.m_RVAFixups = null;
			this.m_mdMethodFixups = null;
			this.m_exceptions = null;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00103436 File Offset: 0x00102436
		internal override Type[] GetParameterTypes()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = new Type[0];
			}
			return this.m_parameterTypes;
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00103452 File Offset: 0x00102452
		internal void SetToken(MethodToken token)
		{
			this.m_tkMethod = token;
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0010345B File Offset: 0x0010245B
		internal byte[] GetBody()
		{
			return this.m_ubBody;
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00103463 File Offset: 0x00102463
		internal int[] GetTokenFixups()
		{
			return this.m_mdMethodFixups;
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0010346B File Offset: 0x0010246B
		internal int[] GetRVAFixups()
		{
			return this.m_RVAFixups;
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00103474 File Offset: 0x00102474
		internal SignatureHelper GetMethodSignature()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = new Type[0];
			}
			this.m_signature = SignatureHelper.GetMethodSigHelper(this.m_module, this.m_callingConvention, (this.m_inst != null) ? this.m_inst.Length : 0, (this.m_returnType == null) ? typeof(void) : this.m_returnType, this.m_returnTypeRequiredCustomModifiers, this.m_returnTypeOptionalCustomModifiers, this.m_parameterTypes, this.m_parameterTypeRequiredCustomModifiers, this.m_parameterTypeOptionalCustomModifiers);
			return this.m_signature;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x001034FD File Offset: 0x001024FD
		internal SignatureHelper GetLocalsSignature()
		{
			if (this.m_ilGenerator != null && this.m_ilGenerator.m_localCount != 0)
			{
				return this.m_ilGenerator.m_localSignature;
			}
			return this.m_localSignature;
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00103526 File Offset: 0x00102526
		internal int GetNumberOfExceptions()
		{
			return this.m_numExceptions;
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0010352E File Offset: 0x0010252E
		internal __ExceptionInstance[] GetExceptionInstances()
		{
			return this.m_exceptions;
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00103538 File Offset: 0x00102538
		internal int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00103566 File Offset: 0x00102566
		internal bool IsTypeCreated()
		{
			return this.m_containingType != null && this.m_containingType.m_hasBeenCreated;
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0010357D File Offset: 0x0010257D
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_containingType;
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00103588 File Offset: 0x00102588
		public override bool Equals(object obj)
		{
			if (!(obj is MethodBuilder))
			{
				return false;
			}
			if (!this.m_strName.Equals(((MethodBuilder)obj).m_strName))
			{
				return false;
			}
			if (this.m_iAttributes != ((MethodBuilder)obj).m_iAttributes)
			{
				return false;
			}
			SignatureHelper methodSignature = ((MethodBuilder)obj).GetMethodSignature();
			return methodSignature.Equals(this.GetMethodSignature());
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001035EB File Offset: 0x001025EB
		public override int GetHashCode()
		{
			return this.m_strName.GetHashCode();
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x001035F8 File Offset: 0x001025F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			stringBuilder.Append("Name: " + this.m_strName + " " + Environment.NewLine);
			stringBuilder.Append("Attributes: " + (int)this.m_iAttributes + Environment.NewLine);
			stringBuilder.Append("Method Signature: " + this.GetMethodSignature() + Environment.NewLine);
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x00103680 File Offset: 0x00102680
		public override string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00103688 File Offset: 0x00102688
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.GetToken().Token;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004AA9 RID: 19113 RVA: 0x001036A3 File Offset: 0x001026A3
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x001036B0 File Offset: 0x001026B0
		public override Type DeclaringType
		{
			get
			{
				if (this.m_containingType.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_containingType;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004AAB RID: 19115 RVA: 0x001036C7 File Offset: 0x001026C7
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x001036CA File Offset: 0x001026CA
		public override Type ReflectedType
		{
			get
			{
				if (this.m_containingType.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_containingType;
			}
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x001036E1 File Offset: 0x001026E1
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x001036F2 File Offset: 0x001026F2
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dwMethodImplFlags;
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x001036FA File Offset: 0x001026FA
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_iAttributes;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x00103702 File Offset: 0x00102702
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x0010370A File Offset: 0x0010270A
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0010371B File Offset: 0x0010271B
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0010371E File Offset: 0x0010271E
		internal override Type GetReturnType()
		{
			return this.m_returnType;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00103728 File Offset: 0x00102728
		public override ParameterInfo[] GetParameters()
		{
			if (!this.m_bIsBaked)
			{
				throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
			}
			Type runtimeType = this.m_containingType.m_runtimeType;
			MethodInfo method = runtimeType.GetMethod(this.m_strName, this.m_parameterTypes);
			return method.GetParameters();
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004AB5 RID: 19125 RVA: 0x00103774 File Offset: 0x00102774
		public override ParameterInfo ReturnParameter
		{
			get
			{
				if (!this.m_bIsBaked)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
				}
				Type runtimeType = this.m_containingType.m_runtimeType;
				MethodInfo method = runtimeType.GetMethod(this.m_strName, this.m_parameterTypes);
				return method.ReturnParameter;
			}
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x001037BE File Offset: 0x001027BE
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x001037CF File Offset: 0x001027CF
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x001037E0 File Offset: 0x001027E0
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x001037F1 File Offset: 0x001027F1
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_bIsGenMethDef;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x001037F9 File Offset: 0x001027F9
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00103800 File Offset: 0x00102800
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x00103811 File Offset: 0x00102811
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_inst != null;
			}
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0010381F File Offset: 0x0010281F
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00103827 File Offset: 0x00102827
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return new MethodBuilderInstantiation(this, typeArguments);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00103830 File Offset: 0x00102830
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (this.m_inst != null)
			{
				throw new InvalidOperationException();
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i] == null)
				{
					throw new ArgumentNullException("names");
				}
			}
			if (this.m_tkMethod.Token != 0)
			{
				throw new InvalidOperationException();
			}
			if (names.Length == 0)
			{
				throw new ArgumentException();
			}
			this.m_bIsGenMethDef = true;
			this.m_inst = new GenericTypeParameterBuilder[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				this.m_inst[j] = new GenericTypeParameterBuilder(new TypeBuilder(names[j], j, this));
			}
			return this.m_inst;
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x001038D5 File Offset: 0x001028D5
		internal void ThrowIfGeneric()
		{
			if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x001038F0 File Offset: 0x001028F0
		public MethodToken GetToken()
		{
			if (this.m_tkMethod.Token == 0)
			{
				if (this.m_link != null)
				{
					this.m_link.GetToken();
				}
				int sigLength;
				byte[] signature = this.GetMethodSignature().InternalGetSignature(out sigLength);
				this.m_tkMethod = new MethodToken(TypeBuilder.InternalDefineMethod(this.m_containingType.MetadataTokenInternal, this.m_strName, signature, sigLength, this.Attributes, this.m_module));
				if (this.m_inst != null)
				{
					foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in this.m_inst)
					{
						if (!genericTypeParameterBuilder.m_type.IsCreated())
						{
							genericTypeParameterBuilder.m_type.CreateType();
						}
					}
				}
				TypeBuilder.InternalSetMethodImpl(this.m_module, this.MetadataTokenInternal, this.m_dwMethodImplFlags);
			}
			return this.m_tkMethod;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x001039BA File Offset: 0x001029BA
		public void SetParameters(params Type[] parameterTypes)
		{
			this.CheckContext(parameterTypes);
			this.SetSignature(null, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x001039D0 File Offset: 0x001029D0
		public void SetReturnType(Type returnType)
		{
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.SetSignature(returnType, null, null, null, null, null);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x001039FC File Offset: 0x001029FC
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.CheckContext(new Type[][]
			{
				returnTypeRequiredCustomModifiers,
				returnTypeOptionalCustomModifiers,
				parameterTypes
			});
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			this.ThrowIfGeneric();
			if (returnType != null)
			{
				this.m_returnType = returnType;
			}
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00103A98 File Offset: 0x00102A98
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (position < 0)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			if (position > 0 && (this.m_parameterTypes == null || position > this.m_parameterTypes.Length))
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			attributes &= ~ParameterAttributes.ReservedMask;
			return new ParameterBuilder(this, position, attributes, strParamName);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00103B03 File Offset: 0x00102B03
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (this.m_retParam == null)
			{
				this.m_retParam = new ParameterBuilder(this, 0, ParameterAttributes.None, null);
			}
			this.m_retParam.SetMarshal(unmanagedMarshal);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00103B3C File Offset: 0x00102B3C
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_module;
			if (moduleBuilder.GetSymWriter() == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			if (this.m_symCustomAttrs == null)
			{
				this.m_symCustomAttrs = new ArrayList();
			}
			this.m_symCustomAttrs.Add(new MethodBuilder.SymCustomAttr(name, data));
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00103BAC File Offset: 0x00102BAC
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			this.ThrowIfGeneric();
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			this.m_containingType.ThrowIfCreated();
			byte[] blob = null;
			if (!pset.IsEmpty())
			{
				blob = pset.EncodeXml();
			}
			TypeBuilder.InternalAddDeclarativeSecurity(this.m_module, this.MetadataTokenInternal, action, blob);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00103C2C File Offset: 0x00102C2C
		public void CreateMethodBody(byte[] il, int count)
		{
			this.ThrowIfGeneric();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_containingType.ThrowIfCreated();
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (il == null)
			{
				this.m_ubBody = null;
				return;
			}
			this.m_ubBody = new byte[count];
			Array.Copy(il, this.m_ubBody, count);
			this.m_bIsBaked = true;
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00103CAF File Offset: 0x00102CAF
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			this.m_dwMethodImplFlags = attributes;
			this.m_canBeRuntimeImpl = true;
			TypeBuilder.InternalSetMethodImpl(this.m_module, this.MetadataTokenInternal, attributes);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00103CE2 File Offset: 0x00102CE2
		public ILGenerator GetILGenerator()
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			if (this.m_ilGenerator == null)
			{
				this.m_ilGenerator = new ILGenerator(this);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00103D0A File Offset: 0x00102D0A
		public ILGenerator GetILGenerator(int size)
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			if (this.m_ilGenerator == null)
			{
				this.m_ilGenerator = new ILGenerator(this, size);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00103D33 File Offset: 0x00102D33
		private void ThrowIfShouldNotHaveBody()
		{
			if ((this.m_dwMethodImplFlags & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.m_dwMethodImplFlags & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL || (this.m_iAttributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope || this.m_isDllImport)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ShouldNotHaveMethodBody"));
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x00103D6F File Offset: 0x00102D6F
		// (set) Token: 0x06004ACF RID: 19151 RVA: 0x00103D7D File Offset: 0x00102D7D
		public bool InitLocals
		{
			get
			{
				this.ThrowIfGeneric();
				return this.m_fInitLocals;
			}
			set
			{
				this.ThrowIfGeneric();
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00103D8C File Offset: 0x00102D8C
		public Module GetModule()
		{
			return this.m_module;
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x00103D94 File Offset: 0x00102D94
		public string Signature
		{
			get
			{
				return this.GetMethodSignature().ToString();
			}
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00103DA4 File Offset: 0x00102DA4
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.ThrowIfGeneric();
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.InternalCreateCustomAttribute(this.MetadataTokenInternal, ((ModuleBuilder)this.m_module).GetConstructorToken(con).Token, binaryAttribute, this.m_module, false);
			if (this.IsKnownCA(con))
			{
				this.ParseCA(con, binaryAttribute);
			}
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00103E10 File Offset: 0x00102E10
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.ThrowIfGeneric();
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute((ModuleBuilder)this.m_module, this.MetadataTokenInternal);
			if (this.IsKnownCA(customBuilder.m_con))
			{
				this.ParseCA(customBuilder.m_con, customBuilder.m_blob);
			}
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00103E68 File Offset: 0x00102E68
		private bool IsKnownCA(ConstructorInfo con)
		{
			Type declaringType = con.DeclaringType;
			return declaringType == typeof(MethodImplAttribute) || declaringType == typeof(DllImportAttribute);
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00103E9C File Offset: 0x00102E9C
		private void ParseCA(ConstructorInfo con, byte[] blob)
		{
			Type declaringType = con.DeclaringType;
			if (declaringType == typeof(MethodImplAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				return;
			}
			if (declaringType == typeof(DllImportAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				this.m_isDllImport = true;
			}
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00103EE0 File Offset: 0x00102EE0
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00103EE7 File Offset: 0x00102EE7
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00103EEE File Offset: 0x00102EEE
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00103EF5 File Offset: 0x00102EF5
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002625 RID: 9765
		internal string m_strName;

		// Token: 0x04002626 RID: 9766
		private MethodToken m_tkMethod;

		// Token: 0x04002627 RID: 9767
		internal Module m_module;

		// Token: 0x04002628 RID: 9768
		internal TypeBuilder m_containingType;

		// Token: 0x04002629 RID: 9769
		private MethodBuilder m_link;

		// Token: 0x0400262A RID: 9770
		private int[] m_RVAFixups;

		// Token: 0x0400262B RID: 9771
		private int[] m_mdMethodFixups;

		// Token: 0x0400262C RID: 9772
		private SignatureHelper m_localSignature;

		// Token: 0x0400262D RID: 9773
		internal LocalSymInfo m_localSymInfo;

		// Token: 0x0400262E RID: 9774
		internal ILGenerator m_ilGenerator;

		// Token: 0x0400262F RID: 9775
		private byte[] m_ubBody;

		// Token: 0x04002630 RID: 9776
		private int m_numExceptions;

		// Token: 0x04002631 RID: 9777
		private __ExceptionInstance[] m_exceptions;

		// Token: 0x04002632 RID: 9778
		internal bool m_bIsBaked;

		// Token: 0x04002633 RID: 9779
		private bool m_bIsGlobalMethod;

		// Token: 0x04002634 RID: 9780
		private bool m_fInitLocals;

		// Token: 0x04002635 RID: 9781
		private MethodAttributes m_iAttributes;

		// Token: 0x04002636 RID: 9782
		private CallingConventions m_callingConvention;

		// Token: 0x04002637 RID: 9783
		private MethodImplAttributes m_dwMethodImplFlags;

		// Token: 0x04002638 RID: 9784
		private SignatureHelper m_signature;

		// Token: 0x04002639 RID: 9785
		internal Type[] m_parameterTypes;

		// Token: 0x0400263A RID: 9786
		private ParameterBuilder m_retParam;

		// Token: 0x0400263B RID: 9787
		internal Type m_returnType;

		// Token: 0x0400263C RID: 9788
		private Type[] m_returnTypeRequiredCustomModifiers;

		// Token: 0x0400263D RID: 9789
		private Type[] m_returnTypeOptionalCustomModifiers;

		// Token: 0x0400263E RID: 9790
		private Type[][] m_parameterTypeRequiredCustomModifiers;

		// Token: 0x0400263F RID: 9791
		private Type[][] m_parameterTypeOptionalCustomModifiers;

		// Token: 0x04002640 RID: 9792
		private GenericTypeParameterBuilder[] m_inst;

		// Token: 0x04002641 RID: 9793
		private bool m_bIsGenMethDef;

		// Token: 0x04002642 RID: 9794
		private ArrayList m_symCustomAttrs;

		// Token: 0x04002643 RID: 9795
		internal bool m_canBeRuntimeImpl;

		// Token: 0x04002644 RID: 9796
		internal bool m_isDllImport;

		// Token: 0x02000830 RID: 2096
		private struct SymCustomAttr
		{
			// Token: 0x06004ADA RID: 19162 RVA: 0x00103EFC File Offset: 0x00102EFC
			public SymCustomAttr(string name, byte[] data)
			{
				this.m_name = name;
				this.m_data = data;
			}

			// Token: 0x04002645 RID: 9797
			public string m_name;

			// Token: 0x04002646 RID: 9798
			public byte[] m_data;
		}
	}
}
