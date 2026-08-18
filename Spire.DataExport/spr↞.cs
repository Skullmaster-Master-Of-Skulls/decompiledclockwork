using System;
using System.IO;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x0200004A RID: 74
internal class spr\u219E
{
	// Token: 0x06000262 RID: 610 RVA: 0x00016184 File Offset: 0x00015184
	public spr\u219E(sprḫ A_0)
	{
		this.ᜀ = A_0;
		this.ᜁ = new sprᰉ(this);
		this.ᜂ = new spr\u1FBF(this);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x000161B8 File Offset: 0x000151B8
	private void ᜀ()
	{
		int a_ = 17;
		for (;;)
		{
			this.ᜂ().ᜇ().ᜃ();
			int num = this.ᜂ().ᜁ();
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					int num3;
					if (num3 >= this.ᜂ().ᜀ().ᜌ())
					{
						num2 = 1;
						continue;
					}
					this.ᜂ().ᜀ().ᜀ(num3).ᜀ(num);
					num += this.ᜃ().ᜀ(num3).ᜁ();
					num3++;
					num2 = 3;
					continue;
				}
				case 1:
					goto IL_11B;
				case 2:
					goto IL_EF;
				case 3:
					goto IL_EF;
				case 4:
				{
					if (this.ᜂ().ᜀ().ᜌ() != this.ᜃ().ᜌ())
					{
						if (true)
						{
						}
						num2 = 5;
						continue;
					}
					int num3 = 0;
					num2 = 2;
					continue;
				}
				case 5:
					goto IL_7A;
				}
				break;
				IL_EF:
				num2 = 0;
			}
		}
		IL_7A:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搬䄮䜰刲头帶崸琺䴼娾㍀≂ㅄ⹆♈╊ቌ੎⥐げご㭖୘㹚㹜ぞ፠ݢ", a_)));
		IL_11B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_7A;
		default:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00016300 File Offset: 0x00015300
	public void ᜁ(sprḗ A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1DCF a_2;
				a_2.ᜀ = 0;
				a_2.ᜁ = 0;
				byte[] array = new byte[spr\u1DCF.ᜀ()];
				spr\u2320 spr_u = null;
				this.ᜂ.ᜊ();
				this.ᜁ.ᜂ();
				A_0.Seek(0L, SeekOrigin.Begin);
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						ushort num2;
						if (num2 != 32)
						{
							num = 7;
							continue;
						}
						goto IL_1BF;
					}
					case 1:
						num = 9;
						continue;
					case 2:
						if (A_0.ᜀ(array, array.Length) != array.Length)
						{
							num = 14;
							continue;
						}
						spr\u1DCF.ᜀ(array, ref a_2);
						num = 6;
						continue;
					case 3:
						if (a_2.ᜀ == 2057)
						{
							num = 8;
							continue;
						}
						goto IL_13C;
					case 4:
						goto IL_1BF;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (a_2.ᜀ != 0)
							{
								num = 16;
								continue;
							}
							goto IL_2C4;
						}
						break;
					case 6:
						if (a_2.ᜀ != 10)
						{
							num = 10;
							continue;
						}
						goto IL_2C4;
					case 7:
						num = 13;
						continue;
					case 8:
					{
						if (true)
						{
						}
						ushort num2 = (spr_u as spr\u1F46).ᜅ();
						num = 12;
						continue;
					}
					case 9:
					{
						ushort num2;
						if (num2 != 16)
						{
							num = 11;
							continue;
						}
						spr᱁ spr᱁ = new spr᱁(this);
						spr᱁.ᜀ(A_0, spr_u as spr\u1F46);
						this.ᜂ.ᜀ(spr᱁);
						num = 17;
						continue;
					}
					case 10:
						num = 5;
						continue;
					case 11:
						num = 0;
						continue;
					case 12:
					{
						ushort num2;
						if (num2 != 5)
						{
							num = 1;
							continue;
						}
						this.ᜁ.ᜀ(A_0, spr_u as spr\u1F46);
						num = 4;
						continue;
					}
					case 13:
						goto IL_F6;
					case 14:
						goto IL_1E9;
					case 15:
						goto IL_1BF;
					case 16:
						spr_u = sprᮌ.ᜀ(this.ᜁ, A_0, a_2);
						num = 3;
						continue;
					case 17:
						goto IL_1BF;
					}
					break;
					IL_1BF:
					num = 2;
				}
			}
			IL_F6:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("嘞传唢䐤䬦䀨伪戬弮吰䄲吴䌶倸吺匼怾р㭂♄≆╈᥊⡌ⱎ㹐⅒ㅔ", a_)));
			IL_13C:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("嘞传唢䐤䬦䀨伪戬弮吰䄲吴䌶倸吺匼怾р㭂♄≆╈᥊⡌ⱎ㹐⅒ㅔ", a_)));
			IL_1E9:
			IL_2C4:
			this.ᜁ.ᜊ().ᜀ();
			return;
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x000165E4 File Offset: 0x000155E4
	public void ᜀ(sprḗ A_0)
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
		this.ᜁ.ᜀ(A_0);
		this.ᜂ.ᜀ(A_0);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00016640 File Offset: 0x00015640
	public void ᜄ()
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
		this.ᜁ.ᜂ();
		this.ᜂ.ᜊ();
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00016694 File Offset: 0x00015694
	public sprḫ ᜁ()
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

	// Token: 0x06000268 RID: 616 RVA: 0x000166D8 File Offset: 0x000156D8
	public sprᰉ ᜂ()
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

	// Token: 0x06000269 RID: 617 RVA: 0x0001671C File Offset: 0x0001571C
	public spr\u1FBF ᜃ()
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

	// Token: 0x040000BA RID: 186
	private sprḫ ᜀ;

	// Token: 0x040000BB RID: 187
	private sprᰉ ᜁ;

	// Token: 0x040000BC RID: 188
	private spr\u1FBF ᜂ;
}
