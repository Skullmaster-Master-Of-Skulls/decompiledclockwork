using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000E5 RID: 229
	public class Tab : DocumentSerializable
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0002857C File Offset: 0x0002757C
		// (set) Token: 0x0600037D RID: 893 RVA: 0x000285C0 File Offset: 0x000275C0
		public TabJustification Justification
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
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜀ = value;
							num = 1;
							continue;
						case 1:
							goto IL_6C;
						}
						if (value == this.ᜀ)
						{
							goto IL_6E;
						}
						num = 0;
						break;
					}
				}
				IL_6C:
				IL_6E:
				this.ᜀ();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00028644 File Offset: 0x00027644
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00028688 File Offset: 0x00027688
		public TabLeader TabLeader
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
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜁ = value;
							if (true)
							{
							}
							num = 1;
							continue;
						case 1:
							goto IL_6C;
						}
						if (value == this.ᜁ)
						{
							goto IL_6E;
						}
						num = 0;
						break;
					}
				}
				IL_6C:
				IL_6E:
				this.ᜀ();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0002870C File Offset: 0x0002770C
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00028750 File Offset: 0x00027750
		public float Position
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
				return this.ᜂ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_6C;
						case 2:
							this.ᜂ = value;
							num = 0;
							continue;
						}
						if (value == this.ᜂ)
						{
							goto IL_6E;
						}
						num = 2;
						break;
					}
				}
				IL_6C:
				IL_6E:
				this.ᜀ();
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000287D4 File Offset: 0x000277D4
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00028818 File Offset: 0x00027818
		public float DeletePosition
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
				return this.ᜃ;
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
				this.ᜃ = value;
				this.ᜀ();
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00028860 File Offset: 0x00027860
		internal Tab(IDocument A_0) : base((Document)A_0, null)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0002887C File Offset: 0x0002787C
		internal Tab(IDocument A_0, float A_1, TabJustification A_2, TabLeader A_3) : this(A_0, A_1, 0f, A_2, A_3)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0002889C File Offset: 0x0002789C
		internal Tab(IDocument A_0, float A_1, float A_2, TabJustification A_3, TabLeader A_4) : this(A_0)
		{
			this.ᜂ = A_1;
			this.ᜀ = A_3;
			this.ᜁ = A_4;
			this.ᜃ = A_2;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000288D0 File Offset: 0x000278D0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			writer.WriteValue(ClipboardData.b("㝦٨ᡪѬ᭮ᡰᱲ᭴", a_), this.Position);
			writer.WriteValue(ClipboardData.b("ⵦᱨᡪᥬٮᝰᩲᙴᙶ൸ቺቼᅾ", a_), this.Justification);
			writer.WriteValue(ClipboardData.b("⭦౨੪६੮Ͱ", a_), this.TabLeader);
			writer.WriteValue(ClipboardData.b("⍦౨ݪ࡬᭮ᑰ", a_), this.DeletePosition);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00028988 File Offset: 0x00027988
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 1;
			int num = 6;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_1F1;
					case 1:
						goto IL_184;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("⭦౨੪६੮Ͱ", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_184;
					case 3:
						this.ᜀ = (TabJustification)reader.ReadEnum(ClipboardData.b("ⵦᱨᡪᥬٮᝰᩲᙴᙶ൸ቺቼᅾ", a_), typeof(TabJustification));
						num = 0;
						continue;
					case 4:
						if (true)
						{
						}
						if (reader.HasAttribute(ClipboardData.b("⍦౨ݪ࡬᭮ᑰ", a_)))
						{
							num = 5;
							continue;
						}
						return;
					case 5:
						this.ᜃ = reader.ReadFloat(ClipboardData.b("⍦౨ݪ࡬᭮ᑰ", a_));
						num = 9;
						continue;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("ⵦᱨᡪᥬٮᝰᩲᙴᙶ൸ቺቼᅾ", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_C4;
					case 8:
						goto IL_FF;
					case 9:
						return;
					case 10:
						this.ᜁ = (TabLeader)reader.ReadEnum(ClipboardData.b("⭦౨੪६੮Ͱ", a_), typeof(TabLeader));
						num = 1;
						continue;
					case 11:
						this.ᜂ = reader.ReadFloat(ClipboardData.b("㝦٨ᡪѬ᭮ᡰᱲ᭴", a_));
						num = 8;
						continue;
					}
					if (reader.HasAttribute(ClipboardData.b("㝦٨ᡪѬ᭮ᡰᱲ᭴", a_)))
					{
						num = 11;
						continue;
					}
					IL_FF:
					num = 7;
					continue;
					IL_184:
					num = 4;
					continue;
				}
				IL_C4:
				num = 2;
				continue;
				IL_1F1:
				goto IL_C4;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00028B8C File Offset: 0x00027B8C
		internal Tab ᜁ()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return (Tab)this.CloneImpl();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00028BD4 File Offset: 0x00027BD4
		private void ᜀ()
		{
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						(base.OwnerBase as TabCollection).ᜁ();
						num = 2;
						continue;
					case 2:
						return;
					}
					if (base.OwnerBase == null)
					{
						return;
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x04000CCE RID: 3278
		private int \u25D9\u0089\u0095\u00AB;

		// Token: 0x04000CCF RID: 3279
		private float \u25D9\u00A7\u0094\u0090;

		// Token: 0x04000CD0 RID: 3280
		private long \u25D8\u0081\u00AC\u00B0;

		// Token: 0x04000CD1 RID: 3281
		private new TabJustification ᜀ;

		// Token: 0x04000CD2 RID: 3282
		private string \u2460\u009D\u0097\u009A;

		// Token: 0x04000CD3 RID: 3283
		private TabLeader ᜁ;

		// Token: 0x04000CD4 RID: 3284
		private byte \u2593\u00AB\u0085\u00A9;

		// Token: 0x04000CD5 RID: 3285
		private float ᜂ;

		// Token: 0x04000CD6 RID: 3286
		private float ᜃ;
	}
}
