using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000264 RID: 612
	internal class ParseNode : IComparable
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00103E58 File Offset: 0x00102058
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x00103E60 File Offset: 0x00102060
		public int From
		{
			get
			{
				return this.m_vFrom;
			}
			set
			{
				this.m_vFrom = value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00103E6C File Offset: 0x0010206C
		public int To
		{
			get
			{
				return this.m_vTo;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00103E74 File Offset: 0x00102074
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x00103E7C File Offset: 0x0010207C
		public int PayloadIn
		{
			get
			{
				return this.m_vPayloadIn;
			}
			set
			{
				this.m_vPayloadIn = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x00103E88 File Offset: 0x00102088
		public ParseNode ParentNode
		{
			get
			{
				return this.m_vParentNode;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x00103E90 File Offset: 0x00102090
		public ParseNode NextNode
		{
			get
			{
				return this.m_vNextNode;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00103E98 File Offset: 0x00102098
		public ParseNode FirstChildNode
		{
			get
			{
				return this.m_vFirstChildNode;
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00103EA0 File Offset: 0x001020A0
		public ParseNode(int begin, int end, int sIn, int sOut)
		{
			this.m_vFrom = begin;
			this.m_vTo = end;
			this.m_vPayloadIn = sIn;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00103EC0 File Offset: 0x001020C0
		public void AddChild(ParseNode pn)
		{
			pn.m_vNextNode = this.m_vFirstChildNode;
			this.m_vFirstChildNode = pn;
			pn.m_vParentNode = this;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00103EDC File Offset: 0x001020DC
		public void AddUsedRule(int ruleIdx)
		{
			if (this.m_vRulesUsed == null)
			{
				this.m_vRulesUsed = new List<int>(1);
				this.m_vRulesUsed.Add(ruleIdx);
				return;
			}
			this.m_vRulesUsed.Insert(0, ruleIdx);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00103F0C File Offset: 0x0010210C
		public void AppendUsedRule(int ruleIdx)
		{
			if (this.m_vRulesUsed == null)
			{
				this.m_vRulesUsed = new List<int>(1);
			}
			this.m_vRulesUsed.Add(ruleIdx);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00103F30 File Offset: 0x00102130
		public int CompareTo(object obj)
		{
			ParseNode parseNode = (ParseNode)obj;
			if (parseNode == null)
			{
				return 1;
			}
			if (this.m_vFrom != parseNode.m_vFrom)
			{
				return this.m_vFrom - parseNode.m_vFrom;
			}
			return this.m_vTo - parseNode.m_vFrom;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00103F74 File Offset: 0x00102174
		public List<ParseNode> Children()
		{
			if (this.m_vChildren == null)
			{
				this.m_vChildren = new List<ParseNode>();
				for (ParseNode parseNode = this.m_vFirstChildNode; parseNode != null; parseNode = parseNode.m_vNextNode)
				{
					this.m_vChildren.Add(parseNode);
				}
			}
			return this.m_vChildren;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00103FBC File Offset: 0x001021BC
		public virtual string ToString(ParserGrammarDefinition gd)
		{
			return this.ToString(0, gd);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00103FC8 File Offset: 0x001021C8
		public virtual string ToString(int depth, ParserGrammarDefinition gd)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(4098);
			this.Append(0, gd, ref stringBuilder, ref num);
			return stringBuilder.ToString();
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00103FF8 File Offset: 0x001021F8
		protected bool Append(int depth, ParserGrammarDefinition gd, ref StringBuilder sb, ref int nodeCount)
		{
			this.Append(depth, gd, ref sb);
			if (++nodeCount == 1000)
			{
				sb.Append(string.Format("...\nThe limit of {0} nodes to be displayed has been reached!", 1000));
				return false;
			}
			depth++;
			foreach (ParseNode parseNode in this.Children())
			{
				if (!parseNode.Append(depth, gd, ref sb, ref nodeCount))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00104098 File Offset: 0x00102298
		protected void Append(int depth, ParserGrammarDefinition gd, ref StringBuilder sb)
		{
			sb.Append(' ', 2 * depth);
			sb.Append('[');
			sb.Append(this.m_vFrom);
			sb.Append(',');
			sb.Append(this.m_vTo);
			sb.Append(") ");
			sb.Append(" | ");
			sb.Append(gd.m_vAllSymbols[this.m_vPayloadIn]);
			if (gd != null && this.m_vRulesUsed != null)
			{
				foreach (int num in this.m_vRulesUsed)
				{
					sb.Append(" | ");
					sb.Append(gd.m_vRules[num].ToString());
				}
			}
			sb.Append("\n");
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0010418C File Offset: 0x0010238C
		public void PrintTree(ParserGrammarDefinition gd)
		{
			this.PrintTree(0, gd);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00104198 File Offset: 0x00102398
		public void PrintTree(int depth, ParserGrammarDefinition gd)
		{
			Console.Write(this.ToString(depth, gd));
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x001041A8 File Offset: 0x001023A8
		public string Content(List<LexerToken> src)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = this.m_vFrom; i < this.m_vTo; i++)
			{
				stringBuilder.Append(src[i].m_vContent);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001AF8 RID: 6904
		protected const int c_vMaxNodesToPrint = 1000;

		// Token: 0x04001AF9 RID: 6905
		public List<int> m_vRulesUsed;

		// Token: 0x04001AFA RID: 6906
		protected int m_vFrom;

		// Token: 0x04001AFB RID: 6907
		protected int m_vTo;

		// Token: 0x04001AFC RID: 6908
		protected int m_vPayloadIn;

		// Token: 0x04001AFD RID: 6909
		protected ParseNode m_vParentNode;

		// Token: 0x04001AFE RID: 6910
		protected ParseNode m_vNextNode;

		// Token: 0x04001AFF RID: 6911
		protected ParseNode m_vFirstChildNode;

		// Token: 0x04001B00 RID: 6912
		protected List<ParseNode> m_vChildren;
	}
}
