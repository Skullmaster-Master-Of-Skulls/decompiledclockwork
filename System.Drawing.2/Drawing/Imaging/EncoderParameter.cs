using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000099 RID: 153
	[StructLayout(LayoutKind.Sequential)]
	public sealed class EncoderParameter : IDisposable
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x00022E34 File Offset: 0x00021034
		~EncoderParameter()
		{
			this.Dispose(false);
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00022E64 File Offset: 0x00021064
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00022E71 File Offset: 0x00021071
		public Encoder Encoder
		{
			get
			{
				return new Encoder(this.parameterGuid);
			}
			set
			{
				this.parameterGuid = value.Guid;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00022E7F File Offset: 0x0002107F
		public EncoderParameterValueType Type
		{
			get
			{
				return this.parameterValueType;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00022E7F File Offset: 0x0002107F
		public EncoderParameterValueType ValueType
		{
			get
			{
				return this.parameterValueType;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00022E87 File Offset: 0x00021087
		public int NumberOfValues
		{
			get
			{
				return this.numberOfValues;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00022E8F File Offset: 0x0002108F
		public void Dispose()
		{
			this.Dispose(true);
			GC.KeepAlive(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00022EA4 File Offset: 0x000210A4
		private void Dispose(bool disposing)
		{
			if (this.parameterValue != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.parameterValue);
			}
			this.parameterValue = IntPtr.Zero;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00022ED0 File Offset: 0x000210D0
		public EncoderParameter(Encoder encoder, byte value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeByte;
			this.numberOfValues = 1;
			this.parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteByte(this.parameterValue, value);
			GC.KeepAlive(this);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00022F44 File Offset: 0x00021144
		public EncoderParameter(Encoder encoder, byte value, bool undefined)
		{
			this.parameterGuid = encoder.Guid;
			if (undefined)
			{
				this.parameterValueType = EncoderParameterValueType.ValueTypeUndefined;
			}
			else
			{
				this.parameterValueType = EncoderParameterValueType.ValueTypeByte;
			}
			this.numberOfValues = 1;
			this.parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteByte(this.parameterValue, value);
			GC.KeepAlive(this);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00022FC4 File Offset: 0x000211C4
		public EncoderParameter(Encoder encoder, short value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeShort;
			this.numberOfValues = 1;
			this.parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(short)));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt16(this.parameterValue, value);
			GC.KeepAlive(this);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00023038 File Offset: 0x00021238
		public EncoderParameter(Encoder encoder, long value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeLong;
			this.numberOfValues = 1;
			this.parameterValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this.parameterValue, (int)value);
			GC.KeepAlive(this);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000230AC File Offset: 0x000212AC
		public EncoderParameter(Encoder encoder, int numerator, int denominator)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeRational;
			this.numberOfValues = 1;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(2 * num);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this.parameterValue, numerator);
			Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, num), denominator);
			GC.KeepAlive(this);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00023134 File Offset: 0x00021334
		public EncoderParameter(Encoder encoder, long rangebegin, long rangeend)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeLongRange;
			this.numberOfValues = 1;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(2 * num);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this.parameterValue, (int)rangebegin);
			Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, num), (int)rangeend);
			GC.KeepAlive(this);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000231C0 File Offset: 0x000213C0
		public EncoderParameter(Encoder encoder, int numerator1, int demoninator1, int numerator2, int demoninator2)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeRationalRange;
			this.numberOfValues = 1;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(4 * num);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.WriteInt32(this.parameterValue, numerator1);
			Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, num), demoninator1);
			Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, 2 * num), numerator2);
			Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, 3 * num), demoninator2);
			GC.KeepAlive(this);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00023274 File Offset: 0x00021474
		public EncoderParameter(Encoder encoder, string value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeAscii;
			this.numberOfValues = value.Length;
			this.parameterValue = Marshal.StringToHGlobalAnsi(value);
			GC.KeepAlive(this);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000232D4 File Offset: 0x000214D4
		public EncoderParameter(Encoder encoder, byte[] value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeByte;
			this.numberOfValues = value.Length;
			this.parameterValue = Marshal.AllocHGlobal(this.numberOfValues);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.Copy(value, 0, this.parameterValue, this.numberOfValues);
			GC.KeepAlive(this);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00023348 File Offset: 0x00021548
		public EncoderParameter(Encoder encoder, byte[] value, bool undefined)
		{
			this.parameterGuid = encoder.Guid;
			if (undefined)
			{
				this.parameterValueType = EncoderParameterValueType.ValueTypeUndefined;
			}
			else
			{
				this.parameterValueType = EncoderParameterValueType.ValueTypeByte;
			}
			this.numberOfValues = value.Length;
			this.parameterValue = Marshal.AllocHGlobal(this.numberOfValues);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.Copy(value, 0, this.parameterValue, this.numberOfValues);
			GC.KeepAlive(this);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000233C8 File Offset: 0x000215C8
		public EncoderParameter(Encoder encoder, short[] value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeShort;
			this.numberOfValues = value.Length;
			int num = Marshal.SizeOf(typeof(short));
			this.parameterValue = Marshal.AllocHGlobal(checked(this.numberOfValues * num));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			Marshal.Copy(value, 0, this.parameterValue, this.numberOfValues);
			GC.KeepAlive(this);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002344C File Offset: 0x0002164C
		public unsafe EncoderParameter(Encoder encoder, long[] value)
		{
			this.parameterGuid = encoder.Guid;
			this.parameterValueType = EncoderParameterValueType.ValueTypeLong;
			this.numberOfValues = value.Length;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(checked(this.numberOfValues * num));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			int* ptr = (int*)((void*)this.parameterValue);
			fixed (long[] array = value)
			{
				long* ptr2;
				if (value == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				for (int i = 0; i < value.Length; i++)
				{
					ptr[i] = (int)ptr2[i];
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00023508 File Offset: 0x00021708
		public EncoderParameter(Encoder encoder, int[] numerator, int[] denominator)
		{
			this.parameterGuid = encoder.Guid;
			if (numerator.Length != denominator.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			this.parameterValueType = EncoderParameterValueType.ValueTypeRational;
			this.numberOfValues = numerator.Length;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(checked(this.numberOfValues * 2 * num));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < this.numberOfValues; i++)
			{
				Marshal.WriteInt32(EncoderParameter.Add(i * 2 * num, this.parameterValue), numerator[i]);
				Marshal.WriteInt32(EncoderParameter.Add((i * 2 + 1) * num, this.parameterValue), denominator[i]);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000235D0 File Offset: 0x000217D0
		public EncoderParameter(Encoder encoder, long[] rangebegin, long[] rangeend)
		{
			this.parameterGuid = encoder.Guid;
			if (rangebegin.Length != rangeend.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			this.parameterValueType = EncoderParameterValueType.ValueTypeLongRange;
			this.numberOfValues = rangebegin.Length;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(checked(this.numberOfValues * 2 * num));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < this.numberOfValues; i++)
			{
				Marshal.WriteInt32(EncoderParameter.Add(i * 2 * num, this.parameterValue), (int)rangebegin[i]);
				Marshal.WriteInt32(EncoderParameter.Add((i * 2 + 1) * num, this.parameterValue), (int)rangeend[i]);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00023698 File Offset: 0x00021898
		public EncoderParameter(Encoder encoder, int[] numerator1, int[] denominator1, int[] numerator2, int[] denominator2)
		{
			this.parameterGuid = encoder.Guid;
			if (numerator1.Length != denominator1.Length || numerator1.Length != denominator2.Length || denominator1.Length != denominator2.Length)
			{
				throw SafeNativeMethods.Gdip.StatusException(2);
			}
			this.parameterValueType = EncoderParameterValueType.ValueTypeRationalRange;
			this.numberOfValues = numerator1.Length;
			int num = Marshal.SizeOf(typeof(int));
			this.parameterValue = Marshal.AllocHGlobal(checked(this.numberOfValues * 4 * num));
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < this.numberOfValues; i++)
			{
				Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, 4 * i * num), numerator1[i]);
				Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, (4 * i + 1) * num), denominator1[i]);
				Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, (4 * i + 2) * num), numerator2[i]);
				Marshal.WriteInt32(EncoderParameter.Add(this.parameterValue, (4 * i + 3) * num), denominator2[i]);
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000237A8 File Offset: 0x000219A8
		[Obsolete("This constructor has been deprecated. Use EncoderParameter(Encoder encoder, int numberValues, EncoderParameterValueType type, IntPtr value) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public EncoderParameter(Encoder encoder, int NumberOfValues, int Type, int Value)
		{
			IntSecurity.UnmanagedCode.Demand();
			int num;
			switch (Type)
			{
			case 1:
			case 2:
				num = 1;
				break;
			case 3:
				num = 2;
				break;
			case 4:
				num = 4;
				break;
			case 5:
			case 6:
				num = 8;
				break;
			case 7:
				num = 1;
				break;
			case 8:
				num = 16;
				break;
			default:
				throw SafeNativeMethods.Gdip.StatusException(8);
			}
			int num2 = checked(num * NumberOfValues);
			this.parameterValue = Marshal.AllocHGlobal(num2);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.WriteByte(EncoderParameter.Add(this.parameterValue, i), Marshal.ReadByte((IntPtr)(Value + i)));
			}
			this.parameterValueType = (EncoderParameterValueType)Type;
			this.numberOfValues = NumberOfValues;
			this.parameterGuid = encoder.Guid;
			GC.KeepAlive(this);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00023888 File Offset: 0x00021A88
		public EncoderParameter(Encoder encoder, int numberValues, EncoderParameterValueType type, IntPtr value)
		{
			IntSecurity.UnmanagedCode.Demand();
			int num;
			switch (type)
			{
			case EncoderParameterValueType.ValueTypeByte:
			case EncoderParameterValueType.ValueTypeAscii:
				num = 1;
				break;
			case EncoderParameterValueType.ValueTypeShort:
				num = 2;
				break;
			case EncoderParameterValueType.ValueTypeLong:
				num = 4;
				break;
			case EncoderParameterValueType.ValueTypeRational:
			case EncoderParameterValueType.ValueTypeLongRange:
				num = 8;
				break;
			case EncoderParameterValueType.ValueTypeUndefined:
				num = 1;
				break;
			case EncoderParameterValueType.ValueTypeRationalRange:
				num = 16;
				break;
			default:
				throw SafeNativeMethods.Gdip.StatusException(8);
			}
			int num2 = checked(num * numberValues);
			this.parameterValue = Marshal.AllocHGlobal(num2);
			if (this.parameterValue == IntPtr.Zero)
			{
				throw SafeNativeMethods.Gdip.StatusException(3);
			}
			for (int i = 0; i < num2; i++)
			{
				Marshal.WriteByte(EncoderParameter.Add(this.parameterValue, i), Marshal.ReadByte(value + i));
			}
			this.parameterValueType = type;
			this.numberOfValues = numberValues;
			this.parameterGuid = encoder.Guid;
			GC.KeepAlive(this);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00023962 File Offset: 0x00021B62
		private static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00023972 File Offset: 0x00021B72
		private static IntPtr Add(int a, IntPtr b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x04000880 RID: 2176
		[MarshalAs(UnmanagedType.Struct)]
		private Guid parameterGuid;

		// Token: 0x04000881 RID: 2177
		private int numberOfValues;

		// Token: 0x04000882 RID: 2178
		private EncoderParameterValueType parameterValueType;

		// Token: 0x04000883 RID: 2179
		private IntPtr parameterValue;
	}
}
