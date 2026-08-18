using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x0200049F RID: 1183
	public class ListLevel : DocumentSerializable
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x003D4940 File Offset: 0x003D3940
		// (set) Token: 0x06004094 RID: 16532 RVA: 0x003D4984 File Offset: 0x003D3984
		public ListNumberAlignment NumberAlignment
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06004095 RID: 16533 RVA: 0x003D49C8 File Offset: 0x003D39C8
		// (set) Token: 0x06004096 RID: 16534 RVA: 0x003D4A0C File Offset: 0x003D3A0C
		public int StartAt
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
				return this.\u1715;
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
				this.\u1715 = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x003D4A50 File Offset: 0x003D3A50
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x003D4ABC File Offset: 0x003D3ABC
		public float TabSpaceAfter
		{
			get
			{
				if (this.ᜎ.Tabs.Count > 0)
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
						return this.ᜎ.Tabs[0].Position;
					}
				}
				return 0f;
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
				this.ᜎ.Tabs.AddTab(value);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x003D4B0C File Offset: 0x003D3B0C
		// (set) Token: 0x0600409A RID: 16538 RVA: 0x003D4B54 File Offset: 0x003D3B54
		public float TextPosition
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
				return this.ᜎ.LeftIndent;
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
				this.ᜎ.LeftIndent = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x003D4B9C File Offset: 0x003D3B9C
		// (set) Token: 0x0600409C RID: 16540 RVA: 0x003D4BE0 File Offset: 0x003D3BE0
		public string NumberPrefix
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x003D4C24 File Offset: 0x003D3C24
		// (set) Token: 0x0600409E RID: 16542 RVA: 0x003D4C68 File Offset: 0x003D3C68
		public string NumberSufix
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜐ;
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
				this.ᜐ = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x003D4CAC File Offset: 0x003D3CAC
		// (set) Token: 0x060040A0 RID: 16544 RVA: 0x003D4CF0 File Offset: 0x003D3CF0
		public string BulletCharacter
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x003D4D34 File Offset: 0x003D3D34
		// (set) Token: 0x060040A2 RID: 16546 RVA: 0x003D4D78 File Offset: 0x003D3D78
		public ListPatternType PatternType
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
				return this.\u1717;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u1717 = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x003D4DBC File Offset: 0x003D3DBC
		// (set) Token: 0x060040A4 RID: 16548 RVA: 0x003D4E00 File Offset: 0x003D3E00
		public bool NoRestartByHigher
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
				return this.\u1714;
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
				this.\u1714 = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x003D4E44 File Offset: 0x003D3E44
		public CharacterFormat CharacterFormat
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
				return this.\u170D;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x003D4E88 File Offset: 0x003D3E88
		public ParagraphFormat ParagraphFormat
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜎ;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060040A7 RID: 16551 RVA: 0x003D4ECC File Offset: 0x003D3ECC
		protected ListStyle OwnerListStyle
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
				return base.OwnerBase as ListStyle;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x003D4F14 File Offset: 0x003D3F14
		protected ListLevel PreviousLevel
		{
			get
			{
				ListStyle ownerListStyle;
				int num2;
				for (;;)
				{
					ownerListStyle = this.OwnerListStyle;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_95;
						case 1:
							if (num2 <= 0)
							{
								goto IL_97;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_27;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 2:
							num2 = ownerListStyle.Levels.ᜀ(this);
							num = 1;
							continue;
						case 3:
							goto IL_27;
						}
						break;
						IL_27:
						if (ownerListStyle == null)
						{
							goto IL_97;
						}
						num = 2;
					}
				}
				IL_95:
				return ownerListStyle.Levels[num2 - 1];
				IL_97:
				return null;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x003D4FBC File Offset: 0x003D3FBC
		// (set) Token: 0x060040AA RID: 16554 RVA: 0x003D5000 File Offset: 0x003D4000
		public FollowCharacterType FollowCharacter
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u1719;
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
				this.\u1719 = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060040AB RID: 16555 RVA: 0x003D5044 File Offset: 0x003D4044
		// (set) Token: 0x060040AC RID: 16556 RVA: 0x003D5088 File Offset: 0x003D4088
		public bool IsLegalStyleNumbering
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
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x003D50CC File Offset: 0x003D40CC
		// (set) Token: 0x060040AE RID: 16558 RVA: 0x003D5114 File Offset: 0x003D4114
		public float NumberPosition
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
				return this.ᜎ.FirstLineIndent;
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
				this.ᜎ.FirstLineIndent = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x003D515C File Offset: 0x003D415C
		// (set) Token: 0x060040B0 RID: 16560 RVA: 0x003D51A0 File Offset: 0x003D41A0
		public bool UsePrevLevelPattern
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
				return this.\u171A;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u171A = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x003D51E4 File Offset: 0x003D41E4
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x003D5228 File Offset: 0x003D4228
		internal bool Word6Legacy
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x003D526C File Offset: 0x003D426C
		// (set) Token: 0x060040B4 RID: 16564 RVA: 0x003D52B0 File Offset: 0x003D42B0
		internal int LegacySpace
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
				return this.\u171D;
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
				this.\u171D = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x003D52F4 File Offset: 0x003D42F4
		// (set) Token: 0x060040B6 RID: 16566 RVA: 0x003D5338 File Offset: 0x003D4338
		internal int LegacyIndent
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
				return this.\u171E;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u171E = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x003D537C File Offset: 0x003D437C
		// (set) Token: 0x060040B8 RID: 16568 RVA: 0x003D53C0 File Offset: 0x003D43C0
		internal string ParaStyleName
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
				return this.\u171F;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u171F = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x003D5404 File Offset: 0x003D4404
		// (set) Token: 0x060040BA RID: 16570 RVA: 0x003D5448 File Offset: 0x003D4448
		internal bool NoLevelText
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x003D548C File Offset: 0x003D448C
		// (set) Token: 0x060040BC RID: 16572 RVA: 0x003D54D0 File Offset: 0x003D44D0
		internal bool NoPlaceholder
		{
			[CompilerGenerated]
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜤ;
			}
			[CompilerGenerated]
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
				this.ᜤ = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x003D5514 File Offset: 0x003D4514
		internal int LevelNumber
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A8;
					case 1:
						goto IL_90;
					case 3:
						if (true)
						{
						}
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_90:
						if (base.OwnerBase is OverrideLevelFormat)
						{
							num = 0;
							continue;
						}
						return -1;
					}
					if (false)
					{
					}
					if (this.OwnerListStyle != null)
					{
						goto IL_AA;
					}
					num = 3;
				}
				return -1;
				IL_A8:
				OverrideLevelFormat overrideLevelFormat = base.OwnerBase as OverrideLevelFormat;
				return (overrideLevelFormat.OwnerBase as spr\u177D).ᜃ().ᜀ(overrideLevelFormat);
				IL_AA:
				return this.OwnerListStyle.Levels.ᜀ(this);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x003D55DC File Offset: 0x003D45DC
		// (set) Token: 0x060040BF RID: 16575 RVA: 0x003D5620 File Offset: 0x003D4620
		internal DocPicture PicBullet
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
				return this.ᜡ;
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
				this.ᜡ = value;
				this.ᜡ.ᜀ(this);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x003D5670 File Offset: 0x003D4670
		// (set) Token: 0x060040C1 RID: 16577 RVA: 0x003D56B4 File Offset: 0x003D46B4
		internal short PicBulletId
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
				return this.ᜢ;
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
				this.ᜢ = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x003D56F8 File Offset: 0x003D46F8
		internal int PicIndex
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.CharacterFormat.ListPictureIndex;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x003D5740 File Offset: 0x003D4740
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x003D5784 File Offset: 0x003D4784
		internal bool IsEmptyPicture
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
				return this.ᜣ;
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
				this.ᜣ = value;
			}
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x003D57C8 File Offset: 0x003D47C8
		public ListLevel(ListStyle listStyle) : this(listStyle.Document)
		{
			base.ᜀ(listStyle);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x003D57E8 File Offset: 0x003D47E8
		internal ListLevel(Document A_0)
		{
			int a_ = 0;
			this.ᜋ = new string[]
			{
				ClipboardData.b("॥٧ཀྵ", a_),
				ClipboardData.b("ብὧթ", a_),
				ClipboardData.b("ብgᡩ५୭", a_),
				ClipboardData.b("eݧὩṫ", a_),
				ClipboardData.b("eŧᱩ५", a_),
				ClipboardData.b("ᕥŧቩ", a_),
				ClipboardData.b("ᕥ൧ᱩ५m", a_),
				ClipboardData.b("ͥŧ൩ѫᩭ", a_),
				ClipboardData.b("ࡥŧѩ५", a_),
				ClipboardData.b("ብ൧ѩ", a_),
				ClipboardData.b("ͥѧཀྵᩫ୭ṯ", a_),
				ClipboardData.b("ብὧཀྵkᡭᕯ", a_),
				ClipboardData.b("ብgͩṫᩭᕯ᝱ᩳ", a_),
				ClipboardData.b("eݧὩṫᩭᕯ᝱ᩳ", a_),
				ClipboardData.b("eŧ౩ᡫ୭ᕯᱱ", a_),
				ClipboardData.b("ᕥŧቩᡫ୭ᕯᱱ", a_),
				ClipboardData.b("ᕥ൧ᱩ५mѯ᝱ᅳᡵ", a_),
				ClipboardData.b("ͥŧ൩ѫᩭᕯ᝱ᩳ", a_),
				ClipboardData.b("ࡥŧѩ५ᩭᕯ᝱ᩳ", a_)
			};
			this.ᜌ = new string[]
			{
				ClipboardData.b("ብ൧ѩ", a_),
				ClipboardData.b("ብὧཀྵɫᩭ९", a_),
				ClipboardData.b("ብgͩṫᩭ९", a_),
				ClipboardData.b("eݧᡩᡫ᝭", a_),
				ClipboardData.b("eŧ౩ᡫ᝭", a_),
				ClipboardData.b("ᕥŧቩᡫ᝭", a_),
				ClipboardData.b("ᕥ൧ᱩ५mѯୱ", a_),
				ClipboardData.b("ͥŧ൩ѫᩭ९", a_),
				ClipboardData.b("ࡥŧѩ५ᩭ९", a_)
			};
			this.ᜑ = ClipboardData.b("䡥", a_);
			this.\u1712 = string.Empty;
			this.\u171B = new byte[9];
			base..ctor(A_0, null);
			this.\u170D = this.m_doc.CreateCharacterFormatImpl();
			this.\u170D.ᜀ(this);
			this.ᜎ = this.m_doc.CreateParagraphFormatImpl();
			this.ᜎ.ᜀ(this);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x003D5A70 File Offset: 0x003D4A70
		public void CreateLayoutData(string numStr, byte[] characterOffsets, int levelNumber)
		{
			switch (0)
			{
			default:
			{
				string[] array;
				int num;
				int num2;
				int startIndex;
				int length;
				for (;;)
				{
					char[] separator = new char[]
					{
						'\\',
						Convert.ToChar(levelNumber)
					};
					array = numStr.Split(separator);
					num = array[0].Length + 1;
					num2 = 0;
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							this.\u1712 = numStr.Substring(0, num - 1);
							num3 = 10;
							continue;
						case 1:
							goto IL_D3;
						case 2:
							if (characterOffsets[num2 + 1] == 0)
							{
								goto IL_C7;
							}
							goto IL_93;
						case 3:
							goto IL_155;
						case 4:
							num3 = 2;
							continue;
						case 5:
							return;
						case 6:
							goto IL_1D1;
						case 7:
							if ((int)characterOffsets[num2] == num)
							{
								num3 = 12;
								continue;
							}
							num2++;
							num3 = 13;
							continue;
						case 8:
							if (num2 == 0)
							{
								num3 = 0;
								continue;
							}
							startIndex = (int)characterOffsets[num2 - 1];
							length = num - 1 - (int)characterOffsets[num2 - 1];
							this.\u1712 = numStr.Substring(startIndex, length);
							num3 = 6;
							continue;
						case 9:
							if (num2 != 8)
							{
								num3 = 4;
								continue;
							}
							goto IL_14B;
						case 10:
							if (true)
							{
							}
							goto IL_1D1;
						case 11:
							if (num2 >= 9)
							{
								num3 = 5;
								continue;
							}
							num3 = 7;
							continue;
						case 12:
							num3 = 8;
							continue;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C7;
							default:
								if (false)
								{
								}
								goto IL_155;
							}
							break;
						}
						break;
						IL_C7:
						num3 = 1;
						continue;
						IL_155:
						num3 = 11;
						continue;
						IL_1D1:
						num3 = 9;
					}
				}
				IL_93:
				length = (int)characterOffsets[num2 + 1] - (num + 1);
				startIndex = num + 1;
				this.ᜑ = numStr.Substring(startIndex, length);
				return;
				IL_D3:
				IL_14B:
				this.ᜑ = array[1];
				return;
			}
			}
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x003D5C80 File Offset: 0x003D4C80
		public string GetListItemText(int listItemIndex, ListType listType)
		{
			string result;
			for (;;)
			{
				result = string.Empty;
				int num = 2;
				for (;;)
				{
					ListType listType2;
					switch (num)
					{
					case 0:
						num = 9;
						continue;
					case 1:
						return result;
					case 2:
						if (listType == ListType.Bulleted)
						{
							num = 7;
							continue;
						}
						goto IL_F9;
					case 3:
						if (this.PatternType != ListPatternType.Bullet)
						{
							goto IL_D8;
						}
						goto IL_F9;
					case 4:
						return result;
					case 5:
						goto IL_F9;
					case 6:
						return result;
					case 7:
						num = 3;
						continue;
					case 8:
						switch (listType2)
						{
						case ListType.Numbered:
							result = this.ᜂ(listItemIndex);
							num = 4;
							continue;
						case ListType.Bulleted:
							result = this.\u1713;
							num = 1;
							continue;
						case ListType.NoList:
							goto IL_B0;
						default:
							num = 0;
							continue;
						}
						break;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						}
						if (false)
						{
						}
						goto IL_B0;
					case 10:
						listType = ListType.Numbered;
						if (true)
						{
						}
						num = 5;
						continue;
					}
					break;
					IL_B0:
					result = "";
					num = 6;
					continue;
					IL_D8:
					num = 10;
					continue;
					IL_F9:
					listType2 = listType;
					num = 8;
				}
			}
			return result;
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x003D5DB8 File Offset: 0x003D4DB8
		public ListLevel Clone()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return (ListLevel)this.CloneImpl();
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x003D5E00 File Offset: 0x003D4E00
		internal static ListLevel ᜀ(float A_0, string A_1, ListStyle A_2)
		{
			int a_ = 14;
			ListLevel listLevel;
			string fontName;
			for (;;)
			{
				listLevel = A_2.Document.CreateListLevelImpl(A_2);
				listLevel.\u1715 = 1;
				listLevel.\u1717 = ListPatternType.Bullet;
				fontName = ClipboardData.b("⁳ήᕷόཻ幽칿ꚅ\uda87ﺏ", a_);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_136;
						}
						goto Block_2;
					case 1:
						num = 9;
						continue;
					case 2:
						goto IL_126;
					case 3:
						if (A_1 != null)
						{
							num = 1;
							continue;
						}
						goto IL_1AC;
					case 4:
						num = 10;
						continue;
					case 5:
						if (!(A_1 == ClipboardData.b("᭳", a_)))
						{
							num = 4;
							continue;
						}
						fontName = ClipboardData.b("㝳᥵൷ࡹᕻ᭽ꊁ쪃ﾇ", a_);
						num = 8;
						continue;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_A3;
					case 8:
						goto IL_176;
					case 9:
						goto IL_136;
					case 10:
						if (!(A_1 == ClipboardData.b("펃", a_)))
						{
							num = 6;
							continue;
						}
						fontName = ClipboardData.b("⍳ήᙷᵹ᡻᝽", a_);
						num = 2;
						continue;
					case 11:
						num = 5;
						continue;
					}
					break;
					IL_136:
					if (!(A_1 == ClipboardData.b("쎃", a_)))
					{
						num = 11;
					}
					else
					{
						fontName = ClipboardData.b("❳ཱུᕷ᡹፻ች", a_);
						num = 7;
					}
				}
			}
			IL_A3:
			goto IL_1AC;
			Block_2:
			if (false)
			{
			}
			IL_126:
			IL_176:
			IL_1AC:
			if (true)
			{
			}
			listLevel.\u170D.FontName = fontName;
			listLevel.ᜎ.LeftIndent = A_0;
			listLevel.\u1713 = A_1;
			return listLevel;
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x003D5FE4 File Offset: 0x003D4FE4
		internal static ListLevel ᜀ(int A_0, int A_1, ListPatternType A_2, ListNumberAlignment A_3, ListStyle A_4)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			ListLevel listLevel = A_4.Document.CreateListLevelImpl(A_4);
			listLevel.\u1715 = 1;
			listLevel.\u1717 = A_2;
			listLevel.\u1716 = A_3;
			listLevel.NumberPrefix = string.Empty;
			listLevel.NumberSufix = ClipboardData.b("孴", a_);
			listLevel.ᜎ.LeftIndent = (float)A_0;
			listLevel.\u170D.FontName = ClipboardData.b("ⅴṶᑸṺ๼彾쾀Ꞇ\udb88ﾐ", a_);
			return listLevel;
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x003D6094 File Offset: 0x003D5094
		protected override object CloneImpl()
		{
			ListLevel listLevel;
			for (;;)
			{
				for (;;)
				{
					listLevel = (ListLevel)base.CloneImpl();
					listLevel.\u170D = new CharacterFormat(base.Document);
					listLevel.ᜎ = new ParagraphFormat(base.Document);
					listLevel.ᜎ.ImportContainer(this.ParagraphFormat);
					listLevel.\u170D.ImportContainer(this.CharacterFormat);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								listLevel.PicBullet = (this.ᜡ.Clone() as DocPicture);
								num = 1;
								continue;
							case 1:
								goto IL_D3;
							case 2:
								if (this.PicBullet != null)
								{
									num = 0;
									continue;
								}
								goto IL_D5;
							}
							break;
						}
						break;
					}
					}
				}
			}
			IL_D3:
			IL_D5:
			listLevel.\u171B = new byte[this.\u171B.Length];
			this.\u171B.CopyTo(listLevel.\u171B, 0);
			return listLevel;
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x003D619C File Offset: 0x003D519C
		private string ᜂ(int A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
				for (;;)
				{
					ListPatternType u = this.\u1717;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (u != ListPatternType.None)
							{
								goto IL_261;
							}
							num = 1;
							continue;
						case 1:
							if (this.ᜏ != null)
							{
								num = 3;
								continue;
							}
							goto IL_1DD;
						case 2:
							goto IL_206;
						case 3:
							goto IL_1DB;
						case 4:
							if (!this.NoPlaceholder)
							{
								num = 2;
								continue;
							}
							goto IL_2DA;
						case 5:
							switch (u)
							{
							case ListPatternType.Arabic:
								num = 13;
								continue;
							case ListPatternType.UpRoman:
								goto IL_10D;
							case ListPatternType.LowRoman:
								goto IL_19B;
							case ListPatternType.UpLetter:
								goto IL_22A;
							case ListPatternType.LowLetter:
								num = 4;
								continue;
							case ListPatternType.Ordinal:
								goto IL_17F;
							default:
								num = 8;
								continue;
							}
							break;
						case 6:
							if (A_0 < 9)
							{
								num = 11;
								continue;
							}
							goto IL_12D;
						case 7:
							goto IL_156;
						case 8:
							num = 14;
							continue;
						case 9:
							num = 7;
							continue;
						case 10:
							goto IL_108;
						case 11:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_261;
							default:
								goto IL_DD;
							}
							break;
						case 12:
							num = 0;
							continue;
						case 13:
							if (!this.NoPlaceholder)
							{
								num = 10;
								continue;
							}
							goto IL_15B;
						case 14:
							if (u != ListPatternType.LeadingZero)
							{
								num = 12;
								continue;
							}
							num = 6;
							continue;
						}
						break;
						IL_261:
						num = 9;
					}
				}
				IL_DD:
				if (false)
				{
				}
				return this.ᜏ + ClipboardData.b("嵬", a_) + (A_0 + 1).ToString() + this.ᜐ;
				IL_108:
				return this.ᜏ + (A_0 + 1).ToString() + this.ᜐ;
				IL_10D:
				return this.ᜏ + this.ᜁ(A_0 + 1).ToUpper() + this.ᜐ;
				IL_12D:
				return this.ᜏ + (A_0 + 1).ToString() + this.ᜐ;
				IL_156:
				return this.\u1712 + (A_0 + 1).ToString() + this.ᜑ;
				IL_15B:
				return this.ᜏ + this.ᜐ;
				IL_17F:
				return this.\u1712 + this.ᜀ(A_0 + 1, true) + this.ᜑ;
				IL_19B:
				return this.\u1712 + this.ᜁ(A_0 + 1).ToLower() + this.ᜑ;
				IL_1DB:
				return this.ᜏ + this.ᜐ;
				IL_1DD:
				return "";
				IL_206:
				return this.ᜏ + this.ᜀ(A_0 + 1).ToLower() + this.ᜐ;
				IL_22A:
				return this.\u1712 + this.ᜀ(A_0 + 1).ToUpper() + this.ᜑ;
				IL_2DA:
				return this.ᜏ + this.ᜐ;
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x003D64B4 File Offset: 0x003D54B4
		private string ᜁ(int A_0)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ᜀ(ref A_0, 1000, ClipboardData.b("⩦", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 900, ClipboardData.b("⑦⑨", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 500, ClipboardData.b("⍦", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 400, ClipboardData.b("⑦⵨", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 100, ClipboardData.b("⑦", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 90, ClipboardData.b("㽦⩨", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 50, ClipboardData.b("⭦", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 40, ClipboardData.b("㽦╨", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 10, ClipboardData.b("㽦", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 9, ClipboardData.b("⹦ㅨ", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 5, ClipboardData.b("ㅦ", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 4, ClipboardData.b("⹦㽨", a_)));
			stringBuilder.Append(this.ᜀ(ref A_0, 1, ClipboardData.b("⹦", a_)));
			return stringBuilder.ToString();
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x003D66A0 File Offset: 0x003D56A0
		private string ᜀ(int A_0)
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				Stack<int> stack = ListLevel.ᜀ((float)A_0);
				stringBuilder = new StringBuilder();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_30;
					case 1:
						goto IL_30;
					case 2:
						if (stack.Count > 0)
						{
							int a_ = stack.Pop();
							ListLevel.ᜀ(stringBuilder, a_);
							if (true)
							{
							}
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_6F;
					}
					break;
					IL_30:
					num = 2;
				}
			}
			IL_6F:
			return stringBuilder.ToString();
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x003D6748 File Offset: 0x003D5748
		private string ᜀ(int A_0, bool A_1)
		{
			int a_ = 3;
			string result;
			for (;;)
			{
				result = "";
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_DE;
					case 1:
						result = this.ᜋ[A_0];
						num = 6;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D3;
						default:
							goto IL_111;
						}
						break;
					case 3:
						goto IL_4D;
					case 4:
					{
						if (A_0 < 20)
						{
							num = 1;
							continue;
						}
						int num2 = (int)Math.Floor((double)A_0 / 10.0);
						result = this.ᜌ[num2] + ClipboardData.b("䑨", a_) + this.ᜋ[A_0 - num2 * 10];
						goto IL_D3;
					}
					case 5:
						if (A_1)
						{
							num = 3;
							continue;
						}
						num = 7;
						continue;
					case 6:
						goto IL_60;
					case 7:
						if (A_0 > 99)
						{
							num = 2;
							continue;
						}
						num = 4;
						continue;
					}
					break;
					IL_D3:
					num = 0;
				}
			}
			IL_4D:
			throw new NotImplementedException(ClipboardData.b("ᩨὪᑬͮᑰ卲ᥴṶ੸ེ嵼ᅾꖄﮊﲐﮔﲘﾚ붜캠풢", a_));
			IL_60:
			IL_DE:
			return result;
			IL_111:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(ClipboardData.b("⩨੪ͬŮṰݲ啴Ѷ౸୺ർၾꖄﲈ뎒ﲘ漢爵펠莢톤쾦좨얪趬隮袰", a_));
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x003D68A0 File Offset: 0x003D58A0
		private string ᜀ(ref int A_0, int A_1, string A_2)
		{
			if (true)
			{
			}
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 >= A_1)
						{
							A_0 -= A_1;
							stringBuilder.Append(A_2);
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3A;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6B;
					case 2:
						goto IL_3A;
					case 3:
						goto IL_3A;
					}
					break;
					IL_3A:
					num = 0;
				}
			}
			IL_6B:
			return stringBuilder.ToString();
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x003D6938 File Offset: 0x003D5938
		private static Stack<int> ᜀ(float A_0)
		{
			int a_ = 2;
			int num = 3;
			for (;;)
			{
				Stack<int> stack;
				float num2;
				switch (num)
				{
				case 0:
					goto IL_126;
				case 1:
					if (A_0 > 0f)
					{
						num = 10;
						continue;
					}
					return stack;
				case 2:
					goto IL_67;
				case 4:
					if (num2 == 0f)
					{
						num = 7;
						continue;
					}
					A_0 /= 26f;
					num = 0;
					continue;
				case 5:
					num = 1;
					continue;
				case 6:
					if ((float)((int)A_0) <= 26f)
					{
						num = 5;
						continue;
					}
					num2 = A_0 % 26f;
					num = 4;
					continue;
				case 7:
					A_0 = A_0 / 26f - 1f;
					num2 = 26f;
					num = 8;
					continue;
				case 8:
					goto IL_126;
				case 9:
					goto IL_6C;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						stack.Push((int)A_0);
						num = 11;
						continue;
					}
					break;
				case 11:
					return stack;
				case 12:
					goto IL_6C;
				}
				if (A_0 < 0f)
				{
					num = 2;
					continue;
				}
				goto IL_A4;
				IL_6C:
				num = 6;
				continue;
				IL_A4:
				stack = new Stack<int>();
				num = 9;
				continue;
				IL_126:
				stack.Push((int)num2);
				num = 12;
			}
			IL_67:
			throw new ArgumentOutOfRangeException(ClipboardData.b("१ᡩ൫౭᥯ᅱ", a_), A_0, ClipboardData.b("㹧୩k᭭ᕯ剱ᝳ᝵ᙷ婹ቻᅽꊁꢇﶍ늑꒓", a_));
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x003D6AE8 File Offset: 0x003D5AE8
		private static void ᜀ(StringBuilder A_0, int A_1)
		{
			int a_ = 3;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 > 0)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_B0;
				case 1:
					if (A_1 > 26)
					{
						num = 2;
						continue;
					}
					goto IL_D2;
				case 2:
					goto IL_79;
				case 3:
					num = 1;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_52;
					}
					break;
				}
				IL_31:
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
				goto IL_31;
			}
			IL_52:
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("୨ṪѬͮᕰᙲݴ", a_));
			IL_79:
			IL_B0:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ݨṪl൮ᑰŲ", a_), ClipboardData.b("㽨੪Ŭᩮᑰ卲ᙴᙶ᝸孺፼ၾꎂꦈﲎ뎒ꖔ랖列뾞욠톢삤욦\udda8캪\udfac辮莰薲", a_));
			IL_D2:
			char value = (char)(64 + A_1);
			A_0.Append(value);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x003D6BD8 File Offset: 0x003D5BD8
		internal void ᜁ()
		{
			for (;;)
			{
				if (true)
				{
				}
				this.\u171B = null;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u170D != null)
						{
							num = 5;
							continue;
						}
						goto IL_6F;
					case 1:
						if (this.ᜎ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_51;
						default:
							if (false)
							{
							}
							this.ᜎ.Close();
							this.\u170D = null;
							num = 4;
							continue;
						}
						break;
					case 3:
						goto IL_6F;
					case 4:
						return;
					case 5:
						goto IL_51;
					}
					break;
					IL_51:
					this.\u170D.Close();
					this.\u170D = null;
					num = 3;
					continue;
					IL_6F:
					num = 1;
				}
			}
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x003D6CAC File Offset: 0x003D5CAC
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 13;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 30;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("⁲Ŵᙶ୸ེ㱼୾", a_)))
						{
							num = 29;
							continue;
						}
						goto IL_235;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("㽲ၴၶᡸ᡺ѼⱾ", a_)))
						{
							num = 37;
							continue;
						}
						return;
					case 2:
						this.PatternType = (ListPatternType)reader.ReadEnum(ClipboardData.b("⍲ᑴͶ൸Ṻོᅾ햀廒", a_), typeof(ListPatternType));
						num = 24;
						continue;
					case 3:
						goto IL_3A1;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("⍲ݴቶླྀ⭺ᱼ୾", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_573;
					case 5:
						if (!reader.HasAttribute(ClipboardData.b("㵲ᩴ╶ᱸࡺॼṾ", a_)))
						{
							goto IL_3D5;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 28;
							continue;
						}
						break;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("㕲ᩴ᭶ᕸᑺ੼㱾ﾊﶎ", a_)))
						{
							num = 27;
							continue;
						}
						goto IL_1D7;
					case 7:
						goto IL_573;
					case 8:
						this.UsePrevLevelPattern = reader.ReadBoolean(ClipboardData.b("⍲ݴቶླྀ⭺ᱼ୾", a_));
						num = 7;
						continue;
					case 9:
						if (reader.HasAttribute(ClipboardData.b("㽲ၴၶᡸ᡺Ѽ", a_)))
						{
							num = 39;
							continue;
						}
						goto IL_29D;
					case 10:
						goto IL_235;
					case 11:
						goto IL_269;
					case 12:
						if (reader.HasAttribute(ClipboardData.b("ㅲt᭶ᕸṺॼ⽾ﮈ", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_3A1;
					case 13:
						goto IL_602;
					case 14:
						if (reader.HasAttribute(ClipboardData.b("㽲ၴၶᡸ᡺Ѽ㙾ﶈ", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_602;
					case 15:
						this.\u171E = reader.ReadInt(ClipboardData.b("㽲ၴၶᡸ᡺Ѽ㙾ﶈ", a_));
						num = 13;
						continue;
					case 16:
						if (reader.HasAttribute(ClipboardData.b("⍲ݴቶὸ⭺ᱼ୾", a_)))
						{
							num = 25;
							continue;
						}
						this.NumberPrefix = null;
						num = 38;
						continue;
					case 17:
						if (reader.HasAttribute(ClipboardData.b("㩲ٴ㭶ᱸᱺᱼ፾", a_)))
						{
							num = 31;
							continue;
						}
						goto IL_409;
					case 18:
						return;
					case 19:
						this.TextPosition = reader.ReadFloat(ClipboardData.b("㩲᭴፶ᱸᕺॼ", a_));
						num = 34;
						continue;
					case 20:
						if (reader.HasAttribute(ClipboardData.b("⍲ᑴͶ൸Ṻོᅾ햀廒", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_2FB;
					case 21:
						this.NumberAlignment = (ListNumberAlignment)reader.ReadEnum(ClipboardData.b("㵲t᩶᭸Ṻོ㹾", a_), typeof(ListNumberAlignment));
						num = 40;
						continue;
					case 22:
						if (true)
						{
						}
						this.BulletCharacter = reader.ReadString(ClipboardData.b("ㅲt᭶ᕸṺॼ⽾ﮈ", a_));
						num = 3;
						continue;
					case 23:
						if (reader.HasAttribute(ClipboardData.b("㵲t᩶᭸Ṻོ㹾", a_)))
						{
							num = 21;
							continue;
						}
						goto IL_4B9;
					case 24:
						goto IL_2FB;
					case 25:
						this.NumberPrefix = reader.ReadString(ClipboardData.b("⍲ݴቶὸ⭺ᱼ୾", a_));
						num = 43;
						continue;
					case 26:
						if (reader.HasAttribute(ClipboardData.b("⁲tᅶ⥸᩺ॼ୾", a_)))
						{
							num = 36;
							continue;
						}
						this.NumberSufix = null;
						num = 33;
						continue;
					case 27:
						this.FollowCharacter = (FollowCharacterType)reader.ReadEnum(ClipboardData.b("㕲ᩴ᭶ᕸᑺ੼㱾ﾊﶎ", a_), typeof(FollowCharacterType));
						num = 41;
						continue;
					case 28:
						this.NoRestartByHigher = reader.ReadBoolean(ClipboardData.b("㵲ᩴ╶ᱸࡺॼṾ", a_));
						num = 32;
						continue;
					case 29:
						this.StartAt = reader.ReadInt(ClipboardData.b("⁲Ŵᙶ୸ེ㱼୾", a_));
						num = 10;
						continue;
					case 30:
						if (reader.HasAttribute(ClipboardData.b("㩲᭴፶ᱸᕺॼ", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_5CE;
					case 31:
						this.IsLegalStyleNumbering = reader.ReadBoolean(ClipboardData.b("㩲ٴ㭶ᱸᱺᱼ፾", a_));
						num = 35;
						continue;
					case 32:
						goto IL_3D5;
					case 33:
						goto IL_269;
					case 34:
						goto IL_5CE;
					case 35:
						goto IL_409;
					case 36:
						this.NumberSufix = reader.ReadString(ClipboardData.b("⁲tᅶ⥸᩺ॼ୾", a_));
						num = 11;
						continue;
					case 37:
						this.\u171D = reader.ReadInt(ClipboardData.b("㽲ၴၶᡸ᡺ѼⱾ", a_));
						num = 18;
						continue;
					case 38:
						goto IL_370;
					case 39:
						this.\u171C = reader.ReadBoolean(ClipboardData.b("㽲ၴၶᡸ᡺Ѽ", a_));
						num = 42;
						continue;
					case 40:
						goto IL_4B9;
					case 41:
						goto IL_1D7;
					case 42:
						goto IL_29D;
					case 43:
						goto IL_370;
					}
					break;
					IL_1D7:
					num = 17;
					continue;
					IL_235:
					num = 23;
					continue;
					IL_269:
					num = 12;
					continue;
					IL_29D:
					num = 14;
					continue;
					IL_2FB:
					num = 4;
					continue;
					IL_370:
					num = 26;
					continue;
					IL_3A1:
					num = 20;
					continue;
					IL_3D5:
					num = 9;
					continue;
					IL_409:
					num = 5;
					continue;
					IL_4B9:
					num = 6;
					continue;
					IL_573:
					num = 0;
					continue;
					IL_5CE:
					num = 16;
					continue;
					IL_602:
					num = 1;
				}
			}
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x003D7328 File Offset: 0x003D6328
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 17;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("㹶᝸ὺ᡼ᅾ", a_), this.TextPosition);
				writer.WriteValue(ClipboardData.b("❶୸Ṻ᭼⽾ﮈ", a_), this.NumberPrefix);
				writer.WriteValue(ClipboardData.b("⑶౸ᵺ⵼Ṿ", a_), this.NumberSufix);
				writer.WriteValue(ClipboardData.b("㕶౸᝺ᅼ᩾펂ﶈﾌ", a_), this.BulletCharacter);
				writer.WriteValue(ClipboardData.b("❶ᡸེॼ᩾톄ﺆ麗", a_), this.PatternType);
				writer.WriteValue(ClipboardData.b("❶୸Ṻ୼⽾ﮈ", a_), this.UsePrevLevelPattern);
				writer.WriteValue(ClipboardData.b("⑶൸ོ᩺୾삀", a_), this.StartAt);
				ListStyle ownerListStyle = this.OwnerListStyle;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("㥶౸ᙺὼ᩾슂", a_), this.NumberAlignment);
							num = 3;
							continue;
						}
						break;
					case 1:
						num = 6;
						continue;
					case 2:
						writer.WriteValue(ClipboardData.b("㭶ᱸᱺᱼ᱾", a_), this.\u171C);
						writer.WriteValue(ClipboardData.b("㭶ᱸᱺᱼ᱾쪂歷", a_), this.\u171E);
						writer.WriteValue(ClipboardData.b("㭶ᱸᱺᱼ᱾킂", a_), this.\u171D);
						num = 4;
						continue;
					case 3:
						goto IL_118;
					case 4:
						return;
					case 5:
						if (ownerListStyle != null)
						{
							num = 1;
							continue;
						}
						goto IL_118;
					case 6:
						if (ownerListStyle.ListType == ListType.Numbered)
						{
							num = 0;
							continue;
						}
						goto IL_118;
					case 7:
						if (this.\u171C)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						return;
					}
					break;
					IL_118:
					writer.WriteValue(ClipboardData.b("㹶੸㝺᡼᡾", a_), this.IsLegalStyleNumbering);
					writer.WriteValue(ClipboardData.b("ㅶᙸ᝺ᅼၾ삂ﮈﮎ", a_), this.FollowCharacter);
					writer.WriteValue(ClipboardData.b("㥶ᙸ⥺᡼౾", a_), this.NoRestartByHigher);
					num = 7;
				}
			}
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x003D75A0 File Offset: 0x003D65A0
		protected override void InitXDLSHolder()
		{
			int a_ = 12;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("ɱᕳѵ᥷ᵹ๻ώꦃ", a_), this.ᜎ);
			base.XDLSHolder.AddElement(ClipboardData.b("ᅱᱳ᝵੷᭹ύ੽ꦃ", a_), this.\u170D);
		}

		// Token: 0x04003002 RID: 12290
		private new const float ᜀ = 26f;

		// Token: 0x04003003 RID: 12291
		private const int ᜁ = 64;

		// Token: 0x04003004 RID: 12292
		internal const string ᜂ = "\0";

		// Token: 0x04003005 RID: 12293
		internal const string ᜃ = "\u0001";

		// Token: 0x04003006 RID: 12294
		internal const string ᜄ = "\u0002";

		// Token: 0x04003007 RID: 12295
		internal const string ᜅ = "\u0003";

		// Token: 0x04003008 RID: 12296
		internal const string ᜆ = "\u0004";

		// Token: 0x04003009 RID: 12297
		internal const string ᜇ = "\u0005";

		// Token: 0x0400300A RID: 12298
		internal const string ᜈ = "\u0006";

		// Token: 0x0400300B RID: 12299
		internal const string ᜉ = "\a";

		// Token: 0x0400300C RID: 12300
		internal const string ᜊ = "\b";

		// Token: 0x0400300D RID: 12301
		private readonly string[] ᜋ;

		// Token: 0x0400300E RID: 12302
		private readonly string[] ᜌ;

		// Token: 0x0400300F RID: 12303
		private CharacterFormat \u170D;

		// Token: 0x04003010 RID: 12304
		private ParagraphFormat ᜎ;

		// Token: 0x04003011 RID: 12305
		private string ᜏ;

		// Token: 0x04003012 RID: 12306
		private string ᜐ;

		// Token: 0x04003013 RID: 12307
		private string ᜑ;

		// Token: 0x04003014 RID: 12308
		private string \u1712;

		// Token: 0x04003015 RID: 12309
		private string \u1713;

		// Token: 0x04003016 RID: 12310
		private long \u2609\u0095\u00A5\u00A8;

		// Token: 0x04003017 RID: 12311
		private bool \u1714;

		// Token: 0x04003018 RID: 12312
		private int \u1715;

		// Token: 0x04003019 RID: 12313
		private ListNumberAlignment \u1716;

		// Token: 0x0400301A RID: 12314
		private ListPatternType \u1717;

		// Token: 0x0400301B RID: 12315
		private bool \u1718;

		// Token: 0x0400301C RID: 12316
		private FollowCharacterType \u1719;

		// Token: 0x0400301D RID: 12317
		private bool \u171A;

		// Token: 0x0400301E RID: 12318
		private byte[] \u171B;

		// Token: 0x0400301F RID: 12319
		private bool \u171C;

		// Token: 0x04003020 RID: 12320
		private float \u25D9\u0083\u0094\u0094;

		// Token: 0x04003021 RID: 12321
		private int \u171D;

		// Token: 0x04003022 RID: 12322
		private int \u171E;

		// Token: 0x04003023 RID: 12323
		private string \u171F;

		// Token: 0x04003024 RID: 12324
		private bool ᜠ;

		// Token: 0x04003025 RID: 12325
		private DocPicture ᜡ;

		// Token: 0x04003026 RID: 12326
		private bool \u25D9\u009A\u00AB\u0091;

		// Token: 0x04003027 RID: 12327
		private short ᜢ;

		// Token: 0x04003028 RID: 12328
		private bool ᜣ;

		// Token: 0x04003029 RID: 12329
		[CompilerGenerated]
		private bool ᜤ;
	}
}
