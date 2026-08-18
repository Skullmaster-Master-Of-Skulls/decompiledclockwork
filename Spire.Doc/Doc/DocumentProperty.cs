using System;
using System.Runtime.InteropServices;
using System.Text;
using Spire.CompoundFile.Doc;

namespace Spire.Doc
{
	// Token: 0x020000FC RID: 252
	public class DocumentProperty
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x00047818 File Offset: 0x00046818
		private DocumentProperty()
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0004782C File Offset: 0x0004682C
		internal DocumentProperty(string A_0, object A_1)
		{
			int a_ = 11;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(ClipboardData.b("ɰݲݴ㥶ᡸᙺ᡼", a_));
			}
			if (A_0.Length == 0)
			{
				throw new ArgumentException(ClipboardData.b("ɰݲݴ㥶ᡸᙺ᡼彾검ꎂﮈ놐練뾞쎠욢薤슦쒨\udbaa\ud9ac횮龰", a_));
			}
			this.ᜃ = A_0;
			this.Value = A_1;
			this.ᜅ = DocumentProperty.ᜀ(A_1);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0004789C File Offset: 0x0004689C
		internal DocumentProperty(string A_0, object A_1, PropertyType A_2)
		{
			int a_ = 8;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(ClipboardData.b("ᵭѯq㩳᝵ᕷό", a_));
			}
			if (A_0.Length == 0)
			{
				throw new ArgumentException(ClipboardData.b("ᵭѯq㩳᝵ᕷό屻卽ꁿ꺍望벛ﲝ얟芡솣쮥\ud8a7\udea9햫肭", a_));
			}
			this.ᜃ = A_0;
			this.ᜄ = A_1;
			this.ᜅ = A_2;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00047904 File Offset: 0x00046904
		internal DocumentProperty(BuiltInProperty A_0, object A_1)
		{
			this.ᜂ = A_0;
			this.ᜄ = A_1;
			this.ᜅ = DocumentProperty.ᜀ(A_1);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00047934 File Offset: 0x00046934
		internal DocumentProperty(spr\u2097 A_0, bool A_1)
		{
			int a_ = 7;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(ClipboardData.b("᭬๮Ͱᩲᑴ᥶൸", a_));
			}
			this.ᜃ = A_0.ᜂ();
			if (this.ᜃ == null)
			{
				if (A_1)
				{
					this.ᜂ = (BuiltInProperty)A_0.ᜃ();
				}
				else
				{
					this.ᜂ = A_0.ᜃ() + BuiltInProperty.Category - 2;
				}
			}
			if (A_1 && this.ᜂ == BuiltInProperty.EditTime && A_0.ᜀ() is DateTime)
			{
				this.ᜄ = TimeSpan.FromTicks(((DateTime)A_0.ᜀ()).Ticks - 504911232000000000L);
			}
			else
			{
				this.ᜄ = A_0.ᜀ();
			}
			this.ᜅ = (PropertyType)A_0.ᜁ();
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00047A18 File Offset: 0x00046A18
		internal bool IsBuiltIn
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
				return this.ᜃ == null;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00047A5C File Offset: 0x00046A5C
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00047AA0 File Offset: 0x00046AA0
		internal BuiltInProperty PropertyId
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

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00047AE4 File Offset: 0x00046AE4
		public string Name
		{
			get
			{
				if (this.ᜃ == null)
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
						return this.ᜂ.ToString();
					}
				}
				return this.ᜃ;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00047B40 File Offset: 0x00046B40
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00047B84 File Offset: 0x00046B84
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
				this.ᜁ();
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00047BCC File Offset: 0x00046BCC
		public PropertyValueType ValueType
		{
			get
			{
				int a_ = 10;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜄ is double)
						{
							goto IL_A2;
						}
						num = 7;
						continue;
					case 1:
						if (!(this.ᜄ is int))
						{
							num = 17;
							continue;
						}
						return PropertyValueType.Int;
					case 2:
						if (this.ᜄ is DateTime)
						{
							num = 4;
							continue;
						}
						num = 1;
						continue;
					case 3:
						if (this.Value is byte[])
						{
							num = 10;
							continue;
						}
						num = 8;
						continue;
					case 4:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A2;
						default:
							goto IL_F6;
						}
						break;
					case 5:
						if (this.ᜄ is bool)
						{
							num = 13;
							continue;
						}
						num = 2;
						continue;
					case 7:
						if (this.Value is float)
						{
							num = 14;
							continue;
						}
						num = 3;
						continue;
					case 8:
						if (this.Value is sprᱵ)
						{
							num = 15;
							continue;
						}
						goto IL_1FC;
					case 9:
						goto IL_1F5;
					case 10:
						return PropertyValueType.ByteArray;
					case 11:
						return PropertyValueType.String;
					case 12:
						if (this.ᜄ is int)
						{
							num = 9;
							continue;
						}
						num = 0;
						continue;
					case 13:
						return PropertyValueType.Boolean;
					case 14:
						return PropertyValueType.Float;
					case 15:
						return PropertyValueType.ClipData;
					case 16:
						return PropertyValueType.Double;
					case 17:
						num = 12;
						continue;
					}
					if (this.ᜄ is string)
					{
						num = 11;
						continue;
					}
					num = 5;
					continue;
					IL_A2:
					num = 16;
				}
				return PropertyValueType.String;
				IL_F6:
				if (false)
				{
				}
				return PropertyValueType.Date;
				IL_1F5:
				return PropertyValueType.Int;
				IL_1FC:
				throw new Exception(ClipboardData.b("⁯q᭳ٵᵷࡹࡻݽꁿﶇ겋늑ﮓ뢗햟튡풣즥\udaa7\udea9즫쪭邯욱춳욵\uddb7钹", a_));
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00047DE8 File Offset: 0x00046DE8
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00047E58 File Offset: 0x00046E58
		internal bool Boolean
		{
			get
			{
				int a_ = 1;
				if (this.ᜅ != PropertyType.Bool)
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
						throw new InvalidCastException(ClipboardData.b("⑦ࡨժ䩬᭮兰ၲᩴ᥶ླྀṺོ୾ꆀﲈ권ﮎﺐ뎒ﺞ쾠趢", a_));
					}
				}
				return Convert.ToBoolean(this.ᜄ);
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
				this.ᜅ = PropertyType.Bool;
				this.ᜄ = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00047EA8 File Offset: 0x00046EA8
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00047F24 File Offset: 0x00046F24
		internal int Integer
		{
			get
			{
				int a_ = 13;
				this.ᜁ();
				if (this.ᜅ != PropertyType.Int)
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
						throw new InvalidCastException(ClipboardData.b("ひᑴ᥶幸ེ嵼᱾ﮈﾊ권年ﾒ릘뾞좠춢톤슦캨캪\udfac膮", a_));
					}
				}
				return int.Parse(this.ᜄ.ToString());
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
				this.ᜅ = PropertyType.Int;
				this.ᜄ = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00047F74 File Offset: 0x00046F74
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x00047FE8 File Offset: 0x00046FE8
		internal int Int32
		{
			get
			{
				int a_ = 14;
				this.ᜁ();
				if (this.ᜅ != PropertyType.Int32)
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
						throw new InvalidCastException(ClipboardData.b("㝳᝵ᙷ嵹ࡻ幽꺍ﶗ몙肟쮡쪣튥춧충즫\udcad麯", a_));
					}
				}
				if (true)
				{
				}
				return Convert.ToInt32(this.ᜄ);
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
				this.ᜅ = PropertyType.Int32;
				this.ᜄ = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00048038 File Offset: 0x00047038
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x000480AC File Offset: 0x000470AC
		internal double Double
		{
			get
			{
				int a_ = 4;
				this.ᜁ();
				if (this.ᜅ != PropertyType.Double)
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
						throw new InvalidCastException(ClipboardData.b("⥩൫m坯ٱ味ᕵ᝷ᑹ੻᭽ꒃ曆낏ﮓ뚕ﮝ잟잡횣袥", a_));
					}
				}
				if (true)
				{
				}
				return Convert.ToDouble(this.ᜄ);
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
				this.ᜅ = PropertyType.Double;
				this.ᜄ = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000480FC File Offset: 0x000470FC
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x000481E8 File Offset: 0x000471E8
		internal string Text
		{
			get
			{
				int a_ = 4;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_97;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						if (this.ᜅ == PropertyType.AsciiString)
						{
							num = 1;
							continue;
						}
						goto IL_C9;
					case 4:
						goto IL_87;
					case 5:
						if (this.ᜅ != PropertyType.String)
						{
							num = 0;
							continue;
						}
						goto IL_97;
					case 6:
						goto IL_6B;
					}
					if (this.ᜅ == PropertyType.Empty)
					{
						num = 4;
						continue;
					}
					IL_6B:
					num = 5;
					continue;
					IL_87:
					this.ᜁ();
					num = 6;
					continue;
					IL_97:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_87;
					default:
						goto IL_B7;
					}
				}
				IL_B7:
				if (false)
				{
				}
				return Convert.ToString(this.ᜄ);
				IL_C9:
				throw new InvalidCastException(ClipboardData.b("⥩൫m坯ٱ味ᕵ᝷ᑹ੻᭽ꒃ曆낏ﮓ뚕캟얡誣", a_));
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
				this.ᜅ = this.ᜀ(value);
				this.ᜄ = value;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00048238 File Offset: 0x00047238
		private PropertyType ᜀ(string A_0)
		{
			if (Encoding.UTF8.GetByteCount(A_0) == A_0.Length)
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
					return PropertyType.AsciiString;
				}
			}
			if (true)
			{
			}
			return PropertyType.String;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00048290 File Offset: 0x00047290
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00048348 File Offset: 0x00047348
		internal DateTime DateTime
		{
			get
			{
				DateTime result;
				try
				{
					for (;;)
					{
						this.ᜁ();
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									if (this.ᜅ != PropertyType.DateTime)
									{
										result = DateTime.MinValue;
										num = 3;
										continue;
									}
									break;
								}
								num = 1;
								continue;
							case 1:
								result = Convert.ToDateTime(this.ᜄ);
								num = 2;
								continue;
							case 2:
								goto IL_6A;
							case 3:
								goto IL_7A;
							}
							break;
						}
					}
					IL_6A:
					IL_7A:;
				}
				catch
				{
					result = DateTime.MinValue;
				}
				if (true)
				{
				}
				return result;
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
				this.ᜅ = PropertyType.DateTime;
				this.ᜄ = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00048398 File Offset: 0x00047398
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x000483E0 File Offset: 0x000473E0
		internal TimeSpan TimeSpan
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
				return (TimeSpan)this.Value;
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00048428 File Offset: 0x00047428
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00048498 File Offset: 0x00047498
		internal byte[] Blob
		{
			get
			{
				int a_ = 7;
				for (;;)
				{
					if (true)
					{
					}
					if (this.ᜅ == PropertyType.Blob)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_49;
					}
				}
				return (byte[])this.ᜄ;
				IL_49:
				if (false)
				{
				}
				throw new InvalidCastException(ClipboardData.b("⹬๮ὰ呲Ŵ坶᩸ᑺ፼ॾꞆﾈ搜뎒릘\ud99a쎠趢", a_));
			}
			set
			{
				int a_ = 14;
				while (value != null)
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
						this.ᜅ = PropertyType.Blob;
						this.ᜄ = value;
						return;
					}
				}
				throw new ArgumentNullException(ClipboardData.b("ɳ᝵ᑷཹ᥻", a_));
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00048504 File Offset: 0x00047504
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00048574 File Offset: 0x00047574
		public ClipboardData ClipboardData
		{
			get
			{
				int a_ = 12;
				while (this.ᜅ != PropertyType.ClipboardData)
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
						throw new InvalidCastException(ClipboardData.b("ㅱᕳᡵ彷๹屻ᵽ慎ﺉ겋ﺑ뢗뺝캡춣횥쪧얩춫\udcad풯햳습\ud9b7钹", a_));
					}
				}
				return (ClipboardData)this.ᜄ;
			}
			set
			{
				int a_ = 8;
				while (value != null)
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
						this.ᜅ = PropertyType.ClipboardData;
						this.ᜄ = value;
						return;
					}
				}
				throw new ArgumentNullException(ClipboardData.b("ᡭᅯṱų፵", a_));
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000485E0 File Offset: 0x000475E0
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00048654 File Offset: 0x00047654
		internal string[] StringArray
		{
			get
			{
				int a_ = 4;
				while (this.ᜅ != PropertyType.StringArray)
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
						throw new InvalidCastException(ClipboardData.b("⥩൫m坯ٱ味ᕵ᝷ᑹ੻᭽ꒃ曆낏ﮓ뚕聯벛ﾝ튟킡얣\udfa5袧얩쪫躭쎯욱욳\udfb5횷\uddb9쾻邽", a_));
					}
				}
				if (true)
				{
				}
				return (string[])this.ᜄ;
			}
			set
			{
				int a_ = 13;
				while (value != null)
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
						this.ᜅ = PropertyType.StringArray;
						this.ᜄ = value;
						return;
					}
				}
				throw new ArgumentNullException(ClipboardData.b("ղᑴ᭶౸Ṻ", a_));
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x000486C4 File Offset: 0x000476C4
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00048738 File Offset: 0x00047738
		internal object[] ObjectArray
		{
			get
			{
				int a_ = 10;
				while (this.ᜅ != PropertyType.ObjectArray)
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
						throw new InvalidCastException(ClipboardData.b("㍯፱ᩳ兵౷婹ύᅽﲇꪉ懲ﲏ뚕벛ﾝ캟芡얣풥\udaa7쮩햫躭\udfaf풱钳억첷좹햻킽ꞿ뇁", a_));
					}
				}
				return (object[])this.ᜄ;
			}
			set
			{
				int a_ = 3;
				while (value != null)
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
						this.ᜅ = PropertyType.ObjectArray;
						this.ᜄ = value;
						return;
					}
				}
				if (true)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("Ὠ੪Ŭᩮᑰ", a_));
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x000487A8 File Offset: 0x000477A8
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x000487EC File Offset: 0x000477EC
		internal PropertyType PropertyType
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00048830 File Offset: 0x00047830
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00048874 File Offset: 0x00047874
		internal string LinkSource
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
				int a_ = 17;
				while (!this.IsBuiltIn)
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
						this.ᜆ = value;
						this.ᜇ = true;
						return;
					}
				}
				throw new InvalidOperationException(ClipboardData.b("⍶ᅸቺ๼彾ﾊﾐ뎒벚뾞쎠욢薤힦첨\ud9aa쮬삮쎰\udeb2킴펶馸풺펼龾ꏀ뛂계ꯆ뷈꓌ꇎꏒ꟔룖꧘뻚꿜ꯞ飠췢", a_));
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000488E4 File Offset: 0x000478E4
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00048928 File Offset: 0x00047928
		internal bool LinkToContent
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0004896C File Offset: 0x0004796C
		internal string InternalName
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
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000489B0 File Offset: 0x000479B0
		public bool ToBool()
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
			return (bool)this.ᜄ;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000489F8 File Offset: 0x000479F8
		public DateTime ToDateTime()
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
			return ((DateTime)this.ᜄ).Date;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00048A48 File Offset: 0x00047A48
		public float ToFloat()
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
			return Convert.ToSingle(this.ᜄ);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00048A90 File Offset: 0x00047A90
		public double ToDouble()
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
			return (double)this.ᜄ;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00048AD8 File Offset: 0x00047AD8
		public int ToInt()
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
			return (int)this.ᜄ;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00048B20 File Offset: 0x00047B20
		public override string ToString()
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
			return (string)this.ᜄ;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00048B68 File Offset: 0x00047B68
		public byte[] ToByteArray()
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
			return (byte[])this.ᜄ;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00048BB0 File Offset: 0x00047BB0
		internal sprᱵ ᜐ()
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
			return (sprᱵ)this.ᜄ;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00048BF8 File Offset: 0x00047BF8
		internal bool ᜀ(spr\u2097 A_0, int A_1)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 5;
				object a_3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_143;
					case 1:
						if (this.IsBuiltIn)
						{
							num = 11;
							continue;
						}
						goto IL_1F6;
					case 2:
						if (this.IsBuiltIn)
						{
							num = 9;
							continue;
						}
						A_0.ᜀ(A_1);
						num = 3;
						continue;
					case 3:
						goto IL_143;
					case 4:
						if (A_0.ᜃ() == 10)
						{
							num = 6;
							continue;
						}
						goto IL_1F6;
					case 6:
						num = 13;
						continue;
					case 7:
						if (this.ᜄ == null)
						{
							if (true)
							{
							}
							num = 12;
							continue;
						}
						num = 2;
						continue;
					case 8:
						goto IL_71;
					case 9:
					{
						bool flag;
						int a_2 = this.ᜀ(this.ᜂ, out flag);
						A_0.ᜀ(a_2);
						num = 0;
						continue;
					}
					case 10:
						goto IL_1A6;
					case 11:
						num = 4;
						continue;
					case 12:
						return false;
					case 13:
						if (!(this.ᜄ is TimeSpan))
						{
							goto IL_1F6;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_68;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 14:
						a_3 = DateTime.FromBinary(((TimeSpan)this.ᜄ).Ticks + 504911232000000000L);
						num = 10;
						continue;
					}
					goto IL_65;
					IL_68:
					num = 8;
					continue;
					IL_65:
					if (A_0 == null)
					{
						goto IL_68;
					}
					num = 7;
					continue;
					IL_143:
					a_3 = this.ᜄ;
					num = 1;
				}
				IL_71:
				throw new ArgumentNullException(ClipboardData.b("Ὠ੪ὬٮၰᵲŴ", a_));
				IL_1A6:
				IL_1F6:
				return A_0.ᜀ(a_3, this.ᜅ);
			}
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00048E08 File Offset: 0x00047E08
		internal int ᜀ(BuiltInProperty A_0, out bool A_1)
		{
			int num;
			for (;;)
			{
				num = (int)A_0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_5E;
					case 1:
						if (true)
						{
						}
						num -= 998;
						A_1 = false;
						num2 = 0;
						continue;
					case 2:
						if (num >= 1000)
						{
							num2 = 1;
							continue;
						}
						goto IL_3C;
					case 3:
						goto IL_5E;
					}
					break;
					IL_3C:
					A_1 = true;
					num2 = 3;
					continue;
					IL_5E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						goto IL_74;
					}
				}
			}
			IL_74:
			if (false)
			{
			}
			return num;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00048E9C File Offset: 0x00047E9C
		internal static PropertyType ᜀ(object A_0)
		{
			PropertyType result;
			for (;;)
			{
				result = PropertyType.Null;
				int num = 25;
				for (;;)
				{
					PropertyType propertyType;
					switch (num)
					{
					case 0:
						return result;
					case 1:
						return result;
					case 2:
						if (A_0 is double)
						{
							num = 18;
							continue;
						}
						num = 13;
						continue;
					case 3:
						if (A_0 is ClipboardData)
						{
							num = 21;
							continue;
						}
						return result;
					case 4:
						result = PropertyType.Blob;
						num = 14;
						continue;
					case 5:
						num = 32;
						continue;
					case 6:
						propertyType = PropertyType.String;
						goto IL_26A;
					case 7:
						return result;
					case 8:
						result = PropertyType.ObjectArray;
						num = 7;
						continue;
					case 9:
						goto IL_10F;
					case 10:
						result = PropertyType.Int32;
						num = 30;
						continue;
					case 11:
						if (A_0 is string[])
						{
							num = 31;
							continue;
						}
						num = 27;
						continue;
					case 12:
						num = 15;
						continue;
					case 13:
						if (A_0 is int)
						{
							num = 10;
							continue;
						}
						num = 23;
						continue;
					case 14:
						return result;
					case 15:
						if (true)
						{
						}
						if (A_0 is TimeSpan)
						{
							num = 9;
							continue;
						}
						num = 28;
						continue;
					case 16:
						return result;
					case 17:
						return result;
					case 18:
						result = PropertyType.Double;
						num = 0;
						continue;
					case 19:
						if (!(A_0 is DateTime))
						{
							num = 12;
							continue;
						}
						goto IL_319;
					case 20:
						num = 6;
						continue;
					case 21:
						result = PropertyType.ClipboardData;
						num = 1;
						continue;
					case 22:
						result = PropertyType.Bool;
						num = 24;
						continue;
					case 23:
						if (A_0 is bool)
						{
							num = 22;
							continue;
						}
						num = 19;
						continue;
					case 24:
						return result;
					case 25:
						if (!(A_0 is string))
						{
							num = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10F;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 26:
						return result;
					case 27:
						if (A_0 is byte[])
						{
							num = 4;
							continue;
						}
						num = 3;
						continue;
					case 28:
						if (A_0 is object[])
						{
							num = 8;
							continue;
						}
						num = 11;
						continue;
					case 29:
						propertyType = PropertyType.AsciiString;
						goto IL_26A;
					case 30:
						return result;
					case 31:
						result = PropertyType.StringArray;
						num = 26;
						continue;
					case 32:
						if (Encoding.UTF8.GetByteCount(A_0 as string) != (A_0 as string).Length)
						{
							num = 20;
							continue;
						}
						num = 29;
						continue;
					}
					break;
					IL_26A:
					result = propertyType;
					num = 17;
					continue;
					IL_319:
					result = PropertyType.DateTime;
					num = 16;
					continue;
					IL_10F:
					goto IL_319;
				}
			}
			return result;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000491D4 File Offset: 0x000481D4
		private void ᜁ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_21D;
				case 2:
					goto IL_EF;
				case 3:
					goto IL_1DD;
				case 4:
					if (this.ᜄ is DateTime)
					{
						num = 6;
						continue;
					}
					num = 17;
					continue;
				case 5:
					goto IL_128;
				case 6:
					goto IL_1AD;
				case 7:
					goto IL_1F5;
				case 8:
					goto IL_74;
				case 9:
					if (this.ᜄ is ClipboardData)
					{
						num = 12;
						continue;
					}
					return;
				case 10:
					if (this.ᜄ is string[])
					{
						num = 18;
						continue;
					}
					num = 14;
					continue;
				case 11:
					if (this.ᜄ is bool)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 12:
					this.ᜅ = PropertyType.ClipboardData;
					num = 7;
					continue;
				case 13:
					if (this.ᜄ is int)
					{
						num = 2;
						continue;
					}
					num = 11;
					continue;
				case 14:
					if (this.ᜄ is byte[])
					{
						num = 3;
						continue;
					}
					num = 9;
					continue;
				case 15:
					goto IL_C4;
				case 16:
					if (true)
					{
					}
					if (this.ᜄ is double)
					{
						num = 5;
						continue;
					}
					num = 13;
					continue;
				case 17:
					if (this.ᜄ is object[])
					{
						num = 15;
						continue;
					}
					num = 10;
					continue;
				case 18:
					goto IL_9C;
				}
				if (this.ᜄ is string)
				{
					num = 8;
				}
				else
				{
					num = 16;
				}
			}
			IL_74:
			this.ᜅ = this.ᜀ((string)this.ᜄ);
			return;
			IL_9C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_23F:
				this.ᜅ = PropertyType.Bool;
				return;
			default:
				if (false)
				{
				}
				this.ᜅ = PropertyType.StringArray;
				return;
			}
			IL_C4:
			this.ᜅ = PropertyType.ObjectArray;
			return;
			IL_EF:
			this.ᜅ = PropertyType.Int32;
			return;
			IL_128:
			this.ᜅ = PropertyType.Double;
			return;
			IL_1AD:
			this.ᜅ = PropertyType.DateTime;
			return;
			IL_1DD:
			this.ᜅ = PropertyType.Blob;
			return;
			IL_1F5:
			return;
			IL_21D:
			goto IL_23F;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00049440 File Offset: 0x00048440
		internal void ᜀ(spr\u2097 A_0)
		{
			int a_ = 17;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0.ᜁ() != VarEnum.VT_LPWSTR)
					{
						num = 0;
						continue;
					}
					goto IL_CF;
				case 4:
					if (A_0.ᜁ() != VarEnum.VT_LPSTR)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_CF;
				case 5:
					goto IL_62;
				}
				if (A_0 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BB;
					default:
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
				else
				{
					num = 4;
				}
			}
			IL_62:
			throw new ArgumentNullException(ClipboardData.b("ŶᡸॺᑼṾ", a_));
			IL_7E:
			IL_BB:
			throw new ArgumentOutOfRangeException(ClipboardData.b("㭶ၸᕺᙼⱾ", a_));
			IL_CF:
			this.LinkSource = A_0.ᜀ().ToString();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00049530 File Offset: 0x00048530
		public DocumentProperty Clone()
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
			DocumentProperty documentProperty = (DocumentProperty)base.MemberwiseClone();
			documentProperty.ᜀ();
			return documentProperty;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00049580 File Offset: 0x00048580
		private void ᜀ()
		{
			int num = 3;
			for (;;)
			{
				PropertyType propertyType;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (propertyType <= PropertyType.ClipboardData)
					{
						goto IL_81;
					}
					num = 7;
					continue;
				case 2:
					num = 11;
					continue;
				case 4:
					return;
				case 5:
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						if (propertyType != PropertyType.ClipboardData)
						{
							num = 5;
							continue;
						}
						goto IL_151;
					}
					break;
				case 7:
					if (propertyType != PropertyType.ObjectArray)
					{
						num = 9;
						continue;
					}
					goto IL_B8;
				case 8:
					if (propertyType != PropertyType.StringArray)
					{
						num = 4;
						continue;
					}
					goto IL_8B;
				case 9:
					num = 8;
					continue;
				case 10:
					num = 6;
					continue;
				case 11:
					if (propertyType != PropertyType.Blob)
					{
						num = 10;
						continue;
					}
					goto IL_9E;
				}
				if (this.ᜄ == null)
				{
					num = 0;
					continue;
				}
				propertyType = this.ᜅ;
				num = 1;
				continue;
				IL_81:
				num = 2;
			}
			return;
			IL_8B:
			this.ᜄ = sprᰓ.ᜀ(this.StringArray);
			return;
			IL_9E:
			if (true)
			{
			}
			this.ᜄ = sprᰓ.ᜀ(this.Blob);
			return;
			IL_B8:
			this.ᜄ = sprᰓ.ᜀ(this.ObjectArray);
			return;
			IL_151:
			this.ᜄ = sprᰓ.ᜀ(this.ClipboardData);
		}

		// Token: 0x04000DC8 RID: 3528
		private const int ᜀ = 1000;

		// Token: 0x04000DC9 RID: 3529
		private const int ᜁ = 1600;

		// Token: 0x04000DCA RID: 3530
		private string \u2460\u0087\u00A4\u0084;

		// Token: 0x04000DCB RID: 3531
		private BuiltInProperty ᜂ;

		// Token: 0x04000DCC RID: 3532
		private string ᜃ;

		// Token: 0x04000DCD RID: 3533
		private object ᜄ;

		// Token: 0x04000DCE RID: 3534
		private PropertyType ᜅ;

		// Token: 0x04000DCF RID: 3535
		private string ᜆ;

		// Token: 0x04000DD0 RID: 3536
		private bool ᜇ;
	}
}
