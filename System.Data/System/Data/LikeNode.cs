using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020001A5 RID: 421
	internal sealed class LikeNode : BinaryNode
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x00253868 File Offset: 0x00252C68
		internal LikeNode(DataTable table, int op, ExpressionNode left, ExpressionNode right) : base(table, op, left, right)
		{
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00253888 File Offset: 0x00252C88
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
				return 0 == base.table.IndexOf(s, text);
			case 2:
			{
				string s2 = text.TrimEnd(trimChars);
				return base.table.IsSuffix(s, s2);
			}
			case 3:
				return 0 <= base.table.IndexOf(s, text);
			case 4:
				return 0 == base.table.Compare(s, text);
			case 5:
				return true;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00253A78 File Offset: 0x00252E78
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

		// Token: 0x04000D5B RID: 3419
		internal const int match_left = 1;

		// Token: 0x04000D5C RID: 3420
		internal const int match_right = 2;

		// Token: 0x04000D5D RID: 3421
		internal const int match_middle = 3;

		// Token: 0x04000D5E RID: 3422
		internal const int match_exact = 4;

		// Token: 0x04000D5F RID: 3423
		internal const int match_all = 5;

		// Token: 0x04000D60 RID: 3424
		private int kind;

		// Token: 0x04000D61 RID: 3425
		private string pattern;
	}
}
