using System;
using System.Collections.Generic;
using System.IO;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000DE RID: 222
	internal class PdfCopyFieldsImp : PdfWriter
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00029B03 File Offset: 0x00028B03
		internal PdfCopyFieldsImp(Stream os) : this(os, '\0')
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00029B10 File Offset: 0x00028B10
		internal PdfCopyFieldsImp(Stream os, char pdfVersion) : base(new PdfDocument(), os)
		{
			this.pdf.AddWriter(this);
			if (pdfVersion != '\0')
			{
				base.PdfVersion = pdfVersion;
			}
			this.nd = new Document();
			this.nd.AddDocListener(this.pdf);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00029BCC File Offset: 0x00028BCC
		internal void AddDocument(PdfReader reader, ICollection<int> pagesToKeep)
		{
			if (!this.readers2intrefs.ContainsKey(reader) && reader.Tampered)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("the.document.was.reused"));
			}
			reader = new PdfReader(reader);
			reader.SelectPages(pagesToKeep);
			if (reader.NumberOfPages == 0)
			{
				return;
			}
			reader.Tampered = false;
			this.AddDocument(reader);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00029C28 File Offset: 0x00028C28
		internal void AddDocument(PdfReader reader)
		{
			if (!reader.IsOpenedWithFullPermissions)
			{
				throw new BadPasswordException(MessageLocalization.GetComposedMessage("pdfreader.not.opened.with.owner.password"));
			}
			this.OpenDoc();
			if (this.readers2intrefs.ContainsKey(reader))
			{
				reader = new PdfReader(reader);
			}
			else
			{
				if (reader.Tampered)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("the.document.was.reused"));
				}
				reader.ConsolidateNamedDestinations();
				reader.Tampered = true;
			}
			reader.ShuffleSubsetNames();
			this.readers2intrefs[reader] = new IntHashtable();
			this.readers.Add(reader);
			int numberOfPages = reader.NumberOfPages;
			IntHashtable intHashtable = new IntHashtable();
			for (int i = 1; i <= numberOfPages; i++)
			{
				intHashtable[reader.GetPageOrigRef(i).Number] = 1;
				reader.ReleasePage(i);
			}
			this.pages2intrefs[reader] = intHashtable;
			this.visited[reader] = new IntHashtable();
			this.fields.Add(reader.AcroFields);
			this.UpdateCalculationOrder(reader);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00029D20 File Offset: 0x00028D20
		private static string GetCOName(PdfReader reader, PRIndirectReference refi)
		{
			string text = "";
			while (refi != null)
			{
				PdfObject pdfObject = PdfReader.GetPdfObject(refi);
				if (pdfObject == null || pdfObject.Type != 6)
				{
					break;
				}
				PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
				PdfString asString = pdfDictionary.GetAsString(PdfName.T);
				if (asString != null)
				{
					text = asString.ToUnicodeString() + "." + text;
				}
				refi = (PRIndirectReference)pdfDictionary.Get(PdfName.PARENT);
			}
			if (text.EndsWith("."))
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00029DA4 File Offset: 0x00028DA4
		protected internal void UpdateCalculationOrder(PdfReader reader)
		{
			PdfDictionary catalog = reader.Catalog;
			PdfDictionary asDict = catalog.GetAsDict(PdfName.ACROFORM);
			if (asDict == null)
			{
				return;
			}
			PdfArray asArray = asDict.GetAsArray(PdfName.CO);
			if (asArray == null || asArray.Size == 0)
			{
				return;
			}
			AcroFields acroFields = reader.AcroFields;
			for (int i = 0; i < asArray.Size; i++)
			{
				PdfObject pdfObject = asArray[i];
				if (pdfObject != null && pdfObject.IsIndirect())
				{
					string text = PdfCopyFieldsImp.GetCOName(reader, (PRIndirectReference)pdfObject);
					if (acroFields.GetFieldItem(text) != null)
					{
						text = "." + text;
						if (!this.calculationOrder.Contains(text))
						{
							this.calculationOrder.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00029E58 File Offset: 0x00028E58
		internal void Propagate(PdfObject obj, PdfIndirectReference refo, bool restricted)
		{
			if (obj == null)
			{
				return;
			}
			if (obj is PdfIndirectReference)
			{
				return;
			}
			switch (obj.Type)
			{
			case 5:
				break;
			case 6:
			case 7:
			{
				PdfDictionary pdfDictionary = (PdfDictionary)obj;
				using (Dictionary<PdfName, PdfObject>.KeyCollection.Enumerator enumerator = pdfDictionary.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PdfName pdfName = enumerator.Current;
						if (!restricted || (!pdfName.Equals(PdfName.PARENT) && !pdfName.Equals(PdfName.KIDS)))
						{
							PdfObject pdfObject = pdfDictionary.Get(pdfName);
							if (pdfObject != null && pdfObject.IsIndirect())
							{
								PRIndirectReference prindirectReference = (PRIndirectReference)pdfObject;
								if (!this.SetVisited(prindirectReference) && !this.IsPage(prindirectReference))
								{
									PdfIndirectReference newReference = this.GetNewReference(prindirectReference);
									this.Propagate(PdfReader.GetPdfObjectRelease(prindirectReference), newReference, restricted);
								}
							}
							else
							{
								this.Propagate(pdfObject, null, restricted);
							}
						}
					}
					return;
				}
				break;
			}
			case 8:
			case 9:
				return;
			case 10:
				throw new Exception(MessageLocalization.GetComposedMessage("reference.pointing.to.reference"));
			default:
				return;
			}
			ListIterator<PdfObject> listIterator = ((PdfArray)obj).GetListIterator();
			while (listIterator.HasNext())
			{
				PdfObject pdfObject2 = listIterator.Next();
				if (pdfObject2 != null && pdfObject2.IsIndirect())
				{
					PRIndirectReference prindirectReference2 = (PRIndirectReference)pdfObject2;
					if (!this.IsVisited(prindirectReference2) && !this.IsPage(prindirectReference2))
					{
						PdfIndirectReference newReference2 = this.GetNewReference(prindirectReference2);
						this.Propagate(PdfReader.GetPdfObjectRelease(prindirectReference2), newReference2, restricted);
					}
				}
				else
				{
					this.Propagate(pdfObject2, null, restricted);
				}
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00029FD4 File Offset: 0x00028FD4
		private void AdjustTabOrder(PdfArray annots, PdfIndirectReference ind, PdfNumber nn)
		{
			int intValue = nn.IntValue;
			List<int> list;
			if (!this.tabOrder.TryGetValue(annots, out list))
			{
				list = new List<int>();
				int num = annots.Size - 1;
				for (int i = 0; i < num; i++)
				{
					list.Add(PdfCopyFieldsImp.zero);
				}
				list.Add(intValue);
				this.tabOrder[annots] = list;
				annots.Add(ind);
				return;
			}
			int num2 = list.Count - 1;
			for (int j = num2; j >= 0; j--)
			{
				if (list[j] <= intValue)
				{
					list.Insert(j + 1, intValue);
					annots.Add(j + 1, ind);
					num2 = -2;
					break;
				}
			}
			if (num2 != -2)
			{
				list.Insert(0, intValue);
				annots.Add(0, ind);
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002A094 File Offset: 0x00029094
		protected PdfArray BranchForm(Dictionary<string, object> level, PdfIndirectReference parent, string fname)
		{
			PdfArray pdfArray = new PdfArray();
			foreach (KeyValuePair<string, object> keyValuePair in level)
			{
				string key = keyValuePair.Key;
				object value = keyValuePair.Value;
				PdfIndirectReference pdfIndirectReference = base.PdfIndirectReference;
				PdfDictionary pdfDictionary = new PdfDictionary();
				if (parent != null)
				{
					pdfDictionary.Put(PdfName.PARENT, parent);
				}
				pdfDictionary.Put(PdfName.T, new PdfString(key, "UnicodeBig"));
				string text = fname + "." + key;
				int num = this.calculationOrder.IndexOf(text);
				if (num >= 0)
				{
					this.calculationOrderRefs[num] = pdfIndirectReference;
				}
				if (value is Dictionary<string, object>)
				{
					pdfDictionary.Put(PdfName.KIDS, this.BranchForm((Dictionary<string, object>)value, pdfIndirectReference, text));
					pdfArray.Add(pdfIndirectReference);
					base.AddToBody(pdfDictionary, pdfIndirectReference);
				}
				else
				{
					List<object> list = (List<object>)value;
					pdfDictionary.MergeDifferent((PdfDictionary)list[0]);
					if (list.Count == 3)
					{
						pdfDictionary.MergeDifferent((PdfDictionary)list[2]);
						int num2 = (int)list[1];
						PdfDictionary pdfDictionary2 = this.pageDics[num2 - 1];
						PdfArray pdfArray2 = pdfDictionary2.GetAsArray(PdfName.ANNOTS);
						if (pdfArray2 == null)
						{
							pdfArray2 = new PdfArray();
							pdfDictionary2.Put(PdfName.ANNOTS, pdfArray2);
						}
						PdfNumber nn = (PdfNumber)pdfDictionary.Get(PdfCopyFieldsImp.iTextTag);
						pdfDictionary.Remove(PdfCopyFieldsImp.iTextTag);
						this.AdjustTabOrder(pdfArray2, pdfIndirectReference, nn);
					}
					else
					{
						PdfArray pdfArray3 = new PdfArray();
						for (int i = 1; i < list.Count; i += 2)
						{
							int num3 = (int)list[i];
							PdfDictionary pdfDictionary3 = this.pageDics[num3 - 1];
							PdfArray pdfArray4 = pdfDictionary3.GetAsArray(PdfName.ANNOTS);
							if (pdfArray4 == null)
							{
								pdfArray4 = new PdfArray();
								pdfDictionary3.Put(PdfName.ANNOTS, pdfArray4);
							}
							PdfDictionary pdfDictionary4 = new PdfDictionary();
							pdfDictionary4.Merge((PdfDictionary)list[i + 1]);
							pdfDictionary4.Put(PdfName.PARENT, pdfIndirectReference);
							PdfNumber nn2 = (PdfNumber)pdfDictionary4.Get(PdfCopyFieldsImp.iTextTag);
							pdfDictionary4.Remove(PdfCopyFieldsImp.iTextTag);
							PdfIndirectReference indirectReference = base.AddToBody(pdfDictionary4).IndirectReference;
							this.AdjustTabOrder(pdfArray4, indirectReference, nn2);
							pdfArray3.Add(indirectReference);
							this.Propagate(pdfDictionary4, null, false);
						}
						pdfDictionary.Put(PdfName.KIDS, pdfArray3);
					}
					pdfArray.Add(pdfIndirectReference);
					base.AddToBody(pdfDictionary, pdfIndirectReference);
					this.Propagate(pdfDictionary, null, false);
				}
			}
			return pdfArray;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002A370 File Offset: 0x00029370
		protected void CreateAcroForms()
		{
			if (this.fieldTree.Count == 0)
			{
				return;
			}
			this.form = new PdfDictionary();
			this.form.Put(PdfName.DR, this.resources);
			this.Propagate(this.resources, null, false);
			this.form.Put(PdfName.DA, new PdfString("/Helv 0 Tf 0 g "));
			this.tabOrder = new Dictionary<PdfArray, List<int>>();
			this.calculationOrderRefs = new List<object>();
			foreach (string item in this.calculationOrder)
			{
				this.calculationOrderRefs.Add(item);
			}
			this.form.Put(PdfName.FIELDS, this.BranchForm(this.fieldTree, null, ""));
			if (this.hasSignature)
			{
				this.form.Put(PdfName.SIGFLAGS, new PdfNumber(3));
			}
			PdfArray pdfArray = new PdfArray();
			for (int i = 0; i < this.calculationOrderRefs.Count; i++)
			{
				object obj = this.calculationOrderRefs[i];
				if (obj is PdfIndirectReference)
				{
					pdfArray.Add((PdfIndirectReference)obj);
				}
			}
			if (pdfArray.Size > 0)
			{
				this.form.Put(PdfName.CO, pdfArray);
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002A4D0 File Offset: 0x000294D0
		public override void Close()
		{
			if (this.closing)
			{
				base.Close();
				return;
			}
			this.closing = true;
			this.CloseIt();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002A4F0 File Offset: 0x000294F0
		protected void CloseIt()
		{
			for (int i = 0; i < this.readers.Count; i++)
			{
				this.readers[i].RemoveFields();
			}
			for (int j = 0; j < this.readers.Count; j++)
			{
				PdfReader pdfReader = this.readers[j];
				for (int k = 1; k <= pdfReader.NumberOfPages; k++)
				{
					this.pageRefs.Add(this.GetNewReference(pdfReader.GetPageOrigRef(k)));
					this.pageDics.Add(pdfReader.GetPageN(k));
				}
			}
			this.MergeFields();
			this.CreateAcroForms();
			for (int l = 0; l < this.readers.Count; l++)
			{
				PdfReader pdfReader2 = this.readers[l];
				for (int m = 1; m <= pdfReader2.NumberOfPages; m++)
				{
					PdfDictionary pageN = pdfReader2.GetPageN(m);
					PdfIndirectReference newReference = this.GetNewReference(pdfReader2.GetPageOrigRef(m));
					PdfIndirectReference value = this.root.AddPageRef(newReference);
					pageN.Put(PdfName.PARENT, value);
					this.Propagate(pageN, newReference, false);
				}
			}
			foreach (KeyValuePair<PdfReader, IntHashtable> keyValuePair in this.readers2intrefs)
			{
				PdfReader key = keyValuePair.Key;
				try
				{
					this.file = key.SafeFile;
					this.file.ReOpen();
					IntHashtable value2 = keyValuePair.Value;
					int[] array = value2.ToOrderedKeys();
					for (int n = 0; n < array.Length; n++)
					{
						PRIndirectReference obj = new PRIndirectReference(key, array[n]);
						base.AddToBody(PdfReader.GetPdfObjectRelease(obj), value2[array[n]]);
					}
				}
				finally
				{
					try
					{
						this.file.Close();
						key.Close();
					}
					catch
					{
					}
				}
			}
			this.pdf.Close();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002A704 File Offset: 0x00029704
		internal void AddPageOffsetToField(Dictionary<string, AcroFields.Item> fd, int pageOffset)
		{
			if (pageOffset == 0)
			{
				return;
			}
			foreach (AcroFields.Item item in fd.Values)
			{
				List<int> page = item.page;
				for (int i = 0; i < page.Count; i++)
				{
					int page2 = item.GetPage(i);
					item.ForcePage(i, page2 + pageOffset);
				}
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002A780 File Offset: 0x00029780
		internal void CreateWidgets(List<object> list, AcroFields.Item item)
		{
			for (int i = 0; i < item.Size; i++)
			{
				list.Add(item.GetPage(i));
				PdfDictionary merged = item.GetMerged(i);
				PdfObject pdfObject = merged.Get(PdfName.DR);
				if (pdfObject != null)
				{
					PdfFormField.MergeResources(this.resources, (PdfDictionary)PdfReader.GetPdfObject(pdfObject));
				}
				PdfDictionary pdfDictionary = new PdfDictionary();
				foreach (PdfName key in merged.Keys)
				{
					if (PdfCopyFieldsImp.widgetKeys.ContainsKey(key))
					{
						pdfDictionary.Put(key, merged.Get(key));
					}
				}
				pdfDictionary.Put(PdfCopyFieldsImp.iTextTag, new PdfNumber(item.GetTabOrder(i) + 1));
				list.Add(pdfDictionary);
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002A868 File Offset: 0x00029868
		internal void MergeField(string name, AcroFields.Item item)
		{
			Dictionary<string, object> dictionary = this.fieldTree;
			StringTokenizer stringTokenizer = new StringTokenizer(name, ".");
			if (!stringTokenizer.HasMoreTokens())
			{
				return;
			}
			object obj;
			for (;;)
			{
				string key = stringTokenizer.NextToken();
				dictionary.TryGetValue(key, out obj);
				if (!stringTokenizer.HasMoreTokens())
				{
					goto IL_61;
				}
				if (obj == null)
				{
					obj = new Dictionary<string, object>();
					dictionary[key] = obj;
					dictionary = (Dictionary<string, object>)obj;
				}
				else
				{
					if (!(obj is Dictionary<string, object>))
					{
						break;
					}
					dictionary = (Dictionary<string, object>)obj;
				}
			}
			return;
			IL_61:
			if (obj is Dictionary<string, object>)
			{
				return;
			}
			PdfDictionary merged = item.GetMerged(0);
			if (obj == null)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				if (PdfName.SIG.Equals(merged.Get(PdfName.FT)))
				{
					this.hasSignature = true;
				}
				foreach (PdfName key2 in merged.Keys)
				{
					if (PdfCopyFieldsImp.fieldKeys.ContainsKey(key2))
					{
						pdfDictionary.Put(key2, merged.Get(key2));
					}
				}
				List<object> list = new List<object>();
				list.Add(pdfDictionary);
				this.CreateWidgets(list, item);
				string key;
				dictionary[key] = list;
				return;
			}
			List<object> list2 = (List<object>)obj;
			PdfDictionary pdfDictionary2 = (PdfDictionary)list2[0];
			PdfName pdfName = (PdfName)pdfDictionary2.Get(PdfName.FT);
			PdfName obj2 = (PdfName)merged.Get(PdfName.FT);
			if (pdfName == null || !pdfName.Equals(obj2))
			{
				return;
			}
			int num = 0;
			PdfObject pdfObject = pdfDictionary2.Get(PdfName.FF);
			if (pdfObject != null && pdfObject.IsNumber())
			{
				num = ((PdfNumber)pdfObject).IntValue;
			}
			int num2 = 0;
			PdfObject pdfObject2 = merged.Get(PdfName.FF);
			if (pdfObject2 != null && pdfObject2.IsNumber())
			{
				num2 = ((PdfNumber)pdfObject2).IntValue;
			}
			if (pdfName.Equals(PdfName.BTN))
			{
				if (((num ^ num2) & 65536) != 0)
				{
					return;
				}
				if ((num & 65536) == 0 && ((num ^ num2) & 32768) != 0)
				{
					return;
				}
			}
			else if (pdfName.Equals(PdfName.CH) && ((num ^ num2) & 131072) != 0)
			{
				return;
			}
			this.CreateWidgets(list2, item);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002AA98 File Offset: 0x00029A98
		internal void MergeWithMaster(Dictionary<string, AcroFields.Item> fd)
		{
			foreach (KeyValuePair<string, AcroFields.Item> keyValuePair in fd)
			{
				string key = keyValuePair.Key;
				this.MergeField(key, keyValuePair.Value);
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002AAF8 File Offset: 0x00029AF8
		internal virtual void MergeFields()
		{
			int num = 0;
			for (int i = 0; i < this.fields.Count; i++)
			{
				Dictionary<string, AcroFields.Item> fd = this.fields[i].Fields;
				this.AddPageOffsetToField(fd, num);
				this.MergeWithMaster(fd);
				num += this.readers[i].NumberOfPages;
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002AB52 File Offset: 0x00029B52
		public override PdfIndirectReference GetPageReference(int page)
		{
			return this.pageRefs[page - 1];
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002AB64 File Offset: 0x00029B64
		protected override PdfDictionary GetCatalog(PdfIndirectReference rootObj)
		{
			PdfDictionary catalog = this.pdf.GetCatalog(rootObj);
			if (this.form != null)
			{
				PdfIndirectReference indirectReference = base.AddToBody(this.form).IndirectReference;
				catalog.Put(PdfName.ACROFORM, indirectReference);
			}
			return catalog;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002ABA5 File Offset: 0x00029BA5
		protected PdfIndirectReference GetNewReference(PRIndirectReference refi)
		{
			return new PdfIndirectReference(0, this.GetNewObjectNumber(refi.Reader, refi.Number, 0));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002ABC0 File Offset: 0x00029BC0
		protected internal override int GetNewObjectNumber(PdfReader reader, int number, int generation)
		{
			IntHashtable intHashtable = this.readers2intrefs[reader];
			int num = intHashtable[number];
			if (num == 0)
			{
				num = base.IndirectReferenceNumber;
				intHashtable[number] = num;
			}
			return num;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0002ABF8 File Offset: 0x00029BF8
		protected internal bool SetVisited(PRIndirectReference refi)
		{
			IntHashtable intHashtable;
			if (this.visited.TryGetValue(refi.Reader, out intHashtable))
			{
				int num = intHashtable[refi.Number];
				intHashtable[refi.Number] = 1;
				return num != 0;
			}
			return false;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002AC40 File Offset: 0x00029C40
		protected internal bool IsVisited(PRIndirectReference refi)
		{
			IntHashtable intHashtable;
			return this.visited.TryGetValue(refi.Reader, out intHashtable) && intHashtable.ContainsKey(refi.Number);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002AC70 File Offset: 0x00029C70
		protected internal bool IsVisited(PdfReader reader, int number, int generation)
		{
			IntHashtable intHashtable = this.readers2intrefs[reader];
			return intHashtable.ContainsKey(number);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002AC94 File Offset: 0x00029C94
		protected internal bool IsPage(PRIndirectReference refi)
		{
			IntHashtable intHashtable;
			return this.pages2intrefs.TryGetValue(refi.Reader, out intHashtable) && intHashtable.ContainsKey(refi.Number);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002ACC4 File Offset: 0x00029CC4
		internal override RandomAccessFileOrArray GetReaderFile(PdfReader reader)
		{
			return this.file;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002ACCC File Offset: 0x00029CCC
		public void OpenDoc()
		{
			if (!this.nd.IsOpen())
			{
				this.nd.Open();
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002ACE8 File Offset: 0x00029CE8
		static PdfCopyFieldsImp()
		{
			int value = 1;
			PdfCopyFieldsImp.widgetKeys[PdfName.SUBTYPE] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.CONTENTS] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.RECT] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.NM] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.M] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.F] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.BS] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.BORDER] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.AP] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.AS] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.C] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.A] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.STRUCTPARENT] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.OC] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.H] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.MK] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.DA] = value;
			PdfCopyFieldsImp.widgetKeys[PdfName.Q] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.AA] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.FT] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.TU] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.TM] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.FF] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.V] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.DV] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.DS] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.RV] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.OPT] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.MAXLEN] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.TI] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.I] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.LOCK] = value;
			PdfCopyFieldsImp.fieldKeys[PdfName.SV] = value;
		}

		// Token: 0x040006BF RID: 1727
		private static readonly PdfName iTextTag = new PdfName("_iTextTag_");

		// Token: 0x040006C0 RID: 1728
		private static int zero = 0;

		// Token: 0x040006C1 RID: 1729
		internal List<PdfReader> readers = new List<PdfReader>();

		// Token: 0x040006C2 RID: 1730
		internal Dictionary<PdfReader, IntHashtable> readers2intrefs = new Dictionary<PdfReader, IntHashtable>();

		// Token: 0x040006C3 RID: 1731
		internal Dictionary<PdfReader, IntHashtable> pages2intrefs = new Dictionary<PdfReader, IntHashtable>();

		// Token: 0x040006C4 RID: 1732
		internal Dictionary<PdfReader, IntHashtable> visited = new Dictionary<PdfReader, IntHashtable>();

		// Token: 0x040006C5 RID: 1733
		internal List<AcroFields> fields = new List<AcroFields>();

		// Token: 0x040006C6 RID: 1734
		internal RandomAccessFileOrArray file;

		// Token: 0x040006C7 RID: 1735
		internal Dictionary<string, object> fieldTree = new Dictionary<string, object>();

		// Token: 0x040006C8 RID: 1736
		internal List<PdfIndirectReference> pageRefs = new List<PdfIndirectReference>();

		// Token: 0x040006C9 RID: 1737
		internal List<PdfDictionary> pageDics = new List<PdfDictionary>();

		// Token: 0x040006CA RID: 1738
		internal PdfDictionary resources = new PdfDictionary();

		// Token: 0x040006CB RID: 1739
		internal PdfDictionary form;

		// Token: 0x040006CC RID: 1740
		private bool closing;

		// Token: 0x040006CD RID: 1741
		internal Document nd;

		// Token: 0x040006CE RID: 1742
		private Dictionary<PdfArray, List<int>> tabOrder;

		// Token: 0x040006CF RID: 1743
		private List<string> calculationOrder = new List<string>();

		// Token: 0x040006D0 RID: 1744
		private List<object> calculationOrderRefs;

		// Token: 0x040006D1 RID: 1745
		private bool hasSignature;

		// Token: 0x040006D2 RID: 1746
		protected internal static Dictionary<PdfName, int> widgetKeys = new Dictionary<PdfName, int>();

		// Token: 0x040006D3 RID: 1747
		protected internal static Dictionary<PdfName, int> fieldKeys = new Dictionary<PdfName, int>();
	}
}
