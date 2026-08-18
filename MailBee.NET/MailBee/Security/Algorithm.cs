using System;
using System.Collections;
using System.Security;
using a.j;

namespace MailBee.Security
{
	// Token: 0x020000F8 RID: 248
	public class Algorithm
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x00025C7C File Offset: 0x00024C7C
		static Algorithm()
		{
			Algorithm.il.Add("1.2.840.113549.1.1.1", 41984U);
			Algorithm.il.Add("1.2.840.113549.1.1.2", 32769U);
			Algorithm.il.Add("1.2.840.113549.1.1.3", 32770U);
			Algorithm.il.Add("1.2.840.113549.1.1.4", 32771U);
			Algorithm.il.Add("1.2.840.113549.1.1.5", 32772U);
			Algorithm.il.Add("1.2.840.113549.2.2", 32769U);
			Algorithm.il.Add("1.2.840.113549.2.4", 32770U);
			Algorithm.il.Add("1.2.840.113549.2.5", 32771U);
			Algorithm.il.Add("1.2.840.113549.3.2", 26114U);
			Algorithm.il.Add("1.2.840.113549.3.4", 26625U);
			Algorithm.il.Add("1.2.840.113549.3.7", 26115U);
			Algorithm.il.Add("1.3.14.3.2.2", 32770U);
			Algorithm.il.Add("1.3.14.3.2.3", 32771U);
			Algorithm.il.Add("1.3.14.3.2.4", 32770U);
			Algorithm.il.Add("1.3.14.3.2.7", 26113U);
			Algorithm.il.Add("1.3.14.3.2.12", 8704U);
			Algorithm.il.Add("1.3.14.3.2.13", 32772U);
			Algorithm.il.Add("1.3.14.3.2.15", 32772U);
			Algorithm.il.Add("1.3.14.3.2.18", 32772U);
			Algorithm.il.Add("1.3.14.3.2.22", 41984U);
			Algorithm.il.Add("1.3.14.3.2.26", 32772U);
			Algorithm.il.Add("1.3.14.3.2.27", 32772U);
			Algorithm.il.Add("1.3.14.3.2.29", 32772U);
			Algorithm.il.Add("1.3.14.7.2.3.1", 32769U);
			Algorithm.il.Add("2.16.840.1.101.2.1.1.19", 32772U);
			Algorithm.il.Add("2.16.840.1.101.2.1.1.20", 8704U);
			Algorithm.il.Add("2.16.840.1.101.3.4.2.1", 32780U);
			Algorithm.il.Add("1.2.840.10040.4.1", 8704U);
			Algorithm.il.Add("1.2.840.10046.2.1", 43521U);
			Algorithm.il.Add("1.2.840.113549.1.9.16.3.5", 43522U);
			Algorithm.il.Add("2.16.840.1.101.3.4.1.2", 26126U);
			Algorithm.il.Add("2.16.840.1.101.3.4.1.22", 26127U);
			Algorithm.il.Add("2.16.840.1.101.3.4.1.42", 26128U);
			Algorithm.im.Add(32769U, "1.2.840.113549.2.2");
			Algorithm.im.Add(32770U, "1.2.840.113549.2.4");
			Algorithm.im.Add(32771U, "1.2.840.113549.2.5");
			Algorithm.im.Add(32772U, "1.3.14.3.2.26");
			Algorithm.im.Add(32780U, "2.16.840.1.101.3.4.2.1");
			Algorithm.im.Add(9216U, "1.2.840.113549.1.1.1");
			Algorithm.im.Add(8704U, "1.2.840.10040.4.1");
			Algorithm.im.Add(41984U, "1.2.840.113549.1.1.1");
			Algorithm.im.Add(26113U, "1.3.14.3.2.7");
			Algorithm.im.Add(26115U, "1.2.840.113549.3.7");
			Algorithm.im.Add(26114U, "1.2.840.113549.3.2");
			Algorithm.im.Add(26625U, "1.2.840.113549.3.4");
			Algorithm.im.Add(43521U, "1.2.840.10046.2.1");
			Algorithm.im.Add(43522U, "1.2.840.113549.1.9.16.3.5");
			Algorithm.im.Add(26126U, "2.16.840.1.101.3.4.1.2");
			Algorithm.im.Add(26127U, "2.16.840.1.101.3.4.1.22");
			Algorithm.im.Add(26128U, "2.16.840.1.101.3.4.1.42");
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00026180 File Offset: 0x00025180
		internal Algorithm(uint A_0, int A_1, AlgorithmCategory A_2, string A_3, string A_4)
		{
			this.ig = (int)A_0;
			this.ih = A_1;
			this.ii = A_2;
			this.ij = A_3;
			this.ik = A_4;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x000261D5 File Offset: 0x000251D5
		public int ID
		{
			get
			{
				return this.ig;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x000261DD File Offset: 0x000251DD
		public int BitLength
		{
			get
			{
				return this.ih;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000261E5 File Offset: 0x000251E5
		public AlgorithmCategory Category
		{
			get
			{
				return this.ii;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000261ED File Offset: 0x000251ED
		public string Name
		{
			get
			{
				return this.ij;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000261F5 File Offset: 0x000251F5
		public string Oid
		{
			get
			{
				return this.ik;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00026200 File Offset: 0x00025200
		public static Algorithm CreateInstanceByOid(string oid)
		{
			uint num = Algorithm.a(oid);
			if (num != 0U)
			{
				return new Algorithm(num, 0, Algorithm.c(num), Algorithm.a(num), oid);
			}
			return null;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00026230 File Offset: 0x00025230
		public static Algorithm CreateInstanceById(int algId)
		{
			string text = Algorithm.b((uint)algId);
			if (text != null && text != string.Empty)
			{
				return new Algorithm((uint)algId, 0, Algorithm.c((uint)algId), Algorithm.a((uint)algId), text);
			}
			return null;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002626C File Offset: 0x0002526C
		internal static AlgorithmCategory c(uint A_0)
		{
			uint num = A_0 & 57344U;
			if (num <= 24576U)
			{
				if (num == 8192U)
				{
					return AlgorithmCategory.Signature;
				}
				if (num == 24576U)
				{
					return AlgorithmCategory.DataEncryption;
				}
			}
			else
			{
				if (num == 32768U)
				{
					return AlgorithmCategory.Hash;
				}
				if (num == 40960U)
				{
					return AlgorithmCategory.KeyExchange;
				}
			}
			return AlgorithmCategory.Unknown;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000262B6 File Offset: 0x000252B6
		internal static uint a(string A_0)
		{
			if (Algorithm.il[A_0] == null)
			{
				return 0U;
			}
			return (uint)Algorithm.il[A_0];
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000262D7 File Offset: 0x000252D7
		[SecuritySafeCritical]
		internal static uint a(IntPtr A_0)
		{
			return global::a.j.ab.e.CertOIDToAlgId(A_0);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000262DF File Offset: 0x000252DF
		internal static string b(uint A_0)
		{
			if (Algorithm.im[A_0] == null)
			{
				return string.Empty;
			}
			return (string)Algorithm.im[A_0];
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00026310 File Offset: 0x00025310
		internal static string a(uint A_0)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add(32769U, "MD2");
			hashtable.Add(32770U, "MD4");
			hashtable.Add(32771U, "MD5");
			hashtable.Add(32772U, "SHA1");
			hashtable.Add(32780U, "SHA-256");
			hashtable.Add(32773U, "MAC");
			hashtable.Add(32776U, "SSL3SHAMD5");
			hashtable.Add(32777U, "HMAC");
			hashtable.Add(26113U, "DES");
			hashtable.Add(26115U, "3DES");
			hashtable.Add(26114U, "RC2");
			hashtable.Add(26625U, "RC4");
			hashtable.Add(26126U, "AES 128");
			hashtable.Add(26127U, "AES 192");
			hashtable.Add(26128U, "AES 256");
			if (hashtable[A_0] == null)
			{
				return string.Empty;
			}
			return (string)hashtable[A_0];
		}

		// Token: 0x04000566 RID: 1382
		private const uint a = 0U;

		// Token: 0x04000567 RID: 1383
		private const uint b = 8192U;

		// Token: 0x04000568 RID: 1384
		private const uint c = 16384U;

		// Token: 0x04000569 RID: 1385
		private const uint d = 24576U;

		// Token: 0x0400056A RID: 1386
		private const uint e = 32768U;

		// Token: 0x0400056B RID: 1387
		private const uint f = 40960U;

		// Token: 0x0400056C RID: 1388
		private const uint g = 0U;

		// Token: 0x0400056D RID: 1389
		private const uint h = 512U;

		// Token: 0x0400056E RID: 1390
		private const uint i = 1024U;

		// Token: 0x0400056F RID: 1391
		private const uint j = 1536U;

		// Token: 0x04000570 RID: 1392
		private const uint k = 2048U;

		// Token: 0x04000571 RID: 1393
		private const uint l = 2560U;

		// Token: 0x04000572 RID: 1394
		private const uint m = 3072U;

		// Token: 0x04000573 RID: 1395
		private const uint n = 0U;

		// Token: 0x04000574 RID: 1396
		private const uint o = 0U;

		// Token: 0x04000575 RID: 1397
		private const uint p = 1U;

		// Token: 0x04000576 RID: 1398
		private const uint q = 2U;

		// Token: 0x04000577 RID: 1399
		private const uint r = 3U;

		// Token: 0x04000578 RID: 1400
		private const uint s = 4U;

		// Token: 0x04000579 RID: 1401
		private const uint t = 0U;

		// Token: 0x0400057A RID: 1402
		private const uint u = 1U;

		// Token: 0x0400057B RID: 1403
		private const uint v = 2U;

		// Token: 0x0400057C RID: 1404
		private const uint w = 1U;

		// Token: 0x0400057D RID: 1405
		private const uint x = 3U;

		// Token: 0x0400057E RID: 1406
		private const uint y = 4U;

		// Token: 0x0400057F RID: 1407
		private const uint z = 5U;

		// Token: 0x04000580 RID: 1408
		private const uint aa = 6U;

		// Token: 0x04000581 RID: 1409
		private const uint ab = 7U;

		// Token: 0x04000582 RID: 1410
		private const uint ac = 8U;

		// Token: 0x04000583 RID: 1411
		private const uint ad = 9U;

		// Token: 0x04000584 RID: 1412
		private const uint ae = 12U;

		// Token: 0x04000585 RID: 1413
		private const uint af = 13U;

		// Token: 0x04000586 RID: 1414
		private const uint ag = 14U;

		// Token: 0x04000587 RID: 1415
		private const uint ah = 15U;

		// Token: 0x04000588 RID: 1416
		private const uint ai = 16U;

		// Token: 0x04000589 RID: 1417
		private const uint aj = 17U;

		// Token: 0x0400058A RID: 1418
		private const uint ak = 10U;

		// Token: 0x0400058B RID: 1419
		private const uint al = 11U;

		// Token: 0x0400058C RID: 1420
		private const uint am = 6U;

		// Token: 0x0400058D RID: 1421
		private const uint an = 7U;

		// Token: 0x0400058E RID: 1422
		private const uint ao = 8U;

		// Token: 0x0400058F RID: 1423
		private const uint ap = 9U;

		// Token: 0x04000590 RID: 1424
		private const uint aq = 10U;

		// Token: 0x04000591 RID: 1425
		private const uint ar = 2U;

		// Token: 0x04000592 RID: 1426
		private const uint @as = 1U;

		// Token: 0x04000593 RID: 1427
		private const uint at = 2U;

		// Token: 0x04000594 RID: 1428
		private const uint au = 1U;

		// Token: 0x04000595 RID: 1429
		private const uint av = 2U;

		// Token: 0x04000596 RID: 1430
		private const uint aw = 3U;

		// Token: 0x04000597 RID: 1431
		private const uint ax = 4U;

		// Token: 0x04000598 RID: 1432
		private const uint ay = 1U;

		// Token: 0x04000599 RID: 1433
		private const uint az = 2U;

		// Token: 0x0400059A RID: 1434
		private const uint a0 = 3U;

		// Token: 0x0400059B RID: 1435
		private const uint a1 = 4U;

		// Token: 0x0400059C RID: 1436
		private const uint a2 = 4U;

		// Token: 0x0400059D RID: 1437
		private const uint a3 = 5U;

		// Token: 0x0400059E RID: 1438
		private const uint a4 = 6U;

		// Token: 0x0400059F RID: 1439
		private const uint a5 = 7U;

		// Token: 0x040005A0 RID: 1440
		private const uint a6 = 8U;

		// Token: 0x040005A1 RID: 1441
		private const uint a7 = 9U;

		// Token: 0x040005A2 RID: 1442
		private const uint a8 = 10U;

		// Token: 0x040005A3 RID: 1443
		private const uint a9 = 11U;

		// Token: 0x040005A4 RID: 1444
		private const uint ba = 12U;

		// Token: 0x040005A5 RID: 1445
		private const uint bb = 1U;

		// Token: 0x040005A6 RID: 1446
		private const uint bc = 2U;

		// Token: 0x040005A7 RID: 1447
		private const uint bd = 3U;

		// Token: 0x040005A8 RID: 1448
		private const uint be = 4U;

		// Token: 0x040005A9 RID: 1449
		private const uint bf = 5U;

		// Token: 0x040005AA RID: 1450
		private const uint bg = 6U;

		// Token: 0x040005AB RID: 1451
		private const uint bh = 7U;

		// Token: 0x040005AC RID: 1452
		private const uint bi = 80U;

		// Token: 0x040005AD RID: 1453
		private const uint bj = 32769U;

		// Token: 0x040005AE RID: 1454
		private const uint bk = 32770U;

		// Token: 0x040005AF RID: 1455
		private const uint bl = 32771U;

		// Token: 0x040005B0 RID: 1456
		private const uint bm = 32772U;

		// Token: 0x040005B1 RID: 1457
		private const uint bn = 32772U;

		// Token: 0x040005B2 RID: 1458
		private const uint bo = 32780U;

		// Token: 0x040005B3 RID: 1459
		private const uint bp = 32773U;

		// Token: 0x040005B4 RID: 1460
		private const uint bq = 9216U;

		// Token: 0x040005B5 RID: 1461
		private const uint br = 8704U;

		// Token: 0x040005B6 RID: 1462
		private const uint bs = 41984U;

		// Token: 0x040005B7 RID: 1463
		private const uint bt = 26113U;

		// Token: 0x040005B8 RID: 1464
		private const uint bu = 26121U;

		// Token: 0x040005B9 RID: 1465
		private const uint bv = 26115U;

		// Token: 0x040005BA RID: 1466
		private const uint bw = 26114U;

		// Token: 0x040005BB RID: 1467
		private const uint bx = 26625U;

		// Token: 0x040005BC RID: 1468
		private const uint by = 26626U;

		// Token: 0x040005BD RID: 1469
		private const uint bz = 43521U;

		// Token: 0x040005BE RID: 1470
		private const uint b0 = 43522U;

		// Token: 0x040005BF RID: 1471
		private const uint b1 = 43523U;

		// Token: 0x040005C0 RID: 1472
		private const uint b2 = 43524U;

		// Token: 0x040005C1 RID: 1473
		private const uint b3 = 40963U;

		// Token: 0x040005C2 RID: 1474
		private const uint b4 = 26122U;

		// Token: 0x040005C3 RID: 1475
		private const uint b5 = 26123U;

		// Token: 0x040005C4 RID: 1476
		private const uint b6 = 26124U;

		// Token: 0x040005C5 RID: 1477
		private const uint b7 = 32776U;

		// Token: 0x040005C6 RID: 1478
		private const uint b8 = 19457U;

		// Token: 0x040005C7 RID: 1479
		private const uint b9 = 19458U;

		// Token: 0x040005C8 RID: 1480
		private const uint ca = 19459U;

		// Token: 0x040005C9 RID: 1481
		private const uint cb = 19463U;

		// Token: 0x040005CA RID: 1482
		private const uint cc = 19460U;

		// Token: 0x040005CB RID: 1483
		private const uint cd = 19461U;

		// Token: 0x040005CC RID: 1484
		private const uint ce = 19462U;

		// Token: 0x040005CD RID: 1485
		private const uint cf = 26125U;

		// Token: 0x040005CE RID: 1486
		private const uint cg = 32777U;

		// Token: 0x040005CF RID: 1487
		private const uint ch = 32778U;

		// Token: 0x040005D0 RID: 1488
		private const uint ci = 32779U;

		// Token: 0x040005D1 RID: 1489
		private const uint cj = 26126U;

		// Token: 0x040005D2 RID: 1490
		private const uint ck = 26127U;

		// Token: 0x040005D3 RID: 1491
		private const uint cl = 26128U;

		// Token: 0x040005D4 RID: 1492
		private const uint cm = 26129U;

		// Token: 0x040005D5 RID: 1493
		private const string cn = "1.2.840.113549";

		// Token: 0x040005D6 RID: 1494
		private const string co = "1.2.840.113549.1";

		// Token: 0x040005D7 RID: 1495
		private const string cp = "1.2.840.113549.2";

		// Token: 0x040005D8 RID: 1496
		private const string cq = "1.2.840.113549.3";

		// Token: 0x040005D9 RID: 1497
		private const string cr = "1.2.840.113549.1.1";

		// Token: 0x040005DA RID: 1498
		private const string cs = "1.2.840.113549.1.2";

		// Token: 0x040005DB RID: 1499
		private const string ct = "1.2.840.113549.1.3";

		// Token: 0x040005DC RID: 1500
		private const string cu = "1.2.840.113549.1.4";

		// Token: 0x040005DD RID: 1501
		private const string cv = "1.2.840.113549.1.5";

		// Token: 0x040005DE RID: 1502
		private const string cw = "1.2.840.113549.1.6";

		// Token: 0x040005DF RID: 1503
		private const string cx = "1.2.840.113549.1.7";

		// Token: 0x040005E0 RID: 1504
		private const string cy = "1.2.840.113549.1.8";

		// Token: 0x040005E1 RID: 1505
		private const string cz = "1.2.840.113549.1.9";

		// Token: 0x040005E2 RID: 1506
		private const string c0 = "1.2.840.113549.1.10";

		// Token: 0x040005E3 RID: 1507
		private const string c1 = "1.2.840.113549.1.1.1";

		// Token: 0x040005E4 RID: 1508
		private const string c2 = "1.2.840.113549.1.1.2";

		// Token: 0x040005E5 RID: 1509
		private const string c3 = "1.2.840.113549.1.1.3";

		// Token: 0x040005E6 RID: 1510
		private const string c4 = "1.2.840.113549.1.1.4";

		// Token: 0x040005E7 RID: 1511
		private const string c5 = "1.2.840.113549.1.1.5";

		// Token: 0x040005E8 RID: 1512
		private const string c6 = "1.2.840.113549.1.1.6";

		// Token: 0x040005E9 RID: 1513
		private const string c7 = "1.2.840.113549.1.7.1";

		// Token: 0x040005EA RID: 1514
		private const string c8 = "1.2.840.113549.1.7.2";

		// Token: 0x040005EB RID: 1515
		private const string c9 = "1.2.840.113549.1.7.3";

		// Token: 0x040005EC RID: 1516
		private const string da = "1.2.840.113549.1.7.4";

		// Token: 0x040005ED RID: 1517
		private const string db = "1.2.840.113549.1.7.5";

		// Token: 0x040005EE RID: 1518
		private const string dc = "1.2.840.113549.1.7.5";

		// Token: 0x040005EF RID: 1519
		private const string dd = "1.2.840.113549.1.7.6";

		// Token: 0x040005F0 RID: 1520
		private const string de = "1.2.840.113549.1.9.1";

		// Token: 0x040005F1 RID: 1521
		private const string df = "1.2.840.113549.1.9.2";

		// Token: 0x040005F2 RID: 1522
		private const string dg = "1.2.840.113549.1.9.3";

		// Token: 0x040005F3 RID: 1523
		private const string dh = "1.2.840.113549.1.9.4";

		// Token: 0x040005F4 RID: 1524
		private const string di = "1.2.840.113549.1.9.5";

		// Token: 0x040005F5 RID: 1525
		private const string dj = "1.2.840.113549.1.9.6";

		// Token: 0x040005F6 RID: 1526
		private const string dk = "1.2.840.113549.1.9.7";

		// Token: 0x040005F7 RID: 1527
		private const string dl = "1.2.840.113549.1.9.8";

		// Token: 0x040005F8 RID: 1528
		private const string dm = "1.2.840.113549.1.9.9";

		// Token: 0x040005F9 RID: 1529
		private const string dn = "1.2.840.113549.1.9.15";

		// Token: 0x040005FA RID: 1530
		private const string @do = "1.2.840.113549.1.9.15.1";

		// Token: 0x040005FB RID: 1531
		private const string dp = "1.2.840.113549.2.2";

		// Token: 0x040005FC RID: 1532
		private const string dq = "1.2.840.113549.2.4";

		// Token: 0x040005FD RID: 1533
		private const string dr = "1.2.840.113549.2.5";

		// Token: 0x040005FE RID: 1534
		private const string ds = "2.16.840.1.101.3.4.2.1";

		// Token: 0x040005FF RID: 1535
		private const string dt = "2.16.840.1.101.3.4.2.2";

		// Token: 0x04000600 RID: 1536
		private const string du = "2.16.840.1.101.3.4.2.3";

		// Token: 0x04000601 RID: 1537
		private const string dv = "1.2.840.113549.3.2";

		// Token: 0x04000602 RID: 1538
		private const string dw = "1.2.840.113549.3.4";

		// Token: 0x04000603 RID: 1539
		private const string dx = "1.2.840.113549.3.7";

		// Token: 0x04000604 RID: 1540
		private const string dy = "1.2.840.113549.3.9";

		// Token: 0x04000605 RID: 1541
		private const string dz = "2.5";

		// Token: 0x04000606 RID: 1542
		private const string d0 = "2.5.8";

		// Token: 0x04000607 RID: 1543
		private const string d1 = "2.5.8.1";

		// Token: 0x04000608 RID: 1544
		private const string d2 = "2.5.8.2";

		// Token: 0x04000609 RID: 1545
		private const string d3 = "2.5.8.3";

		// Token: 0x0400060A RID: 1546
		private const string d4 = "2.5.8.1.1";

		// Token: 0x0400060B RID: 1547
		private const string d5 = "1.3.14";

		// Token: 0x0400060C RID: 1548
		private const string d6 = "1.3.14.3.2";

		// Token: 0x0400060D RID: 1549
		private const string d7 = "1.3.14.3.2.2";

		// Token: 0x0400060E RID: 1550
		private const string d8 = "1.3.14.3.2.3";

		// Token: 0x0400060F RID: 1551
		private const string d9 = "1.3.14.3.2.4";

		// Token: 0x04000610 RID: 1552
		private const string ea = "1.3.14.3.2.6";

		// Token: 0x04000611 RID: 1553
		private const string eb = "1.3.14.3.2.7";

		// Token: 0x04000612 RID: 1554
		private const string ec = "1.3.14.3.2.8";

		// Token: 0x04000613 RID: 1555
		private const string ed = "1.3.14.3.2.9";

		// Token: 0x04000614 RID: 1556
		private const string ee = "1.3.14.3.2.10";

		// Token: 0x04000615 RID: 1557
		private const string ef = "1.3.14.3.2.11";

		// Token: 0x04000616 RID: 1558
		private const string eg = "1.3.14.3.2.12";

		// Token: 0x04000617 RID: 1559
		private const string eh = "1.3.14.3.2.13";

		// Token: 0x04000618 RID: 1560
		private const string ei = "1.3.14.3.2.14";

		// Token: 0x04000619 RID: 1561
		private const string ej = "1.3.14.3.2.15";

		// Token: 0x0400061A RID: 1562
		private const string ek = "1.3.14.3.2.16";

		// Token: 0x0400061B RID: 1563
		private const string el = "1.3.14.3.2.17";

		// Token: 0x0400061C RID: 1564
		private const string em = "1.3.14.3.2.18";

		// Token: 0x0400061D RID: 1565
		private const string en = "1.3.14.3.2.19";

		// Token: 0x0400061E RID: 1566
		private const string eo = "1.3.14.3.2.20";

		// Token: 0x0400061F RID: 1567
		private const string ep = "1.3.14.3.2.21";

		// Token: 0x04000620 RID: 1568
		private const string eq = "1.3.14.3.2.22";

		// Token: 0x04000621 RID: 1569
		private const string er = "1.3.14.3.2.23";

		// Token: 0x04000622 RID: 1570
		private const string es = "1.3.14.3.2.24";

		// Token: 0x04000623 RID: 1571
		private const string et = "1.3.14.3.2.25";

		// Token: 0x04000624 RID: 1572
		private const string eu = "1.3.14.3.2.26";

		// Token: 0x04000625 RID: 1573
		private const string ev = "1.3.14.3.2.27";

		// Token: 0x04000626 RID: 1574
		private const string ew = "1.3.14.3.2.28";

		// Token: 0x04000627 RID: 1575
		private const string ex = "1.3.14.3.2.29";

		// Token: 0x04000628 RID: 1576
		private const string ey = "1.3.14.7.2";

		// Token: 0x04000629 RID: 1577
		private const string ez = "1.3.14.7.2.1";

		// Token: 0x0400062A RID: 1578
		private const string e0 = "1.3.14.7.2.2";

		// Token: 0x0400062B RID: 1579
		private const string e1 = "1.3.14.7.2.3";

		// Token: 0x0400062C RID: 1580
		private const string e2 = "1.3.14.7.2.2.1";

		// Token: 0x0400062D RID: 1581
		private const string e3 = "1.3.14.7.2.3.1";

		// Token: 0x0400062E RID: 1582
		private const string e4 = "2.16.840.1.101.3.4.1.2";

		// Token: 0x0400062F RID: 1583
		private const string e5 = "2.16.840.1.101.3.4.1.22";

		// Token: 0x04000630 RID: 1584
		private const string e6 = "2.16.840.1.101.3.4.1.42";

		// Token: 0x04000631 RID: 1585
		private const string e7 = "2.16.840.1.101.3.4.1.5";

		// Token: 0x04000632 RID: 1586
		private const string e8 = "2.16.840.1.101.3.4.1.25";

		// Token: 0x04000633 RID: 1587
		private const string e9 = "2.16.840.1.101.3.4.1.45";

		// Token: 0x04000634 RID: 1588
		private const string fa = "2.16.840.1.101.2.1";

		// Token: 0x04000635 RID: 1589
		private const string fb = "2.16.840.1.101.2.1.1.1";

		// Token: 0x04000636 RID: 1590
		private const string fc = "2.16.840.1.101.2.1.1.2";

		// Token: 0x04000637 RID: 1591
		private const string fd = "2.16.840.1.101.2.1.1.3";

		// Token: 0x04000638 RID: 1592
		private const string fe = "2.16.840.1.101.2.1.1.4";

		// Token: 0x04000639 RID: 1593
		private const string ff = "2.16.840.1.101.2.1.1.5";

		// Token: 0x0400063A RID: 1594
		private const string fg = "2.16.840.1.101.2.1.1.6";

		// Token: 0x0400063B RID: 1595
		private const string fh = "2.16.840.1.101.2.1.1.7";

		// Token: 0x0400063C RID: 1596
		private const string fi = "2.16.840.1.101.2.1.1.8";

		// Token: 0x0400063D RID: 1597
		private const string fj = "2.16.840.1.101.2.1.1.9";

		// Token: 0x0400063E RID: 1598
		private const string fk = "2.16.840.1.101.2.1.1.10";

		// Token: 0x0400063F RID: 1599
		private const string fl = "2.16.840.1.101.2.1.1.11";

		// Token: 0x04000640 RID: 1600
		private const string fm = "2.16.840.1.101.2.1.1.12";

		// Token: 0x04000641 RID: 1601
		private const string fn = "2.16.840.1.101.2.1.1.13";

		// Token: 0x04000642 RID: 1602
		private const string fo = "2.16.840.1.101.2.1.1.14";

		// Token: 0x04000643 RID: 1603
		private const string fp = "2.16.840.1.101.2.1.1.15";

		// Token: 0x04000644 RID: 1604
		private const string fq = "2.16.840.1.101.2.1.1.16";

		// Token: 0x04000645 RID: 1605
		private const string fr = "2.16.840.1.101.2.1.1.17";

		// Token: 0x04000646 RID: 1606
		private const string fs = "2.16.840.1.101.2.1.1.18";

		// Token: 0x04000647 RID: 1607
		private const string ft = "2.16.840.1.101.2.1.1.19";

		// Token: 0x04000648 RID: 1608
		private const string fu = "2.16.840.1.101.2.1.1.20";

		// Token: 0x04000649 RID: 1609
		private const string fv = "2.16.840.1.101.2.1.1.21";

		// Token: 0x0400064A RID: 1610
		private const string fw = "2.5.4.3";

		// Token: 0x0400064B RID: 1611
		private const string fx = "2.5.4.4";

		// Token: 0x0400064C RID: 1612
		private const string fy = "2.5.4.5";

		// Token: 0x0400064D RID: 1613
		private const string fz = "2.5.4.6";

		// Token: 0x0400064E RID: 1614
		private const string f0 = "2.5.4.7";

		// Token: 0x0400064F RID: 1615
		private const string f1 = "2.5.4.8";

		// Token: 0x04000650 RID: 1616
		private const string f2 = "2.5.4.9";

		// Token: 0x04000651 RID: 1617
		private const string f3 = "2.5.4.10";

		// Token: 0x04000652 RID: 1618
		private const string f4 = "2.5.4.11";

		// Token: 0x04000653 RID: 1619
		private const string f5 = "2.5.4.12";

		// Token: 0x04000654 RID: 1620
		private const string f6 = "2.5.4.13";

		// Token: 0x04000655 RID: 1621
		private const string f7 = "2.5.4.14";

		// Token: 0x04000656 RID: 1622
		private const string f8 = "2.5.4.15";

		// Token: 0x04000657 RID: 1623
		private const string f9 = "2.5.4.16";

		// Token: 0x04000658 RID: 1624
		private const string ga = "2.5.4.17";

		// Token: 0x04000659 RID: 1625
		private const string gb = "2.5.4.18";

		// Token: 0x0400065A RID: 1626
		private const string gc = "2.5.4.19";

		// Token: 0x0400065B RID: 1627
		private const string gd = "2.5.4.20";

		// Token: 0x0400065C RID: 1628
		private const string ge = "2.5.4.21";

		// Token: 0x0400065D RID: 1629
		private const string gf = "2.5.4.22";

		// Token: 0x0400065E RID: 1630
		private const string gg = "2.5.4.23";

		// Token: 0x0400065F RID: 1631
		private const string gh = "2.5.4.24";

		// Token: 0x04000660 RID: 1632
		private const string gi = "2.5.4.25";

		// Token: 0x04000661 RID: 1633
		private const string gj = "2.5.4.26";

		// Token: 0x04000662 RID: 1634
		private const string gk = "2.5.4.27";

		// Token: 0x04000663 RID: 1635
		private const string gl = "2.5.4.28";

		// Token: 0x04000664 RID: 1636
		private const string gm = "2.5.4.29";

		// Token: 0x04000665 RID: 1637
		private const string gn = "2.5.4.30";

		// Token: 0x04000666 RID: 1638
		private const string go = "2.5.4.31";

		// Token: 0x04000667 RID: 1639
		private const string gp = "2.5.4.32";

		// Token: 0x04000668 RID: 1640
		private const string gq = "2.5.4.33";

		// Token: 0x04000669 RID: 1641
		private const string gr = "2.5.4.34";

		// Token: 0x0400066A RID: 1642
		private const string gs = "2.5.4.35";

		// Token: 0x0400066B RID: 1643
		private const string gt = "2.5.4.36";

		// Token: 0x0400066C RID: 1644
		private const string gu = "2.5.4.37";

		// Token: 0x0400066D RID: 1645
		private const string gv = "2.5.4.38";

		// Token: 0x0400066E RID: 1646
		private const string gw = "2.5.4.39";

		// Token: 0x0400066F RID: 1647
		private const string gx = "2.5.4.40";

		// Token: 0x04000670 RID: 1648
		private const string gy = "2.5.4.42";

		// Token: 0x04000671 RID: 1649
		private const string gz = "2.5.4.43";

		// Token: 0x04000672 RID: 1650
		private const string g0 = "0.9.2342.19200300.100.1.25";

		// Token: 0x04000673 RID: 1651
		private const string g1 = "2.5.29.1";

		// Token: 0x04000674 RID: 1652
		private const string g2 = "2.5.29.2";

		// Token: 0x04000675 RID: 1653
		private const string g3 = "2.5.29.4";

		// Token: 0x04000676 RID: 1654
		private const string g4 = "2.5.29.7";

		// Token: 0x04000677 RID: 1655
		private const string g5 = "2.5.29.8";

		// Token: 0x04000678 RID: 1656
		private const string g6 = "2.5.29.10";

		// Token: 0x04000679 RID: 1657
		private const string g7 = "2.5.29.15";

		// Token: 0x0400067A RID: 1658
		private const string g8 = "2.5.29.19";

		// Token: 0x0400067B RID: 1659
		private const string g9 = "2.5.29.32";

		// Token: 0x0400067C RID: 1660
		private const string ha = "2.5.29.35";

		// Token: 0x0400067D RID: 1661
		private const string hb = "2.5.29.14";

		// Token: 0x0400067E RID: 1662
		private const string hc = "2.5.29.17";

		// Token: 0x0400067F RID: 1663
		private const string hd = "2.5.29.18";

		// Token: 0x04000680 RID: 1664
		private const string he = "2.5.29.21";

		// Token: 0x04000681 RID: 1665
		private const string hf = "2.5.29.31";

		// Token: 0x04000682 RID: 1666
		private const string hg = "2.5.29.37";

		// Token: 0x04000683 RID: 1667
		private const string hh = "1.3.6.1.5.5.7";

		// Token: 0x04000684 RID: 1668
		private const string hi = "1.3.6.1.5.5.7.2";

		// Token: 0x04000685 RID: 1669
		private const string hj = "1.3.6.1.4.1.311.2.1.14";

		// Token: 0x04000686 RID: 1670
		private const string hk = "1.3.6.1.4.1.311.10.2";

		// Token: 0x04000687 RID: 1671
		private const string hl = "1.3.6.1.4.1.311.10.1";

		// Token: 0x04000688 RID: 1672
		private const string hm = "2.5.29.5";

		// Token: 0x04000689 RID: 1673
		private const string hn = "2.5.29.9";

		// Token: 0x0400068A RID: 1674
		private const string ho = "1.3.6.1.5.5.7.3";

		// Token: 0x0400068B RID: 1675
		private const string hp = "1.3.6.1.5.5.7.3.1";

		// Token: 0x0400068C RID: 1676
		private const string hq = "1.3.6.1.5.5.7.3.2";

		// Token: 0x0400068D RID: 1677
		private const string hr = "1.3.6.1.5.5.7.3.3";

		// Token: 0x0400068E RID: 1678
		private const string hs = "1.3.6.1.5.5.7.3.4";

		// Token: 0x0400068F RID: 1679
		private const string ht = "1.3.6.1.4.1.311.10.3.1";

		// Token: 0x04000690 RID: 1680
		private const string hu = "1.3.6.1.4.1.311.10.3.2";

		// Token: 0x04000691 RID: 1681
		private const string hv = "1.3.6.1.4.1.311.10.4.1";

		// Token: 0x04000692 RID: 1682
		private const string hw = "2.16.840.1.113730";

		// Token: 0x04000693 RID: 1683
		private const string hx = "2.16.840.1.113730.1";

		// Token: 0x04000694 RID: 1684
		private const string hy = "2.16.840.1.113730.1.1";

		// Token: 0x04000695 RID: 1685
		private const string hz = "2.16.840.1.113730.1.2";

		// Token: 0x04000696 RID: 1686
		private const string h0 = "2.16.840.1.113730.1.3";

		// Token: 0x04000697 RID: 1687
		private const string h1 = "2.16.840.1.113730.1.4";

		// Token: 0x04000698 RID: 1688
		private const string h2 = "2.16.840.1.113730.1.7";

		// Token: 0x04000699 RID: 1689
		private const string h3 = "2.16.840.1.113730.1.8";

		// Token: 0x0400069A RID: 1690
		private const string h4 = "2.16.840.1.113730.1.12";

		// Token: 0x0400069B RID: 1691
		private const string h5 = "2.16.840.1.113730.1.13";

		// Token: 0x0400069C RID: 1692
		private const string h6 = "2.16.840.1.113730.2";

		// Token: 0x0400069D RID: 1693
		private const string h7 = "2.16.840.1.113730.2.5";

		// Token: 0x0400069E RID: 1694
		private const string h8 = "1.2.840.113549.1.7.1";

		// Token: 0x0400069F RID: 1695
		private const string h9 = "1.2.840.113549.1.7.2";

		// Token: 0x040006A0 RID: 1696
		private const string ia = "1.2.840.113549.1.7.3";

		// Token: 0x040006A1 RID: 1697
		private const string ib = "1.2.840.113549.1.7.4";

		// Token: 0x040006A2 RID: 1698
		private const string ic = "1.2.840.113549.1.7.5";

		// Token: 0x040006A3 RID: 1699
		private const string id = "1.2.840.113549.1.7.6";

		// Token: 0x040006A4 RID: 1700
		private const string ie = "1.2.840.113549.1.9.3";

		// Token: 0x040006A5 RID: 1701
		private const string @if = "1.2.840.113549.1.9.4";

		// Token: 0x040006A6 RID: 1702
		private int ig;

		// Token: 0x040006A7 RID: 1703
		private int ih;

		// Token: 0x040006A8 RID: 1704
		private AlgorithmCategory ii = AlgorithmCategory.Unknown;

		// Token: 0x040006A9 RID: 1705
		private string ij = string.Empty;

		// Token: 0x040006AA RID: 1706
		private string ik = string.Empty;

		// Token: 0x040006AB RID: 1707
		private static Hashtable il = new Hashtable();

		// Token: 0x040006AC RID: 1708
		private static Hashtable im = new Hashtable();
	}
}
