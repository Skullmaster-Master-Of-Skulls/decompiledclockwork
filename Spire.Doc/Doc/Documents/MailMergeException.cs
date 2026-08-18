using System;
using Spire.CompoundFile.Doc;

namespace Spire.Doc.Documents
{
	// Token: 0x020004EA RID: 1258
	public class MailMergeException : Exception
	{
		// Token: 0x06004116 RID: 16662 RVA: 0x003D9450 File Offset: 0x003D8450
		public MailMergeException()
		{
			int a_ = 6;
			base..ctor(ClipboardData.b("╫m፯ᵱٳѵᵷ᥹ࡻ幽ﮁ겋늑煉벛얟킡쎣쎥袧첩얫쮭\udcaf횱잳", a_));
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x003D947C File Offset: 0x003D847C
		public MailMergeException(Exception innerExc)
		{
			int a_ = 5;
			this..ctor(ClipboardData.b("≪ͬ౮ṰŲݴቶེ᩸嵼౾ꮊ놐ﺒﺖ뮚爵펠쒢삤螦쾨슪좬쎮햰삲", a_), innerExc);
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x003D94A8 File Offset: 0x003D84A8
		public MailMergeException(string message) : base(message)
		{
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x003D94BC File Offset: 0x003D84BC
		public MailMergeException(string message, Exception innerExc) : base(message, innerExc)
		{
		}

		// Token: 0x04003377 RID: 13175
		private int \u2593\u0092\u00A5\u0098;

		// Token: 0x04003378 RID: 13176
		private string \u2460\u00A9\u008D\u0095;

		// Token: 0x04003379 RID: 13177
		private int \u2460\u00A8\u00A7\u0086;

		// Token: 0x0400337A RID: 13178
		private byte[] \u2460\u00A9\u0087\u0083;

		// Token: 0x0400337B RID: 13179
		private int \u2460\u00A6\u0091\u00A5;

		// Token: 0x0400337C RID: 13180
		private bool[] \u2593\u00A6\u00A1\u00B0;

		// Token: 0x0400337D RID: 13181
		private const string ᜀ = "Incorrect syntax of mail merge fields";
	}
}
