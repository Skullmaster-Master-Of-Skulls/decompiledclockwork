using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x0200018A RID: 394
internal class spr᭙ : spr\u17BB
{
	// Token: 0x06000DD0 RID: 3536 RVA: 0x000E4548 File Offset: 0x000E3548
	internal spr᭙(int A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x000E4560 File Offset: 0x000E3560
	private spr᭙(int A_0, int A_1, int A_2) : base(A_0, spr᭙.ᜀ(A_1, A_2))
	{
		this.ᜁ = A_1;
		this.ᜂ = A_1;
		this.ᜃ = A_2;
		this.ᜄ = new byte[this.ᜃ * this.ᜁ];
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x000E45A8 File Offset: 0x000E35A8
	private static int ᜀ(int A_0, int A_1)
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
		return 6 + A_0 * A_1;
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x000E45E8 File Offset: 0x000E35E8
	internal spr᭙(int A_0, spr\u2143[] A_1) : this(A_0, A_1.Length, 8)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				binaryWriter.Write(spr\u20D8.ᜀ(A_1[i].ᜀ));
				binaryWriter.Write(A_1[i].ᜁ);
			}
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x000E4668 File Offset: 0x000E3668
	internal spr᭙(int A_0, sprᠣ[] A_1) : this(A_0, A_1.Length, 12)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				binaryWriter.Write(A_1[i].ᜀ);
				binaryWriter.Write(A_1[i].ᜁ);
				binaryWriter.Write(A_1[i].ᜂ);
			}
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000E46F4 File Offset: 0x000E36F4
	internal spr᭙(int A_0, spr\u2055[] A_1) : this(A_0, A_1.Length, 8)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				binaryWriter.Write(spr᭙.ᜀ(A_1[i].ᜂ()));
				binaryWriter.Write(spr᭙.ᜀ(A_1[i].ᜁ()));
			}
		}
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000E477C File Offset: 0x000E377C
	internal spr᭙(int A_0, spr\u2528[] A_1) : this(A_0, A_1.Length, 8)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				binaryWriter.Write((byte)A_1[i].ᜀ);
				binaryWriter.Write((byte)A_1[i].ᜁ);
				binaryWriter.Write((short)A_1[i].ᜂ);
				binaryWriter.Write((short)A_1[i].ᜃ);
				binaryWriter.Write((short)A_1[i].ᜄ);
			}
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x000E4828 File Offset: 0x000E3828
	internal spr᭙(int A_0, sprᥴ[] A_1) : this(A_0, A_1.Length, 36)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				binaryWriter.Write(A_1[i].ᜀ());
				binaryWriter.Write(A_1[i].ᜇ.ᜁ());
				binaryWriter.Write(A_1[i].ᜈ.ᜁ());
				binaryWriter.Write(A_1[i].ᜉ.ᜂ());
				binaryWriter.Write(A_1[i].ᜊ.ᜂ());
				binaryWriter.Write(A_1[i].ᜋ.ᜂ());
				binaryWriter.Write(A_1[i].ᜌ.ᜂ());
				binaryWriter.Write(A_1[i].\u170D.ᜂ());
				binaryWriter.Write(A_1[i].ᜎ.ᜂ());
			}
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x000E4934 File Offset: 0x000E3934
	internal spr᭙(int A_0, spr\u1D34[] A_1) : this(A_0, A_1.Length, 16)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			foreach (spr\u1D34 spr_u1D in A_1)
			{
				binaryWriter.Write(spr᭙.ᜀ(spr_u1D.ᜀ));
				binaryWriter.Write(spr᭙.ᜀ(spr_u1D.ᜁ));
				binaryWriter.Write(spr᭙.ᜀ(spr_u1D.ᜂ));
				binaryWriter.Write(spr᭙.ᜀ(spr_u1D.ᜃ));
			}
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x000E49DC File Offset: 0x000E39DC
	internal spr᭙(int A_0, int[] A_1) : this(A_0, A_1.Length, 4)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				binaryWriter.Write(A_1[i]);
			}
		}
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x000E4A44 File Offset: 0x000E3A44
	internal spr᭙(int A_0, sprỬ[] A_1) : this(A_0, A_1.Length, 2)
	{
		using (MemoryStream memoryStream = new MemoryStream(this.ᜄ))
		{
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < A_1.Length; i++)
			{
				int num = spr\u1DC6.ᜀ(A_1[i]);
				binaryWriter.Write((ushort)num);
			}
		}
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x000E4AB4 File Offset: 0x000E3AB4
	internal override void ᜀ(BinaryReader A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1CA;
			case 1:
				if (this.ᜁ * this.ᜃ > base.ᜊ())
				{
					goto IL_F4;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_139;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				num = 4;
				continue;
			case 4:
				if (true)
				{
				}
				if (this.ᜂ(this.ᜃ))
				{
					num = 5;
					continue;
				}
				goto IL_F4;
			case 5:
				num = 9;
				continue;
			case 6:
				return;
			case 7:
				this.ᜃ = 4;
				num = 0;
				continue;
			case 8:
				goto IL_139;
			case 9:
				if (this.ᜁ != 0)
				{
					num = 13;
					continue;
				}
				goto IL_F4;
			case 10:
				goto IL_F4;
			case 11:
				if (this.ᜃ == 0)
				{
					num = 10;
					continue;
				}
				goto IL_1CC;
			case 12:
				if (this.ᜃ == 65520)
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
			case 13:
				num = 11;
				continue;
			}
			if (base.ᜊ() == 0)
			{
				num = 6;
				continue;
			}
			this.ᜁ = (int)A_0.ReadUInt16();
			this.ᜂ = (int)A_0.ReadUInt16();
			this.ᜃ = (int)A_0.ReadUInt16();
			num = 12;
			continue;
			IL_F4:
			A_0.BaseStream.Position = 0L;
			this.ᜃ = 4;
			this.ᜁ = base.ᜊ() / this.ᜃ;
			this.ᜂ = base.ᜊ() / this.ᜃ;
			num = 8;
		}
		return;
		IL_139:
		IL_1CA:
		IL_1CC:
		this.ᜄ = A_0.ReadBytes(this.ᜁ * this.ᜃ);
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000E4CA8 File Offset: 0x000E3CA8
	public bool ᜂ(int A_0)
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
				if (this.ᜃ <= 36)
				{
					num = 3;
					continue;
				}
				goto IL_51;
			case 3:
				goto IL_4F;
			}
			if (A_0 < 2)
			{
				goto IL_51;
			}
			num = 0;
		}
		IL_4F:
		return A_0 % 2 == 0;
		IL_51:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_4F;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x000E4D38 File Offset: 0x000E3D38
	internal override void ᜀ(BinaryWriter A_0)
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
		A_0.Write((short)this.ᜁ);
		A_0.Write((short)this.ᜂ);
		A_0.Write((short)this.ᜃ);
		A_0.Write(this.ᜄ);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x000E4DA8 File Offset: 0x000E3DA8
	internal spr\u2143[] ᜅ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 1:
					goto IL_40;
				case 2:
					if (true)
					{
					}
					try
					{
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							spr\u2143[] array = new spr\u2143[this.ᜁ];
							int num2 = 0;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_BC;
								case 1:
								{
									spr\u2143[] result = array;
									num = 4;
									continue;
								}
								case 2:
									if (num2 >= array.Length)
									{
										num = 1;
										continue;
									}
									array[num2] = new spr\u2143
									{
										ᜀ = spr\u20D8.ᜀ(binaryReader.ReadInt32()),
										ᜁ = binaryReader.ReadInt32()
									};
									num2++;
									num = 3;
									continue;
								case 3:
									goto IL_BC;
								case 4:
									goto IL_E8;
								}
								break;
								IL_BC:
								num = 2;
							}
						}
						IL_E8:
						goto IL_14F;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_125;
							case 2:
								((IDisposable)memoryStream).Dispose();
								num = 0;
								continue;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_125:;
					}
					goto IL_128;
				}
				if (this.ᜁ == 0)
				{
					num = 1;
					continue;
				}
				IL_128:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 2;
			}
			IL_40:
			IL_14D:
			return null;
			IL_14F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_14D;
			default:
			{
				if (false)
				{
				}
				spr\u2143[] result;
				return result;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x000E4F3C File Offset: 0x000E3F3C
	internal sprᠣ[] ᜃ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					goto IL_48;
				case 1:
					try
					{
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							sprᠣ[] array = new sprᠣ[this.ᜁ];
							int num2 = 0;
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									sprᠣ[] result = array;
									num = 4;
									continue;
								}
								case 1:
									goto IL_CC;
								case 2:
									if (num2 >= array.Length)
									{
										num = 0;
										continue;
									}
									array[num2] = new sprᠣ
									{
										ᜀ = binaryReader.ReadInt32(),
										ᜁ = binaryReader.ReadInt32(),
										ᜂ = binaryReader.ReadInt32()
									};
									num2++;
									num = 1;
									continue;
								case 3:
									goto IL_CC;
								case 4:
									goto IL_FB;
								}
								break;
								IL_CC:
								num = 2;
							}
						}
						IL_FB:
						goto IL_15A;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_138;
							case 2:
								((IDisposable)memoryStream).Dispose();
								num = 1;
								continue;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_138:;
					}
					goto IL_13B;
				}
				if (true)
				{
				}
				if (this.ᜁ == 0)
				{
					num = 0;
					continue;
				}
				IL_13B:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 1;
			}
			IL_48:
			IL_158:
			return null;
			IL_15A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_158;
			default:
			{
				if (false)
				{
				}
				sprᠣ[] result;
				return result;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x000E50DC File Offset: 0x000E40DC
	internal spr\u2055[] ᜈ()
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					try
					{
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							spr\u2055[] array = new spr\u2055[this.ᜁ];
							int num2 = this.ᜃ;
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_168;
								case 1:
								{
									int num3;
									if (num3 >= array.Length)
									{
										num = 9;
										continue;
									}
									int a_2 = binaryReader.ReadInt32();
									int a_3 = binaryReader.ReadInt32();
									array[num3] = new spr\u2055(spr᭙.ᜁ(a_2), spr᭙.ᜁ(a_3));
									num3++;
									num = 6;
									continue;
								}
								case 2:
									goto IL_213;
								case 3:
									goto IL_213;
								case 4:
								{
									if (num2 != 4)
									{
										num = 15;
										continue;
									}
									int num4 = 0;
									num = 10;
									continue;
								}
								case 5:
									num = 2;
									continue;
								case 6:
									goto IL_168;
								case 7:
								{
									int num4;
									if (num4 >= array.Length)
									{
										num = 5;
										continue;
									}
									int a_4 = (int)binaryReader.ReadInt16();
									int a_5 = (int)binaryReader.ReadInt16();
									array[num4] = new spr\u2055(a_4, a_5);
									num4++;
									num = 12;
									continue;
								}
								case 8:
									goto IL_E2;
								case 9:
									num = 3;
									continue;
								case 10:
									goto IL_1DC;
								case 11:
									num = 8;
									continue;
								case 12:
									goto IL_1DC;
								case 13:
									goto IL_222;
								case 14:
								{
									if (num2 != 8)
									{
										num = 11;
										continue;
									}
									int num3 = 0;
									num = 0;
									continue;
								}
								case 15:
									num = 14;
									continue;
								}
								break;
								IL_168:
								num = 1;
								continue;
								IL_1DC:
								num = 7;
								continue;
								IL_213:
								spr\u2055[] result = array;
								num = 13;
							}
						}
						IL_E2:
						throw new InvalidOperationException(ClipboardData.b("⁴᥶ቸᕺቼࡾꎂﮈﮎ놐杖릘ﺞ토욢薤힦좨\udfaa얬膮", a_));
						IL_222:
						goto IL_281;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_25F;
							case 2:
								((IDisposable)memoryStream).Dispose();
								num = 1;
								continue;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_25F:;
					}
					goto IL_262;
				case 1:
					goto IL_51;
				}
				if (true)
				{
				}
				if (this.ᜁ == 0)
				{
					num = 1;
					continue;
				}
				IL_262:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 0;
			}
			IL_51:
			IL_27F:
			return null;
			IL_281:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_27F;
			default:
			{
				if (false)
				{
				}
				spr\u2055[] result;
				return result;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x000E53B0 File Offset: 0x000E43B0
	internal sprᥴ[] ᜉ()
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					goto IL_40;
				case 2:
					try
					{
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							sprᥴ[] array = new sprᥴ[this.ᜁ];
							int num2 = 0;
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									sprᥴ[] result = array;
									num = 2;
									continue;
								}
								case 1:
									goto IL_1FA;
								case 2:
									goto IL_22C;
								case 3:
								{
									if (num2 >= array.Length)
									{
										num = 0;
										continue;
									}
									sprᥴ sprᥴ = new sprᥴ();
									int num3 = binaryReader.ReadInt32();
									sprᥴ.ᜀ = ((num3 & 8192) != 0);
									sprᥴ.ᜁ = ((num3 & 32) != 0);
									sprᥴ.ᜂ = ((num3 & 16) != 0);
									sprᥴ.ᜃ = ((num3 & 8) != 0);
									sprᥴ.ᜄ = ((num3 & 4) != 0);
									sprᥴ.ᜆ = ((num3 & 2) != 0);
									sprᥴ.ᜅ = ((num3 & 1) != 0);
									sprᥴ.ᜇ = new sprᶂ(spr᭙.ᜀ(binaryReader, true).ᜂ());
									sprᥴ.ᜈ = new sprᶂ(spr᭙.ᜀ(binaryReader, true).ᜂ());
									sprᥴ.ᜉ = spr᭙.ᜀ(binaryReader, (num3 & 2048) != 0);
									sprᥴ.ᜊ = spr᭙.ᜀ(binaryReader, (num3 & 4096) != 0);
									sprᥴ.ᜋ = spr᭙.ᜀ(binaryReader, (num3 & 128) != 0);
									sprᥴ.ᜌ = spr᭙.ᜀ(binaryReader, (num3 & 256) != 0);
									sprᥴ.\u170D = spr᭙.ᜀ(binaryReader, (num3 & 512) != 0);
									sprᥴ.ᜎ = spr᭙.ᜀ(binaryReader, (num3 & 1024) != 0);
									array[num2] = sprᥴ;
									num2++;
									num = 1;
									continue;
								}
								case 4:
									goto IL_1FA;
								}
								break;
								IL_1FA:
								num = 3;
							}
						}
						IL_22C:
						goto IL_293;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_269;
							case 2:
								((IDisposable)memoryStream).Dispose();
								num = 0;
								continue;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_269:;
					}
					goto IL_26C;
				}
				if (this.ᜁ == 0)
				{
					num = 0;
					continue;
				}
				IL_26C:
				memoryStream = new MemoryStream(this.ᜄ);
				if (true)
				{
				}
				num = 2;
			}
			IL_40:
			IL_291:
			return null;
			IL_293:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_291;
			default:
			{
				if (false)
				{
				}
				sprᥴ[] result;
				return result;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000E5694 File Offset: 0x000E4694
	private static sprṚ ᜀ(BinaryReader A_0, bool A_1)
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
		int a_ = A_0.ReadInt32();
		return new sprṚ(a_, A_1);
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x000E56E0 File Offset: 0x000E46E0
	internal spr\u1D34[] ᜆ()
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					try
					{
						spr\u1D34[] result;
						for (;;)
						{
							spr\u1D34[] array = new spr\u1D34[this.ᜁ];
							int num2 = 0;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_E8;
								case 1:
									goto IL_E8;
								case 2:
									goto IL_11D;
								case 3:
								{
									if (num2 >= this.ᜁ)
									{
										num = 4;
										continue;
									}
									BinaryReader binaryReader = new BinaryReader(memoryStream);
									array[num2] = new spr\u1D34
									{
										ᜀ = spr᭙.ᜁ(binaryReader.ReadInt32()),
										ᜁ = spr᭙.ᜁ(binaryReader.ReadInt32()),
										ᜂ = spr᭙.ᜁ(binaryReader.ReadInt32()),
										ᜃ = spr᭙.ᜁ(binaryReader.ReadInt32())
									};
									num2++;
									num = 1;
									continue;
								}
								case 4:
									result = array;
									num = 2;
									continue;
								}
								break;
								IL_E8:
								num = 3;
							}
						}
						IL_11D:
						return result;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									((IDisposable)memoryStream).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_179;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 1;
						}
						IL_179:;
					}
					goto IL_17C;
				case 2:
					goto IL_40;
				}
				if (this.ᜃ == 0)
				{
					num = 2;
					continue;
				}
				IL_17C:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 0;
			}
			IL_40:
			return null;
		}
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x000E58AC File Offset: 0x000E48AC
	internal static int ᜀ(sprṚ A_0)
	{
		int num;
		for (;;)
		{
			num = A_0.ᜂ();
			int num2 = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					return num;
				case 1:
					goto IL_57;
				case 2:
					if (A_0.ᜁ())
					{
						num2 = 1;
						continue;
					}
					return num;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						if (spr᭙.ᜀ(num))
						{
							num2 = 4;
							continue;
						}
						return num;
					}
					break;
				case 4:
					num |= int.MinValue;
					num2 = 0;
					continue;
				}
				break;
				IL_57:
				num2 = 3;
			}
		}
		return num;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x000E5954 File Offset: 0x000E4954
	internal static sprṚ ᜁ(int A_0)
	{
		int num = 2;
		int a_;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (spr᭙.ᜀ(a_))
				{
					num = 1;
					continue;
				}
				goto IL_8B;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_55;
				default:
					goto IL_83;
				}
				break;
			case 3:
				a_ = (A_0 & int.MaxValue);
				goto IL_55;
			}
			if ((A_0 & -2147483648) != 0)
			{
				if (true)
				{
				}
				num = 3;
				continue;
			}
			goto IL_8B;
			IL_55:
			num = 0;
		}
		IL_83:
		if (false)
		{
		}
		return new sprṚ(a_, true);
		IL_8B:
		return new sprṚ(A_0, false);
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x000E59F4 File Offset: 0x000E49F4
	private static bool ᜀ(int A_0)
	{
		if (A_0 < 0)
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
				return false;
			}
		}
		if (true)
		{
		}
		return A_0 <= 127;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000E5A40 File Offset: 0x000E4A40
	internal spr\u2528[] ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					try
					{
						spr\u2528[] result;
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							spr\u2528[] array = new spr\u2528[this.ᜁ];
							int num2 = 0;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_E9;
								case 1:
									if (num2 >= array.Length)
									{
										num = 3;
										continue;
									}
									array[num2] = new spr\u2528
									{
										ᜀ = (Operation)binaryReader.ReadByte(),
										ᜁ = (int)binaryReader.ReadByte(),
										ᜂ = (int)binaryReader.ReadInt16(),
										ᜃ = (int)binaryReader.ReadInt16(),
										ᜄ = (int)binaryReader.ReadInt16()
									};
									num2++;
									num = 2;
									continue;
								case 2:
									goto IL_E9;
								case 3:
									result = array;
									num = 4;
									continue;
								case 4:
									goto IL_118;
								}
								break;
								IL_E9:
								num = 1;
							}
						}
						IL_118:
						return result;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									((IDisposable)memoryStream).Dispose();
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_174;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 0;
						}
						IL_174:;
					}
					goto IL_177;
				case 2:
					goto IL_48;
				}
				if (true)
				{
				}
				if (this.ᜁ == 0)
				{
					num = 2;
					continue;
				}
				IL_177:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 0;
			}
			IL_48:
			return null;
		}
		}
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000E5C00 File Offset: 0x000E4C00
	internal sprỬ[] ᜇ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 1:
					try
					{
						sprỬ[] result;
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							sprỬ[] array = new sprỬ[this.ᜁ];
							int num2 = 0;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (num2 >= array.Length)
									{
										num = 4;
										continue;
									}
									int a_ = (int)binaryReader.ReadUInt16();
									array[num2] = spr\u1DC6.ᜀ(a_);
									num2++;
									num = 1;
									continue;
								}
								case 1:
									goto IL_AB;
								case 2:
									goto IL_AB;
								case 3:
									goto IL_D4;
								case 4:
									result = array;
									num = 3;
									continue;
								}
								break;
								IL_AB:
								num = 0;
							}
						}
						IL_D4:
						return result;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_130;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									((IDisposable)memoryStream).Dispose();
									num = 0;
									continue;
								}
								break;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 2;
						}
						IL_130:;
					}
					goto IL_133;
				case 2:
					goto IL_40;
				}
				if (this.ᜁ == 0)
				{
					num = 2;
					continue;
				}
				IL_133:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 1;
			}
			IL_40:
			if (true)
			{
			}
			return null;
		}
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x000E5D7C File Offset: 0x000E4D7C
	internal int[] ᜄ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				MemoryStream memoryStream;
				switch (num)
				{
				case 0:
					try
					{
						int[] result;
						for (;;)
						{
							BinaryReader binaryReader = new BinaryReader(memoryStream);
							int[] array = new int[this.ᜁ];
							int num2 = 0;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (num2 >= array.Length)
									{
										num = 3;
										continue;
									}
									array[num2] = binaryReader.ReadInt32();
									num2++;
									num = 2;
									continue;
								case 1:
									goto IL_A2;
								case 2:
									goto IL_A2;
								case 3:
									result = array;
									num = 4;
									continue;
								case 4:
									goto IL_C8;
								}
								break;
								IL_A2:
								num = 0;
							}
						}
						IL_C8:
						return result;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									((IDisposable)memoryStream).Dispose();
									num = 1;
									continue;
								}
								break;
							case 1:
								goto IL_124;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 0;
						}
						IL_124:;
					}
					goto IL_127;
				case 2:
					goto IL_48;
				}
				if (this.ᜁ == 0)
				{
					num = 2;
					continue;
				}
				IL_127:
				memoryStream = new MemoryStream(this.ᜄ);
				num = 0;
			}
			IL_48:
			return null;
		}
		}
	}

	// Token: 0x04001732 RID: 5938
	private new const int ᜀ = -2147483648;

	// Token: 0x04001733 RID: 5939
	private int ᜁ;

	// Token: 0x04001734 RID: 5940
	private int ᜂ;

	// Token: 0x04001735 RID: 5941
	private int ᜃ;

	// Token: 0x04001736 RID: 5942
	private byte[] ᜄ;
}
