using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001C6 RID: 454
	internal class SSPISecureChannelType : SSPIInterface
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0006042C File Offset: 0x0005E62C
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x00060435 File Offset: 0x0005E635
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

		// Token: 0x060011F8 RID: 4600 RVA: 0x0006043F File Offset: 0x0005E63F
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			return SafeFreeContextBuffer.EnumeratePackages(SSPISecureChannelType.Library, out pkgnum, out pkgArray);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0006044D File Offset: 0x0005E64D
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0006045E File Offset: 0x0005E65E
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0006046A File Offset: 0x0005E66A
		public int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(SSPISecureChannelType.Library, moduleName, usage, out outCredential);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00060479 File Offset: 0x0005E679
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0006048A File Offset: 0x0005E68A
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPISecureChannelType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0006049C File Offset: 0x0005E69C
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000604C0 File Offset: 0x0005E6C0
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000604E4 File Offset: 0x0005E6E4
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0006050C File Offset: 0x0005E70C
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPISecureChannelType.Library, ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00060534 File Offset: 0x0005E734
		public int EncryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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

		// Token: 0x06001203 RID: 4611 RVA: 0x000605A8 File Offset: 0x0005E7A8
		public int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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

		// Token: 0x06001204 RID: 4612 RVA: 0x0006061C File Offset: 0x0005E81C
		public int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			throw ExceptionHelper.MethodNotSupportedException;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00060623 File Offset: 0x0005E823
		public int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			throw ExceptionHelper.MethodNotSupportedException;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0006062C File Offset: 0x0005E82C
		public unsafe int QueryContextChannelBinding(SafeDeleteContext phContext, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding refHandle)
		{
			refHandle = SafeFreeContextBufferChannelBinding.CreateEmptyHandle(SSPISecureChannelType.Library);
			Bindings bindings = default(Bindings);
			return SafeFreeContextBufferChannelBinding.QueryContextChannelBinding(SSPISecureChannelType.Library, phContext, attribute, &bindings, refHandle);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00060660 File Offset: 0x0005E860
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
					if (!(handleType == typeof(SafeFreeCertContext)))
					{
						throw new ArgumentException(SR.GetString("SSPIInvalidHandleType", new object[]
						{
							handleType.FullName
						}), "handleType");
					}
					refHandle = new SafeFreeCertContext();
				}
			}
			byte* buffer2;
			if (buffer == null || buffer.Length == 0)
			{
				buffer2 = null;
			}
			else
			{
				buffer2 = &buffer[0];
			}
			return SafeFreeContextBuffer.QueryContextAttributes(SSPISecureChannelType.Library, phContext, attribute, buffer2, refHandle);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00060707 File Offset: 0x0005E907
		public int SetContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer)
		{
			return SafeFreeContextBuffer.SetContextAttributes(SSPISecureChannelType.Library, phContext, attribute, buffer);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00060716 File Offset: 0x0005E916
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0006071D File Offset: 0x0005E91D
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00060724 File Offset: 0x0005E924
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.ApplyControlToken(SSPISecureChannelType.Library, ref refContext, inputBuffers);
		}

		// Token: 0x04001480 RID: 5248
		private static readonly SecurDll Library;

		// Token: 0x04001481 RID: 5249
		private static volatile SecurityPackageInfoClass[] m_SecurityPackages;
	}
}
