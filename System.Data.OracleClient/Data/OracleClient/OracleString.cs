using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.OracleClient
{
	// Token: 0x0200007C RID: 124
	public struct OracleString : IComparable, INullable
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x000723F4 File Offset: 0x000717F4
		private OracleString(bool isNull)
		{
			this._value = (isNull ? null : string.Empty);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00072414 File Offset: 0x00071814
		public OracleString(string s)
		{
			this._value = s;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00072434 File Offset: 0x00071834
		internal OracleString(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType, OracleConnection connection, bool boundAsUCS2, bool outputParameterBinding)
		{
			this._value = OracleString.MarshalToString(buffer, valueOffset, lengthOffset, metaType, connection, boundAsUCS2, outputParameterBinding);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00072464 File Offset: 0x00071864
		public bool IsNull
		{
			get
			{
				return null == this._value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00072484 File Offset: 0x00071884
		public int Length
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return this._value.Length;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000724B4 File Offset: 0x000718B4
		public string Value
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return this._value;
			}
		}

		// Token: 0x17000141 RID: 321
		public char this[int index]
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return this._value[index];
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00072514 File Offset: 0x00071914
		public int CompareTo(object obj)
		{
			if (obj.GetType() != typeof(OracleString))
			{
				throw ADP.WrongType(obj.GetType(), typeof(OracleString));
			}
			OracleString oracleString = (OracleString)obj;
			if (this.IsNull)
			{
				if (!oracleString.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (oracleString.IsNull)
				{
					return 1;
				}
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this._value, oracleString._value);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00072594 File Offset: 0x00071994
		public override bool Equals(object value)
		{
			return value is OracleString && (this == (OracleString)value).Value;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000725C4 File Offset: 0x000719C4
		internal static int GetChars(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType, OracleConnection connection, bool boundAsUCS2, int sourceOffset, char[] destinationBuffer, int destinationOffset, int charCount)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.DangerousAddRef(ref flag);
				if (boundAsUCS2)
				{
					if (!metaType.IsLong)
					{
						Marshal.Copy(buffer.DangerousGetDataPtrWithBaseOffset(valueOffset + ADP.CharSize * sourceOffset), destinationBuffer, destinationOffset, charCount);
					}
					else
					{
						NativeBuffer_LongColumnData.CopyOutOfLineChars(buffer.ReadIntPtr(valueOffset), sourceOffset, destinationBuffer, destinationOffset, charCount);
					}
				}
				else
				{
					string text = OracleString.MarshalToString(buffer, valueOffset, lengthOffset, metaType, connection, boundAsUCS2, false);
					int length = text.Length;
					int num = (sourceOffset + charCount > length) ? (length - sourceOffset) : charCount;
					char[] src = text.ToCharArray(sourceOffset, num);
					Buffer.BlockCopy(src, 0, destinationBuffer, destinationOffset * ADP.CharSize, num * ADP.CharSize);
					charCount = num;
				}
			}
			finally
			{
				if (flag)
				{
					buffer.DangerousRelease();
				}
			}
			return charCount;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000726A4 File Offset: 0x00071AA4
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000726D4 File Offset: 0x00071AD4
		internal static int GetLength(NativeBuffer buffer, int lengthOffset, MetaType metaType)
		{
			int result;
			if (metaType.IsLong)
			{
				result = buffer.ReadInt32(lengthOffset);
			}
			else
			{
				result = (int)buffer.ReadInt16(lengthOffset);
			}
			GC.KeepAlive(buffer);
			return result;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00072704 File Offset: 0x00071B04
		internal static string MarshalToString(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType, OracleConnection connection, bool boundAsUCS2, bool outputParameterBinding)
		{
			int num = OracleString.GetLength(buffer, lengthOffset, metaType);
			if (boundAsUCS2 && outputParameterBinding)
			{
				num /= 2;
			}
			bool flag = metaType.IsLong && !outputParameterBinding;
			IntPtr zero = IntPtr.Zero;
			string result;
			if (boundAsUCS2)
			{
				if (flag)
				{
					byte[] array = new byte[num * ADP.CharSize];
					NativeBuffer_LongColumnData.CopyOutOfLineBytes(buffer.ReadIntPtr(valueOffset), 0, array, 0, num * ADP.CharSize);
					result = Encoding.Unicode.GetString(array);
				}
				else
				{
					result = buffer.PtrToStringUni(valueOffset, num);
				}
			}
			else
			{
				byte[] array2;
				if (flag)
				{
					array2 = new byte[num];
					NativeBuffer_LongColumnData.CopyOutOfLineBytes(buffer.ReadIntPtr(valueOffset), 0, array2, 0, num);
				}
				else
				{
					array2 = buffer.ReadBytes(valueOffset, num);
				}
				result = connection.GetString(array2, metaType.UsesNationalCharacterSet);
			}
			GC.KeepAlive(buffer);
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000727C4 File Offset: 0x00071BC4
		internal static int MarshalToNative(object value, int offset, int size, NativeBuffer buffer, int bufferOffset, OCI.DATATYPE ociType, bool bindAsUCS2)
		{
			Encoding encoding = bindAsUCS2 ? Encoding.Unicode : Encoding.UTF8;
			string text;
			if (value is OracleString)
			{
				text = ((OracleString)value)._value;
			}
			else
			{
				text = (string)value;
			}
			string s;
			if (offset == 0 && size == 0)
			{
				s = text;
			}
			else if (size == 0 || offset + size > text.Length)
			{
				s = text.Substring(offset);
			}
			else
			{
				s = text.Substring(offset, size);
			}
			byte[] bytes = encoding.GetBytes(s);
			int num = bytes.Length;
			int num2 = num;
			if (num != 0)
			{
				int num3 = num;
				if (bindAsUCS2)
				{
					num3 /= 2;
				}
				if (OCI.DATATYPE.LONGVARCHAR == ociType)
				{
					buffer.WriteInt32(bufferOffset, num3);
					checked
					{
						bufferOffset += 4;
					}
					num2 += 4;
				}
				buffer.WriteBytes(bufferOffset, bytes, 0, num);
			}
			return num2;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00072874 File Offset: 0x00071C74
		public override string ToString()
		{
			if (this.IsNull)
			{
				return ADP.NullString;
			}
			return this._value;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000728A4 File Offset: 0x00071CA4
		public static OracleString Concat(OracleString x, OracleString y)
		{
			return x + y;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000728C4 File Offset: 0x00071CC4
		public static OracleBoolean Equals(OracleString x, OracleString y)
		{
			return x == y;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000728E4 File Offset: 0x00071CE4
		public static OracleBoolean GreaterThan(OracleString x, OracleString y)
		{
			return x > y;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00072904 File Offset: 0x00071D04
		public static OracleBoolean GreaterThanOrEqual(OracleString x, OracleString y)
		{
			return x >= y;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00072924 File Offset: 0x00071D24
		public static OracleBoolean LessThan(OracleString x, OracleString y)
		{
			return x < y;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00072944 File Offset: 0x00071D44
		public static OracleBoolean LessThanOrEqual(OracleString x, OracleString y)
		{
			return x <= y;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00072964 File Offset: 0x00071D64
		public static OracleBoolean NotEquals(OracleString x, OracleString y)
		{
			return x != y;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00072984 File Offset: 0x00071D84
		public static implicit operator OracleString(string s)
		{
			return new OracleString(s);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000729A4 File Offset: 0x00071DA4
		public static explicit operator string(OracleString x)
		{
			return x.Value;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000729C4 File Offset: 0x00071DC4
		public static OracleString operator +(OracleString x, OracleString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return OracleString.Null;
			}
			OracleString result = new OracleString(x._value + y._value);
			return result;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00072A04 File Offset: 0x00071E04
		public static OracleBoolean operator ==(OracleString x, OracleString y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) == 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00072A44 File Offset: 0x00071E44
		public static OracleBoolean operator >(OracleString x, OracleString y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) > 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00072A84 File Offset: 0x00071E84
		public static OracleBoolean operator >=(OracleString x, OracleString y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) >= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00072AC4 File Offset: 0x00071EC4
		public static OracleBoolean operator <(OracleString x, OracleString y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) < 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00072B04 File Offset: 0x00071F04
		public static OracleBoolean operator <=(OracleString x, OracleString y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) <= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00072B44 File Offset: 0x00071F44
		public static OracleBoolean operator !=(OracleString x, OracleString y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) != 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x040004C3 RID: 1219
		private string _value;

		// Token: 0x040004C4 RID: 1220
		public static readonly OracleString Empty = new OracleString(false);

		// Token: 0x040004C5 RID: 1221
		public static readonly OracleString Null = new OracleString(true);
	}
}
