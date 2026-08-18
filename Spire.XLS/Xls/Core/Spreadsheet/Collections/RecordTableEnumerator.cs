using System;
using System.Collections;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000217 RID: 535
	public class RecordTableEnumerator : IDictionaryEnumerator
	{
		// Token: 0x06001EF8 RID: 7928 RVA: 0x00105E24 File Offset: 0x00104E24
		private RecordTableEnumerator()
		{
			this.ᜁ = -1;
			base..ctor();
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00105E40 File Offset: 0x00104E40
		internal RecordTableEnumerator(XlsCellRecordCollection A_0)
		{
			int a_ = 18;
			this.ᜁ = -1;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㱇⭉⹋≍㕏", a_));
			}
			this.ᜀ = A_0;
			this.ᜂ = A_0.Table;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00105E90 File Offset: 0x00104E90
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
			this.ᜁ = this.ᜂ.ᜁ() - 1;
			this.ᜃ = 0;
			this.ᜀ();
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00105EEC File Offset: 0x00104EEC
		public object Current
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						sprᱧ sprᱧ;
						if (sprᱧ == null)
						{
							num = 1;
							continue;
						}
						goto IL_80;
					}
					case 1:
						goto IL_7C;
					case 3:
						goto IL_39;
					}
					if (this.ᜁ < 0)
					{
						if (true)
						{
						}
						num = 3;
					}
					else
					{
						sprᱧ sprᱧ = this.ᜂ.ᜄ().ᜁ(this.ᜁ);
						num = 0;
					}
				}
				IL_39:
				return null;
				IL_7C:
				return new DictionaryEntry(null, null);
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				default:
				{
					if (false)
					{
					}
					sprᱧ sprᱧ;
					spr\u23A5 spr_u23A = (spr\u23A5)sprᱧ.ᜈ(this.ᜃ);
					long num2 = sprṔ.ᜀ(spr_u23A.ᜅ() + 1, spr_u23A.ᜄ() + 1);
					return new DictionaryEntry(num2, spr_u23A);
				}
				}
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00105FD0 File Offset: 0x00104FD0
		public bool MoveNext()
		{
			for (;;)
			{
				int num = this.ᜂ.ᜇ();
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_101;
					case 1:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num2 = 9;
							continue;
						}
						goto IL_18B;
					}
					case 2:
					{
						if (this.ᜁ < 0)
						{
							goto IL_70;
						}
						sprᱧ sprᱧ2 = this.ᜂ.ᜄ().ᜁ(this.ᜁ);
						num2 = 10;
						continue;
					}
					case 3:
						goto IL_107;
					case 4:
						goto IL_107;
					case 5:
						goto IL_186;
					case 6:
						goto IL_1AA;
					case 7:
						goto IL_18B;
					case 8:
					{
						sprᱧ sprᱧ;
						if (sprᱧ.ᜈ() <= 0)
						{
							num2 = 7;
							continue;
						}
						return true;
					}
					case 9:
						num2 = 8;
						continue;
					case 10:
					{
						sprᱧ sprᱧ2;
						if (sprᱧ2 != null)
						{
							num2 = 15;
							continue;
						}
						return false;
					}
					case 11:
					{
						sprᱧ sprᱧ2;
						if (this.ᜃ >= sprᱧ2.ᜈ())
						{
							num2 = 5;
							continue;
						}
						return true;
					}
					case 12:
						this.ᜁ = this.ᜂ.ᜁ();
						this.ᜃ = 0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 14;
							continue;
						}
						break;
					case 13:
					{
						if (this.ᜁ > num)
						{
							num2 = 0;
							continue;
						}
						sprᱧ sprᱧ = this.ᜂ.ᜄ().ᜁ(this.ᜁ);
						num2 = 3;
						continue;
					}
					case 14:
						if (this.ᜁ >= 0)
						{
							num2 = 17;
							continue;
						}
						return false;
					case 15:
					{
						sprᱧ sprᱧ2;
						this.ᜃ = sprᱧ2.\u1717(this.ᜃ);
						num2 = 11;
						continue;
					}
					case 16:
					{
						if (this.ᜁ > num)
						{
							num2 = 6;
							continue;
						}
						this.ᜁ++;
						sprᱧ sprᱧ = this.ᜂ.ᜄ().ᜁ(this.ᜁ);
						num2 = 4;
						continue;
					}
					case 17:
						num2 = 13;
						continue;
					}
					break;
					IL_70:
					num2 = 12;
					continue;
					IL_107:
					num2 = 1;
					continue;
					IL_18B:
					num2 = 16;
				}
			}
			return false;
			IL_101:
			return false;
			IL_186:
			return this.ᜀ();
			IL_1AA:
			return true;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00106228 File Offset: 0x00105228
		private bool ᜀ()
		{
			for (;;)
			{
				int num = this.ᜂ.ᜇ();
				spr\u223C spr_u223C = this.ᜂ.ᜄ();
				this.ᜁ++;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (this.ᜁ > num)
						{
							num2 = 1;
							continue;
						}
						sprᱧ sprᱧ = spr_u223C.ᜁ(this.ᜁ);
						num2 = 6;
						continue;
					}
					case 1:
						return false;
					case 2:
						if (true)
						{
						}
						num2 = 7;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BA;
						default:
							goto IL_86;
						}
						break;
					case 4:
						goto IL_C7;
					case 5:
						goto IL_C7;
					case 6:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							goto IL_BA;
						}
						goto IL_58;
					}
					case 7:
					{
						sprᱧ sprᱧ;
						if (sprᱧ.ᜈ() > 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_58;
					}
					}
					break;
					IL_58:
					this.ᜁ++;
					num2 = 5;
					continue;
					IL_BA:
					num2 = 2;
					continue;
					IL_C7:
					num2 = 0;
				}
			}
			IL_86:
			if (false)
			{
			}
			this.ᜃ = 0;
			return true;
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00106350 File Offset: 0x00105350
		public object Key
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
				return this.Entry.Key;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0010639C File Offset: 0x0010539C
		public object Value
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
				return this.Entry.Value;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x001063E8 File Offset: 0x001053E8
		public DictionaryEntry Entry
		{
			get
			{
				while (this.ᜁ < 0)
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
						throw new InvalidOperationException();
					}
				}
				sprᱧ sprᱧ = this.ᜂ.ᜄ().ᜁ(this.ᜁ);
				spr\u23A5 spr_u23A = (spr\u23A5)sprᱧ.ᜈ(this.ᜃ);
				return new DictionaryEntry(sprṔ.ᜀ(spr_u23A.ᜅ() + 1, spr_u23A.ᜄ() + 1), spr_u23A);
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00106480 File Offset: 0x00105480
		internal static string b(string A_0, int A_1)
		{
			char[] array = A_0.ToCharArray();
			int num = 1422056245 + A_1;
			int num3;
			int num2;
			if ((num2 = (num3 = 0)) < 1)
			{
				goto IL_47;
			}
			IL_14:
			int num5;
			int num4 = num5 = num2;
			char[] array2 = array;
			int num6 = num5;
			char c = array[num5];
			byte b = (byte)((int)(c & 'ÿ') ^ num++);
			byte b2 = (byte)((int)(c >> 8) ^ num++);
			byte b3 = b2;
			b2 = b;
			b = b3;
			array2[num6] = (ushort)((int)b2 << 8 | (int)b);
			num3 = num4 + 1;
			IL_47:
			if ((num2 = num3) >= array.Length)
			{
				return string.Intern(new string(array));
			}
			goto IL_14;
		}

		// Token: 0x040010E4 RID: 4324
		private XlsCellRecordCollection ᜀ;

		// Token: 0x040010E5 RID: 4325
		private int ᜁ;

		// Token: 0x040010E6 RID: 4326
		private float \u25D8\u0086\u0086\u0085;

		// Token: 0x040010E7 RID: 4327
		private sprủ ᜂ;

		// Token: 0x040010E8 RID: 4328
		private int ᜃ;
	}
}
