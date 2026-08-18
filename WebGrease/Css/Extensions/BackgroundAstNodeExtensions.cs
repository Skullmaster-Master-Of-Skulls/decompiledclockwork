using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Css.Ast;
using WebGrease.Css.ImageAssemblyAnalysis;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;
using WebGrease.Css.ImageAssemblyAnalysis.PropertyModel;

namespace WebGrease.Css.Extensions
{
	// Token: 0x02000185 RID: 389
	public static class BackgroundAstNodeExtensions
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x00077354 File Offset: 0x00075554
		internal static bool TryGetBackgroundDeclaration(this IEnumerable<DeclarationNode> declarationAstNodes, AstNode parentAstNode, out Background backgroundNode, out BackgroundImage backgroundImageNode, out BackgroundPosition backgroundPositionNode, out DeclarationNode backgroundSize, List<string> imageReferencesInInvalidDeclarations, HashSet<string> imageReferencesToIgnore, ImageAssemblyAnalysisLog imageAssemblyAnalysisLog, string outputUnit, double outputUnitFactor, bool ignoreImagesWithNonDefaultBackgroundSize = false)
		{
			backgroundNode = null;
			backgroundImageNode = null;
			backgroundPositionNode = null;
			backgroundSize = null;
			if (BackgroundImage.HasMultipleUrls(parentAstNode.MinifyPrint()))
			{
				imageAssemblyAnalysisLog.SafeAdd(parentAstNode, null, new FailureReason?(FailureReason.MultipleUrls));
				return false;
			}
			DeclarationNode declarationNode = declarationAstNodes.FirstOrDefault((DeclarationNode d) => d.Property == "-wg-spriting");
			if (declarationNode != null && declarationNode.ExprNode.TermNode.StringBasedValue == "ignore")
			{
				imageAssemblyAnalysisLog.SafeAdd(parentAstNode, null, new FailureReason?(FailureReason.SpritingIgnore));
				return false;
			}
			Dictionary<string, DeclarationNode> dictionary = declarationAstNodes.LoadDeclarationPropertiesDictionary();
			DeclarationNode declarationNode2;
			if (dictionary.TryGetValue("background", out declarationNode2))
			{
				if (dictionary.ContainsKey("background-repeat") || dictionary.ContainsKey("background-image") || dictionary.ContainsKey("background-position"))
				{
					throw new ImageAssembleException(CssStrings.DuplicateBackgroundFormatError);
				}
				Background background = new Background(declarationNode2, outputUnit, outputUnitFactor);
				bool flag;
				if (!background.BackgroundImage.VerifyBackgroundUrl(parentAstNode, imageReferencesToIgnore, imageAssemblyAnalysisLog, out flag) || flag)
				{
					return false;
				}
				if (!background.BackgroundRepeat.VerifyBackgroundNoRepeat())
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, background.Url, new FailureReason?(FailureReason.BackgroundRepeatInvalid));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(background.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				if (!background.BackgroundPosition.IsVerticalSpriteCandidate())
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, background.Url, new FailureReason?(FailureReason.IncorrectPosition));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(background.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				if (!BackgroundAstNodeExtensions.TryGetBackgroundSize(ignoreImagesWithNonDefaultBackgroundSize, dictionary, out backgroundSize))
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, background.Url, new FailureReason?(FailureReason.BackgroundSizeIsSetToNonDefaultValue));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(background.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				backgroundNode = background;
				imageAssemblyAnalysisLog.SafeAdd(parentAstNode, background.Url, null);
				return true;
			}
			else
			{
				if (!dictionary.TryGetValue("background-image", out declarationNode2))
				{
					return false;
				}
				BackgroundImage backgroundImage = new BackgroundImage(declarationNode2);
				bool flag2;
				if (!backgroundImage.VerifyBackgroundUrl(parentAstNode, imageReferencesToIgnore, imageAssemblyAnalysisLog, out flag2) || flag2)
				{
					return false;
				}
				DeclarationNode declarationNode3;
				if (!dictionary.TryGetValue("background-repeat", out declarationNode3))
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, backgroundImage.Url, new FailureReason?(FailureReason.NoRepeat));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(backgroundImage.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				if (!new BackgroundRepeat(declarationNode3).VerifyBackgroundNoRepeat())
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, backgroundImage.Url, new FailureReason?(FailureReason.BackgroundRepeatInvalid));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(backgroundImage.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				if (!BackgroundAstNodeExtensions.TryGetBackgroundSize(ignoreImagesWithNonDefaultBackgroundSize, dictionary, out backgroundSize))
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, backgroundImage.Url, new FailureReason?(FailureReason.BackgroundSizeIsSetToNonDefaultValue));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(backgroundImage.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				DeclarationNode declarationNode4;
				if (!dictionary.TryGetValue("background-position", out declarationNode4))
				{
					backgroundImageNode = backgroundImage;
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, backgroundImageNode.Url, null);
					return true;
				}
				BackgroundPosition backgroundPosition = new BackgroundPosition(declarationNode4, outputUnit, outputUnitFactor);
				if (!backgroundPosition.IsVerticalSpriteCandidate())
				{
					imageAssemblyAnalysisLog.SafeAdd(parentAstNode, backgroundImage.Url, new FailureReason?(FailureReason.IncorrectPosition));
					BackgroundAstNodeExtensions.UpdateFailedUrlsList(backgroundImage.Url, imageReferencesInInvalidDeclarations);
					return false;
				}
				backgroundImageNode = backgroundImage;
				backgroundPositionNode = backgroundPosition;
				imageAssemblyAnalysisLog.SafeAdd(parentAstNode, backgroundImage.Url, null);
				return true;
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00077654 File Offset: 0x00075854
		internal static void SafeAdd(this ImageAssemblyAnalysisLog imageAssemblyAnalysisLog, AstNode parentAstNode, string image = null, FailureReason? failureReason = null)
		{
			if (imageAssemblyAnalysisLog != null)
			{
				imageAssemblyAnalysisLog.Add(new ImageAssemblyAnalysis
				{
					AstNode = parentAstNode,
					Image = image,
					FailureReason = failureReason
				});
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00077864 File Offset: 0x00075A64
		internal static IEnumerable<TermWithOperatorNode> DeclarationEnumerator(this DeclarationNode declarationNode)
		{
			if (declarationNode != null)
			{
				yield return new TermWithOperatorNode(" ", declarationNode.ExprNode.TermNode);
				foreach (TermWithOperatorNode termWithOperatorNode in declarationNode.ExprNode.TermsWithOperators)
				{
					yield return termWithOperatorNode;
				}
			}
			yield break;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00077881 File Offset: 0x00075A81
		internal static TermNode CopyTerm(this TermNode termNode)
		{
			if (termNode != null)
			{
				return new TermNode(termNode.UnaryOperator, termNode.NumberBasedValue, termNode.StringBasedValue, termNode.Hexcolor, termNode.FunctionNode, termNode.ImportantComments, null);
			}
			return null;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000778B4 File Offset: 0x00075AB4
		internal static DeclarationNode CreateDeclarationNode(this DeclarationNode declarationNode, List<TermWithOperatorNode> termWithOperatorNodes)
		{
			if (declarationNode == null || termWithOperatorNodes == null || termWithOperatorNodes.Count <= 0)
			{
				return declarationNode;
			}
			TermNode termNode = termWithOperatorNodes[0].TermNode;
			termWithOperatorNodes.RemoveAt(0);
			return new DeclarationNode(declarationNode.Property, new ExprNode(termNode, termWithOperatorNodes.AsReadOnly(), null), declarationNode.Prio, declarationNode.ImportantComments);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0007790C File Offset: 0x00075B0C
		private static bool TryGetBackgroundSize(bool ignoreImagesWithNonDefaultBackgroundSize, IDictionary<string, DeclarationNode> declarationProperties, out DeclarationNode backgroundSize)
		{
			if (declarationProperties.TryGetValue("background-size", out backgroundSize) && ignoreImagesWithNonDefaultBackgroundSize)
			{
				string text = backgroundSize.ExprNode.MinifyPrint();
				if (!text.Equals("auto") && !text.Equals("auto auto"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00077954 File Offset: 0x00075B54
		private static void UpdateFailedUrlsList(string parsedUrl, ICollection<string> imagesCriteriaFailedUrls)
		{
			if (imagesCriteriaFailedUrls != null && !string.IsNullOrWhiteSpace(parsedUrl))
			{
				imagesCriteriaFailedUrls.Add(parsedUrl);
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000779C8 File Offset: 0x00075BC8
		private static Dictionary<string, DeclarationNode> LoadDeclarationPropertiesDictionary(this IEnumerable<DeclarationNode> declarationNodes)
		{
			Dictionary<string, List<DeclarationNode>> declarationPropertyNames = new Dictionary<string, List<DeclarationNode>>(StringComparer.OrdinalIgnoreCase);
			declarationNodes.ForEach(delegate(DeclarationNode declarationNode)
			{
				string property = declarationNode.Property;
				List<DeclarationNode> list;
				if (!declarationPropertyNames.TryGetValue(property, out list))
				{
					list = (declarationPropertyNames[property] = new List<DeclarationNode>());
				}
				list.Add(declarationNode);
			});
			return declarationPropertyNames.ToDictionary((KeyValuePair<string, List<DeclarationNode>> d) => d.Key, (KeyValuePair<string, List<DeclarationNode>> d) => d.Value.LastOrDefault<DeclarationNode>());
		}
	}
}
