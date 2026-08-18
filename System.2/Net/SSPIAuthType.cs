using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001C7 RID: 455
	internal class SSPIAuthType : SSPIInterface
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0006073A File Offset: 0x0005E93A
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00060743 File Offset: 0x0005E943
		public SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SSPIAuthType.m_SecurityPackages;
			}
			set
			{
				SSPIAuthType.m_SecurityPackages = value;
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0006074D File Offset: 0x0005E94D
		public int EnumerateSecurityPackages(out int pkgnum, out SafeFreeContextBuffer pkgArray)
		{
			return SafeFreeContextBuffer.EnumeratePackages(SSPIAuthType.Library, out pkgnum, out pkgArray);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0006075B File Offset: 0x0005E95B
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref AuthIdentity authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPIAuthType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0006076C File Offset: 0x0005E96C
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00060778 File Offset: 0x0005E978
		public int AcquireDefaultCredential(string moduleName, CredentialUse usage, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireDefaultCredential(SSPIAuthType.Library, moduleName, usage, out outCredential);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00060787 File Offset: 0x0005E987
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPIAuthType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00060798 File Offset: 0x0005E998
		public int AcquireCredentialsHandle(string moduleName, CredentialUse usage, ref SecureCredential2 authdata, out SafeFreeCredentials outCredential)
		{
			return SafeFreeCredentials.AcquireCredentialsHandle(SSPIAuthType.Library, moduleName, usage, ref authdata, out outCredential);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000607AC File Offset: 0x0005E9AC
		public int AcceptSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer inputBuffer, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPIAuthType.Library, ref credential, ref context, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000607D0 File Offset: 0x0005E9D0
		public int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers, ContextFlags inFlags, Endianness endianness, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(SSPIAuthType.Library, ref credential, ref context, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000607F4 File Offset: 0x0005E9F4
		public int InitializeSecurityContext(ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPIAuthType.Library, ref credential, ref context, targetName, inFlags, endianness, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0006081C File Offset: 0x0005EA1C
		public int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness endianness, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(SSPIAuthType.Library, ref credential, ref context, targetName, inFlags, endianness, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00060844 File Offset: 0x0005EA44
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

		// Token: 0x0600121A RID: 4634 RVA: 0x000608B8 File Offset: 0x0005EAB8
		public unsafe int DecryptMessage(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
					num = UnsafeNclNativeMethods.NativeNTSSPI.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, &num2);
					context.DangerousRelease();
				}
			}
			if (num == 0 && num2 == 2147483649U)
			{
				throw new InvalidOperationException(SR.GetString("net_auth_message_not_encrypted"));
			}
			return num;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0006094C File Offset: 0x0005EB4C
		public int MakeSignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
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
					result = UnsafeNclNativeMethods.NativeNTSSPI.EncryptMessage(ref context._handle, 2147483649U, inputOutput, sequenceNumber);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000609C4 File Offset: 0x0005EBC4
		public unsafe int VerifySignature(SafeDeleteContext context, SecurityBufferDescriptor inputOutput, uint sequenceNumber)
		{
			int result = -2146893055;
			bool flag = false;
			uint num = 0U;
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
					result = UnsafeNclNativeMethods.NativeNTSSPI.DecryptMessage(ref context._handle, inputOutput, sequenceNumber, &num);
					context.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00060A3C File Offset: 0x0005EC3C
		public int QueryContextChannelBinding(SafeDeleteContext context, ContextAttribute attribute, out SafeFreeContextBufferChannelBinding binding)
		{
			binding = null;
			throw new NotSupportedException();
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00060A48 File Offset: 0x0005EC48
		public unsafe int QueryContextAttributes(SafeDeleteContext context, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle(SSPIAuthType.Library);
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
			return SafeFreeContextBuffer.QueryContextAttributes(SSPIAuthType.Library, context, attribute, buffer2, refHandle);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00060AEF File Offset: 0x0005ECEF
		public int SetContextAttributes(SafeDeleteContext context, ContextAttribute attribute, byte[] buffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00060AF6 File Offset: 0x0005ECF6
		public int QuerySecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle phToken)
		{
			return SSPIAuthType.GetSecurityContextToken(phContext, out phToken);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00060AFF File Offset: 0x0005ECFF
		public int CompleteAuthToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			return SafeDeleteContext.CompleteAuthToken(SSPIAuthType.Library, ref refContext, inputBuffers);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00060B0D File Offset: 0x0005ED0D
		public int ApplyControlToken(ref SafeDeleteContext refContext, SecurityBuffer[] inputBuffers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00060B14 File Offset: 0x0005ED14
		private static int GetSecurityContextToken(SafeDeleteContext phContext, out SafeCloseHandle safeHandle)
		{
			int result = -2146893055;
			bool flag = false;
			safeHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				phContext.DangerousAddRef(ref flag);
			}
			catch (Exception ex)
			{
				if (flag)
				{
					phContext.DangerousRelease();
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
					result = UnsafeNclNativeMethods.SafeNetHandles.QuerySecurityContextToken(ref phContext._handle, out safeHandle);
					phContext.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x04001482 RID: 5250
		private static readonly SecurDll Library;

		// Token: 0x04001483 RID: 5251
		private static volatile SecurityPackageInfoClass[] m_SecurityPackages;
	}
}
