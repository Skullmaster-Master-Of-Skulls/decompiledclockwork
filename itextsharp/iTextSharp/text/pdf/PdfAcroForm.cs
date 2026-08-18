using System;
using System.Collections.Generic;
using System.Text;
using System.util;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200058F RID: 1423
	public class PdfAcroForm : PdfDictionary
	{
		// Token: 0x06003089 RID: 12425 RVA: 0x0012BDA4 File Offset: 0x0012ADA4
		public PdfAcroForm(PdfWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x17000857 RID: 2135
		// (set) Token: 0x0600308A RID: 12426 RVA: 0x0012BDD4 File Offset: 0x0012ADD4
		public bool NeedAppearances
		{
			set
			{
				base.Put(PdfName.NEEDAPPEARANCES, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0012BDF0 File Offset: 0x0012ADF0
		public void AddFieldTemplates(Dictionary<PdfTemplate, object> ft)
		{
			foreach (PdfTemplate key in ft.Keys)
			{
				this.fieldTemplates[key] = ft[key];
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0012BE50 File Offset: 0x0012AE50
		public void AddDocumentField(PdfIndirectReference piref)
		{
			this.documentFields.Add(piref);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0012BE60 File Offset: 0x0012AE60
		public bool IsValid()
		{
			if (this.documentFields.Size == 0)
			{
				return false;
			}
			base.Put(PdfName.FIELDS, this.documentFields);
			if (this.sigFlags != 0)
			{
				base.Put(PdfName.SIGFLAGS, new PdfNumber(this.sigFlags));
			}
			if (this.calculationOrder.Size > 0)
			{
				base.Put(PdfName.CO, this.calculationOrder);
			}
			if (this.fieldTemplates.Count == 0)
			{
				return true;
			}
			PdfDictionary pdfDictionary = new PdfDictionary();
			foreach (PdfTemplate pdfTemplate in this.fieldTemplates.Keys)
			{
				PdfFormField.MergeResources(pdfDictionary, (PdfDictionary)pdfTemplate.Resources);
			}
			base.Put(PdfName.DR, pdfDictionary);
			base.Put(PdfName.DA, new PdfString("/Helv 0 Tf 0 g "));
			PdfDictionary pdfDictionary2 = (PdfDictionary)pdfDictionary.Get(PdfName.FONT);
			if (pdfDictionary2 != null)
			{
				this.writer.EliminateFontSubset(pdfDictionary2);
			}
			return true;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x0012BF78 File Offset: 0x0012AF78
		public void AddCalculationOrder(PdfFormField formField)
		{
			this.calculationOrder.Add(formField.IndirectReference);
		}

		// Token: 0x17000858 RID: 2136
		// (set) Token: 0x0600308F RID: 12431 RVA: 0x0012BF8C File Offset: 0x0012AF8C
		public int SigFlags
		{
			set
			{
				this.sigFlags |= value;
			}
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0012BF9C File Offset: 0x0012AF9C
		public void AddFormField(PdfFormField formField)
		{
			this.writer.AddAnnotation(formField);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0012BFAC File Offset: 0x0012AFAC
		public PdfFormField AddHtmlPostButton(string name, string caption, string value, string url, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfAction action = PdfAction.CreateSubmitForm(url, null, 4);
			PdfFormField pdfFormField = new PdfFormField(this.writer, llx, lly, urx, ury, action);
			this.SetButtonParams(pdfFormField, 65536, name, value);
			this.DrawButton(pdfFormField, caption, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0012C004 File Offset: 0x0012B004
		public PdfFormField AddResetButton(string name, string caption, string value, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfAction action = PdfAction.CreateResetForm(null, 0);
			PdfFormField pdfFormField = new PdfFormField(this.writer, llx, lly, urx, ury, action);
			this.SetButtonParams(pdfFormField, 65536, name, value);
			this.DrawButton(pdfFormField, caption, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0012C058 File Offset: 0x0012B058
		public PdfFormField AddMap(string name, string value, string url, PdfContentByte appearance, float llx, float lly, float urx, float ury)
		{
			PdfAction action = PdfAction.CreateSubmitForm(url, null, 20);
			PdfFormField pdfFormField = new PdfFormField(this.writer, llx, lly, urx, ury, action);
			this.SetButtonParams(pdfFormField, 65536, name, null);
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			pdfAppearance.Add(appearance);
			pdfFormField.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, pdfAppearance);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0012C0C4 File Offset: 0x0012B0C4
		public void SetButtonParams(PdfFormField button, int characteristics, string name, string value)
		{
			button.Button = characteristics;
			button.Flags = 4;
			button.SetPage();
			button.FieldName = name;
			if (value != null)
			{
				button.ValueAsString = value;
			}
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0012C0F0 File Offset: 0x0012B0F0
		public void DrawButton(PdfFormField button, string caption, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			pdfAppearance.DrawButton(0f, 0f, urx - llx, ury - lly, caption, font, fontSize);
			button.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, pdfAppearance);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0012C140 File Offset: 0x0012B140
		public PdfFormField AddHiddenField(string name, string value)
		{
			PdfFormField pdfFormField = PdfFormField.CreateEmpty(this.writer);
			pdfFormField.FieldName = name;
			pdfFormField.ValueAsName = value;
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0012C170 File Offset: 0x0012B170
		public PdfFormField AddSingleLineTextField(string name, string text, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateTextField(this.writer, false, false, 0);
			this.SetTextFieldParams(pdfFormField, text, name, llx, lly, urx, ury);
			this.DrawSingleLineOfText(pdfFormField, text, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0012C1B8 File Offset: 0x0012B1B8
		public PdfFormField AddMultiLineTextField(string name, string text, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateTextField(this.writer, true, false, 0);
			this.SetTextFieldParams(pdfFormField, text, name, llx, lly, urx, ury);
			this.DrawMultiLineOfText(pdfFormField, text, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0012C200 File Offset: 0x0012B200
		public PdfFormField AddSingleLinePasswordField(string name, string text, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateTextField(this.writer, false, true, 0);
			this.SetTextFieldParams(pdfFormField, text, name, llx, lly, urx, ury);
			this.DrawSingleLineOfText(pdfFormField, text, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0012C248 File Offset: 0x0012B248
		public void SetTextFieldParams(PdfFormField field, string text, string name, float llx, float lly, float urx, float ury)
		{
			field.SetWidget(new Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_INVERT);
			field.ValueAsString = text;
			field.DefaultValueAsString = text;
			field.FieldName = name;
			field.Flags = 4;
			field.SetPage();
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0012C284 File Offset: 0x0012B284
		public void DrawSingleLineOfText(PdfFormField field, string text, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			PdfAppearance pdfAppearance2 = (PdfAppearance)pdfAppearance.Duplicate;
			pdfAppearance2.SetFontAndSize(font, fontSize);
			pdfAppearance2.ResetRGBColorFill();
			field.DefaultAppearanceString = pdfAppearance2;
			pdfAppearance.DrawTextField(0f, 0f, urx - llx, ury - lly);
			pdfAppearance.BeginVariableText();
			pdfAppearance.SaveState();
			pdfAppearance.Rectangle(3f, 3f, urx - llx - 6f, ury - lly - 6f);
			pdfAppearance.Clip();
			pdfAppearance.NewPath();
			pdfAppearance.BeginText();
			pdfAppearance.SetFontAndSize(font, fontSize);
			pdfAppearance.ResetRGBColorFill();
			pdfAppearance.SetTextMatrix(4f, (ury - lly) / 2f - fontSize * 0.3f);
			pdfAppearance.ShowText(text);
			pdfAppearance.EndText();
			pdfAppearance.RestoreState();
			pdfAppearance.EndVariableText();
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, pdfAppearance);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0012C37C File Offset: 0x0012B37C
		public void DrawMultiLineOfText(PdfFormField field, string text, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			PdfAppearance pdfAppearance2 = (PdfAppearance)pdfAppearance.Duplicate;
			pdfAppearance2.SetFontAndSize(font, fontSize);
			pdfAppearance2.ResetRGBColorFill();
			field.DefaultAppearanceString = pdfAppearance2;
			pdfAppearance.DrawTextField(0f, 0f, urx - llx, ury - lly);
			pdfAppearance.BeginVariableText();
			pdfAppearance.SaveState();
			pdfAppearance.Rectangle(3f, 3f, urx - llx - 6f, ury - lly - 6f);
			pdfAppearance.Clip();
			pdfAppearance.NewPath();
			pdfAppearance.BeginText();
			pdfAppearance.SetFontAndSize(font, fontSize);
			pdfAppearance.ResetRGBColorFill();
			pdfAppearance.SetTextMatrix(4f, 5f);
			StringTokenizer stringTokenizer = new StringTokenizer(text, "\n");
			float num = ury - lly;
			while (stringTokenizer.HasMoreTokens())
			{
				num -= fontSize * 1.2f;
				pdfAppearance.ShowTextAligned(0, stringTokenizer.NextToken(), 3f, num, 0f);
			}
			pdfAppearance.EndText();
			pdfAppearance.RestoreState();
			pdfAppearance.EndVariableText();
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, pdfAppearance);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0012C49C File Offset: 0x0012B49C
		public PdfFormField AddCheckBox(string name, string value, bool status, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateCheckBox(this.writer);
			this.SetCheckBoxParams(pdfFormField, name, value, status, llx, lly, urx, ury);
			this.DrawCheckBoxAppearences(pdfFormField, value, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0012C4E0 File Offset: 0x0012B4E0
		public void SetCheckBoxParams(PdfFormField field, string name, string value, bool status, float llx, float lly, float urx, float ury)
		{
			field.SetWidget(new Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_TOGGLE);
			field.FieldName = name;
			if (status)
			{
				field.ValueAsName = value;
				field.AppearanceState = value;
			}
			else
			{
				field.ValueAsName = "Off";
				field.AppearanceState = "Off";
			}
			field.Flags = 4;
			field.SetPage();
			field.BorderStyle = new PdfBorderDictionary(1f, 0);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0012C554 File Offset: 0x0012B554
		public void DrawCheckBoxAppearences(PdfFormField field, string value, float llx, float lly, float urx, float ury)
		{
			BaseFont bf = BaseFont.CreateFont("ZapfDingbats", "Cp1252", false);
			float num = ury - lly;
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			PdfAppearance pdfAppearance2 = (PdfAppearance)pdfAppearance.Duplicate;
			pdfAppearance2.SetFontAndSize(bf, num);
			pdfAppearance2.ResetRGBColorFill();
			field.DefaultAppearanceString = pdfAppearance2;
			pdfAppearance.DrawTextField(0f, 0f, urx - llx, ury - lly);
			pdfAppearance.SaveState();
			pdfAppearance.ResetRGBColorFill();
			pdfAppearance.BeginText();
			pdfAppearance.SetFontAndSize(bf, num);
			pdfAppearance.ShowTextAligned(1, "4", (urx - llx) / 2f, (ury - lly) / 2f - num * 0.3f, 0f);
			pdfAppearance.EndText();
			pdfAppearance.RestoreState();
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, value, pdfAppearance);
			PdfAppearance pdfAppearance3 = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			pdfAppearance3.DrawTextField(0f, 0f, urx - llx, ury - lly);
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, "Off", pdfAppearance3);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0012C66C File Offset: 0x0012B66C
		public PdfFormField GetRadioGroup(string name, string defaultValue, bool noToggleToOff)
		{
			PdfFormField pdfFormField = PdfFormField.CreateRadioButton(this.writer, noToggleToOff);
			pdfFormField.FieldName = name;
			pdfFormField.ValueAsName = defaultValue;
			return pdfFormField;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0012C695 File Offset: 0x0012B695
		public void AddRadioGroup(PdfFormField radiogroup)
		{
			this.AddFormField(radiogroup);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0012C6A0 File Offset: 0x0012B6A0
		public PdfFormField AddRadioButton(PdfFormField radiogroup, string value, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateEmpty(this.writer);
			pdfFormField.SetWidget(new Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_TOGGLE);
			string text = ((PdfName)radiogroup.Get(PdfName.V)).ToString().Substring(1);
			if (text.Equals(value))
			{
				pdfFormField.AppearanceState = value;
			}
			else
			{
				pdfFormField.AppearanceState = "Off";
			}
			this.DrawRadioAppearences(pdfFormField, value, llx, lly, urx, ury);
			radiogroup.AddKid(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0012C720 File Offset: 0x0012B720
		public void DrawRadioAppearences(PdfFormField field, string value, float llx, float lly, float urx, float ury)
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			pdfAppearance.DrawRadioField(0f, 0f, urx - llx, ury - lly, true);
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, value, pdfAppearance);
			PdfAppearance pdfAppearance2 = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			pdfAppearance2.DrawRadioField(0f, 0f, urx - llx, ury - lly, false);
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, "Off", pdfAppearance2);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0012C7AC File Offset: 0x0012B7AC
		public PdfFormField AddSelectList(string name, string[] options, string defaultValue, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateList(this.writer, options, 0);
			this.SetChoiceParams(pdfFormField, name, defaultValue, llx, lly, urx, ury);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in options)
			{
				stringBuilder.Append(value).Append('\n');
			}
			this.DrawMultiLineOfText(pdfFormField, stringBuilder.ToString(), font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0012C828 File Offset: 0x0012B828
		public PdfFormField AddSelectList(string name, string[,] options, string defaultValue, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateList(this.writer, options, 0);
			this.SetChoiceParams(pdfFormField, name, defaultValue, llx, lly, urx, ury);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < options.GetLength(0); i++)
			{
				stringBuilder.Append(options[i, 1]).Append('\n');
			}
			this.DrawMultiLineOfText(pdfFormField, stringBuilder.ToString(), font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0012C8A4 File Offset: 0x0012B8A4
		public PdfFormField AddComboBox(string name, string[] options, string defaultValue, bool editable, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateCombo(this.writer, editable, options, 0);
			this.SetChoiceParams(pdfFormField, name, defaultValue, llx, lly, urx, ury);
			if (defaultValue == null)
			{
				defaultValue = options[0];
			}
			this.DrawSingleLineOfText(pdfFormField, defaultValue, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0012C8F8 File Offset: 0x0012B8F8
		public PdfFormField AddComboBox(string name, string[,] options, string defaultValue, bool editable, BaseFont font, float fontSize, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateCombo(this.writer, editable, options, 0);
			this.SetChoiceParams(pdfFormField, name, defaultValue, llx, lly, urx, ury);
			string text = null;
			for (int i = 0; i < options.GetLength(0); i++)
			{
				if (options[i, 0].Equals(defaultValue))
				{
					text = options[i, 1];
					break;
				}
			}
			if (text == null)
			{
				text = options[0, 1];
			}
			this.DrawSingleLineOfText(pdfFormField, text, font, fontSize, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0012C980 File Offset: 0x0012B980
		public void SetChoiceParams(PdfFormField field, string name, string defaultValue, float llx, float lly, float urx, float ury)
		{
			field.SetWidget(new Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_INVERT);
			if (defaultValue != null)
			{
				field.ValueAsString = defaultValue;
				field.DefaultValueAsString = defaultValue;
			}
			field.FieldName = name;
			field.Flags = 4;
			field.SetPage();
			field.BorderStyle = new PdfBorderDictionary(2f, 0);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0012C9DC File Offset: 0x0012B9DC
		public PdfFormField AddSignature(string name, float llx, float lly, float urx, float ury)
		{
			PdfFormField pdfFormField = PdfFormField.CreateSignature(this.writer);
			this.SetSignatureParams(pdfFormField, name, llx, lly, urx, ury);
			this.DrawSignatureAppearences(pdfFormField, llx, lly, urx, ury);
			this.AddFormField(pdfFormField);
			return pdfFormField;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0012CA18 File Offset: 0x0012BA18
		public void SetSignatureParams(PdfFormField field, string name, float llx, float lly, float urx, float ury)
		{
			field.SetWidget(new Rectangle(llx, lly, urx, ury), PdfAnnotation.HIGHLIGHT_INVERT);
			field.FieldName = name;
			field.Flags = 4;
			field.SetPage();
			field.MKBorderColor = BaseColor.BLACK;
			field.MKBackgroundColor = BaseColor.WHITE;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0012CA68 File Offset: 0x0012BA68
		public void DrawSignatureAppearences(PdfFormField field, float llx, float lly, float urx, float ury)
		{
			PdfAppearance pdfAppearance = PdfAppearance.CreateAppearance(this.writer, urx - llx, ury - lly);
			pdfAppearance.SetGrayFill(1f);
			pdfAppearance.Rectangle(0f, 0f, urx - llx, ury - lly);
			pdfAppearance.Fill();
			pdfAppearance.SetGrayStroke(0f);
			pdfAppearance.SetLineWidth(1f);
			pdfAppearance.Rectangle(0.5f, 0.5f, urx - llx - 0.5f, ury - lly - 0.5f);
			pdfAppearance.ClosePathStroke();
			pdfAppearance.SaveState();
			pdfAppearance.Rectangle(1f, 1f, urx - llx - 2f, ury - lly - 2f);
			pdfAppearance.Clip();
			pdfAppearance.NewPath();
			pdfAppearance.RestoreState();
			field.SetAppearance(PdfAnnotation.APPEARANCE_NORMAL, pdfAppearance);
		}

		// Token: 0x04002166 RID: 8550
		private PdfWriter writer;

		// Token: 0x04002167 RID: 8551
		private Dictionary<PdfTemplate, object> fieldTemplates = new Dictionary<PdfTemplate, object>();

		// Token: 0x04002168 RID: 8552
		private PdfArray documentFields = new PdfArray();

		// Token: 0x04002169 RID: 8553
		private PdfArray calculationOrder = new PdfArray();

		// Token: 0x0400216A RID: 8554
		private int sigFlags;
	}
}
