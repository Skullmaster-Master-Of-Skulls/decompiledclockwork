using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001C RID: 28
	internal class NameCriterion : SelectionCriterion
	{
		// Token: 0x1700000E RID: 14
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002904 File Offset: 0x00000B04
		internal virtual string MatchingFileSpec
		{
			set
			{
				if (Directory.Exists(value))
				{
					this._MatchingFileSpec = ".\\" + value + "\\*.*";
				}
				else
				{
					this._MatchingFileSpec = value;
				}
				this._regexString = "^" + Regex.Escape(this._MatchingFileSpec).Replace("\\\\\\*\\.\\*", "\\\\([^\\.]+|.*\\.[^\\\\\\.]*)").Replace("\\.\\*", "\\.[^\\\\\\.]*").Replace("\\*", ".*").Replace("\\?", "[^\\\\\\.]") + "$";
				this._re = new Regex(this._regexString, RegexOptions.IgnoreCase);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000029A8 File Offset: 0x00000BA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("name ").Append(EnumUtil.GetDescription(this.Operator)).Append(" '").Append(this._MatchingFileSpec).Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002A01 File Offset: 0x00000C01
		internal override bool Evaluate(string filename)
		{
			return this._Evaluate(filename);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002A0C File Offset: 0x00000C0C
		private bool _Evaluate(string fullpath)
		{
			string input = (this._MatchingFileSpec.IndexOf('\\') == -1) ? Path.GetFileName(fullpath) : fullpath;
			bool flag = this._re.IsMatch(input);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002A50 File Offset: 0x00000C50
		internal override bool Evaluate(ZipEntry entry)
		{
			string fullpath = entry.FileName.Replace("/", "\\");
			return this._Evaluate(fullpath);
		}

		// Token: 0x04000046 RID: 70
		private Regex _re;

		// Token: 0x04000047 RID: 71
		private string _regexString;

		// Token: 0x04000048 RID: 72
		internal ComparisonOperator Operator;

		// Token: 0x04000049 RID: 73
		private string _MatchingFileSpec;
	}
}
