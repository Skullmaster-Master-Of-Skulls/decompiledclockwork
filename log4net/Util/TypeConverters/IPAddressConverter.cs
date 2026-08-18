using System;
using System.Net;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000EA RID: 234
	internal class IPAddressConverter : IConvertFrom
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x00015346 File Offset: 0x00013546
		public bool CanConvertFrom(Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00015358 File Offset: 0x00013558
		public object ConvertFrom(object source)
		{
			string text = source as string;
			if (text != null && text.Length > 0)
			{
				try
				{
					IPAddress result;
					if (IPAddress.TryParse(text, out result))
					{
						return result;
					}
					IPHostEntry hostEntry = Dns.GetHostEntry(text);
					if (hostEntry != null && hostEntry.AddressList != null && hostEntry.AddressList.Length > 0 && hostEntry.AddressList[0] != null)
					{
						return hostEntry.AddressList[0];
					}
				}
				catch (Exception innerException)
				{
					throw ConversionNotSupportedException.Create(typeof(IPAddress), source, innerException);
				}
			}
			throw ConversionNotSupportedException.Create(typeof(IPAddress), source);
		}

		// Token: 0x04000298 RID: 664
		private static readonly char[] validIpAddressChars = new char[]
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
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'x',
			'X',
			'.',
			':',
			'%'
		};
	}
}
