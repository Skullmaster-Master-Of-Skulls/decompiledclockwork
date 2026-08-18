using System;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000243 RID: 579
internal class sprᤅ : XlsObject, INumberFormat, ICloneParent
{
	// Token: 0x06002318 RID: 8984 RVA: 0x00145ADC File Offset: 0x00144ADC
	protected sprᤅ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜁ();
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x00145AF8 File Offset: 0x00144AF8
	public sprᤅ(spr\u1DF5 A_0, object A_1, spr\u240D A_2)
	{
		int a_ = 8;
		this..ctor(A_0, A_1);
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇", a_));
		}
		this.ᜀ = (spr\u240D)A_2.Clone();
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x00145B40 File Offset: 0x00144B40
	public sprᤅ(spr\u1DF5 A_0, object A_1, int A_2, string A_3)
	{
		int a_ = 12;
		this..ctor(A_0, A_1);
		if (A_3 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("⑁⭃㑅╇⭉㡋", a_));
		}
		if (A_3.Length == 0)
		{
			throw new ArgumentException(RecordTableEnumerator.b("с⭃㑅╇⭉㡋湍⍏♑♓㽕㙗㵙籛㵝şౡ੣॥ᱧ䩩๫୭偯᝱ᥳٵ౷͹剻", a_));
		}
		this.ᜀ = (spr\u240D)spr\u175E.ᜀ(TBIFFRecord.Format);
		this.ᜀ.ᜀ(A_2);
		this.ᜀ.ᜀ(A_3);
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x00145BC8 File Offset: 0x00144BC8
	private void ᜁ()
	{
		int a_ = 3;
		spr\u21FF spr_u21FF;
		for (;;)
		{
			spr_u21FF = (base.FindParent(typeof(spr\u21FF)) as spr\u21FF);
			if (spr_u21FF == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			goto IL_74;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("椸娺似娾⽀㝂", a_), RecordTableEnumerator.b("椸娺似娾⽀㝂敄⡆⭈⅊⡌ⱎ═獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ཨѪᡬŮᕰ嵲", a_));
		IL_74:
		this.ᜂ = spr_u21FF.ᜃ();
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x00145C58 File Offset: 0x00144C58
	public int ᜃ()
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
		return this.ᜀ.ᜀ();
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x00145CA0 File Offset: 0x00144CA0
	public string ᜂ()
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
		return this.ᜀ.ᜁ();
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x00145CE8 File Offset: 0x00144CE8
	public spr\u240D ᜈ()
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
		return this.ᜀ;
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x00145D2C File Offset: 0x00144D2C
	public CellFormatType ᜇ()
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
		this.ᜀ();
		return this.ᜁ[0].ᜑ();
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x00145D80 File Offset: 0x00144D80
	public bool ᜉ()
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
		this.ᜀ();
		return this.ᜁ[0].ᜏ();
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x00145DD4 File Offset: 0x00144DD4
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
		this.ᜀ();
		return this.ᜁ[0].ᜎ();
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x00145E28 File Offset: 0x00144E28
	public bool ᜄ()
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
		this.ᜀ();
		return this.ᜁ[0].ᜉ();
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x00145E7C File Offset: 0x00144E7C
	public int ᜅ()
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
		this.ᜀ();
		return this.ᜁ[0].ᜐ();
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x00145ED0 File Offset: 0x00144ED0
	public void ᜀ(RecordArrayList A_0)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3E;
			case 2:
				goto IL_6C;
			case 3:
				if (this.ᜀ == null)
				{
					num = 2;
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
					goto IL_A0;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_3E:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ㡊", a_));
		IL_6C:
		throw new ApplicationException(RecordTableEnumerator.b("社⹀ㅂ⡄♆㵈歊╌⹎≐獒㭔㡖ⵘ筚㽜㩞Ѡൢ䕤๦ݨɪᥬٮၰὲᱴ൶ᱸὺ卼", a_));
		IL_A0:
		if (false)
		{
		}
		A_0.ᜀ(this.ᜀ);
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x00145F90 File Offset: 0x00144F90
	private void ᜀ()
	{
		for (;;)
		{
			if (true)
			{
			}
			if (this.ᜁ == null)
			{
				goto IL_39;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_30;
			}
		}
		IL_30:
		if (false)
		{
		}
		return;
		IL_39:
		this.ᜁ = this.ᜂ.ᜀ(this.ᜂ());
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x00145FF0 File Offset: 0x00144FF0
	public CellFormatType ᜀ(double A_0)
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
		this.ᜀ();
		return this.ᜁ.ᜂ(A_0);
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x00146040 File Offset: 0x00145040
	public CellFormatType ᜀ(string A_0)
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
		this.ᜀ();
		return this.ᜁ.ᜀ(A_0);
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x00146090 File Offset: 0x00145090
	public string ᜂ(double A_0)
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
		return this.ᜀ(A_0, false);
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x001460D4 File Offset: 0x001450D4
	public string ᜀ(double A_0, bool A_1)
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
		this.ᜀ();
		return this.ᜁ.ᜀ(A_0, A_1);
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x00146124 File Offset: 0x00145124
	public string ᜁ(string A_0)
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
		return this.ᜀ(A_0, false);
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x00146168 File Offset: 0x00145168
	public string ᜀ(string A_0, bool A_1)
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
		this.ᜀ();
		return this.ᜁ.ᜀ(A_0, A_1);
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x001461B8 File Offset: 0x001451B8
	internal bool ᜁ(double A_0)
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
		return this.ᜁ.ᜁ(A_0);
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x00146200 File Offset: 0x00145200
	public object ᜀ(object A_0)
	{
		sprᤅ sprᤅ;
		for (;;)
		{
			sprᤅ = (sprᤅ)base.MemberwiseClone();
			sprᤅ.SetParent(A_0);
			sprᤅ.ᜁ();
			sprᤅ.ᜀ = (spr\u240D)spr\u1CD3.ᜀ(this.ᜀ);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ != null)
					{
						num = 2;
						continue;
					}
					goto IL_8B;
				case 1:
					goto IL_8B;
				case 2:
					sprᤅ.ᜁ = (spr\u2575)this.ᜁ.Clone(sprᤅ);
					goto IL_74;
				}
				break;
				IL_74:
				num = 1;
				continue;
				IL_8B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
					goto IL_A1;
				}
			}
		}
		IL_A1:
		if (false)
		{
		}
		if (true)
		{
		}
		return sprᤅ;
	}

	// Token: 0x0400120E RID: 4622
	private spr\u240D ᜀ;

	// Token: 0x0400120F RID: 4623
	private spr\u2575 ᜁ;

	// Token: 0x04001210 RID: 4624
	private sprឥ ᜂ;
}
