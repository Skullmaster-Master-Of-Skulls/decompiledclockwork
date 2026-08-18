using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200051B RID: 1307
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal abstract class SafePointer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060032C3 RID: 12995 RVA: 0x000AB5ED File Offset: 0x000AA5ED
		protected SafePointer(bool ownsHandle) : base(ownsHandle)
		{
			this._numBytes = SafePointer.Uninitialized;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000AB604 File Offset: 0x000AA604
		public void Initialize(ulong numBytes)
		{
			if (numBytes < 0UL)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (IntPtr.Size == 4 && numBytes > (ulong)-1)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
			}
			if (numBytes >= (ulong)SafePointer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
			}
			this._numBytes = (UIntPtr)numBytes;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000AB67C File Offset: 0x000AA67C
		public void Initialize(uint numElements, uint sizeOfEachElement)
		{
			if (numElements < 0U)
			{
				throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (sizeOfEachElement < 0U)
			{
				throw new ArgumentOutOfRangeException("sizeOfEachElement", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (IntPtr.Size == 4 && numElements * sizeOfEachElement > 4294967295U)
			{
				throw new ArgumentOutOfRangeException("numBytes", Environment.GetResourceString("ArgumentOutOfRange_AddressSpace"));
			}
			if ((ulong)(numElements * sizeOfEachElement) >= (ulong)SafePointer.Uninitialized)
			{
				throw new ArgumentOutOfRangeException("numElements", Environment.GetResourceString("ArgumentOutOfRange_UIntPtrMax-1"));
			}
			this._numBytes = (UIntPtr)(checked(numElements * sizeOfEachElement));
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000AB711 File Offset: 0x000AA711
		public void Initialize<T>(uint numElements) where T : struct
		{
			this.Initialize(numElements, SafePointer.SizeOf<T>());
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000AB71F File Offset: 0x000AA71F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static uint SizeOf<T>() where T : struct
		{
			return SafePointer.SizeOfType(typeof(T));
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000AB730 File Offset: 0x000AA730
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (this._numBytes == SafePointer.Uninitialized)
			{
				throw SafePointer.NotInitialized();
			}
			pointer = (IntPtr)((UIntPtr)0);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = false;
				base.DangerousAddRef(ref flag);
				pointer = (void*)this.handle;
			}
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000AB788 File Offset: 0x000AA788
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleasePointer()
		{
			if (this._numBytes == SafePointer.Uninitialized)
			{
				throw SafePointer.NotInitialized();
			}
			base.DangerousRelease();
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000AB7A8 File Offset: 0x000AA7A8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe T Read<T>(uint byteOffset) where T : struct
		{
			if (this._numBytes == SafePointer.Uninitialized)
			{
				throw SafePointer.NotInitialized();
			}
			uint num = SafePointer.SizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			T result;
			try
			{
				base.DangerousAddRef(ref flag);
				SafePointer.GenericPtrToStructure<T>(ptr, out result, num);
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

		// Token: 0x060032CB RID: 13003 RVA: 0x000AB820 File Offset: 0x000AA820
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void ReadArray<T>(uint byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._numBytes == SafePointer.Uninitialized)
			{
				throw SafePointer.NotInitialized();
			}
			uint num = SafePointer.SizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, checked((ulong)((long)num * unchecked((long)count))));
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				for (int i = 0; i < count; i++)
				{
					SafePointer.GenericPtrToStructure<T>(ptr + (ulong)num * (ulong)((long)i), out array[i + count], num);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000AB91C File Offset: 0x000AA91C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void Write<T>(uint byteOffset, T value) where T : struct
		{
			if (this._numBytes == SafePointer.Uninitialized)
			{
				throw SafePointer.NotInitialized();
			}
			uint num = SafePointer.SizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, (ulong)num);
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				SafePointer.GenericStructureToPtr<T>(ref value, ptr, num);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000AB994 File Offset: 0x000AA994
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void WriteArray<T>(uint byteOffset, T[] array, int index, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._numBytes == SafePointer.Uninitialized)
			{
				throw SafePointer.NotInitialized();
			}
			uint num = SafePointer.SizeOf<T>();
			byte* ptr = (byte*)((void*)this.handle) + byteOffset;
			this.SpaceCheck(ptr, checked((ulong)((long)num * unchecked((long)count))));
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				for (int i = 0; i < count; i++)
				{
					SafePointer.GenericStructureToPtr<T>(ref array[i + count], ptr + (ulong)num * (ulong)((long)i), num);
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000ABA90 File Offset: 0x000AAA90
		public ulong ByteLength
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (this._numBytes == SafePointer.Uninitialized)
				{
					throw SafePointer.NotInitialized();
				}
				return (ulong)this._numBytes;
			}
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000ABAB5 File Offset: 0x000AAAB5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private unsafe void SpaceCheck(byte* ptr, ulong sizeInBytes)
		{
			if ((long)((byte*)ptr - (byte*)((void*)this.handle)) > (long)((ulong)this._numBytes - sizeInBytes))
			{
				SafePointer.NotEnoughRoom();
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000ABADB File Offset: 0x000AAADB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void NotEnoughRoom()
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000ABAEC File Offset: 0x000AAAEC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static InvalidOperationException NotInitialized()
		{
			return new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustCallInitialize"));
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000ABAFD File Offset: 0x000AAAFD
		private unsafe static void GenericPtrToStructure<T>(byte* ptr, out T structure, uint sizeofT) where T : struct
		{
			structure = default(T);
			SafePointer.PtrToStructureNative(ptr, __makeref(structure), sizeofT);
		}

		// Token: 0x060032D3 RID: 13011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void PtrToStructureNative(byte* ptr, TypedReference structure, uint sizeofT);

		// Token: 0x060032D4 RID: 13012 RVA: 0x000ABB13 File Offset: 0x000AAB13
		private unsafe static void GenericStructureToPtr<T>(ref T structure, byte* ptr, uint sizeofT) where T : struct
		{
			SafePointer.StructureToPtrNative(__makeref(structure), ptr, sizeofT);
		}

		// Token: 0x060032D5 RID: 13013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void StructureToPtrNative(TypedReference structure, byte* ptr, uint sizeofT);

		// Token: 0x060032D6 RID: 13014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint SizeOfType(Type type);

		// Token: 0x040019F3 RID: 6643
		private static readonly UIntPtr Uninitialized = (UIntPtr.Size == 4) ? ((UIntPtr)uint.MaxValue) : ((UIntPtr)ulong.MaxValue);

		// Token: 0x040019F4 RID: 6644
		private UIntPtr _numBytes;
	}
}
