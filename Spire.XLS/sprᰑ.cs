using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.CompoundFile.XLS.Native;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x02000472 RID: 1138
internal class sprᰑ : IOleObject
{
	// Token: 0x06004595 RID: 17813 RVA: 0x002A71D0 File Offset: 0x002A61D0
	public IXLSRange ᜑ()
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
		return this.\u1718[this.ᜑ.TopRow, this.ᜑ.LeftColumn];
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x002A722C File Offset: 0x002A622C
	public void ᜀ(IXLSRange A_0)
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
		this.ᜑ.TopRow = A_0.Row;
		this.ᜑ.LeftColumn = A_0.Column;
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x002A728C File Offset: 0x002A628C
	public Size \u1712()
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
		return new Size(this.ᜑ.Width, this.ᜑ.Height);
	}

	// Token: 0x06004598 RID: 17816 RVA: 0x002A72E4 File Offset: 0x002A62E4
	public void ᜀ(Size A_0)
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
		this.ᜑ.Width = A_0.Width;
		this.ᜑ.Height = A_0.Height;
	}

	// Token: 0x06004599 RID: 17817 RVA: 0x002A7344 File Offset: 0x002A6344
	public Image ᜐ()
	{
		IPictureShape pictureShape;
		for (;;)
		{
			pictureShape = this.ᜂ();
			if (pictureShape == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_24;
			}
		}
		return null;
		IL_24:
		if (true)
		{
		}
		if (false)
		{
		}
		return pictureShape.Picture;
	}

	// Token: 0x0600459A RID: 17818 RVA: 0x002A7394 File Offset: 0x002A6394
	public void ᜀ(Image A_0)
	{
		int a_ = 17;
		while (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			throw new NotImplementedException();
		}
		if (true)
		{
		}
		throw new Exception(RecordTableEnumerator.b("ๆ⑈⩊⩌⩎", a_));
	}

	// Token: 0x0600459B RID: 17819 RVA: 0x002A73F8 File Offset: 0x002A63F8
	public bool ᜋ()
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
		return this.ᜊ;
	}

	// Token: 0x0600459C RID: 17820 RVA: 0x002A743C File Offset: 0x002A643C
	public void ᜀ(bool A_0)
	{
		while (!A_0)
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
				this.ᜀ(DVAspect.DVASPECT_CONTENT);
				this.ᜊ = A_0;
				return;
			}
		}
		this.ᜀ(DVAspect.DVASPECT_ICON);
		this.ᜊ = A_0;
	}

	// Token: 0x0600459D RID: 17821 RVA: 0x002A749C File Offset: 0x002A649C
	public OleLinkType ᜉ()
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

	// Token: 0x0600459E RID: 17822 RVA: 0x002A74E0 File Offset: 0x002A64E0
	public void ᜀ(OleLinkType A_0)
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
		this.\u1716 = A_0;
	}

	// Token: 0x0600459F RID: 17823 RVA: 0x002A7524 File Offset: 0x002A6524
	public bool ᜊ()
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
		return this.\u1717;
	}

	// Token: 0x060045A0 RID: 17824 RVA: 0x002A7568 File Offset: 0x002A6568
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
		this.\u1717 = A_0;
	}

	// Token: 0x060045A1 RID: 17825 RVA: 0x002A75AC File Offset: 0x002A65AC
	public XlsWorksheet \u1713()
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
		return this.\u1718;
	}

	// Token: 0x060045A2 RID: 17826 RVA: 0x002A75F0 File Offset: 0x002A65F0
	public byte[] ᜎ()
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
		return this.ᜅ;
	}

	// Token: 0x060045A3 RID: 17827 RVA: 0x002A7634 File Offset: 0x002A6634
	public void ᜀ(byte[] A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x060045A4 RID: 17828 RVA: 0x002A7678 File Offset: 0x002A6678
	public bool ᜈ()
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
		return this.\u1715;
	}

	// Token: 0x060045A5 RID: 17829 RVA: 0x002A76BC File Offset: 0x002A66BC
	public void ᜂ(bool A_0)
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
		this.\u1715 = A_0;
	}

	// Token: 0x060045A6 RID: 17830 RVA: 0x002A7700 File Offset: 0x002A6700
	public OleObjectType ᜏ()
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
		return this.\u1714;
	}

	// Token: 0x060045A7 RID: 17831 RVA: 0x002A7744 File Offset: 0x002A6744
	public void ᜀ(OleObjectType A_0)
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
		this.\u1714 = A_0;
	}

	// Token: 0x060045A8 RID: 17832 RVA: 0x002A7788 File Offset: 0x002A6788
	public Dictionary<string, int> ᜆ()
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

	// Token: 0x060045A9 RID: 17833 RVA: 0x002A77CC File Offset: 0x002A67CC
	public Stream ᜃ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.ᜌ = this.ᜀ();
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_58;
					}
				}
				IL_58:
				if (false)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_70;
			}
			if (this.ᜌ != null)
			{
				break;
			}
			num = 0;
		}
		IL_70:
		return this.ᜌ;
	}

	// Token: 0x060045AA RID: 17834 RVA: 0x002A7854 File Offset: 0x002A6854
	public void ᜀ(Stream A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x060045AB RID: 17835 RVA: 0x002A7898 File Offset: 0x002A6898
	public string ᜁ()
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
		return this.ᜏ;
	}

	// Token: 0x060045AC RID: 17836 RVA: 0x002A78DC File Offset: 0x002A68DC
	public void ᜆ(string A_0)
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
		this.ᜏ = A_0;
	}

	// Token: 0x060045AD RID: 17837 RVA: 0x002A7920 File Offset: 0x002A6920
	public string ᜇ()
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
		return this.ᜋ;
	}

	// Token: 0x060045AE RID: 17838 RVA: 0x002A7964 File Offset: 0x002A6964
	public void ᜈ(string A_0)
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
		this.ᜋ = A_0;
	}

	// Token: 0x060045AF RID: 17839 RVA: 0x002A79A8 File Offset: 0x002A69A8
	public DVAspect \u170D()
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
		return this.ᜐ;
	}

	// Token: 0x060045B0 RID: 17840 RVA: 0x002A79EC File Offset: 0x002A69EC
	public void ᜀ(DVAspect A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x060045B1 RID: 17841 RVA: 0x002A7A30 File Offset: 0x002A6A30
	public int ᜄ()
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
		return this.ᜑ.ShapeId;
	}

	// Token: 0x060045B2 RID: 17842 RVA: 0x002A7A78 File Offset: 0x002A6A78
	public void ᜀ(int A_0)
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
		this.ᜑ = this.\u1718.InnerShapes.ᜀ(A_0);
	}

	// Token: 0x060045B3 RID: 17843 RVA: 0x002A7ACC File Offset: 0x002A6ACC
	public string ᜌ()
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
		return this.\u1712;
	}

	// Token: 0x060045B4 RID: 17844 RVA: 0x002A7B10 File Offset: 0x002A6B10
	public void ᜂ(string A_0)
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
		this.\u1712 = A_0;
	}

	// Token: 0x060045B5 RID: 17845 RVA: 0x002A7B54 File Offset: 0x002A6B54
	public IPictureShape ᜂ()
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
		return this.ᜑ as IPictureShape;
	}

	// Token: 0x060045B6 RID: 17846 RVA: 0x002A7B9C File Offset: 0x002A6B9C
	public void ᜀ(IPictureShape A_0)
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
		this.ᜑ = (XlsShape)A_0;
		this.ᜑ.VmlShape = true;
	}

	// Token: 0x060045B7 RID: 17847 RVA: 0x002A7BF0 File Offset: 0x002A6BF0
	public string ᜅ()
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
		return this.\u1719;
	}

	// Token: 0x060045B8 RID: 17848 RVA: 0x002A7C34 File Offset: 0x002A6C34
	public void ᜄ(string A_0)
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
		this.\u1719 = A_0;
	}

	// Token: 0x060045B9 RID: 17849 RVA: 0x002A7C78 File Offset: 0x002A6C78
	public string \u1714()
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
		return this.\u171A;
	}

	// Token: 0x060045BA RID: 17850 RVA: 0x002A7CBC File Offset: 0x002A6CBC
	public void ᜅ(string A_0)
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
		this.\u171A = A_0;
	}

	// Token: 0x060045BB RID: 17851 RVA: 0x002A7D00 File Offset: 0x002A6D00
	public sprᰑ(XlsWorksheet A_0)
	{
		int a_ = 19;
		this.ᜊ = true;
		this.ᜋ = string.Empty;
		this.\u1713 = string.Empty;
		this.\u1719 = RecordTableEnumerator.b("⡈㭊㵌⍎㡐げ㑔⍖じ㑚㍜灞ᝠൢŤ䥦٨᭪࡬Ů॰ṲᥴᅶᙸॺၼṾꢄ杖햠趢쪤쮦첨쾬얮풰킲솴", a_);
		this.\u171A = RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌뻐뿒냔飖믘뇚룜볞闠", a_);
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException();
		}
		this.\u1718 = A_0;
	}

	// Token: 0x060045BC RID: 17852 RVA: 0x002A7D74 File Offset: 0x002A6D74
	public sprᰑ(string A_0, Image A_1)
	{
		int a_ = 4;
		this.ᜊ = true;
		this.ᜋ = string.Empty;
		this.\u1713 = string.Empty;
		this.\u1719 = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵᝷ᱹ᩻᝽ﾉﺏ몓秊ﾙ펛ﲝ쪟잡잣튥", a_);
		this.\u171A = RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ᅽ캉ﾑ떙꺛꺝邟钡讣풥춧용춫\udaad\ud9af\uddb1\udab3억킷펹첻춽귁ꣃꏅ蟇꣉ꛋꯍ돏ꛑ", a_);
		base..ctor();
		if (!File.Exists(A_0))
		{
			throw new Exception(RecordTableEnumerator.b("氹崻刽㔿❁㝃晅ᭇ≉⍋㭍㱏㙑瑓ᑕ㵗穙ⱛ㽝፟ᅡţɥ", a_));
		}
		this.ᜁ(A_0);
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("猹儻弽✿❁", a_));
		}
		this.ᜀ(A_1);
		this.ᜈ(A_0);
		this.ᜀ(true);
		this.ᜃ(A_0);
	}

	// Token: 0x060045BD RID: 17853 RVA: 0x002A7E30 File Offset: 0x002A6E30
	public sprᰑ(string A_0, Image A_1, OleLinkType A_2)
	{
		int a_ = 2;
		this.ᜊ = true;
		this.ᜋ = string.Empty;
		this.\u1713 = string.Empty;
		this.\u1719 = RecordTableEnumerator.b("夷䨹䰻刽⤿⅁╃㉅ⅇ╉≋慍♏㱑こ硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű女᥵ṷᱹᕻᵽﶇ벑ﮓ歹ﶗ햙ﺛ얟송킣", a_);
		this.\u171A = RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻醽꾿껁ꇃ觅꫇ꃉ꧋귍꓏", a_);
		base..ctor();
		if (!File.Exists(A_0))
		{
			throw new Exception(RecordTableEnumerator.b("渷嬹倻䬽┿ㅁ摃ᕅ⁇╉㥋≍㑏牑ᙓ㍕硗⩙㵛ⵝ፟ݡc", a_));
		}
		this.ᜁ(A_0);
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("焷圹崻夽┿", a_));
		}
		this.ᜀ(A_1);
		this.ᜈ(Path.GetFullPath(A_0));
		this.ᜀ(A_2);
		this.ᜀ(true);
		this.ᜃ(A_0);
	}

	// Token: 0x060045BE RID: 17854 RVA: 0x002A7EF8 File Offset: 0x002A6EF8
	public sprᰑ(string A_0, IPictureShape A_1, OleLinkType A_2)
	{
		int a_ = 14;
		this.ᜊ = true;
		this.ᜋ = string.Empty;
		this.\u1713 = string.Empty;
		this.\u1719 = RecordTableEnumerator.b("╃㙅㡇♉╋ⵍㅏ♑㵓㥕㙗留⩛そџ䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ굿ﾏﮕﶗ낝쾟캡솣쪧삩즫춭쒯", a_);
		this.\u171A = RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ킓秊ﮗﮝ캟횡讣钥颧骩骫膭슯ힱ\ud8b3ힵ첷펹펻킽뎿꫁귃뛅믇ꏋꋍ뗏鷑뛓볕뷗맙꣛", a_);
		base..ctor();
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅⥇㩉⥋", a_));
		}
		this.\u1718 = ((A_1 as XlsShape).Worksheet as XlsWorksheet);
		if (File.Exists(A_0))
		{
			this.ᜁ(A_0);
			this.ᜀ(A_1);
			this.ᜈ(Path.GetFullPath(A_0));
			this.ᜀ(A_2);
			this.ᜀ(true);
			this.ᜃ(A_0);
			return;
		}
		throw new ArgumentException(RecordTableEnumerator.b("Ƀ⽅⑇⽉汋⁍㽏♑瑓さ㝗⽙㉛㩝也", a_));
	}

	// Token: 0x060045BF RID: 17855 RVA: 0x002A7FD8 File Offset: 0x002A6FD8
	public static sprᰑ ᜀ(string A_0, Image A_1)
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
		return new sprᰑ(A_0, A_1);
	}

	// Token: 0x060045C0 RID: 17856 RVA: 0x002A801C File Offset: 0x002A701C
	public static sprᰑ ᜀ(Stream A_0, Image A_1, string A_2)
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
		return new sprᰑ(A_0, A_1, A_2);
	}

	// Token: 0x060045C1 RID: 17857 RVA: 0x002A8064 File Offset: 0x002A7064
	public sprᰑ(Stream A_0, Image A_1, string A_2)
	{
		int a_ = 16;
		this.ᜊ = true;
		this.ᜋ = string.Empty;
		this.\u1713 = string.Empty;
		this.\u1719 = RecordTableEnumerator.b("❅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛⡝๟١䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽꾁﶑ﾙ躟춡좣쎥좩욫쮭펯욱", a_);
		this.\u171A = RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막ꇍ볏럑鯓듕닗뿙뿛ꫝ", a_);
		base..ctor();
		if (!A_0.CanSeek || !A_0.CanRead)
		{
			throw new Exception(RecordTableEnumerator.b("ᕅ㱇㡉⥋⽍㵏", a_));
		}
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("ཅ╇⭉⭋⭍", a_));
		}
		for (;;)
		{
			this.ᜁ(true);
			this.ᜀ(A_1);
			byte[] array = new byte[A_0.Length];
			A_0.Read(array, 0, array.Length);
			A_0.Close();
			this.ᜋ = this.ᜀ(array, A_2);
			using (FileStream fileStream = new FileStream(this.ᜋ, FileMode.Open, FileAccess.Read))
			{
				this.ᜆ = new byte[fileStream.Length];
				fileStream.Read(this.ᜆ, 0, this.ᜆ.Length);
				break;
			}
		}
		this.ᜆ(spr\u20E9.ᜁ());
		this.ᜀ(OleLinkType.Embed);
		this.ᜀ(true);
		this.ᜁ(this.ᜋ, this.ᜁ());
	}

	// Token: 0x060045C2 RID: 17858 RVA: 0x002A81D0 File Offset: 0x002A71D0
	internal string ᜀ(byte[] A_0, string A_1)
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
		string text = Path.ChangeExtension(Path.GetTempPath() + Guid.NewGuid().ToString(), A_1);
		text = text.Replace('\\', '/');
		FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite);
		fileStream.Write(A_0, 0, A_0.Length);
		fileStream.Close();
		return text;
	}

	// Token: 0x060045C3 RID: 17859 RVA: 0x002A8258 File Offset: 0x002A7258
	internal string ᜃ(string A_0)
	{
		int a_ = 10;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				for (;;)
				{
					if (true)
					{
					}
					FileStream fileStream = new FileStream(A_0, FileMode.Open);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_7F;
					}
				}
				IL_7F:
				if (false)
				{
				}
				num = 3;
				continue;
			case 2:
				goto IL_54;
			case 3:
				try
				{
					FileStream fileStream;
					this.ᜆ = new byte[fileStream.Length];
					fileStream.Read(this.ᜆ, 0, this.ᜆ.Length);
					goto IL_45;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						FileStream fileStream;
						switch (num)
						{
						case 0:
							goto IL_107;
						case 2:
							((IDisposable)fileStream).Dispose();
							num = 0;
							continue;
						}
						if (fileStream == null)
						{
							break;
						}
						num = 2;
					}
					IL_107:;
				}
				return A_0;
				IL_45:
				this.ᜋ = A_0;
				num = 2;
				continue;
			}
			if (!File.Exists(A_0))
			{
				goto IL_8F;
			}
			num = 1;
		}
		IL_54:
		return A_0;
		IL_8F:
		throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅็⍉⁋⭍灏㙑㭓㍕⭗穙㉛ㅝᑟ䉡ţṥŧᥩᡫᵭ偯᭱ᩳ噵౷ቹ᥻幽ꒇꪉﲋ뢗ﾙ햟킡솣蚥\udca7슩즫躭햯쪱\uddb3억첷\udfb9튻\uddbdꖿꯃꃅ뻉꓋ꯍ듑뷓뫕뷗", a_));
	}

	// Token: 0x060045C4 RID: 17860 RVA: 0x002A8380 File Offset: 0x002A7380
	public void ᜇ(string A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			this.ᜁ(A_0);
			FileStream fileStream = new FileStream(A_0, FileMode.CreateNew, FileAccess.Write, FileShare.None);
			try
			{
				if (true)
				{
				}
				this.ᜃ().Position = 0L;
				byte[] array = new byte[this.ᜃ().Length - 1536L];
				this.ᜃ().Read(array, 1537, array.Length);
				fileStream.Write(array, 0, array.Length);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)fileStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_C0;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 0;
				}
				IL_C0:;
			}
			break;
		}
		}
	}

	// Token: 0x060045C5 RID: 17861 RVA: 0x002A8460 File Offset: 0x002A7460
	private static void ᜀ(Stream A_0, Stream A_1)
	{
		for (;;)
		{
			byte[] buffer = new byte[2000];
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_2D;
				case 1:
				{
					int count;
					if ((count = A_0.Read(buffer, 0, 2000)) <= 0)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					A_1.Write(buffer, 0, count);
					num = 0;
					continue;
				}
				case 2:
					goto IL_2D;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D;
					default:
						goto IL_77;
					}
					break;
				}
				break;
				IL_2D:
				num = 1;
			}
		}
		IL_77:
		if (false)
		{
		}
		A_1.Flush();
	}

	// Token: 0x060045C6 RID: 17862 RVA: 0x002A8508 File Offset: 0x002A7508
	private void ᜁ(string A_0)
	{
		int a_ = 19;
		for (;;)
		{
			bool flag = false;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (A_0.Length >= 252)
					{
						num = 5;
						continue;
					}
					string directoryName = Path.GetDirectoryName(A_0);
					num = 7;
					continue;
				}
				case 1:
					flag = true;
					num = 3;
					continue;
				case 2:
					goto IL_55;
				case 3:
					goto IL_55;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8F;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (flag)
						{
							num = 6;
							continue;
						}
						return;
					}
					break;
				case 5:
					flag = true;
					num = 2;
					continue;
				case 6:
					goto IL_8F;
				case 7:
				{
					string directoryName;
					if (directoryName.Length >= 248)
					{
						num = 1;
						continue;
					}
					goto IL_55;
				}
				}
				break;
				IL_55:
				num = 4;
			}
		}
		IL_8F:
		throw new PathTooLongException(RecordTableEnumerator.b("ᵈ⍊⡌潎㝐㩒㥔㉖祘㕚㱜㉞Ѡ䍢౤ᑦ䥨Ὢɬn兰ὲᩴ᥶Ṹ啺嵼⭾ꖄﲈ놐ﮜ쒠잢薤솦삨잪좬辮\udfb0튲\ud8b4튶馸횺좼첾뗀Ꞔꋆꟊ꣌볎ꋐꇔ뿖룘뗚﷜ퟠ폢엤蓦臨諪鿬軮鋰蟲郴藶諸\udbfa鳼釾攀⌂焄漆氈⬊椌明挐瘒瘔挖瘘椚搜㼞传䈢䠤䈦न䘪堬尮䔰ጲ圴制ᤸ场堼䰾㉀捂ㅄ⽆⡈╊浌絎敐歒畔㑖ㅘ㩚⽜㹞ɠᝢdᕦᩨ", a_));
	}

	// Token: 0x060045C7 RID: 17863 RVA: 0x002A8614 File Offset: 0x002A7614
	internal void ᜁ(string A_0, string A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1F;
				default:
					if (false)
					{
					}
					this.ᜀ(A_0, A_1);
					num = 0;
					continue;
				}
				break;
			}
			goto IL_1C;
			IL_1F:
			if (true)
			{
			}
			num = 2;
			continue;
			IL_1C:
			if (A_1 != null)
			{
				goto IL_1F;
			}
			break;
		}
	}

	// Token: 0x060045C8 RID: 17864 RVA: 0x002A868C File Offset: 0x002A768C
	internal void ᜀ(string A_0, string A_1, byte[] A_2)
	{
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_27;
				default:
					if (false)
					{
					}
					this.ᜆ = A_2;
					this.ᜀ(A_0, A_1);
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			goto IL_24;
			IL_27:
			num = 1;
			continue;
			IL_24:
			if (A_1 != null)
			{
				goto IL_27;
			}
			break;
		}
	}

	// Token: 0x060045C9 RID: 17865 RVA: 0x002A870C File Offset: 0x002A770C
	internal void ᜀ(string A_0, string A_1)
	{
		for (;;)
		{
			byte[] a_ = this.ᜀ(A_1);
			this.ᜅ = this.ᜀ(a_, A_0, A_1);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (File.Exists(A_0))
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						if (false)
						{
						}
						File.Delete(A_0);
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_55;
				case 3:
					if (this.ᜊ())
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					return;
				}
				break;
				IL_55:
				if (true)
				{
				}
				num = 0;
			}
		}
	}

	// Token: 0x060045CA RID: 17866 RVA: 0x002A87C8 File Offset: 0x002A77C8
	internal byte[] ᜀ(byte[] A_0, string A_1, string A_2)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		MemoryStream a_2 = new MemoryStream(A_0);
		spr\u2399 spr_u = new spr\u2399(a_2, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
		spr\u2399 spr_u2 = spr_u.ᜀ(RecordTableEnumerator.b("ു♃ⱅⵇ⥉㡋ṍ㽏㵑㡓", a_), STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
		spr\u2399 a_3 = spr_u2.ᜀ(A_2, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
		this.ᜁ(a_3, A_1);
		this.ᜁ(a_3);
		this.ᜀ(a_3);
		this.ᜀ(a_3, A_1);
		spr_u2.Flush();
		spr_u.Flush();
		MemoryStream memoryStream = new MemoryStream();
		spr_u.ᜀ(memoryStream);
		memoryStream.Flush();
		byte[] result = memoryStream.ToArray();
		memoryStream.Close();
		spr_u.Close();
		spr_u.Dispose();
		spr_u2.Close();
		spr_u2.Dispose();
		return result;
	}

	// Token: 0x060045CB RID: 17867 RVA: 0x002A88AC File Offset: 0x002A78AC
	private byte[] ᜀ(string A_0)
	{
		int a_ = 13;
		try
		{
			switch (0)
			{
			default:
				for (;;)
				{
					MemoryStream memoryStream = null;
					spr\u2399 spr_u = null;
					spr\u2399 spr_u2 = null;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (memoryStream != null)
							{
								num = 2;
								continue;
							}
							goto IL_252;
						case 1:
							goto IL_252;
						case 2:
							goto IL_18F;
						case 3:
							if (this.ᜎ().Length == 0)
							{
								num = 7;
								continue;
							}
							memoryStream = new MemoryStream(this.ᜎ());
							spr_u = new spr\u2399(memoryStream);
							spr_u2 = spr_u.ᜀ(RecordTableEnumerator.b("ూ❄ⵆⱈ⡊㥌὎㹐㱒㥔", a_), STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_18F;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 4:
							goto IL_72;
						case 5:
							goto IL_72;
						case 6:
							if (this.ᜎ() != null)
							{
								num = 8;
								continue;
							}
							goto IL_226;
						case 7:
							goto IL_226;
						case 8:
							num = 3;
							continue;
						case 9:
							goto IL_25E;
						}
						break;
						IL_72:
						spr\u2604 spr_u3 = new spr\u2604();
						spr_u3.ᜇ().ᜄ(A_0);
						spr_u3.\u170D().ᜁ()[1].ᜀ(spr\u20E9.ᜂ());
						MemoryStream memoryStream2 = new MemoryStream();
						spr_u3.ᜆ();
						spr_u3.ᜂ(memoryStream2);
						spr_u3.ᜊ();
						memoryStream2.Flush();
						byte[] buffer = memoryStream2.ToArray();
						memoryStream2.Close();
						memoryStream2 = new MemoryStream(buffer);
						MemoryStream memoryStream3 = new MemoryStream();
						spr\u2399 spr_u4 = new spr\u2399(memoryStream2);
						spr\u2399 spr_u5 = spr_u4.ᜆ(A_0);
						spr\u2399.ᜀ(spr_u5, spr_u2);
						spr_u.Flush();
						spr_u.ᜀ(memoryStream3);
						memoryStream3.Position = 0L;
						this.ᜅ = memoryStream3.ToArray();
						spr_u4.Close();
						spr_u4.Dispose();
						spr_u5.Close();
						spr_u5.Dispose();
						spr_u.Close();
						spr_u.Dispose();
						spr_u2.Close();
						spr_u2.Dispose();
						memoryStream3.Close();
						memoryStream3.Dispose();
						memoryStream2.Close();
						memoryStream2.Dispose();
						num = 0;
						continue;
						IL_18F:
						memoryStream.Close();
						memoryStream.Dispose();
						num = 1;
						continue;
						IL_226:
						spr_u = spr\u2399.ᜆ();
						spr_u2 = spr_u.ᜈ(RecordTableEnumerator.b("ూ❄ⵆⱈ⡊㥌὎㹐㱒㥔", a_));
						num = 5;
						continue;
						IL_252:
						num = 9;
					}
				}
				IL_25E:
				break;
			}
		}
		catch (Exception)
		{
		}
		if (true)
		{
		}
		return this.ᜅ;
	}

	// Token: 0x060045CC RID: 17868 RVA: 0x002A8B50 File Offset: 0x002A7B50
	private void ᜁ(spr\u2399 A_0, string A_1)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.ᜁ(RecordTableEnumerator.b("䍁ୃ⩅ⵇ", a_));
		this.ᜈ = new spr\u23BD(A_1);
		this.ᜈ.ᜀ(A_0);
		A_0.Close();
	}

	// Token: 0x060045CD RID: 17869 RVA: 0x002A8BC8 File Offset: 0x002A7BC8
	private void ᜁ(spr\u2399 A_0)
	{
		int a_ = 11;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.ᜁ(RecordTableEnumerator.b("䉀ూ❄ⵆH╊⭌⁎", a_));
		this.\u170D = new spr\u2489();
		this.\u170D.ᜀ(A_0);
		A_0.Close();
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x002A8C40 File Offset: 0x002A7C40
	private Stream ᜀ()
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 2;
			MemoryStream memoryStream2;
			for (;;)
			{
				MemoryStream memoryStream;
				spr\u2399 spr_u;
				spr\u2399 spr_u2;
				spr\u2399 spr_u3;
				spr\u2399 spr_u4;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (this.ᜎ().Length == 0)
					{
						num = 3;
						continue;
					}
					goto IL_250;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_268;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					goto IL_29B;
				case 4:
					try
					{
						try
						{
							memoryStream = new MemoryStream(this.ᜎ());
							spr_u = new spr\u2399(memoryStream);
							spr_u2 = spr_u.ᜆ(RecordTableEnumerator.b("ു♃ⱅⵇ⥉㡋ṍ㽏㵑㡓", a_));
							spr_u3 = spr_u2.ᜆ(this.ᜁ().ToString());
							spr_u4 = spr\u2399.ᜆ();
							spr\u2399.ᜀ(spr_u3, spr_u4);
							memoryStream2 = new MemoryStream();
							spr_u4.ᜀ(memoryStream2);
							memoryStream2.Position = 0L;
						}
						catch (Exception)
						{
						}
						goto IL_29F;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								spr_u2.Close();
								spr_u2.Dispose();
								num = 12;
								continue;
							case 2:
								if (spr_u4 != null)
								{
									num = 6;
									continue;
								}
								goto IL_24F;
							case 3:
								spr_u3.Close();
								spr_u3.Dispose();
								num = 9;
								continue;
							case 4:
								goto IL_197;
							case 5:
								memoryStream.Close();
								memoryStream.Dispose();
								num = 4;
								continue;
							case 6:
								spr_u4.Close();
								spr_u4.Dispose();
								num = 13;
								continue;
							case 7:
								if (spr_u3 != null)
								{
									num = 3;
									continue;
								}
								goto IL_1B7;
							case 8:
								if (spr_u != null)
								{
									num = 10;
									continue;
								}
								goto IL_22F;
							case 9:
								goto IL_1B7;
							case 10:
								spr_u.Close();
								spr_u2.Dispose();
								num = 14;
								continue;
							case 11:
								if (spr_u2 != null)
								{
									num = 0;
									continue;
								}
								goto IL_1D8;
							case 12:
								goto IL_1D8;
							case 13:
								goto IL_178;
							case 14:
								goto IL_22F;
							}
							if (memoryStream != null)
							{
								num = 5;
								continue;
							}
							IL_197:
							num = 8;
							continue;
							IL_1B7:
							num = 2;
							continue;
							IL_1D8:
							num = 7;
							continue;
							IL_22F:
							num = 11;
						}
						IL_178:
						IL_24F:;
					}
					goto IL_250;
				}
				if (this.ᜎ() != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_268:
				num = 4;
				continue;
				IL_250:
				memoryStream = new MemoryStream(this.ᜎ());
				spr_u = null;
				spr_u2 = null;
				spr_u3 = null;
				spr_u4 = null;
				memoryStream2 = null;
				goto IL_268;
			}
			IL_29B:
			return null;
			IL_29F:
			if (true)
			{
			}
			return memoryStream2;
		}
		}
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x002A8F2C File Offset: 0x002A7F2C
	private void ᜀ(spr\u2399 A_0)
	{
		int a_ = 16;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_41;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					A_0.ᜁ(RecordTableEnumerator.b("䝅େ╉⅋㹍὏け㹓", a_));
					this.ᜇ = new sprᯤ();
					this.ᜇ.ᜀ(A_0);
					A_0.Close();
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			goto IL_25;
			IL_41:
			num = 1;
			continue;
			IL_25:
			if (!this.ᜀ(A_0.ᜎ(), RecordTableEnumerator.b("䝅େ╉⅋㹍὏け㹓", a_)))
			{
				goto IL_41;
			}
			break;
		}
	}

	// Token: 0x060045D0 RID: 17872 RVA: 0x002A8FF0 File Offset: 0x002A7FF0
	private void ᜀ(spr\u2399 A_0, string A_1)
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		string fileName = Path.GetFileName(A_1);
		byte[] bytes = asciiencoding.GetBytes(fileName);
		byte[] bytes2 = asciiencoding.GetBytes(A_1);
		byte[] array = new byte[2];
		array[0] = 2;
		byte[] array2 = array;
		byte[] array3 = new byte[4];
		array3[2] = 3;
		byte[] array4 = array3;
		int num = 4;
		num += array2.Length;
		num += bytes.Length + 1;
		num += bytes2.Length + 1;
		num += array4.Length;
		num += 4;
		num += bytes2.Length + 1;
		num += 4;
		num += this.ᜆ.Length;
		num += 2;
		int num2 = 0;
		byte[] array5 = new byte[num];
		spr\u20AE.ᜀ(array5, ref num2, num - 4);
		spr\u20AE.ᜀ(array5, ref num2, array2);
		spr\u20AE.ᜀ(array5, ref num2, bytes);
		num2++;
		spr\u20AE.ᜀ(array5, ref num2, bytes2);
		num2++;
		spr\u20AE.ᜀ(array5, ref num2, array4);
		spr\u20AE.ᜀ(array5, ref num2, bytes2.Length + 1);
		spr\u20AE.ᜀ(array5, ref num2, bytes2);
		num2++;
		spr\u20AE.ᜀ(array5, ref num2, this.ᜆ.Length);
		spr\u20AE.ᜀ(array5, ref num2, this.ᜆ);
		A_0.ᜁ(RecordTableEnumerator.b("㨺爼匾⑀牂畄ॆ⡈㽊⑌㥎㑐", a_));
		A_0.Write(array5, 0, array5.Length);
		A_0.Close();
	}

	// Token: 0x060045D1 RID: 17873 RVA: 0x002A917C File Offset: 0x002A817C
	private bool ᜀ(string[] A_0, string A_1)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 0;
			int num2 = A_0.Length;
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					result = true;
					num3 = 4;
					continue;
				case 1:
					if (A_0[num] == A_1)
					{
						num3 = 0;
						continue;
					}
					if (true)
					{
					}
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AC;
					}
					if (false)
					{
					}
					num3 = 5;
					continue;
				case 2:
					goto IL_A1;
				case 3:
					return result;
				case 4:
					return result;
				case 5:
					goto IL_A1;
				case 6:
					goto IL_AC;
				}
				break;
				IL_AC:
				if (num >= num2)
				{
					num3 = 3;
					continue;
				}
				num3 = 1;
				continue;
				IL_A1:
				num3 = 6;
			}
		}
		return result;
	}

	// Token: 0x060045D2 RID: 17874 RVA: 0x002A9248 File Offset: 0x002A8248
	public int \u1715()
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
		XlsExternWorkbook xlsExternWorkbook = this.\u1718.ParentWorkbook.ExternWorkbooks[this.ᜋ];
		return xlsExternWorkbook.Index;
	}

	// Token: 0x04001FC1 RID: 8129
	private const string ᜀ = "ObjectPool";

	// Token: 0x04001FC2 RID: 8130
	private const string ᜁ = "\u0001Ole";

	// Token: 0x04001FC3 RID: 8131
	private const string ᜂ = "\u0003ObjInfo";

	// Token: 0x04001FC4 RID: 8132
	private const string ᜃ = "\u0001CompObj";

	// Token: 0x04001FC5 RID: 8133
	private const string ᜄ = "\u0001Ole10Native";

	// Token: 0x04001FC6 RID: 8134
	private byte[] ᜅ;

	// Token: 0x04001FC7 RID: 8135
	private byte[] ᜆ;

	// Token: 0x04001FC8 RID: 8136
	private sprᯤ ᜇ;

	// Token: 0x04001FC9 RID: 8137
	private spr\u23BD ᜈ;

	// Token: 0x04001FCA RID: 8138
	private SizeF ᜉ;

	// Token: 0x04001FCB RID: 8139
	private bool ᜊ;

	// Token: 0x04001FCC RID: 8140
	private string ᜋ;

	// Token: 0x04001FCD RID: 8141
	private Stream ᜌ;

	// Token: 0x04001FCE RID: 8142
	private spr\u2489 \u170D;

	// Token: 0x04001FCF RID: 8143
	private Dictionary<string, int> ᜎ;

	// Token: 0x04001FD0 RID: 8144
	private string ᜏ;

	// Token: 0x04001FD1 RID: 8145
	private DVAspect ᜐ;

	// Token: 0x04001FD2 RID: 8146
	private XlsShape ᜑ;

	// Token: 0x04001FD3 RID: 8147
	private string \u1712;

	// Token: 0x04001FD4 RID: 8148
	private string \u1713;

	// Token: 0x04001FD5 RID: 8149
	private OleObjectType \u1714;

	// Token: 0x04001FD6 RID: 8150
	private bool \u1715;

	// Token: 0x04001FD7 RID: 8151
	private OleLinkType \u1716;

	// Token: 0x04001FD8 RID: 8152
	private bool \u1717;

	// Token: 0x04001FD9 RID: 8153
	private XlsWorksheet \u1718;

	// Token: 0x04001FDA RID: 8154
	private string \u1719;

	// Token: 0x04001FDB RID: 8155
	private string \u171A;
}
