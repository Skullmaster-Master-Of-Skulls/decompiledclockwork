using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Interface;

// Token: 0x020003E8 RID: 1000
internal class spr\u22DE
{
	// Token: 0x06003811 RID: 14353 RVA: 0x00347F98 File Offset: 0x00346F98
	public void ᜀ(IDocument A_0, string A_1)
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
		XslTransform xslTransform = new XslTransform();
		xslTransform.Load(spr\u22DE.ᜃ(), null, null);
		MemoryStream memoryStream = new MemoryStream();
		this.\u171D = (Document)A_0;
		A_0.SaveToFile(memoryStream, FileFormat.Xml);
		memoryStream.Position = 0L;
		this.\u171C.Load(memoryStream);
		this.ᜂ();
		XmlTextWriter xmlTextWriter = new XmlTextWriter(A_1, Encoding.UTF8);
		xslTransform.Transform(this.\u171C, null, xmlTextWriter, null);
		xmlTextWriter.Close();
	}

	// Token: 0x06003812 RID: 14354 RVA: 0x0034803C File Offset: 0x0034703C
	public void ᜀ(IDocument A_0, Stream A_1)
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
		XslTransform xslTransform = new XslTransform();
		xslTransform.Load(spr\u22DE.ᜃ(), null, null);
		MemoryStream memoryStream = new MemoryStream();
		this.\u171D = (Document)A_0;
		A_0.SaveToFile(memoryStream, FileFormat.Xml);
		memoryStream.Position = 0L;
		this.\u171C.Load(memoryStream);
		this.ᜂ();
		XmlTextWriter xmlTextWriter = new XmlTextWriter(A_1, Encoding.UTF8);
		xslTransform.Transform(this.\u171C, null, xmlTextWriter, null);
		xmlTextWriter.Close();
	}

	// Token: 0x06003813 RID: 14355 RVA: 0x003480E0 File Offset: 0x003470E0
	private static XmlReader ᜃ()
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(ClipboardData.b("╵ࡷ፹๻᭽깿욁ꚇ\ud889ﶍﾏﶗ늛쾟킡삣钥얧용芫횭쎯\udeb1삳", a_));
		return new XmlTextReader(manifestResourceStream);
	}

	// Token: 0x06003814 RID: 14356 RVA: 0x00348148 File Offset: 0x00347148
	private void ᜂ()
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
		this.ᜁ();
		this.ᜀ();
	}

	// Token: 0x06003815 RID: 14357 RVA: 0x00348190 File Offset: 0x00347190
	private void ᜁ()
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			XmlNodeList xmlNodeList = this.\u171C.DocumentElement.SelectNodes(string.Format(ClipboardData.b("൵䡷ݹ卻ս녿ﾁ", a_), ClipboardData.b("յᵷ᥹ࡻ᝽", a_), ClipboardData.b("յᵷ᥹ࡻ᝽", a_)));
			int num = 0;
			this.\u1718 = new int[xmlNodeList.Count];
			IEnumerator enumerator = xmlNodeList.GetEnumerator();
			try
			{
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2A6;
					case 2:
						num2 = 0;
						continue;
					case 3:
						goto IL_238;
					case 4:
						this.\u1718[num] = 1;
						num2 = 3;
						continue;
					case 5:
					{
						XmlNode xmlNode;
						if (xmlNode.Attributes[ClipboardData.b("㑵੷όᵻᕽ썿", a_)].InnerText == ClipboardData.b("㡵᝷㡹๻᭽", a_))
						{
							num2 = 4;
							continue;
						}
						goto IL_238;
					}
					case 6:
					{
						XmlNode xmlNode;
						if (xmlNode.Attributes[ClipboardData.b("㑵੷όᵻᕽ썿", a_)] != null)
						{
							num2 = 8;
							continue;
						}
						goto IL_238;
					}
					case 7:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 2;
							continue;
						}
						if (true)
						{
						}
						XmlNode xmlNode = (XmlNode)enumerator.Current;
						this.\u1718[num] = 0;
						num2 = 6;
						continue;
					}
					case 8:
						num2 = 5;
						continue;
					}
					IL_215:
					num2 = 7;
					continue;
					goto IL_215;
					IL_238:
					num++;
					num2 = 1;
				}
				IL_2A6:
				goto IL_166;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (disposable != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_2F3;
						case 1:
							goto IL_2F1;
						case 2:
							disposable.Dispose();
							num2 = 1;
							continue;
						}
						break;
					}
				}
				IL_2F1:
				IL_2F3:;
			}
			return;
			for (;;)
			{
				IL_166:
				num = 1;
				IEnumerator enumerator2 = xmlNodeList.GetEnumerator();
				try
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
						{
							if (!enumerator2.MoveNext())
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
									num2 = 4;
									continue;
								}
							}
							XmlNode a_2 = (XmlNode)enumerator2.Current;
							this.ᜀ(a_2, xmlNodeList.Count, num);
							num++;
							num2 = 2;
							continue;
						}
						case 3:
							goto IL_118;
						case 4:
							num2 = 3;
							continue;
						}
						IL_A9:
						num2 = 1;
						continue;
						goto IL_A9;
					}
					IL_118:
					break;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator2 as IDisposable;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								disposable2.Dispose();
								num2 = 1;
								continue;
							case 1:
								goto IL_163;
							case 2:
								if (disposable2 != null)
								{
									num2 = 0;
									continue;
								}
								goto IL_165;
							}
							break;
						}
					}
					IL_163:
					IL_165:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06003816 RID: 14358 RVA: 0x003484C8 File Offset: 0x003474C8
	private void ᜀ(XmlNode A_0, int A_1, int A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				XmlNode xmlNode;
				XmlElement xmlElement;
				XmlNode xmlNode2;
				switch (num)
				{
				case 0:
					goto IL_3E8;
				case 1:
					if (xmlNode != null)
					{
						num = 5;
						continue;
					}
					goto IL_319;
				case 2:
					goto IL_463;
				case 4:
					xmlElement.SetAttribute(ClipboardData.b("╥ݧѩᡫݭṯݱᅳ", a_), ClipboardData.b("㉥ᩧὩ५", a_));
					num = 18;
					continue;
				case 5:
					xmlElement.AppendChild(xmlNode.Clone());
					xmlNode.ParentNode.RemoveChild(xmlNode);
					num = 8;
					continue;
				case 6:
					this.\u1717 = (A_0.Attributes[ClipboardData.b("⑥ᩧཀྵ൫խ㍯ᵱၳ፵", a_)].InnerText == ClipboardData.b("⡥൧ᵩ㱫཭ᝯ᝱", a_));
					num = 20;
					continue;
				case 7:
					if (this.\u1718[A_2 - 1] == 1)
					{
						num = 4;
						continue;
					}
					goto IL_27F;
				case 8:
					goto IL_319;
				case 9:
					if (xmlNode2 != null)
					{
						num = 14;
						continue;
					}
					goto IL_2F1;
				case 10:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_205;
							case 2:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
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
										num = 3;
										continue;
									}
								}
								XmlNode a_2 = (XmlNode)enumerator.Current;
								this.ᜄ(a_2);
								num = 1;
								continue;
							}
							case 3:
								num = 0;
								continue;
							}
							IL_1A2:
							num = 2;
							continue;
							goto IL_1A2;
						}
						IL_205:
						goto IL_2F1;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
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
									goto IL_252;
								case 1:
									disposable.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_250;
								}
								break;
							}
						}
						IL_250:
						IL_252:;
					}
					goto IL_253;
				case 11:
					return;
				case 12:
					if (true)
					{
					}
					num = 19;
					continue;
				case 13:
					if (this.\u1717)
					{
						num = 16;
						continue;
					}
					return;
				case 14:
				{
					IEnumerator enumerator = xmlNode2.ChildNodes.GetEnumerator();
					num = 10;
					continue;
				}
				case 15:
					if (this.\u1718[A_2 - 1] == 1)
					{
						num = 17;
						continue;
					}
					goto IL_253;
				case 16:
				{
					xmlElement = this.\u171C.CreateElement(ClipboardData.b("ཥᱧཀྵū", a_));
					XmlAttribute xmlAttribute = this.\u171C.CreateAttribute(ClipboardData.b("ብᅧᩩ५", a_));
					xmlAttribute.InnerText = ClipboardData.b("㙥१ᡩ൫७ɯ፱ѳṵ", a_);
					XmlAttribute xmlAttribute2 = this.\u171C.CreateAttribute(ClipboardData.b("㕥୧ṩ㱫ᱭo", a_));
					xmlAttribute2.InnerText = ClipboardData.b("㉥ᩧὩ५", a_);
					xmlElement.Attributes.Append(xmlAttribute);
					xmlElement.Attributes.Append(xmlAttribute2);
					num = 7;
					continue;
				}
				case 17:
					this.\u1717 = true;
					num = 21;
					continue;
				case 18:
					goto IL_27F;
				case 19:
					if (A_2 == A_1)
					{
						num = 0;
						continue;
					}
					goto IL_463;
				case 20:
					goto IL_2C7;
				case 21:
					goto IL_253;
				case 22:
					if (A_1 != 1)
					{
						num = 12;
						continue;
					}
					goto IL_3E8;
				}
				if (A_0.Attributes[ClipboardData.b("⑥ᩧཀྵ൫խ㍯ᵱၳ፵", a_)] != null)
				{
					num = 6;
					continue;
				}
				goto IL_2C7;
				IL_253:
				num = 22;
				continue;
				IL_27F:
				XmlNode xmlNode3;
				xmlElement.AppendChild(xmlNode3.Clone());
				XmlNode xmlNode4;
				xmlElement.AppendChild(xmlNode4.Clone());
				num = 1;
				continue;
				IL_2C7:
				num = 15;
				continue;
				IL_2F1:
				num = 13;
				continue;
				IL_319:
				xmlNode2.AppendChild(xmlElement);
				xmlNode3.ParentNode.RemoveChild(xmlNode3);
				xmlNode4.ParentNode.RemoveChild(xmlNode4);
				num = 11;
				continue;
				IL_3E8:
				this.\u1717 = false;
				num = 2;
				continue;
				IL_463:
				XmlAttribute xmlAttribute3 = this.\u171C.CreateAttribute(ClipboardData.b("㙥ᩧթᱫ❭ṯ㝱ᩳት⡷᭹๻", a_));
				xmlAttribute3.InnerText = this.\u1717.ToString();
				A_0.Attributes.Append(xmlAttribute3);
				XmlNode xmlNode5 = A_0.SelectSingleNode(ClipboardData.b("ѥݧ๩ᕫ", a_));
				xmlNode2 = xmlNode5.SelectSingleNode(ClipboardData.b("ᙥ१ᡩ൫७ɯ፱ѳṵ୷", a_));
				xmlNode3 = A_0.SelectSingleNode(ClipboardData.b("ᙥ१൩५䍭ͯ᝱s͵ࡷ", a_));
				xmlNode = A_0.SelectSingleNode(ClipboardData.b("եݧ٩ᥫͭṯű", a_));
				xmlNode4 = A_0.SelectSingleNode(ClipboardData.b("๥൧୩࡫୭ɯű女ၵ᝷ᕹࡻ᭽", a_));
				num = 9;
			}
			return;
		}
		}
	}

	// Token: 0x06003817 RID: 14359 RVA: 0x00348A10 File Offset: 0x00347A10
	private void ᜄ(XmlNode A_0)
	{
		int a_ = 14;
		int num = 16;
		for (;;)
		{
			XmlNode xmlNode;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
					if (false)
					{
					}
					if (this.\u1719)
					{
						num = 14;
						continue;
					}
					return;
				}
				break;
			case 1:
				num = 15;
				continue;
			case 2:
				goto IL_261;
			case 3:
				if (xmlNode != null)
				{
					num = 5;
					continue;
				}
				goto IL_D3;
			case 4:
			{
				XmlNode xmlNode2 = A_0.SelectSingleNode(ClipboardData.b("ѳ᝵੷᭹᭻౽ꮅﺋ", a_));
				num = 9;
				continue;
			}
			case 5:
				this.ᜂ(xmlNode);
				num = 10;
				continue;
			case 6:
				if (A_0.Attributes[ClipboardData.b("sཱུࡷό", a_)].Value == ClipboardData.b("⁳᝵᩷ᙹ᥻", a_))
				{
					num = 17;
					continue;
				}
				goto IL_155;
			case 7:
				num = 12;
				continue;
			case 8:
				return;
			case 9:
			{
				XmlNode xmlNode2;
				if (xmlNode2 != null)
				{
					num = 1;
					continue;
				}
				goto IL_261;
			}
			case 10:
				goto IL_D3;
			case 11:
			{
				XmlNode xmlNode2;
				this.\u1719 = (xmlNode2.Attributes[ClipboardData.b("⑳᝵ίό㹻౽입ﺉﲍ", a_)].InnerText == ClipboardData.b("⁳ѵ൷ό", a_).ToLower());
				num = 2;
				continue;
			}
			case 12:
			{
				XmlNode xmlNode2;
				if (xmlNode2.Attributes[ClipboardData.b("⑳᝵ίό㹻౽입ﺉﲍ", a_)] != null)
				{
					num = 11;
					continue;
				}
				goto IL_261;
			}
			case 13:
				goto IL_155;
			case 14:
			{
				XmlAttribute xmlAttribute = this.\u171C.CreateAttribute(ClipboardData.b("㙳ѵᵷ᭹᝻㱽", a_));
				xmlAttribute.InnerText = ClipboardData.b("⁳ѵ൷ό", a_);
				A_0.Attributes.Append(xmlAttribute);
				this.\u1719 = false;
				num = 8;
				continue;
			}
			case 15:
			{
				XmlNode xmlNode2;
				if (xmlNode2.Attributes.Count > 0)
				{
					num = 7;
					continue;
				}
				goto IL_261;
			}
			case 17:
				this.ᜃ(A_0);
				num = 13;
				continue;
			}
			goto IL_61;
			IL_72:
			num = 4;
			continue;
			IL_61:
			if (A_0.ChildNodes.Count > 0)
			{
				goto IL_72;
			}
			goto IL_261;
			IL_D3:
			num = 0;
			continue;
			IL_155:
			xmlNode = A_0.SelectSingleNode(ClipboardData.b("ᵳɵᵷ᝹ཻ", a_));
			if (true)
			{
			}
			num = 3;
			continue;
			IL_261:
			num = 6;
		}
	}

	// Token: 0x06003818 RID: 14360 RVA: 0x00348CD0 File Offset: 0x00347CD0
	private void ᜃ(XmlNode A_0)
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
		spr\u22DE.ᜅ ᜅ = new spr\u22DE.ᜅ();
		ᜅ.ᜀ(A_0);
		ᜅ.ᜁ();
	}

	// Token: 0x06003819 RID: 14361 RVA: 0x00348D20 File Offset: 0x00347D20
	private void ᜂ(XmlNode A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			XmlNodeList xmlNodeList = A_0.SelectNodes(ClipboardData.b("ᵳɵᵷ᝹", a_));
			IEnumerator enumerator = xmlNodeList.GetEnumerator();
			try
			{
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string innerText;
						if (!(innerText == ClipboardData.b("⑳ή᭷๹ॻ౽", a_)))
						{
							num = 1;
							continue;
						}
						XmlNode xmlNode;
						this.ᜁ(xmlNode);
						num = 2;
						continue;
					}
					case 1:
						goto IL_DA;
					case 3:
					{
						XmlNode xmlNode;
						if (xmlNode.Attributes[ClipboardData.b("sཱུࡷό", a_)] != null)
						{
							num = 19;
							continue;
						}
						break;
					}
					case 6:
						num = 14;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DA;
						default:
						{
							if (false)
							{
							}
							string innerText;
							if (!(innerText == ClipboardData.b("㙳᥵᝷ᅹᅻώ힃", a_)))
							{
								num = 11;
								continue;
							}
							XmlNode xmlNode;
							int num2 = this.\u171B.ᜁ(xmlNode.Attributes[ClipboardData.b("㙳᥵᝷ᅹᅻώ쪃", a_)].InnerText);
							XmlAttribute xmlAttribute = this.\u171C.CreateAttribute(ClipboardData.b("ᙳ᥵᝷ᅹᅻώ춃슅", a_));
							xmlAttribute.InnerText = num2.ToString();
							xmlNode.Attributes.Append(xmlAttribute);
							num = 4;
							continue;
						}
						}
						break;
					case 10:
					{
						if (!enumerator.MoveNext())
						{
							num = 15;
							continue;
						}
						XmlNode xmlNode = (XmlNode)enumerator.Current;
						num = 3;
						continue;
					}
					case 11:
						num = 13;
						continue;
					case 13:
					{
						string innerText;
						if (!(innerText == ClipboardData.b("㙳᥵᝷ᅹᅻώ솃", a_)))
						{
							num = 6;
							continue;
						}
						XmlNode xmlNode;
						string text = this.\u171B.ᜀ(xmlNode.Attributes[ClipboardData.b("㙳᥵᝷ᅹᅻώ쪃", a_)].InnerText);
						XmlAttribute xmlAttribute2 = this.\u171C.CreateAttribute(ClipboardData.b("ᙳ᥵᝷ᅹᅻώ춃슅", a_));
						xmlAttribute2.InnerText = text.ToString();
						xmlNode.Attributes.Append(xmlAttribute2);
						num = 8;
						continue;
					}
					case 14:
					{
						string innerText;
						if (!(innerText == ClipboardData.b("㙳ѵᵷ᭹᝻", a_)))
						{
							num = 17;
							continue;
						}
						this.\u1719 = true;
						num = 5;
						continue;
					}
					case 15:
						num = 20;
						continue;
					case 16:
						num = 0;
						continue;
					case 17:
						num = 12;
						continue;
					case 18:
					{
						string innerText;
						XmlNode xmlNode;
						if ((innerText = xmlNode.Attributes[ClipboardData.b("sཱུࡷό", a_)].InnerText) != null)
						{
							num = 16;
							continue;
						}
						break;
					}
					case 19:
						num = 18;
						continue;
					case 20:
						goto IL_37A;
					}
					goto IL_A6;
					IL_DA:
					num = 7;
					continue;
					IL_201:
					num = 10;
					continue;
					IL_A6:
					goto IL_201;
				}
				IL_37A:;
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
							goto IL_3C4;
						case 1:
							goto IL_3C2;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_3C2:
				IL_3C4:;
			}
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x0600381A RID: 14362 RVA: 0x00349118 File Offset: 0x00348118
	private void ᜀ()
	{
		int a_ = 13;
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				IL_58:
				XmlNode xmlNode = this.\u171C.DocumentElement.SelectSingleNode(ClipboardData.b("ᅲtṶᕸེᑼᅾ검麗ﾌﮎ", a_));
				XmlAttribute xmlAttribute = xmlNode.Attributes[ClipboardData.b("㙲ᅴṶ൸⽺ᑼቾ", a_)];
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
						{
							XmlNode xmlNode2 = this.\u171C.SelectSingleNode(ClipboardData.b("㱲㡴㭶", a_));
							XmlAttribute xmlAttribute2 = this.\u171C.CreateAttribute(ClipboardData.b("⍲ݴᡶ൸ṺṼ୾펆ﮊ", a_));
							xmlAttribute2.InnerText = ClipboardData.b("㉲ᥴ᭶ᙸ౺㉼ᅾ廒욄", a_);
							xmlNode2.Attributes.Append(xmlAttribute2);
							num = 3;
							continue;
						}
						case 1:
							num = 4;
							continue;
						case 2:
							goto IL_9A;
						case 3:
							return;
						case 4:
							if (xmlNode.Attributes[ClipboardData.b("㝲ᩴᑶ⩸ṺṼ੾ﺆ", a_)].InnerText == 8.ToString())
							{
								num = 0;
								continue;
							}
							return;
						}
						goto IL_58;
					}
					IL_9A:
					if (xmlNode.Attributes[ClipboardData.b("㝲ᩴᑶ⩸ṺṼ੾ﺆ", a_)] == null)
					{
						return;
					}
					num = 1;
				}
			}
			return;
		}
	}

	// Token: 0x0600381B RID: 14363 RVA: 0x003492A4 File Offset: 0x003482A4
	private void ᜁ(XmlNode A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			double num2;
			string text;
			for (;;)
			{
				NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
				numberFormatInfo.CurrencyDecimalSeparator = ClipboardData.b("䅮", a_);
				double num = 0.0;
				num2 = 0.0;
				XmlNode xmlNode = A_0.SelectSingleNode(ClipboardData.b("ٮᱰቲቴቶ", a_));
				bool flag = A_0.Attributes[ClipboardData.b("♮ɰ㹲ၴͶᡸᵺᑼ፾", a_)].InnerText == ClipboardData.b("㭮Ͱٲၴ", a_).ToLower();
				XmlAttribute xmlAttribute = this.\u171C.CreateAttribute(ClipboardData.b("ⅮၰṲၴ", a_));
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						Image image = this.ᜀ(xmlNode, flag);
						num3 = 22;
						continue;
					}
					case 1:
					{
						string innerText = A_0.Attributes[ClipboardData.b("㡮ᡰᝲŴὶ⩸᡺ᱼ፾", a_)].InnerText;
						Image image;
						num = Convert.ToDouble(innerText, numberFormatInfo) / 100.0 * (double)image.Width;
						num3 = 19;
						continue;
					}
					case 2:
						if (A_0.Attributes[ClipboardData.b("㡮ᡰᝲŴὶ⩸᡺ᱼ፾", a_)] != null)
						{
							num3 = 23;
							continue;
						}
						goto IL_20A;
					case 3:
						goto IL_37C;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B7;
						default:
							if (false)
							{
							}
							if (!flag)
							{
								num3 = 18;
								continue;
							}
							goto IL_496;
						}
						break;
					case 5:
					{
						Image image;
						if (image != null)
						{
							num3 = 13;
							continue;
						}
						goto IL_496;
					}
					case 6:
						if (!flag)
						{
							num3 = 1;
							continue;
						}
						goto IL_20A;
					case 7:
					{
						xmlAttribute.InnerText = string.Format(ClipboardData.b("ᡮṰŲᅴ᩶ᕸ䅺剼偾婢뎂\ud886몊ꆎﶒ", a_), this.\u171A, flag ? ClipboardData.b("ɮ", a_) : ClipboardData.b("n", a_));
						A_0.Attributes.Append(xmlAttribute);
						text = string.Empty;
						Image image = null;
						num3 = 14;
						continue;
					}
					case 8:
						if (A_0.Attributes[ClipboardData.b("ᡮᡰᝲŴὶ", a_)] != null)
						{
							num3 = 21;
							continue;
						}
						goto IL_41B;
					case 9:
						num2 = Convert.ToDouble(A_0.Attributes[ClipboardData.b("ݮᑰᩲቴὶ൸", a_)].InnerText, numberFormatInfo);
						num3 = 3;
						continue;
					case 10:
						if (A_0.Attributes[ClipboardData.b("ݮᑰᩲቴὶ൸", a_)] != null)
						{
							num3 = 9;
							continue;
						}
						goto IL_37C;
					case 11:
					{
						Image image;
						if (image != null)
						{
							num3 = 12;
							continue;
						}
						goto IL_20A;
					}
					case 12:
						num3 = 2;
						continue;
					case 13:
						num3 = 20;
						continue;
					case 14:
						if (xmlNode != null)
						{
							num3 = 0;
							continue;
						}
						goto IL_3E6;
					case 15:
						num3 = 4;
						continue;
					case 16:
						goto IL_41B;
					case 17:
						goto IL_2B7;
					case 18:
					{
						string innerText2 = A_0.Attributes[ClipboardData.b("❮ᑰᩲቴὶ൸⡺ṼṾ", a_)].InnerText;
						Image image;
						num2 = Convert.ToDouble(innerText2, numberFormatInfo) / 100.0 * (double)image.Height;
						num3 = 17;
						continue;
					}
					case 19:
						goto IL_20A;
					case 20:
						if (A_0.Attributes[ClipboardData.b("❮ᑰᩲቴὶ൸⡺ṼṾ", a_)] != null)
						{
							num3 = 15;
							continue;
						}
						goto IL_496;
					case 21:
						num = Convert.ToDouble(A_0.Attributes[ClipboardData.b("ᡮᡰᝲŴὶ", a_)].InnerText, numberFormatInfo);
						num3 = 16;
						continue;
					case 22:
						goto IL_3E6;
					case 23:
						num3 = 6;
						continue;
					}
					break;
					IL_20A:
					if (true)
					{
					}
					text += string.Format(ClipboardData.b("ᡮᡰᝲŴὶ䍸孺ټ佾ﲀ뢂", a_), num);
					num3 = 10;
					continue;
					IL_37C:
					num3 = 5;
					continue;
					IL_3E6:
					num3 = 8;
					continue;
					IL_41B:
					num3 = 11;
				}
			}
			IL_2B7:
			IL_496:
			text += string.Format(ClipboardData.b("ݮᑰᩲቴὶ൸䅺嵼Ѿ놀ﺂ", a_), num2);
			XmlAttribute xmlAttribute2 = this.\u171C.CreateAttribute(ClipboardData.b("ᱮհੲᥴቶ", a_));
			xmlAttribute2.InnerText = text;
			A_0.Attributes.Append(xmlAttribute2);
			this.\u171A++;
			return;
		}
		}
	}

	// Token: 0x0600381C RID: 14364 RVA: 0x003497AC File Offset: 0x003487AC
	private byte[] ᜀ(XmlNode A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(A_0.OuterXml));
				xmlTextReader.Read();
				int num = 0;
				array = new byte[0];
				byte[] array2 = new byte[1000];
				if (true)
				{
				}
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return array;
					case 1:
						if (num >= array2.Length)
						{
							goto IL_FD;
						}
						return array;
					case 2:
						if (!xmlTextReader.EOF)
						{
							goto IL_BA;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FD;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 3:
						goto IL_BA;
					case 4:
						array2 = new byte[array.Length * 2];
						num2 = 2;
						continue;
					}
					break;
					IL_BA:
					num = xmlTextReader.ReadBase64(array2, 0, array2.Length);
					byte[] array3 = new byte[array.Length + num];
					array.CopyTo(array3, 0);
					Array.Copy(array2, 0, array3, array.Length, num);
					array = array3;
					num2 = 1;
					continue;
					IL_FD:
					num2 = 4;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x0600381D RID: 14365 RVA: 0x003498C8 File Offset: 0x003488C8
	private Image ᜀ(XmlNode A_0, bool A_1)
	{
		Image result;
		for (;;)
		{
			byte[] array = this.ᜀ(A_0);
			result = null;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					return result;
				case 2:
				{
					MemoryStream stream;
					result = new Metafile(stream);
					num = 0;
					continue;
				}
				case 3:
				{
					if (true)
					{
					}
					MemoryStream stream = new MemoryStream(array);
					num = 5;
					continue;
				}
				case 4:
					if (array.Length > 0)
					{
						num = 3;
						continue;
					}
					return result;
				case 5:
					if (!A_1)
					{
						MemoryStream stream;
						result = new Bitmap(stream);
						num = 1;
						continue;
					}
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
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x04002A29 RID: 10793
	private const string ᜀ = "Spire.Doc.Resources.word2ml.xslt";

	// Token: 0x04002A2A RID: 10794
	private const string ᜁ = "sections";

	// Token: 0x04002A2B RID: 10795
	private const string ᜂ = "{0}/{1}";

	// Token: 0x04002A2C RID: 10796
	private const string ᜃ = "section";

	// Token: 0x04002A2D RID: 10797
	private const string ᜄ = "body";

	// Token: 0x04002A2E RID: 10798
	private const string ᜅ = "paragraphs";

	// Token: 0x04002A2F RID: 10799
	private const string ᜆ = "paragraph";

	// Token: 0x04002A30 RID: 10800
	private const string ᜇ = "builtin-properties";

	// Token: 0x04002A31 RID: 10801
	private const string ᜈ = "page-setup";

	// Token: 0x04002A32 RID: 10802
	private const string ᜉ = "columns";

	// Token: 0x04002A33 RID: 10803
	private const string ᜊ = "headers-footers";

	// Token: 0x04002A34 RID: 10804
	private const string ᜋ = "items";

	// Token: 0x04002A35 RID: 10805
	private const string ᜌ = "item";

	// Token: 0x04002A36 RID: 10806
	private const string \u170D = "wordml://{0}_{1}.png";

	// Token: 0x04002A37 RID: 10807
	private const string ᜎ = "BreakCode";

	// Token: 0x04002A38 RID: 10808
	private const string ᜏ = "NoBreak";

	// Token: 0x04002A39 RID: 10809
	private const string ᜐ = "NewPage";

	// Token: 0x04002A3A RID: 10810
	private const string ᜑ = "True";

	// Token: 0x04002A3B RID: 10811
	private const string \u1712 = "type";

	// Token: 0x04002A3C RID: 10812
	private const string \u1713 = "Table";

	// Token: 0x04002A3D RID: 10813
	private const string \u1714 = "Picture";

	// Token: 0x04002A3E RID: 10814
	private const string \u1715 = "BookmarkStart";

	// Token: 0x04002A3F RID: 10815
	private const string \u1716 = "BookmarkEnd";

	// Token: 0x04002A40 RID: 10816
	private bool \u1717;

	// Token: 0x04002A41 RID: 10817
	private int[] \u1718;

	// Token: 0x04002A42 RID: 10818
	private bool \u1719;

	// Token: 0x04002A43 RID: 10819
	private int \u171A;

	// Token: 0x04002A44 RID: 10820
	private spr\u22DE.ᜄ \u171B = new spr\u22DE.ᜄ();

	// Token: 0x04002A45 RID: 10821
	private XmlDocument \u171C = new XmlDocument();

	// Token: 0x04002A46 RID: 10822
	private Document \u171D;

	// Token: 0x020003E9 RID: 1001
	internal class ᜃ
	{
		// Token: 0x0600381E RID: 14366 RVA: 0x00349988 File Offset: 0x00348988
		public string ᜀ()
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

		// Token: 0x0600381F RID: 14367 RVA: 0x003499CC File Offset: 0x003489CC
		public void ᜁ(string A_0)
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
			this.ᜀ = A_0;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x00349A10 File Offset: 0x00348A10
		public string ᜁ()
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

		// Token: 0x06003821 RID: 14369 RVA: 0x00349A54 File Offset: 0x00348A54
		public void ᜀ(string A_0)
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
			this.ᜁ = A_0;
		}

		// Token: 0x04002A47 RID: 10823
		private string ᜀ;

		// Token: 0x04002A48 RID: 10824
		private string ᜁ;
	}

	// Token: 0x020003EA RID: 1002
	[DefaultMember("Item")]
	internal class ᜄ : List<spr\u22DE.ᜃ>
	{
		// Token: 0x06003823 RID: 14371 RVA: 0x00349AAC File Offset: 0x00348AAC
		public string ᜀ(string A_0)
		{
			string result;
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
				result = string.Empty;
				using (List<spr\u22DE.ᜃ>.Enumerator enumerator = base.GetEnumerator())
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_C7;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							spr\u22DE.ᜃ ᜃ = enumerator.Current;
							num = 5;
							continue;
						}
						case 2:
							goto IL_BF;
						case 3:
							goto IL_BF;
						case 5:
						{
							spr\u22DE.ᜃ ᜃ;
							if (ᜃ.ᜀ() == A_0)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 6:
						{
							spr\u22DE.ᜃ ᜃ;
							result = ᜃ.ᜁ();
							num = 2;
							continue;
						}
						}
						IL_93:
						num = 1;
						continue;
						goto IL_93;
						IL_BF:
						num = 0;
					}
					IL_C7:;
				}
				break;
			}
			return result;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x00349BA4 File Offset: 0x00348BA4
		public int ᜀ(spr\u22DE.ᜃ A_0)
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
			A_0.ᜀ(this.ᜀ.ToString());
			this.ᜀ++;
			base.Add(A_0);
			return this.ᜀ - 1;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x00349C10 File Offset: 0x00348C10
		public int ᜁ(string A_0)
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
			spr\u22DE.ᜃ ᜃ = new spr\u22DE.ᜃ();
			ᜃ.ᜁ(A_0);
			ᜃ.ᜀ(this.ᜀ.ToString());
			this.ᜀ++;
			base.Add(ᜃ);
			return this.ᜀ - 1;
		}

		// Token: 0x04002A49 RID: 10825
		private int ᜀ;
	}

	// Token: 0x020003EB RID: 1003
	internal class ᜂ
	{
		// Token: 0x06003827 RID: 14375 RVA: 0x00349C9C File Offset: 0x00348C9C
		public double ᜀ()
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

		// Token: 0x06003828 RID: 14376 RVA: 0x00349CE0 File Offset: 0x00348CE0
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
			this.ᜀ = A_0;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x00349D24 File Offset: 0x00348D24
		public ᜂ()
		{
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x00349D38 File Offset: 0x00348D38
		public ᜂ(double A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x04002A4A RID: 10826
		private double ᜀ;
	}

	// Token: 0x020003EC RID: 1004
	internal class ᜁ : List<spr\u22DE.ᜂ>
	{
		// Token: 0x0600382B RID: 14379 RVA: 0x00349D54 File Offset: 0x00348D54
		public int ᜀ(spr\u22DE.ᜂ A_0)
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
			base.Add(A_0);
			return base.Count - 1;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00349DA0 File Offset: 0x00348DA0
		public int ᜁ(double A_0)
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
			return this.ᜀ(new spr\u22DE.ᜂ(A_0));
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x00349DE8 File Offset: 0x00348DE8
		public double ᜀ(double A_0)
		{
			switch (0)
			{
			default:
			{
				double num = 0.0;
				List<spr\u22DE.ᜂ>.Enumerator enumerator = base.GetEnumerator();
				double result;
				try
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							goto IL_E8;
						case 2:
							goto IL_DA;
						case 3:
							if (num > A_0)
							{
								num2 = 4;
								continue;
							}
							break;
						case 4:
							result = num - A_0;
							num2 = 2;
							continue;
						case 5:
							num2 = 1;
							continue;
						case 6:
							if (enumerator.MoveNext())
							{
								spr\u22DE.ᜂ ᜂ = enumerator.Current;
								num += ᜂ.ᜀ();
								num2 = 3;
								continue;
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
								num2 = 5;
								continue;
							}
							break;
						}
						IL_65:
						num2 = 6;
						continue;
						IL_63:
						goto IL_65;
						goto IL_63;
					}
					IL_DA:
					return result;
					IL_E8:
					goto IL_2B;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return result;
				IL_2B:
				return 0.0;
			}
			}
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x00349F0C File Offset: 0x00348F0C
		public int ᜀ(double A_0, double A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				double num2 = 0.0;
				List<spr\u22DE.ᜂ>.Enumerator enumerator = base.GetEnumerator();
				int result;
				try
				{
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							num++;
							num3 = 2;
							continue;
						case 1:
							goto IL_111;
						case 3:
							result = num;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D9;
							default:
								if (false)
								{
								}
								num3 = 1;
								continue;
							}
							break;
						case 4:
							if (A_1 == num2)
							{
								num3 = 3;
								continue;
							}
							num3 = 8;
							continue;
						case 6:
						{
							if (!enumerator.MoveNext())
							{
								num3 = 9;
								continue;
							}
							spr\u22DE.ᜂ ᜂ = enumerator.Current;
							num2 += ᜂ.ᜀ();
							num3 = 4;
							continue;
						}
						case 7:
							goto IL_11F;
						case 8:
							if (num2 > A_0)
							{
								goto IL_D9;
							}
							break;
						case 9:
							num3 = 7;
							continue;
						}
						IL_6E:
						num3 = 6;
						continue;
						goto IL_6E;
						IL_D9:
						num3 = 0;
					}
					IL_111:
					return result;
					IL_11F:
					goto IL_2E;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return result;
				IL_2E:
				return num - 1;
			}
			}
		}
	}

	// Token: 0x020003ED RID: 1005
	internal class ᜀ : List<spr\u22DE.ᜁ>
	{
		// Token: 0x06003830 RID: 14384 RVA: 0x0034A078 File Offset: 0x00349078
		public int ᜀ(spr\u22DE.ᜁ A_0)
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
			int count = A_0.Count;
			base.Add(A_0);
			return count;
		}
	}

	// Token: 0x020003EE RID: 1006
	internal class ᜅ
	{
		// Token: 0x06003832 RID: 14386 RVA: 0x0034A0D8 File Offset: 0x003490D8
		public ᜅ()
		{
			int a_ = 17;
			this.ᜀ = new spr\u22DE.ᜁ();
			this.ᜁ = new spr\u22DE.ᜀ();
			this.ᜃ = new NumberFormatInfo();
			base..ctor();
			this.ᜃ.CurrencyDecimalSeparator = ClipboardData.b("奶", a_);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0034A130 File Offset: 0x00349130
		public void ᜀ(XmlNode A_0)
		{
			int a_ = 10;
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					this.ᜂ = A_0;
					XmlNodeList xmlNodeList = this.ᜂ.SelectNodes(string.Format(ClipboardData.b("୯䉱ॳ奵ͷ䭹Ż", a_), ClipboardData.b("ɯᵱͳյ", a_), ClipboardData.b("ɯᵱͳ", a_)));
					int num = 0;
					for (;;)
					{
						IEnumerator enumerator2;
						switch (num)
						{
						case 0:
							if (xmlNodeList.Count > 0)
							{
								num = 1;
								continue;
							}
							return;
						case 1:
							goto IL_34C;
						case 2:
							try
							{
								num = 1;
								for (;;)
								{
									XmlNodeList xmlNodeList2;
									IEnumerator enumerator;
									spr\u22DE.ᜁ ᜁ;
									switch (num)
									{
									case 2:
										goto IL_301;
									case 3:
										goto IL_2BA;
									case 4:
										if (xmlNodeList2.Count > 0)
										{
											num = 3;
											continue;
										}
										break;
									case 5:
										try
										{
											num = 3;
											for (;;)
											{
												switch (num)
												{
												case 0:
													num = 4;
													continue;
												case 1:
												{
													if (!enumerator.MoveNext())
													{
														num = 0;
														continue;
													}
													XmlNode xmlNode = (XmlNode)enumerator.Current;
													num = 5;
													continue;
												}
												case 4:
													goto IL_26C;
												case 5:
												{
													XmlNode xmlNode;
													if (xmlNode.Attributes[ClipboardData.b("❯᭱ၳɵၷ", a_)] != null)
													{
														num = 6;
														continue;
													}
													break;
												}
												case 6:
												{
													XmlNode xmlNode;
													ᜁ.ᜁ(Convert.ToDouble(xmlNode.Attributes[ClipboardData.b("❯᭱ၳɵၷ", a_)].InnerText, this.ᜃ));
													num = 2;
													continue;
												}
												}
												IL_1BA:
												num = 1;
												continue;
												goto IL_1BA;
											}
											IL_26C:
											goto IL_DD;
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
														goto IL_2B7;
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
														goto IL_2B9;
													}
													break;
												}
											}
											IL_2B7:
											IL_2B9:;
										}
										goto IL_2BA;
										IL_DD:
										this.ᜁ.ᜀ(ᜁ);
										goto IL_EA;
									case 6:
										num = 2;
										continue;
									case 7:
									{
										if (!enumerator2.MoveNext())
										{
											num = 6;
											continue;
										}
										XmlNode xmlNode2 = (XmlNode)enumerator2.Current;
										xmlNodeList2 = xmlNode2.SelectNodes(string.Format(ClipboardData.b("୯䉱ॳ奵ͷ䭹Ż", a_), ClipboardData.b("፯᝱ᡳ᩵୷", a_), ClipboardData.b("፯᝱ᡳ᩵", a_)));
										num = 4;
										continue;
									}
									}
									goto IL_D8;
									IL_EA:
									num = 0;
									continue;
									IL_161:
									num = 7;
									continue;
									IL_D8:
									goto IL_161;
									IL_2BA:
									ᜁ = new spr\u22DE.ᜁ();
									enumerator = xmlNodeList2.GetEnumerator();
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_EA;
									default:
										if (false)
										{
										}
										num = 5;
										break;
									}
								}
								IL_301:
								return;
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
											goto IL_34B;
										case 1:
											goto IL_349;
										case 2:
											disposable2.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_349:
								IL_34B:;
							}
							goto IL_34C;
						}
						break;
						IL_34C:
						enumerator2 = xmlNodeList.GetEnumerator();
						num = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x0034A4D8 File Offset: 0x003494D8
		public void ᜁ()
		{
			int a_ = 13;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ();
					XmlElement xmlElement = this.ᜂ.OwnerDocument.CreateElement(ClipboardData.b("ݲ᝴᭶㹸ॺᑼ᭾", a_));
					List<spr\u22DE.ᜂ>.Enumerator enumerator = this.ᜀ.GetEnumerator();
					int num = 0;
					for (;;)
					{
						IEnumerator enumerator3;
						XmlNodeList xmlNodeList2;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_3DA;
										default:
											if (false)
											{
											}
											break;
										}
										break;
									case 1:
										num = 4;
										continue;
									case 2:
										if (!enumerator.MoveNext())
										{
											num = 1;
											continue;
										}
										goto IL_3DA;
									case 4:
										goto IL_492;
									}
									goto IL_3D5;
									IL_3DA:
									spr\u22DE.ᜂ ᜂ = enumerator.Current;
									XmlElement xmlElement2 = this.ᜂ.OwnerDocument.CreateElement(ClipboardData.b("ᑲݴṶᵸ㡺ቼ፾", a_));
									xmlElement2.SetAttribute(ClipboardData.b("Ѳ", a_), (ᜂ.ᜀ() * 20.0).ToString());
									xmlElement.AppendChild(xmlElement2);
									num = 0;
									continue;
									IL_460:
									num = 2;
									continue;
									IL_3D5:
									goto IL_460;
								}
								IL_492:
								goto IL_33E;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_4A5;
						case 1:
							try
							{
								num = 2;
								for (;;)
								{
									XmlNodeList xmlNodeList;
									double num3;
									switch (num)
									{
									case 0:
										goto IL_2F0;
									case 1:
									{
										IEnumerator enumerator2 = xmlNodeList.GetEnumerator();
										num = 3;
										continue;
									}
									case 3:
										try
										{
											num = 1;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_1F9;
												case 2:
													num = 0;
													continue;
												case 3:
												{
													IEnumerator enumerator2;
													if (!enumerator2.MoveNext())
													{
														num = 2;
														continue;
													}
													XmlNode xmlNode = (XmlNode)enumerator2.Current;
													double num2 = Convert.ToDouble(xmlNode.Attributes[ClipboardData.b("⑲ᱴ፶൸፺", a_)].InnerText, this.ᜃ);
													double a_2 = num3;
													num3 += num2;
													int num4 = this.ᜀ.ᜀ(a_2, num3);
													num = 5;
													continue;
												}
												case 5:
												{
													int num4;
													if (num4 > 1)
													{
														num = 6;
														continue;
													}
													break;
												}
												case 6:
												{
													XmlNode xmlNode;
													XmlAttribute xmlAttribute = xmlNode.OwnerDocument.CreateAttribute(ClipboardData.b("ၲᩴ᭶ᕸ᡺ቼ੾", a_));
													int num4;
													xmlAttribute.InnerText = num4.ToString();
													xmlNode.Attributes.Append(xmlAttribute);
													num = 4;
													continue;
												}
												}
												IL_1C7:
												num = 3;
												continue;
												goto IL_1C7;
											}
											IL_1F9:
											break;
										}
										finally
										{
											for (;;)
											{
												IEnumerator enumerator2;
												IDisposable disposable = enumerator2 as IDisposable;
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
														goto IL_246;
													case 1:
														goto IL_244;
													case 2:
														disposable.Dispose();
														num = 1;
														continue;
													}
													break;
												}
											}
											IL_244:
											IL_246:;
										}
										goto IL_247;
									case 4:
										if (!enumerator3.MoveNext())
										{
											num = 5;
											continue;
										}
										goto IL_247;
									case 5:
										num = 0;
										continue;
									case 6:
										if (xmlNodeList.Count > 0)
										{
											num = 1;
											continue;
										}
										break;
									}
									IL_B5:
									num = 4;
									continue;
									goto IL_B5;
									IL_247:
									XmlNode xmlNode2 = (XmlNode)enumerator3.Current;
									xmlNodeList = xmlNode2.SelectNodes(string.Format(ClipboardData.b("ࡲ䕴੶噸z䱼ɾ", a_), ClipboardData.b("ၲၴ᭶ᕸࡺ", a_), ClipboardData.b("ၲၴ᭶ᕸ", a_)));
									num3 = 0.0;
									num = 6;
								}
								IL_2F0:
								return;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator3 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_33B;
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
											goto IL_33D;
										}
										break;
									}
								}
								IL_33B:
								IL_33D:;
							}
							goto IL_33E;
						case 2:
							goto IL_4A5;
						case 3:
							if (xmlNodeList2.Count > 0)
							{
								num = 2;
								continue;
							}
							return;
						}
						break;
						IL_33E:
						this.ᜂ.AppendChild(xmlElement);
						xmlNodeList2 = this.ᜂ.SelectNodes(string.Format(ClipboardData.b("ࡲ䕴੶噸z䱼ɾ", a_), ClipboardData.b("Ųᩴv੸", a_), ClipboardData.b("Ųᩴv", a_)));
						num = 3;
						continue;
						IL_4A5:
						enumerator3 = xmlNodeList2.GetEnumerator();
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x0034A9F0 File Offset: 0x003499F0
		private void ᜀ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					double num = 0.0;
					double num2 = 0.0;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (this.ᜁ.Count > 0)
							{
								num3 = 3;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							goto IL_C7;
						case 3:
						{
							bool flag = true;
							num3 = 2;
							continue;
						}
						case 4:
							return;
						case 5:
							if (num == 0.0)
							{
								num3 = 4;
								continue;
							}
							if (true)
							{
							}
							num2 += num;
							this.ᜀ.ᜁ(num);
							num3 = 7;
							continue;
						case 6:
						{
							bool flag;
							if (!flag)
							{
								num3 = 1;
								continue;
							}
							num = double.MaxValue;
							List<spr\u22DE.ᜁ>.Enumerator enumerator = this.ᜁ.GetEnumerator();
							num3 = 8;
							continue;
						}
						case 7:
							goto IL_C7;
						case 8:
							try
							{
								num3 = 8;
								for (;;)
								{
									double num5;
									switch (num3)
									{
									case 0:
										num3 = 3;
										continue;
									case 1:
									{
										double num4;
										if (num <= num4)
										{
											num3 = 7;
											continue;
										}
										num3 = 6;
										continue;
									}
									case 2:
									{
										List<spr\u22DE.ᜁ>.Enumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num3 = 0;
											continue;
										}
										spr\u22DE.ᜁ ᜁ = enumerator.Current;
										double num4 = ᜁ.ᜀ(num2);
										num3 = 1;
										continue;
									}
									case 3:
										goto IL_1F0;
									case 4:
										num5 = num;
										goto IL_19B;
									case 6:
									{
										double num4;
										num5 = num4;
										goto IL_19B;
									}
									case 7:
										num3 = 4;
										continue;
									case 8:
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
									}
									IL_17E:
									num3 = 2;
									continue;
									IL_19B:
									num = num5;
									num3 = 5;
									continue;
									goto IL_17E;
								}
								IL_1F0:
								goto IL_75;
							}
							finally
							{
								List<spr\u22DE.ᜁ>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							return;
							IL_75:
							num3 = 5;
							continue;
						}
						break;
						IL_C7:
						num3 = 6;
					}
				}
				return;
			}
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x0034AC10 File Offset: 0x00349C10
		private void ᜀ(spr\u22DE.ᜁ A_0)
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
			this.ᜁ.ᜀ(A_0);
		}

		// Token: 0x04002A4B RID: 10827
		private spr\u22DE.ᜁ ᜀ;

		// Token: 0x04002A4C RID: 10828
		private spr\u22DE.ᜀ ᜁ;

		// Token: 0x04002A4D RID: 10829
		private XmlNode ᜂ;

		// Token: 0x04002A4E RID: 10830
		private NumberFormatInfo ᜃ;
	}
}
