using System;
using System.Collections.Generic;
using System.Globalization;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;
using WebGrease.ImageAssemble;

namespace WebGrease.Css.ImageAssemblyAnalysis.PropertyModel
{
	// Token: 0x02000196 RID: 406
	internal sealed class BackgroundPosition
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x0007958B File Offset: 0x0007778B
		internal BackgroundPosition(string outputUnit, double outputUnitFactor)
		{
			this.outputUnit = outputUnit;
			this.outputUnitFactor = outputUnitFactor;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000795BC File Offset: 0x000777BC
		internal BackgroundPosition(DeclarationNode declarationNode, string outputUnit, double outputUnitFactor)
		{
			this.outputUnit = outputUnit;
			this.outputUnitFactor = outputUnitFactor;
			if (declarationNode == null)
			{
				throw new ArgumentNullException("declarationNode");
			}
			this.DeclarationNode = declarationNode;
			ExprNode exprNode = declarationNode.ExprNode;
			this.ParseTerm(exprNode.TermNode);
			exprNode.TermsWithOperators.ForEach(new Action<TermWithOperatorNode>(this.ParseTermWithOperator));
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00079636 File Offset: 0x00077836
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0007963E File Offset: 0x0007783E
		public DeclarationNode DeclarationNode { get; private set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00079647 File Offset: 0x00077847
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0007964F File Offset: 0x0007784F
		internal float? X { get; private set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00079658 File Offset: 0x00077858
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00079660 File Offset: 0x00077860
		internal float? Y { get; private set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00079669 File Offset: 0x00077869
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00079671 File Offset: 0x00077871
		internal Source? XSource { get; private set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0007967A File Offset: 0x0007787A
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00079682 File Offset: 0x00077882
		internal Source? YSource { get; private set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0007968B File Offset: 0x0007788B
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00079693 File Offset: 0x00077893
		private TermNode XTermNode { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0007969C File Offset: 0x0007789C
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x000796A4 File Offset: 0x000778A4
		private TermNode YTermNode { get; set; }

		// Token: 0x060014F2 RID: 5362 RVA: 0x000796B0 File Offset: 0x000778B0
		internal static DeclarationNode CreateNewDeclaration(float? updatedX, float? updatedY, float webGreaseBackgroundDpi, string outputUnit, double outputUnitFactor)
		{
			if (updatedX != null)
			{
				float? num = updatedX;
				if (num.GetValueOrDefault() == 0f && num != null)
				{
					float? num2 = updatedY;
					if (num2.GetValueOrDefault() == 0f && num2 != null)
					{
						goto IL_45;
					}
				}
				float? number = new float?((float)Math.Round((double)updatedX.Value * outputUnitFactor / (double)webGreaseBackgroundDpi, 3));
				TermNode termNode = new TermNode(number.UnaryOperator(), number.CssUnitValue(outputUnit), null, null, null, null, null);
				List<TermWithOperatorNode> list = new List<TermWithOperatorNode>();
				if (updatedY != null)
				{
					float? num3 = updatedY;
					if (num3.GetValueOrDefault() != 0f || num3 == null)
					{
						float? number2 = new float?((float)Math.Round((double)updatedY.Value * outputUnitFactor / (double)webGreaseBackgroundDpi, 3));
						TermNode termNode2 = new TermNode(number2.UnaryOperator(), number2.CssUnitValue(outputUnit), null, null, null, null, null);
						list.Add(new TermWithOperatorNode(" ", termNode2));
					}
				}
				ExprNode exprNode = new ExprNode(termNode, list.AsReadOnly(), null);
				return new DeclarationNode("background-position", exprNode, null, null);
			}
			IL_45:
			return null;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000797D0 File Offset: 0x000779D0
		internal void AddingMissingXAndY(float? updatedX, float? updatedY, bool isXUpdated, bool isYUpdated, int indexX, int indexY, List<TermWithOperatorNode> newTermsWithOperators, float webGreaseBackgroundDpi)
		{
			string unaryOperator = null;
			string unaryOperator2 = null;
			string numberBasedValue = "center";
			string numberBasedValue2 = "center";
			if (!isXUpdated && !isYUpdated)
			{
				float? number = new float?((float)Math.Round((double)updatedX.GetValueOrDefault() * this.outputUnitFactor / (double)webGreaseBackgroundDpi, 3));
				float? number2 = new float?((float)Math.Round((double)updatedY.GetValueOrDefault() * this.outputUnitFactor / (double)webGreaseBackgroundDpi, 3));
				unaryOperator = number.UnaryOperator();
				unaryOperator2 = number2.UnaryOperator();
				numberBasedValue = number.CssUnitValue(this.outputUnit);
				numberBasedValue2 = number2.CssUnitValue(this.outputUnit);
			}
			if (!isXUpdated)
			{
				newTermsWithOperators.Insert(indexX, new TermWithOperatorNode(" ", new TermNode(unaryOperator, numberBasedValue, null, null, null, null, null)));
				indexY = indexX + 1;
			}
			if (!isYUpdated)
			{
				newTermsWithOperators.Insert(indexY, new TermWithOperatorNode(" ", new TermNode(unaryOperator2, numberBasedValue2, null, null, null, null, null)));
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000798B0 File Offset: 0x00077AB0
		internal bool IsVerticalSpriteCandidate()
		{
			return (this.X == null && this.XSource == null && this.Y == null && this.YSource == null) || (this.Y != null && this.Y.Value == 0f) || this.YSource == Source.Px;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00079948 File Offset: 0x00077B48
		internal bool IsHorizontalRightAligned()
		{
			return (this.XSource == Source.Right && this.Y != null && this.Y.Value == 0f) || (this.XSource == Source.Right && this.YSource == Source.Px) || (this.XSource == Source.Percentage && this.X != null && this.X.Value == 100f && this.Y != null && this.Y.Value == 0f) || (this.XSource == Source.Percentage && this.X != null && this.X.Value == 100f && this.YSource == Source.Px);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00079AA8 File Offset: 0x00077CA8
		internal bool IsHorizontalCenterAligned()
		{
			return (this.XSource == null && this.YSource == Source.Top) || (this.XSource == Source.Center && this.Y != null && this.Y.Value == 0f) || (this.XSource == Source.Center && this.YSource == Source.Px) || (this.XSource == Source.Percentage && this.X != null && this.X.Value == 50f && this.Y != null && this.Y.Value == 0f) || (this.XSource == Source.Percentage && this.X != null && this.X.Value == 50f && this.YSource == Source.Px);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00079C3A File Offset: 0x00077E3A
		internal ImagePosition GetImagePositionInVerticalSprite()
		{
			if (this.IsHorizontalCenterAligned())
			{
				return ImagePosition.Center;
			}
			if (this.IsHorizontalRightAligned())
			{
				return ImagePosition.Right;
			}
			return ImagePosition.Left;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00079C54 File Offset: 0x00077E54
		internal void ParseTerm(TermNode termNode)
		{
			string stringBasedValue;
			if (!string.IsNullOrWhiteSpace(termNode.StringBasedValue) && (stringBasedValue = termNode.StringBasedValue) != null)
			{
				if (!(stringBasedValue == "left"))
				{
					if (!(stringBasedValue == "right"))
					{
						if (!(stringBasedValue == "center"))
						{
							if (!(stringBasedValue == "top"))
							{
								if (stringBasedValue == "bottom")
								{
									this.AssignY(termNode, null, new int?(1), Source.Bottom);
								}
							}
							else
							{
								this.AssignY(termNode, new float?(0f), new int?(1), Source.Top);
							}
						}
						else
						{
							this.AssignXy(termNode, null, new int?(1), Source.Center);
						}
					}
					else
					{
						this.TrySwapXCoordinate();
						this.AssignX(termNode, null, new int?(1), Source.Right);
					}
				}
				else
				{
					this.TrySwapXCoordinate();
					this.AssignX(termNode, new float?(0f), new int?(1), Source.Left);
				}
			}
			if (string.IsNullOrWhiteSpace(termNode.NumberBasedValue))
			{
				return;
			}
			string numberBasedValue = termNode.NumberBasedValue;
			float num;
			if (numberBasedValue.EndsWith("px", StringComparison.OrdinalIgnoreCase) && numberBasedValue.Length > 2 && float.TryParse(numberBasedValue.Substring(0, numberBasedValue.Length - 2), out num))
			{
				this.AssignXy(termNode, new float?(num), new int?(termNode.UnaryOperator.SignInt()), Source.Px);
				return;
			}
			if (numberBasedValue.EndsWith("rem", StringComparison.OrdinalIgnoreCase) && numberBasedValue.Length > 1 && this.outputUnit == "rem" && float.TryParse(numberBasedValue.Substring(0, numberBasedValue.Length - 3), out num))
			{
				this.AssignXy(termNode, new float?((float)((double)num / this.outputUnitFactor)), new int?(termNode.UnaryOperator.SignInt()), Source.Px);
				return;
			}
			if (numberBasedValue.EndsWith("em", StringComparison.OrdinalIgnoreCase) && numberBasedValue.Length > 1 && this.outputUnit == "em" && float.TryParse(numberBasedValue.Substring(0, numberBasedValue.Length - 2), out num))
			{
				this.AssignXy(termNode, new float?((float)((double)num / this.outputUnitFactor)), new int?(termNode.UnaryOperator.SignInt()), Source.Px);
				return;
			}
			if (numberBasedValue.EndsWith("%", StringComparison.OrdinalIgnoreCase) && numberBasedValue.Length > 1 && float.TryParse(numberBasedValue.Substring(0, numberBasedValue.Length - 1), out num))
			{
				this.AssignXy(termNode, new float?(num), new int?(termNode.UnaryOperator.SignInt()), Source.Percentage);
				return;
			}
			if (numberBasedValue.TryParseZeroBasedNumberValue())
			{
				this.AssignXy(termNode, new float?(0f), new int?(termNode.UnaryOperator.SignInt()), Source.NoUnits);
				return;
			}
			if (float.TryParse(numberBasedValue, out num))
			{
				this.AssignXy(termNode, new float?(num), new int?(termNode.UnaryOperator.SignInt()), Source.NoUnits);
				return;
			}
			this.AssignXy(termNode, null, new int?(1), Source.Unknown);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00079F3E File Offset: 0x0007813E
		internal void ParseTermWithOperator(TermWithOperatorNode termWithOperatorNode)
		{
			this.ParseTerm(termWithOperatorNode.TermNode);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00079F4C File Offset: 0x0007814C
		internal bool UpdateTermForX(TermNode termNode, out TermNode updatedTermNode, float? updatedX, float webGreaseBackgroundDpi)
		{
			if (termNode == this.XTermNode)
			{
				float? x = this.X;
				if ((x.GetValueOrDefault() == 0f && x != null) || this.XSource == Source.Px)
				{
					float? number = new float?((float)Math.Round((double)(this.X.GetValueOrDefault() + updatedX.GetValueOrDefault() / webGreaseBackgroundDpi) * this.outputUnitFactor, 3));
					updatedTermNode = new TermNode(number.UnaryOperator(), number.CssUnitValue(this.outputUnit), null, null, null, null, null);
				}
				else
				{
					updatedTermNode = termNode;
				}
				return true;
			}
			updatedTermNode = termNode;
			return false;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00079FFC File Offset: 0x000781FC
		internal bool UpdateTermForY(TermNode termNode, out TermNode updatedTermNode, float? updatedY, float webGreaseBackgroundDpi)
		{
			if (termNode == this.YTermNode)
			{
				float? y = this.Y;
				if ((y.GetValueOrDefault() == 0f && y != null) || this.YSource == Source.Px)
				{
					float? number = new float?((float)Math.Round((double)(this.Y.GetValueOrDefault() + updatedY.GetValueOrDefault() / webGreaseBackgroundDpi) * this.outputUnitFactor, 3));
					updatedTermNode = new TermNode(number.UnaryOperator(), number.CssUnitValue(this.outputUnit), null, null, null, null, null);
				}
				else
				{
					updatedTermNode = termNode;
				}
				return true;
			}
			updatedTermNode = termNode;
			return false;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0007A0AC File Offset: 0x000782AC
		internal DeclarationNode UpdateBackgroundPositionNode(float? updatedX, float? updatedY, float webGreaseBackgroundDpi)
		{
			if (this.DeclarationNode == null)
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			List<TermWithOperatorNode> list = new List<TermWithOperatorNode>();
			foreach (TermWithOperatorNode termWithOperatorNode in this.DeclarationNode.DeclarationEnumerator())
			{
				if (!flag)
				{
					TermNode termNode;
					flag = this.UpdateTermForX(termWithOperatorNode.TermNode, out termNode, updatedX, webGreaseBackgroundDpi);
					if (flag)
					{
						if (flag2)
						{
							list.Insert(num, new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
							continue;
						}
						list.Add(new TermWithOperatorNode(termWithOperatorNode.Operator, termNode.CopyTerm()));
						num2 = list.Count;
						continue;
					}
				}
				if (!flag2)
				{
					TermNode termNode;
					flag2 = this.UpdateTermForY(termWithOperatorNode.TermNode, out termNode, updatedY, webGreaseBackgroundDpi);
					if (flag2)
					{
						if (flag)
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
			this.AddingMissingXAndY(updatedX, updatedY, flag, flag2, num, num2, list, webGreaseBackgroundDpi);
			return this.DeclarationNode.CreateDeclarationNode(list);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0007A200 File Offset: 0x00078400
		private void TrySwapXCoordinate()
		{
			if (this.XSource != Source.Center)
			{
				return;
			}
			this.AssignY(this.XTermNode, this.X, new int?(1), this.XSource.Value);
			this.XTermNode = null;
			this.X = null;
			this.XSource = null;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0007A27C File Offset: 0x0007847C
		private void AssignX(TermNode termNode, float? offset, int? sign, Source source)
		{
			if (this.XSource != null)
			{
				throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.TooManyLengthsError, new object[]
				{
					termNode.PrettyPrint()
				}));
			}
			this.XTermNode = termNode;
			if (offset != null && sign != null)
			{
				float? num = offset;
				int? num2 = sign;
				this.X = ((num != null & num2 != null) ? new float?(num.GetValueOrDefault() * (float)num2.GetValueOrDefault()) : null);
			}
			this.XSource = new Source?(source);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0007A324 File Offset: 0x00078524
		private void AssignY(TermNode termNode, float? offset, int? sign, Source source)
		{
			if (this.YSource != null)
			{
				throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.TooManyLengthsError, new object[]
				{
					termNode.PrettyPrint()
				}));
			}
			this.YTermNode = termNode;
			if (offset != null && sign != null)
			{
				float? num = offset;
				int? num2 = sign;
				this.Y = ((num != null & num2 != null) ? new float?(num.GetValueOrDefault() * (float)num2.GetValueOrDefault()) : null);
			}
			this.YSource = new Source?(source);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0007A3CC File Offset: 0x000785CC
		private void AssignXy(TermNode termNode, float? offset, int? sign, Source source)
		{
			if (this.XSource == null)
			{
				this.AssignX(termNode, offset, sign, source);
				return;
			}
			if (this.YSource == null)
			{
				this.AssignY(termNode, offset, sign, source);
				return;
			}
			throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.TooManyLengthsError, new object[]
			{
				termNode.PrettyPrint()
			}));
		}

		// Token: 0x04000B40 RID: 2880
		private readonly string outputUnit = "px";

		// Token: 0x04000B41 RID: 2881
		private readonly double outputUnitFactor = 1.0;
	}
}
