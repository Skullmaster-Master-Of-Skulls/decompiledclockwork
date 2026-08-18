using System;
using System.Collections;
using System.Text.RegularExpressions;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS;
using Spire.DataExport.XLS.Formula;

// Token: 0x020000F0 RID: 240
internal class spr\u203A : sprᣴ
{
	// Token: 0x06000513 RID: 1299 RVA: 0x0003188C File Offset: 0x0003088C
	internal ushort ᜂ()
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

	// Token: 0x06000514 RID: 1300 RVA: 0x000318D0 File Offset: 0x000308D0
	public spr\u203A(WorkSheet A_0, FormulaTokenCode A_1) : base(A_1, 7)
	{
		this.ᜄ = A_0;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x000318EC File Offset: 0x000308EC
	public new static bool ᜀ(byte A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 != 90)
						{
							num = 3;
							continue;
						}
						return true;
					case 1:
						if (true)
						{
						}
						if (A_0 == 58)
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
							num = 2;
							continue;
						}
						break;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_7A;
					}
					break;
				}
			}
		}
		IL_7A:
		return A_0 == 122;
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00031978 File Offset: 0x00030978
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

	// Token: 0x06000517 RID: 1303 RVA: 0x000319C4 File Offset: 0x000309C4
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
		this.ᜆ = BitConverter.ToUInt16(A_0, A_1 + 2);
		this.ᜇ = A_0[A_1 + 4];
		this.ᜈ = A_0[A_1 + 5];
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00031A34 File Offset: 0x00030A34
	public override byte[] ᜁ()
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
		byte[] array = new byte[base.ᜄ()];
		array[0] = base.\u170D();
		this.ᜀ(this.ᜃ);
		BitConverter.GetBytes(this.ᜂ).CopyTo(array, 1);
		BitConverter.GetBytes(this.ᜆ).CopyTo(array, 3);
		array[5] = base.ᜃ();
		array[6] = this.ᜈ;
		return array;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00031AC8 File Offset: 0x00030AC8
	public override string ᜀ()
	{
		int a_ = 2;
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
			if (this.ᜄ != null)
			{
				return this.ᜄ.SheetName + HyperlinksCollectionEditor.b("㼝", a_) + base.ᜀ();
			}
			break;
		}
		return base.ᜀ();
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00031B44 File Offset: 0x00030B44
	private new void ᜁ(string A_0)
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
		Match match = spr\u203A.ᜁ.Match(A_0);
		this.ᜃ = match.Groups[HyperlinksCollectionEditor.b("紭堯圱儳䈵", a_)].Value;
		string value = match.Groups[HyperlinksCollectionEditor.b("簭弯䔱", a_)].Value;
		string value2 = match.Groups[HyperlinksCollectionEditor.b("洭弯帱䄳嬵嘷", a_)].Value;
		base.ᜀ(value, value2);
		this.ᜀ(this.ᜃ);
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00031C0C File Offset: 0x00030C0C
	private new void ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				int num2;
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 1:
								this.ᜂ = (ushort)num2;
								this.ᜄ.ᜀ.ᜭ = true;
								num = 6;
								continue;
							case 3:
								goto IL_110;
							case 5:
							{
								WorkSheet workSheet;
								if (workSheet.SheetName.Equals(A_0))
								{
									num = 1;
									continue;
								}
								num2++;
								num = 4;
								continue;
							}
							case 6:
								goto IL_FF;
							case 7:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								WorkSheet workSheet = (WorkSheet)enumerator.Current;
								num = 5;
								continue;
							}
							}
							IL_AF:
							num = 7;
							continue;
							goto IL_AF;
						}
						IL_FF:
						IL_110:
						return;
					}
					finally
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
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_173;
									case 2:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_175;
									}
									break;
								}
							}
							IL_173:
							break;
						}
						IL_175:;
					}
					goto IL_176;
				case 2:
					if (this.ᜄ.ᜀ != null)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					goto IL_176;
				case 4:
					num = 2;
					continue;
				}
				if (this.ᜄ != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_176:
				num2 = 0;
				enumerator = this.ᜄ.ᜀ.Sheets.GetEnumerator();
				if (true)
				{
				}
				num = 0;
			}
			return;
		}
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00031E04 File Offset: 0x00030E04
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u203A()
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
		spr\u203A.ᜀ = RegexOptions.Compiled;
		spr\u203A.ᜁ = new Regex(HyperlinksCollectionEditor.b("̪ሬጮ戰嬲倴制䴸Ժ昼挾ቀ捂ᡄ汆恈၊ᅌ湎౐筒橔歖ᩘ㑚ㅜ⩞ౠൢ孤㱦㕨佪ぬ偮⩰㉲塴⵶⑸偺呼坾뺀뾂힄ﺈ떊회펎떐캒ꪔ쮖ﶘ낚뒜", a_), spr\u203A.ᜀ);
	}

	// Token: 0x0400056D RID: 1389
	private new static RegexOptions ᜀ;

	// Token: 0x0400056E RID: 1390
	public new static readonly Regex ᜁ;

	// Token: 0x0400056F RID: 1391
	private ushort ᜂ;

	// Token: 0x04000570 RID: 1392
	private new string ᜃ;

	// Token: 0x04000571 RID: 1393
	private new WorkSheet ᜄ;
}
