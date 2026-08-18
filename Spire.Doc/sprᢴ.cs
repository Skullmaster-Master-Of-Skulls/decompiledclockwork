using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000184 RID: 388
internal class sprᢴ
{
	// Token: 0x06000DA4 RID: 3492 RVA: 0x000E3274 File Offset: 0x000E2274
	private sprᢴ()
	{
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x000E3288 File Offset: 0x000E2288
	internal static string ᜁ(ShapeType A_0)
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
		return (string)sprᢴ.ᜀ()[A_0];
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x000E32D8 File Offset: 0x000E22D8
	internal static spr\u2588 ᜀ(ShapeType A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u2588 spr_u;
			for (;;)
			{
				IL_0E:
				for (;;)
				{
					IL_43:
					if (true)
					{
					}
					object obj;
					Monitor.Enter(obj = sprᢴ.ᜃ);
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
						{
							if (false)
							{
							}
							object obj2;
							switch (num)
							{
							case 0:
								if (spr_u == null)
								{
									num = 1;
									continue;
								}
								return spr_u;
							case 1:
								goto IL_191;
							case 2:
								try
								{
									spr_u = (spr\u2588)sprᢴ.ᜂ[A_0];
									goto IL_14B;
								}
								finally
								{
									Monitor.Exit(obj);
								}
								goto IL_191;
							case 3:
								try
								{
									num = 3;
									for (;;)
									{
										string a_;
										switch (num)
										{
										case 0:
											goto IL_136;
										case 1:
											goto IL_142;
										case 2:
											goto IL_136;
										case 4:
											spr_u = spr\u1DE1.ᜃ(new sprᨉ(a_));
											sprᢴ.ᜂ[A_0] = spr_u;
											num = 0;
											continue;
										case 5:
											spr_u = (spr\u2588)sprᢴ.ᜂ[A_0];
											num = 2;
											continue;
										case 6:
											if (spr\u1CC6.ᜋ(a_))
											{
												num = 4;
												continue;
											}
											goto IL_136;
										}
										if (sprᢴ.ᜂ.ContainsKey(A_0))
										{
											num = 5;
											continue;
										}
										sprᢴ.ᜂ[A_0] = null;
										a_ = sprᢴ.ᜁ(A_0);
										num = 6;
										continue;
										IL_136:
										num = 1;
									}
									IL_142:
									return spr_u;
								}
								finally
								{
									Monitor.Exit(obj2);
								}
								goto IL_14B;
							}
							goto IL_43;
							IL_14B:
							num = 0;
							break;
							IL_191:
							Monitor.Enter(obj2 = sprᢴ.ᜃ);
							num = 3;
							break;
						}
						}
					}
				}
			}
			return spr_u;
		}
		}
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x000E34B0 File Offset: 0x000E24B0
	public static string ᜀ(string A_0)
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
		byte[] bytes = Convert.FromBase64String(A_0);
		return Encoding.ASCII.GetString(bytes);
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x000E3500 File Offset: 0x000E2500
	private static Hashtable ᜁ()
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			Stream stream = spr\u1DA1.ᜂ(ClipboardData.b("㑦ᥨɪὬ੮彰㝲ᩴᑶ坸⥺᡼౾ꎌﲎ戀쒠힢횤", a_));
			Hashtable result;
			try
			{
				string s;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_185;
				default:
				{
					if (false)
					{
					}
					s = string.Empty;
					StreamReader streamReader = new StreamReader(stream);
					try
					{
						s = sprᢴ.ᜀ(ClipboardData.b("㝦Ⅸ㉪", a_) + streamReader.ReadToEnd());
						goto IL_185;
					}
					finally
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								((IDisposable)streamReader).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_B7;
							}
							if (streamReader == null)
							{
								break;
							}
							num = 0;
						}
						IL_B7:;
					}
					break;
				}
				}
				TextReader textReader;
				try
				{
					IL_BA:
					for (;;)
					{
						Hashtable hashtable = new Hashtable(220);
						int num2 = -2;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								result = hashtable;
								num = 4;
								continue;
							case 1:
							{
								string value;
								if ((value = textReader.ReadLine()) == null)
								{
									num = 0;
									continue;
								}
								hashtable.Add((ShapeType)num2, value);
								num2++;
								num = 3;
								continue;
							}
							case 2:
								goto IL_F2;
							case 3:
								goto IL_F2;
							case 4:
								goto IL_142;
							}
							break;
							IL_F2:
							num = 1;
						}
					}
					IL_142:
					goto IL_1CF;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)textReader).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_182;
						}
						if (textReader == null)
						{
							break;
						}
						num = 0;
					}
					IL_182:;
				}
				IL_185:
				textReader = new StringReader(s);
				goto IL_BA;
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)stream).Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_1CC;
					}
					if (stream == null)
					{
						break;
					}
					num = 0;
				}
				IL_1CC:;
			}
			IL_1CF:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x000E373C File Offset: 0x000E273C
	private static Hashtable ᜀ()
	{
		for (;;)
		{
			IL_00:
			int num = 2;
			for (;;)
			{
				object obj;
				switch (num)
				{
				case 0:
					goto IL_B0;
				case 1:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_97;
							case 1:
								goto IL_9F;
							case 3:
								sprᢴ.ᜀ = sprᢴ.ᜁ();
								num = 0;
								continue;
							}
							if (sprᢴ.ᜀ == null)
							{
								num = 3;
								continue;
							}
							IL_97:
							num = 1;
						}
						IL_9F:
						goto IL_D6;
					}
					finally
					{
						if (true)
						{
						}
						Monitor.Exit(obj);
					}
					goto IL_B0;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (sprᢴ.ᜀ == null)
				{
					num = 0;
					continue;
				}
				goto IL_D6;
				IL_B0:
				Monitor.Enter(obj = sprᢴ.ᜁ);
				num = 1;
			}
		}
		IL_D6:
		return sprᢴ.ᜀ;
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x000E3838 File Offset: 0x000E2838
	// Note: this type is marked as 'beforefieldinit'.
	static sprᢴ()
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
		sprᢴ.ᜁ = new object();
		sprᢴ.ᜂ = new Hashtable();
		sprᢴ.ᜃ = new object();
	}

	// Token: 0x0400171A RID: 5914
	private static volatile Hashtable ᜀ;

	// Token: 0x0400171B RID: 5915
	private static readonly object ᜁ;

	// Token: 0x0400171C RID: 5916
	private static readonly Hashtable ᜂ;

	// Token: 0x0400171D RID: 5917
	private static readonly object ᜃ;
}
