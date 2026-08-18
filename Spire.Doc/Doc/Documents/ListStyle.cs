using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x020004A0 RID: 1184
	public class ListStyle : DocumentSerializable, IStyle
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x003D7628 File Offset: 0x003D6628
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x003D766C File Offset: 0x003D666C
		public string Name
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
				return this.ᜆ;
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
				this.ᜆ = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x003D76B0 File Offset: 0x003D66B0
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x003D76F4 File Offset: 0x003D66F4
		public ListType ListType
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x003D7738 File Offset: 0x003D6738
		public ListLevelCollection Levels
		{
			get
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_60;
					case 1:
						if (this.BaseListStyleName.Length > 0)
						{
							goto IL_78;
						}
						goto IL_D0;
					case 2:
						if (this.BaseListStyleName != null)
						{
							num = 3;
							continue;
						}
						goto IL_D0;
					case 3:
						num = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 6:
						this.ᜄ = this.ᜂ(this.BaseListStyleName);
						num = 0;
						continue;
					}
					if (this.ᜄ.Count == 0)
					{
						num = 4;
						continue;
					}
					break;
					IL_78:
					num = 6;
				}
				IL_60:
				IL_D0:
				return this.ᜄ;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x003D781C File Offset: 0x003D681C
		public StyleType StyleType
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
				return StyleType.OtherStyle;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x003D7858 File Offset: 0x003D6858
		// (set) Token: 0x060040DF RID: 16607 RVA: 0x003D789C File Offset: 0x003D689C
		internal bool IsHybrid
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x003D78E0 File Offset: 0x003D68E0
		// (set) Token: 0x060040E1 RID: 16609 RVA: 0x003D7924 File Offset: 0x003D6924
		internal bool IsSimple
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x003D7968 File Offset: 0x003D6968
		// (set) Token: 0x060040E3 RID: 16611 RVA: 0x003D79AC File Offset: 0x003D69AC
		internal bool IsBuiltInStyle
		{
			get
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x003D79F0 File Offset: 0x003D69F0
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x003D7A34 File Offset: 0x003D6A34
		internal string BaseListStyleName
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x003D7A78 File Offset: 0x003D6A78
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x003D7ABC File Offset: 0x003D6ABC
		internal string StyleLink
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
				return this.ᜋ;
			}
			set
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
				this.ᜋ = value;
			}
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x003D7B00 File Offset: 0x003D6B00
		public ListStyle(IDocument doc, ListType listType) : this((Document)doc)
		{
			this.ᜅ = listType;
			this.ᜀ(listType);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x003D7B28 File Offset: 0x003D6B28
		internal ListStyle(Document A_0, ListType A_1, bool A_2) : this(A_0)
		{
			this.ᜅ = A_1;
			this.ᜀ(A_1, A_2);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x003D7B4C File Offset: 0x003D6B4C
		internal ListStyle(Document A_0) : base(A_0, A_0)
		{
			this.ᜄ = new ListLevelCollection(this);
			this.ᜄ.ᜀ(this);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x003D7B7C File Offset: 0x003D6B7C
		public static ListStyle CreateEmptyListStyle(IDocument doc, ListType listType, bool isOneLevelList)
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
			return new ListStyle((Document)doc, listType, isOneLevelList);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x003D7BC8 File Offset: 0x003D6BC8
		public IStyle Clone()
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
			return this.CloneImpl() as IStyle;
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x003D7C10 File Offset: 0x003D6C10
		internal void ᜅ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CA;
				case 2:
					goto IL_A4;
				case 3:
				{
					if (this.ᜄ.Count == 0)
					{
						num = 0;
						continue;
					}
					int count = this.ᜄ.Count;
					int num2 = 0;
					num = 5;
					continue;
				}
				case 4:
					goto IL_BE;
				case 5:
					goto IL_A4;
				case 6:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					ListLevel listLevel = this.ᜄ[num2];
					listLevel.ᜁ();
					num2++;
					num = 2;
					continue;
				}
				case 7:
					num = 3;
					continue;
				}
				IL_30:
				if (this.ᜄ != null)
				{
					num = 7;
					continue;
				}
				goto IL_CA;
				IL_A4:
				num = 6;
				continue;
				IL_CA:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_30;
				default:
					goto IL_E0;
				}
			}
			IL_BE:
			if (true)
			{
			}
			return;
			IL_E0:
			if (false)
			{
			}
			this.ᜄ = null;
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x003D7D14 File Offset: 0x003D6D14
		protected override object CloneImpl()
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
			ListStyle listStyle = (ListStyle)base.CloneImpl();
			listStyle.ᜄ = new ListLevelCollection(listStyle);
			this.ᜄ.CloneToImpl(listStyle.ᜄ);
			return listStyle;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x003D7D7C File Offset: 0x003D6D7C
		protected override void InitXDLSHolder()
		{
			int a_ = 18;
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
			base.XDLSHolder.AddElement(ClipboardData.b("ᑷό੻᭽", a_), this.Levels);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x003D7DE8 File Offset: 0x003D6DE8
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 10;
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
			writer.WriteValue(ClipboardData.b("㹯፱ᥳ፵", a_), this.Name);
			writer.WriteValue(ClipboardData.b("㱯᭱ݳɵⱷ͹౻᭽", a_), this.ListType);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x003D7E6C File Offset: 0x003D6E6C
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 0;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.ReadXmlAttributes(reader);
			this.ᜆ = reader.ReadString(ClipboardData.b("⡥१ݩ५", a_));
			this.ListType = (ListType)reader.ReadEnum(ClipboardData.b("⩥ŧᥩᡫ㩭९ɱᅳ", a_), typeof(ListType));
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x003D7EFC File Offset: 0x003D6EFC
		internal void ᜀ(ListType A_0)
		{
			int a_ = 2;
			for (;;)
			{
				this.Levels.ᜀ();
				base.Document.CreateListLevelImpl(this);
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_154;
						default:
						{
							if (false)
							{
							}
							float num2 = 0.5f;
							num = 3;
							continue;
						}
						}
						break;
					case 2:
						goto IL_228;
					case 3:
						goto IL_228;
					case 4:
					{
						float num2;
						if (num2 >= 4.5f)
						{
							num = 8;
							continue;
						}
						this.Levels.ᜁ(ListLevel.ᜀ((float)((int)(72f * num2)), ClipboardData.b("\udf97", a_), this));
						this.Levels.ᜁ(ListLevel.ᜀ((float)((int)(72.0 * ((double)num2 + 0.5))), ClipboardData.b("ݧ", a_), this));
						this.Levels.ᜁ(ListLevel.ᜀ((float)((int)(72f * (num2 + 1f))), ClipboardData.b("쾗", a_), this));
						num2 += 1.5f;
						num = 2;
						continue;
					}
					case 5:
						goto IL_154;
					case 6:
					{
						float num3;
						if (num3 >= 4.5f)
						{
							num = 0;
							continue;
						}
						if (true)
						{
						}
						int num4;
						this.Levels.ᜁ(ListLevel.ᜀ((int)(72f * num3), num4++, ListPatternType.Arabic, ListNumberAlignment.Left, this));
						this.Levels.ᜁ(ListLevel.ᜀ((int)(72.0 * ((double)num3 + 0.5)), num4++, ListPatternType.LowLetter, ListNumberAlignment.Right, this));
						this.Levels.ᜁ(ListLevel.ᜀ((int)(72f * (num3 + 1f)), num4++, ListPatternType.LowRoman, ListNumberAlignment.Left, this));
						num3 += 1.5f;
						num = 5;
						continue;
					}
					case 7:
						goto IL_154;
					case 8:
						return;
					case 9:
					{
						if (A_0 == ListType.Bulleted)
						{
							num = 1;
							continue;
						}
						int num4 = 0;
						float num3 = 0.5f;
						num = 7;
						continue;
					}
					}
					break;
					IL_154:
					num = 6;
					continue;
					IL_228:
					num = 4;
				}
			}
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x003D8158 File Offset: 0x003D7158
		internal ListLevelCollection ᜂ(string A_0)
		{
			int num = 0;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_A7;
				case 2:
					goto IL_81;
				case 3:
					if (this.m_doc.ListStyles[num2].StyleLink == A_0)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 6;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_100;
					}
					break;
				case 5:
					num2 = 0;
					num = 2;
					continue;
				case 6:
					goto IL_81;
				case 7:
					if (num2 >= this.m_doc.ListStyles.Count)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
				}
				if (this.m_doc.ListStyles.Count > 0)
				{
					num = 5;
					continue;
				}
				break;
				IL_81:
				num = 7;
			}
			IL_A7:
			goto IL_10B;
			IL_100:
			if (false)
			{
			}
			return this.m_doc.ListStyles[num2].Levels;
			IL_10B:
			if (true)
			{
			}
			return null;
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x003D827C File Offset: 0x003D727C
		public ListLevel GetNearLevel(int levelNumber)
		{
			int a_ = 0;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5F;
				case 1:
					if (levelNumber > this.Levels.Count - 1)
					{
						num = 2;
						continue;
					}
					goto IL_C6;
				case 2:
					if (true)
					{
					}
					levelNumber = this.Levels.Count - 1;
					num = 3;
					continue;
				case 3:
					goto IL_78;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (levelNumber < 0)
				{
					num = 0;
					continue;
				}
				IL_7A:
				num = 1;
			}
			IL_5F:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ࡥᵧݩ๫୭ɯ", a_), ClipboardData.b("づ१٩ᥫ୭偯ᅱᕳᡵ塷ᑹ፻੽ꁿꚅﾋﶍ낏ﲓ몙겛", a_));
			IL_78:
			IL_C6:
			return this.Levels[levelNumber];
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x003D835C File Offset: 0x003D735C
		internal void ᜀ(ListType A_0, bool A_1)
		{
			int num = 3;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					goto IL_87;
				case 1:
					num = 2;
					continue;
				case 2:
					num2 = 9;
					goto IL_C0;
				case 4:
					return;
				case 5:
					num2 = 1;
					goto IL_C0;
				case 6:
					goto IL_87;
				case 7:
					if (num3 >= num4)
					{
						num = 4;
						continue;
					}
					this.Levels.ᜁ(base.Document.CreateListLevelImpl(this));
					num3++;
					num = 0;
					continue;
				}
				if (A_1)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C3;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 1;
					continue;
				}
				IL_87:
				num = 7;
				continue;
				IL_C3:
				num = 6;
				continue;
				IL_C0:
				num4 = num2;
				num3 = 0;
				goto IL_C3;
			}
		}

		// Token: 0x0400302A RID: 12330
		private bool \u25D8\u00A2\u00B0\u008B;

		// Token: 0x0400302B RID: 12331
		private string \u2593\u00AF\u0096\u00A6;

		// Token: 0x0400302C RID: 12332
		private new const int ᜀ = 72;

		// Token: 0x0400302D RID: 12333
		internal const string ᜁ = "";

		// Token: 0x0400302E RID: 12334
		internal const string ᜂ = "o";

		// Token: 0x0400302F RID: 12335
		internal const string ᜃ = "";

		// Token: 0x04003030 RID: 12336
		private ListLevelCollection ᜄ;

		// Token: 0x04003031 RID: 12337
		private ListType ᜅ;

		// Token: 0x04003032 RID: 12338
		private byte \u2460\u0097\u0094\u00A5;

		// Token: 0x04003033 RID: 12339
		private string ᜆ;

		// Token: 0x04003034 RID: 12340
		private bool ᜇ;

		// Token: 0x04003035 RID: 12341
		private bool ᜈ;

		// Token: 0x04003036 RID: 12342
		private bool ᜉ;

		// Token: 0x04003037 RID: 12343
		private string ᜊ;

		// Token: 0x04003038 RID: 12344
		private string ᜋ;
	}
}
