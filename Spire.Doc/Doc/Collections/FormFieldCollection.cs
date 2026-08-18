using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace Spire.Doc.Collections
{
	// Token: 0x0200052E RID: 1326
	public class FormFieldCollection : CollectionEx
	{
		// Token: 0x17000535 RID: 1333
		public FormField this[int index]
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
				return (FormField)base.InnerList[index];
			}
		}

		// Token: 0x17000536 RID: 1334
		public FormField this[string formFieldName]
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
				return this.ᜀ(formFieldName);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x00407E68 File Offset: 0x00406E68
		internal Dictionary<string, FormField> FormFieldDictonary
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
				return this.ᜀ;
			}
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x00407EAC File Offset: 0x00406EAC
		internal FormFieldCollection(Body A_0) : base(A_0.Document, A_0)
		{
			this.ᜀ(A_0);
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x00407ED8 File Offset: 0x00406ED8
		public bool ContainsName(string itemName)
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
			return this.ᜀ.ContainsKey(itemName);
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x00407F20 File Offset: 0x00406F20
		internal void ᜀ(string A_0, string A_1)
		{
			for (;;)
			{
				FormField value = this.ᜀ[A_0];
				this.ᜀ.Remove(A_0);
				this.ᜀ.Add(A_1, value);
				TableCell tableCell = base.OwnerBase as TableCell;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (tableCell != null)
						{
							num = 8;
							continue;
						}
						return;
					case 1:
					{
						Body ownerTextBody;
						if (ownerTextBody != null)
						{
							num = 4;
							continue;
						}
						return;
					}
					case 2:
					{
						Body ownerTextBody = tableCell.OwnerRow.OwnerTable.OwnerTextBody;
						num = 1;
						continue;
					}
					case 3:
						return;
					case 4:
						num = 7;
						continue;
					case 5:
					{
						Body ownerTextBody;
						ownerTextBody.FormFields.ᜀ(A_0, A_1);
						num = 3;
						continue;
					}
					case 6:
						if (tableCell.OwnerRow.OwnerTable != null)
						{
							num = 2;
							continue;
						}
						return;
					case 7:
					{
						Body ownerTextBody;
						if (ownerTextBody.IsFormFieldsCreated)
						{
							num = 5;
							continue;
						}
						return;
					}
					case 8:
						goto IL_DB;
					case 9:
						num = 6;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DB;
						default:
							if (false)
							{
							}
							if (tableCell.OwnerRow != null)
							{
								if (true)
								{
								}
								num = 9;
								continue;
							}
							return;
						}
						break;
					}
					break;
					IL_DB:
					num = 10;
				}
			}
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x00408090 File Offset: 0x00407090
		internal void ᜁ(FormField A_0)
		{
			for (;;)
			{
				base.InnerList.Add(A_0);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.ᜀ.ContainsKey(A_0.Name))
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 2:
						num = 0;
						continue;
					case 3:
						if (A_0.Name != string.Empty)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						this.ᜀ.Add(A_0.Name, A_0);
						num = 6;
						continue;
					case 5:
						if (A_0.Name != null)
						{
							num = 1;
							continue;
						}
						return;
					case 6:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x00408188 File Offset: 0x00407188
		internal void ᜀ(FormField A_0)
		{
			for (;;)
			{
				base.InnerList.Remove(A_0);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 1:
					{
						Bookmark bookmark;
						this.m_doc.Bookmarks.Remove(bookmark);
						num = 4;
						continue;
					}
					case 2:
					{
						Bookmark bookmark;
						if (bookmark != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					case 3:
						if (A_0.Name != string.Empty)
						{
							goto IL_11E;
						}
						return;
					case 4:
						return;
					case 5:
						if (!this.ᜀ.ContainsKey(A_0.Name))
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11E;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 6:
						num = 3;
						continue;
					case 7:
					{
						if (true)
						{
						}
						this.ᜀ.Remove(A_0.Name);
						Bookmark bookmark = this.m_doc.Bookmarks.FindByName(A_0.Name);
						num = 2;
						continue;
					}
					case 8:
						if (A_0.Name != null)
						{
							num = 6;
							continue;
						}
						return;
					}
					break;
					IL_11E:
					num = 0;
				}
			}
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x004082E4 File Offset: 0x004072E4
		private void ᜀ(Body A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.ChildObjects.GetEnumerator();
				try
				{
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 1:
							num = 2;
							continue;
						case 2:
							goto IL_119;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							DocumentObjectType documentObjectType = bodyRegion.DocumentObjectType;
							num = 5;
							continue;
						}
						case 4:
						{
							DocumentObjectType documentObjectType;
							if (documentObjectType != DocumentObjectType.Table)
							{
								num = 6;
								continue;
							}
							BodyRegion bodyRegion;
							this.ᜀ((Table)bodyRegion);
							num = 7;
							continue;
						}
						case 5:
						{
							DocumentObjectType documentObjectType;
							if (documentObjectType != DocumentObjectType.Paragraph)
							{
								num = 10;
								continue;
							}
							BodyRegion bodyRegion;
							this.ᜀ((Paragraph)bodyRegion);
							num = 8;
							continue;
						}
						case 6:
							num = 0;
							continue;
						case 10:
							num = 4;
							continue;
						}
						IL_78:
						num = 3;
						continue;
						goto IL_78;
					}
					IL_119:;
				}
				finally
				{
					for (;;)
					{
						if (true)
						{
						}
						IDisposable disposable = enumerator as IDisposable;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_164:
							if (disposable == null)
							{
								goto IL_183;
							}
							num = 0;
							break;
						default:
							if (false)
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
								disposable.Dispose();
								num = 2;
								continue;
							case 1:
								goto IL_164;
							case 2:
								goto IL_181;
							}
							break;
						}
					}
					IL_181:
					IL_183:;
				}
				return;
			}
			}
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x00408490 File Offset: 0x00407490
		private void ᜀ(Paragraph A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.Items.GetEnumerator();
				try
				{
					int num = 7;
					for (;;)
					{
						DocumentObjectType documentObjectType;
						ParagraphBase paragraphBase;
						switch (num)
						{
						case 0:
							goto IL_1ED;
						case 1:
							switch (documentObjectType)
							{
							case DocumentObjectType.Comment:
								this.ᜀ((paragraphBase as Comment).Body);
								num = 10;
								continue;
							case DocumentObjectType.Footnote:
								this.ᜀ((paragraphBase as Footnote).TextBody);
								num = 3;
								continue;
							case DocumentObjectType.TextBox:
								this.ᜀ((paragraphBase as TextBox).Body);
								num = 9;
								continue;
							default:
								num = 11;
								continue;
							}
							break;
						case 2:
							num = 0;
							continue;
						case 4:
							num = 15;
							continue;
						case 5:
							if (paragraphBase.DocumentObjectType == DocumentObjectType.DropDownFormField)
							{
								num = 6;
								continue;
							}
							goto IL_128;
						case 6:
							goto IL_1C4;
						case 8:
							goto IL_128;
						case 11:
							num = 13;
							continue;
						case 12:
							num = 5;
							continue;
						case 14:
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							paragraphBase = (ParagraphBase)enumerator.Current;
							num = 16;
							continue;
						case 15:
							if (paragraphBase.DocumentObjectType != DocumentObjectType.CheckBox)
							{
								num = 12;
								continue;
							}
							goto IL_1C4;
						case 16:
							if (paragraphBase.DocumentObjectType != DocumentObjectType.TextFormField)
							{
								num = 4;
								continue;
							}
							goto IL_1C4;
						}
						IL_100:
						num = 14;
						continue;
						goto IL_100;
						IL_128:
						documentObjectType = paragraphBase.DocumentObjectType;
						num = 1;
						continue;
						IL_1C4:
						this.ᜁ((FormField)paragraphBase);
						num = 8;
					}
					IL_1ED:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_230:
							if (disposable == null)
							{
								goto IL_24F;
							}
							num = 1;
							break;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_24D;
							case 1:
								disposable.Dispose();
								num = 0;
								continue;
							case 2:
								goto IL_230;
							}
							break;
						}
					}
					IL_24D:
					IL_24F:;
				}
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x0040871C File Offset: 0x0040771C
		private void ᜀ(Table A_0)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.Rows.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_178;
						case 1:
							goto IL_16C;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 4;
							continue;
						}
						case 4:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 3;
										continue;
									case 2:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										TableCell a_ = (TableCell)enumerator2.Current;
										this.ᜀ(a_);
										num = 0;
										continue;
									}
									case 3:
										goto IL_103;
									}
									IL_C0:
									num = 2;
									continue;
									goto IL_C0;
								}
								IL_103:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_14A:
										if (disposable == null)
										{
											goto IL_16B;
										}
										num = 2;
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
										switch (num)
										{
										case 0:
											goto IL_14A;
										case 1:
											goto IL_169;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_169:
								IL_16B:;
							}
							goto IL_16C;
						}
						IL_79:
						num = 2;
						continue;
						goto IL_79;
						IL_16C:
						num = 0;
					}
					IL_178:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable2.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable2 != null)
								{
									num = 0;
									continue;
								}
								goto IL_1C1;
							case 2:
								goto IL_1BF;
							}
							break;
						}
					}
					IL_1BF:
					IL_1C1:;
				}
				return;
			}
			}
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x00408920 File Offset: 0x00407920
		private FormField ᜀ(string A_0)
		{
			if (this.ᜀ.ContainsKey(A_0))
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
					return this.ᜀ[A_0];
				}
			}
			return null;
		}

		// Token: 0x0400365E RID: 13918
		private string \u2609\u0080ªª;

		// Token: 0x0400365F RID: 13919
		private byte \u2460\u0083\u0080\u00A8;

		// Token: 0x04003660 RID: 13920
		private bool[] \u25D8\u00AD\u0081\u008B;

		// Token: 0x04003661 RID: 13921
		private new Dictionary<string, FormField> ᜀ = new Dictionary<string, FormField>();
	}
}
