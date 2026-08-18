using System;
using System.Collections.Generic;
using System.IO;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x02000590 RID: 1424
	[Serializable]
	internal abstract class SpecialPropertySet : MutablePropertySet
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002FC7 RID: 12231
		public abstract ch PropertySetIDMap { get; }

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000E2290 File Offset: 0x000E1290
		public SpecialPropertySet(PropertySet A_0)
		{
			this.delegate1 = new MutablePropertySet(A_0);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000E22A4 File Offset: 0x000E12A4
		public SpecialPropertySet(MutablePropertySet A_0)
		{
			this.delegate1 = A_0;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000E22B3 File Offset: 0x000E12B3
		// (set) Token: 0x06002FCB RID: 12235 RVA: 0x000E22C0 File Offset: 0x000E12C0
		public override int ByteOrder
		{
			get
			{
				return this.delegate1.ByteOrder;
			}
			set
			{
				this.delegate1.ByteOrder = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000E22CE File Offset: 0x000E12CE
		// (set) Token: 0x06002FCD RID: 12237 RVA: 0x000E22DB File Offset: 0x000E12DB
		public override int Format
		{
			get
			{
				return this.delegate1.Format;
			}
			set
			{
				this.delegate1.Format = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000E22E9 File Offset: 0x000E12E9
		// (set) Token: 0x06002FCF RID: 12239 RVA: 0x000E22F6 File Offset: 0x000E12F6
		public override ar ClassID
		{
			get
			{
				return this.delegate1.ClassID;
			}
			set
			{
				this.delegate1.ClassID = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000E2304 File Offset: 0x000E1304
		public override int SectionCount
		{
			get
			{
				return this.delegate1.SectionCount;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000E2311 File Offset: 0x000E1311
		public override List<g7> Sections
		{
			get
			{
				return this.delegate1.Sections;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000E231E File Offset: 0x000E131E
		public override bool IsSummaryInformation
		{
			get
			{
				return this.delegate1.IsSummaryInformation;
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000E232B File Offset: 0x000E132B
		public override Stream cv()
		{
			return this.delegate1.cv();
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000E2338 File Offset: 0x000E1338
		public override bool IsDocumentSummaryInformation
		{
			get
			{
				return this.delegate1.IsDocumentSummaryInformation;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000E2345 File Offset: 0x000E1345
		public override g7 FirstSection
		{
			get
			{
				return this.delegate1.FirstSection;
			}
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000E2352 File Offset: 0x000E1352
		public override void cy(g7 A_0)
		{
			this.delegate1.cy(A_0);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000E2360 File Offset: 0x000E1360
		public override void cz()
		{
			this.delegate1.cz();
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000E237B File Offset: 0x000E137B
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x000E236D File Offset: 0x000E136D
		public override int OSVersion
		{
			get
			{
				return this.delegate1.OSVersion;
			}
			set
			{
				this.delegate1.OSVersion = value;
			}
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000E2388 File Offset: 0x000E1388
		public override Stream c2()
		{
			return this.delegate1.c2();
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000E2395 File Offset: 0x000E1395
		public override void c3(ig A_0, string A_1)
		{
			this.delegate1.c3(A_0, A_1);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000E23A4 File Offset: 0x000E13A4
		public override void c4(Stream A_0)
		{
			this.delegate1.c4(A_0);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000E23B2 File Offset: 0x000E13B2
		public override bool Equals(object o)
		{
			return this.delegate1.Equals(o);
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000E23C0 File Offset: 0x000E13C0
		public override em[] Properties
		{
			get
			{
				return this.delegate1.Properties;
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000E23CD File Offset: 0x000E13CD
		public override object c6(int A_0)
		{
			return this.delegate1.c6(A_0);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000E23DB File Offset: 0x000E13DB
		public override bool c7(int A_0)
		{
			return this.delegate1.c7(A_0);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000E23E9 File Offset: 0x000E13E9
		public override int c8(int A_0)
		{
			return this.delegate1.c8(A_0);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000E23F7 File Offset: 0x000E13F7
		public override int GetHashCode()
		{
			return this.delegate1.GetHashCode();
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000E2404 File Offset: 0x000E1404
		public override string ToString()
		{
			return this.delegate1.ToString();
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000E2411 File Offset: 0x000E1411
		public override bool WasNull
		{
			get
			{
				return this.delegate1.WasNull;
			}
		}

		// Token: 0x0400201C RID: 8220
		private MutablePropertySet delegate1;
	}
}
