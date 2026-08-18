using System;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MailBee;
using MailBee.AddressCheck;
using MailBee.AntiSpam;
using MailBee.EwsMail;
using MailBee.ImapMail;
using MailBee.Outlook;
using MailBee.Pdf;
using MailBee.Pop3Mail;
using MailBee.Security;
using MailBee.SmtpMail;
using Microsoft.Win32;

namespace a
{
	// Token: 0x020004A2 RID: 1186
	internal class bn
	{
		// Token: 0x0600286B RID: 10347 RVA: 0x000BC4A9 File Offset: 0x000BB4A9
		protected bn()
		{
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000BC4B4 File Offset: 0x000BB4B4
		private static bool a(string A_0, Type A_1, out bg A_2)
		{
			bool result = false;
			A_2.a = f.g;
			A_2.b = 0;
			A_2.c = 0;
			if (A_0 == null || A_0 == string.Empty)
			{
				return result;
			}
			A_0 = A_0.Trim();
			int num = A_0.IndexOf('-', 0);
			if (num < 0)
			{
				return result;
			}
			int num2 = A_0.IndexOf('-', num + 1);
			if (num2 < 0)
			{
				return result;
			}
			string text = A_0.Substring(0, num);
			if (text.StartsWith("MBC"))
			{
				A_2.a = f.f;
				return result;
			}
			if (text.Length != 5)
			{
				return result;
			}
			if (text.Substring(0, 2) != "MN")
			{
				return result;
			}
			string value = text.Substring(2);
			int num3 = 0;
			try
			{
				num3 = Convert.ToInt32(value);
			}
			catch (Exception)
			{
				return result;
			}
			if (num3 < 110 || num3 >= 200)
			{
				A_2.a = f.e;
				return result;
			}
			byte[] array = bn.c(A_0.Substring(num + 1, num2 - (num + 1)));
			if (array.Length < 2)
			{
				return result;
			}
			byte[] array2 = new byte[array.Length];
			Array.Copy(array, 0, array2, 0, array2.Length);
			byte b = array[1];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array3 = array;
				int num4 = i;
				array3[num4] ^= b;
			}
			array[1] = b;
			byte[] array4 = new byte[array.Length / 2];
			if (array4.Length < 7)
			{
				return result;
			}
			int num5 = 0;
			for (int j = 0; j < array.Length; j += 2)
			{
				array4[num5] = array[j];
				num5++;
			}
			byte[] array5 = new byte[4];
			Array.Copy(array4, 1, array5, 0, array5.Length);
			int num6 = BitConverter.ToInt32(array5, 0);
			byte[] array6 = bn.c(A_0.Substring(num2 + 1, A_0.Length - (num2 + 1)));
			byte[] array7 = bn.d(array2);
			if (array6.Length != array7.Length)
			{
				return result;
			}
			for (int k = 0; k < array6.Length; k++)
			{
				if (array6[k] != array7[k])
				{
					return result;
				}
			}
			if (!bn.a(num6, A_1))
			{
				byte[] array8 = new byte[2];
				Array.Copy(array4, 1 + array5.Length, array8, 0, array8.Length);
				short num7 = BitConverter.ToInt16(array8, 0);
				DateTime d = new DateTime(2000, 1, 1);
				int num8 = (DateTime.Now - d).Days - (int)num7;
				if (num8 > 30)
				{
					A_2.a = f.d;
					return result;
				}
				A_2.a = f.c;
				A_2.b = 30 - num8;
				A_2.c = 511;
			}
			else
			{
				A_2.a = f.a;
				A_2.c = num6;
			}
			return true;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000BC74C File Offset: 0x000BB74C
		public static bm a(string A_0, Type A_1)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				try
				{
					A_0 = ConfigurationSettings.AppSettings[string.Format("{0}.LicenseKey", "MailBee.Global")];
				}
				catch (ConfigurationException)
				{
				}
			}
			if (A_0 == null || A_0 == string.Empty)
			{
				try
				{
					A_0 = ConfigurationSettings.AppSettings[string.Format("{0}.LicenseKey", A_1.FullName)];
				}
				catch (ConfigurationException)
				{
				}
			}
			if (Global.IsWindows)
			{
				if (A_0 == null || A_0 == string.Empty)
				{
					A_0 = bn.b("MailBee.Global");
				}
				if (A_0 == null || A_0 == string.Empty)
				{
					A_0 = bn.b(A_1.FullName);
				}
			}
			bg a_;
			if (bn.a(A_0, A_1, out a_))
			{
				return new bm(A_0, a_);
			}
			throw new MailBeeLicenseException(new bm(null, a_), A_1);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000BC838 File Offset: 0x000BB838
		private static byte[] d(byte[] A_0)
		{
			byte[] array = new SHA1CryptoServiceProvider().ComputeHash(A_0);
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array2 = array;
				int num = i;
				array2[num] ^= 23;
			}
			array = bn.c(array);
			byte[] array3 = new byte[2];
			Array.Copy(array, 0, array3, 0, 2);
			return array3;
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000BC888 File Offset: 0x000BB888
		private static byte[] c(byte[] A_0)
		{
			int num = 0;
			for (int i = A_0.Length - 1; i >= 0; i--)
			{
				int num2 = (int)(A_0[i] * 2);
				if (num == 1)
				{
					num2 += num;
				}
				if (num2 > 255)
				{
					A_0[i] = Convert.ToByte(num2 & 255);
					num = 1;
				}
				else
				{
					A_0[i] = Convert.ToByte(num2);
					num = 0;
				}
			}
			if (num == 1)
			{
				int num3 = A_0.Length - 1;
				A_0[num3] += 1;
			}
			return A_0;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000BC8F4 File Offset: 0x000BB8F4
		private static byte[] c(string A_0)
		{
			byte[] array = new byte[0];
			if (A_0.Length % 2 == 0)
			{
				array = new byte[A_0.Length / 2];
				int num = 0;
				for (int i = 0; i < A_0.Length; i += 2)
				{
					try
					{
						byte b = byte.Parse(A_0.Substring(i, 2), NumberStyles.HexNumber);
						array[num] = b;
						num++;
					}
					catch (FormatException)
					{
						return array;
					}
				}
			}
			return array;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000BC96C File Offset: 0x000BB96C
		private static string b(byte[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in A_0)
			{
				string text = b.ToString("X");
				stringBuilder.Append((text.Length > 1) ? text : ("0" + text));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000BC9C8 File Offset: 0x000BB9C8
		public static bool a(int A_0, Type A_1)
		{
			if (A_1 == typeof(Smtp))
			{
				return (A_0 & 2) > 0;
			}
			if (A_1 == typeof(Pop3))
			{
				return (A_0 & 1) > 0;
			}
			if (A_1 == typeof(Imap))
			{
				return (A_0 & 4) > 0;
			}
			if (A_1 == typeof(Powerup))
			{
				return (A_0 & 8) > 0;
			}
			if (A_1 == typeof(BayesFilter))
			{
				return (A_0 & 16) > 0;
			}
			if (A_1 == typeof(MsgConvert) || A_1 == typeof(PstReader))
			{
				return (A_0 & 32) > 0;
			}
			if (A_1 == typeof(HtmlToPdf))
			{
				return (A_0 & 64) > 0;
			}
			if (A_1 == typeof(EmailAddressValidator))
			{
				return (A_0 & 128) > 0;
			}
			if (A_1 == typeof(Ews))
			{
				return (A_0 & 256) > 0;
			}
			return A_1 == typeof(Global) && (A_0 & 511) > 0;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000BCB10 File Offset: 0x000BBB10
		private static string b(string A_0)
		{
			string text = bn.a(A_0, false);
			if ((text == null || text == string.Empty) && Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
			{
				text = bn.a(A_0, true);
			}
			return text;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000BCB4C File Offset: 0x000BBB4C
		private static string a(string A_0, bool A_1)
		{
			RegistryKey registryKey = Registry.LocalMachine;
			if (A_1)
			{
				try
				{
					registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				}
				catch (Exception)
				{
					return string.Empty;
				}
			}
			string a_ = string.Empty;
			try
			{
				registryKey = bn.a(registryKey, "Software", false);
				registryKey = bn.a(registryKey, "AfterLogic", false);
				registryKey = bn.a(registryKey, "MailBee.NET Objects", false);
				if (registryKey != null)
				{
					a_ = (string)registryKey.GetValue(string.Format("{0}.LicenseKey", A_0));
				}
			}
			catch (Exception)
			{
				return string.Empty;
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return bn.a(a_);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000BCC0C File Offset: 0x000BBC0C
		private static RegistryKey a(RegistryKey A_0, string A_1, bool A_2)
		{
			if (A_0 == null)
			{
				return A_0;
			}
			return A_0.OpenSubKey(A_1, A_2);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000BCC1C File Offset: 0x000BBC1C
		private static string a(string A_0)
		{
			if (A_0 == null)
			{
				return string.Empty;
			}
			Match match = new Regex("(?<firstPart>\\w{5})-(?<secondPart>\\w{28})-(?<thirdPart>\\w{4})", RegexOptions.IgnoreCase).Match(A_0);
			if (match.Success)
			{
				string value = match.Groups["firstPart"].Value;
				string text = match.Groups["secondPart"].Value;
				string text2 = match.Groups["thirdPart"].Value;
				byte[] a_ = bn.c(text);
				byte[] a_2 = bn.c(text2);
				text = bn.a(a_);
				text2 = bn.a(a_2);
				return string.Format("{0}-{1}-{2}", value, text, text2);
			}
			return A_0;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000BCCC0 File Offset: 0x000BBCC0
		private static string a(byte[] A_0)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				int num = i;
				A_0[num] ^= Convert.ToByte('@');
			}
			return bn.b(A_0);
		}
	}
}
