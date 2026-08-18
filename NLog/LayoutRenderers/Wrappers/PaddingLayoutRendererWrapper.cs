using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000102 RID: 258
	[AmbientProperty("Padding")]
	[AmbientProperty("AlignmentOnTruncation")]
	[LayoutRenderer("pad")]
	[ThreadAgnostic]
	[AmbientProperty("PadCharacter")]
	[AmbientProperty("FixedLength")]
	public sealed class PaddingLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00010019 File Offset: 0x0000E219
		public PaddingLayoutRendererWrapper()
		{
			this.PadCharacter = ' ';
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00010029 File Offset: 0x0000E229
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00010031 File Offset: 0x0000E231
		public int Padding { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001003A File Offset: 0x0000E23A
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00010042 File Offset: 0x0000E242
		[DefaultValue(' ')]
		public char PadCharacter { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001004B File Offset: 0x0000E24B
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00010053 File Offset: 0x0000E253
		[DefaultValue(false)]
		public bool FixedLength { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001005C File Offset: 0x0000E25C
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00010064 File Offset: 0x0000E264
		[DefaultValue(PaddingHorizontalAlignment.Left)]
		public PaddingHorizontalAlignment AlignmentOnTruncation { get; set; }

		// Token: 0x0600073A RID: 1850 RVA: 0x00010070 File Offset: 0x0000E270
		protected override string Transform(string text)
		{
			string text2 = text ?? string.Empty;
			if (this.Padding != 0)
			{
				if (this.Padding > 0)
				{
					text2 = text2.PadLeft(this.Padding, this.PadCharacter);
				}
				else
				{
					text2 = text2.PadRight(-this.Padding, this.PadCharacter);
				}
				int num = this.Padding;
				if (num < 0)
				{
					num = -num;
				}
				if (this.FixedLength && text2.Length > num)
				{
					if (this.AlignmentOnTruncation == PaddingHorizontalAlignment.Right)
					{
						text2 = text2.Substring(text2.Length - num);
					}
					else
					{
						text2 = text2.Substring(0, num);
					}
				}
			}
			return text2;
		}
	}
}
