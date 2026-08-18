using System;
using System.Collections;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

// Token: 0x02000250 RID: 592
internal class spr\u21D6
{
	// Token: 0x06001DCC RID: 7628 RVA: 0x001D7CB0 File Offset: 0x001D6CB0
	public static spr\u21D6 ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6F;
				default:
					if (false)
					{
					}
					spr\u21D6.ᜀ = new spr\u21D6();
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_6D;
			}
			if (true)
			{
			}
			if (spr\u21D6.ᜀ != null)
			{
				break;
			}
			num = 0;
		}
		IL_6D:
		IL_6F:
		return spr\u21D6.ᜀ;
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x001D7D34 File Offset: 0x001D6D34
	public int ᜁ(Paragraph A_0, Regex A_1, string A_2)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				ParagraphItemCollection items = A_0.Items;
				string text = A_0.Text;
				MatchCollection matchCollection = A_1.Matches(text);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (num2 <= 0)
						{
							num = 2;
							continue;
						}
						return num2;
					case 1:
						if (A_0.Document.ReplaceFirst)
						{
							num = 3;
							continue;
						}
						goto IL_C3;
					case 2:
						goto IL_C3;
					case 3:
						num = 0;
						continue;
					case 4:
					{
						if (true)
						{
						}
						int num3 = 0;
						int num4 = 0;
						int length = A_2.Length;
						int num5 = 0;
						int num6 = 0;
						IEnumerator enumerator = matchCollection.GetEnumerator();
						num = 5;
						continue;
					}
					case 5:
						try
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_204:
								num = 14;
								break;
							default:
								if (false)
								{
								}
								num = 5;
								break;
							}
							for (;;)
							{
								int num3;
								int num4;
								int num5;
								int num6;
								TextRange textRange;
								int num7;
								TextRange textRange2;
								int num9;
								switch (num)
								{
								case 0:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									Match match = (Match)enumerator.Current;
									num5 = match.Index + num3;
									num6 = match.Length;
									int length;
									num4 = length - match.Length;
									A_0.ᜀ(num5, num6, A_2);
									num7 = spr\u1AB5.ᜀ(A_0, num5 + 1, out textRange);
									int num8 = textRange.StartPos + textRange.TextLength;
									textRange.SafeText = false;
									num = 2;
									continue;
								}
								case 1:
									num = 8;
									continue;
								case 2:
								{
									int num8;
									if (num8 <= num5 + num6)
									{
										num = 1;
										continue;
									}
									goto IL_2EB;
								}
								case 3:
									goto IL_345;
								case 4:
									if (!A_0.Document.ReplaceFirst)
									{
										goto IL_204;
									}
									goto IL_345;
								case 6:
									goto IL_351;
								case 7:
									goto IL_2EB;
								case 8:
									if (textRange.NextSibling != null)
									{
										num = 11;
										continue;
									}
									goto IL_17B;
								case 9:
									if (textRange2 != null)
									{
										num = 16;
										continue;
									}
									goto IL_215;
								case 10:
									goto IL_215;
								case 11:
									num = 13;
									continue;
								case 12:
									goto IL_1D8;
								case 13:
									if (textRange.NextSibling.DocumentObjectType != DocumentObjectType.TextRange)
									{
										num = 7;
										continue;
									}
									goto IL_17B;
								case 15:
									goto IL_1D8;
								case 16:
									textRange2.TextLength -= num9 - textRange2.StartPos;
									textRange2.StartPos = num9 + num4;
									num7++;
									num = 10;
									continue;
								}
								IL_158:
								num = 0;
								continue;
								goto IL_158;
								IL_17B:
								this.ᜀ(A_0, num5 + num6, num7 + 1, out textRange2);
								num9 = num5 + num6;
								num = 9;
								continue;
								IL_1D8:
								this.ᜀ(A_0, num7 + 1, num4);
								num3 += num4;
								num = 4;
								continue;
								IL_215:
								textRange.TextLength = num9 + num4 - textRange.StartPos;
								num = 15;
								continue;
								IL_2EB:
								textRange.TextLength += num4;
								num = 12;
								continue;
								IL_345:
								num = 6;
							}
							IL_351:;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable != null)
										{
											num = 1;
											continue;
										}
										goto IL_39B;
									case 1:
										disposable.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_399;
									}
									break;
								}
							}
							IL_399:
							IL_39B:;
						}
						goto IL_39C;
					case 6:
						if (matchCollection.Count > 0)
						{
							num = 4;
							continue;
						}
						goto IL_39C;
					case 7:
						return num2;
					}
					break;
					IL_C3:
					num2 += spr\u21D6.ᜀ(A_0, A_1, A_2);
					num = 7;
					continue;
					IL_39C:
					num2 = matchCollection.Count;
					num = 1;
				}
			}
			return num2;
		}
		}
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x001D8130 File Offset: 0x001D7130
	private static int ᜀ(Paragraph A_0, Regex A_1, string A_2)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		int num = 0;
		IEnumerator enumerator = A_0.Items.GetEnumerator();
		try
		{
			int num2 = 9;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					Field field;
					if (field.Type == FieldType.FieldHyperlink)
					{
						num2 = 20;
						continue;
					}
					goto IL_C2;
				}
				case 1:
					goto IL_C2;
				case 2:
					num2 = 19;
					continue;
				case 3:
				{
					Field field;
					if ((field.NextSibling.NextSibling as TextRange).Text == A_2)
					{
						num2 = 12;
						continue;
					}
					goto IL_C2;
				}
				case 4:
					goto IL_2DA;
				case 6:
					goto IL_C2;
				case 7:
				{
					if (!enumerator.MoveNext())
					{
						num2 = 11;
						continue;
					}
					ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
					Body body = null;
					DocumentObjectType documentObjectType = paragraphBase.DocumentObjectType;
					num2 = 18;
					continue;
				}
				case 8:
					goto IL_C2;
				case 10:
				{
					Body body;
					num += body.ᜀ(A_1, A_2);
					num2 = 5;
					continue;
				}
				case 11:
					num2 = 4;
					continue;
				case 12:
				{
					Field field;
					field.Code = A_1.Replace(field.Code, A_2);
					field.FieldValue = A_1.Replace(field.Value, A_2);
					num2 = 6;
					continue;
				}
				case 13:
				{
					Body body;
					if (body != null)
					{
						num2 = 10;
						continue;
					}
					break;
				}
				case 14:
				{
					DocumentObjectType documentObjectType;
					switch (documentObjectType)
					{
					case DocumentObjectType.Comment:
					{
						ParagraphBase paragraphBase;
						Comment comment = (Comment)paragraphBase;
						Body body = comment.Body;
						num2 = 1;
						continue;
					}
					case DocumentObjectType.Footnote:
					{
						ParagraphBase paragraphBase;
						Footnote footnote = (Footnote)paragraphBase;
						Body body = footnote.TextBody;
						num2 = 8;
						continue;
					}
					case DocumentObjectType.TextBox:
					{
						ParagraphBase paragraphBase;
						TextBox textBox = (TextBox)paragraphBase;
						Body body = textBox.Body;
						num2 = 21;
						continue;
					}
					default:
						num2 = 2;
						continue;
					}
					break;
				}
				case 15:
				{
					Field field;
					if (field.NextSibling.NextSibling is TextRange)
					{
						num2 = 17;
						continue;
					}
					goto IL_C2;
				}
				case 16:
					num2 = 14;
					continue;
				case 17:
					num2 = 3;
					continue;
				case 18:
				{
					DocumentObjectType documentObjectType;
					if (documentObjectType != DocumentObjectType.Field)
					{
						num2 = 16;
						continue;
					}
					ParagraphBase paragraphBase;
					Field field = paragraphBase as Field;
					num2 = 0;
					continue;
				}
				case 19:
					goto IL_C2;
				case 20:
					num2 = 15;
					continue;
				case 21:
					goto IL_C2;
				}
				goto IL_AF;
				IL_C2:
				num2 = 13;
				continue;
				IL_14A:
				num2 = 7;
				continue;
				IL_AF:
				goto IL_14A;
			}
			IL_2DA:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						disposable.Dispose();
						num2 = 2;
						continue;
					case 1:
						if (disposable != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_32C;
					case 2:
						goto IL_322;
					}
					break;
				}
			}
			IL_322:
			if (true)
			{
			}
			IL_32C:;
		}
		return num;
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x001D8488 File Offset: 0x001D7488
	internal void ᜀ(TextSelection[] A_0, string A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_C5:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			TextSelection textSelection;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 5;
					continue;
				case 2:
					goto IL_D1;
				case 3:
				{
					int num2;
					if (num2 <= 0)
					{
						num = 4;
						continue;
					}
					textSelection = A_0[num2];
					textSelection.ᜄ();
					this.ᜀ(textSelection);
					num2--;
					num = 6;
					continue;
				}
				case 4:
					goto IL_EF;
				case 5:
				{
					if (true)
					{
					}
					if (A_0.Length == 0)
					{
						goto IL_C5;
					}
					int num3 = A_0.Length - 1;
					int num2 = num3;
					num = 7;
					continue;
				}
				case 6:
					goto IL_D3;
				case 7:
					goto IL_D3;
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_D3:
				num = 3;
			}
			IL_D1:
			return;
			IL_EF:
			textSelection = A_0[0];
			TextRange asOneRange = textSelection.GetAsOneRange();
			asOneRange.Text = A_1;
			return;
		}
		}
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x001D859C File Offset: 0x001D759C
	internal void ᜀ(TextSelection[] A_0, TextSelection A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (A_0.Length == 0)
					{
						num = 10;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						TextSelection textSelection = null;
						int num2 = A_0.Length - 1;
						num3 = num2;
						num = 2;
						continue;
					}
					}
					break;
				case 2:
					goto IL_97;
				case 4:
					goto IL_97;
				case 5:
					goto IL_E1;
				case 6:
				{
					TextSelection textSelection;
					Paragraph a_ = textSelection.OwnerParagraph;
					int a_2;
					A_1.ᜀ(a_, a_2, false, null);
					if (true)
					{
					}
					num = 8;
					continue;
				}
				case 7:
					num = 1;
					continue;
				case 8:
					goto IL_E1;
				case 9:
				{
					if (num3 == 0)
					{
						num = 6;
						continue;
					}
					TextSelection textSelection;
					this.ᜀ(textSelection);
					num = 5;
					continue;
				}
				case 10:
					goto IL_DF;
				case 11:
				{
					if (num3 < 0)
					{
						num = 0;
						continue;
					}
					TextSelection textSelection = A_0[num3];
					int a_2 = textSelection.ᜄ();
					num = 9;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 7;
					continue;
				}
				break;
				IL_97:
				num = 11;
				continue;
				IL_E1:
				num3--;
				num = 4;
			}
			return;
			IL_DF:
			return;
		}
		}
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x001D8704 File Offset: 0x001D7704
	internal void ᜀ(TextSelection[] A_0, TextBodyPart A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_A7;
				case 1:
					num = 7;
					continue;
				case 2:
					goto IL_F1;
				case 3:
				{
					if (num2 == 0)
					{
						num = 9;
						continue;
					}
					TextSelection textSelection;
					this.ᜀ(textSelection);
					num = 10;
					continue;
				}
				case 4:
					goto IL_A7;
				case 5:
					return;
				case 6:
					goto IL_EF;
				case 7:
					if (A_0.Length == 0)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						TextSelection textSelection = null;
						int num3 = A_0.Length - 1;
						num2 = num3;
						num = 4;
						continue;
					}
					}
					break;
				case 9:
				{
					if (true)
					{
					}
					TextSelection textSelection;
					Paragraph paragraph = textSelection.OwnerParagraph;
					int pItemIndex;
					A_1.PasteAt(paragraph.OwnerTextBody, paragraph.ឯ(), pItemIndex);
					num = 2;
					continue;
				}
				case 10:
					goto IL_F1;
				case 11:
				{
					if (num2 < 0)
					{
						num = 5;
						continue;
					}
					TextSelection textSelection = A_0[num2];
					int pItemIndex = textSelection.ᜄ();
					num = 3;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_A7:
				num = 11;
				continue;
				IL_F1:
				num2--;
				num = 0;
			}
			return;
			IL_EF:
			return;
		}
		}
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x001D887C File Offset: 0x001D787C
	private void ᜀ(TextSelection A_0)
	{
		for (;;)
		{
			IL_14:
			int num;
			Paragraph paragraph;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4C:
				if (true)
				{
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				paragraph = A_0.OwnerParagraph;
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3F;
				case 1:
					return;
				case 2:
					paragraph.RemoveSelf();
					num = 1;
					continue;
				}
				goto IL_14;
			}
			IL_3F:
			if (paragraph.Items.Count == 0)
			{
				goto IL_4C;
			}
			break;
		}
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x001D8904 File Offset: 0x001D7904
	private void ᜀ(Paragraph A_0, int A_1, int A_2, out TextRange A_3)
	{
		int num2;
		for (;;)
		{
			int num = 0;
			bool flag = false;
			A_3 = null;
			num2 = A_2;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_FE;
				case 1:
					goto IL_174;
				case 2:
					if (num > A_1)
					{
						num3 = 12;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_123;
					default:
						if (false)
						{
						}
						num3 = 5;
						continue;
					}
					break;
				case 3:
					if (num2 >= A_0.Items.Count)
					{
						num3 = 9;
						continue;
					}
					A_3 = (A_0[num2] as TextRange);
					num3 = 10;
					continue;
				case 4:
					goto IL_13A;
				case 5:
					if (num == A_1)
					{
						num3 = 7;
						continue;
					}
					goto IL_FE;
				case 6:
					goto IL_123;
				case 7:
					flag = true;
					num3 = 0;
					continue;
				case 8:
					goto IL_174;
				case 9:
					return;
				case 10:
					if (A_3 != null)
					{
						num3 = 13;
						continue;
					}
					goto IL_FE;
				case 11:
					if (flag)
					{
						num3 = 6;
						continue;
					}
					num2--;
					num2++;
					num3 = 8;
					continue;
				case 12:
					return;
				case 13:
					num = A_3.StartPos + A_3.TextLength;
					if (true)
					{
					}
					num3 = 2;
					continue;
				}
				break;
				IL_FE:
				A_0.Items.ᜀ(num2);
				num3 = 11;
				continue;
				IL_123:
				num3 = 4;
				continue;
				IL_174:
				num3 = 3;
			}
		}
		return;
		IL_13A:
		A_3 = ((num2 < A_0.Items.Count) ? (A_0[num2] as TextRange) : null);
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x001D8AB0 File Offset: 0x001D7AB0
	private void ᜀ(Paragraph A_0, int A_1, int A_2)
	{
		for (;;)
		{
			int num = A_1;
			int count = A_0.Items.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					if (true)
					{
					}
					ParagraphBase paragraphBase = A_0[num];
					paragraphBase.StartPos += A_2;
					num++;
					goto IL_84;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_84;
					default:
						goto IL_5A;
					}
					break;
				case 2:
					goto IL_30;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 0;
				continue;
				IL_84:
				num2 = 2;
			}
		}
		IL_5A:
		if (false)
		{
		}
	}

	// Token: 0x04001F7B RID: 8059
	[ThreadStatic]
	public static spr\u21D6 ᜀ;
}
