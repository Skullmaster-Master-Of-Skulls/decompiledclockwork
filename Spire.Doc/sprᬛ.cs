using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core;
using Spire.Doc.Core.Biff_Records;
using Spire.Doc.Fields.Shape;

// Token: 0x020003F4 RID: 1012
[CLSCompliant(false)]
internal class sprᬛ : spr\u1F8B, spr\u20B1, IDisposable
{
	// Token: 0x06003885 RID: 14469 RVA: 0x0034CF30 File Offset: 0x0034BF30
	public sprᬛ(Stream A_0)
	{
		this.ᜄ = new spr\u1C2D(A_0, false);
		this.\u170D();
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x0034CF6C File Offset: 0x0034BF6C
	public sprᬛ(string A_0)
	{
		this.ᜄ = new spr\u1C2D(A_0, false);
		this.\u170D();
	}

	// Token: 0x06003887 RID: 14471 RVA: 0x0034CFA8 File Offset: 0x0034BFA8
	public void ᜀ(spr\u2214 A_0)
	{
		for (;;)
		{
			spr\u2214 spr_u = this.ᜉ;
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u2214 spr_u2;
					if (spr_u == spr_u2)
					{
						num = 1;
						continue;
					}
					goto IL_2D;
				}
				case 1:
					return;
				case 2:
					goto IL_2D;
				}
				break;
				IL_2D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					spr\u2214 spr_u2 = spr_u;
					spr\u2214 value = (spr\u2214)Delegate.Combine(spr_u2, A_0);
					spr_u = Interlocked.CompareExchange<spr\u2214>(ref this.ᜉ, value, spr_u2);
					num = 0;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06003888 RID: 14472 RVA: 0x0034D040 File Offset: 0x0034C040
	public void ᜁ(spr\u2214 A_0)
	{
		for (;;)
		{
			spr\u2214 spr_u = this.ᜉ;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_2D;
				case 1:
				{
					spr\u2214 spr_u2;
					if (spr_u == spr_u2)
					{
						num = 2;
						continue;
					}
					goto IL_2D;
				}
				case 2:
					return;
				}
				break;
				IL_2D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					spr\u2214 spr_u2 = spr_u;
					spr\u2214 value = (spr\u2214)Delegate.Remove(spr_u2, A_0);
					spr_u = Interlocked.CompareExchange<spr\u2214>(ref this.ᜉ, value, spr_u2);
					num = 1;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06003889 RID: 14473 RVA: 0x0034D0D8 File Offset: 0x0034C0D8
	public spr\u202E \u1713()
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
		return this.ᜅ.ᜃ().ᜆ();
	}

	// Token: 0x0600388A RID: 14474 RVA: 0x0034D124 File Offset: 0x0034C124
	public spr\u2612 ᜐ()
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
		return (spr\u2612)this.ᜊ;
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x0034D16C File Offset: 0x0034C16C
	public new int ᜋ()
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
		return this.ᜐ().ᜁ() + 1;
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x0034D1B4 File Offset: 0x0034C1B4
	public sprᶍ \u1715()
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

	// Token: 0x0600388D RID: 14477 RVA: 0x0034D1F8 File Offset: 0x0034C1F8
	public BuiltinDocumentProperties ᜠ()
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
		return this.ᜆ;
	}

	// Token: 0x0600388E RID: 14478 RVA: 0x0034D23C File Offset: 0x0034C23C
	public CustomDocumentProperties \u1717()
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

	// Token: 0x0600388F RID: 14479 RVA: 0x0034D280 File Offset: 0x0034C280
	public new MemoryStream ᜊ()
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
		return this.ᜄ.\u1715();
	}

	// Token: 0x06003890 RID: 14480 RVA: 0x0034D2C8 File Offset: 0x0034C2C8
	internal DigitalSignatures ᜡ()
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
		return this.ᜄ.\u1719();
	}

	// Token: 0x06003891 RID: 14481 RVA: 0x0034D310 File Offset: 0x0034C310
	public byte[] ᜏ()
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
		return this.ᜅ.ᜃ().ᜅ();
	}

	// Token: 0x06003892 RID: 14482 RVA: 0x0034D35C File Offset: 0x0034C35C
	public void ᜁ(byte[] A_0)
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
		this.ᜅ.ᜃ().ᜁ(A_0);
	}

	// Token: 0x06003893 RID: 14483 RVA: 0x0034D3A8 File Offset: 0x0034C3A8
	public byte[] \u1719()
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
		return this.ᜅ.ᜃ().ᜌ();
	}

	// Token: 0x06003894 RID: 14484 RVA: 0x0034D3F4 File Offset: 0x0034C3F4
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
		this.ᜅ.ᜃ().ᜀ(A_0);
	}

	// Token: 0x06003895 RID: 14485 RVA: 0x0034D440 File Offset: 0x0034C440
	public MemoryStream \u171C()
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
		return this.ᜄ.ᜊ();
	}

	// Token: 0x06003896 RID: 14486 RVA: 0x0034D488 File Offset: 0x0034C488
	public sprᥚ \u171F()
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
		return this.ᜅ.ᜃ().\u171A();
	}

	// Token: 0x06003897 RID: 14487 RVA: 0x0034D4D4 File Offset: 0x0034C4D4
	public bool \u171B()
	{
		int length;
		for (;;)
		{
			length = base.\u1736().Length;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					length = base.\u1736().TrimStart(new char[]
					{
						' '
					}).Length;
					if (true)
					{
					}
					num = 2;
					continue;
				case 1:
					if (!(base.\u1736().TrimStart(new char[]
					{
						' '
					}) != string.Empty))
					{
						goto IL_AF;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AF;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_AD;
				}
				break;
			}
		}
		IL_AD:
		IL_AF:
		return this.ᜅ.ᜃ().ᜡ().ᜆ(base.ᜫ() - length);
	}

	// Token: 0x06003898 RID: 14488 RVA: 0x0034D5B0 File Offset: 0x0034C5B0
	public new bool ᜈ()
	{
		int length;
		for (;;)
		{
			length = base.\u1736().Length;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					length = base.\u1736().TrimStart(new char[]
					{
						' '
					}).Length;
					num = 1;
					continue;
				case 1:
					goto IL_AD;
				case 2:
					if (true)
					{
					}
					if (!(base.\u1736().TrimStart(new char[]
					{
						' '
					}) != string.Empty))
					{
						goto IL_AF;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AF;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_AD:
		IL_AF:
		return this.ᜅ.ᜃ().\u1719().ᜆ(base.ᜫ() - length);
	}

	// Token: 0x06003899 RID: 14489 RVA: 0x0034D68C File Offset: 0x0034C68C
	public new string ᜉ()
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
		return this.ᜅ.ᜃ().ᜁ();
	}

	// Token: 0x0600389A RID: 14490 RVA: 0x0034D6D8 File Offset: 0x0034C6D8
	public string \u1718()
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
		return this.ᜅ.ᜃ().\u170D();
	}

	// Token: 0x0600389B RID: 14491 RVA: 0x0034D724 File Offset: 0x0034C724
	public string ᜢ()
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
		return this.ᜅ.ᜃ().\u171F();
	}

	// Token: 0x0600389C RID: 14492 RVA: 0x0034D770 File Offset: 0x0034C770
	public string \u171A()
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
		return this.ᜅ.ᜃ().ᜑ();
	}

	// Token: 0x0600389D RID: 14493 RVA: 0x0034D7BC File Offset: 0x0034C7BC
	public bool \u1712()
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
		return this.ᜅ.ᜀ().\u17CD();
	}

	// Token: 0x0600389E RID: 14494 RVA: 0x0034D808 File Offset: 0x0034C808
	internal byte[] \u171E()
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
		return this.ᜅ.ᜃ().\u171B();
	}

	// Token: 0x0600389F RID: 14495 RVA: 0x0034D854 File Offset: 0x0034C854
	internal void ᜂ(byte[] A_0)
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
		this.ᜅ.ᜃ().ᜂ(A_0);
	}

	// Token: 0x060038A0 RID: 14496 RVA: 0x0034D8A0 File Offset: 0x0034C8A0
	public spr\u1DAC \u1716()
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
		return this.ᜊ;
	}

	// Token: 0x060038A1 RID: 14497 RVA: 0x0034D8E4 File Offset: 0x0034C8E4
	public void ᜀ(spr\u1DAC A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x060038A2 RID: 14498 RVA: 0x0034D928 File Offset: 0x0034C928
	public sprῳ ᜀ(WordSubdocument A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_15E;
				case 2:
					switch (A_0)
					{
					case WordSubdocument.Footnote:
						goto IL_142;
					case WordSubdocument.HeaderFooter:
						goto IL_65;
					case WordSubdocument.Endnote:
						goto IL_53;
					case WordSubdocument.Annotation:
						goto IL_111;
					case WordSubdocument.TextBox:
						goto IL_E3;
					case WordSubdocument.HeaderTextBox:
						goto IL_121;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_142;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					break;
				case 4:
					goto IL_4E;
				}
				if (!this.ᜄ)
				{
					num = 4;
				}
				else
				{
					num = 2;
				}
			}
			IL_4E:
			if (true)
			{
			}
			throw new InvalidOperationException(ClipboardData.b("㑶ᡸ᝺ᅼ彾펀춈搜ﲐﮔ톘ﺚﲜﮞ쒠톢趤躦覨즪좬즮\udeb0솲킴鞶춸펺풼첾껂ꃄ돆ꇈ꓊꧌", a_));
			IL_53:
			return this.ᜅ = new spr\u1F4F(this);
			IL_65:
			this.ᜀ(new spr\u1DAC(this));
			this.ᜅ = this.\u1716();
			return this.ᜅ;
			IL_E3:
			return this.ᜅ = new sprᲅ(this);
			IL_111:
			return this.ᜅ = new sprᜩ(this);
			IL_121:
			spr\u226D spr_u226D = new spr\u226D(this);
			spr_u226D.ᜀ(this.\u1716());
			this.ᜅ = spr_u226D;
			return this.ᜅ;
			IL_142:
			return this.ᜅ = new sprᤜ(this);
			IL_15E:
			return null;
		}
		}
	}

	// Token: 0x060038A3 RID: 14499 RVA: 0x0034DA9C File Offset: 0x0034CA9C
	public void ᜀ(Document A_0)
	{
		int a_ = 19;
		int num = 10;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				sprᥬ sprᥬ;
				sprᥬ.ᜆ();
				this.ᜄ.ᜀ(sprᥬ.ᜇ(), sprᥬ.ᜄ(), sprᥬ.ᜅ());
				num = 3;
				continue;
			}
			case 1:
				goto IL_2CF;
			case 2:
				base.ᜀ(new spr\u24E3(this.ᜄ.ᜅ(), this.ᜄ.ᜃ(), this.ᜅ.ᜀ().ᝣ(), this.ᜅ.ᜀ().\u1779(), A_0));
				num = 8;
				continue;
			case 3:
				goto IL_175;
			case 4:
				num = 5;
				continue;
			case 5:
			{
				if (true)
				{
				}
				if (this.ᜉ == null)
				{
					goto IL_2C4;
				}
				text = this.ᜉ();
				sprᥬ sprᥬ = new sprᥬ(this.ᜄ.ᜅ(), this.ᜄ.ᜃ(), this.ᜄ.\u1714(), this.ᜅ.ᜀ());
				bool flag = sprᥬ.ᜅ(text);
				num = 6;
				continue;
			}
			case 6:
			{
				bool flag;
				if (flag)
				{
					num = 0;
					continue;
				}
				goto IL_6B;
			}
			case 7:
				if (this.ᜅ.ᜀ().\u17CD())
				{
					num = 4;
					continue;
				}
				goto IL_175;
			case 8:
				goto IL_2D1;
			case 9:
				if (this.ᜅ.ᜀ().\u1779() != 0)
				{
					num = 2;
					continue;
				}
				goto IL_2D1;
			case 11:
				goto IL_66;
			}
			if (this.ᜄ)
			{
				num = 11;
				continue;
			}
			this.ᜄ = true;
			this.ᜅ.ᜀ().ᜁ(this.ᜄ.ᜃ());
			this.ᜅ.ᜀ().ឰ();
			this.ᜄ.ᜃ(this.ᜅ.ᜀ().\u17C1());
			num = 7;
			continue;
			IL_175:
			this.ᜅ.ᜃ().ᜈ(this.ᜄ.ᜅ());
			base.\u173E();
			this.ᜁ();
			this.ᜅ.ᜄ().ᜀ(this.ᜄ.ᜃ());
			base.ᜤ();
			base.ᝁ();
			this.ᜂ();
			this.ᜄ();
			num = 9;
			continue;
			IL_2C4:
			num = 1;
			continue;
			IL_2D1:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2C4;
			default:
				goto IL_2E7;
			}
		}
		IL_66:
		throw new InvalidOperationException(ClipboardData.b("㵸๺ർ፾ꮊ릘ﶚ쾠잢蒤", a_));
		IL_6B:
		throw new Exception(ClipboardData.b("⥸᩺๼౾ꦈꦊ", a_) + text + ClipboardData.b("學孺ᑼ౾ꆀ力ﾌ뒔", a_));
		IL_2CF:
		throw new ArgumentException(ClipboardData.b("㵸ᑺṼ੾ꦈﺌ꾎ﶒ爵얠辢薤\ud9a8캪쎬辮얰\udbb2킴鞶즸\udaba캼첾뛀곂럄ꏆꗊ꣌꫎뗐ꃒꟖ룘꣚껜꣞軠釢臤짦", a_));
		IL_2E7:
		if (false)
		{
		}
		this.ᜊ.ᜂ();
		this.ᜄ.ᜃ().Position = (long)this.ᜊ.ᜇ();
		this.ᜅ();
	}

	// Token: 0x060038A4 RID: 14500 RVA: 0x0034DDC4 File Offset: 0x0034CDC4
	private new void ᜅ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				IEnumerator<KeyValuePair<int, sprᨼ>> enumerator;
				switch (num)
				{
				case 0:
					if (this.ᜅ.ᜃ().\u1712() != null)
					{
						num = 5;
						continue;
					}
					return;
				case 1:
					goto IL_218;
				case 3:
					if (this.ᜅ.ᜃ().\u1712().ᜈ() != null)
					{
						num = 1;
						continue;
					}
					return;
				case 4:
					num = 0;
					continue;
				case 5:
					num = 3;
					continue;
				case 6:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 1:
							{
								long num2;
								if (base.ᜂ(num2 - 2L) == WordChunkType.Shape)
								{
									num = 8;
									continue;
								}
								break;
							}
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 6;
									continue;
								}
								KeyValuePair<int, sprᨼ> keyValuePair = enumerator.Current;
								int key = keyValuePair.Key;
								long num2 = base.ᜀ(key, this.ᜐ().ᜇ(), 1) - 2L;
								num = 3;
								continue;
							}
							case 3:
							{
								long num2;
								if (base.ᜂ(num2) != WordChunkType.Shape)
								{
									num = 5;
									continue;
								}
								break;
							}
							case 5:
								num = 1;
								continue;
							case 6:
								num = 7;
								continue;
							case 7:
								goto IL_1B7;
							case 8:
							{
								int key;
								sprᨼ value = this.ᜅ.ᜃ().\u1712().ᜈ()[key];
								this.ᜅ.ᜃ().\u1712().ᜈ().Remove(key);
								this.ᜅ.ᜃ().\u1712().ᜈ().Add(key - 1, value);
								num = 0;
								continue;
							}
							}
							IL_10D:
							num = 2;
							continue;
							goto IL_10D;
						}
						IL_1B7:
						return;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_1F9;
							case 2:
								enumerator.Dispose();
								num = 1;
								continue;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 2;
						}
						IL_1F9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F9;
						default:
							if (false)
							{
							}
							break;
						}
					}
					goto IL_218;
				}
				if (this.ᜅ.ᜃ() != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_218:
				SortedItemList<int, sprᨼ> sortedItemList = (SortedItemList<int, sprᨼ>)this.ᜅ.ᜃ().\u1712().ᜈ().CloneAll();
				enumerator = sortedItemList.GetEnumerator();
				num = 6;
			}
			return;
		}
		}
	}

	// Token: 0x060038A5 RID: 14501 RVA: 0x0034E0A4 File Offset: 0x0034D0A4
	private new void ᜄ()
	{
		int a_ = 3;
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				sprᵷ sprᵷ = null;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (sprᵷ != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						goto IL_1C1;
					case 2:
						return;
					case 3:
						try
						{
							int num2;
							for (;;)
							{
								num2 = spr\u2443.ᜀ(this.ᜄ.ᜎ().ᜋ(), 0U, out sprᵷ);
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (num2 == 0)
										{
											Guid a_2 = new Guid(ClipboardData.b("⽨奪呬⥮䥰䙲ぴ䝶呸佺㭼㥾뢀꺂뒄랆뾈뎊ꂌ캎펐ꪒ꒔몖ꦘꎚ궜꾞鎠鞤邦颪隮", a_));
											Guid a_3 = new Guid(ClipboardData.b("⵨幪⹬⭮㕰䙲䕴䕶呸䥺㡼䙾슀꺂뒄랆뢈즊ꂌ뚎ꊐꪒꊔ몖ꦘꎚ궜꾞鎠鞤銪", a_));
											Guid a_4 = new Guid(ClipboardData.b("⵨幪⹬⭮㕰䙲䕴䉶呸䥺㡼䙾슀꺂뒄랆뢈즊ꂌ뚎ꊐꪒꊔ몖ꦘꎚ궜꾞鎠鞤銪", a_));
											this.ᜆ = new BuiltinDocumentProperties();
											this.ᜇ = new CustomDocumentProperties();
											this.ᜀ(sprᵷ, a_2, this.ᜆ.SummaryHash, Spire.Doc.PropertyType.Summary);
											this.ᜀ(sprᵷ, a_3, this.ᜆ.DocumentHash, Spire.Doc.PropertyType.DocumentSummary);
											this.ᜀ(sprᵷ, a_4, this.ᜇ.CustomHash, Spire.Doc.PropertyType.Custom);
											num = 1;
											continue;
										}
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_180;
										default:
											if (false)
											{
											}
											num = 2;
											continue;
										}
										break;
									case 1:
										goto IL_180;
									case 2:
										goto IL_CB;
									}
									break;
								}
							}
							IL_CB:
							throw new ExternalException(ClipboardData.b("⩨੪ͬŮṰݲ啴ᑶ୸Ṻᱼ୾ꎂ횄力뎒햠쪢삤풦覨\ud8aa\ud9ac\uddae풰튲\ud8b4", a_), num2);
							IL_180:
							goto IL_45;
						}
						catch
						{
							this.ᜆ.DocumentHash.Clear();
							this.ᜆ.SummaryHash.Clear();
							this.ᜇ.CustomHash.Clear();
							this.ᜃ();
							goto IL_45;
						}
						goto IL_1C1;
						IL_45:
						num = 0;
						continue;
					}
					break;
					IL_1C1:
					Marshal.ReleaseComObject(sprᵷ);
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x060038A6 RID: 14502 RVA: 0x0034E2A4 File Offset: 0x0034D2A4
	private void ᜀ(sprᵷ A_0, Guid A_1, IDictionary A_2, Spire.Doc.PropertyType A_3)
	{
		switch (0)
		{
		default:
		{
			sprᡮ sprᡮ = null;
			spr\u206A spr_u206A = null;
			try
			{
				for (;;)
				{
					IL_DE:
					int num = A_0.ᜀ(ref A_1, STGM.STGM_SHARE_EXCLUSIVE, out sprᡮ);
					for (;;)
					{
						IL_EB:
						int num2 = 28;
						for (;;)
						{
							string text;
							int num3;
							spr\u23A4 a_;
							object obj;
							DocumentProperty documentProperty;
							int num4;
							spr\u196F[] array;
							switch (num2)
							{
							case 0:
								goto IL_2B1;
							case 1:
								goto IL_388;
							case 2:
								if (text == null)
								{
									num2 = 11;
									continue;
								}
								goto IL_58B;
							case 3:
								if (num3 == 10)
								{
									num2 = 27;
									continue;
								}
								goto IL_44D;
							case 4:
								goto IL_388;
							case 5:
								goto IL_2B1;
							case 6:
								goto IL_388;
							case 7:
								if (a_.ᜀ != 65)
								{
									num2 = 19;
									continue;
								}
								goto IL_388;
							case 8:
								Marshal.ThrowExceptionForHR(num);
								num2 = 34;
								continue;
							case 9:
								if (A_3 == Spire.Doc.PropertyType.Summary)
								{
									num2 = 13;
									continue;
								}
								goto IL_58B;
							case 10:
								goto IL_388;
							case 11:
								documentProperty = new DocumentProperty(this.ᜁ(num3), obj);
								num2 = 38;
								continue;
							case 12:
								goto IL_1C4;
							case 13:
								num2 = 2;
								continue;
							case 14:
								if (documentProperty.PropertyId != BuiltInProperty.Category)
								{
									num2 = 21;
									continue;
								}
								goto IL_28B;
							case 15:
							{
								if (num4 == 0)
								{
									num2 = 25;
									continue;
								}
								num3 = (int)array[0].ᜁ;
								spr\u1DD1[] array2 = new spr\u1DD1[1];
								array2[0].ᜀ = (IntPtr)1L;
								array2[0].ᜁ = (IntPtr)num3;
								spr\u23A4[] array3 = new spr\u23A4[1];
								sprᡮ.ᜀ(1U, array2, array3);
								text = array[0].ᜀ;
								obj = null;
								a_ = array3[0];
								num2 = 7;
								continue;
							}
							case 16:
								num2 = 41;
								continue;
							case 17:
								if (documentProperty.PropertyId != BuiltInProperty.Company)
								{
									num2 = 36;
									continue;
								}
								goto IL_28B;
							case 18:
								goto IL_121;
							case 19:
								num2 = 3;
								continue;
							case 20:
								if (A_3 == Spire.Doc.PropertyType.Summary)
								{
									num2 = 29;
									continue;
								}
								num2 = 17;
								continue;
							case 21:
								num2 = 26;
								continue;
							case 22:
								A_2.Add(text, documentProperty);
								num2 = 1;
								continue;
							case 23:
								goto IL_1C4;
							case 24:
								if (A_3 != Spire.Doc.PropertyType.Custom)
								{
									num2 = 42;
									continue;
								}
								goto IL_44D;
							case 25:
								num2 = 18;
								continue;
							case 26:
								if (documentProperty.PropertyId == BuiltInProperty.Manager)
								{
									num2 = 37;
									continue;
								}
								A_2.Add(num3, documentProperty);
								num2 = 6;
								continue;
							case 27:
								num2 = 24;
								continue;
							case 28:
								if ((long)num == (long)((ulong)-2147287038))
								{
									num2 = 30;
									continue;
								}
								num2 = 33;
								continue;
							case 29:
								A_2.Add((int)this.ᜁ(num3), documentProperty);
								num2 = 10;
								continue;
							case 30:
								num2 = 43;
								continue;
							case 31:
								num2 = 40;
								continue;
							case 32:
								documentProperty = new DocumentProperty(this.ᜀ(num3), obj);
								num2 = 12;
								continue;
							case 33:
							{
								if (num != 0)
								{
									num2 = 8;
									continue;
								}
								spr\u1DD1[] array4 = new spr\u1DD1[1];
								array4[0].ᜀ = (IntPtr)1L;
								array4[0].ᜁ = (IntPtr)1;
								spr\u23A4[] array5 = new spr\u23A4[1];
								sprᡮ.ᜀ(1U, array4, array5);
								a_ = array5[0];
								this.ᜈ = Encoding.GetEncoding(a_.ᜃ);
								sprᡮ.ᜀ(out spr_u206A);
								num2 = 39;
								continue;
							}
							case 34:
								goto IL_4DA;
							case 35:
								if (obj != null)
								{
									num2 = 31;
									continue;
								}
								goto IL_388;
							case 36:
								num2 = 14;
								continue;
							case 37:
								goto IL_28B;
							case 38:
								goto IL_1C4;
							case 39:
								goto IL_388;
							case 40:
								if (A_3 == Spire.Doc.PropertyType.Custom)
								{
									num2 = 22;
									continue;
								}
								num2 = 20;
								continue;
							case 41:
								if (text == null)
								{
									num2 = 32;
									continue;
								}
								goto IL_51C;
							case 42:
								obj = TimeSpan.FromTicks(a_.ᜅ);
								num2 = 5;
								continue;
							case 43:
								goto IL_2E5;
							case 44:
								if (A_3 == Spire.Doc.PropertyType.DocumentSummary)
								{
									num2 = 16;
									continue;
								}
								goto IL_51C;
							}
							goto IL_DE;
							IL_1C4:
							num2 = 35;
							continue;
							IL_28B:
							A_2.Add((int)this.ᜀ(num3), documentProperty);
							num2 = 4;
							continue;
							IL_2B1:
							documentProperty = null;
							num2 = 44;
							continue;
							IL_388:
							array = new spr\u196F[1];
							spr_u206A.ᜀ(1, array, out num4);
							num2 = 15;
							continue;
							IL_44D:
							obj = this.ᜀ(a_);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_EB;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							IL_51C:
							num2 = 9;
							continue;
							IL_58B:
							documentProperty = new DocumentProperty(text, obj, DocumentProperty.ᜀ(obj));
							num2 = 23;
						}
					}
				}
				IL_121:
				IL_2E5:
				IL_4DA:;
			}
			catch
			{
				this.ᜆ.DocumentHash.Clear();
				this.ᜆ.SummaryHash.Clear();
				this.ᜇ.CustomHash.Clear();
				this.ᜃ();
			}
			finally
			{
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						Marshal.ReleaseComObject(sprᡮ);
						num2 = 4;
						continue;
					case 1:
						goto IL_682;
					case 3:
						Marshal.ReleaseComObject(spr_u206A);
						num2 = 1;
						continue;
					case 4:
						goto IL_649;
					case 5:
						if (spr_u206A != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_684;
					}
					if (true)
					{
					}
					if (sprᡮ != null)
					{
						num2 = 0;
						continue;
					}
					IL_649:
					num2 = 5;
				}
				IL_682:
				IL_684:;
			}
			return;
		}
		}
	}

	// Token: 0x060038A7 RID: 14503 RVA: 0x0034E96C File Offset: 0x0034D96C
	private BuiltInProperty ᜁ(int A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						return (BuiltInProperty)A_0;
					case 2:
						switch (A_0)
						{
						case 2:
							return BuiltInProperty.Title;
						case 3:
							return BuiltInProperty.Subject;
						case 4:
							return BuiltInProperty.Author;
						case 5:
							return BuiltInProperty.Keywords;
						case 6:
							return BuiltInProperty.Comments;
						case 7:
							return BuiltInProperty.Template;
						case 8:
							return BuiltInProperty.LastAuthor;
						case 9:
							goto IL_78;
						case 10:
							return BuiltInProperty.EditTime;
						case 11:
							return BuiltInProperty.LastPrinted;
						case 12:
							return BuiltInProperty.CreationDate;
						case 13:
							return BuiltInProperty.LastSaveDate;
						case 14:
							goto IL_A1;
						case 15:
							return BuiltInProperty.WordCount;
						case 16:
							return BuiltInProperty.CharCount;
						case 17:
							return BuiltInProperty.Thumbnail;
						case 18:
							return BuiltInProperty.ApplicationName;
						case 19:
							return BuiltInProperty.Security;
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_78:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8E;
			}
		}
		IL_8E:
		if (false)
		{
		}
		return BuiltInProperty.RevisionNumber;
		IL_A1:
		if (true)
		{
		}
		return BuiltInProperty.PageCount;
	}

	// Token: 0x060038A8 RID: 14504 RVA: 0x0034EA5C File Offset: 0x0034DA5C
	private BuiltInProperty ᜀ(int A_0)
	{
		for (;;)
		{
			IL_30:
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return BuiltInProperty.Manager;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						switch (A_0)
						{
						case 2:
							return BuiltInProperty.Category;
						case 3:
							return (BuiltInProperty)A_0;
						case 4:
							return BuiltInProperty.ByteCount;
						case 5:
							return BuiltInProperty.LineCount;
						case 6:
							return BuiltInProperty.ParagraphCount;
						case 7:
							return BuiltInProperty.SlideCount;
						case 8:
							return BuiltInProperty.NoteCount;
						case 9:
							return BuiltInProperty.HiddenCount;
						case 10:
							return BuiltInProperty.MultimediaClipCount;
						case 11:
							return BuiltInProperty.ScaleCrop;
						case 12:
							goto IL_AD;
						case 13:
							return BuiltInProperty.DocParts;
						case 14:
							return BuiltInProperty.Manager;
						case 15:
							return BuiltInProperty.Company;
						case 16:
							return BuiltInProperty.LinksDirty;
						case 17:
							return BuiltInProperty.CharCount;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_F1;
					}
					goto IL_30;
				}
			}
		}
		return BuiltInProperty.DocParts;
		IL_AD:
		if (true)
		{
		}
		return BuiltInProperty.HeadingPair;
		IL_F1:
		return (BuiltInProperty)A_0;
	}

	// Token: 0x060038A9 RID: 14505 RVA: 0x0034EB70 File Offset: 0x0034DB70
	private object ᜀ(spr\u23A4 A_0)
	{
		switch (0)
		{
		default:
		{
			int num2;
			byte[] array;
			IntPtr source;
			for (;;)
			{
				VarEnum varEnum = (VarEnum)A_0.ᜀ;
				int num = 20;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						VarEnum varEnum2 = varEnum;
						num = 17;
						continue;
					}
					case 1:
					{
						VarEnum varEnum2;
						if (varEnum2 != VarEnum.VT_BOOL)
						{
							num = 24;
							continue;
						}
						goto IL_1A6;
					}
					case 2:
						num = 16;
						continue;
					case 3:
						num = 4;
						continue;
					case 4:
					{
						VarEnum varEnum3;
						if (varEnum3 != VarEnum.VT_INT)
						{
							num = 13;
							continue;
						}
						goto IL_348;
					}
					case 5:
					{
						VarEnum varEnum3;
						switch (varEnum3)
						{
						case VarEnum.VT_LPSTR:
							num2 = 0;
							num = 23;
							continue;
						case VarEnum.VT_LPWSTR:
							goto IL_268;
						default:
							num = 15;
							continue;
						}
						break;
					}
					case 6:
					{
						VarEnum varEnum3;
						if (varEnum3 <= VarEnum.VT_LPWSTR)
						{
							num = 3;
							continue;
						}
						num = 14;
						continue;
					}
					case 7:
						num = 1;
						continue;
					case 8:
						goto IL_3BD;
					case 9:
						goto IL_1DB;
					case 10:
						goto IL_16C;
					case 11:
						goto IL_3E7;
					case 12:
						goto IL_343;
					case 13:
						num = 5;
						continue;
					case 14:
					{
						VarEnum varEnum3;
						switch (varEnum3)
						{
						case VarEnum.VT_FILETIME:
							goto IL_256;
						case VarEnum.VT_BLOB:
						{
							int num3 = A_0.ᜃ;
							array = new byte[num3];
							source = 0;
							num = 21;
							continue;
						}
						default:
							num = 25;
							continue;
						}
						break;
					}
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_337;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 16:
						goto IL_3BD;
					case 17:
					{
						VarEnum varEnum2;
						switch (varEnum2)
						{
						case VarEnum.VT_I2:
							goto IL_116;
						case VarEnum.VT_I4:
							goto IL_226;
						case VarEnum.VT_R4:
							goto IL_CF;
						case VarEnum.VT_R8:
							goto IL_219;
						default:
							num = 7;
							continue;
						}
						break;
					}
					case 18:
						goto IL_1C8;
					case 19:
						goto IL_384;
					case 20:
					{
						if (varEnum <= VarEnum.VT_BOOL)
						{
							num = 0;
							continue;
						}
						VarEnum varEnum3 = varEnum;
						num = 6;
						continue;
					}
					case 21:
					{
						if (IntPtr.Size == 4)
						{
							if (true)
							{
							}
							num = 26;
							continue;
						}
						long num4 = (long)A_0.ᜈ;
						source = (IntPtr)(num4 >> 32);
						num = 10;
						continue;
					}
					case 22:
						if (Marshal.ReadByte(A_0.ᜁ, num2) == 0)
						{
							num = 11;
							continue;
						}
						num2++;
						num = 8;
						continue;
					case 23:
						if (A_0.ᜁ != IntPtr.Zero)
						{
							num = 2;
							continue;
						}
						goto IL_B8;
					case 24:
						goto IL_337;
					case 25:
						num = 28;
						continue;
					case 26:
						source = A_0.ᜈ;
						num = 18;
						continue;
					case 27:
						num = 19;
						continue;
					case 28:
					{
						VarEnum varEnum3;
						if (varEnum3 != VarEnum.VT_CF)
						{
							num = 27;
							continue;
						}
						goto IL_1CD;
					}
					}
					break;
					IL_337:
					num = 12;
					continue;
					IL_3BD:
					num = 22;
				}
			}
			IL_B8:
			return string.Empty;
			IL_CF:
			return null;
			IL_116:
			return A_0.ᜇ;
			IL_16C:
			goto IL_3AC;
			IL_1A6:
			return A_0.ᜄ;
			IL_1C8:
			goto IL_3AC;
			IL_1CD:
			return null;
			IL_1DB:
			goto IL_3EC;
			IL_219:
			return A_0.ᜆ;
			IL_226:
			return A_0.ᜃ;
			IL_256:
			return DateTime.FromFileTime(A_0.ᜅ);
			IL_268:
			return Marshal.PtrToStringUni(A_0.ᜁ);
			IL_343:
			goto IL_3EC;
			IL_348:
			return A_0.ᜃ;
			IL_384:
			goto IL_3EC;
			IL_3AC:
			Marshal.Copy(source, array, 0, array.Length);
			return array;
			IL_3E7:
			byte[] array2 = new byte[num2];
			Marshal.Copy(A_0.ᜁ, array2, 0, num2);
			return this.ᜈ.GetString(array2);
			IL_3EC:
			return null;
		}
		}
	}

	// Token: 0x060038AA RID: 14506 RVA: 0x0034EF6C File Offset: 0x0034DF6C
	private void ᜃ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_10E:
			sprᰇ a_ = new sprᰇ(this.ᜄ.\u171B());
			this.ᜀ(a_);
			num = 2;
			break;
		}
		default:
			if (false)
			{
			}
			goto IL_56;
		}
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_10E;
			case 1:
				if (this.ᜄ.\u1717().Length > 0L)
				{
					num = 7;
					continue;
				}
				return;
			case 2:
				goto IL_A9;
			case 3:
				if (this.ᜄ.\u171B() != null)
				{
					num = 6;
					continue;
				}
				goto IL_A9;
			case 4:
				num = 1;
				continue;
			case 5:
				if (this.ᜄ.\u1717() != null)
				{
					num = 4;
					continue;
				}
				return;
			case 6:
				num = 8;
				continue;
			case 7:
			{
				sprᰇ a_2 = new sprᰇ(this.ᜄ.\u1717());
				this.ᜀ(a_2);
				num = 9;
				continue;
			}
			case 8:
				if (this.ᜄ.\u171B().Length > 0L)
				{
					num = 0;
					continue;
				}
				goto IL_A9;
			case 9:
				return;
			}
			goto IL_56;
			IL_A9:
			if (true)
			{
			}
			this.ᜄ.ᜐ();
			num = 5;
		}
		return;
		IL_56:
		this.ᜄ.ᜇ();
		num = 3;
		goto IL_28;
	}

	// Token: 0x060038AB RID: 14507 RVA: 0x0034F0D8 File Offset: 0x0034E0D8
	private void ᜀ(sprᰇ A_0)
	{
		int a_ = 1;
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				Guid b = new Guid(ClipboardData.b("Ⅶ孨剪⭬坮䑰㙲䕴婶䵸㵺㭼䙾검늂떄놆놈Ꚋ첌춎ꢐꊒ뢔Ꞗꆘꮚ궜궞醢銤骨钬", a_));
				Guid b2 = new Guid(ClipboardData.b("⍦屨⡪⥬⭮䑰䍲䝴婶䭸㹺䑼㱾검늂떄뚆쮈Ꚋ뒌벎ꢐ꒒뢔Ꞗꆘꮚ궜궞醢邨", a_));
				Guid b3 = new Guid(ClipboardData.b("⍦屨⡪⥬⭮䑰䍲䁴婶䭸㹺䑼㱾검늂떄뚆쮈Ꚋ뒌벎ꢐ꒒뢔Ꞗꆘꮚ궜궞醢邨", a_));
				List<sprᮇ> list = A_0.ᜀ();
				int num = 0;
				int count = list.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						sprᮇ sprᮇ;
						this.ᜀ(sprᮇ, this.ᜆ.SummaryHash, true, true);
						num2 = 4;
						continue;
					}
					case 1:
					{
						sprᮇ sprᮇ;
						if (sprᮇ.ᜃ() == b3)
						{
							num2 = 3;
							continue;
						}
						goto IL_1B8;
					}
					case 2:
						goto IL_136;
					case 3:
					{
						sprᮇ sprᮇ;
						this.ᜀ(sprᮇ, this.ᜇ.CustomHash, true, false);
						num2 = 10;
						continue;
					}
					case 4:
						goto IL_1B8;
					case 5:
						goto IL_136;
					case 6:
					{
						sprᮇ sprᮇ;
						this.ᜀ(sprᮇ, this.ᜆ.DocumentHash, false, true);
						num2 = 8;
						continue;
					}
					case 7:
					{
						sprᮇ sprᮇ;
						if (sprᮇ.ᜃ() == b)
						{
							num2 = 0;
							continue;
						}
						num2 = 12;
						continue;
					}
					case 8:
						goto IL_1B8;
					case 9:
					{
						if (num >= count)
						{
							num2 = 11;
							continue;
						}
						sprᮇ sprᮇ = list[num];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_210;
						default:
							if (false)
							{
							}
							num2 = 7;
							continue;
						}
						break;
					}
					case 10:
						goto IL_1B8;
					case 11:
						return;
					case 12:
					{
						sprᮇ sprᮇ;
						if (sprᮇ.ᜃ() == b2)
						{
							goto IL_210;
						}
						num2 = 1;
						continue;
					}
					}
					break;
					IL_136:
					num2 = 9;
					continue;
					IL_1B8:
					num++;
					num2 = 5;
					continue;
					IL_210:
					num2 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x060038AC RID: 14508 RVA: 0x0034F304 File Offset: 0x0034E304
	private void ᜀ(sprᮇ A_0, IDictionary A_1, bool A_2, bool A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Dictionary<int, DocumentProperty> dictionary = null;
				int num = 9;
				for (;;)
				{
					DocumentProperty documentProperty;
					int num2;
					int count;
					List<spr\u1ADE> list;
					object obj;
					switch (num)
					{
					case 0:
						num = 15;
						continue;
					case 1:
						goto IL_2B9;
					case 2:
						goto IL_312;
					case 3:
						if (!A_2)
						{
							num = 0;
							continue;
						}
						goto IL_17E;
					case 4:
					{
						spr\u1ADE spr_u1ADE;
						documentProperty = new DocumentProperty(spr_u1ADE, A_2);
						num = 8;
						continue;
					}
					case 5:
						goto IL_2B7;
					case 6:
						if (documentProperty.Value != null)
						{
							num = 17;
							continue;
						}
						goto IL_2B9;
					case 7:
						goto IL_2B9;
					case 8:
						if (!A_3)
						{
							num = 24;
							continue;
						}
						num = 23;
						continue;
					case 9:
						if (!A_3)
						{
							num = 20;
							continue;
						}
						goto IL_312;
					case 10:
					{
						spr\u1ADE spr_u1ADE;
						int key = spr_u1ADE.ᜁ();
						DocumentProperty documentProperty2 = dictionary[key];
						documentProperty2.ᜀ(spr_u1ADE);
						num = 13;
						continue;
					}
					case 11:
					{
						spr\u1ADE spr_u1ADE;
						if (!A_1.Contains(spr_u1ADE.ᜅ()))
						{
							num = 19;
							continue;
						}
						goto IL_17E;
					}
					case 12:
					{
						if (num2 >= count)
						{
							num = 25;
							continue;
						}
						spr\u1ADE spr_u1ADE = list[num2];
						num = 26;
						continue;
					}
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B7;
						default:
							if (false)
							{
							}
							goto IL_2B9;
						}
						break;
					case 14:
					{
						spr\u1ADE spr_u1ADE;
						dictionary.Add(spr_u1ADE.ᜅ(), documentProperty);
						num = 5;
						continue;
					}
					case 15:
						if (A_3)
						{
							num = 28;
							continue;
						}
						goto IL_17E;
					case 16:
						obj = documentProperty.Name;
						goto IL_201;
					case 17:
						num = 3;
						continue;
					case 18:
					{
						spr\u1ADE spr_u1ADE;
						if (!(spr_u1ADE.ᜅ is ClipboardData))
						{
							num = 4;
							continue;
						}
						goto IL_2B9;
					}
					case 19:
					{
						spr\u1ADE spr_u1ADE;
						A_1.Add(spr_u1ADE.ᜅ(), documentProperty);
						num = 1;
						continue;
					}
					case 20:
						dictionary = new Dictionary<int, DocumentProperty>();
						num = 2;
						continue;
					case 21:
						if (!A_3)
						{
							num = 14;
							continue;
						}
						goto IL_2CB;
					case 22:
						goto IL_2F1;
					case 23:
						obj = (int)documentProperty.PropertyId;
						goto IL_201;
					case 24:
						num = 16;
						continue;
					case 25:
						return;
					case 26:
					{
						spr\u1ADE spr_u1ADE;
						if (spr_u1ADE.ᜂ())
						{
							num = 10;
							continue;
						}
						num = 18;
						continue;
					}
					case 27:
						goto IL_2F1;
					case 28:
						num = 11;
						continue;
					}
					break;
					IL_17E:
					if (true)
					{
					}
					object key2;
					A_1[key2] = documentProperty;
					num = 7;
					continue;
					IL_201:
					key2 = obj;
					num = 21;
					continue;
					IL_2B9:
					num2++;
					num = 22;
					continue;
					IL_2CB:
					num = 6;
					continue;
					IL_2B7:
					goto IL_2CB;
					IL_2F1:
					num = 12;
					continue;
					IL_312:
					list = A_0.ᜄ();
					num2 = 0;
					count = list.Count;
					num = 27;
				}
			}
			return;
		}
	}

	// Token: 0x060038AD RID: 14509 RVA: 0x0034F66C File Offset: 0x0034E66C
	public override WordChunkType ᜆ()
	{
		int a_ = 7;
		while (!this.ᜄ)
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
				throw new InvalidOperationException(ClipboardData.b("╬੮ၰᝲၴն奸ቺ๼彾ﮈ力붒", a_));
			}
		}
		if (true)
		{
		}
		return base.ᜆ();
	}

	// Token: 0x060038AE RID: 14510 RVA: 0x0034F6D8 File Offset: 0x0034E6D8
	public void \u171D()
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
		this.ᜄ.ᜆ();
	}

	// Token: 0x060038AF RID: 14511 RVA: 0x0034F720 File Offset: 0x0034E720
	public override sprᝑ ᜌ()
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
		int a_ = base.ᜄ(this.ᜐ().ᜇ(), 1);
		return this.ᜅ.ᜃ().ᜂ().ᜀ(this.ᜋ, a_);
	}

	// Token: 0x060038B0 RID: 14512 RVA: 0x0034F78C File Offset: 0x0034E78C
	public override sprᨼ ᜇ()
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
		int a_ = base.ᜄ(this.ᜐ().ᜇ(), 1);
		return this.ᜅ.ᜃ().\u1712().ᜀ(this.ᜋ, a_);
	}

	// Token: 0x060038B1 RID: 14513 RVA: 0x0034F7F8 File Offset: 0x0034E7F8
	public override void \u1714()
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_74;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					goto IL_74;
				case 2:
					(this.ᜅ as spr\u1F8B).\u1714();
					num = 1;
					continue;
				}
				if (this.ᜅ == null)
				{
					goto IL_76;
				}
				if (true)
				{
				}
				num = 2;
				break;
			}
		}
		IL_74:
		IL_76:
		base.\u1714();
	}

	// Token: 0x060038B2 RID: 14514 RVA: 0x0034F884 File Offset: 0x0034E884
	public override void ᜎ()
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_74;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					(this.ᜅ as spr\u1F8B).\u1714();
					num = 2;
					continue;
				case 2:
					goto IL_74;
				}
				if (true)
				{
				}
				if (this.ᜅ == null)
				{
					goto IL_76;
				}
				num = 1;
				break;
			}
		}
		IL_74:
		IL_76:
		base.ᜎ();
	}

	// Token: 0x060038B3 RID: 14515 RVA: 0x0034F910 File Offset: 0x0034E910
	protected override void \u170D()
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
		this.ᜃ = new sprᶍ();
		base.\u170D();
		this.ᜅ = new sprᝌ(this.ᜄ);
		this.ᜊ = new spr\u2612(this.ᜅ.ᜄ());
		this.ᜋ = WordSubdocument.Main;
		this.ᜌ = 0;
		this.\u170D = 0;
	}

	// Token: 0x060038B4 RID: 14516 RVA: 0x0034F998 File Offset: 0x0034E998
	protected override void ᜀ(long A_0)
	{
		for (;;)
		{
			IL_14:
			base.ᜀ(A_0);
			for (;;)
			{
				IL_1B:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						if (this.ᜐ().ᜃ(A_0))
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
							goto IL_1B;
						default:
							if (false)
							{
							}
							this.ᜂ();
							num = 0;
							continue;
						}
						break;
					}
					goto IL_14;
				}
			}
		}
	}

	// Token: 0x060038B5 RID: 14517 RVA: 0x0034FA20 File Offset: 0x0034EA20
	private void ᜂ()
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
		this.ᜃ = new sprᶍ(this.ᜐ().ᜀ());
		this.\u1715().ᜀ(this.ᜅ.ᜃ().ᜆ().\u1719());
		this.\u1715().ᜁ(this.ᜅ.ᜃ().ᜆ().\u1712());
	}

	// Token: 0x060038B6 RID: 14518 RVA: 0x0034FAB4 File Offset: 0x0034EAB4
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			sprὀ sprὀ;
			for (;;)
			{
				sprὀ = this.ᜅ.ᜃ();
				spr\u1AA9 spr_u1AA = sprὀ.ᜠ();
				spr\u1C2A[] array = sprὀ.\u1715().ᜁ();
				string[] array2 = new string[array.Length];
				int num = 0;
				int num2 = array2.Length;
				int num3 = 4;
				for (;;)
				{
					sprᲵ sprᲵ;
					spr\u1BA1 spr_u1BA;
					int num4;
					spr\u1BA1 spr_u1BA2;
					int num5;
					int num6;
					spr\u1BA1[] array3;
					sprᲵ sprᲵ2;
					switch (num3)
					{
					case 0:
						base.ᜥ().ᜀ();
						num3 = 16;
						continue;
					case 1:
						sprᲵ.ᜀ(new byte[spr_u1BA.ᜎ().Length]);
						Buffer.BlockCopy(spr_u1BA.ᜎ(), 0, sprᲵ.ᜃ(), 0, spr_u1BA.ᜎ().Length);
						num3 = 8;
						continue;
					case 2:
						num3 = 23;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_417;
						default:
							if (false)
							{
							}
							spr_u1BA.ᜈ().ᜁ(spr_u1AA.ᜀ()[0]);
							spr_u1BA.ᜈ().ᜄ(spr_u1AA.ᜀ()[1]);
							spr_u1BA.ᜈ().ᜂ(spr_u1AA.ᜀ()[2]);
							num3 = 10;
							continue;
						}
						break;
					case 4:
						goto IL_546;
					case 5:
						sprᲵ = base.ᜥ().ᜀ(num4, spr_u1BA.\u1714());
						sprᲵ.ᜃ((int)spr_u1BA.ᜋ());
						sprᲵ.ᜂ((int)spr_u1BA.\u1715());
						sprᲵ.ᜁ((int)spr_u1BA.ᜌ());
						sprᲵ.ᜀ((int)spr_u1BA.ᜐ());
						sprᲵ.ᜀ(spr_u1BA.ᜉ() == WordStyleType.CharacterStyle);
						sprᲵ.ᜂ(spr_u1BA.ᜄ());
						sprᲵ.ᜃ(spr_u1BA.ᜏ());
						sprᲵ.ᜄ(spr_u1BA.\u1716());
						sprᲵ.ᜀ(spr_u1BA.ᜉ());
						num3 = 12;
						continue;
					case 6:
						if (spr_u1BA.\u1714() != null)
						{
							num3 = 5;
							continue;
						}
						goto IL_22A;
					case 7:
						if (spr_u1BA.ᜎ() != null)
						{
							num3 = 1;
							continue;
						}
						goto IL_56C;
					case 8:
						goto IL_56C;
					case 9:
						if (spr_u1BA2.ᜎ() != null)
						{
							num3 = 28;
							continue;
						}
						goto IL_26F;
					case 10:
						goto IL_417;
					case 11:
						if (num4 < 15)
						{
							num3 = 24;
							continue;
						}
						goto IL_498;
					case 12:
						if (spr_u1BA.ᜊ() == 3)
						{
							num3 = 33;
							continue;
						}
						goto IL_56C;
					case 13:
						num3 = 9;
						continue;
					case 14:
						goto IL_216;
					case 15:
						goto IL_123;
					case 16:
						goto IL_216;
					case 17:
						if (num >= num2)
						{
							num3 = 32;
							continue;
						}
						array2[num] = array[num].ᜂ();
						base.ᜥ().ᜀ(array[num]);
						num++;
						num3 = 19;
						continue;
					case 18:
						goto IL_2B0;
					case 19:
						goto IL_546;
					case 20:
						goto IL_22A;
					case 21:
						if (spr_u1BA.ᜈ() != null)
						{
							num3 = 2;
							continue;
						}
						goto IL_446;
					case 22:
						if (num5 >= num6)
						{
							num3 = 18;
							continue;
						}
						spr_u1BA2 = array3[num5];
						num3 = 31;
						continue;
					case 23:
						if (spr_u1BA.ᜈ().ᜋ() == 65535)
						{
							num3 = 25;
							continue;
						}
						goto IL_446;
					case 24:
						num3 = 29;
						continue;
					case 25:
						num3 = 30;
						continue;
					case 26:
						goto IL_123;
					case 27:
						if (spr_u1BA2.ᜊ() == 3)
						{
							num3 = 13;
							continue;
						}
						goto IL_26F;
					case 28:
						sprᲵ2.ᜀ(new byte[spr_u1BA2.ᜎ().Length]);
						Buffer.BlockCopy(spr_u1BA2.ᜎ(), 0, sprᲵ2.ᜃ(), 0, spr_u1BA2.ᜎ().Length);
						num3 = 36;
						continue;
					case 29:
					{
						int num7;
						if (num4 >= num7)
						{
							num3 = 37;
							continue;
						}
						spr_u1BA = array3[num4];
						num3 = 21;
						continue;
					}
					case 30:
						if (spr_u1BA.\u1715() == 4095)
						{
							num3 = 3;
							continue;
						}
						goto IL_446;
					case 31:
						if (spr_u1BA2.\u1714() == null)
						{
							num3 = 0;
							continue;
						}
						sprᲵ2 = base.ᜥ().ᜀ(spr_u1BA2.\u1714(), false);
						sprᲵ2.ᜃ((int)spr_u1BA2.ᜋ());
						sprᲵ2.ᜂ((int)spr_u1BA2.\u1715());
						sprᲵ2.ᜁ((int)spr_u1BA2.ᜌ());
						sprᲵ2.ᜀ((int)spr_u1BA2.ᜐ());
						sprᲵ2.ᜂ(spr_u1BA2.ᜄ());
						sprᲵ2.ᜃ(spr_u1BA2.ᜏ());
						sprᲵ2.ᜄ(spr_u1BA2.\u1716());
						sprᲵ2.ᜀ(spr_u1BA2.ᜉ() == WordStyleType.CharacterStyle);
						sprᲵ2.ᜀ(spr_u1BA2.ᜉ());
						num3 = 27;
						continue;
					case 32:
					{
						base.ᜥ().ᜁ();
						base.ᜥ().ᜀ(array2);
						array3 = sprὀ.ᜏ();
						num4 = 0;
						int num7 = array3.Length;
						num3 = 15;
						continue;
					}
					case 33:
						num3 = 7;
						continue;
					case 34:
						goto IL_28F;
					case 35:
						goto IL_28F;
					case 36:
						goto IL_26F;
					case 37:
						goto IL_498;
					}
					break;
					IL_123:
					num3 = 11;
					continue;
					IL_216:
					num5++;
					num3 = 34;
					continue;
					IL_22A:
					num4++;
					num3 = 26;
					continue;
					IL_26F:
					if (true)
					{
					}
					this.ᜀ(sprᲵ2, spr_u1BA2);
					num3 = 14;
					continue;
					IL_28F:
					num3 = 22;
					continue;
					IL_446:
					num3 = 6;
					continue;
					IL_417:
					goto IL_446;
					IL_498:
					num5 = base.ᜥ().ᜆ();
					num6 = array3.Length;
					num3 = 35;
					continue;
					IL_546:
					num3 = 17;
					continue;
					IL_56C:
					this.ᜀ(sprᲵ, spr_u1BA);
					num3 = 20;
				}
			}
			IL_2B0:
			sprὀ.ᜀ(null);
			sprὀ.ᜀ(null);
			return;
		}
		}
	}

	// Token: 0x060038B7 RID: 14519 RVA: 0x00350118 File Offset: 0x0034F118
	private void ᜀ(sprᲵ A_0, spr\u1BA1 A_1)
	{
		for (;;)
		{
			IL_14:
			sprℵ a_ = new sprℵ(A_1.ᜈ(), base.ᜥ());
			A_0.ᜀ(a_);
			for (;;)
			{
				IL_2D:
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
							goto IL_2D;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							sprᨽ a_2 = new sprᨽ(A_1.\u1712());
							A_0.ᜁ(a_2);
							num = 2;
							continue;
						}
						}
						break;
					case 1:
						if (A_1.\u1712() != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					goto IL_14;
				}
			}
		}
	}

	// Token: 0x060038B8 RID: 14520 RVA: 0x003501BC File Offset: 0x0034F1BC
	private MemoryStream ᜀ(Stream A_0)
	{
		int a_ = 15;
		MemoryStream result;
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			byte[] array = new byte[A_0.Length];
			long position = A_0.Position;
			A_0.Position = 0L;
			A_0.Read(array, 0, array.Length);
			A_0.Position = position;
			result = new MemoryStream(array);
		}
		catch
		{
			throw new ArgumentException(ClipboardData.b("㙴ᙶ᝸ᕺቼ୾ꆀꮊ떔뾞튠힢힤슦좨욪趬", a_));
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060038B9 RID: 14521 RVA: 0x00350260 File Offset: 0x0034F260
	public void ᜑ()
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

	// Token: 0x04002A5D RID: 10845
	private new const int ᜀ = 1;

	// Token: 0x04002A5E RID: 10846
	protected new bool ᜁ;

	// Token: 0x04002A5F RID: 10847
	protected new bool ᜂ;

	// Token: 0x04002A60 RID: 10848
	private sprᶍ ᜃ;

	// Token: 0x04002A61 RID: 10849
	private new bool ᜄ;

	// Token: 0x04002A62 RID: 10850
	private new sprῳ ᜅ;

	// Token: 0x04002A63 RID: 10851
	private new BuiltinDocumentProperties ᜆ = new BuiltinDocumentProperties();

	// Token: 0x04002A64 RID: 10852
	private new CustomDocumentProperties ᜇ = new CustomDocumentProperties();

	// Token: 0x04002A65 RID: 10853
	private new Encoding ᜈ;

	// Token: 0x04002A66 RID: 10854
	private new spr\u2214 ᜉ;

	// Token: 0x04002A67 RID: 10855
	private new spr\u1DAC ᜊ;
}
