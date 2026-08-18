using System;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005A0 RID: 1440
	[Serializable]
	internal class SummaryInformation : SpecialPropertySet
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000E2F63 File Offset: 0x000E1F63
		public override ch PropertySetIDMap
		{
			get
			{
				return ch.b();
			}
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000E2F6A File Offset: 0x000E1F6A
		public SummaryInformation(PropertySet A_0) : base(A_0)
		{
			if (!this.IsSummaryInformation)
			{
				throw new UnexpectedPropertySetTypeException("Not a " + base.GetType().Name);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000E2F96 File Offset: 0x000E1F96
		// (set) Token: 0x06003042 RID: 12354 RVA: 0x000E2FA4 File Offset: 0x000E1FA4
		public string Title
		{
			get
			{
				return (string)this.c6(2);
			}
			set
			{
				((d)this.FirstSection).a(2, value);
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000E2FB8 File Offset: 0x000E1FB8
		public void ai()
		{
			((d)this.FirstSection).a(2L);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000E2FCC File Offset: 0x000E1FCC
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000E2FDA File Offset: 0x000E1FDA
		public string Subject
		{
			get
			{
				return (string)this.c6(3);
			}
			set
			{
				((d)this.FirstSection).a(3, value);
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000E2FEE File Offset: 0x000E1FEE
		public void ae()
		{
			((d)this.FirstSection).a(3L);
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000E3002 File Offset: 0x000E2002
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x000E3010 File Offset: 0x000E2010
		public string Author
		{
			get
			{
				return (string)this.c6(4);
			}
			set
			{
				((d)this.FirstSection).a(4, value);
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000E3024 File Offset: 0x000E2024
		public void r()
		{
			((d)this.FirstSection).a(4L);
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000E3038 File Offset: 0x000E2038
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x000E3046 File Offset: 0x000E2046
		public string Keywords
		{
			get
			{
				return (string)this.c6(5);
			}
			set
			{
				((d)this.FirstSection).a(5, value);
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000E305A File Offset: 0x000E205A
		public void g()
		{
			((d)this.FirstSection).a(5L);
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000E306E File Offset: 0x000E206E
		// (set) Token: 0x0600304E RID: 12366 RVA: 0x000E307C File Offset: 0x000E207C
		public string Comments
		{
			get
			{
				return (string)this.c6(6);
			}
			set
			{
				((d)this.FirstSection).a(6, value);
			}
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000E3090 File Offset: 0x000E2090
		public void ag()
		{
			((d)this.FirstSection).a(6L);
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000E30A4 File Offset: 0x000E20A4
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x000E30B2 File Offset: 0x000E20B2
		public string Template
		{
			get
			{
				return (string)this.c6(7);
			}
			set
			{
				((d)this.FirstSection).a(7, value);
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000E30C6 File Offset: 0x000E20C6
		public void j()
		{
			((d)this.FirstSection).a(7L);
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x000E30DA File Offset: 0x000E20DA
		// (set) Token: 0x06003054 RID: 12372 RVA: 0x000E30E8 File Offset: 0x000E20E8
		public string LastAuthor
		{
			get
			{
				return (string)this.c6(8);
			}
			set
			{
				((d)this.FirstSection).a(8, value);
			}
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000E30FC File Offset: 0x000E20FC
		public void t()
		{
			((d)this.FirstSection).a(8L);
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000E3110 File Offset: 0x000E2110
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x000E311F File Offset: 0x000E211F
		public string RevNumber
		{
			get
			{
				return (string)this.c6(9);
			}
			set
			{
				((d)this.FirstSection).a(9, value);
			}
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000E3134 File Offset: 0x000E2134
		public void m()
		{
			((d)this.FirstSection).a(9L);
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000E3149 File Offset: 0x000E2149
		// (set) Token: 0x0600305A RID: 12378 RVA: 0x000E316C File Offset: 0x000E216C
		public long EditTime
		{
			get
			{
				if (this.c6(10) == null)
				{
					return 0L;
				}
				return a8.a((DateTime)this.c6(10));
			}
			set
			{
				DateTime dateTime = a8.a(value);
				((d)this.FirstSection).a(10, 64L, dateTime);
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000E319B File Offset: 0x000E219B
		public void s()
		{
			((d)this.FirstSection).a(10L);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000E31B0 File Offset: 0x000E21B0
		// (set) Token: 0x0600305D RID: 12381 RVA: 0x000E31BF File Offset: 0x000E21BF
		public DateTime? LastPrinted
		{
			get
			{
				return (DateTime?)this.c6(11);
			}
			set
			{
				((d)this.FirstSection).a(11, 64L, value);
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000E31DC File Offset: 0x000E21DC
		public void aa()
		{
			((d)this.FirstSection).a(11L);
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000E31F1 File Offset: 0x000E21F1
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000E3200 File Offset: 0x000E2200
		public DateTime? CreateDateTime
		{
			get
			{
				return (DateTime?)this.c6(12);
			}
			set
			{
				((d)this.FirstSection).a(12, 64L, value);
			}
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000E321D File Offset: 0x000E221D
		public void ad()
		{
			((d)this.FirstSection).a(12L);
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000E3232 File Offset: 0x000E2232
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x000E3241 File Offset: 0x000E2241
		public DateTime? LastSaveDateTime
		{
			get
			{
				return (DateTime?)this.c6(13);
			}
			set
			{
				((d)this.FirstSection).a(13, 64L, value);
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000E325E File Offset: 0x000E225E
		public void l()
		{
			((d)this.FirstSection).a(13L);
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000E3273 File Offset: 0x000E2273
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x000E327D File Offset: 0x000E227D
		public int PageCount
		{
			get
			{
				return this.c8(14);
			}
			set
			{
				((d)this.FirstSection).a(14, value);
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000E3292 File Offset: 0x000E2292
		public void o()
		{
			((d)this.FirstSection).a(14L);
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000E32A7 File Offset: 0x000E22A7
		// (set) Token: 0x06003069 RID: 12393 RVA: 0x000E32B1 File Offset: 0x000E22B1
		public int WordCount
		{
			get
			{
				return this.c8(15);
			}
			set
			{
				((d)this.FirstSection).a(15, value);
			}
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000E32C6 File Offset: 0x000E22C6
		public void ac()
		{
			((d)this.FirstSection).a(15L);
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000E32DB File Offset: 0x000E22DB
		// (set) Token: 0x0600306C RID: 12396 RVA: 0x000E32E5 File Offset: 0x000E22E5
		public int CharCount
		{
			get
			{
				return this.c8(16);
			}
			set
			{
				((d)this.FirstSection).a(16, value);
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000E32FA File Offset: 0x000E22FA
		public void n()
		{
			((d)this.FirstSection).a(16L);
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000E330F File Offset: 0x000E230F
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x000E331E File Offset: 0x000E231E
		public byte[] Thumbnail
		{
			get
			{
				return (byte[])this.c6(17);
			}
			set
			{
				((d)this.FirstSection).a(17, 30L, value);
			}
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000E3336 File Offset: 0x000E2336
		public void x()
		{
			((d)this.FirstSection).a(17L);
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000E334B File Offset: 0x000E234B
		// (set) Token: 0x06003072 RID: 12402 RVA: 0x000E335A File Offset: 0x000E235A
		public string ApplicationName
		{
			get
			{
				return (string)this.c6(18);
			}
			set
			{
				((d)this.FirstSection).a(18, value);
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000E336F File Offset: 0x000E236F
		public void u()
		{
			((d)this.FirstSection).a(18L);
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000E3384 File Offset: 0x000E2384
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x000E338E File Offset: 0x000E238E
		public int Security
		{
			get
			{
				return this.c8(19);
			}
			set
			{
				((d)this.FirstSection).a(19, value);
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000E33A3 File Offset: 0x000E23A3
		public void aj()
		{
			((d)this.FirstSection).a(19L);
		}

		// Token: 0x0400202A RID: 8234
		public const string DEFAULT_STREAM_NAME = "\u0005SummaryInformation";
	}
}
