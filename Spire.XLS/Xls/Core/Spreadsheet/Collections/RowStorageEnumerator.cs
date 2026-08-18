using System;
using System.Collections;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001F6 RID: 502
	public class RowStorageEnumerator : IEnumerator
	{
		// Token: 0x06001C7C RID: 7292 RVA: 0x000F65D8 File Offset: 0x000F55D8
		private RowStorageEnumerator()
		{
			this.ᜁ = -1;
			base..ctor();
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000F65F4 File Offset: 0x000F55F4
		internal RowStorageEnumerator(sprᱧ A_0, RecordExtractor A_1)
		{
			int a_ = 17;
			this.ᜁ = -1;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆♈㱊", a_));
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⡊≌㵎㕐ᙒⵔ⍖⭘㩚㹜⭞๠ᅢ", a_));
			}
			this.ᜀ = A_0;
			this.ᜂ = A_1;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000F6658 File Offset: 0x000F5658
		public void Reset()
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
			this.ᜁ = -1;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000F669C File Offset: 0x000F569C
		public bool MoveNext()
		{
			int num = 2;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (this.ᜁ == -1)
					{
						num = 9;
						continue;
					}
					int num2 = this.ᜀ.\u1717(this.ᜁ);
					num = 7;
					continue;
				}
				case 1:
					return result;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					return result;
				case 4:
					return result;
				case 5:
					return result;
				case 6:
					result = false;
					num = 1;
					continue;
				case 7:
				{
					int num2;
					if (num2 == this.ᜀ.ᜈ())
					{
						num = 8;
						continue;
					}
					this.ᜁ = num2;
					result = true;
					num = 4;
					continue;
				}
				case 8:
					if (true)
					{
					}
					this.ᜁ = -1;
					result = false;
					num = 5;
					continue;
				case 9:
					this.ᜁ = 0;
					result = true;
					num = 3;
					continue;
				}
				if (this.ᜀ.ᜈ() == 0)
				{
					num = 6;
				}
				else
				{
					num = 0;
				}
			}
			return result;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000F67D4 File Offset: 0x000F57D4
		public object Current
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
				return this.ᜂ.ᜀ(this.ᜀ.\u171D(), this.ᜁ, this.ᜀ.ᜆ());
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000F6838 File Offset: 0x000F5838
		[CLSCompliant(false)]
		internal spr\u225F ᜀ()
		{
			int a_ = 17;
			if (this.ᜁ != -1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ᜀ.ᜑ(this.ᜁ);
				}
			}
			throw new InvalidOperationException(RecordTableEnumerator.b("Ɇ❈㹊⁌⩎⍐㉒⅔㡖⭘筚ⵜぞࡠൢᅤɦ᭨䭪Ѭᱮ兰ᵲᩴͶ奸ࡺ᡼୾ꆀꞆ권漣뮚튠힢쒤즦쪨캪", a_));
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000F68AC File Offset: 0x000F58AC
		public string GetFormulaStringValue()
		{
			int a_ = 17;
			if (this.ᜁ != -1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ᜀ.ᜐ(this.ᜁ);
				}
			}
			throw new InvalidOperationException(RecordTableEnumerator.b("Ɇ❈㹊⁌⩎⍐㉒⅔㡖⭘筚ⵜぞࡠൢᅤɦ᭨䭪Ѭᱮ兰ᵲᩴͶ奸ࡺ᡼୾ꆀꞆ권漣뮚튠힢쒤즦쪨캪", a_));
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000F6920 File Offset: 0x000F5920
		public int RowIndex
		{
			get
			{
				int a_ = 8;
				if (this.ᜁ != -1)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.ᜀ.\u171A(this.ᜁ);
					}
				}
				if (true)
				{
				}
				throw new InvalidOperationException(RecordTableEnumerator.b("笽⸿㝁⥃⍅㩇⭉㡋⅍≏牑⑓㥕ㅗ㑙⡛㭝቟䉡ൣᕥ䡧ѩͫᩭ偯űᅳɵ塷๹፻幽ꒃ늑ﶓﶛ쎟잡", a_));
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000F6994 File Offset: 0x000F5994
		public int ColumnIndex
		{
			get
			{
				int a_ = 2;
				if (this.ᜁ != -1)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this.ᜀ.ᜉ(this.ᜁ);
					}
				}
				if (true)
				{
				}
				throw new InvalidOperationException(RecordTableEnumerator.b("紷吹䤻匽┿ぁ╃㉅❇㡉汋㹍㽏㭑㩓≕㵗⡙籛㝝፟䉡੣॥ᱧ䩩Ὣ୭ѯ剱s᥵塷᭹ቻ幽ﺉ겋ﺏ蓮鍊", a_));
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000F6A08 File Offset: 0x000F5A08
		public int XFIndex
		{
			get
			{
				int a_ = 14;
				if (this.ᜁ != -1)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return (int)this.ᜀ.ᜀ(this.ᜁ, false);
					}
				}
				throw new InvalidOperationException(RecordTableEnumerator.b("Ń⡅㵇❉⥋㱍ㅏ♑㭓⑕硗⩙㍛㝝๟ᙡţᑥ䡧ͩὫ乭ṯᵱs噵୷όࡻ幽ꒃꪉ摒뢗풟쎡쪣얥춧", a_));
			}
		}

		// Token: 0x04001080 RID: 4224
		private int \u25D9\u00A1\u008A\u008D;

		// Token: 0x04001081 RID: 4225
		private sprᱧ ᜀ;

		// Token: 0x04001082 RID: 4226
		private int ᜁ;

		// Token: 0x04001083 RID: 4227
		private RecordExtractor ᜂ;
	}
}
