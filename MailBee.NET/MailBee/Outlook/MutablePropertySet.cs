using System;
using System.Collections.Generic;
using System.IO;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x02000599 RID: 1433
	[Serializable]
	internal class MutablePropertySet : PropertySet
	{
		// Token: 0x06003003 RID: 12291 RVA: 0x000E25A0 File Offset: 0x000E15A0
		public MutablePropertySet()
		{
			this.byteOrder = p.g(PropertySet.BYTE_ORDER_ASSERTION);
			this.format = p.g(PropertySet.FORMAT_ASSERTION);
			this.osVersion = 133636;
			this.a = new ar();
			this.sections = new List<g7>();
			this.sections.Add(new d());
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000E2620 File Offset: 0x000E1620
		public MutablePropertySet(PropertySet A_0)
		{
			this.byteOrder = A_0.ByteOrder;
			this.format = A_0.Format;
			this.osVersion = A_0.OSVersion;
			this.ClassID = A_0.ClassID;
			this.cz();
			if (this.sections == null)
			{
				this.sections = new List<g7>();
			}
			foreach (object obj in A_0.Sections)
			{
				d a_ = new d((g7)obj);
				this.cy(a_);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000E26CB File Offset: 0x000E16CB
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x000E26D3 File Offset: 0x000E16D3
		public override int ByteOrder
		{
			get
			{
				return this.byteOrder;
			}
			set
			{
				this.byteOrder = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000E26E5 File Offset: 0x000E16E5
		// (set) Token: 0x06003007 RID: 12295 RVA: 0x000E26DC File Offset: 0x000E16DC
		public override int Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000E26F6 File Offset: 0x000E16F6
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x000E26ED File Offset: 0x000E16ED
		public override int OSVersion
		{
			get
			{
				return this.osVersion;
			}
			set
			{
				this.osVersion = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000E2707 File Offset: 0x000E1707
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x000E26FE File Offset: 0x000E16FE
		public override ar ClassID
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000E270F File Offset: 0x000E170F
		public virtual void cz()
		{
			this.sections = null;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000E2718 File Offset: 0x000E1718
		public virtual void cy(g7 A_0)
		{
			if (this.sections == null)
			{
				this.sections = new List<g7>();
			}
			this.sections.Add(A_0);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000E273C File Offset: 0x000E173C
		public virtual void c4(Stream A_0)
		{
			int count = this.sections.Count;
			int num = 0;
			num += h7.a(A_0, (short)this.ByteOrder);
			num += h7.a(A_0, (short)this.Format);
			num += h7.b(A_0, this.OSVersion);
			num += h7.a(A_0, this.ClassID);
			num += h7.b(A_0, count);
			int num2 = this.OFFSET_HEADER;
			num2 += count * (this.ClassID.b() + 4);
			int num3 = num2;
			foreach (object obj in this.sections)
			{
				d d = (d)obj;
				if (d.e() == null)
				{
					throw new NoFormatIDException();
				}
				num += h7.a(A_0, d.e());
				num += h7.a(A_0, (uint)num2);
				num2 += d.ah();
			}
			num2 = num3;
			foreach (object obj2 in this.sections)
			{
				d d2 = (d)obj2;
				num2 += d2.a(A_0);
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000E2850 File Offset: 0x000E1850
		public virtual Stream c2()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.c4(memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000E2874 File Offset: 0x000E1874
		public virtual Stream cv()
		{
			Stream result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.c4(memoryStream);
				memoryStream.Flush();
				result = new MemoryStream(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000E28C0 File Offset: 0x000E18C0
		public virtual void c3(ig A_0, string A_1)
		{
			try
			{
				A_0.el(A_1).u();
			}
			catch (FileNotFoundException)
			{
			}
			A_0.em(A_1, this.c2());
		}

		// Token: 0x0400201F RID: 8223
		private int OFFSET_HEADER = PropertySet.BYTE_ORDER_ASSERTION.Length + PropertySet.FORMAT_ASSERTION.Length + 4 + 16 + 4;
	}
}
