using System;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000298 RID: 664
	internal class SerializationHelperSql9
	{
		// Token: 0x0600225E RID: 8798 RVA: 0x0028BCB8 File Offset: 0x0028B0B8
		private SerializationHelperSql9()
		{
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0028BCD8 File Offset: 0x0028B0D8
		internal static int SizeInBytes(Type t)
		{
			return SerializationHelperSql9.SizeInBytes(Activator.CreateInstance(t));
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0028BCF8 File Offset: 0x0028B0F8
		internal static int SizeInBytes(object instance)
		{
			Type type = instance.GetType();
			SerializationHelperSql9.GetFormat(type);
			DummyStream dummyStream = new DummyStream();
			Serializer serializer = SerializationHelperSql9.GetSerializer(instance.GetType());
			serializer.Serialize(dummyStream, instance);
			return (int)dummyStream.Length;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0028BD38 File Offset: 0x0028B138
		internal static void Serialize(Stream s, object instance)
		{
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(s, instance);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0028BD58 File Offset: 0x0028B158
		internal static object Deserialize(Stream s, Type resultType)
		{
			return SerializationHelperSql9.GetSerializer(resultType).Deserialize(s);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0028BD78 File Offset: 0x0028B178
		private static Format GetFormat(Type t)
		{
			return SerializationHelperSql9.GetUdtAttribute(t).Format;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0028BD98 File Offset: 0x0028B198
		private static Serializer GetSerializer(Type t)
		{
			if (SerializationHelperSql9.m_types2Serializers == null)
			{
				SerializationHelperSql9.m_types2Serializers = new Hashtable();
			}
			Serializer serializer = (Serializer)SerializationHelperSql9.m_types2Serializers[t];
			if (serializer == null)
			{
				serializer = SerializationHelperSql9.GetNewSerializer(t);
				SerializationHelperSql9.m_types2Serializers[t] = serializer;
			}
			return serializer;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0028BDE8 File Offset: 0x0028B1E8
		internal static int GetUdtMaxLength(Type t)
		{
			SqlUdtInfo fromType = SqlUdtInfo.GetFromType(t);
			if (Format.Native == fromType.SerializationFormat)
			{
				return SerializationHelperSql9.SizeInBytes(t);
			}
			return fromType.MaxByteSize;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0028BE18 File Offset: 0x0028B218
		private static object[] GetCustomAttributes(Type t)
		{
			return t.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0028BE38 File Offset: 0x0028B238
		internal static SqlUserDefinedTypeAttribute GetUdtAttribute(Type t)
		{
			object[] customAttributes = SerializationHelperSql9.GetCustomAttributes(t);
			if (customAttributes != null && customAttributes.Length == 1)
			{
				return (SqlUserDefinedTypeAttribute)customAttributes[0];
			}
			throw InvalidUdtException.Create(t, "SqlUdtReason_NoUdtAttribute");
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x0028BE78 File Offset: 0x0028B278
		private static Serializer GetNewSerializer(Type t)
		{
			SerializationHelperSql9.GetUdtAttribute(t);
			Format format = SerializationHelperSql9.GetFormat(t);
			switch (format)
			{
			case Format.Native:
				return new NormalizedSerializer(t);
			case Format.UserDefined:
				return new BinarySerializeSerializer(t);
			}
			throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
		}

		// Token: 0x04001664 RID: 5732
		[ThreadStatic]
		private static Hashtable m_types2Serializers;
	}
}
