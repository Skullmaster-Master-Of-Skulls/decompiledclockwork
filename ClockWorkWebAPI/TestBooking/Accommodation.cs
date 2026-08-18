using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class Accommodation
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00011F20 File Offset: 0x00010120
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00011F38 File Offset: 0x00010138
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00011F44 File Offset: 0x00010144
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00011F5C File Offset: 0x0001015C
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00011F68 File Offset: 0x00010168
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00011F80 File Offset: 0x00010180
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00011F8C File Offset: 0x0001018C
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00011FA4 File Offset: 0x000101A4
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00011FB0 File Offset: 0x000101B0
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00011FC8 File Offset: 0x000101C8
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

		// Token: 0x060002E9 RID: 745 RVA: 0x00011FD4 File Offset: 0x000101D4
		public override string ToString()
		{
			bool flag = !string.IsNullOrEmpty(this.lookupText);
			string result;
			if (flag)
			{
				result = string.Format("{0}: {1}", this.title, this.lookupText);
			}
			else
			{
				result = this.title;
			}
			return result;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00012017 File Offset: 0x00010217
		public Accommodation(int cid, string title, string lookupText)
		{
			this.controlId = cid;
			this.title = title;
			this.lookupText = lookupText;
			this.level = 1;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001203D File Offset: 0x0001023D
		public Accommodation(int cid, string title, string lookupText, int level)
		{
			this.controlId = cid;
			this.title = title;
			this.lookupText = lookupText;
			this.level = level;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00012064 File Offset: 0x00010264
		public Accommodation(int cid, string title, string lookupText, string subText, int level)
		{
			this.controlId = cid;
			this.title = title;
			this.lookupText = lookupText;
			this.level = level;
			this.subText = subText;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012094 File Offset: 0x00010294
		public static Accommodation.CalculateExtraTimeMethod ParseExtraTimeMethod(string code)
		{
			string text = code.Trim();
			bool flag = text.Equals("1");
			Accommodation.CalculateExtraTimeMethod result;
			if (flag)
			{
				result = Accommodation.CalculateExtraTimeMethod.MinPerHour;
			}
			else
			{
				bool flag2 = text.Equals("2");
				if (flag2)
				{
					result = Accommodation.CalculateExtraTimeMethod.Percentage_1_33;
				}
				else
				{
					bool flag3 = text.Equals("3");
					if (flag3)
					{
						result = Accommodation.CalculateExtraTimeMethod.Percentage_0_33;
					}
					else
					{
						bool flag4 = text.Equals("4");
						if (flag4)
						{
							result = Accommodation.CalculateExtraTimeMethod.Percentage_33_0;
						}
						else
						{
							bool flag5 = text.Equals("5");
							if (flag5)
							{
								result = Accommodation.CalculateExtraTimeMethod.FlatRate;
							}
							else
							{
								bool flag6 = text.Equals("6");
								if (flag6)
								{
									result = Accommodation.CalculateExtraTimeMethod.MinPerHourInTwoControls;
								}
								else
								{
									result = Accommodation.CalculateExtraTimeMethod.Guess;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012128 File Offset: 0x00010328
		public static double GetExtraTimePercent(string text, db conn)
		{
			conn.Da.SelectCommand.CommandText = "SELECT settingvalue FROM settingsgroups WHERE settingcode=484";
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			Accommodation.CalculateExtraTimeMethod method = (dataTable.Rows.Count > 0) ? Accommodation.ParseExtraTimeMethod(dataTable.Rows[0][0].ToString()) : Accommodation.CalculateExtraTimeMethod.Guess;
			return Accommodation.GetExtraTimePercent(text, method);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00012198 File Offset: 0x00010398
		public static double GetExtraTimePercent(string text, string methodCode)
		{
			return Accommodation.GetExtraTimePercent(text, Accommodation.ParseExtraTimeMethod(methodCode));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000121B8 File Offset: 0x000103B8
		public static double GetExtraTimePercent(string text, Accommodation.CalculateExtraTimeMethod method)
		{
			double num = Accommodation.ExtractNumber(text);
			return Accommodation.GetExtraTimePercent(num, method);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000121D8 File Offset: 0x000103D8
		public static double ExtractNumber(string text)
		{
			string text2 = Accommodation.StripNonDigits(text);
			bool flag = !string.IsNullOrEmpty(text2);
			double result;
			if (flag)
			{
				try
				{
					result = double.Parse(text2);
				}
				catch
				{
					result = 0.0;
				}
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00012238 File Offset: 0x00010438
		public static double GetExtraTimePercent(double num, string methodCode)
		{
			return Accommodation.GetExtraTimePercent(num, Accommodation.ParseExtraTimeMethod(methodCode));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00012258 File Offset: 0x00010458
		public static double GetExtraTimePercent(double num, Accommodation.CalculateExtraTimeMethod method)
		{
			bool flag = num <= 0.0;
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				switch (method)
				{
				case Accommodation.CalculateExtraTimeMethod.MinPerHour:
					result = 1.0 + num / 60.0;
					break;
				case Accommodation.CalculateExtraTimeMethod.Percentage_1_33:
					result = num;
					break;
				case Accommodation.CalculateExtraTimeMethod.Percentage_0_33:
					result = 1.0 + num;
					break;
				case Accommodation.CalculateExtraTimeMethod.Percentage_33_0:
					result = 1.0 + num / 100.0;
					break;
				default:
					result = num;
					break;
				}
			}
			return result;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000122EC File Offset: 0x000104EC
		public static int ApplyExtraTime(int minutes, double extraTimeAmount)
		{
			int num = 5;
			double num2 = (double)minutes * extraTimeAmount;
			double num3 = num2 % (double)num;
			bool flag = num3 >= 1.0;
			if (flag)
			{
				num2 += (double)num - num3;
			}
			return Convert.ToInt32(num2);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012330 File Offset: 0x00010530
		public static string StripNonDigits(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			bool flag2 = false;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				bool flag3 = char.IsDigit(c);
				if (flag3)
				{
					flag2 = true;
					stringBuilder.Append(c);
				}
				else
				{
					bool flag4 = c == '.' && flag && i < length - 1 && char.IsDigit(text[i + 1]);
					if (flag4)
					{
						flag = false;
						stringBuilder.Append(c);
					}
					else
					{
						bool flag5 = flag2;
						if (flag5)
						{
							break;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000123DC File Offset: 0x000105DC
		public static string GetAccommodationsString(List<Accommodation> accommodations)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Accommodation accommodation in accommodations)
			{
				bool flag = stringBuilder.Length > 0;
				if (flag)
				{
					stringBuilder.Append("\r\n");
				}
				stringBuilder.Append("• ");
				stringBuilder.Append(accommodation.Title);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000186 RID: 390
		private int controlId;

		// Token: 0x04000187 RID: 391
		private int level;

		// Token: 0x04000188 RID: 392
		public string title;

		// Token: 0x04000189 RID: 393
		private string lookupText;

		// Token: 0x0400018A RID: 394
		private string subText;

		// Token: 0x0200008E RID: 142
		public enum CalculateExtraTimeMethod
		{
			// Token: 0x04000385 RID: 901
			Guess,
			// Token: 0x04000386 RID: 902
			MinPerHour,
			// Token: 0x04000387 RID: 903
			Percentage_1_33,
			// Token: 0x04000388 RID: 904
			Percentage_0_33,
			// Token: 0x04000389 RID: 905
			Percentage_33_0,
			// Token: 0x0400038A RID: 906
			FlatRate,
			// Token: 0x0400038B RID: 907
			MinPerHourInTwoControls
		}
	}
}
