using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000837 RID: 2103
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_CustomAttributeBuilder))]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class CustomAttributeBuilder : _CustomAttributeBuilder
	{
		// Token: 0x06004B4D RID: 19277 RVA: 0x00104D0D File Offset: 0x00103D0D
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
		{
			this.InitCustomAttributeBuilder(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00104D35 File Offset: 0x00103D35
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues)
		{
			this.InitCustomAttributeBuilder(con, constructorArgs, namedProperties, propertyValues, new FieldInfo[0], new object[0]);
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x00104D54 File Offset: 0x00103D54
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.InitCustomAttributeBuilder(con, constructorArgs, new PropertyInfo[0], new object[0], namedFields, fieldValues);
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00104D73 File Offset: 0x00103D73
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.InitCustomAttributeBuilder(con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00104D8C File Offset: 0x00103D8C
		private bool ValidateType(Type t)
		{
			if (t.IsPrimitive || t == typeof(string) || t == typeof(Type))
			{
				return true;
			}
			if (t.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(t)))
				{
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return true;
				default:
					return false;
				}
			}
			else
			{
				if (t.IsArray)
				{
					return t.GetArrayRank() == 1 && this.ValidateType(t.GetElementType());
				}
				return t == typeof(object);
			}
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00104E2C File Offset: 0x00103E2C
		internal void InitCustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (constructorArgs == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (namedProperties == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (namedFields == null)
			{
				throw new ArgumentNullException("namedFields");
			}
			if (fieldValues == null)
			{
				throw new ArgumentNullException("fieldValues");
			}
			if (namedProperties.Length != propertyValues.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"), "namedProperties, propertyValues");
			}
			if (namedFields.Length != fieldValues.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"), "namedFields, fieldValues");
			}
			if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadConstructor"));
			}
			if ((con.CallingConvention & CallingConventions.Standard) != CallingConventions.Standard)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadConstructorCallConv"));
			}
			this.m_con = con;
			this.m_constructorArgs = new object[constructorArgs.Length];
			Array.Copy(constructorArgs, this.m_constructorArgs, constructorArgs.Length);
			Type[] array;
			if (con is ConstructorBuilder)
			{
				array = ((ConstructorBuilder)con).GetParameterTypes();
			}
			else
			{
				ParameterInfo[] parametersNoCopy = con.GetParametersNoCopy();
				array = new Type[parametersNoCopy.Length];
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					array[i] = parametersNoCopy[i].ParameterType;
				}
			}
			if (array.Length != constructorArgs.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadParameterCountsForConstructor"));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!this.ValidateType(array[i]))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (constructorArgs[i] != null)
				{
					TypeCode typeCode = Type.GetTypeCode(array[i]);
					if (typeCode != Type.GetTypeCode(constructorArgs[i].GetType()) && (typeCode != TypeCode.Object || !this.ValidateType(constructorArgs[i].GetType())))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadParameterTypeForConstructor"), new object[]
						{
							i
						}));
					}
				}
			}
			MemoryStream output = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write(1);
			for (int i = 0; i < constructorArgs.Length; i++)
			{
				this.EmitValue(binaryWriter, array[i], constructorArgs[i]);
			}
			binaryWriter.Write((ushort)(namedProperties.Length + namedFields.Length));
			for (int i = 0; i < namedProperties.Length; i++)
			{
				if (namedProperties[i] == null)
				{
					throw new ArgumentNullException("namedProperties[" + i + "]");
				}
				Type propertyType = namedProperties[i].PropertyType;
				if (propertyValues[i] == null && propertyType.IsPrimitive)
				{
					throw new ArgumentNullException("propertyValues[" + i + "]");
				}
				if (!this.ValidateType(propertyType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
				}
				if (!namedProperties[i].CanWrite)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotAWritableProperty"));
				}
				if (namedProperties[i].DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && !con.DeclaringType.IsSubclassOf(namedProperties[i].DeclaringType) && !TypeBuilder.IsTypeEqual(namedProperties[i].DeclaringType, con.DeclaringType) && (!(namedProperties[i].DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf(((TypeBuilder)namedProperties[i].DeclaringType).m_runtimeType)))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadPropertyForConstructorBuilder"));
				}
				if (propertyValues[i] != null && propertyType != typeof(object) && Type.GetTypeCode(propertyValues[i].GetType()) != Type.GetTypeCode(propertyType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
				}
				binaryWriter.Write(84);
				this.EmitType(binaryWriter, propertyType);
				this.EmitString(binaryWriter, namedProperties[i].Name);
				this.EmitValue(binaryWriter, propertyType, propertyValues[i]);
			}
			for (int i = 0; i < namedFields.Length; i++)
			{
				if (namedFields[i] == null)
				{
					throw new ArgumentNullException("namedFields[" + i + "]");
				}
				Type fieldType = namedFields[i].FieldType;
				if (fieldValues[i] == null && fieldType.IsPrimitive)
				{
					throw new ArgumentNullException("fieldValues[" + i + "]");
				}
				if (!this.ValidateType(fieldType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeInCustomAttribute"));
				}
				if (namedFields[i].DeclaringType != con.DeclaringType && !(con.DeclaringType is TypeBuilderInstantiation) && !con.DeclaringType.IsSubclassOf(namedFields[i].DeclaringType) && !TypeBuilder.IsTypeEqual(namedFields[i].DeclaringType, con.DeclaringType) && (!(namedFields[i].DeclaringType is TypeBuilder) || !con.DeclaringType.IsSubclassOf(((TypeBuilder)namedFields[i].DeclaringType).m_runtimeType)))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldForConstructorBuilder"));
				}
				if (fieldValues[i] != null && fieldType != typeof(object) && Type.GetTypeCode(fieldValues[i].GetType()) != Type.GetTypeCode(fieldType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
				}
				binaryWriter.Write(83);
				this.EmitType(binaryWriter, fieldType);
				this.EmitString(binaryWriter, namedFields[i].Name);
				this.EmitValue(binaryWriter, fieldType, fieldValues[i]);
			}
			this.m_blob = ((MemoryStream)binaryWriter.BaseStream).ToArray();
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00105388 File Offset: 0x00104388
		private void EmitType(BinaryWriter writer, Type type)
		{
			if (type.IsPrimitive)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					writer.Write(2);
					return;
				case TypeCode.Char:
					writer.Write(3);
					return;
				case TypeCode.SByte:
					writer.Write(4);
					return;
				case TypeCode.Byte:
					writer.Write(5);
					return;
				case TypeCode.Int16:
					writer.Write(6);
					return;
				case TypeCode.UInt16:
					writer.Write(7);
					return;
				case TypeCode.Int32:
					writer.Write(8);
					return;
				case TypeCode.UInt32:
					writer.Write(9);
					return;
				case TypeCode.Int64:
					writer.Write(10);
					return;
				case TypeCode.UInt64:
					writer.Write(11);
					return;
				case TypeCode.Single:
					writer.Write(12);
					return;
				case TypeCode.Double:
					writer.Write(13);
					return;
				default:
					return;
				}
			}
			else
			{
				if (type.IsEnum)
				{
					writer.Write(85);
					this.EmitString(writer, type.AssemblyQualifiedName);
					return;
				}
				if (type == typeof(string))
				{
					writer.Write(14);
					return;
				}
				if (type == typeof(Type))
				{
					writer.Write(80);
					return;
				}
				if (type.IsArray)
				{
					writer.Write(29);
					this.EmitType(writer, type.GetElementType());
					return;
				}
				writer.Write(81);
				return;
			}
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x001054B8 File Offset: 0x001044B8
		private void EmitString(BinaryWriter writer, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			uint num = (uint)bytes.Length;
			if (num <= 127U)
			{
				writer.Write((byte)num);
			}
			else if (num <= 16383U)
			{
				writer.Write((byte)(num >> 8 | 128U));
				writer.Write((byte)(num & 255U));
			}
			else
			{
				writer.Write((byte)(num >> 24 | 192U));
				writer.Write((byte)(num >> 16 & 255U));
				writer.Write((byte)(num >> 8 & 255U));
				writer.Write((byte)(num & 255U));
			}
			writer.Write(bytes);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00105554 File Offset: 0x00104554
		private void EmitValue(BinaryWriter writer, Type type, object value)
		{
			if (type.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(type)))
				{
				case TypeCode.SByte:
					writer.Write((sbyte)value);
					return;
				case TypeCode.Byte:
					writer.Write((byte)value);
					return;
				case TypeCode.Int16:
					writer.Write((short)value);
					return;
				case TypeCode.UInt16:
					writer.Write((ushort)value);
					return;
				case TypeCode.Int32:
					writer.Write((int)value);
					return;
				case TypeCode.UInt32:
					writer.Write((uint)value);
					return;
				case TypeCode.Int64:
					writer.Write((long)value);
					return;
				case TypeCode.UInt64:
					writer.Write((ulong)value);
					return;
				default:
					return;
				}
			}
			else if (type == typeof(string))
			{
				if (value == null)
				{
					writer.Write(byte.MaxValue);
					return;
				}
				this.EmitString(writer, (string)value);
				return;
			}
			else if (type == typeof(Type))
			{
				if (value == null)
				{
					writer.Write(byte.MaxValue);
					return;
				}
				string text = TypeNameBuilder.ToString((Type)value, TypeNameBuilder.Format.AssemblyQualifiedName);
				if (text == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidTypeForCA"), new object[]
					{
						value.GetType()
					}));
				}
				this.EmitString(writer, text);
				return;
			}
			else if (type.IsArray)
			{
				if (value == null)
				{
					writer.Write(uint.MaxValue);
					return;
				}
				Array array = (Array)value;
				Type elementType = type.GetElementType();
				writer.Write(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					this.EmitValue(writer, elementType, array.GetValue(i));
				}
				return;
			}
			else if (type.IsPrimitive)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
					writer.Write(((bool)value) ? 1 : 0);
					return;
				case TypeCode.Char:
					writer.Write(Convert.ToInt16((char)value));
					return;
				case TypeCode.SByte:
					writer.Write((sbyte)value);
					return;
				case TypeCode.Byte:
					writer.Write((byte)value);
					return;
				case TypeCode.Int16:
					writer.Write((short)value);
					return;
				case TypeCode.UInt16:
					writer.Write((ushort)value);
					return;
				case TypeCode.Int32:
					writer.Write((int)value);
					return;
				case TypeCode.UInt32:
					writer.Write((uint)value);
					return;
				case TypeCode.Int64:
					writer.Write((long)value);
					return;
				case TypeCode.UInt64:
					writer.Write((ulong)value);
					return;
				case TypeCode.Single:
					writer.Write((float)value);
					return;
				case TypeCode.Double:
					writer.Write((double)value);
					return;
				default:
					return;
				}
			}
			else
			{
				if (type == typeof(object))
				{
					Type type2 = (value == null) ? typeof(string) : ((value is Type) ? typeof(Type) : value.GetType());
					this.EmitType(writer, type2);
					this.EmitValue(writer, type2, value);
					return;
				}
				string text2 = "null";
				if (value != null)
				{
					text2 = value.GetType().ToString();
				}
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadParameterTypeForCAB"), new object[]
				{
					text2
				}));
			}
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00105868 File Offset: 0x00104868
		internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner)
		{
			this.CreateCustomAttribute(mod, tkOwner, mod.GetConstructorToken(this.m_con).Token, false);
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x00105894 File Offset: 0x00104894
		internal int PrepareCreateCustomAttributeToDisk(ModuleBuilder mod)
		{
			return mod.InternalGetConstructorToken(this.m_con, true).Token;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x001058B6 File Offset: 0x001048B6
		internal void CreateCustomAttribute(ModuleBuilder mod, int tkOwner, int tkAttrib, bool toDisk)
		{
			TypeBuilder.InternalCreateCustomAttribute(tkOwner, tkAttrib, this.m_blob, mod, toDisk, typeof(DebuggableAttribute) == this.m_con.DeclaringType);
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x001058DF File Offset: 0x001048DF
		void _CustomAttributeBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x001058E6 File Offset: 0x001048E6
		void _CustomAttributeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x001058ED File Offset: 0x001048ED
		void _CustomAttributeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x001058F4 File Offset: 0x001048F4
		void _CustomAttributeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400266D RID: 9837
		private const byte SERIALIZATION_TYPE_BOOLEAN = 2;

		// Token: 0x0400266E RID: 9838
		private const byte SERIALIZATION_TYPE_CHAR = 3;

		// Token: 0x0400266F RID: 9839
		private const byte SERIALIZATION_TYPE_I1 = 4;

		// Token: 0x04002670 RID: 9840
		private const byte SERIALIZATION_TYPE_U1 = 5;

		// Token: 0x04002671 RID: 9841
		private const byte SERIALIZATION_TYPE_I2 = 6;

		// Token: 0x04002672 RID: 9842
		private const byte SERIALIZATION_TYPE_U2 = 7;

		// Token: 0x04002673 RID: 9843
		private const byte SERIALIZATION_TYPE_I4 = 8;

		// Token: 0x04002674 RID: 9844
		private const byte SERIALIZATION_TYPE_U4 = 9;

		// Token: 0x04002675 RID: 9845
		private const byte SERIALIZATION_TYPE_I8 = 10;

		// Token: 0x04002676 RID: 9846
		private const byte SERIALIZATION_TYPE_U8 = 11;

		// Token: 0x04002677 RID: 9847
		private const byte SERIALIZATION_TYPE_R4 = 12;

		// Token: 0x04002678 RID: 9848
		private const byte SERIALIZATION_TYPE_R8 = 13;

		// Token: 0x04002679 RID: 9849
		private const byte SERIALIZATION_TYPE_STRING = 14;

		// Token: 0x0400267A RID: 9850
		private const byte SERIALIZATION_TYPE_SZARRAY = 29;

		// Token: 0x0400267B RID: 9851
		private const byte SERIALIZATION_TYPE_TYPE = 80;

		// Token: 0x0400267C RID: 9852
		private const byte SERIALIZATION_TYPE_TAGGED_OBJECT = 81;

		// Token: 0x0400267D RID: 9853
		private const byte SERIALIZATION_TYPE_FIELD = 83;

		// Token: 0x0400267E RID: 9854
		private const byte SERIALIZATION_TYPE_PROPERTY = 84;

		// Token: 0x0400267F RID: 9855
		private const byte SERIALIZATION_TYPE_ENUM = 85;

		// Token: 0x04002680 RID: 9856
		internal ConstructorInfo m_con;

		// Token: 0x04002681 RID: 9857
		internal object[] m_constructorArgs;

		// Token: 0x04002682 RID: 9858
		internal byte[] m_blob;
	}
}
