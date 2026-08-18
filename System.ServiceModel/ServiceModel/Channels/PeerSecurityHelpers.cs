using System;
using System.IdentityModel.Claims;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A23 RID: 2595
	internal class PeerSecurityHelpers
	{
		// Token: 0x0600672C RID: 26412 RVA: 0x001817CC File Offset: 0x0017F9CC
		public static byte[] ComputeHash(X509Certificate2 cert, string pwd)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = cert.PublicKey.Key as RSACryptoServiceProvider;
			byte[] message = rsacryptoServiceProvider.ExportCspBlob(false);
			return PeerSecurityHelpers.ComputeHash(message, pwd);
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x001817FC File Offset: 0x0017F9FC
		public static byte[] ComputeHash(Claim claim, string pwd)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = claim.Resource as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			byte[] result;
			using (rsacryptoServiceProvider)
			{
				byte[] array = rsacryptoServiceProvider.ExportCspBlob(false);
				if (array == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
				}
				result = PeerSecurityHelpers.ComputeHash(array, pwd);
			}
			return result;
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x0018186C File Offset: 0x0017FA6C
		public static byte[] ComputeHash(byte[] message, string pwd)
		{
			byte[] result = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			try
			{
				array = Encoding.Unicode.GetBytes(pwd.Trim());
				using (HMACSHA256 hmacsha = new HMACSHA256(array))
				{
					using (SHA256Managed sha256Managed = new SHA256Managed())
					{
						array2 = sha256Managed.ComputeHash(array);
						array3 = DiagnosticUtility.Utility.AllocateByteArray(checked(message.Length + array2.Length));
						Array.Copy(array2, array3, array2.Length);
						Array.Copy(message, 0, array3, array2.Length, message.Length);
						result = hmacsha.ComputeHash(array3);
					}
				}
			}
			finally
			{
				PeerSecurityHelpers.ArrayClear(array);
				PeerSecurityHelpers.ArrayClear(array2);
				PeerSecurityHelpers.ArrayClear(array3);
			}
			return result;
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x00181938 File Offset: 0x0017FB38
		private static void ArrayClear(byte[] buffer)
		{
			if (buffer != null)
			{
				Array.Clear(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x00181948 File Offset: 0x0017FB48
		public static bool Authenticate(Claim claim, string password, byte[] authenticator)
		{
			bool result = false;
			if (authenticator == null)
			{
				return false;
			}
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				array = PeerSecurityHelpers.ComputeHash(claim, password);
				if (array.Length == authenticator.Length)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != authenticator[i])
						{
							result = false;
							break;
						}
					}
					result = true;
				}
			}
			finally
			{
				PeerSecurityHelpers.ArrayClear(array);
			}
			return result;
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x001819AC File Offset: 0x0017FBAC
		public static bool AuthenticateRequest(Claim claim, string password, Message message)
		{
			PeerHashToken peerHashToken = PeerRequestSecurityToken.CreateHashTokenFrom(message);
			return peerHashToken.Validate(claim, password);
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x001819C8 File Offset: 0x0017FBC8
		public static bool AuthenticateResponse(Claim claim, string password, Message message)
		{
			PeerHashToken peerHashToken = PeerRequestSecurityTokenResponse.CreateHashTokenFrom(message);
			return peerHashToken.Validate(claim, password);
		}
	}
}
