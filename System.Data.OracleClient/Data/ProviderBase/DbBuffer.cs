using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.ProviderBase
{
	// Token: 0x0200001C RID: 28
	internal abstract class DbBuffer : SafeHandle
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0005A8D4 File Offset: 0x00059CD4
		protected DbBuffer(int initialSize, bool zeroBuffer) : base(IntPtr.Zero, true)
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

		// Token: 0x06000197 RID: 407 RVA: 0x0005A954 File Offset: 0x00059D54
		protected DbBuffer(int initialSize) : this(initialSize, true)
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0005A974 File Offset: 0x00059D74
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0005A994 File Offset: 0x00059D94
		protected int BaseOffset
		{
			get
			{
				return this._baseOffset;
			}
			set
			{
				this._baseOffset = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0005A9B4 File Offset: 0x00059DB4
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0005A9D4 File Offset: 0x00059DD4
		internal int Length
		{
			get
			{
				return this._bufferLength;
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0005A9F4 File Offset: 0x00059DF4
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

		// Token: 0x0600019D RID: 413 RVA: 0x0005AA84 File Offset: 0x00059E84
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

		// Token: 0x0600019E RID: 414 RVA: 0x0005AAF4 File Offset: 0x00059EF4
		internal byte[] ReadBytes(int offset, int length)
		{
			byte[] destination = new byte[length];
			return this.ReadBytes(offset, destination, 0, length);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0005AB14 File Offset: 0x00059F14
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

		// Token: 0x060001A0 RID: 416 RVA: 0x0005AB84 File Offset: 0x00059F84
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

		// Token: 0x060001A1 RID: 417 RVA: 0x0005ABE4 File Offset: 0x00059FE4
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

		// Token: 0x060001A2 RID: 418 RVA: 0x0005AC44 File Offset: 0x0005A044
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

		// Token: 0x060001A3 RID: 419 RVA: 0x0005ACA4 File Offset: 0x0005A0A4
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

		// Token: 0x060001A4 RID: 420 RVA: 0x0005ACE4 File Offset: 0x0005A0E4
		internal void StructureToPtr(int offset, object structure)
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

		// Token: 0x060001A5 RID: 421 RVA: 0x0005AD54 File Offset: 0x0005A154
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

		// Token: 0x060001A6 RID: 422 RVA: 0x0005ADC4 File Offset: 0x0005A1C4
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

		// Token: 0x060001A7 RID: 423 RVA: 0x0005AE24 File Offset: 0x0005A224
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

		// Token: 0x060001A8 RID: 424 RVA: 0x0005AE84 File Offset: 0x0005A284
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

		// Token: 0x060001A9 RID: 425 RVA: 0x0005AEE4 File Offset: 0x0005A2E4
		[Conditional("DEBUG")]
		protected void ValidateCheck(int offset, int count)
		{
			this.Validate(offset, count);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0005AF04 File Offset: 0x0005A304
		protected void Validate(int offset, int count)
		{
			if (offset < 0 || count < 0 || this.Length < checked(offset + count))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidBuffer);
			}
		}

		// Token: 0x040001AD RID: 429
		internal const int LMEM_FIXED = 0;

		// Token: 0x040001AE RID: 430
		internal const int LMEM_MOVEABLE = 2;

		// Token: 0x040001AF RID: 431
		internal const int LMEM_ZEROINIT = 64;

		// Token: 0x040001B0 RID: 432
		private readonly int _bufferLength;

		// Token: 0x040001B1 RID: 433
		private int _baseOffset;
	}
}
