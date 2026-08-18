using System;
using System.Globalization;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000CF RID: 207
	public class PdfDate : PdfString
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0002609C File Offset: 0x0002509C
		public PdfDate(DateTime d)
		{
			this.value = d.ToString("\\D\\:yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
			string text = d.ToString("zzz", DateTimeFormatInfo.InvariantInfo);
			text = text.Replace(":", "'");
			this.value = this.value + text + "'";
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00026100 File Offset: 0x00025100
		public PdfDate() : this(DateTime.Now)
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0002610D File Offset: 0x0002510D
		private static string SetLength(int i, int length)
		{
			return i.ToString().PadLeft(length, '0');
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0002611E File Offset: 0x0002511E
		public string GetW3CDate()
		{
			return PdfDate.GetW3CDate(this.value);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002612C File Offset: 0x0002512C
		public static string GetW3CDate(string d)
		{
			if (d.StartsWith("D:"))
			{
				d = d.Substring(2);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (d.Length < 4)
			{
				return "0000";
			}
			stringBuilder.Append(d.Substring(0, 4));
			d = d.Substring(4);
			if (d.Length < 2)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append('-').Append(d.Substring(0, 2));
			d = d.Substring(2);
			if (d.Length < 2)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append('-').Append(d.Substring(0, 2));
			d = d.Substring(2);
			if (d.Length < 2)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append('T').Append(d.Substring(0, 2));
			d = d.Substring(2);
			if (d.Length < 2)
			{
				stringBuilder.Append(":00Z");
				return stringBuilder.ToString();
			}
			stringBuilder.Append(':').Append(d.Substring(0, 2));
			d = d.Substring(2);
			if (d.Length < 2)
			{
				stringBuilder.Append('Z');
				return stringBuilder.ToString();
			}
			stringBuilder.Append(':').Append(d.Substring(0, 2));
			d = d.Substring(2);
			if (d.StartsWith("-") || d.StartsWith("+"))
			{
				string value = d.Substring(0, 1);
				d = d.Substring(1);
				string value2 = "00";
				if (d.Length >= 2)
				{
					string value3 = d.Substring(0, 2);
					if (d.Length > 2)
					{
						d = d.Substring(3);
						if (d.Length >= 2)
						{
							value2 = d.Substring(0, 2);
						}
					}
					stringBuilder.Append(value).Append(value3).Append(':').Append(value2);
					return stringBuilder.ToString();
				}
			}
			stringBuilder.Append('Z');
			return stringBuilder.ToString();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0002631C File Offset: 0x0002531C
		public static DateTime Decode(string date)
		{
			if (date.StartsWith("D:"))
			{
				date = date.Substring(2);
			}
			int month = 1;
			int day = 1;
			int hour = 0;
			int minute = 0;
			int second = 0;
			int hours = 0;
			int minutes = 0;
			int year = int.Parse(date.Substring(0, 4));
			if (date.Length >= 6)
			{
				month = int.Parse(date.Substring(4, 2));
				if (date.Length >= 8)
				{
					day = int.Parse(date.Substring(6, 2));
					if (date.Length >= 10)
					{
						hour = int.Parse(date.Substring(8, 2));
						if (date.Length >= 12)
						{
							minute = int.Parse(date.Substring(10, 2));
							if (date.Length >= 14)
							{
								second = int.Parse(date.Substring(12, 2));
							}
						}
					}
				}
			}
			DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
			if (date.Length <= 14)
			{
				return dateTime;
			}
			char c = date[14];
			if (c == 'Z')
			{
				return dateTime.ToLocalTime();
			}
			if (date.Length >= 17)
			{
				hours = int.Parse(date.Substring(15, 2));
				if (date.Length >= 20)
				{
					minutes = int.Parse(date.Substring(18, 2));
				}
			}
			TimeSpan t = new TimeSpan(hours, minutes, 0);
			if (c == '-')
			{
				dateTime += t;
			}
			else
			{
				dateTime -= t;
			}
			return dateTime.ToLocalTime();
		}
	}
}
