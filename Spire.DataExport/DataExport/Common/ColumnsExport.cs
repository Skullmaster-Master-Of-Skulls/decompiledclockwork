using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.DBF;
using Spire.DataExport.Delegates;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.Common
{
	// Token: 0x02000164 RID: 356
	public class ColumnsExport : CollectionBase
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0005B84C File Offset: 0x0005A84C
		public ColumnsExport(object Holder, NormalFunc NormalFunc)
		{
			this.ᜀ = Holder;
			this.\u1712 = NormalFunc;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0005B878 File Offset: 0x0005A878
		private void ᜀ()
		{
			int a_ = 17;
			for (;;)
			{
				PropertyInfo property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("測䀮崰䘲場夶䨸", a_));
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1FC;
					case 1:
						if (property != null)
						{
							num = 19;
							continue;
						}
						goto IL_4E7;
					case 2:
						goto IL_669;
					case 3:
						goto IL_527;
					case 4:
						if (property != null)
						{
							num = 24;
							continue;
						}
						goto IL_154;
					case 5:
						if (property != null)
						{
							num = 47;
							continue;
						}
						goto IL_435;
					case 6:
						this.\u170D = (StringListCollection)property.GetValue(this.ᜀ, null);
						num = 42;
						continue;
					case 7:
						this.ᜄ = (IDbCommand)property.GetValue(this.ᜀ, null);
						num = 27;
						continue;
					case 8:
						goto IL_4A7;
					case 9:
						goto IL_739;
					case 10:
						this.ᜇ = (FormatsExport)property.GetValue(this.ᜀ, null);
						num = 3;
						continue;
					case 11:
						return;
					case 12:
						if (property != null)
						{
							num = 17;
							continue;
						}
						goto IL_114;
					case 13:
						if (property != null)
						{
							num = 23;
							continue;
						}
						goto IL_1FC;
					case 14:
						goto IL_435;
					case 15:
						if (property != null)
						{
							num = 28;
							continue;
						}
						goto IL_4A7;
					case 16:
						if (property != null)
						{
							num = 26;
							continue;
						}
						goto IL_6D1;
					case 17:
						this.ᜌ = (StringListCollection)property.GetValue(this.ᜀ, null);
						num = 37;
						continue;
					case 18:
						if (property != null)
						{
							num = 7;
							continue;
						}
						goto IL_3D0;
					case 19:
						this.ᜈ = (bool)property.GetValue(this.ᜀ, null);
						num = 40;
						continue;
					case 20:
						this.ᜉ = (StringListCollection)property.GetValue(this.ᜀ, null);
						num = 2;
						continue;
					case 21:
						this.ᜑ = (CultureInfo)property.GetValue(this.ᜀ, null);
						num = 11;
						continue;
					case 22:
						if (property != null)
						{
							num = 21;
							continue;
						}
						return;
					case 23:
						this.ᜃ = (ExportSource)property.GetValue(this.ᜀ, null);
						num = 0;
						continue;
					case 24:
						this.ᜊ = (StringListCollection)property.GetValue(this.ᜀ, null);
						num = 41;
						continue;
					case 25:
						if (property != null)
						{
							num = 20;
							continue;
						}
						goto IL_669;
					case 26:
						this.ᜂ = (StringListCollection)property.GetValue(this.ᜀ, null);
						num = 30;
						continue;
					case 27:
						goto IL_3D0;
					case 28:
						this.ᜏ = (int)property.GetValue(this.ᜀ, null);
						num = 8;
						continue;
					case 29:
						goto IL_1BC;
					case 30:
						goto IL_6D1;
					case 31:
						this.ᜋ = (StringListCollection)property.GetValue(this.ᜀ, null);
						num = 43;
						continue;
					case 32:
						goto IL_2BC;
					case 33:
						if (property != null)
						{
							num = 31;
							continue;
						}
						goto IL_564;
					case 34:
						if (property != null)
						{
							num = 36;
							continue;
						}
						goto IL_1BC;
					case 35:
						if (property != null)
						{
							num = 10;
							continue;
						}
						goto IL_527;
					case 36:
						this.ᜐ = (int)property.GetValue(this.ᜀ, null);
						num = 29;
						continue;
					case 37:
						goto IL_114;
					case 38:
						goto IL_264;
					case 39:
						if (property != null)
						{
							num = 6;
							continue;
						}
						goto IL_318;
					case 40:
						goto IL_4E7;
					case 41:
						if (true)
						{
						}
						goto IL_154;
					case 42:
						goto IL_318;
					case 43:
						goto IL_564;
					case 44:
						if (property != null)
						{
							num = 38;
							continue;
						}
						goto IL_739;
					case 45:
						if (property != null)
						{
							num = 46;
							continue;
						}
						goto IL_2BC;
					case 46:
						this.ᜅ = (DataTable)property.GetValue(this.ᜀ, null);
						num = 32;
						continue;
					case 47:
						this.ᜆ = (ListView)property.GetValue(this.ᜀ, null);
						num = 14;
						continue;
					}
					break;
					IL_114:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("縬䐮堰䌲朴堶丸䠺", a_));
					num = 15;
					continue;
					IL_154:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("礬䘮䔰弲倴䐶", a_));
					num = 33;
					continue;
					IL_1BC:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("測䀮崰䘲場夶䨸眺堼儾♀㝂ⵄ", a_));
					num = 39;
					continue;
					IL_1FC:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("縬縮細瀲娴娶吸娺匼嬾", a_));
					num = 18;
					continue;
					IL_264:
					this.ᜎ = (StringListCollection)property.GetValue(this.ᜀ, null);
					num = 9;
					continue;
					IL_2BC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_264;
					default:
						if (false)
						{
						}
						property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("愬䘮䈰䜲挴帶尸䰺", a_));
						num = 5;
						continue;
					}
					IL_318:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("挬䀮䔰朲䜴䈶圸堺尼䬾⁀⅂⥄≆ੈ⑊⅌㩎㱐㵒♔", a_));
					num = 44;
					continue;
					IL_3D0:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("椬丮䔰刲愴嘶嬸场堼", a_));
					num = 45;
					continue;
					IL_435:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("椬丮䔰刲猴堶䬸嘺尼䬾㉀", a_));
					num = 35;
					continue;
					IL_4A7:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("怬丮䤰愲娴䀶䨸", a_));
					num = 34;
					continue;
					IL_4E7:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("測娮䈰䜲娴娶缸吺似刾⁀㝂㙄", a_));
					num = 25;
					continue;
					IL_527:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("氬娮䔰尲瘴嘶唸堺渼䬾㍀ᝂ㱄㝆ⱈ", a_));
					num = 1;
					continue;
					IL_564:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("測䀮崰䘲場夶䨸稺儼嘾♀ⵂ", a_));
					num = 12;
					continue;
					IL_669:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("測䀮崰䘲場夶䨸氺吼嬾㕀⭂", a_));
					num = 4;
					continue;
					IL_6D1:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("椬丮䔰刲昴堶䰸䤺帼娾", a_));
					num = 13;
					continue;
					IL_739:
					property = this.ᜀ.GetType().GetProperty(HyperlinksCollectionEditor.b("測娮崰䜲䀴䔶尸", a_));
					num = 22;
				}
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0005BFFC File Offset: 0x0005AFFC
		private int ᜀ(int A_0, bool A_1, IDataReader A_2, DataTable A_3)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int result;
				string text;
				DataColumn dataColumn2;
				for (;;)
				{
					result = -1;
					int num = 21;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_52F;
						case 1:
						{
							if (this.ᜆ == null)
							{
								num = 18;
								continue;
							}
							int num2 = 0;
							num = 11;
							continue;
						}
						case 2:
						{
							ExportSource exportSource = this.ᜃ;
							num = 20;
							continue;
						}
						case 3:
							return -1;
						case 4:
							goto IL_2C2;
						case 5:
							if (!A_1)
							{
								num = 28;
								continue;
							}
							goto IL_440;
						case 6:
							result = A_0;
							num = 45;
							continue;
						case 7:
							goto IL_1BD;
						case 8:
						{
							int num2;
							if (num2 >= this.ᜆ.Columns.Count)
							{
								num = 19;
								continue;
							}
							num = 17;
							continue;
						}
						case 9:
							goto IL_5AE;
						case 10:
							try
							{
								bool flag = (bool)spr\u2059.ᜀ(text, HyperlinksCollectionEditor.b("椟儡栣䤥䘧䴩", a_), A_3);
								goto IL_37F;
							}
							catch
							{
								bool flag = false;
								goto IL_37F;
							}
							goto IL_357;
							IL_37F:
							num = 35;
							continue;
						case 11:
							goto IL_4B0;
						case 12:
							num = 3;
							continue;
						case 13:
							num = 5;
							continue;
						case 14:
							if (text != null)
							{
								num = 9;
								continue;
							}
							return -1;
						case 15:
						{
							DataColumn dataColumn;
							if (dataColumn.DataType != typeof(byte[]))
							{
								num = 36;
								continue;
							}
							return result;
						}
						case 16:
							if (A_2.IsClosed)
							{
								num = 4;
								continue;
							}
							text = (string)spr\u2059.ᜀ(A_2.GetName(A_0), HyperlinksCollectionEditor.b("挟䴡䠣匥䔧䐩戫伭崯圱", a_), A_3);
							num = 22;
							continue;
						case 17:
						{
							int num2;
							if (string.Compare(this.ᜆ.Columns[num2].Text, this.ᜂ[A_0], true) == 0)
							{
								num = 44;
								continue;
							}
							num2++;
							num = 39;
							continue;
						}
						case 18:
							goto IL_37A;
						case 19:
							num = 0;
							continue;
						case 20:
						{
							ExportSource exportSource;
							switch (exportSource)
							{
							case ExportSource.SqlCommand:
								num = 24;
								continue;
							case ExportSource.DataTable:
								num = 32;
								continue;
							default:
								num = 48;
								continue;
							}
							break;
						}
						case 21:
							if (this.ᜂ == null)
							{
								num = 42;
								continue;
							}
							text = null;
							num = 25;
							continue;
						case 22:
							if (text != null)
							{
								num = 34;
								continue;
							}
							return result;
						case 23:
							num = 31;
							continue;
						case 24:
							if (A_2 == null)
							{
								num = 7;
								continue;
							}
							num = 16;
							continue;
						case 25:
						{
							if (this.ᜂ.Count == 0)
							{
								num = 2;
								continue;
							}
							ExportSource exportSource2 = this.ᜃ;
							num = 47;
							continue;
						}
						case 26:
							goto IL_2F1;
						case 27:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_52F;
							default:
								if (false)
								{
								}
								if (dataColumn2 != null)
								{
									num = 33;
									continue;
								}
								return -1;
							}
							break;
						case 28:
							num = 15;
							continue;
						case 29:
							num = 41;
							continue;
						case 30:
							goto IL_134;
						case 31:
						{
							bool flag;
							if (!flag)
							{
								num = 43;
								continue;
							}
							return result;
						}
						case 32:
						{
							if (this.ᜅ == null)
							{
								num = 46;
								continue;
							}
							DataColumn dataColumn = this.ᜅ.Columns[A_0];
							num = 37;
							continue;
						}
						case 33:
							goto IL_609;
						case 34:
						{
							bool flag = false;
							num = 10;
							continue;
						}
						case 35:
							if (!A_1)
							{
								num = 29;
								continue;
							}
							goto IL_2E3;
						case 36:
							goto IL_440;
						case 37:
						{
							DataColumn dataColumn;
							if (dataColumn != null)
							{
								num = 13;
								continue;
							}
							return result;
						}
						case 38:
							if (this.ᜅ == null)
							{
								num = 30;
								continue;
							}
							dataColumn2 = this.ᜅ.Columns[this.ᜂ[A_0]];
							num = 27;
							continue;
						case 39:
							goto IL_4B0;
						case 40:
							goto IL_44E;
						case 41:
							if (A_2.GetFieldType(A_0) == typeof(byte[]))
							{
								num = 23;
								continue;
							}
							goto IL_2E3;
						case 42:
							goto IL_10C;
						case 43:
							goto IL_2E3;
						case 44:
						{
							int num2;
							return num2;
						}
						case 45:
							goto IL_147;
						case 46:
							goto IL_26D;
						case 47:
						{
							ExportSource exportSource2;
							switch (exportSource2)
							{
							case ExportSource.SqlCommand:
								text = (string)spr\u2059.ᜀ(this.ᜂ[A_0], HyperlinksCollectionEditor.b("挟䴡䠣匥䔧䐩戫伭崯圱", a_), A_3);
								num = 14;
								continue;
							case ExportSource.DataTable:
								num = 38;
								continue;
							case ExportSource.ListView:
								goto IL_357;
							default:
								num = 12;
								continue;
							}
							break;
						}
						case 48:
							num = 6;
							continue;
						}
						break;
						IL_2E3:
						result = A_0;
						num = 26;
						continue;
						IL_357:
						num = 1;
						continue;
						IL_440:
						result = A_0;
						num = 40;
						continue;
						IL_4B0:
						num = 8;
					}
				}
				IL_10C:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ⴟ⠡朣䤥䐧弩䄫䀭甯䨱䐳夵䨷丹ػнጿ❁ぃՅ❇♉㥋⍍㹏᱑⅓㭕㩗㽙⹛坝ᙟ͡ᙣ履剧㕩ͫᥭṯ᝱ٳ㍵w੹፻౽삅", a_));
				IL_134:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ⴟ⠡朣䤥䐧弩䄫䀭䌯眱䰳䘵圷䠹䠻н稿ᅁ⅃㉅େ╉⁋㭍㵏㱑ᩓ⍕㕗㡙㥛ⱝ䱟ᑡգᑥ剧偩㍫ŭݯᱱᅳѵ㱷᭹ࡻώ푿", a_));
				IL_147:
				return result;
				IL_1BD:
				throw new ArgumentNullException(HyperlinksCollectionEditor.b("ⴟ⠡朣䤥䐧弩䄫䀭甯䨱䐳夵䨷丹ػнጿ❁ぃՅ❇♉㥋⍍㹏᱑⅓㭕㩗㽙⹛牝䁟ᑡգᑥ剧偩㹫୭ᅯᙱᅳѵ", a_));
				IL_26D:
				if (true)
				{
				}
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ⴟ⠡愣帥堧䔩師娭猯崱堳䌵唷吹伻н稿ᅁ⅃㉅େ╉⁋㭍㵏㱑ᩓ⍕㕗㡙㥛ⱝ䱟ᑡգᑥ剧偩㍫ŭݯᱱᅳѵ㱷᭹ࡻώ푿ꒉ", a_));
				IL_2C2:
				throw new ArgumentException(HyperlinksCollectionEditor.b("挟䴡䠣匥䔧䐩椫嘭䀯崱䘳䈵ȷ9漻嬽㐿Ł⭃⩅㵇❉≋M╏㽑㙓㍕⩗癙ㅛⵝݟ塡幣㑥൧୩࡫୭ɯ剱ݳɵ᥷๹᥻幽ꒃﾋ벑", a_));
				IL_2F1:
				return result;
				IL_37A:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ⴟ⠡愣帥堧䔩師娭猯崱堳䌵唷吹伻н稿ᅁ⅃㉅େ╉⁋㭍㵏㱑ᩓ⍕㕗㡙㥛ⱝ䱟ᑡգᑥ剧偩㍫ŭݯᱱᅳѵ㑷፹ཻ੽홿", a_));
				IL_44E:
				return result;
				IL_52F:
				return -1;
				IL_5AE:
				return A_2.GetOrdinal(text);
				IL_609:
				return dataColumn2.Ordinal;
			}
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0005C63C File Offset: 0x0005B63C
		private void ᜂ(int A_0, IDataReader A_1)
		{
			int a_ = 8;
			ColumnExport columnExport;
			for (;;)
			{
				IL_41:
				columnExport = this[A_0];
				for (;;)
				{
					IL_49:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (A_1 == null)
							{
								num = 4;
								continue;
							}
							num = 7;
							continue;
						case 1:
							goto IL_B1;
						case 2:
							return;
						case 3:
						{
							if (columnExport.Number == -1)
							{
								num = 11;
								continue;
							}
							ExportSource exportSource = this.ᜃ;
							num = 10;
							continue;
						}
						case 4:
							goto IL_188;
						case 5:
							goto IL_105;
						case 6:
							if (this.ᜅ == null)
							{
								num = 5;
								continue;
							}
							goto IL_18A;
						case 7:
							if (A_1.IsClosed)
							{
								num = 9;
								continue;
							}
							goto IL_1AC;
						case 8:
							if (this.ᜆ == null)
							{
								num = 1;
								continue;
							}
							goto IL_1E7;
						case 9:
							goto IL_13C;
						case 10:
						{
							ExportSource exportSource;
							switch (exportSource)
							{
							case ExportSource.SqlCommand:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_49;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case ExportSource.DataTable:
								num = 6;
								continue;
							case ExportSource.ListView:
								num = 8;
								continue;
							default:
								num = 2;
								continue;
							}
							break;
						}
						case 11:
							goto IL_62;
						}
						goto IL_41;
					}
				}
			}
			IL_62:
			throw new Exception(HyperlinksCollectionEditor.b("朣䤥䐧弩䄫䀭䌯眱䰳䘵圷䠹䠻н稿ᅁ⅃㉅େ╉⁋㭍㵏㱑ᩓ㝕㕗㽙灛㍝፟ա幣⽥٧ᱩ൫ɭ᥯ᙱᕳɵᵷ婹ύᅽꢇ曆몓", a_));
			IL_B1:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᙗ㭙ㅛ㭝䱟ᑡգᑥ剧㕩ͫᥭṯ᝱ٳ㩵ᅷॹࡻ⡽", a_));
			IL_105:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᙗ㭙ㅛ㭝䱟ᑡգᑥ剧㕩ͫᥭṯ᝱ٳ㉵᥷๹ᵻ⩽", a_));
			IL_13C:
			throw new ArgumentException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᙗ㭙ㅛ㭝䱟ᑡգᑥ剧㡩५཭ᑯ᝱ٳ", a_));
			IL_188:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᙗ㭙ㅛ㭝䱟ᑡգᑥ剧㡩५཭ᑯ᝱ٳ", a_));
			IL_18A:
			columnExport.Name = this.ᜅ.Columns[columnExport.Number].ColumnName;
			return;
			IL_1AC:
			columnExport.Name = A_1.GetName(columnExport.Number);
			return;
			IL_1E7:
			columnExport.Name = this.ᜆ.Columns[columnExport.Number].Text;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0005C854 File Offset: 0x0005B854
		private void ᜅ(int A_0, IDataReader A_1, DataTable A_2)
		{
			int a_ = 6;
			ColumnExport columnExport;
			bool a_2;
			for (;;)
			{
				columnExport = this[A_0];
				int num = 7;
				for (;;)
				{
					ExportSource exportSource;
					switch (num)
					{
					case 0:
						switch (exportSource)
						{
						case ExportSource.SqlCommand:
							num = 9;
							continue;
						case ExportSource.DataTable:
							num = 11;
							continue;
						case ExportSource.ListView:
							num = 5;
							continue;
						default:
							num = 18;
							continue;
						}
						break;
					case 1:
						if (columnExport.Number == 0)
						{
							num = 8;
							continue;
						}
						columnExport.ColExportType = spr\u2059.ᜁ(this.ᜆ.Items[0].SubItems[columnExport.Number].Text, this.ᜇ.BooleanTrue, this.ᜇ.BooleanFalse);
						num = 12;
						continue;
					case 2:
						goto IL_123;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_341;
						default:
							if (false)
							{
							}
							if (!this.ᜈ)
							{
								num = 6;
								continue;
							}
							num = 16;
							continue;
						}
						break;
					case 4:
						goto IL_85;
					case 5:
						if (this.ᜆ == null)
						{
							num = 2;
							continue;
						}
						num = 3;
						continue;
					case 6:
						goto IL_2EC;
					case 7:
						if (columnExport.Number == -1)
						{
							num = 4;
							continue;
						}
						goto IL_341;
					case 8:
						goto IL_1BD;
					case 9:
						if (A_1 == null)
						{
							num = 15;
							continue;
						}
						num = 13;
						continue;
					case 10:
						goto IL_314;
					case 11:
						if (true)
						{
						}
						if (this.ᜅ == null)
						{
							num = 10;
							continue;
						}
						goto IL_319;
					case 12:
						goto IL_214;
					case 13:
						if (A_1.IsClosed)
						{
							num = 19;
							continue;
						}
						a_2 = false;
						num = 14;
						continue;
					case 14:
						goto IL_2A2;
					case 15:
						goto IL_27C;
					case 16:
						if (this.ᜆ.Items.Count > 0)
						{
							num = 17;
							continue;
						}
						return;
					case 17:
						num = 1;
						continue;
					case 18:
						return;
					case 19:
						goto IL_19A;
					}
					break;
					IL_341:
					exportSource = this.ᜃ;
					num = 0;
				}
			}
			IL_85:
			goto IL_24C;
			IL_123:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⼡⸣攥䜧䘩夫䌭帯䄱焳丵䠷唹主䨽稿硁ᝃ⍅㱇ॉ⍋≍╏㽑㩓ɕ⅗⩙㥛牝ᙟ͡ᙣ履㝧թ᭫mᕯq㡳ή୷๹⩻᝽", a_));
			IL_19A:
			throw new ArgumentException(HyperlinksCollectionEditor.b("⼡⸣攥䜧䘩夫䌭帯䄱焳丵䠷唹主䨽稿硁ᝃ⍅㱇ॉ⍋≍╏㽑㩓ɕ⅗⩙㥛牝ᙟ͡ᙣ履㩧ཀྵ൫੭ᕯq", a_));
			IL_1BD:
			columnExport.ColExportType = spr\u2059.ᜁ(this.ᜆ.Items[0].Text, this.ᜇ.BooleanTrue, this.ᜇ.BooleanFalse);
			return;
			IL_214:
			return;
			IL_24C:
			throw new Exception(HyperlinksCollectionEditor.b("⼡⸣攥䜧䘩夫䌭帯䄱焳丵䠷唹主䨽稿硁ᝃ⍅㱇ॉ⍋≍╏㽑㩓ɕ⅗⩙㥛牝ᙟ͡ᙣ履୧թk᭭ᵯᱱ婳㡵൷᝹ṻ᭽", a_));
			IL_27C:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⼡⸣攥䜧䘩夫䌭帯䄱焳丵䠷唹主䨽稿硁ᝃ⍅㱇ॉ⍋≍╏㽑㩓ɕ⅗⩙㥛牝ᙟ͡ᙣ履㩧ཀྵ൫੭ᕯq", a_));
			IL_2A2:
			try
			{
				a_2 = (bool)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("次圣樥䜧䐩䬫", a_), A_2);
				goto IL_160;
			}
			catch
			{
				a_2 = false;
				goto IL_160;
			}
			goto IL_24C;
			IL_160:
			columnExport.ColExportType = spr\u2059.ᜀ(A_1.GetFieldType(columnExport.Number), a_2);
			return;
			IL_2EC:
			columnExport.ColExportType = ColExportType.String;
			return;
			IL_314:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⼡⸣攥䜧䘩夫䌭帯䄱焳丵䠷唹主䨽稿硁ᝃ⍅㱇ॉ⍋≍╏㽑㩓ɕ⅗⩙㥛牝ᙟ͡ᙣ履㝧թ᭫mᕯqび᝵౷᭹⡻ώ", a_));
			IL_319:
			columnExport.ColExportType = spr\u2059.ᜀ(this.ᜅ.Columns[columnExport.Number].DataType, false);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0005CBE8 File Offset: 0x0005BBE8
		private void ᜅ(int A_0)
		{
			for (;;)
			{
				ColumnExport columnExport = this[A_0];
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						columnExport.Format = this.ᜉ.GetValueByIndex(num2);
						if (true)
						{
						}
						num = 7;
						continue;
					}
					case 1:
						if (columnExport.ColExportType != ColExportType.Unknown)
						{
							num = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_95;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 2:
						num = 3;
						continue;
					case 3:
						goto IL_1DC;
					case 4:
					{
						int num2;
						if (num2 > -1)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 5:
					{
						ColExportType colExportType = columnExport.ColExportType;
						num = 14;
						continue;
					}
					case 6:
						goto IL_1DC;
					case 7:
						return;
					case 8:
						goto IL_1DC;
					case 9:
						if (this.ᜇ != null)
						{
							num = 5;
							continue;
						}
						goto IL_1DC;
					case 10:
					{
						int num2 = this.ᜉ.IndexOfName(columnExport.Name);
						num = 4;
						continue;
					}
					case 11:
						return;
					case 12:
						if (this.ᜉ != null)
						{
							num = 10;
							continue;
						}
						return;
					case 13:
						goto IL_1DC;
					case 14:
					{
						ColExportType colExportType;
						switch (colExportType)
						{
						case ColExportType.Integer:
						case ColExportType.Bigint:
							columnExport.Format = this.ᜇ.Integer;
							num = 6;
							continue;
						case ColExportType.Float:
							columnExport.Format = this.ᜇ.Float;
							num = 15;
							continue;
						case ColExportType.Currency:
							goto IL_95;
						case ColExportType.DateTime:
							columnExport.Format = this.ᜇ.DateTime;
							num = 8;
							continue;
						case ColExportType.Time:
							columnExport.Format = this.ᜇ.Time;
							num = 16;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					}
					case 15:
						goto IL_1DC;
					case 16:
						goto IL_1DC;
					}
					break;
					IL_95:
					columnExport.Format = this.ᜇ.Currency;
					num = 13;
					continue;
					IL_1DC:
					num = 12;
				}
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0005CE20 File Offset: 0x0005BE20
		private void ᜁ(int A_0, IDataReader A_1)
		{
			int a_ = 1;
			ColumnExport columnExport;
			int num2;
			for (;;)
			{
				columnExport = this[A_0];
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1BF;
					case 1:
						if (num2 > -1)
						{
							num = 13;
							continue;
						}
						goto IL_17A;
					case 2:
						goto IL_EB;
					case 3:
					{
						if (columnExport.Number == -1)
						{
							num = 10;
							continue;
						}
						ExportSource exportSource = this.ᜃ;
						num = 11;
						continue;
					}
					case 4:
						if (A_1 == null)
						{
							num = 6;
							continue;
						}
						num = 14;
						continue;
					case 5:
						num2 = this.ᜋ.IndexOfName(columnExport.Name);
						num = 1;
						continue;
					case 6:
						goto IL_254;
					case 7:
						if (this.ᜅ == null)
						{
							num = 12;
							continue;
						}
						goto IL_104;
					case 8:
						return;
					case 9:
						if (this.ᜋ == null)
						{
							goto IL_17A;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_98;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 10:
						goto IL_199;
					case 11:
					{
						ExportSource exportSource;
						switch (exportSource)
						{
						case ExportSource.SqlCommand:
							num = 4;
							continue;
						case ExportSource.DataTable:
							goto IL_98;
						case ExportSource.ListView:
							num = 15;
							continue;
						default:
							num = 8;
							continue;
						}
						break;
					}
					case 12:
						goto IL_B6;
					case 13:
						goto IL_202;
					case 14:
						if (true)
						{
						}
						if (A_1.IsClosed)
						{
							num = 2;
							continue;
						}
						goto IL_13F;
					case 15:
						if (this.ᜆ == null)
						{
							num = 0;
							continue;
						}
						goto IL_26D;
					}
					break;
					IL_98:
					num = 7;
					continue;
					IL_17A:
					num = 3;
				}
			}
			IL_B6:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("လᔞ戠䰢䤤刦䐨䔪帬樮䤰䌲娴䔶䴸ĺܼ氾⑀㝂ل⡆╈㹊⁌ⅎቐ㉒╔⍖じ㑚㍜獞ᝠɢᝤ嵦㙨ѪᩬŮᑰŲㅴᙶ൸᩺⥼Ṿ", a_));
			IL_EB:
			throw new ArgumentException(HyperlinksCollectionEditor.b("လᔞ戠䰢䤤刦䐨䔪帬樮䤰䌲娴䔶䴸ĺܼ氾⑀㝂ل⡆╈㹊⁌ⅎቐ㉒╔⍖じ㑚㍜獞ᝠɢᝤ嵦㭨๪౬୮ᑰŲ", a_));
			IL_104:
			columnExport.Caption = this.ᜅ.Columns[this[A_0].Number].Caption;
			return;
			IL_13F:
			columnExport.Caption = A_1.GetName(columnExport.Number);
			return;
			IL_199:
			throw new Exception(HyperlinksCollectionEditor.b("လᔞ戠䰢䤤刦䐨䔪帬樮䤰䌲娴䔶䴸ĺܼ氾⑀㝂ل⡆╈㹊⁌ⅎቐ㉒╔⍖じ㑚㍜獞ᝠɢᝤ嵦੨ѪŬᩮᱰᵲ孴㥶౸ᙺὼ᩾", a_));
			IL_1BF:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("လᔞ戠䰢䤤刦䐨䔪帬樮䤰䌲娴䔶䴸ĺܼ氾⑀㝂ل⡆╈㹊⁌ⅎቐ㉒╔⍖じ㑚㍜獞ᝠɢᝤ嵦㙨ѪᩬŮᑰŲ㥴Ṷ੸ེ⭼ᙾ", a_));
			IL_202:
			columnExport.Caption = this.ᜋ.GetValueByIndex(num2);
			return;
			IL_254:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("လᔞ戠䰢䤤刦䐨䔪帬樮䤰䌲娴䔶䴸ĺܼ氾⑀㝂ل⡆╈㹊⁌ⅎቐ㉒╔⍖じ㑚㍜獞ᝠɢᝤ嵦㭨๪౬୮ᑰŲ", a_));
			IL_26D:
			columnExport.Caption = this.ᜆ.Columns[this[A_0].Number].Text;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0005D0C4 File Offset: 0x0005C0C4
		private void ᜄ(int A_0)
		{
			switch (0)
			{
			default:
			{
				ColumnExport columnExport;
				for (;;)
				{
					columnExport = this[A_0];
					int num = 1;
					for (;;)
					{
						int num2;
						char c;
						switch (num)
						{
						case 0:
							goto IL_12F;
						case 1:
							if (this.ᜌ != null)
							{
								num = 4;
								continue;
							}
							return;
						case 2:
							goto IL_209;
						case 3:
							num = 5;
							continue;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12F;
							default:
								if (false)
								{
								}
								num2 = this.ᜌ.IndexOfName(columnExport.Name);
								num = 9;
								continue;
							}
							break;
						case 5:
							if (c != 'L')
							{
								num = 10;
								continue;
							}
							goto IL_1C2;
						case 6:
						{
							ColExportType colExportType;
							switch (colExportType)
							{
							case ColExportType.Integer:
							case ColExportType.Bigint:
							case ColExportType.Float:
							case ColExportType.Currency:
								goto IL_127;
							case ColExportType.DateTime:
							case ColExportType.Time:
							case ColExportType.String:
								goto IL_16A;
							case ColExportType.Boolean:
								goto IL_1AC;
							default:
								num = 12;
								continue;
							}
							break;
						}
						case 7:
							goto IL_17D;
						case 8:
							num = 2;
							continue;
						case 9:
							if (num2 > -1)
							{
								num = 0;
								continue;
							}
							num = 14;
							continue;
						case 10:
							num = 11;
							continue;
						case 11:
							if (c != 'R')
							{
								num = 8;
								continue;
							}
							goto IL_182;
						case 12:
							if (true)
							{
							}
							num = 16;
							continue;
						case 13:
							return;
						case 14:
						{
							if (columnExport.ColExportType == ColExportType.Unknown)
							{
								num = 13;
								continue;
							}
							ColExportType colExportType = columnExport.ColExportType;
							num = 6;
							continue;
						}
						case 15:
							if (c != 'C')
							{
								num = 3;
								continue;
							}
							goto IL_20E;
						case 16:
							goto IL_16A;
						}
						break;
						IL_12F:
						c = this.ᜌ.GetValueByIndex(num2).ToUpper()[0];
						num = 15;
						continue;
						IL_16A:
						columnExport.ColAlign = ColumAlign.Left;
						num = 7;
					}
				}
				IL_127:
				columnExport.ColAlign = ColumAlign.Right;
				return;
				IL_17D:
				return;
				IL_182:
				columnExport.ColAlign = ColumAlign.Right;
				return;
				IL_1AC:
				columnExport.ColAlign = ColumAlign.Center;
				return;
				IL_1C2:
				columnExport.ColAlign = ColumAlign.Left;
				return;
				IL_209:
				columnExport.ColAlign = ColumAlign.Left;
				return;
				IL_20E:
				columnExport.ColAlign = ColumAlign.Center;
				return;
			}
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0005D314 File Offset: 0x0005C314
		private void ᜄ(int A_0, IDataReader A_1, DataTable A_2)
		{
			int a_ = 13;
			switch (0)
			{
			default:
				for (;;)
				{
					ColumnExport columnExport = this[A_0];
					int num = 20;
					for (;;)
					{
						ExportSource exportSource;
						switch (num)
						{
						case 0:
						{
							int num2;
							if (num2 > -1)
							{
								num = 2;
								continue;
							}
							goto IL_1E0;
						}
						case 1:
							goto IL_4B0;
						case 2:
							try
							{
								int num2;
								columnExport.Length = Convert.ToInt32(this.\u170D.GetValueByIndex(num2));
								return;
							}
							catch
							{
								columnExport.Length = 255;
								return;
							}
							goto IL_2B5;
						case 3:
						{
							DataColumn dataColumn;
							columnExport.Length = dataColumn.MaxLength;
							num = 9;
							continue;
						}
						case 4:
							columnExport.Length = 0;
							num = 22;
							continue;
						case 5:
							goto IL_4B0;
						case 6:
							if (columnExport.ColExportType != ColExportType.Guid)
							{
								num = 25;
								continue;
							}
							goto IL_3ED;
						case 7:
						{
							if (this.ᜅ == null)
							{
								num = 28;
								continue;
							}
							DataColumn dataColumn = this.ᜅ.Columns[columnExport.Number];
							num = 39;
							continue;
						}
						case 8:
							if (this.\u170D != null)
							{
								num = 23;
								continue;
							}
							goto IL_1E0;
						case 9:
							goto IL_4B0;
						case 10:
						{
							if (A_1.IsClosed)
							{
								num = 35;
								continue;
							}
							bool flag = false;
							num = 42;
							continue;
						}
						case 11:
							goto IL_4B0;
						case 12:
							switch (exportSource)
							{
							case ExportSource.SqlCommand:
								goto IL_2B5;
							case ExportSource.DataTable:
								columnExport.Length = 255;
								num = 7;
								continue;
							case ExportSource.ListView:
								columnExport.Length = 255;
								num = 11;
								continue;
							default:
								num = 18;
								continue;
							}
							break;
						case 13:
							goto IL_153;
						case 14:
						{
							bool flag;
							if (flag)
							{
								num = 24;
								continue;
							}
							num = 41;
							continue;
						}
						case 15:
							num = 21;
							continue;
						case 16:
							if (columnExport.ColExportType == ColExportType.Guid)
							{
								num = 13;
								continue;
							}
							columnExport.Length = 255;
							num = 19;
							continue;
						case 17:
							goto IL_345;
						case 18:
							num = 5;
							continue;
						case 19:
							goto IL_4B0;
						case 20:
							if (!columnExport.IsString)
							{
								num = 29;
								continue;
							}
							goto IL_3ED;
						case 21:
							if (this.ᜀ is DBFExport)
							{
								num = 26;
								continue;
							}
							return;
						case 22:
							goto IL_247;
						case 23:
						{
							int num2 = this.\u170D.IndexOfName(columnExport.Name);
							num = 0;
							continue;
						}
						case 24:
						{
							int num3;
							columnExport.Length = num3;
							num = 37;
							continue;
						}
						case 25:
							num = 33;
							continue;
						case 26:
							num = 30;
							continue;
						case 27:
							try
							{
								int num3 = (int)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("樨䐪䄬娮尰崲昴帶䌸帺", a_), A_2);
								goto IL_12F;
							}
							catch
							{
								int num3 = 0;
								goto IL_12F;
							}
							goto IL_4B0;
							IL_12F:
							num = 14;
							continue;
						case 28:
							goto IL_12D;
						case 29:
							num = 6;
							continue;
						case 30:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_153;
							default:
								if (false)
								{
								}
								if ((this.ᜀ as DBFExport).AutoFitColWidth)
								{
									num = 4;
									continue;
								}
								return;
							}
							break;
						case 31:
							goto IL_2D3;
						case 32:
							goto IL_4B0;
						case 33:
							if (columnExport.ColExportType != ColExportType.Binary)
							{
								num = 40;
								continue;
							}
							goto IL_3ED;
						case 34:
							goto IL_4B0;
						case 35:
							goto IL_3D7;
						case 36:
							if (A_1 == null)
							{
								num = 31;
								continue;
							}
							if (true)
							{
							}
							num = 10;
							continue;
						case 37:
							goto IL_4B0;
						case 38:
							if (this.ᜀ != null)
							{
								num = 15;
								continue;
							}
							return;
						case 39:
						{
							DataColumn dataColumn;
							if (dataColumn.MaxLength != -1)
							{
								num = 3;
								continue;
							}
							num = 16;
							continue;
						}
						case 40:
							return;
						case 41:
						{
							if (columnExport.ColExportType == ColExportType.Guid)
							{
								num = 17;
								continue;
							}
							int num3;
							columnExport.Length = Math.Min(num3, 255);
							num = 32;
							continue;
						}
						case 42:
						{
							try
							{
								bool flag = (bool)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("怨堪愬䀮弰吲", a_), A_2);
								goto IL_4D8;
							}
							catch
							{
								bool flag = false;
								goto IL_4D8;
							}
							goto IL_345;
							IL_4D8:
							int num3 = 0;
							num = 27;
							continue;
						}
						}
						break;
						IL_153:
						columnExport.Length = 36;
						num = 1;
						continue;
						IL_1E0:
						exportSource = this.ᜃ;
						num = 12;
						continue;
						IL_2B5:
						num = 36;
						continue;
						IL_345:
						columnExport.Length = 36;
						num = 34;
						continue;
						IL_3ED:
						num = 8;
						continue;
						IL_4B0:
						num = 38;
					}
				}
				IL_12D:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("␨K測䀮崰䘲場夶䨸縺䔼伾⹀ㅂㅄ絆獈煊Ṍ⩎═ၒ㩔㭖ⱘ㙚㍜፞Ѡൢɤ፦Ũ䝪䵬᥮ၰŲ佴᩶♸ᑺ੼ᅾ솄ﶈ\ud98cﾒ", a_));
				IL_247:
				return;
				IL_2D3:
				throw new ArgumentNullException(HyperlinksCollectionEditor.b("␨K測䀮崰䘲場夶䨸縺䔼伾⹀ㅂㅄ絆獈ᡊ⡌㭎ቐ㱒㥔≖㑘㕚ᅜ㩞འѢᅤས䕨ᵪ౬ᵮ䭰ⅲၴᙶᵸṺོ", a_));
				IL_3D7:
				throw new ArgumentException(HyperlinksCollectionEditor.b("␨K測䀮崰䘲場夶䨸縺䔼伾⹀ㅂㅄ絆獈ᡊ⡌㭎ቐ㱒㥔≖㑘㕚ᅜ㩞འѢᅤས䕨ᵪ౬ᵮ䭰ⅲၴᙶᵸṺོ", a_));
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0005D8F0 File Offset: 0x0005C8F0
		private void ᜃ(int A_0, IDataReader A_1, DataTable A_2)
		{
			int a_ = 14;
			for (;;)
			{
				ColumnExport columnExport = this[A_0];
				ExportSource exportSource = this.ᜃ;
				if (true)
				{
				}
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
							goto IL_AE;
						default:
							goto IL_6C;
						}
						break;
					case 1:
						switch (exportSource)
						{
						case ExportSource.SqlCommand:
							goto IL_74;
						case ExportSource.DataTable:
						case ExportSource.ListView:
							goto IL_AE;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						return;
					}
					break;
					IL_AE:
					columnExport.Size = 0L;
					num = 2;
				}
			}
			IL_6C:
			if (false)
			{
			}
			return;
			try
			{
				IL_74:
				ColumnExport columnExport;
				columnExport.Size = (long)((int)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("椩䌫䈭䔯弱娳攵儷䀹夻", a_), A_2));
				return;
			}
			catch
			{
				ColumnExport columnExport;
				columnExport.Size = 0L;
				return;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0005D9DC File Offset: 0x0005C9DC
		private void ᜀ(int A_0, IDataReader A_1)
		{
			int a_ = 16;
			ColumnExport columnExport;
			for (;;)
			{
				columnExport = this[A_0];
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 > -1)
						{
							num = 9;
							continue;
						}
						goto IL_167;
					}
					case 1:
					{
						int num2 = this.ᜊ.IndexOfName(columnExport.Name);
						num = 0;
						continue;
					}
					case 2:
						goto IL_144;
					case 3:
						if (A_1.IsClosed)
						{
							num = 8;
							continue;
						}
						goto IL_98;
					case 4:
						goto IL_162;
					case 5:
						if (A_1 == null)
						{
							num = 4;
							continue;
						}
						num = 3;
						continue;
					case 6:
						goto IL_186;
					case 7:
					{
						if (columnExport.Number == -1)
						{
							num = 6;
							continue;
						}
						ExportSource exportSource = this.ᜃ;
						num = 13;
						continue;
					}
					case 8:
						goto IL_286;
					case 9:
						goto IL_213;
					case 10:
						if (this.ᜆ == null)
						{
							num = 2;
							continue;
						}
						goto IL_28B;
					case 11:
						if (this.ᜊ == null)
						{
							goto IL_167;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_98;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 12:
						goto IL_256;
					case 13:
					{
						ExportSource exportSource;
						switch (exportSource)
						{
						case ExportSource.SqlCommand:
							num = 5;
							continue;
						case ExportSource.DataTable:
							num = 15;
							continue;
						case ExportSource.ListView:
							num = 10;
							continue;
						default:
							num = 14;
							continue;
						}
						break;
					}
					case 14:
						goto IL_1E2;
					case 15:
						if (this.ᜅ == null)
						{
							num = 12;
							continue;
						}
						goto IL_C5;
					}
					break;
					IL_167:
					num = 7;
				}
			}
			IL_98:
			columnExport.Width = A_1.GetName(columnExport.Number).Length;
			return;
			IL_C5:
			columnExport.Width = this.ᜅ.Columns[columnExport.Number].ColumnName.Length;
			return;
			IL_10F:
			throw new ArgumentException(HyperlinksCollectionEditor.b("Å␭猯崱堳䌵唷吹伻笽㠿㉁⭃㑅㱇灉癋ᵍ㕏♑ᝓ㥕㑗⽙ㅛそ㝟ୡcብg䙩ᩫ཭ɯ䡱♳፵᥷ṹ᥻౽", a_));
			IL_144:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("Å␭猯崱堳䌵唷吹伻笽㠿㉁⭃㑅㱇灉癋ᵍ㕏♑ᝓ㥕㑗⽙ㅛそ㝟ୡcብg䙩ᩫ཭ɯ䡱⭳᥵ཷᑹ᥻౽챿\ude87轢", a_));
			IL_162:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("Å␭猯崱堳䌵唷吹伻笽㠿㉁⭃㑅㱇灉癋ᵍ㕏♑ᝓ㥕㑗⽙ㅛそ㝟ୡcብg䙩ᩫ཭ɯ䡱♳፵᥷ṹ᥻౽", a_));
			IL_186:
			throw new Exception(HyperlinksCollectionEditor.b("Å␭猯崱堳䌵唷吹伻笽㠿㉁⭃㑅㱇灉癋ᵍ㕏♑ᝓ㥕㑗⽙ㅛそ㝟ୡcብg䙩ᩫ཭ɯ䡱ᝳ᥵ᑷཹᅻၽ깿첁ﺋ", a_));
			IL_1E2:
			if (true)
			{
			}
			return;
			IL_213:
			try
			{
				int num2;
				columnExport.Width = Convert.ToInt32(this.ᜊ.GetValueByIndex(num2));
				return;
			}
			catch
			{
				columnExport.Width = 0;
				return;
			}
			goto IL_10F;
			IL_256:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("Å␭猯崱堳䌵唷吹伻笽㠿㉁⭃㑅㱇灉癋ᵍ㕏♑ᝓ㥕㑗⽙ㅛそ㝟ୡcብg䙩ᩫ཭ɯ䡱⭳᥵ཷᑹ᥻౽쑿\udc87", a_));
			IL_286:
			goto IL_10F;
			IL_28B:
			columnExport.Width = this.ᜆ.Columns[columnExport.Number].Width / spr\u2059.ᜀ(this.ᜆ, HyperlinksCollectionEditor.b("琫", a_));
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0005DCC0 File Offset: 0x0005CCC0
		private void ᜂ(int A_0, IDataReader A_1, DataTable A_2)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				ColumnExport columnExport;
				bool flag;
				int a_2;
				for (;;)
				{
					if (true)
					{
					}
					columnExport = this[A_0];
					int num = 8;
					for (;;)
					{
						ExportSource exportSource;
						switch (num)
						{
						case 0:
							goto IL_260;
						case 1:
							goto IL_DA;
						case 2:
							if (A_1.IsClosed)
							{
								num = 7;
								continue;
							}
							flag = false;
							num = 4;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1EB;
							default:
								goto IL_106;
							}
							break;
						case 4:
							goto IL_298;
						case 5:
							goto IL_8A;
						case 6:
							if (flag)
							{
								num = 9;
								continue;
							}
							goto IL_178;
						case 7:
							goto IL_1E9;
						case 8:
							if (columnExport.Number == -1)
							{
								num = 5;
								continue;
							}
							goto IL_1EB;
						case 9:
						{
							DataColumn dataColumn;
							a_2 = dataColumn.MaxLength;
							num = 12;
							continue;
						}
						case 10:
							switch (exportSource)
							{
							case ExportSource.SqlCommand:
								num = 14;
								continue;
							case ExportSource.DataTable:
								num = 11;
								continue;
							case ExportSource.ListView:
								goto IL_2CB;
							default:
								num = 3;
								continue;
							}
							break;
						case 11:
						{
							if (this.ᜅ == null)
							{
								num = 0;
								continue;
							}
							DataColumn dataColumn = this.ᜅ.Columns[columnExport.Number];
							flag = (dataColumn.MaxLength > 255);
							num = 6;
							continue;
						}
						case 12:
							goto IL_120;
						case 13:
							try
							{
								a_2 = (int)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("崝伟両儣䬥䘧礩䔫吭唯", a_), A_2);
								goto IL_8F;
							}
							catch
							{
								a_2 = 0;
								goto IL_8F;
							}
							goto Block_8;
						case 14:
							if (A_1 == null)
							{
								num = 1;
								continue;
							}
							num = 2;
							continue;
						}
						break;
						Block_8:
						try
						{
							IL_298:
							flag = (bool)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("圝匟渡䬣䠥伧", a_), A_2);
							goto IL_A9;
						}
						catch
						{
							flag = false;
							goto IL_A9;
						}
						goto IL_2CB;
						IL_A9:
						a_2 = 0;
						num = 13;
						continue;
						IL_1EB:
						exportSource = this.ᜃ;
						num = 10;
					}
				}
				IL_8A:
				throw new Exception(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнጿ❁ぃՅ❇♉㥋⍍㹏őՓᩕ౗⍙ⱛ㭝䱟ᑡգᑥ剧३ͫɭկάᩳ塵㙷ཹᅻᱽ", a_));
				IL_8F:
				columnExport.SQLType = spr\u2059.ᜀ(A_1.GetFieldType(columnExport.Number), flag, a_2);
				return;
				IL_DA:
				throw new ArgumentNullException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнጿ❁ぃՅ❇♉㥋⍍㹏őՓᩕ౗⍙ⱛ㭝䱟ᑡգᑥ剧㡩५཭ᑯ᝱ٳ", a_));
				IL_106:
				if (false)
				{
				}
				return;
				IL_120:
				IL_178:
				columnExport.SQLType = spr\u2059.ᜀ(this.ᜅ.Columns[columnExport.Number].DataType, false, 0);
				return;
				IL_1E9:
				throw new ArgumentException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнጿ❁ぃՅ❇♉㥋⍍㹏őՓᩕ౗⍙ⱛ㭝䱟ᑡգᑥ剧㡩५཭ᑯ᝱ٳ", a_));
				IL_260:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнጿ❁ぃՅ❇♉㥋⍍㹏őՓᩕ౗⍙ⱛ㭝䱟ᑡգᑥ剧㕩ͫᥭṯ᝱ٳ㉵᥷๹ᵻ⩽", a_));
				IL_2CB:
				spr\u2059.ᜀ(columnExport);
				return;
			}
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0005DFBC File Offset: 0x0005CFBC
		private void ᜃ(int A_0)
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
			ColumnExport columnExport = this[A_0];
			columnExport.AllowFormat = (columnExport.ColExportType == ColExportType.Integer || columnExport.ColExportType == ColExportType.Bigint || columnExport.ColExportType == ColExportType.Currency || columnExport.ColExportType == ColExportType.Float || columnExport.ColExportType == ColExportType.DateTime || columnExport.ColExportType == ColExportType.Time);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0005E04C File Offset: 0x0005D04C
		private void ᜂ(int A_0)
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
			ColumnExport columnExport = this[A_0];
			columnExport.IsNumeric = (columnExport.ColExportType == ColExportType.Integer || columnExport.ColExportType == ColExportType.Bigint || columnExport.ColExportType == ColExportType.Currency || columnExport.ColExportType == ColExportType.Float);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0005E0C4 File Offset: 0x0005D0C4
		private void ᜁ(int A_0)
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
			ColumnExport columnExport = this[A_0];
			columnExport.IsString = (columnExport.ColExportType == ColExportType.String);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0005E118 File Offset: 0x0005D118
		private void ᜀ(int A_0)
		{
			for (;;)
			{
				ColumnExport columnExport = this[A_0];
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_2C;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						default:
							if (false)
							{
							}
							if (this.ᜎ != null)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 4:
						columnExport.NotTruncatable = (this.ᜎ.IndexOf(columnExport.Name) > -1);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
					IL_2C:
					if (columnExport.ColExportType != ColExportType.String)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
				}
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0005E1D4 File Offset: 0x0005D1D4
		private void ᜁ(int A_0, IDataReader A_1, DataTable A_2)
		{
			int a_ = 8;
			ColumnExport columnExport;
			bool flag;
			for (;;)
			{
				if (true)
				{
				}
				columnExport = this[A_0];
				ExportSource exportSource = this.ᜃ;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B5:
					num = 6;
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
					case 0:
						switch (exportSource)
						{
						case ExportSource.SqlCommand:
							num = 10;
							continue;
						case ExportSource.DataTable:
							num = 4;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_1C1;
					case 2:
						num = 3;
						continue;
					case 3:
						goto IL_A2;
					case 4:
						if (this.ᜅ == null)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					case 5:
						goto IL_1A0;
					case 6:
						goto IL_C0;
					case 7:
						try
						{
							flag = (bool)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("洣唥搧䔩䈫䤭", a_), A_2);
						}
						catch
						{
							flag = false;
						}
						num = 11;
						continue;
					case 8:
						goto IL_FE;
					case 9:
						if (A_1.IsClosed)
						{
							num = 8;
							continue;
						}
						flag = false;
						num = 7;
						continue;
					case 10:
						if (A_1 == null)
						{
							goto IL_B5;
						}
						num = 9;
						continue;
					case 11:
						goto IL_15B;
					}
					break;
				}
			}
			IL_A2:
			columnExport.IsBlob = false;
			return;
			IL_C0:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᅗ⥙ṛ㉝ཟa䡣ၥ१ᡩ噫㱭ᕯ፱ၳ፵੷", a_));
			IL_FE:
			throw new ArgumentException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᅗ⥙ṛ㉝ཟa䡣ၥ१ᡩ噫㱭ᕯ፱ၳ፵੷", a_));
			IL_15B:
			columnExport.IsBlob = (A_1.GetFieldType(columnExport.Number) == typeof(byte[]) && flag);
			return;
			IL_1A0:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("⤣Ⱕ欧䔩䀫嬭崯就䜳猵䀷䨹医䰽㐿硁繃ᕅⵇ㹉ཋ⅍㱏❑㥓㡕ᅗ⥙ṛ㉝ཟa䡣ၥ१ᡩ噫ㅭὯձᩳ፵੷㹹ᵻ੽횁", a_));
			IL_1C1:
			columnExport.IsBlob = (this.ᜅ.Columns[columnExport.Number].DataType == typeof(byte[]) && columnExport.Length > 255);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0005E3FC File Offset: 0x0005D3FC
		private void ᜀ(int A_0, IDataReader A_1, DataTable A_2)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				ColumnExport columnExport;
				DataColumn dataColumn;
				for (;;)
				{
					columnExport = this[A_0];
					ExportSource exportSource = this.ᜃ;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜅ == null)
							{
								num = 4;
								continue;
							}
							dataColumn = this.ᜅ.Columns[columnExport.Number];
							num = 5;
							continue;
						case 1:
							try
							{
								bool flag = (bool)spr\u2059.ᜀ(A_1.GetName(columnExport.Number), HyperlinksCollectionEditor.b("唛洝氟䴡䨣䄥", a_), A_2);
								goto IL_9B;
							}
							catch
							{
								bool flag = false;
								goto IL_9B;
							}
							goto IL_1EA;
							IL_9B:
							num = 3;
							continue;
						case 2:
							switch (exportSource)
							{
							case ExportSource.SqlCommand:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_96;
								default:
								{
									if (false)
									{
									}
									bool flag = false;
									num = 1;
									continue;
								}
								}
								break;
							case ExportSource.DataTable:
								num = 0;
								continue;
							default:
								num = 9;
								continue;
							}
							break;
						case 3:
						{
							bool flag;
							columnExport.IsMemo = (A_1.GetFieldType(columnExport.Number) == typeof(string) && (flag || columnExport.Size > 255L));
							num = 6;
							continue;
						}
						case 4:
							goto IL_96;
						case 5:
							goto IL_F3;
						case 6:
							return;
						case 7:
							goto IL_1AC;
						case 8:
							goto IL_14A;
						case 9:
							if (true)
							{
							}
							num = 7;
							continue;
						}
						break;
						IL_96:
						num = 8;
					}
				}
				IL_F3:
				columnExport.IsMemo = (dataColumn.DataType == typeof(string) && columnExport.Length > 255);
				return;
				IL_14A:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ᄛᐝ嘟䌡嘣ᰥ眧䔩嬫䀭唯䀱瀳圵䰷嬹栻弽∿⹁⅃橅桇❉㡋⩍橏őㅓ≕᭗㕙せ⭝ൟౡⵣᕥ╧ཀྵūŭ屯剱ᝳ᩵୷䁹⵻㭽ﲇ즉ﾑ望", a_));
				IL_1AC:
				IL_1EA:
				columnExport.IsMemo = false;
				return;
			}
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0005E60C File Offset: 0x0005D60C
		public void Fill(bool exportLongColumn)
		{
			int a_ = 2;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					this.ᜀ();
					int num2 = 0;
					for (;;)
					{
						DataTable dataTable;
						switch (num2)
						{
						case 0:
							if (this.ᜂ == null)
							{
								num2 = 9;
								continue;
							}
							dataTable = null;
							num2 = 5;
							continue;
						case 1:
							if (this.ᜄ.Connection.State == ConnectionState.Closed)
							{
								num2 = 8;
								continue;
							}
							goto IL_98;
						case 2:
							goto IL_11E;
						case 3:
							goto IL_98;
						case 4:
							Monitor.Enter(this);
							num2 = 1;
							continue;
						case 5:
							if (this.ᜃ == ExportSource.SqlCommand)
							{
								num2 = 4;
								continue;
							}
							goto IL_11E;
						case 6:
							if (this.ᜀ is ExportBase)
							{
								num2 = 7;
								continue;
							}
							goto IL_11E;
						case 7:
							(this.ᜀ as ExportBase).SQLCommandSchema = dataTable;
							num2 = 2;
							continue;
						case 8:
							goto IL_592;
						case 9:
							goto IL_74;
						}
						break;
						IL_98:
						if (true)
						{
						}
						spr\u2059.ᜀ = this.ᜄ.ExecuteReader(CommandBehavior.SchemaOnly);
						dataTable = spr\u2059.ᜀ.GetSchemaTable();
						num2 = 6;
						continue;
						IL_592:
						this.ᜄ.Connection.Open();
						num2 = 3;
						continue;
						try
						{
							IL_11E:
							num2 = 26;
							for (;;)
							{
								int num4;
								switch (num2)
								{
								case 0:
									goto IL_2D6;
								case 1:
									if (this.ᜅ == null)
									{
										num2 = 12;
										continue;
									}
									num = this.ᜅ.Columns.Count;
									num2 = 21;
									continue;
								case 2:
								{
									ColumnExport columnExport = this.Add(new ColumnExport(this));
									int num3;
									columnExport.Number = num3;
									num2 = 25;
									continue;
								}
								case 3:
									if (spr\u2059.ᜀ.IsClosed)
									{
										num2 = 5;
										continue;
									}
									num = spr\u2059.ᜀ.FieldCount;
									num2 = 6;
									continue;
								case 4:
									goto IL_376;
								case 5:
									goto IL_295;
								case 6:
									goto IL_376;
								case 7:
									goto IL_540;
								case 8:
									if (num4 < num)
									{
										int num3 = this.ᜀ(num4, exportLongColumn, spr\u2059.ᜀ, dataTable);
										num2 = 14;
										continue;
									}
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_354;
									default:
										if (false)
										{
										}
										num2 = 28;
										continue;
									}
									break;
								case 9:
									goto IL_1EA;
								case 10:
								{
									int num5;
									if (num5 >= base.Count)
									{
										num2 = 17;
										continue;
									}
									this.ᜂ(num5, spr\u2059.ᜀ);
									this.ᜅ(num5, spr\u2059.ᜀ, dataTable);
									this.ᜁ(num5, spr\u2059.ᜀ);
									this.ᜅ(num5);
									this.ᜄ(num5);
									this.ᜂ(num5, spr\u2059.ᜀ, dataTable);
									this.ᜁ(num5);
									this.ᜃ(num5);
									this.ᜂ(num5);
									this.ᜀ(num5, spr\u2059.ᜀ);
									this.ᜄ(num5, spr\u2059.ᜀ, dataTable);
									this.ᜃ(num5, spr\u2059.ᜀ, dataTable);
									this.ᜁ(num5, spr\u2059.ᜀ, dataTable);
									this.ᜀ(num5, spr\u2059.ᜀ, dataTable);
									this.ᜀ(num5);
									num5++;
									num2 = 23;
									continue;
								}
								case 11:
									goto IL_376;
								case 12:
									goto IL_2D1;
								case 13:
									num2 = 4;
									continue;
								case 14:
								{
									int num3;
									if (num3 != -1)
									{
										num2 = 2;
										continue;
									}
									goto IL_3C9;
								}
								case 15:
								{
									ExportSource exportSource = this.ᜃ;
									num2 = 24;
									continue;
								}
								case 16:
									goto IL_376;
								case 17:
									num2 = 7;
									continue;
								case 18:
									goto IL_3C4;
								case 19:
									goto IL_2D6;
								case 20:
									goto IL_360;
								case 21:
									goto IL_376;
								case 22:
									if (this.ᜆ == null)
									{
										goto IL_354;
									}
									num = this.ᜆ.Columns.Count;
									num2 = 16;
									continue;
								case 23:
									goto IL_1EA;
								case 24:
								{
									ExportSource exportSource;
									switch (exportSource)
									{
									case ExportSource.SqlCommand:
										num2 = 27;
										continue;
									case ExportSource.DataTable:
										num2 = 1;
										continue;
									case ExportSource.ListView:
										num2 = 22;
										continue;
									default:
										num2 = 13;
										continue;
									}
									break;
								}
								case 25:
									goto IL_3C9;
								case 27:
									if (spr\u2059.ᜀ == null)
									{
										num2 = 18;
										continue;
									}
									num2 = 3;
									continue;
								case 28:
								{
									int num5 = 0;
									num2 = 9;
									continue;
								}
								}
								if (this.ᜂ.Count == 0)
								{
									num2 = 15;
									continue;
								}
								num = this.ᜂ.Count;
								num2 = 11;
								continue;
								IL_1EA:
								num2 = 10;
								continue;
								IL_2D6:
								num2 = 8;
								continue;
								IL_354:
								num2 = 20;
								continue;
								IL_376:
								num4 = 0;
								num2 = 19;
								continue;
								IL_3C9:
								num4++;
								num2 = 0;
							}
							IL_295:
							throw new ArgumentException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнؿ⭁⡃⩅摇㱉ⵋ㱍橏ᝑⱓ♕㝗⡙⡛ᵝཟཡॣ॥٧䑩⡫཭ѯ፱♳፵᥷ṹ᥻౽", a_));
							IL_2D1:
							throw new NullReferenceException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнؿ⭁⡃⩅摇㱉ⵋ㱍橏൑㭓⅕㙗㽙⹛ᩝşᙡգ㉥१ࡩk୭", a_));
							IL_360:
							throw new NullReferenceException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнؿ⭁⡃⩅摇㱉ⵋ㱍橏൑㭓⅕㙗㽙⹛ቝय़ᅡၣづŧཀྵ᭫", a_));
							IL_3C4:
							throw new ArgumentNullException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнؿ⭁⡃⩅摇㱉ⵋ㱍橏ᝑⱓ♕㝗⡙⡛ᵝཟཡॣ॥٧䑩⡫཭ѯ፱♳፵᥷ṹ᥻౽", a_));
							IL_540:
							return;
						}
						finally
						{
							num2 = 0;
							for (;;)
							{
								switch (num2)
								{
								case 1:
									goto IL_58F;
								case 2:
									spr\u2059.ᜀ.Close();
									Monitor.Exit(this);
									num2 = 1;
									continue;
								}
								if (this.ᜃ != ExportSource.SqlCommand)
								{
									break;
								}
								num2 = 2;
							}
							IL_58F:;
						}
						goto IL_592;
					}
				}
				IL_74:
				throw new NullReferenceException(HyperlinksCollectionEditor.b("ጝ⨟愡䬣䨥崧䜩䈫崭甯䨱䐳夵䨷丹ػнؿ⭁⡃⩅摇㱉ⵋ㱍橏൑㭓⅕㙗㽙⹛᭝ᡟቡୣᑥᱧཀྵ࡫⡭᥯᝱ᡳት୷", a_));
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0005EC20 File Offset: 0x0005DC20
		public void AutoCalcColWidth()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IEnumerator enumerator = base.GetEnumerator();
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_578;
						case 1:
							goto IL_3D;
						case 2:
							if (this.ᜃ == ExportSource.SqlCommand)
							{
								num = 0;
								continue;
							}
							goto IL_3D;
						case 3:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										ColumnExport columnExport;
										columnExport.Width = columnExport.Caption.Length;
										num = 2;
										continue;
									}
									case 3:
										goto IL_50E;
									case 4:
										num = 3;
										continue;
									case 5:
									{
										if (!enumerator.MoveNext())
										{
											num = 4;
											continue;
										}
										ColumnExport columnExport = (ColumnExport)enumerator.Current;
										num = 6;
										continue;
									}
									case 6:
									{
										ColumnExport columnExport;
										if (columnExport.Caption.Length > columnExport.Width)
										{
											num = 0;
											continue;
										}
										break;
									}
									}
									IL_492:
									num = 5;
									continue;
									goto IL_492;
								}
								IL_50E:
								goto IL_430;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_565;
										case 1:
											goto IL_575;
										case 2:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_565;
											default:
												if (false)
												{
												}
												if (disposable != null)
												{
													num = 0;
													continue;
												}
												goto IL_577;
											}
											break;
										}
										break;
										IL_565:
										disposable.Dispose();
										num = 1;
									}
								}
								IL_575:
								IL_577:;
							}
							goto IL_578;
						}
						break;
						IL_430:
						num = 2;
						continue;
						try
						{
							IL_3D:
							for (;;)
							{
								spr\u2059.ᜀ(this.ᜃ, this.ᜅ, this.ᜆ);
								int num2 = 0;
								num = 25;
								for (;;)
								{
									int num3;
									switch (num)
									{
									case 0:
									{
										ColumnExport columnExport2;
										string text;
										if (columnExport2.Length < text.Length)
										{
											num = 3;
											continue;
										}
										goto IL_302;
									}
									case 1:
										goto IL_380;
									case 2:
										goto IL_302;
									case 3:
									{
										ColumnExport columnExport2;
										string text;
										columnExport2.Length = text.Length;
										num = 2;
										continue;
									}
									case 4:
										num = 0;
										continue;
									case 5:
										this.ᜁ = 0;
										num = 18;
										continue;
									case 6:
										if (this.ᜁ >= this.ᜐ)
										{
											num = 24;
											continue;
										}
										goto IL_13B;
									case 7:
										goto IL_3A9;
									case 8:
										num = 6;
										continue;
									case 9:
										if (!spr\u2059.ᜀ(this.ᜃ, this.ᜅ, this.ᜆ, this.ᜁ, this.ᜐ, this.ᜏ))
										{
											num = 22;
											continue;
										}
										goto IL_3D2;
									case 10:
										goto IL_3A9;
									case 11:
										goto IL_3DE;
									case 12:
										num = 13;
										continue;
									case 13:
										if (this.ᜀ is DBFExport)
										{
											num = 16;
											continue;
										}
										goto IL_302;
									case 14:
									{
										ColumnExport columnExport2;
										if (columnExport2.ColExportType == ColExportType.String)
										{
											num = 4;
											continue;
										}
										goto IL_302;
									}
									case 15:
										if (this.ᜀ != null)
										{
											num = 12;
											continue;
										}
										goto IL_302;
									case 16:
										num = 14;
										continue;
									case 17:
										spr\u2059.ᜀ(this.ᜃ, spr\u2059.ᜀ, this.ᜅ, ref this.ᜁ);
										this.ᜀ(this.ᜀ, this.ᜁ);
										Thread.Sleep(0);
										num = 28;
										continue;
									case 18:
										goto IL_14E;
									case 19:
									{
										ColumnExport columnExport2;
										string text;
										if (columnExport2.Width < text.Length)
										{
											num = 21;
											continue;
										}
										goto IL_1C7;
									}
									case 20:
										goto IL_1C7;
									case 21:
									{
										ColumnExport columnExport2;
										string text;
										columnExport2.Width = text.Length;
										num = 20;
										continue;
									}
									case 22:
										num = 23;
										continue;
									case 23:
										if (this.ᜐ != 0)
										{
											num = 8;
											continue;
										}
										goto IL_13B;
									case 24:
										goto IL_3D2;
									case 25:
										goto IL_380;
									case 26:
										if (num2 >= this.ᜏ)
										{
											num = 5;
											continue;
										}
										spr\u2059.ᜀ(this.ᜃ, spr\u2059.ᜀ, this.ᜅ, ref this.ᜁ);
										num2++;
										num = 1;
										continue;
									case 27:
									{
										if (num3 >= base.Count)
										{
											num = 17;
											continue;
										}
										string text = spr\u2059.ᜀ(this.ᜃ, spr\u2059.ᜀ, this.ᜆ, this, this.ᜑ, this.\u1712, num3, this.ᜁ, this.ᜏ, true);
										ColumnExport columnExport2 = this[num3];
										num = 19;
										continue;
									}
									case 28:
										goto IL_14E;
									}
									break;
									IL_13B:
									num3 = 0;
									num = 7;
									continue;
									IL_14E:
									num = 9;
									continue;
									IL_1C7:
									num = 15;
									continue;
									IL_302:
									num3++;
									num = 10;
									continue;
									IL_380:
									num = 26;
									continue;
									IL_3A9:
									num = 27;
									continue;
									IL_3D2:
									num = 11;
								}
							}
							IL_3DE:
							goto IL_59F;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_42D;
								case 1:
									spr\u2059.ᜀ.Close();
									Monitor.Exit(this);
									num = 0;
									continue;
								}
								if (this.ᜃ != ExportSource.SqlCommand)
								{
									break;
								}
								num = 1;
							}
							IL_42D:;
						}
						goto IL_430;
						IL_578:
						Monitor.Enter(this);
						spr\u2059.ᜀ = this.ᜄ.ExecuteReader();
						num = 1;
					}
				}
				IL_59F:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0005F208 File Offset: 0x0005E208
		public int IndexOfName(string Name)
		{
			int num;
			for (;;)
			{
				if (true)
				{
				}
				num = 0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return num;
				default:
				{
					if (false)
					{
					}
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_92;
						case 1:
							if (num >= base.Count)
							{
								num2 = 4;
								continue;
							}
							num2 = 3;
							continue;
						case 2:
							goto IL_94;
						case 3:
							if (string.Compare(Name, this[num].Name, true) == 0)
							{
								num2 = 0;
								continue;
							}
							num++;
							num2 = 5;
							continue;
						case 4:
							return -1;
						case 5:
							goto IL_94;
						}
						break;
						IL_94:
						num2 = 1;
					}
					break;
				}
				}
			}
			return num;
			IL_92:
			return num;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0005F2CC File Offset: 0x0005E2CC
		public void EmptyTags()
		{
			IEnumerator enumerator = base.GetEnumerator();
			try
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
							goto IL_5E;
						default:
							goto IL_8E;
						}
						break;
					case 3:
						goto IL_5E;
					case 4:
						num = 0;
						continue;
					}
					goto IL_37;
					IL_5E:
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					ColumnExport columnExport = (ColumnExport)enumerator.Current;
					columnExport.Tag = 0;
					num = 1;
					continue;
					IL_56:
					num = 3;
					continue;
					IL_37:
					goto IL_56;
				}
				IL_8E:
				if (false)
				{
				}
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
							goto IL_DC;
						case 1:
							disposable.Dispose();
							if (true)
							{
							}
							num = 0;
							continue;
						case 2:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_DE;
						}
						break;
					}
				}
				IL_DC:
				IL_DE:;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0005F3C8 File Offset: 0x0005E3C8
		public bool GetColumnIsNull(int Index, IDataReader Reader)
		{
			int a_ = 12;
			ColumnExport columnExport;
			for (;;)
			{
				if (true)
				{
				}
				columnExport = this[Index];
				ExportSource exportSource = this.ᜃ;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_11E;
					case 1:
						num = 7;
						continue;
					case 2:
						if (Reader == null)
						{
							num = 4;
							continue;
						}
						num = 8;
						continue;
					case 3:
						switch (exportSource)
						{
						case ExportSource.SqlCommand:
							num = 2;
							continue;
						case ExportSource.DataTable:
							num = 6;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					case 4:
						goto IL_BF;
					case 5:
						goto IL_90;
					case 6:
						if (spr\u2059.ᜂ == null)
						{
							num = 5;
							continue;
						}
						goto IL_EF;
					case 7:
						goto IL_E0;
					case 8:
						if (Reader.IsClosed)
						{
							num = 0;
							continue;
						}
						goto IL_E2;
					}
					break;
				}
			}
			IL_90:
			throw new NullReferenceException(HyperlinksCollectionEditor.b("┧\u2029漫䄭尯䜱夳堵䬷缹䐻丽⼿ぁぃ籅片൉⥋㩍ፏ㵑㡓⍕㕗㑙ᕛⵝ⹟ᝡࡣ੥䑧ᱩ൫ᱭ䩯㝱౳ٵ᝷ࡹࡻ㵽ꒉ쾋ﮍ좙", a_));
			IL_BF:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("┧\u2029漫䄭尯䜱夳堵䬷缹䐻丽⼿ぁぃ籅片൉⥋㩍ፏ㵑㡓⍕㕗㑙ᕛⵝ⹟ᝡࡣ੥䑧ᱩ൫ᱭ䩯ⁱᅳ᝵ᱷό๻", a_));
			IL_E0:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_11E:
				throw new ArgumentException(HyperlinksCollectionEditor.b("┧\u2029漫䄭尯䜱夳堵䬷缹䐻丽⼿ぁぃ籅片൉⥋㩍ፏ㵑㡓⍕㕗㑙ᕛⵝ⹟ᝡࡣ੥䑧ᱩ൫ᱭ䩯ⁱᅳ᝵ᱷό๻", a_));
			default:
				if (false)
				{
				}
				return false;
			}
			IL_E2:
			return Reader.IsDBNull(columnExport.Number);
			IL_EF:
			return spr\u2059.ᜂ.IsNull(columnExport.Number);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0005F52C File Offset: 0x0005E52C
		public bool ContainsBLOB()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = base.GetEnumerator();
				bool result;
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 4;
							continue;
						case 1:
							goto IL_C4;
						case 3:
							result = true;
							num = 1;
							continue;
						case 4:
							goto IL_D2;
						case 5:
						{
							ColumnExport columnExport;
							if (columnExport.IsBlob)
							{
								num = 3;
								continue;
							}
							goto IL_6F;
						}
						case 6:
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							goto IL_8B;
						}
						goto IL_51;
						IL_6F:
						num = 6;
						continue;
						IL_51:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							IL_8B:
							ColumnExport columnExport = (ColumnExport)enumerator.Current;
							num = 5;
							break;
						}
						default:
							if (false)
							{
							}
							goto IL_6F;
						}
					}
					IL_C4:
					return result;
					IL_D2:
					return false;
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
								goto IL_11B;
							case 1:
								disposable.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_119;
							}
							break;
						}
					}
					IL_119:
					IL_11B:
					if (true)
					{
					}
				}
				return result;
			}
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0005F670 File Offset: 0x0005E670
		public bool ContainsMemo()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = base.GetEnumerator();
				bool result;
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_C8;
						case 1:
						{
							ColumnExport columnExport;
							if (columnExport.IsMemo)
							{
								num = 5;
								continue;
							}
							goto IL_65;
						}
						case 2:
							num = 0;
							continue;
						case 3:
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							goto IL_81;
						case 5:
							result = true;
							num = 6;
							continue;
						case 6:
							goto IL_BA;
						}
						goto IL_47;
						IL_65:
						num = 3;
						continue;
						IL_47:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							IL_81:
							ColumnExport columnExport = (ColumnExport)enumerator.Current;
							num = 1;
							break;
						}
						default:
							if (false)
							{
							}
							goto IL_65;
						}
					}
					IL_BA:
					goto IL_112;
					IL_C8:
					return false;
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
									num = 2;
									continue;
								}
								goto IL_111;
							case 1:
								goto IL_10F;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_10F:
					IL_111:;
				}
				IL_112:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0005F7B4 File Offset: 0x0005E7B4
		public ColumnExport Add(ColumnExport Item)
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
			base.InnerList.Add(Item);
			return Item;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0005F800 File Offset: 0x0005E800
		public void AddRange(ColumnExport[] Items)
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
			base.InnerList.AddRange(Items);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0005F848 File Offset: 0x0005E848
		internal string ᜀ(string A_0)
		{
			if (this.\u1712 == null)
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
					return string.Empty;
				}
			}
			return this.\u1712(A_0);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0005F8A0 File Offset: 0x0005E8A0
		internal void ᜀ(object A_0, int A_1)
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					goto IL_57;
				case 2:
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_57:
					(this.ᜀ as ExportBase).ᜂ(A_0, A_1);
					num = 2;
					break;
				default:
					if (false)
					{
					}
					if (!(this.ᜀ is ExportBase))
					{
						return;
					}
					num = 1;
					break;
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0005F92C File Offset: 0x0005E92C
		public object Holder
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000040 RID: 64
		public ColumnExport this[int Index]
		{
			get
			{
				int a_ = 12;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (Index >= base.InnerList.Count)
						{
							num = 2;
							continue;
						}
						goto IL_B1;
					case 2:
						goto IL_AF;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
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
					}
					if (Index < 0)
					{
						break;
					}
					num = 3;
				}
				IL_41:
				throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("愧䐩娫伭尯嬱倳礵䠷弹主弽㐿⭁⭃⡅ᝇ͉≋⩍㕏⩑᭓⍕ⱗᕙ㩛ᱝཟᝡ੣ɥ᭧", a_)), Index));
				IL_AF:
				goto IL_41;
				IL_B1:
				return base.InnerList[Index] as ColumnExport;
			}
			set
			{
				int a_ = 15;
				int num;
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
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AC;
					case 1:
						num = 2;
						continue;
					case 2:
						if (Index >= base.InnerList.Count)
						{
							num = 0;
							continue;
						}
						goto IL_AE;
					case 3:
						if (true)
						{
						}
						break;
					}
					if (Index < 0)
					{
						break;
					}
					num = 1;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊьⅎ㕐㙒ⵔᡖⱘ⽚ቜ㥞⍠ౢၤ०൨ᡪ", a_)), Index));
				IL_AC:
				goto IL_65;
				IL_AE:
				base.InnerList[Index] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0005FB08 File Offset: 0x0005EB08
		public FormatsExport OwnerFormatsExport
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
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0005FB4C File Offset: 0x0005EB4C
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0005FB90 File Offset: 0x0005EB90
		internal NormalFunc NormalFunc
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
				return this.\u1712;
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
				this.\u1712 = value;
			}
		}

		// Token: 0x040006FA RID: 1786
		private object ᜀ;

		// Token: 0x040006FB RID: 1787
		private byte[] \u2609\u0086\u00A3\u00A1;

		// Token: 0x040006FC RID: 1788
		private int ᜁ;

		// Token: 0x040006FD RID: 1789
		private StringListCollection ᜂ;

		// Token: 0x040006FE RID: 1790
		private ExportSource ᜃ;

		// Token: 0x040006FF RID: 1791
		private IDbCommand ᜄ;

		// Token: 0x04000700 RID: 1792
		private DataTable ᜅ;

		// Token: 0x04000701 RID: 1793
		private ListView ᜆ;

		// Token: 0x04000702 RID: 1794
		private int \u25D9\u0081\u00AE\u00AC;

		// Token: 0x04000703 RID: 1795
		private FormatsExport ᜇ;

		// Token: 0x04000704 RID: 1796
		private bool ᜈ;

		// Token: 0x04000705 RID: 1797
		private StringListCollection ᜉ;

		// Token: 0x04000706 RID: 1798
		private StringListCollection ᜊ;

		// Token: 0x04000707 RID: 1799
		private StringListCollection ᜋ;

		// Token: 0x04000708 RID: 1800
		private StringListCollection ᜌ;

		// Token: 0x04000709 RID: 1801
		private StringListCollection \u170D;

		// Token: 0x0400070A RID: 1802
		private string \u2460\u0081\u008F\u00A0;

		// Token: 0x0400070B RID: 1803
		private StringListCollection ᜎ;

		// Token: 0x0400070C RID: 1804
		private int ᜏ;

		// Token: 0x0400070D RID: 1805
		private int ᜐ;

		// Token: 0x0400070E RID: 1806
		private CultureInfo ᜑ = CultureInfo.CurrentCulture;

		// Token: 0x0400070F RID: 1807
		private NormalFunc \u1712;
	}
}
