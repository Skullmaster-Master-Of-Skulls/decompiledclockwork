using System;
using System.Collections.Generic;
using System.Text;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Authentication
{
	// Token: 0x020000C1 RID: 193
	[Message("SSH_MSG_USERAUTH_INFO_REQUEST", 60)]
	internal class InformationRequestMessage : Message
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		public string Name { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001F201 File Offset: 0x0001D401
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0001F209 File Offset: 0x0001D409
		public string Instruction { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0001F212 File Offset: 0x0001D412
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0001F21A File Offset: 0x0001D41A
		public string Language { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0001F223 File Offset: 0x0001D423
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0001F22B File Offset: 0x0001D42B
		public IEnumerable<AuthenticationPrompt> Prompts { get; private set; }

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001F234 File Offset: 0x0001D434
		protected override void LoadData()
		{
			this.Name = base.ReadString(Encoding.UTF8);
			this.Instruction = base.ReadString(Encoding.UTF8);
			this.Language = base.ReadString(SshData.Ascii);
			uint num = base.ReadUInt32();
			List<AuthenticationPrompt> list = new List<AuthenticationPrompt>();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				string request = base.ReadString(Encoding.UTF8);
				bool isEchoed = base.ReadBoolean();
				list.Add(new AuthenticationPrompt(num2, isEchoed, request));
				num2++;
			}
			this.Prompts = list;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		protected override void SaveData()
		{
			throw new NotImplementedException();
		}
	}
}
