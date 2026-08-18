using System;
using System.Collections;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Fields;

// Token: 0x02000156 RID: 342
internal class sprᨉ : sprỽ
{
	// Token: 0x06000960 RID: 2400 RVA: 0x0007E7FC File Offset: 0x0007D7FC
	internal sprᨉ(string A_0)
	{
		this.ᜀ = new spr\u2464(A_0, sprᨉ.ᜃ);
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0007E820 File Offset: 0x0007D820
	internal sprᨉ(Stream A_0)
	{
		this.ᜀ = new spr\u2464(A_0);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0007E840 File Offset: 0x0007D840
	internal sprᨉ(Stream A_0, Document A_1) : this(A_0)
	{
		this.ᜁ = A_1;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0007E85C File Offset: 0x0007D85C
	internal sprᨉ(Stream A_0, Document A_1, sprᣑ A_2) : this(A_0, A_1)
	{
		this.ᜂ = A_2;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0007E878 File Offset: 0x0007D878
	Document sprỽ.ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0007E8BC File Offset: 0x0007D8BC
	spr\u2464 sprỽ.ᜂ()
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

	// Token: 0x06000966 RID: 2406 RVA: 0x0007E900 File Offset: 0x0007D900
	private sprᣑ ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0007E944 File Offset: 0x0007D944
	spr\u1CDF sprỽ.ᜃ(string A_0)
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
		return null;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0007E980 File Offset: 0x0007D980
	byte[] sprỽ.ᜂ(string A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_52;
		}
		if (false)
		{
		}
		string text = this.ᜀ().ᜁ(A_0, false, false);
		if (!this.ᜀ().\u1717().ContainsKey(text))
		{
			byte[] array = this.ᜀ().ᜮ(text);
			sprᠾ sprᠾ = new sprᠾ(array);
			this.ᜁ.Images.ᜀ(sprᠾ);
			this.ᜀ().\u1717().Add(text, sprᠾ.ᜀ());
			return array;
		}
		if (true)
		{
		}
		IL_52:
		sprᠾ sprᠾ2 = this.ᜁ.Images.ᜀ(this.ᜀ().\u1717()[text]);
		return sprᠾ2.ᜃ();
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0007EA48 File Offset: 0x0007DA48
	string sprỽ.ᜁ(string A_0)
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
		return null;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0007EA84 File Offset: 0x0007DA84
	bool sprỽ.ᜄ(string A_0)
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
		return false;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0007EAC0 File Offset: 0x0007DAC0
	void sprỽ.ᜀ(sprᩍ A_0)
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
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0007EAFC File Offset: 0x0007DAFC
	void sprỽ.ᜀ(string A_0, sprᩍ A_1)
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
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0007EB38 File Offset: 0x0007DB38
	sprᩍ sprỽ.ᜀ(string A_0)
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
		return null;
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0007EB74 File Offset: 0x0007DB74
	void sprỽ.ᜁ()
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
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0007EBB0 File Offset: 0x0007DBB0
	void sprỽ.ᜁ(sprᩍ A_0)
	{
		TextBox textBox;
		for (;;)
		{
			textBox = new TextBox(this.ᜁ);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_131;
				case 1:
					textBox.Format.InternalMargin.ᜃ(A_0.ឤ());
					num = 10;
					continue;
				case 2:
					if (A_0.ᝢ() != 3.4028235E+38f)
					{
						num = 4;
						continue;
					}
					goto IL_C3;
				case 3:
					if (A_0.ឤ() != 3.4028235E+38f)
					{
						goto IL_DB;
					}
					goto IL_8C;
				case 4:
					textBox.Format.InternalMargin.ᜂ(A_0.ᝢ());
					num = 5;
					continue;
				case 5:
					goto IL_C3;
				case 6:
					textBox.Format.InternalMargin.ᜀ(A_0.ហ());
					num = 7;
					continue;
				case 7:
					goto IL_109;
				case 8:
					if (true)
					{
					}
					if (A_0.ដ() != 3.4028235E+38f)
					{
						num = 11;
						continue;
					}
					goto IL_131;
				case 9:
					if (A_0.ហ() != 3.4028235E+38f)
					{
						num = 6;
						continue;
					}
					goto IL_198;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						goto IL_8C;
					}
					break;
				case 11:
					textBox.Format.InternalMargin.ᜁ(A_0.ដ());
					num = 0;
					continue;
				}
				break;
				IL_8C:
				num = 8;
				continue;
				IL_C3:
				num = 3;
				continue;
				IL_DB:
				num = 1;
				continue;
				IL_131:
				num = 9;
			}
		}
		IL_109:
		IL_198:
		this.ᜀ().ᜐ(this.ᜀ.ᜠ(), textBox);
		(A_0 as spr\u248F).ᜎ().Add(textBox);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0007ED80 File Offset: 0x0007DD80
	void sprỽ.ᜀ(string A_0, spr\u2588 A_1)
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
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0007EDBC File Offset: 0x0007DDBC
	spr\u2588 sprỽ.ᜅ(string A_0)
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
		return null;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0007EDF8 File Offset: 0x0007DDF8
	static sprᨉ()
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᨉ.ᜃ = new Hashtable();
		sprᨉ.ᜃ.Add(ClipboardData.b("Ὠ", a_), ClipboardData.b("ᱨᥪͬ啮ɰၲᵴቶᑸ᩺๼剾뺒ꆚ춠", a_));
		sprᨉ.ᜃ.Add(ClipboardData.b("٨", a_), ClipboardData.b("ᱨᥪͬ啮ɰၲᵴቶᑸ᩺๼剾뺒ꆚ咽잠쪢욤슦鎨쒪쮬즮\ud8b0킲킴", a_));
	}

	// Token: 0x0400137A RID: 4986
	private readonly spr\u2464 ᜀ;

	// Token: 0x0400137B RID: 4987
	private readonly Document ᜁ;

	// Token: 0x0400137C RID: 4988
	private readonly sprᣑ ᜂ;

	// Token: 0x0400137D RID: 4989
	private static readonly Hashtable ᜃ;
}
