using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Spire.CompoundFile.Doc;

// Token: 0x020003BB RID: 955
[CLSCompliant(false)]
internal class spr\u19E4
{
	// Token: 0x06003601 RID: 13825 RVA: 0x0032B9C4 File Offset: 0x0032A9C4
	internal Stream ᜀ()
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				spr\u2578 spr_u;
				MemoryStream memoryStream;
				switch (num)
				{
				case 1:
					try
					{
						for (;;)
						{
							byte[] array = new byte[8];
							spr_u.Read(array, 0, 8);
							int num2 = BitConverter.ToInt32(array, 0);
							int num3 = num2 % this.ᜀ;
							num = 0;
							for (;;)
							{
								int num4;
								switch (num)
								{
								case 0:
									if (num3 <= 0)
									{
										num = 1;
										continue;
									}
									num = 4;
									continue;
								case 1:
									num = 2;
									continue;
								case 2:
									num4 = num2;
									goto IL_D1;
								case 3:
									goto IL_116;
								case 4:
									num4 = num2 + this.ᜀ - num3;
									goto IL_D1;
								}
								break;
								IL_D1:
								int num5 = num4;
								byte[] array2 = new byte[num5];
								spr_u.Read(array2, 0, num5);
								byte[] buffer = this.ᜀ(array2, this.ᜄ);
								memoryStream.Write(buffer, 0, num2);
								memoryStream.Position = 0L;
								num = 3;
							}
						}
						IL_116:
						return memoryStream;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_172;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_172;
								default:
									if (false)
									{
									}
									((IDisposable)spr_u).Dispose();
									num = 0;
									continue;
								}
								break;
							}
							if (spr_u == null)
							{
								break;
							}
							num = 1;
						}
						IL_172:;
					}
					goto IL_175;
				case 2:
					goto IL_51;
				}
				if (true)
				{
				}
				if (this.ᜄ == null)
				{
					num = 2;
					continue;
				}
				IL_175:
				memoryStream = new MemoryStream();
				spr_u = this.ᜃ.ᜁ(ClipboardData.b("㍵ᙷ᥹๻ݽ\ud887", a_));
				num = 1;
			}
			IL_51:
			throw new InvalidOperationException(ClipboardData.b("㽵ᙷ᥹፻౽ꢇ憎ﶍﮓﲗ뒙", a_));
		}
		}
	}

	// Token: 0x06003602 RID: 13826 RVA: 0x0032BBA8 File Offset: 0x0032ABA8
	internal void ᜃ(spr\u2547 A_0)
	{
		int a_ = 3;
		int num = 3;
		for (;;)
		{
			spr\u2547 spr_u;
			switch (num)
			{
			case 0:
				try
				{
					this.ᜁ(spr_u);
					this.ᜂ(spr_u);
					return;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_EB;
						case 2:
							spr_u.Dispose();
							num = 0;
							continue;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 2;
					}
					IL_EB:;
				}
				goto Block_3;
			case 1:
				goto IL_EE;
			case 2:
				goto IL_3E;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			this.ᜃ = A_0;
			Stream stream = A_0.ᜁ(ClipboardData.b("ⱨժ๬ᵮࡰͲŴṶᙸᕺ㑼ᅾ", a_));
			num = 1;
			continue;
			Block_3:
			try
			{
				IL_EE:
				this.ᜂ = new spr\u2505(stream);
				goto IL_48;
			}
			finally
			{
				num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14A;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						((IDisposable)stream).Dispose();
						goto IL_14A;
					case 2:
						goto IL_152;
					}
					if (stream != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_14A:
					num = 2;
				}
				IL_152:;
			}
			return;
			IL_48:
			spr_u = A_0.ᜅ(ClipboardData.b("潨⽪౬᭮ၰ⁲մᙶ᩸Ṻ๼", a_));
			num = 0;
		}
		IL_3E:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᩨὪɬᵮၰᑲၴ", a_));
	}

	// Token: 0x06003603 RID: 13827 RVA: 0x0032BD28 File Offset: 0x0032AD28
	internal bool ᜀ(string A_0)
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
		sprẰ a_ = this.ᜂ.ᜁ();
		this.ᜄ = this.ᜀ(A_0, a_);
		return this.ᜄ != null;
	}

	// Token: 0x06003604 RID: 13828 RVA: 0x0032BD8C File Offset: 0x0032AD8C
	private byte[] ᜀ(string A_0, sprẰ A_1)
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
		byte[] a_ = A_1.ᜁ();
		byte[] array = this.ᜅ.ᜀ(A_0, a_, 16);
		byte[] buffer = this.ᜀ(A_1.ᜀ(), array);
		this.ᜀ(A_1.ᜂ(), array);
		SHA1 sha = new SHA1Managed();
		sha.ComputeHash(buffer);
		return array;
	}

	// Token: 0x06003605 RID: 13829 RVA: 0x0032BE0C File Offset: 0x0032AE0C
	private byte[] ᜀ(byte[] A_0, byte[] A_1)
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
		spr\u21ED a_ = new spr\u21ED(spr\u21ED.KeySize.Bits128, A_1);
		return this.ᜅ.ᜀ(A_0, new spr\u1AED.ᜀ(a_.ᜀ), A_1.Length);
	}

	// Token: 0x06003606 RID: 13830 RVA: 0x0032BE6C File Offset: 0x0032AE6C
	private void ᜂ(spr\u2547 A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				List<spr\u226A> list = this.ᜁ.ᜀ();
				int num = 0;
				for (;;)
				{
					spr\u2547 spr_u3;
					switch (num)
					{
					case 0:
					{
						if (true)
						{
						}
						if (list.Count != 1)
						{
							num = 3;
							continue;
						}
						spr\u226A spr_u226A = list[0];
						string a_2 = spr_u226A.ᜁ();
						string a_3 = null;
						spr\u2547 spr_u = A_0.ᜅ(ClipboardData.b("㝲ᑴͶᡸ⡺ർṾ첄", a_));
						num = 2;
						continue;
					}
					case 1:
						try
						{
							string a_3;
							spr\u2547 spr_u2 = spr_u3.ᜅ(a_3);
							try
							{
								this.ᜀ(spr_u2);
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_12B;
									case 2:
										spr_u2.Dispose();
										num = 0;
										continue;
									}
									if (spr_u2 == null)
									{
										break;
									}
									num = 2;
								}
								IL_12B:;
							}
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
									goto IL_170;
								case 2:
									spr_u3.Dispose();
									num = 1;
									continue;
								}
								if (spr_u3 == null)
								{
									break;
								}
								num = 2;
							}
							IL_170:;
						}
						goto Block_3;
					case 2:
						goto IL_173;
					case 3:
						goto IL_69;
					}
					break;
					Block_3:
					try
					{
						IL_173:
						string a_2;
						spr\u2547 spr_u;
						Stream stream = spr_u.ᜁ(a_2);
						try
						{
							for (;;)
							{
								List<string> list2;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
								{
									if (false)
									{
									}
									spr\u1C3C spr_u1C3C = new spr\u1C3C(stream);
									list2 = spr_u1C3C.ᜀ();
									num = 0;
									break;
								}
								}
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										if (list2.Count != 1)
										{
											num = 2;
											continue;
										}
										string a_3 = list2[0];
										num = 1;
										continue;
									}
									case 1:
										goto IL_209;
									case 2:
										goto IL_1DE;
									}
									break;
								}
							}
							IL_1DE:
							throw new Exception(ClipboardData.b("㩲᭴Ŷᡸ᝺ᑼ᭾ꆀ", a_));
							IL_209:;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_248;
								case 1:
									((IDisposable)stream).Dispose();
									num = 0;
									continue;
								}
								if (stream == null)
								{
									break;
								}
								num = 1;
							}
							IL_248:;
						}
						goto IL_6B;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							spr\u2547 spr_u;
							switch (num)
							{
							case 1:
								goto IL_28D;
							case 2:
								spr_u.Dispose();
								num = 1;
								continue;
							}
							if (spr_u == null)
							{
								break;
							}
							num = 2;
						}
						IL_28D:;
					}
					return;
					IL_6B:
					spr_u3 = A_0.ᜅ(ClipboardData.b("❲ݴᙶ᝸ࡺ᭼ၾ첄", a_));
					num = 1;
				}
			}
			IL_69:
			throw new Exception(ClipboardData.b("㩲᭴Ŷᡸ᝺ᑼ᭾ꆀ", a_));
		}
	}

	// Token: 0x06003607 RID: 13831 RVA: 0x0032C140 File Offset: 0x0032B140
	private void ᜁ(spr\u2547 A_0)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			spr\u2578 spr_u;
			switch (num)
			{
			case 0:
				goto IL_38;
			case 1:
				try
				{
					this.ᜁ = new sprᢳ(spr_u);
					return;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)spr_u).Dispose();
							goto IL_99;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_99;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 2:
							goto IL_A1;
						}
						if (spr_u != null)
						{
							num = 0;
							continue;
						}
						break;
						IL_99:
						num = 2;
					}
					IL_A1:;
				}
				goto IL_A4;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_A4:
			spr_u = A_0.ᜁ(ClipboardData.b("⥬๮հቲ♴ݶᡸ᡺᡼㉾", a_));
			num = 1;
		}
		IL_38:
		throw new ArgumentNullException(ClipboardData.b("६๮հቲ♴ݶᡸ᡺᡼౾", a_));
	}

	// Token: 0x06003608 RID: 13832 RVA: 0x0032C244 File Offset: 0x0032B244
	private void ᜀ(spr\u2547 A_0)
	{
		int a_ = 7;
		Stream stream = A_0.ᜁ(ClipboardData.b("歬㽮Ͱᩲᡴᙶ୸ɺ", a_));
		try
		{
			new sprᯗ(stream);
			new spr\u1D52(stream);
		}
		finally
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_83;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					((IDisposable)stream).Dispose();
					goto IL_83;
				case 2:
					goto IL_8B;
				}
				if (stream != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_83:
				num = 2;
			}
			IL_8B:;
		}
	}

	// Token: 0x04002965 RID: 10597
	private int ᜀ = 16;

	// Token: 0x04002966 RID: 10598
	private sprᢳ ᜁ;

	// Token: 0x04002967 RID: 10599
	private spr\u2505 ᜂ;

	// Token: 0x04002968 RID: 10600
	private spr\u2547 ᜃ;

	// Token: 0x04002969 RID: 10601
	private byte[] ᜄ;

	// Token: 0x0400296A RID: 10602
	private spr\u1AED ᜅ = new spr\u1AED();
}
