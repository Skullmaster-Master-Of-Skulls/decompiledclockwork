using System;
using System.Collections;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x020000ED RID: 237
	public abstract class DocumentObjectCollection : DocumentSerializableCollection, IDocumentObjectCollection
	{
		// Token: 0x17000163 RID: 355
		public DocumentObject this[int index]
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
				return base.InnerList[index] as DocumentObject;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0002B5E4 File Offset: 0x0002A5E4
		public DocumentObject FirstItem
		{
			get
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
					if (base.Count <= 0)
					{
						return null;
					}
					break;
				}
				return this[0];
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0002B634 File Offset: 0x0002A634
		public DocumentObject LastItem
		{
			get
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
					if (base.Count <= 0)
					{
						if (true)
						{
						}
						return null;
					}
					break;
				}
				return this[base.Count - 1];
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0002B68C File Offset: 0x0002A68C
		internal bool Joined
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
				return base.OwnerBase != null;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0002B6D4 File Offset: 0x0002A6D4
		internal DocumentObject Owner
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
				return (DocumentObject)base.OwnerBase;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003FA RID: 1018
		protected abstract Type[] TypesOfElement { get; }

		// Token: 0x060003FB RID: 1019 RVA: 0x0002B71C File Offset: 0x0002A71C
		internal DocumentObjectCollection(Document A_0) : this(A_0, null)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0002B734 File Offset: 0x0002A734
		internal DocumentObjectCollection(Document A_0, DocumentObject A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0002B754 File Offset: 0x0002A754
		public int Add(IDocumentObject entity)
		{
			int a_ = 16;
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
				if (entity == null)
				{
					throw new ArgumentNullException(ClipboardData.b("፵ᙷ๹ᕻ੽勵", a_));
				}
				break;
			}
			int num = base.Count;
			this.OnInsert(num, (DocumentObject)entity);
			num = base.Count;
			num = this.ᜅ(num, (DocumentObject)entity);
			base.InnerList.Add(entity);
			this.OnInsertComplete(num, (DocumentObject)entity);
			this.ᜃ(num, (DocumentObject)entity);
			return num;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0002B804 File Offset: 0x0002A804
		public void Clear()
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
			this.OnClear();
			base.InnerList.Clear();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0002B850 File Offset: 0x0002A850
		public bool Contains(IDocumentObject entity)
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
			return base.InnerList.Contains(entity);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0002B898 File Offset: 0x0002A898
		public int IndexOf(IDocumentObject entity)
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
			return base.InnerList.IndexOf(entity);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0002B8E0 File Offset: 0x0002A8E0
		public void Insert(int index, IDocumentObject entity)
		{
			int a_ = 11;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.m_doc.ᜇ)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					num = 10;
					continue;
				case 3:
					if (!this.m_doc.ᜇ)
					{
						num = 7;
						continue;
					}
					goto IL_63;
				case 4:
					goto IL_63;
				case 5:
					if (entity is FormField)
					{
						goto IL_17E;
					}
					goto IL_63;
				case 6:
					index = this.ᜂ(index, (DocumentObject)entity);
					num = 4;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_54;
				case 10:
					if (entity is FormField)
					{
						if (true)
						{
						}
						num = 11;
						continue;
					}
					return;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17E;
					default:
						if (false)
						{
						}
						this.ᜁ(index, (DocumentObject)entity);
						num = 1;
						continue;
					}
					break;
				}
				if (entity == null)
				{
					num = 8;
					continue;
				}
				this.OnInsert(index, (DocumentObject)entity);
				num = 3;
				continue;
				IL_63:
				base.InnerList.Insert(index, entity);
				this.OnInsertComplete(index, (DocumentObject)entity);
				num = 0;
				continue;
				IL_17E:
				num = 6;
			}
			IL_54:
			throw new ArgumentNullException(ClipboardData.b("ᑰᵲŴṶ൸ɺ", a_));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0002BA7C File Offset: 0x0002AA7C
		public void Remove(IDocumentObject entity)
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
			this.ᜀ(entity);
			this.OnRemove(this.IndexOf(entity));
			base.InnerList.Remove(entity);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0002BAD8 File Offset: 0x0002AAD8
		public void RemoveAt(int index)
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
			this.ᜀ(base.InnerList[index] as IDocumentObject);
			this.OnRemove(index);
			base.InnerList.RemoveAt(index);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0002BB40 File Offset: 0x0002AB40
		private void ᜀ(IDocumentObject A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					Bookmark bookmark;
					switch (num)
					{
					case 0:
						if (bookmark.BookmarkStart != null)
						{
							num = 13;
							continue;
						}
						goto IL_CD;
					case 2:
						if (bookmark != null)
						{
							num = 11;
							continue;
						}
						return;
					case 3:
					{
						IEnumerator enumerator = (A_0 as Table).Rows.GetEnumerator();
						num = 6;
						continue;
					}
					case 4:
						if (true)
						{
						}
						if (A_0 is BookmarkStart)
						{
							num = 5;
							continue;
						}
						return;
					case 5:
						bookmark = base.Document.Bookmarks.FindByName((A_0 as BookmarkStart).Name);
						num = 2;
						continue;
					case 6:
						goto IL_4E2;
					case 7:
						goto IL_434;
					case 8:
						bookmark.BookmarkEnd.ᜃ = true;
						num = 15;
						continue;
					case 9:
						try
						{
							num = 7;
							for (;;)
							{
								Bookmark bookmark2;
								switch (num)
								{
								case 0:
								{
									ParagraphBase paragraphBase;
									IEnumerator enumerator2 = (paragraphBase as TextBox).Body.Items.GetEnumerator();
									num = 2;
									continue;
								}
								case 1:
								{
									ParagraphBase paragraphBase;
									if (paragraphBase is BookmarkStart)
									{
										num = 12;
										continue;
									}
									num = 10;
									continue;
								}
								case 2:
									try
									{
										num = 4;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 1;
												continue;
											case 1:
												goto IL_21B;
											case 2:
											{
												IEnumerator enumerator2;
												if (!enumerator2.MoveNext())
												{
													num = 0;
													continue;
												}
												BodyRegion a_ = (BodyRegion)enumerator2.Current;
												this.ᜀ(a_);
												num = 3;
												continue;
											}
											}
											IL_1F5:
											num = 2;
											continue;
											goto IL_1F5;
										}
										IL_21B:
										break;
									}
									finally
									{
										for (;;)
										{
											IEnumerator enumerator2;
											IDisposable disposable = enumerator2 as IDisposable;
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_266;
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
													goto IL_268;
												}
												break;
											}
										}
										IL_266:
										IL_268:;
									}
									goto IL_269;
								case 3:
									bookmark2.BookmarkStart.ᜃ = true;
									num = 6;
									continue;
								case 4:
								{
									IEnumerator enumerator3;
									if (!enumerator3.MoveNext())
									{
										num = 5;
										continue;
									}
									ParagraphBase paragraphBase = (ParagraphBase)enumerator3.Current;
									num = 1;
									continue;
								}
								case 5:
									num = 11;
									continue;
								case 6:
									goto IL_286;
								case 9:
									goto IL_34D;
								case 10:
								{
									ParagraphBase paragraphBase;
									if (paragraphBase is TextBox)
									{
										num = 0;
										continue;
									}
									break;
								}
								case 11:
									goto IL_3A5;
								case 12:
								{
									ParagraphBase paragraphBase;
									bookmark2 = base.Document.Bookmarks.FindByName((paragraphBase as BookmarkStart).Name);
									num = 17;
									continue;
								}
								case 13:
									goto IL_269;
								case 14:
									if (bookmark2.BookmarkStart != null)
									{
										num = 3;
										continue;
									}
									goto IL_286;
								case 15:
									if (bookmark2.BookmarkEnd != null)
									{
										num = 13;
										continue;
									}
									goto IL_34D;
								case 16:
									num = 14;
									continue;
								case 17:
									if (bookmark2 != null)
									{
										num = 16;
										continue;
									}
									break;
								}
								goto IL_150;
								IL_269:
								bookmark2.BookmarkEnd.ᜃ = true;
								num = 9;
								continue;
								IL_286:
								num = 15;
								continue;
								IL_327:
								num = 4;
								continue;
								IL_150:
								goto IL_327;
								IL_34D:
								base.Document.Bookmarks.InnerList.Remove(bookmark2);
								num = 8;
							}
							IL_3A5:
							return;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator3;
								IDisposable disposable2 = enumerator3 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_3F0;
									case 1:
										disposable2.Dispose();
										num = 0;
										continue;
									case 2:
										if (disposable2 != null)
										{
											num = 1;
											continue;
										}
										goto IL_3F2;
									}
									break;
								}
							}
							IL_3F0:
							IL_3F2:;
						}
						goto IL_3F3;
					case 10:
						goto IL_CD;
					case 11:
						num = 0;
						continue;
					case 12:
						if (A_0 is Table)
						{
							num = 3;
							continue;
						}
						num = 4;
						continue;
					case 13:
						goto IL_3F3;
					case 14:
					{
						IEnumerator enumerator3 = (A_0 as Paragraph).Items.GetEnumerator();
						num = 9;
						continue;
					}
					case 15:
						goto IL_411;
					case 16:
						if (bookmark.BookmarkEnd != null)
						{
							num = 8;
							continue;
						}
						goto IL_411;
					}
					if (A_0 is Paragraph)
					{
						num = 14;
						continue;
					}
					num = 12;
					continue;
					IL_CD:
					num = 16;
					continue;
					IL_3F3:
					bookmark.BookmarkStart.ᜃ = true;
					num = 10;
					continue;
					IL_411:
					base.Document.Bookmarks.InnerList.Remove(bookmark);
					num = 7;
				}
				IL_434:
				return;
				IL_4E2:
				try
				{
					num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_731;
						case 1:
						{
							IEnumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator4 = tableRow.Cells.GetEnumerator();
							num = 3;
							continue;
						}
						case 2:
							num = 0;
							continue;
						case 3:
							try
							{
								num = 2;
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
												{
													IEnumerator enumerator5;
													if (!enumerator5.MoveNext())
													{
														num = 2;
														continue;
													}
													BodyRegion a_2 = (BodyRegion)enumerator5.Current;
													this.ᜀ(a_2);
													num = 3;
													continue;
												}
												case 2:
													num = 4;
													continue;
												case 4:
													goto IL_5F1;
												}
												IL_5AA:
												num = 1;
												continue;
												goto IL_5AA;
											}
											IL_5F1:;
										}
										finally
										{
											for (;;)
											{
												IEnumerator enumerator5;
												IDisposable disposable3 = enumerator5 as IDisposable;
												num = 0;
												for (;;)
												{
													switch (num)
													{
													case 0:
														if (disposable3 != null)
														{
															num = 2;
															continue;
														}
														goto IL_63B;
													case 1:
														goto IL_639;
													case 2:
														disposable3.Dispose();
														num = 1;
														continue;
													}
													break;
												}
											}
											IL_639:
											IL_63B:;
										}
										break;
									case 1:
									{
										IEnumerator enumerator4;
										if (!enumerator4.MoveNext())
										{
											num = 4;
											continue;
										}
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_65D;
										default:
										{
											if (false)
											{
											}
											TableCell tableCell = (TableCell)enumerator4.Current;
											IEnumerator enumerator5 = tableCell.Items.GetEnumerator();
											num = 0;
											continue;
										}
										}
										break;
									}
									case 3:
										goto IL_6B4;
									case 4:
										goto IL_65D;
									}
									IL_63C:
									num = 1;
									continue;
									goto IL_63C;
									IL_65D:
									num = 3;
								}
								IL_6B4:;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator4;
									IDisposable disposable4 = enumerator4 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_6FC;
										case 1:
											if (disposable4 != null)
											{
												num = 2;
												continue;
											}
											goto IL_6FE;
										case 2:
											disposable4.Dispose();
											num = 0;
											continue;
										}
										break;
									}
								}
								IL_6FC:
								IL_6FE:;
							}
							break;
						}
						IL_6FF:
						num = 1;
						continue;
						goto IL_6FF;
					}
					IL_731:;
				}
				finally
				{
					for (;;)
					{
						IEnumerator enumerator;
						IDisposable disposable5 = enumerator as IDisposable;
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable5 != null)
								{
									num = 2;
									continue;
								}
								goto IL_77E;
							case 1:
								goto IL_77C;
							case 2:
								disposable5.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_77C:
					IL_77E:;
				}
				return;
			}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0002C348 File Offset: 0x0002B348
		internal DocumentObject ᜁ(DocumentObject A_0)
		{
			int num;
			for (;;)
			{
				num = this.IndexOf(A_0);
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
							goto IL_3E;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						if (num >= 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_3E;
					case 2:
						if (num > base.Count - 2)
						{
							num2 = 3;
							continue;
						}
						goto IL_83;
					case 3:
						goto IL_81;
					}
					break;
				}
			}
			IL_3E:
			return null;
			IL_81:
			goto IL_3E;
			IL_83:
			return this[num + 1];
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0002C3E4 File Offset: 0x0002B3E4
		internal DocumentObject ᜂ(DocumentObject A_0)
		{
			int num;
			for (;;)
			{
				num = this.IndexOf(A_0);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3E;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 1:
						if (num > base.Count - 1)
						{
							num2 = 3;
							continue;
						}
						goto IL_83;
					case 2:
						if (num >= 1)
						{
							num2 = 0;
							continue;
						}
						goto IL_3E;
					case 3:
						goto IL_81;
					}
					break;
				}
			}
			IL_3E:
			return null;
			IL_81:
			goto IL_3E;
			IL_83:
			return this[num - 1];
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0002C480 File Offset: 0x0002B480
		internal int ᜀ(int A_0, DocumentObjectType A_1, bool A_2)
		{
			for (;;)
			{
				IL_00:
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_0 < 0)
						{
							num = 6;
							continue;
						}
						DocumentObject documentObject = base.InnerList[A_0] as DocumentObject;
						num = 4;
						continue;
					}
					case 1:
						return A_0;
					case 2:
						num = 0;
						continue;
					case 3:
						if (A_0 <= base.InnerList.Count - 1)
						{
							num = 2;
							continue;
						}
						return -1;
					case 4:
					{
						DocumentObject documentObject;
						if (documentObject.DocumentObjectType == A_1)
						{
							num = 1;
							continue;
						}
						goto IL_00;
					}
					case 5:
						if (true)
						{
						}
						break;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43;
						default:
							goto IL_8F;
						}
						break;
					}
					goto IL_34;
					IL_43:
					num = 3;
					continue;
					IL_34:
					A_0 += (A_2 ? 1 : -1);
					goto IL_43;
				}
			}
			IL_8F:
			if (false)
			{
			}
			return -1;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0002C568 File Offset: 0x0002B568
		internal void ᜀ(DocumentObjectType A_0)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					DocumentObject documentObject;
					switch (num2)
					{
					case 0:
						if (num >= base.Count)
						{
							num2 = 6;
							continue;
						}
						documentObject = this[num];
						num2 = 1;
						continue;
					case 1:
						goto IL_9E;
					case 2:
						documentObject.ᜀ(null);
						base.InnerList.RemoveAt(num);
						num--;
						num2 = 4;
						continue;
					case 3:
						goto IL_B4;
					case 4:
						goto IL_33;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							goto IL_B4;
						}
						break;
					case 6:
						return;
					}
					break;
					IL_33:
					num++;
					num2 = 5;
					continue;
					IL_B4:
					if (true)
					{
					}
					num2 = 0;
					continue;
					IL_9E:
					if (documentObject.DocumentObjectType != A_0)
					{
						goto IL_33;
					}
					num2 = 2;
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0002C654 File Offset: 0x0002B654
		internal void ᜀ(DocumentObjectCollection A_0)
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3F;
						default:
							goto IL_5D;
						}
						break;
					case 1:
						goto IL_2B;
					case 2:
						if (num >= count)
						{
							goto IL_3F;
						}
						A_0.Add(this[num].Clone());
						num++;
						num2 = 1;
						continue;
					case 3:
						goto IL_2B;
					}
					break;
					IL_2B:
					if (true)
					{
					}
					num2 = 2;
					continue;
					IL_3F:
					num2 = 0;
				}
			}
			IL_5D:
			if (false)
			{
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0002C6F4 File Offset: 0x0002B6F4
		protected virtual void OnClear()
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_37;
						default:
							goto IL_55;
						}
						break;
					case 1:
						if (num >= count)
						{
							goto IL_37;
						}
						this[num].ᜀ(null);
						num++;
						num2 = 3;
						continue;
					case 2:
						goto IL_2B;
					case 3:
						goto IL_2B;
					}
					break;
					IL_2B:
					num2 = 1;
					continue;
					IL_37:
					num2 = 0;
				}
			}
			IL_55:
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.ᜀ(DocumentObjectCollection.ChangeItemsType.Clear, null);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0002C798 File Offset: 0x0002B798
		protected virtual void OnInsert(int index, DocumentObject entity)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.Joined)
					{
						num = 9;
						continue;
					}
					goto IL_1C6;
				case 1:
					goto IL_165;
				case 3:
					goto IL_81;
				case 4:
				{
					Document document;
					if (base.Document != document)
					{
						num = 8;
						continue;
					}
					goto IL_6B;
				}
				case 5:
					goto IL_14E;
				case 6:
				{
					bool flag;
					if (!flag)
					{
						num = 3;
						continue;
					}
					goto IL_14E;
				}
				case 7:
					goto IL_66;
				case 8:
					num = 11;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
					{
						if (false)
						{
						}
						bool flag = entity.Owner == null;
						this.Owner.DeepDetached;
						Document document = entity.Document;
						num = 4;
						continue;
					}
					}
					break;
				case 10:
					goto IL_180;
				case 11:
				{
					bool flag;
					if (!flag)
					{
						num = 10;
						continue;
					}
					entity.CloneRelationsTo(base.Document, this.Owner);
					num = 12;
					continue;
				}
				case 12:
					goto IL_6B;
				}
				if (!this.ᜀ(entity))
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				num = 0;
				continue;
				IL_6B:
				num = 6;
				continue;
				IL_81:
				entity.RemoveSelf();
				num = 5;
				continue;
				IL_14E:
				entity.ᜀ(this.Owner);
				num = 1;
			}
			IL_66:
			string message = string.Format(ClipboardData.b("㍯፱ᩳᡵ᝷๹屻᝽ﲇꪉ낏﶑ﲕﶗ蓮뺝쾟쒡蒣튥톧\udaa9즫躭쮯花즳隵톷풹좻톽뛁곃ꏅ뇉﷋돍", a_), entity.DocumentObjectType, this.Owner.DocumentObjectType);
			throw new ArgumentException(message);
			IL_165:
			goto IL_1C6;
			IL_180:
			throw new InvalidOperationException(ClipboardData.b("⥯ᵱų噵᭷᭹ቻ幽ꚅ꺍ﺏ﶑뒓얟욡蒣쎥욧\udea9얫\udaad즯銱튳쒵ힷힹ鲻톽뒿꫁ꇃ듅껉ꏋ귍ꗏ뿑뇓룕곗", a_));
			IL_1C6:
			this.ᜀ.ᜀ(DocumentObjectCollection.ChangeItemsType.Add, entity);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0002C978 File Offset: 0x0002B978
		protected virtual void OnInsertComplete(int index, DocumentObject entity)
		{
			int num = 6;
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
						goto IL_59;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 2:
					num = 8;
					continue;
				case 3:
					num = 5;
					continue;
				case 4:
					if (entity is Section)
					{
						num = 1;
						continue;
					}
					return;
				case 5:
					goto IL_59;
				case 7:
					if (entity.Document != null)
					{
						num = 3;
						continue;
					}
					return;
				case 8:
					if (true)
					{
					}
					if (!this.Owner.DeepDetached)
					{
						num = 10;
						continue;
					}
					return;
				case 9:
					(entity as Section).ᜄ();
					num = 0;
					continue;
				case 10:
					entity.CloneCommit();
					num = 4;
					continue;
				}
				if (this.Joined)
				{
					num = 2;
					continue;
				}
				break;
				IL_59:
				if (entity.Document.ᜇ)
				{
					break;
				}
				num = 9;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0002CAB0 File Offset: 0x0002BAB0
		protected virtual void OnRemove(int index)
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
			DocumentObject documentObject = this[index];
			documentObject.ᜀ(null);
			this.ᜀ.ᜀ(DocumentObjectCollection.ChangeItemsType.Remove, documentObject);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0002CB08 File Offset: 0x0002BB08
		private bool ᜀ(DocumentObject A_0)
		{
			bool flag;
			for (;;)
			{
				A_0.GetType();
				flag = this.TypesOfElement[0].IsInstanceOfType(A_0);
				int num = 4;
				for (;;)
				{
					if (true)
					{
					}
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
							break;
						}
						num = 2;
						continue;
					case 1:
						flag = this.TypesOfElement[2].IsInstanceOfType(A_0);
						num = 5;
						continue;
					case 2:
						if (this.TypesOfElement.Length > 1)
						{
							num = 7;
							continue;
						}
						goto IL_81;
					case 3:
						goto IL_81;
					case 4:
						if (!flag)
						{
							num = 0;
							continue;
						}
						goto IL_81;
					case 5:
						return flag;
					case 6:
						if (!flag)
						{
							num = 1;
							continue;
						}
						return flag;
					case 7:
						flag = this.TypesOfElement[1].IsInstanceOfType(A_0);
						num = 3;
						continue;
					}
					break;
					IL_81:
					num = 6;
				}
			}
			return flag;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0002CC10 File Offset: 0x0002BC10
		private int ᜅ(int A_0, DocumentObject A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((A_1 as Field).End != null)
					{
						num = 10;
						continue;
					}
					return A_0;
				case 1:
					num = 8;
					continue;
				case 2:
					if (A_1 is FormField)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					num = 5;
					continue;
				case 4:
					return A_0;
				case 5:
					if (A_1 is Field)
					{
						num = 11;
						continue;
					}
					return A_0;
				case 6:
					num = 2;
					continue;
				case 7:
					return A_0;
				case 8:
					if (!this.m_doc.ᜇ)
					{
						num = 6;
						continue;
					}
					return A_0;
				case 9:
					goto IL_101;
				case 10:
					base.Document.ClonedFields.Push(A_1 as Field);
					num = 4;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_101;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (this.m_doc != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_101:
				A_0 = this.ᜂ(A_0, A_1);
				num = 7;
			}
			return A_0;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0002CD6C File Offset: 0x0002BD6C
		private void ᜄ(int A_0, DocumentObject A_1)
		{
			int num = 11;
			Field field;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9A;
				case 1:
					goto IL_10A;
				case 2:
					num = 8;
					continue;
				case 3:
					num = 5;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17E;
					default:
						if (false)
						{
						}
						if (base.Document.ClonedFields.Count > 0)
						{
							num = 9;
							continue;
						}
						return;
					}
					break;
				case 5:
					if (A_1 is FormField)
					{
						num = 1;
						continue;
					}
					num = 10;
					continue;
				case 6:
					if ((A_1 as FieldMark).Type == FieldMarkType.FieldSeparator)
					{
						num = 0;
						continue;
					}
					field = base.Document.ClonedFields.Pop();
					field.End = (A_1 as FieldMark);
					num = 12;
					continue;
				case 7:
					num = 4;
					continue;
				case 8:
					if (!this.m_doc.ᜇ)
					{
						num = 3;
						continue;
					}
					return;
				case 9:
					field = base.Document.ClonedFields.Peek();
					num = 6;
					continue;
				case 10:
					if (A_1 is FieldMark)
					{
						goto IL_17E;
					}
					return;
				case 12:
					return;
				}
				if (this.m_doc != null)
				{
					num = 2;
					continue;
				}
				return;
				IL_17E:
				num = 7;
			}
			IL_9A:
			field.Separator = (A_1 as FieldMark);
			return;
			IL_10A:
			if (true)
			{
			}
			this.ᜁ(A_0, A_1);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0002CF18 File Offset: 0x0002BF18
		private void ᜃ(int A_0, DocumentObject A_1)
		{
			int num = 7;
			Field field;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					num = 10;
					continue;
				case 2:
					field = base.Document.ClonedFields.Peek();
					num = 9;
					continue;
				case 3:
					if (!this.m_doc.ᜇ)
					{
						num = 6;
						continue;
					}
					return;
				case 4:
					if (A_1 is FormField)
					{
						num = 11;
						continue;
					}
					num = 8;
					continue;
				case 5:
					goto IL_9A;
				case 6:
					num = 4;
					continue;
				case 8:
					if (true)
					{
					}
					if (A_1 is FieldMark)
					{
						goto IL_186;
					}
					return;
				case 9:
					if ((A_1 as FieldMark).Type == FieldMarkType.FieldSeparator)
					{
						num = 5;
						continue;
					}
					field = base.Document.ClonedFields.Pop();
					field.End = (A_1 as FieldMark);
					num = 0;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_186;
					default:
						if (false)
						{
						}
						if (base.Document.ClonedFields.Count > 0)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				case 11:
					goto IL_10A;
				case 12:
					num = 3;
					continue;
				}
				if (this.m_doc != null)
				{
					num = 12;
					continue;
				}
				return;
				IL_186:
				num = 1;
			}
			IL_9A:
			field.Separator = (A_1 as FieldMark);
			return;
			IL_10A:
			this.ᜀ(A_0, A_1);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0002D0C4 File Offset: 0x0002C0C4
		private int ᜂ(int A_0, DocumentObject A_1)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					FormFieldType formFieldType = (A_1 as FormField).FormFieldType;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_38A:
						num = 22;
						break;
					default:
						if (false)
						{
						}
						num = 14;
						break;
					}
					for (;;)
					{
						CheckBoxFormField checkBoxFormField;
						TextFormField textFormField;
						DropDownFormField dropDownFormField;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_3A7;
						case 2:
							goto IL_D5;
						case 3:
							num = 18;
							continue;
						case 4:
							if (checkBoxFormField.Name != null)
							{
								num = 9;
								continue;
							}
							goto IL_2D8;
						case 5:
							num = 17;
							continue;
						case 6:
							goto IL_141;
						case 7:
							goto IL_1AF;
						case 8:
							goto IL_341;
						case 9:
							num = 21;
							continue;
						case 10:
							goto IL_2D8;
						case 11:
							if (textFormField.Name != null)
							{
								goto IL_38A;
							}
							goto IL_D5;
						case 12:
							if (dropDownFormField.Name != null)
							{
								num = 5;
								continue;
							}
							goto IL_141;
						case 13:
							goto IL_2A1;
						case 14:
							switch (formFieldType)
							{
							case FormFieldType.TextInput:
								textFormField = (A_1 as TextFormField);
								num = 11;
								continue;
							case FormFieldType.CheckBox:
								checkBoxFormField = (A_1 as CheckBoxFormField);
								num = 4;
								continue;
							case FormFieldType.DropDown:
								dropDownFormField = (A_1 as DropDownFormField);
								num = 12;
								continue;
							default:
								num = 0;
								continue;
							}
							break;
						case 15:
							if (textFormField.Name == string.Empty)
							{
								num = 2;
								continue;
							}
							goto IL_341;
						case 16:
							goto IL_280;
						case 17:
							if (dropDownFormField.Name == string.Empty)
							{
								num = 6;
								continue;
							}
							goto IL_3DB;
						case 18:
							if (textFormField.DefaultText == string.Empty)
							{
								num = 16;
								continue;
							}
							goto IL_3DB;
						case 19:
							goto IL_33C;
						case 20:
							if (textFormField.DefaultText != null)
							{
								num = 3;
								continue;
							}
							goto IL_280;
						case 21:
							if (checkBoxFormField.Name == string.Empty)
							{
								num = 10;
								continue;
							}
							goto IL_3DB;
						case 22:
							num = 15;
							continue;
						}
						break;
						IL_D5:
						string text = ClipboardData.b("㡫୭࡯ٱ⭳", a_) + Guid.NewGuid().ToString().Replace(ClipboardData.b("䅫", a_), ClipboardData.b("㍫", a_));
						textFormField.Name = text.Substring(0, 20);
						num = 8;
						continue;
						IL_141:
						string text2 = ClipboardData.b("⡫ᱭὯɱ⭳", a_) + Guid.NewGuid().ToString().Replace(ClipboardData.b("䅫", a_), ClipboardData.b("㍫", a_));
						dropDownFormField.Name = text2.Substring(0, 20);
						num = 7;
						continue;
						IL_280:
						textFormField.DefaultText = ClipboardData.b("湋汍牏灑癓", a_);
						num = 13;
						continue;
						IL_2D8:
						string text3 = ClipboardData.b("⽫٭ᕯᅱέ⥵", a_) + Guid.NewGuid().ToString().Replace(ClipboardData.b("䅫", a_), ClipboardData.b("㍫", a_));
						checkBoxFormField.Name = text3.Substring(0, 20);
						num = 19;
						continue;
						IL_341:
						num = 20;
					}
				}
				IL_1AF:
				if (true)
				{
				}
				IL_2A1:
				IL_33C:
				IL_3A7:
				IL_3DB:
				(this.Owner as Paragraph).ᜂ((A_1 as FormField).Name);
				(this.Owner as Paragraph).Items.Insert(A_0, new BookmarkStart(base.Document, (A_1 as FormField).Name));
				A_0++;
				return A_0;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0002D4FC File Offset: 0x0002C4FC
		private void ᜁ(int A_0, DocumentObject A_1)
		{
			if (A_1 is TextFormField)
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
					(this.Owner as Paragraph).Items.Insert(++A_0, new FieldMark(this.m_doc, FieldMarkType.FieldSeparator));
					(this.Owner as Paragraph).Items.Insert(++A_0, new FieldMark(this.m_doc, FieldMarkType.FieldEnd));
					(this.Owner as Paragraph).Items.Insert(++A_0, new BookmarkEnd(base.Document, (A_1 as FormField).Name));
					return;
				}
			}
			(this.Owner as Paragraph).Items.Insert(++A_0, new FieldMark(this.m_doc, FieldMarkType.FieldEnd));
			(this.Owner as Paragraph).Items.Insert(++A_0, new BookmarkEnd(base.Document, (A_1 as FormField).Name));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0002D61C File Offset: 0x0002C61C
		private void ᜀ(int A_0, DocumentObject A_1)
		{
			if (A_1 is TextFormField)
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
					(this.Owner as Paragraph).Items.Add(new FieldMark(this.m_doc, FieldMarkType.FieldSeparator));
					(this.Owner as Paragraph).Items.Add(new FieldMark(this.m_doc, FieldMarkType.FieldEnd));
					(this.Owner as Paragraph).Items.Add(new BookmarkEnd(base.Document, (A_1 as FormField).Name));
					return;
				}
			}
			(this.Owner as Paragraph).Items.Add(new FieldMark(this.m_doc, FieldMarkType.FieldEnd));
			(this.Owner as Paragraph).Items.Add(new BookmarkEnd(base.Document, (A_1 as FormField).Name));
		}

		// Token: 0x04000D00 RID: 3328
		internal new DocumentObjectCollection.ᜀ ᜀ = new DocumentObjectCollection.ᜀ();

		// Token: 0x02000531 RID: 1329
		public enum ChangeItemsType
		{
			// Token: 0x04003663 RID: 13923
			Add,
			// Token: 0x04003664 RID: 13924
			Remove,
			// Token: 0x04003665 RID: 13925
			Clear
		}

		// Token: 0x02000532 RID: 1330
		// (Invoke) Token: 0x06004576 RID: 17782
		public delegate void ChangeItems(DocumentObjectCollection.ChangeItemsType type, DocumentObject entity);

		// Token: 0x02000533 RID: 1331
		internal new class ᜀ : IEnumerable
		{
			// Token: 0x06004579 RID: 17785 RVA: 0x00408DD4 File Offset: 0x00407DD4
			public void ᜁ(DocumentObjectCollection.ChangeItems A_0)
			{
				int a_ = 14;
				if (!this.ᜀ.Contains(A_0))
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
						this.ᜀ.Add(A_0);
						return;
					}
				}
				if (true)
				{
				}
				throw new ArgumentException(ClipboardData.b("ᱳ᝵ᙷṹၻ᭽ꊁ慎늑", a_));
			}

			// Token: 0x0600457A RID: 17786 RVA: 0x00408E48 File Offset: 0x00407E48
			public void ᜀ(DocumentObjectCollection.ChangeItems A_0)
			{
				int a_ = 16;
				if (this.ᜀ.Contains(A_0))
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
						this.ᜀ.Remove(A_0);
						return;
					}
				}
				if (true)
				{
				}
				throw new ArgumentException(ClipboardData.b("ṵ᥷ᑹ᡻ችꒃﺉ겋ﮑ", a_));
			}

			// Token: 0x0600457B RID: 17787 RVA: 0x00408EC0 File Offset: 0x00407EC0
			public IEnumerator ᜀ()
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
				return this.ᜀ.GetEnumerator();
			}

			// Token: 0x0600457C RID: 17788 RVA: 0x00408F0C File Offset: 0x00407F0C
			public void ᜀ(DocumentObjectCollection.ChangeItemsType A_0, DocumentObject A_1)
			{
				using (List<DocumentObjectCollection.ChangeItems>.Enumerator enumerator = this.ᜀ.GetEnumerator())
				{
					int num = 4;
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
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								DocumentObjectCollection.ChangeItems changeItems = enumerator.Current;
								changeItems.DynamicInvoke(new object[]
								{
									A_0,
									A_1
								});
								num = 3;
								continue;
							}
							case 1:
								goto IL_A4;
							case 2:
								num = 1;
								continue;
							}
							break;
						}
						IL_7E:
						num = 0;
						continue;
						IL_4E:
						goto IL_7E;
						goto IL_4E;
					}
					IL_A4:;
				}
				if (true)
				{
				}
			}

			// Token: 0x04003666 RID: 13926
			private List<DocumentObjectCollection.ChangeItems> ᜀ = new List<DocumentObjectCollection.ChangeItems>();
		}
	}
}
