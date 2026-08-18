using System;
using System.IO;

// Token: 0x02000059 RID: 89
internal class spr\u2155 : sprᠺ
{
	// Token: 0x060002E8 RID: 744 RVA: 0x0001B61C File Offset: 0x0001A61C
	public spr\u2155(ushort A_0, ushort A_1, string A_2, Stream A_3, int A_4) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
		this.ᜁ = A_3;
		this.ᜂ = A_4;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0001B654 File Offset: 0x0001A654
	protected int ᜂ()
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.ᜁ != null)
					{
						num2 = 3;
						continue;
					}
					goto IL_76;
				case 1:
					goto IL_50;
				case 2:
				{
					FileInfo fileInfo = new FileInfo(this.ᜀ);
					num = (int)fileInfo.Length;
					num2 = 1;
					continue;
				}
				case 3:
					num2 = 9;
					continue;
				case 4:
					if (File.Exists(this.ᜀ))
					{
						num2 = 2;
						continue;
					}
					goto IL_50;
				case 5:
					return num;
				case 6:
					num = (int)this.ᜁ.Length;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
						if (false)
						{
						}
						num2 = 8;
						continue;
					}
					break;
				case 7:
					if (true)
					{
					}
					if (this.ᜂ == 7)
					{
						goto IL_6C;
					}
					return num;
				case 8:
					goto IL_50;
				case 9:
					if (this.ᜁ.Length > 0L)
					{
						num2 = 6;
						continue;
					}
					goto IL_76;
				case 10:
					num -= 14;
					num2 = 5;
					continue;
				}
				break;
				IL_50:
				num2 = 7;
				continue;
				IL_6C:
				num2 = 10;
				continue;
				IL_76:
				num2 = 4;
			}
		}
		return num;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0001B79C File Offset: 0x0001A79C
	public override void ᜀ(sprḗ A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = this.ᜂ;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_F5;
					case 1:
						goto IL_D9;
					case 2:
						switch (num2)
						{
						case 2:
							base.ᜂ(534);
							num3 = 6;
							continue;
						case 3:
							base.ᜂ(980);
							num3 = 5;
							continue;
						case 4:
							goto IL_133;
						case 5:
							base.ᜂ(1130);
							num3 = 7;
							continue;
						case 6:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_131;
							default:
								if (false)
								{
								}
								base.ᜂ(1760);
								num3 = 4;
								continue;
							}
							break;
						case 7:
							base.ᜂ(1960);
							num = 14;
							num3 = 0;
							continue;
						default:
							num3 = 3;
							continue;
						}
						break;
					case 3:
						num3 = 1;
						continue;
					case 4:
						goto IL_AF;
					case 5:
						goto IL_CB;
					case 6:
						goto IL_118;
					case 7:
						goto IL_131;
					}
					break;
				}
			}
			IL_AF:
			IL_CB:
			IL_D9:
			IL_F5:
			IL_118:
			IL_131:
			IL_133:
			int a_ = this.ᜂ();
			sprᮌ.ᜀ((ushort)(61464 + this.ᜂ), base.ᜆ(), base.ᜄ(), a_, A_0);
			byte[] a_2 = new byte[17];
			A_0.ᜁ(a_2, 17);
			this.ᜁ.Seek((long)num, SeekOrigin.Begin);
			A_0.ᜀ(this.ᜁ, this.ᜁ.Length - this.ᜁ.Position);
			return;
		}
		}
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0001B948 File Offset: 0x0001A948
	public override void ᜀ(byte[] A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num4;
			for (;;)
			{
				int num = 0;
				int num2 = this.ᜂ;
				int num3 = 13;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_295;
					case 1:
						goto IL_1A0;
					case 2:
					{
						if (sprᮌ.ᜀ())
						{
							num3 = 11;
							continue;
						}
						FileStream fileStream = new FileStream(this.ᜀ, FileMode.Open);
						num3 = 7;
						continue;
					}
					case 3:
						if (this.ᜁ != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_2FE;
					case 4:
						goto IL_2F9;
					case 5:
						goto IL_295;
					case 6:
						goto IL_171;
					case 7:
						try
						{
							FileStream fileStream;
							fileStream.Seek((long)num, SeekOrigin.Begin);
							num4 = fileStream.Read(A_0, A_1, (int)(fileStream.Length - fileStream.Position));
							goto IL_327;
						}
						finally
						{
							num3 = 2;
							for (;;)
							{
								FileStream fileStream;
								switch (num3)
								{
								case 0:
									((IDisposable)fileStream).Dispose();
									num3 = 1;
									continue;
								case 1:
									goto IL_229;
								}
								if (fileStream == null)
								{
									break;
								}
								num3 = 0;
							}
							IL_229:;
						}
						goto IL_22C;
					case 8:
						if (this.ᜁ.Length > 0L)
						{
							num3 = 1;
							continue;
						}
						goto IL_2FE;
					case 9:
						goto IL_295;
					case 10:
						num3 = 0;
						continue;
					case 11:
					{
						FileStream fileStream2 = sprᮌ.ᜂ(this.ᜀ);
						num3 = 6;
						continue;
					}
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F9;
						}
						if (false)
						{
						}
						goto IL_295;
					case 13:
						switch (num2)
						{
						case 2:
							base.ᜂ(534);
							num3 = 15;
							continue;
						case 3:
							base.ᜂ(980);
							num3 = 14;
							continue;
						case 4:
							goto IL_295;
						case 5:
							base.ᜂ(1130);
							num3 = 5;
							continue;
						case 6:
							base.ᜂ(1760);
							num3 = 9;
							continue;
						case 7:
							goto IL_22C;
						default:
							num3 = 10;
							continue;
						}
						break;
					case 14:
						goto IL_295;
					case 15:
						goto IL_295;
					}
					break;
					IL_22C:
					base.ᜂ(1960);
					num = 14;
					num3 = 12;
					continue;
					IL_295:
					int num5 = this.ᜂ();
					sprᮌ.ᜀ((ushort)(61464 + this.ᜂ), base.ᜆ(), base.ᜄ(), num5 + 17, A_0, ref A_1);
					Array.Clear(A_0, A_1, 17);
					A_1 += 17;
					if (true)
					{
					}
					num3 = 3;
					continue;
					IL_2F9:
					num3 = 8;
					continue;
					IL_2FE:
					num4 = 0;
					num3 = 2;
				}
			}
			return;
			IL_171:
			try
			{
				int num;
				FileStream fileStream2;
				fileStream2.Seek((long)num, SeekOrigin.Begin);
				num4 = fileStream2.Read(A_0, A_1, (int)(fileStream2.Length - fileStream2.Position));
				goto IL_327;
			}
			finally
			{
				int num3 = 2;
				for (;;)
				{
					FileStream fileStream2;
					switch (num3)
					{
					case 0:
						goto IL_FE;
					case 1:
						((IDisposable)fileStream2).Dispose();
						num3 = 0;
						continue;
					}
					if (fileStream2 == null)
					{
						break;
					}
					num3 = 1;
				}
				IL_FE:;
			}
			IL_1A0:
			return;
			IL_327:
			A_1 += num4;
			return;
		}
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001BCA0 File Offset: 0x0001ACA0
	public override int ᜀ()
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
		return this.ᜂ() + 17 + sizeof(spr\u1CC5);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0001BCEC File Offset: 0x0001ACEC
	public int ᜁ()
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

	// Token: 0x040000D3 RID: 211
	private new string ᜀ = string.Empty;

	// Token: 0x040000D4 RID: 212
	private Stream ᜁ;

	// Token: 0x040000D5 RID: 213
	private int ᜂ;
}
