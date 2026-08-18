using System;
using System.Collections.Generic;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000D4 RID: 212
	public class CustomDocumentProperties : DocumentSerializable
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000181E0 File Offset: 0x000171E0
		public Dictionary<string, DocumentProperty> CustomHash
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
		}

		// Token: 0x170000CD RID: 205
		public DocumentProperty this[string name]
		{
			get
			{
				if (this.ᜄ.ContainsKey(name))
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
						return this.ᜄ[name];
					}
				}
				return null;
			}
		}

		// Token: 0x170000CE RID: 206
		public DocumentProperty this[int index]
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 0;
					Dictionary<string, DocumentProperty>.KeyCollection.Enumerator enumerator = this.ᜄ.Keys.GetEnumerator();
					DocumentProperty result;
					try
					{
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									goto IL_75;
								}
								string key = enumerator.Current;
								num2 = 5;
								continue;
							}
							case 3:
								goto IL_E4;
							case 4:
							{
								string key;
								result = this.ᜄ[key];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_75;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							}
							case 5:
								if (num == index)
								{
									num2 = 4;
									continue;
								}
								num++;
								num2 = 1;
								continue;
							case 6:
								goto IL_F2;
							case 7:
								num2 = 6;
								continue;
							}
							IL_63:
							num2 = 0;
							continue;
							goto IL_63;
							IL_75:
							num2 = 7;
						}
						IL_E4:
						return result;
						IL_F2:
						goto IL_2D;
					}
					finally
					{
						if (true)
						{
						}
						((IDisposable)enumerator).Dispose();
					}
					return result;
					IL_2D:
					return null;
				}
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000183AC File Offset: 0x000173AC
		public int Count
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
				return this.ᜄ.Count;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000183F4 File Offset: 0x000173F4
		internal CustomDocumentProperties() : this(0)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00018408 File Offset: 0x00017408
		internal CustomDocumentProperties(int A_0) : base(null, null)
		{
			this.ᜄ = new Dictionary<string, DocumentProperty>(A_0);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001842C File Offset: 0x0001742C
		public DocumentProperty Add(string name, object value)
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
			DocumentProperty documentProperty = new DocumentProperty(name, value, DocumentProperty.ᜀ(value));
			this.ᜄ.Add(name, documentProperty);
			return documentProperty;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00018484 File Offset: 0x00017484
		public void Remove(string name)
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
			this.CustomHash.Remove(name);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000184CC File Offset: 0x000174CC
		public CustomDocumentProperties Clone()
		{
			switch (0)
			{
			default:
			{
				CustomDocumentProperties customDocumentProperties = new CustomDocumentProperties(this.ᜄ.Count);
				Dictionary<string, DocumentProperty>.KeyCollection.Enumerator enumerator = this.ᜄ.Keys.GetEnumerator();
				try
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_A8:
						num = 3;
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
						case 2:
							goto IL_CE;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							string key = enumerator.Current;
							DocumentProperty documentProperty = this.ᜄ[key];
							customDocumentProperties.ᜄ.Add(key, documentProperty.Clone());
							num = 1;
							continue;
						}
						case 4:
							num = 2;
							continue;
						}
						break;
					}
					goto IL_A8;
					IL_CE:;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return customDocumentProperties;
			}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000185DC File Offset: 0x000175DC
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 11;
			switch (0)
			{
			default:
				for (;;)
				{
					base.WriteXmlContent(writer);
					IXDLSAttributeWriter ixdlsattributeWriter = writer as IXDLSAttributeWriter;
					XmlWriter xmlWriter = (writer as sprṑ).ᜀ();
					int num = 4;
					for (;;)
					{
						Dictionary<string, DocumentProperty>.KeyCollection.Enumerator enumerator;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							if (true)
							{
							}
							if (this.ᜄ.Count > 0)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							goto IL_426;
						case 3:
							goto IL_7A;
							try
							{
								for (;;)
								{
									IL_7A:
									num = 22;
									for (;;)
									{
										switch (num)
										{
										case 0:
										{
											PropertyType propertyType;
											if (propertyType != PropertyType.Int)
											{
												num = 4;
												continue;
											}
											xmlWriter.WriteAttributeString(ClipboardData.b("╰ੲմቶ", a_), ClipboardData.b("ᡰᵲŴ", a_));
											DocumentProperty documentProperty;
											ixdlsattributeWriter.WriteValue(ClipboardData.b("❰ቲᥴɶᱸ", a_), documentProperty.Integer);
											num = 20;
											continue;
										}
										case 1:
											num = 14;
											continue;
										case 2:
											goto IL_17A;
										case 3:
											goto IL_17A;
										case 4:
											num = 16;
											continue;
										case 5:
											num = 10;
											continue;
										case 6:
											num = 12;
											continue;
										case 8:
										{
											PropertyType propertyType;
											if (propertyType <= PropertyType.Bool)
											{
												num = 18;
												continue;
											}
											num = 0;
											continue;
										}
										case 9:
											goto IL_17A;
										case 10:
										{
											PropertyType propertyType;
											if (propertyType != PropertyType.Bool)
											{
												num = 6;
												continue;
											}
											xmlWriter.WriteAttributeString(ClipboardData.b("╰ੲմቶ", a_), ClipboardData.b("፰ᱲᩴ᭶", a_));
											DocumentProperty documentProperty;
											ixdlsattributeWriter.WriteValue(ClipboardData.b("❰ቲᥴɶᱸ", a_), documentProperty.Boolean);
											num = 9;
											continue;
										}
										case 11:
											num = 19;
											continue;
										case 12:
											goto IL_17A;
										case 13:
										{
											PropertyType propertyType;
											if (propertyType != PropertyType.Double)
											{
												num = 5;
												continue;
											}
											xmlWriter.WriteAttributeString(ClipboardData.b("╰ੲմቶ", a_), ClipboardData.b("ᕰᱲtᕶᕸṺ", a_));
											DocumentProperty documentProperty;
											ixdlsattributeWriter.WriteValue(ClipboardData.b("❰ቲᥴɶᱸ", a_), documentProperty.Double);
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_7A;
											default:
												if (false)
												{
												}
												num = 21;
												continue;
											}
											break;
										}
										case 14:
											goto IL_17A;
										case 15:
										{
											PropertyType propertyType;
											if (propertyType != PropertyType.DateTime)
											{
												num = 1;
												continue;
											}
											xmlWriter.WriteAttributeString(ClipboardData.b("╰ੲմቶ", a_), ClipboardData.b("㕰ቲŴቶ⵸ቺၼ᩾", a_));
											DocumentProperty documentProperty;
											ixdlsattributeWriter.WriteValue(ClipboardData.b("❰ቲᥴɶᱸ", a_), documentProperty.DateTime);
											num = 3;
											continue;
										}
										case 16:
										{
											PropertyType propertyType;
											if (propertyType != PropertyType.String)
											{
												num = 17;
												continue;
											}
											xmlWriter.WriteAttributeString(ClipboardData.b("╰ੲմቶ", a_), ClipboardData.b("ɰݲݴṶ᝸ᱺ", a_));
											DocumentProperty documentProperty;
											ixdlsattributeWriter.WriteValue(ClipboardData.b("❰ቲᥴɶᱸ", a_), documentProperty.ToString());
											num = 2;
											continue;
										}
										case 17:
											num = 15;
											continue;
										case 18:
											num = 13;
											continue;
										case 19:
											goto IL_416;
										case 20:
											goto IL_17A;
										case 21:
											goto IL_17A;
										case 23:
										{
											if (!enumerator.MoveNext())
											{
												num = 11;
												continue;
											}
											string text = enumerator.Current;
											DocumentProperty documentProperty = this.ᜄ[text];
											xmlWriter.WriteStartElement(ClipboardData.b("ŰŲᩴݶᱸॺॼپ", a_));
											xmlWriter.WriteAttributeString(ClipboardData.b("㽰ቲᡴቶ", a_), text);
											PropertyType propertyType = documentProperty.PropertyType;
											num = 8;
											continue;
										}
										}
										goto IL_EC;
										IL_17A:
										xmlWriter.WriteEndElement();
										num = 7;
										continue;
										IL_23C:
										num = 23;
										continue;
										IL_EC:
										goto IL_23C;
									}
								}
								IL_416:
								return;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_426;
						case 4:
							if (this.ᜄ != null)
							{
								num = 0;
								continue;
							}
							return;
						}
						break;
						IL_426:
						enumerator = this.ᜄ.Keys.GetEnumerator();
						num = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00018A84 File Offset: 0x00017A84
		protected override bool ReadXmlContent(IXDLSContentReader reader)
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
			bool result = base.ReadXmlContent(reader);
			this.ᜀ(reader as XDLSReader);
			return result;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00018AD4 File Offset: 0x00017AD4
		private void ᜀ(XDLSReader A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_3E7:
					num = 16;
					break;
				default:
					if (false)
					{
					}
					num = 8;
					break;
				}
				object a_2;
				for (;;)
				{
					XmlReader innerReader;
					string text2;
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
						goto IL_139;
					case 2:
						if (!A_0.InnerReader.IsEmptyElement)
						{
							num = 27;
							continue;
						}
						goto IL_30C;
					case 3:
						spr᧓.ᝉ = new Dictionary<string, int>(7)
						{
							{
								ClipboardData.b("ၱ᭳᥵ᑷ", a_),
								0
							},
							{
								ClipboardData.b("űsѵᅷᑹ᭻", a_),
								1
							},
							{
								ClipboardData.b("㙱ᕳɵᵷ⹹ᕻ፽", a_),
								2
							},
							{
								ClipboardData.b("᭱ᩳɵ", a_),
								3
							},
							{
								ClipboardData.b("ᙱ᭳͵᩷ᙹ᥻", a_),
								4
							},
							{
								ClipboardData.b("፱ٳѵ᥷͹", a_),
								5
							},
							{
								ClipboardData.b("ᅱᡳήࡷ", a_),
								6
							}
						};
						num = 17;
						continue;
					case 4:
						goto IL_139;
					case 5:
						num = 23;
						continue;
					case 6:
						goto IL_30C;
					case 7:
						goto IL_30C;
					case 9:
						if (innerReader.LocalName == ClipboardData.b("ɱٳ᥵ࡷό๻੽勵", a_))
						{
							num = 22;
							continue;
						}
						return;
					case 10:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 19;
							continue;
						}
						A_0.InnerReader.Read();
						num = 1;
						continue;
					case 11:
						goto IL_32F;
					case 12:
						goto IL_C7;
					case 13:
					{
						string key;
						int num2;
						if (spr᧓.ᝉ.TryGetValue(key, out num2))
						{
							num = 25;
							continue;
						}
						goto IL_30C;
					}
					case 14:
						goto IL_30C;
					case 15:
						goto IL_30C;
					case 16:
						goto IL_30C;
					case 17:
						goto IL_359;
					case 18:
						goto IL_30C;
					case 19:
						a_2 = A_0.ReadChildBinaryElement();
						num = 24;
						continue;
					case 20:
					{
						string key;
						string text;
						if ((key = text) != null)
						{
							num = 5;
							continue;
						}
						goto IL_30C;
					}
					case 21:
					{
						int num2;
						switch (num2)
						{
						case 0:
							goto IL_3CD;
						case 1:
							a_2 = A_0.ReadString(ClipboardData.b("⑱ᕳ᩵൷ό", a_));
							num = 26;
							continue;
						case 2:
							a_2 = A_0.ReadDateTime(ClipboardData.b("⑱ᕳ᩵൷ό", a_));
							num = 15;
							continue;
						case 3:
							a_2 = A_0.ReadInt(ClipboardData.b("⑱ᕳ᩵൷ό", a_));
							num = 6;
							continue;
						case 4:
							a_2 = A_0.ReadDouble(ClipboardData.b("⑱ᕳ᩵൷ό", a_));
							num = 18;
							continue;
						case 5:
							num = 2;
							continue;
						case 6:
						{
							string a_3 = A_0.ReadString(ClipboardData.b("⑱ᕳ᩵൷ό", a_));
							sprᱵ sprᱵ = new sprᱵ();
							sprᱵ.ᜀ(a_3);
							a_2 = sprᱵ;
							num = 14;
							continue;
						}
						default:
							num = 0;
							continue;
						}
						break;
					}
					case 22:
					{
						string text = A_0.ReadString(ClipboardData.b("♱൳ٵᵷ", a_));
						text2 = A_0.ReadString(ClipboardData.b("㱱ᕳ᭵ᵷ", a_));
						a_2 = null;
						if (true)
						{
						}
						num = 20;
						continue;
					}
					case 23:
						if (spr᧓.ᝉ == null)
						{
							num = 3;
							continue;
						}
						goto IL_359;
					case 24:
						goto IL_30C;
					case 25:
						num = 21;
						continue;
					case 26:
						goto IL_30C;
					case 27:
						A_0.InnerReader.ReadStartElement();
						num = 4;
						continue;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					innerReader = A_0.InnerReader;
					num = 9;
					continue;
					IL_139:
					num = 10;
					continue;
					IL_30C:
					DocumentProperty value = new DocumentProperty(text2, a_2);
					this.ᜄ.Add(text2, value);
					num = 11;
					continue;
					IL_359:
					num = 13;
				}
				IL_C7:
				throw new ArgumentNullException(ClipboardData.b("qᅳ᝵ᱷό๻", a_));
				IL_32F:
				return;
				IL_3CD:
				a_2 = A_0.ReadBoolean(ClipboardData.b("⑱ᕳ᩵൷ό", a_));
				goto IL_3E7;
			}
			}
		}

		// Token: 0x04000C49 RID: 3145
		internal new const string ᜀ = "property";

		// Token: 0x04000C4A RID: 3146
		internal const string ᜁ = "name";

		// Token: 0x04000C4B RID: 3147
		private float \u2593\u007F\u0087\u00AB;

		// Token: 0x04000C4C RID: 3148
		internal const string ᜂ = "pid";

		// Token: 0x04000C4D RID: 3149
		internal const string ᜃ = "fmtid";

		// Token: 0x04000C4E RID: 3150
		private float[] \u2593\u009C\u008B\u007F;

		// Token: 0x04000C4F RID: 3151
		private float \u25D9\u009E\u0098\u00A0;

		// Token: 0x04000C50 RID: 3152
		internal Dictionary<string, DocumentProperty> ᜄ;
	}
}
