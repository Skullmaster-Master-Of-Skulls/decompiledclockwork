using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000199 RID: 409
	internal class ResponseDescription
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00053888 File Offset: 0x00051A88
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000538A6 File Offset: 0x00051AA6
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x000538C7 File Offset: 0x00051AC7
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x000538E8 File Offset: 0x00051AE8
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00053909 File Offset: 0x00051B09
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x0400130C RID: 4876
		internal const int NoStatus = -1;

		// Token: 0x0400130D RID: 4877
		internal bool Multiline;

		// Token: 0x0400130E RID: 4878
		internal int Status = -1;

		// Token: 0x0400130F RID: 4879
		internal string StatusDescription;

		// Token: 0x04001310 RID: 4880
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x04001311 RID: 4881
		internal string StatusCodeString;
	}
}
