using System;
using System.Collections;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200081C RID: 2076
	internal class DynamicScope
	{
		// Token: 0x060049C5 RID: 18885 RVA: 0x00100D85 File Offset: 0x000FFD85
		internal DynamicScope()
		{
			this.m_tokens = new ArrayList();
			this.m_tokens.Add(null);
		}

		// Token: 0x17000CA7 RID: 3239
		internal object this[int token]
		{
			get
			{
				token &= 16777215;
				if (token < 0 || token > this.m_tokens.Count)
				{
					return null;
				}
				return this.m_tokens[token];
			}
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x00100DD0 File Offset: 0x000FFDD0
		internal int GetTokenFor(VarArgMethod varArgMethod)
		{
			return this.m_tokens.Add(varArgMethod) | 167772160;
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x00100DE4 File Offset: 0x000FFDE4
		internal string GetString(int token)
		{
			return this[token] as string;
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x00100DF4 File Offset: 0x000FFDF4
		internal byte[] ResolveSignature(int token, int fromMethod)
		{
			if (fromMethod == 0)
			{
				return (byte[])this[token];
			}
			VarArgMethod varArgMethod = this[token] as VarArgMethod;
			if (varArgMethod == null)
			{
				return null;
			}
			return varArgMethod.m_signature.GetSignature(true);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x00100E30 File Offset: 0x000FFE30
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			MethodBase methodBase = RuntimeType.GetMethodBase(method);
			if (methodBase.DeclaringType != null && methodBase.DeclaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGenericLcg"), new object[]
				{
					methodBase,
					methodBase.DeclaringType.GetGenericTypeDefinition()
				}));
			}
			return this.m_tokens.Add(method) | 100663296;
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x00100EA4 File Offset: 0x000FFEA4
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle typeContext)
		{
			return this.m_tokens.Add(new GenericMethodInfo(method, typeContext)) | 100663296;
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x00100EBE File Offset: 0x000FFEBE
		public int GetTokenFor(DynamicMethod method)
		{
			return this.m_tokens.Add(method) | 100663296;
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x00100ED2 File Offset: 0x000FFED2
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			return this.m_tokens.Add(field) | 67108864;
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x00100EEB File Offset: 0x000FFEEB
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle typeContext)
		{
			return this.m_tokens.Add(new GenericFieldInfo(field, typeContext)) | 67108864;
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x00100F05 File Offset: 0x000FFF05
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			return this.m_tokens.Add(type) | 33554432;
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x00100F1E File Offset: 0x000FFF1E
		public int GetTokenFor(string literal)
		{
			return this.m_tokens.Add(literal) | 1879048192;
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x00100F32 File Offset: 0x000FFF32
		public int GetTokenFor(byte[] signature)
		{
			return this.m_tokens.Add(signature) | 285212672;
		}

		// Token: 0x040025C5 RID: 9669
		internal ArrayList m_tokens;
	}
}
