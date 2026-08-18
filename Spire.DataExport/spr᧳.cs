using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x0200013F RID: 319
internal class spr\u19F3 : sprᠺ
{
	// Token: 0x060007D1 RID: 2001 RVA: 0x0004E688 File Offset: 0x0004D688
	public spr\u19F3(ushort A_0, ushort A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0004E6A8 File Offset: 0x0004D6A8
	public override void ᜀ(sprḗ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				uint num = 0U;
				StringBuilder stringBuilder = new StringBuilder();
				base.ᜂ((ushort)this.ᜀ.ᜁ());
				sprᮌ.ᜀ(61451, base.ᜆ(), base.ᜄ(), this.ᜀ() - sizeof(spr\u1CC5), A_0);
				IEnumerator enumerator = this.ᜀ.ᜂ();
				if (true)
				{
				}
				int num2 = 3;
				for (;;)
				{
					byte[] bytes;
					switch (num2)
					{
					case 0:
						if (stringBuilder.Length > 0)
						{
							num2 = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						goto IL_255;
					case 3:
						try
						{
							num2 = 8;
							for (;;)
							{
								spr\u17D3 spr_u17D;
								switch (num2)
								{
								case 0:
									num2 = 6;
									continue;
								case 2:
									goto IL_159;
								case 3:
									stringBuilder.Append('\0');
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_F9;
									default:
										if (false)
										{
										}
										num2 = 2;
										continue;
									}
									break;
								case 4:
									if ((spr_u17D.ᜁ() & 32768) != 0)
									{
										num2 = 0;
										continue;
									}
									break;
								case 5:
									num2 = 9;
									continue;
								case 6:
									if (stringBuilder.Length > 0)
									{
										num2 = 3;
										continue;
									}
									goto IL_159;
								case 7:
									if (!enumerator.MoveNext())
									{
										num2 = 5;
										continue;
									}
									goto IL_F9;
								case 9:
									goto IL_207;
								}
								goto IL_F4;
								IL_F9:
								spr_u17D = (spr\u17D3)enumerator.Current;
								bytes = BitConverter.GetBytes(spr_u17D.ᜁ());
								A_0.ᜁ(bytes, bytes.Length);
								bytes = BitConverter.GetBytes(spr_u17D.ᜂ());
								A_0.ᜁ(bytes, bytes.Length);
								num2 = 4;
								continue;
								IL_159:
								stringBuilder.Append(spr_u17D.ᜀ());
								num += spr_u17D.ᜂ();
								num2 = 1;
								continue;
								IL_17D:
								num2 = 7;
								continue;
								IL_F4:
								goto IL_17D;
							}
							IL_207:
							goto IL_91;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_252;
									case 1:
										if (disposable != null)
										{
											num2 = 2;
											continue;
										}
										goto IL_254;
									case 2:
										disposable.Dispose();
										num2 = 0;
										continue;
									}
									break;
								}
							}
							IL_252:
							IL_254:;
						}
						goto IL_255;
						IL_91:
						num2 = 0;
						continue;
					}
					break;
					IL_255:
					bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
					A_0.ᜁ(bytes, (int)num);
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0004E950 File Offset: 0x0004D950
	public override void ᜀ(byte[] A_0, ref int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				base.ᜂ((ushort)this.ᜀ.ᜁ());
				sprᮌ.ᜀ(61451, base.ᜆ(), base.ᜄ(), this.ᜀ() - sizeof(spr\u1CC5), A_0, ref A_1);
				IEnumerator enumerator = this.ᜀ.ᜂ();
				if (true)
				{
				}
				int num2 = 0;
				for (;;)
				{
					byte[] bytes;
					switch (num2)
					{
					case 0:
						try
						{
							num2 = 5;
							for (;;)
							{
								spr\u17D3 spr_u17D;
								switch (num2)
								{
								case 0:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_186;
									default:
										if (false)
										{
										}
										stringBuilder.Append(spr_u17D.ᜀ());
										num += (int)spr_u17D.ᜂ();
										num2 = 3;
										continue;
									}
									break;
								case 1:
									if ((spr_u17D.ᜁ() & 32768) != 0)
									{
										num2 = 0;
										continue;
									}
									break;
								case 2:
									num2 = 6;
									continue;
								case 4:
									goto IL_186;
								case 6:
									goto IL_1D0;
								}
								goto IL_E9;
								IL_186:
								if (!enumerator.MoveNext())
								{
									num2 = 2;
									continue;
								}
								spr_u17D = (spr\u17D3)enumerator.Current;
								bytes = BitConverter.GetBytes(spr_u17D.ᜁ());
								Array.Copy(bytes, 0, A_0, A_1, bytes.Length);
								A_1 += 2;
								bytes = BitConverter.GetBytes(spr_u17D.ᜂ());
								Array.Copy(bytes, 0, A_0, A_1, bytes.Length);
								A_1 += 4;
								num2 = 1;
								continue;
								IL_17A:
								num2 = 4;
								continue;
								IL_E9:
								goto IL_17A;
							}
							IL_1D0:
							goto IL_92;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num2 = 0;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										if (disposable != null)
										{
											num2 = 2;
											continue;
										}
										goto IL_21D;
									case 1:
										goto IL_21B;
									case 2:
										disposable.Dispose();
										num2 = 1;
										continue;
									}
									break;
								}
							}
							IL_21B:
							IL_21D:;
						}
						goto IL_21E;
						IL_92:
						num2 = 3;
						continue;
					case 1:
						goto IL_21E;
					case 2:
						return;
					case 3:
						if (stringBuilder.Length > 0)
						{
							num2 = 1;
							continue;
						}
						return;
					}
					break;
					IL_21E:
					bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
					Array.Copy(bytes, 0, A_0, A_1, num);
					A_1 += num;
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0004EBC8 File Offset: 0x0004DBC8
	public override int ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = sizeof(spr\u1CC5);
			IEnumerator enumerator = this.ᜀ.ᜂ();
			try
			{
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 1;
							continue;
						}
						spr\u17D3 spr_u17D = (spr\u17D3)enumerator.Current;
						num += sizeof(spr\u19F3.ᜀ);
						num2 = 4;
						continue;
					}
					case 1:
						num2 = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_DF;
						}
						break;
					case 4:
					{
						spr\u17D3 spr_u17D;
						if ((spr_u17D.ᜁ() & 32768) != 0)
						{
							num2 = 6;
							continue;
						}
						break;
					}
					case 6:
					{
						spr\u17D3 spr_u17D;
						num += (int)spr_u17D.ᜂ();
						num2 = 2;
						continue;
					}
					}
					IL_8A:
					num2 = 0;
					continue;
					goto IL_8A;
				}
				IL_DF:
				if (false)
				{
				}
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_129;
						case 1:
							disposable.Dispose();
							num2 = 0;
							continue;
						case 2:
							if (disposable != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_12B;
						}
						break;
					}
				}
				IL_129:
				IL_12B:;
			}
			if (true)
			{
			}
			return num;
		}
		}
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0004ED24 File Offset: 0x0004DD24
	public spr\u25E1 ᜁ()
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

	// Token: 0x04000622 RID: 1570
	private new spr\u25E1 ᜀ = new spr\u25E1();

	// Token: 0x02000140 RID: 320
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	private new struct ᜀ
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x0004ED68 File Offset: 0x0004DD68
		public ᜀ(ushort A_0, uint A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x04000623 RID: 1571
		public ushort ᜀ;

		// Token: 0x04000624 RID: 1572
		public uint ᜁ;
	}
}
