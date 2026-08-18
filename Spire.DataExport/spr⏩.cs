using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.PDF;
using Spire.DataExport.ResourceMgr;

// Token: 0x02000045 RID: 69
internal class spr\u23E9
{
	// Token: 0x06000228 RID: 552 RVA: 0x000138E0 File Offset: 0x000128E0
	public spr\u23E9(ExportBase A_0, Stream A_1)
	{
		int a_ = 0;
		this.ᜂ = new StringListCollection();
		this.ᜃ = new ArrayList();
		this.ᜄ = new SortedList();
		this.ᜅ = new PdfFont();
		this.ᜈ = 1.0;
		this.ᜉ = 1.0;
		this.ᜊ = Color.Black;
		this.ᜋ = Color.Black;
		this.ᜎ = CultureInfo.CurrentCulture;
		base..ctor();
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		if (this.ᜀ != null)
		{
			this.ᜎ = new CultureInfo(this.ᜀ.Culture.LCID);
		}
		this.ᜎ.NumberFormat.NumberDecimalSeparator = HyperlinksCollectionEditor.b("㈛", a_);
		this.ᜌ = new sprᶆ();
		this.ᜌ.ᜂ(0);
		this.ᜌ.ᜀ(0);
		this.ᜌ.ᜃ(612);
		this.ᜌ.ᜁ(792);
		this.\u170D = new sprᶆ();
		this.\u170D.ᜂ(50);
		this.\u170D.ᜀ(50);
		this.\u170D.ᜃ(562);
		this.\u170D.ᜁ(742);
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00013A44 File Offset: 0x00012A44
	private int ᜅ()
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
		this.ᜇ++;
		this.ᜃ.Add((int)this.ᜁ.Length);
		return this.ᜇ;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00013AB0 File Offset: 0x00012AB0
	private string ᜀ(string A_0, params object[] A_1)
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
		return string.Format(this.ᜎ, A_0, A_1);
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00013AF8 File Offset: 0x00012AF8
	private void ᜁ(string A_0)
	{
		int a_ = 14;
		for (;;)
		{
			this.ᜁ.Seek(0L, SeekOrigin.End);
			if (this.ᜀ == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_4C;
			}
		}
		throw new NullReferenceException(HyperlinksCollectionEditor.b("✩☫縭启吱瀳夵嬷伹儻嬽⸿㙁繃籅὇㡉╋㩍㕏ő⁓⑕橗ਖ਼ᡛᡝ䱟ᑡգᑥ剧㕩ͫᥭṯ᝱ٳ", a_));
		IL_4C:
		if (true)
		{
		}
		if (false)
		{
		}
		byte[] bytes = (this.ᜀ as TextExport).CurrentEncoding.GetBytes(A_0);
		this.ᜁ.Write(bytes, 0, bytes.Length);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00013B90 File Offset: 0x00012B90
	private void ᜄ()
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜁ(HyperlinksCollectionEditor.b("ࠬ缮田甲ᠴض᜸࠺〼", a_));
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00013BE8 File Offset: 0x00012BE8
	private int ᜁ(int A_0, int A_1)
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
		int num = this.ᜅ();
		this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("唭/伱ᐳصᠷ唹帻吽䴿繁硃晅杇ṉ㕋㹍㕏牑筓ᕕ㥗⹙㵛㉝ཟա䑣步䝧㩩൫७ᕯű味൵䥷ݹ屻乽ꁿ킁ꒃ讅ꞇ얉曆揄ﲏﮑ望몙겝\udd9f芡钣蚥盛誩ꆫ邭躯뾱톳\ud8b5\udcb7햹\udebb풽춿", a_), new object[]
		{
			num,
			A_0,
			A_1
		}));
		return num;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00013C74 File Offset: 0x00012C74
	private int ᜃ()
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int num = this.ᜅ();
		this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("匧ᨩ儫อ/ሱ嬳吵刷㜹;Ƚ怿流၃㽅㡇⽉汋慍὏❑⁓㩕ㅗ㑙㥛ⵝ浟䵡❣॥ᵧѩᡫ乭䁯罱䩳䡵畷όቻ᩽讅", a_), new object[]
		{
			num
		}));
		return num;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00013CEC File Offset: 0x00012CEC
	private string ᜀ(string A_0, Font A_1)
	{
		int a_ = 19;
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder(A_0);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if ((A_1.Style & FontStyle.Italic) != FontStyle.Italic)
					{
						goto IL_161;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 1:
					if ((A_1.Style & FontStyle.Bold) == FontStyle.Bold)
					{
						goto IL_57;
					}
					goto IL_B2;
				case 2:
					goto IL_6B;
				case 3:
					num = 7;
					continue;
				case 4:
					stringBuilder.Append(HyperlinksCollectionEditor.b("̮猰尲头匶", a_));
					num = 2;
					continue;
				case 5:
					stringBuilder.Append(HyperlinksCollectionEditor.b("̮猰尲头匶瀸伺尼匾⡀⁂", a_));
					num = 9;
					continue;
				case 6:
					stringBuilder.Append(HyperlinksCollectionEditor.b("̮砰䜲吴嬶倸堺", a_));
					num = 8;
					continue;
				case 7:
					if ((A_1.Style & FontStyle.Italic) == FontStyle.Italic)
					{
						num = 5;
						continue;
					}
					goto IL_B2;
				case 8:
					goto IL_118;
				case 9:
					goto IL_13A;
				case 10:
					if ((A_1.Style & FontStyle.Bold) == FontStyle.Bold)
					{
						num = 4;
						continue;
					}
					goto IL_6B;
				}
				break;
				IL_57:
				num = 3;
				continue;
				IL_6B:
				num = 0;
				continue;
				IL_B2:
				num = 10;
			}
		}
		IL_118:
		IL_13A:
		IL_161:
		return stringBuilder.ToString();
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00013E60 File Offset: 0x00012E60
	private string ᜀ(string A_0)
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		StringBuilder stringBuilder = new StringBuilder(A_0);
		stringBuilder.Replace(HyperlinksCollectionEditor.b("Ԥ", a_), string.Empty);
		return stringBuilder.ToString();
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00013ECC File Offset: 0x00012ECC
	private int ᜂ()
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder(this.ᜄ.Count);
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					int num3;
					PdfFont pdfFont;
					switch (num2)
					{
					case 0:
						goto IL_251;
					case 1:
						goto IL_194;
					case 2:
						goto IL_203;
					case 3:
						goto IL_1BD;
					case 4:
						goto IL_251;
					case 5:
					{
						if (true)
						{
						}
						StringBuilder stringBuilder2;
						this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("儩ᰫ匭ုȱᐳ夵娷倹ㄻȽ簿扁歃ቅㅇ㩉⥋湍罏ᑑ㭓㡕ⱗ坙獛൝ᕟaၣὥᡧཀྵ䱫䅭⑯qų፵ⱷ͹౻᭽赿궁쪃겋ꆍꎑ鮕랗\ud899ﶛ얟쮣좥\udca7誩莫햭芯쾱릳馵ﶷ풹\udfbb톽꒿ꯁ꫃ꇅ럋﷍귏\udfd1ﯓ郕뇗꣙꿛ꫝꏟ諡藣铥죧\udae9쇭볯鏱蟳苵믷鋹鷻賽⃿、㄃㌅ԇ┉嬋服琏昑簓攕㠗䄙望⨝崟缡⤣थ渧䔩䈫娭琯圱䜳唵䨷匹䰻䨽⼿ぁ摃穅瑇敉ᡋ㝍⁏㝑瑓祕ṗ㕙㉛⩝⑟ݡᝣեᩧͩᱫᩭὯq味奵㹷ᙹᵻ᥽ꊁ랃뒅ꢇꖉ쪋ﺏ횓풕벛얝邟芡钣蚥颧誩鲫躯貱릳袵蚷랹\ud9bb킽꒿귁ꛃ곅엇", a_), new object[]
						{
							num3,
							pdfFont.FontName,
							this.ᜀ(this.ᜀ(pdfFont.CustomFont.Name), pdfFont.CustomFont),
							sprᤓ.\u1718[(int)pdfFont.Encoding],
							stringBuilder2.ToString()
						}));
						num2 = 8;
						continue;
					}
					case 6:
						if (pdfFont.AllowCustomFont)
						{
							num2 = 10;
							continue;
						}
						goto IL_71;
					case 7:
						if (num >= this.ᜄ.Count)
						{
							num2 = 3;
							continue;
						}
						pdfFont = (this.ᜄ.GetByIndex(num) as PdfFont);
						num3 = this.ᜅ();
						num2 = 6;
						continue;
					case 8:
						goto IL_203;
					case 9:
						goto IL_194;
					case 10:
					{
						StringBuilder stringBuilder2 = new StringBuilder(pdfFont.ReturnFontLength() * 2);
						int num4 = 0;
						num2 = 4;
						continue;
					}
					case 11:
					{
						int num4;
						if (num4 < pdfFont.ReturnFontLength())
						{
							StringBuilder stringBuilder2;
							stringBuilder2.Append(((int)((float)pdfFont.GetWidth(num4) * 1000f / pdfFont.CustomFont.Size)).ToString());
							stringBuilder2.Append(' ');
							num4++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_71;
						default:
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						break;
					}
					}
					break;
					IL_71:
					this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("儩ᰫ匭ုȱᐳ夵娷倹ㄻȽ簿扁歃ቅㅇ㩉⥋湍罏ᑑ㭓㡕ⱗ坙獛൝ᕟaၣὥᡧཀྵ䱫䅭⑯ୱѳ፵䥷睹卻ぽꚅꞇ붋鶏붑횓ﾙ\uda9b캟횡蒣覥펧颩톫ꎭ龯\udab3햵ힷ\udeb9햻킽ꞿ뷅﯇량쇋雏믑ꛓꗕ곗駙듛뿝鋟싡퓣쟧ꛩ跫鷭蓯뇱鳳韵諷\udaf9컻쯽㗿༁㨃㠅ԇ漉戋樍缏瀑縓ᬕ", a_), new object[]
					{
						num3,
						pdfFont.FontName,
						sprᤓ.\u1717[(int)pdfFont.PdfFontName],
						sprᤓ.\u1718[(int)pdfFont.Encoding]
					}));
					num2 = 2;
					continue;
					IL_194:
					num2 = 7;
					continue;
					IL_203:
					stringBuilder.Append(this.ᜀ(HyperlinksCollectionEditor.b("ԩ圫ḭ䴯ሱ伳ܵ䔷ᨹ఻ḽሿ扁", a_), new object[]
					{
						pdfFont.FontName,
						num3
					}));
					num++;
					num2 = 9;
					continue;
					IL_251:
					num2 = 11;
				}
			}
			IL_1BD:
			int num5 = this.ᜅ();
			this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("儩ᰫ匭ုȱᐳ夵娷倹ㄻȽ簿扁歃E❇⑉㡋湍汏湑⽓杕╗穙扛恝䁟䵡㑣ᑥݧ३㽫୭ѯ剱⽳噵坷⩹㡻㡽ꁿ궁킃ﺉ겋펍낏겑ꪓ鮕ﶗ슟좡ꦣ", a_), new object[]
			{
				num5,
				stringBuilder.ToString()
			}));
			return num5;
		}
		}
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00014200 File Offset: 0x00013200
	private int ᜀ(int A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num3;
			for (;;)
			{
				StringBuilder stringBuilder = new StringBuilder(this.ᜂ.Count);
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_235;
					case 1:
						goto IL_261;
					case 2:
						goto IL_235;
					case 3:
						return num3;
					case 4:
						if (num < this.ᜂ.Count)
						{
							stringBuilder.AppendFormat(this.ᜎ, HyperlinksCollectionEditor.b("崥ᠧ圩ఫḭု怱ᐳ", a_), new object[]
							{
								this.ᜇ + 2 + num
							});
							num++;
							num2 = 6;
							continue;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return num3;
						default:
							if (false)
							{
							}
							num2 = 7;
							continue;
						}
						break;
					case 5:
					{
						int num4;
						if (num4 >= this.ᜂ.Count)
						{
							num2 = 3;
							continue;
						}
						int num5 = this.ᜅ();
						this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("崥ᠧ圩ఫḭု崱嘳尵㔷ع;ḽ漿ᙁ㵃㙅ⵇ橉捋ṍㅏ㕑ㅓ孕睗ਖ਼㵛ⱝ՟ౡၣ䙥፧孩ᅫ乭䁯剱♳筵坷⡹᥻ൽ黎겋ꊏ뒓ꚕ뢗좙醛놝잡삣쾥즧쎫횭邯쾳薵얷骹잻誽붿뿃뗇럋귏近\ud9d3崙賗꣙뗛돝ꋟ跡鳣웥돧釩\udbeb鏭탯觱쳳诵\ud8f7臹엻菽⃿礁㔃㘅甇圉ċℍ匏紑稓戕紗琙栛洝\u001f夡ᔣᜥ唧✩ራိ㴯圱娳刵圷堹嘻㌽", a_), new object[]
						{
							num5,
							num3,
							A_0,
							this.ᜌ.ᜃ(),
							this.ᜌ.ᜀ(),
							this.ᜌ.ᜄ(),
							this.ᜌ.ᜁ(),
							this.\u170D.ᜃ(),
							this.\u170D.ᜀ(),
							this.\u170D.ᜄ(),
							this.\u170D.ᜁ(),
							this.ᜂ[num4]
						}));
						num4++;
						num2 = 2;
						continue;
					}
					case 6:
						goto IL_261;
					case 7:
					{
						string text = stringBuilder.ToString();
						num3 = this.ᜅ();
						this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("崥ᠧ圩ఫḭု崱嘳尵㔷ع;ḽ漿ᙁ㵃㙅ⵇ橉捋ṍㅏ㕑ㅓ╕啗留Ὓㅝᕟౡၣ䙥፧孩ᅫ捭彯㥱ᵳት୷婹❻ս뉿ﾁ\ud983讅뚇뒉膋ﺏﮓ鞙", a_), new object[]
						{
							num3,
							this.ᜂ.Count,
							text
						}));
						int num4 = 0;
						num2 = 0;
						continue;
					}
					}
					break;
					IL_235:
					num2 = 5;
					continue;
					IL_261:
					num2 = 4;
				}
			}
			return num3;
		}
		}
	}

	// Token: 0x06000233 RID: 563 RVA: 0x000144C4 File Offset: 0x000134C4
	private int ᜁ()
	{
		int a_ = 14;
		if (true)
		{
		}
		int result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				result = (int)this.ᜁ.Position;
				this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("利師䬭嘯㼱гᘵ䌷ਹ䄻㌽瀿牁瑃癅硇穉籋繍恏扑瑓恕浗潙潛歝䁟ѡ䑣步", a_), new object[]
				{
					this.ᜇ + 1
				}));
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 2:
							goto IL_11D;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							int num2 = (int)enumerator.Current;
							this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("儩ᰫȭįȱำ刵षਹ䄻ḽ瀿牁瑃癅硇橉≋湍嵏", a_), new object[]
							{
								num2
							}));
							num = 3;
							continue;
						}
						}
						IL_F5:
						num = 4;
						continue;
						goto IL_F5;
					}
					IL_11D:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_166;
							case 2:
								goto IL_164;
							}
							break;
						}
					}
					IL_164:
					IL_166:;
				}
				break;
			}
			}
			break;
		}
		return result;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00014654 File Offset: 0x00013654
	private void ᜀ(int A_0, int A_1)
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
		this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("匦嬨䨪䐬䌮吰䄲㠴ଶԸᬺሼ氾⡀㥂⁄杆㉈筊が䉎繐Œ㩔㡖ⵘ筚♜湞ᱠ䍢啤䝦㭨晪卬兮籰rŴᙶ୸ེռൾ袄ﲆ뮈肌", a_), new object[]
		{
			this.ᜇ + 1,
			A_0,
			A_1
		}));
	}

	// Token: 0x06000235 RID: 565 RVA: 0x000146DC File Offset: 0x000136DC
	private void ᜀ()
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
		this.ᜁ(HyperlinksCollectionEditor.b("ଭ甯紱爳", a_));
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00014734 File Offset: 0x00013734
	public void ᜇ()
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
		this.ᜂ.Clear();
		this.ᜃ.Clear();
		this.ᜇ = 0;
		this.ᜄ.Clear();
		this.ᜈ = 1.0;
		this.ᜉ = 1.0;
		this.ᜊ = Color.Black;
		this.ᜋ = Color.Black;
		this.ᜄ();
	}

	// Token: 0x06000237 RID: 567 RVA: 0x000147D4 File Offset: 0x000137D4
	public void ᜉ()
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
		this.ᜋ();
		int a_ = this.ᜃ();
		int a_2 = this.ᜂ();
		int a_3 = this.ᜀ(a_2);
		int a_4 = this.ᜁ(a_3, a_);
		int a_5 = this.ᜁ();
		this.ᜀ(a_4, a_5);
		this.ᜀ();
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0001484C File Offset: 0x0001384C
	public void ᜂ(PdfFont A_0)
	{
		int a_ = 2;
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_4A;
		}
		if (false)
		{
		}
		if (this.ᜄ.ContainsKey(A_0.FontName))
		{
			throw new Exception(this.ᜀ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("圝丟吡䔣䨥䄧丩挫席唯䀱唳䈵儷唹刻愽ؿⵁ⩃㉅േ㉉╋㵍⑏", a_)), new object[]
			{
				A_0.FontName
			}));
		}
		IL_4A:
		this.ᜄ.Add(A_0.FontName, A_0);
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000148EC File Offset: 0x000138EC
	public void ᜈ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜋ();
		int num = this.ᜅ();
		this.ᜂ.Add(this.ᜀ(HyperlinksCollectionEditor.b("攝ဟ弡Уᘥࠧ砩", a_), new object[]
		{
			num
		}));
		this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("攝ဟ弡Уᘥࠧ䔩丫䐭㴯ั࠳ᘵ᜷瘹夻倽✿㙁ⱃ晅㍇等ㅋ湍恏牑ٓ癕晗摙兛ⵝᑟၡţݥէ杩", a_), new object[]
		{
			num,
			num + 1
		}));
		this.ᜆ = (int)this.ᜁ.Position;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x000149B8 File Offset: 0x000139B8
	public void ᜋ()
	{
		int a_ = 4;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
			{
				int num2 = (int)(this.ᜁ.Position - (long)this.ᜆ);
				this.ᜁ(HyperlinksCollectionEditor.b("䔟䰡䀣唥尧堩䤫伭崯㼱儳堵尷唹帻吽䴿", a_));
				this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("嬟ሡ夣إᠧ਩䌫䰭娯㼱伳ܵ䔷ᨹㄻ嬽⸿♁⭃⑅≇䝉", a_), new object[]
				{
					this.ᜅ(),
					num2
				}));
				num = 1;
				continue;
			}
			}
			if (true)
			{
			}
			if (this.ᜂ.Count <= 0)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00014AA4 File Offset: 0x00013AA4
	public void ᜀ(int A_0, int A_1, string A_2, Color A_3)
	{
		int a_ = 9;
		for (;;)
		{
			Color a_2 = Color.Black;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_3 != this.ᜋ)
					{
						num = 4;
						continue;
					}
					goto IL_1E0;
				case 1:
					goto IL_145;
				case 2:
					this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("朤猦␨Ъ嘬Ἦ䰰ጲ临ض䐸ᬺ椼夾䱀㡂睄㩆楈お繌㉎煐ݒㅔ睖煘⁚楜≞䡠䍢ㅤ൦摨⹪㥬扮", a_), new object[]
					{
						this.ᜅ.FontName,
						this.ᜅ.CustomFont.Size,
						A_0,
						A_1,
						A_2
					}));
					num = 6;
					continue;
				case 3:
					if (this.ᜅ.AllowCustomFont)
					{
						num = 2;
						continue;
					}
					this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("朤猦␨Ъ嘬Ἦ䰰ጲ临ض䐸ᬺ椼夾䱀㡂睄㩆楈お繌㉎煐ݒㅔ睖煘⁚楜≞䡠䍢ㅤ൦摨⹪㥬扮", a_), new object[]
					{
						this.ᜅ.FontName,
						this.ᜅ.Size,
						A_0,
						A_1,
						A_2
					}));
					num = 9;
					continue;
				case 4:
					goto IL_88;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						a_2 = this.ᜀ(A_3);
						num = 1;
						continue;
					}
					break;
				case 6:
					goto IL_66;
				case 7:
					goto IL_140;
				case 8:
					if (A_3 != this.ᜋ)
					{
						num = 5;
						continue;
					}
					goto IL_145;
				case 9:
					goto IL_66;
				}
				break;
				IL_66:
				num = 0;
				continue;
				IL_88:
				this.ᜀ(a_2);
				num = 7;
				continue;
				IL_145:
				num = 3;
			}
		}
		IL_140:
		IL_1E0:
		if (true)
		{
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00014C9C File Offset: 0x00013C9C
	public void ᜀ(int A_0, int A_1, StringListCollection A_2)
	{
		for (;;)
		{
			int num = 0;
			if (true)
			{
			}
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					this.ᜀ(A_0, A_1 - (int)((double)num * this.ᜉ * (double)this.ᜅ.CustomFont.Size), A_2[num], this.ᜋ);
					num2 = 7;
					continue;
				case 2:
					if (this.ᜅ.AllowCustomFont)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ(A_0, A_1 - (int)((double)num * this.ᜉ * (double)this.ᜅ.Size), A_2[num], this.ᜋ);
					num2 = 6;
					continue;
				case 3:
					goto IL_A6;
				case 4:
					goto IL_A6;
				case 5:
					if (num >= A_2.Count)
					{
						num2 = 0;
						continue;
					}
					num2 = 2;
					continue;
				case 6:
					goto IL_3C;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						if (false)
						{
						}
						goto IL_3C;
					}
					break;
				}
				break;
				IL_3C:
				num++;
				num2 = 4;
				continue;
				IL_A6:
				num2 = 5;
			}
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00014DD8 File Offset: 0x00013DD8
	public void ᜀ(double A_0, double A_1, double A_2, double A_3, Color A_4)
	{
		int a_ = 10;
		for (;;)
		{
			Color a_2 = Color.Black;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (A_4 != this.ᜊ)
					{
						num = 5;
						continue;
					}
					goto IL_68;
				case 2:
					goto IL_C8;
				case 3:
					this.ᜁ(a_2);
					num = 0;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C8;
					default:
						if (false)
						{
						}
						goto IL_68;
					}
					break;
				case 5:
					a_2 = this.ᜁ(A_4);
					num = 4;
					continue;
				}
				break;
				IL_68:
				this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("崥ᠧဩ䨫Ἥ䴯ሱ伳ܵȷ尹഻䌽怿⽁摃䭅㍇硉癋⡍慏⽑瑓ⵕ歗恙㩛潝ᵟ䉡ࡣ䙥敧ᅩ填呭ᙯ䍱ॳ噵ཷ婹煻⵽赿", a_), new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					this.ᜈ
				}));
				num = 2;
				continue;
				IL_C8:
				if (!(A_4 != this.ᜊ))
				{
					return;
				}
				if (true)
				{
				}
				num = 3;
			}
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00014F14 File Offset: 0x00013F14
	public void ᜁ(double A_0, double A_1, double A_2, double A_3, Color A_4)
	{
		int a_ = 7;
		for (;;)
		{
			Color a_2 = Color.Black;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (A_4 != this.ᜋ)
					{
						num = 3;
						continue;
					}
					goto IL_68;
				case 2:
					this.ᜀ(a_2);
					num = 0;
					continue;
				case 3:
					a_2 = this.ᜀ(A_4);
					num = 5;
					continue;
				case 4:
					goto IL_C4;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C4;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_68;
					}
					break;
				}
				break;
				IL_68:
				this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("堢ᔤᴦ伨ᨪ倬༮䨰Ȳ༴儶࠸䘺ᴼ䐾獀祂⍄癆㑈歊㙌籎歐㕒摔⩖祘⥚㡜罞᩠坢ᡤ橦", a_), new object[]
				{
					A_0,
					A_1,
					A_2,
					A_3,
					'f'
				}));
				num = 4;
				continue;
				IL_C4:
				if (!(A_4 != this.ᜋ))
				{
					return;
				}
				num = 2;
			}
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0001504C File Offset: 0x0001404C
	public void ᜁ(PdfFont A_0)
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
		this.ᜅ = (A_0.Clone() as PdfFont);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00015098 File Offset: 0x00014098
	public Color ᜀ(Color A_0)
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
		Color result = this.ᜋ;
		byte r = A_0.R;
		byte g = A_0.G;
		byte b = A_0.B;
		this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("搞ᄠ帢Ԥ尦ᠨ嘪ബ吮̰串ᔴ䔶常㘺", a_), new object[]
		{
			(int)(r / byte.MaxValue),
			(int)(g / byte.MaxValue),
			(int)(b / byte.MaxValue)
		}));
		this.ᜋ = A_0;
		return result;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00015158 File Offset: 0x00014158
	public Color ᜁ(Color A_0)
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Color result = this.ᜊ;
		byte r = A_0.R;
		byte g = A_0.G;
		byte b = A_0.B;
		this.ᜁ(this.ᜀ(HyperlinksCollectionEditor.b("堢ᔤᴦ伨ᤪ倬༮䨰Ȳ༴儶ସ䘺ᴼ䐾獀祂⍄畆㑈歊Ὄࡎ屐", a_), new object[]
		{
			(float)r / 255f,
			(float)g / 255f,
			(float)b / 255f
		}));
		this.ᜊ = A_0;
		return result;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0001521C File Offset: 0x0001421C
	public CultureInfo \u1712()
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
		return this.ᜎ;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00015260 File Offset: 0x00014260
	public void ᜀ(CultureInfo A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜎ = A_0;
				num = 1;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				default:
					goto IL_68;
				}
				break;
			case 2:
				num = 4;
				continue;
			case 4:
				if (A_0 != this.ᜎ)
				{
					goto IL_81;
				}
				return;
			}
			if (A_0 != null)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			return;
			IL_81:
			num = 0;
		}
		IL_68:
		if (false)
		{
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x000152F8 File Offset: 0x000142F8
	public int \u170D()
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

	// Token: 0x06000245 RID: 581 RVA: 0x0001533C File Offset: 0x0001433C
	public PdfFont ᜐ()
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

	// Token: 0x06000246 RID: 582 RVA: 0x00015380 File Offset: 0x00014380
	public void ᜀ(PdfFont A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜅ = A_0;
				num = 1;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				default:
					goto IL_68;
				}
				break;
			case 2:
				num = 3;
				continue;
			case 3:
				if (A_0 != this.ᜅ)
				{
					goto IL_81;
				}
				return;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			return;
			IL_81:
			num = 0;
		}
		IL_68:
		if (false)
		{
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00015418 File Offset: 0x00014418
	public double ᜎ()
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

	// Token: 0x06000248 RID: 584 RVA: 0x0001545C File Offset: 0x0001445C
	public void ᜁ(double A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x000154A0 File Offset: 0x000144A0
	public double ᜆ()
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
		return this.ᜉ;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x000154E4 File Offset: 0x000144E4
	public void ᜀ(double A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00015528 File Offset: 0x00014528
	public Color ᜌ()
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

	// Token: 0x0600024C RID: 588 RVA: 0x0001556C File Offset: 0x0001456C
	public Color ᜊ()
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
		return this.ᜋ;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x000155B0 File Offset: 0x000145B0
	public sprᶆ ᜏ()
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
		return this.ᜌ;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000155F4 File Offset: 0x000145F4
	public void ᜁ(sprᶆ A_0)
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
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				default:
					goto IL_68;
				}
				break;
			case 1:
				num = 4;
				continue;
			case 3:
				this.ᜌ = A_0;
				num = 0;
				continue;
			case 4:
				if (A_0 != this.ᜌ)
				{
					goto IL_81;
				}
				return;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			return;
			IL_81:
			num = 3;
		}
		IL_68:
		if (false)
		{
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0001568C File Offset: 0x0001468C
	public sprᶆ ᜑ()
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
		return this.\u170D;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000156D0 File Offset: 0x000146D0
	public void ᜀ(sprᶆ A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.\u170D = A_0;
				num = 1;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_79;
				default:
					goto IL_60;
				}
				break;
			case 3:
				num = 4;
				continue;
			case 4:
				if (A_0 != this.\u170D)
				{
					goto IL_79;
				}
				return;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return;
			IL_79:
			num = 0;
		}
		IL_60:
		if (false)
		{
		}
	}

	// Token: 0x040000A5 RID: 165
	private ExportBase ᜀ;

	// Token: 0x040000A6 RID: 166
	private Stream ᜁ;

	// Token: 0x040000A7 RID: 167
	private StringListCollection ᜂ;

	// Token: 0x040000A8 RID: 168
	private ArrayList ᜃ;

	// Token: 0x040000A9 RID: 169
	private SortedList ᜄ;

	// Token: 0x040000AA RID: 170
	private PdfFont ᜅ;

	// Token: 0x040000AB RID: 171
	private int ᜆ;

	// Token: 0x040000AC RID: 172
	private int ᜇ;

	// Token: 0x040000AD RID: 173
	private double ᜈ;

	// Token: 0x040000AE RID: 174
	private double ᜉ;

	// Token: 0x040000AF RID: 175
	private Color ᜊ;

	// Token: 0x040000B0 RID: 176
	private Color ᜋ;

	// Token: 0x040000B1 RID: 177
	private sprᶆ ᜌ;

	// Token: 0x040000B2 RID: 178
	private sprᶆ \u170D;

	// Token: 0x040000B3 RID: 179
	private CultureInfo ᜎ;
}
