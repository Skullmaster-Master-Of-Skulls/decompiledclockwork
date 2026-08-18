using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000823 RID: 2083
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x06004A1F RID: 18975 RVA: 0x00101C29 File Offset: 0x00100C29
		private EventBuilder()
		{
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x00101C31 File Offset: 0x00100C31
		internal EventBuilder(Module mod, string name, EventAttributes attr, int eventType, TypeBuilder type, EventToken evToken)
		{
			this.m_name = name;
			this.m_module = mod;
			this.m_attributes = attr;
			this.m_evToken = evToken;
			this.m_type = type;
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00101C5E File Offset: 0x00100C5E
		public EventToken GetEventToken()
		{
			return this.m_evToken;
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00101C68 File Offset: 0x00100C68
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_evToken.Token, MethodSemanticsAttributes.AddOn, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00101CB4 File Offset: 0x00100CB4
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_evToken.Token, MethodSemanticsAttributes.RemoveOn, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00101D00 File Offset: 0x00100D00
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_evToken.Token, MethodSemanticsAttributes.Fire, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x00101D4C File Offset: 0x00100D4C
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_type.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_evToken.Token, MethodSemanticsAttributes.Other, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x00101D98 File Offset: 0x00100D98
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
			this.m_type.ThrowIfCreated();
			TypeBuilder.InternalCreateCustomAttribute(this.m_evToken.Token, ((ModuleBuilder)this.m_module).GetConstructorToken(con).Token, binaryAttribute, this.m_module, false);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00101DFD File Offset: 0x00100DFD
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_type.ThrowIfCreated();
			customBuilder.CreateCustomAttribute((ModuleBuilder)this.m_module, this.m_evToken.Token);
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00101E34 File Offset: 0x00100E34
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x00101E3B File Offset: 0x00100E3B
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x00101E42 File Offset: 0x00100E42
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x00101E49 File Offset: 0x00100E49
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040025E0 RID: 9696
		private string m_name;

		// Token: 0x040025E1 RID: 9697
		private EventToken m_evToken;

		// Token: 0x040025E2 RID: 9698
		private Module m_module;

		// Token: 0x040025E3 RID: 9699
		private EventAttributes m_attributes;

		// Token: 0x040025E4 RID: 9700
		private TypeBuilder m_type;
	}
}
