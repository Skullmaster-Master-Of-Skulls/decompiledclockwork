using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020004EF RID: 1263
	internal class SSPISecureChannelType : SSPIInterface
	{
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000A25EC File Offset: 0x000A15EC
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x000A25F3 File Offset: 0x000A15F3
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPISecureChannelType.m_SecurityPackages;
			}
			set
			{
				SSPISecureChannelType.m_SecurityPackages = value;
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000A25FB File Offset: 0x000A15FB
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			return SafeFreeContextBuffer.EnumeratePackages(SSPISecureChannelType.Library, out pkgnum, out pkgArray);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000A2609 File Offset: 0x000A1609
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000A261A File Offset: 0x000A161A
		public int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(SSPISecureChannelType.Library, moduleName, usage, out outCredential);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000A2629 File Offset: 0x000A1629
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000A263C File Offset: 0x000A163C
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000A2660 File Offset: 0x000A1660
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000A2684 File Offset: 0x000A1684
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000A26AC File Offset: 0x000A16AC
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000A26D4 File Offset: 0x000A16D4
		private int EncryptMessageHelper9x(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
			catch
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					result = UnsafeNclNativeMethods.NativeSSLWin9xSSPI.SealMessage(ref context._handle, 0U, inputOutput, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000A2764 File Offset: 0x000A1764
		private int EncryptMessageHelper(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
			catch
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					result = UnsafeNclNativeMethods.NativeNTSSPI.EncryptMessage(ref context._handle, 0U, inputOutput, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000A27F4 File Offset: 0x000A17F4
		public int EncryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			if (ComNetOS.IsWin9x)
			{
				return this.EncryptMessageHelper9x(context, inputOutput, sequenceNumber);
			}
			return this.EncryptMessageHelper(context, inputOutput, sequenceNumber);
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000A2810 File Offset: 0x000A1810
		private int DecryptMessageHelper9x(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
			catch
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					result = UnsafeNclNativeMethods.NativeSSLWin9xSSPI.UnsealMessage(ref context._handle, inputOutput, IntPtr.Zero, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000A28A4 File Offset: 0x000A18A4
		private int DecryptMessageHelper(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
			catch
			{
				if (flag)
				{
					context.DangerousRelease();
					flag = false;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					result = UnsafeNclNativeMethods.NativeNTSSPI.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, null);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000A2934 File Offset: 0x000A1934
		public int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			if (ComNetOS.IsWin9x)
			{
				return this.DecryptMessageHelper9x(context, inputOutput, sequenceNumber);
			}
			return this.DecryptMessageHelper(context, inputOutput, sequenceNumber);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000A2950 File Offset: 0x000A1950
		public int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			throw ExceptionHelper.MethodNotSupportedException;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000A2957 File Offset: 0x000A1957
		public int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			throw ExceptionHelper.MethodNotSupportedException;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000A2960 File Offset: 0x000A1960
		public unsafe int QueryContextChannelBinding(SafeDeleteContext phContext, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle)
		{
			refHandle = SafeFreeContextBufferChannelBinding.CreateEmptyHandle(SSPISecureChannelType.Library);
			Bindings bindings = default(Bindings);
			return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding(SSPISecureChannelType.Library, phContext, attribute, &bindings, refHandle);
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000A2994 File Offset: 0x000A1994
		public unsafe int QueryContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle(SSPISecureChannelType.Library);
				}
				else
				{
					if (handleType != typeof(SafeFreeCertContext))
					{
						throw new ArgumentException(SR.GetString("SSPIInvalidHandleType", new object[]
						{
							handleType.FullName
						}), "handleType");
					}
					refHandle = new SafeFreeCertContext();
				}
			}
			fixed (byte* ptr = buffer)
			{
				return SafeFreeContextBuffer.QueryContextAttributes(SSPISecureChannelType.Library, phContext, attribute, ptr, refHandle);
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000A2A31 File Offset: 0x000A1A31
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000A2A38 File Offset: 0x000A1A38
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040026C3 RID: 9923
		private static readonly SecurDll Library = ComNetOS.IsWin9x ? SecurDll.SCHANNEL : SecurDll.SECURITY;

		// Token: 0x040026C4 RID: 9924
		private static SecurityPackageInfoClass[] m_SecurityPackages;
	}
}
