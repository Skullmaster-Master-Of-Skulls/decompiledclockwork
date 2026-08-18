using System;
using System.Globalization;
using System.Text;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200014F RID: 335
	public class DerUtcTime : Asn1Object
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x000428A8 File Offset: 0x000418A8
		public static DerUtcTime GetInstance(object obj)
		{
			if (obj == null || obj is DerUtcTime)
			{
				return (DerUtcTime)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerUtcTime(((Asn1OctetString)obj).GetOctets());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000428FA File Offset: 0x000418FA
		public static DerUtcTime GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerUtcTime.GetInstance(obj.GetObject());
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00042908 File Offset: 0x00041908
		public DerUtcTime(string time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
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

		// Token: 0x06000C02 RID: 3074 RVA: 0x00042960 File Offset: 0x00041960
		public DerUtcTime(DateTime time)
		{
			this.time = time.ToString("yyMMddHHmmss") + "Z";
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00042984 File Offset: 0x00041984
		internal DerUtcTime(byte[] bytes)
		{
			this.time = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000429A1 File Offset: 0x000419A1
		public DateTime ToDateTime()
		{
			return this.ParseDateString(this.TimeString, "yyMMddHHmmss'GMT'zzz");
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000429B4 File Offset: 0x000419B4
		public DateTime ToAdjustedDateTime()
		{
			return this.ParseDateString(this.AdjustedTimeString, "yyyyMMddHHmmss'GMT'zzz");
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000429C8 File Offset: 0x000419C8
		private DateTime ParseDateString(string dateStr, string formatStr)
		{
			return DateTime.ParseExact(dateStr, formatStr, DateTimeFormatInfo.InvariantInfo).ToUniversalTime();
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x000429EC File Offset: 0x000419EC
		public string TimeString
		{
			get
			{
				if (this.time.IndexOf('-') < 0 && this.time.IndexOf('+') < 0)
				{
					if (this.time.Length == 11)
					{
						return this.time.Substring(0, 10) + "00GMT+00:00";
					}
					return this.time.Substring(0, 12) + "GMT+00:00";
				}
				else
				{
					int num = this.time.IndexOf('-');
					if (num < 0)
					{
						num = this.time.IndexOf('+');
					}
					string text = this.time;
					if (num == this.time.Length - 3)
					{
						text += "00";
					}
					if (num == 10)
					{
						return string.Concat(new string[]
						{
							text.Substring(0, 10),
							"00GMT",
							text.Substring(10, 3),
							":",
							text.Substring(13, 2)
						});
					}
					return string.Concat(new string[]
					{
						text.Substring(0, 12),
						"GMT",
						text.Substring(12, 3),
						":",
						text.Substring(15, 2)
					});
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00042B25 File Offset: 0x00041B25
		[Obsolete("Use 'AdjustedTimeString' property instead")]
		public string AdjustedTime
		{
			get
			{
				return this.AdjustedTimeString;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00042B30 File Offset: 0x00041B30
		public string AdjustedTimeString
		{
			get
			{
				string timeString = this.TimeString;
				string str = (timeString[0] < '5') ? "20" : "19";
				return str + timeString;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00042B63 File Offset: 0x00041B63
		private byte[] GetOctets()
		{
			return Encoding.ASCII.GetBytes(this.time);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00042B75 File Offset: 0x00041B75
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(23, this.GetOctets());
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00042B88 File Offset: 0x00041B88
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUtcTime derUtcTime = asn1Object as DerUtcTime;
			return derUtcTime != null && this.time.Equals(derUtcTime.time);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00042BB2 File Offset: 0x00041BB2
		protected override int Asn1GetHashCode()
		{
			return this.time.GetHashCode();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00042BBF File Offset: 0x00041BBF
		public override string ToString()
		{
			return this.time;
		}

		// Token: 0x04000983 RID: 2435
		private readonly string time;
	}
}
