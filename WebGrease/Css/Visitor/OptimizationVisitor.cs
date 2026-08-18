using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x020001A1 RID: 417
	internal class OptimizationVisitor : NodeVisitor
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0007C1EA File Offset: 0x0007A3EA
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0007C1F2 File Offset: 0x0007A3F2
		internal IEnumerable<string> NonMergeRuleSetSelectors { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0007C1FB File Offset: 0x0007A3FB
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0007C203 File Offset: 0x0007A403
		internal bool ShouldMergeMediaQueries { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0007C20C File Offset: 0x0007A40C
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0007C214 File Offset: 0x0007A414
		internal bool ShouldMergeBasedOnCommonDeclarations { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0007C21D File Offset: 0x0007A41D
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0007C225 File Offset: 0x0007A425
		internal bool ShouldPreventOrderBasedConflict { get; set; }

		// Token: 0x06001562 RID: 5474 RVA: 0x0007C230 File Offset: 0x0007A430
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			if (styleSheet == null)
			{
				return null;
			}
			OrderedDictionary mergedNodeDictionary = this.GetMergedNodeDictionary(styleSheet.StyleSheetRules);
			List<StyleSheetRuleNode> list = mergedNodeDictionary.Values.Cast<StyleSheetRuleNode>().ToList<StyleSheetRuleNode>();
			return new StyleSheetNode(styleSheet.CharSetString, styleSheet.Imports, styleSheet.Namespaces, list.AsSafeReadOnly<StyleSheetRuleNode>());
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0007C280 File Offset: 0x0007A480
		private static void OptimizeRulesetNode(RulesetNode currentRuleSet, OrderedDictionary ruleSetMediaPageDictionary, OrderedDictionary rulesetHashKeysDictionary, bool shouldPreventOrderBasedConflict)
		{
			string text = currentRuleSet.PrintSelector();
			string text2 = currentRuleSet.PrintSelector();
			if (rulesetHashKeysDictionary.Contains(text))
			{
				text2 = ((List<string>)rulesetHashKeysDictionary[text]).Last<string>();
			}
			else
			{
				rulesetHashKeysDictionary.Add(text, new List<string>());
				(rulesetHashKeysDictionary[text] as List<string>).Add(text);
			}
			if (OptimizationVisitor.ShouldCollapseTheNewRuleset(text2, ruleSetMediaPageDictionary, currentRuleSet, shouldPreventOrderBasedConflict))
			{
				RulesetNode value = OptimizationVisitor.MergeDeclarations((RulesetNode)ruleSetMediaPageDictionary[text2], currentRuleSet);
				ruleSetMediaPageDictionary.Remove(text2);
				ruleSetMediaPageDictionary.Add(text2, value);
				return;
			}
			RulesetNode rulesetNode = OptimizationVisitor.OptimizeRuleset(currentRuleSet);
			if (rulesetNode != null)
			{
				while (ruleSetMediaPageDictionary.Contains(text2))
				{
					text2 = OptimizationVisitor.GenerateRandomkey();
				}
				ruleSetMediaPageDictionary.Add(text2, rulesetNode);
				(rulesetHashKeysDictionary[text] as List<string>).Add(text2);
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0007C33C File Offset: 0x0007A53C
		private static void MergeBasedOnCommonDeclarations(RulesetNode currentRuleSet, OrderedDictionary ruleSetMediaPageDictionary)
		{
			string text = currentRuleSet.PrintSelector();
			IEnumerator enumerator = ruleSetMediaPageDictionary.Keys.GetEnumerator();
			object obj = ruleSetMediaPageDictionary[text];
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			bool flag = false;
			while (enumerator.MoveNext())
			{
				object obj2 = enumerator.Current;
				object obj3 = ruleSetMediaPageDictionary[obj2];
				if (ruleSetMediaPageDictionary.Contains(text) && obj.GetType().IsAssignableFrom(obj3.GetType()) && !obj2.Equals(text))
				{
					RulesetNode rulesetNode = (RulesetNode)obj3;
					RulesetNode rulesetNode2 = (RulesetNode)obj;
					if (rulesetNode.ShouldMergeWith(rulesetNode2))
					{
						RulesetNode mergedRulesetNode = rulesetNode2.GetMergedRulesetNode(rulesetNode);
						string key = mergedRulesetNode.PrintSelector();
						if (rulesetNode2.Declarations.Count < 1)
						{
							flag = true;
						}
						if (rulesetNode.Declarations.Count < 1)
						{
							orderedDictionary.Add(obj2, null);
						}
						while (ruleSetMediaPageDictionary.Contains(key) || orderedDictionary.Contains(key))
						{
							key = OptimizationVisitor.GenerateRandomkey();
						}
						orderedDictionary.Add(key, mergedRulesetNode);
					}
				}
			}
			if (flag)
			{
				orderedDictionary.Add(text, null);
			}
			foreach (object key2 in orderedDictionary.Keys)
			{
				if (orderedDictionary[key2] == null)
				{
					ruleSetMediaPageDictionary.Remove(key2);
				}
				else
				{
					ruleSetMediaPageDictionary.Add(key2, orderedDictionary[key2]);
				}
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0007C4B4 File Offset: 0x0007A6B4
		private static string GenerateRandomkey()
		{
			string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>(element, 16)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0007C500 File Offset: 0x0007A700
		private static bool ShouldCollapseTheNewRuleset(string hashKey, OrderedDictionary ruleSetMediaPageDictionary, RulesetNode currentRuleSet, bool shouldPreventOrderBasedConflict)
		{
			if (!ruleSetMediaPageDictionary.Contains(hashKey))
			{
				return false;
			}
			if (!shouldPreventOrderBasedConflict)
			{
				return true;
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			List<StyleSheetRuleNode> list = ruleSetMediaPageDictionary.Values.Cast<StyleSheetRuleNode>().ToList<StyleSheetRuleNode>();
			list.Reverse();
			foreach (StyleSheetRuleNode styleSheetRuleNode in list)
			{
				if (currentRuleSet.GetType().IsAssignableFrom(styleSheetRuleNode.GetType()))
				{
					RulesetNode rulesetNode = styleSheetRuleNode as RulesetNode;
					if (rulesetNode.PrintSelector().Equals(currentRuleSet.PrintSelector()))
					{
						return true;
					}
					foreach (DeclarationNode declarationNode in rulesetNode.Declarations)
					{
						string property = declarationNode.Property;
						if (!orderedDictionary.Contains(property))
						{
							orderedDictionary[property] = declarationNode;
						}
					}
					RulesetNode rulesetNode2 = (RulesetNode)ruleSetMediaPageDictionary[hashKey];
					if (rulesetNode2.HasConflictingDeclaration(orderedDictionary))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0007C630 File Offset: 0x0007A830
		private static RulesetNode MergeDeclarations(RulesetNode sourceRuleset, RulesetNode destinationRuleset)
		{
			OrderedDictionary orderedDictionary = OptimizationVisitor.UniqueDeclarations(destinationRuleset);
			OrderedDictionary orderedDictionary2 = OptimizationVisitor.UniqueDeclarations(sourceRuleset);
			foreach (object obj in orderedDictionary.Values)
			{
				DeclarationNode newDeclaration = (DeclarationNode)obj;
				OptimizationVisitor.AddDeclaration(orderedDictionary2, newDeclaration);
			}
			List<DeclarationNode> list = orderedDictionary2.Values.Cast<DeclarationNode>().ToList<DeclarationNode>();
			return new RulesetNode(destinationRuleset.SelectorsGroupNode, list.AsReadOnly(), sourceRuleset.ImportantComments);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0007C6C8 File Offset: 0x0007A8C8
		private static void AddDeclaration(OrderedDictionary uniqueSourceDeclarations, DeclarationNode newDeclaration)
		{
			string uniquePropertyKey = OptimizationVisitor.GetUniquePropertyKey(newDeclaration);
			if (uniqueSourceDeclarations.Contains(uniquePropertyKey))
			{
				DeclarationNode declarationNode = uniqueSourceDeclarations[uniquePropertyKey] as DeclarationNode;
				if (OptimizationVisitor.HasImportantFlag(declarationNode) && !OptimizationVisitor.HasImportantFlag(newDeclaration))
				{
					return;
				}
				uniqueSourceDeclarations.Remove(uniquePropertyKey);
			}
			uniqueSourceDeclarations.Add(uniquePropertyKey, newDeclaration);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0007C712 File Offset: 0x0007A912
		private static bool HasImportantFlag(DeclarationNode declarationNode)
		{
			return declarationNode.Prio.Equals("!important");
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0007C724 File Offset: 0x0007A924
		private static string GetUniquePropertyKey(DeclarationNode declarationNode)
		{
			string property = declarationNode.Property;
			string vendorPrefix = OptimizationVisitor.GetVendorPrefix(property);
			if (!string.IsNullOrWhiteSpace(vendorPrefix))
			{
				return property;
			}
			string stringBasedValue = declarationNode.ExprNode.TermNode.StringBasedValue;
			if (!string.IsNullOrWhiteSpace(stringBasedValue))
			{
				string vendorPrefix2 = OptimizationVisitor.GetVendorPrefix(stringBasedValue);
				if (!string.IsNullOrWhiteSpace(vendorPrefix2))
				{
					return vendorPrefix2 + property;
				}
			}
			return property;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0007C77C File Offset: 0x0007A97C
		private static string GetVendorPrefix(string stringBasedValue)
		{
			if (stringBasedValue.StartsWith("-", StringComparison.OrdinalIgnoreCase))
			{
				int num = stringBasedValue.IndexOf("-", 2, StringComparison.OrdinalIgnoreCase);
				if (num < stringBasedValue.Length - 1)
				{
					return stringBasedValue.Substring(0, num + 1);
				}
			}
			return null;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0007C7BC File Offset: 0x0007A9BC
		private static OrderedDictionary UniqueDeclarations(RulesetNode rulesetNode)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (DeclarationNode newDeclaration in rulesetNode.Declarations)
			{
				OptimizationVisitor.AddDeclaration(orderedDictionary, newDeclaration);
			}
			return orderedDictionary;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0007C810 File Offset: 0x0007AA10
		private static RulesetNode OptimizeRuleset(RulesetNode rulesetNode)
		{
			if (rulesetNode.Declarations.Count == 0)
			{
				return null;
			}
			OrderedDictionary orderedDictionary = OptimizationVisitor.UniqueDeclarations(rulesetNode);
			List<DeclarationNode> list = orderedDictionary.Values.Cast<DeclarationNode>().ToList<DeclarationNode>();
			return new RulesetNode(rulesetNode.SelectorsGroupNode, list.AsReadOnly(), rulesetNode.ImportantComments);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0007C878 File Offset: 0x0007AA78
		private OrderedDictionary GetMergedNodeDictionary(IEnumerable<StyleSheetRuleNode> styleSheetRuleNodes)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			OrderedDictionary rulesetHashKeysDictionary = new OrderedDictionary();
			foreach (StyleSheetRuleNode styleSheetRuleNode in styleSheetRuleNodes)
			{
				RulesetNode rulesetNode = styleSheetRuleNode as RulesetNode;
				if (rulesetNode != null && (this.NonMergeRuleSetSelectors == null || !this.NonMergeRuleSetSelectors.Any((string r) => r.Equals(rulesetNode.PrintSelector(), StringComparison.OrdinalIgnoreCase))))
				{
					OptimizationVisitor.OptimizeRulesetNode(rulesetNode, orderedDictionary, rulesetHashKeysDictionary, this.ShouldPreventOrderBasedConflict);
				}
				else
				{
					if (this.ShouldMergeMediaQueries)
					{
						MediaNode mediaNode = styleSheetRuleNode as MediaNode;
						if (mediaNode != null)
						{
							this.OptimizeMediaQuery(mediaNode, orderedDictionary);
							continue;
						}
					}
					string key = styleSheetRuleNode.MinifyPrint();
					if (!orderedDictionary.Contains(key))
					{
						orderedDictionary.Add(key, styleSheetRuleNode);
					}
					else
					{
						orderedDictionary[key] = styleSheetRuleNode;
					}
				}
			}
			if (this.ShouldMergeBasedOnCommonDeclarations)
			{
				foreach (StyleSheetRuleNode styleSheetRuleNode2 in styleSheetRuleNodes)
				{
					RulesetNode rulesetNode2 = styleSheetRuleNode2 as RulesetNode;
					if (rulesetNode2 != null)
					{
						OptimizationVisitor.MergeBasedOnCommonDeclarations(rulesetNode2, orderedDictionary);
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0007C9BC File Offset: 0x0007ABBC
		private void OptimizeMediaQuery(MediaNode mediaNode, OrderedDictionary ruleSetMediaPageDictionary)
		{
			string key = mediaNode.PrintSelector();
			List<PageNode> list = mediaNode.PageNodes.ToList<PageNode>();
			List<RulesetNode> list2 = mediaNode.Rulesets.ToList<RulesetNode>();
			if (ruleSetMediaPageDictionary.Contains(key))
			{
				MediaNode mediaNode2 = ruleSetMediaPageDictionary[key] as MediaNode;
				if (mediaNode2 != null)
				{
					list = mediaNode2.PageNodes.Concat(list).ToList<PageNode>();
					list2 = mediaNode2.Rulesets.Concat(list2).ToList<RulesetNode>();
				}
				ruleSetMediaPageDictionary.Remove(key);
			}
			ruleSetMediaPageDictionary.Add(key, new MediaNode(mediaNode.MediaQueries, this.GetMergedNodeDictionary(list2).Values.Cast<RulesetNode>().ToList<RulesetNode>().AsSafeReadOnly<RulesetNode>(), list.ToSafeReadOnlyCollection<PageNode>()));
		}
	}
}
