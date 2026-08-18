using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class PaperSize
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x0001DD97 File Offset: 0x0001BF97
		public PaperSize()
		{
			this.kind = PaperKind.Custom;
			this.name = string.Empty;
			this.createdByDefaultConstructor = true;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		internal PaperSize(PaperKind kind, string name, int width, int height)
		{
			this.kind = kind;
			this.name = name;
			this.width = width;
			this.height = height;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001DDDD File Offset: 0x0001BFDD
		public PaperSize(string name, int width, int height)
		{
			this.kind = PaperKind.Custom;
			this.name = name;
			this.width = width;
			this.height = height;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001DE01 File Offset: 0x0001C001
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001DE09 File Offset: 0x0001C009
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.GetString("PSizeNotCustom"));
				}
				this.height = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001DE32 File Offset: 0x0001C032
		public PaperKind Kind
		{
			get
			{
				if (this.kind <= PaperKind.PrcEnvelopeNumber10Rotated && this.kind != (PaperKind)48 && this.kind != (PaperKind)49)
				{
					return this.kind;
				}
				return PaperKind.Custom;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001DE5A File Offset: 0x0001C05A
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001DE62 File Offset: 0x0001C062
		public string PaperName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.GetString("PSizeNotCustom"));
				}
				this.name = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001DE8B File Offset: 0x0001C08B
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001DE93 File Offset: 0x0001C093
		public int RawKind
		{
			get
			{
				return (int)this.kind;
			}
			set
			{
				this.kind = (PaperKind)value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001DE9C File Offset: 0x0001C09C
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.GetString("PSizeNotCustom"));
				}
				this.width = value;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PaperSize ",
				this.PaperName,
				" Kind=",
				TypeDescriptor.GetConverter(typeof(PaperKind)).ConvertToString((int)this.Kind),
				" Height=",
				this.Height.ToString(CultureInfo.InvariantCulture),
				" Width=",
				this.Width.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x0400069F RID: 1695
		private PaperKind kind;

		// Token: 0x040006A0 RID: 1696
		private string name;

		// Token: 0x040006A1 RID: 1697
		private int width;

		// Token: 0x040006A2 RID: 1698
		private int height;

		// Token: 0x040006A3 RID: 1699
		private bool createdByDefaultConstructor;
	}
}
