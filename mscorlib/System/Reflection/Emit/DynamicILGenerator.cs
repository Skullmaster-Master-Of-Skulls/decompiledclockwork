using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000817 RID: 2071
	internal class DynamicILGenerator : ILGenerator
	{
		// Token: 0x0600497E RID: 18814 RVA: 0x000FF924 File Offset: 0x000FE924
		internal DynamicILGenerator(DynamicMethod method, byte[] methodSignature, int size) : base(method, size)
		{
			this.m_scope = new DynamicScope();
			this.m_methodSigToken = this.m_scope.GetTokenFor(methodSignature);
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x000FF94B File Offset: 0x000FE94B
		internal unsafe RuntimeMethodHandle GetCallableMethod(void* module)
		{
			return new RuntimeMethodHandle(ModuleHandle.GetDynamicMethod(module, this.m_methodBuilder.Name, (byte[])this.m_scope[this.m_methodSigToken], new DynamicResolver(this)));
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x000FF980 File Offset: 0x000FE980
		public override LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (localType.GetType() != typeof(RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			LocalBuilder result = new LocalBuilder(this.m_localCount, localType, this.m_methodBuilder);
			this.m_localSignature.AddArgument(localType, pinned);
			this.m_localCount++;
			return result;
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x000FF9EC File Offset: 0x000FE9EC
		public override void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			int num = 0;
			DynamicMethod dynamicMethod = meth as DynamicMethod;
			int tokenFor;
			if (dynamicMethod == null)
			{
				if (!(meth is RuntimeMethodInfo))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "meth");
				}
				if (meth.DeclaringType != null && (meth.DeclaringType.IsGenericType || meth.DeclaringType.IsArray))
				{
					tokenFor = this.m_scope.GetTokenFor(meth.MethodHandle, meth.DeclaringType.TypeHandle);
				}
				else
				{
					tokenFor = this.m_scope.GetTokenFor(meth.MethodHandle);
				}
			}
			else
			{
				if (opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOpCodeOnDynamicMethod"));
				}
				tokenFor = this.m_scope.GetTokenFor(dynamicMethod);
			}
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (opcode.m_push == StackBehaviour.Varpush && meth.ReturnType != typeof(void))
			{
				num++;
			}
			if (opcode.m_pop == StackBehaviour.Varpop)
			{
				num -= meth.GetParametersNoCopy().Length;
			}
			if (!meth.IsStatic && !opcode.Equals(OpCodes.Newobj) && !opcode.Equals(OpCodes.Ldtoken) && !opcode.Equals(OpCodes.Ldftn))
			{
				num--;
			}
			base.UpdateStackSize(opcode, num);
			this.m_length = this.PutInteger4(tokenFor, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x000FFB6C File Offset: 0x000FEB6C
		[ComVisible(true)]
		public override void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null || !(con is RuntimeConstructorInfo))
			{
				throw new ArgumentNullException("con");
			}
			if (con.DeclaringType != null && (con.DeclaringType.IsGenericType || con.DeclaringType.IsArray))
			{
				this.Emit(opcode, con.MethodHandle, con.DeclaringType.TypeHandle);
				return;
			}
			this.Emit(opcode, con.MethodHandle);
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x000FFBD8 File Offset: 0x000FEBD8
		public void Emit(OpCode opcode, RuntimeMethodHandle meth)
		{
			if (meth.IsNullHandle())
			{
				throw new ArgumentNullException("meth");
			}
			int tokenFor = this.m_scope.GetTokenFor(meth);
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.UpdateStackSize(opcode, 1);
			this.m_length = this.PutInteger4(tokenFor, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x000FFC38 File Offset: 0x000FEC38
		public void Emit(OpCode opcode, RuntimeMethodHandle meth, RuntimeTypeHandle typeContext)
		{
			if (meth.IsNullHandle())
			{
				throw new ArgumentNullException("meth");
			}
			if (typeContext.IsNullHandle())
			{
				throw new ArgumentNullException("typeContext");
			}
			int tokenFor = this.m_scope.GetTokenFor(meth, typeContext);
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.UpdateStackSize(opcode, 1);
			this.m_length = this.PutInteger4(tokenFor, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x000FFCAA File Offset: 0x000FECAA
		public override void Emit(OpCode opcode, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.Emit(opcode, type.TypeHandle);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x000FFCC8 File Offset: 0x000FECC8
		public void Emit(OpCode opcode, RuntimeTypeHandle typeHandle)
		{
			if (typeHandle.IsNullHandle())
			{
				throw new ArgumentNullException("typeHandle");
			}
			int tokenFor = this.m_scope.GetTokenFor(typeHandle);
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			this.m_length = this.PutInteger4(tokenFor, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x000FFD20 File Offset: 0x000FED20
		public override void Emit(OpCode opcode, FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (!(field is RuntimeFieldInfo))
			{
				throw new ArgumentNullException("field");
			}
			if (field.DeclaringType == null)
			{
				this.Emit(opcode, field.FieldHandle);
				return;
			}
			this.Emit(opcode, field.FieldHandle, field.DeclaringType.GetTypeHandleInternal());
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x000FFD7C File Offset: 0x000FED7C
		public void Emit(OpCode opcode, RuntimeFieldHandle fieldHandle)
		{
			if (fieldHandle.IsNullHandle())
			{
				throw new ArgumentNullException("fieldHandle");
			}
			int tokenFor = this.m_scope.GetTokenFor(fieldHandle);
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			this.m_length = this.PutInteger4(tokenFor, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x000FFDD4 File Offset: 0x000FEDD4
		public void Emit(OpCode opcode, RuntimeFieldHandle fieldHandle, RuntimeTypeHandle typeContext)
		{
			if (fieldHandle.IsNullHandle())
			{
				throw new ArgumentNullException("fieldHandle");
			}
			int tokenFor = this.m_scope.GetTokenFor(fieldHandle, typeContext);
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			this.m_length = this.PutInteger4(tokenFor, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x000FFE2C File Offset: 0x000FEE2C
		public override void Emit(OpCode opcode, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int num = this.AddStringLiteral(str);
			num |= 1879048192;
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			this.m_length = this.PutInteger4(num, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x000FFE80 File Offset: 0x000FEE80
		public override void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				num--;
			}
			num--;
			base.UpdateStackSize(opcode, num);
			int value = this.AddSignature(memberRefSignature.GetSignature(true));
			this.m_length = this.PutInteger4(value, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x000FFF30 File Offset: 0x000FEF30
		public override void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(unmanagedCallConv, returnType);
			if (parameterTypes != null)
			{
				for (int i = 0; i < num2; i++)
				{
					methodSigHelper.AddArgument(parameterTypes[i]);
				}
			}
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= num2;
			}
			num--;
			base.UpdateStackSize(opcode, num);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			int value = this.AddSignature(methodSigHelper.GetSignature(true));
			this.m_length = this.PutInteger4(value, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x000FFFCC File Offset: 0x000FEFCC
		public override void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (methodInfo.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "methodInfo");
			}
			if (methodInfo.DeclaringType != null && methodInfo.DeclaringType.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "methodInfo");
			}
			int memberRefToken = this.GetMemberRefToken(methodInfo, optionalParameterTypes);
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			num -= methodInfo.GetParameterTypes().Length;
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			base.UpdateStackSize(opcode, num);
			this.m_length = this.PutInteger4(memberRefToken, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x001000B4 File Offset: 0x000FF0B4
		public override void Emit(OpCode opcode, SignatureHelper signature)
		{
			int num = 0;
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			this.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (opcode.m_pop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				base.UpdateStackSize(opcode, num);
			}
			int value = this.AddSignature(signature.GetSignature(true));
			this.m_length = this.PutInteger4(value, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x00100126 File Offset: 0x000FF126
		public override Label BeginExceptionBlock()
		{
			return base.BeginExceptionBlock();
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x0010012E File Offset: 0x000FF12E
		public override void EndExceptionBlock()
		{
			base.EndExceptionBlock();
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x00100136 File Offset: 0x000FF136
		public override void BeginExceptFilterBlock()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x00100148 File Offset: 0x000FF148
		public override void BeginCatchBlock(Type exceptionType)
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			if (_ExceptionInfo.GetCurrentState() == 1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ShouldNotSpecifyExceptionType"));
				}
				this.Emit(OpCodes.Endfilter);
			}
			else
			{
				if (exceptionType == null)
				{
					throw new ArgumentNullException("exceptionType");
				}
				if (exceptionType.GetType() != typeof(RuntimeType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
				}
				Label endLabel = _ExceptionInfo.GetEndLabel();
				this.Emit(OpCodes.Leave, endLabel);
				base.UpdateStackSize(OpCodes.Nop, 1);
			}
			_ExceptionInfo.MarkCatchAddr(this.m_length, exceptionType);
			_ExceptionInfo.m_filterAddr[_ExceptionInfo.m_currentCatch - 1] = this.m_scope.GetTokenFor(exceptionType.TypeHandle);
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00100222 File Offset: 0x000FF222
		public override void BeginFaultBlock()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00100233 File Offset: 0x000FF233
		public override void BeginFinallyBlock()
		{
			base.BeginFinallyBlock();
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0010023B File Offset: 0x000FF23B
		public override void UsingNamespace(string ns)
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x0010024C File Offset: 0x000FF24C
		public override void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x0010025D File Offset: 0x000FF25D
		public override void BeginScope()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0010026E File Offset: 0x000FF26E
		public override void EndScope()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x0010027F File Offset: 0x000FF27F
		internal override int GetMaxStackSize()
		{
			return this.m_maxStackSize;
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00100288 File Offset: 0x000FF288
		internal override int GetMemberRefToken(MethodBase methodInfo, Type[] optionalParameterTypes)
		{
			if (optionalParameterTypes != null && (methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			if (!(methodInfo is RuntimeMethodInfo) && !(methodInfo is DynamicMethod))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "methodInfo");
			}
			ParameterInfo[] parametersNoCopy = methodInfo.GetParametersNoCopy();
			Type[] array;
			if (parametersNoCopy != null && parametersNoCopy.Length != 0)
			{
				array = new Type[parametersNoCopy.Length];
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					array[i] = parametersNoCopy[i].ParameterType;
				}
			}
			else
			{
				array = null;
			}
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(methodInfo.CallingConvention, methodInfo.GetReturnType(), array, optionalParameterTypes);
			return this.m_scope.GetTokenFor(new VarArgMethod(methodInfo as MethodInfo, memberRefSignature));
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00100344 File Offset: 0x000FF344
		internal override SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num;
			if (parameterTypes == null)
			{
				num = 0;
			}
			else
			{
				num = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(call, returnType);
			for (int i = 0; i < num; i++)
			{
				methodSigHelper.AddArgument(parameterTypes[i]);
			}
			if (optionalParameterTypes != null && optionalParameterTypes.Length != 0)
			{
				methodSigHelper.AddSentinel();
				for (int i = 0; i < optionalParameterTypes.Length; i++)
				{
					methodSigHelper.AddArgument(optionalParameterTypes[i]);
				}
			}
			return methodSigHelper;
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x001003A4 File Offset: 0x000FF3A4
		private int AddStringLiteral(string s)
		{
			int tokenFor = this.m_scope.GetTokenFor(s);
			return tokenFor | 1879048192;
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x001003C8 File Offset: 0x000FF3C8
		private int AddSignature(byte[] sig)
		{
			int tokenFor = this.m_scope.GetTokenFor(sig);
			return tokenFor | 285212672;
		}

		// Token: 0x040025AD RID: 9645
		internal DynamicScope m_scope;

		// Token: 0x040025AE RID: 9646
		private int m_methodSigToken;
	}
}
