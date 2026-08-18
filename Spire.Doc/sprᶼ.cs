using System;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000383 RID: 899
internal class spr\u1DBC : spr\u17BB
{
	// Token: 0x0600324B RID: 12875 RVA: 0x002E5D10 File Offset: 0x002E4D10
	internal spr\u1DBC(int A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x0600324C RID: 12876 RVA: 0x002E5D28 File Offset: 0x002E4D28
	internal spr\u1DBC(int A_0, string A_1, string A_2, string A_3) : base(A_0, 0)
	{
		this.ᜀ = A_1;
		this.ᜁ = A_2;
		this.ᜂ = A_3;
		base.ᜃ(this.ᜀ());
	}

	// Token: 0x0600324D RID: 12877 RVA: 0x002E5D60 File Offset: 0x002E4D60
	private int ᜀ()
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
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter a_ = new BinaryWriter(memoryStream);
		this.ᜀ(a_);
		return (int)memoryStream.Position;
	}

	// Token: 0x0600324E RID: 12878 RVA: 0x002E5DB8 File Offset: 0x002E4DB8
	internal override void ᜀ(BinaryReader A_0)
	{
		for (;;)
		{
			int num = (int)A_0.BaseStream.Position;
			try
			{
				this.ᜃ(A_0);
			}
			finally
			{
				if (true)
				{
				}
				int num2 = num + base.ᜊ();
				A_0.BaseStream.Position = (long)num2;
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
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x002E5E34 File Offset: 0x002E4E34
	private void ᜃ(BinaryReader A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 18;
			byte[] array;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.HasGUID) != (EsUriFlags)0)
					{
						num = 1;
						continue;
					}
					goto IL_16F;
				}
				case 1:
					A_0.ReadBytes(16);
					num = 22;
					continue;
				case 2:
					goto IL_14D;
				case 3:
					goto IL_14D;
				case 4:
					goto IL_221;
				case 5:
					goto IL_21C;
				case 6:
					goto IL_129;
				case 7:
					if (sprὊ.ᜂ(array, spr\u1DBC.ᜄ))
					{
						num = 9;
						continue;
					}
					num = 21;
					continue;
				case 8:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.HasDisplayName) != (EsUriFlags)0)
					{
						num = 10;
						continue;
					}
					goto IL_1C8;
				}
				case 9:
					this.ᜀ = spr\u1DBC.ᜂ(A_0);
					num = 2;
					continue;
				case 10:
					sprឱ.ᜁ(A_0);
					num = 25;
					continue;
				case 11:
				{
					if (num2 != 2)
					{
						num = 24;
						continue;
					}
					EsUriFlags esUriFlags = (EsUriFlags)A_0.ReadInt32();
					num = 8;
					continue;
				}
				case 12:
					this.ᜀ = spr\u1DBC.ᜁ(A_0);
					num = 28;
					continue;
				case 13:
					this.ᜁ = sprឱ.ᜁ(A_0);
					num = 6;
					continue;
				case 14:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.HasLocationStr) != (EsUriFlags)0)
					{
						num = 13;
						continue;
					}
					goto IL_129;
				}
				case 15:
					A_0.ReadBytes(8);
					num = 5;
					continue;
				case 16:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.HasMoniker) == (EsUriFlags)0)
					{
						goto IL_14D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_25D;
					default:
						if (false)
						{
						}
						num = 27;
						continue;
					}
					break;
				}
				case 17:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.HasFrameName) != (EsUriFlags)0)
					{
						num = 19;
						continue;
					}
					goto IL_221;
				}
				case 19:
					this.ᜂ = sprឱ.ᜁ(A_0);
					num = 4;
					continue;
				case 20:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.HasCreationTime) != (EsUriFlags)0)
					{
						num = 15;
						continue;
					}
					return;
				}
				case 21:
					if (sprὊ.ᜂ(array, spr\u1DBC.ᜅ))
					{
						num = 12;
						continue;
					}
					goto IL_C0;
				case 22:
					goto IL_16F;
				case 23:
					return;
				case 24:
					return;
				case 25:
					goto IL_1C8;
				case 26:
				{
					EsUriFlags esUriFlags;
					if ((esUriFlags & EsUriFlags.MonikerSavedAsStr) != (EsUriFlags)0)
					{
						num = 29;
						continue;
					}
					array = A_0.ReadBytes(16);
					num = 7;
					continue;
				}
				case 27:
					goto IL_25D;
				case 28:
					goto IL_14D;
				case 29:
					if (true)
					{
					}
					this.ᜀ = sprឱ.ᜁ(A_0);
					num = 3;
					continue;
				}
				if (base.ᜊ() == 0)
				{
					num = 23;
					continue;
				}
				A_0.ReadBytes(16);
				num2 = A_0.ReadInt32();
				num = 11;
				continue;
				IL_129:
				num = 0;
				continue;
				IL_14D:
				num = 14;
				continue;
				IL_16F:
				num = 20;
				continue;
				IL_1C8:
				num = 17;
				continue;
				IL_221:
				num = 16;
				continue;
				IL_25D:
				num = 26;
			}
			return;
			IL_C0:
			throw new InvalidOperationException(string.Format(ClipboardData.b("╯ᱱέᡵ᝷൹ቻ幽ﮁ慎ﮏ늑秊ﶛ躟芡즥욧쎩잫쮭슯銱ﺹ鲻ힽ뎿뿃뗇", a_), new Guid(array).ToString()));
			IL_21C:
			return;
		}
		}
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x002E61D4 File Offset: 0x002E51D4
	private static string ᜂ(BinaryReader A_0)
	{
		string text = sprឱ.ᜂ(A_0);
		int num = text.IndexOf('\0');
		if (num < 0)
		{
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
				return text;
			}
		}
		return text.Substring(0, num);
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x002E6230 File Offset: 0x002E5230
	private static string ᜁ(BinaryReader A_0)
	{
		switch (0)
		{
		default:
		{
			for (;;)
			{
				A_0.ReadUInt16();
				string result = sprឱ.ᜀ(A_0);
				A_0.ReadUInt16();
				A_0.ReadUInt16();
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (true)
						{
						}
						int num3 = A_0.ReadInt32();
						num2 = 4;
						continue;
					}
					case 1:
						goto IL_A9;
					case 2:
						goto IL_AB;
					case 3:
						goto IL_AB;
					case 4:
					{
						int num3;
						if (num3 <= 0)
						{
							return result;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					}
					case 5:
						goto IL_B7;
					}
					break;
					IL_AB:
					num2 = 5;
					continue;
					IL_B7:
					if (num >= 5)
					{
						num2 = 0;
					}
					else
					{
						A_0.ReadInt32();
						num++;
						num2 = 3;
					}
				}
			}
			IL_A9:
			int count = A_0.ReadInt32();
			A_0.ReadInt16();
			byte[] bytes = A_0.ReadBytes(count);
			return Encoding.Unicode.GetString(bytes);
		}
		}
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x002E6348 File Offset: 0x002E5348
	internal override void ᜀ(BinaryWriter A_0)
	{
		int num;
		int num2;
		for (;;)
		{
			A_0.Write(spr\u1DBC.ᜃ);
			A_0.Write(2);
			num = (int)A_0.BaseStream.Position;
			num2 = 0;
			A_0.Write(num2);
			int num3 = 7;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_79;
				case 1:
					IL_B2:
					if (spr\u1CC6.ᜋ(this.ᜁ))
					{
						num3 = 4;
						continue;
					}
					goto IL_167;
				case 2:
					goto IL_13C;
				case 3:
					sprឱ.ᜁ(this.ᜂ, A_0);
					num2 |= 128;
					num3 = 2;
					continue;
				case 4:
					sprឱ.ᜁ(this.ᜁ, A_0);
					num2 |= 8;
					num3 = 8;
					continue;
				case 5:
					A_0.Write(spr\u1DBC.ᜄ);
					sprឱ.ᜀ(this.ᜀ, A_0);
					num2 |= 1;
					num2 |= 2;
					num3 = 0;
					continue;
				case 6:
					if (spr\u1CC6.ᜋ(this.ᜀ))
					{
						num3 = 5;
						continue;
					}
					goto IL_79;
				case 7:
					if (spr\u1CC6.ᜋ(this.ᜂ))
					{
						num3 = 3;
						continue;
					}
					goto IL_13C;
				case 8:
					goto IL_13A;
				}
				break;
				IL_79:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B2;
				default:
					if (false)
					{
					}
					num3 = 1;
					continue;
				}
				IL_13C:
				num3 = 6;
			}
		}
		IL_13A:
		IL_167:
		int num4 = (int)A_0.BaseStream.Position;
		A_0.BaseStream.Position = (long)num;
		A_0.Write(num2);
		A_0.BaseStream.Position = (long)num4;
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x002E64EC File Offset: 0x002E54EC
	internal string ᜅ()
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

	// Token: 0x06003254 RID: 12884 RVA: 0x002E6530 File Offset: 0x002E5530
	internal string ᜄ()
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

	// Token: 0x06003255 RID: 12885 RVA: 0x002E6574 File Offset: 0x002E5574
	internal string ᜃ()
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

	// Token: 0x06003256 RID: 12886 RVA: 0x002E65B8 File Offset: 0x002E55B8
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1DBC()
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
		spr\u1DBC.ᜃ = new byte[]
		{
			208,
			201,
			234,
			121,
			249,
			186,
			206,
			17,
			140,
			130,
			0,
			170,
			0,
			75,
			169,
			11
		};
		spr\u1DBC.ᜄ = new byte[]
		{
			224,
			201,
			234,
			121,
			249,
			186,
			206,
			17,
			140,
			130,
			0,
			170,
			0,
			75,
			169,
			11
		};
		spr\u1DBC.ᜅ = new byte[]
		{
			3,
			3,
			0,
			0,
			0,
			0,
			0,
			0,
			192,
			0,
			0,
			0,
			0,
			0,
			0,
			70
		};
	}

	// Token: 0x0400274D RID: 10061
	private new string ᜀ;

	// Token: 0x0400274E RID: 10062
	private string ᜁ;

	// Token: 0x0400274F RID: 10063
	private string ᜂ;

	// Token: 0x04002750 RID: 10064
	private static readonly byte[] ᜃ;

	// Token: 0x04002751 RID: 10065
	private static readonly byte[] ᜄ;

	// Token: 0x04002752 RID: 10066
	private static readonly byte[] ᜅ;
}
