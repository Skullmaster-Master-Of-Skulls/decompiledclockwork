using System;
using System.Globalization;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000054 RID: 84
	public struct GeneratedCodeMapping
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public GeneratedCodeMapping(int startLine, int startColumn, int startGeneratedColumn, int codeLength)
		{
			this = new GeneratedCodeMapping(null, startLine, startColumn, startGeneratedColumn, codeLength);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010FF9 File Offset: 0x0000F1F9
		public GeneratedCodeMapping(int startOffset, int startLine, int startColumn, int startGeneratedColumn, int codeLength)
		{
			this = new GeneratedCodeMapping(new int?(startOffset), startLine, startColumn, startGeneratedColumn, codeLength);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00011010 File Offset: 0x0000F210
		private GeneratedCodeMapping(int? startOffset, int startLine, int startColumn, int startGeneratedColumn, int codeLength)
		{
			this = default(GeneratedCodeMapping);
			if (startLine < 0)
			{
				throw new ArgumentOutOfRangeException("startLine", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"startLine",
					"0"
				}));
			}
			if (startColumn < 0)
			{
				throw new ArgumentOutOfRangeException("startColumn", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"startColumn",
					"0"
				}));
			}
			if (startGeneratedColumn < 0)
			{
				throw new ArgumentOutOfRangeException("startGeneratedColumn", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"startGeneratedColumn",
					"0"
				}));
			}
			if (codeLength < 0)
			{
				throw new ArgumentOutOfRangeException("codeLength", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"codeLength",
					"0"
				}));
			}
			this.StartOffset = startOffset;
			this.StartLine = startLine;
			this.StartColumn = startColumn;
			this.StartGeneratedColumn = startGeneratedColumn;
			this.CodeLength = codeLength;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00011123 File Offset: 0x0000F323
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001112B File Offset: 0x0000F32B
		public int? StartOffset { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00011134 File Offset: 0x0000F334
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001113C File Offset: 0x0000F33C
		public int CodeLength { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00011145 File Offset: 0x0000F345
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0001114D File Offset: 0x0000F34D
		public int StartColumn { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00011156 File Offset: 0x0000F356
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001115E File Offset: 0x0000F35E
		public int StartGeneratedColumn { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00011167 File Offset: 0x0000F367
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0001116F File Offset: 0x0000F36F
		public int StartLine { get; set; }

		// Token: 0x060003EA RID: 1002 RVA: 0x00011178 File Offset: 0x0000F378
		public override bool Equals(object obj)
		{
			if (!(obj is GeneratedCodeMapping))
			{
				return false;
			}
			GeneratedCodeMapping generatedCodeMapping = (GeneratedCodeMapping)obj;
			return this.CodeLength == generatedCodeMapping.CodeLength && this.StartColumn == generatedCodeMapping.StartColumn && this.StartGeneratedColumn == generatedCodeMapping.StartGeneratedColumn && this.StartLine == generatedCodeMapping.StartLine && (this.StartOffset == null || generatedCodeMapping.StartOffset == null || this.StartOffset.Equals(generatedCodeMapping.StartOffset));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011218 File Offset: 0x0000F418
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}, {1}, {2}) -> (?, {3}) [{4}]", new object[]
			{
				(this.StartOffset == null) ? "?" : this.StartOffset.Value.ToString(CultureInfo.CurrentCulture),
				this.StartLine,
				this.StartColumn,
				this.StartGeneratedColumn,
				this.CodeLength
			});
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000112AC File Offset: 0x0000F4AC
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.CodeLength).Add(this.StartColumn).Add(this.StartGeneratedColumn).Add(this.StartLine).Add(this.StartOffset).CombinedHash;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000112FF File Offset: 0x0000F4FF
		public static bool operator ==(GeneratedCodeMapping left, GeneratedCodeMapping right)
		{
			return left.Equals(right);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00011314 File Offset: 0x0000F514
		public static bool operator !=(GeneratedCodeMapping left, GeneratedCodeMapping right)
		{
			return !left.Equals(right);
		}
	}
}
