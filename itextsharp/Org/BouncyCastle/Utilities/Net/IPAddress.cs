using System;
using System.Globalization;

namespace Org.BouncyCastle.Utilities.Net
{
	// Token: 0x020002E4 RID: 740
	public class IPAddress
	{
		// Token: 0x06001B72 RID: 7026 RVA: 0x000A5170 File Offset: 0x000A4170
		public static bool IsValid(string address)
		{
			return IPAddress.IsValidIPv4(address) || IPAddress.IsValidIPv6(address);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000A5182 File Offset: 0x000A4182
		public static bool IsValidWithNetMask(string address)
		{
			return IPAddress.IsValidIPv4WithNetmask(address) || IPAddress.IsValidIPv6WithNetmask(address);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000A5194 File Offset: 0x000A4194
		public static bool IsValidIPv4(string address)
		{
			try
			{
				return IPAddress.unsafeIsValidIPv4(address);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000A51D0 File Offset: 0x000A41D0
		private static bool unsafeIsValidIPv4(string address)
		{
			if (address.Length == 0)
			{
				return false;
			}
			int num = 0;
			string text = address + ".";
			int num2 = 0;
			int num3;
			while (num2 < text.Length && (num3 = text.IndexOf('.', num2)) > num2)
			{
				if (num == 4)
				{
					return false;
				}
				string s = text.Substring(num2, num3 - num2);
				int num4 = int.Parse(s);
				if (num4 < 0 || num4 > 255)
				{
					return false;
				}
				num2 = num3 + 1;
				num++;
			}
			return num == 4;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000A5248 File Offset: 0x000A4248
		public static bool IsValidIPv4WithNetmask(string address)
		{
			int num = address.IndexOf("/");
			string text = address.Substring(num + 1);
			return num > 0 && IPAddress.IsValidIPv4(address.Substring(0, num)) && (IPAddress.IsValidIPv4(text) || IPAddress.IsMaskValue(text, 32));
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000A5294 File Offset: 0x000A4294
		public static bool IsValidIPv6WithNetmask(string address)
		{
			int num = address.IndexOf("/");
			string text = address.Substring(num + 1);
			return num > 0 && IPAddress.IsValidIPv6(address.Substring(0, num)) && (IPAddress.IsValidIPv6(text) || IPAddress.IsMaskValue(text, 128));
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000A52E4 File Offset: 0x000A42E4
		private static bool IsMaskValue(string component, int size)
		{
			int num = int.Parse(component);
			try
			{
				return num >= 0 && num <= size;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000A5330 File Offset: 0x000A4330
		public static bool IsValidIPv6(string address)
		{
			try
			{
				return IPAddress.unsafeIsValidIPv6(address);
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return false;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000A536C File Offset: 0x000A436C
		private static bool unsafeIsValidIPv6(string address)
		{
			if (address.Length == 0)
			{
				return false;
			}
			int num = 0;
			string text = address + ":";
			bool flag = false;
			int num2 = 0;
			int num3;
			while (num2 < text.Length && (num3 = text.IndexOf(':', num2)) >= num2)
			{
				if (num == 8)
				{
					return false;
				}
				if (num2 != num3)
				{
					string text2 = text.Substring(num2, num3 - num2);
					if (num3 == text.Length - 1 && text2.IndexOf('.') > 0)
					{
						if (!IPAddress.IsValidIPv4(text2))
						{
							return false;
						}
						num++;
					}
					else
					{
						string s = text.Substring(num2, num3 - num2);
						int num4 = int.Parse(s, NumberStyles.AllowHexSpecifier);
						if (num4 < 0 || num4 > 65535)
						{
							return false;
						}
					}
				}
				else
				{
					if (num3 != 1 && num3 != text.Length - 1 && flag)
					{
						return false;
					}
					flag = true;
				}
				num2 = num3 + 1;
				num++;
			}
			return num == 8 || flag;
		}
	}
}
