using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001A RID: 26
	internal class SizeCriterion : SelectionCriterion
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002608 File Offset: 0x00000808
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("size ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ").Append(this.Size.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000265C File Offset: 0x0000085C
		internal override bool Evaluate(string filename)
		{
			FileInfo fileInfo = new FileInfo(filename);
			return this._Evaluate(fileInfo.Length);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000267C File Offset: 0x0000087C
		private bool _Evaluate(long Length)
		{
			bool result;
			switch (this.Operator)
			{
			case ComparisonOperator.GreaterThan:
				result = (Length > this.Size);
				break;
			case ComparisonOperator.GreaterThanOrEqualTo:
				result = (Length >= this.Size);
				break;
			case ComparisonOperator.LesserThan:
				result = (Length < this.Size);
				break;
			case ComparisonOperator.LesserThanOrEqualTo:
				result = (Length <= this.Size);
				break;
			case ComparisonOperator.EqualTo:
				result = (Length == this.Size);
				break;
			case ComparisonOperator.NotEqualTo:
				result = (Length != this.Size);
				break;
			default:
				throw new ArgumentException("Operator");
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000270F File Offset: 0x0000090F
		internal override bool Evaluate(ZipEntry entry)
		{
			return this._Evaluate(entry.UncompressedSize);
		}

		// Token: 0x04000041 RID: 65
		internal ComparisonOperator Operator;

		// Token: 0x04000042 RID: 66
		internal long Size;
	}
}
