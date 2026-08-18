using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001B RID: 27
	internal class TimeCriterion : SelectionCriterion
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002728 File Offset: 0x00000928
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Which.ToString()).Append(" ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ").Append(this.Time.ToString("yyyy-MM-dd-HH:mm:ss"));
			return stringBuilder.ToString();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002798 File Offset: 0x00000998
		internal override bool Evaluate(string filename)
		{
			DateTime x;
			switch (this.Which)
			{
			case WhichTime.atime:
				x = File.GetLastAccessTime(filename).ToUniversalTime();
				break;
			case WhichTime.mtime:
				x = File.GetLastWriteTime(filename).ToUniversalTime();
				break;
			case WhichTime.ctime:
				x = File.GetCreationTime(filename).ToUniversalTime();
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return this._Evaluate(x);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002808 File Offset: 0x00000A08
		private bool _Evaluate(DateTime x)
		{
			bool result;
			switch (this.Operator)
			{
			case ComparisonOperator.GreaterThan:
				result = (x > this.Time);
				break;
			case ComparisonOperator.GreaterThanOrEqualTo:
				result = (x >= this.Time);
				break;
			case ComparisonOperator.LesserThan:
				result = (x < this.Time);
				break;
			case ComparisonOperator.LesserThanOrEqualTo:
				result = (x <= this.Time);
				break;
			case ComparisonOperator.EqualTo:
				result = (x == this.Time);
				break;
			case ComparisonOperator.NotEqualTo:
				result = (x != this.Time);
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000028A4 File Offset: 0x00000AA4
		internal override bool Evaluate(ZipEntry entry)
		{
			DateTime x;
			switch (this.Which)
			{
			case WhichTime.atime:
				x = entry.AccessedTime;
				break;
			case WhichTime.mtime:
				x = entry.ModifiedTime;
				break;
			case WhichTime.ctime:
				x = entry.CreationTime;
				break;
			default:
				throw new ArgumentException("??time");
			}
			return this._Evaluate(x);
		}

		// Token: 0x04000043 RID: 67
		internal ComparisonOperator Operator;

		// Token: 0x04000044 RID: 68
		internal WhichTime Which;

		// Token: 0x04000045 RID: 69
		internal DateTime Time;
	}
}
