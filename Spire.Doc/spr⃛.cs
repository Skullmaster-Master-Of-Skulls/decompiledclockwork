using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000356 RID: 854
internal class spr\u20DB
{
	// Token: 0x06002DD6 RID: 11734 RVA: 0x002BCFF8 File Offset: 0x002BBFF8
	internal spr\u20DB(sprᰎ A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06002DD7 RID: 11735 RVA: 0x002BD014 File Offset: 0x002BC014
	public void ᜀ(Stream A_0)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜆ())
				{
					num = 2;
					continue;
				}
				goto IL_A6;
			case 1:
				goto IL_3C;
			case 2:
				goto IL_90;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_3C:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A6:
			this.ᜅ().ᜀ(A_0, this.ᜁ());
			return;
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("Ṭ᭮Ͱᙲᑴ᩶", a_));
		}
		IL_90:
		throw new InvalidOperationException(ClipboardData.b("㥬ݮᡰr啴㡶㕸㹺嵼ၾﶈꮊﲎ놐ﾒﲔ練ﺚ列뎞膠슢쮤쎦覨좪첬솮\udfb0\udcb2솴鞶\udbb8\udeba鶼첾ꃀ뗂ꃄꏆ苌ꇎ뷐꫒닖듘맚룜믞藠蛢臤쟦ꛨ꟪꣬쿮黰釲鿴鋶髸迺軼\udffe戀戂欄✆欈渊ⴌ簎瀐攒瀔猖㜘", a_));
	}

	// Token: 0x06002DD8 RID: 11736 RVA: 0x002BD0DC File Offset: 0x002BC0DC
	public void ᜁ(string A_0)
	{
		Stream stream = File.Create(A_0);
		try
		{
			if (true)
			{
			}
			this.ᜀ(stream);
		}
		finally
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6D;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_39;
					default:
						if (false)
						{
						}
						((IDisposable)stream).Dispose();
						num = 0;
						continue;
					}
					break;
				}
				goto IL_36;
				IL_39:
				num = 2;
				continue;
				IL_36:
				if (stream != null)
				{
					goto IL_39;
				}
				break;
			}
			IL_6D:;
		}
	}

	// Token: 0x06002DD9 RID: 11737 RVA: 0x002BD174 File Offset: 0x002BC174
	public string ᜃ()
	{
		if (this.ᜅ() == null)
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
				return "";
			}
		}
		return this.ᜅ().ᜀ(this.ᜁ());
	}

	// Token: 0x06002DDA RID: 11738 RVA: 0x002BD1D0 File Offset: 0x002BC1D0
	public MemoryStream ᜃ(string A_0)
	{
		if (this.ᜂ() != null)
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
				return (MemoryStream)this.ᜂ().ᜂ()[A_0];
			}
		}
		return null;
	}

	// Token: 0x06002DDB RID: 11739 RVA: 0x002BD230 File Offset: 0x002BC230
	internal bool ᜌ()
	{
		if (this.ᜆ())
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			}
			if (false)
			{
			}
			return this.ᜅ() == null;
		}
		return false;
	}

	// Token: 0x06002DDC RID: 11740 RVA: 0x002BD280 File Offset: 0x002BC280
	public string ᜁ()
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
		return (string)this.ᜀ(4113);
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x002BD2CC File Offset: 0x002BC2CC
	public void ᜀ(string A_0)
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ɳ᝵ᑷཹ᥻", a_));
		this.ᜀ(4113, A_0);
	}

	// Token: 0x06002DDE RID: 11742 RVA: 0x002BD330 File Offset: 0x002BC330
	public bool ᜆ()
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
		return spr\u1CC6.ᜋ(this.ᜇ());
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x002BD378 File Offset: 0x002BC378
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
		return (string)this.ᜀ(4114);
	}

	// Token: 0x06002DE0 RID: 11744 RVA: 0x002BD3C4 File Offset: 0x002BC3C4
	public void ᜄ(string A_0)
	{
		int a_ = 14;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ɳ᝵ᑷཹ᥻", a_));
		this.ᜀ(4114, A_0);
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x002BD428 File Offset: 0x002BC428
	public string ᜄ()
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
		return (string)this.ᜀ(4115);
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x002BD474 File Offset: 0x002BC474
	public void ᜂ(string A_0)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("ᡭᅯṱų፵", a_));
		this.ᜀ(4115, A_0);
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x002BD4D8 File Offset: 0x002BC4D8
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
		return (bool)this.ᜀ(4116);
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x002BD524 File Offset: 0x002BC524
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
		this.ᜀ(4116, A_0);
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x002BD570 File Offset: 0x002BC570
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
		return (bool)this.ᜀ(826);
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x002BD5BC File Offset: 0x002BC5BC
	internal void ᜂ(bool A_0)
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
		this.ᜀ(826, A_0);
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x002BD608 File Offset: 0x002BC608
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
		return (bool)this.ᜀ(4117);
	}

	// Token: 0x06002DE8 RID: 11752 RVA: 0x002BD654 File Offset: 0x002BC654
	public void ᜁ(bool A_0)
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
		this.ᜀ(4117, A_0);
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x002BD6A0 File Offset: 0x002BC6A0
	public Guid ᜏ()
	{
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (this.ᜈ() != null)
				{
					num = 3;
					continue;
				}
				goto IL_97;
			case 2:
				goto IL_38;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				default:
					goto IL_50;
				}
				break;
			}
			if (this.ᜂ() != null)
			{
				num = 2;
				continue;
			}
			IL_62:
			num = 0;
		}
		IL_38:
		return this.ᜂ().ᜂ().ᜀ();
		IL_50:
		if (false)
		{
		}
		return this.ᜈ().ᜁ();
		IL_97:
		return Guid.Empty;
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x002BD74C File Offset: 0x002BC74C
	internal spr\u1CDF ᜅ()
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
		return (spr\u1CDF)this.ᜀ(4112);
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x002BD798 File Offset: 0x002BC798
	internal void ᜀ(spr\u1CDF A_0)
	{
		int a_ = 5;
		if (A_0 == null)
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
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("ᵪ౬ͮѰᙲ", a_));
		}
		this.ᜀ(4112, A_0);
		this.ᜀ();
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x002BD808 File Offset: 0x002BC808
	internal sprᶑ ᜂ()
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
		return this.ᜅ() as sprᶑ;
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x002BD850 File Offset: 0x002BC850
	internal sprẃ ᜈ()
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
		return this.ᜅ() as sprẃ;
	}

	// Token: 0x06002DEE RID: 11758 RVA: 0x002BD898 File Offset: 0x002BC898
	private void ᜀ()
	{
		int a_ = 6;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ().ᜂ().Contains(ClipboardData.b("潫Ⅽቯᡱ㵳ᡵṷᕹ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_CA;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5F;
				default:
					goto IL_7D;
				}
				break;
			case 2:
			{
				spr\u19ED spr_u19ED = new spr\u19ED(this.ᜂ().ᜂ());
				this.ᜂ(spr_u19ED.ᜄ());
				goto IL_5F;
			}
			case 3:
				num = 0;
				continue;
			}
			if (this.ᜂ() != null)
			{
				num = 3;
				continue;
			}
			goto IL_CA;
			IL_5F:
			num = 1;
		}
		IL_7D:
		if (false)
		{
		}
		IL_CA:
		if (true)
		{
		}
	}

	// Token: 0x06002DEF RID: 11759 RVA: 0x002BD978 File Offset: 0x002BC978
	internal int \u170D()
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
		return (int)this.ᜀ(267);
	}

	// Token: 0x06002DF0 RID: 11760 RVA: 0x002BD9C4 File Offset: 0x002BC9C4
	internal void ᜂ(int A_0)
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
		this.ᜀ(267, A_0);
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x002BDA10 File Offset: 0x002BCA10
	internal OleLinkType ᜉ()
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
		return (OleLinkType)this.ᜀ(4118);
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x002BDA5C File Offset: 0x002BCA5C
	internal void ᜀ(OleLinkType A_0)
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
		this.ᜀ(4118, A_0);
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x002BDAA8 File Offset: 0x002BCAA8
	internal int ᜋ()
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
		return (int)this.ᜀ(4119);
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x002BDAF4 File Offset: 0x002BCAF4
	internal void ᜁ(int A_0)
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
		this.ᜀ(4119, A_0);
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x002BDB40 File Offset: 0x002BCB40
	private object ᜀ(int A_0)
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
		return this.ᜀ.ᜂ(A_0);
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x002BDB88 File Offset: 0x002BCB88
	private void ᜀ(int A_0, object A_1)
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
		this.ᜀ.ᜀ(A_0, A_1);
	}

	// Token: 0x0400269C RID: 9884
	private readonly sprᰎ ᜀ;
}
