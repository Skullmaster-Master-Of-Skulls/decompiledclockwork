using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000843 RID: 2115
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x06004BE7 RID: 19431 RVA: 0x0010A2F4 File Offset: 0x001092F4
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			if (unmanagedMarshal == null)
			{
				throw new ArgumentNullException("unmanagedMarshal");
			}
			byte[] array = unmanagedMarshal.InternalGetBytes();
			TypeBuilder.InternalSetMarshalInfo(this.m_methodBuilder.GetModule(), this.m_pdToken.Token, array, array.Length);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0010A338 File Offset: 0x00109338
		public virtual void SetConstant(object defaultValue)
		{
			TypeBuilder.SetConstantValue(this.m_methodBuilder.GetModule(), this.m_pdToken.Token, (this.m_iPosition == 0) ? this.m_methodBuilder.m_returnType : this.m_methodBuilder.m_parameterTypes[this.m_iPosition - 1], defaultValue);
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0010A38C File Offset: 0x0010938C
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.InternalCreateCustomAttribute(this.m_pdToken.Token, ((ModuleBuilder)this.m_methodBuilder.GetModule()).GetConstructorToken(con).Token, binaryAttribute, this.m_methodBuilder.GetModule(), false);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0010A3F0 File Offset: 0x001093F0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute((ModuleBuilder)this.m_methodBuilder.GetModule(), this.m_pdToken.Token);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0010A421 File Offset: 0x00109421
		private ParameterBuilder()
		{
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0010A42C File Offset: 0x0010942C
		internal ParameterBuilder(MethodBuilder methodBuilder, int sequence, ParameterAttributes attributes, string strParamName)
		{
			this.m_iPosition = sequence;
			this.m_strParamName = strParamName;
			this.m_methodBuilder = methodBuilder;
			this.m_strParamName = strParamName;
			this.m_attributes = attributes;
			this.m_pdToken = new ParameterToken(TypeBuilder.InternalSetParamInfo(this.m_methodBuilder.GetModule(), this.m_methodBuilder.GetToken().Token, sequence, attributes, strParamName));
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0010A496 File Offset: 0x00109496
		public virtual ParameterToken GetToken()
		{
			return this.m_pdToken;
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0010A49E File Offset: 0x0010949E
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0010A4A5 File Offset: 0x001094A5
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0010A4AC File Offset: 0x001094AC
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0010A4B3 File Offset: 0x001094B3
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x0010A4BA File Offset: 0x001094BA
		internal virtual int MetadataTokenInternal
		{
			get
			{
				return this.m_pdToken.Token;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x0010A4C7 File Offset: 0x001094C7
		public virtual string Name
		{
			get
			{
				return this.m_strParamName;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x0010A4CF File Offset: 0x001094CF
		public virtual int Position
		{
			get
			{
				return this.m_iPosition;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x0010A4D7 File Offset: 0x001094D7
		public virtual int Attributes
		{
			get
			{
				return (int)this.m_attributes;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0010A4DF File Offset: 0x001094DF
		public bool IsIn
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.In) != ParameterAttributes.None;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004BF7 RID: 19447 RVA: 0x0010A4EF File Offset: 0x001094EF
		public bool IsOut
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.Out) != ParameterAttributes.None;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0010A4FF File Offset: 0x001094FF
		public bool IsOptional
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.Optional) != ParameterAttributes.None;
			}
		}

		// Token: 0x040027CA RID: 10186
		private string m_strParamName;

		// Token: 0x040027CB RID: 10187
		private int m_iPosition;

		// Token: 0x040027CC RID: 10188
		private ParameterAttributes m_attributes;

		// Token: 0x040027CD RID: 10189
		private MethodBuilder m_methodBuilder;

		// Token: 0x040027CE RID: 10190
		private ParameterToken m_pdToken;
	}
}
