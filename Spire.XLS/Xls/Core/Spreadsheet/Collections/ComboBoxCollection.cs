using System;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000204 RID: 516
	public class ComboBoxCollection : CollectionExtended<IComboBoxShape>, IComboBoxes
	{
		// Token: 0x06001DA1 RID: 7585 RVA: 0x000FCB3C File Offset: 0x000FBB3C
		public IComboBoxShape AddComboBox(int row, int column, int height, int width)
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
			XlsComboBoxShape xlsComboBoxShape = this.ᜀ.Shapes.AddComboBox() as XlsComboBoxShape;
			sprᮋ sprᮋ = xlsComboBoxShape.ClientAnchor;
			sprᮋ.ᜇ(column - 1);
			sprᮋ.ᜆ(row - 1);
			sprᮋ.ᜂ(column);
			sprᮋ.ᜅ(row);
			sprᮋ.ᜀ(0);
			sprᮋ.ᜃ(0);
			sprᮋ.ᜁ(0);
			sprᮋ.ᜄ(0);
			xlsComboBoxShape.Width = width;
			xlsComboBoxShape.Height = height;
			xlsComboBoxShape.EvaluateTopLeftPosition();
			return xlsComboBoxShape;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000FCBE8 File Offset: 0x000FBBE8
		internal ComboBoxCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 15;
			base..ctor(A_0, A_1);
			this.ᜀ = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
			if (this.ᜀ == null)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕄♆㭈⹊⍌㭎", a_));
			}
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000FCC40 File Offset: 0x000FBC40
		public void AddComboBox(IComboBoxShape combobox)
		{
			int a_ = 19;
			while (combobox != null)
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
					base.Add(combobox);
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⩈⑊⁌ⵎ㹐ㅒ㩔⽖", a_));
		}

		// Token: 0x17000B05 RID: 2821
		internal IComboBoxShape this[string A_0]
		{
			get
			{
				switch (0)
				{
				default:
				{
					IComboBoxShape result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 5;
									continue;
								}
								IComboBoxShape comboBoxShape = base[num];
								num2 = 1;
								continue;
							}
							case 1:
							{
								IComboBoxShape comboBoxShape;
								if (comboBoxShape.Name == A_0)
								{
									num2 = 2;
									continue;
								}
								num++;
								num2 = 3;
								continue;
							}
							case 2:
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
									IComboBoxShape comboBoxShape;
									result = comboBoxShape;
									break;
								}
								}
								num2 = 6;
								continue;
							case 3:
								goto IL_B8;
							case 4:
								goto IL_B8;
							case 5:
								return result;
							case 6:
								return result;
							}
							break;
							IL_B8:
							if (true)
							{
							}
							num2 = 0;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x040010B2 RID: 4274
		private int \u2593\u008F\u009A\u0086;

		// Token: 0x040010B3 RID: 4275
		private byte[] \u2609\u009B\u008B\u00A0;

		// Token: 0x040010B4 RID: 4276
		private int \u2609\u0094\u0090\u008E;

		// Token: 0x040010B5 RID: 4277
		private string \u25D8\u00A9\u0090\u00A8;

		// Token: 0x040010B6 RID: 4278
		private long[] \u2460\u007F\u009E\u00AC;

		// Token: 0x040010B7 RID: 4279
		private int \u2609\u00A0\u0093\u0098;

		// Token: 0x040010B8 RID: 4280
		private new XlsWorksheetBase ᜀ;
	}
}
