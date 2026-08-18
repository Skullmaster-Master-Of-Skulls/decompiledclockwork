using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using MailBee;

namespace a.k
{
	// Token: 0x02000135 RID: 309
	internal class c
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x0002D64F File Offset: 0x0002C64F
		public b[] c()
		{
			return (b[])this.b.ToArray(typeof(b));
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002D66B File Offset: 0x0002C66B
		public b[] d()
		{
			return (b[])this.c.ToArray(typeof(b));
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002D687 File Offset: 0x0002C687
		public b[] e()
		{
			return (b[])this.d.ToArray(typeof(b));
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002D6A3 File Offset: 0x0002C6A3
		public b[] f()
		{
			return (b[])this.e.ToArray(typeof(b));
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002D6BF File Offset: 0x0002C6BF
		public b[] a()
		{
			return (b[])this.f.ToArray(typeof(b));
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002D6DB File Offset: 0x0002C6DB
		public b[] b()
		{
			return (b[])this.g.ToArray(typeof(b));
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002D6F7 File Offset: 0x0002C6F7
		public StringDictionary i()
		{
			return this.k;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002D6FF File Offset: 0x0002C6FF
		public string[] g()
		{
			return (string[])this.h.ToArray(typeof(string));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002D71B File Offset: 0x0002C71B
		public string[] h()
		{
			return (string[])this.i.ToArray(typeof(string));
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002D737 File Offset: 0x0002C737
		public b[] j()
		{
			return (b[])this.j.ToArray(typeof(b));
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002D754 File Offset: 0x0002C754
		public c(string[] A_0, bool A_1)
		{
			bool flag = false;
			this.a = new Hashtable();
			this.b = new ArrayList();
			this.c = new ArrayList();
			this.d = new ArrayList();
			this.e = new ArrayList();
			this.f = new ArrayList();
			this.g = new ArrayList();
			this.h = new ArrayList();
			this.i = new ArrayList();
			this.j = new ArrayList(2);
			this.k = new StringDictionary();
			for (int i = 0; i < A_0.Length; i++)
			{
				if (!File.Exists(A_0[i]))
				{
					if (!A_1 || (!flag && i == A_0.Length - 1))
					{
						throw new MailBeeIOException(31);
					}
				}
				else
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
					xmlReaderSettings.IgnoreWhitespace = true;
					xmlReaderSettings.ProhibitDtd = false;
					xmlReaderSettings.XmlResolver = new XmlUrlResolver();
					XmlReader xmlReader = XmlReader.Create(A_0[i], xmlReaderSettings);
					XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(xmlReader);
					xmlValidatingReader.ValidationType = ValidationType.DTD;
					try
					{
						xmlDocument.Load(xmlValidatingReader);
					}
					catch (XmlException a_)
					{
						if (!A_1 || (!flag && i == A_0.Length - 1))
						{
							throw new MailBeeIOException(33, a_);
						}
					}
					finally
					{
						if (xmlReader != null)
						{
							xmlReader.Close();
						}
					}
					this.a(xmlDocument);
					flag = true;
				}
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002D8B8 File Offset: 0x0002C8B8
		public c(byte[] A_0)
		{
			this.a = new Hashtable();
			this.b = new ArrayList();
			this.c = new ArrayList();
			this.d = new ArrayList();
			this.e = new ArrayList();
			this.f = new ArrayList();
			this.g = new ArrayList();
			this.h = new ArrayList();
			this.i = new ArrayList();
			this.j = new ArrayList(2);
			this.k = new StringDictionary();
			XmlDocument xmlDocument = new XmlDocument();
			XmlReader xmlReader = XmlReader.Create("C:\\BounceDatabase\\all.xml", new XmlReaderSettings
			{
				IgnoreWhitespace = true,
				ProhibitDtd = false,
				XmlResolver = new a(A_0)
			});
			XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(xmlReader);
			xmlValidatingReader.ValidationType = ValidationType.DTD;
			try
			{
				xmlDocument.Load(xmlValidatingReader);
			}
			catch (XmlException a_)
			{
				throw new MailBeeIOException(33, a_);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			this.a(xmlDocument);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002D9C8 File Offset: 0x0002C9C8
		public string c(string A_0)
		{
			if (A_0 != null && this.a.ContainsKey(A_0))
			{
				return ((f)this.a[A_0]).b();
			}
			return string.Empty;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002D9F8 File Offset: 0x0002C9F8
		private d b(string A_0)
		{
			if (A_0 == "start")
			{
				return global::a.k.d.a;
			}
			if (A_0 == "end")
			{
				return global::a.k.d.b;
			}
			if (A_0 == "any")
			{
				return global::a.k.d.c;
			}
			if (!(A_0 == "all"))
			{
				return global::a.k.d.c;
			}
			return global::a.k.d.d;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002DA44 File Offset: 0x0002CA44
		private void a(XmlDocument A_0)
		{
			XmlNodeList elementsByTagName = A_0.GetElementsByTagName("type");
			for (int i = 0; i < elementsByTagName.Count; i++)
			{
				string value = elementsByTagName[i].Attributes["id"].Value;
				if (this.a.ContainsKey(value))
				{
					this.a.Remove(value);
				}
				string a_ = (elementsByTagName[i].ParentNode.Name != "type") ? null : elementsByTagName[i].ParentNode.Attributes["id"].Value;
				this.a.Add(value, new f(elementsByTagName[i].Attributes["name"].Value, a_));
			}
			elementsByTagName = A_0.GetElementsByTagName("fromaddress");
			for (int j = 0; j < elementsByTagName.Count; j++)
			{
				b b = default(b);
				b.a = elementsByTagName[j].Attributes["keyword"].Value;
				b.b = this.b(elementsByTagName[j].Attributes["match"].Value);
				string name = elementsByTagName[j].ParentNode.Name;
				if (!(name == "fromnames"))
				{
					if (!(name == "fromemails"))
					{
						if (!(name == "fromremarks"))
						{
							if (name == "fromstrings")
							{
								this.e.Add(b);
							}
						}
						else
						{
							this.d.Add(b);
						}
					}
					else
					{
						this.c.Add(b);
					}
				}
				else
				{
					this.b.Add(b);
				}
			}
			elementsByTagName = A_0.GetElementsByTagName("subjects");
			for (int k = 0; k < elementsByTagName.Count; k++)
			{
				XmlNodeList childNodes = elementsByTagName[k].ChildNodes;
				for (int l = 0; l < childNodes.Count; l++)
				{
					b b2 = default(b);
					if (childNodes[l].Name == "include" || childNodes[l].Name == "exclude")
					{
						XmlNodeList childNodes2 = childNodes[l].ChildNodes;
						for (int m = 0; m < childNodes2.Count; m++)
						{
							b2.a = childNodes2[m].Attributes["keyword"].Value;
							b2.b = this.b(childNodes2[m].Attributes["match"].Value);
							if (childNodes2[m].Attributes["type"] != null)
							{
								b2.c = childNodes2[m].Attributes["type"].Value;
							}
							string name = childNodes[l].Name;
							if (!(name == "include"))
							{
								if (name == "exclude")
								{
									this.g.Add(b2);
								}
							}
							else
							{
								this.f.Add(b2);
							}
						}
					}
				}
			}
			elementsByTagName = A_0.GetElementsByTagName("regexp");
			for (int n = 0; n < elementsByTagName.Count; n++)
			{
				string value2 = elementsByTagName[n].Attributes["name"].Value;
				if (this.k.ContainsKey(value2))
				{
					this.k.Remove(value2);
				}
				this.k.Add(value2, elementsByTagName[n].ParentNode["mask"].InnerText);
			}
			elementsByTagName = A_0.GetElementsByTagName("description");
			for (int num = 0; num < elementsByTagName.Count; num++)
			{
				b b3 = default(b);
				b3.a = Regex.Escape(elementsByTagName[num].Attributes["keyword"].Value).Replace("\\{0}", this.k["BLANK"]);
				b3.b = global::a.k.d.c;
				b3.c = elementsByTagName[num].Attributes["type"].Value;
				this.j.Add(b3);
			}
			for (int num2 = 1; num2 <= 10; num2++)
			{
				elementsByTagName = A_0.GetElementsByTagName("contents");
				for (int num3 = 0; num3 < elementsByTagName.Count; num3++)
				{
					if (int.Parse(elementsByTagName[num3].Attributes["priority"].Value) == num2)
					{
						string text = Regex.Replace(Regex.Replace(elementsByTagName[num3].ChildNodes[0].Value, "[\t\r\n]", " "), " +", " ").Trim();
						this.h.Add(text);
						text = Regex.Escape(text).ToLower();
						foreach (object obj in this.k)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							text = text.Replace("\\{" + ((string)dictionaryEntry.Key).ToLower() + "}", string.Concat(new object[]
							{
								"(?<",
								dictionaryEntry.Key,
								">",
								dictionaryEntry.Value,
								")"
							}));
						}
						this.i.Add("^" + text + "$");
					}
				}
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002E090 File Offset: 0x0002D090
		private Stream a(string A_0)
		{
			XmlValidatingReader xmlValidatingReader = null;
			XmlTextReader xmlTextReader = null;
			XmlDocument xmlDocument = new XmlDocument();
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				xmlTextReader = new XmlTextReader(A_0);
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
				xmlValidatingReader = new XmlValidatingReader(xmlTextReader);
				xmlValidatingReader.ValidationType = ValidationType.DTD;
				xmlDocument.Load(xmlValidatingReader);
				xmlDocument.Save(memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
				if (xmlValidatingReader != null)
				{
					xmlValidatingReader.Close();
				}
			}
			return memoryStream;
		}

		// Token: 0x040007B8 RID: 1976
		private Hashtable a;

		// Token: 0x040007B9 RID: 1977
		private ArrayList b;

		// Token: 0x040007BA RID: 1978
		private ArrayList c;

		// Token: 0x040007BB RID: 1979
		private ArrayList d;

		// Token: 0x040007BC RID: 1980
		private ArrayList e;

		// Token: 0x040007BD RID: 1981
		private ArrayList f;

		// Token: 0x040007BE RID: 1982
		private ArrayList g;

		// Token: 0x040007BF RID: 1983
		private ArrayList h;

		// Token: 0x040007C0 RID: 1984
		private ArrayList i;

		// Token: 0x040007C1 RID: 1985
		private ArrayList j;

		// Token: 0x040007C2 RID: 1986
		private StringDictionary k;

		// Token: 0x040007C3 RID: 1987
		private const string l = "type";
	}
}
