using System;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200006C RID: 108
	internal class SerializationHelperSql9
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x00047418 File Offset: 0x00046818
		private SerializationHelperSql9()
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0004742C File Offset: 0x0004682C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int SizeInBytes(Type t)
		{
			return SerializationHelperSql9.SizeInBytes(Activator.CreateInstance(t));
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00047444 File Offset: 0x00046844
		internal static int SizeInBytes(object instance)
		{
			Type type = instance.GetType();
			Format format = SerializationHelperSql9.GetFormat(type);
			DummyStream dummyStream = new DummyStream();
			Serializer serializer = SerializationHelperSql9.GetSerializer(instance.GetType());
			serializer.Serialize(dummyStream, instance);
			return (int)dummyStream.Length;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00047480 File Offset: 0x00046880
		internal static void Serialize(Stream s, object instance)
		{
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(s, instance);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000474A0 File Offset: 0x000468A0
		internal static object Deserialize(Stream s, Type resultType)
		{
			return SerializationHelperSql9.GetSerializer(resultType).Deserialize(s);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000474BC File Offset: 0x000468BC
		private static Format GetFormat(Type t)
		{
			return SerializationHelperSql9.GetUdtAttribute(t).Format;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000474D4 File Offset: 0x000468D4
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

		// Token: 0x06000532 RID: 1330 RVA: 0x0004751C File Offset: 0x0004691C
		internal static int GetUdtMaxLength(Type t)
		{
			SqlUdtInfo fromType = SqlUdtInfo.GetFromType(t);
			if (Format.Native == fromType.SerializationFormat)
			{
				return SerializationHelperSql9.SizeInBytes(t);
			}
			return fromType.MaxByteSize;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00047548 File Offset: 0x00046948
		private static object[] GetCustomAttributes(Type t)
		{
			return t.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00047568 File Offset: 0x00046968
		internal static SqlUserDefinedTypeAttribute GetUdtAttribute(Type t)
		{
			object[] customAttributes = SerializationHelperSql9.GetCustomAttributes(t);
			if (customAttributes != null && customAttributes.Length == 1)
			{
				return (SqlUserDefinedTypeAttribute)customAttributes[0];
			}
			throw InvalidUdtException.Create(t, "SqlUdtReason_NoUdtAttribute");
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000475A0 File Offset: 0x000469A0
		private static Serializer GetNewSerializer(Type t)
		{
			SqlUserDefinedTypeAttribute udtAttribute = SerializationHelperSql9.GetUdtAttribute(t);
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

		// Token: 0x040001E8 RID: 488
		[ThreadStatic]
		private static Hashtable m_types2Serializers;
	}
}
