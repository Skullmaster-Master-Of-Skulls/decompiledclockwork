using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200001D RID: 29
	internal class TypeCriterion : SelectionCriterion
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002A82 File Offset: 0x00000C82
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002A8F File Offset: 0x00000C8F
		internal string AttributeString
		{
			get
			{
				return this.ObjectType.ToString();
			}
			set
			{
				if (value.Length != 1 || (value[0] != 'D' && value[0] != 'F'))
				{
					throw new ArgumentException("Specify a single character: either D or F");
				}
				this.ObjectType = value[0];
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("type ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ").Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002B18 File Offset: 0x00000D18
		internal override bool Evaluate(string filename)
		{
			bool flag = (this.ObjectType == 'D') ? Directory.Exists(filename) : File.Exists(filename);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002B50 File Offset: 0x00000D50
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = (this.ObjectType == 'D') ? entry.IsDirectory : (!entry.IsDirectory);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x0400004A RID: 74
		private char ObjectType;

		// Token: 0x0400004B RID: 75
		internal ComparisonOperator Operator;
	}
}
