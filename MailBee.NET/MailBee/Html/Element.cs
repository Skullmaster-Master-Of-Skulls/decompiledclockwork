using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace MailBee.Html
{
	// Token: 0x02000004 RID: 4
	public class Element
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003679 File Offset: 0x00002679
		public TagAttributeCollection Attributes
		{
			get
			{
				if (this.TagName == null)
				{
					return null;
				}
				if (this.a.IsReparseNeeded)
				{
					this.a.d();
				}
				return this.a;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000036A3 File Offset: 0x000026A3
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000036C4 File Offset: 0x000026C4
		public string InnerHtml
		{
			get
			{
				this.a();
				return this.c.Substring(this.g, this.h);
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.c();
				this.a();
				this.OuterHtml = this.OuterHtml.Remove(this.g, this.h).Insert(this.g, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003711 File Offset: 0x00002711
		public ElementCollection InnerElements
		{
			get
			{
				this.a();
				return this.b;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000371F File Offset: 0x0000271F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000372D File Offset: 0x0000272D
		public string OuterHtml
		{
			get
			{
				this.c();
				return this.c;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.c = value;
				this.IsReparseNeeded = true;
				if (!this.o && this.ParentElement != null)
				{
					this.ParentElement.IsRebuildNeeded = true;
				}
				this.IsRebuildNeeded = false;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000376B File Offset: 0x0000276B
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000037A4 File Offset: 0x000027A4
		public string TagDefinition
		{
			get
			{
				this.a();
				if (this.i != this.k || this.i != 0)
				{
					return this.c.Substring(this.i, this.k);
				}
				return null;
			}
			set
			{
				if (value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.a();
				if (value == null)
				{
					this.i = 0;
					this.j = 0;
					this.k = 0;
					this.OuterHtml = this.InnerHtml;
					return;
				}
				StringBuilder stringBuilder = new StringBuilder("<");
				stringBuilder.Append(value);
				stringBuilder.Append(">");
				stringBuilder.Append(this.InnerHtml);
				if (this.l)
				{
					stringBuilder.Append("</").Append(value.Split(Element.m)[0]).Append(">");
				}
				this.OuterHtml = stringBuilder.ToString();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003859 File Offset: 0x00002859
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00003890 File Offset: 0x00002890
		public string TagName
		{
			get
			{
				this.a();
				if (this.i != this.j || this.i != 0)
				{
					return this.c.Substring(this.i, this.j);
				}
				return null;
			}
			set
			{
				if (value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				if (value == null)
				{
					this.i = 0;
					this.j = 0;
					this.k = 0;
					this.OuterHtml = this.InnerHtml;
					return;
				}
				StringBuilder stringBuilder = new StringBuilder("<");
				stringBuilder.Append(value);
				if (this.TagDefinition != null)
				{
					stringBuilder.Append(this.TagDefinition.Remove(this.i - 1, this.j));
				}
				stringBuilder.Append(">");
				stringBuilder.Append(this.InnerHtml);
				if (this.l)
				{
					stringBuilder.Append("</").Append(value).Append(">");
				}
				this.OuterHtml = stringBuilder.ToString();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000395B File Offset: 0x0000295B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003963 File Offset: 0x00002963
		internal bool IsReparseNeeded
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000396C File Offset: 0x0000296C
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00003974 File Offset: 0x00002974
		internal bool IsRebuildNeeded
		{
			get
			{
				return this.e;
			}
			set
			{
				if (!this.o)
				{
					this.e = value;
					if (this.e && this.ParentElement != null)
					{
						this.ParentElement.IsRebuildNeeded = value;
					}
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000039A1 File Offset: 0x000029A1
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000039A9 File Offset: 0x000029A9
		internal Element ParentElement
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000039B2 File Offset: 0x000029B2
		public Element()
		{
			this.a(null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000039D3 File Offset: 0x000029D3
		public Element(string htmlText) : this(htmlText, null)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000039DD File Offset: 0x000029DD
		internal Element(string A_0, Element A_1)
		{
			this.OuterHtml = A_0;
			this.a(A_1);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003A05 File Offset: 0x00002A05
		private void a(Element A_0)
		{
			this.b = new ElementCollection(this);
			this.a = new TagAttributeCollection(this);
			this.ParentElement = A_0;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003A28 File Offset: 0x00002A28
		public ElementReadOnlyCollection GetAllElements()
		{
			ElementCollection elementCollection = new ElementCollection();
			elementCollection.Add(this);
			this.a(elementCollection);
			return new ElementReadOnlyCollection(elementCollection);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003A50 File Offset: 0x00002A50
		internal void a(ElementCollection A_0)
		{
			foreach (object obj in this.InnerElements)
			{
				Element element = (Element)obj;
				A_0.Add(element);
				element.a(A_0);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003AB0 File Offset: 0x00002AB0
		public ElementReadOnlyCollection GetAllElementsByName(string tagName)
		{
			ElementCollection elementCollection = new ElementCollection();
			elementCollection.Add(this);
			this.a(elementCollection);
			return new ElementReadOnlyCollection(elementCollection, tagName);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003AD8 File Offset: 0x00002AD8
		public TagAttribute GetAttributeByName(string attrName)
		{
			if (attrName == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (object obj in this.Attributes)
			{
				TagAttribute tagAttribute = (TagAttribute)obj;
				if (tagAttribute.Name.ToLower() == attrName.ToLower())
				{
					return tagAttribute;
				}
			}
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003B54 File Offset: 0x00002B54
		public TagAttributeReadOnlyCollection GetAttributesByName(string attrName)
		{
			return new TagAttributeReadOnlyCollection(this.Attributes, attrName);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003B64 File Offset: 0x00002B64
		public ElementReadOnlyCollection GetInnerElementsByName(string tagName)
		{
			ElementCollection a_ = new ElementCollection();
			this.a(a_);
			return new ElementReadOnlyCollection(a_, tagName);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003B85 File Offset: 0x00002B85
		public void Remove()
		{
			if (this.ParentElement != null)
			{
				this.ParentElement.InnerElements.Remove(this);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BA4 File Offset: 0x00002BA4
		private void a()
		{
			if (!this.IsReparseNeeded)
			{
				return;
			}
			this.o = true;
			this.b = new ElementCollection(this);
			this.a = new TagAttributeCollection(this);
			this.g = 0;
			this.h = this.c.Length;
			this.i = (this.j = (this.k = 0));
			StringCollection stringCollection = new StringCollection();
			int num = 0;
			int num2 = 4;
			char c = ' ';
			bool flag = false;
			for (int i = 0; i < this.c.Length; i++)
			{
				if (num2 == 1 || num2 == 3)
				{
					num2 = 4;
				}
				if (num2 == 4)
				{
					if (this.c[i] == '<')
					{
						num2 = 0;
					}
					else
					{
						num2 = 2;
					}
					num = i;
					flag = false;
				}
				if (num2 == 0)
				{
					if (i > 2 && this.c.Length >= num + 4 && this.c.Substring(num, 4) == "<!--")
					{
						if (this.c.Substring(i - 2, 3) == "-->" || i == this.c.Length - 1)
						{
							num2 = 1;
						}
					}
					else if (!flag && this.c.Length >= num + "<!DOCTYPE".Length && this.c.Substring(num, "<!DOCTYPE".Length).ToUpper() == "<!DOCTYPE")
					{
						flag = true;
					}
					else if (this.c[i] == '\'' || this.c[i] == '"')
					{
						num2 = 5;
						c = this.c[i];
					}
					else if (this.c[i] == '[' && flag)
					{
						num2 = 6;
					}
					else if (this.c[i] == '>' || i == this.c.Length - 1)
					{
						num2 = 1;
					}
					else if (i < this.c.Length - 1 && this.c[i + 1] == '<')
					{
						num2 = 1;
					}
				}
				else if (num2 == 5 && (this.c[i] == '>' || i == this.c.Length - 1))
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 5 && c == this.c[i])
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 6 && (this.c[i] == ']' || i == this.c.Length - 1))
				{
					num2 = 0;
				}
				else if (num2 == 2 && ((i < this.c.Length - 1 && this.c[i + 1] == '<') || i == this.c.Length - 1))
				{
					num2 = 3;
				}
				if (num2 == 1 || num2 == 3 || i == this.c.Length - 1)
				{
					int length = i - num + 1;
					string value = this.c.Substring(num, length);
					stringCollection.Add(value);
				}
			}
			bool flag2 = false;
			List<int> list = new List<int>();
			for (int j = 0; j < stringCollection.Count; j++)
			{
				string text = stringCollection[j];
				if (flag2)
				{
					if (text.Length >= "</script>".Length && text[0] == '<' && text.ToLower().StartsWith("</script"))
					{
						flag2 = false;
					}
					else
					{
						list.Add(j);
					}
				}
				else if (text.Length >= "<script>".Length && text[0] == '<' && text.ToLower().StartsWith("<script"))
				{
					flag2 = true;
				}
			}
			for (int k = list.Count - 1; k > -1; k--)
			{
				stringCollection.RemoveAt(list[k]);
			}
			int num3 = 0;
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			string text2 = null;
			byte b = 1;
			for (int l = 0; l < stringCollection.Count; l++)
			{
				stringBuilder.Append(stringCollection[l]);
				if (stringCollection[l][0] == '<')
				{
					if (stringCollection[l].Length < 2 || stringCollection[l][1] != '/')
					{
						string text3 = stringCollection[l].Split(Element.m)[0];
						string text4 = text3.Substring(1, text3.Length - 1).ToLower();
						if (text2 == null && num3 == 0)
						{
							text2 = text4.ToLower();
						}
						if (text2 == text4)
						{
							num3++;
						}
					}
					else
					{
						string text5 = stringCollection[l].Split(Element.m)[0];
						string text6 = text5.Substring(2, text5.Length - 2).ToLower();
						if (text2 == text6)
						{
							num3--;
						}
					}
				}
				if (l + 1 < stringCollection.Count && stringCollection[l + 1][0] == '<' && num3 > 0 && text2 != null && (text2 == "p" || text2 == "br" || text2 == "br/" || text2 == "img" || text2 == "li" || text2 == "meta" || text2 == "base" || text2 == "head" || text2 == "link" || text2 == "input" || text2 == "bgsound" || text2 == "td" || text2.StartsWith("!--")))
				{
					string text7 = stringCollection[l + 1].Split(Element.m)[0];
					string text8 = text7.Substring(1, text7.Length - 1).ToLower();
					if (text2 == text8 || text2 == "br" || text2 == "br/" || text2 == "meta" || text2 == "base" || text2 == "head" || text2 == "link" || text2 == "img" || text2 == "input" || text2 == "bgsound" || text2.StartsWith("!--"))
					{
						num3 = 0;
					}
				}
				if (l == stringCollection.Count - 1)
				{
					this.l = (num3 == 0);
					if (num3 != 0)
					{
						b = 0;
					}
					num3 = 0;
				}
				if (num3 == 0 && stringBuilder.Length != 0)
				{
					if (stringBuilder.ToString() == this.c)
					{
						StringBuilder stringBuilder2 = new StringBuilder(string.Empty);
						if (stringCollection.Count == 1)
						{
							stringBuilder2.Append(stringCollection[0]);
							if (stringCollection[0].StartsWith("<") && stringCollection[0].EndsWith(">"))
							{
								this.g = stringCollection[0].Length;
								this.h = 0;
								this.i = 1;
								this.k = this.g - 2;
								this.j = ((!stringCollection[0].StartsWith("<!--")) ? (stringCollection[0].Split(Element.m)[0].Length - 1) : 3);
								this.a = new TagAttributeCollection(this.c.Substring(this.i + this.j, this.k - this.j).Trim(), this);
							}
						}
						else
						{
							for (int m = 1; m < stringCollection.Count - (int)b; m++)
							{
								stringBuilder2.Append(stringCollection[m]);
							}
							this.g = this.c.IndexOf(">" + stringBuilder2.ToString());
							if (this.g > -1)
							{
								this.g++;
								this.h = stringBuilder2.ToString().Length;
								this.i = 1;
								this.k = this.g - 2;
								this.j = ((!stringCollection[0].StartsWith("<!--")) ? (stringCollection[0].Split(Element.m)[0].Length - 1) : 3);
								this.a = new TagAttributeCollection(this.c.Substring(this.i + this.j, this.k - this.j), this);
								this.IsReparseNeeded = false;
								this.b.Add(new Element(this.c.Substring(this.g, this.h), this));
							}
							else
							{
								this.g = 0;
								this.h = 0;
								this.k = 0;
								this.a = new TagAttributeCollection();
								this.IsReparseNeeded = false;
							}
						}
					}
					else
					{
						this.b.Add(new Element(stringBuilder.ToString(), this));
					}
					stringBuilder = new StringBuilder(string.Empty);
					text2 = null;
				}
			}
			this.IsReparseNeeded = false;
			this.o = false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000451C File Offset: 0x0000351C
		internal void c()
		{
			if (!this.IsRebuildNeeded || this.o)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			if (this.TagName != null)
			{
				stringBuilder.Append("<").Append(this.TagName);
				if (this.Attributes != null && this.Attributes.Definition != string.Empty)
				{
					stringBuilder.Append(" " + this.Attributes.Definition);
				}
				stringBuilder.Append(">");
			}
			foreach (object obj in this.InnerElements)
			{
				Element element = (Element)obj;
				stringBuilder.Append(element.OuterHtml);
			}
			if (this.TagName != null && this.l)
			{
				stringBuilder.Append("</").Append(this.TagName).Append(">");
			}
			this.OuterHtml = stringBuilder.ToString();
			this.IsRebuildNeeded = false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004644 File Offset: 0x00003644
		public void Process(RuleSet rules, ProcessElementDelegate del)
		{
			if (rules == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.p = true;
			this.a(this, this, rules, del);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004664 File Offset: 0x00003664
		public string ProcessToString(RuleSet rules, ProcessElementDelegate del)
		{
			if (rules == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.p = true;
			Element element = new Element(this.OuterHtml);
			this.a(element, element, rules, del);
			return element.OuterHtml;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000046A0 File Offset: 0x000036A0
		public void Stop()
		{
			this.p = false;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000046AC File Offset: 0x000036AC
		private void b(Element A_0, Element A_1, RuleSet A_2, ProcessElementDelegate A_3)
		{
			foreach (object obj in A_2)
			{
				Rule rule = (Rule)obj;
				if (!this.p)
				{
					break;
				}
				Regex regex = new Regex("^" + rule.TagName + "$", RegexOptions.IgnoreCase);
				if (A_0.TagName != null && regex.Match(A_0.TagName).Success)
				{
					bool flag = true;
					if (rule.TagAttributes != null)
					{
						foreach (object obj2 in rule.TagAttributes)
						{
							TagAttribute tagAttribute = (TagAttribute)obj2;
							flag = false;
							Regex regex2 = new Regex(tagAttribute.Name, RegexOptions.IgnoreCase);
							Regex regex3 = null;
							if (tagAttribute.Value != null)
							{
								regex3 = new Regex("['\"]*" + tagAttribute.Value, RegexOptions.IgnoreCase);
							}
							foreach (object obj3 in A_0.Attributes)
							{
								TagAttribute tagAttribute2 = (TagAttribute)obj3;
								Group group = regex2.Match(tagAttribute2.Name);
								Match match = null;
								if (tagAttribute.Value != null && tagAttribute2.Value != null)
								{
									match = regex3.Match(tagAttribute2.Value);
								}
								if (group.Success && (tagAttribute.Value == null || (match != null && match.Success)))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								break;
							}
						}
					}
					if (flag && (A_3 == null || A_3(A_0, rule)))
					{
						if (!this.p)
						{
							break;
						}
						switch (rule.RuleType)
						{
						case TagRuleTypes.ProcessingRule:
							if (rule.ReplaceMode)
							{
								if (rule.AttrsToAdd.Count == 1)
								{
									for (int i = 0; i < rule.AttrsToRemove.Count; i++)
									{
										Regex regex4 = new Regex(rule.AttrsToRemove[i].Name, RegexOptions.IgnoreCase);
										Regex regex5 = null;
										if (rule.AttrsToRemove[i].Value != null)
										{
											regex5 = new Regex("['\"]*" + rule.AttrsToRemove[i].Value, RegexOptions.IgnoreCase);
										}
										for (int j = A_0.Attributes.Count - 1; j >= 0; j--)
										{
											Group group2 = regex4.Match(A_0.Attributes[j].Name);
											Match match2 = null;
											if (A_0.Attributes[j].Value != null && rule.AttrsToRemove[i].Value != null)
											{
												match2 = regex5.Match(A_0.Attributes[j].Value);
											}
											if (group2.Success && (rule.AttrsToRemove[i].Value == null || match2.Success))
											{
												A_0.Attributes[j].Remove();
											}
										}
									}
									A_0.Attributes.Add(rule.AttrsToAdd[0]);
								}
								else if (rule.AttrsToAdd.Count == rule.AttrsToRemove.Count)
								{
									for (int k = 0; k < rule.AttrsToRemove.Count; k++)
									{
										Regex regex6 = new Regex(rule.AttrsToRemove[k].Name, RegexOptions.IgnoreCase);
										Regex regex7 = null;
										if (rule.AttrsToRemove[k].Value != null)
										{
											regex7 = new Regex(rule.AttrsToRemove[k].Value, RegexOptions.IgnoreCase);
										}
										for (int l = 0; l < A_0.Attributes.Count; l++)
										{
											Group group3 = regex6.Match(A_0.Attributes[l].Name);
											Match match3 = null;
											if (rule.AttrsToRemove[k].Value != null && A_0.Attributes[l].Value != null)
											{
												match3 = regex7.Match(A_0.Attributes[l].Value);
											}
											if (group3.Success && (rule.AttrsToRemove[k].Value == null || match3.Success))
											{
												A_0.Attributes[l].Name = rule.AttrsToAdd[k].Name;
												A_0.Attributes[l].Value = rule.AttrsToAdd[k].Value;
											}
										}
									}
								}
							}
							else
							{
								if (rule.AttrsToRemove != null)
								{
									for (int m = 0; m < rule.AttrsToRemove.Count; m++)
									{
										Regex regex8 = new Regex(rule.AttrsToRemove[m].Name, RegexOptions.IgnoreCase);
										Regex regex9 = null;
										if (rule.AttrsToRemove[m].Value != null)
										{
											regex9 = new Regex("['\"]*" + rule.AttrsToRemove[m].Value, RegexOptions.IgnoreCase);
										}
										for (int n = A_0.Attributes.Count - 1; n >= 0; n--)
										{
											Group group4 = regex8.Match(A_0.Attributes[n].Name);
											Match match4 = null;
											if (rule.AttrsToRemove[m].Value != null)
											{
												match4 = regex9.Match(A_0.Attributes[n].Value);
											}
											if (group4.Success && (rule.AttrsToRemove[m].Value == null || match4.Success))
											{
												A_0.Attributes[n].Remove();
											}
										}
									}
								}
								if (rule.AttrsToAdd != null)
								{
									for (int num = 0; num < rule.AttrsToAdd.Count; num++)
									{
										A_0.Attributes.Add(rule.AttrsToAdd[num]);
									}
								}
							}
							break;
						case TagRuleTypes.RemovalRule:
							A_0.Remove();
							if (A_0 == A_1)
							{
								A_0.OuterHtml = string.Empty;
							}
							break;
						case TagRuleTypes.ReplacementRule:
							if (rule.ReplaceElem != null)
							{
								if (A_0.ParentElement == null)
								{
									A_0 = rule.ReplaceElem;
								}
								else
								{
									for (int num2 = 0; num2 < A_0.ParentElement.InnerElements.Count; num2++)
									{
										if (A_0.ParentElement.InnerElements[num2] == A_0)
										{
											A_0.ParentElement.InnerElements.RemoveAt(num2);
											A_0.ParentElement.InnerElements.Add(rule.ReplaceElem, num2);
											break;
										}
									}
								}
							}
							else if (rule.ReplaceTagDefinitionOnly)
							{
								A_0.TagDefinition = rule.ReplaceStr;
							}
							else
							{
								A_0.OuterHtml = rule.ReplaceStr;
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004DEC File Offset: 0x00003DEC
		private bool a(Element A_0, Element A_1, RuleSet A_2, ProcessElementDelegate A_3)
		{
			this.b(A_0, A_1, A_2, A_3);
			int num = A_0.InnerElements.Count - 1;
			while (num >= 0 && this.p)
			{
				this.p = this.a(A_0.InnerElements[num], A_1, A_2, A_3);
				if (!this.p)
				{
					break;
				}
				num--;
			}
			return this.p;
		}

		// Token: 0x04000019 RID: 25
		private TagAttributeCollection a;

		// Token: 0x0400001A RID: 26
		private ElementCollection b;

		// Token: 0x0400001B RID: 27
		private string c = string.Empty;

		// Token: 0x0400001C RID: 28
		private bool d = true;

		// Token: 0x0400001D RID: 29
		private bool e;

		// Token: 0x0400001E RID: 30
		private Element f;

		// Token: 0x0400001F RID: 31
		private int g;

		// Token: 0x04000020 RID: 32
		private int h;

		// Token: 0x04000021 RID: 33
		private int i;

		// Token: 0x04000022 RID: 34
		private int j;

		// Token: 0x04000023 RID: 35
		private int k;

		// Token: 0x04000024 RID: 36
		private bool l;

		// Token: 0x04000025 RID: 37
		private static readonly char[] m = " \t\r\n>".ToCharArray();

		// Token: 0x04000026 RID: 38
		private const string n = "['\"]*";

		// Token: 0x04000027 RID: 39
		private bool o;

		// Token: 0x04000028 RID: 40
		private bool p;

		// Token: 0x02000005 RID: 5
		private enum a
		{
			// Token: 0x0400002A RID: 42
			a,
			// Token: 0x0400002B RID: 43
			b,
			// Token: 0x0400002C RID: 44
			c,
			// Token: 0x0400002D RID: 45
			d,
			// Token: 0x0400002E RID: 46
			e,
			// Token: 0x0400002F RID: 47
			f,
			// Token: 0x04000030 RID: 48
			g
		}
	}
}
