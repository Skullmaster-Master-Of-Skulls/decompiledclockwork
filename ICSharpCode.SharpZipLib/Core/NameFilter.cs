using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000057 RID: 87
	public class NameFilter : IScanFilter
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0001547B File Offset: 0x0001447B
		public NameFilter(string filter)
		{
			this.filter_ = filter;
			this.inclusions_ = new ArrayList();
			this.exclusions_ = new ArrayList();
			this.Compile();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000154A8 File Offset: 0x000144A8
		public static bool IsValidExpression(string expression)
		{
			bool result = true;
			try
			{
				new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000154D8 File Offset: 0x000144D8
		public static bool IsValidFilterExpression(string toTest)
		{
			bool result = true;
			try
			{
				if (toTest != null)
				{
					string[] array = NameFilter.SplitQuoted(toTest);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null && array[i].Length > 0)
						{
							string pattern;
							if (array[i][0] == '+')
							{
								pattern = array[i].Substring(1, array[i].Length - 1);
							}
							else if (array[i][0] == '-')
							{
								pattern = array[i].Substring(1, array[i].Length - 1);
							}
							else
							{
								pattern = array[i];
							}
							new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
						}
					}
				}
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001557C File Offset: 0x0001457C
		public static string[] SplitQuoted(string original)
		{
			char c = '\\';
			char[] array = new char[]
			{
				';'
			};
			ArrayList arrayList = new ArrayList();
			if (original != null && original.Length > 0)
			{
				int i = -1;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < original.Length)
				{
					i++;
					if (i >= original.Length)
					{
						arrayList.Add(stringBuilder.ToString());
					}
					else if (original[i] == c)
					{
						i++;
						if (i >= original.Length)
						{
							throw new ArgumentException("Missing terminating escape character", "original");
						}
						if (Array.IndexOf<char>(array, original[i]) < 0)
						{
							stringBuilder.Append(c);
						}
						stringBuilder.Append(original[i]);
					}
					else if (Array.IndexOf<char>(array, original[i]) >= 0)
					{
						arrayList.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(original[i]);
					}
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001568C File Offset: 0x0001468C
		public override string ToString()
		{
			return this.filter_;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00015694 File Offset: 0x00014694
		public bool IsIncluded(string name)
		{
			bool result = false;
			if (this.inclusions_.Count == 0)
			{
				result = true;
			}
			else
			{
				foreach (object obj in this.inclusions_)
				{
					Regex regex = (Regex)obj;
					if (regex.IsMatch(name))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015708 File Offset: 0x00014708
		public bool IsExcluded(string name)
		{
			bool result = false;
			foreach (object obj in this.exclusions_)
			{
				Regex regex = (Regex)obj;
				if (regex.IsMatch(name))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001576C File Offset: 0x0001476C
		public bool IsMatch(string name)
		{
			return this.IsIncluded(name) && !this.IsExcluded(name);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015784 File Offset: 0x00014784
		private void Compile()
		{
			if (this.filter_ == null)
			{
				return;
			}
			string[] array = NameFilter.SplitQuoted(this.filter_);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && array[i].Length > 0)
				{
					bool flag = array[i][0] != '-';
					string pattern;
					if (array[i][0] == '+')
					{
						pattern = array[i].Substring(1, array[i].Length - 1);
					}
					else if (array[i][0] == '-')
					{
						pattern = array[i].Substring(1, array[i].Length - 1);
					}
					else
					{
						pattern = array[i];
					}
					if (flag)
					{
						this.inclusions_.Add(new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
					}
					else
					{
						this.exclusions_.Add(new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
					}
				}
			}
		}

		// Token: 0x040002A7 RID: 679
		private string filter_;

		// Token: 0x040002A8 RID: 680
		private ArrayList inclusions_;

		// Token: 0x040002A9 RID: 681
		private ArrayList exclusions_;
	}
}
