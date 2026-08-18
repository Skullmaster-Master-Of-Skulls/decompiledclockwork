using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x02000487 RID: 1159
	[Serializable]
	internal class StringExpressionSet
	{
		// Token: 0x06002DEC RID: 11756 RVA: 0x000998B7 File Offset: 0x000988B7
		public StringExpressionSet() : this(true, null, false)
		{
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000998C2 File Offset: 0x000988C2
		public StringExpressionSet(string str) : this(true, str, false)
		{
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000998CD File Offset: 0x000988CD
		public StringExpressionSet(bool ignoreCase, bool throwOnRelative) : this(ignoreCase, null, throwOnRelative)
		{
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000998D8 File Offset: 0x000988D8
		public StringExpressionSet(bool ignoreCase, string str, bool throwOnRelative)
		{
			this.m_list = null;
			this.m_ignoreCase = ignoreCase;
			this.m_throwOnRelative = throwOnRelative;
			if (str == null)
			{
				this.m_expressions = null;
				return;
			}
			this.AddExpressions(str);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x00099907 File Offset: 0x00098907
		protected virtual StringExpressionSet CreateNewEmpty()
		{
			return new StringExpressionSet();
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x00099910 File Offset: 0x00098910
		public virtual StringExpressionSet Copy()
		{
			StringExpressionSet stringExpressionSet = this.CreateNewEmpty();
			if (this.m_list != null)
			{
				stringExpressionSet.m_list = new ArrayList(this.m_list);
			}
			stringExpressionSet.m_expressions = this.m_expressions;
			stringExpressionSet.m_ignoreCase = this.m_ignoreCase;
			stringExpressionSet.m_throwOnRelative = this.m_throwOnRelative;
			return stringExpressionSet;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x00099962 File Offset: 0x00098962
		public void SetThrowOnRelative(bool throwOnRelative)
		{
			this.m_throwOnRelative = throwOnRelative;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x0009996B File Offset: 0x0009896B
		private static string StaticProcessWholeString(string str)
		{
			return str.Replace(StringExpressionSet.m_alternateDirectorySeparator, StringExpressionSet.m_directorySeparator);
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0009997D File Offset: 0x0009897D
		private static string StaticProcessSingleString(string str)
		{
			return str.Trim(StringExpressionSet.m_trimChars);
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0009998A File Offset: 0x0009898A
		protected virtual string ProcessWholeString(string str)
		{
			return StringExpressionSet.StaticProcessWholeString(str);
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x00099992 File Offset: 0x00098992
		protected virtual string ProcessSingleString(string str)
		{
			return StringExpressionSet.StaticProcessSingleString(str);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0009999C File Offset: 0x0009899C
		public void AddExpressions(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return;
			}
			str = this.ProcessWholeString(str);
			if (this.m_expressions == null)
			{
				this.m_expressions = str;
			}
			else
			{
				this.m_expressions = this.m_expressions + StringExpressionSet.m_separators[0] + str;
			}
			this.m_expressionsArray = null;
			string[] array = this.Split(str);
			if (this.m_list == null)
			{
				this.m_list = new ArrayList();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					string text = this.ProcessSingleString(array[i]);
					int num = text.IndexOf('\0');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					if (text != null && !text.Equals(""))
					{
						if (this.m_throwOnRelative)
						{
							if ((text.Length < 3 || text[1] != ':' || text[2] != '\\' || ((text[0] < 'a' || text[0] > 'z') && (text[0] < 'A' || text[0] > 'Z'))) && (text.Length < 2 || text[0] != '\\' || text[1] != '\\'))
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
							}
							text = StringExpressionSet.CanonicalizePath(text);
						}
						this.m_list.Add(text);
					}
				}
			}
			this.Reduce();
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x00099B1C File Offset: 0x00098B1C
		public void AddExpressions(string[] str, bool checkForDuplicates, bool needFullPath)
		{
			this.AddExpressions(StringExpressionSet.CreateListFromExpressions(str, needFullPath), checkForDuplicates);
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x00099B2C File Offset: 0x00098B2C
		public void AddExpressions(ArrayList exprArrayList, bool checkForDuplicates)
		{
			this.m_expressionsArray = null;
			this.m_expressions = null;
			if (this.m_list != null)
			{
				this.m_list.AddRange(exprArrayList);
			}
			else
			{
				this.m_list = new ArrayList(exprArrayList);
			}
			if (checkForDuplicates)
			{
				this.Reduce();
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x00099B68 File Offset: 0x00098B68
		internal static ArrayList CreateListFromExpressions(string[] str, bool needFullPath)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == null)
				{
					throw new ArgumentNullException("str");
				}
				string text = StringExpressionSet.StaticProcessWholeString(str[i]);
				if (text != null && text.Length != 0)
				{
					string text2 = StringExpressionSet.StaticProcessSingleString(text);
					int num = text2.IndexOf('\0');
					if (num != -1)
					{
						text2 = text2.Substring(0, num);
					}
					if (text2 != null && text2.Length != 0)
					{
						if ((text2.Length < 3 || text2[1] != ':' || text2[2] != '\\' || ((text2[0] < 'a' || text2[0] > 'z') && (text2[0] < 'A' || text2[0] > 'Z'))) && (text2.Length < 2 || text2[0] != '\\' || text2[1] != '\\'))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
						}
						text2 = StringExpressionSet.CanonicalizePath(text2, needFullPath);
						arrayList.Add(text2);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x00099C82 File Offset: 0x00098C82
		protected void CheckList()
		{
			if (this.m_list == null && this.m_expressions != null)
			{
				this.CreateList();
			}
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x00099C9C File Offset: 0x00098C9C
		protected string[] Split(string expressions)
		{
			if (this.m_throwOnRelative)
			{
				ArrayList arrayList = new ArrayList();
				string[] array = expressions.Split(new char[]
				{
					'"'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (i % 2 == 0)
					{
						string[] array2 = array[i].Split(new char[]
						{
							';'
						});
						for (int j = 0; j < array2.Length; j++)
						{
							if (array2[j] != null && !array2[j].Equals(""))
							{
								arrayList.Add(array2[j]);
							}
						}
					}
					else
					{
						arrayList.Add(array[i]);
					}
				}
				string[] array3 = new string[arrayList.Count];
				IEnumerator enumerator = arrayList.GetEnumerator();
				int num = 0;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					array3[num++] = (string)obj;
				}
				return array3;
			}
			return expressions.Split(StringExpressionSet.m_separators);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00099D84 File Offset: 0x00098D84
		protected void CreateList()
		{
			string[] array = this.Split(this.m_expressions);
			this.m_list = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && !array[i].Equals(""))
				{
					string text = this.ProcessSingleString(array[i]);
					int num = text.IndexOf('\0');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					if (text != null && !text.Equals(""))
					{
						if (this.m_throwOnRelative)
						{
							if ((text.Length < 3 || text[1] != ':' || text[2] != '\\' || ((text[0] < 'a' || text[0] > 'z') && (text[0] < 'A' || text[0] > 'Z'))) && (text.Length < 2 || text[0] != '\\' || text[1] != '\\'))
							{
								throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
							}
							text = StringExpressionSet.CanonicalizePath(text);
						}
						this.m_list.Add(text);
					}
				}
			}
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x00099EA5 File Offset: 0x00098EA5
		public bool IsEmpty()
		{
			if (this.m_list == null)
			{
				return this.m_expressions == null;
			}
			return this.m_list.Count == 0;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x00099EC8 File Offset: 0x00098EC8
		public bool IsSubsetOf(StringExpressionSet ses)
		{
			if (this.IsEmpty())
			{
				return true;
			}
			if (ses == null || ses.IsEmpty())
			{
				return false;
			}
			this.CheckList();
			ses.CheckList();
			for (int i = 0; i < this.m_list.Count; i++)
			{
				if (!this.StringSubsetStringExpression((string)this.m_list[i], ses, this.m_ignoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00099F34 File Offset: 0x00098F34
		public bool IsSubsetOfPathDiscovery(StringExpressionSet ses)
		{
			if (this.IsEmpty())
			{
				return true;
			}
			if (ses == null || ses.IsEmpty())
			{
				return false;
			}
			this.CheckList();
			ses.CheckList();
			for (int i = 0; i < this.m_list.Count; i++)
			{
				if (!StringExpressionSet.StringSubsetStringExpressionPathDiscovery((string)this.m_list[i], ses, this.m_ignoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x00099F9C File Offset: 0x00098F9C
		public StringExpressionSet Union(StringExpressionSet ses)
		{
			if (ses == null || ses.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return ses.Copy();
			}
			this.CheckList();
			ses.CheckList();
			StringExpressionSet stringExpressionSet = (ses.m_list.Count > this.m_list.Count) ? ses : this;
			StringExpressionSet stringExpressionSet2 = (ses.m_list.Count <= this.m_list.Count) ? ses : this;
			StringExpressionSet stringExpressionSet3 = stringExpressionSet.Copy();
			stringExpressionSet3.Reduce();
			for (int i = 0; i < stringExpressionSet2.m_list.Count; i++)
			{
				stringExpressionSet3.AddSingleExpressionNoDuplicates((string)stringExpressionSet2.m_list[i]);
			}
			stringExpressionSet3.GenerateString();
			return stringExpressionSet3;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0009A054 File Offset: 0x00099054
		public StringExpressionSet Intersect(StringExpressionSet ses)
		{
			if (this.IsEmpty() || ses == null || ses.IsEmpty())
			{
				return this.CreateNewEmpty();
			}
			this.CheckList();
			ses.CheckList();
			StringExpressionSet stringExpressionSet = this.CreateNewEmpty();
			for (int i = 0; i < this.m_list.Count; i++)
			{
				for (int j = 0; j < ses.m_list.Count; j++)
				{
					if (this.StringSubsetString((string)this.m_list[i], (string)ses.m_list[j], this.m_ignoreCase))
					{
						if (stringExpressionSet.m_list == null)
						{
							stringExpressionSet.m_list = new ArrayList();
						}
						stringExpressionSet.AddSingleExpressionNoDuplicates((string)this.m_list[i]);
					}
					else if (this.StringSubsetString((string)ses.m_list[j], (string)this.m_list[i], this.m_ignoreCase))
					{
						if (stringExpressionSet.m_list == null)
						{
							stringExpressionSet.m_list = new ArrayList();
						}
						stringExpressionSet.AddSingleExpressionNoDuplicates((string)ses.m_list[j]);
					}
				}
			}
			stringExpressionSet.GenerateString();
			return stringExpressionSet;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0009A184 File Offset: 0x00099184
		protected void GenerateString()
		{
			if (this.m_list != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				IEnumerator enumerator = this.m_list.GetEnumerator();
				bool flag = true;
				while (enumerator.MoveNext())
				{
					if (!flag)
					{
						stringBuilder.Append(StringExpressionSet.m_separators[0]);
					}
					else
					{
						flag = false;
					}
					string text = (string)enumerator.Current;
					if (text != null)
					{
						int num = text.IndexOf(StringExpressionSet.m_separators[0]);
						if (num != -1)
						{
							stringBuilder.Append('"');
						}
						stringBuilder.Append(text);
						if (num != -1)
						{
							stringBuilder.Append('"');
						}
					}
				}
				this.m_expressions = stringBuilder.ToString();
				return;
			}
			this.m_expressions = null;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0009A225 File Offset: 0x00099225
		public override string ToString()
		{
			this.CheckList();
			this.Reduce();
			this.GenerateString();
			return this.m_expressions;
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0009A23F File Offset: 0x0009923F
		public string[] ToStringArray()
		{
			if (this.m_expressionsArray == null && this.m_list != null)
			{
				this.m_expressionsArray = (string[])this.m_list.ToArray(typeof(string));
			}
			return this.m_expressionsArray;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x0009A278 File Offset: 0x00099278
		protected bool StringSubsetStringExpression(string left, StringExpressionSet right, bool ignoreCase)
		{
			for (int i = 0; i < right.m_list.Count; i++)
			{
				if (this.StringSubsetString(left, (string)right.m_list[i], ignoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x0009A2BC File Offset: 0x000992BC
		protected static bool StringSubsetStringExpressionPathDiscovery(string left, StringExpressionSet right, bool ignoreCase)
		{
			for (int i = 0; i < right.m_list.Count; i++)
			{
				if (StringExpressionSet.StringSubsetStringPathDiscovery(left, (string)right.m_list[i], ignoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0009A2FC File Offset: 0x000992FC
		protected virtual bool StringSubsetString(string left, string right, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (right == null || left == null || right.Length == 0 || left.Length == 0 || right.Length > left.Length)
			{
				return false;
			}
			if (right.Length == left.Length)
			{
				return string.Compare(right, left, comparisonType) == 0;
			}
			if (left.Length - right.Length == 1 && left[left.Length - 1] == StringExpressionSet.m_directorySeparator)
			{
				return string.Compare(left, 0, right, 0, right.Length, comparisonType) == 0;
			}
			if (right[right.Length - 1] == StringExpressionSet.m_directorySeparator)
			{
				return string.Compare(right, 0, left, 0, right.Length, comparisonType) == 0;
			}
			return left[right.Length] == StringExpressionSet.m_directorySeparator && string.Compare(right, 0, left, 0, right.Length, comparisonType) == 0;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0009A3DC File Offset: 0x000993DC
		protected static bool StringSubsetStringPathDiscovery(string left, string right, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (right == null || left == null || right.Length == 0 || left.Length == 0)
			{
				return false;
			}
			if (right.Length == left.Length)
			{
				return string.Compare(right, left, comparisonType) == 0;
			}
			string text;
			string text2;
			if (right.Length < left.Length)
			{
				text = right;
				text2 = left;
			}
			else
			{
				text = left;
				text2 = right;
			}
			return string.Compare(text, 0, text2, 0, text.Length, comparisonType) == 0 && ((text.Length == 3 && text.EndsWith(":\\", StringComparison.Ordinal) && ((text[0] >= 'A' && text[0] <= 'Z') || (text[0] >= 'a' && text[0] <= 'z'))) || text2[text.Length] == StringExpressionSet.m_directorySeparator);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0009A4A8 File Offset: 0x000994A8
		protected void AddSingleExpressionNoDuplicates(string expression)
		{
			int i = 0;
			this.m_expressionsArray = null;
			this.m_expressions = null;
			if (this.m_list == null)
			{
				this.m_list = new ArrayList();
			}
			while (i < this.m_list.Count)
			{
				if (this.StringSubsetString((string)this.m_list[i], expression, this.m_ignoreCase))
				{
					this.m_list.RemoveAt(i);
				}
				else
				{
					if (this.StringSubsetString(expression, (string)this.m_list[i], this.m_ignoreCase))
					{
						return;
					}
					i++;
				}
			}
			this.m_list.Add(expression);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0009A548 File Offset: 0x00099548
		protected void Reduce()
		{
			this.CheckList();
			if (this.m_list == null)
			{
				return;
			}
			for (int i = 0; i < this.m_list.Count - 1; i++)
			{
				int j = i + 1;
				while (j < this.m_list.Count)
				{
					if (this.StringSubsetString((string)this.m_list[j], (string)this.m_list[i], this.m_ignoreCase))
					{
						this.m_list.RemoveAt(j);
					}
					else if (this.StringSubsetString((string)this.m_list[i], (string)this.m_list[j], this.m_ignoreCase))
					{
						this.m_list[i] = this.m_list[j];
						this.m_list.RemoveAt(j);
						j = i + 1;
					}
					else
					{
						j++;
					}
				}
			}
		}

		// Token: 0x06002E0C RID: 11788
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetLongPathName(string path);

		// Token: 0x06002E0D RID: 11789 RVA: 0x0009A638 File Offset: 0x00099638
		internal static string CanonicalizePath(string path)
		{
			return StringExpressionSet.CanonicalizePath(path, true);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0009A644 File Offset: 0x00099644
		internal static string CanonicalizePath(string path, bool needFullPath)
		{
			if (path.IndexOf('~') != -1)
			{
				path = StringExpressionSet.GetLongPathName(path);
			}
			if (path.IndexOf(':', 2) != -1)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
			}
			if (needFullPath)
			{
				string text = Path.GetFullPathInternal(path);
				if (path.EndsWith("\\.", StringComparison.Ordinal))
				{
					if (text.EndsWith("\\", StringComparison.Ordinal))
					{
						text += ".";
					}
					else
					{
						text += "\\.";
					}
				}
				return text;
			}
			return path;
		}

		// Token: 0x040017A7 RID: 6055
		protected ArrayList m_list;

		// Token: 0x040017A8 RID: 6056
		protected bool m_ignoreCase;

		// Token: 0x040017A9 RID: 6057
		protected string m_expressions;

		// Token: 0x040017AA RID: 6058
		protected string[] m_expressionsArray;

		// Token: 0x040017AB RID: 6059
		protected bool m_throwOnRelative;

		// Token: 0x040017AC RID: 6060
		protected static readonly char[] m_separators = new char[]
		{
			';'
		};

		// Token: 0x040017AD RID: 6061
		protected static readonly char[] m_trimChars = new char[]
		{
			' '
		};

		// Token: 0x040017AE RID: 6062
		protected static readonly char m_directorySeparator = '\\';

		// Token: 0x040017AF RID: 6063
		protected static readonly char m_alternateDirectorySeparator = '/';
	}
}
