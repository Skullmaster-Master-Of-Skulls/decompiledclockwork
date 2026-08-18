using System;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200020F RID: 527
	public class TextBoxCollection : CollectionExtended<ITextBoxShape>, ITextBoxes
	{
		// Token: 0x06001EDB RID: 7899 RVA: 0x001056B4 File Offset: 0x001046B4
		internal TextBoxCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 8;
			base..ctor(A_0, A_1);
			this.ᜀ = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
			if (this.ᜀ == null)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("丽ℿぁ⅃⡅㱇", a_));
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0010570C File Offset: 0x0010470C
		public void AddTextBox(ITextBoxShape textbox)
		{
			int a_ = 19;
			if (textbox == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("㵈⹊㕌㭎㍐㱒ⵔ", a_));
				}
			}
			base.Add(textbox);
		}

		// Token: 0x17000B5E RID: 2910
		public new ITextBoxShape this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.List[index];
			}
		}

		// Token: 0x17000B5F RID: 2911
		public ITextBoxShape this[string name]
		{
			get
			{
				switch (0)
				{
				default:
				{
					ITextBoxShape result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								ITextBoxShape textBoxShape = this[num];
								num2 = 4;
								continue;
							}
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_47;
								default:
									if (false)
									{
									}
									goto IL_BB;
								}
								break;
							case 2:
								goto IL_47;
							case 3:
								return result;
							case 4:
							{
								ITextBoxShape textBoxShape;
								if (textBoxShape.Name == name)
								{
									num2 = 5;
									continue;
								}
								num++;
								num2 = 1;
								continue;
							}
							case 5:
							{
								ITextBoxShape textBoxShape;
								result = textBoxShape;
								num2 = 6;
								continue;
							}
							case 6:
								return result;
							}
							break;
							IL_BB:
							if (true)
							{
							}
							num2 = 0;
							continue;
							IL_47:
							goto IL_BB;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x001058A8 File Offset: 0x001048A8
		public ITextBoxShape AddTextBox(int row, int column, int height, int width)
		{
			switch (0)
			{
			default:
			{
				XlsTextBoxShape xlsTextBoxShape;
				for (;;)
				{
					xlsTextBoxShape = (this.ᜀ.Shapes.AddTextBox() as XlsTextBoxShape);
					sprᮋ sprᮋ = xlsTextBoxShape.ClientAnchor;
					sprᮋ.ᜇ(column - 1);
					sprᮋ.ᜆ(row - 1);
					sprᮋ.ᜂ(column);
					sprᮋ.ᜅ(row);
					sprᮋ.ᜀ(0);
					sprᮋ.ᜃ(0);
					sprᮋ.ᜁ(0);
					sprᮋ.ᜄ(0);
					xlsTextBoxShape.Width = width;
					xlsTextBoxShape.Height = height;
					XlsChart xlsChart = base.Parent as XlsChart;
					int num = 1;
					for (;;)
					{
						sprᾹ sprᾹ;
						sprᾹ sprᾹ2;
						switch (num)
						{
						case 0:
							sprᾹ = xlsChart.SecondaryParentAxis;
							goto IL_19A;
						case 1:
							if (xlsChart != null)
							{
								num = 3;
								continue;
							}
							return xlsTextBoxShape;
						case 2:
							num = 0;
							continue;
						case 3:
							num = 9;
							continue;
						case 4:
						{
							sprᶓ sprᶓ;
							sprᶓ.ᜁ(328);
							sprᶓ.ᜂ(243);
							sprᶓ.ᜀ(3125);
							sprᶓ.ᜃ(3283);
							num = 6;
							continue;
						}
						case 5:
						{
							sprᶓ sprᶓ = sprᾹ2.ᜈ();
							num = 10;
							continue;
						}
						case 6:
							return xlsTextBoxShape;
						case 7:
							sprᾹ = xlsChart.PrimaryParentAxis;
							goto IL_19A;
						case 8:
							if (sprᾹ2 == null)
							{
								return xlsTextBoxShape;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F7;
							default:
								if (false)
								{
								}
								num = 5;
								continue;
							}
							break;
						case 9:
							if (xlsChart.PrimaryParentAxis == null)
							{
								num = 2;
								continue;
							}
							num = 7;
							continue;
						case 10:
						{
							sprᶓ sprᶓ;
							if (sprᶓ.ᜄ() == 0)
							{
								goto IL_F7;
							}
							return xlsTextBoxShape;
						}
						}
						break;
						IL_F7:
						num = 4;
						continue;
						IL_19A:
						sprᾹ2 = sprᾹ;
						num = 8;
					}
				}
				return xlsTextBoxShape;
			}
			}
		}

		// Token: 0x040010D6 RID: 4310
		private long \u25D8\u00AB\u00A7\u0094;

		// Token: 0x040010D7 RID: 4311
		private byte[] \u2460\u00A3\u008B\u009D;

		// Token: 0x040010D8 RID: 4312
		private byte \u25D8\u00AB\u00A8\u0093;

		// Token: 0x040010D9 RID: 4313
		private int[] \u2593\u00A4\u008C\u008A;

		// Token: 0x040010DA RID: 4314
		private byte \u25D9\u00B0\u008C\u00A6;

		// Token: 0x040010DB RID: 4315
		private new XlsWorksheetBase ᜀ;
	}
}
