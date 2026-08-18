using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x020001AD RID: 429
	internal static class HttpDigest
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x0005AE6C File Offset: 0x0005906C
		static HttpDigest()
		{
			HttpDigest.ReadSuppressExtendedProtectionRegistryValue();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005AF98 File Offset: 0x00059198
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Lsa")]
		private static void ReadSuppressExtendedProtectionRegistryValue()
		{
			HttpDigest.suppressExtendedProtection = !ComNetOS.IsWin7orLater;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa"))
				{
					try
					{
						if (registryKey.GetValueKind("SuppressExtendedProtection") == RegistryValueKind.DWord)
						{
							HttpDigest.suppressExtendedProtection = ((int)registryKey.GetValue("SuppressExtendedProtection") == 1);
						}
					}
					catch (UnauthorizedAccessException ex)
					{
						if (Logging.On)
						{
							Logging.PrintWarning(Logging.Web, typeof(HttpDigest), "ReadSuppressExtendedProtectionRegistryValue", ex.Message);
						}
					}
					catch (IOException ex2)
					{
						if (Logging.On)
						{
							Logging.PrintWarning(Logging.Web, typeof(HttpDigest), "ReadSuppressExtendedProtectionRegistryValue", ex2.Message);
						}
					}
				}
			}
			catch (SecurityException ex3)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, typeof(HttpDigest), "ReadSuppressExtendedProtectionRegistryValue", ex3.Message);
				}
			}
			catch (ObjectDisposedException ex4)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, typeof(HttpDigest), "ReadSuppressExtendedProtectionRegistryValue", ex4.Message);
				}
			}
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005B0E4 File Offset: 0x000592E4
		internal static HttpDigestChallenge Interpret(string challenge, int startingPoint, HttpWebRequest httpWebRequest)
		{
			HttpDigestChallenge httpDigestChallenge = new HttpDigestChallenge();
			httpDigestChallenge.SetFromRequest(httpWebRequest);
			startingPoint = ((startingPoint == -1) ? 0 : (startingPoint + DigestClient.SignatureSize));
			int num = startingPoint;
			for (;;)
			{
				int num2 = num;
				int num3 = AuthenticationManager.SplitNoQuotes(challenge, ref num2);
				if (num2 < 0)
				{
					goto IL_9E;
				}
				string text = challenge.Substring(num, num2 - num);
				if (string.Compare(text, "charset", StringComparison.OrdinalIgnoreCase) == 0)
				{
					string text2;
					if (num3 < 0)
					{
						text2 = HttpDigest.unquote(challenge.Substring(num2 + 1));
					}
					else
					{
						text2 = HttpDigest.unquote(challenge.Substring(num2 + 1, num3 - num2 - 1));
					}
					if (string.Compare(text2, "utf-8", StringComparison.OrdinalIgnoreCase) == 0)
					{
						break;
					}
				}
				if (num3 < 0)
				{
					goto IL_9E;
				}
				num = num3 + 1;
			}
			httpDigestChallenge.UTF8Charset = true;
			IL_9E:
			num = startingPoint;
			for (;;)
			{
				int num2 = num;
				int num3 = AuthenticationManager.SplitNoQuotes(challenge, ref num2);
				if (num2 < 0)
				{
					break;
				}
				string text = challenge.Substring(num, num2 - num);
				string text2;
				if (num3 < 0)
				{
					text2 = HttpDigest.unquote(challenge.Substring(num2 + 1));
				}
				else
				{
					text2 = HttpDigest.unquote(challenge.Substring(num2 + 1, num3 - num2 - 1));
				}
				if (httpDigestChallenge.UTF8Charset)
				{
					bool flag = true;
					for (int i = 0; i < text2.Length; i++)
					{
						if (text2[i] > '\u007f')
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						byte[] array = new byte[text2.Length];
						for (int j = 0; j < text2.Length; j++)
						{
							array[j] = (byte)text2[j];
						}
						text2 = Encoding.UTF8.GetString(array);
					}
				}
				bool flag2 = httpDigestChallenge.defineAttribute(text, text2);
				if (num3 < 0 || !flag2)
				{
					break;
				}
				num = num3 + 1;
			}
			if (httpDigestChallenge.Nonce == null)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, SR.GetString("net_log_digest_requires_nonce"));
				}
				return null;
			}
			return httpDigestChallenge;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0005B29C File Offset: 0x0005949C
		private static string CharsetEncode(string rawString, HttpDigest.Charset charset)
		{
			if (charset == HttpDigest.Charset.UTF8 || charset == HttpDigest.Charset.ANSI)
			{
				byte[] array = (charset == HttpDigest.Charset.UTF8) ? Encoding.UTF8.GetBytes(rawString) : Encoding.Default.GetBytes(rawString);
				char[] array2 = new char[array.Length];
				array.CopyTo(array2, 0);
				rawString = new string(array2);
			}
			return rawString;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0005B2E8 File Offset: 0x000594E8
		private static HttpDigest.Charset DetectCharset(string rawString)
		{
			HttpDigest.Charset result = HttpDigest.Charset.ASCII;
			for (int i = 0; i < rawString.Length; i++)
			{
				if (rawString[i] > '\u007f')
				{
					byte[] bytes = Encoding.Default.GetBytes(rawString);
					string @string = Encoding.Default.GetString(bytes);
					result = ((string.Compare(rawString, @string, StringComparison.Ordinal) == 0) ? HttpDigest.Charset.ANSI : HttpDigest.Charset.UTF8);
					break;
				}
			}
			return result;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0005B340 File Offset: 0x00059540
		internal static Authorization Authenticate(HttpDigestChallenge digestChallenge, NetworkCredential NC, string spn, ChannelBinding binding)
		{
			string text = NC.InternalGetUserName();
			if (ValidationHelper.IsBlankString(text))
			{
				return null;
			}
			string text2 = NC.InternalGetPassword();
			bool flag = HttpDigest.IsUpgraded(digestChallenge.Nonce, binding);
			if (flag)
			{
				digestChallenge.ServiceName = spn;
				digestChallenge.ChannelBinding = HttpDigest.hashChannelBinding(binding, digestChallenge.MD5provider);
			}
			if (digestChallenge.QopPresent)
			{
				if (digestChallenge.ClientNonce == null || digestChallenge.Stale)
				{
					if (flag)
					{
						digestChallenge.ClientNonce = HttpDigest.createUpgradedNonce(digestChallenge);
					}
					else
					{
						digestChallenge.ClientNonce = HttpDigest.createNonce(32);
					}
					digestChallenge.NonceCount = 1;
				}
				else
				{
					digestChallenge.NonceCount++;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			HttpDigest.Charset charset = HttpDigest.DetectCharset(text);
			if (!digestChallenge.UTF8Charset && charset == HttpDigest.Charset.UTF8)
			{
				return null;
			}
			HttpDigest.Charset charset2 = HttpDigest.DetectCharset(text2);
			if (!digestChallenge.UTF8Charset && charset2 == HttpDigest.Charset.UTF8)
			{
				return null;
			}
			if (digestChallenge.UTF8Charset)
			{
				stringBuilder.Append(HttpDigest.pair("charset", "utf-8", false));
				stringBuilder.Append(",");
				if (charset == HttpDigest.Charset.UTF8)
				{
					text = HttpDigest.CharsetEncode(text, HttpDigest.Charset.UTF8);
					stringBuilder.Append(HttpDigest.pair("username", text, true));
					stringBuilder.Append(",");
				}
				else
				{
					stringBuilder.Append(HttpDigest.pair("username", HttpDigest.CharsetEncode(text, HttpDigest.Charset.UTF8), true));
					stringBuilder.Append(",");
					text = HttpDigest.CharsetEncode(text, charset);
				}
			}
			else
			{
				text = HttpDigest.CharsetEncode(text, charset);
				stringBuilder.Append(HttpDigest.pair("username", text, true));
				stringBuilder.Append(",");
			}
			text2 = HttpDigest.CharsetEncode(text2, charset2);
			stringBuilder.Append(HttpDigest.pair("realm", digestChallenge.Realm, true));
			stringBuilder.Append(",");
			stringBuilder.Append(HttpDigest.pair("nonce", digestChallenge.Nonce, true));
			stringBuilder.Append(",");
			stringBuilder.Append(HttpDigest.pair("uri", digestChallenge.Uri, true));
			if (digestChallenge.QopPresent)
			{
				if (digestChallenge.Algorithm != null)
				{
					stringBuilder.Append(",");
					stringBuilder.Append(HttpDigest.pair("algorithm", digestChallenge.Algorithm, true));
				}
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("cnonce", digestChallenge.ClientNonce, true));
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("nc", digestChallenge.NonceCount.ToString("x8", NumberFormatInfo.InvariantInfo), false));
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("qop", "auth", true));
				if (flag)
				{
					stringBuilder.Append(",");
					stringBuilder.Append(HttpDigest.pair("hashed-dirs", "service-name,channel-binding", true));
					stringBuilder.Append(",");
					stringBuilder.Append(HttpDigest.pair("service-name", digestChallenge.ServiceName, true));
					stringBuilder.Append(",");
					stringBuilder.Append(HttpDigest.pair("channel-binding", digestChallenge.ChannelBinding, true));
				}
			}
			string text3 = HttpDigest.responseValue(digestChallenge, text, text2);
			if (text3 == null)
			{
				return null;
			}
			stringBuilder.Append(",");
			stringBuilder.Append(HttpDigest.pair("response", text3, true));
			if (digestChallenge.Opaque != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("opaque", digestChallenge.Opaque, true));
			}
			return new Authorization("Digest " + stringBuilder.ToString(), false);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0005B6C1 File Offset: 0x000598C1
		private static bool IsUpgraded(string nonce, ChannelBinding binding)
		{
			return (binding != null || !HttpDigest.suppressExtendedProtection) && AuthenticationManager.SspSupportsExtendedProtection && nonce.StartsWith("+Upgraded+", StringComparison.Ordinal);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0005B6E6 File Offset: 0x000598E6
		internal static string unquote(string quotedString)
		{
			return quotedString.Trim().Trim("\"".ToCharArray());
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0005B6FD File Offset: 0x000598FD
		internal static string pair(string name, string value, bool quote)
		{
			if (quote)
			{
				return name + "=\"" + value + "\"";
			}
			return name + "=" + value;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0005B720 File Offset: 0x00059920
		private static string responseValue(HttpDigestChallenge challenge, string username, string password)
		{
			string text = HttpDigest.computeSecret(challenge, username, password);
			if (text == null)
			{
				return null;
			}
			string text2 = challenge.Method + ":" + challenge.Uri;
			if (text2 == null)
			{
				return null;
			}
			string str = HttpDigest.hashString(text, challenge.MD5provider);
			string text3 = HttpDigest.hashString(text2, challenge.MD5provider);
			string str2 = challenge.Nonce + ":" + (challenge.QopPresent ? string.Concat(new string[]
			{
				challenge.NonceCount.ToString("x8", NumberFormatInfo.InvariantInfo),
				":",
				challenge.ClientNonce,
				":auth:",
				text3
			}) : text3);
			return HttpDigest.hashString(str + ":" + str2, challenge.MD5provider);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0005B7E8 File Offset: 0x000599E8
		private static string computeSecret(HttpDigestChallenge challenge, string username, string password)
		{
			if (challenge.Algorithm == null || string.Compare(challenge.Algorithm, "md5", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return string.Concat(new string[]
				{
					username,
					":",
					challenge.Realm,
					":",
					password
				});
			}
			if (string.Compare(challenge.Algorithm, "md5-sess", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return string.Concat(new string[]
				{
					HttpDigest.hashString(string.Concat(new string[]
					{
						username,
						":",
						challenge.Realm,
						":",
						password
					}), challenge.MD5provider),
					":",
					challenge.Nonce,
					":",
					challenge.ClientNonce
				});
			}
			if (Logging.On)
			{
				Logging.PrintError(Logging.Web, SR.GetString("net_log_digest_hash_algorithm_not_supported", new object[]
				{
					challenge.Algorithm
				}));
			}
			return null;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		private static byte[] formatChannelBindingForHash(ChannelBinding binding)
		{
			int value = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.InitiatorTypeOffset);
			int num = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.InitiatorLengthOffset);
			int value2 = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.AcceptorTypeOffset);
			int num2 = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.AcceptorLengthOffset);
			int num3 = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.ApplicationDataLengthOffset);
			byte[] array = new byte[HttpDigest.MinimumFormattedBindingLength + num + num2 + num3];
			BitConverter.GetBytes(value).CopyTo(array, 0);
			BitConverter.GetBytes(num).CopyTo(array, HttpDigest.SizeOfInt);
			int num4 = 2 * HttpDigest.SizeOfInt;
			if (num > 0)
			{
				int b = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.InitiatorOffsetOffset);
				Marshal.Copy(IntPtrHelper.Add(binding.DangerousGetHandle(), b), array, num4, num);
				num4 += num;
			}
			BitConverter.GetBytes(value2).CopyTo(array, num4);
			BitConverter.GetBytes(num2).CopyTo(array, num4 + HttpDigest.SizeOfInt);
			num4 += 2 * HttpDigest.SizeOfInt;
			if (num2 > 0)
			{
				int b2 = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.AcceptorOffsetOffset);
				Marshal.Copy(IntPtrHelper.Add(binding.DangerousGetHandle(), b2), array, num4, num2);
				num4 += num2;
			}
			BitConverter.GetBytes(num3).CopyTo(array, num4);
			num4 += HttpDigest.SizeOfInt;
			if (num3 > 0)
			{
				int b3 = Marshal.ReadInt32(binding.DangerousGetHandle(), HttpDigest.ApplicationDataOffsetOffset);
				Marshal.Copy(IntPtrHelper.Add(binding.DangerousGetHandle(), b3), array, num4, num3);
			}
			return array;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0005BA64 File Offset: 0x00059C64
		private static string hashChannelBinding(ChannelBinding binding, MD5CryptoServiceProvider MD5provider)
		{
			if (binding == null)
			{
				return "00000000000000000000000000000000";
			}
			byte[] buffer = HttpDigest.formatChannelBindingForHash(binding);
			byte[] rawbytes = MD5provider.ComputeHash(buffer);
			return HttpDigest.hexEncode(rawbytes);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0005BA90 File Offset: 0x00059C90
		private static string hashString(string myString, MD5CryptoServiceProvider MD5provider)
		{
			byte[] array = new byte[myString.Length];
			for (int i = 0; i < myString.Length; i++)
			{
				array[i] = (byte)myString[i];
			}
			byte[] rawbytes = MD5provider.ComputeHash(array);
			return HttpDigest.hexEncode(rawbytes);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0005BAD8 File Offset: 0x00059CD8
		private static string hexEncode(byte[] rawbytes)
		{
			int num = rawbytes.Length;
			char[] array = new char[2 * num];
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				array[num2++] = Uri.HexLowerChars[rawbytes[i] >> 4];
				array[num2++] = Uri.HexLowerChars[(int)(rawbytes[i] & 15)];
				i++;
			}
			return new string(array);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0005BB2C File Offset: 0x00059D2C
		private static string createNonce(int length)
		{
			byte[] array = new byte[length];
			char[] array2 = new char[length];
			HttpDigest.RandomGenerator.GetBytes(array);
			for (int i = 0; i < length; i++)
			{
				array2[i] = Uri.HexLowerChars[(int)(array[i] & 15)];
			}
			return new string(array2);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0005BB78 File Offset: 0x00059D78
		private static string createUpgradedNonce(HttpDigestChallenge digestChallenge)
		{
			string s = digestChallenge.ServiceName + ":" + digestChallenge.ChannelBinding;
			byte[] rawbytes = digestChallenge.MD5provider.ComputeHash(Encoding.ASCII.GetBytes(s));
			return "+Upgraded+v1" + HttpDigest.hexEncode(rawbytes) + HttpDigest.createNonce(32);
		}

		// Token: 0x040013CB RID: 5067
		internal const string DA_algorithm = "algorithm";

		// Token: 0x040013CC RID: 5068
		internal const string DA_cnonce = "cnonce";

		// Token: 0x040013CD RID: 5069
		internal const string DA_domain = "domain";

		// Token: 0x040013CE RID: 5070
		internal const string DA_nc = "nc";

		// Token: 0x040013CF RID: 5071
		internal const string DA_nonce = "nonce";

		// Token: 0x040013D0 RID: 5072
		internal const string DA_opaque = "opaque";

		// Token: 0x040013D1 RID: 5073
		internal const string DA_qop = "qop";

		// Token: 0x040013D2 RID: 5074
		internal const string DA_realm = "realm";

		// Token: 0x040013D3 RID: 5075
		internal const string DA_response = "response";

		// Token: 0x040013D4 RID: 5076
		internal const string DA_stale = "stale";

		// Token: 0x040013D5 RID: 5077
		internal const string DA_uri = "uri";

		// Token: 0x040013D6 RID: 5078
		internal const string DA_username = "username";

		// Token: 0x040013D7 RID: 5079
		internal const string DA_charset = "charset";

		// Token: 0x040013D8 RID: 5080
		internal const string DA_cipher = "cipher";

		// Token: 0x040013D9 RID: 5081
		internal const string DA_hasheddirs = "hashed-dirs";

		// Token: 0x040013DA RID: 5082
		internal const string DA_servicename = "service-name";

		// Token: 0x040013DB RID: 5083
		internal const string DA_channelbinding = "channel-binding";

		// Token: 0x040013DC RID: 5084
		internal const string SupportedQuality = "auth";

		// Token: 0x040013DD RID: 5085
		internal const string ValidSeparator = ", \"'\t\r\n";

		// Token: 0x040013DE RID: 5086
		internal const string HashedDirs = "service-name,channel-binding";

		// Token: 0x040013DF RID: 5087
		internal const string Upgraded = "+Upgraded+";

		// Token: 0x040013E0 RID: 5088
		internal const string UpgradedV1 = "+Upgraded+v1";

		// Token: 0x040013E1 RID: 5089
		internal const string ZeroChannelBindingHash = "00000000000000000000000000000000";

		// Token: 0x040013E2 RID: 5090
		private const string suppressExtendedProtectionKey = "System\\CurrentControlSet\\Control\\Lsa";

		// Token: 0x040013E3 RID: 5091
		private const string suppressExtendedProtectionKeyPath = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Lsa";

		// Token: 0x040013E4 RID: 5092
		private const string suppressExtendedProtectionValueName = "SuppressExtendedProtection";

		// Token: 0x040013E5 RID: 5093
		private static volatile bool suppressExtendedProtection;

		// Token: 0x040013E6 RID: 5094
		private static readonly RNGCryptoServiceProvider RandomGenerator = new RNGCryptoServiceProvider();

		// Token: 0x040013E7 RID: 5095
		private static int InitiatorTypeOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "dwInitiatorAddrType");

		// Token: 0x040013E8 RID: 5096
		private static int InitiatorLengthOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "cbInitiatorLength");

		// Token: 0x040013E9 RID: 5097
		private static int InitiatorOffsetOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "dwInitiatorOffset");

		// Token: 0x040013EA RID: 5098
		private static int AcceptorTypeOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "dwAcceptorAddrType");

		// Token: 0x040013EB RID: 5099
		private static int AcceptorLengthOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "cbAcceptorLength");

		// Token: 0x040013EC RID: 5100
		private static int AcceptorOffsetOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "dwAcceptorOffset");

		// Token: 0x040013ED RID: 5101
		private static int ApplicationDataLengthOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "cbApplicationDataLength");

		// Token: 0x040013EE RID: 5102
		private static int ApplicationDataOffsetOffset = (int)Marshal.OffsetOf(typeof(SecChannelBindings), "dwApplicationDataOffset");

		// Token: 0x040013EF RID: 5103
		private static int SizeOfInt = Marshal.SizeOf(typeof(int));

		// Token: 0x040013F0 RID: 5104
		private static int MinimumFormattedBindingLength = 5 * HttpDigest.SizeOfInt;

		// Token: 0x0200074E RID: 1870
		private enum Charset
		{
			// Token: 0x04003201 RID: 12801
			ASCII,
			// Token: 0x04003202 RID: 12802
			ANSI,
			// Token: 0x04003203 RID: 12803
			UTF8
		}
	}
}
