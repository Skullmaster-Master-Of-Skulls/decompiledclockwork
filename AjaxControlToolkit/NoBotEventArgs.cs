using System;

namespace AjaxControlToolkit
{
	// Token: 0x0200014D RID: 333
	public class NoBotEventArgs : EventArgs
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00017864 File Offset: 0x00015A64
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0001786C File Offset: 0x00015A6C
		public string ChallengeScript
		{
			get
			{
				return this._challengeScript;
			}
			set
			{
				this._challengeScript = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00017875 File Offset: 0x00015A75
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0001787D File Offset: 0x00015A7D
		public string RequiredResponse
		{
			get
			{
				return this._requiredResponse;
			}
			set
			{
				this._requiredResponse = value;
			}
		}

		// Token: 0x04000373 RID: 883
		private string _challengeScript = string.Empty;

		// Token: 0x04000374 RID: 884
		private string _requiredResponse = string.Empty;
	}
}
