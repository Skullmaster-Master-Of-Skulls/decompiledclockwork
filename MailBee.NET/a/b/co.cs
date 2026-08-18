using System;
using System.Text;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200025F RID: 607
	internal class co : ii
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x00060294 File Offset: 0x0005F294
		public virtual string d7()
		{
			if (this.x.a(4105))
			{
				e2 e = this.x.b(4105);
				if (e.h.Length != 0)
				{
					return fw.a(e.h);
				}
				int a_ = e.g;
				h1 h = this.y.b(a_);
				if (h != null)
				{
					return fw.a(h.b());
				}
			}
			return string.Empty;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00060301 File Offset: 0x0005F301
		public virtual int e4()
		{
			return this.b(23, 1);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0006030C File Offset: 0x0005F30C
		public override string gr()
		{
			return this.d(26);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00060318 File Offset: 0x0005F318
		public virtual string dz()
		{
			string text = this.d(55);
			if (text != null && text.Length >= 2 && text[0] == '\u0001')
			{
				if (text.Length == 2)
				{
					text = "";
				}
				else
				{
					text = text.Substring(2, text.Length - 2);
				}
			}
			return text;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00060366 File Offset: 0x0005F366
		public virtual DateTime eu()
		{
			return this.f(57);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00060370 File Offset: 0x0005F370
		public virtual string dg()
		{
			return this.d(64);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0006037A File Offset: 0x0005F37A
		public virtual string em()
		{
			return this.d(66);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00060384 File Offset: 0x0005F384
		public virtual string db()
		{
			return this.d(100);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0006038E File Offset: 0x0005F38E
		public virtual string dx()
		{
			return this.d(101);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00060398 File Offset: 0x0005F398
		public virtual string dj()
		{
			return this.d(112);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000603A2 File Offset: 0x0005F3A2
		public virtual string eh()
		{
			return this.d(117);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000603AC File Offset: 0x0005F3AC
		public virtual string c0()
		{
			return this.d(118);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000603B6 File Offset: 0x0005F3B6
		public virtual string de()
		{
			return this.d(125);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000603C0 File Offset: 0x0005F3C0
		public virtual bool cr()
		{
			return (this.h(3591) & 1) != 0;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000603D2 File Offset: 0x0005F3D2
		public virtual bool dc()
		{
			return (this.h(3591) & 2) != 0;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000603E4 File Offset: 0x0005F3E4
		public virtual bool e0()
		{
			return (this.h(3591) & 4) != 0;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000603F6 File Offset: 0x0005F3F6
		public virtual bool ek()
		{
			return (this.h(3591) & 8) != 0;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00060408 File Offset: 0x0005F408
		public virtual bool en()
		{
			return (this.h(3591) & 32) != 0;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0006041B File Offset: 0x0005F41B
		public virtual bool c8()
		{
			return (this.h(3591) & 64) != 0;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0006042E File Offset: 0x0005F42E
		public virtual bool ec()
		{
			return (this.h(3591) & 128) != 0;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00060444 File Offset: 0x0005F444
		public virtual int eg()
		{
			return this.h(1);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0006044D File Offset: 0x0005F44D
		public virtual bool dw()
		{
			return this.h(35) != 0;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0006045A File Offset: 0x0005F45A
		public virtual int @do()
		{
			return this.h(38);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00060464 File Offset: 0x0005F464
		public virtual bool et()
		{
			return this.h(41) != 0;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00060471 File Offset: 0x0005F471
		public virtual bool cy()
		{
			return this.h(43) != 0;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0006047E File Offset: 0x0005F47E
		public virtual int dy()
		{
			return this.h(46);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00060488 File Offset: 0x0005F488
		public virtual int dv()
		{
			return this.h(54);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00060492 File Offset: 0x0005F492
		public virtual byte[] di()
		{
			return this.g(59);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0006049C File Offset: 0x0005F49C
		public virtual string ep()
		{
			return this.d(68);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x000604A6 File Offset: 0x0005F4A6
		public virtual string cp()
		{
			return this.d(73);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x000604B0 File Offset: 0x0005F4B0
		public virtual string ed()
		{
			return this.d(80);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000604BA File Offset: 0x0005F4BA
		public virtual bool eo()
		{
			return this.h(87) != 0;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x000604C7 File Offset: 0x0005F4C7
		public virtual bool ej()
		{
			return this.h(88) != 0;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x000604D4 File Offset: 0x0005F4D4
		public virtual string dd()
		{
			return this.d(89);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000604DE File Offset: 0x0005F4DE
		public virtual bool e5()
		{
			return this.e(99);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000604E8 File Offset: 0x0005F4E8
		public virtual string d2()
		{
			return this.d(100);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000604F2 File Offset: 0x0005F4F2
		public virtual string d0()
		{
			return this.d(114);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x000604FC File Offset: 0x0005F4FC
		public virtual string ey()
		{
			return this.d(115);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00060506 File Offset: 0x0005F506
		public virtual string du()
		{
			return this.d(116);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00060510 File Offset: 0x0005F510
		public virtual string c1()
		{
			return this.d(119);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006051A File Offset: 0x0005F51A
		public virtual string c9()
		{
			return this.d(120);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00060524 File Offset: 0x0005F524
		public virtual bool eq()
		{
			return this.h(3078) != 0;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00060534 File Offset: 0x0005F534
		public virtual bool dn()
		{
			return this.h(3080) != 0;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00060544 File Offset: 0x0005F544
		public virtual int d9()
		{
			return this.h(3093);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00060551 File Offset: 0x0005F551
		public virtual bool e1()
		{
			return this.h(3095) != 0;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00060561 File Offset: 0x0005F561
		public virtual byte[] dl()
		{
			return this.g(3097);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006056E File Offset: 0x0005F56E
		public virtual string cn()
		{
			return this.d(3098);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0006057B File Offset: 0x0005F57B
		public virtual string dq()
		{
			return this.d(3102);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00060588 File Offset: 0x0005F588
		public virtual string d3()
		{
			return this.d(3103);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00060595 File Offset: 0x0005F595
		public virtual long cu()
		{
			return this.i(3592);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x000605A2 File Offset: 0x0005F5A2
		public virtual int ee()
		{
			return this.h(3619);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x000605AF File Offset: 0x0005F5AF
		public virtual string ds()
		{
			return this.d(3624);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000605BC File Offset: 0x0005F5BC
		public virtual string cs()
		{
			return this.d(3625);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000605C9 File Offset: 0x0005F5C9
		public virtual int cm()
		{
			return this.h(3681);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000605D6 File Offset: 0x0005F5D6
		public virtual int dt()
		{
			return this.h(4094);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x000605E3 File Offset: 0x0005F5E3
		public virtual bool d1()
		{
			return this.h(3585) != 0;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000605F3 File Offset: 0x0005F5F3
		public virtual bool c4()
		{
			return this.h(3599) != 0;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00060603 File Offset: 0x0005F603
		public virtual bool eb()
		{
			return this.h(3615) != 0;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00060613 File Offset: 0x0005F613
		public virtual bool dr()
		{
			return this.h(3682) != 0;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00060623 File Offset: 0x0005F623
		public virtual string e2()
		{
			return this.d(3586);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00060630 File Offset: 0x0005F630
		public virtual string el()
		{
			return this.d(3587);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0006063D File Offset: 0x0005F63D
		public virtual string cz()
		{
			return this.d(3588);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0006064A File Offset: 0x0005F64A
		public virtual DateTime c6()
		{
			return this.f(3590);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00060657 File Offset: 0x0005F657
		public string d4()
		{
			return this.a;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00060660 File Offset: 0x0005F660
		public virtual string d6()
		{
			string text = null;
			e2 e = this.x.b(16381);
			if (e == null)
			{
				e = this.x.b(16350);
			}
			if (e != null)
			{
				text = bs.a(e.g);
				if (this.a == null && text != null)
				{
					this.a = text.ToLower();
				}
			}
			return this.a(4096, 0, text);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000606C7 File Offset: 0x0005F6C7
		public virtual string er()
		{
			return this.d(26137);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000606D4 File Offset: 0x0005F6D4
		public virtual int d8()
		{
			return this.h(4102);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000606E1 File Offset: 0x0005F6E1
		public virtual int c7()
		{
			return this.h(4103);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000606EE File Offset: 0x0005F6EE
		public virtual string es()
		{
			return this.d(4104);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000606FB File Offset: 0x0005F6FB
		public virtual int e3()
		{
			return this.h(4112);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00060708 File Offset: 0x0005F708
		public virtual int cq()
		{
			return this.h(4113);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00060718 File Offset: 0x0005F718
		public virtual string c3()
		{
			string text = null;
			e2 e = this.x.b(16350);
			if (e == null)
			{
				e = this.x.b(16381);
			}
			if (e != null)
			{
				text = bs.a(e.g);
				if (this.a == null && text != null)
				{
					this.a = text.ToLower();
				}
			}
			return this.a(4115, 0, text);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0006077F File Offset: 0x0005F77F
		public virtual string dk()
		{
			return this.d(4149);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0006078C File Offset: 0x0005F78C
		public virtual string e6()
		{
			return this.d(4162);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00060799 File Offset: 0x0005F799
		public virtual string ef()
		{
			return this.d(4166);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000607A6 File Offset: 0x0005F7A6
		public virtual int cw()
		{
			return this.h(4224);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000607B3 File Offset: 0x0005F7B3
		public virtual int e7()
		{
			return this.h(4225);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000607C0 File Offset: 0x0005F7C0
		public virtual DateTime ct()
		{
			return this.f(4226);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000607CD File Offset: 0x0005F7CD
		public virtual bool ew()
		{
			return this.h(4338) != 0;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000607DD File Offset: 0x0005F7DD
		public virtual string d5()
		{
			return this.d(4339);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000607EA File Offset: 0x0005F7EA
		public virtual bool df()
		{
			return this.h(4340) != 0;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x000607FA File Offset: 0x0005F7FA
		public virtual bool cx()
		{
			return this.h(4341) != 0;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0006080A File Offset: 0x0005F80A
		public virtual bool dp()
		{
			return this.h(4342) != 0;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006081A File Offset: 0x0005F81A
		public virtual int cv()
		{
			this.b();
			if (this.g == null)
			{
				return 0;
			}
			return this.g.a4();
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00060837 File Offset: 0x0005F837
		public virtual DateTime co()
		{
			return this.f(this.u.b(33028, 10));
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00060851 File Offset: 0x0005F851
		public virtual DateTime da()
		{
			return this.f(this.u.b(33029, 10));
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0006086B File Offset: 0x0005F86B
		public virtual bool ez()
		{
			return this.e(this.u.b(34051, 1));
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00060884 File Offset: 0x0005F884
		public virtual int ei()
		{
			return this.h(this.u.b(34049, 1));
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0006089D File Offset: 0x0005F89D
		public virtual bool dh()
		{
			return this.da() != DateTime.MinValue;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000608B0 File Offset: 0x0005F8B0
		public virtual string[] g9()
		{
			string[] array = new string[0];
			if (this.x.a(32790))
			{
				try
				{
					e2 e = this.x.b(32790);
					if (e.h.Length == 0)
					{
						return array;
					}
					int num = (int)e.h[0];
					if (num > 0)
					{
						array = new string[num];
						int[] array2 = new int[num];
						for (int i = 0; i < num; i++)
						{
							array2[i] = (int)ii.a(e.h, i * 4 + 1, (i + 1) * 4 + 1);
						}
						for (int j = 0; j < array2.Length - 1; j++)
						{
							int num2 = array2[j];
							int num3 = array2[j + 1] - num2;
							byte[] array3 = new byte[num3];
							Array.Copy(e.h, num2, array3, 0, num3);
							string @string = Encoding.GetEncoding("UTF-16LE").GetString(array3, 0, array3.Length);
							array[j] = @string;
						}
						int num4 = array2[array2.Length - 1];
						int num5 = e.h.Length - num4;
						byte[] array4 = new byte[num5];
						Array.Copy(e.h, num4, array4, 0, num5);
						string string2 = Encoding.GetEncoding("UTF-16LE").GetString(array4, 0, array4.Length);
						array[array.Length - 1] = string2;
					}
				}
				catch (Exception)
				{
				}
				return array;
			}
			return array;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00060A24 File Offset: 0x0005FA24
		public virtual int c5()
		{
			try
			{
				this.a();
			}
			catch (Exception)
			{
				return 0;
			}
			if (this.h == null)
			{
				return 0;
			}
			return this.h.a4();
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00060A68 File Offset: 0x0005FA68
		public virtual string dm()
		{
			if (this.g != null)
			{
				return this.g.b();
			}
			return "No recipients table!";
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00060A83 File Offset: 0x0005FA83
		internal co(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00060A8D File Offset: 0x0005FA8D
		internal co(bs A_0, dx A_1, c0 A_2, fb A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00060A9A File Offset: 0x0005FA9A
		public virtual bool c2()
		{
			return (this.h(3591) & 16) != 0;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00060AAD File Offset: 0x0005FAAD
		public virtual bool ex()
		{
			return (this.h(4225) & 8) > 0;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00060ABF File Offset: 0x0005FABF
		public virtual bool ea()
		{
			return (this.h(4225) & 4) > 0;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00060AD4 File Offset: 0x0005FAD4
		private new void b()
		{
			try
			{
				int a_ = 1682;
				if (this.g == null && this.y != null && this.y.a(a_))
				{
					h1 h = this.y.b(a_);
					fb a_2 = null;
					if (h.c > 0)
					{
						a_2 = this.u.d((long)h.c);
					}
					this.g = new ad(new di(this.u, h), a_2);
				}
			}
			catch (Exception)
			{
				this.g = null;
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00060B64 File Offset: 0x0005FB64
		private new void a()
		{
			int a_ = 1649;
			if (this.h == null && this.y != null && this.y.a(a_))
			{
				h1 h = this.y.b(a_);
				fb a_2 = null;
				if (h.c > 0)
				{
					a_2 = this.u.d((long)h.c);
				}
				this.h = new ad(new di(this.u, h), a_2);
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00060BD8 File Offset: 0x0005FBD8
		public virtual string ev()
		{
			return this.d(3588);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00060BE8 File Offset: 0x0005FBE8
		public new virtual fl c(int A_0)
		{
			this.a();
			int num = 0;
			if (this.h != null)
			{
				num = this.h.a4();
			}
			if (A_0 >= num)
			{
				throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToFetchAttachmentNumber0Only1InThisEmail, A_0, num), 1210);
			}
			int a_ = this.h.a().a(A_0).b(26610).g;
			h1 h = this.y.b(a_);
			byte[] array = h.b();
			if (array != null && array.Length != 0)
			{
				c0 a_2 = new c0(new di(this.u, h));
				fb a_3 = new fb();
				if (h.c > 0)
				{
					a_3 = this.u.d((long)h.c);
				}
				return new fl(this.u, a_2, a_3);
			}
			throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToFetchAttachmentNumber0UnableToReadAttachmentDetailsTable, A_0), 1210);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00060CE0 File Offset: 0x0005FCE0
		public new virtual hf b(int A_0)
		{
			if (A_0 >= this.cv())
			{
				throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToFetchRecipientNumber0, A_0), 1210);
			}
			ew ew = this.g.a().a(A_0);
			if (ew != null)
			{
				return new hf(ew);
			}
			return null;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00060D34 File Offset: 0x0005FD34
		public override string ToString()
		{
			return string.Format("PSTEmail: {0}\nImportance: {1}\nMessage Class: {2}\n\n{3}\n\n\n{4}{5}", new object[]
			{
				this.dz(),
				this.e4(),
				this.gr(),
				this.de(),
				this.x,
				this.y
			});
		}

		// Token: 0x04001049 RID: 4169
		private new string a;

		// Token: 0x0400104A RID: 4170
		public new const int b = 0;

		// Token: 0x0400104B RID: 4171
		public new const int c = 1;

		// Token: 0x0400104C RID: 4172
		public new const int d = 2;

		// Token: 0x0400104D RID: 4173
		public new const int e = 1;

		// Token: 0x0400104E RID: 4174
		public new const int f = 2;

		// Token: 0x0400104F RID: 4175
		private new ad g;

		// Token: 0x04001050 RID: 4176
		private new ad h;
	}
}
