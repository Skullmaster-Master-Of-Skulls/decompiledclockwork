using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002FC RID: 764
internal class spr\u2604 : spr\u2496
{
	// Token: 0x06002F10 RID: 12048 RVA: 0x001A4D28 File Offset: 0x001A3D28
	internal spr\u19E8 ᜅ()
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

	// Token: 0x06002F11 RID: 12049 RVA: 0x001A4D6C File Offset: 0x001A3D6C
	public spr᱐ \u170D()
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

	// Token: 0x06002F12 RID: 12050 RVA: 0x001A4DB0 File Offset: 0x001A3DB0
	public spr\u20C3 ᜋ()
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
		return this.ᜆ;
	}

	// Token: 0x06002F13 RID: 12051 RVA: 0x001A4DF4 File Offset: 0x001A3DF4
	internal spr\u23D5 ᜌ()
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
		return this.ᜄ;
	}

	// Token: 0x06002F14 RID: 12052 RVA: 0x001A4E38 File Offset: 0x001A3E38
	internal sprប ᜈ()
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

	// Token: 0x06002F15 RID: 12053 RVA: 0x001A4E7C File Offset: 0x001A3E7C
	internal Stream ᜉ()
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

	// Token: 0x06002F16 RID: 12054 RVA: 0x001A4EC0 File Offset: 0x001A3EC0
	internal bool ᜄ()
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
		return this.ᜊ;
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x001A4F04 File Offset: 0x001A3F04
	internal void ᜀ(bool A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x06002F18 RID: 12056 RVA: 0x001A4F48 File Offset: 0x001A3F48
	public static void ᜃ()
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			spr\u2604 spr_u = new spr\u2604();
			FileStream fileStream = new FileStream(RecordTableEnumerator.b("爵ȷ昹樻簽Ŀفⵃ❅⑇╉⭋恍⡏㹑❓", a_), FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				spr_u.ᜃ(fileStream);
				spr\u20C3 spr_u20C = spr_u.ᜋ();
				spr\u1FDC spr_u1FDC = spr_u20C.ᜁ(RecordTableEnumerator.b("愵圷䠹圻尽⼿ⵁ⽃", a_));
				Console.WriteLine(spr_u1FDC.Length);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_C5;
					case 2:
						((IDisposable)fileStream).Dispose();
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C7;
					default:
						if (false)
						{
						}
						if (fileStream == null)
						{
							goto IL_C7;
						}
						num = 2;
						break;
					}
				}
				IL_C5:
				IL_C7:;
			}
			return;
		}
		}
	}

	// Token: 0x06002F19 RID: 12057 RVA: 0x001A5038 File Offset: 0x001A4038
	private static void ᜀ(string A_0, spr᱐ A_1)
	{
		int a_ = 10;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				string path = Path.Combine(A_0, RecordTableEnumerator.b("␿⭁㙃⍅⭇㹉⍋㱍⥏籑⁓⹕ⱗ", a_));
				StreamWriter streamWriter = new StreamWriter(path);
				try
				{
					for (;;)
					{
						List<spr\u1DAB> list = A_1.ᜁ();
						int num = 0;
						int count = list.Count;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								spr\u1DAB spr_u1DAB = list[num];
								streamWriter.WriteLine(new string('-', 20));
								streamWriter.WriteLine(RecordTableEnumerator.b("Կⱁぃ㑅ㅇ͉⡋瑍灏⥑摓⭕", a_), spr_u1DAB.ᜁ());
								streamWriter.WriteLine(RecordTableEnumerator.b("฿⍁⥃⍅片橉㝋繍ⵏ繑瑓ፕ㙗⹙⹛❝㑟᭡ᑣͥ剧䩩ᝫ彭൯", a_), spr_u1DAB.ᜀ(), spr_u1DAB.ᜄ());
								streamWriter.WriteLine(RecordTableEnumerator.b("ి❁≃㉅片橉㝋繍ⵏ繑瑓ѕㅗ㵙㑛⩝婟䉡ὣ坥ᕧ䙩䱫⵭ᡯ᭱ᡳት䉷婹ݻ䱽ﵿ", a_), spr_u1DAB.ᜈ(), spr_u1DAB.ᜉ(), spr_u1DAB.ᜅ());
								streamWriter.WriteLine(RecordTableEnumerator.b("ܿ㝁ⵃ≅片橉㝋繍ⵏ繑瑓ቕ㥗⹙㥛ᵝ቟ݡգብ൧偩䱫ᕭ䅯ཱ塳噵㱷᭹ࡻ᭽쵿뚋꺍ꂑ", a_), spr_u1DAB.ᜇ(), spr_u1DAB.ᜊ(), spr_u1DAB.ᜂ());
								streamWriter.WriteLine(RecordTableEnumerator.b("ጿ㙁╃㑅㱇᥉⥋ⵍ⑏㵑♓汕硗⅙汛⍝䱟䉡㝣ཥቧཀྵ噫乭୯䍱ॳ", a_), spr_u1DAB.ᜃ(), spr_u1DAB.ᜌ());
								num++;
								num2 = 1;
								continue;
							}
							case 1:
								goto IL_1A4;
							case 2:
								goto IL_1D2;
							case 3:
								num2 = 2;
								continue;
							case 4:
								goto IL_1A4;
							}
							break;
							IL_1A4:
							num2 = 0;
						}
					}
					IL_1D2:;
				}
				finally
				{
					if (true)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							goto IL_217;
						case 2:
							((IDisposable)streamWriter).Dispose();
							num2 = 1;
							continue;
						}
						if (streamWriter == null)
						{
							break;
						}
						num2 = 2;
					}
					IL_217:;
				}
				break;
			}
			}
			break;
		}
	}

	// Token: 0x06002F1A RID: 12058 RVA: 0x001A527C File Offset: 0x001A427C
	private static void ᜀ(string A_0, spr\u20C3 A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string[] array = A_1.ᜁ();
				int num = 0;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_E4;
					case 1:
					{
						try
						{
							string text;
							spr\u20C3 spr_u20C;
							spr\u2604.ᜀ(text, spr_u20C);
							goto IL_12B;
						}
						finally
						{
							num2 = 1;
							for (;;)
							{
								spr\u20C3 spr_u20C;
								switch (num2)
								{
								case 0:
									goto IL_B4;
								case 2:
									spr_u20C.Dispose();
									num2 = 0;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_B6;
								default:
									if (false)
									{
									}
									if (spr_u20C == null)
									{
										goto IL_B6;
									}
									num2 = 2;
									break;
								}
							}
							IL_B4:
							IL_B6:;
						}
						goto IL_B7;
						IL_12B:
						int num3;
						num3++;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_E4;
					case 3:
						goto IL_B7;
					case 4:
						return;
					case 5:
						goto IL_B7;
					case 6:
					{
						string[] array2 = A_1.ᜂ();
						int num3 = 0;
						num2 = 2;
						continue;
					}
					case 7:
					{
						int num3;
						string[] array2;
						if (num3 >= array2.Length)
						{
							num2 = 4;
							continue;
						}
						string text2 = array2[num3];
						string text = Path.Combine(A_0, text2);
						Directory.CreateDirectory(text);
						spr\u20C3 spr_u20C = A_1.ᜅ(text2);
						num2 = 1;
						continue;
					}
					case 8:
					{
						if (true)
						{
						}
						if (num >= array.Length)
						{
							num2 = 6;
							continue;
						}
						string a_ = array[num];
						spr\u2604.ᜀ(A_0, a_, A_1);
						num++;
						num2 = 3;
						continue;
					}
					}
					break;
					IL_B7:
					num2 = 8;
					continue;
					IL_E4:
					num2 = 7;
				}
			}
			return;
		}
	}

	// Token: 0x06002F1B RID: 12059 RVA: 0x001A542C File Offset: 0x001A442C
	private static void ᜀ(string A_0, string A_1, spr\u20C3 A_2)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			byte[] buffer = new byte[32768];
			Stream stream = A_2.ᜁ(A_1);
			try
			{
				int num = 1;
				for (;;)
				{
					FileStream fileStream;
					switch (num)
					{
					case 0:
						goto IL_67;
					case 2:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 4;
									continue;
								case 2:
								{
									int count;
									if ((count = stream.Read(buffer, 0, 32768)) <= 0)
									{
										num = 0;
										continue;
									}
									fileStream.Write(buffer, 0, count);
									num = 1;
									continue;
								}
								case 4:
									goto IL_121;
								}
								IL_DC:
								num = 2;
								continue;
								goto IL_DC;
							}
							IL_121:;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_15E;
								case 1:
									((IDisposable)fileStream).Dispose();
									num = 0;
									continue;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 1;
							}
							IL_15E:;
						}
						num = 3;
						continue;
					case 3:
						goto IL_16D;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							if (false)
							{
							}
							goto IL_97;
						}
						break;
					}
					if (A_1[0] < ' ')
					{
						num = 0;
						continue;
					}
					goto IL_97;
					IL_67:
					A_1 = A_1.Substring(1);
					num = 4;
					continue;
					IL_97:
					string path = Path.Combine(A_0, A_1);
					fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
					num = 2;
				}
				IL_16D:;
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)stream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_1AA;
					}
					if (stream == null)
					{
						break;
					}
					num = 0;
				}
				IL_1AA:;
			}
			return;
		}
		}
	}

	// Token: 0x06002F1C RID: 12060 RVA: 0x001A5624 File Offset: 0x001A4624
	public spr\u2604()
	{
		this.ᜁ = new MemoryStream();
		this.ᜂ();
	}

	// Token: 0x06002F1D RID: 12061 RVA: 0x001A5648 File Offset: 0x001A4648
	public spr\u2604(Stream A_0)
	{
		this.ᜃ(A_0);
	}

	// Token: 0x06002F1E RID: 12062 RVA: 0x001A5664 File Offset: 0x001A4664
	public spr\u2604(string A_0, bool A_1)
	{
		if (!A_1)
		{
			for (;;)
			{
				using (FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					this.ᜃ(fileStream);
					break;
				}
			}
		}
		else
		{
			this.ᜁ = new FileStream(A_0, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			this.ᜂ();
		}
	}

	// Token: 0x06002F1F RID: 12063 RVA: 0x001A56C8 File Offset: 0x001A46C8
	public void ᜃ(Stream A_0)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
				{
					int num2;
					if (num2 >= 0)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 3:
				{
					int num2;
					this.ᜇ = new MemoryStream(this.ᜃ.ᜀ(this.ᜁ, num2, this));
					this.ᜈ = new MemoryStream(this.ᜃ.ᜀ(this.ᜁ, this.ᜂ.\u1712(), this));
					this.ᜉ = new sprប(this.ᜇ, this.ᜂ.ᜑ(), this.ᜈ, 0);
					this.ᜃ.ᜅ(num2);
					this.ᜃ.ᜅ(this.ᜂ.\u1712());
					goto IL_10C;
				}
				case 4:
					goto IL_70;
				}
				if (A_0 != null)
				{
					long position = A_0.Position;
					long length = A_0.Length;
					int num3 = (int)(length - position);
					MemoryStream memoryStream = new MemoryStream(num3);
					memoryStream.SetLength((long)num3);
					A_0.Read(memoryStream.GetBuffer(), 0, num3);
					memoryStream.Position = 0L;
					this.ᜁ = memoryStream;
					this.ᜂ = new spr\u19E8(this.ᜁ);
					this.ᜄ = new spr\u23D5(this.ᜁ, this.ᜂ);
					this.ᜃ = new sprប(this, this.ᜁ, this.ᜄ, this.ᜂ);
					byte[] a_2 = this.ᜃ.ᜀ(this.ᜁ, this.ᜂ.ᜐ(), this);
					this.ᜅ = new spr᱐(a_2);
					spr\u1DAB spr_u1DAB = this.ᜅ.ᜁ()[0];
					this.ᜆ = new sprឤ(this, spr_u1DAB);
					int num2 = spr_u1DAB.ᜃ();
					num = 2;
					continue;
				}
				if (true)
				{
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
					num = 4;
					continue;
				}
				IL_10C:
				num = 0;
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹䠻䰽┿⍁⥃", a_));
		}
		}
	}

	// Token: 0x06002F20 RID: 12064 RVA: 0x001A5908 File Offset: 0x001A4908
	private void ᜂ()
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
		this.ᜅ = new spr᱐();
		this.ᜆ = new sprឤ(this, RecordTableEnumerator.b("ᑅ❇╉㡋湍ᕏ㱑⁓⑕⅗", a_), 0);
		this.ᜁ.SetLength(512L);
		this.ᜂ = new spr\u19E8();
		this.ᜄ = new spr\u23D5();
		spr\u1DAB spr_u1DAB = this.ᜆ.ᜂ();
		spr_u1DAB.ᜀ(spr\u1DAB.EntryType.Root);
		this.ᜅ.ᜀ(spr_u1DAB);
		this.ᜃ = new sprប(this.ᜁ, this.ᜂ.\u170D(), 512);
	}

	// Token: 0x06002F21 RID: 12065 RVA: 0x001A59DC File Offset: 0x001A49DC
	internal void ᜀ(byte[] A_0, int A_1, int A_2, spr\u19E8 A_3)
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
		ushort a_ = A_3.\u170D();
		int count = A_3.ᜁ();
		long position = spr\u2604.ᜀ(A_2, a_);
		this.ᜁ.Position = position;
		this.ᜁ.Read(A_0, A_1, count);
	}

	// Token: 0x06002F22 RID: 12066 RVA: 0x001A5A4C File Offset: 0x001A4A4C
	internal Stream ᜀ(spr\u1DAB A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				sprប sprប;
				Stream a_2;
				Stream stream;
				Stream stream2;
				byte[] array;
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 1:
					sprប = this.ᜉ;
					a_2 = this.ᜇ;
					num = 3;
					continue;
				case 2:
					stream = new MemoryStream();
					goto IL_147;
				case 3:
					goto IL_F2;
				case 4:
					goto IL_F2;
				case 5:
					return stream2;
				case 6:
					if (true)
					{
					}
					num = 14;
					continue;
				case 7:
					if (A_0.ᜌ() < this.ᜂ.ᜉ())
					{
						num = 1;
						continue;
					}
					goto IL_86;
				case 8:
					goto IL_71;
				case 9:
					IL_142:
					num = 2;
					continue;
				case 10:
					if (A_0.ᜄ() == spr\u1DAB.EntryType.Stream)
					{
						num = 6;
						continue;
					}
					return stream2;
				case 11:
					stream = new MemoryStream(array);
					goto IL_147;
				case 13:
					if (array == null)
					{
						num = 9;
						continue;
					}
					num = 11;
					continue;
				case 14:
					if (this.ᜉ != null)
					{
						num = 0;
						continue;
					}
					goto IL_86;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				stream2 = null;
				num = 10;
				continue;
				IL_86:
				sprប = this.ᜃ;
				a_2 = this.ᜁ;
				num = 4;
				continue;
				IL_F2:
				array = sprប.ᜀ(a_2, A_0.ᜃ(), this);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_142;
				default:
					if (false)
					{
					}
					num = 13;
					continue;
				}
				IL_147:
				stream2 = stream;
				stream2.SetLength((long)((ulong)A_0.ᜌ()));
				num = 5;
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("娾⽀㝂㝄㹆", a_));
		}
		}
	}

	// Token: 0x06002F23 RID: 12067 RVA: 0x001A5C3C File Offset: 0x001A4C3C
	internal void ᜂ(spr\u1DAB A_0, Stream A_1)
	{
		int a_ = 16;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3C;
			case 1:
				goto IL_CB;
			case 2:
				if (A_1.Length >= (long)((ulong)this.ᜂ.ᜉ()))
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					this.ᜀ(A_0, A_1);
					num = 1;
					continue;
				}
				break;
			case 3:
				if (true)
				{
				}
				this.ᜁ(A_0, A_1);
				num = 5;
				continue;
			case 5:
				goto IL_60;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 2;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("⍅♇㹉㹋㝍", a_));
		IL_60:
		IL_CB:
		A_0.ᜀ((uint)A_1.Length);
	}

	// Token: 0x06002F24 RID: 12068 RVA: 0x001A5D24 File Offset: 0x001A4D24
	private void ᜁ(spr\u1DAB A_0, Stream A_1)
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
		this.ᜂ.\u170D();
		int num = this.ᜂ.ᜁ();
		long num2 = (long)((ulong)A_0.ᜌ());
		long length = A_1.Length;
		int a_ = (int)Math.Ceiling((double)num2 / (double)num);
		int a_2 = (int)Math.Ceiling((double)length / (double)num);
		this.ᜀ(A_0, a_, a_2, this.ᜃ);
		this.ᜀ(this.ᜁ, A_0.ᜃ(), A_1, this.ᜃ);
	}

	// Token: 0x06002F25 RID: 12069 RVA: 0x001A5DC8 File Offset: 0x001A4DC8
	private void ᜀ(spr\u1DAB A_0, Stream A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					goto IL_85;
				case 2:
					goto IL_83;
				case 3:
					if (this.ᜉ == null)
					{
						num = 5;
						continue;
					}
					goto IL_D9;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜇ = new MemoryStream();
						num = 1;
						continue;
					}
					break;
				case 5:
					this.ᜉ = new sprប(this.ᜇ, this.ᜂ.ᜑ(), 0);
					num = 2;
					continue;
				}
				IL_40:
				if (this.ᜇ == null)
				{
					num = 4;
					continue;
				}
				goto IL_85;
				goto IL_40;
				IL_85:
				num = 3;
			}
			IL_83:
			IL_D9:
			this.ᜂ.ᜑ();
			int num2 = this.ᜉ.ᜁ();
			long num3 = (long)((ulong)A_0.ᜌ());
			long length = A_1.Length;
			int a_ = (int)Math.Ceiling((double)num3 / (double)num2);
			int a_2 = (int)Math.Ceiling((double)length / (double)num2);
			this.ᜀ(A_0, a_, a_2, this.ᜉ);
			this.ᜀ(this.ᜇ, A_0.ᜃ(), A_1, this.ᜉ);
			return;
		}
		}
	}

	// Token: 0x06002F26 RID: 12070 RVA: 0x001A5F18 File Offset: 0x001A4F18
	private void ᜀ(Stream A_0, int A_1, Stream A_2, sprប A_3)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 1;
			long position2;
			for (;;)
			{
				long position;
				byte[] buffer;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_14B;
				case 2:
					goto IL_EC;
				case 3:
					position = A_3.ᜆ(A_1);
					num = 2;
					continue;
				case 4:
					goto IL_116;
				case 5:
					goto IL_62;
				case 6:
					if (A_1 >= 0)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_14D;
				case 7:
				{
					int count;
					if ((count = A_2.Read(buffer, 0, num2)) <= 0)
					{
						num = 4;
						continue;
					}
					A_0.Position = position;
					A_0.Write(buffer, 0, count);
					A_1 = A_3.ᜂ(A_1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14B;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				}
				if (A_2 == null)
				{
					num = 5;
					continue;
				}
				position = A_3.ᜆ(A_1);
				num2 = A_3.ᜁ();
				buffer = new byte[num2];
				position2 = A_2.Position;
				A_2.Position = 0L;
				num = 0;
				continue;
				IL_EC:
				num = 7;
				continue;
				IL_14B:
				goto IL_EC;
			}
			IL_62:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁぃ㑅ⵇ⭉⅋", a_));
			IL_116:
			IL_14D:
			A_2.Position = position2;
			return;
		}
		}
	}

	// Token: 0x06002F27 RID: 12071 RVA: 0x001A607C File Offset: 0x001A507C
	private void ᜀ(spr\u1DAB A_0, int A_1, int A_2, sprប A_3)
	{
		int num = 1;
		for (;;)
		{
			int num2;
			int a_;
			int num3;
			switch (num)
			{
			case 0:
				if (num2 < 0)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				A_0.ᜆ(a_);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_84;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 3:
				goto IL_84;
			case 4:
				if (A_0.ᜐ < 0)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			case 5:
				return;
			case 6:
				num = 3;
				continue;
			case 7:
				num3 = A_0.ᜐ;
				goto IL_8D;
			case 8:
				return;
			}
			if (A_1 == A_2)
			{
				num = 5;
				continue;
			}
			if (true)
			{
			}
			num = 4;
			continue;
			IL_8D:
			num2 = num3;
			a_ = this.ᜀ(num2, A_1, A_2, A_3);
			num = 0;
			continue;
			IL_84:
			num3 = A_0.ᜃ();
			goto IL_8D;
		}
	}

	// Token: 0x06002F28 RID: 12072 RVA: 0x001A617C File Offset: 0x001A517C
	private int ᜀ(int A_0, int A_1, int A_2, sprប A_3)
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				if (true)
				{
				}
				result = -1;
				int num = 18;
				for (;;)
				{
					int num2;
					int num5;
					switch (num)
					{
					case 0:
						num2 = A_0;
						goto IL_219;
					case 1:
					{
						if (A_1 < A_2)
						{
							num = 2;
							continue;
						}
						int num3 = 0;
						num = 6;
						continue;
					}
					case 2:
						num = 8;
						continue;
					case 3:
					{
						int num4 = A_3.ᜀ(A_0, A_2 - A_1);
						num = 10;
						continue;
					}
					case 4:
						goto IL_C2;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15E;
						default:
							goto IL_B7;
						}
						break;
					case 6:
						goto IL_1A3;
					case 7:
					{
						int num4;
						result = num4;
						num = 5;
						continue;
					}
					case 8:
						if (A_0 < 0)
						{
							num = 17;
							continue;
						}
						num = 19;
						continue;
					case 9:
					{
						int num3;
						if (num3 >= A_2 - 1)
						{
							num = 13;
							continue;
						}
						A_0 = A_3.ᜂ(A_0);
						num3++;
						num = 15;
						continue;
					}
					case 10:
						if (A_0 < 0)
						{
							num = 7;
							continue;
						}
						return result;
					case 11:
						result = A_0;
						num = 12;
						continue;
					case 12:
						return result;
					case 13:
						goto IL_15E;
					case 14:
						return result;
					case 15:
						goto IL_1A3;
					case 16:
						if (num5 < 0)
						{
							num = 3;
							continue;
						}
						A_0 = num5;
						num5 = A_3.ᜂ(A_0);
						num = 4;
						continue;
					case 17:
						num = 0;
						continue;
					case 18:
						if (A_1 == A_2)
						{
							num = 11;
							continue;
						}
						num = 1;
						continue;
					case 19:
						num2 = A_3.ᜂ(A_0);
						goto IL_219;
					case 20:
						goto IL_C2;
					}
					break;
					IL_C2:
					num = 16;
					continue;
					IL_15E:
					A_3.ᜅ(A_0);
					num = 14;
					continue;
					IL_1A3:
					num = 9;
					continue;
					IL_219:
					num5 = num2;
					num = 20;
				}
			}
			IL_B7:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06002F29 RID: 12073 RVA: 0x001A63B8 File Offset: 0x001A53B8
	[CLSCompliant(false)]
	public static long ᜀ(int A_0, ushort A_1)
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
		return (long)((A_0 << (int)A_1) + 512);
	}

	// Token: 0x06002F2A RID: 12074 RVA: 0x001A6400 File Offset: 0x001A5400
	[CLSCompliant(false)]
	public static long ᜀ(int A_0, ushort A_1, int A_2)
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
		return (long)((A_0 << (int)A_1) + A_2);
	}

	// Token: 0x06002F2B RID: 12075 RVA: 0x001A6444 File Offset: 0x001A5444
	public static bool ᜁ(Stream A_0)
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
		return spr\u19E8.ᜀ(A_0);
	}

	// Token: 0x06002F2C RID: 12076 RVA: 0x001A6488 File Offset: 0x001A5488
	internal spr\u1DAB ᜀ(string A_0, spr\u1DAB.EntryType A_1)
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
		spr\u1DAB spr_u1DAB = new spr\u1DAB(A_0, A_1, this.ᜅ.ᜁ().Count);
		this.ᜅ.ᜀ(spr_u1DAB);
		spr\u1DAB spr_u1DAB2 = spr_u1DAB;
		DateTime now;
		spr_u1DAB.ᜁ(now = DateTime.Now);
		spr_u1DAB2.ᜀ(now);
		return spr_u1DAB;
	}

	// Token: 0x06002F2D RID: 12077 RVA: 0x001A64FC File Offset: 0x001A54FC
	internal void ᜁ(spr\u1DAB A_0)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0.ᜄ() == spr\u1DAB.EntryType.Stream)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30;
				default:
					if (false)
					{
					}
					this.ᜃ.ᜅ(A_0.ᜃ());
					A_0.ᜆ(-1);
					num = 3;
					continue;
				}
				break;
			case 3:
				return;
			case 4:
				goto IL_38;
			}
			goto IL_2D;
			IL_30:
			num = 4;
			continue;
			IL_2D:
			if (A_0 == null)
			{
				goto IL_30;
			}
			A_0.ᜀ(spr\u1DAB.EntryType.Invalid);
			if (true)
			{
			}
			num = 1;
		}
		IL_38:
		throw new ArgumentNullException(RecordTableEnumerator.b("❂ⱄ㕆ⱈ⡊㥌⁎⍐⩒ၔ㥖ⵘ⥚⑜", a_));
	}

	// Token: 0x06002F2E RID: 12078 RVA: 0x001A65D0 File Offset: 0x001A55D0
	internal int ᜀ(spr\u1DAB A_0, long A_1, byte[] A_2, int A_3)
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
	}

	// Token: 0x06002F2F RID: 12079 RVA: 0x001A6628 File Offset: 0x001A5628
	internal void ᜀ(spr\u1DAB A_0, long A_1, byte[] A_2, int A_3, int A_4)
	{
		switch (0)
		{
		default:
		{
			long num3;
			for (;;)
			{
				if (true)
				{
				}
				this.ᜂ.\u170D();
				int num = this.ᜂ.ᜁ();
				long num2 = (long)((ulong)A_0.ᜌ());
				num3 = A_1 + (long)A_4;
				int num4 = (int)Math.Ceiling((double)num2 / (double)num);
				int num5 = (int)Math.Ceiling((double)num3 / (double)num);
				int num6 = 6;
				for (;;)
				{
					int num8;
					int num9;
					int num10;
					switch (num6)
					{
					case 0:
						this.ᜀ(A_0, num4, num5, this.ᜃ);
						num6 = 2;
						continue;
					case 1:
					{
						if (A_4 <= 0)
						{
							num6 = 3;
							continue;
						}
						int num7 = Math.Min(A_4, num8 - num9);
						long position = this.ᜃ.ᜆ(num10) + (long)num9;
						this.ᜁ.Position = position;
						this.ᜁ.Write(A_2, A_3, num7);
						num9 = 0;
						A_3 += num7;
						A_4 -= num7;
						num10 = this.ᜃ.ᜂ(num10);
						goto IL_116;
					}
					case 2:
						goto IL_12E;
					case 3:
						goto IL_1B7;
					case 4:
						goto IL_197;
					case 5:
						goto IL_197;
					case 6:
						if (num5 <= num4)
						{
							goto IL_12E;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_116;
						default:
							if (false)
							{
							}
							num6 = 0;
							continue;
						}
						break;
					}
					break;
					IL_116:
					num6 = 5;
					continue;
					IL_12E:
					num10 = A_0.ᜃ();
					num8 = this.ᜃ.ᜁ();
					int ᜑ = num8;
					this.ᜀ(A_0, A_1, ref ᜑ, ref num10);
					A_0.ᜐ = num10;
					A_0.ᜑ = ᜑ;
					num9 = (int)(A_1 % (long)num8);
					num6 = 4;
					continue;
					IL_197:
					num6 = 1;
				}
			}
			IL_1B7:
			A_0.ᜀ((uint)Math.Max((long)((ulong)A_0.ᜌ()), num3));
			return;
		}
		}
	}

	// Token: 0x06002F30 RID: 12080 RVA: 0x001A6804 File Offset: 0x001A5804
	private void ᜀ(spr\u1DAB A_0, long A_1, ref int A_2, ref int A_3)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = this.ᜃ.ᜁ();
			int num2 = 6;
			for (;;)
			{
				long num3;
				long num5;
				switch (num2)
				{
				case 0:
					num2 = 12;
					continue;
				case 1:
				{
					int num4;
					num3 = A_1 + (long)num - (long)num4;
					goto IL_138;
				}
				case 2:
					A_2 = A_0.ᜑ;
					A_3 = A_0.ᜐ;
					num2 = 7;
					continue;
				case 3:
					return;
				case 4:
					if ((long)A_2 > A_1)
					{
						num2 = 3;
						continue;
					}
					A_3 = this.ᜃ.ᜂ(A_3);
					A_2 += num;
					num2 = 10;
					continue;
				case 5:
					goto IL_C8;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_161;
					default:
						if (false)
						{
						}
						if (A_0.ᜐ >= 0)
						{
							num2 = 11;
							continue;
						}
						goto IL_C8;
					}
					break;
				case 7:
					goto IL_C8;
				case 8:
					if ((long)A_0.ᜑ <= num5)
					{
						num2 = 2;
						continue;
					}
					Debugger.Break();
					num2 = 5;
					continue;
				case 9:
				{
					int num4;
					if (num4 <= 0)
					{
						num2 = 0;
						continue;
					}
					goto IL_161;
				}
				case 10:
					goto IL_C8;
				case 11:
				{
					int num4 = (int)A_1 % num;
					num2 = 9;
					continue;
				}
				case 12:
					num3 = A_1;
					goto IL_138;
				}
				break;
				IL_C8:
				num2 = 4;
				continue;
				IL_138:
				num5 = num3;
				num2 = 8;
				continue;
				IL_161:
				num2 = 1;
			}
		}
	}

	// Token: 0x06002F31 RID: 12081 RVA: 0x001A6988 File Offset: 0x001A5988
	public spr\u20C3 ᜇ()
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
		return this.ᜆ;
	}

	// Token: 0x06002F32 RID: 12082 RVA: 0x001A69CC File Offset: 0x001A59CC
	public void ᜆ()
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
		this.ᜆ.ᜄ();
		this.ᜁ();
		this.ᜀ();
		this.ᜃ.ᜀ(this.ᜁ, this.ᜄ, this.ᜂ);
		this.ᜄ.ᜀ(this.ᜁ, this.ᜂ);
		this.ᜂ.ᜂ(this.ᜁ);
		this.ᜁ.Position = 0L;
	}

	// Token: 0x06002F33 RID: 12083 RVA: 0x001A6A70 File Offset: 0x001A5A70
	public void ᜂ(Stream A_0)
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
		this.ᜆ();
		this.ᜀ(A_0);
	}

	// Token: 0x06002F34 RID: 12084 RVA: 0x001A6AB8 File Offset: 0x001A5AB8
	private void ᜀ(Stream A_0)
	{
		MemoryStream memoryStream;
		for (;;)
		{
			memoryStream = (this.ᜁ as MemoryStream);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					byte[] buffer;
					int count;
					if ((count = this.ᜁ.Read(buffer, 0, 32768)) > 0)
					{
						A_0.Write(buffer, 0, count);
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 1:
					goto IL_52;
				case 2:
				{
					if (memoryStream != null)
					{
						num = 4;
						continue;
					}
					byte[] buffer = new byte[32768];
					if (true)
					{
					}
					num = 3;
					continue;
				}
				case 3:
					goto IL_79;
				case 4:
					goto IL_3F;
				case 5:
					return;
				}
				break;
				IL_79:
				num = 0;
				continue;
				IL_52:
				goto IL_79;
			}
		}
		IL_3F:
		memoryStream.WriteTo(A_0);
	}

	// Token: 0x06002F35 RID: 12085 RVA: 0x001A6B90 File Offset: 0x001A5B90
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_93;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_40;
					default:
						if (false)
						{
						}
						if (this.ᜇ.Length == 0L)
						{
							num = 2;
							continue;
						}
						goto IL_95;
					}
					break;
				}
				goto IL_38;
				IL_40:
				num = 1;
				continue;
				IL_38:
				if (this.ᜇ != null)
				{
					goto IL_40;
				}
				break;
			}
			return;
			IL_93:
			return;
			IL_95:
			int num2 = (int)Math.Ceiling((double)this.ᜇ.Length / (double)this.ᜂ.ᜁ());
			spr\u1DAB spr_u1DAB = this.ᜅ.ᜁ()[0];
			int num3 = spr_u1DAB.ᜃ();
			int a_ = (int)Math.Ceiling(spr_u1DAB.ᜌ() / (double)this.ᜃ.ᜁ());
			num3 = this.ᜀ(num3, a_, num2, this.ᜃ);
			this.ᜀ(this.ᜁ, num3, this.ᜇ, this.ᜃ);
			spr\u1DAB spr_u1DAB2 = this.ᜅ.ᜁ()[0];
			spr_u1DAB2.ᜆ(num3);
			spr_u1DAB2.ᜀ((uint)this.ᜇ.Length);
			MemoryStream memoryStream = new MemoryStream();
			this.ᜉ.ᜀ(memoryStream, this.ᜂ.ᜁ());
			num2 = (int)Math.Ceiling((double)memoryStream.Length / (double)this.ᜂ.ᜁ());
			num3 = this.ᜀ(this.ᜂ.\u1712(), this.ᜂ.ᜆ(), num2, this.ᜃ);
			this.ᜀ(this.ᜁ, num3, memoryStream, this.ᜃ);
			this.ᜂ.ᜄ(num3);
			this.ᜂ.ᜑ();
			this.ᜂ.ᜆ(num2);
			return;
		}
		}
	}

	// Token: 0x06002F36 RID: 12086 RVA: 0x001A6D78 File Offset: 0x001A5D78
	private void ᜀ()
	{
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			int a_;
			int num;
			for (;;)
			{
				memoryStream = new MemoryStream();
				this.ᜅ.ᜀ(memoryStream);
				a_ = (int)Math.Ceiling((double)memoryStream.Length / (double)this.ᜂ.ᜁ());
				num = this.ᜂ.ᜐ();
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_B2;
					case 1:
						goto IL_CC;
					case 2:
						if (num < 0)
						{
							num2 = 3;
							continue;
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
							num2 = 0;
							continue;
						}
						break;
					case 3:
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				}
			}
			IL_B2:
			int num3 = this.ᜃ.ᜃ(num);
			goto IL_CF;
			IL_CC:
			num3 = 0;
			IL_CF:
			int a_2 = num3;
			int num4;
			this.ᜂ.ᜂ(num4 = this.ᜀ(num, a_2, a_, this.ᜃ));
			num = num4;
			this.ᜀ(this.ᜁ, num, memoryStream, this.ᜃ);
			return;
		}
		}
	}

	// Token: 0x06002F37 RID: 12087 RVA: 0x001A6E8C File Offset: 0x001A5E8C
	public void ᜀ(string A_0)
	{
		if (true)
		{
		}
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
			FileStream fileStream = new FileStream(A_0, FileMode.Create, FileAccess.Write, FileShare.None);
			try
			{
				this.ᜂ(fileStream);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						((IDisposable)fileStream).Dispose();
						num = 0;
						continue;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_70:;
			}
			break;
		}
		}
	}

	// Token: 0x06002F38 RID: 12088 RVA: 0x001A6F28 File Offset: 0x001A5F28
	public void ᜊ()
	{
		int num = 1;
		for (;;)
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
				switch (num)
				{
				case 0:
					return;
				case 2:
					goto IL_5C;
				}
				if (this.ᜆ != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			IL_5C:
			this.ᜆ.ᜃ();
			this.ᜆ = null;
			this.ᜁ.Dispose();
			this.ᜁ = null;
			this.ᜂ = null;
			this.ᜃ = null;
			this.ᜅ = null;
			num = 0;
		}
	}

	// Token: 0x04001523 RID: 5411
	private const string ᜀ = "Root Entry";

	// Token: 0x04001524 RID: 5412
	private Stream ᜁ;

	// Token: 0x04001525 RID: 5413
	private spr\u19E8 ᜂ;

	// Token: 0x04001526 RID: 5414
	private sprប ᜃ;

	// Token: 0x04001527 RID: 5415
	private spr\u23D5 ᜄ;

	// Token: 0x04001528 RID: 5416
	private spr᱐ ᜅ;

	// Token: 0x04001529 RID: 5417
	private sprឤ ᜆ;

	// Token: 0x0400152A RID: 5418
	private Stream ᜇ;

	// Token: 0x0400152B RID: 5419
	private Stream ᜈ;

	// Token: 0x0400152C RID: 5420
	private sprប ᜉ;

	// Token: 0x0400152D RID: 5421
	private bool ᜊ;
}
