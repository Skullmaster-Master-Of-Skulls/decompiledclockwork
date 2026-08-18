using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class PrinterResolution
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0001EA02 File Offset: 0x0001CC02
		public PrinterResolution()
		{
			this.kind = PrinterResolutionKind.Custom;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001EA11 File Offset: 0x0001CC11
		internal PrinterResolution(PrinterResolutionKind kind, int x, int y)
		{
			this.kind = kind;
			this.x = x;
			this.y = y;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001EA2E File Offset: 0x0001CC2E
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001EA36 File Offset: 0x0001CC36
		public PrinterResolutionKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -4, 0))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PrinterResolutionKind));
				}
				this.kind = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001EA66 File Offset: 0x0001CC66
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001EA6E File Offset: 0x0001CC6E
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001EA77 File Offset: 0x0001CC77
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001EA7F File Offset: 0x0001CC7F
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public override string ToString()
		{
			if (this.kind != PrinterResolutionKind.Custom)
			{
				return "[PrinterResolution " + TypeDescriptor.GetConverter(typeof(PrinterResolutionKind)).ConvertToString((int)this.Kind) + "]";
			}
			return string.Concat(new string[]
			{
				"[PrinterResolution X=",
				this.X.ToString(CultureInfo.InvariantCulture),
				" Y=",
				this.Y.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x040006CA RID: 1738
		private int x;

		// Token: 0x040006CB RID: 1739
		private int y;

		// Token: 0x040006CC RID: 1740
		private PrinterResolutionKind kind;
	}
}
