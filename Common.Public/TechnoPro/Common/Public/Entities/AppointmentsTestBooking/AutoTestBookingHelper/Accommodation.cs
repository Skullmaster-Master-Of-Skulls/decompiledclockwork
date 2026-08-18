using System;
using System.Collections.Generic;
using System.Text;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200052F RID: 1327
	[Serializable]
	public class Accommodation
	{
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x0002AFD6 File Offset: 0x000291D6
		// (set) Token: 0x06002A0D RID: 10765 RVA: 0x0002AFDE File Offset: 0x000291DE
		public int ControlId { get; set; }

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x0002AFE7 File Offset: 0x000291E7
		// (set) Token: 0x06002A0F RID: 10767 RVA: 0x0002AFEF File Offset: 0x000291EF
		public string Title { get; set; }

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x0002AFF8 File Offset: 0x000291F8
		// (set) Token: 0x06002A11 RID: 10769 RVA: 0x0002B000 File Offset: 0x00029200
		public string LookupText { get; set; }

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x0002B009 File Offset: 0x00029209
		// (set) Token: 0x06002A13 RID: 10771 RVA: 0x0002B011 File Offset: 0x00029211
		public int Level { get; set; }

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x0002B01A File Offset: 0x0002921A
		// (set) Token: 0x06002A15 RID: 10773 RVA: 0x0002B022 File Offset: 0x00029222
		public string SubText { get; set; }

		// Token: 0x06002A16 RID: 10774 RVA: 0x0002B02C File Offset: 0x0002922C
		public override string ToString()
		{
			return (!string.IsNullOrEmpty(this.LookupText)) ? string.Format("{0}: {1}", this.Title, this.LookupText) : this.Title;
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0002B069 File Offset: 0x00029269
		public Accommodation(int cid, string title, string lookupText, int level)
		{
			this.ControlId = cid;
			this.Title = title;
			this.LookupText = lookupText;
			this.Level = level;
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x0002B094 File Offset: 0x00029294
		public Accommodation(int cid, string title, string lookupText, string subText, int level)
		{
			this.ControlId = cid;
			this.Title = title;
			this.LookupText = lookupText;
			this.Level = level;
			this.SubText = subText;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0002B0C8 File Offset: 0x000292C8
		public Accommodation()
		{
			this.ControlId = 0;
			this.Title = "";
			this.LookupText = "";
			this.Level = 0;
			this.SubText = "";
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x0002B108 File Offset: 0x00029308
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

		// Token: 0x06002A1B RID: 10779 RVA: 0x0002B19C File Offset: 0x0002939C
		public static double GetExtraTimePercent(string text, string methodCode)
		{
			return Accommodation.GetExtraTimePercent(text, Accommodation.ParseExtraTimeMethod(methodCode));
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0002B1BC File Offset: 0x000293BC
		public static double GetExtraTimePercent(string text, Accommodation.CalculateExtraTimeMethod method)
		{
			double num = Accommodation.ExtractNumber(text);
			return Accommodation.GetExtraTimePercent(num, method);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0002B1DC File Offset: 0x000293DC
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

		// Token: 0x06002A1E RID: 10782 RVA: 0x0002B23C File Offset: 0x0002943C
		public static double GetExtraTimePercent(double num, string methodCode)
		{
			Accommodation.CalculateExtraTimeMethod method = Accommodation.ParseExtraTimeMethod(methodCode);
			return Accommodation.GetExtraTimePercent(num, method);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0002B25C File Offset: 0x0002945C
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

		// Token: 0x06002A20 RID: 10784 RVA: 0x0002B2F0 File Offset: 0x000294F0
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

		// Token: 0x06002A21 RID: 10785 RVA: 0x0002B330 File Offset: 0x00029530
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

		// Token: 0x06002A22 RID: 10786 RVA: 0x0002B3DC File Offset: 0x000295DC
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

		// Token: 0x0200061A RID: 1562
		public enum CalculateExtraTimeMethod
		{
			// Token: 0x0400213B RID: 8507
			Guess,
			// Token: 0x0400213C RID: 8508
			MinPerHour,
			// Token: 0x0400213D RID: 8509
			Percentage_1_33,
			// Token: 0x0400213E RID: 8510
			Percentage_0_33,
			// Token: 0x0400213F RID: 8511
			Percentage_33_0,
			// Token: 0x04002140 RID: 8512
			FlatRate,
			// Token: 0x04002141 RID: 8513
			MinPerHourInTwoControls
		}
	}
}
