using System;
using System.Drawing;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;

// Token: 0x02000195 RID: 405
internal class spr\u1771 : IDisposable
{
	// Token: 0x06000F5E RID: 3934 RVA: 0x000F1494 File Offset: 0x000F0494
	public int \u171C()
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
		return this.ᜄ;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000F14D8 File Offset: 0x000F04D8
	public int \u1717()
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

	// Token: 0x06000F60 RID: 3936 RVA: 0x000F151C File Offset: 0x000F051C
	public ImageFormat \u1719()
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
		return this.ᜅ;
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000F1560 File Offset: 0x000F0560
	public Size \u1718()
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
		return new Size(this.ᜄ, this.ᜃ);
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000F15AC File Offset: 0x000F05AC
	internal byte[] \u1716()
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
		return this.ᜋ;
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x000F15F0 File Offset: 0x000F05F0
	public ImageFormat \u171E()
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
		return this.ᜅ;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x000F1634 File Offset: 0x000F0634
	public float \u171D()
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
		return this.ᜌ;
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x000F1678 File Offset: 0x000F0678
	public bool \u171A()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7C;
			case 1:
				if (this.\u171E() != ImageFormat.Wmf)
				{
					num = 0;
					continue;
				}
				return true;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (this.\u171E() == ImageFormat.Emf)
			{
				return true;
			}
			num = 3;
		}
		return false;
		IL_7C:
		return false;
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x000F1704 File Offset: 0x000F0704
	public spr\u1771(Stream A_0)
	{
		int a_ = 7;
		this.ᜆ = new byte[]
		{
			137,
			80,
			78,
			71,
			13,
			10,
			26,
			10
		};
		this.ᜇ = new byte[]
		{
			byte.MaxValue,
			216
		};
		this.ᜈ = new byte[]
		{
			66,
			77
		};
		this.ᜉ = new byte[]
		{
			73,
			73
		};
		this.ᜊ = new byte[]
		{
			77,
			77
		};
		base..ctor();
		if (A_0.CanRead)
		{
			if (A_0.CanSeek)
			{
				this.ᜂ = A_0;
				this.\u1715();
				if (this.ᜅ == ImageFormat.Unknown)
				{
					throw new ArgumentException(ClipboardData.b("㥬ݮᑰ卲ᱴ᩶ᡸᱺ᡼彾권ﶒﮔ뮚ﾜ爵膠킢키힦\ud9a8쒪\udfac\udbae풰ힲ鮴", a_));
				}
				return;
			}
		}
		throw new ArgumentException(ClipboardData.b("㹬᭮Ͱᙲᑴ᩶", a_));
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x000F17F0 File Offset: 0x000F07F0
	internal spr\u1771()
	{
		this.ᜆ = new byte[]
		{
			137,
			80,
			78,
			71,
			13,
			10,
			26,
			10
		};
		this.ᜇ = new byte[]
		{
			byte.MaxValue,
			216
		};
		this.ᜈ = new byte[]
		{
			66,
			77
		};
		this.ᜉ = new byte[]
		{
			73,
			73
		};
		this.ᜊ = new byte[]
		{
			77,
			77
		};
		base..ctor();
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x000F1880 File Offset: 0x000F0880
	private void \u1715()
	{
		int num = 32;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜎ())
				{
					num = 24;
					continue;
				}
				goto IL_298;
			case 1:
				num = 0;
				continue;
			case 2:
				if (this.ᜏ())
				{
					num = 5;
					continue;
				}
				goto IL_10A;
			case 3:
				this.ᜅ = ImageFormat.Png;
				this.ᜌ();
				num = 9;
				continue;
			case 4:
				num = 20;
				continue;
			case 5:
				this.ᜅ = ImageFormat.Jpeg;
				this.ᜋ();
				num = 14;
				continue;
			case 6:
				if (this.ᜅ == ImageFormat.Unknown)
				{
					num = 22;
					continue;
				}
				goto IL_358;
			case 7:
				num = 23;
				continue;
			case 8:
				this.ᜅ = ImageFormat.Bmp;
				this.\u1712();
				num = 25;
				continue;
			case 9:
				goto IL_391;
			case 10:
				goto IL_358;
			case 11:
				if (this.ᜅ == ImageFormat.Unknown)
				{
					num = 16;
					continue;
				}
				goto IL_332;
			case 12:
				this.ᜀ();
				this.ᜋ = new byte[this.ᜂ.Length];
				this.ᜂ.Read(this.ᜋ, 0, this.ᜋ.Length);
				num = 18;
				continue;
			case 13:
				goto IL_EF;
			case 14:
				goto IL_10A;
			case 15:
				goto IL_298;
			case 16:
				num = 29;
				continue;
			case 17:
				if (this.ᜅ == ImageFormat.Unknown)
				{
					num = 31;
					continue;
				}
				goto IL_10A;
			case 18:
				return;
			case 19:
				if (this.ᜅ == ImageFormat.Unknown)
				{
					num = 4;
					continue;
				}
				goto IL_19B;
			case 20:
				if (this.\u1714())
				{
					num = 28;
					continue;
				}
				goto IL_19B;
			case 21:
				goto IL_1C1;
			case 22:
				num = 13;
				continue;
			case 23:
				if (this.\u170D())
				{
					num = 33;
					continue;
				}
				goto IL_1C1;
			case 24:
				this.ᜅ = ImageFormat.Gif;
				this.ᜊ();
				num = 15;
				continue;
			case 25:
				goto IL_332;
			case 26:
				if (this.ᜅ == ImageFormat.Unknown)
				{
					num = 7;
					continue;
				}
				goto IL_1C1;
			case 27:
				goto IL_19B;
			case 28:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EF;
				default:
					if (false)
					{
					}
					this.ᜅ = ImageFormat.Tiff;
					this.ᜇ();
					num = 27;
					continue;
				}
				break;
			case 29:
				if (this.\u1713())
				{
					num = 8;
					continue;
				}
				goto IL_332;
			case 30:
				this.ᜅ = ImageFormat.Icon;
				this.ᜉ();
				num = 10;
				continue;
			case 31:
				if (true)
				{
				}
				num = 2;
				continue;
			case 33:
				this.ᜈ();
				num = 21;
				continue;
			case 34:
				if (this.ᜅ == ImageFormat.Unknown)
				{
					num = 1;
					continue;
				}
				goto IL_298;
			case 35:
				if (this.ᜅ != ImageFormat.Unknown)
				{
					num = 12;
					continue;
				}
				return;
			}
			if (this.ᜐ())
			{
				num = 3;
				continue;
			}
			goto IL_391;
			IL_EF:
			if (this.ᜑ())
			{
				num = 30;
				continue;
			}
			goto IL_358;
			IL_10A:
			num = 34;
			continue;
			IL_19B:
			num = 35;
			continue;
			IL_1C1:
			num = 6;
			continue;
			IL_298:
			num = 26;
			continue;
			IL_332:
			num = 19;
			continue;
			IL_358:
			num = 11;
			continue;
			IL_391:
			num = 17;
		}
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x000F1C60 File Offset: 0x000F0C60
	private bool \u1714()
	{
		for (;;)
		{
			this.ᜀ();
			byte[] array = new byte[3];
			this.ᜂ.Read(array, 0, array.Length);
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (array[0] == this.ᜊ[0])
					{
						num = 7;
						continue;
					}
					return false;
				case 1:
					if (array[2] == 42)
					{
						num = 2;
						continue;
					}
					return false;
				case 2:
					goto IL_8D;
				case 3:
					if (array[1] == this.ᜊ[1])
					{
						num = 5;
						continue;
					}
					return false;
				case 4:
					goto IL_10C;
				case 5:
					goto IL_70;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 7:
					num = 3;
					continue;
				case 8:
					if (array[0] == this.ᜉ[0])
					{
						num = 6;
						continue;
					}
					goto IL_10C;
				case 9:
					if (array[1] != this.ᜉ[1])
					{
						num = 4;
						continue;
					}
					goto IL_70;
				}
				break;
				IL_70:
				num = 1;
				continue;
				IL_10C:
				num = 0;
			}
		}
		IL_8D:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x000F1DA4 File Offset: 0x000F0DA4
	private bool \u1713()
	{
		for (;;)
		{
			this.ᜀ();
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_97;
				case 1:
					return true;
				case 2:
					goto IL_6F;
				case 3:
					if (num >= this.ᜈ.Length)
					{
						num2 = 1;
						continue;
					}
					num2 = 5;
					continue;
				case 4:
					goto IL_97;
				case 5:
					if ((int)this.ᜈ[num] != this.ᜂ.ReadByte())
					{
						num2 = 2;
						continue;
					}
					num++;
					num2 = 4;
					continue;
				}
				break;
				IL_97:
				num2 = 3;
			}
		}
		IL_6F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		case 1:
			goto IL_8F;
		default:
			goto IL_8F;
		}
		return false;
		IL_8F:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x000F1E6C File Offset: 0x000F0E6C
	private void \u1712()
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
		int num = 14;
		byte[] buffer = new byte[num];
		this.ᜂ.Read(buffer, 0, num);
		this.ᜅ();
		this.ᜄ = this.ᜅ();
		this.ᜃ = this.ᜅ();
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x000F1EE8 File Offset: 0x000F0EE8
	private bool ᜑ()
	{
		for (;;)
		{
			this.ᜀ();
			int num = this.ᜂ();
			int num2 = this.ᜂ();
			if (true)
			{
			}
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num2 == 1)
					{
						num3 = 3;
						continue;
					}
					return false;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						num3 = 0;
						continue;
					}
					break;
				case 2:
					if (num == 0)
					{
						num3 = 1;
						continue;
					}
					return false;
				case 3:
					goto IL_88;
				}
				break;
			}
		}
		return true;
		IL_88:
		return true;
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x000F1F80 File Offset: 0x000F0F80
	private bool ᜐ()
	{
		for (;;)
		{
			this.ᜀ();
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= this.ᜆ.Length)
					{
						num2 = 2;
						continue;
					}
					num2 = 5;
					continue;
				case 1:
					goto IL_97;
				case 2:
					return true;
				case 3:
					goto IL_97;
				case 4:
					goto IL_6F;
				case 5:
					if ((int)this.ᜆ[num] != this.ᜂ.ReadByte())
					{
						num2 = 4;
						continue;
					}
					num++;
					if (true)
					{
					}
					num2 = 3;
					continue;
				}
				break;
				IL_97:
				num2 = 0;
			}
		}
		IL_6F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		case 1:
			goto IL_8F;
		default:
			goto IL_8F;
		}
		return false;
		IL_8F:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x000F2048 File Offset: 0x000F1048
	private bool ᜏ()
	{
		for (;;)
		{
			this.ᜀ();
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if ((int)this.ᜇ[num] != this.ᜂ.ReadByte())
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 5;
					continue;
				case 1:
					goto IL_97;
				case 2:
					if (num >= this.ᜇ.Length)
					{
						num2 = 4;
						continue;
					}
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 3:
					goto IL_6F;
				case 4:
					return true;
				case 5:
					goto IL_97;
				}
				break;
				IL_97:
				num2 = 2;
			}
		}
		IL_6F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		case 1:
			goto IL_8F;
		default:
			goto IL_8F;
		}
		return false;
		IL_8F:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x000F2110 File Offset: 0x000F1110
	private bool ᜎ()
	{
		int a_ = 16;
		for (;;)
		{
			if (true)
			{
			}
			this.ᜀ();
			string text = this.ᜀ(6);
			if (!text.StartsWith(ClipboardData.b("ㅵㅷ㱹䑻", a_)))
			{
				return false;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_4D;
			}
		}
		IL_4D:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x000F2180 File Offset: 0x000F1180
	private bool \u170D()
	{
		for (;;)
		{
			this.ᜀ();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_96;
			default:
			{
				if (false)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜅ() == -1698247209)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						return false;
					case 1:
						if (this.ᜅ() == 1)
						{
							num = 2;
							continue;
						}
						this.ᜀ();
						num = 0;
						continue;
					case 2:
						goto IL_53;
					case 3:
						goto IL_96;
					}
					break;
				}
				break;
			}
			}
		}
		IL_53:
		this.ᜅ = ImageFormat.Emf;
		return true;
		IL_96:
		this.ᜅ = ImageFormat.Wmf;
		return true;
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x000F2230 File Offset: 0x000F1230
	private void ᜌ()
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
		Bitmap bitmap = new Bitmap(this.ᜂ);
		this.ᜄ = bitmap.Width;
		this.ᜃ = bitmap.Height;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x000F2298 File Offset: 0x000F1298
	private void ᜋ()
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
		Bitmap bitmap = new Bitmap(this.ᜂ);
		this.ᜄ = bitmap.Width;
		this.ᜃ = bitmap.Height;
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x000F2300 File Offset: 0x000F1300
	private void ᜊ()
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
		this.ᜄ = this.ᜃ();
		this.ᜃ = this.ᜃ();
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x000F2354 File Offset: 0x000F1354
	private void ᜉ()
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
		byte[] array = new byte[6];
		this.ᜂ.Read(array, 0, array.Length);
		this.ᜄ = this.ᜂ.ReadByte();
		this.ᜃ = this.ᜂ.ReadByte();
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000F23D0 File Offset: 0x000F13D0
	private void ᜈ()
	{
		byte[] array;
		for (;;)
		{
			IL_1C:
			this.ᜀ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_45;
				case 1:
					goto IL_86;
				case 2:
					if (this.ᜅ == ImageFormat.Emf)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 3:
					array = new byte[10];
					this.ᜂ.Read(array, 0, array.Length);
					this.ᜄ = this.ᜁ();
					this.ᜃ = this.ᜁ();
					num = 1;
					continue;
				case 4:
					if (this.\u1719() == ImageFormat.Wmf)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				}
				goto IL_1C;
			}
			IL_86:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_9C;
			}
		}
		IL_45:
		array = new byte[16];
		this.ᜂ.Read(array, 0, array.Length);
		this.ᜄ = this.ᜅ();
		this.ᜃ = this.ᜅ();
		return;
		IL_9C:
		if (false)
		{
		}
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000F24E0 File Offset: 0x000F14E0
	private void ᜇ()
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 256;
				int num2 = 257;
				this.ᜂ.Position = 4L;
				int num3 = this.ᜅ();
				int num4 = 8;
				for (;;)
				{
					if (true)
					{
					}
					switch (num4)
					{
					case 0:
						goto IL_153;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_94;
						default:
						{
							if (false)
							{
							}
							int num5;
							this.ᜃ = num5;
							num4 = 5;
							continue;
						}
						}
						break;
					case 2:
					{
						int num6;
						if (num6 == num)
						{
							num4 = 10;
							continue;
						}
						num4 = 4;
						continue;
					}
					case 3:
						goto IL_A0;
					case 4:
					{
						int num6;
						if (num6 == num2)
						{
							num4 = 1;
							continue;
						}
						num4 = 7;
						continue;
					}
					case 5:
						goto IL_153;
					case 6:
					{
						if ((long)num3 >= this.ᜂ.Length)
						{
							num4 = 9;
							continue;
						}
						int num6 = this.ᜃ();
						this.ᜃ();
						this.ᜅ();
						int num5 = this.ᜅ();
						num4 = 2;
						continue;
					}
					case 7:
						if (this.ᜃ != 0)
						{
							num4 = 12;
							continue;
						}
						goto IL_153;
					case 8:
						if ((long)num3 > this.ᜂ.Length)
						{
							goto IL_94;
						}
						this.ᜂ.Position = (long)(num3 + 2);
						num4 = 11;
						continue;
					case 9:
						return;
					case 10:
					{
						int num5;
						this.ᜄ = num5;
						num4 = 0;
						continue;
					}
					case 11:
						goto IL_153;
					case 12:
						return;
					}
					break;
					IL_94:
					num4 = 3;
					continue;
					IL_153:
					num4 = 6;
				}
			}
			IL_A0:
			throw new Exception(ClipboardData.b("㱧ͩ੫࡭偯᭱ᥳ᝵ίό屻᡽ꚅﺋﲍﲗ", a_));
		}
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x000F26C0 File Offset: 0x000F16C0
	private int ᜆ()
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
		byte[] array = new byte[4];
		this.ᜂ.Read(array, 0, 4);
		return ((int)array[0] << 24) + ((int)array[1] << 16) + ((int)array[2] << 8) + (int)array[3];
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x000F2728 File Offset: 0x000F1728
	private int ᜅ()
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
		byte[] array = new byte[4];
		this.ᜂ.Read(array, 0, 4);
		return (int)array[0] + ((int)array[1] << 8) + ((int)array[2] << 16) + ((int)array[3] << 24);
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x000F2790 File Offset: 0x000F1790
	private int ᜄ()
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
		byte[] array = new byte[2];
		this.ᜂ.Read(array, 0, 2);
		return ((int)array[0] << 8) + (int)array[1];
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x000F27EC File Offset: 0x000F17EC
	private int ᜃ()
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
		byte[] array = new byte[2];
		this.ᜂ.Read(array, 0, 2);
		return (int)array[0] | (int)array[1] << 8;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x000F2848 File Offset: 0x000F1848
	private int ᜂ()
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
		int num = this.ᜂ.ReadByte();
		return num + (this.ᜂ.ReadByte() << 8) & 65535;
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x000F28A4 File Offset: 0x000F18A4
	private int ᜁ()
	{
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_51:
			num -= 65536;
			if (true)
			{
			}
			num2 = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_30;
		}
		for (;;)
		{
			IL_1E:
			switch (num2)
			{
			case 0:
				return num;
			case 1:
				if (num > 32767)
				{
					num2 = 2;
					continue;
				}
				return num;
			case 2:
				goto IL_4F;
			}
			goto IL_30;
		}
		IL_4F:
		goto IL_51;
		IL_30:
		num = this.ᜂ();
		num2 = 1;
		goto IL_1E;
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000F2928 File Offset: 0x000F1928
	private string ᜀ(int A_0)
	{
		char[] array;
		for (;;)
		{
			if (true)
			{
			}
			array = new char[A_0];
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_3D;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						goto IL_3D;
					}
					break;
				case 2:
					if (num >= A_0)
					{
						num2 = 3;
						continue;
					}
					array[num] = (char)this.ᜂ.ReadByte();
					num++;
					num2 = 1;
					continue;
				case 3:
					goto IL_51;
				}
				break;
				IL_3D:
				num2 = 2;
			}
		}
		IL_51:
		return new string(array);
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x000F29C8 File Offset: 0x000F19C8
	private void ᜀ()
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
		this.ᜂ.Position = 0L;
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x000F2A10 File Offset: 0x000F1A10
	internal static spr\u1771 ᜀ(MemoryStream A_0)
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
		return new spr\u1771(A_0);
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x000F2A54 File Offset: 0x000F1A54
	internal void ᜀ(MemoryStream A_0, ImageFormat A_1)
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

	// Token: 0x06000F81 RID: 3969 RVA: 0x000F2A90 File Offset: 0x000F1A90
	public void \u171B()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_59:
			this.ᜂ.Dispose();
			this.ᜂ = null;
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_30;
		}
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				if (this.ᜂ != null)
				{
					num = 2;
					continue;
				}
				return;
			case 1:
				return;
			case 2:
				goto IL_57;
			}
			goto IL_30;
		}
		IL_57:
		goto IL_59;
		IL_30:
		this.ᜋ = null;
		if (true)
		{
		}
		num = 0;
		goto IL_1E;
	}

	// Token: 0x04001766 RID: 5990
	private const string ᜀ = "GIF8";

	// Token: 0x04001767 RID: 5991
	private const int ᜁ = 42;

	// Token: 0x04001768 RID: 5992
	private Stream ᜂ;

	// Token: 0x04001769 RID: 5993
	private int ᜃ;

	// Token: 0x0400176A RID: 5994
	private int ᜄ;

	// Token: 0x0400176B RID: 5995
	private ImageFormat ᜅ;

	// Token: 0x0400176C RID: 5996
	private byte[] ᜆ;

	// Token: 0x0400176D RID: 5997
	private byte[] ᜇ;

	// Token: 0x0400176E RID: 5998
	private byte[] ᜈ;

	// Token: 0x0400176F RID: 5999
	private byte[] ᜉ;

	// Token: 0x04001770 RID: 6000
	private byte[] ᜊ;

	// Token: 0x04001771 RID: 6001
	private byte[] ᜋ;

	// Token: 0x04001772 RID: 6002
	private float ᜌ;
}
