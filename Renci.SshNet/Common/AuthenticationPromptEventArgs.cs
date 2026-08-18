using System;
using System.Collections.Generic;

namespace Renci.SshNet.Common
{
	// Token: 0x020000EA RID: 234
	public class AuthenticationPromptEventArgs : AuthenticationEventArgs
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002064D File Offset: 0x0001E84D
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00020655 File Offset: 0x0001E855
		public string Language { get; private set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002065E File Offset: 0x0001E85E
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00020666 File Offset: 0x0001E866
		public string Instruction { get; private set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002066F File Offset: 0x0001E86F
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00020677 File Offset: 0x0001E877
		public IEnumerable<AuthenticationPrompt> Prompts { get; private set; }

		// Token: 0x060009D1 RID: 2513 RVA: 0x00020680 File Offset: 0x0001E880
		public AuthenticationPromptEventArgs(string username, string instruction, string language, IEnumerable<AuthenticationPrompt> prompts) : base(username)
		{
			this.Instruction = instruction;
			this.Language = language;
			this.Prompts = prompts;
		}
	}
}
