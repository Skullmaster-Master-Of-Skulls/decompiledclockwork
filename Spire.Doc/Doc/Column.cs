using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000EE RID: 238
	public class Column : DocumentSerializable
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0002D724 File Offset: 0x0002C724
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0002D768 File Offset: 0x0002C768
		public float Width
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

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0002D7AC File Offset: 0x0002C7AC
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0002D7F0 File Offset: 0x0002C7F0
		public float Space
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0002D834 File Offset: 0x0002C834
		public Column(IDocument doc) : base((Document)doc, null)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0002D850 File Offset: 0x0002C850
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ㅥŧ๩ᡫ٭", a_), this.Width);
			writer.WriteValue(ClipboardData.b("㕥ᡧ୩ཫݭṯᕱ", a_), this.Space);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0002D8D0 File Offset: 0x0002C8D0
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.ReadXmlAttributes(reader);
			this.Width = reader.ReadFloat(ClipboardData.b("㥭᥯ᙱsṵ", a_));
			this.Space = reader.ReadFloat(ClipboardData.b("㵭o፱ᝳήᙷᵹ", a_));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0002D950 File Offset: 0x0002C950
		internal Column ᜀ()
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
			return (Column)base.CloneImpl();
		}

		// Token: 0x04000D01 RID: 3329
		private new float ᜀ;

		// Token: 0x04000D02 RID: 3330
		private float[] \u25D9\u0097\u0099\u008F;

		// Token: 0x04000D03 RID: 3331
		private long \u25D8\u0081\u00A1\u00A9;

		// Token: 0x04000D04 RID: 3332
		private string[] \u2593\u00A0\u0088\u0082;

		// Token: 0x04000D05 RID: 3333
		private float ᜁ;
	}
}
