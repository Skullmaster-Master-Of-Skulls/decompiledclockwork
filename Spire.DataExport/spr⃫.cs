using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using Spire.DataExport.Access;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Common;

// Token: 0x02000033 RID: 51
internal class spr\u20EB : spr\u1BFE
{
	// Token: 0x0600019F RID: 415 RVA: 0x0000F04C File Offset: 0x0000E04C
	public spr\u20EB(ExportBase A_0, Stream A_1) : base(A_0, A_1)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000F080 File Offset: 0x0000E080
	public void ᜀ(string A_0, string A_1, string A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			bool flag = false;
			OleDbConnection oleDbConnection = new OleDbConnection();
			try
			{
				oleDbConnection.ConnectionString = A_0;
				try
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
						oleDbConnection.Open();
						try
						{
							for (;;)
							{
								DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
								int num = 4;
								for (;;)
								{
									OleDbCommand oleDbCommand;
									switch (num)
									{
									case 0:
										num = 1;
										continue;
									case 1:
										goto IL_A2;
									case 2:
										try
										{
											try
											{
												oleDbCommand.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												throw new Exception(ex.Message + HyperlinksCollectionEditor.b("∮㬰爲嘴吶尸䠺丼栾㍀⩂ㅄ≆㭈煊睌౎⍐㙒㑔⍖㱘ཚ㱜㵞ൠ٢", a_));
											}
											goto IL_270;
										}
										finally
										{
											oleDbCommand.Dispose();
										}
										goto IL_E0;
										IL_270:
										num = 5;
										continue;
									case 3:
										if (flag)
										{
											num = 0;
											continue;
										}
										goto IL_E0;
									case 4:
										try
										{
											IEnumerator enumerator = oleDbSchemaTable.Rows.GetEnumerator();
											try
											{
												num = 2;
												for (;;)
												{
													switch (num)
													{
													case 0:
														goto IL_1ED;
													case 1:
														goto IL_1F9;
													case 3:
														if (!flag)
														{
															num = 6;
															continue;
														}
														goto IL_1ED;
													case 4:
													{
														if (!enumerator.MoveNext())
														{
															num = 0;
															continue;
														}
														DataRow dataRow = (DataRow)enumerator.Current;
														num = 5;
														continue;
													}
													case 5:
													{
														DataRow dataRow;
														if (string.Compare(dataRow[HyperlinksCollectionEditor.b("笮瀰焲礴父昸漺搼漾р", a_)].ToString(), HyperlinksCollectionEditor.b("笮瀰焲礴父", a_)) == 0)
														{
															num = 7;
															continue;
														}
														break;
													}
													case 7:
													{
														DataRow dataRow;
														flag = (string.Compare(dataRow[HyperlinksCollectionEditor.b("笮瀰焲礴父昸町簼爾р", a_)].ToString(), A_1, true) == 0);
														num = 3;
														continue;
													}
													}
													IL_189:
													num = 4;
													continue;
													goto IL_189;
													IL_1ED:
													num = 1;
												}
												IL_1F9:;
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
																num = 1;
																continue;
															}
															goto IL_243;
														case 1:
															disposable.Dispose();
															num = 2;
															continue;
														case 2:
															goto IL_241;
														}
														break;
													}
												}
												IL_241:
												IL_243:;
											}
										}
										finally
										{
											oleDbSchemaTable.Dispose();
										}
										num = 3;
										continue;
									case 5:
										goto IL_27C;
									}
									break;
									IL_E0:
									oleDbCommand = new OleDbCommand(A_2, oleDbConnection);
									num = 2;
								}
							}
							IL_A2:
							IL_27C:;
						}
						finally
						{
							oleDbConnection.Close();
						}
						break;
					}
				}
				catch
				{
				}
			}
			finally
			{
				oleDbConnection.Dispose();
			}
			return;
		}
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000F3CC File Offset: 0x0000E3CC
	public void ᜀ(string A_0, string A_1, ParameterData[] A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			for (;;)
			{
				string format = string.Empty;
				int num = 0;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if ((this.ᜀ as AccessExport).Password.Length > 0)
						{
							num = 3;
							continue;
						}
						format = HyperlinksCollectionEditor.b("猢圤䠦弨䈪䤬䨮䌰า破帶娸䤺刼䰾⹀╂ㅄ楆͈⹊㥌慎Ṑὒၔፖ᭘畚楜煞兠塢Ⅴ٦ᵨ੪䵬㱮Ṱٲݴᑶᱸ䙺ټ佾ﲀ", a_);
						goto IL_1BE;
					case 1:
						goto IL_170;
					case 2:
						goto IL_170;
					case 3:
						format = HyperlinksCollectionEditor.b("猢圤䠦弨䈪䤬䨮䌰า破帶娸䤺刼䰾⹀╂ㅄ楆͈⹊㥌慎Ṑὒၔፖ᭘畚楜煞兠塢Ⅴ٦ᵨ੪䵬㱮Ṱٲݴᑶᱸ䙺ټ佾ﲀ", a_) + string.Format(HyperlinksCollectionEditor.b("ᠢ漤䈦崨ପ戬挮琰眲眴ശ紸娺䤼帾⍀≂㙄≆楈ᭊⱌ㱎≐⑒㩔╖㵘晚♜潞ᱠ", a_), (this.ᜀ as AccessExport).Password);
						num = 5;
						continue;
					case 4:
						goto IL_1AD;
					case 5:
						goto IL_E3;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1BE;
						default:
						{
							if (false)
							{
							}
							if (num2 >= A_2.Length)
							{
								num = 4;
								continue;
							}
							if (true)
							{
							}
							ParameterData parameterData = A_2[num2];
							this.ᜁ.Parameters.Add(new OleDbParameter(parameterData.Name, parameterData.Type, parameterData.Size, parameterData.ColumnName)).IsNullable = true;
							num2++;
							num = 2;
							continue;
						}
						}
						break;
					case 7:
						goto IL_E3;
					}
					break;
					IL_E3:
					this.ᜁ.Connection = new OleDbConnection(string.Format(format, A_0));
					this.ᜁ.Connection.Open();
					this.ᜁ.CommandText = A_1;
					num2 = 0;
					num = 1;
					continue;
					IL_170:
					num = 6;
					continue;
					IL_1BE:
					num = 7;
				}
			}
			IL_1AD:
			this.ᜁ.Prepare();
			return;
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000F5B4 File Offset: 0x0000E5B4
	internal void ᜀ(string A_0, IEnumerable<string> A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = string.Empty;
				int num = 0;
				for (;;)
				{
					OleDbConnection oleDbConnection;
					switch (num)
					{
					case 0:
						if ((this.ᜀ as AccessExport).Password.Length > 0)
						{
							num = 2;
							continue;
						}
						if (true)
						{
						}
						text = HyperlinksCollectionEditor.b("礨太䈬央堰圲倴䔶и瘺吼尾㍀ⱂ㙄⡆⽈㽊捌Վ㑐❒答ᡖᕘṚᥜᵞ你坢䭤坦剨⽪౬᭮ၰ卲♴ᡶ౸ॺṼ᩾벀떄惘", a_);
						num = 4;
						continue;
					case 1:
						goto IL_1E5;
					case 2:
						text = HyperlinksCollectionEditor.b("礨太䈬央堰圲倴䔶и瘺吼尾㍀ⱂ㙄⡆⽈㽊捌Վ㑐❒答ᡖᕘṚᥜᵞ你坢䭤坦剨⽪౬᭮ၰ卲♴ᡶ౸ॺṼ᩾벀떄惘", a_) + string.Format(HyperlinksCollectionEditor.b("ረ愪䠬嬮ᄰ簲礴父紸示ܼ笾⁀㝂⑄╆⡈㡊⡌潎Ő㉒♔⑖⹘㑚⽜㭞屠ᡢ啤ᩦ", a_), (this.ᜀ as AccessExport).Password);
						num = 1;
						continue;
					case 3:
						try
						{
							oleDbConnection.Open();
							OleDbCommand oleDbCommand = new OleDbCommand();
							try
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
									oleDbCommand.Connection = oleDbConnection;
									IEnumerator<string> enumerator = A_1.GetEnumerator();
									try
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 4;
												continue;
											case 2:
											{
												if (!enumerator.MoveNext())
												{
													num = 0;
													continue;
												}
												string commandText = enumerator.Current;
												oleDbCommand.CommandText = commandText;
												oleDbCommand.ExecuteNonQuery();
												num = 3;
												continue;
											}
											case 4:
												goto IL_120;
											}
											IL_D9:
											num = 2;
											continue;
											goto IL_D9;
										}
										IL_120:;
									}
									finally
									{
										num = 2;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_15F;
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
										IL_15F:;
									}
									break;
								}
								}
							}
							finally
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										((IDisposable)oleDbCommand).Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_19F;
									}
									if (oleDbCommand == null)
									{
										break;
									}
									num = 1;
								}
								IL_19F:;
							}
							return;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									((IDisposable)oleDbConnection).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_1E2;
								}
								if (oleDbConnection == null)
								{
									break;
								}
								num = 1;
							}
							IL_1E2:;
						}
						goto IL_1E5;
					case 4:
						goto IL_1E5;
					}
					break;
					IL_1E5:
					text = string.Format(text, A_0);
					oleDbConnection = new OleDbConnection(text);
					num = 3;
				}
			}
			return;
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000F880 File Offset: 0x0000E880
	public void ᜁ(string A_0)
	{
		int a_ = 18;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		}
		if (false)
		{
		}
		switch (0)
		{
		default:
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(HyperlinksCollectionEditor.b("紭䀯嬱䘳匵ᘷ縹崻䨽ℿ݁㱃㙅❇㡉㡋恍ᅏㅑ㝓㍕⭗⥙牛፝⑟⁡㭣⁥Ⅷ♩⥫", a_));
			try
			{
				int num = 3;
				for (;;)
				{
					int num2;
					byte[] array;
					switch (num)
					{
					case 0:
					{
						if (num2 > 0)
						{
							num = 1;
							continue;
						}
						FileStream fileStream;
						fileStream.Close();
						num = 7;
						continue;
					}
					case 1:
					{
						FileStream fileStream;
						fileStream.Write(array, 0, num2);
						num = 6;
						continue;
					}
					case 2:
					{
						FileStream fileStream = File.Create(A_0);
						num2 = 2048;
						array = new byte[2048];
						num = 4;
						continue;
					}
					case 4:
						goto IL_D9;
					case 5:
						goto IL_126;
					case 6:
						goto IL_D9;
					case 7:
						goto IL_11A;
					}
					if (A_0 != string.Empty)
					{
						num = 2;
						continue;
					}
					goto IL_11A;
					IL_D9:
					num2 = manifestResourceStream.Read(array, 0, array.Length);
					num = 0;
					continue;
					IL_11A:
					num = 5;
				}
				IL_126:;
			}
			finally
			{
				if (true)
				{
				}
				manifestResourceStream.Close();
			}
			break;
		}
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000F9D4 File Offset: 0x0000E9D4
	public bool ᜀ(string A_0)
	{
		int a_ = 1;
		bool result;
		for (;;)
		{
			switch (0)
			{
			default:
				try
				{
					for (;;)
					{
						Type typeFromProgID = Type.GetTypeFromProgID(HyperlinksCollectionEditor.b("尜嬞渠笢ତ搦䠨弪䰬䌮帰吲", a_));
						object obj = Activator.CreateInstance(typeFromProgID);
						if (true)
						{
						}
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								result = false;
								num = 3;
								continue;
							case 1:
								goto IL_106;
							case 2:
							{
								if (obj == null)
								{
									num = 0;
									continue;
								}
								typeFromProgID.InvokeMember(HyperlinksCollectionEditor.b("帜洞䐠䈢儤䈦", a_), BindingFlags.InvokeMethod, null, obj, new object[]
								{
									string.Format(HyperlinksCollectionEditor.b("䴜洞丠唢䰤䌦䰨太ာ戮堰倲䜴堶䨸吺嬼䬾潀ू⁄㍆杈ъŌ੎ᕐᅒ答捖睘歚晜᭞`ᝢѤ䝦㩨Ѫᡬᵮተᙲ䡴౶䥸ٺ", a_), A_0)
								});
								object target = typeFromProgID.InvokeMember(HyperlinksCollectionEditor.b("尜簞唠䨢匤䈦樨䐪䌬䄮吰倲䄴帶嘸唺", a_), BindingFlags.GetProperty, null, obj, null);
								typeFromProgID.InvokeMember(HyperlinksCollectionEditor.b("帜猞丠倢䀤", a_), BindingFlags.InvokeMethod, null, target, null);
								result = true;
								num = 1;
								continue;
							}
							case 3:
								goto IL_7B;
							}
							break;
						}
					}
					IL_7B:
					IL_106:;
				}
				catch (Exception)
				{
					result = false;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_123;
				}
				break;
			}
		}
		IL_123:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000FB28 File Offset: 0x0000EB28
	public void ᜀ(string A_0, bool A_1)
	{
		int a_ = 15;
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
				case 1:
					if (File.Exists(A_0))
					{
						goto IL_118;
					}
					goto IL_55;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_12D;
				}
				if (!A_1)
				{
					num = 2;
					continue;
				}
				goto IL_55;
			}
			IL_118:
			num = 3;
		}
		try
		{
			IL_55:
			num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ(A_0);
					num = 6;
					continue;
				case 1:
					if (!this.ᜀ(A_0))
					{
						num = 0;
						continue;
					}
					goto IL_CF;
				case 2:
					goto IL_CF;
				case 4:
					this.ᜁ(A_0);
					num = 2;
					continue;
				case 5:
					goto IL_D7;
				case 6:
					goto IL_CF;
				}
				if (sprᮌ.ᜀ())
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
				IL_CF:
				num = 5;
			}
			IL_D7:
			return;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + HyperlinksCollectionEditor.b("☪✬渮到倲倴䐶䨸氺似嘾㕀♂㝄絆獈ࡊ㽌⩎ぐ❒ごᙖ㩘㡚㡜ⱞበ❢Ѥ፦ࡨ४౬ᱮᑰ", a_));
		}
		IL_F9:
		if (true)
		{
		}
		return;
		IL_12D:
		goto IL_F9;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000FC74 File Offset: 0x0000EC74
	public void ᜀ(int A_0, string A_1)
	{
		if (A_1.Length != 0)
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
				this.ᜁ.Parameters[A_0].Value = A_1;
				return;
			}
		}
		this.ᜁ.Parameters[A_0].Value = DBNull.Value;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000FCEC File Offset: 0x0000ECEC
	internal void ᜀ(int A_0, byte[] A_1)
	{
		if (true)
		{
		}
		if (A_1 != null)
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
				this.ᜁ.Parameters[A_0].Value = A_1;
				return;
			}
		}
		this.ᜁ.Parameters[A_0].Value = DBNull.Value;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x0000FD60 File Offset: 0x0000ED60
	public OleDbCommand ᜀ()
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

	// Token: 0x04000088 RID: 136
	private ExportBase ᜀ;

	// Token: 0x04000089 RID: 137
	private OleDbCommand ᜁ = new OleDbCommand();

	// Token: 0x0400008A RID: 138
	private string ᜂ = string.Empty;
}
