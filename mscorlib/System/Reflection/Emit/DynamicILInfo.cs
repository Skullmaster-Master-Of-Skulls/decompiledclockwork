using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200081B RID: 2075
	[ComVisible(true)]
	public class DynamicILInfo
	{
		// Token: 0x060049B0 RID: 18864 RVA: 0x00100AFC File Offset: 0x000FFAFC
		internal DynamicILInfo(DynamicScope scope, DynamicMethod method, byte[] methodSignature)
		{
			this.m_method = method;
			this.m_scope = scope;
			this.m_methodSignature = this.m_scope.GetTokenFor(methodSignature);
			this.m_exceptions = new byte[0];
			this.m_code = new byte[0];
			this.m_localSignature = new byte[0];
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x00100B53 File Offset: 0x000FFB53
		internal unsafe RuntimeMethodHandle GetCallableMethod(void* module)
		{
			return new RuntimeMethodHandle(ModuleHandle.GetDynamicMethod(module, this.m_method.Name, (byte[])this.m_scope[this.m_methodSignature], new DynamicResolver(this)));
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x00100B87 File Offset: 0x000FFB87
		internal byte[] LocalSignature
		{
			get
			{
				if (this.m_localSignature == null)
				{
					this.m_localSignature = SignatureHelper.GetLocalVarSigHelper().InternalGetSignatureArray();
				}
				return this.m_localSignature;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x00100BA7 File Offset: 0x000FFBA7
		internal byte[] Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x00100BAF File Offset: 0x000FFBAF
		internal byte[] Code
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x00100BB7 File Offset: 0x000FFBB7
		internal int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x00100BBF File Offset: 0x000FFBBF
		public DynamicMethod DynamicMethod
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x00100BC7 File Offset: 0x000FFBC7
		internal DynamicScope DynamicScope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00100BCF File Offset: 0x000FFBCF
		public void SetCode(byte[] code, int maxStackSize)
		{
			if (code == null)
			{
				code = new byte[0];
			}
			this.m_code = (byte[])code.Clone();
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x00100BF4 File Offset: 0x000FFBF4
		[CLSCompliant(false)]
		public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
		{
			if (codeSize < 0)
			{
				throw new ArgumentOutOfRangeException("codeSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			this.m_code = new byte[codeSize];
			for (int i = 0; i < codeSize; i++)
			{
				this.m_code[i] = *code;
				code++;
			}
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00100C49 File Offset: 0x000FFC49
		public void SetExceptions(byte[] exceptions)
		{
			if (exceptions == null)
			{
				exceptions = new byte[0];
			}
			this.m_exceptions = (byte[])exceptions.Clone();
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x00100C68 File Offset: 0x000FFC68
		[CLSCompliant(false)]
		public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
		{
			if (exceptionsSize < 0)
			{
				throw new ArgumentOutOfRangeException("exceptionsSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			this.m_exceptions = new byte[exceptionsSize];
			for (int i = 0; i < exceptionsSize; i++)
			{
				this.m_exceptions[i] = *exceptions;
				exceptions++;
			}
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00100CB6 File Offset: 0x000FFCB6
		public void SetLocalSignature(byte[] localSignature)
		{
			if (localSignature == null)
			{
				localSignature = new byte[0];
			}
			this.m_localSignature = (byte[])localSignature.Clone();
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00100CD4 File Offset: 0x000FFCD4
		[CLSCompliant(false)]
		public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
		{
			if (signatureSize < 0)
			{
				throw new ArgumentOutOfRangeException("signatureSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			this.m_localSignature = new byte[signatureSize];
			for (int i = 0; i < signatureSize; i++)
			{
				this.m_localSignature[i] = *localSignature;
				localSignature++;
			}
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00100D22 File Offset: 0x000FFD22
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x00100D30 File Offset: 0x000FFD30
		public int GetTokenFor(DynamicMethod method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x00100D3E File Offset: 0x000FFD3E
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(method, contextType);
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00100D4D File Offset: 0x000FFD4D
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			return this.DynamicScope.GetTokenFor(field);
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x00100D5B File Offset: 0x000FFD5B
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			return this.DynamicScope.GetTokenFor(type);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x00100D69 File Offset: 0x000FFD69
		public int GetTokenFor(string literal)
		{
			return this.DynamicScope.GetTokenFor(literal);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x00100D77 File Offset: 0x000FFD77
		public int GetTokenFor(byte[] signature)
		{
			return this.DynamicScope.GetTokenFor(signature);
		}

		// Token: 0x040025BE RID: 9662
		private DynamicMethod m_method;

		// Token: 0x040025BF RID: 9663
		private DynamicScope m_scope;

		// Token: 0x040025C0 RID: 9664
		private byte[] m_exceptions;

		// Token: 0x040025C1 RID: 9665
		private byte[] m_code;

		// Token: 0x040025C2 RID: 9666
		private byte[] m_localSignature;

		// Token: 0x040025C3 RID: 9667
		private int m_maxStackSize;

		// Token: 0x040025C4 RID: 9668
		private int m_methodSignature;
	}
}
