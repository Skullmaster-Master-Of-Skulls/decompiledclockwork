using System;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x0200004A RID: 74
	public class Sparkline : ISparkline
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0002984C File Offset: 0x0002884C
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00029890 File Offset: 0x00028890
		public CellRange DataRange
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
			set
			{
				int a_ = 8;
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AB;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						if (value.LastColumn - value.Column == 1)
						{
							goto IL_AB;
						}
						goto IL_B5;
					case 2:
						goto IL_B3;
					case 3:
						num = 1;
						continue;
					}
					if (value.LastRow - value.Row == 1)
					{
						num = 3;
						continue;
					}
					goto IL_B5;
					IL_AB:
					num = 2;
				}
				IL_B3:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("稽ℿ㙁╃ᑅ⥇⑉⭋⭍", a_), RecordTableEnumerator.b("樽⠿❁摃㑅⥇⑉⭋⭍灏㽑⅓╕ⱗ穙㹛㭝䁟ݡᱣե൧ཀྵ࡫乭ͯ᭱ᩳᅵᑷό屻౽ꪃ", a_));
				IL_B5:
				this.ᜀ = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0002995C File Offset: 0x0002895C
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x000299A0 File Offset: 0x000289A0
		public CellRange RefRange
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
			set
			{
				int a_ = 16;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_A9;
					case 2:
						if (value.Columns.Length != 1)
						{
							goto IL_99;
						}
						goto IL_AB;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_99;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value.Rows.Length == 1)
					{
						num = 0;
						continue;
					}
					break;
					IL_99:
					if (true)
					{
					}
					num = 1;
				}
				IL_64:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᑅⵇⱉ⥋㱍㕏㱑㝓㍕੗㭙㉛㥝՟", a_), RecordTableEnumerator.b("ᑅⵇⱉ⥋㱍㕏㱑㝓㍕硗⡙㵛そݟݡ䑣ཥ᭧䩩ɫŭѯ剱ɳ᝵ᑷ፹᡻剽ꁿ첁ꢇ낏ﮑ望뚕鍊뺝펟쎡즣쎥袧즩쎫슭얯\udfb1\udab3隵ힷ좹鲻첽꾿뗁", a_));
				IL_A9:
				goto IL_64;
				IL_AB:
				this.ᜁ = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00029A60 File Offset: 0x00028A60
		public int Column
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Column;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00029AA8 File Offset: 0x00028AA8
		public int Row
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.Row;
			}
		}

		// Token: 0x040000E3 RID: 227
		private int \u2460\u00AE\u0083\u0085;

		// Token: 0x040000E4 RID: 228
		private long \u25D8\u0091\u0097\u009D;

		// Token: 0x040000E5 RID: 229
		private int \u25D8\u00A4\u0087\u0092;

		// Token: 0x040000E6 RID: 230
		private bool[] \u2460\u0090\u00AB\u0082;

		// Token: 0x040000E7 RID: 231
		private CellRange ᜀ;

		// Token: 0x040000E8 RID: 232
		private CellRange ᜁ;
	}
}
