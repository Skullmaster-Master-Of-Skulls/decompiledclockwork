using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.intern
{
	// Token: 0x02000592 RID: 1426
	public class PdfAnnotationsImp
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x0012CD1A File Offset: 0x0012BD1A
		public PdfAnnotationsImp(PdfWriter writer)
		{
			this.acroForm = new PdfAcroForm(writer);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0012CD39 File Offset: 0x0012BD39
		public bool HasValidAcroForm()
		{
			return this.acroForm.IsValid();
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x0012CD46 File Offset: 0x0012BD46
		public PdfAcroForm AcroForm
		{
			get
			{
				return this.acroForm;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (set) Token: 0x060030CA RID: 12490 RVA: 0x0012CD4E File Offset: 0x0012BD4E
		public int SigFlags
		{
			set
			{
				this.acroForm.SigFlags = value;
			}
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0012CD5C File Offset: 0x0012BD5C
		public void AddCalculationOrder(PdfFormField formField)
		{
			this.acroForm.AddCalculationOrder(formField);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0012CD6C File Offset: 0x0012BD6C
		public void AddAnnotation(PdfAnnotation annot)
		{
			if (annot.IsForm())
			{
				PdfFormField pdfFormField = (PdfFormField)annot;
				if (pdfFormField.Parent == null)
				{
					this.AddFormFieldRaw(pdfFormField);
					return;
				}
			}
			else
			{
				this.annotations.Add(annot);
			}
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0012CDA4 File Offset: 0x0012BDA4
		public void AddPlainAnnotation(PdfAnnotation annot)
		{
			this.annotations.Add(annot);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0012CDB4 File Offset: 0x0012BDB4
		private void AddFormFieldRaw(PdfFormField field)
		{
			this.annotations.Add(field);
			List<PdfFormField> kids = field.Kids;
			if (kids != null)
			{
				for (int i = 0; i < kids.Count; i++)
				{
					this.AddFormFieldRaw(kids[i]);
				}
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0012CDF5 File Offset: 0x0012BDF5
		public bool HasUnusedAnnotations()
		{
			return this.annotations.Count > 0;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0012CE05 File Offset: 0x0012BE05
		public void ResetAnnotations()
		{
			this.annotations = this.delayedAnnotations;
			this.delayedAnnotations = new List<PdfAnnotation>();
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0012CE20 File Offset: 0x0012BE20
		public PdfArray RotateAnnotations(PdfWriter writer, Rectangle pageSize)
		{
			PdfArray pdfArray = new PdfArray();
			int num = pageSize.Rotation % 360;
			int currentPageNumber = writer.CurrentPageNumber;
			for (int i = 0; i < this.annotations.Count; i++)
			{
				PdfAnnotation pdfAnnotation = this.annotations[i];
				int placeInPage = pdfAnnotation.PlaceInPage;
				if (placeInPage > currentPageNumber)
				{
					this.delayedAnnotations.Add(pdfAnnotation);
				}
				else
				{
					if (pdfAnnotation.IsForm())
					{
						if (!pdfAnnotation.IsUsed())
						{
							Dictionary<PdfTemplate, object> templates = pdfAnnotation.Templates;
							if (templates != null)
							{
								this.acroForm.AddFieldTemplates(templates);
							}
						}
						PdfFormField pdfFormField = (PdfFormField)pdfAnnotation;
						if (pdfFormField.Parent == null)
						{
							this.acroForm.AddDocumentField(pdfFormField.IndirectReference);
						}
					}
					if (pdfAnnotation.IsAnnotation())
					{
						pdfArray.Add(pdfAnnotation.IndirectReference);
						if (!pdfAnnotation.IsUsed())
						{
							PdfRectangle pdfRectangle = (PdfRectangle)pdfAnnotation.Get(PdfName.RECT);
							if (pdfRectangle != null)
							{
								int num2 = num;
								if (num2 != 90)
								{
									if (num2 != 180)
									{
										if (num2 == 270)
										{
											pdfAnnotation.Put(PdfName.RECT, new PdfRectangle(pdfRectangle.Bottom, pageSize.Right - pdfRectangle.Left, pdfRectangle.Top, pageSize.Right - pdfRectangle.Right));
										}
									}
									else
									{
										pdfAnnotation.Put(PdfName.RECT, new PdfRectangle(pageSize.Right - pdfRectangle.Left, pageSize.Top - pdfRectangle.Bottom, pageSize.Right - pdfRectangle.Right, pageSize.Top - pdfRectangle.Top));
									}
								}
								else
								{
									pdfAnnotation.Put(PdfName.RECT, new PdfRectangle(pageSize.Top - pdfRectangle.Bottom, pdfRectangle.Left, pageSize.Top - pdfRectangle.Top, pdfRectangle.Right));
								}
							}
						}
					}
					if (!pdfAnnotation.IsUsed())
					{
						pdfAnnotation.SetUsed();
						writer.AddToBody(pdfAnnotation, pdfAnnotation.IndirectReference);
					}
				}
			}
			return pdfArray;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0012D02C File Offset: 0x0012C02C
		public static PdfAnnotation ConvertAnnotation(PdfWriter writer, Annotation annot, Rectangle defaultRect)
		{
			switch (annot.AnnotationType)
			{
			case 1:
				return new PdfAnnotation(writer, annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry(), new PdfAction((Uri)annot.Attributes["url"]));
			case 2:
				return new PdfAnnotation(writer, annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry(), new PdfAction((string)annot.Attributes["file"]));
			case 3:
				return new PdfAnnotation(writer, annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry(), new PdfAction((string)annot.Attributes["file"], (string)annot.Attributes["destination"]));
			case 4:
				return new PdfAnnotation(writer, annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry(), new PdfAction((string)annot.Attributes["file"], (int)annot.Attributes["page"]));
			case 5:
				return new PdfAnnotation(writer, annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry(), new PdfAction((int)annot.Attributes["named"]));
			case 6:
				return new PdfAnnotation(writer, annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry(), new PdfAction((string)annot.Attributes["application"], (string)annot.Attributes["parameters"], (string)annot.Attributes["operation"], (string)annot.Attributes["defaultdir"]));
			case 7:
			{
				bool[] array = (bool[])annot.Attributes["parameters"];
				string text = (string)annot.Attributes["file"];
				string mimeType = (string)annot.Attributes["mime"];
				PdfFileSpecification fs;
				if (array[0])
				{
					fs = PdfFileSpecification.FileEmbedded(writer, text, text, null);
				}
				else
				{
					fs = PdfFileSpecification.FileExtern(writer, text);
				}
				return PdfAnnotation.CreateScreen(writer, new Rectangle(annot.GetLlx(), annot.GetLly(), annot.GetUrx(), annot.GetUry()), text, fs, mimeType, array[1]);
			}
			default:
				return new PdfAnnotation(writer, defaultRect.Left, defaultRect.Bottom, defaultRect.Right, defaultRect.Top, new PdfString(annot.Title, "UnicodeBig"), new PdfString(annot.Content, "UnicodeBig"));
			}
		}

		// Token: 0x04002177 RID: 8567
		protected internal PdfAcroForm acroForm;

		// Token: 0x04002178 RID: 8568
		protected internal List<PdfAnnotation> annotations;

		// Token: 0x04002179 RID: 8569
		protected internal List<PdfAnnotation> delayedAnnotations = new List<PdfAnnotation>();
	}
}
