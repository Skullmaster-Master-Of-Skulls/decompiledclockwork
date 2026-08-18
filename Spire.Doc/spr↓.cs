using System;
using System.Collections;
using System.IO;
using System.Threading;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020001C4 RID: 452
internal class spr\u2193
{
	// Token: 0x06001327 RID: 4903 RVA: 0x0013A0B8 File Offset: 0x001390B8
	internal static spr\u213A ᜀ(PageBorderArt A_0)
	{
		int a_ = 7;
		if (!spr\u2193.ᜀ().Contains((int)A_0))
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
				throw new InvalidOperationException(ClipboardData.b("㡬Ůᩰᵲᩴv᝸孺ὼၾꦈﾌﮎ뾐", a_));
			}
		}
		if (true)
		{
		}
		return (spr\u213A)spr\u2193.ᜀ()[(int)A_0];
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x0013A13C File Offset: 0x0013913C
	private static Hashtable ᜁ()
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			Hashtable hashtable = new Hashtable();
			Stream stream = spr\u1DA1.ᜂ(ClipboardData.b("㑦ᥨɪὬ੮彰㝲ᩴᑶ坸⥺᡼౾ꎌ\udf8e햖列爵펠힤펦螨좬즮\ud8b0\uddb2\udcb4쎶킸풺펼첾믂꣄ꯆ", a_));
			try
			{
				for (;;)
				{
					spr\u20C4 spr_u20C = new spr\u20C4(stream);
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_2AE;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2AE;
							default:
							{
								if (false)
								{
								}
								if (!spr_u20C.ᜃ(ClipboardData.b("╦٨ᥪ६੮Ͱ㉲ݴͶ㵸Ṻ᭼ᙾﺌ", a_)))
								{
									num = 0;
									continue;
								}
								spr\u213A spr_u213A = new spr\u213A(Convert.ToInt32(spr_u20C.ᜀ(ClipboardData.b("๦൨", a_), ClipboardData.b("䩦塨", a_))), Convert.ToInt32(spr_u20C.ᜀ(ClipboardData.b("Ѧ٨ժᥬᵮၰၲŴṶᙸᕺ", a_), ClipboardData.b("坦", a_))), Convert.ToInt32(spr_u20C.ᜀ(ClipboardData.b("ས౨፪ᵬ๮ὰrᱴᡶ᝸", a_), ClipboardData.b("坦", a_))), Convert.ToInt32(spr_u20C.ᜀ(ClipboardData.b("ᅦ౨፪ᵬ๮ὰrᱴᡶ᝸", a_), ClipboardData.b("坦", a_))));
								spr_u213A.ᜀ(BorderType.Top, PageBorderArtElementPosition.First, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("፦ը", a_), "")));
								spr_u213A.ᜀ(BorderType.Top, PageBorderArtElementPosition.Middle, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("፦", a_), "")));
								spr_u213A.ᜀ(BorderType.Top, PageBorderArtElementPosition.Last, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("፦᭨", a_), "")));
								spr_u213A.ᜀ(BorderType.Left, PageBorderArtElementPosition.Middle, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("୦", a_), "")));
								spr_u213A.ᜀ(BorderType.Right, PageBorderArtElementPosition.Middle, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("ᕦ", a_), "")));
								spr_u213A.ᜀ(BorderType.Bottom, PageBorderArtElementPosition.First, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("զը", a_), "")));
								spr_u213A.ᜀ(BorderType.Bottom, PageBorderArtElementPosition.Middle, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("զ", a_), "")));
								spr_u213A.ᜀ(BorderType.Bottom, PageBorderArtElementPosition.Last, spr\u2193.ᜀ(spr_u20C.ᜀ(ClipboardData.b("զ᭨", a_), "")));
								hashtable.Add(spr_u213A.ᜁ(), spr_u213A);
								num = 2;
								continue;
							}
							}
							break;
						case 2:
							goto IL_25F;
						case 3:
							goto IL_2BA;
						case 4:
							goto IL_25F;
						}
						break;
						IL_25F:
						num = 1;
						continue;
						IL_2AE:
						num = 3;
					}
				}
				IL_2BA:;
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_2F7;
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
				IL_2F7:;
			}
			return hashtable;
		}
		}
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x0013A46C File Offset: 0x0013946C
	private static Hashtable ᜀ()
	{
		int num = 1;
		for (;;)
		{
			object obj;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				try
				{
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7B;
						case 1:
							spr\u2193.ᜁ = spr\u2193.ᜁ();
							num = 0;
							continue;
						case 2:
							goto IL_83;
						}
						if (spr\u2193.ᜁ == null)
						{
							num = 1;
							continue;
						}
						IL_7B:
						num = 2;
					}
					IL_83:
					goto IL_D6;
				}
				finally
				{
					Monitor.Exit(obj);
				}
				goto IL_8C;
			case 2:
				goto IL_8C;
			}
			if (spr\u2193.ᜁ == null)
			{
				num = 2;
				continue;
			}
			break;
			IL_8C:
			Monitor.Enter(obj = spr\u2193.ᜀ);
			num = 0;
		}
		IL_D6:
		return spr\u2193.ᜁ;
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0013A568 File Offset: 0x00139568
	internal static byte[] ᜀ(string A_0)
	{
		int a_ = 3;
		byte[] result;
		try
		{
			Stream stream = spr\u1DA1.ᜂ(string.Format(ClipboardData.b("㩨᭪Ѭᵮᑰ嵲ㅴᡶ᩸啺⽼᩾ﺌꆎ손\udb98ﮞ쒠톢햦\udda8薪횬龮첰", a_), A_0));
			try
			{
				spr\u2481 a_2 = spr\u2481.ᜀ(1, 1, 96.0, 96.0);
				result = spr\u2075.ᜀ(spr\u1CC6.ᜀ(stream), a_2);
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_9B;
						}
						break;
					}
					if (stream == null)
					{
						goto IL_A3;
					}
					num = 0;
				}
				IL_9B:
				if (false)
				{
				}
				IL_A3:;
			}
		}
		catch
		{
			result = new byte[0];
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x0013A668 File Offset: 0x00139668
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2193()
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
		spr\u2193.ᜀ = new object();
	}

	// Token: 0x040018B2 RID: 6322
	private static readonly object ᜀ;

	// Token: 0x040018B3 RID: 6323
	private static volatile Hashtable ᜁ;
}
