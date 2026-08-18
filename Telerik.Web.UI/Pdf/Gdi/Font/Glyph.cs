using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200161D RID: 5661
	internal class Glyph
	{
		// Token: 0x0600DC48 RID: 56392 RVA: 0x003025AC File Offset: 0x003007AC
		public Glyph(int glyphIndex)
		{
			this.glyphIndex = glyphIndex;
			this.children = new ArrayList();
		}

		// Token: 0x0600DC49 RID: 56393 RVA: 0x003025C6 File Offset: 0x003007C6
		public void SetGlyphData(byte[] glyphData)
		{
			this.glyphData = glyphData;
		}

		// Token: 0x17004367 RID: 17255
		// (get) Token: 0x0600DC4A RID: 56394 RVA: 0x003025CF File Offset: 0x003007CF
		public int Index
		{
			get
			{
				return this.glyphIndex;
			}
		}

		// Token: 0x17004368 RID: 17256
		// (get) Token: 0x0600DC4B RID: 56395 RVA: 0x003025D7 File Offset: 0x003007D7
		public int Length
		{
			get
			{
				if (this.glyphData == null)
				{
					return 0;
				}
				return this.glyphData.Length;
			}
		}

		// Token: 0x0600DC4C RID: 56396 RVA: 0x003025EB File Offset: 0x003007EB
		public void AddChild(int glyphIndex)
		{
			this.children.Add(glyphIndex);
		}

		// Token: 0x17004369 RID: 17257
		// (get) Token: 0x0600DC4D RID: 56397 RVA: 0x003025FF File Offset: 0x003007FF
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x1700436A RID: 17258
		// (get) Token: 0x0600DC4E RID: 56398 RVA: 0x00302607 File Offset: 0x00300807
		public bool IsComposite
		{
			get
			{
				return this.children.Count != 0;
			}
		}

		// Token: 0x0600DC4F RID: 56399 RVA: 0x0030261A File Offset: 0x0030081A
		public void Write(FontFileStream stream)
		{
			if (this.glyphData != null && this.glyphData.Length > 0)
			{
				stream.Write(this.glyphData, 0, this.glyphData.Length);
			}
		}

		// Token: 0x04003DBA RID: 15802
		private int glyphIndex;

		// Token: 0x04003DBB RID: 15803
		private byte[] glyphData;

		// Token: 0x04003DBC RID: 15804
		private IList children;
	}
}
