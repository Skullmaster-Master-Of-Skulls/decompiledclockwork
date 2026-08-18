using System;
using System.Collections.Generic;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.ImageAssemblyAnalysis.PropertyModel
{
	// Token: 0x02000193 RID: 403
	internal sealed class Background
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x00078F70 File Offset: 0x00077170
		internal Background(DeclarationNode declarationAstNode, string outputUnit, double outputUnitFactor)
		{
			this.DeclarationAstNode = declarationAstNode;
			this.BackgroundImage = new BackgroundImage();
			this.BackgroundPosition = new BackgroundPosition(outputUnit, outputUnitFactor);
			this.BackgroundRepeat = new BackgroundRepeat();
			ExprNode exprNode = declarationAstNode.ExprNode;
			TermNode termNode = exprNode.TermNode;
			this.BackgroundImage.ParseTerm(termNode);
			this.BackgroundPosition.ParseTerm(termNode);
			this.BackgroundRepeat.ParseTerm(termNode);
			exprNode.TermsWithOperators.ForEach(delegate(TermWithOperatorNode termWithOperator)
			{
				this.BackgroundImage.ParseTermWithOperator(termWithOperator);
				this.BackgroundPosition.ParseTermWithOperator(termWithOperator);
				this.BackgroundRepeat.ParseTermWithOperator(termWithOperator);
			});
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00078FFD File Offset: 0x000771FD
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00079005 File Offset: 0x00077205
		public DeclarationNode DeclarationAstNode { get; private set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0007900E File Offset: 0x0007720E
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x00079016 File Offset: 0x00077216
		internal BackgroundImage BackgroundImage { get; private set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0007901F File Offset: 0x0007721F
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x00079027 File Offset: 0x00077227
		internal BackgroundPosition BackgroundPosition { get; private set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00079030 File Offset: 0x00077230
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x00079038 File Offset: 0x00077238
		internal BackgroundRepeat BackgroundRepeat { get; private set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00079041 File Offset: 0x00077241
		internal string Url
		{
			get
			{
				return this.BackgroundImage.Url;
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00079050 File Offset: 0x00077250
		internal DeclarationNode UpdateBackgroundNode(string updatedUrl, int? updatedX, int? updatedY, float webGreaseBackgroundDpi)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = 0;
			int num2 = 0;
			List<TermWithOperatorNode> list = new List<TermWithOperatorNode>();
			foreach (TermWithOperatorNode termWithOperatorNode in this.DeclarationAstNode.DeclarationEnumerator())
			{
				if (!flag)
				{
					TermNode termNode;
					flag = this.BackgroundImage.UpdateTermForUrl(termWithOperatorNode.TermNode, out termNode, updatedUrl);
					if (flag)
					{
						list.Add(new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
						continue;
					}
				}
				if (!flag2)
				{
					BackgroundPosition backgroundPosition = this.BackgroundPosition;
					TermNode termNode2 = termWithOperatorNode.TermNode;
					int? num3 = updatedX;
					TermNode termNode;
					flag2 = backgroundPosition.UpdateTermForX(termNode2, out termNode, (num3 != null) ? new float?((float)num3.GetValueOrDefault()) : null, webGreaseBackgroundDpi);
					if (flag2)
					{
						if (flag3)
						{
							list.Insert(num, new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
							continue;
						}
						list.Add(new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
						num2 = list.Count;
						continue;
					}
				}
				if (!flag3)
				{
					BackgroundPosition backgroundPosition2 = this.BackgroundPosition;
					TermNode termNode3 = termWithOperatorNode.TermNode;
					int? num4 = updatedY;
					TermNode termNode;
					flag3 = backgroundPosition2.UpdateTermForY(termNode3, out termNode, (num4 != null) ? new float?((float)num4.GetValueOrDefault()) : null, webGreaseBackgroundDpi);
					if (flag3)
					{
						if (flag2)
						{
							list.Insert(num2, new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
							continue;
						}
						list.Add(new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
						num = list.Count - 1;
						continue;
					}
				}
				list.Add(termWithOperatorNode);
			}
			BackgroundPosition backgroundPosition3 = this.BackgroundPosition;
			int? num5 = updatedX;
			float? updatedX2 = (num5 != null) ? new float?((float)num5.GetValueOrDefault()) : null;
			int? num6 = updatedY;
			backgroundPosition3.AddingMissingXAndY(updatedX2, (num6 != null) ? new float?((float)num6.GetValueOrDefault()) : null, flag2, flag3, num, num2, list, webGreaseBackgroundDpi);
			return this.DeclarationAstNode.CreateDeclarationNode(list);
		}
	}
}
