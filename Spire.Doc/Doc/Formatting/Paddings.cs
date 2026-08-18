using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x0200046E RID: 1134
	public class Paddings : FormatBase
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x00399108 File Offset: 0x00398108
		// (set) Token: 0x06003E28 RID: 15912 RVA: 0x00399150 File Offset: 0x00398150
		public float Left
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
				return (float)base[1];
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
				base[1] = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x00399198 File Offset: 0x00398198
		// (set) Token: 0x06003E2A RID: 15914 RVA: 0x003991E0 File Offset: 0x003981E0
		public float Top
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
				return (float)base[2];
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
				base[2] = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x00399228 File Offset: 0x00398228
		// (set) Token: 0x06003E2C RID: 15916 RVA: 0x00399270 File Offset: 0x00398270
		public float Right
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
				return (float)base[4];
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
				base[4] = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06003E2D RID: 15917 RVA: 0x003992B8 File Offset: 0x003982B8
		// (set) Token: 0x06003E2E RID: 15918 RVA: 0x00399300 File Offset: 0x00398300
		public float Bottom
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
				return (float)base[3];
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
				base[3] = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (set) Token: 0x06003E2F RID: 15919 RVA: 0x00399348 File Offset: 0x00398348
		public float All
		{
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
				this.Bottom = value;
				this.Top = value;
				this.Right = value;
				this.Left = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x003993A8 File Offset: 0x003983A8
		internal bool IsEmpty
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
				this.ᜀ();
				return this.ᜀ;
			}
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x003993F0 File Offset: 0x003983F0
		internal Paddings(FormatBase A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x0039940C File Offset: 0x0039840C
		internal Paddings()
		{
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x00399428 File Offset: 0x00398428
		private void ᜀ()
		{
			int num = 6;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜀ = true;
					num = 7;
					continue;
				case 1:
					this.ᜀ = true;
					goto IL_73;
				case 2:
					if (this.Top == 0f)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_73;
					default:
						goto IL_DC;
					}
					break;
				case 3:
					goto IL_117;
				case 4:
					this.ᜀ = true;
					num = 5;
					continue;
				case 5:
					if (this.Bottom == 0f)
					{
						num = 3;
						continue;
					}
					goto IL_5C;
				case 7:
					if (this.Right == 0f)
					{
						num = 1;
						continue;
					}
					goto IL_92;
				}
				if (this.Left == 0f)
				{
					num = 0;
					continue;
				}
				goto IL_11C;
				IL_73:
				num = 2;
			}
			IL_5C:
			this.ᜀ = false;
			return;
			IL_92:
			this.ᜀ = false;
			return;
			IL_DC:
			if (false)
			{
			}
			this.ᜀ = false;
			return;
			IL_117:
			this.ᜀ = true;
			return;
			IL_11C:
			this.ᜀ = false;
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00399558 File Offset: 0x00398558
		internal void ᜀ(Paddings A_0)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 2:
					this.Left = A_0.Left;
					this.Right = A_0.Right;
					this.Top = A_0.Top;
					this.Bottom = A_0.Bottom;
					num = 0;
					continue;
				}
				goto IL_1C;
				IL_2C:
				num = 2;
				continue;
				IL_1C:
				if (true)
				{
				}
				if (!A_0.IsDefault)
				{
					goto IL_2C;
				}
				IL_7A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					goto IL_90;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x003995FC File Offset: 0x003985FC
		protected override object GetDefValue(int key)
		{
			int a_ = 1;
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch (key)
						{
						case 1:
							goto IL_A5;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4F;
							default:
								goto IL_67;
							}
							break;
						case 3:
							goto IL_9A;
						case 4:
							goto IL_82;
						default:
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_98;
					case 2:
						goto IL_4F;
					}
					break;
					IL_4F:
					num = 1;
				}
			}
			IL_67:
			if (false)
			{
			}
			return 0f;
			IL_82:
			return 0f;
			IL_98:
			throw new ArgumentException(ClipboardData.b("౦౨ቪ䵬ݮၰr啴Ṷ᝸ൺᱼ፾ꖄ", a_));
			IL_9A:
			return 0f;
			IL_A5:
			return 0f;
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x003996CC File Offset: 0x003986CC
		protected override void InitXDLSHolder()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					base.XDLSHolder.SkipMe = true;
					num = 1;
					continue;
				case 1:
					goto IL_56;
				}
				goto IL_1C;
				IL_2C:
				num = 0;
				continue;
				IL_1C:
				if (true)
				{
				}
				if (base.IsDefault)
				{
					goto IL_2C;
				}
				IL_56:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					goto IL_6C;
				}
			}
			IL_6C:
			if (false)
			{
			}
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x0039974C File Offset: 0x0039874C
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 6;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D5;
					case 1:
						writer.WriteValue(ClipboardData.b("㡫ŭo", a_), this.Top);
						num = 8;
						continue;
					case 2:
						goto IL_8D;
					case 3:
						writer.WriteValue(ClipboardData.b("⹫ŭѯٱ᭳᭵", a_), this.Bottom);
						num = 10;
						continue;
					case 4:
						writer.WriteValue(ClipboardData.b("⁫୭ᙯٱ", a_), this.Left);
						num = 0;
						continue;
					case 5:
						if (base.HasKey(2))
						{
							num = 1;
							continue;
						}
						return;
					case 6:
						writer.WriteValue(ClipboardData.b("㹫ݭᝯᩱs", a_), this.Right);
						num = 2;
						continue;
					case 7:
						if (base.HasKey(4))
						{
							num = 6;
							continue;
						}
						goto IL_8D;
					case 8:
						return;
					case 9:
						if (!base.HasKey(3))
						{
							goto IL_14E;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_88;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 10:
						goto IL_88;
					case 11:
						if (base.HasKey(1))
						{
							num = 4;
							continue;
						}
						goto IL_D5;
					}
					break;
					IL_8D:
					num = 9;
					continue;
					IL_D5:
					num = 7;
					continue;
					IL_14E:
					num = 5;
					continue;
					IL_88:
					goto IL_14E;
				}
			}
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x003998F4 File Offset: 0x003988F4
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 19;
			for (;;)
			{
				if (true)
				{
				}
				base.ReadXmlAttributes(reader);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A8;
					case 1:
						this.Bottom = reader.ReadFloat(ClipboardData.b("㭸ᑺॼ୾", a_));
						num = 10;
						continue;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("⵸ᑺർ", a_)))
						{
							num = 11;
							continue;
						}
						return;
					case 3:
						if (reader.HasAttribute(ClipboardData.b("⭸ቺ᩼᝾", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_A8;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("㕸Ṻ᭼୾", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_F5;
					case 5:
						goto IL_F5;
					case 6:
						this.Right = reader.ReadFloat(ClipboardData.b("⭸ቺ᩼᝾", a_));
						num = 0;
						continue;
					case 7:
						this.Left = reader.ReadFloat(ClipboardData.b("㕸Ṻ᭼୾", a_));
						num = 5;
						continue;
					case 8:
						if (!reader.HasAttribute(ClipboardData.b("㭸ᑺॼ୾", a_)))
						{
							goto IL_184;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A3;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 9:
						return;
					case 10:
						goto IL_A3;
					case 11:
						this.Top = reader.ReadFloat(ClipboardData.b("⵸ᑺർ", a_));
						num = 9;
						continue;
					}
					break;
					IL_A8:
					num = 8;
					continue;
					IL_F5:
					num = 3;
					continue;
					IL_184:
					num = 2;
					continue;
					IL_A3:
					goto IL_184;
				}
			}
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x00399AE0 File Offset: 0x00398AE0
		protected override void OnChange(FormatBase format, int propertyKey)
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
			base.OnChange(format, propertyKey);
		}

		// Token: 0x04002D72 RID: 11634
		private bool \u2609\u0087\u009A\u0098;

		// Token: 0x04002D73 RID: 11635
		public const int LeftKey = 1;

		// Token: 0x04002D74 RID: 11636
		public const int TopKey = 2;

		// Token: 0x04002D75 RID: 11637
		private int \u25D8\u00A2\u00B0\u007F;

		// Token: 0x04002D76 RID: 11638
		public const int BottomKey = 3;

		// Token: 0x04002D77 RID: 11639
		public const int RightKey = 4;

		// Token: 0x04002D78 RID: 11640
		private bool \u2609\u009D\u00AB\u008A;

		// Token: 0x04002D79 RID: 11641
		private int[] \u2593\u00A8\u00AE\u00A8;

		// Token: 0x04002D7A RID: 11642
		private new bool ᜀ = true;
	}
}
