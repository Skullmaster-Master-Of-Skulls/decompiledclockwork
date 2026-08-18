using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.ProviderBase
{
	// Token: 0x02000209 RID: 521
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06001CA6 RID: 7334 RVA: 0x00269B48 File Offset: 0x00268F48
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

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00269BC8 File Offset: 0x00268FC8
		protected DbBuffer(int initialSize) : this(initialSize, true)
		{
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00269BE8 File Offset: 0x00268FE8
		protected DbBuffer(IntPtr invalidHandleValue, bool ownsHandle) : base(invalidHandleValue, ownsHandle)
		{
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00269C08 File Offset: 0x00269008
		private int BaseOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00269C18 File Offset: 0x00269018
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00269C38 File Offset: 0x00269038
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00269C58 File Offset: 0x00269058
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

		// Token: 0x06001CAD RID: 7341 RVA: 0x00269CE8 File Offset: 0x002690E8
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

		// Token: 0x06001CAE RID: 7342 RVA: 0x00269D58 File Offset: 0x00269158
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

		// Token: 0x06001CAF RID: 7343 RVA: 0x00269DB8 File Offset: 0x002691B8
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] destination = new byte[length];
			return this.ReadBytes(offset, destination, 0, length);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00269DD8 File Offset: 0x002691D8
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

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00269E48 File Offset: 0x00269248
		internal char ReadChar(int offset)
		{
			short num = this.ReadInt16(offset);
			return (char)num;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00269E68 File Offset: 0x00269268
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

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00269EE8 File Offset: 0x002692E8
		internal double ReadDouble(int offset)
		{
			long value = this.ReadInt64(offset);
			return BitConverter.Int64BitsToDouble(value);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00269F08 File Offset: 0x00269308
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

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00269F68 File Offset: 0x00269368
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

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00269FD8 File Offset: 0x002693D8
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

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0026A038 File Offset: 0x00269438
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

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0026A0A8 File Offset: 0x002694A8
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

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0026A108 File Offset: 0x00269508
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

		// Token: 0x06001CBA RID: 7354 RVA: 0x0026A168 File Offset: 0x00269568
		internal unsafe float ReadSingle(int offset)
		{
			int num = this.ReadInt32(offset);
			return *(float*)(&num);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0026A188 File Offset: 0x00269588
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

		// Token: 0x06001CBC RID: 7356 RVA: 0x0026A1C8 File Offset: 0x002695C8
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

		// Token: 0x06001CBD RID: 7357 RVA: 0x0026A238 File Offset: 0x00269638
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

		// Token: 0x06001CBE RID: 7358 RVA: 0x0026A298 File Offset: 0x00269698
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

		// Token: 0x06001CBF RID: 7359 RVA: 0x0026A308 File Offset: 0x00269708
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

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0026A378 File Offset: 0x00269778
		internal void WriteDouble(int offset, double value)
		{
			this.WriteInt64(offset, BitConverter.DoubleToInt64Bits(value));
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0026A398 File Offset: 0x00269798
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

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0026A3F8 File Offset: 0x002697F8
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

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0026A468 File Offset: 0x00269868
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

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0026A4C8 File Offset: 0x002698C8
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

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0026A538 File Offset: 0x00269938
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

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0026A598 File Offset: 0x00269998
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

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0026A5F8 File Offset: 0x002699F8
		internal unsafe void WriteSingle(int offset, float value)
		{
			this.WriteInt32(offset, *(int*)(&value));
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0026A618 File Offset: 0x00269A18
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

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0026A678 File Offset: 0x00269A78
		internal Guid ReadGuid(int offset)
		{
			byte[] array = new byte[16];
			this.ReadBytes(offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0026A6A8 File Offset: 0x00269AA8
		internal void WriteGuid(int offset, Guid value)
		{
			this.StructureToPtr(offset, value);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0026A6C8 File Offset: 0x00269AC8
		internal DateTime ReadDate(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0026A6F8 File Offset: 0x00269AF8
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

		// Token: 0x06001CCD RID: 7373 RVA: 0x0026A748 File Offset: 0x00269B48
		internal TimeSpan ReadTime(int offset)
		{
			short[] array = new short[3];
			this.ReadInt16Array(offset, array, 0, 3);
			return new TimeSpan((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]));
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0026A778 File Offset: 0x00269B78
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

		// Token: 0x06001CCF RID: 7375 RVA: 0x0026A7C8 File Offset: 0x00269BC8
		internal DateTime ReadDateTime(int offset)
		{
			short[] array = new short[6];
			this.ReadInt16Array(offset, array, 0, 6);
			int num = this.ReadInt32(offset + 12);
			DateTime dateTime = new DateTime((int)((ushort)array[0]), (int)((ushort)array[1]), (int)((ushort)array[2]), (int)((ushort)array[3]), (int)((ushort)array[4]), (int)((ushort)array[5]));
			return dateTime.AddTicks((long)(num / 100));
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0026A828 File Offset: 0x00269C28
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

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0026A8A8 File Offset: 0x00269CA8
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

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0026A938 File Offset: 0x00269D38
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

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0026A9A8 File Offset: 0x00269DA8
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0026A9C8 File Offset: 0x00269DC8
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x04001095 RID: 4245
		internal const int LMEM_FIXED = 0;

		// Token: 0x04001096 RID: 4246
		internal const int LMEM_MOVEABLE = 2;

		// Token: 0x04001097 RID: 4247
		internal const int LMEM_ZEROINIT = 64;

		// Token: 0x04001098 RID: 4248
		private readonly int _bufferLength;
	}
}
