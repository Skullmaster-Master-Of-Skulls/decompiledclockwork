using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000026 RID: 38
	internal static class Rfc6238AuthenticationService
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000366C File Offset: 0x0000186C
		private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string modifier)
		{
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
			byte[] array = hashAlgorithm.ComputeHash(Rfc6238AuthenticationService.ApplyModifier(bytes, modifier));
			int num = (int)(array[array.Length - 1] & 15);
			int num2 = (int)(array[num] & 127) << 24 | (int)(array[num + 1] & byte.MaxValue) << 16 | (int)(array[num + 2] & byte.MaxValue) << 8 | (int)(array[num + 3] & byte.MaxValue);
			return num2 % 1000000;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036D8 File Offset: 0x000018D8
		private static byte[] ApplyModifier(byte[] input, string modifier)
		{
			if (string.IsNullOrEmpty(modifier))
			{
				return input;
			}
			byte[] bytes = Rfc6238AuthenticationService._encoding.GetBytes(modifier);
			byte[] array = new byte[checked(input.Length + bytes.Length)];
			Buffer.BlockCopy(input, 0, array, 0, input.Length);
			Buffer.BlockCopy(bytes, 0, array, input.Length, bytes.Length);
			return array;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003724 File Offset: 0x00001924
		private static ulong GetCurrentTimeStepNumber()
		{
			return (ulong)((DateTime.UtcNow - Rfc6238AuthenticationService._unixEpoch).Ticks / Rfc6238AuthenticationService._timestep.Ticks);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003758 File Offset: 0x00001958
		public static int GenerateCode(SecurityToken securityToken, string modifier = null)
		{
			if (securityToken == null)
			{
				throw new ArgumentNullException("securityToken");
			}
			ulong currentTimeStepNumber = Rfc6238AuthenticationService.GetCurrentTimeStepNumber();
			int result;
			using (HMACSHA1 hmacsha = new HMACSHA1(securityToken.GetDataNoClone()))
			{
				result = Rfc6238AuthenticationService.ComputeTotp(hmacsha, currentTimeStepNumber, modifier);
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000037AC File Offset: 0x000019AC
		public static bool ValidateCode(SecurityToken securityToken, int code, string modifier = null)
		{
			if (securityToken == null)
			{
				throw new ArgumentNullException("securityToken");
			}
			ulong currentTimeStepNumber = Rfc6238AuthenticationService.GetCurrentTimeStepNumber();
			using (HMACSHA1 hmacsha = new HMACSHA1(securityToken.GetDataNoClone()))
			{
				for (int i = -2; i <= 2; i++)
				{
					int num = Rfc6238AuthenticationService.ComputeTotp(hmacsha, currentTimeStepNumber + (ulong)((long)i), modifier);
					if (num == code)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000014 RID: 20
		private static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000015 RID: 21
		private static readonly TimeSpan _timestep = TimeSpan.FromMinutes(3.0);

		// Token: 0x04000016 RID: 22
		private static readonly Encoding _encoding = new UTF8Encoding(false, true);
	}
}
