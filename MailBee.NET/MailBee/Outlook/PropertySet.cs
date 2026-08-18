using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x0200059A RID: 1434
	[Serializable]
	internal class PropertySet
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x000E2900 File Offset: 0x000E1900
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x000E2908 File Offset: 0x000E1908
		public virtual int ByteOrder
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

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000E2911 File Offset: 0x000E1911
		// (set) Token: 0x06003016 RID: 12310 RVA: 0x000E2919 File Offset: 0x000E1919
		public virtual int Format
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

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000E2922 File Offset: 0x000E1922
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x000E292A File Offset: 0x000E192A
		public virtual int OSVersion
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

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000E2933 File Offset: 0x000E1933
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x000E293B File Offset: 0x000E193B
		public virtual ar ClassID
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

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000E2944 File Offset: 0x000E1944
		public virtual int SectionCount
		{
			get
			{
				return this.sections.Count;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x000E2951 File Offset: 0x000E1951
		public virtual List<g7> Sections
		{
			get
			{
				return this.sections;
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000E2959 File Offset: 0x000E1959
		protected PropertySet()
		{
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000E2964 File Offset: 0x000E1964
		public PropertySet(Stream A_0)
		{
			if (PropertySet.a(A_0))
			{
				byte[] array = new byte[(A_0 as e5).aq()];
				A_0.Read(array, 0, array.Length);
				this.a(array, 0, array.Length);
				return;
			}
			throw new NoPropertySetStreamException("this stream may not be a valid property set stream");
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000E29B2 File Offset: 0x000E19B2
		public PropertySet(byte[] A_0, int A_1, int A_2)
		{
			if (PropertySet.b(A_0, A_1, A_2))
			{
				this.a(A_0, A_1, A_2);
				return;
			}
			throw new NoPropertySetStreamException();
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000E29D3 File Offset: 0x000E19D3
		public PropertySet(byte[] A_0) : this(A_0, 0, A_0.Length)
		{
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000E29E0 File Offset: 0x000E19E0
		public static bool a(Stream A_0)
		{
			e5 e = A_0 as e5;
			int num = 50;
			if (e == null || !e.cc())
			{
				throw new MarkUnsupportedException(A_0.GetType().Name);
			}
			e.ar(num);
			byte[] array = new byte[num];
			int a_ = A_0.Read(array, 0, Math.Min(array.Length, e.aq()));
			bool result = PropertySet.b(array, 0, a_);
			e.at();
			return result;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000E2A48 File Offset: 0x000E1A48
		public static bool b(byte[] A_0, int A_1, int A_2)
		{
			int num = p.j(A_0, A_1);
			int num2 = A_1 + 2;
			byte[] a_ = new byte[2];
			p.a(a_, 0, (short)num);
			if (!d4.a(a_, PropertySet.BYTE_ORDER_ASSERTION))
			{
				return false;
			}
			int num3 = p.j(A_0, num2);
			num2 += 2;
			byte[] a_2 = new byte[2];
			p.a(a_2, 0, (short)num3);
			if (!d4.a(a_2, PropertySet.FORMAT_ASSERTION))
			{
				return false;
			}
			p.h(A_0, A_1);
			num2 += 4;
			new ar(A_0, A_1);
			num2 += 16;
			long num4 = p.h(A_0, num2);
			num2 += 4;
			return num4 >= 0L;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000E2AD4 File Offset: 0x000E1AD4
		private void a(byte[] A_0, int A_1, int A_2)
		{
			this.byteOrder = p.j(A_0, A_1);
			int num = A_1 + 2;
			this.format = p.j(A_0, num);
			num += 2;
			this.osVersion = (int)p.h(A_0, num);
			num += 4;
			this.a = new ar(A_0, num);
			num += 16;
			int num2 = p.i(A_0, num);
			num += 4;
			if (num2 < 0)
			{
				throw new HPSFRuntimeException("Section count " + num2 + " is negative.");
			}
			this.sections = new List<g7>(num2);
			for (int i = 0; i < num2; i++)
			{
				g7 item = new g7(A_0, num);
				num += this.ClassID.b() + 4;
				this.sections.Add(item);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000E2B90 File Offset: 0x000E1B90
		public virtual bool IsSummaryInformation
		{
			get
			{
				return this.sections.Count > 0 && d4.a(this.sections[0].e().a(), @as.a);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000E2BC2 File Offset: 0x000E1BC2
		public virtual bool IsDocumentSummaryInformation
		{
			get
			{
				return this.sections.Count > 0 && d4.a(this.sections[0].e().a(), @as.b);
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06003026 RID: 12326 RVA: 0x000E2BF4 File Offset: 0x000E1BF4
		public virtual em[] Properties
		{
			get
			{
				return this.FirstSection.aj();
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000E2C01 File Offset: 0x000E1C01
		public virtual object c6(int A_0)
		{
			return this.FirstSection.ak((long)A_0);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000E2C10 File Offset: 0x000E1C10
		public virtual bool c7(int A_0)
		{
			return this.FirstSection.b(A_0);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000E2C1E File Offset: 0x000E1C1E
		public virtual int c8(int A_0)
		{
			return this.FirstSection.c((long)A_0);
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x000E2C2D File Offset: 0x000E1C2D
		public virtual bool WasNull
		{
			get
			{
				return this.FirstSection.f();
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x000E2C3A File Offset: 0x000E1C3A
		public virtual g7 FirstSection
		{
			get
			{
				if (this.SectionCount < 1)
				{
					throw new MissingSectionException("Property Set does not contain any sections.");
				}
				return this.sections[0];
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000E2C5C File Offset: 0x000E1C5C
		public g7 SingleSection
		{
			get
			{
				int num = this.SectionCount;
				if (num != 1)
				{
					throw new NoSingleSectionException("Property Set Contains " + num + " sections.");
				}
				return this.sections[0];
			}
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000E2C9C File Offset: 0x000E1C9C
		public override bool Equals(object o)
		{
			if (o == null || !(o is PropertySet))
			{
				return false;
			}
			PropertySet propertySet = (PropertySet)o;
			int num = propertySet.ByteOrder;
			int num2 = this.ByteOrder;
			ar ar = propertySet.ClassID;
			ar obj = this.ClassID;
			int num3 = propertySet.Format;
			int num4 = this.Format;
			int num5 = propertySet.OSVersion;
			int num6 = this.OSVersion;
			int num7 = propertySet.SectionCount;
			int num8 = this.SectionCount;
			return num == num2 && ar.Equals(obj) && num3 == num4 && num5 == num6 && num7 == num8 && a8.b(this.Sections, propertySet.Sections);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000E2D39 File Offset: 0x000E1D39
		public override int GetHashCode()
		{
			throw new InvalidOperationException("FIXME: Not yet implemented.");
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000E2D48 File Offset: 0x000E1D48
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int value = this.SectionCount;
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append('[');
			stringBuilder.Append("byteOrder: ");
			stringBuilder.Append(this.ByteOrder);
			stringBuilder.Append(", classID: ");
			stringBuilder.Append(this.ClassID);
			stringBuilder.Append(", format: ");
			stringBuilder.Append(this.Format);
			stringBuilder.Append(", OSVersion: ");
			stringBuilder.Append(this.OSVersion);
			stringBuilder.Append(", sectionCount: ");
			stringBuilder.Append(value);
			stringBuilder.Append(", sections: [\n");
			foreach (g7 g in this.Sections)
			{
				stringBuilder.Append(g.ToString());
			}
			stringBuilder.Append(']');
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04002020 RID: 8224
		protected static byte[] BYTE_ORDER_ASSERTION = new byte[]
		{
			254,
			byte.MaxValue
		};

		// Token: 0x04002021 RID: 8225
		protected int byteOrder;

		// Token: 0x04002022 RID: 8226
		protected static byte[] FORMAT_ASSERTION = new byte[2];

		// Token: 0x04002023 RID: 8227
		protected int format;

		// Token: 0x04002024 RID: 8228
		protected int osVersion;

		// Token: 0x04002025 RID: 8229
		public const int OS_WIN16 = 0;

		// Token: 0x04002026 RID: 8230
		public const int OS_MACINTOSH = 1;

		// Token: 0x04002027 RID: 8231
		public const int OS_WIN32 = 2;

		// Token: 0x04002028 RID: 8232
		[NonSerialized]
		protected ar a;

		// Token: 0x04002029 RID: 8233
		protected List<g7> sections;
	}
}
