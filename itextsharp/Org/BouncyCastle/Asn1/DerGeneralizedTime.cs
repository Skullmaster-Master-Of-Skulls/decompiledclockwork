using System;
using System.Globalization;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000045 RID: 69
	public class DerGeneralizedTime : Asn1Object
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00009D34 File Offset: 0x00008D34
		public static DerGeneralizedTime GetInstance(object obj)
		{
			if (obj == null || obj is DerGeneralizedTime)
			{
				return (DerGeneralizedTime)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerGeneralizedTime(((Asn1OctetString)obj).GetOctets());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009D8B File Offset: 0x00008D8B
		public static DerGeneralizedTime GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerGeneralizedTime.GetInstance(obj.GetObject());
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009D98 File Offset: 0x00008D98
		public DerGeneralizedTime(string time)
		{
			this.time = time;
			try
			{
				this.ToDateTime();
			}
			catch (FormatException ex)
			{
				throw new ArgumentException("invalid date string: " + ex.Message);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009DE4 File Offset: 0x00008DE4
		public DerGeneralizedTime(DateTime time)
		{
			this.time = time.ToString("yyyyMMddHHmmss\\Z");
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009DFE File Offset: 0x00008DFE
		internal DerGeneralizedTime(byte[] bytes)
		{
			this.time = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00009E1B File Offset: 0x00008E1B
		public string TimeString
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009E24 File Offset: 0x00008E24
		public string GetTime()
		{
			if (this.time[this.time.Length - 1] == 'Z')
			{
				return this.time.Substring(0, this.time.Length - 1) + "GMT+00:00";
			}
			int num = this.time.Length - 5;
			char c = this.time[num];
			if (c == '-' || c == '+')
			{
				return string.Concat(new string[]
				{
					this.time.Substring(0, num),
					"GMT",
					this.time.Substring(num, 3),
					":",
					this.time.Substring(num + 3)
				});
			}
			num = this.time.Length - 3;
			c = this.time[num];
			if (c == '-' || c == '+')
			{
				return this.time.Substring(0, num) + "GMT" + this.time.Substring(num) + ":00";
			}
			return this.time + this.CalculateGmtOffset();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009F44 File Offset: 0x00008F44
		private string CalculateGmtOffset()
		{
			char c = '+';
			int num = TimeZone.CurrentTimeZone.GetUtcOffset(this.ToDateTime()).Minutes;
			if (num < 0)
			{
				c = '-';
				num = -num;
			}
			int num2 = num / 60;
			num %= 60;
			return string.Concat(new object[]
			{
				"GMT",
				c,
				DerGeneralizedTime.Convert(num2),
				":",
				DerGeneralizedTime.Convert(num)
			});
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009FC0 File Offset: 0x00008FC0
		private static string Convert(int time)
		{
			if (time < 10)
			{
				return "0" + time;
			}
			return time.ToString();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009FE0 File Offset: 0x00008FE0
		public DateTime ToDateTime()
		{
			string text = this.time;
			bool makeUniversal = false;
			string formatStr;
			if (text.EndsWith("Z"))
			{
				if (this.HasFractionalSeconds)
				{
					int count = text.Length - text.IndexOf('.') - 2;
					formatStr = "yyyyMMddHHmmss." + this.FString(count) + "\\Z";
				}
				else
				{
					formatStr = "yyyyMMddHHmmss\\Z";
				}
			}
			else if (this.time.IndexOf('-') > 0 || this.time.IndexOf('+') > 0)
			{
				text = this.GetTime();
				makeUniversal = true;
				if (this.HasFractionalSeconds)
				{
					int count2 = text.IndexOf("GMT") - 1 - text.IndexOf('.');
					formatStr = "yyyyMMddHHmmss." + this.FString(count2) + "'GMT'zzz";
				}
				else
				{
					formatStr = "yyyyMMddHHmmss'GMT'zzz";
				}
			}
			else if (this.HasFractionalSeconds)
			{
				int count3 = text.Length - 1 - text.IndexOf('.');
				formatStr = "yyyyMMddHHmmss." + this.FString(count3);
			}
			else
			{
				formatStr = "yyyyMMddHHmmss";
			}
			return this.ParseDateString(text, formatStr, makeUniversal);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A0F0 File Offset: 0x000090F0
		private string FString(int count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append('f');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A120 File Offset: 0x00009120
		private DateTime ParseDateString(string dateStr, string formatStr, bool makeUniversal)
		{
			DateTime result = DateTime.ParseExact(dateStr, formatStr, DateTimeFormatInfo.InvariantInfo);
			if (!makeUniversal)
			{
				return result;
			}
			return result.ToUniversalTime();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A146 File Offset: 0x00009146
		private bool HasFractionalSeconds
		{
			get
			{
				return this.time.IndexOf('.') == 14;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A159 File Offset: 0x00009159
		private byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.time);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A16B File Offset: 0x0000916B
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(24, this.GetOctets());
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A17C File Offset: 0x0000917C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGeneralizedTime derGeneralizedTime = asn1Object as DerGeneralizedTime;
			return derGeneralizedTime != null && this.time.Equals(derGeneralizedTime.time);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A1A6 File Offset: 0x000091A6
		protected override int Asn1GetHashCode()
		{
			return this.time.GetHashCode();
		}

		// Token: 0x040000D1 RID: 209
		private readonly string time;
	}
}
