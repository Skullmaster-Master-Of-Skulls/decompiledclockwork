using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IdentityModel
{
	// Token: 0x02000095 RID: 149
	internal class SafeDeleteContext : SafeHandle
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00012058 File Offset: 0x00010258
		protected SafeDeleteContext() : base(IntPtr.Zero, true)
		{
			this._handle = default(SSPIHandle);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00012072 File Offset: 0x00010272
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001208C File Offset: 0x0001028C
		internal unsafe static int InitializeSecurityContext(SafeFreeCredentials inCredentials, ref SafeDeleteContext refContext, string targetName, SspiContextFlags inFlags, Endianness endianness, SecurityBuffer inSecBuffer, SecurityBuffer[] inSecBuffers, SecurityBuffer outSecBuffer, ref SspiContextFlags outFlags)
		{
			if (inCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inCredentials");
			}
			SecurityBufferDescriptor securityBufferDescriptor = null;
			if (inSecBuffer != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(1);
			}
			else if (inSecBuffers != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(inSecBuffers.Length);
			}
			SecurityBufferDescriptor securityBufferDescriptor2 = new SecurityBufferDescriptor(1);
			bool flag = (inFlags & SspiContextFlags.AllocateMemory) != SspiContextFlags.Zero;
			int result = -1;
			SSPIHandle sspihandle = default(SSPIHandle);
			if (refContext != null)
			{
				sspihandle = refContext._handle;
			}
			GCHandle[] array = null;
			GCHandle gchandle = default(GCHandle);
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				gchandle = GCHandle.Alloc(outSecBuffer.token, GCHandleType.Pinned);
				SecurityBufferStruct[] array2 = new SecurityBufferStruct[(securityBufferDescriptor == null) ? 1 : securityBufferDescriptor.Count];
				try
				{
					SecurityBufferStruct[] array3;
					void* unmanagedPointer;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						unmanagedPointer = null;
					}
					else
					{
						unmanagedPointer = (void*)(&array3[0]);
					}
					if (securityBufferDescriptor != null)
					{
						securityBufferDescriptor.UnmanagedPointer = unmanagedPointer;
						array = new GCHandle[securityBufferDescriptor.Count];
						for (int i = 0; i < securityBufferDescriptor.Count; i++)
						{
							SecurityBuffer securityBuffer = (inSecBuffer != null) ? inSecBuffer : inSecBuffers[i];
							if (securityBuffer != null)
							{
								array2[i].count = securityBuffer.size;
								array2[i].type = securityBuffer.type;
								if (securityBuffer.unmanagedToken != null)
								{
									array2[i].token = securityBuffer.unmanagedToken.DangerousGetHandle();
								}
								else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
								{
									array2[i].token = IntPtr.Zero;
								}
								else
								{
									array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
									array2[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
								}
							}
						}
					}
					SecurityBufferStruct[] array4 = new SecurityBufferStruct[1];
					try
					{
						SecurityBufferStruct[] array5;
						void* unmanagedPointer2;
						if ((array5 = array4) == null || array5.Length == 0)
						{
							unmanagedPointer2 = null;
						}
						else
						{
							unmanagedPointer2 = (void*)(&array5[0]);
						}
						securityBufferDescriptor2.UnmanagedPointer = unmanagedPointer2;
						array4[0].count = outSecBuffer.size;
						array4[0].type = outSecBuffer.type;
						if (outSecBuffer.token == null || outSecBuffer.token.Length == 0)
						{
							array4[0].token = IntPtr.Zero;
						}
						else
						{
							array4[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outSecBuffer.token, outSecBuffer.offset);
						}
						if (flag)
						{
							safeFreeContextBuffer = SafeFreeContextBuffer.CreateEmptyHandle();
						}
						if (refContext == null || refContext.IsInvalid)
						{
							refContext = new SafeDeleteContext();
						}
						if (targetName == null || targetName.Length == 0)
						{
							targetName = " ";
						}
						try
						{
							fixed (string text = targetName)
							{
								char* ptr = text;
								if (ptr != null)
								{
									ptr += RuntimeHelpers.OffsetToStringData / 2;
								}
								result = SafeDeleteContext.MustRunInitializeSecurityContext(inCredentials, sspihandle.IsZero ? null : ((void*)(&sspihandle)), (byte*)((targetName == " ") ? null : ptr), inFlags, endianness, securityBufferDescriptor, refContext, securityBufferDescriptor2, ref outFlags, safeFreeContextBuffer);
							}
						}
						finally
						{
							string text = null;
						}
						outSecBuffer.size = array4[0].count;
						outSecBuffer.type = array4[0].type;
						if (outSecBuffer.size > 0)
						{
							outSecBuffer.token = DiagnosticUtility.Utility.AllocateByteArray(outSecBuffer.size);
							Marshal.Copy(array4[0].token, outSecBuffer.token, 0, outSecBuffer.size);
						}
						else
						{
							outSecBuffer.token = null;
						}
					}
					finally
					{
						SecurityBufferStruct[] array5 = null;
					}
				}
				finally
				{
					SecurityBufferStruct[] array3 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Close();
				}
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001248C File Offset: 0x0001068C
		private unsafe static int MustRunInitializeSecurityContext(SafeFreeCredentials inCredentials, void* inContextPtr, byte* targetName, SspiContextFlags inFlags, Endianness endianness, SecurityBufferDescriptor inputBuffer, SafeDeleteContext outContext, SecurityBufferDescriptor outputBuffer, ref SspiContextFlags attributes, SafeFreeContextBuffer handleTemplate)
		{
			int num = -1;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				inCredentials.DangerousAddRef(ref flag);
				outContext.DangerousAddRef(ref flag2);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					inCredentials.DangerousRelease();
					flag = false;
				}
				if (flag2)
				{
					outContext.DangerousRelease();
					flag2 = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					inCredentials = null;
				}
				else if (flag && flag2)
				{
					SSPIHandle handle = inCredentials._handle;
					long num2;
					num = SafeDeleteContext.InitializeSecurityContextW(ref handle, inContextPtr, targetName, inFlags, 0, endianness, inputBuffer, 0, ref outContext._handle, outputBuffer, ref attributes, out num2);
					if (outContext._EffectiveCredential != inCredentials && ((long)num & (long)((ulong)-2147483648)) == 0L)
					{
						if (outContext._EffectiveCredential != null)
						{
							outContext._EffectiveCredential.DangerousRelease();
						}
						outContext._EffectiveCredential = inCredentials;
					}
					else
					{
						inCredentials.DangerousRelease();
					}
					outContext.DangerousRelease();
					if (handleTemplate != null)
					{
						handleTemplate.Set(((SecurityBufferStruct*)outputBuffer.UnmanagedPointer)->token);
						if (handleTemplate.IsInvalid)
						{
							handleTemplate.SetHandleAsInvalid();
						}
					}
				}
				if (inContextPtr == null && ((long)num & (long)((ulong)-2147483648)) != 0L)
				{
					outContext._handle.SetToInvalid();
				}
			}
			return num;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000125C8 File Offset: 0x000107C8
		internal unsafe static int AcceptSecurityContext(SafeFreeCredentials inCredentials, ref SafeDeleteContext refContext, SspiContextFlags inFlags, Endianness endianness, SecurityBuffer inSecBuffer, SecurityBuffer[] inSecBuffers, SecurityBuffer outSecBuffer, ref SspiContextFlags outFlags)
		{
			if (inCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inCredentials");
			}
			SecurityBufferDescriptor securityBufferDescriptor = null;
			if (inSecBuffer != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(1);
			}
			else if (inSecBuffers != null)
			{
				securityBufferDescriptor = new SecurityBufferDescriptor(inSecBuffers.Length);
			}
			SecurityBufferDescriptor securityBufferDescriptor2 = new SecurityBufferDescriptor(1);
			bool flag = (inFlags & SspiContextFlags.AllocateMemory) != SspiContextFlags.Zero;
			int result = -1;
			SSPIHandle sspihandle = default(SSPIHandle);
			if (refContext != null)
			{
				sspihandle = refContext._handle;
			}
			GCHandle[] array = null;
			GCHandle gchandle = default(GCHandle);
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				gchandle = GCHandle.Alloc(outSecBuffer.token, GCHandleType.Pinned);
				SecurityBufferStruct[] array2 = new SecurityBufferStruct[(securityBufferDescriptor == null) ? 1 : securityBufferDescriptor.Count];
				try
				{
					SecurityBufferStruct[] array3;
					void* unmanagedPointer;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						unmanagedPointer = null;
					}
					else
					{
						unmanagedPointer = (void*)(&array3[0]);
					}
					if (securityBufferDescriptor != null)
					{
						securityBufferDescriptor.UnmanagedPointer = unmanagedPointer;
						array = new GCHandle[securityBufferDescriptor.Count];
						for (int i = 0; i < securityBufferDescriptor.Count; i++)
						{
							SecurityBuffer securityBuffer = (inSecBuffer != null) ? inSecBuffer : inSecBuffers[i];
							if (securityBuffer != null)
							{
								array2[i].count = securityBuffer.size;
								array2[i].type = securityBuffer.type;
								if (securityBuffer.unmanagedToken != null)
								{
									array2[i].token = securityBuffer.unmanagedToken.DangerousGetHandle();
								}
								else if (securityBuffer.token == null || securityBuffer.token.Length == 0)
								{
									array2[i].token = IntPtr.Zero;
								}
								else
								{
									array[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
									array2[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
								}
							}
						}
					}
					SecurityBufferStruct[] array4 = new SecurityBufferStruct[1];
					try
					{
						SecurityBufferStruct[] array5;
						void* unmanagedPointer2;
						if ((array5 = array4) == null || array5.Length == 0)
						{
							unmanagedPointer2 = null;
						}
						else
						{
							unmanagedPointer2 = (void*)(&array5[0]);
						}
						securityBufferDescriptor2.UnmanagedPointer = unmanagedPointer2;
						array4[0].count = outSecBuffer.size;
						array4[0].type = outSecBuffer.type;
						if (outSecBuffer.token == null || outSecBuffer.token.Length == 0)
						{
							array4[0].token = IntPtr.Zero;
						}
						else
						{
							array4[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outSecBuffer.token, outSecBuffer.offset);
						}
						if (flag)
						{
							safeFreeContextBuffer = SafeFreeContextBuffer.CreateEmptyHandle();
						}
						if (refContext == null || refContext.IsInvalid)
						{
							refContext = new SafeDeleteContext();
						}
						result = SafeDeleteContext.MustRunAcceptSecurityContext(inCredentials, sspihandle.IsZero ? null : ((void*)(&sspihandle)), securityBufferDescriptor, inFlags, endianness, refContext, securityBufferDescriptor2, ref outFlags, safeFreeContextBuffer);
						outSecBuffer.size = array4[0].count;
						outSecBuffer.type = array4[0].type;
						if (outSecBuffer.size > 0)
						{
							outSecBuffer.token = DiagnosticUtility.Utility.AllocateByteArray(outSecBuffer.size);
							Marshal.Copy(array4[0].token, outSecBuffer.token, 0, outSecBuffer.size);
						}
						else
						{
							outSecBuffer.token = null;
						}
					}
					finally
					{
						SecurityBufferStruct[] array5 = null;
					}
				}
				finally
				{
					SecurityBufferStruct[] array3 = null;
				}
			}
			finally
			{
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].IsAllocated)
						{
							array[j].Free();
						}
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Close();
				}
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012974 File Offset: 0x00010B74
		private unsafe static int MustRunAcceptSecurityContext(SafeFreeCredentials inCredentials, void* inContextPtr, SecurityBufferDescriptor inputBuffer, SspiContextFlags inFlags, Endianness endianness, SafeDeleteContext outContext, SecurityBufferDescriptor outputBuffer, ref SspiContextFlags outFlags, SafeFreeContextBuffer handleTemplate)
		{
			int num = -1;
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				inCredentials.DangerousAddRef(ref flag);
				outContext.DangerousAddRef(ref flag2);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					inCredentials.DangerousRelease();
					flag = false;
				}
				if (flag2)
				{
					outContext.DangerousRelease();
					flag2 = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					inCredentials = null;
				}
				else if (flag && flag2)
				{
					SSPIHandle handle = inCredentials._handle;
					long num2;
					num = SafeDeleteContext.AcceptSecurityContext(ref handle, inContextPtr, inputBuffer, inFlags, endianness, ref outContext._handle, outputBuffer, ref outFlags, out num2);
					if (outContext._EffectiveCredential != inCredentials && ((long)num & (long)((ulong)-2147483648)) == 0L)
					{
						if (outContext._EffectiveCredential != null)
						{
							outContext._EffectiveCredential.DangerousRelease();
						}
						outContext._EffectiveCredential = inCredentials;
					}
					else
					{
						inCredentials.DangerousRelease();
					}
					outContext.DangerousRelease();
					if (handleTemplate != null)
					{
						handleTemplate.Set(((SecurityBufferStruct*)outputBuffer.UnmanagedPointer)->token);
						if (handleTemplate.IsInvalid)
						{
							handleTemplate.SetHandleAsInvalid();
						}
					}
					if (inContextPtr == null && ((long)num & (long)((ulong)-2147483648)) != 0L)
					{
						outContext._handle.SetToInvalid();
					}
				}
			}
			return num;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00012AAC File Offset: 0x00010CAC
		public static int ImpersonateSecurityContext(SafeDeleteContext context)
		{
			int result = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					result = SafeDeleteContext.ImpersonateSecurityContext(ref context._handle);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012B28 File Offset: 0x00010D28
		public static int EncryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int result = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					result = SafeDeleteContext.EncryptMessage(ref context._handle, 0U, inputOutput, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public unsafe static int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int num = -2146893055;
			bool flag = false;
			uint num2 = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				context.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					num = SafeDeleteContext.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, &num2);
					context.DangerousRelease();
				}
			}
			if (num == 0 && num2 == 2147483649U)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SspiPayloadNotEncrypted")));
			}
			return num;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00012C4C File Offset: 0x00010E4C
		internal int GetSecurityContextToken(out SafeCloseHandle safeHandle)
		{
			int result = -2146893055;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (flag)
				{
					base.DangerousRelease();
					flag = false;
				}
				if (!(ex is ObjectDisposedException))
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					result = SafeDeleteContext.QuerySecurityContextToken(ref this._handle, out safeHandle);
					base.DangerousRelease();
				}
				else
				{
					safeHandle = new SafeCloseHandle(IntPtr.Zero, false);
				}
			}
			return result;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00012CD8 File Offset: 0x00010ED8
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return SafeDeleteContext.DeleteSecurityContext(ref this._handle) == 0;
		}

		// Token: 0x060004E8 RID: 1256
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		private static extern int QuerySecurityContextToken(ref SSPIHandle phContext, out SafeCloseHandle handle);

		// Token: 0x060004E9 RID: 1257
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int InitializeSecurityContextW(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] byte* targetName, [In] SspiContextFlags inFlags, [In] int reservedI, [In] Endianness endianness, [In] SecurityBufferDescriptor inputBuffer, [In] int reservedII, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref SspiContextFlags attributes, out long timestamp);

		// Token: 0x060004EA RID: 1258
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int AcceptSecurityContext(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] SecurityBufferDescriptor inputBuffer, [In] SspiContextFlags inFlags, [In] Endianness endianness, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref SspiContextFlags attributes, out long timestamp);

		// Token: 0x060004EB RID: 1259
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int DeleteSecurityContext(ref SSPIHandle handlePtr);

		// Token: 0x060004EC RID: 1260
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int ImpersonateSecurityContext(ref SSPIHandle handlePtr);

		// Token: 0x060004ED RID: 1261
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal static extern int EncryptMessage(ref SSPIHandle contextHandle, [In] uint qualityOfProtection, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber);

		// Token: 0x060004EE RID: 1262
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("security.Dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int DecryptMessage(ref SSPIHandle contextHandle, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber, uint* qualityOfProtection);

		// Token: 0x04000454 RID: 1108
		private const string SECURITY = "security.Dll";

		// Token: 0x04000455 RID: 1109
		private const string dummyStr = " ";

		// Token: 0x04000456 RID: 1110
		private static readonly byte[] dummyBytes = new byte[1];

		// Token: 0x04000457 RID: 1111
		internal SSPIHandle _handle;

		// Token: 0x04000458 RID: 1112
		private SafeFreeCredentials _EffectiveCredential;
	}
}
