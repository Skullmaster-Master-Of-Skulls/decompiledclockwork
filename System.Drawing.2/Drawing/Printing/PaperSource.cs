using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class PaperSource
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0001DF68 File Offset: 0x0001C168
		public PaperSource()
		{
			this.kind = PaperSourceKind.Custom;
			this.name = string.Empty;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001DF86 File Offset: 0x0001C186
		internal PaperSource(PaperSourceKind kind, string name)
		{
			this.kind = kind;
			this.name = name;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001DF9C File Offset: 0x0001C19C
		public PaperSourceKind Kind
		{
			get
			{
				if (this.kind >= (PaperSourceKind)256)
				{
					return PaperSourceKind.Custom;
				}
				return this.kind;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001DFB7 File Offset: 0x0001C1B7
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0001DFBF File Offset: 0x0001C1BF
		public int RawKind
		{
			get
			{
				return (int)this.kind;
			}
			set
			{
				this.kind = (PaperSourceKind)value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public string SourceName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PaperSource ",
				this.SourceName,
				" Kind=",
				TypeDescriptor.GetConverter(typeof(PaperSourceKind)).ConvertToString(this.Kind),
				"]"
			});
		}

		// Token: 0x040006A4 RID: 1700
		private string name;

		// Token: 0x040006A5 RID: 1701
		private PaperSourceKind kind;
	}
}
