using System;
using System.Collections;

namespace MailBee.Html
{
	// Token: 0x0200000D RID: 13
	public class RuleSet : CollectionBase
	{
		// Token: 0x1700002F RID: 47
		public Rule this[int index]
		{
			get
			{
				return (Rule)base.List[index];
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005B52 File Offset: 0x00004B52
		public void AddTagProcessingCondition(string tagName, TagAttributeCollection tagAttrs)
		{
			base.List.Add(new Rule(TagRuleTypes.ProcessingCondition, tagName, tagAttrs));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005B68 File Offset: 0x00004B68
		public void AddTagProcessingRule(string tagName, TagAttributeCollection tagAttrs, TagAttributeCollection attrsToAdd, TagAttributeCollection attrsToRemove, bool replaceMode)
		{
			base.List.Add(new Rule(TagRuleTypes.ProcessingRule, tagName, tagAttrs, attrsToAdd, attrsToRemove, replaceMode));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005B83 File Offset: 0x00004B83
		public void AddTagRemovalRule(string tagName, TagAttributeCollection tagAttrs)
		{
			base.List.Add(new Rule(TagRuleTypes.RemovalRule, tagName, tagAttrs));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005B99 File Offset: 0x00004B99
		public void AddTagReplacementRule(string tagName, TagAttributeCollection tagAttrs, Element replacement)
		{
			base.List.Add(new Rule(TagRuleTypes.ReplacementRule, tagName, tagAttrs, replacement));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005BB0 File Offset: 0x00004BB0
		public void AddTagReplacementRule(string tagName, TagAttributeCollection tagAttrs, string replacement, bool replaceTagDefinitionOnly)
		{
			base.List.Add(new Rule(TagRuleTypes.ReplacementRule, tagName, tagAttrs, replacement, replaceTagDefinitionOnly));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005BCC File Offset: 0x00004BCC
		public static RuleSet GetSafeHtmlRules()
		{
			RuleSet ruleSet = new RuleSet();
			ruleSet.AddTagRemovalRule("script", null);
			ruleSet.AddTagRemovalRule("iframe", null);
			ruleSet.AddTagRemovalRule("bgsound", null);
			ruleSet.AddTagRemovalRule("embed", null);
			ruleSet.AddTagRemovalRule("frame", null);
			ruleSet.AddTagRemovalRule("frameset", null);
			ruleSet.AddTagRemovalRule("object", null);
			ruleSet.AddTagRemovalRule("applet", null);
			TagAttributeCollection tagAttributeCollection = new TagAttributeCollection();
			tagAttributeCollection.Add(new TagAttribute
			{
				Definition = "contenteditable"
			});
			ruleSet.AddTagProcessingRule(".*", tagAttributeCollection, null, tagAttributeCollection, false);
			tagAttributeCollection = new TagAttributeCollection();
			tagAttributeCollection.Add(new TagAttribute
			{
				Definition = "data.*"
			});
			ruleSet.AddTagProcessingRule(".*", tagAttributeCollection, null, tagAttributeCollection, false);
			tagAttributeCollection = new TagAttributeCollection();
			tagAttributeCollection.Add(new TagAttribute
			{
				Definition = "^on.*"
			});
			ruleSet.AddTagProcessingRule(".*", tagAttributeCollection, null, tagAttributeCollection, false);
			tagAttributeCollection = new TagAttributeCollection();
			tagAttributeCollection.Add(new TagAttribute
			{
				Name = "(low)?src",
				Value = "(javascript:.*)|(vbscript:.*)|(about:.*)"
			});
			ruleSet.AddTagProcessingRule("img", tagAttributeCollection, null, tagAttributeCollection, false);
			ruleSet.AddTagProcessingRule("input", tagAttributeCollection, null, tagAttributeCollection, false);
			tagAttributeCollection = new TagAttributeCollection();
			tagAttributeCollection.Add(new TagAttribute
			{
				Name = "href",
				Value = "(javascript:.*)|(vbscript:.*)|(about:.*)"
			});
			ruleSet.AddTagProcessingRule("a", tagAttributeCollection, null, tagAttributeCollection, false);
			return ruleSet;
		}
	}
}
