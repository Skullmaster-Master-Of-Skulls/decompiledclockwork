using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Interface;

// Token: 0x0200042D RID: 1069
internal class sprᤍ
{
	// Token: 0x06003B6E RID: 15214 RVA: 0x003719B8 File Offset: 0x003709B8
	public IDocument ᜀ(string A_0)
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
		this.ᜃ.Load(A_0);
		XslTransform xslTransform = new XslTransform();
		xslTransform.Load(sprᤍ.ᜀ(), null, null);
		MemoryStream memoryStream = new MemoryStream();
		this.ᜂ();
		xslTransform.Transform(this.ᜃ, null, memoryStream, null);
		memoryStream.Position = 0L;
		IDocument document = new Document();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(memoryStream);
		memoryStream = new MemoryStream((int)memoryStream.Length);
		xmlDocument.Save(memoryStream);
		memoryStream.Position = 0L;
		document.LoadFromStream(memoryStream, FileFormat.Xml);
		return document;
	}

	// Token: 0x06003B6F RID: 15215 RVA: 0x00371A70 File Offset: 0x00370A70
	public void ᜀ(Stream A_0, Document A_1)
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
		this.ᜃ.Load(A_0);
		XslTransform xslTransform = new XslTransform();
		xslTransform.Load(sprᤍ.ᜀ(), null, null);
		MemoryStream memoryStream = new MemoryStream();
		this.ᜂ();
		xslTransform.Transform(this.ᜃ, null, memoryStream, null);
		memoryStream.Position = 0L;
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(memoryStream);
		memoryStream = new MemoryStream((int)memoryStream.Length);
		xmlDocument.Save(memoryStream);
		memoryStream.Position = 0L;
		A_1.LoadFromStream(memoryStream, FileFormat.Xml);
	}

	// Token: 0x06003B70 RID: 15216 RVA: 0x00371B20 File Offset: 0x00370B20
	private void ᜂ()
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			this.ᜁ();
			XmlNode xmlNode = this.ᜃ.DocumentElement.SelectSingleNode(ClipboardData.b("ᱪ坬൮Ṱᝲ౴", a_), this.ᜅ);
			XmlNodeList xmlNodeList = xmlNode.SelectNodes(ClipboardData.b("ᱪᕬ啮ɰᙲᙴͶ", a_), this.ᜅ);
			IEnumerator enumerator = xmlNodeList.GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					XmlNodeList xmlNodeList2;
					IEnumerator enumerator2;
					switch (num)
					{
					case 0:
						num = 8;
						continue;
					case 1:
						goto IL_21A;
					case 3:
					{
						XmlNode xmlNode2;
						xmlNodeList2 = xmlNode2.SelectNodes(ClipboardData.b("ᱪ坬Ὦ", a_), this.ᜅ);
						num = 1;
						continue;
					}
					case 4:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 1:
								{
									if (!enumerator2.MoveNext())
									{
										num = 2;
										continue;
									}
									XmlNode a_2 = (XmlNode)enumerator2.Current;
									this.ᜃ(a_2);
									num = 0;
									continue;
								}
								case 2:
									num = 4;
									continue;
								case 4:
									goto IL_1A9;
								}
								IL_162:
								num = 1;
								continue;
								goto IL_162;
							}
							IL_1A9:;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator2 as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1F1;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_1F3;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_1F1:
							IL_1F3:;
						}
						break;
					case 5:
					{
						XmlNode xmlNode2;
						if (xmlNode2 != null)
						{
							num = 3;
							continue;
						}
						goto IL_21A;
					}
					case 6:
					{
						XmlNode xmlNode3;
						XmlNode xmlNode2 = xmlNode3.SelectSingleNode(ClipboardData.b("ᱪ坬൮Ṱᝲ౴", a_), this.ᜅ);
						num = 5;
						continue;
					}
					case 7:
						if (xmlNodeList2.Count == 0)
						{
							num = 6;
							continue;
						}
						goto IL_21A;
					case 8:
						goto IL_269;
					case 9:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						XmlNode xmlNode3 = (XmlNode)enumerator.Current;
						xmlNodeList2 = xmlNode3.SelectNodes(ClipboardData.b("ᱪ坬Ὦ", a_), this.ᜅ);
						num = 7;
						continue;
					}
					}
					IL_1F4:
					num = 9;
					continue;
					goto IL_1F4;
					IL_21A:
					enumerator2 = xmlNodeList2.GetEnumerator();
					num = 4;
				}
				IL_269:;
			}
			finally
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_2C4:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_29C;
				}
				IDisposable disposable2;
				for (;;)
				{
					IL_289:
					switch (num)
					{
					case 0:
						goto IL_2CD;
					case 1:
						if (disposable2 != null)
						{
							num = 2;
							continue;
						}
						goto IL_2CF;
					case 2:
						goto IL_2BB;
					}
					goto IL_29C;
				}
				IL_2BB:
				disposable2.Dispose();
				goto IL_2C4;
				IL_2CD:
				IL_2CF:
				goto EndFinally_12;
				IL_29C:
				disposable2 = (enumerator as IDisposable);
				num = 1;
				goto IL_289;
				EndFinally_12:;
			}
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06003B71 RID: 15217 RVA: 0x00371E3C File Offset: 0x00370E3C
	private void ᜃ(XmlNode A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			XmlNodeList xmlNodeList = A_0.SelectNodes(ClipboardData.b("ᑴ᩶ᕸ䅺ᱼᅾﶈ", a_), this.ᜅ);
			XmlNodeList xmlNodeList2 = A_0.SelectNodes(ClipboardData.b("ɴ䵶ॸቺṼ୾", a_), this.ᜅ);
			IEnumerator enumerator = xmlNodeList.GetEnumerator();
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						XmlNode a_2 = (XmlNode)enumerator.Current;
						this.ᜂ(a_2);
						num = 4;
						continue;
					}
					case 3:
						goto IL_199;
					}
					IL_154:
					num = 2;
					continue;
					goto IL_154;
				}
				IL_199:
				goto IL_11F;
			}
			finally
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1FC:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					goto IL_1CC;
				}
				IDisposable disposable;
				for (;;)
				{
					IL_1B9:
					switch (num)
					{
					case 0:
						goto IL_1EB;
					case 1:
						if (disposable != null)
						{
							num = 0;
							continue;
						}
						goto IL_207;
					case 2:
						goto IL_205;
					}
					goto IL_1CC;
				}
				IL_1EB:
				if (true)
				{
				}
				disposable.Dispose();
				goto IL_1FC;
				IL_205:
				IL_207:
				goto EndFinally_10;
				IL_1CC:
				disposable = (enumerator as IDisposable);
				num = 1;
				goto IL_1B9;
				EndFinally_10:;
			}
			return;
			for (;;)
			{
				IL_11F:
				IEnumerator enumerator2 = xmlNodeList2.GetEnumerator();
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 2:
							goto IL_D1;
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
							XmlNode a_3 = (XmlNode)enumerator2.Current;
							this.ᜁ(a_3);
							num = 1;
							continue;
						}
						}
						IL_8C:
						num = 4;
						continue;
						goto IL_8C;
					}
					IL_D1:
					break;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator2 as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_11C;
							case 1:
								if (disposable2 != null)
								{
									num = 2;
									continue;
								}
								goto IL_11E;
							case 2:
								disposable2.Dispose();
								num = 0;
								continue;
							}
							break;
						}
					}
					IL_11C:
					IL_11E:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06003B72 RID: 15218 RVA: 0x00372070 File Offset: 0x00371070
	private void ᜂ(XmlNode A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			string a_2;
			for (;;)
			{
				string innerText = A_0.Attributes[ClipboardData.b("ѯୱѳ፵", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ݯ", a_))].InnerText;
				a_2 = string.Empty;
				int num = 6;
				for (;;)
				{
					string innerText2;
					switch (num)
					{
					case 0:
					{
						string a;
						if ((a = innerText) != null)
						{
							goto IL_16A;
						}
						return;
					}
					case 1:
						goto IL_118;
					case 2:
					{
						if (true)
						{
						}
						string a;
						if (!(a == ClipboardData.b("❯ᵱٳት噷㡹፻ᅽꒉ즋", a_)))
						{
							num = 7;
							continue;
						}
						XmlAttribute xmlAttribute = this.ᜃ.CreateAttribute(ClipboardData.b("ݯ䡱ᩳ᝵ᕷό", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ݯ", a_)));
						xmlAttribute.InnerText = this.ᜄ.ᜀ(innerText2);
						A_0.Attributes.Append(xmlAttribute);
						num = 8;
						continue;
					}
					case 3:
					{
						string a;
						if (!(a == ClipboardData.b("❯ᵱٳት噷㡹፻ᅽꒉ\udf8b揄", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_24D;
					}
					case 4:
						num = 3;
						continue;
					case 5:
						a_2 = A_0.Attributes[ClipboardData.b("ṯ፱ᥳ፵", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ݯ", a_))].InnerText;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16A;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 6:
						if (A_0.Attributes[ClipboardData.b("ṯ፱ᥳ፵", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ݯ", a_))] != null)
						{
							num = 5;
							continue;
						}
						goto IL_118;
					case 7:
						return;
					case 8:
						goto IL_1DE;
					case 9:
						num = 2;
						continue;
					}
					break;
					IL_118:
					innerText2 = A_0.Attributes[ClipboardData.b("᥯ᙱ", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ᅯάᡳ", a_))].InnerText;
					num = 0;
					continue;
					IL_16A:
					num = 4;
				}
			}
			return;
			IL_1DE:
			return;
			IL_24D:
			this.ᜄ.ᜁ(a_2);
			return;
		}
		}
	}

	// Token: 0x06003B73 RID: 15219 RVA: 0x00372318 File Offset: 0x00371318
	private void ᜁ(XmlNode A_0)
	{
		int a_ = 6;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
				for (;;)
				{
					XmlNode xmlNode = A_0.SelectSingleNode(ClipboardData.b("᭫呭ቯ᭱ᩳ㉵᥷๹ᵻ", a_), this.ᜅ);
					XmlNode xmlNode2 = A_0.SelectSingleNode(ClipboardData.b("ᩫ呭ͯᩱᕳٵᵷ", a_), this.ᜅ);
					Image image = null;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							XmlAttribute xmlAttribute = this.ᜃ.CreateAttribute(ClipboardData.b("իͭᝯ╱ᵳት౷ቹ", a_));
							double num2;
							xmlAttribute.InnerText = num2.ToString();
							xmlNode2.Attributes.Append(xmlAttribute);
							XmlAttribute xmlAttribute2 = this.ᜃ.CreateAttribute(ClipboardData.b("իͭᝯ㩱ᅳήίቹࡻ", a_));
							double num3;
							xmlAttribute2.InnerText = num3.ToString();
							xmlNode2.Attributes.Append(xmlAttribute2);
							num = 5;
							continue;
						}
						case 1:
							if (xmlNode != null)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_11E;
						case 2:
							image = this.ᜀ(xmlNode, false);
							num = 3;
							continue;
						case 3:
							goto IL_11E;
						case 4:
							if (xmlNode2 != null)
							{
								num = 0;
								continue;
							}
							return;
						case 5:
							return;
						}
						break;
						IL_11E:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							double num2 = (double)image.Width;
							double num3 = (double)image.Height;
							num = 4;
							break;
						}
						}
					}
				}
				break;
			}
		}
	}

	// Token: 0x06003B74 RID: 15220 RVA: 0x003724A8 File Offset: 0x003714A8
	private void ᜁ()
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
		this.ᜅ = new XmlNamespaceManager(this.ᜃ.NameTable);
		this.ᜅ.AddNamespace(ClipboardData.b("ᡮ", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ᡮ", a_)));
		this.ᜅ.AddNamespace(ClipboardData.b("ᡮ॰", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("ᡮ॰", a_)));
		this.ᜅ.AddNamespace(ClipboardData.b("๮ᱰὲ", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("๮ᱰὲ", a_)));
		this.ᜅ.AddNamespace(ClipboardData.b("᥮", a_), this.ᜃ.DocumentElement.GetNamespaceOfPrefix(ClipboardData.b("᥮", a_)));
	}

	// Token: 0x06003B75 RID: 15221 RVA: 0x003725E0 File Offset: 0x003715E0
	private static XmlReader ᜀ()
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
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(ClipboardData.b("㭧ᩩիᱭᕯ山び᥵᭷呹⹻᭽ﾋꂍﶏﺑꚓ낝\ud89f톡좣튥", a_));
		return new XmlTextReader(manifestResourceStream);
	}

	// Token: 0x06003B76 RID: 15222 RVA: 0x00372648 File Offset: 0x00371648
	private byte[] ᜀ(XmlNode A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_F0:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_47;
			}
			break;
		}
		byte[] array2;
		byte[] array;
		XmlTextReader xmlTextReader;
		int num2;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				array = new byte[array2.Length * 2];
				num = 4;
				continue;
			case 1:
				goto IL_A3;
			case 2:
				goto IL_A5;
			case 3:
				goto IL_E2;
			case 4:
				if (xmlTextReader.EOF)
				{
					num = 1;
					continue;
				}
				goto IL_A5;
			}
			goto IL_47;
			IL_A5:
			num2 = xmlTextReader.ReadBase64(array, 0, array.Length);
			byte[] array3 = new byte[array2.Length + num2];
			array2.CopyTo(array3, 0);
			Array.Copy(array, 0, array3, array2.Length, num2);
			array2 = array3;
			num = 3;
		}
		IL_A3:
		return array2;
		IL_E2:
		if (true)
		{
		}
		if (num2 >= array.Length)
		{
			goto IL_F0;
		}
		return array2;
		IL_47:
		xmlTextReader = new XmlTextReader(new StringReader(A_0.OuterXml));
		xmlTextReader.Read();
		num2 = 0;
		array2 = new byte[0];
		array = new byte[1000];
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06003B77 RID: 15223 RVA: 0x00372764 File Offset: 0x00371764
	private Image ᜀ(XmlNode A_0, bool A_1)
	{
		Image result;
		for (;;)
		{
			IL_00:
			for (;;)
			{
				IL_44:
				byte[] array = this.ᜀ(A_0);
				result = null;
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
						{
							if (A_1)
							{
								num = 5;
								continue;
							}
							MemoryStream stream;
							result = new Bitmap(stream);
							num = 2;
							continue;
						}
						case 1:
							if (array.Length > 0)
							{
								num = 4;
								continue;
							}
							return result;
						case 2:
							return result;
						case 3:
							return result;
						case 4:
						{
							MemoryStream stream = new MemoryStream(array);
							num = 0;
							continue;
						}
						case 5:
						{
							MemoryStream stream;
							result = new Metafile(stream);
							num = 3;
							continue;
						}
						}
						goto IL_44;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x04002BA1 RID: 11169
	private const string ᜀ = "Spire.Doc.Resources.ml2word.xslt";

	// Token: 0x04002BA2 RID: 11170
	private const string ᜁ = "Word.Bookmark.Start";

	// Token: 0x04002BA3 RID: 11171
	private const string ᜂ = "Word.Bookmark.End";

	// Token: 0x04002BA4 RID: 11172
	private XmlDocument ᜃ = new XmlDocument();

	// Token: 0x04002BA5 RID: 11173
	private sprᤍ.ᜀ ᜄ = new sprᤍ.ᜀ();

	// Token: 0x04002BA6 RID: 11174
	private XmlNamespaceManager ᜅ;

	// Token: 0x0200042E RID: 1070
	internal class ᜁ
	{
		// Token: 0x06003B78 RID: 15224 RVA: 0x0037282C File Offset: 0x0037182C
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

		// Token: 0x06003B79 RID: 15225 RVA: 0x00372870 File Offset: 0x00371870
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

		// Token: 0x06003B7A RID: 15226 RVA: 0x003728B4 File Offset: 0x003718B4
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

		// Token: 0x06003B7B RID: 15227 RVA: 0x003728F8 File Offset: 0x003718F8
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

		// Token: 0x04002BA7 RID: 11175
		private string ᜀ;

		// Token: 0x04002BA8 RID: 11176
		private string ᜁ;
	}

	// Token: 0x0200042F RID: 1071
	[DefaultMember("Item")]
	internal class ᜀ : List<sprᤍ.ᜁ>
	{
		// Token: 0x06003B7D RID: 15229 RVA: 0x00372950 File Offset: 0x00371950
		public string ᜀ(string A_0)
		{
			string result = string.Empty;
			using (List<sprᤍ.ᜁ>.Enumerator enumerator = base.GetEnumerator())
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						sprᤍ.ᜁ ᜁ;
						result = ᜁ.ᜀ();
						num = 4;
						continue;
					}
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 5;
							continue;
						}
						sprᤍ.ᜁ ᜁ = enumerator.Current;
						num = 6;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_86;
						default:
							goto IL_BA;
						}
						break;
					case 4:
						goto IL_99;
					case 5:
						goto IL_86;
					case 6:
					{
						if (true)
						{
						}
						sprᤍ.ᜁ ᜁ;
						if (ᜁ.ᜁ() == A_0)
						{
							num = 0;
							continue;
						}
						break;
					}
					}
					IL_6D:
					num = 1;
					continue;
					goto IL_6D;
					IL_99:
					num = 2;
					continue;
					IL_86:
					goto IL_99;
				}
				IL_BA:
				if (false)
				{
				}
			}
			return result;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x00372A48 File Offset: 0x00371A48
		public int ᜀ(sprᤍ.ᜁ A_0)
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
			A_0.ᜀ(this.ᜀ.ToString());
			this.ᜀ++;
			base.Add(A_0);
			return this.ᜀ - 1;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x00372AB4 File Offset: 0x00371AB4
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
			sprᤍ.ᜁ ᜁ = new sprᤍ.ᜁ();
			ᜁ.ᜁ(A_0);
			ᜁ.ᜀ(this.ᜀ.ToString());
			this.ᜀ++;
			base.Add(ᜁ);
			return this.ᜀ - 1;
		}

		// Token: 0x04002BA9 RID: 11177
		private int ᜀ;
	}
}
