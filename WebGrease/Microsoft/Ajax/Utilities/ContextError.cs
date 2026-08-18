using System;
using System.ComponentModel;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D4 RID: 212
	public class ContextError
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00041F28 File Offset: 0x00040128
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x00041F30 File Offset: 0x00040130
		public int ErrorNumber { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00041F39 File Offset: 0x00040139
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x00041F41 File Offset: 0x00040141
		public string File { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00041F4A File Offset: 0x0004014A
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x00041F52 File Offset: 0x00040152
		public virtual int Severity { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00041F5B File Offset: 0x0004015B
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00041F63 File Offset: 0x00040163
		public virtual string Subcategory { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00041F6C File Offset: 0x0004016C
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00041F74 File Offset: 0x00040174
		[Localizable(false)]
		public virtual string ErrorCode { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00041F7D File Offset: 0x0004017D
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00041F85 File Offset: 0x00040185
		public virtual int StartLine { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00041F8E File Offset: 0x0004018E
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00041F96 File Offset: 0x00040196
		public virtual int StartColumn { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00041F9F File Offset: 0x0004019F
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00041FA7 File Offset: 0x000401A7
		public virtual int EndLine { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00041FB0 File Offset: 0x000401B0
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x00041FB8 File Offset: 0x000401B8
		public virtual int EndColumn { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00041FC1 File Offset: 0x000401C1
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x00041FC9 File Offset: 0x000401C9
		public virtual string Message { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00041FD2 File Offset: 0x000401D2
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00041FDA File Offset: 0x000401DA
		public virtual bool IsError { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00041FE3 File Offset: 0x000401E3
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00041FEB File Offset: 0x000401EB
		public string HelpKeyword { get; set; }

		// Token: 0x06000E2F RID: 3631 RVA: 0x00041FF4 File Offset: 0x000401F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.File))
			{
				stringBuilder.Append(this.File);
			}
			if (this.StartLine > 0)
			{
				stringBuilder.AppendFormat("({0}", this.StartLine);
				if (this.EndLine > this.StartLine)
				{
					if (this.StartColumn > 0 && this.EndColumn > 0)
					{
						stringBuilder.AppendFormat(",{0},{1},{2}", this.StartColumn, this.EndLine, this.EndColumn);
					}
					else
					{
						stringBuilder.AppendFormat("-{0}", this.EndLine);
					}
				}
				else if (this.StartColumn > 0)
				{
					stringBuilder.AppendFormat(",{0}", this.StartColumn);
					if (this.EndColumn > this.StartColumn)
					{
						stringBuilder.AppendFormat("-{0}", this.EndColumn);
					}
				}
				stringBuilder.Append(')');
			}
			stringBuilder.Append(':');
			if (!string.IsNullOrEmpty(this.Subcategory))
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(this.Subcategory);
			}
			stringBuilder.Append(this.IsError ? " error " : " warning ");
			if (!string.IsNullOrEmpty(this.ErrorCode))
			{
				stringBuilder.Append(this.ErrorCode);
			}
			stringBuilder.Append(": ");
			if (!string.IsNullOrEmpty(this.Message))
			{
				stringBuilder.Append(this.Message);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00042188 File Offset: 0x00040388
		internal static string GetSubcategory(int severity)
		{
			switch (severity)
			{
			case 0:
				return CommonStrings.Severity0;
			case 1:
				return CommonStrings.Severity1;
			case 2:
				return CommonStrings.Severity2;
			case 3:
				return CommonStrings.Severity3;
			case 4:
				return CommonStrings.Severity4;
			default:
				return CommonStrings.SeverityUnknown.FormatInvariant(new object[]
				{
					severity
				});
			}
		}
	}
}
