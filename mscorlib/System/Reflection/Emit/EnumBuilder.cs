using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200084F RID: 2127
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EnumBuilder : Type, _EnumBuilder
	{
		// Token: 0x06004DB4 RID: 19892 RVA: 0x0010F10C File Offset: 0x0010E10C
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0010F131 File Offset: 0x0010E131
		public Type CreateType()
		{
			return this.m_typeBuilder.CreateType();
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x0010F13E File Offset: 0x0010E13E
		public TypeToken TypeToken
		{
			get
			{
				return this.m_typeBuilder.TypeToken;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x0010F14B File Offset: 0x0010E14B
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this.m_underlyingField;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x0010F153 File Offset: 0x0010E153
		public override string Name
		{
			get
			{
				return this.m_typeBuilder.Name;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x0010F160 File Offset: 0x0010E160
		public override Guid GUID
		{
			get
			{
				return this.m_typeBuilder.GUID;
			}
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x0010F170 File Offset: 0x0010E170
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06004DBB RID: 19899 RVA: 0x0010F195 File Offset: 0x0010E195
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x0010F1A2 File Offset: 0x0010E1A2
		public override Assembly Assembly
		{
			get
			{
				return this.m_typeBuilder.Assembly;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x0010F1AF File Offset: 0x0010E1AF
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.m_typeBuilder.TypeHandle;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x0010F1BC File Offset: 0x0010E1BC
		public override string FullName
		{
			get
			{
				return this.m_typeBuilder.FullName;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x0010F1C9 File Offset: 0x0010E1C9
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.m_typeBuilder.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x0010F1D6 File Offset: 0x0010E1D6
		public override string Namespace
		{
			get
			{
				return this.m_typeBuilder.Namespace;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x0010F1E3 File Offset: 0x0010E1E3
		public override Type BaseType
		{
			get
			{
				return this.m_typeBuilder.BaseType;
			}
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x0010F1F0 File Offset: 0x0010E1F0
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0010F204 File Offset: 0x0010E204
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetConstructors(bindingAttr);
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x0010F212 File Offset: 0x0010E212
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.m_typeBuilder.GetMethod(name, bindingAttr);
			}
			return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x0010F23A File Offset: 0x0010E23A
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMethods(bindingAttr);
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x0010F248 File Offset: 0x0010E248
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetField(name, bindingAttr);
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x0010F257 File Offset: 0x0010E257
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetFields(bindingAttr);
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x0010F265 File Offset: 0x0010E265
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.m_typeBuilder.GetInterface(name, ignoreCase);
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x0010F274 File Offset: 0x0010E274
		public override Type[] GetInterfaces()
		{
			return this.m_typeBuilder.GetInterfaces();
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x0010F281 File Offset: 0x0010E281
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004DCB RID: 19915 RVA: 0x0010F290 File Offset: 0x0010E290
		public override EventInfo[] GetEvents()
		{
			return this.m_typeBuilder.GetEvents();
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0010F29D File Offset: 0x0010E29D
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0010F2AE File Offset: 0x0010E2AE
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetProperties(bindingAttr);
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0010F2BC File Offset: 0x0010E2BC
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x0010F2CA File Offset: 0x0010E2CA
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0010F2D9 File Offset: 0x0010E2D9
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMember(name, type, bindingAttr);
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0010F2E9 File Offset: 0x0010E2E9
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetMembers(bindingAttr);
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0010F2F7 File Offset: 0x0010E2F7
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.m_typeBuilder.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0010F305 File Offset: 0x0010E305
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.m_typeBuilder.GetEvents(bindingAttr);
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x0010F313 File Offset: 0x0010E313
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.m_typeBuilder.m_iAttr;
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0010F320 File Offset: 0x0010E320
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x0010F323 File Offset: 0x0010E323
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x0010F326 File Offset: 0x0010E326
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0010F329 File Offset: 0x0010E329
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0010F32C File Offset: 0x0010E32C
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0010F32F File Offset: 0x0010E32F
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0010F332 File Offset: 0x0010E332
		public override Type GetElementType()
		{
			return this.m_typeBuilder.GetElementType();
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0010F33F File Offset: 0x0010E33F
		protected override bool HasElementTypeImpl()
		{
			return this.m_typeBuilder.HasElementType;
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004DDD RID: 19933 RVA: 0x0010F34C File Offset: 0x0010E34C
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.m_underlyingType;
			}
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x0010F354 File Offset: 0x0010E354
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x0010F362 File Offset: 0x0010E362
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0010F371 File Offset: 0x0010E371
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0010F380 File Offset: 0x0010E380
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_typeBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004DE2 RID: 19938 RVA: 0x0010F38E File Offset: 0x0010E38E
		public override Type DeclaringType
		{
			get
			{
				return this.m_typeBuilder.DeclaringType;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06004DE3 RID: 19939 RVA: 0x0010F39B File Offset: 0x0010E39B
		public override Type ReflectedType
		{
			get
			{
				return this.m_typeBuilder.ReflectedType;
			}
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x0010F3A8 File Offset: 0x0010E3A8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_typeBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004DE5 RID: 19941 RVA: 0x0010F3B7 File Offset: 0x0010E3B7
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_typeBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0010F3C4 File Offset: 0x0010E3C4
		private EnumBuilder()
		{
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0010F3CC File Offset: 0x0010E3CC
		public override Type MakePointerType()
		{
			return SymbolType.FormCompoundType("*".ToCharArray(), this, 0);
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0010F3DF File Offset: 0x0010E3DF
		public override Type MakeByRefType()
		{
			return SymbolType.FormCompoundType("&".ToCharArray(), this, 0);
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0010F3F2 File Offset: 0x0010E3F2
		public override Type MakeArrayType()
		{
			return SymbolType.FormCompoundType("[]".ToCharArray(), this, 0);
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0010F408 File Offset: 0x0010E408
		public override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			string text = "";
			if (rank == 1)
			{
				text = "*";
			}
			else
			{
				for (int i = 1; i < rank; i++)
				{
					text += ",";
				}
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
			{
				text
			});
			return SymbolType.FormCompoundType(text2.ToCharArray(), this, 0);
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x0010F474 File Offset: 0x0010E474
		internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, Module module)
		{
			if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ShouldOnlySetVisibilityFlags"), "name");
			}
			this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof(Enum), null, module, PackingSize.Unspecified, null);
			this.m_underlyingType = underlyingType;
			this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.Private | FieldAttributes.SpecialName);
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0010F4E7 File Offset: 0x0010E4E7
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0010F4EE File Offset: 0x0010E4EE
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0010F4F5 File Offset: 0x0010E4F5
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0010F4FC File Offset: 0x0010E4FC
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400284B RID: 10315
		private Type m_underlyingType;

		// Token: 0x0400284C RID: 10316
		internal TypeBuilder m_typeBuilder;

		// Token: 0x0400284D RID: 10317
		private FieldBuilder m_underlyingField;

		// Token: 0x0400284E RID: 10318
		internal Type m_runtimeType;
	}
}
