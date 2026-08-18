using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.Collections;

namespace Telerik.Web.UI.Calendar.Utils
{
	// Token: 0x02001014 RID: 4116
	internal class Utility
	{
		// Token: 0x0600A1EA RID: 41450 RVA: 0x0023FB30 File Offset: 0x0023DD30
		internal static int[] DateToIntArray(DateTime date)
		{
			return new int[]
			{
				date.Year,
				date.Month,
				date.Day
			};
		}

		// Token: 0x0600A1EB RID: 41451 RVA: 0x0023FB64 File Offset: 0x0023DD64
		internal static string ConvertUnitValueToClientString(Unit inputUnit)
		{
			if (inputUnit == Unit.Empty)
			{
				return string.Empty;
			}
			if (inputUnit.Type == UnitType.Pixel)
			{
				return inputUnit.Value.ToString();
			}
			return "\"" + inputUnit.ToString() + "\"";
		}

		// Token: 0x0600A1EC RID: 41452 RVA: 0x0023FBBC File Offset: 0x0023DDBC
		internal static string SetCellID(string prefixOfID, DateTime cellDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(prefixOfID);
			stringBuilder.Append("_");
			int[] array = Utility.DateToIntArray(cellDate);
			stringBuilder.Append(array[0].ToString());
			stringBuilder.Append("_");
			stringBuilder.Append(array[1].ToString());
			stringBuilder.Append("_");
			stringBuilder.Append(array[2].ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0600A1ED RID: 41453 RVA: 0x0023FC44 File Offset: 0x0023DE44
		internal static string SetCellID(string prefixOfID, int index)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(prefixOfID);
			stringBuilder.Append("_");
			stringBuilder.Append(index.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0600A1EE RID: 41454 RVA: 0x0023FC80 File Offset: 0x0023DE80
		internal static string ConvertSingleValueToClientString(object inputValue)
		{
			string text = inputValue as string;
			if (text != null)
			{
				if (!(text == "\"\"") && !string.IsNullOrEmpty(text))
				{
					return "\"" + Utility.QuoteJsString(Convert.ToString(inputValue)) + "\"";
				}
				return string.Empty;
			}
			else
			{
				if (inputValue is bool)
				{
					int value = 0;
					if ((bool)inputValue)
					{
						value = 1;
					}
					return Convert.ToString(value);
				}
				if (inputValue is DateTime)
				{
					int[] inputArray = Utility.DateToIntArray((DateTime)inputValue);
					string value2 = Utility.ConvertToClientArray1D(inputArray);
					return Convert.ToString(value2);
				}
				if (inputValue.GetType().IsEnum)
				{
					return Convert.ToString(Convert.ToInt32(inputValue));
				}
				return Convert.ToString(inputValue);
			}
		}

		// Token: 0x0600A1EF RID: 41455 RVA: 0x0023FD2C File Offset: 0x0023DF2C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal static string ConvertToClientArray1D(object inputArray)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			if (inputArray != null && inputArray is Array)
			{
				if (((Array)inputArray).Length > 0)
				{
					for (int i = 0; i < ((Array)inputArray).Length; i++)
					{
						string value = string.Empty;
						try
						{
							if (((Array)inputArray).GetValue(i) != null)
							{
								value = Utility.ConvertSingleValueToClientString(((Array)inputArray).GetValue(i));
								if (!string.IsNullOrEmpty(value))
								{
									stringBuilder.Append(value);
								}
							}
						}
						catch
						{
							flag = true;
							break;
						}
						if (!string.IsNullOrEmpty(value))
						{
							stringBuilder.Append(",");
						}
					}
				}
				else
				{
					stringBuilder.Append(",");
				}
			}
			else
			{
				object[] array = null;
				if (inputArray != null && (inputArray is ICollection || inputArray is IList))
				{
					if (inputArray is ICollection)
					{
						array = new object[((ICollection)inputArray).Count];
						((ICollection)inputArray).CopyTo(array, 0);
					}
					else if (inputArray is IList)
					{
						array = new object[((IList)inputArray).Count];
						((IList)inputArray).CopyTo(array, 0);
					}
				}
				for (int j = 0; j < array.Length; j++)
				{
					try
					{
						if (array[j] != null)
						{
							if (array[j].GetType().IsPrimitive || array[j] is DateTime || array[j] is string || array[j].GetType().IsEnum)
							{
								stringBuilder.Append(Utility.ConvertSingleValueToClientString(array[j]));
							}
							else if (array[j] is Unit)
							{
								stringBuilder.Append(Utility.ConvertUnitValueToClientString((Unit)array[j]));
							}
							else if (array[j] is IClientData)
							{
								stringBuilder.Append(Utility.ConvertToClientArray1D(((IClientData)array[j]).GetClientData()));
							}
							else if (array[j] is Array || array[j] is ICollection || array[j] is IList)
							{
								stringBuilder.Append(Utility.ConvertToClientArray1D(array[j]));
							}
							else
							{
								if (!(array[j] is Style))
								{
									flag = true;
									break;
								}
								string name = string.Format(CultureInfo.InvariantCulture, "SpecialDayStyle_{1}_{0:M_d}", new object[]
								{
									(DateTime)array[1],
									((DateTime)array[1]).Year
								});
								stringBuilder.Append("{" + Utility.GetStyle(name, array[j] as Style) + "}");
							}
						}
					}
					catch (Exception)
					{
						flag = true;
						break;
					}
					stringBuilder.Append(",");
				}
			}
			if (!flag)
			{
				if (stringBuilder.ToString().EndsWith(","))
				{
					stringBuilder.Remove(stringBuilder.Length - ",".Length, ",".Length);
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
			throw new FormatException("The server data arrays could not be presented as client-side data arrays.");
		}

		// Token: 0x0600A1F0 RID: 41456 RVA: 0x00240070 File Offset: 0x0023E270
		internal static string ConvertToClientHash(ArrayList inputArrayList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			for (int i = 0; i < inputArrayList.Count; i++)
			{
				if (inputArrayList[i] is Pair)
				{
					Pair pair = (Pair)inputArrayList[i];
					stringBuilder.AppendFormat("\"{0}\"", pair.First);
					stringBuilder.Append(" : ");
					stringBuilder.Append(pair.Second);
					if (i < inputArrayList.Count - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600A1F1 RID: 41457 RVA: 0x00240110 File Offset: 0x0023E310
		internal static void ConvertToServerDateTimeCollection(DateTimeCollection dateTimeCollection, string inputString)
		{
			int num = 0;
			if (string.IsNullOrEmpty(inputString))
			{
				return;
			}
			for (int i = 0; i < inputString.Length; i++)
			{
				if (inputString[i] == '[')
				{
					num++;
				}
				else if (inputString[i] == ']')
				{
					num--;
				}
			}
			if (num != 0)
			{
				throw new FormatException("The input format of the selected dates array is invalid.");
			}
			inputString = inputString.Replace("[", string.Empty).Replace("]", string.Empty);
			string[] array = inputString.Split(",".ToCharArray());
			for (int i = 0; i < array.Length / 3; i++)
			{
				DateTime inputDate = new DateTime(int.Parse(array[i * 3]), int.Parse(array[i * 3 + 1]), int.Parse(array[i * 3 + 2]));
				dateTimeCollection.Add(new RadDate(inputDate));
			}
		}

		// Token: 0x0600A1F2 RID: 41458 RVA: 0x002401E0 File Offset: 0x0023E3E0
		internal static string QuoteJsString(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (c <= '"')
				{
					char c2 = c;
					switch (c2)
					{
					case '\t':
						stringBuilder.Append("\\t");
						goto IL_C2;
					case '\n':
						stringBuilder.Append("\\n");
						goto IL_C2;
					case '\v':
					case '\f':
						break;
					case '\r':
						stringBuilder.Append("\\r");
						goto IL_C2;
					default:
						if (c2 == '"')
						{
							stringBuilder.Append("\\\"");
							goto IL_C2;
						}
						break;
					}
					stringBuilder.Append(value[i]);
				}
				else if (c == '\'')
				{
					stringBuilder.Append("\\'");
				}
				else if (c == '\\')
				{
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
				IL_C2:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600A1F3 RID: 41459 RVA: 0x002402C8 File Offset: 0x0023E4C8
		internal static string GetStyle(string name, Style value)
		{
			string arg = "";
			string arg2 = "";
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			value.AddAttributesToRender(htmlTextWriter);
			htmlTextWriter.RenderBeginTag("");
			htmlTextWriter.RenderEndTag();
			string text = stringWriter.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf("style=\"");
				if (num >= 0)
				{
					num += 7;
					int num2 = text.IndexOf("\"", num);
					arg = text.Substring(num, num2 - num);
				}
				num = text.IndexOf("class=\"");
				if (num >= 0)
				{
					num += 7;
					int num3 = text.IndexOf("\"", num);
					arg2 = text.Substring(num, num3 - num);
				}
			}
			return string.Format("\"{0}\": [\"{1}\", \"{2}\"]", name, arg, arg2);
		}

		// Token: 0x0600A1F4 RID: 41460 RVA: 0x00240394 File Offset: 0x0023E594
		internal static string GetClientSideHash(Hashtable table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			int num = 0;
			foreach (object obj in table.Keys)
			{
				string text = (string)obj;
				stringBuilder.AppendFormat("\"{0}\":{1}", text, table[text]);
				if (num < table.Count - 1)
				{
					stringBuilder.Append(",");
				}
				num++;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
