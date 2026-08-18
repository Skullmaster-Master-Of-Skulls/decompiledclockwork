using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000BA RID: 186
	internal static class MetadataWriterUtilities
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x0001560C File Offset: 0x0001380C
		public static SignatureTypeCode GetConstantTypeCode(object value)
		{
			if (value == null)
			{
				return (SignatureTypeCode)18;
			}
			if (value.GetType() == typeof(int))
			{
				return SignatureTypeCode.Int32;
			}
			if (value.GetType() == typeof(string))
			{
				return SignatureTypeCode.String;
			}
			if (value.GetType() == typeof(bool))
			{
				return SignatureTypeCode.Boolean;
			}
			if (value.GetType() == typeof(char))
			{
				return SignatureTypeCode.Char;
			}
			if (value.GetType() == typeof(byte))
			{
				return SignatureTypeCode.Byte;
			}
			if (value.GetType() == typeof(long))
			{
				return SignatureTypeCode.Int64;
			}
			if (value.GetType() == typeof(double))
			{
				return SignatureTypeCode.Double;
			}
			if (value.GetType() == typeof(short))
			{
				return SignatureTypeCode.Int16;
			}
			if (value.GetType() == typeof(ushort))
			{
				return SignatureTypeCode.UInt16;
			}
			if (value.GetType() == typeof(uint))
			{
				return SignatureTypeCode.UInt32;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return SignatureTypeCode.SByte;
			}
			if (value.GetType() == typeof(ulong))
			{
				return SignatureTypeCode.UInt64;
			}
			if (value.GetType() == typeof(float))
			{
				return SignatureTypeCode.Single;
			}
			throw new ArgumentException("Invalid constant type", "value");
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00015738 File Offset: 0x00013938
		internal static void SerializeRowCounts(BlobBuilder writer, ImmutableArray<int> rowCounts)
		{
			for (int i = 0; i < rowCounts.Length; i++)
			{
				int num = rowCounts[i];
				if (num > 0)
				{
					writer.WriteInt32(num);
				}
			}
		}
	}
}
