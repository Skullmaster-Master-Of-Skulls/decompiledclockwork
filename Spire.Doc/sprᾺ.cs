using System;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;

// Token: 0x02000450 RID: 1104
internal class sprᾺ : IDocProperty, ICloneable
{
	// Token: 0x06003D2E RID: 15662 RVA: 0x0038F5D0 File Offset: 0x0038E5D0
	private sprᾺ()
	{
	}

	// Token: 0x06003D2F RID: 15663 RVA: 0x0038F5E4 File Offset: 0x0038E5E4
	public sprᾺ(string A_0, object A_1)
	{
		int a_ = 3;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᩨὪὬⅮၰṲၴ", a_));
		}
		if (A_0.Length == 0)
		{
			throw new ArgumentException(ClipboardData.b("ᩨὪὬⅮၰṲၴ坶呸孺๼୾ꦈﾐﲒ랖ﮘﺚ붜爵철펢톤\udea6螨", a_));
		}
		this.ᜃ = A_0;
		this.ᜀ(A_1);
	}

	// Token: 0x06003D30 RID: 15664 RVA: 0x0038F648 File Offset: 0x0038E648
	public sprᾺ(BuiltInProperty A_0, object A_1)
	{
		this.ᜂ = A_0;
		this.ᜄ = A_1;
	}

	// Token: 0x06003D31 RID: 15665 RVA: 0x0038F66C File Offset: 0x0038E66C
	public sprᾺ(spr\u2097 A_0, bool A_1)
	{
		int a_ = 2;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ṧ୩ṫݭᅯᱱs", a_));
		}
		this.ᜃ = A_0.ᜂ();
		if (this.ᜃ == null)
		{
			if (A_1)
			{
				this.ᜂ = (BuiltInProperty)A_0.ᜃ();
			}
			else
			{
				this.ᜂ = A_0.ᜃ() + BuiltInProperty.Category - 2;
			}
		}
		this.ᜄ = A_0.ᜀ();
		this.ᜅ = (PropertyType)A_0.ᜁ();
	}

	// Token: 0x06003D32 RID: 15666 RVA: 0x0038F6F8 File Offset: 0x0038E6F8
	public bool ᜎ()
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
		return this.ᜃ == null;
	}

	// Token: 0x06003D33 RID: 15667 RVA: 0x0038F73C File Offset: 0x0038E73C
	public BuiltInProperty ᜏ()
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
		return this.ᜂ;
	}

	// Token: 0x06003D34 RID: 15668 RVA: 0x0038F780 File Offset: 0x0038E780
	public void ᜀ(BuiltInProperty A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003D35 RID: 15669 RVA: 0x0038F7C4 File Offset: 0x0038E7C4
	public string \u1713()
	{
		if (this.ᜃ == null)
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
				return this.ᜂ.ToString();
			}
		}
		return this.ᜃ;
	}

	// Token: 0x06003D36 RID: 15670 RVA: 0x0038F820 File Offset: 0x0038E820
	public object ᜑ()
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
		return this.ᜄ;
	}

	// Token: 0x06003D37 RID: 15671 RVA: 0x0038F864 File Offset: 0x0038E864
	public void ᜀ(object A_0)
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
		this.ᜄ = A_0;
		this.ᜁ();
	}

	// Token: 0x06003D38 RID: 15672 RVA: 0x0038F8AC File Offset: 0x0038E8AC
	public bool ᜃ()
	{
		int a_ = 5;
		if (this.ᜅ != PropertyType.Bool)
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
				throw new InvalidCastException(ClipboardData.b("⡪౬Ů噰ݲ啴ᑶᙸᕺ୼᩾ꖄ놐杖랖ﮘ쒠슢쮤覦", a_));
			}
		}
		if (true)
		{
		}
		return Convert.ToBoolean(this.ᜄ);
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x0038F91C File Offset: 0x0038E91C
	public void ᜀ(bool A_0)
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
		this.ᜅ = PropertyType.Bool;
		this.ᜄ = A_0;
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x0038F96C File Offset: 0x0038E96C
	public int ᜋ()
	{
		int a_ = 17;
		if (this.ᜅ != PropertyType.Int)
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
				throw new InvalidCastException(ClipboardData.b("㑶ᡸᕺ婼୾ꆀﾈﾌﮎ놐ﮖﺚ붜캠莢첤즦\udda8캪쪬쪮쎰鶲", a_));
			}
		}
		if (true)
		{
		}
		return Convert.ToInt32(this.ᜄ);
	}

	// Token: 0x06003D3B RID: 15675 RVA: 0x0038F9DC File Offset: 0x0038E9DC
	public void ᜁ(int A_0)
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
		this.ᜅ = PropertyType.Int;
		this.ᜄ = A_0;
	}

	// Token: 0x06003D3C RID: 15676 RVA: 0x0038FA2C File Offset: 0x0038EA2C
	public int ᜇ()
	{
		int a_ = 19;
		if (true)
		{
		}
		if (this.ᜅ != PropertyType.Int32)
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
				throw new InvalidCastException(ClipboardData.b("㩸᩺፼塾ꎂﶊﶎ뎒뾞햠첢薤캦잨\udfaa좬좮풰솲鮴", a_));
			}
		}
		return Convert.ToInt32(this.ᜄ);
	}

	// Token: 0x06003D3D RID: 15677 RVA: 0x0038FA9C File Offset: 0x0038EA9C
	public void ᜀ(int A_0)
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
		this.ᜅ = PropertyType.Int32;
		this.ᜄ = A_0;
	}

	// Token: 0x06003D3E RID: 15678 RVA: 0x0038FAEC File Offset: 0x0038EAEC
	public double ᜊ()
	{
		int a_ = 0;
		if (this.ᜅ != PropertyType.Double)
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
				throw new InvalidCastException(ClipboardData.b("╥१ѩ䭫ᩭ偯ᅱ᭳ᡵ๷ό๻੽ꁿﶇ겋揄ﾏ늑ﶓﾙﮛﮝ튟財", a_));
			}
		}
		if (true)
		{
		}
		return Convert.ToDouble(this.ᜄ);
	}

	// Token: 0x06003D3F RID: 15679 RVA: 0x0038FB5C File Offset: 0x0038EB5C
	public void ᜀ(double A_0)
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
		this.ᜅ = PropertyType.Double;
		this.ᜄ = A_0;
	}

	// Token: 0x06003D40 RID: 15680 RVA: 0x0038FBAC File Offset: 0x0038EBAC
	public string \u1712()
	{
		int a_ = 6;
		if (this.ᜅ != PropertyType.String)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				throw new InvalidCastException(ClipboardData.b("⽫཭ṯ啱s噵᭷ᕹቻࡽꚅﺇﮍ늑秊뢗즟첡쎣袥", a_));
			}
		}
		return Convert.ToString(this.ᜄ);
	}

	// Token: 0x06003D41 RID: 15681 RVA: 0x0038FC1C File Offset: 0x0038EC1C
	public void ᜀ(string A_0)
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
		this.ᜅ = PropertyType.String;
		this.ᜄ = A_0;
	}

	// Token: 0x06003D42 RID: 15682 RVA: 0x0038FC68 File Offset: 0x0038EC68
	public DateTime ᜄ()
	{
		int a_ = 15;
		if (this.ᜅ != PropertyType.DateTime)
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
				throw new InvalidCastException(ClipboardData.b("㙴ᙶ᝸屺ॼ彾力歷꾎璉ﲘ뮚膠쒤펦첨ﾪ쒬슮풰鶲", a_));
			}
		}
		return Convert.ToDateTime(this.ᜄ);
	}

	// Token: 0x06003D43 RID: 15683 RVA: 0x0038FCD8 File Offset: 0x0038ECD8
	public void ᜀ(DateTime A_0)
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
		this.ᜅ = PropertyType.DateTime;
		this.ᜄ = A_0;
	}

	// Token: 0x06003D44 RID: 15684 RVA: 0x0038FD28 File Offset: 0x0038ED28
	public TimeSpan \u1715()
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
		return new TimeSpan(this.ᜄ().AddYears(-1600).Ticks);
	}

	// Token: 0x06003D45 RID: 15685 RVA: 0x0038FD84 File Offset: 0x0038ED84
	public void ᜀ(TimeSpan A_0)
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
		DateTime a_ = new DateTime(A_0.Ticks);
		a_ = a_.AddYears(1600);
		this.ᜀ(a_);
	}

	// Token: 0x06003D46 RID: 15686 RVA: 0x0038FDE4 File Offset: 0x0038EDE4
	public byte[] ᜅ()
	{
		int a_ = 14;
		if (this.ᜅ != PropertyType.Blob)
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
				throw new InvalidCastException(ClipboardData.b("㝳᝵ᙷ嵹ࡻ幽꺍ﶗ몙肟좣즥쪧蒩", a_));
			}
		}
		return (byte[])this.ᜄ;
	}

	// Token: 0x06003D47 RID: 15687 RVA: 0x0038FE54 File Offset: 0x0038EE54
	public void ᜀ(byte[] A_0)
	{
		int a_ = 16;
		if (A_0 != null)
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
				this.ᜅ = PropertyType.Blob;
				this.ᜄ = A_0;
				return;
			}
		}
		throw new ArgumentNullException(ClipboardData.b("u᥷ᙹॻ᭽", a_));
	}

	// Token: 0x06003D48 RID: 15688 RVA: 0x0038FEC0 File Offset: 0x0038EEC0
	public ClipboardData ᜈ()
	{
		int a_ = 4;
		if (true)
		{
		}
		if (this.ᜅ != PropertyType.ClipboardData)
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
				throw new InvalidCastException(ClipboardData.b("⥩൫m坯ٱ味ᕵ᝷ᑹ੻᭽ꒃ曆낏ﮓ뚕\udb97슟춡얣풥첧춫\udaad톯鲱", a_));
			}
		}
		return (ClipboardData)this.ᜄ;
	}

	// Token: 0x06003D49 RID: 15689 RVA: 0x0038FF30 File Offset: 0x0038EF30
	public void ᜀ(ClipboardData A_0)
	{
		int a_ = 13;
		if (A_0 != null)
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
				this.ᜅ = PropertyType.ClipboardData;
				this.ᜄ = A_0;
				return;
			}
		}
		throw new ArgumentNullException(ClipboardData.b("ղᑴ᭶౸Ṻ", a_));
	}

	// Token: 0x06003D4A RID: 15690 RVA: 0x0038FF9C File Offset: 0x0038EF9C
	public string[] ᜆ()
	{
		int a_ = 19;
		if (this.ᜅ != PropertyType.StringArray)
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
				if (true)
				{
				}
				throw new InvalidCastException(ClipboardData.b("㩸᩺፼塾ꎂﶊﶎ뎒뾞햠첢薤욦잨讪첬\uddae쎰튲체鞶횸\uddba鶼첾뗀뇂계꧆껈룊", a_));
			}
		}
		return (string[])this.ᜄ;
	}

	// Token: 0x06003D4B RID: 15691 RVA: 0x00390010 File Offset: 0x0038F010
	public void ᜀ(string[] A_0)
	{
		int a_ = 17;
		if (A_0 != null)
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
				if (true)
				{
				}
				this.ᜅ = PropertyType.StringArray;
				this.ᜄ = A_0;
				return;
			}
		}
		throw new ArgumentNullException(ClipboardData.b("Ŷᡸ᝺ࡼ᩾", a_));
	}

	// Token: 0x06003D4C RID: 15692 RVA: 0x00390080 File Offset: 0x0038F080
	public object[] \u170D()
	{
		int a_ = 16;
		if (this.ᜅ == PropertyType.ObjectArray)
		{
			if (true)
			{
			}
		}
		else
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
				throw new InvalidCastException(ClipboardData.b("㕵᥷ᑹ孻੽ꁿﺇﺋ揄낏歹ﾙ벛쾟芡얣좥袧쮩\udeab\udcad톯쮱钳\ud9b5\udeb7骹쾻쪽늿ꯁ꫃ꇅ믇", a_));
			}
		}
		return (object[])this.ᜄ;
	}

	// Token: 0x06003D4D RID: 15693 RVA: 0x003900F4 File Offset: 0x0038F0F4
	public void ᜀ(object[] A_0)
	{
		int a_ = 6;
		if (A_0 != null)
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
				this.ᜅ = PropertyType.ObjectArray;
				this.ᜄ = A_0;
				return;
			}
		}
		throw new ArgumentNullException(ClipboardData.b("ᩫ཭ᱯݱᅳ", a_));
	}

	// Token: 0x06003D4E RID: 15694 RVA: 0x00390164 File Offset: 0x0038F164
	public PropertyType ᜌ()
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
		return this.ᜅ;
	}

	// Token: 0x06003D4F RID: 15695 RVA: 0x003901A8 File Offset: 0x0038F1A8
	public void ᜀ(PropertyType A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06003D50 RID: 15696 RVA: 0x003901EC File Offset: 0x0038F1EC
	public string \u1714()
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
		return this.ᜆ;
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x00390230 File Offset: 0x0038F230
	public void ᜁ(string A_0)
	{
		int a_ = 1;
		if (!this.ᜎ())
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
				this.ᜆ = A_0;
				this.ᜇ = true;
				return;
			}
		}
		throw new InvalidOperationException(ClipboardData.b("㍦ŨɪṬ佮ṰͲၴնᡸེᑼၾꎂ겊歷꾎떔ﲘﮜ펠캢삤쎦覨쒪쎬辮펰욲\udcb4\udbb6춸隺풼톾돂럄꣆마껊뿌믎꣐﷒", a_));
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x003902A0 File Offset: 0x0038F2A0
	public bool ᜐ()
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
		return this.ᜇ;
	}

	// Token: 0x06003D53 RID: 15699 RVA: 0x003902E4 File Offset: 0x0038F2E4
	public void ᜁ(bool A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x06003D54 RID: 15700 RVA: 0x00390328 File Offset: 0x0038F328
	public string ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x06003D55 RID: 15701 RVA: 0x0039036C File Offset: 0x0038F36C
	public bool ᜀ(spr\u2097 A_0, int A_1)
	{
		int a_ = 3;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_97;
			case 1:
				goto IL_C1;
			case 3:
				goto IL_75;
			case 4:
				if (this.ᜄ == null)
				{
					goto IL_D6;
				}
				num = 5;
				continue;
			case 5:
				if (this.ᜎ())
				{
					num = 6;
					continue;
				}
				A_0.ᜀ(A_1);
				num = 1;
				continue;
			case 6:
			{
				bool flag;
				int a_2 = sprᾺ.ᜀ(this.ᜂ, out flag);
				A_0.ᜀ(a_2);
				num = 0;
				continue;
			}
			case 7:
				return false;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 3;
				continue;
			}
			IL_D6:
			num = 7;
		}
		IL_75:
		throw new ArgumentNullException(ClipboardData.b("Ὠ੪ὬٮၰᵲŴ", a_));
		IL_97:
		IL_C1:
		return A_0.ᜀ(this.ᜄ, this.ᜅ);
	}

	// Token: 0x06003D56 RID: 15702 RVA: 0x00390494 File Offset: 0x0038F494
	public static int ᜀ(BuiltInProperty A_0, out bool A_1)
	{
		int num;
		for (;;)
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
				num = (int)A_0;
				break;
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num -= 998;
					A_1 = false;
					num2 = 3;
					continue;
				case 1:
					return num;
				case 2:
					if (true)
					{
					}
					if (num >= 1000)
					{
						num2 = 0;
						continue;
					}
					A_1 = true;
					num2 = 1;
					continue;
				case 3:
					return num;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x06003D57 RID: 15703 RVA: 0x00390528 File Offset: 0x0038F528
	private void ᜁ()
	{
		if (true)
		{
		}
		int num = 18;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_144;
			case 1:
				if (this.ᜄ is string[])
				{
					goto IL_99;
				}
				num = 2;
				continue;
			case 2:
				if (this.ᜄ is byte[])
				{
					num = 13;
					continue;
				}
				num = 10;
				continue;
			case 3:
				goto IL_7C;
			case 4:
				if (this.ᜄ is double)
				{
					num = 0;
					continue;
				}
				num = 9;
				continue;
			case 5:
				this.ᜅ = PropertyType.ClipboardData;
				num = 6;
				continue;
			case 6:
				goto IL_1F6;
			case 7:
				goto IL_A4;
			case 8:
				if (this.ᜄ is object[])
				{
					num = 14;
					continue;
				}
				num = 1;
				continue;
			case 9:
				if (this.ᜄ is int)
				{
					num = 11;
					continue;
				}
				num = 17;
				continue;
			case 10:
				if (this.ᜄ is ClipboardData)
				{
					num = 5;
					continue;
				}
				return;
			case 11:
				goto IL_113;
			case 12:
				goto IL_21E;
			case 13:
				goto IL_1DE;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
					goto IL_E2;
				}
				break;
			case 15:
				if (this.ᜄ is DateTime)
				{
					num = 16;
					continue;
				}
				num = 8;
				continue;
			case 16:
				goto IL_1AD;
			case 17:
				if (this.ᜄ is bool)
				{
					num = 12;
					continue;
				}
				num = 15;
				continue;
			}
			if (this.ᜄ is string)
			{
				num = 3;
				continue;
			}
			num = 4;
			continue;
			IL_99:
			num = 7;
		}
		IL_7C:
		this.ᜅ = PropertyType.String;
		return;
		IL_A4:
		this.ᜅ = PropertyType.StringArray;
		return;
		IL_E2:
		if (false)
		{
		}
		this.ᜅ = PropertyType.ObjectArray;
		return;
		IL_113:
		this.ᜅ = PropertyType.Int;
		return;
		IL_144:
		this.ᜅ = PropertyType.Double;
		return;
		IL_1AD:
		this.ᜅ = PropertyType.DateTime;
		return;
		IL_1DE:
		this.ᜅ = PropertyType.Blob;
		return;
		IL_1F6:
		return;
		IL_21E:
		this.ᜅ = PropertyType.Bool;
	}

	// Token: 0x06003D58 RID: 15704 RVA: 0x00390784 File Offset: 0x0038F784
	public void ᜀ(spr\u2097 A_0)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				goto IL_90;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_90;
				default:
					goto IL_5C;
				}
				break;
			case 4:
				goto IL_86;
			case 5:
				if (A_0.ᜁ() != VarEnum.VT_LPWSTR)
				{
					num = 4;
					continue;
				}
				goto IL_CF;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_90:
			if (A_0.ᜁ() == VarEnum.VT_LPSTR)
			{
				goto IL_CF;
			}
			num = 0;
		}
		IL_5C:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᡭᅯqᵳ᝵ᙷ๹", a_));
		IL_86:
		throw new ArgumentOutOfRangeException(ClipboardData.b("≭᥯ᱱέ╵᝷ཹ๻ᵽ", a_));
		IL_CF:
		this.ᜁ(A_0.ᜀ().ToString());
	}

	// Token: 0x06003D59 RID: 15705 RVA: 0x00390874 File Offset: 0x0038F874
	[CLSCompliant(false)]
	public void ᜀ(sprᡮ A_0, sprᾁ A_1, int A_2)
	{
		int a_ = 8;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.ᜎ())
				{
					num = 9;
					continue;
				}
				goto IL_105;
			case 1:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				this.ᜀ(A_1, A_2);
				A_1.ᜀ(A_0);
				goto IL_7F;
			case 2:
				A_1.ᜀ(A_2 + (PIDSI)16777216);
				A_1.ᜁ(this.ᜆ);
				A_1.ᜀ(A_0);
				num = 6;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7F;
				default:
					if (false)
					{
					}
					if (this.ᜐ())
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
			case 4:
				goto IL_105;
			case 6:
				return;
			case 7:
				goto IL_B5;
			case 8:
				goto IL_4C;
			case 9:
			{
				uint num2 = (uint)A_2;
				A_0.ᜁ(1U, ref num2, ref this.ᜃ);
				num = 4;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 1;
			continue;
			IL_7F:
			num = 0;
			continue;
			IL_105:
			num = 3;
		}
		IL_4C:
		throw new ArgumentNullException(ClipboardData.b("ᵭѯᵱٳ♵੷ᕹ౻", a_));
		IL_B5:
		throw new ArgumentNullException(ClipboardData.b("ᡭᅯqᵳ᝵ᙷ๹", a_));
	}

	// Token: 0x06003D5A RID: 15706 RVA: 0x003909E4 File Offset: 0x0038F9E4
	public object ᜉ()
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
		sprᾺ sprᾺ = (sprᾺ)base.MemberwiseClone();
		sprᾺ.ᜀ();
		return sprᾺ;
	}

	// Token: 0x06003D5B RID: 15707 RVA: 0x00390A34 File Offset: 0x0038FA34
	private void ᜀ()
	{
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					PropertyType propertyType;
					if (propertyType != PropertyType.StringArray)
					{
						num = 7;
						continue;
					}
					goto IL_A2;
				}
				case 1:
					return;
				case 3:
				{
					PropertyType propertyType;
					if (propertyType != PropertyType.ObjectArray)
					{
						num = 6;
						continue;
					}
					goto IL_F4;
				}
				case 4:
					num = 3;
					continue;
				case 5:
				{
					PropertyType propertyType;
					if (propertyType == PropertyType.Blob)
					{
						goto IL_B5;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 6:
					num = 0;
					continue;
				case 7:
					return;
				}
				if (this.ᜄ == null)
				{
					num = 1;
				}
				else
				{
					PropertyType propertyType = this.ᜅ;
					num = 5;
				}
			}
		}
		return;
		IL_A2:
		this.ᜄ = sprᰓ.ᜀ(this.ᜆ());
		return;
		IL_B5:
		this.ᜄ = sprᰓ.ᜀ(this.ᜅ());
		return;
		IL_F4:
		this.ᜄ = sprᰓ.ᜀ(this.\u170D());
	}

	// Token: 0x04002C29 RID: 11305
	private const int ᜀ = 1000;

	// Token: 0x04002C2A RID: 11306
	public const int ᜁ = 1600;

	// Token: 0x04002C2B RID: 11307
	private BuiltInProperty ᜂ;

	// Token: 0x04002C2C RID: 11308
	private string ᜃ;

	// Token: 0x04002C2D RID: 11309
	private object ᜄ;

	// Token: 0x04002C2E RID: 11310
	private PropertyType ᜅ;

	// Token: 0x04002C2F RID: 11311
	private string ᜆ;

	// Token: 0x04002C30 RID: 11312
	private bool ᜇ;
}
