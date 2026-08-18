using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000006 RID: 6
	internal static class BlobWriterImpl
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002C74 File Offset: 0x00000E74
		internal static int GetCompressedIntegerSize(int value)
		{
			if (value <= 127)
			{
				return 1;
			}
			if (value <= 16383)
			{
				return 2;
			}
			return 4;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002C68 File Offset: 0x00000E68
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowValueArgumentOutOfRange()
		{
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002C88 File Offset: 0x00000E88
		internal static void WriteCompressedInteger(ref BlobWriter writer, int value)
		{
			if (value <= 127)
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (value <= 16383)
			{
				writer.WriteUInt16BE((ushort)(32768 | value));
				return;
			}
			if (value <= 536870911)
			{
				writer.WriteUInt32BE((uint)(-1073741824 | value));
				return;
			}
			BlobWriterImpl.ThrowValueArgumentOutOfRange();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002CD8 File Offset: 0x00000ED8
		internal static void WriteCompressedInteger(BlobBuilder writer, int value)
		{
			if (value <= 127)
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (value <= 16383)
			{
				writer.WriteUInt16BE((ushort)(32768 | value));
				return;
			}
			if (value <= 536870911)
			{
				writer.WriteUInt32BE((uint)(-1073741824 | value));
				return;
			}
			BlobWriterImpl.ThrowValueArgumentOutOfRange();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002D28 File Offset: 0x00000F28
		internal static void WriteCompressedSignedInteger(ref BlobWriter writer, int value)
		{
			int num = value >> 31;
			if ((value & -64) == (num & -64))
			{
				int num2 = (value & 63) << 1 | (num & 1);
				writer.WriteByte((byte)num2);
				return;
			}
			if ((value & -8192) == (num & -8192))
			{
				int num3 = (value & 8191) << 1 | (num & 1);
				writer.WriteUInt16BE((ushort)(32768 | num3));
				return;
			}
			if ((value & -268435456) == (num & -268435456))
			{
				int num4 = (value & 268435455) << 1 | (num & 1);
				writer.WriteUInt32BE((uint)(-1073741824 | num4));
				return;
			}
			BlobWriterImpl.ThrowValueArgumentOutOfRange();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002DB8 File Offset: 0x00000FB8
		internal static void WriteCompressedSignedInteger(BlobBuilder writer, int value)
		{
			int num = value >> 31;
			if ((value & -64) == (num & -64))
			{
				int num2 = (value & 63) << 1 | (num & 1);
				writer.WriteByte((byte)num2);
				return;
			}
			if ((value & -8192) == (num & -8192))
			{
				int num3 = (value & 8191) << 1 | (num & 1);
				writer.WriteUInt16BE((ushort)(32768 | num3));
				return;
			}
			if ((value & -268435456) == (num & -268435456))
			{
				int num4 = (value & 268435455) << 1 | (num & 1);
				writer.WriteUInt32BE((uint)(-1073741824 | num4));
				return;
			}
			BlobWriterImpl.ThrowValueArgumentOutOfRange();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002E48 File Offset: 0x00001048
		internal static void WriteConstant(ref BlobWriter writer, object value)
		{
			if (value == null)
			{
				writer.WriteUInt32(0U);
				return;
			}
			Type type = value.GetType();
			if (type.GetTypeInfo().IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(bool))
			{
				writer.WriteBoolean((bool)value);
				return;
			}
			if (type == typeof(int))
			{
				writer.WriteInt32((int)value);
				return;
			}
			if (type == typeof(string))
			{
				writer.WriteUTF16((string)value);
				return;
			}
			if (type == typeof(byte))
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (type == typeof(char))
			{
				writer.WriteUInt16((ushort)((char)value));
				return;
			}
			if (type == typeof(double))
			{
				writer.WriteDouble((double)value);
				return;
			}
			if (type == typeof(short))
			{
				writer.WriteInt16((short)value);
				return;
			}
			if (type == typeof(long))
			{
				writer.WriteInt64((long)value);
				return;
			}
			if (type == typeof(sbyte))
			{
				writer.WriteSByte((sbyte)value);
				return;
			}
			if (type == typeof(float))
			{
				writer.WriteSingle((float)value);
				return;
			}
			if (type == typeof(ushort))
			{
				writer.WriteUInt16((ushort)value);
				return;
			}
			if (type == typeof(uint))
			{
				writer.WriteUInt32((uint)value);
				return;
			}
			if (type == typeof(ulong))
			{
				writer.WriteUInt64((ulong)value);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002FD4 File Offset: 0x000011D4
		internal static void WriteConstant(BlobBuilder writer, object value)
		{
			if (value == null)
			{
				writer.WriteUInt32(0U);
				return;
			}
			Type type = value.GetType();
			if (type.GetTypeInfo().IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(bool))
			{
				writer.WriteBoolean((bool)value);
				return;
			}
			if (type == typeof(int))
			{
				writer.WriteInt32((int)value);
				return;
			}
			if (type == typeof(string))
			{
				writer.WriteUTF16((string)value);
				return;
			}
			if (type == typeof(byte))
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (type == typeof(char))
			{
				writer.WriteUInt16((ushort)((char)value));
				return;
			}
			if (type == typeof(double))
			{
				writer.WriteDouble((double)value);
				return;
			}
			if (type == typeof(short))
			{
				writer.WriteInt16((short)value);
				return;
			}
			if (type == typeof(long))
			{
				writer.WriteInt64((long)value);
				return;
			}
			if (type == typeof(sbyte))
			{
				writer.WriteSByte((sbyte)value);
				return;
			}
			if (type == typeof(float))
			{
				writer.WriteSingle((float)value);
				return;
			}
			if (type == typeof(ushort))
			{
				writer.WriteUInt16((ushort)value);
				return;
			}
			if (type == typeof(uint))
			{
				writer.WriteUInt32((uint)value);
				return;
			}
			if (type == typeof(ulong))
			{
				writer.WriteUInt64((ulong)value);
				return;
			}
			throw new ArgumentException();
		}

		// Token: 0x0400000A RID: 10
		internal const int SingleByteCompressedIntegerMaxValue = 127;

		// Token: 0x0400000B RID: 11
		internal const int TwoByteCompressedIntegerMaxValue = 16383;

		// Token: 0x0400000C RID: 12
		internal const int MaxCompressedIntegerValue = 536870911;
	}
}
