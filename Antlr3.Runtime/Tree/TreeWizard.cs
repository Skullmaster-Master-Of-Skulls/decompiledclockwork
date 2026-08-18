using System;
using System.Collections;
using System.Collections.Generic;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200005E RID: 94
	public class TreeWizard
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0000B5E3 File Offset: 0x000097E3
		public TreeWizard(ITreeAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000B5F2 File Offset: 0x000097F2
		public TreeWizard(ITreeAdaptor adaptor, IDictionary<string, int> tokenNameToTypeMap)
		{
			this.adaptor = adaptor;
			this.tokenNameToTypeMap = tokenNameToTypeMap;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000B608 File Offset: 0x00009808
		public TreeWizard(ITreeAdaptor adaptor, string[] tokenNames)
		{
			this.adaptor = adaptor;
			this.tokenNameToTypeMap = this.ComputeTokenTypes(tokenNames);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000B624 File Offset: 0x00009824
		public TreeWizard(string[] tokenNames) : this(new CommonTreeAdaptor(), tokenNames)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000B634 File Offset: 0x00009834
		public virtual IDictionary<string, int> ComputeTokenTypes(string[] tokenNames)
		{
			IDictionary<string, int> dictionary = new Dictionary<string, int>();
			if (tokenNames == null)
			{
				return dictionary;
			}
			for (int i = 4; i < tokenNames.Length; i++)
			{
				string key = tokenNames[i];
				dictionary[key] = i;
			}
			return dictionary;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000B668 File Offset: 0x00009868
		public virtual int GetTokenType(string tokenName)
		{
			if (this.tokenNameToTypeMap == null)
			{
				return 0;
			}
			int result;
			if (this.tokenNameToTypeMap.TryGetValue(tokenName, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000B694 File Offset: 0x00009894
		public IDictionary<int, IList> Index(object t)
		{
			IDictionary<int, IList> dictionary = new Dictionary<int, IList>();
			this.IndexCore(t, dictionary);
			return dictionary;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000B6B0 File Offset: 0x000098B0
		protected virtual void IndexCore(object t, IDictionary<int, IList> m)
		{
			if (t == null)
			{
				return;
			}
			int type = this.adaptor.GetType(t);
			IList list;
			if (!m.TryGetValue(type, out list) || list == null)
			{
				list = new List<object>();
				m[type] = list;
			}
			list.Add(t);
			int childCount = this.adaptor.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this.IndexCore(child, m);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000B724 File Offset: 0x00009924
		public virtual IList Find(object t, int ttype)
		{
			IList list = new List<object>();
			this.Visit(t, ttype, new TreeWizard.FindTreeWizardVisitor(list));
			return list;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000B748 File Offset: 0x00009948
		public virtual IList Find(object t, string pattern)
		{
			IList list = new List<object>();
			TreePatternLexer tokenizer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(tokenizer, this, new TreeWizard.TreePatternTreeAdaptor());
			TreeWizard.TreePattern treePattern = (TreeWizard.TreePattern)treePatternParser.Pattern();
			if (treePattern == null || treePattern.IsNil || treePattern.GetType() == typeof(TreeWizard.WildcardTreePattern))
			{
				return null;
			}
			int type = treePattern.Type;
			this.Visit(t, type, new TreeWizard.FindTreeWizardContextVisitor(this, treePattern, list));
			return list;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public virtual object FindFirst(object t, int ttype)
		{
			return null;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000B7B7 File Offset: 0x000099B7
		public virtual object FindFirst(object t, string pattern)
		{
			return null;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000B7BA File Offset: 0x000099BA
		public void Visit(object t, int ttype, TreeWizard.IContextVisitor visitor)
		{
			this.VisitCore(t, null, 0, ttype, visitor);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000B7C7 File Offset: 0x000099C7
		public void Visit(object t, int ttype, Action<object> action)
		{
			this.Visit(t, ttype, new TreeWizard.ActionVisitor(action));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000B7D8 File Offset: 0x000099D8
		protected virtual void VisitCore(object t, object parent, int childIndex, int ttype, TreeWizard.IContextVisitor visitor)
		{
			if (t == null)
			{
				return;
			}
			if (this.adaptor.GetType(t) == ttype)
			{
				visitor.Visit(t, parent, childIndex, null);
			}
			int childCount = this.adaptor.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this.VisitCore(child, t, i, ttype, visitor);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000B838 File Offset: 0x00009A38
		public void Visit(object t, string pattern, TreeWizard.IContextVisitor visitor)
		{
			TreePatternLexer tokenizer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(tokenizer, this, new TreeWizard.TreePatternTreeAdaptor());
			TreeWizard.TreePattern treePattern = (TreeWizard.TreePattern)treePatternParser.Pattern();
			if (treePattern == null || treePattern.IsNil || treePattern.GetType() == typeof(TreeWizard.WildcardTreePattern))
			{
				return;
			}
			IDictionary<string, object> labels = new Dictionary<string, object>();
			int type = treePattern.Type;
			this.Visit(t, type, new TreeWizard.VisitTreeWizardContextVisitor(this, visitor, labels, treePattern));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public bool Parse(object t, string pattern, IDictionary<string, object> labels)
		{
			TreePatternLexer tokenizer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(tokenizer, this, new TreeWizard.TreePatternTreeAdaptor());
			TreeWizard.TreePattern tpattern = (TreeWizard.TreePattern)treePatternParser.Pattern();
			return this.ParseCore(t, tpattern, labels);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public bool Parse(object t, string pattern)
		{
			return this.Parse(t, pattern, null);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		protected virtual bool ParseCore(object t1, TreeWizard.TreePattern tpattern, IDictionary<string, object> labels)
		{
			if (t1 == null || tpattern == null)
			{
				return false;
			}
			if (tpattern.GetType() != typeof(TreeWizard.WildcardTreePattern))
			{
				if (this.adaptor.GetType(t1) != tpattern.Type)
				{
					return false;
				}
				if (tpattern.hasTextArg && !this.adaptor.GetText(t1).Equals(tpattern.Text))
				{
					return false;
				}
			}
			if (tpattern.label != null && labels != null)
			{
				labels[tpattern.label] = t1;
			}
			int childCount = this.adaptor.GetChildCount(t1);
			int childCount2 = tpattern.ChildCount;
			if (childCount != childCount2)
			{
				return false;
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t1, i);
				TreeWizard.TreePattern tpattern2 = (TreeWizard.TreePattern)tpattern.GetChild(i);
				if (!this.ParseCore(child, tpattern2, labels))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public virtual object Create(string pattern)
		{
			TreePatternLexer tokenizer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(tokenizer, this, this.adaptor);
			return treePatternParser.Pattern();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000B9DE File Offset: 0x00009BDE
		public static bool Equals(object t1, object t2, ITreeAdaptor adaptor)
		{
			return TreeWizard.EqualsCore(t1, t2, adaptor);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public bool Equals(object t1, object t2)
		{
			return TreeWizard.EqualsCore(t1, t2, this.adaptor);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		protected static bool EqualsCore(object t1, object t2, ITreeAdaptor adaptor)
		{
			if (t1 == null || t2 == null)
			{
				return false;
			}
			if (adaptor.GetType(t1) != adaptor.GetType(t2))
			{
				return false;
			}
			if (!adaptor.GetText(t1).Equals(adaptor.GetText(t2)))
			{
				return false;
			}
			int childCount = adaptor.GetChildCount(t1);
			int childCount2 = adaptor.GetChildCount(t2);
			if (childCount != childCount2)
			{
				return false;
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = adaptor.GetChild(t1, i);
				object child2 = adaptor.GetChild(t2, i);
				if (!TreeWizard.EqualsCore(child, child2, adaptor))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000F3 RID: 243
		protected ITreeAdaptor adaptor;

		// Token: 0x040000F4 RID: 244
		protected IDictionary<string, int> tokenNameToTypeMap;

		// Token: 0x0200005F RID: 95
		public interface IContextVisitor
		{
			// Token: 0x0600043A RID: 1082
			void Visit(object t, object parent, int childIndex, IDictionary<string, object> labels);
		}

		// Token: 0x02000060 RID: 96
		public abstract class Visitor : TreeWizard.IContextVisitor
		{
			// Token: 0x0600043B RID: 1083 RVA: 0x0000BA79 File Offset: 0x00009C79
			public virtual void Visit(object t, object parent, int childIndex, IDictionary<string, object> labels)
			{
				this.Visit(t);
			}

			// Token: 0x0600043C RID: 1084
			public abstract void Visit(object t);
		}

		// Token: 0x02000061 RID: 97
		private class ActionVisitor : TreeWizard.Visitor
		{
			// Token: 0x0600043E RID: 1086 RVA: 0x0000BA8A File Offset: 0x00009C8A
			public ActionVisitor(Action<object> action)
			{
				this._action = action;
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x0000BA99 File Offset: 0x00009C99
			public override void Visit(object t)
			{
				this._action(t);
			}

			// Token: 0x040000F5 RID: 245
			private Action<object> _action;
		}

		// Token: 0x02000062 RID: 98
		public class TreePattern : CommonTree
		{
			// Token: 0x06000440 RID: 1088 RVA: 0x0000BAA7 File Offset: 0x00009CA7
			public TreePattern(IToken payload) : base(payload)
			{
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x0000BAB0 File Offset: 0x00009CB0
			public override string ToString()
			{
				if (this.label != null)
				{
					return "%" + this.label + ":";
				}
				return base.ToString();
			}

			// Token: 0x040000F6 RID: 246
			public string label;

			// Token: 0x040000F7 RID: 247
			public bool hasTextArg;
		}

		// Token: 0x02000063 RID: 99
		public class WildcardTreePattern : TreeWizard.TreePattern
		{
			// Token: 0x06000442 RID: 1090 RVA: 0x0000BAD6 File Offset: 0x00009CD6
			public WildcardTreePattern(IToken payload) : base(payload)
			{
			}
		}

		// Token: 0x02000064 RID: 100
		public class TreePatternTreeAdaptor : CommonTreeAdaptor
		{
			// Token: 0x06000443 RID: 1091 RVA: 0x0000BADF File Offset: 0x00009CDF
			public override object Create(IToken payload)
			{
				return new TreeWizard.TreePattern(payload);
			}
		}

		// Token: 0x02000065 RID: 101
		private class FindTreeWizardVisitor : TreeWizard.Visitor
		{
			// Token: 0x06000445 RID: 1093 RVA: 0x0000BAEF File Offset: 0x00009CEF
			public FindTreeWizardVisitor(IList nodes)
			{
				this._nodes = nodes;
			}

			// Token: 0x06000446 RID: 1094 RVA: 0x0000BAFE File Offset: 0x00009CFE
			public override void Visit(object t)
			{
				this._nodes.Add(t);
			}

			// Token: 0x040000F8 RID: 248
			private IList _nodes;
		}

		// Token: 0x02000066 RID: 102
		private class FindTreeWizardContextVisitor : TreeWizard.IContextVisitor
		{
			// Token: 0x06000447 RID: 1095 RVA: 0x0000BB0D File Offset: 0x00009D0D
			public FindTreeWizardContextVisitor(TreeWizard outer, TreeWizard.TreePattern tpattern, IList subtrees)
			{
				this._outer = outer;
				this._tpattern = tpattern;
				this._subtrees = subtrees;
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x0000BB2A File Offset: 0x00009D2A
			public void Visit(object t, object parent, int childIndex, IDictionary<string, object> labels)
			{
				if (this._outer.ParseCore(t, this._tpattern, null))
				{
					this._subtrees.Add(t);
				}
			}

			// Token: 0x040000F9 RID: 249
			private TreeWizard _outer;

			// Token: 0x040000FA RID: 250
			private TreeWizard.TreePattern _tpattern;

			// Token: 0x040000FB RID: 251
			private IList _subtrees;
		}

		// Token: 0x02000067 RID: 103
		private class VisitTreeWizardContextVisitor : TreeWizard.IContextVisitor
		{
			// Token: 0x06000449 RID: 1097 RVA: 0x0000BB4E File Offset: 0x00009D4E
			public VisitTreeWizardContextVisitor(TreeWizard outer, TreeWizard.IContextVisitor visitor, IDictionary<string, object> labels, TreeWizard.TreePattern tpattern)
			{
				this._outer = outer;
				this._visitor = visitor;
				this._labels = labels;
				this._tpattern = tpattern;
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x0000BB73 File Offset: 0x00009D73
			public void Visit(object t, object parent, int childIndex, IDictionary<string, object> unusedlabels)
			{
				this._labels.Clear();
				if (this._outer.ParseCore(t, this._tpattern, this._labels))
				{
					this._visitor.Visit(t, parent, childIndex, this._labels);
				}
			}

			// Token: 0x040000FC RID: 252
			private TreeWizard _outer;

			// Token: 0x040000FD RID: 253
			private TreeWizard.IContextVisitor _visitor;

			// Token: 0x040000FE RID: 254
			private IDictionary<string, object> _labels;

			// Token: 0x040000FF RID: 255
			private TreeWizard.TreePattern _tpattern;
		}
	}
}
