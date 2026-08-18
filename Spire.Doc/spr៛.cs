using System;
using System.Collections;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020001E7 RID: 487
internal class spr\u17DB
{
	// Token: 0x06001531 RID: 5425 RVA: 0x001597BC File Offset: 0x001587BC
	private spr\u17DB()
	{
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x001597D0 File Offset: 0x001587D0
	internal static MailMergeMainDocumentType ᜄ(string A_0)
	{
		int a_ = 2;
		int num = 9;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_5B;
			case 1:
				spr᧓.ᜥ = new Dictionary<string, int>(8)
				{
					{
						ClipboardData.b("୧୩ᡫ཭ᱯᵱ፳", a_),
						0
					},
					{
						ClipboardData.b("൧ݩ൫ݭᱯ", a_),
						1
					},
					{
						ClipboardData.b("൧ѩᩫ୭ᱯᵱѳ፵୷", a_),
						2
					},
					{
						ClipboardData.b("๧୩ᑫ", a_),
						3
					},
					{
						ClipboardData.b("๧թṫͭ㱯᝱sɵᵷࡹཻ", a_),
						4
					},
					{
						ClipboardData.b("๧թṫͭ嵯ṱᅳɵ౷ό๻ൽ", a_),
						5
					},
					{
						ClipboardData.b("է୩իɭ᥯ᱱ፳㩵᥷᡹᥻ች", a_),
						6
					},
					{
						ClipboardData.b("է୩իɭ᥯ᱱ፳孵ᑷ᭹ṻ᭽", a_),
						7
					}
				};
				num = 0;
				continue;
			case 2:
				num = 4;
				continue;
			case 3:
				goto IL_63;
			case 4:
				return MailMergeMainDocumentType.FormLetters;
			case 5:
				num = 6;
				continue;
			case 6:
				if (spr᧓.ᜥ == null)
				{
					num = 1;
					continue;
				}
				goto IL_5B;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_63;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			case 8:
				switch (num2)
				{
				case 0:
					return MailMergeMainDocumentType.Catalog;
				case 1:
					return MailMergeMainDocumentType.Email;
				case 2:
					return MailMergeMainDocumentType.Envelopes;
				case 3:
					return MailMergeMainDocumentType.Fax;
				case 4:
					return MailMergeMainDocumentType.FormLetters;
				case 5:
					return MailMergeMainDocumentType.FormLetters;
				case 6:
					return MailMergeMainDocumentType.MailingLabels;
				case 7:
					return MailMergeMainDocumentType.MailingLabels;
				default:
					num = 2;
					continue;
				}
				break;
			}
			if (A_0 != null)
			{
				num = 5;
				continue;
			}
			return MailMergeMainDocumentType.FormLetters;
			IL_5B:
			num = 3;
			continue;
			IL_63:
			if (true)
			{
			}
			if (!spr᧓.ᜥ.TryGetValue(A_0, out num2))
			{
				return MailMergeMainDocumentType.FormLetters;
			}
			num = 7;
		}
		return MailMergeMainDocumentType.FormLetters;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x001599D4 File Offset: 0x001589D4
	internal static string ᜀ(MailMergeMainDocumentType A_0, bool A_1)
	{
		int a_ = 16;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1CA;
				case 1:
					if (A_0 <= MailMergeMainDocumentType.Catalog)
					{
						num = 12;
						continue;
					}
					num = 13;
					continue;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_15C;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CA;
					default:
						goto IL_EE;
					}
					break;
				case 5:
					if (A_0 != MailMergeMainDocumentType.Catalog)
					{
						num = 11;
						continue;
					}
					goto IL_1DC;
				case 6:
					num = 5;
					continue;
				case 7:
					goto IL_12E;
				case 8:
					if (!A_1)
					{
						num = 7;
						continue;
					}
					goto IL_170;
				case 9:
					if (true)
					{
					}
					if (!A_1)
					{
						num = 4;
						continue;
					}
					goto IL_133;
				case 10:
					switch (A_0)
					{
					case MailMergeMainDocumentType.FormLetters:
						num = 8;
						continue;
					case MailMergeMainDocumentType.MailingLabels:
						num = 9;
						continue;
					case (MailMergeMainDocumentType)3:
						goto IL_1FA;
					case MailMergeMainDocumentType.Envelopes:
						goto IL_7B;
					default:
						num = 6;
						continue;
					}
					break;
				case 11:
					num = 3;
					continue;
				case 12:
					num = 10;
					continue;
				case 13:
					if (A_0 != MailMergeMainDocumentType.Email)
					{
						num = 2;
						continue;
					}
					goto IL_142;
				case 14:
					num = 15;
					continue;
				case 15:
					goto IL_18A;
				}
				break;
				IL_1CA:
				if (A_0 == MailMergeMainDocumentType.Fax)
				{
					goto IL_1EB;
				}
				num = 14;
			}
		}
		IL_7B:
		return ClipboardData.b("፵ᙷ౹᥻ች", a_);
		IL_EE:
		if (false)
		{
		}
		return ClipboardData.b("᭵᥷፹ၻ᝽ꦃ", a_);
		IL_12E:
		return ClipboardData.b("ၵ᝷ࡹᅻ卽ﾋ", a_);
		IL_133:
		return ClipboardData.b("᭵᥷፹ၻ᝽좃ﶍ", a_);
		IL_142:
		return ClipboardData.b("፵ᕷ᭹ᕻች", a_);
		IL_15C:
		goto IL_1FA;
		IL_170:
		return ClipboardData.b("ၵ᝷ࡹᅻ㉽慎黎", a_);
		IL_18A:
		goto IL_1FA;
		IL_1DC:
		return ClipboardData.b("ᕵ᥷๹ᵻች", a_);
		IL_1EB:
		return ClipboardData.b("ၵ᥷ɹ", a_);
		IL_1FA:
		throw new InvalidOperationException(ClipboardData.b("⍵ᙷᅹቻᅽꒃ꺍ﶏﶗ몙ﾝ즟첡蒣슥잧즩\ud9ab쎭햯\udcb1삳隵첷쎹첻\udbbd", a_));
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x00159BF0 File Offset: 0x00158BF0
	internal static MailMergeDestination ᜃ(string A_0)
	{
		int a_ = 15;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("ၴ᩶ᡸቺᅼ", a_)))
				{
					num = 3;
					continue;
				}
				return MailMergeDestination.Email;
			case 1:
				goto IL_76;
			case 3:
				num = 8;
				continue;
			case 4:
				goto IL_69;
			case 5:
				num = 7;
				continue;
			case 6:
				num = 12;
				continue;
			case 7:
				if (!(A_0 == ClipboardData.b("մնၸᕺॼ᩾", a_)))
				{
					num = 10;
					continue;
				}
				return MailMergeDestination.Printer;
			case 8:
				if (!(A_0 == ClipboardData.b("፴ᙶŸ", a_)))
				{
					num = 9;
					continue;
				}
				return MailMergeDestination.Fax;
			case 9:
				num = 4;
				continue;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_76;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 11:
				num = 1;
				continue;
			case 12:
				if (!(A_0 == ClipboardData.b("᭴ቶ๸㽺ቼ᱾ﶈ", a_)))
				{
					num = 11;
					continue;
				}
				return MailMergeDestination.NewDocument;
			}
			if (A_0 != null)
			{
				num = 6;
				continue;
			}
			return MailMergeDestination.NewDocument;
			IL_76:
			if (A_0 == ClipboardData.b("᭴ቶ๸噺᥼ၾﾊ", a_))
			{
				break;
			}
			num = 5;
		}
		return MailMergeDestination.NewDocument;
		IL_69:
		return MailMergeDestination.NewDocument;
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x00159D90 File Offset: 0x00158D90
	internal static string ᜀ(MailMergeDestination A_0, bool A_1)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_10B;
			case 1:
				if (!A_1)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_CC;
			case 2:
				switch (A_0)
				{
				case MailMergeDestination.NewDocument:
					num = 1;
					continue;
				case MailMergeDestination.Printer:
					goto IL_4C;
				case MailMergeDestination.Email:
					goto IL_B7;
				case (MailMergeDestination)3:
					goto IL_11D;
				case MailMergeDestination.Fax:
					goto IL_DB;
				default:
					num = 3;
					continue;
				}
				break;
			case 3:
				num = 4;
				continue;
			case 4:
				goto IL_11B;
			case 6:
				goto IL_4A;
			}
			if (A_0 == MailMergeDestination.NewDocument)
			{
				num = 6;
			}
			else
			{
				num = 2;
			}
		}
		IL_4A:
		return "";
		IL_4C:
		return ClipboardData.b("ݶ୸ቺ፼୾", a_);
		IL_B7:
		return ClipboardData.b("ቶᑸ᩺ᑼ፾", a_);
		IL_CC:
		return ClipboardData.b("᥶ᱸ౺㥼ၾﾊ", a_);
		IL_DB:
		return ClipboardData.b("ᅶᡸͺ", a_);
		IL_10B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_CC;
		default:
			if (false)
			{
			}
			return ClipboardData.b("᥶ᱸ౺偼᭾歷", a_);
		}
		IL_11B:
		IL_11D:
		return "";
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00159EC0 File Offset: 0x00158EC0
	internal static MailMergeDataType ᜂ(string A_0)
	{
		int a_ = 18;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
			{
				int num2;
				if (spr᧓.ᜦ.TryGetValue(A_0, out num2))
				{
					num = 7;
					continue;
				}
				return MailMergeDataType.None;
			}
			case 2:
				spr᧓.ᜦ = new Dictionary<string, int>(12)
				{
					{
						ClipboardData.b("ᱷ᭹ࡻώ", a_),
						0
					},
					{
						ClipboardData.b("㥷᥹ύ᭽", a_),
						1
					},
					{
						ClipboardData.b("ᙷ᭹ࡻ᝽", a_),
						2
					},
					{
						ClipboardData.b("㝷㹹⽻ㅽ", a_),
						3
					},
					{
						ClipboardData.b("᝷ṹṻᵽ", a_),
						4
					},
					{
						ClipboardData.b("㝷㹹㹻㵽", a_),
						5
					},
					{
						ClipboardData.b("ॷཹ᥻౽勵", a_),
						6
					},
					{
						ClipboardData.b("⥷⹹", a_),
						7
					},
					{
						ClipboardData.b("୷੹๻᭽", a_),
						8
					},
					{
						ClipboardData.b("㵷ɹύ᭽", a_),
						9
					},
					{
						ClipboardData.b("౷όѻ੽왿", a_),
						10
					},
					{
						ClipboardData.b("ṷ፹ၻ᭽", a_),
						11
					}
				};
				num = 8;
				continue;
			case 3:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_201;
				case 1:
					return MailMergeDataType.Database;
				case 2:
					return MailMergeDataType.Native;
				case 3:
					return MailMergeDataType.Native;
				case 4:
					return MailMergeDataType.Odbc;
				case 5:
					return MailMergeDataType.Odbc;
				case 6:
					return MailMergeDataType.Query;
				case 7:
					return MailMergeDataType.Query;
				case 8:
					return MailMergeDataType.Spreadsheet;
				case 9:
					goto IL_A8;
				case 10:
					return MailMergeDataType.TextFile;
				case 11:
					return MailMergeDataType.TextFile;
				default:
					num = 0;
					continue;
				}
				break;
			}
			case 5:
				goto IL_1FB;
			case 6:
				num = 9;
				continue;
			case 7:
				num = 3;
				continue;
			case 8:
				goto IL_232;
			case 9:
				if (spr᧓.ᜦ == null)
				{
					num = 2;
					continue;
				}
				goto IL_232;
			}
			if (A_0 != null)
			{
				num = 6;
				continue;
			}
			return MailMergeDataType.None;
			IL_232:
			num = 1;
		}
		return MailMergeDataType.Query;
		IL_A8:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return MailMergeDataType.Query;
		default:
			if (false)
			{
			}
			return MailMergeDataType.Spreadsheet;
		}
		return MailMergeDataType.TextFile;
		IL_1FB:
		return MailMergeDataType.None;
		IL_201:
		if (true)
		{
		}
		return MailMergeDataType.Database;
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x0015A134 File Offset: 0x00159134
	internal static string ᜀ(MailMergeDataType A_0, bool A_1)
	{
		int a_ = 4;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1C8;
				case 1:
					goto IL_152;
				case 2:
					if (!A_1)
					{
						num = 1;
						continue;
					}
					goto IL_17E;
				case 3:
					switch (A_0)
					{
					case MailMergeDataType.TextFile:
						num = 11;
						continue;
					case MailMergeDataType.Database:
						num = 10;
						continue;
					case MailMergeDataType.Spreadsheet:
						num = 14;
						continue;
					case MailMergeDataType.Query:
						num = 5;
						continue;
					case MailMergeDataType.Odbc:
						num = 12;
						continue;
					case MailMergeDataType.Native:
						num = 2;
						continue;
					}
					goto IL_75;
				case 4:
					goto IL_16D;
				case 5:
					if (!A_1)
					{
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						goto IL_106;
					}
					break;
				case 6:
					goto IL_98;
				case 7:
					goto IL_1F3;
				case 8:
					num = 13;
					continue;
				case 9:
					goto IL_B6;
				case 10:
					if (!A_1)
					{
						num = 0;
						continue;
					}
					goto IL_BB;
				case 11:
					if (!A_1)
					{
						num = 4;
						continue;
					}
					goto IL_16F;
				case 12:
					if (!A_1)
					{
						num = 7;
						continue;
					}
					goto IL_12A;
				case 13:
					goto IL_198;
				case 14:
					if (!A_1)
					{
						num = 6;
						continue;
					}
					goto IL_D2;
				}
				break;
				IL_75:
				num = 8;
			}
		}
		IL_98:
		return ClipboardData.b("⽩ᑫ൭ᕯṱ", a_);
		IL_B6:
		return ClipboardData.b("㭩㡫", a_);
		IL_BB:
		if (true)
		{
		}
		return ClipboardData.b("๩൫ᩭᅯၱᕳյᵷ", a_);
		IL_D2:
		return ClipboardData.b("ᥩᱫᱭᕯ፱ၳյၷό᥻੽", a_);
		IL_106:
		if (false)
		{
		}
		return ClipboardData.b("᭩ᥫ୭ɯୱ", a_);
		IL_12A:
		return ClipboardData.b("թ࡫౭፯", a_);
		IL_152:
		return ClipboardData.b("╩⡫㵭㽯", a_);
		IL_16D:
		return ClipboardData.b("౩իɭᕯ", a_);
		IL_16F:
		return ClipboardData.b("ṩ५᙭ѯ㑱ᵳ᩵ᵷ", a_);
		IL_17E:
		return ClipboardData.b("ѩ൫ᩭ᥯ѱᅳ", a_);
		IL_198:
		throw new InvalidOperationException(ClipboardData.b("㽩ɫխṯᵱͳᡵ塷᝹ᵻ᝽ꊁ慎꺍뢗튟송솣蚥\udca7펩\udcab쮭麯", a_));
		IL_1C8:
		return ClipboardData.b("⭩ཫ൭ᕯűݳ", a_);
		IL_1F3:
		return ClipboardData.b("╩⡫Ɑ㍯", a_);
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0015A378 File Offset: 0x00159378
	internal static OdsoDataSourceType ᜀ(string A_0, bool A_1)
	{
		int a_ = 0;
		int num = 8;
		for (;;)
		{
			int num3;
			switch (num)
			{
			case 0:
				goto IL_1B3;
			case 1:
				if (true)
				{
				}
				num = 4;
				continue;
			case 2:
				goto IL_1DF;
			case 3:
				spr᧓.ᜧ = new Dictionary<string, int>(9)
				{
					{
						ClipboardData.b("ݥ౧๩ṫ୭ͯű㙳᥵᝷ᅹ", a_),
						0
					},
					{
						ClipboardData.b("ɥ१ṩ൫౭ᅯűᅳ", a_),
						1
					},
					{
						ClipboardData.b("ɥݧ३ᥫͭᕯᱱs䝵", a_),
						2
					},
					{
						ClipboardData.b("ɥݧ३ᥫͭᕯᱱs䑵", a_),
						3
					},
					{
						ClipboardData.b("ͥէ୩իɭ", a_),
						4
					},
					{
						ClipboardData.b("੥൧൩൫൭९", a_),
						5
					},
					{
						ClipboardData.b("୥१ᥩᡫ୭ɯ", a_),
						6
					},
					{
						ClipboardData.b("ࡥ१ṩիᡭᕯ", a_),
						7
					},
					{
						ClipboardData.b("ብ൧ቩᡫ", a_),
						8
					}
				};
				num = 0;
				continue;
			case 4:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return OdsoDataSourceType.AddressBook;
				case 1:
					return OdsoDataSourceType.Database;
				case 2:
					return OdsoDataSourceType.Document1;
				case 3:
					return OdsoDataSourceType.Document2;
				case 4:
					return OdsoDataSourceType.Email;
				case 5:
					return OdsoDataSourceType.Legacy;
				case 6:
					return OdsoDataSourceType.Master;
				case 7:
					return OdsoDataSourceType.Native;
				case 8:
					return OdsoDataSourceType.Text;
				default:
					num = 6;
					continue;
				}
				break;
			}
			case 5:
			{
				int num2;
				if (spr᧓.ᜧ.TryGetValue(A_0, out num2))
				{
					num = 1;
					continue;
				}
				goto IL_1DF;
			}
			case 6:
				num = 2;
				continue;
			case 7:
				if (num3 != -2147483648)
				{
					num = 10;
					continue;
				}
				return OdsoDataSourceType.None;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return OdsoDataSourceType.Text;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 9:
				if (spr᧓.ᜧ == null)
				{
					num = 3;
					continue;
				}
				goto IL_1B3;
			case 10:
				return (OdsoDataSourceType)num3;
			case 11:
				num = 9;
				continue;
			}
			if (A_0 != null)
			{
				num = 11;
				continue;
			}
			goto IL_1DF;
			IL_1B3:
			num = 5;
			continue;
			IL_1DF:
			num3 = sprᜌ.ᜊ(A_0);
			num = 7;
		}
		return OdsoDataSourceType.Master;
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x0015A5C8 File Offset: 0x001595C8
	internal static string ᜀ(OdsoDataSourceType A_0, bool A_1)
	{
		int a_ = 16;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch (A_0)
				{
				case OdsoDataSourceType.Text:
					goto IL_138;
				case OdsoDataSourceType.Database:
					goto IL_16B;
				case OdsoDataSourceType.AddressBook:
					goto IL_156;
				case OdsoDataSourceType.Document1:
					goto IL_100;
				case OdsoDataSourceType.Document2:
					goto IL_58;
				case OdsoDataSourceType.Native:
					goto IL_B6;
				case OdsoDataSourceType.Email:
					goto IL_147;
				case OdsoDataSourceType.None:
					goto IL_F2;
				case OdsoDataSourceType.Legacy:
					goto IL_49;
				case OdsoDataSourceType.Master:
					goto IL_E3;
				default:
					num = 4;
					continue;
				}
				break;
			case 1:
				goto IL_11A;
			case 2:
				if (A_1)
				{
					num = 5;
					continue;
				}
				goto IL_17A;
			case 4:
				num = 1;
				continue;
			case 5:
				num = 0;
				continue;
			case 6:
				goto IL_44;
			}
			if (A_0 == OdsoDataSourceType.None)
			{
				num = 6;
			}
			else
			{
				num = 2;
			}
		}
		IL_44:
		return "";
		IL_49:
		return ClipboardData.b("᩵ᵷᵹᵻᵽ勵", a_);
		IL_58:
		return ClipboardData.b("ት᝷᥹ॻ፽뒅", a_);
		IL_B6:
		return ClipboardData.b("ᡵ᥷๹ᕻࡽ", a_);
		IL_E3:
		return ClipboardData.b("᭵᥷ॹࡻ᭽", a_);
		IL_F2:
		if (true)
		{
		}
		return "";
		IL_100:
		return ClipboardData.b("ት᝷᥹ॻ፽랅", a_);
		IL_11A:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_100;
		default:
			if (false)
			{
			}
			goto IL_F2;
		}
		IL_138:
		return ClipboardData.b("ɵᵷɹࡻ", a_);
		IL_147:
		return ClipboardData.b("፵ᕷ᭹ᕻች", a_);
		IL_156:
		return ClipboardData.b("᝵ᱷṹ๻᭽욃", a_);
		IL_16B:
		return ClipboardData.b("ት᥷๹ᵻᱽ", a_);
		IL_17A:
		int num2 = (int)A_0;
		return num2.ToString();
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x0015A758 File Offset: 0x00159758
	internal static OdsoFieldMappingType ᜁ(string A_0)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 2:
				goto IL_84;
			case 3:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return OdsoFieldMappingType.Column;
				case 1:
					return OdsoFieldMappingType.Column;
				case 2:
					return OdsoFieldMappingType.Null;
				case 3:
				case 4:
				case 5:
				case 6:
					return OdsoFieldMappingType.Null;
				default:
					num = 0;
					continue;
				}
				break;
			}
			case 4:
				num = 3;
				continue;
			case 5:
				goto IL_89;
			case 6:
				if (spr᧓.ᜨ == null)
				{
					num = 5;
					continue;
				}
				goto IL_1A7;
			case 7:
			{
				int num2;
				if (spr᧓.ᜨ.TryGetValue(A_0, out num2))
				{
					num = 4;
					continue;
				}
				return OdsoFieldMappingType.Null;
			}
			case 8:
				num = 6;
				continue;
			case 9:
				goto IL_1A7;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_89:
				spr᧓.ᜨ = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("६൮㉰ᱲᥴɶᑸᕺ", a_),
						0
					},
					{
						ClipboardData.b("६൮屰ၲᩴ᭶౸ᙺ፼", a_),
						1
					},
					{
						ClipboardData.b("ͬᩮᵰὲ", a_),
						2
					},
					{
						ClipboardData.b("౬୮ᕰŲၴѶ੸噺ὼ፾", a_),
						3
					},
					{
						ClipboardData.b("Ṭ๮ᵰٲŴᙶ൸ቺቼᅾ", a_),
						4
					},
					{
						ClipboardData.b("l๮ŰͲၴ፶", a_),
						5
					},
					{
						ClipboardData.b("ཬ๮Ͱၲᩴ፶ᱸ", a_),
						6
					}
				};
				num = 9;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 != null)
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				return OdsoFieldMappingType.Null;
			}
			IL_1A7:
			num = 7;
		}
		return OdsoFieldMappingType.Column;
		IL_84:
		return OdsoFieldMappingType.Null;
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x0015A938 File Offset: 0x00159938
	internal static string ᜀ(OdsoFieldMappingType A_0, bool A_1)
	{
		int a_ = 14;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch (A_0)
				{
				case OdsoFieldMappingType.Column:
					num = 6;
					continue;
				case OdsoFieldMappingType.Null:
					goto IL_B5;
				default:
					num = 3;
					continue;
				}
				break;
			case 1:
				goto IL_8B;
			case 2:
				goto IL_EC;
			case 3:
				num = 2;
				continue;
			case 4:
				goto IL_41;
			case 6:
				if (!A_1)
				{
					num = 1;
					continue;
				}
				goto IL_D2;
			}
			if (A_0 == OdsoFieldMappingType.Null)
			{
				num = 4;
			}
			else
			{
				num = 0;
			}
		}
		IL_41:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5F:
			return ClipboardData.b("ၳᑵ啷᥹፻ች", a_);
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			return "";
		}
		IL_8B:
		goto IL_5F;
		IL_B5:
		return ClipboardData.b("ᩳ͵ᑷᙹ", a_);
		IL_D2:
		return ClipboardData.b("ၳᑵ㭷ᕹၻ୽", a_);
		IL_EC:
		return "";
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x0015AA38 File Offset: 0x00159A38
	internal static PredefinedMergeFieldName ᜀ(string A_0)
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
		return (PredefinedMergeFieldName)spr\u19FA.ᜀ(spr\u17DB.ᜀ, A_0, PredefinedMergeFieldName.Invalid);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x0015AA8C File Offset: 0x00159A8C
	internal static string ᜀ(PredefinedMergeFieldName A_0)
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
		return (string)spr\u19FA.ᜀ(spr\u17DB.ᜁ, A_0, "");
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0015AAE4 File Offset: 0x00159AE4
	static spr\u17DB()
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
		spr\u17DB.ᜀ = new Hashtable();
		spr\u17DB.ᜁ = new Hashtable();
		spr\u19FA.ᜁ(new object[]
		{
			ClipboardData.b("㩮ὰᩲѴɶᱸ孺㑼᭾ﶎ", a_),
			PredefinedMergeFieldName.UniqueIdentifier,
			ClipboardData.b("ⱮṰٲݴͶᱸࡺѼ彾햀", a_),
			PredefinedMergeFieldName.CourtesyTitle,
			ClipboardData.b("⥮ᡰŲٴͶ奸㕺ᱼቾ", a_),
			PredefinedMergeFieldName.FirstName,
			ClipboardData.b("≮ᡰᝲᅴ᭶ᱸ孺㍼Ṿ", a_),
			PredefinedMergeFieldName.MiddleName,
			ClipboardData.b("⍮ၰrŴ坶㝸᩺ၼ᩾", a_),
			PredefinedMergeFieldName.LastName,
			ClipboardData.b("㱮Ѱᕲ፴ṶŸ", a_),
			PredefinedMergeFieldName.Suffix,
			ClipboardData.b("ⅮᡰၲṴ᥶ᡸᙺ᡼", a_),
			PredefinedMergeFieldName.Nickname,
			ClipboardData.b("╮Ṱᅲ啴⍶ၸེᅼ᩾", a_),
			PredefinedMergeFieldName.JobTitle,
			ClipboardData.b("ⱮṰṲմᙶ᝸ɺ", a_),
			PredefinedMergeFieldName.Company,
			ClipboardData.b("⹮ᕰᝲݴቶ੸ࡺ嵼乾", a_),
			PredefinedMergeFieldName.Address1,
			ClipboardData.b("⹮ᕰᝲݴቶ੸ࡺ嵼䵾", a_),
			PredefinedMergeFieldName.Address2,
			ClipboardData.b("Ɱᡰݲ౴", a_),
			PredefinedMergeFieldName.City,
			ClipboardData.b("㱮հቲŴቶ", a_),
			PredefinedMergeFieldName.State,
			ClipboardData.b("㽮ṰrŴᙶᕸ孺㹼ၾ", a_),
			PredefinedMergeFieldName.PostalCode,
			ClipboardData.b("ⱮṰٲ᭴Ͷ୸ɺ嵼ၾꎂ힄", a_),
			PredefinedMergeFieldName.CountryOrRegion,
			ClipboardData.b("⵮Ѱrᱴ᥶ᱸࡺ๼彾톀", a_),
			PredefinedMergeFieldName.BusinessPhone,
			ClipboardData.b("⵮Ѱrᱴ᥶ᱸࡺ๼彾잀ﶄ", a_),
			PredefinedMergeFieldName.BusinessFax,
			ClipboardData.b("❮ṰṲၴ坶⥸፺ቼᅾ", a_),
			PredefinedMergeFieldName.HomePhone,
			ClipboardData.b("❮ṰṲၴ坶㽸᩺ռ", a_),
			PredefinedMergeFieldName.HomeFax,
			ClipboardData.b("⩮屰ṲᑴṶᕸ孺㱼᭾愈", a_),
			PredefinedMergeFieldName.EmailAddress,
			ClipboardData.b("㡮ᑰᅲ啴❶ᡸᱺ᡼", a_),
			PredefinedMergeFieldName.WebPage,
			ClipboardData.b("㱮ŰᱲtѶᱸ孺㹼ၾ愈권\udb8e璉", a_),
			PredefinedMergeFieldName.SpouseCourtesyTitle,
			ClipboardData.b("㱮ŰᱲtѶᱸ孺㭼ᙾꞆ있", a_),
			PredefinedMergeFieldName.SpouseFirstName,
			ClipboardData.b("㱮ŰᱲtѶᱸ孺ぼᙾꦈ얊", a_),
			PredefinedMergeFieldName.SpouseMiddleName,
			ClipboardData.b("㱮ŰᱲtѶᱸ孺ㅼṾꖄ즆", a_),
			PredefinedMergeFieldName.SpouseLastName,
			ClipboardData.b("㱮ŰᱲtѶᱸ孺㍼ᙾ", a_),
			PredefinedMergeFieldName.SpouseNickname,
			ClipboardData.b("㽮ᥰᱲ᭴ቶ൸ቺṼ彾욀ꮊ뎒펔ﺖ뾞슢좤슦", a_),
			PredefinedMergeFieldName.PhoneticGuideForFirstName,
			ClipboardData.b("㽮ᥰᱲ᭴ቶ൸ቺṼ彾욀ꮊ뎒\ud994붜톞삠캢삤", a_),
			PredefinedMergeFieldName.PhoneticGuideForLastName,
			ClipboardData.b("⹮ᕰᝲݴቶ੸ࡺ嵼䱾", a_),
			PredefinedMergeFieldName.Address3,
			ClipboardData.b("⭮ᑰͲᑴն൸ᙺ᡼ᅾ", a_),
			PredefinedMergeFieldName.Department
		}, spr\u17DB.ᜀ, spr\u17DB.ᜁ);
	}

	// Token: 0x040019B2 RID: 6578
	private static readonly Hashtable ᜀ;

	// Token: 0x040019B3 RID: 6579
	private static readonly Hashtable ᜁ;
}
