using System;
using System.Text.RegularExpressions;

namespace Renci.SshNet
{
	// Token: 0x0200001C RID: 28
	public class ExpectAction
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000048CC File Offset: 0x00002ACC
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000048D4 File Offset: 0x00002AD4
		public Regex Expect { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000048DD File Offset: 0x00002ADD
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000048E5 File Offset: 0x00002AE5
		public Action<string> Action { get; private set; }

		// Token: 0x0600013B RID: 315 RVA: 0x000048EE File Offset: 0x00002AEE
		public ExpectAction(Regex expect, Action<string> action)
		{
			if (expect == null)
			{
				throw new ArgumentNullException("expect");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.Expect = expect;
			this.Action = action;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004920 File Offset: 0x00002B20
		public ExpectAction(string expect, Action<string> action)
		{
			if (expect == null)
			{
				throw new ArgumentNullException("expect");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.Expect = new Regex(Regex.Escape(expect));
			this.Action = action;
		}
	}
}
