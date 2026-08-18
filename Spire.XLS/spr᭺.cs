using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Spire.CompoundFile.XLS;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlReaders.Shapes;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x02000468 RID: 1128
internal class spr\u1B7A
{
	// Token: 0x060044AF RID: 17583 RVA: 0x0028EBF0 File Offset: 0x0028DBF0
	public spr\u1B7A(XlsWorkbook A_0)
	{
		int a_ = 13;
		this.ᡊ = new Dictionary<int, spr\u2175>();
		this.ᡋ = new Dictionary<int, spr\u2175>();
		this.ᡌ = new Dictionary<Type, spr\u2175>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("⅂⩄⡆≈", a_));
		}
		this.ᡇ = A_0;
		this.ᡈ = new FormulaUtil(this.ᡇ.AppImplementation, this.ᡇ, NumberFormatInfo.InvariantInfo, ',', ';');
		this.ᡉ = new RecordExtractor();
		this.ᡊ.Add(202, new spr\u2104());
		this.ᡊ.Add(201, new spr\u214F());
		this.ᡊ.Add(75, new spr\u22AC());
		this.ᡋ.Add(75, new spr᠙());
		this.ᡌ.Add(typeof(XlsBitmapShape), new spr\u1C8A());
		this.ᡌ.Add(typeof(ExcelPicture), new spr\u1C8A());
		this.ᡌ.Add(typeof(XlsChartShape), new spr\u21A8());
		this.ᡌ.Add(typeof(Chart), new spr\u21A8());
		this.ᡌ.Add(typeof(XlsTextBoxShape), new sprᴙ());
	}

	// Token: 0x060044B0 RID: 17584 RVA: 0x0028ED50 File Offset: 0x0028DD50
	public Dictionary<int, spr\u2175> ᜃ()
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
		return this.ᡋ;
	}

	// Token: 0x060044B1 RID: 17585 RVA: 0x0028ED94 File Offset: 0x0028DD94
	public Dictionary<int, spr\u2175> ᜄ()
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
		return this.ᡊ;
	}

	// Token: 0x060044B2 RID: 17586 RVA: 0x0028EDD8 File Offset: 0x0028DDD8
	public virtual ExcelVersion ᜀ()
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
		return ExcelVersion.Version2007;
	}

	// Token: 0x060044B3 RID: 17587 RVA: 0x0028EE14 File Offset: 0x0028DE14
	internal XlsWorksheet ᜅ()
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
		return this.ᡎ;
	}

	// Token: 0x060044B4 RID: 17588 RVA: 0x0028EE58 File Offset: 0x0028DE58
	public void ᜀ(XmlWriter A_0, IDictionary<string, string> A_1, IDictionary<string, string> A_2)
	{
		int a_ = 14;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3F;
			case 1:
				goto IL_57;
			case 2:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				goto IL_E4;
			case 4:
				goto IL_DF;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 2;
			}
		}
		IL_3F:
		goto IL_6D;
		IL_57:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❃⥅♇㹉⥋⁍⑏ᵑ≓㍕⩗⡙㕛㩝՟ᅡ", a_));
		}
		IL_DF:
		throw new ArgumentNullException(RecordTableEnumerator.b("❃⥅♇㹉⥋⁍⑏ᙑㅓさ㥗⽙せ⩝፟", a_));
		IL_E4:
		A_0.WriteStartDocument(true);
		A_0.WriteStartElement(RecordTableEnumerator.b("၃㽅㡇⽉㽋", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅ릕ꪗꪙ겛ꢝ辟송쮣좥\udca7쾩슫\udaad鶯욱춳욵\uddb7즹", a_));
		this.ᜀ(A_0, A_1, RecordTableEnumerator.b("C⍅⹇⭉㥋≍⑏", a_), RecordTableEnumerator.b("Ń㹅㱇⽉≋㵍㥏㵑㩓", a_), RecordTableEnumerator.b("݃⥅♇㹉⥋⁍⑏ّⵓ♕㵗", a_), null);
		this.ᜀ(A_0, A_2, RecordTableEnumerator.b("ୃぅⵇ㡉㹋❍㑏㝑", a_), RecordTableEnumerator.b("ᑃ❅㩇㹉ɋ⽍㵏㝑", a_), RecordTableEnumerator.b("݃⥅♇㹉⥋⁍⑏ّⵓ♕㵗", a_), new spr\u23DF());
		A_0.WriteEndElement();
	}

	// Token: 0x060044B5 RID: 17589 RVA: 0x0028EFE4 File Offset: 0x0028DFE4
	public void ᜀ(XmlWriter A_0, Stream A_1, Stream A_2, List<Dictionary<string, string>> A_3, RelationsCollection A_4, Dictionary<XlsPivotCache, string> A_5, Stream A_6)
	{
		int a_ = 19;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		}
		IL_50:
		A_0.WriteStartElement(RecordTableEnumerator.b("㹈⑊㽌⑎㍐㱒㩔㱖", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﺌﾎ爵햠캢즤袦鮨鮪鶬馮麰\udeb2풴\udeb6ힸ", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("ㅈ♊⅌ⅎ≐", a_), RecordTableEnumerator.b("㭈", a_), null, RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌", a_));
		this.ᜀ(A_0, this.ᡇ.DataHolder.\u171B());
		this.ᜌ(A_0);
		this.ᜋ(A_0);
		this.ᜀ(A_0, A_3);
		this.ᜇ(A_0);
		this.ᜀ(A_0, A_6);
		this.ᜀ(A_0, A_4);
		this.ᜑ(A_0);
		this.\u170D(A_0);
		this.ᜀ(A_0, A_5, A_4);
		this.ᜀ(A_0, A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x0028F104 File Offset: 0x0028E104
	private void \u170D(XmlWriter A_0)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("≀≂⥄⑆᥈㥊", a_));
		bool flag = !this.ᡇ.IsDisplayPrecision;
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("❀㙂⥄⭆᥈㥊⡌ⱎ㡐⁒㱔㡖㝘", a_), flag, !flag);
		A_0.WriteAttributeString(RecordTableEnumerator.b("≀≂⥄⑆H⽊", a_), this.ᡇ.DataHolder.\u1719());
		A_0.WriteEndElement();
	}

	// Token: 0x060044B7 RID: 17591 RVA: 0x0028F1B0 File Offset: 0x0028E1B0
	private void ᜌ(XmlWriter A_0)
	{
		int a_ = 9;
		for (;;)
		{
			bool date = this.ᡇ.Date1904;
			A_0.WriteStartElement(RecordTableEnumerator.b("䠾⹀ㅂ⹄╆♈⑊♌὎⍐", a_));
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("嬾⁀㝂⁄癆灈筊祌", a_), this.ᡇ.Date1904, false);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᡇ.CodeName != null)
					{
						num = 2;
						continue;
					}
					goto IL_D6;
				case 1:
					goto IL_D4;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("尾⹀❂⁄ॆ⡈♊⡌", a_), this.ᡇ.CodeName);
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_D4:
		IL_D6:
		A_0.WriteEndElement();
	}

	// Token: 0x060044B8 RID: 17592 RVA: 0x0028F29C File Offset: 0x0028E29C
	private void ᜀ(XmlWriter A_0, Dictionary<XlsPivotCache, string> A_1, RelationsCollection A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				XlsPivotCachesCollection xlsPivotCachesCollection;
				int num2;
				int num4;
				Dictionary<string, string>.Enumerator enumerator2;
				sprវ sprវ;
				switch (num)
				{
				case 0:
					if (xlsPivotCachesCollection == null)
					{
						num = 17;
						continue;
					}
					num = 1;
					continue;
				case 1:
					num2 = xlsPivotCachesCollection.Count;
					goto IL_2B0;
				case 3:
					try
					{
						num = 3;
						for (;;)
						{
							string arg;
							string text;
							int num3;
							switch (num)
							{
							case 0:
								goto IL_228;
							case 1:
							{
								IEnumerator<XlsPivotCache> enumerator;
								if (!enumerator.MoveNext())
								{
									num = 6;
									continue;
								}
								XlsPivotCache xlsPivotCache = enumerator.Current;
								arg = A_1[xlsPivotCache];
								text = A_2.GenerateRelationId();
								num = 2;
								continue;
							}
							case 2:
							{
								if (this.ᡇ.Options == ExcelParseOptions.DoNotParsePivotTable)
								{
									num = 4;
									continue;
								}
								XlsPivotCache xlsPivotCache;
								num3 = xlsPivotCache.Index + 1;
								num = 8;
								continue;
							}
							case 4:
							{
								XlsPivotCache xlsPivotCache;
								num3 = xlsPivotCache.Index;
								num = 7;
								continue;
							}
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1B7;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 7:
								goto IL_1B7;
							case 8:
								goto IL_1B9;
							}
							IL_123:
							num = 1;
							continue;
							goto IL_123;
							IL_1B9:
							this.ᜁ(A_0, num3.ToString(), text);
							A_2[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ᕹ᩻᡽슅曆ﲑ릕ꪗꪙ겛ꢝ辟킡솣쪥즧\udea9얫솭\udeaf솱\udcb3\udfb5좷즹鎻캽ꦿ듁ꯃ닅诇ꯉ꿋ꛍ뗏雑뇓냕뇗듙뗛ꫝ觟跡諣", a_));
							num = 5;
							continue;
							IL_1B7:
							goto IL_1B9;
						}
						IL_228:
						goto IL_406;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							IEnumerator<XlsPivotCache> enumerator;
							switch (num)
							{
							case 0:
								goto IL_26A;
							case 1:
								enumerator.Dispose();
								num = 0;
								continue;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 1;
						}
						IL_26A:;
					}
					goto IL_26D;
				case 4:
					if (true)
					{
					}
					if (A_2 == null)
					{
						num = 12;
						continue;
					}
					goto IL_26D;
				case 5:
					if (num4 <= 0)
					{
						num = 15;
						continue;
					}
					goto IL_3CA;
				case 6:
				{
					IEnumerator<XlsPivotCache> enumerator = xlsPivotCachesCollection.GetEnumerator();
					num = 3;
					continue;
				}
				case 7:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 2;
								continue;
							case 2:
								goto IL_357;
							case 3:
							{
								if (!enumerator2.MoveNext())
								{
									num = 0;
									continue;
								}
								KeyValuePair<string, string> keyValuePair = enumerator2.Current;
								this.ᜁ(A_0, keyValuePair.Key, keyValuePair.Value);
								num = 1;
								continue;
							}
							}
							IL_331:
							num = 3;
							continue;
							goto IL_331;
						}
						IL_357:
						goto IL_37B;
					}
					finally
					{
						((IDisposable)enumerator2).Dispose();
					}
					goto IL_367;
					IL_37B:
					num = 11;
					continue;
				case 8:
					goto IL_438;
				case 9:
					if (sprវ.ᜢ().Count > 0)
					{
						num = 14;
						continue;
					}
					return;
				case 10:
					goto IL_418;
				case 11:
					if (num4 > 0)
					{
						num = 6;
						continue;
					}
					goto IL_406;
				case 12:
					goto IL_E9;
				case 13:
					goto IL_84;
				case 14:
					goto IL_3CA;
				case 15:
					num = 9;
					continue;
				case 16:
					num2 = 0;
					goto IL_2B0;
				case 17:
					num = 16;
					continue;
				case 18:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					num = 4;
					continue;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 18;
				continue;
				IL_26D:
				xlsPivotCachesCollection = this.ᡇ.PivotCaches;
				num = 0;
				continue;
				IL_2B0:
				num4 = num2;
				sprវ = this.ᡇ.DataHolder;
				num = 5;
				continue;
				IL_3CA:
				A_0.WriteStartElement(RecordTableEnumerator.b("䘵儷䰹医䨽̿⍁❃⹅ⵇ㥉", a_));
				enumerator2 = sprវ.ᜢ().GetEnumerator();
				num = 7;
				continue;
				IL_406:
				A_0.WriteEndElement();
				num = 10;
			}
			IL_84:
			goto IL_367;
			IL_E9:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷嘹崻䨽⤿ⵁ⩃㕅", a_));
			IL_367:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_418:
			return;
			IL_438:
			throw new ArgumentNullException(RecordTableEnumerator.b("唵夷夹吻嬽ؿ⭁⡃⍅㭇", a_));
		}
		}
	}

	// Token: 0x060044B9 RID: 17593 RVA: 0x0028F734 File Offset: 0x0028E734
	private void ᜁ(XmlWriter A_0, string A_1, string A_2)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("丽⤿㑁⭃㉅େ⭉⽋♍㕏", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("崽ℿ⅁ⱃ⍅Ň⹉", a_), A_1);
		A_0.WriteAttributeString(RecordTableEnumerator.b("圽␿", a_), RecordTableEnumerator.b("嘽㐿㙁㑃籅杇敉㽋ⵍ㡏㝑㥓㝕⭗瑙㍛⹝՟ౡᱣ୥ѧ౩ͫᱭᵯ፱sյ噷ᕹ๻᥽꽿쪍ﾏﮕﶗ놝銟銡钣邥螧\ud8a9즫슭톯욱\uddb3\ud9b5횷즹풻ힽ낿뇁", a_), A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x060044BA RID: 17594 RVA: 0x0028F7CC File Offset: 0x0028E7CC
	private void ᜀ(XmlWriter A_0, FileVersion A_1)
	{
		int a_ = 15;
		for (;;)
		{
			if (true)
			{
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("⍄⹆╈⹊ᭌ⩎⍐⁒㱔㡖㝘", a_));
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_18E;
				case 1:
					A_0.WriteAttributeString(RecordTableEnumerator.b("♄⡆ⵈ⹊͌⹎㱐㙒", a_), A_1.CodeName);
					num = 8;
					continue;
				case 2:
					if (A_1.ApplicationName != null)
					{
						num = 11;
						continue;
					}
					goto IL_D4;
				case 3:
					goto IL_D4;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F1;
					}
					if (false)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥄⡆㹈⹊㹌㭎ᑐ㝒㱔⍖㱘㽚", a_), A_1.LowestEdited);
					num = 7;
					continue;
				case 5:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⥄♆㩈㽊ࡌ⭎㡐❒ご㍖", a_), A_1.LastEdited);
					goto IL_1F1;
				case 6:
					if (A_1.LowestEdited != null)
					{
						num = 4;
						continue;
					}
					goto IL_144;
				case 7:
					goto IL_144;
				case 8:
					goto IL_18C;
				case 9:
					if (A_1.CodeName != null)
					{
						num = 1;
						continue;
					}
					goto IL_1FE;
				case 10:
					if (A_1.LastEdited != null)
					{
						num = 5;
						continue;
					}
					goto IL_18E;
				case 11:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⑄㝆㥈Պⱌ≎㑐", a_), A_1.ApplicationName);
					num = 3;
					continue;
				case 12:
					goto IL_FA;
				case 13:
					A_0.WriteAttributeString(RecordTableEnumerator.b("㝄㉆㥈ॊ㡌♎㵐㝒", a_), A_1.BuildVersion);
					num = 12;
					continue;
				case 14:
					if (A_1.BuildVersion != null)
					{
						num = 13;
						continue;
					}
					goto IL_FA;
				}
				break;
				IL_D4:
				num = 10;
				continue;
				IL_FA:
				num = 9;
				continue;
				IL_144:
				num = 14;
				continue;
				IL_18E:
				num = 6;
				continue;
				IL_1F1:
				num = 0;
			}
		}
		IL_18C:
		IL_1FE:
		A_0.WriteEndElement();
	}

	// Token: 0x060044BB RID: 17595 RVA: 0x0028F9E0 File Offset: 0x0028E9E0
	private void ᜋ(XmlWriter A_0)
	{
		int a_ = 3;
		int num = 5;
		for (;;)
		{
			spr\u24C3 spr_u24C;
			switch (num)
			{
			case 0:
				if (this.ᡇ.IsCellProtection)
				{
					num = 10;
					continue;
				}
				return;
			case 1:
				num = 0;
				continue;
			case 2:
			{
				ushort num2 = spr_u24C.ᜀ();
				num = 8;
				continue;
			}
			case 3:
				A_0.WriteAttributeString(RecordTableEnumerator.b("丸吺似吾⍀ⱂ⩄ⱆ᥈⩊㹌㱎♐㱒❔㍖", a_), spr_u24C.ᜀ().ToString(RecordTableEnumerator.b("愸༺", a_)));
				num = 11;
				continue;
			case 4:
				if (!this.ᡇ.IsWindowProtection)
				{
					num = 1;
					continue;
				}
				goto IL_7B;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A3;
				default:
					goto IL_15C;
				}
				break;
			case 7:
				if (spr_u24C != null)
				{
					num = 2;
					continue;
				}
				goto IL_F0;
			case 8:
			{
				ushort num2;
				if (num2 != 0)
				{
					num = 3;
					continue;
				}
				goto IL_F0;
			}
			case 9:
				goto IL_141;
			case 10:
				goto IL_7B;
			case 11:
				goto IL_F0;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 4;
			continue;
			IL_A3:
			num = 7;
			continue;
			IL_7B:
			if (true)
			{
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("丸吺似吾⍀ⱂ⩄ⱆ᥈㥊≌㭎㑐げ⅔㹖㙘㕚", a_));
			spr_u24C = this.ᡇ.Password;
			goto IL_A3;
			IL_F0:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("唸吺帼吾ቀ㝂㝄㉆⩈㽊㡌㵎㑐", a_), this.ᡇ.IsCellProtection, false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("唸吺帼吾ᙀ⩂⭄⍆♈㱊㹌", a_), this.ᡇ.IsWindowProtection, false);
			A_0.WriteEndElement();
			num = 9;
		}
		IL_141:
		return;
		IL_15C:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
	}

	// Token: 0x060044BC RID: 17596 RVA: 0x0028FBCC File Offset: 0x0028EBCC
	public void ᜀ(XmlWriter A_0, spr\u1FBC A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					List<Rectangle> list;
					if (list != null)
					{
						num = 9;
						continue;
					}
					return;
				}
				case 1:
					goto IL_11D;
				case 2:
					goto IL_17D;
				case 3:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					List<Rectangle> list;
					Rectangle a_2 = list[num2];
					spr\u25A6.ᜀ a_3 = A_1.ᜄ(a_2);
					A_0.WriteStartElement(RecordTableEnumerator.b("⭅ⵇ㡉⭋⭍ፏ㝑㡓㩕", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㑅ⵇⱉ", a_), this.ᜀ(a_3));
					A_0.WriteEndElement();
					num2++;
					num = 2;
					continue;
				}
				case 4:
				{
					List<Rectangle> list;
					if (list.Count == 0)
					{
						num = 5;
						continue;
					}
					int count = list.Count;
					A_0.WriteStartElement(RecordTableEnumerator.b("⭅ⵇ㡉⭋⭍ፏ㝑㡓㩕⭗", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("╅❇㽉≋㩍", a_), count.ToString());
					int num2 = 0;
					num = 8;
					continue;
				}
				case 5:
					goto IL_FD;
				case 6:
				{
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					List<Rectangle> list = A_1.ᜄ();
					num = 0;
					continue;
				}
				case 7:
					goto IL_199;
				case 8:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FD;
					default:
						if (false)
						{
						}
						goto IL_17D;
					}
					break;
				case 9:
					num = 4;
					continue;
				case 11:
					return;
				}
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				num = 6;
				continue;
				IL_17D:
				num = 3;
			}
			return;
			IL_FD:
			return;
			IL_11D:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_199:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x0028FDC0 File Offset: 0x0028EDC0
	public void ᜑ(XmlWriter A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				sprឦ sprឦ;
				int num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					return;
				case 1:
					sprឦ.ᜆ();
					A_0.WriteStartElement(RecordTableEnumerator.b("嬾⑀╂ⱄ⥆ⱈ⽊͌⹎㱐㙒♔", a_));
					num2 = 0;
					num = 14;
					continue;
				case 2:
					IL_20:
					break;
				case 3:
					num3 = 0;
					goto IL_237;
				case 4:
					num = 3;
					continue;
				case 5:
					num = 18;
					continue;
				case 6:
					num3 = sprឦ.ᜊ();
					goto IL_237;
				case 7:
					goto IL_1D7;
				case 8:
				{
					XlsName xlsName;
					this.ᜀ(A_0, xlsName);
					num = 7;
					continue;
				}
				case 9:
				{
					XlsName xlsName;
					if (xlsName.Name != null)
					{
						num = 5;
						continue;
					}
					goto IL_1D7;
				}
				case 10:
				{
					XlsName xlsName;
					if (!xlsName.Record.ᜉ())
					{
						num = 8;
						continue;
					}
					goto IL_1D7;
				}
				case 11:
				{
					if (num2 >= num4)
					{
						num = 19;
						continue;
					}
					XlsName xlsName = (XlsName)sprឦ.ᜁ(num2);
					num = 9;
					continue;
				}
				case 12:
					if (num4 > 0)
					{
						num = 1;
						continue;
					}
					return;
				case 13:
					goto IL_EA;
				case 14:
					goto IL_EA;
				case 15:
					goto IL_88;
				case 16:
					if (sprឦ == null)
					{
						num = 4;
						continue;
					}
					num = 6;
					continue;
				case 17:
					num = 10;
					continue;
				case 18:
				{
					XlsName xlsName;
					if (xlsName.Name.IndexOf('[') == -1)
					{
						num = 17;
						continue;
					}
					goto IL_1D7;
				}
				case 19:
					A_0.WriteEndElement();
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				sprឦ = this.ᡇ.InnerNamesColection;
				num = 16;
				continue;
				IL_EA:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_20;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				IL_1D7:
				num2++;
				num = 13;
				continue;
				IL_237:
				num4 = num3;
				num = 12;
			}
			IL_88:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		}
		}
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x00290028 File Offset: 0x0028F028
	public Dictionary<int, int> ᜀ(XmlWriter A_0, ref Stream A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 8;
			Dictionary<int, int> result;
			for (;;)
			{
				Stream stream;
				int num2;
				int count;
				XlsWorksheetsCollection innerWorksheets;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_69;
				case 1:
					stream.Position = 0L;
					ShapeParser.WriteNodeFromStream(A_0, stream, true);
					num = 7;
					continue;
				case 2:
					if (A_1 != null)
					{
						num = 6;
						continue;
					}
					goto IL_2BA;
				case 3:
					if (this.ᡇ.DataHolder.ᜏ() != -2147483648)
					{
						num = 10;
						continue;
					}
					goto IL_265;
				case 4:
				{
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					XlsWorksheet xlsWorksheet = (XlsWorksheet)innerWorksheets[num2];
					XlsWorksheetConditionalFormats conditionalFormats = xlsWorksheet.ConditionalFormats;
					Stream item = this.ᜀ(ref A_1, conditionalFormats, ref num3);
					this.ᡍ.Add(item);
					num2++;
					num = 12;
					continue;
				}
				case 5:
					if (stream != null)
					{
						num = 1;
						continue;
					}
					goto IL_326;
				case 6:
					goto IL_2F2;
				case 7:
					goto IL_24C;
				case 9:
					goto IL_265;
				case 10:
					goto IL_2BA;
				case 11:
					goto IL_6E;
				case 12:
					goto IL_6E;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				A_0.WriteStartDocument(true);
				A_0.WriteStartElement(RecordTableEnumerator.b("㉀㝂㱄⭆ⱈᡊ╌⩎㑐❒", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﮈﮒ낞鎠鎢閤醦蚨욪첬욮\udfb0", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㥀⹂⥄⥆㩈", a_), RecordTableEnumerator.b("ⱀ⁂", a_), null, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﮈﾎ벐杖殺漢쎠쪢즤캦\udda8튪芬鶮膰莲莴", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ࡀ⑂⭄⡆㭈⩊⽌⍎㑐", a_), RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸啺ቼൾ겂ﮈﾎ벐杖殺漢쎠쪢즤캦\udda8튪芬鶮膰莲莴", a_), RecordTableEnumerator.b("㥀牂煄♆⩈", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㥀⹂⥄⥆㩈", a_), RecordTableEnumerator.b("㥀牂煄♆⩈", a_), null, RecordTableEnumerator.b("⥀㝂ㅄ㝆獈摊扌㱎㉐㭒ご㩖㡘⡚獜㉞ࡠbᝤࡦᩨѪ୬᭮彰ၲᩴ᩶噸ᑺ᭼᥾ꢆ愈ﮊﾌﾖﲘﺚ춠貢鞤鞦馨銪芬隮麰튲횴", a_));
				this.ᜅ(A_0);
				this.ᜆ(A_0);
				int[] a_2 = this.ᜄ(A_0);
				int[] a_3 = this.ᜃ(A_0);
				sprᢖ sprᢖ = this.ᡇ.InnerExtFormats;
				int count2 = sprᢖ.Count;
				Dictionary<int, int> dictionary = this.ᜀ(A_0, a_2, a_3);
				result = this.ᜀ(A_0, a_2, a_3, dictionary);
				this.ᜀ(A_0, dictionary);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2F2;
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
				IL_6E:
				num = 4;
				continue;
				IL_265:
				this.ᜀ(A_0, A_1);
				this.ᜀ(A_0, this.ᡇ.CustomTableStylesStream);
				this.ᜂ(A_0);
				stream = this.ᡇ.DataHolder.\u1712();
				num = 5;
				continue;
				IL_2BA:
				this.ᡍ = new List<Stream>();
				innerWorksheets = this.ᡇ.InnerWorksheets;
				num3 = 0;
				num2 = 0;
				count = innerWorksheets.Count;
				num = 11;
				continue;
				IL_2F2:
				num = 3;
			}
			IL_69:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
			IL_24C:
			IL_326:
			A_0.WriteEndElement();
			return result;
		}
		}
	}

	// Token: 0x060044BF RID: 17599 RVA: 0x00290364 File Offset: 0x0028F364
	public void ᜁ(XmlWriter A_0, RelationsCollection A_1)
	{
		int a_ = 1;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1.Count == 0)
				{
					num = 3;
					continue;
				}
				A_0.WriteStartDocument(true);
				A_0.WriteStartElement(RecordTableEnumerator.b("收尸场尼䬾⡀ⱂ⭄㑆ⅈ≊㵌㱎", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸୺ᱼ᱾Ꚉ릊붌뾎Ꞑ벒漢캠춢횤쾦삨\udbaa\udeac", a_));
				IEnumerator enumerator = A_1.GetEnumerator();
				num = 4;
				continue;
			}
			case 1:
				if (A_1 != null)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				goto IL_193;
			case 3:
				goto IL_1B4;
			case 4:
				try
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_131;
						case 2:
						{
							IEnumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							KeyValuePair<string, sprᦨ> keyValuePair = (KeyValuePair<string, sprᦨ>)enumerator.Current;
							this.ᜀ(A_0, keyValuePair.Key, keyValuePair.Value);
							num = 3;
							continue;
						}
						case 4:
							num = 1;
							continue;
						}
						IL_E4:
						num = 2;
						continue;
						goto IL_E4;
					}
					IL_131:
					goto IL_1B9;
				}
				finally
				{
					for (;;)
					{
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_192;
									case 1:
										goto IL_190;
									case 2:
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								}
								break;
							}
							}
						}
					}
					IL_190:
					IL_192:;
				}
				goto IL_193;
			case 5:
				if (true)
				{
				}
				break;
			case 6:
				goto IL_48;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 1;
			continue;
			IL_193:
			num = 0;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_1B4:
		return;
		IL_1B9:
		A_0.WriteEndElement();
	}

	// Token: 0x060044C0 RID: 17600 RVA: 0x00290540 File Offset: 0x0028F540
	public void ᜀ(XmlWriter A_0, XlsWorksheet A_1, Stream A_2, Stream A_3, Dictionary<int, int> A_4)
	{
		int a_ = 3;
		int num = 35;
		for (;;)
		{
			Stream stream;
			switch (num)
			{
			case 0:
				goto IL_11E;
			case 1:
				A_0.WriteAttributeString(RecordTableEnumerator.b("崸帺嬼帾㑀⽂ㅄц♈❊ᩌ♎㕐❒㵔", a_), XmlConvert.ToString(A_1.DefaultColumnWidth));
				num = 7;
				continue;
			case 2:
				if (A_3.Length == 0L)
				{
					num = 28;
					continue;
				}
				num = 14;
				continue;
			case 3:
				if (A_1.SparklineGroups.Count > 0)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				goto IL_6F4;
			case 4:
				if (A_3 != null)
				{
					num = 26;
					continue;
				}
				goto IL_241;
			case 5:
				goto IL_681;
			case 6:
				if (A_1.CustomHeight)
				{
					num = 34;
					continue;
				}
				goto IL_504;
			case 7:
				goto IL_455;
			case 8:
				goto IL_576;
			case 9:
			{
				spr\u1CC3 spr_u1CC = new spr\u1CC3(this.ᡇ);
				spr_u1CC.ᜁ(A_0, A_1);
				num = 13;
				continue;
			}
			case 10:
				if (A_1.DefaultColumnWidth != 8.43)
				{
					num = 1;
					continue;
				}
				goto IL_455;
			case 11:
				goto IL_59C;
			case 12:
				goto IL_D1;
			case 13:
				goto IL_3FE;
			case 14:
				stream = A_3;
				goto IL_2C1;
			case 15:
				if (A_1.Version == ExcelVersion.Version2010)
				{
					num = 36;
					continue;
				}
				goto IL_6F4;
			case 16:
				if (this.ᡎ.IsZeroHeight)
				{
					num = 31;
					continue;
				}
				goto IL_59C;
			case 17:
				goto IL_FC;
			case 18:
				goto IL_64D;
			case 19:
				A_0.WriteAttributeString(RecordTableEnumerator.b("嘸为䤼匾⡀ⵂ⁄୆ⱈ㵊⡌⍎ቐ㱒㥔", a_), XmlConvert.ToString(A_1.OutlineLevelColumn));
				num = 39;
				continue;
			case 20:
				if (A_1 == null)
				{
					num = 18;
					continue;
				}
				num = 37;
				continue;
			case 21:
				if (A_1.OutlineLevelRow > 0)
				{
					num = 30;
					continue;
				}
				goto IL_FC;
			case 22:
				stream = this.ᜀ(A_1.Index);
				goto IL_2C1;
			case 23:
				goto IL_276;
			case 24:
				goto IL_D6;
			case 25:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䴸区吼尾⩀ł⩄㍆㵈⑊⁌", a_), XmlConvert.ToString(true));
				num = 5;
				continue;
			case 26:
				num = 2;
				continue;
			case 27:
				if (A_1.OutlineLevelColumn > 0)
				{
					num = 19;
					continue;
				}
				goto IL_60A;
			case 28:
				goto IL_241;
			case 29:
				if (A_1.BaseColumnWidth != 8)
				{
					num = 0;
					continue;
				}
				goto IL_576;
			case 30:
				A_0.WriteAttributeString(RecordTableEnumerator.b("嘸为䤼匾⡀ⵂ⁄୆ⱈ㵊⡌⍎͐㱒≔", a_), XmlConvert.ToString(A_1.OutlineLevelRow));
				num = 17;
				continue;
			case 31:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䌸帺似倾ी♂ⱄ⁆ⅈ㽊", a_), XmlConvert.ToString(true));
				num = 11;
				continue;
			case 32:
				goto IL_504;
			case 33:
				if (A_1.IsThickBottom)
				{
					num = 25;
					continue;
				}
				goto IL_681;
			case 34:
				A_0.WriteAttributeString(RecordTableEnumerator.b("娸为丼䬾⹀⹂ൄ≆⁈ⱊ╌㭎", a_), XmlConvert.ToString(true));
				num = 32;
				continue;
			case 36:
				num = 3;
				continue;
			case 37:
				if (A_2 == null)
				{
					num = 23;
					continue;
				}
				this.ᡎ = A_1;
				A_0.WriteStartDocument(true);
				A_0.WriteStartElement(RecordTableEnumerator.b("丸吺似吾㉀⭂⁄≆㵈", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺๼ཾ愈ﺒ璉뢖ꮘꮚ궜ꦞ躠캢쒤캦잨", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("䬸", a_), null, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ቼ᥾춈搜ﲐﮔ뚘ꦚ궜꾞鞠貢힤슦얨쪪\ud9ac욮\udeb0\uddb2운\udfb6킸쮺캼", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("䄸਺़", a_), null, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㩖じ㡚⽜ぞበౢͤ፦䝨ࡪɬɮ幰ᱲ፴ᅶၸ᡺᡼偾ﺌ殺뒚꾜꾞醠骢誤麦蚨욪첬욮\udfb0", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("吸堺", a_), null, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ၼṾꒈﺖﮘ햠\udaa2誤閦馨鮪鮬", a_));
				this.ᜂ(A_0, A_1);
				this.ᜇ(A_0, A_1);
				this.ᜆ(A_0, A_1);
				A_0.WriteStartElement(RecordTableEnumerator.b("䨸区堼娾㕀Ղ⩄㕆⑈⩊㥌὎⍐", a_));
				num = 10;
				continue;
			case 38:
				if (A_1.IsThickTop)
				{
					num = 40;
					continue;
				}
				goto IL_D6;
			case 39:
				goto IL_60A;
			case 40:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䴸区吼尾⩀ᝂ⩄㝆", a_), XmlConvert.ToString(true));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_11E;
				default:
					if (false)
					{
					}
					num = 24;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 20;
			continue;
			IL_D6:
			num = 33;
			continue;
			IL_FC:
			num = 29;
			continue;
			IL_11E:
			A_0.WriteAttributeString(RecordTableEnumerator.b("嬸娺丼娾ɀⱂ⥄၆⁈⽊㥌❎", a_), XmlConvert.ToString(A_1.BaseColumnWidth));
			num = 8;
			continue;
			IL_241:
			num = 22;
			continue;
			IL_2C1:
			Stream a_2 = stream;
			this.ᜀ(A_0, a_2);
			this.ᜀ(A_0, A_1.DVTable);
			this.ᜃ(A_0, A_1);
			spr\u171C a_3 = new spr\u1CDC();
			spr\u1B7A.ᜄ(A_0, A_1.PageSetup, a_3);
			this.ᜀ(A_0, A_1);
			this.ᜋ(A_0, A_1);
			this.ᜌ(A_0, A_1);
			this.ᜊ(A_0, A_1);
			this.ᜀ(A_0, A_1);
			spr\u1B7A.ᜀ(A_0, A_1, a_3, null);
			this.ᜉ(A_0, A_1);
			this.ᜀ(A_0, A_1);
			this.\u170D(A_0, A_1);
			A_1.DataHolder.ᜀ(A_0, A_1);
			num = 15;
			continue;
			IL_455:
			num = 16;
			continue;
			IL_504:
			A_0.WriteAttributeString(RecordTableEnumerator.b("崸帺嬼帾㑀⽂ㅄᕆ♈㱊Ռ⩎㡐㑒㵔⍖", a_), XmlConvert.ToString(A_1.DefaultRowHeight));
			num = 27;
			continue;
			IL_576:
			num = 38;
			continue;
			IL_59C:
			num = 6;
			continue;
			IL_60A:
			num = 21;
			continue;
			IL_681:
			A_0.WriteEndElement();
			this.ᜀ(A_0, A_1, A_4);
			this.ᜀ(A_0, A_1.CellRecords, A_4, RecordTableEnumerator.b("娸", a_), null, true);
			this.ᜁ(A_0, A_1);
			this.ᜀ(A_0, A_1.AutoFilters);
			this.ᜀ(A_0, A_1.MergeCells);
			num = 4;
		}
		IL_D1:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_276:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似娾⁀⹂ᙄ㍆⡈㥊㥌", a_));
		IL_3FE:
		goto IL_6F4;
		IL_64D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_6F4:
		A_0.WriteEndElement();
	}

	// Token: 0x060044C1 RID: 17601 RVA: 0x00290C48 File Offset: 0x0028FC48
	protected virtual void ᜁ(XmlWriter A_0, XlsWorksheet A_1)
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
	}

	// Token: 0x060044C2 RID: 17602 RVA: 0x00290C84 File Offset: 0x0028FC84
	private void \u170D(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				Stream stream;
				bool flag;
				switch (num)
				{
				case 0:
				{
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					sprᡟ sprᡟ = A_1.DataHolder;
					stream = sprᡟ.ᜐ();
					num = 2;
					continue;
				}
				case 2:
					if (stream != null)
					{
						num = 8;
						continue;
					}
					goto IL_197;
				case 3:
					spr\u1B7A.ᜈ(A_0);
					num = 5;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_141;
					default:
						goto IL_FE;
					}
					break;
				case 5:
					goto IL_118;
				case 6:
					goto IL_E3;
				case 7:
					goto IL_CC;
				case 8:
					flag = spr\u1B7A.ᜀ(A_1.Shapes);
					num = 11;
					continue;
				case 9:
					goto IL_141;
				case 10:
					spr\u1B7A.ᜊ(A_0);
					num = 6;
					continue;
				case 11:
					if (flag)
					{
						num = 3;
						continue;
					}
					goto IL_118;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
				IL_118:
				stream.Position = 0L;
				XmlReader reader = UtilityMethods.ᜀ(stream);
				A_0.WriteNode(reader, false);
				A_0.Flush();
				num = 9;
				continue;
				IL_141:
				if (!flag)
				{
					goto IL_197;
				}
				num = 10;
			}
			IL_CC:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
			IL_E3:
			goto IL_197;
			IL_FE:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_197:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x060044C3 RID: 17603 RVA: 0x00290E30 File Offset: 0x0028FE30
	public static void ᜊ(XmlWriter A_0)
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
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060044C4 RID: 17604 RVA: 0x00290E78 File Offset: 0x0028FE78
	public static void ᜉ(XmlWriter A_0)
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("圹弻", a_), RecordTableEnumerator.b("笹倻䨽┿ぁ⩃❅㱇⽉ཋ⅍㹏♑ㅓ㡕ⱗ", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻፽꞉ﶏ즟횡\udda3覥骧骩鲫颭", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("圹弻", a_), RecordTableEnumerator.b("礹吻儽⤿⅁⅃", a_), RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻፽꞉ﶏ즟횡\udda3覥骧骩鲫颭", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䈹儻刽⸿ㅁ", a_), RecordTableEnumerator.b("嬹഻਽", a_), null, RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㕗㍙㽛ⱝཟᅡୣeᱧ䑩ཫŭᵯ嵱᭳ၵṷ፹ύ᭽꽿ﾇ뾏ꂑ꒓ꞕꢗ떙ﾝ즟첡", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("根夻伽㔿⭁㙃⍅㭇", a_), RecordTableEnumerator.b("嬹഻਽", a_));
	}

	// Token: 0x060044C5 RID: 17605 RVA: 0x00290F70 File Offset: 0x0028FF70
	public static void ᜈ(XmlWriter A_0)
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("吸堺", a_), RecordTableEnumerator.b("砸场䤼娾㍀ⵂ⑄㍆ⱈࡊ≌ⅎ═㙒㭔⍖", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ၼṾꒈﺖﮘ햠\udaa2誤閦馨鮪鮬", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("䄸嘺儼儾㉀", a_), RecordTableEnumerator.b("吸堺", a_), null, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ၼṾꒈﺖﮘ햠\udaa2誤閦馨鮪鮬", a_));
		A_0.WriteStartElement(RecordTableEnumerator.b("吸堺", a_), RecordTableEnumerator.b("稸区刼嘾≀♂", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ၼṾꒈﺖﮘ햠\udaa2誤閦馨鮪鮬", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("欸帺䰼䨾⡀ㅂ⁄㑆", a_), RecordTableEnumerator.b("䄸਺़", a_));
	}

	// Token: 0x060044C6 RID: 17606 RVA: 0x00291068 File Offset: 0x00290068
	internal void ᜁ(XmlWriter A_0, XlsWorksheetBase A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				int num2;
				int num3;
				string[] array;
				switch (num)
				{
				case 0:
				{
					if (num2 >= num3)
					{
						num = 6;
						continue;
					}
					spr\u1B7A.ᜀ ᜀ;
					bool[] array2;
					SheetProtectionType protection;
					ᜀ(A_0, array[num2], sprᱳ.\u171A[num2], array2[num2], protection);
					num2++;
					num = 4;
					continue;
				}
				case 1:
					goto IL_226;
				case 2:
				{
					bool flag;
					if (!flag)
					{
						num = 8;
						continue;
					}
					spr\u1B7A.ᜀ ᜀ = new spr\u1B7A.ᜀ(this.ᜀ);
					array = sprᱳ.\u1717;
					bool[] array2 = sprᱳ.\u1718;
					num = 21;
					continue;
				}
				case 3:
					goto IL_1B7;
				case 4:
					goto IL_226;
				case 5:
					goto IL_24C;
				case 6:
					A_0.WriteEndElement();
					num = 13;
					continue;
				case 7:
					num = 10;
					continue;
				case 8:
				{
					spr\u1B7A.ᜀ ᜀ = new spr\u1B7A.ᜀ(this.ᜁ);
					array = sprᱳ.\u1719;
					bool[] array2 = sprᱳ.\u171B;
					num = 5;
					continue;
				}
				case 9:
				{
					SheetProtectionType protection;
					if (protection != SheetProtectionType.None)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 10:
				{
					bool flag;
					if (flag)
					{
						num = 16;
						continue;
					}
					return;
				}
				case 12:
					if (!A_1.ProtectContents)
					{
						num = 7;
						continue;
					}
					goto IL_1B7;
				case 13:
					return;
				case 14:
				{
					if (A_1 == null)
					{
						num = 17;
						continue;
					}
					SheetProtectionType protection = A_1.Protection;
					bool flag = A_1 is XlsChart;
					num = 12;
					continue;
				}
				case 15:
				{
					string value = A_1.Password.ᜀ().ToString(RecordTableEnumerator.b("怷", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠷嬹伻䴽㜿ⵁ㙃≅", a_), value);
					num = 19;
					continue;
				}
				case 16:
					num = 9;
					continue;
				case 17:
					goto IL_168;
				case 18:
					if (A_1.IsPasswordProtected)
					{
						num = 15;
						continue;
					}
					goto IL_E8;
				case 19:
					goto IL_E8;
				case 20:
					goto IL_B7;
				case 21:
					goto IL_24C;
				}
				if (true)
				{
				}
				if (A_0 != null)
				{
					num = 14;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B7;
				default:
					if (false)
					{
					}
					num = 20;
					continue;
				}
				IL_E8:
				num = 2;
				continue;
				IL_1B7:
				A_0.WriteStartElement(RecordTableEnumerator.b("䬷刹夻嬽㐿ቁ㙃⥅㱇⽉⽋㩍㥏㵑㩓", a_));
				num = 18;
				continue;
				IL_226:
				num = 0;
				continue;
				IL_24C:
				num2 = 0;
				num3 = array.Length;
				num = 1;
			}
			IL_B7:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_168:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
		}
		}
	}

	// Token: 0x060044C7 RID: 17607 RVA: 0x00291378 File Offset: 0x00290378
	private void ᜁ(XmlWriter A_0, string A_1, SheetProtectionType A_2, bool A_3, SheetProtectionType A_4)
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
		bool a_ = (A_4 & A_2) == SheetProtectionType.None;
		spr\u1B7A.ᜀ(A_0, A_1, a_, A_3);
	}

	// Token: 0x060044C8 RID: 17608 RVA: 0x002913C8 File Offset: 0x002903C8
	private void ᜀ(XmlWriter A_0, string A_1, SheetProtectionType A_2, bool A_3, SheetProtectionType A_4)
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
		bool a_ = (A_4 & A_2) != SheetProtectionType.None;
		spr\u1B7A.ᜀ(A_0, A_1, a_, A_3);
	}

	// Token: 0x060044C9 RID: 17609 RVA: 0x00291418 File Offset: 0x00290418
	private void ᜌ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 18;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u2622 spr_u;
					if (spr_u != null)
					{
						num = 6;
						continue;
					}
					return;
				}
				case 1:
					goto IL_195;
				case 2:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("張強吹医䰽┿♁Ń㑅㩇╉㹋㵍", a_));
					int num2 = 0;
					spr\u2622 spr_u;
					int count = spr_u.Count;
					num = 10;
					continue;
				}
				case 3:
					goto IL_158;
				case 4:
					goto IL_AF;
				case 5:
					A_0.WriteEndElement();
					num = 1;
					continue;
				case 6:
					num = 12;
					continue;
				case 7:
				{
					spr\u2622 spr_u;
					int num3;
					if (spr_u[num3].ᜁ() != IgnoreErrorType.None)
					{
						num = 21;
						continue;
					}
					num3++;
					num = 3;
					continue;
				}
				case 8:
				{
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					spr\u2622 spr_u = A_1.ErrorIndicators;
					num = 0;
					continue;
				}
				case 9:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					spr\u2622 spr_u;
					spr\u1F7E a_2 = spr_u[num2];
					this.ᜀ(A_0, a_2);
					num2++;
					num = 19;
					continue;
				}
				case 10:
					goto IL_1DC;
				case 11:
				{
					int num3;
					int count2;
					if (num3 >= count2)
					{
						num = 15;
						continue;
					}
					num = 7;
					continue;
				}
				case 12:
				{
					spr\u2622 spr_u;
					if (spr_u.Count == 0)
					{
						num = 20;
						continue;
					}
					int count2 = spr_u.Count;
					bool flag = false;
					int num3 = 0;
					num = 13;
					continue;
				}
				case 13:
					goto IL_158;
				case 14:
					goto IL_156;
				case 15:
					goto IL_B4;
				case 16:
				{
					bool flag;
					if (flag)
					{
						num = 2;
						continue;
					}
					return;
				}
				case 17:
					goto IL_B4;
				case 19:
					goto IL_1DC;
				case 20:
					goto IL_261;
				case 21:
				{
					bool flag = true;
					num = 17;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AF;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_B4:
				num = 16;
				continue;
				IL_158:
				num = 11;
				continue;
				IL_1DC:
				num = 9;
			}
			IL_AF:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_156:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
			IL_195:
			return;
			IL_261:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x060044CA RID: 17610 RVA: 0x002916C4 File Offset: 0x002906C4
	private void ᜀ(XmlWriter A_0, spr\u1F7E A_1)
	{
		int a_ = 14;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.ᜁ() != IgnoreErrorType.None)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				goto IL_43;
			case 3:
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("ⵃⅅ♇╉㹋⭍㑏ᝑ♓⑕㝗⡙", a_));
				string value = this.ᜀ(A_1);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㝃㝅㩇⽉⩋", a_), value);
				this.ᜀ(A_0, A_1.ᜁ());
				A_0.WriteEndElement();
				num = 5;
				continue;
			}
			case 4:
				goto IL_125;
			case 5:
				goto IL_EB;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 6;
			}
		}
		IL_43:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_EB:
		return;
		IL_125:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵃ⡅ⱇ⍉⽋⽍⑏㵑♓", a_));
	}

	// Token: 0x060044CB RID: 17611 RVA: 0x002917FC File Offset: 0x002907FC
	private void ᜀ(XmlWriter A_0, IgnoreErrorType A_1)
	{
		int a_ = 2;
		int num = 1;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				goto IL_70;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				goto IL_AF;
			case 3:
				if (num2 >= num3)
				{
					num = 5;
					continue;
				}
				num = 4;
				continue;
			case 4:
				if ((A_1 & spr\u1B7A.ᡂ[num2]) != IgnoreErrorType.None)
				{
					num = 7;
					continue;
				}
				goto IL_70;
			case 5:
				goto IL_C9;
			case 6:
				goto IL_6E;
			case 7:
				A_0.WriteAttributeString(spr\u1B7A.ᡅ[num2], RecordTableEnumerator.b("ष", a_));
				num = 0;
				continue;
			case 8:
				goto IL_AF;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num2 = 0;
			num3 = spr\u1B7A.ᡂ.Length;
			num = 8;
			continue;
			IL_74:
			num = 2;
			continue;
			IL_70:
			num2++;
			goto IL_74;
			IL_AF:
			num = 3;
		}
		IL_6E:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_C9:
		if (true)
		{
		}
	}

	// Token: 0x060044CC RID: 17612 RVA: 0x00291920 File Offset: 0x00290920
	private string ᜀ(spr\u1F7E A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 3;
			StringBuilder stringBuilder;
			for (;;)
			{
				int num2;
				int count;
				List<Rectangle> list;
				Rectangle rectangle;
				string value;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						if (false)
						{
						}
						goto IL_102;
					}
					break;
				case 1:
					if (num2 >= count)
					{
						num = 13;
						continue;
					}
					rectangle = list[num2];
					value = sprṔ.ᜂ(rectangle.Left + 1, rectangle.Top + 1);
					stringBuilder.Append(value);
					num = 6;
					continue;
				case 2:
					if (num2 != count - 1)
					{
						num = 9;
						continue;
					}
					goto IL_102;
				case 4:
					goto IL_128;
				case 5:
					goto IL_155;
				case 6:
					if (rectangle.Left == rectangle.Right)
					{
						num = 7;
						continue;
					}
					goto IL_A2;
				case 7:
					num = 10;
					continue;
				case 8:
					goto IL_6D;
				case 9:
					stringBuilder.Append(' ');
					num = 0;
					continue;
				case 10:
					if (rectangle.Top != rectangle.Bottom)
					{
						if (true)
						{
						}
						num = 11;
						continue;
					}
					goto IL_128;
				case 11:
					goto IL_A2;
				case 12:
					goto IL_155;
				case 13:
					goto IL_174;
				}
				goto IL_61;
				IL_64:
				num = 8;
				continue;
				IL_61:
				if (A_0 == null)
				{
					goto IL_64;
				}
				list = A_0.ᜂ();
				stringBuilder = new StringBuilder();
				num2 = 0;
				count = list.Count;
				num = 5;
				continue;
				IL_A2:
				stringBuilder.Append(':');
				value = sprṔ.ᜂ(rectangle.Right + 1, rectangle.Bottom + 1);
				stringBuilder.Append(value);
				num = 4;
				continue;
				IL_102:
				num2++;
				num = 12;
				continue;
				IL_128:
				num = 2;
				continue;
				IL_155:
				num = 1;
			}
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("⽅♇⹉╋ⵍㅏ♑㭓⑕", a_));
			IL_174:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x060044CD RID: 17613 RVA: 0x00291B3C File Offset: 0x00290B3C
	private void ᜋ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 14;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DD;
			case 1:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 6;
					continue;
				}
				IWorksheetCustomProperties worksheetCustomProperties;
				this.ᜀ(A_0, A_1, worksheetCustomProperties[num2]);
				num2++;
				num = 7;
				continue;
			}
			case 2:
			{
				IWorksheetCustomProperties worksheetCustomProperties;
				if (worksheetCustomProperties.Count == 0)
				{
					num = 9;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("❃㍅㭇㹉⍋⍍O⁑㭓♕㵗⡙⡛㝝՟ᅡ", a_));
				int num2 = 0;
				int count = worksheetCustomProperties.Count;
				num = 11;
				continue;
			}
			case 3:
			{
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				IWorksheetCustomProperties worksheetCustomProperties = A_1.InnerCustomProperties;
				num = 8;
				continue;
			}
			case 5:
				goto IL_54;
			case 6:
				goto IL_13B;
			case 7:
				goto IL_117;
			case 8:
			{
				IWorksheetCustomProperties worksheetCustomProperties;
				if (worksheetCustomProperties != null)
				{
					num = 10;
					continue;
				}
				return;
			}
			case 9:
				goto IL_A6;
			case 10:
				num = 2;
				continue;
			case 11:
				goto IL_117;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_A6;
			default:
				if (false)
				{
				}
				num = 3;
				continue;
			}
			IL_117:
			num = 1;
		}
		IL_54:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_A6:
		return;
		IL_DD:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
		IL_13B:
		A_0.WriteEndElement();
	}

	// Token: 0x060044CE RID: 17614 RVA: 0x00291CC8 File Offset: 0x00290CC8
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, ICustomProperty A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			for (;;)
			{
				IL_17:
				if (true)
				{
				}
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
							if (false)
							{
							}
							if (A_1 == null)
							{
								num = 2;
								continue;
							}
							num = 5;
							continue;
						}
						break;
					case 2:
						goto IL_E7;
					case 4:
						goto IL_55;
					case 5:
						if (A_2 == null)
						{
							num = 0;
							continue;
						}
						goto IL_E9;
					}
					if (A_0 == null)
					{
						num = 4;
					}
					else
					{
						num = 1;
					}
				}
			}
			IL_55:
			throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝆㭈⑊㵌⩎⍐❒ⱔ", a_));
			IL_E7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
			IL_E9:
			sprᡟ sprᡟ = A_1.DataHolder;
			sprវ sprវ = sprᡟ.ᜋ();
			RelationsCollection a_2 = sprᡟ.ᜇ();
			A_0.WriteStartElement(RecordTableEnumerator.b("⑆㱈㡊㥌⁎㱐͒❔", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("⥆⡈♊⡌", a_), A_2.Name);
			int num2 = 0;
			spr\u2570 a_3;
			string value = sprវ.ᜀ(RecordTableEnumerator.b("㽆╈摊⹌㩎≐❒㩔㩖क़⥚㉜⽞ѠᅢᅤṦ", a_), RecordTableEnumerator.b("╆⁈╊", a_), RecordTableEnumerator.b("♆㥈㭊⅌♎㉐㉒⅔㹖㙘㕚牜⥞འݢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾꺂ﲒﺚ辠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶풸ힺ鎼\udcbe듀냂뇄꣆꓈鯊뿌ꃎꇐ뛒꟔ꏖꃘ", a_), a_2, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊곎ꓐꃒꇔ룖듘诚꿜냞釠蛢韤鏦部", a_), ref num2, out a_3);
			A_0.WriteAttributeString(RecordTableEnumerator.b("⹆ⵈ", a_), RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊", a_), value);
			this.ᜀ(A_1, a_3, A_2);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044CF RID: 17615 RVA: 0x00291E88 File Offset: 0x00290E88
	private void ᜀ(XlsWorksheet A_0, spr\u2570 A_1, ICustomProperty A_2)
	{
		int a_ = 2;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				goto IL_77;
			case 2:
				goto IL_D6;
			case 3:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 4;
				continue;
			case 4:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				goto IL_D8;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_A9;
			}
			if (false)
			{
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
		IL_77:
		IL_A9:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠷䠹医丽┿ぁぃ㽅", a_));
		IL_D6:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("儷丹夻匽", a_));
		IL_D8:
		byte[] bytes = Encoding.Unicode.GetBytes(A_2.Value);
		Stream stream = A_1.ᜐ();
		stream.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x060044D0 RID: 17616 RVA: 0x00291F90 File Offset: 0x00290F90
	private Stream ᜀ(int A_0)
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
			if (this.ᡍ != null)
			{
				return this.ᡍ[A_0];
			}
			break;
		}
		return null;
	}

	// Token: 0x060044D1 RID: 17617 RVA: 0x00291FE4 File Offset: 0x00290FE4
	public void ᜎ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 0;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				string text;
				if (text == null)
				{
					num = 6;
					continue;
				}
				goto IL_128;
			}
			case 2:
				goto IL_4D;
			case 3:
				goto IL_128;
			case 4:
			{
				if (A_1 == null)
				{
					num = 5;
					continue;
				}
				A_0.WriteStartDocument(true);
				A_0.WriteStartElement(RecordTableEnumerator.b("唵圷圹儻嬽⸿㙁㝃", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ॹ౻౽揄ﶏﺑ뮓꒕ꢗꪙꪛ놝춟쎡춣좥", a_));
				IDictionary<string, int> a_2 = this.ᜈ(A_0, A_1);
				this.ᜀ(A_0, A_1, a_2);
				A_0.WriteEndElement();
				sprᡟ sprᡟ = A_1.DataHolder;
				string text = sprᡟ.ᜆ();
				num = 1;
				continue;
			}
			case 5:
				goto IL_126;
			case 6:
			{
				sprᡟ sprᡟ;
				sprᡟ.ᜆ(sprᡟ.ᜇ().GenerateRelationId());
				num = 3;
				continue;
			}
			}
			goto IL_3F;
			IL_45:
			num = 2;
			continue;
			IL_3F:
			if (A_0 == null)
			{
				goto IL_45;
			}
			num = 4;
			continue;
			IL_128:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_45;
			default:
				goto IL_13E;
			}
		}
		IL_4D:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_126:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
		IL_13E:
		if (false)
		{
		}
	}

	// Token: 0x060044D2 RID: 17618 RVA: 0x00292138 File Offset: 0x00291138
	internal void ᜀ(XmlWriter A_0, ShapeCollectionBase A_1, sprᡟ A_2, Dictionary<int, spr\u2175> A_3, RelationsCollection A_4)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				spr\u18AB spr_u18AB;
				int num3;
				IEnumerator enumerator2;
				XlsShape xlsShape2;
				switch (num)
				{
				case 0:
					num = 32;
					continue;
				case 1:
				{
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 2:
							{
								Dictionary<Stream, object>.KeyCollection.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								Stream stream = enumerator.Current;
								stream.Position = 0L;
								XmlReader reader = UtilityMethods.ᜀ(stream);
								A_0.WriteNode(reader, false);
								num = 1;
								continue;
							}
							case 3:
								goto IL_4E1;
							case 4:
								num = 3;
								continue;
							}
							IL_4BB:
							num = 2;
							continue;
							goto IL_4BB;
						}
						IL_4E1:
						goto IL_251;
					}
					finally
					{
						Dictionary<Stream, object>.KeyCollection.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_4F4;
					IL_251:
					num2 = 0;
					int count = A_1.Count;
					num = 26;
					continue;
				}
				case 2:
					if (A_1.ShapeLayoutStream != null)
					{
						num = 21;
						continue;
					}
					goto IL_557;
				case 3:
					goto IL_3CF;
				case 4:
					goto IL_6B4;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_3BD;
				case 8:
				{
					if (A_1 == null)
					{
						num = 30;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䜾ⱀ⽂", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䜾ⱀ⽂⭄㑆", a_), RecordTableEnumerator.b("䤾", a_), null, RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ղᡴ᭶", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䜾ⱀ⽂⭄㑆", a_), RecordTableEnumerator.b("倾", a_), null, RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("䜾ⱀ⽂⭄㑆", a_), RecordTableEnumerator.b("䜾", a_), null, RecordTableEnumerator.b("䨾㍀ⵂ罄㑆⩈⍊⡌≎ぐ⁒硔㩖じ㡚⽜ぞበౢͤ፦䑨ࡪɬɮ䭰ᱲ፴ᅶၸ᡺᡼䕾ﮂ", a_));
					spr_u18AB = new spr\u18AB();
					Dictionary<Stream, object> dictionary = new Dictionary<Stream, object>();
					num3 = 0;
					int count2 = A_1.Count;
					num = 25;
					continue;
				}
				case 9:
				{
					XlsShape xlsShape;
					if (xlsShape.XmlDataStream != null)
					{
						num = 10;
						continue;
					}
					goto IL_6B4;
				}
				case 10:
				{
					XlsShape xlsShape;
					Stream xmlDataStream = xlsShape.XmlDataStream;
					xmlDataStream.Position = 0L;
					XmlReader reader2 = UtilityMethods.ᜀ(xmlDataStream);
					A_0.WriteNode(reader2, false);
					num = 4;
					continue;
				}
				case 11:
				{
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								int key;
								spr\u2175 spr_u;
								if (A_3.TryGetValue(key, out spr_u))
								{
									num = 2;
									continue;
								}
								break;
							}
							case 1:
								num = 3;
								continue;
							case 2:
							{
								spr\u2175 spr_u;
								Type value;
								spr_u.ᜀ(A_0, value);
								num = 5;
								continue;
							}
							case 3:
								goto IL_627;
							case 6:
							{
								if (!enumerator2.MoveNext())
								{
									num = 1;
									continue;
								}
								KeyValuePair<int, Type> keyValuePair = (KeyValuePair<int, Type>)enumerator2.Current;
								int key = keyValuePair.Key;
								Type value = keyValuePair.Value;
								num = 0;
								continue;
							}
							}
							IL_5A2:
							num = 6;
							continue;
							goto IL_5A2;
						}
						IL_627:
						goto IL_11F;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator2 as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_68E;
								case 1:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										disposable.Dispose();
										break;
									}
									num = 0;
									continue;
								case 2:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_690;
								}
								break;
							}
						}
						IL_68E:
						IL_690:;
					}
					goto IL_691;
					IL_11F:
					Dictionary<Stream, object> dictionary;
					Dictionary<Stream, object>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator();
					num = 1;
					continue;
				}
				case 12:
					goto IL_4F4;
				case 13:
				{
					int count2;
					if (num3 >= count2)
					{
						num = 5;
						continue;
					}
					xlsShape2 = (A_1[num3] as XlsShape);
					xlsShape2.\u1715();
					num = 22;
					continue;
				}
				case 14:
					spr_u18AB.ᜀ(xlsShape2);
					num = 29;
					continue;
				case 15:
					goto IL_302;
				case 16:
				{
					int instance;
					spr\u2175 spr_u2;
					if (A_3.TryGetValue(instance, out spr_u2))
					{
						num = 34;
						continue;
					}
					goto IL_2D9;
				}
				case 17:
					num = 24;
					continue;
				case 18:
				{
					if (xlsShape2.XmlTypeStream == null)
					{
						num = 14;
						continue;
					}
					Dictionary<Stream, object> dictionary;
					dictionary[xlsShape2.XmlTypeStream] = null;
					num = 23;
					continue;
				}
				case 19:
					goto IL_557;
				case 20:
					goto IL_323;
				case 21:
					ShapeParser.WriteNodeFromStream(A_0, A_1.ShapeLayoutStream);
					num = 19;
					continue;
				case 22:
					if (true)
					{
					}
					if (xlsShape2.VmlShape)
					{
						num = 35;
						continue;
					}
					goto IL_3BD;
				case 23:
					if (xlsShape2.ImageRelation != null)
					{
						num = 12;
						continue;
					}
					goto IL_3BD;
				case 24:
				{
					XlsShape xlsShape;
					if (xlsShape.XmlDataStream != null)
					{
						num = 0;
						continue;
					}
					goto IL_3CF;
				}
				case 25:
					goto IL_43F;
				case 26:
					goto IL_302;
				case 27:
				{
					XlsShape xlsShape;
					if (xlsShape.VmlShape)
					{
						num = 17;
						continue;
					}
					goto IL_6B4;
				}
				case 28:
				{
					int count;
					if (num2 >= count)
					{
						num = 20;
						continue;
					}
					XlsShape xlsShape = (XlsShape)A_1[num2];
					xlsShape.\u1715();
					int instance = xlsShape.Instance;
					num = 27;
					continue;
				}
				case 29:
					goto IL_3BD;
				case 30:
					goto IL_6AF;
				case 31:
					goto IL_43F;
				case 32:
				{
					XlsShape xlsShape;
					if (xlsShape.EnableAlternateContent)
					{
						num = 3;
						continue;
					}
					goto IL_2D9;
				}
				case 33:
					goto IL_6B4;
				case 34:
				{
					XlsShape xlsShape;
					spr\u2175 spr_u2;
					spr_u2.ᜀ(A_0, xlsShape, A_2, A_4);
					num = 33;
					continue;
				}
				case 35:
					num = 18;
					continue;
				case 36:
					goto IL_D2;
				}
				if (A_0 == null)
				{
					num = 36;
					continue;
				}
				goto IL_691;
				IL_2D9:
				num = 9;
				continue;
				IL_302:
				num = 28;
				continue;
				IL_3BD:
				num3++;
				num = 31;
				continue;
				IL_3CF:
				num = 16;
				continue;
				IL_43F:
				num = 13;
				continue;
				IL_4F4:
				A_4[xlsShape2.ImageRelationId] = xlsShape2.ImageRelation;
				num = 6;
				continue;
				IL_557:
				enumerator2 = spr_u18AB.ᜀ().GetEnumerator();
				num = 11;
				continue;
				IL_691:
				num = 8;
				continue;
				IL_6B4:
				num2++;
				num = 15;
			}
			IL_D2:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_323:
			A_0.WriteEndElement();
			A_0.Flush();
			return;
			IL_6AF:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀≂㕄≆㩈", a_));
		}
		}
	}

	// Token: 0x060044D3 RID: 17619 RVA: 0x00292844 File Offset: 0x00291844
	public void ᜀ(XmlWriter A_0, spr\u1D9B A_1, sprᡟ A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				XlsShape xlsShape;
				string localName;
				string text;
				string localName2;
				string ns;
				int num2;
				int count;
				bool flag;
				string prefix;
				switch (num)
				{
				case 0:
					num = 36;
					continue;
				case 1:
					if (xlsShape.ᜪ == null)
					{
						num = 3;
						continue;
					}
					goto IL_3AA;
				case 3:
					if (true)
					{
					}
					num = 38;
					continue;
				case 4:
					goto IL_1D4;
				case 5:
					if (xlsShape.ᜩ != null)
					{
						num = 19;
						continue;
					}
					goto IL_1BD;
				case 6:
					goto IL_296;
				case 7:
					goto IL_266;
				case 8:
					localName = RecordTableEnumerator.b("唵尷䠹", a_);
					text = RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟횣장\udfa7쎩슫즭", a_);
					localName2 = RecordTableEnumerator.b("䌵䬷弹主洽⠿⍁㑃⍅㭇", a_);
					ns = RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮗﶛ풟", a_);
					num = 26;
					continue;
				case 9:
					if (this.ᜅ().\u173E == null)
					{
						num = 13;
						continue;
					}
					goto IL_3AA;
				case 10:
				{
					spr\u2175 spr_u;
					spr_u.ᜀ(A_0, xlsShape, A_2, A_2.ᜈ());
					num = 12;
					continue;
				}
				case 11:
					goto IL_DA;
				case 12:
					goto IL_1BD;
				case 13:
					num = 1;
					continue;
				case 14:
					if (!xlsShape.VmlShape)
					{
						goto IL_197;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B8;
					default:
						if (false)
						{
						}
						num = 30;
						continue;
					}
					break;
				case 15:
					if (xlsShape.EnableAlternateContent)
					{
						num = 4;
						continue;
					}
					num = 9;
					continue;
				case 16:
					goto IL_4ED;
				case 17:
					if (num2 >= count)
					{
						num = 33;
						continue;
					}
					xlsShape = (XlsShape)A_1[num2];
					num = 32;
					continue;
				case 18:
					goto IL_1BD;
				case 19:
					goto IL_3AA;
				case 20:
					num = 15;
					continue;
				case 21:
					return;
				case 22:
					goto IL_266;
				case 23:
					goto IL_1B8;
				case 24:
					num = 5;
					continue;
				case 25:
					if (xlsShape.EnableAlternateContent)
					{
						num = 29;
						continue;
					}
					goto IL_1BD;
				case 26:
					goto IL_296;
				case 27:
					if (xlsShape.XmlDataStream != null)
					{
						num = 23;
						continue;
					}
					goto IL_1BD;
				case 28:
					if (flag)
					{
						num = 8;
						continue;
					}
					localName = RecordTableEnumerator.b("丵尷䠹", a_);
					text = RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮝ솟욡힣캥춧쾩\ud8ab슯펱쎳\udfb5횷\uddb9", a_);
					prefix = RecordTableEnumerator.b("丵尷䠹", a_);
					localName2 = RecordTableEnumerator.b("䄵䬷縹主", a_);
					ns = text;
					num = 6;
					continue;
				case 29:
					goto IL_197;
				case 30:
					num = 25;
					continue;
				case 31:
				{
					spr\u2175 spr_u;
					if (this.ᡌ.TryGetValue(xlsShape.GetType(), out spr_u))
					{
						num = 10;
						continue;
					}
					num = 14;
					continue;
				}
				case 32:
					if (xlsShape.VmlShape)
					{
						num = 20;
						continue;
					}
					goto IL_1D4;
				case 33:
					goto IL_291;
				case 34:
					if (A_1 == null)
					{
						num = 16;
						continue;
					}
					num = 35;
					continue;
				case 35:
					if (A_1.Count - A_1.WorksheetBase.VmlShapesCount <= 0)
					{
						num = 0;
						continue;
					}
					goto IL_20A;
				case 36:
					if (!spr\u1B7A.ᜀ(A_1))
					{
						num = 21;
						continue;
					}
					goto IL_20A;
				case 37:
					goto IL_1BD;
				case 38:
					if (xlsShape.ᜫ == null)
					{
						num = 24;
						continue;
					}
					goto IL_3AA;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 34;
				continue;
				IL_197:
				num = 27;
				continue;
				IL_1B8:
				spr\u1A78 spr_u1A = new spr\u1A78();
				spr_u1A.ᜀ(A_0, xlsShape, A_2, A_2.ᜈ());
				num = 18;
				continue;
				IL_1BD:
				num2++;
				num = 7;
				continue;
				IL_1D4:
				num = 31;
				continue;
				IL_20A:
				flag = (A_1.Worksheet == null);
				ns = null;
				prefix = null;
				num = 28;
				continue;
				IL_266:
				num = 17;
				continue;
				IL_296:
				A_0.WriteStartDocument(true);
				A_0.WriteStartElement(prefix, localName2, ns);
				A_0.WriteAttributeString(RecordTableEnumerator.b("丵唷嘹刻䴽", a_), localName, null, text);
				A_0.WriteAttributeString(RecordTableEnumerator.b("丵唷嘹刻䴽", a_), RecordTableEnumerator.b("圵", a_), null, RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮙ", a_));
				int num3 = 0;
				num2 = 0;
				count = A_1.Count;
				num = 22;
				continue;
				IL_3AA:
				this.ᜀ(A_0, xlsShape, A_2, A_2.ᜈ(), num3);
				num3++;
				num = 37;
			}
			IL_DA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_291:
			A_0.WriteEndElement();
			return;
			IL_4ED:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷嬹䰻嬽㌿", a_));
		}
		}
	}

	// Token: 0x060044D4 RID: 17620 RVA: 0x00292DB8 File Offset: 0x00291DB8
	internal void ᜀ(XmlWriter A_0, XlsShape A_1, sprᡟ A_2, RelationsCollection A_3, int A_4)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					goto IL_285;
				case 1:
					goto IL_56D;
				case 2:
					num = 12;
					continue;
				case 3:
				{
					int num2;
					if (num2 >= A_1.ᜩ.Count)
					{
						num = 0;
						continue;
					}
					this.ᜀ(A_0, A_1.ᜩ[num2]);
					num2++;
					num = 38;
					continue;
				}
				case 4:
					if (this.ᜅ().\u173E != null)
					{
						num = 2;
						continue;
					}
					goto IL_5BF;
				case 5:
					num = 24;
					continue;
				case 7:
					goto IL_4BE;
				case 8:
					goto IL_56D;
				case 9:
					if (A_1.ChildShapes.Count > 0)
					{
						num = 7;
						continue;
					}
					goto IL_612;
				case 10:
				{
					int num2 = 0;
					num = 15;
					continue;
				}
				case 11:
					goto IL_5BA;
				case 12:
					if (A_4 < this.ᜅ().\u173E.Count)
					{
						num = 25;
						continue;
					}
					goto IL_5BF;
				case 13:
					goto IL_40C;
				case 14:
				{
					if (num3 >= A_1.ChildShapes.Count)
					{
						num = 23;
						continue;
					}
					XlsChartShape xlsChartShape = (XlsChartShape)A_1.ChildShapes[num3];
					XlsChart xlsChart = xlsChartShape.ChartObject;
					spr\u21A8 spr_u21A = new spr\u21A8();
					string text;
					string a_2 = spr_u21A.ᜀ(A_2, xlsChart, out text);
					spr_u21A.ᜀ(A_0, xlsChartShape, a_2, A_2, true);
					A_2.ᜀ(xlsChart.Relations, text.Substring(1));
					num3++;
					num = 8;
					continue;
				}
				case 15:
					goto IL_E3;
				case 16:
					goto IL_DE;
				case 17:
				{
					int num4 = 0;
					num = 26;
					continue;
				}
				case 18:
					if (A_1.ᜪ != null)
					{
						num = 37;
						continue;
					}
					goto IL_545;
				case 19:
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					num = 32;
					continue;
				case 20:
				{
					int num4;
					if (num4 >= A_1.ᜫ.Count)
					{
						num = 36;
						continue;
					}
					this.ᜀ(A_0, A_1.ᜫ[num4]);
					num4++;
					num = 28;
					continue;
				}
				case 21:
					if (A_1.ᜩ == null)
					{
						num = 5;
						continue;
					}
					goto IL_4BE;
				case 22:
				{
					int num5;
					if (num5 < A_1.ᜪ.Count)
					{
						this.ᜀ(A_0, A_1.ᜪ[num5]);
						num5++;
						num = 13;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29C;
					default:
						if (false)
						{
						}
						num = 33;
						continue;
					}
					break;
				}
				case 23:
					A_0.WriteEndElement();
					num = 35;
					continue;
				case 24:
					if (A_1.ᜫ == null)
					{
						num = 39;
						continue;
					}
					goto IL_4BE;
				case 25:
					this.ᜀ(A_0, this.ᜅ().\u173E[A_4]);
					this.ᜀ(A_0, this.ᜅ().\u173E[A_4 + 1]);
					num = 29;
					continue;
				case 26:
					goto IL_233;
				case 27:
					if (A_1.ᜫ != null)
					{
						goto IL_29C;
					}
					goto IL_361;
				case 28:
					goto IL_233;
				case 29:
					goto IL_5BF;
				case 30:
					goto IL_40C;
				case 31:
					goto IL_280;
				case 32:
				{
					if (A_2 == null)
					{
						num = 31;
						continue;
					}
					A_1.GetType();
					A_0.WriteStartElement(RecordTableEnumerator.b("䈵伷唹缻嬽ⰿ⹁Ճ⡅⭇≉⍋㱍", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮝ솟욡힣캥춧쾩\ud8ab슯펱쎳\udfb5횷\uddb9", a_));
					spr\u1A78 spr_u1A = new spr\u1A78();
					spr_u1A.ᜁ(A_0, RecordTableEnumerator.b("倵䨷唹儻", a_), A_1.LeftColumn, A_1.LeftColumnOffset, A_1.TopRow, A_1.TopRowOffset, A_1.Worksheet, RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮝ솟욡힣캥춧쾩\ud8ab슯펱쎳\udfb5횷\uddb9", a_));
					spr_u1A.ᜁ(A_0, RecordTableEnumerator.b("䈵圷", a_), A_1.RightColumn, A_1.RightColumnOffset, A_1.BottomRow, A_1.BottomRowOffset, A_1.Worksheet, RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮝ솟욡힣캥춧쾩\ud8ab슯펱쎳\udfb5횷\uddb9", a_));
					num = 18;
					continue;
				}
				case 33:
					goto IL_545;
				case 34:
					if (A_1.ᜩ != null)
					{
						num = 10;
						continue;
					}
					goto IL_285;
				case 35:
					goto IL_387;
				case 36:
					goto IL_361;
				case 37:
				{
					int num5 = 0;
					num = 30;
					continue;
				}
				case 38:
					goto IL_E3;
				case 39:
					if (true)
					{
					}
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				num = 19;
				continue;
				IL_E3:
				num = 3;
				continue;
				IL_233:
				num = 20;
				continue;
				IL_285:
				num = 27;
				continue;
				IL_29C:
				num = 17;
				continue;
				IL_361:
				num3 = 0;
				num = 1;
				continue;
				IL_40C:
				num = 22;
				continue;
				IL_4BE:
				A_0.WriteStartElement(RecordTableEnumerator.b("儵䨷䨹漻丽", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮝ솟욡힣캥춧쾩\ud8ab슯펱쎳\udfb5횷\uddb9", a_));
				A_4 *= 2;
				num = 4;
				continue;
				IL_545:
				num = 21;
				continue;
				IL_56D:
				num = 14;
				continue;
				IL_5BF:
				num = 34;
			}
			IL_DE:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_280:
			throw new ArgumentNullException(RecordTableEnumerator.b("帵圷嘹堻嬽㈿", a_));
			IL_387:
			goto IL_612;
			IL_5BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷嬹䰻嬽", a_));
			IL_612:
			A_0.WriteElementString(RecordTableEnumerator.b("唵吷匹夻倽㐿ف╃㉅⥇", a_), RecordTableEnumerator.b("帵䰷丹䰻н漿流㝃╅⁇⽉⅋⽍⍏籑㭓♕㵗㑙⑛㍝౟ѡୣᑥէ୩ᡫᵭ幯ᵱٳᅵ坷ṹ๻ώꎋ벍ꂏꊑꊓ릕ﮝ솟욡힣캥춧쾩\ud8ab슯펱쎳\udfb5횷\uddb9", a_), string.Empty);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044D5 RID: 17621 RVA: 0x00293404 File Offset: 0x00292404
	public static bool ᜀ(IShapes A_0)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = A_0.GetEnumerator();
			bool result;
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						result = true;
						num = 3;
						continue;
					case 2:
						goto IL_B5;
					case 3:
						goto IL_AA;
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num = 6;
							continue;
						}
						XlsShape xlsShape = (XlsShape)enumerator.Current;
						num = 5;
						continue;
					}
					case 5:
					{
						XlsShape xlsShape;
						if (xlsShape.EnableAlternateContent)
						{
							num = 0;
							continue;
						}
						break;
					}
					case 6:
						num = 2;
						continue;
					}
					IL_5B:
					num = 4;
					continue;
					goto IL_5B;
				}
				IL_AA:
				return result;
				IL_B5:
				return false;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_118;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								disposable.Dispose();
								break;
							}
							num = 0;
							continue;
						case 2:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_11A;
						}
						break;
					}
				}
				IL_118:
				IL_11A:;
			}
			return result;
		}
		}
	}

	// Token: 0x060044D6 RID: 17622 RVA: 0x00293540 File Offset: 0x00292540
	public RelationsCollection ᜅ(XmlWriter A_0, XlsExternWorkbook A_1)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_80;
			case 1:
				if (!A_1.IsOleLink)
				{
					num = 0;
					continue;
				}
				goto IL_AD;
			case 2:
				goto IL_66;
			case 3:
				goto IL_A2;
			case 4:
				if (A_1.IsInternalReference)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			}
			if (!A_1.IsAddInFunctions)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_66;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				num = 2;
				continue;
			}
			goto IL_82;
			IL_66:
			num = 4;
		}
		IL_80:
		return this.ᜄ(A_0, A_1);
		IL_82:
		return null;
		IL_A2:
		goto IL_82;
		IL_AD:
		return this.ᜃ(A_0, A_1);
	}

	// Token: 0x060044D7 RID: 17623 RVA: 0x00293604 File Offset: 0x00292604
	public RelationsCollection ᜄ(XmlWriter A_0, XlsExternWorkbook A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 17;
			string text;
			RelationsCollection relationsCollection;
			string text2;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (!text.StartsWith(RecordTableEnumerator.b("娻圽ⰿ❁繃楅杇敉", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_273;
				case 1:
					if (true)
					{
					}
					text = RecordTableEnumerator.b("娻圽ⰿ❁繃楅杇敉", a_) + text;
					num = 4;
					continue;
				case 2:
					flag = false;
					goto IL_2A1;
				case 3:
					num = 5;
					continue;
				case 4:
					goto IL_140;
				case 5:
					if (!text.StartsWith(RecordTableEnumerator.b("总", a_)))
					{
						goto IL_10B;
					}
					goto IL_356;
				case 6:
					goto IL_356;
				case 7:
					if (flag2)
					{
						num = 18;
						continue;
					}
					goto IL_140;
				case 8:
					if (!text.StartsWith(RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇", a_)))
					{
						num = 23;
						continue;
					}
					goto IL_273;
				case 9:
				{
					bool flag3;
					if (!flag3)
					{
						num = 10;
						continue;
					}
					num = 19;
					continue;
				}
				case 10:
					num = 16;
					continue;
				case 11:
					num = 20;
					continue;
				case 12:
					if (!text.Contains(RecordTableEnumerator.b("ػ戽", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_356;
				case 13:
				{
					bool flag3;
					if (flag3)
					{
						num = 1;
						continue;
					}
					goto IL_140;
				}
				case 14:
				{
					bool flag3 = File.Exists(text);
					num = 6;
					continue;
				}
				case 15:
					flag = (text[0] != '/');
					goto IL_2A1;
				case 16:
					goto IL_28E;
				case 18:
					num = 12;
					continue;
				case 19:
					goto IL_2D3;
				case 20:
				{
					if (A_1.IsAddInFunctions)
					{
						num = 27;
						continue;
					}
					relationsCollection = new RelationsCollection();
					text2 = relationsCollection.GenerateRelationId();
					text = this.ᜀ(A_1.URL);
					bool flag3 = true;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10B;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				case 21:
					num = 8;
					continue;
				case 22:
					goto IL_304;
				case 23:
					num = 15;
					continue;
				case 24:
					goto IL_AB;
				case 25:
					if (!A_1.IsInternalReference)
					{
						num = 11;
						continue;
					}
					goto IL_11C;
				case 26:
					if (A_1 == null)
					{
						num = 22;
						continue;
					}
					num = 25;
					continue;
				case 27:
					goto IL_183;
				}
				if (A_0 == null)
				{
					num = 24;
					continue;
				}
				num = 26;
				continue;
				IL_10B:
				num = 14;
				continue;
				IL_140:
				num = 9;
				continue;
				IL_273:
				num = 2;
				continue;
				IL_2A1:
				flag2 = flag;
				num = 7;
				continue;
				IL_356:
				num = 13;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_11C:
			return null;
			IL_183:
			goto IL_11C;
			IL_28E:
			string text3 = RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㝙㕛㵝቟ൡᝣ॥๧ṩ䉫൭Ὧά孳᥵ṷᱹᕻᵽ궁뚃뚅뢇벉ꎋﲍﺑ좟쮡풣향螧튩삫좯욱톳쒵횷\udbb9킻ꦿ곁꿃雅꧇뻉꓋꣏뻑蓓럕곗닙釛럝鏟釡跣裥迧", a_);
			goto IL_376;
			IL_2D3:
			text3 = RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽좋煉뎛겝邟銡銣覥\udaa7쾩삫쾭쒯\udbb1\udbb3\ud8b5쮷특햻캽뎿ꇃ뻅볇꿉뻋ꃍ뇏뻑飓뿕뛗뇙賛뿝铟諡", a_);
			goto IL_376;
			IL_304:
			throw new ArgumentNullException(RecordTableEnumerator.b("帻儽⼿⥁", a_));
			IL_376:
			string a_2 = text3;
			relationsCollection[text2] = new sprᦨ(text, a_2, true);
			A_0.WriteStartElement(RecordTableEnumerator.b("夻䘽㐿❁㙃⡅⥇♉K❍㹏㥑", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽ﾋﮕ떙꺛꺝邟钡讣쮥즧쎩슫", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("夻䘽㐿❁㙃⡅⥇♉๋⅍㽏㥑", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("唻娽", a_), RecordTableEnumerator.b("吻䨽㐿㉁繃楅杇㥉⽋♍㕏㽑㕓╕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ塵᝷ࡹ᭻兽좋煉뎛겝邟銡銣覥\udaa7쾩삫쾭쒯\udbb1\udbb3\ud8b5쮷특햻캽뎿", a_), text2);
			this.ᜁ(A_0, A_1);
			this.ᜀ(A_0, A_1);
			this.ᜂ(A_0, A_1);
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return relationsCollection;
		}
		}
	}

	// Token: 0x060044D8 RID: 17624 RVA: 0x00293A18 File Offset: 0x00292A18
	public RelationsCollection ᜃ(XmlWriter A_0, XlsExternWorkbook A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 11;
			string text;
			RelationsCollection relationsCollection;
			string text2;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (flag)
					{
						num = 20;
						continue;
					}
					goto IL_1BE;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_298;
					default:
						goto IL_2FE;
					}
					break;
				case 2:
					goto IL_235;
				case 3:
					if (!text.StartsWith(RecordTableEnumerator.b("机", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_286;
				case 4:
					flag2 = (text[0] != '/');
					goto IL_23A;
				case 5:
				{
					bool flag3;
					if (flag3)
					{
						goto IL_298;
					}
					goto IL_1BE;
				}
				case 6:
				{
					bool flag3;
					if (!flag3)
					{
						num = 18;
						continue;
					}
					num = 1;
					continue;
				}
				case 7:
					if (!text.Contains(RecordTableEnumerator.b("ĺ愼", a_)))
					{
						num = 23;
						continue;
					}
					goto IL_286;
				case 8:
				{
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					relationsCollection = new RelationsCollection();
					text2 = relationsCollection.GenerateRelationId();
					text = this.ᜀ(A_1.URL);
					bool flag3 = true;
					num = 14;
					continue;
				}
				case 9:
					flag2 = false;
					goto IL_23A;
				case 10:
				{
					bool flag3 = File.Exists(text);
					num = 19;
					continue;
				}
				case 12:
					num = 4;
					continue;
				case 13:
					text = RecordTableEnumerator.b("崺吼匾⑀祂橄框晈", a_) + text;
					num = 22;
					continue;
				case 14:
					if (!text.StartsWith(RecordTableEnumerator.b("崺吼匾⑀祂橄框晈", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_113;
				case 15:
					if (!text.StartsWith(RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_113;
				case 16:
					goto IL_B3;
				case 17:
					num = 15;
					continue;
				case 18:
					num = 16;
					continue;
				case 19:
					goto IL_286;
				case 20:
					num = 7;
					continue;
				case 21:
					goto IL_A2;
				case 22:
					goto IL_1BE;
				case 23:
					num = 3;
					continue;
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				num = 8;
				continue;
				IL_113:
				num = 9;
				continue;
				IL_1BE:
				num = 6;
				continue;
				IL_23A:
				flag = flag2;
				num = 0;
				continue;
				IL_286:
				num = 5;
				continue;
				IL_298:
				num = 13;
			}
			IL_A2:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_B3:
			if (true)
			{
			}
			string text3 = RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㑘㉚㹜ⵞ๠ၢ੤Ŧᵨ䕪๬nᱰ屲ᩴᅶὸቺṼ᩾꺀놂떄랆뾈ꒊﾌ﶐ﺖ좠펢횤袦톨잪힮얰횲잴\ud9b6\ud8b8ힺ횾꿀ꣂ闄ꛆ뷈ꏊ럎뷐菒듔ꏖ뇘雚드곞鋠諢诤胦", a_);
			goto IL_314;
			IL_235:
			throw new ArgumentNullException(RecordTableEnumerator.b("夺刼倾⩀", a_));
			IL_2FE:
			if (false)
			{
			}
			text3 = RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾곂꧄ꋆ蛈꧊Ꟍ꫎닐꟒", a_);
			IL_314:
			string a_2 = text3;
			relationsCollection[text2] = new sprᦨ(text, a_2, true);
			A_0.WriteStartDocument(true);
			A_0.WriteStartElement(RecordTableEnumerator.b("帺䔼䬾⑀ㅂ⭄♆╈݊⑌ⅎ㩐", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼౾ﮖ뚘ꦚ궜꾞鞠貢좤욦삨얪", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("吺儼娾ീ⩂⭄ⱆ", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("䌺值匾⽀あ", a_), RecordTableEnumerator.b("䤺", a_), null, RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("刺夼", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾", a_), text2);
			A_0.WriteAttributeString(RecordTableEnumerator.b("䬺似倾♀ੂ⅄", a_), A_1.ProgramId);
			A_0.WriteStartElement(RecordTableEnumerator.b("吺儼娾ࡀ㝂⁄⩆㩈", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("吺儼娾ࡀ㝂⁄⩆", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("唺尼刾⑀", a_), RecordTableEnumerator.b("᰺", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("娺夼䤾⡀あ⁄", a_), RecordTableEnumerator.b("਺", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("䬺似娾❀♂㝄ᝆ⁈⡊", a_), RecordTableEnumerator.b("਺", a_));
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return relationsCollection;
		}
		}
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x00293EA0 File Offset: 0x00292EA0
	private void ᜂ(XmlWriter A_0, XlsExternWorkbook A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			for (;;)
			{
				int count = A_1.Worksheets.Count;
				int num = 2;
				for (;;)
				{
					Dictionary<string, string> dictionary;
					int num2;
					XlsExternWorksheet xlsExternWorksheet;
					switch (num)
					{
					case 0:
						if (dictionary == null)
						{
							num = 4;
							continue;
						}
						goto IL_143;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EE;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							goto IL_143;
						}
						break;
					case 2:
						if (count > 0)
						{
							num = 8;
							continue;
						}
						return;
					case 3:
						goto IL_6F;
					case 4:
						dictionary = new Dictionary<string, string>();
						num = 1;
						continue;
					case 5:
						return;
					case 6:
						A_0.WriteEndElement();
						num = 5;
						continue;
					case 7:
						goto IL_EE;
					case 8:
						A_0.WriteStartElement(RecordTableEnumerator.b("丼圾⑀♂ㅄ͆⡈㽊ⱌᱎ㑐❒", a_));
						num2 = 0;
						num = 7;
						continue;
					case 9:
						if (num2 >= count)
						{
							num = 6;
							continue;
						}
						xlsExternWorksheet = A_1.Worksheets.Values[num2];
						dictionary = xlsExternWorksheet.AdditionalAttributes;
						num = 0;
						continue;
					}
					break;
					IL_6F:
					num = 9;
					continue;
					IL_EE:
					goto IL_6F;
					IL_143:
					dictionary[RecordTableEnumerator.b("丼圾⑀♂ㅄๆⵈ", a_)] = xlsExternWorksheet.Index.ToString();
					this.ᜀ(A_0, xlsExternWorksheet.CellRecords, null, RecordTableEnumerator.b("帼娾ⵀ⽂", a_), dictionary, false);
					num2++;
					num = 3;
				}
			}
			return;
		}
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x00294048 File Offset: 0x00293048
	private void ᜁ(XmlWriter A_0, XlsExternWorkbook A_1)
	{
		int a_ = 10;
		for (;;)
		{
			int sheetNumber = A_1.SheetNumber;
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (sheetNumber <= 0)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_67;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					if (true)
					{
					}
					A_0.WriteEndElement();
					num = 4;
					continue;
				case 2:
					goto IL_67;
				case 3:
					goto IL_69;
				case 4:
					return;
				case 5:
				{
					if (num2 >= sheetNumber)
					{
						num = 1;
						continue;
					}
					string sheetName = A_1.GetSheetName(num2);
					A_0.WriteStartElement(RecordTableEnumerator.b("㌿⩁⅃⍅㱇щⵋ⍍㕏", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㘿⍁⡃", a_), sheetName);
					A_0.WriteEndElement();
					num2++;
					num = 6;
					continue;
				}
				case 6:
					goto IL_69;
				}
				break;
				IL_67:
				A_0.WriteStartElement(RecordTableEnumerator.b("㌿⩁⅃⍅㱇щⵋ⍍㕏⅑", a_));
				num2 = 0;
				num = 3;
				continue;
				IL_69:
				num = 5;
			}
		}
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x00294168 File Offset: 0x00293168
	private void ᜀ(XmlWriter A_0, XlsExternWorkbook A_1)
	{
		int a_ = 4;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_10A;
			case 1:
				goto IL_50;
			case 2:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				sprᭆ sprᭆ = A_1.ExternNames;
				int count = sprᭆ.Count;
				num = 7;
				continue;
			}
			case 3:
				if (true)
				{
				}
				A_0.WriteEndElement();
				num = 8;
				continue;
			case 4:
			{
				int count;
				int num2;
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				sprᭆ sprᭆ;
				this.ᜀ(A_0, sprᭆ.ᜀ(num2));
				num2++;
				num = 0;
				continue;
			}
			case 5:
				goto IL_10A;
			case 6:
				goto IL_CC;
			case 7:
			{
				int count;
				if (count > 0)
				{
					num = 10;
					continue;
				}
				return;
			}
			case 8:
				return;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("帹夻堽⤿ⱁ⅃≅ه⭉⅋⭍⍏", a_));
					int num2 = 0;
					num = 5;
					continue;
				}
				}
				break;
			}
			IL_45:
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 2;
			continue;
			goto IL_45;
			IL_10A:
			num = 4;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_CC:
		throw new ArgumentNullException(RecordTableEnumerator.b("堹医儽⬿", a_));
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x002942E0 File Offset: 0x002932E0
	private void ᜀ(XmlWriter A_0, sprἉ A_1)
	{
		int a_ = 4;
		int num = 3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				goto IL_61;
			case 1:
				goto IL_111;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
					goto IL_59;
				}
				break;
			case 4:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䤹吻嬽┿㙁ൃ≅", a_), num2.ToString());
				num = 1;
				continue;
			case 5:
				if (true)
				{
				}
				if (num2 != 0)
				{
					num = 4;
					continue;
				}
				goto IL_13B;
			case 6:
				goto IL_139;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 0;
			continue;
			IL_61:
			spr\u2141 spr_u = A_1.ᜄ();
			A_0.WriteStartElement(RecordTableEnumerator.b("帹夻堽⤿ⱁ⅃≅ه⭉⅋⭍", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("吹崻匽┿", a_), A_1.ᜃ());
			num2 = (int)spr_u.ᜋ();
			num = 5;
		}
		IL_59:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		IL_111:
		goto IL_13B;
		IL_139:
		throw new ArgumentNullException(RecordTableEnumerator.b("弹䐻䨽┿ぁ⩃ࡅ⥇❉⥋", a_));
		IL_13B:
		A_0.WriteEndElement();
	}

	// Token: 0x060044DD RID: 17629 RVA: 0x00294430 File Offset: 0x00293430
	private void ᜊ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 12;
		int num = 10;
		for (;;)
		{
			string text;
			sprᡟ sprᡟ;
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			case 1:
				if (spr\u1B7A.ᜀ(A_1.Shapes))
				{
					num = 11;
					continue;
				}
				return;
			case 2:
				goto IL_F9;
			case 3:
				return;
			case 4:
				if (text == null)
				{
					num = 5;
					continue;
				}
				goto IL_F9;
			case 5:
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
					sprᡟ.ᜃ(text = sprᡟ.ᜇ().GenerateRelationId());
					sprᡟ.ᜇ()[text] = null;
					num = 2;
					continue;
				}
				break;
			case 6:
				num = 1;
				continue;
			case 7:
				goto IL_F4;
			case 8:
				if (A_1.Shapes.Count - A_1.VmlShapesCount - A_1.AutoFilters.Count <= 0)
				{
					num = 6;
					continue;
				}
				goto IL_5C;
			case 9:
				goto IL_57;
			case 11:
				goto IL_5C;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 0;
			continue;
			IL_5C:
			sprᡟ = A_1.DataHolder;
			text = sprᡟ.ᜊ();
			num = 4;
			continue;
			IL_F9:
			A_0.WriteStartElement(RecordTableEnumerator.b("♁㙃❅㽇⍉≋⥍", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("⭁⁃", a_), RecordTableEnumerator.b("⩁ぃ㉅㡇灉捋慍⍏ㅑ㱓㍕㕗㭙⽛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ剻ᅽꮃ횑ﮓ鍊풟趡隣隥颧鲩莫\udcad햯\udeb1햳습톷햹튻춽ꢿꯁ듃뗅", a_), text);
			A_0.WriteEndElement();
			num = 3;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_F4:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁⱃ⍅ⵇ㹉", a_));
	}

	// Token: 0x060044DE RID: 17630 RVA: 0x0029461C File Offset: 0x0029361C
	internal void ᜀ(XmlWriter A_0, XlsWorksheetBase A_1)
	{
		int a_ = 3;
		int num = 5;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D9;
			case 1:
			{
				if (!A_1.HasVmlShapes)
				{
					num = 2;
					continue;
				}
				sprᡟ sprᡟ = A_1.DataHolder;
				text = sprᡟ.ᜎ();
				num = 4;
				continue;
			}
			case 2:
				return;
			case 3:
			{
				sprᡟ sprᡟ;
				sprᡟ.ᜄ(text = sprᡟ.ᜇ().GenerateRelationId());
				sprᡟ.ᜇ()[text] = null;
				num = 0;
				continue;
			}
			case 4:
				if (text == null)
				{
					num = 3;
					continue;
				}
				goto IL_12C;
			case 6:
				goto IL_AC;
			case 7:
				goto IL_64;
			case 8:
				goto IL_9E;
			}
			if (A_0 != null)
			{
				num = 8;
				continue;
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
				num = 7;
				continue;
			}
			IL_9E:
			if (A_1 == null)
			{
				num = 6;
			}
			else
			{
				if (true)
				{
				}
				num = 1;
			}
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_AC:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_D9:
		IL_12C:
		A_0.WriteStartElement(RecordTableEnumerator.b("唸帺娼帾≀㩂ń㕆⡈㱊⑌ⅎ㙐", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("倸强", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ቼ᥾춈搜ﲐﮔ뚘ꦚ궜꾞鞠貢힤슦얨쪪\ud9ac욮\udeb0\uddb2운\udfb6킸쮺캼", a_), text);
		A_0.WriteEndElement();
	}

	// Token: 0x060044DF RID: 17631 RVA: 0x00294794 File Offset: 0x00293794
	private void ᜉ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				string text;
				sprᰑ sprᰑ;
				string text2;
				sprᡟ sprᡟ;
				int num3;
				switch (num)
				{
				case 0:
					text = RecordTableEnumerator.b("ॅчཉ᥋ṍᑏፑSፕݗ᭙ၛढ़⅟㭡㝣", a_);
					goto IL_3AD;
				case 1:
					goto IL_2A9;
				case 2:
					goto IL_417;
				case 3:
					if (sprᰑ.ᜉ() == OleLinkType.Link)
					{
						num = 16;
						continue;
					}
					goto IL_16F;
				case 5:
					goto IL_259;
				case 6:
					if (A_1 == null)
					{
						num = 10;
						continue;
					}
					num = 25;
					continue;
				case 7:
					A_0.WriteAttributeString(RecordTableEnumerator.b("≅㹇୉㽋㹍㕏ㅑ⁓", a_), sprᰑ.\u170D().ToString());
					num = 23;
					continue;
				case 8:
					goto IL_3D1;
				case 9:
					goto IL_2C9;
				case 10:
					goto IL_4BA;
				case 11:
					text2 = sprᰑ.ᜌ();
					num = 17;
					continue;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3D1;
					default:
						goto IL_DA;
					}
					break;
				case 13:
					if (sprᰑ.ᜉ() == OleLinkType.Embed)
					{
						num = 27;
						continue;
					}
					goto IL_241;
				case 14:
					text2 = sprᡟ.ᜇ().GenerateRelationId();
					sprᰑ.ᜂ(text2);
					num = 5;
					continue;
				case 15:
					if (sprᰑ.\u170D() == DVAspect.DVASPECT_ICON)
					{
						num = 7;
						continue;
					}
					goto IL_472;
				case 16:
				{
					int num2 = sprᰑ.\u1715() + 1;
					string value = string.Format(RecordTableEnumerator.b("ᵅ㍇穉ㅋፍ煏畑獓煕罗", a_), num2);
					A_0.WriteAttributeString(RecordTableEnumerator.b("⩅ⅇ⑉❋", a_), value);
					num = 21;
					continue;
				}
				case 17:
					if (text2 == null)
					{
						num = 14;
						continue;
					}
					goto IL_259;
				case 18:
					return;
				case 19:
					num = 0;
					continue;
				case 20:
					if (sprᰑ.ᜉ() == OleLinkType.Embed)
					{
						num = 11;
						continue;
					}
					goto IL_417;
				case 21:
					if (sprᰑ.\u170D() != DVAspect.DVASPECT_ICON)
					{
						num = 19;
						continue;
					}
					num = 26;
					continue;
				case 22:
					goto IL_2A9;
				case 23:
					if (true)
					{
					}
					goto IL_472;
				case 24:
				{
					if (A_1.OleObjects.Count == 0)
					{
						num = 18;
						continue;
					}
					text2 = null;
					sprᡟ = A_1.DataHolder;
					A_0.WriteStartElement(RecordTableEnumerator.b("⥅⑇⽉͋ⱍ㩏㝑㝓≕⭗", a_));
					sprᜭ sprᜭ = (sprᜭ)A_1.OleObjects;
					num3 = 0;
					int count = sprᜭ.Count;
					num = 22;
					continue;
				}
				case 25:
					if (A_1.HasOleObjects)
					{
						num = 28;
						continue;
					}
					return;
				case 26:
					text = RecordTableEnumerator.b("ॅчཉ᥋ṍᑏፑSፕݗᕙቛᵝ⅟⹡⡣", a_);
					goto IL_3AD;
				case 27:
					A_0.WriteAttributeString(RecordTableEnumerator.b("⽅ⱇ", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막", a_), text2);
					num = 29;
					continue;
				case 28:
					num = 24;
					continue;
				case 29:
					goto IL_241;
				case 30:
				{
					int count;
					if (num3 >= count)
					{
						num = 9;
						continue;
					}
					sprᜭ sprᜭ;
					sprᰑ = (sprᰑ)sprᜭ[num3];
					num = 20;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num = 6;
				continue;
				IL_16F:
				int shapeId = (sprᰑ.ᜂ() as XlsShape).ShapeId;
				A_0.WriteAttributeString(RecordTableEnumerator.b("㕅⁇⭉㱋⭍᥏㙑", a_), shapeId.ToString());
				num = 13;
				continue;
				IL_3D1:
				goto IL_16F;
				IL_241:
				A_0.WriteEndElement();
				num3++;
				num = 1;
				continue;
				IL_259:
				sprᡟ.ᜇ()[text2] = null;
				num = 2;
				continue;
				IL_2A9:
				num = 30;
				continue;
				IL_3AD:
				string value2 = text;
				A_0.WriteAttributeString(RecordTableEnumerator.b("⥅⑇⽉᥋㹍㑏㍑⁓㍕", a_), value2);
				num = 8;
				continue;
				IL_417:
				A_0.WriteStartElement(RecordTableEnumerator.b("⥅⑇⽉͋ⱍ㩏㝑㝓≕", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㙅㩇╉⭋ݍ㑏", a_), spr\u20E9.ᜀ(sprᰑ.ᜏ()));
				num = 15;
				continue;
				IL_472:
				num = 3;
			}
			IL_DA:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_2C9:
			A_0.WriteEndElement();
			return;
			IL_4BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		}
		}
	}

	// Token: 0x060044E0 RID: 17632 RVA: 0x00294C68 File Offset: 0x00293C68
	internal static void ᜀ(XmlWriter A_0, XlsWorksheetBase A_1, spr\u171C A_2, RelationsCollection A_3)
	{
		int a_ = 16;
		int num = 9;
		string text;
		for (;;)
		{
			sprᡟ sprᡟ;
			switch (num)
			{
			case 0:
				if (A_3 == null)
				{
					goto IL_6C;
				}
				goto IL_114;
			case 1:
				goto IL_114;
			case 2:
				goto IL_5C;
			case 3:
				goto IL_135;
			case 4:
				num = 0;
				continue;
			case 5:
				goto IL_19B;
			case 6:
			{
				XlsHeaderFooterShapeCollection xlsHeaderFooterShapeCollection;
				if (xlsHeaderFooterShapeCollection.Count == 0)
				{
					num = 5;
					continue;
				}
				sprᡟ = A_1.DataHolder;
				text = sprᡟ.ᜅ();
				num = 11;
				continue;
			}
			case 7:
			{
				XlsHeaderFooterShapeCollection xlsHeaderFooterShapeCollection;
				if (xlsHeaderFooterShapeCollection != null)
				{
					num = 13;
					continue;
				}
				return;
			}
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6C;
				default:
					if (false)
					{
					}
					A_3 = sprᡟ.ᜇ();
					num = 1;
					continue;
				}
				break;
			case 10:
				goto IL_B7;
			case 11:
				if (text == null)
				{
					num = 4;
					continue;
				}
				goto IL_1A0;
			case 12:
			{
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 10;
					continue;
				}
				XlsHeaderFooterShapeCollection xlsHeaderFooterShapeCollection = A_1.InnerHeaderFooterShapes;
				num = 7;
				continue;
			}
			case 13:
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 12;
			continue;
			IL_6C:
			num = 8;
			continue;
			IL_114:
			sprᡟ.ᜁ(text = A_3.GenerateRelationId());
			A_3[text] = null;
			num = 3;
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_B7:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		IL_135:
		goto IL_1A0;
		IL_19B:
		return;
		IL_1A0:
		A_0.WriteStartElement(RecordTableEnumerator.b("⩅ⵇⵉⵋⵍ⥏ᙑ♓㝕⽗㍙㉛㥝⡟②", a_), A_2.ᜇ());
		A_0.WriteAttributeString(RecordTableEnumerator.b("⽅ⱇ", a_), RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막", a_), text);
		A_0.WriteEndElement();
	}

	// Token: 0x060044E1 RID: 17633 RVA: 0x00294E58 File Offset: 0x00293E58
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, IDictionary<string, int> A_2)
	{
		int a_ = 19;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						A_0.WriteStartElement(RecordTableEnumerator.b("⩈⑊⁌≎㑐㵒⅔᭖じ⡚⥜", a_));
						int num2 = 0;
						XlsCommentsCollection innerComments;
						int count = innerComments.Count;
						num = 6;
						continue;
					}
					case 1:
					{
						if (A_2 == null)
						{
							num = 2;
							continue;
						}
						XlsCommentsCollection innerComments = A_1.InnerComments;
						int count2 = innerComments.Count;
						num = 4;
						continue;
					}
					case 2:
						goto IL_1A8;
					case 3:
						goto IL_188;
					case 4:
					{
						int count2;
						if (count2 > 0)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 5:
						goto IL_109;
					case 6:
						goto IL_10E;
					case 7:
						if (A_1 == null)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					case 8:
						goto IL_10E;
					case 9:
						A_0.WriteEndElement();
						if (true)
						{
						}
						num = 3;
						continue;
					case 10:
						goto IL_76;
					case 12:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						XlsCommentsCollection innerComments;
						ICommentShape a_2 = innerComments[num2];
						this.ᜀ(A_0, a_2, A_2);
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					}
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 7;
					continue;
					IL_10E:
					num = 12;
				}
				break;
			}
			}
		}
		IL_76:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_109:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
		IL_188:
		return;
		IL_1A8:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ≊⹌๎⑐❒㵔㡖⭘⡚", a_));
	}

	// Token: 0x060044E2 RID: 17634 RVA: 0x00295038 File Offset: 0x00294038
	private void ᜀ(XmlWriter A_0, ICommentShape A_1, IDictionary<string, int> A_2)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				num = 2;
				continue;
			case 1:
				goto IL_5E;
			case 2:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				goto IL_DB;
			case 4:
				goto IL_D9;
			case 5:
				goto IL_46;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 0;
			}
		}
		IL_46:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼倾ⱀ⹂⁄⥆㵈", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		}
		IL_5E:
		throw new ArgumentNullException(RecordTableEnumerator.b("夼嘾≀ɂい㍆ⅈ⑊㽌㱎", a_));
		IL_D9:
		goto IL_60;
		IL_DB:
		string value = sprṔ.ᜂ(A_1.Column, A_1.Row);
		int num2 = A_2[A_1.Author];
		XlsComment xlsComment = (XlsComment)A_1;
		A_0.WriteStartElement(RecordTableEnumerator.b("帼倾ⱀ⹂⁄⥆㵈", a_));
		A_0.WriteAttributeString(RecordTableEnumerator.b("似娾❀", a_), value);
		A_0.WriteAttributeString(RecordTableEnumerator.b("尼䨾㕀⭂⩄㕆H⽊", a_), num2.ToString());
		A_0.WriteStartElement(RecordTableEnumerator.b("䤼娾㥀㝂", a_));
		this.ᜀ(A_0, xlsComment.InnerRichText.TextObject);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x060044E3 RID: 17635 RVA: 0x002951BC File Offset: 0x002941BC
	private IDictionary<string, int> ᜈ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 2;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						int count;
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							XlsCommentsCollection innerComments;
							ICommentShape commentShape = innerComments[num2];
							string author = commentShape.Author;
							num = 6;
							continue;
						}
						}
						break;
					}
					case 1:
						A_0.WriteEndElement();
						num = 9;
						continue;
					case 2:
						goto IL_104;
					case 3:
					{
						if (A_1 == null)
						{
							num = 10;
							continue;
						}
						XlsCommentsCollection innerComments = A_1.InnerComments;
						int num3 = 0;
						IDictionary<string, int> dictionary = null;
						int count = innerComments.Count;
						num = 13;
						continue;
					}
					case 5:
					{
						string author;
						A_0.WriteElementString(RecordTableEnumerator.b("夷伹䠻嘽⼿ぁ", a_), author);
						int num3;
						IDictionary<string, int> dictionary;
						dictionary.Add(author, num3);
						num3++;
						num = 8;
						continue;
					}
					case 6:
					{
						string author;
						IDictionary<string, int> dictionary;
						if (!dictionary.ContainsKey(author))
						{
							num = 5;
							continue;
						}
						goto IL_72;
					}
					case 7:
					{
						IDictionary<string, int> dictionary = new Dictionary<string, int>();
						A_0.WriteStartElement(RecordTableEnumerator.b("夷伹䠻嘽⼿ぁ㝃", a_));
						num2 = 0;
						num = 2;
						continue;
					}
					case 8:
						goto IL_72;
					case 9:
					{
						IDictionary<string, int> dictionary;
						return dictionary;
					}
					case 10:
						goto IL_F7;
					case 11:
						goto IL_6D;
					case 12:
						goto IL_104;
					case 13:
					{
						int count;
						if (count > 0)
						{
							num = 7;
							continue;
						}
						IDictionary<string, int> dictionary;
						return dictionary;
					}
					}
					if (A_0 == null)
					{
						num = 11;
						continue;
					}
					num = 3;
					continue;
					IL_72:
					num2++;
					num = 12;
					continue;
					IL_104:
					num = 0;
				}
				break;
			}
			}
		}
		IL_6D:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_F7:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
	}

	// Token: 0x060044E4 RID: 17636 RVA: 0x002953C8 File Offset: 0x002943C8
	private void ᜇ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 9;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				A_0.WriteStartElement(RecordTableEnumerator.b("嬾⡀⹂⁄⥆㩈≊≌ⅎ", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䴾⑀╂", a_), A_1.AllocatedRange.RangeAddressLocal);
				A_0.WriteEndElement();
				num = 0;
				continue;
			case 2:
				if (A_1.FirstColumn > 0)
				{
					goto IL_91;
				}
				return;
			case 3:
				if (A_1.LastColumn <= A_1.Workbook.MaxColumnCount)
				{
					num = 1;
					continue;
				}
				return;
			case 5:
				num = 2;
				continue;
			case 6:
				goto IL_53;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_91;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 8:
				if (A_1.FirstRow > 0)
				{
					num = 5;
					continue;
				}
				return;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 6;
				continue;
			}
			num = 8;
			continue;
			IL_91:
			num = 7;
		}
		IL_53:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
	}

	// Token: 0x060044E5 RID: 17637 RVA: 0x00295518 File Offset: 0x00294518
	private void ᜆ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			ViewMode viewMode;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_21E:
				viewMode = A_1.ViewMode;
				num = 12;
				break;
			default:
				if (false)
				{
				}
				num = 11;
				break;
			}
			for (;;)
			{
				IXLSRange topLeftCell;
				switch (num)
				{
				case 0:
					goto IL_2DE;
				case 1:
					A_0.WriteAttributeString(RecordTableEnumerator.b("䴺吼娾㙀", a_), RecordTableEnumerator.b("䬺尼堾⑀ł㝄≆⡈⁊ᵌ㵎㑐╒㱔㉖⹘", a_));
					num = 14;
					continue;
				case 2:
				{
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䠺唼娾⑀㝂ፄ⹆ⱈ㱊㹌", a_));
					A_0.WriteStartElement(RecordTableEnumerator.b("䠺唼娾⑀㝂ፄ⹆ⱈ㱊", a_));
					IWorkbook workbook = A_1.Workbook;
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䰺吼儾╀ⱂ㉄ᝆ㭈⑊㥌⩎㉐❒㱔㡖㝘", a_), workbook.IsWindowProtection, false);
					topLeftCell = A_1.TopLeftCell;
					num = 5;
					continue;
				}
				case 3:
					if (topLeftCell != null)
					{
						num = 17;
						continue;
					}
					goto IL_38A;
				case 4:
					num = 3;
					continue;
				case 5:
					if (!A_1.IsFreezePanes)
					{
						num = 4;
						continue;
					}
					goto IL_38A;
				case 6:
					goto IL_48D;
				case 7:
					goto IL_219;
				case 8:
					goto IL_287;
				case 9:
					goto IL_1F6;
				case 10:
					goto IL_311;
				case 12:
					switch (viewMode)
					{
					case ViewMode.Normal:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䴺吼娾㙀", a_), RecordTableEnumerator.b("唺刼䴾ⱀ≂⥄", a_));
						num = 9;
						continue;
					case ViewMode.Preview:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䴺吼娾㙀", a_), RecordTableEnumerator.b("䬺尼堾⑀ł㝄≆⡈⁊ᵌ㵎㑐╒㱔㉖⹘", a_));
						num = 13;
						continue;
					case ViewMode.Layout:
						A_0.WriteAttributeString(RecordTableEnumerator.b("䴺吼娾㙀", a_), RecordTableEnumerator.b("䬺尼堾⑀ག⑄㹆♈㹊㥌", a_));
						num = 10;
						continue;
					default:
						num = 15;
						continue;
					}
					break;
				case 13:
					goto IL_282;
				case 14:
					goto IL_F5;
				case 15:
					num = 0;
					continue;
				case 16:
					goto IL_38A;
				case 17:
					num = 22;
					continue;
				case 18:
					if (A_1.WindowTwo.\u170D())
					{
						num = 1;
						continue;
					}
					goto IL_21E;
				case 19:
					if (!A_1.WindowTwo.ᜅ())
					{
						num = 20;
						continue;
					}
					goto IL_48D;
				case 20:
					A_0.WriteAttributeString(RecordTableEnumerator.b("强堼夾⁀㙂⥄㍆่㥊⑌⭎ቐ㱒㥔㡖⭘", a_), RecordTableEnumerator.b("଺", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("堺刼匾⹀ㅂౄ⍆", a_), ((int)A_1.GridLineColor).ToString());
					num = 6;
					continue;
				case 21:
					goto IL_C2;
				case 22:
					if (topLeftCell.Row == 1)
					{
						num = 23;
						continue;
					}
					goto IL_287;
				case 23:
					num = 24;
					continue;
				case 24:
					if (topLeftCell.Column != 1)
					{
						num = 8;
						continue;
					}
					goto IL_38A;
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				num = 2;
				continue;
				IL_287:
				A_0.WriteAttributeString(RecordTableEnumerator.b("伺刼伾ീ♂⍄㍆ੈ⹊⅌⍎", a_), topLeftCell.RangeAddressLocal);
				num = 16;
				continue;
				IL_38A:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀т㝄⹆ⵈ݊⑌ⅎ㑐⁒", a_), A_1.GridLinesVisible, true);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀ᅂ⩄うੈ⑊⅌ݎ㑐㉒ㅔ㉖⭘⡚", a_), A_1.RowColumnHeadersVisible, true);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䠺唼倾㙀᥂⁄㕆♈㡊", a_), A_1.IsDisplayZeros, true);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䄺刼倾ⱀ၂♄♆╈⹊", a_), A_1.Excel2007ZoomScale, 100);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䄺刼倾ⱀ၂♄♆╈⹊͌⁎⍐㹒㑔㭖", a_), A_1.RealZoomScaleNormal, 0);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䄺刼倾ⱀ၂♄♆╈⹊Ṍ❎㑐㙒⅔᭖㡘≚㉜⩞ᕠ㕢౤ɦṨ", a_), A_1.RealZoomScalePageBreakView, 0);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䄺刼倾ⱀ၂♄♆╈⹊ᵌ⹎㙐㙒ᥔ㙖⁘㑚⡜⭞㝠੢dၦ", a_), A_1.RealZoomScalePageLayoutView, 0);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䤺吼堾⥀㝂ᅄ⡆Ո⹊⭌㭎", a_), A_1.IsRightToLeft, false);
				num = 19;
				continue;
				IL_48D:
				num = 18;
			}
			IL_C2:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_F5:
			IL_1F6:
			goto IL_4BA;
			IL_219:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
			IL_282:
			IL_2DE:
			IL_311:
			IL_4BA:
			if (true)
			{
			}
			A_0.WriteAttributeString(RecordTableEnumerator.b("䰺刼䴾⩀⅂⩄⡆≈ᵊ⑌⩎♐ᩒㅔ", a_), RecordTableEnumerator.b("଺", a_));
			this.ᜄ(A_0, A_1);
			this.ᜅ(A_0, A_1);
			A_0.WriteEndElement();
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x00295A28 File Offset: 0x00294A28
	private void ᜅ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 8;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_EB:
			goto IL_67;
		default:
			if (false)
			{
			}
			goto IL_45;
		}
		int num;
		IXLSRange ixlsrange;
		string rangeAddressLocal;
		for (;;)
		{
			IL_27:
			switch (num)
			{
			case 0:
				if (ixlsrange != null)
				{
					num = 5;
					continue;
				}
				return;
			case 1:
				if (A_1.Pane != null)
				{
					num = 4;
					continue;
				}
				goto IL_67;
			case 2:
				return;
			case 3:
				goto IL_EB;
			case 4:
				if (true)
				{
				}
				A_0.WriteAttributeString(RecordTableEnumerator.b("丽ℿⱁ⅃", a_), ((sprᱭ.ActivePane)this.ᜀ(A_1.Pane)).ToString());
				num = 3;
				continue;
			case 5:
				rangeAddressLocal = ixlsrange.RangeAddressLocal;
				A_0.WriteStartElement(RecordTableEnumerator.b("䴽┿⹁⅃╅㱇⍉⍋⁍", a_));
				num = 1;
				continue;
			}
			goto IL_45;
		}
		return;
		IL_45:
		ixlsrange = A_1.ᜬ();
		num = 0;
		goto IL_27;
		IL_67:
		A_0.WriteAttributeString(RecordTableEnumerator.b("弽⌿㙁ⵃぅⵇॉ⥋≍㱏", a_), rangeAddressLocal);
		A_0.WriteAttributeString(RecordTableEnumerator.b("䴽ㄿぁ⅃⁅", a_), rangeAddressLocal);
		A_0.WriteEndElement();
		num = 2;
		goto IL_27;
	}

	// Token: 0x060044E7 RID: 17639 RVA: 0x00295B68 File Offset: 0x00294B68
	private void ᜄ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 11;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					spr\u2408 spr_u;
					sprṫ sprṫ;
					switch (num)
					{
					case 0:
					{
						string value = RecordTableEnumerator.b("❀ㅂ⩄㵆ⱈ╊", a_);
						A_0.WriteAttributeString(RecordTableEnumerator.b("㉀㝂⑄㍆ⱈ", a_), value);
						num = 15;
						continue;
					}
					case 1:
						goto IL_317;
					case 2:
						num = 23;
						continue;
					case 3:
						num = 8;
						continue;
					case 4:
						if (A_1.VerticalSplit == 0)
						{
							num = 2;
							continue;
						}
						goto IL_37B;
					case 5:
						goto IL_22B;
					case 6:
						goto IL_317;
					case 7:
						if (spr_u.ᜃ() <= 0)
						{
							num = 3;
							continue;
						}
						goto IL_E7;
					case 8:
						if (spr_u.ᜄ() > 0)
						{
							num = 25;
							continue;
						}
						return;
					case 9:
						if (sprṫ.ᜀ())
						{
							num = 0;
							continue;
						}
						goto IL_255;
					case 10:
						if (!A_1.IsFreezePanes)
						{
							num = 16;
							continue;
						}
						goto IL_37B;
					case 11:
						num = 9;
						continue;
					case 12:
						if (sprṫ.ᜁ())
						{
							num = 26;
							continue;
						}
						goto IL_230;
					case 13:
						if (sprṫ.ᜁ())
						{
							num = 11;
							continue;
						}
						goto IL_255;
					case 14:
						if (!sprṫ.ᜀ())
						{
							num = 17;
							continue;
						}
						goto IL_230;
					case 15:
						goto IL_317;
					case 16:
						num = 4;
						continue;
					case 17:
					{
						string value = RecordTableEnumerator.b("❀ㅂ⩄㵆ⱈ╊Ṍ㽎㵐㩒⅔", a_);
						A_0.WriteAttributeString(RecordTableEnumerator.b("㉀㝂⑄㍆ⱈ", a_), value);
						num = 6;
						continue;
					}
					case 19:
						num = 7;
						continue;
					case 20:
						goto IL_AB;
					case 21:
						if (spr_u != null)
						{
							num = 19;
							continue;
						}
						return;
					case 22:
						if (A_1 == null)
						{
							num = 5;
							continue;
						}
						num = 10;
						continue;
					case 23:
						if (A_1.HorizontalSplit != 0)
						{
							num = 24;
							continue;
						}
						return;
					case 24:
						goto IL_37B;
					case 25:
						goto IL_E7;
					case 26:
						num = 14;
						continue;
					case 27:
						return;
					}
					if (A_0 == null)
					{
						num = 20;
						continue;
					}
					num = 22;
					continue;
					IL_E7:
					A_0.WriteStartElement(RecordTableEnumerator.b("ㅀ≂⭄≆", a_));
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㥀၂㕄⭆⁈㽊", a_), spr_u.ᜃ(), 0);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㡀၂㕄⭆⁈㽊", a_), spr_u.ᜄ(), 0);
					string value2 = sprṔ.ᜂ(spr_u.ᜅ() + 1, spr_u.ᜀ() + 1);
					A_0.WriteAttributeString(RecordTableEnumerator.b("㕀ⱂ㕄୆ⱈⵊ㥌౎㑐㽒㥔", a_), value2);
					string value3 = ((sprᱭ.ActivePane)spr_u.ᜆ()).ToString();
					A_0.WriteAttributeString(RecordTableEnumerator.b("⁀⁂ㅄ⹆㽈⹊ᵌ⹎㽐㙒", a_), value3);
					sprṫ = A_1.WindowTwo;
					num = 12;
					continue;
					IL_230:
					num = 13;
					continue;
					IL_255:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						string value = RecordTableEnumerator.b("㉀㍂⥄⹆㵈", a_);
						num = 1;
						continue;
					}
					}
					IL_317:
					A_0.WriteEndElement();
					num = 27;
					continue;
					IL_37B:
					spr_u = A_1.Pane;
					num = 21;
				}
				break;
			}
			}
		}
		IL_AB:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_22B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
	}

	// Token: 0x060044E8 RID: 17640 RVA: 0x00295F6C File Offset: 0x00294F6C
	private void ᜀ(XmlWriter A_0, Stream A_1)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1B7A.ᜀ(A_0, A_1, RecordTableEnumerator.b("㝄⡆♈㽊", a_));
	}

	// Token: 0x060044E9 RID: 17641 RVA: 0x00295FC8 File Offset: 0x00294FC8
	public static void ᜀ(XmlWriter A_0, Stream A_1, string A_2)
	{
		int a_ = 10;
		int num = 7;
		for (;;)
		{
			if (true)
			{
			}
			XmlReader xmlReader;
			switch (num)
			{
			case 0:
				num = 18;
				continue;
			case 1:
				return;
			case 2:
				if (!(xmlReader.Name == RecordTableEnumerator.b("㈿ⵁ⭃㉅", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_1BF;
			case 3:
				if (xmlReader.Name != A_2)
				{
					num = 4;
					continue;
				}
				goto IL_164;
			case 4:
				num = 6;
				continue;
			case 5:
				if (!xmlReader.EOF)
				{
					num = 10;
					continue;
				}
				return;
			case 6:
				IL_DE:
				if (!(xmlReader.Name != RecordTableEnumerator.b("㈿ⵁ⭃㉅", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_1D3;
			case 8:
				num = 9;
				continue;
			case 9:
				if (A_1.Length > 0L)
				{
					num = 16;
					continue;
				}
				return;
			case 10:
				num = 3;
				continue;
			case 11:
				num = 2;
				continue;
			case 12:
				goto IL_207;
			case 13:
				goto IL_84;
			case 14:
				if (xmlReader.NodeType == XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				goto IL_1D3;
			case 15:
				goto IL_164;
			case 16:
				A_1.Position = 0L;
				xmlReader = UtilityMethods.ᜀ(A_1);
				num = 17;
				continue;
			case 17:
				goto IL_207;
			case 18:
				goto IL_84;
			case 19:
				if (!(xmlReader.Name == A_2))
				{
					num = 11;
					continue;
				}
				goto IL_1BF;
			}
			if (A_1 != null)
			{
				num = 8;
				continue;
			}
			break;
			IL_84:
			num = 5;
			continue;
			IL_1D3:
			A_0.WriteNode(xmlReader, false);
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_DE;
			default:
				if (false)
				{
				}
				num = 13;
				continue;
			}
			IL_164:
			num = 14;
			continue;
			IL_1BF:
			xmlReader.Read();
			num = 12;
			continue;
			IL_207:
			num = 19;
		}
	}

	// Token: 0x060044EA RID: 17642 RVA: 0x00296208 File Offset: 0x00295208
	private void ᜀ(XmlWriter A_0, string A_1, sprᦨ A_2)
	{
		int a_ = 9;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_2.ᜀ())
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				goto IL_199;
			case 1:
				goto IL_8C;
			case 2:
				goto IL_DA;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40;
				default:
					if (false)
					{
					}
					A_0.WriteAttributeString(RecordTableEnumerator.b("款⁀ㅂ≄≆㵈ي≌⭎㑐", a_), RecordTableEnumerator.b("稾㥀㝂⁄㕆❈⩊⅌", a_));
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_77;
			case 5:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 8;
				continue;
			case 6:
				goto IL_48;
			case 8:
				if (A_2 == null)
				{
					num = 4;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("派⑀⽂⑄㍆⁈⑊⍌㱎㥐㩒╔", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("瘾╀", a_), A_1);
				A_0.WriteAttributeString(RecordTableEnumerator.b("款㡀㍂⁄", a_), A_2.ᜃ());
				A_0.WriteAttributeString(RecordTableEnumerator.b("款⁀ㅂ≄≆㵈", a_), A_2.ᜂ());
				num = 0;
				continue;
			}
			goto IL_3D;
			IL_40:
			num = 6;
			continue;
			IL_3D:
			if (A_0 == null)
			{
				goto IL_40;
			}
			num = 5;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_77:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⽂⑄㍆⁈⑊⍌", a_));
		IL_8C:
		throw new ArgumentNullException(RecordTableEnumerator.b("吾⑀㩂", a_));
		IL_DA:
		IL_199:
		A_0.WriteEndElement();
	}

	// Token: 0x060044EB RID: 17643 RVA: 0x002963B4 File Offset: 0x002953B4
	private void ᜇ(XmlWriter A_0)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			int num2;
			int count;
			ITabSheets tabSheets;
			switch (num)
			{
			case 1:
				goto IL_4B;
			case 2:
				goto IL_4D;
			case 3:
				if (num2 >= count)
				{
					num = 5;
					continue;
				}
				num = 8;
				continue;
			case 4:
				goto IL_97;
			case 5:
				goto IL_D7;
			case 6:
				goto IL_117;
			case 7:
				goto IL_97;
			case 8:
				if (((XlsWorksheetBase)tabSheets[num2]).ᜠ != null)
				{
					num = 6;
					continue;
				}
				goto IL_4D;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("あⵄ≆ⱈ㽊㹌", a_));
			tabSheets = this.ᡇ.TabSheets;
			num2 = 0;
			count = tabSheets.Count;
			if (true)
			{
			}
			num = 4;
			continue;
			IL_4D:
			num2++;
			num = 7;
			continue;
			IL_97:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_117:
				this.ᜀ(A_0, tabSheets[num2]);
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_D7:
		A_0.WriteEndElement();
	}

	// Token: 0x060044EC RID: 17644 RVA: 0x002964FC File Offset: 0x002954FC
	private void ᜀ(XmlWriter A_0, ITabSheet A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				sprᡟ sprᡟ;
				string text;
				XLSXVisibility visibility;
				string text3;
				switch (num)
				{
				case 0:
					if (sprᡟ == null)
					{
						num = 3;
						continue;
					}
					num = 13;
					continue;
				case 1:
					goto IL_D8;
				case 2:
					goto IL_1EB;
				case 3:
					num = 4;
					continue;
				case 4:
					text = null;
					goto IL_210;
				case 5:
				{
					string text2 = visibility.ToString();
					text2 = spr\u1B7A.ᜄ(text2);
					A_0.WriteAttributeString(RecordTableEnumerator.b("䠺䤼帾㕀♂", a_), text2);
					num = 2;
					continue;
				}
				case 6:
					IL_C6:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					sprᡟ = ((XlsWorksheetBase)A_1).ᜠ;
					num = 0;
					continue;
				case 7:
					goto IL_7D;
				case 8:
					if (sprᡟ != null)
					{
						num = 9;
						continue;
					}
					goto IL_DD;
				case 9:
					sprᡟ.ᜅ(text3);
					num = 11;
					continue;
				case 10:
					if (true)
					{
					}
					break;
				case 11:
					goto IL_DD;
				case 12:
					if (text3 == null)
					{
						num = 15;
						continue;
					}
					goto IL_DD;
				case 13:
					text = sprᡟ.\u170D();
					goto IL_210;
				case 14:
					if (visibility != XLSXVisibility.Visible)
					{
						num = 5;
						continue;
					}
					goto IL_26A;
				case 15:
					text3 = this.ᜂ();
					num = 8;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 6;
				continue;
				IL_DD:
				A_0.WriteStartElement(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("唺尼刾⑀", a_), A_1.Name);
				A_0.WriteAttributeString(RecordTableEnumerator.b("䠺唼娾⑀㝂ౄ⍆", a_), text3);
				A_0.WriteAttributeString(RecordTableEnumerator.b("刺夼", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼ၾ쾊ﺒ練뒚꾜꾞醠関誤햦첨잪첬\udbae\ud8b0\udcb2\udbb4쒶톸튺춼첾", a_), sprᡟ.ᜌ());
				visibility = (XLSXVisibility)A_1.Visibility;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C6;
				default:
					if (false)
					{
					}
					num = 14;
					continue;
				}
				IL_210:
				text3 = text;
				num = 12;
			}
			IL_7D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_D8:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
			IL_1EB:
			IL_26A:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044ED RID: 17645 RVA: 0x0029677C File Offset: 0x0029577C
	private string ᜂ()
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				XlsWorkbookObjectsCollection objects = this.ᡇ.Objects;
				num = 0;
				int num2 = 0;
				int count = objects.Count;
				int num3 = 5;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						sprᡟ sprᡟ;
						string text = sprᡟ.\u170D();
						num3 = 11;
						continue;
					}
					case 1:
						goto IL_79;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							if (num2 >= count)
							{
								num3 = 12;
								continue;
							}
							XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)objects[num2];
							sprᡟ sprᡟ = xlsWorksheetBase.DataHolder;
							num3 = 6;
							continue;
						}
						}
						break;
					case 3:
					{
						int num4;
						num = num4;
						num3 = 1;
						continue;
					}
					case 4:
						num3 = 7;
						continue;
					case 5:
						goto IL_CA;
					case 6:
					{
						sprᡟ sprᡟ;
						if (sprᡟ != null)
						{
							num3 = 0;
							continue;
						}
						goto IL_147;
					}
					case 7:
					{
						string text;
						int num4;
						if (int.TryParse(text, out num4))
						{
							num3 = 8;
							continue;
						}
						goto IL_147;
					}
					case 8:
						num3 = 10;
						continue;
					case 9:
						goto IL_CA;
					case 10:
					{
						int num4;
						if (num4 > num)
						{
							num3 = 3;
							continue;
						}
						goto IL_147;
					}
					case 11:
					{
						string text;
						if (text != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_147;
					}
					case 12:
						goto IL_10A;
					}
					break;
					IL_CA:
					num3 = 2;
					continue;
					IL_147:
					num2++;
					num3 = 9;
					continue;
					IL_79:
					goto IL_147;
				}
			}
			IL_10A:
			return (num + 1).ToString();
		}
		}
	}

	// Token: 0x060044EE RID: 17646 RVA: 0x00296924 File Offset: 0x00295924
	private string ᜀ(spr\u25A6.ᜀ A_0)
	{
		int a_ = 13;
		if (A_0 == null)
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
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⁆⁈⑊⍌", a_));
		}
		string str = sprṔ.ᜂ(A_0.ᜅ() + 1, A_0.ᜂ() + 1);
		string str2 = sprṔ.ᜂ(A_0.ᜃ() + 1, A_0.ᜇ() + 1);
		return str + RecordTableEnumerator.b("祂", a_) + str2;
	}

	// Token: 0x060044EF RID: 17647 RVA: 0x002969C4 File Offset: 0x002959C4
	private void ᜀ(XmlWriter A_0, INamedRange A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					goto IL_23D;
				case 1:
					if (!this.ᡇ.HasApostrophe)
					{
						num = 2;
						continue;
					}
					goto IL_F0;
				case 2:
					num = 8;
					continue;
				case 3:
					text = RecordTableEnumerator.b("ᠺ猼績ీق穄", a_);
					num = 0;
					continue;
				case 4:
					goto IL_94;
				case 6:
					goto IL_141;
				case 7:
					text = text.Replace(RecordTableEnumerator.b("᰺", a_), "");
					num = 15;
					continue;
				case 8:
					if (!this.ᜅ(text))
					{
						num = 7;
						continue;
					}
					goto IL_F0;
				case 9:
					goto IL_16B;
				case 10:
					if (text.StartsWith(RecordTableEnumerator.b("ᠺ漼稾݀", a_)))
					{
						goto IL_112;
					}
					goto IL_16B;
				case 11:
					text = RecordTableEnumerator.b("ᠺ漼稾݀扂", a_);
					num = 9;
					continue;
				case 12:
				{
					XlsName xlsName = (XlsName)A_1;
					XlsWorksheet worksheet = xlsName.Worksheet;
					string value = this.ᜀ(worksheet).ToString();
					A_0.WriteAttributeString(RecordTableEnumerator.b("场刼尾⁀⽂ᙄ⽆ⱈ⹊㥌َ㕐", a_), value);
					num = 20;
					continue;
				}
				case 13:
					return;
				case 14:
					A_0.WriteStartElement(RecordTableEnumerator.b("强堼夾⡀ⵂ⁄⍆݈⩊⁌⩎", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("唺尼刾⑀", a_), A_1.Name);
					num = 18;
					continue;
				case 15:
					goto IL_F0;
				case 16:
					if (!string.IsNullOrEmpty(text))
					{
						num = 14;
						continue;
					}
					return;
				case 17:
					if (text == null)
					{
						num = 3;
						continue;
					}
					goto IL_23D;
				case 18:
					if (A_1.IsLocal)
					{
						num = 12;
						continue;
					}
					goto IL_189;
				case 19:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					text = ((XlsName)A_1).GetValue(this.ᡈ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_112;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 20:
					goto IL_189;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 19;
				continue;
				IL_F0:
				num = 10;
				continue;
				IL_112:
				num = 11;
				continue;
				IL_16B:
				A_0.WriteString(text);
				A_0.WriteEndElement();
				num = 13;
				continue;
				IL_189:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("区吼嬾╀♂⭄", a_), !A_1.Visible, false);
				num = 17;
				continue;
				IL_23D:
				num = 1;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_141:
			throw new ArgumentNullException(RecordTableEnumerator.b("唺尼刾⑀", a_));
		}
		}
	}

	// Token: 0x060044F0 RID: 17648 RVA: 0x00296CF4 File Offset: 0x00295CF4
	private int ᜀ(XlsWorksheet A_0)
	{
		int a_ = 10;
		for (;;)
		{
			int num = -1;
			ITabSheets tabSheets = this.ᡇ.TabSheets;
			int num2 = 0;
			int num3 = 4;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_109;
				case 1:
					goto IL_FE;
				case 2:
					goto IL_52;
				case 3:
					if (true)
					{
					}
					num = num2;
					num3 = 6;
					continue;
				case 4:
					goto IL_FE;
				case 5:
					if (!(tabSheets[num2].Name == A_0.Name))
					{
						num2++;
						num3 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_109;
					default:
						if (false)
						{
						}
						num3 = 3;
						continue;
					}
					break;
				case 6:
					goto IL_52;
				case 7:
					if (num == -1)
					{
						num3 = 8;
						continue;
					}
					return num;
				case 8:
					goto IL_69;
				}
				break;
				IL_52:
				num3 = 7;
				continue;
				IL_109:
				if (num2 >= tabSheets.Count)
				{
					num3 = 2;
					continue;
				}
				num3 = 5;
				continue;
				IL_FE:
				num3 = 0;
			}
		}
		IL_69:
		throw new ArgumentException(RecordTableEnumerator.b("िⱁ㉃❅⑇⍉⡋湍͏㩑ㅓ㍕ⱗ", a_));
	}

	// Token: 0x060044F1 RID: 17649 RVA: 0x00296E28 File Offset: 0x00295E28
	private bool ᜅ(string A_0)
	{
		for (;;)
		{
			char[] array = A_0.ToCharArray();
			int num = 0;
			int num2 = array.Length;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						return true;
					case 1:
						if (!char.IsLetterOrDigit(array[num]))
						{
							num3 = 0;
							continue;
						}
						if (true)
						{
						}
						num++;
						num3 = 5;
						continue;
					case 2:
						if (num >= num2)
						{
							num3 = 4;
							continue;
						}
						num3 = 1;
						continue;
					case 3:
						goto IL_94;
					case 4:
						return false;
					case 5:
						goto IL_94;
					}
					break;
					IL_94:
					num3 = 2;
				}
				break;
			}
			}
		}
		return true;
	}

	// Token: 0x060044F2 RID: 17650 RVA: 0x00296EE8 File Offset: 0x00295EE8
	private void ᜆ(XmlWriter A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				int num2;
				int count;
				XlsFontsCollection innerFonts;
				switch (num)
				{
				case 0:
					goto IL_116;
				case 1:
					goto IL_116;
				case 3:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					IFont a_2 = innerFonts[num2];
					this.ᜀ(A_0, a_2, RecordTableEnumerator.b("娻儽⸿㙁", a_));
					num2++;
					num = 0;
					continue;
				}
				case 4:
					goto IL_135;
				case 5:
					goto IL_55;
				}
				IL_49:
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				innerFonts = this.ᡇ.InnerFonts;
				count = innerFonts.Count;
				A_0.WriteStartElement(RecordTableEnumerator.b("娻儽⸿㙁㝃", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("弻儽㔿ⱁぃ", a_), count.ToString());
				num2 = 0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				goto IL_49;
				IL_116:
				num = 3;
			}
			IL_55:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_135:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044F3 RID: 17651 RVA: 0x00297034 File Offset: 0x00296034
	private void ᜀ(XmlWriter A_0, IFont A_1, string A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int charSet;
				FontUnderlineType underline;
				string localName;
				switch (num)
				{
				case 0:
					if (charSet != 1)
					{
						num = 4;
						continue;
					}
					goto IL_15D;
				case 1:
					goto IL_309;
				case 2:
					A_0.WriteElementString(RecordTableEnumerator.b("⑅", a_), string.Empty);
					num = 1;
					continue;
				case 3:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("㍅", a_));
					string text = underline.ToString();
					text = char.ToLower(text[0]) + UtilityMethods.ᜀ(text);
					A_0.WriteAttributeString(RecordTableEnumerator.b("ぅ⥇♉", a_), text);
					A_0.WriteEndElement();
					num = 14;
					continue;
				}
				case 4:
					A_0.WriteStartElement(RecordTableEnumerator.b("╅⁇⭉㹋㵍㕏♑", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ぅ⥇♉", a_), charSet.ToString());
					A_0.WriteEndElement();
					num = 17;
					continue;
				case 5:
					goto IL_1C5;
				case 6:
					A_0.WriteStartElement(RecordTableEnumerator.b("ぅⵇ㡉㡋ཌྷ㱏㭑㍓㡕", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("ぅ⥇♉", a_), A_1.VerticalAlignment.ToString().ToLower(CultureInfo.InvariantCulture));
					A_0.WriteEndElement();
					num = 27;
					continue;
				case 7:
					this.ᜃ(A_0, RecordTableEnumerator.b("╅❇♉⍋㱍", a_), (A_1 as IInternalFont).Font.OColor);
					num = 5;
					continue;
				case 9:
					A_0.WriteElementString(RecordTableEnumerator.b("㕅㱇㡉╋╍㕏", a_), string.Empty);
					num = 19;
					continue;
				case 10:
					if (A_1.IsBold)
					{
						num = 2;
						continue;
					}
					goto IL_309;
				case 11:
					if (true)
					{
					}
					if (underline != FontUnderlineType.None)
					{
						num = 3;
						continue;
					}
					goto IL_1ED;
				case 12:
					goto IL_B0;
				case 13:
					if (A_1.IsStrikethrough)
					{
						num = 9;
						continue;
					}
					goto IL_3C6;
				case 14:
					goto IL_1ED;
				case 15:
					goto IL_349;
				case 16:
					if (A_1.IsItalic)
					{
						num = 26;
						continue;
					}
					goto IL_34E;
				case 17:
					goto IL_15D;
				case 18:
					if (A_1.VerticalAlignment != FontVertialAlignmentType.Baseline)
					{
						num = 6;
						continue;
					}
					goto IL_4C9;
				case 19:
					goto IL_3C6;
				case 20:
					goto IL_34E;
				case 21:
					if (A_1 == null)
					{
						num = 15;
						continue;
					}
					A_0.WriteStartElement(A_2);
					num = 10;
					continue;
				case 22:
					if (A_1.KnownColor != (ExcelColors)32767)
					{
						num = 7;
						continue;
					}
					goto IL_1C5;
				case 23:
					if (A_2 == RecordTableEnumerator.b("㑅ᡇ㡉", a_))
					{
						num = 24;
						continue;
					}
					goto IL_B0;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E7;
					default:
						if (false)
						{
						}
						localName = RecordTableEnumerator.b("㑅็╉≋㩍", a_);
						num = 12;
						continue;
					}
					break;
				case 25:
					goto IL_AB;
				case 26:
					A_0.WriteElementString(RecordTableEnumerator.b("⽅", a_), string.Empty);
					goto IL_2E7;
				case 27:
					goto IL_2C9;
				}
				if (A_0 == null)
				{
					num = 25;
					continue;
				}
				num = 21;
				continue;
				IL_B0:
				A_0.WriteStartElement(localName);
				A_0.WriteAttributeString(RecordTableEnumerator.b("ぅ⥇♉", a_), A_1.FontName);
				A_0.WriteEndElement();
				charSet = (int)((XlsFont)A_1).CharSet;
				num = 0;
				continue;
				IL_15D:
				num = 22;
				continue;
				IL_1C5:
				num = 18;
				continue;
				IL_1ED:
				num = 13;
				continue;
				IL_2E7:
				num = 20;
				continue;
				IL_309:
				num = 16;
				continue;
				IL_34E:
				underline = A_1.Underline;
				num = 11;
				continue;
				IL_3C6:
				A_0.WriteStartElement(RecordTableEnumerator.b("㕅㉇", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("ぅ⥇♉", a_), XmlConvert.ToString(A_1.Size));
				A_0.WriteEndElement();
				localName = RecordTableEnumerator.b("⡅⥇❉⥋", a_);
				num = 23;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_2C9:
			goto IL_4C9;
			IL_349:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅❇⑉㡋", a_));
			IL_4C9:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044F4 RID: 17652 RVA: 0x00297510 File Offset: 0x00296510
	private void ᜃ(XmlWriter A_0, string A_1, OColor A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.WriteStartElement(A_1);
				ColorType colorType = A_2.ColorType;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CE;
				default:
				{
					if (false)
					{
					}
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_FE;
						case 1:
							goto IL_CE;
						case 2:
							if (true)
							{
							}
							num = 4;
							continue;
						case 3:
							switch (colorType)
							{
							case ColorType.Known:
								A_0.WriteAttributeString(RecordTableEnumerator.b("⁈╊⥌⩎⥐㙒ㅔ", a_), A_2.Value.ToString());
								num = 0;
								continue;
							case ColorType.RGB:
								A_0.WriteAttributeString(RecordTableEnumerator.b("㭈ⱊ⽌", a_), A_2.Value.ToString(RecordTableEnumerator.b("ᅈ絊", a_)));
								num = 1;
								continue;
							case ColorType.Theme:
								A_0.WriteAttributeString(RecordTableEnumerator.b("㵈⍊⡌≎㑐", a_), A_2.Value.ToString());
								num = 5;
								continue;
							default:
								num = 2;
								continue;
							}
							break;
						case 4:
							goto IL_114;
						case 5:
							goto IL_144;
						}
						break;
					}
					break;
				}
				}
			}
			IL_CE:
			IL_FE:
			IL_114:
			IL_144:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㵈≊⍌㭎", a_), A_2.Tint, 0.0);
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x060044F5 RID: 17653 RVA: 0x0029768C File Offset: 0x0029668C
	private void ᜅ(XmlWriter A_0)
	{
		int a_ = 11;
		int num = 7;
		for (;;)
		{
			int count;
			List<spr\u240D> list;
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_B8;
			case 2:
				goto IL_82;
			case 3:
				goto IL_47;
			case 4:
			{
				int num2;
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				this.ᜀ(A_0, list[num2]);
				num2++;
				num = 2;
				continue;
			}
			case 5:
				goto IL_82;
			case 6:
			{
				if (count == 0)
				{
					num = 0;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⽀㙂⡄ņ⑈㽊㹌", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("≀ⱂい⥆㵈", a_), count.ToString());
				int num2 = 0;
				num = 5;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			list = this.ᡇ.InnerFormats.ᜀ(ExcelVersion.Version2007);
			count = list.Count;
			num = 6;
			continue;
			IL_82:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_47;
			default:
				if (false)
				{
				}
				num = 4;
				break;
			}
		}
		IL_47:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_B8:
		A_0.WriteEndElement();
	}

	// Token: 0x060044F6 RID: 17654 RVA: 0x002977E0 File Offset: 0x002967E0
	private void ᜀ(XmlWriter A_0, spr\u240D A_1)
	{
		int a_ = 1;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("夶䰸嘺笼刾㕀", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("夶䰸嘺笼刾㕀ੂ⅄", a_), A_1.ᜀ().ToString());
				string value = A_1.ᜁ();
				num = 2;
				continue;
			}
			case 1:
				goto IL_10C;
			case 2:
				if (A_1.ᜁ().Equals(RecordTableEnumerator.b("搶䴸娺匼嬾⁀ㅂ⅄", a_)))
				{
					num = 6;
					continue;
				}
				goto IL_12C;
			case 4:
				goto IL_12A;
			case 5:
				goto IL_43;
			case 6:
			{
				if (true)
				{
				}
				string value = RecordTableEnumerator.b("瀶尸唺堼䴾⁀⽂", a_);
				num = 1;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 0;
			}
		}
		IL_43:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_B8:
		throw new ArgumentNullException(RecordTableEnumerator.b("儶嘸䤺值帾㕀", a_));
		IL_10C:
		goto IL_12C;
		IL_12A:
		goto IL_B8;
		IL_12C:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_B8;
		default:
		{
			if (false)
			{
			}
			string value;
			A_0.WriteAttributeString(RecordTableEnumerator.b("儶嘸䤺值帾㕀B⩄⍆ⱈ", a_), value);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x060044F7 RID: 17655 RVA: 0x00297950 File Offset: 0x00296950
	private int[] ᜄ(XmlWriter A_0)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 8;
			int[] array2;
			for (;;)
			{
				XlsFill xlsFill;
				int num3;
				XlsFill[] array;
				int num4;
				int count;
				sprᢖ sprᢖ;
				Dictionary<XlsFill, int> dictionary;
				switch (num)
				{
				case 0:
					goto IL_37A;
				case 1:
					xlsFill = new XlsFill();
					xlsFill.Pattern = ExcelPatternType.None;
					xlsFill.PatternColorObject.SetKnownColor(ExcelColors.BlackCustom);
					xlsFill.OColor.SetKnownColor((ExcelColors)65);
					num = 22;
					continue;
				case 2:
				{
					int num2;
					if (num2 > num3)
					{
						num = 11;
						continue;
					}
					this.ᜄ(A_0, array[num2]);
					num2++;
					num = 25;
					continue;
				}
				case 3:
					if (num4 >= count)
					{
						num = 4;
						continue;
					}
					num = 7;
					continue;
				case 4:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("倵儷嘹倻䴽", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("唵圷伹刻䨽", a_), (num3 + 1).ToString());
					int num2 = 0;
					num = 23;
					continue;
				}
				case 5:
					goto IL_391;
				case 6:
					goto IL_AB;
				case 7:
					if (num3 == -1)
					{
						num = 1;
						continue;
					}
					num = 16;
					continue;
				case 9:
					goto IL_B0;
				case 10:
					Array.Resize<XlsFill>(ref array, num3 + 1);
					num = 5;
					continue;
				case 11:
					goto IL_27D;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_380;
					default:
						if (false)
						{
						}
						xlsFill = new XlsFill();
						xlsFill.Pattern = ExcelPatternType.Percent10;
						xlsFill.PatternColorObject.SetKnownColor(ExcelColors.BlackCustom);
						xlsFill.OColor.SetKnownColor((ExcelColors)65);
						num = 18;
						continue;
					}
					break;
				case 13:
					num = 21;
					continue;
				case 14:
					goto IL_11F;
				case 15:
					if (num3 != 0)
					{
						num = 13;
						continue;
					}
					goto IL_B0;
				case 16:
					if (num3 == 0)
					{
						num = 12;
						continue;
					}
					xlsFill = new XlsFill(sprᢖ.ᜁ(num4));
					num = 27;
					continue;
				case 17:
					goto IL_37A;
				case 18:
					goto IL_34B;
				case 19:
					array2[num4] = dictionary[xlsFill];
					num = 0;
					continue;
				case 20:
					if (dictionary.ContainsKey(xlsFill))
					{
						num = 19;
						continue;
					}
					num3 = dictionary.Count;
					dictionary.Add(xlsFill, num3);
					num = 26;
					continue;
				case 21:
					if (num3 == 1)
					{
						num = 9;
						continue;
					}
					goto IL_37A;
				case 22:
					goto IL_34B;
				case 23:
					goto IL_25C;
				case 24:
					goto IL_11F;
				case 25:
					goto IL_25C;
				case 26:
					if (num3 >= array.Length)
					{
						num = 10;
						continue;
					}
					goto IL_391;
				case 27:
					goto IL_34B;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				dictionary = new Dictionary<XlsFill, int>();
				sprᢖ = this.ᡇ.InnerExtFormats;
				count = sprᢖ.Count;
				array2 = new int[count];
				array = new XlsFill[count];
				num3 = -1;
				num4 = 0;
				num = 24;
				continue;
				IL_B0:
				num4--;
				num = 17;
				continue;
				IL_11F:
				num = 3;
				continue;
				IL_25C:
				num = 2;
				continue;
				IL_34B:
				if (true)
				{
				}
				num = 20;
				continue;
				IL_380:
				num = 14;
				continue;
				IL_37A:
				num4++;
				goto IL_380;
				IL_391:
				array[num3] = xlsFill;
				array2[num4] = num3;
				num = 15;
			}
			IL_AB:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_27D:
			A_0.WriteEndElement();
			return array2;
		}
		}
	}

	// Token: 0x060044F8 RID: 17656 RVA: 0x00297D40 File Offset: 0x00296D40
	private void ᜄ(XmlWriter A_0, XlsFill A_1)
	{
		int a_ = 8;
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜂ(A_0, A_1);
				num = 7;
				continue;
			case 2:
				goto IL_D5;
			case 3:
				if (A_1.Pattern == ExcelPatternType.Gradient)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4C;
				default:
					if (false)
					{
					}
					this.ᜃ(A_0, A_1);
					num = 4;
					continue;
				}
				break;
			case 4:
				goto IL_BA;
			case 5:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("堽⤿⹁⡃", a_));
				num = 3;
				continue;
			case 6:
				goto IL_4C;
			case 7:
				goto IL_5E;
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				num = 5;
			}
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_5E:
		IL_BA:
		goto IL_120;
		IL_D5:
		throw new ArgumentNullException(RecordTableEnumerator.b("昽ⰿㅁɃ⽅⑇♉", a_));
		IL_120:
		A_0.WriteEndElement();
	}

	// Token: 0x060044F9 RID: 17657 RVA: 0x00297E74 File Offset: 0x00296E74
	private void ᜃ(XmlWriter A_0, XlsFill A_1)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			OColor ocolor;
			switch (num)
			{
			case 0:
				goto IL_93;
			case 2:
				if (ocolor.ColorType == ColorType.Known)
				{
					num = 7;
					continue;
				}
				goto IL_153;
			case 3:
				if (ocolor.ᜂ(this.ᡇ) != (ExcelColors)65)
				{
					num = 6;
					continue;
				}
				goto IL_12C;
			case 4:
				if (ocolor.ColorType == ColorType.Known)
				{
					num = 12;
					continue;
				}
				goto IL_75;
			case 5:
				if (ocolor.ᜂ(this.ᡇ) != ExcelColors.BlackCustom)
				{
					num = 10;
					continue;
				}
				goto IL_278;
			case 6:
				goto IL_75;
			case 7:
				num = 5;
				continue;
			case 8:
				goto IL_E8;
			case 9:
				if (A_1.Pattern == ExcelPatternType.Solid)
				{
					num = 14;
					continue;
				}
				ocolor = A_1.PatternColorObject;
				num = 4;
				continue;
			case 10:
				goto IL_153;
			case 11:
				goto IL_1D6;
			case 12:
				num = 3;
				continue;
			case 13:
				goto IL_70;
			case 14:
				this.ᜀ(A_0, RecordTableEnumerator.b("弸尺縼倾ⵀⱂ㝄", a_), A_1.OColor);
				this.ᜀ(A_0, RecordTableEnumerator.b("嬸尺縼倾ⵀⱂ㝄", a_), A_1.PatternColorObject);
				num = 11;
				continue;
			case 15:
				if (A_1 == null)
				{
					num = 8;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䤸娺䤼䬾⑀ㅂ⭄ņ⁈❊⅌", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䤸娺䤼䬾⑀ㅂ⭄ፆえ㭊⡌", a_), this.ᜀ(A_1.Pattern));
				num = 9;
				continue;
			case 16:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_93;
				default:
					goto IL_18A;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 13;
				continue;
			}
			num = 15;
			continue;
			IL_75:
			this.ᜀ(A_0, RecordTableEnumerator.b("弸尺縼倾ⵀⱂ㝄", a_), ocolor);
			num = 0;
			continue;
			IL_12C:
			ocolor = A_1.OColor;
			num = 2;
			continue;
			IL_93:
			goto IL_12C;
			IL_153:
			this.ᜀ(A_0, RecordTableEnumerator.b("嬸尺縼倾ⵀⱂ㝄", a_), ocolor);
			num = 16;
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_E8:
		throw new ArgumentNullException(RecordTableEnumerator.b("愸场丼社⡀⽂⥄", a_));
		IL_18A:
		if (false)
		{
		}
		IL_1D6:
		IL_278:
		A_0.WriteEndElement();
	}

	// Token: 0x060044FA RID: 17658 RVA: 0x00298100 File Offset: 0x00297100
	private void ᜂ(XmlWriter A_0, XlsFill A_1)
	{
		int a_ = 3;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_EC;
			case 1:
				goto IL_C1;
			case 2:
			{
				GradientStyleType gradientStyle;
				if (gradientStyle != GradientStyleType.From_Center)
				{
					num = 8;
					continue;
				}
				goto IL_EC;
			}
			case 3:
			{
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("常䤺尼嬾⡀♂⭄㍆཈≊⅌⍎", a_));
				GradientStyleType gradientStyle = A_1.GradientStyle;
				num = 2;
				continue;
			}
			case 4:
				goto IL_4C;
			case 5:
				goto IL_11B;
			case 6:
			{
				GradientStyleType gradientStyle;
				if (gradientStyle == GradientStyleType.From_Corner)
				{
					num = 0;
					continue;
				}
				this.ᜁ(A_0, A_1);
				num = 7;
				continue;
			}
			case 7:
				goto IL_D6;
			case 8:
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 3;
			continue;
			IL_EC:
			this.ᜀ(A_0, A_1);
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_131;
			default:
				if (false)
				{
				}
				num = 5;
				break;
			}
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_C1:
		throw new ArgumentNullException(RecordTableEnumerator.b("弸刺儼匾", a_));
		IL_D6:
		IL_11B:
		IL_131:
		if (true)
		{
		}
		A_0.WriteEndElement();
	}

	// Token: 0x060044FB RID: 17659 RVA: 0x0029824C File Offset: 0x0029724C
	private void ᜁ(XmlWriter A_0, XlsFill A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			double a_2;
			for (;;)
			{
				GradientStyleType gradientStyle = A_1.GradientStyle;
				GradientVariantsType gradientVariant = A_1.GradientVariant;
				a_2 = 0.0;
				int num = 28;
				for (;;)
				{
					double num2;
					double num3;
					double num4;
					double num5;
					switch (num)
					{
					case 0:
						num2 = (double)315;
						goto IL_37A;
					case 1:
						goto IL_260;
					case 2:
						goto IL_388;
					case 3:
						num3 = (double)45;
						goto IL_36A;
					case 4:
						goto IL_378;
					case 5:
						num = 16;
						continue;
					case 6:
						if (gradientVariant != GradientVariantsType.ShadingVariants1)
						{
							num = 22;
							continue;
						}
						num = 7;
						continue;
					case 7:
						num2 = (double)135;
						goto IL_37A;
					case 8:
						num4 = (double)0;
						goto IL_328;
					case 9:
					{
						GradientStyleType gradientStyleType = gradientStyle;
						num = 17;
						continue;
					}
					case 10:
						goto IL_151;
					case 11:
					{
						GradientStyleType gradientStyleType2;
						switch (gradientStyleType2)
						{
						case GradientStyleType.Horizontal:
							num = 13;
							continue;
						case GradientStyleType.Vertical:
							num = 19;
							continue;
						case GradientStyleType.Diagonl_Up:
							num = 21;
							continue;
						case GradientStyleType.Diagonl_Down:
							num = 6;
							continue;
						default:
							num = 18;
							continue;
						}
						break;
					}
					case 12:
						num = 26;
						continue;
					case 13:
						if (gradientVariant != GradientVariantsType.ShadingVariants1)
						{
							num = 5;
							continue;
						}
						num = 24;
						continue;
					case 14:
						num = 23;
						continue;
					case 15:
						num = 30;
						continue;
					case 16:
						num5 = (double)270;
						goto IL_38A;
					case 17:
					{
						GradientStyleType gradientStyleType;
						switch (gradientStyleType)
						{
						case GradientStyleType.Horizontal:
							a_2 = 90.0;
							num = 29;
							continue;
						case GradientStyleType.Vertical:
							goto IL_2C7;
						case GradientStyleType.Diagonl_Up:
							a_2 = 45.0;
							num = 20;
							continue;
						case GradientStyleType.Diagonl_Down:
							a_2 = 135.0;
							num = 1;
							continue;
						default:
							num = 14;
							continue;
						}
						break;
					}
					case 18:
						num = 10;
						continue;
					case 19:
						if (gradientVariant != GradientVariantsType.ShadingVariants1)
						{
							num = 15;
							continue;
						}
						goto IL_169;
					case 20:
						goto IL_F9;
					case 21:
						if (gradientVariant != GradientVariantsType.ShadingVariants1)
						{
							num = 12;
							continue;
						}
						num = 3;
						continue;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_169;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 23:
						goto IL_26E;
					case 24:
						num5 = (double)90;
						goto IL_38A;
					case 25:
						goto IL_336;
					case 26:
						num3 = (double)225;
						goto IL_36A;
					case 27:
						goto IL_398;
					case 28:
					{
						if (gradientVariant == GradientVariantsType.ShadingVariants3)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						GradientStyleType gradientStyleType2 = gradientStyle;
						num = 11;
						continue;
					}
					case 29:
						goto IL_293;
					case 30:
						num4 = (double)180;
						goto IL_328;
					}
					break;
					IL_169:
					num = 8;
					continue;
					IL_328:
					a_2 = num4;
					num = 25;
					continue;
					IL_36A:
					a_2 = num3;
					num = 4;
					continue;
					IL_37A:
					a_2 = num2;
					num = 2;
					continue;
					IL_38A:
					a_2 = num5;
					num = 27;
				}
			}
			IL_F9:
			goto IL_2C7;
			IL_151:
			throw new ArgumentException(RecordTableEnumerator.b("ቆ❈⁊⍌⁎♐㵒畔ざ⭘㩚㥜㙞Ѡൢᅤ䝦ᩨὪᑬͮᑰ", a_));
			IL_260:
			goto IL_2C7;
			IL_26E:
			throw new ArgumentException(RecordTableEnumerator.b("ቆ❈⁊⍌⁎♐㵒畔ざ⭘㩚㥜㙞Ѡൢᅤ䝦ᩨὪᑬͮᑰ", a_));
			IL_293:
			IL_2C7:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⍆ⱈⱊ㽌⩎㑐", a_), a_2, 0.0);
			this.ᜀ(A_0, 0.0, A_1.OColor);
			this.ᜀ(A_0, 0.5, A_1.PatternColorObject);
			this.ᜀ(A_0, 1.0, A_1.OColor);
			return;
			IL_336:
			IL_378:
			IL_388:
			IL_398:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⍆ⱈⱊ㽌⩎㑐", a_), a_2, 0.0);
			this.ᜀ(A_0, 0.0, A_1.OColor);
			this.ᜀ(A_0, 1.0, A_1.PatternColorObject);
			return;
		}
		}
	}

	// Token: 0x060044FC RID: 17660 RVA: 0x00298678 File Offset: 0x00297678
	private void ᜀ(XmlWriter A_0, XlsFill A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			double a_2;
			double a_3;
			double a_4;
			double a_5;
			for (;;)
			{
				GradientStyleType gradientStyle = A_1.GradientStyle;
				GradientVariantsType gradientVariant = A_1.GradientVariant;
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㝂㱄㝆ⱈ", a_), RecordTableEnumerator.b("㍂⑄㍆ⅈ", a_), string.Empty);
				a_2 = double.MinValue;
				a_3 = double.MinValue;
				a_4 = double.MinValue;
				a_5 = double.MinValue;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1C4;
					case 1:
						goto IL_EE;
					case 2:
					{
						GradientVariantsType gradientVariantsType;
						switch (gradientVariantsType)
						{
						case GradientVariantsType.ShadingVariants1:
							goto IL_1C6;
						case GradientVariantsType.ShadingVariants2:
							a_5 = (a_4 = 1.0);
							num = 3;
							continue;
						case GradientVariantsType.ShadingVariants3:
							a_3 = (a_2 = 1.0);
							num = 1;
							continue;
						case GradientVariantsType.ShadingVariants4:
							a_3 = (a_2 = (a_4 = (a_5 = 1.0)));
							goto IL_18E;
						default:
							num = 4;
							continue;
						}
						break;
					}
					case 3:
						goto IL_10D;
					case 4:
						num = 0;
						continue;
					case 5:
					{
						if (gradientStyle == GradientStyleType.From_Center)
						{
							num = 7;
							continue;
						}
						GradientVariantsType gradientVariantsType = gradientVariant;
						num = 2;
						continue;
					}
					case 6:
						goto IL_17A;
					case 7:
						if (true)
						{
						}
						a_3 = (a_2 = (a_4 = (a_5 = 0.5)));
						num = 6;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_18E;
						default:
							goto IL_1B0;
						}
						break;
					}
					break;
					IL_18E:
					num = 8;
				}
			}
			IL_EE:
			IL_10D:
			IL_17A:
			goto IL_1C6;
			IL_1B0:
			if (false)
			{
			}
			goto IL_1C6;
			IL_1C4:
			throw new ArgumentException(RecordTableEnumerator.b("ᙂ⭄ⱆ❈⑊㩌ⅎ煐㑒❔㙖㵘㉚㡜ㅞᕠ䍢፤٦᭨ɪ౬Ůհ", a_));
			IL_1C6:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㝂⩄㝆", a_), a_2, double.MinValue);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⅂⩄㍆㵈⑊⁌", a_), a_3, double.MinValue);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⽂⁄ⅆ㵈", a_), a_4, double.MinValue);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("ㅂⱄ⁆ⅈ㽊", a_), a_5, double.MinValue);
			this.ᜀ(A_0, 0.0, A_1.OColor);
			this.ᜀ(A_0, 1.0, A_1.PatternColorObject);
			return;
		}
		}
	}

	// Token: 0x060044FD RID: 17661 RVA: 0x002988F4 File Offset: 0x002978F4
	private void ᜀ(XmlWriter A_0, double A_1, OColor A_2)
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
		A_0.WriteStartElement(RecordTableEnumerator.b("㭇㹉⍋㹍", a_));
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㡇╉㽋❍⑏㭑㭓㡕", a_), A_1, double.MinValue);
		this.ᜀ(A_0, RecordTableEnumerator.b("⭇╉⁋⅍≏", a_), A_2);
		A_0.WriteEndElement();
	}

	// Token: 0x060044FE RID: 17662 RVA: 0x00298988 File Offset: 0x00297988
	private string ᜀ(ExcelPatternType A_0)
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
		return ((XLSXPattern)A_0).ToString();
	}

	// Token: 0x060044FF RID: 17663 RVA: 0x002989D0 File Offset: 0x002979D0
	private int[] ᜃ(XmlWriter A_0)
	{
		int a_ = 19;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_222:
			goto IL_B3;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 10;
				break;
			}
			break;
		}
		int num2;
		int[] array2;
		for (;;)
		{
			IL_3E:
			int count;
			int num3;
			XlsBordersCollection[] array;
			Dictionary<XlsBordersCollection, int> dictionary;
			sprᢖ sprᢖ;
			switch (num)
			{
			case 0:
			{
				XlsWorkbook xlsWorkbook = new XlsWorkbook(this.ᡇ.AppImplementation, this.ᡇ, ExcelVersion.Version2007);
				XlsBordersCollection xlsBordersCollection = new XlsBordersCollection(xlsWorkbook.AppImplementation, xlsWorkbook, true);
				AddtionalFormatWrapper a_2 = new AddtionalFormatWrapper(xlsWorkbook, 0);
				xlsBordersCollection.InnerList.Clear();
				xlsBordersCollection.InnerList.Add(new XlsBorder(xlsWorkbook.AppImplementation, xlsWorkbook, a_2, BordersLineType.DiagonalDown));
				xlsBordersCollection.InnerList.Add(new XlsBorder(xlsWorkbook.AppImplementation, xlsWorkbook, a_2, BordersLineType.DiagonalUp));
				xlsBordersCollection.InnerList.Add(new XlsBorder(xlsWorkbook.AppImplementation, xlsWorkbook, a_2, BordersLineType.EdgeBottom));
				xlsBordersCollection.InnerList.Add(new XlsBorder(xlsWorkbook.AppImplementation, xlsWorkbook, a_2, BordersLineType.EdgeLeft));
				xlsBordersCollection.InnerList.Add(new XlsBorder(xlsWorkbook.AppImplementation, xlsWorkbook, a_2, BordersLineType.EdgeRight));
				xlsBordersCollection.InnerList.Add(new XlsBorder(xlsWorkbook.AppImplementation, xlsWorkbook, a_2, BordersLineType.EdgeTop));
				XlsBordersCollection xlsBordersCollection2 = xlsBordersCollection;
				num = 12;
				continue;
			}
			case 1:
				goto IL_1E4;
			case 2:
			{
				if (num2 >= count)
				{
					if (true)
					{
					}
					num = 15;
					continue;
				}
				XlsBordersCollection xlsBordersCollection2 = null;
				num = 16;
				continue;
			}
			case 3:
				goto IL_399;
			case 4:
				if (num3 == 0)
				{
					num = 18;
					continue;
				}
				goto IL_B3;
			case 5:
			{
				int num4;
				if (num4 > num3)
				{
					num = 19;
					continue;
				}
				this.ᜀ(A_0, array[num4]);
				num4++;
				num = 9;
				continue;
			}
			case 6:
				goto IL_222;
			case 7:
			{
				XlsBordersCollection xlsBordersCollection2;
				array2[num2] = dictionary[xlsBordersCollection2];
				num = 6;
				continue;
			}
			case 8:
				goto IL_AE;
			case 9:
				goto IL_1E4;
			case 11:
				goto IL_1B7;
			case 12:
				goto IL_399;
			case 13:
			{
				XlsBordersCollection xlsBordersCollection2;
				if (dictionary.ContainsKey(xlsBordersCollection2))
				{
					num = 7;
					continue;
				}
				num3 = dictionary.Count;
				dictionary.Add(xlsBordersCollection2, num3);
				array[num3] = xlsBordersCollection2;
				array2[num2] = num3;
				num = 4;
				continue;
			}
			case 14:
				goto IL_1B7;
			case 15:
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("⭈⑊㽌⭎㑐⅒♔", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⩈⑊㡌ⅎ═", a_), (num3 + 1).ToString());
				int num4 = 0;
				num = 1;
				continue;
			}
			case 16:
			{
				if (num3 == -1)
				{
					num = 0;
					continue;
				}
				XlsBordersCollection xlsBordersCollection2 = (XlsBordersCollection)sprᢖ.ᜁ(num2).ᜪ();
				num = 3;
				continue;
			}
			case 17:
				goto IL_35E;
			case 18:
				num2--;
				num = 17;
				continue;
			case 19:
				goto IL_205;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			dictionary = new Dictionary<XlsBordersCollection, int>();
			sprᢖ = this.ᡇ.InnerExtFormats;
			count = sprᢖ.Count;
			array2 = new int[count];
			array = new XlsBordersCollection[count];
			num3 = -1;
			num2 = 0;
			num = 11;
			continue;
			IL_1B7:
			num = 2;
			continue;
			IL_1E4:
			num = 5;
			continue;
			IL_399:
			num = 13;
		}
		IL_AE:
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
		IL_205:
		A_0.WriteEndElement();
		return array2;
		IL_35E:
		IL_B3:
		num2++;
		num = 14;
		goto IL_3E;
	}

	// Token: 0x06004500 RID: 17664 RVA: 0x00298DA8 File Offset: 0x00297DA8
	private void ᜀ(XmlWriter A_0, string A_1, ExcelColors A_2)
	{
		int a_ = 17;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_152:
			num = 4;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_F7;
			case 2:
				goto IL_68;
			case 3:
				goto IL_13A;
			case 4:
				goto IL_15D;
			case 5:
				num = 9;
				continue;
			case 6:
				A_0.WriteAttributeString(RecordTableEnumerator.b("♆㱈㽊≌", a_), RecordTableEnumerator.b("癆", a_));
				num = 3;
				continue;
			case 7:
				if (A_1 != null)
				{
					num = 5;
					continue;
				}
				goto IL_8E;
			case 8:
			{
				if (A_2 > (ExcelColors)65)
				{
					num = 6;
					continue;
				}
				string localName = RecordTableEnumerator.b("⹆❈⽊⡌㝎㑐㝒", a_);
				int num2 = (int)A_2;
				A_0.WriteAttributeString(localName, num2.ToString());
				num = 0;
				continue;
			}
			case 9:
				if (A_1.Length == 0)
				{
					goto IL_152;
				}
				A_0.WriteStartElement(A_1);
				num = 8;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 7;
			}
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_8E:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍆⡈ⱊ͌⹎㱐㙒", a_));
		IL_F7:
		IL_13A:
		goto IL_162;
		IL_15D:
		goto IL_8E;
		IL_162:
		A_0.WriteEndElement();
	}

	// Token: 0x06004501 RID: 17665 RVA: 0x00298F20 File Offset: 0x00297F20
	private void ᜂ(XmlWriter A_0, string A_1, OColor A_2)
	{
		int a_ = 17;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 0;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6A;
			case 2:
				if (A_1.Length == 0)
				{
					num = 5;
					continue;
				}
				goto IL_C3;
			case 3:
				num = 2;
				continue;
			case 4:
				if (A_1 != null)
				{
					num = 3;
					continue;
				}
				goto IL_AF;
			case 5:
				goto IL_84;
			}
			if (A_0 == null)
			{
				goto IL_62;
			}
			num = 4;
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_84:
		IL_AF:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍆⡈ⱊ͌⹎㱐㙒", a_));
		IL_C3:
		int value = A_2.Value;
		A_0.WriteStartElement(A_1);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㕆⹈⥊", a_), value.ToString(RecordTableEnumerator.b("὆", a_)));
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㍆⁈╊㥌", a_), A_2.Tint, 0.0);
		A_0.WriteEndElement();
	}

	// Token: 0x06004502 RID: 17666 RVA: 0x00299050 File Offset: 0x00298050
	private void ᜁ(XmlWriter A_0, string A_1, OColor A_2)
	{
		int a_ = 5;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 5;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 2:
				goto IL_84;
			case 3:
				if (A_1 != null)
				{
					num = 0;
					continue;
				}
				goto IL_AF;
			case 4:
				if (A_1.Length == 0)
				{
					num = 2;
					continue;
				}
				goto IL_C3;
			case 5:
				goto IL_6A;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				goto IL_62;
			}
			num = 3;
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_84:
		IL_AF:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("伺尼堾ཀ≂⡄≆", a_));
		IL_C3:
		A_0.WriteStartElement(A_1);
		A_0.WriteAttributeString(RecordTableEnumerator.b("伺唼娾ⱀ♂", a_), A_2.Value.ToString());
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("伺吼儾㕀", a_), A_2.Tint, 0.0);
		A_0.WriteEndElement();
	}

	// Token: 0x06004503 RID: 17667 RVA: 0x00299174 File Offset: 0x00298174
	private void ᜀ(XmlWriter A_0, string A_1, OColor A_2)
	{
		int a_ = 16;
		for (;;)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_50;
				case 3:
					if (A_1 != null)
					{
						num = 0;
						continue;
					}
					goto IL_FB;
				case 4:
					goto IL_EF;
				case 5:
				{
					if (A_1.Length == 0)
					{
						num = 6;
						continue;
					}
					ColorType colorType = A_2.ColorType;
					num = 7;
					continue;
				}
				case 6:
					goto IL_130;
				case 7:
				{
					ColorType colorType;
					switch (colorType)
					{
					case ColorType.Known:
						goto IL_6E;
					case ColorType.RGB:
						goto IL_C6;
					case ColorType.Theme:
						goto IL_F1;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 8:
					if (true)
					{
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_50:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_66;
			}
		}
		IL_66:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_6E:
		this.ᜀ(A_0, A_1, (ExcelColors)A_2.Value);
		return;
		IL_C6:
		this.ᜂ(A_0, A_1, A_2);
		return;
		IL_EF:
		throw new NotImplementedException();
		IL_F1:
		this.ᜁ(A_0, A_1, A_2);
		return;
		IL_FB:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉅⥇ⵉɋ⽍㵏㝑", a_));
		IL_130:
		goto IL_FB;
	}

	// Token: 0x06004504 RID: 17668 RVA: 0x002992C4 File Offset: 0x002982C4
	private void ᜀ(XmlWriter A_0, XlsBordersCollection A_1)
	{
		int a_ = 6;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5A:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			case 2:
				goto IL_62;
			case 3:
				if (true)
				{
				}
				break;
			}
			if (A_0 == null)
			{
				goto IL_5A;
			}
			num = 1;
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("帻儽㈿♁⅃㑅㭇", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("帻儽㈿♁⅃㑅", a_));
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("堻圽ℿ╁⭃⡅⥇♉᥋㹍", a_), A_1[BordersLineType.DiagonalUp].ShowDiagonalLine, false);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("堻圽ℿ╁⭃⡅⥇♉ࡋ⅍❏㱑", a_), A_1[BordersLineType.DiagonalDown].ShowDiagonalLine, false);
		this.ᜀ(A_0, (XlsBorder)A_1[BordersLineType.EdgeLeft]);
		this.ᜀ(A_0, (XlsBorder)A_1[BordersLineType.EdgeRight]);
		this.ᜀ(A_0, (XlsBorder)A_1[BordersLineType.EdgeTop]);
		this.ᜀ(A_0, (XlsBorder)A_1[BordersLineType.EdgeBottom]);
		this.ᜀ(A_0, (XlsBorder)A_1[BordersLineType.DiagonalUp]);
		A_0.WriteEndElement();
	}

	// Token: 0x06004505 RID: 17669 RVA: 0x00299430 File Offset: 0x00298430
	private void ᜀ(XmlWriter A_0, XlsBorder A_1)
	{
		int a_ = 14;
		for (;;)
		{
			IL_09:
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string text;
					A_0.WriteStartElement(text);
					num = 4;
					continue;
				}
				case 1:
					goto IL_C4;
				case 2:
				{
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					string text = spr\u1B7A.ᜀ(A_1.BorderIndex);
					num = 9;
					continue;
				}
				case 3:
					goto IL_56;
				case 4:
					if (A_1.LineStyle != LineStyleType.None)
					{
						num = 7;
						continue;
					}
					goto IL_C9;
				case 5:
					return;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						A_0.WriteAttributeString(RecordTableEnumerator.b("㝃㉅ㅇ♉⥋", a_), this.ᜀ(A_1));
						this.ᜀ(A_0, RecordTableEnumerator.b("❃⥅⑇╉㹋", a_), A_1.OColor);
						num = 8;
						continue;
					}
					break;
				case 8:
					goto IL_C9;
				case 9:
				{
					string text;
					if (text != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				num = 2;
				continue;
				IL_C9:
				A_0.WriteEndElement();
				num = 5;
			}
		}
		IL_56:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_C4:
		throw new ArgumentNullException(RecordTableEnumerator.b("♃⥅㩇⹉⥋㱍", a_));
	}

	// Token: 0x06004506 RID: 17670 RVA: 0x002995A8 File Offset: 0x002985A8
	private static string ᜀ(BordersLineType A_0)
	{
		string result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_46:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					result = ((XLSXBorderIndex)A_0).ToString();
					num = 0;
					continue;
				case 2:
					if (A_0 != BordersLineType.DiagonalDown)
					{
						num = 1;
						continue;
					}
					return result;
				}
				goto IL_30;
			}
			return result;
		}
		default:
			if (false)
			{
			}
			break;
		}
		IL_30:
		if (true)
		{
		}
		result = null;
		goto IL_46;
	}

	// Token: 0x06004507 RID: 17671 RVA: 0x00299628 File Offset: 0x00298628
	private string ᜀ(XlsBorder A_0)
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
		XLSXBorderLineStyle lineStyle = (XLSXBorderLineStyle)A_0.LineStyle;
		string a_ = lineStyle.ToString();
		return spr\u1B7A.ᜄ(a_);
	}

	// Token: 0x06004508 RID: 17672 RVA: 0x0029967C File Offset: 0x0029867C
	private Dictionary<int, int> ᜀ(XmlWriter A_0, int[] A_1, int[] A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			Dictionary<int, int> dictionary;
			for (;;)
			{
				int num = 1;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
						goto IL_193;
					case 2:
						goto IL_193;
					case 3:
					{
						if (A_2 == null)
						{
							num = 11;
							continue;
						}
						A_0.WriteStartElement(RecordTableEnumerator.b("≀♂⥄⭆ᩈ㽊㑌⍎㑐୒㍔⑖", a_));
						sprᢖ sprᢖ = this.ᡇ.InnerExtFormats;
						int count = this.ᡇ.Styles.Count;
						A_0.WriteAttributeString(RecordTableEnumerator.b("≀ⱂい⥆㵈", a_), count.ToString());
						dictionary = new Dictionary<int, int>();
						int num2 = 0;
						num3 = 0;
						int count2 = sprᢖ.Count;
						num = 2;
						continue;
					}
					case 4:
						goto IL_6C;
					case 5:
					{
						int count2;
						if (num3 >= count2)
						{
							num = 10;
							continue;
						}
						sprᢖ sprᢖ;
						spr\u192F spr_u192F = sprᢖ.ᜁ(num3);
						num = 7;
						continue;
					}
					case 6:
					{
						spr\u192F spr_u192F;
						this.ᜀ(A_0, A_1, A_2, spr_u192F, null, true);
						int num2;
						dictionary.Add(spr_u192F.ᜠ(), num2);
						num2++;
						num = 9;
						continue;
					}
					case 7:
					{
						spr\u192F spr_u192F;
						if (!spr_u192F.ᝇ())
						{
							num = 6;
							continue;
						}
						goto IL_13B;
					}
					case 8:
						if (A_1 == null)
						{
							num = 12;
							continue;
						}
						num = 3;
						continue;
					case 9:
						goto IL_13B;
					case 10:
						goto IL_1B1;
					case 11:
						goto IL_1D1;
					case 12:
						goto IL_136;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 8;
					continue;
					IL_13B:
					num3++;
					num = 0;
					continue;
					IL_193:
					num = 5;
				}
				IL_1B1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_219;
				}
			}
			IL_6C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
			IL_136:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄ņ⁈❊⅌َ㽐㝒ご⽖㱘⡚", a_));
			IL_1D1:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄Ն♈㥊⥌⩎⍐ᩒ㭔㍖㱘⍚㡜ⱞ", a_));
			IL_219:
			if (false)
			{
			}
			A_0.WriteEndElement();
			return dictionary;
		}
		}
	}

	// Token: 0x06004509 RID: 17673 RVA: 0x002998B0 File Offset: 0x002988B0
	private Dictionary<int, int> ᜀ(XmlWriter A_0, int[] A_1, int[] A_2, Dictionary<int, int> A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			Dictionary<int, int> dictionary;
			for (;;)
			{
				for (;;)
				{
					sprᢖ sprᢖ = this.ᡇ.InnerExtFormats;
					int count = sprᢖ.Count;
					int count2 = this.ᡇ.InnerStyles.Count;
					A_0.WriteStartElement(RecordTableEnumerator.b("⭇⽉⁋≍ࡏ㑑❓", a_));
					int num = 0;
					dictionary = new Dictionary<int, int>();
					int num2 = 0;
					int num3 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							goto IL_12A;
						case 1:
							goto IL_12A;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								spr\u192F spr_u192F;
								dictionary.Add(spr_u192F.ᜠ(), num);
								this.ᜀ(A_0, A_1, A_2, spr_u192F, A_3, false);
								num++;
								num3 = 4;
								continue;
							}
							}
							break;
						case 3:
						{
							spr\u192F spr_u192F;
							if (spr_u192F.ᝇ())
							{
								num3 = 2;
								continue;
							}
							goto IL_9F;
						}
						case 4:
							goto IL_9F;
						case 5:
						{
							if (num2 >= count)
							{
								num3 = 6;
								continue;
							}
							spr\u192F spr_u192F = sprᢖ.ᜁ(num2);
							num3 = 3;
							continue;
						}
						case 6:
							goto IL_147;
						}
						break;
						IL_9F:
						num2++;
						num3 = 1;
						continue;
						IL_12A:
						num3 = 5;
					}
				}
			}
			IL_147:
			A_0.WriteEndElement();
			return dictionary;
		}
		}
	}

	// Token: 0x0600450A RID: 17674 RVA: 0x00299A10 File Offset: 0x00298A10
	private void ᜀ(XmlWriter A_0, int[] A_1, int[] A_2, spr\u192F A_3, Dictionary<int, int> A_4, bool A_5)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num2 = A_3.ᜯ();
					goto IL_1B2;
				case 1:
					num = 13;
					continue;
				case 2:
					if (A_3 == null)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B2;
					default:
					{
						if (false)
						{
						}
						int num3 = A_3.ᜠ();
						A_0.WriteStartElement(RecordTableEnumerator.b("䌺嬼", a_));
						A_0.WriteAttributeString(RecordTableEnumerator.b("唺䠼刾݀⹂ㅄๆⵈ", a_), A_3.ᝊ().ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("崺刼儾㕀ੂ⅄", a_), A_3.\u173B().ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("崺吼匾ⵀੂ⅄", a_), A_1[num3].ToString());
						A_0.WriteAttributeString(RecordTableEnumerator.b("夺刼䴾╀♂㝄ๆⵈ", a_), A_2[num3].ToString());
						num = 6;
						continue;
					}
					}
					break;
				case 3:
					goto IL_200;
				case 4:
					goto IL_7A;
				case 5:
					goto IL_28B;
				case 6:
					if (A_3.ᝇ())
					{
						num = 1;
						continue;
					}
					goto IL_28D;
				case 7:
					goto IL_182;
				case 9:
					goto IL_227;
				case 10:
					if (A_1 == null)
					{
						num = 12;
						continue;
					}
					num = 11;
					continue;
				case 11:
					if (A_2 == null)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					num = 2;
					continue;
				case 12:
					goto IL_1A5;
				case 13:
					if (!A_4.TryGetValue(A_3.ᜯ(), out num2))
					{
						num = 0;
						continue;
					}
					goto IL_200;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 10;
				continue;
				IL_1B2:
				num = 3;
				continue;
				IL_200:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䌺嬼瘾╀", a_), num2.ToString());
				num = 9;
			}
			IL_7A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_182:
			throw new ArgumentNullException(RecordTableEnumerator.b("崺刼䴾ⱀ≂ㅄ", a_));
			IL_1A5:
			throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾݀⩂⥄⭆H╊⥌⩎⥐㙒♔", a_));
			IL_227:
			goto IL_28D;
			IL_28B:
			throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾̀ⱂ㝄⍆ⱈ㥊ьⅎ㕐㙒ⵔ㉖⩘", a_));
			IL_28D:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娺䴼伾ⵀ㩂ф⭆⁈ⱊ⍌≎㑐㵒⅔", a_), A_3.ᜦ(), A_5);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娺䴼伾ⵀ㩂݄⡆㭈⽊⡌㵎", a_), A_3.\u1719(), A_5);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娺䴼伾ⵀ㩂̈́⡆❈㽊", a_), A_3.ᝀ(), A_5);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娺䴼伾ⵀ㩂ୄ㉆⑈⥊⡌㵎ᝐ㱒❔㩖㡘⽚", a_), A_3.\u173D(), A_5);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娺䴼伾ⵀ㩂̈́⹆╈❊", a_), A_3.\u1753(), A_5);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("娺䴼伾ⵀ㩂ᕄ㕆♈㽊⡌ⱎ═㩒㩔㥖", a_), A_3.\u1717(), A_5);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䨺䠼倾㕀♂ᕄ㕆ⱈⵊ⑌㝎", a_), A_3.\u1713(), false);
			this.ᜁ(A_0, A_3);
			this.ᜀ(A_0, A_3);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600450B RID: 17675 RVA: 0x00299D8C File Offset: 0x00298D8C
	private void ᜁ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 10;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5D;
			case 1:
			{
				string value = ((HAlign2007)A_1.ᜋ()).ToString();
				A_0.WriteAttributeString(RecordTableEnumerator.b("⠿ⵁ㙃⽅㉇╉≋㩍ㅏ㹑", a_), value);
				num = 0;
				continue;
			}
			case 2:
				goto IL_DD;
			case 3:
				A_0.WriteStartElement(RecordTableEnumerator.b("ℿ⹁ⵃⅅ♇❉⥋⁍⑏", a_));
				num = 9;
				continue;
			case 4:
				goto IL_15F;
			case 6:
				goto IL_58;
			case 7:
				if (A_1.\u171D() != VerticalAlignType.Bottom)
				{
					num = 8;
					continue;
				}
				goto IL_15F;
			case 8:
			{
				string value2 = ((VAlign2007)A_1.\u171D()).ToString();
				A_0.WriteAttributeString(RecordTableEnumerator.b("㘿❁㙃㉅ⅇ⥉ⵋ≍", a_), value2);
				num = 4;
				continue;
			}
			case 9:
				if (A_1.ᜋ() != HorizontalAlignType.General)
				{
					num = 1;
					continue;
				}
				goto IL_5D;
			case 10:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 11;
				continue;
			case 11:
				if (!this.ᜀ(A_1))
				{
					num = 3;
					continue;
				}
				return;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_228;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 10;
			continue;
			IL_5D:
			num = 7;
			continue;
			IL_15F:
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㐿❁㱃㉅ᩇ╉㡋⽍⑏㭑㭓㡕", a_), A_1.\u171B(), 0);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㜿ぁ╃㙅᱇⽉㑋㩍", a_), A_1.\u1733(), false);
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("⤿ⱁ⁃⍅♇㹉", a_), A_1.\u171A(), 0);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⨿㝁㝃㉅ⅇⱉ㕋ɍㅏ⅑⁓ᩕㅗ㑙㥛", a_), A_1.ᜱ(), false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㌿⩁㙃⽅♇ⅉᡋ⅍ᙏ㭑⁓", a_), A_1.ᝏ(), false);
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㈿❁╃≅ⅇ⑉⭋ō≏㙑ㅓ⑕", a_), (int)A_1.\u171C(), 0);
			A_0.WriteEndElement();
			num = 12;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_DD:
		throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
		IL_228:
		if (false)
		{
		}
	}

	// Token: 0x0600450C RID: 17676 RVA: 0x0029A004 File Offset: 0x00299004
	private bool ᜀ(spr\u192F A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 8;
					continue;
				case 2:
					num = 12;
					continue;
				case 3:
					if (A_0.\u171C() == ReadingOrderType.Context)
					{
						num = 11;
						continue;
					}
					return false;
				case 4:
					goto IL_13C;
				case 5:
					num = 3;
					continue;
				case 6:
					num = 10;
					continue;
				case 7:
					if (!A_0.ᝏ())
					{
						num = 6;
						continue;
					}
					return false;
				case 8:
					if (A_0.\u171A() == 0)
					{
						num = 2;
						continue;
					}
					return false;
				case 9:
					if (!A_0.\u1733())
					{
						num = 4;
						continue;
					}
					return false;
				case 10:
					if (A_0.\u171B() == 0)
					{
						num = 13;
						continue;
					}
					return false;
				case 11:
					num = 7;
					continue;
				case 12:
					if (A_0.ᜱ())
					{
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 13:
					num = 9;
					continue;
				}
				if (true)
				{
				}
				if (A_0.ᜋ() != HorizontalAlignType.General)
				{
					return false;
				}
				num = 0;
			}
		}
		IL_13C:
		return A_0.\u171D() == VerticalAlignType.Bottom;
	}

	// Token: 0x0600450D RID: 17677 RVA: 0x0029A174 File Offset: 0x00299174
	private void ᜀ(XmlWriter A_0, spr\u192F A_1)
	{
		int a_ = 14;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D2;
			case 1:
				if (true)
				{
				}
				num = 7;
				continue;
			case 3:
				num = 4;
				continue;
			case 4:
				if (!A_1.ᝎ())
				{
					num = 5;
					continue;
				}
				return;
			case 5:
				goto IL_D7;
			case 6:
				if (A_1.\u1717())
				{
					num = 1;
					continue;
				}
				return;
			case 7:
				if (!A_1.\u1755())
				{
					num = 3;
					continue;
				}
				goto IL_D7;
			case 8:
				return;
			case 9:
				goto IL_50;
			case 10:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 10;
			continue;
			IL_D7:
			A_0.WriteStartElement(RecordTableEnumerator.b("㑃㑅❇㹉⥋ⵍ⑏㭑㭓㡕", a_));
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("ⱃ⽅ⱇ⹉⥋⁍", a_), A_1.\u1755(), false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⡃⥅⭇ⅉ⥋⩍", a_), A_1.ᝎ(), true);
			A_0.WriteEndElement();
			num = 8;
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_D2:
			throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		}
	}

	// Token: 0x0600450E RID: 17678 RVA: 0x0029A30C File Offset: 0x0029930C
	private void ᜀ(XmlWriter A_0, Dictionary<int, int> A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CA;
				case 1:
					goto IL_7B;
				case 2:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					XlsStylesCollection xlsStylesCollection;
					XlsStyle a_2 = (XlsStyle)xlsStylesCollection[num2];
					this.ᜀ(A_0, a_2, A_1);
					num2++;
					num = 0;
					continue;
				}
				case 3:
					goto IL_103;
				case 5:
					goto IL_E6;
				case 6:
				{
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					XlsStylesCollection xlsStylesCollection = this.ᡇ.InnerStyles;
					int count = xlsStylesCollection.Count;
					A_0.WriteStartElement(RecordTableEnumerator.b("吶尸场儼氾㕀㩂⥄≆㩈", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("吶嘸为匼䬾", a_), count.ToString());
					int num2 = 0;
					num = 7;
					continue;
				}
				case 7:
					goto IL_CA;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E8;
				}
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				IL_E8:
				num = 6;
				continue;
				IL_CA:
				num = 2;
			}
			IL_7B:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_E6:
			A_0.WriteEndElement();
			return;
			IL_103:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("弶堸䠺唼焾⑀㑂ᕄ♆㭈⹊⍌㭎ᡐ㵒ㅔ㉖⅘㹚⹜", a_));
		}
		}
	}

	// Token: 0x0600450F RID: 17679 RVA: 0x0029A484 File Offset: 0x00299484
	private void ᜀ(XmlWriter A_0, XlsStyle A_1, Dictionary<int, int> A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					sprᬐ sprᬐ;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⽅ч⽉㩋⭍㱏", a_), sprᬐ.ᜀ().ToString());
					num = 8;
					continue;
				}
				case 1:
					goto IL_116;
				case 2:
				{
					sprᬐ sprᬐ;
					if (sprᬐ.ᜀ() != 255)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_64;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_20C;
				}
				case 3:
				{
					if (A_2 == null)
					{
						num = 7;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("╅ⵇ♉⁋ᵍ⑏⭑㡓㍕", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("⡅⥇❉⥋", a_), A_1.Name);
					int num2 = A_2[A_1.ExtendedFormatIndex];
					A_0.WriteAttributeString(RecordTableEnumerator.b("㹅⹇͉⡋", a_), num2.ToString());
					num = 9;
					continue;
				}
				case 4:
					goto IL_64;
				case 5:
				{
					sprᬐ sprᬐ = A_1.Record;
					A_0.WriteAttributeString(RecordTableEnumerator.b("⑅㵇⍉⁋㩍㥏㱑ᵓ㉕", a_), sprᬐ.ᜁ().ToString());
					num = 2;
					continue;
				}
				case 6:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
				case 7:
					goto IL_1F3;
				case 8:
					goto IL_149;
				case 9:
					if (A_1.BuiltIn)
					{
						num = 5;
						continue;
					}
					goto IL_20C;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 6;
				}
			}
			IL_64:
			if (true)
			{
			}
			throw new ArgumentNullException();
			IL_116:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㍉⁋⭍", a_));
			IL_149:
			goto IL_20C;
			IL_1F3:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹅⥇㥉⑋M㕏║ѓ㝕⩗㽙㉛⩝⥟ౡcͥၧཀྵὫ", a_));
			IL_20C:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004510 RID: 17680 RVA: 0x0029A6A4 File Offset: 0x002996A4
	private void ᜀ(XmlWriter A_0, IDictionary<string, string> A_1, string A_2, string A_3, string A_4, spr\u24A5 A_5)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_99;
				case 2:
					goto IL_79;
				case 3:
					if (A_2.Length == 0)
					{
						num = 9;
						continue;
					}
					num = 8;
					continue;
				case 5:
					if (A_2 != null)
					{
						num = 0;
						continue;
					}
					goto IL_2B1;
				case 6:
					num = 15;
					continue;
				case 7:
					goto IL_F9;
				case 8:
					if (A_3 != null)
					{
						num = 6;
						continue;
					}
					goto IL_9E;
				case 9:
					goto IL_29B;
				case 10:
					goto IL_D9;
				case 11:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					num = 5;
					continue;
				case 12:
					goto IL_2F3;
				case 13:
				{
					if (A_4.Length == 0)
					{
						num = 1;
						continue;
					}
					IEnumerator<KeyValuePair<string, string>> enumerator = A_1.GetEnumerator();
					num = 10;
					continue;
				}
				case 14:
					num = 13;
					continue;
				case 15:
					if (A_3.Length == 0)
					{
						num = 12;
						continue;
					}
					num = 16;
					continue;
				case 16:
					if (A_4 != null)
					{
						num = 14;
						continue;
					}
					goto IL_25C;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 11;
				}
			}
			IL_79:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
			IL_99:
			goto IL_25C;
			IL_9E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽃⍅ㅇ୉㡋㩍≏㭑㙓⍕ⱗ㽙ቛ㽝ൟݡ", a_));
			IL_D9:
			goto IL_166;
			try
			{
				for (;;)
				{
					IL_166:
					num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_166;
							default:
							{
								if (false)
								{
								}
								IEnumerator<KeyValuePair<string, string>> enumerator;
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								KeyValuePair<string, string> keyValuePair = enumerator.Current;
								string key = keyValuePair.Key;
								string value = keyValuePair.Value;
								A_0.WriteStartElement(A_2);
								A_0.WriteAttributeString(A_3, key);
								A_0.WriteAttributeString(A_4, value);
								A_0.WriteEndElement();
								num = 3;
								continue;
							}
							}
							break;
						case 1:
							goto IL_219;
						case 2:
							num = 1;
							continue;
						}
						IL_1D2:
						num = 0;
						continue;
						goto IL_1D2;
					}
				}
				IL_219:
				return;
			}
			finally
			{
				num = 2;
				for (;;)
				{
					IEnumerator<KeyValuePair<string, string>> enumerator;
					switch (num)
					{
					case 0:
						goto IL_259;
					case 1:
						enumerator.Dispose();
						num = 0;
						continue;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 1;
				}
				IL_259:;
			}
			goto IL_25C;
			IL_F9:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぃ⥅ᭇ⽉㹋❍ㅏ㹑㵓ⱕ㵗", a_));
			IL_25C:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉃❅⑇㽉⥋ཌྷ⑏♑♓㽕㩗⽙⡛㭝⹟͡ॣͥ", a_));
			IL_29B:
			IL_2B1:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ぃ❅⽇щⵋ⍍㕏", a_));
			IL_2F3:
			goto IL_9E;
		}
		}
	}

	// Token: 0x06004511 RID: 17681 RVA: 0x0029A9BC File Offset: 0x002999BC
	internal static string ᜄ(string A_0)
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
		return char.ToLower(A_0[0]) + A_0.Remove(0, 1);
	}

	// Token: 0x06004512 RID: 17682 RVA: 0x0029AA18 File Offset: 0x00299A18
	internal static void ᜀ(XmlWriter A_0, string A_1, bool A_2, bool A_3)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 1:
				text = RecordTableEnumerator.b("獂", a_);
				goto IL_B7;
			case 2:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_7E;
					}
				}
				IL_7E:
				if (false)
				{
				}
				num = 1;
				continue;
			case 3:
				text = RecordTableEnumerator.b("牂", a_);
				goto IL_B7;
			case 4:
				num = 5;
				continue;
			case 5:
				if (!A_2)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
			case 6:
				return;
			}
			if (A_2 != A_3)
			{
				num = 4;
				continue;
			}
			break;
			IL_B7:
			string value = text;
			A_0.WriteAttributeString(A_1, value);
			if (true)
			{
			}
			num = 6;
		}
	}

	// Token: 0x06004513 RID: 17683 RVA: 0x0029AAFC File Offset: 0x00299AFC
	internal static void ᜁ(XmlWriter A_0, string A_1, int A_2, int A_3)
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
				break;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_60;
				}
				break;
			case 2:
			{
				string value = A_2.ToString();
				A_0.WriteAttributeString(A_1, value);
				num = 1;
				continue;
			}
			}
			if (A_2 == A_3)
			{
				return;
			}
			num = 2;
		}
		IL_60:
		if (false)
		{
		}
	}

	// Token: 0x06004514 RID: 17684 RVA: 0x0029AB7C File Offset: 0x00299B7C
	internal static void ᜀ(XmlWriter A_0, string A_1, double A_2, double A_3)
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
		spr\u1B7A.ᜀ(A_0, A_1, A_2, A_3, null);
	}

	// Token: 0x06004515 RID: 17685 RVA: 0x0029ABC4 File Offset: 0x00299BC4
	internal static void ᜀ(XmlWriter A_0, string A_1, double A_2, double A_3, string A_4)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_61;
				}
				break;
			case 2:
			{
				string value = XmlConvert.ToString(A_2);
				A_0.WriteAttributeString(A_1, A_4, value);
				if (true)
				{
				}
				num = 0;
				continue;
			}
			}
			if (A_2 == A_3)
			{
				return;
			}
			num = 2;
		}
		IL_61:
		if (false)
		{
		}
	}

	// Token: 0x06004516 RID: 17686 RVA: 0x0029AC44 File Offset: 0x00299C44
	internal static void ᜁ(XmlWriter A_0, string A_1, string A_2, string A_3)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_5D;
				}
				break;
			case 1:
				A_0.WriteAttributeString(A_1, A_2);
				num = 0;
				continue;
			}
			if (!(A_2 != A_3))
			{
				return;
			}
			if (true)
			{
			}
			num = 1;
		}
		IL_5D:
		if (false)
		{
		}
	}

	// Token: 0x06004517 RID: 17687 RVA: 0x0029ACC0 File Offset: 0x00299CC0
	internal static void ᜀ(XmlWriter A_0, string A_1, Enum A_2, Enum A_3)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_62;
				}
				break;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				A_0.WriteAttributeString(A_1, spr\u1B7A.ᜄ(A_2.ToString()));
				num = 0;
				continue;
			}
			if (A_2 == A_3)
			{
				return;
			}
			num = 2;
		}
		IL_62:
		if (false)
		{
		}
	}

	// Token: 0x06004518 RID: 17688 RVA: 0x0029AD44 File Offset: 0x00299D44
	internal static void ᜀ(XmlWriter A_0, string A_1, string A_2, string A_3)
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
					continue;
				default:
					goto IL_5D;
				}
				break;
			case 2:
				A_0.WriteElementString(A_1, A_2);
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (!(A_2 != A_3))
			{
				return;
			}
			num = 2;
		}
		IL_5D:
		if (false)
		{
		}
	}

	// Token: 0x06004519 RID: 17689 RVA: 0x0029ADC0 File Offset: 0x00299DC0
	private static void ᜀ(XmlWriter A_0, string A_1, string A_2, string A_3, string A_4)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.WriteElementString(A_4, A_1, null, A_2);
				num = 1;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_60;
				}
				break;
			case 2:
				if (true)
				{
				}
				break;
			}
			if (!(A_2 != A_3))
			{
				return;
			}
			num = 0;
		}
		IL_60:
		if (false)
		{
		}
	}

	// Token: 0x0600451A RID: 17690 RVA: 0x0029AE40 File Offset: 0x00299E40
	private static void ᜀ(XmlWriter A_0, string A_1, int A_2, int A_3)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				if (true)
				{
				}
				string value = A_2.ToString();
				A_0.WriteElementString(A_1, value);
				num = 2;
				continue;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_60;
				}
				break;
			}
			if (A_2 == A_3)
			{
				return;
			}
			num = 1;
		}
		IL_60:
		if (false)
		{
		}
	}

	// Token: 0x0600451B RID: 17691 RVA: 0x0029AEC0 File Offset: 0x00299EC0
	public void ᜂ(XmlWriter A_0, XlsWorksheetBase A_1)
	{
		int a_ = 6;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_3C;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						if (A_1 == null)
						{
							num = 0;
							continue;
						}
						goto IL_A1;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽┿❁ぃ", a_));
		IL_A1:
		throw new NotImplementedException();
	}

	// Token: 0x0600451C RID: 17692 RVA: 0x0029AF74 File Offset: 0x00299F74
	public void ᜀ(XmlWriter A_0, XlsCellRecordCollection A_1, Dictionary<int, int> A_2, string A_3, Dictionary<string, string> A_4, bool A_5)
	{
		int a_ = 1;
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_1.ContainsRow(num2 - 1))
				{
					num = 4;
					continue;
				}
				goto IL_11E;
			case 1:
				goto IL_108;
			case 2:
				goto IL_C3;
			case 3:
				goto IL_11E;
			case 4:
				goto IL_55;
			case 6:
				goto IL_C8;
			case 7:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䐶儸帺堼䬾Հ≂ㅄ♆", a_));
				this.ᜁ(A_0, A_4);
				num2 = A_1.FirstRow;
				int lastRow = A_1.LastRow;
				num = 6;
				continue;
			}
			case 8:
			{
				int lastRow;
				if (num2 <= lastRow)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_55;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			case 9:
				goto IL_C8;
			case 10:
				goto IL_50;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 7;
			continue;
			IL_55:
			sprᱧ a_2 = A_1.Table.ᜄ().ᜁ(num2 - 1);
			this.ᜀ(A_0, a_2, A_1, num2 - 1, A_2, A_3, A_5);
			num = 3;
			continue;
			IL_C8:
			num = 8;
			continue;
			IL_11E:
			num2++;
			num = 9;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_C3:
		throw new ArgumentNullException(RecordTableEnumerator.b("吶尸场儼䰾", a_));
		IL_108:
		if (true)
		{
		}
		A_0.WriteEndElement();
	}

	// Token: 0x0600451D RID: 17693 RVA: 0x0029B110 File Offset: 0x0029A110
	private void ᜁ(XmlWriter A_0, Dictionary<string, string> A_1)
	{
		int a_ = 11;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_130;
			case 1:
				try
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							Dictionary<string, string>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							KeyValuePair<string, string> keyValuePair = enumerator.Current;
							A_0.WriteAttributeString(keyValuePair.Key, keyValuePair.Value);
							num = 2;
							continue;
						}
						case 3:
							goto IL_120;
						case 4:
							num = 3;
							continue;
						}
						IL_D7:
						num = 0;
						continue;
						goto IL_D7;
					}
					IL_120:
					return;
				}
				finally
				{
					Dictionary<string, string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_130;
			case 3:
			{
				if (A_1.Count == 0)
				{
					num = 5;
					continue;
				}
				Dictionary<string, string>.Enumerator enumerator = A_1.GetEnumerator();
				num = 1;
				continue;
			}
			case 4:
				if (A_1 != null)
				{
					num = 0;
					continue;
				}
				goto IL_79;
			case 5:
				goto IL_151;
			case 6:
				goto IL_66;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_130:
				num = 3;
				continue;
			}
			if (false)
			{
			}
			if (A_0 == null)
			{
				num = 6;
			}
			else
			{
				num = 4;
			}
		}
		IL_66:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙀ㅂⱄ㍆ⱈ㥊", a_));
		IL_79:
		if (true)
		{
		}
		return;
		IL_151:
		goto IL_79;
	}

	// Token: 0x0600451E RID: 17694 RVA: 0x0029B284 File Offset: 0x0029A284
	private void ᜀ(XmlWriter A_0, sprᱧ A_1, XlsCellRecordCollection A_2, int A_3, Dictionary<int, int> A_4, string A_5, bool A_6)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 9;
					continue;
				case 2:
					goto IL_18F;
				case 3:
					goto IL_1D0;
				case 4:
					goto IL_84;
				case 5:
					num = 16;
					continue;
				case 6:
					A_1.ᜊ(true);
					num = 3;
					continue;
				case 7:
					if ((int)A_1.\u1718() != A_2.AppImplementation.ᜅ())
					{
						num = 5;
						continue;
					}
					goto IL_31A;
				case 8:
					if (A_4 != null)
					{
						num = 15;
						continue;
					}
					goto IL_20F;
				case 9:
					if (A_1.\u171C() >= 0)
					{
						num = 11;
						continue;
					}
					goto IL_105;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1FB;
					default:
						if (false)
						{
						}
						goto IL_105;
					}
					break;
				case 11:
				{
					string a_2 = (A_1.\u171C() + 1).ToString() + RecordTableEnumerator.b("片", a_) + (A_1.\u171E() + 1).ToString();
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㭇㩉ⵋ⁍⍏", a_), a_2, string.Empty);
					num = 10;
					continue;
				}
				case 12:
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㭇", a_), A_4[(int)A_1.ᜇ()], 0);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⭇㽉㽋㩍㽏㽑ቓ㥕⩗㝙㵛⩝", a_), A_1.\u1719(), false);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⁇㹉", a_), (double)A_1.\u1718() / 20.0, 12.75);
					num = 13;
					continue;
				case 13:
					goto IL_20F;
				case 14:
					if (A_6)
					{
						num = 1;
						continue;
					}
					goto IL_105;
				case 15:
					num = 17;
					continue;
				case 16:
					if (A_1.ᜌ())
					{
						num = 6;
						continue;
					}
					goto IL_31A;
				case 17:
					if (A_4.ContainsKey((int)A_1.ᜇ()))
					{
						num = 12;
						continue;
					}
					goto IL_20F;
				case 18:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("㩇╉㭋", a_));
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㩇", a_), (A_3 + 1).ToString(), string.Empty);
					num = 14;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 18;
				continue;
				IL_105:
				if (true)
				{
				}
				num = 8;
				continue;
				IL_20F:
				num = 7;
			}
			IL_84:
			goto IL_1FB;
			IL_18F:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇╉㭋", a_));
			IL_1D0:
			goto IL_31A;
			IL_1FB:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_31A:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⭇╉⁋≍ㅏ≑❓㍕㱗", a_), A_1.ᜣ(), false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⭇㽉㽋㩍㽏㽑᱓㍕ㅗ㵙㑛⩝", a_), A_1.\u1713(), false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⁇⍉⡋⩍㕏㱑", a_), A_1.ᜅ(), false);
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("❇㽉㡋≍㥏㱑ㅓᩕ㵗ⱙ㥛㉝", a_), (int)A_1.\u1717(), 0);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㱇≉╋ⵍ㭏ّ㭓♕", a_), A_1.\u171F(), false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㱇≉╋ⵍ㭏ၑ㭓≕", a_), A_1.ᜑ(), false);
			this.ᜀ(A_0, A_1, A_2, A_4, A_5);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600451F RID: 17695 RVA: 0x0029B660 File Offset: 0x0029A660
	private void ᜀ(XmlWriter A_0, sprᱧ A_1, XlsCellRecordCollection A_2, Dictionary<int, int> A_3, string A_4)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				RowStorageEnumerator rowStorageEnumerator;
				switch (num)
				{
				case 0:
					goto IL_14C;
				case 2:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.MulRK:
						this.ᜀ(A_0, (sprᨾ)biffRecordRaw, A_3);
						num = 8;
						continue;
					case TBIFFRecord.MulBlank:
						this.ᜀ(A_0, (sprᲀ)biffRecordRaw, A_3);
						num = 13;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				}
				case 3:
					num = 4;
					continue;
				case 4:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.Blank)
					{
						num = 7;
						continue;
					}
					spr\u171D spr_u171D = (spr\u171D)biffRecordRaw;
					this.ᜀ(A_0, spr_u171D.\u1714() + 1, spr_u171D.\u1713() + 1, (int)spr_u171D.\u1712(), A_3);
					num = 12;
					continue;
				}
				case 5:
					goto IL_231;
				case 6:
					if (A_1 == null)
					{
						num = 17;
						continue;
					}
					num = 16;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_14C;
				case 9:
					return;
				case 10:
					goto IL_80;
				case 11:
					goto IL_14C;
				case 12:
					goto IL_14C;
				case 13:
					goto IL_14C;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_231;
					}
					goto Block_7;
				case 15:
				{
					if (!rowStorageEnumerator.MoveNext())
					{
						num = 9;
						continue;
					}
					biffRecordRaw = (rowStorageEnumerator.Current as BiffRecordRaw);
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 2;
					continue;
				}
				case 16:
					if (A_2 == null)
					{
						num = 14;
						continue;
					}
					rowStorageEnumerator = (A_1.ᜀ(this.ᡉ) as RowStorageEnumerator);
					num = 0;
					continue;
				case 17:
					goto IL_12D;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 6;
				continue;
				IL_14C:
				num = 15;
				continue;
				IL_231:
				this.ᜀ(A_0, biffRecordRaw, rowStorageEnumerator, A_2, A_3, A_4);
				num = 11;
			}
			IL_80:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_12D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅❇㵉", a_));
			Block_7:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╅ⵇ♉⁋㵍", a_));
		}
		}
	}

	// Token: 0x06004520 RID: 17696 RVA: 0x0029B8EC File Offset: 0x0029A8EC
	private void ᜀ(XmlWriter A_0, BiffRecordRaw A_1, RowStorageEnumerator A_2, XlsCellRecordCollection A_3, Dictionary<int, int> A_4, string A_5)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 12;
			string a_4;
			spr\u1B7A.CellType a_5;
			for (;;)
			{
				spr᱒ a_3;
				string a_6;
				switch (num)
				{
				case 0:
					goto IL_168;
				case 1:
					goto IL_2C8;
				case 2:
					if (A_1.TypeCode == TBIFFRecord.Formula)
					{
						num = 8;
						continue;
					}
					goto IL_375;
				case 3:
					goto IL_112;
				case 4:
				{
					spr\u225F a_2;
					this.ᜀ(A_0, a_2);
					num = 19;
					continue;
				}
				case 5:
					num = 15;
					continue;
				case 6:
					if (A_3 == null)
					{
						num = 3;
						continue;
					}
					num = 18;
					continue;
				case 7:
					goto IL_332;
				case 8:
					a_3 = (spr᱒)A_1;
					num = 20;
					continue;
				case 9:
					goto IL_16D;
				case 10:
				{
					string text;
					if (this.ᜅ().InlineStrings.TryGetValue(text, out a_4))
					{
						num = 14;
						continue;
					}
					goto IL_16D;
				}
				case 11:
					if (A_4 != null)
					{
						num = 5;
						continue;
					}
					goto IL_332;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_274;
					default:
						if (false)
						{
						}
						if (A_1 == null)
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					}
					break;
				case 14:
					a_5 = spr\u1B7A.CellType.inlineStr;
					a_6 = RecordTableEnumerator.b("帶圸场吼儾⑀၂ㅄ㕆", a_);
					num = 9;
					continue;
				case 15:
				{
					spr\u23A5 spr_u23A;
					if (A_4.ContainsKey((int)spr_u23A.ᜆ()))
					{
						num = 16;
						continue;
					}
					goto IL_332;
				}
				case 16:
				{
					spr\u23A5 spr_u23A;
					int num2 = (int)spr_u23A.ᜆ();
					num2 = A_4[num2];
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䐶", a_), num2, 0);
					num = 7;
					continue;
				}
				case 17:
					goto IL_285;
				case 18:
				{
					if (true)
					{
					}
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					A_0.WriteStartElement(A_5);
					spr\u23A5 spr_u23A = A_1 as spr\u23A5;
					string text = sprṔ.ᜂ(spr_u23A.ᜅ() + 1, spr_u23A.ᜄ() + 1);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䔶", a_), text, null);
					num = 11;
					continue;
				}
				case 19:
					goto IL_285;
				case 20:
				{
					spr\u225F a_2;
					if ((a_2 = A_2.ᜀ()) != null)
					{
						goto IL_274;
					}
					this.ᜀ(A_0, a_3, A_3);
					num = 17;
					continue;
				}
				case 21:
					goto IL_94;
				case 22:
					goto IL_29D;
				}
				if (A_0 == null)
				{
					num = 21;
					continue;
				}
				num = 13;
				continue;
				IL_16D:
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䌶", a_), a_6, RecordTableEnumerator.b("夶", a_));
				num = 2;
				continue;
				IL_274:
				num = 4;
				continue;
				IL_285:
				this.ᜀ(A_0, a_3, a_5, A_2);
				num = 22;
				continue;
				IL_332:
				a_5 = this.ᜀ(A_1, out a_6);
				a_4 = null;
				num = 10;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_112:
			throw new ArgumentNullException(RecordTableEnumerator.b("吶尸场儼䰾", a_));
			IL_168:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀", a_));
			IL_29D:
			goto IL_375;
			IL_2C8:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶嘸䰺渼䬾⹀ㅂ⑄⁆ⱈ๊⍌㩎㱐㙒❔㙖ⵘ㑚⽜", a_));
			IL_375:
			this.ᜀ(A_0, A_1, a_5, a_4);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004521 RID: 17697 RVA: 0x0029BC80 File Offset: 0x0029AC80
	private void ᜀ(XmlWriter A_0, int A_1, int A_2, int A_3, Dictionary<int, int> A_4)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				if (A_4 == null)
				{
					num = 3;
					continue;
				}
				goto IL_A2;
			case 3:
				goto IL_8C;
			}
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
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_8C:
		throw new ArgumentNullException(RecordTableEnumerator.b("⠿⍁㝃⹅ه⽉㭋ṍㅏ⁑ㅓ㡕ⱗፙ㉛㩝՟ᩡţᕥ", a_));
		IL_A2:
		A_0.WriteStartElement(RecordTableEnumerator.b("⌿", a_));
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㈿", a_), sprṔ.ᜂ(A_2, A_1), string.Empty);
		int a_2 = A_4[A_3];
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㌿", a_), a_2, 0);
		A_0.WriteEndElement();
	}

	// Token: 0x06004522 RID: 17698 RVA: 0x0029BD8C File Offset: 0x0029AD8C
	private void ᜀ(XmlWriter A_0, sprᲀ A_1, Dictionary<int, int> A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_EB;
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					int a_2;
					int num3;
					List<ushort> list;
					this.ᜀ(A_0, a_2, num3 + num2, Convert.ToInt32(list[num2]), A_2);
					num2++;
					num = 0;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_167;
					default:
						goto IL_73;
					}
					break;
				case 3:
					goto IL_E6;
				case 4:
					return;
				case 5:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 6:
				{
					if (A_2 == null)
					{
						goto IL_167;
					}
					if (true)
					{
					}
					int a_2 = A_1.\u1714() + 1;
					int num3 = A_1.ᜆ() + 1;
					List<ushort> list = A_1.ᜄ();
					int num2 = 0;
					int count = list.Count;
					num = 9;
					continue;
				}
				case 7:
					goto IL_173;
				case 9:
					goto IL_EB;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 5;
				continue;
				IL_EB:
				num = 1;
				continue;
				IL_167:
				num = 7;
			}
			IL_73:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_E6:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭅㵇♉๋≍ㅏ㱑㽓ѕ㵗㥙㍛ⱝџ", a_));
			IL_173:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹅⥇㥉⑋M㕏║ѓ㝕⩗㽙㉛⩝⥟ౡcͥၧཀྵὫ", a_));
		}
		}
	}

	// Token: 0x06004523 RID: 17699 RVA: 0x0029BF28 File Offset: 0x0029AF28
	private void ᜀ(XmlWriter A_0, sprᨾ A_1, Dictionary<int, int> A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (A_2 == null)
					{
						goto IL_1DC;
					}
					int a_2 = A_1.\u1714() + 1;
					int num2 = A_1.ᜅ() + 1;
					List<sprᨾ.ᜀ> list = A_1.ᜀ();
					int num3 = 0;
					int count = list.Count;
					num = 8;
					continue;
				}
				case 1:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1DC;
					default:
						goto IL_73;
					}
					break;
				case 3:
					goto IL_1E8;
				case 4:
					return;
				case 5:
					goto IL_E1;
				case 7:
				{
					int num3;
					int count;
					if (num3 >= count)
					{
						num = 4;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("帼", a_));
					List<sprᨾ.ᜀ> list;
					sprᨾ.ᜀ ᜀ = list[num3];
					int a_2;
					int num2;
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("似", a_), sprṔ.ᜂ(num2 + num3, a_2), string.Empty);
					int key = (int)ᜀ.ᜀ();
					int a_3 = A_2[key];
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("丼", a_), a_3, 0);
					A_0.WriteElementString(RecordTableEnumerator.b("䬼", a_), XmlConvert.ToString(ᜀ.ᜂ()));
					A_0.WriteEndElement();
					num3++;
					num = 9;
					continue;
				}
				case 8:
					goto IL_E6;
				case 9:
					goto IL_E6;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
				IL_E6:
				num = 7;
				continue;
				IL_1DC:
				num = 3;
			}
			IL_73:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_E1:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("值䨾ⵀᅂ⹄ᕆⱈ⡊≌㵎㕐", a_));
			IL_1E8:
			throw new ArgumentNullException(RecordTableEnumerator.b("唼帾㉀⭂ୄ≆㹈ᭊⱌ㵎㑐㵒⅔Ṗ㝘㽚㡜❞Ѡၢ", a_));
		}
		}
	}

	// Token: 0x06004524 RID: 17700 RVA: 0x0029C140 File Offset: 0x0029B140
	private void ᜀ(XmlWriter A_0, spr\u225F A_1)
	{
		int a_ = 3;
		int num = 4;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("弸", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䴸", a_), spr\u1B7A.FormulaType.array.ToString());
				A_0.WriteAttributeString(RecordTableEnumerator.b("堸堺尼", a_), RecordTableEnumerator.b("䴸䤺䠼娾", a_));
				text = this.ᡈ.ᜁ(A_1.ᜅ());
				num = 3;
				continue;
			case 1:
				goto IL_51;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_FB;
				}
				break;
			case 3:
				if (text.Length > 8000)
				{
					num = 5;
					continue;
				}
				goto IL_17B;
			case 5:
				goto IL_E3;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_51:
		throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
		IL_E3:
		throw new ApplicationException(RecordTableEnumerator.b("缸吺似刾㑀⽂⑄杆╈⹊⍌⡎═㭒畔㹖⩘筚⥜ぞ๠䍢।ࡦݨ౪䍬佮㱰ቲ൴Ṷᑸ๺ၼ彾ﲈ꾎﶐ﮔ붜튠莢", a_) + 8000 + RecordTableEnumerator.b("᜸", a_));
		IL_FB:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似帾㡀ᅂ⁄⑆♈㥊⥌", a_));
		IL_17B:
		string value = sprṔ.ᜀ(A_1.ᜉ() + 1, A_1.ᜈ() + 1, A_1.\u170D() + 1, A_1.ᜀ() + 1);
		A_0.WriteAttributeString(RecordTableEnumerator.b("䬸帺嬼", a_), value);
		A_0.WriteString(text);
		A_0.WriteEndElement();
	}

	// Token: 0x06004525 RID: 17701 RVA: 0x0029C310 File Offset: 0x0029B310
	private void ᜀ(XmlWriter A_0, spr᱒ A_1, XlsCellRecordCollection A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 14;
			string text;
			for (;;)
			{
				sprᱧ sprᱧ;
				spr\u252B spr_u252B;
				Ptg ptg;
				switch (num)
				{
				case 0:
					if (sprᱧ.ᜊ(spr_u252B.ᜆ()))
					{
						num = 1;
						continue;
					}
					goto IL_BA;
				case 1:
					return;
				case 2:
					goto IL_89;
				case 3:
					goto IL_1A3;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E3;
					default:
						if (false)
						{
						}
						if (A_1 == null)
						{
							num = 3;
							continue;
						}
						num = 18;
						continue;
					}
					break;
				case 5:
					goto IL_249;
				case 6:
					if (!this.ᡈ.ᜂ(A_1.ᜂ()))
					{
						num = 8;
						continue;
					}
					num = 15;
					continue;
				case 7:
					goto IL_2E3;
				case 8:
					num = 12;
					continue;
				case 9:
					goto IL_84;
				case 10:
					if (text.Length > 8000)
					{
						num = 17;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("ⅆ", a_));
					num = 6;
					continue;
				case 11:
					if (ptg.TokenCode == FormulaToken.tExp)
					{
						num = 7;
						continue;
					}
					goto IL_BA;
				case 12:
					goto IL_257;
				case 13:
					text = UtilityMethods.ᜀ(text);
					num = 2;
					continue;
				case 15:
					goto IL_1CC;
				case 16:
					if (text[0] == '=')
					{
						num = 13;
						continue;
					}
					goto IL_89;
				case 17:
					goto IL_B5;
				case 18:
					if (A_2 == null)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					ptg = A_1.ᜂ()[0];
					num = 11;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 4;
				continue;
				IL_89:
				num = 10;
				continue;
				IL_BA:
				this.ᡈ.ᜃ(A_1.ᜂ());
				text = this.ᡈ.ᜀ(A_1.ᜂ(), 0, 0, false, null, false, true, A_2.sheet);
				num = 16;
				continue;
				IL_2E3:
				spr_u252B = (ptg as spr\u252B);
				sprᱧ = A_2.Table.ᜄ().ᜁ(spr_u252B.ᜇ());
				num = 0;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
			IL_B5:
			throw new ApplicationException(RecordTableEnumerator.b("੆⡈㍊⑌≎⑐㹒畔ㅖ㙘⥚ぜ⩞ൠɢ䕤୦౨ժ੬᭮ᥰ卲ᱴѶ奸", a_) + 8000 + RecordTableEnumerator.b("楆", a_));
			IL_1A3:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈㥊⁌㩎㵐㉒ݔ㉖㩘㑚⽜㭞", a_));
			IL_1CC:
			bool flag = A_1.ᜃ();
			goto IL_2E9;
			IL_249:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑆ⱈ❊⅌㱎", a_));
			IL_257:
			flag = true;
			IL_2E9:
			bool a_2 = flag;
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⑆⡈", a_), a_2, false);
			A_0.WriteString(text);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004526 RID: 17702 RVA: 0x0029C62C File Offset: 0x0029B62C
	private void ᜀ(XmlWriter A_0, BiffRecordRaw A_1, spr\u1B7A.CellType A_2, string A_3)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 8;
			string value;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AD;
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					num = 5;
					continue;
				case 3:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Number:
						goto IL_1A8;
					case TBIFFRecord.Label:
						goto IL_298;
					case TBIFFRecord.BoolErr:
					{
						spr\u249B spr_u249B = (spr\u249B)A_1;
						num = 4;
						continue;
					}
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CD;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					break;
				}
				case 4:
				{
					spr\u249B spr_u249B;
					if (spr_u249B.ᜂ())
					{
						num = 13;
						continue;
					}
					value = spr_u249B.ᜄ().ToString();
					num = 9;
					continue;
				}
				case 5:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.RK)
					{
						num = 1;
						continue;
					}
					goto IL_1A8;
				}
				case 6:
					if (A_2 == spr\u1B7A.CellType.inlineStr)
					{
						num = 7;
						continue;
					}
					goto IL_121;
				case 7:
					goto IL_DF;
				case 9:
					goto IL_11C;
				case 10:
				{
					if (A_1 == null)
					{
						num = 11;
						continue;
					}
					TBIFFRecord typeCode = A_1.TypeCode;
					num = 14;
					continue;
				}
				case 11:
					goto IL_FF;
				case 12:
					goto IL_75;
				case 13:
				{
					spr\u249B spr_u249B;
					value = FormulaUtil.ErrorCodeToName[(int)spr_u249B.ᜄ()];
					num = 0;
					continue;
				}
				case 14:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.LabelSST)
					{
						num = 15;
						continue;
					}
					num = 6;
					continue;
				}
				case 15:
					goto IL_1CD;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num = 10;
				continue;
				IL_1CD:
				num = 3;
			}
			IL_75:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
			IL_7A:
			A_0.WriteElementString(RecordTableEnumerator.b("㹇", a_), value);
			return;
			IL_AD:
			goto IL_7A;
			IL_DF:
			A_0.WriteStartElement(RecordTableEnumerator.b("ⅇ㥉", a_));
			A_0.WriteElementString(RecordTableEnumerator.b("㱇", a_), A_3);
			A_0.WriteEndElement();
			return;
			IL_FF:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑", a_));
			IL_11C:
			goto IL_7A;
			IL_121:
			A_0.WriteElementString(RecordTableEnumerator.b("㹇", a_), (A_1 as spr\u1C7C).ᜁ().ToString());
			return;
			IL_1A8:
			A_0.WriteElementString(RecordTableEnumerator.b("㹇", a_), XmlConvert.ToString(((spr\u2230)A_1).ᜀ()));
			return;
			IL_298:
			A_0.WriteElementString(RecordTableEnumerator.b("㹇", a_), ((spr\u2170)A_1).ᜁ());
			return;
		}
		}
	}

	// Token: 0x06004527 RID: 17703 RVA: 0x0029C8F0 File Offset: 0x0029B8F0
	private void ᜀ(XmlWriter A_0, spr᱒ A_1, spr\u1B7A.CellType A_2, RowStorageEnumerator A_3)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_17E;
				case 1:
					goto IL_232;
				case 2:
					goto IL_97;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_172;
					default:
						if (false)
						{
						}
						if (!A_1.ᜁ())
						{
							num = 12;
							continue;
						}
						num = 5;
						continue;
					}
					break;
				case 4:
				{
					double d;
					if (!double.IsNaN(d))
					{
						num = 7;
						continue;
					}
					return;
				}
				case 5:
					goto IL_139;
				case 6:
					return;
				case 7:
					A_0.WriteElementString(RecordTableEnumerator.b("ぅ", a_), XmlConvert.ToString(A_1.\u170D()));
					num = 8;
					continue;
				case 8:
					return;
				case 9:
					goto IL_74;
				case 11:
					if (A_3 == null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					num = 13;
					continue;
				case 12:
					num = 1;
					continue;
				case 13:
					switch (A_2)
					{
					case spr\u1B7A.CellType.b:
						num = 3;
						continue;
					case spr\u1B7A.CellType.e:
						goto IL_CB;
					case spr\u1B7A.CellType.inlineStr:
					case spr\u1B7A.CellType.s:
						return;
					case spr\u1B7A.CellType.n:
					{
						double d = A_1.\u170D();
						num = 4;
						continue;
					}
					case spr\u1B7A.CellType.str:
						goto IL_180;
					default:
						num = 6;
						continue;
					}
					break;
				case 14:
					if (A_1 == null)
					{
						goto IL_172;
					}
					num = 11;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				num = 14;
				continue;
				IL_172:
				num = 0;
			}
			IL_74:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_97:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅❇㵉Ὃ㩍㽏⁑㕓ㅕ㵗Ὑ㉛⭝ൟݡᙣݥᱧթṫ", a_));
			IL_CB:
			string value = FormulaUtil.ErrorCodeToName[(int)A_1.ᜏ()];
			A_0.WriteElementString(RecordTableEnumerator.b("ぅ", a_), value);
			return;
			IL_139:
			string text = RecordTableEnumerator.b("睅", a_);
			goto IL_20F;
			IL_17E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏", a_));
			IL_180:
			A_0.WriteElementString(RecordTableEnumerator.b("ぅ", a_), A_3.GetFormulaStringValue());
			return;
			IL_20F:
			string value2 = text;
			A_0.WriteElementString(RecordTableEnumerator.b("ぅ", a_), value2);
			return;
			IL_232:
			text = RecordTableEnumerator.b("癅", a_);
			goto IL_20F;
		}
		}
	}

	// Token: 0x06004528 RID: 17704 RVA: 0x0029CB6C File Offset: 0x0029BB6C
	private spr\u1B7A.CellType ᜀ(BiffRecordRaw A_0, out string A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 6;
			spr\u1B7A.CellType result;
			for (;;)
			{
				TBIFFRecord typeCode;
				switch (num)
				{
				case 0:
				{
					spr\u249B spr_u249B;
					if (spr_u249B.ᜂ())
					{
						num = 10;
						continue;
					}
					result = spr\u1B7A.CellType.b;
					A_1 = RecordTableEnumerator.b("唶", a_);
					num = 27;
					continue;
				}
				case 1:
					return result;
				case 2:
					result = spr\u1B7A.CellType.b;
					A_1 = RecordTableEnumerator.b("唶", a_);
					num = 16;
					continue;
				case 3:
					if (typeCode != TBIFFRecord.MulRK)
					{
						num = 21;
						continue;
					}
					goto IL_FE;
				case 4:
					num = 15;
					continue;
				case 5:
					num = 14;
					continue;
				case 7:
				{
					if (typeCode != TBIFFRecord.Formula)
					{
						num = 22;
						continue;
					}
					spr᱒ spr᱒ = (spr᱒)A_0;
					num = 26;
					continue;
				}
				case 8:
					if (typeCode <= TBIFFRecord.RString)
					{
						num = 12;
						continue;
					}
					num = 11;
					continue;
				case 9:
				{
					spr᱒ spr᱒;
					if (spr᱒.ᜅ())
					{
						num = 33;
						continue;
					}
					result = spr\u1B7A.CellType.n;
					A_1 = RecordTableEnumerator.b("夶", a_);
					num = 13;
					continue;
				}
				case 10:
					result = spr\u1B7A.CellType.e;
					A_1 = RecordTableEnumerator.b("制", a_);
					num = 1;
					continue;
				case 11:
					if (typeCode != TBIFFRecord.LabelSST)
					{
						num = 5;
						continue;
					}
					goto IL_3AA;
				case 12:
					num = 7;
					continue;
				case 13:
					goto IL_1D3;
				case 14:
					switch (typeCode)
					{
					case TBIFFRecord.Number:
						goto IL_FE;
					case TBIFFRecord.Label:
						result = spr\u1B7A.CellType.str;
						A_1 = RecordTableEnumerator.b("䐶䴸䤺", a_);
						num = 30;
						continue;
					case TBIFFRecord.BoolErr:
					{
						spr\u249B spr_u249B = (spr\u249B)A_0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					default:
						num = 24;
						continue;
					}
					break;
				case 15:
					goto IL_3A5;
				case 16:
					goto IL_1B0;
				case 17:
					return result;
				case 18:
					if (typeCode != TBIFFRecord.RString)
					{
						num = 23;
						continue;
					}
					goto IL_3AA;
				case 19:
					if (typeCode != TBIFFRecord.RK)
					{
						num = 4;
						continue;
					}
					goto IL_FE;
				case 20:
					goto IL_31F;
				case 21:
					num = 18;
					continue;
				case 22:
					num = 3;
					continue;
				case 23:
					num = 34;
					continue;
				case 24:
					num = 19;
					continue;
				case 25:
					result = spr\u1B7A.CellType.e;
					A_1 = RecordTableEnumerator.b("制", a_);
					num = 20;
					continue;
				case 26:
				{
					spr᱒ spr᱒;
					if (spr᱒.ᜋ())
					{
						num = 2;
						continue;
					}
					num = 32;
					continue;
				}
				case 27:
					goto IL_1F6;
				case 28:
					goto IL_22A;
				case 29:
					goto IL_CA;
				case 30:
					goto IL_342;
				case 31:
					goto IL_11C;
				case 32:
				{
					if (true)
					{
					}
					spr᱒ spr᱒;
					if (spr᱒.ᜄ())
					{
						num = 25;
						continue;
					}
					num = 9;
					continue;
				}
				case 33:
					result = spr\u1B7A.CellType.str;
					A_1 = RecordTableEnumerator.b("䐶䴸䤺", a_);
					num = 28;
					continue;
				case 34:
					goto IL_207;
				}
				if (A_0 == null)
				{
					num = 29;
					continue;
				}
				typeCode = A_0.TypeCode;
				num = 8;
				continue;
				IL_FE:
				result = spr\u1B7A.CellType.n;
				A_1 = RecordTableEnumerator.b("夶", a_);
				num = 31;
				continue;
				IL_3AA:
				result = spr\u1B7A.CellType.s;
				A_1 = RecordTableEnumerator.b("䐶", a_);
				num = 17;
			}
			IL_CA:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀", a_));
			IL_11C:
			IL_1B0:
			IL_1D3:
			IL_1F6:
			return result;
			IL_207:
			goto IL_22F;
			IL_22A:
			return result;
			IL_22F:
			throw new NotImplementedException(RecordTableEnumerator.b("䌶䀸䬺堼", a_));
			IL_31F:
			IL_342:
			return result;
			IL_3A5:
			goto IL_22F;
		}
		}
	}

	// Token: 0x06004529 RID: 17705 RVA: 0x0029CFCC File Offset: 0x0029BFCC
	public void ᜏ(XmlWriter A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				SSTDictionary sstdictionary;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					sstdictionary.RemoveUnnecessaryStrings();
					num = 2;
					continue;
				case 1:
					goto IL_10E;
				case 2:
					goto IL_73;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_ED;
					default:
						goto IL_145;
					}
					break;
				case 4:
					goto IL_ED;
				case 6:
					goto IL_12A;
				case 7:
				{
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					object sstcontentByIndex = sstdictionary.GetSSTContentByIndex(num2);
					this.ᜀ(A_0, sstcontentByIndex);
					num2++;
					if (true)
					{
					}
					num = 1;
					continue;
				}
				case 8:
					goto IL_10E;
				case 9:
					goto IL_109;
				case 10:
					if (!this.ᡇ.IsCreated)
					{
						num = 0;
						continue;
					}
					goto IL_73;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 4;
				continue;
				IL_73:
				A_0.WriteStartDocument();
				A_0.WriteStartElement(RecordTableEnumerator.b("㝃㕅㱇", a_), RecordTableEnumerator.b("ⱃ㉅㱇㩉癋慍罏⅑㝓㹕㵗㝙㵛ⵝ也ൡᑣͥ٧ቩūɭᙯᵱٳ᭵᥷๹ཻ偽ꦅﮇ憎ﺋﺕﶗﾙ첟趡隣隥颧鲩莫쎭톯\udbb1\udab3", a_));
				count = sstdictionary.Count;
				A_0.WriteAttributeString(RecordTableEnumerator.b("ㅃ⡅ⅇ㭉㥋⭍ፏ㵑⅓㡕ⱗ", a_), count.ToString());
				num2 = 0;
				num = 8;
				continue;
				IL_ED:
				if (this.ᡇ.HasInlineStrings)
				{
					num = 9;
					continue;
				}
				sstdictionary = this.ᡇ.InnerSST;
				num = 10;
				continue;
				IL_10E:
				num = 7;
			}
			IL_109:
			this.ᡇ.SSTStream.Position = 0L;
			ShapeParser.WriteNodeFromStream(A_0, this.ᡇ.SSTStream);
			return;
			IL_12A:
			A_0.WriteEndElement();
			return;
			IL_145:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		}
		}
	}

	// Token: 0x0600452A RID: 17706 RVA: 0x0029D1C8 File Offset: 0x0029C1C8
	private void ᜀ(XmlWriter A_0, object A_1)
	{
		int a_ = 5;
		int num = 13;
		for (;;)
		{
			string text;
			string text2;
			int length;
			switch (num)
			{
			case 0:
				goto IL_10E;
			case 1:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䠺吼", a_));
				spr\u223A spr_u223A = A_1 as spr\u223A;
				num = 3;
				continue;
			}
			case 2:
				goto IL_17E;
			case 3:
			{
				spr\u223A spr_u223A;
				if (spr_u223A != null)
				{
					num = 4;
					continue;
				}
				goto IL_91;
			}
			case 4:
				num = 6;
				continue;
			case 5:
				goto IL_7B;
			case 6:
			{
				spr\u223A spr_u223A;
				if (spr_u223A.ᜆ() > 0)
				{
					num = 11;
					continue;
				}
				goto IL_91;
			}
			case 7:
				num = 9;
				continue;
			case 8:
				text = A_1.ToString();
				goto IL_261;
			case 9:
			{
				spr\u223A spr_u223A;
				text = spr_u223A.ᜏ();
				goto IL_261;
			}
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7B;
				}
				if (false)
				{
				}
				if (text2[0] != ' ')
				{
					num = 14;
					continue;
				}
				goto IL_113;
			case 11:
			{
				spr\u223A spr_u223A;
				this.ᜀ(A_0, spr_u223A);
				num = 0;
				continue;
			}
			case 12:
				goto IL_1A8;
			case 14:
				num = 20;
				continue;
			case 15:
				goto IL_180;
			case 16:
				goto IL_113;
			case 17:
			{
				spr\u223A spr_u223A;
				if (spr_u223A != null)
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			}
			case 18:
				num = 10;
				continue;
			case 19:
				if (length > 0)
				{
					num = 18;
					continue;
				}
				goto IL_180;
			case 20:
				if (text2[length - 1] == ' ')
				{
					num = 16;
					continue;
				}
				goto IL_180;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 1;
			continue;
			IL_91:
			num = 17;
			continue;
			IL_113:
			A_0.WriteAttributeString(RecordTableEnumerator.b("䌺值匾", a_), RecordTableEnumerator.b("䠺䴼帾≀♂", a_), null, RecordTableEnumerator.b("䬺似娾㉀♂㝄ㅆⱈ", a_));
			num = 15;
			continue;
			IL_180:
			text2 = this.ᜂ(text2);
			text2 = this.ᜃ(text2);
			A_0.WriteString(text2);
			A_0.WriteEndElement();
			num = 12;
			continue;
			IL_261:
			text2 = text;
			text2 = text2.Replace(RecordTableEnumerator.b("ㄺ", a_), RecordTableEnumerator.b("㘺㜼", a_));
			length = text2.Length;
			A_0.WriteStartElement(RecordTableEnumerator.b("伺", a_));
			if (true)
			{
			}
			num = 19;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_10E:
		goto IL_2CA;
		IL_17E:
		throw new ArgumentNullException(RecordTableEnumerator.b("伺堼䜾㕀", a_));
		IL_1A8:
		IL_2CA:
		A_0.WriteEndElement();
	}

	// Token: 0x0600452B RID: 17707 RVA: 0x0029D4A8 File Offset: 0x0029C4A8
	private string ᜃ(string A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder();
				int num = 10;
				for (;;)
				{
					int num2;
					int num3;
					char[] array;
					switch (num)
					{
					case 0:
						goto IL_F6;
					case 1:
						goto IL_15A;
					case 2:
					{
						if (num2 > 56319)
						{
							num = 0;
							continue;
						}
						char c;
						stringBuilder.Append(c);
						num = 6;
						continue;
					}
					case 3:
					{
						int length = A_0.Length;
						num = 5;
						continue;
					}
					case 4:
						goto IL_133;
					case 5:
						goto IL_B5;
					case 6:
						goto IL_CD;
					case 7:
					{
						char c;
						if (Array.IndexOf<char>(spr\u1B7A.ᡆ, c) >= 0)
						{
							num = 8;
							continue;
						}
						goto IL_F6;
					}
					case 8:
						goto IL_182;
					case 9:
						goto IL_CD;
					case 10:
						if (A_0 != null)
						{
							num = 3;
							continue;
						}
						goto IL_B5;
					case 11:
						goto IL_133;
					case 12:
					{
						if (num3 >= array.Length)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						char c = array[num3];
						num2 = (int)c;
						num = 13;
						continue;
					}
					case 13:
						if (num2 >= 32)
						{
							goto IL_8C;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_182;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 14:
						num = 7;
						continue;
					}
					break;
					IL_8C:
					num = 2;
					continue;
					IL_182:
					goto IL_8C;
					IL_B5:
					array = A_0.ToCharArray();
					num3 = 0;
					num = 11;
					continue;
					IL_CD:
					num3++;
					num = 4;
					continue;
					IL_F6:
					stringBuilder.Append(string.Format(RecordTableEnumerator.b("栶䄸䀺഼䈾Ṁ", a_), num2.ToString(RecordTableEnumerator.b("漶സ", a_))));
					num = 9;
					continue;
					IL_133:
					num = 12;
				}
			}
			IL_15A:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x0600452C RID: 17708 RVA: 0x0029D688 File Offset: 0x0029C688
	private string ᜂ(string A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder(A_0);
				int num = 0;
				int num2 = 0;
				int num3 = 1;
				for (;;)
				{
					int num6;
					switch (num3)
					{
					case 0:
					{
						string a_2;
						if (spr\u1B7A.ᜁ(a_2))
						{
							num3 = 3;
							continue;
						}
						goto IL_18D;
					}
					case 1:
						goto IL_130;
					case 2:
						goto IL_151;
					case 3:
					{
						if (true)
						{
						}
						int num4;
						stringBuilder.Insert(num4 - 2 + num, RecordTableEnumerator.b("携䔼༾煀療̈́", a_));
						num += RecordTableEnumerator.b("携䔼༾煀療̈́", a_).Length;
						num3 = 8;
						continue;
					}
					case 4:
					{
						int num5;
						if (num5 == 4)
						{
							num3 = 11;
							continue;
						}
						goto IL_18D;
					}
					case 5:
					{
						int num4;
						int num5 = num6 - num4;
						num3 = 4;
						continue;
					}
					case 6:
						goto IL_130;
					case 7:
						if (num6 != -1)
						{
							num3 = 5;
							continue;
						}
						goto IL_1E2;
					case 8:
						goto IL_18D;
					case 9:
					{
						if (num2 >= A_0.Length)
						{
							num3 = 2;
							continue;
						}
						int num4 = A_0.IndexOf(RecordTableEnumerator.b("携䔼", a_), num2);
						num3 = 12;
						continue;
					}
					case 10:
					{
						int num4;
						num4 += 2;
						num6 = A_0.IndexOf(RecordTableEnumerator.b("携", a_), num4);
						num3 = 7;
						continue;
					}
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							int num4;
							string a_2 = A_0.Substring(num4, 4);
							break;
						}
						}
						num3 = 0;
						continue;
					case 12:
					{
						int num4;
						if (num4 != -1)
						{
							num3 = 10;
							continue;
						}
						goto IL_1E2;
					}
					}
					break;
					IL_130:
					num3 = 9;
					continue;
					IL_18D:
					num2 = num6;
					num3 = 6;
				}
			}
			IL_151:
			IL_1E2:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x0600452D RID: 17709 RVA: 0x0029D880 File Offset: 0x0029C880
	private static bool ᜁ(string A_0)
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
		int num;
		return int.TryParse(A_0, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num);
	}

	// Token: 0x0600452E RID: 17710 RVA: 0x0029D8D0 File Offset: 0x0029C8D0
	private void ᜀ(XmlWriter A_0, spr\u223A A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 1;
			XlsFontsCollection innerFonts;
			string text;
			string a_2;
			int a_3;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					A_1.ᜈ();
					innerFonts = this.ᡇ.InnerFonts;
					SortedList<int, int> sortedList = A_1.ᜇ();
					text = A_1.ᜏ();
					a_2 = string.Empty;
					a_3 = -1;
					num2 = 0;
					IEnumerator<KeyValuePair<int, int>> enumerator = sortedList.GetEnumerator();
					num = 3;
					continue;
				}
				case 2:
					goto IL_1A1;
				case 3:
					goto IL_1E6;
				case 4:
					goto IL_56;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
			}
			IL_56:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_156:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠻嬽㠿㙁", a_));
			IL_1A1:
			goto IL_156;
			IL_1E6:
			try
			{
				num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						IEnumerator<KeyValuePair<int, int>> enumerator;
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						KeyValuePair<int, int> keyValuePair = enumerator.Current;
						int num3 = keyValuePair.Key - num2;
						a_2 = text.Substring(num2, num3);
						this.ᜀ(A_0, innerFonts, a_2, a_3);
						a_3 = keyValuePair.Value;
						num2 += num3;
						num = 3;
						continue;
					}
					case 2:
						goto IL_BA;
					case 3:
						goto IL_9F;
					case 4:
						goto IL_111;
					}
					goto IL_81;
					IL_9F:
					num = 0;
					continue;
					IL_81:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_BA:
						num = 4;
						break;
					default:
						if (false)
						{
						}
						goto IL_9F;
					}
				}
				IL_111:
				goto IL_1EB;
			}
			finally
			{
				num = 2;
				for (;;)
				{
					IEnumerator<KeyValuePair<int, int>> enumerator;
					switch (num)
					{
					case 0:
						goto IL_153;
					case 1:
						enumerator.Dispose();
						num = 0;
						continue;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 1;
				}
				IL_153:;
			}
			goto IL_156;
			IL_1EB:
			a_2 = text.Substring(num2);
			this.ᜀ(A_0, innerFonts, a_2, a_3);
			return;
		}
		}
	}

	// Token: 0x0600452F RID: 17711 RVA: 0x0029DAEC File Offset: 0x0029CAEC
	private void ᜀ(XmlWriter A_0, XlsFontsCollection A_1, string A_2, int A_3)
	{
		int a_ = 14;
		int num = 8;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				num = 10;
				continue;
			case 1:
				goto IL_22D;
			case 2:
				num = 9;
				continue;
			case 3:
				num = 15;
				continue;
			case 4:
				if (!A_2.EndsWith(RecordTableEnumerator.b("䵃", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_1A1;
			case 5:
				num = 7;
				continue;
			case 6:
				goto IL_177;
			case 7:
				if (!A_2.EndsWith(RecordTableEnumerator.b("䥃䱅", a_)))
				{
					num = 20;
					continue;
				}
				goto IL_1A1;
			case 9:
				if (!A_2.StartsWith(RecordTableEnumerator.b("䥃䱅", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_1A1;
			case 10:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				A_2 = A_2.Replace(RecordTableEnumerator.b("乃", a_), RecordTableEnumerator.b("䥃䱅", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("㙃", a_));
				num = 12;
				continue;
			case 11:
				goto IL_1DD;
			case 12:
				if (A_3 != -1)
				{
					num = 17;
					continue;
				}
				goto IL_317;
			case 13:
				if (A_2[0] != ' ')
				{
					num = 18;
					continue;
				}
				goto IL_1A1;
			case 14:
				goto IL_317;
			case 15:
				if (A_2.StartsWith(RecordTableEnumerator.b("䵃", a_)))
				{
					num = 16;
					continue;
				}
				goto IL_351;
			case 16:
				goto IL_1A1;
			case 17:
			{
				IFont a_2 = A_1[A_3];
				this.ᜀ(A_0, a_2, RecordTableEnumerator.b("㙃ᙅ㩇", a_));
				num = 14;
				continue;
			}
			case 18:
				num = 19;
				continue;
			case 19:
			{
				if (true)
				{
				}
				char c;
				if (c != ' ')
				{
					num = 2;
					continue;
				}
				goto IL_1A1;
			}
			case 20:
				num = 4;
				continue;
			case 21:
				goto IL_87;
			case 22:
				if (length > 0)
				{
					goto IL_341;
				}
				goto IL_351;
			case 23:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_341;
				default:
				{
					if (false)
					{
					}
					char c = A_2[length - 1];
					num = 13;
					continue;
				}
				}
				break;
			}
			if (A_0 == null)
			{
				num = 21;
				continue;
			}
			num = 0;
			continue;
			IL_1A1:
			A_0.WriteAttributeString(RecordTableEnumerator.b("㱃⭅⑇", a_), RecordTableEnumerator.b("㝃㙅⥇⥉⥋", a_), null, RecordTableEnumerator.b("㑃㑅ⵇ㥉⥋㱍♏㝑", a_));
			num = 11;
			continue;
			IL_317:
			A_0.WriteStartElement(RecordTableEnumerator.b("ぃ", a_));
			length = A_2.Length;
			num = 22;
			continue;
			IL_341:
			num = 23;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍃㑅ⅇ㹉⥋㱍", a_));
		IL_177:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅♇㹉㽋", a_));
		IL_1DD:
		goto IL_351;
		IL_22D:
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇᥉㡋㱍㥏㱑㍓", a_));
		IL_351:
		A_0.WriteValue(A_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06004530 RID: 17712 RVA: 0x0029DE60 File Offset: 0x0029CE60
	private void ᜂ(XmlWriter A_0)
	{
		int a_ = 1;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A5;
			case 1:
				if (!this.ᜁ())
				{
					num = 4;
					continue;
				}
				goto IL_A5;
			case 3:
				goto IL_40;
			case 4:
				IL_8F:
				A_0.WriteStartElement(RecordTableEnumerator.b("吶嘸场刼䴾㉀", a_));
				this.ᜁ(A_0);
				A_0.WriteEndElement();
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_A5:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8F;
			default:
				goto IL_BB;
			}
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_BB:
		if (false)
		{
		}
	}

	// Token: 0x06004531 RID: 17713 RVA: 0x0029DF30 File Offset: 0x0029CF30
	private void ᜁ(XmlWriter A_0)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_17:
				int num = 3;
				for (;;)
				{
					int num2;
					int num3;
					Color[] palette;
					switch (num)
					{
					case 0:
						goto IL_117;
					case 1:
						goto IL_F8;
					case 2:
						goto IL_F8;
					case 4:
						if (num2 >= num3)
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							Color color = palette[num2];
							this.ᜂ(A_0, RecordTableEnumerator.b("似堾⍀B⩄⭆♈㥊", a_), color);
							num2++;
							num = 1;
							continue;
						}
						}
						break;
					case 5:
						goto IL_4D;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("吼儾╀♂㵄≆ⵈࡊ≌⍎㹐⅒♔", a_));
					palette = this.ᡇ.Palette;
					num2 = 0;
					num3 = palette.Length;
					num = 2;
					continue;
					IL_F8:
					num = 4;
				}
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
			IL_117:
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004532 RID: 17714 RVA: 0x0029E068 File Offset: 0x0029D068
	private bool ᜁ()
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				List<Color> innerPalette = this.ᡇ.InnerPalette;
				Color[] array = XlsWorkbook.ᜨ;
				result = true;
				int num = 0;
				int count = innerPalette.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return result;
					case 1:
					{
						Color color;
						Color color2;
						if (color.ToArgb() != color2.ToArgb())
						{
							num2 = 5;
							continue;
						}
						num++;
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					case 2:
						goto IL_CB;
					case 3:
						goto IL_CB;
					case 4:
						if (num < count)
						{
							Color color = innerPalette[num];
							Color color2 = array[num];
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							num2 = 6;
							continue;
						}
						break;
					case 5:
						result = false;
						num2 = 0;
						continue;
					case 6:
						return result;
					}
					break;
					IL_CB:
					num2 = 4;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06004533 RID: 17715 RVA: 0x0029E17C File Offset: 0x0029D17C
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1, Dictionary<int, int> A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				bool flag;
				double defaultColumnWidth;
				int num2;
				spr\u216E spr_u216E;
				switch (num)
				{
				case 0:
					goto IL_89;
				case 1:
				{
					if (A_2 == null)
					{
						num = 9;
						continue;
					}
					spr\u216E[] array = A_1.ColumnInformation;
					flag = true;
					defaultColumnWidth = A_1.DefaultColumnWidth;
					num2 = 1;
					int maxColumnCount = this.ᡇ.MaxColumnCount;
					goto IL_245;
				}
				case 2:
					num = 17;
					continue;
				case 4:
					goto IL_84;
				case 5:
				{
					int maxColumnCount;
					if (num2 > maxColumnCount)
					{
						num = 2;
						continue;
					}
					spr\u216E[] array;
					spr_u216E = array[num2];
					num = 16;
					continue;
				}
				case 6:
					A_0.WriteEndElement();
					num = 11;
					continue;
				case 7:
					if (A_1 == null)
					{
						num = 18;
						continue;
					}
					num = 1;
					continue;
				case 8:
					if (flag)
					{
						num = 10;
						continue;
					}
					goto IL_89;
				case 9:
					goto IL_1AC;
				case 10:
					A_0.WriteStartElement(RecordTableEnumerator.b("崽⼿⹁㝃", a_));
					num = 0;
					continue;
				case 11:
					goto IL_163;
				case 12:
					num = 8;
					continue;
				case 13:
					goto IL_13F;
				case 14:
					goto IL_F6;
				case 15:
					goto IL_F6;
				case 16:
					if (spr_u216E != null)
					{
						num = 12;
						continue;
					}
					goto IL_13F;
				case 17:
					if (!flag)
					{
						num = 6;
						continue;
					}
					return;
				case 18:
					goto IL_13D;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 7;
				continue;
				IL_89:
				num2 = this.ᜀ(A_0, spr_u216E, A_2, defaultColumnWidth, A_1);
				flag = false;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_245:
					num = 14;
					continue;
				default:
					if (false)
					{
					}
					num = 13;
					continue;
				}
				IL_F6:
				num = 5;
				continue;
				IL_13F:
				num2++;
				num = 15;
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
			IL_13D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
			IL_163:
			return;
			IL_1AC:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("娽⤿⅁ᝃ㉅ㅇ♉⥋㵍", a_));
		}
		}
	}

	// Token: 0x06004534 RID: 17716 RVA: 0x0029E3E0 File Offset: 0x0029D3E0
	private int ᜀ(XmlWriter A_0, spr\u216E A_1, Dictionary<int, int> A_2, double A_3, XlsWorksheet A_4)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 10;
			int result;
			double num4;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
				{
					int num2;
					if (!A_2.TryGetValue(num2, out num3))
					{
						num = 8;
						continue;
					}
					goto IL_26A;
				}
				case 3:
					goto IL_17F;
				case 4:
					goto IL_2A2;
				case 5:
					goto IL_26A;
				case 6:
					if (A_2 == null)
					{
						num = 0;
						continue;
					}
					result = this.ᜀ(A_4, (int)(A_1.ᜈ() + 1));
					A_0.WriteStartElement(RecordTableEnumerator.b("夹医刽", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("圹唻倽", a_), ((int)(A_1.ᜈ() + 1)).ToString());
					A_0.WriteAttributeString(RecordTableEnumerator.b("圹崻䘽", a_), result.ToString());
					num4 = (double)A_4.ᜊ((int)A_1.ᜉ()) / 256.0;
					A_0.WriteAttributeString(RecordTableEnumerator.b("䴹唻娽㐿⩁", a_), num4.ToString(NumberFormatInfo.InvariantInfo));
					num = 3;
					continue;
				case 7:
					goto IL_2D6;
				case 8:
				{
					int num2;
					num3 = num2;
					num = 5;
					continue;
				}
				case 9:
					goto IL_1A4;
				case 11:
					goto IL_74;
				case 12:
				{
					int num2 = (int)A_1.ᜌ();
					num = 2;
					continue;
				}
				case 13:
					goto IL_201;
				case 14:
					if (A_0 != null)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17F;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				num = 14;
				continue;
				IL_17F:
				if ((int)A_1.ᜌ() != A_4.ParentWorkbook.DefaultXFIndex)
				{
					num = 12;
					continue;
				}
				IL_1A4:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("刹唻娽␿❁⩃", a_), A_1.ᜆ(), false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("堹夻䴽㐿сⵃ㉅", a_), A_1.ᜅ(), false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䨹吻儽⸿❁ぃ⽅⭇", a_), A_1.ᜁ(), false);
				num = 13;
				continue;
				IL_26A:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䤹䠻䜽ⰿ❁", a_), num3.ToString());
				num = 9;
			}
			IL_74:
			if (true)
			{
			}
			return int.MaxValue;
			IL_201:
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("夹䤻䴽㐿ⵁ⥃ᅅⅇ⹉㡋♍", a_), A_1.ᜎ() || A_3 != num4, false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("夹医刽ⰿ⍁㑃㕅ⵇ⹉", a_), A_1.ᜄ(), false);
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("唹䤻䨽ⰿ⭁⩃⍅ч⽉㩋⭍㱏", a_), (int)A_1.ᜊ(), 0);
			A_0.WriteEndElement();
			return result;
			IL_2A2:
			throw new ArgumentNullException(RecordTableEnumerator.b("帹唻崽ጿ㙁㵃⩅ⵇ㥉", a_));
			IL_2D6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
		}
		}
	}

	// Token: 0x06004535 RID: 17717 RVA: 0x0029E720 File Offset: 0x0029D720
	private int ᜀ(XlsWorksheet A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u216E[] array = A_0.ColumnInformation;
				spr\u216E spr_u216E = array[A_1];
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
					{
						spr\u216E spr_u216E2;
						if (spr_u216E2.ᜄ() == spr_u216E.ᜄ())
						{
							num = 8;
							continue;
						}
						return A_1;
					}
					case 2:
						goto IL_FC;
					case 3:
					{
						if (true)
						{
						}
						spr\u216E spr_u216E2;
						if (spr_u216E2.ᜊ() == spr_u216E.ᜊ())
						{
							num = 14;
							continue;
						}
						return A_1;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return A_1;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 5:
					{
						spr\u216E spr_u216E2;
						if (spr_u216E2.ᜌ() == spr_u216E.ᜌ())
						{
							num = 12;
							continue;
						}
						return A_1;
					}
					case 6:
					{
						spr\u216E spr_u216E2;
						if (spr_u216E2.ᜉ() == spr_u216E.ᜉ())
						{
							num = 4;
							continue;
						}
						return A_1;
					}
					case 7:
					{
						spr\u216E spr_u216E2;
						if (spr_u216E2 != null)
						{
							num = 9;
							continue;
						}
						return A_1;
					}
					case 8:
						num = 15;
						continue;
					case 9:
						num = 5;
						continue;
					case 10:
					{
						if (A_1 >= this.ᡇ.MaxColumnCount)
						{
							num = 13;
							continue;
						}
						int num2 = A_1 + 1;
						spr\u216E spr_u216E2 = array[num2];
						num = 7;
						continue;
					}
					case 11:
						goto IL_FC;
					case 12:
						num = 6;
						continue;
					case 13:
						return A_1;
					case 14:
						A_1++;
						num = 2;
						continue;
					case 15:
					{
						spr\u216E spr_u216E2;
						if (spr_u216E2.ᜆ() == spr_u216E.ᜆ())
						{
							num = 0;
							continue;
						}
						return A_1;
					}
					}
					break;
					IL_FC:
					num = 10;
				}
			}
			return A_1;
		}
	}

	// Token: 0x06004536 RID: 17718 RVA: 0x0029E90C File Offset: 0x0029D90C
	private void ᜀ(XmlWriter A_0, XlsDataValidationTable A_1)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				int num2;
				int count;
				if (num2 < count)
				{
					XlsDataValidationCollection a_2 = A_1[num2];
					this.ᜀ(A_0, a_2);
					num2++;
					num = 9;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			case 2:
				return;
			case 3:
				goto IL_72;
			case 4:
			{
				if (A_1.Count == 0)
				{
					num = 7;
					continue;
				}
				int num2 = 0;
				int count = A_1.Count;
				num = 6;
				continue;
			}
			case 5:
				goto IL_4C;
			case 6:
				goto IL_7F;
			case 7:
				goto IL_10A;
			case 8:
				num = 4;
				continue;
			case 9:
				goto IL_7F;
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 3;
			continue;
			IL_72:
			if (A_1 != null)
			{
				num = 8;
				continue;
			}
			return;
			IL_7F:
			num = 1;
		}
		IL_4C:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_10A:;
	}

	// Token: 0x06004537 RID: 17719 RVA: 0x0029EA34 File Offset: 0x0029DA34
	private void ᜀ(XmlWriter A_0, XlsDataValidationCollection A_1)
	{
		int a_ = 10;
		int num = 10;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_175;
			case 1:
				goto IL_192;
			case 2:
				if (A_1 != null)
				{
					num = 4;
					continue;
				}
				return;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_1DE;
				}
				break;
			case 4:
				num = 5;
				continue;
			case 5:
			{
				if (A_1.Count == 0)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("␿⍁ぃ❅ṇ⭉⁋❍㑏㍑⁓㽕㝗㑙⽛", a_));
				int count = A_1.Count;
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("⌿ⵁㅃ⡅㱇", a_), count, 0);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("␿⭁㝃❅⩇♉⥋ṍ≏㵑㥓♕ⱗ⥙", a_), A_1.IsPromptBoxVisible, false);
				num = 8;
				continue;
			}
			case 6:
				goto IL_60;
			case 7:
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㠿ᕁⵃ⡅ⱇ╉㭋", a_), A_1.PromptBoxVPosition, 0);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㤿ᕁⵃ⡅ⱇ╉㭋", a_), A_1.PromptBoxHPosition, 0);
				num = 6;
				continue;
			case 8:
				if (A_1.IsPromptBoxPositionFixed)
				{
					num = 7;
					continue;
				}
				goto IL_60;
			case 9:
				goto IL_5B;
			case 11:
				goto IL_175;
			case 12:
			{
				int count;
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				this.ᜀ(A_0, A_1[num2]);
				num2++;
				num = 11;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 2;
			continue;
			IL_60:
			num2 = 0;
			num = 0;
			continue;
			IL_175:
			num = 12;
		}
		IL_5B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_192:
		A_0.WriteEndElement();
		return;
		IL_1DE:
		if (false)
		{
		}
	}

	// Token: 0x06004538 RID: 17720 RVA: 0x0029EC30 File Offset: 0x0029DC30
	private void ᜀ(XmlWriter A_0, XlsValidation A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			string text;
			string text2;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_15E:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("嘶唸场刼䠾̀⽂⑄⥆≈", a_), A_1.IgnoreBlank, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼笾㍀ⱂ㕄͆♈㱊⍌", a_), A_1.IsSuppressDropDownArrow, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼瘾⽀㍂い㍆ш⹊㹌㱎ぐ㑒ご", a_), A_1.ShowInput, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䐶儸吺䨼稾㍀ㅂ⩄㕆ш⹊㹌㱎ぐ㑒ご", a_), A_1.ShowError, false);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("制䬸䤺刼䴾ᕀ⩂ㅄ⭆ⱈ", a_), A_1.ErrorTitle, string.Empty);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("制䬸䤺刼䴾", a_), A_1.ErrorMessage, string.Empty);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䜶䬸吺值伾㕀ᝂⱄ㍆╈⹊", a_), A_1.InputTitle, string.Empty);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䜶䬸吺值伾㕀", a_), A_1.InputMessage, string.Empty);
				string a_2 = string.Join(RecordTableEnumerator.b("᜶", a_), A_1.DVRanges);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䐶䠸䤺堼夾", a_), a_2, null);
				text = A_1.GetFirstSecondFormula(this.ᡈ, true);
				text2 = A_1.GetFirstSecondFormula(this.ᡈ, false);
				num = 6;
				break;
			}
			default:
				if (false)
				{
				}
				num = 8;
				break;
			}
			for (;;)
			{
				AlertStyleType alertStyle;
				ValidationComparisonOperator compareOperator;
				switch (num)
				{
				case 0:
					text = text.Replace('\0', ',');
					A_0.WriteElementString(RecordTableEnumerator.b("儶嘸䤺值䨾ⵀ≂瑄", a_), text);
					num = 22;
					continue;
				case 1:
					if (alertStyle != AlertStyleType.Stop)
					{
						num = 9;
						continue;
					}
					goto IL_429;
				case 2:
					if (text != string.Empty)
					{
						num = 0;
						continue;
					}
					goto IL_11A;
				case 3:
				{
					CellDataType allowType;
					if (allowType != CellDataType.Any)
					{
						num = 13;
						continue;
					}
					goto IL_402;
				}
				case 4:
					if (compareOperator != ValidationComparisonOperator.Between)
					{
						num = 19;
						continue;
					}
					goto IL_15E;
				case 5:
				{
					if (A_1 == null)
					{
						num = 21;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("匶堸伺尼椾⁀⽂ⱄ⍆⡈㽊⑌⁎㽐", a_));
					CellDataType allowType = A_1.AllowType;
					num = 3;
					continue;
				}
				case 6:
					if (text != null)
					{
						num = 16;
						continue;
					}
					goto IL_11A;
				case 7:
					goto IL_402;
				case 9:
					A_0.WriteAttributeString(RecordTableEnumerator.b("制䬸䤺刼䴾ቀ㝂㱄⭆ⱈ", a_), this.ᜀ(alertStyle));
					num = 10;
					continue;
				case 10:
					goto IL_429;
				case 11:
					if (true)
					{
					}
					if (text2 != string.Empty)
					{
						num = 14;
						continue;
					}
					goto IL_453;
				case 12:
					num = 11;
					continue;
				case 13:
				{
					CellDataType allowType;
					A_0.WriteAttributeString(RecordTableEnumerator.b("䌶䀸䬺堼", a_), this.ᜀ(allowType));
					num = 7;
					continue;
				}
				case 14:
					text2 = text2.Replace('\0', ',');
					A_0.WriteElementString(RecordTableEnumerator.b("儶嘸䤺值䨾ⵀ≂睄", a_), text2);
					num = 20;
					continue;
				case 15:
					if (text2 != null)
					{
						num = 12;
						continue;
					}
					goto IL_453;
				case 16:
					num = 2;
					continue;
				case 17:
					goto IL_B0;
				case 18:
					goto IL_104;
				case 19:
					A_0.WriteAttributeString(RecordTableEnumerator.b("堶䤸帺似帾㕀ⱂ㝄", a_), this.ᜀ(compareOperator));
					num = 18;
					continue;
				case 20:
					goto IL_2E6;
				case 21:
					goto IL_15C;
				case 22:
					goto IL_11A;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				num = 5;
				continue;
				IL_11A:
				num = 15;
				continue;
				IL_402:
				alertStyle = A_1.AlertStyle;
				num = 1;
				continue;
				IL_429:
				compareOperator = A_1.CompareOperator;
				num = 4;
			}
			IL_B0:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_104:
			goto IL_15E;
			IL_15C:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶堸伺尼椾⁀⽂ⱄ⍆⡈㽊⑌⁎㽐", a_));
			IL_2E6:
			IL_453:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004539 RID: 17721 RVA: 0x0029F098 File Offset: 0x0029E098
	private string ᜀ(CellDataType A_0)
	{
		int a_ = 0;
		for (;;)
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_CC;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CC;
					default:
						if (false)
						{
						}
						switch (A_0)
						{
						case CellDataType.Any:
							goto IL_A3;
						case CellDataType.Integer:
							goto IL_CE;
						case CellDataType.Decimal:
							goto IL_EC;
						case CellDataType.User:
							goto IL_DD;
						case CellDataType.Date:
							goto IL_FB;
						case CellDataType.Time:
							goto IL_B2;
						case CellDataType.TextLength:
							goto IL_85;
						case CellDataType.Formula:
							goto IL_94;
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
		}
		IL_85:
		return RecordTableEnumerator.b("䈵崷䈹䠻爽┿ⱁ⍃㉅⁇", a_);
		IL_94:
		return RecordTableEnumerator.b("唵䴷䤹䠻儽ⴿ", a_);
		IL_A3:
		return RecordTableEnumerator.b("堵圷吹夻", a_);
		IL_B2:
		return RecordTableEnumerator.b("䈵儷圹夻", a_);
		IL_CC:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("刵夷丹崻樽㤿㉁⅃", a_));
		IL_CE:
		return RecordTableEnumerator.b("䄵倷唹倻嬽", a_);
		IL_DD:
		return RecordTableEnumerator.b("娵儷䤹䠻", a_);
		IL_EC:
		return RecordTableEnumerator.b("刵崷夹唻匽ℿ⹁", a_);
		IL_FB:
		return RecordTableEnumerator.b("刵夷丹夻", a_);
	}

	// Token: 0x0600453A RID: 17722 RVA: 0x0029F1C4 File Offset: 0x0029E1C4
	private string ᜀ(AlertStyleType A_0)
	{
		int a_ = 17;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_69;
			}
			if (false)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_9A;
				case 2:
					switch (A_0)
					{
					case AlertStyleType.Stop:
						goto IL_69;
					case AlertStyleType.Warning:
						goto IL_9C;
					case AlertStyleType.Info:
						goto IL_78;
					default:
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_69:
		return RecordTableEnumerator.b("㑆㵈⑊㵌", a_);
		IL_78:
		return RecordTableEnumerator.b("⹆❈ⵊ≌㵎㱐㉒⅔㹖㙘㕚", a_);
		IL_9A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("≆㭈㥊≌㵎ɐ❒ⱔ㭖㱘", a_));
		IL_9C:
		return RecordTableEnumerator.b("う⡈㥊⍌♎㽐㑒", a_);
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x0029F290 File Offset: 0x0029E290
	private string ᜀ(ValidationComparisonOperator A_0)
	{
		int a_ = 19;
		for (;;)
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_CC;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CC;
					default:
						if (false)
						{
						}
						switch (A_0)
						{
						case ValidationComparisonOperator.Between:
							goto IL_A3;
						case ValidationComparisonOperator.NotBetween:
							goto IL_B2;
						case ValidationComparisonOperator.Equal:
							goto IL_FB;
						case ValidationComparisonOperator.NotEqual:
							goto IL_DD;
						case ValidationComparisonOperator.Greater:
							goto IL_EC;
						case ValidationComparisonOperator.Less:
							goto IL_CE;
						case ValidationComparisonOperator.GreaterOrEqual:
							goto IL_94;
						case ValidationComparisonOperator.LessOrEqual:
							goto IL_85;
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
		}
		IL_85:
		return RecordTableEnumerator.b("╈⹊㹌㱎Ր㭒㑔㥖ᙘ⥚ᡜ⹞ᑠɢ।", a_);
		IL_94:
		return RecordTableEnumerator.b("⹈㥊⡌⹎═㙒❔͖ㅘ㩚㍜ၞ፠♢ᑤቦࡨݪ", a_);
		IL_A3:
		return RecordTableEnumerator.b("⭈⹊㥌㡎㑐㙒㭔", a_);
		IL_B2:
		return RecordTableEnumerator.b("❈⑊㥌ൎ㑐❒≔㉖㱘㕚", a_);
		IL_CC:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩈⑊⁌㽎ぐ⅒ごᡖ⥘㹚⽜㹞ᕠౢᝤ", a_));
		IL_CE:
		return RecordTableEnumerator.b("╈⹊㹌㱎Ր㭒㑔㥖", a_);
		IL_DD:
		return RecordTableEnumerator.b("❈⑊㥌੎⁐♒㑔㭖", a_);
		IL_EC:
		return RecordTableEnumerator.b("⹈㥊⡌⹎═㙒❔͖ㅘ㩚㍜", a_);
		IL_FB:
		return RecordTableEnumerator.b("ⱈ㩊㡌⹎㵐", a_);
	}

	// Token: 0x0600453C RID: 17724 RVA: 0x0029F3BC File Offset: 0x0029E3BC
	public void ᜀ(XmlWriter A_0, IAutoFilters A_1)
	{
		int a_ = 0;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_191:
			num = 4;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_15E;
			case 1:
				goto IL_178;
			case 3:
				goto IL_15E;
			case 4:
				goto IL_19C;
			case 5:
				if (A_1 != null)
				{
					num = 11;
					continue;
				}
				return;
			case 6:
			{
				XlsAutoFilter xlsAutoFilter;
				this.ᜂ(A_0, xlsAutoFilter);
				num = 12;
				continue;
			}
			case 7:
			{
				int count;
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				XlsAutoFilter xlsAutoFilter = (XlsAutoFilter)A_1[num2];
				num = 8;
				continue;
			}
			case 8:
			{
				XlsAutoFilter xlsAutoFilter;
				if (xlsAutoFilter.IsFiltered)
				{
					num = 6;
					continue;
				}
				goto IL_FF;
			}
			case 9:
				goto IL_7C;
			case 10:
			{
				if (A_1.Count == 0)
				{
					goto IL_191;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("圵䴷丹医砽⤿⹁ぃ⍅㩇", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("䐵崷尹", a_), (A_1.Range as XlsRange).RangeAddressLocal);
				num2 = 0;
				int count = A_1.Count;
				num = 0;
				continue;
			}
			case 11:
				num = 10;
				continue;
			case 12:
				goto IL_FF;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 5;
			continue;
			IL_FF:
			num2++;
			num = 3;
			continue;
			IL_15E:
			num = 7;
		}
		IL_7C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_178:
		A_0.WriteEndElement();
		return;
		IL_19C:;
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x0029F570 File Offset: 0x0029E570
	private void ᜂ(XmlWriter A_0, XlsAutoFilter A_1)
	{
		int a_ = 5;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 9;
			for (;;)
			{
				string a_2;
				string a_3;
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 1:
					goto IL_154;
				case 2:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("崺吼匾㕀♂㝄ц♈❊㡌≎㽐", a_));
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("堺刼匾ࡀ❂", a_), A_1.Index - 1, -1);
					num = 13;
					continue;
				case 3:
					a_2 = A_1.SecondCondition.String;
					if (true)
					{
					}
					num = 1;
					continue;
				case 4:
					goto IL_243;
				case 5:
					this.ᜁ(A_0, A_1);
					num = 4;
					continue;
				case 6:
					goto IL_168;
				case 7:
					if (!A_1.IsSimple2)
					{
						num = 17;
						continue;
					}
					goto IL_12A;
				case 8:
					goto IL_128;
				case 10:
					goto IL_21A;
				case 11:
					if (A_1.HasFirstCondition)
					{
						num = 16;
						continue;
					}
					goto IL_1E1;
				case 12:
					if (A_1.HasSecondCondition)
					{
						num = 3;
						continue;
					}
					goto IL_154;
				case 13:
					if (A_1.IsTop10Items)
					{
						num = 5;
						continue;
					}
					num = 15;
					continue;
				case 14:
					goto IL_99;
				case 15:
					if (!A_1.IsSimple1)
					{
						num = 0;
						continue;
					}
					goto IL_12A;
				case 16:
					a_3 = A_1.FirstCondition.String;
					num = 18;
					continue;
				case 17:
					this.ᜀ(A_0, A_1);
					num = 10;
					continue;
				case 18:
					goto IL_1E1;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				num = 2;
				continue;
				IL_12A:
				a_3 = null;
				a_2 = null;
				num = 11;
				continue;
				IL_154:
				this.ᜀ(A_0, a_3, a_2);
				num = 6;
				continue;
				IL_1E1:
				num = 12;
			}
			IL_99:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_128:
			throw new ArgumentNullException(RecordTableEnumerator.b("娺䠼䬾⹀Ղⱄ⭆㵈⹊㽌", a_));
			IL_168:
			IL_243:
			break;
		}
		}
		IL_21A:
		A_0.WriteEndElement();
	}

	// Token: 0x0600453E RID: 17726 RVA: 0x0029F7C8 File Offset: 0x0029E7C8
	private void ᜀ(XmlWriter A_0, string A_1, string A_2)
	{
		int a_ = 10;
		for (;;)
		{
			IL_09:
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(A_0, A_1);
					num = 7;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						if (A_1 != null)
						{
							num = 0;
							continue;
						}
						goto IL_10C;
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					if (A_2 == null)
					{
						num = 9;
						continue;
					}
					goto IL_6E;
				case 4:
					if (A_2 != null)
					{
						num = 8;
						continue;
					}
					goto IL_146;
				case 6:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					goto IL_6E;
				case 7:
					goto IL_10C;
				case 8:
					this.ᜀ(A_0, A_2);
					num = 10;
					continue;
				case 9:
					return;
				case 10:
					goto IL_F6;
				case 11:
					goto IL_54;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 6;
				continue;
				IL_6E:
				if (true)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("☿⭁⡃㉅ⵇ㡉㽋", a_));
				num = 1;
				continue;
				IL_10C:
				num = 4;
			}
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_F6:
		IL_146:
		A_0.WriteEndElement();
	}

	// Token: 0x0600453F RID: 17727 RVA: 0x0029F924 File Offset: 0x0029E924
	private void ᜀ(XmlWriter A_0, string A_1)
	{
		int a_ = 19;
		if (A_0 != null)
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
				A_0.WriteStartElement(RecordTableEnumerator.b("⽈≊⅌㭎㑐⅒", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("㽈⩊⅌", a_), A_1);
				A_0.WriteEndElement();
				return;
			}
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
	}

	// Token: 0x06004540 RID: 17728 RVA: 0x0029F9B0 File Offset: 0x0029E9B0
	private void ᜁ(XmlWriter A_0, XlsAutoFilter A_1)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_33;
			default:
				goto IL_33;
			}
			IL_70:
			num = 2;
			continue;
			IL_33:
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_83;
			case 1:
				goto IL_5A;
			case 2:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			if (A_0 != null)
			{
				goto IL_70;
			}
			num = 1;
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
		IL_83:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("圵䴷丹医砽⤿⹁ぃ⍅㩇", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("䈵圷䨹഻฽", a_));
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䈵圷䨹", a_), A_1.ShowTopItem, true);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䘵崷䠹弻嬽⸿㙁", a_), A_1.IsTop10Percent, false);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䀵夷嘹", a_), A_1.Top10Items, -1);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("倵儷嘹䠻嬽㈿ᑁ╃⩅", a_), A_1.FirstCondition.Double, -1.0);
		A_0.WriteEndElement();
	}

	// Token: 0x06004541 RID: 17729 RVA: 0x0029FAF4 File Offset: 0x0029EAF4
	private void ᜀ(XmlWriter A_0, XlsAutoFilter A_1)
	{
		int a_ = 8;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(A_0, A_1.FirstCondition);
				num = 1;
				continue;
			case 1:
				goto IL_13A;
			case 2:
				goto IL_10A;
			case 4:
				if (A_1.HasSecondCondition)
				{
					num = 7;
					continue;
				}
				goto IL_16E;
			case 5:
				if (A_1.HasFirstCondition)
				{
					num = 0;
					continue;
				}
				goto IL_13A;
			case 6:
				goto IL_57;
			case 7:
				goto IL_158;
			case 8:
				goto IL_124;
			case 9:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_158;
				}
				if (false)
				{
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("崽㔿ㅁぃ⥅╇౉╋≍⑏㝑♓╕", a_));
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("弽⸿♁", a_), A_1.IsAnd, false);
				num = 5;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 9;
			continue;
			IL_13A:
			num = 4;
			continue;
			IL_158:
			this.ᜀ(A_0, A_1.SecondCondition);
			num = 8;
		}
		IL_57:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_10A:
		throw new ArgumentNullException(RecordTableEnumerator.b("弽㔿㙁⭃Eⅇ♉㡋⭍≏", a_));
		IL_124:
		IL_16E:
		A_0.WriteEndElement();
	}

	// Token: 0x06004542 RID: 17730 RVA: 0x0029FC78 File Offset: 0x0029EC78
	private void ᜀ(XmlWriter A_0, IAutoFilterCondition A_1)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_33;
			default:
				goto IL_33;
			}
			IL_70:
			if (true)
			{
			}
			num = 0;
			continue;
			IL_33:
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				goto IL_A1;
			case 1:
				goto IL_8B;
			case 3:
				goto IL_5A;
			}
			if (A_0 != null)
			{
				goto IL_70;
			}
			num = 3;
		}
		IL_5A:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("帾㑀㝂⩄ņ⁈❊㥌⩎⍐ၒ㩔㥖㵘㉚⥜㙞๠ൢ", a_));
		IL_A1:
		A_0.WriteStartElement(RecordTableEnumerator.b("尾㑀あㅄ⡆⑈ൊ⑌⍎═㙒❔", a_));
		string a_2 = this.ᜀ(A_1.ConditionOperator);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("倾ㅀ♂㝄♆㵈⑊㽌", a_), a_2, RecordTableEnumerator.b("娾぀㙂⑄⭆", a_));
		string a_3 = this.ᜀ(A_1);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䤾⁀⽂", a_), a_3, string.Empty);
		A_0.WriteEndElement();
	}

	// Token: 0x06004543 RID: 17731 RVA: 0x0029FD94 File Offset: 0x0029ED94
	private string ᜀ(FilterConditionType A_0)
	{
		int a_ = 3;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (A_0)
					{
					case FilterConditionType.Less:
						goto IL_51;
					case FilterConditionType.Equal:
						goto IL_6F;
					case FilterConditionType.LessOrEqual:
						goto IL_C8;
					case FilterConditionType.Greater:
						goto IL_D7;
					case FilterConditionType.NotEqual:
						goto IL_7E;
					case FilterConditionType.GreaterOrEqual:
						goto IL_60;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_BE;
				}
				break;
			}
		}
		IL_51:
		return RecordTableEnumerator.b("唸帺丼䰾ᕀ⭂⑄⥆", a_);
		IL_60:
		return RecordTableEnumerator.b("常䤺堼帾㕀♂㝄ፆⅈ⩊⍌", a_);
		IL_6F:
		return RecordTableEnumerator.b("尸䨺䠼帾ⵀ", a_);
		IL_7E:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_E6:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("弸刺儼䬾⑀ㅂل⡆❈⽊⑌㭎㡐㱒㭔", a_));
		default:
			if (false)
			{
			}
			return RecordTableEnumerator.b("圸吺䤼稾぀㙂⑄⭆", a_);
		}
		IL_BE:
		if (true)
		{
		}
		goto IL_E6;
		IL_C8:
		return RecordTableEnumerator.b("唸帺丼䰾ᕀ⭂⑄⥆و㥊ࡌ㹎⑐㉒㥔", a_);
		IL_D7:
		return RecordTableEnumerator.b("常䤺堼帾㕀♂㝄ፆⅈ⩊⍌", a_);
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x0029FE9C File Offset: 0x0029EE9C
	private ushort ᜀ(spr\u2408 A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					A_0.ᜀ(1);
					num = 5;
					continue;
				case 2:
					if (A_0.ᜄ() == 0)
					{
						num = 6;
						continue;
					}
					goto IL_73;
				case 4:
					A_0.ᜀ(2);
					num = 8;
					continue;
				case 5:
					goto IL_EB;
				case 6:
					A_0.ᜀ(3);
					num = 10;
					continue;
				case 7:
					if (A_0.ᜃ() != 0)
					{
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 8:
					goto IL_6E;
				case 9:
					if (A_0.ᜄ() == 0)
					{
						num = 1;
						continue;
					}
					goto IL_144;
				case 10:
					goto IL_142;
				case 11:
					if (A_0.ᜃ() == 0)
					{
						num = 0;
						continue;
					}
					goto IL_73;
				case 12:
					if (true)
					{
					}
					num = 11;
					continue;
				}
				if (A_0 != null)
				{
					num = 12;
					continue;
				}
				goto IL_144;
				IL_73:
				num = 7;
			}
		}
		IL_6E:
		IL_EB:
		IL_142:
		IL_144:
		return A_0.ᜆ();
	}

	// Token: 0x06004545 RID: 17733 RVA: 0x0029FFF4 File Offset: 0x0029EFF4
	private string ᜀ(IAutoFilterCondition A_0)
	{
		int a_ = 14;
		for (;;)
		{
			FilterDataType dataType = A_0.DataType;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 1:
					switch (dataType)
					{
					case FilterDataType.FloatingPoint:
						goto IL_E7;
					case FilterDataType.String:
						goto IL_AB;
					case FilterDataType.Boolean:
						num = 2;
						continue;
					case FilterDataType.ErrorCode:
						goto IL_B2;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					break;
				case 2:
					if (!A_0.Boolean)
					{
						goto IL_9E;
					}
					goto IL_D0;
				case 3:
					num = 4;
					continue;
				case 4:
					goto IL_CE;
				}
				break;
				IL_9E:
				num = 0;
			}
		}
		IL_A9:
		return RecordTableEnumerator.b("瑃", a_);
		IL_AB:
		return A_0.String;
		IL_B2:
		return FormulaUtil.ErrorCodeToName[(int)A_0.ErrorCode];
		IL_CE:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁃❅㱇⭉ᡋ㝍⁏㝑", a_));
		IL_D0:
		if (true)
		{
		}
		return RecordTableEnumerator.b("畃", a_);
		IL_E7:
		return A_0.Double.ToString();
	}

	// Token: 0x06004546 RID: 17734 RVA: 0x002A010C File Offset: 0x0029F10C
	private void ᜀ(XmlWriter A_0, XmlWriter A_1, XlsConditionalFormats A_2, ref int A_3, ref int A_4)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_177;
				case 3:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B5;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 4:
					goto IL_1B3;
				case 5:
					goto IL_68;
				case 6:
					goto IL_115;
				case 7:
				{
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					int count = A_2.Count;
					num = 9;
					continue;
				}
				case 8:
					goto IL_15B;
				case 9:
				{
					int count;
					if (count == 0)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("⅁⭃⡅ⱇ⍉㡋❍㽏㱑㕓㩕ṗ㕙⹛㍝şᙡၣཥ٧൩", a_));
					string a_2 = string.Join(RecordTableEnumerator.b("扁", a_), A_2.CellsList);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("ㅁ㕃㑅ⵇⱉ", a_), a_2, null);
					int num2 = 0;
					num = 10;
					continue;
				}
				case 10:
					goto IL_15B;
				case 11:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					sprᲖ a_3 = A_2[num2] as sprᲖ;
					this.ᜀ(A_0, A_1, a_3, ref A_3, ref A_4);
					num2++;
					num = 8;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
				IL_15B:
				num = 11;
			}
			IL_68:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_115:
			goto IL_1B5;
			IL_177:
			A_0.WriteEndElement();
			return;
			IL_1B3:
			throw new ArgumentNullException(RecordTableEnumerator.b("⅁⭃⡅ⱇ⍉㡋❍㽏㱑㕓㩕ṗ㕙⹛㍝şᙡᝣ", a_));
			IL_1B5:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋੍⡏㑑", a_));
		}
		}
	}

	// Token: 0x06004547 RID: 17735 RVA: 0x002A02FC File Offset: 0x0029F2FC
	private void ᜀ(XmlWriter A_0, XmlWriter A_1, sprᲖ A_2, ref int A_3, ref int A_4)
	{
		int a_ = 16;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 31;
			for (;;)
			{
				ConditionalFormatType formatType;
				XlsConditionalFormat xlsConditionalFormat;
				ConditionalFormatType conditionalFormatType;
				switch (num)
				{
				case 0:
					num = 23;
					continue;
				case 1:
					num = 18;
					continue;
				case 2:
					if (A_2.SecondFormula != string.Empty)
					{
						goto IL_EC;
					}
					goto IL_372;
				case 3:
					goto IL_1B5;
				case 4:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
				case 5:
					goto IL_49D;
				case 6:
					goto IL_26E;
				case 7:
					goto IL_291;
				case 8:
					if (A_2 == null)
					{
						num = 11;
						continue;
					}
					formatType = A_2.FormatType;
					A_0.WriteStartElement(RecordTableEnumerator.b("╅⹇ᡉ㥋≍㕏", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("㉅ㅇ㩉⥋", a_), this.ᜀ(A_2.FormatType));
					num = 20;
					continue;
				case 9:
					if (formatType == ConditionalFormatType.CellValue)
					{
						num = 12;
						continue;
					}
					goto IL_3F9;
				case 10:
					goto IL_3D8;
				case 11:
					goto IL_304;
				case 12:
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("⥅㡇⽉㹋⽍⑏㵑♓", a_), this.ᜀ(A_2.Operator), string.Empty);
					num = 28;
					continue;
				case 13:
					if (A_2.FirstFormula != null)
					{
						num = 1;
						continue;
					}
					goto IL_165;
				case 14:
				{
					string value = xlsConditionalFormat.ᜀ(this.ᡈ, false);
					A_0.WriteElementString(RecordTableEnumerator.b("⁅❇㡉⅋㭍㱏㍑", a_), value);
					num = 16;
					continue;
				}
				case 15:
					num = 25;
					continue;
				case 16:
					goto IL_372;
				case 17:
					goto IL_C6;
				case 18:
					if (A_2.FirstFormula != string.Empty)
					{
						num = 29;
						continue;
					}
					goto IL_165;
				case 19:
					num = 2;
					continue;
				case 20:
					if (!A_2.IsFontFormatPresent)
					{
						num = 24;
						continue;
					}
					goto IL_296;
				case 21:
					if (!A_2.IsBorderFormatPresent)
					{
						num = 0;
						continue;
					}
					goto IL_296;
				case 22:
					goto IL_296;
				case 23:
					if (!A_2.IsPatternFormatPresent)
					{
						goto IL_3D8;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EC;
					default:
						if (false)
						{
						}
						num = 22;
						continue;
					}
					break;
				case 24:
					num = 21;
					continue;
				case 25:
					goto IL_14C;
				case 26:
					switch (conditionalFormatType)
					{
					case ConditionalFormatType.DataBar:
						this.ᜀ(A_0, A_2.DataBar.Wrapped);
						num = 6;
						continue;
					case ConditionalFormatType.IconSet:
						this.ᜀ(A_0, A_2.IconSet.Wrapped);
						num = 3;
						continue;
					case ConditionalFormatType.ColorScale:
						this.ᜀ(A_0, A_2.ColorScale.Wrapped);
						num = 7;
						continue;
					default:
						num = 15;
						continue;
					}
					break;
				case 27:
					goto IL_165;
				case 28:
					goto IL_3F9;
				case 29:
				{
					string value2 = xlsConditionalFormat.ᜀ(this.ᡈ, true);
					A_0.WriteElementString(RecordTableEnumerator.b("⁅❇㡉⅋㭍㱏㍑", a_), value2);
					num = 27;
					continue;
				}
				case 30:
					if (A_2.SecondFormula != null)
					{
						num = 19;
						continue;
					}
					goto IL_372;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				num = 4;
				continue;
				IL_EC:
				num = 14;
				continue;
				IL_165:
				num = 30;
				continue;
				IL_296:
				this.ᜃ(A_1, A_2);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("≅ぇⱉՋ⩍", a_), A_3, int.MinValue);
				A_3++;
				num = 10;
				continue;
				IL_372:
				conditionalFormatType = formatType;
				num = 26;
				continue;
				IL_3D8:
				num = 9;
				continue;
				IL_3F9:
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㙅㩇⍉⍋㱍㥏♑ⵓ", a_), A_4, int.MinValue);
				A_4++;
				xlsConditionalFormat = (XlsConditionalFormat)A_2;
				num = 13;
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_14C:
			IL_1B5:
			IL_26E:
			IL_291:
			goto IL_4A2;
			IL_304:
			throw new ArgumentNullException(RecordTableEnumerator.b("╅❇⑉⡋❍⑏㭑㭓㡕", a_));
			IL_49D:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏ᙑⱓさ", a_));
			IL_4A2:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004548 RID: 17736 RVA: 0x002A07B4 File Offset: 0x0029F7B4
	private void ᜀ(XmlWriter A_0, IColorScale A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					IList<IColorConditionValue> criteria;
					this.ᜀ(A_0, criteria[num2]);
					num2++;
					num = 5;
					continue;
				}
				case 1:
				{
					int num3 = 0;
					IList<IColorConditionValue> criteria;
					int count2 = criteria.Count;
					num = 3;
					continue;
				}
				case 2:
				{
					int num3;
					int count2;
					if (num3 >= count2)
					{
						num = 4;
						continue;
					}
					IList<IColorConditionValue> criteria;
					this.ᜂ(A_0, RecordTableEnumerator.b("弻儽ⰿⵁ㙃", a_), criteria[num3].FormatColor);
					num3++;
					num = 9;
					continue;
				}
				case 3:
					goto IL_163;
				case 4:
					goto IL_183;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E6;
					default:
						if (false)
						{
						}
						goto IL_137;
					}
					break;
				case 6:
					goto IL_E6;
				case 8:
					goto IL_137;
				case 9:
					goto IL_163;
				case 10:
					goto IL_65;
				case 11:
				{
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("弻儽ⰿⵁ㙃ᕅ⭇⭉⁋⭍", a_));
					IList<IColorConditionValue> criteria = A_1.Criteria;
					int num2 = 0;
					int count = criteria.Count;
					num = 8;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 11;
				continue;
				IL_137:
				if (true)
				{
				}
				num = 0;
				continue;
				IL_163:
				num = 2;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_E6:
			throw new ArgumentNullException(RecordTableEnumerator.b("弻儽ⰿⵁ㙃ᕅ⭇⭉⁋⭍", a_));
			IL_183:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004549 RID: 17737 RVA: 0x002A0994 File Offset: 0x0029F994
	private void ᜀ(XmlWriter A_0, IIconSet A_1)
	{
		int a_ = 17;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_94;
			case 1:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				IList<IConditionValue> iconCriteria;
				this.ᜀ(A_0, iconCriteria[num2]);
				num2++;
				goto IL_58;
			}
			case 2:
				goto IL_94;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
					goto IL_194;
				}
				break;
			case 4:
				goto IL_D4;
			case 5:
				goto IL_44;
			case 7:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⹆⩈⑊⍌ᱎ㑐❒", a_));
				A_0.WriteAttributeString(RecordTableEnumerator.b("⹆⩈⑊⍌ᱎ㑐❒", a_), spr\u21EF.ᜥ[(int)A_1.IconSet]);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㝆ⱈ㥊⹌⩎㽐❒", a_), A_1.PercentileValues, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㕆ⱈ㵊⡌㵎≐㙒", a_), A_1.IsReverseOrder, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㑆ⅈ⑊㩌᥎ぐ㽒⁔㉖", a_), !A_1.ShowIconOnly, true);
				IList<IConditionValue> iconCriteria = A_1.IconCriteria;
				int num2 = 0;
				int count = iconCriteria.Count;
				num = 2;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			if (true)
			{
			}
			num = 7;
			continue;
			IL_58:
			num = 0;
			continue;
			IL_94:
			num = 1;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
		IL_D4:
		throw new ArgumentNullException(RecordTableEnumerator.b("⹆⩈⑊⍌ᱎ㑐❒", a_));
		IL_194:
		if (false)
		{
		}
		A_0.WriteEndElement();
	}

	// Token: 0x0600454A RID: 17738 RVA: 0x002A0B44 File Offset: 0x0029FB44
	private void ᜀ(XmlWriter A_0, IDataBar A_1)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 2:
				goto IL_46;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_85;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬾⁀㝂⑄Ն⡈㥊", a_));
		IL_85:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_6F;
		default:
			if (false)
			{
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("嬾⁀㝂⑄Ն⡈㥊", a_));
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("刾⡀ⵂॄ≆❈ⱊ㥌❎", a_), A_1.PercentMin, 10);
			spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("刾⁀㭂ॄ≆❈ⱊ㥌❎", a_), A_1.PercentMax, 90);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䰾⥀ⱂ㉄ᅆ⡈❊㡌⩎", a_), A_1.ShowValue, true);
			this.ᜀ(A_0, A_1.MinPoint);
			this.ᜀ(A_0, A_1.MaxPoint);
			this.ᜂ(A_0, RecordTableEnumerator.b("尾⹀⽂⩄㕆", a_), A_1.BarColor);
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x0600454B RID: 17739 RVA: 0x002A0C9C File Offset: 0x0029FC9C
	private void ᜀ(XmlWriter A_0, IConditionValue A_1)
	{
		int a_ = 12;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			}
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("⅁≃ぅ❇", a_));
		int type = (int)A_1.Type;
		string value = spr\u21EF.ᜤ[type];
		A_0.WriteAttributeString(RecordTableEnumerator.b("㙁㵃㙅ⵇ", a_), value);
		A_0.WriteAttributeString(RecordTableEnumerator.b("㑁╃⩅", a_), A_1.Value);
		A_0.WriteEndElement();
	}

	// Token: 0x0600454C RID: 17740 RVA: 0x002A0D54 File Offset: 0x0029FD54
	private string ᜀ(ComparisonOperatorType A_0)
	{
		int a_ = 1;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A2;
				case 1:
					switch (A_0)
					{
					case ComparisonOperatorType.None:
						goto IL_5B;
					case ComparisonOperatorType.Between:
						goto IL_79;
					case ComparisonOperatorType.NotBetween:
						goto IL_88;
					case ComparisonOperatorType.Equal:
						goto IL_F5;
					case ComparisonOperatorType.NotEqual:
						goto IL_B3;
					case ComparisonOperatorType.Greater:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_E0;
						}
						break;
					case ComparisonOperatorType.Less:
						goto IL_6A;
					case ComparisonOperatorType.GreaterOrEqual:
						goto IL_104;
					case ComparisonOperatorType.LessOrEqual:
						goto IL_A4;
					}
					num = 2;
					continue;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_5B:
		return RecordTableEnumerator.b("夶嘸伺縼倾⽀㝂⑄⹆❈㡊", a_);
		IL_6A:
		return RecordTableEnumerator.b("嬶尸䠺丼款⥀≂⭄", a_);
		IL_79:
		return RecordTableEnumerator.b("唶尸伺䨼娾⑀ⵂ", a_);
		IL_88:
		return RecordTableEnumerator.b("夶嘸伺缼娾㕀㑂⁄≆❈", a_);
		IL_A2:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儶倸场䤼娾㍀B⩄⥆ⵈ≊㥌♎㹐㵒", a_));
		IL_A4:
		return RecordTableEnumerator.b("嬶尸䠺丼款⥀≂⭄ࡆ㭈๊㱌㩎ぐ㽒", a_);
		IL_B3:
		return RecordTableEnumerator.b("夶嘸伺砼举㑀≂⥄", a_);
		IL_E0:
		if (false)
		{
		}
		return RecordTableEnumerator.b("倶䬸帺尼䬾⑀ㅂᅄ⽆⡈╊", a_);
		IL_F5:
		return RecordTableEnumerator.b("制䠸为尼匾", a_);
		IL_104:
		return RecordTableEnumerator.b("倶䬸帺尼䬾⑀ㅂᅄ⽆⡈╊Ɍ㵎ᑐ≒⁔㙖㕘", a_);
	}

	// Token: 0x0600454D RID: 17741 RVA: 0x002A0E94 File Offset: 0x0029FE94
	private string ᜀ(ConditionalFormatType A_0)
	{
		int a_ = 15;
		for (;;)
		{
			IL_39:
			int num = 0;
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
						switch (A_0)
						{
						case ConditionalFormatType.CellValue:
							goto IL_B5;
						case ConditionalFormatType.Formula:
							goto IL_78;
						case ConditionalFormatType.DataBar:
							goto IL_C4;
						case ConditionalFormatType.IconSet:
							goto IL_69;
						case ConditionalFormatType.ColorScale:
							goto IL_9C;
						}
						goto IL_5F;
					case 1:
						goto IL_92;
					case 2:
						num = 1;
						continue;
					}
					goto IL_39;
				}
				IL_5F:
				num = 2;
			}
		}
		IL_69:
		return RecordTableEnumerator.b("ⱄ⑆♈╊Ṍ⩎═", a_);
		IL_78:
		return RecordTableEnumerator.b("⁄㽆㥈㥊⡌㱎≐㩒㩔㥖", a_);
		IL_92:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅄ㹆㥈⹊์ॎ", a_));
		IL_9C:
		return RecordTableEnumerator.b("♄⡆╈⑊㽌ᱎ㉐㉒㥔㉖", a_);
		IL_B5:
		return RecordTableEnumerator.b("♄≆╈❊ь㱎", a_);
		IL_C4:
		return RecordTableEnumerator.b("⅄♆㵈⩊ཌ⹎⍐", a_);
	}

	// Token: 0x0600454E RID: 17742 RVA: 0x002A0F88 File Offset: 0x0029FF88
	public Stream ᜀ(ref Stream A_0, XlsWorksheetConditionalFormats A_1, ref int A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 3;
			XmlWriter xmlWriter;
			XmlWriter xmlWriter2;
			MemoryStream memoryStream;
			Stream stream;
			for (;;)
			{
				int num2;
				int count;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					XlsConditionalFormats a_2 = A_1[num2];
					this.ᜀ(xmlWriter, xmlWriter2, a_2, ref A_2, ref num3);
					num2++;
					if (true)
					{
					}
					num = 9;
					continue;
				}
				case 1:
					A_2 = num4;
					num3 = num4 + 1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B6;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 2:
					goto IL_F4;
				case 4:
					goto IL_61;
				case 5:
					goto IL_66;
				case 6:
					goto IL_C8;
				case 7:
					if (num4 != -2147483648)
					{
						num = 1;
						continue;
					}
					goto IL_66;
				case 8:
					goto IL_B6;
				case 9:
					goto IL_CD;
				case 10:
					goto IL_CD;
				}
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				count = A_1.Count;
				num = 8;
				continue;
				IL_66:
				num2 = 0;
				num = 10;
				continue;
				IL_B6:
				if (count == 0)
				{
					num = 6;
					continue;
				}
				num4 = this.ᡇ.DataHolder.ᜏ();
				memoryStream = new MemoryStream();
				StreamWriter a_3 = new StreamWriter(memoryStream);
				xmlWriter2 = UtilityMethods.ᜀ(a_3);
				stream = new MemoryStream();
				StreamWriter a_4 = new StreamWriter(stream);
				xmlWriter = UtilityMethods.ᜀ(a_4);
				xmlWriter2.WriteStartElement(RecordTableEnumerator.b("㭈⑊≌㭎", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﺌﾎ爵햠캢즤袦鮨鮪鶬馮麰\udeb2풴\udeb6ힸ", a_));
				xmlWriter2.WriteStartElement(RecordTableEnumerator.b("ⵈ㍊⭌㱎", a_));
				spr\u1B7A.ᜀ(xmlWriter2, A_0, RecordTableEnumerator.b("ⵈ㍊⭌㱎", a_));
				xmlWriter.WriteStartElement(RecordTableEnumerator.b("㭈⑊≌㭎", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊﺌﾎ爵햠캢즤袦鮨鮪鶬馮麰\udeb2풴\udeb6ힸ", a_));
				num3 = 1;
				num = 7;
				continue;
				IL_CD:
				num = 0;
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩈⑊⍌⭎㡐❒㱔㡖㝘㩚ㅜᥞ๠ᅢࡤ٦ᵨᡪ", a_));
			IL_C8:
			return null;
			IL_F4:
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
			xmlWriter2.WriteEndElement();
			xmlWriter2.WriteEndElement();
			xmlWriter2.Flush();
			A_0 = memoryStream;
			return stream;
		}
		}
	}

	// Token: 0x0600454F RID: 17743 RVA: 0x002A11E0 File Offset: 0x002A01E0
	private void ᜃ(XmlWriter A_0, sprᲖ A_1)
	{
		int a_ = 8;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				goto IL_85;
			case 1:
				goto IL_46;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				goto IL_6F;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("崽⼿ⱁ⁃⽅㱇⍉⍋⁍", a_));
		IL_85:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_6F;
		default:
			if (false)
			{
			}
			A_0.WriteStartElement(RecordTableEnumerator.b("娽㠿⑁", a_));
			this.ᜀ(A_0, A_1);
			this.ᜁ(A_0, A_1);
			this.ᜂ(A_0, A_1);
			A_0.WriteEndElement();
			return;
		}
	}

	// Token: 0x06004550 RID: 17744 RVA: 0x002A12C0 File Offset: 0x002A02C0
	private void ᜂ(XmlWriter A_0, sprᲖ A_1)
	{
		int a_ = 1;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9F;
			case 1:
				return;
			case 2:
				if (true)
				{
				}
				if (!A_1.IsBorderFormatPresent)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_B7;
				}
				break;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				num = 2;
				continue;
			case 5:
				goto IL_3C;
			}
			IL_31:
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 3;
			continue;
			goto IL_31;
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
		IL_9F:
		throw new ArgumentNullException(RecordTableEnumerator.b("吶嘸唺夼嘾㕀⩂⩄⥆", a_));
		IL_B7:
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("唶嘸䤺夼娾㍀", a_));
		this.ᜀ(A_0, BorderIndex2007.left, A_1.LeftBorderStyle, A_1.ᜈ());
		this.ᜀ(A_0, BorderIndex2007.right, A_1.RightBorderStyle, A_1.ᜉ());
		this.ᜀ(A_0, BorderIndex2007.top, A_1.TopBorderStyle, A_1.ᜆ());
		this.ᜀ(A_0, BorderIndex2007.bottom, A_1.BottomBorderStyle, A_1.ᜇ());
		A_0.WriteEndElement();
	}

	// Token: 0x06004551 RID: 17745 RVA: 0x002A1400 File Offset: 0x002A0400
	private void ᜀ(XmlWriter A_0, BorderIndex2007 A_1, LineStyleType A_2, OColor A_3)
	{
		int a_ = 0;
		if (true)
		{
		}
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				if (A_2 != LineStyleType.None)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				return;
			case 3:
			{
				A_0.WriteStartElement(A_1.ToString());
				string text = A_2.ToString();
				text = spr\u1B7A.ᜄ(text);
				A_0.WriteAttributeString(RecordTableEnumerator.b("䔵䰷䌹倻嬽", a_), text);
				this.ᜀ(A_0, RecordTableEnumerator.b("唵圷嘹医䰽", a_), A_3);
				A_0.WriteEndElement();
				num = 2;
				continue;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5C;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
	}

	// Token: 0x06004552 RID: 17746 RVA: 0x002A1510 File Offset: 0x002A0510
	private void ᜁ(XmlWriter A_0, sprᲖ A_1)
	{
		int a_ = 5;
		int num = 13;
		OColor a_2;
		for (;;)
		{
			OColor ocolor;
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (ocolor.Value != 65)
				{
					num = 11;
					continue;
				}
				goto IL_1ED;
			case 2:
				goto IL_112;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_64;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 4:
				A_0.WriteAttributeString(RecordTableEnumerator.b("䬺尼䬾㕀♂㝄⥆ᵈ㉊㵌⩎", a_), this.ᜀ(A_1.FillPattern));
				num = 6;
				continue;
			case 5:
				if (A_1.FillPattern != ExcelPatternType.None)
				{
					num = 4;
					continue;
				}
				goto IL_64;
			case 6:
				goto IL_64;
			case 7:
				if (true)
				{
				}
				if (!A_1.IsPatternFormatPresent)
				{
					num = 0;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("崺吼匾ⵀ", a_));
				A_0.WriteStartElement(RecordTableEnumerator.b("䬺尼䬾㕀♂㝄⥆཈≊⅌⍎", a_));
				num = 5;
				continue;
			case 8:
				goto IL_1A1;
			case 9:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num = 7;
				continue;
			case 10:
				if (ocolor.ColorType == ColorType.Known)
				{
					num = 3;
					continue;
				}
				goto IL_180;
			case 11:
				goto IL_180;
			case 12:
				goto IL_5F;
			}
			if (A_0 == null)
			{
				num = 12;
				continue;
			}
			num = 9;
			continue;
			IL_64:
			ocolor = A_1.ᜄ();
			a_2 = A_1.ᜅ();
			num = 10;
			continue;
			IL_180:
			this.ᜀ(A_0, RecordTableEnumerator.b("崺娼簾⹀⽂⩄㕆", a_), ocolor);
			num = 8;
		}
		IL_5F:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_112:
		throw new ArgumentNullException(RecordTableEnumerator.b("堺刼儾╀⩂ㅄ⹆♈╊", a_));
		IL_1A1:
		IL_1ED:
		this.ᜀ(A_0, RecordTableEnumerator.b("夺娼簾⹀⽂⩄㕆", a_), a_2);
		A_0.WriteEndElement();
		A_0.WriteEndElement();
	}

	// Token: 0x06004553 RID: 17747 RVA: 0x002A172C File Offset: 0x002A072C
	private void ᜀ(XmlWriter A_0, sprᲖ A_1)
	{
		int a_ = 13;
		int num = 5;
		for (;;)
		{
			FontUnderlineType fontUnderlineType;
			switch (num)
			{
			case 0:
				if (A_1.IsBold)
				{
					num = 2;
					continue;
				}
				goto IL_18F;
			case 1:
				A_0.WriteStartElement(RecordTableEnumerator.b("㙂", a_));
				num = 21;
				continue;
			case 2:
				A_0.WriteElementString(RecordTableEnumerator.b("⅂", a_), string.Empty);
				num = 9;
				continue;
			case 3:
				if (fontUnderlineType != FontUnderlineType.None)
				{
					num = 1;
					continue;
				}
				goto IL_1B6;
			case 4:
				goto IL_87;
			case 6:
				A_0.WriteElementString(RecordTableEnumerator.b("あㅄ㕆⁈⁊⡌", a_), string.Empty);
				num = 15;
				continue;
			case 7:
				if (!A_1.IsFontFormatPresent)
				{
					num = 10;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("╂⩄⥆㵈", a_));
				num = 0;
				continue;
			case 8:
				this.ᜀ(A_0, RecordTableEnumerator.b("⁂⩄⭆♈㥊", a_), A_1.ᜊ());
				num = 13;
				continue;
			case 9:
				goto IL_18F;
			case 10:
				return;
			case 11:
				goto IL_130;
			case 12:
				fontUnderlineType = FontUnderlineType.Single;
				num = 11;
				continue;
			case 13:
				goto IL_207;
			case 14:
				goto IL_1B6;
			case 15:
				goto IL_16B;
			case 16:
				if (A_1 == null)
				{
					num = 19;
					continue;
				}
				num = 7;
				continue;
			case 17:
				goto IL_9E;
			case 18:
				if (A_1.IsItalic)
				{
					num = 20;
					continue;
				}
				goto IL_9E;
			case 19:
				goto IL_12B;
			case 20:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_87;
				default:
					if (false)
					{
					}
					A_0.WriteElementString(RecordTableEnumerator.b("⩂", a_), string.Empty);
					num = 17;
					continue;
				}
				break;
			case 21:
				if (!Enum.IsDefined(typeof(FontUnderlineType), fontUnderlineType))
				{
					num = 12;
					continue;
				}
				goto IL_130;
			case 22:
				if (true)
				{
				}
				if (A_1.IsStrikeThrough)
				{
					num = 6;
					continue;
				}
				goto IL_16B;
			case 23:
				if (A_1.FontKnownColor != (ExcelColors)(-1))
				{
					num = 8;
					continue;
				}
				goto IL_32C;
			}
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			num = 16;
			continue;
			IL_9E:
			fontUnderlineType = A_1.Underline;
			num = 3;
			continue;
			IL_130:
			string text = fontUnderlineType.ToString();
			text = spr\u1B7A.ᜄ(text);
			A_0.WriteAttributeString(RecordTableEnumerator.b("㕂⑄⭆", a_), text);
			A_0.WriteEndElement();
			num = 14;
			continue;
			IL_16B:
			num = 23;
			continue;
			IL_18F:
			num = 18;
			continue;
			IL_1B6:
			num = 22;
		}
		IL_87:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
		IL_12B:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁂⩄⥆ⵈ≊㥌♎㹐㵒", a_));
		IL_207:
		IL_32C:
		A_0.WriteEndElement();
	}

	// Token: 0x06004554 RID: 17748 RVA: 0x002A1A6C File Offset: 0x002A0A6C
	private void ᜃ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_166;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_20;
					}
					goto Block_3;
				case 2:
					return;
				case 3:
					goto IL_68;
				case 4:
				{
					XlsHyperLinksCollection xlsHyperLinksCollection;
					if (xlsHyperLinksCollection == null)
					{
						num = 2;
						continue;
					}
					RelationsCollection a_2 = A_1.DataHolder.ᜇ();
					int count = xlsHyperLinksCollection.Count;
					num = 10;
					continue;
				}
				case 5:
					goto IL_10A;
				case 6:
				{
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					XlsHyperLinksCollection xlsHyperLinksCollection = A_1.InnerHyperLinksOrNull;
					num = 4;
					continue;
				}
				case 7:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					XlsHyperLinksCollection xlsHyperLinksCollection;
					RelationsCollection a_2;
					this.ᜀ(A_0, (XlsHyperLink)xlsHyperLinksCollection[num2], a_2);
					num2++;
					num = 9;
					continue;
				}
				case 9:
					goto IL_14A;
				case 10:
				{
					int count;
					if (count == 0)
					{
						num = 1;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("嘽㤿㉁⅃㑅⑇⍉≋╍⍏", a_));
					int num2 = 0;
					num = 11;
					continue;
				}
				case 11:
					goto IL_14A;
				}
				IL_59:
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 6;
				continue;
				IL_20:
				goto IL_59;
				IL_14A:
				num = 7;
			}
			IL_68:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤽㈿⭁ぃ⍅㩇", a_));
			Block_3:
			if (false)
			{
			}
			return;
			IL_10A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
			IL_166:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004555 RID: 17749 RVA: 0x002A1C24 File Offset: 0x002A0C24
	private void ᜀ(XmlWriter A_0, XlsHyperLink A_1, RelationsCollection A_2)
	{
		int a_ = 10;
		int num = 16;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 17;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⠿㭁㑃⍅㩇♉╋⁍㭏", a_));
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㈿❁≃", a_), (A_1.Range as XlsRange).RangeAddressLocal, string.Empty);
				num = 18;
				continue;
			case 1:
				if (!text.Contains(RecordTableEnumerator.b("稿ṁ", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_291;
			case 2:
				goto IL_1FC;
			case 3:
				num = 5;
				continue;
			case 4:
				if (A_1.Address != RecordTableEnumerator.b("䀿", a_))
				{
					num = 9;
					continue;
				}
				goto IL_337;
			case 5:
				if (!text.StartsWith(RecordTableEnumerator.b("渿汁", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_80;
			case 6:
				if (text == null)
				{
					goto IL_337;
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
					num = 19;
					continue;
				}
				break;
			case 7:
				goto IL_7B;
			case 8:
				if (A_1.Type == HyperLinkType.File)
				{
					num = 3;
					continue;
				}
				goto IL_80;
			case 9:
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("ⰿⵁ❃❅㱇⍉⍋⁍", a_), A_1.Address, string.Empty);
				num = 14;
				continue;
			case 10:
				if (A_1.Type == HyperLinkType.Unc)
				{
					num = 12;
					continue;
				}
				goto IL_E7;
			case 11:
				goto IL_80;
			case 12:
				goto IL_291;
			case 13:
				num = 1;
				continue;
			case 14:
				goto IL_22B;
			case 15:
				num = 4;
				continue;
			case 17:
				goto IL_157;
			case 18:
			{
				if (A_1.Type == HyperLinkType.Workbook)
				{
					num = 15;
					continue;
				}
				string value = A_2.GenerateRelationId();
				text = A_1.Address;
				num = 8;
				continue;
			}
			case 19:
			{
				sprᦨ a_2 = new sprᦨ(text, RecordTableEnumerator.b("⠿㙁ぃ㙅片敉捋㵍㍏㩑ㅓ㭕㥗⥙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷呹፻౽궁풏﶑ﾙ辟邡钣隥麧薩\udeab쮭\udcaf펱삳\udfb5ힷ풹쾻횽ꦿ닁럃ꃇ돉볋ꯍꋏ뻑뷓룕돗", a_), true);
				A_2.ᜀ(a_2);
				string value;
				A_0.WriteAttributeString(RecordTableEnumerator.b("㈿", a_), RecordTableEnumerator.b("⤿♁", a_), null, value);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("ⰿⵁ❃❅㱇⍉⍋⁍", a_), A_1.SubAddress, string.Empty);
				num = 2;
				continue;
			}
			case 20:
				goto IL_E7;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			num = 0;
			continue;
			IL_80:
			num = 10;
			continue;
			IL_E7:
			text = this.ᜀ(text);
			num = 6;
			continue;
			IL_291:
			text = RecordTableEnumerator.b("☿⭁⡃⍅片敉捋慍", a_) + text;
			num = 20;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㜿ぁⵃ㉅ⵇ㡉", a_));
		IL_157:
		throw new ArgumentNullException(RecordTableEnumerator.b("⠿㭁㑃⍅㩇♉╋⁍㭏", a_));
		IL_1FC:
		IL_22B:
		IL_337:
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("㐿ⵁ⭃⩅㱇⍉㱋", a_), A_1.ScreenTip, null);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("␿⭁㝃㙅⑇⭉㕋", a_), A_1.TextToDisplay, string.Empty);
		A_0.WriteEndElement();
	}

	// Token: 0x06004556 RID: 17750 RVA: 0x002A1FA8 File Offset: 0x002A0FA8
	internal static void ᜄ(XmlWriter A_0, IPageSetupBase A_1, spr\u171C A_2)
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_8B;
			case 1:
				if (A_1 == null)
				{
					goto IL_83;
				}
				goto IL_A1;
			case 3:
				goto IL_62;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_83:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㉁╃ⅅⵇ᥉⥋㩍╏≑", a_));
		IL_A1:
		spr\u1B7A.ᜃ(A_0, A_1, A_2);
		spr\u1B7A.ᜂ(A_0, A_1, A_2);
		spr\u1B7A.ᜁ(A_0, A_1, A_2);
		spr\u1B7A.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06004557 RID: 17751 RVA: 0x002A2078 File Offset: 0x002A1078
	internal static void ᜃ(XmlWriter A_0, IPageSetupBase A_1, spr\u171C A_2)
	{
		int a_ = 2;
		int num = 15;
		for (;;)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			switch (num)
			{
			case 0:
				flag = false;
				goto IL_8B;
			case 1:
				if (!flag2)
				{
					num = 17;
					continue;
				}
				goto IL_206;
			case 2:
				num = 9;
				continue;
			case 3:
				return;
			case 4:
			{
				IPageSetup pageSetup;
				flag3 = pageSetup.IsPrintHeadings;
				goto IL_13F;
			}
			case 5:
				if (A_1.CenterVertically)
				{
					num = 14;
					continue;
				}
				return;
			case 6:
				goto IL_1BE;
			case 7:
				num = 10;
				continue;
			case 8:
				flag3 = false;
				goto IL_13F;
			case 9:
				if (!A_1.CenterHorizontally)
				{
					num = 20;
					continue;
				}
				goto IL_206;
			case 10:
			{
				IPageSetup pageSetup;
				flag = pageSetup.IsPrintGridlines;
				goto IL_8B;
			}
			case 11:
			{
				IPageSetup pageSetup;
				if (pageSetup != null)
				{
					num = 13;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1D4;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			}
			case 12:
			{
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				IPageSetup pageSetup = A_1 as IPageSetup;
				num = 18;
				continue;
			}
			case 13:
				num = 4;
				continue;
			case 14:
				goto IL_206;
			case 16:
				if (!flag4)
				{
					num = 2;
					continue;
				}
				goto IL_206;
			case 17:
				num = 16;
				continue;
			case 18:
			{
				IPageSetup pageSetup;
				if (pageSetup != null)
				{
					num = 7;
					continue;
				}
				num = 0;
				continue;
			}
			case 19:
				goto IL_7B;
			case 20:
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				num = 19;
				continue;
			}
			num = 12;
			continue;
			IL_8B:
			flag2 = flag;
			num = 11;
			continue;
			IL_13F:
			flag4 = flag3;
			if (true)
			{
			}
			num = 1;
			continue;
			IL_206:
			A_0.WriteStartElement(RecordTableEnumerator.b("䠷䠹唻倽㐿ു㑃㉅ⅇ╉≋㵍", a_), A_2.ᜇ());
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("強䠹唻娽ి⭁⩃⍅㭇", a_), flag2, false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("倷弹崻娽⤿ⱁ⍃㕅", a_), flag4, false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("倷唹主圽㨿ⵁ⩃㉅⥇♉ཋ⭍㹏♑ㅓ⑕㵗㹙", a_), A_1.CenterHorizontally, false);
			spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("丷弹主䨽⤿⅁╃⩅େ⽉≋㩍㕏⁑ㅓ㉕", a_), A_1.CenterVertically, false);
			A_0.WriteEndElement();
			num = 3;
		}
		IL_7B:
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_1BE:
		IL_1D4:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠷嬹嬻嬽ጿ❁ぃ㍅㡇", a_));
	}

	// Token: 0x06004558 RID: 17752 RVA: 0x002A231C File Offset: 0x002A131C
	internal static void ᜂ(XmlWriter A_0, IPageSetupBase A_1, spr\u171C A_2)
	{
		int a_ = 17;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6A;
				case 1:
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					goto IL_D8;
				case 3:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				case 4:
					goto IL_7F;
				case 5:
					goto IL_D6;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 3;
				}
			}
			IL_7F:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑆♈╊㹌㭎ぐ㵒⅔⑖", a_));
			IL_D6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝆⡈ⱊ⡌ᱎ㑐❒⁔❖", a_));
			IL_D8:
			A_0.WriteStartElement(A_2.ᜀ(), A_2.ᜇ());
			spr\u1B7A.ᜀ(A_0, A_2.ᜁ(), A_1.LeftMargin, double.MinValue);
			spr\u1B7A.ᜀ(A_0, A_2.ᜂ(), A_1.RightMargin, double.MinValue);
			spr\u1B7A.ᜀ(A_0, A_2.ᜃ(), A_1.TopMargin, double.MinValue);
			spr\u1B7A.ᜀ(A_0, A_2.ᜄ(), A_1.BottomMargin, double.MinValue);
			spr\u1B7A.ᜀ(A_0, A_2.ᜅ(), A_1.HeaderMarginInch, double.MinValue);
			spr\u1B7A.ᜀ(A_0, A_2.ᜆ(), A_1.FooterMarginInch, double.MinValue);
			A_0.WriteEndElement();
			return;
		}
		}
		IL_6A:
		throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
	}

	// Token: 0x06004559 RID: 17753 RVA: 0x002A24BC File Offset: 0x002A14BC
	internal static void ᜁ(XmlWriter A_0, IPageSetupBase A_1, spr\u171C A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				XlsPageSetupBase xlsPageSetupBase;
				switch (num)
				{
				case 0:
					goto IL_307;
				case 1:
					if (true)
					{
					}
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				case 2:
				{
					XlsPageSetup xlsPageSetup;
					if (xlsPageSetup != null)
					{
						goto IL_3C9;
					}
					goto IL_175;
				}
				case 3:
				{
					string relationId;
					A_0.WriteAttributeString(RecordTableEnumerator.b("唻娽", a_), relationId);
					num = 0;
					continue;
				}
				case 4:
				{
					if ((A_1 as XlsPageSetupBase).IsSettingsNotValid)
					{
						num = 5;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䰻弽✿❁ᝃ⍅㱇㽉㱋", a_), A_2.ᜇ());
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䰻弽〿❁㙃ᕅⅇぉ⥋", a_), (int)A_1.PaperSize, 1);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("伻崽ℿ⹁⅃", a_), A_1.Zoom, 100);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("娻圽㈿ㅁぃᙅ⥇ⵉ⥋M╏㽑㙓㍕⩗", a_), A_1.FirstPageNumber, 1);
					XlsPageSetup xlsPageSetup = A_1 as XlsPageSetup;
					num = 2;
					continue;
				}
				case 5:
					return;
				case 6:
				{
					XlsPageSetup xlsPageSetup;
					if (xlsPageSetup != null)
					{
						num = 8;
						continue;
					}
					goto IL_411;
				}
				case 7:
					goto IL_173;
				case 8:
				{
					XlsPageSetup xlsPageSetup;
					string relationId = xlsPageSetup.RelationId;
					num = 16;
					continue;
				}
				case 10:
				{
					XlsPageSetup xlsPageSetup;
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("娻圽㐿ᙁ⭃ᅅⅇ⹉㡋♍", a_), xlsPageSetup.FitToPagesWide, 1);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("娻圽㐿ᙁ⭃ๅⵇ⍉⭋♍⑏", a_), xlsPageSetup.FitToPagesTall, 1);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C9;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				}
				case 11:
					goto IL_321;
				case 12:
					goto IL_7C;
				case 13:
					if (!xlsPageSetupBase.IsSettingsNotValid)
					{
						num = 15;
						continue;
					}
					goto IL_321;
				case 14:
					goto IL_175;
				case 15:
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("弻儽〿⭁⅃㕅", a_), A_1.Copies, 1);
					num = 11;
					continue;
				case 16:
				{
					string relationId;
					if (relationId != null)
					{
						num = 3;
						continue;
					}
					goto IL_411;
				}
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				num = 1;
				continue;
				IL_175:
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䰻弽✿❁ୃ㑅ⱇ⽉㹋", a_), A_1.Order.ToString(), OrderType.DownThenOver.ToString());
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("医䰽⤿❁⩃㉅⥇㹉╋⅍㹏", a_), A_1.Orientation, (PageOrientationType)0);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("帻刽ℿ⅁⽃݅♇⹉ᭋ♍㥏♑ㅓ", a_), A_1.BlackAndWhite, false);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("堻䰽ℿ⑁ぃ", a_), A_1.Draft, false);
				string a_2 = spr\u1B7A.ᜀ(A_1.PrintComments);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("弻嬽ⰿ⹁݃⥅╇❉⥋⁍⑏⅑", a_), a_2, RecordTableEnumerator.b("刻儽⸿❁", a_));
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䤻䴽┿сⵃ㑅㭇㹉᱋⽍㝏㝑ᩓ⍕㕗㡙㥛ⱝ", a_), !A_1.AutoFirstPageNumber, false);
				string a_3 = spr\u1B7A.ᜀ(A_1.PrintErrors);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("夻䰽㈿ⵁ㙃㕅", a_), a_3, RecordTableEnumerator.b("堻圽㌿㉁⡃❅ㅇ⽉⡋", a_));
				xlsPageSetupBase = (XlsPageSetupBase)A_1;
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("吻儽㈿⭁㹃⥅♇㹉ⵋ≍ᑏ≑㵓", a_), xlsPageSetupBase.HResolution, 600);
				spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䨻嬽㈿㙁ⵃ╅⥇♉ࡋ㹍㥏", a_), xlsPageSetupBase.VResolution, 600);
				num = 13;
				continue;
				IL_321:
				num = 6;
				continue;
				IL_3C9:
				num = 10;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
			IL_173:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰻弽✿❁ᝃ⍅㱇㽉㱋", a_));
			IL_307:
			IL_411:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600455A RID: 17754 RVA: 0x002A28E0 File Offset: 0x002A18E0
	internal static void ᜀ(XmlWriter A_0, IPageSetupBase A_1, spr\u171C A_2)
	{
		int a_ = 6;
		int num = 7;
		for (;;)
		{
			string fullHeaderString;
			string fullFooterString;
			switch (num)
			{
			case 0:
				if (fullHeaderString != null)
				{
					num = 6;
					continue;
				}
				goto IL_83;
			case 1:
				goto IL_E5;
			case 2:
				return;
			case 3:
				num = 10;
				continue;
			case 4:
				goto IL_83;
			case 5:
				if (fullHeaderString.Length <= 0)
				{
					num = 4;
					continue;
				}
				goto IL_E5;
			case 6:
				num = 5;
				continue;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				break;
			case 8:
				if (fullFooterString != null)
				{
					num = 3;
					continue;
				}
				return;
			case 9:
				goto IL_7E;
			case 10:
				if (fullFooterString.Length > 0)
				{
					num = 1;
					continue;
				}
				return;
			case 11:
			{
				if (A_1 == null)
				{
					num = 12;
					continue;
				}
				XlsPageSetupBase xlsPageSetupBase = (XlsPageSetupBase)A_1;
				fullHeaderString = xlsPageSetupBase.FullHeaderString;
				fullFooterString = xlsPageSetupBase.FullFooterString;
				num = 0;
				continue;
			}
			case 12:
				goto IL_E0;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 11;
			continue;
			IL_83:
			if (true)
			{
			}
			num = 8;
			continue;
			IL_E5:
			A_0.WriteStartElement(RecordTableEnumerator.b("吻嬽ℿ♁⅃㑅็╉⍋㩍㕏⁑", a_), A_2.ᜇ());
			A_0.WriteElementString(RecordTableEnumerator.b("医娽␿ੁ⅃❅ⱇ⽉㹋", a_), A_2.ᜇ(), fullHeaderString);
			A_0.WriteElementString(RecordTableEnumerator.b("医娽␿с⭃⥅㱇⽉㹋", a_), A_2.ᜇ(), fullFooterString);
			A_0.WriteEndElement();
			num = 2;
		}
		IL_7E:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		IL_E0:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰻弽✿❁ᝃ⍅㱇㽉㱋", a_));
	}

	// Token: 0x0600455B RID: 17755 RVA: 0x002A2ABC File Offset: 0x002A1ABC
	private static string ᜀ(PrintCommentType A_0)
	{
		int a_ = 2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			string result;
			for (;;)
			{
				result = null;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_8E;
					case 2:
						switch (A_0)
						{
						case PrintCommentType.InPlace:
							result = RecordTableEnumerator.b("夷䤹砻圽㌿㉁⡃❅ㅇ⽉⡋", a_);
							num = 5;
							continue;
						case PrintCommentType.NoComments:
							result = RecordTableEnumerator.b("嘷唹刻嬽", a_);
							num = 4;
							continue;
						case PrintCommentType.SheetEnd:
							result = RecordTableEnumerator.b("夷丹礻倽␿", a_);
							num = 1;
							continue;
						default:
							num = 0;
							continue;
						}
						break;
					case 3:
						goto IL_B1;
					case 4:
						goto IL_A7;
					case 5:
						goto IL_E1;
					}
					break;
				}
			}
			IL_8E:
			IL_A7:
			IL_E1:
			if (true)
			{
			}
			return result;
		}
		}
		IL_B1:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠷䠹唻倽㐿แ⭃╅⥇㹉╋⅍㹏", a_));
	}

	// Token: 0x0600455C RID: 17756 RVA: 0x002A2BB8 File Offset: 0x002A1BB8
	private static string ᜀ(PrintErrorsType A_0)
	{
		int a_ = 19;
		string result;
		for (;;)
		{
			result = null;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case PrintErrorsType.Displayed:
						result = RecordTableEnumerator.b("ⵈ≊㹌㽎㵐㉒ⱔ㉖㵘", a_);
						num = 1;
						continue;
					case PrintErrorsType.Blank:
						result = RecordTableEnumerator.b("⭈❊ⱌⅎ㩐", a_);
						num = 3;
						continue;
					case PrintErrorsType.Dash:
						result = RecordTableEnumerator.b("ⵈ⩊㹌❎", a_);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case PrintErrorsType.NA:
						result = RecordTableEnumerator.b("݈੊", a_);
						num = 6;
						continue;
					default:
						num = 5;
						continue;
					}
					break;
				case 1:
					goto IL_10E;
				case 2:
					goto IL_AB;
				case 3:
					goto IL_F2;
				case 4:
					goto IL_B8;
				case 5:
					num = 4;
					continue;
				case 6:
					goto IL_70;
				}
				break;
			}
		}
		IL_70:
		IL_AB:
		return result;
		IL_B8:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㥈㥊⑌ⅎ═ὒ㩔㑖㡘⽚㑜ぞའ", a_));
		IL_F2:
		return result;
		IL_10E:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x0600455D RID: 17757 RVA: 0x002A2CE0 File Offset: 0x002A1CE0
	private string ᜀ(string A_0)
	{
		int a_ = 19;
		if (A_0 == null)
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
				return null;
			}
		}
		return A_0.Replace(RecordTableEnumerator.b("楈", a_), RecordTableEnumerator.b("汈祊経", a_));
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x002A2D50 File Offset: 0x002A1D50
	private void ᜂ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 12;
			for (;;)
			{
				IPageSetup pageSetup;
				ExcelColors tabKnownColor;
				string codeName;
				switch (num)
				{
				case 0:
					goto IL_17D;
				case 1:
					if (pageSetup.IsFitToPage)
					{
						num = 6;
						continue;
					}
					goto IL_370;
				case 2:
					if (!pageSetup.IsSummaryRowBelow)
					{
						num = 5;
						continue;
					}
					goto IL_287;
				case 3:
					if (true)
					{
					}
					if (pageSetup.IsSummaryColumnRight)
					{
						num = 19;
						continue;
					}
					goto IL_FE;
				case 4:
					goto IL_95;
				case 5:
					goto IL_FE;
				case 6:
					A_0.WriteStartElement(RecordTableEnumerator.b("㕄♆⹈⹊Ṍ⩎═ْ╔ݖ⭘", a_));
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("⍄⹆㵈Ὂ≌὎ぐ㑒ご", a_), pageSetup.IsFitToPage, false);
					A_0.WriteEndElement();
					num = 18;
					continue;
				case 7:
					if (tabKnownColor != (ExcelColors)(-1))
					{
						num = 20;
						continue;
					}
					goto IL_95;
				case 8:
					A_0.WriteAttributeString(RecordTableEnumerator.b("♄⡆ⵈ⹊͌⹎㱐㙒", a_), codeName);
					num = 14;
					continue;
				case 9:
					num = 13;
					continue;
				case 10:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_220;
				case 11:
					if (codeName.Length > 0)
					{
						num = 8;
						continue;
					}
					goto IL_C9;
				case 13:
					if (codeName != null)
					{
						num = 15;
						continue;
					}
					goto IL_C9;
				case 14:
					goto IL_C9;
				case 15:
					num = 11;
					continue;
				case 16:
					goto IL_287;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_220;
					default:
						goto IL_32D;
					}
					break;
				case 18:
					goto IL_1F5;
				case 19:
					num = 2;
					continue;
				case 20:
				{
					A_0.WriteStartElement(RecordTableEnumerator.b("ㅄ♆⭈ࡊ≌⍎㹐⅒", a_));
					string localName = RecordTableEnumerator.b("ⱄ⥆ⵈ⹊㕌⩎㕐", a_);
					int num2 = (int)tabKnownColor;
					A_0.WriteAttributeString(localName, num2.ToString());
					A_0.WriteEndElement();
					num = 4;
					continue;
				}
				case 21:
					if (this.ᡇ.HasMacros)
					{
						num = 9;
						continue;
					}
					goto IL_C9;
				}
				if (A_0 == null)
				{
					num = 17;
					continue;
				}
				num = 10;
				continue;
				IL_95:
				pageSetup = A_1.PageSetup;
				num = 3;
				continue;
				IL_C9:
				num = 7;
				continue;
				IL_FE:
				A_0.WriteStartElement(RecordTableEnumerator.b("⩄㉆㵈❊⑌ⅎ㑐͒❔", a_));
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㙄㉆⑈♊ⱌ㵎⡐Œ㱔ざㅘ⽚", a_), pageSetup.IsSummaryColumnRight, true);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("㙄㉆⑈♊ⱌ㵎⡐ᅒご㭖㙘ⱚ", a_), pageSetup.IsSummaryRowBelow, true);
				A_0.WriteEndElement();
				num = 16;
				continue;
				IL_220:
				A_0.WriteStartElement(RecordTableEnumerator.b("㙄⽆ⱈ⹊㥌὎⍐", a_));
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("ㅄ㕆⡈╊㹌♎═㩒㩔㥖᱘ⵚ㱜㍞ᑠɢᅤ๦٨ժ", a_), A_1.IsTransitionEvaluation, false);
				tabKnownColor = A_1.TabKnownColor;
				codeName = A_1.CodeName;
				num = 21;
				continue;
				IL_287:
				num = 1;
			}
			IL_17D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄⽆ⱈ⹊㥌", a_));
			IL_1F5:
			goto IL_370;
			IL_32D:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㉄㕆⁈㽊⡌㵎", a_));
			IL_370:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600455F RID: 17759 RVA: 0x002A30D4 File Offset: 0x002A20D4
	private void ᜀ(XmlWriter A_0, XlsWorksheet A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			Bitmap backgoundImage;
			for (;;)
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1 == null)
						{
							num = 3;
							continue;
						}
						backgoundImage = A_1.PageSetup.BackgoundImage;
						num = 2;
						continue;
					case 1:
						goto IL_7A;
					case 2:
						if (backgoundImage == null)
						{
							num = 1;
							continue;
						}
						goto IL_E6;
					case 3:
						goto IL_E4;
					case 5:
						goto IL_57;
					}
					if (A_0 == null)
					{
						num = 5;
					}
					else
					{
						num = 0;
					}
				}
				IL_7A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_C2;
				}
			}
			IL_57:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
			IL_C2:
			if (false)
			{
			}
			return;
			IL_E4:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
			IL_E6:
			sprᡟ sprᡟ = A_1.DataHolder;
			sprវ sprវ = sprᡟ.ᜋ();
			string key;
			string value = sprវ.ᜀ(backgoundImage.RawFormat, out key);
			sprវ.\u171A()[key] = value;
			string arg = sprវ.ᜀ(backgoundImage, null);
			RelationsCollection relationsCollection = sprᡟ.ᜇ();
			string text = relationsCollection.GenerateRelationId();
			relationsCollection[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막ꟍ뷏돑돓돕", a_));
			A_0.WriteStartElement(RecordTableEnumerator.b("㙅ⅇ⥉㡋㭍≏㝑", a_));
			A_0.WriteAttributeString(RecordTableEnumerator.b("㑅", a_), RecordTableEnumerator.b("⽅ⱇ", a_), null, text);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x002A3278 File Offset: 0x002A2278
	public void \u1712(XmlWriter A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				TimeSpan editTime;
				DocumentProperty documentProperty;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_79;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
				{
					int num2 = (int)editTime.TotalMinutes;
					A_0.WriteElementString(RecordTableEnumerator.b("洸吺䤼帾ⵀᝂⱄ⩆ⱈ", a_), num2.ToString());
					num = 4;
					continue;
				}
				case 2:
				{
					byte[] blob = documentProperty.Blob;
					string text = Encoding.Unicode.GetString(blob, 0, blob.Length);
					text = text.Remove(text.Length - 1);
					A_0.WriteElementString(RecordTableEnumerator.b("焸䈺䴼娾㍀⽂ⱄ⥆≈ॊⱌ㱎㑐", a_), text);
					num = 3;
					continue;
				}
				case 3:
					goto IL_190;
				case 4:
					goto IL_79;
				case 5:
					if (editTime != TimeSpan.MinValue)
					{
						num = 1;
						continue;
					}
					goto IL_79;
				case 6:
					if (true)
					{
					}
					if (documentProperty != null)
					{
						num = 2;
						continue;
					}
					goto IL_344;
				case 7:
					goto IL_74;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				A_0.WriteStartDocument(true);
				A_0.WriteStartElement(RecordTableEnumerator.b("椸䤺刼伾⑀ㅂㅄ⹆ⱈ㡊", a_), RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ቼ᥾춈搜ﲐﮔ뚘ꦚ궜꾞鞠貢삤\udfa6\udda8캪쎬쮮풰ힲ颴잶쮸풺춼\udabe돀럂계ꋆ뫈", a_));
				XlsBuiltInDocumentProperties xlsBuiltInDocumentProperties = this.ᡇ.BuiltInDocumentProperties as XlsBuiltInDocumentProperties;
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("砸䬺䴼匾⡀⁂⑄㍆⁈⑊⍌", a_), xlsBuiltInDocumentProperties.ApplicationName, null);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("稸区尼䴾⁀⁂ㅄ≆㭈㡊", a_), xlsBuiltInDocumentProperties.Characters, int.MinValue);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("稸吺值伾⁀ⵂ㱄", a_), xlsBuiltInDocumentProperties.Company, null);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("甸刺匼娾㉀", a_), xlsBuiltInDocumentProperties.LineCount, int.MinValue);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("琸娺匼帾♀♂㝄", a_), xlsBuiltInDocumentProperties.Manager, null);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("琸瘺縼匾⡀㍂㙄", a_), xlsBuiltInDocumentProperties.MultimediaClipCount, int.MinValue);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("眸吺䤼娾㉀", a_), xlsBuiltInDocumentProperties.SlideCount, int.MinValue);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("椸娺娼娾㉀", a_), xlsBuiltInDocumentProperties.PageCount, int.MinValue);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("椸娺似帾♀ㅂ⑄㝆ⅈ㡊", a_), xlsBuiltInDocumentProperties.ParagraphCount, int.MinValue);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("椸䤺堼䰾⑀ⵂㅄ♆㵈≊≌ⅎᝐ㱒❔㩖㡘⽚", a_), xlsBuiltInDocumentProperties.PresentationTarget, null);
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("洸帺值伾ⵀ≂ㅄ≆", a_), xlsBuiltInDocumentProperties.Template, null);
				editTime = xlsBuiltInDocumentProperties.EditTime;
				num = 5;
				continue;
				IL_79:
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("游吺似嬾㉀", a_), xlsBuiltInDocumentProperties.WordCount, int.MinValue);
				spr\u1AA2 spr_u1AA = (spr\u1AA2)this.ᡇ.CustomDocumentProperties;
				documentProperty = (spr_u1AA.ᜁ(RecordTableEnumerator.b("昸欺琼笾ṀགౄॆɈॊౌᱎᑐ", a_)) as DocumentProperty);
				num = 6;
			}
			IL_74:
			throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
			IL_190:
			IL_344:
			this.ᜀ(A_0);
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004561 RID: 17761 RVA: 0x002A35D8 File Offset: 0x002A25D8
	protected virtual void ᜀ(XmlWriter A_0)
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("稺䴼伾ᝀ♂㝄㑆⁈⑊⍌", a_), RecordTableEnumerator.b("਺༼ᄾ煀獂畄睆", a_), null);
	}

	// Token: 0x06004562 RID: 17762 RVA: 0x002A3640 File Offset: 0x002A2640
	public void ᜐ(XmlWriter A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_49;
				case 2:
				{
					DateTime lastPrinted;
					if (lastPrinted != DateTime.MinValue)
					{
						num = 4;
						continue;
					}
					goto IL_332;
				}
				case 3:
					goto IL_99;
				case 4:
				{
					DateTime lastPrinted;
					string value = lastPrinted.ToUniversalTime().ToString(RecordTableEnumerator.b("丶䀸䈺䐼ሾీโ桄⍆ⵈὊՌݎ歐㹒㡔浖⩘⡚ݜ", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("吶䤸", a_), RecordTableEnumerator.b("嬶堸䠺䤼漾㍀⩂⭄㍆ⱈ⽊", a_), null, value);
					num = 3;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					IBuiltInDocumentProperties builtInDocumentProperties = this.ᡇ.BuiltInDocumentProperties;
					A_0.WriteStartDocument(true);
					A_0.WriteStartElement(RecordTableEnumerator.b("吶䤸", a_), RecordTableEnumerator.b("吶嘸䤺堼漾㍀ⱂ㕄≆㭈㽊⑌⩎≐", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸୺ᱼ᱾Ꚉ릊붌뾎Ꞑ벒漢列ﺞ햠슢誤쒦욨\ud9aa좬芮솰솲\udab4잶\udcb8즺즼횾꓀냂", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("伶吸场匼䰾", a_), RecordTableEnumerator.b("匶娸", a_), null, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㕄㉆㭈❊捌⁎⍐㑒穔㍖㩘瑚㡜㍞Ѡ๢d०ᵨᡪ䉬幮彰䉲婴", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("伶吸场匼䰾", a_), RecordTableEnumerator.b("匶娸伺堼䴾ⱀあ", a_), null, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㕄㉆㭈❊捌⁎⍐㑒穔㍖㩘瑚⥜㩞፠๢ᙤ䡦", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("伶吸场匼䰾", a_), RecordTableEnumerator.b("匶娸嘺吼䬾㡀㍂⁄", a_), null, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㕄㉆㭈❊捌⁎⍐㑒穔㍖㩘瑚㥜㱞ౠ੢ᅤṦᥨ๪䉬", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("伶吸场匼䰾", a_), RecordTableEnumerator.b("伶䨸刺", a_), null, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㉄う㹈敊㩌籎罐㱒❔ざ癘楚浜潞偠䱢㵤⩦╨㡪๬ݮᑰṲᑴ婶ၸᕺ๼୾", a_));
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶堸伺堼堾⹀ㅂ㱄", a_), builtInDocumentProperties.Category, null, RecordTableEnumerator.b("吶䤸", a_));
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("吶䬸帺尼䬾⹀ㅂ", a_), builtInDocumentProperties.Author, null, RecordTableEnumerator.b("匶娸", a_));
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("匶尸䠺帼䴾⡀㍂ㅄ⹆♈╊", a_), builtInDocumentProperties.Comments, null, RecordTableEnumerator.b("匶娸", a_));
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("尶尸䈺䨼倾㍀❂㙄", a_), builtInDocumentProperties.Keywords, null, RecordTableEnumerator.b("吶䤸", a_));
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("嬶堸䠺䤼爾⹀❂ⱄⅆ⁈⹊⥌ൎ⡐", a_), builtInDocumentProperties.LastAuthor, null, RecordTableEnumerator.b("吶䤸", a_));
					this.ᜀ(A_0, RecordTableEnumerator.b("吶䬸帺尼䬾⑀❂", a_), (builtInDocumentProperties as XlsBuiltInDocumentProperties).CreatedTime);
					this.ᜀ(A_0, RecordTableEnumerator.b("娶嘸强吼夾⡀♂⅄", a_), (builtInDocumentProperties as XlsBuiltInDocumentProperties).LastSaveTime);
					spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䐶䰸夺圼娾≀㝂", a_), builtInDocumentProperties.Subject, null, RecordTableEnumerator.b("匶娸", a_));
					DateTime lastPrinted = builtInDocumentProperties.LastPrinted;
					num = 2;
				}
			}
			IL_49:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_99:
			IL_332:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_99;
			default:
			{
				if (false)
				{
				}
				IBuiltInDocumentProperties builtInDocumentProperties;
				spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("䌶倸伺儼娾", a_), builtInDocumentProperties.Title, null, RecordTableEnumerator.b("匶娸", a_));
				A_0.WriteEndElement();
				return;
			}
			}
			break;
		}
		}
	}

	// Token: 0x06004563 RID: 17763 RVA: 0x002A39CC File Offset: 0x002A29CC
	private void ᜀ(XmlWriter A_0, string A_1, DateTime A_2)
	{
		int a_ = 17;
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
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				return;
			case 2:
			{
				A_0.WriteStartElement(RecordTableEnumerator.b("⍆⩈㽊⡌㵎㱐⁒", a_), A_1, null);
				A_0.WriteAttributeString(RecordTableEnumerator.b("㽆㩈≊", a_), RecordTableEnumerator.b("㍆え㭊⡌", a_), null, RecordTableEnumerator.b("⍆⩈㽊⡌㵎㱐⁒潔V橘ᡚᥜ୞❠", a_));
				string data = A_2.ToUniversalTime().ToString(RecordTableEnumerator.b("㹆え㉊㑌扎᱐Ṓ硔㍖㵘ཚᕜ᝞孠๢ࡤ嵦ᩨᡪ㝬", a_));
				A_0.WriteRaw(data);
				A_0.WriteEndElement();
				num = 1;
				continue;
			}
			}
			if (!(A_2.Date != DateTime.MinValue))
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06004564 RID: 17764 RVA: 0x002A3ACC File Offset: 0x002A2ACC
	public void ᜎ(XmlWriter A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				XlsDocumentProperty xlsDocumentProperty;
				switch (num)
				{
				case 0:
					goto IL_1A5;
				case 1:
					goto IL_18F;
				case 2:
				{
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					spr\u1AA2 spr_u1AA;
					xlsDocumentProperty = spr_u1AA[num2];
					num = 6;
					continue;
				}
				case 3:
					goto IL_173;
				case 5:
					goto IL_8A;
				case 6:
					if (xlsDocumentProperty.Name != RecordTableEnumerator.b("栶椸爺礼怾ീੂୄెୈ੊Ṍ੎", a_))
					{
						num = 10;
						continue;
					}
					goto IL_1A5;
				case 7:
					goto IL_ED;
				case 8:
					goto IL_173;
				case 9:
					if (xlsDocumentProperty.Name != RecordTableEnumerator.b("栶椸爺礼怾ीགౄॆɈᡊ", a_))
					{
						num = 7;
						continue;
					}
					goto IL_1A5;
				case 10:
					if (true)
					{
					}
					num = 9;
					continue;
				}
				int num3;
				if (A_0 != null)
				{
					A_0.WriteStartDocument(true);
					A_0.WriteStartElement(RecordTableEnumerator.b("朶䬸吺䴼娾㍀㝂ⱄ≆㩈", a_), RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ᑺ᭼᥾쎆ﶒ뢖ꮘꮚ궜ꦞ躠삢키풦\udda8쒪사芮솰솲\udab4잶\udcb8즺즼횾꓀냂", a_));
					A_0.WriteAttributeString(RecordTableEnumerator.b("伶吸场匼䰾", a_), RecordTableEnumerator.b("䄶䴸", a_), null, RecordTableEnumerator.b("弶䴸伺䴼Ծ湀求㙄⑆ⅈ⹊⁌⹎≐絒㩔❖㱘㕚╜㉞ൠբ੤ᕦѨ੪ᥬᱮ彰ᱲݴၶ噸ᑺ᭼᥾쎆ﶒ뢖ꮘꮚ궜ꦞ躠잢쪤쒦令\ud9aa슬\udfae슰캶즸\udeba캼", a_));
					spr\u1AA2 spr_u1AA = (spr\u1AA2)this.ᡇ.CustomDocumentProperties;
					num3 = 2;
					num2 = 0;
					int count = spr_u1AA.Count;
					num = 3;
					continue;
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
					num = 5;
					continue;
				}
				IL_ED:
				this.ᜀ(A_0, xlsDocumentProperty, num3);
				num3++;
				num = 0;
				continue;
				IL_173:
				num = 2;
				continue;
				IL_1A5:
				num2++;
				num = 8;
			}
			IL_8A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀶䬸刺䤼娾㍀", a_));
			IL_18F:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004565 RID: 17765 RVA: 0x002A3CD8 File Offset: 0x002A2CD8
	private void ᜀ(XmlWriter A_0, XlsDocumentProperty A_1, int A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_269;
				case 1:
					num = 20;
					continue;
				case 2:
					goto IL_20C;
				case 3:
				{
					PropertyType propertyType;
					if (propertyType <= PropertyType.Bool)
					{
						num = 14;
						continue;
					}
					num = 10;
					continue;
				}
				case 4:
					goto IL_35B;
				case 5:
				{
					PropertyType propertyType;
					if (propertyType != PropertyType.Bool)
					{
						num = 16;
						continue;
					}
					string text = A_1.Boolean.ToString();
					text = text.ToLower(CultureInfo.InvariantCulture);
					A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("娷唹医刽", a_), null, text);
					goto IL_3A4;
				}
				case 6:
					goto IL_3C1;
				case 7:
					goto IL_3B0;
				case 8:
					num = 21;
					continue;
				case 10:
				{
					PropertyType propertyType;
					if (propertyType != PropertyType.Int)
					{
						num = 24;
						continue;
					}
					string value = A_1.Integer.ToString();
					A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("儷吹䠻", a_), null, value);
					num = 13;
					continue;
				}
				case 11:
				{
					PropertyType propertyType;
					switch (propertyType)
					{
					case PropertyType.AsciiString:
						A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("吷䨹伻䨽㈿", a_), null, A_1.Text);
						num = 17;
						continue;
					case PropertyType.String:
						A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("吷䨹䬻䴽㐿ぁ", a_), null, A_1.Text);
						num = 0;
						continue;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 12:
					num = 5;
					continue;
				case 13:
					goto IL_2B0;
				case 14:
					num = 18;
					continue;
				case 15:
					goto IL_22F;
				case 16:
					if (true)
					{
					}
					num = 6;
					continue;
				case 17:
					goto IL_FF;
				case 18:
				{
					PropertyType propertyType;
					switch (propertyType)
					{
					case PropertyType.Int32:
					{
						string value2 = A_1.Int32.ToString();
						A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("儷ู", a_), null, value2);
						num = 2;
						continue;
					}
					case (PropertyType)4:
						goto IL_4AB;
					case PropertyType.Double:
					{
						string value3 = A_1.Double.ToString(NumberFormatInfo.InvariantInfo);
						A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("䨷ȹ", a_), null, value3);
						num = 4;
						continue;
					}
					default:
						num = 12;
						continue;
					}
					break;
				}
				case 19:
					goto IL_495;
				case 20:
				{
					PropertyType propertyType;
					if (propertyType != PropertyType.DateTime)
					{
						num = 8;
						continue;
					}
					string value4 = A_1.DateTime.ToUniversalTime().ToString(RecordTableEnumerator.b("䄷䌹䔻䜽洿ཁृ歅ⱇ⹉ᡋٍᡏ桑㥓㭕扗⥙⽛ѝ", a_));
					A_0.WriteElementString(RecordTableEnumerator.b("丷丹", a_), RecordTableEnumerator.b("帷匹倻嬽㐿⭁⥃⍅", a_), null, value4);
					num = 19;
					continue;
				}
				case 21:
					goto IL_4A6;
				case 22:
				{
					if (A_1 == null)
					{
						num = 15;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䠷䠹医丽┿ぁぃ㽅", a_));
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("帷圹䠻圽␿", a_), RecordTableEnumerator.b("䌷縹ऻ紽пف煃癅絇杉繋୍楏ᅑ祓杕桗歙ṛ獝奟兡嵣入䕧婩呫幭䁯䁱㙳䑵㭷㱹䕻㽽앿ﾁ", a_), string.Empty);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("䠷匹堻", a_), A_2, int.MinValue);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("嘷嬹儻嬽", a_), A_1.Name, string.Empty);
					PropertyType propertyType = A_1.PropertyType;
					num = 3;
					continue;
				}
				case 23:
					goto IL_C5;
				case 24:
					num = 11;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_3A4:
					num = 7;
					break;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 23;
					}
					else
					{
						num = 22;
					}
					break;
				}
			}
			IL_C5:
			throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
			IL_FF:
			IL_20C:
			goto IL_4AB;
			IL_22F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠷䠹医丽┿ぁぃ㽅", a_));
			IL_269:
			IL_2B0:
			IL_35B:
			IL_3B0:
			IL_3C1:
			IL_495:
			IL_4A6:
			IL_4AB:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x06004566 RID: 17766 RVA: 0x002A4198 File Offset: 0x002A3198
	private void ᜀ(XmlWriter A_0, List<Dictionary<string, string>> A_1)
	{
		int a_ = 16;
		int num = 1;
		for (;;)
		{
			Dictionary<string, string> dictionary;
			Dictionary<string, string> dictionary2;
			switch (num)
			{
			case 0:
				A_1 = new List<Dictionary<string, string>>();
				A_1.Add(dictionary);
				num = 12;
				continue;
			case 2:
				return;
			case 3:
				dictionary2 = A_1[0];
				goto IL_170;
			case 4:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				goto IL_14F;
			case 5:
				try
				{
					num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							List<Dictionary<string, string>>.Enumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							Dictionary<string, string> a_2 = enumerator.Current;
							this.ᜀ(A_0, a_2);
							num = 4;
							continue;
						}
						case 1:
							goto IL_11B;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						IL_DC:
						num = 0;
						continue;
						IL_C0:
						goto IL_DC;
						goto IL_C0;
					}
					IL_11B:
					goto IL_250;
				}
				finally
				{
					List<Dictionary<string, string>>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				goto IL_12E;
			case 6:
				num = 11;
				continue;
			case 7:
				if (A_1 != null)
				{
					num = 8;
					continue;
				}
				num = 10;
				continue;
			case 8:
				if (true)
				{
				}
				num = 3;
				continue;
			case 9:
			{
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("⑅❇╉❋ᡍ㥏㝑⍓╕", a_));
				List<Dictionary<string, string>>.Enumerator enumerator = A_1.GetEnumerator();
				num = 5;
				continue;
			}
			case 10:
				dictionary2 = new Dictionary<string, string>();
				goto IL_170;
			case 11:
				if (dictionary.Count != 0)
				{
					num = 0;
					continue;
				}
				goto IL_14F;
			case 12:
				goto IL_14F;
			case 13:
				goto IL_5F;
			}
			if (A_0 == null)
			{
				num = 13;
				continue;
			}
			IL_12E:
			num = 7;
			continue;
			IL_14F:
			num = 9;
			continue;
			IL_170:
			dictionary = dictionary2;
			this.ᜀ(dictionary, RecordTableEnumerator.b("❅⭇㹉╋㡍㕏ّ㕓㑕", a_), this.ᡇ.ActiveSheetIndex);
			this.ᜀ(dictionary, RecordTableEnumerator.b("⁅ⅇ㡉㽋㩍͏㩑ㅓ㍕ⱗ", a_), this.ᡇ.DisplayedTab);
			num = 4;
		}
		IL_5F:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ㩇⍉㡋⭍≏", a_));
		IL_250:
		A_0.WriteEndElement();
	}

	// Token: 0x06004567 RID: 17767 RVA: 0x002A440C File Offset: 0x002A340C
	private void ᜀ(XmlWriter A_0, Dictionary<string, string> A_1)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				A_0.WriteStartElement(RecordTableEnumerator.b("䨼倾㍀⡂❄⡆♈⁊ᭌ♎㑐⑒", a_));
				Dictionary<string, string>.Enumerator enumerator = A_1.GetEnumerator();
				num = 2;
				continue;
			}
			case 2:
				goto IL_154;
			case 3:
				goto IL_12C;
			case 4:
				goto IL_3B;
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 0;
			}
		}
		IL_3B:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_EB:
		throw new ArgumentNullException(RecordTableEnumerator.b("夼嘾≀ᕂⱄ≆㹈", a_));
		IL_12C:
		goto IL_EB;
		IL_154:
		try
		{
			num = 4;
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
						num = 3;
						continue;
					}
					break;
				case 2:
				{
					Dictionary<string, string>.Enumerator enumerator;
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					spr\u1B7A.ᜁ(A_0, keyValuePair.Key, keyValuePair.Value, string.Empty);
					num = 1;
					continue;
				}
				case 3:
					goto IL_DB;
				}
				IL_6E:
				num = 2;
				continue;
				IL_6C:
				goto IL_6E;
				goto IL_6C;
			}
			IL_DB:
			goto IL_159;
		}
		finally
		{
			Dictionary<string, string>.Enumerator enumerator;
			((IDisposable)enumerator).Dispose();
		}
		goto IL_EB;
		IL_159:
		A_0.WriteEndElement();
	}

	// Token: 0x06004568 RID: 17768 RVA: 0x002A4594 File Offset: 0x002A3594
	private void ᜀ(Dictionary<string, string> A_0, string A_1, int A_2)
	{
		if (true)
		{
		}
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_80;
			case 1:
				return;
			case 2:
				if (A_2 != 0)
				{
					num = 0;
					continue;
				}
				return;
			case 3:
				num = 5;
				continue;
			case 4:
				goto IL_A4;
			case 5:
				if (A_2 != 0)
				{
					num = 4;
					continue;
				}
				goto IL_82;
			}
			if (A_0.ContainsKey(A_1))
			{
				num = 3;
				continue;
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
				num = 2;
				continue;
			}
			IL_80:
			A_0.Add(A_1, A_2.ToString());
			num = 1;
		}
		IL_82:
		A_0.Remove(A_1);
		return;
		IL_A4:
		A_0[A_1] = A_2.ToString();
	}

	// Token: 0x06004569 RID: 17769 RVA: 0x002A4674 File Offset: 0x002A3674
	private void ᜀ(XmlWriter A_0, RelationsCollection A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				bool flag;
				int num2;
				XlsExternBookCollection externWorkbooks;
				XlsExternWorkbook xlsExternWorkbook;
				switch (num)
				{
				case 0:
					goto IL_104;
				case 2:
					if (!flag)
					{
						num = 8;
						continue;
					}
					return;
				case 3:
				{
					int count;
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					xlsExternWorkbook = externWorkbooks[num2];
					num = 10;
					continue;
				}
				case 4:
					num = 15;
					continue;
				case 5:
					num = 19;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AF;
					default:
						if (false)
						{
						}
						flag = false;
						A_0.WriteStartElement(RecordTableEnumerator.b("夻䘽㐿❁㙃⡅⥇♉ṋ⭍㙏㝑♓㍕㙗㥙㥛ⵝ", a_));
						num = 12;
						continue;
					}
					break;
				case 7:
					goto IL_1EA;
				case 8:
					A_0.WriteEndElement();
					num = 20;
					continue;
				case 9:
					if (!string.IsNullOrEmpty(xlsExternWorkbook.URL))
					{
						num = 5;
						continue;
					}
					goto IL_91;
				case 10:
					if (!xlsExternWorkbook.IsInternalReference)
					{
						num = 11;
						continue;
					}
					goto IL_91;
				case 11:
					num = 9;
					continue;
				case 12:
					goto IL_160;
				case 13:
					goto IL_91;
				case 14:
				{
					num2 = 0;
					int count = externWorkbooks.Count;
					if (true)
					{
					}
					num = 7;
					continue;
				}
				case 15:
					goto IL_AF;
				case 16:
					goto IL_8C;
				case 17:
					goto IL_1EA;
				case 18:
					if (externWorkbooks.Count != 0)
					{
						num = 14;
						continue;
					}
					goto IL_104;
				case 19:
					if (!xlsExternWorkbook.IsAddInFunctions)
					{
						num = 4;
						continue;
					}
					goto IL_91;
				case 20:
					return;
				}
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				externWorkbooks = this.ᡇ.ExternWorkbooks;
				IWorksheets worksheets = this.ᡇ.Worksheets;
				flag = true;
				num = 18;
				continue;
				IL_91:
				num2++;
				num = 17;
				continue;
				IL_AF:
				if (flag)
				{
					num = 6;
					continue;
				}
				goto IL_160;
				IL_104:
				num = 2;
				continue;
				IL_160:
				this.ᜀ(xlsExternWorkbook, A_0, A_1);
				num = 13;
				continue;
				IL_1EA:
				num = 3;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬻䰽⤿㙁⅃㑅", a_));
		}
		}
	}

	// Token: 0x0600456A RID: 17770 RVA: 0x002A4900 File Offset: 0x002A3900
	private void ᜀ(XlsExternWorkbook A_0, XmlWriter A_1, RelationsCollection A_2)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				goto IL_3C;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_A1;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 3;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("崷䈹䠻嬽㈿ⱁك⥅❇ⅉ", a_));
		IL_6F:
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
		throw new ArgumentNullException(RecordTableEnumerator.b("伷䠹唻䨽┿ぁ", a_));
		IL_A1:
		string arg = this.ᡇ.DataHolder.ᜀ(A_0);
		A_1.WriteStartElement(RecordTableEnumerator.b("崷䈹䠻嬽㈿ⱁ╃⩅ᩇ⽉⩋⭍≏㝑㩓㕕㵗", a_));
		string text = A_2.GenerateRelationId();
		A_2[text] = new sprᦨ('/' + arg, RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻醽ꖿ뫁냃ꏅ뫇꓉귋ꋍ鳏믑뫓뷕", a_));
		A_1.WriteAttributeString(RecordTableEnumerator.b("儷帹", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻", a_), text);
		A_1.WriteEndElement();
	}

	// Token: 0x0600456B RID: 17771 RVA: 0x002A4A2C File Offset: 0x002A3A2C
	private void ᜀ(XmlWriter A_0, IWorksheet A_1)
	{
		int a_ = 7;
		int num = 9;
		for (;;)
		{
			XlsVPageBreaksCollection xlsVPageBreaksCollection;
			switch (num)
			{
			case 0:
			{
				XlsHPageBreaksCollection xlsHPageBreaksCollection;
				this.ᜀ(A_0, xlsHPageBreaksCollection);
				num = 5;
				continue;
			}
			case 1:
				goto IL_D8;
			case 2:
				goto IL_4C;
			case 3:
				goto IL_9D;
			case 4:
				if (xlsVPageBreaksCollection != null)
				{
					num = 7;
					continue;
				}
				goto IL_129;
			case 5:
				goto IL_EE;
			case 6:
			{
				XlsHPageBreaksCollection xlsHPageBreaksCollection;
				if (xlsHPageBreaksCollection != null)
				{
					num = 0;
					continue;
				}
				goto IL_EE;
			}
			case 7:
				this.ᜀ(A_0, xlsVPageBreaksCollection);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D8;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 8:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				XlsHPageBreaksCollection xlsHPageBreaksCollection = (XlsHPageBreaksCollection)A_1.HPageBreaks;
				num = 6;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 8;
			continue;
			IL_EE:
			xlsVPageBreaksCollection = (XlsVPageBreaksCollection)A_1.VPageBreaks;
			num = 4;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨼䴾⡀㝂⁄㕆", a_));
		IL_9D:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄ", a_));
		IL_D8:
		IL_129:
		if (true)
		{
		}
	}

	// Token: 0x0600456C RID: 17772 RVA: 0x002A4B6C File Offset: 0x002A3B6C
	private void ᜀ(XmlWriter A_0, XlsHPageBreaksCollection A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				List<XlsHPageBreak> list;
				int num2;
				XlsHPageBreak xlsHPageBreak;
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					SortedList<int, List<XlsHPageBreak>> sortedList;
					int key;
					if (!sortedList.TryGetValue(key, out list))
					{
						num = 2;
						continue;
					}
					goto IL_C2;
				}
				case 2:
				{
					list = new List<XlsHPageBreak>();
					SortedList<int, List<XlsHPageBreak>> sortedList;
					int key;
					sortedList.Add(key, list);
					num = 14;
					continue;
				}
				case 3:
					goto IL_109;
				case 4:
					goto IL_94;
				case 5:
				{
					int count;
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					xlsHPageBreak = (XlsHPageBreak)A_1[num2];
					int key = (int)xlsHPageBreak.HPageBreak.ᜃ();
					num = 1;
					continue;
				}
				case 6:
				{
					int num3 = 0;
					SortedList<int, List<XlsHPageBreak>> sortedList;
					int count2 = sortedList.Count;
					num = 11;
					continue;
				}
				case 7:
				{
					if (A_1 == null)
					{
						num = 15;
						continue;
					}
					int count = A_1.Count;
					goto IL_E6;
				}
				case 8:
					goto IL_2FF;
				case 9:
				{
					int count;
					if (count == 0)
					{
						num = 0;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("䴾⹀㑂݄㕆ⱈ⩊♌㱎", a_));
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("尾⹀㙂⭄㍆", a_), count, 0);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("刾⁀ⵂい♆╈ॊ㽌⩎ぐ㡒ᙔ㡖ⱘ㕚⥜", a_), A_1.ManualBreakCount, 0);
					SortedList<int, List<XlsHPageBreak>> sortedList = new SortedList<int, List<XlsHPageBreak>>();
					num2 = 0;
					num = 20;
					continue;
				}
				case 10:
				{
					int num4;
					int count3;
					if (num4 >= count3)
					{
						num = 16;
						continue;
					}
					List<XlsHPageBreak> list2;
					XlsHPageBreak xlsHPageBreak2 = list2[num4];
					spr\u2539.ᜀ ᜀ = xlsHPageBreak2.HPageBreak;
					this.ᜀ(A_0, (int)ᜀ.ᜃ(), (int)ᜀ.ᜀ(), (int)ᜀ.ᜁ(), xlsHPageBreak2.Type);
					num4++;
					num = 3;
					continue;
				}
				case 11:
					goto IL_165;
				case 12:
					goto IL_186;
				case 14:
					goto IL_C2;
				case 15:
					goto IL_14A;
				case 16:
				{
					int num3;
					num3++;
					num = 19;
					continue;
				}
				case 17:
				{
					int num3;
					int count2;
					if (num3 >= count2)
					{
						num = 12;
						continue;
					}
					SortedList<int, List<XlsHPageBreak>> sortedList;
					List<XlsHPageBreak> list2 = sortedList.Values[num3];
					int num4 = 0;
					int count3 = list2.Count;
					num = 18;
					continue;
				}
				case 18:
					goto IL_109;
				case 19:
					goto IL_165;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E6;
					default:
						if (false)
						{
						}
						goto IL_2FF;
					}
					break;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 7;
				continue;
				IL_C2:
				list.Add(xlsHPageBreak);
				num2++;
				num = 8;
				continue;
				IL_E6:
				num = 9;
				continue;
				IL_109:
				num = 10;
				continue;
				IL_165:
				num = 17;
				continue;
				IL_2FF:
				num = 5;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠾㍀⩂ㅄ≆㭈", a_));
			IL_14A:
			throw new ArgumentNullException(RecordTableEnumerator.b("圾ᅀ≂≄≆⭈㥊⡌⹎㩐⁒", a_));
			IL_186:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600456D RID: 17773 RVA: 0x002A4EA0 File Offset: 0x002A3EA0
	private void ᜀ(XmlWriter A_0, XlsVPageBreaksCollection A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 17;
			for (;;)
			{
				int num2;
				XlsVPageBreak xlsVPageBreak;
				List<XlsVPageBreak> list;
				switch (num)
				{
				case 0:
				{
					int count;
					if (num2 >= count)
					{
						num = 10;
						continue;
					}
					xlsVPageBreak = (XlsVPageBreak)A_1[num2];
					int key = (int)xlsVPageBreak.VPageBreak.ᜁ();
					num = 2;
					continue;
				}
				case 1:
				{
					if (true)
					{
					}
					int num3;
					num3++;
					num = 3;
					continue;
				}
				case 2:
				{
					int key;
					SortedList<int, List<XlsVPageBreak>> sortedList;
					if (!sortedList.TryGetValue(key, out list))
					{
						num = 5;
						continue;
					}
					goto IL_BA;
				}
				case 3:
					goto IL_15D;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
						if (false)
						{
						}
						goto IL_2FF;
					}
					break;
				case 5:
				{
					list = new List<XlsVPageBreak>();
					int key;
					SortedList<int, List<XlsVPageBreak>> sortedList;
					sortedList.Add(key, list);
					num = 16;
					continue;
				}
				case 6:
					goto IL_101;
				case 7:
				{
					int num3;
					int count2;
					if (num3 >= count2)
					{
						num = 14;
						continue;
					}
					SortedList<int, List<XlsVPageBreak>> sortedList;
					List<XlsVPageBreak> list2 = sortedList.Values[num3];
					int num4 = 0;
					int count3 = list2.Count;
					num = 12;
					continue;
				}
				case 8:
					return;
				case 9:
				{
					if (A_1 == null)
					{
						num = 20;
						continue;
					}
					int count = A_1.Count;
					goto IL_DE;
				}
				case 10:
				{
					int num3 = 0;
					SortedList<int, List<XlsVPageBreak>> sortedList;
					int count2 = sortedList.Count;
					num = 19;
					continue;
				}
				case 11:
				{
					int count;
					if (count == 0)
					{
						num = 8;
						continue;
					}
					A_0.WriteStartElement(RecordTableEnumerator.b("唵圷嘹縻䰽┿⍁⽃㕅", a_));
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("唵圷伹刻䨽", a_), count, 0);
					spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("嬵夷吹䤻弽ⰿA㙃⍅⥇ⅉཋ⅍╏㱑⁓", a_), A_1.ManualBreakCount, 0);
					SortedList<int, List<XlsVPageBreak>> sortedList = new SortedList<int, List<XlsVPageBreak>>();
					num2 = 0;
					num = 4;
					continue;
				}
				case 12:
					goto IL_101;
				case 13:
				{
					int num4;
					int count3;
					if (num4 >= count3)
					{
						num = 1;
						continue;
					}
					List<XlsVPageBreak> list2;
					XlsVPageBreak xlsVPageBreak2 = list2[num4];
					spr\u2583.ᜀ ᜀ = xlsVPageBreak2.VPageBreak;
					this.ᜀ(A_0, (int)ᜀ.ᜁ(), (int)ᜀ.ᜃ(), (int)ᜀ.ᜀ(), xlsVPageBreak2.Type);
					num4++;
					num = 6;
					continue;
				}
				case 14:
					goto IL_17E;
				case 15:
					goto IL_8C;
				case 16:
					goto IL_BA;
				case 18:
					goto IL_2FF;
				case 19:
					goto IL_15D;
				case 20:
					goto IL_142;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				num = 9;
				continue;
				IL_BA:
				list.Add(xlsVPageBreak);
				num2++;
				num = 18;
				continue;
				IL_DE:
				num = 11;
				continue;
				IL_101:
				num = 13;
				continue;
				IL_15D:
				num = 7;
				continue;
				IL_2FF:
				num = 0;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			IL_142:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀵样嬹嬻嬽∿ぁ⅃❅⍇㥉", a_));
			IL_17E:
			A_0.WriteEndElement();
			return;
		}
		}
	}

	// Token: 0x0600456E RID: 17774 RVA: 0x002A51D4 File Offset: 0x002A41D4
	private void ᜀ(XmlWriter A_0, int A_1, int A_2, int A_3, PageBreakType A_4)
	{
		int a_ = 9;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0.WriteStartElement(RecordTableEnumerator.b("崾㍀⡂", a_));
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("嘾╀", a_), A_1, 0);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("刾⡀ⵂ", a_), A_2, 0);
		spr\u1B7A.ᜁ(A_0, RecordTableEnumerator.b("刾⁀㭂", a_), A_3, 0);
		spr\u1B7A.ᜀ(A_0, RecordTableEnumerator.b("刾⁀ⵂ", a_), A_4 == PageBreakType.Manual, false);
		A_0.WriteEndElement();
	}

	// Token: 0x0600456F RID: 17775 RVA: 0x002A5290 File Offset: 0x002A4290
	public static void ᜀ(XmlWriter A_0, Size A_1)
	{
		int a_ = 5;
		int num = 0;
		int num2;
		int num3;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_44;
			case 2:
				goto IL_122;
			case 3:
				if (num2 <= 0)
				{
					num = 6;
					continue;
				}
				goto IL_12F;
			case 4:
				goto IL_9E;
			case 5:
				goto IL_46;
			case 6:
				num2 = 6293304;
				num = 4;
				continue;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_122;
				default:
					if (false)
					{
					}
					if (num3 <= 0)
					{
						num = 2;
						continue;
					}
					goto IL_46;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num3 = A_1.Width;
			num2 = A_1.Height;
			num3 = (int)spr\u17FF.ᜀ((double)num3, MeasureUnits.EMU);
			num2 = (int)spr\u17FF.ᜀ((double)num2, MeasureUnits.EMU);
			A_0.WriteStartElement(RecordTableEnumerator.b("帺䔼䬾", a_), RecordTableEnumerator.b("区䤼䬾ㅀ祂橄框㩈⡊╌⩎㱐㉒♔祖㙘⭚㡜ㅞᥠ๢।Ŧ٨ᥪl๮հr孴ᡶ୸ᱺ剼᭾뺐ꆒꖔꞖ꾘뒚펠욢쒤쎦\udaa8쎪좬쪮얰잴횶캸튺펼\ud8be", a_));
			num = 7;
			continue;
			IL_46:
			num = 3;
			continue;
			IL_122:
			if (true)
			{
			}
			num3 = 8666049;
			num = 5;
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
		IL_9E:
		IL_12F:
		A_0.WriteAttributeString(RecordTableEnumerator.b("堺䔼", a_), num3.ToString());
		A_0.WriteAttributeString(RecordTableEnumerator.b("堺䐼", a_), num2.ToString());
		A_0.WriteEndElement();
	}

	// Token: 0x06004570 RID: 17776 RVA: 0x002A5408 File Offset: 0x002A4408
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1B7A()
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
		spr\u1B7A.ᡂ = new IgnoreErrorType[]
		{
			IgnoreErrorType.EmptyCellReferences,
			IgnoreErrorType.EvaluateToError,
			IgnoreErrorType.InconsistentFormula,
			IgnoreErrorType.NumberAsText,
			IgnoreErrorType.OmittedCells,
			IgnoreErrorType.TextDate,
			IgnoreErrorType.UnlockedFormulaCells
		};
		spr\u1B7A.\u1843 = new string[]
		{
			RecordTableEnumerator.b("帶弸ᬺ儼嘾⽀♂ń㕆⡈㱊⍌潎⅐㩒ⵔ㉖㕘᝚㑜ㅞѠ㑢౤ͦᵨͪ䵬彮", a_),
			RecordTableEnumerator.b("䐶䰸嘺ᴼ缾煀捂瑄杆祈", a_),
			RecordTableEnumerator.b("䐶䰸嘺ᴼ༾慀獂敄݆硈", a_),
			RecordTableEnumerator.b("䜶䬸吺夼Ἶŀ煂敄癆楈祊", a_),
			RecordTableEnumerator.b("䜶䬸吺夼Ἶŀ灂敄畆硈絊経罎煐⍒㱔⽖㱘㝚ੜ㙞ՠᝢ൤", a_),
			RecordTableEnumerator.b("䜶䬸吺夼Ἶŀ灂敄畆硈絊経罎煐⍒㱔⽖㱘㝚ᕜ㩞ࡠѢ൤፦", a_),
			RecordTableEnumerator.b("䐶䰸嘺ᴼ缾煀捂畄杆硈", a_),
			RecordTableEnumerator.b("䜶䬸吺夼Ἶŀ畂敄癆楈祊", a_),
			RecordTableEnumerator.b("䜶䬸吺夼Ἶŀ瑂敄畆硈絊経罎煐⍒㱔⽖㱘㝚ੜ㙞ՠᝢ൤", a_),
			RecordTableEnumerator.b("䐶䰸嘺ᴼ缾祀捂睄癆罈筊経潎慐", a_),
			RecordTableEnumerator.b("䜶䬸吺夼Ἶŀ瑂敄畆硈絊経罎煐⍒㱔⽖㱘㝚ᕜ㩞ࡠѢ൤፦", a_),
			RecordTableEnumerator.b("䐶䰸嘺ᴼ缾灀獂敄畆硈絊経罎煐捒", a_)
		};
		spr\u1B7A.ᡄ = RecordTableEnumerator.b("䀶倸唺夼倾㙀ᝂ⁄㽆㵈歊ᙌ㑎慐⹒ࡔ", a_);
		spr\u1B7A.ᡅ = new string[]
		{
			RecordTableEnumerator.b("制吸䬺䤼䘾ɀ♂⥄⭆ᭈ⹊⭌⩎⍐㙒㭔㑖㱘", a_),
			RecordTableEnumerator.b("制伸娺儼稾㍀ㅂ⩄㕆", a_),
			RecordTableEnumerator.b("儶嘸䤺值䨾ⵀ≂", a_),
			RecordTableEnumerator.b("夶䰸嘺弼娾㍀၂ㅄ⡆㭈⹊⥌๎≐ݒご⽖ⵘ", a_),
			RecordTableEnumerator.b("儶嘸䤺值䨾ⵀ≂ᝄ♆❈ⱊ⡌", a_),
			RecordTableEnumerator.b("䌶丸吺礼嘾♀⩂ㅄፆⱈ㍊㥌ᙎ㑐㉒❔", a_),
			RecordTableEnumerator.b("䈶圸场刼尾⩀♂⅄ņ♈㥊⁌㩎㵐㉒", a_)
		};
		spr\u1B7A.ᡆ = new char[]
		{
			'\n',
			'\r',
			'\t'
		};
	}

	// Token: 0x04001E61 RID: 7777
	private const int ᜀ = 8000;

	// Token: 0x04001E62 RID: 7778
	public const string ᜁ = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?>";

	// Token: 0x04001E63 RID: 7779
	public const string ᜂ = "http://schemas.openxmlformats.org/package/2006/content-types";

	// Token: 0x04001E64 RID: 7780
	public const string ᜃ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";

	// Token: 0x04001E65 RID: 7781
	public const string ᜄ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

	// Token: 0x04001E66 RID: 7782
	public const string ᜅ = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

	// Token: 0x04001E67 RID: 7783
	public const string ᜆ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet";

	// Token: 0x04001E68 RID: 7784
	public const string ᜇ = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet";

	// Token: 0x04001E69 RID: 7785
	public const string ᜈ = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties";

	// Token: 0x04001E6A RID: 7786
	public const string ᜉ = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";

	// Token: 0x04001E6B RID: 7787
	public const string ᜊ = "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main";

	// Token: 0x04001E6C RID: 7788
	public const string ᜋ = "{E15A36E0-9728-4e99-A89B-3F7291B0FE68}";

	// Token: 0x04001E6D RID: 7789
	public const string ᜌ = "http://schemas.microsoft.com/office/excel/2006/main";

	// Token: 0x04001E6E RID: 7790
	public const string \u170D = "{05C60535-1F16-4fd2-B633-F4F36F0B64E0}";

	// Token: 0x04001E6F RID: 7791
	public const string ᜎ = "mc";

	// Token: 0x04001E70 RID: 7792
	public const string ᜏ = "http://schemas.openxmlformats.org/markup-compatibility/2006";

	// Token: 0x04001E71 RID: 7793
	public const string ᜐ = "cp";

	// Token: 0x04001E72 RID: 7794
	public const string ᜑ = "http://purl.org/dc/elements/1.1/";

	// Token: 0x04001E73 RID: 7795
	public const string \u1712 = "dc";

	// Token: 0x04001E74 RID: 7796
	public const string \u1713 = "http://purl.org/dc/terms/";

	// Token: 0x04001E75 RID: 7797
	public const string \u1714 = "dcterms";

	// Token: 0x04001E76 RID: 7798
	public const string \u1715 = "http://purl.org/dc/dcmitype/";

	// Token: 0x04001E77 RID: 7799
	public const string \u1716 = "dcmitype";

	// Token: 0x04001E78 RID: 7800
	public const string \u1717 = "http://www.w3.org/2001/XMLSchema-instance";

	// Token: 0x04001E79 RID: 7801
	public const string \u1718 = "xsi";

	// Token: 0x04001E7A RID: 7802
	public const string \u1719 = "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties";

	// Token: 0x04001E7B RID: 7803
	public const string \u171A = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes";

	// Token: 0x04001E7C RID: 7804
	public const string \u171B = "application/vnd.openxmlformats-officedocument.oleObject";

	// Token: 0x04001E7D RID: 7805
	public const string \u171C = "bin";

	// Token: 0x04001E7E RID: 7806
	public const string \u171D = "vt";

	// Token: 0x04001E7F RID: 7807
	public const string \u171E = "r";

	// Token: 0x04001E80 RID: 7808
	public const string \u171F = "x14";

	// Token: 0x04001E81 RID: 7809
	public const string ᜠ = "xm";

	// Token: 0x04001E82 RID: 7810
	public const string ᜡ = "Types";

	// Token: 0x04001E83 RID: 7811
	public const string ᜢ = "Extension";

	// Token: 0x04001E84 RID: 7812
	public const string ᜣ = "Default";

	// Token: 0x04001E85 RID: 7813
	public const string ᜤ = "ContentType";

	// Token: 0x04001E86 RID: 7814
	public const string ᜥ = "Override";

	// Token: 0x04001E87 RID: 7815
	public const string ᜦ = "PartName";

	// Token: 0x04001E88 RID: 7816
	public const string ᜧ = "workbook";

	// Token: 0x04001E89 RID: 7817
	public const string ᜨ = "sheets";

	// Token: 0x04001E8A RID: 7818
	public const string ᜩ = "sheet";

	// Token: 0x04001E8B RID: 7819
	public const string ᜪ = "name";

	// Token: 0x04001E8C RID: 7820
	public const string ᜫ = "sheetId";

	// Token: 0x04001E8D RID: 7821
	public const string ᜬ = "id";

	// Token: 0x04001E8E RID: 7822
	public const string ᜭ = "Id";

	// Token: 0x04001E8F RID: 7823
	public const string ᜮ = "state";

	// Token: 0x04001E90 RID: 7824
	public const string ᜯ = "calcPr";

	// Token: 0x04001E91 RID: 7825
	public const string ᜰ = "calcId";

	// Token: 0x04001E92 RID: 7826
	public const string ᜱ = "tabSelected";

	// Token: 0x04001E93 RID: 7827
	public const string \u1732 = "hidden";

	// Token: 0x04001E94 RID: 7828
	public const string \u1733 = "veryHidden";

	// Token: 0x04001E95 RID: 7829
	public const string \u1734 = "visible";

	// Token: 0x04001E96 RID: 7830
	public const string \u1735 = "Relationships";

	// Token: 0x04001E97 RID: 7831
	public const string \u1736 = "Relationship";

	// Token: 0x04001E98 RID: 7832
	public const string \u1737 = "Type";

	// Token: 0x04001E99 RID: 7833
	public const string \u1738 = "Target";

	// Token: 0x04001E9A RID: 7834
	public const string \u1739 = "TargetMode";

	// Token: 0x04001E9B RID: 7835
	public const string \u173A = "External";

	// Token: 0x04001E9C RID: 7836
	public const string \u173B = "mergeCells";

	// Token: 0x04001E9D RID: 7837
	public const string \u173C = "count";

	// Token: 0x04001E9E RID: 7838
	public const string \u173D = "mergeCell";

	// Token: 0x04001E9F RID: 7839
	public const string \u173E = "ref";

	// Token: 0x04001EA0 RID: 7840
	public const string \u173F = "definedNames";

	// Token: 0x04001EA1 RID: 7841
	public const string ᝀ = "definedName";

	// Token: 0x04001EA2 RID: 7842
	public const string ᝁ = "name";

	// Token: 0x04001EA3 RID: 7843
	public const string ᝂ = "localSheetId";

	// Token: 0x04001EA4 RID: 7844
	public const string ᝃ = "styleSheet";

	// Token: 0x04001EA5 RID: 7845
	public const string ᝄ = "fonts";

	// Token: 0x04001EA6 RID: 7846
	public const string ᝅ = "font";

	// Token: 0x04001EA7 RID: 7847
	public const string ᝆ = "b";

	// Token: 0x04001EA8 RID: 7848
	public const string ᝇ = "i";

	// Token: 0x04001EA9 RID: 7849
	public const string ᝈ = "u";

	// Token: 0x04001EAA RID: 7850
	public const string ᝉ = "val";

	// Token: 0x04001EAB RID: 7851
	public const string ᝊ = "sz";

	// Token: 0x04001EAC RID: 7852
	public const string ᝋ = "strike";

	// Token: 0x04001EAD RID: 7853
	public const string ᝌ = "name";

	// Token: 0x04001EAE RID: 7854
	public const string ᝍ = "color";

	// Token: 0x04001EAF RID: 7855
	public const string ᝎ = "indexed";

	// Token: 0x04001EB0 RID: 7856
	public const string ᝏ = "theme";

	// Token: 0x04001EB1 RID: 7857
	public const string ᝐ = "tint";

	// Token: 0x04001EB2 RID: 7858
	public const string ᝑ = "rgb";

	// Token: 0x04001EB3 RID: 7859
	public const string \u1752 = "indexedColors";

	// Token: 0x04001EB4 RID: 7860
	public const string \u1753 = "colors";

	// Token: 0x04001EB5 RID: 7861
	public const string \u1754 = "rgbColor";

	// Token: 0x04001EB6 RID: 7862
	public const string \u1755 = "shadow";

	// Token: 0x04001EB7 RID: 7863
	public const string \u1756 = "vertAlign";

	// Token: 0x04001EB8 RID: 7864
	public const string \u1757 = "family";

	// Token: 0x04001EB9 RID: 7865
	public const string \u1758 = "charset";

	// Token: 0x04001EBA RID: 7866
	public const string \u1759 = "numFmts";

	// Token: 0x04001EBB RID: 7867
	public const string \u175A = "numFmt";

	// Token: 0x04001EBC RID: 7868
	public const string \u175B = "numFmtId";

	// Token: 0x04001EBD RID: 7869
	public const string \u175C = "formatCode";

	// Token: 0x04001EBE RID: 7870
	public const string \u175D = "fills";

	// Token: 0x04001EBF RID: 7871
	public const string \u175E = "fill";

	// Token: 0x04001EC0 RID: 7872
	public const string \u175F = "patternFill";

	// Token: 0x04001EC1 RID: 7873
	public const string ᝠ = "gradientFill";

	// Token: 0x04001EC2 RID: 7874
	public const string ᝡ = "type";

	// Token: 0x04001EC3 RID: 7875
	public const string ᝢ = "linear";

	// Token: 0x04001EC4 RID: 7876
	public const string ᝣ = "path";

	// Token: 0x04001EC5 RID: 7877
	public const string ᝤ = "degree";

	// Token: 0x04001EC6 RID: 7878
	public const string ᝥ = "bottom";

	// Token: 0x04001EC7 RID: 7879
	public const string ᝦ = "left";

	// Token: 0x04001EC8 RID: 7880
	public const string ᝧ = "right";

	// Token: 0x04001EC9 RID: 7881
	public const string ᝨ = "top";

	// Token: 0x04001ECA RID: 7882
	public const string ᝩ = "stop";

	// Token: 0x04001ECB RID: 7883
	public const string ᝪ = "position";

	// Token: 0x04001ECC RID: 7884
	public const string ᝫ = "patternType";

	// Token: 0x04001ECD RID: 7885
	public const string ᝬ = "bgColor";

	// Token: 0x04001ECE RID: 7886
	public const string \u176D = "fgColor";

	// Token: 0x04001ECF RID: 7887
	public const string ᝮ = "borders";

	// Token: 0x04001ED0 RID: 7888
	public const string ᝯ = "border";

	// Token: 0x04001ED1 RID: 7889
	public const string ᝰ = "style";

	// Token: 0x04001ED2 RID: 7890
	public const string \u1771 = "color";

	// Token: 0x04001ED3 RID: 7891
	public const string \u1772 = "worksheet";

	// Token: 0x04001ED4 RID: 7892
	public const string \u1773 = "dimension";

	// Token: 0x04001ED5 RID: 7893
	public const string \u1774 = "sheetData";

	// Token: 0x04001ED6 RID: 7894
	public const string \u1775 = "c";

	// Token: 0x04001ED7 RID: 7895
	public const string \u1776 = "cm";

	// Token: 0x04001ED8 RID: 7896
	public const string \u1777 = "ph";

	// Token: 0x04001ED9 RID: 7897
	public const string \u1778 = "r";

	// Token: 0x04001EDA RID: 7898
	public const string \u1779 = "s";

	// Token: 0x04001EDB RID: 7899
	public const string \u177A = "t";

	// Token: 0x04001EDC RID: 7900
	public const string \u177B = "vm";

	// Token: 0x04001EDD RID: 7901
	public const string \u177C = "f";

	// Token: 0x04001EDE RID: 7902
	public const string \u177D = "v";

	// Token: 0x04001EDF RID: 7903
	public const string \u177E = "is";

	// Token: 0x04001EE0 RID: 7904
	public const string \u177F = "rPr";

	// Token: 0x04001EE1 RID: 7905
	public const string ក = "rFont";

	// Token: 0x04001EE2 RID: 7906
	public const string ខ = "cols";

	// Token: 0x04001EE3 RID: 7907
	public const string គ = "col";

	// Token: 0x04001EE4 RID: 7908
	public const string ឃ = "min";

	// Token: 0x04001EE5 RID: 7909
	public const string ង = "max";

	// Token: 0x04001EE6 RID: 7910
	public const string ច = "width";

	// Token: 0x04001EE7 RID: 7911
	public const string ឆ = "style";

	// Token: 0x04001EE8 RID: 7912
	public const string ជ = "customWidth";

	// Token: 0x04001EE9 RID: 7913
	public const string ឈ = "bestFit";

	// Token: 0x04001EEA RID: 7914
	public const string ញ = "row";

	// Token: 0x04001EEB RID: 7915
	public const string ដ = "r";

	// Token: 0x04001EEC RID: 7916
	public const string ឋ = "ht";

	// Token: 0x04001EED RID: 7917
	public const string ឌ = "hidden";

	// Token: 0x04001EEE RID: 7918
	public const string ឍ = "customFormat";

	// Token: 0x04001EEF RID: 7919
	public const string ណ = "customHeight";

	// Token: 0x04001EF0 RID: 7920
	public const string ត = "collapsed";

	// Token: 0x04001EF1 RID: 7921
	public const string ថ = "outlineLevel";

	// Token: 0x04001EF2 RID: 7922
	public const string ទ = "thickBot";

	// Token: 0x04001EF3 RID: 7923
	public const string ធ = "thickTop";

	// Token: 0x04001EF4 RID: 7924
	public const string ន = "t";

	// Token: 0x04001EF5 RID: 7925
	public const string ប = "aca";

	// Token: 0x04001EF6 RID: 7926
	public const string ផ = "si";

	// Token: 0x04001EF7 RID: 7927
	public const string ព = "ref";

	// Token: 0x04001EF8 RID: 7928
	public const string ភ = "authors";

	// Token: 0x04001EF9 RID: 7929
	public const string ម = "author";

	// Token: 0x04001EFA RID: 7930
	public const string យ = "commentList";

	// Token: 0x04001EFB RID: 7931
	public const string រ = "comment";

	// Token: 0x04001EFC RID: 7932
	public const string ល = "text";

	// Token: 0x04001EFD RID: 7933
	public const string វ = "comments";

	// Token: 0x04001EFE RID: 7934
	public const string ឝ = "authorId";

	// Token: 0x04001EFF RID: 7935
	public const string ឞ = "n";

	// Token: 0x04001F00 RID: 7936
	public const string ស = "cellStyleXfs";

	// Token: 0x04001F01 RID: 7937
	public const string ហ = "cellXfs";

	// Token: 0x04001F02 RID: 7938
	public const string ឡ = "dxfs";

	// Token: 0x04001F03 RID: 7939
	public const string អ = "tableStyles";

	// Token: 0x04001F04 RID: 7940
	public const string ឣ = "xf";

	// Token: 0x04001F05 RID: 7941
	public const string ឤ = "fontId";

	// Token: 0x04001F06 RID: 7942
	public const string ឥ = "fillId";

	// Token: 0x04001F07 RID: 7943
	public const string ឦ = "borderId";

	// Token: 0x04001F08 RID: 7944
	public const string ឧ = "xfId";

	// Token: 0x04001F09 RID: 7945
	public const string ឨ = "cellStyles";

	// Token: 0x04001F0A RID: 7946
	public const string ឩ = "cellStyle";

	// Token: 0x04001F0B RID: 7947
	public const string ឪ = "builtinId";

	// Token: 0x04001F0C RID: 7948
	public const string ឫ = "iLevel";

	// Token: 0x04001F0D RID: 7949
	public const string ឬ = "applyAlignment";

	// Token: 0x04001F0E RID: 7950
	public const string ឭ = "applyBorder";

	// Token: 0x04001F0F RID: 7951
	public const string ឮ = "applyFont";

	// Token: 0x04001F10 RID: 7952
	public const string ឯ = "applyNumberFormat";

	// Token: 0x04001F11 RID: 7953
	public const string ឰ = "applyFill";

	// Token: 0x04001F12 RID: 7954
	public const string ឱ = "applyProtection";

	// Token: 0x04001F13 RID: 7955
	public const string ឲ = "alignment";

	// Token: 0x04001F14 RID: 7956
	public const string ឳ = "protection";

	// Token: 0x04001F15 RID: 7957
	public const string \u17B4 = "indent";

	// Token: 0x04001F16 RID: 7958
	public const string \u17B5 = "horizontal";

	// Token: 0x04001F17 RID: 7959
	public const string \u17B6 = "justifyLastLine";

	// Token: 0x04001F18 RID: 7960
	public const string \u17B7 = "readingOrder";

	// Token: 0x04001F19 RID: 7961
	public const string \u17B8 = "shrinkToFit";

	// Token: 0x04001F1A RID: 7962
	public const string \u17B9 = "textRotation";

	// Token: 0x04001F1B RID: 7963
	public const string \u17BA = "wrapText";

	// Token: 0x04001F1C RID: 7964
	public const string \u17BB = "vertical";

	// Token: 0x04001F1D RID: 7965
	public const string \u17BC = "hidden";

	// Token: 0x04001F1E RID: 7966
	public const string \u17BD = "locked";

	// Token: 0x04001F1F RID: 7967
	public const bool \u17BE = false;

	// Token: 0x04001F20 RID: 7968
	public const bool \u17BF = true;

	// Token: 0x04001F21 RID: 7969
	public const string \u17C0 = "quotePrefix";

	// Token: 0x04001F22 RID: 7970
	public const string \u17C1 = "diagonalDown";

	// Token: 0x04001F23 RID: 7971
	public const string \u17C2 = "diagonalUp";

	// Token: 0x04001F24 RID: 7972
	public const string \u17C3 = "sst";

	// Token: 0x04001F25 RID: 7973
	public const string \u17C4 = "uniqueCount";

	// Token: 0x04001F26 RID: 7974
	public const string \u17C5 = "si";

	// Token: 0x04001F27 RID: 7975
	public const string \u17C6 = "t";

	// Token: 0x04001F28 RID: 7976
	public const string \u17C7 = "r";

	// Token: 0x04001F29 RID: 7977
	public const string \u17C8 = "root";

	// Token: 0x04001F2A RID: 7978
	public const string \u17C9 = "space";

	// Token: 0x04001F2B RID: 7979
	public const string \u17CA = "xml";

	// Token: 0x04001F2C RID: 7980
	public const string \u17CB = "preserve";

	// Token: 0x04001F2D RID: 7981
	private const string \u17CC = "n";

	// Token: 0x04001F2E RID: 7982
	private const string \u17CD = "s";

	// Token: 0x04001F2F RID: 7983
	private const string \u17CE = "b";

	// Token: 0x04001F30 RID: 7984
	private const string \u17CF = "e";

	// Token: 0x04001F31 RID: 7985
	private const string \u17D0 = "str";

	// Token: 0x04001F32 RID: 7986
	private const string \u17D1 = "inlineStr";

	// Token: 0x04001F33 RID: 7987
	public const string \u17D2 = "theme";

	// Token: 0x04001F34 RID: 7988
	public const string \u17D3 = "themeElements";

	// Token: 0x04001F35 RID: 7989
	public const string \u17D4 = "clrScheme";

	// Token: 0x04001F36 RID: 7990
	public const string \u17D5 = "val";

	// Token: 0x04001F37 RID: 7991
	public const string \u17D6 = "sysClr";

	// Token: 0x04001F38 RID: 7992
	public const string \u17D7 = "val";

	// Token: 0x04001F39 RID: 7993
	public const string \u17D8 = "lastClr";

	// Token: 0x04001F3A RID: 7994
	public const string \u17D9 = "dxf";

	// Token: 0x04001F3B RID: 7995
	public const string \u17DA = "phoneticPr";

	// Token: 0x04001F3C RID: 7996
	public const string \u17DB = "phonetic";

	// Token: 0x04001F3D RID: 7997
	public const string ៜ = "hyperlinks";

	// Token: 0x04001F3E RID: 7998
	public const string \u17DD = "hyperlink";

	// Token: 0x04001F3F RID: 7999
	public const string \u17DE = "display";

	// Token: 0x04001F40 RID: 8000
	public const string \u17DF = "id";

	// Token: 0x04001F41 RID: 8001
	public const string ០ = "location";

	// Token: 0x04001F42 RID: 8002
	public const string ១ = "ref";

	// Token: 0x04001F43 RID: 8003
	public const string ២ = "tooltip";

	// Token: 0x04001F44 RID: 8004
	public const string ៣ = "sheetPr";

	// Token: 0x04001F45 RID: 8005
	public const string ៤ = "pageSetUpPr";

	// Token: 0x04001F46 RID: 8006
	public const string ៥ = "fitToPage";

	// Token: 0x04001F47 RID: 8007
	public const string ៦ = "tabColor";

	// Token: 0x04001F48 RID: 8008
	public const string ៧ = "outlinePr";

	// Token: 0x04001F49 RID: 8009
	public const string ៨ = "summaryBelow";

	// Token: 0x04001F4A RID: 8010
	public const string ៩ = "summaryRight";

	// Token: 0x04001F4B RID: 8011
	public const string \u17EA = "picture";

	// Token: 0x04001F4C RID: 8012
	public const string \u17EB = "file:///";

	// Token: 0x04001F4D RID: 8013
	public const string \u17EC = "http://";

	// Token: 0x04001F4E RID: 8014
	public const string \u17ED = "sheetFormatPr";

	// Token: 0x04001F4F RID: 8015
	public const string \u17EE = "zeroHeight";

	// Token: 0x04001F50 RID: 8016
	public const string \u17EF = "defaultRowHeight";

	// Token: 0x04001F51 RID: 8017
	public const string \u17F0 = "defaultColWidth";

	// Token: 0x04001F52 RID: 8018
	public const string \u17F1 = "baseColWidth";

	// Token: 0x04001F53 RID: 8019
	public const string \u17F2 = "thickBottom";

	// Token: 0x04001F54 RID: 8020
	public const string \u17F3 = "thickTop";

	// Token: 0x04001F55 RID: 8021
	public const string \u17F4 = "outlineLevelCol";

	// Token: 0x04001F56 RID: 8022
	public const string \u17F5 = "outlineLevelRow";

	// Token: 0x04001F57 RID: 8023
	public const string \u17F6 = "bookViews";

	// Token: 0x04001F58 RID: 8024
	public const string \u17F7 = "workbookView";

	// Token: 0x04001F59 RID: 8025
	public const string \u17F8 = "activeTab";

	// Token: 0x04001F5A RID: 8026
	public const string \u17F9 = "autoFilterDateGrouping";

	// Token: 0x04001F5B RID: 8027
	public const string \u17FA = "firstSheet";

	// Token: 0x04001F5C RID: 8028
	public const string \u17FB = "minimized";

	// Token: 0x04001F5D RID: 8029
	public const string \u17FC = "showHorizontalScroll";

	// Token: 0x04001F5E RID: 8030
	public const string \u17FD = "showSheetTabs";

	// Token: 0x04001F5F RID: 8031
	public const string \u17FE = "showVerticalScroll";

	// Token: 0x04001F60 RID: 8032
	public const string \u17FF = "tabRatio";

	// Token: 0x04001F61 RID: 8033
	public const string \u1800 = "visibility";

	// Token: 0x04001F62 RID: 8034
	public const string \u1801 = "windowHeight";

	// Token: 0x04001F63 RID: 8035
	public const string \u1802 = "windowWidth";

	// Token: 0x04001F64 RID: 8036
	public const string \u1803 = "xWindow";

	// Token: 0x04001F65 RID: 8037
	public const string \u1804 = "yWindow";

	// Token: 0x04001F66 RID: 8038
	public const string \u1805 = "rowBreaks";

	// Token: 0x04001F67 RID: 8039
	public const string \u1806 = "colBreaks";

	// Token: 0x04001F68 RID: 8040
	public const string \u1807 = "count";

	// Token: 0x04001F69 RID: 8041
	public const string \u1808 = "manualBreakCount";

	// Token: 0x04001F6A RID: 8042
	public const string \u1809 = "brk";

	// Token: 0x04001F6B RID: 8043
	public const string \u180A = "id";

	// Token: 0x04001F6C RID: 8044
	public const string \u180B = "man";

	// Token: 0x04001F6D RID: 8045
	public const string \u180C = "max";

	// Token: 0x04001F6E RID: 8046
	public const string \u180D = "min";

	// Token: 0x04001F6F RID: 8047
	public const string \u180E = "sheetViews";

	// Token: 0x04001F70 RID: 8048
	public const string \u180F = "sheetView";

	// Token: 0x04001F71 RID: 8049
	public const string ᠐ = "showZeros";

	// Token: 0x04001F72 RID: 8050
	public const string ᠑ = "workbookViewId";

	// Token: 0x04001F73 RID: 8051
	public const string ᠒ = "zoomScale";

	// Token: 0x04001F74 RID: 8052
	public const string ᠓ = "zoomScaleNormal";

	// Token: 0x04001F75 RID: 8053
	public const string ᠔ = "zoomScaleSheetLayoutView";

	// Token: 0x04001F76 RID: 8054
	public const string ᠕ = "zoomScalePageLayoutView";

	// Token: 0x04001F77 RID: 8055
	public const string ᠖ = "view";

	// Token: 0x04001F78 RID: 8056
	public const string ᠗ = "pageLayout";

	// Token: 0x04001F79 RID: 8057
	public const string ᠘ = "pageBreakPreview";

	// Token: 0x04001F7A RID: 8058
	public const string ᠙ = "normal";

	// Token: 0x04001F7B RID: 8059
	public const string \u181A = "1";

	// Token: 0x04001F7C RID: 8060
	public const string \u181B = "0";

	// Token: 0x04001F7D RID: 8061
	public const string \u181C = "column";

	// Token: 0x04001F7E RID: 8062
	public const string \u181D = "stacked";

	// Token: 0x04001F7F RID: 8063
	public const string \u181E = "gap";

	// Token: 0x04001F80 RID: 8064
	public const string \u181F = "zero";

	// Token: 0x04001F81 RID: 8065
	public const string ᠠ = "span";

	// Token: 0x04001F82 RID: 8066
	public const string ᠡ = "custom";

	// Token: 0x04001F83 RID: 8067
	public const string ᠢ = "group";

	// Token: 0x04001F84 RID: 8068
	public const string ᠣ = "showGridLines";

	// Token: 0x04001F85 RID: 8069
	public const string ᠤ = "rightToLeft";

	// Token: 0x04001F86 RID: 8070
	public const string ᠥ = "defaultGridColor";

	// Token: 0x04001F87 RID: 8071
	public const string ᠦ = "colorId";

	// Token: 0x04001F88 RID: 8072
	public const string ᠧ = "customProperties";

	// Token: 0x04001F89 RID: 8073
	public const string ᠨ = "customPr";

	// Token: 0x04001F8A RID: 8074
	public const string ᠩ = "ignoredErrors";

	// Token: 0x04001F8B RID: 8075
	public const string ᠪ = "ignoredError";

	// Token: 0x04001F8C RID: 8076
	public const string ᠫ = "OLEUPDATE_ONCALL";

	// Token: 0x04001F8D RID: 8077
	public const string ᠬ = "OLEUPDATE_ALWAYS";

	// Token: 0x04001F8E RID: 8078
	public const string ᠭ = "sqref";

	// Token: 0x04001F8F RID: 8079
	public const string ᠮ = "fileVersion";

	// Token: 0x04001F90 RID: 8080
	public const string ᠯ = "rupBuild";

	// Token: 0x04001F91 RID: 8081
	public const string ᠰ = "lastEdited";

	// Token: 0x04001F92 RID: 8082
	public const string ᠱ = "lowestEdited";

	// Token: 0x04001F93 RID: 8083
	public const string ᠲ = "workbookPr";

	// Token: 0x04001F94 RID: 8084
	public const string ᠳ = "date1904";

	// Token: 0x04001F95 RID: 8085
	public const string ᠴ = "fullPrecision";

	// Token: 0x04001F96 RID: 8086
	public const string ᠵ = "appName";

	// Token: 0x04001F97 RID: 8087
	public const string ᠶ = "xl";

	// Token: 0x04001F98 RID: 8088
	private const string ᠷ = "windowProtection";

	// Token: 0x04001F99 RID: 8089
	public const string ᠸ = "functionGroups";

	// Token: 0x04001F9A RID: 8090
	public const string ᠹ = "codeName";

	// Token: 0x04001F9B RID: 8091
	private const string ᠺ = "spans";

	// Token: 0x04001F9C RID: 8092
	public const string ᠻ = "extLst";

	// Token: 0x04001F9D RID: 8093
	public const string ᠼ = "ext";

	// Token: 0x04001F9E RID: 8094
	public const string ᠽ = "ca";

	// Token: 0x04001F9F RID: 8095
	private const int ᠾ = 32;

	// Token: 0x04001FA0 RID: 8096
	public const string ᠿ = "transitionEvaluation";

	// Token: 0x04001FA1 RID: 8097
	public const string ᡀ = "showRowColHeaders";

	// Token: 0x04001FA2 RID: 8098
	private const string ᡁ = "12.0000";

	// Token: 0x04001FA3 RID: 8099
	public static IgnoreErrorType[] ᡂ;

	// Token: 0x04001FA4 RID: 8100
	private static string[] \u1843;

	// Token: 0x04001FA5 RID: 8101
	private static string ᡄ;

	// Token: 0x04001FA6 RID: 8102
	public static string[] ᡅ;

	// Token: 0x04001FA7 RID: 8103
	private static readonly char[] ᡆ;

	// Token: 0x04001FA8 RID: 8104
	private XlsWorkbook ᡇ;

	// Token: 0x04001FA9 RID: 8105
	private FormulaUtil ᡈ;

	// Token: 0x04001FAA RID: 8106
	private RecordExtractor ᡉ;

	// Token: 0x04001FAB RID: 8107
	private Dictionary<int, spr\u2175> ᡊ;

	// Token: 0x04001FAC RID: 8108
	private Dictionary<int, spr\u2175> ᡋ;

	// Token: 0x04001FAD RID: 8109
	private Dictionary<Type, spr\u2175> ᡌ;

	// Token: 0x04001FAE RID: 8110
	private List<Stream> ᡍ;

	// Token: 0x04001FAF RID: 8111
	private XlsWorksheet ᡎ;

	// Token: 0x02000469 RID: 1129
	public enum CellType
	{
		// Token: 0x04001FB1 RID: 8113
		b,
		// Token: 0x04001FB2 RID: 8114
		e,
		// Token: 0x04001FB3 RID: 8115
		inlineStr,
		// Token: 0x04001FB4 RID: 8116
		n,
		// Token: 0x04001FB5 RID: 8117
		s,
		// Token: 0x04001FB6 RID: 8118
		str
	}

	// Token: 0x0200046A RID: 1130
	public enum FormulaType
	{
		// Token: 0x04001FB8 RID: 8120
		array,
		// Token: 0x04001FB9 RID: 8121
		dataTable,
		// Token: 0x04001FBA RID: 8122
		normal,
		// Token: 0x04001FBB RID: 8123
		shared
	}

	// Token: 0x0200046B RID: 1131
	// (Invoke) Token: 0x06004572 RID: 17778
	private delegate void ᜀ(XmlWriter A_0, string A_1, SheetProtectionType A_2, bool A_3, SheetProtectionType A_4);
}
