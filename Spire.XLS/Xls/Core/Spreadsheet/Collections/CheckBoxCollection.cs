using System;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000212 RID: 530
	public class CheckBoxCollection : CollectionExtended<object>, ICheckBoxes
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x00105A98 File Offset: 0x00104A98
		internal CheckBoxCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 15;
			base..ctor(A_0, A_1);
			this.ᜀ = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
			if (this.ᜀ == null)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕄♆㭈⹊⍌㭎", a_));
			}
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00105AF0 File Offset: 0x00104AF0
		public void AddCheckBox(ICheckBoxShape checkbox)
		{
			int a_ = 1;
			if (checkbox == null)
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_36;
					}
				}
				IL_36:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("吶儸帺帼吾⍀ⱂ㵄", a_));
			}
			base.Add(checkbox);
		}

		// Token: 0x17000B62 RID: 2914
		public ICheckBoxShape this[int index]
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
				return base.List[index] as ICheckBoxShape;
			}
		}

		// Token: 0x17000B63 RID: 2915
		internal ICheckBoxShape this[string A_0]
		{
			get
			{
				ICheckBoxShape result;
				for (;;)
				{
					IL_00:
					switch (0)
					{
					default:
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
									ICheckBoxShape checkBoxShape;
									if (checkBoxShape.Name == A_0)
									{
										num2 = 2;
										continue;
									}
									num++;
									num2 = 6;
									continue;
								}
								case 1:
									return result;
								case 2:
								{
									ICheckBoxShape checkBoxShape;
									result = checkBoxShape;
									num2 = 1;
									continue;
								}
								case 3:
								{
									if (num >= count)
									{
										num2 = 5;
										continue;
									}
									ICheckBoxShape checkBoxShape = this[num];
									num2 = 0;
									continue;
								}
								case 4:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_00;
									default:
										if (false)
										{
										}
										goto IL_BB;
									}
									break;
								case 5:
									return result;
								case 6:
									goto IL_BB;
								}
								break;
								IL_BB:
								if (true)
								{
								}
								num2 = 3;
							}
						}
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00105C90 File Offset: 0x00104C90
		public ICheckBoxShape AddCheckBox(int row, int column, int height, int width)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			sprថ sprថ = this.ᜀ.Shapes.AddCheckBox() as sprថ;
			sprᮋ sprᮋ = sprថ.ClientAnchor;
			sprᮋ.ᜇ(column - 1);
			sprᮋ.ᜆ(row - 1);
			sprᮋ.ᜂ(column);
			sprᮋ.ᜅ(row);
			sprᮋ.ᜀ(0);
			sprᮋ.ᜃ(0);
			sprᮋ.ᜁ(0);
			sprᮋ.ᜄ(0);
			sprថ.Fill.BackColor = spr\u1D39.ᜂ;
			sprថ.Line.BackColor = spr\u1D39.ᜁ;
			sprថ.HasLineFormat = false;
			sprថ.HasFill = false;
			sprថ.Width = width;
			sprថ.Height = height;
			return sprថ;
		}

		// Token: 0x040010DC RID: 4316
		private bool \u2593\u0081\u00AF\u00A6;

		// Token: 0x040010DD RID: 4317
		private long[] \u25D8\u008F\u0084\u0083;

		// Token: 0x040010DE RID: 4318
		private bool \u25D9\u0086\u00A1\u0093;

		// Token: 0x040010DF RID: 4319
		private new XlsWorksheetBase ᜀ;
	}
}
