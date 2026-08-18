using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x02000492 RID: 1170
	public class DropDownItem : DocumentSerializable
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x003B04A4 File Offset: 0x003AF4A4
		// (set) Token: 0x06004006 RID: 16390 RVA: 0x003B04E8 File Offset: 0x003AF4E8
		public string Text
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x003B052C File Offset: 0x003AF52C
		public DropDownItem(IDocument doc) : base((Document)doc, null)
		{
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x003B0554 File Offset: 0x003AF554
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ᩲŴቶᑸ⽺᡼ݾ", a_), this.ᜀ);
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x003B05BC File Offset: 0x003AF5BC
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 9;
			for (;;)
			{
				IL_1D:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_52:
					if (!reader.HasAttribute(ClipboardData.b("ٮհᙲᡴ⍶ᱸͺॼ", a_)))
					{
						return;
					}
					num = 1;
					break;
				case 1:
					goto IL_3D;
				default:
					goto IL_3D;
				}
				for (;;)
				{
					IL_0B:
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜀ = reader.ReadString(ClipboardData.b("ٮհᙲᡴ⍶ᱸͺॼ", a_));
						if (true)
						{
						}
						num = 0;
						continue;
					case 2:
						goto IL_52;
					}
					goto IL_1D;
				}
				IL_3D:
				if (false)
				{
				}
				base.ReadXmlAttributes(reader);
				num = 2;
				goto IL_0B;
			}
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x003B066C File Offset: 0x003AF66C
		internal DropDownItem ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (DropDownItem)this.CloneImpl();
		}

		// Token: 0x04002F87 RID: 12167
		private string \u2460\u0093\u00A1\u0084;

		// Token: 0x04002F88 RID: 12168
		private float[] \u2460\u008B\u007F\u0096;

		// Token: 0x04002F89 RID: 12169
		private string \u25D8\u00AF\u00A9\u0082;

		// Token: 0x04002F8A RID: 12170
		private bool \u2609\u0088\u00A1\u008F;

		// Token: 0x04002F8B RID: 12171
		private int \u2593\u009E\u009F\u00A7;

		// Token: 0x04002F8C RID: 12172
		private int \u2460\u008C\u008C\u0085;

		// Token: 0x04002F8D RID: 12173
		private new string ᜀ = "";
	}
}
