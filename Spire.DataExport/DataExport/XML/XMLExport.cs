using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Forms;
using Spire.DataExport.License;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.XML
{
	// Token: 0x02000185 RID: 389
	[LicenseProvider(typeof(DataExportLicenseProvider))]
	[ToolboxItem(true)]
	public class XMLExport : TextExport
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x0007019C File Offset: 0x0006F19C
		protected override void InitializeVariables()
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
			this.ᜁ = !LicenseManager.IsValid(base.GetType(), this, out this.ᜀ);
			base.InitializeVariables();
			this.ExportNullField = true;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00070200 File Offset: 0x0006F200
		protected override void Dispose(bool disposing)
		{
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7A;
					case 1:
						goto IL_4C;
					case 3:
						if (true)
						{
						}
						goto IL_72;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_4C:
						this.ᜀ.Dispose();
						this.ᜀ = null;
						num = 3;
						continue;
					default:
						if (false)
						{
						}
						if (this.ᜀ != null)
						{
							num = 1;
							continue;
						}
						break;
					}
					IL_72:
					num = 0;
				}
				IL_7A:;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000702AC File Offset: 0x0006F2AC
		public override void SaveToFile()
		{
			for (;;)
			{
				spr\u2561.ᜀ = this.ᜁ;
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
							goto IL_48;
						default:
							goto IL_66;
						}
						break;
					case 1:
						if (this.ᜁ)
						{
							num = 3;
							continue;
						}
						goto IL_9C;
					case 2:
						if (Environment.UserInteractive)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						goto IL_9C;
					case 3:
						num = 2;
						continue;
					case 4:
						AboutDataExport.ShowAbout(false);
						goto IL_48;
					}
					break;
					IL_48:
					num = 0;
				}
			}
			IL_66:
			if (false)
			{
			}
			IL_9C:
			base.SaveToFile();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0007035C File Offset: 0x0006F35C
		public override void SaveToStream(Stream Stream)
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
			spr\u2561.ᜀ = this.ᜁ;
			base.SaveToStream(Stream);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000703AC File Offset: 0x0006F3AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SaveToHttpResponse(string FileName, HttpResponse response, SaveType saveType)
		{
			int a_ = 11;
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				if (true)
				{
				}
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䘦夨嬪䄬䘮到刲䄴帶嘸唺ሼ䜾ⱀ⽂", a_), response, saveType);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_98;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_64;
						default:
							if (false)
							{
							}
							((IDisposable)memoryStream).Dispose();
							num = 0;
							continue;
						}
						break;
					}
					goto IL_61;
					IL_64:
					num = 2;
					continue;
					IL_61:
					if (memoryStream != null)
					{
						goto IL_64;
					}
					break;
				}
				IL_98:;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00070470 File Offset: 0x0006F470
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SaveToHttpResponse(string FileName, HttpResponse response)
		{
			int a_ = 14;
			spr\u2561.ᜀ = this.ᜁ;
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				base.SaveToStream(memoryStream);
				base.ExportToHttpResponse(FileName, memoryStream, HyperlinksCollectionEditor.b("䬩尫席尯嬱圳圵䰷匹医倽漿㩁⥃⩅", a_), response, SaveType.Attachment);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_98;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_64;
						default:
							if (false)
							{
							}
							((IDisposable)memoryStream).Dispose();
							num = 0;
							continue;
						}
						break;
					}
					goto IL_59;
					IL_64:
					num = 1;
					continue;
					IL_59:
					if (true)
					{
					}
					if (memoryStream != null)
					{
						goto IL_64;
					}
					break;
				}
				IL_98:;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00070534 File Offset: 0x0006F534
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SaveToHttpResponse(HttpResponse response)
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
			spr\u2561.ᜀ = this.ᜁ;
			this.SaveToHttpResponse(this.FileName, response);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00070588 File Offset: 0x0006F588
		protected override void BeginDataExport()
		{
			int a_ = 5;
			string arg;
			string arg2;
			for (;;)
			{
				base.BeginDataExport();
				arg = string.Empty;
				int num = 4;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						arg = HyperlinksCollectionEditor.b("堠䘢嘤", a_);
						num = 6;
						continue;
					case 1:
						goto IL_A3;
					case 2:
						arg2 = string.Format(HyperlinksCollectionEditor.b("䐠䴢䘤䠦䴨䈪䌬䠮రᄲ临ܶ䐸᤺ᴼ", a_), this.ᜁ.Encoding);
						num = 7;
						continue;
					case 3:
						goto IL_120;
					case 4:
						if (this.ᜁ.StandAlone)
						{
							num = 0;
							continue;
						}
						arg = HyperlinksCollectionEditor.b("传䰢", a_);
						num = 1;
						continue;
					case 5:
						if (this.ᜁ.Encoding.Length > 0)
						{
							num = 2;
							continue;
						}
						arg2 = string.Empty;
						num = 3;
						continue;
					case 6:
						IL_10D:
						goto IL_A3;
					case 7:
						goto IL_9E;
					}
					break;
					IL_A3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10D;
					default:
						if (false)
						{
						}
						arg2 = string.Empty;
						num = 5;
						break;
					}
				}
			}
			IL_9E:
			IL_120:
			this.ᜀ().ᜇ(string.Format(HyperlinksCollectionEditor.b("ᴠᰢ崤䨦䔨ପ嬬䨮䌰䀲尴堶圸غἼ䐾煀㹂杄杆㉈穊が㱎═㉒㭔㍖㡘㝚㉜ㅞѠ幢䝤ᱦ孨ᙪ佬偮佰", a_), this.ᜁ.Version, arg2, arg));
			this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ᴠ朢搤猦栨笪氬氮稰瘲愴᜶漸帺似䰾⡀ⱂ⭄穆歈祊捌罎獐浒", a_));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0007071C File Offset: 0x0006F71C
		protected override void BeforeExport()
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
			this.ᜀ().ᜁ(HyperlinksCollectionEditor.b("伜倞瘠朢搤猦栨", a_), string.Empty);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00070780 File Offset: 0x0006F780
		private string ᜀ(string A_0)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					stringBuilder = new StringBuilder(A_0);
					char c = ' ';
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_AD;
						case 1:
							goto IL_1BD;
						case 2:
							goto IL_18D;
						case 3:
							if (true)
							{
							}
							goto IL_75;
						case 4:
						{
							char c2;
							if (c2 > '\u007f')
							{
								num = 5;
								continue;
							}
							stringBuilder.Replace(c2.ToString(), string.Format(HyperlinksCollectionEditor.b("砦儨倪ᴬᔮ椰ܲ䠴栶", a_), (ushort)c2));
							c2 += '\u0001';
							num = 14;
							continue;
						}
						case 5:
							goto IL_1B8;
						case 6:
						{
							char c3;
							if (c3 > '@')
							{
								num = 9;
								continue;
							}
							stringBuilder.Replace(c3.ToString(), string.Format(HyperlinksCollectionEditor.b("砦儨倪ᴬᔮ椰ܲ䠴栶", a_), (ushort)c3));
							c3 += '\u0001';
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_24E;
							default:
								if (false)
								{
								}
								num = 15;
								continue;
							}
							break;
						}
						case 7:
							goto IL_75;
						case 8:
							goto IL_AD;
						case 9:
						{
							char c4 = '[';
							num = 3;
							continue;
						}
						case 10:
						{
							stringBuilder.Replace(HyperlinksCollectionEditor.b("䜦", a_), string.Format(HyperlinksCollectionEditor.b("砦儨倪ᴬᔮ椰ܲ䠴栶", a_), 96));
							char c2 = '{';
							num = 2;
							continue;
						}
						case 11:
						{
							stringBuilder.Replace(HyperlinksCollectionEditor.b("ࠦ", a_), string.Format(HyperlinksCollectionEditor.b("砦儨倪ᴬᔮ椰ܲ䠴栶", a_), 47));
							char c3 = ';';
							num = 1;
							continue;
						}
						case 12:
							if (c > ',')
							{
								num = 11;
								continue;
							}
							stringBuilder.Replace(c.ToString(), string.Format(HyperlinksCollectionEditor.b("砦儨倪ᴬᔮ椰ܲ䠴栶", a_), (ushort)c));
							c += '\u0001';
							num = 8;
							continue;
						case 13:
						{
							char c4;
							if (c4 > '^')
							{
								num = 10;
								continue;
							}
							stringBuilder.Replace(c4.ToString(), string.Format(HyperlinksCollectionEditor.b("砦儨倪ᴬᔮ椰ܲ䠴栶", a_), (ushort)c4));
							c4 += '\u0001';
							goto IL_24E;
						}
						case 14:
							goto IL_18D;
						case 15:
							goto IL_1BD;
						}
						break;
						IL_75:
						num = 13;
						continue;
						IL_AD:
						num = 12;
						continue;
						IL_18D:
						num = 4;
						continue;
						IL_1BD:
						num = 6;
						continue;
						IL_24E:
						num = 7;
					}
				}
				IL_1B8:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00070A4C File Offset: 0x0006FA4C
		protected override string GetColumnTitle(int Index)
		{
			int a_ = 15;
			string text;
			for (;;)
			{
				IL_1D:
				text = this.ᜀ(base.ColumnsExport[Index].Name);
				ColExport colExport = base.ExportRowExport.ColByName(base.ColumnsExport[Index].Name);
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_81:
					num = 2;
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
						goto IL_7E;
					case 1:
						goto IL_AA;
					case 2:
						colExport.XMLElementName = text;
						num = 1;
						continue;
					}
					goto IL_1D;
				}
				IL_7E:
				if (colExport != null)
				{
					goto IL_81;
				}
				break;
			}
			IL_AA:
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format(HyperlinksCollectionEditor.b("洪䐬䨮崰圲笴嘶吸帺<ᴾ㩀獂㡄敆", a_), text) + ' ');
			stringBuilder.Append(string.Format(HyperlinksCollectionEditor.b("漪䐬尮䄰弲吴丶甸娺弼娾ⵀ繂杄㱆祈㙊潌", a_), text) + ' ');
			stringBuilder.Append(string.Format(HyperlinksCollectionEditor.b("洪䐬䨮崰圲愴丶䤸帺<ᴾ㩀獂㡄敆", a_), spr\u2059.ᜀ(base.ColumnsExport[Index].ColExportType)) + ' ');
			stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("洪䐬䨮崰圲瘴嬶堸䠺丼Ⱦ捀㡂畄㩆歈", a_), HyperlinksCollectionEditor.b("漪䰬嬮倰瀲娴嬶䰸嘺匼", a_));
			return stringBuilder.ToString();
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00070BC0 File Offset: 0x0006FBC0
		protected override void WriteTitleRow()
		{
			int a_ = 14;
			for (;;)
			{
				this.ᜀ().ᜁ(HyperlinksCollectionEditor.b("朩椫稭焯瘱申戵礷", a_), string.Empty);
				this.ᜀ().ᜁ(HyperlinksCollectionEditor.b("氩攫欭簯瘱朳", a_), string.Empty);
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_69;
					case 1:
						goto IL_69;
					case 2:
						goto IL_92;
					case 3:
						if (true)
						{
						}
						if (num >= base.ColumnsExport.Count)
						{
							num2 = 2;
							continue;
						}
						for (;;)
						{
							this.ᜀ().ᜂ(HyperlinksCollectionEditor.b("氩攫欭簯瘱", a_), this.GetColumnTitle(num));
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							}
							break;
						}
						IL_D8:
						if (false)
						{
						}
						num2 = 1;
						continue;
						goto IL_D8;
					}
					break;
					IL_69:
					num2 = 3;
				}
			}
			IL_92:
			this.ᜀ().ᜀ(HyperlinksCollectionEditor.b("氩攫欭簯瘱朳", a_));
			this.ᜀ().ᜀ(HyperlinksCollectionEditor.b("朩椫稭焯瘱申戵礷", a_));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00070CF0 File Offset: 0x0006FCF0
		protected override string GetDataRow(bool NeedFormat)
		{
			int a_ = 9;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder = new StringBuilder(base.ExportRowExport.Count);
				IEnumerator enumerator = base.ExportRowExport.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_10C;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D6;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								goto IL_D6;
							}
							ColExport colExport = (ColExport)enumerator.Current;
							string value = string.Format(HyperlinksCollectionEditor.b("帤ᜦ吨ᘪ༬吮0串᜴", a_), colExport.Name.Replace(' ', '_'), colExport.GetExportedValue(NeedFormat)) + ' ';
							stringBuilder.Append(value);
							num = 2;
							continue;
						}
						}
						IL_C5:
						num = 3;
						continue;
						goto IL_C5;
						IL_D6:
						num = 1;
					}
					IL_10C:;
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
								goto IL_153;
							case 1:
								if (disposable != null)
								{
									num = 2;
									continue;
								}
								goto IL_155;
							case 2:
								disposable.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_153:
					IL_155:;
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00070E7C File Offset: 0x0006FE7C
		protected override void WriteRow()
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				spr\u25E9 spr_u25E;
				for (;;)
				{
					bool formatValue = true;
					spr_u25E = this.ᜀ();
					List<ColExport> list = new List<ColExport>();
					StringBuilder stringBuilder = new StringBuilder(base.ExportRowExport.Count);
					IEnumerator enumerator = base.ExportRowExport.GetEnumerator();
					int num = 3;
					for (;;)
					{
						List<ColExport>.Enumerator enumerator2;
						switch (num)
						{
						case 0:
							try
							{
								num = 8;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 4;
										continue;
									case 2:
									{
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										ColExport colExport = enumerator2.Current;
										string text = colExport.GetExportedValue(formatValue, false);
										num = 7;
										continue;
									}
									case 3:
									{
										ColExport colExport;
										spr_u25E.ᜀ(colExport.XMLElementName, null, true);
										string text;
										spr_u25E.ᜆ(text);
										spr_u25E.ᜀ(colExport.XMLElementName);
										num = 0;
										continue;
									}
									case 4:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											continue;
										default:
											goto IL_1AD;
										}
										break;
									case 5:
									{
										string text = string.Empty;
										num = 10;
										continue;
									}
									case 6:
										if (this.ExportNullField)
										{
											num = 5;
											continue;
										}
										goto IL_AF;
									case 7:
									{
										string text;
										if (text == null)
										{
											num = 11;
											continue;
										}
										goto IL_AF;
									}
									case 9:
									{
										string text;
										if (text != null)
										{
											num = 3;
											continue;
										}
										break;
									}
									case 10:
										goto IL_AF;
									case 11:
										num = 6;
										continue;
									}
									goto IL_AD;
									IL_AF:
									num = 9;
									continue;
									IL_E6:
									num = 2;
									continue;
									IL_AD:
									goto IL_E6;
								}
								IL_1AD:
								if (false)
								{
								}
								goto IL_1F3;
							}
							finally
							{
								((IDisposable)enumerator2).Dispose();
							}
							goto IL_1C3;
						case 1:
							if (true)
							{
							}
							if (list.Count > 0)
							{
								num = 2;
								continue;
							}
							goto IL_583;
						case 2:
							goto IL_1C3;
						case 3:
							try
							{
								num = 3;
								for (;;)
								{
									ColExport colExport2;
									string text2;
									switch (num)
									{
									case 0:
									{
										bool flag;
										if (flag)
										{
											num = 26;
											continue;
										}
										text2 = colExport2.GetExportedValue(formatValue, false);
										num = 23;
										continue;
									}
									case 4:
										num = 21;
										continue;
									case 5:
										if (text2 != null)
										{
											num = 11;
											continue;
										}
										break;
									case 6:
										if (stringBuilder.Length > 0)
										{
											num = 16;
											continue;
										}
										goto IL_2FF;
									case 7:
										if (base.ColumnsExport[colExport2.ColumnIndex].ColExportType == ColExportType.Binary)
										{
											num = 17;
											continue;
										}
										text2 = null;
										num = 8;
										continue;
									case 8:
										if (base.ColumnsExport[colExport2.ColumnIndex].ColExportType == ColExportType.String)
										{
											num = 22;
											continue;
										}
										num = 0;
										continue;
									case 9:
										goto IL_4BF;
									case 10:
									{
										bool flag;
										if (!flag)
										{
											num = 19;
											continue;
										}
										break;
									}
									case 11:
										num = 6;
										continue;
									case 13:
										if (this.ExportNullField)
										{
											num = 18;
											continue;
										}
										num = 10;
										continue;
									case 14:
										text2 = string.Empty;
										num = 24;
										continue;
									case 15:
									{
										if (!enumerator.MoveNext())
										{
											num = 4;
											continue;
										}
										colExport2 = (ColExport)enumerator.Current;
										bool flag = colExport2.OriginalDataIsNull;
										num = 7;
										continue;
									}
									case 16:
										stringBuilder.Append(' ');
										num = 25;
										continue;
									case 17:
										num = 13;
										continue;
									case 18:
										list.Add(colExport2);
										num = 1;
										continue;
									case 19:
										list.Add(colExport2);
										num = 12;
										continue;
									case 20:
										if (this.ExportNullField)
										{
											num = 14;
											continue;
										}
										goto IL_4BF;
									case 21:
										goto IL_4EC;
									case 22:
										text2 = colExport2.GetExportedValue(formatValue, this.ExportNullField);
										num = 9;
										continue;
									case 23:
										goto IL_4BF;
									case 24:
										goto IL_4BF;
									case 25:
										goto IL_2FF;
									case 26:
										num = 20;
										continue;
									}
									goto IL_286;
									IL_2FF:
									stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("攝ဟ弡ᤣХ匧ᬩ儫భ", a_), colExport2.XMLElementName, text2);
									num = 2;
									continue;
									IL_499:
									num = 15;
									continue;
									IL_286:
									goto IL_499;
									IL_4BF:
									num = 5;
								}
								IL_4EC:;
							}
							finally
							{
								for (;;)
								{
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
											goto IL_536;
										case 1:
											goto IL_534;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_534:
								IL_536:;
							}
							spr_u25E.ᜀ(HyperlinksCollectionEditor.b("䰝漟甡", a_), stringBuilder.ToString(), false);
							num = 1;
							continue;
						}
						break;
						IL_1C3:
						spr_u25E.ᜀ(true);
						spr_u25E.ᜌ();
						enumerator2 = list.GetEnumerator();
						num = 0;
					}
				}
				IL_1F3:
				spr_u25E.ᜀ(HyperlinksCollectionEditor.b("䰝漟甡", a_));
				return;
				IL_583:
				spr_u25E.ᜀ(false);
				spr_u25E.ᜌ();
				return;
			}
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00071450 File Offset: 0x00070450
		protected override void AfterExport()
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ().ᜀ(HyperlinksCollectionEditor.b("縫愭术瘱申戵礷", a_));
			this.ᜀ().ᜇ(HyperlinksCollectionEditor.b("ါĭ琯猱怳眵样笹缻甽Կᙁ穃", a_));
			base.AfterExport();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000714CC File Offset: 0x000704CC
		internal new spr\u25E9 ᜀ()
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
			return base.ᜀ() as spr\u25E9;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00071514 File Offset: 0x00070514
		protected override Type GetWriterType()
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
			return typeof(spr\u25E9);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0007155C File Offset: 0x0007055C
		internal override string NormalString(string S)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					stringBuilder = new StringBuilder(S.Length);
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_151;
						case 1:
							num2 = 15;
							continue;
						case 2:
						{
							if (num >= S.Length)
							{
								num2 = 10;
								continue;
							}
							if (true)
							{
							}
							char c = S[num];
							num2 = 4;
							continue;
						}
						case 3:
							goto IL_F9;
						case 4:
						{
							char c;
							if (!this.CharInSpecialCharacters(c))
							{
								stringBuilder.Append(c);
								num2 = 12;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_24F;
							default:
								if (false)
								{
								}
								num2 = 17;
								continue;
							}
							break;
						}
						case 5:
							num2 = 13;
							continue;
						case 6:
						{
							char c2;
							if (c2 != '&')
							{
								num2 = 5;
								continue;
							}
							stringBuilder.Append(HyperlinksCollectionEditor.b("Ԣ䐤䨦夨ဪ", a_));
							num2 = 16;
							continue;
						}
						case 7:
							goto IL_F9;
						case 8:
							goto IL_F9;
						case 9:
						{
							char c2;
							if (c2 != '"')
							{
								num2 = 14;
								continue;
							}
							stringBuilder.Append(HyperlinksCollectionEditor.b("Ԣ吤刦䘨弪ᘬ", a_));
							num2 = 7;
							continue;
						}
						case 10:
							goto IL_175;
						case 11:
							goto IL_151;
						case 12:
							goto IL_F9;
						case 13:
						{
							char c2;
							switch (c2)
							{
							case '<':
								stringBuilder.Append(HyperlinksCollectionEditor.b("Ԣ䤤匦ረ", a_));
								num2 = 8;
								continue;
							case '=':
								goto IL_F9;
							case '>':
								stringBuilder.Append(HyperlinksCollectionEditor.b("Ԣ䈤匦ረ", a_));
								num2 = 3;
								continue;
							default:
								num2 = 1;
								continue;
							}
							break;
						}
						case 14:
							num2 = 6;
							continue;
						case 15:
							goto IL_F9;
						case 16:
							goto IL_F9;
						case 17:
						{
							char c;
							char c2 = c;
							num2 = 9;
							continue;
						}
						}
						break;
						IL_F9:
						num++;
						num2 = 11;
						continue;
						IL_151:
						num2 = 2;
					}
				}
				IL_175:
				IL_24F:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000717C0 File Offset: 0x000707C0
		protected override bool CharInSpecialCharacters(char Char)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					char[] array = new char[]
					{
						'<',
						'>',
						'&',
						'"'
					};
					char[] array2 = array;
					int num = 0;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							bool result;
							return result;
						}
						case 1:
						{
							bool result = true;
							num2 = 0;
							continue;
						}
						case 2:
							goto IL_6E;
						case 3:
						{
							char c;
							if (Char == c)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CD;
							default:
								if (false)
								{
								}
								num++;
								num2 = 6;
								continue;
							}
							break;
						}
						case 4:
						{
							if (num >= array2.Length)
							{
								num2 = 2;
								continue;
							}
							char c = array2[num];
							num2 = 3;
							continue;
						}
						case 5:
							goto IL_55;
						case 6:
							goto IL_55;
						}
						break;
						IL_55:
						num2 = 4;
					}
				}
				IL_6E:
				IL_CD:
				if (true)
				{
				}
				return false;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000718B8 File Offset: 0x000708B8
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x000718FC File Offset: 0x000708FC
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Description("Allows you to select string fields that will not be truncated by occurrences of carriage returns.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new StringListCollection NotTruncatableColumns
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
				return base.NotTruncatableColumns;
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
				base.NotTruncatableColumns = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00071940 File Offset: 0x00070940
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x00071984 File Offset: 0x00070984
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets or sets options of the result xml file.")]
		public XmlOptions XmlOptions
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
				return this.ᜁ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					IL_0A:
					switch (num)
					{
					case 1:
						return;
					case 2:
						if (value != this.ᜁ)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						return;
					case 3:
						this.ᜁ = value;
						num = 1;
						continue;
					case 4:
						num = 2;
						continue;
					}
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
							num = 4;
							goto IL_0A;
						}
					}
					break;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00071A1C File Offset: 0x00070A1C
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00071A60 File Offset: 0x00070A60
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public new StringListCollection Titles
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
				return base.Titles;
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
				base.Titles = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00071AA4 File Offset: 0x00070AA4
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x00071AE8 File Offset: 0x00070AE8
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new FormatsExport DataFormats
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
				return base.DataFormats;
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
				base.DataFormats = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00071B2C File Offset: 0x00070B2C
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00071B70 File Offset: 0x00070B70
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new StringListCollection CustomFormats
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
				return base.CustomFormats;
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
				base.CustomFormats = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00071BB4 File Offset: 0x00070BB4
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00071BF8 File Offset: 0x00070BF8
		[Editor(typeof(XmlFileNameEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public new string FileName
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
				return base.FileName;
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
				base.FileName = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00071C3C File Offset: 0x00070C3C
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00071C80 File Offset: 0x00070C80
		[DefaultValue(EncodingType.UTF8)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new EncodingType DataEncoding
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
				return base.DataEncoding;
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
				base.DataEncoding = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00071CC4 File Offset: 0x00070CC4
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00071D08 File Offset: 0x00070D08
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Indicate whether export null data field.")]
		[Browsable(true)]
		public bool ExportNullField
		{
			[CompilerGenerated]
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
			[CompilerGenerated]
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
				this.ᜂ = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00071D4C File Offset: 0x00070D4C
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00071D90 File Offset: 0x00070D90
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[Description("Indicate whether export long char/binary column.")]
		[DefaultValue(false)]
		public new bool ExportLongColumn
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
				return base.ExportLongColumn;
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
				base.ExportLongColumn = value;
			}
		}

		// Token: 0x0400082A RID: 2090
		private new License ᜀ;

		// Token: 0x0400082B RID: 2091
		private byte[] \u2593\u00AB\u00A7\u009C;

		// Token: 0x0400082C RID: 2092
		private new XmlOptions ᜁ = new XmlOptions();

		// Token: 0x0400082D RID: 2093
		[CompilerGenerated]
		private new bool ᜂ;
	}
}
