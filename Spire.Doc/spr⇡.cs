using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Pdf;
using Spire.Pdf.Actions;
using Spire.Pdf.Annotations;
using Spire.Pdf.Bookmarks;
using Spire.Pdf.General;
using Spire.Pdf.Graphics;

// Token: 0x02000446 RID: 1094
[ToolboxItem(false)]
internal class spr\u21E1 : Component
{
	// Token: 0x06003CDF RID: 15583 RVA: 0x0038B560 File Offset: 0x0038A560
	private List<PageSetup> ᜁ()
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
		return this.ᜁ;
	}

	// Token: 0x06003CE0 RID: 15584 RVA: 0x0038B5A4 File Offset: 0x0038A5A4
	internal void ᜀ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x0038B5E8 File Offset: 0x0038A5E8
	public spr\u21E1()
	{
		this.ᜁ = new List<PageSetup>();
	}

	// Token: 0x06003CE2 RID: 15586 RVA: 0x0038B610 File Offset: 0x0038A610
	public PdfNewDocument ᜀ(Document A_0)
	{
		PdfNewDocument result;
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
			spr\u1A69 spr_u1A = new spr\u1A69();
			try
			{
				A_0.OperationType = DocumentOperationType.Layout;
				spr_u1A.ᜁ(A_0);
				A_0.OperationType = DocumentOperationType.None;
				result = this.ᜀ(spr_u1A);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_85;
					case 2:
						((IDisposable)spr_u1A).Dispose();
						num = 1;
						continue;
					}
					if (spr_u1A == null)
					{
						goto IL_8F;
					}
					num = 2;
				}
				IL_85:
				if (true)
				{
				}
				IL_8F:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x06003CE3 RID: 15587 RVA: 0x0038B6C0 File Offset: 0x0038A6C0
	public PdfNewDocument ᜀ(Document A_0, ToPdfParameterList A_1)
	{
		PdfNewDocument result;
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
			spr\u1A69 spr_u1A = new spr\u1A69();
			try
			{
				if (true)
				{
				}
				A_0.OperationType = DocumentOperationType.Layout;
				spr_u1A.ᜀ(A_1);
				spr_u1A.ᜁ(A_0);
				A_0.OperationType = DocumentOperationType.None;
				result = this.ᜀ(spr_u1A, A_1.EmbeddedFontNameList);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_9A;
					case 2:
						((IDisposable)spr_u1A).Dispose();
						num = 1;
						continue;
					}
					if (spr_u1A == null)
					{
						break;
					}
					num = 2;
				}
				IL_9A:;
			}
			break;
		}
		}
		return result;
	}

	// Token: 0x06003CE4 RID: 15588 RVA: 0x0038B77C File Offset: 0x0038A77C
	public PdfNewDocument ᜀ(Document A_0, List<string> A_1)
	{
		PdfNewDocument result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return result;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1A69 spr_u1A = new spr\u1A69();
		try
		{
			A_0.OperationType = DocumentOperationType.Layout;
			spr_u1A.ᜀ(new ToPdfParameterList());
			spr_u1A.ᜁ(A_0);
			A_0.OperationType = DocumentOperationType.None;
			result = this.ᜀ(spr_u1A, A_1);
		}
		finally
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_99;
				case 2:
					((IDisposable)spr_u1A).Dispose();
					num = 0;
					continue;
				}
				if (spr_u1A == null)
				{
					break;
				}
				num = 2;
			}
			IL_99:;
		}
		return result;
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x0038B838 File Offset: 0x0038A838
	public PdfNewDocument ᜀ(string A_0)
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
		Document a_ = new Document(A_0, Spire.Doc.FileFormat.Auto);
		return this.ᜀ(a_);
	}

	// Token: 0x06003CE6 RID: 15590 RVA: 0x0038B884 File Offset: 0x0038A884
	public PdfNewDocument ᜀ(Stream A_0)
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
		Document a_ = new Document(A_0, Spire.Doc.FileFormat.Auto);
		return this.ᜀ(a_);
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x0038B8D0 File Offset: 0x0038A8D0
	private PdfNewDocument ᜀ()
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
		PdfNewDocument pdfNewDocument = new PdfNewDocument();
		pdfNewDocument.ᜈ();
		pdfNewDocument.PageSettings.Margins.All = 0f;
		pdfNewDocument.FileStructure.CrossReferenceType = PdfCrossReferenceType.CrossReferenceTable;
		pdfNewDocument.FileStructure.Version = PdfVersion.Version1_4;
		return pdfNewDocument;
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x0038B948 File Offset: 0x0038A948
	private PdfSection ᜀ(PageSetup A_0)
	{
		int a_ = 17;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		PdfSection pdfSection = this.ᜂ.Sections.Add();
		pdfSection.PageSettings.Margins.All = 0f;
		pdfSection.PageSettings.Orientation = ((A_0.Orientation == PageOrientation.Portrait || A_0.Orientation.ToString() == ClipboardData.b("䙶", a_)) ? PdfPageOrientation.Portrait : PdfPageOrientation.Landscape);
		pdfSection.PageSettings.Size = A_0.PageSize;
		return pdfSection;
	}

	// Token: 0x06003CE9 RID: 15593 RVA: 0x0038BA08 File Offset: 0x0038AA08
	private void ᜁ(spr\u1A69 A_0)
	{
		for (;;)
		{
			IL_34:
			if (true)
			{
			}
			int num = 0;
			int num2 = 2;
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
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_52;
					case 2:
						goto IL_52;
					case 3:
						goto IL_5A;
					}
					goto IL_34;
					IL_52:
					num2 = 3;
					continue;
				}
				IL_5A:
				if (num >= A_0.ᜤ().Count)
				{
					num2 = 0;
				}
				else
				{
					spr\u1F89 spr_u1F = A_0.ᜤ()[num];
					this.ᜁ().Add(spr_u1F.ᜂ());
					num++;
					num2 = 1;
				}
			}
		}
	}

	// Token: 0x06003CEA RID: 15594 RVA: 0x0038BAB8 File Offset: 0x0038AAB8
	private void ᜀ(BuiltinDocumentProperties A_0)
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
		this.ᜂ.DocumentInformation.Author = A_0.Author;
		this.ᜂ.DocumentInformation.CreationDate = A_0.CreateDate;
		this.ᜂ.DocumentInformation.Creator = A_0.Company;
		this.ᜂ.DocumentInformation.Keywords = A_0.Keywords;
		this.ᜂ.DocumentInformation.Producer = A_0.Company;
		this.ᜂ.DocumentInformation.Subject = A_0.Subject;
		this.ᜂ.DocumentInformation.Title = A_0.Title;
	}

	// Token: 0x06003CEB RID: 15595 RVA: 0x0038BB90 File Offset: 0x0038AB90
	private void ᜀ(List<Dictionary<string, RectangleF>> A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= A_0.Count)
						{
							num2 = 2;
							continue;
						}
						Dictionary<string, RectangleF>.Enumerator enumerator = A_0[num].GetEnumerator();
						num2 = 4;
						continue;
					}
					case 1:
						goto IL_91;
					case 2:
						return;
					case 3:
						goto IL_91;
					case 4:
						try
						{
							num2 = 3;
							for (;;)
							{
								switch (num2)
								{
								case 0:
								{
									PdfUriAnnotation pdfUriAnnotation;
									(this.ᜃ as PdfNewPage).Annotations.Add(pdfUriAnnotation);
									num2 = 6;
									continue;
								}
								case 1:
								{
									string key;
									if (!key.Equals(string.Empty))
									{
										num2 = 8;
										continue;
									}
									break;
								}
								case 2:
								{
									Dictionary<string, RectangleF>.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num2 = 9;
										continue;
									}
									KeyValuePair<string, RectangleF> keyValuePair = enumerator.Current;
									RectangleF value = keyValuePair.Value;
									string key = keyValuePair.Key;
									num2 = 1;
									continue;
								}
								case 4:
									goto IL_201;
								case 5:
								{
									if (this.ᜃ is PdfNewPage)
									{
										num2 = 0;
										continue;
									}
									PdfUriAnnotation pdfUriAnnotation;
									(this.ᜃ as PdfPageWidget).AnnotationsWidget.Add(pdfUriAnnotation);
									num2 = 7;
									continue;
								}
								case 8:
								{
									RectangleF value;
									PdfUriAnnotation pdfUriAnnotation = new PdfUriAnnotation(value);
									string key;
									pdfUriAnnotation.Uri = key;
									pdfUriAnnotation.Border.Width = 0f;
									num2 = 5;
									continue;
								}
								case 9:
									num2 = 4;
									continue;
								}
								IL_11C:
								num2 = 2;
								continue;
								goto IL_11C;
							}
							IL_201:
							goto IL_45;
						}
						finally
						{
							Dictionary<string, RectangleF>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						return;
						IL_45:
						if (true)
						{
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					}
					break;
					IL_91:
					num2 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06003CEC RID: 15596 RVA: 0x0038BDD0 File Offset: 0x0038ADD0
	private void ᜀ(List<Dictionary<string, spr᮶>> A_0)
	{
		int num;
		Dictionary<string, PdfBookmark> dictionary;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
					try
					{
						num = 24;
						for (;;)
						{
							KeyValuePair<string, spr᮶> keyValuePair;
							string key;
							PdfBookmark pdfBookmark3;
							switch (num)
							{
							case 0:
								goto IL_141;
							case 1:
								goto IL_3FA;
							case 2:
								num = 4;
								continue;
							case 3:
								goto IL_3FA;
							case 4:
								if (keyValuePair.Value.ᜃ() == 1)
								{
									num = 5;
									continue;
								}
								goto IL_1DF;
							case 5:
								dictionary.Clear();
								num = 20;
								continue;
							case 6:
							{
								spr᮶ value;
								PdfBookmark pdfBookmark = this.ᜂ.Bookmarks.Add(value.ᜆ());
								pdfBookmark.Color = Color.SaddleBrown;
								pdfBookmark.DisplayStyle = PdfTextStyle.Bold;
								PdfDocumentLinkAnnotation pdfDocumentLinkAnnotation;
								PdfDestination destination;
								pdfDocumentLinkAnnotation.Destination = destination;
								pdfBookmark.Action = new PdfGoToAction(pdfDocumentLinkAnnotation.Destination);
								dictionary.Add(keyValuePair.Value.ᜃ().ToString(), pdfBookmark);
								num = 1;
								continue;
							}
							case 7:
								if (dictionary.ContainsKey(keyValuePair.Value.ᜃ().ToString()))
								{
									num = 16;
									continue;
								}
								goto IL_141;
							case 8:
							{
								PdfBookmark pdfBookmark2;
								if (pdfBookmark2 != null)
								{
									num = 26;
									continue;
								}
								goto IL_3FA;
							}
							case 9:
							{
								spr᮶ value;
								PdfDestination destination = new PdfDestination(this.ᜂ.Pages[value.ᜇ() - 1], value.ᜂ().Location);
								num = 11;
								continue;
							}
							case 10:
								num = 22;
								continue;
							case 11:
							{
								if (keyValuePair.Value.ᜃ() == 1)
								{
									num = 6;
									continue;
								}
								PdfBookmark pdfBookmark2 = null;
								dictionary.TryGetValue((keyValuePair.Value.ᜃ() - 1).ToString(), out pdfBookmark2);
								num = 8;
								continue;
							}
							case 12:
							{
								spr᮶ value;
								if (value.ᜀ() == this.ᜂ.Pages.IndexOf(this.ᜃ) + 1)
								{
									num = 2;
									continue;
								}
								break;
							}
							case 13:
								num = 21;
								continue;
							case 15:
							{
								PdfDocumentLinkAnnotation pdfDocumentLinkAnnotation;
								(this.ᜃ as PdfNewPage).Annotations.Add(pdfDocumentLinkAnnotation);
								num = 14;
								continue;
							}
							case 16:
								dictionary.Remove(keyValuePair.Value.ᜃ().ToString());
								num = 0;
								continue;
							case 17:
							{
								Dictionary<string, spr᮶>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 10;
									continue;
								}
								keyValuePair = enumerator.Current;
								spr᮶ value = keyValuePair.Value;
								num = 12;
								continue;
							}
							case 18:
							{
								spr᮶ value;
								if (this.ᜂ.Pages.Count >= value.ᜇ())
								{
									num = 13;
									continue;
								}
								goto IL_3FA;
							}
							case 19:
							{
								float height = this.ᜂ.PageSettings.Height;
								spr᮶ value;
								PdfDocumentLinkAnnotation pdfDocumentLinkAnnotation = new PdfDocumentLinkAnnotation(new RectangleF(new PointF(value.ᜁ().X, height - value.ᜁ().Bottom - value.ᜁ().Height), value.ᜁ().Size));
								pdfDocumentLinkAnnotation.Border = new PdfAnnotationBorder(0f);
								num = 18;
								continue;
							}
							case 20:
								goto IL_1DF;
							case 21:
							{
								spr᮶ value;
								if (value.ᜇ() != 0)
								{
									num = 9;
									continue;
								}
								goto IL_3FA;
							}
							case 22:
								goto IL_58B;
							case 23:
							{
								if (this.ᜃ is PdfNewPage)
								{
									num = 15;
									continue;
								}
								PdfDocumentLinkAnnotation pdfDocumentLinkAnnotation;
								(this.ᜃ as PdfPageWidget).AnnotationsWidget.Add(pdfDocumentLinkAnnotation);
								num = 27;
								continue;
							}
							case 25:
								if (!key.Equals(string.Empty))
								{
									num = 19;
									continue;
								}
								break;
							case 26:
							{
								spr᮶ value;
								PdfBookmark pdfBookmark2;
								pdfBookmark3 = pdfBookmark2.Add(value.ᜆ());
								pdfBookmark3.Color = Color.SaddleBrown;
								pdfBookmark3.DisplayStyle = PdfTextStyle.Bold;
								PdfDocumentLinkAnnotation pdfDocumentLinkAnnotation;
								PdfDestination destination;
								pdfDocumentLinkAnnotation.Destination = destination;
								pdfBookmark3.Action = new PdfGoToAction(pdfDocumentLinkAnnotation.Destination);
								num = 7;
								continue;
							}
							}
							goto IL_13C;
							IL_141:
							dictionary.Add(keyValuePair.Value.ᜃ().ToString(), pdfBookmark3);
							num = 3;
							continue;
							IL_1DF:
							key = keyValuePair.Key;
							num = 25;
							continue;
							IL_23C:
							num = 17;
							continue;
							IL_13C:
							goto IL_23C;
							IL_3FA:
							num = 23;
						}
						IL_58B:
						goto IL_5D;
					}
					finally
					{
						Dictionary<string, spr᮶>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					return;
					IL_5D:
					if (true)
					{
					}
					num2++;
					num = 3;
					continue;
				case 1:
				{
					if (num2 >= A_0.Count)
					{
						num = 4;
						continue;
					}
					Dictionary<string, spr᮶>.Enumerator enumerator = A_0[num2].GetEnumerator();
					num = 0;
					continue;
				}
				case 2:
					goto IL_8D;
				case 3:
					goto IL_8D;
				case 4:
					return;
				}
				goto IL_47;
				IL_8D:
				num = 1;
			}
			return;
		default:
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			}
			break;
		}
		IL_47:
		dictionary = new Dictionary<string, PdfBookmark>();
		num2 = 0;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06003CED RID: 15597 RVA: 0x0038C398 File Offset: 0x0038B398
	private PdfNewDocument ᜀ(spr\u1A69 A_0)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				this.ᜁ(A_0);
				int count = A_0.ᜤ().Count;
				this.ᜂ = this.ᜀ();
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						try
						{
							MemoryStream memoryStream;
							A_0.ᜀ(num, 1, ImageType.Metafile, memoryStream, false, false, true);
							PdfSection pdfSection = this.ᜀ(this.ᜁ()[num]);
							PdfNewPage pdfNewPage = pdfSection.Pages.Add();
							this.ᜃ = pdfNewPage;
							PdfMetafile pdfMetafile = (PdfMetafile)PdfImage.FromImage(A_0.\u171E().ᜀ()[num].ᜀ());
							try
							{
								pdfMetafile.Quality = (long)this.ᜄ;
								pdfMetafile.Draw(pdfNewPage, new RectangleF(PointF.Empty, pdfNewPage.Size), true);
							}
							finally
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_16E;
									case 1:
										((IDisposable)pdfMetafile).Dispose();
										num2 = 0;
										continue;
									}
									if (pdfMetafile == null)
									{
										break;
									}
									num2 = 1;
								}
								IL_16E:;
							}
							this.ᜀ(A_0.\u171E().ᜀ()[num].ᜂ());
							A_0.\u171E().ᜀ()[num].ᜀ().Dispose();
							A_0.\u171E().ᜀ()[num].ᜀ(null);
							goto IL_273;
						}
						finally
						{
							num2 = 2;
							for (;;)
							{
								MemoryStream memoryStream;
								switch (num2)
								{
								case 0:
									goto IL_1FF;
								case 1:
									((IDisposable)memoryStream).Dispose();
									num2 = 0;
									continue;
								}
								if (memoryStream == null)
								{
									break;
								}
								num2 = 1;
							}
							IL_1FF:;
						}
						goto IL_202;
						IL_273:
						num++;
						num2 = 7;
						continue;
					case 1:
						goto IL_8D;
					case 2:
					{
						int num3;
						if (num3 >= count)
						{
							num2 = 8;
							continue;
						}
						this.ᜃ = this.ᜂ.Pages[num3];
						this.ᜀ(A_0.\u1717());
						num3++;
						num2 = 6;
						continue;
					}
					case 3:
					{
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						MemoryStream memoryStream = new MemoryStream();
						num2 = 0;
						continue;
					}
					case 4:
						goto IL_202;
					case 5:
					{
						int num3 = 0;
						num2 = 4;
						continue;
					}
					case 6:
						goto IL_202;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							goto IL_8D;
						}
						break;
					case 8:
						goto IL_21F;
					}
					break;
					IL_8D:
					num2 = 3;
					continue;
					IL_202:
					num2 = 2;
				}
			}
			IL_21F:
			this.ᜀ(A_0.\u171A().BuiltinDocumentProperties);
			A_0.ᜠ();
			return this.ᜂ;
		}
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x0038C69C File Offset: 0x0038B69C
	private PdfNewDocument ᜀ(spr\u1A69 A_0, List<string> A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜁ(A_0);
				int count = A_0.ᜤ().Count;
				this.ᜂ = this.ᜀ();
				int num = 0;
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_1FB;
					case 1:
					{
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						MemoryStream memoryStream = new MemoryStream();
						num2 = 2;
						continue;
					}
					case 2:
						try
						{
							MemoryStream memoryStream;
							A_0.ᜀ(num, 1, ImageType.Metafile, memoryStream, false, false, true);
							PdfSection pdfSection = this.ᜀ(this.ᜁ()[num]);
							PdfNewPage pdfNewPage = pdfSection.Pages.Add();
							this.ᜃ = pdfNewPage;
							PdfMetafile pdfMetafile = (PdfMetafile)PdfImage.FromImage(A_0.\u171E().ᜀ()[num].ᜀ());
							try
							{
								pdfMetafile.Quality = (long)this.ᜄ;
								pdfMetafile.ᜀ(pdfNewPage, new RectangleF(PointF.Empty, pdfNewPage.Size), true, A_1);
							}
							finally
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										((IDisposable)pdfMetafile).Dispose();
										num2 = 1;
										continue;
									case 1:
										goto IL_167;
									}
									if (pdfMetafile == null)
									{
										break;
									}
									num2 = 0;
								}
								IL_167:;
							}
							this.ᜀ(A_0.\u171E().ᜀ()[num].ᜂ());
							A_0.\u171E().ᜀ()[num].ᜀ().Dispose();
							A_0.\u171E().ᜀ()[num].ᜀ(null);
							goto IL_26C;
						}
						finally
						{
							num2 = 2;
							for (;;)
							{
								MemoryStream memoryStream;
								switch (num2)
								{
								case 0:
									goto IL_1F8;
								case 1:
									((IDisposable)memoryStream).Dispose();
									num2 = 0;
									continue;
								}
								if (memoryStream == null)
								{
									break;
								}
								num2 = 1;
							}
							IL_1F8:;
						}
						goto IL_1FB;
						IL_26C:
						num++;
						if (true)
						{
						}
						num2 = 7;
						continue;
					case 3:
					{
						int num3;
						if (num3 >= count)
						{
							num2 = 4;
							continue;
						}
						this.ᜃ = this.ᜂ.Pages[num3];
						this.ᜀ(A_0.\u1717());
						num3++;
						num2 = 6;
						continue;
					}
					case 4:
						goto IL_218;
					case 5:
					{
						int num3 = 0;
						num2 = 0;
						continue;
					}
					case 6:
						goto IL_1FB;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							goto IL_85;
						}
						break;
					case 8:
						goto IL_85;
					}
					break;
					IL_85:
					num2 = 1;
					continue;
					IL_1FB:
					num2 = 3;
				}
			}
			IL_218:
			this.ᜀ(A_0.\u171A().BuiltinDocumentProperties);
			A_0.ᜠ();
			return this.ᜂ;
		}
	}

	// Token: 0x04002C0B RID: 11275
	private const ImageType ᜀ = ImageType.Metafile;

	// Token: 0x04002C0C RID: 11276
	private List<PageSetup> ᜁ;

	// Token: 0x04002C0D RID: 11277
	private PdfNewDocument ᜂ;

	// Token: 0x04002C0E RID: 11278
	private PdfPageBase ᜃ;

	// Token: 0x04002C0F RID: 11279
	private int ᜄ = 80;
}
