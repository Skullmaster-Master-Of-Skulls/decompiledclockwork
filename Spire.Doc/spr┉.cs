using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;

// Token: 0x0200022F RID: 559
internal class spr\u2509
{
	// Token: 0x06001AB6 RID: 6838 RVA: 0x001BE204 File Offset: 0x001BD204
	public static XmlSchema ᜀ()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Stream stream = spr\u2509.ᜀ(ClipboardData.b("ᵸ᝺๼剾ꎌ", a_));
		return XmlSchema.Read(stream, new ValidationEventHandler(spr\u2509.ᜀ));
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x001BE270 File Offset: 0x001BD270
	public XmlSchema ᜂ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Stream a_2 = spr\u2509.ᜀ(ClipboardData.b("౧٩Ὣ䍭ᵯ᝱s᝵啷ॹύᙽꢅ", a_));
		XmlDocument a_3 = spr\u2509.ᜀ(a_2);
		return this.ᜀ(a_3);
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x001BE2D8 File Offset: 0x001BD2D8
	public XmlSchema ᜀ(XmlDocument A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			this.ᜅ = new XmlNamespaceManager(A_0.NameTable);
			this.ᜅ.AddNamespace(ClipboardData.b("ᵯ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻੽慎ꊋ뮓튕풗즙톛ﮝ풟쎡얥삧쾩솫쾭麯쪱잳튵", a_));
			this.ᜆ = A_0.DocumentElement;
			XmlNodeList xmlNodeList = this.ᜆ.SelectNodes(ClipboardData.b("ᵯ䡱ᵳᡵ᭷ᙹॻ᩽", a_), this.ᜅ);
			IEnumerator enumerator = xmlNodeList.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_2F4;
					case 1:
						num = 0;
						continue;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						XmlNode xmlNode = (XmlNode)enumerator.Current;
						string value = xmlNode.Attributes[ClipboardData.b("ṯ፱ᥳ፵", a_)].Value;
						string value2 = xmlNode.Attributes[ClipboardData.b("ṯ፱ᥳ፵୷੹ᵻᵽ", a_)].Value;
						Stream a_2 = this.ᜀ(value, value2);
						XmlDocument a_3 = spr\u2509.ᜀ(a_2);
						this.ᜀ(A_0, a_3);
						num = 3;
						continue;
					}
					}
					IL_237:
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
					goto IL_237;
				}
				IL_2F4:
				goto IL_145;
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
							goto IL_349;
						case 2:
							goto IL_33F;
						}
						break;
					}
				}
				IL_33F:
				if (true)
				{
				}
				IL_349:;
			}
			goto IL_34A;
			for (;;)
			{
				IL_145:
				this.ᜄ = new XmlSchema();
				XmlElement xmlElement = this.ᜆ[ClipboardData.b("ᵯ䡱ٳ᥵᝷๹", a_)];
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.Name = xmlElement.Attributes[ClipboardData.b("ṯ፱ᥳ፵", a_)].Value;
				xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(xmlElement.Attributes[ClipboardData.b("ѯୱѳ፵", a_)].Value);
				this.ᜄ.Items.Add(xmlSchemaElement);
				XmlNodeList xmlNodeList2 = this.ᜆ.SelectNodes(ClipboardData.b("ᵯ䡱sཱུࡷό", a_), this.ᜅ);
				IEnumerator enumerator2 = xmlNodeList2.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 2:
							goto IL_F7;
						case 3:
							num = 2;
							continue;
						case 4:
						{
							if (!enumerator2.MoveNext())
							{
								num = 3;
								continue;
							}
							XmlNode a_4 = (XmlNode)enumerator2.Current;
							this.ᜀ(a_4);
							num = 0;
							continue;
						}
						}
						IL_B0:
						num = 4;
						continue;
						goto IL_B0;
					}
					IL_F7:
					break;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator2 as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_142;
							case 1:
								disposable2.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_144;
							}
							break;
						}
					}
					IL_142:
					IL_144:;
				}
			}
			IL_34A:
			return this.ᜄ;
		}
		}
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x001BE654 File Offset: 0x001BD654
	protected virtual Stream ᜀ(string A_0, string A_1)
	{
		int a_ = 0;
		if (A_1 == ClipboardData.b("㕥ᡧͩṫ୭幯㙱᭳ᕵ噷㹹ほ⵽", a_))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_41;
				}
			}
			IL_41:
			if (true)
			{
			}
			if (false)
			{
			}
			return spr\u2509.ᜀ(A_0);
		}
		return null;
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x001BE6B8 File Offset: 0x001BD6B8
	protected static Stream ᜀ(string A_0)
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
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		return executingAssembly.GetManifestResourceStream(ClipboardData.b("㡪ᵬٮͰᙲ孴㍶ᙸ᡺卼⵾ﮈﲎ뾐", a_) + A_0);
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x001BE71C File Offset: 0x001BD71C
	protected static XmlDocument ᜀ(Stream A_0)
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
		StreamReader streamReader = new StreamReader(A_0);
		string xml = streamReader.ReadToEnd();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);
		return xmlDocument;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x001BE774 File Offset: 0x001BD774
	protected static void ᜀ(object A_0, ValidationEventArgs A_1)
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
		throw new DLSException(A_1.Message);
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x001BE7BC File Offset: 0x001BD7BC
	private void ᜀ(XmlNode A_0)
	{
		int a_ = 1;
		for (;;)
		{
			XmlAttribute xmlAttribute = A_0.Attributes[ClipboardData.b("੦٨ཪ࡬", a_)];
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 14;
					continue;
				case 1:
					num = 11;
					continue;
				case 2:
					num = 8;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
				{
					string value;
					if (!(value == ClipboardData.b("ɦݨṪl", a_)))
					{
						num = 2;
						continue;
					}
					goto IL_8C;
				}
				case 5:
					if (xmlAttribute != null)
					{
						num = 1;
						continue;
					}
					this.ᜀ(new XmlSchemaChoice(), A_0, false);
					num = 10;
					continue;
				case 6:
				{
					string value;
					if (!(value == ClipboardData.b("ᑦᥨ੪๬੮", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_CF;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						string value;
						if (!(value == ClipboardData.b("ѦŨѪѬ౮ᑰ", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_1C1;
					}
					}
					break;
				case 8:
				{
					string value;
					if (!(value == ClipboardData.b("ᝦࡨὪᥬ੮Ͱᵲ", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_1B8;
				}
				case 9:
					return;
				case 10:
					goto IL_14E;
				case 11:
				{
					string value;
					if ((value = xmlAttribute.Value) != null)
					{
						num = 13;
						continue;
					}
					return;
				}
				case 12:
					num = 6;
					continue;
				case 13:
					num = 7;
					continue;
				case 14:
				{
					if (true)
					{
					}
					string value;
					if (!(value == ClipboardData.b("f᭨ѪᡬὮᡰᵲቴ", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_128;
				}
				}
				break;
			}
		}
		IL_8C:
		this.ᜀ(A_0, ModeType.Enum);
		return;
		IL_CF:
		this.ᜀ(A_0, ModeType.Space);
		return;
		IL_128:
		this.ᜀ(new XmlSchemaSequence(), A_0, true);
		return;
		IL_14E:
		return;
		IL_1B8:
		this.ᜀ(A_0, ModeType.Pattern);
		return;
		IL_1C1:
		this.ᜀ(new XmlSchemaChoice(), A_0, false);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x001BE9F4 File Offset: 0x001BD9F4
	private void ᜀ(XmlNode A_0, ModeType A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			XmlSchemaSimpleType xmlSchemaSimpleType;
			for (;;)
			{
				xmlSchemaSimpleType = new XmlSchemaSimpleType();
				xmlSchemaSimpleType.Name = A_0.Attributes[ClipboardData.b("᭴ᙶᑸṺ", a_)].Value;
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
				xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName(ClipboardData.b("ٴͶ୸ቺ፼᡾", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ꞈﲊ뺌ꆎﺐ뢖ꮘꮚ궜꺞躠ﮢ直좪얬쪮\udcb0튲", a_));
				xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_241;
					case 1:
						goto IL_2BF;
					case 2:
						goto IL_257;
					case 3:
						goto IL_12C;
					case 4:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_1D7;
								case 3:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 4;
										continue;
									}
									XmlNode xmlNode = (XmlNode)enumerator.Current;
									XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
									xmlSchemaEnumerationFacet.Value = xmlNode.Attributes[ClipboardData.b("ʹᙶᕸ๺᡼", a_)].Value;
									xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
									num = 0;
									continue;
								}
								case 4:
									num = 1;
									continue;
								}
								IL_159:
								num = 3;
								continue;
								goto IL_159;
							}
							IL_1D7:
							goto IL_2FD;
						}
						finally
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
											disposable.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_23E;
										case 2:
											if (disposable != null)
											{
												num = 0;
												continue;
											}
											goto IL_240;
										}
										break;
									}
								}
								IL_23E:
								break;
							}
							IL_240:;
						}
						goto IL_241;
					case 5:
						switch (A_1)
						{
						case ModeType.Enum:
						{
							XmlNodeList xmlNodeList = A_0.SelectNodes(ClipboardData.b("ᡴ䵶ᱸᕺࡼቾ", a_), this.ᜅ);
							IEnumerator enumerator = xmlNodeList.GetEnumerator();
							num = 4;
							continue;
						}
						case ModeType.Pattern:
						{
							XmlNode xmlNode2 = A_0.SelectSingleNode(ClipboardData.b("ᡴ䵶ॸ᩺ॼ୾", a_), this.ᜅ);
							XmlSchemaPatternFacet xmlSchemaPatternFacet = new XmlSchemaPatternFacet();
							xmlSchemaPatternFacet.Value = xmlNode2.Attributes[ClipboardData.b("ʹᙶᕸ๺᡼", a_)].Value;
							xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaPatternFacet);
							num = 3;
							continue;
						}
						case ModeType.Space:
						{
							XmlNode xmlNode3 = A_0.SelectSingleNode(ClipboardData.b("ᡴ䵶๸፺ᑼ୾", a_), this.ᜅ);
							XmlSchemaWhiteSpaceFacet xmlSchemaWhiteSpaceFacet = new XmlSchemaWhiteSpaceFacet();
							xmlSchemaWhiteSpaceFacet.Value = xmlNode3.Attributes[ClipboardData.b("ʹᙶᕸ๺᡼", a_)].Value;
							xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaWhiteSpaceFacet);
							num = 1;
							continue;
						}
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
					IL_241:
					num = 2;
				}
			}
			IL_12C:
			IL_257:
			goto IL_2FD;
			IL_2BF:
			if (true)
			{
			}
			IL_2FD:
			this.ᜄ.Items.Add(xmlSchemaSimpleType);
			return;
		}
		}
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x001BED20 File Offset: 0x001BDD20
	private void ᜀ(XmlSchemaGroupBase A_0, XmlNode A_1, bool A_2)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			for (;;)
			{
				xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = A_1.Attributes[ClipboardData.b("ᱱᕳ᭵ᵷ", a_)].Value;
				int num = 4;
				for (;;)
				{
					if (true)
					{
					}
					IEnumerator enumerator;
					IEnumerator enumerator2;
					switch (num)
					{
					case 0:
					{
						string name = xmlSchemaComplexType.Name + ClipboardData.b("㍱sɵ੷㵹๻ᅽ", a_);
						XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = new XmlSchemaAttributeGroupRef();
						xmlSchemaAttributeGroupRef.RefName = new XmlQualifiedName(name);
						xmlSchemaComplexType.Attributes.Add(xmlSchemaAttributeGroupRef);
						XmlSchemaAttributeGroup xmlSchemaAttributeGroup = new XmlSchemaAttributeGroup();
						xmlSchemaAttributeGroup.Name = name;
						this.ᜄ.Items.Add(xmlSchemaAttributeGroup);
						XmlSchemaObjectCollection attributes = xmlSchemaAttributeGroup.Attributes;
						num = 1;
						continue;
					}
					case 1:
						goto IL_4FB;
					case 2:
					{
						string name2 = xmlSchemaComplexType.Name + ClipboardData.b("㕱ٳ᥵൷੹", a_);
						xmlSchemaComplexType.Particle = new XmlSchemaGroupRef
						{
							RefName = new XmlQualifiedName(name2)
						};
						XmlSchemaGroup xmlSchemaGroup = new XmlSchemaGroup();
						xmlSchemaGroup.Name = name2;
						xmlSchemaGroup.Particle = A_0;
						this.ᜄ.Items.Add(xmlSchemaGroup);
						num = 3;
						continue;
					}
					case 3:
						goto IL_274;
					case 4:
						if (A_2)
						{
							num = 2;
							continue;
						}
						goto IL_37B;
					case 5:
						goto IL_274;
					case 6:
						if (A_2)
						{
							num = 0;
							continue;
						}
						goto IL_4FB;
					case 7:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 1:
									num = 3;
									continue;
								case 3:
									goto IL_32D;
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num = 1;
										continue;
									}
									XmlNode a_2 = (XmlNode)enumerator.Current;
									XmlSchemaObjectCollection attributes;
									this.ᜀ(a_2, attributes);
									num = 0;
									continue;
								}
								}
								IL_2E4:
								num = 4;
								continue;
								goto IL_2E4;
							}
							IL_32D:
							goto IL_5A5;
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
										goto IL_378;
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
										goto IL_37A;
									}
									break;
								}
							}
							IL_378:
							IL_37A:;
						}
						goto IL_37B;
					case 8:
					{
						goto IL_41E;
						try
						{
							for (;;)
							{
								IL_41E:
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_41E;
										default:
										{
											if (false)
											{
											}
											if (!enumerator2.MoveNext())
											{
												num = 3;
												continue;
											}
											XmlNode a_3 = (XmlNode)enumerator2.Current;
											this.ᜁ(a_3, A_0);
											num = 4;
											continue;
										}
										}
										break;
									case 2:
										goto IL_4AD;
									case 3:
										num = 2;
										continue;
									}
									IL_468:
									num = 0;
									continue;
									goto IL_468;
								}
							}
							IL_4AD:
							goto IL_3E8;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator2 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable2 != null)
										{
											num = 2;
											continue;
										}
										goto IL_4FA;
									case 1:
										goto IL_4F8;
									case 2:
										disposable2.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_4F8:
							IL_4FA:;
						}
						goto IL_4FB;
						IL_3E8:
						XmlNodeList xmlNodeList = A_1.SelectNodes(ClipboardData.b("ά乳ᅵ੷ᕹॻ๽", a_), this.ᜅ);
						IEnumerator enumerator3 = xmlNodeList.GetEnumerator();
						num = 9;
						continue;
					}
					case 9:
					{
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									IEnumerator enumerator3;
									if (!enumerator3.MoveNext())
									{
										num = 7;
										continue;
									}
									XmlNode xmlNode = (XmlNode)enumerator3.Current;
									XmlAttribute xmlAttribute = xmlNode.Attributes[ClipboardData.b("qᅳၵ", a_)];
									num = 3;
									continue;
								}
								case 2:
								{
									XmlAttribute xmlAttribute;
									XmlNode a_4 = this.ᜆ.SelectSingleNode(ClipboardData.b("ά乳ᅵ੷ᕹॻ๽\udb7f슁놋ꦍ", a_) + xmlAttribute.Value + ClipboardData.b("啱⥳", a_), this.ᜅ);
									this.ᜀ(a_4, A_0);
									num = 6;
									continue;
								}
								case 3:
								{
									XmlAttribute xmlAttribute;
									if (xmlAttribute != null)
									{
										num = 2;
										continue;
									}
									XmlNode xmlNode;
									this.ᜀ(xmlNode, A_0);
									num = 4;
									continue;
								}
								case 5:
									goto IL_226;
								case 7:
									num = 5;
									continue;
								}
								IL_1F4:
								num = 0;
								continue;
								goto IL_1F4;
							}
							IL_226:
							goto IL_3BD;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator3;
								IDisposable disposable3 = enumerator3 as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable3.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable3 != null)
										{
											num = 0;
											continue;
										}
										goto IL_273;
									case 2:
										goto IL_271;
									}
									break;
								}
							}
							IL_271:
							IL_273:;
						}
						goto IL_274;
						IL_3BD:
						XmlSchemaObjectCollection attributes = xmlSchemaComplexType.Attributes;
						num = 6;
						continue;
					}
					}
					break;
					IL_274:
					this.ᜄ.Items.Add(xmlSchemaComplexType);
					XmlNodeList xmlNodeList2 = A_1.SelectNodes(ClipboardData.b("ά乳፵ᑷόᅻ᭽", a_), this.ᜅ);
					enumerator2 = xmlNodeList2.GetEnumerator();
					num = 8;
					continue;
					IL_37B:
					xmlSchemaComplexType.Particle = A_0;
					xmlSchemaComplexType.Particle.MaxOccursString = ClipboardData.b("ݱᩳᑵ᝷ཹቻ᩽", a_);
					xmlSchemaComplexType.Particle.MinOccurs = 0m;
					num = 5;
					continue;
					IL_4FB:
					XmlNodeList xmlNodeList3 = A_1.SelectNodes(ClipboardData.b("ά乳᝵౷๹๻᝽", a_), this.ᜅ);
					enumerator = xmlNodeList3.GetEnumerator();
					num = 7;
				}
			}
			IL_5A5:
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = ClipboardData.b("᭱ၳ", a_);
			xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName(ClipboardData.b("᭱ᩳɵ", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ꢅﾇ릉ꊋ뮓꒕ꢗꪙ궛놝쮧슩즫쎭톯", a_));
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
			return;
		}
		}
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x001BF370 File Offset: 0x001BE370
	private void ᜁ(XmlNode A_0, XmlSchemaGroupBase A_1)
	{
		int a_ = 4;
		XmlSchemaElement xmlSchemaElement;
		for (;;)
		{
			xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = A_0.Attributes[ClipboardData.b("ѩ൫ͭᕯ", a_)].Value;
			XmlAttribute xmlAttribute = A_0.Attributes[ClipboardData.b("ṩᕫṭᕯ", a_)];
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!((IList)this.ᜃ).Contains(xmlAttribute.Value))
					{
						xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(xmlAttribute.Value);
						num = 3;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_105;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_103;
				case 2:
					xmlSchemaElement.SchemaTypeName = new XmlQualifiedName(xmlAttribute.Value, ClipboardData.b("ɩᡫᩭo䡱孳奵ཷ൹୻偽놁ꪃ慎ꎋ벍ꂏꊑꖓ릕삗힙킛춝쎟쪡솣쮥즧", a_));
					num = 1;
					continue;
				case 3:
					goto IL_CD;
				}
				break;
			}
		}
		IL_CD:
		IL_103:
		IL_105:
		A_1.Items.Add(xmlSchemaElement);
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x001BF490 File Offset: 0x001BE490
	private void ᜀ(XmlNode A_0, XmlSchemaGroupBase A_1)
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
		XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
		xmlSchemaElement.Name = A_0.Attributes[ClipboardData.b("०ࡨ٪࡬", a_)].Value;
		A_1.Items.Add(xmlSchemaElement);
		XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
		xmlSchemaElement.SchemaType = xmlSchemaComplexType;
		XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
		xmlSchemaComplexType.Particle = xmlSchemaSequence;
		xmlSchemaComplexType.Particle.MaxOccursString = ClipboardData.b("ቦݨ४ɬᩮὰᝲၴ፶", a_);
		xmlSchemaComplexType.Particle.MinOccurs = 0m;
		XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
		xmlSchemaElement2.Name = A_0.Attributes[ClipboardData.b("๦ᵨ๪l", a_)].Value;
		xmlSchemaElement2.SchemaTypeName = new XmlQualifiedName(A_0.Attributes[ClipboardData.b("፦ၨ᭪࡬", a_)].Value);
		xmlSchemaSequence.Items.Add(xmlSchemaElement2);
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x001BF5B0 File Offset: 0x001BE5B0
	private void ᜀ(XmlNode A_0, XmlSchemaObjectCollection A_1)
	{
		int a_ = 8;
		XmlSchemaAttribute xmlSchemaAttribute;
		for (;;)
		{
			xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = A_0.Attributes[ClipboardData.b("mᅯάᅳ", a_)].Value;
			XmlAttribute xmlAttribute = A_0.Attributes[ClipboardData.b("ᩭ९ɱᅳ", a_)];
			XmlAttribute xmlAttribute2 = A_0.Attributes[ClipboardData.b("࡭᥯ੱᅳት", a_)];
			int num = 0;
			for (;;)
			{
				IL_15:
				switch (num)
				{
				case 0:
					while (!((IList)this.ᜃ).Contains(xmlAttribute.Value))
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
							xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName(xmlAttribute.Value);
							num = 1;
							goto IL_15;
						}
					}
					num = 5;
					continue;
				case 1:
					goto IL_C0;
				case 2:
					xmlSchemaAttribute.FixedValue = xmlAttribute2.Value;
					num = 3;
					continue;
				case 3:
					goto IL_166;
				case 4:
					goto IL_C0;
				case 5:
					xmlSchemaAttribute.SchemaTypeName = new XmlQualifiedName(xmlAttribute.Value, ClipboardData.b("٭ѯٱѳ䱵坷啹୻ॽ겁떅ꚇﺋ뾏ꂑ꒓ꚕꦗ떙쒛펝잣캥춧잩춫", a_));
					num = 4;
					continue;
				case 6:
					if (xmlAttribute2 != null)
					{
						num = 2;
						continue;
					}
					goto IL_168;
				}
				break;
				IL_C0:
				if (true)
				{
				}
				num = 6;
			}
		}
		IL_166:
		IL_168:
		A_1.Add(xmlSchemaAttribute);
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x001BF730 File Offset: 0x001BE730
	private void ᜀ(XmlDocument A_0, XmlDocument A_1)
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
		this.ᜀ(A_1, A_0, ClipboardData.b("٪坬᭮ࡰͲၴ", a_));
		this.ᜀ(A_1, A_0, ClipboardData.b("٪坬࡮Ͱᱲtݶ", a_));
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x001BF7A0 File Offset: 0x001BE7A0
	private void ᜀ(XmlDocument A_0, XmlDocument A_1, string A_2)
	{
		int a_ = 18;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			XmlNodeList xmlNodeList = A_0.DocumentElement.SelectNodes(A_2, this.ᜅ);
			IEnumerator enumerator = xmlNodeList.GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1A6;
					case 3:
					{
						XmlNode xmlNode = A_1.CreateNode(XmlNodeType.Element, ClipboardData.b("౷όᅻ๽", a_), string.Empty);
						XmlNode xmlNode2;
						xmlNode.InnerXml = xmlNode2.OuterXml;
						A_1.DocumentElement.AppendChild(xmlNode.FirstChild);
						num = 7;
						continue;
					}
					case 4:
						num = 0;
						continue;
					case 5:
					{
						XmlNode xmlNode3;
						if (xmlNode3 == null)
						{
							num = 3;
							continue;
						}
						string innerXml = xmlNode3.InnerXml;
						XmlNode xmlNode2;
						xmlNode3.InnerXml = innerXml + xmlNode2.InnerXml;
						num = 1;
						continue;
					}
					case 6:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						XmlNode xmlNode2 = (XmlNode)enumerator.Current;
						string value = xmlNode2.Attributes[ClipboardData.b("ᙷ᭹ᅻ᭽", a_)].Value;
						XmlNode xmlNode3 = A_1.DocumentElement.SelectSingleNode(A_2 + ClipboardData.b("⍷㩹ቻώ릃ꆅ", a_) + value + ClipboardData.b("彷❹", a_), this.ᜅ);
						num = 5;
						continue;
					}
					}
					IL_FE:
					num = 6;
					continue;
					goto IL_FE;
				}
				IL_1A6:;
			}
			finally
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
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_20A;
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
								goto IL_20C;
							}
							break;
						}
					}
					IL_20A:
					break;
				}
				IL_20C:;
			}
			return;
		}
		}
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x001BF9D8 File Offset: 0x001BE9D8
	public spr\u2509()
	{
		int a_ = 8;
		this.ᜃ = new string[]
		{
			ClipboardData.b("ᵭѯqᵳᡵί", a_),
			ClipboardData.b("࡭ᱯᵱᕳɵ", a_),
			ClipboardData.b("౭Ὧᵱᡳ፵᥷ᑹ", a_),
			ClipboardData.b("ݭṯٱ", a_),
			ClipboardData.b("੭ᅯٱᅳɵᅷ᝹᥻", a_),
			ClipboardData.b("౭ᅯűᅳ䁵䱷㡹ᕻၽﶃ", a_)
		};
		base..ctor();
	}

	// Token: 0x04001E63 RID: 7779
	protected const string ᜀ = "Spire.Doc.Resources";

	// Token: 0x04001E64 RID: 7780
	private const string ᜁ = "http://www.w3.org/2001/XMLSchema";

	// Token: 0x04001E65 RID: 7781
	private const string ᜂ = "http://tempuri.org/DLSMetaSchema.xsd";

	// Token: 0x04001E66 RID: 7782
	private readonly string[] ᜃ;

	// Token: 0x04001E67 RID: 7783
	private XmlSchema ᜄ;

	// Token: 0x04001E68 RID: 7784
	private XmlNamespaceManager ᜅ;

	// Token: 0x04001E69 RID: 7785
	private XmlElement ᜆ;
}
