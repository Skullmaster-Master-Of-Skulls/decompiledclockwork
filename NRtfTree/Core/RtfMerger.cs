using System;
using System.Collections.Generic;
using System.Drawing;
using Net.Sgoliver.NRtfTree.Util;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x0200000D RID: 13
	public class RtfMerger
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000051EF File Offset: 0x000033EF
		public RtfMerger(string sSourceDocFullPathName, string sDestFileFullPathName, bool bolRemoveLastParCmd)
		{
			this.baseRtfDoc = new RtfTree();
			this.baseRtfDoc.LoadRtfFile(sSourceDocFullPathName);
			this.destFilePath = sDestFileFullPathName;
			this.removeLastPar = bolRemoveLastParCmd;
			this.placeHolder = new Dictionary<string, RtfTree>();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005228 File Offset: 0x00003428
		public void AddPlaceHolder(string ph, string path)
		{
			RtfTree rtfTree = new RtfTree();
			if (rtfTree.LoadRtfFile(path) == 0)
			{
				this.placeHolder.Add(ph, rtfTree);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005253 File Offset: 0x00003453
		public void RemovePlaceHolder(string ph)
		{
			this.placeHolder.Remove(ph);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005264 File Offset: 0x00003464
		public void MergeRtfDoc()
		{
			RtfTreeNode mainGroup = this.baseRtfDoc.MainGroup;
			if (mainGroup != null)
			{
				this.analizeTextContent(mainGroup);
				if (this.destFilePath != null)
				{
					this.baseRtfDoc.SaveRtf(this.destFilePath);
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000052A0 File Offset: 0x000034A0
		private void analizeTextContent(RtfTreeNode parentNode)
		{
			if (parentNode != null && parentNode.HasChildNodes())
			{
				int i = 0;
				while (i < parentNode.ChildNodes.Count)
				{
					RtfTreeNode rtfTreeNode = parentNode.ChildNodes[i];
					if (rtfTreeNode.NodeType == RtfNodeType.Text)
					{
						using (Dictionary<string, RtfTree>.KeyCollection.Enumerator enumerator = this.placeHolder.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								int num = rtfTreeNode.NodeKey.IndexOf(text);
								if (num != -1)
								{
									RtfTree docToInsert = this.placeHolder[text].CloneTree();
									this.mergeCore(parentNode, i, docToInsert, text, num);
									i--;
									break;
								}
							}
							goto IL_AE;
						}
						goto IL_9F;
					}
					goto IL_9F;
					IL_AE:
					i++;
					continue;
					IL_9F:
					if (rtfTreeNode.HasChildNodes())
					{
						this.analizeTextContent(rtfTreeNode);
						goto IL_AE;
					}
					goto IL_AE;
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005380 File Offset: 0x00003580
		private void mergeCore(RtfTreeNode parentNode, int iNdIndex, RtfTree docToInsert, string strCompletePlaceholder, int intPlaceHolderNodePos)
		{
			if (docToInsert.RootNode.HasChildNodes())
			{
				int num = iNdIndex + 1;
				this.mainAdjustColor(docToInsert);
				this.mainAdjustFont(docToInsert);
				this.cleanToInsertDoc(docToInsert);
				if (docToInsert.RootNode.FirstChild.HasChildNodes())
				{
					this.execMergeDoc(parentNode, docToInsert, num);
				}
				if (parentNode.ChildNodes[iNdIndex].NodeKey.Length != intPlaceHolderNodePos + strCompletePlaceholder.Length)
				{
					string key = parentNode.ChildNodes[iNdIndex].NodeKey.Substring(parentNode.ChildNodes[iNdIndex].NodeKey.IndexOf(strCompletePlaceholder) + strCompletePlaceholder.Length);
					parentNode.InsertChild(num + 1, new RtfTreeNode(RtfNodeType.Text, key, false, 0));
				}
				if (intPlaceHolderNodePos == 0)
				{
					parentNode.RemoveChild(iNdIndex);
					return;
				}
				parentNode.ChildNodes[iNdIndex].NodeKey = parentNode.ChildNodes[iNdIndex].NodeKey.Substring(0, intPlaceHolderNodePos);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005474 File Offset: 0x00003674
		private int getFontID(ref RtfFontTable fontDestTbl, string sFontName)
		{
			int num;
			if ((num = fontDestTbl.IndexOf(sFontName)) == -1)
			{
				fontDestTbl.AddFont(sFontName);
				num = fontDestTbl.IndexOf(sFontName);
				RtfNodeCollection rtfNodeCollection = this.baseRtfDoc.RootNode.SelectNodes("fonttbl");
				RtfTreeNode rtfTreeNode = new RtfTreeNode(RtfNodeType.Group);
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "f", true, num));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "fnil", false, 0));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Text, sFontName + ";", false, 0));
				rtfNodeCollection[0].ParentNode.AppendChild(rtfTreeNode);
			}
			return num;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005510 File Offset: 0x00003710
		private int getColorID(RtfColorTable colorDestTbl, Color iColorName)
		{
			int result;
			if ((result = colorDestTbl.IndexOf(iColorName)) == -1)
			{
				result = colorDestTbl.Count;
				colorDestTbl.AddColor(iColorName);
				RtfNodeCollection rtfNodeCollection = this.baseRtfDoc.RootNode.SelectNodes("colortbl");
				rtfNodeCollection[0].ParentNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "red", true, (int)iColorName.R));
				rtfNodeCollection[0].ParentNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "green", true, (int)iColorName.G));
				rtfNodeCollection[0].ParentNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "blue", true, (int)iColorName.B));
				rtfNodeCollection[0].ParentNode.AppendChild(new RtfTreeNode(RtfNodeType.Text, ";", false, 0));
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000055DC File Offset: 0x000037DC
		private void mainAdjustFont(RtfTree docToInsert)
		{
			RtfFontTable fontTable = this.baseRtfDoc.GetFontTable();
			RtfFontTable fontTable2 = docToInsert.GetFontTable();
			this.adjustFontRecursive(docToInsert.RootNode, fontTable, fontTable2);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000560C File Offset: 0x0000380C
		private void adjustFontRecursive(RtfTreeNode parentNode, RtfFontTable fontDestTbl, RtfFontTable fontToCopyTbl)
		{
			if (parentNode != null && parentNode.HasChildNodes())
			{
				for (int i = 0; i < parentNode.ChildNodes.Count; i++)
				{
					if (parentNode.ChildNodes[i].NodeType == RtfNodeType.Keyword && (parentNode.ChildNodes[i].NodeKey == "f" || parentNode.ChildNodes[i].NodeKey == "stshfdbch" || parentNode.ChildNodes[i].NodeKey == "stshfloch" || parentNode.ChildNodes[i].NodeKey == "stshfhich" || parentNode.ChildNodes[i].NodeKey == "stshfbi" || parentNode.ChildNodes[i].NodeKey == "deff" || parentNode.ChildNodes[i].NodeKey == "af") && parentNode.ChildNodes[i].HasParameter)
					{
						parentNode.ChildNodes[i].Parameter = this.getFontID(ref fontDestTbl, fontToCopyTbl[parentNode.ChildNodes[i].Parameter]);
					}
					this.adjustFontRecursive(parentNode.ChildNodes[i], fontDestTbl, fontToCopyTbl);
				}
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005788 File Offset: 0x00003988
		private void mainAdjustColor(RtfTree docToInsert)
		{
			RtfColorTable colorTable = this.baseRtfDoc.GetColorTable();
			RtfColorTable colorTable2 = docToInsert.GetColorTable();
			this.adjustColorRecursive(docToInsert.RootNode, colorTable, colorTable2);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000057B8 File Offset: 0x000039B8
		private void adjustColorRecursive(RtfTreeNode parentNode, RtfColorTable colorDestTbl, RtfColorTable colorToCopyTbl)
		{
			if (parentNode != null && parentNode.HasChildNodes())
			{
				for (int i = 0; i < parentNode.ChildNodes.Count; i++)
				{
					if (parentNode.ChildNodes[i].NodeType == RtfNodeType.Keyword && (parentNode.ChildNodes[i].NodeKey == "cf" || parentNode.ChildNodes[i].NodeKey == "cb" || parentNode.ChildNodes[i].NodeKey == "pncf" || parentNode.ChildNodes[i].NodeKey == "brdrcf" || parentNode.ChildNodes[i].NodeKey == "cfpat" || parentNode.ChildNodes[i].NodeKey == "cbpat" || parentNode.ChildNodes[i].NodeKey == "clcfpatraw" || parentNode.ChildNodes[i].NodeKey == "clcbpatraw" || parentNode.ChildNodes[i].NodeKey == "ulc" || parentNode.ChildNodes[i].NodeKey == "chcfpat" || parentNode.ChildNodes[i].NodeKey == "chcbpat" || parentNode.ChildNodes[i].NodeKey == "highlight" || parentNode.ChildNodes[i].NodeKey == "clcbpat" || parentNode.ChildNodes[i].NodeKey == "clcfpat") && parentNode.ChildNodes[i].HasParameter)
					{
						parentNode.ChildNodes[i].Parameter = this.getColorID(colorDestTbl, colorToCopyTbl[parentNode.ChildNodes[i].Parameter]);
					}
					this.adjustColorRecursive(parentNode.ChildNodes[i], colorDestTbl, colorToCopyTbl);
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005A10 File Offset: 0x00003C10
		private void execMergeDoc(RtfTreeNode parentNode, RtfTree treeToCopyParent, int intCurrIndex)
		{
			RtfTreeNode node = treeToCopyParent.RootNode.FirstChild.SelectSingleChildNode("pard");
			int num = treeToCopyParent.RootNode.FirstChild.ChildNodes.IndexOf(node);
			RtfTreeNode rtfTreeNode = new RtfTreeNode(RtfNodeType.Group);
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pard", false, 0));
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "plain", false, 0));
			for (int i = num + 1; i < treeToCopyParent.RootNode.FirstChild.ChildNodes.Count; i++)
			{
				RtfTreeNode newNode = treeToCopyParent.RootNode.FirstChild.ChildNodes[i].CloneNode(true);
				rtfTreeNode.AppendChild(newNode);
			}
			parentNode.InsertChild(intCurrIndex, rtfTreeNode);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005AC8 File Offset: 0x00003CC8
		private void cleanToInsertDoc(RtfTree docToInsert)
		{
			RtfTreeNode lastChild = docToInsert.RootNode.FirstChild.LastChild;
			if (this.removeLastPar && lastChild.NodeType == RtfNodeType.Keyword && lastChild.NodeKey == "par")
			{
				docToInsert.RootNode.FirstChild.RemoveChild(lastChild);
			}
		}

		// Token: 0x0400003F RID: 63
		private RtfTree baseRtfDoc;

		// Token: 0x04000040 RID: 64
		private string destFilePath;

		// Token: 0x04000041 RID: 65
		private bool removeLastPar;

		// Token: 0x04000042 RID: 66
		private Dictionary<string, RtfTree> placeHolder;
	}
}
