using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebGrease.Activities;
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
	// Token: 0x0200019F RID: 415
	public sealed class ImageAssemblyScanVisitor : NodeVisitor
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0007AFBF File Offset: 0x000791BF
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x0007AFC7 File Offset: 0x000791C7
		internal IWebGreaseContext Context { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0007AFD0 File Offset: 0x000791D0
		internal ImageAssemblyScanOutput DefaultImageAssemblyScanOutput
		{
			get
			{
				return this._defaultImageAssemblyScanOutput;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0007AFD8 File Offset: 0x000791D8
		internal IList<ImageAssemblyScanOutput> ImageAssemblyScanOutputs
		{
			get
			{
				return this._imageAssemblyScanOutputs;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0007AFF4 File Offset: 0x000791F4
		public ImageAssemblyScanVisitor(string cssPath, IEnumerable<string> imageReferencesToIgnore, bool ignoreImagesWithNonDefaultBackgroundSize = false, string outputUnit = "px", double outputUnitFactor = 1.0, IDictionary<string, string> availableImageSources = null, string missingImage = null, bool imageNotFoundThrowError = false)
		{
			this._missingImage = missingImage;
			this.imageNotFoundThrowError = imageNotFoundThrowError;
			this._availableImageSources = availableImageSources;
			this._imageAssemblyScanOutputs.Add(this._defaultImageAssemblyScanOutput);
			this._cssPath = cssPath.GetFullPathWithLowercase();
			this._ignoreImagesWithNonDefaultBackgroundSize = ignoreImagesWithNonDefaultBackgroundSize;
			this.outputUnit = outputUnit;
			this.outputUnitFactor = outputUnitFactor;
			if (imageReferencesToIgnore != null)
			{
				imageReferencesToIgnore.ForEach(delegate(string imageReferenceToIgnore)
				{
					this._imageReferencesToIgnore.Add(imageReferenceToIgnore.NormalizeUrl());
				});
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0007B0A6 File Offset: 0x000792A6
		public ImageAssemblyAnalysisLog ImageAssemblyAnalysisLog
		{
			get
			{
				return this._imageAssemblyAnalysisLog;
			}
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0007B0B8 File Offset: 0x000792B8
		public override AstNode VisitStyleSheetNode(StyleSheetNode styleSheet)
		{
			this._imagesCriteriaFailedReferences.Clear();
			styleSheet.StyleSheetRules.ForEach(delegate(StyleSheetRuleNode styleSheetRuleNode)
			{
				styleSheetRuleNode.Accept(this);
			});
			return styleSheet;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0007B0DD File Offset: 0x000792DD
		public override AstNode VisitRulesetNode(RulesetNode rulesetNode)
		{
			this.VisitBackgroundDeclarationNode(rulesetNode.Declarations, rulesetNode);
			return rulesetNode;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0007B101 File Offset: 0x00079301
		public override AstNode VisitMediaNode(MediaNode mediaNode)
		{
			mediaNode.Rulesets.ForEach(delegate(RulesetNode rulesetNode)
			{
				rulesetNode.Accept(this);
			});
			mediaNode.PageNodes.ForEach(delegate(PageNode pageNode)
			{
				pageNode.Accept(this);
			});
			return mediaNode;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0007B132 File Offset: 0x00079332
		public override AstNode VisitPageNode(PageNode pageNode)
		{
			this.VisitBackgroundDeclarationNode(pageNode.Declarations, pageNode);
			return pageNode;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0007B142 File Offset: 0x00079342
		public override AstNode VisitTermWithOperatorNode(TermWithOperatorNode termWithOperatorNode)
		{
			termWithOperatorNode.TermNode.Accept(this);
			return termWithOperatorNode;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0007B208 File Offset: 0x00079408
		private void VisitBackgroundDeclarationNode(IEnumerable<DeclarationNode> declarations, AstNode parent)
		{
			try
			{
				List<string> list = new List<string>();
				Background background;
				BackgroundImage backgroundImage;
				BackgroundPosition backgroundPosition;
				DeclarationNode declarationNode;
				if (!declarations.TryGetBackgroundDeclaration(parent, out background, out backgroundImage, out backgroundPosition, out declarationNode, list, this._imageReferencesToIgnore, this._imageAssemblyAnalysisLog, this.outputUnit, this.outputUnitFactor, this._ignoreImagesWithNonDefaultBackgroundSize))
				{
					list.ForEach(delegate(string imagesCriteriaFailedUrl)
					{
						string url = imagesCriteriaFailedUrl.NormalizeUrl().MakeAbsoluteTo(this._cssPath);
						if (this._imageAssemblyScanOutputs.Any((ImageAssemblyScanOutput imageAssemblyScanOutput) => (from imageReference in imageAssemblyScanOutput.ImageReferencesToAssemble
						where imageReference.AbsoluteImagePath == url
						select imageReference).Any<InputImage>()))
						{
							throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.DuplicateImageReferenceWithDifferentRulesError, new object[]
							{
								url
							}));
						}
						this._imagesCriteriaFailedReferences.Add(url);
					});
				}
				else if (background != null)
				{
					this.AddImageReference(background.Url, background.BackgroundPosition);
				}
				else if (backgroundImage != null && backgroundPosition != null)
				{
					this.AddImageReference(backgroundImage.Url, backgroundPosition);
				}
			}
			catch (Exception innerException)
			{
				throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.InnerExceptionSelector, new object[]
				{
					parent.PrettyPrint()
				}), innerException);
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0007B33C File Offset: 0x0007953C
		private void AddImageReference(string url, BackgroundPosition backgroundPosition)
		{
			string text = url.NormalizeUrl();
			string url2 = url;
			if (ResourcesResolver.LocalizationResourceKeyRegex.IsMatch(text))
			{
				return;
			}
			url = this.GetAbsoluteImagePath(text);
			if (this._imageReferencesToIgnore.Contains(text) || this._imageReferencesToIgnore.Contains(Path.GetDirectoryName(text) + "\\*"))
			{
				return;
			}
			if (this._imagesCriteriaFailedReferences.Any((string ir) => ir.Equals(url, StringComparison.OrdinalIgnoreCase)))
			{
				throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.DuplicateImageReferenceWithDifferentRulesError, new object[]
				{
					url
				}));
			}
			ImagePosition imagePosition = backgroundPosition.GetImagePositionInVerticalSprite();
			bool flag = false;
			for (int i = 1; i < this._imageAssemblyScanOutputs.Count; i++)
			{
				ImageAssemblyScanOutput imageAssemblyScanOutput = this._imageAssemblyScanOutputs[i];
				if (imageAssemblyScanOutput.ImageAssemblyScanInput.ImagesInBucket.Contains(url))
				{
					if (!imageAssemblyScanOutput.ImageReferencesToAssemble.Any((InputImage inputImage) => inputImage.AbsoluteImagePath == url && inputImage.Position == imagePosition))
					{
						imageAssemblyScanOutput.ImageReferencesToAssemble.Add(new InputImage
						{
							AbsoluteImagePath = url,
							Position = imagePosition,
							OriginalImagePath = url2
						});
						flag = true;
					}
				}
			}
			if (flag)
			{
				return;
			}
			if (this._defaultImageAssemblyScanOutput.ImageReferencesToAssemble.Any((InputImage inputImage) => inputImage.AbsoluteImagePath == url && inputImage.Position == imagePosition))
			{
				return;
			}
			this._defaultImageAssemblyScanOutput.ImageReferencesToAssemble.Add(new InputImage
			{
				AbsoluteImagePath = url,
				Position = imagePosition,
				OriginalImagePath = url2
			});
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0007B510 File Offset: 0x00079710
		private string GetAbsoluteImagePath(string relativeUrl)
		{
			string text2;
			if (this._availableImageSources != null)
			{
				string text = this._availableImageSources.ContainsKey(relativeUrl) ? this._availableImageSources[relativeUrl] : null;
				text2 = text;
			}
			else
			{
				text2 = relativeUrl.MakeAbsoluteTo(this._cssPath);
			}
			if (string.IsNullOrWhiteSpace(text2) || !File.Exists(text2))
			{
				bool flag = relativeUrl.Equals(this._missingImage);
				if (!string.IsNullOrWhiteSpace(this._missingImage) && !flag)
				{
					text2 = this.GetAbsoluteImagePath(this._missingImage);
				}
				else if (this.imageNotFoundThrowError)
				{
					throw new FileNotFoundException(string.Concat(new string[]
					{
						"Could not find the image file:",
						relativeUrl,
						" (",
						text2,
						")"
					}), text2 ?? string.Empty);
				}
			}
			return text2;
		}

		// Token: 0x04000B5F RID: 2911
		private readonly bool _ignoreImagesWithNonDefaultBackgroundSize;

		// Token: 0x04000B60 RID: 2912
		private readonly string outputUnit;

		// Token: 0x04000B61 RID: 2913
		private readonly double outputUnitFactor;

		// Token: 0x04000B62 RID: 2914
		private readonly string _cssPath;

		// Token: 0x04000B63 RID: 2915
		private readonly string _missingImage;

		// Token: 0x04000B64 RID: 2916
		private readonly ImageAssemblyScanOutput _defaultImageAssemblyScanOutput = new ImageAssemblyScanOutput();

		// Token: 0x04000B65 RID: 2917
		private readonly ImageAssemblyAnalysisLog _imageAssemblyAnalysisLog = new ImageAssemblyAnalysisLog();

		// Token: 0x04000B66 RID: 2918
		private readonly IList<ImageAssemblyScanOutput> _imageAssemblyScanOutputs = new List<ImageAssemblyScanOutput>();

		// Token: 0x04000B67 RID: 2919
		private readonly HashSet<string> _imageReferencesToIgnore = new HashSet<string>();

		// Token: 0x04000B68 RID: 2920
		private readonly IDictionary<string, string> _availableImageSources;

		// Token: 0x04000B69 RID: 2921
		private readonly HashSet<string> _imagesCriteriaFailedReferences = new HashSet<string>();

		// Token: 0x04000B6A RID: 2922
		private bool imageNotFoundThrowError;
	}
}
