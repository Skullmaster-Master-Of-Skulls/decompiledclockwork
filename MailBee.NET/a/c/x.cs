using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace a.c
{
	// Token: 0x0200022D RID: 557
	internal class x
	{
		// Token: 0x060012A0 RID: 4768 RVA: 0x000530AC File Offset: 0x000520AC
		public x(XmlDocument A_0)
		{
			this.c = A_0;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000530C8 File Offset: 0x000520C8
		private string d(string A_0)
		{
			string[] array = new string[]
			{
				"(\\w)\\s+(\\w)",
				"(\\w)\\s*>\\s*(\\w)",
				"(\\w):first-child",
				"(\\w)\\s*\\+\\s*(\\w)",
				"(\\w)\\[([\\w\\-]+)]",
				"(\\w)\\[([\\w\\-]+)\\=\\\"(.*)\\\"]",
				"(\\w+|\\*)+\\.([\\w\\-]+)+",
				"\\.([\\w\\-]+)",
				"(\\w+)\\#([\\w\\-]+)",
				"\\#([\\w\\-]+)"
			};
			string[] array2 = new string[]
			{
				"$1//$2",
				"$1/$2",
				"*[1]/self::$1",
				"$1/following-sibling::*[1]/self::$2",
				"$1 [ @$2 ]",
				"$1[ contains( concat( \" \", @$2, \" \" ), concat( \" \", \"$3\", \" \" ) ) ]",
				"$1[ contains( concat( \" \", @class, \" \" ), concat( \" \", \"$2\", \" \" ) ) ]",
				"*[ contains( concat( \" \", @class, \" \" ), concat( \" \", \"$1\", \" \" ) ) ]",
				"$1[ @id = \"$2\" ]",
				"*[ @id = \"$1\" ]"
			};
			for (int i = 0; i < array.Length; i++)
			{
				A_0 = Regex.Replace(A_0, array[i], array2[i]);
			}
			return "//" + A_0;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000531B0 File Offset: 0x000521B0
		private int c(string A_0)
		{
			A_0 = A_0.Replace(">", " > ");
			A_0 = A_0.Replace("+", " + ");
			int num = 0;
			foreach (string text in A_0.Split(new char[]
			{
				' '
			}))
			{
				if (text.IndexOf('#') != -1)
				{
					num += 100;
				}
				else if (text.IndexOf('.') != -1)
				{
					num += 10;
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00053230 File Offset: 0x00052230
		private string b(string A_0)
		{
			A_0 = Regex.Replace(A_0, "(\\s)+?class=\"(.*?)\"(\\s)+?", " ");
			A_0 = Regex.Replace(A_0, "(\\s)+?id=\"(.*?)\"(\\s)+?", " ");
			return A_0;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00053258 File Offset: 0x00052258
		public XmlDocument b()
		{
			StringWriter stringWriter = new StringWriter();
			this.c.Save(stringWriter);
			string input = stringWriter.ToString();
			MatchCollection matchCollection = new Regex("<style(.*?)>(.*?)</style>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(input);
			if (matchCollection.Count == 0)
			{
				return this.c;
			}
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				Match match2 = new Regex("<!\\[CDATA\\[(.*?)//\\]\\]>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(match.Groups[2].Value);
				if (match2.Success)
				{
					this.a = this.a + match2.Groups[1].Value.Replace("&quot;", "\"") + "\n";
				}
			}
			this.a();
			if (this.b.Count > 0)
			{
				foreach (object obj2 in this.b)
				{
					Hashtable hashtable = (Hashtable)obj2;
					string xpath = this.d((string)hashtable["selector"]);
					XmlNodeList xmlNodeList = null;
					try
					{
						xmlNodeList = this.c.SelectNodes(xpath);
					}
					catch (XPathException)
					{
						continue;
					}
					foreach (object obj3 in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj3;
						StringDictionary stringDictionary = new StringDictionary();
						XmlAttribute xmlAttribute = xmlNode.Attributes["style"];
						if (xmlAttribute != null)
						{
							foreach (string text in xmlAttribute.Value.Split(new char[]
							{
								';'
							}))
							{
								if (!(text == string.Empty))
								{
									string[] array2 = text.Trim().Split(new char[]
									{
										':'
									}, 2);
									if (array2.Length == 2 && !(array2[1] == string.Empty))
									{
										stringDictionary[array2[0]] = array2[1].Trim();
									}
								}
							}
						}
						foreach (object obj4 in ((StringDictionary)hashtable["properties"]).Keys)
						{
							string key = (string)obj4;
							stringDictionary[key] = ((StringDictionary)hashtable["properties"])[key];
						}
						ArrayList arrayList = new ArrayList();
						foreach (object obj5 in stringDictionary.Keys)
						{
							string text2 = (string)obj5;
							arrayList.Add(text2 + ": " + stringDictionary[text2] + ";");
						}
						string innerText = string.Join(" ", (string[])arrayList.ToArray(typeof(string)));
						if (innerText != string.Empty)
						{
							XmlAttribute xmlAttribute2 = this.c.CreateAttribute("style");
							xmlAttribute2.InnerText = innerText;
							xmlNode.Attributes.Append(xmlAttribute2);
						}
					}
				}
			}
			return this.c;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00053674 File Offset: 0x00052674
		private void a()
		{
			string[] array = Regex.Replace(Regex.Replace(this.a.Replace("\r", "").Replace("\n", "").Replace('"', '\''), "/\\*.*?\\*/", ""), "\\s\\s+", " ").Split(new char[]
			{
				'}'
			});
			int num = 1;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					'{'
				});
				if (array3.Length >= 2 && !(array3[1] == string.Empty))
				{
					string text = array3[0].Trim();
					string a_ = array3[1].Trim();
					foreach (string text2 in text.Split(new char[]
					{
						','
					}))
					{
						if (text2.IndexOf(':') == -1)
						{
							string text3 = text2.Trim();
							Hashtable hashtable = new Hashtable();
							hashtable["selector"] = text3;
							hashtable["properties"] = this.a(a_);
							hashtable["specifity"] = this.c(text3);
							this.b.Add(hashtable);
						}
					}
					num++;
				}
			}
			this.b.Sort(new i());
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000537DC File Offset: 0x000527DC
		private StringDictionary a(string A_0)
		{
			string[] array = A_0.Split(new char[]
			{
				';'
			});
			StringDictionary stringDictionary = new StringDictionary();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					':'
				}, 2);
				if (array3.Length == 2 && !(array3[1] == string.Empty))
				{
					stringDictionary[array3[0].Trim()] = array3[1].Trim();
				}
			}
			return stringDictionary;
		}

		// Token: 0x04000F44 RID: 3908
		private string a;

		// Token: 0x04000F45 RID: 3909
		private ArrayList b = new ArrayList();

		// Token: 0x04000F46 RID: 3910
		private XmlDocument c;
	}
}
