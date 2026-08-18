using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x0200009B RID: 155
	internal class NumericMode
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x0000E488 File Offset: 0x0000C688
		internal static List<long> EncodeData(string values)
		{
			List<long> list = new List<long>();
			while (values.Length > 0)
			{
				if (values.Length >= 44)
				{
					string values2 = "1" + values.Substring(0, 44);
					values = values.Substring(44);
					list.AddRange(NumericMode.CalculateRange(values2));
				}
				else
				{
					string values3 = "1" + values;
					values = string.Empty;
					list.AddRange(NumericMode.CalculateRange(values3));
				}
			}
			return list;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		private static List<long> CalculateRange(string values)
		{
			List<long> list = new List<long>();
			List<long> list2 = new List<long>();
			List<ulong> list3 = new List<ulong>();
			string text = values;
			while (text != "0")
			{
				list3 = NumericMode.Remainder(text);
				long item = (long)list3[list3.Count - 1];
				list2.Add(item);
				text = NumericMode.Division900(text);
			}
			for (int i = list2.Count - 1; i >= 0; i--)
			{
				list.Add(list2[i]);
			}
			return list;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000E578 File Offset: 0x0000C778
		private static string Division900(string values)
		{
			string text = string.Empty;
			if (values.Length <= 10)
			{
				text += NumericMode.CalculateDivision5(values);
			}
			else if (values.Length <= 20)
			{
				text += NumericMode.CalculateDivision5(ulong.Parse(values.Substring(0, values.Length - 10)).ToString());
				text += NumericMode.CalculateDivision4(values);
			}
			else if (values.Length <= 30)
			{
				text += NumericMode.CalculateDivision5(ulong.Parse(values.Substring(0, values.Length - 20)).ToString());
				text += NumericMode.CalculateDivision4(values);
				text += NumericMode.CalculateDivision3(values);
			}
			else if (values.Length <= 40)
			{
				text += NumericMode.CalculateDivision5(ulong.Parse(values.Substring(0, values.Length - 30)).ToString());
				text += NumericMode.CalculateDivision4(values);
				text += NumericMode.CalculateDivision3(values);
				text += NumericMode.CalculateDivision2(values);
			}
			else
			{
				text += NumericMode.CalculateDivision5(ulong.Parse(values.Substring(0, values.Length - 40)).ToString());
				text += NumericMode.CalculateDivision4(values);
				text += NumericMode.CalculateDivision3(values);
				text += NumericMode.CalculateDivision2(values);
				text += NumericMode.CalculateDivision1(values);
			}
			return text;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		private static string CalculateDivision5(string data)
		{
			List<ulong> list = NumericMode.Remainder(data);
			ulong num = list[list.Count - 1];
			ulong num2 = list[list.Count - 2];
			return (num2 / 900UL).ToString();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000E73C File Offset: 0x0000C93C
		private static string CalculateDivision4(string data)
		{
			List<ulong> list = NumericMode.Remainder(data);
			ulong num = list[list.Count - 1];
			ulong num2 = list[list.Count - 2];
			ulong num3 = list[list.Count - 3];
			List<ulong> list2 = NumericMode.Remainder(num2.ToString());
			ulong num4 = list2[list2.Count - 1];
			return ((ulong)(num4 * Math.Pow(10.0, 10.0) + num3) / 900UL).ToString();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		private static string CalculateDivision3(string data)
		{
			List<ulong> list = NumericMode.Remainder(data);
			ulong num = list[list.Count - 1];
			ulong num2 = list[list.Count - 2];
			ulong num3 = list[list.Count - 3];
			ulong num4 = list[list.Count - 4];
			List<ulong> list2 = NumericMode.Remainder(num2.ToString() + num3.ToString());
			ulong num5 = list2[list2.Count - 1];
			ulong num6 = (ulong)(num5 * Math.Pow(10.0, 10.0) + num4) / 900UL;
			string str = string.Empty;
			if (num <= 9UL && num6.ToString().Length < 10)
			{
				str = "00";
			}
			else if (num >= 10UL && num <= 99UL && num6.ToString().Length < 10)
			{
				str = "0";
			}
			return str + num6.ToString();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000E8CC File Offset: 0x0000CACC
		private static string CalculateDivision2(string data)
		{
			List<ulong> list = NumericMode.Remainder(data);
			ulong num = list[list.Count - 1];
			ulong num2 = list[list.Count - 2];
			ulong num3 = list[list.Count - 3];
			ulong num4 = list[list.Count - 4];
			ulong num5 = list[list.Count - 5];
			List<ulong> list2 = NumericMode.Remainder(num2.ToString() + num3.ToString() + num4.ToString());
			ulong num6 = list2[list2.Count - 1];
			ulong num7 = (ulong)(num6 * Math.Pow(10.0, 10.0) + num5) / 900UL;
			string str = string.Empty;
			if (num <= 9UL && num7.ToString().Length < 10)
			{
				str = "00";
			}
			else if (num >= 10UL && num <= 99UL && num7.ToString().Length < 10)
			{
				str = "0";
			}
			return str + num7.ToString();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		private static string CalculateDivision1(string data)
		{
			List<ulong> list = NumericMode.Remainder(data);
			ulong num = list[list.Count - 1];
			ulong num2 = list[list.Count - 2];
			ulong num3 = list[list.Count - 3];
			ulong num4 = list[list.Count - 4];
			ulong num5 = list[list.Count - 5];
			ulong num6 = list[list.Count - 6];
			List<ulong> list2 = NumericMode.Remainder(num2.ToString() + num3.ToString() + num4.ToString() + num5.ToString());
			ulong num7 = list2[list2.Count - 1];
			ulong num8 = (ulong)(num7 * Math.Pow(10.0, 10.0) + num6) / 900UL;
			string str = string.Empty;
			if (num <= 9UL && num8.ToString().Length < 10)
			{
				str = "00";
			}
			else if (num >= 10UL && num <= 99UL && num8.ToString().Length < 10)
			{
				str = "0";
			}
			return str + num8.ToString();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000EB14 File Offset: 0x0000CD14
		private static List<ulong> Remainder(string values)
		{
			List<ulong> list = new List<ulong>();
			if (values.Length <= 10)
			{
				ulong num = ulong.Parse(values);
				ulong item = NumericMode.CalculateRemainder5(num);
				list.Add(num);
				list.Add(item);
			}
			else if (values.Length <= 20)
			{
				ulong num2 = ulong.Parse(values.Substring(0, values.Length - 10));
				ulong num = ulong.Parse(values.Substring(values.Length - 10));
				ulong item = NumericMode.CalculateRemainder4(num2, num);
				list.Add(num);
				list.Add(num2);
				list.Add(item);
			}
			else if (values.Length <= 30)
			{
				ulong num3 = ulong.Parse(values.Substring(0, values.Length - 20));
				ulong num2 = ulong.Parse(values.Substring(values.Length - 20, 10));
				ulong num = ulong.Parse(values.Substring(values.Length - 10));
				ulong item = NumericMode.CalculateRemainder3(num3, num2, num);
				list.Add(num);
				list.Add(num2);
				list.Add(num3);
				list.Add(item);
			}
			else if (values.Length <= 40)
			{
				ulong num4 = ulong.Parse(values.Substring(0, values.Length - 30));
				ulong num3 = ulong.Parse(values.Substring(values.Length - 30, 10));
				ulong num2 = ulong.Parse(values.Substring(values.Length - 20, 10));
				ulong num = ulong.Parse(values.Substring(values.Length - 10));
				ulong item = NumericMode.CalculateRemainder2(num4, num3, num2, num);
				list.Add(num);
				list.Add(num2);
				list.Add(num3);
				list.Add(num4);
				list.Add(item);
			}
			else
			{
				ulong num5 = ulong.Parse(values.Substring(0, values.Length - 40));
				ulong num4 = ulong.Parse(values.Substring(values.Length - 40, 10));
				ulong num3 = ulong.Parse(values.Substring(values.Length - 30, 10));
				ulong num2 = ulong.Parse(values.Substring(values.Length - 20, 10));
				ulong num = ulong.Parse(values.Substring(values.Length - 10));
				ulong item = NumericMode.CalculateRemainder1(num5, num4, num3, num2, num);
				list.Add(num);
				list.Add(num2);
				list.Add(num3);
				list.Add(num4);
				list.Add(num5);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		private static ulong CalculateRemainder5(ulong group5)
		{
			return group5 % 900UL;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		private static ulong CalculateRemainder4(ulong group4, ulong group5)
		{
			ulong num = NumericMode.CalculateRemainder5(group4);
			return (ulong)(num * Math.Pow(10.0, 10.0) + group5) % 900UL;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		private static ulong CalculateRemainder3(ulong group3, ulong group4, ulong group5)
		{
			ulong num = NumericMode.CalculateRemainder5(group3);
			ulong num2 = (ulong)((num * Math.Pow(10.0, 10.0) + group4) % 900.0);
			return (ulong)((num2 * Math.Pow(10.0, 10.0) + group5) % 900.0);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000EE50 File Offset: 0x0000D050
		private static ulong CalculateRemainder2(ulong group2, ulong group3, ulong group4, ulong group5)
		{
			ulong num = NumericMode.CalculateRemainder5(group2);
			ulong num2 = (ulong)(num * Math.Pow(10.0, 10.0) + group3) % 900UL;
			ulong num3 = (ulong)(num2 * Math.Pow(10.0, 10.0) + group4) % 900UL;
			return (ulong)(num3 * Math.Pow(10.0, 10.0) + group5) % 900UL;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		private static ulong CalculateRemainder1(ulong group1, ulong group2, ulong group3, ulong group4, ulong group5)
		{
			ulong num = NumericMode.CalculateRemainder5(group1);
			ulong num2 = (ulong)(num * Math.Pow(10.0, 10.0) + group2) % 900UL;
			ulong num3 = (ulong)(num2 * Math.Pow(10.0, 10.0) + group3) % 900UL;
			ulong num4 = (ulong)(num3 * Math.Pow(10.0, 10.0) + group4) % 900UL;
			return (ulong)(num4 * Math.Pow(10.0, 10.0) + group5) % 900UL;
		}
	}
}
