using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000621 RID: 1569
	public class FormulaUtil : XlsObject
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06005F24 RID: 24356 RVA: 0x003B7030 File Offset: 0x003B6030
		// (remove) Token: 0x06005F25 RID: 24357 RVA: 0x003B70C4 File Offset: 0x003B60C4
		public static event EvaluateEventHandler FormulaEvaluator
		{
			add
			{
				for (;;)
				{
					EvaluateEventHandler evaluateEventHandler = FormulaUtil.ᜩ;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						EvaluateEventHandler evaluateEventHandler2;
						switch (num)
						{
						case 0:
							if (evaluateEventHandler == evaluateEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_36;
						case 1:
							goto IL_66;
						case 2:
							goto IL_36;
						}
						break;
						IL_36:
						evaluateEventHandler2 = evaluateEventHandler;
						EvaluateEventHandler value2 = (EvaluateEventHandler)Delegate.Combine(evaluateEventHandler2, value);
						evaluateEventHandler = Interlocked.CompareExchange<EvaluateEventHandler>(ref FormulaUtil.ᜩ, value2, evaluateEventHandler2);
						num = 0;
					}
				}
				IL_66:
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
			remove
			{
				for (;;)
				{
					EvaluateEventHandler evaluateEventHandler = FormulaUtil.ᜩ;
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						EvaluateEventHandler evaluateEventHandler2;
						switch (num)
						{
						case 0:
							goto IL_66;
						case 1:
							goto IL_36;
						case 2:
							if (evaluateEventHandler == evaluateEventHandler2)
							{
								num = 0;
								continue;
							}
							goto IL_36;
						}
						break;
						IL_36:
						evaluateEventHandler2 = evaluateEventHandler;
						EvaluateEventHandler value2 = (EvaluateEventHandler)Delegate.Remove(evaluateEventHandler2, value);
						evaluateEventHandler = Interlocked.CompareExchange<EvaluateEventHandler>(ref FormulaUtil.ᜩ, value2, evaluateEventHandler2);
						num = 2;
					}
				}
				IL_66:
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

		// Token: 0x06005F26 RID: 24358 RVA: 0x003B7158 File Offset: 0x003B6158
		static FormulaUtil()
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
			FormulaUtil.\u1712 = RegexOptions.Compiled;
			FormulaUtil.\u1713 = new int[][][]
			{
				new int[][]
				{
					new int[]
					{
						1,
						1,
						1
					},
					new int[]
					{
						2,
						3,
						3
					},
					new int[]
					{
						3,
						3,
						3
					}
				},
				new int[][]
				{
					new int[]
					{
						2,
						2,
						3
					},
					new int[]
					{
						2,
						2,
						3
					},
					new int[]
					{
						2,
						2,
						3
					}
				},
				new int[][]
				{
					new int[]
					{
						3,
						3,
						3
					},
					new int[]
					{
						3,
						3,
						3
					},
					new int[]
					{
						3,
						3,
						3
					}
				},
				new int[][]
				{
					new int[]
					{
						2,
						2,
						1
					},
					new int[]
					{
						2,
						2,
						3
					},
					new int[]
					{
						2,
						2,
						3
					}
				}
			};
			FormulaUtil.OpenBrackets = new char[]
			{
				'{',
				'(',
				'"',
				'\'',
				'['
			};
			FormulaUtil.CloseBrackets = new char[]
			{
				'}',
				')',
				'"',
				'\'',
				']'
			};
			FormulaUtil.StringBrackets = new char[]
			{
				'"'
			};
			FormulaUtil.UnaryOperations = new string[]
			{
				RecordTableEnumerator.b("ᬽ", a_),
				RecordTableEnumerator.b("ᘽ", a_),
				RecordTableEnumerator.b("ᔽ", a_),
				RecordTableEnumerator.b("ጽ", a_)
			};
			FormulaUtil.PlusMinusArray = new string[]
			{
				RecordTableEnumerator.b("ᔽ", a_),
				RecordTableEnumerator.b("ጽ", a_)
			};
			FormulaUtil.\u1714 = FormulaUtil.ᜀ(FormulaUtil.PlusMinusArray);
			FormulaUtil.FunctionIdToAlias = new Dictionary<ExcelFunction, string>(356);
			FormulaUtil.FunctionIdToParamCount = new Dictionary<ExcelFunction, int>(356);
			FormulaUtil.FunctionAliasToId = new Dictionary<string, ExcelFunction>();
			FormulaUtil.\u1715 = new Dictionary<ExcelFunction, Dictionary<Type, sprᨳ>>(356);
			FormulaUtil.ErrorNameToConstructor = new Dictionary<string, ConstructorInfo>(7);
			FormulaUtil.\u1716 = new Dictionary<int, string>(7);
			FormulaUtil.\u1717 = new Dictionary<string, int>(7);
			FormulaUtil.\u1718 = new Dictionary<FormulaToken, FormulaUtil.ᜀ>(25);
			FormulaUtil.\u1719 = new Dictionary<FormulaToken, Ptg>();
			FormulaUtil.CellRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁݃⥅⑇㽉⅋⁍慏汑ན੕籗ݙ捛՝⅟佡㹣㭥፧孩䁫嵭൯孱屳䥵䑷⡹፻ॽ녿벁\udf83\uda85겇힉뎋튍릑붓", a_), FormulaUtil.\u1712);
			FormulaUtil.CellR1C1Regex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᙃ⥅㽇等牋ᱍ୏๑ན୕杗ř[獝㵟嵡㽣噥䕧卩ㅫ䑭⭯⹱⥳⭵䝷卹呻䅽뱿솁ﶇ뾍꺏톑쾓쪕쎗잙ꎛ얝ﲟ辡趥閩麭鶯讱鲵﾿", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRangeRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁݃⥅⑇㽉⅋⁍慏汑ན੕籗ݙ捛՝⅟佡㹣ݥ䕧ၩㅫᕭ䅯幱䝳୵具剹䍻䉽퉿랅뚇톉킋ꪍ춏궑좓뎗뎙ꚛ뚝龟麡즥쒧\udfa9솫삭芯貱鲷莻膿黃Ʂ냉釋뗍ﻑꯕ닟跡鏣퓥훧뇩냫쫭귯췱ꣳ鋵폷폹", a_), FormulaUtil.\u1712);
			FormulaUtil.FullRowRangeRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᙃ⥅㽇等牋ᕍ౏癑॓楕ї㹙睛睝婟䩡季婥㩧թ᭫屭乯⥱⡳創╷䕹⁻᩽ꭿꮁ", a_), FormulaUtil.\u1712);
			FormulaUtil.FullColumnRangeRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁݃⥅⑇㽉⅋⁍慏汑ན੕籗ݙ捛՝⅟佡㹣ݥ䕧ၩㅫᕭ䅯幱䝳୵具䁹呻䅽뱿솁ﶇ벍꺏즑좓늕얗ꖙ잛\udf9d趟얣讥튧힫龭鲯膱즳龵", a_), FormulaUtil.\u1712);
			FormulaUtil.Full3DRowRangeRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹⹻ᅽ뎁몃\udd85풇꺉톋튍릑붓겕낗ꖙꂛ첝쾟햡隣颥被횱龳龵", a_), FormulaUtil.\u1712);
			FormulaUtil.Full3DColumnRangeRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹㽻ᅽ릇뒉힋튍뒏쾑쾓힕떗삙ﶛ뎝\uda9fﾡ\udfa3鞥蒧馩톫螭誯骱讳誵﮷햹킻쮽궿곁鏇雉鏍诏鏑陸賕맗ꛛ菝鯟폡죣헥闧쏩", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRangeR1C1Regex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᙃ⥅㽇等牋ᱍ୏๑ན୕杗ř[獝㵟嵡㽣噥䕧卩ㅫ䑭⭯⹱⥳⭵䝷卹呻䅽뱿솁ﶇ뾍꺏톑쾓쪕쎗잙ꎛ얝ﲟ辡念馥骩膫鞭颱莻鞽謹﯃鉶髇ꗉ믋ﳍ胑迓諕菗蟙藝볟쿡맣\ud9e5돧\udae9쇫ퟭ귯\ud8f1꿳ꫵꗷꟹ쏻퟽⣿㴁㠃䔅朇昉礋挍縏‑⨓唕䌗䘙䜛䌝἟礡砣ଥ甧ᔩ眫ḭᴯ଱椳ᰵ挷昹愻挽缿歁", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRangeR1C1ShortRegex = new Regex(RecordTableEnumerator.b("攽ሿ㹁݃ᭅፇᙉᝋፍ潏॑ࡓ筕՗教ݛɝ䵟剡䥣彥㕧䁩㝫㉭ⵯ⽱䭳", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRangeR1C13DShortRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳⵵⩷ٹ㽻⍽\udb7f\ude81\udf83\udb85랇톉킋ꎍ춏궑쾓ꚕ떗ꎙ솛뒝ﮟﺡ念ﮥ鞧", a_), FormulaUtil.\u1712);
			FormulaUtil.Cell3DRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹㽻ᅽ릇뒉힋튍뒏쾑ꮓ축\ud997랙욛ﾝ趟\ud8a1念\udda5馧蚩龫펭馯骱讳誵햹쮻辽ﺿ駁飃闇郋꫍﯏﯑", a_), FormulaUtil.\u1712);
			FormulaUtil.CellR1C13DRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹⹻ᅽ뎁몃풅펇횉힋펍꾏즑좓뮕얗ꖙ잛꺝趟鮡念貥辯鮱鲳覵蒷惡펻튽떿꿁꫃觉韋鋍诏近跕蓗臛믟틡짣\udfe5뗧샩럫닭귯꿱쯳\udff5", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRange3DRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹㽻ᅽ릇뒉힋튍뒏쾑ꮓ축\ud997랙욛ﾝ趟\ud8a1念\udda5馧蚩龫펭馯骱讳誵햹쮻辽ﺿ駁飃闇郋꫍﯏﯑ﻕ鿛뇝賟韡解裥\udae7퓩럫닭퓯꿱쯳귵맷ퟹ꛻鿽ⷿ码夃紅㤇☉㼋猍㤏㨑⬓⨕䨗甙欛Ⱍḟ礡砣ȥ甧ᔩ瀫䨭ᬯᬱ", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRange3DRegex2 = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹㽻ᅽ릇뒉힋튍뒏쾑ꮓ축\ud997랙욛ﾝ趟\ud8a1念\udda5馧蚩龫펭馯骱讳誵햹쮻辽ﺿ駁飃闇郋꫍﯏﯑ﻕ进뛝藟蟡郣꣥觧蟩觫\udced컯꧱ꫳ꯵ꏷ샹ꃻ퇽㿿弁⸃⼅匇嘉ⴋ匍㠏ⴑ⠓唕眗瘙椛猝丟အᨣ紥琧ษ焫ᄭ欯猱ᤳ氵夷᜹䘻挽㬿獁桃畅㕇捉摋煍汏Q㭓⅕橗摙ݛɝ䑟㽡季㩥౧䅩䕫", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRangeR1C13DRegex = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹⹻ᅽ뎁몃\udd85\uda87힉뎋햍첏즑즓ꦕ쎗욙놛쎝龟說钣讥醧蚫覵醷銹莻芽莿귁ꣃ독ꗇ꓉﷋诏金觓菗蛙蟛菝\udfdf맡룣쯥뗧헩럫\udeed\uddef쯱꧳\udcf5ꏷ꛹ꇻꏽ㿿⬁㸃⸅㜇㘉帋愍朏‑⨓䴕䨗䜙⌛䔝簟礡礣ᤥ猧瘩ī猭༯椱гᬵķ朹ᘻ攽᰿ὁ᥃祅慇扉獋牍ፏ㵑㡓⍕㕗㑙湛恝㭟Ⅱ㥣奥㍧㙩㝫㍭佯⥱⡳孵╷䕹❻乽굿뮁\ud983겅펇횉톋펍꾏뮑", a_), FormulaUtil.\u1712);
			FormulaUtil.CellRangeR1C13DRegex2 = new Regex(RecordTableEnumerator.b("ᘽ缿繁ᝃ⹅ⵇ⽉㡋Mㅏ㽑ㅓ桕͗љś՝婟㹡䭣奥㕧䁩䕫㕭Ɐ危⥳幵䝷䙹⹻ᅽ뎁몃\udd85\uda87힉뎋햍첏즑즓ꦕ쎗욙놛쎝龟說钣讥醧蚫覵醷銹莻芽莿귁ꣃ독ꗇ꓉﷋诏金觓菗蛙蟛菝\udfdf맡룣쯥뗧헩럫\udeed\uddef쯱꧳\udcf5ꏷ꛹ꇻꏽ㿿⬁㸃⸅㜇㘉弋昍甏眑怓堕礗眙礛Ⱍḟ礡稣笥猧ဩ瀫ĭ༯漱ḳἵ挷昹ᴻ挽栿絁硃ᑅ❇㵉繋灍୏Q॓楕͗ٙݛ͝彟㥡㡣䭥㕧啩㝫幭嵯䭱⥳屵⍷♹ⅻ⍽뽿ꮁ것릅뒇즉ﾑ望꒕ꚗ솙\udf9b쎝龟說ﶥ閩鶯讳袷鞹薻駁飃鯅闇", a_), FormulaUtil.\u1712);
			FormulaUtil.\u171A = new Regex(RecordTableEnumerator.b("ᘽ朿絁浃湅睇癉᱋⽍⑏㩑橓ൕٗ絙ś՝㹟㹡㽣㭥䍧㙩に䝭佯婱䭳䩵㩷ᕹ፻ᕽ칿뚇톉튋튍춏쾑뾓쪕얗뎙ꎛ뚝龟麡캥춧쾩\ud8ab톯\udfb1톳袵謹黁暈闇鋍ﳓ裙뷛냝蟟蟡ꫣ蟥藧迩틫뗭껯꿱꿳쳵ꓷ헹쏻ꏽ⫿⬁", a_), FormulaUtil.\u1712);
			FormulaUtil.\u171B = new ExcelFunction[]
			{
				ExcelFunction.CELL,
				ExcelFunction.INFO,
				ExcelFunction.NOW,
				ExcelFunction.TODAY
			};
			FormulaUtil.\u171C = new FormulaToken[]
			{
				FormulaToken.tNameX1,
				FormulaToken.tNameX2,
				FormulaToken.tNameX3
			};
			FormulaUtil.\u171D = new FormulaToken[]
			{
				FormulaToken.tName1,
				FormulaToken.tName2,
				FormulaToken.tName3
			};
			FormulaUtil.\u171E = new ExcelFunction[]
			{
				ExcelFunction.HEX2BIN,
				ExcelFunction.HEX2DEC,
				ExcelFunction.HEX2OCT,
				ExcelFunction.COUNTIFS,
				ExcelFunction.BIN2DEC,
				ExcelFunction.BIN2HEX,
				ExcelFunction.BIN2OCT,
				ExcelFunction.DEC2BIN,
				ExcelFunction.DEC2HEX,
				ExcelFunction.DEC2OCT,
				ExcelFunction.OCT2BIN,
				ExcelFunction.OCT2DEC,
				ExcelFunction.OCT2HEX,
				ExcelFunction.ODDFPRICE,
				ExcelFunction.ODDFYIELD,
				ExcelFunction.ODDLPRICE,
				ExcelFunction.ODDLYIELD,
				ExcelFunction.ISODD,
				ExcelFunction.ISEVEN,
				ExcelFunction.LCM,
				ExcelFunction.GCD,
				ExcelFunction.SUMIFS,
				ExcelFunction.AVERAGEIF,
				ExcelFunction.AVERAGEIFS,
				ExcelFunction.CONVERT,
				ExcelFunction.COMPLEX,
				ExcelFunction.COUPDAYBS,
				ExcelFunction.COUPDAYS,
				ExcelFunction.COUPDAYSNC,
				ExcelFunction.COUPNCD,
				ExcelFunction.COUPNUM,
				ExcelFunction.COUPPCD,
				ExcelFunction.DELTA,
				ExcelFunction.DISC,
				ExcelFunction.DOLLARDE,
				ExcelFunction.DOLLARFR,
				ExcelFunction.DURATION,
				ExcelFunction.EDATE,
				ExcelFunction.EFFECT,
				ExcelFunction.EOMONTH,
				ExcelFunction.ERF,
				ExcelFunction.ERFC,
				ExcelFunction.FACTDOUBLE,
				ExcelFunction.GESTEP,
				ExcelFunction.IFERROR,
				ExcelFunction.IMABS,
				ExcelFunction.IMAGINARY,
				ExcelFunction.IMARGUMENT,
				ExcelFunction.IMCONJUGATE,
				ExcelFunction.IMCOS,
				ExcelFunction.IMEXP,
				ExcelFunction.IMLN,
				ExcelFunction.IMLOG10,
				ExcelFunction.IMLOG2,
				ExcelFunction.IMREAL,
				ExcelFunction.IMSIN,
				ExcelFunction.IMSQRT,
				ExcelFunction.IMSUB,
				ExcelFunction.IMSUM,
				ExcelFunction.IMDIV,
				ExcelFunction.IMPOWER,
				ExcelFunction.IMPRODUCT,
				ExcelFunction.ACCRINT,
				ExcelFunction.ACCRINTM
			};
			FormulaUtil.\u171F = new ExcelFunction[]
			{
				ExcelFunction.AGGREGATE,
				ExcelFunction.CHISQ_DIST,
				ExcelFunction.CHISQ_DIST,
				ExcelFunction.BETA_INV,
				ExcelFunction.BETA_DIST,
				ExcelFunction.BINOM_DIST,
				ExcelFunction.BINOM_INV,
				ExcelFunction.CEILING_PRECISE,
				ExcelFunction.CHISQ_DIST_RT,
				ExcelFunction.CHISQ_INV_RT,
				ExcelFunction.CHISQ_TEST,
				ExcelFunction.CONFIDENCE_NORM,
				ExcelFunction.CONFIDENCE_T,
				ExcelFunction.COVARIANCE_P,
				ExcelFunction.COVARIANCE_S,
				ExcelFunction.ERF_PRECISE,
				ExcelFunction.ERFC_PRECISE,
				ExcelFunction.EXPON_DIST,
				ExcelFunction.F_DIST,
				ExcelFunction.F_DIST_RT,
				ExcelFunction.F_INV,
				ExcelFunction.F_INV_RT,
				ExcelFunction.F_TEST,
				ExcelFunction.FLOOR_PRECISE,
				ExcelFunction.GAMMA_DIST,
				ExcelFunction.GAMMA_INV,
				ExcelFunction.GAMMALN_PRECISE,
				ExcelFunction.HYPGEOM_DIST,
				ExcelFunction.LOGNORM_DIST,
				ExcelFunction.LOGNORM_INV,
				ExcelFunction.MODE_MULT,
				ExcelFunction.MODE_SNGL,
				ExcelFunction.NEGBINOM_DIST,
				ExcelFunction.NETWORKDAYS_INTL,
				ExcelFunction.NORM_DIST,
				ExcelFunction.NORM_INV,
				ExcelFunction.NORM_S_DIST,
				ExcelFunction.PERCENTILE_EXC,
				ExcelFunction.PERCENTILE_INC,
				ExcelFunction.PERCENTRANK_EXC,
				ExcelFunction.PERCENTRANK_INC,
				ExcelFunction.POISSON_DIST,
				ExcelFunction.QUARTILE_EXC,
				ExcelFunction.QUARTILE_INC,
				ExcelFunction.RANK_AVG,
				ExcelFunction.RANK_EQ,
				ExcelFunction.STDEV_P,
				ExcelFunction.STDEV_S,
				ExcelFunction.T_DIST,
				ExcelFunction.T_DIST_2T,
				ExcelFunction.T_DIST_RT,
				ExcelFunction.T_INV,
				ExcelFunction.T_INV_2T,
				ExcelFunction.T_TEST,
				ExcelFunction.VAR_P,
				ExcelFunction.VAR_S,
				ExcelFunction.WEIBULL_DIST,
				ExcelFunction.WORKDAY_INTL,
				ExcelFunction.Z_TEST
			};
			FormulaUtil.ᜢ = new string[]
			{
				RecordTableEnumerator.b("ḽ", a_),
				RecordTableEnumerator.b("ᠽ", a_),
				RecordTableEnumerator.b("ᐽ", a_),
				RecordTableEnumerator.b("ᔽ", a_),
				RecordTableEnumerator.b("ሽ", a_),
				RecordTableEnumerator.b("ጽ", a_),
				RecordTableEnumerator.b("ᄽ", a_),
				RecordTableEnumerator.b("Ƚ縿", a_),
				RecordTableEnumerator.b("Ƚ紿", a_),
				RecordTableEnumerator.b("Ƚ", a_),
				RecordTableEnumerator.b("̽", a_),
				RecordTableEnumerator.b("=紿", a_),
				RecordTableEnumerator.b("=", a_),
				RecordTableEnumerator.b("怽", a_)
			};
			FormulaUtil.ᜄ();
			FormulaUtil.ᜂ();
			FormulaUtil.ᜁ();
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x003B7C70 File Offset: 0x003B6C70
		internal FormulaUtil(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 11;
			this.ᜣ = new string[][]
			{
				new string[]
				{
					RecordTableEnumerator.b("慀", a_),
					RecordTableEnumerator.b("浀", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("ὀ", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("歀", a_),
					RecordTableEnumerator.b("湀", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("橀", a_),
					RecordTableEnumerator.b("汀", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("杀", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("絀", a_),
					RecordTableEnumerator.b("絀繂", a_),
					RecordTableEnumerator.b("絀終", a_),
					RecordTableEnumerator.b("籀", a_),
					RecordTableEnumerator.b("罀", a_),
					RecordTableEnumerator.b("罀繂", a_)
				}
			};
			this.ᜤ = new SortedList(new StringComparer());
			this.ᜦ = RecordTableEnumerator.b("穀", a_);
			this.ᜧ = RecordTableEnumerator.b("浀", a_);
			base..ctor(A_0, A_1);
			this.ᜆ();
			this.ᜅ();
			this.ᜥ = new SortedList[this.ᜣ.Length];
			this.ᜀ();
			spr\u1DF5 spr_u1DF = base.ReservedHandle;
			this.ᜨ = new spr\u236F(this.ᜡ);
			this.SetSeparators(spr_u1DF.\u1717(), spr_u1DF.\u1716());
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x003B7E68 File Offset: 0x003B6E68
		internal FormulaUtil(spr\u1DF5 A_0, object A_1, NumberFormatInfo A_2, char A_3, char A_4)
		{
			int a_ = 5;
			this.ᜣ = new string[][]
			{
				new string[]
				{
					RecordTableEnumerator.b("ᬺ", a_),
					RecordTableEnumerator.b("᜺", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("攺", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("ᄺ", a_),
					RecordTableEnumerator.b("ᐺ", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("်", a_),
					RecordTableEnumerator.b("ᘺ", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("ᴺ", a_)
				},
				new string[]
				{
					RecordTableEnumerator.b("ܺ", a_),
					RecordTableEnumerator.b("ܺ<", a_),
					RecordTableEnumerator.b("̼ܺ", a_),
					RecordTableEnumerator.b("غ", a_),
					RecordTableEnumerator.b("Ժ", a_),
					RecordTableEnumerator.b("Ժ<", a_)
				}
			};
			this.ᜤ = new SortedList(new StringComparer());
			this.ᜦ = RecordTableEnumerator.b(":", a_);
			this.ᜧ = RecordTableEnumerator.b("᜺", a_);
			base..ctor(A_0, A_1);
			this.ᜆ();
			this.ᜅ();
			this.ᜥ = new SortedList[this.ᜣ.Length];
			this.ᜀ();
			this.ᜨ = new spr\u236F(this.ᜡ);
			this.ᜨ.ᜀ(A_2);
			this.ᜠ = A_2;
			this.SetSeparators(A_3, A_4);
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x003B8060 File Offset: 0x003B7060
		private void ᜆ()
		{
			int a_ = 2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜡ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
				if (this.ᜡ != null)
				{
					return;
				}
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("漷唹主唽∿ⵁ⭃ⵅ", a_), RecordTableEnumerator.b("笷嬹刻᤽㐿扁≃⽅♇⹉汋㹍ㅏ⁑ㅓ㡕ⱗ穙⭛ㅝ቟ॡ٣॥ݧũ", a_));
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x003B80EC File Offset: 0x003B70EC
		private void ᜅ()
		{
			for (;;)
			{
				int num = 0;
				int num2 = FormulaUtil.ᜢ.Length;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_2A;
					case 1:
						if (num < num2)
						{
							this.ᜤ.Add(FormulaUtil.ᜢ[num], null);
							num++;
							num3 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num3 = 2;
							continue;
						}
						break;
					case 2:
						return;
					case 3:
						goto IL_2C;
					}
					break;
					IL_2C:
					num3 = 1;
					continue;
					IL_2A:
					goto IL_2C;
				}
			}
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x003B8190 File Offset: 0x003B7190
		private static void ᜄ()
		{
			for (;;)
			{
				Type[] array = spr\u17FF.ᜑ;
				Type typeFromHandle = typeof(Ptg);
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (array[num].IsSubclassOf(typeFromHandle))
						{
							num2 = 6;
							continue;
						}
						goto IL_41;
					case 1:
						goto IL_B5;
					case 2:
						goto IL_B5;
					case 3:
						return;
					case 4:
						goto IL_41;
					case 5:
						if (num >= array.Length)
						{
							goto IL_C6;
						}
						num2 = 0;
						continue;
					case 6:
						FormulaUtil.RegisterTokenClass(array[num]);
						num2 = 4;
						continue;
					}
					break;
					IL_41:
					num++;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_C6:
						num2 = 3;
						continue;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					IL_B5:
					num2 = 5;
				}
			}
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x003B8270 File Offset: 0x003B7270
		private static void ᜃ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					string[] names = Enum.GetNames(typeof(ExcelFunction));
					Type typeFromHandle = typeof(ExcelFunction);
					int num = 0;
					int num2 = names.Length;
					int num3 = 6;
					for (;;)
					{
						string a_;
						ExcelFunction a_2;
						DefaultValueAttribute defaultValueAttribute;
						sprᨳ[] a_3;
						switch (num3)
						{
						case 0:
						{
							DescriptionAttribute descriptionAttribute;
							a_ = descriptionAttribute.Description;
							goto IL_AF;
						}
						case 1:
							num3 = 4;
							continue;
						case 2:
							goto IL_18B;
						case 3:
						{
							DescriptionAttribute descriptionAttribute;
							if (descriptionAttribute == null)
							{
								num3 = 1;
								continue;
							}
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
								num3 = 0;
								continue;
							}
							break;
						}
						case 4:
						{
							string text;
							a_ = text;
							goto IL_AF;
						}
						case 5:
							return;
						case 6:
							goto IL_18B;
						case 7:
						{
							if (num >= num2)
							{
								num3 = 5;
								continue;
							}
							string text = names[num];
							a_2 = (ExcelFunction)Enum.Parse(typeof(ExcelFunction), text, true);
							MemberInfo[] member = typeFromHandle.GetMember(text);
							MemberInfo element = member[0];
							defaultValueAttribute = (DefaultValueAttribute)Attribute.GetCustomAttribute(element, typeof(DefaultValueAttribute));
							DescriptionAttribute descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(element, typeof(DescriptionAttribute));
							a_3 = (sprᨳ[])Attribute.GetCustomAttributes(element, typeof(sprᨳ));
							num3 = 3;
							continue;
						}
						}
						break;
						IL_AF:
						FormulaUtil.ᜁ(a_, a_2, a_3, (defaultValueAttribute != null) ? ((int)defaultValueAttribute.Value) : -1);
						num++;
						num3 = 2;
						continue;
						IL_18B:
						num3 = 7;
					}
				}
				return;
			}
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x003B842C File Offset: 0x003B742C
		private static void ᜂ()
		{
			int a_ = 7;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			sprᨳ[] a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀൂᅄ", a_), ExcelFunction.COUNT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼社", a_), ExcelFunction.IF, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ཀɂ", a_), ExcelFunction.ISNA, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾рᅂᝄࡆᭈ", a_), ExcelFunction.ISERROR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీ", a_), ExcelFunction.SUM, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼椾рᅂфFై", a_), ExcelFunction.AVERAGE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(spr\u2372), 3),
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾ཀ", a_), ExcelFunction.MIN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(spr\u2372), 3),
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼績᥀", a_), ExcelFunction.MAX, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瀾ᙀ", a_), ExcelFunction.ROW, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ീᙂࡄॆ", a_), ExcelFunction.COLUMN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼績", a_), ExcelFunction.NA, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2),
				new sprᨳ(typeof(sprᲔ), new int[]
				{
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼漾ᝀ", a_), ExcelFunction.NPV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款Հقፄ", a_), ExcelFunction.STDEV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瀾ീགфᕆ", a_), ExcelFunction.DOLLAR, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾᥀قń", a_), ExcelFunction.FIXED, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼瘾ཀ", a_), ExcelFunction.SIN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ቀ", a_), ExcelFunction.COS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼績ཀ", a_), ExcelFunction.TAN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼款@ൂ", a_), ExcelFunction.ATAN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼瘾", a_), ExcelFunction.PI, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼渾ፀᝂ", a_), ExcelFunction.SQRT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼朾ᅀ", a_), ExcelFunction.EXP, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼焾", a_), ExcelFunction.LN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـ牂畄", a_), ExcelFunction.LOG10, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼紾ቀ", a_), ExcelFunction.ABS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾ᕀ", a_), ExcelFunction.INT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼瘾ـൂ", a_), ExcelFunction.SIGN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瀾ᑀൂń", a_), ExcelFunction.ROUND, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼稾᥀煂݄ๆ݈", a_), ExcelFunction.HEX2BIN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼稾᥀煂ńɆੈ", a_), ExcelFunction.HEX2DEC, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼稾᥀煂੄цᵈ", a_), ExcelFunction.HEX2OCT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼瘾ཀ煂ńɆੈ", a_), ExcelFunction.BIN2DEC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼瘾ཀ煂ൄɆᅈ", a_), ExcelFunction.BIN2HEX, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼瘾ཀ煂੄цᵈ", a_), ExcelFunction.BIN2OCT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ɀ煂݄ๆ݈", a_), ExcelFunction.DEC2BIN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ɀ煂ൄɆᅈ", a_), ExcelFunction.DEC2HEX, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ɀ煂੄цᵈ", a_), ExcelFunction.DEC2OCT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼簾ᕀ煂݄ๆ݈", a_), ExcelFunction.OCT2BIN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼簾ᕀ煂ńɆੈ", a_), ExcelFunction.OCT2DEC, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼簾ᕀ煂ൄɆᅈ", a_), ExcelFunction.OCT2HEX, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼笾ՀՂᕄᕆHࡊࡌ", a_), ExcelFunction.ODDFPRICE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼笾ՀՂ᱄ๆై݊ौ", a_), ExcelFunction.ODDFYIELD, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼笾ՀགᕄᕆHࡊࡌ", a_), ExcelFunction.ODDLPRICE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼笾Հག᱄ๆై݊ौ", a_), ExcelFunction.ODDLYIELD, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾рᕂDॆ", a_), ExcelFunction.ISEVEN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾เ݂ń", a_), ExcelFunction.ISODD, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼椾рᅂфFైɊୌᱎ", a_), ExcelFunction.AVERAGEIFS, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼椾рᅂфFైɊୌ", a_), ExcelFunction.AVERAGEIF, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ཀᕂDᕆᵈ", a_), ExcelFunction.CONVERT, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ీፂॄɆᅈ", a_), ExcelFunction.COMPLEX, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀፂńن၈ॊṌ", a_), ExcelFunction.COUPDAYBS, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀፂńن၈ᡊ", a_), ExcelFunction.COUPDAYS, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀፂńن၈ᡊ͌౎", a_), ExcelFunction.COUPDAYSNC, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀፂୄцൈ", a_), ExcelFunction.COUPNCD, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀፂୄቆш", a_), ExcelFunction.COUPNUM, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀፂᕄцൈ", a_), ExcelFunction.COUPPCD, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ീᝂф", a_), ExcelFunction.DELTA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瘾ቀB", a_), ExcelFunction.DISC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瀾ീགфᕆൈ๊", a_), ExcelFunction.DOLLARDE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瀾ീགфᕆ཈᥊", a_), ExcelFunction.DOLLARFR, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼樾ፀɂᅄๆوՊ", a_), ExcelFunction.DURATION, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼笾@ᝂD", a_), ExcelFunction.EDATE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼社݀قلፆ", a_), ExcelFunction.EFFECT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼瀾ీూୄፆň", a_), ExcelFunction.EOMONTH, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼派݀", a_), ExcelFunction.ERF, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼派݀B", a_), ExcelFunction.ERFC, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼績ɀᝂńࡆ᱈ॊŌ੎", a_), ExcelFunction.FACTDOUBLE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ቀᝂDᝆ", a_), ExcelFunction.GESTEP, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼社рᅂᝄࡆᭈ", a_), ExcelFunction.IFERROR, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾@łᙄ", a_), ExcelFunction.IMABS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾@тౄॆࡈ᥊ᑌ", a_), ExcelFunction.IMAGINARY, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾@ᅂɄቆш๊͌᭎", a_), ExcelFunction.IMARGUMENT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ɀూୄെ᱈ొౌ᭎ᑐ", a_), ExcelFunction.IMCONJUGATE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ɀూᙄ", a_), ExcelFunction.IMCOS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾рᭂᕄ", a_), ExcelFunction.IMEXP, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ീൂ", a_), ExcelFunction.IMLN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ീూɄ癆祈", a_), ExcelFunction.IMLOG10, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ീూɄ畆", a_), ExcelFunction.IMLOG2, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ᅀూቄɆᭈ", a_), ExcelFunction.IMPOWER, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ᅀᅂ੄͆᱈ࡊ᥌", a_), ExcelFunction.IMPRODUCT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ፀقф୆", a_), ExcelFunction.IMREAL, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ቀੂୄ", a_), ExcelFunction.IMSIN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ቀቂᝄፆ", a_), ExcelFunction.IMSQRT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ቀᙂ݄", a_), ExcelFunction.IMSUB, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾ቀᙂࡄ", a_), ExcelFunction.IMSUM, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼爾Հੂፄ", a_), ExcelFunction.IMDIV, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼簾ీ", a_), ExcelFunction.LCM, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీੂ̈́ᑆ", a_), ExcelFunction.SUMIFS, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼簾Հ", a_), ExcelFunction.GCD, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀൂᅄๆ཈ᡊ", a_), ExcelFunction.COUNTIFS, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼簾ɀᅂౄॆᵈ", a_), ExcelFunction.ACCRINT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼簾ɀᅂౄॆᵈي", a_), ExcelFunction.ACCRINTM, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼砾ـᅂDFࡈὊࡌ", a_), ExcelFunction.AGGREGATE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼爾เᅂńɆ่᥊์", a_), ExcelFunction.AMORDEGRC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼爾เᅂॄๆ݈ࡊ", a_), ExcelFunction.AMORLINC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼績ीᝂᅄɆᅈὊ", a_), ExcelFunction.BAHTTEXT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ቀ၂D୆H", a_), ExcelFunction.BESSELI, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ቀ၂D୆͈", a_), ExcelFunction.BESSELJ, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ቀ၂D୆Ɉ", a_), ExcelFunction.BESSELK, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ቀ၂D୆၈", a_), ExcelFunction.BESSELY, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قไᝆHيࡌɎፐᙒݔ", a_), ExcelFunction.CUBEKPIMEMBER, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قࡄɆшॊࡌᵎ", a_), ExcelFunction.CUBEMEMBER, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قᝄن݈Jࡌ୎᱐ᙒᡔᕖ᱘ग़", a_), ExcelFunction.CUBERANKEDMEMBER, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قᙄɆᵈ", a_), ExcelFunction.CUBESET, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قᙄɆᵈࡊɌᩎὐݒ", a_), ExcelFunction.CUBESETCOUNT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قࡄɆшॊࡌᵎŐŒᩔݖ᱘ग़ड़ٞ", a_), ExcelFunction.CUBEMEMBERPROPERTY, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾ీੂᕄ੆ᵈ", a_), ExcelFunction.CUMIPMT, a_2, 6);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾ీፂᝄๆ݈ࡊ", a_), ExcelFunction.CUMPRINC, a_2, 6);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼椾ቀBൄɆൈṊŌ੎", a_), ExcelFunction.FVSCHEDULE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾ᕀᅂфፆై", a_), ExcelFunction.INTRATE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾̀قፄنՈṊࡌ", a_), ExcelFunction.CUBEVALUE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼笾ᑀᅂфፆHъ͌", a_), ExcelFunction.MDURATION, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼派เᙂୄ͆", a_), ExcelFunction.MROUND, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼樾ീᝂౄॆويь๎ᵐ", a_), ExcelFunction.MULTINOMIAL, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼稾ᕀᑂ੄ᕆɈཊౌᙎɐ", a_), ExcelFunction.NETWORKDAYS, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ీੂୄنՈ", a_), ExcelFunction.NOMINAL, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派ࡀBD", a_), ExcelFunction.PRICE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派ࡀBD͆Hᡊ์", a_), ExcelFunction.PRICEDISC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派ࡀBD੆ࡈὊ", a_), ExcelFunction.PRICEMAT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("氼樾เᝂౄɆ݈Ὂ", a_), ExcelFunction.QUOTIENT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績ཀ݂݄Ɇᵈ᱊ࡌ੎ὐ", a_), ExcelFunction.RANDBETWEEN, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ɀقౄᅆైཊ", a_), ExcelFunction.RECEIVED, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾ፀੂDᑆᩈṊL", a_), ExcelFunction.SERIESSUM, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼渾ፀᝂᕄๆ", a_), ExcelFunction.SQRTPI, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼紾ࡀགॄɆᡈ", a_), ExcelFunction.TBILLEQ, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼紾ࡀགॄᝆᭈɊ์੎", a_), ExcelFunction.TBILLPRICE, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼紾ࡀགॄṆH๊Ō୎", a_), ExcelFunction.TBILLYIELD, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼稾рࡂୄቆш", a_), ExcelFunction.WEEKNUM, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼瀾ፀࡂńن၈", a_), ExcelFunction.WORKDAY, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("攼瘾ፀᅂ", a_), ExcelFunction.XIRR, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("攼焾ᅀᕂ", a_), ExcelFunction.XNPV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("搼稾@ᅂ̈́ᕆࡈࡊ", a_), ExcelFunction.YEARFRAC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("搼瘾рགń", a_), ExcelFunction.YIELD, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("搼瘾рགń͆Hᡊ์", a_), ExcelFunction.YIELDDISC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("搼瘾рགń੆ࡈὊ", a_), ExcelFunction.YIELDMAT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼瀾ፀࡂńن၈敊ьŎՐὒ", a_), ExcelFunction.WORKDAYINTL, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ᕀɂ歄ๆ݈ᵊ", a_), ExcelFunction.BETA_INV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼瘾ཀూࡄ楆ൈɊṌ᭎", a_), ExcelFunction.BINOM_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼瘾ཀూࡄ楆HՊᭌ", a_), ExcelFunction.BINOM_INV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼稾ࡀགౄॆ่敊ᵌᵎᑐၒ᱔і᱘", a_), ExcelFunction.CEILING_PRECISE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀ၂ᑄ楆ൈɊṌ᭎", a_), ExcelFunction.CHISQ_DIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀ၂ᑄ楆ൈɊṌ᭎罐ŒŔ", a_), ExcelFunction.CHISQ_DIST_RT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀ၂ᑄ楆HՊᭌ", a_), ExcelFunction.CHISQ_INV, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀ၂ᑄ楆HՊᭌ慎͐ݒ", a_), ExcelFunction.CHISQ_INV_RT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀ၂ᑄ楆ᵈ๊Ṍ᭎", a_), ExcelFunction.CHISQ_TEST, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ཀՂౄ͆ైՊ์੎罐ᵒᩔՖᑘ", a_), ExcelFunction.CONFIDENCE_NORM, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ཀՂౄ͆ైՊ์੎罐ݒ", a_), ExcelFunction.CONFIDENCE_T, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᝀɂᝄๆࡈՊ์੎罐͒", a_), ExcelFunction.COVARIANCE_P, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᝀɂᝄๆࡈՊ์੎罐R", a_), ExcelFunction.COVARIANCE_S, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼派݀浂ᕄᕆైࡊьᱎᑐ", a_), ExcelFunction.ERF_PRECISE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼派݀B歄ᝆᭈ๊์َɐᙒ", a_), ExcelFunction.ERFC_PRECISE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼ᄾՀੂᙄፆ", a_), ExcelFunction.F_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼ᄾՀੂᙄፆ杈᥊᥌", a_), ExcelFunction.F_DIST_RT, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼ᄾࡀൂፄ", a_), ExcelFunction.F_INV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼ᄾᕀقᙄፆ", a_), ExcelFunction.F_TEST, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼猾เూᝄ楆᥈᥊ࡌ౎ᡐRၔ", a_), ExcelFunction.FLOOR_PRECISE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼ᄾࡀൂፄ楆ᭈὊ", a_), ExcelFunction.F_INV_RT, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼績ీโф楆ൈɊṌ᭎", a_), ExcelFunction.GAMMA_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼績ీโф楆HՊᭌ", a_), ExcelFunction.GAMMA_INV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼績ీโф୆݈敊ᵌᵎᑐၒ᱔і᱘", a_), ExcelFunction.GAMMALN_PRECISE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼显ᅀтDࡆш敊ौَɐݒ", a_), ExcelFunction.HYPGEOM_DIST, a_2, 5);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـൂ੄ᕆш敊ौَɐݒ", a_), ExcelFunction.LOGNORM_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـൂ੄ᕆш敊ьŎݐ", a_), ExcelFunction.LOGNORM_INV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瀾Հق歄੆᱈݊᥌", a_), ExcelFunction.MODE_MULT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瀾Հق歄ᑆ݈ొŌ", a_), ExcelFunction.MODE_SNGL, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼稾ـłౄॆوي捌୎ᡐRŔ", a_), ExcelFunction.NEGBINOM_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼稾ᕀᑂ੄ᕆɈཊౌᙎɐ絒᱔ᥖ൘᝚", a_), ExcelFunction.NETWORKDAYS_INTL, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโ歄͆Hᡊ᥌", a_), ExcelFunction.NORM_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโ歄ๆ݈ᵊ", a_), ExcelFunction.NORM_INV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโ歄ᑆ杈ཊьᱎՐ", a_), ExcelFunction.NORM_S_DIST, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀBDॆᵈɊŌ੎罐ᙒൔᑖ", a_), ExcelFunction.PERCENTILE_EXC, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀBDॆᵈɊŌ੎罐ᩒ᭔ᑖ", a_), ExcelFunction.PERCENTILE_INC, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀBDॆᵈ᥊ౌŎᩐ絒ၔབᩘ", a_), ExcelFunction.PERCENTRANK_EXC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀBDॆᵈ᥊ౌŎᩐ絒᱔ᥖᩘ", a_), ExcelFunction.PERCENTRANK_INC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼瀾ࡀ၂ᙄࡆ݈敊ौَɐݒ", a_), ExcelFunction.POISSON_DIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("氼樾@ᅂᅄๆՈ๊捌੎ॐၒ", a_), ExcelFunction.QUARTILE_EXC, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("氼樾@ᅂᅄๆՈ๊捌َὐၒ", a_), ExcelFunction.QUARTILE_INC, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績ཀࡂ歄نὈొ", a_), ExcelFunction.RANK_AVG, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績ཀࡂ歄Ɇᡈ", a_), ExcelFunction.RANK_EQ, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款Հقፄ楆᥈", a_), ExcelFunction.STDEV_P, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款Հقፄ楆ᩈ", a_), ExcelFunction.STDEV_S, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼ᄾՀੂᙄፆ", a_), ExcelFunction.T_DIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼ᄾՀੂᙄፆ杈祊᥌", a_), ExcelFunction.T_DIST_2T, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼ᄾՀੂᙄፆ杈᥊᥌", a_), ExcelFunction.T_DIST_RT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼ᄾࡀൂፄ", a_), ExcelFunction.T_INV, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ፀ浂ᕄ", a_), ExcelFunction.VAR_P, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ፀ浂ᙄ", a_), ExcelFunction.VAR_S, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼稾ࡀł၄୆Ո敊ौَɐݒ", a_), ExcelFunction.WEIBULL_DIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("朼ᄾᕀقᙄፆ", a_), ExcelFunction.Z_TEST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1,
					1
				}),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾เࡂ၄ᝆ", a_), ExcelFunction.LOOKUP, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾Հقᵄ", a_), ExcelFunction.INDEX, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ᅀᝂ", a_), ExcelFunction.REPT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾Հ", a_), ExcelFunction.MID, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼稾ཀ", a_), ExcelFunction.LEN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ീᙂD", a_), ExcelFunction.VALUE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼派ᑀق", a_), ExcelFunction.TRUE, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼績ീ၂D", a_), ExcelFunction.FALSE, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼焾Հ", a_), ExcelFunction.AND, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼派", a_), ExcelFunction.OR, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ᕀ", a_), ExcelFunction.NOT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瀾Հ", a_), ExcelFunction.MOD, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼簾เᙂୄፆ", a_), ExcelFunction.DCOUNT, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼氾ᑀโ", a_), ExcelFunction.DSUM, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᝀقᝄن่๊", a_), ExcelFunction.DAVERAGE, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼爾ࡀൂ", a_), ExcelFunction.DMIN, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼爾@ᭂ", a_), ExcelFunction.DMAX, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼氾ᕀ݂Dᅆ", a_), ExcelFunction.DSTDEV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ፀ", a_), ExcelFunction.VAR, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼椾@ᅂ", a_), ExcelFunction.DVAR, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼稾᥀ᝂ", a_), ExcelFunction.TEXT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瘾ཀقᙄፆ", a_), ExcelFunction.LINEST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼派рൂń", a_), ExcelFunction.TREND, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـقᙄፆ", a_), ExcelFunction.LOGEST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼派เᑂᅄཆ", a_), ExcelFunction.GROWTH, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼瀾ᕀూ", a_), ExcelFunction.GOTO, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼績ീᝂ", a_), ExcelFunction.HALT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼椾", a_), ExcelFunction.PV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼椾", a_), ExcelFunction.FV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼漾рᅂ", a_), ExcelFunction.NPER, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼爾ᕀ", a_), ExcelFunction.PMT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績ᕀق", a_), ExcelFunction.RATE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(spr\u2372), 3),
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾ፀᅂ", a_), ExcelFunction.MIRR, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(spr\u2372), 3),
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼派ፀ", a_), ExcelFunction.IRR, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績ཀ݂", a_), ExcelFunction.RAND, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1
				}),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼績ᕀBൄ", a_), ExcelFunction.MATCH, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᕀق", a_), ExcelFunction.DATE, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼瘾ీق", a_), ExcelFunction.TIME, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᡀ", a_), ExcelFunction.DAY, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瀾ཀᝂൄ", a_), ExcelFunction.MONTH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("搼稾@ᅂ", a_), ExcelFunction.YEAR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼稾рࡂńن၈", a_), ExcelFunction.WEEKDAY, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼瀾ᑀᅂ", a_), ExcelFunction.HOUR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾ཀᙂᅄɆ", a_), ExcelFunction.MINUTE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾ɀూୄ͆", a_), ExcelFunction.SECOND, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ᙀ", a_), ExcelFunction.NOW, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼派рɂᙄ", a_), ExcelFunction.AREAS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瀾ᙀ၂", a_), ExcelFunction.ROWS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ീᙂࡄॆᩈ", a_), ExcelFunction.COLUMNS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼社݀၂Dፆ", a_), ExcelFunction.OFFSET, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼紾ቀᅂDņ", a_), ExcelFunction.ABSREF, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ീᅂDņ", a_), ExcelFunction.RELREF, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼派ـᙂࡄɆ݈Ὂ", a_), ExcelFunction.ARGUMENT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾@ᅂلཆ", a_), ExcelFunction.SEARCH, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(spr\u2372), 3),
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼派@ൂᙄᝆوᡊࡌ", a_), ExcelFunction.TRANSPOSE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼派ፀూᝄ", a_), ExcelFunction.ERROR, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款рፂ", a_), ExcelFunction.STEP, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼显ᅀق", a_), ExcelFunction.TYPE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼簾ीూ", a_), ExcelFunction.ECHO, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾ᕀൂф੆ై", a_), ExcelFunction.SETNAME, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼績ീགDᕆ", a_), ExcelFunction.CALLER, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ፀق̈́", a_), ExcelFunction.DEREF, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼瘾ཀ݂੄၆ᩈ", a_), ExcelFunction.WINDOWS, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾ፀੂDᑆ", a_), ExcelFunction.SERIES, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瀾ɀᙂࡄɆ݈ὊṌ", a_), ExcelFunction.DOCUMENTS, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼簾ᕀੂፄɆੈ๊Ō͎", a_), ExcelFunction.ACTIVECELL, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾ീقلፆHъ͌", a_), ExcelFunction.SELECTION, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ቀᙂॄፆ", a_), ExcelFunction.RESULT, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼款@ൂ睄", a_), ExcelFunction.ATAN2, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼氾ࡀൂ", a_), ExcelFunction.ASIN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼簾เ၂", a_), ExcelFunction.ACOS, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾เూᙄɆ", a_), ExcelFunction.CHOOSE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼猾เూไቆ᥈", a_), ExcelFunction.HLOOKUP, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼猾เూไቆ᥈", a_), ExcelFunction.VLOOKUP, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瘾ཀࡂᙄ", a_), ExcelFunction.LINKS, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾ᅀᙂᅄ", a_), ExcelFunction.INPUT, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ፀق̈́", a_), ExcelFunction.ISREF, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀՂ੄ᕆшṊŌ๎", a_), ExcelFunction.GETFORMULA, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀൂф੆ై", a_), ExcelFunction.GETNAME, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾ᕀᕂф୆᱈๊", a_), ExcelFunction.SETVALUE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـ", a_), ExcelFunction.LOG, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼朾рB", a_), ExcelFunction.EXEC, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾@ᅂ", a_), ExcelFunction.CHAR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ᙀقᝄ", a_), ExcelFunction.LOWER, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("格漾ᅀقᝄ", a_), ExcelFunction.UPPER, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派เፂDᕆ", a_), ExcelFunction.PROPER, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼稾݀ᝂ", a_), ExcelFunction.LEFT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瘾ـୂᅄ", a_), ExcelFunction.RIGHT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼朾@Bᅄ", a_), ExcelFunction.EXACT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼派ࡀโ", a_), ExcelFunction.TRIM, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ᅀགфцై", a_), ExcelFunction.REPLACE, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾̀၂ᅄๆᵈṊ᥌੎", a_), ExcelFunction.SUBSTITUTE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾Հق", a_), ExcelFunction.CODE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼績ీقᙄ", a_), ExcelFunction.NAMES, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瘾ፀقلፆو᥊ᑌ", a_), ExcelFunction.DIRECTORY, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾ཀ݂", a_), ExcelFunction.FIND, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼稾ീག", a_), ExcelFunction.CELL, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾рᅂᝄ", a_), ExcelFunction.ISERR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ᕀقᵄፆ", a_), ExcelFunction.ISTEXT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ཀᙂࡄՆై᥊", a_), ExcelFunction.ISNUMBER, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾̀གфॆɈ", a_), ExcelFunction.ISBLANK, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼", a_), ExcelFunction.T, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼", a_), ExcelFunction.N, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瀾ᅀقୄ", a_), ExcelFunction.FOPEN, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼簾ീూᙄɆ", a_), ExcelFunction.FCLOSE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼氾ࡀ᥂D", a_), ExcelFunction.FSIZE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼派рɂń୆݈", a_), ExcelFunction.FREADLN, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼派рɂń", a_), ExcelFunction.FREAD, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼栾ፀੂᅄɆՈՊ", a_), ExcelFunction.FWRITELN, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼栾ፀੂᅄɆ", a_), ExcelFunction.FWRITE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼漾เ၂", a_), ExcelFunction.FPOS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᕀقፄنՈṊࡌ", a_), ExcelFunction.DATEVALUE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼瘾ీقፄنՈṊࡌ", a_), ExcelFunction.TIMEVALUE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼猾ཀ", a_), ExcelFunction.SLN, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼显Հ", a_), ExcelFunction.SYD, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼笾̀", a_), ExcelFunction.DDB, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀ݂Dņ", a_), ExcelFunction.GETDEF, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾݀ᝂD὆ᵈ", a_), ExcelFunction.REFTEXT, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼稾᥀ᝂᝄɆ཈", a_), ExcelFunction.TEXTREF, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾ՀੂᝄɆੈὊ", a_), ExcelFunction.INDIRECT, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ـੂᙄፆై᥊", a_), ExcelFunction.REGISTER, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼績ീག", a_), ExcelFunction.CALL, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼笾Հłфᕆ", a_), ExcelFunction.ADDBAR, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼笾ՀโDॆ᱈", a_), ExcelFunction.ADDMENU, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼笾ՀB੄੆ш੊͌୎", a_), ExcelFunction.ADDCOMMAND, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼焾@łॄɆੈъLɎၐᵒᅔ", a_), ExcelFunction.ENABLECOMMAND, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾рBไцويL๎ὐᝒ", a_), ExcelFunction.CHECKCOMMAND, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ཀɂࡄɆੈъLɎၐᵒᅔ", a_), ExcelFunction.RENAMECOMMAND, a_2, 2);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼眾เᑂ݄نᭈ", a_), ExcelFunction.SHOWBAR, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ീقᅄɆш๊͌ᩎ", a_), ExcelFunction.DELETEMENU, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ീقᅄɆੈъLɎၐᵒᅔ", a_), ExcelFunction.DELETECOMMAND, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀBൄنᭈὊь᭎ᑐṒ", a_), ExcelFunction.GETCHARTITEM, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼瘾@ག੄Fୈъᕌ", a_), ExcelFunction.DIALOGBOX, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼猾рɂୄ", a_), ExcelFunction.CLEAN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼笾рᝂDᕆш", a_), ExcelFunction.MDETERM, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾ཀᕂDᕆᩈ๊", a_), ExcelFunction.MINVERSE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼爾ᑀགᅄ", a_), ExcelFunction.MMULT, a_2, 2);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾ീقᙄ", a_), ExcelFunction.FILES, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼漾ీᝂ", a_), ExcelFunction.IPMT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼漾ీᝂ", a_), ExcelFunction.PPMT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀൂᅄن", a_), ExcelFunction.COUNTA, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼績ཀBD୆Ɉ๊ᑌ", a_), ExcelFunction.CANCELKEY, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾ࡀᝂౄنᵈ๊", a_), ExcelFunction.INITIATE, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾၀ᙂDᑆᵈ", a_), ExcelFunction.REQUEST, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼瀾ੀق", a_), ExcelFunction.POKE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼朾рB၄ፆై", a_), ExcelFunction.EXECUTE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼稾ፀโౄॆࡈὊࡌ", a_), ExcelFunction.TERMINATE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ቀᝂфᕆᵈ", a_), ExcelFunction.RESTART, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼稾ീፂ", a_), ExcelFunction.HELP, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀłфᕆ", a_), ExcelFunction.GETBAR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派เ݂၄цᵈ", a_), ExcelFunction.PRODUCT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼績ɀᝂ", a_), ExcelFunction.FACT, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀBD୆Ո", a_), ExcelFunction.GETCELL, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀᑂ੄ᕆɈᡊᵌ๎ቐᙒ", a_), ExcelFunction.GETWORKSPACE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀᑂౄॆൈъᩌ", a_), ExcelFunction.GETWINDOW, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀ݂੄ц᱈يࡌŎՐ", a_), ExcelFunction.GETDOCUMENT, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼漾ፀూńቆੈὊ", a_), ExcelFunction.DPRODUCT, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ཀూୄፆైፊ᥌", a_), ExcelFunction.ISNONTEXT, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀൂ੄ፆై", a_), ExcelFunction.GETNOTE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ᕀق", a_), ExcelFunction.NOTE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款Հقፄᝆ", a_), ExcelFunction.STDEVP, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ፀፂ", a_), ExcelFunction.VARP, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼氾ᕀ݂Dᅆ᥈", a_), ExcelFunction.DSTDEVP, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼椾@ᅂᕄ", a_), ExcelFunction.DVARP, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼派ᑀൂل", a_), ExcelFunction.TRUNC, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ീూɄๆੈ੊Ō", a_), ExcelFunction.ISLOGICAL, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼簾เᙂୄፆࡈ", a_), ExcelFunction.DCOUNTA, a_2, 3);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ീقᅄɆୈ੊Ὄ", a_), ExcelFunction.DELETEBAR, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("格焾ፀقɄๆᩈὊࡌᵎ", a_), ExcelFunction.UNREGISTER, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("格氾Հూॄ୆ࡈ᥊", a_), ExcelFunction.USDOLLAR, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾ཀ݂݄", a_), ExcelFunction.FINDB, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼稾@ᅂلཆୈ", a_), ExcelFunction.SEARCHB, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ᅀགфцైॊ", a_), ExcelFunction.REPLACEB, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼稾݀ᝂ݄", a_), ExcelFunction.LEFTB, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瘾ـୂᅄՆ", a_), ExcelFunction.RIGHTB, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾Հł", a_), ExcelFunction.MIDB, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼稾ཀł", a_), ExcelFunction.LENB, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瀾ᑀൂńቆ᥈", a_), ExcelFunction.ROUNDUP, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瀾ᑀൂń͆و᱊͌", a_), ExcelFunction.ROUNDDOWN, a_2, 2);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼氾ɀ", a_), ExcelFunction.ASC, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼紾ɀ၂", a_), ExcelFunction.DBCS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績ཀࡂ", a_), ExcelFunction.RANK, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼笾ՀᅂDᑆᩈ", a_), ExcelFunction.ADDRESS, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᡀ၂癄煆祈", a_), ExcelFunction.DAYS360, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼瀾Հɂ᱄", a_), ExcelFunction.TODAY, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼笾̀", a_), ExcelFunction.VDB, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼稾Հੂфॆ", a_), ExcelFunction.MEDIAN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీፂᝄࡆൈṊ์᭎", a_), ExcelFunction.SUMPRODUCT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼瘾ཀୂ", a_), ExcelFunction.SINH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ቀୂ", a_), ExcelFunction.COSH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼績ཀୂ", a_), ExcelFunction.TANH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼氾ࡀൂൄ", a_), ExcelFunction.ASINH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼簾เ၂ൄ", a_), ExcelFunction.ACOSH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼款@ൂൄ", a_), ExcelFunction.ATANH, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼砾рᝂ", a_), ExcelFunction.DGET, a_2, 3);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼派рɂᅄɆوॊ݌੎ቐݒ", a_), ExcelFunction.CREATEOBJECT, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼瀾ീɂᅄๆՈ๊", a_), ExcelFunction.VOLATILE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼績ቀᝂDᕆᭈъὌ", a_), ExcelFunction.LASTERROR, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾ቀᝂ੄੆᱈ՊौN", a_), ExcelFunction.CUSTOMUNDO, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼樾ቀᝂ੄੆ᭈ๊ᵌ੎ၐݒ", a_), ExcelFunction.CUSTOMREPEAT, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瀾ፀโ၄୆ࡈࡊɌŎݐᙒݔ͖", a_), ExcelFunction.FORMULACONVERT, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀགౄॆɈɊ͌ॎṐ", a_), ExcelFunction.GETLINKINFO, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼稾᥀ᝂ݄ࡆᅈ", a_), ExcelFunction.TEXTBOX, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾݀ూ", a_), ExcelFunction.INFO, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼派เᙂᕄ", a_), ExcelFunction.GROUP, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀూ݄െైࡊ᥌", a_), ExcelFunction.GETOBJECT, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼紾", a_), ExcelFunction.DB, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼績ᑀ၂D", a_), ExcelFunction.PAUSE, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ቀᙂࡄɆ", a_), ExcelFunction.RESUME, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼派рቂ၄Ɇ݈ࡊᑌ", a_), ExcelFunction.FREQUENCY, a_2, 2);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼笾Հᝂ੄ࡆՈॊౌᵎ", a_), ExcelFunction.ADDTOOLBAR, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ീقᅄɆᵈъɌ͎ፐቒݔ", a_), ExcelFunction.DELETETOOLBAR, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼䨾㉀㝂⩄⩆཈㹊⍌ⱎ═㩒㩔㥖", a_), ExcelFunction.CustomFunction, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ቀقᅄፆوъŌൎၐŒ", a_), ExcelFunction.RESETTOOLBAR, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼椾@ག၄نᵈ๊", a_), ExcelFunction.EVALUATE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀᝂ੄ࡆՈॊౌᵎ", a_), ExcelFunction.GETTOOLBAR, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀᝂ੄ࡆՈ", a_), ExcelFunction.GETTOOL, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼漾рགॄๆ݈ొ์ݎᑐၒṔ", a_), ExcelFunction.SPELLINGCHECK, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼派ፀూᝄ楆ᵈቊᵌ੎", a_), ExcelFunction.ERRORTYPE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼漾ᅀᝂౄፆՈ๊", a_), ExcelFunction.APPTITLE, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼瘾ཀ݂੄၆ᵈɊ᥌͎ᑐ", a_), ExcelFunction.WINDOWTITLE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼績ᝀقᅄࡆو݊ཌ๎͐", a_), ExcelFunction.SAVETOOLBAR, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼焾@łॄɆᵈъɌ͎", a_), ExcelFunction.ENABLETOOL, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派р၂ᙄፆوъŌ", a_), ExcelFunction.PRESSTOOL, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ـੂᙄፆై᥊ь୎", a_), ExcelFunction.REGISTERID, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀᑂ੄ᕆɈॊɌNᩐ", a_), ExcelFunction.GETWORKBOOK, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼椾р݂Dᅆ", a_), ExcelFunction.AVEDEV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ᕀɂ歄͆Hᡊ᥌", a_), ExcelFunction.BETA_DIST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼ᄾᕀقᙄፆ", a_), ExcelFunction.T_TEST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼ᄾࡀൂፄ楆筈Ὂ", a_), ExcelFunction.T_INV_2T, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼朾ᅀూୄ楆ൈɊṌ᭎", a_), ExcelFunction.EXPON_DIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ᕀɂńๆᩈὊ", a_), ExcelFunction.BETADIST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼樾ፀూلࡆ݈ᵊࡌᵎՐ", a_), ExcelFunction.EUROCONVERT, a_2, 5);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼稾ـੂᙄፆై᥊捌َᕐ", a_), ExcelFunction.REGISTER_ID, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼眾เൂDፆHࡊ", a_), ExcelFunction.PHONETIC, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼渾ീ浂ᝄɆᡈṊࡌᱎՐ", a_), ExcelFunction.SQL_REQUEST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("眼瘾ቀ", a_), ExcelFunction.JIS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼績ీโф୆݈", a_), ExcelFunction.GAMMALN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼稾ᕀɂౄॆὈ", a_), ExcelFunction.BETAINV, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("缼瘾ཀూࡄ͆Hᡊ᥌", a_), ExcelFunction.BINOMDIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀ݂ౄᑆᵈ", a_), ExcelFunction.CHIDIST, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀੂୄᅆ", a_), ExcelFunction.CHIINV, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ీłౄॆ", a_), ExcelFunction.COMBIN, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ཀՂౄ͆ైՊ์੎", a_), ExcelFunction.CONFIDENCE, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼派ࡀᝂ݄ๆ݈ъL", a_), ExcelFunction.CRITBINOM, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼椾рൂ", a_), ExcelFunction.EVEN, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("砼朾ᅀూୄ͆Hᡊ᥌", a_), ExcelFunction.EXPONDIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼笾ࡀ၂ᅄ", a_), ExcelFunction.FDIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾ཀᕂ", a_), ExcelFunction.FINV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾ቀୂDᕆ", a_), ExcelFunction.FISHER, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瘾ቀୂDᕆHՊᭌ", a_), ExcelFunction.FISHERINV, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼猾เూᝄ", a_), ExcelFunction.FLOOR, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼績ీโф͆Hᡊ᥌", a_), ExcelFunction.GAMMADIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼績ీโфๆ݈ᵊ", a_), ExcelFunction.GAMMAINV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼稾ࡀགౄॆ่", a_), ExcelFunction.CEILING, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼显ᅀтDࡆшཊьᱎՐ", a_), ExcelFunction.HYPGEOMDIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـൂ੄ᕆшཊьᱎՐ", a_), ExcelFunction.LOGNORMDIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼瀾ـੂୄᅆ", a_), ExcelFunction.LOGINV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼稾ـłౄॆويौَɐݒ", a_), ExcelFunction.NEGBINOMDIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโńๆᩈὊ", a_), ExcelFunction.NORMDIST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโᙄ͆Hᡊ᥌", a_), ExcelFunction.NORMSDIST, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโౄॆὈ", a_), ExcelFunction.NORMINV, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ፀโᙄๆ݈ᵊ", a_), ExcelFunction.NORMSINV, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款@ൂńنᭈཊьᕎᑐ", a_), ExcelFunction.STANDARDIZE, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼笾Հ", a_), ExcelFunction.ODD, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀโ၄ፆ", a_), ExcelFunction.PERMUT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼瀾ࡀ၂ᙄࡆ݈", a_), ExcelFunction.POISSON, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼笾ࡀ၂ᅄ", a_), ExcelFunction.TDIST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("樼稾ࡀł၄୆Ո", a_), ExcelFunction.WEIBULL, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీᭂࡄṆ筈", a_), ExcelFunction.SUMXMY2, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీᭂ睄੆၈祊", a_), ExcelFunction.SUMX2MY2, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీᭂ睄ᝆ၈祊", a_), ExcelFunction.SUMX2PY2, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼眾ࡀᝂDᑆᵈ", a_), ExcelFunction.CHITEST, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ፀᅂD୆", a_), ExcelFunction.CORREL, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᝀɂᝄ", a_), ExcelFunction.COVAR, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					3,
					3
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼瀾ፀقلنᩈὊ", a_), ExcelFunction.FORECAST, a_2, 3);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("笼款р၂ᅄ", a_), ExcelFunction.FTEST, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼焾ᕀقᝄцైᭊ᥌", a_), ExcelFunction.INTERCEPT, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾@ᅂᙄࡆ݈", a_), ExcelFunction.PEARSON, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼氾၀", a_), ExcelFunction.RSQ, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款рᩂᵄ", a_), ExcelFunction.STEYX, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼猾เፂD", a_), ExcelFunction.SLOPE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼款р၂ᅄ", a_), ExcelFunction.TTEST, a_2, 4);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					3,
					3,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼派เł", a_), ExcelFunction.PROB, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ᝀ၂ᑄ", a_), ExcelFunction.DEVSQ, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾เโDن݈", a_), ExcelFunction.GEOMEAN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼績ፀโDن݈", a_), ExcelFunction.HARMEAN, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీ၂ᑄ", a_), ExcelFunction.SUMSQ, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(spr\u2372), 3),
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瘼樾ፀᝂ", a_), ExcelFunction.KURT, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼琾рᑂ", a_), ExcelFunction.SKEW, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("朼款р၂ᅄ", a_), ExcelFunction.ZTEST, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("焼績ፀтD", a_), ExcelFunction.LARGE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼爾@གॄ", a_), ExcelFunction.SMALL, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("氼樾@ᅂᅄๆՈ๊", a_), ExcelFunction.QUARTILE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀBDॆᵈɊŌ੎", a_), ExcelFunction.PERCENTILE, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼稾ፀBDॆᵈ᥊ౌŎᩐ", a_), ExcelFunction.PERCENTRANK, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瀾Հق", a_), ExcelFunction.MODE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼派ࡀโࡄɆࡈՊ", a_), ExcelFunction.TRIMMEAN, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("椼瘾ཀᕂ", a_), ExcelFunction.TINV, a_2, 2);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瀾ᝀੂDцويL๎ὐᝒ", a_), ExcelFunction.MOVIECOMMAND, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀโ੄ᅆH๊", a_), ExcelFunction.GETMOVIE, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ཀBфፆైՊౌ᭎ᑐ", a_), ExcelFunction.CONCATENATE, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼瀾ᙀقᝄ", a_), ExcelFunction.POWER, a_2, 2);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("洼瘾ᝀూᅄنൈཊौ๎Րቒ", a_), ExcelFunction.PIVOTADDDATA, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀፂౄᅆوὊ᥌๎ፐὒၔ", a_), ExcelFunction.GETPIVOTTABLE, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀፂౄᅆوὊୌَᑐὒᅔ", a_), ExcelFunction.GETPIVOTFIELD, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀፂౄᅆوὊь᭎ᑐṒ", a_), ExcelFunction.GETPIVOTITEM, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼績Հੂфॆᩈ", a_), ExcelFunction.RADIANS, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼稾ـᅂDɆᩈ", a_), ExcelFunction.DEGREES, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾̀ᝂ੄ፆࡈ݊", a_), ExcelFunction.SUBTOTAL, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2,
					1
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼樾ీੂ̈́", a_), ExcelFunction.SUMIF, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), new int[]
				{
					1,
					2
				})
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀൂᅄๆ཈", a_), ExcelFunction.COUNTIF, a_2, 2);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("縼瀾ᑀൂᅄՆՈ੊͌ю", a_), ExcelFunction.COUNTBLANK, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼簾рൂфᕆHъੌ੎Ր", a_), ExcelFunction.SCENARIOGET, a_2, 0);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼漾ᕀੂ੄ॆᩈ݊ьᱎՐRቔቖ൘", a_), ExcelFunction.OPTIONSLISTSGET, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("琼氾ᅀโᅄ", a_), ExcelFunction.ISPMT, a_2, 4);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᕀقńๆ཈", a_), ExcelFunction.DATEDIF, a_2, 3);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("礼績ᕀقᙄፆᭈɊ͌ࡎ", a_), ExcelFunction.DATESTRING, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼樾ీłDᕆᩈὊὌَὐᑒ", a_), ExcelFunction.NUMBERSTRING, a_2, 1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("漼瀾ీɂୄ", a_), ExcelFunction.ROMAN, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("爼漾рൂńๆࡈ݊Ɍࡎ", a_), ExcelFunction.OPENDIALOG, a_2, 1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼績ᝀقńๆࡈ݊Ɍࡎ", a_), ExcelFunction.SAVEDIALOG, a_2, 0);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("稼稾ᕀፂౄᅆوὊौ๎Րቒ", a_), ExcelFunction.GETPIVOTDATA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 2)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("甼显ᅀقᝄ୆HՊٌ", a_), ExcelFunction.HYPERLINK, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("簼椾рᅂфFై੊", a_), ExcelFunction.AVERAGEA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼績᥀ɂ", a_), ExcelFunction.MAXA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1),
				new sprᨳ(typeof(spr\u2372), 3)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("瀼瘾ཀɂ", a_), ExcelFunction.MINA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款Հقፄᝆࡈ", a_), ExcelFunction.STDEVPA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ፀፂф", a_), ExcelFunction.VARPA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("渼款Հقፄن", a_), ExcelFunction.STDEVA, a_2, -1);
			a_2 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦊ), 1)
			};
			FormulaUtil.ᜁ(RecordTableEnumerator.b("欼績ፀɂ", a_), ExcelFunction.VARA, a_2, -1);
			a_2 = new sprᨳ[0];
			FormulaUtil.ᜁ(RecordTableEnumerator.b("猼瀾ཀق", a_), ExcelFunction.NONE, a_2, -1);
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x003BEBBC File Offset: 0x003BDBBC
		private static void ᜁ()
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					IL_3C:
					Type[] array = spr\u17FF.ᜑ;
					int num = 0;
					int num2 = array.Length;
					int num3 = 3;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							switch (num3)
							{
							case 0:
								return;
							case 1:
								goto IL_52;
							case 2:
								if (num >= num2)
								{
									num3 = 0;
									continue;
								}
								FormulaUtil.ᜀ(array[num]);
								num++;
								num3 = 1;
								continue;
							case 3:
								goto IL_52;
							}
							goto IL_3C;
							IL_52:
							num3 = 2;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x003BEC54 File Offset: 0x003BDC54
		private static void ᜀ(Type A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						object[] customAttributes = A_0.GetCustomAttributes(typeof(spr\u1CD7), false);
						int num = 0;
						int num2 = customAttributes.Length;
						int num3 = 2;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_70;
							case 1:
								return;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									goto IL_70;
								}
								break;
							case 3:
							{
								if (num >= num2)
								{
									num3 = 1;
									continue;
								}
								if (true)
								{
								}
								spr\u1CD7 spr_u1CD = customAttributes[num] as spr\u1CD7;
								ConstructorInfo constructor = A_0.GetConstructor(new Type[]
								{
									typeof(string)
								});
								FormulaUtil.ErrorNameToConstructor.Add(spr_u1CD.ᜁ(), constructor);
								FormulaUtil.\u1716.Add(spr_u1CD.ᜀ(), spr_u1CD.ᜁ());
								FormulaUtil.\u1717.Add(spr_u1CD.ᜁ(), spr_u1CD.ᜀ());
								num++;
								num3 = 0;
								continue;
							}
							}
							break;
							IL_70:
							num3 = 3;
						}
					}
				}
				return;
			}
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x003BED70 File Offset: 0x003BDD70
		private void ᜀ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = this.ᜣ.Length;
					int num2 = 0;
					int num3 = 10;
					for (;;)
					{
						string[] array;
						int num8;
						string[] array2;
						switch (num3)
						{
						case 0:
							num2++;
							num3 = 8;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_129;
							default:
								goto IL_207;
							}
							break;
						case 2:
							goto IL_D9;
						case 3:
						{
							int num4;
							int num5;
							if (num4 >= num5)
							{
								num3 = 0;
								continue;
							}
							this.ᜤ[this.ᜣ[num2][num4]] = num2;
							num4++;
							num3 = 4;
							continue;
						}
						case 4:
							goto IL_1AC;
						case 5:
							goto IL_1AC;
						case 6:
						{
							int num6 = 0;
							int num7 = 0;
							array = null;
							num8 = num - 1;
							num3 = 13;
							continue;
						}
						case 7:
							goto IL_17B;
						case 8:
							goto IL_1CD;
						case 9:
						{
							if (num8 < 0)
							{
								num3 = 1;
								continue;
							}
							int num7 = this.ᜣ[num8].Length;
							int num6;
							num6 += num7;
							array2 = new string[num6];
							num3 = 11;
							continue;
						}
						case 10:
							goto IL_1CD;
						case 11:
							if (num8 < num - 1)
							{
								num3 = 14;
								continue;
							}
							goto IL_D9;
						case 12:
						{
							if (num2 >= num)
							{
								num3 = 6;
								continue;
							}
							int num4 = 0;
							int num5 = this.ᜣ[num2].Length;
							goto IL_129;
						}
						case 13:
							goto IL_17B;
						case 14:
						{
							int num7;
							array.CopyTo(array2, num7);
							num3 = 2;
							continue;
						}
						}
						break;
						IL_D9:
						this.ᜣ[num8].CopyTo(array2, 0);
						this.ᜥ[num8] = FormulaUtil.ᜀ(array2);
						array = array2;
						num8--;
						num3 = 7;
						continue;
						IL_129:
						num3 = 5;
						continue;
						IL_17B:
						num3 = 9;
						continue;
						IL_1AC:
						num3 = 3;
						continue;
						IL_1CD:
						num3 = 12;
					}
				}
				IL_207:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x003BEF8C File Offset: 0x003BDF8C
		private static SortedList ᜀ(string[] A_0)
		{
			int a_ = 18;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B7;
				case 1:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					SortedList sortedList;
					sortedList.Add(A_0[num2], null);
					num2++;
					num = 4;
					continue;
				}
				case 2:
				{
					SortedList sortedList;
					return sortedList;
				}
				case 4:
					goto IL_B7;
				case 5:
					goto IL_3C;
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
					break;
				default:
				{
					if (false)
					{
					}
					int num3 = A_0.Length;
					SortedList sortedList = new SortedList(new StringComparer(), num3);
					int num2 = 0;
					num = 0;
					continue;
				}
				}
				IL_B7:
				num = 1;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋ᵍ⑏⁑㵓㡕㽗⥙", a_));
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x003BF070 File Offset: 0x003BE070
		internal Ptg[] ᜀ(string A_0, int A_1, int A_2, IWorksheet A_3)
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
			Ptg[] a_ = this.ᜀ(A_0, A_3, null);
			IWorkbook workbook = A_3.Workbook;
			return this.ᜀ(a_, A_1, A_2, workbook);
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x003BF0C8 File Offset: 0x003BE0C8
		internal Ptg[] ᜀ(Ptg[] A_0, int A_1, int A_2, IWorkbook A_3)
		{
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					return A_0;
				case 1:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 0;
						continue;
					}
					sprẄ sprẄ = A_0[num2] as sprẄ;
					num = 6;
					continue;
				}
				case 2:
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
					break;
				case 3:
				{
					if (true)
					{
					}
					int num3 = A_0.Length;
					num2 = 0;
					num = 8;
					continue;
				}
				case 4:
					goto IL_68;
				case 5:
					goto IL_B0;
				case 6:
				{
					sprẄ sprẄ;
					if (sprẄ == null)
					{
						num = 7;
						continue;
					}
					goto IL_68;
				}
				case 7:
					A_0[num2] = A_0[num2].ConvertPtgToNPtg(A_3, A_1 - 1, A_2 - 1);
					num = 4;
					continue;
				case 8:
					goto IL_B0;
				}
				if (A_0 != null)
				{
					num = 3;
					continue;
				}
				break;
				IL_68:
				num2++;
				num = 5;
				continue;
				IL_B0:
				num = 1;
			}
			return A_0;
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x003BF1C8 File Offset: 0x003BE1C8
		internal Ptg[] ᜃ(string A_0)
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
			return this.ᜀ(A_0, null, null);
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x003BF20C File Offset: 0x003BE20C
		internal Ptg[] ᜀ(string A_0, IWorksheet A_1, Dictionary<string, string> A_2)
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
			return this.ᜁ(A_0, A_1, null, 0, A_2, ParseFormulaOptions.RootLevel, 0, 0);
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x003BF258 File Offset: 0x003BE258
		internal Ptg[] ᜀ(string A_0, IWorksheet A_1, Dictionary<string, string> A_2, int A_3, int A_4, bool A_5)
		{
			int num = 3;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_70;
				case 2:
					goto IL_5C;
				}
				while (A_5)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					num = 2;
					goto IL_0A;
				}
				num = 0;
			}
			IL_5C:
			ParseFormulaOptions parseFormulaOptions = ParseFormulaOptions.RootLevel | ParseFormulaOptions.UseR1C1;
			goto IL_73;
			IL_70:
			parseFormulaOptions = ParseFormulaOptions.RootLevel;
			IL_73:
			ParseFormulaOptions a_ = parseFormulaOptions;
			ParseParameters a_2 = new ParseParameters(A_1, A_2, A_5, A_3, A_4, this, this.ᜡ);
			this.ᜨ.ᜁ(A_0, null, 0, a_, a_2);
			return this.ᜨ.ᜀ().ToArray();
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x003BF310 File Offset: 0x003BE310
		internal Ptg[] ᜁ(string A_0, IWorksheet A_1, Dictionary<Type, sprᨳ> A_2, int A_3, Dictionary<string, string> A_4, ParseFormulaOptions A_5, int A_6, int A_7)
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
			bool r1C = (A_5 & ParseFormulaOptions.UseR1C1) != ParseFormulaOptions.None;
			ParseParameters a_ = new ParseParameters(A_1, A_4, r1C, A_6, A_7, this, this.ᜡ);
			this.ᜨ.ᜁ(A_0, A_2, A_3, A_5, a_);
			return this.ᜨ.ᜀ().ToArray();
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x003BF390 File Offset: 0x003BE390
		public string GetLeftUnaryOperand(string strFormula, int OpIndex)
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
			return FormulaUtil.GetOperand(strFormula, OpIndex, this.ᜤ, true);
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x003BF3DC File Offset: 0x003BE3DC
		public string GetRightUnaryOperand(string strFormula, int OpIndex)
		{
			string text;
			for (;;)
			{
				if (true)
				{
				}
				text = FormulaUtil.GetOperand(strFormula, OpIndex, this.ᜤ, false);
				int length = text.Length;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return text;
					case 1:
						text = '%' + text.Substring(0, length - 1);
						num = 0;
						continue;
					case 2:
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return text;
						default:
							if (false)
							{
							}
							if (text[length - 1] == '%')
							{
								num = 1;
								continue;
							}
							return text;
						}
						break;
					case 4:
						if (length > 0)
						{
							num = 2;
							continue;
						}
						return text;
					}
					break;
				}
			}
			return text;
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x003BF4A8 File Offset: 0x003BE4A8
		public string GetRightBinaryOperand(string strFormula, int iFirstChar, string operation)
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
			int num = (int)this.ᜤ[operation];
			return FormulaUtil.GetOperand(strFormula, iFirstChar - 1, this.ᜥ[num], false);
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x003BF508 File Offset: 0x003BE508
		public string GetFunctionOperand(string strFormula, int iFirstChar)
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
			SortedList arrBreakStrings = FormulaUtil.ᜀ(new string[]
			{
				this.OperandsSeparator
			});
			return FormulaUtil.GetOperand(strFormula, iFirstChar, arrBreakStrings, false);
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x003BF564 File Offset: 0x003BE564
		[CLSCompliant(false)]
		internal string ᜀ(spr᱒ A_0)
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
			return this.ᜀ(A_0, false);
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x003BF5A8 File Offset: 0x003BE5A8
		[CLSCompliant(false)]
		internal string ᜀ(spr᱒ A_0, bool A_1)
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
			Ptg[] a_ = A_0.ᜑ();
			return this.ᜀ(a_, A_0.\u1714(), A_0.\u1713(), A_1, false);
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x003BF600 File Offset: 0x003BE600
		[CLSCompliant(false)]
		internal string ᜀ(spr\u2614 A_0)
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
			Ptg[] a_ = A_0.ᜄ();
			return this.ᜀ(a_, 0, 0, false, false);
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x003BF650 File Offset: 0x003BE650
		[CLSCompliant(false)]
		internal string ᜀ(spr\u2614 A_0, int A_1, int A_2)
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
			return this.ᜀ(A_0, A_1, A_2, false, false);
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x003BF698 File Offset: 0x003BE698
		[CLSCompliant(false)]
		internal string ᜀ(spr\u2614 A_0, int A_1, int A_2, bool A_3, bool A_4)
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
			Ptg[] a_ = A_0.ᜄ();
			return this.ᜀ(a_, A_1, A_2, A_3, A_4);
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x003BF6E8 File Offset: 0x003BE6E8
		internal string ᜁ(Ptg[] A_0)
		{
			while (A_0 == null)
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
					return null;
				}
			}
			return this.ᜀ(A_0, 0, 0, false, false);
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x003BF738 File Offset: 0x003BE738
		internal string ᜀ(Ptg[] A_0, int A_1, int A_2, bool A_3, bool A_4)
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
			return this.ᜀ(A_0, A_1, A_2, A_3, null, A_4);
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x003BF784 File Offset: 0x003BE784
		internal string ᜀ(Ptg[] A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
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
			return this.ᜀ(A_0, A_1, A_2, A_3, A_4, false, A_5, null);
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x003BF7D0 File Offset: 0x003BE7D0
		internal string ᜀ(Ptg[] A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5, bool A_6, IWorksheet A_7)
		{
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					if (true)
					{
					}
					string text;
					Stack<object> stack;
					int num2;
					int num3;
					Ptg ptg;
					spr\u2086 spr_u;
					string operand;
					switch (num)
					{
					case 0:
						text += stack.Pop().ToString();
						num = 14;
						continue;
					case 1:
						goto IL_152;
					case 2:
						goto IL_110;
					case 3:
						goto IL_251;
					case 4:
						goto IL_110;
					case 5:
						if (stack.Count != 0)
						{
							num = 0;
							continue;
						}
						return text;
					case 7:
						num = 21;
						continue;
					case 8:
						if (num2 >= num3)
						{
							num = 18;
							continue;
						}
						ptg = A_0[num2];
						num = 17;
						continue;
					case 9:
						operand = spr_u.ᜀ(this, A_1, A_2, A_3);
						num = 20;
						continue;
					case 10:
						goto IL_8F;
					case 11:
						if (A_4 == null)
						{
							num = 19;
							continue;
						}
						goto IL_1BC;
					case 12:
						goto IL_20F;
					case 13:
						goto IL_1BC;
					case 14:
						return text;
					case 15:
						if (A_5)
						{
							num = 7;
							continue;
						}
						goto IL_D5;
					case 16:
						goto IL_20F;
					case 17:
					{
						if (!ptg.IsOperation)
						{
							num = 3;
							continue;
						}
						sprឯ sprឯ = (sprឯ)ptg;
						sprឯ.ᜀ(this, stack, A_6);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_251;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					}
					case 18:
						num = 5;
						continue;
					case 19:
						A_4 = this.ᜠ;
						num = 13;
						continue;
					case 20:
						goto IL_152;
					case 21:
						if (spr_u != null)
						{
							num = 9;
							continue;
						}
						goto IL_D5;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 11;
					continue;
					IL_D5:
					operand = ptg.ToString(this, A_1, A_2, A_3, A_4, A_6, A_7);
					num = 1;
					continue;
					IL_110:
					num = 8;
					continue;
					IL_152:
					FormulaUtil.PushOperandToStack(stack, operand);
					num = 12;
					continue;
					IL_1BC:
					A_0 = FormulaUtil.ᜀ(A_0);
					text = string.Empty;
					stack = new Stack<object>();
					num2 = 0;
					num3 = A_0.Length;
					num = 4;
					continue;
					IL_20F:
					num2++;
					num = 2;
					continue;
					IL_251:
					spr_u = (ptg as spr\u2086);
					num = 15;
				}
				IL_8F:
				return null;
			}
			}
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x003BFA7C File Offset: 0x003BEA7C
		internal void ᜃ(Ptg[] A_0)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						if (this.ᜡ.Version != ExcelVersion.Version2010)
						{
							goto IL_9C;
						}
						goto IL_DD;
					case 1:
						if (true)
						{
						}
						goto IL_BB;
					case 2:
						num = 0;
						continue;
					case 3:
					{
						spr\u1B43 spr_u1B;
						if (FormulaUtil.ᜁ(spr_u1B.ᜑ()))
						{
							num = 2;
							continue;
						}
						goto IL_DD;
					}
					case 4:
					{
						spr\u1B43 spr_u1B;
						if (spr_u1B == null)
						{
							goto IL_DD;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9C;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					}
					case 5:
					{
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						Ptg ptg = A_0[num2];
						spr\u1B43 spr_u1B = ptg as spr\u1B43;
						num = 4;
						continue;
					}
					case 6:
						goto IL_BB;
					case 7:
						return;
					case 8:
						goto IL_A5;
					case 9:
						num = 3;
						continue;
					case 11:
						return;
					}
					if (A_0 == null)
					{
						num = 11;
						continue;
					}
					num2 = 0;
					num3 = A_0.Length;
					num = 6;
					continue;
					IL_9C:
					num = 8;
					continue;
					IL_BB:
					num = 5;
					continue;
					IL_DD:
					num2++;
					num = 1;
				}
				return;
				IL_A5:
				throw new NotSupportedException(RecordTableEnumerator.b("款⥀♂敄ⅆ♈㥊⁌㩎㵐㉒畔㹖⩘筚㍜ぞᕠ䍢ᙤቦᥨ᭪ɬᵮհᙲᅴ坶ၸᕺ嵼୾Ꞇ\udf88ﾌﲎﲒﮔ", a_));
			}
			}
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x003BFBF0 File Offset: 0x003BEBF0
		public List<string> SplitArray(string strFormula, string strSeparator)
		{
			int a_ = 15;
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
						goto IL_B4;
					case 2:
						goto IL_10E;
					case 3:
					{
						List<string> list;
						return list;
					}
					case 4:
						if (strFormula.Length == 0)
						{
							num = 2;
							continue;
						}
						goto IL_113;
					case 5:
						goto IL_58;
					case 6:
						goto IL_B4;
					case 7:
					{
						int num2;
						int length;
						if (num2 >= length)
						{
							num = 3;
							continue;
						}
						SortedList sortedList;
						string operand = FormulaUtil.GetOperand(strFormula, num2, sortedList, false);
						num2 += operand.Length + 1;
						List<string> list;
						list.Add(operand);
						num = 1;
						continue;
					}
					}
					if (strFormula == null)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
					IL_B4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_113:
						List<string> list = new List<string>();
						SortedList sortedList = new SortedList(1);
						sortedList.Add(strSeparator, null);
						int num2 = -1;
						int length = strFormula.Length;
						num = 6;
						break;
					}
					default:
						if (false)
						{
						}
						num = 7;
						break;
					}
				}
				IL_58:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㙄㍆㭈ൊ≌㵎㱐♒㥔㙖", a_));
				IL_10E:
				throw new ArgumentException(RecordTableEnumerator.b("㙄㍆㭈ൊ≌㵎㱐♒㥔㙖祘癚絜ⱞᕠᅢ౤०๨䭪๬๮ὰᵲᩴͶ奸᥺᡼彾", a_));
			}
			}
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x003BFD4C File Offset: 0x003BED4C
		internal bool ᜀ(Ptg A_0, int[] A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 13;
				bool result;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						int num3;
						if (num2 != num3)
						{
							num = 1;
							continue;
						}
						return result;
					}
					case 1:
					{
						int num3;
						spr\u25A0 spr_u25A;
						spr_u25A.ᜀ((ushort)(num3 + 1));
						result = true;
						num = 12;
						continue;
					}
					case 2:
						if (FormulaUtil.ᜀ(FormulaUtil.\u171C, A_0.TokenCode) != -1)
						{
							num = 11;
							continue;
						}
						return result;
					case 3:
					{
						spr\u1B76 spr_u1B;
						int num4;
						spr_u1B.ᜀ((ushort)(num4 + 1));
						result = true;
						num = 9;
						continue;
					}
					case 4:
						if (FormulaUtil.ᜀ(FormulaUtil.\u171D, A_0.TokenCode) != -1)
						{
							num = 6;
							continue;
						}
						num = 2;
						continue;
					case 5:
					{
						spr\u1B76 spr_u1B;
						int num5 = (int)(spr_u1B.ᜂ() - 1);
						int num4 = A_1[num5];
						num = 8;
						continue;
					}
					case 6:
					{
						if (true)
						{
						}
						spr\u25A0 spr_u25A = (spr\u25A0)A_0;
						int num2 = (int)(spr_u25A.ᜀ() - 1);
						int num3 = A_1[num2];
						num = 0;
						continue;
					}
					case 7:
						return false;
					case 8:
					{
						int num4;
						int num5;
						if (num4 != num5)
						{
							num = 3;
							continue;
						}
						return result;
					}
					case 9:
						goto IL_133;
					case 10:
					{
						spr\u1B76 spr_u1B;
						if (this.ᜡ.IsLocalReference((int)spr_u1B.ᜃ()))
						{
							num = 5;
							continue;
						}
						return result;
					}
					case 11:
					{
						spr\u1B76 spr_u1B = (spr\u1B76)A_0;
						num = 10;
						continue;
					}
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DF;
						default:
							goto IL_199;
						}
						break;
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					IL_DF:
					result = false;
					num = 4;
				}
				return false;
				IL_133:
				return result;
				IL_199:
				if (false)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x003BFF30 File Offset: 0x003BEF30
		internal bool ᜀ(Ptg A_0, IDictionary<int, int> A_1)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 15;
				bool result;
				for (;;)
				{
					int num2;
					int num5;
					int num6;
					int num7;
					switch (num)
					{
					case 0:
					{
						int num3;
						num2 = A_1[num3];
						goto IL_1F1;
					}
					case 1:
					{
						int num4;
						if (num4 != num5)
						{
							num = 7;
							continue;
						}
						return result;
					}
					case 2:
					{
						int num3;
						if (!A_1.ContainsKey(num3))
						{
							num = 11;
							continue;
						}
						num = 0;
						continue;
					}
					case 3:
					{
						int num3;
						num2 = num3;
						goto IL_1F1;
					}
					case 4:
					{
						int num4;
						num6 = num4;
						goto IL_278;
					}
					case 5:
						num = 4;
						continue;
					case 6:
						if (FormulaUtil.ᜀ(FormulaUtil.\u171C, A_0.TokenCode) != -1)
						{
							num = 8;
							continue;
						}
						return result;
					case 7:
					{
						spr\u25A0 spr_u25A;
						spr_u25A.ᜀ((ushort)(num5 + 1));
						result = true;
						num = 21;
						continue;
					}
					case 8:
					{
						spr\u1B76 spr_u1B = (spr\u1B76)A_0;
						num = 10;
						continue;
					}
					case 9:
					{
						int num4;
						if (!A_1.ContainsKey(num4))
						{
							num = 5;
							continue;
						}
						num = 14;
						continue;
					}
					case 10:
					{
						spr\u1B76 spr_u1B;
						if (this.ᜡ.IsLocalReference((int)spr_u1B.ᜃ()))
						{
							num = 12;
							continue;
						}
						return result;
					}
					case 11:
						num = 3;
						continue;
					case 12:
					{
						spr\u1B76 spr_u1B;
						int num3 = (int)(spr_u1B.ᜂ() - 1);
						num = 2;
						continue;
					}
					case 13:
					{
						int num3;
						if (num7 != num3)
						{
							num = 18;
							continue;
						}
						return result;
					}
					case 14:
					{
						int num4;
						num6 = A_1[num4];
						goto IL_278;
					}
					case 16:
						if (FormulaUtil.ᜀ(FormulaUtil.\u171D, A_0.TokenCode) != -1)
						{
							num = 20;
							continue;
						}
						num = 6;
						continue;
					case 17:
						goto IL_90;
					case 18:
					{
						spr\u1B76 spr_u1B;
						spr_u1B.ᜀ((ushort)(num7 + 1));
						result = true;
						num = 19;
						continue;
					}
					case 19:
						goto IL_1B5;
					case 20:
					{
						spr\u25A0 spr_u25A = (spr\u25A0)A_0;
						int num4 = (int)(spr_u25A.ᜀ() - 1);
						num = 9;
						continue;
					}
					case 21:
						return result;
					}
					if (A_0 == null)
					{
						num = 17;
						continue;
					}
					result = false;
					num = 16;
					continue;
					IL_1F3:
					num = 13;
					continue;
					IL_278:
					num5 = num6;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F3;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					IL_1F1:
					num7 = num2;
					goto IL_1F3;
				}
				IL_90:
				throw new ArgumentNullException(RecordTableEnumerator.b("〿㙁⍃", a_));
				IL_1B5:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x003C01F4 File Offset: 0x003BF1F4
		internal bool ᜀ(Ptg[] A_0, IDictionary<int, int> A_1)
		{
			int a_ = 14;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9B;
				case 1:
					goto IL_D0;
				case 2:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E4;
					default:
					{
						if (false)
						{
						}
						bool flag;
						flag |= this.ᜀ(A_0[num2], A_1);
						num2++;
						num = 0;
						continue;
					}
					}
					break;
				}
				case 3:
				{
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					bool flag = false;
					int num2 = 0;
					int num3 = A_0.Length;
					goto IL_E4;
				}
				case 5:
				{
					bool flag;
					return flag;
				}
				case 6:
					goto IL_9B;
				case 7:
					return false;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				num = 3;
				continue;
				IL_9B:
				num = 2;
				continue;
				IL_E4:
				num = 6;
			}
			return false;
			IL_D0:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁃⽅⭇щ⥋㥍᥏㱑こ㍕⁗", a_));
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x003C02F4 File Offset: 0x003BF2F4
		internal bool ᜁ(Ptg[] A_0, int[] A_1)
		{
			int a_ = 19;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D0;
				case 1:
				{
					bool flag;
					return flag;
				}
				case 2:
					goto IL_9B;
				case 3:
					return false;
				case 4:
					goto IL_9B;
				case 5:
				{
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					bool flag = false;
					int num2 = 0;
					int num3 = A_0.Length;
					goto IL_E4;
				}
				case 6:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E4;
					default:
					{
						if (false)
						{
						}
						bool flag;
						flag |= this.ᜀ(A_0[num2], A_1);
						num2++;
						num = 2;
						continue;
					}
					}
					break;
				}
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
				IL_9B:
				num = 6;
				continue;
				IL_E4:
				num = 4;
			}
			return false;
			IL_D0:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⡈㥊㽌Ŏ㑐⑒᱔㥖㵘㹚╜", a_));
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x003C03F4 File Offset: 0x003BF3F4
		public void SetSeparators(char operandsSeparator, char arrayRowsSeparator)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				string text;
				string text2;
				for (;;)
				{
					text = operandsSeparator.ToString();
					text2 = arrayRowsSeparator.ToString();
					int num = 2;
					for (;;)
					{
						int num2;
						int num3;
						string[] a_2;
						string[] a_3;
						switch (num)
						{
						case 0:
							num = 6;
							continue;
						case 1:
							return;
						case 2:
							if (text == this.ᜧ)
							{
								num = 0;
								continue;
							}
							goto IL_9D;
						case 3:
							goto IL_141;
						case 4:
						{
							if (num2 >= num3)
							{
								goto IL_156;
							}
							SortedList a_ = this.ᜥ[num2];
							this.ᜀ(a_, a_2, a_3);
							num2++;
							num = 3;
							continue;
						}
						case 5:
							goto IL_141;
						case 6:
							if (text2 == this.ᜦ)
							{
								num = 1;
								continue;
							}
							goto IL_9D;
						case 7:
							goto IL_162;
						}
						break;
						IL_9D:
						a_2 = new string[]
						{
							this.ᜧ,
							this.ᜦ
						};
						a_3 = new string[]
						{
							text,
							text2
						};
						this.ᜀ(this.ᜤ, a_2, a_3);
						num2 = 0;
						num3 = this.ᜥ.Length;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_156:
							num = 7;
							continue;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						IL_141:
						num = 4;
					}
				}
				return;
				IL_162:
				this.ᜧ = text;
				this.ᜦ = text2;
				this.ᜨ.ᜀ(operandsSeparator, arrayRowsSeparator);
				return;
			}
			}
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x003C058C File Offset: 0x003BF58C
		private void ᜀ(IDictionary A_0, string[] A_1, string[] A_2)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
					{
						object[] array;
						string key;
						array[num2] = A_0[key];
						A_0.Remove(key);
						num = 17;
						continue;
					}
					case 1:
						goto IL_20F;
					case 2:
						goto IL_1C8;
					case 3:
					{
						int num4;
						if (num3 >= num4)
						{
							num = 14;
							continue;
						}
						goto IL_1A5;
					}
					case 4:
						goto IL_102;
					case 5:
						num3 = 0;
						num = 2;
						continue;
					case 7:
					{
						bool flag;
						if (flag)
						{
							num = 0;
							continue;
						}
						goto IL_2A1;
					}
					case 8:
					{
						int num4;
						if (num4 != A_2.Length)
						{
							num = 1;
							continue;
						}
						object[] array = new object[num4];
						bool[] array2 = new bool[num4];
						num2 = 0;
						num = 9;
						continue;
					}
					case 9:
						goto IL_9F;
					case 10:
						goto IL_1C8;
					case 11:
					{
						int num4;
						if (num2 >= num4)
						{
							num = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A5;
						default:
						{
							if (false)
							{
							}
							string key = A_1[num2];
							bool[] array2;
							bool flag = array2[num2] = A_0.Contains(key);
							num = 7;
							continue;
						}
						}
						break;
					}
					case 12:
						goto IL_15C;
					case 13:
						goto IL_9A;
					case 14:
						return;
					case 15:
						goto IL_146;
					case 16:
					{
						if (A_2 == null)
						{
							num = 4;
							continue;
						}
						int num4 = A_1.Length;
						num = 8;
						continue;
					}
					case 17:
						goto IL_2A1;
					case 18:
					{
						bool[] array2;
						if (array2[num3])
						{
							num = 21;
							continue;
						}
						goto IL_15C;
					}
					case 19:
						goto IL_9F;
					case 20:
						if (A_1 == null)
						{
							num = 15;
							continue;
						}
						num = 16;
						continue;
					case 21:
					{
						object[] array;
						A_0.Add(A_2[num3], array[num3]);
						num = 12;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 13;
						continue;
					}
					if (true)
					{
					}
					num = 20;
					continue;
					IL_9F:
					num = 11;
					continue;
					IL_15C:
					num3++;
					num = 10;
					continue;
					IL_1A5:
					num = 18;
					continue;
					IL_1C8:
					num = 3;
					continue;
					IL_2A1:
					num2++;
					num = 19;
				}
				IL_9A:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⵀ⩂㙄㍆", a_));
				IL_102:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄ॆⱈ㱊ٌ⩎⡐", a_));
				IL_146:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄ࡆ╈⽊ٌ⩎⡐", a_));
				IL_20F:
				throw new ArgumentException(RecordTableEnumerator.b("⁀ㅂ㝄ࡆ╈⽊ٌ⩎⡐獒㑔㥖㵘筚㱜ⵞ፠ⵢdၦ≨๪ᑬ佮ᕰᱲ啴᥶ᙸེ嵼᱾愈ﮊ뎒滛붜햠쮢삤햦", a_));
			}
			}
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x003C0850 File Offset: 0x003BF850
		internal static void ᜀ(Ptg[] A_0, bool[] A_1)
		{
			int num = 1;
			for (;;)
			{
				sprẄ sprẄ;
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_8A;
				case 2:
					if (sprẄ != null)
					{
						num = 6;
						continue;
					}
					goto IL_41;
				case 3:
					goto IL_41;
				case 4:
					return;
				case 5:
					if (num2 >= num3)
					{
						num = 7;
						continue;
					}
					sprẄ = (A_0[num2] as sprẄ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_87;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 6:
					goto IL_87;
				case 7:
					return;
				case 8:
					goto IL_8A;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num2 = 0;
				num3 = A_0.Length;
				num = 8;
				continue;
				IL_41:
				num2++;
				num = 0;
				continue;
				IL_8A:
				num = 5;
				continue;
				IL_87:
				if (true)
				{
				}
				A_1[(int)sprẄ.ᜁ()] = true;
				num = 3;
			}
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x003C0944 File Offset: 0x003BF944
		internal static bool ᜀ(Ptg[] A_0, int[] A_1)
		{
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					if (true)
					{
					}
					result = false;
					int num = 0;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							if (A_0 != null)
							{
								num = 2;
								continue;
							}
							return result;
						case 1:
						{
							sprẄ sprẄ;
							if (sprẄ != null)
							{
								num = 4;
								continue;
							}
							goto IL_5F;
						}
						case 2:
							num2 = 0;
							num3 = A_0.Length;
							num = 7;
							continue;
						case 3:
							return result;
						case 4:
						{
							sprẄ sprẄ;
							int num4 = (int)sprẄ.ᜁ();
							int num5 = A_1[num4];
							sprẄ.ᜂ((ushort)num5);
							result = true;
							num = 8;
							continue;
						}
						case 5:
							goto IL_CE;
						case 6:
							goto IL_C2;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CE;
							default:
								if (false)
								{
								}
								goto IL_C2;
							}
							break;
						case 8:
							goto IL_5F;
						}
						break;
						IL_5F:
						num2++;
						num = 6;
						continue;
						IL_C2:
						num = 5;
						continue;
						IL_CE:
						if (num2 >= num3)
						{
							num = 3;
						}
						else
						{
							sprẄ sprẄ = A_0[num2] as sprẄ;
							num = 1;
						}
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x003C0A68 File Offset: 0x003BFA68
		private Ptg ᜀ(string A_0, IWorksheet A_1, Dictionary<string, string> A_2, ParseFormulaOptions A_3)
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
			return this.ᜀ(A_0, A_1, null, 0, A_2, A_3, 0, 0);
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x003C0AB4 File Offset: 0x003BFAB4
		private Ptg ᜀ(string A_0, IWorksheet A_1, Dictionary<Type, sprᨳ> A_2, int A_3, Dictionary<string, string> A_4, ParseFormulaOptions A_5, int A_6, int A_7)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				bool flag;
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				FormulaToken a_2;
				double num2;
				for (;;)
				{
					flag = ((A_5 & ParseFormulaOptions.UseR1C1) != ParseFormulaOptions.None);
					int num = 18;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 23;
							continue;
						case 1:
						{
							bool flag2;
							if (!flag2)
							{
								num = 39;
								continue;
							}
							goto IL_193;
						}
						case 2:
							num = 10;
							continue;
						case 3:
							goto IL_F6;
						case 4:
							goto IL_65E;
						case 5:
							goto IL_193;
						case 6:
							if (A_4 != null)
							{
								num = 26;
								continue;
							}
							goto IL_45C;
						case 7:
							A_0 = A_0.Replace(text, A_4[text]);
							num = 34;
							continue;
						case 8:
							if (!this.ᜡ.ThrowOnUnknownNames)
							{
								num = 40;
								continue;
							}
							goto IL_2E2;
						case 9:
							if (this.IsCellRange3D(A_0, flag, out text, out text2, out text3, out text4, out text5))
							{
								num = 16;
								continue;
							}
							num = 21;
							continue;
						case 10:
							if (A_4.ContainsKey(text))
							{
								num = 15;
								continue;
							}
							goto IL_660;
						case 11:
							if (A_0[0] == '{')
							{
								num = 30;
								continue;
							}
							num = 33;
							continue;
						case 12:
							goto IL_18E;
						case 13:
							if (A_1 != null)
							{
								num = 36;
								continue;
							}
							goto IL_4BD;
						case 14:
							if (A_0[0] == '"')
							{
								num = 29;
								continue;
							}
							num = 11;
							continue;
						case 15:
							text = A_4[text];
							num = 4;
							continue;
						case 16:
							a_2 = spr\u1BFD.ᜀ(FormulaUtil.ᜀ(typeof(spr\u1BFD), 0, A_2, A_3, A_5));
							num = 6;
							continue;
						case 17:
						{
							bool flag2;
							if (flag2)
							{
								num = 22;
								continue;
							}
							try
							{
								bool.Parse(A_0);
								return FormulaUtil.ᜀ(FormulaToken.tBoolean, A_0);
							}
							catch (FormatException)
							{
								goto IL_21A;
							}
							goto IL_2E2;
							IL_21A:
							num = 8;
							continue;
						}
						case 18:
							if (A_0.Length == 0)
							{
								num = 3;
								continue;
							}
							num = 14;
							continue;
						case 19:
							if (true)
							{
							}
							a_2 = sprᣋ.ᜀ(FormulaUtil.ᜀ(typeof(sprᣋ), 0, A_2, A_3, A_5));
							num = 35;
							continue;
						case 20:
						{
							bool flag2;
							if (flag2)
							{
								num = 32;
								continue;
							}
							goto IL_3F1;
						}
						case 21:
							if (FormulaUtil.IsCell3D(A_0, flag, out text, out text2, out text3))
							{
								num = 19;
								continue;
							}
							num = 28;
							continue;
						case 22:
							goto IL_1B2;
						case 23:
							if (num2 >= 0.0)
							{
								num = 31;
								continue;
							}
							goto IL_3F1;
						case 24:
							if (num2 <= 65535.0)
							{
								num = 0;
								continue;
							}
							goto IL_3F1;
						case 25:
							a_2 = spr\u25A0.ᜀ(FormulaUtil.ᜀ(typeof(spr\u25A0), 0, A_2, A_3, A_5));
							num = 13;
							continue;
						case 26:
							num = 38;
							continue;
						case 27:
							if (FormulaUtil.IsCell(A_0, flag, out text2, out text3))
							{
								num = 12;
								continue;
							}
							num = 9;
							continue;
						case 28:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_32B;
							default:
							{
								if (false)
								{
								}
								if (FormulaUtil.ᜀ(A_0, this.ᜡ, A_1))
								{
									num = 25;
									continue;
								}
								bool flag2 = double.TryParse(A_0, NumberStyles.Integer, null, out num2);
								num = 20;
								continue;
							}
							}
							break;
						case 29:
							goto IL_612;
						case 30:
							goto IL_597;
						case 31:
							goto IL_123;
						case 32:
							num = 24;
							continue;
						case 33:
							if (this.ᜀ(A_0, flag, out text2, out text3, out text4, out text5))
							{
								num = 37;
								continue;
							}
							num = 27;
							continue;
						case 34:
							goto IL_56C;
						case 35:
							if (A_4 != null)
							{
								num = 2;
								continue;
							}
							goto IL_660;
						case 36:
							goto IL_161;
						case 37:
							goto IL_393;
						case 38:
							if (A_4.ContainsKey(text))
							{
								num = 7;
								continue;
							}
							goto IL_45C;
						case 39:
						{
							bool flag2 = double.TryParse(A_0, NumberStyles.Any, this.ᜠ, out num2);
							num = 5;
							continue;
						}
						case 40:
							goto IL_242;
						}
						break;
						IL_193:
						num = 17;
						continue;
						IL_3F1:
						num = 1;
					}
				}
				IL_F6:
				goto IL_32B;
				IL_123:
				return FormulaUtil.ᜀ(FormulaToken.tInteger, A_0);
				IL_161:
				return FormulaUtil.ᜀ(a_2, new object[]
				{
					A_0,
					this.ᜡ,
					A_1
				});
				IL_18E:
				a_2 = sprᦊ.ᜀ(FormulaUtil.ᜀ(typeof(sprᦊ), 0, A_2, A_3, A_5));
				return FormulaUtil.ᜀ(a_2, A_6, A_7, text2, text3, flag);
				IL_1B2:
				return FormulaUtil.ᜀ(FormulaToken.tNumber, new object[]
				{
					num2
				});
				IL_242:
				this.ᜡ.Names.Add(A_0);
				a_2 = spr\u25A0.ᜀ(FormulaUtil.ᜀ(typeof(spr\u25A0), 0, A_2, A_3, A_5));
				return FormulaUtil.ᜀ(a_2, A_0, this.ᜡ);
				IL_2E2:
				throw new ArgumentException(RecordTableEnumerator.b("礹崻倽朿㙁摃㙅⥇㡉㽋⭍灏㑑㭓⑕㕗⽙せ㽝婟䉡", a_) + A_0);
				IL_32B:
				return FormulaUtil.ᜁ(FormulaToken.tMissingArgument);
				IL_393:
				a_2 = sprᲔ.ᜀ(FormulaUtil.ᜀ(typeof(sprᲔ), 0, A_2, A_3, A_5));
				return FormulaUtil.ᜀ(a_2, A_6, A_7, text2, text3, text4, text5, flag, this.ᜡ);
				IL_45C:
				int num3 = this.ᜡ.AddSheetReference(text);
				return FormulaUtil.ᜀ(a_2, new object[]
				{
					A_6,
					A_7,
					num3,
					text2,
					text3,
					text4,
					text5,
					flag
				});
				IL_4BD:
				return FormulaUtil.ᜀ(a_2, A_0, this.ᜡ);
				IL_56C:
				goto IL_45C;
				IL_597:
				a_2 = spr\u2372.ᜀ(FormulaUtil.ᜀ(typeof(spr\u2372), 2, A_2, A_3, A_5));
				return FormulaUtil.ᜀ(a_2, new object[]
				{
					A_0,
					this
				});
				IL_612:
				return FormulaUtil.ᜀ(FormulaToken.tStringConstant, A_0.Substring(1, A_0.Length - 2));
				IL_65E:
				IL_660:
				int num4 = this.ᜡ.AddSheetReference(text);
				return FormulaUtil.ᜀ(a_2, new object[]
				{
					A_6,
					A_7,
					num4,
					text2,
					text3,
					flag
				});
			}
			}
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x003C11E4 File Offset: 0x003C01E4
		private static string ᜂ(string A_0)
		{
			int num = 6;
			for (;;)
			{
				int length;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 1:
					return A_0;
				case 2:
					num = 7;
					continue;
				case 3:
					goto IL_3F;
				case 4:
					goto IL_6B;
				case 5:
					if (length >= 2)
					{
						num = 0;
						continue;
					}
					return A_0;
				case 7:
					if ('\'' == A_0[length - 1])
					{
						num = 8;
						continue;
					}
					return A_0;
				case 8:
					if (true)
					{
					}
					A_0 = A_0.Substring(1, length - 2);
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				length = A_0.Length;
				num = 5;
				continue;
				IL_6B:
				if (A_0[0] != '\'')
				{
					return A_0;
				}
				num = 2;
			}
			IL_3F:
			return null;
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x003C12E4 File Offset: 0x003C02E4
		internal static Ptg[] ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
		{
			List<Ptg> list;
			for (;;)
			{
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5A:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					list = new List<Ptg>();
					num2 = 0;
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_4E;
					case 1:
						if (num2 >= A_1)
						{
							goto IL_5A;
						}
						list.Add(FormulaUtil.ᜀ(A_0, ref num2, A_2));
						num = 2;
						continue;
					case 2:
						goto IL_4E;
					case 3:
						goto IL_62;
					}
					break;
					IL_4E:
					num = 1;
				}
			}
			IL_62:
			return list.ToArray();
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x003C1380 File Offset: 0x003C0380
		internal static Ptg[] ᜀ(DataProvider A_0, int A_1, int A_2, out int A_3, ExcelVersion A_4)
		{
			List<Ptg> list;
			for (;;)
			{
				list = new List<Ptg>();
				int num = A_1 + A_2;
				int num2 = 5;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_D1;
					case 1:
						if (num3 >= list.Count)
						{
							num2 = 0;
							continue;
						}
						num2 = 7;
						continue;
					case 2:
						goto IL_8C;
					case 3:
						goto IL_B2;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8C;
						default:
							if (false)
							{
							}
							A_3 = A_1;
							num3 = 0;
							num2 = 3;
							continue;
						}
						break;
					case 5:
						goto IL_8E;
					case 6:
						if (A_1 >= num)
						{
							num2 = 4;
							continue;
						}
						list.Add(FormulaUtil.ᜀ(A_0, ref A_1, A_4));
						num2 = 9;
						continue;
					case 7:
						if (list[num3] is sprḝ)
						{
							num2 = 2;
							continue;
						}
						goto IL_FA;
					case 8:
						goto IL_B2;
					case 9:
						goto IL_8E;
					case 10:
						goto IL_FA;
					}
					break;
					IL_8C:
					A_3 = (list[num3] as sprḝ).ᜀ(A_0, A_3);
					num2 = 10;
					continue;
					IL_8E:
					if (true)
					{
					}
					num2 = 6;
					continue;
					IL_B2:
					num2 = 1;
					continue;
					IL_FA:
					num3++;
					num2 = 8;
				}
			}
			IL_D1:
			return list.ToArray();
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x003C14D0 File Offset: 0x003C04D0
		internal static byte[] ᜀ(Ptg[] A_0, ExcelVersion A_1)
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
				if (A_0 == null)
				{
					return null;
				}
				break;
			}
			int num;
			return FormulaUtil.ᜀ(A_0, out num, A_1);
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x003C151C File Offset: 0x003C051C
		internal static byte[] ᜀ(Ptg[] A_0, out int A_1, ExcelVersion A_2)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 3;
				spr\u177A spr_u177A;
				for (;;)
				{
					int num2;
					int num3;
					int num5;
					switch (num)
					{
					case 0:
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						goto IL_18B;
					case 1:
					{
						A_1 = spr_u177A.ᜁ();
						int num4 = 0;
						num = 4;
						continue;
					}
					case 2:
						goto IL_D5;
					case 4:
						goto IL_14B;
					case 5:
						goto IL_16A;
					case 6:
						goto IL_69;
					case 7:
					{
						int num4;
						spr\u177A a_2 = (A_0[num4] as spr\u2372).ᜂ();
						spr_u177A.ᜀ(a_2);
						num = 2;
						continue;
					}
					case 8:
						goto IL_16A;
					case 9:
						goto IL_168;
					case 10:
						goto IL_14B;
					case 11:
					{
						int num4;
						if (A_0[num4] is spr\u2372)
						{
							num = 7;
							continue;
						}
						goto IL_D5;
					}
					case 12:
					{
						int num4;
						if (num4 >= num5)
						{
							num = 9;
							continue;
						}
						num = 11;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 6;
						continue;
					}
					spr_u177A = new spr\u177A(true);
					num5 = A_0.Length;
					num2 = 0;
					num3 = num5;
					num = 8;
					continue;
					IL_D5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_18B:
						spr_u177A.ᜀ(A_0[num2].ToByteArray(A_2));
						num2++;
						num = 5;
						continue;
					default:
					{
						if (false)
						{
						}
						int num4;
						num4++;
						num = 10;
						continue;
					}
					}
					IL_14B:
					num = 12;
					continue;
					IL_16A:
					num = 0;
				}
				IL_69:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("嘶䬸䤺椼倾⩀♂⭄㑆", a_));
				IL_168:
				return spr_u177A.ᜀ();
			}
			}
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x003C16DC File Offset: 0x003C06DC
		public static string GetLeftBinaryOperand(string strFormula, int OpIndex)
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
			return FormulaUtil.GetOperand(strFormula, OpIndex, FormulaUtil.\u1714, true);
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x003C1724 File Offset: 0x003C0724
		public static int FindCorrespondingBracket(string strFormula, int BracketPos)
		{
			int a_ = 14;
			int num = 0;
			int a_2;
			char[] a_3;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (FormulaUtil.ᜀ(FormulaUtil.CloseBrackets, strFormula[BracketPos]) != -1)
					{
						num = 3;
						continue;
					}
					goto IL_CD;
				case 2:
					goto IL_85;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						if (false)
						{
						}
						a_2 = -1;
						a_3 = FormulaUtil.CloseBrackets;
						num = 2;
						continue;
					}
					break;
				case 4:
					goto IL_CB;
				case 5:
					goto IL_4D;
				}
				if (FormulaUtil.ᜀ(FormulaUtil.OpenBrackets, strFormula[BracketPos]) != -1)
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
				IL_4D:
				if (true)
				{
				}
				a_2 = 1;
				a_3 = FormulaUtil.OpenBrackets;
				num = 4;
			}
			IL_85:
			IL_CB:
			goto IL_E1;
			IL_CD:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᝃ㙅ⵇ⥉╋⡍㥏㝑こ癕⡗㕙⽛㝝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵᥷婹౻ᅽ겋늑聯蓮ﮝ풟", a_));
			IL_E1:
			return FormulaUtil.ᜀ(strFormula, BracketPos, a_3, a_2);
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x003C181C File Offset: 0x003C081C
		public static string GetOperand(string strFormula, int OpIndex, SortedList arrBreakStrings, bool IsLeft)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 28;
				int num2;
				string text;
				for (;;)
				{
					char[] a_2;
					int length;
					char[] array;
					char[] array2;
					int num3;
					int num4;
					char[] a_3;
					switch (num)
					{
					case 0:
						if (FormulaUtil.ᜀ(strFormula, num2, arrBreakStrings) != -1)
						{
							num = 54;
							continue;
						}
						goto IL_24B;
					case 1:
						if (FormulaUtil.ᜀ(a_2, strFormula[num2]) != -1)
						{
							num = 7;
							continue;
						}
						goto IL_4FC;
					case 2:
						if (FormulaUtil.ᜀ(a_2, strFormula[num2]) != -1)
						{
							num = 18;
							continue;
						}
						goto IL_2F4;
					case 3:
						if (num2 < strFormula.Length)
						{
							num = 11;
							continue;
						}
						return text;
					case 4:
						num = 19;
						continue;
					case 5:
						goto IL_203;
					case 6:
						num = 12;
						continue;
					case 7:
						goto IL_3A2;
					case 8:
						goto IL_551;
					case 9:
						goto IL_2C5;
					case 10:
						if (!IsLeft)
						{
							num = 4;
							continue;
						}
						num = 44;
						continue;
					case 11:
						num = 39;
						continue;
					case 12:
						if (num2 >= length)
						{
							num = 8;
							continue;
						}
						num = 53;
						continue;
					case 13:
						if (!IsLeft)
						{
							num = 47;
							continue;
						}
						num = 43;
						continue;
					case 14:
						if (FormulaUtil.FindCorrespondingBracket(strFormula, num2) == -1)
						{
							num = 42;
							continue;
						}
						goto IL_588;
					case 15:
						if (num2 >= 0)
						{
							num = 20;
							continue;
						}
						goto IL_1DF;
					case 16:
						num = 36;
						continue;
					case 17:
						goto IL_4C7;
					case 18:
						num = 14;
						continue;
					case 19:
						array = FormulaUtil.OpenBrackets;
						goto IL_2A6;
					case 20:
						num = 24;
						continue;
					case 21:
						if (!IsLeft)
						{
							num = 35;
							continue;
						}
						num = 48;
						continue;
					case 22:
						if (num2 >= 0)
						{
							num = 6;
							continue;
						}
						goto IL_551;
					case 23:
						goto IL_1DF;
					case 24:
						if (num2 < length)
						{
							num = 46;
							continue;
						}
						goto IL_1DF;
					case 25:
						goto IL_24B;
					case 26:
						goto IL_124;
					case 27:
						goto IL_56C;
					case 29:
						if (!IsLeft)
						{
							num = 9;
							continue;
						}
						num = 50;
						continue;
					case 30:
						array2 = FormulaUtil.CloseBrackets;
						goto IL_227;
					case 31:
						if (arrBreakStrings == null)
						{
							num = 17;
							continue;
						}
						num = 10;
						continue;
					case 32:
						num2 = FormulaUtil.FindCorrespondingBracket(strFormula, num2);
						num = 25;
						continue;
					case 33:
						num = 40;
						continue;
					case 34:
						if (!IsLeft)
						{
							num = 27;
							continue;
						}
						goto IL_5D2;
					case 35:
						num = 49;
						continue;
					case 36:
						goto IL_1DF;
					case 37:
						text = FormulaUtil.ᜀ(strFormula, num2);
						num2 += text.Length;
						num = 3;
						continue;
					case 38:
						if (FormulaUtil.IndexOf(FormulaUtil.PlusMinusArray, strFormula[num2].ToString()) == -1)
						{
							num = 16;
							continue;
						}
						num2 += num3;
						if (true)
						{
						}
						num = 45;
						continue;
					case 39:
						if (FormulaUtil.ᜀ(strFormula, num2, arrBreakStrings) == -1)
						{
							num = 52;
							continue;
						}
						return text;
					case 40:
						if (strFormula[num2] == '#')
						{
							num = 37;
							continue;
						}
						goto IL_4FC;
					case 41:
						num4 = 1;
						goto IL_3B8;
					case 42:
						goto IL_2F4;
					case 43:
						num4 = -1;
						goto IL_3B8;
					case 44:
						array = FormulaUtil.CloseBrackets;
						goto IL_2A6;
					case 45:
						goto IL_203;
					case 46:
						num = 38;
						continue;
					case 47:
						num = 41;
						continue;
					case 48:
						goto IL_1A0;
					case 49:
						goto IL_3FF;
					case 50:
						array2 = FormulaUtil.OpenBrackets;
						goto IL_227;
					case 51:
						if (!IsLeft)
						{
							num = 33;
							continue;
						}
						goto IL_4FC;
					case 52:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C5;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 53:
						if (FormulaUtil.ᜀ(a_3, strFormula[num2]) != -1)
						{
							num = 32;
							continue;
						}
						num = 2;
						continue;
					case 54:
						goto IL_588;
					}
					if (strFormula == null)
					{
						num = 26;
						continue;
					}
					num = 31;
					continue;
					IL_1DF:
					num = 22;
					continue;
					IL_203:
					num = 15;
					continue;
					IL_227:
					a_2 = array2;
					num = 13;
					continue;
					IL_24B:
					num2 += num3;
					num = 23;
					continue;
					IL_2A6:
					a_3 = array;
					num = 29;
					continue;
					IL_2C5:
					num = 30;
					continue;
					IL_2F4:
					num = 0;
					continue;
					IL_3B8:
					num3 = num4;
					num2 = OpIndex + num3;
					num = 51;
					continue;
					IL_4FC:
					length = strFormula.Length;
					num = 5;
					continue;
					IL_551:
					num = 34;
					continue;
					IL_588:
					num = 21;
				}
				IL_124:
				throw new ArgumentNullException(RecordTableEnumerator.b("㌿㙁㙃E❇㡉⅋㭍㱏㍑", a_));
				IL_1A0:
				string text2 = strFormula.Substring(num2 + 1, OpIndex - num2 - 1);
				IL_2EF:
				text = text2;
				return text;
				IL_3A2:
				return text;
				IL_3FF:
				text2 = strFormula.Substring(OpIndex + 1, num2 - OpIndex - 1);
				goto IL_2EF;
				IL_4C7:
				throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃х㩇⽉ⵋ╍͏♑♓㽕㙗㵙⽛", a_));
				IL_56C:
				return strFormula.Substring(OpIndex + 1);
				IL_5D2:
				return strFormula.Substring(0, OpIndex);
			}
			}
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x003C1E04 File Offset: 0x003C0E04
		[CLSCompliant(false)]
		internal static void ᜀ(string A_0, ExcelFunction A_1, sprᨳ[] A_2)
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
			FormulaUtil.ᜁ(A_0, A_1, A_2, -1);
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x003C1E48 File Offset: 0x003C0E48
		[CLSCompliant(false)]
		internal static void ᜁ(string A_0, ExcelFunction A_1, sprᨳ[] A_2, int A_3)
		{
			for (;;)
			{
				IL_44:
				Dictionary<Type, sprᨳ> dictionary = null;
				for (;;)
				{
					IL_46:
					int num = 10;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_93;
						case 1:
							goto IL_D7;
						case 2:
							if (dictionary == null)
							{
								num = 11;
								continue;
							}
							goto IL_D7;
						case 3:
							goto IL_1B5;
						case 4:
							if (A_2.Length != 0)
							{
								num = 8;
								continue;
							}
							goto IL_1B5;
						case 5:
							if (num2 >= A_2.Length)
							{
								num = 9;
								continue;
							}
							dictionary.Add(A_2[num2].ᜁ(), A_2[num2]);
							num2++;
							num = 12;
							continue;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_46;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 7:
							FormulaUtil.FunctionIdToParamCount.Add(A_1, A_3);
							num = 14;
							continue;
						case 8:
							dictionary = new Dictionary<Type, sprᨳ>();
							num = 3;
							continue;
						case 9:
							num = 2;
							continue;
						case 10:
							if (A_2 != null)
							{
								num = 6;
								continue;
							}
							goto IL_1B5;
						case 11:
							if (true)
							{
							}
							dictionary = new Dictionary<Type, sprᨳ>(1);
							dictionary.Add(typeof(sprᦊ), new sprᨳ(2));
							num = 1;
							continue;
						case 12:
							goto IL_93;
						case 13:
							if (A_3 != -1)
							{
								num = 7;
								continue;
							}
							return;
						case 14:
							return;
						}
						goto IL_44;
						IL_93:
						num = 5;
						continue;
						IL_D7:
						FormulaUtil.\u1715.Add(A_1, dictionary);
						FormulaUtil.FunctionIdToAlias.Add(A_1, A_0);
						FormulaUtil.FunctionAliasToId.Add(A_0, A_1);
						num = 13;
						continue;
						IL_1B5:
						num2 = 0;
						num = 0;
					}
				}
			}
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x003C201C File Offset: 0x003C101C
		[CLSCompliant(false)]
		public static void RegisterFunction(string functionName, ExcelFunction index, int paramCount)
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
			FormulaUtil.ᜁ(functionName, index, null, paramCount);
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x003C2060 File Offset: 0x003C1060
		[CLSCompliant(false)]
		public static void RegisterFunction(string functionName, ExcelFunction index)
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
			FormulaUtil.RegisterFunction(functionName, index, -1);
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x003C20A4 File Offset: 0x003C10A4
		public static void RaiseFormulaEvaluation(object sender, EvaluateEventArgs e)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					FormulaUtil.ᜩ(sender, e);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_23;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				goto IL_1C;
				IL_23:
				num = 2;
				continue;
				IL_1C:
				if (FormulaUtil.ᜩ != null)
				{
					goto IL_23;
				}
				break;
			}
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x003C2124 File Offset: 0x003C1124
		public static void RegisterTokenClass(Type type)
		{
			int a_ = 10;
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
						return;
					case 1:
						goto IL_DB;
					case 2:
						if (type.IsSubclassOf(typeof(Ptg)))
						{
							spr\u2400[] array = (spr\u2400[])type.GetCustomAttributes(typeof(spr\u2400), false);
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 3:
					{
						spr\u2400[] array;
						if (array.Length == 0)
						{
							num = 8;
							continue;
						}
						if (true)
						{
						}
						FormulaUtil.ᜀ ᜀ = new FormulaUtil.ᜀ(type);
						Ptg value = ᜀ.ᜊ();
						int num2 = 0;
						num = 9;
						continue;
					}
					case 5:
						goto IL_5D;
					case 6:
						goto IL_D6;
					case 7:
					{
						spr\u2400[] array;
						int num2;
						if (num2 >= array.Length)
						{
							num = 0;
							continue;
						}
						FormulaToken key = array[num2].ᜀ();
						FormulaUtil.ᜀ ᜀ;
						FormulaUtil.\u1718.Add(array[num2].ᜀ(), ᜀ);
						Ptg value;
						FormulaUtil.\u1719.Add(key, value);
						num2++;
						num = 1;
						continue;
					}
					case 8:
						return;
					case 9:
						goto IL_DB;
					}
					goto IL_51;
					IL_54:
					num = 5;
					continue;
					IL_51:
					if (type == null)
					{
						goto IL_54;
					}
					num = 2;
					continue;
					IL_DB:
					num = 7;
				}
				IL_5D:
				throw new ArgumentNullException(RecordTableEnumerator.b("㐿㭁㑃⍅", a_));
				IL_D6:
				throw new ArgumentException(RecordTableEnumerator.b("⌿⹁╃㕅㭇橉⅋㭍⍏♑瑓㑕㵗穙㡛㭝቟ୡባͥ౧䩩੫ᱭὯά味♵౷ᵹ屻ᵽ", a_), RecordTableEnumerator.b("㐿㭁㑃⍅", a_));
			}
			}
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x003C22E0 File Offset: 0x003C12E0
		[CLSCompliant(false)]
		public static void RegisterAdditionalAlias(string aliasName, ExcelFunction functionIndex)
		{
			int a_ = 7;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (FormulaUtil.FunctionAliasToId.ContainsKey(aliasName))
					{
						num = 2;
						continue;
					}
					goto IL_FB;
				case 1:
					goto IL_47;
				case 2:
					goto IL_8C;
				case 4:
					if (aliasName.Length == 0)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 5:
					goto IL_F9;
				}
				if (aliasName == null)
				{
					num = 1;
				}
				else
				{
					num = 4;
				}
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("尼匾⡀≂㙄ॆ⡈♊⡌", a_));
			IL_8C:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("尼匾⡀≂㙄ॆ⡈♊⡌", a_), RecordTableEnumerator.b("簼匾⡀≂㙄杆❈⩊⁌⩎煐㉒㥔╖㱘㩚㥜♞䅠٢ᵤ๦ᩨὪṬ䅮", a_));
			IL_F9:
			throw new ArgumentException(RecordTableEnumerator.b("尼匾⡀≂㙄ॆ⡈♊⡌潎籐獒♔⍖⭘㉚㍜㡞䅠bѤ०ݨѪᥬ佮፰ᙲ啴ቶᑸ୺ॼپ", a_));
			IL_FB:
			FormulaUtil.FunctionAliasToId.Add(aliasName, functionIndex);
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x003C23F4 File Offset: 0x003C13F4
		internal static void ᜀ(Ptg A_0, int A_1, int A_2)
		{
			int a_ = 10;
			int num = 6;
			spr\u25A0 spr_u25A;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u1B76 spr_u1B = (spr\u1B76)A_0;
					num = 8;
					continue;
				}
				case 1:
					spr_u25A = (spr\u25A0)A_0;
					if (true)
					{
					}
					num = 9;
					continue;
				case 2:
					goto IL_15B;
				case 3:
					if (FormulaUtil.ᜀ(FormulaUtil.\u171D, A_0.TokenCode) != -1)
					{
						num = 1;
						continue;
					}
					num = 7;
					continue;
				case 4:
				{
					spr\u1B76 spr_u1B;
					spr_u1B.ᜀ((ushort)(A_2 + 1));
					num = 5;
					continue;
				}
				case 5:
					goto IL_C2;
				case 7:
					if (FormulaUtil.ᜀ(FormulaUtil.\u171C, A_0.TokenCode) != -1)
					{
						num = 0;
						continue;
					}
					return;
				case 8:
				{
					spr\u1B76 spr_u1B;
					if ((int)(spr_u1B.ᜂ() - 1) != A_1)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 9:
					if ((int)(spr_u25A.ᜀ() - 1) == A_1)
					{
						num = 2;
						continue;
					}
					return;
				case 10:
					goto IL_50;
				}
				if (A_0 == null)
				{
					num = 10;
				}
				else
				{
					num = 3;
				}
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("〿㙁⍃", a_));
			IL_C2:
			return;
			IL_15B:
			spr_u25A.ᜀ((ushort)(A_2 + 1));
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x003C2564 File Offset: 0x003C1564
		internal static int ᜀ(FormulaToken[] A_0, FormulaToken A_1)
		{
			int a_ = 17;
			int num = 3;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_47;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						if (num2 >= num3)
						{
							num = 5;
							continue;
						}
						num = 6;
						continue;
					}
					break;
				case 2:
					goto IL_83;
				case 4:
					return num2;
				case 5:
					return -1;
				case 6:
					if (A_0[num2] == A_1)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 2;
					continue;
				case 7:
					goto IL_DB;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				num2 = 0;
				num3 = A_0.Length;
				num = 7;
				continue;
				IL_83:
				num = 1;
				continue;
				IL_DB:
				goto IL_83;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("♆㭈㥊ⱌ㙎", a_));
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x003C2654 File Offset: 0x003C1654
		public static int IndexOf(string[] array, string value)
		{
			int a_ = 3;
			int num = 0;
			for (;;)
			{
				int num2;
				int num3;
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
						goto IL_E0;
					default:
						if (false)
						{
						}
						if (num2 >= num3)
						{
							num = 3;
							continue;
						}
						num = 6;
						continue;
					}
					break;
				case 2:
					goto IL_90;
				case 3:
					return -1;
				case 4:
					goto IL_4F;
				case 5:
					return num2;
				case 6:
					if (array[num2] == value)
					{
						num = 5;
						continue;
					}
					num2++;
					num = 2;
					continue;
				case 7:
					goto IL_E0;
				}
				if (array == null)
				{
					num = 4;
					continue;
				}
				num2 = 0;
				num3 = array.Length;
				num = 7;
				continue;
				IL_90:
				num = 1;
				continue;
				IL_E0:
				goto IL_90;
			}
			IL_4F:
			throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似帾㡀", a_));
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x003C2748 File Offset: 0x003C1748
		[CLSCompliant(false)]
		public static int IndexOf(ExcelFunction[] array, ExcelFunction value)
		{
			int a_ = 13;
			int num = 0;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 1:
					if (array[num2] == value)
					{
						num = 5;
						continue;
					}
					num2++;
					num = 6;
					continue;
				case 2:
					goto IL_DB;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					goto IL_47;
				case 5:
					return num2;
				case 6:
					goto IL_83;
				case 7:
					return -1;
				}
				if (array == null)
				{
					num = 4;
					continue;
				}
				num2 = 0;
				num3 = array.Length;
				num = 2;
				continue;
				IL_83:
				if (true)
				{
				}
				num = 3;
				continue;
				IL_DB:
				goto IL_83;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄㕆⡈㉊", a_));
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x003C2838 File Offset: 0x003C1838
		private static int ᜀ(char[] A_0, char A_1)
		{
			int a_ = 4;
			int num = 5;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
				{
					char c;
					if (A_1 == c)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 3;
					continue;
				}
				case 1:
					goto IL_C2;
				case 2:
					goto IL_51;
				case 3:
					goto IL_8F;
				case 4:
					return num2;
				case 6:
					goto IL_8F;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num2;
					default:
					{
						if (false)
						{
						}
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						char c = A_0[num2];
						num = 0;
						continue;
					}
					}
					break;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num2 = 0;
				num3 = A_0.Length;
				num = 6;
				continue;
				IL_8F:
				num = 7;
			}
			IL_51:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ℿ㭁", a_));
			IL_C2:
			if (true)
			{
			}
			return -1;
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x003C2928 File Offset: 0x003C1928
		private static int ᜀ(string A_0, int A_1, string[] A_2)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 17;
				int num2;
				for (;;)
				{
					int length;
					string text;
					switch (num)
					{
					case 0:
						if (length != 1)
						{
							num = 21;
							continue;
						}
						return num2;
					case 1:
						if (text == null)
						{
							num = 8;
							continue;
						}
						num = 20;
						continue;
					case 2:
						return -1;
					case 3:
						return -1;
					case 4:
						goto IL_119;
					case 5:
					{
						int num3;
						if (num3 == 0)
						{
							num = 18;
							continue;
						}
						char c = A_0[A_1];
						int num4 = FormulaUtil.ᜀ(A_2, c);
						num = 16;
						continue;
					}
					case 6:
						goto IL_21A;
					case 7:
						return -1;
					case 8:
						goto IL_298;
					case 9:
						goto IL_A2;
					case 10:
						goto IL_137;
					case 11:
					{
						char c;
						if (text[0] != c)
						{
							num = 7;
							continue;
						}
						num = 0;
						continue;
					}
					case 12:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 3;
							continue;
						}
						text = A_2[num2];
						length = text.Length;
						num = 1;
						continue;
					}
					case 13:
						goto IL_194;
					case 14:
						goto IL_E1;
					case 15:
						goto IL_194;
					case 16:
					{
						int num4;
						if (num4 < 0)
						{
							num = 2;
							continue;
						}
						num2 = num4;
						num = 13;
						continue;
					}
					case 18:
						return -1;
					case 19:
					{
						if (A_2 == null)
						{
							num = 6;
							continue;
						}
						int num3 = A_2.Length;
						num = 5;
						continue;
					}
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_119;
						default:
							if (false)
							{
							}
							if (length == 0)
							{
								num = 14;
								continue;
							}
							num = 11;
							continue;
						}
						break;
					case 21:
						num = 4;
						continue;
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					num = 19;
					continue;
					IL_119:
					if (string.Compare(A_0, A_1 + 1, text, 1, length - 1) == 0)
					{
						num = 10;
						continue;
					}
					num2++;
					num = 15;
					continue;
					IL_194:
					num = 12;
				}
				IL_A2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㉀㝂㝄ņ♈㥊⁌㩎㵐㉒", a_));
				IL_E1:
				throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㝄⹆❈ⱊ浌ⱎぐ㵒牔⍖祘㥚㡜罞Ѡ๢ᕤ፦ၨ", a_));
				IL_137:
				return num2;
				IL_21A:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁀ㅂ㝄Ն㭈⹊ⱌ⑎ɐ❒❔㹖㝘㱚⹜", a_));
				IL_298:
				throw new ArgumentNullException();
			}
			}
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x003C2BD4 File Offset: 0x003C1BD4
		private static int ᜀ(string A_0, int A_1, SortedList A_2)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 2;
				int num3;
				for (;;)
				{
					string text;
					int length;
					switch (num)
					{
					case 0:
						return -1;
					case 1:
						goto IL_D6;
					case 3:
						num = 8;
						continue;
					case 4:
						return -1;
					case 5:
					{
						int num2;
						if (num2 < 0)
						{
							num = 18;
							continue;
						}
						num3 = num2;
						num = 12;
						continue;
					}
					case 6:
					{
						char c;
						if (text[0] != c)
						{
							num = 20;
							continue;
						}
						num = 17;
						continue;
					}
					case 7:
						goto IL_18C;
					case 8:
						goto IL_111;
					case 9:
						goto IL_9A;
					case 10:
						if (text == null)
						{
							num = 21;
							continue;
						}
						num = 11;
						continue;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_111;
						default:
							if (false)
							{
							}
							if (length == 0)
							{
								num = 1;
								continue;
							}
							num = 6;
							continue;
						}
						break;
					case 12:
						goto IL_18C;
					case 13:
					{
						if (A_2 == null)
						{
							if (true)
							{
							}
							num = 14;
							continue;
						}
						int count = A_2.Count;
						num = 15;
						continue;
					}
					case 14:
						goto IL_21A;
					case 15:
					{
						int count;
						if (count == 0)
						{
							num = 4;
							continue;
						}
						char c = A_0[A_1];
						int num2 = FormulaUtil.ᜀ(A_2, c);
						num = 5;
						continue;
					}
					case 16:
						if (num3 < 0)
						{
							num = 0;
							continue;
						}
						text = (string)A_2.GetKey(num3);
						length = text.Length;
						num = 10;
						continue;
					case 17:
						if (length != 1)
						{
							num = 3;
							continue;
						}
						return num3;
					case 18:
						return -1;
					case 19:
						goto IL_12F;
					case 20:
						return -1;
					case 21:
						goto IL_2A1;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num = 13;
					continue;
					IL_111:
					if (string.Compare(A_0, A_1 + 1, text, 1, length - 1) == 0)
					{
						num = 19;
						continue;
					}
					num3--;
					num = 7;
					continue;
					IL_18C:
					num = 16;
				}
				IL_9A:
				throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆཈⑊㽌≎⑐㽒㑔", a_));
				IL_D6:
				throw new ArgumentException(RecordTableEnumerator.b("၂ㅄ㕆⁈╊⩌潎㉐㉒㭔灖ⵘ筚㽜㩞䅠٢ࡤᝦᵨቪ", a_));
				IL_12F:
				return num3;
				IL_21A:
				throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄㕆ୈ㥊⡌⹎㩐R⅔╖じ㕚㩜ⱞ", a_));
				IL_2A1:
				throw new ArgumentNullException();
			}
			}
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x003C2E88 File Offset: 0x003C1E88
		private static int ᜀ(string[] A_0, char A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					int num3;
					string text;
					switch (num)
					{
					case 0:
						return num2;
					case 1:
						num = 6;
						continue;
					case 2:
					{
						char c;
						if (c < A_1)
						{
							num = 1;
							continue;
						}
						goto IL_C9;
					}
					case 3:
						goto IL_C9;
					case 4:
					{
						int num4;
						num3 = num4;
						num = 19;
						continue;
					}
					case 5:
						goto IL_149;
					case 6:
					{
						int num4;
						if (num3 != num4)
						{
							num = 4;
							continue;
						}
						goto IL_26F;
					}
					case 8:
						if (text == null)
						{
							num = 23;
							continue;
						}
						num = 21;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15D;
						default:
						{
							if (false)
							{
							}
							int num4;
							if (num2 != num4)
							{
								num = 15;
								continue;
							}
							goto IL_26F;
						}
						}
						break;
					case 10:
						num = 9;
						continue;
					case 11:
						goto IL_9C;
					case 12:
					{
						if (num2 == num3)
						{
							num = 20;
							continue;
						}
						int num4 = (num2 + num3) / 2;
						string text2 = A_0[num4];
						num = 22;
						continue;
					}
					case 13:
						goto IL_293;
					case 14:
						return num3;
					case 15:
					{
						int num4;
						num2 = num4;
						num = 24;
						continue;
					}
					case 16:
					{
						char c;
						if (c >= A_1)
						{
							num = 10;
							continue;
						}
						num = 2;
						continue;
					}
					case 17:
						if (text[0] == A_1)
						{
							num = 14;
							continue;
						}
						text = A_0[num2];
						num = 8;
						continue;
					case 18:
						if (text == null)
						{
							num = 13;
							continue;
						}
						num = 17;
						continue;
					case 19:
						goto IL_C9;
					case 20:
						goto IL_26F;
					case 21:
						goto IL_15D;
					case 22:
					{
						string text2;
						if (text2 == null)
						{
							num = 5;
							continue;
						}
						char c = text2[0];
						num = 16;
						continue;
					}
					case 23:
						goto IL_11F;
					case 24:
						goto IL_C9;
					}
					if (A_0 == null)
					{
						num = 11;
						continue;
					}
					num3 = 0;
					num2 = A_0.Length - 1;
					num = 3;
					continue;
					IL_C9:
					num = 12;
					continue;
					IL_15D:
					if (true)
					{
					}
					if (text[0] == A_1)
					{
						num = 0;
						continue;
					}
					return -1;
					IL_26F:
					text = A_0[num3];
					num = 18;
				}
				IL_9C:
				throw new ArgumentNullException(RecordTableEnumerator.b("圵䨷䠹漻䨽㈿⭁⩃ⅅṇ⭉⁋㭍㕏⅑", a_));
				IL_11F:
				throw new ArgumentNullException();
				IL_149:
				throw new ArgumentNullException(RecordTableEnumerator.b("攵䰷䠹唻倽✿扁ⵃ⡅桇㹉⑋⭍灏㍑♓⑕㥗⍙籛㵝şౡ䍣ብ䡧ࡩ५乭ṯݱᡳ᩵噷", a_));
				IL_293:
				throw new ArgumentNullException();
			}
			}
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x003C3150 File Offset: 0x003C2150
		private static int ᜁ(SortedList A_0, char A_1)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 17;
				for (;;)
				{
					string text;
					int num2;
					int num3;
					switch (num)
					{
					case 0:
					{
						char c;
						if (c < A_1)
						{
							num = 8;
							continue;
						}
						goto IL_D4;
					}
					case 1:
						goto IL_17A;
					case 2:
						if (text == null)
						{
							num = 15;
							continue;
						}
						num = 1;
						continue;
					case 3:
						goto IL_2B4;
					case 4:
						return num2;
					case 5:
						goto IL_D4;
					case 6:
					{
						string text2;
						if (text2 == null)
						{
							num = 13;
							continue;
						}
						char c = text2[0];
						num = 19;
						continue;
					}
					case 7:
						goto IL_A7;
					case 8:
						num = 9;
						continue;
					case 9:
					{
						int num4;
						if (num3 != num4)
						{
							num = 12;
							continue;
						}
						goto IL_287;
					}
					case 10:
						goto IL_D4;
					case 11:
						if (text[0] == A_1)
						{
							num = 18;
							continue;
						}
						text = (string)A_0.GetKey(num2);
						num = 2;
						continue;
					case 12:
					{
						int num4;
						num3 = num4;
						num = 14;
						continue;
					}
					case 13:
						goto IL_166;
					case 14:
						goto IL_D4;
					case 15:
						goto IL_133;
					case 16:
					{
						int num4;
						num2 = num4;
						num = 10;
						continue;
					}
					case 18:
						return num3;
					case 19:
					{
						char c;
						if (c >= A_1)
						{
							num = 21;
							continue;
						}
						num = 0;
						continue;
					}
					case 20:
					{
						if (num2 == num3)
						{
							num = 24;
							continue;
						}
						int num4 = (num2 + num3) / 2;
						string text2 = (string)A_0.GetKey(num4);
						num = 6;
						continue;
					}
					case 21:
						num = 23;
						continue;
					case 22:
						if (text == null)
						{
							num = 3;
							continue;
						}
						num = 11;
						continue;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17A;
						default:
						{
							if (false)
							{
							}
							int num4;
							if (num2 != num4)
							{
								num = 16;
								continue;
							}
							goto IL_287;
						}
						}
						break;
					case 24:
						goto IL_287;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					num3 = 0;
					num2 = A_0.Count - 1;
					num = 5;
					continue;
					IL_D4:
					num = 20;
					continue;
					IL_17A:
					if (text[0] == A_1)
					{
						num = 4;
						continue;
					}
					return -1;
					IL_287:
					text = (string)A_0.GetKey(num3);
					num = 22;
				}
				IL_A7:
				throw new ArgumentNullException(RecordTableEnumerator.b("夷䠹主洽㐿ぁⵃ⡅⽇᱉ⵋ≍╏㝑❓", a_));
				IL_133:
				throw new ArgumentNullException();
				IL_166:
				throw new ArgumentNullException(RecordTableEnumerator.b("欷丹主圽⸿╁摃⽅♇橉㡋♍㕏牑㕓⑕⩗㭙╛繝͟͡੣䅥ᱧ䩩๫୭偯ᱱų᩵ᑷ呹", a_));
				IL_2B4:
				throw new ArgumentNullException();
			}
			}
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x003C3438 File Offset: 0x003C2438
		private static int ᜀ(SortedList A_0, char A_1)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 22;
				for (;;)
				{
					string text;
					int num2;
					int num4;
					switch (num)
					{
					case 0:
						if (text == null)
						{
							num = 24;
							continue;
						}
						num = 14;
						continue;
					case 1:
					{
						int num3;
						if (num2 != num3)
						{
							num = 7;
							continue;
						}
						goto IL_287;
					}
					case 2:
					{
						int num3;
						if (num4 != num3)
						{
							num = 11;
							continue;
						}
						goto IL_287;
					}
					case 3:
						return num4;
					case 4:
						goto IL_132;
					case 5:
					{
						char c;
						if (c > A_1)
						{
							num = 13;
							continue;
						}
						num = 15;
						continue;
					}
					case 6:
						if (true)
						{
						}
						goto IL_C9;
					case 7:
					{
						int num3;
						num2 = num3;
						num = 21;
						continue;
					}
					case 8:
					{
						if (num4 == num2)
						{
							num = 16;
							continue;
						}
						int num3 = (num4 + num2) / 2;
						string text2 = (string)A_0.GetKey(num3);
						num = 20;
						continue;
					}
					case 9:
						return num2;
					case 10:
						num = 1;
						continue;
					case 11:
					{
						int num3;
						num4 = num3;
						num = 23;
						continue;
					}
					case 12:
						goto IL_9C;
					case 13:
						num = 2;
						continue;
					case 14:
						if (text[0] == A_1)
						{
							num = 9;
							continue;
						}
						text = (string)A_0.GetKey(num4);
						num = 19;
						continue;
					case 15:
					{
						char c;
						if (c <= A_1)
						{
							num = 10;
							continue;
						}
						goto IL_C9;
					}
					case 16:
						goto IL_287;
					case 17:
						goto IL_181;
					case 18:
						if (text[0] == A_1)
						{
							num = 3;
							continue;
						}
						return -1;
					case 19:
						if (text == null)
						{
							num = 4;
							continue;
						}
						num = 18;
						continue;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_295;
						default:
						{
							if (false)
							{
							}
							string text2;
							if (text2 == null)
							{
								num = 17;
								continue;
							}
							char c = text2[0];
							num = 5;
							continue;
						}
						}
						break;
					case 21:
						goto IL_C9;
					case 23:
						goto IL_C9;
					case 24:
						goto IL_2B4;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					num2 = 0;
					num4 = A_0.Count - 1;
					num = 6;
					continue;
					IL_C9:
					num = 8;
					continue;
					IL_295:
					num = 0;
					continue;
					IL_287:
					text = (string)A_0.GetKey(num2);
					goto IL_295;
				}
				IL_9C:
				throw new ArgumentNullException(RecordTableEnumerator.b("弽㈿ぁᝃ㉅㩇⍉≋⥍ُ㍑㡓⍕㵗⥙", a_));
				IL_132:
				throw new ArgumentNullException();
				IL_181:
				throw new ArgumentNullException(RecordTableEnumerator.b("洽㐿ぁⵃ⡅⽇橉╋⁍灏♑㱓㍕硗㭙⹛ⱝş᭡䑣ե१ѩ䭫ᩭ偯ၱᅳ噵ᙷཹၻች깿", a_));
				IL_2B4:
				throw new ArgumentNullException();
			}
			}
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x003C3720 File Offset: 0x003C2720
		[CLSCompliant(false)]
		internal static Ptg[] ᜀ(spr\u1DE2 A_0, IWorkbook A_1, int A_2, int A_3)
		{
			int a_ = 9;
			Ptg[] array;
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_75:
				Ptg[] array2;
				array[num] = array2[num].ConvertSharedToken(A_1, A_2, A_3);
				num++;
				num2 = 1;
				break;
			}
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num2 = 2;
					break;
				}
				break;
			}
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_73;
				case 1:
					goto IL_CD;
				case 3:
					goto IL_CD;
				case 4:
					if (num >= num3)
					{
						num2 = 5;
						continue;
					}
					goto IL_75;
				case 5:
					return array;
				}
				if (A_0 == null)
				{
					num2 = 0;
					continue;
				}
				Ptg[] array2 = A_0.ᜁ();
				num3 = array2.Length;
				array = new Ptg[num3];
				num = 0;
				num2 = 3;
				continue;
				IL_CD:
				num2 = 4;
			}
			IL_73:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀≂㝄≆ⵈ", a_));
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x003C381C File Offset: 0x003C281C
		internal Ptg[] ᜀ(Ptg[] A_0, int A_1, int A_2, Rectangle A_3, int A_4, Rectangle A_5, int A_6, int A_7)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int i = 0;
					int num = A_0.Length;
					if (true)
					{
					}
					int num2 = 3;
					for (;;)
					{
						IL_10:
						switch (num2)
						{
						case 0:
							return A_0;
						case 1:
							while (i < num)
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
									Ptg ptg = A_0[i];
									bool flag;
									A_0[i] = ptg.Offset(A_1, A_6 - 1, A_7 - 1, A_2, A_3, A_4, A_5, out flag, this.ᜡ);
									i++;
									num2 = 2;
									goto IL_10;
								}
								}
							}
							num2 = 0;
							continue;
						case 2:
							goto IL_4A;
						case 3:
							goto IL_4A;
						}
						break;
						IL_4A:
						num2 = 1;
					}
				}
				return A_0;
			}
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x003C38E0 File Offset: 0x003C28E0
		internal Ptg[] ᜀ(Ptg[] A_0, int A_1, int A_2)
		{
			for (;;)
			{
				int num = 0;
				int num2 = A_0.Length;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return A_0;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							if (num >= num2)
							{
								num3 = 1;
								continue;
							}
							Ptg ptg = A_0[num];
							A_0[num] = ptg.Offset(A_1, A_2, this.ᜡ);
							num++;
							num3 = 2;
							continue;
						}
						}
						break;
					case 1:
						return A_0;
					case 2:
						goto IL_28;
					case 3:
						goto IL_28;
					}
					break;
					IL_28:
					num3 = 0;
				}
			}
			return A_0;
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x003C3980 File Offset: 0x003C2980
		public static void PushOperandToStack(Stack<object> operands, string operand)
		{
			int a_ = 16;
			int num = 2;
			string item;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					sprᯡ sprᯡ;
					if (sprᯡ != null)
					{
						num = 6;
						continue;
					}
					goto IL_12B;
				}
				case 1:
					goto IL_E0;
				case 3:
					goto IL_FB;
				case 4:
					if (operand == null)
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					item = operand;
					num = 7;
					continue;
				case 5:
				{
					object obj = operands.Peek();
					sprᯡ sprᯡ = obj as sprᯡ;
					goto IL_5B;
				}
				case 6:
				{
					operands.Pop();
					sprᯡ sprᯡ;
					item = sprᯡ.ToString() + operand;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				case 7:
					if (operands.Count > 0)
					{
						num = 5;
						continue;
					}
					goto IL_12B;
				case 8:
					goto IL_4B;
				}
				if (operands == null)
				{
					num = 8;
					continue;
				}
				num = 4;
				continue;
				IL_5B:
				num = 0;
			}
			IL_4B:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥅㡇⽉㹋⽍㹏㙑❓", a_));
			IL_E0:
			goto IL_12B;
			IL_FB:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥅㡇⽉㹋⽍㹏㙑", a_));
			IL_12B:
			operands.Push(item);
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x003C3AC0 File Offset: 0x003C2AC0
		private Ptg[] ᜀ(string A_0, int A_1, IWorkbook A_2, IWorksheet A_3, Dictionary<Type, sprᨳ> A_4, int A_5, Dictionary<string, string> A_6, ParseFormulaOptions A_7, int A_8, int A_9)
		{
			int a_ = 16;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				List<Ptg> list;
				string text2;
				int a_2;
				sprឯ sprឯ;
				int num2;
				int a_8;
				for (;;)
				{
					list = new List<Ptg>();
					string text = A_0.Substring(0, A_1);
					text2 = text.ToUpper();
					a_2 = FormulaUtil.ᜀ(typeof(spr\u231A), 1, A_4, A_5, A_7);
					int num = 25;
					for (;;)
					{
						ExcelFunction excelFunction;
						Dictionary<Type, sprᨳ> dictionary;
						int num3;
						ParseFormulaOptions parseFormulaOptions;
						int num4;
						int num5;
						string[] array;
						switch (num)
						{
						case 0:
							excelFunction = FormulaUtil.FunctionAliasToId[text2];
							num = 5;
							continue;
						case 1:
							goto IL_F2;
						case 2:
							if ((A_7 & ParseFormulaOptions.RootLevel) != ParseFormulaOptions.None)
							{
								num = 15;
								continue;
							}
							goto IL_1AB;
						case 3:
							goto IL_1AB;
						case 4:
							goto IL_15D;
						case 5:
							excelFunction = FormulaUtil.FunctionAliasToId[text2];
							sprឯ = null;
							num = 9;
							continue;
						case 6:
						{
							int a_3 = FormulaUtil.ᜀ(typeof(spr\u1B43), 1, A_4, A_5, A_7);
							FormulaToken a_4 = spr\u1B43.ᜀ(a_3);
							sprឯ = (sprឯ)FormulaUtil.ᜀ(a_4, excelFunction);
							num = 20;
							continue;
						}
						case 7:
							if (FormulaUtil.FunctionAliasToId.ContainsKey(text2))
							{
								num = 0;
								continue;
							}
							goto IL_2D4;
						case 8:
							if (num2 != -1)
							{
								num = 4;
								continue;
							}
							goto IL_2FE;
						case 9:
						{
							if (FormulaUtil.FunctionIdToParamCount.ContainsKey(excelFunction))
							{
								num = 6;
								continue;
							}
							int a_5 = FormulaUtil.ᜀ(typeof(spr\u231A), 1, A_4, A_5, A_7);
							FormulaToken a_6 = spr\u231A.ᜀ(a_5);
							sprឯ = (sprឯ)FormulaUtil.ᜀ(a_6, excelFunction);
							num = 22;
							continue;
						}
						case 10:
							num = 8;
							continue;
						case 11:
						{
							if (dictionary != null)
							{
								num = 16;
								continue;
							}
							string a_7;
							list.AddRange(this.ᜀ(a_7, A_2, A_3, null, num3, A_6, parseFormulaOptions, A_8, A_9));
							num = 24;
							continue;
						}
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D4;
							default:
								if (false)
								{
								}
								goto IL_383;
							}
							break;
						case 13:
							goto IL_270;
						case 14:
							if (FormulaUtil.ᜀ(text, A_2, out a_8, out num2))
							{
								num = 10;
								continue;
							}
							goto IL_296;
						case 15:
							parseFormulaOptions--;
							num = 3;
							continue;
						case 16:
						{
							string a_7;
							list.AddRange(this.ᜀ(a_7, A_2, A_3, dictionary, num3, A_6, parseFormulaOptions, A_8, A_9));
							num = 19;
							continue;
						}
						case 17:
							list.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, 1, 0));
							num = 12;
							continue;
						case 18:
						{
							if (num4 >= num5)
							{
								num = 26;
								continue;
							}
							string a_7 = array[num4];
							num = 11;
							continue;
						}
						case 19:
							goto IL_1CB;
						case 20:
							goto IL_10E;
						case 21:
							if (FormulaUtil.IndexOf(FormulaUtil.\u171B, excelFunction) != -1)
							{
								num = 17;
								continue;
							}
							goto IL_383;
						case 22:
							goto IL_10E;
						case 23:
							goto IL_270;
						case 24:
							goto IL_1CB;
						case 25:
							if (text2 == RecordTableEnumerator.b("ཅ็", a_))
							{
								num = 1;
								continue;
							}
							num = 7;
							continue;
						case 26:
							goto IL_291;
						}
						break;
						IL_10E:
						num = 21;
						continue;
						IL_1AB:
						parseFormulaOptions |= ParseFormulaOptions.ParseOperand;
						num4 = 0;
						num5 = array.Length;
						num = 13;
						continue;
						IL_1CB:
						num3++;
						num4++;
						num = 23;
						continue;
						IL_270:
						num = 18;
						continue;
						IL_2D4:
						num = 14;
						continue;
						IL_383:
						array = sprឯ.ᜀ(A_0, ref A_1, this);
						num3 = 0;
						dictionary = FormulaUtil.\u1715[excelFunction];
						parseFormulaOptions = A_7;
						num = 2;
					}
				}
				IL_F2:
				return this.ᜁ(a_2, A_0, A_1, A_2, A_3, A_6, A_7, A_8, A_9);
				IL_15D:
				return this.ᜀ(a_2, A_0, A_1, a_8, num2, A_2, A_3, A_6, A_7, A_8, A_9);
				IL_291:
				list.Add(sprឯ);
				return list.ToArray();
				IL_296:
				throw new ArgumentException(RecordTableEnumerator.b("ፅ♇ⅉ≋⅍❏㱑瑓さⵗ㑙㽛⩝य़ൡ੣䙥٧୩ū୭䩯剱即", a_) + text2 + RecordTableEnumerator.b("慅桇ⱉ⍋㱍㵏❑㡓㝕扗穙", a_) + A_0);
				IL_2FE:
				return this.ᜀ(a_2, A_0, A_1, A_2, A_3, A_6, A_7, A_8, A_9);
			}
			}
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x003C3F54 File Offset: 0x003C2F54
		internal static bool ᜀ(string A_0, IWorkbook A_1, out int A_2, out int A_3)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					bool flag;
					INamedRange namedRange;
					switch (num)
					{
					case 0:
					{
						if (A_1 == null)
						{
							num = 7;
							continue;
						}
						XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1;
						Match match = FormulaUtil.\u171A.Match(A_0);
						flag = false;
						num = 2;
						continue;
					}
					case 2:
					{
						Match match;
						if (match.Success)
						{
							num = 11;
							continue;
						}
						goto IL_CE;
					}
					case 3:
					{
						Match match;
						if (match.Value == A_0)
						{
							num = 21;
							continue;
						}
						goto IL_CE;
					}
					case 4:
						goto IL_AB;
					case 5:
					{
						XlsWorkbook xlsWorkbook;
						namedRange = xlsWorkbook.Names.Add(A_0);
						num = 12;
						continue;
					}
					case 6:
						goto IL_AB;
					case 7:
						return false;
					case 8:
						goto IL_AB;
					case 9:
						return flag;
					case 10:
						if (!flag)
						{
							num = 13;
							continue;
						}
						return flag;
					case 11:
						num = 3;
						continue;
					case 12:
						goto IL_1AF;
					case 13:
						num = 17;
						continue;
					case 14:
						goto IL_18D;
					case 15:
						if (A_0.Length == 0)
						{
							num = 14;
							continue;
						}
						A_2 = -1;
						A_3 = -1;
						num = 0;
						continue;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_93;
						default:
						{
							if (false)
							{
							}
							XlsWorkbook xlsWorkbook;
							namedRange = xlsWorkbook.Names[A_0];
							num = 19;
							continue;
						}
						}
						break;
					case 17:
					{
						XlsWorkbook xlsWorkbook;
						if (!xlsWorkbook.ThrowOnUnknownNames)
						{
							num = 16;
							continue;
						}
						return flag;
					}
					case 18:
						goto IL_9C;
					case 19:
						if (namedRange == null)
						{
							num = 5;
							continue;
						}
						goto IL_1AF;
					case 20:
					{
						XlsWorkbook xlsWorkbook;
						if (FormulaUtil.ᜀ(xlsWorkbook, A_0, ref A_3))
						{
							num = 22;
							continue;
						}
						flag = xlsWorkbook.ExternWorkbooks.ContainsExternName(A_0, ref A_2, ref A_3);
						num = 8;
						continue;
					}
					case 21:
					{
						XlsWorkbook xlsWorkbook;
						Match match;
						flag = FormulaUtil.ᜀ(xlsWorkbook, match, ref A_2, ref A_3);
						num = 6;
						continue;
					}
					case 22:
						flag = true;
						num = 4;
						continue;
					}
					goto IL_85;
					IL_93:
					num = 18;
					continue;
					IL_85:
					if (true)
					{
					}
					if (A_0 == null)
					{
						goto IL_93;
					}
					num = 15;
					continue;
					IL_AB:
					num = 10;
					continue;
					IL_CE:
					num = 20;
					continue;
					IL_1AF:
					A_3 = namedRange.Index;
					flag = true;
					num = 9;
				}
				IL_9C:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉ੋ㭍㹏ㅑ⁓㽕㝗㑙ቛ㽝ൟݡ", a_));
				IL_18D:
				throw new ArgumentException(RecordTableEnumerator.b("㕅㱇㡉ੋ㭍㹏ㅑ⁓㽕㝗㑙ቛ㽝ൟݡ䑣䭥䡧ᥩᡫᱭ᥯ᱱ፳噵᭷᭹ቻၽꒃꪉ", a_));
			}
			}
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x003C4230 File Offset: 0x003C3230
		private static bool ᜀ(XlsWorkbook A_0, string A_1, ref int A_2)
		{
			int a_ = 3;
			int num = 6;
			XlsName xlsName;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return false;
				case 1:
					goto IL_7C;
				case 2:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				case 3:
					if (xlsName == null)
					{
						num = 0;
						continue;
					}
					goto IL_10C;
				case 4:
					if (A_1.Length == 0)
					{
						num = 1;
						continue;
					}
					xlsName = (A_0.InnerNamesColection.ᜅ(A_1) as XlsName);
					num = 3;
					continue;
				case 5:
					goto IL_B3;
				case 7:
					goto IL_44;
				}
				if (A_0 == null)
				{
					num = 7;
				}
				else
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬸吺刼吾", a_));
			IL_7C:
			throw new ArgumentException(RecordTableEnumerator.b("䨸伺似社㑀ⵂ♄㍆⁈⑊⍌Ŏぐ㹒ご睖瑘筚⹜⭞፠੢୤f䥨ࡪ౬ŮὰᱲŴ坶᭸Ṻ嵼᩾ﺆ", a_));
			IL_B3:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似社㑀ⵂ♄㍆⁈⑊⍌Ŏぐ㹒ご", a_));
			IL_10C:
			if (true)
			{
			}
			A_2 = xlsName.Index;
			return xlsName.IsFunction;
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x003C4360 File Offset: 0x003C3360
		private static bool ᜀ(XlsWorkbook A_0, Match A_1, ref int A_2, ref int A_3)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 14;
				XlsExternWorkbook xlsExternWorkbook;
				string value2;
				for (;;)
				{
					string text;
					int length;
					string value;
					switch (num)
					{
					case 0:
						goto IL_88;
					case 1:
						return false;
					case 2:
						if (text != null)
						{
							num = 11;
							continue;
						}
						goto IL_2A0;
					case 3:
						if (text.Length == 0)
						{
							num = 9;
							continue;
						}
						goto IL_225;
					case 4:
						num = 5;
						continue;
					case 5:
						if (text[length - 1] == ']')
						{
							num = 17;
							continue;
						}
						goto IL_1F7;
					case 6:
						goto IL_1F7;
					case 7:
						if (xlsExternWorkbook == null)
						{
							num = 1;
							continue;
						}
						goto IL_2B5;
					case 8:
						if (xlsExternWorkbook == null)
						{
							num = 16;
							continue;
						}
						goto IL_2B5;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A0;
						default:
							if (false)
							{
							}
							goto IL_2A0;
						}
						break;
					case 10:
						if (value.Length > 0)
						{
							num = 13;
							continue;
						}
						xlsExternWorkbook = A_0.ExternWorkbooks.GetBookByShortName(text);
						num = 8;
						continue;
					case 11:
						num = 3;
						continue;
					case 12:
						if (text[0] == '[')
						{
							num = 4;
							continue;
						}
						goto IL_1F7;
					case 13:
						xlsExternWorkbook = A_0.ExternWorkbooks[value + text];
						num = 7;
						continue;
					case 15:
						goto IL_225;
					case 16:
						return false;
					case 17:
						text = text.Substring(1, length - 2);
						num = 6;
						continue;
					}
					if (true)
					{
					}
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					value = A_1.Groups[RecordTableEnumerator.b("ፂ⑄㍆ⅈ", a_)].Value;
					text = A_1.Groups[RecordTableEnumerator.b("ł⩄⡆≈Պⱌ≎㑐", a_)].Value;
					string text2 = A_1.Groups[RecordTableEnumerator.b("၂ⵄ≆ⱈ㽊͌⹎㱐㙒", a_)].Value;
					value2 = A_1.Groups[RecordTableEnumerator.b("ᅂ⑄⥆⹈⹊͌⹎㱐㙒", a_)].Value;
					num = 2;
					continue;
					IL_1F7:
					xlsExternWorkbook = null;
					num = 10;
					continue;
					IL_225:
					length = text.Length;
					num = 12;
					continue;
					IL_2A0:
					text = text2;
					text2 = null;
					num = 15;
				}
				IL_88:
				throw new ArgumentNullException(RecordTableEnumerator.b("⹂", a_));
				IL_2B5:
				A_2 = xlsExternWorkbook.Index;
				A_3 = xlsExternWorkbook.ExternNames.ᜂ(value2);
				return A_3 >= 0;
			}
			}
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x003C4644 File Offset: 0x003C3644
		private Ptg[] ᜁ(int A_0, string A_1, int A_2, IWorkbook A_3, IWorksheet A_4, Dictionary<string, string> A_5, ParseFormulaOptions A_6, int A_7, int A_8)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				List<Ptg> list;
				sprឯ sprឯ;
				for (;;)
				{
					list = new List<Ptg>();
					ExcelFunction excelFunction = ExcelFunction.IF;
					FormulaToken a_2 = spr\u231A.ᜀ(A_0);
					sprឯ = (sprឯ)FormulaUtil.ᜀ(a_2, excelFunction);
					int num = 5;
					for (;;)
					{
						int num2;
						int num5;
						int num6;
						int num7;
						Ptg[] array2;
						int num10;
						int num11;
						string[] array3;
						bool flag;
						int num12;
						switch (num)
						{
						case 0:
						{
							List<Ptg[]> list2;
							string a_3;
							Dictionary<Type, sprᨳ> dictionary;
							ParseFormulaOptions parseFormulaOptions;
							list2.Add(this.ᜀ(a_3, A_3, A_4, dictionary, num2, A_5, parseFormulaOptions, A_7, A_8));
							num = 3;
							continue;
						}
						case 1:
							goto IL_3CC;
						case 2:
						{
							List<Ptg[]> list2;
							Ptg[] array = list2[2];
							int num3 = 0;
							int num4 = array.Length;
							num = 33;
							continue;
						}
						case 3:
							goto IL_259;
						case 4:
							goto IL_FC;
						case 5:
							if (FormulaUtil.IndexOf(FormulaUtil.\u171B, excelFunction) != -1)
							{
								num = 9;
								continue;
							}
							goto IL_29A;
						case 6:
							num = 30;
							continue;
						case 7:
							goto IL_1F9;
						case 8:
						{
							Ptg[] array;
							list.AddRange(array);
							list.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, new object[]
							{
								num5,
								3
							}));
							num = 12;
							continue;
						}
						case 9:
							list.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, 1, 0));
							num = 13;
							continue;
						case 10:
						{
							int num3;
							int num4;
							if (num3 >= num4)
							{
								num = 11;
								continue;
							}
							Ptg[] array;
							ExcelVersion version;
							num6 += array[num3].GetSize(version);
							num3++;
							num = 32;
							continue;
						}
						case 11:
							num6 += 4;
							num = 7;
							continue;
						case 12:
							goto IL_4D1;
						case 13:
							goto IL_29A;
						case 14:
						{
							Ptg[] array;
							if (array != null)
							{
								num = 8;
								continue;
							}
							goto IL_546;
						}
						case 15:
							goto IL_417;
						case 16:
						{
							List<Ptg[]> list2;
							list.AddRange(list2[0]);
							num7 = 0;
							num6 = 0;
							array2 = list2[1];
							Ptg[] array = null;
							ExcelVersion version = ((XlsWorkbook)A_3).Version;
							int num8 = 0;
							int num9 = array2.Length;
							num = 15;
							continue;
						}
						case 17:
						{
							if (num10 >= num11)
							{
								num = 16;
								continue;
							}
							string a_3 = array3[num10];
							num = 29;
							continue;
						}
						case 18:
							goto IL_417;
						case 19:
							goto IL_3C7;
						case 20:
							goto IL_259;
						case 21:
							num = 22;
							continue;
						case 22:
							if (array3.Length == 3)
							{
								num = 2;
								continue;
							}
							goto IL_1F9;
						case 23:
						{
							if (array3.Length < 2)
							{
								num = 19;
								continue;
							}
							num2 = 0;
							Dictionary<Type, sprᨳ> dictionary = FormulaUtil.\u1715[excelFunction];
							List<Ptg[]> list2 = new List<Ptg[]>(4);
							ParseFormulaOptions parseFormulaOptions = A_6;
							num = 25;
							continue;
						}
						case 24:
							if (!flag)
							{
								num = 6;
								continue;
							}
							num = 27;
							continue;
						case 25:
							if ((A_6 & ParseFormulaOptions.RootLevel) != ParseFormulaOptions.None)
							{
								num = 26;
								continue;
							}
							goto IL_FC;
						case 26:
						{
							ParseFormulaOptions parseFormulaOptions;
							parseFormulaOptions--;
							num = 4;
							continue;
						}
						case 27:
							num12 = 8;
							goto IL_2CA;
						case 28:
						{
							int num8;
							int num9;
							if (num8 >= num9)
							{
								num = 21;
								continue;
							}
							ExcelVersion version;
							num7 += array2[num8].GetSize(version);
							num8++;
							num = 18;
							continue;
						}
						case 29:
						{
							Dictionary<Type, sprᨳ> dictionary;
							if (dictionary != null)
							{
								num = 0;
								continue;
							}
							if (true)
							{
							}
							List<Ptg[]> list2;
							string a_3;
							ParseFormulaOptions parseFormulaOptions;
							list2.Add(this.ᜀ(a_3, A_3, A_4, null, num2, A_5, parseFormulaOptions, A_7, A_8));
							num = 20;
							continue;
						}
						case 30:
							num12 = 0;
							goto IL_2CA;
						case 31:
							goto IL_3CC;
						case 32:
							goto IL_46B;
						case 33:
							goto IL_46B;
						case 34:
							if (array3.Length <= 3)
							{
								num = 35;
								continue;
							}
							goto IL_285;
						case 35:
							num = 23;
							continue;
						}
						break;
						IL_FC:
						num10 = 0;
						num11 = array3.Length;
						num = 1;
						continue;
						IL_1F9:
						list.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, new object[]
						{
							2,
							num7 + 4
						}));
						list.AddRange(array2);
						flag = ((A_6 & ParseFormulaOptions.InArray) == ParseFormulaOptions.None);
						num = 24;
						continue;
						IL_259:
						num2++;
						num10++;
						num = 31;
						continue;
						IL_29A:
						array3 = sprឯ.ᜀ(A_1, ref A_2, this);
						num = 34;
						continue;
						IL_2CA:
						num5 = num12;
						list.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, new object[]
						{
							num5,
							num6 + 3
						}));
						num = 14;
						continue;
						IL_3CC:
						num = 17;
						continue;
						IL_417:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_285;
						default:
							if (false)
							{
							}
							num = 28;
							continue;
						}
						IL_46B:
						num = 10;
					}
				}
				IL_285:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("े㡉⭋㭍㵏㝑㩓≕硗㥙㍛⭝๟ᙡ䑣୥ᵧᥩᡫ乭ቯ᝱味䑵塷ᕹ๻幽덿", a_), A_1);
				IL_3C7:
				goto IL_285;
				IL_4D1:
				IL_546:
				list.Add(sprឯ);
				return list.ToArray();
			}
			}
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x003C4BA4 File Offset: 0x003C3BA4
		private Ptg[] ᜀ(int A_0, string A_1, int A_2, IWorkbook A_3, IWorksheet A_4, Dictionary<string, string> A_5, ParseFormulaOptions A_6, int A_7, int A_8)
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
			string leftUnaryOperand = this.GetLeftUnaryOperand(A_1, A_2);
			XlsWorkbook xlsWorkbook = (XlsWorkbook)A_3;
			int a_;
			int nameIndexes = xlsWorkbook.ExternWorkbooks.GetNameIndexes(leftUnaryOperand, out a_);
			return this.ᜀ(A_0, A_1, A_2, nameIndexes, a_, A_3, A_4, A_5, A_6, A_7, A_8);
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x003C4C18 File Offset: 0x003C3C18
		private Ptg[] ᜀ(int A_0, string A_1, int A_2, int A_3, int A_4, IWorkbook A_5, IWorksheet A_6, Dictionary<string, string> A_7, ParseFormulaOptions A_8, int A_9, int A_10)
		{
			switch (0)
			{
			default:
			{
				List<Ptg> list;
				sprឯ sprឯ;
				for (;;)
				{
					list = new List<Ptg>();
					ExcelFunction excelFunction = ExcelFunction.CustomFunction;
					FormulaToken a_ = spr\u231A.ᜀ(A_0);
					sprឯ = (sprឯ)FormulaUtil.ᜀ(a_, excelFunction);
					int num = 5;
					for (;;)
					{
						Ptg ptg;
						int num2;
						int num3;
						string[] array;
						Dictionary<Type, sprᨳ> dictionary;
						int num4;
						ParseFormulaOptions parseFormulaOptions;
						switch (num)
						{
						case 0:
							ptg = FormulaUtil.ᜀ(FormulaToken.tNameX1, new object[]
							{
								A_3,
								A_4
							});
							goto IL_188;
						case 1:
							num = 10;
							continue;
						case 2:
							if (A_3 == -1)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							num = 0;
							continue;
						case 3:
							goto IL_134;
						case 4:
						{
							if (num2 >= num3)
							{
								num = 8;
								continue;
							}
							string a_2 = array[num2];
							num = 12;
							continue;
						}
						case 5:
							if (FormulaUtil.IndexOf(FormulaUtil.\u171B, excelFunction) != -1)
							{
								num = 6;
								continue;
							}
							goto IL_20D;
						case 6:
							list.Add(FormulaUtil.ᜀ(FormulaToken.tAttr, 1, 0));
							num = 13;
							continue;
						case 7:
							IL_2C4:
							goto IL_134;
						case 8:
							goto IL_155;
						case 9:
							goto IL_C9;
						case 10:
							ptg = FormulaUtil.ᜀ(FormulaToken.tName1, new object[]
							{
								A_4
							});
							goto IL_188;
						case 11:
							goto IL_2A2;
						case 12:
						{
							if (dictionary != null)
							{
								num = 17;
								continue;
							}
							string a_2;
							list.AddRange(this.ᜀ(a_2, A_5, A_6, null, num4, A_7, parseFormulaOptions, A_9, A_10));
							num = 14;
							continue;
						}
						case 13:
							goto IL_20D;
						case 14:
							goto IL_2A2;
						case 15:
							if ((A_8 & ParseFormulaOptions.RootLevel) != ParseFormulaOptions.None)
							{
								num = 16;
								continue;
							}
							goto IL_C9;
						case 16:
							parseFormulaOptions--;
							num = 9;
							continue;
						case 17:
						{
							string a_2;
							list.AddRange(this.ᜀ(a_2, A_5, A_6, dictionary, num4, A_7, parseFormulaOptions, A_9, A_10));
							num = 11;
							continue;
						}
						}
						break;
						IL_C9:
						num = 2;
						continue;
						IL_134:
						num = 4;
						continue;
						IL_20D:
						XlsWorkbook xlsWorkbook = (XlsWorkbook)A_5;
						array = sprឯ.ᜀ(A_1, ref A_2, this);
						num4 = 0;
						dictionary = FormulaUtil.\u1715[excelFunction];
						parseFormulaOptions = A_8;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C4;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						IL_188:
						Ptg item = ptg;
						list.Add(item);
						num2 = 0;
						num3 = array.Length;
						num = 3;
						continue;
						IL_2A2:
						num4++;
						num2++;
						num = 7;
					}
				}
				IL_155:
				list.Add(sprឯ);
				return list.ToArray();
			}
			}
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x003C4EFC File Offset: 0x003C3EFC
		internal static Ptg ᜃ(string A_0, int A_1)
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
			string key = FormulaUtil.ᜀ(A_0, A_1);
			ConstructorInfo constructorInfo = FormulaUtil.ErrorNameToConstructor[key];
			return (Ptg)constructorInfo.Invoke(new object[]
			{
				A_0
			});
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x003C4F64 File Offset: 0x003C3F64
		private Ptg[] ᜀ(string A_0, IWorkbook A_1, IWorksheet A_2, Dictionary<Type, sprᨳ> A_3, int A_4, Dictionary<string, string> A_5, ParseFormulaOptions A_6, int A_7, int A_8)
		{
			while (A_0.Length != 0)
			{
				Ptg[] result;
				try
				{
					A_6 |= ParseFormulaOptions.ParseOperand;
					result = this.ᜁ(A_0, A_2, A_3, A_4, A_5, A_6, A_7, A_8);
					goto IL_46;
				}
				catch (Exception)
				{
					throw;
				}
				break;
				IL_46:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (false)
				{
				}
				return result;
				IL_2A:
				if (true)
				{
				}
				return new Ptg[]
				{
					FormulaUtil.ᜀ(FormulaToken.tMissingArgument, A_0)
				};
			}
			goto IL_2A;
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x003C4FF0 File Offset: 0x003C3FF0
		private static sprឯ ᜀ(char A_0)
		{
			string text;
			for (;;)
			{
				if (true)
				{
				}
				text = A_0.ToString();
				if (A_0 == '(')
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_1;
			}
			return (sprឯ)FormulaUtil.ᜀ(FormulaToken.tParentheses, text);
			Block_1:
			if (false)
			{
			}
			FormulaToken a_ = spr\u23FA.ᜀ(text);
			return (sprឯ)FormulaUtil.ᜀ(a_, text);
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x003C505C File Offset: 0x003C405C
		private sprឯ ᜁ(string A_0)
		{
			for (;;)
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_48;
					case 1:
						goto IL_54;
					case 2:
						num = 1;
						continue;
					}
					if (!(A_0 == this.ᜧ))
					{
						if (true)
						{
						}
						num = 2;
					}
					else
					{
						num = 0;
					}
				}
				IL_54:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_74;
				}
			}
			IL_48:
			FormulaToken formulaToken = FormulaToken.tCellRangeList;
			goto IL_82;
			IL_74:
			if (false)
			{
			}
			formulaToken = spr᱃.ᜀ(A_0);
			IL_82:
			FormulaToken a_ = formulaToken;
			return (sprឯ)FormulaUtil.ᜀ(a_, A_0);
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x003C50F8 File Offset: 0x003C40F8
		public static bool IsCell(string strFormula, bool bR1C1, out string strRow, out string strColumn)
		{
			int a_ = 8;
			int num = 11;
			bool flag2;
			for (;;)
			{
				Match match;
				bool flag;
				Regex regex;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					flag = (match.Value == strFormula);
					goto IL_15E;
				case 2:
					strRow = match.Groups[RecordTableEnumerator.b("氽⼿㕁畃", a_)].Value;
					strColumn = match.Groups[RecordTableEnumerator.b("紽⼿⹁ㅃ⭅♇等", a_)].Value;
					num = 10;
					continue;
				case 3:
					flag = false;
					goto IL_15E;
				case 4:
					if (match.Success)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_104;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 5:
					if (flag2)
					{
						num = 2;
						continue;
					}
					strRow = null;
					strColumn = null;
					num = 9;
					continue;
				case 6:
					goto IL_104;
				case 7:
					regex = FormulaUtil.CellR1C1Regex;
					goto IL_10B;
				case 8:
					num = 6;
					continue;
				case 9:
					return flag2;
				case 10:
					return flag2;
				case 11:
					if (true)
					{
					}
					break;
				}
				if (!bR1C1)
				{
					num = 8;
					continue;
				}
				num = 7;
				continue;
				IL_10B:
				Regex regex2 = regex;
				match = regex2.Match(strFormula);
				num = 4;
				continue;
				IL_104:
				regex = FormulaUtil.CellRegex;
				goto IL_10B;
				IL_15E:
				flag2 = flag;
				num = 5;
			}
			return flag2;
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x003C5288 File Offset: 0x003C4288
		internal bool ᜀ(string A_0, bool A_1, out string A_2, out string A_3, out string A_4, out string A_5)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				bool flag;
				for (;;)
				{
					A_3 = null;
					A_5 = null;
					A_2 = null;
					A_4 = null;
					int num = 8;
					for (;;)
					{
						Regex regex;
						Match match;
						switch (num)
						{
						case 0:
							A_2 = A_0;
							A_4 = A_0;
							num = 18;
							continue;
						case 1:
							if (flag)
							{
								goto IL_39B;
							}
							return flag;
						case 2:
							regex = FormulaUtil.CellRangeRegex;
							goto IL_14C;
						case 3:
							match = FormulaUtil.CellRangeR1C1ShortRegex.Match(A_0);
							flag = FormulaUtil.ᜀ(match, A_0);
							num = 6;
							continue;
						case 4:
							return flag;
						case 5:
							num = 2;
							continue;
						case 6:
							if (A_0[0] == 'R')
							{
								num = 0;
								continue;
							}
							A_5 = A_0;
							A_3 = A_0;
							num = 4;
							continue;
						case 7:
							if (flag)
							{
								num = 17;
								continue;
							}
							match = FormulaUtil.FullColumnRangeRegex.Match(A_0);
							flag = FormulaUtil.ᜀ(match, A_0);
							num = 1;
							continue;
						case 8:
							if (!A_1)
							{
								num = 5;
								continue;
							}
							num = 14;
							continue;
						case 9:
							return flag;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_39B;
							default:
								if (false)
								{
								}
								A_3 = match.Groups[RecordTableEnumerator.b("琶嘸场䠼刾⽀牂", a_)].Value;
								A_5 = match.Groups[RecordTableEnumerator.b("琶嘸场䠼刾⽀煂", a_)].Value;
								A_2 = RecordTableEnumerator.b("ጶ࠸", a_);
								A_4 = RecordTableEnumerator.b("ጶ", a_) + this.ᜡ.MaxRowCount.ToString();
								num = 9;
								continue;
							}
							break;
						case 11:
							if (true)
							{
							}
							if (flag)
							{
								num = 12;
								continue;
							}
							num = 16;
							continue;
						case 12:
							A_3 = match.Groups[RecordTableEnumerator.b("琶嘸场䠼刾⽀牂", a_)].Value;
							A_5 = match.Groups[RecordTableEnumerator.b("琶嘸场䠼刾⽀煂", a_)].Value;
							A_2 = match.Groups[RecordTableEnumerator.b("收嘸䰺఼", a_)].Value;
							A_4 = match.Groups[RecordTableEnumerator.b("收嘸䰺༼", a_)].Value;
							num = 13;
							continue;
						case 13:
							return flag;
						case 14:
							regex = FormulaUtil.CellRangeR1C1Regex;
							goto IL_14C;
						case 15:
							return flag;
						case 16:
							if (A_1)
							{
								num = 3;
								continue;
							}
							match = FormulaUtil.FullRowRangeRegex.Match(A_0);
							flag = FormulaUtil.ᜀ(match, A_0);
							num = 7;
							continue;
						case 17:
							A_3 = RecordTableEnumerator.b("ጶ砸", a_);
							A_5 = RecordTableEnumerator.b("ጶ", a_) + sprṔ.ᜀ(this.ᜡ.MaxColumnCount);
							A_2 = match.Groups[RecordTableEnumerator.b("收嘸䰺఼", a_)].Value;
							A_4 = match.Groups[RecordTableEnumerator.b("收嘸䰺༼", a_)].Value;
							num = 15;
							continue;
						case 18:
							return flag;
						}
						break;
						IL_14C:
						Regex regex2 = regex;
						match = regex2.Match(A_0);
						flag = FormulaUtil.ᜀ(match, A_0);
						num = 11;
						continue;
						IL_39B:
						num = 10;
					}
				}
				return flag;
			}
			}
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x003C5660 File Offset: 0x003C4660
		private static bool ᜀ(Match A_0, string A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Index == 0)
					{
						num = 2;
						continue;
					}
					return false;
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_7D;
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
					if (!A_0.Success)
					{
						return false;
					}
					break;
				}
				if (true)
				{
				}
				num = 1;
			}
			IL_7D:
			return A_0.Length == A_1.Length;
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x003C56F8 File Offset: 0x003C46F8
		public static bool IsCell3D(string strFormula, bool bR1C1, out string strSheetName, out string strRow, out string strColumn)
		{
			int a_ = 12;
			int num = 2;
			Match match;
			for (;;)
			{
				Regex regex;
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					regex = FormulaUtil.CellR1C13DRegex;
					goto IL_16C;
				case 1:
					num = 8;
					continue;
				case 3:
					flag = false;
					goto IL_124;
				case 4:
					goto IL_141;
				case 5:
					if (flag2)
					{
						num = 4;
						continue;
					}
					goto IL_1B4;
				case 6:
					if (match.Index == 0)
					{
						num = 1;
						continue;
					}
					goto IL_59;
				case 7:
					if (true)
					{
					}
					regex = FormulaUtil.Cell3DRegex;
					goto IL_16C;
				case 8:
					goto IL_6F;
				case 9:
					if (match.Success)
					{
						num = 10;
						continue;
					}
					goto IL_59;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 11:
					num = 7;
					continue;
				}
				if (!bR1C1)
				{
					num = 11;
					continue;
				}
				num = 0;
				continue;
				IL_59:
				num = 3;
				continue;
				IL_124:
				flag2 = flag;
				num = 5;
				continue;
				IL_6F:
				flag = (match.Length == strFormula.Length);
				goto IL_124;
				IL_16C:
				Regex regex2 = regex;
				match = regex2.Match(strFormula);
				num = 9;
			}
			IL_141:
			strSheetName = match.Groups[RecordTableEnumerator.b("ᅁⱃ⍅ⵇ㹉ɋ⽍㵏㝑", a_)].Value;
			strSheetName = FormulaUtil.ᜂ(strSheetName);
			strRow = match.Groups[RecordTableEnumerator.b("၁⭃ㅅ祇", a_)].Value;
			strColumn = match.Groups[RecordTableEnumerator.b("Ł⭃⩅㵇❉≋罍", a_)].Value;
			return true;
			IL_1B4:
			strSheetName = null;
			strRow = null;
			strColumn = null;
			return false;
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x003C58C4 File Offset: 0x003C48C4
		public bool IsCellRange3D(string strFormula, bool bR1C1, out string strSheetName, out string strRow1, out string strColumn1, out string strRow2, out string strColumn2)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				Match match;
				bool flag;
				for (;;)
				{
					IL_CB:
					strSheetName = null;
					strRow1 = null;
					strColumn1 = null;
					strRow2 = null;
					strColumn2 = null;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_2B8:
						if (match.Index != 0)
						{
							goto IL_263;
						}
						num = 14;
						break;
					default:
						if (false)
						{
						}
						num = 7;
						break;
					}
					for (;;)
					{
						IL_1C:
						Regex regex;
						bool flag2;
						bool flag3;
						Regex regex2;
						switch (num)
						{
						case 0:
							num = 36;
							continue;
						case 1:
							goto IL_319;
						case 2:
							goto IL_4D9;
						case 3:
							num = 41;
							continue;
						case 4:
							goto IL_5FE;
						case 5:
							num = 22;
							continue;
						case 6:
							if (!flag)
							{
								num = 19;
								continue;
							}
							goto IL_4FD;
						case 7:
							if (!bR1C1)
							{
								num = 21;
								continue;
							}
							num = 11;
							continue;
						case 8:
							strSheetName = match.Groups[RecordTableEnumerator.b("ᩈ⍊⡌⩎═ᵒ㑔㩖㱘", a_)].Value;
							num = 31;
							continue;
						case 9:
							if (bR1C1)
							{
								num = 12;
								continue;
							}
							goto IL_3BC;
						case 10:
							if (match.Success)
							{
								num = 17;
								continue;
							}
							goto IL_130;
						case 11:
							regex = FormulaUtil.CellRangeR1C13DRegex;
							goto IL_51D;
						case 12:
							match = FormulaUtil.CellRangeR1C13DShortRegex.Match(strFormula);
							flag = FormulaUtil.ᜀ(match, strFormula);
							num = 18;
							continue;
						case 13:
							if (match.Success)
							{
								num = 5;
								continue;
							}
							goto IL_263;
						case 14:
							if (true)
							{
							}
							num = 40;
							continue;
						case 15:
							flag2 = false;
							goto IL_6A7;
						case 16:
							goto IL_4FD;
						case 17:
							num = 26;
							continue;
						case 18:
							if (flag)
							{
								num = 8;
								continue;
							}
							goto IL_6B9;
						case 19:
							num = 35;
							continue;
						case 20:
							regex = FormulaUtil.CellRange3DRegex;
							goto IL_51D;
						case 21:
							num = 20;
							continue;
						case 22:
							goto IL_2B8;
						case 23:
							if (!flag)
							{
								num = 34;
								continue;
							}
							goto IL_3BC;
						case 24:
							if (flag)
							{
								num = 2;
								continue;
							}
							match = FormulaUtil.Full3DColumnRangeRegex.Match(strFormula);
							flag = FormulaUtil.ᜀ(match, strFormula);
							num = 30;
							continue;
						case 25:
							strSheetName = match.Groups[RecordTableEnumerator.b("ᩈ⍊⡌⩎═ᵒ㑔㩖㱘", a_)].Value;
							strRow1 = match.Groups[RecordTableEnumerator.b("ᭈ⑊㩌繎", a_)].Value;
							strColumn1 = match.Groups[RecordTableEnumerator.b("ੈ⑊⅌㩎㱐㵒摔", a_)].Value;
							strRow2 = match.Groups[RecordTableEnumerator.b("ᭈ⑊㩌絎", a_)].Value;
							strColumn2 = match.Groups[RecordTableEnumerator.b("ੈ⑊⅌㩎㱐㵒杔", a_)].Value;
							num = 4;
							continue;
						case 26:
							if (match.Index == 0)
							{
								num = 3;
								continue;
							}
							goto IL_130;
						case 27:
						{
							string text;
							strRow1 = (text = strFormula.Substring(strSheetName.Length + 1));
							strRow2 = text;
							num = 29;
							continue;
						}
						case 28:
							flag3 = false;
							goto IL_486;
						case 29:
							goto IL_1D6;
						case 30:
							if (flag)
							{
								num = 1;
								continue;
							}
							goto IL_399;
						case 31:
						{
							if (strFormula[strSheetName.Length + 1] == 'R')
							{
								num = 27;
								continue;
							}
							string text2;
							strColumn2 = (text2 = strFormula.Substring(strSheetName.Length + 1));
							strColumn1 = text2;
							num = 33;
							continue;
						}
						case 32:
							if (flag)
							{
								num = 25;
								continue;
							}
							goto IL_6B9;
						case 33:
							goto IL_23E;
						case 34:
							num = 9;
							continue;
						case 35:
							if (!bR1C1)
							{
								num = 0;
								continue;
							}
							num = 39;
							continue;
						case 36:
							regex2 = FormulaUtil.CellRange3DRegex2;
							goto IL_182;
						case 37:
							match = FormulaUtil.Full3DRowRangeRegex.Match(strFormula);
							flag = FormulaUtil.ᜀ(match, strFormula);
							num = 24;
							continue;
						case 38:
							if (!flag)
							{
								num = 37;
								continue;
							}
							goto IL_399;
						case 39:
							regex2 = FormulaUtil.CellRangeR1C13DRegex2;
							goto IL_182;
						case 40:
							flag3 = (match.Length == strFormula.Length);
							goto IL_486;
						case 41:
							flag2 = (match.Length == strFormula.Length);
							goto IL_6A7;
						}
						goto IL_CB;
						IL_130:
						num = 15;
						continue;
						IL_182:
						Regex regex3 = regex2;
						match = regex3.Match(strFormula);
						num = 10;
						continue;
						IL_399:
						num = 23;
						continue;
						IL_3BC:
						num = 32;
						continue;
						IL_486:
						flag = flag3;
						num = 6;
						continue;
						IL_4FD:
						num = 38;
						continue;
						IL_51D:
						regex3 = regex;
						match = regex3.Match(strFormula);
						num = 13;
						continue;
						IL_6A7:
						flag = flag2;
						num = 16;
					}
					IL_263:
					num = 28;
					goto IL_1C;
				}
				IL_1D6:
				IL_23E:
				goto IL_6B9;
				IL_319:
				strSheetName = match.Groups[RecordTableEnumerator.b("ᩈ⍊⡌⩎═ᵒ㑔㩖㱘", a_)].Value;
				strColumn1 = match.Groups[RecordTableEnumerator.b("ੈ⑊⅌㩎㱐㵒摔", a_)].Value;
				strColumn2 = match.Groups[RecordTableEnumerator.b("ੈ⑊⅌㩎㱐㵒杔", a_)].Value;
				strRow1 = RecordTableEnumerator.b("浈穊", a_);
				strRow2 = RecordTableEnumerator.b("浈", a_) + this.ᜡ.MaxRowCount.ToString();
				strSheetName = FormulaUtil.ᜂ(strSheetName);
				return flag;
				IL_4D9:
				strSheetName = match.Groups[RecordTableEnumerator.b("ᩈ⍊⡌⩎═ᵒ㑔㩖㱘", a_)].Value;
				strColumn1 = RecordTableEnumerator.b("浈੊", a_);
				strColumn2 = RecordTableEnumerator.b("浈", a_) + sprṔ.ᜀ(this.ᜡ.MaxColumnCount);
				strRow1 = match.Groups[RecordTableEnumerator.b("ᭈ⑊㩌繎", a_)].Value;
				strRow2 = match.Groups[RecordTableEnumerator.b("ᭈ⑊㩌絎", a_)].Value;
				strSheetName = FormulaUtil.ᜂ(strSheetName);
				return flag;
				IL_5FE:
				IL_6B9:
				strSheetName = FormulaUtil.ᜂ(strSheetName);
				return flag;
			}
			}
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x003C5F94 File Offset: 0x003C4F94
		private static bool ᜂ(string A_0, int A_1)
		{
			bool result;
			using (Dictionary<string, ConstructorInfo>.KeyCollection.Enumerator enumerator = FormulaUtil.ErrorNameToConstructor.Keys.GetEnumerator())
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B0:
					num = 5;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 1:
						result = true;
						num = 4;
						continue;
					case 2:
					{
						string text;
						if (string.Compare(A_0, A_1, text, 0, text.Length) == 0)
						{
							num = 1;
							continue;
						}
						break;
					}
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 6;
							continue;
						}
						string text = enumerator.Current;
						num = 2;
						continue;
					}
					case 4:
						goto IL_AE;
					case 5:
						goto IL_B8;
					case 6:
						goto IL_77;
					}
					IL_5E:
					num = 3;
					continue;
					goto IL_5E;
				}
				IL_77:
				goto IL_B0;
				IL_AE:
				goto IL_CB;
				IL_B8:
				return false;
			}
			IL_CB:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x003C6090 File Offset: 0x003C5090
		private static bool ᜀ(string A_0, IWorkbook A_1, IWorksheet A_2)
		{
			bool flag;
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
				for (;;)
				{
					flag = false;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!flag)
							{
								num = 7;
								continue;
							}
							return flag;
						case 1:
							return flag;
						case 2:
							if (A_2 != null)
							{
								num = 4;
								continue;
							}
							goto IL_82;
						case 3:
							goto IL_82;
						case 4:
							flag = ((XlsWorksheet)A_2).Names.Contains(A_0);
							num = 3;
							continue;
						case 5:
							if (A_1 != null)
							{
								num = 6;
								continue;
							}
							return flag;
						case 6:
							flag = A_1.Names.Contains(A_0);
							num = 1;
							continue;
						case 7:
							num = 5;
							continue;
						}
						break;
						IL_82:
						num = 0;
					}
				}
				break;
			}
			return flag;
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x003C6170 File Offset: 0x003C5170
		private static int ᜀ(string A_0, int A_1, char[] A_2, int A_3)
		{
			int a_ = 3;
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = false;
					int num = FormulaUtil.ᜀ(FormulaUtil.OpenBrackets, A_0[A_1]);
					int num2 = 19;
					for (;;)
					{
						int num3;
						int length;
						switch (num2)
						{
						case 0:
							if (FormulaUtil.ᜀ(A_2, A_0[num3]) != -1)
							{
								num2 = 6;
								continue;
							}
							goto IL_ED;
						case 1:
							goto IL_176;
						case 2:
							if (num3 < 0)
							{
								num2 = 8;
								continue;
							}
							if (true)
							{
							}
							num2 = 10;
							continue;
						case 3:
							flag = true;
							num2 = 1;
							continue;
						case 4:
						{
							char c;
							if (FormulaUtil.ᜀ(FormulaUtil.StringBrackets, c) != -1)
							{
								num2 = 3;
								continue;
							}
							goto IL_176;
						}
						case 5:
							return num3;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_152;
							default:
								if (false)
								{
								}
								num3 = FormulaUtil.ᜀ(A_0, num3, A_2, A_3);
								num2 = 18;
								continue;
							}
							break;
						case 7:
						{
							char c = FormulaUtil.CloseBrackets[num];
							num2 = 20;
							continue;
						}
						case 8:
							goto IL_171;
						case 9:
							if (!flag)
							{
								num2 = 17;
								continue;
							}
							goto IL_ED;
						case 10:
						{
							char c;
							if (A_0[num3] == c)
							{
								num2 = 5;
								continue;
							}
							num2 = 9;
							continue;
						}
						case 11:
							if (num3 < length)
							{
								num2 = 16;
								continue;
							}
							goto IL_28F;
						case 12:
							goto IL_1B0;
						case 13:
							goto IL_130;
						case 14:
							goto IL_209;
						case 15:
						{
							if (num == -1)
							{
								num2 = 13;
								continue;
							}
							char c = FormulaUtil.OpenBrackets[num];
							num2 = 14;
							continue;
						}
						case 16:
							goto IL_152;
						case 17:
							num2 = 0;
							continue;
						case 18:
							goto IL_ED;
						case 19:
							if (num != -1)
							{
								num2 = 7;
								continue;
							}
							num = FormulaUtil.ᜀ(FormulaUtil.CloseBrackets, A_0[A_1]);
							num2 = 15;
							continue;
						case 20:
							goto IL_209;
						case 21:
							goto IL_1B0;
						}
						break;
						IL_ED:
						num3 += A_3;
						num2 = 21;
						continue;
						IL_152:
						num2 = 2;
						continue;
						IL_176:
						num3 = A_1 + A_3;
						length = A_0.Length;
						num2 = 12;
						continue;
						IL_1B0:
						num2 = 11;
						continue;
						IL_209:
						num2 = 4;
					}
				}
				IL_130:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("樸䬺堼尾⡀╂ⱄ≆ⵈ歊㵌⁎≐㩒⅔㹖㙘㕚絜㙞በ䍢୤ࡦᵨ䭪౬佮ŰᱲٴṶ൸ቺቼᅾꆀꞆ力敖", a_));
				IL_171:
				IL_28F:
				throw new ArgumentException(RecordTableEnumerator.b("簸䌺䴼䴾⑀あ㙄⹆♈╊浌♎≐獒㱔㥖⽘㩚ㅜ㙞ՠ䵢䕤⑦ࡨժ䩬᭮兰ᕲᱴ᥶ᵸ孺Ṽၾ麗ﶒ랖ﮘﲜﲞ쪠욢톤", a_));
			}
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x003C6420 File Offset: 0x003C5420
		private static bool ᜁ(string A_0, int A_1)
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
			return FormulaUtil.ᜀ(A_0, A_1, FormulaUtil.UnaryOperations) != -1;
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x003C6470 File Offset: 0x003C5470
		private bool ᜀ(string A_0, int A_1, out int A_2)
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
			A_2 = FormulaUtil.ᜀ(A_0, A_1, this.ᜤ);
			return A_2 != -1;
		}

		// Token: 0x06005F83 RID: 24451 RVA: 0x003C64C4 File Offset: 0x003C54C4
		private static bool ᜀ(string A_0, out int A_1)
		{
			for (;;)
			{
				A_1 = -1;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A7;
					case 1:
						if (A_1 != -1)
						{
							num = 0;
							continue;
						}
						return false;
					case 2:
						if (FormulaUtil.ᜀ(FormulaUtil.StringBrackets, A_0[0]) == -1)
						{
							A_1 = A_0.IndexOf('(');
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A7;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_65;
					}
					break;
				}
			}
			IL_65:
			if (true)
			{
			}
			return false;
			IL_A7:
			return FormulaUtil.FindCorrespondingBracket(A_0, A_1) == A_0.Length - 1;
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x003C6580 File Offset: 0x003C5580
		private static string ᜀ(string A_0, int A_1)
		{
			int a_ = 14;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_14F;
				case 2:
					goto IL_3B;
				}
				if (A_0[A_1] != '#')
				{
					num = 2;
				}
				else
				{
					Dictionary<string, ConstructorInfo>.KeyCollection.Enumerator enumerator = FormulaUtil.ErrorNameToConstructor.Keys.GetEnumerator();
					if (true)
					{
					}
					num = 0;
				}
			}
			IL_3B:
			IL_FA:
			throw new ArgumentException(RecordTableEnumerator.b("੃⥅㱇橉⥋㱍≏㵑♓癕⭗⹙⹛㝝๟ա", a_));
			IL_14F:
			try
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_DF:
					num = 5;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				string result;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A3;
					case 2:
					{
						string text;
						if (string.Compare(A_0, A_1, text, 0, text.Length) == 0)
						{
							num = 3;
							continue;
						}
						break;
					}
					case 3:
					{
						string text;
						result = text;
						num = 6;
						continue;
					}
					case 4:
					{
						Dictionary<string, ConstructorInfo>.KeyCollection.Enumerator enumerator;
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						string text = enumerator.Current;
						num = 2;
						continue;
					}
					case 5:
						goto IL_EA;
					case 6:
						goto IL_AF;
					}
					IL_8A:
					num = 4;
					continue;
					goto IL_8A;
				}
				IL_A3:
				goto IL_DF;
				IL_AF:
				return result;
				IL_EA:
				goto IL_10E;
			}
			finally
			{
				Dictionary<string, ConstructorInfo>.KeyCollection.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			goto IL_FA;
			IL_10E:
			throw new ArgumentException(RecordTableEnumerator.b("Ń㑅㩇╉㹋湍㹏㍑㥓㍕硗ⵙ㵛ⵝ䁟ౡୣብ䡧౩ͫ᭭ṯᙱ", a_));
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x003C66F4 File Offset: 0x003C56F4
		private static int ᜀ(Type A_0, Dictionary<Type, sprᨳ> A_1, int A_2)
		{
			int num = 3;
			sprᨳ sprᨳ;
			for (;;)
			{
				switch (num)
				{
				case 0:
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
						if (A_1.TryGetValue(A_0, out sprᨳ))
						{
							num = 5;
							continue;
						}
						break;
					}
					num = 1;
					continue;
				case 1:
					if (sprᨳ == null)
					{
						num = 2;
						continue;
					}
					return 2;
				case 2:
					num = 4;
					continue;
				case 4:
					if (A_0 != typeof(sprᦊ))
					{
						num = 7;
						continue;
					}
					return 2;
				case 5:
					goto IL_CA;
				case 6:
					return 2;
				case 7:
					goto IL_67;
				}
				if (A_1 == null)
				{
					num = 6;
				}
				else
				{
					num = 0;
				}
			}
			return 2;
			IL_67:
			return FormulaUtil.ᜀ(typeof(sprᦊ), A_1, A_2);
			IL_CA:
			return sprᨳ.ᜀ(A_2);
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x003C67EC File Offset: 0x003C57EC
		internal static int ᜀ(Type A_0, int A_1, Dictionary<Type, sprᨳ> A_2, int A_3, ParseFormulaOptions A_4)
		{
			int num3;
			for (;;)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = (A_4 & ParseFormulaOptions.InName) != ParseFormulaOptions.None;
						int num = 15;
						for (;;)
						{
							int num2;
							int num4;
							switch (num)
							{
							case 0:
								num2 = 3;
								goto IL_1A0;
							case 1:
								return 2;
							case 2:
								num = 8;
								continue;
							case 3:
								num = 4;
								continue;
							case 4:
							{
								bool flag2;
								if (!flag2)
								{
									num = 19;
									continue;
								}
								num = 12;
								continue;
							}
							case 5:
								num3 = 2;
								num = 6;
								continue;
							case 6:
								goto IL_215;
							case 7:
								if (!flag)
								{
									num = 3;
									continue;
								}
								num = 11;
								continue;
							case 8:
							{
								bool flag3;
								if (!flag3)
								{
									num = 5;
									continue;
								}
								return num3;
							}
							case 9:
								num = 10;
								continue;
							case 10:
							{
								bool flag4;
								if (flag4)
								{
									if (true)
									{
									}
									num = 2;
									continue;
								}
								return num3;
							}
							case 11:
								num4 = 2;
								goto IL_C9;
							case 12:
								num4 = 2;
								goto IL_C9;
							case 13:
								goto IL_95;
							case 14:
								num4 = 0;
								goto IL_C9;
							case 15:
								if (flag)
								{
									num = 13;
									continue;
								}
								num = 16;
								continue;
							case 16:
							{
								if (A_2 == null)
								{
									num = 1;
									continue;
								}
								int num5 = FormulaUtil.ᜀ(A_0, A_2, A_3) - 1;
								bool flag3 = (A_4 & ParseFormulaOptions.RootLevel) != ParseFormulaOptions.None;
								bool flag2 = (A_4 & ParseFormulaOptions.InArray) != ParseFormulaOptions.None;
								bool flag4 = (A_4 & ParseFormulaOptions.ParseComplexOperand) != ParseFormulaOptions.None;
								num = 21;
								continue;
							}
							case 17:
								if (num3 == 1)
								{
									num = 9;
									continue;
								}
								return num3;
							case 18:
								num = 20;
								continue;
							case 19:
								num = 14;
								continue;
							case 20:
							{
								int num5;
								num2 = num5;
								goto IL_1A0;
							}
							case 21:
							{
								bool flag3;
								if (!flag3)
								{
									num = 18;
									continue;
								}
								num = 0;
								continue;
							}
							}
							break;
							IL_C9:
							int num6 = num4;
							int num7;
							num3 = FormulaUtil.\u1713[num7][A_1][num6];
							num = 17;
							continue;
							IL_1A0:
							num7 = num2;
							num = 7;
						}
					}
					IL_95:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_22D;
					}
					break;
				}
			}
			IL_215:
			return num3;
			IL_22D:
			if (false)
			{
			}
			return 1;
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x003C6A50 File Offset: 0x003C5A50
		internal static Ptg ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
		{
			int a_ = 12;
			FormulaToken formulaToken;
			Ptg ptg;
			for (;;)
			{
				formulaToken = (FormulaToken)A_0.ReadByte(A_1);
				if (!FormulaUtil.\u1719.TryGetValue(formulaToken, out ptg))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				goto IL_70;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("Ł╃⡅♇╉㡋湍㙏㭑㩓㉕硗᱙㍛ⱝൟᝡࡣݥ䡧ṩͫխᕯᱱ味ŵᅷ๹ᑻ幽늇ꪉ", a_) + formulaToken);
			IL_70:
			ptg = (Ptg)ptg.Clone();
			ptg.TokenCode = formulaToken;
			A_1++;
			ptg.InfillPTG(A_0, ref A_1, A_2);
			return ptg;
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x003C6AF0 File Offset: 0x003C5AF0
		internal static Ptg ᜁ(FormulaToken A_0)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜊ();
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x003C6B48 File Offset: 0x003C5B48
		internal static Ptg ᜀ(FormulaToken A_0)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_0);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x003C6BA0 File Offset: 0x003C5BA0
		internal static Ptg ᜀ(FormulaToken A_0, string A_1)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x003C6BF8 File Offset: 0x003C5BF8
		internal static Ptg ᜀ(FormulaToken A_0, string A_1, IWorkbook A_2)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1, A_2);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x003C6C50 File Offset: 0x003C5C50
		internal static Ptg ᜀ(FormulaToken A_0, params object[] A_1)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x003C6CA8 File Offset: 0x003C5CA8
		[CLSCompliant(false)]
		internal static Ptg ᜀ(FormulaToken A_0, ushort A_1, ushort A_2)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1, A_2);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x003C6D00 File Offset: 0x003C5D00
		[CLSCompliant(false)]
		internal static Ptg ᜀ(FormulaToken A_0, ExcelFunction A_1)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x003C6D58 File Offset: 0x003C5D58
		internal static Ptg ᜀ(FormulaToken A_0, int A_1, int A_2, string A_3, string A_4, bool A_5)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1, A_2, A_3, A_4, A_5);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x003C6DB8 File Offset: 0x003C5DB8
		internal static Ptg ᜀ(FormulaToken A_0, int A_1, int A_2, string A_3, string A_4, string A_5, string A_6, bool A_7, IWorkbook A_8)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x003C6E1C File Offset: 0x003C5E1C
		internal static Ptg ᜀ(FormulaToken A_0, int A_1, int A_2, int A_3, string A_4, string A_5, string A_6, string A_7, bool A_8, IWorkbook A_9)
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
			FormulaUtil.ᜀ ᜀ = FormulaUtil.\u1718[A_0];
			Ptg ptg = ᜀ.ᜀ(A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			ptg.TokenCode = A_0;
			return ptg;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x003C6E84 File Offset: 0x003C5E84
		private static Ptg[] ᜀ(Ptg[] A_0)
		{
			switch (0)
			{
			default:
			{
				List<Ptg> list;
				for (;;)
				{
					list = new List<Ptg>();
					int num = 0;
					int num2 = 16;
					for (;;)
					{
						Ptg ptg;
						switch (num2)
						{
						case 0:
							if (!(ptg is spr\u1DFC))
							{
								num2 = 5;
								continue;
							}
							goto IL_E6;
						case 1:
						{
							sprᯡ sprᯡ = ptg as sprᯡ;
							num2 = 2;
							continue;
						}
						case 2:
						{
							sprᯡ sprᯡ;
							if (!sprᯡ.ᜅ())
							{
								num2 = 6;
								continue;
							}
							goto IL_E6;
						}
						case 3:
							goto IL_E6;
						case 4:
							goto IL_83;
						case 5:
							num2 = 14;
							continue;
						case 6:
							num2 = 15;
							continue;
						case 7:
						{
							sprᯡ sprᯡ;
							if (!sprᯡ.ᜆ())
							{
								num2 = 9;
								continue;
							}
							goto IL_E6;
						}
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_10C;
							default:
								if (false)
								{
								}
								num2 = 7;
								continue;
							}
							break;
						case 9:
							goto IL_1D4;
						case 10:
							if (num >= A_0.Length)
							{
								num2 = 12;
								continue;
							}
							ptg = A_0[num];
							num2 = 11;
							continue;
						case 11:
							goto IL_10C;
						case 12:
							goto IL_169;
						case 13:
							goto IL_149;
						case 14:
							if (ptg is sprᢜ)
							{
								num2 = 17;
								continue;
							}
							goto IL_83;
						case 15:
						{
							sprᯡ sprᯡ;
							if (!sprᯡ.ᜏ())
							{
								num2 = 8;
								continue;
							}
							goto IL_E6;
						}
						case 16:
							goto IL_149;
						case 17:
						{
							sprᢜ sprᢜ = ptg as sprᢜ;
							list.AddRange(sprᢜ.ᜂ());
							num2 = 4;
							continue;
						}
						}
						break;
						IL_83:
						list.Add(ptg);
						num2 = 3;
						continue;
						IL_E6:
						num++;
						num2 = 13;
						continue;
						IL_10C:
						if (ptg is sprᯡ)
						{
							num2 = 1;
							continue;
						}
						goto IL_1D4;
						IL_149:
						num2 = 10;
						continue;
						IL_1D4:
						num2 = 0;
					}
				}
				IL_169:
				if (true)
				{
				}
				return list.ToArray();
			}
			}
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x003C709C File Offset: 0x003C609C
		private string ᜀ(string A_0)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					if (A_0[A_0.Length - 1] != '%')
					{
						num = 6;
						continue;
					}
					string leftUnaryOperand = this.GetLeftUnaryOperand(A_0, A_0.Length - 1);
					int startIndex = A_0.Length - leftUnaryOperand.Length - 1;
					A_0 = A_0.Insert(startIndex, RecordTableEnumerator.b("捅", a_));
					A_0 = A_0.Substring(0, A_0.Length - 1);
					num2--;
					num = 2;
					continue;
				}
				case 2:
					if (true)
					{
					}
					if (num2 == 0)
					{
						num = 5;
						continue;
					}
					goto IL_108;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_147;
					default:
						goto IL_59;
					}
					break;
				case 4:
					goto IL_147;
				case 5:
					goto IL_EF;
				case 6:
					return A_0;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				num2 = A_0.Length;
				num = 4;
				continue;
				IL_108:
				num = 0;
				continue;
				IL_147:
				goto IL_108;
			}
			IL_59:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㡉ੋ⅍≏㽑⅓㩕㥗", a_));
			IL_EF:
			throw new ArgumentException(RecordTableEnumerator.b("㕅㱇㡉ੋ⅍≏㽑⅓㩕㥗", a_));
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x003C71F4 File Offset: 0x003C61F4
		internal static void ᜀ(string A_0, ExcelFunction A_1, sprᨳ[] A_2, int A_3)
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
			FormulaUtil.FunctionAliasToId.Remove(A_0);
			FormulaUtil.\u1715.Remove(A_1);
			FormulaUtil.FunctionIdToAlias.Remove(A_1);
			FormulaUtil.FunctionIdToParamCount.Remove(A_1);
			FormulaUtil.ᜁ(A_0, A_1, A_2, A_3);
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005F95 RID: 24469 RVA: 0x003C7268 File Offset: 0x003C6268
		public static Dictionary<int, string> ErrorCodeToName
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
				return FormulaUtil.\u1716;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x003C72A8 File Offset: 0x003C62A8
		public static Dictionary<string, int> ErrorNameToCode
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
				return FormulaUtil.\u1717;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005F97 RID: 24471 RVA: 0x003C72E8 File Offset: 0x003C62E8
		public string ArrayRowSeparator
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
				return this.ᜦ;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005F98 RID: 24472 RVA: 0x003C732C File Offset: 0x003C632C
		public string OperandsSeparator
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
				return this.ᜧ;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005F99 RID: 24473 RVA: 0x003C7370 File Offset: 0x003C6370
		// (set) Token: 0x06005F9A RID: 24474 RVA: 0x003C73B4 File Offset: 0x003C63B4
		public NumberFormatInfo NumberFormat
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005F9B RID: 24475 RVA: 0x003C73F8 File Offset: 0x003C63F8
		public IWorkbook ParentWorkbook
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
				return this.ᜡ;
			}
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x003C743C File Offset: 0x003C643C
		internal static bool ᜁ(ExcelFunction A_0)
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
			return Array.IndexOf<ExcelFunction>(FormulaUtil.\u171F, A_0) >= 0;
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x003C7488 File Offset: 0x003C6488
		internal static bool ᜀ(ExcelFunction A_0)
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
			return Array.IndexOf<ExcelFunction>(FormulaUtil.\u171E, A_0) >= 0;
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x003C74D4 File Offset: 0x003C64D4
		internal bool ᜂ(Ptg[] A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 9;
				for (;;)
				{
					int num2;
					int num3;
					bool result;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A7;
						default:
						{
							if (false)
							{
							}
							if (num2 >= num3)
							{
								num = 2;
								continue;
							}
							Ptg ptg = A_0[num2];
							sprẄ sprẄ = ptg as sprẄ;
							num = 7;
							continue;
						}
						}
						break;
					case 1:
						goto IL_B5;
					case 2:
						return result;
					case 3:
					{
						sprẄ sprẄ;
						if (this.ᜡ.IsExternalReference((int)sprẄ.ᜁ()))
						{
							num = 4;
							continue;
						}
						goto IL_F1;
					}
					case 4:
						result = true;
						num = 10;
						continue;
					case 5:
						return false;
					case 6:
						goto IL_B5;
					case 7:
					{
						sprẄ sprẄ;
						if (sprẄ != null)
						{
							num = 8;
							continue;
						}
						goto IL_F1;
					}
					case 8:
						if (true)
						{
						}
						num = 3;
						continue;
					case 10:
						return result;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					result = false;
					num2 = 0;
					num3 = A_0.Length;
					IL_A7:
					num = 6;
					continue;
					IL_B5:
					num = 0;
					continue;
					IL_F1:
					num2++;
					num = 1;
				}
				return false;
			}
			}
		}

		// Token: 0x04002DC5 RID: 11717
		internal const int ᜀ = 0;

		// Token: 0x04002DC6 RID: 11718
		internal const int ᜁ = 1;

		// Token: 0x04002DC7 RID: 11719
		private const int ᜂ = 2;

		// Token: 0x04002DC8 RID: 11720
		private const int ᜃ = 0;

		// Token: 0x04002DC9 RID: 11721
		private const int ᜄ = 1;

		// Token: 0x04002DCA RID: 11722
		private const int ᜅ = 2;

		// Token: 0x04002DCB RID: 11723
		private const int ᜆ = 3;

		// Token: 0x04002DCC RID: 11724
		public const int DEF_NAME_INDEX = 1;

		// Token: 0x04002DCD RID: 11725
		public const int DEF_REFERENCE_INDEX = 2;

		// Token: 0x04002DCE RID: 11726
		public const int DEF_ARRAY_INDEX = 2;

		// Token: 0x04002DCF RID: 11727
		internal const int ᜇ = 8;

		// Token: 0x04002DD0 RID: 11728
		internal const int ᜈ = 0;

		// Token: 0x04002DD1 RID: 11729
		private const char ᜉ = '[';

		// Token: 0x04002DD2 RID: 11730
		private const char ᜊ = ']';

		// Token: 0x04002DD3 RID: 11731
		internal const string ᜋ = "Column1";

		// Token: 0x04002DD4 RID: 11732
		internal const string ᜌ = "Column2";

		// Token: 0x04002DD5 RID: 11733
		internal const string \u170D = "Row1";

		// Token: 0x04002DD6 RID: 11734
		internal const string ᜎ = "Row2";

		// Token: 0x04002DD7 RID: 11735
		private const char ᜏ = '\'';

		// Token: 0x04002DD8 RID: 11736
		public const string Excel2010FunctionPrefix = "_xlfn.";

		// Token: 0x04002DD9 RID: 11737
		public const string DEF_SHEETNAME_GROUP = "SheetName";

		// Token: 0x04002DDA RID: 11738
		public const string DEF_BOOKNAME_GROUP = "BookName";

		// Token: 0x04002DDB RID: 11739
		public const string DEF_RANGENAME_GROUP = "RangeName";

		// Token: 0x04002DDC RID: 11740
		public const string DEF_ROW_GROUP = "Row1";

		// Token: 0x04002DDD RID: 11741
		public const string DEF_COLUMN_GROUP = "Column1";

		// Token: 0x04002DDE RID: 11742
		public const string DEF_PATH_GROUP = "Path";

		// Token: 0x04002DDF RID: 11743
		private const string ᜐ = "SheetName";

		// Token: 0x04002DE0 RID: 11744
		private const string ᜑ = "[^][:\\/?]*";

		// Token: 0x04002DE1 RID: 11745
		private static RegexOptions \u1712;

		// Token: 0x04002DE2 RID: 11746
		private static readonly int[][][] \u1713;

		// Token: 0x04002DE3 RID: 11747
		public static readonly char[] OpenBrackets;

		// Token: 0x04002DE4 RID: 11748
		public static readonly char[] CloseBrackets;

		// Token: 0x04002DE5 RID: 11749
		public static readonly char[] StringBrackets;

		// Token: 0x04002DE6 RID: 11750
		public static readonly string[] UnaryOperations;

		// Token: 0x04002DE7 RID: 11751
		public static readonly string[] PlusMinusArray;

		// Token: 0x04002DE8 RID: 11752
		private static readonly SortedList \u1714;

		// Token: 0x04002DE9 RID: 11753
		public static readonly Dictionary<ExcelFunction, string> FunctionIdToAlias;

		// Token: 0x04002DEA RID: 11754
		public static readonly Dictionary<ExcelFunction, int> FunctionIdToParamCount;

		// Token: 0x04002DEB RID: 11755
		public static readonly Dictionary<string, ExcelFunction> FunctionAliasToId;

		// Token: 0x04002DEC RID: 11756
		internal static readonly Dictionary<ExcelFunction, Dictionary<Type, sprᨳ>> \u1715;

		// Token: 0x04002DED RID: 11757
		public static readonly Dictionary<string, ConstructorInfo> ErrorNameToConstructor;

		// Token: 0x04002DEE RID: 11758
		private static readonly Dictionary<int, string> \u1716;

		// Token: 0x04002DEF RID: 11759
		private static readonly Dictionary<string, int> \u1717;

		// Token: 0x04002DF0 RID: 11760
		private static readonly Dictionary<FormulaToken, FormulaUtil.ᜀ> \u1718;

		// Token: 0x04002DF1 RID: 11761
		private static readonly Dictionary<FormulaToken, Ptg> \u1719;

		// Token: 0x04002DF2 RID: 11762
		public static readonly Regex CellRegex;

		// Token: 0x04002DF3 RID: 11763
		public static readonly Regex CellR1C1Regex;

		// Token: 0x04002DF4 RID: 11764
		public static readonly Regex CellRangeRegex;

		// Token: 0x04002DF5 RID: 11765
		public static readonly Regex FullRowRangeRegex;

		// Token: 0x04002DF6 RID: 11766
		public static readonly Regex FullColumnRangeRegex;

		// Token: 0x04002DF7 RID: 11767
		public static readonly Regex Full3DRowRangeRegex;

		// Token: 0x04002DF8 RID: 11768
		public static readonly Regex Full3DColumnRangeRegex;

		// Token: 0x04002DF9 RID: 11769
		public static readonly Regex CellRangeR1C1Regex;

		// Token: 0x04002DFA RID: 11770
		public static readonly Regex CellRangeR1C1ShortRegex;

		// Token: 0x04002DFB RID: 11771
		public static readonly Regex CellRangeR1C13DShortRegex;

		// Token: 0x04002DFC RID: 11772
		public static readonly Regex Cell3DRegex;

		// Token: 0x04002DFD RID: 11773
		public static readonly Regex CellR1C13DRegex;

		// Token: 0x04002DFE RID: 11774
		public static readonly Regex CellRange3DRegex;

		// Token: 0x04002DFF RID: 11775
		public static readonly Regex CellRange3DRegex2;

		// Token: 0x04002E00 RID: 11776
		public static readonly Regex CellRangeR1C13DRegex;

		// Token: 0x04002E01 RID: 11777
		public static readonly Regex CellRangeR1C13DRegex2;

		// Token: 0x04002E02 RID: 11778
		private static readonly Regex \u171A;

		// Token: 0x04002E03 RID: 11779
		internal static readonly ExcelFunction[] \u171B;

		// Token: 0x04002E04 RID: 11780
		internal static readonly FormulaToken[] \u171C;

		// Token: 0x04002E05 RID: 11781
		internal static readonly FormulaToken[] \u171D;

		// Token: 0x04002E06 RID: 11782
		private static readonly ExcelFunction[] \u171E;

		// Token: 0x04002E07 RID: 11783
		private static readonly ExcelFunction[] \u171F;

		// Token: 0x04002E08 RID: 11784
		private NumberFormatInfo ᜠ;

		// Token: 0x04002E09 RID: 11785
		private XlsWorkbook ᜡ;

		// Token: 0x04002E0A RID: 11786
		private static readonly string[] ᜢ;

		// Token: 0x04002E0B RID: 11787
		private string[][] ᜣ;

		// Token: 0x04002E0C RID: 11788
		private SortedList ᜤ;

		// Token: 0x04002E0D RID: 11789
		private SortedList[] ᜥ;

		// Token: 0x04002E0E RID: 11790
		private string ᜦ;

		// Token: 0x04002E0F RID: 11791
		private string ᜧ;

		// Token: 0x04002E10 RID: 11792
		private spr\u236F ᜨ;

		// Token: 0x04002E11 RID: 11793
		private static EvaluateEventHandler ᜩ;

		// Token: 0x02000622 RID: 1570
		internal enum ConstructorId
		{
			// Token: 0x04002E13 RID: 11795
			Default,
			// Token: 0x04002E14 RID: 11796
			String,
			// Token: 0x04002E15 RID: 11797
			ByteArrayOffset,
			// Token: 0x04002E16 RID: 11798
			StringParent,
			// Token: 0x04002E17 RID: 11799
			TwoUShorts,
			// Token: 0x04002E18 RID: 11800
			FunctionIndex,
			// Token: 0x04002E19 RID: 11801
			TwoStrings,
			// Token: 0x04002E1A RID: 11802
			FourStrings,
			// Token: 0x04002E1B RID: 11803
			Int3String4Bool,
			// Token: 0x04002E1C RID: 11804
			TokenType
		}

		// Token: 0x02000623 RID: 1571
		internal class ᜀ
		{
			// Token: 0x06005F9F RID: 24479 RVA: 0x003C761C File Offset: 0x003C661C
			private ᜀ()
			{
				this.ᜀ = new Dictionary<int, ConstructorInfo>();
				base..ctor();
			}

			// Token: 0x06005FA0 RID: 24480 RVA: 0x003C763C File Offset: 0x003C663C
			public ᜀ(Type A_0)
			{
				int a_ = 13;
				this.ᜀ = new Dictionary<int, ConstructorInfo>();
				base..ctor();
				if (A_0 == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("㝂㱄㝆ⱈ", a_), RecordTableEnumerator.b("ᝂ⩄ⱆⱈ╊浌㭎⡐⍒ご睖㩘㩚㍜硞ᕠ䍢ݤɦ䥨ժᡬͮᵰ", a_));
				}
				if (!A_0.IsSubclassOf(typeof(Ptg)))
				{
					throw new ArgumentException(RecordTableEnumerator.b("⁂⥄♆㩈㡊浌㱎㥐㱒⁔㭖㵘筚㽜㩞䅠ݢdᑦ੨๪ͬ୮ၰᵲŴ坶ᙸᵺ嵼⽾", a_), RecordTableEnumerator.b("㝂㱄㝆ⱈ", a_));
				}
				this.ᜁ = A_0;
				this.ᜉ(A_0.GetConstructor(Type.EmptyTypes));
				this.ᜈ(A_0.GetConstructor(new Type[]
				{
					typeof(string)
				}));
				this.ᜇ(A_0.GetConstructor(new Type[]
				{
					typeof(DataProvider),
					typeof(int)
				}));
				this.ᜆ(A_0.GetConstructor(new Type[]
				{
					typeof(string),
					typeof(IWorkbook)
				}));
				this.ᜅ(A_0.GetConstructor(new Type[]
				{
					typeof(ushort),
					typeof(ushort)
				}));
				this.ᜄ(A_0.GetConstructor(new Type[]
				{
					typeof(ExcelFunction)
				}));
				Type[] types = new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(string),
					typeof(string),
					typeof(bool)
				};
				this.ᜃ(A_0.GetConstructor(types));
				types = new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(string),
					typeof(string),
					typeof(string),
					typeof(string),
					typeof(bool),
					typeof(IWorkbook)
				};
				this.ᜂ(A_0.GetConstructor(types));
				types = new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(string),
					typeof(string),
					typeof(string),
					typeof(string),
					typeof(bool),
					typeof(IWorkbook)
				};
				this.ᜁ(A_0.GetConstructor(types));
				types = new Type[]
				{
					typeof(FormulaToken)
				};
				this.ᜀ(A_0.GetConstructor(types));
			}

			// Token: 0x06005FA1 RID: 24481 RVA: 0x003C7950 File Offset: 0x003C6950
			public Ptg ᜊ()
			{
				Ptg result;
				try
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
					result = (Ptg)this.ᜉ().Invoke(null);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				return result;
			}

			// Token: 0x06005FA2 RID: 24482 RVA: 0x003C79B8 File Offset: 0x003C69B8
			public Ptg ᜀ(FormulaToken A_0)
			{
				Ptg result;
				try
				{
					switch (1 == 1)
					{
					}
					if (false)
					{
					}
					result = (Ptg)this.ᜀ().Invoke(new object[]
					{
						A_0
					});
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				if (true)
				{
				}
				return result;
			}

			// Token: 0x06005FA3 RID: 24483 RVA: 0x003C7A30 File Offset: 0x003C6A30
			public Ptg ᜀ(string A_0)
			{
				Ptg result;
				try
				{
					switch (1 == 1)
					{
					}
					if (false)
					{
					}
					result = (Ptg)this.ᜈ().Invoke(new object[]
					{
						A_0
					});
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				if (true)
				{
				}
				return result;
			}

			// Token: 0x06005FA4 RID: 24484 RVA: 0x003C7AA4 File Offset: 0x003C6AA4
			public Ptg ᜀ(DataProvider A_0, ref int A_1, ParseParameters A_2)
			{
				Ptg result;
				try
				{
					switch (1 == 1)
					{
					}
					if (false)
					{
					}
					Ptg ptg = (Ptg)this.ᜇ().Invoke(new object[]
					{
						A_0,
						A_1
					});
					A_1 += ptg.GetSize(A_2.Version);
					result = ptg;
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				if (true)
				{
				}
				return result;
			}

			// Token: 0x06005FA5 RID: 24485 RVA: 0x003C7B34 File Offset: 0x003C6B34
			public Ptg ᜀ(params object[] A_0)
			{
				switch (0)
				{
				default:
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_AE:
						num = 0;
						break;
					default:
						if (false)
						{
						}
						goto IL_47;
					}
					int num2;
					Type[] array;
					for (;;)
					{
						IL_2C:
						ConstructorInfo constructor;
						switch (num)
						{
						case 0:
							goto IL_BC;
						case 1:
							try
							{
								return (Ptg)constructor.Invoke(A_0);
							}
							catch (TargetInvocationException ex)
							{
								throw ex.InnerException;
							}
							goto IL_87;
						case 2:
							goto IL_87;
						case 3:
							goto IL_BC;
						case 4:
							if (num2 >= A_0.Length)
							{
								num = 2;
								continue;
							}
							goto IL_9F;
						}
						goto IL_47;
						IL_87:
						constructor = this.ᜁ.GetConstructor(array);
						num = 1;
						continue;
						IL_BC:
						num = 4;
					}
					IL_9F:
					array[num2] = A_0[num2].GetType();
					num2++;
					goto IL_AE;
					IL_47:
					if (true)
					{
					}
					array = new Type[A_0.Length];
					num2 = 0;
					num = 3;
					goto IL_2C;
				}
				}
			}

			// Token: 0x06005FA6 RID: 24486 RVA: 0x003C7C30 File Offset: 0x003C6C30
			public Ptg ᜀ(string A_0, IWorkbook A_1)
			{
				Ptg result;
				try
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
					result = (Ptg)this.ᜆ().Invoke(new object[]
					{
						A_0,
						A_1
					});
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				return result;
			}

			// Token: 0x06005FA7 RID: 24487 RVA: 0x003C7CA8 File Offset: 0x003C6CA8
			public Ptg ᜀ(ushort A_0, ushort A_1)
			{
				Ptg result;
				try
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
					result = (Ptg)this.ᜅ().Invoke(new object[]
					{
						A_0,
						A_1
					});
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				return result;
			}

			// Token: 0x06005FA8 RID: 24488 RVA: 0x003C7D2C File Offset: 0x003C6D2C
			public Ptg ᜀ(ExcelFunction A_0)
			{
				Ptg result;
				try
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
					result = (Ptg)this.ᜄ().Invoke(new object[]
					{
						A_0
					});
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				return result;
			}

			// Token: 0x06005FA9 RID: 24489 RVA: 0x003C7DA4 File Offset: 0x003C6DA4
			public Ptg ᜀ(int A_0, int A_1, string A_2, string A_3, bool A_4)
			{
				Ptg result;
				try
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
					result = (Ptg)this.ᜃ().Invoke(new object[]
					{
						A_0,
						A_1,
						A_2,
						A_3,
						A_4
					});
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				return result;
			}

			// Token: 0x06005FAA RID: 24490 RVA: 0x003C7E38 File Offset: 0x003C6E38
			public Ptg ᜀ(int A_0, int A_1, string A_2, string A_3, string A_4, string A_5, bool A_6, IWorkbook A_7)
			{
				Ptg result;
				try
				{
					switch (1 == 1)
					{
					}
					if (false)
					{
					}
					object[] parameters = new object[]
					{
						A_0,
						A_1,
						A_2,
						A_3,
						A_4,
						A_5,
						A_6,
						A_7
					};
					result = (Ptg)this.ᜂ().Invoke(parameters);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				if (true)
				{
				}
				return result;
			}

			// Token: 0x06005FAB RID: 24491 RVA: 0x003C7EE0 File Offset: 0x003C6EE0
			public Ptg ᜀ(int A_0, int A_1, int A_2, string A_3, string A_4, string A_5, string A_6, bool A_7, IWorkbook A_8)
			{
				Ptg result;
				try
				{
					switch (1 == 1)
					{
					}
					if (false)
					{
					}
					object[] parameters = new object[]
					{
						A_0,
						A_1,
						A_2,
						A_3,
						A_4,
						A_5,
						A_6,
						A_7,
						A_8
					};
					result = (Ptg)this.ᜁ().Invoke(parameters);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				if (true)
				{
				}
				return result;
			}

			// Token: 0x06005FAC RID: 24492 RVA: 0x003C7F90 File Offset: 0x003C6F90
			private ConstructorInfo ᜉ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.Default);
			}

			// Token: 0x06005FAD RID: 24493 RVA: 0x003C7FD4 File Offset: 0x003C6FD4
			private void ᜉ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.Default, A_0);
			}

			// Token: 0x06005FAE RID: 24494 RVA: 0x003C8018 File Offset: 0x003C7018
			private ConstructorInfo ᜈ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.String);
			}

			// Token: 0x06005FAF RID: 24495 RVA: 0x003C805C File Offset: 0x003C705C
			private void ᜈ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.String, A_0);
			}

			// Token: 0x06005FB0 RID: 24496 RVA: 0x003C80A0 File Offset: 0x003C70A0
			private ConstructorInfo ᜇ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.ByteArrayOffset);
			}

			// Token: 0x06005FB1 RID: 24497 RVA: 0x003C80E4 File Offset: 0x003C70E4
			private void ᜇ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.ByteArrayOffset, A_0);
			}

			// Token: 0x06005FB2 RID: 24498 RVA: 0x003C8128 File Offset: 0x003C7128
			private ConstructorInfo ᜆ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.StringParent);
			}

			// Token: 0x06005FB3 RID: 24499 RVA: 0x003C816C File Offset: 0x003C716C
			private void ᜆ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.StringParent, A_0);
			}

			// Token: 0x06005FB4 RID: 24500 RVA: 0x003C81B0 File Offset: 0x003C71B0
			private ConstructorInfo ᜅ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.TwoUShorts);
			}

			// Token: 0x06005FB5 RID: 24501 RVA: 0x003C81F4 File Offset: 0x003C71F4
			private void ᜅ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.TwoUShorts, A_0);
			}

			// Token: 0x06005FB6 RID: 24502 RVA: 0x003C8238 File Offset: 0x003C7238
			private ConstructorInfo ᜄ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.FunctionIndex);
			}

			// Token: 0x06005FB7 RID: 24503 RVA: 0x003C827C File Offset: 0x003C727C
			private void ᜄ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.FunctionIndex, A_0);
			}

			// Token: 0x06005FB8 RID: 24504 RVA: 0x003C82C0 File Offset: 0x003C72C0
			private ConstructorInfo ᜃ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.TwoStrings);
			}

			// Token: 0x06005FB9 RID: 24505 RVA: 0x003C8304 File Offset: 0x003C7304
			private void ᜃ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.TwoStrings, A_0);
			}

			// Token: 0x06005FBA RID: 24506 RVA: 0x003C8348 File Offset: 0x003C7348
			private ConstructorInfo ᜂ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.FourStrings);
			}

			// Token: 0x06005FBB RID: 24507 RVA: 0x003C838C File Offset: 0x003C738C
			private void ᜂ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.FourStrings, A_0);
			}

			// Token: 0x06005FBC RID: 24508 RVA: 0x003C83D0 File Offset: 0x003C73D0
			private ConstructorInfo ᜁ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.Int3String4Bool);
			}

			// Token: 0x06005FBD RID: 24509 RVA: 0x003C8414 File Offset: 0x003C7414
			private void ᜁ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.Int3String4Bool, A_0);
			}

			// Token: 0x06005FBE RID: 24510 RVA: 0x003C8458 File Offset: 0x003C7458
			private ConstructorInfo ᜀ()
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
				return this.ᜀ(FormulaUtil.ConstructorId.TokenType);
			}

			// Token: 0x06005FBF RID: 24511 RVA: 0x003C849C File Offset: 0x003C749C
			private void ᜀ(ConstructorInfo A_0)
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
				this.ᜀ(FormulaUtil.ConstructorId.TokenType, A_0);
			}

			// Token: 0x06005FC0 RID: 24512 RVA: 0x003C84E0 File Offset: 0x003C74E0
			private ConstructorInfo ᜀ(FormulaUtil.ConstructorId A_0)
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
				ConstructorInfo result;
				this.ᜀ.TryGetValue((int)A_0, out result);
				return result;
			}

			// Token: 0x06005FC1 RID: 24513 RVA: 0x003C852C File Offset: 0x003C752C
			private void ᜀ(FormulaUtil.ConstructorId A_0, ConstructorInfo A_1)
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
					if (A_1 != null)
					{
						this.ᜀ[(int)A_0] = A_1;
						return;
					}
					break;
				}
				this.ᜀ.Remove((int)A_0);
			}

			// Token: 0x04002E1D RID: 11805
			private Dictionary<int, ConstructorInfo> ᜀ;

			// Token: 0x04002E1E RID: 11806
			private Type ᜁ;
		}
	}
}
