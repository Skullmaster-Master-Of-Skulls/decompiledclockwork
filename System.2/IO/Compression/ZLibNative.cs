using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Compression
{
	// Token: 0x02000424 RID: 1060
	internal static class ZLibNative
	{
		// Token: 0x060027AD RID: 10157 RVA: 0x000B6B14 File Offset: 0x000B4D14
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForDeflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle)
		{
			return ZLibNative.CreateZLibStreamForDeflate(out zLibStreamHandle, ZLibNative.CompressionLevel.DefaultCompression, -15, 8, ZLibNative.CompressionStrategy.DefaultStrategy);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000B6B21 File Offset: 0x000B4D21
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForDeflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle, ZLibNative.CompressionLevel level, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy)
		{
			zLibStreamHandle = new ZLibNative.ZLibStreamHandle();
			return zLibStreamHandle.DeflateInit2_(level, windowBits, memLevel, strategy);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000B6B36 File Offset: 0x000B4D36
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForInflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle)
		{
			return ZLibNative.CreateZLibStreamForInflate(out zLibStreamHandle, -15);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000B6B40 File Offset: 0x000B4D40
		[SecurityCritical]
		public static ZLibNative.ErrorCode CreateZLibStreamForInflate(out ZLibNative.ZLibStreamHandle zLibStreamHandle, int windowBits)
		{
			zLibStreamHandle = new ZLibNative.ZLibStreamHandle();
			return zLibStreamHandle.InflateInit2_(windowBits);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000B6B51 File Offset: 0x000B4D51
		[SecurityCritical]
		public static int ZLibCompileFlags()
		{
			return ZLibNative.ZLibStreamHandle.ZLibCompileFlags();
		}

		// Token: 0x0400218C RID: 8588
		public const string ZLibNativeDllName = "clrcompression.dll";

		// Token: 0x0400218D RID: 8589
		private const string Kernel32DllName = "kernel32.dll";

		// Token: 0x0400218E RID: 8590
		public const string ZLibVersion = "1.3.1";

		// Token: 0x0400218F RID: 8591
		internal static readonly IntPtr ZNullPtr = (IntPtr)0;

		// Token: 0x04002190 RID: 8592
		public const int Deflate_DefaultWindowBits = -15;

		// Token: 0x04002191 RID: 8593
		public const int Deflate_DefaultMemLevel = 8;

		// Token: 0x02000818 RID: 2072
		public enum FlushCode
		{
			// Token: 0x040035A5 RID: 13733
			NoFlush,
			// Token: 0x040035A6 RID: 13734
			PartialFlush,
			// Token: 0x040035A7 RID: 13735
			SyncFlush,
			// Token: 0x040035A8 RID: 13736
			FullFlush,
			// Token: 0x040035A9 RID: 13737
			Finish,
			// Token: 0x040035AA RID: 13738
			Block
		}

		// Token: 0x02000819 RID: 2073
		public enum ErrorCode
		{
			// Token: 0x040035AC RID: 13740
			Ok,
			// Token: 0x040035AD RID: 13741
			StreamEnd,
			// Token: 0x040035AE RID: 13742
			NeedDictionary,
			// Token: 0x040035AF RID: 13743
			ErrorNo = -1,
			// Token: 0x040035B0 RID: 13744
			StreamError = -2,
			// Token: 0x040035B1 RID: 13745
			DataError = -3,
			// Token: 0x040035B2 RID: 13746
			MemError = -4,
			// Token: 0x040035B3 RID: 13747
			BufError = -5,
			// Token: 0x040035B4 RID: 13748
			VersionError = -6
		}

		// Token: 0x0200081A RID: 2074
		public enum CompressionLevel
		{
			// Token: 0x040035B6 RID: 13750
			NoCompression,
			// Token: 0x040035B7 RID: 13751
			BestSpeed,
			// Token: 0x040035B8 RID: 13752
			BestCompression = 9,
			// Token: 0x040035B9 RID: 13753
			DefaultCompression = -1
		}

		// Token: 0x0200081B RID: 2075
		public enum CompressionStrategy
		{
			// Token: 0x040035BB RID: 13755
			Filtered = 1,
			// Token: 0x040035BC RID: 13756
			HuffmanOnly,
			// Token: 0x040035BD RID: 13757
			Rle,
			// Token: 0x040035BE RID: 13758
			Fixed,
			// Token: 0x040035BF RID: 13759
			DefaultStrategy = 0
		}

		// Token: 0x0200081C RID: 2076
		public enum CompressionMethod
		{
			// Token: 0x040035C1 RID: 13761
			Deflated = 8
		}

		// Token: 0x0200081D RID: 2077
		internal struct ZStream
		{
			// Token: 0x040035C2 RID: 13762
			internal IntPtr nextIn;

			// Token: 0x040035C3 RID: 13763
			internal uint availIn;

			// Token: 0x040035C4 RID: 13764
			internal uint totalIn;

			// Token: 0x040035C5 RID: 13765
			internal IntPtr nextOut;

			// Token: 0x040035C6 RID: 13766
			internal uint availOut;

			// Token: 0x040035C7 RID: 13767
			internal uint totalOut;

			// Token: 0x040035C8 RID: 13768
			internal IntPtr msg;

			// Token: 0x040035C9 RID: 13769
			internal IntPtr state;

			// Token: 0x040035CA RID: 13770
			internal IntPtr zalloc;

			// Token: 0x040035CB RID: 13771
			internal IntPtr zfree;

			// Token: 0x040035CC RID: 13772
			internal IntPtr opaque;

			// Token: 0x040035CD RID: 13773
			internal int dataType;

			// Token: 0x040035CE RID: 13774
			internal uint adler;

			// Token: 0x040035CF RID: 13775
			internal uint reserved;
		}

		// Token: 0x0200081E RID: 2078
		// (Invoke) Token: 0x06004511 RID: 17681
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode DeflateInit2_Delegate(ZLibNative.ZStream* stream, ZLibNative.CompressionLevel level, ZLibNative.CompressionMethod method, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy, [MarshalAs(UnmanagedType.LPStr)] string version, int streamSize);

		// Token: 0x0200081F RID: 2079
		// (Invoke) Token: 0x06004515 RID: 17685
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode DeflateDelegate(ZLibNative.ZStream* stream, ZLibNative.FlushCode flush);

		// Token: 0x02000820 RID: 2080
		// (Invoke) Token: 0x06004519 RID: 17689
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode DeflateEndDelegate(ZLibNative.ZStream* stream);

		// Token: 0x02000821 RID: 2081
		// (Invoke) Token: 0x0600451D RID: 17693
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode InflateInit2_Delegate(ZLibNative.ZStream* stream, int windowBits, [MarshalAs(UnmanagedType.LPStr)] string version, int streamSize);

		// Token: 0x02000822 RID: 2082
		// (Invoke) Token: 0x06004521 RID: 17697
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode InflateDelegate(ZLibNative.ZStream* stream, ZLibNative.FlushCode flush);

		// Token: 0x02000823 RID: 2083
		// (Invoke) Token: 0x06004525 RID: 17701
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private unsafe delegate ZLibNative.ErrorCode InflateEndDelegate(ZLibNative.ZStream* stream);

		// Token: 0x02000824 RID: 2084
		// (Invoke) Token: 0x06004529 RID: 17705
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		private delegate int ZlibCompileFlagsDelegate();

		// Token: 0x02000825 RID: 2085
		private class NativeMethods
		{
			// Token: 0x0600452C RID: 17708
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
			internal static extern IntPtr GetProcAddress(ZLibNative.SafeLibraryHandle moduleHandle, string procName);

			// Token: 0x0600452D RID: 17709
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern ZLibNative.SafeLibraryHandle LoadLibrary(string libPath);

			// Token: 0x0600452E RID: 17710
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[DllImport("kernel32.dll", ExactSpelling = true)]
			internal static extern bool FreeLibrary(IntPtr moduleHandle);
		}

		// Token: 0x02000826 RID: 2086
		[SecurityCritical]
		private class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06004530 RID: 17712 RVA: 0x0012124C File Offset: 0x0011F44C
			[SecurityCritical]
			internal SafeLibraryHandle() : base(true)
			{
			}

			// Token: 0x06004531 RID: 17713 RVA: 0x00121258 File Offset: 0x0011F458
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[SecurityCritical]
			protected override bool ReleaseHandle()
			{
				bool result = ZLibNative.NativeMethods.FreeLibrary(this.handle);
				this.handle = IntPtr.Zero;
				return result;
			}
		}

		// Token: 0x02000827 RID: 2087
		[SecurityCritical]
		public sealed class ZLibStreamHandle : SafeHandleMinusOneIsInvalid
		{
			// Token: 0x06004532 RID: 17714 RVA: 0x0012127D File Offset: 0x0011F47D
			public unsafe ZLibStreamHandle() : base(true)
			{
				this.zStreamPtr = (ZLibNative.ZStream*)((void*)ZLibNative.ZLibStreamHandle.AllocWithZeroOut(sizeof(ZLibNative.ZStream)));
				this.initializationState = ZLibNative.ZLibStreamHandle.State.NotInitialized;
				this.handle = IntPtr.Zero;
			}

			// Token: 0x17000FAE RID: 4014
			// (get) Token: 0x06004533 RID: 17715 RVA: 0x001212B0 File Offset: 0x0011F4B0
			public ZLibNative.ZLibStreamHandle.State InitializationState
			{
				[SecurityCritical]
				get
				{
					return this.initializationState;
				}
			}

			// Token: 0x06004534 RID: 17716 RVA: 0x001212BC File Offset: 0x0011F4BC
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[SecurityCritical]
			protected unsafe override bool ReleaseHandle()
			{
				bool result;
				try
				{
					if (ZLibNative.ZLibStreamHandle.zlibLibraryHandle == null || ZLibNative.ZLibStreamHandle.zlibLibraryHandle.IsInvalid)
					{
						result = false;
					}
					else
					{
						switch (this.InitializationState)
						{
						case ZLibNative.ZLibStreamHandle.State.NotInitialized:
							result = true;
							break;
						case ZLibNative.ZLibStreamHandle.State.InitializedForDeflate:
							result = (this.DeflateEnd() == ZLibNative.ErrorCode.Ok);
							break;
						case ZLibNative.ZLibStreamHandle.State.InitializedForInflate:
							result = (this.InflateEnd() == ZLibNative.ErrorCode.Ok);
							break;
						case ZLibNative.ZLibStreamHandle.State.Disposed:
							result = true;
							break;
						default:
							result = false;
							break;
						}
					}
				}
				finally
				{
					if (this.zStreamPtr != null)
					{
						Marshal.FreeHGlobal((IntPtr)((void*)this.zStreamPtr));
						this.zStreamPtr = null;
					}
				}
				return result;
			}

			// Token: 0x17000FAF RID: 4015
			// (get) Token: 0x06004535 RID: 17717 RVA: 0x00121358 File Offset: 0x0011F558
			// (set) Token: 0x06004536 RID: 17718 RVA: 0x00121365 File Offset: 0x0011F565
			public unsafe IntPtr NextIn
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->nextIn;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->nextIn = value;
					}
				}
			}

			// Token: 0x17000FB0 RID: 4016
			// (get) Token: 0x06004537 RID: 17719 RVA: 0x0012137D File Offset: 0x0011F57D
			// (set) Token: 0x06004538 RID: 17720 RVA: 0x0012138A File Offset: 0x0011F58A
			public unsafe uint AvailIn
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->availIn;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->availIn = value;
					}
				}
			}

			// Token: 0x17000FB1 RID: 4017
			// (get) Token: 0x06004539 RID: 17721 RVA: 0x001213A2 File Offset: 0x0011F5A2
			public unsafe uint TotalIn
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->totalIn;
				}
			}

			// Token: 0x17000FB2 RID: 4018
			// (get) Token: 0x0600453A RID: 17722 RVA: 0x001213AF File Offset: 0x0011F5AF
			// (set) Token: 0x0600453B RID: 17723 RVA: 0x001213BC File Offset: 0x0011F5BC
			public unsafe IntPtr NextOut
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->nextOut;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->nextOut = value;
					}
				}
			}

			// Token: 0x17000FB3 RID: 4019
			// (get) Token: 0x0600453C RID: 17724 RVA: 0x001213D4 File Offset: 0x0011F5D4
			// (set) Token: 0x0600453D RID: 17725 RVA: 0x001213E1 File Offset: 0x0011F5E1
			public unsafe uint AvailOut
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->availOut;
				}
				[SecurityCritical]
				set
				{
					if (this.zStreamPtr != null)
					{
						this.zStreamPtr->availOut = value;
					}
				}
			}

			// Token: 0x17000FB4 RID: 4020
			// (get) Token: 0x0600453E RID: 17726 RVA: 0x001213F9 File Offset: 0x0011F5F9
			public unsafe uint TotalOut
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->totalOut;
				}
			}

			// Token: 0x17000FB5 RID: 4021
			// (get) Token: 0x0600453F RID: 17727 RVA: 0x00121406 File Offset: 0x0011F606
			public unsafe int DataType
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->dataType;
				}
			}

			// Token: 0x17000FB6 RID: 4022
			// (get) Token: 0x06004540 RID: 17728 RVA: 0x00121413 File Offset: 0x0011F613
			public unsafe uint Adler
			{
				[SecurityCritical]
				get
				{
					return this.zStreamPtr->adler;
				}
			}

			// Token: 0x06004541 RID: 17729 RVA: 0x00121420 File Offset: 0x0011F620
			[SecurityCritical]
			private void EnsureNotDisposed()
			{
				if (this.InitializationState == ZLibNative.ZLibStreamHandle.State.Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
			}

			// Token: 0x06004542 RID: 17730 RVA: 0x0012143C File Offset: 0x0011F63C
			[SecurityCritical]
			private void EnsureState(ZLibNative.ZLibStreamHandle.State requiredState)
			{
				if (this.InitializationState != requiredState)
				{
					throw new InvalidOperationException("InitializationState != " + requiredState.ToString());
				}
			}

			// Token: 0x06004543 RID: 17731 RVA: 0x00121464 File Offset: 0x0011F664
			[SecurityCritical]
			public ZLibNative.ErrorCode DeflateInit2_(ZLibNative.CompressionLevel level, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.NotInitialized);
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode result;
				try
				{
				}
				finally
				{
					result = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateInit2_Delegate(this.zStreamPtr, level, ZLibNative.CompressionMethod.Deflated, windowBits, memLevel, strategy, "1.3.1", sizeof(ZLibNative.ZStream));
					this.initializationState = ZLibNative.ZLibStreamHandle.State.InitializedForDeflate;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousAddRef(ref flag);
				}
				return result;
			}

			// Token: 0x06004544 RID: 17732 RVA: 0x001214D0 File Offset: 0x0011F6D0
			[SecurityCritical]
			public ZLibNative.ErrorCode Deflate(ZLibNative.FlushCode flush)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForDeflate);
				return ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateDelegate(this.zStreamPtr, flush);
			}

			// Token: 0x06004545 RID: 17733 RVA: 0x001214F0 File Offset: 0x0011F6F0
			[SecurityCritical]
			public ZLibNative.ErrorCode DeflateEnd()
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForDeflate);
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode result;
				try
				{
				}
				finally
				{
					result = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateEndDelegate(this.zStreamPtr);
					this.initializationState = ZLibNative.ZLibStreamHandle.State.Disposed;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousRelease();
				}
				return result;
			}

			// Token: 0x06004546 RID: 17734 RVA: 0x00121548 File Offset: 0x0011F748
			[SecurityCritical]
			public ZLibNative.ErrorCode InflateInit2_(int windowBits)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.NotInitialized);
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode result;
				try
				{
				}
				finally
				{
					result = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateInit2_Delegate(this.zStreamPtr, windowBits, "1.3.1", sizeof(ZLibNative.ZStream));
					this.initializationState = ZLibNative.ZLibStreamHandle.State.InitializedForInflate;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousAddRef(ref flag);
				}
				return result;
			}

			// Token: 0x06004547 RID: 17735 RVA: 0x001215B0 File Offset: 0x0011F7B0
			[SecurityCritical]
			public ZLibNative.ErrorCode Inflate(ZLibNative.FlushCode flush)
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForInflate);
				return ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateDelegate(this.zStreamPtr, flush);
			}

			// Token: 0x06004548 RID: 17736 RVA: 0x001215D0 File Offset: 0x0011F7D0
			[SecurityCritical]
			public ZLibNative.ErrorCode InflateEnd()
			{
				this.EnsureNotDisposed();
				this.EnsureState(ZLibNative.ZLibStreamHandle.State.InitializedForInflate);
				RuntimeHelpers.PrepareConstrainedRegions();
				ZLibNative.ErrorCode result;
				try
				{
				}
				finally
				{
					result = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateEndDelegate(this.zStreamPtr);
					this.initializationState = ZLibNative.ZLibStreamHandle.State.Disposed;
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle.DangerousRelease();
				}
				return result;
			}

			// Token: 0x06004549 RID: 17737 RVA: 0x00121628 File Offset: 0x0011F828
			[SecurityCritical]
			public unsafe string GetErrorMessage()
			{
				if (ZLibNative.ZNullPtr.Equals(this.zStreamPtr->msg))
				{
					return string.Empty;
				}
				return new string((sbyte*)((void*)this.zStreamPtr->msg));
			}

			// Token: 0x0600454A RID: 17738 RVA: 0x00121671 File Offset: 0x0011F871
			[SecurityCritical]
			internal static int ZLibCompileFlags()
			{
				return ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.zlibCompileFlagsDelegate();
			}

			// Token: 0x0600454B RID: 17739 RVA: 0x00121680 File Offset: 0x0011F880
			[SecurityCritical]
			private unsafe static IntPtr AllocWithZeroOut(int byteCount)
			{
				IntPtr intPtr = Marshal.AllocHGlobal(byteCount);
				byte* ptr = (byte*)((void*)intPtr);
				int num = byteCount / 4;
				int* ptr2 = (int*)ptr;
				for (int i = 0; i < num; i++)
				{
					ptr2[i] = 0;
				}
				num *= 4;
				ptr += num;
				int num2 = byteCount - num;
				for (int j = 0; j < num2; j++)
				{
					ptr[j] = 0;
				}
				return intPtr;
			}

			// Token: 0x040035D0 RID: 13776
			[SecurityCritical]
			private static ZLibNative.SafeLibraryHandle zlibLibraryHandle;

			// Token: 0x040035D1 RID: 13777
			[SecurityCritical]
			private unsafe ZLibNative.ZStream* zStreamPtr;

			// Token: 0x040035D2 RID: 13778
			[SecurityCritical]
			private volatile ZLibNative.ZLibStreamHandle.State initializationState;

			// Token: 0x02000930 RID: 2352
			[SecurityCritical]
			private static class NativeZLibDLLStub
			{
				// Token: 0x060046AC RID: 18092 RVA: 0x00126F10 File Offset: 0x00125110
				[SecuritySafeCritical]
				private static void LoadZLibDLL()
				{
					new FileIOPermission(PermissionState.Unrestricted).Assert();
					string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
					string text = Path.Combine(runtimeDirectory, "clrcompression.dll");
					if (!File.Exists(text))
					{
						throw new DllNotFoundException("clrcompression.dll");
					}
					ZLibNative.SafeLibraryHandle safeLibraryHandle = ZLibNative.NativeMethods.LoadLibrary(text);
					if (safeLibraryHandle.IsInvalid)
					{
						int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
						Marshal.ThrowExceptionForHR(hrforLastWin32Error, new IntPtr(-1));
						throw new InvalidOperationException();
					}
					ZLibNative.ZLibStreamHandle.zlibLibraryHandle = safeLibraryHandle;
				}

				// Token: 0x060046AD RID: 18093 RVA: 0x00126F7C File Offset: 0x0012517C
				[SecurityCritical]
				private static DT CreateDelegate<DT>(string entryPointName)
				{
					IntPtr procAddress = ZLibNative.NativeMethods.GetProcAddress(ZLibNative.ZLibStreamHandle.zlibLibraryHandle, entryPointName);
					if (IntPtr.Zero == procAddress)
					{
						throw new EntryPointNotFoundException("clrcompression.dll!" + entryPointName);
					}
					return (DT)((object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DT)));
				}

				// Token: 0x060046AE RID: 18094 RVA: 0x00126FC8 File Offset: 0x001251C8
				[SecuritySafeCritical]
				private static void InitDelegates()
				{
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateInit2_Delegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.DeflateInit2_Delegate>("deflateInit2_");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.DeflateDelegate>("deflate");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateEndDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.DeflateEndDelegate>("deflateEnd");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateInit2_Delegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.InflateInit2_Delegate>("inflateInit2_");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.InflateDelegate>("inflate");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateEndDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.InflateEndDelegate>("inflateEnd");
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.zlibCompileFlagsDelegate = ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.CreateDelegate<ZLibNative.ZlibCompileFlagsDelegate>("zlibCompileFlags");
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateInit2_Delegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.deflateEndDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateInit2_Delegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.inflateEndDelegate);
					RuntimeHelpers.PrepareDelegate(ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.zlibCompileFlagsDelegate);
				}

				// Token: 0x060046AF RID: 18095 RVA: 0x00127084 File Offset: 0x00125284
				[SecuritySafeCritical]
				static NativeZLibDLLStub()
				{
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.LoadZLibDLL();
					ZLibNative.ZLibStreamHandle.NativeZLibDLLStub.InitDelegates();
				}

				// Token: 0x04003DD6 RID: 15830
				[SecurityCritical]
				internal static ZLibNative.DeflateInit2_Delegate deflateInit2_Delegate;

				// Token: 0x04003DD7 RID: 15831
				[SecurityCritical]
				internal static ZLibNative.DeflateDelegate deflateDelegate;

				// Token: 0x04003DD8 RID: 15832
				[SecurityCritical]
				internal static ZLibNative.DeflateEndDelegate deflateEndDelegate;

				// Token: 0x04003DD9 RID: 15833
				[SecurityCritical]
				internal static ZLibNative.InflateInit2_Delegate inflateInit2_Delegate;

				// Token: 0x04003DDA RID: 15834
				[SecurityCritical]
				internal static ZLibNative.InflateDelegate inflateDelegate;

				// Token: 0x04003DDB RID: 15835
				[SecurityCritical]
				internal static ZLibNative.InflateEndDelegate inflateEndDelegate;

				// Token: 0x04003DDC RID: 15836
				[SecurityCritical]
				internal static ZLibNative.ZlibCompileFlagsDelegate zlibCompileFlagsDelegate;
			}

			// Token: 0x02000931 RID: 2353
			public enum State
			{
				// Token: 0x04003DDE RID: 15838
				NotInitialized,
				// Token: 0x04003DDF RID: 15839
				InitializedForDeflate,
				// Token: 0x04003DE0 RID: 15840
				InitializedForInflate,
				// Token: 0x04003DE1 RID: 15841
				Disposed
			}
		}
	}
}
