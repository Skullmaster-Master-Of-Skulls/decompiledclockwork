using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Spire.DataExport.Access;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Delegates;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.Utils;
using Spire.DataExport.XLS;

// Token: 0x0200012A RID: 298
internal abstract class spr\u2059
{
	// Token: 0x06000715 RID: 1813
	[DllImport("user32")]
	public static extern int FindWindow(string A_0, string A_1);

	// Token: 0x06000716 RID: 1814 RVA: 0x00044AC4 File Offset: 0x00043AC4
	public static ColExportType ᜀ(Type A_0, bool A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 == typeof(TimeSpan))
				{
					num = 25;
					continue;
				}
				num = 43;
				continue;
			case 1:
				return ColExportType.Unknown;
			case 2:
				if (A_0 == typeof(double))
				{
					num = 7;
					continue;
				}
				num = 15;
				continue;
			case 3:
				goto IL_12A;
			case 5:
				if (A_0 != typeof(short))
				{
					num = 35;
					continue;
				}
				return ColExportType.Integer;
			case 6:
				num = 8;
				continue;
			case 7:
				goto IL_FF;
			case 8:
				if (A_0 != typeof(int))
				{
					num = 38;
					continue;
				}
				return ColExportType.Integer;
			case 9:
				return ColExportType.Boolean;
			case 10:
				goto IL_37C;
			case 11:
				if (A_0 == typeof(char))
				{
					num = 18;
					continue;
				}
				num = 39;
				continue;
			case 12:
				num = 2;
				continue;
			case 13:
				if (A_0 == typeof(byte[]))
				{
					num = 40;
					continue;
				}
				num = 24;
				continue;
			case 14:
				return ColExportType.Guid;
			case 15:
				if (A_0 == typeof(decimal))
				{
					num = 26;
					continue;
				}
				num = 27;
				continue;
			case 16:
				num = 5;
				continue;
			case 17:
				if (!A_1)
				{
					goto IL_2C9;
				}
				return ColExportType.String;
			case 18:
				goto IL_155;
			case 19:
				return ColExportType.DateTime;
			case 20:
				if (A_0 != typeof(ushort))
				{
					num = 21;
					continue;
				}
				return ColExportType.Integer;
			case 21:
				num = 28;
				continue;
			case 22:
				if (true)
				{
				}
				if (A_0 != typeof(float))
				{
					num = 12;
					continue;
				}
				return ColExportType.Float;
			case 23:
				num = 31;
				continue;
			case 24:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C9;
				}
				if (false)
				{
				}
				if (A_0 == typeof(string))
				{
					num = 42;
					continue;
				}
				goto IL_290;
			case 25:
				return ColExportType.Time;
			case 26:
				return ColExportType.Currency;
			case 27:
				if (A_0 == typeof(DateTime))
				{
					num = 19;
					continue;
				}
				num = 0;
				continue;
			case 28:
				if (A_0 == typeof(uint))
				{
					num = 10;
					continue;
				}
				num = 29;
				continue;
			case 29:
				if (A_0 != typeof(long))
				{
					num = 32;
					continue;
				}
				return ColExportType.Bigint;
			case 30:
				if (A_0 == typeof(ulong))
				{
					num = 3;
					continue;
				}
				num = 34;
				continue;
			case 31:
				if (A_0 != typeof(sbyte))
				{
					num = 16;
					continue;
				}
				return ColExportType.Integer;
			case 32:
				num = 30;
				continue;
			case 33:
				goto IL_290;
			case 34:
				if (A_0 == typeof(bool))
				{
					num = 9;
					continue;
				}
				num = 22;
				continue;
			case 35:
				num = 36;
				continue;
			case 36:
				if (A_0 != typeof(int))
				{
					num = 6;
					continue;
				}
				return ColExportType.Integer;
			case 37:
				if (A_0 != typeof(string))
				{
					num = 41;
					continue;
				}
				return ColExportType.String;
			case 38:
				num = 20;
				continue;
			case 39:
				if (A_0 != typeof(byte))
				{
					num = 23;
					continue;
				}
				return ColExportType.Integer;
			case 40:
				return ColExportType.Binary;
			case 41:
				num = 11;
				continue;
			case 42:
				num = 17;
				continue;
			case 43:
				if (A_0 == typeof(Guid))
				{
					num = 14;
					continue;
				}
				return ColExportType.Unknown;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 13;
			continue;
			IL_290:
			num = 37;
			continue;
			IL_2C9:
			num = 33;
		}
		return ColExportType.Unknown;
		IL_FF:
		return ColExportType.Float;
		IL_12A:
		return ColExportType.Bigint;
		IL_155:
		return ColExportType.String;
		IL_37C:
		return ColExportType.Integer;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00044F50 File Offset: 0x00043F50
	public static string ᜀ(Type A_0, bool A_1, int A_2)
	{
		int a_ = 12;
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_222;
			case 1:
				num = 31;
				continue;
			case 2:
				goto IL_396;
			case 3:
				num = 38;
				continue;
			case 4:
				if (A_0 == typeof(decimal))
				{
					num = 5;
					continue;
				}
				num = 22;
				continue;
			case 5:
				goto IL_162;
			case 6:
				if (A_1)
				{
					num = 29;
					continue;
				}
				num = 30;
				continue;
			case 7:
				num = 24;
				continue;
			case 8:
				if (A_0 == typeof(bool))
				{
					num = 25;
					continue;
				}
				num = 15;
				continue;
			case 9:
				num = 4;
				continue;
			case 10:
				num = 17;
				continue;
			case 11:
				if (A_0 == typeof(byte[]))
				{
					num = 39;
					continue;
				}
				num = 13;
				continue;
			case 12:
				num = 18;
				continue;
			case 13:
				if (A_0 == typeof(string))
				{
					num = 16;
					continue;
				}
				goto IL_26E;
			case 15:
				if (A_0 != typeof(float))
				{
					num = 12;
					continue;
				}
				goto IL_227;
			case 16:
				goto IL_2A0;
			case 17:
				if (A_0 != typeof(ushort))
				{
					num = 2;
					continue;
				}
				goto IL_2F5;
			case 18:
				if (A_0 != typeof(double))
				{
					num = 9;
					continue;
				}
				goto IL_227;
			case 19:
				if (A_0 != typeof(uint))
				{
					num = 32;
					continue;
				}
				goto IL_2F5;
			case 20:
				if (A_0 == typeof(char))
				{
					num = 21;
					continue;
				}
				num = 37;
				continue;
			case 21:
				goto IL_36B;
			case 22:
				if (A_0 != typeof(DateTime))
				{
					num = 36;
					continue;
				}
				goto IL_236;
			case 23:
				if (A_0 == typeof(TimeSpan))
				{
					num = 28;
					continue;
				}
				goto IL_4DC;
			case 24:
				if (A_0 != typeof(short))
				{
					num = 1;
					continue;
				}
				goto IL_2F5;
			case 25:
				goto IL_100;
			case 26:
				if (A_0 != typeof(ulong))
				{
					num = 40;
					continue;
				}
				goto IL_2F5;
			case 27:
				if (A_0 != typeof(string))
				{
					num = 34;
					continue;
				}
				goto IL_245;
			case 28:
				goto IL_4A2;
			case 29:
				num = 11;
				continue;
			case 30:
				if (A_0 == typeof(byte[]))
				{
					num = 0;
					continue;
				}
				num = 27;
				continue;
			case 31:
				if (A_0 != typeof(int))
				{
					num = 33;
					continue;
				}
				goto IL_2F5;
			case 32:
				num = 26;
				continue;
			case 33:
				if (true)
				{
				}
				num = 41;
				continue;
			case 34:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_396;
				default:
					if (false)
					{
					}
					num = 20;
					continue;
				}
				break;
			case 35:
				goto IL_D5;
			case 36:
				num = 23;
				continue;
			case 37:
				if (A_0 != typeof(byte))
				{
					num = 3;
					continue;
				}
				goto IL_2F5;
			case 38:
				if (A_0 != typeof(sbyte))
				{
					num = 7;
					continue;
				}
				goto IL_2F5;
			case 39:
				goto IL_3DB;
			case 40:
				num = 8;
				continue;
			case 41:
				if (A_0 != typeof(long))
				{
					num = 10;
					continue;
				}
				goto IL_2F5;
			}
			if (A_0 == null)
			{
				num = 35;
				continue;
			}
			num = 6;
			continue;
			IL_396:
			num = 19;
		}
		IL_D5:
		return HyperlinksCollectionEditor.b("紧搩末怭缯攱稳", a_);
		IL_100:
		goto IL_2F5;
		IL_162:
		goto IL_227;
		IL_222:
		return string.Format(HyperlinksCollectionEditor.b("樧挩戫漭戯欱ᰳ䴵࠷䜹ᔻ", a_), A_2);
		IL_227:
		return HyperlinksCollectionEditor.b("氧攩礫氭簯眱ᐳ昵樷缹缻眽ጿୁୃࡅ", a_);
		IL_236:
		return HyperlinksCollectionEditor.b("氧欩砫欭", a_);
		IL_245:
		return string.Format(HyperlinksCollectionEditor.b("欧戩洫簭ᠯ䤱г䬵ᄷ", a_), A_2);
		IL_26E:
		return HyperlinksCollectionEditor.b("紧搩末怭缯攱稳", a_);
		IL_2A0:
		return HyperlinksCollectionEditor.b("樧昩挫洭", a_);
		IL_2F5:
		return HyperlinksCollectionEditor.b("愧搩砫欭眯眱昳", a_);
		IL_36B:
		goto IL_245;
		IL_3DB:
		return HyperlinksCollectionEditor.b("樧昩挫氭", a_);
		IL_4A2:
		goto IL_236;
		IL_4DC:
		return HyperlinksCollectionEditor.b("紧搩末怭缯攱稳", a_);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00045448 File Offset: 0x00044448
	public static string ᜀ(ColumnExport A_0)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_42;
			case 1:
			{
				ColExportType colExportType;
				switch (colExportType)
				{
				case ColExportType.Integer:
				case ColExportType.Bigint:
				case ColExportType.Boolean:
					goto IL_56;
				case ColExportType.Float:
				case ColExportType.Currency:
					goto IL_47;
				case ColExportType.DateTime:
				case ColExportType.Time:
					goto IL_F9;
				case ColExportType.String:
					goto IL_C3;
				default:
					num = 4;
					continue;
				}
				break;
			}
			case 3:
				goto IL_113;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_47;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				ColExportType colExportType = A_0.ColExportType;
				num = 1;
			}
		}
		IL_42:
		if (true)
		{
		}
		return HyperlinksCollectionEditor.b("瀤椦戨攪戬砮缰", a_);
		IL_47:
		return HyperlinksCollectionEditor.b("愤栦簨椪愬樮ᄰ挲朴父稸爺渼氾ࡀూୄ", a_);
		IL_56:
		return HyperlinksCollectionEditor.b("氤椦紨渪樬樮挰", a_);
		IL_C3:
		return string.Format(HyperlinksCollectionEditor.b("昤漦栨礪Ԭ吮İ串ᰴ", a_), A_0.Length);
		IL_F9:
		return HyperlinksCollectionEditor.b("愤昦紨渪", a_);
		IL_113:
		return HyperlinksCollectionEditor.b("瀤椦戨攪戬砮缰", a_);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x00045578 File Offset: 0x00044578
	public static ColExportType ᜁ(string A_0, string A_1, string A_2)
	{
		int num = 3;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				if (string.Compare(A_0, A_2, true) == 0)
				{
					num = 5;
					continue;
				}
				try
				{
					Convert.ToInt32(A_0);
					return ColExportType.Integer;
				}
				catch
				{
					goto IL_C6;
				}
				goto IL_92;
			case 1:
				return ColExportType.String;
			case 2:
				if (string.Compare(A_0, A_1, true) != 0)
				{
					num = 4;
					continue;
				}
				return ColExportType.Boolean;
			case 4:
				goto IL_92;
			case 5:
				goto IL_B2;
			}
			while (A_0.Length != 0)
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
					num = 2;
					goto IL_0A;
				}
			}
			num = 1;
			continue;
			IL_92:
			num = 0;
		}
		return ColExportType.String;
		IL_B2:
		if (true)
		{
		}
		return ColExportType.Boolean;
		try
		{
			IL_C6:
			Convert.ToDouble(A_0);
			return ColExportType.Float;
		}
		catch
		{
			goto IL_74;
		}
		return ColExportType.Boolean;
		try
		{
			IL_74:
			Convert.ToDateTime(A_0);
			return ColExportType.DateTime;
		}
		catch
		{
		}
		return ColExportType.String;
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00045684 File Offset: 0x00044684
	public static string ᜀ(string A_0, string A_1, IFormatProvider A_2, ColExportType A_3, NormalFunc A_4)
	{
		int a_ = 16;
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
					goto IL_49;
				case 2:
					goto IL_28F;
				case 3:
					if (A_0.Length == 0)
					{
						num = 2;
						continue;
					}
					goto IL_4E;
				}
				if (A_4 == null)
				{
					num = 1;
				}
				else
				{
					num = 3;
				}
			}
			IL_49:
			if (true)
			{
			}
			throw new NullReferenceException(HyperlinksCollectionEditor.b("Å␭甯䨱䐳夵䨷丹椻䨽⤿⹁㝃籅片౉⍋㱍㵏㍑⁓ቕ㥗⹙㵛牝ᙟ͡ᙣ履♧թṫͭᅯṱ㉳͵ᙷ᥹", a_));
			try
			{
				IL_4E:
				string result;
				for (;;)
				{
					num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_234;
						case 1:
							goto IL_124;
						case 2:
							goto IL_1E0;
						case 3:
							goto IL_153;
						case 4:
							goto IL_1B1;
						case 5:
							goto IL_ED;
						case 6:
							result = A_0;
							num = 0;
							continue;
						case 7:
							goto IL_220;
						case 8:
							switch (A_3)
							{
							case ColExportType.Integer:
								result = A_4(int.Parse(A_0, NumberStyles.Any).ToString(A_1, A_2));
								break;
							case ColExportType.Bigint:
								result = A_4(long.Parse(A_0, NumberStyles.Any).ToString(A_1, A_2));
								num = 7;
								continue;
							case ColExportType.Float:
								result = A_4(double.Parse(A_0, NumberStyles.Any).ToString(A_1, A_2));
								num = 3;
								continue;
							case ColExportType.Currency:
								result = A_4(decimal.Parse(A_0, NumberStyles.Any).ToString(A_1, A_2));
								num = 2;
								continue;
							case ColExportType.DateTime:
								result = A_4(DateTime.Parse(A_0, CultureInfo.CurrentCulture, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault).ToString(A_1, A_2));
								num = 5;
								continue;
							case ColExportType.Time:
								result = A_4(TimeSpan.Parse(A_0).ToString());
								num = 4;
								continue;
							case ColExportType.String:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									result = A_4(A_0);
									num = 1;
									continue;
								}
								break;
							default:
								num = 10;
								continue;
							}
							num = 9;
							continue;
						case 9:
							goto IL_182;
						case 10:
							num = 6;
							continue;
						}
						break;
					}
				}
				IL_ED:
				IL_124:
				IL_153:
				IL_182:
				IL_1B1:
				IL_1E0:
				IL_220:
				IL_234:
				return result;
			}
			catch
			{
				return string.Empty;
			}
			IL_240:
			return string.Empty;
			IL_28F:
			goto IL_240;
		}
		}
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00045940 File Offset: 0x00044940
	public static string ᜀ(ExportSource A_0)
	{
		int a_ = 8;
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
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						goto IL_80;
					case ExportSource.DataTable:
						goto IL_71;
					case ExportSource.ListView:
						goto IL_9C;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9A;
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
				case 1:
					goto IL_9A;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_71:
		return HyperlinksCollectionEditor.b("怣䜥尧䬩砫伭刯帱儳", a_);
		IL_80:
		return HyperlinksCollectionEditor.b("朣䤥䔧䜩䴫䀭启", a_);
		IL_9A:
		return string.Empty;
		IL_9C:
		return HyperlinksCollectionEditor.b("栣伥嬧帩稫䜭唯䔱", a_);
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00045A00 File Offset: 0x00044A00
	public static IComponent ᜂ(ExportSource A_0, IDbCommand A_1, DataTable A_2, ListView A_3)
	{
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
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						goto IL_6A;
					case ExportSource.DataTable:
						return A_2;
					case ExportSource.ListView:
						return A_3;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
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
				case 1:
					goto IL_79;
				case 2:
					num = 1;
					continue;
				}
				break;
			}
		}
		return A_2;
		IL_6A:
		return (IComponent)A_1;
		IL_79:
		return null;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00045A8C File Offset: 0x00044A8C
	public static void ᜁ(ExportSource A_0, IDbCommand A_1, DataTable A_2, ListView A_3)
	{
		int a_ = 5;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 == ExportSource.SqlCommand)
				{
					num = 1;
					continue;
				}
				goto IL_1CE;
			case 1:
				num = 19;
				continue;
			case 2:
				goto IL_215;
			case 3:
				num = 8;
				continue;
			case 4:
				if (A_0 == ExportSource.SqlCommand)
				{
					num = 6;
					continue;
				}
				return;
			case 5:
				if (A_0 == ExportSource.ListView)
				{
					num = 3;
					continue;
				}
				goto IL_9A;
			case 6:
				num = 15;
				continue;
			case 7:
				goto IL_104;
			case 8:
				if (true)
				{
				}
				if (A_3 != null)
				{
					goto IL_9A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_77;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			case 10:
				if (A_0 == ExportSource.DataTable)
				{
					num = 17;
					continue;
				}
				goto IL_215;
			case 11:
				goto IL_1AE;
			case 12:
				goto IL_77;
			case 13:
				goto IL_14B;
			case 14:
				if (A_2 != null)
				{
					num = 2;
					continue;
				}
				goto IL_1EC;
			case 15:
				if (A_1.Connection == null)
				{
					num = 7;
					continue;
				}
				return;
			case 16:
				if (A_1 != null)
				{
					num = 18;
					continue;
				}
				goto IL_1EC;
			case 17:
				num = 14;
				continue;
			case 18:
				goto IL_106;
			case 19:
				if (A_1.CommandText.Length == 0)
				{
					num = 13;
					continue;
				}
				goto IL_1CE;
			}
			if (A_0 == ExportSource.SqlCommand)
			{
				num = 12;
				continue;
			}
			goto IL_106;
			IL_77:
			num = 16;
			continue;
			IL_9A:
			num = 0;
			continue;
			IL_106:
			num = 10;
			continue;
			IL_1CE:
			num = 4;
			continue;
			IL_215:
			num = 5;
		}
		IL_104:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("怠儢䈤否瘨株䈬䈮尰刲嬴匶稸吺匼儾⑀⁂ㅄ⹆♈╊", a_)));
		IL_14B:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("怠儢䈤否瘨株䈬䈮尰刲嬴匶洸帺䔼䬾р⹂㕄㍆え", a_)));
		IL_1AE:
		IL_1EC:
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("怠儢䈤否瘨漪䰬嬮倰怲娴䈶䬸堺堼稾ⱀ㍂ㅄ㹆", a_)), spr\u2059.ᜀ(A_0)));
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00045CD0 File Offset: 0x00044CD0
	public static void ᜀ(ExportSource A_0, IDbCommand A_1, DataTable A_2, ListView A_3)
	{
		int a_ = 13;
		for (;;)
		{
			spr\u2059.ᜁ(A_0, A_1, A_2, A_3);
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AF;
				case 1:
					if (A_0 != ExportSource.SqlCommand)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					if (A_1.Connection.State != ConnectionState.Open)
					{
						goto IL_A4;
					}
					return;
				}
				break;
				IL_A4:
				num = 0;
			}
		}
		IL_AF:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("怨䔪嬬丮崰娲儴砶䤸帺似帾㕀⩂⩄⥆ᙈࡊ≌ⅎ㽐㙒㙔⍖じ㑚㍜ᱞൠౢᙤɦ൨", a_)));
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00045D90 File Offset: 0x00044D90
	public static void ᜁ(ExportSource A_0, ListView A_1)
	{
		int a_ = 12;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 2:
				goto IL_65;
			case 3:
				if (true)
				{
				}
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				A_1.BeginUpdate();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_A3;
			}
			goto IL_2D;
			IL_31:
			num = 0;
			continue;
			IL_2D:
			if (A_0 == ExportSource.ListView)
			{
				goto IL_31;
			}
			break;
		}
		IL_65:
		return;
		IL_A3:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029椫嘭䀯崱䘳䈵洷丹唻刽㌿硁繃Ʌⅇ㥉ⵋⱍ㱏㝑ᝓ㥕㙗⹙⹛ㅝ౟ᅡ䡣ၥ१ᡩ噫≭᥯űs⁵ᅷό୻", a_));
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00045E44 File Offset: 0x00044E44
	public static void ᜀ(ExportSource A_0, ListView A_1)
	{
		int a_ = 3;
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
				goto IL_A3;
			case 2:
				goto IL_6D;
			case 3:
				num = 4;
				continue;
			case 4:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				A_1.EndUpdate();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			goto IL_35;
			IL_39:
			num = 3;
			continue;
			IL_35:
			if (A_0 == ExportSource.ListView)
			{
				goto IL_39;
			}
			break;
		}
		IL_6D:
		return;
		IL_A3:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("ሞ⬠昢崤圦䘨太夬種䔰娲头䐶̸ĺ砼儾⁀⅂⥄≆ੈ⑊⍌㭎⍐㱒㥔⑖畘ⵚ㱜ⵞ孠⽢౤ᑦᵨ㵪Ѭ੮ٰ", a_));
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00045EF8 File Offset: 0x00044EF8
	public static bool ᜁ(ExportSource A_0, DataTable A_1, ListView A_2)
	{
		int a_ = 14;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						num = 3;
						continue;
					case ExportSource.DataTable:
						num = 2;
						continue;
					case ExportSource.ListView:
						num = 8;
						continue;
					default:
						num = 4;
						continue;
					}
					break;
				case 1:
					return true;
				case 2:
					if (A_1 != null)
					{
						goto IL_66;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_100;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 3:
					if (spr\u2059.ᜀ == null)
					{
						num = 7;
						continue;
					}
					num = 6;
					continue;
				case 4:
					num = 1;
					continue;
				case 5:
					goto IL_194;
				case 6:
					if (spr\u2059.ᜀ.IsClosed)
					{
						num = 5;
						continue;
					}
					goto IL_164;
				case 7:
					goto IL_100;
				case 8:
					if (A_2 == null)
					{
						num = 9;
						continue;
					}
					goto IL_116;
				case 9:
					goto IL_15F;
				case 10:
					goto IL_AF;
				}
				break;
			}
		}
		IL_66:
		return A_1.Rows.Count == 0;
		IL_AF:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅็⍉㹋㵍⑏繑≓㝕⩗恙ᡛ㽝ᑟ͡っݥ੧٩५", a_));
		IL_100:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅็⍉㹋㵍⑏繑≓㝕⩗恙ᡛ㽝ᑟ͡㙣ͥ१๩५ᱭ", a_));
		IL_116:
		return A_2.Items.Count == 0;
		IL_15F:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅็⍉㹋㵍⑏繑≓㝕⩗恙ၛ㝝፟ᙡ㉣ཥ൧ᵩ", a_));
		IL_164:
		return !spr\u2059.ᜀ.Read();
		IL_194:
		throw new ArgumentException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅็⍉㹋㵍⑏繑≓㝕⩗恙ᡛ㽝ᑟ͡㙣ͥ१๩५ᱭ", a_));
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0004609C File Offset: 0x0004509C
	public static void ᜀ(ExportSource A_0, DataTable A_1, ListView A_2)
	{
		int a_ = 13;
		for (;;)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 != null)
					{
						A_2.Items[0].Selected = true;
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E7;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 1:
					if (A_1.Rows.Count > 0)
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
					goto IL_174;
				case 4:
					goto IL_F2;
				case 5:
					goto IL_11B;
				case 6:
					goto IL_13D;
				case 7:
					if (spr\u2059.ᜀ.IsClosed)
					{
						num = 3;
						continue;
					}
					goto IL_142;
				case 8:
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						num = 10;
						continue;
					case ExportSource.DataTable:
						num = 11;
						continue;
					case ExportSource.ListView:
						num = 0;
						continue;
					default:
						num = 2;
						continue;
					}
					break;
				case 9:
					goto IL_9E;
				case 10:
					if (spr\u2059.ᜀ == null)
					{
						goto IL_E7;
					}
					num = 7;
					continue;
				case 11:
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					num = 1;
					continue;
				case 12:
					goto IL_1AB;
				}
				break;
				IL_E7:
				num = 4;
			}
		}
		return;
		IL_9E:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("␨K栬圮䄰尲䜴䌶永伺吼匾㉀祂罄ņ⁈㥊㹌㭎結╒㑔╖捘὚㱜⭞`㝢Ѥզը๪", a_));
		IL_F2:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("␨K栬圮䄰尲䜴䌶永伺吼匾㉀祂罄ņ⁈㥊㹌㭎結╒㑔╖捘὚㱜⭞`ㅢd٦൨๪Ὤ", a_));
		IL_11B:
		spr\u2059.ᜂ = A_1.Rows[0];
		return;
		IL_13D:
		return;
		IL_142:
		spr\u2059.ᜁ = spr\u2059.ᜀ.Read();
		return;
		IL_174:
		throw new ArgumentException(HyperlinksCollectionEditor.b("␨K栬圮䄰尲䜴䌶永伺吼匾㉀祂罄ņ⁈㥊㹌㭎結╒㑔╖捘὚㱜⭞`ㅢd٦൨๪Ὤ", a_));
		IL_1AB:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("␨K栬圮䄰尲䜴䌶永伺吼匾㉀祂罄ņ⁈㥊㹌㭎結╒㑔╖捘᝚㑜ⱞᕠ㕢౤ɦṨ", a_));
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00046284 File Offset: 0x00045284
	public static void ᜀ(ExportSource A_0, IDataReader A_1, DataTable A_2, int A_3, DataRowEventHandler A_4, object A_5, ref int A_6)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A9;
				case 1:
					if (num >= A_3)
					{
						num2 = 3;
						continue;
					}
					num2 = 2;
					continue;
				case 2:
					if (spr\u2059.ᜁ)
					{
						num2 = 7;
						continue;
					}
					goto IL_38;
				case 3:
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_A9;
					}
					break;
				case 5:
					A_4(A_5, A_6);
					num2 = 6;
					continue;
				case 6:
					goto IL_38;
				case 7:
					spr\u2059.ᜀ(A_0, A_1, A_2, ref A_6);
					num2 = 8;
					continue;
				case 8:
					if (A_4 != null)
					{
						num2 = 5;
						continue;
					}
					goto IL_38;
				}
				break;
				IL_3C:
				num2 = 4;
				continue;
				IL_38:
				num++;
				goto IL_3C;
				IL_A9:
				num2 = 1;
			}
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00046384 File Offset: 0x00045384
	public static void ᜀ(ExportSource A_0, IDataReader A_1, DataTable A_2, ref int A_3)
	{
		int a_ = 19;
		for (;;)
		{
			A_3++;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A4;
				case 1:
					if (A_1.IsClosed)
					{
						num = 8;
						continue;
					}
					goto IL_7C;
				case 2:
					if (A_3 < A_2.Rows.Count)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
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
						spr\u2059.ᜂ = A_2.Rows[A_3];
						break;
					}
					num = 5;
					continue;
				case 4:
					if (A_2 == null)
					{
						num = 7;
						continue;
					}
					num = 2;
					continue;
				case 5:
					goto IL_13D;
				case 6:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 7:
					goto IL_11B;
				case 8:
					goto IL_160;
				case 9:
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						num = 6;
						continue;
					case ExportSource.DataTable:
						num = 4;
						continue;
					default:
						num = 10;
						continue;
					}
					break;
				case 10:
					return;
				}
				break;
			}
		}
		return;
		IL_7C:
		spr\u2059.ᜁ = A_1.Read();
		return;
		IL_A4:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("∮㬰瘲䴴䜶嘸䤺䤼樾㕀⩂⥄㑆獈煊͌⩎⥐❒祔⅖㡘⥚杜᭞`ᝢѤ㕦౨੪६੮Ͱ", a_));
		IL_11B:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("∮㬰瘲䴴䜶嘸䤺䤼樾㕀⩂⥄㑆獈煊͌⩎⥐❒祔⅖㡘⥚杜᭞`ᝢѤ㍦ࡨ४Ŭ੮", a_));
		IL_13D:
		return;
		IL_160:
		throw new ArgumentException(HyperlinksCollectionEditor.b("∮㬰瘲䴴䜶嘸䤺䤼樾㕀⩂⥄㑆獈煊͌⩎⥐❒祔⅖㡘⥚杜᭞`ᝢѤ㕦౨੪६੮Ͱ", a_));
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0004651C File Offset: 0x0004551C
	public static bool ᜀ(ExportSource A_0, DataTable A_1, ListView A_2, int A_3, int A_4, int A_5)
	{
		int a_ = 12;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					num = 5;
					continue;
				case 2:
					goto IL_7E;
				case 3:
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						goto IL_BE;
					case ExportSource.DataTable:
						num = 7;
						continue;
					case ExportSource.ListView:
						num = 8;
						continue;
					default:
						num = 1;
						continue;
					}
					break;
				case 4:
					goto IL_14C;
				case 5:
					return false;
				case 6:
					if (A_4 > 0)
					{
						num = 2;
						continue;
					}
					goto IL_107;
				case 7:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					goto IL_14E;
				case 8:
					if (A_2 != null)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_7E:
		return A_3 == Math.Max(Math.Min(A_4, A_2.Items.Count - A_5), 0);
		IL_BC:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029椫嘭䀯崱䘳䈵洷丹唻刽㌿硁繃ͅ❇ⱉ恋㡍ㅏ⁑湓ᩕㅗ⥙⡛࡝य़ݡ፣", a_));
		IL_BE:
		return !spr\u2059.ᜁ;
		IL_107:
		return A_3 == Math.Max(A_2.Items.Count - A_5, 0);
		IL_14C:
		throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029椫嘭䀯崱䘳䈵洷丹唻刽㌿硁繃ͅ❇ⱉ恋㡍ㅏ⁑湓ቕ㥗⹙㵛੝şaࡣͥ", a_));
		IL_14E:
		return A_3 >= A_1.Rows.Count;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0004668C File Offset: 0x0004568C
	public static string ᜀ(IDataReader A_0, int A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				byte[] array = new byte[8192];
				long num = 0L;
				long num2 = 0L;
				stringBuilder = new StringBuilder(8192);
				stringBuilder.Append(HyperlinksCollectionEditor.b("⼞夠", a_));
				int num3 = 6;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_13E;
					case 1:
						goto IL_9A;
					case 2:
					{
						int num4;
						if ((long)num4 >= num)
						{
							num3 = 1;
							continue;
						}
						stringBuilder.Append(spr\u2059.ᜃ[array[num4] >> 4 & 15]);
						stringBuilder.Append(spr\u2059.ᜃ[(int)(array[num4] & 15)]);
						num4++;
						num3 = 5;
						continue;
					}
					case 3:
						if ((num = A_0.GetBytes(A_1, num2, array, 0, array.Length)) > 0L)
						{
							num2 += num;
							int num4 = 0;
							num3 = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9A;
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
					case 4:
						goto IL_82;
					case 5:
						goto IL_82;
					case 6:
						goto IL_EF;
					}
					break;
					IL_82:
					num3 = 2;
					continue;
					IL_EF:
					num3 = 3;
					continue;
					IL_9A:
					goto IL_EF;
				}
			}
			IL_13E:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x000467E0 File Offset: 0x000457E0
	public static string ᜀ(byte[] A_0)
	{
		int a_ = 16;
		StringBuilder stringBuilder;
		for (;;)
		{
			IL_21:
			stringBuilder = new StringBuilder(A_0.Length * 2 + 2);
			stringBuilder.Append(HyperlinksCollectionEditor.b("ᰫ嘭", a_));
			int num = 0;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_CF:
				goto IL_73;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				IL_0B:
				switch (num2)
				{
				case 0:
					goto IL_96;
				case 1:
					goto IL_CF;
				case 2:
					if (num >= A_0.Length)
					{
						num2 = 0;
						continue;
					}
					stringBuilder.Append(spr\u2059.ᜃ[A_0[num] >> 4 & 15]);
					stringBuilder.Append(spr\u2059.ᜃ[(int)(A_0[num] & 15)]);
					num++;
					num2 = 1;
					continue;
				case 3:
					goto IL_71;
				}
				goto IL_21;
			}
			IL_71:
			IL_73:
			num2 = 2;
			goto IL_0B;
		}
		IL_96:
		return stringBuilder.ToString();
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x000468C4 File Offset: 0x000458C4
	public static string ᜀ(ExportSource A_0, IDataReader A_1, ListView A_2, ColumnsExport A_3, CultureInfo A_4, NormalFunc A_5, int A_6, int A_7, int A_8, bool A_9)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 16;
			ColumnExport columnExport;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					goto IL_4B7;
				case 1:
				{
					object obj;
					text = spr\u2059.ᜀ(obj as byte[]);
					num = 48;
					continue;
				}
				case 2:
					if (!(A_3.Holder is AccessExport))
					{
						num = 5;
						continue;
					}
					goto IL_2FC;
				case 3:
					text = string.Empty;
					num = 25;
					continue;
				case 4:
				{
					ColExportType colExportType;
					switch (colExportType)
					{
					case ColExportType.DateTime:
						text = ((DateTime)spr\u2059.ᜂ[columnExport.Number]).ToString(HyperlinksCollectionEditor.b("匩唫圭䤯ἱ礳笵ᔷ帹堻ḽ࠿ੁ繃⭅╇灉㽋㵍", a_));
						num = 13;
						continue;
					case ColExportType.Time:
						text = spr\u2059.ᜂ[columnExport.Number].ToString();
						num = 38;
						continue;
					case ColExportType.String:
					case ColExportType.Boolean:
					case ColExportType.Guid:
						goto IL_839;
					case ColExportType.Binary:
					{
						object obj = spr\u2059.ᜂ[columnExport.Number];
						goto IL_5B6;
					}
					case ColExportType.Unknown:
						text = DBNull.Value.ToString();
						num = 14;
						continue;
					default:
						num = 7;
						continue;
					}
					break;
				}
				case 5:
					text = text.Replace(HyperlinksCollectionEditor.b("✩☫", a_), HyperlinksCollectionEditor.b("਩", a_)).TrimEnd(new char[]
					{
						' '
					});
					num = 24;
					continue;
				case 6:
					goto IL_769;
				case 7:
					num = 43;
					continue;
				case 8:
					goto IL_2FC;
				case 9:
					num = 22;
					continue;
				case 10:
					num = 47;
					continue;
				case 11:
				{
					object obj;
					if (obj is byte[])
					{
						num = 1;
						continue;
					}
					goto IL_7A9;
				}
				case 12:
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					num = 18;
					continue;
				case 13:
					goto IL_4B7;
				case 14:
					goto IL_4B7;
				case 15:
				{
					if (A_2 == null)
					{
						num = 49;
						continue;
					}
					int number = columnExport.Number;
					num = 27;
					continue;
				}
				case 17:
				{
					ColExportType colExportType2;
					switch (colExportType2)
					{
					case ColExportType.DateTime:
						text = A_1.GetDateTime(columnExport.Number).ToString(HyperlinksCollectionEditor.b("匩唫圭䤯ἱ礳笵ᔷ帹堻ḽ࠿ੁ繃⭅╇灉㽋㵍", a_));
						num = 32;
						continue;
					case ColExportType.Time:
						text = A_1.GetValue(columnExport.Number).ToString();
						num = 36;
						continue;
					case ColExportType.String:
					case ColExportType.Boolean:
					case ColExportType.Guid:
						goto IL_486;
					case ColExportType.Binary:
						text = spr\u2059.ᜀ(A_1, columnExport.Number);
						num = 41;
						continue;
					case ColExportType.Unknown:
						text = DBNull.Value.ToString();
						num = 40;
						continue;
					default:
						num = 10;
						continue;
					}
					break;
				}
				case 18:
					if (A_1.IsClosed)
					{
						num = 34;
						continue;
					}
					num = 30;
					continue;
				case 19:
					goto IL_4B7;
				case 20:
				{
					if (spr\u2059.ᜂ.IsNull(columnExport.Number))
					{
						num = 53;
						continue;
					}
					ColExportType colExportType = columnExport.ColExportType;
					num = 4;
					continue;
				}
				case 21:
					if (spr\u2059.ᜂ == null)
					{
						num = 23;
						continue;
					}
					num = 20;
					continue;
				case 22:
					if (!(A_3.Holder is WorkSheet))
					{
						num = 59;
						continue;
					}
					goto IL_2FC;
				case 23:
					goto IL_541;
				case 24:
					goto IL_2FC;
				case 25:
					goto IL_4B7;
				case 26:
					num = 3;
					continue;
				case 27:
				{
					int number;
					if (number == 0)
					{
						num = 39;
						continue;
					}
					text = A_2.Items[A_7 + A_8].SubItems[number].Text;
					num = 0;
					continue;
				}
				case 28:
					num = 54;
					continue;
				case 29:
					if (A_9)
					{
						num = 60;
						continue;
					}
					return text;
				case 30:
				{
					if (A_1.IsDBNull(columnExport.Number))
					{
						num = 33;
						continue;
					}
					ColExportType colExportType2 = columnExport.ColExportType;
					num = 17;
					continue;
				}
				case 31:
					return text;
				case 32:
					goto IL_4B7;
				case 33:
					text = DBNull.Value.ToString();
					num = 58;
					continue;
				case 34:
					goto IL_222;
				case 35:
				{
					object obj;
					if (obj == null)
					{
						num = 50;
						continue;
					}
					num = 11;
					continue;
				}
				case 36:
					goto IL_4B7;
				case 37:
					if (columnExport.ColExportType == ColExportType.String)
					{
						num = 42;
						continue;
					}
					goto IL_2FC;
				case 38:
					goto IL_4B7;
				case 39:
					text = A_2.Items[A_7 + A_8].Text;
					num = 44;
					continue;
				case 40:
					goto IL_4B7;
				case 41:
					goto IL_4B7;
				case 42:
					num = 51;
					continue;
				case 43:
					goto IL_839;
				case 44:
					goto IL_4B7;
				case 45:
					if (A_3.Holder != null)
					{
						num = 28;
						continue;
					}
					goto IL_2FC;
				case 46:
					text = spr\u2059.ᜀ(text, new char[]
					{
						'\r',
						'\n'
					});
					num = 8;
					continue;
				case 47:
					goto IL_486;
				case 48:
					goto IL_4B7;
				case 49:
					goto IL_245;
				case 50:
					text = DBNull.Value.ToString();
					num = 57;
					continue;
				case 51:
					if (!columnExport.NotTruncatable)
					{
						num = 46;
						continue;
					}
					num = 45;
					continue;
				case 52:
					goto IL_136;
				case 53:
					text = DBNull.Value.ToString();
					num = 55;
					continue;
				case 54:
					if (!(A_3.Holder is CellExport))
					{
						num = 9;
						continue;
					}
					goto IL_2FC;
				case 55:
					goto IL_4B7;
				case 56:
					switch (A_0)
					{
					case ExportSource.SqlCommand:
						num = 12;
						continue;
					case ExportSource.DataTable:
						num = 21;
						continue;
					case ExportSource.ListView:
						num = 15;
						continue;
					default:
						num = 26;
						continue;
					}
					break;
				case 57:
					goto IL_4B7;
				case 58:
					goto IL_4B7;
				case 59:
					num = 2;
					continue;
				case 60:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B6;
					default:
						if (false)
						{
						}
						text = spr\u2059.ᜀ(text, columnExport.Format, A_4, columnExport.ColExportType, A_3.NormalFunc);
						num = 31;
						continue;
					}
					break;
				case 61:
					goto IL_4B7;
				}
				if (A_3 == null)
				{
					num = 52;
					continue;
				}
				columnExport = A_3[A_6];
				text = string.Empty;
				num = 56;
				continue;
				IL_2FC:
				num = 29;
				continue;
				IL_486:
				text = A_1[columnExport.Number].ToString();
				num = 61;
				continue;
				IL_4B7:
				num = 37;
				continue;
				IL_5B6:
				num = 35;
				continue;
				IL_839:
				text = spr\u2059.ᜂ[columnExport.Number].ToString();
				if (true)
				{
				}
				num = 19;
			}
			IL_136:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅ཇ⽉㡋്㽏㹑ၓ㝕ⱗ㭙灛⡝şၡ幣╥ݧ٩ᥫͭṯű", a_));
			IL_222:
			throw new ArgumentException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅ཇ⽉㡋്㽏㹑ၓ㝕ⱗ㭙灛⡝şၡ幣≥१ṩ൫㱭ᕯ፱ၳ፵੷", a_));
			IL_245:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅ཇ⽉㡋്㽏㹑ၓ㝕ⱗ㭙灛⡝şၡ幣⩥ŧᥩᡫ㡭᥯᝱ͳ", a_));
			IL_541:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅ཇ⽉㡋്㽏㹑ၓ㝕ⱗ㭙灛⡝şၡ幣╥ᵧᡩṫ୭ṯٱ♳᥵ཷ", a_));
			IL_769:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("✩☫欭䠯䈱嬳䐵䰷漹䠻圽ⰿㅁ繃籅ཇ⽉㡋്㽏㹑ၓ㝕ⱗ㭙灛⡝şၡ幣≥१ṩ൫㱭ᕯ፱ၳ፵੷", a_));
			IL_7A9:
			throw new InvalidOperationException(string.Format(HyperlinksCollectionEditor.b("温䴫娭儯ሱ嬳倵ᠷ夹医刽㔿⽁⩃ᵅ㍇穉ㅋፍ灏㭑❓㡕罗⹙籛㱝ᥟᙡţ㵥㕧䑩", a_), columnExport.Number));
		}
		}
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0004713C File Offset: 0x0004613C
	public static string ᜀ(string A_0, char A_1)
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
		StringBuilder stringBuilder = new StringBuilder(A_0);
		stringBuilder.Replace(A_1.ToString(), A_1.ToString() + A_1.ToString());
		stringBuilder.Insert(0, A_1);
		stringBuilder.Append(A_1);
		return stringBuilder.ToString();
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x000471B8 File Offset: 0x000461B8
	public static int ᜀ(Control A_0, string A_1)
	{
		int num = 2;
		Graphics graphics;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (graphics != null)
				{
					goto IL_72;
				}
				goto IL_C5;
			case 1:
				goto IL_7A;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				return 0;
			case 4:
				if (A_1.Length == 0)
				{
					num = 5;
					continue;
				}
				graphics = Graphics.FromHwnd(A_0.Handle);
				num = 0;
				continue;
			case 5:
				return 0;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 4;
			continue;
			IL_72:
			num = 1;
		}
		return 0;
		IL_7A:
		if (true)
		{
		}
		SizeF sizeF = graphics.MeasureString(A_1, A_0.Font);
		graphics.Dispose();
		return (int)sizeF.Width;
		IL_C5:
		return A_1.Length * 8;
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00047294 File Offset: 0x00046294
	public static string ᜀ(string A_0, char[] A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (num2 > -1)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				return A_0;
			}
			case 1:
			{
				int num2;
				A_0 = A_0.Remove(num2, A_0.Length - num2);
				num = 2;
				continue;
			}
			case 2:
				return A_0;
			case 4:
				goto IL_34;
			}
			if (A_0.Length == 0)
			{
				num = 4;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
				{
					if (false)
					{
					}
					int num2 = A_0.IndexOfAny(A_1);
					num = 0;
					break;
				}
				}
			}
		}
		IL_34:
		return string.Empty;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00047348 File Offset: 0x00046348
	public static bool ᜀ(XMLFile A_0, string A_1)
	{
		Array array2;
		for (;;)
		{
			Array array = null;
			array2 = null;
			A_0.ReadSections(ref array);
			int num = 4;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (Array.BinarySearch(array, A_1) < 0)
					{
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					return false;
				case 2:
					A_0.ReadValues(A_1, ref array2);
					goto IL_5E;
				case 3:
					return false;
				case 4:
					if (array == null)
					{
						num = 1;
						continue;
					}
					Array.Sort(array);
					num = 0;
					continue;
				case 5:
					if (array2 == null)
					{
						num = 3;
						continue;
					}
					goto IL_75;
				}
				break;
				IL_5E:
				num = 5;
			}
		}
		return false;
		IL_75:
		return array2.Length > 0;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0004741C File Offset: 0x0004641C
	public static Font ᜀ()
	{
		int a_ = 6;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return new Font(HyperlinksCollectionEditor.b("挡嘣伥䤧䘩", a_), 10f, FontStyle.Regular, GraphicsUnit.World, 1);
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0004747C File Offset: 0x0004647C
	public static string ᜀ(string A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			string text;
			string str;
			StringBuilder stringBuilder;
			for (;;)
			{
				text = A_0;
				str = string.Empty;
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_153;
					case 1:
						goto IL_8E;
					case 2:
						if (num2 >= A_2)
						{
							num = 6;
							continue;
						}
						stringBuilder.Append('0');
						num2++;
						num = 7;
						continue;
					case 3:
					{
						str = Path.GetExtension(text);
						string directoryName = Path.GetDirectoryName(text);
						num = 10;
						continue;
					}
					case 4:
						goto IL_E3;
					case 5:
						goto IL_153;
					case 6:
						goto IL_100;
					case 7:
						goto IL_E3;
					case 8:
					{
						string directoryName;
						text = directoryName + '\\' + Path.GetFileNameWithoutExtension(text);
						num = 5;
						continue;
					}
					case 9:
						goto IL_172;
					case 10:
					{
						string directoryName;
						if (directoryName.Length > 0)
						{
							num = 8;
							continue;
						}
						text = Path.GetFileNameWithoutExtension(text);
						num = 0;
						continue;
					}
					case 11:
						if (A_2 < 0)
						{
							num = 9;
							continue;
						}
						goto IL_8E;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_172;
						default:
							if (false)
							{
							}
							if (Path.HasExtension(text))
							{
								num = 3;
								continue;
							}
							goto IL_153;
						}
						break;
					}
					break;
					IL_8E:
					stringBuilder = new StringBuilder(A_2);
					num2 = 0;
					num = 4;
					continue;
					IL_E3:
					num = 2;
					continue;
					IL_153:
					num = 11;
					continue;
					IL_172:
					A_2 = 0;
					num = 1;
				}
			}
			IL_100:
			return text + A_1.ToString(stringBuilder.ToString()) + str;
		}
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00047634 File Offset: 0x00046634
	public static int ᜀ(string A_0, Font A_1)
	{
		SizeF sizeF;
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
			sizeF = SizeF.Empty;
			Graphics graphics = Graphics.FromHwnd((IntPtr)0);
			try
			{
				sizeF = graphics.MeasureString(A_0, A_1);
			}
			finally
			{
				graphics.Dispose();
			}
			break;
		}
		}
		if (true)
		{
		}
		return (int)sizeF.Width;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x000476B0 File Offset: 0x000466B0
	public static string ᜀ(ColExportType A_0)
	{
		int a_ = 19;
		for (;;)
		{
			IL_39:
			if (true)
			{
			}
			int num = 1;
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
						goto IL_D4;
					case 1:
						switch (A_0)
						{
						case ColExportType.Integer:
						case ColExportType.Bigint:
							goto IL_AB;
						case ColExportType.Float:
							goto IL_103;
						case ColExportType.Currency:
							goto IL_F4;
						case ColExportType.DateTime:
							goto IL_9C;
						case ColExportType.Time:
							goto IL_D6;
						case ColExportType.String:
							goto IL_8D;
						case ColExportType.Boolean:
							goto IL_BA;
						case ColExportType.Guid:
							goto IL_112;
						case ColExportType.Binary:
							goto IL_E5;
						default:
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_C9;
					}
					goto IL_39;
				}
				IL_C9:
				num = 0;
			}
		}
		IL_8D:
		return HyperlinksCollectionEditor.b("簮䔰䄲尴夶常", a_);
		IL_9C:
		return HyperlinksCollectionEditor.b("欮倰䜲倴挶倸嘺堼", a_);
		IL_AB:
		return HyperlinksCollectionEditor.b("昮弰䜲倴倶尸䤺", a_);
		IL_BA:
		return HyperlinksCollectionEditor.b("洮帰尲头制堸唺", a_);
		IL_D4:
		goto IL_112;
		IL_D6:
		return HyperlinksCollectionEditor.b("笮堰帲倴", a_);
		IL_E5:
		return HyperlinksCollectionEditor.b("洮堰崲吴䔶䀸", a_);
		IL_F4:
		return HyperlinksCollectionEditor.b("氮䐰䄲䜴制圸堺䐼", a_);
		IL_103:
		return HyperlinksCollectionEditor.b("椮崰尲吴䌶", a_);
		IL_112:
		return HyperlinksCollectionEditor.b("種弰堲嬴堶丸唺", a_);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x000477E0 File Offset: 0x000467E0
	public static object ᜀ(string A_0, string A_1, DataTable A_2)
	{
		int a_ = 11;
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2D;
				}
				break;
			}
		}
		IL_2D:
		if (false)
		{
		}
		IEnumerator enumerator = A_2.Rows.GetEnumerator();
		object result;
		try
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_F6;
				case 1:
				{
					DataRow dataRow;
					result = dataRow[A_1];
					if (true)
					{
					}
					num = 0;
					continue;
				}
				case 2:
				{
					DataRow dataRow;
					if (string.Compare(dataRow[HyperlinksCollectionEditor.b("搦䘨䜪堬䈮弰紲吴娶尸", a_)].ToString(), A_0) == 0)
					{
						num = 1;
						continue;
					}
					break;
				}
				case 3:
					goto IL_104;
				case 4:
				{
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					DataRow dataRow = (DataRow)enumerator.Current;
					num = 2;
					continue;
				}
				case 6:
					num = 3;
					continue;
				}
				IL_7D:
				num = 4;
				continue;
				goto IL_7D;
			}
			IL_F6:
			return result;
			IL_104:
			goto IL_41;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (disposable != null)
						{
							num = 1;
							continue;
						}
						goto IL_14D;
					case 1:
						disposable.Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_14B;
					}
					break;
				}
			}
			IL_14B:
			IL_14D:;
		}
		return result;
		IL_41:
		return null;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0004794C File Offset: 0x0004694C
	public static void ᜀ(ExportSource A_0, IDbCommand A_1, DataTable A_2, ListView A_3, StringListCollection A_4, ListDictionary A_5, ListDictionary A_6)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2059.ᜁ(A_0, A_1, A_2, A_3);
				A_5.Clear();
				A_6.Clear();
				int num = 2;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_528;
					case 1:
						goto IL_117;
					case 2:
						switch (A_0)
						{
						case ExportSource.SqlCommand:
							num = 10;
							continue;
						case ExportSource.DataTable:
							num = 16;
							continue;
						case ExportSource.ListView:
							num = 17;
							continue;
						default:
							num = 6;
							continue;
						}
						break;
					case 3:
						A_5.Remove(A_4[num2]);
						A_6.Add(A_4[num2], 0);
						num = 8;
						continue;
					case 4:
					{
						if (true)
						{
						}
						int num3;
						if (num3 >= A_3.Columns.Count)
						{
							num = 5;
							continue;
						}
						A_5.Add(A_3.Columns[num3].Text, 0);
						num3++;
						num = 13;
						continue;
					}
					case 5:
						goto IL_67F;
					case 6:
						num = 11;
						continue;
					case 7:
						if (num2 >= A_4.Count)
						{
							num = 18;
							continue;
						}
						goto IL_3E6;
					case 8:
						goto IL_46E;
					case 9:
						if (A_5.Contains(A_4[num2]))
						{
							num = 3;
							continue;
						}
						goto IL_46E;
					case 10:
						if (A_1.Connection.State != ConnectionState.Open)
						{
							num = 19;
							continue;
						}
						goto IL_11C;
					case 11:
						goto IL_67F;
					case 12:
						goto IL_11C;
					case 13:
						goto IL_C2;
					case 14:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									DataColumn dataColumn;
									if (dataColumn.DataType == typeof(byte[]))
									{
										num = 1;
										continue;
									}
									A_5.Add(dataColumn.ColumnName, 0);
									num = 6;
									continue;
								}
								case 1:
								{
									DataColumn dataColumn;
									A_5.Add(dataColumn.ColumnName, 1);
									num = 7;
									continue;
								}
								case 3:
									num = 5;
									continue;
								case 4:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									DataColumn dataColumn = (DataColumn)enumerator.Current;
									num = 0;
									continue;
								}
								case 5:
									goto IL_634;
								}
								IL_5E3:
								num = 4;
								continue;
								goto IL_5E3;
							}
							IL_634:;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_67C;
									case 1:
										disposable.Dispose();
										num = 0;
										continue;
									case 2:
										if (disposable != null)
										{
											num = 1;
											continue;
										}
										goto IL_67E;
									}
									break;
								}
							}
							IL_67C:
							IL_67E:;
						}
						goto IL_67F;
					case 15:
						goto IL_4B6;
					case 16:
					{
						if (A_2 == null)
						{
							num = 0;
							continue;
						}
						IEnumerator enumerator = A_2.Columns.GetEnumerator();
						num = 14;
						continue;
					}
					case 17:
					{
						if (A_3 == null)
						{
							num = 1;
							continue;
						}
						int num3 = 0;
						num = 21;
						continue;
					}
					case 18:
						return;
					case 19:
						A_1.Connection.Open();
						num = 12;
						continue;
					case 20:
						goto IL_4B6;
					case 21:
						goto IL_C2;
					}
					break;
					IL_C2:
					num = 4;
					continue;
					IL_3E6:
					num = 9;
					continue;
					try
					{
						IL_11C:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							lock (A_1)
							{
								IDataReader dataReader = spr\u2059.ᜀ = A_1.ExecuteReader(CommandBehavior.SchemaOnly);
								try
								{
									for (;;)
									{
										DataTable schemaTable = spr\u2059.ᜀ.GetSchemaTable();
										int num4 = 0;
										num = 2;
										for (;;)
										{
											bool flag;
											bool flag3;
											bool flag4;
											bool flag5;
											switch (num)
											{
											case 0:
												goto IL_32A;
											case 1:
												flag = false;
												goto IL_224;
											case 2:
												goto IL_353;
											case 3:
											{
												bool flag2;
												flag = flag2;
												goto IL_224;
											}
											case 4:
												if (!flag3)
												{
													num = 19;
													continue;
												}
												goto IL_32A;
											case 5:
												if (spr\u2059.ᜀ.GetFieldType(num4) == typeof(string))
												{
													num = 9;
													continue;
												}
												num = 1;
												continue;
											case 6:
											{
												if (num4 >= spr\u2059.ᜀ.FieldCount)
												{
													num = 13;
													continue;
												}
												bool flag2 = false;
												num = 8;
												continue;
											}
											case 7:
												goto IL_246;
											case 8:
												try
												{
													bool flag2 = (bool)spr\u2059.ᜀ(spr\u2059.ᜀ.GetName(num4), HyperlinksCollectionEditor.b("愧天怫䄭帯唱", a_), schemaTable);
													goto IL_25B;
												}
												catch
												{
													bool flag2 = false;
													goto IL_25B;
												}
												goto IL_32A;
												IL_25B:
												num = 14;
												continue;
											case 9:
												num = 3;
												continue;
											case 10:
												num = 15;
												continue;
											case 11:
												goto IL_389;
											case 12:
												goto IL_246;
											case 13:
												num = 11;
												continue;
											case 14:
												if (spr\u2059.ᜀ.GetFieldType(num4) == typeof(byte[]))
												{
													num = 10;
													continue;
												}
												num = 18;
												continue;
											case 15:
											{
												bool flag2;
												flag4 = flag2;
												goto IL_1F2;
											}
											case 16:
												if (flag5)
												{
													num = 0;
													continue;
												}
												A_5.Add(spr\u2059.ᜀ.GetName(num4), 0);
												num = 7;
												continue;
											case 17:
												goto IL_353;
											case 18:
												flag4 = false;
												goto IL_1F2;
											case 19:
												num = 16;
												continue;
											}
											break;
											IL_1F2:
											flag3 = flag4;
											num = 5;
											continue;
											IL_224:
											flag5 = flag;
											num = 4;
											continue;
											IL_246:
											num4++;
											num = 17;
											continue;
											IL_32A:
											A_5.Add(spr\u2059.ᜀ.GetName(num4), 1);
											num = 12;
											continue;
											IL_353:
											num = 6;
										}
									}
									IL_389:;
								}
								finally
								{
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											dataReader.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_3C8;
										}
										if (dataReader == null)
										{
											break;
										}
										num = 0;
									}
									IL_3C8:;
								}
							}
							break;
						}
						goto IL_67F;
					}
					finally
					{
						A_1.Connection.Close();
					}
					goto IL_3E6;
					IL_46E:
					num2++;
					num = 15;
					continue;
					IL_4B6:
					num = 7;
					continue;
					IL_67F:
					num2 = 0;
					num = 20;
				}
			}
			IL_117:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029椫嘭䀯崱䘳䈵洷丹唻刽㌿硁繃Ņⵇ㹉ཋ⅍㱏❑㥓㡕⭗癙⩛㽝቟塡⡣ཥ᭧ṩ㩫ݭᕯձ", a_));
			IL_528:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029椫嘭䀯崱䘳䈵洷丹唻刽㌿硁繃Ņⵇ㹉ཋ⅍㱏❑㥓㡕⭗癙⩛㽝቟塡⁣ݥᱧ୩㡫཭ቯṱᅳ", a_));
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000480A4 File Offset: 0x000470A4
	public static int ᜁ(Color A_0)
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
		int num = A_0.ToArgb();
		num &= 16777215;
		return (num & 65280) | num % 256 * 65536 | num / 65536 % 256;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00048114 File Offset: 0x00047114
	public static void ᜀ(ListView A_0, ListItemProc A_1, bool A_2, bool A_3)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				try
				{
					num = 4;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							num2 = A_0.Items.Count - 1;
							num = 6;
							continue;
						case 1:
							if (!A_0.Items[num3].Selected)
							{
								num = 12;
								continue;
							}
							goto IL_211;
						case 2:
							if (A_3)
							{
								num = 16;
								continue;
							}
							goto IL_179;
						case 3:
							goto IL_179;
						case 5:
							A_1(A_0.Items[num3]);
							num = 9;
							continue;
						case 6:
							goto IL_1F5;
						case 7:
							goto IL_211;
						case 8:
							goto IL_14D;
						case 9:
							goto IL_F3;
						case 10:
							goto IL_26D;
						case 11:
							goto IL_278;
						case 12:
							num = 20;
							continue;
						case 13:
							num = 2;
							continue;
						case 14:
							goto IL_26D;
						case 15:
							if (num3 >= A_0.Items.Count)
							{
								num = 10;
								continue;
							}
							num = 1;
							continue;
						case 16:
							goto IL_113;
						case 17:
							A_1(A_0.Items[num2]);
							num = 3;
							continue;
						case 18:
							goto IL_1F5;
						case 19:
							num = 14;
							continue;
						case 20:
							if (A_3)
							{
								num = 7;
								continue;
							}
							goto IL_F3;
						case 21:
							if (!A_0.Items[num2].Selected)
							{
								num = 13;
								continue;
							}
							goto IL_113;
						case 22:
							if (A_1 != null)
							{
								num = 5;
								continue;
							}
							goto IL_F3;
						case 23:
							if (A_1 != null)
							{
								num = 17;
								continue;
							}
							goto IL_179;
						case 24:
							if (num2 < 0)
							{
								num = 19;
								continue;
							}
							num = 21;
							continue;
						case 25:
							goto IL_14D;
						}
						if (A_2)
						{
							num = 0;
							continue;
						}
						num3 = 0;
						num = 8;
						continue;
						IL_F3:
						num3++;
						num = 25;
						continue;
						IL_113:
						num = 23;
						continue;
						IL_14D:
						num = 15;
						continue;
						IL_179:
						num2--;
						num = 18;
						continue;
						IL_1F5:
						num = 24;
						continue;
						IL_211:
						num = 22;
						continue;
						IL_26D:
						num = 11;
					}
					IL_278:
					return;
				}
				finally
				{
					A_0.EndUpdate();
				}
				goto IL_281;
			case 2:
				return;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			IL_281:
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
				A_0.BeginUpdate();
				num = 0;
				break;
			}
		}
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00048404 File Offset: 0x00047404
	public static void ᜀ(ListView A_0, CustomItemProc A_1, bool A_2, bool A_3)
	{
		int num = 1;
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
					return;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			case 2:
				try
				{
					num = 25;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							goto IL_35A;
						case 1:
							if (A_0.Items[num2].Tag != null)
							{
								num = 26;
								continue;
							}
							goto IL_14A;
						case 2:
							goto IL_14A;
						case 3:
							if (A_0.Items[num2].Tag is CustomItem)
							{
								num = 14;
								continue;
							}
							goto IL_14A;
						case 4:
							if (A_3)
							{
								num = 29;
								continue;
							}
							goto IL_14A;
						case 5:
							goto IL_1F2;
						case 6:
							goto IL_1C6;
						case 7:
							num = 24;
							continue;
						case 8:
							if (!A_0.Items[num2].Selected)
							{
								num = 9;
								continue;
							}
							goto IL_11C;
						case 9:
							num = 4;
							continue;
						case 10:
							goto IL_278;
						case 11:
							goto IL_1C6;
						case 12:
							if (A_0.Items[num3].Tag is CustomItem)
							{
								num = 28;
								continue;
							}
							goto IL_1F2;
						case 13:
							num3 = A_0.Items.Count - 1;
							num = 10;
							continue;
						case 14:
							A_1((CustomItem)A_0.Items[num2].Tag);
							num = 2;
							continue;
						case 15:
							if (A_0.Items[num3].Tag != null)
							{
								num = 23;
								continue;
							}
							goto IL_1F2;
						case 16:
							if (num3 < 0)
							{
								num = 18;
								continue;
							}
							num = 17;
							continue;
						case 17:
							if (!A_0.Items[num3].Selected)
							{
								num = 7;
								continue;
							}
							goto IL_21E;
						case 18:
							num = 0;
							continue;
						case 19:
							goto IL_21E;
						case 20:
							goto IL_278;
						case 21:
							if (num2 >= A_0.Items.Count)
							{
								num = 22;
								continue;
							}
							num = 8;
							continue;
						case 22:
							goto IL_35A;
						case 23:
							num = 12;
							continue;
						case 24:
							if (A_3)
							{
								num = 19;
								continue;
							}
							goto IL_1F2;
						case 26:
							num = 3;
							continue;
						case 27:
							goto IL_365;
						case 28:
							A_1((CustomItem)A_0.Items[num3].Tag);
							num = 5;
							continue;
						case 29:
							goto IL_11C;
						}
						if (A_2)
						{
							num = 13;
							continue;
						}
						num2 = 0;
						num = 6;
						continue;
						IL_11C:
						num = 1;
						continue;
						IL_14A:
						num2++;
						num = 11;
						continue;
						IL_1C6:
						num = 21;
						continue;
						IL_1F2:
						num3--;
						num = 20;
						continue;
						IL_21E:
						num = 15;
						continue;
						IL_278:
						num = 16;
						continue;
						IL_35A:
						num = 27;
					}
					IL_365:
					return;
				}
				finally
				{
					A_0.EndUpdate();
				}
				goto IL_36E;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			IL_36E:
			A_0.BeginUpdate();
			num = 2;
		}
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x000487B4 File Offset: 0x000477B4
	public static void ᜀ(CustomItem A_0, FontStyle A_1, bool A_2)
	{
		for (;;)
		{
			CellFont cellFont = null;
			ItemType itemType = A_0.ItemType;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_10A;
				case 2:
					goto IL_10A;
				case 3:
					return;
				case 4:
					spr\u2059.ᜀ(cellFont, A_1, A_2);
					num = 3;
					continue;
				case 5:
					if (true)
					{
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
						goto IL_10A;
					}
					break;
				case 6:
					switch (itemType)
					{
					case ItemType.ItemFormat:
					case ItemType.FieldFormat:
						cellFont = ((CellFormat)A_0).Font;
						num = 5;
						continue;
					case ItemType.NoteFormat:
						cellFont = ((CellNoteFormat)A_0).Font;
						num = 8;
						continue;
					case ItemType.Hyperlink:
						cellFont = ((CellHyperlink)A_0).Format.Font;
						num = 7;
						continue;
					case ItemType.Note:
						cellFont = ((CellNote)A_0).Format.Font;
						num = 1;
						continue;
					default:
						num = 0;
						continue;
					}
					break;
				case 7:
					goto IL_10A;
				case 8:
					goto IL_10A;
				case 9:
					if (cellFont != null)
					{
						num = 4;
						continue;
					}
					return;
				}
				break;
				IL_10A:
				num = 9;
			}
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00048900 File Offset: 0x00047900
	public static void ᜀ(CellFont A_0, FontStyle A_1, bool A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1 != FontStyle.Strikeout)
				{
					num = 5;
					continue;
				}
				goto IL_129;
			case 2:
				num = 1;
				continue;
			case 3:
				switch (A_1)
				{
				case FontStyle.Bold:
					goto IL_121;
				case FontStyle.Italic:
					goto IL_EB;
				default:
					num = 9;
					continue;
				}
				break;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F4;
				default:
					if (false)
					{
					}
					if (A_1 != FontStyle.Strikeout)
					{
						num = 6;
						continue;
					}
					goto IL_6B;
				}
				break;
			case 5:
				return;
			case 6:
				return;
			case 7:
				if (true)
				{
				}
				num = 3;
				continue;
			case 8:
				switch (A_1)
				{
				case FontStyle.Bold:
					goto IL_A8;
				case FontStyle.Italic:
					goto IL_E3;
				default:
					num = 2;
					continue;
				}
				break;
			case 9:
				num = 4;
				continue;
			}
			if (A_2)
			{
				num = 7;
				continue;
			}
			IL_F4:
			num = 8;
		}
		return;
		IL_6B:
		A_0.Strikeout = true;
		return;
		IL_A8:
		A_0.Bold = false;
		return;
		IL_E3:
		A_0.Italic = false;
		return;
		IL_EB:
		A_0.Italic = true;
		return;
		IL_121:
		A_0.Bold = true;
		return;
		IL_129:
		A_0.Strikeout = false;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00048A40 File Offset: 0x00047A40
	private static Point ᜀ(Graphics A_0, Font A_1)
	{
		switch (0)
		{
		default:
		{
			char[] array;
			for (;;)
			{
				array = new char[52];
				int num = 0;
				if (true)
				{
				}
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (num3 > 25)
						{
							num2 = 7;
							continue;
						}
						array[num3 + 26] = (char)(num3 + 97);
						num3++;
						num2 = 1;
						continue;
					}
					case 1:
						goto IL_C1;
					case 2:
						goto IL_E0;
					case 3:
						goto IL_C1;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						default:
						{
							if (false)
							{
							}
							int num3 = 0;
							num2 = 3;
							continue;
						}
						}
						break;
					case 5:
						if (num > 25)
						{
							num2 = 4;
							continue;
						}
						array[num] = (char)(num + 65);
						num++;
						num2 = 2;
						continue;
					case 6:
						goto IL_52;
					case 7:
						goto IL_DE;
					}
					break;
					IL_C1:
					num2 = 0;
					continue;
					IL_E0:
					num2 = 5;
					continue;
					IL_52:
					goto IL_E0;
				}
			}
			IL_DE:
			SizeF sizeF = A_0.MeasureString(new string(array), A_1);
			Point result = new Point(0, 0);
			result.X = (int)sizeF.Width;
			result.Y = (int)sizeF.Height;
			result.X /= 52;
			return result;
		}
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00048B98 File Offset: 0x00047B98
	public static bool ᜀ(WorkSheet A_0, string A_1, string A_2, ref string A_3)
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				Form form = new Form();
				Graphics a_2 = form.CreateGraphics();
				Point point = spr\u2059.ᜀ(a_2, form.Font);
				form.FormBorderStyle = FormBorderStyle.FixedDialog;
				form.MaximizeBox = false;
				form.MinimizeBox = false;
				form.ShowInTaskbar = false;
				form.Text = A_1;
				form.Width = (int)(Math.BigMul(180, point.X) / 4L);
				form.StartPosition = FormStartPosition.CenterScreen;
				Label label = new Label();
				label.Parent = form;
				label.Text = A_2;
				label.Left = (int)(Math.BigMul(8, point.X) / 4L);
				label.Top = (int)(Math.BigMul(8, point.Y) / 8L);
				label.Width = (int)(Math.BigMul(160, point.X) / 4L);
				label.Height = 16;
				int top = 0;
				TextBox textBox = null;
				ComboBox comboBox = null;
				int num = 14;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_245;
					case 1:
						A_0.SQLCommand.Connection.Open();
						num = 10;
						continue;
					case 2:
						return true;
					case 3:
						if (A_0.SQLCommand != null)
						{
							num = 15;
							continue;
						}
						goto IL_632;
					case 4:
						A_3 = textBox.Text;
						if (true)
						{
						}
						num = 2;
						continue;
					case 5:
						if (A_0.ListView != null)
						{
							num = 18;
							continue;
						}
						goto IL_632;
					case 6:
						num = 13;
						continue;
					case 7:
						num = 25;
						continue;
					case 8:
					{
						IEnumerator enumerator = A_0.DataTable.Columns.GetEnumerator();
						num = 0;
						continue;
					}
					case 9:
						if (A_0.SQLCommand.CommandText.Length != 0)
						{
							num = 19;
							continue;
						}
						goto IL_632;
					case 10:
						goto IL_426;
					case 11:
						return true;
					case 12:
						if (A_0.DataTable != null)
						{
							num = 8;
							continue;
						}
						goto IL_632;
					case 13:
						goto IL_632;
					case 14:
					{
						if (A_0 == null)
						{
							num = 21;
							continue;
						}
						comboBox = new ComboBox();
						comboBox.Parent = form;
						comboBox.Left = label.Left;
						comboBox.Top = label.Top + label.Height + 5;
						comboBox.Width = (int)(Math.BigMul(160, point.X) / 4L);
						comboBox.MaxLength = 255;
						comboBox.Text = A_3;
						comboBox.SelectAll();
						top = comboBox.Top + comboBox.Height + 15;
						ExportSource dataSource = A_0.DataSource;
						num = 26;
						continue;
					}
					case 15:
						goto IL_30E;
					case 16:
						goto IL_632;
					case 17:
						if (A_0.SQLCommand.Connection.State != ConnectionState.Open)
						{
							num = 1;
							continue;
						}
						goto IL_426;
					case 18:
					{
						IEnumerator enumerator2 = A_0.ListView.Columns.GetEnumerator();
						num = 24;
						continue;
					}
					case 19:
						num = 23;
						continue;
					case 20:
						num = 17;
						continue;
					case 21:
						goto IL_52A;
					case 22:
						if (form.ShowDialog() == DialogResult.OK)
						{
							num = 7;
							continue;
						}
						return false;
					case 23:
						if (A_0.SQLCommand.Connection != null)
						{
							num = 20;
							continue;
						}
						goto IL_632;
					case 24:
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 2:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 4;
										continue;
									}
									ColumnHeader columnHeader = (ColumnHeader)enumerator2.Current;
									comboBox.Items.Add(columnHeader.Text);
									num = 0;
									continue;
								}
								case 3:
									goto IL_1F7;
								case 4:
									num = 3;
									continue;
								}
								IL_1A4:
								num = 2;
								continue;
								goto IL_1A4;
							}
							IL_1F7:
							goto IL_632;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable = enumerator2 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_242;
									case 2:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_244;
									}
									break;
								}
							}
							IL_242:
							IL_244:;
						}
						goto Block_3;
					case 25:
						if (A_0 == null)
						{
							num = 4;
							continue;
						}
						A_3 = comboBox.Text;
						num = 11;
						continue;
					case 26:
					{
						ExportSource dataSource;
						switch (dataSource)
						{
						case ExportSource.SqlCommand:
							num = 3;
							continue;
						case ExportSource.DataTable:
							num = 12;
							continue;
						case ExportSource.ListView:
							num = 5;
							continue;
						default:
							num = 6;
							continue;
						}
						break;
					}
					}
					break;
					IL_30E:
					num = 9;
					continue;
					Block_3:
					try
					{
						IL_245:
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 1:
								num = 2;
								continue;
							case 2:
								goto IL_2C0;
							case 3:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								DataColumn dataColumn = (DataColumn)enumerator.Current;
								comboBox.Items.Add(dataColumn.ColumnName);
								num = 0;
								continue;
							}
							}
							IL_29A:
							num = 3;
							continue;
							goto IL_29A;
						}
						IL_2C0:
						goto IL_632;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable2 = enumerator as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_30B;
								case 1:
									if (disposable2 != null)
									{
										num = 2;
										continue;
									}
									goto IL_30D;
								case 2:
									disposable2.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_30B:
						IL_30D:;
					}
					goto IL_30E;
					IL_52A:
					textBox = new TextBox();
					textBox.Parent = form;
					textBox.Left = label.Left;
					textBox.Top = label.Top + label.Height + 5;
					textBox.Width = (int)(Math.BigMul(160, point.X) / 4L);
					textBox.MaxLength = 255;
					textBox.Text = A_3;
					textBox.SelectAll();
					top = textBox.Top + textBox.Height + 15;
					num = 16;
					continue;
					try
					{
						IL_426:
						IDataReader dataReader = A_0.SQLCommand.ExecuteReader(CommandBehavior.SchemaOnly);
						try
						{
							for (;;)
							{
								int num2;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num2 = 0;
									break;
								}
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_4A4;
									case 1:
										goto IL_4A4;
									case 2:
										goto IL_4D2;
									case 3:
										if (num2 >= dataReader.FieldCount)
										{
											num = 4;
											continue;
										}
										comboBox.Items.Add(dataReader.GetName(num2));
										num2++;
										num = 1;
										continue;
									case 4:
										num = 2;
										continue;
									}
									break;
									IL_4A4:
									num = 3;
								}
							}
							IL_4D2:;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_511;
								case 1:
									dataReader.Dispose();
									num = 0;
									continue;
								}
								if (dataReader == null)
								{
									break;
								}
								num = 1;
							}
							IL_511:;
						}
						goto IL_632;
					}
					finally
					{
						A_0.SQLCommand.Connection.Close();
					}
					goto IL_52A;
					IL_632:
					int width = (int)(Math.BigMul(50, point.X) / 4L);
					int height = (int)(Math.BigMul(14, point.Y) / 8L);
					Button button = new Button();
					button.Parent = form;
					button.Text = HyperlinksCollectionEditor.b("戬搮", a_);
					button.DialogResult = DialogResult.OK;
					form.AcceptButton = button;
					button.Left = (int)(Math.BigMul(38, point.X) / 4L);
					button.Top = top;
					button.Width = width;
					button.Height = height;
					Button button2 = new Button();
					button2.Parent = form;
					button2.Text = HyperlinksCollectionEditor.b("測丮弰倲倴嬶", a_);
					button2.DialogResult = DialogResult.Cancel;
					form.CancelButton = button2;
					button2.Left = (int)(Math.BigMul(92, point.X) / 4L);
					button2.Top = top;
					button2.Width = width;
					button2.Height = height;
					form.Height = form.Height - form.ClientSize.Height + button2.Top + button2.Height + 13;
					num = 22;
				}
			}
			return true;
		}
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00049464 File Offset: 0x00048464
	public static string ᜀ(string A_0, string A_1, string A_2)
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
		string result = A_2;
		spr\u2059.ᜀ(null, A_0, A_1, ref result);
		return result;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x000494B0 File Offset: 0x000484B0
	public static Color ᜀ(CellColor A_0)
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
		uint value = spr\u2009.᠑[(int)A_0];
		byte[] bytes = BitConverter.GetBytes(value);
		return Color.FromArgb((int)bytes[0], (int)bytes[1], (int)bytes[2]);
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x00049508 File Offset: 0x00048508
	public static CellColor ᜀ(Color A_0)
	{
		int num;
		for (;;)
		{
			num = spr\u2009.᠑.GetLowerBound(0);
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return (CellColor)num;
				case 1:
					goto IL_6A;
				case 2:
					if ((ulong)spr\u2009.᠑[num] == (ulong)((long)A_0.ToArgb()))
					{
						num2 = 0;
						continue;
					}
					num++;
					num2 = 1;
					continue;
				case 3:
					goto IL_6A;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (num > spr\u2009.᠑.GetUpperBound(0))
						{
							if (true)
							{
							}
							num2 = 5;
							continue;
						}
						break;
					}
					num2 = 2;
					continue;
				case 5:
					return CellColor.Black;
				}
				break;
				IL_6A:
				num2 = 4;
			}
		}
		return (CellColor)num;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x000495D4 File Offset: 0x000485D4
	public static void ᜀ(CustomItem A_0, XlsFontUnderline A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
			{
				ItemType itemType;
				switch (itemType)
				{
				case ItemType.ItemFormat:
				case ItemType.FieldFormat:
					goto IL_BC;
				case ItemType.NoteFormat:
					goto IL_3F;
				case ItemType.Hyperlink:
					goto IL_53;
				case ItemType.Note:
					goto IL_CE;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_53;
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
			}
			case 3:
				return;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				ItemType itemType = A_0.ItemType;
				num = 2;
			}
		}
		return;
		IL_3F:
		((CellNoteFormat)A_0).Font.Underline = A_1;
		return;
		IL_53:
		((CellHyperlink)A_0).Format.Font.Underline = A_1;
		return;
		IL_BC:
		((CellFormat)A_0).Font.Underline = A_1;
		return;
		IL_CE:
		((CellNote)A_0).Format.Font.Underline = A_1;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x000496C8 File Offset: 0x000486C8
	public static void ᜀ(CustomItem A_0, Spire.DataExport.XLS.HorizontalAlignment A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				return;
			case 3:
			{
				ItemType itemType;
				switch (itemType)
				{
				case ItemType.ItemFormat:
				case ItemType.FieldFormat:
					goto IL_BC;
				case ItemType.NoteFormat:
					goto IL_37;
				case ItemType.Hyperlink:
					goto IL_4B;
				case ItemType.Note:
					goto IL_CE;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4B;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				if (true)
				{
				}
				ItemType itemType = A_0.ItemType;
				num = 3;
			}
		}
		return;
		IL_37:
		((CellNoteFormat)A_0).Alignment.Horizontal = A_1;
		return;
		IL_4B:
		((CellHyperlink)A_0).Format.Alignment.Horizontal = A_1;
		return;
		IL_BC:
		((CellFormat)A_0).Alignment.Horizontal = A_1;
		return;
		IL_CE:
		((CellNote)A_0).Format.Alignment.Horizontal = A_1;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x000497BC File Offset: 0x000487BC
	public static void ᜀ(CustomItem A_0, VerticalAlignment A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ItemType itemType;
				switch (itemType)
				{
				case ItemType.ItemFormat:
				case ItemType.FieldFormat:
					goto IL_BC;
				case ItemType.NoteFormat:
					goto IL_37;
				case ItemType.Hyperlink:
					goto IL_4B;
				case ItemType.Note:
					goto IL_CE;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4B;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
			case 1:
				return;
			case 2:
				return;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				ItemType itemType = A_0.ItemType;
				if (true)
				{
				}
				num = 0;
			}
		}
		return;
		IL_37:
		((CellNoteFormat)A_0).Alignment.Vertical = A_1;
		return;
		IL_4B:
		((CellHyperlink)A_0).Format.Alignment.Vertical = A_1;
		return;
		IL_BC:
		((CellFormat)A_0).Alignment.Vertical = A_1;
		return;
		IL_CE:
		((CellNote)A_0).Format.Alignment.Vertical = A_1;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x000498B0 File Offset: 0x000488B0
	private static int ᜀ(Graphics A_0, Pen A_1, int A_2, int A_3, int A_4, int A_5, int A_6)
	{
		int result;
		for (;;)
		{
			IL_30:
			result = 0;
			int num = 2;
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
						goto IL_6A;
					case 1:
						goto IL_52;
					case 2:
						if (true)
						{
						}
						if (A_4 >= A_6)
						{
							num = 1;
							continue;
						}
						goto IL_6C;
					}
					goto IL_30;
				}
				IL_52:
				A_4 = A_6;
				result = -1;
				num = 0;
			}
		}
		IL_6A:
		IL_6C:
		A_0.DrawLine(A_1, A_2, A_3, A_4, A_5);
		return result;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00049938 File Offset: 0x00048938
	public static void ᜀ(CellBorderStyle A_0, Graphics A_1, Brush A_2, Pen A_3, Font A_4, Rectangle A_5)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num2;
			int num3;
			int num4;
			for (;;)
			{
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 28;
						continue;
					case 1:
						goto IL_3D3;
					case 2:
						goto IL_27A;
					case 3:
						return;
					case 4:
						num = 27;
						continue;
					case 5:
						goto IL_1E4;
					case 6:
						if (spr\u2059.ᜀ(A_1, A_3, num2 + 18, num3, num2 + 21, num3, num4) != 0)
						{
							num = 14;
							continue;
						}
						num2 += 25;
						num = 5;
						continue;
					case 7:
						return;
					case 8:
						if (true)
						{
						}
						switch (A_0)
						{
						case CellBorderStyle.None:
							goto IL_174;
						case CellBorderStyle.Thin:
						case CellBorderStyle.Medium:
						case CellBorderStyle.Thick:
							goto IL_3A6;
						case CellBorderStyle.Dashed:
						case CellBorderStyle.MediumDashed:
							goto IL_3D3;
						case CellBorderStyle.Dotted:
							goto IL_37B;
						case CellBorderStyle.Double:
							goto IL_14D;
						case CellBorderStyle.Hair:
							goto IL_449;
						case CellBorderStyle.DashDot:
						case CellBorderStyle.MediumDashDot:
						case CellBorderStyle.SlantedDashDot:
							goto IL_33C;
						case CellBorderStyle.DashDotDot:
						case CellBorderStyle.MediumDashDotDot:
							goto IL_1E4;
						default:
							num = 18;
							continue;
						}
						break;
					case 9:
						goto IL_27A;
					case 10:
						if (A_0 != CellBorderStyle.Medium)
						{
							num = 4;
							continue;
						}
						goto IL_3B6;
					case 11:
						if (spr\u2059.ᜀ(A_1, A_3, num2 + 11, num3, num2 + 14, num3, num4) != 0)
						{
							num = 24;
							continue;
						}
						num2 += 18;
						num = 26;
						continue;
					case 12:
						if (spr\u2059.ᜀ(A_1, A_3, num2, num3, num2 + 2, num3, num4) != 0)
						{
							num = 3;
							continue;
						}
						num2 += 4;
						num = 17;
						continue;
					case 13:
						return;
					case 14:
						return;
					case 15:
						return;
					case 16:
						if (spr\u2059.ᜀ(A_1, A_3, num2, num3, num2 + 7, num3, num4) != 0)
						{
							num = 20;
							continue;
						}
						num2 += 14;
						num = 1;
						continue;
					case 17:
						goto IL_449;
					case 18:
						return;
					case 19:
						goto IL_27A;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A5;
						default:
							goto IL_22A;
						}
						break;
					case 21:
						if (spr\u2059.ᜀ(A_1, A_3, num2, num3, num2 + 7, num3, num4) != 0)
						{
							num = 15;
							continue;
						}
						num = 25;
						continue;
					case 22:
						return;
					case 23:
						goto IL_37B;
					case 24:
						return;
					case 25:
						if (spr\u2059.ᜀ(A_1, A_3, num2 + 11, num3, num2 + 14, num3, num4) != 0)
						{
							num = 7;
							continue;
						}
						num = 6;
						continue;
					case 26:
						goto IL_33C;
					case 27:
						switch (A_0)
						{
						case CellBorderStyle.Thick:
							A_3.Width = 3f;
							num = 2;
							continue;
						case CellBorderStyle.Double:
						case CellBorderStyle.Hair:
						case CellBorderStyle.DashDot:
						case CellBorderStyle.DashDotDot:
							goto IL_309;
						case CellBorderStyle.MediumDashed:
						case CellBorderStyle.MediumDashDot:
						case CellBorderStyle.MediumDashDotDot:
						case CellBorderStyle.SlantedDashDot:
							goto IL_3B6;
						default:
							num = 0;
							continue;
						}
						break;
					case 28:
						goto IL_309;
					case 29:
						if (spr\u2059.ᜀ(A_1, A_3, num2, num3, num2 + 3, num3, num4) != 0)
						{
							num = 22;
							continue;
						}
						num2 += 6;
						num = 23;
						continue;
					case 30:
						if (spr\u2059.ᜀ(A_1, A_3, num2, num3, num2 + 7, num3, num4) != 0)
						{
							num = 13;
							continue;
						}
						num = 11;
						continue;
					}
					break;
					IL_1E4:
					num = 21;
					continue;
					IL_2A5:
					num = 8;
					continue;
					IL_27A:
					num4 = A_5.Right - A_5.Left - 10;
					num3 = (A_5.Bottom + A_5.Top) / 2;
					num2 = 10;
					goto IL_2A5;
					IL_309:
					A_3.Width = 1f;
					num = 19;
					continue;
					IL_33C:
					num = 30;
					continue;
					IL_37B:
					num = 29;
					continue;
					IL_3B6:
					A_3.Width = 2f;
					num = 9;
					continue;
					IL_3D3:
					num = 16;
					continue;
					IL_449:
					num = 12;
				}
			}
			return;
			IL_14D:
			spr\u2059.ᜀ(A_1, A_3, num2, num3 - 1, num2 + num4, num3 - 1, num4);
			spr\u2059.ᜀ(A_1, A_3, num2, num3 + 1, num2 + num4, num3 + 1, num4);
			return;
			IL_174:
			A_1.DrawString(HyperlinksCollectionEditor.b("欤䠦䜨个", a_), A_4, A_2, (float)(A_5.Height + 5), (float)((A_5.Height - A_4.Height) % 2 + A_5.Top));
			return;
			IL_22A:
			if (false)
			{
			}
			return;
			IL_3A6:
			spr\u2059.ᜀ(A_1, A_3, num2, num3, num2 + num4, num3, num4);
			return;
		}
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00049DD4 File Offset: 0x00048DD4
	public static void ᜀ(Graphics A_0, Pen A_1, int A_2, int A_3, int A_4)
	{
		switch (A_2)
		{
		case 2:
			if (true)
			{
			}
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 1, A_4);
			A_0.DrawLine(A_1, A_3 + 2, A_4, A_3 + 3, A_4);
			A_0.DrawLine(A_1, A_3 + 1, A_4 + 1, A_3 + 2, A_4 + 1);
			A_0.DrawLine(A_1, A_3 + 3, A_4 + 1, A_3 + 4, A_4 + 1);
			A_0.DrawLine(A_1, A_3, A_4 + 2, A_3 + 1, A_4 + 2);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 2, A_3 + 3, A_4 + 2);
			A_0.DrawLine(A_1, A_3 + 1, A_4 + 3, A_3 + 2, A_4 + 3);
			A_0.DrawLine(A_1, A_3 + 3, A_4 + 3, A_3 + 4, A_4 + 3);
			return;
		case 3:
			A_0.DrawLine(A_1, A_3 + 1, A_4, A_3 + 4, A_4);
			A_0.DrawLine(A_1, A_3, A_4 + 1, A_3 + 2, A_4 + 1);
			A_0.DrawLine(A_1, A_3 + 3, A_4 + 1, A_3 + 4, A_4 + 1);
			A_0.DrawLine(A_1, A_3 + 1, A_4 + 2, A_3 + 4, A_4 + 2);
			A_0.DrawLine(A_1, A_3, A_4 + 3, A_3 + 2, A_4 + 3);
			A_0.DrawLine(A_1, A_3 + 3, A_4 + 3, A_3 + 4, A_4 + 3);
			return;
		case 4:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 1, A_4);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 1, A_3 + 3, A_4 + 1);
			A_0.DrawLine(A_1, A_3, A_4 + 2, A_3 + 1, A_4 + 2);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 3, A_3 + 3, A_4 + 3);
			return;
		case 5:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4);
			A_0.DrawLine(A_1, A_3, A_4 + 1, A_3 + 4, A_4 + 1);
			return;
		case 6:
			A_0.DrawLine(A_1, A_3, A_4, A_3, A_4 + 4);
			A_0.DrawLine(A_1, A_3 + 1, A_4, A_3 + 1, A_4 + 4);
			return;
		case 7:
			A_0.DrawLine(A_1, A_3 + 1, A_4, A_3 + 5, A_4 + 4);
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4 + 4);
			return;
		case 8:
			A_0.DrawLine(A_1, A_3 + 3, A_4, A_3 - 1, A_4 + 4);
			A_0.DrawLine(A_1, A_3 + 4, A_4, A_3, A_4 + 4);
			return;
		case 9:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 2, A_4);
			A_0.DrawLine(A_1, A_3, A_4 + 1, A_3 + 2, A_4 + 1);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 2, A_3 + 4, A_4 + 2);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 3, A_3 + 4, A_4 + 3);
			return;
		case 10:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4);
			A_0.DrawLine(A_1, A_3, A_4 + 1, A_3 + 2, A_4 + 1);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 2, A_3 + 4, A_4 + 2);
			A_0.DrawLine(A_1, A_3, A_4 + 3, A_3 + 4, A_4 + 3);
			return;
		case 11:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4);
			return;
		case 12:
			A_0.DrawLine(A_1, A_3, A_4, A_3, A_4 + 4);
			return;
		case 13:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4 + 4);
			return;
		case 14:
			A_0.DrawLine(A_1, A_3 + 4, A_4, A_3, A_4 + 4);
			return;
		case 15:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4);
			A_0.DrawLine(A_1, A_3, A_4, A_3, A_4 + 4);
			return;
		case 16:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 4, A_4 + 4);
			A_0.DrawLine(A_1, A_3 + 2, A_4, A_3, A_4 + 2);
			return;
		case 17:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 1, A_4);
			A_0.DrawLine(A_1, A_3 + 2, A_4 + 2, A_3 + 3, A_4 + 2);
			return;
		case 18:
			A_0.DrawLine(A_1, A_3, A_4, A_3 + 1, A_4);
			A_0.DrawLine(A_1, A_3 + 4, A_4 + 2, A_3 + 5, A_4 + 2);
			return;
		default:
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
			return;
		}
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0004A1D0 File Offset: 0x000491D0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2059()
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
		spr\u2059.ᜀ = null;
		spr\u2059.ᜁ = true;
		spr\u2059.ᜂ = null;
		spr\u2059.ᜃ = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};
	}

	// Token: 0x040005DD RID: 1501
	public static IDataReader ᜀ;

	// Token: 0x040005DE RID: 1502
	public static bool ᜁ;

	// Token: 0x040005DF RID: 1503
	public static DataRow ᜂ;

	// Token: 0x040005E0 RID: 1504
	private static char[] ᜃ;
}
