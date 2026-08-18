using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200004B RID: 75
	public class DotTreeGenerator
	{
		// Token: 0x0600039D RID: 925 RVA: 0x000098F8 File Offset: 0x00007AF8
		public virtual string ToDot(object tree, ITreeAdaptor adaptor)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in this.HeaderLines)
			{
				stringBuilder.AppendLine(value);
			}
			this.nodeNumber = 0;
			IEnumerable<string> enumerable = this.DefineNodes(tree, adaptor);
			this.nodeNumber = 0;
			IEnumerable<string> enumerable2 = this.DefineEdges(tree, adaptor);
			foreach (string value2 in enumerable)
			{
				stringBuilder.AppendLine(value2);
			}
			stringBuilder.AppendLine();
			foreach (string value3 in enumerable2)
			{
				stringBuilder.AppendLine(value3);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000099F8 File Offset: 0x00007BF8
		public virtual string ToDot(ITree tree)
		{
			return this.ToDot(tree, new CommonTreeAdaptor());
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009CAC File Offset: 0x00007EAC
		protected virtual IEnumerable<string> DefineNodes(object tree, ITreeAdaptor adaptor)
		{
			if (tree != null)
			{
				int i = adaptor.GetChildCount(tree);
				if (i != 0)
				{
					yield return this.GetNodeText(adaptor, tree);
					for (int j = 0; j < i; j++)
					{
						object child = adaptor.GetChild(tree, j);
						yield return this.GetNodeText(adaptor, child);
						foreach (string t in this.DefineNodes(child, adaptor))
						{
							yield return t;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00009FF8 File Offset: 0x000081F8
		protected virtual IEnumerable<string> DefineEdges(object tree, ITreeAdaptor adaptor)
		{
			if (tree != null)
			{
				int i = adaptor.GetChildCount(tree);
				if (i != 0)
				{
					string parentName = "n" + this.GetNodeNumber(tree);
					string parentText = adaptor.GetText(tree);
					for (int j = 0; j < i; j++)
					{
						object child = adaptor.GetChild(tree, j);
						string childText = adaptor.GetText(child);
						string childName = "n" + this.GetNodeNumber(child);
						yield return string.Format("  {0} -> {1} // \"{2}\" -> \"{3}\"", new object[]
						{
							parentName,
							childName,
							this.FixString(parentText),
							this.FixString(childText)
						});
						foreach (string t in this.DefineEdges(child, adaptor))
						{
							yield return t;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A024 File Offset: 0x00008224
		protected virtual string GetNodeText(ITreeAdaptor adaptor, object t)
		{
			string text = adaptor.GetText(t);
			string arg = "n" + this.GetNodeNumber(t);
			return string.Format("  {0} [label=\"{1}\"];", arg, this.FixString(text));
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A064 File Offset: 0x00008264
		protected virtual int GetNodeNumber(object t)
		{
			int result;
			if (this.nodeToNumberMap.TryGetValue(t, out result))
			{
				return result;
			}
			this.nodeToNumberMap[t] = this.nodeNumber;
			this.nodeNumber++;
			return this.nodeNumber - 1;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A0AC File Offset: 0x000082AC
		protected virtual string FixString(string text)
		{
			if (text != null)
			{
				text = Regex.Replace(text, "\"", "\\\\\"");
				text = Regex.Replace(text, "\\t", "    ");
				text = Regex.Replace(text, "\\n", "\\\\n");
				text = Regex.Replace(text, "\\r", "\\\\r");
				if (text.Length > 20)
				{
					text = text.Substring(0, 8) + "..." + text.Substring(text.Length - 8);
				}
			}
			return text;
		}

		// Token: 0x040000B7 RID: 183
		private const string Footer = "}";

		// Token: 0x040000B8 RID: 184
		private const string NodeFormat = "  {0} [label=\"{1}\"];";

		// Token: 0x040000B9 RID: 185
		private const string EdgeFormat = "  {0} -> {1} // \"{2}\" -> \"{3}\"";

		// Token: 0x040000BA RID: 186
		private readonly string[] HeaderLines = new string[]
		{
			"digraph {",
			"",
			"\tordering=out;",
			"\tranksep=.4;",
			"\tbgcolor=\"lightgrey\"; node [shape=box, fixedsize=false, fontsize=12, fontname=\"Helvetica-bold\", fontcolor=\"blue\"",
			"\t\twidth=.25, height=.25, color=\"black\", fillcolor=\"white\", style=\"filled, solid, bold\"];",
			"\tedge [arrowsize=.5, color=\"black\", style=\"bold\"]",
			""
		};

		// Token: 0x040000BB RID: 187
		private Dictionary<object, int> nodeToNumberMap = new Dictionary<object, int>();

		// Token: 0x040000BC RID: 188
		private int nodeNumber;
	}
}
