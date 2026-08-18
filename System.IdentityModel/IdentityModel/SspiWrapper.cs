using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A8 RID: 168
	internal static class SspiWrapper
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00013519 File Offset: 0x00011719
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00013520 File Offset: 0x00011720
		public static SecurityPackageInfoClass[] SecurityPackages
		{
			get
			{
				return SspiWrapper.securityPackages;
			}
			set
			{
				SspiWrapper.securityPackages = value;
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00013528 File Offset: 0x00011728
		private static SecurityPackageInfoClass[] EnumerateSecurityPackages()
		{
			if (SspiWrapper.SecurityPackages != null)
			{
				return SspiWrapper.SecurityPackages;
			}
			int num = 0;
			SafeFreeContextBuffer safeFreeContextBuffer = null;
			try
			{
				int num2 = SafeFreeContextBuffer.EnumeratePackages(out num, out safeFreeContextBuffer);
				if (num2 != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num2));
				}
				SecurityPackageInfoClass[] array = new SecurityPackageInfoClass[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = new SecurityPackageInfoClass(safeFreeContextBuffer, i);
				}
				SspiWrapper.SecurityPackages = array;
			}
			finally
			{
				if (safeFreeContextBuffer != null)
				{
					safeFreeContextBuffer.Close();
				}
			}
			return SspiWrapper.SecurityPackages;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000135B0 File Offset: 0x000117B0
		public static SecurityPackageInfoClass GetVerifyPackageInfo(string packageName)
		{
			SecurityPackageInfoClass[] array = SspiWrapper.EnumerateSecurityPackages();
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (string.Compare(array[i].Name, packageName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return array[i];
					}
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SSPIPackageNotSupported", new object[]
			{
				packageName
			})));
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001360C File Offset: 0x0001180C
		public static bool IsNegotiateExPackagePresent()
		{
			SecurityPackageInfoClass[] array = SspiWrapper.EnumerateSecurityPackages();
			if (array != null)
			{
				int num = 2097152;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i].Capabilities & num) != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013648 File Offset: 0x00011848
		public static SafeFreeCredentials AcquireDefaultCredential(string package, CredentialUse intent, params string[] additionalPackages)
		{
			SafeFreeCredentials result = null;
			AuthIdentityEx authIdentityEx = new AuthIdentityEx(null, null, null, additionalPackages);
			int num = SafeFreeCredentials.AcquireDefaultCredential(package, intent, ref authIdentityEx, out result);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			return result;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00013684 File Offset: 0x00011884
		public static SafeFreeCredentials AcquireCredentialsHandle(string package, CredentialUse intent, ref AuthIdentityEx authdata)
		{
			SafeFreeCredentials result = null;
			int num = SafeFreeCredentials.AcquireCredentialsHandle(package, intent, ref authdata, out result);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			return result;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000136B4 File Offset: 0x000118B4
		public static SafeFreeCredentials AcquireCredentialsHandle(string package, CredentialUse intent, SecureCredential scc)
		{
			SafeFreeCredentials result = null;
			int num = SafeFreeCredentials.AcquireCredentialsHandle(package, intent, ref scc, out result);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			return result;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000136E4 File Offset: 0x000118E4
		public static SafeFreeCredentials AcquireCredentialsHandle(string package, CredentialUse intent, ref IntPtr ppAuthIdentity)
		{
			SafeFreeCredentials result = null;
			int num = SafeFreeCredentials.AcquireCredentialsHandle(package, intent, ref ppAuthIdentity, out result);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			return result;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00013714 File Offset: 0x00011914
		internal static int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, SspiContextFlags inFlags, Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref SspiContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(credential, ref context, targetName, inFlags, datarep, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00013734 File Offset: 0x00011934
		internal static int InitializeSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, SspiContextFlags inFlags, Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref SspiContextFlags outFlags)
		{
			return SafeDeleteContext.InitializeSecurityContext(credential, ref context, targetName, inFlags, datarep, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00013753 File Offset: 0x00011953
		internal static int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext refContext, SspiContextFlags inFlags, Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref SspiContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(credential, ref refContext, inFlags, datarep, inputBuffer, null, outputBuffer, ref outFlags);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013765 File Offset: 0x00011965
		internal static int AcceptSecurityContext(SafeFreeCredentials credential, ref SafeDeleteContext refContext, SspiContextFlags inFlags, Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref SspiContextFlags outFlags)
		{
			return SafeDeleteContext.AcceptSecurityContext(credential, ref refContext, inFlags, datarep, null, inputBuffers, outputBuffer, ref outFlags);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013777 File Offset: 0x00011977
		public static int QuerySecurityContextToken(SafeDeleteContext context, out SafeCloseHandle token)
		{
			return context.GetSecurityContextToken(out token);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013780 File Offset: 0x00011980
		private unsafe static int QueryContextAttributes(SafeDeleteContext phContext, ContextAttribute attribute, byte[] buffer, Type handleType, out SafeHandle refHandle)
		{
			refHandle = null;
			if (handleType != null)
			{
				if (handleType == typeof(SafeFreeContextBuffer))
				{
					refHandle = SafeFreeContextBuffer.CreateEmptyHandle();
				}
				else
				{
					if (!(handleType == typeof(SafeFreeCertContext)))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("handleType", SR.GetString("ValueMustBeOf2Types", new object[]
						{
							typeof(SafeFreeContextBuffer).ToString(),
							typeof(SafeFreeCertContext).ToString()
						})));
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
			return SafeFreeContextBuffer.QueryContextAttributes(phContext, attribute, buffer2, refHandle);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00013844 File Offset: 0x00011A44
		public unsafe static object QueryContextAttributes(SafeDeleteContext securityContext, ContextAttribute contextAttribute)
		{
			int num = IntPtr.Size;
			Type handleType = null;
			if (contextAttribute <= ContextAttribute.RemoteCertificate)
			{
				switch (contextAttribute)
				{
				case ContextAttribute.Sizes:
					num = SecSizes.SizeOf;
					goto IL_122;
				case ContextAttribute.Names:
					handleType = typeof(SafeFreeContextBuffer);
					goto IL_122;
				case ContextAttribute.Lifespan:
					num = LifeSpan_Struct.Size;
					goto IL_122;
				case ContextAttribute.DceInfo:
				case (ContextAttribute)5:
				case ContextAttribute.Authority:
				case (ContextAttribute)7:
				case (ContextAttribute)8:
				case (ContextAttribute)11:
				case (ContextAttribute)13:
					break;
				case ContextAttribute.StreamSizes:
					num = StreamSizes.SizeOf;
					goto IL_122;
				case ContextAttribute.SessionKey:
					handleType = typeof(SafeFreeContextBuffer);
					num = SecPkgContext_SessionKey.Size;
					goto IL_122;
				case ContextAttribute.PackageInfo:
					handleType = typeof(SafeFreeContextBuffer);
					goto IL_122;
				case ContextAttribute.NegotiationInfo:
					handleType = typeof(SafeFreeContextBuffer);
					num = Marshal.SizeOf(typeof(NegotiationInfo));
					goto IL_122;
				case ContextAttribute.Flags:
					goto IL_122;
				default:
					if (contextAttribute == ContextAttribute.RemoteCertificate)
					{
						handleType = typeof(SafeFreeCertContext);
						goto IL_122;
					}
					break;
				}
			}
			else
			{
				if (contextAttribute == ContextAttribute.LocalCertificate)
				{
					handleType = typeof(SafeFreeCertContext);
					goto IL_122;
				}
				if (contextAttribute == ContextAttribute.ConnectionInfo)
				{
					num = Marshal.SizeOf(typeof(SslConnectionInfo));
					goto IL_122;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("contextAttribute", (int)contextAttribute, typeof(ContextAttribute)));
			IL_122:
			SafeHandle safeHandle = null;
			object result = null;
			try
			{
				byte[] array = new byte[num];
				int num2 = SspiWrapper.QueryContextAttributes(securityContext, contextAttribute, array, handleType, out safeHandle);
				if (num2 != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num2));
				}
				if (contextAttribute <= ContextAttribute.RemoteCertificate)
				{
					switch (contextAttribute)
					{
					case ContextAttribute.Sizes:
						break;
					case ContextAttribute.Names:
						return Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
					case ContextAttribute.Lifespan:
						return new LifeSpan(array);
					case ContextAttribute.DceInfo:
					case (ContextAttribute)5:
					case ContextAttribute.Authority:
					case (ContextAttribute)7:
					case (ContextAttribute)8:
					case (ContextAttribute)11:
					case (ContextAttribute)13:
						goto IL_2BC;
					case ContextAttribute.StreamSizes:
						return new StreamSizes(array);
					case ContextAttribute.SessionKey:
						try
						{
							byte[] array2;
							void* value;
							if ((array2 = array) == null || array2.Length == 0)
							{
								value = null;
							}
							else
							{
								value = (void*)(&array2[0]);
							}
							result = new SecuritySessionKeyClass(safeHandle, Marshal.ReadInt32(new IntPtr(value)));
						}
						finally
						{
							byte[] array2 = null;
						}
						goto IL_2BC;
					case ContextAttribute.PackageInfo:
						return new SecurityPackageInfoClass(safeHandle, 0);
					case ContextAttribute.NegotiationInfo:
						try
						{
							byte[] array2;
							void* value2;
							if ((array2 = array) == null || array2.Length == 0)
							{
								value2 = null;
							}
							else
							{
								value2 = (void*)(&array2[0]);
							}
							return new NegotiationInfoClass(safeHandle, Marshal.ReadInt32(new IntPtr(value2), NegotiationInfo.NegotiationStateOffset));
						}
						finally
						{
							byte[] array2 = null;
						}
						goto IL_26A;
					case ContextAttribute.Flags:
						try
						{
							byte[] array2;
							byte* value3;
							if ((array2 = array) == null || array2.Length == 0)
							{
								value3 = null;
							}
							else
							{
								value3 = &array2[0];
							}
							return Marshal.ReadInt32(new IntPtr((void*)value3));
						}
						finally
						{
							byte[] array2 = null;
						}
						break;
					default:
						if (contextAttribute != ContextAttribute.RemoteCertificate)
						{
							goto IL_2BC;
						}
						goto IL_26A;
					}
					return new SecSizes(array);
				}
				if (contextAttribute != ContextAttribute.LocalCertificate)
				{
					if (contextAttribute != ContextAttribute.ConnectionInfo)
					{
						goto IL_2BC;
					}
					return new SslConnectionInfo(array);
				}
				IL_26A:
				result = safeHandle;
				safeHandle = null;
				IL_2BC:;
			}
			finally
			{
				if (safeHandle != null)
				{
					safeHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013B80 File Offset: 0x00011D80
		public static int QuerySpecifiedTarget(SafeDeleteContext securityContext, out string specifiedTarget)
		{
			int size = IntPtr.Size;
			Type typeFromHandle = typeof(SafeFreeContextBuffer);
			SafeHandle safeHandle = null;
			specifiedTarget = null;
			int num;
			try
			{
				byte[] buffer = new byte[size];
				num = SspiWrapper.QueryContextAttributes(securityContext, ContextAttribute.SpecifiedTarget, buffer, typeFromHandle, out safeHandle);
				if (num != 0)
				{
					return num;
				}
				specifiedTarget = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
			}
			finally
			{
				if (safeHandle != null)
				{
					safeHandle.Close();
				}
			}
			return num;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public static void ImpersonateSecurityContext(SafeDeleteContext context)
		{
			int num = SafeDeleteContext.ImpersonateSecurityContext(context);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013C18 File Offset: 0x00011E18
		public unsafe static int EncryptDecryptHelper(SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber, bool encrypt, bool isGssBlob)
		{
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(input.Length);
			SecurityBufferStruct[] array = new SecurityBufferStruct[input.Length];
			byte[][] array2 = new byte[input.Length][];
			SecurityBufferStruct[] array3;
			void* unmanagedPointer;
			if ((array3 = array) == null || array3.Length == 0)
			{
				unmanagedPointer = null;
			}
			else
			{
				unmanagedPointer = (void*)(&array3[0]);
			}
			securityBufferDescriptor.UnmanagedPointer = unmanagedPointer;
			GCHandle[] array4 = new GCHandle[input.Length];
			int result;
			try
			{
				for (int i = 0; i < input.Length; i++)
				{
					SecurityBuffer securityBuffer = input[i];
					array[i].count = securityBuffer.size;
					array[i].type = securityBuffer.type;
					if (securityBuffer.token == null || securityBuffer.token.Length == 0)
					{
						array[i].token = IntPtr.Zero;
					}
					else
					{
						array4[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
						array2[i] = securityBuffer.token;
					}
				}
				int num;
				if (encrypt)
				{
					num = SafeDeleteContext.EncryptMessage(context, securityBufferDescriptor, sequenceNumber);
				}
				else
				{
					num = SafeDeleteContext.DecryptMessage(context, securityBufferDescriptor, sequenceNumber);
				}
				for (int j = 0; j < input.Length; j++)
				{
					SecurityBuffer securityBuffer2 = input[j];
					securityBuffer2.size = array[j].count;
					securityBuffer2.type = array[j].type;
					checked
					{
						if (securityBuffer2.size == 0)
						{
							securityBuffer2.offset = 0;
							securityBuffer2.token = null;
						}
						else if (isGssBlob && !encrypt && securityBuffer2.type == BufferType.Data)
						{
							securityBuffer2.token = DiagnosticUtility.Utility.AllocateByteArray(securityBuffer2.size);
							Marshal.Copy(array[j].token, securityBuffer2.token, 0, securityBuffer2.size);
						}
						else
						{
							int k;
							for (k = 0; k < input.Length; k++)
							{
								if (array2[k] != null)
								{
									byte* ptr = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(array2[k], 0));
									if ((void*)array[j].token >= (void*)ptr && (byte*)((void*)array[j].token) + securityBuffer2.size == ptr + array2[k].Length)
									{
										securityBuffer2.offset = (int)(unchecked((long)((byte*)((void*)array[j].token) - (byte*)ptr)));
										securityBuffer2.token = array2[k];
										break;
									}
								}
							}
							if (k >= input.Length)
							{
								securityBuffer2.size = 0;
								securityBuffer2.offset = 0;
								securityBuffer2.token = null;
							}
							if (securityBuffer2.offset < 0 || securityBuffer2.offset > ((securityBuffer2.token == null) ? 0 : securityBuffer2.token.Length))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SspiWrapperEncryptDecryptAssert1", new object[]
								{
									securityBuffer2.offset
								})));
							}
							if (securityBuffer2.size < 0 || securityBuffer2.size > ((securityBuffer2.token == null) ? 0 : (securityBuffer2.token.Length - securityBuffer2.offset)))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SspiWrapperEncryptDecryptAssert2", new object[]
								{
									securityBuffer2.size
								})));
							}
						}
					}
				}
				result = num;
			}
			finally
			{
				for (int l = 0; l < array4.Length; l++)
				{
					if (array4[l].IsAllocated)
					{
						array4[l].Free();
					}
				}
			}
			return result;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013FAC File Offset: 0x000121AC
		public static int EncryptMessage(SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SspiWrapper.EncryptDecryptHelper(context, input, sequenceNumber, true, false);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00013FB8 File Offset: 0x000121B8
		public static int DecryptMessage(SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber, bool isGssBlob)
		{
			return SspiWrapper.EncryptDecryptHelper(context, input, sequenceNumber, false, isGssBlob);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013FC4 File Offset: 0x000121C4
		public static uint SspiPromptForCredential(string targetName, string packageName, out IntPtr ppAuthIdentity, ref bool saveCredentials)
		{
			CREDUI_INFO credui_INFO = default(CREDUI_INFO);
			credui_INFO.cbSize = Marshal.SizeOf(typeof(CREDUI_INFO));
			credui_INFO.pszCaptionText = SR.GetString("SspiLoginPromptHeaderMessage");
			credui_INFO.pszMessageText = "";
			return NativeMethods.SspiPromptForCredentials(targetName, ref credui_INFO, 0U, packageName, IntPtr.Zero, out ppAuthIdentity, ref saveCredentials, 0U);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00014022 File Offset: 0x00012222
		public static bool IsSspiPromptingNeeded(uint ErrorOrNtStatus)
		{
			return NativeMethods.SspiIsPromptingNeeded(ErrorOrNtStatus);
		}

		// Token: 0x040004B3 RID: 1203
		private const int SECPKG_FLAG_NEGOTIABLE2 = 2097152;

		// Token: 0x040004B4 RID: 1204
		private static SecurityPackageInfoClass[] securityPackages;
	}
}
