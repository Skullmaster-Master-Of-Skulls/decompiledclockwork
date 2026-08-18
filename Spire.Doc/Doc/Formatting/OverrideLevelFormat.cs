using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000478 RID: 1144
	public class OverrideLevelFormat : DocumentSerializable
	{
		// Token: 0x06003FEC RID: 16364 RVA: 0x003AF920 File Offset: 0x003AE920
		internal OverrideLevelFormat(Document A_0) : base(A_0, null)
		{
			this.ᜃ = new ListLevel(base.Document);
			this.ᜃ.ᜀ(this);
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x003AF954 File Offset: 0x003AE954
		// (set) Token: 0x06003FEE RID: 16366 RVA: 0x003AF998 File Offset: 0x003AE998
		internal bool OverrideStartAtValue
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x003AF9DC File Offset: 0x003AE9DC
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x003AFA20 File Offset: 0x003AEA20
		internal bool OverrideFormatting
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
				return this.ᜂ;
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
				this.ᜂ = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x003AFA64 File Offset: 0x003AEA64
		// (set) Token: 0x06003FF2 RID: 16370 RVA: 0x003AFAA8 File Offset: 0x003AEAA8
		internal int StartAt
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x003AFAEC File Offset: 0x003AEAEC
		// (set) Token: 0x06003FF4 RID: 16372 RVA: 0x003AFB30 File Offset: 0x003AEB30
		internal ListLevel OverrideListLevel
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
			}
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x003AFB74 File Offset: 0x003AEB74
		protected override void InitXDLSHolder()
		{
			int a_ = 14;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("ᡳ፵๷όၻ卽慎", a_), this.ᜃ);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x003AFBE0 File Offset: 0x003AEBE0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 14;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1C7;
					case 1:
						if (this.ᜄ != 0)
						{
							num = 4;
							continue;
						}
						goto IL_16B;
					case 2:
						goto IL_C2;
					case 3:
						if (this.ᜂ)
						{
							num = 7;
							continue;
						}
						goto IL_1C7;
					case 4:
						writer.WriteValue(ClipboardData.b("♳፵୷ό๻ࡽ떃", a_), this.ᜄ);
						num = 10;
						continue;
					case 5:
						if (this.ᜅ != 0)
						{
							goto IL_17E;
						}
						goto IL_104;
					case 6:
						writer.WriteValue(ClipboardData.b("㝳ṵ᥷ᑹ᭻᭽퍿ﲇ쮉", a_), this.ᜁ);
						writer.WriteValue(ClipboardData.b("❳ɵ᥷ࡹࡻ㽽", a_), this.ᜀ);
						num = 2;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17E;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("㝳ṵ᥷ᑹ᭻᭽왿ﺉ", a_), this.ᜂ);
							num = 0;
							continue;
						}
						break;
					case 8:
						if (this.ᜁ)
						{
							num = 6;
							continue;
						}
						goto IL_C2;
					case 9:
						writer.WriteValue(ClipboardData.b("♳፵୷ό๻ࡽ랃", a_), this.ᜆ);
						num = 12;
						continue;
					case 10:
						goto IL_16B;
					case 11:
						writer.WriteValue(ClipboardData.b("♳፵୷ό๻ࡽ뚃", a_), this.ᜅ);
						num = 13;
						continue;
					case 12:
						goto IL_1B3;
					case 13:
						goto IL_104;
					case 14:
						if (this.ᜆ != 0)
						{
							num = 9;
							continue;
						}
						return;
					}
					break;
					IL_C2:
					num = 3;
					continue;
					IL_104:
					num = 14;
					continue;
					IL_16B:
					num = 5;
					continue;
					IL_17E:
					num = 11;
					continue;
					IL_1C7:
					num = 1;
				}
			}
			IL_1B3:
			if (true)
			{
			}
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x003AFE00 File Offset: 0x003AEE00
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 17;
			for (;;)
			{
				if (true)
				{
				}
				base.ReadXmlAttributes(reader);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F8;
					case 1:
						this.ᜁ = reader.ReadBoolean(ClipboardData.b("㑶ᅸ᩺፼᡾킂ﮈﾊ첌ﮎ", a_));
						num = 4;
						continue;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("㑶ᅸ᩺፼᡾얂歷", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_F8;
					case 3:
						if (reader.HasAttribute(ClipboardData.b("╶ᱸࡺ᡼ൾ떆", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_12C;
					case 4:
						goto IL_260;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("⑶൸ོ᩺୾삀", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_C7;
					case 6:
						goto IL_12C;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("㑶ᅸ᩺፼᡾킂ﮈﾊ첌ﮎ", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_260;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_187;
						default:
							if (false)
							{
							}
							this.ᜀ = reader.ReadInt(ClipboardData.b("⑶൸ོ᩺୾삀", a_));
							num = 12;
							continue;
						}
						break;
					case 9:
						this.ᜂ = reader.ReadBoolean(ClipboardData.b("㑶ᅸ᩺፼᡾얂歷", a_));
						num = 0;
						continue;
					case 10:
						goto IL_187;
					case 11:
						this.ᜄ = reader.ReadInt(ClipboardData.b("╶ᱸࡺ᡼ൾ뚆", a_));
						num = 17;
						continue;
					case 12:
						goto IL_C7;
					case 13:
						if (reader.HasAttribute(ClipboardData.b("╶ᱸࡺ᡼ൾ뒆", a_)))
						{
							num = 15;
							continue;
						}
						return;
					case 14:
						if (reader.HasAttribute(ClipboardData.b("╶ᱸࡺ᡼ൾ뚆", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_96;
					case 15:
						this.ᜆ = reader.ReadInt(ClipboardData.b("╶ᱸࡺ᡼ൾ뒆", a_));
						num = 16;
						continue;
					case 16:
						return;
					case 17:
						goto IL_96;
					}
					break;
					IL_96:
					num = 3;
					continue;
					IL_C7:
					num = 14;
					continue;
					IL_F8:
					num = 7;
					continue;
					IL_12C:
					num = 13;
					continue;
					IL_187:
					this.ᜅ = reader.ReadInt(ClipboardData.b("╶ᱸࡺ᡼ൾ떆", a_));
					num = 6;
					continue;
					IL_260:
					num = 5;
				}
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x003B00C0 File Offset: 0x003AF0C0
		protected override object CloneImpl()
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
			OverrideLevelFormat overrideLevelFormat = (OverrideLevelFormat)base.CloneImpl();
			overrideLevelFormat.OverrideListLevel = this.OverrideListLevel.Clone();
			return overrideLevelFormat;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x003B011C File Offset: 0x003AF11C
		internal void ᜁ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						this.ᜃ.ᜁ();
						this.ᜃ = null;
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				goto IL_26;
				IL_36:
				num = 1;
				continue;
				IL_26:
				if (true)
				{
				}
				if (this.ᜃ != null)
				{
					goto IL_36;
				}
				break;
			}
		}

		// Token: 0x04002E4A RID: 11850
		private new int ᜀ;

		// Token: 0x04002E4B RID: 11851
		private byte[] \u2460\u0092\u0085\u00A0;

		// Token: 0x04002E4C RID: 11852
		private bool ᜁ;

		// Token: 0x04002E4D RID: 11853
		private float[] \u25D8\u0096\u009D\u00A0;

		// Token: 0x04002E4E RID: 11854
		private byte[] \u2609\u008C\u008A\u009D;

		// Token: 0x04002E4F RID: 11855
		private bool ᜂ;

		// Token: 0x04002E50 RID: 11856
		private bool \u25D9\u0099ª\u00A8;

		// Token: 0x04002E51 RID: 11857
		private float[] \u25D8\u00A1\u008A\u0081;

		// Token: 0x04002E52 RID: 11858
		private ListLevel ᜃ;

		// Token: 0x04002E53 RID: 11859
		internal int ᜄ;

		// Token: 0x04002E54 RID: 11860
		internal int ᜅ;

		// Token: 0x04002E55 RID: 11861
		private string \u25D9\u0092\u0081\u008E;

		// Token: 0x04002E56 RID: 11862
		internal int ᜆ;
	}
}
