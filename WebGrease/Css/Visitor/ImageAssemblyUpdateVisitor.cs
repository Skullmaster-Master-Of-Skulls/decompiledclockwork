using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Extensions;
using WebGrease.Css.ImageAssemblyAnalysis;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;
using WebGrease.Css.ImageAssemblyAnalysis.PropertyModel;
using WebGrease.Extensions;
using WebGrease.ImageAssemble;

namespace WebGrease.Css.Visitor
{
	// Token: 0x020001A0 RID: 416
	public class ImageAssemblyUpdateVisitor : NodeVisitor
	{
		// Token: 0x0600154A RID: 5450 RVA: 0x0007B5E0 File Offset: 0x000797E0
		internal ImageAssemblyUpdateVisitor(string cssPath, IEnumerable<ImageLog> imageLogs, float dpi = 1f, string outputUnit = "px", double outputUnitFactor = 1.0, string destinationDirectory = null, string prependToDestination = null, IDictionary<string, string> availableSourceImages = null, string missingImage = null)
		{
			this.outputUnit = outputUnit;
			this.outputUnitFactor = outputUnitFactor;
			this.destinationDirectory = destinationDirectory;
			this.prependToDestination = prependToDestination;
			this.availableSourceImages = availableSourceImages;
			this.missingImage = missingImage;
			this.dpi = dpi;
			this.cssPath = cssPath.GetFullPathWithLowercase();
			try
			{
				this.inputImages = imageLogs.SelectMany((ImageLog i) => i.InputImages);
			}
			catch (Exception innerException)
			{
				throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.InnerExceptionFile, new object[]
				{
					string.Join<ImageLog>(','.ToString(CultureInfo.InvariantCulture), imageLogs)
				}), innerException);
			}
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0007B6D0 File Offset: 0x000798D0
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			List<StyleSheetRuleNode> updatedStyleSheetRuleNodes = new List<StyleSheetRuleNode>();
			styleSheet.StyleSheetRules.ForEach(delegate(StyleSheetRuleNode styleSheetRuleNode)
			{
				updatedStyleSheetRuleNodes.Add((StyleSheetRuleNode)styleSheetRuleNode.Accept(this));
			});
			return new StyleSheetNode(styleSheet.CharSetString, styleSheet.Imports, styleSheet.Namespaces, updatedStyleSheetRuleNodes.AsReadOnly());
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0007B72E File Offset: 0x0007992E
		public override AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			return new RulesetNode(rulesetNode.SelectorsGroupNode, this.UpdateDeclarations(rulesetNode.Declarations, rulesetNode), rulesetNode.ImportantComments);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0007B794 File Offset: 0x00079994
		public override AstNode VisitMediaNode(MediaNode mediaNode)
		{
			List<RulesetNode> updatedRulesets = new List<RulesetNode>();
			List<PageNode> updatedPageNodes = new List<PageNode>();
			mediaNode.Rulesets.ForEach(delegate(RulesetNode rulesetNode)
			{
				updatedRulesets.Add((RulesetNode)rulesetNode.Accept(this));
			});
			mediaNode.PageNodes.ForEach(delegate(PageNode pageNode)
			{
				updatedPageNodes.Add((PageNode)pageNode.Accept(this));
			});
			return new MediaNode(mediaNode.MediaQueries, updatedRulesets.AsReadOnly(), updatedPageNodes.AsReadOnly());
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0007B813 File Offset: 0x00079A13
		public override AstNode VisitPageNode(PageNode pageNode)
		{
			return new PageNode(pageNode.PseudoPage, this.UpdateDeclarations(pageNode.Declarations, pageNode));
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0007B82D File Offset: 0x00079A2D
		private static void UpdateDeclarations(IList<DeclarationNode> declarationNodes, DeclarationNode originalDeclarationNode, DeclarationNode updatedDeclarationNode)
		{
			declarationNodes[declarationNodes.IndexOf(originalDeclarationNode)] = updatedDeclarationNode;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0007B840 File Offset: 0x00079A40
		private static string GetPositionString(float? value, Source? source)
		{
			if (source != null)
			{
				switch (source.Value)
				{
				case Source.Left:
					return "left";
				case Source.Right:
					return "right";
				case Source.Center:
					return "center";
				case Source.Top:
					return "top";
				case Source.Bottom:
					return "bottom";
				case Source.Px:
					return string.Format(CultureInfo.InvariantCulture, "{0}px", new object[]
					{
						value.GetValueOrDefault()
					});
				case Source.Percentage:
					return string.Format(CultureInfo.InvariantCulture, "{0}%", new object[]
					{
						value.GetValueOrDefault()
					});
				case Source.NoUnits:
					if (value != null)
					{
						return value.Value.ToString(CultureInfo.InvariantCulture);
					}
					return string.Empty;
				case Source.Rem:
					return string.Format(CultureInfo.InvariantCulture, "{0}rem", new object[]
					{
						value.GetValueOrDefault()
					});
				case Source.Em:
					return string.Format(CultureInfo.InvariantCulture, "{0}em", new object[]
					{
						value.GetValueOrDefault()
					});
				}
				return ((value != null) ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty) + source.Value.ToString();
			}
			if (value != null)
			{
				return value.Value.ToString(CultureInfo.InvariantCulture);
			}
			return "center";
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0007B9DC File Offset: 0x00079BDC
		private static DeclarationNode CreateDebugOriginalPositionComment(float? xPosition, Source? xSource, float? yPosition, Source? ySource)
		{
			bool flag = xPosition != null || xSource != null;
			bool flag2 = yPosition != null || ySource != null;
			if (!flag && !flag2)
			{
				return ImageAssemblyUpdateVisitor.CreateDebugDeclarationComment("-wg-original-position", "0 0");
			}
			return ImageAssemblyUpdateVisitor.CreateDebugDeclarationComment("-wg-original-position", ImageAssemblyUpdateVisitor.GetPositionString(xPosition, xSource) + " " + ImageAssemblyUpdateVisitor.GetPositionString(yPosition, ySource));
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0007BA4C File Offset: 0x00079C4C
		private static DeclarationNode CreateDebugSpritePositionComment(int? xPixels, int? yPixels)
		{
			return ImageAssemblyUpdateVisitor.CreateDebugDeclarationComment("-wg-sprite-position", string.Concat(new object[]
			{
				Math.Abs(xPixels.GetValueOrDefault()),
				"px ",
				Math.Abs(yPixels.GetValueOrDefault()),
				"px"
			}));
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0007BAA8 File Offset: 0x00079CA8
		private static DeclarationNode CreateDpiComment(double dpi)
		{
			return ImageAssemblyUpdateVisitor.CreateDebugDeclarationComment("-wg-background-dpi", dpi.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0007BAC0 File Offset: 0x00079CC0
		private static DeclarationNode CreateDebugDeclarationComment(string propertyName, string propertyValue)
		{
			return new DeclarationNode("/* " + propertyName, new ExprNode(new TermNode(string.Empty, null, propertyValue + "; */", null, null, null, null), null, null), string.Empty, null);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0007BAFC File Offset: 0x00079CFC
		private ReadOnlyCollection<DeclarationNode> UpdateDeclarations(ReadOnlyCollection<DeclarationNode> declarationNodes, AstNode parent)
		{
			ReadOnlyCollection<DeclarationNode> result;
			try
			{
				Background background;
				BackgroundImage backgroundImage;
				BackgroundPosition backgroundPosition;
				DeclarationNode backgroundSizeNode;
				if (!declarationNodes.TryGetBackgroundDeclaration(parent, out background, out backgroundImage, out backgroundPosition, out backgroundSizeNode, null, null, null, this.outputUnit, this.outputUnitFactor, false))
				{
					result = declarationNodes;
				}
				else
				{
					List<DeclarationNode> list = new List<DeclarationNode>(declarationNodes);
					if (this.dpi != 1f)
					{
						list.Insert(0, ImageAssemblyUpdateVisitor.CreateDpiComment((double)this.dpi));
					}
					if (background != null)
					{
						AssembledImage assembledImage;
						if (!this.TryGetAssembledImage(background.Url, background.BackgroundPosition, out assembledImage))
						{
							return declarationNodes;
						}
						list.Insert(0, ImageAssemblyUpdateVisitor.CreateDebugOriginalPositionComment(background.BackgroundPosition.X, background.BackgroundPosition.XSource, background.BackgroundPosition.Y, background.BackgroundPosition.YSource));
						list.Insert(0, ImageAssemblyUpdateVisitor.CreateDebugSpritePositionComment(assembledImage.X, assembledImage.Y));
						DeclarationNode updatedDeclarationNode = background.UpdateBackgroundNode(assembledImage.RelativeOutputFilePath, assembledImage.X, assembledImage.Y, this.dpi);
						ImageAssemblyUpdateVisitor.UpdateDeclarations(list, background.DeclarationAstNode, updatedDeclarationNode);
						this.SetBackgroundSize(list, backgroundSizeNode, this.dpi, assembledImage);
					}
					else if (backgroundImage != null)
					{
						AssembledImage assembledImage;
						if (!this.TryGetAssembledImage(backgroundImage.Url, backgroundPosition, out assembledImage))
						{
							return declarationNodes;
						}
						DeclarationNode updatedDeclarationNode2 = backgroundImage.UpdateBackgroundImageNode(assembledImage.RelativeOutputFilePath);
						ImageAssemblyUpdateVisitor.UpdateDeclarations(list, backgroundImage.DeclarationNode, updatedDeclarationNode2);
						if (backgroundPosition != null)
						{
							list.Insert(0, ImageAssemblyUpdateVisitor.CreateDebugOriginalPositionComment(backgroundPosition.X, backgroundPosition.XSource, backgroundPosition.Y, backgroundPosition.YSource));
							list.Insert(0, ImageAssemblyUpdateVisitor.CreateDebugSpritePositionComment(assembledImage.X, assembledImage.Y));
							BackgroundPosition backgroundPosition2 = backgroundPosition;
							int? x = assembledImage.X;
							float? updatedX = (x != null) ? new float?((float)x.GetValueOrDefault()) : null;
							int? y = assembledImage.Y;
							updatedDeclarationNode2 = backgroundPosition2.UpdateBackgroundPositionNode(updatedX, (y != null) ? new float?((float)y.GetValueOrDefault()) : null, this.dpi);
							ImageAssemblyUpdateVisitor.UpdateDeclarations(list, backgroundPosition.DeclarationNode, updatedDeclarationNode2);
						}
						else
						{
							int? x2 = assembledImage.X;
							float? updatedX2 = (x2 != null) ? new float?((float)x2.GetValueOrDefault()) : null;
							int? y2 = assembledImage.Y;
							DeclarationNode declarationNode = BackgroundPosition.CreateNewDeclaration(updatedX2, (y2 != null) ? new float?((float)y2.GetValueOrDefault()) : null, this.dpi, this.outputUnit, this.outputUnitFactor);
							list.Insert(0, ImageAssemblyUpdateVisitor.CreateDebugSpritePositionComment(assembledImage.X, assembledImage.Y));
							if (declarationNode != null)
							{
								list.Add(declarationNode);
							}
						}
						this.SetBackgroundSize(list, backgroundSizeNode, this.dpi, assembledImage);
					}
					result = list.AsReadOnly();
				}
			}
			catch (Exception innerException)
			{
				throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.InnerExceptionSelector, new object[]
				{
					parent.PrettyPrint()
				}), innerException);
			}
			return result;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0007BE14 File Offset: 0x0007A014
		private void SetBackgroundSize(List<DeclarationNode> updatedDeclarations, DeclarationNode backgroundSizeNode, float dpiFactor, AssembledImage assembledImage)
		{
			if (backgroundSizeNode != null)
			{
				updatedDeclarations.Remove(backgroundSizeNode);
			}
			if ((dpiFactor != 1f || this.outputUnit != null) && assembledImage.SpriteHeight != null && assembledImage.SpriteWidth != null)
			{
				updatedDeclarations.AddRange(this.CreateBackgroundSizeNode(assembledImage, dpiFactor));
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0007BE70 File Offset: 0x0007A070
		private IEnumerable<DeclarationNode> CreateBackgroundSizeNode(AssembledImage assembledImage, float dpiFactor)
		{
			int? spriteWidth = assembledImage.SpriteWidth;
			float? number = new float?((float)Math.Round((double)((spriteWidth != null) ? ((float)spriteWidth.GetValueOrDefault()) : 0f) * this.outputUnitFactor / (double)dpiFactor, 3));
			int? spriteHeight = assembledImage.SpriteHeight;
			float? number2 = new float?((float)Math.Round((double)((spriteHeight != null) ? ((float)spriteHeight.GetValueOrDefault()) : 0f) * this.outputUnitFactor / (double)dpiFactor, 3));
			TermNode termNode = new TermNode(number.UnaryOperator(), number.CssUnitValue(this.outputUnit), null, null, null, null, null);
			TermNode termNode2 = new TermNode(number2.UnaryOperator(), number2.CssUnitValue(this.outputUnit), null, null, null, null, null);
			List<TermWithOperatorNode> enumerable = new List<TermWithOperatorNode>
			{
				new TermWithOperatorNode(" ", termNode2)
			};
			DeclarationNode declarationNode = new DeclarationNode("background-size", new ExprNode(termNode, enumerable.ToSafeReadOnlyCollection<TermWithOperatorNode>(), null), null, null);
			return new DeclarationNode[]
			{
				ImageAssemblyUpdateVisitor.CreateDebugDeclarationComment("-wg-background-size-params", string.Concat(new object[]
				{
					" (sprite size: ",
					assembledImage.SpriteWidth,
					"px ",
					assembledImage.SpriteHeight,
					"px) (output unit factor: ",
					this.outputUnitFactor,
					") (dpi: ",
					dpiFactor,
					") (imageposition:",
					assembledImage.ImagePosition,
					")"
				})),
				declarationNode
			};
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0007C060 File Offset: 0x0007A260
		private bool TryGetAssembledImage(string parsedImagePath, BackgroundPosition backgroundPosition, out AssembledImage assembledImage)
		{
			if (this.availableSourceImages == null && string.IsNullOrWhiteSpace(this.cssPath))
			{
				throw new BuildWorkflowException("Need either images or css path to be able to set a valid image file.");
			}
			assembledImage = null;
			if (this.inputImages == null)
			{
				return false;
			}
			if (parsedImagePath.StartsWith("hash://", StringComparison.OrdinalIgnoreCase))
			{
				parsedImagePath = parsedImagePath.Substring(7);
			}
			if (this.availableSourceImages != null)
			{
				if (!this.availableSourceImages.TryGetValue(parsedImagePath.NormalizeUrl(), out parsedImagePath) && !string.IsNullOrWhiteSpace(this.missingImage))
				{
					parsedImagePath = this.availableSourceImages.TryGetValue(this.missingImage);
				}
			}
			else
			{
				parsedImagePath = parsedImagePath.MakeAbsoluteTo(this.cssPath);
			}
			ImagePosition imagePosition = ImagePosition.Left;
			if (backgroundPosition != null)
			{
				imagePosition = backgroundPosition.GetImagePositionInVerticalSprite();
			}
			assembledImage = this.inputImages.FirstOrDefault((AssembledImage inputImage) => inputImage.ImagePosition == imagePosition && inputImage.OriginalFilePath.Equals(parsedImagePath, StringComparison.OrdinalIgnoreCase));
			if (assembledImage != null && assembledImage.OutputFilePath != null)
			{
				assembledImage.RelativeOutputFilePath = ((!string.IsNullOrWhiteSpace(this.destinationDirectory)) ? Path.Combine(this.prependToDestination, assembledImage.OutputFilePath.MakeRelativeToDirectory(this.destinationDirectory).Replace('\\', '/')) : assembledImage.OutputFilePath.MakeRelativeTo(this.cssPath, new char[0]));
				if (!string.IsNullOrWhiteSpace(assembledImage.RelativeOutputFilePath))
				{
					assembledImage.RelativeOutputFilePath = assembledImage.RelativeOutputFilePath.Replace('\\', '/');
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000B6C RID: 2924
		private readonly string outputUnit;

		// Token: 0x04000B6D RID: 2925
		private readonly double outputUnitFactor;

		// Token: 0x04000B6E RID: 2926
		private readonly string cssPath;

		// Token: 0x04000B6F RID: 2927
		private readonly IEnumerable<AssembledImage> inputImages;

		// Token: 0x04000B70 RID: 2928
		private readonly float dpi;

		// Token: 0x04000B71 RID: 2929
		private readonly string destinationDirectory;

		// Token: 0x04000B72 RID: 2930
		private readonly string prependToDestination;

		// Token: 0x04000B73 RID: 2931
		private readonly IDictionary<string, string> availableSourceImages;

		// Token: 0x04000B74 RID: 2932
		private readonly string missingImage;
	}
}
