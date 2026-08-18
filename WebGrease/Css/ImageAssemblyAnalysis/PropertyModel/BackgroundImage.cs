using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;
using WebGrease.Extensions;

namespace WebGrease.Css.ImageAssemblyAnalysis.PropertyModel
{
	// Token: 0x02000194 RID: 404
	internal sealed class BackgroundImage
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x00079290 File Offset: 0x00077490
		internal BackgroundImage()
		{
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00079298 File Offset: 0x00077498
		internal BackgroundImage(DeclarationNode declarationNode)
		{
			if (declarationNode == null)
			{
				throw new ArgumentNullException("declarationNode");
			}
			this.DeclarationNode = declarationNode;
			ExprNode exprNode = declarationNode.ExprNode;
			this.ParseTerm(exprNode.TermNode);
			exprNode.TermsWithOperators.ForEach(new Action<TermWithOperatorNode>(this.ParseTermWithOperator));
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000792EA File Offset: 0x000774EA
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x000792F2 File Offset: 0x000774F2
		public DeclarationNode DeclarationNode { get; private set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000792FB File Offset: 0x000774FB
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x00079303 File Offset: 0x00077503
		internal TermNode UrlTermNode { get; private set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0007930C File Offset: 0x0007750C
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00079314 File Offset: 0x00077514
		internal string Url { get; private set; }

		// Token: 0x060014DA RID: 5338 RVA: 0x0007931D File Offset: 0x0007751D
		internal static bool HasMultipleUrls(string text)
		{
			return !string.IsNullOrWhiteSpace(text) && BackgroundImage.MultipleUrlsRegex.Matches(text).Count > 1 && text.IndexOf("background", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00079350 File Offset: 0x00077550
		internal static bool TryGetUrl(TermNode termNode, out string url)
		{
			if (termNode != null && !string.IsNullOrWhiteSpace(termNode.StringBasedValue))
			{
				string stringBasedValue = termNode.StringBasedValue;
				Match match = BackgroundImage.UrlRegex.Match(stringBasedValue);
				if (match.Success && match.Groups.Count > 2)
				{
					string value;
					url = (value = match.Groups[1].Value);
					if (!string.IsNullOrWhiteSpace(value))
					{
						return true;
					}
				}
			}
			url = null;
			return false;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x000793BC File Offset: 0x000775BC
		internal bool VerifyBackgroundUrl(AstNode parent, HashSet<string> imageReferencesToIgnore, ImageAssemblyAnalysisLog imageAssemblyAnalysisLog, out bool shouldIgnore)
		{
			shouldIgnore = false;
			if (string.IsNullOrWhiteSpace(this.Url))
			{
				imageAssemblyAnalysisLog.SafeAdd(parent, this.Url, new FailureReason?(FailureReason.NoUrl));
				return false;
			}
			if (imageReferencesToIgnore != null)
			{
				string url = this.Url;
				string item = url.NormalizeUrl();
				if (imageReferencesToIgnore.Contains(item))
				{
					imageAssemblyAnalysisLog.SafeAdd(parent, this.Url, new FailureReason?(FailureReason.IgnoreUrl));
					shouldIgnore = true;
					return false;
				}
			}
			return true;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00079424 File Offset: 0x00077624
		internal void ParseTerm(TermNode termNode)
		{
			if (termNode == null)
			{
				return;
			}
			string url;
			if (!BackgroundImage.TryGetUrl(termNode, out url))
			{
				return;
			}
			this.UrlTermNode = termNode;
			this.Url = url;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0007944E File Offset: 0x0007764E
		internal void ParseTermWithOperator(TermWithOperatorNode termWithOperatorNode)
		{
			if (termWithOperatorNode == null)
			{
				return;
			}
			this.ParseTerm(termWithOperatorNode.TermNode);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00079460 File Offset: 0x00077660
		internal bool UpdateTermForUrl(TermNode originalTermNode, out TermNode updatedTermNode, string updatedUrl)
		{
			if (originalTermNode == this.UrlTermNode)
			{
				updatedUrl = string.Format(CultureInfo.CurrentUICulture, "url({0})", new object[]
				{
					updatedUrl
				});
				updatedTermNode = new TermNode(originalTermNode.UnaryOperator, originalTermNode.NumberBasedValue, updatedUrl, originalTermNode.Hexcolor, originalTermNode.FunctionNode, originalTermNode.ImportantComments, null);
				return true;
			}
			updatedTermNode = originalTermNode;
			return false;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000794C0 File Offset: 0x000776C0
		internal DeclarationNode UpdateBackgroundImageNode(string updatedUrl)
		{
			if (this.DeclarationNode == null)
			{
				return null;
			}
			ExprNode exprNode = this.DeclarationNode.ExprNode;
			TermNode termNode = exprNode.TermNode;
			TermNode termNode2;
			if (this.UpdateTermForUrl(termNode, out termNode2, updatedUrl))
			{
				return new DeclarationNode(this.DeclarationNode.Property, new ExprNode(termNode2, exprNode.TermsWithOperators, exprNode.ImportantComments), this.DeclarationNode.Prio, this.DeclarationNode.ImportantComments);
			}
			return this.DeclarationNode;
		}

		// Token: 0x04000B2E RID: 2862
		internal static readonly string UrlRegEx = "url\\((?<quote>[\"']?)\\s*((hash\\(.*?\\))|(%?([-./\\w_]+)(\\:\\w*)?%?))\\s*\\k<quote>\\)";

		// Token: 0x04000B2F RID: 2863
		private static readonly Regex MultipleUrlsRegex = new Regex(BackgroundImage.UrlRegEx, RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000B30 RID: 2864
		private static readonly Regex UrlRegex = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}$", new object[]
		{
			BackgroundImage.UrlRegEx
		}), RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
