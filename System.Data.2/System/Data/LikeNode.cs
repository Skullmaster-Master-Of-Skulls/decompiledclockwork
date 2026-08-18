using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000E7 RID: 231
	internal sealed class LikeNode : BinaryNode
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x0007BA8C File Offset: 0x0007AE8C
		internal LikeNode(DataTable table, int op, ExpressionNode left, ExpressionNode right) : base(table, op, left, right)
		{
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0007BAA4 File Offset: 0x0007AEA4
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			object obj = this.left.Eval(row, version);
			if (obj == DBNull.Value || (this.left.IsSqlColumn && DataStorage.IsObjectSqlNull(obj)))
			{
				return DBNull.Value;
			}
			string text;
			if (this.pattern == null)
			{
				object obj2 = this.right.Eval(row, version);
				if (!(obj2 is string) && !(obj2 is SqlString))
				{
					base.SetTypeMismatchError(this.op, obj.GetType(), obj2.GetType());
				}
				if (obj2 == DBNull.Value || DataStorage.IsObjectSqlNull(obj2))
				{
					return DBNull.Value;
				}
				string pat = (string)SqlConvert.ChangeType2(obj2, StorageType.String, typeof(string), base.FormatProvider);
				text = this.AnalyzePattern(pat);
				if (this.right.IsConstant())
				{
					this.pattern = text;
				}
			}
			else
			{
				text = this.pattern;
			}
			if (!(obj is string) && !(obj is SqlString))
			{
				base.SetTypeMismatchError(this.op, obj.GetType(), typeof(string));
			}
			char[] trimChars = new char[]
			{
				' ',
				'\u3000'
			};
			string text2;
			if (obj is SqlString)
			{
				text2 = ((SqlString)obj).Value;
			}
			else
			{
				text2 = (string)obj;
			}
			string s = text2.TrimEnd(trimChars);
			switch (this.kind)
			{
			case 1:
				return base.table.IndexOf(s, text) == 0;
			case 2:
			{
				string s2 = text.TrimEnd(trimChars);
				return base.table.IsSuffix(s, s2);
			}
			case 3:
				return 0 <= base.table.IndexOf(s, text);
			case 4:
				return base.table.Compare(s, text) == 0;
			case 5:
				return true;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0007BC84 File Offset: 0x0007B084
		internal string AnalyzePattern(string pat)
		{
			int length = pat.Length;
			char[] array = new char[length + 1];
			pat.CopyTo(0, array, 0, length);
			array[length] = '\0';
			char[] array2 = new char[length + 1];
			int num = 0;
			int num2 = 0;
			int i = 0;
			while (i < length)
			{
				if (array[i] != '*')
				{
					if (array[i] != '%')
					{
						if (array[i] != '[')
						{
							array2[num++] = array[i];
							i++;
							continue;
						}
						i++;
						if (i >= length)
						{
							throw ExprException.InvalidPattern(pat);
						}
						array2[num++] = array[i++];
						if (i >= length)
						{
							throw ExprException.InvalidPattern(pat);
						}
						if (array[i] != ']')
						{
							throw ExprException.InvalidPattern(pat);
						}
						i++;
						continue;
					}
				}
				while ((array[i] == '*' || array[i] == '%') && i < length)
				{
					i++;
				}
				if ((i < length && num > 0) || num2 >= 2)
				{
					throw ExprException.InvalidPattern(pat);
				}
				num2++;
			}
			string result = new string(array2, 0, num);
			if (num2 == 0)
			{
				this.kind = 4;
			}
			else if (num > 0)
			{
				if (array[0] == '*' || array[0] == '%')
				{
					if (array[length - 1] == '*' || array[length - 1] == '%')
					{
						this.kind = 3;
					}
					else
					{
						this.kind = 2;
					}
				}
				else
				{
					this.kind = 1;
				}
			}
			else
			{
				this.kind = 5;
			}
			return result;
		}

		// Token: 0x0400048C RID: 1164
		internal const int match_left = 1;

		// Token: 0x0400048D RID: 1165
		internal const int match_right = 2;

		// Token: 0x0400048E RID: 1166
		internal const int match_middle = 3;

		// Token: 0x0400048F RID: 1167
		internal const int match_exact = 4;

		// Token: 0x04000490 RID: 1168
		internal const int match_all = 5;

		// Token: 0x04000491 RID: 1169
		private int kind;

		// Token: 0x04000492 RID: 1170
		private string pattern;
	}
}
