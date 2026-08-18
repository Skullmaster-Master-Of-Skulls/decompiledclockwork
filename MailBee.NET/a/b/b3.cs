using System;
using System.Text;

namespace a.b
{
	// Token: 0x020003A1 RID: 929
	internal class b3
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x00089B5C File Offset: 0x00088B5C
		public static int a(int A_0)
		{
			if (A_0 <= 186)
			{
				if (A_0 <= 89)
				{
					switch (A_0)
					{
					case 0:
						return 1252;
					case 1:
						return 0;
					case 2:
						return 42;
					default:
						switch (A_0)
						{
						case 77:
							return 10000;
						case 78:
							return 10001;
						case 79:
							return 10003;
						case 80:
							return 10008;
						case 81:
							return 10002;
						case 82:
							return 0;
						case 83:
							return 10005;
						case 84:
							return 10004;
						case 85:
							return 10006;
						case 86:
							return 10081;
						case 87:
							return 10021;
						case 88:
							return 10029;
						case 89:
							return 10007;
						}
						break;
					}
				}
				else
				{
					switch (A_0)
					{
					case 128:
						return 932;
					case 129:
						return 949;
					case 130:
						return 1361;
					case 131:
					case 132:
					case 133:
					case 135:
						break;
					case 134:
						return 936;
					case 136:
						return 950;
					default:
						switch (A_0)
						{
						case 161:
							return 1253;
						case 162:
							return 1254;
						case 163:
							return 1258;
						default:
							switch (A_0)
							{
							case 177:
								return 1255;
							case 178:
								return 1256;
							case 179:
								return 0;
							case 180:
								return 0;
							case 181:
								return 0;
							case 186:
								return 1257;
							}
							break;
						}
						break;
					}
				}
			}
			else if (A_0 <= 222)
			{
				if (A_0 == 204)
				{
					return 1251;
				}
				if (A_0 == 222)
				{
					return 874;
				}
			}
			else
			{
				if (A_0 == 238)
				{
					return 1250;
				}
				if (A_0 == 254)
				{
					return 437;
				}
				if (A_0 == 255)
				{
					return 850;
				}
			}
			return 0;
		}

		// Token: 0x040014F6 RID: 5366
		public const string a = "rtf";

		// Token: 0x040014F7 RID: 5367
		public const int b = 1;

		// Token: 0x040014F8 RID: 5368
		public const string c = "generator";

		// Token: 0x040014F9 RID: 5369
		public const string d = "viewkind";

		// Token: 0x040014FA RID: 5370
		public const string e = "ansi";

		// Token: 0x040014FB RID: 5371
		public const string f = "mac";

		// Token: 0x040014FC RID: 5372
		public const string g = "pc";

		// Token: 0x040014FD RID: 5373
		public const string h = "pca";

		// Token: 0x040014FE RID: 5374
		public const string i = "ansicpg";

		// Token: 0x040014FF RID: 5375
		public const int j = 1252;

		// Token: 0x04001500 RID: 5376
		public const int k = 42;

		// Token: 0x04001501 RID: 5377
		public static readonly Encoding l = Encoding.GetEncoding(1252);

		// Token: 0x04001502 RID: 5378
		public const string m = "uc";

		// Token: 0x04001503 RID: 5379
		public const string n = "u";

		// Token: 0x04001504 RID: 5380
		public const string o = "upr";

		// Token: 0x04001505 RID: 5381
		public const string p = "ud";

		// Token: 0x04001506 RID: 5382
		public const string q = "fonttbl";

		// Token: 0x04001507 RID: 5383
		public const string r = "deff";

		// Token: 0x04001508 RID: 5384
		public const string s = "f";

		// Token: 0x04001509 RID: 5385
		public const string t = "fnil";

		// Token: 0x0400150A RID: 5386
		public const string u = "froman";

		// Token: 0x0400150B RID: 5387
		public const string v = "fswiss";

		// Token: 0x0400150C RID: 5388
		public const string w = "fmodern";

		// Token: 0x0400150D RID: 5389
		public const string x = "fscript";

		// Token: 0x0400150E RID: 5390
		public const string y = "fdecor";

		// Token: 0x0400150F RID: 5391
		public const string z = "ftech";

		// Token: 0x04001510 RID: 5392
		public const string aa = "fbidi";

		// Token: 0x04001511 RID: 5393
		public const string ab = "fcharset";

		// Token: 0x04001512 RID: 5394
		public const string ac = "fprq";

		// Token: 0x04001513 RID: 5395
		public const string ad = "fs";

		// Token: 0x04001514 RID: 5396
		public const string ae = "dn";

		// Token: 0x04001515 RID: 5397
		public const string af = "up";

		// Token: 0x04001516 RID: 5398
		public const string ag = "sub";

		// Token: 0x04001517 RID: 5399
		public const string ah = "super";

		// Token: 0x04001518 RID: 5400
		public const string ai = "nosupersub";

		// Token: 0x04001519 RID: 5401
		public const string aj = "flomajor";

		// Token: 0x0400151A RID: 5402
		public const string ak = "fhimajor";

		// Token: 0x0400151B RID: 5403
		public const string al = "fdbmajor";

		// Token: 0x0400151C RID: 5404
		public const string am = "fbimajor";

		// Token: 0x0400151D RID: 5405
		public const string an = "flominor";

		// Token: 0x0400151E RID: 5406
		public const string ao = "fhiminor";

		// Token: 0x0400151F RID: 5407
		public const string ap = "fdbminor";

		// Token: 0x04001520 RID: 5408
		public const string aq = "fbiminor";

		// Token: 0x04001521 RID: 5409
		public const int ar = 24;

		// Token: 0x04001522 RID: 5410
		public const string @as = "cpg";

		// Token: 0x04001523 RID: 5411
		public const string at = "colortbl";

		// Token: 0x04001524 RID: 5412
		public const string au = "red";

		// Token: 0x04001525 RID: 5413
		public const string av = "green";

		// Token: 0x04001526 RID: 5414
		public const string aw = "blue";

		// Token: 0x04001527 RID: 5415
		public const string ax = "cf";

		// Token: 0x04001528 RID: 5416
		public const string ay = "cb";

		// Token: 0x04001529 RID: 5417
		public const string az = "chcbpat";

		// Token: 0x0400152A RID: 5418
		public const string a0 = "highlight";

		// Token: 0x0400152B RID: 5419
		public const string a1 = "header";

		// Token: 0x0400152C RID: 5420
		public const string a2 = "headerf";

		// Token: 0x0400152D RID: 5421
		public const string a3 = "headerl";

		// Token: 0x0400152E RID: 5422
		public const string a4 = "headerr";

		// Token: 0x0400152F RID: 5423
		public const string a5 = "footer";

		// Token: 0x04001530 RID: 5424
		public const string a6 = "footerf";

		// Token: 0x04001531 RID: 5425
		public const string a7 = "footerl";

		// Token: 0x04001532 RID: 5426
		public const string a8 = "footerr";

		// Token: 0x04001533 RID: 5427
		public const string a9 = "footnote";

		// Token: 0x04001534 RID: 5428
		public const string ba = ";";

		// Token: 0x04001535 RID: 5429
		public const string bb = "*";

		// Token: 0x04001536 RID: 5430
		public const string bc = "~";

		// Token: 0x04001537 RID: 5431
		public const string bd = "-";

		// Token: 0x04001538 RID: 5432
		public const string be = "_";

		// Token: 0x04001539 RID: 5433
		public const string bf = "page";

		// Token: 0x0400153A RID: 5434
		public const string bg = "sect";

		// Token: 0x0400153B RID: 5435
		public const string bh = "par";

		// Token: 0x0400153C RID: 5436
		public const string bi = "line";

		// Token: 0x0400153D RID: 5437
		public const string bj = "tab";

		// Token: 0x0400153E RID: 5438
		public const string bk = "emdash";

		// Token: 0x0400153F RID: 5439
		public const string bl = "endash";

		// Token: 0x04001540 RID: 5440
		public const string bm = "emspace";

		// Token: 0x04001541 RID: 5441
		public const string bn = "enspace";

		// Token: 0x04001542 RID: 5442
		public const string bo = "qmspace";

		// Token: 0x04001543 RID: 5443
		public const string bp = "bullet";

		// Token: 0x04001544 RID: 5444
		public const string bq = "lquote";

		// Token: 0x04001545 RID: 5445
		public const string br = "rquote";

		// Token: 0x04001546 RID: 5446
		public const string bs = "ldblquote";

		// Token: 0x04001547 RID: 5447
		public const string bt = "rdblquote";

		// Token: 0x04001548 RID: 5448
		public const string bu = "plain";

		// Token: 0x04001549 RID: 5449
		public const string bv = "pard";

		// Token: 0x0400154A RID: 5450
		public const string bw = "sectd";

		// Token: 0x0400154B RID: 5451
		public const string bx = "b";

		// Token: 0x0400154C RID: 5452
		public const string by = "i";

		// Token: 0x0400154D RID: 5453
		public const string bz = "ul";

		// Token: 0x0400154E RID: 5454
		public const string b0 = "ulnone";

		// Token: 0x0400154F RID: 5455
		public const string b1 = "strike";

		// Token: 0x04001550 RID: 5456
		public const string b2 = "v";

		// Token: 0x04001551 RID: 5457
		public const string b3 = "ql";

		// Token: 0x04001552 RID: 5458
		public const string b4 = "qc";

		// Token: 0x04001553 RID: 5459
		public const string b5 = "qr";

		// Token: 0x04001554 RID: 5460
		public const string b6 = "qj";

		// Token: 0x04001555 RID: 5461
		public const string b7 = "stylesheet";

		// Token: 0x04001556 RID: 5462
		public const string b8 = "info";

		// Token: 0x04001557 RID: 5463
		public const string b9 = "version";

		// Token: 0x04001558 RID: 5464
		public const string ca = "vern";

		// Token: 0x04001559 RID: 5465
		public const string cb = "nofpages";

		// Token: 0x0400155A RID: 5466
		public const string cc = "nofwords";

		// Token: 0x0400155B RID: 5467
		public const string cd = "nofchars";

		// Token: 0x0400155C RID: 5468
		public const string ce = "id";

		// Token: 0x0400155D RID: 5469
		public const string cf = "title";

		// Token: 0x0400155E RID: 5470
		public const string cg = "subject";

		// Token: 0x0400155F RID: 5471
		public const string ch = "author";

		// Token: 0x04001560 RID: 5472
		public const string ci = "manager";

		// Token: 0x04001561 RID: 5473
		public const string cj = "company";

		// Token: 0x04001562 RID: 5474
		public const string ck = "operator";

		// Token: 0x04001563 RID: 5475
		public const string cl = "category";

		// Token: 0x04001564 RID: 5476
		public const string cm = "keywords";

		// Token: 0x04001565 RID: 5477
		public const string cn = "comment";

		// Token: 0x04001566 RID: 5478
		public const string co = "doccomm";

		// Token: 0x04001567 RID: 5479
		public const string cp = "hlinkbase";

		// Token: 0x04001568 RID: 5480
		public const string cq = "creatim";

		// Token: 0x04001569 RID: 5481
		public const string cr = "revtim";

		// Token: 0x0400156A RID: 5482
		public const string cs = "printim";

		// Token: 0x0400156B RID: 5483
		public const string ct = "buptim";

		// Token: 0x0400156C RID: 5484
		public const string cu = "yr";

		// Token: 0x0400156D RID: 5485
		public const string cv = "mo";

		// Token: 0x0400156E RID: 5486
		public const string cw = "dy";

		// Token: 0x0400156F RID: 5487
		public const string cx = "hr";

		// Token: 0x04001570 RID: 5488
		public const string cy = "min";

		// Token: 0x04001571 RID: 5489
		public const string cz = "sec";

		// Token: 0x04001572 RID: 5490
		public const string c0 = "edmins";

		// Token: 0x04001573 RID: 5491
		public const string c1 = "userprops";

		// Token: 0x04001574 RID: 5492
		public const string c2 = "proptype";

		// Token: 0x04001575 RID: 5493
		public const string c3 = "propname";

		// Token: 0x04001576 RID: 5494
		public const string c4 = "staticval";

		// Token: 0x04001577 RID: 5495
		public const string c5 = "linkval";

		// Token: 0x04001578 RID: 5496
		public const int c6 = 3;

		// Token: 0x04001579 RID: 5497
		public const int c7 = 5;

		// Token: 0x0400157A RID: 5498
		public const int c8 = 64;

		// Token: 0x0400157B RID: 5499
		public const int c9 = 11;

		// Token: 0x0400157C RID: 5500
		public const int da = 30;

		// Token: 0x0400157D RID: 5501
		public const string db = "pict";

		// Token: 0x0400157E RID: 5502
		public const string dc = "shppict";

		// Token: 0x0400157F RID: 5503
		public const string dd = "nonshppict";

		// Token: 0x04001580 RID: 5504
		public const string de = "emfblip";

		// Token: 0x04001581 RID: 5505
		public const string df = "pngblip";

		// Token: 0x04001582 RID: 5506
		public const string dg = "jpegblip";

		// Token: 0x04001583 RID: 5507
		public const string dh = "macpict";

		// Token: 0x04001584 RID: 5508
		public const string di = "pmmetafile";

		// Token: 0x04001585 RID: 5509
		public const string dj = "wmetafile";

		// Token: 0x04001586 RID: 5510
		public const string dk = "dibitmap";

		// Token: 0x04001587 RID: 5511
		public const string dl = "wbitmap";

		// Token: 0x04001588 RID: 5512
		public const string dm = "picw";

		// Token: 0x04001589 RID: 5513
		public const string dn = "pich";

		// Token: 0x0400158A RID: 5514
		public const string @do = "picwgoal";

		// Token: 0x0400158B RID: 5515
		public const string dp = "pichgoal";

		// Token: 0x0400158C RID: 5516
		public const string dq = "picscalex";

		// Token: 0x0400158D RID: 5517
		public const string dr = "picscaley";

		// Token: 0x0400158E RID: 5518
		public const string ds = "pntext";

		// Token: 0x0400158F RID: 5519
		public const string dt = "listtext";

		// Token: 0x04001590 RID: 5520
		public const string du = "objattph";
	}
}
