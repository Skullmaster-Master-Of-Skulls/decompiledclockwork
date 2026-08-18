using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Net
{
	// Token: 0x0200020E RID: 526
	internal static class SSPIWrapper
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x0006718C File Offset: 0x0006538C
		internal static SecurityPackageInfoClass[] EnumerateSecurityPackages(SSPIInterface SecModule)
		{
			if (SecModule.SecurityPackages == null)
			{
				lock (SecModule)
				{
					if (SecModule.SecurityPackages == null)
					{
						int num = 0;
						SafeFreeContextBuffer safeFreeContextBuffer = null;
						try
						{
							int num2 = SecModule.EnumerateSecurityPackages(out num, out safeFreeContextBuffer);
							if (num2 != 0)
							{
								throw new Win32Exception(num2);
							}
							SecurityPackageInfoClass[] array = new SecurityPackageInfoClass[num];
							if (Logging.On)
							{
								Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_enumerating_security_packages"));
							}
							for (int i = 0; i < num; i++)
							{
								array[i] = new SecurityPackageInfoClass(safeFreeContextBuffer, i);
								if (Logging.On)
								{
									Logging.PrintInfo(Logging.Web, "    " + array[i].Name);
								}
							}
							SecModule.SecurityPackages = array;
						}
						finally
						{
							if (safeFreeContextBuffer != null)
							{
								safeFreeContextBuffer.Close();
							}
						}
					}
				}
			}
			return SecModule.SecurityPackages;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00067280 File Offset: 0x00065480
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName)
		{
			return SSPIWrapper.GetVerifyPackageInfo(secModule, packageName, false);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0006728C File Offset: 0x0006548C
		internal static SecurityPackageInfoClass GetVerifyPackageInfo(SSPIInterface secModule, string packageName, bool throwIfMissing)
		{
			SecurityPackageInfoClass[] array = SSPIWrapper.EnumerateSecurityPackages(secModule);
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
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_package_not_found", new object[]
				{
					packageName
				}));
			}
			if (throwIfMissing)
			{
				throw new NotSupportedException(SR.GetString("net_securitypackagesupport"));
			}
			return null;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000672FF File Offset: 0x000654FF
		private static ConcurrentDictionary<string, SafeFreeCredentials> InitDefaultCredentialsHandleCache()
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Format("{0}: {1} = {2}", "InitDefaultCredentialsHandleCache", "defaultCredentialsHandleCacheSize", SSPIWrapper.s_DefaultCredentialsHandleCacheSize));
			}
			return new ConcurrentDictionary<string, SafeFreeCredentials>(Environment.ProcessorCount, SSPIWrapper.s_DefaultCredentialsHandleCacheSize);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00067340 File Offset: 0x00065540
		public static SafeFreeCredentials AcquireDefaultCredential(SSPIInterface SecModule, string package, CredentialUse intent)
		{
			SafeFreeCredentials safeFreeCredentials = null;
			string text = null;
			bool flag;
			if (SSPIWrapper.s_DefaultCredentialsHandleCacheEnabled)
			{
				text = string.Format("{0}_{1}_{2}", package, intent.ToString(), WindowsIdentity.GetCurrent().Name);
				flag = SSPIWrapper.s_DefaultCredentialsHandleCache.Value.TryGetValue(text, out safeFreeCredentials);
			}
			else
			{
				flag = false;
			}
			if (Logging.On)
			{
				if (text == null)
				{
					text = string.Format("{0}_{1}_{2}", package, intent.ToString(), WindowsIdentity.GetCurrent().Name);
				}
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcquireDefaultCredential(package = ",
					package,
					", intent = ",
					intent.ToString(),
					", identity = ",
					text,
					", cached = ",
					flag.ToString(),
					")"
				}));
			}
			if (!flag)
			{
				int num = SecModule.AcquireDefaultCredential(package, intent, out safeFreeCredentials);
				if (num != 0)
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
						{
							"AcquireDefaultCredential()",
							string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[]
							{
								num
							})
						}));
					}
					throw new Win32Exception(num);
				}
				if (SSPIWrapper.s_DefaultCredentialsHandleCacheEnabled && SSPIWrapper.s_DefaultCredentialsHandleCache.Value.Count < SSPIWrapper.s_DefaultCredentialsHandleCacheSize)
				{
					try
					{
						SSPIWrapper.s_DefaultCredentialsHandleCache.Value.TryAdd(text, safeFreeCredentials);
					}
					catch (OverflowException)
					{
					}
				}
			}
			return safeFreeCredentials;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000674CC File Offset: 0x000656CC
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, ref AuthIdentity authdata)
		{
			if (Logging.On)
			{
				TraceSource web = Logging.Web;
				string[] array = new string[7];
				array[0] = "AcquireCredentialsHandle(package  = ";
				array[1] = package;
				array[2] = ", intent   = ";
				array[3] = intent.ToString();
				array[4] = ", authdata = ";
				int num = 5;
				AuthIdentity authIdentity = authdata;
				array[num] = authIdentity.ToString();
				array[6] = ")";
				Logging.PrintInfo(web, string.Concat(array));
			}
			SafeFreeCredentials result = null;
			int num2 = SecModule.AcquireCredentialsHandle(package, intent, ref authdata, out result);
			if (num2 != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[]
						{
							num2
						})
					}));
				}
				throw new Win32Exception(num2);
			}
			return result;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000675A4 File Offset: 0x000657A4
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, ref SafeSspiAuthDataHandle authdata)
		{
			if (Logging.On)
			{
				TraceSource web = Logging.Web;
				string[] array = new string[7];
				array[0] = "AcquireCredentialsHandle(package  = ";
				array[1] = package;
				array[2] = ", intent   = ";
				array[3] = intent.ToString();
				array[4] = ", authdata = ";
				int num = 5;
				SafeSspiAuthDataHandle safeSspiAuthDataHandle = authdata;
				array[num] = ((safeSspiAuthDataHandle != null) ? safeSspiAuthDataHandle.ToString() : null);
				array[6] = ")";
				Logging.PrintInfo(web, string.Concat(array));
			}
			SafeFreeCredentials result = null;
			int num2 = SecModule.AcquireCredentialsHandle(package, intent, ref authdata, out result);
			if (num2 != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[]
						{
							num2
						})
					}));
				}
				throw new Win32Exception(num2);
			}
			return result;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00067678 File Offset: 0x00065878
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, SecureCredential scc)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcquireCredentialsHandle(package = ",
					package,
					", intent  = ",
					intent.ToString(),
					", scc     = ",
					scc.ToString(),
					")"
				}));
			}
			SafeFreeCredentials result = null;
			int num = SecModule.AcquireCredentialsHandle(package, intent, ref scc, out result);
			if (num != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[]
						{
							num
						})
					}));
				}
				throw new Win32Exception(num);
			}
			return result;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0006774C File Offset: 0x0006594C
		public static SafeFreeCredentials AcquireCredentialsHandle(SSPIInterface SecModule, string package, CredentialUse intent, SecureCredential2 scc)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcquireCredentialsHandle(package = ",
					package,
					", intent  = ",
					intent.ToString(),
					", scc     = ",
					scc.ToString(),
					")"
				}));
			}
			SafeFreeCredentials result = null;
			int num = SecModule.AcquireCredentialsHandle(package, intent, ref scc, out result);
			if (num != 0)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
					{
						"AcquireCredentialsHandle()",
						string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[]
						{
							num
						})
					}));
				}
				throw new Win32Exception(num);
			}
			return result;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00067820 File Offset: 0x00065A20
		internal static int InitializeSecurityContext(SSPIInterface SecModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"InitializeSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", targetName = ",
					targetName,
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.InitializeSecurityContext(ref credential, ref context, targetName, inFlags, datarep, inputBuffer, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffer", new object[]
				{
					"InitializeSecurityContext",
					(inputBuffer == null) ? 0 : inputBuffer.size,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00067908 File Offset: 0x00065B08
		internal static int InitializeSecurityContext(SSPIInterface SecModule, SafeFreeCredentials credential, ref SafeDeleteContext context, string targetName, ContextFlags inFlags, Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"InitializeSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", targetName = ",
					targetName,
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.InitializeSecurityContext(credential, ref context, targetName, inFlags, datarep, inputBuffers, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffers", new object[]
				{
					"InitializeSecurityContext",
					(inputBuffers == null) ? 0 : inputBuffers.Length,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000679EC File Offset: 0x00065BEC
		internal static int AcceptSecurityContext(SSPIInterface SecModule, ref SafeFreeCredentials credential, ref SafeDeleteContext context, ContextFlags inFlags, Endianness datarep, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcceptSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.AcceptSecurityContext(ref credential, ref context, inputBuffer, inFlags, datarep, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffer", new object[]
				{
					"AcceptSecurityContext",
					(inputBuffer == null) ? 0 : inputBuffer.size,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00067AC4 File Offset: 0x00065CC4
		internal static int AcceptSecurityContext(SSPIInterface SecModule, SafeFreeCredentials credential, ref SafeDeleteContext context, ContextFlags inFlags, Endianness datarep, SecurityBuffer[] inputBuffers, SecurityBuffer outputBuffer, ref ContextFlags outFlags)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, string.Concat(new string[]
				{
					"AcceptSecurityContext(credential = ",
					credential.ToString(),
					", context = ",
					ValidationHelper.ToString(context),
					", inFlags = ",
					inFlags.ToString(),
					")"
				}));
			}
			int num = SecModule.AcceptSecurityContext(credential, ref context, inputBuffers, inFlags, datarep, outputBuffer, ref outFlags);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_sspi_security_context_input_buffers", new object[]
				{
					"AcceptSecurityContext",
					(inputBuffers == null) ? 0 : inputBuffers.Length,
					outputBuffer.size,
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00067B98 File Offset: 0x00065D98
		internal static int CompleteAuthToken(SSPIInterface SecModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = SecModule.CompleteAuthToken(ref context, inputBuffers);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_operation_returned_something", new object[]
				{
					"CompleteAuthToken()",
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00067BE4 File Offset: 0x00065DE4
		internal static int ApplyControlToken(SSPIInterface SecModule, ref SafeDeleteContext context, SecurityBuffer[] inputBuffers)
		{
			int num = SecModule.ApplyControlToken(ref context, inputBuffers);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_operation_returned_something", new object[]
				{
					"ApplyControlToken()",
					(SecurityStatus)num
				}));
			}
			return num;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00067C2D File Offset: 0x00065E2D
		public static int QuerySecurityContextToken(SSPIInterface SecModule, SafeDeleteContext context, out SafeCloseHandle token)
		{
			return SecModule.QuerySecurityContextToken(context, out token);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00067C37 File Offset: 0x00065E37
		public static int EncryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Encrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00067C43 File Offset: 0x00065E43
		public static int DecryptMessage(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.Decrypt, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00067C50 File Offset: 0x00065E50
		public static int ApplyAlertToken(SSPIInterface secModule, ref SafeFreeCredentials credentialsHandle, SafeDeleteContext securityContext, TlsAlertType alertType, TlsAlertMessage alertMessage)
		{
			Interop.SChannel.SCHANNEL_ALERT_TOKEN schannel_ALERT_TOKEN;
			schannel_ALERT_TOKEN.dwTokenType = 2U;
			schannel_ALERT_TOKEN.dwAlertType = (uint)alertType;
			schannel_ALERT_TOKEN.dwAlertNumber = (uint)alertMessage;
			SecurityBuffer[] array = new SecurityBuffer[1];
			int num = Marshal.SizeOf(typeof(Interop.SChannel.SCHANNEL_ALERT_TOKEN));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			int result;
			try
			{
				byte[] array2 = new byte[num];
				Marshal.StructureToPtr(schannel_ALERT_TOKEN, intPtr, false);
				Marshal.Copy(intPtr, array2, 0, num);
				array[0] = new SecurityBuffer(array2, BufferType.Token);
				result = SSPIWrapper.ApplyControlToken(secModule, ref securityContext, array);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00067CE4 File Offset: 0x00065EE4
		public static int ApplyShutdownToken(SSPIInterface secModule, ref SafeFreeCredentials credentialsHandle, SafeDeleteContext securityContext)
		{
			int value = 1;
			SecurityBuffer[] array = new SecurityBuffer[1];
			byte[] bytes = BitConverter.GetBytes(value);
			array[0] = new SecurityBuffer(bytes, BufferType.Token);
			return SSPIWrapper.ApplyControlToken(secModule, ref securityContext, array);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00067D14 File Offset: 0x00065F14
		internal static int MakeSignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.MakeSignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00067D20 File Offset: 0x00065F20
		public static int VerifySignature(SSPIInterface secModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			return SSPIWrapper.EncryptDecryptHelper(SSPIWrapper.OP.VerifySignature, secModule, context, input, sequenceNumber);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00067D2C File Offset: 0x00065F2C
		private unsafe static int EncryptDecryptHelper(SSPIWrapper.OP op, SSPIInterface SecModule, SafeDeleteContext context, SecurityBuffer[] input, uint sequenceNumber)
		{
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(input.Length);
			SecurityBufferStruct[] array = new SecurityBufferStruct[input.Length];
			SecurityBufferStruct[] array2;
			SecurityBufferStruct* unmanagedPointer;
			if ((array2 = array) == null || array2.Length == 0)
			{
				unmanagedPointer = null;
			}
			else
			{
				unmanagedPointer = &array2[0];
			}
			securityBufferDescriptor.UnmanagedPointer = (void*)unmanagedPointer;
			GCHandle[] array3 = new GCHandle[input.Length];
			byte[][] array4 = new byte[input.Length][];
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
						array3[i] = GCHandle.Alloc(securityBuffer.token, GCHandleType.Pinned);
						array[i].token = Marshal.UnsafeAddrOfPinnedArrayElement(securityBuffer.token, securityBuffer.offset);
						array4[i] = securityBuffer.token;
					}
				}
				int num;
				switch (op)
				{
				case SSPIWrapper.OP.Encrypt:
					num = SecModule.EncryptMessage(context, securityBufferDescriptor, sequenceNumber);
					break;
				case SSPIWrapper.OP.Decrypt:
					num = SecModule.DecryptMessage(context, securityBufferDescriptor, sequenceNumber);
					break;
				case SSPIWrapper.OP.MakeSignature:
					num = SecModule.MakeSignature(context, securityBufferDescriptor, sequenceNumber);
					break;
				case SSPIWrapper.OP.VerifySignature:
					num = SecModule.VerifySignature(context, securityBufferDescriptor, sequenceNumber);
					break;
				default:
					throw ExceptionHelper.MethodNotImplementedException;
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
						else
						{
							int k;
							for (k = 0; k < input.Length; k++)
							{
								if (array4[k] != null)
								{
									byte* ptr = (byte*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(array4[k], 0));
									if ((void*)array[j].token >= (void*)ptr && (byte*)((void*)array[j].token) + securityBuffer2.size == ptr + array4[k].Length)
									{
										securityBuffer2.offset = (int)(unchecked((long)((byte*)((void*)array[j].token) - (byte*)ptr)));
										securityBuffer2.token = array4[k];
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
						}
					}
				}
				if (num != 0 && Logging.On)
				{
					if (num == 590625)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_returned_something", new object[]
						{
							op,
							"SEC_I_RENEGOTIATE"
						}));
					}
					else
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_log_operation_failed_with_error", new object[]
						{
							op,
							string.Format(CultureInfo.CurrentCulture, "0X{0:X}", new object[]
							{
								num
							})
						}));
					}
				}
				result = num;
			}
			finally
			{
				for (int l = 0; l < array3.Length; l++)
				{
					if (array3[l].IsAllocated)
					{
						array3[l].Free();
					}
				}
			}
			return result;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00068084 File Offset: 0x00066284
		public static SafeFreeContextBufferChannelBinding QueryContextChannelBinding(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute)
		{
			SafeFreeContextBufferChannelBinding result;
			int num = SecModule.QueryContextChannelBinding(securityContext, contextAttribute, out result);
			if (num != 0)
			{
				return null;
			}
			return result;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x000680A4 File Offset: 0x000662A4
		public static object QueryContextAttributes(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute)
		{
			int num;
			return SSPIWrapper.QueryContextAttributes(SecModule, securityContext, contextAttribute, out num);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000680BC File Offset: 0x000662BC
		public unsafe static object QueryContextAttributes(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute, out int errorCode)
		{
			int num = IntPtr.Size;
			Type handleType = null;
			if (contextAttribute <= ContextAttribute.ClientSpecifiedSpn)
			{
				if (contextAttribute <= ContextAttribute.PackageInfo)
				{
					switch (contextAttribute)
					{
					case ContextAttribute.Sizes:
						num = SecSizes.SizeOf;
						goto IL_143;
					case ContextAttribute.Names:
						handleType = typeof(SafeFreeContextBuffer);
						goto IL_143;
					case ContextAttribute.Lifespan:
					case ContextAttribute.DceInfo:
						break;
					case ContextAttribute.StreamSizes:
						num = StreamSizes.SizeOf;
						goto IL_143;
					default:
						if (contextAttribute == ContextAttribute.PackageInfo)
						{
							handleType = typeof(SafeFreeContextBuffer);
							goto IL_143;
						}
						break;
					}
				}
				else
				{
					if (contextAttribute == ContextAttribute.NegotiationInfo)
					{
						handleType = typeof(SafeFreeContextBuffer);
						num = Marshal.SizeOf(typeof(NegotiationInfo));
						goto IL_143;
					}
					if (contextAttribute == ContextAttribute.ClientSpecifiedSpn)
					{
						handleType = typeof(SafeFreeContextBuffer);
						goto IL_143;
					}
				}
			}
			else if (contextAttribute <= ContextAttribute.LocalCertificate)
			{
				if (contextAttribute == ContextAttribute.RemoteCertificate)
				{
					handleType = typeof(SafeFreeCertContext);
					goto IL_143;
				}
				if (contextAttribute == ContextAttribute.LocalCertificate)
				{
					handleType = typeof(SafeFreeCertContext);
					goto IL_143;
				}
			}
			else
			{
				if (contextAttribute == ContextAttribute.IssuerListInfoEx)
				{
					num = Marshal.SizeOf(typeof(IssuerListInfoEx));
					handleType = typeof(SafeFreeContextBuffer);
					goto IL_143;
				}
				if (contextAttribute == ContextAttribute.ConnectionInfo)
				{
					num = Marshal.SizeOf(typeof(SslConnectionInfo));
					goto IL_143;
				}
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"ContextAttribute"
			}), "contextAttribute");
			IL_143:
			SafeHandle safeHandle = null;
			object result = null;
			try
			{
				byte[] array = new byte[num];
				errorCode = SecModule.QueryContextAttributes(securityContext, contextAttribute, array, handleType, out safeHandle);
				if (errorCode != 0)
				{
					return null;
				}
				if (contextAttribute <= ContextAttribute.ClientSpecifiedSpn)
				{
					if (contextAttribute <= ContextAttribute.PackageInfo)
					{
						switch (contextAttribute)
						{
						case ContextAttribute.Sizes:
							result = new SecSizes(array);
							break;
						case ContextAttribute.Names:
							result = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
							break;
						case ContextAttribute.Lifespan:
						case ContextAttribute.DceInfo:
							break;
						case ContextAttribute.StreamSizes:
							result = new StreamSizes(array);
							break;
						default:
							if (contextAttribute == ContextAttribute.PackageInfo)
							{
								result = new SecurityPackageInfoClass(safeHandle, 0);
							}
							break;
						}
					}
					else
					{
						if (contextAttribute != ContextAttribute.NegotiationInfo)
						{
							if (contextAttribute != ContextAttribute.ClientSpecifiedSpn)
							{
								goto IL_279;
							}
						}
						else
						{
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
								return new NegotiationInfoClass(safeHandle, Marshal.ReadInt32(new IntPtr(value), NegotiationInfo.NegotiationStateOffest));
							}
							finally
							{
								byte[] array2 = null;
							}
						}
						result = Marshal.PtrToStringUni(safeHandle.DangerousGetHandle());
					}
				}
				else if (contextAttribute <= ContextAttribute.LocalCertificate)
				{
					if (contextAttribute == ContextAttribute.RemoteCertificate || contextAttribute == ContextAttribute.LocalCertificate)
					{
						result = safeHandle;
						safeHandle = null;
					}
				}
				else if (contextAttribute != ContextAttribute.IssuerListInfoEx)
				{
					if (contextAttribute == ContextAttribute.ConnectionInfo)
					{
						result = new SslConnectionInfo(array);
					}
				}
				else
				{
					result = new IssuerListInfoEx(safeHandle, array);
					safeHandle = null;
				}
				IL_279:;
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

		// Token: 0x060013BE RID: 5054 RVA: 0x00068388 File Offset: 0x00066588
		public static int SetContextAttributes(SSPIInterface SecModule, SafeDeleteContext securityContext, ContextAttribute contextAttribute, object value)
		{
			if (contextAttribute == ContextAttribute.UiInfo)
			{
				IntPtr intPtr = (IntPtr)value;
				byte[] array = new byte[IntPtr.Size];
				if (IntPtr.Size == 4)
				{
					int num = intPtr.ToInt32();
					array[0] = (byte)num;
					array[1] = (byte)(num >> 8);
					array[2] = (byte)(num >> 16);
					array[3] = (byte)(num >> 24);
				}
				else
				{
					long num2 = intPtr.ToInt64();
					array[0] = (byte)num2;
					array[1] = (byte)(num2 >> 8);
					array[2] = (byte)(num2 >> 16);
					array[3] = (byte)(num2 >> 24);
					array[4] = (byte)(num2 >> 32);
					array[5] = (byte)(num2 >> 40);
					array[6] = (byte)(num2 >> 48);
					array[7] = (byte)(num2 >> 56);
				}
				return SecModule.SetContextAttributes(securityContext, contextAttribute, array);
			}
			throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
			{
				"ContextAttribute"
			}), "contextAttribute");
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00068450 File Offset: 0x00066650
		public static string ErrorDescription(int errorCode)
		{
			if (errorCode == -1)
			{
				return "An exception when invoking Win32 API";
			}
			SecurityStatus securityStatus = (SecurityStatus)errorCode;
			if (securityStatus <= SecurityStatus.MessageAltered)
			{
				switch (securityStatus)
				{
				case SecurityStatus.InvalidHandle:
					return "Invalid handle";
				case SecurityStatus.Unsupported:
				case SecurityStatus.InternalError:
					break;
				case SecurityStatus.TargetUnknown:
					return "Target unknown";
				case SecurityStatus.PackageNotFound:
					return "Package not found";
				default:
					if (securityStatus == SecurityStatus.InvalidToken)
					{
						return "Invalid token";
					}
					if (securityStatus == SecurityStatus.MessageAltered)
					{
						return "Message altered";
					}
					break;
				}
			}
			else
			{
				if (securityStatus == SecurityStatus.IncompleteMessage)
				{
					return "Message incomplete";
				}
				switch (securityStatus)
				{
				case SecurityStatus.BufferNotEnough:
					return "Buffer not enough";
				case SecurityStatus.WrongPrincipal:
					return "Wrong principal";
				case (SecurityStatus)(-2146893021):
				case SecurityStatus.TimeSkew:
					break;
				case SecurityStatus.UntrustedRoot:
					return "Untrusted root";
				default:
					if (securityStatus == SecurityStatus.ContinueNeeded)
					{
						return "Continue needed";
					}
					break;
				}
			}
			return "0x" + errorCode.ToString("x", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04001570 RID: 5488
		private static int s_DefaultCredentialsHandleCacheSize = SettingsSectionInternal.Section.DefaultCredentialsHandleCacheSize;

		// Token: 0x04001571 RID: 5489
		private static bool s_DefaultCredentialsHandleCacheEnabled = SSPIWrapper.s_DefaultCredentialsHandleCacheSize > 0;

		// Token: 0x04001572 RID: 5490
		private static readonly Lazy<ConcurrentDictionary<string, SafeFreeCredentials>> s_DefaultCredentialsHandleCache = new Lazy<ConcurrentDictionary<string, SafeFreeCredentials>>(new Func<ConcurrentDictionary<string, SafeFreeCredentials>>(SSPIWrapper.InitDefaultCredentialsHandleCache));

		// Token: 0x0200075C RID: 1884
		private enum OP
		{
			// Token: 0x04003234 RID: 12852
			Encrypt = 1,
			// Token: 0x04003235 RID: 12853
			Decrypt,
			// Token: 0x04003236 RID: 12854
			MakeSignature,
			// Token: 0x04003237 RID: 12855
			VerifySignature
		}
	}
}
