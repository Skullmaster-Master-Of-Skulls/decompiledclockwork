using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000815 RID: 2069
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x06004912 RID: 18706 RVA: 0x000FD8A7 File Offset: 0x000FC8A7
		private ConstructorBuilder()
		{
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000FD8B0 File Offset: 0x000FC8B0
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, Module mod, TypeBuilder type)
		{
			this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, null, null, null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type, false);
			type.m_listMethods.Add(this.m_methodBuilder);
			int num;
			this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out num);
			this.m_methodBuilder.GetToken();
			this.m_ReturnILGen = true;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x000FD918 File Offset: 0x000FC918
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Module mod, TypeBuilder type) : this(name, attributes, callingConvention, parameterTypes, null, null, mod, type)
		{
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000FD936 File Offset: 0x000FC936
		internal override Type[] GetParameterTypes()
		{
			return this.m_methodBuilder.GetParameterTypes();
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x000FD943 File Offset: 0x000FC943
		public override string ToString()
		{
			return this.m_methodBuilder.ToString();
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004917 RID: 18711 RVA: 0x000FD950 File Offset: 0x000FC950
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_methodBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x000FD95D File Offset: 0x000FC95D
		public override Module Module
		{
			get
			{
				return this.m_methodBuilder.Module;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x000FD96A File Offset: 0x000FC96A
		public override Type ReflectedType
		{
			get
			{
				return this.m_methodBuilder.ReflectedType;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x000FD977 File Offset: 0x000FC977
		public override Type DeclaringType
		{
			get
			{
				return this.m_methodBuilder.DeclaringType;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x000FD984 File Offset: 0x000FC984
		public override string Name
		{
			get
			{
				return this.m_methodBuilder.Name;
			}
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x000FD991 File Offset: 0x000FC991
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x000FD9A4 File Offset: 0x000FC9A4
		public override ParameterInfo[] GetParameters()
		{
			if (!this.m_methodBuilder.m_bIsBaked)
			{
				throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
			}
			Type runtimeType = this.m_methodBuilder.GetTypeBuilder().m_runtimeType;
			ConstructorInfo constructor = runtimeType.GetConstructor(this.m_methodBuilder.m_parameterTypes);
			return constructor.GetParameters();
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x000FD9F7 File Offset: 0x000FC9F7
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodBuilder.Attributes;
			}
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x000FDA04 File Offset: 0x000FCA04
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_methodBuilder.GetMethodImplementationFlags();
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x000FDA11 File Offset: 0x000FCA11
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_methodBuilder.MethodHandle;
			}
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x000FDA1E File Offset: 0x000FCA1E
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x000FDA2F File Offset: 0x000FCA2F
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x000FDA3D File Offset: 0x000FCA3D
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x000FDA4C File Offset: 0x000FCA4C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x000FDA5B File Offset: 0x000FCA5B
		public MethodToken GetToken()
		{
			return this.m_methodBuilder.GetToken();
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x000FDA68 File Offset: 0x000FCA68
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			attributes &= ~ParameterAttributes.ReservedMask;
			return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x000FDA81 File Offset: 0x000FCA81
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.m_methodBuilder.SetSymCustomAttribute(name, data);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x000FDA90 File Offset: 0x000FCA90
		public ILGenerator GetILGenerator()
		{
			if (!this.m_ReturnILGen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator();
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x000FDAB5 File Offset: 0x000FCAB5
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (!this.m_ReturnILGen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator(streamSize);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x000FDADC File Offset: 0x000FCADC
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			if (this.m_methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			byte[] blob = pset.EncodeXml();
			TypeBuilder.InternalAddDeclarativeSecurity(this.GetModule(), this.GetToken().Token, action, blob);
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x000FDB66 File Offset: 0x000FCB66
		public override CallingConventions CallingConvention
		{
			get
			{
				if (this.DeclaringType.IsGenericType)
				{
					return CallingConventions.HasThis;
				}
				return CallingConventions.Standard;
			}
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x000FDB79 File Offset: 0x000FCB79
		public Module GetModule()
		{
			return this.m_methodBuilder.GetModule();
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x000FDB86 File Offset: 0x000FCB86
		public Type ReturnType
		{
			get
			{
				return this.m_methodBuilder.GetReturnType();
			}
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x000FDB93 File Offset: 0x000FCB93
		internal override Type GetReturnType()
		{
			return this.m_methodBuilder.GetReturnType();
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x000FDBA0 File Offset: 0x000FCBA0
		public string Signature
		{
			get
			{
				return this.m_methodBuilder.Signature;
			}
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x000FDBAD File Offset: 0x000FCBAD
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x000FDBBC File Offset: 0x000FCBBC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_methodBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x000FDBCA File Offset: 0x000FCBCA
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.m_methodBuilder.SetImplementationFlags(attributes);
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x000FDBD8 File Offset: 0x000FCBD8
		// (set) Token: 0x06004934 RID: 18740 RVA: 0x000FDBE5 File Offset: 0x000FCBE5
		public bool InitLocals
		{
			get
			{
				return this.m_methodBuilder.InitLocals;
			}
			set
			{
				this.m_methodBuilder.InitLocals = value;
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x000FDBF3 File Offset: 0x000FCBF3
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x000FDBFA File Offset: 0x000FCBFA
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x000FDC01 File Offset: 0x000FCC01
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x000FDC08 File Offset: 0x000FCC08
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002590 RID: 9616
		internal MethodBuilder m_methodBuilder;

		// Token: 0x04002591 RID: 9617
		internal bool m_ReturnILGen;
	}
}
