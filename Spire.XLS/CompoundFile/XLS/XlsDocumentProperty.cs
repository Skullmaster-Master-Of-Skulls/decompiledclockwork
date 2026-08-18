using System;
using System.Runtime.InteropServices;
using Spire.CompoundFile.XLS.Native;
using Spire.CompoundFile.XLS.Net;
using Spire.Xls;
using Spire.Xls.Core.Interface;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.CompoundFile.XLS
{
	// Token: 0x02000129 RID: 297
	public class XlsDocumentProperty : IDocumentProperty, ICloneable
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x0007D630 File Offset: 0x0007C630
		private XlsDocumentProperty()
		{
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0007D644 File Offset: 0x0007C644
		public XlsDocumentProperty(string strName, object value)
		{
			int a_ = 19;
			base..ctor();
			if (strName == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌Ŏぐ㹒ご", a_));
			}
			if (strName.Length == 0)
			{
				throw new ArgumentException(RecordTableEnumerator.b("㩈㽊㽌Ŏぐ㹒ご睖瑘筚⹜⭞፠੢୤f䥨ࡪ౬ŮὰᱲŴ坶᭸Ṻ嵼᩾ﺆꞈ", a_));
			}
			this.ᜂ = strName;
			this.Value = value;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0007D6A8 File Offset: 0x0007C6A8
		public XlsDocumentProperty(BuiltInPropertyType propertyId, object value)
		{
			this.ᜁ = propertyId;
			this.ᜃ = value;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0007D6CC File Offset: 0x0007C6CC
		public XlsDocumentProperty(IPropertyData variant, bool bSummary)
		{
			int a_ = 12;
			base..ctor();
			if (variant == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㑁╃㑅ⅇ⭉≋㩍", a_));
			}
			this.ᜂ = variant.Name;
			if (this.ᜂ == null)
			{
				if (bSummary)
				{
					this.ᜁ = (BuiltInPropertyType)variant.Id;
				}
				else
				{
					this.ᜁ = variant.Id + BuiltInPropertyType.Category - 2;
				}
			}
			this.ᜃ = variant.Value;
			this.ᜄ = (PropertyType)variant.Type;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0007D758 File Offset: 0x0007C758
		public bool IsBuiltIn
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
				return this.ᜂ == null;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0007D79C File Offset: 0x0007C79C
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0007D7E0 File Offset: 0x0007C7E0
		public BuiltInPropertyType PropertyId
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0007D824 File Offset: 0x0007C824
		public string Name
		{
			get
			{
				if (this.ᜂ != null)
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
						break;
					}
					if (true)
					{
					}
					return this.ᜂ;
				}
				return this.ᜁ.ToString();
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0007D880 File Offset: 0x0007C880
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0007D8C4 File Offset: 0x0007C8C4
		public object Value
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
				this.ᜁ();
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0007D90C File Offset: 0x0007C90C
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0007D97C File Offset: 0x0007C97C
		public bool Boolean
		{
			get
			{
				int a_ = 2;
				if (this.ᜄ == PropertyType.Bool)
				{
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
					return Convert.ToBoolean(this.ᜃ);
				}
				throw new InvalidCastException(RecordTableEnumerator.b("笷嬹刻᤽㐿扁❃⥅♇㱉⥋㱍⑏牑≓㝕㑗⽙㥛繝ᑟൡ䑣ѥݧթk୭ᅯᱱ婳", a_));
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
				this.ᜄ = PropertyType.Bool;
				this.ᜃ = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0007D9CC File Offset: 0x0007C9CC
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0007DA3C File Offset: 0x0007CA3C
		public int Integer
		{
			get
			{
				int a_ = 1;
				if (this.ᜄ == PropertyType.Int)
				{
					if (true)
					{
					}
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
					return Convert.ToInt32(this.ᜃ);
				}
				throw new InvalidCastException(RecordTableEnumerator.b("琶堸唺ᨼ䬾慀⁂⩄⥆㽈⹊㽌㭎煐╒㑔㭖ⱘ㹚絜⭞๠䍢౤०ᵨ๪੬੮Ͱ嵲", a_));
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
				this.ᜄ = PropertyType.Int;
				this.ᜃ = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0007DA8C File Offset: 0x0007CA8C
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0007DAFC File Offset: 0x0007CAFC
		public int Int32
		{
			get
			{
				int a_ = 14;
				if (true)
				{
				}
				if (this.ᜄ == PropertyType.Int32)
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
						break;
					}
					return Convert.ToInt32(this.ᜃ);
				}
				throw new InvalidCastException(RecordTableEnumerator.b("݃❅♇浉㡋湍㍏㵑㩓⁕㵗⡙⡛繝ᙟ͡ࡣ፥൧䩩ᡫŭ偯᭱ᩳɵᵷᵹ᥻౽깿", a_));
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
				this.ᜄ = PropertyType.Int32;
				this.ᜃ = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0007DB4C File Offset: 0x0007CB4C
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0007DBBC File Offset: 0x0007CBBC
		public double Double
		{
			get
			{
				int a_ = 17;
				if (this.ᜄ == PropertyType.Double)
				{
					if (true)
					{
					}
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
					return Convert.ToDouble(this.ᜃ);
				}
				throw new InvalidCastException(RecordTableEnumerator.b("ц⡈╊橌㭎煐げ㩔㥖⽘㹚⽜⭞䅠ᕢѤ୦ᱨ๪䵬᭮Ṱ卲ᱴ᥶൸Ṻ᩼᩾궂", a_));
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
				this.ᜄ = PropertyType.Double;
				this.ᜃ = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0007DC0C File Offset: 0x0007CC0C
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0007DC7C File Offset: 0x0007CC7C
		public string Text
		{
			get
			{
				int a_ = 7;
				if (this.ᜄ == PropertyType.String)
				{
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
					return Convert.ToString(this.ᜃ);
				}
				throw new InvalidCastException(RecordTableEnumerator.b("縼帾⽀摂ㅄ杆⩈⑊⍌㥎㑐⅒⅔睖⽘㩚ㅜ⩞Ѡ䍢ᅤࡦ䥨ᡪᥬᵮᡰᵲቴ奶", a_));
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
				this.ᜄ = PropertyType.String;
				this.ᜃ = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0007DCC8 File Offset: 0x0007CCC8
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0007DD38 File Offset: 0x0007CD38
		public DateTime DateTime
		{
			get
			{
				int a_ = 8;
				if (this.ᜄ == PropertyType.DateTime)
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
						break;
					}
					if (true)
					{
					}
					return Convert.ToDateTime(this.ᜃ);
				}
				throw new InvalidCastException(RecordTableEnumerator.b("紽ℿⱁ捃㉅桇⥉⍋⁍♏㝑♓≕硗ⱙ㵛㉝ᕟݡ䑣ብݧ䩩⡫཭ѯ᝱⁳ήᕷό剻", a_));
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
				this.ᜄ = PropertyType.DateTime;
				this.ᜃ = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0007DD88 File Offset: 0x0007CD88
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0007DDE4 File Offset: 0x0007CDE4
		public TimeSpan TimeSpan
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
				return new TimeSpan(this.DateTime.AddYears(-1600).Ticks);
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
				DateTime dateTime = new DateTime(value.Ticks);
				dateTime = dateTime.AddYears(1600);
				this.DateTime = dateTime;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0007DE44 File Offset: 0x0007CE44
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0007DEB4 File Offset: 0x0007CEB4
		public byte[] Blob
		{
			get
			{
				int a_ = 16;
				if (this.ᜄ == PropertyType.Blob)
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
						if (true)
						{
						}
						break;
					}
					return (byte[])this.ᜃ;
				}
				throw new InvalidCastException(RecordTableEnumerator.b("Յ⥇⑉歋㩍灏ㅑ㭓㡕⹗㽙⹛⩝䁟ᑡգ੥ᵧཀྵ䱫ᩭὯ剱㙳᩵᝷᡹剻", a_));
			}
			set
			{
				int a_ = 15;
				if (value == null)
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
						break;
					}
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("㍄♆╈㹊⡌", a_));
				}
				this.ᜄ = PropertyType.Blob;
				this.ᜃ = value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0007DF20 File Offset: 0x0007CF20
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0007DF94 File Offset: 0x0007CF94
		public string[] StringArray
		{
			get
			{
				int a_ = 19;
				if (true)
				{
				}
				if (this.ᜄ == PropertyType.StringArray)
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
						break;
					}
					return (string[])this.ᜃ;
				}
				throw new InvalidCastException(RecordTableEnumerator.b("ੈ⩊⍌桎═獒㙔㡖㝘ⵚ㡜ⵞᕠ䍢፤٦ըṪ࡬佮հᱲ啴ᙶ᝸孺ᱼൾﲄꞆ권ﲎﲔ練ﺘ뎜", a_));
			}
			set
			{
				int a_ = 11;
				if (value == null)
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
						break;
					}
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("㝀≂⥄㉆ⱈ", a_));
				}
				this.ᜄ = PropertyType.StringArray;
				this.ᜃ = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0007E004 File Offset: 0x0007D004
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0007E078 File Offset: 0x0007D078
		public object[] ObjectArray
		{
			get
			{
				int a_ = 3;
				if (this.ᜄ == PropertyType.ObjectArray)
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
						break;
					}
					return (object[])this.ᜃ;
				}
				if (true)
				{
				}
				throw new InvalidCastException(RecordTableEnumerator.b("稸娺匼ᠾ㕀捂♄⡆❈㵊⡌㵎═獒⍔㙖㕘⹚㡜罞ᕠౢ䕤٦ݨ䭪౬ᵮͰቲ౴坶ᙸᵺ嵼౾ꎌ", a_));
			}
			set
			{
				int a_ = 8;
				if (value == null)
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
						break;
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("䠽ℿ⹁ㅃ⍅", a_));
				}
				if (true)
				{
				}
				this.ᜄ = PropertyType.ObjectArray;
				this.ᜃ = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0007E0E8 File Offset: 0x0007D0E8
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0007E12C File Offset: 0x0007D12C
		public PropertyType PropertyType
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0007E170 File Offset: 0x0007D170
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0007E1B4 File Offset: 0x0007D1B4
		public string LinkSource
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
				int a_ = 6;
				if (this.IsBuiltIn)
				{
					if (true)
					{
					}
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
					throw new InvalidOperationException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃⥅㡇⽉㹋⽍⑏㭑㭓㡕硗㥙㵛そ䝟ᙡ䑣ѥ൧䩩ᱫ୭ɯᑱ᭳ѵᕷό᡻幽ꒃﶇ揄붏ﮑ望뚕얟킡킣\udfa5蚧", a_));
				}
				this.ᜅ = value;
				this.ᜆ = true;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0007E224 File Offset: 0x0007D224
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0007E268 File Offset: 0x0007D268
		public bool LinkToContent
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

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0007E2AC File Offset: 0x0007D2AC
		public string InternalName
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
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0007E2F0 File Offset: 0x0007D2F0
		public bool FillPropVariant(IPropertyData variant, int iPropertyId)
		{
			int a_ = 5;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜃ == null)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 1:
					goto IL_66;
				case 2:
					if (this.IsBuiltIn)
					{
						goto IL_F6;
					}
					variant.Id = iPropertyId;
					if (true)
					{
					}
					num = 4;
					continue;
				case 3:
					return false;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F6;
					default:
						goto IL_BB;
					}
					break;
				case 5:
					goto IL_47;
				case 6:
				{
					bool flag;
					int id = XlsDocumentProperty.CorrectIndex(this.ᜁ, out flag);
					variant.Id = id;
					num = 1;
					continue;
				}
				}
				if (variant == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
				IL_F6:
				num = 6;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼䴾⡀≂⭄㍆", a_));
			IL_66:
			goto IL_106;
			IL_BB:
			if (false)
			{
			}
			IL_106:
			return variant.SetValue(this.ᜃ, this.ᜄ);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0007E418 File Offset: 0x0007D418
		public static int CorrectIndex(BuiltInPropertyType propertyId, out bool bSummary)
		{
			int num;
			for (;;)
			{
				num = (int)propertyId;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= 1000)
						{
							num2 = 1;
							continue;
						}
						goto IL_34;
					case 1:
						num -= 998;
						bSummary = false;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						}
						if (false)
						{
						}
						num2 = 3;
						continue;
					case 2:
						goto IL_3F;
					case 3:
						return num;
					}
					break;
					IL_34:
					bSummary = true;
					num2 = 2;
				}
			}
			IL_3F:
			if (true)
			{
			}
			return num;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0007E4AC File Offset: 0x0007D4AC
		private void ᜁ()
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜃ is byte[])
					{
						num = 6;
						continue;
					}
					return;
				case 1:
					if (this.ᜃ is int)
					{
						num = 11;
						continue;
					}
					num = 15;
					continue;
				case 2:
					goto IL_1E3;
				case 3:
					goto IL_F4;
				case 4:
					goto IL_CC;
				case 5:
					if (this.ᜃ is object[])
					{
						goto IL_E9;
					}
					num = 13;
					continue;
				case 6:
					this.ᜄ = PropertyType.Blob;
					num = 9;
					continue;
				case 8:
					goto IL_69;
				case 9:
					goto IL_10C;
				case 10:
					goto IL_1A9;
				case 11:
					goto IL_140;
				case 12:
					if (this.ᜃ is double)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
				case 13:
					if (!(this.ᜃ is string[]))
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E9;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 14:
					goto IL_20E;
				case 15:
					if (this.ᜃ is bool)
					{
						num = 2;
						continue;
					}
					num = 16;
					continue;
				case 16:
					if (this.ᜃ is DateTime)
					{
						num = 14;
						continue;
					}
					num = 5;
					continue;
				}
				if (this.ᜃ is string)
				{
					num = 8;
					continue;
				}
				num = 12;
				continue;
				IL_E9:
				num = 3;
			}
			IL_69:
			this.ᜄ = PropertyType.String;
			return;
			IL_CC:
			if (true)
			{
			}
			this.ᜄ = PropertyType.Double;
			return;
			IL_F4:
			this.ᜄ = PropertyType.ObjectArray;
			return;
			IL_10C:
			return;
			IL_140:
			this.ᜄ = PropertyType.Int;
			return;
			IL_1A9:
			this.ᜄ = PropertyType.StringArray;
			return;
			IL_1E3:
			this.ᜄ = PropertyType.Bool;
			return;
			IL_20E:
			this.ᜄ = PropertyType.DateTime;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0007E6CC File Offset: 0x0007D6CC
		public void SetLinkSource(IPropertyData variant)
		{
			int a_ = 15;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_62;
				case 2:
					IL_74:
					if (variant.Type != VarEnum.VT_LPSTR)
					{
						num = 3;
						continue;
					}
					goto IL_B0;
				case 3:
					num = 4;
					continue;
				case 4:
					if (variant.Type != VarEnum.VT_LPWSTR)
					{
						num = 1;
						continue;
					}
					goto IL_B0;
				case 5:
					goto IL_3C;
				}
				if (variant == null)
				{
					num = 5;
					continue;
				}
				num = 2;
				continue;
				IL_B0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
					goto IL_C6;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍄♆㭈≊ⱌⅎ═", a_));
			IL_62:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ॄ⹆❈⁊Ṍ⁎⑐⅒㙔㉖", a_));
			IL_C6:
			if (false)
			{
			}
			this.LinkSource = variant.Value.ToString();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0007E7B8 File Offset: 0x0007D7B8
		[CLSCompliant(false)]
		internal void ᜀ(spr\u17B9 A_0, spr\u1D49 A_1, int A_2)
		{
			int a_ = 7;
			if (true)
			{
			}
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.IsBuiltIn)
					{
						goto IL_129;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_108;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_54;
				case 2:
				{
					uint num2 = (uint)A_2;
					A_0.ᜁ(1U, ref num2, ref this.ᜂ);
					num = 7;
					continue;
				}
				case 3:
					goto IL_DC;
				case 4:
					if (this.LinkToContent)
					{
						num = 9;
						continue;
					}
					return;
				case 5:
					return;
				case 7:
					goto IL_129;
				case 8:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					this.FillPropVariant(A_1, A_2);
					A_1.ᜀ(A_0);
					num = 0;
					continue;
				case 9:
					A_1.ᜀ(A_2 + (PIDSI)16777216);
					A_1.ᜁ(this.ᜅ);
					A_1.ᜀ(A_0);
					goto IL_108;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 8;
				continue;
				IL_108:
				num = 5;
				continue;
				IL_129:
				num = 4;
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾⹀ㅂᕄ㕆♈㭊", a_));
			IL_DC:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾㍀⩂⑄⥆㵈", a_));
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0007E924 File Offset: 0x0007D924
		public object Clone()
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
			XlsDocumentProperty xlsDocumentProperty = (XlsDocumentProperty)base.MemberwiseClone();
			xlsDocumentProperty.ᜀ();
			return xlsDocumentProperty;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0007E974 File Offset: 0x0007D974
		private void ᜀ()
		{
			int num = 1;
			for (;;)
			{
				PropertyType propertyType;
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 2:
					return;
				case 3:
					if (propertyType != PropertyType.StringArray)
					{
						num = 5;
						continue;
					}
					goto IL_85;
				case 4:
					num = 3;
					continue;
				case 5:
					return;
				case 6:
					if (propertyType != PropertyType.Blob)
					{
						goto IL_7B;
					}
					goto IL_98;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						if (propertyType != PropertyType.ObjectArray)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						goto IL_F1;
					}
					break;
				}
				if (this.ᜃ == null)
				{
					num = 2;
					continue;
				}
				propertyType = this.ᜄ;
				num = 6;
				continue;
				IL_7B:
				num = 0;
			}
			return;
			IL_85:
			this.ᜃ = sprἽ.ᜀ(this.StringArray);
			return;
			IL_98:
			this.ᜃ = sprἽ.ᜀ(this.Blob);
			return;
			IL_F1:
			this.ᜃ = sprἽ.ᜀ(this.ObjectArray);
		}

		// Token: 0x04000B85 RID: 2949
		private const int ᜀ = 1000;

		// Token: 0x04000B86 RID: 2950
		private bool[] \u25D8\u0081\u00AC\u0095;

		// Token: 0x04000B87 RID: 2951
		private long[] \u25D9\u00AF\u0091\u0099;

		// Token: 0x04000B88 RID: 2952
		public const int DEF_FILE_TIME_START_YEAR = 1600;

		// Token: 0x04000B89 RID: 2953
		private BuiltInPropertyType ᜁ;

		// Token: 0x04000B8A RID: 2954
		private int[] \u2609\u0086\u009A\u009D;

		// Token: 0x04000B8B RID: 2955
		private string ᜂ;

		// Token: 0x04000B8C RID: 2956
		private object ᜃ;

		// Token: 0x04000B8D RID: 2957
		private string[] \u2593\u0087\u0081\u00A3;

		// Token: 0x04000B8E RID: 2958
		private PropertyType ᜄ;

		// Token: 0x04000B8F RID: 2959
		private long \u25D9\u008A\u0083\u008F;

		// Token: 0x04000B90 RID: 2960
		private string ᜅ;

		// Token: 0x04000B91 RID: 2961
		private bool ᜆ;
	}
}
