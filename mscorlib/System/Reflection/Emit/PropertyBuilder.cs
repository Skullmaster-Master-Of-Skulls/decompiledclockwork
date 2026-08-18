using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000845 RID: 2117
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x06004C01 RID: 19457 RVA: 0x0010A575 File Offset: 0x00109575
		private PropertyBuilder()
		{
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0010A580 File Offset: 0x00109580
		internal PropertyBuilder(Module mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
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
			this.m_name = name;
			this.m_module = mod;
			this.m_signature = sig;
			this.m_attributes = attr;
			this.m_returnType = returnType;
			this.m_prToken = prToken;
			this.m_tkProperty = prToken.Token;
			this.m_getMethod = null;
			this.m_setMethod = null;
			this.m_containingType = containingType;
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0010A62C File Offset: 0x0010962C
		public void SetConstant(object defaultValue)
		{
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_module, this.m_prToken.Token, this.m_returnType, defaultValue);
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004C04 RID: 19460 RVA: 0x0010A656 File Offset: 0x00109656
		public PropertyToken PropertyToken
		{
			get
			{
				return this.m_prToken;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004C05 RID: 19461 RVA: 0x0010A65E File Offset: 0x0010965E
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_tkProperty;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004C06 RID: 19462 RVA: 0x0010A666 File Offset: 0x00109666
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0010A674 File Offset: 0x00109674
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_prToken.Token, MethodSemanticsAttributes.Getter, mdBuilder.GetToken().Token);
			this.m_getMethod = mdBuilder;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x0010A6C8 File Offset: 0x001096C8
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_prToken.Token, MethodSemanticsAttributes.Setter, mdBuilder.GetToken().Token);
			this.m_setMethod = mdBuilder;
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x0010A71C File Offset: 0x0010971C
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.InternalDefineMethodSemantics(this.m_module, this.m_prToken.Token, MethodSemanticsAttributes.Other, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0010A768 File Offset: 0x00109768
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
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.InternalCreateCustomAttribute(this.m_prToken.Token, ((ModuleBuilder)this.m_module).GetConstructorToken(con).Token, binaryAttribute, this.m_module, false);
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0010A7CD File Offset: 0x001097CD
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			customBuilder.CreateCustomAttribute((ModuleBuilder)this.m_module, this.m_prToken.Token);
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0010A804 File Offset: 0x00109804
		public override object GetValue(object obj, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x0010A815 File Offset: 0x00109815
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x0010A826 File Offset: 0x00109826
		public override void SetValue(object obj, object value, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x0010A837 File Offset: 0x00109837
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0010A848 File Offset: 0x00109848
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0010A859 File Offset: 0x00109859
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_getMethod == null)
			{
				return this.m_getMethod;
			}
			if ((this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_getMethod;
			}
			return null;
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0010A885 File Offset: 0x00109885
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_setMethod == null)
			{
				return this.m_setMethod;
			}
			if ((this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_setMethod;
			}
			return null;
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0010A8B1 File Offset: 0x001098B1
		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x0010A8C2 File Offset: 0x001098C2
		public override Type PropertyType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x0010A8CA File Offset: 0x001098CA
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x0010A8D2 File Offset: 0x001098D2
		public override bool CanRead
		{
			get
			{
				return this.m_getMethod != null;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x0010A8DF File Offset: 0x001098DF
		public override bool CanWrite
		{
			get
			{
				return this.m_setMethod != null;
			}
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x0010A8EC File Offset: 0x001098EC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x0010A8FD File Offset: 0x001098FD
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0010A90E File Offset: 0x0010990E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0010A91F File Offset: 0x0010991F
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0010A926 File Offset: 0x00109926
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0010A92D File Offset: 0x0010992D
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0010A934 File Offset: 0x00109934
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x0010A93B File Offset: 0x0010993B
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004C20 RID: 19488 RVA: 0x0010A943 File Offset: 0x00109943
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x0010A94B File Offset: 0x0010994B
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x040027D1 RID: 10193
		private string m_name;

		// Token: 0x040027D2 RID: 10194
		private PropertyToken m_prToken;

		// Token: 0x040027D3 RID: 10195
		private int m_tkProperty;

		// Token: 0x040027D4 RID: 10196
		private Module m_module;

		// Token: 0x040027D5 RID: 10197
		private SignatureHelper m_signature;

		// Token: 0x040027D6 RID: 10198
		private PropertyAttributes m_attributes;

		// Token: 0x040027D7 RID: 10199
		private Type m_returnType;

		// Token: 0x040027D8 RID: 10200
		private MethodInfo m_getMethod;

		// Token: 0x040027D9 RID: 10201
		private MethodInfo m_setMethod;

		// Token: 0x040027DA RID: 10202
		private TypeBuilder m_containingType;
	}
}
