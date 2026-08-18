using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x0200018D RID: 397
internal class spr\u1937 : sprᩍ
{
	// Token: 0x06000E99 RID: 3737 RVA: 0x000EA248 File Offset: 0x000E9248
	internal spr\u1937(DocumentBase A_0) : base((Document)A_0)
	{
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x000EA264 File Offset: 0x000E9264
	internal spr\u1937(DocumentBase A_0, Spire.Doc.Fields.Shape.ShapeType A_1)
	{
		int a_ = 8;
		this..ctor(A_0);
		if (A_1 != Spire.Doc.Fields.Shape.ShapeType.OleControl && A_1 != Spire.Doc.Fields.Shape.ShapeType.OleObject && A_1 != Spire.Doc.Fields.Shape.ShapeType.NonPrimitive)
		{
			if (A_1 != Spire.Doc.Fields.Shape.ShapeType.CustomShape)
			{
				base.ᜀ(A_1);
				return;
			}
		}
		throw new NotSupportedException(ClipboardData.b("⵭ᅯᱱᩳ᥵౷婹ύ౽ꢇ黎뚕ﲙ벛좟쮡힣蚥\udca7펩\udcab쮭麯", a_));
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x000EA2BC File Offset: 0x000E92BC
	internal new static spr\u1937 ᜀ(Document A_0)
	{
		spr\u1937 spr_u;
		for (;;)
		{
			IL_1E:
			spr_u = new spr\u1937(A_0, Spire.Doc.Fields.Shape.ShapeType.Rectangle);
			spr_u.ᜀ(TextWrappingStyle.Inline);
			spr_u.ᜆ(true);
			spr_u.ᜁ(sprᣫ.ᜁ);
			spr_u.ᜄ(false);
			spr_u.ᜡ().ᜂ(true);
			spr_u.ᜡ().ᜁ(true);
			spr_u.ᜄ(1.5);
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D4:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Sections.Count > 0)
					{
						num = 2;
						continue;
					}
					goto IL_E1;
				case 1:
					goto IL_DF;
				case 2:
					goto IL_B5;
				}
				goto IL_1E;
			}
			IL_B5:
			spr_u.ᜅ((double)A_0.Sections[0].PageSetup.ClientWidth);
			goto IL_D4;
		}
		IL_DF:
		IL_E1:
		spr_u.ᜡ().ᜁ(100.0);
		return spr_u;
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x000EA3C0 File Offset: 0x000E93C0
	public override DocumentObjectType ᜁ()
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
		return DocumentObjectType.Shape;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x000EA400 File Offset: 0x000E9400
	protected override void ᜂ()
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
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x000EA43C File Offset: 0x000E943C
	public bool ᝆ()
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
		return (bool)base.ᜈ(443);
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x000EA488 File Offset: 0x000E9488
	public new void ᜆ(bool A_0)
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
		base.ᜀ(443, A_0);
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x000EA4D4 File Offset: 0x000E94D4
	public Color \u1738()
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
		return this.ᜦ().ᜀ();
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x000EA51C File Offset: 0x000E951C
	public new void ᜁ(Color A_0)
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
		this.ᜦ().ᜃ(A_0);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x000EA564 File Offset: 0x000E9564
	internal bool \u1716()
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
		return (bool)base.ᜈ(511);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x000EA5B0 File Offset: 0x000E95B0
	internal void ᜅ(bool A_0)
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
		base.ᜀ(511, A_0);
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x000EA5FC File Offset: 0x000E95FC
	public bool \u171D()
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
		return this.\u1736().ᜂ();
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000EA644 File Offset: 0x000E9644
	public void ᜄ(bool A_0)
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
		this.\u1736().ᜀ(A_0);
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x000EA68C File Offset: 0x000E968C
	public double ᝅ()
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
		return this.\u1736().ᜃ();
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x000EA6D4 File Offset: 0x000E96D4
	public new void ᜀ(double A_0)
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
		this.\u1736().ᜀ(A_0);
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x000EA71C File Offset: 0x000E971C
	public Color ᜣ()
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
		return this.\u1736().\u1714();
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x000EA764 File Offset: 0x000E9764
	public new void ᜀ(Color A_0)
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
		this.\u1736().ᜃ(A_0);
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x000EA7AC File Offset: 0x000E97AC
	internal BWMode \u171E()
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
		return (BWMode)base.ᜈ(772);
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x000EA7F8 File Offset: 0x000E97F8
	internal new void ᜀ(BWMode A_0)
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
		base.ᜀ(772, A_0);
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x000EA844 File Offset: 0x000E9844
	internal BWMode ᜰ()
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
		return (BWMode)base.ᜈ(773);
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000EA890 File Offset: 0x000E9890
	internal void ᜂ(BWMode A_0)
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
		base.ᜀ(773, A_0);
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x000EA8DC File Offset: 0x000E98DC
	internal BWMode ᜤ()
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
		return (BWMode)base.ᜈ(774);
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x000EA928 File Offset: 0x000E9928
	internal new void ᜁ(BWMode A_0)
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
		base.ᜀ(774, A_0);
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x000EA974 File Offset: 0x000E9974
	internal string \u173B()
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
		return (string)base.ᜈ(910);
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x000EA9C0 File Offset: 0x000E99C0
	internal new void ᜁ(string A_0)
	{
		int a_ = 16;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("u᥷ᙹॻ᭽", a_));
		base.ᜀ(910, A_0);
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000EAA24 File Offset: 0x000E9A24
	internal string \u1715()
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
		return (string)base.ᜈ(919);
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000EAA70 File Offset: 0x000E9A70
	internal new void ᜀ(string A_0)
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		spr\u1CC6.ᜀ(A_0, ClipboardData.b("Ὠ੪Ŭᩮᑰ", a_));
		base.ᜀ(919, A_0);
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x000EAAD4 File Offset: 0x000E9AD4
	internal bool \u173E()
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
		return (bool)base.ᜈ(827);
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x000EAB20 File Offset: 0x000E9B20
	internal void ᜈ(bool A_0)
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
		base.ᜀ(827, A_0);
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x000EAB6C File Offset: 0x000E9B6C
	internal spr\u2554 \u1736()
	{
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
					break;
				default:
					goto IL_62;
				}
				break;
			case 2:
				this.ᜀ = new spr\u2554(this);
				num = 1;
				continue;
			}
			if (this.ᜀ != null)
			{
				goto IL_72;
			}
			num = 2;
		}
		IL_62:
		if (true)
		{
		}
		if (false)
		{
		}
		IL_72:
		return this.ᜀ;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x000EABF4 File Offset: 0x000E9BF4
	internal sprᤖ ᜦ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜁ = new sprᤖ(this);
				num = 2;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_62;
				}
				break;
			}
			if (this.ᜁ != null)
			{
				goto IL_72;
			}
			num = 0;
		}
		IL_62:
		if (true)
		{
		}
		if (false)
		{
		}
		IL_72:
		return this.ᜁ;
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x000EAC7C File Offset: 0x000E9C7C
	public bool \u173C()
	{
		if (base.ᝥ())
		{
			for (;;)
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
					goto IL_28;
				}
			}
			IL_28:
			if (false)
			{
			}
			return this.ᜮ().\u1718();
		}
		return false;
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x000EACD0 File Offset: 0x000E9CD0
	internal bool \u1735()
	{
		if (true)
		{
		}
		if (base.ᝥ())
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_28;
				}
			}
			IL_28:
			if (false)
			{
			}
			return this.ᜮ().ᜑ();
		}
		return false;
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x000EAD24 File Offset: 0x000E9D24
	internal sprỏ ᜮ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_78;
			case 1:
			{
				Document document = base.Document;
				this.ᜂ = new sprỏ(this, document);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			case 2:
				if (this.ᜂ == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 4:
				goto IL_3E;
			}
			if (!base.ᝥ())
			{
				num = 4;
			}
			else
			{
				num = 2;
			}
		}
		IL_3E:
		return null;
		IL_78:
		if (true)
		{
		}
		IL_A1:
		return this.ᜂ;
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x000EADD8 File Offset: 0x000E9DD8
	internal spr\u20DB \u171B()
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_46;
			case 1:
				goto IL_78;
			case 2:
				this.ᜃ = new spr\u20DB(this);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_46;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 3:
				if (this.ᜃ == null)
				{
					num = 2;
					continue;
				}
				goto IL_99;
			}
			if (!base.ត())
			{
				if (true)
				{
				}
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_46:
		return null;
		IL_78:
		IL_99:
		return this.ᜃ;
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x000EAE84 File Offset: 0x000E9E84
	internal bool ᜱ()
	{
		if (base.ឌ())
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_20;
				}
			}
			IL_20:
			if (true)
			{
			}
			if (false)
			{
			}
			return this.\u171B().ᜈ() != null;
		}
		return false;
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x000EAEDC File Offset: 0x000E9EDC
	internal sprᴐ ᜧ()
	{
		int num = 1;
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
					goto IL_6A;
				}
				break;
			case 2:
				if (true)
				{
				}
				this.ᜄ = new sprᴐ(this);
				num = 0;
				continue;
			}
			if (this.ᜄ != null)
			{
				goto IL_72;
			}
			num = 2;
		}
		IL_6A:
		if (false)
		{
		}
		IL_72:
		return this.ᜄ;
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x000EAF64 File Offset: 0x000E9F64
	internal spr\u1F8D \u173A()
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
				this.ᜅ = new spr\u1F8D(this);
				num = 2;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_6A;
				}
				break;
			}
			if (this.ᜅ != null)
			{
				goto IL_72;
			}
			num = 0;
		}
		IL_6A:
		if (false)
		{
		}
		IL_72:
		return this.ᜅ;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000EAFEC File Offset: 0x000E9FEC
	internal sprℹ \u173F()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7D;
			case 1:
				this.ᜇ = new sprℹ(this);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_43;
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
				goto IL_43;
			case 4:
				if (this.ᜇ == null)
				{
					num = 1;
					continue;
				}
				goto IL_9E;
			}
			if (base.ᜊ(1983) == null)
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
		}
		IL_43:
		return null;
		IL_7D:
		IL_9E:
		return this.ᜇ;
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x000EB0A0 File Offset: 0x000EA0A0
	internal sprᣫ ᜡ()
	{
		if (true)
		{
		}
		int num = 2;
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
					goto IL_6A;
				}
				break;
			case 1:
				this.ᜆ = new sprᣫ(this);
				num = 0;
				continue;
			}
			if (this.ᜆ != null)
			{
				goto IL_72;
			}
			num = 1;
		}
		IL_6A:
		if (false)
		{
		}
		IL_72:
		return this.ᜆ;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x000EB128 File Offset: 0x000EA128
	internal new ConnectorType ᜄ()
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
		return base.ᜄ();
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x000EB16C File Offset: 0x000EA16C
	internal new void ᜀ(ConnectorType A_0)
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
		base.ᜀ(A_0);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x000EB1B0 File Offset: 0x000EA1B0
	internal new static bool ᜀ(spr\u1937 A_0)
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
				goto IL_7C;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				if (!A_0.ᝆ())
				{
					num = 0;
					continue;
				}
				return true;
			}
			goto IL_32;
			IL_35:
			num = 2;
			continue;
			IL_32:
			if (A_0 != null)
			{
				goto IL_35;
			}
			return false;
		}
		return true;
		IL_7C:
		return A_0.\u173C();
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x000EB23C File Offset: 0x000EA23C
	internal Spire.Doc.Fields.Shape.OleObjectType \u1737()
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.\u171B().ᜆ())
				{
					num = 4;
					continue;
				}
				return Spire.Doc.Fields.Shape.OleObjectType.Linked;
			case 1:
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return Spire.Doc.Fields.Shape.OleObjectType.Embedded;
				default:
					if (false)
					{
					}
					if (base.\u1758())
					{
						num = 1;
						continue;
					}
					return Spire.Doc.Fields.Shape.OleObjectType.None;
				}
				break;
			case 3:
				goto IL_38;
			case 4:
				return Spire.Doc.Fields.Shape.OleObjectType.Embedded;
			}
			if (base.ឌ())
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_38:
		if (true)
		{
		}
		return Spire.Doc.Fields.Shape.OleObjectType.Control;
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x000EB2F4 File Offset: 0x000EA2F4
	internal sprỬ[] ᝀ()
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
		return (sprỬ[])base.ᜈ(326);
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x000EB340 File Offset: 0x000EA340
	internal spr\u2055[] ᝁ()
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
		return (spr\u2055[])base.ᜈ(325);
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x000EB38C File Offset: 0x000EA38C
	internal spr\u2528[] \u1734()
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
		return (spr\u2528[])base.ᜈ(342);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x000EB3D8 File Offset: 0x000EA3D8
	internal int ᜂ(int A_0)
	{
		int a_ = 8;
		int a_2;
		for (;;)
		{
			IL_61:
			int num = 7;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_170;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_163;
					case 1:
						goto IL_196;
					case 2:
						goto IL_110;
					case 3:
						goto IL_170;
					case 4:
						goto IL_126;
					case 5:
						goto IL_DC;
					case 6:
						num = 3;
						continue;
					case 7:
						switch (A_0)
						{
						case 1:
							a_2 = 327;
							num = 4;
							continue;
						case 2:
							a_2 = 328;
							if (true)
							{
							}
							num = 9;
							continue;
						case 3:
							a_2 = 329;
							num = 8;
							continue;
						case 4:
							a_2 = 330;
							num = 0;
							continue;
						case 5:
							a_2 = 331;
							num = 1;
							continue;
						case 6:
							a_2 = 332;
							num = 5;
							continue;
						case 7:
							a_2 = 333;
							num = 11;
							continue;
						case 8:
							a_2 = 334;
							num = 12;
							continue;
						case 9:
							a_2 = 335;
							num = 10;
							continue;
						case 10:
							a_2 = 336;
							num = 2;
							continue;
						default:
							num = 6;
							continue;
						}
						break;
					case 8:
						goto IL_1A9;
					case 9:
						goto IL_FA;
					case 10:
						goto IL_183;
					case 11:
						goto IL_13C;
					case 12:
						goto IL_C6;
					}
					goto IL_61;
				}
			}
		}
		IL_C6:
		IL_DC:
		IL_FA:
		IL_110:
		IL_126:
		IL_13C:
		IL_163:
		goto IL_1AB;
		IL_170:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ݭṯᙱᅳ๵", a_));
		IL_183:
		IL_196:
		IL_1A9:
		IL_1AB:
		return (int)base.ᜈ(a_2);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x000EB59C File Offset: 0x000EA59C
	internal Color \u1732()
	{
		object obj;
		for (;;)
		{
			obj = base.ᜊ(282);
			int num = 0;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					while (obj != null)
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
							num = 2;
							goto IL_02;
						}
					}
					goto IL_67;
				case 1:
					if ((int)obj == -1)
					{
						num = 3;
						continue;
					}
					goto IL_8B;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_89;
				}
				break;
			}
		}
		IL_67:
		return Color.Empty;
		IL_89:
		goto IL_67;
		IL_8B:
		return Color.FromArgb((int)obj | -16777216);
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x000EB648 File Offset: 0x000EA648
	internal bool ᜨ()
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
		return (bool)base.ᜈ(380);
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x000EB694 File Offset: 0x000EA694
	internal float ᜭ()
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
		return (float)spr\u23C4.ᜋ((int)base.ᜈ(459));
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x000EB6E8 File Offset: 0x000EA6E8
	internal int \u1717()
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
		return (int)base.ᜈ(339);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x000EB734 File Offset: 0x000EA734
	internal void ᜃ(int A_0)
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
		base.ᜀ(339, A_0);
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x000EB780 File Offset: 0x000EA780
	internal int ᜬ()
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
		return (int)base.ᜈ(340);
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x000EB7CC File Offset: 0x000EA7CC
	internal new void ᜁ(int A_0)
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
		base.ᜀ(340, A_0);
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x000EB818 File Offset: 0x000EA818
	internal bool \u171F()
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
		return (bool)base.ᜈ(190);
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x000EB864 File Offset: 0x000EA864
	internal new void ᜇ(bool A_0)
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
		base.ᜀ(190, A_0);
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x000EB8B0 File Offset: 0x000EA8B0
	internal bool ᝂ()
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
		return (bool)base.ᜈ(574);
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x000EB8FC File Offset: 0x000EA8FC
	internal void ᜂ(bool A_0)
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
		base.ᜀ(574, A_0);
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x000EB948 File Offset: 0x000EA948
	internal bool \u171C()
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
		return (bool)base.ᜈ(700);
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x000EB994 File Offset: 0x000EA994
	internal void ᜃ(bool A_0)
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
		base.ᜀ(700, A_0);
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x000EB9E0 File Offset: 0x000EA9E0
	internal int \u1733()
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
		return (int)base.ᜈ(720);
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x000EBA2C File Offset: 0x000EAA2C
	internal int ᝃ()
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
		return (int)base.ᜈ(644);
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x000EBA78 File Offset: 0x000EAA78
	internal int ᜫ()
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
		return (int)base.ᜈ(645);
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x000EBAC4 File Offset: 0x000EAAC4
	internal int ᜩ()
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
		return (int)base.ᜈ(715);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x000EBB10 File Offset: 0x000EAB10
	internal int ᝇ()
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
		return (int)base.ᜈ(716);
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x000EBB5C File Offset: 0x000EAB5C
	internal int \u171A()
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
		return (int)base.ᜈ(717);
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x000EBBA8 File Offset: 0x000EABA8
	internal int ᜢ()
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
		return (int)base.ᜈ(718);
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x000EBBF4 File Offset: 0x000EABF4
	internal int ᜯ()
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
		return (int)base.ᜈ(719);
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x000EBC40 File Offset: 0x000EAC40
	internal bool ᜥ()
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
		return (bool)base.ᜈ(765);
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x000EBC8C File Offset: 0x000EAC8C
	internal int ᝄ()
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
		return (int)base.ᜈ(705);
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x000EBCD8 File Offset: 0x000EACD8
	internal int \u1739()
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
		return (int)base.ᜈ(704);
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x000EBD24 File Offset: 0x000EAD24
	internal ThreeDRenderMode ᜪ()
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
		return (ThreeDRenderMode)base.ᜈ(713);
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000EBD70 File Offset: 0x000EAD70
	internal sprᥴ[] \u1718()
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
		return (sprᥴ[])base.ᜈ(341);
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x000EBDBC File Offset: 0x000EADBC
	internal spr\u1D34[] \u173D()
	{
		if (true)
		{
		}
		spr\u1D34[] array;
		for (;;)
		{
			array = (spr\u1D34[])base.ᜈ(343);
			int num = 3;
			for (;;)
			{
				IL_14:
				switch (num)
				{
				case 0:
					if (array.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_86;
				case 1:
					return array;
				case 2:
					num = 0;
					continue;
				case 3:
					while (array != null)
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
							num = 2;
							goto IL_14;
						}
					}
					goto IL_86;
				}
				break;
			}
		}
		return array;
		IL_86:
		return new spr\u1D34[]
		{
			new spr\u1D34
			{
				ᜀ = new sprṚ(-base.ᝍ(), false),
				ᜁ = new sprṚ(-base.ឈ(), false),
				ᜂ = new sprṚ(base.\u1776() - base.ᝍ(), false),
				ᜃ = new sprṚ(base.ឍ() - base.ឈ(), false)
			}
		};
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x000EBEBC File Offset: 0x000EAEBC
	internal new void ᜀ(spr\u1D34[] A_0)
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
		base.ᜀ(343, A_0);
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x000EBF04 File Offset: 0x000EAF04
	internal bool \u1719()
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
		return (bool)base.ᜈ(442);
	}

	// Token: 0x04001740 RID: 5952
	private new spr\u2554 ᜀ;

	// Token: 0x04001741 RID: 5953
	private new sprᤖ ᜁ;

	// Token: 0x04001742 RID: 5954
	private new sprỏ ᜂ;

	// Token: 0x04001743 RID: 5955
	private spr\u20DB ᜃ;

	// Token: 0x04001744 RID: 5956
	private new sprᴐ ᜄ;

	// Token: 0x04001745 RID: 5957
	private new spr\u1F8D ᜅ;

	// Token: 0x04001746 RID: 5958
	private new sprᣫ ᜆ;

	// Token: 0x04001747 RID: 5959
	private new sprℹ ᜇ;
}
