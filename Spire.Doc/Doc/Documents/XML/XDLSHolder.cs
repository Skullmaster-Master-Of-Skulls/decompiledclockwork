using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents.XML
{
	// Token: 0x02000548 RID: 1352
	public class XDLSHolder
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600467B RID: 18043 RVA: 0x0040FCD0 File Offset: 0x0040ECD0
		// (set) Token: 0x0600467C RID: 18044 RVA: 0x0040FD14 File Offset: 0x0040ED14
		public int ID
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
				return this.ᜀ;
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x0040FD58 File Offset: 0x0040ED58
		// (set) Token: 0x0600467E RID: 18046 RVA: 0x0040FD9C File Offset: 0x0040ED9C
		public bool Cleared
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
				return this.ᜃ;
			}
			set
			{
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
							goto IL_7D;
						}
						if (false)
						{
						}
						break;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_6C;
					case 3:
						goto IL_7D;
					case 4:
						goto IL_90;
					}
					if (value != this.ᜃ)
					{
						num = 1;
						continue;
					}
					break;
					IL_7D:
					if (value)
					{
						if (true)
						{
						}
						num = 4;
					}
					else
					{
						this.ᜃ = false;
						num = 2;
					}
				}
				IL_6C:
				return;
				IL_90:
				this.ᜀ();
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x0040FE3C File Offset: 0x0040EE3C
		// (set) Token: 0x06004680 RID: 18048 RVA: 0x0040FE80 File Offset: 0x0040EE80
		public bool EnableID
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

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x0040FEC4 File Offset: 0x0040EEC4
		// (set) Token: 0x06004682 RID: 18050 RVA: 0x0040FF08 File Offset: 0x0040EF08
		public bool SkipMe
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

		// Token: 0x06004684 RID: 18052 RVA: 0x0040FF70 File Offset: 0x0040EF70
		public void AddElement(string tagName, object value)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_67:
				num = 2;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_38:
				switch (num)
				{
				case 0:
					goto IL_5A;
				case 2:
					goto IL_6F;
				}
				if (this.ᜁ != null)
				{
					goto IL_71;
				}
				num = 0;
			}
			IL_5A:
			this.ᜁ = new Dictionary<string, object>();
			goto IL_67;
			IL_6F:
			IL_71:
			this.ᜁ[tagName] = value;
			return;
			IL_20:
			if (false)
			{
			}
			if (true)
			{
			}
			num = 1;
			goto IL_38;
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x0040FFFC File Offset: 0x0040EFFC
		public void AddRefElement(string tagName, object value)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5F:
				if (true)
				{
				}
				num = 0;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 1:
					goto IL_52;
				}
				if (this.ᜂ != null)
				{
					goto IL_71;
				}
				num = 1;
			}
			IL_52:
			this.ᜂ = new Dictionary<string, object>();
			goto IL_5F;
			IL_6F:
			IL_71:
			this.ᜂ[tagName] = value;
			return;
			IL_20:
			if (false)
			{
			}
			num = 2;
			goto IL_30;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x00410088 File Offset: 0x0040F088
		public void WriteHolder(IXDLSContentWriter writer)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 1;
				Dictionary<string, object>.KeyCollection.Enumerator enumerator;
				for (;;)
				{
					Dictionary<string, object>.KeyCollection.Enumerator enumerator2;
					switch (num)
					{
					case 0:
						goto IL_126;
					case 2:
						enumerator = this.ᜂ.Keys.GetEnumerator();
						num = 5;
						continue;
					case 3:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator2.MoveNext())
									{
										num = 1;
										continue;
									}
									string text = enumerator2.Current;
									writer.WriteChildElement(text, this.ᜁ[text]);
									num = 2;
									continue;
								}
								case 1:
									num = 4;
									continue;
								case 4:
									goto IL_113;
								}
								IL_C7:
								num = 0;
								continue;
								goto IL_C7;
							}
							IL_113:
							goto IL_80;
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_126;
					case 4:
						if (this.ᜂ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 5:
						goto IL_7B;
					}
					if (this.ᜁ != null)
					{
						num = 0;
						continue;
					}
					IL_80:
					num = 4;
					continue;
					IL_126:
					enumerator2 = this.ᜁ.Keys.GetEnumerator();
					num = 3;
				}
				IL_7B:
				try
				{
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							IDocumentSerializable documentSerializable;
							if (documentSerializable != null)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 1:
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
						case 2:
							goto IL_211;
						case 5:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							string text2 = enumerator.Current;
							IDocumentSerializable documentSerializable = this.ᜂ[text2] as IDocumentSerializable;
							num = 0;
							continue;
						}
						case 6:
						{
							IDocumentSerializable documentSerializable;
							string text2;
							writer.WriteChildRefElement(text2, documentSerializable.XDLSHolder.ID);
							num = 4;
							continue;
						}
						}
						IL_178:
						num = 5;
						continue;
						goto IL_178;
					}
					IL_211:;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return;
			}
			}
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x004102D4 File Offset: 0x0040F2D4
		public bool ReadHolder(IXDLSContentReader reader)
		{
			int a_ = 11;
			switch (0)
			{
			default:
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
					int num = 17;
					object obj;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 4;
							continue;
						case 1:
						{
							string tagName = reader.TagName;
							num = 2;
							continue;
						}
						case 2:
							if (this.ᜁ != null)
							{
								num = 0;
								continue;
							}
							goto IL_EA;
						case 3:
						{
							string tagName;
							obj = this.ᜁ[tagName];
							num = 5;
							continue;
						}
						case 4:
						{
							string tagName;
							if (this.ᜁ.ContainsKey(tagName))
							{
								num = 3;
								continue;
							}
							goto IL_EA;
						}
						case 5:
							if (obj != null)
							{
								num = 13;
								continue;
							}
							goto IL_EA;
						case 6:
						{
							IXDLSFactory ixdlsfactory;
							obj = ixdlsfactory.Create(reader);
							string tagName;
							this.ᜁ[tagName] = obj;
							num = 7;
							continue;
						}
						case 7:
							goto IL_1F0;
						case 8:
						{
							IXDLSFactory ixdlsfactory;
							if (ixdlsfactory != null)
							{
								num = 6;
								continue;
							}
							goto IL_246;
						}
						case 9:
							num = 11;
							continue;
						case 10:
							goto IL_244;
						case 11:
						{
							string tagName;
							if (this.ᜂ.ContainsKey(tagName))
							{
								num = 18;
								continue;
							}
							return false;
						}
						case 12:
							this.ᜂ[reader.TagName] = -1;
							num = 10;
							continue;
						case 13:
						{
							IXDLSFactory ixdlsfactory = obj as IXDLSFactory;
							num = 8;
							continue;
						}
						case 14:
						{
							string attributeValue;
							if (attributeValue == null)
							{
								num = 12;
								continue;
							}
							this.ᜂ[reader.TagName] = XmlConvert.ToInt32(attributeValue);
							num = 16;
							continue;
						}
						case 15:
							if (this.ᜂ != null)
							{
								if (true)
								{
								}
								num = 9;
								continue;
							}
							return false;
						case 16:
							return false;
						case 18:
						{
							string attributeValue = reader.GetAttributeValue(ClipboardData.b("Ͱᙲ፴", a_));
							num = 14;
							continue;
						}
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							num = 1;
							continue;
						}
						return false;
						IL_EA:
						num = 15;
					}
					IL_1F0:
					goto IL_246;
					IL_244:
					break;
					IL_246:
					return reader.ReadChildElement(obj);
				}
				}
				return false;
			}
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x00410558 File Offset: 0x0040F558
		public void AfterDeserialization(IDocumentSerializable owner)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				Dictionary<string, object>.KeyCollection.Enumerator enumerator3;
				for (;;)
				{
					Dictionary<string, object>.KeyCollection.Enumerator enumerator2;
					switch (num)
					{
					case 0:
						if (this.ᜂ != null)
						{
							num = 2;
							continue;
						}
						goto IL_3FB;
					case 1:
						try
						{
							num = 4;
							for (;;)
							{
								IDocumentSerializable documentSerializable;
								switch (num)
								{
								case 0:
									goto IL_1DF;
								case 1:
								{
									if (documentSerializable != null)
									{
										num = 0;
										continue;
									}
									string key;
									IXDLSSerializableCollection ixdlsserializableCollection = this.ᜁ[key] as IXDLSSerializableCollection;
									num = 2;
									continue;
								}
								case 2:
								{
									IXDLSSerializableCollection ixdlsserializableCollection;
									if (ixdlsserializableCollection != null)
									{
										num = 9;
										continue;
									}
									break;
								}
								case 5:
									try
									{
										num = 6;
										for (;;)
										{
											switch (num)
											{
											case 0:
											{
												IDocumentSerializable documentSerializable2;
												if (documentSerializable2 != null)
												{
													num = 2;
													continue;
												}
												break;
											}
											case 1:
												goto IL_175;
											case 2:
											{
												IDocumentSerializable documentSerializable2;
												documentSerializable2.XDLSHolder.AfterDeserialization(documentSerializable2);
												num = 4;
												continue;
											}
											case 3:
											{
												IEnumerator enumerator;
												if (!enumerator.MoveNext())
												{
													num = 5;
													continue;
												}
												IDocumentSerializable documentSerializable2 = (IDocumentSerializable)enumerator.Current;
												num = 0;
												continue;
											}
											case 5:
												num = 1;
												continue;
											}
											IL_14C:
											num = 3;
											continue;
											goto IL_14C;
										}
										IL_175:
										break;
									}
									finally
									{
										for (;;)
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
														goto IL_1DC;
													case 1:
														disposable.Dispose();
														num = 0;
														continue;
													case 2:
														if (disposable == null)
														{
															goto IL_1DE;
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
															num = 1;
															continue;
														}
														break;
													}
													break;
												}
											}
										}
										IL_1DC:
										IL_1DE:;
									}
									goto IL_1DF;
								case 6:
								{
									if (!enumerator2.MoveNext())
									{
										num = 7;
										continue;
									}
									string key = enumerator2.Current;
									documentSerializable = (this.ᜁ[key] as IDocumentSerializable);
									num = 1;
									continue;
								}
								case 7:
									num = 8;
									continue;
								case 8:
									goto IL_2B0;
								case 9:
								{
									IXDLSSerializableCollection ixdlsserializableCollection;
									IEnumerator enumerator = ixdlsserializableCollection.GetEnumerator();
									num = 5;
									continue;
								}
								}
								goto IL_DC;
								IL_1DF:
								documentSerializable.XDLSHolder.AfterDeserialization(documentSerializable);
								num = 3;
								continue;
								IL_1F9:
								num = 6;
								continue;
								IL_DC:
								goto IL_1F9;
							}
							IL_2B0:
							goto IL_83;
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_2C3;
					case 2:
						enumerator3 = this.ᜂ.Keys.GetEnumerator();
						num = 4;
						continue;
					case 4:
						goto IL_7E;
					case 5:
						goto IL_2C3;
					}
					if (this.ᜁ != null)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					IL_83:
					num = 0;
					continue;
					IL_2C3:
					enumerator2 = this.ᜁ.Keys.GetEnumerator();
					num = 1;
				}
				IL_7E:
				try
				{
					num = 5;
					for (;;)
					{
						string text;
						int value;
						switch (num)
						{
						case 0:
							goto IL_38C;
						case 1:
							if (!enumerator3.MoveNext())
							{
								num = 9;
								continue;
							}
							text = enumerator3.Current;
							value = -1;
							num = 7;
							continue;
						case 2:
							goto IL_3EB;
						case 3:
							if (this.ᜂ[text] is int)
							{
								num = 8;
								continue;
							}
							goto IL_38C;
						case 6:
							num = 3;
							continue;
						case 7:
							if (this.ᜂ[text] != null)
							{
								num = 6;
								continue;
							}
							goto IL_38C;
						case 8:
							value = (int)this.ᜂ[text];
							num = 0;
							continue;
						case 9:
							num = 2;
							continue;
						}
						IL_322:
						num = 1;
						continue;
						goto IL_322;
						IL_38C:
						owner.RestoreReference(text, value);
						num = 4;
					}
					IL_3EB:;
				}
				finally
				{
					((IDisposable)enumerator3).Dispose();
				}
				IL_3FB:
				this.ᜀ();
				return;
			}
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x004109B4 File Offset: 0x0040F9B4
		public void BeforeSerialization()
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					Dictionary<string, object>.KeyCollection.Enumerator enumerator;
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
								case 0:
								{
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									string key = enumerator.Current;
									IDocumentSerializable documentSerializable = this.ᜁ[key] as IDocumentSerializable;
									num = 7;
									continue;
								}
								case 1:
								{
									IDocumentSerializable documentSerializable;
									documentSerializable.XDLSHolder.Cleared = true;
									documentSerializable.XDLSHolder.BeforeSerialization();
									num = 5;
									continue;
								}
								case 2:
								{
									int num2 = 0;
									IXDLSSerializableCollection ixdlsserializableCollection;
									IEnumerator enumerator2 = ixdlsserializableCollection.GetEnumerator();
									num = 6;
									continue;
								}
								case 3:
									num = 4;
									continue;
								case 4:
									goto IL_295;
								case 6:
									try
									{
										num = 0;
										for (;;)
										{
											IEnumerator enumerator2;
											IDocumentSerializable documentSerializable2;
											switch (num)
											{
											case 1:
												num = 3;
												continue;
											case 2:
											{
												documentSerializable2.XDLSHolder.Cleared = true;
												int num2;
												documentSerializable2.XDLSHolder.ID = num2;
												documentSerializable2.XDLSHolder.BeforeSerialization();
												num2++;
												num = 4;
												continue;
											}
											case 3:
												goto IL_1A9;
											case 5:
												if (!enumerator2.MoveNext())
												{
													num = 1;
													continue;
												}
												goto IL_11D;
											case 6:
												if (documentSerializable2 != null)
												{
													switch ((1 == 1) ? 1 : 0)
													{
													case 0:
													case 2:
														goto IL_11D;
													default:
														if (false)
														{
														}
														num = 2;
														continue;
													}
												}
												break;
											}
											IL_FD:
											num = 5;
											continue;
											goto IL_FD;
											IL_11D:
											documentSerializable2 = (IDocumentSerializable)enumerator2.Current;
											num = 6;
										}
										IL_1A9:;
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
													goto IL_1F1;
												case 2:
													if (disposable != null)
													{
														num = 0;
														continue;
													}
													goto IL_1F3;
												}
												break;
											}
										}
										IL_1F1:
										IL_1F3:;
									}
									break;
								case 7:
								{
									IDocumentSerializable documentSerializable;
									if (documentSerializable != null)
									{
										num = 1;
										continue;
									}
									string key;
									IXDLSSerializableCollection ixdlsserializableCollection = this.ᜁ[key] as IXDLSSerializableCollection;
									num = 9;
									continue;
								}
								case 9:
								{
									IXDLSSerializableCollection ixdlsserializableCollection;
									if (ixdlsserializableCollection != null)
									{
										num = 2;
										continue;
									}
									break;
								}
								}
								IL_1F4:
								num = 0;
								continue;
								goto IL_1F4;
							}
							IL_295:
							return;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_2A5;
					case 1:
						goto IL_2A5;
					}
					if (true)
					{
					}
					if (this.ᜁ != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_2A5:
					enumerator = this.ᜁ.Keys.GetEnumerator();
					num = 0;
				}
				return;
			}
			}
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00410CC0 File Offset: 0x0040FCC0
		private void ᜀ()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ.Clear();
					num = 5;
					continue;
				case 1:
					this.ᜂ.Clear();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8D;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
					if (this.ᜂ != null)
					{
						goto IL_8D;
					}
					goto IL_B2;
				case 3:
					goto IL_7B;
				case 5:
					goto IL_7D;
				}
				if (this.ᜁ != null)
				{
					num = 0;
					continue;
				}
				IL_7D:
				num = 2;
				continue;
				IL_8D:
				num = 1;
			}
			IL_7B:
			IL_B2:
			this.ᜃ = true;
		}

		// Token: 0x040036A9 RID: 13993
		private int ᜀ = -1;

		// Token: 0x040036AA RID: 13994
		private Dictionary<string, object> ᜁ;

		// Token: 0x040036AB RID: 13995
		private byte \u2609\u009B\u009D\u0092;

		// Token: 0x040036AC RID: 13996
		private long[] \u2609\u00A7\u00B0\u0097;

		// Token: 0x040036AD RID: 13997
		private Dictionary<string, object> ᜂ;

		// Token: 0x040036AE RID: 13998
		private bool ᜃ = true;

		// Token: 0x040036AF RID: 13999
		private int \u2593\u00B0\u00AF\u00AE;

		// Token: 0x040036B0 RID: 14000
		private bool ᜄ;

		// Token: 0x040036B1 RID: 14001
		private bool ᜅ;
	}
}
