using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Data.OracleClient
{
	// Token: 0x02000037 RID: 55
	internal abstract class OciHandle : SafeHandle
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x0005BB24 File Offset: 0x0005AF24
		protected OciHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0005BB44 File Offset: 0x0005AF44
		protected OciHandle(OCI.HTYPE handleType) : base(IntPtr.Zero, false)
		{
			this._handleType = handleType;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0005BB64 File Offset: 0x0005AF64
		protected OciHandle(OciHandle parentHandle, OCI.HTYPE handleType) : this(parentHandle, handleType, OCI.MODE.OCI_DEFAULT, OciHandle.HANDLEFLAG.DEFAULT)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0005BB84 File Offset: 0x0005AF84
		protected OciHandle(OciHandle parentHandle, OCI.HTYPE handleType, OCI.MODE ocimode, OciHandle.HANDLEFLAG handleflags) : this()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._handleType = handleType;
				this._parentHandle = parentHandle;
				this._refCount = 1;
				int num;
				if (handleType <= OCI.HTYPE.OCI_DTYPE_FILE)
				{
					switch (handleType)
					{
					case OCI.HTYPE.OCI_HTYPE_ENV:
						if ((handleflags & OciHandle.HANDLEFLAG.NLS) == OciHandle.HANDLEFLAG.NLS)
						{
							num = TracedNativeMethods.OCIEnvNlsCreate(out this.handle, ocimode, 0, 0);
							if (num != 0 || IntPtr.Zero == this.handle)
							{
								throw ADP.OperationFailed("OCIEnvNlsCreate", num);
							}
							goto IL_177;
						}
						else
						{
							num = TracedNativeMethods.OCIEnvCreate(out this.handle, ocimode);
							if (num != 0 || IntPtr.Zero == this.handle)
							{
								throw ADP.OperationFailed("OCIEnvCreate", num);
							}
							goto IL_177;
						}
						break;
					case OCI.HTYPE.OCI_HTYPE_ERROR:
					case OCI.HTYPE.OCI_HTYPE_SVCCTX:
					case OCI.HTYPE.OCI_HTYPE_STMT:
					case OCI.HTYPE.OCI_HTYPE_SERVER:
					case OCI.HTYPE.OCI_HTYPE_SESSION:
						num = TracedNativeMethods.OCIHandleAlloc(parentHandle.EnvironmentHandle, out this.handle, handleType);
						if (num != 0 || IntPtr.Zero == this.handle)
						{
							throw ADP.OperationFailed("OCIHandleAlloc", num);
						}
						goto IL_177;
					case OCI.HTYPE.OCI_HTYPE_BIND:
					case OCI.HTYPE.OCI_HTYPE_DEFINE:
					case OCI.HTYPE.OCI_HTYPE_DESCRIBE:
						goto IL_177;
					default:
						switch (handleType)
						{
						case OCI.HTYPE.OCI_DTYPE_FIRST:
						case OCI.HTYPE.OCI_DTYPE_ROWID:
						case OCI.HTYPE.OCI_DTYPE_FILE:
							break;
						case OCI.HTYPE.OCI_DTYPE_SNAP:
						case OCI.HTYPE.OCI_DTYPE_RSET:
						case OCI.HTYPE.OCI_DTYPE_PARAM:
						case OCI.HTYPE.OCI_DTYPE_COMPLEXOBJECTCOMP:
							goto IL_177;
						default:
							goto IL_177;
						}
						break;
					}
				}
				else if (handleType != OCI.HTYPE.OCI_DTYPE_INTERVAL_DS)
				{
					switch (handleType)
					{
					case OCI.HTYPE.OCI_DTYPE_TIMESTAMP:
					case OCI.HTYPE.OCI_DTYPE_TIMESTAMP_TZ:
					case OCI.HTYPE.OCI_DTYPE_TIMESTAMP_LTZ:
						break;
					default:
						goto IL_177;
					}
				}
				num = TracedNativeMethods.OCIDescriptorAlloc(parentHandle.EnvironmentHandle, out this.handle, handleType);
				if (num != 0 || IntPtr.Zero == this.handle)
				{
					throw ADP.OperationFailed("OCIDescriptorAlloc", num);
				}
				IL_177:
				if (parentHandle != null)
				{
					parentHandle.AddRef();
					this._isUnicode = parentHandle.IsUnicode;
				}
				else
				{
					this._isUnicode = ((handleflags & OciHandle.HANDLEFLAG.UNICODE) == OciHandle.HANDLEFLAG.UNICODE);
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0005BD54 File Offset: 0x0005B154
		internal OciHandle EnvironmentHandle
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				OciHandle result;
				if (this.HandleType == OCI.HTYPE.OCI_HTYPE_ENV)
				{
					result = this;
				}
				else
				{
					result = this.ParentHandle.EnvironmentHandle;
				}
				return result;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0005BD84 File Offset: 0x0005B184
		internal OCI.HTYPE HandleType
		{
			get
			{
				return this._handleType;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0005BDA4 File Offset: 0x0005B1A4
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0005BDC4 File Offset: 0x0005B1C4
		internal bool IsUnicode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._isUnicode;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0005BDE4 File Offset: 0x0005B1E4
		internal OciHandle ParentHandle
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._parentHandle;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0005BE04 File Offset: 0x0005B204
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int AddRef()
		{
			return Interlocked.Increment(ref this._refCount);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0005BE24 File Offset: 0x0005B224
		internal void GetAttribute(OCI.ATTR attribute, out byte value, OciErrorHandle errorHandle)
		{
			uint num = 0U;
			int num2 = TracedNativeMethods.OCIAttrGet(this, out value, out num, attribute, errorHandle);
			if (num2 != 0)
			{
				OracleException.Check(errorHandle, num2);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0005BE54 File Offset: 0x0005B254
		internal void GetAttribute(OCI.ATTR attribute, out short value, OciErrorHandle errorHandle)
		{
			uint num = 0U;
			int num2 = TracedNativeMethods.OCIAttrGet(this, out value, out num, attribute, errorHandle);
			if (num2 != 0)
			{
				OracleException.Check(errorHandle, num2);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0005BE84 File Offset: 0x0005B284
		internal void GetAttribute(OCI.ATTR attribute, out int value, OciErrorHandle errorHandle)
		{
			uint num = 0U;
			int num2 = TracedNativeMethods.OCIAttrGet(this, out value, out num, attribute, errorHandle);
			if (num2 != 0)
			{
				OracleException.Check(errorHandle, num2);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0005BEB4 File Offset: 0x0005B2B4
		internal void GetAttribute(OCI.ATTR attribute, out string value, OciErrorHandle errorHandle, OracleConnection connection)
		{
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			int num2 = TracedNativeMethods.OCIAttrGet(this, ref zero, ref num, attribute, errorHandle);
			if (num2 != 0)
			{
				OracleException.Check(errorHandle, num2);
			}
			byte[] array = new byte[num];
			Marshal.Copy(zero, array, 0, checked((int)num));
			value = connection.GetString(array);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0005BF04 File Offset: 0x0005B304
		internal byte[] GetBytes(string value)
		{
			uint length = (uint)value.Length;
			byte[] array;
			if (this.IsUnicode)
			{
				array = new byte[(ulong)length * (ulong)((long)ADP.CharSize)];
				this.GetBytes(value.ToCharArray(), 0, length, array, 0);
			}
			else
			{
				byte[] array2 = new byte[length * 4U];
				uint bytes = this.GetBytes(value.ToCharArray(), 0, length, array2, 0);
				array = new byte[bytes];
				Buffer.BlockCopy(array2, 0, array, 0, checked((int)bytes));
			}
			return array;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0005BF74 File Offset: 0x0005B374
		internal uint GetBytes(char[] chars, int charIndex, uint charCount, byte[] bytes, int byteIndex)
		{
			uint num;
			if (this.IsUnicode)
			{
				num = checked((uint)((long)charCount * unchecked((long)ADP.CharSize)));
				Buffer.BlockCopy(chars, charIndex * ADP.CharSize, bytes, byteIndex, checked((int)num));
			}
			else
			{
				OciHandle environmentHandle = this.EnvironmentHandle;
				GCHandle gchandle = default(GCHandle);
				GCHandle gchandle2 = default(GCHandle);
				int num2;
				try
				{
					gchandle = GCHandle.Alloc(chars, GCHandleType.Pinned);
					IntPtr src = new IntPtr((long)gchandle.AddrOfPinnedObject() + (long)charIndex);
					IntPtr zero;
					if (bytes == null)
					{
						zero = IntPtr.Zero;
						num = 0U;
					}
					else
					{
						gchandle2 = GCHandle.Alloc(bytes, GCHandleType.Pinned);
						zero = new IntPtr((long)gchandle2.AddrOfPinnedObject() + (long)byteIndex);
						num = checked((uint)(bytes.Length - byteIndex));
					}
					num2 = UnsafeNativeMethods.OCIUnicodeToCharSet(environmentHandle, zero, num, src, charCount, out num);
				}
				finally
				{
					gchandle.Free();
					if (gchandle2.IsAllocated)
					{
						gchandle2.Free();
					}
				}
				if (num2 != 0)
				{
					throw ADP.OperationFailed("OCIUnicodeToCharSet", num2);
				}
			}
			return num;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0005C074 File Offset: 0x0005B474
		internal uint GetChars(byte[] bytes, int byteIndex, uint byteCount, char[] chars, int charIndex)
		{
			uint num;
			if (this.IsUnicode)
			{
				num = checked((uint)((long)byteCount / unchecked((long)ADP.CharSize)));
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * ADP.CharSize, checked((int)byteCount));
			}
			else
			{
				OciHandle environmentHandle = this.EnvironmentHandle;
				GCHandle gchandle = default(GCHandle);
				GCHandle gchandle2 = default(GCHandle);
				int num2;
				try
				{
					gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
					IntPtr src = new IntPtr((long)gchandle.AddrOfPinnedObject() + (long)byteIndex);
					IntPtr zero;
					if (chars == null)
					{
						zero = IntPtr.Zero;
						num = 0U;
					}
					else
					{
						gchandle2 = GCHandle.Alloc(chars, GCHandleType.Pinned);
						zero = new IntPtr((long)gchandle2.AddrOfPinnedObject() + (long)charIndex);
						num = checked((uint)(chars.Length - charIndex));
					}
					num2 = UnsafeNativeMethods.OCICharSetToUnicode(environmentHandle, zero, num, src, byteCount, out num);
				}
				finally
				{
					gchandle.Free();
					if (gchandle2.IsAllocated)
					{
						gchandle2.Free();
					}
				}
				if (num2 != 0)
				{
					throw ADP.OperationFailed("OCICharSetToUnicode", num2);
				}
			}
			return num;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0005C174 File Offset: 0x0005B574
		internal static string GetAttributeName(OciHandle handle, OCI.ATTR atype)
		{
			if (OCI.HTYPE.OCI_DTYPE_PARAM == handle.HandleType)
			{
				return ((OCI.PATTR)atype).ToString();
			}
			return atype.ToString();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0005C1A4 File Offset: 0x0005B5A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static IntPtr HandleValueToTrace(OciHandle handle)
		{
			return handle.DangerousGetHandle();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0005C1C4 File Offset: 0x0005B5C4
		internal string PtrToString(NativeBuffer buf)
		{
			string result;
			if (this.IsUnicode)
			{
				result = buf.PtrToStringUni(0);
			}
			else
			{
				result = buf.PtrToStringAnsi(0);
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0005C1F4 File Offset: 0x0005B5F4
		internal string PtrToString(IntPtr buf, int len)
		{
			string result;
			if (this.IsUnicode)
			{
				result = Marshal.PtrToStringUni(buf, len);
			}
			else
			{
				result = Marshal.PtrToStringAnsi(buf, len);
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0005C224 File Offset: 0x0005B624
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int Release()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			int num;
			try
			{
			}
			finally
			{
				num = Interlocked.Decrement(ref this._refCount);
				if (num == 0)
				{
					IntPtr intPtr = Interlocked.CompareExchange(ref this.handle, IntPtr.Zero, this.handle);
					if (IntPtr.Zero != intPtr)
					{
						OCI.HTYPE handleType = this.HandleType;
						OciHandle parentHandle = this.ParentHandle;
						OCI.HTYPE htype = handleType;
						int num2;
						if (htype <= OCI.HTYPE.OCI_DTYPE_FIRST)
						{
							switch (htype)
							{
							case OCI.HTYPE.OCI_HTYPE_ENV:
								num2 = TracedNativeMethods.OCIHandleFree(intPtr, handleType);
								if (num2 != 0)
								{
									throw ADP.OperationFailed("OCIHandleFree", num2);
								}
								goto IL_15E;
							case OCI.HTYPE.OCI_HTYPE_ERROR:
							case OCI.HTYPE.OCI_HTYPE_STMT:
							case OCI.HTYPE.OCI_HTYPE_SESSION:
								break;
							case OCI.HTYPE.OCI_HTYPE_SVCCTX:
							{
								OciHandle ociHandle = parentHandle;
								if (ociHandle != null)
								{
									OciHandle parentHandle2 = ociHandle.ParentHandle;
									if (parentHandle2 != null)
									{
										OciHandle parentHandle3 = parentHandle2.ParentHandle;
										if (parentHandle3 != null)
										{
											num2 = TracedNativeMethods.OCISessionEnd(intPtr, parentHandle3.DangerousGetHandle(), ociHandle.DangerousGetHandle(), OCI.MODE.OCI_DEFAULT);
										}
									}
								}
								break;
							}
							case OCI.HTYPE.OCI_HTYPE_BIND:
							case OCI.HTYPE.OCI_HTYPE_DEFINE:
							case OCI.HTYPE.OCI_HTYPE_DESCRIBE:
								goto IL_15E;
							case OCI.HTYPE.OCI_HTYPE_SERVER:
								TracedNativeMethods.OCIServerDetach(intPtr, parentHandle.DangerousGetHandle(), OCI.MODE.OCI_DEFAULT);
								break;
							default:
								if (htype != OCI.HTYPE.OCI_DTYPE_FIRST)
								{
									goto IL_15E;
								}
								goto IL_146;
							}
							num2 = TracedNativeMethods.OCIHandleFree(intPtr, handleType);
							if (num2 != 0)
							{
								throw ADP.OperationFailed("OCIHandleFree", num2);
							}
							goto IL_15E;
						}
						else
						{
							switch (htype)
							{
							case OCI.HTYPE.OCI_DTYPE_ROWID:
							case OCI.HTYPE.OCI_DTYPE_FILE:
								break;
							case OCI.HTYPE.OCI_DTYPE_COMPLEXOBJECTCOMP:
								goto IL_15E;
							default:
								if (htype != OCI.HTYPE.OCI_DTYPE_INTERVAL_DS)
								{
									switch (htype)
									{
									case OCI.HTYPE.OCI_DTYPE_TIMESTAMP:
									case OCI.HTYPE.OCI_DTYPE_TIMESTAMP_TZ:
									case OCI.HTYPE.OCI_DTYPE_TIMESTAMP_LTZ:
										break;
									default:
										goto IL_15E;
									}
								}
								break;
							}
						}
						IL_146:
						num2 = TracedNativeMethods.OCIDescriptorFree(intPtr, handleType);
						if (num2 != 0)
						{
							throw ADP.OperationFailed("OCIDescriptorFree", num2);
						}
						IL_15E:
						if (parentHandle != null)
						{
							parentHandle.Release();
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0005C3C4 File Offset: 0x0005B7C4
		protected override bool ReleaseHandle()
		{
			this.Release();
			return true;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0005C3E4 File Offset: 0x0005B7E4
		internal static void SafeDispose(ref OciHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0005C404 File Offset: 0x0005B804
		internal static void SafeDispose(ref OciEnvironmentHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0005C424 File Offset: 0x0005B824
		internal static void SafeDispose(ref OciErrorHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0005C444 File Offset: 0x0005B844
		internal static void SafeDispose(ref OciRowidDescriptor handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0005C464 File Offset: 0x0005B864
		internal static void SafeDispose(ref OciStatementHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0005C484 File Offset: 0x0005B884
		internal static void SafeDispose(ref OciSessionHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0005C4A4 File Offset: 0x0005B8A4
		internal static void SafeDispose(ref OciServiceContextHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0005C4C4 File Offset: 0x0005B8C4
		internal static void SafeDispose(ref OciServerHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0005C4E4 File Offset: 0x0005B8E4
		internal static void SafeDispose(ref OciDefineHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0005C504 File Offset: 0x0005B904
		internal static void SafeDispose(ref OciBindHandle handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0005C524 File Offset: 0x0005B924
		internal static void SafeDispose(ref OciParameterDescriptor handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0005C544 File Offset: 0x0005B944
		internal static void SafeDispose(ref OciDateTimeDescriptor handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0005C564 File Offset: 0x0005B964
		internal void SetAttribute(OCI.ATTR attribute, int value, OciErrorHandle errorHandle)
		{
			int num = TracedNativeMethods.OCIAttrSet(this, ref value, 0U, attribute, errorHandle);
			if (num != 0)
			{
				OracleException.Check(errorHandle, num);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0005C594 File Offset: 0x0005B994
		internal void SetAttribute(OCI.ATTR attribute, OciHandle value, OciErrorHandle errorHandle)
		{
			int num = TracedNativeMethods.OCIAttrSet(this, value, 0U, attribute, errorHandle);
			if (num != 0)
			{
				OracleException.Check(errorHandle, num);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0005C5C4 File Offset: 0x0005B9C4
		internal void SetAttribute(OCI.ATTR attribute, string value, OciErrorHandle errorHandle)
		{
			uint length = (uint)value.Length;
			byte[] array = new byte[length * 4U];
			uint bytes = this.GetBytes(value.ToCharArray(), 0, length, array, 0);
			int num = TracedNativeMethods.OCIAttrSet(this, array, bytes, attribute, errorHandle);
			if (num != 0)
			{
				OracleException.Check(errorHandle, num);
			}
		}

		// Token: 0x0400031C RID: 796
		private OCI.HTYPE _handleType;

		// Token: 0x0400031D RID: 797
		private int _refCount;

		// Token: 0x0400031E RID: 798
		private OciHandle _parentHandle;

		// Token: 0x0400031F RID: 799
		private bool _isUnicode;

		// Token: 0x02000038 RID: 56
		[Flags]
		protected enum HANDLEFLAG
		{
			// Token: 0x04000321 RID: 801
			DEFAULT = 0,
			// Token: 0x04000322 RID: 802
			UNICODE = 1,
			// Token: 0x04000323 RID: 803
			NLS = 2
		}
	}
}
