using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000825 RID: 2085
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x00101EB8 File Offset: 0x00100EB8
		internal FieldBuilder(TypeBuilder typeBuilder, string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			if (fieldName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "fieldName");
			}
			if (fieldName[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "fieldName");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(void))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldType"));
			}
			this.m_fieldName = fieldName;
			this.m_typeBuilder = typeBuilder;
			this.m_fieldType = type;
			this.m_Attributes = (attributes & ~FieldAttributes.ReservedMask);
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this.m_typeBuilder.Module);
			fieldSigHelper.AddArgument(type, requiredCustomModifiers, optionalCustomModifiers);
			int sigLength;
			byte[] signature = fieldSigHelper.InternalGetSignature(out sigLength);
			this.m_fieldTok = TypeBuilder.InternalDefineField(typeBuilder.TypeToken.Token, fieldName, signature, sigLength, this.m_Attributes, this.m_typeBuilder.Module);
			this.m_tkField = new FieldToken(this.m_fieldTok, type);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x00101FC6 File Offset: 0x00100FC6
		internal void SetData(byte[] data, int size)
		{
			if (data != null)
			{
				this.m_data = new byte[data.Length];
				Array.Copy(data, this.m_data, data.Length);
			}
			this.m_typeBuilder.Module.InternalSetFieldRVAContent(this.m_tkField.Token, data, size);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00102005 File Offset: 0x00101005
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_typeBuilder;
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x0010200D File Offset: 0x0010100D
		internal override int MetadataTokenInternal
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004A38 RID: 19000 RVA: 0x00102015 File Offset: 0x00101015
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00102022 File Offset: 0x00101022
		public override string Name
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x0010202A File Offset: 0x0010102A
		public override Type DeclaringType
		{
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x00102041 File Offset: 0x00101041
		public override Type ReflectedType
		{
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x00102058 File Offset: 0x00101058
		public override Type FieldType
		{
			get
			{
				return this.m_fieldType;
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00102060 File Offset: 0x00101060
		public override object GetValue(object obj)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x00102071 File Offset: 0x00101071
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004A3F RID: 19007 RVA: 0x00102082 File Offset: 0x00101082
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x00102093 File Offset: 0x00101093
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_Attributes;
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0010209B File Offset: 0x0010109B
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x001020AC File Offset: 0x001010AC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x001020BD File Offset: 0x001010BD
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x001020CE File Offset: 0x001010CE
		public FieldToken GetToken()
		{
			return this.m_tkField;
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x001020D8 File Offset: 0x001010D8
		public void SetOffset(int iOffset)
		{
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.InternalSetFieldOffset(this.m_typeBuilder.Module, this.GetToken().Token, iOffset);
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00102110 File Offset: 0x00101110
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.m_typeBuilder.ThrowIfCreated();
			if (unmanagedMarshal == null)
			{
				throw new ArgumentNullException("unmanagedMarshal");
			}
			byte[] array = unmanagedMarshal.InternalGetBytes();
			TypeBuilder.InternalSetMarshalInfo(this.m_typeBuilder.Module, this.GetToken().Token, array, array.Length);
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00102160 File Offset: 0x00101160
		public void SetConstant(object defaultValue)
		{
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_typeBuilder.Module, this.GetToken().Token, this.m_fieldType, defaultValue);
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x001021A0 File Offset: 0x001011A0
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			ModuleBuilder moduleBuilder = this.m_typeBuilder.Module as ModuleBuilder;
			this.m_typeBuilder.ThrowIfCreated();
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.InternalCreateCustomAttribute(this.m_tkField.Token, moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, moduleBuilder, false);
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00102208 File Offset: 0x00101208
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_typeBuilder.ThrowIfCreated();
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			ModuleBuilder mod = this.m_typeBuilder.Module as ModuleBuilder;
			customBuilder.CreateCustomAttribute(mod, this.m_tkField.Token);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x00102251 File Offset: 0x00101251
		void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00102258 File Offset: 0x00101258
		void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x0010225F File Offset: 0x0010125F
		void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00102266 File Offset: 0x00101266
		void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040025E7 RID: 9703
		private int m_fieldTok;

		// Token: 0x040025E8 RID: 9704
		private FieldToken m_tkField;

		// Token: 0x040025E9 RID: 9705
		private TypeBuilder m_typeBuilder;

		// Token: 0x040025EA RID: 9706
		private string m_fieldName;

		// Token: 0x040025EB RID: 9707
		private FieldAttributes m_Attributes;

		// Token: 0x040025EC RID: 9708
		private Type m_fieldType;

		// Token: 0x040025ED RID: 9709
		internal byte[] m_data;
	}
}
