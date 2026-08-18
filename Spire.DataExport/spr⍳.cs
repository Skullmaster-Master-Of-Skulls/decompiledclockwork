using System;
using System.Collections;
using System.Text.RegularExpressions;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS;
using Spire.DataExport.XLS.Formula;

// Token: 0x0200012F RID: 303
internal class spr\u2373 : sprὶ
{
	// Token: 0x06000762 RID: 1890 RVA: 0x0004AE7C File Offset: 0x00049E7C
	internal new ushort ᜂ()
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

	// Token: 0x06000763 RID: 1891 RVA: 0x0004AEC0 File Offset: 0x00049EC0
	public spr\u2373(WorkSheet A_0, FormulaTokenCode A_1) : base(A_1, 11)
	{
		this.ᜄ = A_0;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0004AEE0 File Offset: 0x00049EE0
	public static bool ᜀ(byte A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					if (A_0 == 91)
					{
						return true;
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
						num = 0;
						continue;
					}
					break;
				case 2:
					if (true)
					{
					}
					if (A_0 != 59)
					{
						num = 3;
						continue;
					}
					return true;
				case 3:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_7A:
		return A_0 == 123;
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0004AF6C File Offset: 0x00049F6C
	public override void ᜀ(object[] A_0)
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
		this.ᜁ(A_0[0] as string);
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0004AFB8 File Offset: 0x00049FB8
	public override void ᜀ(byte[] A_0, int A_1)
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
		this.ᜂ = BitConverter.ToUInt16(A_0, A_1);
		this.ᜂ = BitConverter.ToUInt16(A_0, A_1 + 2);
		this.ᜃ = BitConverter.ToUInt16(A_0, A_1 + 4);
		this.ᜄ = A_0[A_1 + 6];
		this.ᜆ = A_0[A_1 + 7];
		this.ᜅ = A_0[A_1 + 8];
		this.ᜇ = A_0[A_1 + 9];
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0004B04C File Offset: 0x0004A04C
	public override byte[] ᜁ()
	{
		byte[] array2;
		for (;;)
		{
			byte[] array = base.ᜁ();
			array2 = new byte[base.ᜄ()];
			array2[0] = array[0];
			Array.Copy(array, 1, array2, 3, 8);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜃ != null)
					{
						num = 2;
						continue;
					}
					goto IL_98;
				case 1:
					goto IL_96;
				case 2:
					for (;;)
					{
						this.ᜀ(this.ᜃ);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_73;
						}
					}
					IL_73:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_96:
		IL_98:
		BitConverter.GetBytes(this.ᜂ).CopyTo(array2, 1);
		return array2;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0004B104 File Offset: 0x0004A104
	public override string ᜀ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜃ + HyperlinksCollectionEditor.b("༭", a_) + base.ᜀ();
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0004B168 File Offset: 0x0004A168
	private new void ᜁ(string A_0)
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
		Match match = spr\u2373.ᜁ.Match(A_0);
		this.ᜃ = match.Groups[HyperlinksCollectionEditor.b("笧䈩䤫䬭䐯", a_)].Value;
		string value = match.Groups[HyperlinksCollectionEditor.b("欧䔩䀫嬭崯就Գ", a_)].Value;
		string value2 = match.Groups[HyperlinksCollectionEditor.b("稧䔩嬫Ἥ", a_)].Value;
		string value3 = match.Groups[HyperlinksCollectionEditor.b("欧䔩䀫嬭崯就س", a_)].Value;
		string value4 = match.Groups[HyperlinksCollectionEditor.b("稧䔩嬫ᰭ", a_)].Value;
		base.ᜀ(value2, value4, value, value3);
		this.ᜀ(this.ᜃ);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0004B270 File Offset: 0x0004A270
	private void ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								this.ᜂ = (ushort)num2;
								this.ᜄ.ᜀ.ᜭ = true;
								num = 3;
								continue;
							case 1:
								goto IL_11D;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 7;
									continue;
								}
								WorkSheet workSheet = (WorkSheet)enumerator.Current;
								num = 5;
								continue;
							}
							case 3:
								goto IL_10C;
							case 5:
							{
								WorkSheet workSheet;
								if (workSheet.SheetName.Equals(A_0))
								{
									num = 0;
									continue;
								}
								num2++;
								num = 6;
								continue;
							}
							case 7:
								num = 1;
								continue;
							}
							IL_83:
							if (true)
							{
							}
							num = 2;
							continue;
							goto IL_83;
						}
						IL_10C:
						IL_11D:
						return;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									for (;;)
									{
										disposable.Dispose();
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											goto IL_171;
										}
									}
									IL_171:
									if (false)
									{
									}
									num = 1;
									continue;
								case 1:
									goto IL_180;
								case 2:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_182;
								}
								break;
							}
						}
						IL_180:
						IL_182:;
					}
					goto IL_183;
				case 1:
					goto IL_183;
				}
				if (this.ᜄ != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_183:
				num2 = 0;
				enumerator = this.ᜄ.ᜀ.Sheets.GetEnumerator();
				num = 0;
			}
			return;
		}
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0004B43C File Offset: 0x0004A43C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2373()
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		spr\u2373.ᜀ = RegexOptions.Compiled;
		spr\u2373.ᜁ = new Regex(HyperlinksCollectionEditor.b("ਡᬣᨥ笧䈩䤫䬭䐯ఱ漳樵欷ᨹ愻ᔽ椿᥁ᡃ杅ᕇ扉獋牍ፏ㵑㡓⍕㕗㑙浛恝㭟㹡䁣㭥坧ㅩ⵫䍭⩯⽱彳彵偷䕹䁻ⱽ떃뢅펇횉ꢋ펍꾏캑붕놗ꂙ뒛ꆝ鲟쮣쪥\udda7잩슫鲭躯銵薹ﾽ飁駃苏뷑ꏓ臙胛﫝뷟\udde1룣若쏧쏩", a_), spr\u2373.ᜀ);
	}

	// Token: 0x040005EB RID: 1515
	private new static RegexOptions ᜀ;

	// Token: 0x040005EC RID: 1516
	public new static readonly Regex ᜁ;

	// Token: 0x040005ED RID: 1517
	private new ushort ᜂ;

	// Token: 0x040005EE RID: 1518
	private new string ᜃ;

	// Token: 0x040005EF RID: 1519
	private new WorkSheet ᜄ;
}
