using System;
using System.Collections;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x0200058F RID: 1423
	[Serializable]
	internal class DocumentSummaryInformation : SpecialPropertySet
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x000E1DC9 File Offset: 0x000E0DC9
		public override ch PropertySetIDMap
		{
			get
			{
				return ch.a();
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000E1DD0 File Offset: 0x000E0DD0
		public DocumentSummaryInformation(PropertySet A_0) : base(A_0)
		{
			if (!this.IsDocumentSummaryInformation)
			{
				throw new UnexpectedPropertySetTypeException("Not a " + base.GetType().Name);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000E1DFC File Offset: 0x000E0DFC
		// (set) Token: 0x06002F97 RID: 12183 RVA: 0x000E1E0A File Offset: 0x000E0E0A
		public string Category
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

		// Token: 0x06002F98 RID: 12184 RVA: 0x000E1E1E File Offset: 0x000E0E1E
		public void o()
		{
			((d)this.FirstSection).a(2L);
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000E1E32 File Offset: 0x000E0E32
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000E1E40 File Offset: 0x000E0E40
		public string PresentationFormat
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

		// Token: 0x06002F9B RID: 12187 RVA: 0x000E1E54 File Offset: 0x000E0E54
		public void ag()
		{
			((d)this.FirstSection).a(3L);
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000E1E68 File Offset: 0x000E0E68
		// (set) Token: 0x06002F9D RID: 12189 RVA: 0x000E1E71 File Offset: 0x000E0E71
		public int ByteCount
		{
			get
			{
				return this.c8(4);
			}
			set
			{
				((d)this.FirstSection).a(4, value);
			}
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000E1E85 File Offset: 0x000E0E85
		public void aa()
		{
			((d)this.FirstSection).a(4L);
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000E1E99 File Offset: 0x000E0E99
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x000E1EA2 File Offset: 0x000E0EA2
		public int LineCount
		{
			get
			{
				return this.c8(5);
			}
			set
			{
				((d)this.FirstSection).a(5, value);
			}
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000E1EB6 File Offset: 0x000E0EB6
		public void s()
		{
			((d)this.FirstSection).a(5L);
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000E1ECA File Offset: 0x000E0ECA
		// (set) Token: 0x06002FA3 RID: 12195 RVA: 0x000E1ED3 File Offset: 0x000E0ED3
		public int ParCount
		{
			get
			{
				return this.c8(6);
			}
			set
			{
				((d)this.FirstSection).a(6, value);
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000E1EE7 File Offset: 0x000E0EE7
		public void f()
		{
			((d)this.FirstSection).a(6L);
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000E1EFB File Offset: 0x000E0EFB
		// (set) Token: 0x06002FA6 RID: 12198 RVA: 0x000E1F04 File Offset: 0x000E0F04
		public int SlideCount
		{
			get
			{
				return this.c8(7);
			}
			set
			{
				((d)this.FirstSection).a(7, value);
			}
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000E1F18 File Offset: 0x000E0F18
		public void w()
		{
			((d)this.FirstSection).a(7L);
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000E1F2C File Offset: 0x000E0F2C
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x000E1F35 File Offset: 0x000E0F35
		public int NoteCount
		{
			get
			{
				return this.c8(8);
			}
			set
			{
				((d)this.FirstSection).a(8, value);
			}
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000E1F49 File Offset: 0x000E0F49
		public void l()
		{
			((d)this.FirstSection).a(8L);
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000E1F5D File Offset: 0x000E0F5D
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x000E1F67 File Offset: 0x000E0F67
		public int HiddenCount
		{
			get
			{
				return this.c8(9);
			}
			set
			{
				((d)this.Sections[0]).a(9, value);
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000E1F82 File Offset: 0x000E0F82
		public void x()
		{
			((d)this.FirstSection).a(9L);
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000E1F97 File Offset: 0x000E0F97
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x000E1FA1 File Offset: 0x000E0FA1
		public int MMClipCount
		{
			get
			{
				return this.c8(10);
			}
			set
			{
				((d)this.FirstSection).a(10, value);
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000E1FB6 File Offset: 0x000E0FB6
		public void g()
		{
			((d)this.FirstSection).a(10L);
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000E1FCB File Offset: 0x000E0FCB
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x000E1FD5 File Offset: 0x000E0FD5
		public bool Scale
		{
			get
			{
				return this.c7(11);
			}
			set
			{
				((d)this.FirstSection).b(11, value);
			}
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000E1FEA File Offset: 0x000E0FEA
		public void h()
		{
			((d)this.FirstSection).a(11L);
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000E1FFF File Offset: 0x000E0FFF
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x000E200E File Offset: 0x000E100E
		public byte[] HeadingPair
		{
			get
			{
				return (byte[])this.c6(12);
			}
			set
			{
				throw new NotImplementedException("Writing byte arrays ");
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000E201A File Offset: 0x000E101A
		public void y()
		{
			((d)this.FirstSection).a(12L);
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000E202F File Offset: 0x000E102F
		// (set) Token: 0x06002FB8 RID: 12216 RVA: 0x000E203E File Offset: 0x000E103E
		public byte[] Docparts
		{
			get
			{
				return (byte[])this.c6(13);
			}
			set
			{
				throw new NotImplementedException("Writing byte arrays");
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000E204A File Offset: 0x000E104A
		public void ab()
		{
			((d)this.FirstSection).a(13L);
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000E205F File Offset: 0x000E105F
		// (set) Token: 0x06002FBB RID: 12219 RVA: 0x000E206E File Offset: 0x000E106E
		public string Manager
		{
			get
			{
				return (string)this.c6(14);
			}
			set
			{
				((d)this.FirstSection).a(14, value);
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000E2083 File Offset: 0x000E1083
		public void e()
		{
			((d)this.FirstSection).a(14L);
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000E2098 File Offset: 0x000E1098
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x000E20A7 File Offset: 0x000E10A7
		public string Company
		{
			get
			{
				return (string)this.c6(15);
			}
			set
			{
				((d)this.FirstSection).a(15, value);
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000E20BC File Offset: 0x000E10BC
		public void k()
		{
			((d)this.FirstSection).a(15L);
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000E20D1 File Offset: 0x000E10D1
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x000E20DB File Offset: 0x000E10DB
		public bool LinksDirty
		{
			get
			{
				return this.c7(16);
			}
			set
			{
				((d)this.FirstSection).b(16, value);
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000E20F0 File Offset: 0x000E10F0
		public void b()
		{
			((d)this.FirstSection).a(16L);
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000E2108 File Offset: 0x000E1108
		// (set) Token: 0x06002FC4 RID: 12228 RVA: 0x000E21B0 File Offset: 0x000E11B0
		public m CustomProperties
		{
			get
			{
				m m = null;
				if (this.SectionCount >= 2)
				{
					m = new m();
					g7 g = this.Sections[1];
					IDictionary dictionary = g.al();
					em[] array = g.aj();
					int num = 0;
					foreach (em em in array)
					{
						long num2 = em.e();
						if (num2 != 0L && num2 != 1L)
						{
							num++;
							cz cz = new cz(em, (string)dictionary[num2]);
							m.a(cz.a(), cz);
						}
					}
					if (m.Count != num)
					{
						m.a(false);
					}
				}
				return m;
			}
			set
			{
				this.a();
				d d = (d)this.Sections[1];
				IDictionary a_ = value.f();
				d.d();
				int num = value.a();
				if (num < 0)
				{
					num = d.a();
				}
				if (num < 0)
				{
					num = 1200;
				}
				value.a(num);
				d.a(num);
				d.am(a_);
				foreach (object obj in value.Values)
				{
					em a_2 = (em)obj;
					d.a(a_2);
				}
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000E2240 File Offset: 0x000E1240
		private new void a()
		{
			if (this.SectionCount < 2)
			{
				d d = new d();
				d.a(@as.c);
				this.cy(d);
			}
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000E226E File Offset: 0x000E126E
		public void m()
		{
			if (this.SectionCount >= 2)
			{
				this.Sections.RemoveAt(1);
				return;
			}
			throw new HPSFRuntimeException("Illegal internal format of Document SummaryInformation stream: second section is missing.");
		}

		// Token: 0x0400201B RID: 8219
		public const string DEFAULT_STREAM_NAME = "\u0005DocumentSummaryInformation";
	}
}
