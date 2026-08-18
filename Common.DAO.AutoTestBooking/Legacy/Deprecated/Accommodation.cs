using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoPro.Common.DAO.AutoTestBooking.Legacy.Deprecated
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class Accommodation
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00007BD8 File Offset: 0x00005DD8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public int Controlid
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this.controlId = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00007BE9 File Offset: 0x00005DE9
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00007BF1 File Offset: 0x00005DF1
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00007BFA File Offset: 0x00005DFA
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00007C02 File Offset: 0x00005E02
		public string LookupText
		{
			get
			{
				return this.lookupText;
			}
			set
			{
				this.lookupText = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00007C0B File Offset: 0x00005E0B
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00007C13 File Offset: 0x00005E13
		public int Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00007C1C File Offset: 0x00005E1C
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00007C24 File Offset: 0x00005E24
		public string SubText
		{
			get
			{
				return this.subText;
			}
			set
			{
				this.subText = value;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00007C2D File Offset: 0x00005E2D
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.lookupText))
			{
				return string.Format("{0}: {1}", this.title, this.lookupText);
			}
			return this.title;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00007C59 File Offset: 0x00005E59
		public Accommodation(int cid, string title, string lookupText)
		{
			this.controlId = cid;
			this.title = title;
			this.lookupText = lookupText;
			this.level = 1;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00007C7D File Offset: 0x00005E7D
		public Accommodation(int cid, string title, string lookupText, int level)
		{
			this.controlId = cid;
			this.title = title;
			this.lookupText = lookupText;
			this.level = level;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00007CA2 File Offset: 0x00005EA2
		public Accommodation(int cid, string title, string lookupText, string subText, int level)
		{
			this.controlId = cid;
			this.title = title;
			this.lookupText = lookupText;
			this.level = level;
			this.subText = subText;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public static Accommodation.CalculateExtraTimeMethod ParseExtraTimeMethod(string code)
		{
			string text = code.Trim();
			if (text.Equals("1"))
			{
				return Accommodation.CalculateExtraTimeMethod.MinPerHour;
			}
			if (text.Equals("2"))
			{
				return Accommodation.CalculateExtraTimeMethod.Percentage_1_33;
			}
			if (text.Equals("3"))
			{
				return Accommodation.CalculateExtraTimeMethod.Percentage_0_33;
			}
			if (text.Equals("4"))
			{
				return Accommodation.CalculateExtraTimeMethod.Percentage_33_0;
			}
			if (text.Equals("5"))
			{
				return Accommodation.CalculateExtraTimeMethod.FlatRate;
			}
			if (text.Equals("6"))
			{
				return Accommodation.CalculateExtraTimeMethod.MinPerHourInTwoControls;
			}
			return Accommodation.CalculateExtraTimeMethod.Guess;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00007D3F File Offset: 0x00005F3F
		public static double GetExtraTimePercent(string text, string methodCode)
		{
			return Accommodation.GetExtraTimePercent(text, Accommodation.ParseExtraTimeMethod(methodCode));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00007D4D File Offset: 0x00005F4D
		public static double GetExtraTimePercent(string text, Accommodation.CalculateExtraTimeMethod method)
		{
			return Accommodation.GetExtraTimePercent(Accommodation.ExtractNumber(text), method);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00007D5C File Offset: 0x00005F5C
		public static double ExtractNumber(string text)
		{
			string text2 = Accommodation.StripNonDigits(text);
			if (!string.IsNullOrEmpty(text2))
			{
				try
				{
					return double.Parse(text2);
				}
				catch
				{
					return 0.0;
				}
			}
			return 0.0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00007DAC File Offset: 0x00005FAC
		public static double GetExtraTimePercent(double num, string methodCode)
		{
			return Accommodation.GetExtraTimePercent(num, Accommodation.ParseExtraTimeMethod(methodCode));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00007DBC File Offset: 0x00005FBC
		public static double GetExtraTimePercent(double num, Accommodation.CalculateExtraTimeMethod method)
		{
			if (num <= 0.0)
			{
				return 0.0;
			}
			switch (method)
			{
			case Accommodation.CalculateExtraTimeMethod.MinPerHour:
				return 1.0 + num / 60.0;
			case Accommodation.CalculateExtraTimeMethod.Percentage_1_33:
				return num;
			case Accommodation.CalculateExtraTimeMethod.Percentage_0_33:
				return 1.0 + num;
			case Accommodation.CalculateExtraTimeMethod.Percentage_33_0:
				return 1.0 + num / 100.0;
			default:
				return num;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00007E34 File Offset: 0x00006034
		public static int ApplyExtraTime(int minutes, double extraTimeAmount)
		{
			int num = 5;
			double num2 = (double)minutes * extraTimeAmount;
			double num3 = num2 % (double)num;
			if (num3 >= 1.0)
			{
				num2 += (double)num - num3;
			}
			return Convert.ToInt32(num2);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00007E68 File Offset: 0x00006068
		public static string StripNonDigits(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			bool flag2 = false;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (char.IsDigit(c))
				{
					flag2 = true;
					stringBuilder.Append(c);
				}
				else if (c == '.' && flag && i < length - 1 && char.IsDigit(text[i + 1]))
				{
					flag = false;
					stringBuilder.Append(c);
				}
				else if (flag2)
				{
					break;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00007EF0 File Offset: 0x000060F0
		public static string GetAccommodationsString(List<Accommodation> accommodations)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Accommodation accommodation in accommodations)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\r\n");
				}
				stringBuilder.Append("• ");
				stringBuilder.Append(accommodation.Title);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000008 RID: 8
		private int controlId;

		// Token: 0x04000009 RID: 9
		private int level;

		// Token: 0x0400000A RID: 10
		public string title;

		// Token: 0x0400000B RID: 11
		private string lookupText;

		// Token: 0x0400000C RID: 12
		private string subText;

		// Token: 0x0200001D RID: 29
		public enum CalculateExtraTimeMethod
		{
			// Token: 0x04000051 RID: 81
			Guess,
			// Token: 0x04000052 RID: 82
			MinPerHour,
			// Token: 0x04000053 RID: 83
			Percentage_1_33,
			// Token: 0x04000054 RID: 84
			Percentage_0_33,
			// Token: 0x04000055 RID: 85
			Percentage_33_0,
			// Token: 0x04000056 RID: 86
			FlatRate,
			// Token: 0x04000057 RID: 87
			MinPerHourInTwoControls
		}
	}
}
