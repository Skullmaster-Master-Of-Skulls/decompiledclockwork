using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Web.Management.Utility
{
	// Token: 0x0200007F RID: 127
	internal sealed class HttpApiWrapper
	{
		// Token: 0x0600038E RID: 910 RVA: 0x00009599 File Offset: 0x00008599
		public static string ConvertBytesToCertificateHexString(byte[] sArray)
		{
			return HttpApiWrapper.ConvertBytesToCertificateHexString(sArray, 0U, (uint)sArray.Length);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000095A8 File Offset: 0x000085A8
		public static string ConvertBytesToCertificateHexString(byte[] sArray, uint start, uint end)
		{
			string result = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2U];
				uint num = start;
				uint num2 = 0U;
				while (num < end)
				{
					uint num3 = (uint)((sArray[(int)((UIntPtr)num)] & 240) >> 4);
					array[(int)((UIntPtr)(num2++))] = HttpApiWrapper._hexValues[(int)((UIntPtr)num3)];
					num3 = (uint)(sArray[(int)((UIntPtr)num)] & 15);
					array[(int)((UIntPtr)(num2++))] = HttpApiWrapper._hexValues[(int)((UIntPtr)num3)];
					num += 1U;
				}
				result = new string(array);
			}
			return result;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00009618 File Offset: 0x00008618
		public static byte[] ConvertCertificateHexStringToBytes(string hexString)
		{
			if (hexString == null)
			{
				throw new ArgumentNullException("hexString");
			}
			bool flag = false;
			int i = 0;
			int num = hexString.Length;
			if (num >= 2 && hexString[0] == '0' && (hexString[1] == 'x' || hexString[1] == 'X'))
			{
				num = hexString.Length - 2;
				i = 2;
			}
			if (num % 2 != 0)
			{
				int num2 = num % 3;
			}
			byte[] array;
			if (num >= 3 && hexString[i + 2] == ' ')
			{
				flag = true;
				array = new byte[num / 3 + 1];
			}
			else
			{
				array = new byte[num / 2];
			}
			int num3 = 0;
			while (i < hexString.Length)
			{
				int num4 = HttpApiWrapper.ConvertHexDigit(hexString[i]);
				int num5 = HttpApiWrapper.ConvertHexDigit(hexString[i + 1]);
				array[num3] = (byte)(num5 | num4 << 4);
				if (flag)
				{
					i++;
				}
				i += 2;
				num3++;
			}
			return array;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000096EC File Offset: 0x000086EC
		public static int ConvertHexDigit(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (int)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (int)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (int)(val - 'A' + '\n');
			}
			return 0;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009724 File Offset: 0x00008724
		public static void CreateSSLBinding(IPEndPoint endPoint, X509Certificate2 certificateObject, string certificateStoreName)
		{
			if (certificateObject == null)
			{
				throw new ArgumentNullException("certificateObject");
			}
			byte[] certHash = certificateObject.GetCertHash();
			if (certHash == null)
			{
				throw new ArgumentNullException("certificateHash");
			}
			HttpApiWrapper.CreateSSLBinding(endPoint, certHash, certificateStoreName);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000975C File Offset: 0x0000875C
		public static void CreateSSLBinding(IPEndPoint endPoint, byte[] certificateHash, string certificateStoreName)
		{
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED http_SERVICE_CONFIG_SSL_PARAM_MANAGED = new HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED();
			http_SERVICE_CONFIG_SSL_PARAM_MANAGED.pSslHash = certificateHash;
			http_SERVICE_CONFIG_SSL_PARAM_MANAGED.AppId = HttpApiWrapper.IisAppId;
			if (certificateStoreName == null || string.IsNullOrEmpty(certificateStoreName.Trim()))
			{
				http_SERVICE_CONFIG_SSL_PARAM_MANAGED.pSslCertStoreName = "MY";
			}
			else
			{
				http_SERVICE_CONFIG_SSL_PARAM_MANAGED.pSslCertStoreName = certificateStoreName;
			}
			HttpApiWrapper.CreateSSLBinding(endPoint, http_SERVICE_CONFIG_SSL_PARAM_MANAGED);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000097BC File Offset: 0x000087BC
		public static void CreateSSLBinding(IPEndPoint endPoint, HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED allSSLData)
		{
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			if (allSSLData == null)
			{
				throw new ArgumentNullException("allSSLData");
			}
			uint num = HttpApiWrapper.Init();
			if (num != 0U)
			{
				throw new SystemException();
			}
			HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET http_SERVICE_CONFIG_SSL_SET = HttpApiWrapper.PrepareHttpSetInfo(endPoint, allSSLData);
			try
			{
				HttpApiWrapper.DeleteSSLBinding(endPoint);
				num = HttpApiWrapper.HttpSetServiceConfiguration(IntPtr.Zero, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID.HttpServiceConfigSSLCertInfo, ref http_SERVICE_CONFIG_SSL_SET, (uint)Marshal.SizeOf(http_SERVICE_CONFIG_SSL_SET), IntPtr.Zero);
				if (num == 2U)
				{
					throw new FileNotFoundException();
				}
				if (num != 0U)
				{
					if (num == 122U || num != 50U)
					{
					}
					throw new Win32Exception((int)num);
				}
			}
			finally
			{
				if (http_SERVICE_CONFIG_SSL_SET.KeyDesc.pIpPort != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(http_SERVICE_CONFIG_SSL_SET.KeyDesc.pIpPort);
				}
				if (http_SERVICE_CONFIG_SSL_SET.ParamDesc.pSslHash != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(http_SERVICE_CONFIG_SSL_SET.ParamDesc.pSslHash);
				}
				HttpApiWrapper.Terminate();
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000098B4 File Offset: 0x000088B4
		public unsafe static void DeleteSSLBinding(IPEndPoint endPoint)
		{
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			uint num = HttpApiWrapper.Init();
			if (num != 0U)
			{
				throw new SystemException();
			}
			try
			{
				byte[] rawBytesFromEndPoint = HttpApiWrapper.GetRawBytesFromEndPoint(endPoint);
				try
				{
					fixed (byte* ptr = rawBytesFromEndPoint)
					{
						HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET http_SERVICE_CONFIG_SSL_SET = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET);
						http_SERVICE_CONFIG_SSL_SET.KeyDesc = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_KEY);
						http_SERVICE_CONFIG_SSL_SET.ParamDesc = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM);
						http_SERVICE_CONFIG_SSL_SET.KeyDesc.pIpPort = (IntPtr)((void*)ptr);
						num = HttpApiWrapper.HttpDeleteServiceConfiguration(IntPtr.Zero, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID.HttpServiceConfigSSLCertInfo, ref http_SERVICE_CONFIG_SSL_SET, (uint)Marshal.SizeOf(http_SERVICE_CONFIG_SSL_SET), IntPtr.Zero);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (num != 0U && num != 2U)
				{
					throw new IOException(num.ToString(CultureInfo.InvariantCulture));
				}
			}
			finally
			{
				HttpApiWrapper.Terminate();
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009998 File Offset: 0x00008998
		public static uint Init()
		{
			uint num = HttpApiWrapper.HttpInitialize(new HttpApiWrapper.HTTPAPI_VERSION
			{
				Major = 1,
				Minor = 0
			}, 3U, IntPtr.Zero);
			if (num != 0U)
			{
				return num;
			}
			HttpApiWrapper._httpInitializedAtLeastOnce = true;
			return num;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000099D8 File Offset: 0x000089D8
		public unsafe static HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED GetAllSSLBindingProperties(IPEndPoint endPoint)
		{
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			uint num = HttpApiWrapper.Init();
			if (num != 0U)
			{
				throw new SystemException();
			}
			SafeGlobalAllocHandle safeGlobalAllocHandle = null;
			HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED result;
			try
			{
				byte[] rawBytesFromEndPoint = HttpApiWrapper.GetRawBytesFromEndPoint(endPoint);
				try
				{
					fixed (byte* ptr = rawBytesFromEndPoint)
					{
						HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_QUERY http_SERVICE_CONFIG_SSL_QUERY = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_QUERY);
						http_SERVICE_CONFIG_SSL_QUERY.QueryDesc = HttpApiWrapper.HTTP_SERVICE_CONFIG_QUERY_TYPE.HttpServiceConfigQueryExact;
						http_SERVICE_CONFIG_SSL_QUERY.dwToken = 0;
						http_SERVICE_CONFIG_SSL_QUERY.KeyDesc = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_KEY);
						http_SERVICE_CONFIG_SSL_QUERY.KeyDesc.pIpPort = (IntPtr)((void*)ptr);
						uint outputConfigInfoLength = 0U;
						uint num2 = 0U;
						num = HttpApiWrapper.HttpQueryServiceConfiguration(IntPtr.Zero, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID.HttpServiceConfigSSLCertInfo, ref http_SERVICE_CONFIG_SSL_QUERY, (uint)Marshal.SizeOf(http_SERVICE_CONFIG_SSL_QUERY), SafeGlobalAllocHandle.Empty, outputConfigInfoLength, ref num2, IntPtr.Zero);
						if (num == 2U)
						{
							return null;
						}
						if (num == 259U)
						{
							num = 0U;
						}
						else if (num == 122U)
						{
							safeGlobalAllocHandle = new SafeGlobalAllocHandle((int)num2);
							outputConfigInfoLength = num2;
							num = HttpApiWrapper.HttpQueryServiceConfiguration(IntPtr.Zero, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID.HttpServiceConfigSSLCertInfo, ref http_SERVICE_CONFIG_SSL_QUERY, (uint)Marshal.SizeOf(http_SERVICE_CONFIG_SSL_QUERY), safeGlobalAllocHandle, outputConfigInfoLength, ref num2, IntPtr.Zero);
						}
						if (num != 0U)
						{
							throw new Win32Exception((int)num);
						}
						HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET nativeStructure = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET);
						nativeStructure = safeGlobalAllocHandle.MarshalToStructure<HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET>();
						result = new HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED(nativeStructure);
					}
				}
				finally
				{
					byte* ptr = null;
				}
			}
			finally
			{
				if (safeGlobalAllocHandle != null)
				{
					safeGlobalAllocHandle.Close();
				}
				HttpApiWrapper.Terminate();
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00009B34 File Offset: 0x00008B34
		public static X509Certificate2 GetCertificateFromStore(string certificateHash)
		{
			X509Certificate2 result = null;
			X509Store x509Store = null;
			try
			{
				x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509CertificateCollection x509CertificateCollection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, certificateHash, false);
				if (x509CertificateCollection.Count < 1)
				{
					return null;
				}
				result = (X509Certificate2)x509CertificateCollection[0];
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009B9C File Offset: 0x00008B9C
		private static byte[] GetRawBytesFromEndPoint(IPEndPoint endPoint)
		{
			SocketAddress socketAddress = endPoint.Serialize();
			byte[] array = new byte[socketAddress.Size];
			for (int i = 0; i < socketAddress.Size; i++)
			{
				array[i] = socketAddress[i];
			}
			return array;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00009BDC File Offset: 0x00008BDC
		public static object GetSSLBindingProperty(IPEndPoint endPoint, HttpApiWrapper.HttpPropertyName propName)
		{
			object result = null;
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED allSSLBindingProperties = HttpApiWrapper.GetAllSSLBindingProperties(endPoint);
			if (allSSLBindingProperties == null)
			{
				return null;
			}
			switch (propName)
			{
			case HttpApiWrapper.HttpPropertyName.SSLCertHash:
				if (allSSLBindingProperties.SslHashLength > 0U)
				{
					result = allSSLBindingProperties.pSslHash;
				}
				break;
			case HttpApiWrapper.HttpPropertyName.SSLStoreName:
				result = allSSLBindingProperties.pSslCertStoreName;
				break;
			case HttpApiWrapper.HttpPropertyName.AppId:
				result = allSSLBindingProperties.AppId;
				break;
			case HttpApiWrapper.HttpPropertyName.CertCheckMode:
				result = allSSLBindingProperties.DefaultCertCheckMode;
				break;
			case HttpApiWrapper.HttpPropertyName.RevocationFreshnessTime:
				result = allSSLBindingProperties.DefaultRevocationFreshnessTime;
				break;
			case HttpApiWrapper.HttpPropertyName.RevocationURLRetrievalTimeout:
				result = allSSLBindingProperties.DefaultRevocationUrlRetrievalTimeout;
				break;
			case HttpApiWrapper.HttpPropertyName.SslCtlIdentifier:
				result = allSSLBindingProperties.pDefaultSslCtlIdentifier;
				break;
			case HttpApiWrapper.HttpPropertyName.SslCtlStoreName:
				result = allSSLBindingProperties.pDefaultSslCtlStoreName;
				break;
			case HttpApiWrapper.HttpPropertyName.SSLUseDsMapper:
			{
				uint defaultFlags = allSSLBindingProperties.DefaultFlags;
				result = ((defaultFlags & 1U) != 0U);
				break;
			}
			case HttpApiWrapper.HttpPropertyName.SSLAlwaysNegoClientCert:
			{
				uint defaultFlags = allSSLBindingProperties.DefaultFlags;
				result = ((defaultFlags & 2U) != 0U);
				break;
			}
			case HttpApiWrapper.HttpPropertyName.DefaultFlags:
				result = allSSLBindingProperties.DefaultFlags;
				break;
			}
			return result;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00009CEC File Offset: 0x00008CEC
		private static HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET PrepareHttpSetInfo(IPEndPoint endPoint, HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED allSSLData)
		{
			byte[] rawBytesFromEndPoint = HttpApiWrapper.GetRawBytesFromEndPoint(endPoint);
			IntPtr intPtr = Marshal.AllocHGlobal(rawBytesFromEndPoint.Length);
			Marshal.Copy(rawBytesFromEndPoint, 0, intPtr, rawBytesFromEndPoint.Length);
			HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET result = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET);
			result.KeyDesc = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_KEY);
			result.ParamDesc = default(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM);
			if (allSSLData.pSslHash != null)
			{
				IntPtr intPtr2 = Marshal.AllocHGlobal(allSSLData.pSslHash.Length);
				Marshal.Copy(allSSLData.pSslHash, 0, intPtr2, allSSLData.pSslHash.Length);
				result.ParamDesc.pSslHash = intPtr2;
				result.ParamDesc.SslHashLength = (uint)allSSLData.pSslHash.Length;
			}
			else
			{
				result.ParamDesc.pSslHash = IntPtr.Zero;
				result.ParamDesc.SslHashLength = 0U;
			}
			result.KeyDesc.pIpPort = intPtr;
			result.ParamDesc.AppId = allSSLData.AppId;
			result.ParamDesc.pSslCertStoreName = allSSLData.pSslCertStoreName;
			result.ParamDesc.DefaultCertCheckMode = allSSLData.DefaultCertCheckMode;
			result.ParamDesc.DefaultRevocationFreshnessTime = allSSLData.DefaultRevocationFreshnessTime;
			result.ParamDesc.DefaultRevocationUrlRetrievalTimeout = allSSLData.DefaultRevocationUrlRetrievalTimeout;
			result.ParamDesc.pDefaultSslCtlIdentifier = allSSLData.pDefaultSslCtlIdentifier;
			result.ParamDesc.pDefaultSslCtlStoreName = allSSLData.pDefaultSslCtlStoreName;
			result.ParamDesc.DefaultFlags = allSSLData.DefaultFlags;
			return result;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00009E40 File Offset: 0x00008E40
		public static uint Terminate()
		{
			uint result = 0U;
			if (HttpApiWrapper._httpInitializedAtLeastOnce)
			{
				result = HttpApiWrapper.HttpTerminate(3U, IntPtr.Zero);
			}
			return result;
		}

		// Token: 0x0600039D RID: 925
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern uint HttpInitialize(HttpApiWrapper.HTTPAPI_VERSION version, uint flags, IntPtr reserved);

		// Token: 0x0600039E RID: 926
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern uint HttpTerminate(uint flags, IntPtr reserved);

		// Token: 0x0600039F RID: 927
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern uint HttpQueryServiceConfiguration(IntPtr serviceHandle, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID configID, ref HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_QUERY pInputConfigInfo, uint InputConfigInfoLength, SafeGlobalAllocHandle pOutputConfigInfo, uint OutputConfigInfoLength, [In] [Out] ref uint pReturnLength, IntPtr pOverlapped);

		// Token: 0x060003A0 RID: 928
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern uint HttpSetServiceConfiguration(IntPtr serviceHandle, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID configID, ref HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET pInputConfigInfo, uint InputConfigInfoLength, IntPtr pOverlapped);

		// Token: 0x060003A1 RID: 929
		[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern uint HttpDeleteServiceConfiguration(IntPtr serviceHandle, HttpApiWrapper.HTTP_SERVICE_CONFIG_ID configID, ref HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET pInputConfigInfo, uint InputConfigInfoLength, IntPtr pOverlapped);

		// Token: 0x0400013B RID: 315
		private const string IisCertStoreName = "MY";

		// Token: 0x0400013C RID: 316
		public const uint ERROR_SUCCESS = 0U;

		// Token: 0x0400013D RID: 317
		public const uint ERROR_FILE_NOT_FOUND = 2U;

		// Token: 0x0400013E RID: 318
		public const uint ERROR_NOT_SUPPORTED = 50U;

		// Token: 0x0400013F RID: 319
		public const uint ERROR_INSUFFICIENT_BUFFER = 122U;

		// Token: 0x04000140 RID: 320
		public const uint ERROR_ALREADY_EXISTS = 183U;

		// Token: 0x04000141 RID: 321
		public const uint ERROR_NO_MORE_ITEMS = 259U;

		// Token: 0x04000142 RID: 322
		public const uint HTTP_INITIALIZE_SERVER = 1U;

		// Token: 0x04000143 RID: 323
		public const uint HTTP_INITIALIZE_CONFIG = 2U;

		// Token: 0x04000144 RID: 324
		public const uint HTTP_SERVICE_CONFIG_SSL_FLAG_USE_DS_MAPPER = 1U;

		// Token: 0x04000145 RID: 325
		public const uint HTTP_SERVICE_CONFIG_SSL_FLAG_NEGOTIATE_CLIENT_CERT = 2U;

		// Token: 0x04000146 RID: 326
		private static readonly char[] _hexValues = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x04000147 RID: 327
		private static bool _httpInitializedAtLeastOnce;

		// Token: 0x04000148 RID: 328
		public static readonly Guid IisAppId = new Guid("4DC3E181-E14B-4a21-B022-59FC669B0914");

		// Token: 0x02000080 RID: 128
		public enum HttpPropertyName
		{
			// Token: 0x0400014A RID: 330
			SSLCertHash,
			// Token: 0x0400014B RID: 331
			SSLStoreName,
			// Token: 0x0400014C RID: 332
			AppId,
			// Token: 0x0400014D RID: 333
			CertCheckMode,
			// Token: 0x0400014E RID: 334
			RevocationFreshnessTime,
			// Token: 0x0400014F RID: 335
			RevocationURLRetrievalTimeout,
			// Token: 0x04000150 RID: 336
			SslCtlIdentifier,
			// Token: 0x04000151 RID: 337
			SslCtlStoreName,
			// Token: 0x04000152 RID: 338
			SSLUseDsMapper,
			// Token: 0x04000153 RID: 339
			SSLAlwaysNegoClientCert,
			// Token: 0x04000154 RID: 340
			DefaultFlags
		}

		// Token: 0x02000081 RID: 129
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HTTPAPI_VERSION
		{
			// Token: 0x04000155 RID: 341
			public ushort Major;

			// Token: 0x04000156 RID: 342
			public ushort Minor;
		}

		// Token: 0x02000082 RID: 130
		public enum HTTP_SERVICE_CONFIG_ID
		{
			// Token: 0x04000158 RID: 344
			HttpServiceConfigIPListenList,
			// Token: 0x04000159 RID: 345
			HttpServiceConfigSSLCertInfo,
			// Token: 0x0400015A RID: 346
			HttpServiceConfigUrlAclInfo,
			// Token: 0x0400015B RID: 347
			HttpServiceConfigMax
		}

		// Token: 0x02000083 RID: 131
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HTTP_SERVICE_CONFIG_SSL_PARAM
		{
			// Token: 0x0400015C RID: 348
			public uint SslHashLength;

			// Token: 0x0400015D RID: 349
			public IntPtr pSslHash;

			// Token: 0x0400015E RID: 350
			public Guid AppId;

			// Token: 0x0400015F RID: 351
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pSslCertStoreName;

			// Token: 0x04000160 RID: 352
			public uint DefaultCertCheckMode;

			// Token: 0x04000161 RID: 353
			public uint DefaultRevocationFreshnessTime;

			// Token: 0x04000162 RID: 354
			public uint DefaultRevocationUrlRetrievalTimeout;

			// Token: 0x04000163 RID: 355
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pDefaultSslCtlIdentifier;

			// Token: 0x04000164 RID: 356
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pDefaultSslCtlStoreName;

			// Token: 0x04000165 RID: 357
			public uint DefaultFlags;
		}

		// Token: 0x02000084 RID: 132
		public enum HTTP_SERVICE_CONFIG_QUERY_TYPE
		{
			// Token: 0x04000167 RID: 359
			HttpServiceConfigQueryExact,
			// Token: 0x04000168 RID: 360
			HttpServiceConfigQueryNext,
			// Token: 0x04000169 RID: 361
			HttpServiceConfigQueryMax
		}

		// Token: 0x02000085 RID: 133
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HTTP_SERVICE_CONFIG_SSL_KEY
		{
			// Token: 0x0400016A RID: 362
			public IntPtr pIpPort;
		}

		// Token: 0x02000086 RID: 134
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HTTP_SERVICE_CONFIG_SSL_QUERY
		{
			// Token: 0x0400016B RID: 363
			public HttpApiWrapper.HTTP_SERVICE_CONFIG_QUERY_TYPE QueryDesc;

			// Token: 0x0400016C RID: 364
			public HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_KEY KeyDesc;

			// Token: 0x0400016D RID: 365
			public int dwToken;
		}

		// Token: 0x02000087 RID: 135
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HTTP_SERVICE_CONFIG_SSL_SET
		{
			// Token: 0x0400016E RID: 366
			public HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_KEY KeyDesc;

			// Token: 0x0400016F RID: 367
			public HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_PARAM ParamDesc;
		}

		// Token: 0x02000088 RID: 136
		public class HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED
		{
			// Token: 0x060003A3 RID: 931 RVA: 0x00009EB0 File Offset: 0x00008EB0
			public HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED()
			{
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00009EB8 File Offset: 0x00008EB8
			public HTTP_SERVICE_CONFIG_SSL_PARAM_MANAGED(HttpApiWrapper.HTTP_SERVICE_CONFIG_SSL_SET nativeStructure)
			{
				this.SslHashLength = nativeStructure.ParamDesc.SslHashLength;
				this.pSslHash = new byte[nativeStructure.ParamDesc.SslHashLength];
				Marshal.Copy(nativeStructure.ParamDesc.pSslHash, this.pSslHash, 0, (int)nativeStructure.ParamDesc.SslHashLength);
				this.AppId = nativeStructure.ParamDesc.AppId;
				this.pSslCertStoreName = nativeStructure.ParamDesc.pSslCertStoreName;
				this.DefaultCertCheckMode = nativeStructure.ParamDesc.DefaultCertCheckMode;
				this.DefaultRevocationFreshnessTime = nativeStructure.ParamDesc.DefaultRevocationFreshnessTime;
				this.DefaultRevocationUrlRetrievalTimeout = nativeStructure.ParamDesc.DefaultRevocationUrlRetrievalTimeout;
				this.pDefaultSslCtlIdentifier = nativeStructure.ParamDesc.pDefaultSslCtlIdentifier;
				this.pDefaultSslCtlStoreName = nativeStructure.ParamDesc.pDefaultSslCtlStoreName;
				this.DefaultFlags = nativeStructure.ParamDesc.DefaultFlags;
			}

			// Token: 0x04000170 RID: 368
			public uint SslHashLength;

			// Token: 0x04000171 RID: 369
			public byte[] pSslHash;

			// Token: 0x04000172 RID: 370
			public Guid AppId;

			// Token: 0x04000173 RID: 371
			public string pSslCertStoreName;

			// Token: 0x04000174 RID: 372
			public uint DefaultCertCheckMode;

			// Token: 0x04000175 RID: 373
			public uint DefaultRevocationFreshnessTime;

			// Token: 0x04000176 RID: 374
			public uint DefaultRevocationUrlRetrievalTimeout;

			// Token: 0x04000177 RID: 375
			public string pDefaultSslCtlIdentifier;

			// Token: 0x04000178 RID: 376
			public string pDefaultSslCtlStoreName;

			// Token: 0x04000179 RID: 377
			public uint DefaultFlags;
		}
	}
}
