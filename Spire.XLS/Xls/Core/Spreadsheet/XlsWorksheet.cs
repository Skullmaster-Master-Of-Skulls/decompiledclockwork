using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Spire.Xls.Calculation;
using Spire.Xls.Collections;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.Tables;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200004D RID: 77
	public class XlsWorksheet : XlsWorksheetBase, spr\u252A, spr\u1D46, ICloneParent, IInternalWorksheet
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000549 RID: 1353 RVA: 0x0002AFE0 File Offset: 0x00029FE0
		// (remove) Token: 0x0600054A RID: 1354 RVA: 0x0002B074 File Offset: 0x0002A074
		public event XlsRange.CellValueChangedEventHandler CellValueChanged
		{
			add
			{
				for (;;)
				{
					IL_42:
					XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler = this.ᜇ;
					int num = 1;
					for (;;)
					{
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
							if (true)
							{
							}
							XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler2;
							switch (num)
							{
							case 0:
								if (cellValueChangedEventHandler == cellValueChangedEventHandler2)
								{
									goto IL_7C;
								}
								goto IL_53;
							case 1:
								goto IL_53;
							case 2:
								return;
							}
							goto IL_42;
							IL_53:
							cellValueChangedEventHandler2 = cellValueChangedEventHandler;
							XlsRange.CellValueChangedEventHandler value2 = (XlsRange.CellValueChangedEventHandler)Delegate.Combine(cellValueChangedEventHandler2, value);
							cellValueChangedEventHandler = Interlocked.CompareExchange<XlsRange.CellValueChangedEventHandler>(ref this.ᜇ, value2, cellValueChangedEventHandler2);
							num = 0;
							continue;
						}
						}
						IL_7C:
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					IL_42:
					XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler = this.ᜇ;
					int num = 0;
					for (;;)
					{
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
							if (true)
							{
							}
							XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_53;
							case 1:
								return;
							case 2:
								if (cellValueChangedEventHandler == cellValueChangedEventHandler2)
								{
									goto IL_7C;
								}
								goto IL_53;
							}
							goto IL_42;
							IL_53:
							cellValueChangedEventHandler2 = cellValueChangedEventHandler;
							XlsRange.CellValueChangedEventHandler value2 = (XlsRange.CellValueChangedEventHandler)Delegate.Remove(cellValueChangedEventHandler2, value);
							cellValueChangedEventHandler = Interlocked.CompareExchange<XlsRange.CellValueChangedEventHandler>(ref this.ᜇ, value2, cellValueChangedEventHandler2);
							num = 2;
							continue;
						}
						}
						IL_7C:
						num = 1;
					}
				}
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600054B RID: 1355 RVA: 0x0002B108 File Offset: 0x0002A108
		// (remove) Token: 0x0600054C RID: 1356 RVA: 0x0002B19C File Offset: 0x0002A19C
		public event XlsWorksheet.ErrorFunctionEventHandler MissingFunction
		{
			add
			{
				for (;;)
				{
					IL_3A:
					XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler = this.ᜋ;
					int num = 0;
					for (;;)
					{
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
							XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_4B;
							case 1:
								return;
							case 2:
								if (errorFunctionEventHandler == errorFunctionEventHandler2)
								{
									goto IL_74;
								}
								goto IL_4B;
							}
							goto IL_3A;
							IL_4B:
							errorFunctionEventHandler2 = errorFunctionEventHandler;
							XlsWorksheet.ErrorFunctionEventHandler value2 = (XlsWorksheet.ErrorFunctionEventHandler)Delegate.Combine(errorFunctionEventHandler2, value);
							errorFunctionEventHandler = Interlocked.CompareExchange<XlsWorksheet.ErrorFunctionEventHandler>(ref this.ᜋ, value2, errorFunctionEventHandler2);
							num = 2;
							continue;
						}
						}
						IL_74:
						if (true)
						{
						}
						num = 1;
					}
				}
			}
			remove
			{
				for (;;)
				{
					IL_3A:
					XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler = this.ᜋ;
					int num = 0;
					for (;;)
					{
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
							XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_4B;
							case 1:
								return;
							case 2:
								if (errorFunctionEventHandler == errorFunctionEventHandler2)
								{
									goto IL_74;
								}
								goto IL_4B;
							}
							goto IL_3A;
							IL_4B:
							errorFunctionEventHandler2 = errorFunctionEventHandler;
							XlsWorksheet.ErrorFunctionEventHandler value2 = (XlsWorksheet.ErrorFunctionEventHandler)Delegate.Remove(errorFunctionEventHandler2, value);
							errorFunctionEventHandler = Interlocked.CompareExchange<XlsWorksheet.ErrorFunctionEventHandler>(ref this.ᜋ, value2, errorFunctionEventHandler2);
							num = 2;
							continue;
						}
						}
						IL_74:
						if (true)
						{
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002B230 File Offset: 0x0002A230
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0002B274 File Offset: 0x0002A274
		public FormulaEngine FormulaEngine
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
				return this.ᜈ;
			}
			set
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
				this.ᜈ = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0002B2B8 File Offset: 0x0002A2B8
		public bool HasSheetCalculation
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
				return this.ᜉ;
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002B2FC File Offset: 0x0002A2FC
		public void EnableSheetEnvalution(bool enabled)
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
				if (!enabled)
				{
					this.ᜑ();
					return;
				}
				if (true)
				{
				}
				break;
			}
			this.\u1713();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0002B34C File Offset: 0x0002A34C
		internal void \u1713()
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					IEnumerator enumerator;
					Hashtable hashtable;
					Hashtable hashtable2;
					switch (num)
					{
					case 0:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_10D;
								case 1:
								{
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									IWorksheet worksheet = (IWorksheet)enumerator.Current;
									worksheet.FormulaEngine.ᜀ.ᜀ(hashtable);
									num = 4;
									continue;
								}
								case 3:
									num = 0;
									continue;
								}
								IL_E7:
								num = 1;
								continue;
								goto IL_E7;
							}
							IL_10D:
							goto IL_2D5;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_15A;
									case 2:
										goto IL_158;
									}
									break;
								}
							}
							IL_158:
							IL_15A:;
						}
						goto Block_3;
						IL_2D5:
						this.ᜉ = true;
						num = 6;
						continue;
					case 1:
						if (hashtable2 != null)
						{
							num = 8;
							continue;
						}
						goto IL_2B2;
					case 2:
						goto IL_611;
					case 3:
						goto IL_42F;
					case 4:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 2;
									continue;
								case 1:
									goto IL_3D3;
								case 2:
									goto IL_3E1;
								case 4:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 0;
										continue;
									}
									string text = (string)enumerator2.Current;
									hashtable.Add(text.ToUpper(CultureInfo.InvariantCulture), hashtable2[text]);
									num = 1;
									continue;
								}
								}
								goto IL_363;
								IL_381:
								num = 4;
								continue;
								IL_363:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_3D3:
									goto IL_381;
								default:
									if (false)
									{
									}
									goto IL_381;
								}
							}
							IL_3E1:
							goto IL_2B2;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable2 = enumerator2 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable2 != null)
										{
											num = 1;
											continue;
										}
										goto IL_42E;
									case 1:
										disposable2.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_42C;
									}
									break;
								}
							}
							IL_42C:
							IL_42E:;
						}
						goto Block_6;
					case 6:
						return;
					case 7:
						goto IL_15B;
					case 8:
					{
						IEnumerator enumerator2 = hashtable2.Keys.GetEnumerator();
						if (true)
						{
						}
						num = 4;
						continue;
					}
					}
					if (this.FormulaEngine == null)
					{
						num = 2;
						continue;
					}
					break;
					IL_2B2:
					enumerator = base.ParentWorkbook.Worksheets.GetEnumerator();
					num = 0;
					continue;
					Block_3:
					IEnumerator enumerator3;
					int a_2;
					string text2;
					try
					{
						IL_15B:
						num = 0;
						for (;;)
						{
							IWorksheet worksheet2;
							switch (num)
							{
							case 1:
								goto IL_1AF;
							case 2:
								num = 3;
								continue;
							case 3:
								goto IL_264;
							case 4:
								if (worksheet2.FormulaEngine == null)
								{
									num = 7;
									continue;
								}
								goto IL_1AF;
							case 6:
								if (!enumerator3.MoveNext())
								{
									num = 2;
									continue;
								}
								worksheet2 = (IWorksheet)enumerator3.Current;
								num = 4;
								continue;
							case 7:
								worksheet2.FormulaEngine = new FormulaEngine(worksheet2);
								num = 1;
								continue;
							}
							IL_18F:
							num = 6;
							continue;
							goto IL_18F;
							IL_1AF:
							this.FormulaEngine.ᜀ.ᜀ(worksheet2.Name, worksheet2, a_2);
							worksheet2.FormulaEngine.ᜀ.ᜁ(new spr\u21C1(this.ᜀ));
							text2 = text2 + worksheet2.Name + RecordTableEnumerator.b("晆", a_);
							num = 5;
						}
						IL_264:
						goto IL_314;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable3 = enumerator3 as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable3 != null)
									{
										num = 1;
										continue;
									}
									goto IL_2B1;
								case 1:
									disposable3.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_2AF;
								}
								break;
							}
						}
						IL_2AF:
						IL_2B1:;
					}
					goto IL_2B2;
					IL_314:
					hashtable2 = new Hashtable();
					IEnumerator enumerator4 = base.ParentWorkbook.Names.GetEnumerator();
					num = 3;
					continue;
					Block_6:
					try
					{
						IL_42F:
						num = 5;
						for (;;)
						{
							INamedRange namedRange;
							switch (num)
							{
							case 0:
								goto IL_5C3;
							case 1:
								if (!enumerator4.MoveNext())
								{
									num = 3;
									continue;
								}
								namedRange = (INamedRange)enumerator4.Current;
								num = 4;
								continue;
							case 2:
								hashtable2.Add((namedRange.Scope + RecordTableEnumerator.b("晆", a_) + namedRange.Name).ToUpper(), namedRange.Value.Replace(RecordTableEnumerator.b("恆", a_), ""));
								num = 7;
								continue;
							case 3:
								num = 0;
								continue;
							case 4:
								if (namedRange.Scope.Length > 0)
								{
									num = 8;
									continue;
								}
								goto IL_575;
							case 8:
								num = 9;
								continue;
							case 9:
								if (text2.IndexOf(RecordTableEnumerator.b("晆", a_) + namedRange.Scope + RecordTableEnumerator.b("晆", a_)) > -1)
								{
									num = 2;
									continue;
								}
								goto IL_575;
							}
							IL_4B6:
							num = 1;
							continue;
							goto IL_4B6;
							IL_575:
							hashtable2.Add(namedRange.Name.ToUpper(), namedRange.Value.Replace(RecordTableEnumerator.b("恆", a_), ""));
							num = 6;
						}
						IL_5C3:
						goto IL_2ED;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable4 = enumerator4 as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_60E;
								case 1:
									if (disposable4 != null)
									{
										num = 2;
										continue;
									}
									goto IL_610;
								case 2:
									disposable4.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_60E:
						IL_610:;
					}
					goto IL_611;
					IL_2ED:
					hashtable = new Hashtable();
					num = 1;
					continue;
					IL_611:
					this.FormulaEngine = new FormulaEngine(this);
					this.FormulaEngine.ᜀ.\u170D(true);
					a_2 = FormulaEngine.ᜁ();
					text2 = RecordTableEnumerator.b("晆", a_);
					enumerator3 = base.ParentWorkbook.Worksheets.GetEnumerator();
					num = 7;
				}
				return;
			}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002BA24 File Offset: 0x0002AA24
		private new void ᜀ(object A_0, spr\u2623 A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsWorksheet.ErrorFunctionEventArgs errorFunctionEventArgs = new XlsWorksheet.ErrorFunctionEventArgs();
					errorFunctionEventArgs.FunctionName = A_1.ᜁ();
					errorFunctionEventArgs.CellRange = A_1.ᜀ();
					this.ᜋ(this, errorFunctionEventArgs);
					num = 3;
					continue;
				}
				case 2:
					if (this.FormulaEngine == null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 3:
					return;
				case 4:
					goto IL_37;
				}
				if (this.ᜋ != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_37:
				num = 2;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0002BAEC File Offset: 0x0002AAEC
		internal void ᜑ()
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					IEnumerator enumerator = base.ParentWorkbook.Worksheets.GetEnumerator();
					num = 3;
					continue;
				}
				case 1:
					if (base.ParentWorkbook != null)
					{
						num = 6;
						continue;
					}
					return;
				case 2:
					num = 1;
					continue;
				case 3:
					try
					{
						num = 0;
						for (;;)
						{
							IWorksheet worksheet;
							switch (num)
							{
							case 1:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								worksheet = (IWorksheet)enumerator.Current;
								num = 6;
								continue;
							}
							case 2:
								worksheet.FormulaEngine.ᜀ.ᜀ(new spr\u21C1(this.ᜀ));
								worksheet.FormulaEngine.Dispose();
								num = 5;
								continue;
							case 3:
								num = 4;
								continue;
							case 4:
								goto IL_1BB;
							case 5:
								goto IL_199;
							case 6:
								if (worksheet.FormulaEngine != null)
								{
									num = 2;
									continue;
								}
								goto IL_199;
							}
							IL_122:
							num = 1;
							continue;
							goto IL_122;
							IL_199:
							worksheet.FormulaEngine = null;
							num = 7;
						}
						IL_1BB:
						goto IL_A4;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable = enumerator as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									disposable.Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_1FE;
								case 2:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_200;
								}
								break;
							}
						}
						IL_1FE:
						IL_200:;
					}
					return;
					IL_A4:
					this.ᜉ = false;
					num = 4;
					continue;
				case 4:
					return;
				case 5:
					if (base.ParentWorkbook.Worksheets != null)
					{
						num = 0;
						continue;
					}
					return;
				case 6:
					goto IL_A2;
				case 7:
					if (true)
					{
					}
					break;
				}
				if (this.FormulaEngine == null)
				{
					break;
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
					num = 2;
					continue;
				}
				IL_A2:
				num = 5;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002BD0C File Offset: 0x0002AD0C
		public object GetCaculateValue(int row, int col)
		{
			IXLSRange ixlsrange;
			for (;;)
			{
				ixlsrange = this[row, col];
				if (!ixlsrange.HasFormula)
				{
					goto IL_48;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_27;
				}
			}
			IL_27:
			if (true)
			{
			}
			if (false)
			{
			}
			return ixlsrange.Formula;
			IL_48:
			return ixlsrange.Value;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002BD68 File Offset: 0x0002AD68
		public void SetCaculateValue(object value, int row, int col)
		{
			int num = 2;
			for (;;)
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
					switch (num)
					{
					case 0:
						this.SetValue(row, col, value.ToString());
						goto IL_5B;
					case 1:
						return;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				IL_5B:
				num = 1;
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002BDE4 File Offset: 0x0002ADE4
		internal void ᜯ()
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
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000557 RID: 1367 RVA: 0x0002BE20 File Offset: 0x0002AE20
		// (remove) Token: 0x06000558 RID: 1368 RVA: 0x0002BEB8 File Offset: 0x0002AEB8
		public event ValueChangedEventHandler CaculateValueChanged
		{
			add
			{
				for (;;)
				{
					ValueChangedEventHandler valueChangedEventHandler = this.ᜌ;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 0;
						break;
					}
					for (;;)
					{
						ValueChangedEventHandler valueChangedEventHandler2;
						switch (num)
						{
						case 0:
							goto IL_49;
						case 1:
							if (valueChangedEventHandler == valueChangedEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_49;
						case 2:
							return;
						}
						break;
						IL_49:
						valueChangedEventHandler2 = valueChangedEventHandler;
						ValueChangedEventHandler value2 = (ValueChangedEventHandler)Delegate.Combine(valueChangedEventHandler2, value);
						valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜌ, value2, valueChangedEventHandler2);
						num = 1;
					}
				}
			}
			remove
			{
				if (true)
				{
				}
				for (;;)
				{
					ValueChangedEventHandler valueChangedEventHandler = this.ᜌ;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
					for (;;)
					{
						ValueChangedEventHandler valueChangedEventHandler2;
						switch (num)
						{
						case 0:
							goto IL_49;
						case 1:
							return;
						case 2:
							if (valueChangedEventHandler == valueChangedEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_49;
						}
						break;
						IL_49:
						valueChangedEventHandler2 = valueChangedEventHandler;
						ValueChangedEventHandler value2 = (ValueChangedEventHandler)Delegate.Remove(valueChangedEventHandler2, value);
						valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜌ, value2, valueChangedEventHandler2);
						num = 2;
					}
				}
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002BF4C File Offset: 0x0002AF4C
		public void OnCaculateValueChanged(int row, int col, string value)
		{
			int num = 1;
			for (;;)
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
					switch (num)
					{
					case 0:
						return;
					case 2:
					{
						ValueChangedEventArgs e = new ValueChangedEventArgs(row, col, value);
						this.ᜌ(this, e);
						goto IL_60;
					}
					}
					if (this.ᜌ != null)
					{
						num = 2;
						continue;
					}
					return;
				}
				IL_60:
				if (true)
				{
				}
				num = 0;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002BFD8 File Offset: 0x0002AFD8
		static XlsWorksheet()
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
			XlsWorksheet.\u170D = new TBIFFRecord[]
			{
				TBIFFRecord.AutoFilter,
				TBIFFRecord.AutoFilterInfo,
				TBIFFRecord.FilterMode
			};
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002C038 File Offset: 0x0002B038
		internal XlsWorksheet(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002C0A0 File Offset: 0x0002B0A0
		internal XlsWorksheet(spr\u1DF5 A_0, object A_1, sprἛ A_2, ExcelParseOptions A_3, bool A_4, Dictionary<int, int> A_5, IDecryptor A_6) : base(A_0, A_1, A_2, A_3, A_4, A_5, A_6)
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002C114 File Offset: 0x0002B114
		protected override void InitializeCollections()
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
			base.InitializeCollections();
			this.\u171E = new XlsName.NameIndexChangedEventHandler(this.ᜀ);
			spr\u2158 spr_u = (spr\u2158)base.ReservedHandle;
			this.ᜏ = new XlsCellRecordCollection(spr_u, this);
			this.\u1712 = new PageSetup(spr_u, this);
			this.\u1718 = new spr\u25EF(spr_u, this);
			this.ᜠ = new AutoFiltersCollection(spr_u, this);
			this.ᜪ = new WorksheetConditionalFormats(spr_u, this);
			this.ᜮ = new spr\u2622(base.AppImplementation, this);
			this.ᜐ = new spr\u216E[this.m_book.MaxColumnCount + 2];
			this.\u173D = base.AppImplementation.ᜣ();
			base.Index = this.m_book.Worksheets.Count;
			this.\u1716 = new List<spr\u21A4>();
			this.DefaultColumnWidth = spr_u.\u1713();
			this.DefaultRowHeight = spr_u.\u1718();
			this.StandardHeightFlag = spr_u.ᜰ();
			this.AttachEvents();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002C23C File Offset: 0x0002B23C
		protected void ClearAll()
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
			this.ClearAll(WorksheetCopyType.CopyAll);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002C284 File Offset: 0x0002B284
		internal override void ClearAll(WorksheetCopyType flags)
		{
			for (;;)
			{
				this.ᜏ.Clear();
				this.\u1716.Clear();
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A3;
					case 1:
						this.ᜢ.Clear();
						num = 5;
						continue;
					case 2:
						if (this.ᜠ != null)
						{
							num = 8;
							continue;
						}
						goto IL_80;
					case 3:
						if (this.ᜫ != null)
						{
							goto IL_141;
						}
						return;
					case 4:
						if (this.ᜢ != null)
						{
							num = 1;
							continue;
						}
						goto IL_12E;
					case 5:
						goto IL_12E;
					case 6:
						goto IL_80;
					case 7:
						this.\u1718.ᜂ();
						num = 0;
						continue;
					case 8:
						this.ᜠ.Clear();
						num = 6;
						continue;
					case 9:
						return;
					case 10:
						this.ᜫ.Clear();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_141;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 11:
						if ((flags & WorksheetCopyType.CopyNames) != WorksheetCopyType.None)
						{
							num = 7;
							continue;
						}
						goto IL_A3;
					}
					break;
					IL_80:
					num = 4;
					continue;
					IL_A3:
					base.ClearAll(flags);
					num = 2;
					continue;
					IL_12E:
					num = 3;
					continue;
					IL_141:
					num = 10;
				}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002C400 File Offset: 0x0002B400
		protected void CopyNames(XlsWorksheet basedOn, Dictionary<string, string> hashNewSheetNames, Dictionary<int, int> hashNewNameIndexes, Dictionary<int, int> hashExternSheetIndexes)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					IXLSRange refersToRange;
					XlsName xlsName;
					sprឦ sprឦ;
					int index;
					int num2;
					sprឦ sprឦ2;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						if (refersToRange == null)
						{
							num = 5;
							continue;
						}
						num = 9;
						continue;
					case 1:
						goto IL_F8;
					case 2:
						hashNewSheetNames = new Dictionary<string, string>();
						hashNewSheetNames.Add(base.Name, basedOn.Name);
						num = 7;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_31B;
						default:
							if (false)
							{
							}
							goto IL_3CE;
						}
						break;
					case 5:
						num = 22;
						continue;
					case 6:
					{
						sprῚ a_2 = (sprῚ)xlsName.Record.Clone();
						sprᤗ.ᜀ(a_2, xlsName.Workbook, hashNewSheetNames, hashExternSheetIndexes, this.m_book);
						INamedRange namedRange = sprឦ.ᜀ(a_2);
						hashNewNameIndexes[index] = namedRange.Index;
						num = 4;
						continue;
					}
					case 7:
						goto IL_2D8;
					case 8:
						goto IL_F8;
					case 9:
						if (refersToRange.Worksheet == basedOn)
						{
							num = 20;
							continue;
						}
						goto IL_3CE;
					case 10:
						goto IL_2B2;
					case 11:
						if (hashNewSheetNames == null)
						{
							num = 2;
							continue;
						}
						goto IL_2D8;
					case 12:
						goto IL_3CE;
					case 13:
						goto IL_2B2;
					case 14:
						if (!xlsName.IsLocal)
						{
							if (true)
							{
							}
							num = 23;
							continue;
						}
						goto IL_3CE;
					case 15:
						if (num2 < 0)
						{
							num = 21;
							continue;
						}
						this.\u1718[num2].Delete();
						num2--;
						num = 1;
						continue;
					case 16:
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									int num3;
									xlsName = (XlsName)sprឦ2.ᜁ(num3);
									INamedRange namedRange = sprឦ.ᜀ(xlsName, this, hashExternSheetIndexes, hashNewSheetNames);
									hashNewNameIndexes[num3] = namedRange.Index;
									num = 4;
									continue;
								}
								case 2:
									goto IL_241;
								case 3:
								{
									Dictionary<int, object>.KeyCollection.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 6;
										continue;
									}
									int num3 = enumerator.Current;
									num = 5;
									continue;
								}
								case 5:
								{
									int num3;
									if (!hashNewNameIndexes.ContainsKey(num3))
									{
										num = 0;
										continue;
									}
									break;
								}
								case 6:
									num = 2;
									continue;
								}
								IL_212:
								num = 3;
								continue;
								goto IL_212;
							}
							IL_241:
							return;
						}
						finally
						{
							Dictionary<int, object>.KeyCollection.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						goto IL_254;
					case 17:
						if (num4 >= num5)
						{
							num = 19;
							continue;
						}
						xlsName = (XlsName)sprឦ2.ᜁ(num4);
						num = 14;
						continue;
					case 18:
						goto IL_98;
					case 19:
					{
						Dictionary<int, object> dictionary = basedOn.ᜉ();
						Dictionary<int, object>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator();
						num = 16;
						continue;
					}
					case 20:
					{
						INamedRange namedRange = sprឦ.ᜀ(xlsName, this, hashExternSheetIndexes, hashNewSheetNames);
						hashNewNameIndexes[index] = namedRange.Index;
						num = 12;
						continue;
					}
					case 21:
						num = 11;
						continue;
					case 22:
						if (!sprឦ.ᜄ(xlsName.Name))
						{
							num = 6;
							continue;
						}
						goto IL_3CE;
					case 23:
						goto IL_254;
					}
					if (basedOn == null)
					{
						num = 18;
						continue;
					}
					num2 = this.\u1718.Count - 1;
					num = 8;
					continue;
					IL_F8:
					num = 15;
					continue;
					IL_254:
					refersToRange = xlsName.RefersToRange;
					index = xlsName.Index;
					num = 0;
					continue;
					IL_2B2:
					num = 17;
					continue;
					IL_31B:
					num = 10;
					continue;
					IL_2D8:
					this.\u1718.ᜀ(basedOn.\u1718, hashNewSheetNames, hashNewNameIndexes, NamesMergeOptionsType.MakeLocal, hashExternSheetIndexes);
					sprឦ2 = (basedOn.Workbook.Names as sprឦ);
					sprឦ = (base.Workbook.Names as sprឦ);
					num4 = 0;
					num5 = sprឦ2.ᜊ();
					goto IL_31B;
					IL_3CE:
					num4++;
					num = 13;
				}
				IL_98:
				throw new ArgumentNullException(RecordTableEnumerator.b("堹崻䴽┿♁ୃ⡅", a_));
			}
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002C870 File Offset: 0x0002B870
		private Dictionary<int, object> ᜉ()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				Dictionary<int, object> dictionary;
				for (;;)
				{
					spr\u223C spr_u223C = this.ᜏ.Table.ᜄ();
					dictionary = new Dictionary<int, object>();
					int num = this.m_iFirstRow;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_62;
							default:
								goto IL_EB;
							}
							break;
						case 1:
							goto IL_64;
						case 2:
						{
							sprᱧ sprᱧ;
							if (sprᱧ != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_64;
						}
						case 3:
						{
							sprᱧ sprᱧ;
							sprᱧ.ᜀ(dictionary);
							num2 = 1;
							continue;
						}
						case 4:
							goto IL_62;
						case 5:
							goto IL_B4;
						case 6:
						{
							if (num > this.m_iLastRow)
							{
								num2 = 0;
								continue;
							}
							sprᱧ sprᱧ = spr_u223C.ᜁ(num);
							num2 = 2;
							continue;
						}
						}
						break;
						IL_64:
						num++;
						num2 = 5;
						continue;
						IL_B4:
						num2 = 6;
						continue;
						IL_62:
						goto IL_B4;
					}
				}
				IL_EB:
				if (false)
				{
				}
				return dictionary;
			}
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002C974 File Offset: 0x0002B974
		protected void CopyRowHeight(XlsWorksheet sourceSheet, Dictionary<int, int> hashExtFormatIndexes)
		{
			int a_ = 10;
			if (sourceSheet == null)
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
					break;
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㌿ⵁㅃ㑅⭇⽉Ὃ♍㕏㝑⁓", a_));
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002C9D4 File Offset: 0x0002B9D4
		protected void CopyConditionalFormats(XlsWorksheet sourceSheet)
		{
			int a_ = 5;
			if (sourceSheet == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺刼䨾㍀⁂⁄ᑆⅈ⹊⡌㭎", a_));
			}
			if (true)
			{
			}
			sourceSheet.\u171A();
			this.ᜪ.CopyFrom(sourceSheet.ᜪ);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002CA48 File Offset: 0x0002BA48
		protected void CopyAutoFilters(XlsWorksheet sourceSheet)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					List<BiffRecordRaw> u171B;
					switch (num)
					{
					case 0:
					{
						List<BiffRecordRaw> list = this.AutoFilterRecords;
						int num2 = 0;
						int count = u171B.Count;
						num = 9;
						continue;
					}
					case 1:
						goto IL_11E;
					case 2:
						if (true)
						{
						}
						this.ᜠ = sourceSheet.ᜠ.Clone(this);
						num = 4;
						continue;
					case 4:
						return;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D9;
						default:
						{
							if (false)
							{
							}
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 1;
								continue;
							}
							List<BiffRecordRaw> list;
							list.Add((BiffRecordRaw)spr\u1CD3.ᜀ(u171B[num2]));
							num2++;
							num = 6;
							continue;
						}
						}
						break;
					case 6:
						goto IL_66;
					case 7:
						goto IL_D9;
					case 8:
						goto IL_61;
					case 9:
						goto IL_66;
					case 10:
						if (sourceSheet.\u171B != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (sourceSheet == null)
					{
						num = 8;
						continue;
					}
					u171B = sourceSheet.\u171B;
					num = 7;
					continue;
					IL_66:
					num = 5;
					continue;
					IL_D9:
					if (u171B != null)
					{
						num = 0;
						continue;
					}
					IL_11E:
					num = 10;
				}
				IL_61:
				throw new ArgumentNullException(RecordTableEnumerator.b("䴽⼿㝁㙃╅ⵇ᥉⑋⭍㕏♑", a_));
			}
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002CBC8 File Offset: 0x0002BBC8
		protected void CopyDataValidations(XlsWorksheet sourceSheet)
		{
			int a_ = 12;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
				{
					XlsDataValidationTable u171F;
					if (u171F != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 3:
				{
					XlsDataValidationTable u171F;
					this.\u171F = (XlsDataValidationTable)u171F.Clone(this);
					num = 1;
					continue;
				}
				case 4:
					goto IL_5C;
				}
				if (sourceSheet == null)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5C;
					default:
						if (false)
						{
						}
						num = 4;
						break;
					}
				}
				else
				{
					XlsDataValidationTable u171F = sourceSheet.\u171F;
					num = 2;
				}
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⭃㍅㩇⥉⥋ᵍ㡏㝑ㅓ≕", a_));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002CC8C File Offset: 0x0002BC8C
		protected void CopyColumnWidth(XlsWorksheet sourceSheet, Dictionary<int, int> hashExtFormatIndexes)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 17;
				for (;;)
				{
					int num2;
					int num4;
					int num5;
					double num6;
					int num8;
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
					{
						if (num2 < 0)
						{
							num = 9;
							continue;
						}
						spr\u216E spr_u216E;
						spr_u216E.ᜅ((ushort)num2);
						num = 20;
						continue;
					}
					case 2:
					{
						int defaultXFIndex;
						int num3;
						if (hashExtFormatIndexes.TryGetValue(defaultXFIndex, out num3))
						{
							num = 18;
							continue;
						}
						return;
					}
					case 3:
						goto IL_25F;
					case 4:
						num = 23;
						continue;
					case 5:
						goto IL_1D4;
					case 6:
					{
						spr\u216E spr_u216E;
						if (spr_u216E != null)
						{
							num = 4;
							continue;
						}
						goto IL_25F;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E9;
						default:
						{
							if (false)
							{
							}
							int defaultXFIndex = sourceSheet.ParentWorkbook.DefaultXFIndex;
							num = 2;
							continue;
						}
						}
						break;
					case 8:
						goto IL_25F;
					case 9:
					{
						spr\u216E spr_u216E;
						spr_u216E.ᜉ();
						int columnWidthPixels = sourceSheet.GetColumnWidthPixels(num4);
						this.SetColumnWidthInPixels(num4, columnWidthPixels);
						num2 = (int)spr_u216E.ᜉ();
						num = 3;
						continue;
					}
					case 10:
						goto IL_2B7;
					case 11:
					{
						if (num4 >= num5)
						{
							goto IL_1E9;
						}
						spr\u216E spr_u216E = this.ᜐ[num4];
						num = 6;
						continue;
					}
					case 12:
						return;
					case 13:
						goto IL_25F;
					case 14:
					{
						int num3;
						List<int> a_2 = this.ᜀ(this.ᜐ, num3);
						num = 10;
						continue;
					}
					case 15:
					{
						if (num6 < 0.0)
						{
							num = 19;
							continue;
						}
						spr\u216E spr_u216E;
						spr_u216E.ᜅ((ushort)((double)spr_u216E.ᜉ() * num6));
						num = 8;
						continue;
					}
					case 16:
						if (hashExtFormatIndexes != null)
						{
							num = 7;
							continue;
						}
						return;
					case 18:
					{
						List<int> a_2 = null;
						num = 22;
						continue;
					}
					case 19:
					{
						if (true)
						{
						}
						spr\u216E spr_u216E;
						int num7 = (int)spr_u216E.ᜉ();
						int columnWidthPixels2 = sourceSheet.GetColumnWidthPixels(num4);
						this.SetColumnWidthInPixels(num4, columnWidthPixels2);
						num6 = (double)spr_u216E.ᜉ() / (double)num7;
						num = 13;
						continue;
					}
					case 20:
						goto IL_25F;
					case 21:
						goto IL_1D4;
					case 22:
					{
						int defaultXFIndex;
						int num3;
						if (num3 != defaultXFIndex)
						{
							num = 14;
							continue;
						}
						goto IL_2B7;
					}
					case 23:
					{
						List<int> a_2;
						if (this.ᜀ(a_2, ref num8, num4))
						{
							num = 0;
							continue;
						}
						num = 15;
						continue;
					}
					case 24:
						goto IL_9C;
					}
					if (sourceSheet == null)
					{
						num = 24;
						continue;
					}
					spr\u216E[] array = spr\u1CD3.ᜀ(sourceSheet.ᜐ);
					int length = Math.Min(array.Length, this.ᜐ.Length);
					Array.Copy(array, this.ᜐ, length);
					this.ᜀ(this.ᜐ, sourceSheet, hashExtFormatIndexes);
					num = 16;
					continue;
					IL_1D4:
					num = 11;
					continue;
					IL_1E9:
					num = 12;
					continue;
					IL_25F:
					num4++;
					num = 21;
					continue;
					IL_2B7:
					num6 = -1.0;
					num2 = -1;
					num8 = 0;
					num4 = 1;
					num5 = this.ᜐ.Length;
					num = 5;
				}
				IL_9C:
				throw new ArgumentNullException(RecordTableEnumerator.b("あ⩄㉆㭈⡊⡌ᱎ㥐㙒ご⍖", a_));
			}
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002D018 File Offset: 0x0002C018
		private new bool ᜀ(List<int> A_0, ref int A_1, int A_2)
		{
			int num = 11;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 0:
					goto IL_D0;
				case 1:
				{
					if (A_1 >= count)
					{
						num = 10;
						continue;
					}
					int num2 = A_0[A_1];
					num = 3;
					continue;
				}
				case 2:
					return false;
				case 3:
					goto IL_D0;
				case 4:
					return true;
				case 5:
				{
					int num2;
					if (num2 < A_2)
					{
						A_1++;
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				}
				case 6:
				{
					int num2;
					if (num2 == A_2)
					{
						num = 4;
						continue;
					}
					return false;
				}
				case 7:
					return false;
				case 8:
					return false;
				case 9:
				{
					if (A_1 >= count)
					{
						num = 8;
						continue;
					}
					if (true)
					{
					}
					int num2 = A_0[A_1];
					num = 0;
					continue;
				}
				case 10:
					return false;
				case 12:
					num = 6;
					continue;
				case 13:
					if (count == 0)
					{
						num = 7;
						continue;
					}
					num = 9;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				count = A_0.Count;
				num = 13;
				continue;
				IL_D0:
				num = 5;
			}
			return false;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002D17C File Offset: 0x0002C17C
		private new void ᜀ(ICollection A_0, XlsWorksheet A_1, Dictionary<int, int> A_2)
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
			this.ᜀ(A_0, A_1, A_2, true);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002D1C4 File Offset: 0x0002C1C4
		private new void ᜀ(ICollection A_0, XlsWorksheet A_1, Dictionary<int, int> A_2, bool A_3)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_1.Workbook == base.Workbook)
						{
							num = 2;
							continue;
						}
						XlsWorkbook parentWorkbook = A_1.ParentWorkbook;
						int defaultXFIndex = this.m_book.DefaultXFIndex;
						IEnumerator enumerator = A_0.GetEnumerator();
						num = 4;
						continue;
					}
					case 1:
						goto IL_5E;
					case 2:
						goto IL_1FC;
					case 4:
						goto IL_224;
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				IL_5E:
				throw new ArgumentNullException(RecordTableEnumerator.b("弻儽ⰿ⹁⅃╅㱇⍉⍋⁍", a_));
				IL_1FC:
				return;
				IL_224:
				try
				{
					num = 6;
					for (;;)
					{
						spr\u2502 spr_u;
						int num2;
						switch (num)
						{
						case 0:
							if (spr_u != null)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_168;
								default:
									if (false)
									{
									}
									num = 9;
									continue;
								}
							}
							break;
						case 1:
							num = 2;
							continue;
						case 2:
						{
							int defaultXFIndex;
							if (num2 == defaultXFIndex)
							{
								num = 3;
								continue;
							}
							break;
						}
						case 3:
							goto IL_11F;
						case 5:
							if (!A_3)
							{
								num = 1;
								continue;
							}
							goto IL_11F;
						case 7:
						{
							IEnumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 8;
								continue;
							}
							spr_u = (spr\u2502)enumerator.Current;
							num = 0;
							continue;
						}
						case 8:
							goto IL_168;
						case 9:
							num2 = (int)spr_u.ᜃ();
							num2 = A_2[num2];
							num = 5;
							continue;
						case 10:
							goto IL_174;
						}
						IL_A3:
						num = 7;
						continue;
						goto IL_A3;
						IL_11F:
						spr_u.ᜁ((ushort)num2);
						num = 4;
						continue;
						IL_168:
						num = 10;
					}
					IL_174:
					return;
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
									num = 2;
									continue;
								}
								goto IL_1C0;
							case 1:
								goto IL_1BE;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_1BE:
					IL_1C0:;
				}
				return;
			}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002D418 File Offset: 0x0002C418
		private new void ᜀ(ICollection A_0, int[] A_1)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						try
						{
							num = 4;
							for (;;)
							{
								spr\u2502 spr_u;
								switch (num)
								{
								case 0:
									goto IL_C0;
								case 1:
									goto IL_10B;
								case 3:
									if (!enumerator.MoveNext())
									{
										num = 5;
										continue;
									}
									spr_u = (spr\u2502)enumerator.Current;
									num = 6;
									continue;
								case 5:
									num = 1;
									continue;
								case 6:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_C0;
									default:
										if (false)
										{
										}
										if (spr_u != null)
										{
											num = 0;
											continue;
										}
										break;
									}
									break;
								}
								goto IL_81;
								IL_C0:
								int num2 = (int)spr_u.ᜃ();
								num2 = A_1[num2];
								spr_u.ᜁ((ushort)num2);
								num = 2;
								continue;
								IL_C2:
								num = 3;
								continue;
								IL_81:
								goto IL_C2;
							}
							IL_10B:
							return;
						}
						finally
						{
							for (;;)
							{
								if (true)
								{
								}
								IDisposable disposable = enumerator as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_157;
									case 2:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_159;
									}
									break;
								}
							}
							IL_157:
							IL_159:;
						}
						goto IL_15A;
					case 1:
						goto IL_44;
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					IL_15A:
					enumerator = A_0.GetEnumerator();
					num = 0;
				}
				IL_44:
				throw new ArgumentNullException(RecordTableEnumerator.b("♄⡆╈❊⡌ⱎ═㩒㩔㥖", a_));
			}
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002D5BC File Offset: 0x0002C5BC
		private new List<int> ᜀ(spr\u216E[] A_0, int A_1)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 4;
				List<int> list;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						return list;
					case 1:
						if (num2 > this.m_book.MaxColumnCount)
						{
							num = 0;
							continue;
						}
						num = 3;
						continue;
					case 2:
					{
						spr\u216E spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
						spr\u216E spr_u216E2 = spr_u216E;
						ushort a_2;
						spr_u216E.ᜀ(a_2 = (ushort)(num2 - 1));
						spr_u216E2.ᜄ(a_2);
						spr_u216E.ᜃ((ushort)A_1);
						A_0[num2] = spr_u216E;
						list.Add(num2);
						num = 5;
						continue;
					}
					case 3:
						if (A_0[num2] == null)
						{
							num = 2;
							continue;
						}
						goto IL_5E;
					case 5:
						goto IL_5E;
					case 6:
						goto IL_CB;
					case 7:
						goto IL_CB;
					case 8:
						goto IL_5C;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					list = new List<int>();
					num2 = 1;
					num = 6;
					continue;
					IL_5E:
					num2++;
					num = 7;
					continue;
					IL_CB:
					num = 1;
				}
				IL_5C:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return list;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("娸吺儼䨾ⱀⵂ㙄", a_));
				}
				break;
			}
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002D718 File Offset: 0x0002C718
		protected void CopyMerges(XlsWorksheet sourceSheet)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u1FBC spr_u1FBC;
					if (spr_u1FBC != null)
					{
						num = 4;
						continue;
					}
					return;
				}
				case 1:
					return;
				case 2:
					goto IL_54;
				case 4:
				{
					spr\u1FBC spr_u1FBC;
					this.\u1714 = (spr\u1FBC)spr\u1CD3.ᜀ(spr_u1FBC, this);
					if (true)
					{
					}
					num = 1;
					continue;
				}
				}
				if (sourceSheet == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				else
				{
					spr\u1FBC spr_u1FBC = sourceSheet.MergeCells;
					num = 0;
				}
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸吺䠼䴾≀♂ᙄ⽆ⱈ⹊㥌", a_));
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002D7DC File Offset: 0x0002C7DC
		protected void AttachEvents()
		{
			int a_ = 12;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (this.m_book.Loading)
					{
						num = 2;
						continue;
					}
					goto IL_A4;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					default:
						goto IL_7B;
					}
					break;
				}
				goto IL_29;
				IL_5B:
				num = 0;
				continue;
				IL_29:
				if (true)
				{
				}
				if (!this.m_book.Styles.Contains(RecordTableEnumerator.b("ు⭃㑅╇⭉⁋", a_)))
				{
					goto IL_5B;
				}
				goto IL_A4;
			}
			IL_7B:
			if (false)
			{
			}
			return;
			IL_A4:
			(this.m_book.Styles[RecordTableEnumerator.b("ు⭃㑅╇⭉⁋", a_)].Font as FontWrapper).AfterChangeEvent += this.ᜀ;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002D8C8 File Offset: 0x0002C8C8
		protected void DetachEvents()
		{
			int a_ = 1;
			int num = 2;
			for (;;)
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
					switch (num)
					{
					case 0:
						(this.m_book.Styles[RecordTableEnumerator.b("礶嘸䤺值帾ⵀ", a_)].Font as FontWrapper).AfterChangeEvent -= this.ᜀ;
						num = 1;
						continue;
					case 1:
						return;
					}
					break;
				}
				if (true)
				{
				}
				if (!this.m_book.Styles.Contains(RecordTableEnumerator.b("礶嘸䤺值帾ⵀ", a_)))
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002D998 File Offset: 0x0002C998
		protected override void OnDispose()
		{
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.ᜏ != null)
					{
						num = 7;
						continue;
					}
					goto IL_C6;
				case 2:
					goto IL_C6;
				case 4:
					if (!this.m_bIsDisposed)
					{
						num = 6;
						continue;
					}
					goto IL_C6;
				case 5:
					num = 4;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 7:
					this.ᜏ.Dispose();
					this.ᜏ = null;
					num = 2;
					continue;
				}
				if (!this.m_bIsDisposed)
				{
					num = 5;
					continue;
				}
				break;
				IL_C6:
				this.ᝈ = null;
				this.ᝇ = null;
				base.OnDispose();
				this.DetachEvents();
				num = 0;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002DA94 File Offset: 0x0002CA94
		protected void CopyPageSetup(XlsWorksheet sourceSheet)
		{
			int a_ = 19;
			if (sourceSheet == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("㩈⑊㡌㵎㉐㙒ٔ㽖㱘㹚⥜", a_));
				}
			}
			this.\u1712 = sourceSheet.\u1712.Clone(this);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002DB04 File Offset: 0x0002CB04
		protected int ImportExtendedFormat(int iXFIndex, XlsWorkbook basedOn, Dictionary<int, int> hashExtFormatIndexes)
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
			return this.m_book.InnerExtFormats.ᜀ(basedOn.InnerExtFormats.ᜁ(iXFIndex), hashExtFormatIndexes);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002DB5C File Offset: 0x0002CB5C
		protected internal override void UpdateStyleIndexes(int[] styleIndexes)
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
			this.ᜀ(this.ᜐ, styleIndexes);
			this.ᜏ.UpdateExtendedFormatIndex(styleIndexes);
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0002DBB0 File Offset: 0x0002CBB0
		internal spr\u1FBC MergeCells
		{
			get
			{
				for (;;)
				{
					base.ParseData();
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
								goto IL_81;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								this.\u1714 = new spr\u1FBC((spr\u2158)base.ReservedHandle, this);
								num = 2;
								continue;
							}
							break;
						case 1:
							if (this.\u1714 == null)
							{
								num = 0;
								continue;
							}
							goto IL_83;
						case 2:
							goto IL_81;
						}
						break;
					}
				}
				IL_81:
				IL_83:
				return this.\u1714;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0002DC48 File Offset: 0x0002CC48
		internal spr\u216E[] ColumnInformation
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
				base.ParseData();
				return this.ᜐ;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0002DC90 File Offset: 0x0002CC90
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0002DCE8 File Offset: 0x0002CCE8
		public int VerticalSplit
		{
			get
			{
				base.ParseData();
				if (this.\u1717 == null)
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
						return 0;
					}
				}
				return this.\u1717.ᜃ();
			}
			set
			{
				for (;;)
				{
					base.ParseData();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_70;
							default:
								if (false)
								{
								}
								this.CreateEmptyPane();
								num = 1;
								continue;
							}
							break;
						case 1:
							goto IL_70;
						case 2:
							if (this.\u1717 == null)
							{
								num = 0;
								continue;
							}
							goto IL_72;
						}
						break;
					}
				}
				IL_70:
				IL_72:
				this.\u1717.ᜀ((int)((ushort)value));
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0002DD74 File Offset: 0x0002CD74
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0002DDCC File Offset: 0x0002CDCC
		public int HorizontalSplit
		{
			get
			{
				base.ParseData();
				if (this.\u1717 == null)
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
						return 0;
					}
				}
				return this.\u1717.ᜄ();
			}
			set
			{
				for (;;)
				{
					base.ParseData();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (this.\u1717 == null)
							{
								num = 1;
								continue;
							}
							goto IL_72;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_70;
							default:
								if (false)
								{
								}
								this.CreateEmptyPane();
								num = 2;
								continue;
							}
							break;
						case 2:
							goto IL_70;
						}
						break;
					}
				}
				IL_70:
				IL_72:
				this.\u1717.ᜁ((int)((ushort)value));
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0002DE58 File Offset: 0x0002CE58
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0002DEB0 File Offset: 0x0002CEB0
		public int FirstVisibleRow
		{
			get
			{
				base.ParseData();
				if (this.\u1717 == null)
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
						return 0;
					}
				}
				return this.\u1717.ᜀ();
			}
			set
			{
				for (;;)
				{
					base.ParseData();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (this.\u1717 == null)
							{
								num = 1;
								continue;
							}
							goto IL_72;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_70;
							default:
								if (false)
								{
								}
								this.CreateEmptyPane();
								num = 2;
								continue;
							}
							break;
						case 2:
							goto IL_70;
						}
						break;
					}
				}
				IL_70:
				IL_72:
				this.\u1717.ᜂ((int)((ushort)value));
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0002DF3C File Offset: 0x0002CF3C
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0002DF94 File Offset: 0x0002CF94
		public int FirstVisibleColumn
		{
			get
			{
				base.ParseData();
				if (this.\u1717 == null)
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
						if (true)
						{
						}
						return 0;
					}
				}
				return this.\u1717.ᜅ();
			}
			set
			{
				for (;;)
				{
					base.ParseData();
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
								goto IL_68;
							default:
								if (false)
								{
								}
								this.CreateEmptyPane();
								num = 2;
								continue;
							}
							break;
						case 1:
							if (this.\u1717 == null)
							{
								num = 0;
								continue;
							}
							goto IL_72;
						case 2:
							goto IL_68;
						}
						break;
					}
				}
				IL_68:
				if (true)
				{
				}
				IL_72:
				this.\u1717.ᜃ((int)((ushort)value));
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0002E020 File Offset: 0x0002D020
		protected internal IXLSRange PrintArea
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
				return this.AllocatedRange;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0002E064 File Offset: 0x0002D064
		public int SelectionCount
		{
			get
			{
				int num;
				for (;;)
				{
					base.ParseData();
					num = 1;
					if (true)
					{
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (this.\u1717 != null)
							{
								num2 = 5;
								continue;
							}
							goto IL_B5;
						case 1:
							if (this.\u1717.ᜄ() != 0)
							{
								num2 = 6;
								continue;
							}
							goto IL_B5;
						case 2:
							goto IL_52;
						case 3:
							goto IL_B5;
						case 4:
							if (this.\u1717.ᜃ() != 0)
							{
								num2 = 7;
								continue;
							}
							goto IL_52;
						case 5:
							IL_50:
							num2 = 4;
							continue;
						case 6:
							num *= 2;
							num2 = 3;
							continue;
						case 7:
							num *= 2;
							num2 = 2;
							continue;
						}
						break;
						IL_B5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						default:
							goto IL_CB;
						}
						IL_52:
						num2 = 1;
					}
				}
				IL_CB:
				if (false)
				{
				}
				return num;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0002E150 File Offset: 0x0002D150
		public XlsDataValidationTable DVTable
		{
			get
			{
				for (;;)
				{
					base.ParseData();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.\u171F == null)
							{
								num = 2;
								continue;
							}
							goto IL_76;
						case 1:
							goto IL_74;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_74;
							default:
								if (false)
								{
								}
								this.\u171F = new XlsDataValidationTable(base.ReservedHandle, this);
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_74:
				IL_76:
				if (true)
				{
				}
				return this.\u171F;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0002E1E4 File Offset: 0x0002D1E4
		public IAutoFilters AutoFilters
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
				base.ParseData();
				return this.ᜠ;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0002E22C File Offset: 0x0002D22C
		protected internal XlsHyperLinksCollection InnerHyperLinks
		{
			get
			{
				for (;;)
				{
					base.ParseData();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜢ == null)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_83;
						case 1:
							goto IL_81;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_81;
							default:
								if (false)
								{
								}
								this.ᜢ = new HyperLinksCollection((spr\u2158)base.ReservedHandle, this);
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_81:
				IL_83:
				return this.ᜢ;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0002E2C4 File Offset: 0x0002D2C4
		internal XlsHyperLinksCollection InnerHyperLinksOrNull
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
				base.ParseData();
				return this.ᜢ;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0002E30C File Offset: 0x0002D30C
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0002E350 File Offset: 0x0002D350
		public ViewMode ViewMode
		{
			get
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
				return this.m_view;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						try
						{
							for (;;)
							{
								num = 8;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_16D;
									case 1:
										base.Zoom = this.\u173F;
										num = 7;
										continue;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_13D;
										default:
											goto IL_18E;
										}
										break;
									case 3:
										if (this.ᝁ != 0)
										{
											num = 4;
											continue;
										}
										goto IL_16D;
									case 4:
										base.Zoom = this.ᝁ;
										num = 10;
										continue;
									case 5:
										if (this.\u173F != 0)
										{
											goto IL_13D;
										}
										goto IL_16D;
									case 6:
										goto IL_16D;
									case 7:
										goto IL_16D;
									case 8:
										switch (value)
										{
										case ViewMode.Normal:
											num = 5;
											continue;
										case ViewMode.Preview:
											num = 11;
											continue;
										case ViewMode.Layout:
											num = 3;
											continue;
										default:
											num = 9;
											continue;
										}
										break;
									case 9:
										num = 6;
										continue;
									case 10:
										goto IL_16D;
									case 11:
										if (this.ᝀ != 0)
										{
											num = 12;
											continue;
										}
										goto IL_16D;
									case 12:
										base.Zoom = this.ᝀ;
										num = 0;
										continue;
									}
									break;
									IL_13D:
									num = 1;
									continue;
									IL_16D:
									num = 2;
								}
							}
							IL_18E:
							if (false)
							{
							}
							goto IL_32;
						}
						catch (Exception)
						{
							goto IL_32;
						}
						goto IL_19F;
						IL_32:
						this.m_view = value;
						num = 2;
						continue;
					case 2:
						goto IL_4B;
					}
					if (this.m_view == value)
					{
						break;
					}
					num = 1;
				}
				IL_4B:
				IL_19F:
				if (true)
				{
				}
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0002E520 File Offset: 0x0002D520
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0002E564 File Offset: 0x0002D564
		public override int Zoom
		{
			get
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
				return base.Zoom;
			}
			set
			{
				for (;;)
				{
					base.Zoom = value;
					ViewMode viewMode = this.ViewMode;
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7E;
						case 1:
							if (this.ᝀ != 0)
							{
								num = 0;
								continue;
							}
							goto IL_EF;
						case 2:
							goto IL_9E;
						case 3:
							this.ᝁ = value;
							goto IL_AF;
						case 4:
							if (this.\u173F != 0)
							{
								num = 2;
								continue;
							}
							goto IL_EF;
						case 5:
							if (this.ᝁ != 0)
							{
								num = 3;
								continue;
							}
							goto IL_EF;
						case 6:
							return;
						case 7:
							switch (viewMode)
							{
							case ViewMode.Normal:
								num = 4;
								continue;
							case ViewMode.Preview:
								num = 1;
								continue;
							case ViewMode.Layout:
								num = 5;
								continue;
							default:
								num = 6;
								continue;
							}
							break;
						case 8:
							goto IL_EF;
						}
						break;
						IL_AF:
						num = 8;
						continue;
						IL_EF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							goto IL_105;
						}
					}
				}
				return;
				IL_7E:
				this.ᝀ = value;
				return;
				IL_9E:
				if (true)
				{
				}
				this.\u173F = value;
				return;
				IL_105:
				if (false)
				{
				}
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0002E67C File Offset: 0x0002D67C
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0002E710 File Offset: 0x0002D710
		public int ZoomScaleNormal
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_7F;
					case 2:
						if (true)
						{
						}
						if (this.ViewMode == ViewMode.Normal)
						{
							num = 1;
							continue;
						}
						goto IL_81;
					}
					if (this.\u173F != 0)
					{
						goto IL_81;
					}
					num = 0;
				}
				IL_7F:
				return this.Zoom;
				IL_81:
				return this.\u173F;
			}
			set
			{
				int a_ = 3;
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						if (this.ViewMode == ViewMode.Normal)
						{
							num = 6;
							continue;
						}
						return;
					case 2:
						num = 4;
						continue;
					case 3:
						goto IL_D6;
					case 4:
						if (value > 400)
						{
							num = 3;
							continue;
						}
						this.\u173F = value;
						num = 1;
						continue;
					case 5:
						return;
					case 6:
						base.Zoom = value;
						num = 5;
						continue;
					}
					if (value < 10)
					{
						break;
					}
					num = 2;
				}
				IL_70:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_D6:
					goto IL_70;
				}
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("挸吺刼刾ቀ⁂⑄⭆ⱈՊ≌㵎㱐㉒㥔", a_), RecordTableEnumerator.b("洸区堼Ἶ⽀㙂⡄╆ⱈ㥊浌≎⑐⁒⅔睖㭘㹚絜㵞Ѡᝢቤɦ౨ժ䵬幮䅰卲ᑴ᥶ᵸ孺䥼佾놀궂", a_));
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0002E80C File Offset: 0x0002D80C
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0002E8A4 File Offset: 0x0002D8A4
		public int ZoomScalePageBreakView
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						if (this.ViewMode == ViewMode.Preview)
						{
							num = 3;
							continue;
						}
						goto IL_82;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 3:
						goto IL_80;
					}
					if (this.ᝀ != 0)
					{
						goto IL_82;
					}
					num = 2;
				}
				IL_80:
				return this.Zoom;
				IL_82:
				return this.ᝀ;
			}
			set
			{
				int a_ = 1;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.ViewMode == ViewMode.Preview)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						return;
					case 2:
						if (value > 400)
						{
							num = 5;
							continue;
						}
						this.ᝀ = value;
						num = 1;
						continue;
					case 4:
						base.Zoom = value;
						num = 0;
						continue;
					case 5:
						goto IL_DA;
					case 6:
						num = 2;
						continue;
					}
					if (value < 10)
					{
						break;
					}
					num = 6;
				}
				IL_71:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_DA:
					goto IL_71;
				}
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("洶嘸吺值氾≀≂⥄≆᥈⩊⩌⩎ፐ⅒ご㙖㉘൚㑜㩞ᙠ", a_), RecordTableEnumerator.b("挶儸帺ᴼ儾㑀⹂❄≆㭈歊⁌㩎≐❒畔㕖㱘筚㽜㩞ᕠᑢdɦݨ䭪屬彮兰ቲ᭴፶奸佺䵼佾꾀", a_));
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0002E9A4 File Offset: 0x0002D9A4
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0002EA3C File Offset: 0x0002DA3C
		public int ZoomScalePageLayoutView
		{
			get
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ViewMode == ViewMode.Layout)
						{
							num = 2;
							continue;
						}
						goto IL_82;
					case 2:
						goto IL_80;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (this.ᝁ != 0)
					{
						goto IL_82;
					}
					num = 3;
				}
				IL_80:
				return this.Zoom;
				IL_82:
				return this.ᝁ;
			}
			set
			{
				int a_ = 10;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ViewMode == ViewMode.Layout)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						num = 2;
						continue;
					case 2:
						if (value > 400)
						{
							if (true)
							{
							}
							num = 6;
							continue;
						}
						this.ᝁ = value;
						num = 0;
						continue;
					case 4:
						base.Zoom = value;
						num = 5;
						continue;
					case 5:
						return;
					case 6:
						goto IL_D7;
					}
					if (value < 10)
					{
						break;
					}
					num = 1;
				}
				IL_69:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_D7:
					goto IL_69;
				default:
					if (false)
					{
					}
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᨿⵁ⭃⭅ᭇ⥉ⵋ≍㕏ɑ㕓ㅕ㵗ᙙ㵛❝ཟᝡၣづŧཀྵ᭫", a_), RecordTableEnumerator.b("ᐿ⩁⅃晅♇㽉⅋ⱍ㕏⁑瑓㭕ⵗ⥙⡛繝ɟݡ䑣ѥ൧ṩ᭫୭ᕯᱱ味䝵䡷婹ᵻၽꊁ낃뚅뢇ꒉ", a_));
				}
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0002EB38 File Offset: 0x0002DB38
		internal int RealZoomScaleNormal
		{
			get
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
				return this.\u173F;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0002EB7C File Offset: 0x0002DB7C
		internal int RealZoomScalePageBreakView
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
				return this.ᝀ;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0002EBC0 File Offset: 0x0002DBC0
		internal int RealZoomScalePageLayoutView
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
				return this.ᝁ;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0002EC04 File Offset: 0x0002DC04
		internal int Excel2007ZoomScale
		{
			get
			{
				for (;;)
				{
					ViewMode viewMode = this.ViewMode;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (viewMode)
							{
							case ViewMode.Normal:
								num = 6;
								continue;
							case ViewMode.Preview:
								num = 4;
								continue;
							case ViewMode.Layout:
								num = 8;
								continue;
							default:
								num = 3;
								continue;
							}
							break;
						case 1:
							goto IL_B7;
						case 2:
							goto IL_79;
						case 3:
							num = 1;
							continue;
						case 4:
							if (this.ᝀ != 0)
							{
								num = 2;
								continue;
							}
							goto IL_104;
						case 5:
							goto IL_D7;
						case 6:
							if (this.\u173F != 0)
							{
								num = 7;
								continue;
							}
							goto IL_104;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CC;
							default:
								goto IL_F7;
							}
							break;
						case 8:
							if (this.ᝁ != 0)
							{
								goto IL_CC;
							}
							goto IL_104;
						}
						break;
						IL_CC:
						num = 5;
					}
				}
				IL_79:
				return this.ᝀ;
				IL_B7:
				goto IL_104;
				IL_D7:
				if (true)
				{
				}
				return this.ᝁ;
				IL_F7:
				if (false)
				{
				}
				return this.\u173F;
				IL_104:
				return base.Zoom;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0002ED1C File Offset: 0x0002DD1C
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0002ED68 File Offset: 0x0002DD68
		public int DefaultPrintRowHeight
		{
			get
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
				base.ParseData();
				return this.\u1712.DefaultRowHeight;
			}
			set
			{
				for (;;)
				{
					base.ParseData();
					if (true)
					{
					}
					int num = 11;
					for (;;)
					{
						int num2;
						int defaultRowHeight;
						switch (num)
						{
						case 0:
							if (this.DefaultRowHeight != this.m_book.StandardRowHeight * 20.0)
							{
								num = 10;
								continue;
							}
							goto IL_D8;
						case 1:
							goto IL_16C;
						case 2:
						{
							sprᱧ sprᱧ;
							if (!sprᱧ.\u1713())
							{
								num = 5;
								continue;
							}
							goto IL_1F6;
						}
						case 3:
						{
							if (num2 > this.m_iLastRow)
							{
								num = 1;
								continue;
							}
							sprᱧ sprᱧ = sprᜑ.ᜀ(this, num2, false);
							num = 17;
							continue;
						}
						case 4:
							goto IL_D8;
						case 5:
							num = 19;
							continue;
						case 6:
							return;
						case 7:
							goto IL_108;
						case 8:
							num = 0;
							continue;
						case 9:
							num = 2;
							continue;
						case 10:
							this.\u1712.DefaultRowHeightFlag = true;
							num = 4;
							continue;
						case 11:
							if (this.\u1712.DefaultRowHeight != value)
							{
								num = 8;
								continue;
							}
							return;
						case 12:
							goto IL_1F6;
						case 13:
							num = 15;
							continue;
						case 14:
							num2 = this.m_iFirstRow;
							num = 16;
							continue;
						case 15:
							if (this.m_iLastRow >= 0)
							{
								num = 14;
								continue;
							}
							goto IL_16C;
						case 16:
							goto IL_108;
						case 17:
						{
							sprᱧ sprᱧ;
							if (sprᱧ != null)
							{
								num = 9;
								continue;
							}
							goto IL_1F6;
						}
						case 18:
							if (this.m_iFirstRow >= 0)
							{
								num = 13;
								continue;
							}
							goto IL_16C;
						case 19:
						{
							sprᱧ sprᱧ;
							if ((int)sprᱧ.\u1718() == defaultRowHeight)
							{
								num = 20;
								continue;
							}
							goto IL_1F6;
						}
						case 20:
						{
							sprᱧ sprᱧ;
							sprᱧ.ᜃ((ushort)value);
							num = 12;
							continue;
						}
						}
						break;
						IL_D8:
						defaultRowHeight = this.\u1712.DefaultRowHeight;
						num = 18;
						continue;
						IL_108:
						num = 3;
						continue;
						IL_16C:
						this.\u1712.DefaultRowHeight = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						IL_1F6:
						num2++;
						num = 7;
					}
				}
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0002EFD0 File Offset: 0x0002DFD0
		internal sprᤗ InnerNames
		{
			get
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
				return this.\u1718;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0002F014 File Offset: 0x0002E014
		internal XlsDataValidationTable InnerDVTable
		{
			get
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
				return this.\u171F;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0002F058 File Offset: 0x0002E058
		public XlsCellRecordCollection CellRecords
		{
			get
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
				base.ParseData();
				return this.ᜏ;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0002F0A0 File Offset: 0x0002E0A0
		internal override XlsPageSetupBase PageSetupBase
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
				base.ParseData();
				return this.\u1712;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0002F0E8 File Offset: 0x0002E0E8
		public XlsWorksheetConditionalFormats ConditionalFormats
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
				base.ParseData();
				return this.ᜪ;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0002F130 File Offset: 0x0002E130
		internal spr\u2408 Pane
		{
			get
			{
				for (;;)
				{
					base.ParseData();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7C;
						case 1:
							if (this.\u1717 == null)
							{
								num = 2;
								continue;
							}
							goto IL_7E;
						case 2:
							if (true)
							{
							}
							this.\u1717 = (spr\u2408)spr\u175E.ᜀ(TBIFFRecord.Pane);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_7C:
				IL_7E:
				return this.\u1717;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0002F1C4 File Offset: 0x0002E1C4
		internal List<spr\u21A4> Selections
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
				base.ParseData();
				return this.\u1716;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0002F20C File Offset: 0x0002E20C
		internal spr\u256D InnerCustomProperties
		{
			get
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
				base.ParseData();
				return this.ᜫ;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0002F254 File Offset: 0x0002E254
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0002F2A0 File Offset: 0x0002E2A0
		public bool UseRangesCache
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
				base.ParseData();
				return this.ᜏ.UseCache;
			}
			set
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
				base.ParseData();
				this.ᜏ.UseCache = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0002F2F0 File Offset: 0x0002E2F0
		private List<BiffRecordRaw> AutoFilterRecords
		{
			get
			{
				for (;;)
				{
					base.ParseData();
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.\u171B == null)
							{
								num = 1;
								continue;
							}
							goto IL_77;
						case 1:
							this.\u171B = new List<BiffRecordRaw>();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 2:
							goto IL_75;
						}
						break;
					}
				}
				IL_75:
				IL_77:
				return this.\u171B;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0002F37C File Offset: 0x0002E37C
		private List<BiffRecordRaw> DConRecords
		{
			get
			{
				for (;;)
				{
					base.ParseData();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜩ == null)
							{
								num = 2;
								continue;
							}
							goto IL_77;
						case 1:
							goto IL_75;
						case 2:
							if (true)
							{
							}
							this.ᜩ = new List<BiffRecordRaw>();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_75:
				IL_77:
				return this.ᜩ;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0002F408 File Offset: 0x0002E408
		private List<BiffRecordRaw> SortRecords
		{
			get
			{
				for (;;)
				{
					base.ParseData();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_75;
						case 1:
							this.ᜣ = new List<BiffRecordRaw>();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 2:
							if (this.ᜣ == null)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_77;
						}
						break;
					}
				}
				IL_75:
				IL_77:
				return this.ᜣ;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0002F494 File Offset: 0x0002E494
		internal spr\u2622 ErrorIndicators
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
				base.ParseData();
				return this.ᜮ;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0002F4DC File Offset: 0x0002E4DC
		public string QuotedName
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base.ParseData();
				return RecordTableEnumerator.b("扄", a_) + base.Name.Replace(RecordTableEnumerator.b("扄", a_), RecordTableEnumerator.b("扄恆", a_)) + RecordTableEnumerator.b("扄", a_);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0002F570 File Offset: 0x0002E570
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0002F5B8 File Offset: 0x0002E5B8
		public ExcelVersion Version
		{
			get
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
				return this.m_book.Version;
			}
			set
			{
				switch (0)
				{
				default:
				{
					int num = 37;
					for (;;)
					{
						sprᤗ sprᤗ;
						XlsHPageBreaksCollection xlsHPageBreaksCollection;
						spr\u1D9B spr_u1D9B;
						sprវ sprវ;
						XlsVPageBreaksCollection xlsVPageBreaksCollection;
						spr\u216E[] array;
						switch (num)
						{
						case 0:
							goto IL_32B;
						case 1:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_19D;
									case 2:
										num = 0;
										continue;
									case 3:
									{
										IEnumerator<XlsConditionalFormats> enumerator;
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										XlsConditionalFormats xlsConditionalFormats = enumerator.Current;
										xlsConditionalFormats.ᜂ();
										num = 4;
										continue;
									}
									}
									IL_15C:
									num = 3;
									continue;
									goto IL_15C;
								}
								IL_19D:
								goto IL_4EE;
							}
							finally
							{
								num = 1;
								for (;;)
								{
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
										IEnumerator<XlsConditionalFormats> enumerator;
										switch (num)
										{
										case 0:
											enumerator.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_1FB;
										}
										if (enumerator == null)
										{
											goto IL_1FD;
										}
										break;
									}
									}
									num = 0;
								}
								IL_1FB:
								IL_1FD:;
							}
							goto IL_1FE;
						case 2:
							goto IL_7C9;
						case 3:
							goto IL_6CE;
						case 4:
							goto IL_56B;
						case 5:
							num = 11;
							continue;
						case 6:
							if (this.m_iLastColumn != 2147483647)
							{
								num = 17;
								continue;
							}
							goto IL_230;
						case 7:
							this.ᜅ();
							num = 56;
							continue;
						case 8:
							goto IL_230;
						case 9:
							this.m_iFirstColumn = Math.Min(this.m_iFirstColumn, this.m_book.MaxColumnCount);
							num = 40;
							continue;
						case 10:
							goto IL_757;
						case 11:
							if (this.\u1717.ᜀ() <= this.m_book.MaxRowCount - 1)
							{
								num = 46;
								continue;
							}
							goto IL_3AC;
						case 12:
							sprᤗ.ᜀ(value);
							num = 19;
							continue;
						case 13:
							num = 44;
							continue;
						case 14:
							num = 41;
							continue;
						case 15:
							if (value == ExcelVersion.Version97to2003)
							{
								num = 13;
								continue;
							}
							goto IL_357;
						case 16:
							goto IL_357;
						case 17:
							this.m_iLastColumn = Math.Min(this.m_iLastColumn, this.m_book.MaxColumnCount);
							num = 8;
							continue;
						case 18:
							goto IL_51A;
						case 19:
							goto IL_5F5;
						case 20:
							goto IL_61D;
						case 21:
							if (xlsHPageBreaksCollection != null)
							{
								num = 23;
								continue;
							}
							goto IL_51A;
						case 22:
							spr_u1D9B.ᜀ(value);
							num = 52;
							continue;
						case 23:
							num = 25;
							continue;
						case 24:
							if (value == ExcelVersion.Version97to2003)
							{
								num = 55;
								continue;
							}
							goto IL_4EE;
						case 25:
							if (value == ExcelVersion.Version97to2003)
							{
								num = 38;
								continue;
							}
							goto IL_51A;
						case 26:
							if (sprᤗ != null)
							{
								num = 12;
								continue;
							}
							goto IL_5F5;
						case 27:
							goto IL_3AC;
						case 28:
							goto IL_5A0;
						case 29:
							if (sprវ != null)
							{
								num = 47;
								continue;
							}
							goto IL_4EE;
						case 30:
							goto IL_5A0;
						case 31:
							this.m_iFirstRow = Math.Min(this.m_iFirstRow, this.m_book.MaxRowCount);
							num = 2;
							continue;
						case 32:
							if (this.m_iFirstColumn != 2147483647)
							{
								if (true)
								{
								}
								num = 9;
								continue;
							}
							goto IL_5C8;
						case 33:
							xlsVPageBreaksCollection.ᜂ();
							num = 10;
							continue;
						case 34:
							if (xlsVPageBreaksCollection != null)
							{
								num = 60;
								continue;
							}
							goto IL_757;
						case 35:
						{
							int num2 = array.Length;
							num = 30;
							continue;
						}
						case 36:
							num = 42;
							continue;
						case 38:
							xlsHPageBreaksCollection.ᜂ();
							num = 18;
							continue;
						case 39:
							if (this.AutoFilters.Count != 0)
							{
								num = 45;
								continue;
							}
							goto IL_56B;
						case 40:
							goto IL_5C8;
						case 41:
							if (array[array.Length - 1] != null)
							{
								num = 36;
								continue;
							}
							goto IL_61D;
						case 42:
							if (this.\u173C != null)
							{
								num = 35;
								continue;
							}
							goto IL_61D;
						case 43:
							if (this.\u1717.ᜅ() > this.m_book.MaxColumnCount - 1)
							{
								num = 27;
								continue;
							}
							goto IL_32B;
						case 44:
							if (this.\u1714 != null)
							{
								num = 57;
								continue;
							}
							goto IL_357;
						case 45:
						{
							XlsAutoFiltersCollection xlsAutoFiltersCollection = (XlsAutoFiltersCollection)this.AutoFilters;
							xlsAutoFiltersCollection.ᜀ(this.m_book.MaxRowCount, this.m_book.MaxColumnCount, value);
							num = 4;
							continue;
						}
						case 46:
							num = 43;
							continue;
						case 47:
						{
							this.ᜀ(sprវ);
							XlsWorksheetConditionalFormats conditionalFormats = this.ConditionalFormats;
							IEnumerator<XlsConditionalFormats> enumerator = conditionalFormats.GetEnumerator();
							num = 1;
							continue;
						}
						case 48:
							if (this.\u1717 != null)
							{
								num = 5;
								continue;
							}
							goto IL_32B;
						case 49:
							if (this.m_book.IsConverted)
							{
								num = 14;
								continue;
							}
							goto IL_61D;
						case 50:
							if (value == ExcelVersion.Version97to2003)
							{
								num = 33;
								continue;
							}
							goto IL_757;
						case 51:
							if (this.m_iFirstRow != -1)
							{
								num = 31;
								continue;
							}
							goto IL_7C9;
						case 52:
							goto IL_777;
						case 53:
							if (this.Version == ExcelVersion.Version97to2003)
							{
								num = 7;
								continue;
							}
							return;
						case 54:
						{
							int num2;
							if (num2 >= this.ᜐ.Length)
							{
								num = 20;
								continue;
							}
							spr\u216E spr_u216E = this.\u173C.Clone() as spr\u216E;
							spr_u216E.ᜄ((ushort)num2);
							spr_u216E.ᜀ((ushort)num2);
							this.ᜐ[num2] = spr_u216E;
							num2++;
							num = 28;
							continue;
						}
						case 55:
							num = 29;
							continue;
						case 56:
							return;
						case 57:
							goto IL_1FE;
						case 58:
							if (spr_u1D9B != null)
							{
								num = 22;
								continue;
							}
							goto IL_777;
						case 59:
							this.m_iLastRow = Math.Min(this.m_iLastRow, this.m_book.MaxRowCount);
							num = 3;
							continue;
						case 60:
							num = 50;
							continue;
						}
						if (this.m_iLastRow != -1)
						{
							num = 59;
							continue;
						}
						goto IL_6CE;
						IL_1FE:
						this.\u1714.ᜂ(this.m_book.MaxRowCount, this.m_book.MaxColumnCount);
						num = 16;
						continue;
						IL_230:
						array = this.ᜐ;
						this.ᜐ = new spr\u216E[this.m_book.MaxColumnCount + 2];
						Array.Copy(array, 0, this.ᜐ, 0, Math.Min(array.Length, this.ᜐ.Length));
						num = 49;
						continue;
						IL_32B:
						spr_u1D9B = base.InnerShapes;
						num = 58;
						continue;
						IL_357:
						num = 39;
						continue;
						IL_3AC:
						this.\u1717 = null;
						num = 0;
						continue;
						IL_4EE:
						sprᤗ = this.InnerNames;
						num = 26;
						continue;
						IL_51A:
						xlsVPageBreaksCollection = (XlsVPageBreaksCollection)this.VPageBreaks;
						num = 34;
						continue;
						IL_56B:
						sprវ = ((XlsWorkbook)base.Workbook).DataHolder;
						num = 24;
						continue;
						IL_5A0:
						num = 54;
						continue;
						IL_5C8:
						num = 6;
						continue;
						IL_5F5:
						num = 48;
						continue;
						IL_61D:
						this.ᜏ.Version = value;
						xlsHPageBreaksCollection = (XlsHPageBreaksCollection)this.HPageBreaks;
						num = 21;
						continue;
						IL_6CE:
						num = 51;
						continue;
						IL_757:
						num = 15;
						continue;
						IL_777:
						num = 53;
						continue;
						IL_7C9:
						num = 32;
					}
					return;
				}
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002FE10 File Offset: 0x0002EE10
		private new void ᜅ()
		{
			int num = 0;
			for (;;)
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
					switch (num)
					{
					case 1:
						this.ᜡ.Clear();
						num = 2;
						continue;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (this.ᜡ == null)
					{
						return;
					}
					break;
				}
				num = 1;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0002FE90 File Offset: 0x0002EE90
		internal RecordExtractor RecordExtractor
		{
			get
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
				return this.CellRecords.RecordExtractor;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0002FED8 File Offset: 0x0002EED8
		internal sprᱥ RowHeightHelper
		{
			get
			{
				int num = 0;
				for (;;)
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
						switch (num)
						{
						case 1:
							goto IL_7B;
						case 2:
							this.ᜱ = new sprᱥ(new sprᱥ.ᜀ(this.GetRowHeightPixels));
							num = 1;
							continue;
						}
						if (true)
						{
						}
						if (this.ᜱ != null)
						{
							goto IL_7D;
						}
						break;
					}
					num = 2;
				}
				IL_7B:
				IL_7D:
				return this.ᜱ;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0002FF68 File Offset: 0x0002EF68
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0002FFAC File Offset: 0x0002EFAC
		internal bool IsVisible
		{
			get
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
				return this.\u1734;
			}
			set
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
				this.\u1734 = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0002FFF0 File Offset: 0x0002EFF0
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x00030034 File Offset: 0x0002F034
		internal bool IsZeroHeight
		{
			get
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
				return this.\u1735;
			}
			set
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
				this.\u1735 = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00030078 File Offset: 0x0002F078
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x000300BC File Offset: 0x0002F0BC
		internal int BaseColumnWidth
		{
			get
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
				return this.\u1736;
			}
			set
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
				this.\u1736 = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00030100 File Offset: 0x0002F100
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00030144 File Offset: 0x0002F144
		internal bool IsThickBottom
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
				return this.\u1737;
			}
			set
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
				this.\u1737 = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00030188 File Offset: 0x0002F188
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x000301CC File Offset: 0x0002F1CC
		internal bool IsThickTop
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
				return this.\u1738;
			}
			set
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
				this.\u1738 = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00030210 File Offset: 0x0002F210
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00030254 File Offset: 0x0002F254
		internal byte OutlineLevelColumn
		{
			get
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
				return this.\u1739;
			}
			set
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
				this.\u1739 = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00030298 File Offset: 0x0002F298
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000302DC File Offset: 0x0002F2DC
		internal byte OutlineLevelRow
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
				return this.\u173A;
			}
			set
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
				this.\u173A = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00030320 File Offset: 0x0002F320
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00030364 File Offset: 0x0002F364
		internal bool CustomHeight
		{
			get
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
				return this.\u173B;
			}
			set
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
				this.\u173B = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000303A8 File Offset: 0x0002F3A8
		public bool HasMergedCells
		{
			get
			{
				if (this.\u1714 != null)
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
							goto IL_32;
						}
					}
					IL_32:
					if (false)
					{
					}
					return this.\u1714.ᜅ() > 0;
				}
				return false;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00030400 File Offset: 0x0002F400
		internal ListObjectCollection InnerListObjects
		{
			get
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
				return this.\u1732;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00030444 File Offset: 0x0002F444
		protected override SheetProtectionType DefaultProtectionOptions
		{
			get
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
				return SheetProtectionType.LockedCells | SheetProtectionType.UnLockedCells;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00030484 File Offset: 0x0002F484
		protected override SheetProtectionType UnprotectedOptions
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
				return SheetProtectionType.Content;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000304C4 File Offset: 0x0002F4C4
		internal Dictionary<string, string> InlineStrings
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᝄ = new Dictionary<string, string>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6F;
					}
					if (true)
					{
					}
					if (this.ᝄ != null)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.ᝄ;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00030548 File Offset: 0x0002F548
		internal List<BiffRecordRaw> PreserveExternalConnection
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᝅ = new List<BiffRecordRaw>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_67;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_67;
					}
					if (this.ᝅ != null)
					{
						break;
					}
					num = 1;
				}
				IL_67:
				if (true)
				{
				}
				return this.ᝅ;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000305CC File Offset: 0x0002F5CC
		internal List<Stream> PreservePivotTables
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᝆ = new List<Stream>();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (true)
					{
					}
					if (this.ᝆ != null)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.ᝆ;
			}
		}

		// Token: 0x1700020A RID: 522
		public IXLSRange this[int row, int column]
		{
			get
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
				return this.AllocatedRange[row, column];
			}
		}

		// Token: 0x1700020B RID: 523
		public IXLSRange this[int row, int column, int lastRow, int lastColumn]
		{
			get
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
				return this.AllocatedRange[row, column, lastRow, lastColumn];
			}
		}

		// Token: 0x1700020C RID: 524
		public IXLSRange this[string name]
		{
			get
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
				return this[name, false];
			}
		}

		// Token: 0x1700020D RID: 525
		internal IXLSRange this[string A_0, bool A_1]
		{
			get
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
				return this.AllocatedRange[A_0, A_1];
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00030770 File Offset: 0x0002F770
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x000307CC File Offset: 0x0002F7CC
		public int ActivePane
		{
			get
			{
				base.ParseData();
				if (this.\u1717 == null)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_30;
						}
					}
					IL_30:
					if (false)
					{
					}
					if (true)
					{
					}
					return int.MinValue;
				}
				return (int)this.\u1717.ᜆ();
			}
			set
			{
				for (;;)
				{
					base.ParseData();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.CreateEmptyPane();
							goto IL_60;
						case 1:
							goto IL_68;
						case 2:
							if (this.\u1717 != null)
							{
								goto IL_72;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_60;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						break;
						IL_60:
						num = 1;
					}
				}
				IL_68:
				if (true)
				{
				}
				IL_72:
				this.\u1717.ᜀ((ushort)value);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00030858 File Offset: 0x0002F858
		public IXLSRange[] Cells
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
				return this.AllocatedRange.Cells;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000308A0 File Offset: 0x0002F8A0
		public IXLSRange[] Columns
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
				return this.AllocatedRange.Columns;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000308E8 File Offset: 0x0002F8E8
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00030930 File Offset: 0x0002F930
		public bool DisplayPageBreaks
		{
			get
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
				base.ParseData();
				return this.ᜑ;
			}
			set
			{
				for (;;)
				{
					base.ParseData();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							base.SetChanged();
							this.ᜑ = value;
							goto IL_70;
						case 1:
							if (this.ᜑ == value)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_70;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 2:
							return;
						}
						break;
						IL_70:
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000309B8 File Offset: 0x0002F9B8
		public IOleObjects OleObjects
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						this.ᝂ = new sprᜭ(this);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_70;
					}
					if (this.ᝂ != null)
					{
						break;
					}
					num = 0;
				}
				IL_70:
				return this.ᝂ;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00030A40 File Offset: 0x0002FA40
		public bool HasOleObjects
		{
			get
			{
				if (this.ᝂ != null)
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
							goto IL_32;
						}
					}
					IL_32:
					if (false)
					{
					}
					return this.ᝂ.Count > 0;
				}
				return false;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00030A98 File Offset: 0x0002FA98
		public SparklineGroupCollection SparklineGroups
		{
			get
			{
				int a_ = 4;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᝃ = new SparklineGroupCollection(this.m_book);
						num = 1;
						continue;
					case 1:
						goto IL_71;
					case 2:
						num = 9;
						continue;
					case 3:
						this.m_book.Version = ExcelVersion.Version2010;
						num = 5;
						continue;
					case 5:
						goto IL_7D;
					case 6:
						if (this.Version == ExcelVersion.Version2010)
						{
							goto IL_91;
						}
						goto IL_126;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_91;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 8:
						if (this.ᝃ == null)
						{
							num = 0;
							continue;
						}
						goto IL_C7;
					case 9:
						if (this.Version == ExcelVersion.Version2010)
						{
							num = 3;
							continue;
						}
						goto IL_7D;
					}
					if (this.m_book.Loading)
					{
						num = 2;
						continue;
					}
					IL_7D:
					num = 6;
					continue;
					IL_91:
					if (true)
					{
					}
					num = 7;
				}
				IL_71:
				IL_C7:
				return this.ᝃ;
				IL_126:
				throw new NotSupportedException(RecordTableEnumerator.b("椹䰻弽㈿⥁⡃⽅♇⽉汋ⵍㅏ㱑瑓㡕㝗⹙籛㱝՟䉡ᝣ፥ᡧᩩͫᱭѯ᝱ၳ噵ṷᕹ๻幽꺍몙ﮝ튟톡춣즥욧蒩", a_));
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00030BE0 File Offset: 0x0002FBE0
		public IHPageBreaks HPageBreaks
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
				base.ParseData();
				return this.\u1712.HPageBreaks;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00030C2C File Offset: 0x0002FC2C
		public IHyperLinks HyperLinks
		{
			get
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
				return this.InnerHyperLinks;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00030C70 File Offset: 0x0002FC70
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00030CBC File Offset: 0x0002FCBC
		public bool IsDisplayZeros
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
				base.ParseData();
				return base.WindowTwo.ᜄ();
			}
			set
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
				base.ParseData();
				base.WindowTwo.ᜂ(value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00030D0C File Offset: 0x0002FD0C
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00030D58 File Offset: 0x0002FD58
		public bool GridLinesVisible
		{
			get
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
				base.ParseData();
				return base.WindowTwo.\u1713();
			}
			set
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
				base.ParseData();
				base.WindowTwo.ᜁ(value);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00030DA8 File Offset: 0x0002FDA8
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x00030DF4 File Offset: 0x0002FDF4
		public bool RowColumnHeadersVisible
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
				base.ParseData();
				return base.WindowTwo.ᜏ();
			}
			set
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
				base.ParseData();
				base.WindowTwo.ᜀ(value);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00030E44 File Offset: 0x0002FE44
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00030E8C File Offset: 0x0002FE8C
		public bool IsStringsPreserved
		{
			get
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
				base.ParseData();
				return this.\u171A;
			}
			set
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
				base.ParseData();
				this.ᜰ.ᜀ();
				this.\u171A = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00030EE0 File Offset: 0x0002FEE0
		public IXLSRange[] MergedCells
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					IXLSRange[] array2;
					for (;;)
					{
						base.ParseData();
						int num = 11;
						for (;;)
						{
							IXLSRange[] array;
							int num2;
							int num4;
							switch (num)
							{
							case 0:
								goto IL_157;
							case 1:
								goto IL_157;
							case 2:
								array = null;
								goto IL_133;
							case 3:
								return array2;
							case 4:
								if (array2 != null)
								{
									num = 6;
									continue;
								}
								return array2;
							case 5:
								num2 = 0;
								goto IL_187;
							case 6:
							{
								List<Rectangle> list = this.\u1714.ᜄ();
								int num3 = 0;
								num = 1;
								continue;
							}
							case 7:
								num2 = this.\u1714.ᜅ();
								goto IL_187;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return array2;
								default:
									if (false)
									{
									}
									if (num4 <= 0)
									{
										num = 10;
										continue;
									}
									num = 12;
									continue;
								}
								break;
							case 9:
							{
								int num3;
								if (num3 >= num4)
								{
									num = 3;
									continue;
								}
								List<Rectangle> list;
								Rectangle rectangle = list[num3];
								XlsRange xlsRange = base.AppImplementation.ᜀ(this, rectangle.X + 1, rectangle.Y + 1, rectangle.Right + 1, rectangle.Bottom + 1);
								array2[num3] = xlsRange;
								num3++;
								num = 0;
								continue;
							}
							case 10:
								num = 2;
								continue;
							case 11:
								if (this.\u1714 == null)
								{
									num = 13;
									continue;
								}
								num = 7;
								continue;
							case 12:
								array = new IXLSRange[num4];
								goto IL_133;
							case 13:
								num = 5;
								continue;
							}
							break;
							IL_133:
							array2 = array;
							num = 4;
							continue;
							IL_157:
							num = 9;
							continue;
							IL_187:
							num4 = num2;
							num = 8;
						}
					}
					return array2;
				}
				}
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000310B4 File Offset: 0x000300B4
		public INameRanges Names
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
				return this.\u1718;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000310F8 File Offset: 0x000300F8
		public IPageSetup PageSetup
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
				base.ParseData();
				return this.\u1712;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00031140 File Offset: 0x00030140
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000311A0 File Offset: 0x000301A0
		internal IXLSRange PaneFirstVisible
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
				base.ParseData();
				return base.AppImplementation.ᜀ(this, this.FirstVisibleColumn + 1, this.FirstVisibleRow + 1);
			}
			set
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
				base.ParseData();
				this.FirstVisibleRow = value.Row - 1;
				this.FirstVisibleColumn = value.Column - 1;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00031200 File Offset: 0x00030200
		public IXLSRange AllocatedRange
		{
			[DebuggerStepThrough]
			get
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
				return this.Range;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00031244 File Offset: 0x00030244
		public IXLSRange[] Rows
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
				return this.AllocatedRange.Rows;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0003128C File Offset: 0x0003028C
		public bool IsFreezePanes
		{
			get
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
				return base.WindowTwo.ᜁ();
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x000312D4 File Offset: 0x000302D4
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x00031334 File Offset: 0x00030334
		protected internal IXLSRange SplitCell
		{
			get
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
				base.ParseData();
				return base.AppImplementation.ᜀ(this, this.VerticalSplit + 1, this.HorizontalSplit + 1);
			}
			set
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
				base.ParseData();
				this.VerticalSplit = value.Column - 1;
				this.HorizontalSplit = value.Row - 1;
				base.WindowTwo.ᜊ(true);
				base.WindowTwo.ᜈ(true);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000313AC File Offset: 0x000303AC
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x000313F8 File Offset: 0x000303F8
		public double DefaultRowHeight
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
				return (double)this.DefaultPrintRowHeight / 20.0;
			}
			set
			{
				int a_ = 18;
				if (true)
				{
				}
				if (value < 0.0)
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_3F;
						}
					}
					IL_3F:
					if (false)
					{
					}
					throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ే⽉⩋⽍╏㹑⁓ѕ㝗ⵙᑛ㭝य़աౣብ", a_), RecordTableEnumerator.b("ే⽉⩋⽍╏㹑⁓ѕ㝗ⵙᑛ㭝य़աౣብ䡧ᥩѫŭկṱၳ噵᩷ό屻᥽겋揄望뚕ﾙ躟", a_));
				}
				this.DefaultPrintRowHeight = (int)(value * 20.0);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00031480 File Offset: 0x00030480
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x000314CC File Offset: 0x000304CC
		public bool StandardHeightFlag
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
				base.ParseData();
				return this.\u1712.DefaultRowHeightFlag;
			}
			set
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
				base.ParseData();
				this.\u1712.DefaultRowHeightFlag = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0003151C File Offset: 0x0003051C
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00031564 File Offset: 0x00030564
		public double DefaultColumnWidth
		{
			get
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
				base.ParseData();
				return this.\u1713;
			}
			set
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
				base.ParseData();
				this.\u1713 = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000315AC File Offset: 0x000305AC
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x000315F0 File Offset: 0x000305F0
		public ExcelSheetType Type
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
				return this.\u1719;
			}
			set
			{
				for (;;)
				{
					this.\u1719 = value;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_4E;
						case 1:
							base.IsSupported = true;
							num = 2;
							continue;
						case 2:
							return;
						case 3:
							if (this.\u1719 != ExcelSheetType.NormalWorksheet)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4E;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 4:
							if (!base.IsSupported)
							{
								num = 0;
								continue;
							}
							return;
						}
						break;
						IL_4E:
						num = 3;
					}
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00031698 File Offset: 0x00030698
		public XlsRange Range
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						base.ParseData();
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 10;
								continue;
							case 1:
								goto IL_9C;
							case 2:
								if (this.m_iFirstRow == this.m_iLastRow)
								{
									num = 4;
									continue;
								}
								goto IL_EC;
							case 3:
								if (this.m_iFirstColumn == this.m_iLastColumn)
								{
									num = 0;
									continue;
								}
								goto IL_C1;
							case 4:
								num = 11;
								continue;
							case 5:
								goto IL_12C;
							case 6:
								this.ᜎ.Dispose();
								goto IL_8B;
							case 7:
								goto IL_15E;
							case 8:
								goto IL_C1;
							case 9:
								goto IL_1A0;
							case 10:
								if (this.m_iFirstColumn != 2147483647)
								{
									num = 8;
									continue;
								}
								goto IL_9C;
							case 11:
								if (this.m_iFirstRow < 0)
								{
									num = 1;
									continue;
								}
								goto IL_EC;
							case 12:
								if (this.ᜎ != null)
								{
									num = 6;
									continue;
								}
								goto IL_15E;
							}
							break;
							IL_8B:
							num = 7;
							continue;
							IL_15E:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8B;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								this.ᜎ = base.AppImplementation.ᜂ(this);
								num = 9;
								continue;
							}
							IL_9C:
							num = 12;
							continue;
							IL_C1:
							num = 2;
							continue;
							IL_EC:
							int iFirstRow = this.m_iFirstRow;
							int iFirstColumn = this.m_iFirstColumn;
							int iLastRow = this.m_iLastRow;
							int iLastColumn = this.m_iLastColumn;
							this.ᜀ(ref iFirstRow, ref iFirstColumn, ref iLastRow, ref iLastColumn);
							this.ᜀ(iFirstRow, iFirstColumn, iLastRow, iLastColumn);
							num = 5;
						}
					}
					IL_12C:
					IL_1A0:
					return this.ᜎ;
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00031878 File Offset: 0x00030878
		internal IXLSRange[] UsedCells
		{
			get
			{
				if (true)
				{
				}
				switch (0)
				{
				default:
				{
					base.ParseData();
					List<IXLSRange> list = new List<IXLSRange>();
					int num = 0;
					IDictionaryEnumerator enumerator = this.ᜏ.GetEnumerator();
					try
					{
						int num2 = 3;
						for (;;)
						{
							DictionaryEntry dictionaryEntry;
							switch (num2)
							{
							case 0:
								goto IL_C9;
							case 1:
								if (dictionaryEntry.Value != null)
								{
									num2 = 0;
									continue;
								}
								goto IL_A9;
							case 2:
								if (!enumerator.MoveNext())
								{
									num2 = 5;
									continue;
								}
								dictionaryEntry = (DictionaryEntry)enumerator.Current;
								num2 = 1;
								continue;
							case 4:
								goto IL_110;
							case 5:
								num2 = 4;
								continue;
							case 6:
								goto IL_A9;
							}
							goto IL_61;
							IL_A9:
							num2 = 2;
							continue;
							IL_61:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
							{
								IL_C9:
								spr\u23A5 spr_u23A = dictionaryEntry.Value as spr\u23A5;
								list.Add(this.InnerGetCell(spr_u23A.ᜅ() + 1, spr_u23A.ᜄ() + 1));
								num++;
								num2 = 6;
								break;
							}
							default:
								if (false)
								{
								}
								goto IL_A9;
							}
						}
						IL_110:;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							int num2 = 0;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									if (disposable != null)
									{
										num2 = 2;
										continue;
									}
									goto IL_15A;
								case 1:
									goto IL_158;
								case 2:
									disposable.Dispose();
									num2 = 1;
									continue;
								}
								break;
							}
						}
						IL_158:
						IL_15A:;
					}
					return list.ToArray();
				}
				}
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00031A00 File Offset: 0x00030A00
		public IVPageBreaks VPageBreaks
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
				base.ParseData();
				return this.\u1712.VPageBreaks;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00031A4C File Offset: 0x00030A4C
		public bool IsEmpty
		{
			get
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
				base.ParseData();
				return this.m_iFirstRow == -1;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00031A98 File Offset: 0x00030A98
		protected internal IWorksheetCustomProperties CustomProperties
		{
			get
			{
				for (;;)
				{
					for (;;)
					{
						base.ParseData();
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (this.ᜫ == null)
								{
									num = 2;
									continue;
								}
								goto IL_77;
							case 1:
								goto IL_75;
							case 2:
								this.ᜫ = new spr\u256D();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									if (true)
									{
									}
									num = 1;
									continue;
								}
								break;
							}
							break;
						}
					}
				}
				IL_75:
				IL_77:
				return this.ᜫ;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00031B24 File Offset: 0x00030B24
		internal IMigrantRange MigrantRange
		{
			get
			{
				for (;;)
				{
					for (;;)
					{
						base.ParseData();
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (true)
								{
								}
								this.ᜃ();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							case 1:
								goto IL_70;
							case 2:
								if (this.ᜬ == null)
								{
									num = 0;
									continue;
								}
								goto IL_72;
							}
							break;
						}
					}
				}
				IL_70:
				IL_72:
				return this.ᜬ;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00031BAC File Offset: 0x00030BAC
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00031BF0 File Offset: 0x00030BF0
		public bool AllocatedRangeIncludesFormatting
		{
			get
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
				return this.ᜯ;
			}
			set
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
				this.ᜯ = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00031C34 File Offset: 0x00030C34
		public PivotTablesCollection PivotTables
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							this.ᜡ = new PivotTablesCollection((spr\u2158)base.AppImplementation, this);
							num = 2;
							continue;
						case 2:
							goto IL_71;
						}
						if (this.ᜡ != null)
						{
							break;
						}
						num = 1;
					}
					IL_71:
					break;
				}
				}
				return this.ᜡ;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00031CC4 File Offset: 0x00030CC4
		internal PivotTablesCollection InnerPivotTables
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
				return this.ᜡ;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00031D08 File Offset: 0x00030D08
		public IListObjects ListObjects
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.\u1732 = new ListObjectCollection();
							num = 2;
							continue;
						case 2:
							goto IL_65;
						}
						if (this.\u1732 != null)
						{
							break;
						}
						num = 0;
					}
					IL_65:
					break;
				}
				}
				return this.\u1732;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00031D8C File Offset: 0x00030D8C
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00031DD8 File Offset: 0x00030DD8
		public override bool ProtectContents
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
				return (this.InnerProtection & SheetProtectionType.Content) == SheetProtectionType.None;
			}
			internal set
			{
				if (!value)
				{
					if (true)
					{
					}
				}
				else
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
						this.InnerProtection &= ~SheetProtectionType.Content;
						return;
					}
				}
				this.InnerProtection |= SheetProtectionType.Content;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060005F4 RID: 1524 RVA: 0x00031E40 File Offset: 0x00030E40
		// (remove) Token: 0x060005F5 RID: 1525 RVA: 0x00031ED4 File Offset: 0x00030ED4
		public event XlsEventHandler ColumnWidthChanged
		{
			add
			{
				for (;;)
				{
					for (;;)
					{
						XlsEventHandler xlsEventHandler = this.ᝇ;
						int num = 2;
						for (;;)
						{
							XlsEventHandler xlsEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_72;
							case 1:
								if (xlsEventHandler != xlsEventHandler2)
								{
									goto IL_25;
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
									num = 0;
									continue;
								}
								break;
							case 2:
								goto IL_25;
							}
							break;
							IL_25:
							xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᝇ, value2, xlsEventHandler2);
							num = 1;
						}
					}
				}
				IL_72:
				if (true)
				{
				}
			}
			remove
			{
				for (;;)
				{
					for (;;)
					{
						XlsEventHandler xlsEventHandler = this.ᝇ;
						int num = 0;
						for (;;)
						{
							XlsEventHandler xlsEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_25;
							case 1:
								if (xlsEventHandler != xlsEventHandler2)
								{
									goto IL_25;
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
									num = 2;
									continue;
								}
								break;
							case 2:
								goto IL_72;
							}
							break;
							IL_25:
							xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᝇ, value2, xlsEventHandler2);
							num = 1;
						}
					}
				}
				IL_72:
				if (true)
				{
				}
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060005F6 RID: 1526 RVA: 0x00031F68 File Offset: 0x00030F68
		// (remove) Token: 0x060005F7 RID: 1527 RVA: 0x00032000 File Offset: 0x00031000
		public event XlsEventHandler RowHeightChanged
		{
			add
			{
				for (;;)
				{
					for (;;)
					{
						XlsEventHandler xlsEventHandler = this.ᝈ;
						int num = 1;
						for (;;)
						{
							XlsEventHandler xlsEventHandler2;
							switch (num)
							{
							case 0:
								if (true)
								{
								}
								if (xlsEventHandler != xlsEventHandler2)
								{
									goto IL_25;
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
									num = 2;
									continue;
								}
								break;
							case 1:
								goto IL_25;
							case 2:
								return;
							}
							break;
							IL_25:
							xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᝈ, value2, xlsEventHandler2);
							num = 0;
						}
					}
				}
			}
			remove
			{
				for (;;)
				{
					for (;;)
					{
						if (true)
						{
						}
						XlsEventHandler xlsEventHandler = this.ᝈ;
						int num = 0;
						for (;;)
						{
							XlsEventHandler xlsEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_2D;
							case 1:
								if (xlsEventHandler != xlsEventHandler2)
								{
									goto IL_2D;
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
									num = 2;
									continue;
								}
								break;
							case 2:
								return;
							}
							break;
							IL_2D:
							xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᝈ, value2, xlsEventHandler2);
							num = 1;
						}
					}
				}
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00032098 File Offset: 0x00031098
		public IInternalWorksheet GetClonedObject(Dictionary<string, string> hashNewNames, XlsWorkbook book)
		{
			string text;
			for (;;)
			{
				text = base.Name;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_44;
					case 1:
					{
						string text2;
						if (!hashNewNames.TryGetValue(text, out text2))
						{
							goto IL_92;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 2:
						if (hashNewNames != null)
						{
							num = 0;
							continue;
						}
						goto IL_92;
					case 3:
					{
						string text2;
						text = text2;
						num = 4;
						continue;
					}
					case 4:
						goto IL_42;
					}
					break;
					IL_44:
					if (true)
					{
					}
					num = 1;
				}
			}
			IL_42:
			IL_92:
			return book.Worksheets[text] as IInternalWorksheet;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00032148 File Offset: 0x00031148
		internal new void ᜀ(sprវ A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_41;
				case 1:
				{
					sprᡟ sprᡟ;
					List<spr\u21A7> a_;
					sprᡟ.ᜀ(a_, this);
					num = 0;
					continue;
				}
				case 3:
				{
					sprᡟ sprᡟ;
					if (sprᡟ == null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				case 4:
				{
					List<spr\u21A7> a_ = A_0.\u1713();
					sprᡟ sprᡟ = base.DataHolder;
					goto IL_59;
				}
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				return;
				IL_59:
				num = 3;
			}
			IL_41:
			if (true)
			{
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000321EC File Offset: 0x000311EC
		internal void \u171A()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 1:
					if (this.Version == ExcelVersion.Version2010)
					{
						num = 3;
						continue;
					}
					goto IL_81;
				case 2:
					if (true)
					{
					}
					num = 1;
					continue;
				case 3:
					goto IL_41;
				}
				if (this.Version != ExcelVersion.Version2007)
				{
					num = 2;
					continue;
				}
				IL_41:
				sprវ a_ = this.m_book.DataHolder;
				this.ᜀ(a_);
				num = 0;
			}
			IL_5C:
			IL_81:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5C;
			default:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00032298 File Offset: 0x00031298
		internal new sprᱧ ᜃ(int A_0, bool A_1)
		{
			base.ParseData();
			if (this.ᜏ != null)
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
					return this.ᜏ.Table.ᜀ(A_0, base.AppImplementation.ᜅ(), A_1, this.m_book.Version);
				}
			}
			return null;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00032310 File Offset: 0x00031310
		internal spr\u2502 ᜋ(int A_0)
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
			base.ParseData();
			return this.ᜃ(A_0 - 1, false);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00032360 File Offset: 0x00031360
		public override void UpdateExtendedFormatIndex(Dictionary<int, int> dictFormats)
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
			base.ParseData();
			base.UpdateExtendedFormatIndex(dictFormats);
			this.ᜏ.UpdateExtendedFormatIndex(dictFormats);
			this.ᜀ(this.ᜐ, dictFormats);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000323C4 File Offset: 0x000313C4
		public void UpdateExtendedFormatIndex(int maxCount)
		{
			int a_ = 1;
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 5;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_DE;
						case 1:
						{
							spr\u216E spr_u216E;
							int defaultXFIndex;
							spr_u216E.ᜃ((ushort)defaultXFIndex);
							num = 3;
							continue;
						}
						case 2:
							goto IL_DE;
						case 3:
							IL_7E:
							goto IL_110;
						case 4:
						{
							spr\u216E spr_u216E;
							if (spr_u216E != null)
							{
								num = 10;
								continue;
							}
							goto IL_110;
						}
						case 5:
						{
							if (maxCount <= 0)
							{
								num = 8;
								continue;
							}
							this.ᜏ.UpdateExtendedFormatIndex(maxCount);
							int defaultXFIndex = this.m_book.DefaultXFIndex;
							num2 = 0;
							int num3 = this.ᜐ.Length;
							num = 0;
							continue;
						}
						case 6:
						{
							spr\u216E spr_u216E;
							if ((int)spr_u216E.ᜌ() >= maxCount)
							{
								num = 1;
								continue;
							}
							goto IL_110;
						}
						case 7:
							return;
						case 8:
							goto IL_68;
						case 9:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 7;
								continue;
							}
							if (true)
							{
							}
							spr\u216E spr_u216E = this.ᜐ[num2];
							num = 4;
							continue;
						}
						case 10:
							num = 6;
							continue;
						}
						break;
						IL_DE:
						num = 9;
						continue;
						IL_110:
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7E;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
					}
				}
				IL_68:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("娶堸䌺縼倾㑀ⵂㅄ", a_));
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00032540 File Offset: 0x00031540
		protected internal RangeRichTextString CreateLabelSSTRTFString(long cellIndex)
		{
			RangeRichTextString result;
			for (;;)
			{
				result = null;
				IXLSRange range = this.ᜏ.GetRange(cellIndex);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_82;
					case 1:
						return result;
					case 2:
						return result;
					case 3:
						if (true)
						{
						}
						if (range != null)
						{
							num = 0;
							continue;
						}
						result = new RangeRichTextString(base.ReservedHandle, this, cellIndex);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_82;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
					IL_82:
					result = (RangeRichTextString)range.RichText;
					num = 2;
				}
			}
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000325EC File Offset: 0x000315EC
		protected internal CellRange[] Find(XlsRange range, byte findValue, bool bIsError, bool bIsFindFirst)
		{
			int a_ = 11;
			if (range != null)
			{
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
					base.ParseData();
					List<long> a_2 = this.ᜏ.Find(range, findValue, bIsError, bIsFindFirst);
					return this.ᜀ(a_2);
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀≂⭄⁆ⱈ", a_));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00032668 File Offset: 0x00031668
		protected internal CellRange[] Find(IXLSRange range, double findValue, FindType flags, bool bIsFindFirst)
		{
			int a_ = 0;
			if (true)
			{
			}
			if (range != null)
			{
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
					base.ParseData();
					List<long> a_2 = this.ᜏ.Find(range, findValue, flags, bIsFindFirst);
					return this.ᜀ(a_2);
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽", a_));
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000326E4 File Offset: 0x000316E4
		protected internal CellRange[] Find(XlsRange range, string findValue, FindType flags, bool bIsFindFirst)
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
			return this.ᜀ(range, findValue, flags, ExcelFindOptions.None, bIsFindFirst);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0003272C File Offset: 0x0003172C
		internal new CellRange[] ᜀ(IXLSRange A_0, string A_1, FindType A_2, ExcelFindOptions A_3, bool A_4)
		{
			int a_ = 1;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					goto IL_5E;
				case 2:
					if (A_1.Length == 0)
					{
						num = 1;
						continue;
					}
					goto IL_B1;
				case 3:
					if (A_1 != null)
					{
						num = 5;
						continue;
					}
					goto IL_AF;
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B1;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 3;
				}
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔶堸唺娼娾", a_));
			IL_5E:
			IL_AF:
			return null;
			IL_B1:
			base.ParseData();
			List<long> a_2 = this.ᜏ.ᜀ(A_0, A_1, A_2, A_3, A_4);
			return this.ᜀ(a_2);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0003280C File Offset: 0x0003180C
		internal new void ᜀ(IXLSRange A_0, IXLSRange A_1, CopyRangeOptions A_2, bool A_3)
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
			this.ᜀ(A_0, A_1, A_2, A_3, null);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00032854 File Offset: 0x00031854
		internal new void ᜀ(IXLSRange A_0, IXLSRange A_1, CopyRangeOptions A_2, bool A_3, sprᯣ A_4)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 6;
					for (;;)
					{
						XlsRange xlsRange;
						Rectangle rectangle;
						Rectangle rectangle2;
						XlsWorksheet xlsWorksheet;
						XlsWorksheet xlsWorksheet2;
						sprủ sprủ;
						switch (num)
						{
						case 0:
						{
							int num2;
							if (num2 > xlsRange.FirstRow)
							{
								num = 18;
								continue;
							}
							goto IL_F8;
						}
						case 1:
							if ((A_2 & CopyRangeOptions.CopyErrorIndicators) != CopyRangeOptions.None)
							{
								num = 31;
								continue;
							}
							goto IL_4AA;
						case 2:
							if ((A_2 & CopyRangeOptions.UpdateMerges) != CopyRangeOptions.None)
							{
								num = 30;
								continue;
							}
							goto IL_415;
						case 3:
							goto IL_38C;
						case 4:
							goto IL_11E;
						case 5:
							goto IL_415;
						case 6:
							if (A_0 == A_1)
							{
								if (true)
								{
								}
								num = 21;
								continue;
							}
							num = 13;
							continue;
						case 7:
							if (A_4 != null)
							{
								num = 19;
								continue;
							}
							goto IL_11E;
						case 8:
							if ((A_2 & CopyRangeOptions.CopyShapes) != CopyRangeOptions.None)
							{
								num = 33;
								continue;
							}
							goto IL_5AF;
						case 9:
							goto IL_F8;
						case 10:
						{
							int num3;
							if (num3 > xlsRange.FirstColumn)
							{
								num = 29;
								continue;
							}
							goto IL_4E8;
						}
						case 11:
						{
							int iSourceIndex;
							int iDestIndex;
							this.m_book.UpdateFormula(iSourceIndex, rectangle, iDestIndex, rectangle2);
							num = 22;
							continue;
						}
						case 12:
							if ((A_2 & CopyRangeOptions.CopyConditionalFormats) == CopyRangeOptions.CopyConditionalFormats)
							{
								num = 27;
								continue;
							}
							goto IL_D4;
						case 13:
							if (this.CanMove(ref A_0, A_1))
							{
								num = 17;
								continue;
							}
							goto IL_3B1;
						case 14:
							goto IL_4AA;
						case 15:
							xlsWorksheet.ᜂ(rectangle.Y + 1, rectangle.X + 1, rectangle.Height + 1, rectangle.Width + 1, rectangle2.Y + 1, rectangle2.X + 1, xlsWorksheet2, true);
							num = 3;
							continue;
						case 16:
							goto IL_3B7;
						case 17:
							xlsWorksheet2 = (XlsWorksheet)A_0.Worksheet;
							xlsWorksheet = (XlsWorksheet)A_1.Worksheet;
							num = 7;
							continue;
						case 18:
						{
							int num2;
							sprᜑ.ᜀ(xlsWorksheet2, num2);
							num = 9;
							continue;
						}
						case 19:
							A_4.ᜀ();
							num = 4;
							continue;
						case 20:
							goto IL_4E8;
						case 21:
							return;
						case 22:
							goto IL_482;
						case 23:
							if (sprủ != null)
							{
								num = 24;
								continue;
							}
							goto IL_3B7;
						case 24:
							sprủ.ᜉ();
							num = 16;
							continue;
						case 25:
							IL_2F2:
							goto IL_D4;
						case 26:
							if ((A_2 & CopyRangeOptions.CopyDataValidations) == CopyRangeOptions.CopyDataValidations)
							{
								num = 15;
								continue;
							}
							goto IL_38C;
						case 27:
							xlsWorksheet.ᜁ(rectangle.Y + 1, rectangle.X + 1, rectangle.Height + 1, rectangle.Width + 1, rectangle2.Y + 1, rectangle2.X + 1, xlsWorksheet2, true);
							num = 25;
							continue;
						case 28:
						{
							bool flag;
							if (flag)
							{
								num = 11;
								continue;
							}
							goto IL_482;
						}
						case 29:
						{
							int num3;
							sprᜑ.ᜁ(xlsWorksheet2, num3);
							num = 20;
							continue;
						}
						case 30:
							XlsWorksheet.ᜀ(A_0, A_1, true);
							num = 5;
							continue;
						case 31:
							xlsWorksheet.ᜀ(rectangle.Y + 1, rectangle.X + 1, rectangle.Height + 1, rectangle.Width + 1, rectangle2.Y + 1, rectangle2.X + 1, xlsWorksheet2, true);
							num = 14;
							continue;
						case 32:
							goto IL_355;
						case 33:
							rectangle.X++;
							rectangle.Y++;
							rectangle2.X++;
							rectangle2.Y++;
							((spr\u22F9)base.Shapes).ᜀ(xlsWorksheet2, rectangle, rectangle2, false);
							num = 32;
							continue;
						}
						break;
						IL_D4:
						num = 28;
						continue;
						IL_11E:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F2;
						default:
						{
							if (false)
							{
							}
							int iSourceIndex = this.m_book.AddSheetReference(xlsWorksheet);
							int iDestIndex = this.m_book.AddSheetReference(xlsWorksheet2);
							this.m_book.AddSheetReference(xlsWorksheet);
							rectangle = Rectangle.FromLTRB(A_1.Column - 1, A_1.Row - 1, A_1.LastColumn - 1, A_1.LastRow - 1);
							rectangle2 = Rectangle.FromLTRB(A_0.Column - 1, A_0.Row - 1, A_0.LastColumn - 1, A_0.LastRow - 1);
							int row = A_0.Row;
							int row2 = A_1.Row;
							int column = A_0.Column;
							int column2 = A_1.Column;
							bool flag = (A_2 & CopyRangeOptions.UpdateFormulas) != CopyRangeOptions.None;
							xlsRange = (XlsRange)A_0;
							int num2 = 0;
							int num3 = 0;
							sprủ = this.ᜀ(A_1, A_0, ref num2, ref num3, xlsWorksheet.ᜏ);
							num = 2;
							continue;
						}
						}
						IL_F8:
						num = 1;
						continue;
						IL_38C:
						num = 8;
						continue;
						IL_3B7:
						sprᜑ.ᜀ(xlsWorksheet2, xlsRange.FirstRow);
						sprᜑ.ᜁ(xlsWorksheet2, xlsRange.FirstColumn);
						num = 10;
						continue;
						IL_415:
						xlsWorksheet.ᜀ(rectangle);
						xlsWorksheet2.CellRecords.ClearRange(rectangle2);
						this.ᜀ(sprủ, xlsWorksheet2.ᜏ.Table, A_3);
						num = 23;
						continue;
						IL_482:
						num = 26;
						continue;
						IL_4AA:
						num = 12;
						continue;
						IL_4E8:
						num = 0;
					}
				}
				return;
				IL_355:
				goto IL_5AF;
				IL_3B1:
				throw new sprṁ();
				IL_5AF:
				this.ᜃ((XlsRange)A_1, (XlsRange)A_0);
				return;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00032E24 File Offset: 0x00031E24
		protected internal IXLSRange CopyRange(IXLSRange destination, IXLSRange source)
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
			return this.ᜁ(destination, source, CopyRangeOptions.UpdateMerges);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00032E68 File Offset: 0x00031E68
		internal IXLSRange ᜁ(IXLSRange A_0, IXLSRange A_1, CopyRangeOptions A_2)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 24;
				for (;;)
				{
					IL_22:
					int num3;
					int num4;
					int num6;
					int num7;
					int column;
					int num8;
					int num9;
					int num10;
					int num11;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						int num5;
						XlsRange xlsRange = (XlsRange)A_0[num4, num5, num4 + num6 - 1, num5 + num7 - 1];
						num = 21;
						continue;
					}
					case 1:
						num = 18;
						continue;
					case 2:
						goto IL_207;
					case 3:
						goto IL_153;
					case 4:
						goto IL_22A;
					case 5:
						while (A_1 != null)
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
								base.ParseData();
								num = 6;
								goto IL_22;
							}
						}
						num = 19;
						continue;
					case 6:
					{
						if (A_1.Worksheet != this)
						{
							num = 3;
							continue;
						}
						XlsRange xlsRange2 = (XlsRange)A_1;
						XlsRange xlsRange = (XlsRange)A_0;
						num4 = A_0.Row;
						column = A_0.Column;
						num = 9;
						continue;
					}
					case 7:
						num8++;
						num4 += num6;
						num = 4;
						continue;
					case 8:
						return A_0;
					case 9:
					{
						XlsRange xlsRange;
						if (xlsRange.IsSingleCell)
						{
							num = 20;
							continue;
						}
						goto IL_34F;
					}
					case 10:
						goto IL_A0;
					case 11:
						if (num9 % num6 == 0)
						{
							num = 1;
							continue;
						}
						goto IL_33B;
					case 12:
					{
						XlsRange xlsRange2;
						int lastRow = num4 + xlsRange2.LastRow - xlsRange2.Row;
						int lastColumn = column + xlsRange2.LastColumn - xlsRange2.Column;
						XlsRange xlsRange = (XlsRange)xlsRange[num4, column, lastRow, lastColumn];
						A_0 = xlsRange;
						num = 15;
						continue;
					}
					case 13:
						goto IL_33B;
					case 14:
						goto IL_207;
					case 15:
						goto IL_34F;
					case 16:
					{
						XlsRange xlsRange2;
						if (!xlsRange2.IsSingleCell)
						{
							num = 12;
							continue;
						}
						goto IL_34F;
					}
					case 17:
					{
						if (num8 >= num10)
						{
							num = 8;
							continue;
						}
						int num2 = 0;
						int num5 = column;
						num = 14;
						continue;
					}
					case 18:
						if (num11 % num7 == 0)
						{
							num = 22;
							continue;
						}
						goto IL_33B;
					case 19:
						goto IL_2E9;
					case 20:
						num = 16;
						continue;
					case 21:
					{
						if (true)
						{
						}
						XlsRange xlsRange;
						if (!xlsRange.AreFormulaArraysNotSeparated)
						{
							num = 25;
							continue;
						}
						XlsRange xlsRange2;
						this.ᜀ(xlsRange2, xlsRange, A_2);
						int num2;
						num2++;
						int num5;
						num5 += num7;
						num = 2;
						continue;
					}
					case 22:
						num10 = num9 / num6;
						num3 = num11 / num7;
						num = 13;
						continue;
					case 23:
						goto IL_22A;
					case 25:
						goto IL_E9;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 5;
					continue;
					IL_207:
					num = 0;
					continue;
					IL_22A:
					num = 17;
					continue;
					IL_33B:
					num8 = 0;
					num = 23;
					continue;
					IL_34F:
					num9 = A_0.LastRow - num4 + 1;
					num11 = A_0.LastColumn - column + 1;
					num6 = A_1.LastRow - A_1.Row + 1;
					num7 = A_1.LastColumn - A_1.Column + 1;
					num10 = 1;
					num3 = 1;
					num = 11;
				}
				IL_A0:
				throw new ArgumentNullException(RecordTableEnumerator.b("╀♂㙄㍆⁈╊ⱌ㭎㡐㱒㭔", a_));
				IL_E9:
				throw new sprṁ();
				IL_153:
				XlsWorksheet xlsWorksheet = (XlsWorksheet)A_1.Worksheet;
				return xlsWorksheet.CopyRange(A_0, A_1);
				IL_2E9:
				throw new ArgumentNullException(RecordTableEnumerator.b("㉀ⱂい㕆⩈⹊", a_));
			}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0003322C File Offset: 0x0003222C
		private new void ᜂ(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5, XlsWorksheet A_6, bool A_7)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					XlsDataValidationTable xlsDataValidationTable;
					bool flag;
					bool flag2;
					bool flag3;
					bool flag4;
					switch (num)
					{
					case 0:
						if (this.\u171F != null)
						{
							num = 13;
							continue;
						}
						num = 3;
						continue;
					case 1:
						if (xlsDataValidationTable.Count == 0)
						{
							num = 9;
							continue;
						}
						return;
					case 2:
						xlsDataValidationTable = A_6.DVTable;
						num = 20;
						continue;
					case 3:
						flag = true;
						goto IL_26D;
					case 4:
						num = 8;
						continue;
					case 5:
						num = 27;
						continue;
					case 7:
						goto IL_CC;
					case 8:
						if (A_5 != 2147483647)
						{
							num = 23;
							continue;
						}
						return;
					case 9:
						A_6.\u171F = null;
						num = 14;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_359;
						default:
							if (false)
							{
							}
							if (this.\u171F.Count == 0)
							{
								num = 31;
								continue;
							}
							goto IL_28F;
						}
						break;
					case 11:
						return;
					case 12:
						num = 19;
						continue;
					case 13:
						num = 24;
						continue;
					case 14:
						return;
					case 15:
						if (flag2)
						{
							num = 2;
							continue;
						}
						goto IL_CC;
					case 16:
						flag3 = (xlsDataValidationTable.Count == 0);
						goto IL_21E;
					case 17:
						if (A_0 != -1)
						{
							num = 12;
							continue;
						}
						return;
					case 18:
						goto IL_359;
					case 19:
						if (A_4 == -1)
						{
							num = 29;
							continue;
						}
						xlsDataValidationTable = A_6.\u171F;
						num = 0;
						continue;
					case 20:
						goto IL_CC;
					case 21:
					{
						Rectangle rectangle = new Rectangle(A_5 - 1, A_4 - 1, A_3, A_2);
						xlsDataValidationTable.Remove(new Rectangle[]
						{
							rectangle
						});
						if (true)
						{
						}
						num = 7;
						continue;
					}
					case 22:
						flag3 = true;
						goto IL_21E;
					case 23:
						num = 17;
						continue;
					case 24:
						flag = (this.\u171F.Count == 0);
						goto IL_26D;
					case 25:
						goto IL_C7;
					case 26:
						if (flag4)
						{
							num = 5;
							continue;
						}
						goto IL_2D2;
					case 27:
						if (flag2)
						{
							num = 11;
							continue;
						}
						goto IL_2D2;
					case 28:
						goto IL_28F;
					case 29:
						goto IL_32A;
					case 30:
						num = 16;
						continue;
					case 31:
						this.\u171F = null;
						num = 28;
						continue;
					case 32:
						if (xlsDataValidationTable != null)
						{
							num = 30;
							continue;
						}
						num = 22;
						continue;
					case 33:
						if (flag4)
						{
							num = 21;
							continue;
						}
						num = 15;
						continue;
					}
					if (A_6 == null)
					{
						num = 25;
						continue;
					}
					A_6.ParseData();
					base.ParseData();
					num = 18;
					continue;
					IL_CC:
					this.\u171F.ᜀ(xlsDataValidationTable, A_0, A_1, A_4, A_5, A_2, A_3, A_7);
					num = 10;
					continue;
					IL_21E:
					flag2 = flag3;
					num = 26;
					continue;
					IL_26D:
					flag4 = flag;
					num = 32;
					continue;
					IL_28F:
					num = 1;
					continue;
					IL_2D2:
					num = 33;
					continue;
					IL_359:
					if (A_1 == 2147483647)
					{
						return;
					}
					num = 4;
				}
				IL_C7:
				throw new ArgumentNullException(RecordTableEnumerator.b("娽┿ㅁぃᕅ⁇⽉⥋㩍", a_));
				IL_32A:
				return;
			}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00033610 File Offset: 0x00032610
		internal new void ᜀ(XlsRange A_0, XlsRange A_1, CopyRangeOptions A_2)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					IL_22:
					sprủ sprủ;
					int row;
					int column;
					int num2;
					int num3;
					int row2;
					int column2;
					XlsWorksheet xlsWorksheet;
					Rectangle a_2;
					switch (num)
					{
					case 0:
						goto IL_38F;
					case 1:
						if (sprủ != null)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						goto IL_276;
					case 3:
						goto IL_271;
					case 4:
						if (true)
						{
						}
						sprủ.ᜉ();
						num = 3;
						continue;
					case 5:
						if ((A_2 & CopyRangeOptions.CopyDataValidations) != CopyRangeOptions.None)
						{
							num = 8;
							continue;
						}
						goto IL_1C0;
					case 6:
						if ((A_2 & CopyRangeOptions.CopyErrorIndicators) == CopyRangeOptions.CopyErrorIndicators)
						{
							num = 27;
							continue;
						}
						goto IL_D0;
					case 7:
						goto IL_F8;
					case 8:
						this.ᜂ(row, column, num2, num3, row2, column2, xlsWorksheet, false);
						num = 23;
						continue;
					case 9:
						this.ᜁ(row, column, num2, num3, row2, column2, xlsWorksheet, false);
						num = 15;
						continue;
					case 11:
					{
						Dictionary<spr\u225F, object> dictionary;
						xlsWorksheet.ᜀ(dictionary.Keys, true);
						num = 7;
						continue;
					}
					case 12:
						goto IL_33C;
					case 13:
						this.ᜀ(A_1, A_0);
						num = 2;
						continue;
					case 14:
					{
						Dictionary<spr\u225F, object> dictionary;
						if (dictionary != null)
						{
							num = 24;
							continue;
						}
						goto IL_F8;
					}
					case 15:
						goto IL_165;
					case 16:
					{
						Dictionary<spr\u225F, object> dictionary;
						if (dictionary.Count > 0)
						{
							num = 11;
							continue;
						}
						goto IL_F8;
					}
					case 17:
						if ((A_2 & CopyRangeOptions.UpdateMerges) != CopyRangeOptions.None)
						{
							num = 13;
							continue;
						}
						goto IL_276;
					case 18:
						if (A_1 == null)
						{
							num = 12;
							continue;
						}
						num = 21;
						continue;
					case 19:
						if ((A_2 & CopyRangeOptions.CopyShapes) != CopyRangeOptions.None)
						{
							num = 22;
							continue;
						}
						goto IL_38F;
					case 20:
						goto IL_CB;
					case 21:
					{
						if (!A_1.AreFormulaArraysNotSeparated)
						{
							num = 25;
							continue;
						}
						base.ParseData();
						num2 = A_0.LastRow - A_0.Row + 1;
						num3 = A_0.LastColumn - A_0.Column + 1;
						sprủ = this.ᜏ.ᜀ(A_1, A_0, out a_2);
						Dictionary<spr\u225F, object> dictionary = A_1.FormulaArrays;
						xlsWorksheet = (XlsWorksheet)A_1.Worksheet;
						num = 17;
						continue;
					}
					case 22:
					{
						Rectangle rectangle = new Rectangle(column, row, num3 - 1, num2 - 1);
						Rectangle a_3 = rectangle;
						a_3.X = column2;
						a_3.Y = row2;
						spr\u22F9 spr_u22F = (spr\u22F9)base.Shapes;
						spr_u22F.ᜀ(xlsWorksheet, rectangle, a_3, true);
						num = 0;
						continue;
					}
					case 23:
						goto IL_1C0;
					case 24:
						num = 16;
						continue;
					case 25:
						goto IL_160;
					case 26:
						if ((A_2 & CopyRangeOptions.CopyConditionalFormats) == CopyRangeOptions.CopyConditionalFormats)
						{
							num = 9;
							continue;
						}
						goto IL_165;
					case 27:
						this.ᜀ(row, column, num2, num3, row2, column2, xlsWorksheet, false);
						num = 28;
						continue;
					case 28:
						goto IL_D0;
					}
					while (A_0 == null)
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
							num = 20;
							goto IL_22;
						}
					}
					num = 18;
					continue;
					IL_D0:
					num = 26;
					continue;
					IL_F8:
					column2 = A_1.Column;
					row2 = A_1.Row;
					row = A_0.Row;
					column = A_0.Column;
					num = 19;
					continue;
					IL_165:
					num = 5;
					continue;
					IL_1C0:
					this.ᜀ(row, column, num2, num3, row2, column2, xlsWorksheet, sprủ, a_2, A_2);
					num = 1;
					continue;
					IL_276:
					num = 14;
					continue;
					IL_38F:
					A_1.ClearContents();
					num = 6;
				}
				IL_CB:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⭃㍅㩇⥉⥋", a_));
				IL_160:
				throw new sprṁ(RecordTableEnumerator.b("Ł╃⡅潇㹉汋ⵍ㽏≑ⵓ癕ⱗ㕙籛㩝՟ᅡၣཥ٧୩ᡫݭὯᱱ味ѵ᥷ᑹ᭻᭽깿", a_));
				IL_271:
				return;
				IL_33C:
				throw new ArgumentNullException(RecordTableEnumerator.b("♁⅃㕅㱇⍉≋⽍⑏㭑㭓㡕", a_));
			}
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00033A48 File Offset: 0x00032A48
		private void ᜁ(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5, XlsWorksheet A_6, bool A_7)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 22;
				for (;;)
				{
					int num2;
					int index;
					XlsConditionalFormats xlsConditionalFormats2;
					Rectangle a_2;
					Rectangle a_3;
					switch (num)
					{
					case 0:
						if (A_0 != -1)
						{
							num = 10;
							continue;
						}
						return;
					case 1:
						goto IL_24E;
					case 2:
						goto IL_34E;
					case 3:
						goto IL_A8;
					case 4:
						num = 8;
						continue;
					case 5:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 23;
							continue;
						}
						XlsConditionalFormats xlsConditionalFormats = this.ᜪ[num2];
						int num4 = A_5 - A_1;
						int num5 = A_4 - A_0;
						index = A_6.Index;
						XlsWorksheetConditionalFormats conditionalFormats;
						xlsConditionalFormats2 = xlsConditionalFormats.ᜀ(A_0, A_1, A_2, A_3, A_7, num5, num4, conditionalFormats);
						num = 13;
						continue;
					}
					case 6:
					{
						XlsWorksheetConditionalFormats conditionalFormats;
						conditionalFormats.Add(xlsConditionalFormats2);
						num = 24;
						continue;
					}
					case 7:
					{
						XlsConditionalFormats xlsConditionalFormats;
						this.ᜪ.ᜀ(xlsConditionalFormats);
						num2--;
						int num3;
						num3--;
						num = 1;
						continue;
					}
					case 8:
						if (xlsConditionalFormats2[0].FormatType == ConditionalFormatType.Formula)
						{
							num = 2;
							continue;
						}
						goto IL_AD;
					case 9:
						num = 0;
						continue;
					case 10:
						num = 20;
						continue;
					case 11:
						goto IL_24C;
					case 12:
						if (xlsConditionalFormats2 != null)
						{
							num = 4;
							continue;
						}
						goto IL_AD;
					case 13:
						if (!A_7)
						{
							num = 14;
							continue;
						}
						goto IL_AD;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34E;
						default:
						{
							if (false)
							{
							}
							a_2 = new Rectangle(A_1 - 1, A_0 - 1, A_3, A_2);
							a_3 = new Rectangle(A_1 - 1, A_0 - 1, 0, 0);
							int num4;
							int num5;
							a_3.Offset(num4, num5);
							num = 12;
							continue;
						}
						}
						break;
					case 15:
						if (xlsConditionalFormats2 != null)
						{
							num = 6;
							continue;
						}
						goto IL_197;
					case 16:
					{
						XlsConditionalFormats xlsConditionalFormats;
						if (xlsConditionalFormats.IsEmpty)
						{
							num = 7;
							continue;
						}
						goto IL_24E;
					}
					case 17:
						if (A_5 != 2147483647)
						{
							num = 9;
							continue;
						}
						return;
					case 18:
						num = 17;
						continue;
					case 19:
						goto IL_272;
					case 20:
					{
						if (A_4 == -1)
						{
							num = 11;
							continue;
						}
						XlsWorksheetConditionalFormats conditionalFormats = A_6.ConditionalFormats;
						num2 = 0;
						int num3 = this.ᜪ.Count;
						num = 19;
						continue;
					}
					case 21:
						goto IL_AD;
					case 23:
						return;
					case 24:
						goto IL_197;
					case 25:
						goto IL_272;
					case 26:
						if (A_1 != 2147483647)
						{
							num = 18;
							continue;
						}
						return;
					}
					if (A_6 == null)
					{
						num = 3;
						continue;
					}
					A_6.\u171A();
					this.\u171A();
					num = 26;
					continue;
					IL_AD:
					num = 16;
					continue;
					IL_197:
					num2++;
					num = 25;
					continue;
					IL_24E:
					num = 15;
					continue;
					IL_272:
					if (true)
					{
					}
					num = 5;
					continue;
					IL_34E:
					xlsConditionalFormats2.ᜀ(index, index, a_2, index, a_3);
					num = 21;
				}
				IL_A8:
				throw new ArgumentNullException(RecordTableEnumerator.b("≅ⵇ㥉㡋ᵍ㡏㝑ㅓ≕", a_));
				IL_24C:
				return;
			}
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00033DC8 File Offset: 0x00032DC8
		private new void ᜀ(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5, XlsWorksheet A_6, bool A_7)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int num3;
					spr\u1F7E spr_u1F7E;
					spr\u1F7E spr_u1F7E2;
					List<Rectangle> list;
					switch (num)
					{
					case 0:
						if (num2 >= A_2)
						{
							num = 16;
							continue;
						}
						num3 = 0;
						num = 11;
						continue;
					case 2:
					{
						if (num3 >= A_3)
						{
							num = 20;
							continue;
						}
						Rectangle rectangle = new Rectangle(A_1 + num3 - 1, A_0 + num2 - 1, 0, 0);
						Rectangle[] a_2 = new Rectangle[]
						{
							rectangle
						};
						spr_u1F7E = this.ᜮ.ᜀ(a_2);
						num = 13;
						continue;
					}
					case 3:
						goto IL_8D;
					case 4:
						goto IL_F6;
					case 5:
						goto IL_138;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_138;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 7:
						goto IL_14A;
					case 8:
						if (true)
						{
						}
						spr_u1F7E2.ᜃ(list);
						num = 5;
						continue;
					case 9:
						if (A_7)
						{
							num = 17;
							continue;
						}
						goto IL_16E;
					case 10:
						goto IL_16E;
					case 11:
						goto IL_F6;
					case 12:
						if (spr_u1F7E2 != null)
						{
							num = 15;
							continue;
						}
						goto IL_92;
					case 13:
						if (spr_u1F7E != null)
						{
							num = 6;
							continue;
						}
						goto IL_138;
					case 14:
						if (spr_u1F7E2.ᜁ() != spr_u1F7E.ᜁ())
						{
							num = 8;
							continue;
						}
						goto IL_92;
					case 15:
						num = 14;
						continue;
					case 16:
						return;
					case 17:
					{
						Rectangle[] a_2;
						spr_u1F7E.ᜀ(a_2);
						num = 10;
						continue;
					}
					case 18:
						goto IL_14A;
					case 19:
						goto IL_138;
					case 20:
						num2++;
						num = 18;
						continue;
					}
					if (A_6 == null)
					{
						num = 3;
						continue;
					}
					num2 = 0;
					num = 7;
					continue;
					IL_92:
					spr_u1F7E.ᜃ(list);
					spr\u2622 spr_u;
					spr_u.ᜀ(spr_u1F7E);
					num = 19;
					continue;
					IL_F6:
					num = 2;
					continue;
					IL_138:
					num3++;
					num = 4;
					continue;
					IL_14A:
					num = 0;
					continue;
					IL_16E:
					spr_u1F7E = new spr\u1F7E(spr_u1F7E.ᜁ());
					list = new List<Rectangle>();
					Rectangle rectangle2 = new Rectangle(A_5 + num3 - 1, A_4 + num2 - 1, 0, 0);
					list.Add(rectangle2);
					spr_u = A_6.ᜮ;
					spr_u1F7E2 = spr_u.ᜀ(new Rectangle[]
					{
						rectangle2
					});
					num = 12;
				}
				IL_8D:
				throw new ArgumentNullException(RecordTableEnumerator.b("尷弹伻䨽ጿ⩁⅃⍅㱇", a_));
			}
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000340C4 File Offset: 0x000330C4
		internal new void ᜀ(spr\u23A5 A_0, string A_1, IDictionary A_2, long A_3, XlsWorkbook A_4, Dictionary<int, int> A_5, CopyRangeOptions A_6)
		{
			if (true)
			{
			}
			for (;;)
			{
				base.ParseData();
				this.ᜏ.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
				A_0.ᜄ();
				A_0.ᜅ();
				XlsRange range = this.ᜏ.GetRange(A_3);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (range == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_91;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_91;
					}
					break;
					IL_91:
					range.\u171B();
					num = 0;
				}
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00034178 File Offset: 0x00033178
		internal new void ᜀ(int A_0, int A_1, int A_2, int A_3, int A_4, int A_5, XlsWorksheet A_6, sprủ A_7, Rectangle A_8, CopyRangeOptions A_9)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					Dictionary<int, int> a_ = null;
					Dictionary<int, int> a_2 = null;
					bool flag = (A_9 & CopyRangeOptions.CopyStyles) != CopyRangeOptions.None;
					int num = 15;
					for (;;)
					{
						Dictionary<long, long> dictionary;
						int num2;
						int num4;
						spr\u225F spr_u225F;
						int num5;
						int num6;
						int a_4;
						spr\u23A5 spr_u23A;
						spr\u23A5 spr_u23A2;
						int num8;
						sprᱧ sprᱧ2;
						XlsCellRecordCollection cellRecords;
						int num11;
						switch (num)
						{
						case 0:
						{
							long key;
							if (dictionary.ContainsKey(key))
							{
								num = 25;
								continue;
							}
							int a_3 = num2 - 1;
							int num3 = num4 - 1;
							long value = sprṔ.ᜀ(num3, a_3);
							dictionary[key] = value;
							spr_u225F.ᜃ(Math.Max(spr_u225F.ᜈ(), A_1 - 1) + num5);
							spr_u225F.ᜂ(Math.Max(spr_u225F.ᜉ(), A_0 - 1) + num6);
							spr_u225F.ᜁ(Math.Min(spr_u225F.ᜀ(), A_1 + A_3 - 2));
							spr\u225F spr_u225F2 = spr_u225F;
							spr_u225F2.ᜁ(spr_u225F2.ᜀ() + num5);
							spr_u225F.ᜀ(Math.Min(spr_u225F.\u170D(), A_0 + A_2 - 2));
							spr\u225F spr_u225F3 = spr_u225F;
							spr_u225F3.ᜀ(spr_u225F3.\u170D() + num6);
							num = 27;
							continue;
						}
						case 1:
							goto IL_1E5;
						case 2:
							goto IL_464;
						case 3:
							goto IL_2CE;
						case 4:
							return;
						case 5:
						{
							sprᱧ sprᱧ;
							int num7;
							spr_u23A = sprᱧ.ᜆ(num7 - 1, a_4);
							goto IL_3F9;
						}
						case 6:
							if (spr_u23A2.get_TypeCode() == TBIFFRecord.Formula)
							{
								num = 12;
								continue;
							}
							goto IL_1E5;
						case 7:
							spr_u23A = null;
							goto IL_3F9;
						case 8:
							goto IL_D2;
						case 9:
						{
							if (num8 >= A_3)
							{
								num = 23;
								continue;
							}
							num4 = A_5 + num8;
							int num7 = A_1 + num8;
							long num9 = sprṔ.ᜀ(num4, num2);
							int num10;
							sprủ sprủ = this.ᜀ(num10, num7, A_8, A_7, this.ᜏ.Table);
							sprᱧ sprᱧ = sprủ.ᜄ().ᜁ(num10 - 1);
							num = 18;
							continue;
						}
						case 10:
							goto IL_4E5;
						case 11:
							num = 7;
							continue;
						case 12:
						{
							sprủ sprủ;
							spr_u225F = sprủ.ᜀ(spr_u23A2);
							num = 24;
							continue;
						}
						case 13:
							goto IL_1E5;
						case 14:
							goto IL_2FC;
						case 15:
							if (flag)
							{
								num = 29;
								continue;
							}
							goto IL_4E5;
						case 16:
						{
							int a_3;
							int num3;
							sprᱧ2.ᜀ(num4 - 1, a_3, num3, base.ReservedHandle.\u171D());
							num = 13;
							continue;
						}
						case 17:
							if (sprᱧ2 != null)
							{
								num = 16;
								continue;
							}
							goto IL_1E5;
						case 18:
						{
							sprᱧ sprᱧ;
							if (sprᱧ == null)
							{
								num = 11;
								continue;
							}
							num = 5;
							continue;
						}
						case 19:
						{
							sprᱧ sprᱧ;
							int num7;
							string a_5 = sprᱧ.ᜌ(num7 - 1);
							long num9;
							A_6.ᜀ(spr_u23A2, a_5, a_2, num9, this.m_book, a_, A_9);
							num = 6;
							continue;
						}
						case 20:
						{
							if (spr_u23A2 != null)
							{
								num = 19;
								continue;
							}
							long num9;
							cellRecords.Remove(num9);
							num = 1;
							continue;
						}
						case 21:
							goto IL_D2;
						case 22:
							goto IL_2FC;
						case 23:
							if (true)
							{
							}
							num11++;
							num = 14;
							continue;
						case 24:
							if (spr_u225F != null)
							{
								num = 26;
								continue;
							}
							goto IL_1E5;
						case 25:
						{
							long key;
							long a_6 = dictionary[key];
							int a_3 = sprṔ.ᜁ(a_6);
							int num3 = sprṔ.ᜀ(a_6);
							num = 8;
							continue;
						}
						case 26:
						{
							long key = sprṔ.ᜀ(spr_u225F.ᜈ(), spr_u225F.ᜉ());
							int a_3 = 0;
							int num3 = 0;
							num = 0;
							continue;
						}
						case 27:
							if ((A_9 & CopyRangeOptions.UpdateFormulas) != CopyRangeOptions.None)
							{
								num = 30;
								continue;
							}
							goto IL_464;
						case 28:
						{
							if (num11 >= A_2)
							{
								num = 4;
								continue;
							}
							num2 = A_4 + num11;
							int num10 = A_0 + num11;
							num8 = 0;
							num = 31;
							continue;
						}
						case 29:
							a_2 = this.ᜀ(A_0, A_1, A_2, A_3, A_6, out a_);
							num = 10;
							continue;
						case 30:
							this.ᜀ(spr_u225F, A_6, num6, num5);
							num = 2;
							continue;
						case 31:
							goto IL_2CE;
						}
						break;
						IL_D2:
						sprủ sprủ2;
						sprᱧ2 = sprủ2.ᜄ().ᜁ(num2 - 1);
						num = 17;
						continue;
						IL_1E5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D2;
						}
						if (false)
						{
						}
						num8++;
						num = 3;
						continue;
						IL_2CE:
						num = 9;
						continue;
						IL_2FC:
						num = 28;
						continue;
						IL_3F9:
						spr_u23A2 = spr_u23A;
						num = 20;
						continue;
						IL_464:
						sprᱧ sprᱧ3 = sprủ2.ᜄ().ᜁ(num2 - 1);
						sprᱧ3.ᜀ(num4 - 1, spr_u225F, base.ReservedHandle.\u171D());
						num = 21;
						continue;
						IL_4E5:
						cellRecords = A_6.CellRecords;
						num5 = A_5 - A_1;
						num6 = A_4 - A_0;
						dictionary = new Dictionary<long, long>();
						sprủ2 = A_6.CellRecords.Table;
						a_4 = base.ReservedHandle.\u171D();
						num11 = 0;
						num = 22;
					}
				}
				return;
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000346D4 File Offset: 0x000336D4
		internal new void ᜀ(ICollection<spr\u225F> A_0, bool A_1)
		{
			int a_ = 11;
			int num = 1;
			for (;;)
			{
				IEnumerator<spr\u225F> enumerator;
				switch (num)
				{
				case 0:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 2;
								continue;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									goto IL_B9;
								}
								break;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								spr\u225F a_2 = enumerator.Current;
								this.ᜀ(a_2, A_1);
								num = 3;
								continue;
							}
							}
							IL_81:
							num = 4;
							continue;
							goto IL_81;
						}
						IL_B9:
						if (false)
						{
						}
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								enumerator.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_F8;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 0;
						}
						IL_F8:;
					}
					goto IL_FB;
				case 2:
					goto IL_3D;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_FB:
				if (true)
				{
				}
				base.ParseData();
				enumerator = A_0.GetEnumerator();
				num = 0;
			}
			IL_3D:
			throw new ArgumentNullException(RecordTableEnumerator.b("≀ⱂ⥄ᕆⱈ♊≌㥎㑐", a_));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00034828 File Offset: 0x00033828
		internal new void ᜀ(spr\u225F A_0, bool A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					if (true)
					{
					}
					int num = 8;
					for (;;)
					{
						int num2;
						int num3;
						int num5;
						int num6;
						int num7;
						switch (num)
						{
						case 0:
							goto IL_98;
						case 1:
							goto IL_93;
						case 2:
						{
							if (num2 > num3)
							{
								num = 12;
								continue;
							}
							int num4 = num5;
							num = 7;
							continue;
						}
						case 3:
							goto IL_16B;
						case 4:
							num2++;
							num = 3;
							continue;
						case 5:
							goto IL_18D;
						case 6:
							if (A_1)
							{
								num = 11;
								continue;
							}
							goto IL_18D;
						case 7:
							goto IL_98;
						case 8:
							if (A_0 != null)
							{
								num6 = A_0.ᜉ() + 1;
								num5 = A_0.ᜈ() + 1;
								num3 = A_0.\u170D() + 1;
								num7 = A_0.ᜀ() + 1;
								num = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19E;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 9:
						{
							int num4;
							if (num4 > num7)
							{
								num = 4;
								continue;
							}
							this.ᜏ.ᜁ(num2, num4, null);
							num4++;
							num = 0;
							continue;
						}
						case 10:
							goto IL_16B;
						case 11:
							goto IL_19E;
						case 12:
							return;
						}
						break;
						IL_98:
						num = 9;
						continue;
						IL_16B:
						num = 2;
						continue;
						IL_18D:
						num2 = num6;
						num = 10;
						continue;
						IL_19E:
						this.AllocatedRange[num6, num5, num3, num7].Text = "";
						num = 5;
					}
				}
				IL_93:
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿", a_));
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000349FC File Offset: 0x000339FC
		internal new Ptg[] ᜀ(Ptg[] A_0, int A_1, int A_2)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					Ptg[] array;
					bool flag;
					int num2;
					bool flag2;
					int num3;
					switch (num)
					{
					case 1:
						goto IL_135;
					case 2:
						if (true)
						{
						}
						if (A_1 == 0)
						{
							num = 13;
							continue;
						}
						num = 6;
						continue;
					case 3:
						return array;
					case 4:
						if (flag)
						{
							goto IL_96;
						}
						array[num2] = (Ptg)A_0[num2].Clone();
						num = 9;
						continue;
					case 5:
						goto IL_6D;
					case 6:
						flag2 = true;
						goto IL_184;
					case 7:
						goto IL_135;
					case 8:
						array[num2] = A_0[num2].Offset(A_1, A_2, this.m_book);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_96;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 9:
						goto IL_D2;
					case 10:
						goto IL_D2;
					case 11:
						if (num2 >= num3)
						{
							num = 3;
							continue;
						}
						num = 4;
						continue;
					case 12:
						flag2 = (A_2 != 0);
						goto IL_184;
					case 13:
						num = 12;
						continue;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					base.ParseData();
					num = 2;
					continue;
					IL_96:
					num = 8;
					continue;
					IL_D2:
					num2++;
					num = 1;
					continue;
					IL_135:
					num = 11;
					continue;
					IL_184:
					flag = flag2;
					array = new Ptg[A_0.Length];
					num2 = 0;
					num3 = A_0.Length;
					num = 7;
				}
				IL_6D:
				throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似社⹀ㅂ⡄㉆╈⩊", a_));
			}
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00034BAC File Offset: 0x00033BAC
		public override void UpdateFormula(int currentIndex, int srcIndex, Rectangle srcRect, int destIndex, Rectangle destRect)
		{
			for (;;)
			{
				IL_30:
				base.ParseData();
				this.ᜏ.UpdateFormula(currentIndex, srcIndex, srcRect, destIndex, destRect);
				base.UpdateFormula(currentIndex, srcIndex, srcRect, destIndex, destRect);
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜪ.ᜀ(currentIndex, srcIndex, srcRect, destIndex, destRect);
							num = 1;
							continue;
						case 1:
							goto IL_96;
						case 2:
							if (this.ᜪ != null)
							{
								num = 0;
								continue;
							}
							goto IL_98;
						}
						goto IL_30;
					}
				}
			}
			IL_96:
			IL_98:
			if (true)
			{
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00034C5C File Offset: 0x00033C5C
		public void AutoFitRow(int rowIndex)
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
			int column = this.AllocatedRange.Column;
			int lastColumn = this.AllocatedRange.LastColumn;
			this.AutofitRow(rowIndex, column, lastColumn, true);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00034CBC File Offset: 0x00033CBC
		public void AutoFitColumn(int columnIndex)
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
			this.AutofitColumn(columnIndex, this.AllocatedRange.Row, this.AllocatedRange.LastRow);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00034D14 File Offset: 0x00033D14
		public void AutofitColumn(int columnIndex, int firstRow, int lastRow)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 19;
				double num2;
				for (;;)
				{
					int num4;
					switch (num)
					{
					case 0:
						num2 = 255.0;
						num = 8;
						continue;
					case 1:
					{
						SizeF sizeF;
						SizeF sizeF2;
						if (sizeF.Width < sizeF2.Width)
						{
							num = 18;
							continue;
						}
						goto IL_155;
					}
					case 2:
						if (firstRow != 0)
						{
							num = 21;
							continue;
						}
						return;
					case 3:
					{
						long num3;
						RichTextString a_2;
						bool flag;
						SizeF sizeF2 = this.ᜀ(num3, false, a_2, false, out flag);
						num = 1;
						continue;
					}
					case 4:
						num = 5;
						continue;
					case 5:
					{
						if (firstRow > lastRow)
						{
							num = 13;
							continue;
						}
						SizeF sizeF = new SizeF(0f, 0f);
						RichTextString a_2 = new RichTextString(base.AppImplementation, this, false, true);
						bool flag = false;
						num4 = firstRow;
						num = 7;
						continue;
					}
					case 6:
					{
						long num3;
						if (this.ᜏ.Contains(num3))
						{
							num = 3;
							continue;
						}
						goto IL_155;
					}
					case 7:
						goto IL_A2;
					case 8:
						goto IL_13C;
					case 9:
						if (num2 == 0.0)
						{
							num = 24;
							continue;
						}
						goto IL_330;
					case 10:
						if (num2 > 255.0)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						num = 9;
						continue;
					case 11:
						goto IL_A2;
					case 12:
						if (lastRow != 0)
						{
							num = 4;
							continue;
						}
						return;
					case 13:
						goto IL_2D9;
					case 14:
						if (columnIndex > this.m_book.MaxColumnCount)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D9;
							}
							if (false)
							{
							}
							num = 22;
							continue;
						}
						base.ParseData();
						num = 2;
						continue;
					case 15:
						goto IL_155;
					case 16:
					{
						if (num4 > lastRow)
						{
							num = 17;
							continue;
						}
						long num3 = sprṔ.ᜀ(columnIndex, num4);
						num = 6;
						continue;
					}
					case 17:
					{
						SizeF sizeF;
						num2 = Math.Ceiling(this.PixelsToColumnWidth((double)((int)Math.Ceiling((double)sizeF.Width))));
						num = 10;
						continue;
					}
					case 18:
					{
						SizeF sizeF;
						SizeF sizeF2;
						sizeF.Width = sizeF2.Width;
						num = 15;
						continue;
					}
					case 20:
						num = 14;
						continue;
					case 21:
						num = 12;
						continue;
					case 22:
						goto IL_2B8;
					case 23:
						goto IL_189;
					case 24:
						num2 = (double)spr\u17FF.\u170D.Width;
						num = 23;
						continue;
					}
					if (columnIndex >= 1)
					{
						num = 20;
						continue;
					}
					goto IL_141;
					IL_A2:
					num = 16;
					continue;
					IL_155:
					num4++;
					num = 11;
				}
				IL_13C:
				goto IL_330;
				IL_141:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("≀ⱂ⥄ๆ❈⽊⡌㝎", a_));
				IL_189:
				goto IL_330;
				IL_2B8:
				goto IL_141;
				IL_2D9:
				return;
				IL_330:
				this.SetColumnWidth(columnIndex, num2);
				return;
			}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0003505C File Offset: 0x0003405C
		internal new void ᜀ(XlsWorksheet A_0, Dictionary<string, string> A_1, Dictionary<string, string> A_2, Dictionary<int, int> A_3, WorksheetCopyType A_4)
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
			Dictionary<int, int> hashExtFormatIndexes = new Dictionary<int, int>();
			Dictionary<int, int> hashNameIndexes = new Dictionary<int, int>();
			Dictionary<int, int> hashExternSheets = new Dictionary<int, int>();
			this.CopyFrom(A_0, A_1, A_2, A_3, A_4, hashExtFormatIndexes, hashNameIndexes, hashExternSheets);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000350BC File Offset: 0x000340BC
		internal new void ᜀ(XlsWorksheet A_0, Dictionary<string, string> A_1, Dictionary<string, string> A_2, Dictionary<int, int> A_3, WorksheetCopyType A_4, Dictionary<int, int> A_5, Dictionary<int, int> A_6)
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
			this.CopyFrom(A_0, A_1, A_2, A_3, A_4, A_5, A_6, new Dictionary<int, int>(0));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00035110 File Offset: 0x00034110
		public void CopyFrom(XlsWorksheet worksheet, Dictionary<string, string> hashStyleNames, Dictionary<string, string> hashWorksheetNames, Dictionary<int, int> dicFontIndexes, WorksheetCopyType flags, Dictionary<int, int> hashExtFormatIndexes, Dictionary<int, int> hashNameIndexes, Dictionary<int, int> hashExternSheets)
		{
			for (;;)
			{
				base.ParseData();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.CopyColumnWidth(worksheet, hashExtFormatIndexes);
						flags &= ~WorksheetCopyType.CopyColumnHeight;
						num = 11;
						continue;
					case 1:
						goto IL_1D5;
					case 2:
						this.CopyRowHeight(worksheet, hashExtFormatIndexes);
						flags &= ~WorksheetCopyType.CopyRowHeight;
						num = 1;
						continue;
					case 3:
						goto IL_406;
					case 4:
						this.m_iFirstRow = worksheet.m_iFirstRow;
						this.m_iLastRow = worksheet.m_iLastRow;
						this.m_iFirstColumn = worksheet.m_iFirstColumn;
						this.Zoom = worksheet.Zoom;
						this.m_iLastColumn = worksheet.m_iLastColumn;
						this.ᜏ.CopyCells(worksheet.ᜏ, hashStyleNames, hashWorksheetNames, hashExtFormatIndexes, hashNameIndexes, dicFontIndexes, hashExternSheets);
						this.ᜀ(worksheet.ᜮ);
						this.ᜀ(worksheet.ᜢ);
						num = 21;
						continue;
					case 5:
						if ((flags & WorksheetCopyType.CopyPageSetup) != WorksheetCopyType.None)
						{
							num = 12;
							continue;
						}
						goto IL_11B;
					case 6:
						goto IL_160;
					case 7:
						if ((flags & WorksheetCopyType.ClearBefore) != WorksheetCopyType.None)
						{
							if (true)
							{
							}
							num = 17;
							continue;
						}
						goto IL_3E1;
					case 8:
						goto IL_3E1;
					case 9:
						this.CopyMerges(worksheet);
						num = 16;
						continue;
					case 10:
						this.CopyDataValidations(worksheet);
						num = 3;
						continue;
					case 11:
						goto IL_2C5;
					case 12:
						this.CopyPageSetup(worksheet);
						num = 34;
						continue;
					case 13:
						if ((flags & WorksheetCopyType.CopyAutoFilters) != WorksheetCopyType.None)
						{
							num = 14;
							continue;
						}
						goto IL_160;
					case 14:
						this.CopyAutoFilters(worksheet);
						num = 6;
						continue;
					case 15:
						this.CopyConditionalFormats(worksheet);
						num = 33;
						continue;
					case 16:
						goto IL_1AD;
					case 17:
						this.ClearAll();
						flags &= ~WorksheetCopyType.ClearBefore;
						num = 8;
						continue;
					case 18:
						if ((flags & WorksheetCopyType.CopyNames) != WorksheetCopyType.None)
						{
							num = 30;
							continue;
						}
						goto IL_309;
					case 19:
						if ((flags & WorksheetCopyType.CopyDataValidations) != WorksheetCopyType.None)
						{
							num = 10;
							continue;
						}
						goto IL_406;
					case 20:
						if ((flags & WorksheetCopyType.CopyColumnHeight) != WorksheetCopyType.None)
						{
							num = 0;
							continue;
						}
						goto IL_2C5;
					case 21:
						goto IL_383;
					case 22:
						if ((flags & WorksheetCopyType.CopyCells) != WorksheetCopyType.None)
						{
							num = 4;
							continue;
						}
						goto IL_383;
					case 23:
						return;
					case 24:
						goto IL_309;
					case 25:
						goto IL_188;
					case 26:
						if ((flags & WorksheetCopyType.CopyRowHeight) != WorksheetCopyType.None)
						{
							num = 2;
							continue;
						}
						goto IL_1D5;
					case 27:
						if ((flags & WorksheetCopyType.CopyMerges) != WorksheetCopyType.None)
						{
							num = 9;
							continue;
						}
						goto IL_1AD;
					case 28:
						if ((flags & WorksheetCopyType.CopyConditionlFormats) != WorksheetCopyType.None)
						{
							num = 15;
							continue;
						}
						goto IL_3BC;
					case 29:
						this.ᜁ(worksheet, hashWorksheetNames);
						num = 23;
						continue;
					case 30:
						this.CopyNames(worksheet, hashWorksheetNames, hashNameIndexes, hashExternSheets);
						flags &= ~WorksheetCopyType.CopyNames;
						num = 24;
						continue;
					case 31:
						if ((flags & WorksheetCopyType.CopyTables) != WorksheetCopyType.None)
						{
							num = 35;
							continue;
						}
						goto IL_188;
					case 32:
						if ((flags & WorksheetCopyType.CopyPivotTables) != WorksheetCopyType.None)
						{
							num = 29;
							continue;
						}
						return;
					case 33:
						goto IL_3BC;
					case 34:
						goto IL_11B;
					case 35:
						this.ᜀ(worksheet, hashWorksheetNames);
						num = 25;
						continue;
					}
					break;
					IL_11B:
					num = 31;
					continue;
					IL_160:
					num = 19;
					continue;
					IL_309:
					base.ᜀ(worksheet, hashStyleNames, hashWorksheetNames, dicFontIndexes, flags, hashExtFormatIndexes);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_160;
					default:
						if (false)
						{
						}
						num = 22;
						continue;
					}
					IL_188:
					num = 32;
					continue;
					IL_1AD:
					num = 28;
					continue;
					IL_1D5:
					num = 18;
					continue;
					IL_2C5:
					num = 26;
					continue;
					IL_383:
					num = 27;
					continue;
					IL_3BC:
					num = 13;
					continue;
					IL_3E1:
					num = 20;
					continue;
					IL_406:
					num = 5;
				}
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00035558 File Offset: 0x00034558
		private void ᜁ(XlsWorksheet A_0, Dictionary<string, string> A_1)
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					return;
				case 2:
					if (A_0.ᜡ != null)
					{
						num = 5;
						continue;
					}
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (A_0.ᜡ.Count == 0)
						{
							num = 1;
							continue;
						}
						goto IL_BD;
					}
					break;
				case 4:
					goto IL_4E;
				case 5:
					num = 3;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 2;
				}
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅅ❇㡉❋㵍㡏㝑ㅓ≕", a_));
			IL_BD:
			this.ᜡ = A_0.ᜡ.Clone(this, A_1);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00035638 File Offset: 0x00034638
		private new void ᜀ(XlsWorksheet A_0, Dictionary<string, string> A_1)
		{
			int a_ = 10;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 5;
					continue;
				case 2:
					return;
				case 3:
					if (A_0.\u1732 != null)
					{
						num = 1;
						continue;
					}
					return;
				case 4:
					goto IL_46;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_0.\u1732.Count == 0)
						{
							num = 2;
							continue;
						}
						goto IL_BD;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 3;
				}
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("㜿ⵁ㙃ⵅ㭇≉⥋⭍⑏", a_));
			IL_BD:
			this.\u1732 = A_0.\u1732.ᜀ(this, A_1);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00035718 File Offset: 0x00034718
		private new void ᜀ(spr\u2622 A_0)
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜮ = (spr\u2622)A_0.Clone(this);
					goto IL_4C;
				case 1:
					if (A_0.Count > 0)
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					goto IL_54;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						goto IL_9F;
					}
					break;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				base.ParseData();
				num = 1;
				continue;
				IL_4C:
				num = 3;
			}
			IL_54:
			return;
			IL_9F:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⹀㙂㝄⑆ⱈ๊㽌㵎㹐⅒♔", a_));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000357E0 File Offset: 0x000347E0
		private new void ᜀ(XlsHyperLinksCollection A_0)
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
			base.ParseData();
			this.ᜢ = (HyperLinksCollection)spr\u1CD3.ᜀ(A_0, this);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00035834 File Offset: 0x00034834
		protected internal bool CanMove(ref IXLSRange destination, IXLSRange source)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					bool flag;
					XlsRange xlsRange;
					Dictionary<spr\u225F, object> dictionary;
					switch (num)
					{
					case 0:
						goto IL_138;
					case 1:
						goto IL_78;
					case 2:
						goto IL_122;
					case 3:
						num = 6;
						continue;
					case 5:
						if (flag)
						{
							num = 3;
							continue;
						}
						goto IL_1DB;
					case 6:
					{
						XlsRange xlsRange2;
						if (xlsRange.Worksheet != xlsRange2.Worksheet)
						{
							num = 11;
							continue;
						}
						goto IL_122;
					}
					case 7:
					{
						if (source == null)
						{
							num = 9;
							continue;
						}
						base.ParseData();
						xlsRange = (XlsRange)destination;
						XlsRange xlsRange2 = (XlsRange)source;
						int lastRow = xlsRange.FirstRow + xlsRange2.LastRow - xlsRange2.FirstRow;
						int lastColumn = xlsRange.FirstColumn + xlsRange2.LastColumn - xlsRange2.FirstColumn;
						destination = (xlsRange = (XlsRange)xlsRange.InnerWorksheet.AllocatedRange[xlsRange.Row, xlsRange.Column, lastRow, lastColumn]);
						num = 10;
						continue;
					}
					case 8:
						goto IL_1C2;
					case 9:
						goto IL_BF;
					case 10:
					{
						XlsRange xlsRange2;
						if (xlsRange == xlsRange2)
						{
							num = 8;
							continue;
						}
						dictionary = new Dictionary<spr\u225F, object>();
						flag = xlsRange2.ᜀ(dictionary);
						num = 5;
						continue;
					}
					case 11:
						dictionary.Clear();
						num = 2;
						continue;
					}
					if (true)
					{
					}
					if (destination == null)
					{
						num = 1;
						continue;
					}
					num = 7;
					continue;
					IL_122:
					flag = xlsRange.ᜀ(dictionary);
					num = 0;
				}
				IL_78:
				throw new ArgumentNullException(RecordTableEnumerator.b("♁⅃㕅㱇⍉≋⽍⑏㭑㭓㡕", a_));
				IL_BF:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⭃㍅㩇⥉⥋", a_));
				IL_138:
				goto IL_1DB;
				IL_1C2:
				return true;
				IL_1DB:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				default:
				{
					if (false)
					{
					}
					bool flag;
					return flag;
				}
				}
				break;
			}
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00035A3C File Offset: 0x00034A3C
		internal bool ᜆ(int A_0, int A_1, InsertOptionsType A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 15;
					for (;;)
					{
						int num4;
						switch (num)
						{
						case 0:
							return false;
						case 1:
						{
							int num2;
							int num3;
							if (num2 < num3)
							{
								num = 13;
								continue;
							}
							num = 12;
							continue;
						}
						case 2:
							goto IL_D6;
						case 3:
							if (this.m_iLastRow <= A_0)
							{
								num = 6;
								continue;
							}
							num = 16;
							continue;
						case 4:
							if (this.m_iFirstRow > this.m_iLastRow)
							{
								num = 11;
								continue;
							}
							return true;
						case 5:
							goto IL_D6;
						case 6:
							return true;
						case 7:
							if (num4 > 0)
							{
								num = 23;
								continue;
							}
							return true;
						case 8:
							goto IL_267;
						case 9:
							goto IL_28B;
						case 10:
						{
							XlsRange xlsRange;
							if (!xlsRange.AreFormulaArraysNotSeparated)
							{
								num = 0;
								continue;
							}
							goto IL_2C4;
						}
						case 11:
							this.m_iLastRow = (this.m_iFirstRow = -1);
							this.m_iLastColumn = (this.m_iFirstColumn = int.MaxValue);
							num = 14;
							continue;
						case 12:
						{
							int num2;
							if (!this.ᜆ(num2))
							{
								num = 17;
								continue;
							}
							this.m_iLastRow--;
							num2--;
							num = 5;
							continue;
						}
						case 13:
							num = 4;
							continue;
						case 14:
							goto IL_216;
						case 15:
							if (A_0 >= 1)
							{
								num = 24;
								continue;
							}
							return false;
						case 16:
							if (A_0 >= this.m_iFirstRow)
							{
								num = 20;
								continue;
							}
							goto IL_2C4;
						case 17:
							return false;
						case 18:
						{
							XlsRange xlsRange = (XlsRange)this.AllocatedRange[A_0, this.m_iFirstColumn, this.m_iLastRow, this.m_iLastColumn];
							num = 10;
							continue;
						}
						case 19:
							if (this.m_iLastColumn <= this.m_book.MaxColumnCount)
							{
								num = 18;
								continue;
							}
							goto IL_2C4;
						case 20:
							num = 19;
							continue;
						case 21:
							if (A_1 <= 0)
							{
								num = 9;
								continue;
							}
							num = 3;
							continue;
						case 22:
							if (A_0 > this.m_book.MaxRowCount)
							{
								num = 8;
								continue;
							}
							num = 21;
							continue;
						case 23:
						{
							int num3 = Math.Max(this.m_iLastRow - num4, this.m_iFirstRow);
							int num2 = this.m_iLastRow;
							num = 2;
							continue;
						}
						case 24:
							num = 22;
							continue;
						}
						break;
						IL_D6:
						num = 1;
						continue;
						IL_2C4:
						num4 = this.m_iLastRow + A_1 - this.m_book.MaxRowCount;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 7;
							break;
						}
					}
				}
				return false;
				IL_216:
				return true;
				IL_267:
				return false;
				IL_28B:
				return false;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00035D68 File Offset: 0x00034D68
		internal new bool ᜅ(int A_0, int A_1, InsertOptionsType A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 4;
					for (;;)
					{
						int num4;
						switch (num)
						{
						case 0:
							return false;
						case 1:
							goto IL_263;
						case 2:
							if (true)
							{
							}
							num = 12;
							continue;
						case 3:
						{
							XlsRange xlsRange;
							if (!xlsRange.AreFormulaArraysNotSeparated)
							{
								num = 7;
								continue;
							}
							goto IL_9C;
						}
						case 4:
							if (A_0 >= 1)
							{
								num = 14;
								continue;
							}
							return false;
						case 5:
							if (this.m_iLastColumn >= A_0)
							{
								num = 11;
								continue;
							}
							return true;
						case 6:
							goto IL_FD;
						case 7:
							return false;
						case 8:
							this.m_iLastRow = (this.m_iFirstRow = -1);
							this.m_iLastColumn = (this.m_iFirstColumn = int.MaxValue);
							num = 13;
							continue;
						case 9:
						{
							XlsRange xlsRange = (XlsRange)this.AllocatedRange[this.m_iFirstRow, A_0, this.m_iLastRow, this.m_iLastColumn];
							num = 3;
							continue;
						}
						case 10:
							goto IL_102;
						case 11:
							num = 22;
							continue;
						case 12:
							if (this.m_iFirstColumn > this.m_iLastColumn)
							{
								num = 8;
								continue;
							}
							return true;
						case 13:
							goto IL_18C;
						case 14:
							num = 1;
							continue;
						case 15:
						{
							int num2;
							if (!this.ᜅ(num2))
							{
								num = 0;
								continue;
							}
							this.m_iLastColumn--;
							num2--;
							num = 10;
							continue;
						}
						case 16:
							goto IL_280;
						case 17:
						{
							int num3 = Math.Max(this.m_iLastColumn - num4, this.m_iFirstColumn);
							int num2 = this.m_iLastColumn;
							num = 20;
							continue;
						}
						case 18:
						{
							int num2;
							int num3;
							if (num2 < num3)
							{
								num = 2;
								continue;
							}
							num = 15;
							continue;
						}
						case 19:
							if (num4 > 0)
							{
								num = 17;
								continue;
							}
							return true;
						case 20:
							goto IL_102;
						case 21:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_263;
							default:
								if (false)
								{
								}
								if (A_0 >= this.m_iFirstColumn)
								{
									num = 9;
									continue;
								}
								goto IL_9C;
							}
							break;
						case 22:
							if (this.m_iFirstColumn == 2147483647)
							{
								num = 6;
								continue;
							}
							num = 21;
							continue;
						}
						break;
						IL_9C:
						num4 = this.m_iLastColumn + A_1 - this.m_book.MaxColumnCount;
						num = 19;
						continue;
						IL_102:
						num = 18;
						continue;
						IL_263:
						if (A_0 > this.m_book.MaxColumnCount)
						{
							num = 16;
						}
						else
						{
							num = 5;
						}
					}
				}
				IL_FD:
				return true;
				IL_18C:
				return true;
				IL_280:
				return false;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00036078 File Offset: 0x00035078
		protected internal IXLSRange GetRangeByString(string rangeValue)
		{
			switch (0)
			{
			default:
			{
				int num = 23;
				List<IXLSRange> list;
				IXLSRanges ixlsranges;
				for (;;)
				{
					FormulaUtil formulaUtil;
					Stack<object> stack;
					Ptg[] array;
					int num2;
					int num3;
					List<IXLSRange> list3;
					switch (num)
					{
					case 0:
						try
						{
							this.\u1713();
							string text = this.FormulaEngine.ᜀ.ឥ(rangeValue);
							IXLSRange result = this[text.Substring(text.LastIndexOf('!') + 1)];
							this.ᜑ();
							return result;
						}
						catch (Exception)
						{
							return null;
						}
						goto IL_FD;
					case 1:
						goto IL_FD;
					case 2:
						goto IL_282;
					case 3:
						num = 17;
						continue;
					case 4:
						formulaUtil = new FormulaUtil(base.AppImplementation, this.m_book, NumberFormatInfo.InvariantInfo, ',', ';');
						goto IL_1ED;
					case 5:
					{
						list = (List<IXLSRange>)stack.Pop();
						List<IXLSRange> list2 = (List<IXLSRange>)stack.Peek();
						list2.AddRange(list);
						list.Clear();
						num = 6;
						continue;
					}
					case 6:
						goto IL_FD;
					case 7:
						num = 27;
						continue;
					case 8:
						num = 24;
						continue;
					case 9:
						num = 21;
						continue;
					case 10:
						goto IL_422;
					case 11:
						return ixlsranges;
					case 12:
						if (array[num2].TokenCode == FormulaToken.tCellRangeList)
						{
							num = 5;
							continue;
						}
						goto IL_FD;
					case 13:
						if (array[num2] is sprỜ)
						{
							num = 8;
							continue;
						}
						num = 12;
						continue;
					case 14:
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						num = 13;
						continue;
					case 15:
						list3 = new List<IXLSRange>();
						goto IL_331;
					case 16:
						list3 = list;
						goto IL_331;
					case 17:
						formulaUtil = this.m_book.FormulaUtil;
						goto IL_1ED;
					case 18:
						goto IL_399;
					case 19:
						num = 15;
						continue;
					case 20:
					{
						int count;
						if (count == 1)
						{
							num = 10;
							continue;
						}
						ixlsranges = (list[0].Worksheet as XlsWorksheet).ᜮ();
						int num4 = 0;
						num = 25;
						continue;
					}
					case 21:
						if (rangeValue.Length == 0)
						{
							num = 2;
							continue;
						}
						goto IL_168;
					case 22:
						if (!this.m_book.Loading)
						{
							num = 3;
							continue;
						}
						num = 4;
						continue;
					case 24:
						if (list == null)
						{
							num = 19;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_168;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					case 25:
						goto IL_192;
					case 26:
						goto IL_399;
					case 27:
					{
						if (stack.Count != 1)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						list = (List<IXLSRange>)stack.Pop();
						int count = list.Count;
						num = 20;
						continue;
					}
					case 28:
					{
						int count;
						int num4;
						if (num4 >= count)
						{
							num = 11;
							continue;
						}
						(ixlsranges as XlsRangesCollection).Add(list[num4]);
						num4++;
						num = 29;
						continue;
					}
					case 29:
						goto IL_192;
					}
					if (rangeValue != null)
					{
						num = 9;
						continue;
					}
					goto IL_3BF;
					IL_FD:
					num2++;
					num = 18;
					continue;
					IL_168:
					num = 22;
					continue;
					IL_192:
					num = 28;
					continue;
					IL_1ED:
					FormulaUtil formulaUtil2 = formulaUtil;
					array = formulaUtil2.ᜃ(rangeValue);
					stack = new Stack<object>();
					list = new List<IXLSRange>();
					num2 = 0;
					num3 = array.Length;
					num = 26;
					continue;
					IL_331:
					List<IXLSRange> list4 = list3;
					IXLSRange item = ((sprỜ)array[num2]).ᜀ(base.Workbook, this);
					list4.Add(item);
					stack.Push(list4);
					list = null;
					num = 1;
					continue;
					IL_399:
					num = 14;
				}
				return ixlsranges;
				IL_282:
				IL_3BF:
				return null;
				IL_422:
				return list[0];
			}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000364C0 File Offset: 0x000354C0
		protected internal void UpdateNamedRangeIndexes(int[] arrNewIndex)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				spr\u1D9B spr_u1D9B;
				switch (num)
				{
				case 0:
					return;
				case 2:
					if (this.\u171F != null)
					{
						num = 5;
						continue;
					}
					goto IL_49;
				case 3:
					spr_u1D9B.ᜀ(arrNewIndex);
					num = 0;
					continue;
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						goto IL_9C;
					}
					break;
				case 5:
					this.\u171F.UpdateNamedRangeIndexes(arrNewIndex);
					num = 7;
					continue;
				case 6:
					if (spr_u1D9B != null)
					{
						num = 3;
						continue;
					}
					return;
				case 7:
					goto IL_7C;
				}
				if (arrNewIndex == null)
				{
					num = 4;
					continue;
				}
				base.ParseData();
				this.ᜏ.UpdateNameIndexes(this.m_book, arrNewIndex);
				num = 2;
				continue;
				IL_49:
				spr_u1D9B = base.InnerShapes;
				num = 6;
				continue;
				IL_7C:
				goto IL_49;
			}
			IL_9C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("尼䴾㍀ൂ⁄うH╊⥌⩎⥐", a_));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000365E0 File Offset: 0x000355E0
		protected internal void UpdateNamedRangeIndexes(IDictionary<int, int> dicNewIndex)
		{
			int a_ = 13;
			int num = 1;
			for (;;)
			{
				spr\u1D9B spr_u1D9B;
				switch (num)
				{
				case 0:
					if (this.\u171F != null)
					{
						num = 8;
						continue;
					}
					goto IL_CD;
				case 2:
					goto IL_50;
				case 3:
					goto IL_68;
				case 4:
					return;
				case 5:
					if (this.ᜏ != null)
					{
						num = 10;
						continue;
					}
					goto IL_68;
				case 6:
					spr_u1D9B.ᜀ(dicNewIndex);
					num = 4;
					continue;
				case 7:
					goto IL_CD;
				case 8:
					this.\u171F.UpdateNamedRangeIndexes(dicNewIndex);
					num = 7;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (spr_u1D9B != null)
						{
							num = 6;
							continue;
						}
						return;
					}
					break;
				case 10:
					this.ᜏ.UpdateNameIndexes(this.m_book, dicNewIndex);
					num = 3;
					continue;
				}
				if (dicNewIndex == null)
				{
					num = 2;
					continue;
				}
				base.ParseData();
				num = 5;
				continue;
				IL_68:
				num = 0;
				continue;
				IL_CD:
				spr_u1D9B = base.InnerShapes;
				num = 9;
			}
			IL_50:
			throw new ArgumentNullException(RecordTableEnumerator.b("❂ⱄ⑆݈⹊㩌َ㽐㝒ご⽖", a_));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0003672C File Offset: 0x0003572C
		protected internal int GetStringIndex(long cellIndex)
		{
			spr\u1C7C spr_u1C7C;
			for (;;)
			{
				IL_38:
				base.ParseData();
				for (;;)
				{
					IL_3E:
					int num = 3;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3E;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								if (true)
								{
								}
								base.ParentWorkbook.InnerSST.Parse();
								num = 2;
								continue;
							case 1:
								if (spr_u1C7C == null)
								{
									num = 4;
									continue;
								}
								goto IL_B6;
							case 2:
								goto IL_62;
							case 3:
								if (base.ParentWorkbook != null)
								{
									num = 0;
									continue;
								}
								goto IL_62;
							case 4:
								return -1;
							}
							goto IL_38;
							IL_62:
							spr_u1C7C = (this.ᜏ.ᜄ(cellIndex) as spr\u1C7C);
							num = 1;
							break;
						}
					}
				}
			}
			return -1;
			IL_B6:
			return spr_u1C7C.ᜁ();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000367F8 File Offset: 0x000357F8
		internal new spr\u223A ᜂ(long A_0)
		{
			spr\u1C7C spr_u1C7C;
			for (;;)
			{
				spr_u1C7C = (this.ᜏ.ᜄ(A_0) as spr\u1C7C);
				if (spr_u1C7C == null)
				{
					break;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_41;
				}
			}
			return null;
			IL_41:
			if (false)
			{
			}
			int a_ = spr_u1C7C.ᜁ();
			return this.m_book.InnerSST[a_];
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00036864 File Offset: 0x00035864
		public object GetTextObject(long cellIndex)
		{
			spr\u1C7C spr_u1C7C;
			for (;;)
			{
				base.ParseData();
				spr_u1C7C = (this.ᜏ.ᜄ(cellIndex) as spr\u1C7C);
				if (spr_u1C7C == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_1;
			}
			return null;
			Block_1:
			if (true)
			{
			}
			if (false)
			{
			}
			int a_ = spr_u1C7C.ᜁ();
			return this.m_book.InnerSST[a_];
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000368D8 File Offset: 0x000358D8
		internal new spr\u192F ᜃ(long A_0)
		{
			spr\u23A5 spr_u23A;
			for (;;)
			{
				base.ParseData();
				spr_u23A = this.ᜏ.ᜄ(A_0);
				if (spr_u23A == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_42;
				}
			}
			if (true)
			{
			}
			return null;
			IL_42:
			if (false)
			{
			}
			int a_ = (int)spr_u23A.ᜆ();
			return this.m_book.InnerExtFormats.ᜁ(a_);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00036948 File Offset: 0x00035948
		protected internal void SetLabelSSTIndex(long cellIndex, int iSSTIndex)
		{
			int a_ = 15;
			int num;
			int num2;
			spr\u23A5 spr_u23A;
			for (;;)
			{
				base.ParseData();
				num = sprṔ.ᜁ(cellIndex);
				num2 = sprṔ.ᜀ(cellIndex);
				spr_u23A = this.ᜏ.ᜄ(num, num2);
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_1A9;
					case 1:
						goto IL_18C;
					case 2:
						if (iSSTIndex == -1)
						{
							num3 = 4;
							continue;
						}
						num3 = 8;
						continue;
					case 3:
						if (spr_u23A.get_TypeCode() != TBIFFRecord.LabelSST)
						{
							num3 = 10;
							continue;
						}
						goto IL_1F7;
					case 4:
						if (true)
						{
						}
						num3 = 5;
						continue;
					case 5:
						if (spr_u23A != null)
						{
							num3 = 11;
							continue;
						}
						goto IL_1A9;
					case 6:
						goto IL_DE;
					case 7:
						if (spr_u23A != null)
						{
							num3 = 12;
							continue;
						}
						goto IL_16C;
					case 8:
						if (iSSTIndex >= 0)
						{
							num3 = 15;
							continue;
						}
						goto IL_158;
					case 9:
						goto IL_18A;
					case 10:
						goto IL_16C;
					case 11:
						IL_14C:
						num3 = 14;
						continue;
					case 12:
						num3 = 3;
						continue;
					case 13:
						if (iSSTIndex >= this.m_book.InnerSST.Count)
						{
							num3 = 6;
							continue;
						}
						num3 = 7;
						continue;
					case 14:
						if (spr_u23A.get_TypeCode() != TBIFFRecord.Blank)
						{
							num3 = 0;
							continue;
						}
						goto IL_18C;
					case 15:
						num3 = 13;
						continue;
					}
					break;
					IL_18C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14C;
					default:
						goto IL_1A2;
					}
					IL_16C:
					spr_u23A = (spr\u23A5)this.ᜀ(TBIFFRecord.LabelSST, num, num2);
					num3 = 9;
					continue;
					IL_1A9:
					this.ᜏ.ᜁ(num, num2, (spr\u23A5)this.ᜀ(TBIFFRecord.Blank, num, num2));
					num3 = 1;
				}
			}
			IL_DE:
			IL_158:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄᑆᩈὊьⅎ㕐㙒ⵔ", a_));
			IL_18A:
			goto IL_1F7;
			IL_1A2:
			if (false)
			{
			}
			return;
			IL_1F7:
			((spr\u1C7C)spr_u23A).ᜀ(iSSTIndex);
			this.ᜏ.ᜁ(num, num2, spr_u23A);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00036B68 File Offset: 0x00035B68
		protected internal void UpdateStringIndexes(List<int> arrNewIndexes)
		{
			int a_ = 16;
			if (arrNewIndexes != null)
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
					base.ParseData();
					this.ᜏ.UpdateStringIndexes(arrNewIndexes);
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉ɋ⭍❏᭑㩓㉕㵗≙㥛ⵝ", a_));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00036BD8 File Offset: 0x00035BD8
		public void RemoveMergedCells(IXLSRange range)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					int lastRow;
					int column;
					int num3;
					int lastColumn;
					int row;
					switch (num)
					{
					case 0:
						if (num2 > lastRow)
						{
							goto IL_143;
						}
						num3 = column;
						num = 3;
						continue;
					case 1:
						goto IL_76;
					case 2:
						goto IL_92;
					case 3:
						goto IL_76;
					case 5:
						if (num3 != column)
						{
							num = 2;
							continue;
						}
						goto IL_11B;
					case 6:
						if (num3 > lastColumn)
						{
							num = 12;
							continue;
						}
						num = 13;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_143;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 8:
						goto IL_132;
					case 9:
						goto IL_71;
					case 10:
						goto IL_11B;
					case 11:
						goto IL_132;
					case 12:
						num2++;
						num = 8;
						continue;
					case 13:
						if (num2 == row)
						{
							num = 7;
							continue;
						}
						goto IL_92;
					case 14:
						return;
					}
					if (range == null)
					{
						num = 9;
						continue;
					}
					base.ParseData();
					row = range.Row;
					column = range.Column;
					lastRow = range.LastRow;
					lastColumn = range.LastColumn;
					num2 = row;
					num = 11;
					continue;
					IL_76:
					num = 6;
					continue;
					IL_92:
					long key = sprṔ.ᜀ(num3, num2);
					this.ᜏ.Remove(key);
					num = 10;
					continue;
					IL_11B:
					num3++;
					num = 1;
					continue;
					IL_132:
					num = 0;
					continue;
					IL_143:
					num = 14;
				}
				IL_71:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㈿⍁⩃ⅅⵇ", a_));
			}
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00036DB8 File Offset: 0x00035DB8
		public void SetActiveCell(IXLSRange range)
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
			this.SetActiveCell(range, true);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00036DFC File Offset: 0x00035DFC
		public void SetActiveCell(IXLSRange range, bool updateApplication)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_5B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8A;
						default:
							goto IL_82;
						}
						break;
					case 3:
						base.ParseData();
						num = 2;
						continue;
					case 4:
						if (updateApplication)
						{
							num = 3;
							continue;
						}
						goto IL_B5;
					}
					if (range == null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					IL_8A:
					num = 4;
				}
				IL_5B:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⑄⥆⹈⹊", a_));
				IL_82:
				if (false)
				{
				}
				IL_B5:
				base.AppImplementation.ᜂ(range);
				this.ᜁ();
				spr\u21A4 spr_u21A = this.ᜄ();
				int num2 = range.Column - 1;
				int num3 = range.Row - 1;
				spr_u21A.ᜀ((ushort)num2);
				spr_u21A.ᜂ((ushort)num3);
				spr\u21A4.ᜀ a_2 = new spr\u21A4.ᜀ((ushort)num3, (ushort)num3, (byte)num2, (byte)num2);
				spr_u21A.ᜀ(0, a_2);
				return;
			}
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00036F10 File Offset: 0x00035F10
		private new void ᜀ(IXLSRange A_0)
		{
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num = 12;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15D;
					default:
						if (false)
						{
						}
						goto IL_BC;
					}
					break;
				case 3:
					num2 = 2;
					goto IL_15D;
				case 4:
				{
					IXLSRange splitCell;
					if (A_0.Row < splitCell.Row)
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				}
				case 5:
					goto IL_BC;
				case 6:
				{
					IXLSRange splitCell;
					if (A_0.Column >= splitCell.Column)
					{
						num = 7;
						continue;
					}
					num2 = 1;
					num = 5;
					continue;
				}
				case 7:
					num2 = 3;
					num = 9;
					continue;
				case 8:
				{
					if (true)
					{
					}
					IXLSRange splitCell = this.SplitCell;
					num = 4;
					continue;
				}
				case 9:
					goto IL_BC;
				case 10:
					goto IL_BC;
				case 11:
					return;
				case 12:
				{
					IXLSRange splitCell;
					if (A_0.Column >= splitCell.Column)
					{
						num = 3;
						continue;
					}
					num2 = 0;
					num = 1;
					continue;
				}
				}
				if (base.WindowTwo.ᜁ())
				{
					num = 8;
					continue;
				}
				break;
				IL_BC:
				this.\u1717.ᜀ((ushort)num2);
				num = 11;
				continue;
				IL_15D:
				num = 10;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0003708C File Offset: 0x0003608C
		private new spr\u21A4 ᜄ()
		{
			switch (0)
			{
			default:
			{
				spr\u21A4 spr_u21A;
				for (;;)
				{
					spr_u21A = null;
					int num = 1;
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						int count;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (spr_u21A == null)
							{
								num = 3;
								continue;
							}
							return spr_u21A;
						case 1:
							if (this.\u1717 == null)
							{
								num = 15;
								continue;
							}
							num = 13;
							continue;
						case 2:
							num2 = 0;
							goto IL_19C;
						case 3:
							num = 10;
							continue;
						case 4:
							goto IL_14C;
						case 5:
						{
							spr\u21A4 spr_u21A2;
							spr_u21A = spr_u21A2;
							num = 8;
							continue;
						}
						case 6:
							goto IL_12B;
						case 7:
							goto IL_78;
						case 8:
							goto IL_78;
						case 9:
							return spr_u21A;
						case 10:
							if (this.\u1716.Count == 1)
							{
								num = 4;
								continue;
							}
							return spr_u21A;
						case 11:
						{
							spr\u21A4 spr_u21A2;
							if ((int)spr_u21A2.ᜀ() == num3)
							{
								num = 5;
								continue;
							}
							num4++;
							num = 6;
							continue;
						}
						case 12:
						{
							if (num4 >= count)
							{
								num = 7;
								continue;
							}
							spr\u21A4 spr_u21A2 = this.\u1716[num4];
							num = 11;
							continue;
						}
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_14C;
							default:
								if (false)
								{
								}
								num2 = (int)this.\u1717.ᜆ();
								goto IL_19C;
							}
							break;
						case 14:
							goto IL_12B;
						case 15:
							num = 2;
							continue;
						}
						break;
						IL_78:
						num = 0;
						continue;
						IL_12B:
						num = 12;
						continue;
						IL_14C:
						spr_u21A = this.\u1716[0];
						num = 9;
						continue;
						IL_19C:
						num3 = num2;
						num4 = 0;
						count = this.\u1716.Count;
						num = 14;
					}
				}
				return spr_u21A;
			}
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0003726C File Offset: 0x0003626C
		internal IXLSRange ᜬ()
		{
			int num;
			int num2;
			for (;;)
			{
				base.ParseData();
				spr\u21A4 spr_u21A = this.ᜄ();
				num = 0;
				num2 = 0;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_5C;
					case 1:
						num = (int)spr_u21A.ᜂ();
						num2 = (int)spr_u21A.ᜁ();
						num3 = 0;
						continue;
					case 2:
						if (spr_u21A != null)
						{
							goto IL_30;
						}
						goto IL_5C;
					}
					break;
					IL_30:
					num3 = 1;
					continue;
					IL_5C:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						goto IL_7A;
					}
				}
			}
			IL_7A:
			if (false)
			{
			}
			return this[num + 1, num2 + 1];
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00037308 File Offset: 0x00036308
		internal new bool ᜂ(spr᱒ A_0)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.ᜑ() != null)
					{
						num = 4;
						continue;
					}
					return false;
				case 1:
					goto IL_F6;
				case 2:
					if (A_0.ᜑ().Length != 0)
					{
						Ptg ptg = A_0.ᜑ()[0];
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_93;
				case 5:
				{
					Ptg ptg;
					if (ptg.TokenCode == FormulaToken.tExp)
					{
						num = 6;
						continue;
					}
					return false;
				}
				case 6:
					goto IL_6A;
				}
				if (true)
				{
				}
				if (A_0 != null)
				{
					num = 3;
					continue;
				}
				return false;
				IL_93:
				num = 2;
			}
			IL_6A:
			return this.CellRecords.ᜁ(A_0.\u1714() + 1, A_0.\u1713() + 1) != null;
			IL_F6:
			return false;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00037414 File Offset: 0x00036414
		public bool IsArrayFormula(long cellIndex)
		{
			if (true)
			{
			}
			base.ParseData();
			spr᱒ spr᱒ = this.ᜏ.ᜄ(cellIndex) as spr᱒;
			if (spr᱒ == null)
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
					return false;
				}
			}
			return this.ᜂ(spr᱒);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00037478 File Offset: 0x00036478
		public bool HasArrayFormula(long cellIndex)
		{
			base.ParseData();
			spr᱒ spr᱒ = this.ᜏ.ᜄ(cellIndex) as spr᱒;
			if (spr᱒ == null)
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
					return false;
				}
			}
			return this.ᜂ(spr᱒);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000374DC File Offset: 0x000364DC
		protected internal double InnerGetRowHeight(int rowIndex, bool bRaiseEvents)
		{
			int a_ = 14;
			int num = 3;
			sprᱧ sprᱧ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!sprᱧ.ᜅ())
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						goto IL_C0;
					}
					break;
				case 1:
					if (rowIndex > this.m_book.MaxRowCount)
					{
						num = 4;
						continue;
					}
					sprᱧ = sprᜑ.ᜀ(this, rowIndex - 1, false);
					num = 5;
					continue;
				case 2:
					goto IL_124;
				case 4:
					goto IL_A8;
				case 5:
					goto IL_6F;
				case 6:
					num = 1;
					continue;
				case 7:
					num = 0;
					continue;
				}
				if (rowIndex >= 1)
				{
					num = 6;
					continue;
				}
				break;
				IL_6F:
				if (sprᱧ == null)
				{
					goto IL_129;
				}
				if (true)
				{
				}
				num = 7;
			}
			IL_A8:
			goto IL_D0;
			IL_C0:
			if (false)
			{
			}
			return 0.0;
			IL_D0:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙃⥅㽇͉≋⩍㕏⩑瑓㕕㥗㑙筛⩝䁟aţ䙥ѧཀྵὫᵭ偯ٱᱳ፵ᙷ婹䵻幽ꚅﺋ뒓", a_) + this.m_book.MaxRowCount.ToString());
			IL_124:
			return (double)sprᱧ.\u1718() / 20.0;
			IL_129:
			return this.DefaultRowHeight;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00037618 File Offset: 0x00036618
		internal override object Clone(object parent, bool cloneShapes)
		{
			int a_ = 14;
			int num = 3;
			XlsWorksheet xlsWorksheet;
			for (;;)
			{
				switch (num)
				{
				case 0:
					xlsWorksheet.ᜫ = (spr\u256D)this.ᜫ.CloneAll();
					num = 2;
					continue;
				case 1:
					goto IL_47;
				case 2:
					goto IL_49;
				case 4:
					goto IL_64;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						goto IL_E5;
					}
					break;
				case 6:
					if (this.ᜫ != null)
					{
						num = 0;
						continue;
					}
					goto IL_49;
				case 7:
					if (this.\u171C != null)
					{
						num = 4;
						continue;
					}
					goto IL_19C;
				}
				if (parent == null)
				{
					num = 1;
					continue;
				}
				base.ParseData();
				xlsWorksheet = (XlsWorksheet)base.Clone(parent, cloneShapes);
				xlsWorksheet.ᜎ = null;
				xlsWorksheet.ᜬ = null;
				xlsWorksheet.\u1717 = (spr\u2408)spr\u1CD3.ᜀ(this.\u1717);
				xlsWorksheet.ᜣ = spr\u1CD3.ᜀ(this.ᜣ);
				xlsWorksheet.ᜩ = spr\u1CD3.ᜀ(this.ᜩ);
				xlsWorksheet.\u171B = spr\u1CD3.ᜀ(this.\u171B);
				xlsWorksheet.\u1716 = spr\u1CD3.ᜀ<spr\u21A4>(this.\u1716);
				num = 6;
				continue;
				IL_49:
				num = 7;
				continue;
				IL_64:
				if (true)
				{
				}
				xlsWorksheet.\u171C = spr\u1CD3.ᜀ<int, spr\u2114>(this.\u171C);
				xlsWorksheet.\u171D = spr\u1CD3.ᜀ<long, spr\u2114>(this.\u171D);
				num = 5;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_));
			IL_E5:
			if (false)
			{
			}
			IL_19C:
			xlsWorksheet.ᜐ = spr\u1CD3.ᜀ(this.ᜐ);
			xlsWorksheet.\u171F = (XlsDataValidationTable)spr\u1CD3.ᜀ(this.\u171F, xlsWorksheet);
			xlsWorksheet.\u1718 = new spr\u25EF((spr\u2158)base.ReservedHandle, this);
			xlsWorksheet.\u1712 = this.\u1712.Clone(xlsWorksheet);
			xlsWorksheet.\u1714 = (spr\u1FBC)spr\u1CD3.ᜀ(this.\u1714, xlsWorksheet);
			xlsWorksheet.ᜠ = this.ᜠ.Clone(xlsWorksheet);
			xlsWorksheet.ᜡ = (PivotTablesCollection)spr\u1CD3.ᜀ(this.ᜡ, xlsWorksheet);
			xlsWorksheet.ᜢ = (HyperLinksCollection)spr\u1CD3.ᜀ(this.ᜢ, xlsWorksheet);
			xlsWorksheet.ᜪ = (WorksheetConditionalFormats)spr\u1CD3.ᜀ(this.ᜪ, xlsWorksheet);
			xlsWorksheet.ᜏ = this.ᜏ.Clone(xlsWorksheet);
			xlsWorksheet.m_book.InnerWorksheets.InnerAdd(xlsWorksheet);
			return xlsWorksheet;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000378A4 File Offset: 0x000368A4
		internal void ᜭ()
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
			base.ParseData();
			this.ᜏ.ReAddAllStrings();
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000378F0 File Offset: 0x000368F0
		internal new bool? ᜀ(ICombinedRange A_0)
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
			return this.ᜰ.ᜀ(A_0);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00037938 File Offset: 0x00036938
		public override void MarkUsedReferences(bool[] usedItems)
		{
			for (;;)
			{
				this.ᜏ.ᜀ(usedItems);
				if (true)
				{
				}
				int num = 5;
				for (;;)
				{
					int num2;
					int count;
					IChartShapes charts;
					switch (num)
					{
					case 0:
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						(charts[num2] as XlsChart).MarkUsedReferences(usedItems);
						num2++;
						num = 6;
						continue;
					case 1:
						if (this.ᜪ != null)
						{
							num = 3;
							continue;
						}
						goto IL_118;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						}
						if (false)
						{
						}
						this.ᜪ.ᜀ(usedItems);
						num = 9;
						continue;
					case 4:
						this.\u171F.MarkUsedReferences(usedItems);
						num = 8;
						continue;
					case 5:
						if (this.\u171F != null)
						{
							num = 4;
							continue;
						}
						goto IL_CF;
					case 6:
						goto IL_B3;
					case 7:
						goto IL_B3;
					case 8:
						goto IL_CF;
					case 9:
						goto IL_118;
					}
					break;
					IL_B3:
					num = 0;
					continue;
					IL_CF:
					num = 1;
					continue;
					IL_118:
					charts = base.Charts;
					num2 = 0;
					count = charts.Count;
					num = 7;
				}
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00037A80 File Offset: 0x00036A80
		public override void UpdateReferenceIndexes(int[] arrUpdatedIndexes)
		{
			for (;;)
			{
				this.ᜏ.ᜀ(arrUpdatedIndexes);
				int num = 9;
				for (;;)
				{
					int num2;
					int count;
					IChartShapes charts;
					switch (num)
					{
					case 0:
						goto IL_11B;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						}
						if (false)
						{
						}
						this.ᜪ.ᜀ(arrUpdatedIndexes);
						num = 0;
						continue;
					case 2:
						return;
					case 3:
						this.\u171F.UpdateReferenceIndexes(arrUpdatedIndexes);
						num = 5;
						continue;
					case 4:
						if (this.ᜪ != null)
						{
							num = 1;
							continue;
						}
						goto IL_11B;
					case 5:
						goto IL_D2;
					case 6:
						if (true)
						{
						}
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						(charts[num2] as XlsChart).UpdateReferenceIndexes(arrUpdatedIndexes);
						num2++;
						num = 8;
						continue;
					case 7:
						goto IL_AE;
					case 8:
						goto IL_AE;
					case 9:
						if (this.\u171F != null)
						{
							num = 3;
							continue;
						}
						goto IL_D2;
					}
					break;
					IL_AE:
					num = 6;
					continue;
					IL_D2:
					num = 4;
					continue;
					IL_11B:
					charts = base.Charts;
					num2 = 0;
					count = charts.Count;
					num = 7;
				}
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00037BC8 File Offset: 0x00036BC8
		protected void CreateEmptyPane()
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
			this.\u1717 = (spr\u2408)spr\u175E.ᜀ(TBIFFRecord.Pane);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00037C18 File Offset: 0x00036C18
		protected void CopyCell(IXLSRange destCell, IXLSRange sourceCell)
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
			this.ᜀ(destCell, sourceCell, CopyRangeOptions.None);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00037C5C File Offset: 0x00036C5C
		internal new void ᜀ(IXLSRange A_0, IXLSRange A_1, CopyRangeOptions A_2)
		{
			int a_ = 1;
			XlsRange xlsRange;
			XlsRange xlsRange2;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 1;
					for (;;)
					{
						int num2;
						int num3;
						spr᱒ spr᱒;
						spr᱒ spr᱒2;
						switch (num)
						{
						case 0:
							if (A_1 == null)
							{
								num = 3;
								continue;
							}
							xlsRange = (XlsRange)A_0;
							xlsRange2 = (XlsRange)A_1;
							num = 23;
							continue;
						case 2:
							if (xlsRange2.Record != null)
							{
								num = 14;
								continue;
							}
							goto IL_1FC;
						case 3:
							goto IL_130;
						case 4:
							if (!xlsRange2.IsSingleCell)
							{
								num = 7;
								continue;
							}
							xlsRange.ExtendedFormatIndex = xlsRange2.ExtendedFormatIndex;
							num = 2;
							continue;
						case 5:
							if (true)
							{
							}
							num2 = A_0.Column - A_1.Column;
							goto IL_147;
						case 6:
							num = 4;
							continue;
						case 7:
							goto IL_1E5;
						case 8:
							num = 9;
							continue;
						case 9:
							num2 = 0;
							goto IL_147;
						case 10:
							goto IL_214;
						case 11:
							if (xlsRange2.Record is spr᱒)
							{
								num = 18;
								continue;
							}
							goto IL_1FC;
						case 12:
							return;
						case 13:
							num = 22;
							continue;
						case 14:
							num = 11;
							continue;
						case 15:
						{
							bool flag;
							if (!flag)
							{
								num = 13;
								continue;
							}
							num = 17;
							continue;
						}
						case 16:
						{
							bool flag;
							if (!flag)
							{
								num = 8;
								continue;
							}
							num = 5;
							continue;
						}
						case 17:
							num3 = A_0.Row - A_1.Row;
							goto IL_2B3;
						case 18:
							num = 20;
							continue;
						case 19:
							goto IL_98;
						case 20:
							if (!A_1.HasFormulaArray)
							{
								spr᱒ = (spr᱒)xlsRange2.Record;
								spr᱒2 = (spr᱒)spr᱒.ᜆ();
								spr᱒2.ᜇ((int)((ushort)(A_0.Row - 1)));
								spr᱒2.ᜆ((int)((ushort)(A_0.Column - 1)));
								bool flag = (A_2 & CopyRangeOptions.UpdateFormulas) != CopyRangeOptions.None;
								num = 15;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								num = 12;
								continue;
							}
							break;
						case 21:
							goto IL_172;
						case 22:
							num3 = 0;
							goto IL_2B3;
						case 23:
							if (xlsRange.IsSingleCell)
							{
								num = 6;
								continue;
							}
							goto IL_272;
						}
						if (A_0 == null)
						{
							num = 19;
							continue;
						}
						num = 0;
						continue;
						IL_147:
						int a_2 = num2;
						int a_3;
						spr᱒2.ᜁ(this.ᜀ(spr᱒.ᜑ(), a_3, a_2));
						xlsRange.ᜁ(spr᱒2);
						num = 21;
						continue;
						IL_1FC:
						A_0.Value = A_1.Value;
						num = 10;
						continue;
						IL_2B3:
						a_3 = num3;
						num = 16;
					}
					break;
				}
				}
			}
			IL_98:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶尸䠺䤼簾⑀⽂⥄", a_));
			IL_130:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐶嘸为似尾⑀B⁄⭆╈", a_));
			IL_172:
			goto IL_33E;
			IL_1E5:
			goto IL_272;
			IL_214:
			goto IL_33E;
			IL_272:
			throw new ArgumentException(RecordTableEnumerator.b("琶嘸䬺䐼Ἶ㍀♂≄⹆♈╊浌⭎㹐獒㭔㡖ⵘ筚ぜ㹞ᕠb൤䥦", a_));
			IL_33E:
			this.ᜀ(xlsRange2, xlsRange);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00037FB0 File Offset: 0x00036FB0
		private new void ᜃ(XlsRange A_0, XlsRange A_1)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							goto IL_108;
						}
						IXLSRange range = A_0.Hyperlinks[num2].Range;
						int num3;
						int num4;
						((XlsHyperLink)A_0.Hyperlinks[num2]).Range = this[range.Row + num3, range.Column + num4];
						num2++;
						num = 2;
						continue;
					}
					case 2:
						goto IL_F8;
					case 3:
						goto IL_F8;
					case 4:
					{
						if (A_1 == null)
						{
							num = 8;
							continue;
						}
						int num3 = this.ᜁ(A_0, A_1);
						int num4 = this.ᜂ(A_0, A_1);
						num = 7;
						continue;
					}
					case 5:
						goto IL_83;
					case 6:
						goto IL_114;
					case 7:
						if (A_0.Hyperlinks != null)
						{
							num = 9;
							continue;
						}
						goto IL_197;
					case 8:
						goto IL_F3;
					case 9:
					{
						int num2 = 0;
						int count = A_0.Hyperlinks.Count;
						num = 3;
						continue;
					}
					}
					if (A_0 != null)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_108;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					IL_F8:
					num = 1;
					continue;
					IL_108:
					num = 6;
				}
				IL_83:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤹医䬽㈿⅁⅃", a_));
				IL_F3:
				throw new ArgumentNullException(RecordTableEnumerator.b("帹夻䴽㐿", a_));
				IL_114:
				IL_197:
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0003815C File Offset: 0x0003715C
		private new int ᜂ(XlsRange A_0, XlsRange A_1)
		{
			int result;
			for (;;)
			{
				result = 0;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (true)
						{
						}
						result = A_1.Column - A_0.Column;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						if (A_0.Row != A_1.Row)
						{
							num = 1;
							continue;
						}
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000381E8 File Offset: 0x000371E8
		private int ᜁ(XlsRange A_0, XlsRange A_1)
		{
			int result;
			for (;;)
			{
				result = 0;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (true)
						{
						}
						result = A_1.Row - A_0.Row;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
						if (A_0.Row != A_1.Row)
						{
							num = 1;
							continue;
						}
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00038274 File Offset: 0x00037274
		private new void ᜀ(XlsRange A_0, XlsRange A_1)
		{
			int a_ = 10;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Comment != null)
					{
						goto IL_10F;
					}
					return;
				case 1:
					if (A_0.IsSingleCell)
					{
						num = 4;
						continue;
					}
					goto IL_78;
				case 2:
					return;
				case 3:
					if (!A_1.IsSingleCell)
					{
						num = 8;
						continue;
					}
					num = 0;
					continue;
				case 4:
					num = 3;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10F;
					default:
						goto IL_70;
					}
					break;
				case 6:
					A_1.AddComment(A_0.Comment);
					num = 2;
					continue;
				case 7:
					if (A_1 == null)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					num = 1;
					continue;
				case 8:
					goto IL_AA;
				case 9:
					goto IL_CD;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 7;
				continue;
				IL_10F:
				num = 6;
			}
			IL_70:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿ⵁㅃ㑅⭇⽉", a_));
			IL_78:
			throw new ArgumentException(RecordTableEnumerator.b("̿ⵁ㑃㽅桇㡉⥋⥍㥏㵑㩓癕㱗㕙籛そཟᙡ䑣୥१ṩཫ٭", a_));
			IL_AA:
			goto IL_78;
			IL_CD:
			throw new ArgumentNullException(RecordTableEnumerator.b("␿❁㝃㉅", a_));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000383D8 File Offset: 0x000373D8
		private void ᜁ(bool A_0)
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
			this.ᜁ(A_0, 1);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0003841C File Offset: 0x0003741C
		private void ᜁ(bool A_0, int A_1)
		{
			int a_ = 2;
			if (true)
			{
			}
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8C;
				case 1:
					goto IL_C5;
				case 2:
				{
					int num2;
					if (num2 < 0)
					{
						num = 5;
						continue;
					}
					this.ᜏ.RemoveRow(num2);
					int num3;
					num3++;
					num2--;
					num = 4;
					continue;
				}
				case 3:
				{
					int num3;
					if (num3 >= A_1)
					{
						num = 10;
						continue;
					}
					num = 2;
					continue;
				}
				case 4:
					goto IL_C5;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C5;
					default:
					{
						if (false)
						{
						}
						int num3;
						A_1 = num3;
						num = 0;
						continue;
					}
					}
					break;
				case 6:
					goto IL_63;
				case 7:
					return;
				case 9:
				{
					if (A_1 == 0)
					{
						num = 7;
						continue;
					}
					base.ParseData();
					int num4 = this.ᜏ.Table.ᜇ() + 1;
					int num3 = 0;
					int num2 = num4;
					num = 1;
					continue;
				}
				case 10:
					goto IL_DF;
				}
				if (A_1 < 0)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
				IL_C5:
				num = 3;
			}
			IL_63:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬷唹䤻倽㐿", a_));
			IL_8C:
			IL_DF:
			this.m_iLastRow = this.ᜏ.Table.ᜇ() + 1;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00038584 File Offset: 0x00037584
		private new void ᜀ(bool A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int iLastColumn = this.m_iLastColumn;
					this.m_iLastColumn = iLastColumn - 1;
					this.ᜏ.RemoveLastColumn(iLastColumn);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_10F;
						case 1:
						{
							Rectangle rectSource = Rectangle.FromLTRB(iLastColumn, 0, this.m_book.MaxColumnCount - 1, this.m_book.MaxRowCount - 1);
							Rectangle rectDest = Rectangle.FromLTRB(iLastColumn - 1, 0, this.m_book.MaxColumnCount - 1, this.m_book.MaxRowCount - 1);
							int num2 = this.m_book.AddSheetReference(this);
							this.m_book.UpdateFormula(num2, rectSource, num2, rectDest);
							num = 2;
							continue;
						}
						case 2:
							return;
						case 3:
							if (A_0)
							{
								num = 1;
								continue;
							}
							return;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								this.m_iLastColumn = (this.m_iFirstColumn = int.MaxValue);
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 5:
							if (this.m_iFirstColumn > this.m_iLastColumn)
							{
								num = 4;
								continue;
							}
							goto IL_10F;
						}
						break;
						IL_10F:
						num = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000386EC File Offset: 0x000376EC
		private new void ᜀ(bool A_0, int A_1)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					IL_4F:
					base.ParseData();
					int iLastColumn = this.m_iLastColumn;
					this.m_iLastColumn = iLastColumn - 1;
					int num = 0;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_187:
						num2 = 5;
						break;
					default:
						if (false)
						{
						}
						num2 = 10;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (A_0)
							{
								num2 = 7;
								continue;
							}
							return;
						case 1:
							if (this.m_iFirstColumn > this.m_iLastColumn)
							{
								num2 = 9;
								continue;
							}
							goto IL_1CB;
						case 2:
							if (this.m_iLastColumn < 0)
							{
								num2 = 4;
								continue;
							}
							this.ᜏ.RemoveLastColumn(this.m_iLastColumn + 1);
							num++;
							this.m_iLastColumn--;
							num2 = 8;
							continue;
						case 3:
							num2 = 2;
							continue;
						case 4:
							goto IL_B5;
						case 5:
							return;
						case 6:
							if (num < A_1)
							{
								num2 = 3;
								continue;
							}
							goto IL_B5;
						case 7:
							goto IL_1E6;
						case 8:
							goto IL_FD;
						case 9:
							this.m_iLastColumn = (this.m_iFirstColumn = int.MaxValue);
							num2 = 11;
							continue;
						case 10:
							goto IL_FD;
						case 11:
							goto IL_1CB;
						}
						goto IL_4F;
						IL_B5:
						this.m_iLastColumn = this.ᜏ.LastColumn + 1;
						num2 = 1;
						continue;
						IL_FD:
						num2 = 6;
						continue;
						IL_1CB:
						num2 = 0;
					}
					IL_1E6:
					Rectangle rectSource = Rectangle.FromLTRB(iLastColumn + A_1 - 1, 0, this.m_book.MaxColumnCount - 1, this.m_book.MaxRowCount - 1);
					Rectangle rectDest = Rectangle.FromLTRB(iLastColumn - 1, 0, this.m_book.MaxColumnCount - 1, this.m_book.MaxRowCount - 1);
					int num3 = this.m_book.AddSheetReference(this);
					this.m_book.UpdateFormula(num3, rectSource, num3, rectDest);
					goto IL_187;
				}
				return;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0003890C File Offset: 0x0003790C
		private new void ᜀ(Rectangle A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 11;
					for (;;)
					{
						int num7;
						switch (num)
						{
						case 0:
							goto IL_ED;
						case 1:
							goto IL_15C;
						case 2:
							goto IL_ED;
						case 3:
							return;
						case 4:
							goto IL_82;
						case 5:
						{
							int num2 = A_0.Top + 1;
							int num3 = A_0.Left + 1;
							int num4 = A_0.Bottom + 1;
							int num5 = A_0.Right + 1;
							int num6 = num2;
							num = 0;
							continue;
						}
						case 6:
						{
							int num4;
							int num6;
							if (num6 > num4)
							{
								num = 3;
								continue;
							}
							int num3;
							num7 = num3;
							num = 4;
							continue;
						}
						case 7:
						{
							XlsRange range;
							if (range != null)
							{
								num = 1;
								continue;
							}
							goto IL_AC;
						}
						case 8:
						{
							if (true)
							{
							}
							int num5;
							if (num7 > num5)
							{
								num = 10;
								continue;
							}
							int num6;
							long iKey = sprṔ.ᜀ(num7, num6);
							XlsRange range = this.ᜏ.GetRange(iKey);
							num = 7;
							continue;
						}
						case 9:
							goto IL_AC;
						case 10:
						{
							int num6;
							num6++;
							num = 2;
							continue;
						}
						case 11:
							if (this.ᜏ.UseCache)
							{
								num = 5;
								continue;
							}
							return;
						case 12:
							goto IL_82;
						}
						break;
						IL_82:
						num = 8;
						continue;
						IL_AC:
						num7++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							IL_15C:
							XlsRange range;
							range.PartialClear();
							num = 9;
							continue;
						}
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						IL_ED:
						num = 6;
					}
				}
				return;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00038AC8 File Offset: 0x00037AC8
		private new sprủ ᜀ(IXLSRange A_0, IXLSRange A_1, ref int A_2, ref int A_3, XlsCellRecordCollection A_4)
		{
			int a_ = 19;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_8C;
					}
					break;
				case 1:
					goto IL_60;
				case 2:
					if (A_4 == null)
					{
						num = 1;
						continue;
					}
					goto IL_DF;
				case 3:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				case 4:
					goto IL_D3;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 0;
				}
				else
				{
					num = 3;
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("㵈⩊⽌⍎㑐R㩔≖⭘㡚㡜", a_));
			IL_8C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈⑊㡌㵎㉐㙒", a_));
			IL_D3:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ⹊㹌㭎㡐㵒㑔⍖じ㑚㍜", a_));
			IL_DF:
			XlsWorksheet xlsWorksheet = (XlsWorksheet)A_1.Parent;
			XlsWorksheet xlsWorksheet2 = (XlsWorksheet)A_0.Worksheet;
			int a_2 = A_1.Row - A_0.Row;
			int a_3 = A_1.Column - A_0.Column;
			int lastColumn = A_0.LastColumn;
			int column = A_0.Column;
			return A_4.ᜀ((XlsRange)A_0, a_2, a_3, ref A_2, ref A_3);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00038C08 File Offset: 0x00037C08
		private new void ᜀ(sprủ A_0, sprủ A_1, bool A_2)
		{
			int a_ = 5;
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_EC:
				A_1.ᜄ(num);
				num2 = 25;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num2 = 24;
					break;
				}
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					sprᱧ sprᱧ;
					sprᱧ sprᱧ2;
					sprᱧ.ᜀ(sprᱧ2);
					num2 = 17;
					continue;
				}
				case 1:
				{
					sprᱧ sprᱧ;
					if (sprᱧ != null)
					{
						num2 = 15;
						continue;
					}
					goto IL_19D;
				}
				case 2:
					goto IL_2C9;
				case 3:
				{
					sprᱧ sprᱧ = new sprᱧ(num, base.AppImplementation.ᜅ(), A_1.ᜈ().DefaultXFIndex);
					A_1.ᜀ(num, sprᱧ);
					num2 = 5;
					continue;
				}
				case 4:
					goto IL_1CD;
				case 5:
					goto IL_CC;
				case 6:
				{
					sprᱧ sprᱧ2;
					if (sprᱧ2.ᜈ() > 0)
					{
						num2 = 18;
						continue;
					}
					goto IL_19D;
				}
				case 7:
					goto IL_19D;
				case 8:
					if (true)
					{
					}
					num2 = 21;
					continue;
				case 9:
				{
					sprᱧ sprᱧ;
					if (sprᱧ.ᜈ() == 0)
					{
						num2 = 8;
						continue;
					}
					goto IL_19D;
				}
				case 10:
					return;
				case 11:
					num2 = 1;
					continue;
				case 12:
					goto IL_21A;
				case 13:
					goto IL_C7;
				case 14:
					if (A_1 == null)
					{
						num2 = 2;
						continue;
					}
					num2 = 16;
					continue;
				case 15:
					num2 = 9;
					continue;
				case 16:
				{
					if (A_0.ᜁ() < 0)
					{
						num2 = 10;
						continue;
					}
					num = A_0.ᜁ();
					int num3 = A_0.ᜇ();
					num2 = 12;
					continue;
				}
				case 17:
					goto IL_12D;
				case 18:
				{
					sprᱧ sprᱧ2;
					sprᜑ.ᜁ(this, sprᱧ2.\u171C() + 1);
					sprᜑ.ᜁ(this, sprᱧ2.\u171E() + 1);
					sprᱧ sprᱧ;
					sprᱧ.ᜀ(sprᱧ2, base.ReservedHandle.\u171D(), A_1.ᜈ().HeapHandle);
					num2 = 7;
					continue;
				}
				case 19:
					return;
				case 20:
				{
					sprᱧ sprᱧ;
					if (sprᱧ == null)
					{
						num2 = 3;
						continue;
					}
					goto IL_CC;
				}
				case 21:
					if (A_2)
					{
						num2 = 4;
						continue;
					}
					goto IL_19D;
				case 22:
					goto IL_21A;
				case 23:
					if (A_2)
					{
						num2 = 0;
						continue;
					}
					goto IL_12D;
				case 25:
					goto IL_19D;
				case 26:
				{
					int num3;
					if (num > num3)
					{
						num2 = 19;
						continue;
					}
					sprᱧ sprᱧ2 = A_0.ᜄ().ᜁ(num);
					sprᱧ sprᱧ = A_1.ᜄ().ᜁ(num);
					num2 = 27;
					continue;
				}
				case 27:
				{
					sprᱧ sprᱧ2;
					if (sprᱧ2 == null)
					{
						num2 = 11;
						continue;
					}
					sprᱧ sprᱧ = A_1.ᜄ().ᜁ(num);
					sprᜑ.ᜀ(this, num + 1);
					num2 = 20;
					continue;
				}
				}
				if (A_0 == null)
				{
					num2 = 13;
					continue;
				}
				num2 = 14;
				continue;
				IL_CC:
				num2 = 23;
				continue;
				IL_12D:
				num2 = 6;
				continue;
				IL_19D:
				num++;
				num2 = 22;
				continue;
				IL_21A:
				num2 = 26;
			}
			IL_C7:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺刼䨾㍀⁂⁄", a_));
			IL_1CD:
			goto IL_EC;
			IL_2C9:
			throw new ArgumentNullException(RecordTableEnumerator.b("强堼䰾㕀⩂⭄♆㵈≊≌ⅎ", a_));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00038F90 File Offset: 0x00037F90
		private new static void ᜀ(IDictionary A_0, Rectangle A_1)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						int num3;
						if (num2 > num3)
						{
							num = 5;
							continue;
						}
						int num5;
						long num4 = sprṔ.ᜀ(num2, num5);
						A_0.Remove(num4);
						num2++;
						num = 9;
						continue;
					}
					case 1:
						goto IL_114;
					case 3:
						goto IL_114;
					case 4:
						goto IL_8B;
					case 5:
					{
						int num5;
						num5++;
						num = 1;
						continue;
					}
					case 6:
						return;
					case 7:
						goto IL_90;
					case 8:
					{
						int num5;
						int num6;
						if (num5 > num6)
						{
							num = 6;
							continue;
						}
						int num7;
						int num2 = num7;
						num = 7;
						continue;
					}
					case 9:
						goto IL_90;
					}
					if (true)
					{
					}
					if (A_0 != null)
					{
						int num8 = A_1.Top + 1;
						int num7 = A_1.Left + 1;
						int num6 = A_1.Bottom + 1;
						int num3 = A_1.Right + 1;
						int num5 = num8;
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_133;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					IL_90:
					num = 0;
					continue;
					IL_114:
					num = 8;
				}
				IL_8B:
				IL_133:
				throw new ArgumentNullException(RecordTableEnumerator.b("⅄⹆⩈㽊⑌⁎㽐㉒❔⹖", a_));
			}
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0003910C File Offset: 0x0003810C
		internal new void ᜀ(ICombinedRange A_0, bool? A_1)
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
			this.ᜰ.ᜀ(A_0, A_1);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00039154 File Offset: 0x00038154
		private new void ᜀ(spr\u225F A_0, IWorksheet A_1, int A_2, int A_3)
		{
			int a_ = 2;
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_46;
					case 2:
						if (A_1 == null)
						{
							num = 3;
							continue;
						}
						goto IL_85;
					case 3:
						goto IL_6F;
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						num = 2;
					}
				}
				IL_85:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_9B;
				}
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("夷䠹主弽㤿", a_));
			IL_6F:
			throw new ArgumentNullException(RecordTableEnumerator.b("尷弹伻䨽ጿ⩁⅃⍅㱇", a_));
			IL_9B:
			if (false)
			{
			}
			XlsWorkbook xlsWorkbook = (XlsWorkbook)A_1.Workbook;
			A_0.ᜀ(xlsWorkbook.FormulaUtil.ᜀ(A_0.ᜅ(), A_2, A_3));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00039228 File Offset: 0x00038228
		private new sprủ ᜀ(int A_0, int A_1, Rectangle A_2, sprủ A_3, sprủ A_4)
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
				if (!UtilityMethods.ᜀ(A_2, A_1, A_0))
				{
					if (true)
					{
					}
					return A_4;
				}
				break;
			}
			return A_3;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00039274 File Offset: 0x00038274
		private new Dictionary<int, int> ᜀ(int A_0, int A_1, int A_2, int A_3, XlsWorksheet A_4, out Dictionary<int, int> A_5)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 1;
				IList<spr\u192F> list;
				for (;;)
				{
					int num4;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C5;
						default:
						{
							if (false)
							{
							}
							int num2;
							int num3;
							if (num2 >= num3)
							{
								num = 5;
								continue;
							}
							num4 = A_1;
							int num5 = A_1 + A_3;
							num = 9;
							continue;
						}
						}
						break;
					case 2:
					{
						spr\u23A5 spr_u23A;
						if (spr_u23A != null)
						{
							num = 13;
							continue;
						}
						goto IL_77;
					}
					case 3:
						goto IL_77;
					case 4:
						goto IL_128;
					case 5:
						goto IL_18C;
					case 6:
						goto IL_FD;
					case 7:
					{
						int num5;
						if (num4 >= num5)
						{
							num = 14;
							continue;
						}
						int num2;
						long a_2 = sprṔ.ᜀ(num4, num2);
						spr\u23A5 spr_u23A = this.ᜏ.ᜄ(a_2);
						num = 2;
						continue;
					}
					case 8:
						goto IL_72;
					case 9:
						goto IL_128;
					case 10:
						goto IL_14B;
					case 11:
						goto IL_14B;
					case 12:
					{
						if (this.m_book == A_4.Workbook)
						{
							num = 6;
							continue;
						}
						base.ParseData();
						A_5 = new Dictionary<int, int>();
						Dictionary<int, object> a_3 = new Dictionary<int, object>();
						list = new List<spr\u192F>();
						sprᢖ sprᢖ = this.m_book.InnerExtFormats;
						int num2 = A_0;
						int num3 = A_0 + A_2;
						num = 10;
						continue;
					}
					case 13:
					{
						spr\u23A5 spr_u23A;
						int a_4 = (int)spr_u23A.ᜆ();
						Dictionary<int, object> a_3;
						sprᢖ sprᢖ;
						sprᢖ.ᜀ(a_3, list, a_4);
						num = 3;
						continue;
					}
					case 14:
					{
						int num2;
						num2++;
						num = 11;
						continue;
					}
					}
					if (A_4 == null)
					{
						num = 8;
						continue;
					}
					goto IL_C5;
					IL_77:
					num4++;
					num = 4;
					continue;
					IL_C5:
					A_5 = null;
					num = 12;
					continue;
					IL_128:
					num = 7;
					continue;
					IL_14B:
					num = 0;
				}
				IL_72:
				throw new ArgumentNullException(RecordTableEnumerator.b("嬾⑀あㅄᑆⅈ⹊⡌㭎", a_));
				IL_FD:
				return null;
				IL_18C:
				XlsWorkbook parentWorkbook = A_4.ParentWorkbook;
				sprᢖ sprᢖ2 = parentWorkbook.InnerExtFormats;
				return sprᢖ2.ᜀ(list, out A_5);
			}
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000394A8 File Offset: 0x000384A8
		private new void ᜀ(long A_0)
		{
			for (;;)
			{
				if (true)
				{
				}
				base.ParseData();
				this.ᜏ.Remove(A_0);
				XlsRange range = this.ᜏ.GetRange(A_0);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							range.ClearContents();
							num = 0;
							continue;
						}
						break;
					case 2:
						if (range != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00039540 File Offset: 0x00038540
		private new void ᜀ(spr\u225F A_0)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num2;
				int num3;
				int num4;
				for (;;)
				{
					base.ParseData();
					Ptg ptg = FormulaUtil.ᜀ(FormulaToken.tExp, new object[]
					{
						A_0.ᜉ(),
						(ushort)A_0.ᜈ()
					});
					num = A_0.\u170D();
					num2 = A_0.ᜀ();
					num3 = A_0.ᜉ();
					num4 = A_0.ᜈ();
					int num5 = num3;
					int num6 = 1;
					for (;;)
					{
						switch (num6)
						{
						case 0:
						{
							if (num5 > num)
							{
								num6 = 5;
								continue;
							}
							int num7 = num4;
							num6 = 2;
							continue;
						}
						case 1:
							goto IL_117;
						case 2:
							goto IL_112;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_112;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								goto IL_117;
							}
							break;
						case 4:
						{
							int num7;
							if (num7 > num2)
							{
								num6 = 7;
								continue;
							}
							long a_ = sprṔ.ᜀ(num7 + 1, num5 + 1);
							spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(TBIFFRecord.Formula);
							spr᱒.ᜇ(num5);
							spr᱒.ᜆ(num7);
							spr᱒.ᜁ(new Ptg[]
							{
								(Ptg)ptg.Clone()
							});
							this.ᜏ.ᜁ(a_, spr᱒);
							XlsRange xlsRange = (XlsRange)this.AllocatedRange[num5 + 1, num7 + 1];
							xlsRange.\u171B();
							num7++;
							num6 = 6;
							continue;
						}
						case 5:
							goto IL_137;
						case 6:
							goto IL_E0;
						case 7:
							num5++;
							num6 = 3;
							continue;
						}
						break;
						IL_E0:
						num6 = 4;
						continue;
						IL_112:
						goto IL_E0;
						IL_117:
						num6 = 0;
					}
				}
				IL_137:
				this.UpdateFirstLast(num3 + 1, num4 + 1);
				this.UpdateFirstLast(num + 1, num2 + 1);
				return;
			}
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00039738 File Offset: 0x00038738
		private new spr\u225F ᜀ(spr\u225F A_0, IXLSRange A_1, IXLSRange A_2, int A_3, int A_4, bool A_5)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				spr\u225F spr_u225F;
				int a_2;
				for (;;)
				{
					IL_17:
					int num = 10;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (A_1 == null)
							{
								num = 16;
								continue;
							}
							num = 18;
							continue;
						case 1:
							if (!A_5)
							{
								num = 5;
								continue;
							}
							num = 6;
							continue;
						case 2:
							num = 17;
							continue;
						case 3:
							goto IL_32D;
						case 4:
						{
							Rectangle rectangle;
							if (rectangle.Left < 0)
							{
								num = 7;
								continue;
							}
							num = 15;
							continue;
						}
						case 5:
							num = 11;
							continue;
						case 6:
							num2 = spr_u225F.ᜉ() - A_0.ᜉ();
							goto IL_342;
						case 7:
							goto IL_286;
						case 8:
						{
							Rectangle rectangle;
							if (rectangle.IsEmpty)
							{
								num = 12;
								continue;
							}
							rectangle.Offset(A_1.Row - A_2.Row, A_1.Column - A_2.Column);
							num = 4;
							continue;
						}
						case 9:
							goto IL_31F;
						case 11:
							num2 = 0;
							goto IL_342;
						case 12:
							goto IL_132;
						case 13:
							goto IL_88;
						case 14:
							if (!A_5)
							{
								num = 2;
								continue;
							}
							num = 3;
							continue;
						case 15:
						{
							Rectangle rectangle;
							if (rectangle.Top < 0)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_17;
								}
								if (false)
								{
								}
								num = 9;
								continue;
							}
							if (true)
							{
							}
							spr_u225F = (spr\u225F)spr\u175E.ᜀ(TBIFFRecord.Array);
							spr_u225F.ᜂ(A_3 + A_1.Row - A_2.Row - 1);
							spr_u225F.ᜃ(A_4 + A_1.Column - A_2.Column - 1);
							spr_u225F.ᜀ(spr_u225F.ᜉ() - rectangle.Left + rectangle.Right);
							spr_u225F.ᜁ(spr_u225F.ᜈ() - rectangle.Top + rectangle.Bottom);
							spr_u225F.ᜀ(true);
							spr_u225F.ᜁ(true);
							num = 1;
							continue;
						}
						case 16:
							goto IL_2A6;
						case 17:
							goto IL_21D;
						case 18:
						{
							if (A_2 == null)
							{
								num = 19;
								continue;
							}
							base.ParseData();
							Rectangle a = Rectangle.FromLTRB(A_0.ᜉ(), A_0.ᜈ(), A_0.\u170D(), A_0.ᜀ());
							Rectangle b = Rectangle.FromLTRB(A_2.Row - 1, A_2.Column - 1, A_2.LastRow - 1, A_2.LastColumn - 1);
							Rectangle rectangle = Rectangle.Intersect(a, b);
							num = 8;
							continue;
						}
						case 19:
							goto IL_241;
						}
						if (A_0 == null)
						{
							num = 13;
							continue;
						}
						num = 0;
						continue;
						IL_342:
						a_2 = num2;
						num = 14;
					}
				}
				IL_88:
				throw new ArgumentNullException(RecordTableEnumerator.b("╃㑅㩇⭉㕋ᵍ㽏❑♓㕕㵗", a_));
				IL_132:
				throw new ArgumentNullException(RecordTableEnumerator.b("ൃ⡅㱇⽉㹋㵍㕏ㅑ⁓㽕㝗㑙籛㝝፟䉡ţ୥ᡧṩᕫ", a_));
				IL_21D:
				int num3 = 0;
				goto IL_365;
				IL_241:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃⥅㵇㡉⽋⭍", a_));
				IL_286:
				throw new ArgumentOutOfRangeException();
				IL_2A6:
				throw new ArgumentNullException(RecordTableEnumerator.b("⁃⍅㭇㹉╋⁍ㅏ♑㵓㥕㙗", a_));
				IL_31F:
				throw new ArgumentOutOfRangeException();
				IL_32D:
				num3 = spr_u225F.ᜈ() - A_0.ᜈ();
				IL_365:
				int a_3 = num3;
				spr_u225F.ᜀ(this.ᜀ(A_0.ᜅ(), a_2, a_3));
				return spr_u225F;
			}
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00039AC4 File Offset: 0x00038AC4
		protected void CheckRangesSizes(IXLSRange destination, IXLSRange source)
		{
			int a_ = 3;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					if (destination.LastColumn - destination.Column != source.LastColumn - source.Column)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 3:
					goto IL_B6;
				}
				if (destination.LastRow - destination.Row != source.LastRow - source.Row)
				{
					break;
				}
				num = 0;
			}
			IL_4F:
			throw new ArgumentException(RecordTableEnumerator.b("欸娺匼堾⑀あ敄⍆♈歊⍌⁎═獒㡔㙖ⵘ㡚㕜煞", a_));
			IL_B6:
			goto IL_4F;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00039B94 File Offset: 0x00038B94
		private new void ᜀ(IXLSRange A_0, IXLSRange A_1)
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
			XlsWorksheet.ᜀ(A_0, A_1, false);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00039BD8 File Offset: 0x00038BD8
		private new static void ᜀ(IXLSRange A_0, IXLSRange A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				XlsRange xlsRange;
				XlsRange xlsRange2;
				spr\u1FBC spr_u1FBC;
				for (;;)
				{
					xlsRange = (XlsRange)A_1;
					xlsRange2 = (XlsRange)A_0;
					spr_u1FBC = xlsRange.InnerWorksheet.MergeCells;
					spr\u1FBC spr_u1FBC2 = xlsRange2.InnerWorksheet.MergeCells;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								if (spr_u1FBC != null)
								{
									num = 3;
									continue;
								}
								return;
							}
							break;
						case 1:
							goto IL_10B;
						case 2:
							goto IL_136;
						case 3:
							num = 4;
							continue;
						case 4:
						{
							if (spr_u1FBC == spr_u1FBC2)
							{
								num = 2;
								continue;
							}
							int a_ = A_0.Row - A_1.Row;
							int a_2 = A_0.Column - A_1.Column;
							List<spr\u25A6.ᜀ> a_3 = spr_u1FBC.ᜀ(xlsRange, A_2);
							Rectangle a_4 = Rectangle.FromLTRB(A_0.Column - 1, A_0.Row - 1, A_0.LastColumn - 1, A_0.LastRow - 1);
							spr_u1FBC2.ᜀ(a_4);
							spr_u1FBC2.ᜀ(a_3, a_, a_2);
							num = 1;
							continue;
						}
						}
						break;
					}
				}
				IL_10B:
				return;
				IL_136:
				spr_u1FBC.ᜀ(xlsRange2, xlsRange, A_2);
				return;
			}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00039D20 File Offset: 0x00038D20
		internal spr\u2114 ᜌ(int A_0)
		{
			int a_ = 17;
			int num = 0;
			spr\u2114 result;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_64;
				case 2:
					if (this.\u171C != null)
					{
						num = 4;
						continue;
					}
					return result;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_9E;
					}
					break;
				case 4:
					this.\u171C.TryGetValue(A_0, out result);
					num = 1;
					continue;
				}
				if (A_0 < 0)
				{
					if (true)
					{
					}
					num = 3;
				}
				else
				{
					base.ParseData();
					result = null;
					num = 2;
				}
			}
			IL_64:
			return result;
			IL_9E:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎煐潒畔杖", a_));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00039DE8 File Offset: 0x00038DE8
		internal new void ᜀ(spr\u2114 A_0)
		{
			switch (0)
			{
			default:
			{
				int key;
				long key2;
				for (;;)
				{
					base.ParseData();
					key = (int)A_0.ᜄ();
					bool flag = this.\u171C == null;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (flag)
							{
								goto IL_89;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7F;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 1:
							if (this.\u171C.ContainsKey(key))
							{
								num = 3;
								continue;
							}
							goto IL_89;
						case 2:
							this.\u171C = new SortedList<int, spr\u2114>();
							this.\u171D = new SortedList<long, spr\u2114>();
							num = 6;
							continue;
						case 3:
						{
							spr\u2114 spr_u = this.\u171C[key];
							key2 = sprṔ.ᜀ((int)spr_u.ᜁ(), (int)spr_u.ᜀ());
							this.\u171D.Remove(key2);
							num = 5;
							continue;
						}
						case 4:
							goto IL_7F;
						case 5:
							goto IL_E1;
						case 6:
							goto IL_137;
						case 7:
							if (flag)
							{
								num = 2;
								continue;
							}
							goto IL_139;
						}
						break;
						IL_7F:
						if (true)
						{
						}
						num = 1;
						continue;
						IL_89:
						num = 7;
					}
				}
				IL_E1:
				IL_137:
				IL_139:
				this.\u171C[key] = A_0;
				key2 = sprṔ.ᜀ((int)A_0.ᜁ(), (int)A_0.ᜀ());
				this.\u171D[key2] = A_0;
				return;
			}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00039F5C File Offset: 0x00038F5C
		public void AutofitRow(int rowIndex, int firstColumn, int lastColumn, bool bRaiseEvents)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				bool bIsBadFontHeight;
				double num3;
				for (;;)
				{
					base.ParseData();
					RichTextString a_2 = new RichTextString(base.AppImplementation, this, false, true);
					int num = 5;
					for (;;)
					{
						SizeF sizeF2;
						int num4;
						switch (num)
						{
						case 0:
							goto IL_15F;
						case 1:
							goto IL_161;
						case 2:
							goto IL_19D;
						case 3:
						{
							long num2;
							SizeF sizeF = this.ᜀ(num2, true, a_2, false, out bIsBadFontHeight);
							num = 8;
							continue;
						}
						case 4:
							if (true)
							{
							}
							if (num3 > 409.5)
							{
								num = 11;
								continue;
							}
							goto IL_31F;
						case 5:
							if (firstColumn != 0)
							{
								num = 6;
								continue;
							}
							return;
						case 6:
							num = 12;
							continue;
						case 7:
							goto IL_293;
						case 8:
						{
							SizeF sizeF;
							if (sizeF2.Height < sizeF.Height)
							{
								num = 10;
								continue;
							}
							goto IL_293;
						}
						case 9:
							num = 14;
							continue;
						case 10:
						{
							SizeF sizeF;
							sizeF2.Height = sizeF.Height;
							num = 7;
							continue;
						}
						case 11:
							num3 = 409.5;
							num = 2;
							continue;
						case 12:
							if (lastColumn != 0)
							{
								num = 9;
								continue;
							}
							return;
						case 13:
							sizeF2.Height = (this.m_book.Styles[RecordTableEnumerator.b("ॆ♈㥊⁌⹎㵐", a_)].Font as FontWrapper).Wrapped.MeasureString('0'.ToString()).Height;
							num = 19;
							continue;
						case 14:
							if (firstColumn > lastColumn)
							{
								num = 0;
								continue;
							}
							sizeF2 = new SizeF(0f, 0f);
							(this.m_book.Styles[RecordTableEnumerator.b("ॆ♈㥊⁌⹎㵐", a_)].Font as FontWrapper).Wrapped;
							bIsBadFontHeight = false;
							num4 = firstColumn;
							num = 1;
							continue;
						case 15:
						{
							if (num4 > lastColumn)
							{
								num = 20;
								continue;
							}
							long num2 = sprṔ.ᜀ(num4, rowIndex);
							num = 18;
							continue;
						}
						case 16:
							if (sizeF2.Height == 0f)
							{
								num = 13;
								continue;
							}
							goto IL_FE;
						case 17:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_28E;
							default:
								if (false)
								{
								}
								goto IL_161;
							}
							break;
						case 18:
						{
							long num2;
							if (this.ᜏ.Contains(num2))
							{
								num = 3;
								continue;
							}
							goto IL_293;
						}
						case 19:
							goto IL_28E;
						case 20:
							num = 16;
							continue;
						}
						break;
						IL_FE:
						num3 = spr\u17FF.ᜀ((double)sizeF2.Height, MeasureUnits.Point);
						num = 4;
						continue;
						IL_28E:
						goto IL_FE;
						IL_161:
						num = 15;
						continue;
						IL_293:
						num4++;
						num = 17;
					}
				}
				return;
				IL_15F:
				return;
				IL_19D:
				IL_31F:
				(this.AllocatedRange[rowIndex, firstColumn] as XlsRange).SetRowHeight(num3, bIsBadFontHeight);
				return;
			}
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0003A2A4 File Offset: 0x000392A4
		internal new void ᜀ(int A_0, double A_1, bool A_2, MeasureUnits A_3, bool A_4)
		{
			sprᱧ sprᱧ;
			for (;;)
			{
				A_1 = base.ReservedHandle.ᜀ(A_1, A_3, MeasureUnits.Point);
				sprᱧ = sprᜑ.ᜀ(this, A_0 - 1, true);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						ushort num2;
						if (sprᱧ.\u1718() != num2)
						{
							num = 1;
							continue;
						}
						goto IL_65;
					}
					case 1:
					{
						ushort num2;
						sprᱧ.ᜃ(num2);
						sprᱧ.ᜊ(A_2);
						sprᜑ.ᜀ(this, A_0);
						base.SetChanged();
						num = 7;
						continue;
					}
					case 2:
					{
						if (A_1 == 0.0)
						{
							num = 5;
							continue;
						}
						ushort num2 = (ushort)Math.Round(A_1 * 20.0);
						num = 0;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D7;
						default:
							if (false)
							{
							}
							if (A_4)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 4:
						goto IL_D7;
					case 5:
						goto IL_63;
					case 6:
						return;
					case 7:
						goto IL_65;
					}
					break;
					IL_65:
					num = 3;
					continue;
					IL_D7:
					this.RaiseRowHeightChangedEvent(A_0, A_1);
					if (true)
					{
					}
					num = 6;
				}
			}
			IL_63:
			sprᱧ.ᜅ(true);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0003A3E0 File Offset: 0x000393E0
		private bool ᜆ(int A_0)
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
			return this.ᜁ(A_0, true);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0003A424 File Offset: 0x00039424
		private bool ᜁ(int A_0, bool A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 5;
					for (;;)
					{
						bool flag;
						spr\u23A5 spr_u23A;
						int num3;
						switch (num)
						{
						case 0:
						{
							flag = true;
							long num2;
							spr_u23A = this.ᜏ.ᜄ(num2);
							num = 1;
							continue;
						}
						case 1:
							if (A_1)
							{
								num = 13;
								continue;
							}
							goto IL_94;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_16B;
							default:
								if (false)
								{
								}
								num = 15;
								continue;
							}
							break;
						case 3:
						{
							long num2;
							if (this.ᜏ.Contains(num2))
							{
								num = 0;
								continue;
							}
							goto IL_10A;
						}
						case 4:
							goto IL_94;
						case 5:
							if (A_0 >= this.m_iFirstRow)
							{
								num = 17;
								continue;
							}
							return true;
						case 6:
							goto IL_226;
						case 7:
						{
							if (A_0 > this.m_iLastRow)
							{
								num = 6;
								continue;
							}
							int defaultXFIndex = this.m_book.DefaultXFIndex;
							num3 = this.m_iFirstColumn;
							num = 9;
							continue;
						}
						case 8:
							if (flag)
							{
								num = 19;
								continue;
							}
							goto IL_10A;
						case 9:
							goto IL_139;
						case 10:
							goto IL_126;
						case 11:
							return true;
						case 12:
							goto IL_16B;
						case 13:
							num = 12;
							continue;
						case 14:
						{
							int defaultXFIndex;
							int num4;
							if (num4 != defaultXFIndex)
							{
								num = 2;
								continue;
							}
							goto IL_126;
						}
						case 15:
						{
							int num4;
							if (num4 == 0)
							{
								num = 10;
								continue;
							}
							goto IL_94;
						}
						case 16:
							goto IL_139;
						case 17:
							num = 7;
							continue;
						case 18:
						{
							if (num3 > this.m_iLastColumn)
							{
								num = 11;
								continue;
							}
							long num2 = sprṔ.ᜀ(num3, A_0);
							num = 3;
							continue;
						}
						case 19:
							return false;
						case 20:
						{
							int num4 = (int)spr_u23A.ᜆ();
							num = 14;
							continue;
						}
						}
						break;
						IL_94:
						num = 8;
						continue;
						IL_16B:
						if (spr_u23A.get_TypeCode() == TBIFFRecord.Blank)
						{
							num = 20;
							continue;
						}
						goto IL_94;
						IL_10A:
						if (true)
						{
						}
						num3++;
						num = 16;
						continue;
						IL_126:
						flag = false;
						num = 4;
						continue;
						IL_139:
						num = 18;
					}
				}
				return false;
				IL_226:
				return true;
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0003A690 File Offset: 0x00039690
		private new bool ᜅ(int A_0)
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
			return this.ᜀ(A_0, true);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0003A6D4 File Offset: 0x000396D4
		private new bool ᜀ(int A_0, bool A_1)
		{
			int a_ = 9;
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 22;
					for (;;)
					{
						spr\u23A5 spr_u23A;
						bool flag;
						int num2;
						long num4;
						switch (num)
						{
						case 0:
							num = 18;
							continue;
						case 1:
							goto IL_167;
						case 2:
							if (A_0 >= this.m_iFirstColumn)
							{
								num = 9;
								continue;
							}
							return true;
						case 3:
							if (spr_u23A.get_TypeCode() == TBIFFRecord.Blank)
							{
								num = 12;
								continue;
							}
							goto IL_206;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_198;
							default:
								if (false)
								{
								}
								if (flag)
								{
									num = 16;
									continue;
								}
								goto IL_1F1;
							}
							break;
						case 5:
							goto IL_B5;
						case 6:
							goto IL_206;
						case 7:
							num = 3;
							continue;
						case 8:
							goto IL_198;
						case 9:
							num = 10;
							continue;
						case 10:
						{
							if (A_0 > this.m_iLastColumn)
							{
								num = 11;
								continue;
							}
							int defaultXFIndex = this.m_book.DefaultXFIndex;
							num2 = this.m_iFirstRow;
							num = 19;
							continue;
						}
						case 11:
							goto IL_11A;
						case 12:
						{
							int num3 = (int)spr_u23A.ᜆ();
							num = 21;
							continue;
						}
						case 13:
							num = 24;
							continue;
						case 14:
							goto IL_28F;
						case 15:
							if (num2 > this.m_iLastRow)
							{
								if (true)
								{
								}
								num = 20;
								continue;
							}
							num4 = sprṔ.ᜀ(A_0, num2);
							num = 17;
							continue;
						case 16:
							return false;
						case 17:
							if (this.ᜏ.Contains(num4))
							{
								num = 8;
								continue;
							}
							goto IL_1F1;
						case 18:
						{
							int num3;
							if (num3 == 0)
							{
								num = 5;
								continue;
							}
							goto IL_206;
						}
						case 19:
							goto IL_167;
						case 20:
							return true;
						case 21:
						{
							int defaultXFIndex;
							int num3;
							if (num3 != defaultXFIndex)
							{
								num = 0;
								continue;
							}
							goto IL_B5;
						}
						case 22:
							if (A_0 >= 1)
							{
								num = 13;
								continue;
							}
							goto IL_121;
						case 23:
							if (A_1)
							{
								num = 7;
								continue;
							}
							goto IL_206;
						case 24:
							if (A_0 > this.m_book.MaxColumnCount)
							{
								num = 14;
								continue;
							}
							num = 2;
							continue;
						}
						break;
						IL_B5:
						flag = false;
						num = 6;
						continue;
						IL_167:
						num = 15;
						continue;
						IL_198:
						flag = true;
						spr_u23A = this.ᜏ.ᜄ(num4);
						num = 23;
						continue;
						IL_1F1:
						num2++;
						num = 1;
						continue;
						IL_206:
						num = 4;
					}
				}
				IL_11A:
				return true;
				IL_121:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("簾⹀⽂い⩆❈Ɋ⍌⭎㑐⭒", a_), RecordTableEnumerator.b("椾⁀⽂い≆楈⡊ⱌⅎ㽐㱒⅔睖㭘㹚絜㍞Ѡၢᙤ䝦塨䭪౬Ůᕰ卲ቴնᱸ᩺ॼ᩾ꎂ권떔쾠莢첤즦춨캪햬膮", a_));
				IL_28F:
				goto IL_121;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0003A9DC File Offset: 0x000399DC
		private new int ᜀ(IXLSRange A_0, string A_1, string A_2, int A_3)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 16;
				int length;
				int length2;
				int num2;
				string text;
				for (;;)
				{
					int num4;
					switch (num)
					{
					case 0:
						goto IL_2AE;
					case 1:
						goto IL_158;
					case 2:
						goto IL_244;
					case 3:
					{
						if (A_2 == null)
						{
							num = 0;
							continue;
						}
						length = A_1.Length;
						length2 = A_2.Length;
						bool flag = true;
						num2 = A_3;
						num = 18;
						continue;
					}
					case 4:
						if (text.IndexOf('\n') >= 0)
						{
							num = 10;
							continue;
						}
						goto IL_3FD;
					case 5:
					{
						char c;
						if (c == '"')
						{
							num = 12;
							continue;
						}
						goto IL_399;
					}
					case 6:
						goto IL_17B;
					case 7:
						num = 15;
						continue;
					case 8:
						goto IL_158;
					case 9:
					{
						int num3;
						if (num3 != -1)
						{
							num = 28;
							continue;
						}
						num2++;
						num = 29;
						continue;
					}
					case 10:
						A_0.IsWrapText = true;
						num = 2;
						continue;
					case 11:
						if (A_1 == null)
						{
							num = 19;
							continue;
						}
						num = 3;
						continue;
					case 12:
						num = 26;
						continue;
					case 13:
						if (num4 > 1)
						{
							num = 7;
							continue;
						}
						goto IL_17B;
					case 14:
					{
						if (num2 >= length)
						{
							num = 30;
							continue;
						}
						char c = A_1[num2];
						num = 5;
						continue;
					}
					case 15:
						if (text[0] == '"')
						{
							num = 25;
							continue;
						}
						goto IL_17B;
					case 17:
					{
						bool flag;
						if (flag)
						{
							num = 32;
							continue;
						}
						goto IL_34F;
					}
					case 18:
						goto IL_158;
					case 19:
						goto IL_3F8;
					case 20:
					{
						bool flag = false;
						num = 27;
						continue;
					}
					case 21:
					{
						int num3 = A_1.IndexOf('"', num2 + 1);
						num = 9;
						continue;
					}
					case 22:
						if (string.CompareOrdinal(A_1, num2, A_2, 0, length2) == 0)
						{
							num = 20;
							continue;
						}
						num2++;
						num = 1;
						continue;
					case 23:
						goto IL_C2;
					case 24:
						text = text.Substring(1, num4 - 2);
						num = 6;
						continue;
					case 25:
						num = 31;
						continue;
					case 26:
						if (num2 + 1 >= length)
						{
							goto IL_399;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FA;
						default:
							if (false)
							{
							}
							num = 21;
							continue;
						}
						break;
					case 27:
						goto IL_FA;
					case 28:
					{
						int num3;
						num2 = num3 + 1;
						num = 8;
						continue;
					}
					case 29:
						goto IL_158;
					case 30:
						goto IL_34F;
					case 31:
						if (text[num4 - 1] == '"')
						{
							num = 24;
							continue;
						}
						goto IL_17B;
					case 32:
						num = 14;
						continue;
					}
					if (A_0 == null)
					{
						num = 23;
						continue;
					}
					num = 11;
					continue;
					IL_158:
					num = 17;
					continue;
					IL_FA:
					goto IL_158;
					IL_17B:
					text = text.Replace(RecordTableEnumerator.b("ḻ᰽", a_), RecordTableEnumerator.b("ḻ", a_));
					num = 4;
					continue;
					IL_34F:
					num4 = num2 - A_3;
					text = A_1.Substring(A_3, num4);
					num = 13;
					continue;
					IL_399:
					num = 22;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("主弽⸿╁⅃", a_));
				IL_244:
				goto IL_3FD;
				IL_2AE:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("伻嬽〿⍁㙃❅㱇╉㹋", a_));
				IL_3F8:
				throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿၁⭃ㅅᭇ㹉㹋❍㹏㕑", a_));
				IL_3FD:
				A_0.Value = text;
				A_0.Text = text;
				return Math.Min(length, num2 + length2 - 1);
			}
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0003AE04 File Offset: 0x00039E04
		internal new SizeF ᜀ(IXLSRange A_0, bool A_1, bool A_2)
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
			long a_ = sprṔ.ᜀ(A_0.Column, A_0.Row);
			return this.ᜀ(a_, A_1, A_2);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0003AE5C File Offset: 0x00039E5C
		internal new SizeF ᜀ(long A_0, bool A_1, bool A_2)
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
			RichTextString a_ = new RichTextString(base.AppImplementation, this, false, true);
			bool flag = false;
			return this.ᜀ(A_0, A_1, a_, A_2, out flag);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0003AEB4 File Offset: 0x00039EB4
		private new SizeF ᜀ(long A_0, bool A_1, RichTextString A_2, bool A_3, out bool A_4)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				bool flag;
				SizeF sizeF;
				spr\u192F spr_u192F;
				for (;;)
				{
					base.ParseData();
					this.ᜏ.FillRTFString(A_0, A_1, A_2);
					int num = sprṔ.ᜁ(A_0);
					int num2 = sprṔ.ᜀ(A_0);
					flag = false;
					string text = A_2.Text;
					int num3 = 31;
					for (;;)
					{
						int a_2;
						IXLSRange range;
						switch (num3)
						{
						case 0:
							sizeF.Height = this.ᜀ(sizeF, a_2, true);
							num3 = 40;
							continue;
						case 1:
						{
							spr\u25A6.ᜀ ᜀ;
							if (ᜀ.ᜇ() < num - 1)
							{
								num3 = 36;
								continue;
							}
							goto IL_2DC;
						}
						case 2:
							goto IL_256;
						case 3:
							num3 = 14;
							continue;
						case 4:
							if (this.\u1714 != null)
							{
								num3 = 32;
								continue;
							}
							goto IL_3DC;
						case 5:
							goto IL_2DC;
						case 6:
							num3 = 18;
							continue;
						case 7:
							num3 = 19;
							continue;
						case 8:
							if (text.Length == 0)
							{
								num3 = 26;
								continue;
							}
							num3 = 4;
							continue;
						case 9:
							if (range != null)
							{
								num3 = 3;
								continue;
							}
							goto IL_24A;
						case 10:
						{
							spr\u25A6.ᜀ ᜀ;
							if (ᜀ.ᜂ() > num - 1)
							{
								num3 = 25;
								continue;
							}
							goto IL_2DC;
						}
						case 11:
							num3 = 33;
							continue;
						case 12:
						{
							spr\u25A6.ᜀ ᜀ;
							if (ᜀ != null)
							{
								num3 = 21;
								continue;
							}
							goto IL_3DC;
						}
						case 13:
							sizeF = this.ᜀ(sizeF, spr_u192F, this.ᜏ, A_0);
							num3 = 17;
							continue;
						case 14:
							if (range.Row == num)
							{
								num3 = 16;
								continue;
							}
							goto IL_24A;
						case 15:
							num3 = 27;
							continue;
						case 16:
							num3 = 20;
							continue;
						case 17:
							goto IL_24A;
						case 18:
							if (spr_u192F.\u1733())
							{
								num3 = 22;
								continue;
							}
							goto IL_50D;
						case 19:
							if (num2 <= range.LastColumn)
							{
								num3 = 13;
								continue;
							}
							goto IL_24A;
						case 20:
							if (num2 >= range.Column)
							{
								num3 = 7;
								continue;
							}
							goto IL_24A;
						case 21:
							num3 = 35;
							continue;
						case 22:
							sizeF = this.ᜀ(this, A_2);
							num3 = 43;
							continue;
						case 23:
							if (!A_1)
							{
								if (true)
								{
								}
								num3 = 11;
								continue;
							}
							goto IL_3DC;
						case 24:
							if (!A_3)
							{
								num3 = 38;
								continue;
							}
							goto IL_21C;
						case 25:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_326;
							default:
								if (false)
								{
								}
								num3 = 1;
								continue;
							}
							break;
						case 26:
							goto IL_312;
						case 27:
						{
							spr\u25A6.ᜀ ᜀ;
							if (ᜀ.ᜃ() >= num2 - 1)
							{
								num3 = 5;
								continue;
							}
							goto IL_3DC;
						}
						case 28:
							goto IL_21C;
						case 29:
							num3 = 8;
							continue;
						case 30:
							if (A_1)
							{
								num3 = 34;
								continue;
							}
							sizeF = this.ᜀ(sizeF, spr_u192F);
							num3 = 24;
							continue;
						case 31:
							if (text != null)
							{
								num3 = 29;
								continue;
							}
							goto IL_4F9;
						case 32:
						{
							Rectangle a_3 = Rectangle.FromLTRB(num2 - 1, num - 1, num2 - 1, num - 1);
							spr\u25A6.ᜀ ᜀ = this.\u1714.ᜂ(a_3);
							num3 = 12;
							continue;
						}
						case 33:
						{
							spr\u25A6.ᜀ ᜀ;
							if (ᜀ.ᜅ() > num2 - 1)
							{
								num3 = 15;
								continue;
							}
							goto IL_2DC;
						}
						case 34:
							num3 = 39;
							continue;
						case 35:
							if (A_1)
							{
								goto IL_326;
							}
							goto IL_337;
						case 36:
							goto IL_337;
						case 37:
							goto IL_3DC;
						case 38:
							sizeF.Width = this.ᜀ(sizeF, a_2, false);
							num3 = 28;
							continue;
						case 39:
							if (!flag)
							{
								num3 = 6;
								continue;
							}
							goto IL_50D;
						case 40:
							goto IL_24A;
						case 41:
							num3 = 10;
							continue;
						case 42:
							if (!A_3)
							{
								num3 = 0;
								continue;
							}
							goto IL_24A;
						case 43:
							goto IL_50D;
						}
						break;
						IL_21C:
						range = this.ᜠ.Range;
						num3 = 9;
						continue;
						IL_24A:
						num3 = 2;
						continue;
						IL_2DC:
						flag = true;
						num3 = 37;
						continue;
						IL_326:
						num3 = 41;
						continue;
						IL_337:
						num3 = 23;
						continue;
						IL_3DC:
						(this.m_book.Styles[RecordTableEnumerator.b("礶嘸䤺值帾ⵀ", a_)].Font as FontWrapper).Wrapped;
						spr_u192F = this.ᜃ(A_0);
						a_2 = spr_u192F.\u171B();
						sizeF = A_2.StringSize;
						spr_u192F.ᜋ();
						num3 = 30;
						continue;
						IL_50D:
						num3 = 42;
					}
				}
				IL_256:
				A_4 = (flag && spr_u192F.\u1733());
				return sizeF;
				IL_312:
				IL_4F9:
				A_4 = false;
				return new SizeF(0f, 0f);
			}
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0003B428 File Offset: 0x0003A428
		private new Size ᜀ(IWorksheet A_0, IRichTextString A_1)
		{
			for (;;)
			{
				switch (0)
				{
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
					break;
				}
			}
			IL_24:
			if (false)
			{
			}
			Size empty;
			for (;;)
			{
				if (true)
				{
				}
				RichTextString richTextString = A_1 as RichTextString;
				string[] array = richTextString.Text.Split(new char[]
				{
					'\n'
				});
				int num = 0;
				empty = Size.Empty;
				int columnWidthPixels = A_0.GetColumnWidthPixels(1);
				string[] array2 = array;
				int num2 = 0;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_9D;
					case 1:
					{
						if (num2 >= array2.Length)
						{
							num3 = 3;
							continue;
						}
						string text = array2[num2];
						RichTextString richTextString2 = richTextString.Clone(richTextString.Parent) as RichTextString;
						string text2 = text.TrimEnd(new char[]
						{
							'\r'
						});
						richTextString2.ᜁ(num, text2.Length);
						Size size = this.ᜀ(text, columnWidthPixels, richTextString2);
						empty.Height += size.Height;
						empty.Width = Math.Max(size.Width, empty.Width);
						num += text.Length + 1;
						num2++;
						num3 = 0;
						continue;
					}
					case 2:
						goto IL_9D;
					case 3:
						return empty;
					}
					break;
					IL_9D:
					num3 = 1;
				}
			}
			return empty;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0003B59C File Offset: 0x0003A59C
		private new Size ᜀ(string A_0, int A_1, RichTextString A_2)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7C:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_30;
			}
			int num2;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					goto IL_62;
				case 2:
					if (num2 > A_1)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_86;
				}
				goto IL_30;
			}
			IL_62:
			SizeF sizeF = this.ᜀ(A_2, A_1);
			goto IL_7C;
			IL_84:
			IL_86:
			Size empty;
			empty.Height += (int)sizeF.Height;
			empty.Width = Math.Max((int)sizeF.Width, empty.Width);
			return empty;
			IL_30:
			empty = Size.Empty;
			sizeF = A_2.StringSize;
			num2 = (int)sizeF.Width;
			num = 2;
			goto IL_1E;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0003B664 File Offset: 0x0003A664
		private new Size ᜀ(RichTextString A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				Size empty;
				for (;;)
				{
					RichTextString richTextString = (RichTextString)A_0.Clone(A_0.Parent);
					int num = 0;
					int num2 = 0;
					int length = A_0.Text.Length;
					empty = Size.Empty;
					int num3 = 14;
					for (;;)
					{
						SizeF sizeF;
						switch (num3)
						{
						case 0:
						{
							RichTextString richTextString2;
							A_0 = richTextString2;
							int num4;
							num = num4;
							num3 = 5;
							continue;
						}
						case 1:
							goto IL_19A;
						case 2:
							goto IL_111;
						case 3:
						{
							SizeF stringSize;
							if (stringSize.Width < (float)A_1)
							{
								num3 = 7;
								continue;
							}
							goto IL_1BE;
						}
						case 4:
							goto IL_111;
						case 5:
							goto IL_20B;
						case 6:
						{
							SizeF stringSize;
							if (stringSize.Width > (float)A_1)
							{
								num3 = 0;
								continue;
							}
							goto IL_20B;
						}
						case 7:
							num3 = 15;
							continue;
						case 8:
							goto IL_1E6;
						case 9:
							goto IL_1BE;
						case 10:
						{
							if (num2 >= length)
							{
								num3 = 16;
								continue;
							}
							A_0 = this.ᜀ(richTextString, num, ref num);
							RichTextString richTextString2 = null;
							int num4 = -1;
							SizeF stringSize = A_0.StringSize;
							num3 = 12;
							continue;
						}
						case 11:
							if (A_0 != null)
							{
								num3 = 13;
								continue;
							}
							num = num2;
							sizeF = this.ᜀ(richTextString, num2, ref num, A_1);
							num3 = 4;
							continue;
						case 12:
							goto IL_1E6;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return empty;
							default:
								if (false)
								{
								}
								sizeF = A_0.StringSize;
								num3 = 2;
								continue;
							}
							break;
						case 14:
							goto IL_19A;
						case 15:
						{
							if (true)
							{
							}
							if (num >= richTextString.Text.Length)
							{
								num3 = 9;
								continue;
							}
							RichTextString richTextString2 = A_0;
							int num4 = num;
							A_0 = this.ᜀ(richTextString, num2, ref num);
							SizeF stringSize = A_0.StringSize;
							num3 = 8;
							continue;
						}
						case 16:
							return empty;
						}
						break;
						IL_111:
						empty.Width = Math.Max((int)sizeF.Width, empty.Width);
						empty.Height += (int)sizeF.Height;
						num2 = num;
						num3 = 1;
						continue;
						IL_19A:
						num3 = 10;
						continue;
						IL_1BE:
						num3 = 6;
						continue;
						IL_1E6:
						num3 = 3;
						continue;
						IL_20B:
						num3 = 11;
					}
				}
				return empty;
			}
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0003B8D8 File Offset: 0x0003A8D8
		private new SizeF ᜀ(RichTextString A_0, int A_1, ref int A_2, int A_3)
		{
			switch (0)
			{
			default:
			{
				Size size;
				for (;;)
				{
					int length = A_0.Text.Length;
					size = Size.Empty;
					Size size2 = Size.Empty;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_13D;
						case 1:
							goto IL_12D;
						case 2:
							goto IL_68;
						case 3:
						{
							if (size.Width >= A_3)
							{
								num = 1;
								continue;
							}
							RichTextString richTextString = (RichTextString)A_0.Clone(A_0.Parent);
							richTextString.ᜁ(A_1, A_2 - A_1 + 1);
							size2 = size;
							size = richTextString.StringSize.ToSize();
							num = 6;
							continue;
						}
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13D;
							default:
								if (false)
								{
								}
								goto IL_68;
							}
							break;
						case 5:
							if (A_2 < length)
							{
								num = 7;
								continue;
							}
							goto IL_147;
						case 6:
							if (size.Width > A_3)
							{
								num = 8;
								continue;
							}
							A_2++;
							num = 4;
							continue;
						case 7:
							num = 3;
							continue;
						case 8:
							size = size2;
							num = 0;
							continue;
						}
						break;
						IL_68:
						num = 5;
					}
				}
				IL_12D:
				goto IL_147;
				IL_13D:
				if (true)
				{
				}
				IL_147:
				return size;
			}
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0003BA34 File Offset: 0x0003AA34
		private new RichTextString ᜀ(RichTextString A_0, int A_1, ref int A_2)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_98:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_38;
			}
			int num2;
			for (;;)
			{
				IL_1E:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_A3;
				case 1:
					if (num2 < 0)
					{
						num = 2;
						continue;
					}
					goto IL_A5;
				case 2:
					goto IL_7E;
				}
				goto IL_38;
			}
			IL_7E:
			RichTextString richTextString;
			num2 = richTextString.Text.Length - 1;
			goto IL_98;
			IL_A3:
			IL_A5:
			richTextString.ᜁ(A_1, num2 - A_1 + 1);
			A_2 = num2 + 1;
			return richTextString;
			IL_38:
			richTextString = (RichTextString)A_0.Clone(A_0.Parent);
			num2 = richTextString.Text.IndexOfAny(new char[]
			{
				'-',
				' '
			}, A_2);
			num = 1;
			goto IL_1E;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0003BAF8 File Offset: 0x0003AAF8
		private new SizeF ᜀ(SizeF A_0, spr\u192F A_1)
		{
			int a_ = 11;
			int num = 9;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (A_1.\u171B() != 0)
					{
						num = 2;
						continue;
					}
					goto IL_13B;
				case 1:
					num = 8;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					if (flag)
					{
						num = 6;
						continue;
					}
					goto IL_13B;
				case 4:
					goto IL_A3;
				case 5:
					if (A_1.\u171A() == 0)
					{
						num = 4;
						continue;
					}
					goto IL_13B;
				case 6:
					num = 0;
					continue;
				case 7:
					goto IL_5E;
				case 8:
					flag2 = (A_1.ᜋ() != HorizontalAlignType.Right);
					goto IL_E0;
				case 10:
					flag2 = false;
					goto IL_E0;
				case 11:
					if (A_1.ᜋ() != HorizontalAlignType.Left)
					{
						num = 1;
						continue;
					}
					num = 10;
					continue;
				}
				IL_53:
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 11;
				continue;
				goto IL_53;
				IL_E0:
				flag = flag2;
				num = 3;
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
			IL_A3:
			if (true)
			{
			}
			return A_0;
			IL_13B:
			A_0.Width += (float)(A_1.\u171A() * 12);
			return A_0;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0003BC5C File Offset: 0x0003AC5C
		private new float ᜀ(SizeF A_0, int A_1, bool A_2)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					goto IL_156;
				case 3:
					if (A_1 == 180)
					{
						num = 2;
						continue;
					}
					num = 15;
					continue;
				case 4:
					goto IL_B3;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A3;
					default:
						if (false)
						{
						}
						A_1 -= 90;
						num = 4;
						continue;
					}
					break;
				case 6:
					goto IL_13E;
				case 7:
					A_1 = 90 - A_1;
					num = 8;
					continue;
				case 8:
					goto IL_F2;
				case 9:
					if (A_1 != 90)
					{
						num = 0;
						continue;
					}
					goto IL_156;
				case 10:
					if (true)
					{
					}
					num = 13;
					continue;
				case 11:
					if (!A_2)
					{
						num = 12;
						continue;
					}
					goto IL_D9;
				case 12:
					goto IL_172;
				case 13:
					if (!A_2)
					{
						num = 6;
						continue;
					}
					goto IL_AB;
				case 14:
					if (A_2)
					{
						num = 7;
						continue;
					}
					goto IL_197;
				case 15:
					if (A_1 > 90)
					{
						num = 5;
						continue;
					}
					goto IL_B3;
				}
				if (A_1 == 0)
				{
					num = 10;
					continue;
				}
				num = 9;
				continue;
				IL_B3:
				num = 14;
				continue;
				IL_156:
				num = 11;
			}
			IL_A3:
			return A_0.Height;
			IL_AB:
			return A_0.Height;
			IL_D9:
			return A_0.Width;
			IL_F2:
			goto IL_197;
			IL_13E:
			return A_0.Width;
			IL_172:
			goto IL_A3;
			IL_197:
			float num2 = (float)Math.Sin((double)(0.017453292f * (float)A_1)) * A_0.Height;
			float num3 = (float)Math.Cos((double)(0.017453292f * (float)A_1)) * A_0.Width;
			return num3 + num2;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0003BE34 File Offset: 0x0003AE34
		private new XlsFont ᜀ(spr\u23A5 A_0, out int A_1)
		{
			int a_ = 5;
			sprᢖ sprᢖ;
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
				sprᢖ = this.m_book.InnerExtFormats;
				if (sprᢖ.Count <= (int)A_0.ᜆ())
				{
					throw new ArgumentException(RecordTableEnumerator.b("堺堼匾ⵀՂ⩄㕆⑈⩊㥌", a_));
				}
				break;
			}
			spr\u192F spr_u192F = sprᢖ.ᜁ((int)A_0.ᜆ());
			A_1 = spr_u192F.\u171B();
			return (XlsFont)this.m_book.InnerFonts[spr_u192F.\u173B()];
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0003BED8 File Offset: 0x0003AED8
		protected override void CopyOptions(XlsWorksheetBase sourceSheet)
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
			base.CopyOptions(sourceSheet);
			XlsWorksheet xlsWorksheet = (XlsWorksheet)sourceSheet;
			this.RowColumnHeadersVisible = xlsWorksheet.RowColumnHeadersVisible;
			this.IsStringsPreserved = xlsWorksheet.IsStringsPreserved;
			this.GridLinesVisible = xlsWorksheet.GridLinesVisible;
			this.\u1717 = (spr\u2408)spr\u1CD3.ᜀ(xlsWorksheet.\u1717);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0003BF5C File Offset: 0x0003AF5C
		protected override void OnRealIndexChanged(int iOldIndex)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				num = 1;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					goto IL_6D;
				}
				if (this.\u1718 == null)
				{
					return;
				}
				num = 0;
			}
			IL_52:
			this.\u1718.ᜀ(base.RealIndex);
			goto IL_65;
			IL_6D:
			if (true)
			{
			}
			return;
			IL_20:
			if (false)
			{
			}
			num = 2;
			goto IL_30;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0003BFE0 File Offset: 0x0003AFE0
		private void ᜁ(int A_0, int A_1, bool A_2)
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
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0003C01C File Offset: 0x0003B01C
		private new SizeF ᜀ(SizeF A_0, spr\u192F A_1, XlsCellRecordCollection A_2, long A_3)
		{
			int a_ = 5;
			int num = 22;
			HorizontalAlignType horizontalAlignType;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_201;
				case 1:
					if (horizontalAlignType == HorizontalAlignType.Center)
					{
						num = 8;
						continue;
					}
					goto IL_289;
				case 2:
					num = 9;
					continue;
				case 3:
					goto IL_163;
				case 4:
					if (horizontalAlignType != HorizontalAlignType.Left)
					{
						num = 6;
						continue;
					}
					goto IL_125;
				case 5:
					if (horizontalAlignType != HorizontalAlignType.Fill)
					{
						num = 2;
						continue;
					}
					return A_0;
				case 6:
					num = 1;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_125;
				case 9:
					if (horizontalAlignType == HorizontalAlignType.Right)
					{
						num = 24;
						continue;
					}
					num = 21;
					continue;
				case 10:
					A_0.Width += 16f;
					num = 13;
					continue;
				case 11:
					num = 17;
					continue;
				case 12:
					goto IL_B4;
				case 13:
					goto IL_152;
				case 14:
					if (num2 < 90)
					{
						num = 10;
						continue;
					}
					goto IL_152;
				case 15:
					if (A_2 == null)
					{
						num = 0;
						continue;
					}
					horizontalAlignType = A_1.ᜋ();
					num2 = A_1.\u171B();
					goto IL_106;
				case 16:
					if (horizontalAlignType != HorizontalAlignType.CenterAcrossSelection)
					{
						num = 7;
						continue;
					}
					return A_0;
				case 17:
					if (horizontalAlignType == HorizontalAlignType.Distributed)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				case 18:
					return A_0;
				case 19:
					num = 14;
					continue;
				case 20:
					if (num2 > 0)
					{
						num = 19;
						continue;
					}
					goto IL_152;
				case 21:
					if (horizontalAlignType != HorizontalAlignType.Justify)
					{
						num = 11;
						continue;
					}
					goto IL_163;
				case 23:
					goto IL_130;
				case 24:
					goto IL_1E3;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_106:
					num = 16;
					continue;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						num = 12;
						continue;
					}
					num = 15;
					continue;
				}
				IL_125:
				num = 23;
				continue;
				IL_152:
				num = 18;
				continue;
				IL_163:
				num = 20;
			}
			IL_B4:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("崺刼䴾ⱀ≂ㅄ", a_));
			IL_130:
			A_0.Width += (float)((horizontalAlignType == HorizontalAlignType.Left) ? 16 : 32);
			return A_0;
			IL_1E3:
			return A_0;
			IL_201:
			throw new ArgumentNullException(RecordTableEnumerator.b("堺刼匾", a_));
			IL_289:
			return this.ᜀ(A_0, num2, A_2, A_3);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0003C2C0 File Offset: 0x0003B2C0
		private new SizeF ᜀ(SizeF A_0, int A_1, XlsCellRecordCollection A_2, long A_3)
		{
			int a_ = 16;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					goto IL_F1;
				case 3:
					num = 17;
					continue;
				case 4:
					num = 8;
					continue;
				case 5:
					if (!this.ᜏ.ContainFormulaBoolOrError(A_3))
					{
						num = 10;
						continue;
					}
					goto IL_220;
				case 6:
					if (A_1 >= 180)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return A_0;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 7:
					if (this.ᜏ.ContainBoolOrError(A_3))
					{
						num = 12;
						continue;
					}
					num = 11;
					continue;
				case 8:
					if (A_1 == 0)
					{
						num = 18;
						continue;
					}
					return A_0;
				case 9:
					goto IL_D3;
				case 10:
					num = 7;
					continue;
				case 11:
					if (A_1 > 0)
					{
						num = 3;
						continue;
					}
					goto IL_D3;
				case 12:
					goto IL_11B;
				case 13:
					if (!this.ᜏ.ContainNumber(A_3))
					{
						num = 4;
						continue;
					}
					return A_0;
				case 14:
					num = 13;
					continue;
				case 15:
					goto IL_19B;
				case 16:
					if (!this.ᜏ.ContainFormulaNumber(A_3))
					{
						num = 14;
						continue;
					}
					return A_0;
				case 17:
					if (A_1 >= 90)
					{
						num = 9;
						continue;
					}
					goto IL_168;
				case 18:
					A_0.Width += 16f;
					num = 15;
					continue;
				}
				if (A_2 == null)
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
				IL_D3:
				num = 6;
			}
			IL_73:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╅❇♉", a_));
			IL_F1:
			goto IL_168;
			IL_11B:
			goto IL_220;
			IL_168:
			A_0.Width += 16f;
			return A_0;
			IL_19B:
			return A_0;
			IL_220:
			A_0.Width += 32f;
			return A_0;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0003C504 File Offset: 0x0003B504
		private new void ᜃ()
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
			this.ᜬ = new spr\u24F1((spr\u2158)base.ReservedHandle, this);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0003C558 File Offset: 0x0003B558
		private new IStyle ᜀ(IDictionary A_0, int A_1)
		{
			int a_ = 8;
			int num = 5;
			spr\u2502 spr_u;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CA;
				case 1:
					goto IL_3C;
				case 2:
					goto IL_46;
				case 3:
					if (spr_u != null)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 4:
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					spr_u = (spr\u2502)A_0[A_1];
					num = 3;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("娽⤿⅁ୃ㍅㱇♉╋⁍㕏⅑", a_));
			IL_46:
			if (true)
			{
			}
			int num2 = this.m_book.DefaultXFIndex;
			goto IL_D2;
			IL_CA:
			num2 = (int)spr_u.ᜃ();
			IL_D2:
			int iXFIndex = num2;
			return new AddtionalFormatWrapper(this.m_book, iXFIndex);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0003C644 File Offset: 0x0003B644
		private new int ᜀ(int A_0, int A_1, IStyle A_2, IDictionary A_3, XlsWorksheet.ᜀ A_4, bool A_5)
		{
			int num;
			for (;;)
			{
				IL_28:
				base.ParseData();
				num = this.ᜀ(A_2);
				int num2 = A_0;
				for (;;)
				{
					IL_38:
					int num3 = 1;
					for (;;)
					{
						spr\u2502 spr_u;
						switch (num3)
						{
						case 0:
							if (true)
							{
							}
							if (!A_3.Contains(num2))
							{
								num3 = 2;
								continue;
							}
							num3 = 3;
							continue;
						case 1:
							goto IL_42;
						case 2:
							num3 = 5;
							continue;
						case 3:
							spr_u = (spr\u2502)A_3[num2];
							goto IL_E4;
						case 4:
							return num;
						case 5:
							spr_u = A_4(num2);
							goto IL_E4;
						case 6:
							goto IL_42;
						case 7:
							if (num2 <= A_1)
							{
								num3 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_38;
							default:
								if (false)
								{
								}
								num3 = 4;
								continue;
							}
							break;
						}
						goto IL_28;
						IL_42:
						num3 = 7;
						continue;
						IL_E4:
						spr\u2502 spr_u2 = spr_u;
						spr_u2.ᜁ((ushort)num);
						this.ᜀ(num2, (ushort)num);
						num2++;
						num3 = 6;
					}
				}
			}
			return num;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0003C75C File Offset: 0x0003B75C
		private new int ᜀ(int A_0, int A_1, IStyle A_2, IList A_3, XlsWorksheet.ᜀ A_4, bool A_5)
		{
			int num;
			for (;;)
			{
				IL_28:
				base.ParseData();
				num = this.ᜀ(A_2);
				int num2 = A_0;
				for (;;)
				{
					IL_38:
					int num3 = 7;
					for (;;)
					{
						spr\u2502 spr_u;
						switch (num3)
						{
						case 0:
							if (A_3[num2] == null)
							{
								num3 = 1;
								continue;
							}
							num3 = 2;
							continue;
						case 1:
							num3 = 3;
							continue;
						case 2:
							spr_u = (spr\u2502)A_3[num2];
							goto IL_CF;
						case 3:
							spr_u = A_4(num2);
							goto IL_CF;
						case 4:
							if (num2 <= A_1)
							{
								num3 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_38;
							default:
								if (false)
								{
								}
								num3 = 6;
								continue;
							}
							break;
						case 5:
							if (true)
							{
							}
							goto IL_42;
						case 6:
							return num;
						case 7:
							goto IL_42;
						}
						goto IL_28;
						IL_42:
						num3 = 4;
						continue;
						IL_CF:
						spr\u2502 spr_u2 = spr_u;
						spr_u2.ᜁ((ushort)num);
						num2++;
						num3 = 5;
					}
				}
			}
			return num;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0003C860 File Offset: 0x0003B860
		private new int ᜀ(IStyle A_0)
		{
			int a_ = 4;
			int num = 2;
			int extendedFormatIndex;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
					if (extendedFormatIndex == -2147483648)
					{
						num = 0;
						continue;
					}
					goto IL_B5;
				case 3:
					goto IL_46;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					extendedFormatIndex = ((IExtendIndex)A_0).ExtendedFormatIndex;
					num = 1;
				}
			}
			for (;;)
			{
				IL_9F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_5E;
				}
			}
			IL_5E:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("帹夻堽ℿ㝁⡃㉅ᭇ㹉㕋≍㕏", a_));
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("帹夻堽ℿ㝁⡃㉅ᭇ㹉㕋≍㕏", a_));
			IL_B5:
			spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ(extendedFormatIndex);
			spr_u192F = spr_u192F.ᜭ();
			return spr_u192F.ᜠ();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0003C944 File Offset: 0x0003B944
		private new spr\u2502 ᜄ(int A_0)
		{
			int a_ = 9;
			for (;;)
			{
				IL_21:
				base.ParseData();
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_57:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B1;
					case 1:
						goto IL_53;
					case 2:
						if (A_0 > this.m_book.MaxColumnCount)
						{
							num = 0;
							continue;
						}
						goto IL_B3;
					case 3:
						num = 2;
						continue;
					}
					goto IL_21;
				}
				IL_53:
				if (A_0 >= 1)
				{
					goto IL_57;
				}
				break;
			}
			IL_61:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾ɀⱂ⥄㉆⑈╊ьⅎ㕐㙒ⵔ", a_), RecordTableEnumerator.b("簾⹀⽂い⩆❈歊⑌ⅎ㕐㙒ⵔ睖じ⡚絜ぞᑠᝢ䕤ࡦཨ䭪Ὤ๮ὰᑲၴ奶", a_));
			IL_B1:
			goto IL_61;
			IL_B3:
			spr\u216E spr_u216E = spr\u175E.ᜀ(TBIFFRecord.ColumnInfo) as spr\u216E;
			spr_u216E.ᜄ((ushort)(A_0 - 1));
			spr_u216E.ᜀ((ushort)(A_0 - 1));
			spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
			sprᜑ.ᜁ(this, A_0);
			this.ᜐ[A_0] = spr_u216E;
			return spr_u216E;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0003CA48 File Offset: 0x0003BA48
		private new void ᜃ(int A_0, int A_1, InsertOptionsType A_2, bool A_3)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int a_;
					int num5;
					int num6;
					int num7;
					int a_2;
					switch (num)
					{
					case 0:
						num2 = A_0 + A_1;
						goto IL_B4;
					case 1:
						num = 18;
						continue;
					case 2:
						if (A_3)
						{
							num = 4;
							continue;
						}
						num = 12;
						continue;
					case 3:
						goto IL_221;
					case 4:
						num = 6;
						continue;
					case 6:
						if (A_2 != InsertOptionsType.FormatAsBefore)
						{
							num = 1;
							continue;
						}
						num = 10;
						continue;
					case 7:
						goto IL_10C;
					case 8:
						if (num3 >= num4)
						{
							num = 14;
							continue;
						}
						this.ᜁ(this.m_iFirstRow, a_, this.m_iLastRow, 1, this.m_iFirstRow, num3, this, false);
						num3++;
						num = 7;
						continue;
					case 9:
						goto IL_10C;
					case 10:
						num5 = A_0 - 1;
						goto IL_1FE;
					case 11:
						goto IL_221;
					case 12:
						if (A_2 != InsertOptionsType.FormatAsBefore)
						{
							num = 19;
							continue;
						}
						num = 15;
						continue;
					case 13:
						return;
					case 14:
						return;
					case 15:
						num2 = A_0 - 1;
						goto IL_B4;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							goto IL_1EF;
						}
						break;
					case 17:
						if (num6 >= num7)
						{
							num = 13;
							continue;
						}
						this.ᜁ(a_2, this.m_iFirstColumn, 1, this.m_iLastColumn, num6, this.m_iFirstColumn, this, false);
						num6++;
						num = 3;
						continue;
					case 18:
						num5 = A_0 + A_1;
						goto IL_1FE;
					case 19:
						num = 0;
						continue;
					}
					if (A_2 == InsertOptionsType.FormatDefault)
					{
						num = 16;
						continue;
					}
					num = 2;
					continue;
					IL_BD:
					num = 9;
					continue;
					IL_B4:
					a_ = num2;
					num3 = A_0;
					num4 = A_0 + A_1;
					goto IL_BD;
					IL_10C:
					num = 8;
					continue;
					IL_1FE:
					a_2 = num5;
					int iLastColumn = this.m_iLastColumn;
					int iFirstColumn = this.m_iFirstColumn;
					num6 = A_0;
					num7 = A_0 + A_1;
					num = 11;
					continue;
					IL_221:
					num = 17;
				}
				return;
				IL_1EF:
				if (true)
				{
				}
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0003CC9C File Offset: 0x0003BC9C
		private new void ᜂ(int A_0, int A_1, InsertOptionsType A_2, bool A_3)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = this.ᜂ(A_0, A_1, A_2);
					int num2 = 10;
					for (;;)
					{
						sprᱧ a_;
						int num6;
						long num5;
						spr\u216E a_2;
						long num7;
						spr\u171D spr_u171D;
						spr\u23A5 spr_u23A;
						long a_3;
						switch (num2)
						{
						case 0:
							goto IL_40A;
						case 1:
							goto IL_40A;
						case 2:
						{
							int num3;
							int num4;
							if (num3 >= num4)
							{
								num2 = 38;
								continue;
							}
							num2 = 4;
							continue;
						}
						case 3:
							goto IL_483;
						case 4:
							if (!A_3)
							{
								num2 = 9;
								continue;
							}
							num2 = 31;
							continue;
						case 5:
							a_ = sprᜑ.ᜀ(this, num - 1, false);
							num2 = 20;
							continue;
						case 6:
							num5 = sprṔ.ᜀ(num, num6);
							goto IL_281;
						case 7:
							goto IL_3D3;
						case 8:
							num2 = 11;
							continue;
						case 9:
							num2 = 12;
							continue;
						case 10:
							if (!A_3)
							{
								num2 = 22;
								continue;
							}
							num2 = 26;
							continue;
						case 11:
							if (A_3)
							{
								num2 = 5;
								continue;
							}
							a_2 = this.ᜐ[num];
							num2 = 28;
							continue;
						case 12:
						{
							int num3;
							num7 = sprṔ.ᜀ(num3, num6);
							goto IL_349;
						}
						case 13:
							return;
						case 14:
						{
							int num8;
							if (num8 > 0)
							{
								num2 = 35;
								continue;
							}
							goto IL_42C;
						}
						case 15:
						{
							int num8 = -1;
							int num9 = -1;
							num2 = 41;
							continue;
						}
						case 16:
						{
							int num3;
							spr_u171D.ᜆ((A_3 ? num6 : num3) - 1);
							spr_u171D.ᜁ(spr_u23A.ᜆ());
							this.ᜏ.ᜁ(a_3, spr_u171D);
							num3++;
							num2 = 7;
							continue;
						}
						case 17:
							goto IL_483;
						case 18:
						{
							int num3;
							spr_u171D.ᜇ((A_3 ? num3 : num6) - 1);
							num2 = 16;
							continue;
						}
						case 19:
							if (spr_u23A != null)
							{
								num2 = 34;
								continue;
							}
							goto IL_3F6;
						case 20:
							goto IL_1FA;
						case 21:
							num5 = sprṔ.ᜀ(num6, num);
							goto IL_281;
						case 22:
						{
							int num8 = this.m_iFirstRow;
							int num9 = this.m_iLastRow;
							num2 = 25;
							continue;
						}
						case 23:
							goto IL_185;
						case 24:
							if ((int)spr_u23A.ᜆ() != this.m_book.DefaultXFIndex)
							{
								num2 = 27;
								continue;
							}
							goto IL_3F6;
						case 25:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2B1;
							default:
								if (false)
								{
								}
								goto IL_185;
							}
							break;
						case 26:
						{
							if (this.m_iFirstColumn == 2147483647)
							{
								num2 = 15;
								continue;
							}
							int num8 = this.m_iFirstColumn;
							int num9 = this.m_iLastColumn;
							num2 = 23;
							continue;
						}
						case 27:
						{
							int num3 = A_0;
							int num4 = A_0 + A_1;
							num2 = 39;
							continue;
						}
						case 28:
							goto IL_1FA;
						case 29:
							goto IL_42C;
						case 30:
							num2 = 6;
							continue;
						case 31:
						{
							int num3;
							num7 = sprṔ.ᜀ(num6, num3);
							goto IL_349;
						}
						case 32:
							if (true)
							{
							}
							if (!A_3)
							{
								num2 = 30;
								continue;
							}
							num2 = 21;
							continue;
						case 33:
							if (A_3)
							{
								num2 = 40;
								continue;
							}
							return;
						case 34:
							goto IL_2B1;
						case 35:
						{
							int num8;
							num6 = num8;
							num2 = 1;
							continue;
						}
						case 36:
						{
							int num9;
							if (num6 > num9)
							{
								num2 = 29;
								continue;
							}
							num2 = 32;
							continue;
						}
						case 37:
						{
							int num10;
							int num11;
							if (num10 >= num11)
							{
								num2 = 13;
								continue;
							}
							this.ᜀ(a_, a_2, A_3, num, num10, A_2);
							num10++;
							num2 = 17;
							continue;
						}
						case 38:
							goto IL_3F6;
						case 39:
							goto IL_3D3;
						case 40:
						{
							int num10 = A_0;
							int num11 = A_0 + A_1;
							num2 = 3;
							continue;
						}
						case 41:
							goto IL_185;
						case 42:
							if (num != -1)
							{
								num2 = 8;
								continue;
							}
							goto IL_42C;
						}
						break;
						IL_185:
						a_ = null;
						a_2 = null;
						num2 = 42;
						continue;
						IL_1FA:
						num2 = 14;
						continue;
						IL_281:
						a_3 = num5;
						spr_u23A = this.ᜏ.ᜄ(a_3);
						num2 = 19;
						continue;
						IL_2B1:
						num2 = 24;
						continue;
						IL_349:
						a_3 = num7;
						spr_u171D = (spr\u171D)spr\u175E.ᜀ(TBIFFRecord.Blank);
						num2 = 18;
						continue;
						IL_3D3:
						num2 = 2;
						continue;
						IL_3F6:
						num6++;
						num2 = 0;
						continue;
						IL_40A:
						num2 = 36;
						continue;
						IL_42C:
						num2 = 33;
						continue;
						IL_483:
						num2 = 37;
					}
				}
				return;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0003D14C File Offset: 0x0003C14C
		private new void ᜀ(sprᱧ A_0, spr\u216E A_1, bool A_2, int A_3, int A_4, InsertOptionsType A_5)
		{
			switch (0)
			{
			default:
			{
				int num = 4;
				sprᱧ sprᱧ;
				spr\u216E spr_u216E3;
				sprᱧ sprᱧ2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u216E spr_u216E2;
						spr\u216E spr_u216E = spr_u216E2;
						ushort a_;
						spr_u216E2.ᜀ(a_ = (ushort)(A_4 - 1));
						spr_u216E.ᜄ(a_);
						num = 15;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_132;
						default:
						{
							if (false)
							{
							}
							if (A_2)
							{
								num = 5;
								continue;
							}
							if (true)
							{
							}
							spr\u216E spr_u216E2 = (spr\u216E)spr\u1CD3.ᜀ(A_1);
							this.ᜐ[A_4] = spr_u216E2;
							num = 19;
							continue;
						}
						}
						break;
					case 2:
						num = 1;
						continue;
					case 3:
						if (A_0 == null)
						{
							num = 6;
							continue;
						}
						goto IL_134;
					case 5:
						sprᱧ = sprᜑ.ᜀ(this, A_4 - 1, A_0 != null);
						num = 3;
						continue;
					case 6:
						num = 14;
						continue;
					case 7:
						goto IL_24A;
					case 8:
						goto IL_152;
					case 9:
						goto IL_1DF;
					case 10:
						if (A_2)
						{
							num = 20;
							continue;
						}
						spr_u216E3 = this.ᜐ[A_4];
						num = 17;
						continue;
					case 11:
						if (A_3 != -1)
						{
							num = 2;
							continue;
						}
						return;
					case 12:
						if (sprᱧ2 != null)
						{
							num = 13;
							continue;
						}
						return;
					case 13:
						goto IL_132;
					case 14:
						if (sprᱧ != null)
						{
							num = 7;
							continue;
						}
						goto IL_134;
					case 15:
						goto IL_176;
					case 16:
						if (A_0 != null)
						{
							num = 8;
							continue;
						}
						return;
					case 17:
						if (spr_u216E3 != null)
						{
							num = 9;
							continue;
						}
						return;
					case 18:
						num = 10;
						continue;
					case 19:
					{
						spr\u216E spr_u216E2;
						if (spr_u216E2 != null)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 20:
						sprᱧ2 = sprᜑ.ᜀ(this, A_4 - 1, false);
						num = 12;
						continue;
					}
					if (A_5 == InsertOptionsType.FormatDefault)
					{
						num = 18;
						continue;
					}
					num = 11;
					continue;
					IL_134:
					num = 16;
				}
				IL_132:
				sprᱧ2.ᜢ();
				sprᱧ2.ᜃ((ushort)base.AppImplementation.ᜅ());
				sprᱧ2.ᜀ((ushort)this.m_book.DefaultXFIndex);
				return;
				IL_152:
				sprᱧ.ᜀ(A_0);
				return;
				IL_176:
				return;
				IL_1DF:
				spr\u216E spr_u216E4 = spr_u216E3;
				ushort a_2;
				spr_u216E3.ᜀ(a_2 = (ushort)(A_4 - 1));
				spr_u216E4.ᜄ(a_2);
				spr_u216E3.ᜃ((ushort)this.m_book.DefaultXFIndex);
				spr_u216E3.\u170D();
				return;
				IL_24A:
				sprᱧ.ᜃ((ushort)base.AppImplementation.ᜅ());
				sprᱧ.ᜢ();
				return;
			}
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0003D41C File Offset: 0x0003C41C
		private new int ᜂ(int A_0, int A_1, InsertOptionsType A_2)
		{
			for (;;)
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
							goto IL_96;
						default:
							goto IL_71;
						}
						break;
					case 1:
						switch (A_2)
						{
						case InsertOptionsType.FormatAsBefore:
							A_0--;
							num = 2;
							continue;
						case InsertOptionsType.FormatAsAfter:
							A_0 += A_1;
							num = 0;
							continue;
						case InsertOptionsType.FormatDefault:
							goto IL_98;
						default:
							if (true)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 2:
						return A_0;
					case 3:
						return A_0;
					case 4:
						num = 5;
						continue;
					case 5:
						goto IL_96;
					}
					break;
					IL_98:
					A_0 = -1;
					num = 3;
					continue;
					IL_96:
					goto IL_98;
				}
			}
			IL_71:
			if (false)
			{
			}
			return A_0;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0003D4DC File Offset: 0x0003C4DC
		private new CellFormatType ᜀ(int A_0, int A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				int a_;
				for (;;)
				{
					spr\u23A5 spr_u23A;
					int num2;
					spr\u216E spr_u216E;
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
						goto IL_C4;
					case 2:
						if (spr_u23A == null)
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					case 4:
						goto IL_113;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DB;
						default:
							goto IL_AF;
						}
						break;
					case 6:
						num2 = (int)spr_u23A.ᜆ();
						goto IL_B7;
					case 7:
						num2 = this.m_book.DefaultXFIndex;
						goto IL_B7;
					case 8:
						spr_u216E = this.ᜐ[A_1];
						num = 9;
						continue;
					case 9:
						goto IL_DB;
					}
					if (true)
					{
					}
					if (A_2)
					{
						num = 8;
						continue;
					}
					spr_u23A = this.ᜏ.ᜄ(A_0, A_1);
					num = 2;
					continue;
					IL_B7:
					a_ = num2;
					num = 1;
					continue;
					IL_DB:
					if (spr_u216E == null)
					{
						num = 5;
					}
					else
					{
						a_ = (int)spr_u216E.ᜌ();
						num = 4;
					}
				}
				IL_AF:
				if (false)
				{
				}
				return CellFormatType.General;
				IL_C4:
				IL_113:
				spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ(a_);
				int a_2 = spr_u192F.ᝊ();
				sprᤅ sprᤅ = this.m_book.InnerFormats.ᜁ(a_2);
				return sprᤅ.ᜀ(1.0);
			}
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0003D648 File Offset: 0x0003C648
		private new CellExportType ᜀ(CellFormatType A_0, int A_1, int A_2, int A_3, ExportDataTableOptions A_4)
		{
			switch (0)
			{
			default:
			{
				CellExportType result;
				for (;;)
				{
					base.ParseData();
					spr\u23A5 spr_u23A = this.ᜏ.ᜄ(A_1, A_2);
					result = CellExportType.Text;
					int num = A_1 + A_3;
					bool a_ = (A_4 & ExportDataTableOptions.DefaultStyleColumnTypes) != ExportDataTableOptions.None;
					int num2 = 42;
					for (;;)
					{
						CellExportType cellExportType;
						CellExportType cellExportType2;
						CellExportType cellExportType3;
						switch (num2)
						{
						case 0:
							if (spr_u23A != null)
							{
								num2 = 15;
								continue;
							}
							goto IL_27C;
						case 1:
							num2 = 47;
							continue;
						case 2:
							num2 = 45;
							continue;
						case 3:
							num2 = 36;
							continue;
						case 4:
							if (A_0 != CellFormatType.DateTime)
							{
								num2 = 2;
								continue;
							}
							num2 = 16;
							continue;
						case 5:
							num2 = 39;
							continue;
						case 6:
							goto IL_4C0;
						case 7:
							goto IL_27C;
						case 8:
							cellExportType = CellExportType.Error;
							goto IL_46E;
						case 9:
							if (spr_u23A != null)
							{
								num2 = 30;
								continue;
							}
							goto IL_4C0;
						case 10:
							goto IL_4A1;
						case 11:
						{
							spr\u249B spr_u249B;
							if (!spr_u249B.ᜂ())
							{
								num2 = 22;
								continue;
							}
							num2 = 8;
							continue;
						}
						case 12:
						{
							TBIFFRecord typeCode;
							switch (typeCode)
							{
							case TBIFFRecord.Number:
								goto IL_44A;
							case TBIFFRecord.Label:
								goto IL_4A1;
							case TBIFFRecord.BoolErr:
								num2 = 37;
								continue;
							default:
								num2 = 18;
								continue;
							}
							break;
						}
						case 13:
							num2 = 10;
							continue;
						case 14:
							IL_330:
							if (spr_u23A.get_TypeCode() != TBIFFRecord.Blank)
							{
								num2 = 7;
								continue;
							}
							goto IL_2F5;
						case 15:
							num2 = 14;
							continue;
						case 16:
							cellExportType2 = CellExportType.DateTime;
							goto IL_4B1;
						case 17:
							cellExportType3 = CellExportType.DateTime;
							goto IL_2E3;
						case 18:
							if (true)
							{
							}
							num2 = 29;
							continue;
						case 19:
							if (A_0 != CellFormatType.DateTime)
							{
								num2 = 1;
								continue;
							}
							num2 = 17;
							continue;
						case 20:
							goto IL_4C0;
						case 21:
							num2 = 27;
							continue;
						case 22:
							num2 = 24;
							continue;
						case 23:
							goto IL_4C0;
						case 24:
							cellExportType = CellExportType.Bool;
							goto IL_46E;
						case 25:
							num2 = 38;
							continue;
						case 26:
						{
							spr\u249B spr_u249B = (spr\u249B)spr_u23A;
							num2 = 11;
							continue;
						}
						case 27:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.LabelSST)
							{
								num2 = 13;
								continue;
							}
							result = CellExportType.Text;
							num2 = 20;
							continue;
						}
						case 28:
							num2 = 19;
							continue;
						case 29:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.RK)
							{
								num2 = 3;
								continue;
							}
							goto IL_44A;
						}
						case 30:
						{
							TBIFFRecord typeCode = spr_u23A.get_TypeCode();
							num2 = 43;
							continue;
						}
						case 31:
							num2 = 44;
							continue;
						case 32:
							goto IL_4C0;
						case 33:
							goto IL_4C0;
						case 34:
							num2 = 0;
							continue;
						case 35:
						{
							bool flag;
							if (flag)
							{
								num2 = 25;
								continue;
							}
							result = CellExportType.Formula;
							num2 = 6;
							continue;
						}
						case 36:
							goto IL_4A1;
						case 37:
							if (A_0 != CellFormatType.Text)
							{
								num2 = 26;
								continue;
							}
							goto IL_4C0;
						case 38:
							if (A_0 != CellFormatType.Text)
							{
								num2 = 28;
								continue;
							}
							goto IL_4C0;
						case 39:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.Formula)
							{
								num2 = 21;
								continue;
							}
							bool flag = (A_4 & ExportDataTableOptions.ComputedFormulaValues) != ExportDataTableOptions.None;
							num2 = 35;
							continue;
						}
						case 40:
							if (A_1 <= num)
							{
								num2 = 31;
								continue;
							}
							goto IL_27C;
						case 41:
							goto IL_47D;
						case 42:
							goto IL_47D;
						case 43:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.LabelSST)
							{
								num2 = 5;
								continue;
							}
							num2 = 12;
							continue;
						}
						case 44:
							if (spr_u23A != null)
							{
								num2 = 34;
								continue;
							}
							goto IL_2F5;
						case 45:
							cellExportType2 = CellExportType.Number;
							goto IL_4B1;
						case 46:
							goto IL_4C0;
						case 47:
							cellExportType3 = CellExportType.Number;
							goto IL_2E3;
						}
						break;
						IL_27C:
						num2 = 9;
						continue;
						IL_2E3:
						result = cellExportType3;
						num2 = 33;
						continue;
						IL_2F5:
						A_1++;
						A_0 = this.ᜀ(A_1, A_2, a_);
						spr_u23A = this.ᜏ.ᜄ(A_1, A_2);
						num2 = 41;
						continue;
						IL_4C0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_330;
						default:
							goto IL_4D6;
						}
						IL_44A:
						num2 = 4;
						continue;
						IL_46E:
						result = cellExportType;
						num2 = 46;
						continue;
						IL_47D:
						num2 = 40;
						continue;
						IL_4A1:
						result = CellExportType.Text;
						num2 = 32;
						continue;
						IL_4B1:
						result = cellExportType2;
						num2 = 23;
					}
				}
				IL_4D6:
				if (false)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0003DB34 File Offset: 0x0003CB34
		private new Type ᜀ(CellExportType A_0, bool A_1)
		{
			int a_ = 1;
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (A_0)
						{
						case CellExportType.Bool:
							goto IL_63;
						case CellExportType.Number:
							goto IL_C8;
						case CellExportType.Text:
						case CellExportType.Error:
						case CellExportType.Formula:
							goto IL_B0;
						case CellExportType.DateTime:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BB;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case CellExportType.TimeSpan:
							goto IL_DE;
						default:
							if (true)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 1:
						if (!A_1)
						{
							num = 2;
							continue;
						}
						goto IL_6E;
					case 2:
						goto IL_AE;
					case 3:
						goto IL_C6;
					case 4:
						goto IL_BB;
					}
					break;
					IL_BB:
					num = 3;
				}
			}
			IL_63:
			return typeof(bool);
			IL_6E:
			return typeof(double);
			IL_AE:
			return typeof(DateTime);
			IL_B0:
			return typeof(string);
			IL_C6:
			goto IL_DE;
			IL_C8:
			return typeof(double);
			IL_DE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("戶圸债匼倾㙀ⵂ敄㍆え㭊⡌", a_));
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0003DC3C File Offset: 0x0003CC3C
		private new object ᜀ(int A_0, int A_1, CellExportType A_2, bool A_3, bool A_4)
		{
			switch (0)
			{
			default:
			{
				object result;
				for (;;)
				{
					base.ParseData();
					spr\u23A5 spr_u23A = this.ᜏ.ᜄ(A_0, A_1);
					int num = 17;
					for (;;)
					{
						bool flag;
						double num3;
						double num4;
						double num5;
						switch (num)
						{
						case 0:
						{
							spr\u249B spr_u249B;
							flag = (spr_u249B.ᜄ() != 0);
							goto IL_3C0;
						}
						case 1:
						{
							spr\u249B spr_u249B;
							if (spr_u249B.ᜂ())
							{
								num = 8;
								continue;
							}
							num = 0;
							continue;
						}
						case 2:
						{
							spr᱒ spr᱒;
							byte a_ = spr᱒.ᜏ();
							result = this.ᜀ(a_, A_0);
							num = 16;
							continue;
						}
						case 3:
							if (A_3)
							{
								num = 53;
								continue;
							}
							goto IL_500;
						case 4:
							if (A_2 != CellExportType.Text)
							{
								num = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_57E;
							default:
								if (false)
								{
								}
								num = 35;
								continue;
							}
							break;
						case 5:
							switch (A_2)
							{
							case CellExportType.Bool:
							{
								spr\u249B spr_u249B = spr_u23A as spr\u249B;
								num = 41;
								continue;
							}
							case CellExportType.Number:
							{
								spr\u2230 spr_u = spr_u23A as spr\u2230;
								num = 58;
								continue;
							}
							case CellExportType.Text:
							case CellExportType.TimeSpan:
								goto IL_4E6;
							case CellExportType.DateTime:
							{
								spr\u2230 spr_u = spr_u23A as spr\u2230;
								num = 47;
								continue;
							}
							case CellExportType.Error:
								result = this.GetError(A_0, A_1);
								num = 46;
								continue;
							default:
								num = 52;
								continue;
							}
							break;
						case 6:
							if (A_3)
							{
								num = 14;
								continue;
							}
							goto IL_618;
						case 7:
						{
							spr᱒ spr᱒;
							if (spr᱒.ᜄ())
							{
								num = 2;
								continue;
							}
							goto IL_500;
						}
						case 8:
							goto IL_360;
						case 9:
						{
							string formulaStringValue;
							result = formulaStringValue;
							num = 27;
							continue;
						}
						case 10:
						{
							if (((spr᱒)spr_u23A).ᜋ())
							{
								num = 57;
								continue;
							}
							double num2;
							result = num2;
							num = 24;
							continue;
						}
						case 11:
							flag = false;
							goto IL_3C0;
						case 12:
							goto IL_554;
						case 13:
							goto IL_223;
						case 14:
							num = 37;
							continue;
						case 15:
							goto IL_239;
						case 16:
							goto IL_41E;
						case 17:
							if (spr_u23A != null)
							{
								num = 51;
								continue;
							}
							goto IL_36F;
						case 18:
							if (true)
							{
							}
							num = 21;
							continue;
						case 19:
							goto IL_3BB;
						case 20:
						{
							double num2;
							DateTime dateTime = UtilityMethods.ᜀ(num2);
							num = 25;
							continue;
						}
						case 21:
						{
							if (spr_u23A.get_TypeCode() != TBIFFRecord.Formula)
							{
								num = 13;
								continue;
							}
							double num2 = ((spr᱒)spr_u23A).ᜌ();
							string formulaStringValue = this.GetFormulaStringValue(A_0, A_1);
							spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ((int)spr_u23A.ᜆ());
							sprᤅ sprᤅ = spr_u192F.ᝁ() as sprᤅ;
							num = 49;
							continue;
						}
						case 22:
							num3 = double.NaN;
							goto IL_6A5;
						case 23:
							goto IL_4FB;
						case 24:
							goto IL_29D;
						case 25:
						{
							if (!A_4)
							{
								num = 42;
								continue;
							}
							DateTime dateTime;
							result = dateTime.ToOADate();
							num = 60;
							continue;
						}
						case 26:
						{
							double num2;
							sprᤅ sprᤅ;
							if (sprᤅ.ᜀ(num2) == CellFormatType.DateTime)
							{
								num = 28;
								continue;
							}
							num = 10;
							continue;
						}
						case 27:
							goto IL_1B3;
						case 28:
							num = 30;
							continue;
						case 29:
						{
							spr᱒ spr᱒ = spr_u23A as spr᱒;
							num = 6;
							continue;
						}
						case 30:
						{
							double num2;
							if (!double.IsNaN(num2))
							{
								num = 20;
								continue;
							}
							result = this.GetFormulaBoolValue(A_0, A_1);
							num = 54;
							continue;
						}
						case 31:
							num = 22;
							continue;
						case 32:
							if (A_4)
							{
								num = 62;
								continue;
							}
							result = UtilityMethods.ᜀ(num4);
							num = 38;
							continue;
						case 33:
							num = 1;
							continue;
						case 34:
						{
							if (spr_u23A.get_TypeCode() == TBIFFRecord.Blank)
							{
								num = 12;
								continue;
							}
							spr᱒ spr᱒ = null;
							num = 61;
							continue;
						}
						case 35:
							num = 40;
							continue;
						case 36:
							num5 = double.NaN;
							goto IL_266;
						case 37:
						{
							spr᱒ spr᱒;
							if (spr᱒.ᜇ())
							{
								num = 39;
								continue;
							}
							goto IL_618;
						}
						case 38:
							goto IL_21E;
						case 39:
							goto IL_57C;
						case 40:
							if (A_3)
							{
								num = 18;
								continue;
							}
							goto IL_223;
						case 41:
						{
							spr\u249B spr_u249B;
							if (spr_u249B != null)
							{
								num = 33;
								continue;
							}
							goto IL_360;
						}
						case 42:
						{
							DateTime dateTime;
							result = dateTime;
							num = 59;
							continue;
						}
						case 43:
							return result;
						case 44:
							goto IL_3D2;
						case 45:
						{
							spr\u2230 spr_u;
							num5 = spr_u.ᜀ();
							goto IL_266;
						}
						case 46:
							return result;
						case 47:
						{
							spr\u2230 spr_u;
							if (spr_u == null)
							{
								num = 56;
								continue;
							}
							num = 45;
							continue;
						}
						case 48:
							goto IL_4E6;
						case 49:
						{
							string formulaStringValue;
							if (formulaStringValue != null)
							{
								num = 9;
								continue;
							}
							num = 3;
							continue;
						}
						case 50:
						{
							spr\u2230 spr_u;
							num3 = spr_u.ᜀ();
							goto IL_6A5;
						}
						case 51:
							num = 34;
							continue;
						case 52:
							num = 48;
							continue;
						case 53:
							num = 7;
							continue;
						case 54:
							goto IL_200;
						case 55:
							goto IL_462;
						case 56:
							num = 36;
							continue;
						case 57:
							result = this.GetFormulaBoolValue(A_0, A_1);
							num = 19;
							continue;
						case 58:
						{
							spr\u2230 spr_u;
							if (spr_u == null)
							{
								num = 31;
								continue;
							}
							num = 50;
							continue;
						}
						case 59:
							return result;
						case 60:
							return result;
						case 61:
							if (spr_u23A.get_TypeCode() == TBIFFRecord.Formula)
							{
								num = 29;
								continue;
							}
							goto IL_618;
						case 62:
							result = num4;
							num = 55;
							continue;
						}
						break;
						IL_223:
						result = this.ᜀ(spr_u23A, A_4);
						num = 15;
						continue;
						IL_266:
						num4 = num5;
						num = 32;
						continue;
						IL_360:
						num = 11;
						continue;
						IL_3C0:
						result = flag;
						num = 44;
						continue;
						IL_4E6:
						result = this.GetText(A_0, A_1);
						num = 23;
						continue;
						IL_500:
						num = 26;
						continue;
						IL_618:
						result = null;
						num = 4;
						continue;
						IL_6A5:
						result = num3;
						num = 43;
					}
				}
				IL_1B3:
				IL_200:
				IL_21E:
				IL_239:
				IL_29D:
				return result;
				IL_36F:
				return DBNull.Value;
				IL_3BB:
				IL_3D2:
				IL_41E:
				IL_462:
				IL_4FB:
				return result;
				IL_554:
				goto IL_36F;
				IL_57C:
				IL_57E:
				return DBNull.Value;
			}
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0003E358 File Offset: 0x0003D358
		internal new string ᜀ(spr\u23A5 A_0, bool A_1)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 7;
				object obj2;
				for (;;)
				{
					object obj;
					sprᤅ sprᤅ;
					double num2;
					TBIFFRecord typeCode;
					object obj3;
					object obj4;
					switch (num)
					{
					case 0:
						goto IL_19F;
					case 1:
						goto IL_51B;
					case 2:
					{
						spr\u223A spr_u223A;
						obj = spr_u223A.ᜏ();
						goto IL_181;
					}
					case 3:
						if (sprᤅ.ᜇ() == CellFormatType.DateTime)
						{
							num = 29;
							continue;
						}
						obj2 = num2;
						num = 24;
						continue;
					case 4:
						num = 5;
						continue;
					case 5:
					{
						if (typeCode != TBIFFRecord.LabelSST)
						{
							num = 11;
							continue;
						}
						spr\u1C7C spr_u1C7C = (spr\u1C7C)A_0;
						object sstcontentByIndex = this.m_book.InnerSST.GetSSTContentByIndex(spr_u1C7C.ᜁ());
						spr\u223A spr_u223A = sstcontentByIndex as spr\u223A;
						if (true)
						{
						}
						num = 36;
						continue;
					}
					case 6:
						num = 0;
						continue;
					case 8:
						switch (typeCode)
						{
						case TBIFFRecord.Blank:
							obj2 = string.Empty;
							num = 30;
							continue;
						case (TBIFFRecord)514:
						case (TBIFFRecord)518:
							goto IL_1EF;
						case TBIFFRecord.Number:
							goto IL_373;
						case TBIFFRecord.Label:
						{
							spr\u2170 spr_u = (spr\u2170)A_0;
							obj2 = spr_u.ᜁ();
							num = 34;
							continue;
						}
						case TBIFFRecord.BoolErr:
						{
							spr\u249B spr_u249B = (spr\u249B)A_0;
							int key = (int)spr_u249B.ᜄ();
							num = 27;
							continue;
						}
						case TBIFFRecord.String:
						{
							spr\u21DF spr_u21DF = (spr\u21DF)A_0;
							obj2 = spr_u21DF.ᜁ();
							num = 31;
							continue;
						}
						default:
							num = 14;
							continue;
						}
						break;
					case 9:
					{
						Ptg[] array;
						if (this.ᜀ(array))
						{
							num = 38;
							continue;
						}
						obj2 = this.ᜀ(A_0.ᜄ(), A_0.ᜅ(), array, false, this.m_book.FormulaUtil, false);
						num = 23;
						continue;
					}
					case 10:
					{
						int key;
						obj3 = FormulaUtil.ErrorCodeToName[key];
						goto IL_3DF;
					}
					case 11:
						num = 26;
						continue;
					case 12:
						goto IL_17C;
					case 13:
					{
						spr\u249B spr_u249B;
						bool flag = spr_u249B.ᜄ() != 0;
						num = 15;
						continue;
					}
					case 14:
						num = 18;
						continue;
					case 15:
					{
						bool flag;
						obj3 = flag.ToString().ToUpper();
						goto IL_3DF;
					}
					case 16:
						num = 37;
						continue;
					case 17:
						num = 25;
						continue;
					case 18:
						if (typeCode != TBIFFRecord.RK)
						{
							num = 6;
							continue;
						}
						goto IL_373;
					case 19:
					{
						if (!A_1)
						{
							goto IL_274;
						}
						DateTime dateTime = DateTime.FromOADate(num2);
						num = 32;
						continue;
					}
					case 20:
						num = 35;
						continue;
					case 21:
						goto IL_3EC;
					case 22:
						goto IL_18E;
					case 23:
						goto IL_509;
					case 24:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_274;
						default:
							goto IL_368;
						}
						break;
					case 25:
					{
						if (typeCode != TBIFFRecord.Formula)
						{
							num = 4;
							continue;
						}
						spr᱒ spr᱒ = (spr᱒)A_0;
						Ptg[] array = spr᱒.ᜑ();
						num = 9;
						continue;
					}
					case 26:
						goto IL_110;
					case 27:
					{
						spr\u249B spr_u249B;
						if (!spr_u249B.ᜂ())
						{
							num = 13;
							continue;
						}
						num = 10;
						continue;
					}
					case 28:
						goto IL_DA;
					case 29:
						num = 19;
						continue;
					case 30:
						goto IL_30E;
					case 31:
						goto IL_2A1;
					case 32:
					{
						DateTime dateTime;
						obj4 = dateTime.ToOADate();
						goto IL_50E;
					}
					case 33:
						if (typeCode <= TBIFFRecord.LabelSST)
						{
							num = 17;
							continue;
						}
						num = 8;
						continue;
					case 34:
						goto IL_480;
					case 35:
						obj4 = UtilityMethods.ᜀ(num2);
						goto IL_50E;
					case 36:
					{
						spr\u223A spr_u223A;
						if (spr_u223A == null)
						{
							num = 16;
							continue;
						}
						num = 2;
						continue;
					}
					case 37:
					{
						object sstcontentByIndex;
						obj = sstcontentByIndex;
						goto IL_181;
					}
					case 38:
					{
						spr᱒ spr᱒;
						obj2 = this.ᜀ(spr᱒);
						num = 12;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 28;
						continue;
					}
					typeCode = A_0.get_TypeCode();
					num = 33;
					continue;
					IL_181:
					obj2 = obj;
					num = 22;
					continue;
					IL_274:
					num = 20;
					continue;
					IL_373:
					num2 = ((spr\u2230)A_0).ᜀ();
					int a_2 = (int)A_0.ᜆ();
					int a_3 = this.m_book.InnerExtFormats.ᜁ(a_2).ᝊ();
					sprᤅ = this.m_book.InnerFormats.ᜁ(a_3);
					num = 3;
					continue;
					IL_3DF:
					obj2 = obj3;
					num = 21;
					continue;
					IL_50E:
					obj2 = obj4;
					num = 1;
				}
				IL_DA:
				return string.Empty;
				IL_110:
				goto IL_1EF;
				IL_17C:
				IL_18E:
				goto IL_51D;
				IL_19F:
				IL_1EF:
				throw new ArgumentException(RecordTableEnumerator.b("ɀ≂⭄⥆♈㽊浌㵎㑐げ㩔ざ㝘㉚❜㩞䅠bd୦ը䭪ᥬ᙮Űᙲ孴", a_));
				IL_2A1:
				IL_30E:
				goto IL_51D;
				IL_368:
				if (false)
				{
				}
				IL_3EC:
				IL_480:
				IL_509:
				IL_51B:
				IL_51D:
				return obj2.ToString();
			}
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0003E888 File Offset: 0x0003D888
		private new void ᜀ(ICollection A_0, IDictionary A_1)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.GetEnumerator();
				try
				{
					int num = 5;
					for (;;)
					{
						spr\u2502 spr_u;
						switch (num)
						{
						case 0:
						{
							int num2 = (int)A_1[num2];
							spr_u.ᜁ((ushort)num2);
							num = 4;
							continue;
						}
						case 1:
							goto IL_117;
						case 2:
							num = 1;
							continue;
						case 3:
							if (spr_u != null)
							{
								num = 7;
								continue;
							}
							break;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_59;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 6:
						{
							int num2;
							if (A_1.Contains(num2))
							{
								num = 0;
								continue;
							}
							break;
						}
						case 7:
						{
							int num2 = (int)spr_u.ᜃ();
							num = 6;
							continue;
						}
						case 8:
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							goto IL_59;
						}
						goto IL_57;
						IL_59:
						spr_u = (spr\u2502)enumerator.Current;
						num = 3;
						continue;
						IL_7C:
						num = 8;
						continue;
						IL_57:
						goto IL_7C;
					}
					IL_117:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_165;
							case 2:
								goto IL_15B;
							}
							break;
						}
					}
					IL_15B:
					if (true)
					{
					}
					IL_165:;
				}
				return;
			}
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0003EA0C File Offset: 0x0003DA0C
		private new CellRange[] ᜀ(List<long> A_0)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 1;
				CellRange[] array;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_DC;
					case 2:
						goto IL_D0;
					case 3:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						long a_ = A_0[num2];
						int row = sprṔ.ᜁ(a_);
						int column = sprṔ.ᜀ(a_);
						array[num2] = (this[row, column] as CellRange);
						num2++;
						num = 0;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_111;
						}
						break;
					case 5:
					{
						if (A_0.Count == 0)
						{
							num = 2;
							continue;
						}
						int count = A_0.Count;
						array = new CellRange[count];
						int num2 = 0;
						num = 6;
						continue;
					}
					case 6:
						goto IL_DC;
					case 7:
						num = 5;
						continue;
					}
					if (A_0 != null)
					{
						num = 7;
						continue;
					}
					break;
					IL_DC:
					num = 3;
				}
				IL_D0:
				goto IL_119;
				IL_111:
				if (false)
				{
				}
				return array;
				IL_119:
				return null;
			}
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0003EB38 File Offset: 0x0003DB38
		private new IXLSRange ᜀ(BiffRecordRaw A_0, double A_1, bool A_2, bool A_3)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 13;
				spr\u23A5 spr_u23A;
				for (;;)
				{
					double num2;
					switch (num)
					{
					case 0:
						if (A_0 is sprỔ)
						{
							num = 7;
							continue;
						}
						goto IL_19E;
					case 1:
						goto IL_19E;
					case 2:
					{
						spr\u19FF spr_u19FF = (spr\u19FF)A_0;
						num2 = spr_u19FF.ᜅ();
						num = 14;
						continue;
					}
					case 3:
						goto IL_104;
					case 4:
						if (A_0 is spr\u19FF)
						{
							num = 2;
							continue;
						}
						goto IL_14D;
					case 5:
						if (A_0 is spr᱒)
						{
							num = 6;
							continue;
						}
						goto IL_F8;
					case 6:
					{
						spr᱒ spr᱒ = (spr᱒)A_0;
						num2 = spr᱒.ᜌ();
						num = 11;
						continue;
					}
					case 7:
					{
						sprỔ sprỔ = (sprỔ)A_0;
						num2 = sprỔ.ᜀ();
						num = 1;
						continue;
					}
					case 8:
						goto IL_79;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_104;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 10:
						goto IL_117;
					case 11:
						goto IL_F8;
					case 12:
						num = 4;
						continue;
					case 14:
						goto IL_14D;
					case 15:
						if (A_2)
						{
							num = 12;
							continue;
						}
						goto IL_19E;
					case 16:
						if (A_3)
						{
							num = 9;
							continue;
						}
						goto IL_F8;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					num2 = double.MinValue;
					spr_u23A = (spr\u23A5)A_0;
					num = 15;
					continue;
					IL_F8:
					num = 3;
					continue;
					IL_104:
					if (num2 != A_1)
					{
						num = 10;
						continue;
					}
					goto IL_1FD;
					IL_14D:
					num = 0;
					continue;
					IL_19E:
					num = 16;
				}
				IL_79:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌", a_));
				IL_117:
				return null;
				IL_1FD:
				return this.AllocatedRange[spr_u23A.ᜄ() + 1, spr_u23A.ᜅ() + 1];
			}
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0003ED60 File Offset: 0x0003DD60
		private new IXLSRange ᜀ(spr\u249B A_0, byte A_1, bool A_2)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 == A_0.ᜂ())
					{
						num = 3;
						continue;
					}
					goto IL_D2;
				case 1:
					goto IL_5F;
				case 3:
					num = 4;
					continue;
				case 4:
					if (A_0.ᜄ() == A_1)
					{
						num = 1;
						continue;
					}
					goto IL_D2;
				case 5:
					goto IL_44;
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 5;
				}
				else
				{
					num = 0;
				}
			}
			for (;;)
			{
				IL_5F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_B0;
				}
			}
			IL_B0:
			if (false)
			{
			}
			return this.AllocatedRange[A_0.\u1714() + 1, A_0.\u1713() + 1];
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("♃⥅❇♉ो㱍≏㵑♓", a_));
			IL_D2:
			return null;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0003EE40 File Offset: 0x0003DE40
		protected internal IXLSRange InnerGetCell(int column, int row)
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
			return this.InnerGetCell(column, row, this.ᜅ(row, column));
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0003EE8C File Offset: 0x0003DE8C
		protected internal IXLSRange InnerGetCell(int column, int row, int iXFIndex)
		{
			IXLSRange ixlsrange;
			for (;;)
			{
				base.ParseData();
				ixlsrange = this.ᜏ.GetRange(row, column);
				int num = 4;
				for (;;)
				{
					XlsRange xlsRange;
					switch (num)
					{
					case 0:
					{
						BiffRecordRaw biffRecordRaw = this.ᜏ.ᜄ(row, column) as BiffRecordRaw;
						num = 8;
						continue;
					}
					case 1:
						return ixlsrange;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_123;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							xlsRange = base.AppImplementation.ᜀ(this, column, row, column, row);
							num = 6;
							continue;
						}
						break;
					case 3:
						xlsRange.ExtendedFormatIndex = (ushort)iXFIndex;
						num = 5;
						continue;
					case 4:
						if (ixlsrange == null)
						{
							num = 0;
							continue;
						}
						return ixlsrange;
					case 5:
						goto IL_58;
					case 6:
						if ((int)xlsRange.ExtendedFormatIndex != iXFIndex)
						{
							goto IL_123;
						}
						goto IL_58;
					case 7:
						return ixlsrange;
					case 8:
					{
						BiffRecordRaw biffRecordRaw;
						if (biffRecordRaw == null)
						{
							num = 2;
							continue;
						}
						ixlsrange = this.ᜀ(biffRecordRaw);
						num = 1;
						continue;
					}
					}
					break;
					IL_58:
					this.ᜏ.SetRange(row, column, xlsRange);
					ixlsrange = xlsRange;
					num = 7;
					continue;
					IL_123:
					num = 3;
				}
			}
			return ixlsrange;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0003EFD0 File Offset: 0x0003DFD0
		private new IXLSRange ᜀ(BiffRecordRaw A_0)
		{
			int a_ = 6;
			while (A_0 != null)
			{
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
					base.ParseData();
					XlsRange xlsRange = base.AppImplementation.ᜀ(this, A_0, false);
					long cellIndex = xlsRange.CellIndex;
					this.ᜏ.SetRange(cellIndex, xlsRange);
					return xlsRange;
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅", a_));
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0003F058 File Offset: 0x0003E058
		public void SetFirstColumn(int columnIndex)
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
			this.AccessColumn(columnIndex);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0003F09C File Offset: 0x0003E09C
		public void SetLastColumn(int columnIndex)
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
			this.AccessColumn(columnIndex);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0003F0E0 File Offset: 0x0003E0E0
		public void SetFirstRow(int rowIndex)
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
			this.AccessRow(rowIndex);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003F124 File Offset: 0x0003E124
		public void SetLastRow(int rowIndex)
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
			this.AccessRow(rowIndex);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0003F168 File Offset: 0x0003E168
		protected internal void AccessColumn(int iColumnIndex)
		{
			for (;;)
			{
				base.ParseData();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						if (this.m_iFirstColumn <= iColumnIndex)
						{
							num = 9;
							continue;
						}
						goto IL_102;
					case 3:
						if (this.m_iLastColumn >= iColumnIndex)
						{
							goto IL_BB;
						}
						goto IL_ED;
					case 4:
						goto IL_A7;
					case 5:
						if (this.m_iLastColumn == 2147483647)
						{
							num = 6;
							continue;
						}
						return;
					case 6:
						goto IL_ED;
					case 7:
						goto IL_102;
					case 8:
						if (this.m_iFirstColumn == 2147483647)
						{
							num = 7;
							continue;
						}
						goto IL_A7;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BB;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					}
					break;
					IL_A7:
					num = 3;
					continue;
					IL_BB:
					num = 0;
					continue;
					IL_ED:
					this.m_iLastColumn = (int)((ushort)iColumnIndex);
					num = 1;
					continue;
					IL_102:
					this.m_iFirstColumn = (int)((ushort)iColumnIndex);
					num = 4;
				}
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0003F28C File Offset: 0x0003E28C
		protected internal void AccessRow(int iRowIndex)
		{
			for (;;)
			{
				base.ParseData();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.m_iLastRow >= iRowIndex)
						{
							goto IL_AF;
						}
						goto IL_E5;
					case 1:
						goto IL_E5;
					case 2:
						goto IL_F9;
					case 3:
						if (this.m_iFirstRow <= iRowIndex)
						{
							num = 5;
							continue;
						}
						goto IL_F9;
					case 4:
						if (this.m_iLastRow < 0)
						{
							num = 1;
							continue;
						}
						return;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 6:
						num = 4;
						continue;
					case 7:
						goto IL_9B;
					case 8:
						if (this.m_iFirstRow < 0)
						{
							num = 2;
							continue;
						}
						goto IL_9B;
					case 9:
						return;
					}
					break;
					IL_9B:
					num = 0;
					continue;
					IL_AF:
					num = 6;
					continue;
					IL_E5:
					this.m_iLastRow = iRowIndex;
					num = 9;
					continue;
					IL_F9:
					this.m_iFirstRow = iRowIndex;
					num = 7;
				}
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0003F3A8 File Offset: 0x0003E3A8
		protected void UpdateFirstLast(int iRowIndex, int iColumnIndex)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_3A:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_iFirstRow = ((this.m_iFirstRow > iRowIndex || this.m_iFirstRow < 0) ? iRowIndex : this.m_iFirstRow);
						if (true)
						{
						}
						num = 2;
						continue;
					case 1:
						this.m_iFirstColumn = ((this.m_iFirstColumn > iColumnIndex || this.m_iFirstColumn == int.MaxValue) ? ((int)((ushort)iColumnIndex)) : this.m_iFirstColumn);
						num = 3;
						continue;
					case 2:
						goto IL_F1;
					case 3:
						this.m_iLastColumn = ((this.m_iLastColumn < iColumnIndex || this.m_iLastColumn == int.MaxValue) ? ((int)((ushort)iColumnIndex)) : this.m_iLastColumn);
						num = 0;
						continue;
					}
					goto IL_34;
				}
				IL_F1:
				this.m_iLastRow = ((this.m_iLastRow < iRowIndex || this.m_iLastRow < 0) ? iRowIndex : this.m_iLastRow);
				return;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_34:
			base.ParseData();
			goto IL_3A;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0003F4D0 File Offset: 0x0003E4D0
		protected internal void InnerSetCell(int column, int row, XlsRange range)
		{
			int a_ = 15;
			while (!range.IsSingleCell)
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
					throw new ArgumentException(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㡚㱜ㅞ䅠ൢ੤፦䥨ᡪᡬὮŰᱲݴͶᱸὺ嵼᥾ꖄﲈ歷ﾒ랖滛ﺚ튠趢", a_));
				}
			}
			base.ParseData();
			this.ᜏ.SetRange(row, column, range);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0003F548 File Offset: 0x0003E548
		internal new void ᜀ(long A_0, BiffRecordRaw A_1)
		{
			int a_ = 4;
			while (A_1 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃", a_));
				}
			}
			base.ParseData();
			spr\u23A5 spr_u23A = (spr\u23A5)A_1;
			sprᜑ.ᜁ(this, spr_u23A.ᜅ() + 1);
			sprᜑ.ᜀ(this, spr_u23A.ᜄ() + 1);
			this.ᜏ.ᜁ(A_0, spr_u23A);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0003F5DC File Offset: 0x0003E5DC
		internal new void ᜀ(int A_0, int A_1, BiffRecordRaw A_2)
		{
			int a_ = 6;
			while (A_2 == null)
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
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅", a_));
				}
			}
			base.ParseData();
			this.ᜏ.ᜁ(A_1, A_0, A_2 as spr\u23A5);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0003F654 File Offset: 0x0003E654
		protected internal void InnerGetDimensions(out int left, out int top, out int right, out int bottom)
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
			base.ParseData();
			left = this.m_iFirstColumn;
			right = this.m_iLastColumn;
			top = this.m_iFirstRow;
			bottom = this.m_iLastRow;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0003F6B8 File Offset: 0x0003E6B8
		protected internal void InnerGetColumnDimensions(int column, out int top, out int bottom)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num2;
				for (;;)
				{
					base.ParseData();
					num = -1;
					num2 = -1;
					int num3 = this.FirstRow;
					int lastRow = this.LastRow;
					int num4 = 1;
					for (;;)
					{
						switch (num4)
						{
						case 0:
							if (num < num3)
							{
								num4 = 5;
								continue;
							}
							goto IL_82;
						case 1:
							goto IL_CB;
						case 2:
							goto IL_13A;
						case 3:
							goto IL_82;
						case 4:
						{
							long key;
							if (this.ᜏ.Contains(key))
							{
								num4 = 10;
								continue;
							}
							goto IL_13A;
						}
						case 5:
							num = num3;
							num4 = 3;
							continue;
						case 6:
							goto IL_CB;
						case 7:
							if (num2 == -1)
							{
								num4 = 8;
								continue;
							}
							goto IL_13A;
						case 8:
							num2 = num3;
							if (true)
							{
							}
							num4 = 2;
							continue;
						case 9:
						{
							if (num3 > lastRow)
							{
								num4 = 11;
								continue;
							}
							long key = sprṔ.ᜀ(column, num3);
							goto IL_10E;
						}
						case 10:
							num4 = 0;
							continue;
						case 11:
							goto IL_103;
						}
						break;
						IL_82:
						num4 = 7;
						continue;
						IL_CB:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_10E:
							num4 = 4;
							continue;
						default:
							if (false)
							{
							}
							num4 = 9;
							continue;
						}
						IL_13A:
						num3++;
						num4 = 6;
					}
				}
				IL_103:
				top = num2;
				bottom = num;
				return;
			}
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0003F830 File Offset: 0x0003E830
		internal new void ᜀ(Dictionary<int, int> A_0, spr\u202C A_1)
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
			base.ParseData();
			this.ᜏ.ᜀ(A_0, A_1);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0003F880 File Offset: 0x0003E880
		private void ᜁ(int A_0, int A_1, InsertOptionsType A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					spr\u216E spr_u216E = null;
					int num = this.m_book.MaxColumnCount;
					int num2 = 6;
					for (;;)
					{
						int num5;
						switch (num2)
						{
						case 0:
						{
							spr\u216E spr_u216E2 = spr_u216E;
							ushort a_;
							spr_u216E.ᜀ(a_ = (ushort)(num - 1));
							spr_u216E2.ᜄ(a_);
							num2 = 2;
							continue;
						}
						case 1:
							goto IL_191;
						case 2:
							goto IL_D0;
						case 3:
							goto IL_244;
						case 4:
							if (spr_u216E != null)
							{
								num2 = 18;
								continue;
							}
							return;
						case 5:
							goto IL_8F;
						case 6:
							goto IL_244;
						case 7:
							spr_u216E = this.ᜐ[A_0 - 1];
							num2 = 5;
							continue;
						case 8:
							spr_u216E = null;
							num2 = 15;
							continue;
						case 9:
							goto IL_191;
						case 10:
							return;
						case 11:
						{
							int num3;
							int num4;
							if (num3 >= num4)
							{
								num2 = 10;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_18C;
							default:
							{
								if (false)
								{
								}
								spr_u216E = (spr\u216E)spr_u216E.Clone();
								spr\u216E spr_u216E3 = spr_u216E;
								ushort a_2;
								spr_u216E.ᜀ(a_2 = (ushort)(num3 - 1));
								spr_u216E3.ᜄ(a_2);
								this.ᜐ[num3] = spr_u216E;
								num3++;
								num2 = 1;
								continue;
							}
							}
							break;
						}
						case 12:
							if (A_2 == InsertOptionsType.FormatAsAfter)
							{
								num2 = 13;
								continue;
							}
							goto IL_8F;
						case 13:
							spr_u216E = this.ᜐ[A_0 + A_1];
							num2 = 16;
							continue;
						case 14:
							if (num <= A_0 + A_1 - 1)
							{
								num2 = 8;
								continue;
							}
							num5 = num - A_1;
							spr_u216E = this.ᜐ[num5];
							num2 = 17;
							continue;
						case 15:
							if (A_2 == InsertOptionsType.FormatAsBefore)
							{
								num2 = 7;
								continue;
							}
							num2 = 12;
							continue;
						case 16:
							goto IL_18C;
						case 17:
							if (true)
							{
							}
							if (spr_u216E != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_D0;
						case 18:
						{
							int num3 = A_0;
							int num4 = A_0 + A_1;
							num2 = 9;
							continue;
						}
						}
						break;
						IL_8F:
						num2 = 4;
						continue;
						IL_D0:
						this.ᜐ[num] = spr_u216E;
						this.ᜐ[num5] = null;
						num--;
						num2 = 3;
						continue;
						IL_191:
						num2 = 11;
						continue;
						IL_244:
						num2 = 14;
						continue;
						IL_18C:
						goto IL_8F;
					}
				}
				return;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0003FAF8 File Offset: 0x0003EAF8
		private new void ᜀ(int A_0, int A_1, InsertOptionsType A_2)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						base.ParseData();
						spr\u216E spr_u216E = null;
						int num = A_0;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num > this.m_book.MaxColumnCount - A_1)
								{
									num2 = 7;
									continue;
								}
								int num3 = num + A_1;
								spr_u216E = this.ᜐ[num3];
								if (true)
								{
								}
								num2 = 9;
								continue;
							}
							case 1:
								return;
							case 2:
								goto IL_9E;
							case 3:
								goto IL_141;
							case 4:
								goto IL_141;
							case 5:
							{
								spr\u216E spr_u216E2 = spr_u216E;
								int maxColumnCount;
								ushort a_;
								spr_u216E.ᜀ(a_ = (ushort)(maxColumnCount - 1));
								spr_u216E2.ᜄ(a_);
								num2 = 1;
								continue;
							}
							case 6:
								if (spr_u216E != null)
								{
									num2 = 5;
									continue;
								}
								return;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
								{
									if (false)
									{
									}
									int maxColumnCount = this.m_book.MaxColumnCount;
									spr_u216E = (spr\u216E)spr\u1CD3.ᜀ(this.ᜐ[maxColumnCount - 1]);
									this.ᜐ[maxColumnCount] = spr_u216E;
									num2 = 6;
									continue;
								}
								}
								break;
							case 8:
							{
								spr\u216E spr_u216E3 = spr_u216E;
								ushort a_2;
								spr_u216E.ᜀ(a_2 = (ushort)(num - 1));
								spr_u216E3.ᜄ(a_2);
								num2 = 2;
								continue;
							}
							case 9:
								if (spr_u216E != null)
								{
									num2 = 8;
									continue;
								}
								goto IL_9E;
							}
							break;
							IL_9E:
							this.ᜐ[num] = spr_u216E;
							num++;
							num2 = 4;
							continue;
							IL_141:
							num2 = 0;
						}
					}
					break;
				}
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0003FC94 File Offset: 0x0003EC94
		private new void ᜀ(ref int A_0, ref int A_1, ref int A_2, ref int A_3)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1FE:
				A_2--;
				num = 9;
				break;
			default:
				if (false)
				{
				}
				num = 19;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_3 < A_1)
					{
						num = 16;
						continue;
					}
					num = 14;
					continue;
				case 1:
					num = 10;
					continue;
				case 2:
					num = 8;
					continue;
				case 3:
					if (this.ᜂ(A_1))
					{
						num = 20;
						continue;
					}
					goto IL_1B9;
				case 4:
					if (A_2 < A_0)
					{
						num = 2;
						continue;
					}
					num = 22;
					continue;
				case 5:
					goto IL_1FE;
				case 6:
					if (A_0 > A_2)
					{
						num = 23;
						continue;
					}
					num = 11;
					continue;
				case 7:
					goto IL_F6;
				case 8:
					goto IL_AF;
				case 9:
					goto IL_165;
				case 10:
					goto IL_1B9;
				case 11:
					if (this.ᜃ(A_0))
					{
						num = 24;
						continue;
					}
					goto IL_165;
				case 12:
					goto IL_AF;
				case 13:
					goto IL_165;
				case 14:
					if (!this.ᜂ(A_3))
					{
						num = 17;
						continue;
					}
					A_3--;
					num = 21;
					continue;
				case 15:
					num = 7;
					continue;
				case 16:
					goto IL_1D6;
				case 17:
					return;
				case 18:
					if (A_1 > A_3)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
				case 20:
					A_1++;
					num = 12;
					continue;
				case 21:
					goto IL_1B9;
				case 22:
					if (this.ᜃ(A_2))
					{
						num = 5;
						continue;
					}
					goto IL_AF;
				case 23:
					num = 13;
					continue;
				case 24:
					A_0++;
					num = 25;
					continue;
				case 25:
					goto IL_F6;
				}
				if (!this.ᜯ)
				{
					num = 15;
					continue;
				}
				goto IL_262;
				IL_AF:
				num = 18;
				continue;
				IL_F6:
				num = 6;
				continue;
				IL_165:
				num = 4;
				continue;
				IL_1B9:
				num = 0;
			}
			return;
			IL_1D6:
			IL_262:
			if (true)
			{
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003FF0C File Offset: 0x0003EF0C
		private new bool ᜃ(int A_0)
		{
			bool result;
			for (;;)
			{
				result = true;
				int num = this.m_iFirstColumn;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num > this.m_iLastColumn)
						{
							num2 = 4;
							continue;
						}
						num2 = 5;
						continue;
					case 1:
						if (true)
						{
						}
						goto IL_95;
					case 2:
						result = false;
						num2 = 6;
						continue;
					case 3:
						goto IL_95;
					case 4:
						return result;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (this.GetCellType(A_0, num, false) != XlsWorksheet.TRangeValueType.Blank)
							{
								num2 = 2;
								continue;
							}
							num++;
							num2 = 1;
							continue;
						}
						break;
					case 6:
						return result;
					}
					break;
					IL_95:
					num2 = 0;
				}
			}
			return result;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003FFDC File Offset: 0x0003EFDC
		private new bool ᜂ(int A_0)
		{
			bool result;
			for (;;)
			{
				if (true)
				{
				}
				result = true;
				int num = this.m_iFirstRow;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (this.GetCellType(num, A_0, false) != XlsWorksheet.TRangeValueType.Blank)
							{
								num2 = 2;
								continue;
							}
							num++;
							num2 = 3;
							continue;
						}
						break;
					case 1:
						goto IL_95;
					case 2:
						result = false;
						num2 = 6;
						continue;
					case 3:
						goto IL_95;
					case 4:
						if (num > this.m_iLastRow)
						{
							num2 = 5;
							continue;
						}
						num2 = 0;
						continue;
					case 5:
						return result;
					case 6:
						return result;
					}
					break;
					IL_95:
					num2 = 4;
				}
			}
			return result;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000400AC File Offset: 0x0003F0AC
		private new void ᜀ(int A_0, int A_1, int A_2, int A_3)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜎ.Dispose();
					num = 6;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					if (this.ᜎ != null)
					{
						num = 1;
						continue;
					}
					goto IL_167;
				case 4:
					if (this.ᜎ.LastColumn != A_3)
					{
						goto IL_101;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F4;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 5:
					if (this.ᜎ.FirstRow == A_0)
					{
						num = 2;
						continue;
					}
					goto IL_101;
				case 6:
					goto IL_D6;
				case 7:
					if (this.ᜎ.FirstColumn == A_1)
					{
						goto IL_F4;
					}
					goto IL_101;
				case 8:
					num = 9;
					continue;
				case 9:
					if (this.ᜎ.LastRow == A_2)
					{
						num = 10;
						continue;
					}
					goto IL_101;
				case 10:
					goto IL_98;
				case 11:
					num = 7;
					continue;
				case 12:
					num = 5;
					continue;
				}
				if (this.ᜎ != null)
				{
					if (true)
					{
					}
					num = 11;
					continue;
				}
				goto IL_101;
				IL_F4:
				num = 12;
				continue;
				IL_101:
				num = 3;
			}
			IL_98:
			this.ᜎ.ResetCells();
			return;
			IL_D6:
			IL_167:
			this.ᜎ = base.AppImplementation.ᜀ(this, A_1, A_0, A_3, A_2);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00040238 File Offset: 0x0003F238
		public void CopyToClipboard()
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
			base.ParseData();
			this.m_book.CopyToClipboard(this);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00040288 File Offset: 0x0003F288
		public void ReparseFormula()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					XlsRange xlsRange = (XlsRange)this.AllocatedRange;
					List<CellRange> list = xlsRange.CellsList;
					int i = 0;
					int count = list.Count;
					int num = 3;
					for (;;)
					{
						IL_10:
						switch (num)
						{
						case 0:
							goto IL_56;
						case 1:
							return;
						case 2:
							while (i < count)
							{
								XlsRange xlsRange2 = list[i];
								xlsRange2.ReparseFormulaString();
								i++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num = 0;
									goto IL_10;
								}
							}
							num = 1;
							continue;
						case 3:
							if (true)
							{
							}
							goto IL_56;
						}
						break;
						IL_56:
						num = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0004034C File Offset: 0x0003F34C
		internal new bool ᜀ(XlsRange A_0)
		{
			switch (0)
			{
			default:
			{
				bool flag;
				for (;;)
				{
					IL_47:
					flag = false;
					int a_ = A_0.Column - 1;
					int num = A_0.LastColumn - 1;
					int num2 = A_0.Row - 1;
					int lastRow = A_0.LastRow;
					for (;;)
					{
						int num3 = 8;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_167;
							case 1:
							{
								int num4;
								if (num4 <= num)
								{
									num3 = 3;
									continue;
								}
								goto IL_167;
							}
							case 2:
							{
								sprᱧ sprᱧ;
								if (sprᱧ != null)
								{
									num3 = 6;
									continue;
								}
								goto IL_167;
							}
							case 3:
							{
								flag = true;
								int num4;
								sprᱧ sprᱧ;
								sprᱧ.ᜁ(num4 + 1, num, base.AppImplementation.ᜨ());
								num3 = 4;
								continue;
							}
							case 4:
								goto IL_167;
							case 5:
							{
								if (!flag)
								{
									num3 = 9;
									continue;
								}
								sprᱧ sprᱧ;
								sprᱧ.ᜁ(a_, num, base.AppImplementation.ᜨ());
								num3 = 0;
								continue;
							}
							case 6:
								num3 = 5;
								continue;
							case 7:
								goto IL_F0;
							case 8:
								goto IL_F0;
							case 9:
							{
								sprᱧ sprᱧ;
								int num4 = sprᱧ.ᜉ(a_, num);
								num3 = 1;
								continue;
							}
							case 10:
							{
								if (num2 >= lastRow)
								{
									num3 = 11;
									continue;
								}
								sprᱧ sprᱧ = this.ᜏ.Table.ᜄ().ᜁ(num2);
								num3 = 2;
								continue;
							}
							case 11:
								goto IL_10D;
							}
							goto IL_47;
							IL_F0:
							num3 = 10;
							continue;
							IL_167:
							num2++;
							if (true)
							{
							}
							num3 = 7;
						}
						IL_10D:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_123;
						}
					}
				}
				IL_123:
				if (false)
				{
				}
				return flag;
			}
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0004050C File Offset: 0x0003F50C
		protected override SheetProtectionType PrepareProtectionOptions(SheetProtectionType options)
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
			return options &= ~SheetProtectionType.Content;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00040554 File Offset: 0x0003F554
		public double GetRowHeight(int row)
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
			return this.InnerGetRowHeight(row, true);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00040598 File Offset: 0x0003F598
		internal new void ᜀ(sprἛ A_0, IDecryptor A_1)
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
			base.ᜀ(A_0, ExcelParseOptions.Default, false, null, A_1);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000405E0 File Offset: 0x0003F5E0
		protected override void PrepareVariables(ExcelParseOptions options, bool bSkipParsing)
		{
			for (;;)
			{
				if (true)
				{
				}
				base.PrepareVariables(options, bSkipParsing);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜩ != null)
						{
							num = 5;
							continue;
						}
						goto IL_9B;
					case 1:
						goto IL_5F;
					case 2:
						goto IL_5D;
					case 3:
						this.\u171B.Clear();
						num = 1;
						continue;
					case 4:
						if (this.\u171B != null)
						{
							num = 3;
							continue;
						}
						goto IL_5F;
					case 5:
						this.ᜩ.Clear();
						num = 2;
						continue;
					}
					break;
					IL_5F:
					num = 0;
				}
			}
			IL_5D:
			IL_9B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5D;
			default:
				if (false)
				{
				}
				this.ᜧ = -1;
				this.ᜦ = -1;
				this.ᜤ = -1;
				this.ᜥ = -1;
				return;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000406C0 File Offset: 0x0003F6C0
		internal override void ParseRecord(BiffRecordRaw raw, bool bIgnoreStyles, Dictionary<int, int> hashNewXFormatIndexes)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				sprᱎ sprᱎ;
				for (;;)
				{
					TBIFFRecord typeCode;
					spr\u23A5 spr_u23A;
					switch (num)
					{
					case 0:
						goto IL_2F2;
					case 2:
						goto IL_B5B;
					case 3:
						goto IL_29E;
					case 4:
						goto IL_AB1;
					case 5:
						num = 92;
						continue;
					case 6:
						num = 77;
						continue;
					case 7:
						return;
					case 8:
						if (bIgnoreStyles)
						{
							num = 46;
							continue;
						}
						goto IL_7C3;
					case 9:
						num = 32;
						continue;
					case 10:
						goto IL_61D;
					case 11:
						num = 99;
						continue;
					case 12:
						if (UtilityMethods.ᜀ(XlsWorksheet.\u170D, raw.TypeCode) >= 0)
						{
							num = 69;
							continue;
						}
						goto IL_68E;
					case 13:
						switch (typeCode)
						{
						case (TBIFFRecord)2161:
						case (TBIFFRecord)2162:
							goto IL_B86;
						default:
							num = 9;
							continue;
						}
						break;
					case 14:
						num = 60;
						continue;
					case 15:
						if (sprᱎ.ᜁ() != 8)
						{
							num = 0;
							continue;
						}
						return;
					case 16:
						num = 43;
						continue;
					case 17:
						if (!this.KeepRecord)
						{
							if (true)
							{
							}
							num = 98;
							continue;
						}
						goto IL_5F9;
					case 18:
						goto IL_5F9;
					case 19:
						num = 13;
						continue;
					case 20:
						if (!this.KeepRecord)
						{
							num = 67;
							continue;
						}
						goto IL_7F4;
					case 21:
						num = 81;
						continue;
					case 22:
						if (typeCode != TBIFFRecord.Sort)
						{
							num = 56;
							continue;
						}
						goto IL_73C;
					case 23:
						this.KeepRecord = true;
						this.ᜎ.Add(raw);
						num = 28;
						continue;
					case 24:
						goto IL_7C3;
					case 25:
						if (!base.IsSkipParsing)
						{
							num = 41;
							continue;
						}
						return;
					case 26:
						return;
					case 27:
						num = 57;
						continue;
					case 28:
						goto IL_27A;
					case 29:
						if (typeCode != TBIFFRecord.HLink)
						{
							num = 66;
							continue;
						}
						num = 30;
						continue;
					case 30:
						if (!this.KeepRecord)
						{
							num = 34;
							continue;
						}
						goto IL_45C;
					case 31:
						if (typeCode != TBIFFRecord.DCON)
						{
							num = 6;
							continue;
						}
						goto IL_5AA;
					case 32:
						switch (typeCode)
						{
						case (TBIFFRecord)2167:
							goto IL_B86;
						case TBIFFRecord.Feature12:
							goto IL_8F7;
						default:
							num = 42;
							continue;
						}
						break;
					case 33:
						if (typeCode != TBIFFRecord.MergeCells)
						{
							num = 40;
							continue;
						}
						goto IL_77A;
					case 34:
						this.KeepRecord = true;
						this.ᜎ.Add(raw);
						num = 62;
						continue;
					case 35:
						if (typeCode != TBIFFRecord.RangeProtection)
						{
							num = 19;
							continue;
						}
						goto IL_3E8;
					case 36:
						num = 33;
						continue;
					case 37:
						if (this.ᜤ < 0)
						{
							num = 82;
							continue;
						}
						return;
					case 38:
						goto IL_826;
					case 39:
						goto IL_7F4;
					case 40:
						return;
					case 41:
						num = 12;
						continue;
					case 42:
						return;
					case 43:
						if (typeCode != TBIFFRecord.CustomProperty)
						{
							num = 11;
							continue;
						}
						num = 17;
						continue;
					case 44:
					{
						spr᱒ a_ = (spr᱒)raw;
						this.ᜁ(a_);
						num = 55;
						continue;
					}
					case 45:
						if (!this.KeepRecord)
						{
							num = 4;
							continue;
						}
						goto IL_3F5;
					case 46:
						spr_u23A.ᜀ((ushort)this.ᜀ((int)spr_u23A.ᜆ(), hashNewXFormatIndexes));
						num = 24;
						continue;
					case 47:
						num = 63;
						continue;
					case 48:
						if (this.ᜨ < 0)
						{
							num = 10;
							continue;
						}
						return;
					case 49:
						if (typeCode <= TBIFFRecord.MergeCells)
						{
							num = 50;
							continue;
						}
						num = 80;
						continue;
					case 50:
						num = 87;
						continue;
					case 51:
						return;
					case 52:
						if (typeCode != TBIFFRecord.Row)
						{
							num = 5;
							continue;
						}
						goto IL_7B5;
					case 53:
						return;
					case 54:
						switch (typeCode)
						{
						case TBIFFRecord.Note:
							goto IL_320;
						case TBIFFRecord.Selection:
							goto IL_448;
						default:
							num = 76;
							continue;
						}
						break;
					case 55:
						goto IL_507;
					case 56:
						num = 95;
						continue;
					case 57:
						if (typeCode <= TBIFFRecord.Pane)
						{
							num = 74;
							continue;
						}
						num = 31;
						continue;
					case 58:
						this.\u1733 = new List<BiffRecordRaw>();
						num = 72;
						continue;
					case 59:
						if (spr_u23A != null)
						{
							num = 71;
							continue;
						}
						goto IL_7C3;
					case 60:
						if (raw.TypeCode == TBIFFRecord.Formula)
						{
							num = 44;
							continue;
						}
						goto IL_507;
					case 61:
						if (typeCode != TBIFFRecord.Pane)
						{
							num = 51;
							continue;
						}
						goto IL_6DE;
					case 62:
						goto IL_45C;
					case 63:
						if (typeCode <= TBIFFRecord.DVal)
						{
							num = 97;
							continue;
						}
						num = 29;
						continue;
					case 64:
						num = 22;
						continue;
					case 65:
						if (typeCode != TBIFFRecord.Qsi)
						{
							num = 96;
							continue;
						}
						goto IL_8F7;
					case 66:
						num = 52;
						continue;
					case 67:
						this.KeepRecord = true;
						this.ᜎ.Add(raw);
						num = 39;
						continue;
					case 68:
						return;
					case 69:
						this.AutoFilterRecords.Add(raw);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AB1;
						default:
							if (false)
							{
							}
							num = 88;
							continue;
						}
						break;
					case 70:
						return;
					case 71:
						num = 8;
						continue;
					case 72:
						goto IL_B43;
					case 73:
						if (this.ᜧ < 0)
						{
							num = 38;
							continue;
						}
						return;
					case 74:
						num = 54;
						continue;
					case 75:
						goto IL_480;
					case 76:
						num = 61;
						continue;
					case 77:
						if (typeCode != TBIFFRecord.DefaultColWidth)
						{
							num = 21;
							continue;
						}
						sprᱎ = (sprᱎ)raw;
						num = 15;
						continue;
					case 78:
						if (this.ᜦ < 0)
						{
							num = 3;
							continue;
						}
						return;
					case 79:
						goto IL_3F5;
					case 80:
						if (typeCode <= TBIFFRecord.Index)
						{
							num = 47;
							continue;
						}
						num = 84;
						continue;
					case 81:
						if (typeCode != TBIFFRecord.ColumnInfo)
						{
							num = 70;
							continue;
						}
						goto IL_1FC;
					case 82:
						goto IL_419;
					case 83:
						if (!this.KeepRecord)
						{
							num = 23;
							continue;
						}
						goto IL_27A;
					case 84:
						if (typeCode <= TBIFFRecord.Qsif)
						{
							num = 16;
							continue;
						}
						num = 35;
						continue;
					case 85:
						if (typeCode != TBIFFRecord.ExternalSourceInfo)
						{
							num = 36;
							continue;
						}
						goto IL_8F7;
					case 86:
						if (this.\u1733 == null)
						{
							num = 58;
							continue;
						}
						goto IL_B43;
					case 87:
						if (typeCode <= TBIFFRecord.ColumnInfo)
						{
							num = 27;
							continue;
						}
						num = 91;
						continue;
					case 88:
						goto IL_68E;
					case 89:
						if (typeCode != TBIFFRecord.PivotString)
						{
							num = 93;
							continue;
						}
						goto IL_8F7;
					case 90:
						switch (typeCode)
						{
						case TBIFFRecord.CondFMT:
							num = 83;
							continue;
						case TBIFFRecord.CF:
							return;
						case TBIFFRecord.DVal:
							num = 20;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					case 91:
						if (typeCode <= TBIFFRecord.PivotViewDefinition)
						{
							num = 64;
							continue;
						}
						num = 89;
						continue;
					case 92:
						if (typeCode != TBIFFRecord.Index)
						{
							num = 26;
							continue;
						}
						goto IL_9A8;
					case 93:
						num = 85;
						continue;
					case 94:
						if (this.ᜥ < 0)
						{
							num = 75;
							continue;
						}
						return;
					case 95:
						if (typeCode != TBIFFRecord.PivotViewDefinition)
						{
							num = 68;
							continue;
						}
						num = 45;
						continue;
					case 96:
						num = 90;
						continue;
					case 97:
						num = 65;
						continue;
					case 98:
						this.KeepRecord = true;
						this.ᜎ.Add(raw);
						num = 18;
						continue;
					case 99:
						switch (typeCode)
						{
						case TBIFFRecord.QsiSXTag:
						case TBIFFRecord.DBQueryExt:
						case TBIFFRecord.Qsir:
						case TBIFFRecord.Qsif:
							goto IL_8F7;
						case (TBIFFRecord)2052:
						case (TBIFFRecord)2053:
							return;
						default:
							num = 53;
							continue;
						}
						break;
					}
					if (this.m_book.HasDuplicatedNames)
					{
						num = 14;
						continue;
					}
					goto IL_507;
					IL_27A:
					num = 78;
					continue;
					IL_3F5:
					num = 37;
					continue;
					IL_45C:
					num = 94;
					continue;
					IL_507:
					num = 25;
					continue;
					IL_5F9:
					num = 48;
					continue;
					IL_68E:
					spr_u23A = (raw as spr\u23A5);
					num = 59;
					continue;
					IL_7C3:
					typeCode = raw.TypeCode;
					num = 49;
					continue;
					IL_7F4:
					spr\u22CB spr_u22CB = (spr\u22CB)raw;
					spr_u22CB.ᜁ(false);
					num = 73;
					continue;
					IL_AB1:
					this.KeepRecord = true;
					this.ᜎ.Add(raw);
					num = 79;
					continue;
					IL_B43:
					this.\u1733.Add(raw);
					num = 2;
					continue;
					IL_B86:
					num = 86;
				}
				IL_1FC:
				this.ᜀ((spr\u216E)raw, bIgnoreStyles);
				return;
				IL_29E:
				this.ᜦ = this.ᜎ.Count - 1;
				return;
				IL_2F2:
				this.\u1713 = this.m_book.WidthToFileWidth((double)sprᱎ.ᜁ());
				return;
				IL_320:
				this.ᜀ(raw as spr\u2114);
				return;
				IL_3E8:
				this.ᜀ((spr\u1A5D)raw);
				return;
				IL_419:
				this.ᜤ = this.ᜎ.Count - 1;
				return;
				IL_448:
				this.\u1716.Add((spr\u21A4)raw);
				return;
				IL_480:
				this.ᜥ = this.ᜎ.Count - 1;
				return;
				IL_5AA:
				this.DConRecords.Add(raw);
				return;
				IL_61D:
				this.ᜨ = this.ᜎ.Count - 1;
				return;
				IL_6DE:
				this.\u1717 = (spr\u2408)raw;
				return;
				IL_73C:
				this.SortRecords.Add(raw);
				return;
				IL_77A:
				this.MergeCells.ᜀ((spr\u25A6)raw);
				return;
				IL_7B5:
				this.ᜀ((spr\u20BA)raw, bIgnoreStyles);
				return;
				IL_826:
				this.ᜧ = this.ᜎ.Count - 1;
				return;
				IL_8F7:
				this.PreserveExternalConnection.Add(raw);
				return;
				IL_9A8:
				this.ᜭ = (spr\u218B)raw;
				return;
				IL_B5B:
				return;
			}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00041278 File Offset: 0x00040278
		private new void ᜀ(spr\u1A5D A_0)
		{
			int a_ = 15;
			int num = 0;
			for (;;)
			{
				spr\u1F7E spr_u1F7E;
				switch (num)
				{
				case 1:
					goto IL_43;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						if (false)
						{
						}
						this.ᜮ = new spr\u2622(base.AppImplementation, this);
						if (true)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					if (this.ᜮ == null)
					{
						num = 2;
						continue;
					}
					goto IL_E9;
				case 4:
					if (spr_u1F7E == null)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 5:
					goto IL_C2;
				case 6:
					return;
				}
				goto IL_35;
				IL_3B:
				num = 1;
				continue;
				IL_35:
				if (A_0 == null)
				{
					goto IL_3B;
				}
				spr_u1F7E = A_0.ᜂ();
				num = 4;
			}
			IL_43:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⩈⑊㽌⭎", a_));
			IL_C2:
			IL_E9:
			this.ᜮ.ᜀ(A_0.ᜂ());
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00041380 File Offset: 0x00040380
		private void ᜁ(spr᱒ A_0)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num4;
					int num5;
					Ptg[] array;
					switch (num)
					{
					case 0:
					{
						Ptg ptg;
						spr\u1B76 spr_u1B = (spr\u1B76)ptg;
						int num2 = (int)spr_u1B.ᜃ();
						int num3 = (int)spr_u1B.ᜂ();
						num = 10;
						continue;
					}
					case 1:
						goto IL_64;
					case 2:
						goto IL_127;
					case 3:
					{
						if (num4 >= num5)
						{
							num = 4;
							continue;
						}
						Ptg ptg = array[num4];
						if (true)
						{
						}
						num = 8;
						continue;
					}
					case 4:
						return;
					case 6:
						goto IL_127;
					case 7:
						goto IL_159;
					case 8:
					{
						Ptg ptg;
						if (FormulaUtil.ᜀ(FormulaUtil.\u171C, ptg.TokenCode) != -1)
						{
							num = 0;
							continue;
						}
						goto IL_159;
					}
					case 9:
					{
						int num2;
						XlsExternWorkbook xlsExternWorkbook = this.m_book.ExternWorkbooks[num2];
						spr\u1B76 spr_u1B;
						int num3;
						spr_u1B.ᜀ((ushort)(xlsExternWorkbook.GetNewIndex(num3 - 1) + 1));
						num = 7;
						continue;
					}
					case 10:
					{
						int num2;
						if (!this.m_book.IsLocalReference(num2))
						{
							num = 9;
							continue;
						}
						goto IL_159;
					}
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_159;
					}
					if (false)
					{
					}
					array = A_0.ᜑ();
					num4 = 0;
					num5 = array.Length;
					num = 2;
					continue;
					IL_127:
					num = 3;
					continue;
					IL_159:
					num4++;
					num = 6;
				}
				IL_64:
				throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃㍅⑇⭉", a_));
			}
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00041534 File Offset: 0x00040534
		private new int ᜀ(int A_0, Dictionary<int, int> A_1)
		{
			int a_ = 19;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_1 != null)
				{
					if (true)
					{
					}
					sprỶ sprỶ = this.m_book.InnerExtFormatRecords[A_0];
					int key = (int)sprỶ.ᜂ();
					return A_1[key];
				}
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅈ⩊㹌❎ὐ㙒≔བ὘㑚⽜㉞`ᝢⱤ०൨๪ᕬ੮ɰ", a_));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x000415B4 File Offset: 0x000405B4
		internal new void ᜀ(TextReader A_0, string A_1, int A_2, int A_3, bool A_4)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag)
						{
							num = 11;
							continue;
						}
						goto IL_210;
					}
					case 1:
						goto IL_279;
					case 2:
						goto IL_210;
					case 3:
					{
						string text;
						if (text.Length > 0)
						{
							num = 13;
							continue;
						}
						goto IL_12D;
					}
					case 4:
						goto IL_12D;
					case 5:
					{
						if (A_1 == null)
						{
							num = 17;
							continue;
						}
						int length = A_1.Length;
						num = 7;
						continue;
					}
					case 6:
						goto IL_1DD;
					case 7:
					{
						int length;
						if (length == 0)
						{
							num = 6;
							continue;
						}
						int num2 = A_2;
						StringBuilder a_2 = new StringBuilder();
						int num3 = A_3;
						num = 1;
						continue;
					}
					case 8:
					{
						int num2;
						num2++;
						int num3 = A_3;
						num = 9;
						continue;
					}
					case 9:
						goto IL_14E;
					case 11:
					{
						string text = text.Remove(text.Length - 1);
						num = 2;
						continue;
					}
					case 12:
						goto IL_14E;
					case 13:
					{
						string text;
						int num2;
						int num3;
						this.ᜀ(this.AllocatedRange[num2, num3], text, A_1, 0);
						num = 4;
						continue;
					}
					case 14:
						goto IL_84;
					case 15:
					{
						if (A_0.Peek() < 0)
						{
							num = 18;
							continue;
						}
						StringBuilder a_2;
						string text = XlsWorksheet.ᜀ(A_0, A_1, a_2, A_4);
						bool flag = text.EndsWith(RecordTableEnumerator.b("乃", a_));
						num = 0;
						continue;
					}
					case 16:
					{
						bool flag;
						if (flag)
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_279;
						default:
						{
							if (false)
							{
							}
							int num3;
							num3++;
							num = 12;
							continue;
						}
						}
						break;
					}
					case 17:
						goto IL_123;
					case 18:
						return;
					}
					if (A_0 == null)
					{
						num = 14;
						continue;
					}
					num = 5;
					continue;
					IL_12D:
					num = 16;
					continue;
					IL_14E:
					num = 15;
					continue;
					IL_279:
					goto IL_14E;
					IL_210:
					num = 3;
				}
				IL_84:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅㩇⽉ⵋ⍍я㵑ٓ㍕㥗㹙", a_));
				IL_123:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃⍅㡇⭉㹋⽍⑏㵑♓", a_));
				IL_1DD:
				throw new ArgumentException(RecordTableEnumerator.b("㝃⍅㡇⭉㹋⽍⑏㵑♓", a_));
			}
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00041840 File Offset: 0x00040840
		private new static string ᜀ(TextReader A_0, string A_1, StringBuilder A_2, bool A_3)
		{
			for (;;)
			{
				A_2.Length = 0;
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						char c;
						if (c != '\r')
						{
							goto IL_96;
						}
						goto IL_FE;
					}
					case 1:
						goto IL_FE;
					case 2:
					{
						char c;
						A_2.Append(c);
						XlsWorksheet.ᜀ(A_0, c, A_2, A_1, A_3);
						num = 1;
						continue;
					}
					case 3:
						num = 10;
						continue;
					case 4:
					{
						char c;
						if (c == '"')
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					}
					case 5:
					{
						char c;
						A_2.Append(c);
						num = 11;
						continue;
					}
					case 6:
						if (num2 >= 0)
						{
							num = 8;
							continue;
						}
						goto IL_155;
					case 7:
						goto IL_FC;
					case 8:
					{
						if (true)
						{
						}
						char c = (char)num2;
						num = 4;
						continue;
					}
					case 9:
						if (XlsWorksheet.ᜀ(A_2, A_1))
						{
							num = 7;
							continue;
						}
						goto IL_FE;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_96;
						default:
						{
							if (false)
							{
							}
							char c;
							if (c == '\n')
							{
								num = 5;
								continue;
							}
							A_2.Append(c);
							num = 9;
							continue;
						}
						}
						break;
					case 11:
						goto IL_134;
					case 12:
						goto IL_FE;
					}
					break;
					IL_96:
					num = 3;
					continue;
					IL_FE:
					num2 = A_0.Read();
					num = 6;
				}
			}
			IL_FC:
			IL_134:
			IL_155:
			return A_2.ToString();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000419A8 File Offset: 0x000409A8
		private new static bool ᜀ(StringBuilder A_0, string A_1)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 2;
				bool result;
				for (;;)
				{
					int length;
					int length2;
					switch (num)
					{
					case 0:
					{
						result = true;
						int num2 = length - 1;
						int num3 = length2 - 1;
						num = 5;
						continue;
					}
					case 1:
						goto IL_E1;
					case 3:
						if (length >= length2)
						{
							num = 0;
							continue;
						}
						return result;
					case 4:
						result = false;
						num = 9;
						continue;
					case 5:
						goto IL_E1;
					case 6:
					{
						int num3;
						if (num3 < 0)
						{
							num = 8;
							continue;
						}
						num = 10;
						continue;
					}
					case 7:
						goto IL_66;
					case 8:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_11C;
						}
						break;
					case 9:
						goto IL_80;
					case 10:
					{
						int num2;
						int num3;
						if (A_0[num2] != A_1[num3])
						{
							num = 4;
							continue;
						}
						num2--;
						num3--;
						num = 1;
						continue;
					}
					}
					if (string.IsNullOrEmpty(A_1))
					{
						num = 7;
						continue;
					}
					length = A_0.Length;
					length2 = A_1.Length;
					result = false;
					num = 3;
					continue;
					IL_E1:
					num = 6;
				}
				IL_66:
				throw new ArgumentException(RecordTableEnumerator.b("䐶尸䬺尼䴾⁀㝂⩄㕆", a_));
				IL_80:
				return result;
				IL_11C:
				if (false)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00041B24 File Offset: 0x00040B24
		private new static void ᜀ(TextReader A_0, char A_1, StringBuilder A_2, string A_3, bool A_4)
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
				if (!A_4)
				{
					XlsWorksheet.ᜀ(A_0, A_1, A_2, A_3);
					return;
				}
				break;
			}
			XlsWorksheet.ᜀ(A_0, A_1, A_2);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00041B78 File Offset: 0x00040B78
		private new static void ᜀ(TextReader A_0, char A_1, StringBuilder A_2, string A_3)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = true;
					int num = 14;
					for (;;)
					{
						char c2;
						int num2;
						switch (num)
						{
						case 0:
							num = 5;
							continue;
						case 1:
						{
							char c;
							if (c == '\n')
							{
								num = 4;
								continue;
							}
							goto IL_131;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_151;
							default:
							{
								if (false)
								{
								}
								char c = (char)A_0.Peek();
								num = 12;
								continue;
							}
							}
							break;
						case 3:
							goto IL_131;
						case 4:
							goto IL_76;
						case 5:
						{
							if (true)
							{
							}
							char c;
							if (c != '\r')
							{
								num = 6;
								continue;
							}
							goto IL_76;
						}
						case 6:
							goto IL_151;
						case 7:
							num = 11;
							continue;
						case 8:
							if (flag)
							{
								num = 7;
								continue;
							}
							return;
						case 9:
							goto IL_131;
						case 10:
							if (c2 == A_1)
							{
								num = 2;
								continue;
							}
							A_2.Append(c2);
							num = 3;
							continue;
						case 11:
							if (num2 <= 0)
							{
								num = 13;
								continue;
							}
							goto IL_DD;
						case 12:
						{
							char c;
							if (c != Convert.ToChar(A_3))
							{
								num = 0;
								continue;
							}
							goto IL_76;
						}
						case 13:
							return;
						case 14:
							goto IL_DD;
						}
						break;
						IL_76:
						flag = false;
						A_2.Append(c2);
						num = 9;
						continue;
						IL_DD:
						num2 = A_0.Read();
						c2 = (char)num2;
						num = 10;
						continue;
						IL_131:
						num = 8;
						continue;
						IL_151:
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00041D24 File Offset: 0x00040D24
		private new static void ᜀ(TextReader A_0, char A_1, StringBuilder A_2)
		{
			for (;;)
			{
				for (;;)
				{
					int num = A_0.Read();
					char c = (char)num;
					A_2.Append(c);
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (c != A_1)
							{
								num2 = 3;
								continue;
							}
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4A;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								if (num <= 0)
								{
									num2 = 2;
									continue;
								}
								break;
							}
							break;
						case 2:
							return;
						case 3:
							goto IL_4A;
						}
						break;
						IL_4A:
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00041DB8 File Offset: 0x00040DB8
		private new static int ᜀ(string A_0, char A_1)
		{
			int num;
			for (;;)
			{
				if (true)
				{
				}
				num = 0;
				int num2 = A_0.Length - 1;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						num++;
						num3 = 5;
						continue;
					case 1:
						goto IL_A5;
					case 2:
						if (A_0[num2] == A_1)
						{
							num3 = 0;
							continue;
						}
						goto IL_41;
					case 3:
						if (num2 < 0)
						{
							num3 = 6;
							continue;
						}
						num3 = 2;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41;
						default:
							if (false)
							{
							}
							goto IL_A5;
						}
						break;
					case 5:
						goto IL_41;
					case 6:
						return num;
					}
					break;
					IL_41:
					num2--;
					num3 = 4;
					continue;
					IL_A5:
					num3 = 3;
				}
			}
			return num;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00041E88 File Offset: 0x00040E88
		protected internal override void ParseData(Dictionary<int, int> dictUpdatedSSTIndexes)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!base.IsSkipParsing)
					{
						num = 4;
						continue;
					}
					goto IL_278;
				case 2:
					num = 11;
					continue;
				case 3:
					if (this.ᜨ >= 0)
					{
						num = 18;
						continue;
					}
					goto IL_278;
				case 4:
					goto IL_225;
				case 5:
					if (this.ᜦ >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_1DB;
				case 6:
					if (this.ᜧ >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_1B4;
				case 7:
					num = 8;
					continue;
				case 8:
				{
					bool flag;
					if (!flag)
					{
						num = 15;
						continue;
					}
					goto IL_1B4;
				}
				case 9:
					num = 0;
					continue;
				case 10:
					goto IL_1DB;
				case 11:
				{
					bool flag;
					if (!flag)
					{
						num = 16;
						continue;
					}
					goto IL_1DB;
				}
				case 12:
					if (this.ᜠ == null)
					{
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_225;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.AttachEvents();
						this.ᜠ.ᜁ(this, dictUpdatedSSTIndexes);
						num = 19;
						continue;
					}
					break;
				case 13:
					goto IL_FB;
				case 14:
				{
					if (base.IsParsing)
					{
						num = 20;
						continue;
					}
					base.IsParsing = true;
					bool flag = false;
					num = 12;
					continue;
				}
				case 15:
					this.ExtractDataValidation(this.ᜧ);
					num = 21;
					continue;
				case 16:
					this.ExtractConditionalFormats(this.ᜦ);
					num = 10;
					continue;
				case 17:
					num = 14;
					continue;
				case 18:
					this.ExtractCustomProperties(this.ᜨ);
					num = 13;
					continue;
				case 19:
					goto IL_164;
				case 20:
					goto IL_220;
				case 21:
					goto IL_1B4;
				}
				if (!base.IsParsed)
				{
					num = 17;
					continue;
				}
				return;
				IL_1B4:
				num = 3;
				continue;
				IL_1DB:
				num = 6;
				continue;
				IL_225:
				int iStartIndex = this.ExtractCalculationOptions();
				this.ᜂ();
				this.ExtractPageSetup(iStartIndex);
				this.ExtractPivotTables(this.ᜤ);
				this.ExtractHyperLinks(this.ᜥ);
				num = 5;
			}
			IL_FB:
			IL_164:
			goto IL_278;
			IL_220:
			return;
			IL_278:
			base.IsSaved = true;
			base.IsParsed = true;
			base.IsParsing = false;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00042124 File Offset: 0x00041124
		private new void ᜂ()
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
			this.ᜏ.ReplaceSharedFormula();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0004216C File Offset: 0x0004116C
		internal new void ᜀ(spr\u216E A_0, bool A_1)
		{
			int a_ = 15;
			int num = 7;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (A_1)
					{
						num = 8;
						continue;
					}
					goto IL_143;
				case 1:
					goto IL_143;
				case 2:
					goto IL_5F;
				case 3:
					goto IL_F6;
				case 4:
					if ((int)A_0.ᜀ() == this.m_book.MaxColumnCount)
					{
						num = 12;
						continue;
					}
					goto IL_12E;
				case 5:
					goto IL_F6;
				case 6:
					if (A_0.ᜈ() != A_0.ᜀ())
					{
						if (true)
						{
						}
						num = 11;
						continue;
					}
					goto IL_1E5;
				case 8:
					A_0.ᜃ((ushort)this.m_book.DefaultXFIndex);
					num = 1;
					continue;
				case 9:
					goto IL_12E;
				case 10:
				{
					if (num2 > (int)A_0.ᜀ())
					{
						num = 13;
						continue;
					}
					int num3 = num2 + 1;
					spr\u216E spr_u216E = (spr\u216E)A_0.Clone();
					spr_u216E.ᜄ((ushort)num2);
					spr_u216E.ᜀ((ushort)num2);
					this.ᜐ[num3] = spr_u216E;
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F6;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 11:
					num = 4;
					continue;
				case 12:
					this.\u173C = (A_0.Clone() as spr\u216E);
					num = 9;
					continue;
				case 13:
					return;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
				IL_F6:
				num = 10;
				continue;
				IL_12E:
				num2 = (int)A_0.ᜈ();
				num = 5;
				continue;
				IL_143:
				A_0.ᜅ((ushort)this.ᜈ((int)A_0.ᜉ()));
				A_0.ᜌ();
				num = 6;
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("♄⡆╈㹊⁌ⅎᡐ㵒㍔㡖", a_));
			IL_1E5:
			this.ᜐ[(int)(A_0.ᜈ() + 1)] = A_0;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00042370 File Offset: 0x00041370
		internal new void ᜀ(spr\u20BA A_0, bool A_1)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				sprᱧ sprᱧ;
				for (;;)
				{
					IL_17:
					int num = 7;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							goto IL_294;
						case 1:
							goto IL_1BC;
						case 2:
							if (A_1)
							{
								num = 4;
								continue;
							}
							goto IL_1D9;
						case 3:
							if (this.FirstColumn == 2147483647)
							{
								num = 16;
								continue;
							}
							goto IL_145;
						case 4:
							A_0.ᜃ((ushort)this.m_book.DefaultXFIndex);
							A_0.ᜅ(false);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num = 14;
								continue;
							}
							break;
						case 5:
							goto IL_9A;
						case 6:
							goto IL_2C2;
						case 8:
						{
							spr\u192F spr_u192F = (spr\u192F)spr_u192F.\u1758();
							spr_u192F.ᜄ(num2);
							spr_u192F.ᜑ().ᜀ(sprỶ.TXFType.XF_STYLE);
							spr_u192F = this.m_book.InnerExtFormats.ᜁ(spr_u192F);
							sprᱧ.ᜀ((ushort)spr_u192F.ᜠ());
							num = 1;
							continue;
						}
						case 9:
							goto IL_2F0;
						case 10:
						{
							if (num2 > this.m_book.InnerExtFormats.Count)
							{
								num = 0;
								continue;
							}
							spr\u192F spr_u192F = this.m_book.InnerExtFormats.ᜁ(num2);
							num = 19;
							continue;
						}
						case 11:
							this.FirstRow = num3;
							num = 6;
							continue;
						case 12:
							if (base.LastColumn == 2147483647)
							{
								num = 13;
								continue;
							}
							goto IL_262;
						case 13:
							base.LastColumn = 1;
							num = 21;
							continue;
						case 14:
							goto IL_1D9;
						case 15:
							if (true)
							{
							}
							this.LastRow = num3;
							num = 9;
							continue;
						case 16:
							this.FirstColumn = 1;
							num = 20;
							continue;
						case 17:
							if (num3 > this.LastRow)
							{
								num = 15;
								continue;
							}
							goto IL_2F0;
						case 18:
							if (num3 < this.FirstRow)
							{
								num = 11;
								continue;
							}
							goto IL_2C2;
						case 19:
						{
							spr\u192F spr_u192F;
							if (!spr_u192F.ᝇ())
							{
								num = 8;
								continue;
							}
							return;
						}
						case 20:
							goto IL_145;
						case 21:
							goto IL_262;
						}
						if (A_0 == null)
						{
							num = 5;
							continue;
						}
						num = 2;
						continue;
						IL_145:
						num = 12;
						continue;
						IL_1D9:
						sprᱧ = sprᜑ.ᜀ(this, (int)A_0.ᜇ(), true);
						sprᱧ.ᜀ(A_0, base.AppImplementation.ᜈ());
						num3 = (int)(A_0.ᜇ() + 1);
						num = 18;
						continue;
						IL_262:
						num2 = (int)sprᱧ.ᜇ();
						num = 10;
						continue;
						IL_2C2:
						num = 17;
						continue;
						IL_2F0:
						num = 3;
					}
				}
				IL_9A:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺刼䠾", a_));
				IL_1BC:
				return;
				IL_294:
				sprᱧ.ᜀ((ushort)this.m_book.DefaultXFIndex);
				return;
			}
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0004269C File Offset: 0x0004169C
		protected void ExtractHyperLinks(int iLinkIndex)
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
				if (iLinkIndex >= 0)
				{
					if (true)
					{
					}
					this.InnerHyperLinks.Clear();
					this.InnerHyperLinks.Parse(this.ᜎ, iLinkIndex);
					return;
				}
				break;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000426FC File Offset: 0x000416FC
		protected int ExtractCalculationOptions()
		{
			int num;
			for (;;)
			{
				num = 0;
				int count = this.ᜎ.Count;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (true)
						{
						}
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						BiffRecordRaw biffRecordRaw = this.ᜎ[num];
						num2 = 1;
						continue;
					}
					case 1:
					{
						BiffRecordRaw biffRecordRaw;
						if (Array.IndexOf<TBIFFRecord>(sprỆ.ᜀ, biffRecordRaw.TypeCode) != -1)
						{
							num2 = 2;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BC;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					}
					case 2:
						goto IL_BC;
					case 3:
						goto IL_BE;
					case 4:
						goto IL_BE;
					case 5:
						return 0;
					}
					break;
					IL_BE:
					num2 = 0;
				}
			}
			IL_BC:
			return this.m_book.InnerCalculation.ᜀ(this.ᜎ, num);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000427EC File Offset: 0x000417EC
		protected void ExtractPageSetup(int iStartIndex)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 3;
				int num2;
				for (;;)
				{
					int count;
					switch (num)
					{
					case 0:
					{
						TBIFFRecord typeCode;
						if (typeCode != TBIFFRecord.PrintHeaders)
						{
							num = 8;
							continue;
						}
						goto IL_63;
					}
					case 1:
						return;
					case 2:
						goto IL_C4;
					case 4:
						goto IL_C4;
					case 5:
						goto IL_5E;
					case 6:
						goto IL_9C;
					case 7:
					{
						TBIFFRecord typeCode;
						if (typeCode == TBIFFRecord.DefaultRowHeight)
						{
							num = 6;
							continue;
						}
						num2++;
						num = 4;
						continue;
					}
					case 8:
						num = 7;
						continue;
					case 9:
						if (num2 >= count)
						{
							num = 1;
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
							BiffRecordRaw biffRecordRaw = this.ᜎ[num2];
							TBIFFRecord typeCode = biffRecordRaw.TypeCode;
							num = 0;
							continue;
						}
						}
						break;
					}
					if (iStartIndex < 0)
					{
						num = 5;
						continue;
					}
					num2 = iStartIndex;
					count = this.ᜎ.Count;
					num = 2;
					continue;
					IL_C4:
					if (true)
					{
					}
					num = 9;
				}
				IL_5E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("丼䬾⁀ㅂㅄๆ❈⽊⡌㝎", a_));
				IL_63:
				this.\u1712 = new PageSetup((spr\u2158)base.ReservedHandle, this, this.ᜎ, num2);
				return;
				IL_9C:
				goto IL_63;
			}
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00042960 File Offset: 0x00041960
		protected void ExtractConditionalFormats(int iCondFmtPos)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					BiffRecordRaw biffRecordRaw;
					switch (num)
					{
					case 0:
					{
						spr\u21C4 spr_u21C;
						List<spr\u206F> list;
						this.ᜀ(spr_u21C, list);
						num = 13;
						continue;
					}
					case 1:
						goto IL_1B7;
					case 2:
						if (true)
						{
						}
						goto IL_B8;
					case 4:
					{
						spr\u21C4 spr_u21C;
						List<spr\u206F> list;
						this.ᜀ(spr_u21C, list);
						list.Clear();
						num = 2;
						continue;
					}
					case 5:
					{
						spr\u21C4 spr_u21C;
						if (spr_u21C != null)
						{
							num = 4;
							continue;
						}
						goto IL_B8;
					}
					case 6:
					{
						bool flag = false;
						num = 14;
						continue;
					}
					case 7:
					{
						spr\u21C4 spr_u21C;
						if (spr_u21C != null)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 8:
						goto IL_A2;
					case 9:
						num = 7;
						continue;
					case 10:
						goto IL_1B7;
					case 11:
					{
						TBIFFRecord typeCode;
						switch (typeCode)
						{
						case TBIFFRecord.CondFMT:
							num = 5;
							continue;
						case TBIFFRecord.CF:
						{
							List<spr\u206F> list;
							list.Add((spr\u206F)biffRecordRaw);
							num = 15;
							continue;
						}
						default:
							num = 12;
							continue;
						}
						break;
					}
					case 12:
						num = 6;
						continue;
					case 13:
						return;
					case 14:
						goto IL_A2;
					case 15:
						goto IL_A2;
					case 16:
						goto IL_9D;
					case 17:
					{
						bool flag;
						if (!flag)
						{
							num = 9;
							continue;
						}
						biffRecordRaw = this.ᜎ[iCondFmtPos];
						TBIFFRecord typeCode = biffRecordRaw.TypeCode;
						num = 11;
						continue;
					}
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_B8:
						spr\u21C4 spr_u21C = (spr\u21C4)biffRecordRaw;
						num = 8;
						continue;
					}
					default:
					{
						if (false)
						{
						}
						if (iCondFmtPos < 0)
						{
							num = 16;
							continue;
						}
						bool flag = true;
						spr\u21C4 spr_u21C = null;
						List<spr\u206F> list = new List<spr\u206F>();
						num = 1;
						continue;
					}
					}
					IL_A2:
					iCondFmtPos++;
					num = 10;
					continue;
					IL_1B7:
					num = 17;
				}
				IL_9D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張笷唹刻娽ؿ⽁ぃᙅ❇㥉", a_));
			}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00042B9C File Offset: 0x00041B9C
		protected void ExtractDataValidation(int iDValPos)
		{
			int a_ = 0;
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
				if (iDValPos >= 0)
				{
					this.\u171F = new XlsDataValidationTable(base.ReservedHandle, this, this.ᜎ, ref iDValPos);
					return;
				}
				break;
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張簷氹崻刽ဿⵁ㝃", a_));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00042C14 File Offset: 0x00041C14
		protected void ExtractCustomProperties(int iCustomPropertyPos)
		{
			int a_ = 11;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (iCustomPropertyPos >= 0)
				{
					this.ᜫ = new spr\u256D(this.ᜎ, iCustomPropertyPos);
					return;
				}
				break;
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀Bい㑆㵈⑊⁌὎⍐㱒╔㉖⭘⽚⑜ཞ๠ၢ", a_));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00042C84 File Offset: 0x00041C84
		private new void ᜀ(spr\u21C4 A_0, IList A_1)
		{
			int a_ = 16;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_1 != null)
						{
							goto IL_A1;
						}
						break;
					}
					num = 1;
					continue;
				case 1:
					goto IL_83;
				case 3:
					goto IL_34;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅❇㡉⅋⽍⑏", a_));
			IL_83:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⩅㭇㹉ཋ⅍㹏㙑㵓≕ㅗ㕙㉛ⵝ", a_));
			IL_A1:
			XlsConditionalFormats formats = base.AppImplementation.ᜀ(this, A_0, A_1);
			this.ᜪ.Add(formats);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00042D50 File Offset: 0x00041D50
		public double GetColumnWidth(int columnIndex)
		{
			int a_ = 0;
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
						goto IL_5D;
					default:
						if (false)
						{
						}
						if (columnIndex > this.m_book.MaxColumnCount)
						{
							num = 2;
							continue;
						}
						goto IL_9C;
					}
					break;
				case 2:
					goto IL_9A;
				case 3:
					goto IL_5D;
				}
				if (columnIndex >= 1)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				break;
				IL_5D:
				num = 0;
			}
			IL_49:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唵圷嘹䤻匽⸿ୁ⩃≅ⵇ㉉汋ⵍㅏ㱑瑓㡕㝗⹙籛㱝՟䉡ࡣͥ᭧ᥩ䱫ᩭᡯ᝱ᩳ噵䥷", a_));
			IL_9A:
			goto IL_49;
			IL_9C:
			return this.ᜉ(columnIndex);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00042E04 File Offset: 0x00041E04
		internal double ᜉ(int A_0)
		{
			int a_ = 2;
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				spr\u216E spr_u216E;
				double num2;
				double result;
				switch (num)
				{
				case 0:
					goto IL_55;
				case 2:
					num = 3;
					continue;
				case 3:
					num2 = (double)spr_u216E.ᜉ() / 256.0;
					goto IL_DF;
				case 4:
					return result;
				case 5:
					if (spr_u216E != null)
					{
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_101;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 6:
					result = this.DefaultColumnWidth;
					num = 7;
					continue;
				case 7:
					return result;
				case 8:
					num2 = 0.0;
					goto IL_DF;
				case 9:
					if (!spr_u216E.ᜆ())
					{
						num = 2;
						continue;
					}
					goto IL_101;
				}
				if (A_0 < 1)
				{
					num = 0;
					continue;
				}
				base.ParseData();
				spr_u216E = this.ᜐ[A_0];
				num = 5;
				continue;
				IL_DF:
				result = num2;
				num = 4;
				continue;
				IL_101:
				num = 8;
			}
			IL_55:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷礹医刽㔿⽁⩃晅⭇⭉≋楍⑏牑㙓㍕硗㙙㥛ⵝ፟䉡ၣ๥൧ѩ䱫彭", a_));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00042F40 File Offset: 0x00041F40
		public int ColumnWidthToPixels(double widthInChars)
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
			double fileWidth = this.m_book.WidthToFileWidth(widthInChars);
			return (int)this.m_book.FileWidthToPixels(fileWidth);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00042F98 File Offset: 0x00041F98
		public double PixelsToColumnWidth(double pixels)
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
			return this.m_book.PixelsToWidth(pixels);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00042FE0 File Offset: 0x00041FE0
		internal int ᜈ(int A_0)
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
			double pixels = this.m_book.FileWidthToPixels((double)A_0 / 256.0);
			return (int)(this.m_book.PixelsToWidth(pixels) * 256.0);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0004304C File Offset: 0x0004204C
		internal int ᜊ(int A_0)
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
			return (int)(this.m_book.WidthToFileWidth((double)A_0 / 256.0) * 256.0);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000430AC File Offset: 0x000420AC
		private new void ᜀ(object A_0, NameIndexChangedEventArgs A_1)
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
			throw new NotImplementedException();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000430EC File Offset: 0x000420EC
		internal void ᜪ()
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
			this.\u170D(0);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00043130 File Offset: 0x00042130
		internal void \u170D(int A_0)
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
			throw new NotImplementedException();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00043170 File Offset: 0x00042170
		public void ParseAutoFilters()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜠ.ᜀ(this.\u171B);
					num = 4;
					continue;
				case 2:
					if (this.\u171B.Count > 0)
					{
						goto IL_8D;
					}
					goto IL_9A;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_75;
				}
				if (this.\u171B == null)
				{
					break;
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
					num = 3;
					continue;
				}
				IL_8D:
				num = 0;
			}
			IL_75:
			IL_9A:
			if (true)
			{
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00043220 File Offset: 0x00042220
		protected void ExtractPivotTables(int iStartIndex)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					return;
				case 2:
					if (this.ᜡ == null)
					{
						num = 3;
						continue;
					}
					goto IL_9F;
				case 3:
					this.ᜡ = new PivotTablesCollection((spr\u2158)base.ReservedHandle, this);
					num = 4;
					continue;
				case 4:
					goto IL_7F;
				}
				if (iStartIndex < 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				else
				{
					num = 2;
				}
			}
			return;
			IL_7F:
			IL_9F:
			this.ᜡ.Parse(this.ᜎ, iStartIndex);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000432E0 File Offset: 0x000422E0
		internal spr\u23A5 ᜁ(long A_0)
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
			return this.ᜏ.ᜄ(A_0);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00043328 File Offset: 0x00042328
		[CLSCompliant(false)]
		internal new spr\u23A5 ᜃ(int A_0, int A_1)
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
			return this.ᜏ.ᜄ(A_0, A_1);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00043370 File Offset: 0x00042370
		[CLSCompliant(false)]
		internal override int ParseNextRecord(sprἛ reader, int iBOFCounter, ExcelParseOptions options, bool bSkipStyles, Dictionary<int, int> hashNewXFormatIndexes, IDecryptor decryptor)
		{
			for (;;)
			{
				IL_7C:
				TBIFFRecord tbiffrecord = reader.ᜉ();
				for (;;)
				{
					IL_83:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 <= TBIFFRecord.Row)
							{
								num = 5;
								continue;
							}
							num = 17;
							continue;
						}
						case 1:
							num = 20;
							continue;
						case 2:
							num = 24;
							continue;
						case 3:
							if (true)
							{
							}
							if (iBOFCounter == 1)
							{
								num = 7;
								continue;
							}
							goto IL_30D;
						case 4:
							num = 19;
							continue;
						case 5:
							num = 26;
							continue;
						case 6:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 != TBIFFRecord.Formula)
							{
								num = 1;
								continue;
							}
							goto IL_C9;
						}
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_83;
							default:
							{
								if (false)
								{
								}
								TBIFFRecord tbiffrecord2 = tbiffrecord;
								num = 21;
								continue;
							}
							}
							break;
						case 8:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 == TBIFFRecord.RK)
							{
								num = 13;
								continue;
							}
							goto IL_220;
						}
						case 9:
							if (base.ReservedHandle.\u171C())
							{
								num = 14;
								continue;
							}
							goto IL_137;
						case 10:
							if (decryptor == null)
							{
								num = 16;
								continue;
							}
							goto IL_137;
						case 11:
							return iBOFCounter;
						case 12:
							num = 8;
							continue;
						case 13:
							goto IL_C9;
						case 14:
							num = 10;
							continue;
						case 15:
							num = 28;
							continue;
						case 16:
							num = 22;
							continue;
						case 17:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 != TBIFFRecord.Array)
							{
								num = 12;
								continue;
							}
							goto IL_C9;
						}
						case 18:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 != TBIFFRecord.RString)
							{
								num = 4;
								continue;
							}
							goto IL_C9;
						}
						case 19:
							goto IL_1E8;
						case 20:
						{
							TBIFFRecord tbiffrecord2;
							switch (tbiffrecord2)
							{
							case TBIFFRecord.MulRK:
							case TBIFFRecord.MulBlank:
								goto IL_C9;
							default:
								num = 27;
								continue;
							}
							break;
						}
						case 21:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 <= TBIFFRecord.RString)
							{
								num = 25;
								continue;
							}
							num = 0;
							continue;
						}
						case 22:
							if (!this.ᜏ.ᜀ(this.ᜭ, reader, bSkipStyles, hashNewXFormatIndexes))
							{
								num = 23;
								continue;
							}
							return iBOFCounter;
						case 23:
							goto IL_137;
						case 24:
							goto IL_2E2;
						case 25:
							num = 6;
							continue;
						case 26:
						{
							TBIFFRecord tbiffrecord2;
							if (tbiffrecord2 != TBIFFRecord.LabelSST)
							{
								num = 15;
								continue;
							}
							goto IL_C9;
						}
						case 27:
							num = 18;
							continue;
						case 28:
						{
							TBIFFRecord tbiffrecord2;
							switch (tbiffrecord2)
							{
							case TBIFFRecord.Blank:
							case TBIFFRecord.Number:
							case TBIFFRecord.Label:
							case TBIFFRecord.BoolErr:
							case TBIFFRecord.String:
							case TBIFFRecord.Row:
								goto IL_C9;
							case (TBIFFRecord)514:
							case (TBIFFRecord)518:
								goto IL_220;
							default:
								num = 2;
								continue;
							}
							break;
						}
						}
						goto IL_7C;
						IL_C9:
						num = 9;
						continue;
						IL_137:
						this.ᜏ.ᜀ(reader, bSkipStyles, hashNewXFormatIndexes, decryptor);
						num = 11;
					}
				}
			}
			return iBOFCounter;
			IL_1E8:
			IL_220:
			return base.ParseNextRecord(reader, iBOFCounter, options, bSkipStyles, hashNewXFormatIndexes, decryptor);
			IL_2E2:
			goto IL_220;
			IL_30D:
			return base.ParseNextRecord(reader, iBOFCounter, options, bSkipStyles, hashNewXFormatIndexes, decryptor);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0004369C File Offset: 0x0004269C
		[CLSCompliant(false)]
		internal override void ParseDimensions(spr\u203C dimensions)
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
			base.ParseDimensions(dimensions);
			this.ᜏ.Table.ᜁ(this.m_iLastRow);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000436F4 File Offset: 0x000426F4
		internal new void ᜂ(IXLSRange A_0)
		{
			int a_ = 5;
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_A1;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					if (A_0.Column != A_0.LastColumn)
					{
						num = 0;
						continue;
					}
					goto IL_A3;
				case 3:
					num = 2;
					continue;
				}
				if (A_0.Row != A_0.LastRow)
				{
					break;
				}
				num = 3;
			}
			IL_6F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤺尼儾♀♂", a_));
			IL_A1:
			goto IL_6F;
			IL_A3:
			this.SplitCell = A_0;
			this.PaneFirstVisible = A_0;
			this.ᜁ();
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000437B8 File Offset: 0x000427B8
		private void ᜁ()
		{
			switch (0)
			{
			default:
			{
				Dictionary<int, object> dictionary;
				for (;;)
				{
					int selectionCount = this.SelectionCount;
					dictionary = new Dictionary<int, object>();
					int num = this.\u1716.Count - 1;
					int num2 = 4;
					for (;;)
					{
						int num4;
						switch (num2)
						{
						case 0:
						{
							int a_ = 0;
							int num3 = this.\u1716.Count;
							num2 = 7;
							continue;
						}
						case 1:
						{
							int num3;
							if (num3 >= selectionCount)
							{
								num2 = 2;
								continue;
							}
							if (true)
							{
							}
							spr\u21A4 spr_u21A = (spr\u21A4)spr\u175E.ᜀ(TBIFFRecord.Selection);
							int a_;
							byte b;
							spr_u21A.ᜀ(b = (byte)this.ᜀ(a_, dictionary));
							a_ = (int)b;
							this.\u1716.Add(spr_u21A);
							num3++;
							num2 = 10;
							continue;
						}
						case 2:
							num4 = this.\u1716.Count - selectionCount;
							num2 = 6;
							continue;
						case 3:
							goto IL_DC;
						case 4:
							goto IL_DC;
						case 5:
							goto IL_FD;
						case 6:
							if (num4 > 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_1B6;
						case 7:
							goto IL_73;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_FD;
							default:
								goto IL_12D;
							}
							break;
						case 9:
							if (num < 0)
							{
								num2 = 0;
								continue;
							}
							dictionary[(int)this.\u1716[num].ᜀ()] = null;
							num--;
							num2 = 3;
							continue;
						case 10:
							goto IL_73;
						}
						break;
						IL_73:
						num2 = 1;
						continue;
						IL_DC:
						num2 = 9;
						continue;
						IL_FD:
						this.\u1716.RemoveRange(selectionCount, num4);
						num2 = 8;
					}
				}
				IL_12D:
				if (false)
				{
				}
				IL_1B6:
				this.ᜀ(dictionary);
				return;
			}
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00043984 File Offset: 0x00042984
		private new void ᜀ(Dictionary<int, object> A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 0;
					int num3 = 24;
					for (;;)
					{
						int num4;
						int count;
						Dictionary<int, object> dictionary;
						List<int> list;
						int num5;
						int count2;
						switch (num3)
						{
						case 0:
							goto IL_D6;
						case 1:
							if (num != 0)
							{
								num3 = 22;
								continue;
							}
							num3 = 15;
							continue;
						case 2:
							if (num4 < count)
							{
								num3 = 10;
								continue;
							}
							goto IL_138;
						case 3:
							return;
						case 4:
							goto IL_15D;
						case 5:
							if (!dictionary.ContainsKey((int)this.\u1717.ᜆ()))
							{
								num3 = 23;
								continue;
							}
							return;
						case 6:
							if (this.\u1717 != null)
							{
								num3 = 17;
								continue;
							}
							return;
						case 7:
							this.ᜀ(dictionary, list, A_0, 0);
							this.ᜀ(dictionary, list, A_0, 1);
							this.ᜀ(dictionary, list, A_0, 2);
							this.ᜀ(dictionary, list, A_0, 3);
							num3 = 18;
							continue;
						case 8:
						{
							spr\u21A4 spr_u21A;
							spr_u21A.ᜀ((byte)list[num5]);
							num5++;
							num3 = 27;
							continue;
						}
						case 9:
							goto IL_D6;
						case 10:
							num3 = 16;
							continue;
						case 11:
						{
							int key;
							if (!dictionary.ContainsKey(key))
							{
								num3 = 8;
								continue;
							}
							goto IL_B7;
						}
						case 12:
							num = this.\u1717.ᜃ();
							num2 = this.\u1717.ᜄ();
							num3 = 4;
							continue;
						case 13:
							goto IL_340;
						case 14:
							this.ᜀ(dictionary, list, A_0, 3);
							this.ᜀ(dictionary, list, A_0, 2);
							num3 = 25;
							continue;
						case 15:
							if (num2 != 0)
							{
								num3 = 14;
								continue;
							}
							this.ᜀ(dictionary, list, A_0, 3);
							num3 = 9;
							continue;
						case 16:
						{
							if (num5 >= count2)
							{
								goto IL_23D;
							}
							spr\u21A4 spr_u21A = this.\u1716[num4];
							int key = (int)spr_u21A.ᜀ();
							num3 = 11;
							continue;
						}
						case 17:
							num3 = 5;
							continue;
						case 18:
							goto IL_D6;
						case 19:
							goto IL_138;
						case 20:
							if (true)
							{
							}
							goto IL_340;
						case 21:
							num3 = 26;
							continue;
						case 22:
							this.ᜀ(dictionary, list, A_0, 3);
							this.ᜀ(dictionary, list, A_0, 1);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_23D;
							default:
								if (false)
								{
								}
								num3 = 0;
								continue;
							}
							break;
						case 23:
							this.\u1717.ᜀ(3);
							num3 = 3;
							continue;
						case 24:
							if (this.\u1717 != null)
							{
								num3 = 12;
								continue;
							}
							goto IL_15D;
						case 25:
							goto IL_D6;
						case 26:
							if (num2 != 0)
							{
								num3 = 7;
								continue;
							}
							goto IL_208;
						case 27:
							goto IL_B7;
						case 28:
							if (num != 0)
							{
								num3 = 21;
								continue;
							}
							goto IL_208;
						}
						break;
						IL_B7:
						num4++;
						num3 = 20;
						continue;
						IL_D6:
						num4 = 0;
						num5 = 0;
						count = this.\u1716.Count;
						count2 = list.Count;
						num3 = 13;
						continue;
						IL_138:
						num3 = 6;
						continue;
						IL_15D:
						list = new List<int>();
						dictionary = new Dictionary<int, object>();
						num3 = 28;
						continue;
						IL_208:
						num3 = 1;
						continue;
						IL_23D:
						num3 = 19;
						continue;
						IL_340:
						num3 = 2;
					}
				}
				return;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00043D3C File Offset: 0x00042D3C
		private new void ᜀ(Dictionary<int, object> A_0, List<int> A_1, Dictionary<int, object> A_2, int A_3)
		{
			for (;;)
			{
				A_0.Add(A_3, null);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						A_1.Add(A_3);
						num = 0;
						continue;
					case 2:
						if (A_2.ContainsKey(A_3))
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00043DC4 File Offset: 0x00042DC4
		private new int ᜀ(int A_0, Dictionary<int, object> A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_61;
				case 3:
					if (!A_1.ContainsKey(A_0))
					{
						goto IL_59;
					}
					if (true)
					{
					}
					A_0++;
					num = 0;
					continue;
				}
				IL_48:
				num = 3;
				continue;
				goto IL_48;
				IL_59:
				num = 2;
			}
			IL_61:
			A_1[A_0] = null;
			return A_0;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00043E54 File Offset: 0x00042E54
		public void Clear()
		{
			for (;;)
			{
				base.ParseData();
				base.ClearAll(WorksheetCopyType.CopyAll);
				this.ClearData();
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7C;
					case 1:
						if (this.ᜢ != null)
						{
							num = 4;
							continue;
						}
						goto IL_7C;
					case 2:
						this.\u1714.ᜃ();
						num = 5;
						continue;
					case 3:
						if (this.\u1714 != null)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						this.ᜢ.Clear();
						num = 0;
						continue;
					case 5:
						return;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜏ.Clear();
							break;
						}
						num = 8;
						continue;
					case 7:
						if (this.ᜏ != null)
						{
							num = 6;
							continue;
						}
						goto IL_10B;
					case 8:
						goto IL_10B;
					}
					break;
					IL_7C:
					this.m_iFirstColumn = int.MaxValue;
					this.m_iLastColumn = int.MaxValue;
					this.m_iFirstRow = -1;
					this.m_iLastRow = -1;
					num = 3;
					continue;
					IL_10B:
					if (true)
					{
					}
					this.ᜪ.Clear();
					this.ᜎ = null;
					this.ᜐ = new spr\u216E[this.m_book.MaxColumnCount + 2];
					num = 1;
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00043FD0 File Offset: 0x00042FD0
		internal void ᜧ()
		{
			for (;;)
			{
				base.ParseData();
				base.ClearAll(WorksheetCopyType.CopyAll);
				this.ClearData();
				int num = 9;
				for (;;)
				{
					IEnumerator<INamedRange> enumerator;
					switch (num)
					{
					case 0:
						if (this.ᜠ != null)
						{
							num = 3;
							continue;
						}
						goto IL_15F;
					case 1:
						this.ᜏ.Clear();
						num = 16;
						continue;
					case 2:
						goto IL_2B4;
					case 3:
						this.ᜠ.ᜄ();
						num = 15;
						continue;
					case 4:
						goto IL_CE;
					case 5:
						if (this.ᜢ != null)
						{
							num = 14;
							continue;
						}
						goto IL_2B4;
					case 6:
						if (this.\u1714 != null)
						{
							num = 12;
							continue;
						}
						goto IL_32D;
					case 7:
						if (this.ᝃ != null)
						{
							num = 10;
							continue;
						}
						goto IL_97;
					case 8:
						if (this.\u171F != null)
						{
							num = 17;
							continue;
						}
						return;
					case 9:
						if (this.ᜏ != null)
						{
							num = 1;
							continue;
						}
						goto IL_F4;
					case 10:
						this.ᝃ.Clear();
						num = 21;
						continue;
					case 11:
						return;
					case 12:
						this.\u1714.ᜃ();
						num = 13;
						continue;
					case 13:
						goto IL_32D;
					case 14:
						this.ᜢ.Clear();
						num = 2;
						continue;
					case 15:
						goto IL_15F;
					case 16:
						if (true)
						{
						}
						goto IL_F4;
					case 17:
						this.\u171F.Clear();
						num = 11;
						continue;
					case 18:
						if (this.ᜐ != null)
						{
							num = 19;
							continue;
						}
						goto IL_CE;
					case 19:
						this.ᜐ = null;
						num = 4;
						continue;
					case 20:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 4;
									continue;
								case 1:
									goto IL_26B;
								case 2:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									XlsName xlsName = (XlsName)enumerator.Current;
									xlsName.Record.ClearData();
									num = 1;
									continue;
								}
								case 4:
									goto IL_275;
								}
								goto IL_214;
								IL_232:
								num = 2;
								continue;
								IL_214:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_26B:
									goto IL_232;
								default:
									if (false)
									{
									}
									goto IL_232;
								}
							}
							IL_275:
							goto IL_1CA;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									enumerator.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_2B1;
								}
								if (enumerator == null)
								{
									break;
								}
								num = 1;
							}
							IL_2B1:;
						}
						goto IL_2B4;
						IL_1CA:
						num = 8;
						continue;
					case 21:
						goto IL_97;
					}
					break;
					IL_97:
					enumerator = this.\u1718.GetEnumerator();
					num = 20;
					continue;
					IL_CE:
					num = 5;
					continue;
					IL_F4:
					this.ᜪ.Clear();
					this.ᜎ = null;
					num = 18;
					continue;
					IL_15F:
					this.m_iFirstColumn = int.MaxValue;
					this.m_iLastColumn = int.MaxValue;
					this.m_iFirstRow = -1;
					this.m_iLastRow = -1;
					num = 6;
					continue;
					IL_2B4:
					num = 0;
					continue;
					IL_32D:
					num = 7;
				}
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00044340 File Offset: 0x00043340
		public void ClearData()
		{
			for (;;)
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
					base.ParseData();
					break;
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜏ != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						this.ᜏ.ClearData();
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000443C4 File Offset: 0x000433C4
		public bool CheckExistence(int row, int column)
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
			base.ParseData();
			long key = sprṔ.ᜀ(column, row);
			return this.ᜏ.Contains(key);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0004441C File Offset: 0x0004341C
		internal IXLSRanges ᜮ()
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
			return base.AppImplementation.ᜈ(this);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00044464 File Offset: 0x00043464
		public void CreateNamedRanges(string namedRange, string referRange, bool vertical)
		{
			int a_ = 16;
			switch (0)
			{
			default:
				for (;;)
				{
					IXLSRanges ixlsranges = (this as XlsWorksheet).ᜮ();
					int num = 0;
					for (;;)
					{
						INameRanges names;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
						{
							if (!vertical)
							{
								num = 4;
								continue;
							}
							int num2 = ((IWorksheet)this)[referRange].Column;
							num = 11;
							continue;
						}
						case 1:
							try
							{
								IEnumerator enumerator = ((IWorksheet)this)[namedRange].GetEnumerator();
								try
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
										{
											if (!enumerator.MoveNext())
											{
												num = 4;
												continue;
											}
											IXLSRange ixlsrange = (IXLSRange)enumerator.Current;
											INamedRange namedRange2 = names.Add(ixlsrange.Text);
											namedRange2.RefersToRange = ixlsranges[num3];
											num3++;
											num = 1;
											continue;
										}
										case 3:
											goto IL_1E9;
										case 4:
											num = 3;
											continue;
										}
										IL_1C0:
										num = 0;
										continue;
										goto IL_1C0;
									}
									IL_1E9:;
								}
								finally
								{
									for (;;)
									{
										IDisposable disposable = enumerator as IDisposable;
										num = 2;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_231;
											case 1:
												disposable.Dispose();
												num = 0;
												continue;
											case 2:
												if (disposable != null)
												{
													num = 1;
													continue;
												}
												goto IL_233;
											}
											break;
										}
									}
									IL_231:
									IL_233:;
								}
								return;
							}
							catch (Exception)
							{
								throw new sprṁ(RecordTableEnumerator.b("ࡅ⥇❉⥋⩍ɏ㍑㩓ㅕ㵗穙㵛そџ䉡cݥᱧ୩䱫൭Ὧݱᩳɵ塷᝹ᕻൽ", a_));
							}
							goto IL_24E;
						case 2:
							goto IL_269;
						case 3:
							goto IL_2AB;
						case 4:
							goto IL_24E;
						case 5:
							goto IL_7B;
						case 6:
							if (num4 >= ((IWorksheet)this)[referRange].LastRow + 1)
							{
								num = 10;
								continue;
							}
							(ixlsranges as XlsRangesCollection).Add(((IWorksheet)this)[num4, ((IWorksheet)this)[referRange].Column, num4, ((IWorksheet)this)[referRange].LastColumn]);
							num4++;
							num = 8;
							continue;
						case 7:
							goto IL_7B;
						case 8:
							goto IL_2AB;
						case 9:
						{
							int num2;
							if (num2 >= ((IWorksheet)this)[referRange].LastColumn + 1)
							{
								num = 5;
								continue;
							}
							if (true)
							{
							}
							(ixlsranges as XlsRangesCollection).Add(((IWorksheet)this)[((IWorksheet)this)[referRange].Row, num2, ((IWorksheet)this)[referRange].LastRow, num2]);
							num2++;
							num = 2;
							continue;
						}
						case 10:
							goto IL_29A;
						case 11:
							goto IL_269;
						}
						break;
						IL_7B:
						num3 = 0;
						names = (this as XlsWorksheet).Names;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_29A:
							num = 7;
							continue;
						}
						if (false)
						{
						}
						num = 1;
						continue;
						IL_24E:
						num4 = ((IWorksheet)this)[referRange].Row;
						num = 3;
						continue;
						IL_269:
						num = 9;
						continue;
						IL_2AB:
						num = 6;
					}
				}
				return;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00044784 File Offset: 0x00043784
		protected internal IMarkersDesigner CreateTemplateMarkersProcessor()
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
			return base.AppImplementation.ᜇ(this);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000447CC File Offset: 0x000437CC
		protected internal void ShowColumn(int columnIndex, bool visible)
		{
			int a_ = 0;
			spr\u216E spr_u216E;
			for (;;)
			{
				base.ParseData();
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.SetColumnWidth(columnIndex, this.DefaultColumnWidth);
						num = 7;
						continue;
					case 1:
						num = 5;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FB;
						default:
							if (false)
							{
							}
							if (visible)
							{
								num = 1;
								continue;
							}
							goto IL_1A7;
						}
						break;
					case 3:
						num = 10;
						continue;
					case 4:
						if (true)
						{
						}
						spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
						spr_u216E.ᜄ((ushort)(columnIndex - 1));
						spr_u216E.ᜀ((ushort)(columnIndex - 1));
						spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
						this.ᜐ[columnIndex] = spr_u216E;
						num = 8;
						continue;
					case 5:
						if (spr_u216E.ᜉ() == 0)
						{
							num = 0;
							continue;
						}
						goto IL_1A7;
					case 6:
						if (spr_u216E == null)
						{
							num = 4;
							continue;
						}
						num = 2;
						continue;
					case 7:
						goto IL_135;
					case 8:
						goto IL_EC;
					case 9:
						if (columnIndex >= 0)
						{
							num = 3;
							continue;
						}
						goto IL_FB;
					case 10:
						if (columnIndex > this.m_book.MaxColumnCount)
						{
							num = 11;
							continue;
						}
						spr_u216E = this.ᜐ[columnIndex];
						num = 6;
						continue;
					case 11:
						goto IL_15B;
					}
					break;
				}
			}
			IL_EC:
			goto IL_1A7;
			IL_FB:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唵圷嘹䤻匽⸿ୁ⩃≅ⵇ㉉", a_), RecordTableEnumerator.b("怵夷嘹䤻嬽怿⅁╃⡅♇╉㡋湍㉏㝑瑓㩕㵗⥙⽛繝ᑟ੡գࡥ䡧婩䱫཭ṯᙱ味ᅵ੷όᵻ੽ꒃ꺍ꊏꞑꆓ", a_));
			IL_135:
			goto IL_1A7;
			IL_15B:
			goto IL_FB;
			IL_1A7:
			spr_u216E.ᜄ(!visible);
			this.ᜀ();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00044990 File Offset: 0x00043990
		public void ShowColumn(int columnIndex)
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
			this.ShowColumn(columnIndex, true);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000449D4 File Offset: 0x000439D4
		public void HideColumn(int columnIndex)
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
			this.ShowColumn(columnIndex, false);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00044A18 File Offset: 0x00043A18
		internal new void ᜂ(int A_0, bool A_1)
		{
			int a_ = 7;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_9A;
				case 2:
					if (A_0 > this.m_book.MaxRowCount)
					{
						num = 1;
						continue;
					}
					goto IL_9C;
				case 3:
					num = 2;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9A;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0 < 1)
					{
						goto IL_65;
					}
					num = 3;
					break;
				}
			}
			IL_65:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("似倾㙀ੂ⭄⍆ⱈ㍊", a_));
			IL_9A:
			goto IL_65;
			IL_9C:
			sprᱧ sprᱧ = sprᜑ.ᜀ(this, A_0 - 1, true);
			sprᱧ.ᜅ(!A_1);
			this.ᜀ();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00044ADC File Offset: 0x00043ADC
		public void ShowRow(int rowIndex)
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
			this.ᜂ(rowIndex, true);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00044B20 File Offset: 0x00043B20
		public void HideRow(int rowIndex)
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
			this.ᜂ(rowIndex, false);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00044B64 File Offset: 0x00043B64
		private new void ᜀ()
		{
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					((XlsShape)base.Shapes[num2]).\u1718();
					num = 7;
					continue;
				case 3:
					if (num2 >= base.Shapes.Count)
					{
						num = 0;
						continue;
					}
					num = 8;
					continue;
				case 4:
					goto IL_84;
				case 5:
					return;
				case 6:
					goto IL_57;
				case 7:
					goto IL_4B;
				case 8:
					if (!((XlsShape)base.Shapes[num2]).IsSizeWithCell)
					{
						num = 1;
						continue;
					}
					goto IL_4B;
				}
				if (base.Shapes.Count == 0)
				{
					num = 5;
					continue;
				}
				num2 = 0;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_57;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_4B:
				num2++;
				num = 6;
				continue;
				IL_84:
				num = 3;
				continue;
				IL_57:
				goto IL_84;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00044C80 File Offset: 0x00043C80
		internal new void ᜀ(IXLSRange A_0, bool A_1)
		{
			int a_ = 15;
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = false;
					bool flag2 = false;
					int num = 32;
					for (;;)
					{
						int num3;
						int num5;
						int lastRow;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
						{
							spr\u216E spr_u216E;
							if (spr_u216E.ᜉ() == 0)
							{
								num = 28;
								continue;
							}
							goto IL_17C;
						}
						case 2:
							goto IL_323;
						case 3:
							if (A_1)
							{
								num = 0;
								continue;
							}
							goto IL_17C;
						case 4:
							goto IL_17C;
						case 5:
							goto IL_156;
						case 6:
						{
							int num2;
							int lastColumn;
							if (num2 > lastColumn)
							{
								num = 27;
								continue;
							}
							spr\u216E spr_u216E = this.ᜐ[num2];
							num = 21;
							continue;
						}
						case 7:
							goto IL_217;
						case 8:
						{
							spr\u216E spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
							int num2;
							spr_u216E.ᜄ((ushort)(num2 - 1));
							int lastColumn;
							spr_u216E.ᜀ((ushort)(lastColumn - 1));
							spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
							this.ᜐ[num2] = spr_u216E;
							num = 4;
							continue;
						}
						case 9:
						{
							num3 = A_0.Row;
							int num4 = this.AllocatedRange.LastRow;
							flag2 = true;
							num = 2;
							continue;
						}
						case 10:
							num = 24;
							continue;
						case 11:
						{
							if (num5 > lastRow)
							{
								num = 23;
								continue;
							}
							sprᱧ sprᱧ = sprᜑ.ᜀ(this, num5 - 1, true);
							num = 17;
							continue;
						}
						case 12:
							goto IL_192;
						case 13:
							num = 20;
							continue;
						case 14:
							goto IL_17C;
						case 15:
						{
							if (A_0.Column > this.m_book.MaxColumnCount)
							{
								num = 7;
								continue;
							}
							int num2 = A_0.Column;
							int lastColumn = A_0.LastColumn;
							num = 12;
							continue;
						}
						case 16:
						{
							flag = true;
							num3 = 1;
							int num4 = A_0.Row - 1;
							num = 19;
							continue;
						}
						case 17:
						{
							sprᱧ sprᱧ;
							sprᱧ.ᜅ(flag2 ? A_1 : (!A_1));
							base.ParseData();
							num5++;
							num = 5;
							continue;
						}
						case 18:
							if (A_0.Column < 0)
							{
								goto IL_49D;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_339;
							default:
								if (false)
								{
								}
								num = 29;
								continue;
							}
							break;
						case 19:
						{
							int num4;
							if (num4 < this.AllocatedRange.LastRow)
							{
								num = 9;
								continue;
							}
							goto IL_323;
						}
						case 20:
							if (A_0.LastRow == this.m_book.MaxRowCount)
							{
								num = 10;
								continue;
							}
							goto IL_481;
						case 21:
						{
							spr\u216E spr_u216E;
							if (spr_u216E == null)
							{
								num = 8;
								continue;
							}
							num = 3;
							continue;
						}
						case 22:
						{
							if (A_0.Row > this.m_book.MaxRowCount)
							{
								num = 25;
								continue;
							}
							num3 = A_0.Row;
							int num4 = A_0.LastRow;
							num = 34;
							continue;
						}
						case 23:
							num = 18;
							continue;
						case 24:
							if (!A_1)
							{
								num = 16;
								continue;
							}
							goto IL_481;
						case 25:
							goto IL_3CD;
						case 26:
							goto IL_192;
						case 27:
							goto IL_1B3;
						case 28:
						{
							int num2;
							this.SetColumnWidth(num2, this.DefaultColumnWidth);
							num = 14;
							continue;
						}
						case 29:
							if (true)
							{
							}
							num = 15;
							continue;
						case 30:
							goto IL_339;
						case 31:
							goto IL_156;
						case 32:
							if (A_0.Row >= 1)
							{
								num = 35;
								continue;
							}
							goto IL_4E5;
						case 33:
						{
							spr\u216E spr_u216E;
							spr_u216E.ᜄ(flag ? A_1 : (!A_1));
							int num2;
							num2++;
							num = 26;
							continue;
						}
						case 34:
							if (A_0.LastRow - A_0.Row > this.m_book.MaxRowCount - (A_0.LastRow - A_0.Row))
							{
								num = 13;
								continue;
							}
							goto IL_481;
						case 35:
							num = 22;
							continue;
						}
						break;
						IL_156:
						num = 11;
						continue;
						IL_17C:
						num = 33;
						continue;
						IL_192:
						num = 6;
						continue;
						IL_323:
						this.IsZeroHeight = true;
						A_1 = true;
						num = 30;
						continue;
						IL_481:
						num5 = num3;
						lastRow = A_0.LastRow;
						num = 31;
						continue;
						IL_339:
						goto IL_481;
					}
				}
				IL_1B3:
				this.ᜀ();
				return;
				IL_217:
				goto IL_49D;
				IL_3CD:
				goto IL_4E5;
				IL_49D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ل⡆╈㹊⁌ⅎ", a_), RecordTableEnumerator.b("ፄ♆╈㹊⡌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢।ɦᩨᡪ䵬᭮ᥰቲ᭴坶䥸孺ᱼᅾꎂ歷뎒ﾖ붜궞钠隢", a_));
				IL_4E5:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᝄ⡆㹈", a_));
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0004518C File Offset: 0x0004418C
		internal new void ᜀ(RangesCollection A_0, bool A_1)
		{
			int num = 0;
			for (;;)
			{
				IEnumerator enumerator;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 1:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 1;
									continue;
								case 1:
									goto IL_C0;
								case 3:
								{
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									IXLSRange a_ = (IXLSRange)enumerator.Current;
									this.ᜀ(a_, A_1);
									num = 4;
									continue;
								}
								}
								IL_9E:
								num = 3;
								continue;
								goto IL_9E;
							}
							IL_C0:
							return;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_100;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_102;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_100:
							IL_102:;
						}
						goto IL_103;
					case 2:
						return;
					}
					break;
				}
				if (A_0.Count == 0)
				{
					num = 2;
					continue;
				}
				IL_103:
				enumerator = A_0.GetEnumerator();
				if (true)
				{
				}
				num = 1;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000452CC File Offset: 0x000442CC
		internal new void ᜀ(IXLSRange[] A_0, bool A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				RangesCollection rangesCollection;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_D3;
					case 1:
						goto IL_B5;
					case 2:
						goto IL_B5;
					case 4:
						return;
					case 5:
					{
						int num2;
						if (num2 >= A_0.Length)
						{
							num = 0;
							continue;
						}
						IXLSRange range = A_0[num2];
						rangesCollection.Add(range);
						num2++;
						num = 1;
						continue;
					}
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
						if (A_0.Length != 0)
						{
							rangesCollection = new RangesCollection((spr\u2158)base.ReservedHandle, this);
							int num2 = 0;
							num = 2;
							continue;
						}
						break;
					}
					num = 4;
					continue;
					IL_B5:
					num = 5;
				}
				return;
				IL_D3:
				this.ᜀ(rangesCollection, A_1);
				return;
			}
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000453B8 File Offset: 0x000443B8
		public bool IsColumnVisible(int columnIndex)
		{
			int a_ = 1;
			int num = 0;
			spr\u216E spr_u216E;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						if (spr_u216E == null)
						{
							num = 3;
							continue;
						}
						goto IL_DE;
					}
					break;
				case 2:
					if (columnIndex > this.m_book.MaxColumnCount)
					{
						goto IL_CF;
					}
					base.ParseData();
					spr_u216E = this.ᜐ[columnIndex];
					num = 1;
					continue;
				case 3:
					return true;
				case 4:
					goto IL_DA;
				case 5:
					num = 2;
					continue;
				}
				if (true)
				{
				}
				if (columnIndex >= 1)
				{
					num = 5;
					continue;
				}
				goto IL_91;
				IL_CF:
				num = 4;
			}
			return true;
			IL_91:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吶嘸场䠼刾⽀ੂ⭄⍆ⱈ㍊", a_), RecordTableEnumerator.b("愶堸场䠼娾慀⁂⑄⥆❈⑊㥌潎㍐㙒畔㭖㱘⡚⹜罞ᕠୢѤ०䥨孪䵬๮ὰᝲ啴ၶ୸Ṻᱼ୾ꖄ꾎ꎐꚒꂔ", a_));
			IL_DA:
			goto IL_91;
			IL_DE:
			return !spr_u216E.ᜆ();
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000454AC File Offset: 0x000444AC
		public bool IsRowVisible(int rowIndex)
		{
			int a_ = 11;
			int num = 2;
			sprᱧ sprᱧ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					return true;
				case 3:
					goto IL_BD;
				case 4:
					if (rowIndex > this.m_book.MaxRowCount)
					{
						goto IL_B2;
					}
					sprᱧ = sprᜑ.ᜀ(this, rowIndex - 1, false);
					num = 5;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						if (false)
						{
						}
						if (sprᱧ == null)
						{
							num = 1;
							continue;
						}
						goto IL_C9;
					}
					break;
				}
				if (rowIndex >= 1)
				{
					num = 0;
					continue;
				}
				goto IL_85;
				IL_B2:
				num = 3;
			}
			return true;
			IL_85:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍀ⱂ㉄ๆ❈⽊⡌㝎", a_));
			IL_BD:
			if (true)
			{
			}
			goto IL_85;
			IL_C9:
			return !sprᱧ.ᜅ();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0004558C File Offset: 0x0004458C
		public void InsertRow(int rowIndex)
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
			this.ᜄ(rowIndex, 1, InsertOptionsType.FormatDefault);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000455D0 File Offset: 0x000445D0
		public void InsertRow(int rowIndex, int rowCount)
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
			this.ᜄ(rowIndex, rowCount, InsertOptionsType.FormatDefault);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00045614 File Offset: 0x00044614
		internal new void ᜄ(int A_0, int A_1, InsertOptionsType A_2)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 25;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch (num)
						{
						case 0:
							num = 9;
							continue;
						case 1:
							if (this.ᜆ(A_0, A_1, A_2))
							{
								num = 0;
								continue;
							}
							goto IL_25E;
						case 2:
						{
							IXLSRange a_2 = this.AllocatedRange[A_0, this.m_iFirstColumn, this.m_iLastRow, this.m_iLastColumn];
							CopyRangeOptions a_3 = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.CopyConditionalFormats;
							IXLSRange a_4 = this.AllocatedRange[A_0 + A_1, this.m_iFirstColumn, this.m_iLastRow + A_1, this.m_iLastColumn];
							this.ᜀ(a_4, a_2, a_3, true);
							num = 6;
							continue;
						}
						case 3:
							if (this.m_iFirstColumn < this.m_book.MaxColumnCount)
							{
								num = 2;
								continue;
							}
							this.m_iLastRow += A_1;
							this.ᜏ.Table.ᜌ(A_0 - 1, A_1);
							num = 21;
							continue;
						case 4:
							if (this.\u1714 != null)
							{
								num = 15;
								continue;
							}
							goto IL_202;
						case 5:
							goto IL_202;
						case 6:
							goto IL_272;
						case 7:
							if (!flag)
							{
								num = 10;
								continue;
							}
							goto IL_38F;
						case 8:
							goto IL_2B5;
						case 9:
							if (!base.InnerShapes.ᜀ(A_0, A_1, true, this.m_book.MaxRowCount))
							{
								num = 16;
								continue;
							}
							num = 4;
							continue;
						case 10:
							this.m_iLastRow = A_0;
							goto IL_11C;
						case 11:
							if (A_0 > this.m_book.MaxRowCount)
							{
								num = 18;
								continue;
							}
							num = 1;
							continue;
						case 12:
							goto IL_38F;
						case 13:
							goto IL_192;
						case 14:
							num = 11;
							continue;
						case 15:
							this.\u1714.ᜁ(A_0, A_1);
							num = 5;
							continue;
						case 16:
							goto IL_163;
						case 17:
							this.ᜂ(A_0, A_1, A_2, true);
							num = 8;
							continue;
						case 18:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_11C;
							default:
								goto IL_31B;
							}
							break;
						case 19:
							if (flag)
							{
								num = 24;
								continue;
							}
							num = 20;
							continue;
						case 20:
							if (A_2 != InsertOptionsType.FormatDefault)
							{
								num = 17;
								continue;
							}
							goto IL_3C2;
						case 21:
							goto IL_272;
						case 22:
							if (!flag2)
							{
								num = 23;
								continue;
							}
							goto IL_272;
						case 23:
							num = 7;
							continue;
						case 24:
							this.ᜂ(A_0, A_1, A_2, true);
							this.ᜃ(A_0, A_1, A_2, true);
							this.ᜁ(A_0, A_1, A_2, true);
							num = 13;
							continue;
						case 25:
							if (A_0 >= 1)
							{
								num = 14;
								continue;
							}
							goto IL_197;
						}
						break;
						IL_11C:
						num = 12;
						continue;
						IL_202:
						this.m_book.InnerNamesColection.ᜁ(A_0, A_1, base.Name);
						flag = (A_0 <= this.m_iLastRow);
						flag2 = (A_0 + A_1 >= this.m_book.MaxRowCount);
						num = 22;
						continue;
						IL_272:
						num = 19;
						continue;
						IL_38F:
						num = 3;
					}
				}
				IL_163:
				goto IL_25E;
				IL_192:
				goto IL_3C2;
				IL_197:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䐵圷䴹画倽␿❁㱃", a_));
				IL_25E:
				throw new ArgumentException(RecordTableEnumerator.b("电夷吹刻儽㐿扁ⵃ⡅㭇⽉㹋㩍灏⁑㭓⅕", a_));
				IL_2B5:
				goto IL_3C2;
				IL_31B:
				if (false)
				{
				}
				goto IL_197;
				IL_3C2:
				if (true)
				{
				}
				base.InnerShapes.ᜀ(A_0, A_1, true, false);
				this.m_book.ᜀ(this, A_0, A_1, true, false);
				return;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00045A0C File Offset: 0x00044A0C
		public void InsertColumn(int columnIndex)
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
			this.ᜃ(columnIndex, 1, InsertOptionsType.FormatDefault);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00045A50 File Offset: 0x00044A50
		public void InsertColumn(int columnIndex, int columnCount)
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
			this.ᜃ(columnIndex, columnCount, InsertOptionsType.FormatDefault);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00045A94 File Offset: 0x00044A94
		internal new void ᜃ(int A_0, int A_1, InsertOptionsType A_2)
		{
			int a_ = 4;
			for (;;)
			{
				base.ParseData();
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						num = 10;
						continue;
					case 1:
						if (A_1 >= 1)
						{
							num = 20;
							continue;
						}
						goto IL_203;
					case 2:
						if (this.ᜅ(A_0, A_1, A_2))
						{
							num = 0;
							continue;
						}
						goto IL_2E6;
					case 3:
						goto IL_122;
					case 4:
						num = 23;
						continue;
					case 5:
						if (this.\u1714 != null)
						{
							num = 14;
							continue;
						}
						goto IL_162;
					case 6:
						if (A_1 <= this.m_book.MaxColumnCount)
						{
							num2 = this.m_iFirstColumn;
							num = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2BA;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 7:
						num = 18;
						continue;
					case 8:
						goto IL_1A1;
					case 9:
						goto IL_33D;
					case 10:
						if (!base.InnerShapes.ᜀ(A_0, A_1, false, this.m_book.MaxColumnCount))
						{
							num = 3;
							continue;
						}
						num = 1;
						continue;
					case 11:
						goto IL_162;
					case 12:
						if (A_0 >= 1)
						{
							num = 13;
							continue;
						}
						goto IL_146;
					case 13:
						goto IL_2BA;
					case 14:
						this.\u1714.ᜀ(A_0, A_1);
						num = 11;
						continue;
					case 15:
						if (A_0 > this.m_book.MaxColumnCount)
						{
							num = 16;
							continue;
						}
						num = 2;
						continue;
					case 16:
						goto IL_2E1;
					case 17:
						goto IL_1FE;
					case 18:
						if (A_0 >= num2)
						{
							num = 19;
							continue;
						}
						goto IL_1A1;
					case 19:
						num2 = A_0;
						num = 8;
						continue;
					case 20:
						num = 6;
						continue;
					case 21:
						num = 22;
						continue;
					case 22:
						if (this.m_iFirstRow > 0)
						{
							num = 4;
							continue;
						}
						goto IL_35F;
					case 23:
						if (this.m_iFirstRow <= this.m_book.MaxRowCount)
						{
							num = 7;
							continue;
						}
						goto IL_35F;
					case 24:
						if (A_0 <= this.m_iLastColumn)
						{
							num = 21;
							continue;
						}
						goto IL_35F;
					}
					break;
					IL_162:
					this.m_book.InnerNamesColection.ᜀ(A_0, A_1, base.Name);
					num = 24;
					continue;
					IL_1A1:
					IXLSRange a_2 = this.AllocatedRange[this.m_iFirstRow, num2, this.m_iLastRow, this.m_iLastColumn];
					CopyRangeOptions a_3 = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.CopyConditionalFormats;
					this.ᜀ(this.AllocatedRange[this.m_iFirstRow, num2 + A_1], a_2, a_3, false);
					this.ᜁ(A_0, A_1, A_2);
					this.ᜂ(A_0, A_1, A_2, false);
					num = 17;
					continue;
					IL_2BA:
					num = 15;
				}
			}
			IL_122:
			goto IL_2E6;
			IL_146:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹缻儽ⰿ㝁⥃⡅Ň⑉⡋⭍⡏", a_));
			IL_1FE:
			goto IL_35F;
			IL_203:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夹医刽㔿⽁⩃Յ❇㽉≋㩍", a_), RecordTableEnumerator.b("氹崻刽㔿❁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㙙㥛ⵝ፟䉡啣䙥१ѩ࡫乭ᝯqᅳ᝵౷ό๻幽ꢇ낏ﮓ歹뺝즟첡삣쎥킧", a_));
			IL_2E1:
			goto IL_146;
			IL_2E6:
			throw new ArgumentException(RecordTableEnumerator.b("礹崻倽朿㙁摃⽅♇㥉⥋㱍⑏牑㝓㥕㑗⽙ㅛそ", a_));
			IL_33D:
			goto IL_203;
			IL_35F:
			this.ᜂ(A_0, A_1, A_2, false);
			this.ᜃ(A_0, A_1, A_2, false);
			this.m_book.ᜀ(this, A_0, A_1, false, false);
			this.ᜁ(A_0, A_1, A_2, false);
			base.InnerShapes.ᜀ(A_0, A_1, false, false);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00045E40 File Offset: 0x00044E40
		private void ᜁ(int A_0, int A_1, InsertOptionsType A_2, bool A_3)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A6;
				case 1:
					goto IL_F2;
				case 3:
				{
					int a_ = this.m_book.MaxRowCount - A_0 + 1;
					int a_2 = this.m_book.MaxColumnCount - this.m_iFirstColumn + 1;
					this.ᜂ(A_0, this.m_iFirstColumn, a_, a_2, A_0 + A_1, this.m_iFirstColumn, this, true);
					num = 1;
					continue;
				}
				}
				if (A_3)
				{
					num = 3;
				}
				else
				{
					int a_ = this.m_book.MaxRowCount - this.m_iFirstRow + 1;
					int a_2 = this.m_book.MaxColumnCount - A_0 + 1;
					this.ᜂ(this.m_iFirstRow, A_0, a_, a_2, this.m_iFirstRow, A_0 + A_1, this, true);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A6;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
			}
			IL_A6:
			IL_F2:
			this.ᜀ(A_0, A_1, A_2, A_3);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00045F4C File Offset: 0x00044F4C
		private new void ᜀ(int A_0, int A_1, InsertOptionsType A_2, bool A_3)
		{
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num5;
					int a_3;
					int a_4;
					int num6;
					switch (num)
					{
					case 0:
						if (A_2 == InsertOptionsType.FormatDefault)
						{
							num = 7;
							continue;
						}
						if (true)
						{
						}
						num = 16;
						continue;
					case 1:
					{
						int a_ = A_0 - 1;
						int a_2 = 1;
						num = 6;
						continue;
					}
					case 2:
						goto IL_1A7;
					case 3:
						goto IL_1A7;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F7;
						default:
							if (false)
							{
							}
							goto IL_B4;
						}
						break;
					case 5:
						goto IL_B4;
					case 6:
						goto IL_1A7;
					case 7:
						goto IL_229;
					case 8:
						goto IL_14B;
					case 9:
						goto IL_1A7;
					case 10:
						num = 0;
						continue;
					case 11:
					{
						if (A_3)
						{
							num = 1;
							continue;
						}
						int a_ = 1;
						int a_2 = A_0 - 1;
						num = 3;
						continue;
					}
					case 12:
					{
						int a_ = A_0 + A_1;
						int a_2 = 1;
						num = 9;
						continue;
					}
					case 14:
						num2 = 1;
						num3 = 0;
						num4 = A_0;
						num5 = 1;
						a_3 = 1;
						a_4 = this.m_book.MaxColumnCount;
						num = 4;
						continue;
					case 15:
					{
						if (num6 >= A_1)
						{
							num = 18;
							continue;
						}
						int a_;
						int a_2;
						this.ᜂ(a_, a_2, a_3, a_4, num4, num5, this, false);
						num6++;
						num4 += num2;
						num5 += num3;
						goto IL_F7;
					}
					case 16:
					{
						if (A_3)
						{
							num = 12;
							continue;
						}
						int a_ = 1;
						int a_2 = A_0 + A_1;
						num = 2;
						continue;
					}
					case 17:
						goto IL_14B;
					case 18:
						return;
					case 19:
						if (A_2 != InsertOptionsType.FormatAsBefore)
						{
							num = 10;
							continue;
						}
						goto IL_229;
					}
					if (A_3)
					{
						num = 14;
						continue;
					}
					num2 = 0;
					num3 = 1;
					num4 = 1;
					num5 = A_0;
					a_3 = this.m_book.MaxRowCount;
					a_4 = 1;
					num = 5;
					continue;
					IL_B4:
					num = 19;
					continue;
					IL_F7:
					num = 8;
					continue;
					IL_14B:
					num = 15;
					continue;
					IL_1A7:
					num6 = 0;
					num = 17;
					continue;
					IL_229:
					num = 11;
				}
				return;
			}
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000461A8 File Offset: 0x000451A8
		public void DeleteRow(int index)
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
			this.DeleteRow(index, 1);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000461EC File Offset: 0x000451EC
		public void DeleteRow(int index, int count)
		{
			int a_ = 13;
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 11;
					for (;;)
					{
						int num3;
						int num2;
						int num4;
						int num6;
						int num7;
						int column;
						int lastColumn;
						switch (num)
						{
						case 0:
							num2 = num3;
							goto IL_147;
						case 1:
							num = 17;
							continue;
						case 2:
							if (this.\u1714 != null)
							{
								num = 12;
								continue;
							}
							goto IL_1FF;
						case 3:
							if (num4 > 0)
							{
								num = 10;
								continue;
							}
							return;
						case 4:
						{
							int num5;
							if (num5 > 0)
							{
								num = 24;
								continue;
							}
							goto IL_1FF;
						}
						case 5:
							num6 = num3;
							goto IL_33F;
						case 6:
							goto IL_1FF;
						case 7:
							goto IL_A6;
						case 8:
							return;
						case 9:
							if (true)
							{
							}
							num = 20;
							continue;
						case 10:
							this.ᜁ(true, num4);
							num = 8;
							continue;
						case 11:
							if (count < 0)
							{
								num = 7;
								continue;
							}
							num = 13;
							continue;
						case 12:
							this.\u1714.ᜃ(index, count);
							num = 6;
							continue;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_31C;
							default:
								if (false)
								{
								}
								if (index >= 1)
								{
									num = 18;
									continue;
								}
								goto IL_2BB;
							}
							break;
						case 14:
						{
							if (index > this.m_book.MaxRowCount - count + 1)
							{
								num = 19;
								continue;
							}
							sprủ sprủ = this.ᜏ.Table;
							int num5 = sprủ.ᜁ() + 1;
							num7 = sprủ.ᜇ() + 1;
							num3 = this.m_iFirstColumn;
							num = 16;
							continue;
						}
						case 15:
						{
							int num5 = index + 1;
							IXLSRange a_2 = this.AllocatedRange[num5, column, num7, lastColumn];
							IXLSRange a_3 = this.AllocatedRange[index, column];
							CopyRangeOptions a_4 = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.CopyConditionalFormats;
							new spr\u24D0(this, index, count);
							this.ᜀ(a_3, a_2, a_4, true);
							num = 23;
							continue;
						}
						case 16:
							if (num3 <= 0)
							{
								num = 1;
								continue;
							}
							num = 0;
							continue;
						case 17:
							num2 = 1;
							goto IL_147;
						case 18:
							num = 14;
							continue;
						case 19:
							goto IL_EA;
						case 20:
							num6 = 1;
							goto IL_33F;
						case 21:
						{
							int num5;
							if (num5 <= num7)
							{
								num = 15;
								continue;
							}
							goto IL_EF;
						}
						case 22:
							if (num3 <= 0)
							{
								num = 9;
								continue;
							}
							num = 5;
							continue;
						case 23:
							goto IL_31C;
						case 24:
						{
							XlsRange xlsRange = (XlsRange)this.AllocatedRange[index, column, index + count - 1, lastColumn];
							int num5 = index + count;
							Rectangle rectangle = Rectangle.FromLTRB(this.FirstColumn - 1, index - 1, base.LastColumn - 1, index + count - 2);
							this.ᜪ.Remove(new Rectangle[]
							{
								rectangle
							});
							num = 21;
							continue;
						}
						}
						break;
						IL_EF:
						num = 2;
						continue;
						IL_31C:
						goto IL_EF;
						IL_147:
						column = num2;
						num3 = this.m_iLastColumn;
						num = 22;
						continue;
						IL_1FF:
						this.m_book.InnerNamesColection.ᜀ(index, base.Name, count);
						this.m_book.ᜀ(this, index, count, true, true);
						base.InnerShapes.ᜀ(index, count, true, true);
						num4 = Math.Min(num7 - index + 1, count);
						num = 3;
						continue;
						IL_33F:
						lastColumn = num6;
						num = 4;
					}
				}
				IL_A6:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁂⩄㉆❈㽊", a_));
				IL_EA:
				IL_2BB:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅂ⩄う楈≊⍌⭎㑐⭒", a_));
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000465CC File Offset: 0x000455CC
		private new void ᜂ(int A_0, int A_1)
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
			sprᱧ sprᱧ = sprᜑ.ᜀ(this, A_0, true);
			sprᱧ a_ = sprᜑ.ᜀ(this, A_1, true);
			sprᱧ.ᜀ(a_);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00046620 File Offset: 0x00045620
		public void DeleteColumn(int index)
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
			this.DeleteColumn(index, 1);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00046664 File Offset: 0x00045664
		public void DeleteColumn(int index, int count)
		{
			int a_ = 10;
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseData();
					int num = 21;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.\u171F != null)
							{
								num = 22;
								continue;
							}
							goto IL_14D;
						case 1:
							if (count < 0)
							{
								num = 19;
								continue;
							}
							num = 16;
							continue;
						case 2:
							goto IL_14D;
						case 3:
						{
							XlsRange xlsRange;
							if (!xlsRange.AreFormulaArraysNotSeparated)
							{
								num = 7;
								continue;
							}
							Rectangle rectangle = Rectangle.FromLTRB(index - 1, this.FirstRow - 1, index + count - 2, this.LastRow - 1);
							Rectangle[] array = new Rectangle[]
							{
								rectangle
							};
							this.ᜪ.Remove(array);
							num = 0;
							continue;
						}
						case 4:
							return;
						case 5:
						{
							if (count == 0)
							{
								num = 4;
								continue;
							}
							int iFirstRow = this.m_iFirstRow;
							int iLastRow = this.m_iLastRow;
							int num2 = this.m_iFirstColumn;
							int iLastColumn = this.m_iLastColumn;
							num = 15;
							continue;
						}
						case 6:
							goto IL_3B4;
						case 7:
							goto IL_21A;
						case 8:
						{
							int iLastColumn;
							count = Math.Min(count, iLastColumn - index + 1);
							this.ᜀ(true, count);
							num = 18;
							continue;
						}
						case 9:
							if (index > this.m_book.MaxColumnCount)
							{
								num = 24;
								continue;
							}
							num = 1;
							continue;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_16E;
							default:
								if (false)
								{
								}
								count = this.m_book.MaxColumnCount - index;
								num = 6;
								continue;
							}
							break;
						case 11:
						{
							int num2 = index + count;
							num = 12;
							continue;
						}
						case 12:
						{
							int num2;
							int iLastColumn;
							if (num2 <= iLastColumn)
							{
								num = 25;
								continue;
							}
							goto IL_FD;
						}
						case 13:
						{
							int iFirstRow;
							int iLastRow;
							XlsRange xlsRange = (XlsRange)this.AllocatedRange[iFirstRow, index, iLastRow, index + count - 1];
							num = 3;
							continue;
						}
						case 14:
							goto IL_16E;
						case 15:
						{
							int iFirstRow;
							if (iFirstRow > 0)
							{
								num = 13;
								continue;
							}
							goto IL_16E;
						}
						case 16:
							if (index + count > this.m_book.MaxColumnCount)
							{
								num = 10;
								continue;
							}
							goto IL_3B4;
						case 17:
							num = 9;
							continue;
						case 18:
							goto IL_2AF;
						case 19:
							goto IL_14B;
						case 20:
							if (true)
							{
							}
							this.\u1714.ᜄ(index, count);
							num = 14;
							continue;
						case 21:
							if (index >= 1)
							{
								num = 17;
								continue;
							}
							goto IL_300;
						case 22:
						{
							Rectangle[] array;
							this.\u171F.Remove(array);
							num = 2;
							continue;
						}
						case 23:
							if (this.AllocatedRange.LastColumn >= index)
							{
								num = 8;
								continue;
							}
							return;
						case 24:
							goto IL_2DE;
						case 25:
						{
							int iFirstRow;
							int iLastRow;
							int num2;
							int iLastColumn;
							IXLSRange a_2 = this.AllocatedRange[iFirstRow, num2, iLastRow, iLastColumn];
							IXLSRange a_3 = this.AllocatedRange[iFirstRow, index];
							CopyRangeOptions a_4 = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.CopyConditionalFormats | CopyRangeOptions.CopyDataValidations;
							this.ᜀ(a_3, a_2, a_4, false);
							num = 27;
							continue;
						}
						case 26:
						{
							int iLastColumn;
							if (index < iLastColumn)
							{
								num = 11;
								continue;
							}
							goto IL_FD;
						}
						case 27:
							goto IL_FD;
						case 28:
							if (this.\u1714 != null)
							{
								num = 20;
								continue;
							}
							goto IL_16E;
						}
						break;
						IL_FD:
						num = 28;
						continue;
						IL_14D:
						num = 26;
						continue;
						IL_16E:
						this.m_book.InnerNamesColection.ᜁ(index, base.Name, count);
						this.m_book.ᜀ(this, index, count, false, true);
						this.ᜀ(index, count, InsertOptionsType.FormatDefault);
						base.InnerShapes.ᜀ(index, count, false, true);
						num = 23;
						continue;
						IL_3B4:
						num = 5;
					}
				}
				IL_14B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⌿ⵁㅃ⡅㱇", a_));
				IL_21A:
				throw new sprṁ();
				IL_2AF:
				return;
				IL_2DE:
				IL_300:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⌿ⵁ⡃㍅╇⑉汋❍㹏㙑ㅓ⹕", a_));
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00046AB8 File Offset: 0x00045AB8
		public int GetColumnWidthPixels(int columnIndex)
		{
			int a_ = 0;
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
						goto IL_37;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (columnIndex > this.m_book.MaxColumnCount)
						{
							num = 2;
							continue;
						}
						goto IL_9C;
					}
					break;
				case 2:
					goto IL_9A;
				case 3:
					num = 0;
					continue;
				}
				if (columnIndex < 1)
				{
					break;
				}
				num = 3;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唵圷嘹䤻匽⸿ୁ⩃≅ⵇ㉉", a_));
			IL_9A:
			goto IL_37;
			IL_9C:
			double widthInChars = this.ᜉ(columnIndex);
			return this.ColumnWidthToPixels(widthInChars);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00046B70 File Offset: 0x00045B70
		public int GetRowHeightPixels(int rowIndex)
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
			double rowHeight = this.GetRowHeight(rowIndex);
			return (int)spr\u17FF.ᜁ((double)((float)rowHeight), MeasureUnits.Point);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00046BC0 File Offset: 0x00045BC0
		public int InsertArray<T>(T[] objects, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 10;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					int iXFIndex;
					IXLSRange ixlsrange;
					switch (num)
					{
					case 1:
						if (!isVertical)
						{
							num = 21;
							continue;
						}
						ixlsrange = this.InnerGetCell(firstColumn, firstRow + num2, iXFIndex);
						num = 25;
						continue;
					case 2:
						if (firstColumn > this.m_book.MaxColumnCount)
						{
							num = 15;
							continue;
						}
						base.ParseData();
						num2 = 0;
						num = 8;
						continue;
					case 3:
						goto IL_19F;
					case 4:
						goto IL_DA;
					case 5:
						if (objects[num2].GetType() == typeof(string))
						{
							num = 10;
							continue;
						}
						goto IL_473;
					case 6:
					{
						int num3;
						if (num3 > 0)
						{
							num = 27;
							continue;
						}
						goto IL_4BF;
					}
					case 7:
						num = 13;
						continue;
					case 8:
					{
						if (isVertical)
						{
							num = 17;
							continue;
						}
						int num3 = Math.Min(firstColumn + objects.Length - 1, this.m_book.MaxColumnCount) - firstColumn + 1;
						num = 32;
						continue;
					}
					case 9:
						goto IL_4BF;
					case 10:
						num = 23;
						continue;
					case 11:
						goto IL_24C;
					case 12:
						this.IsStringsPreserved = true;
						num = 30;
						continue;
					case 13:
						if (firstRow > this.m_book.MaxRowCount)
						{
							num = 3;
							continue;
						}
						num = 28;
						continue;
					case 14:
						num = 2;
						continue;
					case 15:
						goto IL_13D;
					case 16:
						return num2;
					case 17:
					{
						int num3 = Math.Min(firstRow + objects.Length - 1, this.m_book.MaxRowCount) - firstRow + 1;
						num = 22;
						continue;
					}
					case 18:
						if (objects[num2].GetType() == typeof(string))
						{
							num = 36;
							continue;
						}
						goto IL_FC;
					case 19:
						goto IL_3E6;
					case 20:
						goto IL_24C;
					case 21:
						ixlsrange = this.InnerGetCell(firstColumn + num2, firstRow, iXFIndex);
						num = 31;
						continue;
					case 22:
						goto IL_3B9;
					case 23:
						if (!this.ᜀ(objects[num2]))
						{
							num = 12;
							continue;
						}
						goto IL_473;
					case 24:
						goto IL_1C8;
					case 25:
						goto IL_316;
					case 26:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 16;
							continue;
						}
						num = 1;
						continue;
					}
					case 27:
						ixlsrange = this.InnerGetCell(firstColumn, firstRow);
						num = 18;
						continue;
					case 28:
						goto IL_2BD;
					case 29:
						goto IL_3E6;
					case 30:
						goto IL_1C8;
					case 31:
						goto IL_316;
					case 32:
						goto IL_3B9;
					case 33:
						if (!this.ᜀ(objects[num2]))
						{
							num = 34;
							continue;
						}
						goto IL_FC;
					case 34:
						this.IsStringsPreserved = true;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2BD;
						default:
							if (false)
							{
							}
							num = 29;
							continue;
						}
						break;
					case 35:
						if (firstRow >= 1)
						{
							num = 7;
							continue;
						}
						goto IL_2D5;
					case 36:
						num = 33;
						continue;
					}
					if (objects == null)
					{
						num = 4;
						continue;
					}
					num = 35;
					continue;
					IL_FC:
					this.IsStringsPreserved = false;
					num = 19;
					continue;
					IL_1C8:
					ixlsrange.Value2 = objects[num2];
					num2++;
					num = 11;
					continue;
					IL_24C:
					num = 26;
					continue;
					IL_2BD:
					if (firstColumn >= 1)
					{
						num = 14;
						continue;
					}
					goto IL_220;
					IL_316:
					num = 5;
					continue;
					IL_3B9:
					iXFIndex = this.m_book.DefaultXFIndex;
					num = 6;
					continue;
					IL_3E6:
					ixlsrange.Value2 = objects[num2];
					XlsRange xlsRange = (XlsRange)ixlsrange;
					iXFIndex = (int)xlsRange.ExtendedFormatIndex;
					num = 9;
					continue;
					IL_473:
					this.IsStringsPreserved = false;
					num = 24;
					continue;
					IL_4BF:
					num2 = 1;
					num = 20;
				}
				IL_DA:
				throw new ArgumentNullException(RecordTableEnumerator.b("⼿⁁⹃⍅⭇㹉㽋", a_));
				IL_13D:
				goto IL_220;
				IL_19F:
				goto IL_2D5;
				IL_220:
				throw new ArgumentNullException(RecordTableEnumerator.b("☿⭁㙃㕅㱇ॉ⍋≍╏㽑㩓", a_));
				IL_2D5:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("☿⭁㙃㕅㱇ᡉ⍋㥍", a_));
			}
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000470A0 File Offset: 0x000460A0
		private new bool ᜀ(object A_0)
		{
			int a_ = 7;
			if (A_0.ToString().StartsWith(RecordTableEnumerator.b("<", a_)))
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
					break;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00047104 File Offset: 0x00046104
		public int InsertArray(object[] arrObject, int firstRow, int firstColumn, bool isVertical)
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
			return this.InsertArray<object>(arrObject, firstRow, firstColumn, isVertical);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0004714C File Offset: 0x0004614C
		public int InsertArray(string[] stringArray, int firstRow, int firstColumn, bool isVertical)
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
			return this.InsertArray<string>(stringArray, firstRow, firstColumn, isVertical);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00047194 File Offset: 0x00046194
		public int InsertArray(int[] intArray, int firstRow, int firstColumn, bool isVertical)
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
			return this.InsertArray<int>(intArray, firstRow, firstColumn, isVertical);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000471DC File Offset: 0x000461DC
		public int InsertArray(double[] doubleArray, int firstRow, int firstColumn, bool isVertical)
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
			return this.InsertArray<double>(doubleArray, firstRow, firstColumn, isVertical);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00047224 File Offset: 0x00046224
		public int InsertArray(DateTime[] dateTimeArray, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 15;
				for (;;)
				{
					int num3;
					IXLSRange ixlsrange;
					int iXFIndex;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 > 0)
						{
							num = 12;
							continue;
						}
						goto IL_198;
					}
					case 1:
						return num3;
					case 2:
						goto IL_198;
					case 3:
						goto IL_1CF;
					case 4:
						ixlsrange = this.InnerGetCell(firstColumn, firstRow);
						num = 21;
						continue;
					case 5:
						goto IL_1AB;
					case 6:
						goto IL_1A6;
					case 7:
					{
						if (isVertical)
						{
							num = 26;
							continue;
						}
						int num2 = Math.Min(firstColumn + dateTimeArray.Length - 1, this.m_book.MaxColumnCount) - firstColumn + 1;
						num = 28;
						continue;
					}
					case 8:
						num = 18;
						continue;
					case 9:
						if (!isVertical)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						ixlsrange = this.InnerGetCell(firstColumn, firstRow);
						num = 14;
						continue;
					case 10:
						goto IL_1AB;
					case 11:
						goto IL_222;
					case 12:
						num = 9;
						continue;
					case 13:
						num = 17;
						continue;
					case 14:
						goto IL_B4;
					case 16:
						goto IL_AF;
					case 17:
						if (firstRow > this.m_book.MaxRowCount)
						{
							num = 25;
							continue;
						}
						num = 22;
						continue;
					case 18:
						if (firstColumn > this.m_book.MaxColumnCount)
						{
							num = 11;
							continue;
						}
						base.ParseData();
						this.IsStringsPreserved = false;
						num3 = 0;
						num = 7;
						continue;
					case 19:
					{
						int num2;
						if (num3 >= num2)
						{
							num = 1;
							continue;
						}
						num = 27;
						continue;
					}
					case 20:
						ixlsrange = this.InnerGetCell(firstColumn + num3, firstRow, iXFIndex);
						num = 10;
						continue;
					case 21:
						goto IL_B4;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1A6;
						default:
							if (false)
							{
							}
							if (firstColumn >= 1)
							{
								num = 8;
								continue;
							}
							goto IL_2C0;
						}
						break;
					case 23:
						if (firstRow >= 1)
						{
							num = 13;
							continue;
						}
						goto IL_2AC;
					case 24:
						goto IL_244;
					case 25:
						goto IL_193;
					case 26:
					{
						int num2 = Math.Min(firstRow + dateTimeArray.Length - 1, this.m_book.MaxRowCount) - firstRow + 1;
						num = 3;
						continue;
					}
					case 27:
						if (!isVertical)
						{
							num = 20;
							continue;
						}
						ixlsrange = this.InnerGetCell(firstColumn, firstRow + num3, iXFIndex);
						num = 5;
						continue;
					case 28:
						goto IL_1CF;
					}
					if (dateTimeArray == null)
					{
						num = 16;
						continue;
					}
					num = 23;
					continue;
					IL_B4:
					ixlsrange.DateTimeValue = dateTimeArray[num3];
					XlsRange xlsRange = (XlsRange)ixlsrange;
					iXFIndex = (int)xlsRange.ExtendedFormatIndex;
					num = 2;
					continue;
					IL_198:
					num3 = 1;
					num = 6;
					continue;
					IL_1AB:
					ixlsrange.DateTimeValue = dateTimeArray[num3];
					num3++;
					num = 24;
					continue;
					IL_1CF:
					iXFIndex = this.m_book.DefaultXFIndex;
					num = 0;
					continue;
					IL_244:
					num = 19;
					continue;
					IL_1A6:
					goto IL_244;
				}
				IL_AF:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⱇ⭉㡋⭍я㭑㥓㍕ᥗ⡙⹛㽝ᥟ", a_));
				IL_193:
				goto IL_2AC;
				IL_222:
				goto IL_2C0;
				IL_2AC:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹇⍉㹋㵍⑏Q㭓⅕", a_));
				IL_2C0:
				throw new ArgumentNullException(RecordTableEnumerator.b("⹇⍉㹋㵍⑏ᅑ㭓㩕ⵗ㝙㉛", a_));
			}
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000475E8 File Offset: 0x000465E8
		public int InsertArray(object[,] objectArray, int firstRow, int firstColumn)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int[] array;
					int num4;
					int num5;
					int num6;
					IXLSRange ixlsrange;
					switch (num)
					{
					case 0:
						this.IsStringsPreserved = true;
						num = 9;
						continue;
					case 1:
						goto IL_F3;
					case 2:
						goto IL_21F;
					case 3:
						goto IL_186;
					case 4:
					{
						if (firstColumn > this.m_book.MaxColumnCount)
						{
							num = 31;
							continue;
						}
						base.ParseData();
						int num2 = Math.Min(firstRow + objectArray.GetLength(0) - 1, this.m_book.MaxRowCount) - firstRow + 1;
						int num3 = Math.Min(firstColumn + objectArray.GetLength(1) - 1, this.m_book.MaxColumnCount) - firstColumn + 1;
						array = new int[num3];
						num = 22;
						continue;
					}
					case 5:
						goto IL_1F8;
					case 6:
						this.IsStringsPreserved = true;
						num = 5;
						continue;
					case 7:
						if (firstRow >= 1)
						{
							num = 12;
							continue;
						}
						goto IL_15E;
					case 9:
						goto IL_1AB;
					case 10:
						if (!this.ᜀ(objectArray[num4, num5]))
						{
							num = 6;
							continue;
						}
						goto IL_146;
					case 11:
						num = 15;
						continue;
					case 12:
						goto IL_43A;
					case 13:
					{
						int num3;
						if (num6 >= num3)
						{
							num = 28;
							continue;
						}
						if (true)
						{
						}
						ixlsrange = this.InnerGetCell(num6 + firstColumn, firstRow);
						num = 29;
						continue;
					}
					case 14:
						if (objectArray[num4, num5].GetType() == typeof(string))
						{
							num = 24;
							continue;
						}
						goto IL_146;
					case 15:
						if (!this.ᜀ(objectArray[0, num6]))
						{
							num = 0;
							continue;
						}
						goto IL_47F;
					case 16:
						num6 = 0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43A;
						default:
							if (false)
							{
							}
							num = 35;
							continue;
						}
						break;
					case 17:
						goto IL_21F;
					case 18:
						goto IL_1F8;
					case 19:
						goto IL_186;
					case 20:
					{
						int num2;
						if (num4 >= num2)
						{
							num = 36;
							continue;
						}
						num5 = 0;
						num = 3;
						continue;
					}
					case 21:
						num = 27;
						continue;
					case 22:
					{
						int num3;
						if (num3 > 0)
						{
							num = 21;
							continue;
						}
						return 0;
					}
					case 23:
						goto IL_463;
					case 24:
						num = 10;
						continue;
					case 25:
						num = 4;
						continue;
					case 26:
						num4++;
						num = 2;
						continue;
					case 27:
					{
						int num2;
						if (num2 > 0)
						{
							num = 16;
							continue;
						}
						return 0;
					}
					case 28:
						num = 37;
						continue;
					case 29:
						if (objectArray[0, num6].GetType() == typeof(string))
						{
							num = 11;
							continue;
						}
						goto IL_47F;
					case 30:
						goto IL_DA;
					case 31:
						goto IL_141;
					case 32:
						goto IL_1AB;
					case 33:
						if (firstColumn >= 1)
						{
							num = 25;
							continue;
						}
						goto IL_1E4;
					case 34:
					{
						int num3;
						if (num5 >= num3)
						{
							num = 26;
							continue;
						}
						ixlsrange = this.InnerGetCell(firstColumn + num5, num4 + firstRow, array[num5]);
						num = 14;
						continue;
					}
					case 35:
						goto IL_F3;
					case 36:
						return num4;
					case 37:
						num4 = 1;
						num = 17;
						continue;
					case 38:
						if (firstRow > this.m_book.MaxRowCount)
						{
							num = 23;
							continue;
						}
						num = 33;
						continue;
					}
					if (objectArray == null)
					{
						num = 30;
						continue;
					}
					num = 7;
					continue;
					IL_F3:
					num = 13;
					continue;
					IL_146:
					this.IsStringsPreserved = false;
					num = 18;
					continue;
					IL_186:
					num = 34;
					continue;
					IL_1AB:
					ixlsrange.Value2 = objectArray[0, num6];
					XlsRange xlsRange = (XlsRange)ixlsrange;
					array[num6] = (int)xlsRange.ExtendedFormatIndex;
					num6++;
					num = 1;
					continue;
					IL_1F8:
					ixlsrange.Value2 = objectArray[num4, num5];
					num5++;
					num = 19;
					continue;
					IL_21F:
					num = 20;
					continue;
					IL_43A:
					num = 38;
					continue;
					IL_47F:
					this.IsStringsPreserved = false;
					num = 32;
				}
				IL_DA:
				throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾เ⅂⽄≆⩈㽊", a_));
				IL_141:
				goto IL_1E4;
				IL_15E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崺吼䴾㉀㝂ᝄ⡆㹈", a_));
				IL_1E4:
				throw new ArgumentNullException(RecordTableEnumerator.b("崺吼䴾㉀㝂ل⡆╈㹊⁌ⅎ", a_));
				IL_463:
				goto IL_15E;
			}
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00047B00 File Offset: 0x00046B00
		public int InsertArrayList(ArrayList arrayList, int firstRow, int firstColumn, bool isVertical)
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
			return this.InsertArray(arrayList.ToArray(), firstRow, firstColumn, isVertical);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00047B4C File Offset: 0x00046B4C
		public int InsertDataTable(DataTable dataTable, bool columnHeaders, int firstRow, int firstColumn)
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
			return this.InsertDataTable(dataTable, columnHeaders, firstRow, firstColumn, -1, -1);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00047B94 File Offset: 0x00046B94
		public int InsertDataTable(DataTable dataTable, bool columnHeaders, int firstRow, int firstColumn, bool transTypes)
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
			return this.InsertDataTable(dataTable, columnHeaders, firstRow, firstColumn, -1, -1, transTypes);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00047BD4 File Offset: 0x00046BD4
		public int InsertDataTable(DataTable dataTable, bool columnHeaders, int firstRow, int firstColumn, int maxRows, int maxColumns)
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
			return this.InsertDataTable(dataTable, columnHeaders, firstRow, firstColumn, maxRows, maxColumns, null, false);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00047C20 File Offset: 0x00046C20
		public int InsertDataTable(DataTable dataTable, bool columnHeaders, int firstRow, int firstColumn, int maxRows, int maxColumns, bool transTypes)
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
			return this.InsertDataTable(dataTable, columnHeaders, firstRow, firstColumn, maxRows, maxColumns, null, transTypes);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00047C70 File Offset: 0x00046C70
		public int InsertDataTable(DataTable dataTable, bool columnHeaders, int firstRow, int firstColumn, int maxRows, int maxColumns, DataColumn[] columnsArray, bool transTypes)
		{
			int a_ = 8;
			int num = 12;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (maxColumns > num2)
					{
						num = 24;
						continue;
					}
					goto IL_42E;
				case 1:
					firstRow++;
					num = 14;
					continue;
				case 2:
					num = 33;
					continue;
				case 3:
					this.ᜁ(maxRows);
					if (true)
					{
					}
					num = 6;
					continue;
				case 4:
					goto IL_261;
				case 5:
					goto IL_41B;
				case 6:
					goto IL_CE;
				case 7:
					if (this.\u173D)
					{
						num = 3;
						continue;
					}
					goto IL_CE;
				case 8:
					if (firstRow > this.m_book.MaxRowCount)
					{
						num = 9;
						continue;
					}
					num = 27;
					continue;
				case 9:
					goto IL_189;
				case 10:
					if (columnsArray.Length == 0)
					{
						num = 4;
						continue;
					}
					goto IL_364;
				case 11:
					goto IL_13A;
				case 13:
					num = 8;
					continue;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DF;
					default:
						if (false)
						{
						}
						goto IL_3F9;
					}
					break;
				case 15:
					if (firstRow >= 1)
					{
						num = 13;
						continue;
					}
					goto IL_2C8;
				case 16:
					if (maxRows >= 0)
					{
						num = 29;
						continue;
					}
					goto IL_41B;
				case 17:
					goto IL_25C;
				case 18:
					goto IL_1D4;
				case 19:
					goto IL_13F;
				case 20:
					if (columnHeaders)
					{
						goto IL_DF;
					}
					goto IL_3F9;
				case 21:
					goto IL_C9;
				case 22:
					if (!transTypes)
					{
						num = 31;
						continue;
					}
					this.ᜀ(dataTable, firstRow, firstColumn, maxRows, maxColumns, columnsArray);
					num = 17;
					continue;
				case 23:
					if (maxRows > num2)
					{
						num = 5;
						continue;
					}
					goto IL_1D4;
				case 24:
					goto IL_390;
				case 25:
				{
					int num3 = 0;
					num = 19;
					continue;
				}
				case 26:
				{
					int num3;
					if (num3 >= maxColumns)
					{
						num = 1;
						continue;
					}
					this.AllocatedRange[firstRow, firstColumn + num3].Text = columnsArray[num3].Caption;
					num3++;
					num = 30;
					continue;
				}
				case 27:
					if (firstColumn >= 1)
					{
						num = 2;
						continue;
					}
					goto IL_1FC;
				case 28:
					if (maxColumns >= 0)
					{
						num = 34;
						continue;
					}
					goto IL_390;
				case 29:
					num = 23;
					continue;
				case 30:
					goto IL_13F;
				case 31:
					this.ᜀ(dataTable, firstRow, firstColumn, maxRows, maxColumns, columnsArray, this.\u173D);
					num = 38;
					continue;
				case 32:
					goto IL_364;
				case 33:
				{
					if (firstColumn > this.m_book.MaxColumnCount)
					{
						num = 11;
						continue;
					}
					base.ParseData();
					int num3 = 0;
					num = 36;
					continue;
				}
				case 34:
					num = 0;
					continue;
				case 35:
					goto IL_42E;
				case 36:
					if (columnsArray != null)
					{
						num = 37;
						continue;
					}
					goto IL_261;
				case 37:
					num = 10;
					continue;
				case 38:
					goto IL_1CF;
				}
				if (dataTable == null)
				{
					num = 21;
					continue;
				}
				num = 15;
				continue;
				IL_CE:
				num = 20;
				continue;
				IL_DF:
				num = 25;
				continue;
				IL_13F:
				num = 26;
				continue;
				IL_1D4:
				num2 = columnsArray.Length;
				num = 28;
				continue;
				IL_261:
				columnsArray = new DataColumn[dataTable.Columns.Count];
				dataTable.Columns.CopyTo(columnsArray, 0);
				num = 32;
				continue;
				IL_364:
				num2 = dataTable.Rows.Count;
				num = 16;
				continue;
				IL_390:
				maxColumns = num2;
				num = 35;
				continue;
				IL_3F9:
				num = 22;
				continue;
				IL_41B:
				maxRows = num2;
				num = 18;
				continue;
				IL_42E:
				maxColumns = Math.Min(maxColumns, this.m_book.MaxColumnCount - firstColumn + 1);
				maxRows = Math.Min(maxRows, this.m_book.MaxRowCount - firstRow);
				num = 7;
			}
			IL_C9:
			throw new ArgumentNullException(RecordTableEnumerator.b("娽ℿ㙁╃ቅ⥇⡉⁋⭍", a_));
			IL_13A:
			goto IL_1FC;
			IL_189:
			goto IL_2C8;
			IL_1CF:
			goto IL_483;
			IL_1FC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("堽⤿ぁ㝃㉅େ╉⁋㭍㵏㱑", a_));
			IL_25C:
			goto IL_483;
			IL_2C8:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("堽⤿ぁ㝃㉅ᩇ╉㭋", a_));
			IL_483:
			this.m_book.MaxImportRows = 0;
			return maxRows;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00048110 File Offset: 0x00047110
		private void ᜁ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.m_book.MaxImportRows = A_0;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (A_0.ToString().Length < 6)
							{
								num = 13;
								continue;
							}
							num = 8;
							continue;
						case 1:
							goto IL_183;
						case 2:
							return;
						case 3:
							if (true)
							{
							}
							num = 16;
							continue;
						case 4:
						{
							int num2;
							int num3;
							if (num2 >= num3)
							{
								num = 12;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_90;
							default:
								if (false)
								{
								}
								this.m_book.MaxImportRows /= 10;
								num2++;
								num = 1;
								continue;
							}
							break;
						}
						case 5:
							if (A_0 != this.m_book.MaxRowCount - 1)
							{
								num = 10;
								continue;
							}
							goto IL_128;
						case 6:
							goto IL_230;
						case 7:
							if (this.m_book.MaxImportRows > 200000)
							{
								num = 15;
								continue;
							}
							return;
						case 8:
							if (A_0.ToString().Length > 6)
							{
								num = 17;
								continue;
							}
							goto IL_128;
						case 9:
							goto IL_230;
						case 10:
							goto IL_90;
						case 11:
						{
							int num4;
							int num5;
							if (num4 >= num5)
							{
								num = 3;
								continue;
							}
							this.m_book.MaxImportRows *= 10;
							num4++;
							num = 9;
							continue;
						}
						case 12:
							goto IL_128;
						case 13:
						{
							int num5 = 6 - A_0.ToString().Length;
							int num4 = 0;
							num = 6;
							continue;
						}
						case 14:
							goto IL_183;
						case 15:
							this.m_book.MaxImportRows = 200000;
							num = 2;
							continue;
						case 16:
							goto IL_128;
						case 17:
						{
							int num3 = A_0.ToString().Length - 6;
							int num2 = 0;
							num = 14;
							continue;
						}
						}
						break;
						IL_90:
						num = 0;
						continue;
						IL_128:
						num = 7;
						continue;
						IL_183:
						num = 4;
						continue;
						IL_230:
						num = 11;
					}
				}
				return;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00048370 File Offset: 0x00047370
		public int InsertDataColumn(DataColumn dataColumn, bool columnHeaders, int firstRow, int firstColumn)
		{
			int a_ = 0;
			if (dataColumn == null)
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
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("刵夷丹崻紽⼿⹁ㅃ⭅♇", a_));
				}
			}
			return this.InsertDataColumns(new DataColumn[]
			{
				dataColumn
			}, columnHeaders, firstRow, firstColumn);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000483E4 File Offset: 0x000473E4
		public int InsertDataColumns(DataColumn[] dataColumns, bool columnHeaders, int firstRow, int firstColumn)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8D;
				case 2:
					goto IL_3C;
				case 3:
					if (dataColumns.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_A3;
				}
				if (true)
				{
				}
				if (dataColumns == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_3C:
			goto IL_8F;
			IL_8D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋ്㽏㹑⅓㭕㙗⥙", a_));
			default:
				if (false)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("≅⥇㹉ⵋ്㽏㹑⅓㭕㙗⥙籛㵝şౡ䑣ࡥݧṩ䱫౭ᕯ剱ᅳ᭵ࡷ๹ջ", a_));
			}
			IL_A3:
			return this.InsertDataTable(dataColumns[0].Table, columnHeaders, firstRow, firstColumn, -1, -1, dataColumns, false);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000484AC File Offset: 0x000474AC
		public int InsertDataView(DataView dataView, bool columnHeaders, int firstRow, int firstColumn)
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
			return this.InsertDataView(dataView, columnHeaders, firstRow, firstColumn, false);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000484F4 File Offset: 0x000474F4
		public int InsertDataView(DataView dataView, bool columnHeaders, int firstRow, int firstColumn, bool transTypes)
		{
			int a_ = 17;
			if (dataView == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("⍆⡈㽊ⱌ᥎㡐㙒≔", a_));
				}
			}
			return this.InsertDataView(dataView, columnHeaders, firstRow, firstColumn, dataView.Count, dataView.Table.Columns.Count, transTypes);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00048574 File Offset: 0x00047574
		public int InsertDataView(DataView dataView, bool columnHeaders, int firstRow, int firstColumn, int maxRows, int maxColumns)
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
			return this.InsertDataView(dataView, columnHeaders, firstRow, firstColumn, maxRows, maxColumns, false);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000485B4 File Offset: 0x000475B4
		public int InsertDataView(DataView dataView, bool columnHeaders, int firstRow, int firstColumn, int maxRows, int maxColumns, bool transTypes)
		{
			int a_ = 15;
			int num = 12;
			for (;;)
			{
				DataColumnCollection columns;
				int count;
				switch (num)
				{
				case 0:
					if (firstColumn >= 1)
					{
						num = 13;
						continue;
					}
					goto IL_2B5;
				case 1:
					if (maxRows >= 0)
					{
						num = 20;
						continue;
					}
					goto IL_362;
				case 2:
					goto IL_197;
				case 3:
				{
					int num2;
					if (num2 >= maxColumns)
					{
						num = 8;
						continue;
					}
					this.AllocatedRange[firstRow, firstColumn + num2].Value2 = columns[num2].Caption;
					num2++;
					num = 18;
					continue;
				}
				case 4:
					if (columnHeaders)
					{
						num = 17;
						continue;
					}
					goto IL_1FD;
				case 5:
					this.ᜁ(dataView, firstRow, firstColumn, maxRows, maxColumns);
					num = 16;
					continue;
				case 6:
					if (firstRow > this.m_book.MaxRowCount)
					{
						num = 24;
						continue;
					}
					num = 0;
					continue;
				case 7:
					if (firstRow >= 1)
					{
						num = 23;
						continue;
					}
					goto IL_2A1;
				case 8:
					firstRow++;
					num = 30;
					continue;
				case 9:
					goto IL_362;
				case 10:
					goto IL_24D;
				case 11:
					if (maxColumns > count)
					{
						num = 19;
						continue;
					}
					goto IL_328;
				case 13:
					goto IL_1D3;
				case 14:
					num = 11;
					continue;
				case 15:
				{
					if (firstColumn > this.m_book.MaxColumnCount)
					{
						num = 21;
						continue;
					}
					base.ParseData();
					int num2 = 0;
					columns = dataView.Table.Columns;
					count = dataView.Count;
					num = 1;
					continue;
				}
				case 16:
					goto IL_192;
				case 17:
				{
					int num2 = 0;
					num = 22;
					continue;
				}
				case 18:
					goto IL_305;
				case 19:
					goto IL_397;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D3;
					default:
						if (false)
						{
						}
						num = 29;
						continue;
					}
					break;
				case 21:
					goto IL_1F8;
				case 22:
					goto IL_305;
				case 23:
					num = 6;
					continue;
				case 24:
					goto IL_13F;
				case 25:
					goto IL_328;
				case 26:
					goto IL_A9;
				case 27:
					if (!transTypes)
					{
						num = 5;
						continue;
					}
					this.ᜀ(dataView, firstRow, firstColumn, maxRows, maxColumns);
					num = 10;
					continue;
				case 28:
					if (maxColumns >= 0)
					{
						if (true)
						{
						}
						num = 14;
						continue;
					}
					goto IL_397;
				case 29:
					if (maxRows > count)
					{
						num = 9;
						continue;
					}
					goto IL_197;
				case 30:
					goto IL_1FD;
				}
				if (dataView == null)
				{
					num = 26;
					continue;
				}
				num = 7;
				continue;
				IL_197:
				count = columns.Count;
				num = 28;
				continue;
				IL_1D3:
				num = 15;
				continue;
				IL_1FD:
				maxRows = Math.Min(maxRows, this.m_book.MaxRowCount - firstRow + 1);
				num = 27;
				continue;
				IL_305:
				num = 3;
				continue;
				IL_328:
				maxColumns = Math.Min(maxColumns, this.m_book.MaxColumnCount - firstColumn + 1);
				num = 4;
				continue;
				IL_362:
				maxRows = count;
				num = 2;
				continue;
				IL_397:
				maxColumns = count;
				num = 25;
			}
			IL_A9:
			throw new ArgumentNullException(RecordTableEnumerator.b("⅄♆㵈⩊ᭌ♎㑐⑒", a_));
			IL_13F:
			goto IL_2A1;
			IL_192:
			return maxRows;
			IL_1F8:
			goto IL_2B5;
			IL_24D:
			return maxRows;
			IL_2A1:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⍄⹆㭈㡊㥌ᵎ㹐⑒", a_));
			IL_2B5:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⍄⹆㭈㡊㥌౎㹐㽒⁔㩖㝘", a_));
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0004896C File Offset: 0x0004796C
		internal new DataTable ᜀ(int A_0, int A_1, int A_2, int A_3, ExportDataTableOptions A_4)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 14;
				for (;;)
				{
					DataTable dataTable;
					int num2;
					DataRow dataRow;
					int num3;
					CellExportType[] array;
					CellExportType cellExportType;
					bool a_2;
					bool flag3;
					CellExportType[] array2;
					int num4;
					DataColumn dataColumn;
					Type dataType;
					int num5;
					int num6;
					switch (num)
					{
					case 0:
						goto IL_10C;
					case 1:
						return dataTable;
					case 2:
						if (num2 >= A_3)
						{
							num = 6;
							continue;
						}
						num = 18;
						continue;
					case 3:
					{
						bool flag;
						A_2 = Math.Min(A_2 + (flag ? 1 : 0), this.m_book.MaxRowCount - A_0 + (flag ? 2 : 1));
						num = 7;
						continue;
					}
					case 4:
						num = 13;
						continue;
					case 5:
						goto IL_397;
					case 6:
						dataTable.Rows.Add(dataRow);
						num3++;
						num = 38;
						continue;
					case 7:
					{
						bool flag2;
						if (!flag2)
						{
							num = 4;
							continue;
						}
						num = 11;
						continue;
					}
					case 8:
						cellExportType = array[num2];
						goto IL_486;
					case 9:
					{
						if (A_1 > this.m_book.MaxColumnCount)
						{
							num = 23;
							continue;
						}
						base.ParseData();
						bool flag = (A_4 & ExportDataTableOptions.ColumnNames) != ExportDataTableOptions.None;
						a_2 = ((A_4 & ExportDataTableOptions.ComputedFormulaValues) != ExportDataTableOptions.None);
						bool flag2 = (A_4 & ExportDataTableOptions.DetectColumnTypes) != ExportDataTableOptions.None;
						bool a_3 = (A_4 & ExportDataTableOptions.DefaultStyleColumnTypes) != ExportDataTableOptions.None;
						flag3 = ((A_4 & ExportDataTableOptions.PreserveOleDate) != ExportDataTableOptions.None);
						dataTable = new DataTable(base.Name);
						A_3 = Math.Min(A_3, this.m_book.MaxColumnCount - A_1 + 1);
						num = 3;
						continue;
					}
					case 10:
					{
						bool flag;
						if (!flag)
						{
							num = 12;
							continue;
						}
						num = 40;
						continue;
					}
					case 11:
						array2 = new CellExportType[A_3];
						goto IL_25A;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4FB;
						default:
							if (false)
							{
							}
							num = 28;
							continue;
						}
						break;
					case 13:
						array2 = null;
						goto IL_25A;
					case 15:
						goto IL_4FD;
					case 16:
						if (num4 >= A_3)
						{
							num = 20;
							continue;
						}
						dataColumn = new DataColumn();
						dataType = typeof(string);
						num = 30;
						continue;
					case 17:
						goto IL_347;
					case 18:
						if (array == null)
						{
							num = 41;
							continue;
						}
						num = 8;
						continue;
					case 19:
						goto IL_4B8;
					case 20:
						A_0 = num5;
						num = 29;
						continue;
					case 21:
					{
						bool flag;
						if (flag)
						{
							num = 36;
							continue;
						}
						goto IL_397;
					}
					case 22:
					{
						bool a_3;
						CellFormatType a_4 = this.ᜀ(num5, A_1 + num4, a_3);
						CellExportType cellExportType2 = this.ᜀ(a_4, A_0, A_1 + num4, A_2, A_4);
						dataType = this.ᜀ(cellExportType2, flag3);
						array[num4] = cellExportType2;
						num = 17;
						continue;
					}
					case 23:
						goto IL_166;
					case 24:
						num = 9;
						continue;
					case 25:
						if (A_0 > this.m_book.MaxRowCount)
						{
							num = 0;
							continue;
						}
						num = 39;
						continue;
					case 26:
						goto IL_410;
					case 27:
						goto IL_3B9;
					case 28:
						num6 = A_0;
						goto IL_2B5;
					case 29:
					{
						bool flag;
						if (flag)
						{
							num = 33;
							continue;
						}
						goto IL_410;
					}
					case 30:
					{
						bool flag2;
						if (flag2)
						{
							num = 22;
							continue;
						}
						goto IL_347;
					}
					case 31:
						if (num3 >= A_2)
						{
							num = 1;
							continue;
						}
						dataRow = dataTable.NewRow();
						num2 = 0;
						num = 35;
						continue;
					case 32:
						goto IL_3B9;
					case 33:
						A_2--;
						num = 26;
						continue;
					case 34:
						cellExportType = CellExportType.Text;
						goto IL_486;
					case 35:
						goto IL_4B8;
					case 36:
						dataColumn.ColumnName = this.AllocatedRange[A_0, A_1 + num4].Value;
						num = 5;
						continue;
					case 37:
						num = 25;
						continue;
					case 38:
						goto IL_4FB;
					case 39:
						if (A_1 >= 1)
						{
							num = 24;
							continue;
						}
						goto IL_168;
					case 40:
						num6 = A_0 + 1;
						goto IL_2B5;
					case 41:
						if (true)
						{
						}
						num = 34;
						continue;
					}
					if (A_0 >= 1)
					{
						num = 37;
						continue;
					}
					break;
					IL_25A:
					array = array2;
					num = 10;
					continue;
					IL_2B5:
					num5 = num6;
					num4 = 0;
					num = 32;
					continue;
					IL_347:
					dataColumn.DataType = dataType;
					num = 21;
					continue;
					IL_397:
					dataTable.Columns.Add(dataColumn);
					num4++;
					num = 27;
					continue;
					IL_3B9:
					num = 16;
					continue;
					IL_410:
					num3 = 0;
					num = 15;
					continue;
					IL_486:
					CellExportType a_5 = cellExportType;
					dataRow[num2] = this.ᜀ(A_0 + num3, A_1 + num2, a_5, a_2, flag3);
					num2++;
					num = 19;
					continue;
					IL_4B8:
					num = 2;
					continue;
					IL_4FD:
					num = 31;
					continue;
					IL_4FB:
					goto IL_4FD;
				}
				IL_10C:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崺吼䴾㉀㝂ᝄ⡆㹈", a_));
				IL_166:
				IL_168:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崺吼䴾㉀㝂ل⡆╈㹊⁌ⅎ", a_));
			}
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00048E9C File Offset: 0x00047E9C
		internal new DataTable ᜀ(IXLSRange A_0, ExportDataTableOptions A_1)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int column;
				int row;
				int num3;
				for (;;)
				{
					IL_17:
					int num = 8;
					for (;;)
					{
						int num2;
						bool flag;
						switch (num)
						{
						case 0:
							if (column == 0)
							{
								num = 2;
								continue;
							}
							goto IL_134;
						case 1:
							goto IL_5D;
						case 2:
							goto IL_B9;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num2 = 0;
								goto IL_102;
							}
							break;
						case 4:
							num2 = 1;
							goto IL_102;
						case 5:
							if (row != 0)
							{
								num = 6;
								continue;
							}
							goto IL_8F;
						case 6:
							num = 0;
							continue;
						case 7:
							if (!flag)
							{
								num = 9;
								continue;
							}
							if (true)
							{
							}
							num = 3;
							continue;
						case 9:
							num = 4;
							continue;
						}
						if (A_0 == null)
						{
							num = 1;
							continue;
						}
						flag = ((A_1 & ExportDataTableOptions.ColumnNames) != ExportDataTableOptions.None);
						num = 7;
						continue;
						IL_102:
						num3 = num2;
						row = A_0.Row;
						column = A_0.Column;
						num = 5;
					}
				}
				IL_5D:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠹崻倽✿❁", a_));
				IL_8F:
				return null;
				IL_B9:
				goto IL_8F;
				IL_134:
				return this.ᜀ(row, column, A_0.LastRow - row + num3, A_0.LastColumn - column + 1, A_1);
			}
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00048FFC File Offset: 0x00047FFC
		public DataTable ExportDataTable()
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
			return this.ᜀ(this.AllocatedRange, ExportDataTableOptions.ColumnNames);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00049044 File Offset: 0x00048044
		public void RemovePanes()
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
			base.ParseData();
			base.WindowTwo.ᜊ(false);
			base.WindowTwo.ᜈ(false);
			this.\u1717 = null;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000490A4 File Offset: 0x000480A4
		protected internal IXLSRange IntersectRanges(IXLSRange range1, IXLSRange range2)
		{
			int a_ = 11;
			int num = 1;
			Rectangle left;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A4;
				case 2:
					goto IL_82;
				case 3:
				{
					if (range1.Parent != range2.Parent)
					{
						num = 2;
						continue;
					}
					Rectangle a = Rectangle.FromLTRB(range1.Column, range1.Row, range1.LastColumn, range1.LastRow);
					Rectangle b = Rectangle.FromLTRB(range2.Column, range2.Row, range2.LastColumn, range2.LastRow);
					left = Rectangle.Intersect(a, b);
					num = 6;
					continue;
				}
				case 4:
					goto IL_126;
				case 5:
					goto IL_60;
				case 6:
					if (left == Rectangle.Empty)
					{
						num = 4;
						continue;
					}
					goto IL_147;
				case 7:
					if (range1 == null)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
				}
				if (range1 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
				else
				{
					num = 7;
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀≂⭄⁆ⱈ穊", a_));
			IL_62:
			return null;
			IL_82:
			goto IL_62;
			IL_A4:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀≂⭄⁆ⱈ祊", a_));
			IL_126:
			if (true)
			{
			}
			return null;
			IL_147:
			return range1[left.Top, left.Left, left.Bottom, left.Right];
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0004921C File Offset: 0x0004821C
		protected internal IXLSRange MergeRanges(IXLSRange range1, IXLSRange range2)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_BE;
					case 1:
						if (range2.Column <= range1.LastColumn + 1)
						{
							num = 13;
							continue;
						}
						goto IL_42D;
					case 2:
					{
						if (range1.Parent != range2.Parent)
						{
							num = 12;
							continue;
						}
						int num2 = range1.LastColumn - range1.Column + 1;
						int num3 = range2.LastColumn - range2.Column + 1;
						int num4 = range1.LastRow - range1.Row + 1;
						int num5 = range2.LastRow - range2.Row + 1;
						num = 28;
						continue;
					}
					case 3:
					{
						IXLSRange ixlsrange = range1;
						range1 = range2;
						range2 = ixlsrange;
						num = 23;
						continue;
					}
					case 5:
					{
						int num2;
						int num3;
						if (num2 == num3)
						{
							num = 9;
							continue;
						}
						goto IL_C3;
					}
					case 6:
						if (range2.Row <= range1.LastRow + 1)
						{
							num = 8;
							continue;
						}
						goto IL_C3;
					case 7:
						goto IL_32A;
					case 8:
						goto IL_405;
					case 9:
						num = 18;
						continue;
					case 10:
						if (range2.Column >= range1.Column)
						{
							goto IL_265;
						}
						goto IL_42D;
					case 11:
						if (true)
						{
						}
						num = 27;
						continue;
					case 12:
						goto IL_301;
					case 13:
						goto IL_1B4;
					case 14:
						if (range2.Column < range1.Column)
						{
							num = 3;
							continue;
						}
						goto IL_248;
					case 15:
						num = 1;
						continue;
					case 16:
						goto IL_428;
					case 17:
					{
						int num4;
						int num5;
						if (num4 == num5)
						{
							num = 20;
							continue;
						}
						goto IL_42D;
					}
					case 18:
						if (range1.Column == range2.Column)
						{
							num = 29;
							continue;
						}
						goto IL_C3;
					case 19:
						goto IL_E7;
					case 20:
						num = 30;
						continue;
					case 21:
						if (range2.Row < range1.Row)
						{
							goto IL_C3;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_265;
						default:
							if (false)
							{
							}
							num = 26;
							continue;
						}
						break;
					case 22:
						num = 14;
						continue;
					case 23:
						goto IL_248;
					case 24:
						if (range2 == null)
						{
							num = 16;
							continue;
						}
						num = 2;
						continue;
					case 25:
						if (range2.Row < range1.Row)
						{
							num = 31;
							continue;
						}
						goto IL_E7;
					case 26:
						num = 6;
						continue;
					case 27:
					{
						int num4;
						int num5;
						if (num4 != num5)
						{
							num = 7;
							continue;
						}
						goto IL_2A0;
					}
					case 28:
					{
						int num2;
						int num3;
						if (num2 != num3)
						{
							num = 11;
							continue;
						}
						goto IL_2A0;
					}
					case 29:
						num = 25;
						continue;
					case 30:
						if (range1.Row == range2.Row)
						{
							num = 22;
							continue;
						}
						goto IL_42D;
					case 31:
					{
						IXLSRange ixlsrange = range1;
						range1 = range2;
						range2 = ixlsrange;
						num = 19;
						continue;
					}
					}
					if (range1 == null)
					{
						num = 0;
						continue;
					}
					num = 24;
					continue;
					IL_C3:
					num = 17;
					continue;
					IL_E7:
					num = 21;
					continue;
					IL_248:
					num = 10;
					continue;
					IL_265:
					num = 15;
					continue;
					IL_2A0:
					num = 5;
				}
				IL_BE:
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽焿", a_));
				IL_1B4:
				return range1[range1.Row, range1.Column, range1.LastRow, Math.Max(range1.LastColumn, range2.LastColumn)];
				IL_301:
				return null;
				IL_32A:
				return null;
				IL_405:
				return range1[range1.Row, range1.Column, Math.Max(range1.LastRow, range2.LastRow), range1.LastColumn];
				IL_428:
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽爿", a_));
				IL_42D:
				return null;
			}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00049658 File Offset: 0x00048658
		private new List<CellRange> ᜀ(string A_0)
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
			base.ParseData();
			Dictionary<int, object> stringIndexes = this.m_book.InnerSST.GetStringIndexes(A_0);
			List<CellRange> list = new List<CellRange>();
			list.AddRange(this.ᜀ(this.ᜏ.Find(stringIndexes)));
			return list;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000496CC File Offset: 0x000486CC
		public void Replace(string oldValue, string newValue)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					IL_3F:
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_79:
						num = 3;
						break;
					case 1:
						goto IL_5F;
					default:
						goto IL_5F;
					}
					List<CellRange> list;
					for (;;)
					{
						IL_18:
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							num2 = list.Count;
							goto IL_103;
						case 1:
							goto IL_C2;
						case 2:
						{
							if (num3 >= num4)
							{
								num = 7;
								continue;
							}
							IXLSRange ixlsrange = list[num3];
							string text = ixlsrange.Text.ToLower();
							oldValue = oldValue.ToLower();
							ixlsrange.Text = text.Replace(oldValue, newValue);
							num3++;
							num = 6;
							continue;
						}
						case 3:
							num = 5;
							continue;
						case 4:
							if (list == null)
							{
								goto IL_79;
							}
							num = 0;
							continue;
						case 5:
							num2 = 0;
							goto IL_103;
						case 6:
							goto IL_C2;
						case 7:
							return;
						}
						goto IL_3F;
						IL_C2:
						num = 2;
						continue;
						IL_103:
						num4 = num2;
						num3 = 0;
						num = 1;
					}
					IL_5F:
					if (false)
					{
					}
					list = this.ᜀ(oldValue);
					num = 4;
					goto IL_18;
				}
				return;
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000497F0 File Offset: 0x000487F0
		public void Replace(string oldValue, DateTime newValue)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num;
					List<CellRange> list;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_71:
						num = 5;
						break;
					default:
						if (false)
						{
						}
						list = this.ᜀ(oldValue);
						num = 7;
						break;
					}
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							num2 = list.Count;
							goto IL_E6;
						case 1:
							goto IL_A5;
						case 2:
							num2 = 0;
							goto IL_E6;
						case 3:
							goto IL_A5;
						case 4:
						{
							if (num3 >= num4)
							{
								num = 6;
								continue;
							}
							IXLSRange ixlsrange = list[num3];
							ixlsrange.DateTimeValue = newValue;
							num3++;
							if (true)
							{
							}
							num = 3;
							continue;
						}
						case 5:
							num = 2;
							continue;
						case 6:
							return;
						case 7:
							if (list == null)
							{
								goto IL_71;
							}
							num = 0;
							continue;
						}
						break;
						IL_A5:
						num = 4;
						continue;
						IL_E6:
						num4 = num2;
						num3 = 0;
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000498F4 File Offset: 0x000488F4
		public void Replace(string oldValue, double newValue)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_37:
					if (true)
					{
					}
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_79:
						num = 6;
						break;
					case 1:
						goto IL_5F;
					default:
						goto IL_5F;
					}
					List<CellRange> list;
					for (;;)
					{
						IL_10:
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							num2 = 0;
							goto IL_E6;
						case 1:
						{
							if (num3 >= num4)
							{
								num = 7;
								continue;
							}
							IXLSRange ixlsrange = list[num3];
							ixlsrange.NumberValue = newValue;
							num3++;
							num = 2;
							continue;
						}
						case 2:
							goto IL_A5;
						case 3:
							goto IL_A5;
						case 4:
							if (list == null)
							{
								goto IL_79;
							}
							num = 5;
							continue;
						case 5:
							num2 = list.Count;
							goto IL_E6;
						case 6:
							num = 0;
							continue;
						case 7:
							return;
						}
						goto IL_37;
						IL_A5:
						num = 1;
						continue;
						IL_E6:
						num4 = num2;
						num3 = 0;
						num = 3;
					}
					IL_5F:
					if (false)
					{
					}
					list = this.ᜀ(oldValue);
					num = 4;
					goto IL_10;
				}
				return;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000499F8 File Offset: 0x000489F8
		public void Replace(string oldValue, string[] newValues, bool isVertical)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num;
					List<CellRange> list;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_71:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						list = this.ᜀ(oldValue);
						num = 4;
						break;
					}
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							goto IL_9F;
						case 1:
							if (true)
							{
							}
							num = 2;
							continue;
						case 2:
							num2 = 0;
							goto IL_E8;
						case 3:
							num2 = list.Count;
							goto IL_E8;
						case 4:
							if (list == null)
							{
								goto IL_71;
							}
							num = 3;
							continue;
						case 5:
							return;
						case 6:
						{
							if (num3 >= num4)
							{
								num = 5;
								continue;
							}
							XlsRange xlsRange = list[num3];
							xlsRange.Replace(oldValue, newValues, isVertical);
							num3++;
							num = 0;
							continue;
						}
						case 7:
							goto IL_9F;
						}
						break;
						IL_9F:
						num = 6;
						continue;
						IL_E8:
						num4 = num2;
						num3 = 0;
						num = 7;
					}
				}
				return;
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00049B00 File Offset: 0x00048B00
		public void Replace(string oldValue, int[] newValues, bool isVertical)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					IL_3F:
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_79:
						num = 6;
						break;
					case 1:
						goto IL_5F;
					default:
						goto IL_5F;
					}
					List<CellRange> list;
					for (;;)
					{
						IL_18:
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
						{
							if (num2 >= num3)
							{
								num = 5;
								continue;
							}
							XlsRange xlsRange = list[num2];
							xlsRange.Replace(oldValue, newValues, isVertical);
							num2++;
							num = 2;
							continue;
						}
						case 1:
							goto IL_A7;
						case 2:
							goto IL_A7;
						case 3:
							num4 = list.Count;
							goto IL_E8;
						case 4:
							if (list == null)
							{
								goto IL_79;
							}
							num = 3;
							continue;
						case 5:
							return;
						case 6:
							num = 7;
							continue;
						case 7:
							num4 = 0;
							goto IL_E8;
						}
						goto IL_3F;
						IL_A7:
						num = 0;
						continue;
						IL_E8:
						num3 = num4;
						num2 = 0;
						num = 1;
					}
					IL_5F:
					if (false)
					{
					}
					list = this.ᜀ(oldValue);
					num = 4;
					goto IL_18;
				}
				return;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00049C08 File Offset: 0x00048C08
		public void Replace(string oldValue, double[] newValues, bool isVertical)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num;
					List<CellRange> list;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_71:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						list = this.ᜀ(oldValue);
						num = 3;
						break;
					}
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							goto IL_9F;
						case 1:
							num = 6;
							continue;
						case 2:
							goto IL_9F;
						case 3:
							if (list == null)
							{
								goto IL_71;
							}
							num = 5;
							continue;
						case 4:
							return;
						case 5:
							if (true)
							{
							}
							num2 = list.Count;
							goto IL_E8;
						case 6:
							num2 = 0;
							goto IL_E8;
						case 7:
						{
							if (num3 >= num4)
							{
								num = 4;
								continue;
							}
							XlsRange xlsRange = list[num3];
							xlsRange.Replace(oldValue, newValues, isVertical);
							num3++;
							num = 0;
							continue;
						}
						}
						break;
						IL_9F:
						num = 7;
						continue;
						IL_E8:
						num4 = num2;
						num3 = 0;
						num = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00049D10 File Offset: 0x00048D10
		public void Replace(string oldValue, DataTable newValues, bool columnHeaders)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num;
					List<CellRange> list;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_71:
						num = 4;
						break;
					default:
						if (false)
						{
						}
						list = this.ᜀ(oldValue);
						num = 0;
						break;
					}
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							if (list == null)
							{
								goto IL_71;
							}
							num = 1;
							continue;
						case 1:
							if (true)
							{
							}
							num2 = list.Count;
							goto IL_E8;
						case 2:
							goto IL_9F;
						case 3:
							return;
						case 4:
							num = 6;
							continue;
						case 5:
						{
							if (num3 >= num4)
							{
								num = 3;
								continue;
							}
							XlsRange xlsRange = list[num3];
							xlsRange.Replace(oldValue, newValues, columnHeaders);
							num3++;
							num = 2;
							continue;
						}
						case 6:
							num2 = 0;
							goto IL_E8;
						case 7:
							goto IL_9F;
						}
						break;
						IL_9F:
						num = 5;
						continue;
						IL_E8:
						num4 = num2;
						num3 = 0;
						num = 7;
					}
				}
				return;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00049E18 File Offset: 0x00048E18
		public void Replace(string oldValue, DataColumn column, bool columnHeaders)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num;
					List<CellRange> list;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_71:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						list = this.ᜀ(oldValue);
						num = 1;
						break;
					}
					for (;;)
					{
						int num2;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							goto IL_9F;
						case 1:
							if (list == null)
							{
								goto IL_71;
							}
							num = 3;
							continue;
						case 2:
							if (true)
							{
							}
							num = 7;
							continue;
						case 3:
							num2 = list.Count;
							goto IL_E8;
						case 4:
							return;
						case 5:
						{
							if (num3 >= num4)
							{
								num = 4;
								continue;
							}
							XlsRange xlsRange = list[num3];
							xlsRange.Replace(oldValue, column, columnHeaders);
							num3++;
							num = 6;
							continue;
						}
						case 6:
							goto IL_9F;
						case 7:
							num2 = 0;
							goto IL_E8;
						}
						break;
						IL_9F:
						num = 5;
						continue;
						IL_E8:
						num4 = num2;
						num3 = 0;
						num = 0;
					}
				}
				return;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00049F20 File Offset: 0x00048F20
		public void Remove()
		{
			for (;;)
			{
				base.ParseData();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u171F != null)
						{
							num = 8;
							continue;
						}
						goto IL_D1;
					case 1:
						if (this.ᜡ != null)
						{
							num = 3;
							continue;
						}
						goto IL_F4;
					case 2:
						goto IL_59;
					case 3:
						this.ᜡ.Clear();
						this.m_book.ᜬ();
						num = 4;
						continue;
					case 4:
						goto IL_F4;
					case 5:
						if (this.ᜪ != null)
						{
							num = 6;
							continue;
						}
						goto IL_59;
					case 6:
						this.ᜪ.Clear();
						num = 2;
						continue;
					case 7:
						goto IL_D1;
					case 8:
						if (true)
						{
						}
						this.\u171F.Clear();
						num = 7;
						continue;
					}
					break;
					IL_59:
					num = 1;
					continue;
					IL_D1:
					num = 5;
					continue;
					IL_F4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_10A;
					}
				}
			}
			IL_10A:
			if (false)
			{
			}
			this.m_book.InnerWorksheets.InnerRemove(base.Index);
			this.\u1718.ᜂ();
			this.Dispose();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0004A064 File Offset: 0x00049064
		public void MoveWorksheet(int destIndex)
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
			int realIndex = base.RealIndex;
			int newIndex = this.ᜀ(destIndex);
			this.m_book.Objects.Move(realIndex, destIndex);
			XlsWorksheetsCollection innerWorksheets = this.m_book.InnerWorksheets;
			innerWorksheets.Move(base.Index, newIndex);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0004A0DC File Offset: 0x000490DC
		private new int ᜀ(int A_0)
		{
			switch (0)
			{
			default:
			{
				IWorksheet worksheet;
				for (;;)
				{
					int num = A_0;
					int objectCount = this.m_book.ObjectCount;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= objectCount)
							{
								num2 = 2;
								continue;
							}
							worksheet = (this.m_book.Objects[num] as IWorksheet);
							num2 = 1;
							continue;
						case 1:
							if (worksheet != null)
							{
								num2 = 5;
								continue;
							}
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7B;
							default:
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							break;
						case 2:
							goto IL_DC;
						case 3:
							goto IL_C0;
						case 4:
							if (true)
							{
							}
							goto IL_C0;
						case 5:
							goto IL_BE;
						}
						break;
						IL_C0:
						num2 = 0;
					}
				}
				IL_7B:
				return worksheet.Index;
				IL_BE:
				goto IL_7B;
				IL_DC:
				IWorksheets worksheets = this.m_book.Worksheets;
				return worksheets.Count - 1;
			}
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0004A1DC File Offset: 0x000491DC
		public void SetColumnWidth(int columnIndex, double width)
		{
			int a_ = 17;
			int num = 3;
			for (;;)
			{
				spr\u216E spr_u216E;
				switch (num)
				{
				case 0:
					goto IL_17C;
				case 1:
					return;
				case 2:
				{
					if (columnIndex > this.m_book.MaxColumnCount)
					{
						num = 12;
						continue;
					}
					double columnWidth = this.GetColumnWidth(columnIndex);
					num = 11;
					continue;
				}
				case 4:
					if (width == 0.0)
					{
						num = 10;
						continue;
					}
					spr_u216E.ᜅ((ushort)(width * 256.0));
					sprᜑ.ᜁ(this, columnIndex);
					this.RaiseColumnWidthChangedEvent(columnIndex, width);
					num = 5;
					continue;
				case 5:
					goto IL_1A3;
				case 6:
				{
					spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
					spr\u216E spr_u216E2 = spr_u216E;
					ushort a_2;
					spr_u216E.ᜀ(a_2 = (ushort)(columnIndex - 1));
					spr_u216E2.ᜄ(a_2);
					spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
					spr_u216E.ᜅ((ushort)(base.AppImplementation.\u1713() * 256.0));
					this.ᜐ[columnIndex] = spr_u216E;
					num = 0;
					continue;
				}
				case 7:
					num = 2;
					continue;
				case 8:
					goto IL_1A3;
				case 9:
					goto IL_20A;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_20A;
					default:
						if (false)
						{
						}
						spr_u216E.ᜄ(true);
						num = 8;
						continue;
					}
					break;
				case 11:
				{
					double columnWidth;
					if (columnWidth != width)
					{
						num = 9;
						continue;
					}
					return;
				}
				case 12:
					goto IL_17A;
				case 13:
					if (spr_u216E == null)
					{
						num = 6;
						continue;
					}
					goto IL_17C;
				}
				if (columnIndex >= 1)
				{
					num = 7;
					continue;
				}
				break;
				IL_17C:
				num = 4;
				continue;
				IL_1A3:
				base.SetChanged();
				num = 1;
				continue;
				IL_20A:
				spr_u216E = this.ᜐ[columnIndex];
				if (true)
				{
				}
				num = 13;
			}
			IL_106:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ц♈❊㡌≎㽐", a_), RecordTableEnumerator.b("ц♈❊㡌≎㽐獒㱔㥖㵘㹚╜罞በୢ੤ቦըཪ䵬൮ᑰ卲᝴ቶ൸౺᡼᩾ꎂ뒄Ꞇ꾎놐ꆒꂔꆖ릘떚", a_));
			IL_17A:
			goto IL_106;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0004A3F8 File Offset: 0x000493F8
		public void SetColumnWidthInPixels(int iColumn, int value)
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
			base.ParseData();
			double width = this.PixelsToColumnWidth((double)value);
			this.SetColumnWidth(iColumn, width);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0004A44C File Offset: 0x0004944C
		protected internal void InnerSetRowHeight(int rowIndex, double height)
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
			this.ᜀ(rowIndex, height, true, MeasureUnits.Point, true);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0004A494 File Offset: 0x00049494
		protected internal void SetRowHeightInPixels(int rowIndex, double value)
		{
			int a_ = 12;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						if (false)
						{
						}
						if (value < 0.0)
						{
							num = 2;
							continue;
						}
						goto IL_E7;
					}
					break;
				case 1:
					num = 5;
					continue;
				case 2:
					goto IL_7F;
				case 4:
					goto IL_D1;
				case 5:
					if (rowIndex > this.m_book.MaxRowCount)
					{
						num = 4;
						continue;
					}
					num = 0;
					continue;
				}
				if (rowIndex < 1)
				{
					goto IL_8B;
				}
				num = 1;
			}
			IL_7F:
			goto IL_D3;
			IL_8B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ぁ⭃ㅅŇ⑉⡋⭍⡏", a_), RecordTableEnumerator.b("ᑁ╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟๡ţᕥ᭧䩩嵫乭ᅯᱱၳ噵ίࡹ᥻ώꚅﲇ낏ﾑ뢗肟쮡쪣슥춧튩芫", a_));
			IL_D1:
			goto IL_8B;
			IL_D3:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑁╃⩅㵇⽉", a_));
			IL_E7:
			this.ᜀ(rowIndex, value, true, MeasureUnits.Pixel, true);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0004A594 File Offset: 0x00049594
		public void SetColumnWidthInPixels(int columnIndex, int count, int value)
		{
			for (;;)
			{
				base.ParseData();
				double width = this.PixelsToColumnWidth((double)value);
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_33;
						default:
							if (false)
							{
							}
							this.SetColumnWidth(columnIndex++, width);
							num++;
							num2 = 3;
							continue;
						}
						break;
					case 1:
						goto IL_33;
					case 2:
						goto IL_51;
					case 3:
						goto IL_33;
					}
					break;
					IL_33:
					num2 = 0;
				}
			}
			IL_51:
			if (true)
			{
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0004A638 File Offset: 0x00049638
		public void SetRowHeight(int rowIndex, double height)
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
			this.InnerSetRowHeight(rowIndex, height);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0004A67C File Offset: 0x0004967C
		public void SetRowHeightPixels(int rowIndex, double height)
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
			this.SetRowHeightInPixels(rowIndex, height);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0004A6C0 File Offset: 0x000496C0
		public void SetRowHeightInPixels(int rowIndex, int count, double value)
		{
			int a_ = 7;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A2;
				case 1:
					goto IL_12A;
				case 2:
					if (count > this.m_book.MaxRowCount)
					{
						goto IL_17E;
					}
					num = 6;
					continue;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_189;
				case 6:
				{
					if (value < 0.0)
					{
						num = 0;
						continue;
					}
					int num2 = 0;
					num = 10;
					continue;
				}
				case 7:
					num = 9;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17E;
					default:
						if (false)
						{
						}
						goto IL_143;
					}
					break;
				case 9:
					if (rowIndex > this.m_book.MaxRowCount)
					{
						num = 1;
						continue;
					}
					num = 13;
					continue;
				case 10:
					goto IL_143;
				case 11:
				{
					int num2;
					if (num2 >= count)
					{
						num = 12;
						continue;
					}
					this.ᜀ(rowIndex++, value, true, MeasureUnits.Pixel, true);
					num2++;
					num = 8;
					continue;
				}
				case 12:
					return;
				case 13:
					if (count >= rowIndex)
					{
						num = 3;
						continue;
					}
					goto IL_1A7;
				}
				if (true)
				{
				}
				if (rowIndex >= 1)
				{
					num = 7;
					continue;
				}
				goto IL_A7;
				IL_143:
				num = 11;
				continue;
				IL_17E:
				num = 4;
			}
			IL_A2:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄", a_));
			IL_A7:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("漼倾㙀捂ౄ⥆ⵈ⹊㕌", a_), RecordTableEnumerator.b("䬼帾ⵀ㙂⁄杆⩈⩊⍌ⅎ㹐❒畔㕖㱘筚ㅜ㩞በၢ䕤፦Ũ੪ͬ佮䁰卲ᑴ᥶ᵸ孺᩼ൾﮈꮊ歷ﶒ떔殺붜캠풢薤캦잨쾪좬힮", a_));
			IL_12A:
			goto IL_A7;
			IL_189:
			IL_1A7:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("砼儾╀捂ᝄ⡆㹈歊ьⅎ㕐㙒ⵔ", a_), RecordTableEnumerator.b("欼帾ⵀ㙂⁄杆⩈⩊⍌ⅎ㹐❒畔㕖㱘筚ㅜ㩞በၢ䕤፦Ũ੪ͬ佮≰ݲᑴն൸孺ོၾꎂ꾎ﶒ랖ﺘﺞ햠욢힤螦\udda8쎪첬솮醰\udeb2풴쾶馸즺튼좾ꫂꯄꏆ곈돊", a_));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0004A898 File Offset: 0x00049898
		protected internal IXLSRange FindOne(string findValue, FindType flags)
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
			return this.ᜀ(findValue, flags, ExcelFindOptions.None);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0004A8DC File Offset: 0x000498DC
		internal IXLSRange ᜁ(string A_0, FindType A_1)
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
			return this.ᜁ(A_0, A_1, false);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0004A920 File Offset: 0x00049920
		internal IXLSRange ᜁ(string A_0, FindType A_1, bool A_2)
		{
			for (;;)
			{
				this.m_book.IsStartsOrEndsWith = new bool?(true);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_7F;
					case 2:
						goto IL_74;
					case 3:
						if (!A_2)
						{
							num = 0;
							continue;
						}
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
			}
			IL_74:
			ExcelFindOptions excelFindOptions = ExcelFindOptions.None;
			goto IL_82;
			IL_7F:
			excelFindOptions = ExcelFindOptions.MatchCase;
			IL_82:
			ExcelFindOptions a_ = excelFindOptions;
			return this.ᜀ(A_0, A_1, a_);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0004A9BC File Offset: 0x000499BC
		internal new IXLSRange ᜀ(string A_0, FindType A_1)
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
			return this.ᜀ(A_0, A_1, false);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0004AA00 File Offset: 0x00049A00
		internal new IXLSRange ᜀ(string A_0, FindType A_1, bool A_2)
		{
			if (true)
			{
			}
			for (;;)
			{
				this.m_book.IsStartsOrEndsWith = new bool?(false);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_74;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_7F;
					case 3:
						if (!A_2)
						{
							num = 1;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_74:
			ExcelFindOptions excelFindOptions = ExcelFindOptions.None;
			goto IL_82;
			IL_7F:
			excelFindOptions = ExcelFindOptions.MatchCase;
			IL_82:
			ExcelFindOptions a_ = excelFindOptions;
			return this.ᜀ(A_0, A_1, a_);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0004AA9C File Offset: 0x00049A9C
		internal new IXLSRange ᜀ(string A_0, FindType A_1, ExcelFindOptions A_2)
		{
			IXLSRange[] array = this.ᜀ(this.AllocatedRange, A_0, A_1, A_2, true);
			if (array != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16;
				}
				if (false)
				{
				}
				return array[0];
			}
			IL_16:
			if (true)
			{
			}
			return null;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0004AAF4 File Offset: 0x00049AF4
		protected internal IXLSRange FindOne(double findValue, FindType flags)
		{
			IXLSRange[] array = this.Find(this.AllocatedRange, findValue, flags, true);
			if (array != null)
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
					return array[0];
				}
			}
			return null;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0004AB4C File Offset: 0x00049B4C
		protected internal IXLSRange FindOne(bool findValue)
		{
			int num = 0;
			IXLSRange[] array;
			for (;;)
			{
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (array == null)
						{
							num = 2;
							continue;
						}
						goto IL_81;
					}
					break;
				case 2:
					goto IL_7C;
				}
				IL_1C:
				array = this.Find(this.AllocatedRange as XlsRange, findValue ? 1 : 0, false, true);
				num = 1;
				continue;
				goto IL_1C;
			}
			IL_7C:
			if (true)
			{
			}
			return null;
			IL_81:
			return array[0];
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0004ABE0 File Offset: 0x00049BE0
		protected internal IXLSRange FindOne(DateTime findValue)
		{
			double findValue2 = UtilityMethods.ᜀ(findValue);
			IXLSRange[] array = this.Find(this.AllocatedRange, findValue2, FindType.Number, true);
			if (array != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_25;
				}
				if (false)
				{
				}
				return array[0];
			}
			if (true)
			{
			}
			IL_25:
			return null;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0004AC40 File Offset: 0x00049C40
		protected internal IXLSRange FindOne(TimeSpan findValue)
		{
			double findValue2 = (double)findValue.Days + (double)(findValue.Hours * 360000 + findValue.Minutes * 6000 + findValue.Seconds * 100 + findValue.Milliseconds) / 8640000.0;
			IXLSRange[] array = this.Find(this.AllocatedRange, findValue2, FindType.Number, true);
			if (array != null)
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
					if (true)
					{
					}
					return array[0];
				}
			}
			return null;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0004ACDC File Offset: 0x00049CDC
		protected internal CellRange[] FindAll(string findValue, FindType flags)
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
			return this.ᜁ(findValue, flags, ExcelFindOptions.None);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0004AD20 File Offset: 0x00049D20
		internal CellRange[] ᜁ(string A_0, FindType A_1, ExcelFindOptions A_2)
		{
			int num = 2;
			List<CellRange> list;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_CF;
				case 1:
					goto IL_BB;
				case 3:
				{
					CellRange[] array;
					if (array != null)
					{
						goto IL_84;
					}
					goto IL_D1;
				}
				case 4:
					num = 5;
					continue;
				case 5:
				{
					if (A_0.Length == 0)
					{
						num = 1;
						continue;
					}
					CellRange[] array = this.ᜀ(this.AllocatedRange as XlsRange, A_0, A_1, A_2, false);
					list = new List<CellRange>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_84;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 6:
				{
					CellRange[] array;
					list.AddRange(array);
					num = 0;
					continue;
				}
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_84:
				num = 6;
			}
			IL_91:
			return null;
			IL_BB:
			goto IL_91;
			IL_CF:
			IL_D1:
			return list.ToArray();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0004AE04 File Offset: 0x00049E04
		protected internal CellRange[] FindAll(double findValue, FindType flags)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
					bool flag2 = (flags & FindType.Number) == FindType.Number;
					int num = 3;
					for (;;)
					{
						CellRange[] array;
						switch (num)
						{
						case 0:
							goto IL_F2;
						case 1:
							num = 4;
							continue;
						case 2:
							if (array != null)
							{
								num = 6;
								continue;
							}
							goto IL_109;
						case 3:
							if (!flag)
							{
								num = 1;
								continue;
							}
							goto IL_65;
						case 4:
							if (flag2)
							{
								goto IL_65;
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
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 5:
							goto IL_107;
						case 6:
							list.AddRange(array);
							num = 5;
							continue;
						}
						break;
						IL_65:
						array = this.Find(this.AllocatedRange, findValue, flags, false);
						list = new List<CellRange>();
						num = 2;
					}
				}
				IL_F2:
				throw new ArgumentException(RecordTableEnumerator.b("氻弽㈿⍁⥃⍅㱇⽉㹋湍㥏⅑瑓㡕㝗⹙籛⡝ş๡ൣɥ䙧", a_));
				IL_107:
				IL_109:
				return list.ToArray();
			}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0004AF20 File Offset: 0x00049F20
		protected internal List<CellRange> FindAll(bool findValue)
		{
			int num = 0;
			List<CellRange> list;
			for (;;)
			{
				CellRange[] array;
				switch (num)
				{
				case 1:
					goto IL_34;
				case 2:
					if (array != null)
					{
						num = 1;
						continue;
					}
					goto IL_7E;
				case 3:
					if (true)
					{
					}
					goto IL_7E;
				}
				array = this.Find(this.AllocatedRange as XlsRange, findValue ? 1 : 0, false, false);
				list = new List<CellRange>();
				num = 2;
				continue;
				IL_34:
				list.AddRange(array);
				num = 3;
				continue;
				IL_7E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
					goto IL_94;
				}
			}
			IL_94:
			if (false)
			{
			}
			return list;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0004AFC8 File Offset: 0x00049FC8
		protected internal CellRange[] FindAll(DateTime findValue)
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
			double findValue2 = UtilityMethods.ᜀ(findValue);
			return this.FindAll(findValue2, FindType.Number | FindType.FormulaValue);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0004B014 File Offset: 0x0004A014
		protected internal CellRange[] FindAll(TimeSpan findValue)
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
			double totalDays = findValue.TotalDays;
			return this.FindAll(totalDays, FindType.Number | FindType.FormulaValue);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0004B060 File Offset: 0x0004A060
		public void SaveToFile(string fileName, string separator)
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
			this.SaveToFile(fileName, separator, Encoding.Unicode);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0004B0A8 File Offset: 0x0004A0A8
		public void SaveToFile(string fileName, string separator, Encoding encoding)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					string directoryName;
					FileStream fileStream;
					string fullPath;
					switch (num)
					{
					case 0:
						if (directoryName.Length > 0)
						{
							num = 13;
							continue;
						}
						goto IL_DC;
					case 1:
						if (directoryName != null)
						{
							num = 12;
							continue;
						}
						goto IL_DC;
					case 3:
					{
						FileAttributes attributes;
						if ((attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
						{
							num = 18;
							continue;
						}
						goto IL_294;
					}
					case 4:
						if (separator != null)
						{
							num = 9;
							continue;
						}
						goto IL_C8;
					case 5:
						goto IL_DC;
					case 6:
						try
						{
							this.SaveToStream(fileStream, separator, encoding);
							fileStream.Close();
							return;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										((IDisposable)fileStream).Dispose();
										num = 1;
										continue;
									}
									break;
								case 1:
									goto IL_1FC;
								}
								IL_1C3:
								if (fileStream != null)
								{
									num = 0;
									continue;
								}
								break;
								goto IL_1C3;
							}
							IL_1FC:;
						}
						goto IL_1FF;
					case 7:
						goto IL_1FF;
					case 8:
						if (separator == string.Empty)
						{
							num = 17;
							continue;
						}
						num = 14;
						continue;
					case 9:
						num = 8;
						continue;
					case 10:
						goto IL_8D;
					case 11:
						if (!Directory.Exists(directoryName))
						{
							num = 7;
							continue;
						}
						goto IL_DC;
					case 12:
						num = 0;
						continue;
					case 13:
						num = 11;
						continue;
					case 14:
						if (fileName.Length == 0)
						{
							num = 15;
							continue;
						}
						fullPath = Path.GetFullPath(fileName);
						directoryName = Path.GetDirectoryName(fullPath);
						num = 19;
						continue;
					case 15:
						goto IL_25F;
					case 16:
					{
						FileAttributes attributes = File.GetAttributes(fullPath);
						num = 3;
						continue;
					}
					case 17:
						goto IL_18C;
					case 18:
						goto IL_15F;
					case 19:
						if (File.Exists(fullPath))
						{
							num = 16;
							continue;
						}
						goto IL_294;
					}
					if (true)
					{
					}
					if (fileName == null)
					{
						num = 10;
						continue;
					}
					num = 4;
					continue;
					IL_DC:
					fileStream = new FileStream(fullPath, FileMode.Create);
					num = 6;
					continue;
					IL_1FF:
					Directory.CreateDirectory(directoryName);
					num = 5;
					continue;
					IL_294:
					num = 1;
				}
				IL_8D:
				throw new ArgumentNullException(RecordTableEnumerator.b("Ƀ⽅⑇⽉≋⽍㵏㝑", a_));
				IL_C8:
				throw new ArgumentNullException(RecordTableEnumerator.b("㝃⍅㡇⭉㹋⽍⑏㵑♓", a_));
				IL_15F:
				throw new IOException(RecordTableEnumerator.b("Ƀ⽅⑇⽉汋❍⍏牑♓㍕㥗㹙㍛そ౟᭡䡣䙥୧୩ɫ乭ṯᵱs噵᩷ό屻ൽꚇ", a_));
				IL_18C:
				goto IL_C8;
				IL_25F:
				throw new ArgumentException(RecordTableEnumerator.b("Ƀ⽅⑇⽉汋Mㅏ㽑ㅓ癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩५ͭoٱ൳塵", a_));
			}
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0004B388 File Offset: 0x0004A388
		public void SaveToStream(Stream stream, string separator)
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
			this.SaveToStream(stream, separator, Encoding.Unicode);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0004B3D0 File Offset: 0x0004A3D0
		public void SaveToStream(Stream stream, string separator, Encoding encoding)
		{
			int a_ = 10;
			StreamWriter streamWriter;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 10;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (separator != null)
							{
								num = 16;
								continue;
							}
							goto IL_1C6;
						case 1:
							goto IL_90;
						case 2:
							goto IL_17B;
						case 3:
							goto IL_152;
						case 4:
						{
							if (num2 > this.m_iLastColumn)
							{
								num = 12;
								continue;
							}
							long cellIndex = sprṔ.ᜀ(num2, num3);
							XlsWorksheet.TRangeValueType trangeValueType = this.ᜏ.ᜃ(num3, num2);
							string value = string.Empty;
							num = 15;
							continue;
						}
						case 5:
						{
							long cellIndex;
							string value = this.ᜏ.GetValue(cellIndex, num3, num2, this.Range);
							streamWriter.Write(value);
							num = 3;
							continue;
						}
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								goto IL_17B;
							}
							break;
						case 7:
							if (num3 > this.m_iLastRow)
							{
								num = 8;
								continue;
							}
							num = 11;
							continue;
						case 8:
							goto IL_19F;
						case 9:
							goto IL_101;
						case 11:
							if (!this.ᜁ(num3, false))
							{
								num = 19;
								continue;
							}
							goto IL_1DA;
						case 12:
							goto IL_1DA;
						case 13:
							goto IL_211;
						case 14:
							if (num2 != this.m_iLastColumn)
							{
								num = 17;
								continue;
							}
							goto IL_211;
						case 15:
						{
							XlsWorksheet.TRangeValueType trangeValueType;
							if (trangeValueType != XlsWorksheet.TRangeValueType.Blank)
							{
								num = 5;
								continue;
							}
							goto IL_152;
						}
						case 16:
							num = 21;
							continue;
						case 17:
							streamWriter.Write(separator);
							num = 13;
							continue;
						case 18:
							goto IL_101;
						case 19:
							num2 = this.m_iFirstColumn;
							num = 18;
							continue;
						case 20:
							goto IL_FC;
						case 21:
							if (separator.Length == 0)
							{
								num = 20;
								continue;
							}
							base.ParseData();
							streamWriter = new StreamWriter(stream, encoding);
							num3 = this.m_iFirstRow;
							num = 2;
							continue;
						}
						if (stream == null)
						{
							num = 1;
							continue;
						}
						num = 0;
						continue;
						IL_101:
						num = 4;
						continue;
						IL_152:
						num = 14;
						continue;
						IL_17B:
						num = 7;
						continue;
						IL_1DA:
						streamWriter.WriteLine();
						num3++;
						num = 6;
						continue;
						IL_211:
						num2++;
						num = 9;
					}
					break;
				}
				}
			}
			IL_90:
			throw new ArgumentException(RecordTableEnumerator.b("㌿㙁㙃⍅⥇❉", a_));
			IL_FC:
			goto IL_1C6;
			IL_19F:
			streamWriter.Flush();
			stream.Flush();
			return;
			IL_1C6:
			throw new ArgumentException(RecordTableEnumerator.b("㌿❁㑃⍅㩇⭉㡋⅍≏", a_));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0004B6B8 File Offset: 0x0004A6B8
		public void SaveToHtml(string fileName, HTMLOptions saveOption)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					string fullPath;
					FileStream fileStream;
					string text;
					switch (num)
					{
					case 0:
						if (File.Exists(fullPath))
						{
							num = 18;
							continue;
						}
						goto IL_25C;
					case 1:
						num = 5;
						continue;
					case 2:
						if (base.HasPictures)
						{
							num = 12;
							continue;
						}
						goto IL_D1;
					case 3:
						try
						{
							sprᯟ sprᯟ = new sprᯟ();
							sprᯟ.ᜀ(fileStream, this, text, saveOption);
							fileStream.Close();
							return;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								IL_1AD:
								switch (num)
								{
								case 1:
									((IDisposable)fileStream).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_1FB;
								}
								while (fileStream != null)
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
										num = 1;
										goto IL_1AD;
									}
								}
								break;
							}
							IL_1FB:;
						}
						goto IL_1FE;
					case 4:
						if (Directory.Exists(text))
						{
							num = 8;
							continue;
						}
						Directory.CreateDirectory(text);
						num = 11;
						continue;
					case 5:
						if (saveOption.ImagePath.Equals(string.Empty))
						{
							num = 14;
							continue;
						}
						goto IL_D1;
					case 6:
						if (saveOption.ImagePath != null)
						{
							num = 1;
							continue;
						}
						goto IL_9B;
					case 7:
						goto IL_10B;
					case 8:
						Directory.Delete(text, true);
						Directory.CreateDirectory(text);
						num = 10;
						continue;
					case 9:
						if (fileName.Length == 0)
						{
							num = 7;
							continue;
						}
						fullPath = Path.GetFullPath(fileName);
						num = 0;
						continue;
					case 10:
						goto IL_1FE;
					case 11:
						goto IL_1FE;
					case 12:
						num = 6;
						continue;
					case 14:
						goto IL_9B;
					case 15:
						goto IL_D1;
					case 16:
						goto IL_81;
					case 17:
						goto IL_25C;
					case 18:
						File.Delete(fullPath);
						num = 17;
						continue;
					}
					if (fileName == null)
					{
						num = 16;
						continue;
					}
					num = 9;
					continue;
					IL_9B:
					num = 4;
					continue;
					IL_D1:
					fileStream = new FileStream(fileName, FileMode.CreateNew);
					num = 3;
					continue;
					IL_1FE:
					string name = new DirectoryInfo(text).Name;
					saveOption.ImagePath = name;
					if (true)
					{
					}
					num = 15;
					continue;
					IL_25C:
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
					text = Path.Combine(Path.GetDirectoryName(fullPath), string.Format(RecordTableEnumerator.b("䀺഼䈾Ṁ╂ⱄ⭆ⱈ㡊", a_), fileNameWithoutExtension));
					num = 2;
				}
				IL_81:
				throw new ArgumentNullException(RecordTableEnumerator.b("紺吼匾⑀ⵂ⑄⩆ⱈ", a_));
				IL_10B:
				throw new ArgumentException(RecordTableEnumerator.b("紺吼匾⑀ൂ⑄⩆ⱈ歊⹌⹎㽐㵒㩔⍖祘㥚㡜罞Ѡ๢ᕤ፦ၨ䕪", a_));
			}
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0004B9B4 File Offset: 0x0004A9B4
		public void SaveToHtml(Stream stream, HTMLOptions saveOption)
		{
			int a_ = 8;
			sprᯟ sprᯟ;
			string a_2;
			for (;;)
			{
				sprᯟ = new sprᯟ();
				a_2 = null;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (saveOption.ImagePath == null)
							{
								goto IL_EC;
							}
							break;
						}
						num = 5;
						continue;
					case 1:
						if (saveOption.ImagePath != null)
						{
							num = 4;
							continue;
						}
						goto IL_54;
					case 2:
						num = 1;
						continue;
					case 3:
						if (!Directory.Exists(saveOption.ImagePath))
						{
							num = 2;
							continue;
						}
						goto IL_54;
					case 4:
						goto IL_D1;
					case 5:
						a_2 = Path.GetFullPath(saveOption.ImagePath);
						num = 6;
						continue;
					case 6:
						goto IL_EA;
					}
					break;
					IL_54:
					num = 0;
				}
			}
			IL_D1:
			throw new ArgumentException(RecordTableEnumerator.b("眽ⴿ⍁⍃⍅桇ᩉⵋ㩍㡏牑こ㥕㵗⥙㉛祝ᑟ䉡ţṥŧᥩᡫ", a_));
			IL_EA:
			IL_EC:
			sprᯟ.ᜀ(stream, this, a_2, saveOption);
			stream.Close();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0004BAC0 File Offset: 0x0004AAC0
		public void SaveToHtml(string filename)
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
			this.SaveToHtml(filename, HTMLOptions.Default);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0004BB08 File Offset: 0x0004AB08
		public void SaveToHtml(Stream stream)
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
			this.SaveToHtml(stream, HTMLOptions.Default);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0004BB50 File Offset: 0x0004AB50
		public void SetDefaultColumnStyle(int columnIndex, IStyle defaultStyle)
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
			this.ᜀ(columnIndex, columnIndex, defaultStyle, this.ᜐ, new XlsWorksheet.ᜀ(this.ᜄ), false);
			sprᜑ.ᜀ(this, columnIndex);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0004BBB0 File Offset: 0x0004ABB0
		public void SetDefaultColumnStyle(int firstColumnIndex, int lastColumnIndex, IStyle defaultStyle)
		{
			for (;;)
			{
				base.ParseData();
				ushort num = (ushort)this.ᜀ(defaultStyle);
				int num2 = firstColumnIndex;
				int num3 = 3;
				for (;;)
				{
					spr\u2502 spr_u;
					switch (num3)
					{
					case 0:
						spr_u = this.ᜄ(num2);
						goto IL_6E;
					case 1:
						goto IL_49;
					case 2:
						if (true)
						{
						}
						if (spr_u == null)
						{
							num3 = 0;
							continue;
						}
						goto IL_49;
					case 3:
						goto IL_A4;
					case 4:
						if (num2 > lastColumnIndex)
						{
							num3 = 5;
							continue;
						}
						spr_u = this.ᜐ[num2];
						num3 = 2;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							goto IL_D4;
						}
						break;
					case 6:
						goto IL_A4;
					}
					break;
					IL_49:
					spr_u.ᜁ(num);
					this.ᜀ(num2, num);
					num2++;
					num3 = 6;
					continue;
					IL_6E:
					num3 = 1;
					continue;
					IL_A4:
					num3 = 4;
				}
			}
			IL_D4:
			if (false)
			{
			}
			sprᜑ.ᜁ(this, firstColumnIndex);
			sprᜑ.ᜁ(this, lastColumnIndex);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0004BCA8 File Offset: 0x0004ACA8
		public void SetDefaultRowStyle(int rowIndex, IStyle defaultStyle)
		{
			switch (0)
			{
			default:
			{
				ushort num;
				sprᱧ sprᱧ;
				for (;;)
				{
					num = (ushort)this.ᜀ(defaultStyle);
					sprᜑ.ᜀ(this, rowIndex);
					rowIndex--;
					sprᱧ = sprᜑ.ᜀ(this, rowIndex, true);
					int num2 = 3;
					for (;;)
					{
						int num3;
						CellRange[] cells;
						switch (num2)
						{
						case 0:
							goto IL_79;
						case 1:
							goto IL_9D;
						case 2:
							if (true)
							{
							}
							if (num3 >= cells.Length)
							{
								num2 = 1;
								continue;
							}
							goto IL_9F;
						case 3:
							if (this.Rows.Length > 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_111;
						case 4:
							goto IL_79;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9F;
							default:
								if (false)
								{
								}
								cells = this.Rows[rowIndex].Cells;
								num3 = 0;
								num2 = 0;
								continue;
							}
							break;
						}
						break;
						IL_79:
						num2 = 2;
						continue;
						IL_9F:
						IXLSRange ixlsrange = cells[num3];
						sprᱧ.ᜁ(rowIndex, ixlsrange.Column - 1, (int)num, base.AppImplementation.ᜨ());
						num3++;
						num2 = 4;
					}
				}
				IL_9D:
				IL_111:
				sprᱧ.ᜀ(num);
				return;
			}
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0004BDD0 File Offset: 0x0004ADD0
		public void SetDefaultRowStyle(int firstRowIndex, int lastRowIndex, IStyle defaultStyle)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						ushort num = (ushort)this.ᜀ(defaultStyle);
						sprᜑ.ᜀ(this, firstRowIndex);
						sprᜑ.ᜀ(this, lastRowIndex);
						firstRowIndex--;
						lastRowIndex--;
						int num2 = firstRowIndex;
						int num3 = 4;
						for (;;)
						{
							sprᱧ sprᱧ;
							switch (num3)
							{
							case 0:
								if (num2 > lastRowIndex)
								{
									num3 = 7;
									continue;
								}
								sprᱧ = sprᜑ.ᜀ(this, num2, true);
								num3 = 1;
								continue;
							case 1:
								if (this.Rows.Length > 0)
								{
									num3 = 6;
									continue;
								}
								goto IL_70;
							case 2:
								goto IL_70;
							case 3:
								goto IL_151;
							case 4:
								goto IL_113;
							case 5:
								goto IL_113;
							case 6:
							{
								CellRange[] cells = this.Rows[num2].Cells;
								int num4 = 0;
								num3 = 8;
								continue;
							}
							case 7:
								return;
							case 8:
								goto IL_151;
							case 9:
							{
								CellRange[] cells;
								int num4;
								if (num4 >= cells.Length)
								{
									num3 = 2;
									continue;
								}
								if (true)
								{
								}
								IXLSRange ixlsrange = cells[num4];
								sprᱧ.ᜁ(num2, ixlsrange.Column - 1, (int)num, base.AppImplementation.ᜨ());
								num4++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num3 = 3;
									continue;
								}
								break;
							}
							}
							break;
							IL_70:
							sprᱧ.ᜀ(num);
							num2++;
							num3 = 5;
							continue;
							IL_113:
							num3 = 0;
							continue;
							IL_151:
							num3 = 9;
						}
					}
					break;
				}
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0004BF60 File Offset: 0x0004AF60
		private new void ᜀ(int A_0, ushort A_1)
		{
			for (;;)
			{
				int num = this.CellRecords.FirstRow - 1;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						spr\u23A5 spr_u23A;
						if (spr_u23A != null)
						{
							num2 = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_149;
						default:
							if (false)
							{
							}
							spr_u23A = this.ᜏ.ᜁ(num + 1, A_0, TBIFFRecord.Blank);
							spr_u23A.ᜀ(A_1);
							this.ᜏ.ᜀ(spr_u23A, false);
							num2 = 4;
							continue;
						}
						break;
					}
					case 1:
						num2 = 3;
						continue;
					case 2:
					{
						if (num > this.CellRecords.LastRow)
						{
							num2 = 11;
							continue;
						}
						sprᱧ sprᱧ = sprᜑ.ᜀ(this, num, false);
						num2 = 5;
						continue;
					}
					case 3:
					{
						sprᱧ sprᱧ;
						if (sprᱧ.ᜇ() != 0)
						{
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						goto IL_145;
					}
					case 4:
						goto IL_145;
					case 5:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_145;
					}
					case 6:
					{
						spr\u23A5 spr_u23A = this.ᜏ.ᜄ(num + 1, A_0);
						num2 = 0;
						continue;
					}
					case 7:
						goto IL_FB;
					case 8:
						goto IL_FB;
					case 9:
					{
						spr\u23A5 spr_u23A;
						spr_u23A.ᜀ(A_1);
						this.ᜏ.ᜀ(spr_u23A, false);
						num2 = 10;
						continue;
					}
					case 10:
						goto IL_145;
					case 11:
						return;
					}
					break;
					IL_FB:
					num2 = 2;
					continue;
					IL_149:
					num2 = 8;
					continue;
					IL_145:
					num++;
					goto IL_149;
				}
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0004C0F4 File Offset: 0x0004B0F4
		public IStyle GetDefaultColumnStyle(int columnIndex)
		{
			int a_ = 12;
			spr\u2502 spr_u;
			for (;;)
			{
				base.ParseData();
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_E0:
					num = 7;
					break;
				default:
					if (false)
					{
					}
					num = 6;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_74;
					case 1:
						if (spr_u == null)
						{
							num = 3;
							continue;
						}
						goto IL_E0;
					case 2:
						goto IL_D6;
					case 3:
						num = 0;
						continue;
					case 4:
						if (columnIndex > this.m_book.MaxColumnCount)
						{
							num = 2;
							continue;
						}
						spr_u = this.ᜐ[columnIndex];
						num = 1;
						continue;
					case 5:
						num = 4;
						continue;
					case 6:
						if (columnIndex >= 1)
						{
							num = 5;
							continue;
						}
						goto IL_F3;
					case 7:
						goto IL_EB;
					}
					break;
				}
			}
			IL_74:
			int num2 = this.m_book.DefaultXFIndex;
			goto IL_12A;
			IL_D6:
			if (true)
			{
			}
			goto IL_F3;
			IL_EB:
			num2 = (int)spr_u.ᜃ();
			goto IL_12A;
			IL_F3:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁݃⥅⑇㽉⅋⁍᥏㱑こ㍕⁗", a_), RecordTableEnumerator.b("ᑁ╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟๡ţᕥ᭧䩩ᡫ٭ᅯᱱ味䝵塷᭹ቻ᩽ꁿﺉﲍ낏ﲓ몙", a_) + this.m_book.MaxColumnCount);
			IL_12A:
			int iXFIndex = num2;
			return new AddtionalFormatWrapper(this.m_book, iXFIndex);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0004C238 File Offset: 0x0004B238
		public IStyle GetDefaultRowStyle(int rowIndex)
		{
			int a_ = 0;
			int num = 2;
			sprᱧ sprᱧ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					num = 8;
					continue;
				case 3:
					goto IL_EF;
				case 4:
					num = 5;
					continue;
				case 5:
					if (!this.m_book.IsFormatted((int)sprᱧ.ᜇ()))
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (sprᱧ != null)
						{
							num = 4;
							continue;
						}
						goto IL_52;
					}
					break;
				case 7:
					goto IL_5A;
				case 8:
					if (true)
					{
					}
					if (rowIndex > this.m_book.MaxRowCount)
					{
						num = 9;
						continue;
					}
					sprᱧ = sprᜑ.ᜀ(this, rowIndex - 1, false);
					num = 6;
					continue;
				case 9:
					goto IL_E2;
				}
				IL_41:
				if (rowIndex >= 1)
				{
					num = 1;
					continue;
				}
				goto IL_F7;
				goto IL_41;
				IL_52:
				num = 7;
			}
			IL_5A:
			int num2 = this.m_book.DefaultXFIndex;
			goto IL_15C;
			IL_E2:
			goto IL_F7;
			IL_EF:
			num2 = (int)sprᱧ.ᜇ();
			goto IL_15C;
			IL_F7:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䐵圷䴹画倽␿❁㱃", a_), RecordTableEnumerator.b("怵夷嘹䤻嬽怿ㅁⱃ⥅㵇♉⡋湍㉏㝑瑓㑕㵗⹙⭛㭝՟ౡ䑣坥䡧୩ɫ੭偯", a_) + this.m_book.MaxRowCount);
			IL_15C:
			int iXFIndex = num2;
			return new AddtionalFormatWrapper(this.m_book, iXFIndex);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0004C3B0 File Offset: 0x0004B3B0
		protected internal void FreeRange(IXLSRange range)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						int num = range.Row;
						int lastRow = range.LastRow;
						if (true)
						{
						}
						int num2 = 5;
						for (;;)
						{
							switch (num2)
							{
							case 0:
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
									int num3;
									int lastColumn;
									if (num3 > lastColumn)
									{
										num2 = 1;
										continue;
									}
									this.FreeRange(num, num3);
									num3++;
									num2 = 3;
									continue;
								}
								}
								break;
							case 1:
								num++;
								num2 = 2;
								continue;
							case 2:
								goto IL_C4;
							case 3:
								goto IL_67;
							case 4:
								return;
							case 5:
								goto IL_C4;
							case 6:
							{
								if (num > lastRow)
								{
									num2 = 4;
									continue;
								}
								int num3 = range.Column;
								int lastColumn = range.LastColumn;
								num2 = 7;
								continue;
							}
							case 7:
								goto IL_67;
							}
							break;
							IL_67:
							num2 = 0;
							continue;
							IL_C4:
							num2 = 6;
						}
					}
				}
				return;
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0004C4BC File Offset: 0x0004B4BC
		protected internal void FreeRange(int rowIndex, int columnIndex)
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
			base.ParseData();
			this.CellRecords.FreeRange(rowIndex, columnIndex);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0004C50C File Offset: 0x0004B50C
		public Image SaveToImage(int firstRow, int firstColumn, int lastRow, int lastColumn)
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
			return this.SaveToImage(null, firstRow, firstColumn, lastRow, base.LastColumn, ImageType.Bitmap);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0004C558 File Offset: 0x0004B558
		public void SaveToImage(string fileName, int firstRow, int firstColumn, int lastRow, int lastColumn)
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
			Image image = this.SaveToImage(null, firstRow, firstColumn, lastRow, base.LastColumn, ImageType.Bitmap);
			image.Save(fileName);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0004C5B0 File Offset: 0x0004B5B0
		public void SaveToImage(string fileName)
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
			Image image = this.SaveToImage(null, this.AllocatedRange.Row, this.AllocatedRange.Column, this.AllocatedRange.LastRow, this.AllocatedRange.LastColumn, ImageType.Bitmap);
			image.Save(fileName);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0004C627 File Offset: 0x0004B627
		public Image SaveToImage(Stream stream, int firstRow, int firstColumn, int lastRow, int lastColumn, ImageType imageType)
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
			return this.SaveToImage(stream, firstRow, firstColumn, lastRow, lastColumn, imageType, EmfType.EmfOnly);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0004C667 File Offset: 0x0004B667
		public Image SaveToImage(Stream stream, int firstRow, int firstColumn, int lastRow, int lastColumn, EmfType emfType)
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
			return this.SaveToImage(stream, firstRow, firstColumn, lastRow, lastColumn, ImageType.Metafile, emfType);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0004C6A8 File Offset: 0x0004B6A8
		public Image SaveToImage(Stream stream, int firstRow, int firstColumn, int lastRow, int lastColumn, ImageType imageType, EmfType emfType)
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
			sprᣀ sprᣀ = new sprᣀ();
			return sprᣀ.ᜀ(this, firstRow, firstColumn, lastRow, lastColumn, imageType, stream, emfType);
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0004C6FC File Offset: 0x0004B6FC
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0004C7AC File Offset: 0x0004B7AC
		public CellRange TopLeftCell
		{
			get
			{
				int num = 3;
				int row;
				int column;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5E;
					case 1:
						goto IL_76;
					case 2:
						goto IL_92;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5E;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (true)
					{
					}
					if (this.IsFreezePanes)
					{
						num = 0;
						continue;
					}
					row = base.TopVisibleRow;
					column = base.LeftVisibleColumn;
					num = 1;
					continue;
					IL_5E:
					row = this.FirstVisibleRow + 1;
					column = this.FirstVisibleColumn + 1;
					num = 2;
				}
				IL_76:
				IL_92:
				return this[row, column] as CellRange;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (value.Column > this.PaneFirstVisible.Column)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						if (value.Row > this.PaneFirstVisible.Row)
						{
							num = 5;
							continue;
						}
						return;
					case 2:
						goto IL_B2;
					case 3:
						return;
					case 5:
						num = 0;
						continue;
					case 6:
						num = 1;
						continue;
					}
					if (this.IsFreezePanes)
					{
						num = 6;
					}
					else
					{
						base.TopVisibleRow = value.Row;
						base.LeftVisibleColumn = value.Column;
						num = 3;
					}
				}
				IL_B2:
				this.FirstVisibleRow = value.Row - 1;
				this.FirstVisibleColumn = value.Column - 1;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0004C8C0 File Offset: 0x0004B8C0
		private new void ᜀ(DataTable A_0, int A_1, int A_2, int A_3, int A_4, DataColumn[] A_5, bool A_6)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u24F1 spr_u24F = new spr\u24F1((spr\u2158)base.ReservedHandle, this);
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							int num2 = 0;
							num = 9;
							continue;
						}
						case 1:
							goto IL_1D8;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_20D;
							default:
								if (false)
								{
								}
								goto IL_189;
							}
							break;
						case 3:
						{
							if (A_6)
							{
								num = 0;
								continue;
							}
							int num3 = 0;
							num = 17;
							continue;
						}
						case 4:
						{
							int num2;
							num2++;
							num = 2;
							continue;
						}
						case 5:
						{
							int num3;
							if (num3 >= A_3)
							{
								num = 13;
								continue;
							}
							DataRow dataRow = A_0.Rows[num3];
							int num4 = 0;
							num = 1;
							continue;
						}
						case 6:
						{
							int num5;
							if (num5 >= A_4)
							{
								num = 4;
								continue;
							}
							DataColumn column = A_5[num5];
							int num2;
							spr_u24F.ᜀ(A_1 + num2, A_2 + num5);
							DataRow dataRow2;
							this.ᜀ(A_1 + num2, A_2 + num5, dataRow2[column].ToString());
							num5++;
							num = 15;
							continue;
						}
						case 7:
							goto IL_166;
						case 8:
							goto IL_1D8;
						case 9:
							goto IL_189;
						case 10:
							return;
						case 11:
						{
							int num2;
							if (num2 >= A_3)
							{
								num = 10;
								continue;
							}
							DataRow dataRow2 = A_0.Rows[num2];
							int num5 = 0;
							num = 12;
							continue;
						}
						case 12:
							if (true)
							{
							}
							goto IL_1AB;
						case 13:
							return;
						case 14:
						{
							int num3;
							num3++;
							num = 7;
							continue;
						}
						case 15:
							goto IL_1AB;
						case 16:
						{
							int num4;
							if (num4 >= A_4)
							{
								num = 14;
								continue;
							}
							DataColumn column = A_5[num4];
							int num3;
							spr_u24F.ᜀ(A_1 + num3, A_2 + num4);
							DataRow dataRow;
							spr_u24F.Value2 = dataRow[column];
							num4++;
							num = 8;
							continue;
						}
						case 17:
							goto IL_20D;
						}
						break;
						IL_166:
						num = 5;
						continue;
						IL_20D:
						goto IL_166;
						IL_189:
						num = 11;
						continue;
						IL_1AB:
						num = 6;
						continue;
						IL_1D8:
						num = 16;
					}
				}
				return;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0004CB24 File Offset: 0x0004BB24
		private new void ᜀ(DataTable A_0, int A_1, int A_2, int A_3, int A_4, DataColumn[] A_5)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u24F1 spr_u24F = new spr\u24F1((spr\u2158)base.ReservedHandle, this);
					List<XlsWorksheet.RangeProperty> list = new List<XlsWorksheet.RangeProperty>();
					int num = 0;
					int num2 = A_5.Length;
					int num3 = 12;
					for (;;)
					{
						object obj;
						int num5;
						switch (num3)
						{
						case 0:
						{
							if (obj == DBNull.Value)
							{
								num3 = 15;
								continue;
							}
							XlsWorksheet.RangeProperty rangeProperty2;
							XlsWorksheet.RangeProperty rangeProperty = rangeProperty2;
							num3 = 11;
							continue;
						}
						case 1:
							list.Add(XlsWorksheet.RangeProperty.Text);
							num3 = 26;
							continue;
						case 2:
						{
							DataColumn dataColumn;
							if (dataColumn.DataType == typeof(TimeSpan))
							{
								num3 = 30;
								continue;
							}
							list.Add(XlsWorksheet.RangeProperty.Value2);
							num3 = 23;
							continue;
						}
						case 3:
							goto IL_305;
						case 4:
							goto IL_131;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_31A;
							default:
							{
								if (false)
								{
								}
								if (true)
								{
								}
								int num4 = 0;
								num3 = 7;
								continue;
							}
							}
							break;
						case 6:
						{
							int num4;
							num4++;
							num3 = 17;
							continue;
						}
						case 7:
							goto IL_238;
						case 8:
						{
							DataColumn dataColumn;
							if (dataColumn.DataType == typeof(string))
							{
								num3 = 1;
								continue;
							}
							num3 = 19;
							continue;
						}
						case 9:
						{
							if (num >= num2)
							{
								num3 = 5;
								continue;
							}
							DataColumn dataColumn = A_5[num];
							num3 = 8;
							continue;
						}
						case 10:
						{
							int num4;
							if (num4 >= A_3)
							{
								num3 = 31;
								continue;
							}
							DataRow dataRow = A_0.Rows[num4];
							num5 = 0;
							num3 = 21;
							continue;
						}
						case 11:
						{
							XlsWorksheet.RangeProperty rangeProperty;
							switch (rangeProperty)
							{
							case XlsWorksheet.RangeProperty.Text:
								spr_u24F.Text = (string)obj;
								num3 = 13;
								continue;
							case XlsWorksheet.RangeProperty.DateTime:
								spr_u24F.DateTimeValue = (DateTime)obj;
								num3 = 25;
								continue;
							case XlsWorksheet.RangeProperty.TimeSpan:
								spr_u24F.TimeSpanValue = (TimeSpan)obj;
								num3 = 4;
								continue;
							default:
								num3 = 29;
								continue;
							}
							break;
						}
						case 12:
							goto IL_25E;
						case 13:
							goto IL_131;
						case 14:
							goto IL_131;
						case 15:
							spr_u24F.Text = "";
							num3 = 14;
							continue;
						case 16:
							goto IL_305;
						case 17:
							goto IL_238;
						case 18:
							list.Add(XlsWorksheet.RangeProperty.DateTime);
							num3 = 3;
							continue;
						case 19:
						{
							DataColumn dataColumn;
							if (dataColumn.DataType == typeof(DateTime))
							{
								num3 = 18;
								continue;
							}
							num3 = 2;
							continue;
						}
						case 20:
							goto IL_31A;
						case 21:
							goto IL_212;
						case 22:
						{
							if (num5 >= A_4)
							{
								num3 = 6;
								continue;
							}
							DataColumn dataColumn = A_5[num5];
							XlsWorksheet.RangeProperty rangeProperty2 = list[num5];
							int num4;
							spr_u24F.ᜀ(A_1 + num4, A_2 + num5);
							DataRow dataRow;
							obj = dataRow[dataColumn];
							num3 = 0;
							continue;
						}
						case 23:
							goto IL_305;
						case 24:
							goto IL_131;
						case 25:
							goto IL_131;
						case 26:
							goto IL_305;
						case 27:
							goto IL_212;
						case 28:
							goto IL_25E;
						case 29:
							num3 = 20;
							continue;
						case 30:
							list.Add(XlsWorksheet.RangeProperty.TimeSpan);
							num3 = 16;
							continue;
						case 31:
							return;
						}
						break;
						IL_131:
						num5++;
						num3 = 27;
						continue;
						IL_212:
						num3 = 22;
						continue;
						IL_238:
						num3 = 10;
						continue;
						IL_25E:
						num3 = 9;
						continue;
						IL_305:
						num++;
						num3 = 28;
						continue;
						IL_31A:
						spr_u24F.Value2 = obj;
						num3 = 24;
					}
				}
				return;
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0004CF20 File Offset: 0x0004BF20
		private void ᜁ(DataView A_0, int A_1, int A_2, int A_3, int A_4)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_A0;
						case 1:
							if (num >= A_3)
							{
								num2 = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6A;
							default:
							{
								if (false)
								{
								}
								DataRowView dataRowView = A_0[num];
								int num3 = 0;
								num2 = 2;
								continue;
							}
							}
							break;
						case 2:
							goto IL_53;
						case 3:
							goto IL_53;
						case 4:
							goto IL_A0;
						case 5:
						{
							int num3;
							if (num3 >= A_4)
							{
								num2 = 6;
								continue;
							}
							if (true)
							{
							}
							IXLSRange ixlsrange = this.InnerGetCell(A_2 + num3, A_1 + num);
							DataRowView dataRowView;
							ixlsrange.Value2 = dataRowView[num3];
							num3++;
							num2 = 3;
							continue;
						}
						case 6:
							goto IL_6A;
						case 7:
							return;
						}
						break;
						IL_53:
						num2 = 5;
						continue;
						IL_6A:
						num++;
						num2 = 4;
						continue;
						IL_A0:
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0004D030 File Offset: 0x0004C030
		private new void ᜀ(DataView A_0, int A_1, int A_2, int A_3, int A_4)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Dictionary<int, XlsWorksheet.RangeProperty> a_ = new Dictionary<int, XlsWorksheet.RangeProperty>(A_4);
					int num = 0;
					int num2 = 1;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
						{
							object obj;
							if (!(obj is DBNull))
							{
								num2 = 15;
								continue;
							}
							goto IL_7B;
						}
						case 1:
							goto IL_14C;
						case 2:
							goto IL_7B;
						case 3:
						{
							object obj;
							if (obj != null)
							{
								num2 = 8;
								continue;
							}
							goto IL_7B;
						}
						case 4:
							goto IL_7B;
						case 5:
						{
							XlsWorksheet.RangeProperty rangeProperty;
							switch (rangeProperty)
							{
							case XlsWorksheet.RangeProperty.Text:
							{
								object obj;
								IXLSRange ixlsrange;
								ixlsrange.Text = (string)obj;
								num2 = 2;
								continue;
							}
							case XlsWorksheet.RangeProperty.DateTime:
							{
								object obj;
								IXLSRange ixlsrange;
								ixlsrange.DateTimeValue = (DateTime)obj;
								num2 = 16;
								continue;
							}
							case XlsWorksheet.RangeProperty.TimeSpan:
							{
								object obj;
								IXLSRange ixlsrange;
								ixlsrange.TimeSpanValue = (TimeSpan)obj;
								num2 = 4;
								continue;
							}
							}
							goto IL_1CF;
						}
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1CF;
							default:
							{
								if (false)
								{
								}
								object obj;
								IXLSRange ixlsrange;
								ixlsrange.Value2 = obj;
								num2 = 18;
								continue;
							}
							}
							break;
						case 7:
						{
							if (num3 >= A_4)
							{
								num2 = 10;
								continue;
							}
							DataRowView dataRowView;
							object obj = dataRowView[num3];
							num2 = 3;
							continue;
						}
						case 8:
							num2 = 0;
							continue;
						case 9:
							goto IL_1DD;
						case 10:
							num++;
							num2 = 12;
							continue;
						case 11:
							goto IL_16C;
						case 12:
							goto IL_14C;
						case 13:
						{
							if (num >= A_3)
							{
								num2 = 11;
								continue;
							}
							DataRowView dataRowView = A_0[num];
							num3 = 0;
							num2 = 9;
							continue;
						}
						case 14:
							num2 = 6;
							continue;
						case 15:
						{
							object obj;
							XlsWorksheet.RangeProperty rangeProperty2 = this.ᜀ(obj, num3, a_);
							IXLSRange ixlsrange = this.InnerGetCell(A_2 + num3, A_1 + num);
							XlsWorksheet.RangeProperty rangeProperty = rangeProperty2;
							num2 = 5;
							continue;
						}
						case 16:
							goto IL_7B;
						case 17:
							goto IL_1DD;
						case 18:
							goto IL_7B;
						}
						break;
						IL_7B:
						num3++;
						num2 = 17;
						continue;
						IL_14C:
						num2 = 13;
						continue;
						IL_1CF:
						num2 = 14;
						continue;
						IL_1DD:
						num2 = 7;
					}
				}
				IL_16C:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0004D294 File Offset: 0x0004C294
		private new XlsWorksheet.RangeProperty ᜀ(object A_0, int A_1, Dictionary<int, XlsWorksheet.RangeProperty> A_2)
		{
			int a_ = 18;
			int num = 13;
			XlsWorksheet.RangeProperty rangeProperty;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2.ContainsKey(A_1))
					{
						goto IL_165;
					}
					rangeProperty = XlsWorksheet.RangeProperty.Value2;
					num = 7;
					continue;
				case 1:
					goto IL_172;
				case 2:
					goto IL_172;
				case 3:
					goto IL_C4;
				case 4:
					goto IL_60;
				case 5:
					if (A_0 is DateTime)
					{
						num = 14;
						continue;
					}
					num = 6;
					continue;
				case 6:
					if (A_0 is TimeSpan)
					{
						num = 9;
						continue;
					}
					goto IL_172;
				case 7:
					if (A_0 is string)
					{
						num = 8;
						continue;
					}
					num = 5;
					continue;
				case 8:
					rangeProperty = XlsWorksheet.RangeProperty.Text;
					num = 12;
					continue;
				case 9:
					rangeProperty = XlsWorksheet.RangeProperty.TimeSpan;
					num = 2;
					continue;
				case 10:
					goto IL_170;
				case 11:
					if (A_2 == null)
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				case 12:
					goto IL_172;
				case 14:
					rangeProperty = XlsWorksheet.RangeProperty.DateTime;
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 11;
				continue;
				IL_165:
				num = 10;
				continue;
				IL_172:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_165;
				default:
					goto IL_188;
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("㹇⭉⁋㭍㕏", a_));
			IL_C4:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁇⭉㽋♍ፏ㵑㡓⍕㕗㑙࡛❝ၟݡᝣ", a_));
			IL_170:
			return A_2[A_1];
			IL_188:
			if (false)
			{
			}
			A_2.Add(A_1, rangeProperty);
			return rangeProperty;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0004D438 File Offset: 0x0004C438
		public override void SerializeDataToList(RecordArrayList records)
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
			this.ᜀ(records, false);
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0004D47C File Offset: 0x0004C47C
		protected override bool ContainsProtection
		{
			get
			{
				while (!base.ContainsProtection)
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
						return this.ᜮ.Count > 0;
					}
				}
				return true;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0004D4D4 File Offset: 0x0004C4D4
		private void ᜁ(RecordArrayList A_0)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 17;
				for (;;)
				{
					int num2;
					spr\u1A5D spr_u1A5D;
					int num4;
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						num2 = 0;
						int num3 = this.ᜮ.Count;
						num = 13;
						continue;
					}
					case 2:
						if (spr_u1A5D.GetStoreSize(ExcelVersion.Version97to2003) > 8224)
						{
							num = 9;
							continue;
						}
						goto IL_223;
					case 3:
						goto IL_190;
					case 4:
					{
						int count;
						if (num4 >= count)
						{
							num = 14;
							continue;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_113;
						default:
						{
							if (false)
							{
							}
							spr_u1A5D = (spr\u1A5D)spr\u175E.ᜀ(TBIFFRecord.RangeProtection);
							spr\u1F7E spr_u1F7E = this.ᜮ[num4];
							spr_u1F7E.ᜄ();
							spr_u1A5D.ᜀ(spr_u1F7E.ᜁ());
							spr_u1A5D.ᜀ(spr_u1F7E);
							num = 2;
							continue;
						}
						}
						break;
					}
					case 5:
						if (this.m_book.IsLoaded)
						{
							num = 25;
							continue;
						}
						goto IL_27A;
					case 6:
						goto IL_240;
					case 7:
						goto IL_28E;
					case 8:
					{
						spr\u1F7E spr_u1F7E2;
						this.ᜮ.Remove(spr_u1F7E2);
						num2--;
						int num3;
						num3--;
						num = 6;
						continue;
					}
					case 9:
						num = 5;
						continue;
					case 10:
					{
						if (this.ᜮ.Count == 0)
						{
							num = 3;
							continue;
						}
						spr\u22A0 spr_u22A = base.SheetProtection;
						num = 18;
						continue;
					}
					case 11:
						if (this.ᜮ != null)
						{
							num = 21;
							continue;
						}
						return;
					case 12:
					{
						spr\u22A0 spr_u22A;
						if ((spr_u22A.ᜁ() | 1024) != 0)
						{
							num = 1;
							continue;
						}
						goto IL_3CE;
					}
					case 13:
						goto IL_100;
					case 14:
						return;
					case 15:
						num = 12;
						continue;
					case 16:
						goto IL_223;
					case 18:
					{
						spr\u22A0 spr_u22A;
						if (spr_u22A != null)
						{
							num = 20;
							continue;
						}
						goto IL_3CE;
					}
					case 19:
					{
						spr\u22A0 spr_u22A;
						if (spr_u22A.ᜀ())
						{
							num = 15;
							continue;
						}
						goto IL_3CE;
					}
					case 20:
						num = 19;
						continue;
					case 21:
						num = 10;
						continue;
					case 22:
					{
						if (this.ᜮ.Count == 0)
						{
							num = 0;
							continue;
						}
						spr\u22A0 spr_u22A = (spr\u22A0)spr\u175E.ᜀ(TBIFFRecord.SheetProtection);
						spr_u22A.ᜀ(3);
						A_0.Add(spr_u22A.Clone());
						num4 = 0;
						int count = this.ᜮ.Count;
						num = 24;
						continue;
					}
					case 23:
						goto IL_100;
					case 24:
						goto IL_28E;
					case 25:
					{
						spr\u1F7E spr_u1F7E;
						spr_u1F7E.ᜀ(1024);
						num = 16;
						continue;
					}
					case 26:
						goto IL_B6;
					case 27:
					{
						int num3;
						if (num2 >= num3)
						{
							goto IL_113;
						}
						spr\u1F7E spr_u1F7E2 = this.ᜮ[num2];
						num = 29;
						continue;
					}
					case 28:
						goto IL_3CE;
					case 29:
					{
						spr\u1F7E spr_u1F7E2;
						if ((spr_u1F7E2.ᜁ() & IgnoreErrorType.UnlockedFormulaCells) != IgnoreErrorType.None)
						{
							num = 8;
							continue;
						}
						goto IL_240;
					}
					}
					if (A_0 == null)
					{
						num = 26;
						continue;
					}
					num = 11;
					continue;
					IL_100:
					num = 27;
					continue;
					IL_113:
					num = 28;
					continue;
					IL_223:
					A_0.ᜀ(spr_u1A5D);
					num4++;
					num = 7;
					continue;
					IL_240:
					num2++;
					num = 23;
					continue;
					IL_28E:
					num = 4;
					continue;
					IL_3CE:
					num = 22;
				}
				IL_B6:
				throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ㡊", a_));
				IL_190:
				return;
				IL_27A:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("款⹀ⱂ敄⩆⡈╊㑌潎⍐㙒㉔㹖㙘㕚⹜罞ᙠ੢ᅤས䥨๪ὬᵮṰŲ啴Ṷ᝸ὺᑼ᱾愈ꖊ권\udf8e﶐ﲘ뮚爵얠횢욤슦覨\udfaa얬쪮\udcb0鎲ힴ튶\udfb8풺쾼\udabe냂꓄뇆ꃈꗊ꫌", a_));
			}
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0004D8DC File Offset: 0x0004C8DC
		private new void ᜀ(RecordArrayList A_0)
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
			throw new NotImplementedException();
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0004D91C File Offset: 0x0004C91C
		internal void ᜆ(RecordArrayList A_0)
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
			this.ᜀ(A_0, true);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0004D960 File Offset: 0x0004C960
		internal new void ᜅ(RecordArrayList A_0)
		{
			int a_ = 9;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 < 255)
					{
						num = 1;
						continue;
					}
					return;
				}
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_100;
					default:
					{
						if (false)
						{
						}
						spr\u216E spr_u216E = (spr\u216E)spr\u175E.ᜀ(TBIFFRecord.ColumnInfo);
						int num2;
						spr_u216E.ᜄ((ushort)(num2 + 1));
						spr_u216E.ᜀ(255);
						spr_u216E.ᜅ((ushort)this.ᜊ((int)(this.DefaultColumnWidth * 256.0)));
						spr_u216E.ᜃ((ushort)this.m_book.DefaultXFIndex);
						A_0.ᜀ(spr_u216E);
						num = 3;
						continue;
					}
					}
					break;
				case 2:
					goto IL_45;
				case 3:
					goto IL_CB;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					int num2 = this.ᜃ(A_0);
					num = 0;
				}
			}
			IL_45:
			goto IL_100;
			IL_CB:
			return;
			IL_100:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ㡊", a_));
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0004DA84 File Offset: 0x0004CA84
		internal new int ᜃ(RecordArrayList A_0)
		{
			switch (0)
			{
			default:
			{
				int num2;
				for (;;)
				{
					int num = 1;
					num2 = 1;
					spr\u216E spr_u216E = null;
					spr\u216E spr_u216E2 = null;
					int num3 = 5;
					for (;;)
					{
						spr\u216E spr_u216E3;
						switch (num3)
						{
						case 0:
							if (num2 <= 256)
							{
								num3 = 8;
								continue;
							}
							goto IL_2CD;
						case 1:
							if (spr_u216E2 == null)
							{
								num3 = 9;
								continue;
							}
							goto IL_95;
						case 2:
							if (spr_u216E == null)
							{
								num3 = 22;
								continue;
							}
							goto IL_FF;
						case 3:
							goto IL_2CD;
						case 4:
							if (spr_u216E != null)
							{
								num3 = 20;
								continue;
							}
							goto IL_2F0;
						case 5:
							goto IL_17D;
						case 6:
							goto IL_1F5;
						case 7:
							goto IL_FF;
						case 8:
							num3 = 24;
							continue;
						case 9:
							num2--;
							spr_u216E2 = this.ᜐ[num2];
							num3 = 25;
							continue;
						case 10:
							goto IL_1CD;
						case 11:
							if (num > 256)
							{
								num3 = 7;
								continue;
							}
							spr_u216E = this.ᜐ[num];
							num3 = 2;
							continue;
						case 12:
							goto IL_1F5;
						case 13:
							spr_u216E3 = (spr\u216E)spr_u216E.Clone();
							num3 = 12;
							continue;
						case 14:
							if (num > 256)
							{
								num3 = 17;
								continue;
							}
							goto IL_1CD;
						case 15:
							goto IL_24B;
						case 16:
							if (spr_u216E.ᜀ(spr_u216E2) != 0)
							{
								goto IL_270;
							}
							goto IL_1A5;
						case 17:
							goto IL_1A0;
						case 18:
							goto IL_1A5;
						case 19:
							spr_u216E2 = null;
							num3 = 18;
							continue;
						case 20:
							num2 = num;
							num3 = 15;
							continue;
						case 21:
							if (num == num2)
							{
								num3 = 13;
								continue;
							}
							spr_u216E3 = (spr\u216E)spr_u216E.Clone();
							spr_u216E3.ᜀ(spr_u216E2.ᜀ());
							num3 = 6;
							continue;
						case 22:
							num++;
							num3 = 10;
							continue;
						case 23:
							goto IL_17D;
						case 24:
							if (spr_u216E2 == null)
							{
								num3 = 3;
								continue;
							}
							goto IL_24B;
						case 25:
							goto IL_95;
						}
						break;
						IL_95:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_270:
							num3 = 19;
							continue;
						default:
							if (false)
							{
							}
							num3 = 21;
							continue;
						}
						IL_FF:
						num3 = 4;
						continue;
						IL_17D:
						num3 = 14;
						continue;
						IL_1A5:
						num3 = 0;
						continue;
						IL_1CD:
						num3 = 11;
						continue;
						IL_1F5:
						spr_u216E3.ᜅ((ushort)this.ᜊ((int)spr_u216E3.ᜉ()));
						A_0.ᜀ(spr_u216E3);
						num = num2 + 1;
						if (true)
						{
						}
						num3 = 23;
						continue;
						IL_24B:
						num2++;
						spr_u216E2 = this.ᜐ[num2];
						num3 = 16;
						continue;
						IL_2CD:
						num3 = 1;
					}
				}
				IL_1A0:
				IL_2F0:
				return num2 - 1;
			}
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0004DD84 File Offset: 0x0004CD84
		internal new void ᜄ(RecordArrayList A_0)
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
			this.ᜪ.SerializeDataToList(A_0);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0004DDCC File Offset: 0x0004CDCC
		internal new void ᜂ(RecordArrayList A_0)
		{
			int a_ = 2;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_85;
				case 2:
					return;
				case 3:
				{
					if (this.\u171F == null)
					{
						num = 2;
						continue;
					}
					int num2 = 0;
					int count = this.\u171F.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9E;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				case 4:
					if (true)
					{
					}
					goto IL_85;
				case 5:
					return;
				case 6:
					goto IL_44;
				case 7:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					XlsDataValidationCollection xlsDataValidationCollection = this.\u171F[num2];
					xlsDataValidationCollection.SerializeDataToList(A_0);
					num2++;
					num = 4;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				goto IL_9E;
				IL_85:
				num = 7;
				continue;
				IL_9E:
				num = 3;
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0004DED8 File Offset: 0x0004CED8
		private bool ᜁ(sprᡣ A_0, sprᡣ A_1)
		{
			int num = 26;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0.\u170D() == A_1.\u170D())
					{
						num = 3;
						continue;
					}
					return false;
				case 3:
					num = 21;
					continue;
				case 4:
					if (Ptg.CompareArrays(A_0.\u1713(), A_1.\u1713()))
					{
						num = 13;
						continue;
					}
					return false;
				case 5:
					num = 10;
					continue;
				case 6:
					num = 7;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_212;
					default:
						if (false)
						{
						}
						if (A_0.ᜄ() == A_1.ᜄ())
						{
							num = 18;
							continue;
						}
						return false;
					}
					break;
				case 8:
					if (A_0.ᜎ() == A_1.ᜎ())
					{
						num = 1;
						continue;
					}
					return false;
				case 9:
					if (A_0.\u1712() == A_1.\u1712())
					{
						num = 6;
						continue;
					}
					return false;
				case 10:
					if (A_0.ᜁ() == A_1.ᜁ())
					{
						num = 27;
						continue;
					}
					return false;
				case 11:
					goto IL_91;
				case 12:
					num = 25;
					continue;
				case 13:
					goto IL_261;
				case 14:
					if (A_0.ᜈ() == A_1.ᜈ())
					{
						num = 12;
						continue;
					}
					return false;
				case 15:
					if (A_0.ᜐ() == A_1.ᜐ())
					{
						num = 22;
						continue;
					}
					return false;
				case 16:
					num = 14;
					continue;
				case 17:
					if (true)
					{
					}
					num = 23;
					continue;
				case 18:
					num = 15;
					continue;
				case 19:
					if (A_0.ᜇ() == A_1.ᜇ())
					{
						num = 24;
						continue;
					}
					return false;
				case 20:
					if (A_0.ᜊ() == A_1.ᜊ())
					{
						num = 0;
						continue;
					}
					return false;
				case 21:
					goto IL_212;
				case 22:
					num = 20;
					continue;
				case 23:
					if (A_0.ᜃ() == A_1.ᜃ())
					{
						num = 5;
						continue;
					}
					return false;
				case 24:
					num = 9;
					continue;
				case 25:
					if (A_0.ᜋ() == A_1.ᜋ())
					{
						num = 17;
						continue;
					}
					return false;
				case 27:
					num = 19;
					continue;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 8;
				continue;
				IL_212:
				if (!(A_0.ᜀ() == A_1.ᜀ()))
				{
					return false;
				}
				num = 16;
			}
			IL_91:
			return A_1 == null;
			IL_261:
			return Ptg.CompareArrays(A_0.\u1714(), A_1.\u1714());
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0004E210 File Offset: 0x0004D210
		private new void ᜀ(sprᡣ A_0, sprᡣ A_1)
		{
			int a_ = 17;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					goto IL_81;
				case 2:
					goto IL_58;
				case 3:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					goto IL_A1;
				}
				IL_31:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
					break;
				}
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑆㱈㥊ौ㥎", a_));
			IL_81:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆㽈Ὂ≌๎㕐㝒", a_));
			IL_A1:
			A_0.ᜀ(A_1.ᜑ());
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0004E2CC File Offset: 0x0004D2CC
		public override void SerializeMsoDrawings(RecordArrayList records)
		{
			int a_ = 3;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								num = 4;
								continue;
							case 2:
							{
								IEnumerator<spr\u2114> enumerator;
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								spr\u2114 a_2 = enumerator.Current;
								records.ᜀ(a_2);
								num = 3;
								continue;
							}
							case 4:
								goto IL_C5;
							}
							IL_8A:
							num = 2;
							continue;
							goto IL_8A;
						}
						IL_C5:
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							IEnumerator<spr\u2114> enumerator;
							switch (num)
							{
							case 0:
								enumerator.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_101;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 0;
						}
						IL_101:;
					}
					goto IL_104;
				case 1:
					goto IL_104;
				case 2:
					if (this.\u171D != null)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					return;
				case 3:
					goto IL_5F;
				case 4:
					if ((base.ReservedHandle.\u1712() & SkipExtRecordsType.Drawings) != SkipExtRecordsType.Drawings)
					{
						num = 1;
						continue;
					}
					return;
				case 6:
				{
					IEnumerator<spr\u2114> enumerator = this.\u171D.Values.GetEnumerator();
					num = 0;
					continue;
				}
				}
				if (records == null)
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
						num = 3;
						continue;
					}
				}
				else
				{
					base.SerializeMsoDrawings(records);
				}
				num = 4;
				continue;
				IL_104:
				num = 2;
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺帼倾㍀❂㙄", a_));
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0004E488 File Offset: 0x0004D488
		[CLSCompliant(false)]
		private new void ᜀ(RecordArrayList A_0, bool A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 33;
				for (;;)
				{
					spr\u203C spr_u203C;
					switch (num)
					{
					case 0:
						goto IL_28D;
					case 1:
						goto IL_5C7;
					case 2:
						this.ᜡ.ᜀ(A_0);
						num = 65;
						continue;
					case 3:
						base.WindowTwo.ᜂ((ushort)this.ZoomScalePageBreakView);
						num = 13;
						continue;
					case 4:
						if (A_0 == null)
						{
							num = 19;
							continue;
						}
						num = 46;
						continue;
					case 5:
						if (this.ᜫ != null)
						{
							num = 39;
							continue;
						}
						goto IL_3B3;
					case 6:
						goto IL_17E;
					case 7:
						goto IL_7CB;
					case 8:
						A_0.AddRange(this.\u1733);
						num = 58;
						continue;
					case 9:
					{
						spr\u218B spr_u218B;
						List<spr\u2466> list;
						spr_u218B.ᜀ(list);
						num = 56;
						continue;
					}
					case 10:
						goto IL_897;
					case 11:
						if (this.VerticalSplit == 0)
						{
							num = 35;
							continue;
						}
						goto IL_737;
					case 12:
						goto IL_7E6;
					case 13:
						goto IL_6A3;
					case 14:
						A_0.AddList(this.ᜩ);
						num = 1;
						continue;
					case 15:
					{
						spr\u218B spr_u218B;
						spr_u218B.ᜁ((this.m_iLastRow == this.m_iFirstRow && this.m_iFirstRow == -1) ? 0 : (this.m_iFirstRow - 1));
						num = 60;
						continue;
					}
					case 16:
						goto IL_83E;
					case 17:
						goto IL_7CB;
					case 18:
						if (this.\u1733 != null)
						{
							num = 8;
							continue;
						}
						goto IL_8FD;
					case 19:
						goto IL_199;
					case 20:
						if (this.VerticalSplit == 0)
						{
							num = 22;
							continue;
						}
						num = 57;
						continue;
					case 21:
						if (this.ᜡ != null)
						{
							num = 2;
							continue;
						}
						goto IL_603;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7E6;
						default:
							if (false)
							{
							}
							this.\u1717.ᜀ(2);
							num = 17;
							continue;
						}
						break;
					case 23:
					{
						int num3;
						int num2 = num3 % 32;
						int num4 = num3 / 32;
						num = 41;
						continue;
					}
					case 24:
						if (this.\u1717 != null)
						{
							num = 68;
							continue;
						}
						goto IL_7E6;
					case 25:
						if (this.HorizontalSplit == 0)
						{
							num = 44;
							continue;
						}
						goto IL_737;
					case 26:
					{
						if (!base.IsParsed)
						{
							num = 64;
							continue;
						}
						this.\u171E.ᜀ(sprḯ.TType.TYPE_WORKSHEET);
						A_0.ᜀ(this.\u171E);
						spr\u218B spr_u218B = null;
						int num3 = this.m_iLastRow - this.m_iFirstRow + 1;
						int num4 = 0;
						num = 36;
						continue;
					}
					case 27:
						goto IL_7CB;
					case 28:
						goto IL_1CF;
					case 29:
						if (this.ViewMode == ViewMode.Preview)
						{
							num = 3;
							continue;
						}
						goto IL_6A3;
					case 30:
					{
						int num4;
						num4++;
						num = 0;
						continue;
					}
					case 31:
						if (this.ᜣ != null)
						{
							num = 67;
							continue;
						}
						goto IL_42F;
					case 32:
						this.\u171C.Clear();
						this.\u171D.Clear();
						num = 38;
						continue;
					case 34:
						base.WindowTwo.ᜁ((ushort)this.ZoomScaleNormal);
						num = 49;
						continue;
					case 35:
						num = 25;
						continue;
					case 36:
					{
						int num3;
						if (num3 > 0)
						{
							num = 23;
							continue;
						}
						goto IL_28D;
					}
					case 37:
						spr_u203C.ᜁ((this.m_iLastRow == this.m_iFirstRow && this.m_iFirstRow == -1) ? 0 : (this.m_iFirstRow - 1));
						num = 69;
						continue;
					case 38:
						return;
					case 39:
						this.ᜫ.ᜀ(A_0);
						num = 53;
						continue;
					case 40:
						this.\u171C.Clear();
						this.\u171D.Clear();
						num = 6;
						continue;
					case 41:
					{
						int num2;
						if (num2 != 0)
						{
							num = 30;
							continue;
						}
						goto IL_28D;
					}
					case 42:
						if (!A_1)
						{
							num = 66;
							continue;
						}
						goto IL_359;
					case 43:
						if (!A_1)
						{
							num = 9;
							continue;
						}
						goto IL_584;
					case 44:
						this.\u1717.ᜀ(3);
						num = 7;
						continue;
					case 45:
						goto IL_359;
					case 46:
						if (!base.IsSupported)
						{
							num = 28;
							continue;
						}
						num = 26;
						continue;
					case 47:
						if (this.ᜩ != null)
						{
							num = 14;
							continue;
						}
						goto IL_5C7;
					case 48:
						goto IL_42F;
					case 49:
						goto IL_660;
					case 50:
						if (this.ViewMode == ViewMode.Normal)
						{
							num = 34;
							continue;
						}
						goto IL_660;
					case 51:
						if (this.ᜢ != null)
						{
							num = 62;
							continue;
						}
						goto IL_897;
					case 52:
						this.\u1714.ᜀ(A_0);
						num = 16;
						continue;
					case 53:
						goto IL_3B3;
					case 54:
						if (this.\u1714 != null)
						{
							num = 52;
							continue;
						}
						goto IL_83E;
					case 55:
						spr_u203C.ᜀ((this.m_iLastColumn == this.m_iFirstColumn && this.m_iFirstColumn == int.MaxValue) ? 0 : ((ushort)this.m_iLastColumn));
						num = 37;
						continue;
					case 56:
						goto IL_584;
					case 57:
						if (this.HorizontalSplit == 0)
						{
							num = 63;
							continue;
						}
						goto IL_7CB;
					case 58:
						goto IL_8FD;
					case 59:
						spr_u203C.ᜀ((this.m_iLastRow == this.m_iFirstRow && this.m_iFirstRow == -1) ? 0 : this.m_iLastRow);
						num = 55;
						continue;
					case 60:
					{
						spr\u218B spr_u218B;
						spr_u218B.ᜀ((this.m_iLastRow == this.m_iFirstRow && this.m_iFirstRow == -1) ? 0 : this.m_iLastRow);
						A_0.ᜀ(spr_u218B);
						if (true)
						{
						}
						num = 45;
						continue;
					}
					case 61:
						if (this.\u171C != null)
						{
							num = 32;
							continue;
						}
						return;
					case 62:
						this.ᜢ.ᜀ(A_0);
						num = 10;
						continue;
					case 63:
						this.\u1717.ᜀ(1);
						num = 27;
						continue;
					case 64:
						goto IL_1FE;
					case 65:
						goto IL_603;
					case 66:
					{
						spr\u218B spr_u218B = (spr\u218B)spr\u175E.ᜀ(TBIFFRecord.Index);
						int num4;
						spr_u218B.ᜀ(new int[num4]);
						num = 15;
						continue;
					}
					case 67:
						A_0.AddList(this.ᜣ);
						num = 48;
						continue;
					case 68:
						num = 11;
						continue;
					case 69:
					{
						spr_u203C.ᜁ((ushort)((this.m_iLastColumn == this.m_iFirstColumn && this.m_iFirstColumn == int.MaxValue) ? 0 : (this.m_iFirstColumn - 1)));
						A_0.ᜀ(spr_u203C);
						List<spr\u2466> list = new List<spr\u2466>();
						int num4 = this.ᜏ.ᜀ(A_0, list);
						num = 43;
						continue;
					}
					}
					if (this.\u171C != null)
					{
						num = 40;
						continue;
					}
					IL_17E:
					num = 4;
					continue;
					IL_28D:
					num = 42;
					continue;
					IL_359:
					this.m_book.InnerCalculation.ᜀ(A_0);
					A_0.ᜀ(this.\u1712);
					num = 5;
					continue;
					IL_3B3:
					this.SerializeProtection(A_0, false);
					sprᱎ sprᱎ = (sprᱎ)spr\u175E.ᜀ(TBIFFRecord.DefaultColWidth);
					sprᱎ.ᜀ((ushort)this.\u1713);
					A_0.ᜀ(sprᱎ);
					this.ᜅ(A_0);
					this.ᜠ.SerializeDataToList(A_0);
					num = 31;
					continue;
					IL_42F:
					spr_u203C = (spr\u203C)spr\u175E.ᜀ(TBIFFRecord.Dimensions);
					num = 59;
					continue;
					IL_584:
					this.SerializeMsoDrawings(A_0);
					num = 47;
					continue;
					IL_5C7:
					num = 21;
					continue;
					IL_603:
					this.SerializeHeaderFooterPictures(A_0);
					base.WindowTwo.ᜅ(this.ViewMode == ViewMode.Preview);
					num = 50;
					continue;
					IL_660:
					num = 29;
					continue;
					IL_6A3:
					this.SerializeWindowTwo(A_0);
					base.ᜐ(A_0);
					num = 24;
					continue;
					IL_737:
					num = 20;
					continue;
					IL_7CB:
					A_0.ᜀ(this.\u1717);
					num = 12;
					continue;
					IL_7E6:
					this.ᜁ();
					A_0.AddList(this.\u1716);
					num = 54;
					continue;
					IL_83E:
					A_0.AddList(this.PreserveExternalConnection);
					A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.UnkMarker));
					num = 51;
					continue;
					IL_897:
					this.ᜄ(A_0);
					this.ᜂ(A_0);
					this.SerializeMacrosSupport(A_0);
					base.ᜑ(A_0);
					base.ᜏ(A_0);
					this.ᜁ(A_0);
					num = 18;
					continue;
					IL_8FD:
					A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.EOF));
					num = 61;
				}
				IL_199:
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
				IL_1CF:
				A_0.AddList(this.ᜎ);
				return;
				IL_1FE:
				this.ᜀ(A_0);
				return;
			}
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0004EDE8 File Offset: 0x0004DDE8
		protected void RaiseColumnWidthChangedEvent(int iColumn, double dNewValue)
		{
			int a_ = 0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_80;
				case 2:
				{
					if (true)
					{
					}
					XlsEventArgs e = new XlsEventArgs(iColumn, dNewValue, RecordTableEnumerator.b("电圷嘹䤻匽⸿ᕁⵃ≅㱇≉", a_));
					this.ᝇ(this, e);
					goto IL_6C;
				}
				}
				if (this.ᝇ != null)
				{
					num = 2;
					continue;
				}
				goto IL_80;
				IL_6C:
				num = 1;
				continue;
				IL_80:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6C;
				default:
					goto IL_96;
				}
			}
			IL_96:
			if (false)
			{
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0004EE94 File Offset: 0x0004DE94
		protected void RaiseRowHeightChangedEvent(int iRow, double dNewValue)
		{
			int a_ = 11;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6E;
				case 1:
				{
					XlsEventArgs e = new XlsEventArgs(iRow, dNewValue, RecordTableEnumerator.b("ፀⱂ㉄ཆⱈ≊⩌❎═", a_));
					this.ᝈ(this, e);
					goto IL_64;
				}
				}
				if (this.ᝈ != null)
				{
					num = 1;
					continue;
				}
				goto IL_6E;
				IL_64:
				num = 0;
				continue;
				IL_6E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_64;
				default:
					goto IL_8E;
				}
			}
			IL_8E:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0004EF40 File Offset: 0x0004DF40
		private new void ᜀ(object A_0, EventArgs A_1)
		{
			int num = 3;
			for (;;)
			{
				sprᱧ sprᱧ;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					goto IL_B2;
				case 2:
					if (sprᱧ != null)
					{
						num = 4;
						continue;
					}
					goto IL_52;
				case 4:
					num = 5;
					continue;
				case 5:
					goto IL_68;
				case 6:
					return;
				case 7:
					goto IL_B2;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_68;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.AutoFitRow(num2);
						num = 0;
						continue;
					}
					break;
				case 9:
					num2 = this.m_iFirstRow;
					num = 1;
					continue;
				case 10:
					if (num2 > this.m_iLastRow)
					{
						num = 6;
						continue;
					}
					sprᱧ = sprᜑ.ᜀ(this, num2, false);
					num = 2;
					continue;
				}
				if (this.m_iFirstRow > 0)
				{
					num = 9;
					continue;
				}
				break;
				IL_52:
				num2++;
				num = 7;
				continue;
				IL_68:
				if (!sprᱧ.\u1713())
				{
					num = 8;
					continue;
				}
				goto IL_52;
				IL_B2:
				num = 10;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0004F06C File Offset: 0x0004E06C
		public void SetCellValue(int rowIndex, int columnIndex, string stringValue)
		{
			int a_ = 0;
			int num = 9;
			double value;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_10C;
				case 1:
					if (stringValue[0] == '=')
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_CF;
					}
					break;
				case 3:
					goto IL_4C;
				case 4:
					goto IL_13C;
				case 5:
					if (stringValue[0] == '#')
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				case 6:
					if (double.TryParse(stringValue, out value))
					{
						num = 2;
						continue;
					}
					goto IL_152;
				case 7:
					goto IL_87;
				case 8:
					if (stringValue.Length == 0)
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				}
				if (stringValue == null)
				{
					num = 3;
				}
				else
				{
					num = 8;
				}
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_));
			IL_87:
			this.ᜁ(rowIndex, columnIndex);
			return;
			IL_CF:
			if (false)
			{
			}
			this.SetNumber(rowIndex, columnIndex, value);
			return;
			IL_10C:
			this.SetError(rowIndex, columnIndex, stringValue, true);
			return;
			IL_13C:
			if (true)
			{
			}
			this.SetFormula(rowIndex, columnIndex, stringValue.Substring(1));
			return;
			IL_152:
			this.ᜀ(rowIndex, columnIndex, stringValue);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0004F1D4 File Offset: 0x0004E1D4
		public void SetValue(int rowIndex, int columnIndex, string stringValue)
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
			this.SetCellValue(rowIndex, columnIndex, stringValue);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0004F218 File Offset: 0x0004E218
		public void SetCellValue(int rowIndex, int columnIndex, bool boolValue)
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
			spr\u249B spr_u249B = (spr\u249B)this.ᜀ(TBIFFRecord.BoolErr, rowIndex, columnIndex);
			spr_u249B.ᜀ(false);
			spr_u249B.ᜀ(boolValue ? 1 : 0);
			this.ᜀ(columnIndex, rowIndex, spr_u249B);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0004F288 File Offset: 0x0004E288
		public void SetNumber(int iRow, int iColumn, double value)
		{
			int a_ = this.ᜀ(iRow, iColumn);
			sprỔ sprỔ = this.ᜁ(iRow, iColumn, value, a_);
			if (sprỔ == null)
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
					this.ᜀ(iRow, iColumn, value, a_);
					return;
				}
			}
			if (true)
			{
			}
			this.ᜀ(iColumn, iRow, sprỔ);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0004F2F0 File Offset: 0x0004E2F0
		public void SetBoolean(int iRow, int iColumn, bool value)
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
			int a_ = this.ᜀ(iRow, iColumn);
			spr\u249B spr_u249B = (spr\u249B)this.ᜀ(TBIFFRecord.BoolErr, iRow, iColumn, a_);
			spr_u249B.ᜀ(false);
			spr_u249B.ᜀ(value ? 1 : 0);
			this.ᜀ(iColumn, iRow, spr_u249B);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0004F36C File Offset: 0x0004E36C
		public void SetText(int iRow, int iColumn, string value)
		{
			int a_ = 2;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_88;
				case 2:
					if (value.Length != 0)
					{
						goto IL_8A;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					num = 2;
					continue;
				}
				if (value == null)
				{
					break;
				}
				num = 3;
			}
			IL_36:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氷弹䐻䨽怿㑁╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟ౡᅣ੥ѧ䩩ͫᱭ偯᝱ᥳٵ౷͹", a_));
			IL_88:
			goto IL_36;
			IL_8A:
			if (true)
			{
			}
			this.ᜀ(iRow, iColumn, value);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0004F414 File Offset: 0x0004E414
		public void SetFormula(int iRow, int iColumn, string value)
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
			this.SetFormula(iRow, iColumn, value, false);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0004F45C File Offset: 0x0004E45C
		public void SetFormula(int iRow, int iColumn, string value, bool bIsR1C1)
		{
			int a_ = 2;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					if (true)
					{
					}
					goto IL_81;
				case 2:
					if (value[0] == '=')
					{
						num = 0;
						continue;
					}
					goto IL_BA;
				case 3:
					num = 2;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						if (value.Length != 0)
						{
							num = 3;
							continue;
						}
						goto IL_50;
					}
					break;
				}
				if (value != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_81:
				num = 5;
			}
			IL_50:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氷弹䐻䨽怿㑁╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟ౡᅣ੥ѧ䩩ͫᱭ偯᝱ᥳٵ౷͹剻幽왿ﲇꪉﾋﶏﮓ歹뢗瀞뺝욟춡횣쮥\udda7용춫躭펯펱\udab3\ud8b5ힷ캹鲻\udcbdꖿ﯅", a_));
			IL_7F:
			goto IL_50;
			IL_BA:
			this.ᜀ(iRow, iColumn, value, bIsR1C1);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0004F530 File Offset: 0x0004E530
		public void SetError(int iRow, int iColumn, string value)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					if (value[0] != '#')
					{
						num = 2;
						continue;
					}
					goto IL_BA;
				case 2:
					goto IL_7F;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						if (value.Length != 0)
						{
							num = 4;
							continue;
						}
						goto IL_50;
					}
					break;
				case 4:
					num = 1;
					continue;
				case 5:
					goto IL_81;
				}
				if (value != null)
				{
					num = 5;
					continue;
				}
				break;
				IL_81:
				num = 3;
			}
			IL_50:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("渹夻䘽㐿扁㉃❅⑇㽉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡੣፥ѧ٩䱫ŭɯ剱ᅳ᭵ࡷ๹ջ偽ꁿ쒁ﮇﺉ겋ﶍﾑ秊몙펟횡蒣쒥춧誩讫趭鞯", a_));
			IL_7F:
			goto IL_50;
			IL_BA:
			this.SetError(iRow, iColumn, value, false);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0004F604 File Offset: 0x0004E604
		public void SetBlank(int iRow, int iColumn)
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
			this.ᜁ(iRow, iColumn);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0004F648 File Offset: 0x0004E648
		private void ᜁ(int A_0, int A_1)
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
			int a_ = this.ᜀ(A_0, A_1);
			BiffRecordRaw a_2 = this.ᜀ(TBIFFRecord.Blank, A_0, A_1, a_);
			this.ᜀ(A_1, A_0, a_2);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0004F6A4 File Offset: 0x0004E6A4
		private new void ᜀ(int A_0, int A_1, double A_2, int A_3)
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
			spr\u19FF spr_u19FF = (spr\u19FF)this.ᜀ(TBIFFRecord.Number, A_0, A_1, A_3);
			spr_u19FF.ᜀ(A_2);
			this.ᜀ(A_1, A_0, spr_u19FF);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0004F704 File Offset: 0x0004E704
		private void ᜁ(int A_0, int A_1, double A_2)
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
			sprỔ sprỔ = (sprỔ)this.ᜀ(TBIFFRecord.RK, A_0, A_1);
			sprỔ.ᜁ(A_2);
			this.ᜀ(A_1, A_0, sprỔ);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0004F764 File Offset: 0x0004E764
		private new void ᜀ(int A_0, int A_1, string A_2, bool A_3)
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
			int a_ = this.ᜀ(A_0, A_1);
			spr᱒ spr᱒ = (spr᱒)this.ᜀ(TBIFFRecord.Formula, A_0, A_1, a_);
			spr᱒.ᜁ(this.m_book.FormulaUtil.ᜀ(A_2, this, null, A_0 - 1, A_1 - 1, A_3));
			this.ᜀ(A_1, A_0, spr᱒);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0004F7E4 File Offset: 0x0004E7E4
		public void SetFormulaNumberValue(int iRow, int iColumn, double value)
		{
			int a_ = 13;
			XlsWorksheet.TRangeValueType cellType = this.GetCellType(iRow, iColumn, false);
			if ((cellType & XlsWorksheet.TRangeValueType.Formula) == XlsWorksheet.TRangeValueType.Formula)
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
					if (true)
					{
					}
					this.ᜀ(iRow, iColumn, value);
					return;
				}
			}
			throw new ArgumentException(RecordTableEnumerator.b("B⑄⥆❈⑊㥌潎≐㙒⅔⑖祘㵚㉜ⵞౠᙢ।٦䥨ᵪ౬ͮѰᙲ啴Ṷ᝸孺Ṽ᩾ꖄ歷꾎ﲒ벚뾞슠첢쮤펦좨슪쎬辮ힰ\udcb2잴\udab6첸ힺ\udcbc", a_));
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0004F858 File Offset: 0x0004E858
		public void SetFormulaErrorValue(int iRow, int iColumn, string value)
		{
			int a_ = 0;
			int num = 4;
			for (;;)
			{
				IL_25:
				switch (num)
				{
				case 0:
				{
					XlsWorksheet.TRangeValueType cellType;
					if ((cellType & XlsWorksheet.TRangeValueType.Formula) != XlsWorksheet.TRangeValueType.Formula)
					{
						num = 2;
						continue;
					}
					goto IL_F2;
				}
				case 1:
				{
					while (!FormulaUtil.ErrorNameToCode.ContainsKey(value))
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
							num = 3;
							goto IL_25;
						}
					}
					XlsWorksheet.TRangeValueType cellType = this.GetCellType(iRow, iColumn, false);
					num = 0;
					continue;
				}
				case 2:
					goto IL_73;
				case 3:
					goto IL_F0;
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					goto IL_4E;
				}
				if (value == null)
				{
					num = 5;
				}
				else
				{
					num = 1;
				}
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_));
			IL_73:
			throw new ArgumentException(RecordTableEnumerator.b("电夷吹刻儽㐿扁㝃⍅㱇㥉汋⡍㽏⁑㥓⍕㑗㭙籛⡝ş๡ᅣͥ䡧ͩɫ乭፯᝱ᡳ᩵塷๹ᑻώꊁ黎ꦍ늑秊ﶛ캟芡슣즥\udaa7잩\ud9ab슭톯", a_));
			IL_F0:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("怵夷嘹䤻嬽怿♁⭃⍅㭇橉≋⅍⑏牑≓㝕㑗㍙㡛繝՟ၡᙣ॥ᩧ䩩Ὣᩭɯ᭱ᩳᅵ噷", a_));
			IL_F2:
			byte a_2 = (byte)FormulaUtil.ErrorNameToCode[value];
			double a_3 = spr᱒.ᜀ(a_2, true);
			this.ᜀ(iRow, iColumn, a_3);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0004F978 File Offset: 0x0004E978
		public void SetFormulaBoolValue(int iRow, int iColumn, bool value)
		{
			int a_ = 16;
			for (;;)
			{
				if (true)
				{
				}
				XlsWorksheet.TRangeValueType cellType = this.GetCellType(iRow, iColumn, false);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5B;
					case 1:
						if ((cellType & XlsWorksheet.TRangeValueType.Formula) != XlsWorksheet.TRangeValueType.Formula)
						{
							num = 0;
							continue;
						}
						num = 5;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_73;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 3:
						goto IL_B8;
					case 4:
						goto IL_65;
					case 5:
						if (!value)
						{
							goto IL_73;
						}
						num = 3;
						continue;
					}
					break;
					IL_73:
					num = 2;
				}
			}
			IL_5B:
			throw new ArgumentException(RecordTableEnumerator.b("Յ⥇⑉≋⅍⑏牑❓㍕ⱗ⥙籛㡝ཟၡॣ፥ѧ୩䱫ᡭᅯṱų፵塷፹ቻ幽ꢇﺉ늑秊ﶗ릝풟芡잣즥욧\udea9춫잭\udeaf銱튳\ud9b5쪷ힹ즻튽ꆿ", a_));
			IL_65:
			byte b = 0;
			goto IL_BB;
			IL_B8:
			b = 1;
			IL_BB:
			byte a_2 = b;
			double a_3 = spr᱒.ᜀ(a_2, false);
			this.ᜀ(iRow, iColumn, a_3);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0004FA54 File Offset: 0x0004EA54
		public void SetFormulaStringValue(int iRow, int iColumn, string value)
		{
			int a_ = 8;
			XlsWorksheet.TRangeValueType cellType = this.GetCellType(iRow, iColumn, false);
			if ((cellType & XlsWorksheet.TRangeValueType.Formula) != XlsWorksheet.TRangeValueType.Formula)
			{
				if (true)
				{
				}
			}
			else
			{
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
					spr\u21DF spr_u21DF = (spr\u21DF)this.RecordExtractor.ᜀ(519);
					spr_u21DF.ᜀ(value);
					double u170D = spr᱒.\u170D;
					this.ᜀ(iRow, iColumn, u170D, spr_u21DF);
					return;
				}
				}
			}
			throw new ArgumentException(RecordTableEnumerator.b("紽ℿⱁ⩃⥅㱇橉㽋⭍⑏⅑瑓さ㝗⡙ㅛ⭝౟͡䑣ၥ१٩ᥫ୭偯᭱ᩳ噵᭷όၻችꁿﲇꪉ望놕몙ﾛ캟횡얣쾥욧誩쪫솭슯\udfb1솳\udab5\ud9b7", a_));
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0004FAEC File Offset: 0x0004EAEC
		public void SetError(int iRow, int iColumn, string value, bool isSetText)
		{
			int a_ = 7;
			int num = 3;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					goto IL_A5;
				case 2:
					num = 0;
					continue;
				}
				if (FormulaUtil.ErrorNameToCode.TryGetValue(value, out num2))
				{
					goto IL_A7;
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
					num = 2;
					continue;
				}
				IL_84:
				if (true)
				{
				}
				if (!isSetText)
				{
					break;
				}
				num = 1;
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("縼帾⽀ⵂ⩄㍆楈㭊ⱌ㵎≐㙒畔㉖⭘⥚㉜ⵞ䅠b੤ͦ౨䕪", a_));
			IL_A5:
			this.ᜀ(iRow, iColumn, value);
			return;
			IL_A7:
			int a_2 = this.ᜀ(iRow, iColumn);
			spr\u249B spr_u249B = (spr\u249B)this.ᜀ(TBIFFRecord.BoolErr, iRow, iColumn, a_2);
			spr_u249B.ᜀ(true);
			spr_u249B.ᜀ((byte)num2);
			this.ᜀ(iColumn, iRow, spr_u249B);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0004FBD8 File Offset: 0x0004EBD8
		private new void ᜀ(int A_0, int A_1, string A_2)
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
			int a_ = this.ᜀ(A_0, A_1);
			int a_2 = this.m_book.InnerSST.AddIncrease(A_2);
			spr\u1C7C spr_u1C7C = (spr\u1C7C)this.ᜀ(TBIFFRecord.LabelSST, A_0, A_1, a_);
			spr_u1C7C.ᜀ(a_2);
			this.ᜀ(A_1, A_0, spr_u1C7C);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0004FC54 File Offset: 0x0004EC54
		private new int ᜀ(int A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					IL_57:
					base.ParseData();
					spr\u23A5 spr_u23A = this.ᜏ.ᜄ(A_0, A_1);
					num = this.m_book.DefaultXFIndex;
					int num2 = 13;
					for (;;)
					{
						spr\u216E spr_u216E;
						spr\u1C7C spr_u1C7C;
						switch (num2)
						{
						case 0:
							if (spr_u216E != null)
							{
								num2 = 9;
								continue;
							}
							goto IL_FC;
						case 1:
							goto IL_FC;
						case 2:
							num2 = 11;
							continue;
						case 3:
							if (num != 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_13A;
						case 4:
						{
							int iIndex = spr_u1C7C.ᜁ();
							this.m_book.InnerSST.RemoveDecrease(iIndex);
							num2 = 15;
							continue;
						}
						case 5:
						{
							sprᱧ sprᱧ;
							if (sprᱧ != null)
							{
								num2 = 14;
								continue;
							}
							goto IL_1A8;
						}
						case 6:
							goto IL_FC;
						case 7:
							goto IL_13A;
						case 8:
							num = (int)spr_u23A.ᜆ();
							num2 = 6;
							continue;
						case 9:
							num = (int)spr_u216E.ᜌ();
							num2 = 1;
							continue;
						case 10:
							if (spr_u1C7C != null)
							{
								num2 = 4;
								continue;
							}
							return num;
						case 11:
							if (num == this.m_book.DefaultXFIndex)
							{
								num2 = 7;
								continue;
							}
							goto IL_FC;
						case 12:
							if (true)
							{
							}
							goto IL_1A8;
						case 13:
						{
							if (spr_u23A != null)
							{
								num2 = 8;
								continue;
							}
							sprᱧ sprᱧ = sprᜑ.ᜀ(this, A_0 - 1, false);
							num2 = 5;
							continue;
						}
						case 14:
						{
							sprᱧ sprᱧ;
							num = (int)sprᱧ.ᜇ();
							num2 = 12;
							continue;
						}
						case 15:
							return num;
						}
						break;
						IL_FC:
						spr_u1C7C = (spr_u23A as spr\u1C7C);
						num2 = 10;
						continue;
						IL_13A:
						spr_u216E = this.ᜐ[A_1];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_57;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						IL_1A8:
						num2 = 3;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0004FE54 File Offset: 0x0004EE54
		internal new int ᜅ(int A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					base.ParseData();
					num = this.ᜏ.GetExtendedFormatIndex(A_0, A_1);
					int num2 = 14;
					for (;;)
					{
						sprᱧ sprᱧ;
						int num3;
						int num4;
						spr\u216E spr_u216E;
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D8;
							default:
								if (false)
								{
								}
								num = (int)sprᱧ.ᜇ();
								num2 = 5;
								continue;
							}
							break;
						case 1:
							num2 = 2;
							continue;
						case 2:
							if (num3 != this.m_book.DefaultXFIndex)
							{
								num2 = 0;
								continue;
							}
							goto IL_17F;
						case 3:
							num4 = 0;
							goto IL_126;
						case 4:
							if (sprᱧ != null)
							{
								num2 = 11;
								continue;
							}
							goto IL_103;
						case 5:
							goto IL_149;
						case 6:
							num4 = (int)sprᱧ.ᜇ();
							goto IL_126;
						case 7:
							if (num3 != 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_17F;
						case 8:
							goto IL_149;
						case 9:
							if (spr_u216E != null)
							{
								num2 = 13;
								continue;
							}
							goto IL_149;
						case 10:
							if (!this.m_book.IsFormatted((int)sprᱧ.ᜇ()))
							{
								num2 = 16;
								continue;
							}
							num2 = 6;
							continue;
						case 11:
							if (true)
							{
							}
							num2 = 10;
							continue;
						case 12:
							return num;
						case 13:
							num = (int)spr_u216E.ᜌ();
							num2 = 8;
							continue;
						case 14:
							if (num < 0)
							{
								num2 = 17;
								continue;
							}
							goto IL_149;
						case 15:
							if (num >= 0)
							{
								num2 = 12;
								continue;
							}
							goto IL_1FF;
						case 16:
							goto IL_103;
						case 17:
							goto IL_D8;
						}
						break;
						IL_D8:
						sprᱧ = sprᜑ.ᜀ(this, A_0 - 1, false);
						num2 = 4;
						continue;
						IL_103:
						num2 = 3;
						continue;
						IL_126:
						num3 = num4;
						num2 = 7;
						continue;
						IL_149:
						num2 = 15;
						continue;
						IL_17F:
						spr_u216E = this.ᜐ[A_1];
						num2 = 9;
					}
				}
				return num;
				IL_1FF:
				return this.m_book.DefaultXFIndex;
			}
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0005006C File Offset: 0x0004F06C
		[CLSCompliant(false)]
		internal new sprỔ ᜂ(int A_0, int A_1, double A_2)
		{
			int num;
			for (;;)
			{
				base.ParseData();
				num = sprỔ.ᜀ(A_2);
				if (num != 2147483647)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_51;
				}
			}
			if (true)
			{
			}
			sprỔ sprỔ = (sprỔ)this.ᜀ(TBIFFRecord.RK, A_0, A_1);
			sprỔ.ᜅ(num);
			return sprỔ;
			IL_51:
			if (false)
			{
			}
			return null;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000500DC File Offset: 0x0004F0DC
		[CLSCompliant(false)]
		internal sprỔ ᜁ(int A_0, int A_1, double A_2, int A_3)
		{
			int num;
			for (;;)
			{
				base.ParseData();
				num = sprỔ.ᜀ(A_2);
				if (num != 2147483647)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_4B;
				}
			}
			sprỔ sprỔ = (sprỔ)this.ᜀ(TBIFFRecord.RK, A_0, A_1, A_3);
			sprỔ.ᜅ(num);
			return sprỔ;
			IL_4B:
			if (true)
			{
			}
			if (false)
			{
			}
			return null;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00050150 File Offset: 0x0004F150
		internal new BiffRecordRaw ᜀ(TBIFFRecord A_0, int A_1, int A_2)
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
			return this.ᜀ(A_0, A_1, A_2, this.ᜅ(A_1, A_2));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0005019C File Offset: 0x0004F19C
		private new BiffRecordRaw ᜀ(TBIFFRecord A_0, int A_1, int A_2, int A_3)
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
			spr\u23A5 spr_u23A = this.RecordExtractor.ᜀ((int)A_0) as spr\u23A5;
			spr_u23A.ᜃ(A_1 - 1);
			spr_u23A.ᜄ(A_2 - 1);
			spr_u23A.ᜀ((ushort)A_3);
			return spr_u23A as BiffRecordRaw;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0005020C File Offset: 0x0004F20C
		private new void ᜀ(int A_0, int A_1, double A_2)
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
			this.ᜀ(A_0, A_1, A_2, null);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00050254 File Offset: 0x0004F254
		private new void ᜀ(int A_0, int A_1, double A_2, spr\u21DF A_3)
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
			base.ParseData();
			this.ᜏ.Table.ᜀ(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000502AC File Offset: 0x0004F2AC
		public CellRange GroupByColumns(int firstColumn, int lastColumn, bool isCollapsed)
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
			base.CheckDisposed();
			return ((XlsRange)this.AllocatedRange[1, firstColumn, 1, lastColumn]).Group(GroupByType.ByColumns, isCollapsed) as CellRange;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00050310 File Offset: 0x0004F310
		public CellRange GroupByRows(int firstRow, int lastRow, bool isCollapsed)
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
			base.CheckDisposed();
			return ((XlsRange)this.AllocatedRange[firstRow, 1, lastRow, 1]).Group(GroupByType.ByRows, isCollapsed) as CellRange;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00050374 File Offset: 0x0004F374
		public CellRange UngroupByColumns(int firstColumn, int lastColumn)
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
			return ((XlsRange)this.AllocatedRange[1, firstColumn, 1, lastColumn]).Ungroup(GroupByType.ByColumns) as CellRange;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000503D0 File Offset: 0x0004F3D0
		public CellRange UngroupByRows(int firstRow, int lastRow)
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
			return ((XlsRange)this.AllocatedRange[firstRow, 1, lastRow, 1]).Ungroup(GroupByType.ByRows) as CellRange;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0005042C File Offset: 0x0004F42C
		public string GetFormula(int row, int column, bool bR1C1)
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
			return this.GetFormula(row, column, bR1C1, false);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00050474 File Offset: 0x0004F474
		public string GetFormula(int row, int column, bool bR1C1, bool isForSerialization)
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
			return this.ᜀ(row, column, bR1C1, this.m_book.FormulaUtil, isForSerialization);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000504C8 File Offset: 0x0004F4C8
		internal new string ᜀ(int A_0, int A_1, bool A_2, FormulaUtil A_3, bool A_4)
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
			base.ParseData();
			Ptg[] a_ = this.ᜏ.Table.ᜅ(A_0, A_1);
			A_0--;
			A_1--;
			return this.ᜀ(A_0, A_1, a_, A_2, A_3, A_4);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00050534 File Offset: 0x0004F534
		private new string ᜀ(int A_0, int A_1, Ptg[] A_2, bool A_3, FormulaUtil A_4, bool A_5)
		{
			int a_ = 3;
			while (A_2 != null)
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
					return RecordTableEnumerator.b("и", a_) + A_4.ᜀ(A_2, A_0, A_1, A_3, null, false, A_5, this);
				}
			}
			if (true)
			{
			}
			return null;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000505A4 File Offset: 0x0004F5A4
		private new string ᜀ(spr᱒ A_0)
		{
			spr\u225F spr_u225F;
			for (;;)
			{
				spr_u225F = this.CellRecords.ᜁ(A_0.\u1714() + 1, A_0.\u1713() + 1);
				if (spr_u225F == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_39;
				}
			}
			return null;
			IL_39:
			if (true)
			{
			}
			if (false)
			{
			}
			return this.m_book.FormulaUtil.ᜀ(spr_u225F.ᜅ(), spr_u225F.ᜉ(), spr_u225F.ᜈ(), false, null, false, false, this);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0005062C File Offset: 0x0004F62C
		public string GetStringValue(long cellIndex)
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
			base.ParseData();
			return this.GetText(sprṔ.ᜁ(cellIndex), sprṔ.ᜀ(cellIndex));
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00050680 File Offset: 0x0004F680
		public string GetText(int row, int column)
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
			base.ParseData();
			return this.ᜏ.Table.ᜁ(row, column, this.m_book.InnerSST);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000506E0 File Offset: 0x0004F6E0
		public string GetFormulaStringValue(int row, int column)
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
			base.ParseData();
			return this.ᜏ.Table.ᜀ(row, column, this.m_book.InnerSST);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00050740 File Offset: 0x0004F740
		public double GetNumber(int row, int column)
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
			base.ParseData();
			return this.ᜏ.Table.\u170D(row, column);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00050794 File Offset: 0x0004F794
		public double GetFormulaNumberValue(int row, int column)
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
			base.ParseData();
			return this.ᜏ.Table.ᜆ(row, column);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000507E8 File Offset: 0x0004F7E8
		public string GetError(int row, int column)
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
			base.ParseData();
			return this.ᜏ.Table.ᜂ(row, column);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0005083C File Offset: 0x0004F83C
		internal new string ᜀ(byte A_0, int A_1)
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
			return this.ᜏ.Table.ᜀ(A_0, A_1);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0005088C File Offset: 0x0004F88C
		public string GetFormulaErrorValue(int row, int column)
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
			base.ParseData();
			return this.ᜏ.Table.ᜁ(row, column);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000508E0 File Offset: 0x0004F8E0
		public bool GetBoolean(int row, int column)
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
			base.ParseData();
			int num = this.ᜏ.Table.ᜇ(row, column);
			return num > 0;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00050938 File Offset: 0x0004F938
		public bool GetFormulaBoolValue(int row, int column)
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
			base.ParseData();
			int num = this.ᜏ.Table.ᜃ(row, column);
			return num > 0;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00050990 File Offset: 0x0004F990
		public bool HasArrayFormulaRecord(int row, int column)
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
			base.ParseData();
			Ptg[] a_ = this.ᜏ.Table.ᜅ(row, column);
			return this.ᜀ(a_);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000509EC File Offset: 0x0004F9EC
		internal new bool ᜀ(Ptg[] A_0)
		{
			int num = 3;
			Ptg ptg;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return false;
				case 1:
					num = 5;
					continue;
				case 2:
					goto IL_95;
				case 4:
					if (ptg.TokenCode != FormulaToken.tExp)
					{
						num = 0;
						continue;
					}
					goto IL_99;
				case 5:
					if (A_0.Length != 1)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						ptg = A_0[0];
						num = 4;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					return false;
				}
				num = 1;
			}
			return false;
			IL_95:
			return false;
			IL_99:
			if (true)
			{
			}
			spr\u252B spr_u252B = ptg as spr\u252B;
			return this.ᜏ.Table.ᜄ(spr_u252B.ᜇ(), spr_u252B.ᜆ());
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00050AC0 File Offset: 0x0004FAC0
		internal new bool ᜄ(int A_0, int A_1)
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
			base.ParseData();
			return this.ᜏ.Table.ᜈ(A_0, A_1);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00050B14 File Offset: 0x0004FB14
		public XlsWorksheet.TRangeValueType GetCellType(int row, int column, bool bNeedFormulaSubType)
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
			base.ParseData();
			return this.ᜏ.Table.ᜀ(row, column, bNeedFormulaSubType);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00050B68 File Offset: 0x0004FB68
		public bool IsExternalFormula(int row, int column)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_63:
					base.ParseData();
					Ptg[] array = this.ᜏ.Table.ᜅ(row, column);
					int num = 7;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							int num2;
							switch (num)
							{
							case 0:
							{
								spr\u2086 spr_u;
								if (spr_u != null)
								{
									num = 4;
									continue;
								}
								goto IL_99;
							}
							case 1:
							{
								int reference;
								if (this.m_book.IsExternalReference(reference))
								{
									goto IL_CE;
								}
								goto IL_99;
							}
							case 2:
								return false;
							case 3:
								return true;
							case 4:
							{
								spr\u2086 spr_u;
								int reference = (int)spr_u.ᜁ();
								num = 1;
								continue;
							}
							case 5:
							{
								int num3;
								if (num2 >= num3)
								{
									num = 2;
									continue;
								}
								spr\u2086 spr_u = array[num2] as spr\u2086;
								num = 0;
								continue;
							}
							case 6:
								goto IL_F0;
							case 7:
								if (array != null)
								{
									num = 8;
									continue;
								}
								return false;
							case 8:
							{
								num2 = 0;
								int num3 = array.Length;
								num = 9;
								continue;
							}
							case 9:
								goto IL_F0;
							}
							goto IL_63;
							IL_99:
							num2++;
							num = 6;
							continue;
							IL_F0:
							num = 5;
							continue;
						}
						}
						IL_CE:
						num = 3;
					}
				}
				return true;
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00050CBC File Offset: 0x0004FCBC
		internal new void ᜀ(object A_0, object A_1, IXLSRange A_2)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_7C;
					}
					break;
				case 1:
				{
					CellValueChangedEventArgs cellValueChangedEventArgs = new CellValueChangedEventArgs();
					cellValueChangedEventArgs.OldValue = A_0;
					cellValueChangedEventArgs.NewValue = A_1;
					cellValueChangedEventArgs.Range = A_2;
					this.ᜇ(this, cellValueChangedEventArgs);
					num = 0;
					continue;
				}
				}
				if (this.ᜇ == null)
				{
					return;
				}
				num = 1;
			}
			IL_7C:
			if (false)
			{
			}
		}

		// Token: 0x040000E9 RID: 233
		internal new const char ᜀ = '0';

		// Token: 0x040000EA RID: 234
		private new const float ᜁ = 0.017453292f;

		// Token: 0x040000EB RID: 235
		private new const int ᜂ = 255;

		// Token: 0x040000EC RID: 236
		private new const double ᜃ = 8.0;

		// Token: 0x040000ED RID: 237
		private new const int ᜄ = 100;

		// Token: 0x040000EE RID: 238
		private new const int ᜅ = 16;

		// Token: 0x040000EF RID: 239
		private const int ᜆ = 12;

		// Token: 0x040000F0 RID: 240
		private XlsRange.CellValueChangedEventHandler ᜇ;

		// Token: 0x040000F1 RID: 241
		private FormulaEngine ᜈ;

		// Token: 0x040000F2 RID: 242
		private bool ᜉ;

		// Token: 0x040000F3 RID: 243
		internal int ᜊ = 9;

		// Token: 0x040000F4 RID: 244
		private XlsWorksheet.ErrorFunctionEventHandler ᜋ;

		// Token: 0x040000F5 RID: 245
		private ValueChangedEventHandler ᜌ;

		// Token: 0x040000F6 RID: 246
		private static readonly TBIFFRecord[] \u170D;

		// Token: 0x040000F7 RID: 247
		private new XlsRange ᜎ;

		// Token: 0x040000F8 RID: 248
		private new XlsCellRecordCollection ᜏ;

		// Token: 0x040000F9 RID: 249
		private new spr\u216E[] ᜐ;

		// Token: 0x040000FA RID: 250
		private new bool ᜑ;

		// Token: 0x040000FB RID: 251
		private XlsPageSetup \u1712;

		// Token: 0x040000FC RID: 252
		private double \u1713 = 8.43;

		// Token: 0x040000FD RID: 253
		internal spr\u1FBC \u1714;

		// Token: 0x040000FE RID: 254
		private WorksheetVisibility \u1715;

		// Token: 0x040000FF RID: 255
		private List<spr\u21A4> \u1716;

		// Token: 0x04000100 RID: 256
		private spr\u2408 \u1717;

		// Token: 0x04000101 RID: 257
		private sprᤗ \u1718;

		// Token: 0x04000102 RID: 258
		private ExcelSheetType \u1719;

		// Token: 0x04000103 RID: 259
		private bool \u171A;

		// Token: 0x04000104 RID: 260
		private List<BiffRecordRaw> \u171B;

		// Token: 0x04000105 RID: 261
		private SortedList<int, spr\u2114> \u171C;

		// Token: 0x04000106 RID: 262
		private SortedList<long, spr\u2114> \u171D;

		// Token: 0x04000107 RID: 263
		private new XlsName.NameIndexChangedEventHandler \u171E;

		// Token: 0x04000108 RID: 264
		private XlsDataValidationTable \u171F;

		// Token: 0x04000109 RID: 265
		private new XlsAutoFiltersCollection ᜠ;

		// Token: 0x0400010A RID: 266
		private PivotTablesCollection ᜡ;

		// Token: 0x0400010B RID: 267
		private XlsHyperLinksCollection ᜢ;

		// Token: 0x0400010C RID: 268
		private List<BiffRecordRaw> ᜣ;

		// Token: 0x0400010D RID: 269
		private int ᜤ = -1;

		// Token: 0x0400010E RID: 270
		private int ᜥ = -1;

		// Token: 0x0400010F RID: 271
		private int ᜦ = -1;

		// Token: 0x04000110 RID: 272
		private int ᜧ = -1;

		// Token: 0x04000111 RID: 273
		private int ᜨ = -1;

		// Token: 0x04000112 RID: 274
		private List<BiffRecordRaw> ᜩ;

		// Token: 0x04000113 RID: 275
		private XlsWorksheetConditionalFormats ᜪ;

		// Token: 0x04000114 RID: 276
		private spr\u256D ᜫ;

		// Token: 0x04000115 RID: 277
		private IMigrantRange ᜬ;

		// Token: 0x04000116 RID: 278
		private spr\u218B ᜭ;

		// Token: 0x04000117 RID: 279
		private spr\u2622 ᜮ;

		// Token: 0x04000118 RID: 280
		private bool ᜯ = true;

		// Token: 0x04000119 RID: 281
		private spr\u19EA ᜰ = new spr\u19EA();

		// Token: 0x0400011A RID: 282
		private sprᱥ ᜱ;

		// Token: 0x0400011B RID: 283
		private ListObjectCollection \u1732;

		// Token: 0x0400011C RID: 284
		private List<BiffRecordRaw> \u1733;

		// Token: 0x0400011D RID: 285
		private bool \u1734;

		// Token: 0x0400011E RID: 286
		private bool \u1735;

		// Token: 0x0400011F RID: 287
		private int \u1736;

		// Token: 0x04000120 RID: 288
		private bool \u1737;

		// Token: 0x04000121 RID: 289
		private bool \u1738;

		// Token: 0x04000122 RID: 290
		private new byte \u1739;

		// Token: 0x04000123 RID: 291
		private byte \u173A;

		// Token: 0x04000124 RID: 292
		private bool \u173B = true;

		// Token: 0x04000125 RID: 293
		private spr\u216E \u173C;

		// Token: 0x04000126 RID: 294
		private bool \u173D;

		// Token: 0x04000127 RID: 295
		internal List<Stream> \u173E;

		// Token: 0x04000128 RID: 296
		private int \u173F;

		// Token: 0x04000129 RID: 297
		private int ᝀ;

		// Token: 0x0400012A RID: 298
		private int ᝁ;

		// Token: 0x0400012B RID: 299
		private sprᜭ ᝂ;

		// Token: 0x0400012C RID: 300
		private SparklineGroupCollection ᝃ;

		// Token: 0x0400012D RID: 301
		private Dictionary<string, string> ᝄ;

		// Token: 0x0400012E RID: 302
		private List<BiffRecordRaw> ᝅ;

		// Token: 0x0400012F RID: 303
		private List<Stream> ᝆ;

		// Token: 0x04000130 RID: 304
		private XlsEventHandler ᝇ;

		// Token: 0x04000131 RID: 305
		private XlsEventHandler ᝈ;

		// Token: 0x020005F5 RID: 1525
		private enum RangeProperty
		{
			// Token: 0x04002C2E RID: 11310
			Value2,
			// Token: 0x04002C2F RID: 11311
			Text,
			// Token: 0x04002C30 RID: 11312
			DateTime,
			// Token: 0x04002C31 RID: 11313
			TimeSpan
		}

		// Token: 0x020005F6 RID: 1526
		[Flags]
		public enum TRangeValueType
		{
			// Token: 0x04002C33 RID: 11315
			Blank = 0,
			// Token: 0x04002C34 RID: 11316
			Error = 1,
			// Token: 0x04002C35 RID: 11317
			Boolean = 2,
			// Token: 0x04002C36 RID: 11318
			Number = 4,
			// Token: 0x04002C37 RID: 11319
			Formula = 8,
			// Token: 0x04002C38 RID: 11320
			String = 16
		}

		// Token: 0x020005F7 RID: 1527
		// (Invoke) Token: 0x06005A02 RID: 23042
		private new delegate spr\u2502 ᜀ(int A_0);

		// Token: 0x020005F8 RID: 1528
		// (Invoke) Token: 0x06005A06 RID: 23046
		public delegate void ErrorFunctionEventHandler(object sender, XlsWorksheet.ErrorFunctionEventArgs e);

		// Token: 0x020005F9 RID: 1529
		public class ErrorFunctionEventArgs : EventArgs
		{
			// Token: 0x17000E00 RID: 3584
			// (get) Token: 0x06005A09 RID: 23049 RVA: 0x00386714 File Offset: 0x00385714
			// (set) Token: 0x06005A0A RID: 23050 RVA: 0x00386758 File Offset: 0x00385758
			public string FunctionName
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
					return this.ᜀ;
				}
				internal set
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
					this.ᜀ = value;
				}
			}

			// Token: 0x17000E01 RID: 3585
			// (get) Token: 0x06005A0B RID: 23051 RVA: 0x0038679C File Offset: 0x0038579C
			// (set) Token: 0x06005A0C RID: 23052 RVA: 0x003867E0 File Offset: 0x003857E0
			public string CellRange
			{
				get
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
					return this.ᜁ;
				}
				internal set
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
					this.ᜁ = value;
				}
			}

			// Token: 0x04002C39 RID: 11321
			private bool \u2609\u008C\u00AC\u009D;

			// Token: 0x04002C3A RID: 11322
			private int[] \u2609\u0092\u00AD\u0095;

			// Token: 0x04002C3B RID: 11323
			private long \u25D8\u0086\u0091\u00A1;

			// Token: 0x04002C3C RID: 11324
			private float \u2609\u009E\u00AC\u0092;

			// Token: 0x04002C3D RID: 11325
			private string ᜀ;

			// Token: 0x04002C3E RID: 11326
			private int[] \u25D8\u008D\u00A7\u00A7;

			// Token: 0x04002C3F RID: 11327
			private string ᜁ;
		}
	}
}
