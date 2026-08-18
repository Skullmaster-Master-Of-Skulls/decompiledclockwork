using System;
using System.Drawing;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Parser.Biff_Records.Charts;

namespace Spire.Xls
{
	// Token: 0x0200005F RID: 95
	public class ChartShadow : XlsObject, IShadow, ICloneParent
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0005CBF0 File Offset: 0x0005BBF0
		internal ChartShadow(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜂ();
			this.ᜀ();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0005CC1C File Offset: 0x0005BC1C
		private void ᜂ()
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
			this.ᜉ = new OColor(spr\u1D39.ᜂ);
			this.ᜉ.AfterChange += this.ᜁ;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0005CC80 File Offset: 0x0005BC80
		private void ᜁ()
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
			ExcelColors excelColors = this.ᜉ.ᜂ(this.ᜁ);
			this.ShadowFormat.ᜁ((ushort)excelColors);
			this.ShadowFormat.ᜀ(excelColors == ExcelColors.Black);
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0005CCF0 File Offset: 0x0005BCF0
		[CLSCompliant(false)]
		internal sprᣐ ShadowFormat
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_57;
					case 1:
						this.ᜆ = (sprᣐ)spr\u175E.ᜀ(TBIFFRecord.ChartMarkerFormat);
						this.ᜆ.ᜁ(true);
						num = 0;
						continue;
					}
					if (this.ᜆ != null)
					{
						goto IL_87;
					}
					num = 1;
				}
				IL_57:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				IL_87:
				return this.ᜆ;
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0005CD8C File Offset: 0x0005BD8C
		private void ᜀ()
		{
			int a_ = 1;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜁ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
				if (this.ᜁ != null)
				{
					return;
				}
				break;
			}
			if (true)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("吶堸唺匼倾㕀捂⍄⹆❈⽊浌㽎ぐ⅒ご㥖ⵘ筚㉜㵞ୠ٢٤፦ᩨ䕪", a_));
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0005CE0C File Offset: 0x0005BE0C
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0005CE54 File Offset: 0x0005BE54
		public XLSXChartShadowOuterType ShadowOuterType
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
				return this.ᜀ.ShadowOuterType;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_4B;
					case 2:
						this.ᜀ.ShadowOuterType = value;
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (value == this.ShadowOuterType)
					{
						return;
					}
					num = 2;
				}
				IL_4B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0005CED4 File Offset: 0x0005BED4
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0005CF1C File Offset: 0x0005BF1C
		public XLSXChartShadowInnerType ShadowInnerType
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
				return this.ᜀ.ShadowInnerType;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.ShadowInnerType = value;
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_4B;
					}
					if (value == this.ShadowInnerType)
					{
						return;
					}
					num = 0;
				}
				IL_4B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0005CF9C File Offset: 0x0005BF9C
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0005CFE0 File Offset: 0x0005BFE0
		public bool HasCustomStyle
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

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0005D024 File Offset: 0x0005C024
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0005D06C File Offset: 0x0005C06C
		public XLSXChartPrespectiveType ShadowPrespectiveType
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
				return this.ᜀ.ShadowPrespectiveType;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.ShadowPrespectiveType = value;
						num = 2;
						continue;
					case 2:
						goto IL_4B;
					}
					if (true)
					{
					}
					if (value == this.ShadowPrespectiveType)
					{
						return;
					}
					num = 0;
				}
				IL_4B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0005D0EC File Offset: 0x0005C0EC
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x0005D130 File Offset: 0x0005C130
		public int Transparency
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
				int a_ = 15;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value > 100)
						{
							num = 2;
							continue;
						}
						goto IL_90;
					case 2:
						goto IL_3F;
					case 3:
						num = 1;
						continue;
					}
					if (value >= 0)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					IL_3F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_55;
					}
				}
				IL_55:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᅄ⽆ⱈ歊㭌⹎㵐♒ご睖⩘㍚㉜⩞ൠݢ䕤զ౨䭪ཬ੮հѲၴቶ᝸孺䵼彾검ꎂ뒄랆릈ꖊ", a_));
				IL_90:
				this.ᜃ = (100 - value) * 1000;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0005D1E0 File Offset: 0x0005C1E0
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0005D224 File Offset: 0x0005C224
		public int Size
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
				return this.ᜄ;
			}
			set
			{
				int a_ = 0;
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3F;
					case 1:
						if (value > 200)
						{
							num = 0;
							continue;
						}
						goto IL_93;
					case 3:
						num = 1;
						continue;
					}
					if (value > 0)
					{
						num = 3;
						continue;
					}
					IL_3F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_55;
					}
				}
				IL_55:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("戵倷弹᰻䠽ℿ⹁ㅃ⍅桇㥉⑋⅍╏㹑こ癕㩗㽙籛㱝՟ᙡ፣ͥ൧ѩ䱫幭偯影味䑵䡷䩹剻", a_));
				IL_93:
				this.ᜄ = value * 1000;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0005D2D4 File Offset: 0x0005C2D4
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0005D318 File Offset: 0x0005C318
		public int Blur
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
				int a_ = 16;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_3F;
					case 2:
						if (value > 100)
						{
							num = 1;
							continue;
						}
						goto IL_90;
					case 3:
						num = 2;
						continue;
					}
					if (value >= 0)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					IL_3F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_55;
					}
				}
				IL_55:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ቅ⁇⽉汋ᡍㅏ㹑⅓㍕硗㕙㩛繝ᑟ੡ţ䙥੧٩ᥫᱭ偯űᱳ᥵൷ᙹ᡻幽ꒃﺉﮋﲑ뒓ꚕ떗ꮙ겛꺝躟", a_));
				IL_90:
				this.ᜅ = value * 12700;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0005D3C4 File Offset: 0x0005C3C4
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0005D408 File Offset: 0x0005C408
		public int Angle
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
				return this.ᜇ;
			}
			set
			{
				int a_ = 10;
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (value > 359)
						{
							num = 2;
							continue;
						}
						goto IL_93;
					case 2:
						goto IL_3F;
					case 3:
						num = 0;
						continue;
					}
					if (value >= 0)
					{
						num = 3;
						continue;
					}
					IL_3F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_55;
					}
				}
				IL_55:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᐿ⩁⅃晅㹇⭉⁋㭍㕏牑❓㹕㝗⽙せ㩝䁟aţ䙥੧ཀྵᡫᥭᕯ᝱ᩳ噵䡷坹佻䭽륿겁", a_));
				IL_93:
				this.ᜇ = value * 60000;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0005D4B8 File Offset: 0x0005C4B8
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0005D504 File Offset: 0x0005C504
		public Color Color
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
				return this.ᜉ.ᜁ(this.ᜁ);
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
				this.ᜉ.ᜀ(value);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0005D54C File Offset: 0x0005C54C
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0005D590 File Offset: 0x0005C590
		public int Distance
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
				return this.ᜈ;
			}
			set
			{
				int a_ = 12;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_37;
					case 2:
						num = 3;
						continue;
					case 3:
						if (true)
						{
						}
						if (value > 200)
						{
							num = 0;
							continue;
						}
						goto IL_93;
					}
					if (value >= 0)
					{
						num = 2;
						continue;
					}
					IL_37:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_4D;
					}
				}
				IL_4D:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇㱉ⵋ≍╏㝑瑓╕し㕙⥛㉝џ䉡٣ͥ䡧ࡩ५ᩭݯ᝱ᅳᡵ塷䩹养䱽끿늁ꪃ", a_));
				IL_93:
				this.ᜈ = value * 12700;
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0005D640 File Offset: 0x0005C640
		object ICloneParent.Clone(object parent)
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
			return this.Clone(parent);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0005D684 File Offset: 0x0005C684
		public ChartShadow Clone(object parent)
		{
			int a_ = 7;
			if (parent == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䴼帾㍀♂⭄㍆", a_));
				}
			}
			if (true)
			{
			}
			ChartShadow chartShadow = (ChartShadow)base.MemberwiseClone();
			chartShadow.ᜀ = (ShadowOptions)spr\u1CD3.ᜀ(this.ᜀ);
			chartShadow.SetParent(parent);
			chartShadow.ᜀ();
			return chartShadow;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0005D714 File Offset: 0x0005C714
		public void CustomShadowStyles(XLSXChartShadowOuterType iOuter, int iTransparency, int iSize, int iBlur, int iAngle, int iDistance, bool CustomShadowStyle)
		{
			int a_ = 4;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_2B6;
				case 1:
					if (iBlur >= 0)
					{
						num = 12;
						continue;
					}
					goto IL_138;
				case 2:
					if (iOuter == XLSXChartShadowOuterType.None)
					{
						num = 7;
						continue;
					}
					num = 10;
					continue;
				case 3:
					if (iDistance >= 0)
					{
						num = 11;
						continue;
					}
					goto IL_1C5;
				case 4:
					goto IL_16E;
				case 5:
					num = 9;
					continue;
				case 7:
					goto IL_133;
				case 8:
					goto IL_88;
				case 9:
					if (iTransparency > 100)
					{
						num = 18;
						continue;
					}
					num = 1;
					continue;
				case 10:
					if (iSize > 0)
					{
						if (true)
						{
						}
						num = 19;
						continue;
					}
					goto IL_239;
				case 11:
					num = 16;
					continue;
				case 12:
					num = 23;
					continue;
				case 13:
					goto IL_1AC;
				case 14:
					if (iAngle > 359)
					{
						num = 17;
						continue;
					}
					num = 3;
					continue;
				case 15:
					if (iTransparency >= 0)
					{
						num = 5;
						continue;
					}
					goto IL_170;
				case 16:
					if (iDistance > 200)
					{
						num = 4;
						continue;
					}
					goto IL_2BB;
				case 17:
					goto IL_292;
				case 18:
					goto IL_26B;
				case 19:
					num = 22;
					continue;
				case 20:
					if (iAngle >= 0)
					{
						num = 21;
						continue;
					}
					goto IL_1B1;
				case 21:
					num = 14;
					continue;
				case 22:
					if (iSize > 200)
					{
						num = 13;
						continue;
					}
					num = 15;
					continue;
				case 23:
					if (iBlur > 100)
					{
						num = 0;
						continue;
					}
					num = 20;
					continue;
				}
				if (!CustomShadowStyle)
				{
					num = 8;
				}
				else
				{
					num = 2;
				}
			}
			IL_88:
			throw new NotSupportedException(RecordTableEnumerator.b("猹䠻ḽ㌿⩁⭃㍅⑇⹉汋ⱍ㕏牑❓㍕ⱗ穙⡛ⱝᕟݡ䑣ብݧ䩩իͭoṱᅳ᭵ᵷᑹࡻ幽ꚅﾉﾋ揄ﾏﾑ뒓ﮙ힟芡힣튥톧용즫", a_));
			IL_133:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_88;
			default:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㑏㵑ㅓ╕硗㑙㍛⩝䁟͡ݣե൧ᩩᡫ乭㹯ᵱݳṵ᥷ṹ፻ॽ", a_));
			}
			IL_138:
			throw new NotSupportedException(RecordTableEnumerator.b("渹吻嬽怿ᑁ╃⩅㵇⽉汋⅍㙏牑⁓㹕㵗穙㹛㉝ᕟၡ䑣ᕥgթᥫɭᑯ剱ᙳ፵塷᡹᥻੽ꂇ몉ꆋ뾍ꂏꊑ붓", a_));
			IL_16E:
			goto IL_1C5;
			IL_170:
			throw new NotSupportedException(RecordTableEnumerator.b("渹吻嬽怿ᑁ╃⩅㵇⽉汋⅍㙏牑⁓㹕㵗穙⡛ⱝşౡᝣᙥ१ᡩ५m፯ୱ味յၷᕹॻችꊁꢇ揄낗ꪙ놛꾝邟銡趣", a_));
			IL_1AC:
			goto IL_239;
			IL_1B1:
			throw new NotSupportedException(RecordTableEnumerator.b("渹吻嬽怿ᑁ╃⩅㵇⽉汋⅍㙏牑⁓㹕㵗穙㵛そݟ๡ţ䙥᭧ɩͫ᭭ᱯᙱ味ᑵᵷ婹ṻ᭽ꊉ벋ꎍꎏꞑ궓뾕", a_));
			IL_1C5:
			throw new NotSupportedException(RecordTableEnumerator.b("渹吻嬽怿ᑁ╃⩅㵇⽉汋⅍㙏牑⁓㹕㵗穙㡛㝝፟ᙡգࡥ୧ཀྵ䱫ᵭᡯᵱų᩵ᱷ婹ṻ᭽ꁿﾇ뢏ꊑ릓꒕ꢗꪙ떛", a_));
			IL_239:
			throw new NotSupportedException(RecordTableEnumerator.b("渹吻嬽怿㑁╃⩅㵇⽉汋⅍㙏牑⁓㹕㵗穙⽛㝝᩟ݡ䑣ᕥgթᥫɭᑯ剱ᙳ፵塷᡹᥻੽ꂇ몉ꆋ벍ꂏꊑ붓", a_));
			IL_26B:
			goto IL_170;
			IL_292:
			goto IL_1B1;
			IL_2B6:
			goto IL_138;
			IL_2BB:
			this.ᜂ = CustomShadowStyle;
			this.ᜀ.ShadowOuterType = iOuter;
			this.ᜃ = (100 - iTransparency) * 1000;
			this.ᜄ = iSize * 1000;
			this.ᜅ = iBlur * 12700;
			this.ᜇ = iAngle * 60000;
			this.ᜈ = iDistance * 12700;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0005DA38 File Offset: 0x0005CA38
		public void CustomShadowStyles(XLSXChartShadowInnerType iInner, int iTransparency, int iBlur, int iAngle, int iDistance, bool CustomShadowStyle)
		{
			int a_ = 12;
			int num = 19;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (iAngle > 359)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					if (iTransparency > 100)
					{
						num = 7;
						continue;
					}
					num = 12;
					continue;
				case 3:
					if (iBlur <= 100)
					{
						num = 15;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F0;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 4:
					if (iDistance > 200)
					{
						num = 18;
						continue;
					}
					goto IL_24A;
				case 5:
					if (iDistance >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_12E;
				case 6:
					goto IL_A3;
				case 7:
					goto IL_1E1;
				case 8:
					num = 3;
					continue;
				case 9:
					if (true)
					{
					}
					num = 2;
					continue;
				case 10:
					if (iTransparency >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_1F7;
				case 11:
					goto IL_245;
				case 12:
					if (iBlur >= 0)
					{
						num = 8;
						continue;
					}
					goto IL_A8;
				case 13:
					if (iInner == XLSXChartShadowInnerType.None)
					{
						num = 14;
						continue;
					}
					num = 10;
					continue;
				case 14:
					goto IL_109;
				case 15:
					if (iAngle >= 0)
					{
						num = 16;
						continue;
					}
					goto IL_1A7;
				case 16:
					num = 0;
					continue;
				case 17:
					goto IL_82;
				case 18:
					goto IL_1A5;
				}
				if (!CustomShadowStyle)
				{
					num = 17;
					continue;
				}
				IL_F0:
				num = 13;
			}
			IL_82:
			throw new NotSupportedException(RecordTableEnumerator.b("ୁぃ晅㭇≉⍋㭍㱏㙑瑓㑕㵗穙⽛㭝ᑟ䉡ၣᑥᵧཀྵ䱫ᩭὯ剱ᵳ᭵ࡷᙹ᥻፽ꚅﲇ꺍벛좟쎡삣즥\udfa7誩\udfab\udaad즯\udeb1톳", a_));
			IL_A3:
			goto IL_1A7;
			IL_A8:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡٣੥ᵧᡩ䱫ᵭᡯᵱų᩵ᱷ婹ṻ᭽ꁿﾇ뢏ꊑ릓ꞕꢗꪙ떛", a_));
			IL_109:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇❉⥋㩍㡏㵑こ癕㱗㕙㥛ⵝ䁟ౡୣብ䡧୩ཫ൭ᕯɱs噵㙷ᕹཻᙽ", a_));
			IL_12E:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡cཥ᭧ṩ൫m፯᝱味յၷᕹॻችꊁꢇ揄낗ꪙ놛겝邟銡趣", a_));
			IL_1A5:
			goto IL_12E;
			IL_1A7:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡գࡥཧ٩५乭ͯᩱ᭳͵ᑷṹ屻ᱽꊁﲇﶉﺏ몑꒓뮕ꮗ꾙ꖛ랝", a_));
			IL_1E1:
			IL_1F7:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡ၣᑥ१ѩὫṭᅯqᅳᡵ᭷͹屻ൽꪉ낏ﾙ鍊袟銡覣鞥颧骩薫", a_));
			IL_245:
			goto IL_A8;
			IL_24A:
			this.ᜂ = CustomShadowStyle;
			this.ᜀ.ShadowInnerType = iInner;
			this.ᜃ = (100 - iTransparency) * 1000;
			this.ᜅ = iBlur * 12700;
			this.ᜇ = iAngle * 60000;
			this.ᜈ = iDistance * 12700;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0005DCDC File Offset: 0x0005CCDC
		public void CustomShadowStyles(XLSXChartPrespectiveType iPerspective, int iTransparency, int iSize, int iBlur, int iAngle, int iDistance, bool CustomShadowStyle)
		{
			int a_ = 12;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (iAngle >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_1A9;
				case 1:
					if (iBlur > 100)
					{
						num = 20;
						continue;
					}
					num = 0;
					continue;
				case 2:
					if (iSize > 0)
					{
						num = 23;
						continue;
					}
					goto IL_231;
				case 4:
					if (iTransparency >= 0)
					{
						num = 13;
						continue;
					}
					goto IL_168;
				case 5:
					goto IL_12B;
				case 6:
					if (iSize > 200)
					{
						num = 10;
						continue;
					}
					num = 4;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					if (iAngle > 359)
					{
						num = 21;
						continue;
					}
					num = 19;
					continue;
				case 9:
					num = 1;
					continue;
				case 10:
					goto IL_1A4;
				case 11:
					if (iDistance > 200)
					{
						num = 22;
						continue;
					}
					goto IL_2BB;
				case 12:
					goto IL_88;
				case 13:
					num = 14;
					continue;
				case 14:
					if (iTransparency > 100)
					{
						if (true)
						{
						}
						num = 15;
						continue;
					}
					num = 18;
					continue;
				case 15:
					goto IL_26B;
				case 16:
					if (iPerspective == XLSXChartPrespectiveType.None)
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				case 17:
					num = 11;
					continue;
				case 18:
					if (iBlur >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_130;
				case 19:
					if (iDistance >= 0)
					{
						num = 17;
						continue;
					}
					goto IL_1BD;
				case 20:
					goto IL_2B6;
				case 21:
					goto IL_292;
				case 22:
					goto IL_166;
				case 23:
					num = 6;
					continue;
				}
				if (!CustomShadowStyle)
				{
					num = 12;
				}
				else
				{
					num = 16;
				}
			}
			IL_88:
			throw new NotSupportedException(RecordTableEnumerator.b("ୁぃ晅㭇≉⍋㭍㱏㙑瑓㑕㵗穙⽛㭝ᑟ䉡ၣᑥᵧཀྵ䱫ᩭὯ剱ᵳ᭵ࡷᙹ᥻፽ꚅﲇ꺍벛좟쎡삣즥\udfa7誩\udfab\udaad즯\udeb1톳", a_));
			IL_12B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_88;
			default:
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇❉⥋㩍㡏㵑こ癕㱗㕙㥛ⵝ䁟ౡୣብ䡧୩ཫ൭ᕯɱs噵㙷ᕹཻᙽ", a_));
			}
			IL_130:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡٣੥ᵧᡩ䱫ᵭᡯᵱų᩵ᱷ婹ṻ᭽ꁿﾇ뢏ꊑ릓ꞕꢗꪙ떛", a_));
			IL_166:
			goto IL_1BD;
			IL_168:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡ၣᑥ१ѩὫṭᅯqᅳᡵ᭷͹屻ൽꪉ낏ﾙ鍊袟銡覣鞥颧骩薫", a_));
			IL_1A4:
			goto IL_231;
			IL_1A9:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡գࡥཧ٩५乭ͯᩱ᭳͵ᑷṹ屻ᱽꊁﲇﶉﺏ몑꒓뮕ꮗ꾙ꖛ랝", a_));
			IL_1BD:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇᱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡cཥ᭧ṩ൫m፯᝱味յၷᕹॻችꊁꢇ揄낗ꪙ놛겝邟銡趣", a_));
			IL_231:
			throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⍅桇㱉ⵋ≍╏㝑瑓㥕㹗穙⡛㙝՟䉡ᝣཥቧཀྵ䱫ᵭᡯᵱų᩵ᱷ婹ṻ᭽ꁿﾇ뢏ꊑ릓꒕ꢗꪙ떛", a_));
			IL_26B:
			goto IL_168;
			IL_292:
			goto IL_1A9;
			IL_2B6:
			goto IL_130;
			IL_2BB:
			this.ᜂ = CustomShadowStyle;
			this.ᜀ.ShadowPrespectiveType = iPerspective;
			this.ᜃ = (100 - iTransparency) * 1000;
			this.ᜄ = iSize * 1000;
			this.ᜅ = iBlur * 12700;
			this.ᜇ = iAngle * 60000;
			this.ᜈ = iDistance * 12700;
		}

		// Token: 0x040001B9 RID: 441
		private ShadowOptions ᜀ = new ShadowOptions();

		// Token: 0x040001BA RID: 442
		private XlsWorkbook ᜁ;

		// Token: 0x040001BB RID: 443
		private bool ᜂ;

		// Token: 0x040001BC RID: 444
		private int ᜃ;

		// Token: 0x040001BD RID: 445
		private float \u2593\u0093\u00A7\u0091;

		// Token: 0x040001BE RID: 446
		private int ᜄ;

		// Token: 0x040001BF RID: 447
		private int ᜅ;

		// Token: 0x040001C0 RID: 448
		private sprᣐ ᜆ;

		// Token: 0x040001C1 RID: 449
		private int ᜇ;

		// Token: 0x040001C2 RID: 450
		private int ᜈ;

		// Token: 0x040001C3 RID: 451
		private string[] \u25D9\u0091\u00A4\u00A6;

		// Token: 0x040001C4 RID: 452
		private OColor ᜉ;
	}
}
