using System;
using Spire.CompoundFile.Doc;

namespace Spire.Doc
{
	// Token: 0x020000F5 RID: 245
	public class Hyphenation
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00040BCC File Offset: 0x0003FBCC
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00040C14 File Offset: 0x0003FC14
		public bool AutoHyphenation
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
				return this.ᜀ.ᜈ();
			}
			set
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
				this.ᜀ.ᜄ(value);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00040C5C File Offset: 0x0003FC5C
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00040CA4 File Offset: 0x0003FCA4
		public bool HyphenateCaps
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
				return this.ᜀ.ᜂ();
			}
			set
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
				this.ᜀ.ᜂ(value);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00040CEC File Offset: 0x0003FCEC
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00040D3C File Offset: 0x0003FD3C
		public float HyphenationZone
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
				return (float)this.ᜀ.ᜀ() / 20f;
			}
			set
			{
				int a_ = 4;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 2:
						goto IL_52;
					case 3:
						IL_8A:
						if (value > 1584f)
						{
							num = 2;
							continue;
						}
						goto IL_9F;
					}
					if ((double)value >= 0.05)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("≩ᕫṭᡯ᝱ᩳ᝵౷፹፻ၽꁿꪉﮍ뒓ﶗ몙ﺛﮝ풟햡솣쎥욧誩鲫肭肯螱钳욵첷骹\uddbb킽꒿ﻉ뻍꓏ﳑ", a_));
				IL_9F:
				this.ᜀ.ᜃ((int)(value * 20f));
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00040DFC File Offset: 0x0003FDFC
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x00040E44 File Offset: 0x0003FE44
		public int ConsecutiveHyphensLimit
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
				return this.ᜀ.ᜡ();
			}
			set
			{
				int a_ = 9;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_37;
					case 1:
						IL_81:
						if (value > 32767)
						{
							num = 0;
							continue;
						}
						goto IL_93;
					case 3:
						num = 1;
						continue;
					}
					if (value >= 0)
					{
						num = 3;
						continue;
					}
					IL_37:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						goto IL_57;
					}
				}
				IL_57:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("ⱮṰᵲٴቶ᩸๺ॼᙾꖄﮊﾐ떔ﮖ膠캢키풦\udda8讪쾬쪮醰톲킴쎶캸\udeba\ud8bc톾ꛆꟈ꿊ﳎ", a_));
				IL_93:
				this.ᜀ.ᜂ(value);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00040EF0 File Offset: 0x0003FEF0
		internal Hyphenation(spr\u202E A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x04000D92 RID: 3474
		private long \u2593\u00AC\u00A5\u0080;

		// Token: 0x04000D93 RID: 3475
		private byte \u2593\u00A1\u0095\u0092;

		// Token: 0x04000D94 RID: 3476
		private float[] \u25D9\u008A\u008B\u009D;

		// Token: 0x04000D95 RID: 3477
		private spr\u202E ᜀ;
	}
}
