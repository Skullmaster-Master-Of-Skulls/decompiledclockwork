using System;
using System.Text;

namespace System.Net
{
	// Token: 0x020004BE RID: 1214
	internal class ResponseDescription
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x00096325 File Offset: 0x00095325
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00096343 File Offset: 0x00095343
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x00096364 File Offset: 0x00095364
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x00096385 File Offset: 0x00095385
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000963A6 File Offset: 0x000953A6
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x04002553 RID: 9555
		internal const int NoStatus = -1;

		// Token: 0x04002554 RID: 9556
		internal bool Multiline;

		// Token: 0x04002555 RID: 9557
		internal int Status = -1;

		// Token: 0x04002556 RID: 9558
		internal string StatusDescription;

		// Token: 0x04002557 RID: 9559
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x04002558 RID: 9560
		internal string StatusCodeString;
	}
}
