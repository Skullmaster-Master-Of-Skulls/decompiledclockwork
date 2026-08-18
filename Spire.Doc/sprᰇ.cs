using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020003EF RID: 1007
internal class sprᰇ
{
	// Token: 0x06003837 RID: 14391 RVA: 0x0034AC58 File Offset: 0x00349C58
	public List<sprᮇ> ᜀ()
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

	// Token: 0x06003838 RID: 14392 RVA: 0x0034AC9C File Offset: 0x00349C9C
	public sprᰇ()
	{
		this.ᜂ = -1;
		this.ᜃ = new List<sprᮇ>();
		base..ctor();
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x0034ACC4 File Offset: 0x00349CC4
	public sprᰇ(Stream A_0)
	{
		int a_ = 6;
		this.ᜂ = -1;
		this.ᜃ = new List<sprᮇ>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("Ὣᩭɯ᝱ᕳ᭵", a_));
		}
		A_0.Position = 0L;
		this.ᜂ(A_0);
		this.ᜃ(A_0);
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x0034AD24 File Offset: 0x00349D24
	private void ᜃ(Stream A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜃ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						goto IL_90;
					}
					break;
				case 1:
					goto IL_38;
				case 2:
				{
					if (num >= count)
					{
						goto IL_44;
					}
					sprᮇ sprᮇ = this.ᜃ[num];
					sprᮇ.ᜂ(A_0);
					num++;
					num2 = 1;
					continue;
				}
				case 3:
					if (true)
					{
					}
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 2;
				continue;
				IL_44:
				num2 = 0;
			}
		}
		IL_90:
		if (false)
		{
		}
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x0034ADC8 File Offset: 0x00349DC8
	private void ᜂ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				byte[] array = new byte[16];
				A_0.Read(array, 0, 4);
				int num = BitConverter.ToInt32(array, 0);
				for (;;)
				{
					IL_49:
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_117;
						case 2:
							goto IL_117;
						case 3:
						{
							if (num != 65534)
							{
								num2 = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_49;
							}
							if (false)
							{
							}
							A_0.Read(array, 0, 2);
							A_0.Read(array, 0, 2);
							A_0.Read(array, 0, 16);
							A_0.Read(array, 0, 4);
							int num3 = BitConverter.ToInt32(array, 0);
							int num4 = 0;
							num2 = 1;
							continue;
						}
						case 4:
						{
							int num3;
							int num4;
							if (num4 >= num3)
							{
								num2 = 0;
								continue;
							}
							A_0.Read(array, 0, 16);
							Guid a_ = new Guid(array);
							int a_2 = sprữ.ᜁ(A_0, array);
							this.ᜃ.Add(new sprᮇ(a_, a_2));
							num4++;
							num2 = 2;
							continue;
						}
						case 5:
							goto IL_63;
						}
						goto IL_2F;
						IL_117:
						if (true)
						{
						}
						num2 = 4;
					}
				}
			}
			IL_63:
			throw new IOException();
		}
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x0034AF18 File Offset: 0x00349F18
	private void ᜁ(Stream A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜃ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					goto IL_30;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						goto IL_86;
					}
					break;
				case 3:
					if (num >= count)
					{
						goto IL_3C;
					}
					this.ᜃ[num].ᜁ(A_0);
					num++;
					num2 = 0;
					continue;
				}
				break;
				IL_30:
				num2 = 3;
				continue;
				IL_3C:
				num2 = 2;
			}
		}
		IL_86:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x0034AFBC File Offset: 0x00349FBC
	private void ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				sprữ.ᜂ(A_0, 65534);
				sprữ.ᜀ(A_0, 261);
				sprữ.ᜀ(A_0, 2);
				int num = 0;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int count = this.ᜃ.Count;
						sprữ.ᜂ(A_0, count);
						List<long> list = new List<long>();
						int num3 = 0;
						num2 = 3;
						continue;
					}
					case 1:
						goto IL_82;
					case 2:
						if (num >= 16)
						{
							num2 = 0;
							continue;
						}
						A_0.WriteByte(0);
						num++;
						if (true)
						{
						}
						num2 = 10;
						continue;
					case 3:
						goto IL_82;
					case 4:
					{
						int num4 = 0;
						num2 = 6;
						continue;
					}
					case 5:
						goto IL_A0;
					case 6:
						goto IL_C2;
					case 7:
						goto IL_17F;
					case 8:
						return;
					case 9:
					{
						int count;
						int num4;
						if (num4 < count)
						{
							sprᮇ sprᮇ = this.ᜃ[num4];
							long position = A_0.Position;
							List<long> list;
							A_0.Position = list[num4];
							sprữ.ᜂ(A_0, (int)position);
							A_0.Position = position;
							sprᮇ.ᜁ(A_0);
							num4++;
							num2 = 7;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17F;
						default:
							if (false)
							{
							}
							num2 = 8;
							continue;
						}
						break;
					}
					case 10:
						goto IL_A0;
					case 11:
					{
						int count;
						int num3;
						if (num3 >= count)
						{
							num2 = 4;
							continue;
						}
						sprᮇ sprᮇ2 = this.ᜃ[num3];
						byte[] array = sprᮇ2.ᜃ().ToByteArray();
						A_0.Write(array, 0, array.Length);
						List<long> list;
						list.Add(A_0.Position);
						sprữ.ᜂ(A_0, 0);
						num3++;
						num2 = 1;
						continue;
					}
					}
					break;
					IL_82:
					num2 = 11;
					continue;
					IL_A0:
					num2 = 2;
					continue;
					IL_C2:
					num2 = 9;
					continue;
					IL_17F:
					goto IL_C2;
				}
			}
			return;
		}
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x0034B1D0 File Offset: 0x0034A1D0
	public void ᜄ(Stream A_0)
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
		this.ᜀ(A_0);
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x0034B214 File Offset: 0x0034A214
	// Note: this type is marked as 'beforefieldinit'.
	static sprᰇ()
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᰇ.ᜁ = new Guid(ClipboardData.b("ၵ䩷䍹᩻䙽땿뒃ꮅ벇랍붏ꎑ꒓ꂕꂗ랙ﶛﲝ馟鎡覣隥邧骩鲫鲭튯肱莳풵讷\udeb9薻", a_));
	}

	// Token: 0x04002A4F RID: 10831
	private const int ᜀ = 65534;

	// Token: 0x04002A50 RID: 10832
	private static readonly Guid ᜁ;

	// Token: 0x04002A51 RID: 10833
	private int ᜂ;

	// Token: 0x04002A52 RID: 10834
	private List<sprᮇ> ᜃ;
}
