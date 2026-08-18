using System;
using System.Drawing;
using System.IO;
using System.Text;
using Net.Sgoliver.NRtfTree.Util;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x02000017 RID: 23
	public class RtfTree
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000615F File Offset: 0x0000435F
		public RtfTree()
		{
			this.rootNode = new RtfTreeNode(RtfNodeType.Root, "ROOT", false, 0);
			this.rootNode.Tree = this;
			this.mergeSpecialCharacters = false;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006190 File Offset: 0x00004390
		public RtfTree CloneTree()
		{
			return new RtfTree
			{
				rootNode = this.rootNode.CloneNode(true)
			};
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000061B8 File Offset: 0x000043B8
		public int LoadRtfFile(string path)
		{
			this.rtf = new StreamReader(path);
			this.lex = new RtfLex(this.rtf);
			int result = this.parseRtfTree();
			this.rtf.Close();
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000061F8 File Offset: 0x000043F8
		public int LoadRtfText(string text)
		{
			this.rtf = new StringReader(text);
			this.lex = new RtfLex(this.rtf);
			int result = this.parseRtfTree();
			this.rtf.Close();
			return result;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006238 File Offset: 0x00004438
		public void SaveRtf(string filePath)
		{
			using (StreamWriter streamWriter = new StreamWriter(filePath))
			{
				streamWriter.Write(this.RootNode.Rtf);
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006288 File Offset: 0x00004488
		public string GetRtf()
		{
			return this.RootNode.Rtf;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006298 File Offset: 0x00004498
		public override string ToString()
		{
			return this.toStringInm(this.rootNode, 0, false);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000062BC File Offset: 0x000044BC
		public string ToStringEx()
		{
			return this.toStringInm(this.rootNode, 0, true);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000062E0 File Offset: 0x000044E0
		public RtfFontTable GetFontTable()
		{
			RtfFontTable rtfFontTable = new RtfFontTable();
			RtfTreeNode rtfTreeNode = this.rootNode;
			RtfTreeNode firstChild = rtfTreeNode.FirstChild;
			bool flag = false;
			int num = 0;
			RtfTreeNode rtfTreeNode2 = new RtfTreeNode();
			while (!flag && num < firstChild.ChildNodes.Count)
			{
				if (firstChild.ChildNodes[num].NodeType == RtfNodeType.Group && firstChild.ChildNodes[num].FirstChild.NodeKey == "fonttbl")
				{
					flag = true;
					rtfTreeNode2 = firstChild.ChildNodes[num];
				}
				num++;
			}
			for (int i = 1; i < rtfTreeNode2.ChildNodes.Count; i++)
			{
				RtfTreeNode rtfTreeNode3 = rtfTreeNode2.ChildNodes[i];
				int index = -1;
				string name = null;
				foreach (object obj in rtfTreeNode3.ChildNodes)
				{
					RtfTreeNode rtfTreeNode4 = (RtfTreeNode)obj;
					if (rtfTreeNode4.NodeKey == "f")
					{
						index = rtfTreeNode4.Parameter;
					}
					if (rtfTreeNode4.NodeType == RtfNodeType.Text)
					{
						name = rtfTreeNode4.NodeKey.Substring(0, rtfTreeNode4.NodeKey.Length - 1);
					}
				}
				rtfFontTable.AddFont(index, name);
			}
			return rtfFontTable;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006444 File Offset: 0x00004644
		public RtfColorTable GetColorTable()
		{
			RtfColorTable rtfColorTable = new RtfColorTable();
			RtfTreeNode rtfTreeNode = this.rootNode;
			RtfTreeNode firstChild = rtfTreeNode.FirstChild;
			bool flag = false;
			int num = 0;
			RtfTreeNode rtfTreeNode2 = new RtfTreeNode();
			while (!flag && num < firstChild.ChildNodes.Count)
			{
				if (firstChild.ChildNodes[num].NodeType == RtfNodeType.Group && firstChild.ChildNodes[num].FirstChild.NodeKey == "colortbl")
				{
					flag = true;
					rtfTreeNode2 = firstChild.ChildNodes[num];
				}
				num++;
			}
			int red = 0;
			int green = 0;
			int blue = 0;
			for (int i = 1; i < rtfTreeNode2.ChildNodes.Count; i++)
			{
				RtfTreeNode rtfTreeNode3 = rtfTreeNode2.ChildNodes[i];
				string nodeKey;
				if (rtfTreeNode3.NodeType == RtfNodeType.Text && rtfTreeNode3.NodeKey.Trim() == ";")
				{
					rtfColorTable.AddColor(Color.FromArgb(red, green, blue));
					red = 0;
					green = 0;
					blue = 0;
				}
				else if (rtfTreeNode3.NodeType == RtfNodeType.Keyword && (nodeKey = rtfTreeNode3.NodeKey) != null)
				{
					if (!(nodeKey == "red"))
					{
						if (!(nodeKey == "green"))
						{
							if (nodeKey == "blue")
							{
								blue = rtfTreeNode3.Parameter;
							}
						}
						else
						{
							green = rtfTreeNode3.Parameter;
						}
					}
					else
					{
						red = rtfTreeNode3.Parameter;
					}
				}
			}
			return rtfColorTable;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000065B0 File Offset: 0x000047B0
		public RtfStyleSheetTable GetStyleSheetTable()
		{
			RtfStyleSheetTable rtfStyleSheetTable = new RtfStyleSheetTable();
			RtfTreeNode rtfTreeNode = this.MainGroup.SelectSingleGroup("stylesheet");
			RtfNodeCollection childNodes = rtfTreeNode.ChildNodes;
			for (int i = 1; i < childNodes.Count; i++)
			{
				RtfTreeNode ssnode = childNodes[i];
				RtfStyleSheet rtfStyleSheet = this.ParseStyleSheet(ssnode);
				rtfStyleSheetTable.AddStyleSheet(rtfStyleSheet.Index, rtfStyleSheet);
			}
			return rtfStyleSheetTable;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006610 File Offset: 0x00004810
		public InfoGroup GetInfoGroup()
		{
			InfoGroup infoGroup = null;
			RtfTreeNode rtfTreeNode = this.RootNode.SelectSingleNode("info");
			if (rtfTreeNode != null)
			{
				infoGroup = new InfoGroup();
				RtfTreeNode rtfTreeNode2;
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("title")) != null)
				{
					infoGroup.Title = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("subject")) != null)
				{
					infoGroup.Subject = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("author")) != null)
				{
					infoGroup.Author = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("manager")) != null)
				{
					infoGroup.Manager = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("company")) != null)
				{
					infoGroup.Company = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("operator")) != null)
				{
					infoGroup.Operator = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("category")) != null)
				{
					infoGroup.Category = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("keywords")) != null)
				{
					infoGroup.Keywords = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("comment")) != null)
				{
					infoGroup.Comment = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("doccomm")) != null)
				{
					infoGroup.DocComment = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("hlinkbase")) != null)
				{
					infoGroup.HlinkBase = rtfTreeNode2.NextSibling.NodeKey;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("version")) != null)
				{
					infoGroup.Version = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("vern")) != null)
				{
					infoGroup.InternalVersion = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("edmins")) != null)
				{
					infoGroup.EditingTime = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("nofpages")) != null)
				{
					infoGroup.NumberOfPages = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("nofchars")) != null)
				{
					infoGroup.NumberOfChars = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("nofwords")) != null)
				{
					infoGroup.NumberOfWords = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("id")) != null)
				{
					infoGroup.Id = rtfTreeNode2.Parameter;
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("creatim")) != null)
				{
					infoGroup.CreationTime = RtfTree.parseDateTime(rtfTreeNode2.ParentNode);
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("revtim")) != null)
				{
					infoGroup.RevisionTime = RtfTree.parseDateTime(rtfTreeNode2.ParentNode);
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("printim")) != null)
				{
					infoGroup.LastPrintTime = RtfTree.parseDateTime(rtfTreeNode2.ParentNode);
				}
				if ((rtfTreeNode2 = this.rootNode.SelectSingleNode("buptim")) != null)
				{
					infoGroup.BackupTime = RtfTree.parseDateTime(rtfTreeNode2.ParentNode);
				}
			}
			return infoGroup;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000694C File Offset: 0x00004B4C
		public Encoding GetEncoding()
		{
			Encoding result = Encoding.Default;
			RtfTreeNode rtfTreeNode = this.RootNode.SelectSingleNode("ansicpg");
			if (rtfTreeNode != null)
			{
				result = Encoding.GetEncoding(rtfTreeNode.Parameter);
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006980 File Offset: 0x00004B80
		private int parseRtfTree()
		{
			int result = 0;
			Encoding enc = Encoding.Default;
			RtfTreeNode rtfTreeNode = this.rootNode;
			this.tok = this.lex.NextToken();
			while (this.tok.Type != RtfTokenType.Eof)
			{
				switch (this.tok.Type)
				{
				case RtfTokenType.Keyword:
				case RtfTokenType.Control:
				case RtfTokenType.Text:
				{
					RtfTreeNode rtfTreeNode2;
					if (this.mergeSpecialCharacters)
					{
						bool flag = this.tok.Type == RtfTokenType.Text || (this.tok.Type == RtfTokenType.Control && this.tok.Key == "'");
						if (rtfTreeNode.LastChild != null && rtfTreeNode.LastChild.NodeType == RtfNodeType.Text && flag)
						{
							if (this.tok.Type == RtfTokenType.Text)
							{
								RtfTreeNode lastChild = rtfTreeNode.LastChild;
								lastChild.NodeKey += this.tok.Key;
								break;
							}
							if (this.tok.Type == RtfTokenType.Control && this.tok.Key == "'")
							{
								RtfTreeNode lastChild2 = rtfTreeNode.LastChild;
								lastChild2.NodeKey += RtfTree.DecodeControlChar(this.tok.Parameter, enc);
								break;
							}
						}
						else if (this.tok.Type == RtfTokenType.Control && this.tok.Key == "'")
						{
							rtfTreeNode2 = new RtfTreeNode(RtfNodeType.Text, RtfTree.DecodeControlChar(this.tok.Parameter, enc), false, 0);
							rtfTreeNode.AppendChild(rtfTreeNode2);
							break;
						}
					}
					rtfTreeNode2 = new RtfTreeNode(this.tok);
					rtfTreeNode.AppendChild(rtfTreeNode2);
					if (this.mergeSpecialCharacters && this.level == 1 && rtfTreeNode2.NodeType == RtfNodeType.Keyword && rtfTreeNode2.NodeKey == "ansicpg")
					{
						enc = Encoding.GetEncoding(rtfTreeNode2.Parameter);
					}
					break;
				}
				case RtfTokenType.Eof:
					goto IL_222;
				case RtfTokenType.GroupStart:
				{
					RtfTreeNode rtfTreeNode2 = new RtfTreeNode(RtfNodeType.Group, "GROUP", false, 0);
					rtfTreeNode.AppendChild(rtfTreeNode2);
					rtfTreeNode = rtfTreeNode2;
					this.level++;
					break;
				}
				case RtfTokenType.GroupEnd:
					rtfTreeNode = rtfTreeNode.ParentNode;
					this.level--;
					break;
				default:
					goto IL_222;
				}
				IL_224:
				this.tok = this.lex.NextToken();
				continue;
				IL_222:
				result = -1;
				goto IL_224;
			}
			if (this.level != 0)
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006BE0 File Offset: 0x00004DE0
		private static string DecodeControlChar(int code, Encoding enc)
		{
			return enc.GetString(new byte[]
			{
				(byte)code
			});
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006C00 File Offset: 0x00004E00
		private string toStringInm(RtfTreeNode curNode, int level, bool showNodeTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RtfNodeCollection childNodes = curNode.ChildNodes;
			for (int i = 0; i < level; i++)
			{
				stringBuilder.Append("  ");
			}
			if (curNode.NodeType == RtfNodeType.Root)
			{
				stringBuilder.Append("ROOT\r\n");
			}
			else if (curNode.NodeType == RtfNodeType.Group)
			{
				stringBuilder.Append("GROUP\r\n");
			}
			else
			{
				if (showNodeTypes)
				{
					stringBuilder.Append(curNode.NodeType);
					stringBuilder.Append(": ");
				}
				stringBuilder.Append(curNode.NodeKey);
				if (curNode.HasParameter)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(Convert.ToString(curNode.Parameter));
				}
				stringBuilder.Append("\r\n");
			}
			if (childNodes != null)
			{
				foreach (object obj in childNodes)
				{
					RtfTreeNode curNode2 = (RtfTreeNode)obj;
					stringBuilder.Append(this.toStringInm(curNode2, level + 1, showNodeTypes));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006D24 File Offset: 0x00004F24
		private static DateTime parseDateTime(RtfTreeNode group)
		{
			int year = 0;
			int month = 0;
			int day = 0;
			int hour = 0;
			int minute = 0;
			int second = 0;
			foreach (object obj in group.ChildNodes)
			{
				RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
				string nodeKey;
				if ((nodeKey = rtfTreeNode.NodeKey) != null)
				{
					if (!(nodeKey == "yr"))
					{
						if (!(nodeKey == "mo"))
						{
							if (!(nodeKey == "dy"))
							{
								if (!(nodeKey == "hr"))
								{
									if (!(nodeKey == "min"))
									{
										if (nodeKey == "sec")
										{
											second = rtfTreeNode.Parameter;
										}
									}
									else
									{
										minute = rtfTreeNode.Parameter;
									}
								}
								else
								{
									hour = rtfTreeNode.Parameter;
								}
							}
							else
							{
								day = rtfTreeNode.Parameter;
							}
						}
						else
						{
							month = rtfTreeNode.Parameter;
						}
					}
					else
					{
						year = rtfTreeNode.Parameter;
					}
				}
			}
			DateTime result = new DateTime(year, month, day, hour, minute, second);
			return result;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006E48 File Offset: 0x00005048
		private string ConvertToText()
		{
			RtfTreeNode node = this.RootNode.FirstChild.SelectSingleChildNode("pard");
			int prim = this.RootNode.FirstChild.ChildNodes.IndexOf(node);
			Encoding encoding = this.GetEncoding();
			return this.ConvertToTextAux(this.RootNode.FirstChild, prim, encoding);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006E9C File Offset: 0x0000509C
		private string ConvertToTextAux(RtfTreeNode curNode, int prim, Encoding enc)
		{
			StringBuilder stringBuilder = new StringBuilder("");
			RtfTreeNode rtfTreeNode = new RtfTreeNode();
			for (int i = prim; i < curNode.ChildNodes.Count; i++)
			{
				rtfTreeNode = curNode.ChildNodes[i];
				if (rtfTreeNode.NodeType == RtfNodeType.Group)
				{
					int index = rtfTreeNode.FirstChild.NodeKey.Equals("*") ? 1 : 0;
					if (!rtfTreeNode.ChildNodes[index].NodeKey.Equals("pict") && !rtfTreeNode.ChildNodes[index].NodeKey.Equals("object") && !rtfTreeNode.ChildNodes[index].NodeKey.Equals("fldinst"))
					{
						stringBuilder.Append(this.ConvertToTextAux(rtfTreeNode, 0, enc));
					}
				}
				else if (rtfTreeNode.NodeType == RtfNodeType.Control)
				{
					if (rtfTreeNode.NodeKey == "'")
					{
						stringBuilder.Append(RtfTree.DecodeControlChar(rtfTreeNode.Parameter, enc));
					}
				}
				else if (rtfTreeNode.NodeType == RtfNodeType.Text)
				{
					stringBuilder.Append(rtfTreeNode.NodeKey);
				}
				else if (rtfTreeNode.NodeType == RtfNodeType.Keyword && rtfTreeNode.NodeKey.Equals("par"))
				{
					stringBuilder.AppendLine("");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006FF8 File Offset: 0x000051F8
		private RtfStyleSheet ParseStyleSheet(RtfTreeNode ssnode)
		{
			RtfStyleSheet rtfStyleSheet = new RtfStyleSheet();
			foreach (object obj in ssnode.ChildNodes)
			{
				RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
				if (rtfTreeNode.NodeKey == "cs")
				{
					rtfStyleSheet.Type = RtfStyleSheetType.Character;
					rtfStyleSheet.Index = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "s")
				{
					rtfStyleSheet.Type = RtfStyleSheetType.Paragraph;
					rtfStyleSheet.Index = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "ds")
				{
					rtfStyleSheet.Type = RtfStyleSheetType.Section;
					rtfStyleSheet.Index = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "ts")
				{
					rtfStyleSheet.Type = RtfStyleSheetType.Table;
					rtfStyleSheet.Index = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "additive")
				{
					rtfStyleSheet.Additive = true;
				}
				else if (rtfTreeNode.NodeKey == "sbasedon")
				{
					rtfStyleSheet.BasedOn = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "snext")
				{
					rtfStyleSheet.Next = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "sautoupd")
				{
					rtfStyleSheet.AutoUpdate = true;
				}
				else if (rtfTreeNode.NodeKey == "shidden")
				{
					rtfStyleSheet.Hidden = true;
				}
				else if (rtfTreeNode.NodeKey == "slink")
				{
					rtfStyleSheet.Link = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "slocked")
				{
					rtfStyleSheet.Locked = true;
				}
				else if (rtfTreeNode.NodeKey == "spersonal")
				{
					rtfStyleSheet.Personal = true;
				}
				else if (rtfTreeNode.NodeKey == "scompose")
				{
					rtfStyleSheet.Compose = true;
				}
				else if (rtfTreeNode.NodeKey == "sreply")
				{
					rtfStyleSheet.Reply = true;
				}
				else if (rtfTreeNode.NodeKey == "styrsid")
				{
					rtfStyleSheet.Styrsid = rtfTreeNode.Parameter;
				}
				else if (rtfTreeNode.NodeKey == "ssemihidden")
				{
					rtfStyleSheet.SemiHidden = true;
				}
				else if (rtfTreeNode.NodeType == RtfNodeType.Group && rtfTreeNode.ChildNodes[0].NodeKey == "*" && rtfTreeNode.ChildNodes[1].NodeKey == "keycode")
				{
					rtfStyleSheet.KeyCode = new RtfNodeCollection();
					for (int i = 2; i < rtfTreeNode.ChildNodes.Count; i++)
					{
						rtfStyleSheet.KeyCode.Add(rtfTreeNode.ChildNodes[i].CloneNode(true));
					}
				}
				else if (rtfTreeNode.NodeType == RtfNodeType.Text)
				{
					rtfStyleSheet.Name = rtfTreeNode.NodeKey.Substring(0, rtfTreeNode.NodeKey.Length - 1);
				}
				else if (rtfTreeNode.NodeKey != "*")
				{
					rtfStyleSheet.Formatting.Add(rtfTreeNode);
				}
			}
			return rtfStyleSheet;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007358 File Offset: 0x00005558
		public RtfTreeNode RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007360 File Offset: 0x00005560
		public RtfTreeNode MainGroup
		{
			get
			{
				if (this.rootNode.HasChildNodes())
				{
					return this.rootNode.ChildNodes[0];
				}
				return null;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007382 File Offset: 0x00005582
		public string Rtf
		{
			get
			{
				return this.rootNode.Rtf;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000738F File Offset: 0x0000558F
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00007397 File Offset: 0x00005597
		public bool MergeSpecialCharacters
		{
			get
			{
				return this.mergeSpecialCharacters;
			}
			set
			{
				this.mergeSpecialCharacters = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000073A0 File Offset: 0x000055A0
		public string Text
		{
			get
			{
				return this.ConvertToText();
			}
		}

		// Token: 0x04000075 RID: 117
		private RtfTreeNode rootNode;

		// Token: 0x04000076 RID: 118
		private TextReader rtf;

		// Token: 0x04000077 RID: 119
		private RtfLex lex;

		// Token: 0x04000078 RID: 120
		private RtfToken tok;

		// Token: 0x04000079 RID: 121
		private int level;

		// Token: 0x0400007A RID: 122
		private bool mergeSpecialCharacters;
	}
}
