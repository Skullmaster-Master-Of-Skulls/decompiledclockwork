using System;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200020D RID: 525
	public class RadioButtonCollection : CollectionExtended<object>, IRadioButtons
	{
		// Token: 0x06001EC9 RID: 7881 RVA: 0x00104A48 File Offset: 0x00103A48
		internal RadioButtonCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 0;
			base..ctor(A_0, A_1);
			this.ᜀ = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
			if (this.ᜀ == null)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䘵夷䠹夻倽㐿", a_));
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00104AA0 File Offset: 0x00103AA0
		public void AddRadioButton(IRadioButton RadioButton)
		{
			int a_ = 18;
			while (RadioButton == null)
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
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("❇㩉㡋❍㽏㱑瑓㑕ⵗ⹙⡛ㅝ๟", a_));
				}
			}
			this.ᜀ(RadioButton);
			base.Add(RadioButton);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00104B0C File Offset: 0x00103B0C
		internal new void ᜀ(IRadioButton A_0)
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
			RadioButton radioButton = (RadioButton)A_0;
			radioButton.InvokeEvent = true;
			radioButton.CheckStateChanged += this.ᜁ;
			radioButton.LinkedCellValueChanged += this.ᜀ;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00104B7C File Offset: 0x00103B7C
		private new void ᜁ(object A_0, XlsEventArgs A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IRadioButton radioButton = (IRadioButton)A_0;
					bool flag = false;
					int num = 6;
					for (;;)
					{
						RadioButton radioButton2;
						int num2;
						switch (num)
						{
						case 0:
							if (flag)
							{
								num = 10;
								continue;
							}
							goto IL_F0;
						case 1:
							num = 15;
							continue;
						case 2:
							goto IL_15B;
						case 3:
							if (radioButton2 != A_0)
							{
								num = 7;
								continue;
							}
							num = 0;
							continue;
						case 4:
							goto IL_F0;
						case 5:
							goto IL_1B4;
						case 6:
							if (base.Count > 0)
							{
								num = 1;
								continue;
							}
							goto IL_1B4;
						case 7:
							radioButton2.CheckState = CheckState.Unchecked;
							num = 4;
							continue;
						case 8:
						{
							int count;
							if (num2 >= count)
							{
								num = 13;
								continue;
							}
							radioButton2 = (RadioButton)this[num2];
							radioButton2.InvokeEvent = false;
							num = 3;
							continue;
						}
						case 9:
							flag = true;
							radioButton.LinkedCell = this[0].LinkedCell;
							num = 5;
							continue;
						case 10:
							IL_BF:
							num = 11;
							continue;
						case 11:
							if (radioButton.CheckState == CheckState.Checked)
							{
								num = 14;
								continue;
							}
							goto IL_F0;
						case 12:
							goto IL_F0;
						case 13:
							return;
						case 14:
							radioButton.LinkedCell.NumberValue = (double)(num2 + 1);
							num = 12;
							continue;
						case 15:
							if (this[0].LinkedCell != null)
							{
								num = 9;
								continue;
							}
							goto IL_1B4;
						case 16:
							goto IL_15B;
						}
						break;
						IL_1B4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BF;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 0;
							int count = base.Count;
							num = 16;
							continue;
						}
						}
						IL_F0:
						radioButton2.InvokeEvent = true;
						num2++;
						num = 2;
						continue;
						IL_15B:
						num = 8;
					}
				}
				return;
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00104D94 File Offset: 0x00103D94
		private new void ᜀ(object A_0, XlsEventArgs A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IRadioButton radioButton = (IRadioButton)A_0;
					IXLSRange ixlsrange = (IXLSRange)A_1.newValue;
					int num = 0;
					int count = base.Count;
					int num2 = 1;
					for (;;)
					{
						RadioButton radioButton2;
						switch (num2)
						{
						case 0:
							goto IL_F7;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13B;
							default:
								if (false)
								{
								}
								goto IL_F7;
							}
							break;
						case 2:
							goto IL_88;
						case 3:
							return;
						case 4:
							if (true)
							{
							}
							num2 = 7;
							continue;
						case 5:
							if (radioButton2.CheckState == CheckState.Checked)
							{
								num2 = 4;
								continue;
							}
							goto IL_88;
						case 6:
							goto IL_13B;
						case 7:
							if (ixlsrange != null)
							{
								num2 = 6;
								continue;
							}
							goto IL_88;
						case 8:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							radioButton2 = (RadioButton)this[num];
							radioButton2.InvokeEvent = false;
							radioButton2.LinkedCell = ixlsrange;
							num2 = 5;
							continue;
						}
						break;
						IL_88:
						radioButton2.InvokeEvent = true;
						num++;
						num2 = 0;
						continue;
						IL_F7:
						num2 = 8;
						continue;
						IL_13B:
						ixlsrange.NumberValue = (double)(num + 1);
						num2 = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00104EE4 File Offset: 0x00103EE4
		internal new void ᜀ()
		{
			switch (0)
			{
			default:
			{
				List<RadioButtonCollection> list = this.ᜁ();
				List<RadioButtonCollection>.Enumerator enumerator = list.GetEnumerator();
				try
				{
					int num = 5;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
						{
							RadioButtonCollection radioButtonCollection;
							RadioButton radioButton = radioButtonCollection[num2 - 1] as RadioButton;
							radioButton.InvokeEvent = false;
							radioButton.CheckState = CheckState.Checked;
							radioButton.InvokeEvent = true;
							num = 6;
							continue;
						}
						case 1:
							num = 7;
							continue;
						case 2:
							num = 9;
							continue;
						case 3:
							if (num2 == 0)
							{
								num = 12;
								continue;
							}
							num = 10;
							continue;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							RadioButtonCollection radioButtonCollection = enumerator.Current;
							IXLSRange linkedCell = radioButtonCollection[0].LinkedCell;
							num = 11;
							continue;
						}
						case 7:
							goto IL_2B8;
						case 8:
						{
							RadioButtonCollection radioButtonCollection;
							num3 = radioButtonCollection.Count + 1;
							goto IL_211;
						}
						case 9:
						{
							RadioButtonCollection radioButtonCollection;
							if (num2 <= radioButtonCollection.Count)
							{
								num = 0;
								continue;
							}
							break;
						}
						case 10:
							if (num2 > 0)
							{
								num = 2;
								continue;
							}
							break;
						case 11:
						{
							IXLSRange linkedCell;
							if (linkedCell == null)
							{
								num = 13;
								continue;
							}
							num = 15;
							continue;
						}
						case 12:
						{
							RadioButtonCollection radioButtonCollection;
							IEnumerator<object> enumerator2 = radioButtonCollection.GetEnumerator();
							num = 14;
							continue;
						}
						case 13:
							num = 8;
							continue;
						case 14:
							try
							{
								num = 3;
								for (;;)
								{
									IEnumerator<object> enumerator2;
									switch (num)
									{
									case 0:
										goto IL_1CC;
									case 1:
										if (!enumerator2.MoveNext())
										{
											num = 2;
											continue;
										}
										goto IL_156;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_156;
										default:
											if (false)
											{
											}
											num = 0;
											continue;
										}
										break;
									}
									goto IL_154;
									IL_156:
									RadioButton radioButton2 = (RadioButton)enumerator2.Current;
									radioButton2.InvokeEvent = false;
									radioButton2.CheckState = CheckState.Unchecked;
									radioButton2.InvokeEvent = true;
									num = 4;
									continue;
									IL_187:
									num = 1;
									continue;
									IL_154:
									goto IL_187;
								}
								IL_1CC:
								break;
							}
							finally
							{
								num = 0;
								for (;;)
								{
									IEnumerator<object> enumerator2;
									switch (num)
									{
									case 1:
										enumerator2.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_20E;
									}
									if (enumerator2 == null)
									{
										break;
									}
									num = 1;
								}
								IL_20E:;
							}
							goto IL_211;
						case 15:
						{
							IXLSRange linkedCell;
							num3 = (int)linkedCell.NumberValue;
							goto IL_211;
						}
						}
						IL_105:
						num = 4;
						continue;
						goto IL_105;
						IL_211:
						num2 = num3;
						num = 3;
					}
					IL_2B8:;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return;
			}
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x001051F8 File Offset: 0x001041F8
		internal new List<RadioButtonCollection> ᜁ()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				IL_3C:
				List<RadioButtonCollection> list = new List<RadioButtonCollection>();
				int num = -1;
				RadioButtonCollection radioButtonCollection = new RadioButtonCollection(base.AppImplementation, base.Parent);
				IEnumerator<object> enumerator = base.GetEnumerator();
				try
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							num2 = 8;
							continue;
						case 3:
						{
							RadioButton radioButton;
							if (num == radioButton.NextButtonId)
							{
								num2 = 5;
								continue;
							}
							break;
						}
						case 4:
						{
							RadioButton radioButton;
							num = radioButton.Index;
							num2 = 0;
							continue;
						}
						case 5:
							num = -1;
							list.Add(radioButtonCollection);
							radioButtonCollection = new RadioButtonCollection(base.AppImplementation, base.Parent);
							num2 = 7;
							continue;
						case 6:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 1;
								continue;
							}
							RadioButton radioButton = (RadioButton)enumerator.Current;
							radioButtonCollection.Add(radioButton);
							num2 = 9;
							continue;
						}
						case 8:
							goto IL_158;
						case 9:
							if (num == -1)
							{
								num2 = 4;
								continue;
							}
							num2 = 3;
							continue;
						}
						IL_AE:
						num2 = 6;
						continue;
						goto IL_AE;
					}
					IL_158:;
				}
				finally
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							enumerator.Dispose();
							num2 = 2;
							continue;
						case 2:
							goto IL_197;
						}
						if (enumerator == null)
						{
							break;
						}
						num2 = 1;
					}
					IL_197:;
				}
				return list;
			}
			}
		}

		// Token: 0x17000B59 RID: 2905
		public IRadioButton this[int index]
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
				return base.List[index] as IRadioButton;
			}
		}

		// Token: 0x17000B5A RID: 2906
		public IRadioButton this[string name]
		{
			get
			{
				switch (0)
				{
				default:
				{
					IRadioButton result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (true)
								{
								}
								goto IL_C3;
							case 1:
							{
								IRadioButton radioButton;
								if (radioButton.Name == name)
								{
									num2 = 4;
									continue;
								}
								num++;
								goto IL_5F;
							}
							case 2:
								goto IL_C3;
							case 3:
								return result;
							case 4:
							{
								IRadioButton radioButton;
								result = radioButton;
								num2 = 3;
								continue;
							}
							case 5:
								return result;
							case 6:
							{
								if (num >= count)
								{
									num2 = 5;
									continue;
								}
								IRadioButton radioButton = this[num];
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_5F;
								default:
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								break;
							}
							}
							break;
							IL_5F:
							num2 = 2;
							continue;
							IL_C3:
							num2 = 6;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x001054EC File Offset: 0x001044EC
		public IRadioButton Add(int row, int column, int height, int width)
		{
			RadioButton radioButton;
			for (;;)
			{
				IL_14:
				radioButton = (this.ᜀ.Shapes.AddRadioButton() as RadioButton);
				sprᮋ sprᮋ = radioButton.ClientAnchor;
				sprᮋ.ᜇ(column - 1);
				sprᮋ.ᜆ(row - 1);
				sprᮋ.ᜂ(column);
				sprᮋ.ᜅ(row);
				sprᮋ.ᜀ(0);
				sprᮋ.ᜃ(0);
				sprᮋ.ᜁ(0);
				sprᮋ.ᜄ(0);
				radioButton.Workbook.CreateFont();
				radioButton.Fill.BackColor = spr\u1D39.ᜂ;
				radioButton.Fill.ForeColor = spr\u1D39.ᜂ;
				radioButton.Line.BackColor = spr\u1D39.ᜁ;
				radioButton.HasLineFormat = false;
				radioButton.Width = width;
				radioButton.Height = height;
				for (;;)
				{
					IL_C9:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							radioButton.IsFirstButton = true;
							if (true)
							{
							}
							num = 1;
							continue;
						case 1:
							return radioButton;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C9;
							default:
								if (false)
								{
								}
								if (base.Count == 1)
								{
									num = 0;
									continue;
								}
								return radioButton;
							}
							break;
						}
						goto IL_14;
					}
				}
			}
			return radioButton;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0010561C File Offset: 0x0010461C
		public IRadioButton Add()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.Add(10, 10, 20, 140);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00105668 File Offset: 0x00104668
		public IRadioButton Add(int row, int column)
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
			return this.Add(row, column, 20, 140);
		}

		// Token: 0x040010CD RID: 4301
		private int \u25D9\u0085\u00A9\u00A1;

		// Token: 0x040010CE RID: 4302
		private bool \u25D8\u00AF\u009Dª;

		// Token: 0x040010CF RID: 4303
		public const int AverageWidth = 140;

		// Token: 0x040010D0 RID: 4304
		private long[] \u2609\u007F\u00A7\u0096;

		// Token: 0x040010D1 RID: 4305
		public const int AverageHeight = 20;

		// Token: 0x040010D2 RID: 4306
		private long[] \u25D9\u00AF\u0095\u0089;

		// Token: 0x040010D3 RID: 4307
		private int[] \u25D9\u008F\u00A2\u0083;

		// Token: 0x040010D4 RID: 4308
		private long[] \u2593\u009F\u008D\u00AE;

		// Token: 0x040010D5 RID: 4309
		private new XlsWorksheetBase ᜀ;
	}
}
