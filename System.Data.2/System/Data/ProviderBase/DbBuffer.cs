using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.ProviderBase
{
	// Token: 0x020002B7 RID: 695
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06002A05 RID: 10757 RVA: 0x00115F50 File Offset: 0x00115350
		private DbBuffer(int initialSize, bool zeroBuffer) : base(IntPtr.Zero, true)
		{
			if (0 < initialSize)
			{
				int flags = zeroBuffer ? 64 : 0;
				this._bufferLength = initialSize;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this.handle = SafeNativeMethods.LocalAlloc(flags, (IntPtr)initialSize);
				}
				if (IntPtr.Zero == this.handle)
				{
					throw new OutOfMemoryException();
				}
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00115FCC File Offset: 0x001153CC
		protected DbBuffer(int initialSize) : this(initialSize, true)
		{
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00115FE4 File Offset: 0x001153E4
		protected DbBuffer(IntPtr invalidHandleValue, bool ownsHandle) : base(invalidHandleValue, ownsHandle)
		{
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x00115FFC File Offset: 0x001153FC
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x0011600C File Offset: 0x0011540C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0011602C File Offset: 0x0011542C
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00116040 File Offset: 0x00115440
		internal string PtrToStringUni(int offset)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2);
			string result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				int num = UnsafeNativeMethods.lstrlenW(ptr);
				this.Validate(offset, 2 * (num + 1));
				result = Marshal.PtrToStringUni(ptr, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x001160C4 File Offset: 0x001154C4
		internal string PtrToStringUni(int offset, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			string result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				result = Marshal.PtrToStringUni(ptr, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00116134 File Offset: 0x00115534
		internal byte ReadByte(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				result = Marshal.ReadByte(ptr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00116194 File Offset: 0x00115594
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] destination = new byte[length];
			return this.ReadBytes(offset, destination, 0, length);
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x001161B4 File Offset: 0x001155B4
		internal byte[] ReadBytes(int offset, byte[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr source = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00116224 File Offset: 0x00115624
		internal char ReadChar(int offset)
		{
			short num = this.ReadInt16(offset);
			return (char)num;
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x0011623C File Offset: 0x0011563C
		internal char[] ReadChars(int offset, char[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr source = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return destination;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x001162B0 File Offset: 0x001156B0
		internal double ReadDouble(int offset)
		{
			long value = this.ReadInt64(offset);
			return BitConverter.Int64BitsToDouble(value);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x001162CC File Offset: 0x001156CC
		internal short ReadInt16(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			short result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				result = Marshal.ReadInt16(ptr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x0011632C File Offset: 0x0011572C
		internal void ReadInt16Array(int offset, short[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr source = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x0011639C File Offset: 0x0011579C
		internal int ReadInt32(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				result = Marshal.ReadInt32(ptr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x001163FC File Offset: 0x001157FC
		internal void ReadInt32Array(int offset, int[] destination, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr source = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, destination, startIndex, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0011646C File Offset: 0x0011586C
		internal long ReadInt64(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			long result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				result = Marshal.ReadInt64(ptr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x001164CC File Offset: 0x001158CC
		internal IntPtr ReadIntPtr(int offset)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			IntPtr result;
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				result = Marshal.ReadIntPtr(ptr, offset);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0011652C File Offset: 0x0011592C
		internal unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00116548 File Offset: 0x00115948
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				SafeNativeMethods.LocalFree(handle);
			}
			return true;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0011657C File Offset: 0x0011597C
		private void StructureToPtr(int offset, object structure)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.StructureToPtr(structure, ptr, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x001165E0 File Offset: 0x001159E0
		internal void WriteByte(int offset, byte value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.WriteByte(ptr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00116640 File Offset: 0x00115A40
		internal void WriteBytes(int offset, byte[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x001166B0 File Offset: 0x00115AB0
		internal void WriteCharArray(int offset, char[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00116720 File Offset: 0x00115B20
		internal void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x0011673C File Offset: 0x00115B3C
		internal void WriteInt16(int offset, short value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.WriteInt16(ptr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x0011679C File Offset: 0x00115B9C
		internal void WriteInt16Array(int offset, short[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 2 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x0011680C File Offset: 0x00115C0C
		internal void WriteInt32(int offset, int value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.WriteInt32(ptr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x0011686C File Offset: 0x00115C6C
		internal void WriteInt32Array(int offset, int[] source, int startIndex, int length)
		{
			offset += this.BaseOffset;
			this.Validate(offset, 4 * length);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr destination = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				Marshal.Copy(source, startIndex, destination, length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x001168DC File Offset: 0x00115CDC
		internal void WriteInt64(int offset, long value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.WriteInt64(ptr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0011693C File Offset: 0x00115D3C
		internal void WriteIntPtr(int offset, IntPtr value)
		{
			offset += this.BaseOffset;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.WriteIntPtr(ptr, offset, value);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0011699C File Offset: 0x00115D9C
		internal unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x001169B4 File Offset: 0x00115DB4
		internal void ZeroMemory()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr dest = base.DangerousGetHandle();
				SafeNativeMethods.ZeroMemory(dest, (IntPtr)this.Length);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00116A10 File Offset: 0x00115E10
		internal Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00116A38 File Offset: 0x00115E38
		internal void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x00116A54 File Offset: 0x00115E54
		internal DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00116A84 File Offset: 0x00115E84
		internal void WriteDate(int offset, DateTime value)
		{
			short[] source = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day
			};
			this.WriteInt16Array(offset, source, 0, 3);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00116AC4 File Offset: 0x00115EC4
		internal TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x00116AF4 File Offset: 0x00115EF4
		internal void WriteTime(int offset, TimeSpan value)
		{
			short[] source = new short[]
			{
				(short)value.Hours,
				(short)value.Minutes,
				(short)value.Seconds
			};
			this.WriteInt16Array(offset, source, 0, 3);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x00116B34 File Offset: 0x00115F34
		internal DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00116B88 File Offset: 0x00115F88
		internal void WriteDateTime(int offset, DateTime value)
		{
			int value2 = (int)(value.Ticks % 10000000L) * 100;
			short[] source = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			this.WriteInt16Array(offset, source, 0, 6);
			this.WriteInt32(offset + 12, value2);
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00116C08 File Offset: 0x00116008
		internal decimal ReadNumeric(int offset)
		{
			byte[] array = new byte[20];
			this.ReadBytes(offset, array, 1, 19);
			int[] array2 = new int[]
			{
				0,
				0,
				0,
				(int)array[2] << 16
			};
			if (array[3] == 0)
			{
				array2[3] |= int.MinValue;
			}
			array2[0] = BitConverter.ToInt32(array, 4);
			array2[1] = BitConverter.ToInt32(array, 8);
			array2[2] = BitConverter.ToInt32(array, 12);
			if (BitConverter.ToInt32(array, 16) != 0)
			{
				throw ADP.NumericToDecimalOverflow();
			}
			return new decimal(array2);
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00116C84 File Offset: 0x00116084
		internal void WriteNumeric(int offset, decimal value, byte precision)
		{
			int[] bits = decimal.GetBits(value);
			byte[] array = new byte[20];
			array[1] = precision;
			Buffer.BlockCopy(bits, 14, array, 2, 2);
			array[3] = ((array[3] == 0) ? 1 : 0);
			Buffer.BlockCopy(bits, 0, array, 4, 12);
			array[16] = 0;
			array[17] = 0;
			array[18] = 0;
			array[19] = 0;
			this.WriteBytes(offset, array, 1, 19);
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00116CE8 File Offset: 0x001160E8
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00116D00 File Offset: 0x00116100
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x04001B13 RID: 6931
		internal const int LMEM_FIXED = 0;

		// Token: 0x04001B14 RID: 6932
		internal const int LMEM_MOVEABLE = 2;

		// Token: 0x04001B15 RID: 6933
		internal const int LMEM_ZEROINIT = 64;

		// Token: 0x04001B16 RID: 6934
		private readonly int _bufferLength;
	}
}
